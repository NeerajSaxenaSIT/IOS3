Imports System.Runtime.CompilerServices
Imports System.Text
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Columns
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraTreeList.Nodes.Operations

Public Module TreeListExtender

    Private Function GetObjectLevelByID(ByVal id As Integer) As Integer
        Dim level As Integer = 0
        Dim dr() As DataRow = dt_IOS_ObjectConfig.Select("ID=" & Chr(39) & id & Chr(39))
        If (dr.Count > 0) Then
            If dr(0)("ID").ToString <> 0 Then
                level = 1 + GetObjectLevelByID(dr(0)("ParentID").ToString)
            End If
        End If
        Return level
    End Function

    Public Function GetNodeLevelByObjectType(ByVal tech As String, ByVal objectType As String, Optional vendor As String = "") As Integer
        If objectType = "PLMN" Then Return 0
        If objectType = "TAGS" Then Return 2
        If objectType = "TAGS_Region" Then Return 3

        Dim dr() As DataRow = Nothing
        dr = dt_IOS_ObjectConfig.Select("Tech=" & Chr(39) & Replace(tech.ToLower, "topx_", "").ToUpper & Chr(39) & " AND Object=" & Chr(39) & objectType & Chr(39), "loadorder")
        If dr IsNot Nothing Then
            If dr.Length > 0 Then
                Dim level As Integer = GetObjectLevelByID(CInt(dr(0)("ID").ToString))
                Return level
            End If
        End If

        If Not (String.IsNullOrEmpty(vendor)) Then
            dr = dt_IOS_ObjectConfig.Select("Technology=" & Chr(39) & tech & Chr(39) & " AND Object=" & Chr(39) & objectType & Chr(39) & " AND Vendor=" & Chr(39) & vendor & Chr(39), "loadorder")
            If dr IsNot Nothing Then
                If dr.Length > 0 Then
                    Dim level As Integer = GetObjectLevelByID(CInt(dr(0)("ID").ToString))
                    Return level
                End If
            End If
        End If

        If tech = "Parameters" Then
            Select Case objectType
                Case "WCEL"
                    Return 3
                Case "CELL"
                    Return 3
                Case "BSC"
                    Return 1
                Case "BCF"
                    Return 2
                Case "WBTS"
                    Return 2
                Case "Zone_2G"
                    Return 2
                Case "Zone_3G"
                    Return 2
                Case "RNC"
                    Return 1
                Case "Region"
                    Return 1
                Case "MR_2G"
                    Return 1
                Case "MR_3G"
                    Return 1
                Case "PLMN"
                    Return 0
            End Select
        ElseIf tech = "TX" Then
            Select Case objectType
                Case "VCI"
                    Return 4
                Case "VPI"
                    Return 3
                Case "WBTS"
                    Return 2
                Case "RNC"
                    Return 1
                    'Case "MSC"
                    'nodelevel = 1
                Case "Region"
                    Return 1
                Case "PLMN"
                    Return 0
            End Select
        ElseIf tech = "RNC" Then
            Select Case objectType
                Case "RNC"
                    Return 1
                Case "PLMN"
                    Return 0
            End Select
        ElseIf tech = "SGSN" Then
            Select Case objectType
                Case "SGSN"
                    Return 1
                Case "PLMN"
                    Return 0
            End Select
        ElseIf tech = "GGSN" Then
            Select Case objectType
                Case "GGSN"
                    Return 1
                Case "PLMN"
                    Return 0
            End Select
        ElseIf tech = "MGW" Then
            Select Case objectType
                Case "MGW"
                    Return 1
                Case "PLMN"
                    Return 0
            End Select
        ElseIf tech = "MSS" Then
            Select Case objectType
                Case "MSS"
                    Return 1
                Case "PLMN"
                    Return 0
            End Select
        ElseIf tech = "BSC" Then
            Select Case objectType
                Case "BSC"
                    Return 1
                Case "PLMN"
                    Return 0
            End Select
        Else
            Select Case objectType
                Case "BTS"
                    Return 3
                Case "CELL"
                    Return 3
                Case "BCF"
                    Return 2
                Case "SITE"
                    Return 2
                Case "Zone"
                    Return 2
                Case "BSC"
                    Return 1
                    'Case "MSC"
                    'nodelevel = 0
                Case "Region"
                    Return 1
                Case "MR"
                    Return 1
                Case "PLMN"
                    Return 0
            End Select
        End If
        Return 0
    End Function

    Public Sub GetFilteredObjectTreeData(ByVal parentList As String, ByVal ObjectID As String, ByRef dsNew As DataSet, ByVal tech As String)
        Try
            Dim dr() As DataRow = Nothing
            dr = dt_IOS_ObjectConfig.Select("[ID] = " & ObjectID & " AND ParentID IS NOT NULL AND SqlID IS NOT NULL AND Tech='" & tech & "'")
            If dr.Length > 0 Then
                Dim sql() As String
                sql = GetSQL(dr(0).Item("SqlID"), Nothing)
                If sql(1).ToLower.LastIndexOf("order by") > -1 Then
                    sql(1) = sql(1).Substring(0, sql(1).ToLower.LastIndexOf("order by") - 1)
                End If
                Dim objSql As String
                objSql = "Select * From ( " & sql(1) & " ) tbl Where tbl.objectid IN('" & parentList & "') Order By tbl.objectname"
                Dim dtParent As New DataTable
                dtParent = IOS.DataLibrary.DataAccessorODBC.GetDataTable(sql(0), objSql)
                If dtParent.Rows.Count > 0 Then
                    dsNew.Tables(0).Merge(dtParent)
                    Dim dtParentID As New DataTable
                    dtParentID = dtParent.DefaultView.ToTable(True, {"ParentID"})
                    If dtParentID.Rows.Count > 0 Then
                        Dim SelectedValues = dtParentID.AsEnumerable().Select(Function(s) s.Field(Of String)("ParentID")).ToArray()
                        Dim commaSeperatedValues As String = String.Join("','", SelectedValues)
                        GetFilteredObjectTreeData(commaSeperatedValues, dr(0).Item("ParentID"), dsNew, tech)
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Function FilterObjectTree(objectType As String, filterObject As String, ByVal tech As String) As DataSet
        Dim dsNewObject As New DataSet
        Try
            Dim dr() As DataRow = Nothing
            dr = dt_IOS_ObjectConfig.Select("[Object] LIKE '" & objectType & "' AND ParentID IS NOT NULL AND SqlID IS NOT NULL AND Tech='" & tech & "'")
            If dr.Length > 0 Then

                'Concatenate All SQL of selected objecttype with UNION ALL
                Dim ObjectSql As New StringBuilder()
                Dim sql() As String
                sql = GetSQL(dr(0).Item("SqlID"), Nothing)
                If sql(1).ToLower.LastIndexOf("order by") > -1 Then
                    sql(1) = sql(1).Substring(0, sql(1).ToLower.LastIndexOf("order by") - 1)
                End If
                Dim objSql As String
                objSql = "Select * From ( " & sql(1) & " ) tbl Where tbl.objectname like '%" & filterObject & "%' And tbl.objecttype IN('" & dr(0).Item(2) & "','" & dr(0).Item(3) & "') Order By tbl.objectname"
                dsNewObject = IOS.DataLibrary.DataAccessorODBC.GetDataSet(sql(0), objSql)

                'Get Parent List for filtered object
                Dim dtParentID As New DataTable
                dtParentID = dsNewObject.Tables(0).DefaultView.ToTable(True, {"ParentID"})
                If dtParentID.Rows.Count > 0 Then
                    Dim SelectedValues = dtParentID.AsEnumerable().Select(Function(s) s.Field(Of String)("ParentID")).ToArray()
                    Dim commaSeperatedValues As String = String.Join("','", SelectedValues)
                    GetFilteredObjectTreeData(commaSeperatedValues, dr(0).Item("ParentID"), dsNewObject, tech)
                End If
            End If
        Catch ex As Exception
            dsNewObject = New DataSet
        End Try
        Return dsNewObject
    End Function

    <Extension()>
    Public Function GetKPIChecked2String(ByRef tv As TreeList, ByVal level As String, ByVal outputtype As String) As String
        Dim outputstr As New System.Text.StringBuilder()
        If outputtype = "ObjectNameWild" Then
            outputstr.Append(" LIKE ")
        ElseIf outputtype = "Naked" Then
            outputstr.Append("")
        Else
            outputstr.Append("IN (")
        End If

        For Each nd As TreeListNode In tv.Nodes
            outputstr.Append(GetChecked2StringByLevel(tv, nd, level, outputtype))
        Next

        Dim outputfinal As String = Nothing
        If outputtype = "ObjectNameWild" Then
            outputfinal = Mid(outputstr.ToString, 1, outputstr.ToString.Length - 9)
        ElseIf outputtype = "Naked" Then
            outputfinal = outputstr.ToString.TrimEnd(",")
        Else
            outputfinal = outputstr.ToString.TrimEnd(",") + ")"
        End If
        Return outputfinal
    End Function

    <Extension()>
    Public Function GetChecked2String(ByRef tree As TreeList, ByVal tech As String, ByVal aggr_to As String, ByVal outputtype As String, Optional filterString As String = "", Optional TagsMapping As Boolean = False) As String
        Dim nodelevel As Integer
        Dim outputstr As New StringBuilder()
        If outputtype = "ObjectNameWild" Then
            outputstr.Append(" LIKE ")
        ElseIf outputtype = "Naked" Then
            outputstr.Append("")
        ElseIf outputtype = "TAGS_CM" Then
            outputstr.Append("")
        ElseIf outputtype = "ObjectType" Then
            outputstr.Append("")
        Else
            outputstr.Append("IN (")
        End If
        nodelevel = 3

        nodelevel = GetNodeLevelByObjectType(tech, aggr_to)

        If outputtype = "TAGS_CM" Then
            nodelevel = 3
        End If

        If TagsMapping = True Then
            nodelevel = 3
        End If

        If outputtype = "FilterTemplate" Then
            nodelevel = 2
        End If

        Dim StaticEndNodesCount As Integer = 0
        Dim StaticCheckedNodesCount As Integer = 0
        If outputtype = "TAGS_Static" Then
            nodelevel = tree.GetEndCheckedNodes()(0).Level
            'Get count of all static tags end checked nodes
            StaticCheckedNodesCount = tree.GetEndCheckedNodes().Count
            StaticEndNodesCount = tree.GetAllCheckedNodes().Where(Function(x) x.Level = nodelevel - 1).First.AllNodesCount
        End If

        For Each nd As TreeListNode In tree.GetAllCheckedNodes
            ' outputstr.Append(GetChecked2StringByLevel(tree, nd, nodelevel, outputtype, filterString))
            If nd.Level = nodelevel Then
                Dim Result As String = ""

                If nd.Checked = True And nd.Level = nodelevel And outputtype = "ObjectName" Then
                    Result = Result & Chr(39) & nd.Item("ObjectName") & Chr(39) & ","
                ElseIf nd.Checked And nd.Level = nodelevel And outputtype = "ObjectNameSplit" Then
                    Result = Result & Chr(39) & Split(nd.Item("ObjectName"), "-")(0).Trim.Substring(0, 5) & Chr(39) & ","
                ElseIf nd.Checked = True And nd.Level = nodelevel And outputtype = "ObjectID" Then
                    Result = Result & Chr(39) & nd.Item("ObjectID").ToString.Replace("'", "''") & Chr(39) & ","
                ElseIf nd.Checked = True And nd.Level = nodelevel And outputtype = "ObjectType" Then
                    Result = Result & nd.Item("ObjectType") & ","
                ElseIf nd.Checked = True And nd.Level = nodelevel And outputtype = "TAGS_CM" Then
                    Result = Result & nd.Item("ObjectName") & " AND "
                ElseIf nd.Checked = True And nd.Level = nodelevel And outputtype = "Naked" Then
                    Result = Result & nd.Item("ObjectName") & ","
                ElseIf nd.Checked = True And nd.Level = nodelevel And outputtype = "TAGS_Static" Then
                    If StaticCheckedNodesCount <> StaticEndNodesCount Then
                        Result = Result & Chr(39) & nd.Item("ObjectID").ToString.Replace("'", "''") & Chr(39) & ","
                    End If
                End If

                outputstr.Append(Result)
            End If
        Next

        Dim outputfinal As String = Nothing
        If outputtype = "ObjectNameWild" Then
            outputfinal = Mid(outputstr.ToString, 1, outputstr.ToString.Length - 9)
        ElseIf outputtype = "Naked" Or outputtype = "ObjectType" Then
            outputfinal = outputstr.ToString.TrimEnd(",")
        ElseIf outputtype = "TAGS_CM" Then
            outputfinal = outputstr.ToString.Substring(0, Len(outputstr.ToString) - 4)
        ElseIf outputtype = "TAGS_Static" AndAlso StaticCheckedNodesCount = StaticEndNodesCount Then
            outputfinal = "LIKE '%'"
        Else
            outputfinal = outputstr.ToString.TrimEnd(",") + ")"
        End If

        Return outputfinal
    End Function

    <Extension()>
    Public Function GetChecked2StringFilterParam(ByRef tree As TreeList, ByVal paramName As String, ByVal outputtype As String, Optional filterString As String = "") As String
        Dim nodelevel As Integer
        Dim readString As Boolean = False
        Dim outputstr As New StringBuilder()
        outputstr.Append(paramName & " IN (")

        If outputtype = "FilterTemplate" Then
            nodelevel = 2
        End If

        For Each nd As TreeListNode In tree.GetAllCheckedNodes
            ' outputstr.Append(GetChecked2StringByLevel(tree, nd, nodelevel, outputtype, filterString))
            If nd.Level = nodelevel Then
                Dim Result As String = ""
                If nd.Checked = True And nd.Level = nodelevel And nd.ParentNode.GetDisplayText("ObjectName") = paramName And outputtype = "FilterTemplate" Then
                    If nd.Item("ObjectName").ToString.ToLower.Contains("like") Or nd.Item("ObjectName").ToString.ToLower.Contains("not like") Or nd.Item("ObjectName").ToString.ToLower.Contains("<>") Or nd.Item("ObjectName").ToString.ToLower.Contains("=") Or
                        nd.Item("ObjectName").ToString.ToLower.Contains(">=") Or nd.Item("ObjectName").ToString.ToLower.Contains("<=") Or nd.Item("ObjectName").ToString.ToLower.Contains("not in") Then
                        outputstr.Replace(paramName & " IN (", "")
                        readString = True
                        If nd.Item("ObjectName").ToString.StartsWith("NOT LIKE ", StringComparison.OrdinalIgnoreCase) Then
                            Dim valuePart As String = nd.Item("ObjectName").ToString.Substring(9).Trim
                            Result = paramName & " NOT LIKE '" & valuePart.Replace("'", "''") & "' And "
                        ElseIf nd.Item("ObjectName").ToString.StartsWith("LIKE ", StringComparison.OrdinalIgnoreCase) Then
                            Dim valuePart As String = nd.Item("ObjectName").ToString.Substring(5).Trim
                            Result = paramName & " LIKE '" & valuePart.Replace("'", "''") & "' And "
                        ElseIf nd.Item("ObjectName").ToString.StartsWith("NOT IN ", StringComparison.OrdinalIgnoreCase) Then
                            Dim startIdx = nd.Item("ObjectName").ToString.IndexOf("("c)
                            Dim endIdx = nd.Item("ObjectName").ToString.LastIndexOf(")"c)

                            If startIdx = -1 OrElse endIdx = -1 OrElse endIdx <= startIdx Then
                                Result = ""
                            End If

                            Dim inner = nd.Item("ObjectName").ToString.Substring(startIdx + 1, endIdx - startIdx - 1)
                            Dim values = inner.Split(","c).Select(Function(v) "'" & v.Trim().Replace("'", "''") & "'")
                            Result = paramName & " NOT IN (" & String.Join(",", values) & ") And "
                        ElseIf nd.Item("ObjectName").ToString.StartsWith("=", StringComparison.OrdinalIgnoreCase) Then
                            Dim valuePart As String = nd.Item("ObjectName").ToString.Substring(2).Trim
                            Result = paramName & " " & nd.Item("ObjectName").ToString.Substring(0, 1).Trim & " " & "'" & valuePart.Replace("'", "''") & "' And "
                        Else
                            Dim valuePart As String = nd.Item("ObjectName").ToString.Substring(3).Trim
                            Result = paramName & " " & nd.Item("ObjectName").ToString.Substring(0, 2).Trim & " " & "'" & valuePart.Replace("'", "''") & "' And "
                        End If
                    Else
                        readString = False
                        Result = Chr(39) & nd.Item("ObjectName").ToString.Replace("'", "''") & Chr(39) & ","
                    End If
                End If

                outputstr.Append(Result)
            End If
        Next

        Dim outputfinal As String = Nothing
        If readString = False Then
            outputfinal = outputstr.ToString.TrimEnd(",") + ")"
            Return outputfinal
        Else
            outputfinal = outputstr.ToString
            Return outputfinal.Substring(0, outputfinal.Length - 4)
        End If
    End Function

    <Extension()>
    Public Function GetChecked2StringByLevel(ByRef tree As TreeList, ByVal nd As TreeListNode, ByVal level As Integer, ByVal outputtype As String, Optional filterString As String = "") As String
        Dim Result As String = ""

        If nd.Checked = True And nd.Level = level And outputtype = "ObjectName" Then
            Result = Result & Chr(39) & nd.Item("ObjectName") & Chr(39) & ","
        ElseIf nd.Checked And nd.Level = level And outputtype = "ObjectNameSplit" Then
            Result = Result & Chr(39) & Split(nd.Item("ObjectName"), "-")(0).Trim.Substring(0, 5) & Chr(39) & ","
        ElseIf nd.Checked = True And nd.Level = level And outputtype = "ObjectID" Then
            Result = Result & Chr(39) & nd.Item("ObjectID").ToString.Replace("'", "''") & Chr(39) & ","
        ElseIf nd.Checked = True And nd.Level = level And outputtype = "ObjectType" Then
            Result = Result & nd.Item("ObjectType") & ","
        ElseIf nd.Checked = True And nd.Level = level And outputtype = "TAGS_CM" Then
            Result = Result & nd.Item("ObjectName") & " OR "
        ElseIf nd.Checked = True And nd.Level = level And outputtype = "Naked" Then
            Result = Result & nd.Item("ObjectName") & ","
        End If

        Dim N As TreeListNode
        For Each N In nd.Nodes
            Result = Result & GetChecked2StringByLevel(tree, N, level, outputtype, filterString)
        Next
        N = Nothing
        Return Result
    End Function

    <Extension()>
    Public Function GetMaxNodeLevel(ByRef tlv As TreeList) As Integer
        Dim op As New GetMaxLevelOperation()
        tlv.NodesIterator.DoOperation(op)
        Return op.MaxLevel
    End Function

    <Extension()>
    Public Sub SetColumnWidth(ByRef tl As TreeList)
        Try
            tl.AutoFillColumn = Nothing
            Dim node As TreeListNode = tl.Nodes(0)
            Dim bestWidth As New TreeListOperationColumnBestWidth(tl, tl.Columns(2))
            bestWidth.GetType().GetField("bestWidth", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic).SetValue(bestWidth, 0)
            bestWidth.Execute(node)
            If node.ParentNode IsNot Nothing Then
                bestWidth.Execute(node.ParentNode)
                GetColumnBestWidth(node.ParentNode, bestWidth)
            End If
            If node.Expanded Then
                GetColumnBestWidth(node, bestWidth)
            End If
            tl.Columns(2).Width = bestWidth.BestWidth
        Catch ex As Exception
        End Try
    End Sub

    Public Sub GetColumnBestWidth(node As TreeListNode, ByRef bestWidth As TreeListOperationColumnBestWidth)
        Try
            For Each nd As TreeListNode In node.Nodes
                bestWidth.Execute(nd)
                If nd.Expanded And nd.Nodes.Count > 0 Then
                    GetColumnBestWidth(nd, bestWidth)
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    <Extension()>
    Public Sub CheckParentNode(ByRef tlv As TreeList, ByRef node As TreeListNode)
        While node.ParentNode IsNot Nothing
            node = node.ParentNode
            Dim oneChildIsChecked As Boolean = CheckOneOfChildsIsChecked(tlv, node)
            If oneChildIsChecked Then
                node.CheckState = CheckState.Checked
            Else
                node.CheckState = CheckState.Unchecked
            End If
        End While
    End Sub

    <Extension()>
    Public Function CheckOneOfChildsIsChecked(ByRef tlv As TreeList, node As TreeListNode) As Boolean
        Dim result As Boolean = False
        For Each item As TreeListNode In node.Nodes
            If item.CheckState = CheckState.Checked Then
                result = True
                Exit For
            End If
        Next
        Return result
    End Function

    <Extension()>
    Public Function GetEndCheckedNodes(ByRef tlv As TreeList, Optional ByVal filterStr As String = Nothing) As List(Of TreeListNode)
        If filterStr Is Nothing Then
            Return tlv.GetAllCheckedNodes().Where(Function(x) x.Nodes.Count = 0).ToList()  'AndAlso x.Visible = True
        ElseIf tlv.GetAllCheckedNodes().Where(Function(x) x.Nodes.Count <> 0 AndAlso x.Item("ObjectName").ToString().ToUpper().Contains(filterStr.ToUpper())).Count <> 0 Then
            Return tlv.GetAllCheckedNodes().Where(Function(x) x.Nodes.Count = 0).ToList()
        Else
            Return tlv.GetAllCheckedNodes().Where(Function(x) x.Nodes.Count = 0 AndAlso x.Item("ObjectName").ToString().ToUpper().Contains(filterStr.ToUpper())).ToList()
        End If
    End Function

    <Extension()>
    Public Sub ExecuteBeforeCheckNode(ByRef tlv As TreeList, ByRef node As TreeListNode)
        If node.Checked Then
            node.UncheckAll()
        Else
            node.CheckAll()
        End If
    End Sub

    <Extension()>
    Public Sub ExecuteAfterCheckNode(ByRef tlv As TreeList, ByRef node As TreeListNode)
        CheckParentNode(tlv, node)
    End Sub

    <Extension()>
    Public Sub ExpandTreeListUntilCertainNode(ByRef tl As TreeList, tnode As TreeListNode)
        If tnode IsNot Nothing Then
            Dim node As TreeListNode = tnode
            tl.CollapseAll()
            While Not IsNothing(node.ParentNode)
                node = node.ParentNode
                node.Expanded = True
            End While
            tl.SelectNode(tnode)
            tl.SetFocusedNode(tnode)
        End If
    End Sub

	<Extension()>
	Public Sub PupulateTreeListColumn(ByRef tl As TreeList, ByVal colList() As String)
		tl.Columns.Clear()
		For i As Integer = 0 To colList.Length - 1
			Dim col1 As TreeListColumn = New TreeListColumn()
			col1.Caption = colList(i)
			col1.VisibleIndex = i
			If colList(i) = "ObjectName" Then
				tl.AutoFillColumn = col1
				col1.Visible = True
			Else
				col1.Visible = False
			End If
			tl.Columns.Add(col1)
		Next
	End Sub

	<Extension()>
	Public Sub PupulateTreeListColumn(ByRef tl As TreeList, ByVal colList() As String, ByVal showCol As String)
		tl.Columns.Clear()
		For i As Integer = 0 To colList.Length - 1
			Dim col1 As TreeListColumn = New TreeListColumn()
			col1.Caption = colList(i)
            col1.VisibleIndex = i
            If colList(i) = showCol Then
                tl.AutoFillColumn = col1
                col1.Visible = True
            Else
                col1.Visible = False
            End If
            tl.Columns.Add(col1)
		Next
	End Sub

	<Extension()>
    Public Sub PopulateTreeList(ByRef tl As TreeList, ParentID As String, rNode As TreeListNode, ds As DataSet, Optional tblname As String = "0", Optional filterObject As String = Nothing, Optional objectType As String = Nothing, Optional tech As String = Nothing)
        Dim foundRows() As DataRow = Nothing
        If tblname = "0" Then
            foundRows = ds.Tables(0).Select("ParentID = " & Chr(39) & ParentID & Chr(39))
        Else
            If ds.Tables.Contains(tblname) = False Then Exit Sub
            foundRows = ds.Tables(tblname).Select("ParentID = " & Chr(39) & ParentID & Chr(39))
        End If

        Dim dsObjectTree As New DataSet
        If filterObject IsNot Nothing Then
            dsObjectTree = FilterObjectTree(objectType, filterObject, tech)
            foundRows = dsObjectTree.Tables(0).Select("ParentID='" & ParentID & "'")
            tblname = "0"
        Else
            dsObjectTree = ds
        End If

        If foundRows.Length > 0 Then
            Dim imgList As ImageList = tl.SelectImageList
            Dim index As Integer = imgList.Images.IndexOfKey("EMPTY")
            For Each row As DataRow In foundRows
                If row.Item(0).ToString <> "" Then
                    If imgList IsNot Nothing Then
                        index = imgList.Images.IndexOfKey(row.Item(3).ToString)
                        If tl.Tag IsNot Nothing Then
                            If tl.Tag.ToString.ToUpper.Contains("2G") Then
                                If row.ItemArray.Count > 4 Then
                                    Select Case nZ(row.Item(4).ToString.Trim, "x")
                                        Case "2"
                                            index = imgList.Images.IndexOfKey("DCS1")
                                        Case "1"
                                            index = imgList.Images.IndexOfKey("DCS")
                                        Case "0"
                                            index = imgList.Images.IndexOfKey("EGSM")
                                        Case Else
                                            index = imgList.Images.IndexOfKey(row.Item(3).ToString)
                                    End Select
                                Else
                                    index = imgList.Images.IndexOfKey(row.Item(3).ToString)
                                End If
                            ElseIf tl.Tag.ToString.ToUpper.Contains("3G") Or tl.Tag.ToString.ToUpper.Contains("4G") Or tl.Tag.ToString.ToUpper.Contains("5G") Or tl.Tag.ToString.ToUpper.Contains("CDR") Then
                                If row.ItemArray.Count > 4 Then
                                    Select Case nZ(row.Item(4).ToString.Trim, "x")
                                        Case "1"
                                            index = imgList.Images.IndexOfKey("BAND1")
                                        Case "2"
                                            index = imgList.Images.IndexOfKey("BAND2")
                                        Case "3"
                                            index = imgList.Images.IndexOfKey("BAND3")
                                        Case "4"
                                            index = imgList.Images.IndexOfKey("BAND4")
                                        Case "5"
                                            index = imgList.Images.IndexOfKey("BAND5")
                                        Case "6"
                                            index = imgList.Images.IndexOfKey("BAND6")
                                        Case "7"
                                            index = imgList.Images.IndexOfKey("BAND7")
                                        Case "8"
                                            index = imgList.Images.IndexOfKey("BAND8")
                                        Case "9"
                                            index = imgList.Images.IndexOfKey("BAND9")
                                        Case Else
                                            index = imgList.Images.IndexOfKey(row.Item(3).ToString)
                                    End Select
                                Else
                                    index = imgList.Images.IndexOfKey(row.Item(3).ToString)
                                End If
                            ElseIf tl.Tag.ToString.ToUpper.Contains("COMMON") Then
                                If row.ItemArray.Count > 4 Then
                                    Select Case nZ(row.Item(4).ToString.Trim, "x")
                                        Case "1"
                                            index = imgList.Images.IndexOfKey("BAND1")
                                        Case "2"
                                            index = imgList.Images.IndexOfKey("BAND2")
                                        Case "3"
                                            index = imgList.Images.IndexOfKey("BAND3")
                                        Case "4"
                                            index = imgList.Images.IndexOfKey("BAND4")
                                        Case "5"
                                            index = imgList.Images.IndexOfKey("BAND5")
                                        Case "6"
                                            index = imgList.Images.IndexOfKey("BAND6")
                                        Case "7"
                                            index = imgList.Images.IndexOfKey("BAND7")
                                        Case "8"
                                            index = imgList.Images.IndexOfKey("BAND8")
                                        Case "9"
                                            index = imgList.Images.IndexOfKey("BAND9")
                                        Case Else
                                            index = imgList.Images.IndexOfKey(row.Item(4).ToString)
                                    End Select
                                Else
                                    index = imgList.Images.IndexOfKey(row.Item(4).ToString)
                                End If
                            Else
                                index = imgList.Images.IndexOfKey(row.Item(3).ToString)
                            End If
                        Else
                            index = imgList.Images.IndexOfKey(row.Item(3).ToString)
                        End If
                    End If
                    Dim Column4 As String
                    If row.Table.Columns.Count = 5 Then
                        Column4 = nZ(row.Item(4).ToString.Trim, "x")
                    Else
                        Column4 = ""
                    End If
                    Dim parentnode As TreeListNode = tl.AppendNode(New Object() {row.Item(0), row.Item(1), row.Item(2), row.Item(3), Column4, index}, rNode)
                    PopulateTreeList(tl, row.Item(0), parentnode, dsObjectTree, tblname)
                    If filterObject IsNot Nothing Then
                        parentnode.ExpandAll()
                    End If
                End If
            Next row
        End If
    End Sub

	<Extension()>
	Public Sub PopulateTreeList(ByRef tl As TreeList, ParentID As String, rNode As TreeListNode, dt As DataTable)
		Dim foundRows() As DataRow = Nothing
		foundRows = dt.Select("ParentID = " & Chr(39) & ParentID & Chr(39))
		If foundRows.Length > 0 Then
			For Each row As DataRow In foundRows
                If row.Item(1).ToString <> "" Then
                    Dim parentnode As TreeListNode = tl.AppendNode(New Object() {row.Item(1), row.Item(2), row.Item(3)}, rNode)
                    If (row.Item(1).ToString.ToUpper = "ROOT") Then
                        parentnode.Tag = row.Item(2).ToString
                    ElseIf (row.Item(0).ToString <> "0") Then
                        parentnode.Tag = row.Item(0).ToString
                    Else
                        parentnode.Tag = row.Item(1).ToString & "_" & row.Item(2).ToString
                    End If
                    PopulateTreeList(tl, row.Item(2), parentnode, dt)
                End If
            Next row
		End If
	End Sub

