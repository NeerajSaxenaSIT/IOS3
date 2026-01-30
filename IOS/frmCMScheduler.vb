Imports IOS.Library
Imports DevExpress.XtraEditors

Public Class frmCMScheduler

#Region "Variables"

    Private dtTech As DataTable = Nothing
    Private dtCategoryManager As New DataTable()
    Private dtObject As DataTable = Nothing
    Private IsPageLoaded As Boolean = False
    Private IsFirstTime As Boolean = False
    Private flag As Boolean = True
    Private IsToShowCategoryManagerContextMeunStrip As Boolean = False
    Private IsTreeDragOrList As Boolean = False     'True for tree False for list
    Private IsTabChangedUsingCotextMenuClick As Boolean = False
    Private isDoubleClickOrkeyPress As Boolean = False
    Private dtSchedule As DataTable = Nothing
    Private rowIndex As New List(Of KeyValuePair(Of Integer, Color))

    Public dtCategoryData As DataTable = Nothing
    Public dsVenderData As DataSet = Nothing
    Public sVenderID As String = Nothing
    Public sObjectID As String = Nothing
    Public sObjectName As String = Nothing
    Public sObjectType As String = Nothing

#End Region

    Private Sub cm_GridViewMap_Opening(sender As System.Object, e As System.ComponentModel.CancelEventArgs)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim cmsTemp As ContextMenuStrip = TryCast(sender, ContextMenuStrip)
            Dim mapGridTmp As DevExpress.XtraGrid.GridControl = TryCast(cmsTemp.SourceControl, DevExpress.XtraGrid.GridControl)
            tsmi_RecordCount.Text = "Record Count: " & mapGridTmp.DefaultView.RowCount
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#Region "Category Manager"

    Sub ManageCategoryManaterOnVenderChange()
        flag = True
        lblMessage.Text = ""
        If (IsFirstTime) Then
            Dim data As New DataTable()
            data.Columns.Add("VendorID", Type.GetType("System.String"))
            data.Columns.Add("ObjectID", Type.GetType("System.String"))
            data.Columns.Add("ObjectName", Type.GetType("System.String"))
            data.Columns.Add("ObjectType", Type.GetType("System.String"))
            data.Columns.Add("CategoryID", Type.GetType("System.Int32"))
            data.Columns.Add("CategoryName", Type.GetType("System.String"))
            data.Columns.Add("HasSchedule", Type.GetType("System.String"))
            data.Rows.Clear()
            bindCategoryGridView(data)
            IsFirstTime = False
        End If
        BindTechnology()
    End Sub

    Sub bindCategoryGridView(ByRef data As DataTable)
        gcCategoryManager.SuspendLayout()
        gcCategoryManager.Refresh()
        dtCategoryManager = data.Copy()
        If Not (data.Columns.Contains(IOSCategoryManager.IS_NEWROW)) Then
            Dim IsNewRow As New DataColumn(IOSCategoryManager.IS_NEWROW, Type.GetType("System.Boolean"))
            IsNewRow.DefaultValue = False
            dtCategoryManager.Columns.Add(IsNewRow)
        End If
        If Not (data.Columns.Contains(IOSCategoryManager.IS_UPDATED)) Then
            Dim isUpdated As New DataColumn(IOSCategoryManager.IS_UPDATED, Type.GetType("System.Boolean"))
            isUpdated.DefaultValue = False
            dtCategoryManager.Columns.Add(isUpdated)
        End If

        gvCategoryManager.Columns.AddField(IOSCategoryManager.VENDER_ID).Visible = True
        gvCategoryManager.Columns.AddField(IOSCategoryManager.OBJECT_ID).Visible = True

        gvCategoryManager.Columns.AddField(IOSCategoryManager.OBJECT_NAME).Visible = True
        gvCategoryManager.Columns.AddField(IOSCategoryManager.OBJECT_TYPE).Visible = True

        gvCategoryManager.Columns.AddField(IOSCategoryManager.CATEGORY_NAME).Visible = True
        gvCategoryManager.Columns.AddField(IOSCategoryManager.HAS_SCHEDULE).Visible = True

        gvCategoryManager.OptionsBehavior.AutoPopulateColumns = False
        gcCategoryManager.DataSource = data
        SetAutoFiltersOnGrid(gcCategoryManager, gvCategoryManager, True)
        gvCategoryManager.OptionsView.ColumnAutoWidth = False
        gcCategoryManager.Refresh()
    End Sub

    Sub SetAutoFiltersOnGrid(ByRef gdvObject As DevExpress.XtraGrid.GridControl, ByRef gridView As DevExpress.XtraGrid.Views.Grid.GridView, Optional ByVal isFromCategoryManager As Boolean = False)
        Dim totalColumns As Int32 = IIf(isFromCategoryManager, gridView.Columns.Count - 1, gridView.Columns.Count)
        Dim frmCatmanagerwidth As Integer = gdvObject.Width - 25
        If (totalColumns > 0) Then
            frmCatmanagerwidth = frmCatmanagerwidth / totalColumns
            Dim k As Integer
            For k = 0 To totalColumns - 1
                gridView.Columns.Item(k).OptionsFilter.AllowFilter = True
                gridView.Columns.Item(k).Width = frmCatmanagerwidth
            Next
            RemoveFiltterFromGrid(gdvObject, gridView)
        End If
    End Sub

    Private Sub RemoveFiltterFromGrid(ByRef gdvObjectRemove As DevExpress.XtraGrid.GridControl, ByRef gView As DevExpress.XtraGrid.Views.Grid.GridView)
        gView.RowFilter.Remove(0, gView.RowCount)
        gdvObjectRemove.Refresh()
        gdvObjectRemove.ResumeLayout()
    End Sub

    Private Function ScheduleData() As DataTable
        Dim objectStr As String = ""
        Dim couter As Integer = 0
        If (dtSchedule Is Nothing) Then
            couter = cmbTargetObject.Properties.Items.Count - 1
            For Each liObject As clsComboBoxItem In cmbTargetObject.Properties.Items
                If (liObject.Value IsNot Nothing) Then
                    objectStr += " ObjectType='" & liObject.Text & "'"
                    If (couter > 1) Then
                        objectStr += " or "
                        couter -= 1
                    End If
                End If
            Next
            Dim cmdText As String = "select pco.* from dbo.IOS_Parameters_CategoriesObject_Scheduled pco inner join IOS_Parameters_Scheduled PS on ps.ScheduleID = pco.ScheduleID Where PS.Executed=0 and (" & objectStr & ")"
            dtSchedule = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, cmdText)
        End If
        Return dtSchedule
    End Function

    Sub HideObjectID()
        If (gvCategoryManager.Columns.Count > 0) Then
            gvCategoryManager.Columns.Item(1).Visible = False
        End If
    End Sub

    Private Function ScheduleCounter(ByVal objID As String, ByVal objName As String, ByVal objType As String) As Integer
        Dim dt As DataTable = ScheduleData()
        Return dt.Select("ObjectID='" & objID & "' AND ObjectName='" & objName & "' AND ObjectType='" & objType & "'").Length
    End Function

    Private Function ScheduleIsOrNot(ByVal objID As String, ByVal objName As String, ByVal objType As String) As Integer
        Dim cmdText As String = "select COUNT(*) as SchedulCounter from dbo.IOS_Parameters_CategoriesObject_Scheduled pco where pco.[ObjectID]='" & objID & "' and [ObjectName]='" & objName & "' and [ObjectType]='" & objType & "'"
        Dim count As Integer = IOS.DataLibrary.DataAccessorODBC.ExecuteScalar(connStrIOSServer, cmdText)
        Return count
    End Function

    Private Sub SetObjectData()
        If (dtObject Is Nothing) Then
            Dim cmdText As String = "select pcs.VendorID,pcs.ObjectID,pcs.ObjectName,pcs.ObjectType,pcs.CategoryID,pc.CategoryName from IOS_Parameters_CategoriesObjects pcs left outer join dbo.IOS_Parameters_Categories pc on pcs.CategoryID= pc.CategoryID where [ObjectType]='" & cmbTargetObject.SelectedItem.ToString & "'" 'pcs.[ObjectID]='" & objID & "' and [ObjectName]='" & objName & "' and
            dtObject = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, cmdText)
        End If
    End Sub

    Private Function GetObjectData(ByVal objID As String, ByVal objName As String, ByVal objType As String) As DataRow
        If (dtObject.Rows.Count <= 0) Then
            SetObjectData()
        End If
        Dim selectedRows() As DataRow = dtObject.Select(String.Format("ObjectID='{0}' AND ObjectName='{1}' AND ObjectType='{2}'", objID, objName, objType))
        If (selectedRows.Length > 0) Then
            Return selectedRows(0)
        Else
            Return Nothing
        End If
    End Function

    Private Function objectExitOrNot(ByVal objID As String, ByVal objName As String, ByVal objType As String) As DataTable
        Dim cmdText As String = "select pcs.VendorID,pcs.ObjectID,pcs.ObjectName,pcs.ObjectType,pcs.CategoryID,pc.CategoryName from IOS_Parameters_CategoriesObjects pcs left outer join dbo.IOS_Parameters_Categories pc on pcs.CategoryID= pc.CategoryID where pcs.[ObjectID]='" & objID & "' and [ObjectName]='" & objName & "' and [ObjectType]='" & objType & "'"
        Return IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, cmdText)
    End Function

    Private Sub vbtnModifyCellType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModifyCellType.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Dim isFilter As Boolean = True
        Try
            Dim dtCategoryByObject As DataTable = New DataTable()
            dtCategoryByObject.Columns.Add(IOSCategoryManager.VENDER_ID)
            dtCategoryByObject.Columns.Add(IOSCategoryManager.OBJECT_ID)
            dtCategoryByObject.Columns.Add(IOSCategoryManager.OBJECT_NAME)
            dtCategoryByObject.Columns.Add(IOSCategoryManager.OBJECT_TYPE)
            dtCategoryByObject.Columns.Add(IOSCategoryManager.CATEGORY_ID)
            dtCategoryByObject.Columns.Add(IOSCategoryManager.CATEGORY_NAME)
            dtCategoryByObject.Columns.Add(IOSCategoryManager.IS_UPDATED)
            dtCategoryByObject.Columns.Add(IOSCategoryManager.IS_NEWROW)
            Dim frmCategoryManager As New frmCategoryManagerDialog()
            frmCategoryManager.SetCategoryData(Me.dtCategoryData)
            frmCategoryManager.ShowDialog()
            Dim categoryManage As IOSCategoryManager = frmCategoryManager.ReturnData
            If (categoryManage IsNot Nothing) Then

                If (categoryManage.IsApplyTo.Equals(IOSCategoryManager.BY_OBJECT)) Then
                    SetObjectData()
                    Dim nodes As TreeNodeCollection = tvObjectTreeStats.Nodes
                    If nodes.Count > 0 Then
                        For Each selectedNode As TreeNode In nodes
                            GetNodeRecursive(selectedNode, categoryManage.GetCategoryID, categoryManage.GetCategoryName, dtCategoryByObject)
                        Next
                    End If
                End If
                If (categoryManage.IsApplyTo.Equals(IOSCategoryManager.BY_GRID)) Then
                    For iRowCnt As Integer = 0 To gvCategoryManager.RowCount - 1
                        If Not gvCategoryManager.RowFilter Is Nothing Then
                            isFilter = False
                        End If
                        For iCnt As Integer = 0 To gvCategoryManager.RowCount - 1
                            If (Not gvCategoryManager.RowFilter Is Nothing) Then
                                gvCategoryManager.SetRowCellValue(iCnt, gvCategoryManager.Columns.Item(5), "Yes")
                                dtCategoryManager.Rows(iCnt)(IOSCategoryManager.HAS_SCHEDULE) = "Yes"

                                Dim drByObject As DataRow = dtCategoryByObject.NewRow()
                                drByObject(IOSCategoryManager.VENDER_ID) = gvCategoryManager.GetRowCellValue(iCnt, gvCategoryManager.Columns.Item(0))
                                drByObject(IOSCategoryManager.OBJECT_ID) = gvCategoryManager.GetRowCellValue(iCnt, gvCategoryManager.Columns.Item(1))
                                drByObject(IOSCategoryManager.OBJECT_NAME) = gvCategoryManager.GetRowCellValue(iCnt, gvCategoryManager.Columns.Item(2))
                                drByObject(IOSCategoryManager.OBJECT_TYPE) = gvCategoryManager.GetRowCellValue(iCnt, gvCategoryManager.Columns.Item(3))
                                drByObject(IOSCategoryManager.CATEGORY_ID) = categoryManage.GetCategoryID
                                drByObject(IOSCategoryManager.CATEGORY_NAME) = categoryManage.GetCategoryName
                                Dim dtExitOrNot As DataTable = objectExitOrNot(gvCategoryManager.GetRowCellValue(iCnt, gvCategoryManager.Columns.Item(1)), gvCategoryManager.GetRowCellValue(iCnt, gvCategoryManager.Columns.Item(2)), gvCategoryManager.GetRowCellValue(iCnt, gvCategoryManager.Columns.Item(3)))
                                If (dtExitOrNot.Rows.Count > 0) Then
                                    drByObject(IOSCategoryManager.IS_UPDATED) = True
                                    drByObject(IOSCategoryManager.IS_NEWROW) = False
                                    dtCategoryManager.Rows(iCnt)(IOSCategoryManager.IS_UPDATED) = True
                                Else

                                    drByObject(IOSCategoryManager.IS_UPDATED) = False
                                    drByObject(IOSCategoryManager.IS_NEWROW) = True
                                    dtCategoryManager.Rows(iCnt)(IOSCategoryManager.IS_UPDATED) = False
                                End If
                                dtCategoryByObject.Rows.Add(drByObject)
                            End If
                        Next
                    Next

                    If (isFilter) Then
                        For iRowCnt As Integer = 0 To gvCategoryManager.RowCount - 1
                            gvCategoryManager.SetRowCellValue(iRowCnt, gvCategoryManager.Columns.Item(5), "Yes")
                            dtCategoryManager.Rows(iRowCnt)(IOSCategoryManager.HAS_SCHEDULE) = "Yes"

                            Dim drByObject As DataRow = dtCategoryByObject.NewRow()
                            drByObject(IOSCategoryManager.VENDER_ID) = gvCategoryManager.GetRowCellValue(iRowCnt, gvCategoryManager.Columns.Item(0))
                            drByObject(IOSCategoryManager.OBJECT_ID) = gvCategoryManager.GetRowCellValue(iRowCnt, gvCategoryManager.Columns.Item(1))
                            drByObject(IOSCategoryManager.OBJECT_NAME) = gvCategoryManager.GetRowCellValue(iRowCnt, gvCategoryManager.Columns.Item(2))
                            drByObject(IOSCategoryManager.OBJECT_TYPE) = gvCategoryManager.GetRowCellValue(iRowCnt, gvCategoryManager.Columns.Item(3))
                            drByObject(IOSCategoryManager.CATEGORY_ID) = categoryManage.GetCategoryID
                            drByObject(IOSCategoryManager.CATEGORY_NAME) = categoryManage.GetCategoryName

                            Dim dtExitOrNot As DataTable = objectExitOrNot(gvCategoryManager.GetRowCellValue(iRowCnt, gvCategoryManager.Columns.Item(1)), gvCategoryManager.GetRowCellValue(iRowCnt, gvCategoryManager.Columns.Item(2)), gvCategoryManager.GetRowCellValue(iRowCnt, gvCategoryManager.Columns.Item(3)))
                            If (dtExitOrNot.Rows.Count > 0) Then
                                drByObject(IOSCategoryManager.IS_UPDATED) = True
                                drByObject(IOSCategoryManager.IS_NEWROW) = False
                                dtCategoryManager.Rows(iRowCnt)(IOSCategoryManager.IS_UPDATED) = True
                            Else
                                drByObject(IOSCategoryManager.IS_UPDATED) = False
                                drByObject(IOSCategoryManager.IS_NEWROW) = True
                                dtCategoryManager.Rows(iRowCnt)(IOSCategoryManager.IS_UPDATED) = False
                            End If
                            dtCategoryByObject.Rows.Add(drByObject)
                        Next
                    End If
                End If

                If (categoryManage.IsApplyTo.Equals(IOSCategoryManager.BY_SELECTION)) Then
                    'Here Code for By Selection
                    For iRowCnt As Integer = 0 To gvCategoryManager.RowCount - 1
                        If (gvCategoryManager.FocusedRowHandle = iRowCnt) Then
                            gvCategoryManager.SetRowCellValue(iRowCnt, gvCategoryManager.Columns.Item(5), "Yes")
                            dtCategoryManager.Rows(iRowCnt)(IOSCategoryManager.HAS_SCHEDULE) = "Yes"

                            Dim drByObject As DataRow = dtCategoryByObject.NewRow()
                            drByObject(IOSCategoryManager.VENDER_ID) = gvCategoryManager.GetRowCellValue(iRowCnt, gvCategoryManager.Columns.Item(0))
                            drByObject(IOSCategoryManager.OBJECT_ID) = gvCategoryManager.GetRowCellValue(iRowCnt, gvCategoryManager.Columns.Item(0))
                            drByObject(IOSCategoryManager.OBJECT_NAME) = gvCategoryManager.GetRowCellValue(iRowCnt, gvCategoryManager.Columns.Item(0))
                            drByObject(IOSCategoryManager.OBJECT_TYPE) = gvCategoryManager.GetRowCellValue(iRowCnt, gvCategoryManager.Columns.Item(0))
                            drByObject(IOSCategoryManager.CATEGORY_ID) = categoryManage.GetCategoryID
                            drByObject(IOSCategoryManager.CATEGORY_NAME) = categoryManage.GetCategoryName
                            Dim dtExitOrNot As DataTable = objectExitOrNot(gvCategoryManager.GetRowCellValue(iRowCnt, gvCategoryManager.Columns.Item(1)), gvCategoryManager.GetRowCellValue(iRowCnt, gvCategoryManager.Columns.Item(2)), gvCategoryManager.GetRowCellValue(iRowCnt, gvCategoryManager.Columns.Item(3)))
                            If (dtExitOrNot.Rows.Count > 0) Then
                                drByObject(IOSCategoryManager.IS_UPDATED) = True
                                drByObject(IOSCategoryManager.IS_NEWROW) = False
                                dtCategoryManager.Rows(iRowCnt)(IOSCategoryManager.IS_UPDATED) = True
                            Else
                                drByObject(IOSCategoryManager.IS_UPDATED) = False
                                drByObject(IOSCategoryManager.IS_NEWROW) = True
                                dtCategoryManager.Rows(iRowCnt)(IOSCategoryManager.IS_UPDATED) = False
                            End If
                            dtCategoryByObject.Rows.Add(drByObject)
                        End If
                    Next
                End If

                Dim scheduleStartDate As DateTime = IIf(categoryManage.IsSchdule, categoryManage.GetStartDate, DateTime.Now)
                SetScheduleCatManager(scheduleStartDate, categoryManage.GetEndDate, categoryManage.GetSchduleType, dtCategoryByObject)
                If (categoryManage.IsApplyTo.Equals(IOSCategoryManager.BY_GRID)) Then
                    lblMessage.Text = "Schedule has been set on Grid Objects."
                    lblMessage.ForeColor = Color.DarkBlue
                ElseIf (categoryManage.IsApplyTo.Equals(IOSCategoryManager.BY_OBJECT)) Then
                    lblMessage.Text = "Schedule has been set on Tree Objects."
                    lblMessage.ForeColor = Color.DarkBlue
                ElseIf (categoryManage.IsApplyTo.Equals(IOSCategoryManager.BY_SELECTION)) Then
                    lblMessage.Text = "Schedule has been set on Selection."
                    lblMessage.ForeColor = Color.DarkBlue
                End If
                gcCategoryManager.SuspendLayout()
                btnShowCellType.PerformClick()
            End If
        Catch ex As Exception
            lblMessage.Text = "Sorry ! Not able to Update."
            lblMessage.ForeColor = Color.Red
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            IOS.DataLibrary.DataAccessorODBC.KeepConnectionOpen = False
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub GetObjectByTree(ByVal treeNodeObject As TreeNode)
        If (treeNodeObject.Checked) Then
            If (Not treeNodeObject.Tag = Nothing) Then
                If (treeNodeObject.ImageKey.ToLower.Trim = cmbTargetObject.SelectedItem.ToString.ToLower.Trim) Then
                    SetTreeNodeValue(treeNodeObject.Tag, treeNodeObject.Text, treeNodeObject.ImageKey, False, 0, "")
                End If
            End If
        End If
        For Each childNode As TreeNode In treeNodeObject.Nodes
            GetObjectByTree(childNode)
        Next
    End Sub

    Private Sub SetTreeNodeValue(ByVal objectId As String, ByVal objectName As String, ByVal objectType As String, ByVal isCatByObject As Boolean, ByVal catIDByObj As Integer, ByVal catNameByObj As String)

        Dim catID As Integer = 0
        Dim catName As String = Nothing
        Dim isScheduleExit As Integer = 0
        Dim dtExitOrNot As DataRow = GetObjectData(objectId, objectName, objectType)
        If (Not dtExitOrNot Is Nothing) Then
            catID = Convert.ToInt32(dtExitOrNot(IOSCategoryManager.CATEGORY_ID))
            If (catID.Equals(0)) Then
                catName = ""
            Else
                catName = dtExitOrNot(IOSCategoryManager.CATEGORY_NAME)
            End If
        Else
            If (isCatByObject) Then
                catID = catIDByObj
                catName = catNameByObj
            End If
        End If
        Dim sRows = From w In dtCategoryManager.AsEnumerable()
                    Where w.Field(Of String)("ObjectID") = objectId AndAlso w.Field(Of String)("ObjectName") = objectName AndAlso w.Field(Of String)("ObjectType") = objectType
                    Select w

        Dim lflag As Boolean = True
        For Each Item As DataRow In sRows
            lflag = False
            Exit For
        Next

        If lflag Then
            isScheduleExit = ScheduleCounter(objectId, objectName, objectType)
            Dim dr As DataRow = dtCategoryManager.NewRow()
            dr(IOSCategoryManager.VENDER_ID) = cmbVendor.SelectedItem.ToString
            dr(IOSCategoryManager.OBJECT_ID) = objectId
            dr(IOSCategoryManager.OBJECT_NAME) = objectName
            dr(IOSCategoryManager.OBJECT_TYPE) = objectType
            dr(IOSCategoryManager.CATEGORY_ID) = catID
            dr(IOSCategoryManager.CATEGORY_NAME) = catName
            If (isScheduleExit > 0) Then
                dr("HasSchedule") = "Yes"
            ElseIf (isCatByObject) Then
                dr("HasSchedule") = "Yes"
            Else
                dr("HasSchedule") = "No"
            End If
            If (Not dtExitOrNot Is Nothing) Then
                dr(IOSCategoryManager.IS_NEWROW) = False
            Else
                dr(IOSCategoryManager.IS_NEWROW) = True
            End If
            dtCategoryManager.Rows.Add(dr)
        End If
    End Sub

    Private Sub gcCategoryManager_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles gcCategoryManager.MouseDown
        IsToShowCategoryManagerContextMeunStrip = False
        If (e.Button = MouseButtons.Right) Then
            Dim cell As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = gvCategoryManager.CalcHitInfo(New System.Drawing.Point(e.X, e.Y))
            If (cell IsNot Nothing) Then
                Dim bInRowCell As Boolean = cell.InRowCell
                If (bInRowCell) Then
                    gvCategoryManager.SelectCell(cell.RowHandle, cell.Column)
                    gcCategoryManager.ContextMenuStrip.Show(Me.gcCategoryManager, New System.Drawing.Point(e.X, e.Y))
                    IsToShowCategoryManagerContextMeunStrip = True
                End If
            Else
                gcCategoryManager.ContextMenuStrip.Hide()
            End If
        End If
    End Sub

    Private Sub gcCategoryManager_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles gcCategoryManager.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub GetNodeRecursive(ByVal treeNodeObject As TreeNode, ByVal catID As String, ByVal catName As String, ByRef dt As DataTable)
        If (treeNodeObject.Checked) Then
            If (Not treeNodeObject.Tag = Nothing) Then
                If (treeNodeObject.Level = Treeview_GetNodeLevel(cmbVendor.SelectedItem.ToString & " " & cmbTechnology.SelectedItem.ToString, cmbTargetObject.SelectedItem.ToString.ToLower.Trim, cmbTargetObject, cmbVendor.SelectedItem.ToString)) Then
                    Dim drByObject As DataRow = dt.NewRow()
                    drByObject(IOSCategoryManager.VENDER_ID) = cmbVendor.SelectedItem.ToString
                    drByObject(IOSCategoryManager.OBJECT_ID) = treeNodeObject.Tag
                    drByObject(IOSCategoryManager.OBJECT_NAME) = treeNodeObject.Text
                    drByObject(IOSCategoryManager.OBJECT_TYPE) = cmbTargetObject.SelectedItem.ToString
                    drByObject(IOSCategoryManager.CATEGORY_ID) = catID
                    drByObject(IOSCategoryManager.CATEGORY_NAME) = catName

                    Dim drObject As DataRow = GetObjectData(drByObject(IOSCategoryManager.OBJECT_ID), drByObject(IOSCategoryManager.OBJECT_NAME), drByObject(IOSCategoryManager.OBJECT_TYPE))

                    If (drObject IsNot Nothing) Then
                        drByObject(IOSCategoryManager.IS_UPDATED) = True
                        drByObject(IOSCategoryManager.IS_NEWROW) = False
                    Else
                        drByObject(IOSCategoryManager.IS_UPDATED) = False
                        drByObject(IOSCategoryManager.IS_NEWROW) = True
                    End If
                    dt.Rows.Add(drByObject)
                End If
            End If
        End If
        For Each childNode As TreeNode In treeNodeObject.Nodes
            GetNodeRecursive(childNode, catID, catName, dt)
        Next
    End Sub

    Private Function SetDataByCatManager(ByVal dtCatManager As DataTable) As DataTable
        Try
            For Each row As DataRow In dtCatManager.Rows
                Dim catId As String = row(IOSCategoryManager.CATEGORY_ID).ToString()
                Dim venderId As String = row(IOSCategoryManager.VENDER_ID).ToString()
                Dim objectId As String = row(IOSCategoryManager.OBJECT_ID).ToString()
                Dim objectName As String = row(IOSCategoryManager.OBJECT_NAME).ToString()
                Dim objectType As String = row(IOSCategoryManager.OBJECT_TYPE).ToString()
                Dim isUpdated As Boolean = Convert.ToBoolean(row(IOSCategoryManager.IS_UPDATED))
                Dim isNew As Boolean = Convert.ToBoolean(row(IOSCategoryManager.IS_NEWROW))
                If (isNew) Then
                    Dim cmdText As String = "INSERT INTO IOS_Parameters_CategoriesObjects ([VendorID],[ObjectID],[ObjectName],[ObjectType],[CategoryID]) VALUES ('" & venderId & "','" & objectId & "','" & objectName & "','" & objectType & "','" & catId & "')"
                    IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connStrIOSServer, cmdText)
                    row(IOSCategoryManager.IS_NEWROW) = False
                ElseIf (isUpdated) Then
                    Dim cmdText As String = "Update IOS_Parameters_CategoriesObjects set CategoryID=" & catId & " where VendorID='" & venderId & "' and ObjectID='" & objectId & "'"
                    IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connStrIOSServer, cmdText)
                    row(IOSCategoryManager.IS_UPDATED) = False
                End If
            Next
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message)
        Finally
        End Try
        Return Nothing
    End Function

    Private Function SetScheduleByObject(ByVal dtScheduleObject As DataTable) As DataTable
        Dim schid As Integer = 0
        Dim connection As New Odbc.OdbcConnection(connStrIOSServer)
        connection.ConnectionTimeout = 5
        connection.Open()
        Dim trans As Odbc.OdbcTransaction = connection.BeginTransaction()
        Try
            Dim cmdText As String
            Dim cmd As New Odbc.OdbcCommand()

            For Each row As DataRow In dtScheduleObject.Rows
                Dim catId As String = row(IOSCategoryManager.CATEGORY_ID).ToString()
                Dim venderId As String = row(IOSCategoryManager.VENDER_ID).ToString()
                Dim objectId As String = row(IOSCategoryManager.OBJECT_ID).ToString()
                Dim objectName As String = row(IOSCategoryManager.OBJECT_NAME).ToString()
                Dim objectType As String = row(IOSCategoryManager.OBJECT_TYPE).ToString()
                Dim isUpdated As Boolean = Convert.ToBoolean(row(IOSCategoryManager.IS_UPDATED))
                Dim isNew As Boolean = Convert.ToBoolean(row(IOSCategoryManager.IS_NEWROW))
                If (isUpdated) Then
                    cmdText = "Update IOS_Parameters_CategoriesObjects set CategoryID=" & catId & " where VendorID='" & venderId & "' and ObjectID='" & objectId & "'"
                    cmd = New Odbc.OdbcCommand(cmdText, connection, trans)
                    cmd.ExecuteScalar()
                    row(IOSCategoryManager.IS_UPDATED) = False
                Else
                    cmdText = "INSERT INTO IOS_Parameters_CategoriesObjects ([VendorID],[ObjectID],[ObjectName],[ObjectType],[CategoryID]) VALUES (?,?,?,?,?)"
                    cmd = New Odbc.OdbcCommand(cmdText, connection, trans)
                    cmd.Parameters.Add(New Odbc.OdbcParameter("@venid", venderId))
                    cmd.Parameters.Add(New Odbc.OdbcParameter("@objid", objectId))
                    cmd.Parameters.Add(New Odbc.OdbcParameter("@objName", objectName))
                    cmd.Parameters.Add(New Odbc.OdbcParameter("@objType", objectType))
                    cmd.Parameters.Add(New Odbc.OdbcParameter("@catid", catId))
                    cmd.ExecuteScalar()
                    row(IOSCategoryManager.IS_NEWROW) = False
                End If
            Next
            trans.Commit()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message)
            trans.Rollback()
        Finally
            connection.Close()
        End Try
        Return Nothing
    End Function

    Private Function SetScheduleCatManager(ByVal scheduleStartDate As Date, ByVal scheduleEndDate? As Date, ByVal scheduleType As Integer, ByVal dtScheduleObject As DataTable) As DataTable
        Dim schid As Integer = 0
        Dim connection As New Odbc.OdbcConnection(connStrIOSServer)
        connection.ConnectionTimeout = 5
        connection.Open()
        Dim trans As Odbc.OdbcTransaction = connection.BeginTransaction()
        Try
            Dim cmdText As String
            Dim cmd As New Odbc.OdbcCommand()
            If (scheduleEndDate = "#1/1/1900#") Then
                cmdText = "insert into IOS_Parameters_Scheduled (ScheduleStartTime,Executed,Owner,ScheduleType) values (?,0,?,?); Select Scope_Identity();"
                cmd = New Odbc.OdbcCommand(cmdText, connection, trans)
                cmd.Parameters.Add(New Odbc.OdbcParameter("@schStartTime", scheduleStartDate))
                cmd.Parameters.Add(New Odbc.OdbcParameter("@vName", System.Environment.UserName))
                cmd.Parameters.Add(New Odbc.OdbcParameter("@schedType", scheduleType))

                schid = cmd.ExecuteScalar()
            Else
                Dim cmdOldDataText As String = "insert into IOS_Parameters_Scheduled (ScheduleStartTime,Executed,Owner,ScheduleType) values (?,0,?,?);  Select Scope_Identity();"
                cmd = New Odbc.OdbcCommand(cmdOldDataText, connection, trans)
                cmd.Parameters.Add(New Odbc.OdbcParameter("@schStartTime", scheduleEndDate))
                cmd.Parameters.Add(New Odbc.OdbcParameter("@vName", System.Environment.UserName))
                cmd.Parameters.Add(New Odbc.OdbcParameter("@schedType", scheduleType))
                schid = cmd.ExecuteScalar()
                prevScheduleObjectDatasave(dtScheduleObject, schid)

                cmdText = "insert into IOS_Parameters_Scheduled (ScheduleEndTime,ScheduleStartTime,Executed,Owner,ScheduleType) values (?,?,0,?,?); Select Scope_Identity();"
                cmd = New Odbc.OdbcCommand(cmdText, connection, trans)
                cmd.Parameters.Add(New Odbc.OdbcParameter("@schEndTime", scheduleEndDate))
                cmd.Parameters.Add(New Odbc.OdbcParameter("@schStartTime", scheduleStartDate))
                cmd.Parameters.Add(New Odbc.OdbcParameter("@vName", System.Environment.UserName))
                cmd.Parameters.Add(New Odbc.OdbcParameter("@schedType", scheduleType))
                schid = cmd.ExecuteScalar()

            End If

            For Each Item As DataRow In dtScheduleObject.Rows
                Dim venderId As String = Item(IOSCategoryManager.VENDER_ID)
                Dim objectId As String = Item(IOSCategoryManager.OBJECT_ID)
                Dim objectName As String = Item(IOSCategoryManager.OBJECT_NAME)
                Dim objectType As String = Item(IOSCategoryManager.OBJECT_TYPE)
                Dim catId As String = Item(IOSCategoryManager.CATEGORY_ID)
                cmdText = "INSERT INTO [dbo].[IOS_Parameters_CategoriesObject_Scheduled]([ScheduleID],[VenderID],[Objectid],[ObjectName],[ObjectType],[CategoryID]) VALUES(?,?,?,?,?,?)"
                cmd = New Odbc.OdbcCommand(cmdText, connection, trans)
                cmd.Parameters.Add(New Odbc.OdbcParameter("@schid", schid))
                cmd.Parameters.Add(New Odbc.OdbcParameter("@venid", venderId))
                cmd.Parameters.Add(New Odbc.OdbcParameter("@objid", objectId))
                cmd.Parameters.Add(New Odbc.OdbcParameter("@objName", objectName))
                cmd.Parameters.Add(New Odbc.OdbcParameter("@objType", objectType))
                cmd.Parameters.Add(New Odbc.OdbcParameter("@catid", catId))
                cmd.ExecuteNonQuery()
            Next

            If scheduleStartDate <= Now() Then
                Dim cmdSP As String = "EXECUTE IOS_Parameters_ScheduleCategoryWithLock;"
                cmd = New Odbc.OdbcCommand(cmdSP, connection, trans)
                cmd.ExecuteNonQuery()
            End If

            trans.Commit()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message)
            trans.Rollback()

        Finally
            connection.Close()
        End Try
        Return Nothing
    End Function

    Private Sub prevScheduleObjectDatasave(ByVal dtScheduleObject As DataTable, ByVal scheduleID As Integer)
        Dim connection As New Odbc.OdbcConnection(connStrIOSServer)
        connection.ConnectionTimeout = 5
        connection.Open()
        Dim trans As Odbc.OdbcTransaction = connection.BeginTransaction()
        Try
            Dim cmdText As String
            Dim cmd As New Odbc.OdbcCommand()
            For Each rowItem As DataRow In dtScheduleObject.Rows
                Dim isUpdated As Boolean = Convert.ToBoolean(rowItem(IOSCategoryManager.IS_UPDATED))
                If (isUpdated) Then
                    Dim venderId As String = rowItem(IOSCategoryManager.VENDER_ID)
                    Dim objectId As String = rowItem(IOSCategoryManager.OBJECT_ID)
                    Dim objectName As String = rowItem(IOSCategoryManager.OBJECT_NAME)
                    Dim objectType As String = rowItem(IOSCategoryManager.OBJECT_TYPE)
                    Dim dtOldCatId As DataTable = objectExitOrNot(objectId, objectName, objectType)
                    Dim catId As String = dtOldCatId.Rows(0)(IOSCategoryManager.CATEGORY_ID) 'rowItem(IOSCategoryManager.CATEGORY_ID)
                    cmdText = "INSERT INTO [dbo].[IOS_Parameters_CategoriesObject_Scheduled]([ScheduleID],[VenderID],[Objectid],[ObjectName],[ObjectType],[CategoryID]) VALUES(?,?,?,?,?,?)"
                    cmd = New Odbc.OdbcCommand(cmdText, connection, trans)
                    cmd.Parameters.Add(New Odbc.OdbcParameter("@schid", scheduleID))
                    cmd.Parameters.Add(New Odbc.OdbcParameter("@venid", venderId))
                    cmd.Parameters.Add(New Odbc.OdbcParameter("@objid", objectId))
                    cmd.Parameters.Add(New Odbc.OdbcParameter("@objName", objectName))
                    cmd.Parameters.Add(New Odbc.OdbcParameter("@objType", objectType))
                    cmd.Parameters.Add(New Odbc.OdbcParameter("@catid", catId))
                    cmd.ExecuteNonQuery()
                End If
            Next
            trans.Commit()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message)
            trans.Rollback()
        Finally
            connection.Close()
        End Try

    End Sub

    Private Sub gcCategoryManager_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles gcCategoryManager.DragDrop
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            lblMessage.Text = ""
            Dim dscrean As New System.Drawing.Point(e.X, e.Y)
            Dim catID As Integer = 0
            Dim catName As String = Nothing
            Dim isScheduleExit As Integer = 0
            Dim dcclient As System.Drawing.Point = gcCategoryManager.PointToClient(dscrean)

            If (IsTreeDragOrList) Then
                Dim node As System.Windows.Forms.TreeNode = e.Data.GetData("System.Windows.Forms.TreeNode")
                Dim ObjectId As String = node.Tag
                Dim ObjectName As String = node.Text
                Dim objectType As String = node.ImageKey
                If (objectType.ToLower.Trim = cmbTargetObject.SelectedItem.ToString.ToLower.Trim) Then

                    Dim selectedRows() As DataRow = dtCategoryManager.Select(String.Format("ObjectID='{0}' AND ObjectName='{1}' AND ObjectType='{2}'", ObjectId, ObjectName, objectType))
                    If Not (selectedRows.Length > 0 And ObjectId.Length > 0) Then
                        Dim dtExitOrNot As DataTable = objectExitOrNot(ObjectId, ObjectName, objectType)
                        If (dtExitOrNot.Rows.Count > 0) Then
                            catID = Convert.ToInt32(dtExitOrNot.Rows(0)(IOSCategoryManager.CATEGORY_ID))
                            catName = dtExitOrNot.Rows(0)(IOSCategoryManager.CATEGORY_NAME)
                        End If
                        isScheduleExit = ScheduleIsOrNot(ObjectId, ObjectName, objectType)
                        Dim dr As DataRow = dtCategoryManager.NewRow()
                        dr(IOSCategoryManager.VENDER_ID) = cmbVendor.SelectedItem.ToString
                        dr(IOSCategoryManager.OBJECT_ID) = ObjectId
                        dr(IOSCategoryManager.OBJECT_NAME) = ObjectName
                        dr(IOSCategoryManager.OBJECT_TYPE) = objectType
                        dr(IOSCategoryManager.CATEGORY_ID) = catID
                        dr(IOSCategoryManager.CATEGORY_NAME) = catName
                        dr("HasSchedule") = IIf(isScheduleExit > 0, "Yes", "No")
                        If (dtExitOrNot.Rows.Count > 0) Then
                            dr(IOSCategoryManager.IS_NEWROW) = False
                        Else
                            dr(IOSCategoryManager.IS_NEWROW) = True
                        End If
                        dtCategoryManager.Rows.Add(dr)
                        gcCategoryManager.DataSource = dtCategoryManager
                        HideObjectID()
                        SetAutoFiltersOnGrid(gcCategoryManager, gvCategoryManager, True)
                        gvCategoryManager.OptionsView.RowAutoHeight = False
                        gcCategoryManager.Refresh()
                    Else
                        lblMessage.Text = "Object Already in grid"
                        lblMessage.ForeColor = Color.Red
                    End If
                End If
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cmsCategorManager_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmsCategorManager.Opening
        e.Cancel = Not IsToShowCategoryManagerContextMeunStrip
        IsToShowCategoryManagerContextMeunStrip = False
    End Sub

    Private Sub btnClearGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearGrid.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            lblMessage.Text = ""
            If (dtCategoryManager IsNot Nothing) Then
                dtCategoryManager.Rows.Clear()
            End If
            gcCategoryManager.DataSource = Nothing
            gvCategoryManager.Columns.Clear()
            SetAutoFiltersOnGrid(gcCategoryManager, gvCategoryManager, True)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub vbtnGetObjects_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetObjects.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            lblMessage.Text = ""
            Dim dtCategory As DataTable
            Dim categoryId As String = "0"
            If (TryCast(cmbCategory.SelectedItem, clsComboBoxItem).Value = Nothing) Then
                categoryId = "0"
            Else
                categoryId = TryCast(cmbCategory.SelectedItem, clsComboBoxItem).Value
            End If
            Dim sqlCategeoryCommand As String = "select pco.VendorID,pco.ObjectID,pco.ObjectName,pco.ObjectType,pco.CategoryID,pc.CategoryName,(select case when COUNT(VenderID) > 0 then 'Yes' else 'No' end from IOS_Parameters_CategoriesObject_Scheduled where VendorID='" & cmbVendor.SelectedItem.ToString & "' and ObjectID= pco.ObjectID) as HasSchedule from dbo.IOS_Parameters_CategoriesObjects pco left outer join dbo.IOS_Parameters_Categories pc on pco.CategoryID= pc.CategoryID where pco.[VendorID]='" & cmbVendor.SelectedItem.ToString & "' and pco.[ObjectType]='" & cmbTargetObject.SelectedItem.ToString & "' and pco.CategoryID='" & categoryId & "'"
            dtCategory = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, sqlCategeoryCommand)
            If (dtCategory.Rows.Count > 0) Then
                gcCategoryManager.SuspendLayout()
                gcCategoryManager.Refresh()
                Dim IsNewRow As New DataColumn(IOSCategoryManager.IS_NEWROW, Type.GetType("System.Boolean")) ''
                IsNewRow.DefaultValue = False
                dtCategory.Columns.Add(IsNewRow)
                Dim isUpdated As New DataColumn(IOSCategoryManager.IS_UPDATED, Type.GetType("System.Boolean"))
                isUpdated.DefaultValue = True
                dtCategory.Columns.Add(isUpdated)

                lblMessage.Text = dtCategory.Rows.Count & " Object Found."
                lblMessage.ForeColor = Color.DarkBlue
                If Not dtCategoryManager Is Nothing Then
                    dtCategoryManager.Rows.Clear()
                End If

                dtCategoryManager = dtCategory.Copy()
                gcCategoryManager.DataSource = Nothing
                gcCategoryManager.DataSource = dtCategoryManager
            Else
                lblMessage.Text = "Sorry ! No Object Found."
                lblMessage.ForeColor = Color.Red
                If Not dtCategoryManager Is Nothing Then
                    dtCategoryManager.Rows.Clear()
                End If
                dtCategoryManager = dtCategory.Copy()
                gcCategoryManager.DataSource = Nothing
                gcCategoryManager.DataSource = dtCategoryManager

            End If
            SetAutoFiltersOnGrid(gcCategoryManager, gvCategoryManager, True)
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub vbtnShowCellType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowCellType.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            dtSchedule = Nothing
            Me.Cursor = Cursors.WaitCursor
            btnClearGrid_Click(sender, e)
            bindCategoryGridView(dtCategoryManager)
            Application.DoEvents()
            Dim nodes As TreeNodeCollection = tvObjectTreeStats.Nodes
            If nodes.Count > 0 Then
                SetObjectData()
                For Each selectedNode As TreeNode In nodes
                    GetObjectByTree(selectedNode)
                Next
                gcCategoryManager.SuspendLayout()
                gcCategoryManager.DataSource = Nothing
                gcCategoryManager.DataSource = dtCategoryManager
                HideObjectID()
                SetAutoFiltersOnGrid(gcCategoryManager, gvCategoryManager, True)
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "Category Scheduler"

    Sub ManageCategorySchedulorOnVenderChange()
        lblMessage.Text = ""
        If Not (IsTabChangedUsingCotextMenuClick) Then
            Dim cmdText As String = "select Executed,ScheduleID,ScheduleStartTime,ScheduleEndTime,Owner from dbo.IOS_Parameters_Scheduled" '' where Owner='" & System.Environment.UserName & "'
            Dim data As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, cmdText)
            gcCategorySchedulor.SuspendLayout()
            gcCategorySchedulor.DataSource = data
            rowIndex.Clear()
            If (gvCategorySchedulor.RowCount > 0) Then
                gvCategorySchedulor.SelectRow(0)
                For iCnt As Integer = 0 To gvCategorySchedulor.RowCount - 1
                    If gvCategorySchedulor.GetRowCellValue(iCnt, gvCategorySchedulor.Columns(0)) = True Then
                        rowIndex.Add(New KeyValuePair(Of Integer, Color)(iCnt, Color.Green))
                    Else
                        rowIndex.Add(New KeyValuePair(Of Integer, Color)(iCnt, Color.Red))
                    End If
                Next
                SetAutoFiltersOnGrid(gcCategorySchedulor, gvCategorySchedulor)
            End If
            gvCategorySchedulor.LayoutChanged()
        End If
    End Sub

    Private Sub gvCategorySchedulor_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs)
        Try
            If rowIndex.Count > 0 Then
                If rowIndex.Exists(Function(x) x.Key = e.RowHandle) Then
                    e.Appearance.BackColor = rowIndex.Find(Function(x) x.Key = e.RowHandle).Value
                    e.Appearance.BackColor2 = Color.White
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvCategorySchedulor_SelectionChanged(ByVal sender As System.Object, ByVal args As DevExpress.Data.SelectionChangedEventArgs)
        Dim flagSelect As Boolean = True
        Dim dtSelect As DataTable
        Dim rowCounter As Integer = 0
        Dim rowIndex As Integer = 0
        For iCnt As Integer = 0 To gvCategorySchedulor.RowCount - 1
            If (flag) Then
                gcScheduleData.SuspendLayout()
                Dim schid As Double = Convert.ToInt64(gvCategorySchedulor.GetRowCellValue(iCnt, gvCategorySchedulor.Columns.Item(1)))
                Dim cmdText = "select pcs.ID, pcs.VenderID as vendorID,pcs.ObjectName,pcs.ObjectType,pc.CategoryName from IOS_Parameters_CategoriesObject_Scheduled pcs left outer join dbo.IOS_Parameters_Categories pc on pc.CategoryID = pcs.CategoryID where pcs.ScheduleID ='" & gvCategorySchedulor.GetRowCellValue(iCnt, gvCategorySchedulor.Columns.Item(1)).ToString & "'"
                dtSelect = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, cmdText)
                gcScheduleData.DataSource = dtSelect

                If (sObjectID IsNot Nothing And sObjectName IsNot Nothing And sObjectType IsNot Nothing) Then
                    For Each drRow As DataRow In dtSelect.Rows
                        If (drRow(IOSCategoryManager.OBJECT_NAME).Equals(sObjectName) And drRow(IOSCategoryManager.OBJECT_TYPE).Equals(sObjectType)) Then
                            rowIndex = rowCounter
                            Exit For
                        End If
                        rowCounter = rowCounter + 1
                    Next
                End If
                flag = False
                If (gvScheduleData.RowCount > 0) Then
                    gvScheduleData.SelectRow(iCnt)
                End If
            Else
                flag = True
            End If
        Next
        SetAutoFiltersOnGrid(gcScheduleData, gvScheduleData)
    End Sub

    Private Sub ToolStripMenuItemDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemDelete.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim flag As Boolean = True
            Dim row As DevExpress.XtraGrid.Views.Grid.GridRow = Nothing
            For iCnt As Integer = 0 To gvScheduleData.RowCount - 1
                If (flag) Then
                    If (gvCategorySchedulor.RowCount > 0 AndAlso gvCategorySchedulor.GetRowCellValue(gvCategorySchedulor.GetSelectedRows()(0), gvCategorySchedulor.Columns.Last()) = Environment.UserName) Then
                        Dim cmdText = "DELETE FROM IOS_Parameters_CategoriesObject_Scheduled WHERE id='" & gvCategorySchedulor.GetRowCellValue(iCnt, gvCategorySchedulor.Columns.First()).ToString & "'"
                        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connStrIOSServer, cmdText)
                        row = gvCategorySchedulor.GetRow(iCnt)
                        flag = False
                    End If
                End If
            Next
            If (row IsNot Nothing) Then
                gcScheduleData.SuspendLayout()
                gvScheduleData.DeleteRow(row.RowHandle)
                gcScheduleData.Refresh()
                gcScheduleData.ResumeLayout()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub gcScheduleData_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles gcScheduleData.MouseDown
        IsToShowCategoryManagerContextMeunStrip = False
        If (e.Button = MouseButtons.Right) Then
            Dim cell As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = gvScheduleData.CalcHitInfo(New System.Drawing.Point(e.X, e.Y))
            If (cell IsNot Nothing) Then
                Dim rowHandle As Integer = cell.RowHandle
                If (rowHandle >= 0) Then
                    gvScheduleData.SelectRow(rowHandle)
                    gvScheduleData.SelectCell(rowHandle, cell.Column)
                    gcScheduleData.ContextMenuStrip.Show(Me.gcScheduleData, New System.Drawing.Point(e.X, e.Y))
                    IsToShowCategoryManagerContextMeunStrip = True
                End If
            Else
                gcScheduleData.ContextMenuStrip.Hide()
            End If
        End If
    End Sub

    Private Sub gvScheduleData_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs)
        If (e.RowHandle > 0 AndAlso (e.Column.ColumnHandle = 2 Or e.Column.ColumnHandle = 3)) Then
            Dim val As String = e.Value.ToString
            Dim colName As String = e.Column.FieldName
            Dim id As String = gvScheduleData.GetRowCellValue(e.RowHandle, gvCategorySchedulor.Columns(0)).ToString
            Dim cmdText As String = "Update IOS_Parameters_CategoriesObject_Scheduled SET " & colName & "='" & val & "' WHERE id='" & id & "'"
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connStrIOSServer, cmdText)
        End If
    End Sub

    Private Sub gvScheduleData_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gvScheduleData.RowCellClick
        If (e.RowHandle > -1 AndAlso (e.Column.ColumnHandle = 2 Or e.Column.ColumnHandle = 3 Or e.Column.ColumnHandle = 1)) Then
            If (gvCategorySchedulor.SelectedRowsCount > 0 AndAlso gvCategorySchedulor.GetRowCellValue(0, gvCategorySchedulor.Columns.Last) = Environment.UserName) Then
                gvCategorySchedulor.OptionsEditForm.ShowOnDoubleClick = DevExpress.Utils.DefaultBoolean.True
            End If
        End If
    End Sub

    Private Sub gvScheduleData_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs)
        If isDoubleClickOrkeyPress = True Then
            gvScheduleData.OptionsBehavior.Editable = True
            e.Cancel = False
        Else
            gvScheduleData.OptionsBehavior.Editable = False
            e.Cancel = True
        End If
    End Sub

    Private Sub gvScheduleData_DoubleClick(sender As Object, e As EventArgs)
        isDoubleClickOrkeyPress = True
    End Sub

    Private Sub gvScheduleData_KeyPress(sender As Object, e As KeyPressEventArgs)
        isDoubleClickOrkeyPress = True
    End Sub

    Private Function SetSchdule(ByVal scheduleStartDate As Date, ByVal scheduleEndDate? As Date, ByVal isWithSchedule As Boolean) As DataTable
        Dim scheduledDate As DateTime = Nothing
        Dim data As New DataTable()
        If (isWithSchedule) Then
            data.Columns.Add("VenderId")
            data.Columns.Add(IOSCategoryManager.OBJECT_ID)
            data.Columns.Add(IOSCategoryManager.CATEGORY_ID)
            data.Columns.Add(IOSCategoryManager.OBJECT_NAME)
            data.Columns.Add("CategoryType")
        End If
        For Each row As DataRow In dtCategoryManager.Rows
            Dim catId As String = row(IOSCategoryManager.CATEGORY_ID).ToString()
            Dim venderId As String = row(IOSCategoryManager.VENDER_ID).ToString()
            Dim objectId As String = row(IOSCategoryManager.OBJECT_ID).ToString()
            Dim objectName As String = row(IOSCategoryManager.OBJECT_NAME).ToString()
            Dim objectType As String = row(IOSCategoryManager.OBJECT_TYPE).ToString()
            Dim isUpdated As Boolean = Convert.ToBoolean(row(IOSCategoryManager.IS_UPDATED))
            Dim isNew As Boolean = Convert.ToBoolean(row(IOSCategoryManager.IS_NEWROW))
            If (isWithSchedule) Then
                data.Rows.Add(venderId, objectId, catId, objectName, objectType)
            End If
            If (isNew) Then
                Dim cmdText As String = "INSERT INTO IOS_Parameters_CategoriesObjects ([VendorID],[ObjectID],[ObjectName],[ObjectType],[CategoryID]) VALUES ('" & venderId & "','" & objectId & "','" & objectName & "','" & objectType & "','" & catId & "')"
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connStrIOSServer, cmdText)
                row(IOSCategoryManager.IS_NEWROW) = False
            ElseIf (isUpdated) Then
                Dim cmdText As String = "Update IOS_Parameters_CategoriesObjects set CategoryID=" & catId & " where VendorID='" & venderId & "' and ObjectID='" & objectId & "'"
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connStrIOSServer, cmdText)
                row(IOSCategoryManager.IS_UPDATED) = False
            End If
        Next
        If (isWithSchedule) Then
            Dim schid As Integer = 0
            Dim connection As New Odbc.OdbcConnection(connStrIOSServer)
            connection.ConnectionTimeout = 5
            connection.Open()
            Dim trans As Odbc.OdbcTransaction = connection.BeginTransaction()
            Try
                Dim cmdText As String
                Dim cmd As New Odbc.OdbcCommand()
                If (scheduleEndDate = "#1/1/1900#") Then
                    cmdText = "insert into IOS_Parameters_Scheduled (ScheduleStartTime,Executed,Owner) values (?,0,?); Select Scope_Identity();"
                    cmd = New Odbc.OdbcCommand(cmdText, connection, trans)
                    cmd.Parameters.Add(New Odbc.OdbcParameter("@schStartTime", scheduleStartDate))
                    cmd.Parameters.Add(New Odbc.OdbcParameter("@vName", System.Environment.UserName))
                    schid = cmd.ExecuteScalar()
                Else
                    Dim cmdOldDataText As String = "insert into IOS_Parameters_Scheduled (ScheduleStartTime,Executed,Owner) values ('" & scheduleEndDate & "',0,'" & System.Environment.UserName & "')"
                    IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connStrIOSServer, cmdOldDataText)
                    cmdText = "insert into IOS_Parameters_Scheduled (ScheduleEndTime,ScheduleStartTime,Executed,Owner) values (?,?,0,?); Select Scope_Identity();"
                    cmd = New Odbc.OdbcCommand(cmdText, connection, trans)
                    cmd.Parameters.Add(New Odbc.OdbcParameter("@schEndTime", scheduleEndDate))
                    cmd.Parameters.Add(New Odbc.OdbcParameter("@schStartTime", scheduleStartDate))
                    cmd.Parameters.Add(New Odbc.OdbcParameter("@vName", System.Environment.UserName))
                    schid = cmd.ExecuteScalar()
                End If
                For Each Item As DataRow In data.Rows
                    Dim venderId As String = Item(0)
                    Dim objectId As String = Item(1)
                    Dim catId As String = Item(2)
                    Dim objectName As String = Item(3)
                    Dim objectType As String = Item(4)
                    cmdText = "INSERT INTO [dbo].[IOS_Parameters_CategoriesObject_Scheduled]([ScheduleID],[VenderID],[Objectid],[ObjectName],[ObjectType],[CategoryID]) VALUES(?,?,?,?,?,?)"
                    cmd = New Odbc.OdbcCommand(cmdText, connection, trans)
                    cmd.Parameters.Add(New Odbc.OdbcParameter("@schid", schid))
                    cmd.Parameters.Add(New Odbc.OdbcParameter("@venid", venderId))
                    cmd.Parameters.Add(New Odbc.OdbcParameter("@objid", objectId))
                    cmd.Parameters.Add(New Odbc.OdbcParameter("@objName", objectName))
                    cmd.Parameters.Add(New Odbc.OdbcParameter("@objType", objectType))
                    cmd.Parameters.Add(New Odbc.OdbcParameter("@catid", catId))
                    cmd.ExecuteNonQuery()
                Next
                trans.Commit()
            Catch ex As Exception
                XtraMessageBox.Show(ex.Message)
                trans.Rollback()
            Finally
                connection.Close()
            End Try
        End If
        Return Nothing
    End Function

    Private Sub gvCategorySchedulor_MouseDown(sender As Object, e As MouseEventArgs) Handles gcCategorySchedulor.MouseDown
        IsToShowCategoryManagerContextMeunStrip = False
        If (e.Button = MouseButtons.Right) Then
            Dim cell As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = gvCategorySchedulor.CalcHitInfo(New System.Drawing.Point(e.X, e.Y))
            If (cell IsNot Nothing) Then
                Dim rowHandle As Integer = cell.RowHandle
                If (rowHandle >= 0) Then
                    gvCategorySchedulor.SelectRow(rowHandle)
                    If (cell.Column.Caption.Equals("ScheduleStartTime")) Then
                        gcCategorySchedulor.ContextMenuStrip.Items(1).Enabled = True
                    Else
                        gcCategorySchedulor.ContextMenuStrip.Items(1).Enabled = False
                    End If
                    gcCategorySchedulor.ContextMenuStrip.Show(Me.gcCategorySchedulor, New System.Drawing.Point(e.X, e.Y))
                    IsToShowCategoryManagerContextMeunStrip = True
                End If
            Else
                gcCategorySchedulor.ContextMenuStrip.Hide()
            End If
        End If
    End Sub

    Private Sub cmsdgvScheduleData_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmsdgvScheduleData.Opening
        e.Cancel = Not IsToShowCategoryManagerContextMeunStrip
        IsToShowCategoryManagerContextMeunStrip = False
    End Sub

    Private Sub cmsSchedule_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmsSchedule.Opening
        e.Cancel = Not IsToShowCategoryManagerContextMeunStrip
        IsToShowCategoryManagerContextMeunStrip = False
    End Sub

