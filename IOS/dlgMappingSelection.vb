Imports MapInfo.Data
Imports MapInfo.Windows.Controls
Imports MapInfo.Geometry
Imports MapInfo.Mapping
Imports MapInfo.Engine
Imports MapInfo.Tools
Imports MapInfo.Styles
Imports LidorSystems.IntegralUI.Lists
Imports System.Text
Imports LidorSystems.IntegralUI.Lists.Collections

Public Class dlgMappingSelection

#Region "Variables Declaration"

    Private TemplateSetting As Integer = 0
    Private dt_OSS_UserParams As DataTable = Nothing
    Private dt_Templates As DataTable = Nothing
    Private ParamQueryString As String = Nothing
    Private Tech_Current As String = Nothing
    Private Objects_Current As String = Nothing
    Private cellsfilter As New List(Of String)
    Private dtExtnNodes As DataTable = Nothing

    'Private objFrmTechnology As frmTechnology = Nothing

#End Region

#Region "Helper"

    Private Sub ConfigurMappingSelectionForm(ByVal frmName As String)
        Try
            Dim form As IOS.Configuration.EntityModel.IOSForm = configMgr.FindFormByName(frmName)
            If Not form Is Nothing Then
                Dim counter As Integer = 0
                ConfigurForm(Me, frmName, counter)

                Dim winCtrl As IOS.Configuration.EntityModel.Control = Nothing
                Dim formControls As List(Of Object) = New List(Of Object) From {
                     tsmi_description, tsmi_mapping, tsmi_mapping_voronoi, tsmi_mapping_label, tsmi_Copy_Table_To_Clipboard, tsmi_Export_Table_To_Excel, tsmi_Show_Only_Differences
                }

                For Each frmControl As Object In formControls
                    winCtrl = form.FindControlByName(frmControl.Name)
                    If Not winCtrl Is Nothing Then
                        frmControl.Enabled = winCtrl.DefaultEnable
                        frmControl.Visible = winCtrl.DefaultVisible
                    End If
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ParentExpand(ByVal tn As TreeListViewNode)
        If tn.Level > 0 Then
            tn.Parent.Expand()
            ParentExpand(tn.Parent)
        End If
    End Sub

    Private Sub RelocateForm()
        Me.Location = frmMapWindow.MapControl1.Location
    End Sub

    Private Sub UpdateParamtersOfExpandedMO(ByRef tlvn As TreeListViewNode)
        Try
            'get MO object
            Dim tlv_itemselected As TreeListViewNode = tlvn
            Dim mo_selected As String = Nothing
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "NIFO ", "Started UpdateParamtersOfExpandedMO")
            If tlv_itemselected.Tag Is Nothing Then
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "NIFO ", "tlv_itemselected.Tag is nothing")
                Exit Sub
            Else
                mo_selected = tlv_itemselected.Text
            End If

            cellsfilter.Clear()
            Objects_Current = ""

            For Each col As TreeListViewColumn In tlvDetail.Columns
                If col.Index <> 0 Then

                    Objects_Current = Objects_Current + Chr(39) & col.HeaderText & Chr(39) + ","
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "NIFO ", "Started UpdateParamtersOfExpandedMO:" + Objects_Current)
                    'objFrmTechnology = Nothing
                    'frmMDI.OpenTechFormDynamically(Tech_Current, objFrmTechnology, True)

                    Select Case Tech_Current.ToUpper
                        Case networkAll.Network3G1.ToUpper
                            cellsfilter.Add("WCEL_CELL_ID = " & Chr(39) & col.HeaderText & Chr(39))
                        Case networkAll.Network2G1.ToUpper
                            cellsfilter.Add("CELL_ID = " & Chr(39) & col.HeaderText & Chr(39))
                        Case networkAll.Network2G2.ToUpper
                            cellsfilter.Add("NameObject = " & Chr(39) & col.HeaderText & Chr(39))
                        Case networkAll.Network2G3.ToUpper
                            cellsfilter.Add("CELL_ID = " & Chr(39) & col.HeaderText & Chr(39))
                        Case networkAll.Network3G2.ToUpper
                            cellsfilter.Add("CellId = " & Chr(39) & col.HeaderText & Chr(39))
                        Case networkAll.Network3G3.ToUpper
                            cellsfilter.Add("CellId = " & Chr(39) & col.HeaderText & Chr(39))
                        Case networkAll.Network4G1.ToUpper
                            cellsfilter.Add("eCellId = " & Chr(39) & col.HeaderText & Chr(39))
                        Case networkAll.Network4G2.ToUpper
                            cellsfilter.Add("eCellId = " & Chr(39) & col.HeaderText & Chr(39))
                        Case networkAll.Network4G3.ToUpper
                            cellsfilter.Add("eCellId = " & Chr(39) & col.HeaderText & Chr(39))
                        Case networkAll.Network5G1.ToUpper
                            cellsfilter.Add("eCellId = " & Chr(39) & col.HeaderText & Chr(39))
                        Case networkAll.Network5G2.ToUpper
                            cellsfilter.Add("eCellId = " & Chr(39) & col.HeaderText & Chr(39))
                        Case networkAll.Network5G3.ToUpper
                            cellsfilter.Add("eCellId = " & Chr(39) & col.HeaderText & Chr(39))
                        Case networkAll.NetworkNode1.ToUpper
                            'TODO
                        Case networkAll.NetworkNode2.ToUpper
                            'TODO
                        Case networkAll.NetworkNode3.ToUpper
                            'TODO
                    End Select
                End If
            Next
            Objects_Current = Objects_Current.TrimEnd(",")

            'get parameters related to MO, and configured

            Dim TemplateParamString As New StringBuilder()
            Dim db_table_name As String = ""
            For Each dr As DataRow In dt_OSS_UserParams.Select("OBJECT = '" + mo_selected + "'")
                db_table_name = dr("DB_table_name").ToString
                TemplateParamString.Append("ISNULL(COALESCE(CAST(mo.")
                TemplateParamString.Append(dr("DB_column_name").ToString)
                TemplateParamString.Append(" as varchar),'NO_VALUE'),'NO_VALUE') AS " & dr("DB_column_name").ToString & ",")
            Next
            TemplateParamString.Remove(TemplateParamString.Length - 1, 1)
            ParamQueryString = TemplateParamString.ToString

            'build string based on sql_id

            'if OSS is checked, add all oss parameters in tree
            If Me.Visible = True And Not Objects_Current Is Nothing And tlvDetail.Columns.Count <> 0 Then
                Dim parray()() As String = Nothing
                Dim strOSS As String = ""
                Dim sql_ossParams As String = ""

                'objFrmTechnology = Nothing
                'frmMDI.OpenTechFormDynamically(Tech_Current, objFrmTechnology, True)

                '    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "NIFO ", "Started UpdateParamtersOfExpandedMO: TechCurrent: " + Tech_Current.ToUpper)
                '    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "NIFO ", "Started UpdateParamtersOfExpandedMO: NetworkAll: " + networkAll.Network5G1.ToUpper)

                'load table based on tech
                Select Case Tech_Current.ToUpper
                    Case networkAll.Network3G1.ToUpper
                        Dim ptemp()() As String = {New String() {"@cellid", "IN (" & Objects_Current & ")"},
                            New String() {"@columns", ParamQueryString},
                            New String() {"@mo_table", db_table_name}}

                        parray = ptemp
                        strOSS = GetSQL(1029, parray)(0)
                        sql_ossParams = GetSQL(1029, parray)(1)
                    Case networkAll.Network2G1.ToUpper
                        'no parameters availabe
                        Dim ptemp()() As String = {New String() {"@cellid", "IN (" & Objects_Current & ")"},
                            New String() {"@columns", ParamQueryString},
                         New String() {"@mo_table", db_table_name}}
                        parray = ptemp
                        strOSS = GetSQL(1031, parray)(0)
                        sql_ossParams = GetSQL(1031, parray)(1)
                    Case networkAll.Network2G2.ToUpper
                        'no parameters availabe
                        Dim ptemp()() As String = {New String() {"@cellid", "IN (" & Objects_Current & ")"},
                            New String() {"@columns", ParamQueryString},
                         New String() {"@mo_table", db_table_name}}
                        parray = ptemp

                        strOSS = GetSQL(1513, parray)(0)
                        sql_ossParams = GetSQL(1513, parray)(1)
                    Case networkAll.Network2G3.ToUpper
                        'no parameters availabe
                        Dim ptemp()() As String = {New String() {"@cellid", "IN (" & Objects_Current & ")"},
                            New String() {"@columns", ParamQueryString},
                         New String() {"@mo_table", db_table_name}}
                        parray = ptemp

                        strOSS = GetSQL(20010, parray)(0)
                        sql_ossParams = GetSQL(20010, parray)(1)
                    Case networkAll.Network3G2.ToUpper
                        'no parameters availabe
                        Dim ptemp()() As String = {New String() {"@cellid", "IN (" & Objects_Current & ")"},
                           New String() {"@columns", ParamQueryString},
                         New String() {"@mo_table", db_table_name}}
                        parray = ptemp
                        strOSS = GetSQL(9511, parray)(0)
                        sql_ossParams = GetSQL(9511, parray)(1)
                    Case networkAll.Network3G3.ToUpper
                        'no parameters availabe
                        Dim ptemp()() As String = {New String() {"@cellid", "IN (" & Objects_Current & ")"},
                            New String() {"@columns", ParamQueryString},
                         New String() {"@mo_table", db_table_name}}
                        parray = ptemp
                        strOSS = GetSQL(30010, parray)(0)
                        sql_ossParams = GetSQL(30010, parray)(1)
                    Case networkAll.Network4G1.ToUpper
                        'no parameters availabe
                        Dim ptemp()() As String = {New String() {"@cellid", "IN (" & Objects_Current & ")"},
                            New String() {"@columns", ParamQueryString},
                         New String() {"@mo_table", db_table_name}}
                        parray = ptemp
                        strOSS = GetSQL(10010, parray)(0)
                        sql_ossParams = GetSQL(10010, parray)(1)
                    Case networkAll.Network4G2.ToUpper
                        'no parameters availabe
                        Dim ptemp()() As String = {New String() {"@cellid", "IN (" & Objects_Current & ")"},
                            New String() {"@columns", ParamQueryString},
                         New String() {"@mo_table", db_table_name}}
                        parray = ptemp
                        strOSS = GetSQL(15010, parray)(0)
                        sql_ossParams = GetSQL(15010, parray)(1)
                    Case networkAll.Network4G3.ToUpper
                        'no parameters availabe
                        Dim ptemp()() As String = {New String() {"@cellid", "IN (" & Objects_Current & ")"},
                            New String() {"@columns", ParamQueryString},
                            New String() {"@mo_table", db_table_name}}
                        parray = ptemp
                        strOSS = GetSQL(17010, parray)(0)
                        sql_ossParams = GetSQL(17010, parray)(1)
                    Case networkAll.Network5G1.ToUpper
                        'UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "NIFO ", "5G1")
                        'no parameters availabe
                        Dim ptemp()() As String = {New String() {"@cellid", "IN (" & Objects_Current & ")"},
                            New String() {"@columns", ParamQueryString},
                            New String() {"@mo_table", db_table_name}}
                        parray = ptemp
                        strOSS = GetSQL(51010, parray)(0)
                        sql_ossParams = GetSQL(51010, parray)(1)
                    Case networkAll.Network5G2.ToUpper
                        'no parameters availabe
                        Dim ptemp()() As String = {New String() {"@cellid", "IN (" & Objects_Current & ")"},
                            New String() {"@columns", ParamQueryString},
                            New String() {"@mo_table", db_table_name}}
                        parray = ptemp
                        strOSS = GetSQL(52010, parray)(0)
                        sql_ossParams = GetSQL(52010, parray)(1)
                    Case networkAll.Network5G3.ToUpper
                        'no parameters availabe
                        Dim ptemp()() As String = {New String() {"@cellid", "IN (" & Objects_Current & ")"},
                            New String() {"@columns", ParamQueryString},
                            New String() {"@mo_table", db_table_name}}
                        parray = ptemp
                        strOSS = GetSQL(53010, parray)(0)
                        sql_ossParams = GetSQL(53010, parray)(1)
                End Select

                '   UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "NIFO ", "Started UpdateParamtersOfExpandedMO: query: " + sql_ossParams)

                If Not sql_ossParams.Contains("@motable") Then
                    sql_ossParams = Replace(sql_ossParams, "mo.", db_table_name + ".")
                End If
                'fire
                Dim dtParams_Cell As System.Data.DataTable = New System.Data.DataTable
                dtParams_Cell = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strOSS, sql_ossParams)


                If dtParams_Cell Is Nothing Then
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "NIFO ", "Started UpdateParamtersOfExpandedMO: No Data")
                    Exit Sub
                End If
                If dtParams_Cell.Rows.Count > tlvSelection.Nodes.Count Then
                    Dim pr As Integer = 0
                    For pr = 0 To dtParams_Cell.Rows.Count - 1
                        If pr + 1 > tlvDetail.Columns.Count - 1 Then
                            Dim tlvc As TreeListViewColumn = New TreeListViewColumn()
                            tlvDetail.Columns.Add(tlvc)
                        End If

                        Select Case Tech_Current.ToUpper
                            Case networkAll.Network3G1.ToUpper
                                tlvDetail.Columns(pr + 1).HeaderText = dtParams_Cell.Rows(pr)("WCEL_CELL_ID")
                            Case networkAll.Network2G1.ToUpper
                                tlvDetail.Columns(pr + 1).HeaderText = dtParams_Cell.Rows(pr)("CELL_ID")
                            Case networkAll.Network2G2.ToUpper
                                tlvDetail.Columns(pr + 1).HeaderText = dtParams_Cell.Rows(pr)("NameObject")
                            Case networkAll.Network2G3.ToUpper
                                tlvDetail.Columns(pr + 1).HeaderText = dtParams_Cell.Rows(pr)("CELL_ID")
                            Case networkAll.Network3G2.ToUpper
                                tlvDetail.Columns(pr + 1).HeaderText = dtParams_Cell.Rows(pr)("CellID")
                            Case networkAll.Network3G3.ToUpper
                                tlvDetail.Columns(pr + 1).HeaderText = dtParams_Cell.Rows(pr)("CellID")
                            Case networkAll.Network4G1.ToUpper
                                tlvDetail.Columns(pr + 1).HeaderText = dtParams_Cell.Rows(pr)("eCellID")
                            Case networkAll.Network4G2.ToUpper
                                tlvDetail.Columns(pr + 1).HeaderText = dtParams_Cell.Rows(pr)("eCellID")
                            Case networkAll.Network4G3.ToUpper
                                tlvDetail.Columns(pr + 1).HeaderText = dtParams_Cell.Rows(pr)("eCellID")
                            Case networkAll.Network5G1.ToUpper
                                tlvDetail.Columns(pr + 1).HeaderText = dtParams_Cell.Rows(pr)("eCellID")
                            Case networkAll.Network5G2.ToUpper
                                tlvDetail.Columns(pr + 1).HeaderText = dtParams_Cell.Rows(pr)("eCellID")
                            Case networkAll.Network5G3.ToUpper
                                tlvDetail.Columns(pr + 1).HeaderText = dtParams_Cell.Rows(pr)("eCellID")
                        End Select
                    Next
                End If

                'map to existing treelistview
                'start construction of tree
                Dim l1 As String = ""
                Dim l2 As String = ""
                Dim l3 As String = ""
                Dim catnode As TreeListViewNode = Nothing
                Dim objnode As TreeListViewNode = Nothing
                Dim paramnode As TreeListViewNode = Nothing

                Dim dt_MO_Params As DataTable = dt_OSS_UserParams.Select("OBJECT = '" & mo_selected & "'").CopyToDataTable
                For Each filterstring As String In cellsfilter
                    Dim ParamRow() As DataRow = dtParams_Cell.Select(filterstring)
                    If cellsfilter.IndexOf(filterstring) = 0 Then 'create the nodes
                        For Each drow As DataRow In dt_MO_Params.Rows
                            'level 0
                            Dim parentnode As TreeListViewNode = tlvDetail.Nodes(dt_MO_Params(0)(0).ToString)
                            'level 1
                            l1 = drow("GROUP_NAME").ToString.Trim
                            catnode = parentnode.Nodes(l1)
                            'level 2
                            If catnode.Nodes.Count > 0 Then
                                l2 = drow("OBJECT").ToString.Trim
                                objnode = catnode.Nodes(l2)
                                'level3
                                If objnode.Nodes.Count > 0 Then
                                    l3 = drow("PARAM").ToString.Trim
                                    paramnode = objnode.Nodes(l3)
                                    If paramnode IsNot Nothing Then
                                        Dim si As TreeListViewSubItem = New TreeListViewSubItem
                                        si.Text = ParamRow(0)(dt_MO_Params.Rows.IndexOf(drow)).ToString
                                        paramnode.SubItems(cellsfilter.IndexOf(filterstring) + 1).Text = si.Text
                                    End If
                                End If
                            End If
                        Next
                    Else
                        For Each drow As DataRow In dt_MO_Params.Rows
                            'level 0
                            Dim parentnode As TreeListViewNode = tlvDetail.Nodes(dt_MO_Params(0)(0).ToString)
                            'level 1
                            l1 = drow("GROUP_NAME").ToString.Trim
                            catnode = parentnode.Nodes(l1)
                            'level 2
                            If catnode.Nodes.Count > 0 Then
                                l2 = drow("OBJECT").ToString.Trim
                                objnode = catnode.Nodes(l2)
                                'level3
                                If objnode.Nodes.Count > 0 Then
                                    l3 = drow("PARAM").ToString.Trim
                                    paramnode = objnode.Nodes(l3)
                                    Dim si As TreeListViewSubItem = New TreeListViewSubItem
                                    si.Text = ParamRow(0)(dt_MO_Params.Rows.IndexOf(drow)).ToString
                                    If si.Text <> paramnode.SubItems(1).Text Then
                                        paramnode.StateImageIndex = 4
                                        paramnode.Parent.StateImageIndex = 4
                                        paramnode.Parent.Parent.StateImageIndex = 4
                                    End If
                                    If paramnode.SubItems.Count - 1 < cellsfilter.IndexOf(filterstring) + 1 Then
                                        paramnode.SubItems.Add(si)
                                    Else
                                        paramnode.SubItems(cellsfilter.IndexOf(filterstring) + 1).Text = si.Text
                                    End If
                                End If
                            End If
                        Next
                    End If
                    tlvDetail.Nodes(dt_OSS_UserParams(0)(0).ToString).Expand()
                Next
            End If

            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "NIFO ", "Completed UpdateParamtersOfExpandedMO")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub UpdateExtensionNodes(ByRef tlvn As TreeListViewNode)
        Try
            If tlvn.Tag Is Nothing Then Exit Sub
            Dim db_table_name As String = tlvn.Key
            Dim strQuery As String = "SELECT * FROM " & db_table_name & " WHERE CELLNAME IN (" & Objects_Current & ")"

            If Me.Visible = True And Not Objects_Current Is Nothing And tlvDetail.Columns.Count <> 0 Then
                Dim dtParams_Cell As System.Data.DataTable = New System.Data.DataTable
                dtParams_Cell = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, strQuery)
                Dim newsubitem As TreeListViewSubItem = Nothing
                For Each drow As DataRow In dtExtnNodes.Select("DbTableName='" & db_table_name & "'")
                    Dim subnode As TreeListViewNode = tlvn.Nodes(drow("FieldToFilter").ToString)
                    newsubitem = New TreeListViewSubItem
                    newsubitem.Text = drow("FieldToFilter").ToString
                    newsubitem.Key = drow("FieldToFilter").ToString
                    subnode.SubItems.Add(newsubitem)

                    Dim i As Integer = 0
                    For Each lvitem2 As TreeListViewNode In tlvSelection.Nodes
                        Dim dr As DataRow = dtParams_Cell.Rows(i)
                        If dr IsNot Nothing Then
                            newsubitem = New TreeListViewSubItem
                            newsubitem.Text = dr(drow("FieldToFilter"))
                            subnode.SubItems.Add(newsubitem)
                        End If
                        i = i + 1
                    Next
                    tlvn.Nodes.Add(subnode)
                Next
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub ParameterMapping(ByVal MappingType As String)
        Dim tn As TreeListViewNode = tlvDetail.SelectedNode
        Dim param_selected = tn.SubItems(0).Text & "_Map"
        If param_selected.Length > 27 Then
            param_selected = param_selected.Substring(0, 27)
        End If
        Dim obj_selected = tn.Parent.Text
        Dim tech As String = tlvSelection.Nodes(0).SubItems(1).Text
        If tlvSelection.Nodes(0).SubItems(3).Text.Contains("NanoBTS") Then
            tech = "NanoBTS"
        ElseIf tlvSelection.Nodes(0).SubItems(3).Text.Contains("Nano3G") Then
            tech = "Nano3G"
        End If

        Dim col_param As String = Nothing
        Dim sql_paramOfCells As String = Nothing
        Dim stross As String = Nothing
        Dim drow() As DataRow = Nothing

        Application.UseWaitCursor = True
        Application.DoEvents()

        Try
            If tn.Parent.Text <> "Physical" Then
                drow = dt_OSS_UserParams.Select("PARAM = " & Chr(39) & tn.SubItems(0).Text & Chr(39) & " AND OBJECT = " & Chr(39) & obj_selected & Chr(39))
                Dim db_table_name As String = drow(0)("DB_table_name")
                col_param = "mo." & drow(0)("DB_column_name") & Chr(32) & Chr(34) & param_selected & Chr(34)
                Dim parray()() As String = {New String() {"@columns", col_param}, New String() {"@mo_table", drow(0)("DB_table_name")}}

                'objFrmTechnology = Nothing
                'frmMDI.OpenTechFormDynamically(tech, objFrmTechnology, True)

                If tech.ToUpper = networkAll.Network3G1.ToUpper Then
                    stross = GetSQL(1033, parray)(0)
                    sql_paramOfCells = GetSQL(1033, parray)(1)
                ElseIf tech.ToUpper = networkAll.Network2G1.ToUpper Then
                    stross = GetSQL(1032, parray)(0)
                    sql_paramOfCells = GetSQL(1032, parray)(1)
                ElseIf tech.ToUpper = networkAll.Network2G2.ToUpper Then
                    stross = GetSQL(1514, parray)(0)
                    sql_paramOfCells = GetSQL(1514, parray)(1)
                ElseIf tech.ToUpper = networkAll.Network2G3.ToUpper Then
                    stross = GetSQL(20011, parray)(0)
                    sql_paramOfCells = GetSQL(20011, parray)(1)
                ElseIf tech.ToUpper = networkAll.Network3G2.ToUpper Then
                    stross = GetSQL(9512, parray)(0)
                    sql_paramOfCells = GetSQL(9512, parray)(1)
                ElseIf tech.ToUpper = networkAll.Network3G3.ToUpper Then
                    stross = GetSQL(30011, parray)(0)
                    sql_paramOfCells = GetSQL(30011, parray)(1)
                ElseIf tech.ToUpper = networkAll.Network4G1.ToUpper Then
                    stross = GetSQL(10011, parray)(0)
                    sql_paramOfCells = GetSQL(10011, parray)(1)
                ElseIf tech.ToUpper = networkAll.Network4G2.ToUpper Then
                    stross = GetSQL(15011, parray)(0)
                    sql_paramOfCells = GetSQL(15011, parray)(1)
                ElseIf tech.ToUpper = networkAll.Network4G3.ToUpper Then
                    stross = GetSQL(17011, parray)(0)
                    sql_paramOfCells = GetSQL(17011, parray)(1)
                ElseIf tech.ToUpper = networkAll.Network5G1.ToUpper Then
                    stross = GetSQL(51011, parray)(0)
                    sql_paramOfCells = GetSQL(51011, parray)(1)
                ElseIf tech.ToUpper = networkAll.Network5G2.ToUpper Then
                    stross = GetSQL(52011, parray)(0)
                    sql_paramOfCells = GetSQL(52011, parray)(1)
                ElseIf tech.ToUpper = networkAll.Network5G3.ToUpper Then
                    stross = GetSQL(53011, parray)(0)
                    sql_paramOfCells = GetSQL(53011, parray)(1)
                ElseIf tech.ToUpper = networkAll.NetworkNode1.ToUpper Then
                    stross = GetSQL(0, parray)(0)
                    sql_paramOfCells = GetSQL(0, parray)(1)
                ElseIf tech.ToUpper = networkAll.NetworkNode2.ToUpper Then
                    stross = GetSQL(0, parray)(0)
                    sql_paramOfCells = GetSQL(0, parray)(1)
                ElseIf tech.ToUpper = networkAll.NetworkNode3.ToUpper Then
                    stross = GetSQL(0, parray)(0)
                    sql_paramOfCells = GetSQL(0, parray)(1)
                End If

                If Not sql_paramOfCells.Contains("@motable") Then
                    sql_paramOfCells = Replace(sql_paramOfCells, "mo.", db_table_name + ".")
                End If
                Application.DoEvents()

                Dim dt_paramOfCells As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(stross, sql_paramOfCells)
                Dim dt_Map_Configuration As DataTable = GetDataTable("dt_Map_Configuration")
                Dim dtTechLayers As DataTable = dt_Map_Configuration.AsEnumerable().Where(Function(x) x.Field(Of String)("LayerTech") = tech.ToUpper).CopyToDataTable()
                Dim visibleLyrCntr As Integer = 0
                Dim processedLyrCntr As Integer = 0

                'checking visible map tech layers in the map
                For Each drTechLyr As DataRow In dtTechLayers.Rows
                    Dim lyr As FeatureLayer = CType(frmMapWindow.MapControl1.Map.Layers(drTechLyr("LayerName").ToString.Trim), FeatureLayer)
                    If lyr IsNot Nothing Then
                        If frmMapWindow.MapControl1.Map.Layers(drTechLyr("LayerName")).IsVisible = True Then
                            visibleLyrCntr = visibleLyrCntr + 1
                        End If
                    End If
                Next

                If dt_paramOfCells IsNot Nothing Then
                    'mapping with visible map layers only
                    For Each drw As DataRow In dtTechLayers.Rows
                        Dim lyr As FeatureLayer = CType(frmMapWindow.MapControl1.Map.Layers(drw("LayerName").ToString.Trim), FeatureLayer)
                        If lyr IsNot Nothing Then
                            If drw("LayerActive") = True AndAlso drw("LayerTech").ToString.ToUpper = tech.ToUpper AndAlso frmMapWindow.MapControl1.Map.Layers(drw("LayerName")).IsVisible = True Then

                                WaitScreen.ShowWaitScreen("Layers To Process: " & CInt(visibleLyrCntr - processedLyrCntr) & vbCrLf & "Mapping Layer: " & drw("LayerName").ToString.Trim)

                                If MappingType = "cells" Then
                                    frmMapWindow.Parameter_Map(dt_paramOfCells, drw("LayerName").ToString.Trim, tech, param_selected)
                                ElseIf MappingType = "voronoi" Then
                                    frmMapWindow.Parameter_Map(dt_paramOfCells.Copy, drw("LayerName").ToString.Trim & "_Voronoi", tech, param_selected)
                                ElseIf MappingType = "label" Then
                                    frmMapWindow.ParameterLabel_Map(dt_paramOfCells, drw("LayerName").ToString.Trim, tech, param_selected)
                                End If

                                WaitScreen.CloseWaitScreen()
                                processedLyrCntr = processedLyrCntr + 1

                            End If
                        End If
                    Next

                    Dim GridCtrl_Map As DevExpress.XtraGrid.GridControl = frmMapWindow.CreateTabWithGridViewForMapData("Parameters Of Cells")
                    Dim GridView_Map As DevExpress.XtraGrid.Views.Grid.GridView = GridCtrl_Map.MainView
                    Library.IOSDevExpressGrid.PopulateDataInGrid(GridCtrl_Map, GridView_Map, dt_paramOfCells, "ALL")
                    dt_paramOfCells.Dispose()
                    dt_paramOfCells = Nothing
                End If
            Else
                'create a thematic for selected table & field
                Dim dt_Map_Configuration As DataTable = GetDataTable("dt_Map_Configuration")

                For Each drw As DataRow In dt_Map_Configuration.Rows
                    If drw("LayerActive") = True AndAlso drw("LayerTech").ToString.ToUpper = tech.ToUpper AndAlso frmMapWindow.MapControl1.Map.Layers(drw("LayerName")).IsVisible = True Then
                        Application.DoEvents()
                        If MappingType = "cells" Then
                            frmMapWindow.CreateThematicOfExistingTable(drw("LayerName").ToString.Trim, tn.SubItems(0).Text)
                        ElseIf MappingType = "voronoi" Then
                            frmMapWindow.CreateThematicOfExistingTable(drw("LayerName").ToString.Trim & "_Voronoi", tn.SubItems(0).Text)
                        ElseIf MappingType = "label" Then
                            frmMapWindow.CreateThematicOfExistingTable_Label("Labels_" & drw("LayerName").ToString.Trim, tn.SubItems(0).Text, drw("LayerName").ToString)
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            Application.UseWaitCursor = False
        Finally
            WaitScreen.CloseWaitScreen()
        End Try
        Application.UseWaitCursor = False
    End Sub

    Private Sub UpdateParamtersOfExpandedMO2(ByRef tlvn As TreeListViewNode, ByVal mo_selected As String, ByRef dtParams_Cell As DataTable)
        Try
            Dim tlv_itemselected As TreeListViewNode = tlvn
            If Me.Visible = True And Not Objects_Current Is Nothing And tlvDetail.Columns.Count <> 0 Then
                If dtParams_Cell Is Nothing Then Exit Sub
                'map to existing treelistview
                'start construction of tree
                Dim l1 As String = ""
                Dim l2 As String = ""
                Dim l3 As String = ""
                Dim catnode As TreeListViewNode = Nothing
                Dim objnode As TreeListViewNode = Nothing
                Dim paramnode As TreeListViewNode = Nothing

                Dim dt_MO_Params As DataTable = dt_OSS_UserParams.Select("OBJECT = '" & mo_selected & "'").CopyToDataTable

                For Each filterstring As String In cellsfilter

                    Dim ParamRow() As DataRow = dtParams_Cell.Select(filterstring)

                    If cellsfilter.IndexOf(filterstring) = 0 Then 'create the nodes 

                        For Each drow As DataRow In dt_MO_Params.Rows
                            'level 0
                            Dim parentnode As TreeListViewNode = tlvDetail.Nodes(dt_MO_Params(0)(0).ToString)
                            'level 1
                            l1 = drow("GROUP_NAME").ToString.Trim
                            catnode = parentnode.Nodes(l1)

                            'level 2
                            If catnode.Nodes.Count > 0 Then
                                l2 = drow("OBJECT").ToString.Trim
                                objnode = catnode.Nodes(l2)

                                'level3
                                If objnode.Nodes.Count > 0 Then
                                    l3 = drow("PARAM").ToString.Trim
                                    paramnode = objnode.Nodes(l3)
                                    Dim si As TreeListViewSubItem = New TreeListViewSubItem
                                    si.Text = ParamRow(0)(dt_MO_Params.Rows.IndexOf(drow)).ToString
                                    paramnode.SubItems(cellsfilter.IndexOf(filterstring) + 1).Text = si.Text
                                End If
                            End If
                        Next
                    Else
                        Dim l1NodeIsVisible As Boolean = True
                        Dim l2NodeIsVisible As Boolean = True
                        Dim l3NodeIsVisible As Boolean = True
                        Dim IsFirsttime As Boolean = True
                        For Each drow As DataRow In dt_MO_Params.Rows
                            'level 0
                            Dim parentnode As TreeListViewNode = tlvDetail.Nodes(dt_MO_Params(0)(0).ToString)
                            'level 1
                            l1 = drow("GROUP_NAME").ToString.Trim
                            catnode = parentnode.Nodes(l1)

                            'level 2
                            If catnode.Nodes.Count > 0 Then
                                l2 = drow("OBJECT").ToString.Trim
                                objnode = catnode.Nodes(l2)

                                'level3
                                If objnode.Nodes.Count > 0 Then
                                    l3 = drow("PARAM").ToString.Trim
                                    paramnode = objnode.Nodes(l3)
                                    Dim si As TreeListViewSubItem = New TreeListViewSubItem
                                    si.Text = ParamRow(0)(dt_MO_Params.Rows.IndexOf(drow)).ToString
                                    If si.Text <> paramnode.SubItems(1).Text Then
                                        paramnode.StateImageIndex = 4
                                        paramnode.Parent.StateImageIndex = 4
                                        paramnode.Parent.Parent.StateImageIndex = 4
                                        paramnode.Visible = True
                                        l3NodeIsVisible = True
                                    Else
                                        paramnode.Visible = False
                                        l3NodeIsVisible = False
                                        objnode.Nodes(l3).Visible = False
                                    End If
                                    If paramnode.SubItems.Count - 1 < cellsfilter.IndexOf(filterstring) + 1 Then
                                        ''If (paramnode.IsVisible) Then
                                        paramnode.SubItems.Add(si)
                                        ''End If
                                    Else
                                        paramnode.SubItems(cellsfilter.IndexOf(filterstring) + 1).Text = si.Text
                                    End If
                                End If
                            End If
                        Next
                    End If
                Next
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub GetMoNode(ByRef selectedNode As TreeListViewNode)
        Dim lstObjet As String = Nothing
        Dim isFirstTime As Boolean = True
        Dim dtParamsCell As DataTable = New DataTable
        For Each df As TreeListViewNode In selectedNode.Nodes
            If (df.Level = 3) Then
                If (isFirstTime) Then
                    isFirstTime = False
                End If
                If (isFirstTime = False And Not lstObjet = df.Text) Then
                    dtParamsCell = GetDtParam(selectedNode.Text)
                    lstObjet = df.Text
                    If dtParamsCell.Rows.Count > tlvSelection.Nodes.Count Then

                        Dim pr As Integer = 0
                        For pr = 0 To dtParamsCell.Rows.Count - 1
                            If pr + 1 > tlvDetail.Columns.Count - 1 Then
                                Dim tlvc As TreeListViewColumn = New TreeListViewColumn()
                                tlvDetail.Columns.Add(tlvc)
                            End If

                            'objFrmTechnology = Nothing
                            'frmMDI.OpenTechFormDynamically(Tech_Current, objFrmTechnology, True)

                            Select Case Tech_Current.ToUpper
                                Case networkAll.Network3G1.ToUpper
                                    tlvDetail.Columns(pr + 1).HeaderText = dtParamsCell.Rows(pr)("WCEL_CELL_ID")
                                Case networkAll.Network2G1.ToUpper
                                    tlvDetail.Columns(pr + 1).HeaderText = dtParamsCell.Rows(pr)("CELL_ID")
                                Case networkAll.Network2G2.ToUpper
                                    tlvDetail.Columns(pr + 1).HeaderText = dtParamsCell.Rows(pr)("NameObject")
                                Case networkAll.Network2G3.ToUpper
                                    tlvDetail.Columns(pr + 1).HeaderText = dtParamsCell.Rows(pr)("CELL_ID")
                                Case networkAll.Network3G2.ToUpper
                                    tlvDetail.Columns(pr + 1).HeaderText = dtParamsCell.Rows(pr)("CellID")
                                Case networkAll.Network3G3.ToUpper
                                    tlvDetail.Columns(pr + 1).HeaderText = dtParamsCell.Rows(pr)("CellID")
                                Case networkAll.Network4G1.ToUpper
                                    tlvDetail.Columns(pr + 1).HeaderText = dtParamsCell.Rows(pr)("eCellID")
                                Case networkAll.Network4G2.ToUpper
                                    tlvDetail.Columns(pr + 1).HeaderText = dtParamsCell.Rows(pr)("eCellID")
                                Case networkAll.Network4G3.ToUpper
                                    tlvDetail.Columns(pr + 1).HeaderText = dtParamsCell.Rows(pr)("eCellID")
                                Case networkAll.Network5G1.ToUpper
                                    'tlvDetail.Columns(pr + 1).HeaderText = dtParamsCell.Rows(pr)("eCellID")    'TODO
                                Case networkAll.Network5G2.ToUpper
                                    'tlvDetail.Columns(pr + 1).HeaderText = dtParamsCell.Rows(pr)("eCellID")    'TODO
                                Case networkAll.Network5G3.ToUpper
                                    'tlvDetail.Columns(pr + 1).HeaderText = dtParamsCell.Rows(pr)("eCellID")    'TODO
                            End Select
                        Next
                    End If
                End If
                If (dtParamsCell IsNot Nothing) Then
                    UpdateParamtersOfExpandedMO2(df, selectedNode.Text, dtParamsCell)
                End If
            End If
        Next
    End Sub

    Private Function TreeViewList_CellExists(ByVal cellid As String) As Boolean
        For Each col As TreeListViewColumn In tlvDetail.Columns
            If col.HeaderText.ToString = cellid Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function GetDataTableFromTLV() As DataTable
        Try
            Dim dtImport As DataTable = New DataTable()
            dtImport.Columns.Add("Parameter Template Name")
            dtImport.Columns.Add("Parameter GroupName")
            dtImport.Columns.Add("Managed Object Name")
            dtImport.Columns.Add("Parameter Name")
            Dim headerList As List(Of String) = New List(Of String)()
            headerList.Add("Parameter Template Name")
            headerList.Add("Parameter GroupName")
            headerList.Add("Managed Object Name")
            headerList.Add("Parameter Name")

            If tlvSelection.Nodes.Count <> 0 Then
                For Each lIitem As TreeListViewNode In tlvSelection.Nodes
                    headerList.Add(lIitem.Text)
                    dtImport.Columns.Add(lIitem.Text)
                Next
            End If

            Dim drTemp As DataRow

            For Each mainTlvNode As LidorSystems.IntegralUI.Lists.TreeListViewNode In tlvDetail.Nodes
                If (mainTlvNode.Text.ToLower = "physical") Then
                    For Each tlvNodeManagedObject As LidorSystems.IntegralUI.Lists.TreeListViewNode In mainTlvNode.Nodes
                        drTemp = dtImport.NewRow
                        drTemp(0) = mainTlvNode.Text
                        drTemp(1) = ""
                        drTemp(2) = ""
                        Dim indexColumn As Integer = 3
                        For Each tlvItemParameterName As TreeListViewSubItem In tlvNodeManagedObject.SubItems
                            drTemp(indexColumn) = tlvItemParameterName.Text
                            indexColumn = indexColumn + 1
                        Next
                        dtImport.Rows.Add(drTemp)
                    Next
                    drTemp = dtImport.NewRow
                    For index = 0 To drTemp.Table.Columns.Count - 1
                        drTemp(index) = "--"
                    Next
                    dtImport.Rows.Add(drTemp)
                Else
                    For Each tlvNodeGoupName As LidorSystems.IntegralUI.Lists.TreeListViewNode In mainTlvNode.Nodes
                        For Each tlvNodeManagedObject As LidorSystems.IntegralUI.Lists.TreeListViewNode In tlvNodeGoupName.Nodes
                            For Each tlvNodeParameterName As LidorSystems.IntegralUI.Lists.TreeListViewNode In tlvNodeManagedObject.Nodes
                                drTemp = dtImport.NewRow
                                drTemp(0) = mainTlvNode.Text
                                drTemp(1) = tlvNodeGoupName.Text
                                drTemp(2) = tlvNodeManagedObject.Text
                                Dim indexColumn As Integer = 3
                                For Each tlvItemParameterName As TreeListViewSubItem In tlvNodeParameterName.SubItems
                                    drTemp(indexColumn) = tlvItemParameterName.Text
                                    indexColumn = indexColumn + 1
                                Next
                                dtImport.Rows.Add(drTemp)
                            Next
                        Next
                    Next
                End If
            Next
            Return dtImport
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            Return Nothing
        End Try
    End Function

    Private Function TreeListView_SearchWildCard(ByVal str As String, ByVal nd As TreeListViewNode, ByVal startindex As Integer)
        Try
            Dim tlvnd As TreeListViewNode = Nothing
            For Each nd In nd.Nodes
                If nd.Text.ToUpper.StartsWith(str.ToUpper) And nd.FlatIndex > startindex Then
                    nd.EnsureVisible()
                    nd.TreeListView.SelectedNode = nd
                    Return nd
                    Exit Function
                End If
                tlvnd = TreeListView_SearchWildCard(str, nd, startindex)
                If Not tlvnd Is Nothing Then
                    Return tlvnd
                End If
            Next
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
        Return Nothing
    End Function

    Private Function ExpandToLevel(ByVal nodes As TreeListViewNodeCollection, ByVal level As Integer) As TreeListViewNode
        If (level > 0) Then
            For Each node As TreeListViewNode In nodes
                If (node.Level = 3) Then
                    Return node
                Else
                    ExpandToLevel(node.Nodes, level - 1)
                End If
            Next
        End If
        Return Nothing
    End Function

    Private Function GetDtParam(ByVal mo_selected As String) As DataTable
        Dim TemplateParamString As New StringBuilder()
        Dim db_table_name As String = ""
        For Each dr As DataRow In dt_OSS_UserParams.Select("OBJECT = '" + mo_selected + "'")
            db_table_name = dr("DB_table_name").ToString
            TemplateParamString.Append("COALESCE(mo.")
            TemplateParamString.Append(dr("DB_column_name").ToString)
            TemplateParamString.Append(",'NO_VALUE') AS " & dr("DB_column_name").ToString & ",")
        Next
        TemplateParamString.Remove(TemplateParamString.Length - 1, 1)
        ParamQueryString = TemplateParamString.ToString

        'build string based on sql_id

        'if OSS is checked, add all oss parameters in tree

        Dim parray()() As String = Nothing
        Dim strOSS As String = ""
        Dim sql_ossParams As String = ""

        'objfrmTechnology = Nothing
        'frmMDI.OpenTechFormDynamically(Tech_Current, objFrmTechnology, True)
        'load table based on tech
        Select Case Tech_Current.ToUpper
            Case networkAll.Network3G1.ToUpper
                Dim ptemp()() As String = {New String() {"@cellid", "IN (" & Objects_Current & ")"},
                   New String() {"@columns", ParamQueryString},
                    New String() {"@mo_table", db_table_name}}

                parray = ptemp
                strOSS = GetSQL(1029, parray)(0)
                sql_ossParams = GetSQL(1029, parray)(1)
            Case networkAll.Network2G1.ToUpper
                'no parameters availabe
                Dim ptemp()() As String = {New String() {"@cellid", "IN (" & Objects_Current & ")"},
                    New String() {"@columns", ParamQueryString},
                 New String() {"@mo_table", db_table_name}}
                parray = ptemp
                strOSS = GetSQL(1031, parray)(0)
                sql_ossParams = GetSQL(1031, parray)(1)
            Case networkAll.Network2G2.ToUpper
                'no parameters availabe
                Dim ptemp()() As String = {New String() {"@cellid", "IN (" & Objects_Current & ")"},
                    New String() {"@columns", ParamQueryString},
                 New String() {"@mo_table", db_table_name}}
                parray = ptemp

                strOSS = GetSQL(1513, parray)(0)
                sql_ossParams = GetSQL(1513, parray)(1)
            Case networkAll.Network2G3.ToUpper
                'no parameters availabe
                Dim ptemp()() As String = {New String() {"@cellid", "IN (" & Objects_Current & ")"},
                    New String() {"@columns", ParamQueryString},
                 New String() {"@mo_table", db_table_name}}
                parray = ptemp

                strOSS = GetSQL(20010, parray)(0)
                sql_ossParams = GetSQL(20010, parray)(1)
            Case networkAll.Network3G2.ToUpper
                'no parameters availabe
                Dim ptemp()() As String = {New String() {"@cellid", "IN (" & Objects_Current & ")"},
                    New String() {"@columns", ParamQueryString},
                 New String() {"@mo_table", db_table_name}}
                parray = ptemp
                strOSS = GetSQL(9511, parray)(0)
                sql_ossParams = GetSQL(9511, parray)(1)
            Case networkAll.Network3G3.ToUpper
                'no parameters availabe
                Dim ptemp()() As String = {New String() {"@cellid", "IN (" & Objects_Current & ")"},
                    New String() {"@columns", ParamQueryString},
                 New String() {"@mo_table", db_table_name}}
                parray = ptemp
                strOSS = GetSQL(30010, parray)(0)
                sql_ossParams = GetSQL(30010, parray)(1)
            Case networkAll.Network4G1.ToUpper
                'no parameters availabe
                Dim ptemp()() As String = {New String() {"@cellid", "IN (" & Objects_Current & ")"},
                    New String() {"@columns", ParamQueryString},
                 New String() {"@mo_table", db_table_name}}
                parray = ptemp
                strOSS = GetSQL(10010, parray)(0)
                sql_ossParams = GetSQL(10010, parray)(1)
            Case networkAll.Network4G2.ToUpper
                'no parameters availabe
                Dim ptemp()() As String = {New String() {"@cellid", "IN (" & Objects_Current & ")"},
                    New String() {"@columns", ParamQueryString},
                 New String() {"@mo_table", db_table_name}}
                parray = ptemp
                strOSS = GetSQL(15010, parray)(0)
                sql_ossParams = GetSQL(15010, parray)(1)
            Case networkAll.Network4G3.ToUpper
                'no parameters availabe
                Dim ptemp()() As String = {New String() {"@cellid", "IN (" & Objects_Current & ")"},
                    New String() {"@columns", ParamQueryString},
                 New String() {"@mo_table", db_table_name}}
                parray = ptemp
                strOSS = GetSQL(17010, parray)(0)
                sql_ossParams = GetSQL(17010, parray)(1)
            Case networkAll.Network5G1.ToUpper
                'no parameters availabe
                Dim ptemp()() As String = {New String() {"@cellid", "IN (" & Objects_Current & ")"},
                    New String() {"@columns", ParamQueryString},
                    New String() {"@mo_table", db_table_name}}
                parray = ptemp
                strOSS = GetSQL(51010, parray)(0)
                sql_ossParams = GetSQL(51010, parray)(1)
            Case networkAll.Network5G2.ToUpper
                'no parameters availabe
                Dim ptemp()() As String = {New String() {"@cellid", "IN (" & Objects_Current & ")"},
                    New String() {"@columns", ParamQueryString},
                 New String() {"@mo_table", db_table_name}}
                parray = ptemp
                strOSS = GetSQL(52010, parray)(0)
                sql_ossParams = GetSQL(52010, parray)(1)
            Case networkAll.Network5G3.ToUpper
                'no parameters availabe
                Dim ptemp()() As String = {New String() {"@cellid", "IN (" & Objects_Current & ")"},
                    New String() {"@columns", ParamQueryString},
                 New String() {"@mo_table", db_table_name}}
                parray = ptemp
                strOSS = GetSQL(53010, parray)(0)
                sql_ossParams = GetSQL(53010, parray)(1)
        End Select

        If Not sql_ossParams.Contains("@motable") Then
            sql_ossParams = Replace(sql_ossParams, "mo.", db_table_name + ".")
        End If
        'fire
        Dim dtParams_Cell As System.Data.DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strOSS, sql_ossParams)
        Return dtParams_Cell
    End Function

    Private Function TreeListView_Search(ByVal str As String, ByVal nodeindex As Integer) As TreeListViewNode
        Dim tn As TreeListViewNode = tlvDetail.FindNode(str, False, nodeindex, True)
        Return tn
    End Function

    Public Sub ListView_ColumnResize()
        For Each col As TreeListViewColumn In tlvSelection.Columns
            tlvSelection.AutoSizeColumn(col)
        Next
    End Sub

    Public Sub TreeListView_BuildFromMapSelection_New2(ByVal objects As String, ByVal tech As String)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Started Tech Current:" & tech)
        Dim tbl2search As MapInfo.Data.Table
        Dim ftr As MapInfo.Data.Feature
        Dim i As Integer = 0
        Tech_Current = tech
        Objects_Current = objects
        Try
            If tlvSelection.Nodes.Count = 0 Then
                cellsfilter.Clear()
            End If
            Dim k As Integer = 0
            If tlvSelection.Nodes.Count <> 0 And tlvSelection.Columns(0).HeaderText & tlvSelection.Columns(1).HeaderText = "Cell ID" & "Tech" Then
                'New Method: Treeviewlist
                '+++++++++++++++++++++++++
                '  cellsfilter.Clear()
                tlvDetail.Nodes.Clear()
                tlvDetail.Columns.Clear()
                Try

                    For Each lvitem2 As TreeListViewNode In tlvSelection.Nodes
                        If TreeViewList_CellExists(lvitem2.SubItems(0).Text.ToString) = False Then
                            tbl2search = MapInfo.Engine.Session.Current.Catalog.GetTable(lvitem2.SubItems(3).Text.ToString.Trim)

                            Dim uniquecellfield As String = frmMapWindow.Layer_Column2Tree(tbl2search.Alias)
                            If frmMapWindow.Layer_Column2Tree(tbl2search.Alias) Is Nothing Then
                                uniquecellfield = "CELLID"
                            End If

                            'searching for feature for selected item
                            ftr = MapInfo.Engine.Session.Current.Catalog.SearchForFeature(tbl2search, MapInfo.Data.SearchInfoFactory.SearchWhere(uniquecellfield & "=" & Chr(39) & lvitem2.Text & Chr(39)))
                            tlvDetail.SuspendLayout()

                            'adding all parameters that are part of feature set downloaded from server
                            Dim column As TreeListViewColumn
                            If tlvDetail.Nodes.Count = 0 Then
                                'adding columns for tree
                                column = New TreeListViewColumn("Parameter", "")
                                tlvDetail.Columns.Add(column)


                                'adding nodes in first column
                                Dim parentnode As TreeListViewNode = New TreeListViewNode("Physical")
                                parentnode.Key = "Physical"
                                tlvDetail.Nodes.Add(parentnode)

                                'add 2e column
                                column = New TreeListViewColumn(lvitem2.SubItems(0).Text.ToString, "")

                            Else
                                'add 3e or more column
                                k = 1
                                column = New TreeListViewColumn(lvitem2.SubItems(0).Text.ToString, "")

                            End If
                            tlvDetail.Columns.Add(column)

                            'objFrmTechnology = Nothing
                            'frmMDI.OpenTechFormDynamically(tech, objFrmTechnology, True)
                            If tech IsNot Nothing Then
                                Select Case tech.ToUpper
                                    Case networkAll.Network3G1.ToUpper
                                        cellsfilter.Add("WCEL_CELL_ID = " & Chr(39) & column.HeaderText & Chr(39))
                                    Case networkAll.Network2G1.ToUpper
                                        cellsfilter.Add("CELL_ID = " & Chr(39) & column.HeaderText & Chr(39))
                                    Case networkAll.Network2G2.ToUpper
                                        cellsfilter.Add("NameObject = " & Chr(39) & column.HeaderText & Chr(39))
                                    Case networkAll.Network2G3.ToUpper
                                        cellsfilter.Add("CELL_ID = " & Chr(39) & column.HeaderText & Chr(39))
                                    Case networkAll.Network3G2.ToUpper
                                        cellsfilter.Add("CellId = " & Chr(39) & column.HeaderText & Chr(39))
                                    Case networkAll.Network3G3.ToUpper
                                        cellsfilter.Add("CellId = " & Chr(39) & column.HeaderText & Chr(39))
                                    Case networkAll.Network4G1.ToUpper
                                        cellsfilter.Add("eCellId = " & Chr(39) & column.HeaderText & Chr(39))
                                    Case networkAll.Network4G2.ToUpper
                                        cellsfilter.Add("eCellId = " & Chr(39) & column.HeaderText & Chr(39))
                                    Case networkAll.Network4G3.ToUpper
                                        cellsfilter.Add("eCellId = " & Chr(39) & column.HeaderText & Chr(39))
                                    Case networkAll.Network5G1.ToUpper
                                        cellsfilter.Add("eCellId = " & Chr(39) & column.HeaderText & Chr(39))
                                    Case networkAll.Network5G2.ToUpper
                                        cellsfilter.Add("eCellId = " & Chr(39) & column.HeaderText & Chr(39))
                                    Case networkAll.Network5G3.ToUpper
                                        cellsfilter.Add("eCellId = " & Chr(39) & column.HeaderText & Chr(39))
                                End Select
                            End If

                            Dim l As Integer = 0
                            For j = 0 To ftr.Columns.Count - 1
                                If ftr.Columns(j).DataType <> MIDbType.FeatureGeometry And ftr.Columns(j).DataType <> MIDbType.Style Then

                                    If k = 0 Then
                                        Dim newnode As TreeListViewNode = New TreeListViewNode(ftr.Columns(j).Alias)
                                        newnode.Key = ftr.Columns(j).Alias
                                        Dim newsubitem As TreeListViewSubItem = New TreeListViewSubItem
                                        newsubitem.Text = ftr.Columns(j).Alias
                                        newnode.SubItems.Add(newsubitem)
                                        newsubitem = New TreeListViewSubItem
                                        newsubitem.Text = ftr.Item(j).ToString
                                        newnode.SubItems.Add(newsubitem)
                                        tlvDetail.Nodes(0).Nodes.Add(newnode)
                                    Else
                                        Dim newsubitem As TreeListViewSubItem = New TreeListViewSubItem
                                        newsubitem.Text = ftr.Item(j).ToString
                                        tlvDetail.Nodes(0).Nodes(j - l).SubItems.Add(newsubitem)
                                    End If
                                Else
                                    l = l + 1
                                End If
                            Next j
                        End If
                    Next
                Catch ex As Exception
                    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                End Try

                'LIVE OSS QUERY
                If dt_Templates Is Nothing Then
                    If Not tech Is Nothing Then
                        Dim sql As String = "SELECT * FROM dbo.IOS_Parameters_Templates "
                        dt_Templates = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, sql)
                    End If
                End If

                If tech = "NanoBTS" Then
                    dt_Templates.DefaultView.RowFilter = "TemplateTech='2G' AND TemplateVendor='IPAccess_NanoBTS'"

                ElseIf tech = "Nano3G" Then
                    dt_Templates.DefaultView.RowFilter = "TemplateTech='3G' AND TemplateVendor='IPAccess_Nano3G'"
                Else
                    dt_Templates.DefaultView.RowFilter = "TemplateTech=" & Chr(39) & tech & Chr(39) ' & " AND NOT TemplateVendor LIKE 'IPAccess*'"
                End If

                Dim test As Integer = dt_Templates.DefaultView.ToTable.Select("TemplateName=" & Chr(39) & cmbTemplate.Text & Chr(39)).Count
                If cmbTemplate.Text = "" Or test = 0 Then
                    BindDevExComboBoxWithValueMember(cmbTemplate, dt_Templates.DefaultView.ToTable, "TemplateId", "TemplateName")
                    cmbTemplate.SelectedItem = cmbTemplate.Properties.Items(0)
                    If cmbTemplate.Text = "" Then Exit Sub
                End If

                Try
                    'BUILDING QUERY FOR OSS
                    Dim TemplateParamString As New StringBuilder()
                    Dim reload As Boolean = False

                    If dt_OSS_UserParams Is Nothing Then
                        reload = True
                    ElseIf dt_OSS_UserParams(0)("TemplateTech").ToString.ToUpper <> tech.ToUpper Or dt_OSS_UserParams(0)("TemplateID") <> TryCast(cmbTemplate.SelectedItem, IOS.Library.clsComboBoxItem).Value Then
                        reload = True
                        If dt_OSS_UserParams(0)("TemplateTech").ToString.ToUpper <> tech.ToUpper Then
                            Try
                                RemoveHandler cmbTemplate.SelectedValueChanged, AddressOf cmbTemplate_SelectedValueChanged
                            Catch
                            End Try

                            If tech = "NanoBTS" Then
                                dt_Templates.DefaultView.RowFilter = "TemplateTech='2G' AND TemplateVendor='IPAccess_NanoBTS'"
                            ElseIf tech = "Nano3G" Then
                                dt_Templates.DefaultView.RowFilter = "TemplateTech='3G' AND TemplateVendor='IPAccess_Nano3G'"
                            Else
                                dt_Templates.DefaultView.RowFilter = "TemplateTech=" & Chr(39) & tech & Chr(39)
                            End If
                            BindDevExComboBoxWithValueMember(cmbTemplate, dt_Templates.DefaultView.ToTable, "TemplateId", "TemplateName")
                            cmbTemplate.SelectedItem = cmbTemplate.Properties.Items(0)
                        End If
                    End If

                    If reload = True Then
                        ''Dim sql_OSS_UserParams As String = "SELECT * FROM dbo.qry_IOS_Parameter_Template WHERE TemplateID = " & TryCast(cmbTemplate.SelectedItem, IOS.Library.clsComboBoxItem).Value & " ORDER BY GROUP_NAME ASC, OBJECT ASC, PARAM ASC"
                        dt_OSS_UserParams = IOS.DataLibrary.clsSQLCommands.GetMapSelectionnOSSUserParams(connStrIOSServer, TryCast(cmbTemplate.SelectedItem, IOS.Library.clsComboBoxItem).Value)
                    End If
                    If Not dt_OSS_UserParams Is Nothing Then
                        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "dt_OSS_UserParams:" & dt_OSS_UserParams.Rows.Count)
                    Else
                        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "dt_OSS_UserParams:null")
                    End If

                    'start construction of tree
                    Dim l1 As String = ""
                    Dim l2 As String = ""
                    Dim l3 As String = ""
                    Dim catnode As TreeListViewNode = Nothing
                    Dim objnode As TreeListViewNode = Nothing
                    Dim paramnode As TreeListViewNode = Nothing

                    If tlvDetail.Nodes.Count < 2 Then 'create the nodes 
                        'level 0
                        Dim parentnode As TreeListViewNode
                        parentnode = New TreeListViewNode(dt_OSS_UserParams(0)(0).ToString)
                        parentnode.Key = dt_OSS_UserParams(0)(0).ToString
                        tlvDetail.Nodes.Add(parentnode)

                        For Each drow As DataRow In dt_OSS_UserParams.Rows
                            'level 1
                            If l1 <> drow("GROUP_NAME").ToString.Trim Then
                                l1 = drow("GROUP_NAME").ToString.Trim
                                catnode = New TreeListViewNode(l1)
                                catnode.Key = l1
                                parentnode.Nodes.Add(catnode)
                            End If

                            'level 2
                            If l2 <> l1 & "_" & drow("OBJECT").ToString.Trim Then
                                l2 = l1 & "_" & drow("OBJECT").ToString.Trim
                                objnode = New TreeListViewNode(drow("OBJECT").ToString.Trim)
                                objnode.Key = drow("OBJECT").ToString.Trim
                                objnode.Tag = "MO"
                                catnode.Nodes.Add(objnode)
                            End If

                            'level3
                            If l3 <> drow("PARAM").ToString.Trim Then
                                l3 = drow("PARAM").ToString.Trim
                                paramnode = New TreeListViewNode(l3)
                                paramnode.Key = l3
                                Dim si As TreeListViewSubItem = New TreeListViewSubItem
                                si.Text = l3
                                paramnode.SubItems.Add(si)
                                si = New TreeListViewSubItem
                                si.Text = ""
                                paramnode.SubItems.Add(si)
                                paramnode.Visible = True
                                objnode.Nodes.Add(paramnode)
                            End If
                        Next
                    ElseIf tlvSelection.Columns.Count > 1 Then
                        '   UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "start UpdateParamtersOfExpandedMO")
                        UpdateParamtersOfExpandedMO(tlvDetail.SelectedNode)
                    End If
                Catch ex As Exception
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
                    ParentExpand(tlvDetail.Nodes(0))
                    tlvDetail.Nodes(0).Selected = True
                    tlvDetail.EnsureVisible(tlvDetail.Nodes(0), LidorSystems.IntegralUI.VerticalAlignment.Center)
                End Try
            ElseIf tlvSelection.Nodes.Count <> 0 And tlvSelection.Columns(0).HeaderText & tlvSelection.Columns(1).HeaderText = "Link Name" & "Tech" Then
                tlvDetail.Nodes.Clear()
                tlvDetail.Columns.Clear()
                Try

                    For Each lvitem2 As TreeListViewNode In tlvSelection.Nodes
                        If TreeViewList_CellExists(lvitem2.SubItems(0).Text.ToString) = False Then
                            tbl2search = MapInfo.Engine.Session.Current.Catalog.GetTable(lvitem2.SubItems(3).Text.ToString.Trim)

                            Dim uniquecellfield As String = frmMapWindow.Layer_Column2Tree(tbl2search.Alias)
                            If frmMapWindow.Layer_Column2Tree(tbl2search.Alias) Is Nothing Then
                                uniquecellfield = "LINKNAME"
                            End If

                            'searching for feature for selected item
                            ftr = MapInfo.Engine.Session.Current.Catalog.SearchForFeature(tbl2search, MapInfo.Data.SearchInfoFactory.SearchWhere(uniquecellfield & "=" & Chr(39) & lvitem2.Text & Chr(39)))
                            tlvDetail.SuspendLayout()

                            'adding all parameters that are part of feature set downloaded from server
                            Dim column As TreeListViewColumn
                            If tlvDetail.Nodes.Count = 0 Then
                                'adding columns for tree
                                column = New TreeListViewColumn("Parameter", "")
                                tlvDetail.Columns.Add(column)


                                'adding nodes in first column
                                Dim parentnode As TreeListViewNode = New TreeListViewNode("Physical")
                                parentnode.Key = "Physical"
                                tlvDetail.Nodes.Add(parentnode)

                                'add 2e column
                                column = New TreeListViewColumn(lvitem2.SubItems(0).Text.ToString, "")

                            Else
                                'add 3e or more column
                                k = 1
                                column = New TreeListViewColumn(lvitem2.SubItems(0).Text.ToString, "")

                            End If
                            tlvDetail.Columns.Add(column)

                            'objFrmTechnology = Nothing
                            'frmMDI.OpenTechFormDynamically(tech, objFrmTechnology, True)

                            Select Case tech.ToUpper
                                Case networkAll.Network3G1.ToUpper
                                    cellsfilter.Add("WCEL_CELL_ID = " & Chr(39) & column.HeaderText & Chr(39))
                                Case networkAll.Network2G1.ToUpper
                                    cellsfilter.Add("CELL_ID = " & Chr(39) & column.HeaderText & Chr(39))
                                Case networkAll.Network2G2.ToUpper
                                    cellsfilter.Add("NameObject = " & Chr(39) & column.HeaderText & Chr(39))
                                Case networkAll.Network2G3.ToUpper
                                    cellsfilter.Add("CELL_ID = " & Chr(39) & column.HeaderText & Chr(39))
                                Case networkAll.Network3G2.ToUpper
                                    cellsfilter.Add("CellId = " & Chr(39) & column.HeaderText & Chr(39))
                                Case networkAll.Network3G3.ToUpper
                                    cellsfilter.Add("CellId = " & Chr(39) & column.HeaderText & Chr(39))
                                Case networkAll.Network4G1.ToUpper
                                    cellsfilter.Add("eCellId = " & Chr(39) & column.HeaderText & Chr(39))
                                Case networkAll.Network4G2.ToUpper
                                    cellsfilter.Add("eCellId = " & Chr(39) & column.HeaderText & Chr(39))
                                Case networkAll.Network4G3.ToUpper
                                    cellsfilter.Add("eCellId = " & Chr(39) & column.HeaderText & Chr(39))
                                Case networkAll.Network5G1.ToUpper
                                    cellsfilter.Add("eCellId = " & Chr(39) & column.HeaderText & Chr(39))
                                Case networkAll.Network5G2.ToUpper
                                    cellsfilter.Add("eCellId = " & Chr(39) & column.HeaderText & Chr(39))
                                Case networkAll.Network5G3.ToUpper
                                    cellsfilter.Add("eCellId = " & Chr(39) & column.HeaderText & Chr(39))
                                Case networkAll.NetworkTX.ToUpper
                                    cellsfilter.Add("LinkName = " & Chr(39) & column.HeaderText & Chr(39))
                                Case networkAll.NetworkTransport.ToUpper
                                    cellsfilter.Add("eCellId = " & Chr(39) & column.HeaderText & Chr(39))
                                Case networkAll.NetworkPDUM.ToUpper
                                    cellsfilter.Add("eCellId = " & Chr(39) & column.HeaderText & Chr(39))
                                Case networkAll.NetworkTWAMP.ToUpper
                                    cellsfilter.Add("eCellId = " & Chr(39) & column.HeaderText & Chr(39))
                                Case networkAll.NetworkHLR.ToUpper
                                    cellsfilter.Add("eCellId = " & Chr(39) & column.HeaderText & Chr(39))
                                Case networkAll.NetworkDWDM.ToUpper
                                    cellsfilter.Add("eCellId = " & Chr(39) & column.HeaderText & Chr(39))
                            End Select

                            Dim l As Integer = 0
                            For j = 0 To ftr.Columns.Count - 1
                                If ftr.Columns(j).DataType <> MIDbType.FeatureGeometry And ftr.Columns(j).DataType <> MIDbType.Style Then

                                    If k = 0 Then
                                        Dim newnode As TreeListViewNode = New TreeListViewNode(ftr.Columns(j).Alias)
                                        newnode.Key = ftr.Columns(j).Alias
                                        Dim newsubitem As TreeListViewSubItem = New TreeListViewSubItem
                                        newsubitem.Text = ftr.Columns(j).Alias
                                        newnode.SubItems.Add(newsubitem)
                                        newsubitem = New TreeListViewSubItem
                                        newsubitem.Text = ftr.Item(j).ToString
                                        newnode.SubItems.Add(newsubitem)
                                        tlvDetail.Nodes(0).Nodes.Add(newnode)
                                    Else
                                        Dim newsubitem As TreeListViewSubItem = New TreeListViewSubItem
                                        newsubitem.Text = ftr.Item(j).ToString
                                        tlvDetail.Nodes(0).Nodes(j - l).SubItems.Add(newsubitem)
                                    End If
                                Else
                                    l = l + 1
                                End If
                            Next j
                        End If
                    Next
                Catch ex As Exception
                    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                End Try
            Else
                tlvDetail.Nodes.Clear()
                tlvDetail.Columns.Clear()
            End If
            '    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "start LoadExtensionRootNodes")
            LoadExtensionRootNodes()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        Finally
            tlvDetail.UpdateCurrentView()
            For Each col As TreeListViewColumn In tlvDetail.Columns
                tlvDetail.AutoSizeColumn(col)
            Next
            If tlvDetail.Columns.Count > 0 Then
                tlvDetail.Columns(0).Width = tlvDetail.Columns(0).Width + 10
            End If
            tlvDetail.UpdateLayout()
        End Try
    End Sub

    Private Sub LoadExtensionRootNodes()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        Dim node As TreeListViewNode = Nothing
        Dim subnode As TreeListViewNode = Nothing

        strConnection = GetSQL(1002, parray)(0)
        sqlParam = GetSQL(1002, parray)(1)

        dtExtnNodes = New DataTable
        dtExtnNodes = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        Dim nd As TreeListViewNode = Nothing
        If dtExtnNodes IsNot Nothing Then
            Dim dtExtn As DataTable = dtExtnNodes.DefaultView.ToTable(True, {"GUIName", "DbTableName"})
            For Each dr As DataRow In dtExtn.Rows
                node = New TreeListViewNode(dr("GUIName"))
                node.Text = dr("GUIName").ToString()
                node.Key = dr("DbTableName")
                node.Tag = "ExtnNode"
                tlvDetail.Nodes.Add(node)

                'Add a dummy node to allow expansion of the extension rootnode.
                For Each dRow As DataRow In dtExtnNodes.Select("DbTableName='" & dr("DbTableName") & "'")
                    nd = New TreeListViewNode(dRow("FieldToFilter"))
                    nd.Key = dRow("FieldToFilter")
                    nd.Tag = dRow("FieldToFilter")
                    node.Nodes.Add(nd)
                Next
            Next
        End If
    End Sub