End Module

Public Class GetMaxLevelOperation
    Inherits TreeListOperation

    Private _maxLevel As Integer = 0
    'Private searchValue As String = Nothing
    'Private fieldName As String = Nothing

    Public Sub New()
        MyBase.New()
    End Sub

    'Public Sub New(_fieldName As String, _SearchValue As String)
    '    searchValue = _SearchValue
    '    fieldName = _fieldName
    'End Sub

    Public Overrides Sub Execute(ByVal node As TreeListNode)
        If node.Level > _maxLevel Then
            _maxLevel = node.Level
        End If

        'If NodeContainsPattern(node, searchValue) Then
        '    node.Visible = True
        '    Dim temp As TreeListNode = node.ParentNode

        '    While temp IsNot Nothing
        '        temp.Visible = True
        '        temp = temp.ParentNode
        '    End While
        'Else
        '    Dim visible As Boolean = False
        '    Dim temp As TreeListNode = node.ParentNode

        '    While temp IsNot Nothing
        '        visible = NodeContainsPattern(temp, searchValue)
        '        If visible Then Exit While
        '        temp = temp.ParentNode
        '    End While

        '    node.Visible = visible
        '    'node.Collapse()
        'End If
    End Sub

    'Private Function NodeContainsPattern(ByVal node As TreeListNode, ByVal pattern As String) As Boolean
    '    'For Each col As TreeListColumn In node.TreeList.Columns
    '    If node.GetValue("ObjectName").ToString.ToUpper.Contains(pattern.ToUpper) Then Return True
    '    'Next
    '    Return False
    'End Function

    Public ReadOnly Property MaxLevel() As Integer
        Get
            Return _maxLevel
        End Get
    End Property

End Class