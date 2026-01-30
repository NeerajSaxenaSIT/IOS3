Imports DevExpress.Data
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes
Imports LidorSystems.IntegralUI.Lists
Imports DevExpress.XtraEditors.Repository
Imports IOS.Library

Public Class frmCMView

#Region "Variables"

    Private dtTech As DataTable = Nothing
    Private dtParamfilter As New DataTable
    Private dtMOList As DataTable = Nothing
    Private dtParameterList As DataTable = Nothing
    Private dtMoObjects As DataTable = Nothing
    Private dtMOPKColList As DataTable = Nothing
    Private p As Point
    Private SelectedRowIndex As Integer = -1
    Private RowListToHide As New List(Of Integer)
    Private dtCMView As New DataTable
    Private dtHistChanges As New DataTable
    Private datetimeEdit As RepositoryItemDateEdit
    Private datetimeEditHC As RepositoryItemDateEdit
    Private riMemoEdit As RepositoryItemMemoEdit

#End Region

#Region "Methods"

    Private Sub ConfigurCMViewForm(ByVal frmName As String)
        Dim form As IOS.Configuration.EntityModel.IOSForm = configMgr.FindFormByName(frmName)
        If Not form Is Nothing Then
            Dim counter As Integer = 0
            ConfigurForm(Me, frmName, counter)

            Dim tsmiConstols As List(Of Object) = New List(Of Object) From {
                tsmi_CompareVsSelection, cm_OT_tsmi_copy, cm_OT_tsmi_paste, cm_OT_tsmi_CheckChilds, tsmi_OT_UnCheck
            }

            For Each control As Object In tsmiConstols
                Dim modelControl As Configuration.EntityModel.Control = form.FindControlByName(control.Name)
                If Not modelControl Is Nothing Then
                    control.Enabled = modelControl.DefaultEnable
                    control.Visible = modelControl.DefaultVisible
                End If
            Next

        End If
    End Sub

    Sub HideShowLeftControls()
        'txtSearchOuter.Enabled = True
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
        ClearComboBox(cmbTemplate, "Select Template")
        ClearComboBox(cmbTargetObject, "Object Type")
    End Sub

    Private Function GetTechnologyName(ByVal tech As String, ByVal vendor As String, ByVal returnObjectColumnsName As String) As String
        Dim rows() As DataRow = dt_IOS_ObjectConfig.Select("Vendor='" & vendor & "' AND Technology='" & tech & "' AND ParamHistory=1")
        If (rows.Count > 0) Then
            Return rows(0)(returnObjectColumnsName).ToString
        End If
        Return ""
    End Function

    Private Sub GetMOList(ByVal vendorName As String, Optional ByVal technology As String = Nothing, Optional ByVal templateName As String = Nothing)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim strConnection As String
            Dim sqlParam As String

            Dim parray()() As String = {
                New String() {"@vendorName", "'" & vendorName & "'"},
                New String() {"@techName", IIf(technology Is Nothing, "NULL", "'" & technology & "'")},
                New String() {"@templateName", IIf(templateName Is Nothing, "NULL", "'" & templateName & "'")}
            }

            strConnection = GetSQL(3500, parray)(0)
            sqlParam = GetSQL(3500, parray)(1)

            dtMOList = New System.Data.DataTable()
            dtMOList = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
            lstViewMO.Columns.Clear()
            lstViewMO.DataSource = dtMOList
            If lstViewMO.Columns.Count > 0 Then
                lstViewMO.Columns(0).Visible = False
            End If

            If lstViewMO.Columns.Count > 0 Then
                lstViewMO.Columns(2).Visible = True
                lstViewMO.Columns(2).Width = 80
                lstViewMO.Columns(2).Caption = "Track Changes"

                Dim img As RepositoryItemImageComboBox = New RepositoryItemImageComboBox()
                img.Items.Add(New DevExpress.XtraEditors.Controls.ImageComboBoxItem("YES", 0))
                img.Items.Add(New DevExpress.XtraEditors.Controls.ImageComboBoxItem("NO", 1))
                img.SmallImages = ImgList
                img.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center
                img.Buttons(0).Visible = False

                lstViewMO.Columns("Tracked").ColumnEdit = img
                lstViewMO.Columns("Tracked").OptionsColumn.AllowEdit = False
            End If

        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub GetListOfParameters(ByVal vendorName As String, ByVal MO As String, Optional tech As String = Nothing, Optional ByVal IsAllParam As Integer = 0)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim strConnection As String
            Dim sqlParam As String

            Dim parray()() As String = {
                New String() {"@vendor", "'" & vendorName & "'"},
                New String() {"@mo", "'" & MO & "'"},
                New String() {"@tech", IIf(tech Is Nothing, "NULL", "'" & tech & "'")},
                New String() {"@IsAllParam", IsAllParam}
            }

            strConnection = GetSQL(3502, parray)(0)
            sqlParam = GetSQL(3502, parray)(1)

            dtParameterList = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
            tlParameterList.DataSource = Nothing
            tlParameterList.Columns.Clear()
            txtSearchPH_KeyUp(Nothing, Nothing)
            tlParameterList.DataSource = dtParameterList
            If tlParameterList.Columns.Count > 0 Then
                tlParameterList.Columns(2).Caption = "Parameter Name"
                tlParameterList.Columns(0).Visible = False
            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub GetObjectsOfMO(vendorName As String, moName As String)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim strConnection As String
            Dim sqlParam As String

            Dim parray()() As String = {
                                            New String() {"@vendor", "'" & vendorName & "'"},
                                            New String() {"@mo", "'" & moName & "'"}
                                         }

            strConnection = GetSQL(3503, parray)(0)
            sqlParam = GetSQL(3503, parray)(1)

            dtMoObjects = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
            gcObject.DataSource = Nothing
            gvObject.Columns.Clear()
            gcObject.DataSource = dtMoObjects
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub LoadObjectTypeCombo(ByVal moName As String)
        Dim strConnection As String = connStrIOSServer.ToString
        dtMOPKColList = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, "EXEC CM_GetMOTablePKColumnsWithOrdinal '" & cmbVendor.SelectedItem.ToString & "', '" & moName & "'")
        BindDevExComboBoxWithTagMember(cmbTargetObject, dtMOPKColList, "COLUMN_NAME", "COLUMN_NAME", "Select Object", "ORDINAL_POSITION", True)
    End Sub

    Private Sub GetChangesForMO(ByVal moName As String, vendor As String, offset As Integer, noOfRows As Integer)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            gvHistChanges.FormatConditions.Clear()

            Dim filterQry As String = GetFilterQueryFromObjectTree()

            Dim dtChangeForMo As New DataTable
            dtChangeForMo = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, "EXEC [CM_GetChangesForMOtoGrid] '" & cmbVendor.SelectedItem.ToString & "', '" & moName & "'," & offset & "," & IIf(noOfRows = 0, noOfRows, Val(txtQueryBatchSize.Text)) & ",'" & filterQry & "'")
            Library.IOSDevExpressGrid.PopulateDataInGrid(gcHistChanges, gcHistChanges.MainView, dtChangeForMo, "ALL")

            txtQueryBatchSize.Tag = offset
            gcHistChanges.Tag = "MOChanges"

            SelectedRowIndex = -1
            gvHistChanges.FormatConditions.Clear()
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub GetChangesForMOSelection(ByVal moName As String, vendor As String, filterQry As String)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            gvHistChanges.FormatConditions.Clear()

            Dim dtChangeForMoSel As New DataTable
            dtChangeForMoSel = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, "EXEC [CM_GetChangesForMOSelection] '" & cmbVendor.SelectedItem.ToString & "', '" & moName & "','" & filterQry & "'")
            Library.IOSDevExpressGrid.PopulateDataInGrid(gcHistChanges, gcHistChanges.MainView, dtChangeForMoSel, "ALL")

            'txtQueryBatchSize.Tag = offset
            gcHistChanges.Tag = "MOChanges"

            SelectedRowIndex = -1
            gvHistChanges.FormatConditions.Clear()
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub GetSelectedNodes(ByVal nodes As TreeListNodes, ByRef list As List(Of KeyValuePair(Of String, List(Of String))))
        Dim obj As KeyValuePair(Of String, List(Of String))
        For Each node As TreeListNode In nodes
            If node.Checked Then
                Dim ndText As String = node.GetDisplayText(cmbTargetObject.Properties.Items(1).ToString)
                If ndText.ToUpper <> "PLMN" Then
                    obj = Nothing
                    If list.Exists(Function(x) x.Key = node.Tag) Then
                        obj = list.FirstOrDefault(Function(x) x.Key = node.Tag)
                        obj.Value.Add(node.GetDisplayText(cmbTargetObject.Properties.Items(1).ToString))
                    Else
                        Dim value As New List(Of String)
                        value.Add(node.GetDisplayText(cmbTargetObject.Properties.Items(1).ToString))
                        obj = New KeyValuePair(Of String, List(Of String))(node.Tag, value)
                        list.Add(obj)
                    End If
                End If
            End If
            GetSelectedNodes(node.Nodes, list)
        Next
    End Sub

    'Public Function TreeView_Checked2String(ByVal targetObject As String, ByVal outputtype As String) As String
    '    Dim nodelevel As Integer
    '    Dim outputstr As New StringBuilder()
    '    If outputtype = "ObjectNameWild" Then
    '        outputstr.Append(" LIKE ")
    '    ElseIf outputtype = "Naked" Then
    '        outputstr.Append("")
    '    ElseIf outputtype = "TAGS_CM" Then
    '        outputstr.Append("")
    '    ElseIf outputtype = "ObjectType" Then
    '        outputstr.Append("")
    '    Else
    '        outputstr.Append("IN (")
    '    End If
    '    nodelevel = 3

    '    nodelevel = Treeview_GetNodeLevel(targetObject)

    '    For Each nd As TreeNode In tvObjectTree.Nodes
    '        outputstr.Append(TreeView_Checked2String_Level(nd, nodelevel, outputtype))
    '    Next

    '    Dim outputfinal As String = Nothing
    '    If outputtype = "ObjectNameWild" Then
    '        outputfinal = Mid(outputstr.ToString, 1, outputstr.ToString.Length - 9)
    '    ElseIf outputtype = "Naked" Then
    '        outputfinal = outputstr.ToString.TrimEnd(",")
    '    Else
    '        outputfinal = outputstr.ToString.TrimEnd(",") + ")"
    '    End If

    '    Return outputfinal
    'End Function

    Private Sub EnableSearchCheckBox()
        If cmbVendor.SelectedIndex > 0 And cmbTechnology.SelectedIndex > 0 Then
            chkSearchAllParameter.Enabled = True
        Else
            chkSearchAllParameter.Enabled = False
            chkSearchAllParameter.Checked = False
        End If
    End Sub

    Private Sub LoadObjectTreeFromMo(strColumns As String)
        Dim dsObjectTree As DataSet = Nothing
        Dim node As TreeListNode = lstViewMO.FocusedNode
        Dim data As DataRowView = lstViewMO.GetDataRecordByNode(node)
        Dim moName As String = Nothing
        If data IsNot Nothing Then
            moName = data.Item(0).ToString
        End If

        dsObjectTree = IOS.DataLibrary.DataAccessorODBC.GetDataSet(connStrIOSServer, "EXEC CM_GetObjectTreeData '" & cmbVendor.SelectedItem.ToString & "', '" & strColumns & "', '" & moName & "'")
        FillObjectTree(dsObjectTree, tvObjectTree)
    End Sub

    Private Sub FillObjectTree(dsData As DataSet, ByRef treeList As TreeList)
        Try
            treeList.Cursor = Cursors.WaitCursor
            treeList.BeginUnboundLoad()
            Application.DoEvents()

            Dim colList() As String = dtMOPKColList.AsEnumerable().Select(Function(r) r.Field(Of String)(0)).ToArray()
            treeList.Columns.Clear()
            treeList.PupulateTreeListColumn(colList, cmbTargetObject.Properties.Items(1).ToString)

            treeList.Nodes.Clear()
            treeList.OptionsView.AutoWidth = True

            Dim dbNode As TreeListNode = Nothing
            Dim tlNode As TreeListNode = treeList.Nodes.Add(New Object() {"PLMN"})

            'Dim rootn As TreeListNode = Nothing
            'rootn.Text = "PLMN"
            'rootn.ImageKey = "EMPTY"
            'rootn.SelectedImageKey = "EMPTY"
            'tree.Nodes.Clear()
            'tree.Nodes.Add(rootn)
            'Dim tNode As New TreeNode
            ' tNode = tree.Nodes(0)

            Dim dtParent As DataTable = dsData.Tables(0).DefaultView.ToTable(True, dsData.Tables(0).Columns(dsData.Tables(0).Columns.Count - 1).ColumnName)
            For Each drParent As DataRow In dtParent.Rows
                dbNode = treeList.AppendNode(New Object() {drParent(0).ToString}, tlNode)
                dbNode.Tag = dsData.Tables(0).Columns(dsData.Tables(0).Columns.Count - 1).ColumnName

                'Dim roottn As TreeNode = New TreeNode()
                'roottn.Text = drParent(0).ToString
                'roottn.Tag = dtData.Columns(dtData.Columns.Count - 1).ColumnName
                'roottn.ImageKey = "EMPTY"
                'roottn.SelectedImageKey = "EMPTY"
                'tNode.Nodes.Add(roottn)
                PopulateObjectTree(treeList, drParent.Table.Columns(0).ColumnName, drParent(0).ToString, dbNode, dsData.Tables(0))
                'PopulateTreeList(tree, drParent(0).ToString, dbNode, dsData)
            Next
            System.GC.Collect()
        Catch ex As Exception
        Finally
            treeList.EndUnboundLoad()
            If treeList.Nodes.Count > 0 Then
                treeList.SelectNode(treeList.Nodes(0))
                treeList.SetFocusedNode(treeList.Nodes(0))
                treeList.CollapseAll()
                treeList.ExpandToLevel(0)
            End If
            treeList.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub PopulateTreeList(ByRef tl As TreeList, ParentID As String, rNode As TreeListNode, ds As DataSet, Optional filterObject As String = Nothing)
        Dim foundRows() As DataRow = Nothing

        foundRows = ds.Tables(0).Select(cmbTargetObject.Properties.Items(1).ToString & " = " & Chr(39) & ParentID & Chr(39))

        Dim dsObjectTree As New DataSet
        'If filterObject IsNot Nothing Then
        '    dsObjectTree = FilterObjectTree(objectType, filterObject, tech)
        '    foundRows = dsObjectTree.Tables(0).Select("ParentID='" & ParentID & "'")
            '    tblname = "0"
            'Else
            '    dsObjectTree = ds
            'End If

            If foundRows.Length > 0 Then
            'Dim imgList As ImageList = tl.SelectImageList
            For Each row As DataRow In foundRows
                If row.Item(0).ToString <> "" Then
                    Dim tlItem As Object() = row.ItemArray.AsEnumerable().ToArray

                    Dim parentnode As TreeListNode = tl.AppendNode(New Object() {tlItem(1)}, rNode)
                    PopulateTreeList(tl, row.Item(1), parentnode, ds, filterObject)
                    If filterObject IsNot Nothing Then
                        parentnode.ExpandAll()
                    End If
                End If
            Next row
        End If
    End Sub

    Private Sub PopulateObjectTree(ByRef treeList As TreeList, ByVal parentColName As String, ByVal inParentID As String, ByVal inTreeNode As TreeListNode, ByVal dt As DataTable)
        Try
            Dim index As Integer = dt.Columns.Count - 1
            Dim childNode As TreeListNode = Nothing
            If index = 0 Then
                Exit Sub
            End If
            inTreeNode.Tag = dt.Columns(index - 1).ToString
            Dim dataRows() As DataRow = dt.Select(parentColName & "='" & inParentID & "'")
            For Each dataRow As DataRow In dataRows
                childNode = treeList.AppendNode(New Object() {dataRow(index - 1).ToString}, inTreeNode)
                childNode.Tag = dt.Columns(index - 1).ToString
                For ind As Integer = dt.Columns.Count - 2 To 1 Step -1
                    childNode = treeList.AppendNode(New Object() {dataRow(ind - 1).ToString}, childNode)
                    childNode.Tag = dt.Columns(ind - 1).ToString
                Next
            Next

            'Next

            'Dim dtChild As New DataTable
            'For i As Integer = dt.Columns.Count - 1 To 0 Step -1
            '    If parentColName = dt.Columns(i).ColumnName Then
            '        If i - 1 < 0 Then Exit Sub
            '        dtChild = dt.DefaultView.ToTable(True, dt.Columns(i - 1).ColumnName, parentColName)
            '        Exit For
            '    End If
            'Next i

            'Dim treeListNode As TreeListNode = Nothing
            'inTreeNode.Nodes.Clear()
            'Dim dataRows() As DataRow = dtChild.Select(parentColName & "='" & inParentID & "'")
            'For Each drParent As DataRow In dataRows
            '    treeListNode = treeList.AppendNode(New Object() {drParent(0).ToString}, inTreeNode)
            '    treeListNode.Tag = drParent.Table.Columns(0).ColumnName
            '    'Dim roottn As TreeNode = New TreeNode()
            '    'roottn.Text = drParent(0).ToString
            '    'roottn.Tag = drParent.Table.Columns(0).ColumnName
            '    'roottn.ImageKey = "EMPTY"
            '    'roottn.SelectedImageKey = "EMPTY"
            '    'inTreeNode.Nodes.Add(roottn)
            '    PopulateObjectTree(treeList, drParent.Table.Columns(0).ColumnName, drParent(0).ToString, treeListNode, dt)
            'Next
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Function Treeview_GetNodeLevel(Optional ByVal subtech As String = Nothing) As Integer 'TODO
        If subtech = "PLMN" Then Return 0
        Dim dr() As DataRow = dtMOPKColList.Select("COLUMN_NAME=" & Chr(39) & subtech & Chr(39))
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
        Dim dr() As DataRow = dtMOPKColList.Select("ORDINAL_POSITION=" & Chr(39) & id & Chr(39))
        If (dr.Count > 0) Then
            If dr(0)("ORDINAL_POSITION").ToString <> 0 Then
                level = CInt(dr(0)("ORDINAL_POSITION").ToString) - 1
            End If
        End If

        Return level
    End Function

    Private Sub GetParamDescListForMO(MOTblName As String)
        gcParamDesc.UseWaitCursor = True
        gcParamDesc.SuspendLayout()

        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@MOTblName", MOTblName}
        }
        strConnection = GetSQL(3504, parray)(0)
        sqlParam = GetSQL(3504, parray)(1)

        Dim dt As DataTable = DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        If dt.Rows.Count > 0 Then

            'Dim dtClone As DataTable = dt.Clone()
            Dim lstColumn As New List(Of Integer)

            For Each dr As DataRow In dt.Rows

                'Dim drClone As DataRow = dtClone.NewRow()

                Dim rowArray() As Object = dr.ItemArray()
                For iCntr = 0 To rowArray.Count - 1
                    'drClone(iCntr) = rowArray(iCntr).ToString.Replace(vbNewLine, " ").Replace(vbCrLf, " ").Replace(Environment.NewLine, " ").Replace(Chr(13), " ").Replace(vbLf, " ")
                    If rowArray(iCntr).ToString.Contains(vbNewLine) Or rowArray(iCntr).ToString.Contains(vbCrLf) Or rowArray(iCntr).ToString.Contains(Environment.NewLine) Or rowArray(iCntr).ToString.Contains(Chr(13)) Or rowArray(iCntr).ToString.Contains(vbLf) Then
                        If Not lstColumn.Contains(iCntr) Then
                            lstColumn.Add(iCntr)
                        End If
                    End If
                Next

                'dtClone.Rows.Add(drClone)

            Next

            'IOSDevExpressGrid.PopulateDataInGrid(gcParamDesc, gvParamDesc, dtClone, "ALL")
            IOSDevExpressGrid.PopulateDataInGrid(gcParamDesc, gvParamDesc, dt, "ALL")

            For iCntr = 0 To gvParamDesc.Columns.Count - 1

                If lstColumn.Contains(iCntr) Then

                    riMemoEdit = New RepositoryItemMemoEdit()
                    riMemoEdit.ReadOnly = True
                    riMemoEdit.Appearance.Options.UseTextOptions = True
                    riMemoEdit.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                    gcParamDesc.RepositoryItems.Add(riMemoEdit)
                    gvParamDesc.Columns(iCntr).ColumnEdit = riMemoEdit
                    gvParamDesc.OptionsView.RowAutoHeight = True

                End If

            Next
            gvParamDesc.OptionsView.RowAutoHeight = True

        Else
            IOSDevExpressGrid.ClearGrid(gcParamDesc)
        End If

        gcParamDesc.ResumeLayout()
        gcParamDesc.Refresh()
        gcParamDesc.UseWaitCursor = False
    End Sub