#End Region

#Region "Form & Control Events"

    Private Sub frmSelectionInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LblMsg.Text = ""
        Me.SuspendLayout()
        Dim objstr As String = Nothing
        Dim tech As String = Nothing
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            cmbTemplate.Text = ""
            If tlvSelection.Columns.Count <> 0 Then
                If tlvSelection.Columns(0).HeaderText & tlvSelection.Columns(1).HeaderText = "Cell ID" & "Tech" Then
                    For Each lvitem As TreeListViewNode In tlvSelection.Nodes
                        objstr = objstr & Chr(39) & lvitem.SubItems(0).Text & Chr(39) & ","
                        tech = lvitem.SubItems(1).Text
                        If lvitem.SubItems(3).Text.Contains("NanoBTS") Then
                            tech = "NanoBTS"
                        ElseIf lvitem.SubItems(3).Text.Contains("Nano3G") Then
                            tech = "Nano3G"
                        End If
                    Next
                    If Not objstr Is Nothing Then
                        objstr = objstr.TrimEnd(",")
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try

        Try
            If Not tech Is Nothing Then
                dt_Templates = IOS.DataLibrary.clsSQLCommands.GetParameterTemplates(connStrIOSServer)
                If tech = "NanoBTS" Then
                    dt_Templates.DefaultView.RowFilter = "TemplateTech='2G' AND TemplateVendor='IPAccess_NanoBTS'"
                ElseIf tech = "Nano3G" Then
                    dt_Templates.DefaultView.RowFilter = "TemplateTech='3G' AND TemplateVendor='IPAccess_Nano3G'"
                Else
                    dt_Templates.DefaultView.RowFilter = "TemplateTech=" & Chr(39) & tech & Chr(39)
                End If

                BindDevExComboBoxWithValueMember(cmbTemplate, dt_Templates.DefaultView.ToTable, "TemplateId", "TemplateName", "Select Item..")
                If cmbTemplate.Properties.Items.Count = 0 Then
                    cmbTemplate.Text = "No Templates"
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try

        AddHandler frmMapWindow.MapControl1.LocationChanged, AddressOf RelocateForm
        Try
            If tlvSelection.Columns.Count <> 0 Then
                Tech_Current = tech
                Objects_Current = objstr
                TreeListView_BuildFromMapSelection_New2(objstr, tech)
            End If
        Catch ex As Exception
            MsgBox(ex)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        Me.ResumeLayout()
        ConfigurMappingSelectionForm("dlgMappingSelection")
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub frm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        frmMapWindow.tlb_SelInfo.Checked = False
    End Sub

    Private Sub frm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            RemoveHandler cmbTemplate.SelectedValueChanged, AddressOf cmbTemplate_SelectedValueChanged
        Catch
        End Try
    End Sub

    Private Sub cmbTemplate_MouseClick(sender As Object, e As MouseEventArgs) Handles cmbTemplate.MouseClick
        AddHandler cmbTemplate.SelectedValueChanged, AddressOf cmbTemplate_SelectedValueChanged
    End Sub

    Private Sub cmbTemplate_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objstr As String = Nothing
        Dim tech As String = Nothing

        Try
            RemoveHandler cmbTemplate.SelectedValueChanged, AddressOf cmbTemplate_SelectedValueChanged
            TemplateSettingSelectionWindow = TryCast(cmbTemplate.SelectedItem, IOS.Library.clsComboBoxItem).Value
            tlvDetail.Nodes.Clear()
            tlvDetail.Columns.Clear()

            dt_OSS_UserParams = Nothing
            ParamQueryString = Nothing

            If tlvSelection.Columns.Count <> 0 Then
                If tlvSelection.Columns(0).HeaderText & tlvSelection.Columns(1).HeaderText = "Cell ID" & "Tech" Then
                    For Each lvitem As TreeListViewNode In tlvSelection.Nodes
                        objstr = objstr & Chr(39) & lvitem.SubItems(0).Text & Chr(39) & ","
                        tech = lvitem.SubItems(1).Text
                        If lvitem.SubItems(3).Text.Contains("NanoBTS") Then
                            tech = "NanoBTS"
                        End If
                    Next
                    If Not objstr Is Nothing Then
                        objstr = objstr.TrimEnd(",")
                    End If
                Else
                    If Not objstr Is Nothing Then
                    End If
                End If
            End If

            If tlvSelection.Columns.Count <> 0 Then
                TreeListView_BuildFromMapSelection_New2(objstr, tech)
            End If
        Catch ex As Exception
            'MsgBox(ex)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tlvDetail_AfterCollapse(sender As Object, e As LidorSystems.IntegralUI.ObjectEventArgs) Handles tlvDetail.AfterCollapse
        For Each col As TreeListViewColumn In tlvDetail.Columns
            tlvDetail.AutoSizeColumn(col)
        Next
    End Sub

    Private Sub tlvDetail_AfterExpand(sender As Object, e As LidorSystems.IntegralUI.ObjectEventArgs) Handles tlvDetail.AfterExpand
        Try
            Dim selectedNode As TreeListViewNode = CType(e.Object, TreeListViewNode)
            If (selectedNode IsNot Nothing) Then
                If selectedNode.GetType.ToString = GetType(TreeListViewNode).ToString Then
                    If (selectedNode.Level = 2) Then
                        If (ceShowOnlyDifference.Checked) Then
                            Dim disValues As List(Of String) = New List(Of String)()
                            For Each tvMOSubNode As TreeListViewNode In selectedNode.Nodes
                                disValues.Clear()
                                For Each tvMOSubSubNode As TreeListViewSubItem In tvMOSubNode.SubItems
                                    If tvMOSubSubNode.Index > 0 Then
                                        disValues.Add(tvMOSubSubNode.Text)
                                    End If
                                Next
                                If (Not disValues.Distinct().Count > 1) Then
                                    tvMOSubNode.Visible = False
                                End If
                            Next
                            selectedNode.Expand()
                        End If
                    End If
                End If
            End If
            tlvDetail.UpdateCurrentView()

            For Each col As TreeListViewColumn In tlvDetail.Columns
                tlvDetail.AutoSizeColumn(col)
            Next
            tlvDetail.Columns(0).Width = tlvDetail.Columns(0).Width + 10
            tlvDetail.UpdateLayout()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tlvDetail_ItemSelectionChanged(sender As Object, e As LidorSystems.IntegralUI.ObjectEventArgs) Handles tlvDetail.ItemSelectionChanged
        Try
            '   UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Started")
            LblMsg.Text = ""
            Dim selectedNode As TreeListViewNode = CType(e.Object, TreeListViewNode)
            If e.Object.GetType.ToString = GetType(TreeListViewNode).ToString Then
                If (selectedNode.Level = 2) Then
                    If (ceShowOnlyDifference.Checked) Then
                        If selectedNode.IsSelected And Not selectedNode.Tag Is Nothing Then
                            UpdateParamtersOfExpandedMO(selectedNode)
                            tlvDetail.UpdateCurrentView()

                            Dim disValues As List(Of String) = New List(Of String)()
                            For Each tvMOSubNode As TreeListViewNode In selectedNode.Nodes
                                disValues.Clear()
                                For Each tvMOSubSubNode As TreeListViewSubItem In tvMOSubNode.SubItems
                                    If tvMOSubSubNode.Index > 0 Then
                                        disValues.Add(tvMOSubSubNode.Text)
                                    End If
                                Next
                                If (Not disValues.Distinct().Count > 1) Then
                                    tvMOSubNode.Visible = False
                                End If
                            Next
                        End If
                    Else
                        If selectedNode.IsSelected And Not selectedNode.Tag Is Nothing Then
                            UpdateParamtersOfExpandedMO(e.Object)
                            tlvDetail.UpdateCurrentView()
                        End If
                    End If
                    For Each col As TreeListViewColumn In tlvDetail.Columns
                        tlvDetail.AutoSizeColumn(col)
                    Next
                    tlvDetail.Columns(0).Width = tlvDetail.Columns(0).Width + 10
                    tlvDetail.UpdateLayout()
                ElseIf selectedNode.Tag = "ExtnNode" AndAlso selectedNode.Level = 0 Then
                    If Not selectedNode.IsExpanded Then
                        UpdateExtensionNodes(selectedNode)
                        tlvDetail.UpdateCurrentView()
                    End If
                Else
                    If (ceShowOnlyDifference.Checked) Then
                        LblMsg.Text = "Select level 2 node"
                        Exit Sub
                    End If
                End If
            End If
            ' UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub txtSelInfoSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSelInfoSearch.KeyDown
        Try
            Dim tn As TreeListViewNode = tlvDetail.SelectedNode
            If tn Is Nothing Then
                tn = tlvDetail.Nodes(0)
                tn.Selected = True
                tn.EnsureVisible(LidorSystems.IntegralUI.VerticalAlignment.Top)
            End If

            If e.KeyCode = Keys.Enter Then
                Dim tn_next As TreeListViewNode = Nothing
                Try
                    For Each tlvnd As TreeListViewNode In tlvDetail.Nodes
                        tn_next = TreeListView_SearchWildCard(txtSelInfoSearch.Text.Trim, tlvnd, tn.FlatIndex)
                        If Not tn_next Is Nothing Then
                            Exit For
                        End If
                    Next
                Catch
                End Try
                If tn_next Is Nothing Then
                    tlvDetail.SelectedNode = tn
                Else
                    tlvDetail.SelectedNode = tn_next
                    ParentExpand(tn)
                    tn_next.Selected = True
                    tlvDetail.EnsureVisible(tn_next, LidorSystems.IntegralUI.VerticalAlignment.Center)
                End If
            Else
                Dim tn_next As TreeListViewNode = Nothing
                For Each tlvnd As TreeListViewNode In tlvDetail.Nodes
                    tn_next = TreeListView_SearchWildCard(txtSelInfoSearch.Text.Trim, tlvnd, 0)
                    If Not tn_next Is Nothing Then
                        Exit For
                    End If
                Next
                If Not tn_next Is Nothing Then
                    tlvDetail.SelectedNode = tn_next
                    ParentExpand(tn)
                    tn_next.Selected = True
                    tlvDetail.EnsureVisible(tn_next, LidorSystems.IntegralUI.VerticalAlignment.Center)
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub txtSelInfoSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSelInfoSearch.TextChanged
        If (txtSelInfoSearch.Text.Trim.Length >= 1) Then
            Dim tn As TreeListViewNode = TreeListView_Search(txtSelInfoSearch.Text.Trim, 0)
            If Not tn Is Nothing Then
                If (tn.Level > 1) Then
                    UpdateParamtersOfExpandedMO(tn.Parent())
                    tn.Selected = True
                    tlvDetail.EnsureVisible(tn, LidorSystems.IntegralUI.VerticalAlignment.Center)
                End If
            End If
        Else
            tlvDetail.CollapseAll()
        End If
    End Sub

    Private Sub ceShowOnlyDifference_CheckedChanged(sender As Object, e As EventArgs) Handles ceShowOnlyDifference.CheckedChanged
        Try
            LblMsg.Text = ""
            Dim objectCurrent As String = IIf(Objects_Current IsNot Nothing, String.Empty, Objects_Current)
            If (ceShowOnlyDifference.Checked) Then
                If (tlvDetail.SelectedNode IsNot Nothing) Then
                    If (tlvDetail.SelectedNode.Level = 2) Then
                        If (tlvDetail.SelectedNode.Tag IsNot Nothing) Then
                            Dim disValues As List(Of String) = New List(Of String)()
                            For Each tvMOSubNode As TreeListViewNode In tlvDetail.SelectedNode.Nodes
                                disValues.Clear()
                                For Each tvMOSubSubNode As TreeListViewSubItem In tvMOSubNode.SubItems
                                    If tvMOSubSubNode.Index > 0 Then
                                        disValues.Add(tvMOSubSubNode.Text)
                                    End If
                                Next
                                If (Not disValues.Distinct().Count > 1) Then
                                    tvMOSubNode.Visible = False
                                End If
                            Next
                            tlvDetail.SelectedNode.Expand()
                        Else
                            For Each df As TreeListViewNode In tlvDetail.SelectedNode.Nodes
                                If (df.Level = 2 And df.Tag IsNot Nothing) Then
                                    GetMoNode(df)
                                    df.Parent.ExpandAll()
                                Else
                                    For Each df2 As TreeListViewNode In df.Nodes
                                        If (df2.Level = 2 And df.Tag IsNot Nothing) Then
                                            GetMoNode(df2)
                                        End If
                                    Next
                                End If
                            Next
                            tlvDetail.Nodes(tlvDetail.SelectedNode.ToString).ExpandAll()
                        End If
                    Else
                        LblMsg.Text = "Select level 2 node"
                        Exit Sub
                    End If
                End If
            Else
                If (Tech_Current IsNot Nothing) Then
                    If (Not objectCurrent = String.Empty) Then
                        TreeListView_BuildFromMapSelection_New2(Objects_Current, Tech_Current)
                    Else
                        If (Objects_Current IsNot Nothing) Then
                            TreeListView_BuildFromMapSelection_New2(Objects_Current, Tech_Current)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            tlvDetail.UpdateCurrentView()
            tlvDetail.UpdateLayout()
        End Try
    End Sub

    Public Sub lvSelection_SubItemSelectionChanged(sender As Object, e As EventArgs) Handles tlvSelection.SubItemSelectionChanged
        Try
            If tlvSelection.Columns(0).HeaderText <> "Key" Then
                Exit Sub
            End If
            tlvDetail.Nodes.Clear()
            tlvDetail.Columns.Clear()

            'search feature based on key and tablename
            Dim tbl2search As MapInfo.Data.Table = Nothing
            Dim lvitem2 As TreeListViewNode = tlvSelection.SelectedNode
            Dim ftr As MapInfo.Data.Feature
            Dim k As Integer = 0
            tbl2search = MapInfo.Engine.Session.Current.Catalog.GetTable(lvitem2.SubItems(2).Text.ToString.Trim)

            ftr = MapInfo.Engine.Session.Current.Catalog.SearchForFeature(tbl2search, MapInfo.Data.SearchInfoFactory.SearchWhere("MI_Key=" & Chr(39) & lvitem2.SubItems(0).Text & Chr(39)))
            tlvDetail.SuspendLayout()

            'create tree for feature

            If tlvSelection.Nodes.Count <> 0 Then

                'adding all parameters that are part of feature set downloaded from server
                If tlvDetail.Nodes.Count = 0 Then
                    'adding columns for tree
                    Dim column As TreeListViewColumn = New TreeListViewColumn("Parameter", "")
                    tlvDetail.Columns.Add(column)

                    'adding nodes in first column
                    'Dim parentnode As TreeListViewNode = New TreeListViewNode(lvitem2.Text)
                    'parentnode.Key = lvitem2.Text
                    'TreeListView1.Nodes.Add(parentnode)

                    'add 2e column
                    column = New TreeListViewColumn("Values", "") ')vitem2.SubItems(0).Text, "")
                    tlvDetail.Columns.Add(column)
                Else
                    'add 3e or more column
                    ' k = 1
                    ' Dim column As TreeListViewColumn = New TreeListViewColumn(lvitem2.SubItems(0).Text.ToString, "")
                    'TreeListView1.Columns.Add(column)
                End If

                Dim l As Integer = 0
                For j = 0 To ftr.Columns.Count - 1
                    If ftr.Columns(j).DataType <> MIDbType.FeatureGeometry And ftr.Columns(j).DataType <> MIDbType.Style Then

                        If k = 0 Then
                            Dim newnode As TreeListViewNode = New TreeListViewNode(ftr.Columns(j).Alias)
                            newnode.Key = ftr.Columns(j).Alias
                            Dim newsubitem As TreeListViewSubItem = New TreeListViewSubItem
                            newsubitem.Text = ftr.Columns(j).Alias
                            newnode.SubItems.Add(newsubitem)
                            newsubitem = New TreeListViewSubItem
                            newsubitem.Text = ftr.Item(j).ToString
                            newnode.SubItems.Add(newsubitem)
                            tlvDetail.Nodes.Add(newnode)
                        Else
                            Dim newsubitem As TreeListViewSubItem = New TreeListViewSubItem
                            newsubitem.Text = ftr.Item(j).ToString
                            tlvDetail.Nodes(j - l).SubItems.Add(newsubitem)
                        End If
                    Else
                        l = l + 1
                    End If
                Next j
            Else
                tlvDetail.Nodes.Clear()
                tlvDetail.Columns.Clear()
            End If
            tlvDetail.UpdateLayout()
            For Each col As TreeListViewColumn In tlvDetail.Columns
                tlvDetail.AutoSizeColumn(col)
            Next
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