#End Region

#Region "Left Region select controls AND tab controls"

    Sub ClearComboBox(ByRef control As DevExpress.XtraEditors.ComboBoxEdit, ByVal firstItem As String)
        control.SuspendLayout()
        control.Properties.Items.Clear()
        control.Properties.Items.Insert(0, firstItem)
        control.SelectedIndex = 0
        control.Refresh()
        control.ResumeLayout()
    End Sub

    Private Sub cmbTechnology_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTechnology.SelectedIndexChanged
        Try
            IsFirstTime = False
            lblMessage.Text = ""
            If Not (cmbTechnology.SelectedIndex = 0) Then
                BindVendor()
            Else
                ClearComboBox(cmbVendor, "Select Vendor")
                ClearComboBox(cmbTargetObject, "Object Type")
                ClearComboBox(cmbCategory, "No Category")
                cmbCategory.Enabled = False
                btnGetObjects.Enabled = False
                If (dtCategoryManager IsNot Nothing) Then
                    dtCategoryManager.Rows.Clear()
                End If
                gcCategoryManager.SuspendLayout()
                gcCategoryManager.DataSource = Nothing
                gcCategoryManager.Refresh()
                gcCategoryManager.ResumeLayout()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmbVendor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVendor.SelectedIndexChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            IsFirstTime = False
            BindCategory()
            BindObjectType()
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub vcmbTargetObject_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTargetObject.SelectedIndexChanged
        Try
            dtSchedule = Nothing
            tvObjectTreeStats.SuspendLayout()
            tvObjectTreeStats.Nodes.Clear()
            tvObjectTreeStats.Refresh()
            tvObjectTreeStats.ResumeLayout()
            If (cmbTargetObject.SelectedIndex > 0) Then
                If Not cmbTargetObject.SelectedItem Is Nothing Then
                    FillObjectTreeData(tvObjectTreeStats, cmbVendor.SelectedItem.ToString() & " " & cmbTechnology.SelectedItem.ToString(), cmbTargetObject.SelectedItem.ToString)

                    btnShowCellType.Enabled = True
                    btnModifyCellType.Enabled = True
                End If
            Else
                btnShowCellType.Enabled = False
                btnModifyCellType.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub vtabParameters_SelectedPageChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles xtcTabParameters.SelectedPageChanged
        Try
            If (IsPageLoaded) Then
                HideShowLeftControls()
                IsFirstTime = True
                ManageCategoryManaterOnVenderChange()
                ManageCategorySchedulorOnVenderChange()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Sub HideShowLeftControls()
        Dim tag As String = xtcTabParameters.SelectedTabPage.Tag
        Select Case tag
            Case "CS"
                txtSearchOuter.Enabled = False
                cmbTargetObject.Enabled = False
                tvObjectTreeStats.Enabled = False
            Case "CM"
                txtSearchOuter.Enabled = True
                cmbTargetObject.Enabled = True
                tvObjectTreeStats.Enabled = True
        End Select
    End Sub

    Sub BindObjectType()
        cmbTargetObject.SuspendLayout()
        cmbTargetObject.Properties.Items.Clear()
        If (cmbTechnology.SelectedIndex > 0 AndAlso cmbVendor.SelectedIndex > 0) Then
            Dim dtobject As DataTable = Nothing
            If (dt_IOS_ObjectConfig IsNot Nothing) Then
                dtobject = New DataView(dt_IOS_ObjectConfig, "Vendor='" & cmbVendor.SelectedItem.ToString & "' and  Technology='" & cmbTechnology.SelectedItem.ToString & "' and " & "Categorymanager=1", "", DataViewRowState.CurrentRows).ToTable(True, "Object")
            End If
            BindDevExComboBoxWithValueMember(cmbTargetObject, dtobject, "Object", "Object", "Object Type")
            btnGetObjects.Enabled = True
        Else
            ClearComboBox(cmbTargetObject, "Object Type")
        End If
        cmbTargetObject.Refresh()
        cmbTargetObject.ResumeLayout()
    End Sub

    Private Sub lstOfCategories_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvObjectTreeStats.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub TreeViewStats_AfterCheck(sender As System.Object, e As System.Windows.Forms.TreeViewEventArgs) Handles tvObjectTreeStats.AfterCheck
        CheckTreeNodeAndCount(e.Node, 0, Nothing)
    End Sub

    Private Function GetTechnologyName(ByVal tech As String, ByVal vendor As String, ByVal returnObjectColumnsName As String) As String
        Dim rows() As DataRow = dt_IOS_ObjectConfig.Select("Vendor='" & vendor & "' AND Technology='" & tech & "' AND ParamHistory=1")
        If (rows.Count > 0) Then
            Return rows(0)(returnObjectColumnsName).ToString
        End If
        Return ""
    End Function

    Private Sub TreeViewStats_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvObjectTreeStats.MouseDown
        Dim tree As TreeView = TryCast(sender, TreeView)
        If (tree IsNot Nothing) Then
            Dim item As TreeViewHitTestInfo = tree.HitTest(e.Location)
            If item.Node IsNot Nothing Then
                If (e.Button = MouseButtons.Left) Then
                    IsTreeDragOrList = True
                    tree.DoDragDrop(item.Node, DragDropEffects.Copy)
                Else
                    tree.SelectedNode = item.Node
                End If
            End If
        End If
    End Sub

    Private Sub txtSearchOuter_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        txtObjectsearch_KeyDown(tvObjectTreeStats, txtSearchOuter.Text, e)
    End Sub

    Private Sub txtSearchOuter_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchOuter.TextChanged
        txtObjectSearch_TextChanged(tvObjectTreeStats, txtSearchOuter.Text)
    End Sub

    Private Sub BindTechnology()
        If (dtTech Is Nothing) Then
            If (dt_IOS_ObjectConfig IsNot Nothing) Then
                dtTech = New DataView(dt_IOS_ObjectConfig, "TemplateManager=1", "", DataViewRowState.CurrentRows).ToTable(True, "Technology")
            End If
        End If
        If (dtTech.Rows.Count > 0) Then
            cmbTechnology.Properties.Items.Clear()
            BindDevExComboBoxWithValueMember(cmbTechnology, dtTech, "Technology", "Technology", "Select Technology")
        End If
        ClearComboBox(cmbVendor, "Select Vendor")
        ClearComboBox(cmbTargetObject, "Object Type")
    End Sub

    Private Sub BindVendor()
        Dim dtVendorPH As DataTable = Nothing
        If (dt_IOS_ObjectConfig IsNot Nothing) Then
            dtVendorPH = New DataView(dt_IOS_ObjectConfig, "Categorymanager=1", "", DataViewRowState.CurrentRows).ToTable(True, "Vendor")
        End If
        cmbVendor.Properties.Items.Clear()
        If (dtVendorPH IsNot Nothing AndAlso dtVendorPH.Rows.Count > 0) Then
            BindDevExComboBoxWithValueMember(cmbVendor, dtVendorPH, "Vendor", "Vendor", "Select Vendor")
        End If
    End Sub