#End Region

#Region "Events"

    Private Sub btnGetTemplate_Click(sender As Object, e As EventArgs) Handles btnGetTemplate.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            gvCMView.FormatConditions.Clear()
            chkSearchAllParameter.Checked = False
            If cmbTemplate.SelectedIndex > 0 AndAlso cmbVendor.SelectedIndex > 0 AndAlso cmbTechnology.SelectedIndex > 0 Then
                GetMOList(cmbVendor.SelectedItem.ToString, cmbTechnology.SelectedItem.ToString, cmbTemplate.SelectedItem.ToString)
                LoadDataToGrid(CType(cmbTemplate.SelectedItem, clsComboBoxItem).Value, cmbVendor.SelectedItem.ToString, "TamplateData")

                dtParamfilter.Rows.Clear()
                For Each col As DevExpress.XtraGrid.Columns.GridColumn In gvCMView.Columns
                    AddParamsToDataTable(col.FieldName)
                Next
                BindParamFilterGrid()

            ElseIf cmbTemplate.SelectedIndex = 0 Then
                GetMOList(cmbVendor.SelectedItem.ToString, cmbTechnology.SelectedItem.ToString)
                dtParamfilter.Rows.Clear()

                IOSDevExpressGrid.ClearGrid(gcCMView)
                IOSDevExpressGrid.ClearGrid(gcHistChanges)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        Me.Cursor = Cursors.Default
        Application.DoEvents()
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub frmCMView_Load(sender As Object, e As EventArgs) Handles Me.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.DoubleBuffered = True
            Me.SuspendLayout()
            Me.WindowState = FormWindowState.Normal
            Me.StartPosition = FormStartPosition.CenterScreen
            Me.Location = Screen.FromControl(frmMDI).Bounds.Location
            Me.BringToFront()

            dtParamfilter.Columns.Add("chk", GetType(System.Boolean))
            dtParamfilter.Columns.Add("param", GetType(System.String))
            BindParamFilterGrid()

            BindVendor()
            Me.ResumeLayout()
            ConfigurCMViewForm("frmCMView")
            gcCMView.Tag = "MOData"
            FiltersInitialize()
            ceLoadObjectTree_CheckedChanged(Nothing, Nothing)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cmbTechnology_SelectedIndexChanged(sender As Object, e As EventArgs) ' Handles cmbTechnology.SelectedIndexChanged
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            dtParamfilter.Rows.Clear()
            ClearComboBox(cmbTemplate, "Select Template")
            ClearComboBox(cmbTargetObject, "Object Type")

            IOSDevExpressGrid.ClearGrid(gcCMView)
            IOSDevExpressGrid.ClearGrid(gcHistChanges)

            If (cmbVendor.SelectedIndex > 0 AndAlso cmbTechnology.SelectedIndex > 0) Then
                GetMOList(cmbVendor.SelectedItem.ToString, cmbTechnology.SelectedItem.ToString)
                Dim cmdText As String = "SELECT * FROM dbo.IOS_Parameters_Templates WHERE LTRIM(RTRIM(Technology))='" + cmbTechnology.SelectedItem.ToString.Trim + "' and Vendor='" + cmbVendor.SelectedItem.ToString.Trim() + "' Order By TemplateName"
                Dim data As DataSet = IOS.DataLibrary.DataAccessorODBC.GetDataSet(connStrIOSServer, cmdText)
                BindDevExComboBoxWithValueMember(cmbTemplate, data.Tables(0), "TemplateID", "TemplateName", "Select Template", True)
            ElseIf cmbTechnology.SelectedIndex = 0 AndAlso cmbVendor.SelectedIndex > 0 Then
                GetMOList(cmbVendor.SelectedItem.ToString)
            End If

            EnableSearchCheckBox()
            '   tlvFilters.Columns.Clear()
            tlvFilters.Nodes.Clear()
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub cmbVendor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVendor.SelectedIndexChanged
        Try
            RemoveHandler cmbTargetObject.SelectedIndexChanged, AddressOf cmbTargetObject_SelectedIndexChanged
            RemoveHandler cmbTechnology.SelectedIndexChanged, AddressOf cmbTechnology_SelectedIndexChanged
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            lstViewMO.DataSource = Nothing
            tlParameterList.DataSource = Nothing
            dtParamfilter.Rows.Clear()
            ClearComboBox(cmbTechnology, "Select Technology")
            ClearComboBox(cmbTemplate, "Select Template")
            ClearComboBox(cmbTargetObject, "Object Type")

            IOSDevExpressGrid.ClearGrid(gcCMView)
            IOSDevExpressGrid.ClearGrid(gcHistChanges)
            IOSDevExpressGrid.ClearGrid(gcObject)

            If cmbVendor.SelectedIndex > 0 Then
                GetMOList(cmbVendor.SelectedItem.ToString)
                BindTechnology()
            End If
            EnableSearchCheckBox()
            '    tlvFilters.Columns.Clear()
            tlvFilters.Nodes.Clear()
            AddHandler cmbTargetObject.SelectedIndexChanged, AddressOf cmbTargetObject_SelectedIndexChanged
            AddHandler cmbTechnology.SelectedIndexChanged, AddressOf cmbTechnology_SelectedIndexChanged
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub txtSearchOuter_TextChanged(sender As Object, e As EventArgs)
        ''txtObjectSearch_TextChanged(tvObjectTree, txtSearchOuter.Text)
    End Sub

    Private Sub TreeViewStats_AfterCheck(sender As Object, e As TreeViewEventArgs)
        CheckTreeNodeAndCount(e.Node, 0, Nothing)
    End Sub

    Private Sub tvObjectTree_AfterCheckNode(sender As Object, e As NodeEventArgs)
        RemoveHandler tvObjectTree.AfterCheckNode, AddressOf tvObjectTree_AfterCheckNode
        CheckTreeListNodeAndCount(e.Node, 0, Nothing)
        AddHandler tvObjectTree.AfterCheckNode, AddressOf tvObjectTree_AfterCheckNode
    End Sub

    Private Sub tvObjectTree_NodeChanged(sender As Object, e As NodeChangedEventArgs)
        RemoveHandler tvObjectTree.NodeChanged, AddressOf tvObjectTree_NodeChanged
        If e.ChangeType = DevExpress.XtraTreeList.NodeChangeTypeEnum.CheckedState Then
            If e.Node.CheckState = CheckState.Checked Then
                e.Node.CheckAll()
            Else
                e.Node.UncheckAll()
            End If
            tvObjectTree.CheckParentNode(e.Node)
            'CheckParentNode(tvObjectTree, e.Node)
        End If
        AddHandler tvObjectTree.NodeChanged, AddressOf tvObjectTree_NodeChanged
    End Sub

    'Private Sub CheckParentNode(ByRef tlv As TreeList, ByRef node As TreeListNode)
    '    While node.ParentNode IsNot Nothing
    '        node = node.ParentNode
    '        Dim oneChildIsChecked As Boolean = CheckOneOfChildsIsChecked(tlv, node)
    '        If oneChildIsChecked Then
    '            node.CheckState = CheckState.Checked
    '        Else
    '            node.CheckState = CheckState.Unchecked
    '        End If
    '    End While
    'End Sub

    'Private Function CheckOneOfChildsIsChecked(ByRef tlv As TreeList, node As TreeListNode) As Boolean
    '    Dim result As Boolean = False
    '    For Each item As TreeListNode In node.Nodes
    '        If item.CheckState = CheckState.Checked Then
    '            result = True
    '        End If
    '    Next
    '    Return result
    'End Function

    Private Sub TreeViewStats_DragOver(sender As Object, e As DragEventArgs) Handles tvObjectTree.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub TreeViewStats_MouseDown(sender As Object, e As MouseEventArgs) Handles tvObjectTree.MouseDown
        Dim tree As TreeList = TryCast(sender, TreeList)
        If (tree IsNot Nothing) Then
            Dim item As TreeListHitInfo = tree.CalcHitInfo(e.Location)
            If item.Node IsNot Nothing Then
                If (e.Button = MouseButtons.Left) Then
                    tree.DoDragDrop(item.Node, DragDropEffects.Copy)
                Else
                    tree.FocusedNode = item.Node
                End If
            End If
        End If
    End Sub

    Private Sub treeList_ColumnFilterChanged(sender As Object, e As EventArgs) Handles tvObjectTree.ColumnFilterChanged
        Try
            Dim tl As TreeList = TryCast(sender, TreeList)
            If String.IsNullOrWhiteSpace(tl.FindFilterText) Then
                tl.CollapseAll()
                tl.ExpandToLevel(0)
            Else
                Dim tNode() As TreeListNode = Nothing

                tNode = tl.FindNodes(Function(node) node.GetDisplayText(0).Contains(tl.FindFilterText))
                If tNode IsNot Nothing Then
                    tl.FocusedNode = tNode(0)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub treeList_CustomRowFilter(sender As Object, e As FilterNodeEventArgs) Handles tvObjectTree.CustomRowFilter
        Try
            Dim parentNode As TreeListNode = e.Node.ParentNode
            If parentNode IsNot Nothing Then
                If e.Node.ParentNode.Visible = True And (e.Node.Item(cmbTargetObject.Properties.Items(1).ToString).ToString().ToUpper().Contains(e.Node.TreeList.FindFilterText.ToUpper()) Or e.Node.ParentNode.Item(cmbTargetObject.Properties.Items(1).ToString).ToString().ToUpper().Contains(e.Node.TreeList.FindFilterText.ToUpper())) Then
                    e.Node.Visible = True 'e.Node.Visible OrElse e.Node.ParentNode.Visible
                    e.Handled = True
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub lstViewMO_MouseMove(sender As Object, e As MouseEventArgs) Handles lstViewMO.MouseMove
        Try
            If e.Button = MouseButtons.Left Then
                Dim node As TreeListNode = lstViewMO.FocusedNode
                Dim data As DataRowView = lstViewMO.GetDataRecordByNode(node)
                If data IsNot Nothing Then
                    Dim obj() As Object = {"MO2Grid", data.Item(0)}
                    lstViewMO.DoDragDrop(obj, DragDropEffects.Copy)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gcObject_MouseMove(sender As Object, e As MouseEventArgs) Handles gcObject.MouseMove
        Try
            If e.Button = MouseButtons.Left Then
                Dim objs As String = Nothing
                Dim selRows() As Integer = gvObject.GetSelectedRows()
                If selRows.Length > 0 Then
                    For i As Integer = 0 To selRows.Length - 1
                        objs &= gvObject.GetRowCellValue(selRows(i), "Dimensions") & ","
                    Next
                End If
                objs = objs.TrimEnd(",")
                Dim obj() As Object = {"Object2ParamSelection", objs}
                gcObject.DoDragDrop(obj, DragDropEffects.Copy)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub lstViewMO_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstViewMO.FocusedNodeChanged
        Try
            'Me.Cursor = Cursors.WaitCursor
            'Application.DoEvents()
            IOSDevExpressGrid.ClearGrid(gcParamDesc)

            Dim node As TreeListNode = lstViewMO.FocusedNode
            Dim data As DataRowView = lstViewMO.GetDataRecordByNode(node)

            If chkSearchAllParameter.Checked = False Then
                Dim MOName As String = Nothing
                If cmbVendor.SelectedIndex > 0 Then
                    If data IsNot Nothing Then
                        MOName = "''" & data.Item(0).ToString & "''"
                        Dim tech As String = Nothing
                        If cmbTechnology.SelectedIndex > 0 Then
                            tech = cmbTechnology.SelectedItem.ToString()
                        End If
                        GetListOfParameters(cmbVendor.SelectedItem.ToString, MOName, tech)
                        GetObjectsOfMO(cmbVendor.SelectedItem.ToString, MOName.Replace("''", ""))
                        GetParamDescListForMO(MOName.Replace("''", ""))
                    End If
                Else
                    tlParameterList.DataSource = Nothing
                    IOSDevExpressGrid.ClearGrid(gcParamDesc)
                End If
            End If
            If ceLoadObjectTree.Checked Then
                If data IsNot Nothing Then
                    LoadObjectTypeCombo(data.Item(0).ToString)
                End If
            End If
            dtParamfilter.Rows.Clear()
            'txtSearchOuter.Text = String.Empty
        Catch ex As Exception
            'Finally
            '    Me.Cursor = Cursors.Default
            '    Application.DoEvents()
        End Try
    End Sub

    Private Sub gcCMView_DragDrop(sender As Object, e As DragEventArgs) Handles gcCMView.DragDrop
        Try
            Dim mo() As Object = e.Data.GetData("System.Object[]")
            If mo IsNot Nothing Then
                If mo(0) = "MO2Grid" Then
                    If chkSearchAllParameter.Checked = False Then
                        GetListOfParameters(cmbVendor.SelectedItem.ToString(), "''" & mo(1) & "''")
                    End If
                    LoadDataToGrid(mo(1), cmbVendor.SelectedItem.ToString, gcCMView.Tag)
                End If

                dtParamfilter.Rows.Clear()
                For Each col As DevExpress.XtraGrid.Columns.GridColumn In gvCMView.Columns
                    AddParamsToDataTable(col.FieldName)
                Next
                BindParamFilterGrid()

                gcHistChanges.DataSource = Nothing
                gvHistChanges.Columns.Clear()
            End If
            e.Effect = DragDropEffects.None
        Catch ex As Exception
        End Try
    End Sub

    Private Sub AddParamsToDataTable(ByVal param As String)
        Dim dr As DataRow = dtParamfilter.NewRow
        dr(0) = True
        dr(1) = param
        dtParamfilter.Rows.Add(dr)
    End Sub

    Private Sub AddObject2paramSelection(ByVal objs As String)
        Dim dimArr() As String = objs.Split(",")
        For i As Integer = 0 To dimArr.Length - 1
            If (dtParamfilter.Rows.Count > 0) Then
                If (dtParamfilter.Select("param='" & dimArr(i) & "'").Length = 0) Then
                    AddParamsToDataTable(dimArr(i))
                    BindParamFilterGrid()
                End If
            Else
                AddParamsToDataTable(dimArr(i))
                BindParamFilterGrid()
            End If
        Next
    End Sub

    Private Sub BindParamFilterGrid()
        IOSDevExpressGrid.PopulateDataInGrid(gcParamFilter, gvParamFilter, dtParamfilter, "ALL",, "param")
        gvParamFilter.Columns(1).OptionsColumn.AllowEdit = False
    End Sub

    Private Sub cmbTargetObject_SelectedIndexChanged(sender As Object, e As EventArgs) 'Handles cmbTargetObject.SelectedIndexChanged
        RemoveHandler tvObjectTree.NodeChanged, AddressOf tvObjectTree_NodeChanged
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            tvObjectTree.SuspendLayout()
            tvObjectTree.Nodes.Clear()
            tvObjectTree.Refresh()
            tvObjectTree.ResumeLayout()
            If (cmbTargetObject.SelectedIndex > 0) Then
                Dim strCols As String = Nothing
                If Not cmbTargetObject.SelectedItem Is Nothing Then
                    Dim view As New DataView(dtMOPKColList) With {
                        .Sort = "ORDINAL_POSITION DESC"
                    }
                    Dim dtTemp As DataTable = view.ToTable()
                    Dim selectedColOrdinal As Integer = cmbTargetObject.SelectedItem.Tag
                    For Each dtRow As DataRow In dtTemp.Rows
                        If CInt(dtRow.Item(1)) <= selectedColOrdinal Then
                            strCols = strCols & ", " & dtRow.Item(0)
                        End If
                    Next
                End If
                strCols = strCols.TrimStart(",")

                tvObjectTree.KeyFieldName = cmbTargetObject.SelectedText
                LoadObjectTreeFromMo(strCols)
            End If
            tvObjectTree.Name = "tvObjectTree"
            AddHandler tvObjectTree.NodeChanged, AddressOf tvObjectTree_NodeChanged
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            tvObjectTree.CollapseAll()
            tvObjectTree.ExpandToLevel(0)
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub txtSearchPH_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearchPH.KeyUp
        If dtParameterList IsNot Nothing Then
            If txtSearchPH.Text.Length > 2 Then
                dtParameterList.DefaultView.RowFilter = "COLUMN_NAME LIKE '" & txtSearchPH.Text & "%'"
            Else
                dtParameterList.DefaultView.RowFilter = ""
            End If
        End If
    End Sub

    Private Sub txtSearchMO_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearchMO.KeyUp
        Try
            If dtMOList IsNot Nothing Then
                If (txtSearchMO.Text.Length > 2) Then
                    dtMOList.DefaultView.RowFilter = "TableName Like '%" + txtSearchMO.Text + "%'"
                Else
                    dtMOList.DefaultView.RowFilter = ""
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tlParameterList_MouseMove(sender As Object, e As MouseEventArgs) Handles tlParameterList.MouseMove
        Try
            If (e.Button = MouseButtons.Left) Then
                Dim nodes As TreeListMultiSelection = tlParameterList.Selection
                Dim params As String = Nothing
                For Each nde As TreeListNode In nodes
                    params &= nde.GetValue("COLUMN_NAME") & ","
                Next

                params = params.TrimEnd(",")
                If params.Length <> 0 Then
                    tlParameterList.DoDragDrop(params, DragDropEffects.Copy)
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Function GetParamColumnList() As String
        Dim paramFilter As String = ""
        If gvParamFilter.RowCount > 0 Then
            For iRow As Integer = 0 To gvParamFilter.RowCount - 1
                paramFilter = paramFilter & ", " & gvParamFilter.GetRowCellValue(iRow, "param").ToString
            Next
            paramFilter = paramFilter.TrimStart(", ")
        Else
            paramFilter = Nothing
        End If
        Return paramFilter
    End Function

    Private Sub tlParameterList_DragDrop(sender As Object, e As DragEventArgs) Handles tlParameterList.DragDrop
        Try
            Dim dr As String = e.Data.GetData("System.String")
            If dr IsNot Nothing Then
                Dim drow() As DataRow = Nothing
                drow = dtParamfilter.Select("param='" & dr & "'")
                If drow.Length > 0 Then
                    dtParamfilter.Rows.Remove(drow(0))
                    gvParamFilter.RefreshData()

                    Dim n As Integer = Val(txtQueryBatchSize.Tag)
                    Dim m As Integer = Val(txtQueryBatchSize.Text)

                    If gcCMView.Tag = "MOData" Then

                        Dim node As TreeListNode = lstViewMO.FocusedNode
                        Dim data As DataRowView = lstViewMO.GetDataRecordByNode(node)
                        If data IsNot Nothing Then
                            LoadDataToGrid(data.Item(0).ToString, cmbVendor.SelectedItem.ToString, "MOData", GetParamColumnList())
                        End If

                    ElseIf gcCMView.Tag = "TemplateData" Then
                        If cmbTemplate.SelectedIndex > 0 Then
                            LoadDataToGrid(CType(cmbTemplate.SelectedItem, clsComboBoxItem).Value, cmbVendor.SelectedItem.ToString, "TemplateData")

                            For Each column As GridColumn In gvCMView.Columns
                                Dim drArray() As DataRow = Nothing
                                Try
                                    drArray = dtParamfilter.Select("param='" & column.FieldName & "'")
                                Catch ex As Exception
                                    drArray = Nothing
                                End Try
                                If drArray IsNot Nothing Then
                                    column.Visible = True
                                Else
                                    column.Visible = False
                                End If
                            Next
                        End If
                    End If

                    If gcHistChanges.Tag = "MOChanges" Then
                        Dim node As TreeListNode = lstViewMO.FocusedNode
                        Dim data As DataRowView = lstViewMO.GetDataRecordByNode(node)
                        If data IsNot Nothing Then
                            LoadDataToGrid(data.Item(0).ToString, cmbVendor.SelectedItem.ToString, "MOChanges")
                        End If
                    End If
                End If
            End If
            e.Effect = DragDropEffects.None
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tlParameterList_DragOver(sender As Object, e As DragEventArgs) Handles tlParameterList.DragOver, gcParamFilter.DragOver, lstViewMO.DragOver, gcCMView.DragOver, tlvFilters.DragOver, gcObject.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub lstParamFilterDragged_DragDrop(sender As Object, e As DragEventArgs) Handles gcParamFilter.DragDrop
        Try
            Dim item As String = e.Data.GetData("System.String")
            If item IsNot Nothing Then
                Dim paramArr As String() = item.Split(",")
                For Each param As String In paramArr
                    If dtParamfilter.Select("param='" & param & "'").Length = 0 Then
                        AddParamsToDataTable(param)
                        BindParamFilterGrid()

                        Dim n As Integer = Val(txtQueryBatchSize.Tag)
                        Dim m As Integer = Val(txtQueryBatchSize.Text)

                        If gcCMView.Tag = "MOData" Then
                            Dim node As TreeListNode = lstViewMO.FocusedNode
                            Dim data As DataRowView = lstViewMO.GetDataRecordByNode(node)
                            If data IsNot Nothing Then
                                LoadDataToGrid(data.Item(0).ToString, cmbVendor.SelectedItem.ToString, "MOData", GetParamColumnList())
                            End If

                        ElseIf gcCMView.Tag = "TemplateData" Then
                            If cmbTemplate.SelectedIndex > 0 Then
                                LoadDataToGrid(CType(cmbTemplate.SelectedItem, clsComboBoxItem).Value, cmbVendor.SelectedItem.ToString, "TemplateData")

                                For Each column As GridColumn In gvCMView.Columns
                                    Dim drArray() As DataRow = Nothing
                                    Try
                                        drArray = dtParamfilter.Select("param='" & column.FieldName & "'")
                                    Catch ex As Exception
                                        drArray = Nothing
                                    End Try
                                    If drArray IsNot Nothing Then
                                        column.Visible = True
                                    Else
                                        column.Visible = False
                                    End If
                                Next
                            End If
                        End If

                        If gcHistChanges.Tag = "MOChanges" Then
                            Dim node As TreeListNode = lstViewMO.FocusedNode
                            Dim data As DataRowView = lstViewMO.GetDataRecordByNode(node)
                            If data IsNot Nothing Then
                                GetChangesForMO(data.Item(0).ToString, cmbVendor.SelectedItem.ToString, n, m)
                            End If
                        End If
                    End If
                Next
            End If

            ' Drag object from Object grid to parameter selection grid
            Dim obj() As Object = e.Data.GetData("System.Object[]")
            If obj IsNot Nothing Then
                If obj(0).ToString = "Object2ParamSelection" Then
                    AddObject2paramSelection(obj(1).ToString)
                    BindParamFilterGrid()

                    Dim n As Integer = Val(txtQueryBatchSize.Tag)
                    Dim m As Integer = Val(txtQueryBatchSize.Text)

                    If gcCMView.Tag = "MOData" Then
                        Dim node As TreeListNode = lstViewMO.FocusedNode
                        Dim data As DataRowView = lstViewMO.GetDataRecordByNode(node)
                        If data IsNot Nothing Then
                            LoadDataToGrid(data.Item(0).ToString, cmbVendor.SelectedItem.ToString, "MOData", GetParamColumnList())
                        End If

                    ElseIf gcCMView.Tag = "TemplateData" Then
                        If cmbTemplate.SelectedIndex > 0 Then
                            LoadDataToGrid(CType(cmbTemplate.SelectedItem, clsComboBoxItem).Value, cmbVendor.SelectedItem.ToString, "TemplateData")

                            For Each column As GridColumn In gvCMView.Columns
                                Dim drArray() As DataRow = Nothing
                                Try
                                    drArray = dtParamfilter.Select("param='" & column.FieldName & "'")
                                Catch ex As Exception
                                    drArray = Nothing
                                End Try
                                If drArray IsNot Nothing Then
                                    column.Visible = True
                                Else
                                    column.Visible = False
                                End If
                            Next
                        End If
                    End If
                End If
            End If

            e.Effect = DragDropEffects.None
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub lstParamFilterDragged_MouseDown(sender As Object, e As MouseEventArgs) Handles gcParamFilter.MouseDown
        Try
            p = Nothing
            Dim grdCtrl As DevExpress.XtraGrid.GridControl = TryCast(sender, DevExpress.XtraGrid.GridControl)
            If (grdCtrl IsNot Nothing) Then
                p = New Point(e.X, e.Y)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvParamFilter_RowCellClick(sender As Object, e As Views.Grid.RowCellClickEventArgs) Handles gvParamFilter.RowCellClick
        Try
            If e.RowHandle > -1 AndAlso e.Column.AbsoluteIndex = 0 Then
                gvParamFilter.SetRowCellValue(e.RowHandle, e.Column, Not e.CellValue)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lstParamFilterDragged_MouseMove(sender As Object, e As MouseEventArgs) Handles gcParamFilter.MouseMove
        Try
            If e.Button = MouseButtons.Left Then
                If (p <> System.Drawing.Point.Empty) Then
                    Dim grdCtrl As DevExpress.XtraGrid.GridControl = TryCast(sender, DevExpress.XtraGrid.GridControl)
                    Dim gv As DevExpress.XtraGrid.Views.Grid.GridView = grdCtrl.MainView
                    Dim index As Integer = gv.FocusedRowHandle
                    If (index > -1) Then
                        Dim drop_effect As DragDropEffects = Nothing
                        drop_effect = grdCtrl.DoDragDrop(gv.GetRowCellValue(index, "param").ToString, DragDropEffects.All)
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub lstParamFilterDragged_KeyUp(sender As Object, e As KeyEventArgs) Handles gcParamFilter.KeyUp
        Try
            If e.KeyCode = Keys.Delete Then
                dtParamfilter.Rows.Remove(gvParamFilter.GetRow(gvParamFilter.FocusedRowHandle).Row)
                gvParamFilter.RefreshData()

                Dim n As Integer = Val(txtQueryBatchSize.Tag)
                Dim m As Integer = Val(txtQueryBatchSize.Text)

                If gcCMView.Tag = "MOData" Then
                    Dim node As TreeListNode = lstViewMO.FocusedNode
                    Dim data As DataRowView = lstViewMO.GetDataRecordByNode(node)
                    If data IsNot Nothing Then
                        LoadDataToGrid(data.Item(0).ToString, cmbVendor.SelectedItem.ToString, "MOData", GetParamColumnList())
                    End If
                ElseIf gcCMView.Tag = "TemplateData" Then
                    If cmbTemplate.SelectedIndex > 0 Then
                        LoadDataToGrid(CType(cmbTemplate.SelectedItem, clsComboBoxItem).Value, cmbVendor.SelectedItem.ToString, "TemplateData")

                        For Each column As GridColumn In gvCMView.Columns
                            Dim drArray() As DataRow = Nothing
                            Try
                                drArray = dtParamfilter.Select("param='" & column.FieldName & "'")
                            Catch ex As Exception
                                drArray = Nothing
                            End Try
                            If drArray.Length > 0 Then
                                column.Visible = True
                            Else
                                column.Visible = False
                            End If
                        Next
                    End If
                End If

                If gcHistChanges.Tag = "MOChanges" Then
                    Dim node As TreeListNode = lstViewMO.FocusedNode
                    Dim data As DataRowView = lstViewMO.GetDataRecordByNode(node)
                    If data IsNot Nothing Then
                        LoadDataToGrid(data.Item(0).ToString, cmbVendor.SelectedItem.ToString, "MOChanges")
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnDump2Csv_Click(sender As Object, e As EventArgs) Handles btnDump2Csv.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim dtExport As New DataTable
            Dim node As TreeListNode = lstViewMO.FocusedNode
            Dim data As DataRowView = lstViewMO.GetDataRecordByNode(node)

            Dim objFileDlg As New SaveFileDialog()
            objFileDlg.Filter = "Comma Delimited|*.csv"
            objFileDlg.Title = "Save a CSV File"

            If objFileDlg.ShowDialog() = DialogResult.OK Then
                If objFileDlg.FileName <> "" Then

                    WaitScreen.ShowWaitScreen("Exporting to CSV...")
                    Application.DoEvents()

                    If gcCMView.Tag = "MOData" Or gcCMView.Tag Is Nothing Or gcCMView.Tag = "" Then
                        If data IsNot Nothing Then
                            dtExport = CreateData(data.Item(0).ToString, cmbVendor.SelectedItem.ToString, 0, 0, currViewRowFilter, "MOData")
                        End If
                    ElseIf gcCMView.Tag = "TemplateData" Then
                        If cmbTemplate.SelectedIndex > 0 Then
                            dtExport = CreateData(CType(cmbTemplate.SelectedItem, clsComboBoxItem).Value, cmbVendor.SelectedItem.ToString, 0, 0, currViewRowFilter, "TemplateData")
                        End If
                    End If
                    IOSDevExpressGrid.DataTable2CSV(dtExport, objFileDlg.FileName)
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

    Private Sub btnDump2Xls_Click(sender As Object, e As EventArgs) Handles btnDump2Xls.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim dtExport As New DataTable
            Dim node As TreeListNode = lstViewMO.FocusedNode
            Dim data As DataRowView = lstViewMO.GetDataRecordByNode(node)

            Dim objFileDlg As New SaveFileDialog()
            objFileDlg.Filter = "Excel Workbook |*.xlsx"
            objFileDlg.Title = "Save an excel File"

            If objFileDlg.ShowDialog() = DialogResult.OK Then
                If objFileDlg.FileName <> "" Then

                    WaitScreen.ShowWaitScreen("Exporting to excel...")
                    Application.DoEvents()

                    If gcCMView.Tag = "MOData" Or gcCMView.Tag Is Nothing Or gcCMView.Tag = "" Then
                        If data IsNot Nothing Then
                            dtExport = CreateData(data.Item(0).ToString, cmbVendor.SelectedItem.ToString, 0, 0, currViewRowFilter, "MOData")
                        End If
                    ElseIf gcCMView.Tag = "TemplateData" Then
                        If cmbTemplate.SelectedIndex > 0 Then
                            dtExport = CreateData(CType(cmbTemplate.SelectedItem, clsComboBoxItem).Value, cmbVendor.SelectedItem.ToString, 0, 0, currViewRowFilter, "TemplateData")
                        End If
                    End If
                    ExportDataTableToExcel_Stream(dtExport, objFileDlg.FileName)
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
        If chkSearchAllParameter.Checked = True Then
            GetListOfParameters(cmbVendor.SelectedItem.ToString, "", cmbTechnology.SelectedItem.ToString, 1)
        End If
    End Sub

    Private Sub tlParameterList_FocusedNodeChanged(sender As Object, e As DevExpress.XtraTreeList.FocusedNodeChangedEventArgs) Handles tlParameterList.FocusedNodeChanged
        Try
            Dim node As TreeListNode = tlParameterList.FocusedNode
            Dim data As DataRowView = tlParameterList.GetDataRecordByNode(node)
            If data IsNot Nothing Then
                lstViewMO.SetFocusedNode(lstViewMO.FindNodeByFieldValue("TableName", data.Item(0).ToString))
                GetObjectsOfMO(cmbVendor.SelectedItem.ToString, data.Item(0).ToString)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        gcCMView.DataSource = Nothing
        gvCMView.Columns.Clear()

        gcHistChanges.DataSource = Nothing
        gvHistChanges.Columns.Clear()

        sccGrids.Collapsed = True
        sccGrids.PanelVisibility = SplitPanelVisibility.Panel1
        dtParamfilter.Rows.Clear()
    End Sub

    Private Sub ceLoadObjectTree_CheckedChanged(sender As Object, e As EventArgs) Handles ceLoadObjectTree.CheckedChanged
        Try
            If ceLoadObjectTree.Checked Then
                cmbTargetObject.Enabled = True
                'txtSearchOuter.Enabled = True
                tvObjectTree.Enabled = True

                Dim node As TreeListNode = lstViewMO.FocusedNode
                Dim data As DataRowView = lstViewMO.GetDataRecordByNode(node)
                If data IsNot Nothing Then
                    LoadObjectTypeCombo(data.Item(0).ToString)
                End If
            Else
                cmbTargetObject.Enabled = False
                'txtSearchOuter.Enabled = False
                tvObjectTree.Enabled = False
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Context Menu Code"

    Dim cm_OT_SourceControl As System.Windows.Forms.Control

    Private Sub cmObjectTree_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmObjectTree.Opening
        Dim targetObject As String = Nothing
        'Dim tech As String = tvObjectTree.Tag
        Dim countchecked As Integer = 0
        targetObject = cmbTargetObject.SelectedItem.ToString

        'count checked boxes
        countchecked = TreeList_CountCheckedNodes(tvObjectTree.Nodes(0))

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
                        If Not Treelist_TextSearch(bufferCell(j).Trim, tvObjectTree.Nodes, True, tvObjectTree.VisibleColumns(0).Caption) Is Nothing Then
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
    End Sub

    Private Sub cm_OT_tsmi_copy_Click(sender As Object, e As EventArgs) Handles cm_OT_tsmi_copy.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Clipboard.Clear()
        Dim tgtObject As String = Nothing
        Try
            tgtObject = cmbTargetObject.SelectedItem.ToString
            Dim copystring As String = TreeList_Checked2String(cmbVendor.SelectedItem.ToString & " " & cmbTechnology.SelectedItem.ToString(), tgtObject, "Naked", tvObjectTree, cmbTargetObject, tvObjectTree.VisibleColumns(0).Caption)
            ' Dim copystring As String = tvObjectTree.GetChecked2String(cmbVendor.SelectedItem.ToString & " " & cmbTechnology.SelectedItem.ToString(), cmbTargetObject.SelectedItem.ToString, "Naked")
            copystring = copystring.Replace(",", ControlChars.NewLine)
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
                        Dim tv_result As TreeListNode = Treelist_TextSearch(bufferCell(j).Trim, tvObjectTree.FocusedNode.Nodes, ExactMatch, tvObjectTree.VisibleColumns(0).Caption)
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
        'Dim tv As TreeView = cm_OT_SourceControl
        Try
            ObjectTreeList_CheckChild(tvObjectTree.FocusedNode)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_OT_UnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmi_OT_UnCheck.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            'Dim tv As TreeView = cm_OT_SourceControl
            TreeList_ClearChecks(tvObjectTree.Nodes(0))
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_CompareVsSelection_Click(sender As Object, e As EventArgs) Handles tsmi_CompareVsSelection.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If gvCMView.RowCount > 0 Then
                Dim rowIndex() As Integer
                rowIndex = gvCMView.GetSelectedRows()
                If rowIndex.Length > 0 Then
                    SelectedRowIndex = rowIndex(0)
                    Dim obj As DataRowView = gvCMView.GetRow(SelectedRowIndex)
                    gvCMView.FormatConditions.Clear()
                    RowListToHide.Clear()

                    'Getting un-highlighted row index from grid
                    Dim objIndex As Integer = -1
                    Dim isRowHighlighted As Boolean = False
                    For rIndex As Integer = 0 To gvCMView.RowCount - 1
                        If rIndex <> SelectedRowIndex Then
                            isRowHighlighted = False

                            For iRow As Integer = 0 To gvParamFilter.RowCount - 1
                                If Convert.ToBoolean(gvParamFilter.GetRowCellValue(iRow, "chk")) = True Then
                                    Dim col As DevExpress.XtraGrid.Columns.GridColumn = Nothing
                                    col = gvCMView.Columns.ColumnByFieldName(gvParamFilter.GetRowCellValue(iRow, "param").ToString)
                                    If col IsNot Nothing Then
                                        Dim value As Object = Nothing
                                        value = gvCMView.GetRowCellValue(rIndex, col)
                                        Dim SelectedRowValue As Object = Nothing
                                        SelectedRowValue = obj.Item(col.FieldName)
                                        If Convert.ToString(value) <> Convert.ToString(SelectedRowValue) Then
                                            isRowHighlighted = True
                                        End If
                                    End If
                                End If
                            Next

                            If isRowHighlighted = False Then
                                RowListToHide.Add(rIndex)
                            End If
                        End If
                    Next

                    'Filter out highlighted rows from complete data
                    Dim dtFilteredData As New DataTable
                    Dim dtGridData As New DataTable
                    dtFilteredData = dtCMView.Clone()   'CType(gcCMView.DataSource, DataTable).Clone()
                    dtGridData = dtCMView 'CType(gcCMView.DataSource, DataTable)

                    Dim i As Integer = 0
                    For i = 0 To dtGridData.Rows.Count - 1
                        If SelectedRowIndex = i Then
                            dtFilteredData.ImportRow(dtGridData.Rows(i))
                            SelectedRowIndex = dtFilteredData.Rows.Count - 1
                        Else
                            If Not RowListToHide.Exists(Function(x) x = i) Then
                                dtFilteredData.ImportRow(dtGridData.Rows(i))
                            End If
                        End If
                    Next

                    'Again fill grid with highlighted data
                    Library.IOSDevExpressGrid.PopulateDataInGrid(gcCMView, gcCMView.MainView, dtFilteredData, "ALL")

                    For Each column As GridColumn In gvCMView.Columns
                        Dim drArray() As DataRow = Nothing
                        Try
                            drArray = dtParamfilter.Select("param='" & column.FieldName & "'")
                        Catch ex As Exception
                            drArray = Nothing
                        End Try
                        If drArray.Length > 0 Then
                            column.Visible = True
                        Else
                            column.Visible = False
                        End If
                    Next

                    gvCMView.ClearSelection()
                    gvCMView.SelectRow(SelectedRowIndex)
                    gvCMView.FocusedRowHandle = SelectedRowIndex

                    'set all cell to be display with highlighted background color
                    Dim cn As StyleFormatCondition
                    For iRow As Integer = 0 To gvParamFilter.RowCount - 1
                        If Convert.ToBoolean(gvParamFilter.GetRowCellValue(iRow, "chk")) = True Then
                            Dim col As DevExpress.XtraGrid.Columns.GridColumn = Nothing
                            col = gvCMView.Columns.ColumnByFieldName(gvParamFilter.GetRowCellValue(iRow, "param").ToString)
                            If col IsNot Nothing Then
                                Dim SelectedRowValue As Object = Nothing
                                SelectedRowValue = obj.Item(col.FieldName)
                                cn = New StyleFormatCondition(FormatConditionEnum.NotEqual, col, Nothing, SelectedRowValue)
                                cn.Appearance.BackColor = Color.LightPink
                                gvCMView.FormatConditions.Add(cn)
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cmsGrid_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsGrid.Opening
        Try
            tsmi_RecordCount.Text = "Record Count: " & gvCMView.RowCount.ToString
            If gvCMView.RowCount = 0 Then
                tsmi_CompareVsSelection.Enabled = False
                tsmi_FreezeColumn.Enabled = False
                tsmi_GetChangesForMO.Enabled = False
                tsmi_GetChangesForSelection.Enabled = False
            Else
                tsmi_CompareVsSelection.Enabled = True
                tsmi_FreezeColumn.Enabled = True
                tsmi_GetChangesForMO.Enabled = True
                tsmi_GetChangesForSelection.Enabled = True
            End If

            For Each gc As GridColumn In gvCMView.Columns
                If gvCMView.FocusedColumn.FieldName = gc.FieldName Then
                    If gc.Fixed = FixedStyle.Left Or gc.Fixed = FixedStyle.Right Then
                        tsmi_FreezeColumn.Text = "Unfreeze Column"
                    Else
                        tsmi_FreezeColumn.Text = "Freeze Column"
                    End If
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmsHistoryChanges_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsHistoryChanges.Opening
        Try
            tsmi_histChangesRecordCount.Text = "Record Count: " & gvHistChanges.RowCount.ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tsmi_FreezeColumn_Click(sender As Object, e As EventArgs) Handles tsmi_FreezeColumn.Click
        Try
            If tsmi_FreezeColumn.Text.Contains("Freeze") Then
                For Each gc As GridColumn In gvCMView.Columns
                    If gvCMView.FocusedColumn.FieldName = gc.FieldName Then
                        gc.Fixed = FixedStyle.Left
                    End If
                Next
            Else
                For Each gc As GridColumn In gvCMView.Columns
                    If gvCMView.FocusedColumn.FieldName = gc.FieldName Then
                        gc.Fixed = FixedStyle.None
                    End If
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private oldTopRowIndex As Integer = 0
    Private Sub tsmi_FreezeRow_Click(sender As Object, e As EventArgs) Handles tsmi_FreezeRow.Click
        Try
            If tsmi_FreezeRow.Text.Contains("Freeze") Then
                oldTopRowIndex = gvCMView.TopRowIndex
                AddHandler gvCMView.TopRowChanged, AddressOf gvCMView_TopRowChanged
                AddHandler gvCMView.CustomDrawCell, AddressOf gvCMView_CustomDrawCell
            Else
                RemoveHandler gvCMView.TopRowChanged, AddressOf gvCMView_TopRowChanged
                RemoveHandler gvCMView.CustomDrawCell, AddressOf gvCMView_CustomDrawCell
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvCMView_TopRowChanged(sender As Object, e As EventArgs)
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = TryCast(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        Dim max As Integer = If(oldTopRowIndex > view.TopRowIndex, oldTopRowIndex, view.TopRowIndex)
        For i As Integer = view.TopRowIndex To max
            view.RefreshRow(i)
        Next
    End Sub

    Private Sub gvCMView_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs)
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = TryCast(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        If e.RowHandle < 0 Then
            Return
        End If
        Dim foreColorForFixedCells As Color = Color.Red
        Dim visibleIndex As Integer = view.GetVisibleIndex(e.RowHandle) - view.TopRowIndex
        If visibleIndex >= 0 AndAlso visibleIndex < 1 Then
            Dim displayText As String = view.GetRowCellDisplayText(visibleIndex, e.Column)
            e.Appearance.DrawString(e.Cache, displayText, e.Bounds)
            e.Handled = True
        End If
    End Sub

    Private Sub tsmi_GetChangesForMO_Click(sender As Object, e As EventArgs) Handles tsmi_GetChangesForMO.Click
        Try
            Dim node As TreeListNode = lstViewMO.FocusedNode
            Dim data As DataRowView = lstViewMO.GetDataRecordByNode(node)
            Dim moName As String = Nothing
            If data IsNot Nothing Then
                moName = data.Item(0).ToString
            End If

            gcHistChanges.Tag = "MOChanges"
            LoadDataToGrid(moName, cmbVendor.SelectedItem.ToString, "MOChanges")
            sccGrids.Collapsed = False
            sccGrids.PanelVisibility = SplitPanelVisibility.Both
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tsmi_GetChangesForSelection_Click(sender As Object, e As EventArgs) Handles tsmi_GetChangesForSelection.Click
        Try
            Dim filterQry As String = Nothing
            Dim rowIndex() As Integer
            rowIndex = gvCMView.GetSelectedRows()
            If rowIndex.Length > 0 Then
                SelectedRowIndex = rowIndex(0)
                Dim obj As DataRowView = gvCMView.GetRow(SelectedRowIndex)

                gvHistChanges.FormatConditions.Clear()
                Dim objList As New List(Of KeyValuePair(Of String, List(Of String)))

                For Each itm As clsComboBoxItem In cmbTargetObject.Properties.Items
                    If itm.Value IsNot Nothing Then
                        For Each rIndex As Integer In gvCMView.GetSelectedRows
                            Dim col As DevExpress.XtraGrid.Columns.GridColumn = Nothing
                            col = gvCMView.Columns.ColumnByFieldName(itm.Value)
                            Dim row As DataRowView = gvCMView.GetRow(rIndex)
                            GetMOPKColumnValues2List(col.FieldName, row, objList)
                        Next
                    End If
                Next

                For Each listItm As KeyValuePair(Of String, List(Of String)) In objList
                    If filterQry IsNot Nothing Then
                        filterQry = filterQry & " AND "
                    Else
                        filterQry = " AND "
                    End If
                    filterQry = filterQry & listItm.Key & " In ("
                    For Each listItm1 As String In listItm.Value
                        filterQry = filterQry & "''" & listItm1 & "'',"
                    Next
                    filterQry = filterQry.TrimEnd(",") & ")"
                Next

                Dim node As TreeListNode = lstViewMO.FocusedNode
                Dim data As DataRowView = lstViewMO.GetDataRecordByNode(node)
                If data IsNot Nothing Then
                    GetChangesForMOSelection(data.Item(0).ToString, cmbVendor.SelectedItem.ToString, filterQry)
                End If

                sccGrids.Collapsed = False
                sccGrids.PanelVisibility = SplitPanelVisibility.Both
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GetMOPKColumnValues2List(ByVal columnName As String, ByVal dataRow As DataRowView, ByRef list As List(Of KeyValuePair(Of String, List(Of String))))
        Dim obj As KeyValuePair(Of String, List(Of String))
        Dim colIndex As Integer = gvCMView.Columns(columnName).AbsoluteIndex
        obj = Nothing
        If list.Exists(Function(x) x.Key = columnName) Then
            obj = list.FirstOrDefault(Function(x) x.Key = columnName)
            If Not obj.Value.Contains(dataRow.Row.Item(colIndex)) Then
                obj.Value.Add(dataRow.Row.Item(colIndex))
            End If
        Else
            Dim value As New List(Of String)
            value.Add(dataRow.Row.Item(colIndex))
            obj = New KeyValuePair(Of String, List(Of String))(columnName, value)
            list.Add(obj)
        End If
    End Sub

    Private Sub tsmi_AllowCellCopy_CheckedChanged(sender As Object, e As EventArgs) Handles tsmi_AllowCellCopy.CheckedChanged, tsmi_HistChangesAllowCellCopy.CheckedChanged
        Try
            Dim tempGrid As DevExpress.XtraGrid.GridControl = frmMapWindow.GetAttachedGrid(sender)
            Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = tempGrid.MainView
            If tsmi_AllowCellCopy.Checked Or tsmi_HistChangesAllowCellCopy.Checked Then
                gridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
            Else
                gridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmi_CopySelection_Click(sender As Object, e As EventArgs) Handles tsmi_CopySelection.Click
        Try
            Dim tempGrid As DevExpress.XtraGrid.GridControl = frmMapWindow.GetAttachedGrid(sender)
            Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = tempGrid.MainView
            IOSDevExpressGrid.CopyGridDataToClipBoard(tempGrid, gridView, False, Not tsmi_AllowCellCopy.Checked)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmi_CopySelectionWithHeader_Click(sender As Object, e As EventArgs) Handles tsmi_CopySelectionWithHeader.Click
        Try
            Dim tempGrid As DevExpress.XtraGrid.GridControl = frmMapWindow.GetAttachedGrid(sender)
            Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = tempGrid.MainView
            IOSDevExpressGrid.CopyGridDataToClipBoard(tempGrid, gridView, False, True)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmi_HistChangesCopySelection_Click(sender As Object, e As EventArgs) Handles tsmi_HistChangesCopySelection.Click
        Try
            Dim tempGrid As DevExpress.XtraGrid.GridControl = frmMapWindow.GetAttachedGrid(sender)
            Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = tempGrid.MainView
            IOSDevExpressGrid.CopyGridDataToClipBoard(tempGrid, gridView, False, Not tsmi_HistChangesAllowCellCopy.Checked)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmi_HistChangesCopySelectionWithHeader_Click(sender As Object, e As EventArgs) Handles tsmi_HistChangesCopySelectionWithHeader.Click
        Try
            Dim tempGrid As DevExpress.XtraGrid.GridControl = frmMapWindow.GetAttachedGrid(sender)
            Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = tempGrid.MainView
            IOSDevExpressGrid.CopyGridDataToClipBoard(tempGrid, gridView, False, True)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmi_ParamDescParam_Click(sender As Object, e As EventArgs) Handles tsmi_ParamDescParam.Click
        Try
            Dim node As TreeListNode = lstViewMO.FocusedNode
            Dim drMO As DataRowView = lstViewMO.GetDataRecordByNode(node)

            Dim ndParam As TreeListNode = tlParameterList.FocusedNode
            Dim drParam As DataRowView = tlParameterList.GetDataRecordByNode(ndParam)

            Dim objParamDesc As New frmParameterDescription()
            objParamDesc.moName = Nothing
            objParamDesc.moTblName = drMO.Item(0).ToString
            objParamDesc.paramName = drParam.Item(2).ToString
            objParamDesc.fromLeft = Me.Left + Me.Width
            objParamDesc.fromTop = Me.Top
            objParamDesc.ShowDialog()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

#End Region

#Region "Filter"

    Public Sub FiltersInitialize()
        Dim col1 As TreeListViewColumn = New TreeListViewColumn("Parameter", "")
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
            Dim flag As Boolean = False
            If TypeOf e.[Object] Is TreeListViewSubItem Then
                Dim node As TreeListViewSubItem = DirectCast(e.[Object], TreeListViewSubItem)
                If e.Label IsNot Nothing Then
                    If node.Index = 0 Then
                        e.Cancel = True
                        XtraMessageBox.Show("Not allowed to change parameter name!", "Node Label Edit")
                    ElseIf node.Index = 1 Then
                        If Not e.Label.IndexOfAny(New Char() {">"c, "<"c, "="c}) >= 0 Then
                            ' Cancel the label edit action, inform the user, and
                            ' place the node in edit mode again.

                            e.Cancel = True
                            XtraMessageBox.Show("The valid characters are '<','>','<>','='", "Node Label Edit")
                            node.BeginEdit()

                        Else
                            flag = True
                        End If
                    ElseIf node.Index = 2 Then
                        If Not IsNumeric(e.Label) And (node.Parent.SubItems(1).Text = ">"c Or node.Parent.SubItems(1).Text = "<"c) Then
                            e.Cancel = True
                            node.BeginEdit()
                        Else
                            flag = True
                        End If
                    End If

                    If flag = True Then
                        e.Cancel = False
                        node.Text = e.Label
                        node.UpdateParent()
                        Dim m As Integer = Val(txtQueryBatchSize.Text)
                        If gcCMView.Tag = "MOData" Or gcCMView.Tag Is Nothing Or gcCMView.Tag = "" Then
                            Dim nd As TreeListNode = lstViewMO.FocusedNode
                            Dim data As DataRowView = lstViewMO.GetDataRecordByNode(nd)
                            If data IsNot Nothing Then

                                LoadDataToGrid(data.Item(0).ToString, cmbVendor.SelectedItem.ToString, "MOData", GetParamColumnList())
                            End If
                        ElseIf gcCMView.Tag = "TemplateData" Then
                            If cmbTemplate.SelectedIndex > 0 Then
                                LoadDataToGrid(CType(cmbTemplate.SelectedItem, clsComboBoxItem).Value, cmbVendor.SelectedItem.ToString, "TemplateData")
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function ApplyParamFilters() As String
        Try
            Dim rowFilterString As String = ""
            tlvFilters.Update()
            tlvFilters.Refresh()
            For Each nd As TreeListViewNode In tlvFilters.Nodes
                If IsNumeric(nd.SubItems(2).Text) Then
                    rowFilterString += nd.SubItems(0).Text & " " & nd.SubItems(1).Text & " " & nd.SubItems(2).Text & " And "
                Else
                    rowFilterString += nd.SubItems(0).Text & " " & nd.SubItems(1).Text & " ''" & nd.SubItems(2).Text & "'' And "
                End If
            Next

            If rowFilterString.Length > 0 Then
                Return rowFilterString.Substring(0, rowFilterString.Length - 5)
            Else
                Return rowFilterString
            End If
        Catch
            Return ""
        End Try
    End Function

    Private Sub tlvFilters_DragDrop(sender As Object, e As DragEventArgs) Handles tlvFilters.DragDrop
        Try
            If e.Data.GetDataPresent("System.String") Then
                If (e.Effect = DragDropEffects.Copy) Or (e.Effect = DragDropEffects.Move) Then
                    Dim tlv As TreeListView = DirectCast(sender, TreeListView)

                    Dim item As Object = CType(e.Data.GetData("System.String"), System.Object)
                    tlv_ParamFilters_Add(tlv, item)
                End If
                Dim m As Integer = Val(txtQueryBatchSize.Text)
                If gcCMView.Tag = "MOData" Or gcCMView.Tag Is Nothing Or gcCMView.Tag = "" Then
                    Dim nd As TreeListNode = lstViewMO.FocusedNode
                    Dim data As DataRowView = lstViewMO.GetDataRecordByNode(nd)
                    If data IsNot Nothing Then
                        LoadDataToGrid(data.Item(0).ToString, cmbVendor.SelectedItem.ToString, "MOData", GetParamColumnList())
                    End If
                ElseIf gcCMView.Tag = "TemplateData" Then
                    If cmbTemplate.SelectedIndex > 0 Then
                        LoadDataToGrid(CType(cmbTemplate.SelectedItem, clsComboBoxItem).Value, cmbVendor.SelectedItem.ToString, "TemplateData")
                    End If
                End If
            End If
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
                'Dim m As Integer = Val(txtQueryBatchSize.Text)
                If gcCMView.Tag = "MOData" Or gcCMView.Tag Is Nothing Or gcCMView.Tag = "" Then
                    Dim nd As TreeListNode = lstViewMO.FocusedNode
                    Dim data As DataRowView = lstViewMO.GetDataRecordByNode(nd)
                    If data IsNot Nothing Then
                        LoadDataToGrid(data.Item(0).ToString, cmbVendor.SelectedItem.ToString, "MOData", GetParamColumnList())
                    End If
                ElseIf gcCMView.Tag = "TemplateData" Then
                    If cmbTemplate.SelectedIndex > 0 Then
                        LoadDataToGrid(CType(cmbTemplate.SelectedItem, clsComboBoxItem).Value, cmbVendor.SelectedItem.ToString, "TemplateData")
                    End If
                End If
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

#End Region

#Region "Infinite Scrolling"

    Private objLock As New Object
    Private objLockHC As New Object

    Private QueryOffset As Integer = 0
    Private IsFirstTimeLoading As Boolean = False
    Private currViewRowFilter As String = ""
    Private currViewSortStr As String = ""
    Private _virtualServerModeSrouce As VirtualServerModeSource

    Private QueryOffsetMoChanges As Integer = 0
    Private IsFirstTimeLoadingMoChanges As Boolean = False
    Private currViewRowFilterMoChanges As String = ""
    Private currViewSortStrMoChanges As String = ""
    Private _virtualServerModeSrouceMOChanges As VirtualServerModeSource

    Private Function GetFilterQueryFromObjectTree() As String
        Dim filter As String = Nothing
        Dim objList As New List(Of KeyValuePair(Of String, List(Of String)))

        GetSelectedNodes(tvObjectTree.Nodes, objList)

        For Each itm As KeyValuePair(Of String, List(Of String)) In objList
            If filter IsNot Nothing Then
                filter = filter & " AND "
            Else
                filter = " WHERE "
            End If
            filter = filter & itm.Key & " IN ("
            For Each itm1 As String In itm.Value
                filter = filter & "''" & itm1 & "'',"
            Next
            filter = filter.TrimEnd(",") & ")"
        Next
        Return filter
    End Function

    Private Function CreateData(mo_templateId As Object, vendor As String, offset As Integer, batchSize As Integer, Optional currentRowFilter As String = Nothing, Optional gridTag As String = Nothing) As DataTable
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim columnList As String = Nothing
        Dim filterQry As String = Nothing
        Dim sortExprMOChanges As String = Nothing

        RowListToHide.Clear()
        columnList = GetParamColumnList()

        Select Case gridTag

            Case "MOData"

                gvCMView.FormatConditions.Clear()
                filterQry = GetFilterQueryFromObjectTree()

                Dim paramFilterString As String = ApplyParamFilters()
                If filterQry Is Nothing Then
                    If paramFilterString <> "" Then
                        filterQry = " WHERE " & paramFilterString
                    End If
                Else
                    If paramFilterString <> "" Then
                        filterQry = filterQry & " AND " & paramFilterString
                    End If
                End If

                If currentRowFilter IsNot Nothing AndAlso currentRowFilter <> "" Then
                    filterQry = IIf(filterQry Is Nothing, " WHERE ", filterQry & " AND ") & currentRowFilter.Replace("'", "''")
                End If

                Dim parray()() As String = {
                    New String() {"@vendor", vendor},
                    New String() {"@mo", mo_templateId},
                    New String() {"@param", IIf(columnList Is Nothing Or columnList = "", "NULL", "'" & columnList & "'")},
                    New String() {"@n", offset},
                    New String() {"@m", batchSize},
                    New String() {"@filter", Chr(39) & filterQry & Chr(39)},
                    New String() {"@sortExpr", IIf(String.IsNullOrEmpty(currViewSortStr), "NULL", Chr(39) & currViewSortStr & Chr(39))}
                }

                strConnection = GetSQL(3501, parray)(0)
                sqlParam = GetSQL(3501, parray)(1)

            Case "TemplateData"

                gvCMView.FormatConditions.Clear()
                Dim paramFilterString As String = ApplyParamFilters()
                If filterQry Is Nothing Then
                    If paramFilterString <> "" Then
                        filterQry = " WHERE " & paramFilterString
                    End If
                Else
                    If paramFilterString <> "" Then
                        filterQry = filterQry & " AND " & paramFilterString
                    End If
                End If

                sqlParam = "EXEC [dbo].[CM_GetParamValueBasedOnTemplate]" _
                                     & " @template_id = " & mo_templateId & ", " _
                                     & " @vendor = '" & vendor & "', " _
                                     & " @n = " & offset & ", " _
                                     & " @m = " & batchSize & "," _
                                     & " @filter = '" & filterQry & "'," _
                                     & " @sortExpr = " & IIf(String.IsNullOrEmpty(currViewSortStr), "NULL", Chr(39) & currViewSortStr & Chr(39))

                strConnection = connStrIOSServer

            Case "MOChanges"

                gvHistChanges.FormatConditions.Clear()
                filterQry = GetFilterQueryFromObjectTree()

                Dim paramFilterString As String = ApplyParamFilters()

                If filterQry Is Nothing Then
                    If paramFilterString <> "" Then
                        filterQry = " WHERE " & paramFilterString
                    End If
                Else
                    If paramFilterString <> "" Then
                        filterQry = filterQry & " AND " & paramFilterString
                    End If
                End If

                If currentRowFilter IsNot Nothing AndAlso currentRowFilter <> "" Then
                    filterQry = IIf(filterQry Is Nothing, " AND ", filterQry & " AND ") & currentRowFilter.Replace("'", "''")
                End If

                sortExprMOChanges = IIf(String.IsNullOrEmpty(currViewSortStrMoChanges), "NULL", Chr(39) & currViewSortStrMoChanges & Chr(39))

                Dim dtChangeForMo As New DataTable
                strConnection = connStrIOSServer
                sqlParam = "EXEC [CM_GetChangesForMOtoGrid] '" & cmbVendor.SelectedItem.ToString & "', '" & mo_templateId & "'," & offset & "," & batchSize & ",'" & filterQry & "'," & sortExprMOChanges

        End Select

        Return IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Function

    Private Sub LoadDataToGrid(ByVal mo_templateID As Object, vendor As String, Optional gridTag As String = Nothing, Optional paramFilter As String = Nothing)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.UseWaitCursor = True
            Application.DoEvents()

            lblQueryBatchSize.Visible = True
            txtQueryBatchSize.Visible = True

            If gridTag = "MOData" Or gridTag = "TemplateData" Then
                dtCMView = Nothing
                QueryOffset = 0
                IsFirstTimeLoading = True
                currViewRowFilter = ""
                currViewSortStr = ""

                If paramFilter Is Nothing Then
                    dtParamfilter.Rows.Clear()
                End If

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

                gcCMView.DataSource = Nothing
                gvCMView.OptionsView.ColumnAutoWidth = False
                gvCMView.Columns.Clear()
                gcCMView.DataSource = _virtualServerModeSrouce

                datetimeEdit = New RepositoryItemDateEdit()
                datetimeEdit.VistaEditTime = DevExpress.Utils.DefaultBoolean.True
                datetimeEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime
                datetimeEdit.Mask.UseMaskAsDisplayFormat = True
                If regionalSettings = False Then
                    datetimeEdit.Mask.EditMask = "yyyy-MM-dd HH:mm:ss"
                Else
                    datetimeEdit.Mask.EditMask = CultureInfoDefault.DateTimeFormat.ShortDatePattern & " " & CultureInfoDefault.DateTimeFormat.ShortTimePattern
                End If

                If dtCMView IsNot Nothing Then
                    For Each dtCol As DataColumn In dtCMView.Columns
                        If dtCol.DataType = GetType(DateTime) Then
                            gvCMView.Columns(dtCol.ColumnName).ColumnEdit = datetimeEdit
                            gvCMView.Columns(dtCol.ColumnName).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                            If regionalSettings = False Then
                                gvCMView.Columns(dtCol.ColumnName).DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss"
                            Else
                                gvCMView.Columns(dtCol.ColumnName).DisplayFormat.FormatString = CultureInfoDefault.DateTimeFormat.ShortDatePattern & " " & CultureInfoDefault.DateTimeFormat.ShortTimePattern
                            End If
                        End If
                        gvCMView.Columns(dtCol.ColumnName).BestFit()
                    Next
                End If

                'If dtCMView IsNot Nothing Then
                '    For Each dtCol As DataColumn In dtCMView.Columns
                '        If dtCol.DataType = GetType(DateTime) Then
                '            gvCMView.Columns(dtCol.ColumnName).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                '            gvCMView.Columns(dtCol.ColumnName).DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss"
                '        End If
                '        gvCMView.Columns(dtCol.ColumnName).BestFit()
                '    Next
                'End If

            ElseIf gridTag = "MOChanges" Then
                dtHistChanges = Nothing
                QueryOffsetMoChanges = 0
                IsFirstTimeLoadingMoChanges = True
                currViewRowFilterMoChanges = ""
                currViewSortStrMoChanges = ""

                If (_virtualServerModeSrouceMOChanges IsNot Nothing) Then
                    RemoveHandler _virtualServerModeSrouceMOChanges.AcquireInnerList, AddressOf VirtualServerModeSrouceMOChanges_AcquireInnerList
                    RemoveHandler _virtualServerModeSrouceMOChanges.ConfigurationChanged, AddressOf VirtualServerModeSrouceMOChanges_ConfigurationChanged
                    RemoveHandler _virtualServerModeSrouceMOChanges.MoreRows, AddressOf VirtualServerModeSrouceMOChanges_MoreRows
                    RemoveHandler _virtualServerModeSrouceMOChanges.GetUniqueValues, AddressOf VirtualServerModeSrouceMOChanges_GetUniqueValues
                End If

                _virtualServerModeSrouceMOChanges = New VirtualServerModeSource()

                AddHandler _virtualServerModeSrouceMOChanges.AcquireInnerList, AddressOf VirtualServerModeSrouceMOChanges_AcquireInnerList
                AddHandler _virtualServerModeSrouceMOChanges.ConfigurationChanged, AddressOf VirtualServerModeSrouceMOChanges_ConfigurationChanged
                AddHandler _virtualServerModeSrouceMOChanges.MoreRows, AddressOf VirtualServerModeSrouceMOChanges_MoreRows
                AddHandler _virtualServerModeSrouceMOChanges.GetUniqueValues, AddressOf VirtualServerModeSrouceMOChanges_GetUniqueValues

                gcHistChanges.DataSource = Nothing
                gvHistChanges.OptionsView.ColumnAutoWidth = False
                gvHistChanges.Columns.Clear()
                gcHistChanges.DataSource = _virtualServerModeSrouceMOChanges

                datetimeEditHC = New RepositoryItemDateEdit()
                datetimeEditHC.VistaEditTime = DevExpress.Utils.DefaultBoolean.True
                datetimeEditHC.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime
                datetimeEditHC.Mask.UseMaskAsDisplayFormat = True
                If regionalSettings = False Then
                    datetimeEditHC.Mask.EditMask = "yyyy-MM-dd HH:mm:ss"
                Else
                    datetimeEditHC.Mask.EditMask = CultureInfoDefault.DateTimeFormat.ShortDatePattern & " " & CultureInfoDefault.DateTimeFormat.ShortTimePattern
                End If

                If dtHistChanges IsNot Nothing Then
                    For Each dtCol As DataColumn In dtHistChanges.Columns
                        If dtCol.DataType = GetType(DateTime) Then
                            gvHistChanges.Columns(dtCol.ColumnName).ColumnEdit = datetimeEditHC
                            gvHistChanges.Columns(dtCol.ColumnName).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                            If regionalSettings = False Then
                                gvHistChanges.Columns(dtCol.ColumnName).DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss"
                            Else
                                gvHistChanges.Columns(dtCol.ColumnName).DisplayFormat.FormatString = CultureInfoDefault.DateTimeFormat.ShortDatePattern & " " & CultureInfoDefault.DateTimeFormat.ShortTimePattern
                            End If
                        End If
                        gvHistChanges.Columns(dtCol.ColumnName).BestFit()
                    Next
                End If

                'For Each dtCol As DataColumn In dtHistChanges.Columns
                '    gvHistChanges.Columns(dtCol.ColumnName).BestFit()
                'Next
            End If

        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.UseWaitCursor = False
            Application.DoEvents()
        End Try
    End Sub

    Private Sub VirtualServerModeSource_AcquireInnerList(ByVal sender As Object, ByVal e As DevExpress.Data.VirtualServerModeAcquireInnerListEventArgs)
        Try
            Dim dtTempColumn As New DataTable
            Dim node As TreeListNode = lstViewMO.FocusedNode
            Dim data As DataRowView = lstViewMO.GetDataRecordByNode(node)

            If dtCMView Is Nothing Then
                If gcCMView.Tag = "MOData" Then
                    If data IsNot Nothing Then
                        dtTempColumn = CreateData(data.Item(0).ToString(), cmbVendor.SelectedItem.ToString, 0, 1, , "MOData") 'CreateData is called to initialize column structure for infinite grid
                    End If
                ElseIf gcCMView.Tag = "TemplateData" Then
                    dtTempColumn = CreateData(CType(cmbTemplate.SelectedItem, clsComboBoxItem).Value, cmbVendor.SelectedItem.ToString, 0, 1, , "TemplateData")
                Else
                    dtTempColumn = dtCMView.Rows.Cast(Of System.Data.DataRow).Take(1)
                End If
            End If

            e.InnerList = dtTempColumn.DefaultView
            e.AddMoreRowsFunc = AddressOf AddMoreRows
            e.ClearAndAddRowsFunc = AddressOf ClearAndAddMoreRows
            e.ReleaseAction = AddressOf ReleaseList
        Catch ex As Exception
        End Try
    End Sub

    Private Sub VirtualServerModeSrouceMOChanges_AcquireInnerList(ByVal sender As Object, ByVal e As DevExpress.Data.VirtualServerModeAcquireInnerListEventArgs)
        Try
            Dim dtTempColumn As New DataTable
            Dim node As TreeListNode = lstViewMO.FocusedNode
            Dim data As DataRowView = lstViewMO.GetDataRecordByNode(node)

            If dtHistChanges Is Nothing Then
                If gcHistChanges.Tag = "MOChanges" Then
                    If data IsNot Nothing Then
                        dtTempColumn = CreateData(data.Item(0).ToString(), cmbVendor.SelectedItem.ToString, 0, 1, , "MOChanges")
                    End If
                Else
                    dtTempColumn = dtHistChanges.Rows.Cast(Of System.Data.DataRow).Take(1)
                End If
            End If

            e.InnerList = dtTempColumn.DefaultView
            e.AddMoreRowsFunc = AddressOf AddMoreRowsMoChanges
            e.ClearAndAddRowsFunc = AddressOf ClearAndAddMoreRowsMoChanges
            e.ReleaseAction = AddressOf ReleaseListMoChanges
        Catch ex As Exception
        End Try
    End Sub

    Public Sub ReleaseList(ByVal list As IList)
        TryCast(list, DataView).Table.Rows.Clear()
    End Sub

    Public Sub ReleaseListMoChanges(ByVal list As IList)
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

    Public Function AddMoreRowsMoChanges(ByVal list As IList, ByVal en As IEnumerable) As IList
        Try
            Dim data = TryCast(en, DataView)
            For Each dr As DataRow In data.Table.Rows
                TryCast(list, DataView).Table.Rows.Add(dr.ItemArray)
            Next dr
            TryCast(list, DataView).Sort = currViewSortStrMoChanges
            Return list
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ClearAndAddMoreRowsMoChanges(ByVal list As IList, ByVal en As IEnumerable) As IList
        Try
            Dim data = TryCast(en, DataView)
            TryCast(list, DataView).Table.Rows.Clear()
            For Each dr As DataRow In data.Table.Rows
                TryCast(list, DataView).Table.Rows.Add(dr.ItemArray)
            Next dr
            TryCast(list, DataView).Sort = currViewSortStrMoChanges
            Return list
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub VirtualServerModeSource_MoreRows(sender As Object, e As DevExpress.Data.VirtualServerModeRowsEventArgs)
        Try
            If IsFirstTimeLoading Then
                gvCMView.OptionsView.WaitAnimationOptions = DevExpress.XtraEditors.WaitAnimationOptions.Indicator
            Else
                gvCMView.OptionsView.WaitAnimationOptions = DevExpress.XtraEditors.WaitAnimationOptions.Panel
            End If

            e.RowsTask = Task.Factory.StartNew(
              Function()
                  SyncLock objLock
                      Try
                          Dim dtData As New DataTable
                          If e.UserData Is Nothing Then
                              If gcCMView.Tag = "MOData" Then
                                  Dim node As TreeListNode = lstViewMO.FocusedNode
                                  Dim data As DataRowView = lstViewMO.GetDataRecordByNode(node)
                                  If data IsNot Nothing Then
                                      dtData = CreateData(data.Item(0).ToString, cmbVendor.SelectedItem.ToString, QueryOffset, Val(txtQueryBatchSize.Text), currViewRowFilter, "MOData")
                                  Else
                                      Dim dt As New DataTable
                                      Return New VirtualServerModeRowsTaskResult(dt.DefaultView, False, Nothing)
                                  End If
                              ElseIf gcCMView.Tag = "TemplateData" Then
                                  dtData = CreateData(CType(cmbTemplate.SelectedItem, clsComboBoxItem).Value, cmbVendor.SelectedItem.ToString, QueryOffset, Val(txtQueryBatchSize.Text), currViewRowFilter, "TemplateData")
                              End If
                          Else
                              dtData = CType(e.UserData, DataView).ToTable()
                          End If

                          Dim moreRows As Boolean = True
                          Dim rowCount As Integer = e.CurrentRowCount

                          If dtCMView IsNot Nothing Then
                              dtCMView.Merge(dtData)
                          Else
                              dtCMView = dtData
                          End If
                          QueryOffset = dtCMView.Rows.Count
                          Dim nextBatch = dtCMView.Clone()

                          Do While nextBatch.Rows.Count < dtData.Rows.Count
                              nextBatch.ImportRow(dtCMView.Rows(rowCount))
                              rowCount += 1
                          Loop

                          moreRows = e.CurrentRowCount + Val(txtQueryBatchSize.Text) <= rowCount
                          Return New VirtualServerModeRowsTaskResult(nextBatch.DefaultView, moreRows, Nothing)

                      Catch
                          Dim dt As New DataTable
                          Return New VirtualServerModeRowsTaskResult(dt.DefaultView, False, Nothing)
                      End Try
                  End SyncLock
              End Function, e.CancellationToken)
            If IsFirstTimeLoading Then
                IsFirstTimeLoading = False
                e.RowsTask.Wait(e.CancellationToken)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub VirtualServerModeSrouceMOChanges_MoreRows(sender As Object, e As DevExpress.Data.VirtualServerModeRowsEventArgs)
        Try
            If IsFirstTimeLoadingMoChanges Then
                gvHistChanges.OptionsView.WaitAnimationOptions = DevExpress.XtraEditors.WaitAnimationOptions.Indicator
            Else
                gvHistChanges.OptionsView.WaitAnimationOptions = DevExpress.XtraEditors.WaitAnimationOptions.Panel
            End If

            e.RowsTask = Task.Factory.StartNew(
              Function()
                  SyncLock objLockHC
                      Try
                          Dim dtData As New DataTable
                          If e.UserData Is Nothing Then
                              If gcHistChanges.Tag = "MOChanges" Then
                                  Dim node As TreeListNode = lstViewMO.FocusedNode
                                  Dim data As DataRowView = lstViewMO.GetDataRecordByNode(node)
                                  If data IsNot Nothing Then
                                      dtData = CreateData(data.Item(0).ToString, cmbVendor.SelectedItem.ToString, QueryOffsetMoChanges, Val(txtQueryBatchSize.Text), currViewRowFilterMoChanges, "MOChanges")
                                  End If
                              End If
                          Else
                              dtData = CType(e.UserData, DataView).ToTable()
                          End If

                          Dim moreRows As Boolean = True
                          Dim rowCount As Integer = e.CurrentRowCount

                          If dtHistChanges IsNot Nothing Then
                              dtHistChanges.Merge(dtData)
                          Else
                              dtHistChanges = dtData
                          End If
                          QueryOffsetMoChanges = dtHistChanges.Rows.Count
                          Dim nextBatch = dtHistChanges.Clone()

                          Do While nextBatch.Rows.Count < dtData.Rows.Count
                              nextBatch.ImportRow(dtHistChanges.Rows(rowCount))
                              rowCount += 1
                          Loop

                          moreRows = e.CurrentRowCount + Val(txtQueryBatchSize.Text) <= rowCount
                          Return New VirtualServerModeRowsTaskResult(nextBatch.DefaultView, moreRows, Nothing)

                      Catch
                          Dim dt As New DataTable
                          Return New VirtualServerModeRowsTaskResult(dt.DefaultView, False, Nothing)
                      End Try
                  End SyncLock
              End Function, e.CancellationToken)
            If IsFirstTimeLoadingMoChanges Then
                IsFirstTimeLoadingMoChanges = False
                e.RowsTask.Wait(e.CancellationToken)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub virtualServerModeSource_ConfigurationChanged(ByVal sender As Object, ByVal e As DevExpress.Data.VirtualServerModeRowsEventArgs)
        Try
            QueryOffset = 0
            dtCMView = Nothing

            'If TypeOf e.ConfigurationInfo.Filter Is DevExpress.Data.Filtering.BinaryOperator Then
            '    Try
            '        Dim rightOperand As DevExpress.Data.Filtering.OperandValue = CType((CType(e.ConfigurationInfo.Filter, DevExpress.Data.Filtering.BinaryOperator)).RightOperand, DevExpress.Data.Filtering.OperandValue)
            '        Dim value As Double = CDbl(rightOperand.Value)
            '        Dim objBinary As New BinaryOperator(CType((CType(e.ConfigurationInfo.Filter, DevExpress.Data.Filtering.BinaryOperator)).LeftOperand, DevExpress.Data.Filtering.OperandProperty).PropertyName, value, CType(e.ConfigurationInfo.Filter, DevExpress.Data.Filtering.BinaryOperator).OperatorType)
            '        currViewRowFilter = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(objBinary)
            '    Catch ex As Exception
            '    End Try
            'End If

            currViewRowFilter = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(e.ConfigurationInfo.Filter)

            '(commented on 10/03/2021) ******************************************************************************************************************************************************************************************************************************************************************************************************************'
            'If TypeOf e.ConfigurationInfo.Filter Is DevExpress.Data.Filtering.GroupOperator Then
            '    Try
            '        If CType(e.ConfigurationInfo.Filter, GroupOperator).Operands.Count > 0 Then
            '            Dim objGO As New GroupOperator()
            '            Dim lastColumn As String = ""
            '            For Each obj In CType(e.ConfigurationInfo.Filter, GroupOperator).Operands
            '                If TypeOf (obj) Is BinaryOperator AndAlso CType(CType(obj, BinaryOperator).LeftOperand, DevExpress.Data.Filtering.OperandProperty).PropertyName.ToLower() <> lastColumn Then
            '                    lastColumn = CType(CType(obj, BinaryOperator).LeftOperand, DevExpress.Data.Filtering.OperandProperty).PropertyName.ToLower()
            '                    If CType(CType(obj, BinaryOperator).LeftOperand, DevExpress.Data.Filtering.OperandProperty).PropertyName.ToLower().Contains("date") Then
            '                        Dim rightOperand As OperandValue = CType(CType(CType(e.ConfigurationInfo.Filter, GroupOperator).Operands(0), BinaryOperator).RightOperand, OperandValue)
            '                        Dim value As Date = CDate(rightOperand.Value)
            '                        objGO.Operands.Add(New BetweenOperator(CType(CType(CType(e.ConfigurationInfo.Filter, GroupOperator).Operands(0), BinaryOperator).LeftOperand, OperandProperty).PropertyName,
            '                            New DateTime(value.Year, value.Month, value.Day, 0, 0, 0),
            '                            New DateTime(value.Year, value.Month, value.Day, 23, 59, 59)))
            '                    Else
            '                        objGO.Operands.Add(obj)
            '                    End If
            '                ElseIf TypeOf (obj) Is FunctionOperator Then
            '                    objGO.Operands.Add(obj)
            '                End If
            '            Next
            '            currViewRowFilter = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(objGO)
            '            currViewRowFilter = currViewRowFilter.Replace("#", "'")
            '        End If
            '    Catch ex As Exception
            '    End Try
            'End If
            '******************************************************************************************************************************************************************************************************************************************************************************************************************'

            'If TypeOf e.ConfigurationInfo.Filter Is DevExpress.Data.Filtering.GroupOperator Then
            '    Try
            '        If CType(e.ConfigurationInfo.Filter, GroupOperator).Operands.Count > 0 Then
            '            If CType(CType(CType(e.ConfigurationInfo.Filter, GroupOperator).Operands(0), BinaryOperator).LeftOperand, DevExpress.Data.Filtering.OperandProperty).PropertyName.ToLower().Contains("date") Then
            '                Dim rightOperand As OperandValue = CType(CType(CType(e.ConfigurationInfo.Filter, GroupOperator).Operands(0), BinaryOperator).RightOperand, OperandValue)
            '                Dim value As Date = CDate(rightOperand.Value)

            '                currViewRowFilter = CriteriaToWhereClauseHelper.GetDataSetWhere(
            '                New BetweenOperator(CType(CType(CType(e.ConfigurationInfo.Filter, GroupOperator).Operands(0), BinaryOperator).LeftOperand, OperandProperty).PropertyName,
            '                New DateTime(value.Year, value.Month, value.Day, 0, 0, 0),
            '                New DateTime(value.Year, value.Month, value.Day, 23, 59, 59)))
            '                currViewRowFilter = currViewRowFilter.Replace("#", "'")
            '            End If
            '        End If
            '    Catch ex As Exception
            '    End Try
            'End If

            If e.ConfigurationInfo.SortInfo IsNot Nothing AndAlso e.ConfigurationInfo.SortInfo.Length > 0 Then
                currViewSortStr = e.ConfigurationInfo.SortInfo(0).ToString()
            End If

            Dim dtData As New DataTable
            If gcCMView.Tag = "MOData" Then
                Dim node As TreeListNode = lstViewMO.FocusedNode
                Dim data As DataRowView = lstViewMO.GetDataRecordByNode(node)
                If data IsNot Nothing Then
                    dtData = CreateData(data.Item(0).ToString, cmbVendor.SelectedItem.ToString, QueryOffset, Val(txtQueryBatchSize.Text), currViewRowFilter, "MOData")
                Else
                    e.UserData = Nothing
                End If
            ElseIf gcCMView.Tag = "TemplateData" Then
                dtData = CreateData(CType(cmbTemplate.SelectedItem, clsComboBoxItem).Value, cmbVendor.SelectedItem.ToString, QueryOffset, Val(txtQueryBatchSize.Text), currViewRowFilter, "TemplateData")
            End If
            e.UserData = dtData.DefaultView
        Catch ex As Exception
        End Try
    End Sub

    Private Sub VirtualServerModeSrouceMOChanges_ConfigurationChanged(ByVal sender As Object, ByVal e As DevExpress.Data.VirtualServerModeRowsEventArgs)
        Try
            QueryOffsetMoChanges = 0
            dtHistChanges = Nothing

            currViewRowFilterMoChanges = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(e.ConfigurationInfo.Filter)

            If TypeOf e.ConfigurationInfo.Filter Is DevExpress.Data.Filtering.GroupOperator Then
                Try
                    If CType(e.ConfigurationInfo.Filter, GroupOperator).Operands.Count > 0 Then
                        Dim objGO As New GroupOperator()
                        Dim lastColumn As String = ""
                        For Each obj In CType(e.ConfigurationInfo.Filter, GroupOperator).Operands
                            If TypeOf (obj) Is BinaryOperator AndAlso CType(CType(obj, BinaryOperator).LeftOperand, DevExpress.Data.Filtering.OperandProperty).PropertyName.ToLower() <> lastColumn Then
                                lastColumn = CType(CType(obj, BinaryOperator).LeftOperand, DevExpress.Data.Filtering.OperandProperty).PropertyName.ToLower()
                                If CType(CType(obj, BinaryOperator).LeftOperand, DevExpress.Data.Filtering.OperandProperty).PropertyName.ToLower().Contains("date") Then
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
                        currViewRowFilterMoChanges = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(objGO)
                        currViewRowFilterMoChanges = currViewRowFilterMoChanges.Replace("#", "'")
                    End If
                Catch ex As Exception
                End Try
            End If

            If e.ConfigurationInfo.SortInfo IsNot Nothing AndAlso e.ConfigurationInfo.SortInfo.Length > 0 Then
                currViewSortStrMoChanges = e.ConfigurationInfo.SortInfo(0).ToString()
            End If

            Dim dtData As New DataTable
            If gcHistChanges.Tag = "MOChanges" Then
                Dim node As TreeListNode = lstViewMO.FocusedNode
                Dim data As DataRowView = lstViewMO.GetDataRecordByNode(node)
                If data IsNot Nothing Then
                    dtData = CreateData(data.Item(0).ToString, cmbVendor.SelectedItem.ToString, QueryOffsetMoChanges, Val(txtQueryBatchSize.Text), currViewRowFilterMoChanges, "MOChanges")
                End If
            End If
            e.UserData = dtData.DefaultView
        Catch ex As Exception
        End Try
    End Sub

    Private Sub virtualServerModeSource_GetUniqueValues(ByVal sender As Object, ByVal e As VirtualServerModeGetUniqueValuesEventArgs)
        e.UniqueValuesTask =
            New System.Threading.Tasks.Task(Of Object())(
            Function()
                Dim filterQry As String = GetFilterQueryFromObjectTree()

                If currViewRowFilter IsNot Nothing AndAlso currViewRowFilter <> "" Then
                    filterQry = IIf(filterQry Is Nothing, " WHERE ", filterQry & " AND ") & currViewRowFilter.Replace("'", "''")
                End If

                Dim node As TreeListNode = lstViewMO.FocusedNode
                Dim data As DataRowView = lstViewMO.GetDataRecordByNode(node)
                If data IsNot Nothing Then
                    Dim dt As New DataTable
                    dt = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, "EXEC [dbo].[CM_MO_Column_Data] '" & cmbVendor.SelectedItem.ToString & "', '" & data.Item(0).ToString() & "', '[" & e.ValuesPropertyName & "]','" & filterQry & "'")
                    Dim filterValue() As Object
                    filterValue = dt.Rows.OfType(Of DataRow)().Select(Function(x) x.Item(0)).ToArray()
                    Return filterValue
                Else
                    Return Nothing
                End If
            End Function, e.CancellationToken)
    End Sub

    Private Sub VirtualServerModeSrouceMOChanges_GetUniqueValues(ByVal sender As Object, ByVal e As VirtualServerModeGetUniqueValuesEventArgs)
        e.UniqueValuesTask =
            New System.Threading.Tasks.Task(Of Object())(
            Function()
                Dim node As TreeListNode = lstViewMO.FocusedNode
                Dim data As DataRowView = lstViewMO.GetDataRecordByNode(node)
                If data IsNot Nothing Then
                    Dim dt As New DataTable
                    dt = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, "EXEC [dbo].[CM_MOChanges_Column_Data] '" & cmbVendor.SelectedItem.ToString & "', '" & data.Item(0).ToString() & "', '[" & e.ValuesPropertyName & "]'")
                    Dim filterValue() As Object
                    filterValue = dt.Rows.OfType(Of DataRow)().Select(Function(x) x.Item(0)).ToArray()
                    Return filterValue
                Else
                    Return Nothing
                End If
            End Function, e.CancellationToken)
    End Sub

#End Region

End Class