#End Region

#Region "Context Menu"

    Private Sub cm_TLV_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cm_TLV.Opening
        Try
            Dim tn As TreeListViewNode = tlvDetail.SelectedNode
            tsmi_description.Enabled = False
            tsmi_mapping.Enabled = False
            tsmi_mapping_voronoi.Enabled = False
            tsmi_mapping_label.Enabled = False

            If Not tn Is Nothing Then
                If tn.SubItems.Count > 1 Then
                    If tn.SubItems(1).Text <> "" And tn.Level = 3 Then
                        tsmi_description.Enabled = True
                        tsmi_mapping.Enabled = True
                        tsmi_mapping_voronoi.Enabled = True
                        tsmi_mapping_label.Enabled = True
                        ' Exit Sub
                    ElseIf tn.Parent IsNot Nothing AndAlso tn.Parent.Text = "Physical" Then
                        tsmi_description.Enabled = False
                        tsmi_mapping.Enabled = True
                        tsmi_mapping_voronoi.Enabled = True
                        tsmi_mapping_label.Enabled = True
                        'Exit Sub
                    Else
                        tsmi_description.Enabled = False
                        tsmi_mapping.Enabled = False
                        tsmi_mapping_voronoi.Enabled = False
                        tsmi_mapping_label.Enabled = False
                        ' Exit Sub
                    End If
                End If
                If (tn.Level = 2) Then
                    'tsmi_CopyToClipboard_Value.Enabled = IIf(tn.Tag.Equals("MO"), True, False)
                    'tsmi_CopyToClipboard_HeaderValue.Enabled = IIf(tn.Tag.Equals("MO"), True, False)
                ElseIf (tn.Level = 3) Then
                    'tsmi_CopyToClipboard_Value.Enabled = IIf(tn.Parent.Tag.Equals("MO"), True, False)
                    'tsmi_CopyToClipboard_HeaderValue.Enabled = IIf(tn.Parent.Tag.Equals("MO"), True, False)
                Else
                    'tsmi_CopyToClipboard_Value.Enabled = False
                    'tsmi_CopyToClipboard_HeaderValue.Enabled = False
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub tsmi_description_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsmi_description.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")

            Dim objParamDesc As New frmParameterDescription()
            objParamDesc.moTblName = Nothing
            objParamDesc.paramName = tlvDetail.SelectedNode.SubItems(0).Text
            objParamDesc.moName = tlvDetail.SelectedNode.Parent.Text
            objParamDesc.fromLeft = Me.Left + Me.Width
            objParamDesc.fromTop = Me.Top
            objParamDesc.ShowDialog()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_mapping_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsmi_mapping.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            ParameterMapping("cells")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_CopyToClipboard_Value_Click(sender As Object, e As EventArgs)
        Clipboard.Clear()
        Dim clipBoradHeader As String = String.Empty
        Dim clipBoradText As String = String.Empty
        Dim clipBoradRow As String = String.Empty
        Dim isMONode As Boolean = False
        Dim isNeedHeaderCopy As Boolean = False
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If (tlvDetail.SelectedNode IsNot Nothing) Then
                Dim selectedNode As TreeListViewNode = tlvDetail.SelectedNode
                If (selectedNode.Tag Is Nothing) Then
                    If (selectedNode.Parent.Tag.Equals("MO")) Then
                        isMONode = True
                    End If
                Else
                    If (selectedNode.Tag.Equals("MO")) Then
                        isMONode = True
                        isNeedHeaderCopy = True
                    End If
                End If
                If (isMONode) Then
                    If (isNeedHeaderCopy) Then
                        clipBoradText = selectedNode.Text & ControlChars.NewLine
                        clipBoradHeader += "Parameter " & ControlChars.Tab
                        If tlvSelection.Nodes.Count <> 0 And tlvSelection.Columns(0).HeaderText & tlvSelection.Columns(1).HeaderText = "Cell ID" & "Tech" Then
                            For Each lIitem As TreeListViewNode In tlvSelection.Nodes
                                clipBoradHeader += lIitem.Text & ControlChars.Tab
                            Next
                        End If
                        clipBoradHeader += ControlChars.NewLine
                        clipBoradText += clipBoradHeader
                    End If

                    If (isNeedHeaderCopy) Then
                        For Each tlvnSelected As TreeListViewNode In selectedNode.Nodes
                            For Each tlvSubItem As TreeListViewSubItem In tlvnSelected.SubItems
                                clipBoradRow += tlvSubItem.Text & ControlChars.Tab
                            Next
                            clipBoradText += clipBoradRow & ControlChars.NewLine
                            clipBoradRow = String.Empty
                        Next

                    Else
                        For Each tlvSubItem As TreeListViewSubItem In selectedNode.SubItems
                            clipBoradRow += tlvSubItem.Text & ControlChars.Tab
                        Next
                        clipBoradText += clipBoradRow & ControlChars.NewLine
                        clipBoradRow = String.Empty

                    End If
                End If
                Clipboard.SetText(clipBoradText)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_mapping_voronoi_Click(sender As Object, e As EventArgs) Handles tsmi_mapping_voronoi.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            frmMapWindow.tlb_Voronoi.Checked = False
            frmMapWindow.tlb_Voronoi.PerformClick()
            ParameterMapping("voronoi")
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_mapping_label_Click(sender As Object, e As EventArgs) Handles tsmi_mapping_label.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            ParameterMapping("label")
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_Copy_Table_To_Clipboard_Click(sender As Object, e As EventArgs) Handles tsmi_Copy_Table_To_Clipboard.Click
        Clipboard.Clear()
        Dim clipBoardHeader As String = String.Empty
        Dim clipBoardText As String = String.Empty
        Dim isMONode As Boolean = False
        Dim isNeedHeaderCopy As Boolean = False
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            'Add a header row.
            clipBoardHeader += "Parameter Template Name" & ControlChars.Tab & "Parameter GroupName" & ControlChars.Tab & "Managed Object Name" & ControlChars.Tab & "Parameter Name" & ControlChars.Tab

            If tlvSelection.Nodes.Count <> 0 Then
                For Each lIitem As TreeListViewNode In tlvSelection.Nodes
                    'Add dynamic header columns from lvSelection treelistview column 0 index.
                    clipBoardHeader += lIitem.Text & ControlChars.Tab
                Next
            End If

            'Add a new line after the header row is complated.
            clipBoardText = clipBoardHeader & ControlChars.NewLine

            For Each mainTlvNode As LidorSystems.IntegralUI.Lists.TreeListViewNode In tlvDetail.Nodes
                If (mainTlvNode.Text.ToLower = "physical") Then
                    For Each tlvNodeManagedObject As LidorSystems.IntegralUI.Lists.TreeListViewNode In mainTlvNode.Nodes
                        'Add a data row for physical node.
                        clipBoardText += mainTlvNode.Text & ControlChars.Tab & ControlChars.Tab & ControlChars.Tab & ControlChars.Tab & ControlChars.Tab & ControlChars.Tab & ControlChars.Tab & ControlChars.Tab

                        For Each tlvItemParameterName As TreeListViewSubItem In tlvNodeManagedObject.SubItems
                            'Add a row for paramter name columnn and the parameter value column.
                            clipBoardText += tlvItemParameterName.Text & ControlChars.Tab
                        Next
                        'Add a new line after every row completed.
                        clipBoardText = clipBoardText & ControlChars.NewLine
                    Next
                Else
                    clipBoardText += "..." & ControlChars.Tab & "..." & ControlChars.Tab & "..." & ControlChars.Tab & "..." & ControlChars.Tab & "..." & ControlChars.Tab & "..." & ControlChars.Tab & "..." & ControlChars.Tab & "..." & ControlChars.NewLine
                    For Each tlvNodeGoupName As LidorSystems.IntegralUI.Lists.TreeListViewNode In mainTlvNode.Nodes
                        For Each tlvNodeManagedObject As LidorSystems.IntegralUI.Lists.TreeListViewNode In tlvNodeGoupName.Nodes
                            For Each tlvNodeParameterName As LidorSystems.IntegralUI.Lists.TreeListViewNode In tlvNodeManagedObject.Nodes
                                'Add a new data row for other nodes.
                                clipBoardText += mainTlvNode.Text & ControlChars.Tab & tlvNodeGoupName.Text & ControlChars.Tab & tlvNodeManagedObject.Text & ControlChars.Tab

                                For Each tlvItemParameterName As TreeListViewSubItem In tlvNodeParameterName.SubItems
                                    'Add on data row for the dynamic columns.
                                    clipBoardText += tlvItemParameterName.Text & ControlChars.Tab
                                Next
                                'Add a new line after every row completed.
                                clipBoardText = clipBoardText & ControlChars.NewLine
                            Next
                        Next
                    Next
                End If
            Next
            'Add entire data from into clipbaord for copying.
            Clipboard.SetText(clipBoardText)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_Export_Table_To_Excel_Click(sender As Object, e As EventArgs) Handles tsmi_Export_Table_To_Excel.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Application.UseWaitCursor = True
            Application.DoEvents()
            Dim dtParameter As DataTable = GetDataTableFromTLV()
            If (dtParameter IsNot Nothing) Then
                ExportDataTableToExcel(dtParameter)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        Application.UseWaitCursor = False
        Application.DoEvents()
    End Sub

    Private Sub tsmi_Copy_Value_To_Clipboard_Click(sender As Object, e As EventArgs) Handles tsmi_Copy_Value_To_Clipboard.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Clipboard.Clear()
            Dim clipBoardText As String = String.Empty
            Dim tn As TreeListViewNode = tlvDetail.SelectedNode
            Dim colCount As Integer = tlvDetail.Columns.Count

            For iCntr = 1 To colCount - 1
                clipBoardText &= tn.SubItems(iCntr).Text & ","
            Next

            clipBoardText = clipBoardText.TrimEnd(",")
            Clipboard.SetText(clipBoardText)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

End Class