#End Region

    Private Sub ConfigurCMSchedulerForm(ByVal frmName As String)
        Dim form As IOS.Configuration.EntityModel.IOSForm = configMgr.FindFormByName(frmName)
        If Not form Is Nothing Then
            Dim counter As Integer = 0
            ConfigurForm(Me, frmName, counter)

            Dim winCtrl As IOS.Configuration.EntityModel.Control = Nothing
            Dim formControls As List(Of Object) = New List(Of Object) From {
                 cmsdgvScheduleData, ToolStripMenuItemSheduleDelete, ToolStripMenuItemSheduleUpdate, tsmi_Export2XML, tsmi_Export2XML_NSN, tsmi_Export2Clipboard, cm_OT_tsmi_copy, cm_OT_tsmi_paste, cm_OT_tsmi_CopyToTag, cm_OT_tsmi_CheckChilds,
                 tsmi_OT_UnCheck, tsmi_OT_MapCell, tsmi_ReloadTree, tsmi_OT_Exception, tsmi_dgv_SelectAll, tsmi_dgv_CopyClipboardWOHeader, tsmi_dgv_CopyClipboardWithHeader, tsmi_dgv_ExportExcel
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

    Sub BindCategory()
        If (cmbTechnology.SelectedIndex > 0 AndAlso cmbVendor.SelectedIndex > 0) Then
            dtCategoryData = IOS.DataLibrary.clsSQLCommands.GetParameterCategory(connStrIOSServer, cmbVendor.SelectedItem.ToString, cmbTechnology.SelectedItem.ToString)
            If (dtCategoryData.Rows.Count > 0) Then 'change there (dtCategoryData IsNot Nothing) Then
                cmbCategory.Properties.Items.Clear()
                cmbCategory.SuspendLayout()
                BindDevExComboBoxWithValueMember(cmbCategory, dtCategoryData, IOSCategoryManager.CATEGORY_ID, IOSCategoryManager.CATEGORY_NAME)
                cmbCategory.Properties.Items.Insert(0, "No Category")
                cmbCategory.Properties.Items(0).IsChecked = True
                cmbCategory.Enabled = True
            End If
        End If
    End Sub

    Private Sub frmCMScheduler_Load(sender As Object, e As EventArgs) Handles Me.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.DoubleBuffered = True
            Me.SuspendLayout()
            Me.ResumeLayout()
            Me.WindowState = FormWindowState.Normal
            Me.BringToFront()
            Me.StartPosition = FormStartPosition.Manual
            Me.Location = Screen.FromControl(frmMDI).Bounds.Location

            dsVenderData = Nothing
            IsPageLoaded = True
            IsFirstTime = True
            BindTechnology()
            xtcTabParameters.SelectedTabPageIndex = 0
            HideShowLeftControls()
            ConfigurCMSchedulerForm("frmCMScheduler")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_dgv_SelectAll_Click(sender As System.Object, e As System.EventArgs)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim grvTemp As DevExpress.XtraGrid.GridControl = frmMapWindow.GetAttachedGrid(sender)
            Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = grvTemp.Views(0)
            IOS.Library.IOSDevExpressGrid.CopyGridDataToClipBoard(grvTemp, gridView, True, True)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_dgv_CopyClipboardWOHeader_Click(sender As System.Object, e As System.EventArgs) Handles tsmi_dgv_CopyClipboardWOHeader.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim tempGrid As DevExpress.XtraGrid.GridControl = frmMapWindow.GetAttachedGrid(sender)
            Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = tempGrid.Views(0)
            IOS.Library.IOSDevExpressGrid.CopyGridDataToClipBoard(tempGrid, gridView, False, False)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_dgv_CopyClipboardWithHeader_Click(sender As Object, e As EventArgs) Handles tsmi_dgv_CopyClipboardWithHeader.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim tempGrid As DevExpress.XtraGrid.GridControl = frmMapWindow.GetAttachedGrid(sender)
            Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = tempGrid.Views(0)
            IOS.Library.IOSDevExpressGrid.CopyGridDataToClipBoard(tempGrid, gridView, False, True)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_dgv_ExportExcel_Click(sender As System.Object, e As System.EventArgs)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim tempGrid As DevExpress.XtraGrid.GridControl = frmMapWindow.GetAttachedGrid(sender)
            IOS.Library.IOSDevExpressGrid.ExportDataGridToExcel(tempGrid)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_Export2XML_NSN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")

        'Read selected ScheduleID
        Dim scheduleid As Integer = 0
        Try
            'If gvCategorySchedulor.SelectedRowsCount > 0 Then
            '    scheduleid = CInt(gvCategorySchedulor.GetRowCellValue(gvCategorySchedulor.GetSelectedRows()(0), gvCategorySchedulor.Columns.Item(1)).ToString)
            'Else
            '    Exit Sub
            'End If
        Catch
            Exit Sub
        End Try

        Try
            Dim sql As String = "SELECT * FROM qry_IOS_Parameters_CategorySchedule2Parameters WHERE scheduleid = " & scheduleid & " ORDER BY OBJECT_GID "
            Dim dt As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, sql)

            If XML_Parameters_Validation("NSN - RAML2.0", dt) Then
                Dim saveFileDialog1 As New SaveFileDialog()
                saveFileDialog1.Filter = "XML|*.xml"
                saveFileDialog1.Title = "Save an XML File"
                saveFileDialog1.ShowDialog()

                ' If the file name is not an empty string open it for saving.
                If saveFileDialog1.FileName <> "" Then
                    Dim success As Boolean = XML_Parameters_NSN(saveFileDialog1.FileName, dt)
                    If success Then
                        MsgBox("Export Success!")
                    End If
                End If
            Else
                MsgBox("Input Table does not contain all columns: Object_DN, Object_GID, ShortName, DefaultValue", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_Export2Clipboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")

        Dim scheduleid As Integer = 0
        Try
            'If gvCategorySchedulor.SelectedRowsCount > 0 Then
            '    scheduleid = CInt(gvCategorySchedulor.GetRowCellValue(gvCategorySchedulor.GetSelectedRows()(0), gvCategorySchedulor.Columns.Item(1)).ToString)
            'Else
            '    Exit Sub
            'End If
        Catch
            Exit Sub
        End Try

        Dim sql As String = "SELECT * FROM qry_IOS_Parameters_CategorySchedule2Parameters WHERE scheduleid = " & scheduleid & " ORDER BY OBJECT_GID "
        Dim dt As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, sql)
        Try
            Dim sb As New System.Text.StringBuilder
            sb = New System.Text.StringBuilder

            For Each dc As DataColumn In dt.Columns
                sb.Append(dc.ColumnName)
                sb.Append(vbTab)
            Next
            sb.Append(vbCrLf)
            For Each r As DataRow In dt.Rows
                For Each dc As DataColumn In dt.Columns
                    sb.Append(r(dc).ToString)
                    sb.Append(vbTab)
                Next
                sb.Append(vbCrLf)
            Next

            Clipboard.SetText(sb.ToString)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub xtcTabParameters_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles xtcTabParameters.SelectedPageChanged
        Try
            If (IsPageLoaded) Then
                HideShowLeftControls()
                IsFirstTime = True
                ManageCategoryManaterOnVenderChange()
                ManageCategorySchedulorOnVenderChange()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvCategorySchedulor_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles gvCategorySchedulor.RowClick
        'Dim flagSelect As Boolean = True
        'Dim dtSelect As DataTable
        'Dim rowCounter As Integer = 0
        'Dim rowIndex As Integer = 0
        'For Each Item As HierarchyItem In vdgvCategorySchedulor.RowsHierarchy.SelectedItems
        '    If (flag) Then
        '        vdgvScheduleData.SuspendLayout()
        '        Dim schid As Double = Convert.ToInt64(Item.Cells(1).Value)
        '        'Dim cmdText = "select pcs.*,ps.Owner from IOS_Parameters_CategoriesObject_Scheduled pcs  inner join IOS_Parameters_Scheduled ps on pcs.ScheduleID=ps.ScheduleID   where pcs.ScheduleID='" & Item.Cells(1).Value & "'"
        '        Dim cmdText = "select pcs.ID, pcs.VenderID as vendorID,pcs.ObjectName,pcs.ObjectType,pc.CategoryName from IOS_Parameters_CategoriesObject_Scheduled pcs left outer join dbo.IOS_Parameters_Categories pc on pc.CategoryID= pcs.CategoryID where pcs.ScheduleID='" & Item.Cells(1).Value & "'"
        '        dtSelect = DataAccessor.ExecuteDataTable(connStrIOSServer, cmdText)
        '        vdgvScheduleData.DataSource = dtSelect

        '        If (sObjectID IsNot Nothing And sObjectName IsNot Nothing And sObjectType IsNot Nothing) Then
        '            For Each drRow As DataRow In dtSelect.Rows
        '                If (drRow(IOSCategoryManager.OBJECT_NAME).Equals(sObjectName) And drRow(IOSCategoryManager.OBJECT_TYPE).Equals(sObjectType)) Then
        '                    ' vdgvScheduleData.RowsHierarchy.Items(rowCounter).Selected = True
        '                    rowIndex = rowCounter
        '                    Exit For
        '                End If
        '                rowCounter = rowCounter + 1
        '            Next
        '        End If
        '        flag = False
        '        If (vdgvScheduleData.RowsHierarchy.Items.Count > 0) Then
        '            vdgvScheduleData.RowsHierarchy.Items(rowIndex).Selected = True
        '        End If
        '    Else
        '        flag = True
        '    End If

        'Next
        'SetAutoFiltersOnGrid(vdgvScheduleData)
    End Sub

    Private Sub gvScheduleData_RowUpdated(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gvScheduleData.RowUpdated
        'Dim gvEditCell As GridCell = args.Cell
        'If (args.Cell.RowItem.ItemIndex > 0 AndAlso (args.Cell.ColumnItem.ItemIndex = 2 Or args.Cell.ColumnItem.ItemIndex = 3)) Then
        '    Dim val As String = gvEditCell.EditValue
        '    Dim colName As String = gvEditCell.ColumnItem.Caption
        '    Dim id As String = vdgvScheduleData.RowsHierarchy.Items(args.Cell.RowItem.ItemIndex).Cells(0).Value
        '    Dim cmdText As String = "Update IOS_Parameters_CategoriesObject_Scheduled SET " & colName & "='" & val & "' WHERE id='" & id & "'"
        '    DataAccessor.ExecuteNonQuery(connStrIOSServer, cmdText)
        'End If
    End Sub

#Region "Context Menu Code"

    Private Sub cm_ObjectTree_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cm_ObjectTree.Opening
        Dim vendor As String = cmbVendor.SelectedItem.ToString
        Dim tech As String = cmbTechnology.SelectedItem.ToString
        Dim aggr_to As String = cmbTargetObject.SelectedItem.ToString
        Dim countchecked As Integer = 0
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim ExactMatch As Boolean = True
            If aggr_to = "WBTS" Or aggr_to = "BCF" Then
                ExactMatch = False
            Else
                ExactMatch = True
            End If

            'count checked boxes
            countchecked = TreeView_CountCheckedNodes(tvObjectTreeStats.Nodes(0))
            tsmi_OT_Exception.Visible = False

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
                            If Not Treeview_TextSearch(bufferCell(j).Trim, tvObjectTreeStats.Nodes, ExactMatch) Is Nothing Then
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
                tvObjectTreeStats.Cursor = Cursors.Arrow
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try

        'tags
        '----

        'get all tags
        Dim sql As String = Nothing
        Dim connstring As String = Nothing
        Dim ds_tag As DataSet = Nothing
        cm_OT_tsmi_CopyToTag.Enabled = False
        Try
            sql = GetSQL(8601, Nothing)(1)
            connstring = GetSQL(8601, Nothing)(0)
            ds_tag = IOS.DataLibrary.DataAccessorODBC.GetDataSet(connstring, sql)

            For Each drow As DataRow In ds_tag.Tables(0).Rows
                Dim tsmi As ToolStripMenuItem = New ToolStripMenuItem(drow(1).ToString.Trim)

                ''AddHandler tsmi.Click, AddressOf cm_OT_CopyToTag_ItemClick
                cm_OT_tsmi_CopyToTag.DropDownItems.Add(tsmi)

            Next
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            ds_tag.Dispose()
            ds_tag = Nothing
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        'exception list
        If tvObjectTreeStats.Name = "TreeView_Tuning_Objects" And countchecked > 0 Then
            tsmi_OT_Exception.Visible = True
        End If
    End Sub

    Private Sub cm_OT_tsmi_copy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cm_OT_tsmi_copy.Click
        Clipboard.Clear()
        Dim tech As String = cmbTechnology.SelectedItem.ToString
        Dim vendor As String = cmbVendor.SelectedItem.ToString
        Dim aggr_to As String = cmbTargetObject.SelectedItem.ToString
        Try
            Dim copystring As String = TreeView_Checked2String(vendor & " " & tech, aggr_to, "Naked", tvObjectTreeStats, cmbTargetObject)
            copystring = copystring.Replace(",", ControlChars.NewLine)
            If Not copystring Is Nothing Or copystring <> "" Then
                Clipboard.SetText(copystring)
            End If
            copystring = Nothing
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        Finally
            tvObjectTreeStats.Cursor = Cursors.Arrow
        End Try
    End Sub

    Private Sub cm_OT_tsmi_paste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cm_OT_tsmi_paste.Click
        Dim tech As String = cmbTechnology.SelectedItem.ToString
        Dim aggr_to As String = cmbTargetObject.SelectedItem.ToString
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        tvObjectTreeStats.Cursor = Cursors.WaitCursor
        Try
            Dim ExactMatch As Boolean = True
            If aggr_to = "WBTS" Or aggr_to = "BCF" Then
                ExactMatch = False
            Else
                ExactMatch = True
            End If

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
                        Dim tv_result As TreeNode = Treeview_TextSearch(bufferCell(j).Trim, tvObjectTreeStats.Nodes, ExactMatch)
                        If Not tv_result Is Nothing Then
                            tv_result.Checked = True
                        End If
                    Next
                Next
            End If

            tvObjectTreeStats.Cursor = Cursors.Arrow
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            tvObjectTreeStats.Cursor = Cursors.Arrow
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cm_OT_tsmi_CheckChilds_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cm_OT_tsmi_CheckChilds.Click
        Try
            Objecttree_CheckChild(tvObjectTreeStats.SelectedNode)
        Catch
        End Try
    End Sub

    Private Sub tsmi_OT_UnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmi_OT_UnCheck.Click
        TreeView_ClearChecks(tvObjectTreeStats.Nodes(0))
    End Sub

    Private Sub tsmi_OT_MapCell_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmi_OT_MapCell.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim tech As String = Me.GetTechnologyName(cmbTechnology.SelectedItem.ToString, cmbVendor.SelectedItem.ToString, "Tech")

            Select Case tech
                Case cmbVendor.SelectedItem.ToString.Trim & " " & "3G"
                    If cmbTargetObject.SelectedItem.ToString = "WCEL" Or cmbTargetObject.SelectedItem.ToString = "TAGS" Then
                        frmMapWindow.Cells_SearchAndDisplay(TreeView_Checked2String(tech, "WCEL", "Naked", tvObjectTreeStats, cmbTargetObject), "3G", Nothing, True)
                    ElseIf cmbTargetObject.SelectedItem.ToString = "WBTS" Then
                        frmMapWindow.Cells_SearchAndDisplay(TreeView_Checked2String(tech, "WBTS", "Naked", tvObjectTreeStats, cmbTargetObject), "3G", Nothing, True)
                    End If
                Case cmbVendor.SelectedItem.ToString.Trim & " " & "2G"
                    If cmbTargetObject.SelectedItem.ToString = "CELL" Or cmbTargetObject.SelectedItem.ToString = "BTS" Or cmbTargetObject.SelectedItem.ToString = "TAGS" Then
                        frmMapWindow.Cells_SearchAndDisplay(TreeView_Checked2String(tech, "CELL", "Naked", tvObjectTreeStats, cmbTargetObject), "2G", Nothing, True)
                    ElseIf cmbTargetObject.SelectedItem.ToString = "BCF" Or cmbTargetObject.SelectedItem.ToString = "SITE" Then
                        frmMapWindow.Cells_SearchAndDisplay(TreeView_Checked2String(tech, "BCF", "Naked", tvObjectTreeStats, cmbTargetObject), "2G", Nothing, True)
                    End If
            End Select
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_ReloadTree_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmi_ReloadTree.Click
        Dim tech As String = Nothing
        Dim aggr_to As String = Nothing
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Me.Cursor = Cursors.WaitCursor
        tvObjectTreeStats.Nodes.Clear()
        tech = cmbTechnology.SelectedItem.ToString
        Try
            Select Case True
                Case tech.Contains("3G")
                    tech = "3G"
                    dsTree3G_wcel.Dispose()
                    dsTree3G_wbts.Dispose()
                    dsTree3G_rnc.Dispose()
                    Application.DoEvents()
                    IOS_ObjectConfig_Load(tech, True)
                    vcmbTargetObject_SelectedIndexChanged(Nothing, Nothing)
                Case tech.Contains("2G")
                    tech = "2G"
                    dsTree2G_bcf.Dispose()
                    dsTree2G_bsc.Dispose()
                    dsTree2G_cel.Dispose()
                    Application.DoEvents()
                    IOS_ObjectConfig_Load(tech, True)
                    vcmbTargetObject_SelectedIndexChanged(Nothing, Nothing)
            End Select
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            Me.Cursor = Cursors.Arrow
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try

        Try
            frmMapWindow.Calendar_GetNetworks_Fill()
            frmMapWindow.Calendar_GetNetwork_Fill_From_DB()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Me.Cursor = Cursors.Arrow
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        Me.Cursor = Cursors.Arrow
    End Sub

#End Region
End Class