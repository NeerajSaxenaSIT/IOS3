Imports System.IO
Imports System.Net
Imports DevExpress.XtraEditors
Imports IOS.Configuration
Imports IOS.DataLibrary
Imports IOS.Library
Imports MapInfo.Engine
Imports MapInfo.Geometry
Imports MapInfo.Mapping
Imports MapInfo.Ogc
Imports DevExpress.XtraGrid
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.Export.Xl
Imports System.Net.NetworkInformation
Imports DevExpress.XtraEditors.Controls
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic.FileIO

Module mdlCommonModule

#Region "Global Variables"

    Public connStrIOSLicenseServer As String = GetDecryptedConnectionString(IOSAppConfigManage.IOSLicenseServer)
    Public connStrIOSServer As String = GetDecryptedConnectionString(IOSAppConfigManage.IOSServer)
    Public connStrDriveTest As String = GetDecryptedConnectionString(IOSAppConfigManage.DriveTest)
    Public connStrSandBoxServer As String = GetDecryptedConnectionString(IOSAppConfigManage.SandBox_Server)
    Public connStrSandBoxHuawei As String = GetDecryptedConnectionString(IOSAppConfigManage.SandBox_Huawei)
    Public connStrCrystalReport As String = ""
    'Public connWebReportServer As String = GetDecryptedConnectionString(IOSAppConfigManage.WebReportServer)

    Public IsClearDefaultSelection As Boolean = True
    Public UserTracking As Boolean = False
    Public UserTrackingStatsCounterType As String = ""
    Public UserTrackingTopXObjects As String = ""
    Public mm As New clsMemoryManagement()
    Public WaitScreen As New clsWaitScreen()
    Public WaitScreenReportEditor As New clsWaitScreen()
    Public dmWaitScreen As New clsWaitScreen()
    Public _logger As New clsLoggerManager()
    Public imgListVendors As New ImageList()
    Public networkAll As New NetworksAll()
    Public configMgr As New IOSConfigManager()
    Public dtIOSObjectActive As DataTable = Nothing
    Public dtIOSSources As DataTable = Nothing
    Public configData As DataTable
    Public dt_IOS_SQL As New DataTable()
    Public dtIOSConnection As DataTable = Nothing
    Public dt_IOS_ObjectConfig As DataTable = Nothing
    Public Treeview_NodeFound As Boolean
    Public TreeView_SearchFound As Integer
    Public chartSetName As String = ""
    Public dtCustomChartKPI As DataTable
    Public ProxyServer As String
    Public PageRequestCount As Integer = 0
    Public TemplateSettingSelectionWindow As Integer = 0
    Public dtCneDataSource As DataTable = Nothing
    Public dtPredefinePeriod As DataTable = Nothing
    Public newAlertName As String = Nothing
    Public paramToExclude As String = Nothing
    Public newCapCategory As String = Nothing
    Public newCongestionJob As String = Nothing
    Public newCapCongRuleName As String = Nothing
    Public copyJobName As String = Nothing
    Public copyRuleName As String = Nothing
    Public newTemplateName As String = Nothing
    Public newFilterName As String = Nothing
    Public newNBIReportName As String = Nothing
    Public objFrmTechList As New List(Of frmTechnology)
    Public dicFrmTechInstances As New Dictionary(Of String, frmTechnology)
    Public objFrmICM As frmICM = Nothing
    Public objfrmSON As frmSON = Nothing
    Public objFrmPCHR As frmTracePCHR = Nothing
    Public objFrmAdHocTiltMngr As frmTiltManagement = Nothing
    Public objDTParserWebClient As frmInternetExplorer = Nothing
    Public objTechTicketsUrlWC As frmInternetExplorer = Nothing
    Public objReportEditWebLinkWC As frmInternetExplorer = Nothing
    Public objFrmParamHistory As frmParameterHistory = Nothing
    Public objSandbox As frmSBMain = Nothing
    Public objGenerateTemplate As frmGenerateTemplate = Nothing
    Public objProxy As WebProxy = Nothing
    Public dtUserConfigClient As New DataTable
    Public dtIncList As New DataTable
    Public dtExcList As New DataTable

    Public dtCellList As New DataTable
    Public dtBandListTiltMngr As New DataTable
    Public dtTiltRule As New DataTable
    Public dtLayer As New DataTable
    Public dtRefChkList As New DataTable
    Public dtAlertName As DataTable = Nothing
    Public dtCongestionJobsList As DataTable = Nothing
    Public dtCongRule As DataTable = Nothing
    Public dtCategory As DataTable = Nothing
    Public dtCMTemplate As DataTable = Nothing
    Public dtMmlConfig As DataTable = Nothing
    Public dsTreeSGSN_Kpi As DataSet
    Public dsTreeMGW_Kpi As DataSet
    Public dsTreeMSS_Kpi As DataSet
    Public dsTreeSGW_Kpi As DataSet
    Public dsTreeTX_Kpi As DataSet
    Public dsTreeMEE_Kpi As DataSet
    Public dsTreeTopXSGW_Kpi As DataSet
    Public dsTreePGW_Kpi As DataSet
    Public dsTreeSAPC_Kpi As DataSet
    Public dsTreeTopXPGW_Kpi As DataSet
    Public dsTreeGGSN_Kpi As DataSet
    Public dsTreeIMS_Kpi As DataSet
    Public dsTreeTM_Kpi As DataSet
    Public dsTreeTM2_Kpi As DataSet
    Public dsTreeTopXTM_Kpi As DataSet
    Public dsTreeTopXTM2_Kpi As DataSet
    Public dsTreeTransport_kpi As DataSet
    Public dsTreeTopXTransport_kpi As DataSet
    Public dsTreeMME_Kpi As DataSet
    Public dsTreeTopXMME_Kpi As DataSet
    Public dsTreeNano3G_Kpi As DataSet
    Public dsTree3G3_Kpi As DataSet '
    Public dsTreeTopX3G3_Kpi As DataSet
    Public dsTreeTopXNano3G_Kpi As DataSet
    Public dsTree3G_Kpi As DataSet
    Public dsTreeTopX3G_Kpi As DataSet
    Public dsTreeTopXCommon_Kpi As DataSet
    Public dsTree2G_Kpi As DataSet
    Public dsTreeNanoBTS_Kpi As DataSet
    Public dsTree2G3_Kpi As DataSet '
    Public dsTreeTopX2G_Kpi As DataSet
    Public dsTreeTopXNanoBTS_Kpi As DataSet
    Public dsTreeTopX2G3_Kpi As DataSet
    Public dsTreePDUM_kpi As DataSet
    Public dsTreeTopXPDUM_kpi As DataSet
    Public dsTreeTwamp_kpi As DataSet
    Public dsTreeTopXTwamp_kpi As DataSet
    Public dsTreeHLR_kpi As DataSet
    Public dsTreeTopXHLR_kpi As DataSet
    Public dsTreeDwdm_kpi As DataSet
    Public dsTreeTopXDwdm_kpi As DataSet
    Public dsTreeHSS_kpi As DataSet
    Public dsTreeTopXHSS_kpi As DataSet
    Public dsTreeUDR_kpi As DataSet
    Public dsTreeTopXUDR_kpi As DataSet

    Public dsTree4G1_Kpi As DataSet
    Public dsTree4G2_Kpi As DataSet
    Public dsTree4G3_Kpi As DataSet

    Public dsTree5G1_Kpi As DataSet
    Public dsTree5G2_Kpi As DataSet
    Public dsTree5G3_Kpi As DataSet

    Public dsTreeNode1_Kpi As DataSet
    Public dsTreeNode2_Kpi As DataSet
    Public dsTreeNode3_Kpi As DataSet

    Public dsTreeTopX4G1_Kpi As DataSet
    Public dsTreeTopX4G2_Kpi As DataSet
    Public dsTreeTopX4G3_Kpi As DataSet

    Public dsTreeTopX5G1_Kpi As DataSet
    Public dsTreeTopX5G2_Kpi As DataSet
    Public dsTreeTopX5G3_Kpi As DataSet

    Public dsTreeTopXNode1_Kpi As DataSet
    Public dsTreeTopXNode2_Kpi As DataSet
    Public dsTreeTopXNode3_Kpi As DataSet

    Public dsTreeMSC_Kpi_CDR As DataSet
    Public dsTreeSGSN_Kpi_CDR As DataSet
    Public dsTreeGGSN_Kpi_CDR As DataSet
    Public dsTreeTopXMSC_Kpi_CDR As DataSet
    Public dsTreeTopXSGSN_Kpi_CDR As DataSet
    Public dsTreeTopXGGSN_Kpi_CDR As DataSet
    Public dsTreeCommon_Kpi As DataSet

    Public dsTree2GVendor1 As New DataSet
    Public dsTree2GVendor2 As New DataSet
    Public dsTree2GVendor3 As New DataSet

    Public dsTree3GVendor1 As New DataSet
    Public dsTree3GVendor2 As New DataSet
    Public dsTree3GVendor3 As New DataSet

    Public dsTree4GVendor1 As New DataSet
    Public dsTree4GVendor2 As New DataSet
    Public dsTree4GVendor3 As New DataSet

    Public dsTree5GVendor1 As New DataSet
    Public dsTree5GVendor2 As New DataSet
    Public dsTree5GVendor3 As New DataSet

    Public dsTreeNodeVendor1 As New DataSet
    Public dsTreeNodeVendor2 As New DataSet
    Public dsTreeNodeVendor3 As New DataSet

    Public dsTreeMMEVendor1 As New DataSet
    Public dsTreeMSSVendor1 As New DataSet
    Public dsTreeMSCVendor1 As New DataSet
    Public dsTreeMGWVendor1 As New DataSet
    Public dsTreeSGWVendor1 As New DataSet
    Public dsTreeTXVendor1 As New DataSet
    Public dsTreeTX2Vendor1 As New DataSet
    Public dsTreeSGSNVendor1 As New DataSet
    Public dsTreeGGSNVendor1 As New DataSet
    Public dsTreePGWVendor1 As New DataSet
    Public dsTreeSAPCVendor1 As New DataSet
    Public dsTreeIMSVendor1 As New DataSet
    Public dsTreeGGSNVendorCDR As New DataSet
    Public dsTreeMSCVendorCDR As New DataSet
    Public dsTreeSGSNVendorCDR As New DataSet
    Public dsTreeTransportVendor1 As New DataSet
    Public dsTreePDUMVendor As New DataSet
    Public dsTreeTwampVendor As New DataSet
    Public dsTreeHLRVendor As New DataSet
    Public dsTreeDwdmVendor As New DataSet
    Public dsTreeHSSVendor As New DataSet
    Public dsTreeUDRVendor As New DataSet

    Public dsTreeCommonTech As New DataSet
    Public dsGridColumnsConfig As New DataSet

    '********** Datasets used in ObjectTree_DataSet_Load method **********'
    Public dsTree3G_wcel As DataSet
    Public dsTree3G_wbts As DataSet
    Public dsTree3G_rnc As DataSet
    Public dsTree2G_bts As DataSet
    Public dsTree2G_cel As DataSet
    Public dsTree2G_bcf As DataSet
    Public dsTree2G_bsc As DataSet
    Public dsTree2G_zone As DataSet
    Public dsTree2G_region As DataSet
    Public dsTree2G_mr As DataSet
    Public dsTree3G_zone As DataSet
    Public dsTree3G_region As DataSet
    Public dsTree3G_mr As DataSet
    Public dsTree3G_VCI As DataSet
    Public dsTree3G_VPI As DataSet
    Public dsTreeNanoBTS_cel As DataSet
    Public dsTreeNanoBTS_site As DataSet
    Public dsTreeNanoBTS_bsc As DataSet
    Public dsTreeNano3g_cel As DataSet
    Public dsTreeNano3g_site As DataSet
    Public dsTreeNano3g_ac As DataSet
    Public dsTree_sgsn As New DataSet
    Public dsTree_ggsn As New DataSet
    Public dsTree_MSS As New DataSet
    Public dsTree_MGW As New DataSet
    Public dsTree_ims As New DataSet

    'Launch Tilt Manager (global campaign ID)
    Public selectedTiltCampaignID As Integer = 0
    Public selectedTiltCampaignName As String = Nothing
    Public dtPointsTiltManager As DataTable = Nothing
    Public terrainProfileResolution As Integer = Nothing
    Public tiltMngrType As String = Nothing
    Public techExportFilePath As String = Nothing
    Public webview2Alert As Boolean = False

    'Service Check Variables
    Public objFrmSvcCheck As frmServiceCheck = Nothing
    Public getMapCoords As Boolean = False
    Public bSvcChkGglMapLoaded As Boolean = False

    'PM KPI Set
    Public objKPISetCreate As frmKPISetCreate = Nothing

    'PM Threshold Set
    Public objThresholdSetCreate As frmThresholdSetCreate = Nothing
    Public underFlowPercentile As Integer = 5
    Public overFlowPercentile As Integer = 95
    Public histChartBinsCount As Integer = 50

    'Perod Calculation Histogram Chart
    Public underFlowPercentile_PC As Integer = 5
    Public overFlowPercentile_PC As Integer = 95
    Public histChartBinsCount_PC As Integer = 50

    'PM Eval Global Variables
    Public underFlowPercentileEval As Integer = 5
    Public overFlowPercentileEval As Integer = 95
    Public histChartBinsCountEval As Integer = 50

    'Crystal Report Data
    Public dtCrystalReports As DataTable = Nothing

    'Dashboard Reports Data
    Public dtDashboardReports As DataTable = Nothing

    'XML Job
    Public newXMLJob As String = Nothing
    Public reloadGoogleMaps As Boolean = True

    'Datamart KPI Configuration
    Public objDMKpiConfig As frmDatamartKpiConfig = Nothing
    Public RefCheckCopyFromCommitted As Boolean = False

    'Ref Check
    Public dtFilterStrings As DataTable = Nothing
    Public dtIncExcObjects As DataTable = Nothing
    Public dtExcludedParams As DataTable = Nothing

    Public objThreadAddKPI As Threading.Thread
    Public objAddKPIThreadLock As New Object

    Public AlertCopyFromCommitted As Boolean = False
    Public SIProjectName As String = Nothing

    Public CultureInfoDefault As Globalization.CultureInfo
    Public CultureUIDefault As Globalization.CultureInfo
    Public TerrainAzimuthDefault As Double = Nothing
    Public TechThematicID As Integer = Nothing
    Public btnSrvCheck As ToolStripButton
    Public bInternetAvailable As Boolean = False
    Public regionalSettings As Boolean = False
    Public NewReportName As String = Nothing
    Public iQryTimeOut As Integer = Nothing

#End Region

#Region "Global Enum"

    Public Enum ComboSelectBased
        ValueBased
        TextBased
    End Enum

    Public Enum Vendor
        HUAWEI
        ERICSSON
        NOKIA
    End Enum

    Public Enum KPIModifyOption
        Add = 0
        Update = 1
    End Enum

#End Region

#Region "Map Global Variables"

    Public EventTypeMapped As String
    Public statusIsSelected As Boolean
    Public dt_Network_TabFiles As DataTable
    Public dt_Map_Configuration As DataTable
    Public dt_OSS_3G_UserParams As New DataTable
    Public dt_OSS_2G_UserParams As New DataTable
    Public dt_IOS_OSSParams As New DataTable
    Public statusForEvent As Boolean = True
    Public csysWGS84 As MapInfo.Geometry.CoordSys
    Public SharedTabFilePath As String = Nothing

#End Region

#Region "Get SQL Method"

    Public Function GetIOSConnection(ConnectionID As Integer) As String()
        Dim result() As String = {"", "", ""}
        Dim drResult() As DataRow = Nothing
        If dtIOSConnection Is Nothing Then
            dtIOSConnection = New DataTable()
            dtIOSConnection = DataAccessorODBC.GetDataTable(connStrIOSServer, "Select * From dbo.IOS_Connections")
        End If

        If dtIOSConnection.Select("ConnectionID=" & ConnectionID).Length > 0 Then
            drResult = dtIOSConnection.Select("ConnectionID=" & ConnectionID)
            If drResult.Length > 0 Then
                result(0) = drResult(0).Item("ConnectionType").ToString.Trim()
                result(1) = drResult(0).Item("ConnectionString").ToString.Trim()
                result(2) = drResult(0).Item("DatabaseName").ToString.Trim()
            End If
        End If
        Return result
    End Function

    Public Function GetSQL(ByVal sqlID As Integer, ByRef parray()() As String, Optional ByRef dtIOSSQL As DataTable = Nothing) As String()
        Dim result() As String = {"", ""}
        Dim drResult() As DataRow = Nothing
        If dtIOSSQL Is Nothing Then
            drResult = dt_IOS_SQL.AsEnumerable().Where(Function(x) x.Field(Of Integer)("SQL_ID") = sqlID).ToArray()
        Else
            drResult = dtIOSSQL.AsEnumerable().Where(Function(x) x.Field(Of Integer)("SQL_ID") = sqlID).ToArray()
        End If

        If drResult.Length > 0 Then
            result(0) = drResult(0).Item("ConnectionString").ToString.Trim()
            result(1) = drResult(0).Item("SQL_Command").ToString.Trim()
            iQryTimeOut = IIf(IsDBNull(drResult(0).Item("QueryTimeOut")), 60, CInt(drResult(0).Item("QueryTimeOut")))
        End If

        If result(1) <> "" Then
            Dim pstring() As String
            If Not parray Is Nothing Then
                For Each pstring In parray
                    result(1) = result(1).Replace(pstring(0), pstring(1))
                Next
            End If
            Return result
        End If
        Return Nothing
    End Function

#End Region

#Region "TreeView Methods"

    Public Function TreeView_CountCheckedNodes(ByVal rootNode As TreeNode) As Integer
        Dim count As Integer = 0
        ' count the root node, if checked
        If rootNode.Checked And rootNode.Nodes.Count = 0 Then count = 1
        ' check the child nodes, by recursively calling this function for all of 
        ' them
        Dim tvn As TreeNode = Nothing
        For Each tvn In rootNode.Nodes
            count += TreeView_CountCheckedNodes(tvn)
        Next
        tvn = Nothing
        Return count
    End Function

    Public Function TreeList_CountCheckedNodes(ByVal rootNode As DevExpress.XtraTreeList.Nodes.TreeListNode) As Integer
        Dim count As Integer = 0
        ' count the root node, if checked
        If rootNode.Checked And rootNode.Nodes.Count = 0 Then count = 1
        ' check the child nodes, by recursively calling this function for all of them
        Dim tvn As DevExpress.XtraTreeList.Nodes.TreeListNode = Nothing
        For Each tvn In rootNode.Nodes
            count += TreeList_CountCheckedNodes(tvn)
        Next
        tvn = Nothing
        Return count
    End Function

    Public Function Treeview_TextSearch(ByVal SearchString As String, ByVal Nodes As TreeNodeCollection, Optional ByVal ExactMatch As Boolean = False) As TreeNode
        Try
            Dim ret As TreeNode
            For Each tn As TreeNode In Nodes
                If ExactMatch = True Then
                    If tn.Text.ToLower = SearchString.ToLower Then Return tn
                Else
                    If tn.Text.IndexOf(SearchString) <> -1 Then Return tn
                End If

                If tn.Nodes.Count > 0 Then
                    ret = Treeview_TextSearch(SearchString, tn.Nodes, ExactMatch)
                    If Not ret Is Nothing Then Return ret
                End If
            Next
        Catch ex As Exception
        End Try
        Return Nothing
    End Function

    Public Function Treelist_TextSearch(SearchString As String, Nodes As TreeListNodes, Optional ExactMatch As Boolean = False, Optional ndColName As String = Nothing) As DevExpress.XtraTreeList.Nodes.TreeListNode
        Try
            Dim ret As DevExpress.XtraTreeList.Nodes.TreeListNode
            For Each tn As DevExpress.XtraTreeList.Nodes.TreeListNode In Nodes
                If ExactMatch = True Then
                    If tn.GetDisplayText(ndColName).ToLower = SearchString.ToLower Then Return tn
                Else
                    If tn.GetDisplayText(ndColName).IndexOf(SearchString) <> -1 Then Return tn
                End If

                If tn.Nodes.Count > 0 Then
                    ret = Treelist_TextSearch(SearchString, tn.Nodes, ExactMatch, ndColName)
                    If Not ret Is Nothing Then Return ret
                End If
            Next
        Catch ex As Exception
        End Try
        Return Nothing
    End Function

    Public Sub TreeView_ClearChecks(ByVal nd As TreeNode)
        For Each node As TreeNode In nd.Nodes
            If node.Checked = True Then
                node.Checked = False
                node.ForeColor = Color.Black
                TreeView_ClearChecks(node)
            End If
        Next
    End Sub

    Public Sub TreeList_ClearChecks(ByVal nd As DevExpress.XtraTreeList.Nodes.TreeListNode)
        For Each node As DevExpress.XtraTreeList.Nodes.TreeListNode In nd.Nodes
            If node.Checked = True Then
                node.Checked = False
                TreeList_ClearChecks(node)
            End If
        Next
    End Sub

    Public Sub Objecttree_CheckChild(ByVal rootnode As TreeNode)
        For Each tvn As TreeNode In rootnode.Nodes
            If Not (tvn.Checked) Then
                tvn.Checked = True
                Objecttree_CheckChild(tvn)
            End If
        Next
    End Sub

    Public Sub DevEx_ObjectTree_CheckChild(ByVal rootnode As TreeListNode)
        For Each tvn As TreeListNode In rootnode.Nodes
            If Not (tvn.Checked) Then
                tvn.Checked = True
                DevEx_ObjectTree_CheckChild(tvn)
            End If
        Next
    End Sub

    Public Sub DevEx_TreeView_ClearChecks(ByVal nd As TreeListNode)
        For Each node As TreeListNode In nd.Nodes
            If node.Checked = True Then
                node.Checked = False
                DevEx_TreeView_ClearChecks(node)
            End If
        Next
    End Sub

    Public Sub ObjectTreeList_CheckChild(ByVal rootnode As DevExpress.XtraTreeList.Nodes.TreeListNode)
        For Each tln As DevExpress.XtraTreeList.Nodes.TreeListNode In rootnode.Nodes
            If Not (tln.Checked) Then
                tln.Checked = True
                ObjectTreeList_CheckChild(tln)
            End If
        Next
    End Sub

    Public Function TreeView_Checked2String_Level(ByVal nd As TreeNode, ByVal level As Integer, ByVal outputtype As String) As String
        Dim Result As String = ""
        If nd.Checked = True And nd.Nodes.Count = 0 And outputtype = "ObjectName" Then
            Result = Result & Chr(39) & nd.Text & Chr(39) & ","
        ElseIf nd.Checked And nd.Nodes.Count = 0 And outputtype = "ObjectNameSplit" Then
            Result = Result & Chr(39) & Split(nd.Text, "-")(0).Trim.Substring(0, 5) & Chr(39) & ","
        ElseIf nd.Checked = True And nd.Nodes.Count = 0 And outputtype = "ObjectID" Then
            Result = Result & Chr(39) & nd.Tag & Chr(39) & ","
        ElseIf nd.Checked = True And nd.Nodes.Count = 0 And outputtype = "ObjectType" Then
            Result = Result & nd.ImageKey & ","
        ElseIf nd.Checked = True And nd.Nodes.Count = 0 And outputtype = "TAGS_CM" Then
            Result = Result & nd.Text & " AND "
        ElseIf nd.Checked = True And nd.Nodes.Count = 0 And outputtype = "Naked" Then
            Result = Result & nd.Text & ","
        ElseIf nd.Checked = True And nd.Nodes.Count = 0 And outputtype = "NewLine" Then
            Result = Result & nd.Text & vbLf
        End If

        Dim N As TreeNode
        For Each N In nd.Nodes
            Result = Result & TreeView_Checked2String_Level(N, level, outputtype)
        Next
        N = Nothing
        Return Result
    End Function

    Public Function TreeList_Checked2String_Level(ByVal tln As TreeListNode, ByVal level As Integer, ByVal outputtype As String, ndColName As String) As String
        Dim Result As String = ""
        If tln.Checked = True And tln.Nodes.Count = 0 And outputtype = "ObjectName" Then
            Result = Result & Chr(39) & tln.GetDisplayText(ndColName) & Chr(39) & ","
        ElseIf tln.Checked And tln.Nodes.Count = 0 And outputtype = "ObjectNameSplit" Then
            Result = Result & Chr(39) & Split(tln.GetDisplayText(ndColName), "-")(0).Trim.Substring(0, 5) & Chr(39) & ","
        ElseIf tln.Checked = True And tln.Nodes.Count = 0 And outputtype = "ObjectID" Then
            Result = Result & Chr(39) & tln.Tag & Chr(39) & ","
        ElseIf tln.Checked = True And tln.Nodes.Count = 0 And outputtype = "ObjectType" Then
            'Result = Result & tln.ImageKey & ","
        ElseIf tln.Checked = True And tln.Nodes.Count = 0 And outputtype = "TAGS_CM" Then
            Result = Result & tln.GetDisplayText(ndColName) & " AND "
        ElseIf tln.Checked = True And tln.Nodes.Count = 0 And outputtype = "Naked" Then
            Result = Result & tln.GetDisplayText(ndColName) & ","
        ElseIf tln.Checked = True And tln.Nodes.Count = 0 And outputtype = "NewLine" Then
            Result = Result & tln.GetDisplayText(ndColName) & vbLf
        End If

        Dim N As TreeListNode
        For Each N In tln.Nodes
            Result = Result & TreeList_Checked2String_Level(N, level, outputtype, ndColName)
        Next
        N = Nothing
        Return Result
    End Function

    Public Function Treeview_GetCheck(ByVal node As TreeNodeCollection, Optional IsLastNode As Boolean = False) As List(Of TreeNode)
        Dim lN As New List(Of TreeNode)
        For Each n As TreeNode In node
            If IsLastNode = True Then
                If n.Checked And n.Nodes.Count = 0 Then lN.Add(n)
            Else
                If n.Checked Then lN.Add(n)
            End If
            lN.AddRange(Treeview_GetCheck(n.Nodes, IsLastNode))
        Next
        Return lN
    End Function

    Public Function Treelist_GetCheck(ByVal node As DevExpress.XtraTreeList.Nodes.TreeListNodes, Optional IsLastNode As Boolean = False) As List(Of DevExpress.XtraTreeList.Nodes.TreeListNode)
        Dim lN As New List(Of DevExpress.XtraTreeList.Nodes.TreeListNode)
        For Each n As DevExpress.XtraTreeList.Nodes.TreeListNode In node
            If IsLastNode = True Then
                If n.Checked And n.Nodes.Count = 0 Then lN.Add(n)
            Else
                If n.Checked Then lN.Add(n)
            End If
            lN.AddRange(Treelist_GetCheck(n.Nodes, IsLastNode))
        Next
        Return lN
    End Function

    Public Function TreeView_CountCheckedAll(ByVal rootNode As TreeNode) As Integer
        Dim count As Integer = 0
        ' count the root node, if checked
        If rootNode.Checked Then count = 1
        ' check the child nodes, by recursively calling this function for all of them
        Dim tvn As TreeNode = Nothing
        For Each tvn In rootNode.Nodes
            count += TreeView_CountCheckedAll(tvn)
        Next
        tvn = Nothing
        Return count
    End Function

    Public Sub TreeView_SearchWildCard(ByVal nd As TreeNode, ByVal str As String, ByVal startindex As Integer, Optional ByVal ExactMatch As Boolean = False, Optional ByVal isKPISearch As Boolean = False)
        nd.TreeView.SuspendLayout()
        If str.Length < 3 Then
            For Each nd In nd.Nodes
                If Treeview_NodeFound = True Then
                    nd.TreeView.ResumeLayout(True)
                    Exit Sub
                End If
                If nd.Text.ToUpper = str.ToUpper Then
                    If Treeview_NodePosition(nd.TreeView, nd) > startindex Then
                        nd.EnsureVisible()
                        If Not isKPISearch Then
                            nd.TreeView.SelectedNode = nd
                        End If
                        nd.BackColor = Color.Coral
                        Treeview_NodeFound = True
                        nd.TreeView.ResumeLayout(True)
                        Exit Sub
                    End If
                Else
                    nd.BackColor = Color.White
                End If
                TreeView_SearchWildCard(nd, str, startindex, , isKPISearch)
            Next
        Else
            For Each nd In nd.Nodes
                If Treeview_NodeFound = True Then
                    nd.TreeView.ResumeLayout(True)
                    'Exit Sub
                End If
                If ExactMatch = False Then
                    If str.IndexOf("*") = 0 And str.LastIndexOf("*") = str.Length - 1 Then
                        If nd.Text.ToUpper.Contains(str.ToUpper.TrimStart("*").TrimEnd("*")) Then
                            If Treeview_NodePosition(nd.TreeView, nd) > startindex Then
                                nd.EnsureVisible()
                                If Not isKPISearch Then
                                    nd.TreeView.SelectedNode = nd
                                End If
                                nd.BackColor = Color.Coral
                                Treeview_NodeFound = True
                                nd.TreeView.ResumeLayout(True)
                                'Exit Sub
                            End If
                        Else
                            nd.BackColor = Color.White
                        End If
                        TreeView_SearchWildCard(nd, str, startindex, False, isKPISearch)
                    ElseIf str.IndexOf("*") = str.Length - 1 Then
                        If nd.Text.ToUpper.StartsWith(str.ToUpper.TrimEnd("*")) Then
                            If Treeview_NodePosition(nd.TreeView, nd) > startindex Then
                                nd.EnsureVisible()
                                If Not isKPISearch Then
                                    nd.TreeView.SelectedNode = nd
                                End If
                                nd.BackColor = Color.Coral
                                Treeview_NodeFound = True
                                nd.TreeView.ResumeLayout(True)
                                'Exit Sub
                            End If
                        Else
                            nd.BackColor = Color.White
                        End If
                        TreeView_SearchWildCard(nd, str, startindex, False, isKPISearch)
                    ElseIf str.IndexOf("*") = 0 Then
                        If nd.Text.ToUpper.EndsWith(str.ToUpper.TrimStart("*")) Then
                            If Treeview_NodePosition(nd.TreeView, nd) > startindex Then
                                nd.EnsureVisible()
                                If Not isKPISearch Then
                                    nd.TreeView.SelectedNode = nd
                                End If
                                nd.BackColor = Color.Coral
                                Treeview_NodeFound = True
                                nd.TreeView.ResumeLayout(True)
                                'Exit Sub
                            End If
                        Else
                            nd.BackColor = Color.White
                        End If
                        TreeView_SearchWildCard(nd, str, startindex, False, isKPISearch)
                    ElseIf str.Contains("*") = False Then
                        If isKPISearch = True Then
                            If nd.Text.ToUpper.StartsWith(str.ToUpper) Then
                                If Treeview_NodePosition(nd.TreeView, nd) > startindex Then
                                    nd.EnsureVisible()
                                    If Not isKPISearch Then
                                        nd.TreeView.SelectedNode = nd
                                    End If
                                    nd.BackColor = Color.Coral
                                    Treeview_NodeFound = True
                                    nd.TreeView.ResumeLayout(True)
                                    Exit Sub
                                End If
                            Else
                                nd.BackColor = Color.White
                            End If
                            TreeView_SearchWildCard(nd, str, startindex, False, isKPISearch)
                        Else
                            If nd.Text.ToUpper.StartsWith(str.ToUpper) Then
                                If Treeview_NodePosition(nd.TreeView, nd) > startindex Then
                                    nd.EnsureVisible()
                                    If Not isKPISearch Then
                                        nd.TreeView.SelectedNode = nd
                                    End If
                                    nd.BackColor = Color.Coral
                                    Treeview_NodeFound = True
                                    nd.TreeView.ResumeLayout(True)
                                    Exit Sub
                                End If
                            Else
                                nd.BackColor = Color.White
                            End If
                        End If
                        TreeView_SearchWildCard(nd, str, startindex, False, isKPISearch)
                    End If
                Else
                    If isKPISearch = True Then
                        If nd.Text.ToUpper.StartsWith(str.ToUpper) Then
                            If Treeview_NodePosition(nd.TreeView, nd) > startindex Then
                                nd.EnsureVisible()
                                If Not isKPISearch Then
                                    nd.TreeView.SelectedNode = nd
                                End If
                                nd.BackColor = Color.Coral
                                Treeview_NodeFound = True
                                nd.TreeView.ResumeLayout(True)
                                Exit Sub
                            End If
                        Else
                            nd.BackColor = Color.White
                        End If
                        TreeView_SearchWildCard(nd, str, startindex, True, isKPISearch)
                    Else
                        If ExactMatch = True Then
                            If nd.Text.ToUpper = str.ToUpper Then
                                If Treeview_NodePosition(nd.TreeView, nd) > startindex Then
                                    nd.EnsureVisible()
                                    If Not isKPISearch Then
                                        nd.TreeView.SelectedNode = nd
                                    End If
                                    nd.BackColor = Color.Coral
                                    Treeview_NodeFound = True
                                    nd.TreeView.ResumeLayout(True)
                                    Exit Sub
                                End If
                            Else
                                nd.BackColor = Color.White
                            End If
                            TreeView_SearchWildCard(nd, str, startindex, True, isKPISearch)
                        Else
                            If nd.Text.ToUpper.StartsWith(str.ToUpper) Then
                                If Treeview_NodePosition(nd.TreeView, nd) > startindex Then
                                    nd.EnsureVisible()
                                    nd.TreeView.SelectedNode = nd
                                    nd.BackColor = Color.Coral
                                    Treeview_NodeFound = True
                                    nd.TreeView.ResumeLayout(True)
                                    Exit Sub
                                End If
                            Else
                                nd.BackColor = Color.White
                            End If
                            TreeView_SearchWildCard(nd, str, startindex)
                        End If
                    End If
                End If
            Next
        End If
        nd.TreeView.ResumeLayout(True)
    End Sub

    'Determine the position (1-based) of the given Node in its TreeView.
    Public Function Treeview_NodePosition(ByVal oTreeView As TreeView, ByVal oNode As TreeNode)
        Dim iPosInTree As Integer = 0
        Do
            Dim iNodeIndex As Integer = oNode.Index
            iPosInTree = iPosInTree + iNodeIndex + 1
            'Get the Parent Node or the TreeView if at the top.
            Dim oParentNode As Object = oNode.Parent
            If oParentNode Is Nothing Then
                oParentNode = oTreeView
            End If
            'Count the Nodes precding this one on the current level.
            Dim I As Integer
            For I = 0 To iNodeIndex - 1
                iPosInTree = iPosInTree + Treeview_NumberOfChildren(oParentNode.Nodes(I))
            Next
            'Go up to the next level.
            oNode = oNode.Parent
        Loop Until oNode Is Nothing
        Return iPosInTree
    End Function

    Public Function Treeview_NumberOfChildren(ByVal oNode As TreeNode)
        If oNode.LastNode Is Nothing Then
            Return 0 'No children
        End If
        Dim iNumChildren = oNode.LastNode.Index + 1
        Dim oSubNode As TreeNode
        For Each oSubNode In oNode.Nodes
            iNumChildren = iNumChildren + Treeview_NumberOfChildren(oSubNode)
        Next
        Return iNumChildren
    End Function

    Public Sub SetGrayToNode(ByRef tNode As TreeNode)
        Try
            For Each nd As TreeNode In tNode.Nodes
                If (nd.Level = 1 And nd.Text = "Static List") Then
                    SetGrayToNode(nd)
                End If
                If (nd.Level = 2 And nd.Parent.Text = "Static List") Then
                    SetGrayToNode(nd)
                End If
                If nd.Level = 3 Then
                    If nd.Parent.Parent.Text = "Static List" Then
                        nd.ForeColor = Color.Gray
                    End If
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Sub FillObjectTreeData(ByRef tree As TreeView, Technology As String, _objectType As String)
        Try
            Dim dsObjects As New DataSet
            Dim imgListObject As New ImageList
            Dim tblname As String = "dsTree3G1_UCELL"
            Dim internaltech As String = "3G1"
            Dim BaseTech As String = "3G"

            For Each dr As DataRow In dt_IOS_ObjectConfig.Rows
                If Technology.ToUpper() = dr("Tech").ToString.ToUpper Then
                    Dim ds As New DataSet
                    internaltech = dr("TechInternal").ToString
                    BaseTech = dr("Technology").ToString.ToUpper

                    tblname = String.Format("dsTree{0}_{1}", internaltech, dr("Object").ToString)
                    If (File.Exists((String.Format("{0}\{1}.xml", GetUserDataPath(), tblname)))) Then
                        ds.ReadXml((String.Format("{0}\{1}.xml", GetUserDataPath(), tblname)), XmlReadMode.ReadSchema)
                    End If

                    If ds.Tables.Count > 0 Then
                        ds.Tables(0).TableName = tblname
                        If Not dr("ParentID").ToString = "0" Then
                            ds.Tables.Item(0).Merge(dsObjects.Tables.Item("dsTree" & internaltech & "_" & dr("ParentObject").ToString))
                        End If
                        If Not dsObjects.Tables.Contains(tblname) Then
                            dsObjects.Tables.Add(ds.Tables(0).Copy)
                        Else
                            dsObjects.Tables.Remove(tblname)
                            dsObjects.Tables.Add(ds.Tables(0).Copy)
                        End If
                        ds.Dispose()
                    End If
                End If
            Next

            tree.ImageList = Nothing
            clsIOSImageList.SetImages(imgListObject, BaseTech)
            tree.ImageList = imgListObject

            tblname = "dsTree" & internaltech & "_" & _objectType

            tree.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim roottn As TreeNode = New TreeNode()
            roottn.Text = "PLMN"
            roottn.ImageKey = "EMPTY"
            roottn.SelectedImageKey = "EMPTY"
            tree.Nodes.Clear()
            tree.Nodes.Add(roottn)
            Dim tNode As New TreeNode
            tNode = tree.Nodes(0)

            Select Case _objectType.ToLower
                Case "tags"
                    Dim ds_tag As New DataSet
                    Try
                        Dim parray()() As String = {
                            New String() {"@Tech", Chr(39) & Technology & Chr(39)},
                            New String() {"@TagOwner", Environment.UserName.ToString}
                        }
                        Dim sqlAndConnectionStr() As String = GetSQL(IOSSqlIds.TAGS_OBJECT_TREE, parray, dt_IOS_SQL)
                        ds_tag = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                        PopulateObjectTree("PLMN", tNode, ds_tag, BaseTech)
                        SetGrayToNode(tNode)
                    Catch ex As Exception
                        _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                    Finally
                        If ds_tag IsNot Nothing Then
                            ds_tag.Dispose()
                            ds_tag = Nothing
                        End If
                    End Try
                Case Else
                    Select Case Technology.ToUpper
                        Case "SGSN", "GGSN", "MGW", "MME", "MSS", "PGW", "SGW", "IMS", "TX", "TRANSPORT", "PDUM", "TWAMP"
                            PopulateObjectTree("1001", tNode, dsObjects, BaseTech, tblname)
                        Case Else
                            PopulateObjectTree(roottn.Text, tNode, dsObjects, BaseTech, tblname)
                    End Select
            End Select
            tNode.Expand()
            tNode = Nothing
        Catch ex As Exception
        Finally
            tree.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Public Sub PopulateObjectTree(ByVal inParentID As String, ByVal inTreeNode As TreeNode, ByVal ds As DataSet, Optional ByVal _BaseTech As String = Nothing, Optional ByVal tblname As String = "0")
        Try
            If inParentID = "PLMN" Then
                inTreeNode.Tag = "PLMN"
            End If
            Dim foundRows() As DataRow = Nothing
            If tblname = "0" Then
                foundRows = ds.Tables(0).Select("ParentID = " & Chr(39) & inParentID & Chr(39))
            Else
                If ds.Tables.Contains(tblname) Then
                    foundRows = ds.Tables(tblname).Select("ParentID = " & Chr(39) & inParentID & Chr(39))
                End If
            End If

            If foundRows IsNot Nothing Then
                For Each parentrow In foundRows
                    If parentrow.Item(0).ToString <> "" Then
                        Dim parentnode As TreeNode = New TreeNode(parentrow.Item(2).ToString.Trim)
                        parentnode.Name = parentrow.Item(2).ToString.Trim
                        parentnode.ImageKey = "EMPTY"
                        parentnode.SelectedImageKey = "EMPTY"

                        If _BaseTech IsNot Nothing Then
                            If _BaseTech.ToUpper = "2G" Then
                                If parentrow.ItemArray.Count > 4 Then
                                    Select Case nZ(parentrow.Item(4).ToString.Trim, "x")
                                        Case "2"
                                            parentnode.ImageKey = "DCS1"
                                            parentnode.SelectedImageKey = "DCS1"
                                        Case "1"
                                            parentnode.ImageKey = "DCS"
                                            parentnode.SelectedImageKey = "DCS"
                                        Case "0"
                                            parentnode.ImageKey = "EGSM"
                                            parentnode.SelectedImageKey = "EGSM"
                                        Case Else
                                            parentnode.ImageKey = parentrow.Item(3).ToString
                                            parentnode.SelectedImageKey = parentrow.Item(3).ToString
                                    End Select
                                Else
                                    parentnode.ImageKey = parentrow.Item(3).ToString
                                    parentnode.SelectedImageKey = parentrow.Item(3).ToString
                                End If
                            ElseIf _BaseTech.ToUpper = "3G" Or _BaseTech.ToUpper = "4G" Or _BaseTech.ToUpper = "5G" Then
                                If parentrow.ItemArray.Count > 4 Then
                                    Select Case nZ(parentrow.Item(4).ToString.Trim, "x")
                                        Case "1"
                                            parentnode.ImageKey = "BAND1"
                                            parentnode.SelectedImageKey = "BAND1"
                                        Case "2"
                                            parentnode.ImageKey = "BAND2"
                                            parentnode.SelectedImageKey = "BAND2"
                                        Case "3"
                                            parentnode.ImageKey = "BAND3"
                                            parentnode.SelectedImageKey = "BAND3"
                                        Case "4"
                                            parentnode.ImageKey = "BAND4"
                                            parentnode.SelectedImageKey = "BAND4"
                                        Case "5"
                                            parentnode.ImageKey = "BAND5"
                                            parentnode.SelectedImageKey = "BAND5"
                                        Case Else
                                            parentnode.ImageKey = parentrow.Item(3).ToString
                                            parentnode.SelectedImageKey = parentrow.Item(3).ToString
                                    End Select
                                Else
                                    parentnode.ImageKey = parentrow.Item(3).ToString
                                    parentnode.SelectedImageKey = parentrow.Item(3).ToString
                                End If
                            ElseIf _BaseTech.ToUpper.Contains("CDR") Then
                                If parentrow.ItemArray.Count > 4 Then
                                    Select Case nZ(parentrow.Item(4).ToString.Trim, "x")
                                        Case "1"
                                            parentnode.ImageKey = "BAND1"
                                            parentnode.SelectedImageKey = "BAND1"
                                        Case "2"
                                            parentnode.ImageKey = "BAND2"
                                            parentnode.SelectedImageKey = "BAND2"
                                        Case "3"
                                            parentnode.ImageKey = "BAND3"
                                            parentnode.SelectedImageKey = "BAND3"
                                        Case "4"
                                            parentnode.ImageKey = "BAND4"
                                            parentnode.SelectedImageKey = "BAND4"
                                        Case "5"
                                            parentnode.ImageKey = "BAND5"
                                            parentnode.SelectedImageKey = "BAND5"
                                        Case Else
                                            parentnode.ImageKey = parentrow.Item(3).ToString.ToUpper
                                            parentnode.SelectedImageKey = parentrow.Item(3).ToString.ToUpper
                                    End Select
                                Else
                                    parentnode.ImageKey = parentrow.Item(3).ToString
                                    parentnode.SelectedImageKey = parentrow.Item(3).ToString
                                End If
                            Else
                                parentnode.ImageKey = parentrow.Item(3).ToString.Trim
                                parentnode.SelectedImageKey = parentrow.Item(3).ToString.Trim
                            End If
                        Else
                            parentnode.ImageKey = parentrow.Item(3).ToString.Trim
                            parentnode.SelectedImageKey = parentrow.Item(3).ToString.Trim
                        End If

                        inTreeNode.Nodes.Add(parentnode)
                        parentnode.Tag = parentrow.Item(0).ToString.Trim
                        PopulateObjectTree(parentrow.Item(0).ToString.Trim, parentnode, ds, _BaseTech, tblname)
                        parentnode = Nothing
                    End If
                Next parentrow
            End If
            foundRows = Nothing
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Public Function Treeview_GetNodeLevel(ByVal tech As String, ByVal objectType As String, ByRef cmb As ComboBoxEdit, Optional vendor As String = "") As Integer
        If objectType = "PLMN" Then Return 0

        If cmb.SelectedItem.ToString.ToUpper = "TAGS" Then
            Select Case objectType
                Case "CELL"
                    Return 2
                Case "WCEL"
                    Return 2
                Case "TAGS"
                    Return 3
                Case "PLMN"
                    Return 0
            End Select
        End If

        Dim dr() As DataRow = dt_IOS_ObjectConfig.Select("Tech=" & Chr(39) & Replace(tech.ToLower, "topx_", "").ToUpper & Chr(39) & " AND Object=" & Chr(39) & objectType & Chr(39), "loadorder")
        If Not dr Is Nothing Then
            If dr.Count > 0 Then
                Dim level As Integer = ObjectTree_GetLevel(CInt(dr(0)("ID").ToString))
                Return level
            End If
        End If

        If Not (String.IsNullOrEmpty(vendor)) Then
            dr = dt_IOS_ObjectConfig.Select("Technology=" & Chr(39) & tech & Chr(39) & " AND Object=" & Chr(39) & objectType & Chr(39) & " AND Vendor=" & Chr(39) & vendor & Chr(39), "loadorder")
            If Not dr Is Nothing Then
                If dr.Count > 0 Then
                    Dim level As Integer = ObjectTree_GetLevel(CInt(dr(0)("ID").ToString))
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
        ElseIf tech = "IMS" Then
            Select Case objectType
                Case "IMS"
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
        ElseIf tech = "TRANSPORT" Then
            Select Case objectType
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

    Function ObjectTree_GetLevel(ByVal id As Integer) As Integer
        Dim level As Integer = 0
        Dim dr() As DataRow = dt_IOS_ObjectConfig.Select("ID=" & Chr(39) & id & Chr(39))
        If (dr.Count > 0) Then
            If dr(0)("ID").ToString <> 0 Then
                level = 1 + ObjectTree_GetLevel(dr(0)("ParentID").ToString)
            End If
        End If
        Return level
    End Function

    Public Function TreeView_Checked2String(ByVal tech As String, ByVal aggr_to As String, ByVal outputtype As String, ByRef tree As TreeView, ByRef cmb As ComboBoxEdit) As String
        Dim nodelevel As Integer
        Dim outputstr As New System.Text.StringBuilder()
        If outputtype = "ObjectNameWild" Then
            outputstr.Append(" LIKE ")
        ElseIf outputtype = "Naked" Then
            outputstr.Append("")
        ElseIf outputtype = "TAGS_CM" Then
            outputstr.Append("")
        ElseIf outputtype = "ObjectType" Then
            outputstr.Append("")
        ElseIf outputtype = "NewLine" Then
            outputstr.Append("")
        Else
            outputstr.Append("IN (")
        End If
        nodelevel = 3
        nodelevel = Treeview_GetNodeLevel(tech, aggr_to, cmb)

        For Each nd As TreeNode In tree.Nodes
            outputstr.Append(TreeView_Checked2String_Level(nd, nodelevel, outputtype))
        Next

        Dim outputfinal As String = Nothing
        If outputtype = "ObjectNameWild" Then
            outputfinal = Mid(outputstr.ToString, 1, outputstr.ToString.Length - 9)
        ElseIf outputtype = "Naked" Then
            outputfinal = outputstr.ToString.TrimEnd(",")
        ElseIf outputtype = "NewLine" Then
            outputfinal = outputstr.ToString.TrimEnd(vbLf)
        ElseIf outputtype = "TAGS_CM" Then
            outputfinal = outputstr.ToString.Substring(0, Len(outputstr.ToString) - 4)
        Else
            outputfinal = outputstr.ToString.TrimEnd(",") + ")"
        End If

        Return outputfinal
    End Function

    Public Function TreeList_Checked2String(ByVal tech As String, ByVal aggr_to As String, ByVal outputtype As String, ByRef tl As TreeList, ByRef cmb As ComboBoxEdit, ByVal ndColName As String) As String
        Dim nodelevel As Integer
        Dim outputstr As New System.Text.StringBuilder()
        If outputtype = "ObjectNameWild" Then
            outputstr.Append(" LIKE ")
        ElseIf outputtype = "Naked" Then
            outputstr.Append("")
        ElseIf outputtype = "TAGS_CM" Then
            outputstr.Append("")
        ElseIf outputtype = "ObjectType" Then
            outputstr.Append("")
        ElseIf outputtype = "NewLine" Then
            outputstr.Append("")
        Else
            outputstr.Append("IN (")
        End If
        nodelevel = 3
        nodelevel = tl.GetEndCheckedNodes().Item(0).Level 'Treeview_GetNodeLevel(tech, aggr_to, cmb)

        For Each nd As TreeListNode In tl.Nodes
            outputstr.Append(TreeList_Checked2String_Level(nd, nodelevel, outputtype, ndColName))
        Next

        Dim outputfinal As String = Nothing
        If outputtype = "ObjectNameWild" Then
            outputfinal = Mid(outputstr.ToString, 1, outputstr.ToString.Length - 9)
        ElseIf outputtype = "Naked" Then
            outputfinal = outputstr.ToString.TrimEnd(",")
        ElseIf outputtype = "NewLine" Then
            outputfinal = outputstr.ToString.TrimEnd(vbLf)
        ElseIf outputtype = "TAGS_CM" Then
            outputfinal = outputstr.ToString.Substring(0, Len(outputstr.ToString) - 4)
        Else
            outputfinal = outputstr.ToString.TrimEnd(",") + ")"
        End If

        Return outputfinal
    End Function

    Public Function TreeView_Checked2String(ByVal tech As String, ByVal aggr_to As String, ByVal outputtype As String, ByRef tree As TreeList, ByRef cmb As ComboBoxEdit) As String
        Dim nodelevel As Integer
        Dim outputstr As New System.Text.StringBuilder()
        If outputtype = "ObjectNameWild" Then
            outputstr.Append(" LIKE ")
        ElseIf outputtype = "Naked" Then
            outputstr.Append("")
        ElseIf outputtype = "TAGS_CM" Then
            outputstr.Append("")
        ElseIf outputtype = "ObjectType" Then
            outputstr.Append("")
        ElseIf outputtype = "NewLine" Then
            outputstr.Append("")
        Else
            outputstr.Append("IN (")
        End If
        nodelevel = 3
        nodelevel = Treeview_GetNodeLevel(tech, aggr_to, cmb)

        For Each nd As TreeNode In tree.Nodes
            outputstr.Append(TreeView_Checked2String_Level(nd, nodelevel, outputtype))
        Next

        Dim outputfinal As String = Nothing
        If outputtype = "ObjectNameWild" Then
            outputfinal = Mid(outputstr.ToString, 1, outputstr.ToString.Length - 9)
        ElseIf outputtype = "Naked" Then
            outputfinal = outputstr.ToString.TrimEnd(",")
        ElseIf outputtype = "NewLine" Then
            outputfinal = outputstr.ToString.TrimEnd(vbLf)
        ElseIf outputtype = "TAGS_CM" Then
            outputfinal = outputstr.ToString.Substring(0, Len(outputstr.ToString) - 4)
        Else
            outputfinal = outputstr.ToString.TrimEnd(",") + ")"
        End If

        Return outputfinal
    End Function

    Sub FillTreeList(ByRef tl As TreeList, tech As String, objectType As String, rNode As String, Optional filterObject As String = Nothing)
        Try
            tl.Cursor = Cursors.WaitCursor
            tl.BeginUnboundLoad()
            Application.DoEvents()

            Dim dsObjects As New DataSet
            Dim imgListObject As New ImageList
            Dim tblname As String = "dsTree3G1_UCELL"
            Dim internaltech As String = "3G1"
            Dim BaseTech As String = "3G"

            For Each dr As DataRow In dt_IOS_ObjectConfig.Rows
                If tech.ToUpper() = dr("Tech").ToString.ToUpper Then
                    Dim ds As New DataSet
                    internaltech = dr("TechInternal").ToString
                    BaseTech = dr("Technology").ToString.ToUpper

                    tblname = String.Format("dsTree{0}_{1}", internaltech, dr("Object").ToString)
                    If (File.Exists((String.Format("{0}\{1}.xml", GetUserDataPath(), tblname)))) Then
                        ds.ReadXml((String.Format("{0}\{1}.xml", GetUserDataPath(), tblname)), XmlReadMode.ReadSchema)
                    End If

                    If ds.Tables.Count > 0 Then
                        ds.Tables(0).TableName = tblname
                        If Not dr("ParentID").ToString = "0" Then
                            ds.Tables.Item(0).Merge(dsObjects.Tables.Item("dsTree" & internaltech & "_" & dr("ParentObject").ToString))
                        End If
                        If Not dsObjects.Tables.Contains(tblname) Then
                            dsObjects.Tables.Add(ds.Tables(0).Copy)
                        Else
                            dsObjects.Tables.Remove(tblname)
                            dsObjects.Tables.Add(ds.Tables(0).Copy)
                        End If
                        ds.Dispose()
                    End If
                End If
            Next

            tblname = "dsTree" & internaltech & "_" & objectType
            tl.PupulateTreeListColumn({"ObjectID", "ParentID", "ObjectName", "ObjectType", "Band", "ImageIndex"})

            tl.Nodes.Clear()
            Dim tlNode As TreeListNode = tl.Nodes.Add(New Object() {rNode, "0", rNode, "EMPTY", "-1", 1})

            Select Case objectType.ToLower
                Case "tags"
                    Dim ds_tag As New DataSet
                    Try
                        Dim parray()() As String = {
                            New String() {"@Tech", Chr(39) & tech & Chr(39)},
                            New String() {"@TagOwner", Environment.UserName.ToString}
                        }
                        Dim sqlAndConnectionStr() As String = GetSQL(IOSSqlIds.TAGS_OBJECT_TREE, parray, dt_IOS_SQL)
                        ds_tag = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                        tl.PopulateTreeList("PLMN", tlNode, ds_tag, "0")
                    Catch ex As Exception
                        _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                    Finally
                        If ds_tag IsNot Nothing Then
                            ds_tag.Dispose()
                            ds_tag = Nothing
                        End If
                    End Try
                Case Else
                    'Dim tblname As String = "0"
                    Select Case tech.ToUpper
                        Case "SGSN", "GGSN", "MGW", "MME", "MSS", "PGW", "SGW", "IMS", "TX", "TRANSPORT", "PDUM", "TWAMP"
                            tl.PopulateTreeList("1001", tlNode, dsObjects, tblname, filterObject, objectType, tech)
                        Case Else
                            tl.PopulateTreeList(rNode, tlNode, dsObjects, tblname, filterObject, objectType, tech)
                    End Select
            End Select
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            tl.EndUnboundLoad()
            If filterObject Is Nothing Then
                If tl.Nodes.Count > 0 Then
                    tl.SelectNode(tl.Nodes(0))
                    tl.SetFocusedNode(tl.Nodes(0))
                    tl.CollapseAll()
                    tl.ExpandToLevel(0)
                End If
            Else
                tl.SetColumnWidth()
            End If
            tl.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

#End Region

#Region "Global Methods"

    Public Function CSVBytesWriter(ByRef dt As DataTable, Optional ByVal withHeader As Boolean = True) As Byte()
        '--------Columns Name-----------
        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
        Dim intClmn As Integer = dt.Columns.Count

        If dt.Columns.Contains("rowHash") Then
            dt.Columns.Remove("rowHash")
        End If

        If withHeader Then
            Dim i As Integer = 0
            For i = 0 To intClmn - 1 Step i + 1
                sb.Append("""" + dt.Columns(i).ColumnName.ToString() + """")
                If i = intClmn - 1 Then
                    sb.Append(" ")
                Else
                    sb.Append(",")
                End If
            Next
            sb.Append(vbNewLine)
        End If

        '--------Data By Columns---------
        Dim row As DataRow
        For Each row In dt.Rows
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
        Return System.Text.Encoding.UTF8.GetBytes(sb.ToString)
    End Function

    Public Sub StreamSQLToCSV(ByVal SqlString As String, ByVal SqlConnString As String, ByVal destinationFile As String)
        Using conn = New Odbc.OdbcConnection(SqlConnString)
            conn.Open()

            Using command = New Odbc.OdbcCommand(SqlString, conn)

                Using reader = command.ExecuteReader()
                    Using outFile = File.CreateText(destinationFile)
                        Dim columnNames As String() = GetColumnNames(reader).ToArray()
                        Dim numFields As Integer = columnNames.Length
                        outFile.WriteLine(String.Join(",", columnNames))

                        If reader.HasRows Then

                            While reader.Read()
                                Dim columnValues As String() = Enumerable.Range(0, numFields).[Select](Function(i) reader.GetValue(i).ToString()).[Select](Function(field) String.Concat("""", field.Replace("""", """"""), """")).ToArray()
                                outFile.WriteLine(String.Join(",", columnValues))
                            End While
                        End If
                    End Using
                End Using
            End Using
            conn.Close()
            conn.Dispose()
        End Using
    End Sub

    Private Iterator Function GetColumnNames(ByVal reader As IDataReader) As IEnumerable(Of String)
        For Each row As DataRow In reader.GetSchemaTable().Rows
            Yield CStr(row("ColumnName"))
        Next
    End Function

    Public Function GetConfigClientKeyValue(Key As String) As Object
        Dim result As Object = Nothing
        Try
            If dtUserConfigClient.Rows.Count > 0 Then
                If dtUserConfigClient.Columns.Contains(Key) Then
                    Select Case dtUserConfigClient.Columns(Key).DataType
                        Case GetType(System.String)
                            If IsDBNull(dtUserConfigClient.Rows(0)(Key)) Then
                                result = ""
                            Else
                                result = Convert.ToString(dtUserConfigClient.Rows(0)(Key))
                            End If
                        Case GetType(System.Int32)
                            If IsDBNull(dtUserConfigClient.Rows(0)(Key)) Then
                                result = 0
                            Else
                                result = Convert.ToInt32(dtUserConfigClient.Rows(0)(Key))
                            End If
                        Case GetType(System.Boolean)
                            If IsDBNull(dtUserConfigClient.Rows(0)(Key)) Then
                                result = False
                            Else
                                result = Convert.ToBoolean(dtUserConfigClient.Rows(0)(Key))
                            End If
                        Case Else
                            result = Convert.ToString(dtUserConfigClient.Rows(0)(Key))
                    End Select
                End If
            End If
        Catch ex As Exception
            result = Nothing
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        Return result
    End Function

    Public Function IsThreshHoldBreached() As Boolean
        Try
            Dim breachedCount As Integer = 0
            breachedCount = DataAccessorODBC.ExecuteScalar(connStrIOSServer, "SELECT Count([Date_Counted]) FROM [dbo].[IOS_Data_Integrity_Evaluation] Where [Threshhold_Breach] = 1 And [Date_Counted] = cast(floor(cast(GETDATE() - 0 As float)) As datetime)")
            If breachedCount > 0 Then
                frmMDI.bbtnDataIntegrity.Glyph = My.Resources.if_alert_triangle_orange_536258
                Return True
            Else
                frmMDI.bbtnDataIntegrity.Glyph = My.Resources.green_box
                Return False
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            frmMDI.bbtnDataIntegrity.Glyph = My.Resources.green_box
            Return True
        End Try
    End Function

    Public Sub UserActionTracking(ByVal Action As String, ByVal InfoType As String, Optional ByVal Info As String = "")
        If UserTracking = True Then
            Dim t_UserTracking As New System.Threading.Thread(AddressOf InsertUserTracking)
            Dim objParam(4) As Object
            objParam(0) = connStrIOSServer
            objParam(1) = Action
            objParam(2) = InfoType
            objParam(3) = Info
            t_UserTracking.Start(objParam)
        End If
    End Sub

    Public Sub InsertUserTracking(objParam() As Object)
        Dim cnQODBC As System.Data.Odbc.OdbcConnection = Nothing
        Dim cmd As New System.Data.Odbc.OdbcCommand()
        Try
            'For SQL debugging purposes
            cnQODBC = New System.Data.Odbc.OdbcConnection(objParam(0))
            cnQODBC.ConnectionTimeout = 5
            cnQODBC.Open()
            cmd.Connection = cnQODBC
            cmd.CommandText = "EXECUTE [dbo].[IOS_Usage_Tracking_Insert] " & Chr(39) & Environment.UserName & Chr(39) & ", " & Chr(39) & objParam(1) & Chr(39) & ", " & Chr(39) & objParam(2) & Chr(39) & ", " & Chr(39) & objParam(3).ToString.Replace("'", "`") & Chr(39) & ""
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        Finally
            If Not cnQODBC Is Nothing Then
                cnQODBC.Close()
            End If
            If Not cnQODBC Is Nothing Then
                cnQODBC.Dispose()
            End If
            If Not cmd Is Nothing Then
                cmd = Nothing
            End If
        End Try
    End Sub

    Public Function IsDateIntervalChartVolumeOK(ByRef dtp1 As DateEdit, ByRef dtp2 As DateEdit, ByVal interval As String, ByVal numofcharts As Int16) As Boolean
        'numofcharts = 1
        'Try
        '    Dim startdate As Date = dtp1.EditValue
        '    Dim enddate As Date = dtp2.EditValue

        '    Dim dateinterval As Long = DateDiff(Microsoft.VisualBasic.DateInterval.Minute, startdate, enddate)
        '    Dim datapoints As Integer = 0
        '    If interval = "RAW" Then
        '        datapoints = dateinterval / 15 * numofcharts
        '    ElseIf interval = "HOUR" Then
        '        datapoints = dateinterval / 60 * numofcharts
        '    Else
        '        datapoints = dateinterval / 1440 * numofcharts
        '    End If
        '    If datapoints > 10000 Then
        '        Return False
        '    Else
        '        Return True
        '    End If
        'Catch ex As Exception

        'End Try
        Return True
    End Function

    Public Function IsDateIntervalObjectsOK(ByRef dtp1 As DateEdit, ByRef dtp2 As DateEdit, ByVal interval As String, ByVal Count_OT As Int32, Optional ByVal limit As Int32 = 100000) As Boolean
        Try
            Dim startdate As Date = dtp1.EditValue
            Dim enddate As Date = dtp2.EditValue

            Dim dateinterval As Long = DateDiff(Microsoft.VisualBasic.DateInterval.Minute, startdate, enddate)
            Dim datapoints As Integer = 0
            If interval = "RAW" Then
                datapoints = dateinterval / 15 * Count_OT
            ElseIf interval = "HOUR" Then
                datapoints = dateinterval / 60 * Count_OT
            ElseIf interval = "DAY" Or interval = "BH" Then
                datapoints = dateinterval / 1440 * Count_OT
            ElseIf interval = "WEEK" Or interval = "WEEKBH" Then
                datapoints = dateinterval / (1440 * 7) * Count_OT
            ElseIf interval = "MONTH" Or interval = "MONTHBH" Then
                datapoints = dateinterval / (1440 * 7 * 30) * Count_OT
            End If
            If datapoints > limit Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception

        End Try
        Return True
    End Function

    Public Sub ListBoxFilterOnTextBoxInput(ByRef lb As ListBoxControl, ByRef txtbox As TextEdit, ByVal displaymember As String)
        lb.DataSource.DefaultView.RowFilter = "[" + displaymember + "] like '%" & txtbox.Text.Trim() & "%'"
    End Sub

    Public Sub CheckTreeNodeAndCount(ByRef nd As TreeNode, ByRef counter As Integer, ByRef lbl As LabelControl)
        Try
            If nd.Checked = True Then
                If nd.Level > 1 Then
                    If nd.Parent.Checked = False Then
                        nd.Parent.Checked = True
                    End If
                End If

                If nd.Nodes.Count > 0 And Treeview_GetCheck(nd.Nodes).Count = 0 Then
                    For Each nde As TreeNode In nd.Nodes
                        If nde.Checked = False Then
                            nde.Checked = True
                        End If
                    Next
                End If
            Else
                If nd.Nodes.Count > 0 Then
                    For Each nde As TreeNode In nd.Nodes
                        If nde.Checked = True Then
                            nde.Checked = False
                        End If
                    Next
                End If
            End If
            If lbl IsNot Nothing Then
                CountCheckedNode(nd, counter, lbl)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Public Sub CheckTreeListNodeAndCount(ByRef nd As TreeListNode, ByRef counter As Integer, ByRef lbl As LabelControl)
        Try
            If nd.Checked = True Then
                If nd.Level > 1 Then
                    If nd.ParentNode.Checked = False Then
                        nd.ParentNode.Checked = True
                    End If
                End If

                If nd.Nodes.Count > 0 And Treelist_GetCheck(nd.Nodes).Count = 0 Then
                    For Each nde As TreeListNode In nd.Nodes
                        If nde.Checked = False Then
                            nde.Checked = True
                        End If
                    Next
                End If
            Else
                If nd.Nodes.Count > 0 Then
                    For Each nde As TreeListNode In nd.Nodes
                        If nde.Checked = True Then
                            nde.Checked = False
                        End If
                    Next
                Else
                    If nd.ParentNode.Checked = True Then
                        nd.ParentNode.Checked = False
                    End If
                End If
            End If
            If lbl IsNot Nothing Then
                CountTreeListCheckedNode(nd, counter, lbl)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Public Sub CountCheckedNode(ByRef nd As TreeNode, ByRef counter As Integer, ByRef lbl As LabelControl)
        If nd.Nodes.Count = 0 Then
            If nd.Checked = True Then
                counter = Math.Max(counter + 1, 0)
            Else
                counter = Math.Max(counter - 1, 0)
            End If
            lbl.Text = "#: " & counter
        End If
    End Sub

    Public Sub CountTreeListCheckedNode(ByRef nd As DevExpress.XtraTreeList.Nodes.TreeListNode, ByRef counter As Integer, ByRef lbl As LabelControl)
        If nd.Nodes.Count = 0 Then
            If nd.Checked = True Then
                counter = Math.Max(counter + 1, 0)
            Else
                counter = Math.Max(counter - 1, 0)
            End If
            lbl.Text = "#: " & counter
        End If
    End Sub

    Public Sub ExportDataTableToExcel(ByVal dtTemp As DataTable)
        Dim savefiledialog1 As New SaveFileDialog()
        savefiledialog1.FileName = ""
        savefiledialog1.Filter = "Excel Workbook |*.xlsx"

        If savefiledialog1.ShowDialog <> DialogResult.OK Then
            Exit Sub
        End If

        Dim fp As String = savefiledialog1.FileName
        Dim xlApp As Microsoft.Office.Interop.Excel.Application = Nothing
        Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook = Nothing
        Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet = Nothing

        If IO.File.Exists(fp) Then
            IO.File.Delete(fp)
        End If

        Try
            xlApp = New Microsoft.Office.Interop.Excel.Application
            xlWorkBook = xlApp.Workbooks.Add()

            Dim dtPhysical As New DataTable
            dtPhysical = dtTemp.Select("[" & dtTemp.Columns(0).ColumnName & "]='Physical'").CopyToDataTable()

            Dim dtParam As New DataTable
            dtParam = dtTemp.Select("[" & dtTemp.Columns(0).ColumnName & "]<>'Physical'").CopyToDataTable()

            Dim dc As System.Data.DataColumn
            Dim dr As System.Data.DataRow
            Dim colIndex As Integer = 0
            Dim rowIndex As Integer = 0

            Dim xlWorksheetPhysical As Microsoft.Office.Interop.Excel.Worksheet = CType(xlWorkBook.Worksheets.Add(), Microsoft.Office.Interop.Excel.Worksheet)
            xlWorksheetPhysical.Name = "Physical"

            For Each dc In dtPhysical.Columns
                colIndex = colIndex + 1
                xlWorksheetPhysical.Cells(1, colIndex) = dc.ColumnName
            Next

            For Each dr In dtPhysical.Rows
                rowIndex = rowIndex + 1
                colIndex = 0
                For Each dc In dtPhysical.Columns
                    colIndex = colIndex + 1
                    xlWorksheetPhysical.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)
                Next
            Next
            xlWorksheetPhysical = Nothing

            colIndex = 0
            rowIndex = 0
            Dim xlWorksheetParam As Microsoft.Office.Interop.Excel.Worksheet = CType(xlWorkBook.Worksheets.Add(), Microsoft.Office.Interop.Excel.Worksheet)

            For Each dc In dtParam.Columns
                colIndex = colIndex + 1
                xlWorksheetParam.Cells(1, colIndex) = dc.ColumnName
            Next

            Dim sheetName As String = "Sheet1"
            For Each dr In dtParam.Rows
                rowIndex = rowIndex + 1
                colIndex = 0
                For Each dc In dtParam.Columns
                    colIndex = colIndex + 1
                    xlWorksheetParam.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)
                Next
                sheetName = dr(0).ToString
            Next
            xlWorksheetParam.Name = sheetName
            xlWorksheetParam = Nothing

            xlWorkBook.SaveAs(fp)
        Catch ex As Exception

        Finally
            If Not xlWorkBook Is Nothing Then
                xlWorkBook.Close(SaveChanges:=True)
            End If
            If Not xlApp Is Nothing Then
                xlApp.Quit()
            End If
            xlApp = Nothing
            xlWorkBook = Nothing
        End Try
    End Sub

    Public Sub ObjectTree_DataSet_Load(ByVal node As String, Optional ByVal reload As Boolean = False, Optional ByVal dt As DataTable = Nothing, Optional ByRef lblOTSTatus As LabelControl = Nothing)
        Try
            Dim tt As New ToolTip
            Dim span As TimeSpan
            Dim strArray As String()()
            Dim strArray2 As String()()
            Dim connStr As String
            Dim sql As String

            Dim list = New List(Of String)
            If Not dt Is Nothing Then
                For Each row As DataRow In dt.Rows
                    list.Add(row(0).ToString)
                Next
            End If

            Select Case node.ToLower
                Case "wbts"
                    dsTree3G_wbts = New DataSet
                    If (File.Exists((GetUserDataPath() & "\dsTree3G_wbts.xml")) And Not reload) Then
                        dsTree3G_wbts.ReadXml((GetUserDataPath() & "\dsTree3G_wbts.xml"), XmlReadMode.ReadSchema)
                    Else
                        strArray2 = Nothing
                        strArray = Nothing
                        connStr = GetSQL(IOSSqlIds.OBJECTTREE_3G_WBTS, strArray2, dt_IOS_SQL)(0)
                        If list.Contains(connStr) Then
                            sql = GetSQL(IOSSqlIds.OBJECTTREE_3G_WBTS, strArray, dt_IOS_SQL)(1)
                            dsTree3G_wbts = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)
                            If Not dsTree3G_wbts Is Nothing Then
                                dsTree3G_wbts.WriteXml((GetUserDataPath() & "\dsTree3G_wbts.xml"), XmlWriteMode.WriteSchema)
                            End If
                        End If
                    End If
                    If dsTree3G_wbts.Tables.Count > 0 Then
                        dsTree3G_wbts.Tables.Item(0).Merge(dsTree3G_rnc.Tables.Item(0))
                    End If
                    Return

                Case "rnc"
                    dsTree3G_rnc = New DataSet
                    If (File.Exists((GetUserDataPath() & "\dsTree3G_rnc.xml")) And Not reload) Then
                        dsTree3G_rnc.ReadXml((GetUserDataPath() & "\dsTree3G_rnc.xml"), XmlReadMode.ReadSchema)
                    Else
                        strArray2 = Nothing
                        strArray = Nothing
                        connStr = GetSQL(IOSSqlIds.OBJECTTREE_3G_RNC, strArray2, dt_IOS_SQL)(0)
                        If list.Contains(connStr) Then
                            sql = GetSQL(IOSSqlIds.OBJECTTREE_3G_RNC, strArray, dt_IOS_SQL)(1)
                            dsTree3G_rnc = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)
                            If Not dsTree3G_rnc Is Nothing Then
                                dsTree3G_rnc.WriteXml((GetUserDataPath() & "\dsTree3G_rnc.xml"), XmlWriteMode.WriteSchema)
                            End If
                        End If
                    End If
                    Return

                Case "sgsn"
                    dsTree_sgsn = New DataSet
                    If (File.Exists((GetUserDataPath() & "\dsTree_sgsn.xml")) And Not reload) Then
                        dsTree_sgsn.ReadXml((GetUserDataPath() & "\dsTree_sgsn.xml"), XmlReadMode.ReadSchema)
                    Else
                        strArray2 = Nothing
                        strArray = Nothing

                        connStr = GetSQL(1109, strArray2)(0)
                        If list.Contains(connStr) Then
                            sql = GetSQL(1109, strArray)(1)
                            dsTree_sgsn = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)

                            If Not dsTree_sgsn Is Nothing Then
                                dsTree_sgsn.WriteXml((GetUserDataPath() & "\dsTree_sgsn.xml"), XmlWriteMode.WriteSchema)
                            End If
                        End If
                    End If
                    Return

                Case "ggsn"
                    dsTree_ggsn = New DataSet
                    If (File.Exists((GetUserDataPath() & "\dsTree_GGSN.xml")) And Not reload) Then
                        dsTree_ggsn.ReadXml((GetUserDataPath() & "\dsTree_GGSN.xml"), XmlReadMode.ReadSchema)
                    Else
                        strArray2 = Nothing
                        strArray = Nothing

                        connStr = GetSQL(1115, strArray2)(0)
                        If list.Contains(connStr) Then
                            sql = GetSQL(1115, strArray)(1)
                            dsTree_ggsn = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)

                            If Not dsTree_ggsn Is Nothing Then
                                dsTree_ggsn.WriteXml((GetUserDataPath() & "\dsTree_GGSN.xml"), XmlWriteMode.WriteSchema)
                            End If
                        End If
                    End If
                    Return

                Case "ims"
                    dsTree_ims = New DataSet
                    If (File.Exists((GetUserDataPath() & "\dsTree_IMS.xml")) And Not reload) Then
                        dsTree_ims.ReadXml((GetUserDataPath() & "\dsTree_IMS.xml"), XmlReadMode.ReadSchema)
                    Else
                        strArray2 = Nothing
                        strArray = Nothing

                        connStr = GetSQL(1120, strArray2)(0)
                        If list.Contains(connStr) Then
                            sql = GetSQL(1120, strArray)(1)
                            dsTree_ims = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)

                            If Not dsTree_ims Is Nothing Then
                                dsTree_ims.WriteXml((GetUserDataPath() & "\dsTree_IMS.xml"), XmlWriteMode.WriteSchema)
                            End If
                        End If
                    End If
                    Return

                Case "mgw"
                    dsTree_MGW = New DataSet
                    If (File.Exists((GetUserDataPath() & "\dsTree_mgw.xml")) And Not reload) Then
                        dsTree_MGW.ReadXml((GetUserDataPath() & "\dsTree_mgw.xml"), XmlReadMode.ReadSchema)
                    Else
                        strArray2 = Nothing
                        strArray = Nothing

                        connStr = GetSQL(1111, strArray2)(0)
                        If list.Contains(connStr) Then
                            sql = GetSQL(1111, strArray)(1)
                            dsTree_MGW = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)

                            If Not dsTree_MGW Is Nothing Then
                                dsTree_MGW.WriteXml((GetUserDataPath() & "\dsTree_mgw.xml"), XmlWriteMode.WriteSchema)
                            End If
                        End If
                    End If
                    Return

                Case "mss"
                    dsTree_MSS = New DataSet
                    If (File.Exists((GetUserDataPath() & "\dsTree_mss.xml")) And Not reload) Then
                        dsTree_MSS.ReadXml((GetUserDataPath() & "\dsTree_mss.xml"), XmlReadMode.ReadSchema)
                    Else
                        strArray2 = Nothing
                        strArray = Nothing

                        connStr = GetSQL(1110, strArray2)(0)
                        If list.Contains(connStr) Then
                            sql = GetSQL(1110, strArray)(1)
                            dsTree_MSS = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)

                            If Not dsTree_MSS Is Nothing Then
                                dsTree_MSS.WriteXml((GetUserDataPath() & "\dsTree_mss.xml"), XmlWriteMode.WriteSchema)
                            End If
                        End If
                    End If
                    Return

                Case "bts"
                    dsTree2G_bts = New DataSet
                    If (File.Exists((GetUserDataPath() & "\dsTree2G_bts.xml")) And Not reload) Then
                        dsTree2G_bts.ReadXml((GetUserDataPath() & "\dsTree2G_bts.xml"), XmlReadMode.ReadSchema)
                        span = DateAndTime.Now - File.GetLastWriteTime((GetUserDataPath() & "\dsTree2G_bts.xml"))
                        If lblOTSTatus IsNot Nothing Then
                            lblOTSTatus.Text = ("Treedata is " & Convert.ToString(Math.Round(span.TotalDays, 0)) & " Days Old... ")
                            If lblOTSTatus.Text.Contains(" 0 ") Then
                                lblOTSTatus.ForeColor = Color.Green
                                'Me.lbl_OT_ToolTip.SetToolTip(Me.lbl_OT_Status_2G, "")
                                tt.SetToolTip(lblOTSTatus, "")
                            Else
                                lblOTSTatus.ForeColor = Color.Orange
                                tt.SetToolTip(lblOTSTatus, "Go To Map Form > Settings > Sync Console Objects ")
                            End If
                        End If
                    Else
                        strArray2 = Nothing
                        strArray = Nothing

                        connStr = GetSQL(1019, strArray2)(0)

                        If list.Contains(connStr) Then
                            sql = GetSQL(1019, strArray)(1)

                            dsTree2G_bts = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)
                            If Not dsTree2G_bts Is Nothing Then
                                dsTree2G_bts.WriteXml((GetUserDataPath() & "\dsTree2G_bts.xml"), XmlWriteMode.WriteSchema)
                            End If

                            If dsTree2G_bts.Tables.Count > 0 Then
                                dsTree2G_bts.Tables.Item(0).Merge(dsTree2G_bcf.Tables.Item(0))
                            End If
                        End If
                    End If
                    Return

                Case "wcel"
                    dsTree3G_wcel = New DataSet
                    If (File.Exists((GetUserDataPath() & "\dsTree3G_wcel.xml")) And Not reload) Then
                        dsTree3G_wcel.ReadXml((GetUserDataPath() & "\dsTree3G_wcel.xml"), XmlReadMode.ReadSchema)
                        '' Dim span5 As TimeSpan = DirectCast((DateAndTime.Now - File.GetLastWriteTime((GetUserDataPath() & "\dsTree3G_wcel.xml"))), TimeSpan)
                        'lbl_OT_Status_3G.Text = ("Treedata is " & Convert.ToString(Math.Round(span5.TotalDays, 0)) & " Days Old ... ")
                        'If lbl_OT_Status_3G.Text.Contains(" 0 ") Then
                        '    lbl_OT_Status_3G.ForeColor = Color.Green
                        '    tt.SetToolTip(lbl_OT_Status_3G, "")
                        'Else
                        '    lbl_OT_Status_3G.ForeColor = Color.Orange
                        '    tt.SetToolTip(lbl_OT_Status_3G, "Go To Map Form > Settings > Sync Console Objects ")

                        'End If
                    Else
                        strArray = Nothing
                        strArray2 = Nothing
                        connStr = GetSQL(IOSSqlIds.OBJECTTREE_3G_WCEL, strArray2, dt_IOS_SQL)(0)
                        If list.Contains(connStr) Then
                            sql = GetSQL(IOSSqlIds.OBJECTTREE_3G_WCEL, strArray, dt_IOS_SQL)(1)
                            dsTree3G_wcel = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)
                            If Not dsTree3G_wcel Is Nothing Then
                                dsTree3G_wcel.WriteXml((GetUserDataPath() & "\dsTree3G_wcel.xml"), XmlWriteMode.WriteSchema)
                            End If
                        End If
                    End If
                    If dsTree3G_wcel.Tables.Count > 0 Then
                        dsTree3G_wcel.Tables.Item(0).Merge(dsTree3G_wbts.Tables.Item(0))
                    End If
                    Return

                Case "vci"
                    dsTree3G_VCI = New DataSet
                    If (File.Exists((GetUserDataPath() & "\dsTree3G_VCI.xml")) And Not reload) Then
                        dsTree3G_VCI.ReadXml((GetUserDataPath() & "\dsTree3G_VCI.xml"), XmlReadMode.ReadSchema)
                    Else
                        strArray2 = Nothing
                        strArray = Nothing

                        connStr = GetSQL(IOSSqlIds.OBJECTTREE_TX_VCI, strArray2, dt_IOS_SQL)(0)

                        If list.Contains(connStr) Then
                            sql = GetSQL(IOSSqlIds.OBJECTTREE_TX_VCI, strArray, dt_IOS_SQL)(1)

                            dsTree3G_VCI = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)

                            If Not dsTree3G_VCI Is Nothing Then
                                dsTree3G_VCI.WriteXml((GetUserDataPath() & "\dsTree3G_VCI.xml"), XmlWriteMode.WriteSchema)
                            End If
                        End If
                    End If
                    If dsTree3G_VCI.Tables.Count > 0 Then
                        dsTree3G_VCI.Tables.Item(0).Merge(dsTree3G_VPI.Tables.Item(0))
                    End If
                    Exit Select

                Case "nano3g_cell"
                    dsTreeNano3g_cel = New DataSet
                    If (File.Exists((GetUserDataPath() & "\dsTreeNano3g_cel.xml")) And Not reload) Then
                        dsTreeNano3g_cel.ReadXml((GetUserDataPath() & "\dsTreeNano3g_cel.xml"), XmlReadMode.ReadSchema)
                        'span = DirectCast((DateAndTime.Now - File.GetLastWriteTime((GetUserDataPath() & "\dsTreeNano3g_cel.xml"))), TimeSpan)
                        'lbl_OT_Status_Nano3G.Text = ("Treedata is " & Convert.ToString(Math.Round(span.TotalDays, 0)) & " Days Old... ")
                        'If lbl_OT_Status_Nano3G.Text.Contains(" 0 ") Then
                        '    lbl_OT_Status_Nano3G.ForeColor = Color.Green

                        '    tt.SetToolTip(lbl_OT_Status_Nano3G, "")

                        '    'lbl_OT_ToolTip.SetToolTip(lbl_OT_Status_NanoBTS, "")
                        'Else
                        '    lbl_OT_Status_Nano3G.ForeColor = Color.Orange
                        '    'lbl_OT_ToolTip.SetToolTip(lbl_OT_Status_NanoBTS, "Go To Map Form > Settings > Sync Console Objects ")
                        '    tt.SetToolTip(lbl_OT_Status_Nano3G, "Go To Map Form > Settings > Sync Console Objects ")
                        'End If
                    Else
                        strArray2 = Nothing
                        strArray = Nothing

                        connStr = GetSQL(IOSSqlIds.OBJECTTREE_NANO3G_CELL, strArray2, dt_IOS_SQL)(0)

                        If list.Contains(connStr) Then
                            sql = GetSQL(IOSSqlIds.OBJECTTREE_NANO3G_CELL, strArray, dt_IOS_SQL)(1)

                            dsTreeNano3g_cel = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)

                            If Not dsTreeNano3g_cel Is Nothing Then
                                dsTreeNano3g_cel.WriteXml((GetUserDataPath() & "\dsTreeNano3g_cel.xml"), XmlWriteMode.WriteSchema)
                            End If
                        End If
                    End If
                    If Not dsTreeNano3g_cel Is Nothing Then
                        dsTreeNano3g_cel.Tables.Item(0).Merge(dsTreeNano3g_site.Tables.Item(0))
                    End If
                    Return

                Case "nano3g_site"
                    dsTreeNano3g_site = New DataSet
                    If (File.Exists((GetUserDataPath() & "\dsTreeNano3g_site.xml")) And Not reload) Then
                        dsTreeNano3g_site.ReadXml((GetUserDataPath() & "\dsTreeNano3g_site.xml"), XmlReadMode.ReadSchema)
                    Else
                        strArray2 = Nothing
                        strArray = Nothing

                        connStr = GetSQL(IOSSqlIds.OBJECTTREE_NANO3G_SITE, strArray2, dt_IOS_SQL)(0)

                        If list.Contains(connStr) Then
                            sql = GetSQL(IOSSqlIds.OBJECTTREE_NANO3G_SITE, strArray, dt_IOS_SQL)(1)

                            dsTreeNano3g_site = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)

                            If Not dsTreeNano3g_site Is Nothing Then
                                dsTreeNano3g_site.WriteXml((GetUserDataPath() & "\dsTreeNano3g_site.xml"), XmlWriteMode.WriteSchema)
                            End If

                        End If

                    End If
                    If Not dsTreeNano3g_site Is Nothing Then
                        dsTreeNano3g_site.Tables.Item(0).Merge(dsTreeNano3g_ac.Tables.Item(0))
                    End If
                    Exit Select

                Case "nano3g_rnc"
                    dsTreeNano3g_ac = New DataSet
                    If (File.Exists((GetUserDataPath() & "\dsTreeNano3g_ac.xml")) And Not reload) Then
                        dsTreeNano3g_ac.ReadXml((GetUserDataPath() & "\dsTreeNano3g_ac.xml"), XmlReadMode.ReadSchema)
                    Else
                        strArray2 = Nothing
                        strArray = Nothing

                        connStr = GetSQL(IOSSqlIds.OBJECTTREE_NANO3G_RNC, strArray2, dt_IOS_SQL)(0)

                        If list.Contains(connStr) Then
                            sql = GetSQL(IOSSqlIds.OBJECTTREE_NANO3G_RNC, strArray, dt_IOS_SQL)(1)
                            dsTreeNano3g_ac = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)

                            If Not dsTreeNano3g_ac Is Nothing Then
                                dsTreeNano3g_ac.WriteXml((GetUserDataPath() & "\dsTreeNano3g_ac.xml"), XmlWriteMode.WriteSchema)
                            End If
                        End If
                    End If
                    Exit Select

            End Select
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Public Function XML_Parameters_NSN(ByVal fn As String, ByVal dt As DataTable) As Boolean
        Try
            Dim objDom As Xml.XmlDocument
            Dim objRaml As Xml.XmlElement

            Dim objCMdata As Xml.XmlElement
            Dim objHeader As Xml.XmlElement
            Dim objLog As Xml.XmlElement
            Dim objMO As Xml.XmlElement = Nothing
            Dim objParam As Xml.XmlElement
            Dim Version, distname, id, objlevel As String  'class_name, param

            objDom = New Xml.XmlDocument
            objDom.LoadXml("<?xml version=""1.0"" encoding=""UTF-8""?><raml><cmData/></raml>")

            objRaml = objDom.GetElementsByTagName("raml").Item(0)
            objRaml.SetAttribute("version", "2.0")
            objRaml.SetAttribute("xmlns", "raml20.xsd")

            objCMdata = objDom.GetElementsByTagName("cmData").Item(0)
            objCMdata.SetAttribute("xmlns", "")
            objCMdata.SetAttribute("type", "plan")
            objCMdata.SetAttribute("scope", "all")
            objCMdata.SetAttribute("name", "default")

            'create header
            objHeader = objDom.CreateElement("header")
            objCMdata.AppendChild(objHeader)

            'create logs
            objLog = objDom.CreateElement("log")
            objHeader.AppendChild(objLog)
            objLog.SetAttribute("dateTime", Now.ToString("dd-MM-yyyy_HH-mm-ss"))
            objLog.SetAttribute("action", "created")

            Dim dn_old As String = ""
            dt.DefaultView.Sort = "Object_GID ASC"

            'create XML for BTS param
            For Each dr As DataRow In dt.DefaultView.ToTable.Rows

                If Not dr("DefaultValue").ToString Is Nothing Then
                    If dr("OBJECT_GID").ToString.Trim <> dn_old Then
                        dn_old = dr("OBJECT_GID").ToString.Trim

                        Version = "RN5.0"
                        distname = dr("Object_DN").ToString
                        id = dr("Object_GID").ToString
                        'If distname.Contains("/WBTS") Then
                        'objlevel = "WBTS"
                        'ElseIf distname.Contains("/BTS") Then
                        ' objlevel = "BTS"
                        'Else
                        objlevel = Split(Split(distname, "/").Last, "-").First
                        'End If

                        objMO = objDom.CreateElement("managedObject")
                        objCMdata.AppendChild(objMO)
                        objMO.SetAttribute("class", objlevel)
                        objMO.SetAttribute("version", Version)
                        objMO.SetAttribute("distName", distname)
                        objMO.SetAttribute("id", id)
                        objMO.SetAttribute("operation", "update")
                    End If

                    objParam = objDom.CreateElement("p")
                    objMO.AppendChild(objParam)
                    objParam.SetAttribute("name", dr("ShortName").ToString.Trim)
                    objParam.InnerText = dr("DefaultValue").ToString.Trim
                Else
                End If
            Next

            'Save XML file
            objDom.Save(fn)
            Process.Start("explorer.exe", "/select," & fn)
            Return True
        Catch ex As Exception
            MsgBox("Failed writing XML: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function XML_Parameters_Validation(ByVal exporttype As String, ByRef dt As DataTable) As Boolean
        Try
            If exporttype = "NSN - RAML2.0" Then
                Dim validation As Integer = 0
                For Each col As DataColumn In dt.Columns
                    If col.Caption.ToUpper = "OBJECT_DN" Then
                        validation = validation + 1
                    End If
                    If col.Caption.ToUpper = "OBJECT_GID" Then
                        validation = validation + 1
                    End If
                    If col.Caption.ToUpper = "SHORTNAME" Then
                        validation = validation + 1
                    End If
                    If col.Caption.ToUpper = "DEFAULTVALUE" Then
                        validation = validation + 1
                    End If
                Next
                If validation = 4 Then
                    Return True
                End If
            End If
        Catch ex As Exception

        End Try
        Return False
    End Function

    Public Function Check_IOS_Table() As Boolean
        Dim dt_IOSSQL As DataTable = Nothing
        dt_IOSSQL = clsSQLCommands.Get_IOS_SQL_Data(connStrIOSServer)
        If dt_IOSSQL Is Nothing Then
            Return False
        Else
            dt_IOS_SQL = dt_IOSSQL
            Return True
        End If
    End Function

    Public Function cmbObjectTree_String2Index(ByRef cmb As ComboBoxEdit, ByVal theitem As String) As Integer
        Dim i As Integer = 0
        For i = 0 To cmb.Properties.Items.Count - 1
            If cmb.Properties.Items(i).ToString.ToUpper = theitem.ToUpper Then
                Return i
            End If
        Next
        Return Nothing
    End Function

    Public Function TileMenu_Status(ByRef tlb_Layer_GM As ToolStripSplitButton) As String
        ' Dim tm As Windows.Forms.ContextMenu = CType(MapToolBar1.Buttons("tlb_Layer_GM").DropDownMenu, Windows.Forms.ContextMenu)
        Try
            For Each item As ToolStripMenuItem In tlb_Layer_GM.DropDownItems
                If item.Checked = True Then
                    Return item.Text
                End If
            Next
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            Return Nothing
        End Try
        Return Nothing
    End Function

    Public Sub ConfigurForm(ByRef frm As Form, ByVal frmName As String, ByRef counter As Integer)
        Try
            Dim forms = From f In configMgr.IOSForms
                        Where f.Name.Trim().ToLower() = frmName.Trim.ToLower()
                        Select f
            If TypeOf (frm) Is frmTechnology Then
                Dim objTech As frmTechnology = CType(frm, frmTechnology)
                For Each form As IOS.Configuration.EntityModel.IOSForm In forms
                    If form.FindCategoriesByName(objTech.Network.ToUpper).Name = objTech.Network.ToUpper Then
                        For Each Item As Control In frm.Controls
                            Dim itemName As String = Item.Name.Trim().ToLower()
                            Dim c = From cat2 In form.Categories
                                    Where cat2.Name.ToUpper = objTech.Network.ToUpper
                                    Select cat2.FindByName(itemName)
                            If (c.Count() > 0) Then
                                If c(0) IsNot Nothing Then
                                    counter = counter + 1
                                    Item.ConfigurControl(c(0).ConfigType)
                                End If
                            End If
                            SearchChildControl(Item, form, counter, frm)
                        Next
                    End If
                Next
            Else
                For Each form As IOS.Configuration.EntityModel.IOSForm In forms
                    For Each Item As Control In frm.Controls
                        Dim itemName As String = Item.Name.Trim().ToLower()
                        Dim c = From cltr In form.Controls Where cltr.ControlId.Trim().ToLower() = itemName Select cltr
                        If (c.Count() > 0) Then
                            counter = counter + 1
                            Item.ConfigurControl(c(0).ConfigType)
                        End If
                        SearchChildControl(Item, form, counter)
                    Next
                Next
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Sub SearchChildControl(ByVal Item As Control, ByRef form As IOS.Configuration.EntityModel.IOSForm, ByRef counter As Integer, Optional ByRef frm As Form = Nothing)
        For Each ctrl As Control In Item.Controls
            Dim itemName As String = ctrl.Name.Trim().ToLower()

            Dim c = From cltr In form.Controls
                    Where cltr.ControlId.Trim().ToLower() = itemName
                    Select cltr

            If TypeOf (frm) Is frmTechnology Then
                Dim objTech As frmTechnology = CType(frm, frmTechnology)
                c = From cat2 In form.Categories
                    Where cat2.Name.ToUpper = objTech.Network.ToUpper
                    Select cat2.FindByName(itemName)
            End If

            If (c.Count() > 0) Then
                If c(0) IsNot Nothing Then
                    counter = counter + 1
                    ctrl.ConfigurControl(c(0).ConfigType)
                    If TypeOf (ctrl) Is DevExpress.XtraBars.Navigation.AccordionControl Then
                        Dim element As DevExpress.XtraBars.Navigation.AccordionControlElementCollection = CType(ctrl, DevExpress.XtraBars.Navigation.AccordionControl).Elements
                        For Each obj As Object In element
                            Dim e = From ele In form.Controls Where ele.ControlId.Trim().ToLower() = obj.Name.ToString.ToLower() Select ele
                            If (e.Count() > 0) Then
                                If e(0).ConfigType = Configuration.EntityModel.ConfigType.Hidden Then
                                    obj.Visible = False
                                Else
                                    obj.Visible = True
                                End If
                                If e(0).ConfigType = Configuration.EntityModel.ConfigType.Enable Then
                                    obj.Enabled = True
                                ElseIf e(0).ConfigType = Configuration.EntityModel.ConfigType.Disable Then
                                    obj.Enabled = False
                                End If
                            End If
                        Next
                    End If
                End If
            End If
            SearchChildControl(ctrl, form, counter, frm)
        Next
    End Sub

    Public Function ColumnInDataTable(ByVal columname As String, ByRef dt As DataTable) As Boolean
        For Each col As DataColumn In dt.Columns
            If col.Caption.ToString.Trim.ToUpper = columname.ToUpper Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function nEmpty(ByVal source As Object, ByVal defaultValue As String) As String
        If source.ToString = "" Then
            Return defaultValue
        Else
            Return source.ToString
        End If
    End Function

    Public Sub WriteString_Log(ByVal text2append As String)
        Try
            Dim FILE_NAME As String = GetUserDataPath() & "\session.log"
            Static LogFileLock As New Object()
            SyncLock LogFileLock
                File.AppendAllText(FILE_NAME, text2append & vbCrLf)
            End SyncLock
        Catch ex As Exception
        End Try
    End Sub

    Public Function GetUserDataPath() As String
        Dim basePath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
        Dim dataPath As String = String.Format("{0}\{1}\{2}\{3}", basePath, Application.CompanyName, Application.ProductName, IOSAppConfigManage.DeploymentName)
        If Not Directory.Exists(dataPath) Then
            Directory.CreateDirectory(dataPath)
        End If
        Return dataPath
    End Function

    'Public Function GetSharedNetworkPath(ByVal networkDate As String, ByVal networkArea As String) As String
    '    Return "Z:\NETWORK\" & networkDate.ToString("yyyyddMM") & "\" & networkArea
    'End Function

    Public Function EmbeddedImage(ByVal Name As String) As System.Drawing.Image
        Try
            Return System.Drawing.Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceStream("IOS." & Name))
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function nZ(ByVal source As Object, ByVal defaultValue As String) As String
        If source Is DBNull.Value Then
            Return defaultValue
        Else
            Return source.ToString
        End If
    End Function

    Public Function String2DataFields(ByRef str() As String, ByRef xval As String) As String
        Dim stroutput As String
        Dim i As Integer
        stroutput = "XValue=" & xval ' a(0)
        For i = 0 To UBound(str)
            stroutput = stroutput & "," & " Yvalue=" & str(i).Replace(",", "\,")
        Next
        String2DataFields = stroutput
    End Function

    Public Function ColorInt2Color(ByVal colorint As Integer) As Color
        Dim color_r, color_g, color_b As Integer
        color_b = CLng(colorint) Mod 256
        color_g = (CLng(colorint) \ 256) Mod 256
        color_r = ((CLng(colorint) \ 256) \ 256) Mod 256
        Return Color.FromArgb(255, color_r, color_g, color_b)
    End Function

    Public Function ColorInt2Color(ByVal colorint As Integer, transparency As Integer) As Color
        Dim color_r, color_g, color_b As Integer
        color_b = CLng(colorint) Mod 256
        color_g = (CLng(colorint) \ 256) Mod 256
        color_r = ((CLng(colorint) \ 256) \ 256) Mod 256
        Return Color.FromArgb(transparency, color_r, color_g, color_b)
    End Function

    Public Sub txtObjectsearch_KeyDown(ByRef tree As TreeView, ByVal text As String, ByRef e As System.Windows.Forms.KeyEventArgs)
        Dim tn As TreeNode = tree.SelectedNode
        If Not tn Is Nothing Then
            If e.KeyCode = Keys.Enter Then
                Treeview_NodeFound = False
                If TreeView_SearchFound = 0 Then
                    TreeView_SearchWildCard(tree.Nodes(0), text, Treeview_NodePosition(tree, tn), True)
                    If Treeview_NodeFound = False Then
                        TreeView_SearchWildCard(tree.Nodes(0), text, Treeview_NodePosition(tree, tn))
                    End If
                Else
                    TreeView_SearchFound = 0
                    TreeView_SearchWildCard(tree.Nodes(0), text, 0, False)
                End If
                If tn.Index = tree.SelectedNode.Index Then
                    tn.EnsureVisible()
                    tn.BackColor = Color.Coral
                Else
                    tn.BackColor = Color.White
                End If
            End If
        End If
    End Sub

    Public Sub txtObjectSearch_TextChanged(ByRef tree As TreeView, ByVal text As String, Optional ByVal isKPISearch As Boolean = False)
        Dim tn As TreeNode = tree.SelectedNode
        TreeView_SearchFound = 0
        Treeview_NodeFound = False
        If tree.Nodes.Count <> 0 Then
            If Not tn Is Nothing Then
                tn.BackColor = Color.White
            End If
            Dim tns() As TreeNode = tree.Nodes(0).Nodes.Find(text, True)
            If tns.Length > 0 Then
                TreeView_SearchWildCard(tree.Nodes(0), text, 0, True, isKPISearch)
                'tns(0).EnsureVisible()
                'If Not isKPISearch Then
                '    tns(0).TreeView.SelectedNode = tns(0)
                'End If
                'tns(0).BackColor = Color.Coral
                'TreeView_SearchFound = 1

                'Dim nd As TreeNode = tree.Nodes(0)
                'For Each nd In nd.Nodes
                '    If nd.Text.ToUpper <> tns(0).Text.ToUpper Then
                '        nd.BackColor = Color.White
                '    End If
                'Next
            Else
                TreeView_SearchWildCard(tree.Nodes(0), text, 0, False, isKPISearch)
            End If
        End If
    End Sub

    Public Function String2Index(ByRef cmb As ComboBoxEdit, ByVal strItem As String) As Integer
        Dim i As Integer = 0
        For i = 0 To cmb.Properties.Items.Count - 1
            If cmb.Properties.Items(i).ToString.ToUpper = strItem.ToUpper Then
                Return i
            End If
        Next
        Return Nothing
    End Function

    Public Sub GetProxyIfAvailable()
        Try
            _logger.SetDebug(System.Reflection.MethodBase.GetCurrentMethod().Name & " - Check Proxy")
            ProxyServer = IOS.Configuration.IOSAppConfigManage.ProxyServer
            If ProxyServer = "" Then
                'A proxy is not in use here
                _logger.SetDebug(System.Reflection.MethodBase.GetCurrentMethod().Name & " - No Proxy")
            Else
                'A proxy is in use
                objProxy = New WebProxy(ProxyServer, False)
                objProxy.UseDefaultCredentials = True
                objProxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials
                _logger.SetDebug(System.Reflection.MethodBase.GetCurrentMethod().Name & " - Trying Default Credentials")

                Try
                    Dim Request As HttpWebRequest = CType(WebRequest.Create("http://www.google.com"), HttpWebRequest)
                    Request.Proxy = objProxy
                    Request.Timeout = 5000
                    Dim response As System.Net.WebResponse = Request.GetResponse()
                    response.Close()
                Catch wx As WebException
                    _logger.SetDebug(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & wx.Message)
                    Select Case wx.Status
                        Case Net.WebExceptionStatus.Timeout
                            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & "Failed - " & wx.Message)
                        Case Net.WebExceptionStatus.ProtocolError
                            If DirectCast(wx.Response, HttpWebResponse).StatusCode = 407 Then
                                Dim cred As ICredentials
                                Dim proxyAuth As New frmProxyAuthentication()
                                If proxyAuth.ShowDialog() = DialogResult.OK Then
                                    cred = New NetworkCredential(proxyAuth.ProxyUsername, proxyAuth.ProxyPassword)
                                    objProxy = New WebProxy(ProxyServer, True, Nothing, cred)
                                    _logger.SetDebug(System.Reflection.MethodBase.GetCurrentMethod().Name & " - Manual Proxy Set")
                                End If
                            End If
                        Case Else
                            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & "Failed - " & wx.Message)
                    End Select
                End Try
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Public Function StringToDoubleArray(ByVal Input As String, ByVal Separators As String()) As Double()
        Dim StringArray() As String = Input.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
        Dim DoubleList As New List(Of Double)

        For x = 0 To StringArray.Length - 1
            Dim TempVal As Double
            If Double.TryParse(StringArray(x), TempVal) = True Then
                DoubleList.Add(TempVal)
            End If
        Next
        Return DoubleList.ToArray()
    End Function

    Public Function IsString(numString As String) As Boolean
        Dim numLong As Long = 0
        Dim canConvert As Boolean = Long.TryParse(numString, numLong)
        If canConvert = True Then
            Return False
        Else
            Dim numDecimal As Decimal = 0
            canConvert = Decimal.TryParse(numString, numDecimal)
            If canConvert = True Then
                Return False
            Else
                Return True
            End If
        End If
    End Function

    Public Function GetDecryptedConnectionString(ByVal connStringValue As String) As String
        Dim decryptedConnString As String = Nothing
        Dim objEncryptor As Aes256Base64Encrypter
        Try
            If connStringValue.ToUpper.StartsWith("DSN") Then
                decryptedConnString = connStringValue
            ElseIf connStringValue.ToUpper.StartsWith("DATA SOURCE") Then
                decryptedConnString = connStringValue
            ElseIf connStringValue.ToUpper.StartsWith("HOST") Then
                decryptedConnString = connStringValue
            Else
                objEncryptor = New Aes256Base64Encrypter
                decryptedConnString = objEncryptor.Decrypt(connStringValue, "c3lls3ns")
            End If
        Catch
        Finally
            objEncryptor = Nothing
        End Try
        Return decryptedConnString
    End Function

    Public Sub Insert_TiltCampaign_Manual(ByVal MBTSNAME As String, ByVal sectorID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@CampaignID", selectedTiltCampaignID},
            New String() {"@MBTSNAME", Chr(39) & MBTSNAME & Chr(39)},
            New String() {"@SECTORID", sectorID}
        }
        strConnection = GetSQL(4909, parray)(0)
        sqlParam = GetSQL(4909, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Public Function Save_ManualTiltCampaign_GetCampaignID() As Integer
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@CampaignName", Chr(39) & selectedTiltCampaignName & Chr(39)},
            New String() {"@UserName", Chr(39) & Environment.UserName & Chr(39)}
        }
        strConnection = GetSQL(4945, parray)(0)
        sqlParam = GetSQL(4945, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        If dt IsNot Nothing Then
            Return CInt(dt.Rows(0)(0).ToString)
        End If
        Return Nothing
    End Function

    Public Function Get_ManualTiltCampaignsList_CurrentUser() As DataTable
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@UserName", Chr(39) & Environment.UserName & Chr(39)}
        }
        strConnection = GetSQL(4946, parray)(0)
        sqlParam = GetSQL(4946, parray)(1)
        Return DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Function

    Public Function GetGridColumnsConfigDataTable(ByVal tblName As String) As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("GridName", GetType(String))
        dt.Columns.Add("ColumnName", GetType(String))
        dt.Columns.Add("ColumnVisibleIndex", GetType(Integer))
        dt.Columns.Add("ColumnWidth", GetType(Integer))
        dt.Columns.Add("ColumnFilter", GetType(String))
        dt.TableName = tblName
        Return dt
    End Function

    Public Sub ManageGridColumnsPosition(ByRef gv As Views.Grid.GridView)
        Dim colsList As List(Of Columns.GridColumn) = gv.Columns.ToList()
        If Not dsGridColumnsConfig.Tables.Contains(gv.Name) Then
            Dim dtNew As DataTable = GetGridColumnsConfigDataTable(gv.Name)
            For Each configCol As Columns.GridColumn In colsList
                Dim dr As DataRow = dtNew.NewRow()
                dr("GridName") = gv.Name
                dr("ColumnName") = configCol.FieldName
                dr("ColumnVisibleIndex") = configCol.VisibleIndex
                dr("ColumnWidth") = configCol.Width
                dr("ColumnFilter") = configCol.FilterInfo.FilterString
                dtNew.Rows.Add(dr)
            Next
            dsGridColumnsConfig.Tables.Add(dtNew)
        Else
            Dim dtConfig As DataTable = dsGridColumnsConfig.Tables(gv.Name)
            If Not dtConfig.Columns.Contains("ColumnFilter") Then
                dtConfig.Columns.Add("ColumnFilter", GetType(String))
            End If
            For Each configCol As Columns.GridColumn In colsList
                Dim dr As DataRow = dtConfig.Select("ColumnName='" & configCol.FieldName & "'")(0)
                dr("GridName") = gv.Name
                dr("ColumnName") = configCol.FieldName
                dr("ColumnVisibleIndex") = configCol.VisibleIndex
                dr("ColumnWidth") = configCol.Width
                dr("ColumnFilter") = configCol.FilterInfo.FilterString
                dtConfig.AcceptChanges()
            Next
        End If
    End Sub

    Public Sub ManageGridColumnsWidth(ByRef gv As Views.Grid.GridView, ByRef col As Columns.GridColumn)
        If Not dsGridColumnsConfig.Tables.Contains(gv.Name) Then
            Dim dtNew As DataTable = GetGridColumnsConfigDataTable(gv.Name)
            Dim colsList As List(Of Columns.GridColumn) = gv.Columns.ToList()
            For Each configCol As Columns.GridColumn In colsList
                Dim dr As DataRow = dtNew.NewRow()
                dr("GridName") = gv.Name
                dr("ColumnName") = configCol.FieldName
                dr("ColumnVisibleIndex") = configCol.VisibleIndex
                dr("ColumnWidth") = configCol.Width
                dr("ColumnFilter") = configCol.FilterInfo.FilterString
                dtNew.Rows.Add(dr)
            Next
            dsGridColumnsConfig.Tables.Add(dtNew)
        Else
            Dim dtConfig As DataTable = dsGridColumnsConfig.Tables(gv.Name)
            If Not dtConfig.Columns.Contains("ColumnFilter") Then
                dtConfig.Columns.Add("ColumnFilter", GetType(String))
            End If
            Dim dr As DataRow = dtConfig.Select("ColumnName='" & col.FieldName & "'")(0)
            dr("ColumnWidth") = col.Width
            dtConfig.AcceptChanges()
        End If
    End Sub

    Public Sub ManageGridColumnsFilter(ByRef gv As Views.Grid.GridView, ByRef col As Columns.GridColumn)
        If Not dsGridColumnsConfig.Tables.Contains(gv.Name) Then
            Dim dtNew As DataTable = GetGridColumnsConfigDataTable(gv.Name)
            Dim colsList As List(Of Columns.GridColumn) = gv.Columns.ToList()
            For Each configCol As Columns.GridColumn In colsList
                Dim dr As DataRow = dtNew.NewRow()
                dr("GridName") = gv.Name
                dr("ColumnName") = configCol.FieldName
                dr("ColumnVisibleIndex") = configCol.VisibleIndex
                dr("ColumnWidth") = configCol.Width
                dr("ColumnFilter") = configCol.FilterInfo.FilterString
                dtNew.Rows.Add(dr)
            Next
            dsGridColumnsConfig.Tables.Add(dtNew)
        Else
            Dim dtConfig As DataTable = dsGridColumnsConfig.Tables(gv.Name)
            If Not dtConfig.Columns.Contains("ColumnFilter") Then
                dtConfig.Columns.Add("ColumnFilter", GetType(String))
            End If
            Dim dr As DataRow = dtConfig.Select("ColumnName='" & col.FieldName & "'")(0)
            dr("ColumnFilter") = col.FilterInfo.FilterString
            dtConfig.AcceptChanges()
        End If
    End Sub

    Public Function XMLToDataSet(ByVal xmlStr As String, ByVal schemaFile As String) As DataSet
        'Convert the XML to a dataset
        Dim sr As New System.IO.StringReader(xmlStr)

        'Convert xmlData to a Dataset
        Dim ds As New DataSet

        If schemaFile = String.Empty Then
            ds.ReadXml(sr, XmlReadMode.InferSchema)
        Else
            ds.ReadXmlSchema(schemaFile)
            ds.ReadXml(sr, XmlReadMode.ReadSchema)
        End If

        For Each relation As DataRelation In ds.Relations
            For Each c As DataColumn In relation.ParentColumns
                If Not relation.ChildTable.Columns.Contains(c.ColumnName) Then
                    relation.ChildTable.Columns.Add(c)
                End If
                For Each dr As DataRow In relation.ChildTable.Rows
                    dr(c.ColumnName) = dr.GetParentRow(relation)(c.ColumnName)
                Next
            Next
        Next

        Return ds
    End Function

    Public Sub SetGridHorizontalScrollToRight(ByRef gv As GridView)
        Dim width As Integer = 0
        Try
            'Dim gvInfo As GridViewInfo = TryCast(gv.GetViewInfo, GridViewInfo)
            'Dim rect As System.Drawing.Rectangle = gvInfo.Bounds
            For Each col As Columns.GridColumn In gv.Columns
                width += col.Width
            Next
            gv.LeftCoord = width
        Catch
        Finally
            width = Nothing
        End Try
    End Sub

    Public Sub SetGridHorizontalScrollToLeft(ByRef gv As GridView)
        gv.LeftCoord = 0
    End Sub

    Public Function SplitWithQuotes(ByVal str As String, ByVal delim As String) As String()
        Dim theString As String = str
        Dim output As New List(Of String)
        Using rdr As New StringReader(theString)
            Using parser As New Microsoft.VisualBasic.FileIO.TextFieldParser(rdr)
                parser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
                parser.Delimiters = New String() {delim}
                parser.HasFieldsEnclosedInQuotes = True
                Dim fields() As String = parser.ReadFields()

                Return fields

            End Using
        End Using
        Return Nothing

    End Function

    Public Function GetDatatableFromCSVFile(ByVal csvFileName As String) As DataTable
        Dim dt As New DataTable()
        Dim filePath As String = IOSAppConfigManage.GetSaveEricssonXmlFilePath
        Dim sr As StreamReader = New StreamReader(filePath + "\" + csvFileName)
        Dim line As String = sr.ReadLine()
        Dim strArray As String() = line.Split(","c)

        Dim row As DataRow

        For Each s As String In strArray
            dt.Columns.Add(New DataColumn())
        Next

        Do
            line = sr.ReadLine
            If Not line = String.Empty Then
                row = dt.NewRow()
                row.ItemArray = SplitWithQuotes(line, ",")
                dt.Rows.Add(row)
            Else
                Exit Do
            End If
        Loop
        Return dt
    End Function

    Public Sub LoadAllCrystalReports()
        Dim connStr As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        connStr = GetSQL(7036, parray)(0)
        sqlParam = GetSQL(7036, parray)(1)
        dtCrystalReports = DataAccessorODBC.GetDataTable(connStr, sqlParam)
    End Sub

    Public Sub LoadAllDashboardReports()
        Dim connStr As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        connStr = GetSQL(8102, parray)(0)
        sqlParam = GetSQL(8102, parray)(1)
        dtDashboardReports = DataAccessorODBC.GetDataTable(connStr, sqlParam)
    End Sub

    Public Sub ExportDataTableToExcel_Stream(ByRef dtData As DataTable, fileName As String)
        Try
            Dim exporter As IXlExporter = XlExport.CreateExporter(XlDocumentFormat.Xlsx)

            Using stream As New FileStream(fileName, FileMode.Create, FileAccess.ReadWrite)
                Using document As IXlDocument = exporter.CreateDocument(stream)
                    Using sheet As IXlSheet = document.CreateSheet()

                        ' 1. Create Columns first (Optimized)
                        For Each dtCol As DataColumn In dtData.Columns
                            Using col As IXlColumn = sheet.CreateColumn()
                                col.WidthInPixels = 150
                            End Using
                        Next

                        ' 2. Define Shared Formatting
                        Dim headerRowFormatting As New XlCellFormatting()
                        headerRowFormatting.Font = New XlFont() With {.Bold = True, .Name = "Calibri"}

                        Dim dateFormatting As New XlCellFormatting()
                        dateFormatting.NumberFormat = "yyyy-MM-dd HH:mm:ss" ' Change to your preferred display

                        ' 3. Create Header Row
                        Using row As IXlRow = sheet.CreateRow()
                            For Each dtCol As DataColumn In dtData.Columns
                                Using cell As IXlCell = row.CreateCell()
                                    cell.Value = dtCol.ColumnName
                                    cell.ApplyFormatting(headerRowFormatting)
                                End Using
                            Next
                        End Using

                        ' 4. Create Data Rows
                        For Each drData As DataRow In dtData.Rows
                            Using row As IXlRow = sheet.CreateRow()
                                For Each dtCol As DataColumn In dtData.Columns
                                    Using cell As IXlCell = row.CreateCell()

                                        Dim val As Object = drData(dtCol)

                                        If IsDBNull(val) Then
                                            cell.Value = XlVariantValue.Empty
                                        ElseIf dtCol.DataType = GetType(DateTime) Then
                                            cell.Value = DirectCast(val, DateTime)
                                            cell.ApplyFormatting(dateFormatting) ' Apply the date style
                                        ElseIf IsNumeric(val) AndAlso Not dtCol.DataType = GetType(String) Then
                                            cell.Value = Convert.ToDouble(val)
                                        Else
                                            cell.Value = val.ToString()
                                        End If

                                    End Using
                                Next
                            End Using
                        Next

                    End Using
                End Using
            End Using
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Public Function GetEncryptedString(ByVal str2Encrypt As String) As String
        Dim encryptedString As String = Nothing
        Dim objEncryptor As New Aes256Base64Encrypter()
        Try
            encryptedString = objEncryptor.Encrypt(str2Encrypt, "c3lls3ns")
        Catch
        Finally
            objEncryptor = Nothing
        End Try
        Return encryptedString
    End Function

    Public Function InternetCheck() As Boolean
        Try
            Dim ping As New Ping()
            Dim reply As PingReply = ping.Send("8.8.8.8") ' Google's public DNS server

            If reply.Status = IPStatus.Success Then
                bInternetAvailable = True
                Return bInternetAvailable
            End If
        Catch ex As Exception
            ' Handle exception if ping fails
        End Try
        bInternetAvailable = False
        Return bInternetAvailable
    End Function

    Public Function IsInternetAvailable() As Boolean
        Try
            Dim request As HttpWebRequest = WebRequest.Create("http://google.com")

            request.Proxy = WebRequest.GetSystemWebProxy()
            request.Proxy.Credentials = CredentialCache.DefaultCredentials

            request.Method = "HEAD"
            request.Timeout = 2000 ' 2 second timeout to prevent hanging

            Using response As HttpWebResponse = request.GetResponse()
                If response.StatusCode = HttpStatusCode.NoContent OrElse response.StatusCode = HttpStatusCode.OK Then
                    bInternetAvailable = True
                    Return bInternetAvailable
                End If
            End Using
        Catch ex As Exception
            bInternetAvailable = False
            Return bInternetAvailable
        End Try
        Return bInternetAvailable
    End Function

    Public Function GetStartOfWeek(inputDate As Date, culture As Globalization.CultureInfo) As Date
        Dim diff As Integer = (7 + (inputDate.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek)) Mod 7
        Return inputDate.AddDays(-diff).Date
    End Function

    Public Function GetFirstDayOfMonth(inputDate As Date) As Date
        Return New Date(inputDate.Year, inputDate.Month, 1)
    End Function

    Public Function MergeTablesByPrefix(src As DataSet) As DataSet
        Dim resultDS As New DataSet()

        ' Group table names by prefix (text before first underscore).
        Dim groups = src.Tables.Cast(Of DataTable)().GroupBy(Function(t) GetPrefix(t.TableName)).ToDictionary(Function(g) g.Key, Function(g) g.ToList())

        For Each kvp In groups
            Dim prefix As String = kvp.Key
            Dim tables As List(Of DataTable) = kvp.Value

            ' If only one table with this prefix, clone it into result (optionally you could still re-create)
            If tables.Count = 1 Then
                resultDS.Tables.Add(tables(0).Copy()) ' copy to avoid referencing original
                resultDS.Tables(resultDS.Tables.Count - 1).TableName = prefix
                Continue For
            End If

            ' Build a new DataTable that contains the union of columns from all tables
            Dim merged As New DataTable(prefix)

            ' Collect columns (preserve datatype and allow DBNull)
            For Each t In tables
                For Each col As DataColumn In t.Columns
                    If Not merged.Columns.Contains(col.ColumnName) Then
                        ' Create a new column using the same DataType, allow DBNull
                        Dim newCol As New DataColumn(col.ColumnName, col.DataType)
                        newCol.AllowDBNull = True
                        merged.Columns.Add(newCol)
                    Else
                        ' If exists but type differs, you could choose to handle conversion.
                        ' Here we leave the existing type (first seen).
                        ' Optionally you could upgrade to Object type if mismatch:
                        Dim existing = merged.Columns(col.ColumnName)
                        If existing.DataType IsNot col.DataType Then
                            ' downgrade to Object to be safe
                            existing.DataType = GetWiderType(existing.DataType, col.DataType)
                        End If
                    End If
                Next
            Next

            ' Now import rows from each table (assign by column name)
            For Each t In tables
                For Each r As DataRow In t.Rows
                    Dim newRow As DataRow = merged.NewRow()
                    For Each col As DataColumn In t.Columns
                        ' If merged table doesn't have the column (shouldn't happen) skip
                        If Not merged.Columns.Contains(col.ColumnName) Then Continue For
                        newRow(col.ColumnName) = If(r.IsNull(col), DBNull.Value, r(col))
                    Next
                    merged.Rows.Add(newRow)
                Next
            Next

            resultDS.Tables.Add(merged)
        Next

        Return resultDS
    End Function

    Public Sub MergeTablesByPrefix_MergeMethod(ByRef src As DataSet)
        Dim resultDS As New DataSet()

        ' Group table names by prefix (text before first underscore).
        Dim groups = src.Tables.Cast(Of DataTable)().GroupBy(Function(t) GetPrefix(t.TableName)).ToDictionary(Function(g) g.Key, Function(g) g.ToList())

        For Each kvp In groups
            Dim prefix As String = kvp.Key
            Dim tables As List(Of DataTable) = kvp.Value

            ' If only one table with this prefix, clone it into result (optionally you could still re-create)
            If tables.Count = 1 Then
                resultDS.Tables.Add(tables(0).Copy()) ' copy to avoid referencing original
                resultDS.Tables(resultDS.Tables.Count - 1).TableName = prefix
                Continue For
            End If

            'identify primkeys
            For Each t In tables
                Try
                    Dim primkeys() As DataColumn
                    Dim i As Int16 = 0

                    For Each col As DataColumn In t.Columns
                        If col.DataType = GetType(System.String) Or col.DataType = GetType(System.DateTime) Then
                            ReDim Preserve primkeys(i)
                            primkeys(i) = col
                            col.AllowDBNull = False
                            i = i + 1
                        End If
                    Next

                    If Not primkeys Is Nothing AndAlso primkeys.Length > 0 Then
                        t.PrimaryKey = primkeys
                    End If

                Catch ex As Exception
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                End Try
            Next

            Dim lst_table2remove As New List(Of String)

            Dim tablecount As Int16 = tables.Count
            For i = 1 To tables.Count - 1
                Try
                    tables(0).Merge(tables(i))
                Catch ex As Exception
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                End Try
                lst_table2remove.Add(tables(i).TableName)
            Next
            For Each str As String In lst_table2remove
                src.Tables.Remove(str)
            Next
        Next
    End Sub

    Private Function GetPrefix(tableName As String) As String
        If String.IsNullOrEmpty(tableName) Then Return tableName
        Dim idx As Integer = tableName.IndexOf("_"c)
        If idx <= 0 Then
            Return tableName.Trim()
        End If
        Return tableName.Substring(0, idx).Trim()
    End Function

    Private Function GetWiderType(t1 As Type, t2 As Type) As Type
        If t1 Is t2 Then Return t1
        ' If either is Object, return Object
        If t1 Is GetType(Object) OrElse t2 Is GetType(Object) Then Return GetType(Object)
        ' If one is nullable underlying type, simplify not handled here
        ' Fallback: use Object to avoid data loss
        Return GetType(Object)
    End Function

    Public Function ConvertCSVToDataTable(ByVal filePath As String) As DataTable
        Dim dt As New DataTable()

        ' Ensure the file exists
        'If Not File.Exists(filePath) Then
        '    Throw New FileNotFoundException($"The file was not found: {filePath}")
        'End If

        Using parser As New TextFieldParser(filePath)
            ' Set the parser to treat the file as delimited text
            parser.TextFieldType = FileIO.FieldType.Delimited
            parser.SetDelimiters(",") ' Specify the delimiter
            parser.HasFieldsEnclosedInQuotes = True ' Handle fields enclosed in quotes, e.g., "Smith, John"

            ' 1. Read the header row and create columns
            Dim headers As String() = parser.ReadFields()
            If headers IsNot Nothing Then
                For Each header In headers
                    dt.Columns.Add(New DataColumn(header, GetType(String)))
                Next
            End If

            ' 2. Read the data rows
            While Not parser.EndOfData
                Dim fields() As String = parser.ReadFields()
                If fields IsNot Nothing Then
                    ' Ensure the number of fields matches the number of columns to prevent errors
                    If fields.Length = dt.Columns.Count Then
                        dt.Rows.Add(fields)
                    Else
                        ' Handle cases where rows have a variable number of columns if necessary
                        ' You might log a warning or adjust the logic based on your needs
                        Console.WriteLine("Warning: Skipping row due to mismatched field count.")
                    End If
                End If
            End While
        End Using

        Return dt
    End Function

#End Region

#Region "Bind ComboBox Methods"

    Public Function GetComboItemFromValue(ByVal value As Object, ByRef ctrlCmb As ComboBoxEdit) As clsComboBoxItem
        Try
            For Each obj As clsComboBoxItem In ctrlCmb.Properties.Items
                If obj.Value = value Then
                    Return obj
                End If
            Next
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetComboItemFromIndex(ByVal index As Integer, ByRef ctrlCmb As ComboBoxEdit) As clsComboBoxItem
        Try
            Dim obj As clsComboBoxItem = ctrlCmb.Properties.Items(index)
            Return obj
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetComboItemFromText(ByVal value As String, ByRef ctrlCmb As ComboBoxEdit) As clsComboBoxItem
        Try
            For Each obj As clsComboBoxItem In ctrlCmb.Properties.Items
                If obj.ToString = value Then
                    Return obj
                End If
            Next
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub BindDevExCheckedComboBox(ByRef ctrlCmb As CheckedComboBoxEdit, ByVal dtData As DataTable,
                                        ByVal valueField As String, ByVal displayField As String, Optional ByVal defaultText As String = Nothing)
        ctrlCmb.SuspendLayout()
        ctrlCmb.Properties.Items.Clear()
        ctrlCmb.Refresh()
        If (defaultText IsNot Nothing) Then
            ctrlCmb.Properties.Items.Add(defaultText)
        End If
        If (dtData IsNot Nothing) Then
            For Each Item As DataRow In dtData.Rows
                Dim objItem As New clsComboBoxItem()
                objItem.Text = Item(displayField)
                objItem.Value = Item(valueField)
                objItem.Tag = Item(valueField)

                Dim cmbItem As New Controls.ComboBoxItem()
                cmbItem.Value = objItem

                ctrlCmb.Properties.Items.Add(cmbItem)
            Next
        End If
        ctrlCmb.Update()
        ctrlCmb.ResumeLayout()
    End Sub

    Public Sub BindDevExComboBoxWithValueMember(ByRef ctrlCmb As ComboBoxEdit, ByVal dtData As DataTable,
                                 ByVal valueField As String, ByVal displayField As String, Optional ByVal defaultText As String = Nothing,
                                 Optional ByVal isFirstItemSelected As Boolean = False)

        ctrlCmb.SuspendLayout()
        ctrlCmb.Properties.Items.Clear()

        If (defaultText IsNot Nothing) Then

            Dim cmbValue As New clsComboBoxItem()
            cmbValue.Text = defaultText
            cmbValue.Value = Nothing

            Dim cmbItem As New Controls.ComboBoxItem()
            cmbItem.Value = cmbValue

            ctrlCmb.Properties.Items.Insert(0, cmbItem)
        End If
        ctrlCmb.Refresh()

        Dim defaultItem As clsComboBoxItem = Nothing
        If (dtData IsNot Nothing) Then
            For Each Item As DataRow In dtData.Rows

                Dim cmbValue As New clsComboBoxItem()
                cmbValue.Text = Item(displayField)
                cmbValue.Value = Item(valueField)

                If dtData.Columns.Contains("IsDefaultPeriod") Then
                    If Item("IsDefaultPeriod") Then
                        defaultItem = cmbValue
                    End If
                End If

                Dim cmbItem As New Controls.ComboBoxItem()
                cmbItem.Value = cmbValue

                ctrlCmb.Properties.Items.Add(cmbItem)
            Next
        End If
        If (defaultText IsNot Nothing) Then ctrlCmb.SelectedIndex = 0

        If isFirstItemSelected Then
            If (dtData IsNot Nothing) Then
                If dtData.Rows.Count > 0 Then
                    ctrlCmb.SelectedIndex = 0
                End If
            End If
        End If

        If defaultItem IsNot Nothing Then
            ctrlCmb.SelectedItem = defaultItem
        End If

        ctrlCmb.Update()
        ctrlCmb.ResumeLayout()
    End Sub

    Public Sub BindDevExRepositoryItemComboBoxWithValueMember(ByRef ctrlCmb As RepositoryItemComboBox, ByVal dtData As DataTable,
                                 ByVal valueField As String, ByVal displayField As String, Optional ByVal defaultText As String = Nothing,
                                 Optional ByVal isFirstItemSelected As Boolean = False)

        'ctrlCmb.SuspendLayout()
        ctrlCmb.Items.Clear()

        If (defaultText IsNot Nothing) Then

            Dim cmbValue As New clsComboBoxItem()
            cmbValue.Text = defaultText
            cmbValue.Value = Nothing

            Dim cmbItem As New Controls.ComboBoxItem()
            cmbItem.Value = cmbValue

            ctrlCmb.Items.Insert(0, cmbItem)
        End If
        'ctrlCmb.Refresh()

        Dim defaultItem As clsComboBoxItem = Nothing
        If (dtData IsNot Nothing) Then
            For Each Item As DataRow In dtData.Rows

                Dim cmbValue As New clsComboBoxItem()
                cmbValue.Text = Item(displayField)
                cmbValue.Value = Item(valueField)

                If dtData.Columns.Contains("IsDefaultPeriod") Then
                    If Item("IsDefaultPeriod") Then
                        defaultItem = cmbValue
                    End If
                End If

                Dim cmbItem As New Controls.ComboBoxItem()
                cmbItem.Value = cmbValue

                ctrlCmb.Items.Add(cmbItem)
            Next
        End If
        'If (defaultText IsNot Nothing) Then ctrlCmb.SelectedIndex = 0

        If isFirstItemSelected Then
            If (dtData IsNot Nothing) Then
                If dtData.Rows.Count > 0 Then
                    'ctrlCmb.SelectedIndex = 0
                End If
            End If
        End If

        If defaultItem IsNot Nothing Then
            'ctrlCmb.SelectedItem = defaultItem
        End If

        'ctrlCmb.Update()
        'ctrlCmb.ResumeLayout()
    End Sub

    Public Sub BindDevExComboBoxWithTagMember(ByRef ctrlCmb As ComboBoxEdit, ByVal dtData As DataTable, ByVal valueField As String, ByVal displayField As String,
                                              Optional ByVal defaultText As String = Nothing, Optional tagValue As String = Nothing, Optional ByVal isFirstItemSelected As Boolean = False)

        ctrlCmb.SuspendLayout()
        ctrlCmb.Properties.Items.Clear()

        If (defaultText IsNot Nothing) Then

            Dim cmbValue As New clsComboBoxItem()
            cmbValue.Text = defaultText
            cmbValue.Value = Nothing
            cmbValue.Tag = Nothing

            Dim cmbItem As New Controls.ComboBoxItem()
            cmbItem.Value = cmbValue

            ctrlCmb.Properties.Items.Insert(0, cmbItem)
        End If
        ctrlCmb.Refresh()

        If (dtData IsNot Nothing) Then
            For Each Item As DataRow In dtData.Rows

                Dim cmbValue As New clsComboBoxItem()
                cmbValue.Text = Item(displayField)
                cmbValue.Value = Item(valueField)
                cmbValue.Tag = Item(tagValue)

                Dim cmbItem As New Controls.ComboBoxItem()
                cmbItem.Value = cmbValue

                ctrlCmb.Properties.Items.Add(cmbItem)
            Next
        End If
        If (defaultText IsNot Nothing) Then ctrlCmb.SelectedIndex = 0
        If isFirstItemSelected Then
            If (dtData IsNot Nothing) Then
                If dtData.Rows.Count > 0 Then
                    ctrlCmb.SelectedIndex = 0
                End If
            End If
        End If
        ctrlCmb.Update()
        ctrlCmb.ResumeLayout()
    End Sub

    Public Sub ClearComboBox(ByRef cmb As ComboBoxEdit, ByVal defaultText As String)
        cmb.SuspendLayout()
        cmb.Properties.Items.Clear()
        cmb.Refresh()
        Dim objItem As New clsComboBoxItem()
        objItem.Text = defaultText
        objItem.Value = -1
        cmb.Properties.Items.Add(objItem)
        cmb.Update()
        cmb.ResumeLayout()
        If (Not cmb.SelectedIndex = 0) Then
            cmb.SelectedIndex = 0
        End If
    End Sub

    Public Sub SetComboBox(ByRef cmb As ComboBoxEdit, ByVal textOrValueBased As ComboSelectBased, ByVal targetValue As String)
        Try
            If textOrValueBased = ComboSelectBased.ValueBased Then
                If targetValue = cmb.SelectedItem.ToString Then
                    Exit Sub
                End If
                For Each cbItem As clsComboBoxItem In cmb.Properties.Items
                    If cbItem.Value = targetValue Then
                        cmb.SelectedItem = cbItem
                        Exit For
                    End If
                Next
            ElseIf textOrValueBased = ComboSelectBased.TextBased Then
                If targetValue = cmb.SelectedItem.ToString Then
                    Exit Sub
                End If
                For Each cbItem As clsComboBoxItem In cmb.Properties.Items
                    If cbItem.Text = targetValue Then
                        cmb.SelectedItem = cbItem
                        Exit For
                    End If
                Next
            End If
        Catch
        End Try
    End Sub

    Public Sub BindDevExLookUpEdit(ByRef ctrlCmb As LookUpEdit, ByVal dtData As DataTable, ByVal valueField As String, ByVal displayField As String, Optional ByVal defaultText As String = Nothing)
        ctrlCmb.SuspendLayout()
        ctrlCmb.Properties.DataSource = Nothing
        ctrlCmb.Properties.ValueMember = Nothing
        ctrlCmb.Properties.DisplayMember = Nothing
        ctrlCmb.Refresh()

        If (defaultText IsNot Nothing) Then
            Dim dr As DataRow = dtData.NewRow
            dr.Item(0) = -1
            dr.Item(0) = defaultText
            dtData.Rows.InsertAt(dr, 0)
            dtData.AcceptChanges()
        End If

        ctrlCmb.Properties.DisplayMember = displayField
        ctrlCmb.Properties.ValueMember = valueField
        ctrlCmb.Properties.DataSource = dtData
        ctrlCmb.Properties.PopulateColumns()
        ctrlCmb.Refresh()
        ctrlCmb.ResumeLayout()
    End Sub

    Public Sub BindCNEDatasourceCombo(ByRef cmb As ComboBoxEdit)
        Try
            If dtCneDataSource Is Nothing Then
                dtCneDataSource = clsSQLCommands.GetCneDataSourceComboBox(connStrIOSServer)
            End If
            If dtCneDataSource IsNot Nothing Then
                BindDevExComboBoxWithValueMember(cmb, dtCneDataSource, "CNE_SourceID", "CNE_SourceName", "Select", True)
                cmb.SelectedIndex = 1
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub BindComboWithPredefinedPeriod(ByRef cmb As ComboBoxEdit)
        Try
            If dtPredefinePeriod Is Nothing Then
                dtPredefinePeriod = clsSQLCommands.GetPredefinedPeriodComboBox(connStrIOSServer)
            End If
            If dtPredefinePeriod IsNot Nothing Then
                Dim cmbName As String = cmb.Name
                If dtPredefinePeriod.AsEnumerable().Where(Function(x) x.Field(Of String)("Control") = cmbName).Count > 0 Then
                    cmb.Enabled = True
                    BindDevExComboBoxWithValueMember(cmb, dtPredefinePeriod.AsEnumerable().Where(Function(x) x.Field(Of String)("Control") = cmbName).CopyToDataTable(), "PredefinedPeriodID", "GUIText", "Select", True)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindComboWithChartSetName(ByRef dt As DataTable, ByRef cmb As ComboBoxEdit)
        Try
            BindDevExComboBoxWithValueMember(cmb, dt, "IsDefault", "ChartSetName", "Select", True)
            SetComboBox(cmb, ComboSelectBased.ValueBased, 1)
        Catch ex As Exception
        End Try
    End Sub

    Public Sub BindComboWithAlignInterval(ByRef cmb As ComboBoxEdit)
        Try
            Dim dt As New DataTable
            dt = clsSQLCommands.GetChartAlignIntervalSet(connStrIOSServer)
            If dt.IsValid() Then
                BindDevExComboBoxWithValueMember(cmb, dt, "IntervalValue", "IntervalName")
                SetComboBox(cmb, ComboSelectBased.ValueBased, 7)
            End If
        Catch
        End Try
    End Sub

#End Region

#Region "Map Window Methods"

    Public Sub AttachAutoCompleteWithTextBox(ByRef txt As TextEdit, ByVal columnName As String, Optional market As String = "ALL")
        'Auto Complete
        Try
            Dim str() As String = Nothing
            Dim tempDT As New DataTable
            If market.ToUpper = "ALL" Then
                tempDT = DataAccessorODBC.GetDataTable(connStrIOSServer, "Select Distinct [" & columnName & "] From [dbo].[IOS_Network_All]")
            Else
                tempDT = DataAccessorODBC.GetDataTable(connStrIOSServer, "Select Distinct [" & columnName & "] From [dbo].[IOS_Network_All] Where [MARKET] = '" & market & "'")
            End If
            str = tempDT.Rows.OfType(Of DataRow)().[Select](Function(k) k(0).ToString()).ToArray()

            Dim collection As New AutoCompleteStringCollection()
            collection.AddRange(str)
            txt.MaskBox.AutoCompleteCustomSource = collection
            txt.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource
            txt.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        Catch ex As Exception
        End Try
    End Sub

    Public Function GetNetworkTabFiles(ByVal selectedDate As String)
        Try
            dt_Network_TabFiles = DataAccessorODBC.GetDataTable(connStrIOSServer, "Select * From IOS_Network_TabFiles Where NetworkDate = " & selectedDate)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
        Return Nothing
    End Function

    Public Function GetDataTable(ByVal str As String) As DataTable
        If str = "dt_Map_Configuration" Then
            Return dt_Map_Configuration
        End If

        If str = "dt_IOS_SQL" Then
            Return dt_IOS_SQL
        End If

        If str = "dt_OSS_Userparams_3G" Then
            Return dt_OSS_3G_UserParams
        End If

        If str = "dt_OSS_Userparams_2G" Then
            Return dt_OSS_2G_UserParams
        End If

        If str = "dt_IOS_OSSParams" Then
            Return dt_IOS_OSSParams
        End If
        Return Nothing
    End Function

    Public Function GetGeomatryFromGeomatryString(ByVal featureGeomatryValue As String, ByVal csysWGS84 As MapInfo.Geometry.CoordSys) As Geometry
        Dim geometry As MapInfo.Geometry.Geometry = Nothing
        geometry = New MapInfo.Ogc.FeatureGeometryFactory(csysWGS84).FeatureGeometryFromWKT(featureGeomatryValue)
        Return geometry
    End Function

    Public Function GetGeomatryStringFromGeomatry(ByVal featureGeometry As FeatureGeometry) As String
        Dim wktValue As String = Nothing
        Dim feaGemfactory As FeatureGeometryFactory = New FeatureGeometryFactory(featureGeometry.CoordSys)
        If (feaGemfactory IsNot Nothing) Then
            If (featureGeometry.Type = GeometryType.Ellipse) Then
                featureGeometry = CType(featureGeometry, MapInfo.Geometry.Ellipse).CreateMultiPolygon(20)
            End If
            wktValue = feaGemfactory.FeatureGeometryToWKT(featureGeometry)
        End If
        Return wktValue
    End Function

    Public Function GetSytle(ByVal tableName As TableNames) As MapInfo.Styles.Style
        If (tableName = TableNames.DT_Scan2G_Parallel Or tableName = TableNames.DT_Scan3G_Parallel Or tableName = TableNames.DT_Scan4G_Parallel Or tableName = TableNames.DT_UE2G_Parallel Or tableName = TableNames.DT_UE3G_Parallel Or tableName = TableNames.DT_UE4G_Parallel Or tableName = TableNames.DT_Compare Or tableName = TableNames.DT_EventGrid Or tableName = TableNames.CellFootPrint) Then
            Return New MapInfo.Styles.CompositeStyle(New MapInfo.Styles.FontPointStyle(33, New MapInfo.Styles.Font("MapInfo Symbols", 6), 0, Color.Black, 8))
        ElseIf (tableName = TableNames.DT_Events Or tableName = TableNames.DT_Events_GetEvents) Then
            '' Return New BitmapPointStyle("",BitmapStyles.ApplyColor,Color.Green,24);
            Return New MapInfo.Styles.CompositeStyle(New MapInfo.Styles.FontPointStyle(42, New MapInfo.Styles.Font("MapInfo Symbols", 20), 0, Color.Black, 36))

        End If
        Return Nothing
    End Function

    Public Sub Mapcontrol_CloseTable(ByVal tbl As String)
        MapInfo.Engine.Session.Current.Catalog.CloseTable(tbl)
    End Sub

    Public Sub TileMap_Bounds(ByVal x1 As Double, ByVal y1 As Double, ByVal x2 As Double, ByVal y2 As Double)
        Dim pt1 As MapInfo.Geometry.DPoint = New MapInfo.Geometry.DPoint(x1, y1)
        Dim pt2 As MapInfo.Geometry.DPoint = New MapInfo.Geometry.DPoint(x2, y2)

        Dim rct As MapInfo.Geometry.DRect = New MapInfo.Geometry.DRect(pt1, pt2)
        MapInfo.Engine.Session.Current.MapFactory(0).SetView(rct, csysWGS84)
    End Sub

    Public Function IntegerToColor(ByVal transparency As Integer, ByVal rgbIneger As Integer) As System.Drawing.Color
        Dim BColorValue As Integer = CLng(rgbIneger) Mod 256
        Dim GColorValue As Integer = (CLng(rgbIneger) \ 256) Mod 256
        Dim RColorValue As Integer = ((CLng(rgbIneger) \ 256) \ 256) Mod 256
        Return Color.FromArgb(transparency, RColorValue, GColorValue, BColorValue)
    End Function

    Public Function IntegerToColorOle(ByVal transparency As Integer, ByVal rgbIneger As Integer) As System.Drawing.Color
        Dim RColorValue As Integer = CLng(rgbIneger) Mod 256
        Dim GColorValue As Integer = (CLng(rgbIneger) \ 256) Mod 256
        Dim BColorValue As Integer = ((CLng(rgbIneger) \ 256) \ 256) Mod 256
        Return Color.FromArgb(transparency, RColorValue, GColorValue, BColorValue)
    End Function

    Public Sub OpenLayoutAndWorkspace(ByVal layoutFilePath As String, ByVal layoutFileName As String)
        Try
            If File.Exists(layoutFilePath & "\" & layoutFileName & "1.xml") Then
                frmMDI.dmMDI.RestoreLayoutFromXml(layoutFilePath & "\" & layoutFileName & "1.xml")
                frmMDI.OpenDockPanelWithFormObjectForMDI()
                frmMapWindow.dmMap.RestoreLayoutFromXml(layoutFilePath & "\" & layoutFileName & "2.xml")
                frmMDI.OpenDockPanelWithFormObjectForMap()
            End If

            Dim wsl1 As New MapInfo.Persistence.WorkSpaceLoader(layoutFilePath & "\" & layoutFileName & ".mws")
            wsl1.Load(frmMapWindow.MapControl1.Map)
            frmMapWindow.QuickLayerAlignWithWorkspace()

            WaitScreen.ShowWaitScreen("Loading Custom Settings...")

            If File.Exists(layoutFilePath & "\" & layoutFileName & ".skin") Then
                Dim appSkin As String = File.ReadAllText(layoutFilePath & "\" & layoutFileName & ".skin")
                frmMDI.DefaultLookAndFeel1.LookAndFeel.SkinName = appSkin.Trim()
            End If

            Dim itemCntr As Integer = 2
            Dim xDoc As XDocument = XDocument.Load(layoutFilePath & "\" & layoutFileName & "1.xml")
            For Each objfrmTech In dicFrmTechInstances.Values.OfType(Of frmTechnology) 'objFrmTechList

                Dim xePanelsNode As XElement = xDoc.Descendants("property").Where(Function(x) x.Attribute("name").Value = "Panels" AndAlso x.Attribute("iskey").Value = "true")(0)
                Dim xeTechNode As XElement = xePanelsNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "Item" & itemCntr.ToString AndAlso x.Attribute("isnull").Value = "true" AndAlso x.Attribute("iskey").Value = "true")(0)
                Dim xeTech As String = xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "Text")(0).Value

                If objfrmTech.Network = xeTech.Trim Then

                    '******************* Loading Stats ******************* 
                    SetComboBox(objfrmTech.cmbChartSetNameStats, ComboSelectBased.TextBased, xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "ChartSetNameStats")(0).Value)
                    SetComboBox(objfrmTech.cmbObjectTreeStats, ComboSelectBased.TextBased, xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "TargetTypeStats")(0).Value)

                    Dim objTreeObjectStats As String = xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "SelectedObjectsStats")(0).Value.Replace("'", "")
                    Dim objListStats() As String = objTreeObjectStats.Split(",")
                    Dim tvlastNodeLevelStats As Integer = objfrmTech.tvObjectsTreeStats.GetMaxNodeLevel()
                    For Each strObj In objListStats
                        Dim tv_result As TreeListNode = objfrmTech.tvObjectsTreeStats.FindNodeByFieldValue("ObjectID", strObj)
                        If Not tv_result Is Nothing Then
                            If tv_result.Checked = False And tv_result.Level = tvlastNodeLevelStats Then
                                tv_result.Checked = True
                                objfrmTech.tvObjectsTreeStats.CheckParentNode(tv_result)
                            End If
                        End If
                    Next

                    SetComboBox(objfrmTech.cmbFilterTemplateStats, ComboSelectBased.TextBased, xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "FilterTemplateStats")(0).Value)

                    Dim tempFilterStrStats As String = xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "FilterStringStats")(0).Value
                    Dim strFilterPartsStats() As String = tempFilterStrStats.Split(New String() {" And "}, StringSplitOptions.RemoveEmptyEntries)

                    For iCntr = 0 To strFilterPartsStats.Length - 1
                        Dim filterStr As String = strFilterPartsStats(iCntr).Replace(" And ", "")
                        Dim filterStrPartsStats() As String = filterStr.Split(New String() {" IN "}, StringSplitOptions.RemoveEmptyEntries)
                        Dim objToMapStats() As String = filterStrPartsStats(1).Trim.Replace("('", "").Replace("')", "").Replace("'", "").Split(",")
                        For jCntr = 0 To objToMapStats.Length - 1
                            Dim tv_result As TreeListNode = objfrmTech.tvObjTreeFilterTempStats.FindNodeByFieldValue("ObjectName", objToMapStats(jCntr))
                            If Not tv_result Is Nothing Then
                                If tv_result.Checked = False Then
                                    tv_result.Checked = True
                                    objfrmTech.tvObjTreeFilterTempStats.CheckParentNode(tv_result)
                                End If
                            End If
                        Next
                        objfrmTech.tvObjTreeFilterTempStats.ExpandAll()
                    Next

                    SetComboBox(objfrmTech.cmbPredefTimeStats, ComboSelectBased.TextBased, xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "PSPredefTimeStats")(0).Value)

                    Dim PSResolutionStats As String = xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "PSResolutionStats")(0).Value
                    If PSResolutionStats = "Hourly" Then
                        objfrmTech.rdoHourlyStats.Checked = True
                    ElseIf PSResolutionStats = "Raw" Then
                        objfrmTech.rdoRawStats.Checked = True
                    ElseIf PSResolutionStats = "Daily" Then
                        objfrmTech.rdoDailyStats.Checked = True
                    ElseIf PSResolutionStats = "DailyBH" Then
                        objfrmTech.rdoDailyBHStats.Checked = True
                    ElseIf PSResolutionStats = "Weekly" Then
                        objfrmTech.rdoWeeklyStats.Checked = True
                    ElseIf PSResolutionStats = "DailyBH2" Then
                        objfrmTech.rdoDailyBH2Stats.Checked = True
                    ElseIf PSResolutionStats = "Monthly" Then
                        objfrmTech.rdoMonthlyStats.Checked = True
                    End If

                    For Each tglBtn As IOSToggleButton In objfrmTech.flpCounterTypeStats.Controls
                        If tglBtn.Text = xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "CounterTypeStats")(0).Value Then
                            tglBtn.ToggleState = CheckState.Checked
                        Else
                            tglBtn.ToggleState = CheckState.Unchecked
                        End If
                    Next

                    Dim ShowPrdCalcLegend As String = xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "ShowPrdCalcLegend")(0).Value
                    objfrmTech.prdCalcChkCmbVisuals.Properties.Items.Item(0).CheckState = IIf(CType(ShowPrdCalcLegend, Boolean) = True, CheckState.Checked, CheckState.Unchecked)

                    Dim ShowPrdCalcSeries As String = xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "ShowPrdCalcSeries")(0).Value
                    objfrmTech.prdCalcChkCmbVisuals.Properties.Items.Item(1).CheckState = IIf(CType(ShowPrdCalcSeries, Boolean) = True, CheckState.Checked, CheckState.Unchecked)

                    Dim ShowPrdCalcBands As String = xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "ShowPrdCalcBands")(0).Value
                    objfrmTech.prdCalcChkCmbVisuals.Properties.Items.Item(2).CheckState = IIf(CType(ShowPrdCalcBands, Boolean) = True, CheckState.Checked, CheckState.Unchecked)

                    Dim ShowPrdCalcWeekendBands As String = xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "ShowPrdCalcWeekendBands")(0).Value
                    objfrmTech.prdCalcChkCmbVisuals.Properties.Items.Item(3).CheckState = IIf(CType(ShowPrdCalcWeekendBands, Boolean) = True, CheckState.Checked, CheckState.Unchecked)

                    Dim ShowPrdCalcHolidayBands As String = xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "ShowPrdCalcHolidayBands")(0).Value
                    objfrmTech.prdCalcChkCmbVisuals.Properties.Items.Item(4).CheckState = IIf(CType(ShowPrdCalcHolidayBands, Boolean) = True, CheckState.Checked, CheckState.Unchecked)

                    Dim PrdCalcPeriodDataStats As String = xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "PrdCalcPeriodDataStats")(0).Value
                    If PrdCalcPeriodDataStats.Contains(";") Then
                        Dim distinctPrds() As String = PrdCalcPeriodDataStats.Split(";")
                        For Each str As String In distinctPrds
                            Dim rowParts() As String = str.Split(",")
                            Dim dr As DataRow = objfrmTech.dtPrdCalcStats.NewRow()
                            dr("PeriodName") = rowParts(0)
                            dr("PeriodStart") = CDate(rowParts(1)).ToString("yyyy-MM-dd")
                            dr("PeriodEnd") = CDate(rowParts(2)).ToString("yyyy-MM-dd")
                            objfrmTech.dtPrdCalcStats.Rows.Add(dr)
                            objfrmTech.dtPrdCalcStats.AcceptChanges()
                        Next
                        IOSDevExpressGrid.PopulateDataInGrid(objfrmTech.gcPrdCalcStats, objfrmTech.gvPrdCalcStats, objfrmTech.dtPrdCalcStats, "ALL", Nothing, "PeriodName", "yyyy-MM-dd")
                    Else
                        Dim distinctPrds() As String = PrdCalcPeriodDataStats.Split(",")
                        Dim dr As DataRow = objfrmTech.dtPrdCalcStats.NewRow()
                        dr("PeriodName") = distinctPrds(0)
                        dr("PeriodStart") = CDate(distinctPrds(1)).ToString("yyyy-MM-dd")
                        dr("PeriodEnd") = CDate(distinctPrds(2)).ToString("yyyy-MM-dd")
                        objfrmTech.dtPrdCalcStats.Rows.Add(dr)
                        objfrmTech.dtPrdCalcStats.AcceptChanges()
                        IOSDevExpressGrid.PopulateDataInGrid(objfrmTech.gcPrdCalcStats, objfrmTech.gvPrdCalcStats, objfrmTech.dtPrdCalcStats, "ALL", Nothing, "PeriodName", "yyyy-MM-dd")
                    End If

                    '******************* Loading TopX ******************* 
                    SetComboBox(objfrmTech.cmbChartSetNameTopX, ComboSelectBased.TextBased, xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "ChartSetNameTopX")(0).Value)
                    SetComboBox(objfrmTech.cmbObjectTreeTopX, ComboSelectBased.TextBased, xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "TargetTypeTopX")(0).Value)

                    Dim objTreeObjectTopX As String = xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "SelectedObjectsTopX")(0).Value.Replace("'", "").Replace("IN", "").Replace("(", "").Replace(")", "")
                    Dim objListTopX() As String = objTreeObjectTopX.Split(",")
                    Dim tvlastNodeLevelTopX As Integer = objfrmTech.tvObjectsTreeTopX.GetMaxNodeLevel()
                    For Each strObj In objListTopX
                        Dim tv_result As TreeListNode = objfrmTech.tvObjectsTreeTopX.FindNodeByFieldValue("ObjectID", strObj.Trim)
                        If Not tv_result Is Nothing Then
                            If tv_result.Checked = False And tv_result.Level = tvlastNodeLevelTopX Then
                                tv_result.Checked = True
                                objfrmTech.tvObjectsTreeTopX.CheckParentNode(tv_result)
                            End If
                        End If
                    Next

                    SetComboBox(objfrmTech.cmbFilterTemplateTopX, ComboSelectBased.TextBased, xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "FilterTemplateTopX")(0).Value)

                    Dim tempFilterStrTopX As String = xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "FilterStringTopX")(0).Value
                    Dim strFilterPartsTopX() As String = tempFilterStrTopX.Split(New String() {" And "}, StringSplitOptions.RemoveEmptyEntries)

                    For iCntr = 0 To strFilterPartsTopX.Length - 1
                        Dim filterStr As String = strFilterPartsTopX(iCntr).Replace(" And ", "")
                        Dim filterStrPartsTopX() As String = filterStr.Split(New String() {" IN "}, StringSplitOptions.RemoveEmptyEntries)
                        Dim objToMapTopX() As String = filterStrPartsTopX(1).Trim.Replace("('", "").Replace("')", "").Replace("'", "").Split(",")
                        For jCntr = 0 To objToMapTopX.Length - 1
                            Dim tv_result As TreeListNode = objfrmTech.tvObjTreeFilterTempTopX.FindNodeByFieldValue("ObjectName", objToMapTopX(jCntr))
                            If Not tv_result Is Nothing Then
                                If tv_result.Checked = False Then
                                    tv_result.Checked = True
                                    objfrmTech.tvObjTreeFilterTempTopX.CheckParentNode(tv_result)
                                End If
                            End If
                        Next
                        objfrmTech.tvObjTreeFilterTempTopX.ExpandAll()
                    Next

                    SetComboBox(objfrmTech.cmbPredefTimeTopX, ComboSelectBased.TextBased, xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "PSPredefTimeTopX")(0).Value)

                    Dim PSResolutionTopX As String = xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "PSResolutionTopX")(0).Value
                    If PSResolutionTopX = "hourly " Then
                        objfrmTech.rdoHourlyTopX.Checked = True
                    ElseIf PSResolutionTopX = "Raw" Then
                        objfrmTech.rdoRawTopX.Checked = True
                    ElseIf PSResolutionTopX = "Daily" Then
                        objfrmTech.rdoDailyTopX.Checked = True
                    ElseIf PSResolutionTopX = "DailyBH" Then
                        objfrmTech.rdoDailyBHTopX.Checked = True
                    ElseIf PSResolutionTopX = "Weekly" Then
                        objfrmTech.rdoWeeklyTopX.Checked = True
                    ElseIf PSResolutionTopX = "DailyBH2" Then
                        objfrmTech.rdoDailyBH2TopX.Checked = True
                    ElseIf PSResolutionTopX = "Monthly" Then
                        objfrmTech.rdoMonthlyTopX.Checked = True
                    End If

                    For Each tglBtn As IOSToggleButton In objfrmTech.flpCounterTypeTopX.Controls
                        If tglBtn.Text = xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "ShowObjectsTopX")(0).Value Then
                            tglBtn.ToggleState = CheckState.Checked
                        Else
                            tglBtn.ToggleState = CheckState.Unchecked
                        End If
                    Next

                    objfrmTech.txtSelectXTopX.Text = xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "NoOfTopX")(0).Value
                    Dim tagsExcListID As String = xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "TagsExcListTopXID")(0).Value.TrimEnd(",")
                    Dim tagsExcListName As String = xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "TagsExcListTopXName")(0).Value.TrimEnd(",")
                    If tagsExcListID = "" Or tagsExcListName = "" Then
                        objfrmTech.chkTagsExcListEnable.Checked = False
                    Else
                        objfrmTech.chkTagsExcListEnable.Checked = CBool(xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "TagsExcListEnable")(0).Value)
                    End If

                    Dim strTempID() As String = tagsExcListID.Split(",")
                    Dim strTempName() As String = tagsExcListName.Split(",")

                    objfrmTech.dtTagsExcListTopX = New DataTable()
                    objfrmTech.dtTagsExcListTopX.Columns.Add("Select", GetType(Boolean))
                    objfrmTech.dtTagsExcListTopX.Columns.Add("ListID", GetType(String))
                    objfrmTech.dtTagsExcListTopX.Columns.Add("ListName", GetType(String))

                    If (tagsExcListID <> "") Or (tagsExcListName <> "") Then
                        Dim i As Integer = 0
                        For Each s As String In strTempID
                            Dim dr As DataRow = objfrmTech.dtTagsExcListTopX.NewRow()
                            dr("Select") = True
                            dr("ListID") = s.ToString
                            dr("ListName") = strTempName(i).ToString
                            objfrmTech.dtTagsExcListTopX.Rows.Add(dr)
                            objfrmTech.dtTagsExcListTopX.AcceptChanges()
                            i = i + 1
                        Next
                    End If

                    IOSDevExpressGrid.PopulateDataInGrid(objfrmTech.gcTagsExcListTopX, objfrmTech.gvTagsExcListTopX, objfrmTech.dtTagsExcListTopX, "ALL", {"ListID"}, "ListName")
                    Dim riChkSelect As RepositoryItemCheckEdit = TryCast(objfrmTech.gcTagsExcListTopX.RepositoryItems.Add("CheckEdit"), RepositoryItemCheckEdit)
                    riChkSelect.CheckStyle = CheckStyles.Standard
                    riChkSelect.AllowGrayed = False
                    riChkSelect.NullStyle = StyleIndeterminate.Unchecked
                    objfrmTech.gvTagsExcListTopX.Columns("Select").ColumnEdit = riChkSelect
                    objfrmTech.gvTagsExcListTopX.Columns("Select").VisibleIndex = 0
                    objfrmTech.bTopXHideGridCols = CBool(xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "BTopXHideGridCols")(0).Value)

                    '******************* Loading Eval *******************
                    SetComboBox(objfrmTech.cmbChartSetNameEval, ComboSelectBased.TextBased, xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "ChartSetNameEval")(0).Value)
                    SetComboBox(objfrmTech.cmbObjectTreeEval, ComboSelectBased.TextBased, xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "TargetTypeEval")(0).Value)

                    Dim objTreeObjectEval As String = xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "SelectedObjectsEval")(0).Value.Replace("'", "")
                    Dim objListEval() As String = objTreeObjectEval.Split(",")
                    Dim tvlastNodeLevelEval As Integer = objfrmTech.tvObjectsTreeEval.GetMaxNodeLevel()
                    For Each strObj In objListEval
                        Dim tv_result As TreeListNode = objfrmTech.tvObjectsTreeEval.FindNodeByFieldValue("ObjectID", strObj)
                        If Not tv_result Is Nothing Then
                            If tv_result.Checked = False And tv_result.Level = tvlastNodeLevelEval Then
                                tv_result.Checked = True
                                objfrmTech.tvObjectsTreeEval.CheckParentNode(tv_result)
                            End If
                        End If
                    Next

                    Dim PrdCalcPeriodDataEval As String = xeTechNode.Descendants("property").Where(Function(x) x.Attribute("name").Value = "PrdCalcPeriodDataEval")(0).Value
                    If PrdCalcPeriodDataEval.Contains(";") Then
                        Dim distinctPrds() As String = PrdCalcPeriodDataEval.Split(";")
                        For Each str As String In distinctPrds
                            Dim rowParts() As String = str.Split(",")
                            Dim dr As DataRow = objfrmTech.dtPrdCalcEval.NewRow()
                            dr("PeriodName") = rowParts(0)
                            dr("PeriodStart") = CDate(rowParts(1)).ToString("yyyy-MM-dd")
                            dr("PeriodEnd") = CDate(rowParts(2)).ToString("yyyy-MM-dd")
                            objfrmTech.dtPrdCalcEval.Rows.Add(dr)
                            objfrmTech.dtPrdCalcEval.AcceptChanges()
                        Next
                        IOSDevExpressGrid.PopulateDataInGrid(objfrmTech.gcPrdCalcEval, objfrmTech.gvPrdCalcEval, objfrmTech.dtPrdCalcEval, "ALL", Nothing, "PeriodName", "yyyy-MM-dd")
                    Else
                        Dim distinctPrds() As String = PrdCalcPeriodDataEval.Split(",")
                        Dim dr As DataRow = objfrmTech.dtPrdCalcEval.NewRow()
                        dr("PeriodName") = distinctPrds(0)
                        dr("PeriodStart") = CDate(distinctPrds(1)).ToString("yyyy-MM-dd")
                        dr("PeriodEnd") = CDate(distinctPrds(2)).ToString("yyyy-MM-dd")
                        objfrmTech.dtPrdCalcEval.Rows.Add(dr)
                        objfrmTech.dtPrdCalcEval.AcceptChanges()
                        IOSDevExpressGrid.PopulateDataInGrid(objfrmTech.gcPrdCalcEval, objfrmTech.gvPrdCalcEval, objfrmTech.dtPrdCalcEval, "ALL", Nothing, "PeriodName", "yyyy-MM-dd")
                    End If
                End If

                itemCntr = itemCntr + 1
            Next
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            WaitScreen.CloseWaitScreen()
        End Try
    End Sub

    Public Sub OpenWorkspace()
        Try
            Dim fd As New OpenFileDialog
            fd.DefaultExt = "mws"
            fd.Filter = "IOS Workspace|*.mws"
            fd.Title = "Open the workspace"
            Dim files() As IO.FileInfo = New IO.DirectoryInfo(GetUserDataPath() & "\Data\").GetFiles("*.mws").OrderByDescending(Function(fi) fi.LastWriteTime).ToArray
            If files.Length > 0 Then
                fd.FileName = files(0).FullName
                If fd.FileName <> "" Then
                    Dim fileparts() As String = files(0).Name.Split(".")
                    OpenLayoutAndWorkspace(files(0).DirectoryName, fileparts(0))
                End If
                frmMapWindow.cmb_FieldSearch_Refresh()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub SaveWorkspace()
        Try
            'Dim fd As New SaveFileDialog
            'fd.DefaultExt = "mws"
            'fd.Filter = "IOS Workspace|*.mws"
            'fd.Title = "Save the workspace"
            'fd.FileName = GetUserDataPath() & "\Data\LastSaved.mws"

            'Dim wsp As New MapInfo.Persistence.WorkSpacePersistence
            'For Each tbl In MapInfo.Engine.Session.Current.Catalog
            '    If tbl.TableInfo.TableType = MapInfo.Data.TableType.AdoNet Then
            '        MapInfo.Engine.Session.Current.Catalog.CloseTable(tbl.Alias)
            '    End If
            'Next
            'wsp.ApplicationName = "IOS"
            'wsp.Save(fd.FileName)
        Catch ex As Exception
        End Try
    End Sub

    Public Sub SendToMap(ByVal tech As String, ByVal kpi As String, ByVal dt_filtered As DataTable,
                         ByVal topxmaptype As Integer, Optional from As EnumSendToMap = EnumSendToMap.FromDefault,
                         Optional dtTheamBins As DataTable = Nothing, Optional mappingColumns() As String = Nothing,
                         Optional MapToVoronoi As Boolean = False, Optional MapToSite As Boolean = False,
                         Optional targettable As String = Nothing, Optional targetcolumn As String = Nothing,
                         Optional _strNetwork As String = "")

        Dim selectedTech As String = String.Empty
        Dim themeExpression As String = kpi
        Dim layerPrefix As String = "TopX"
        Try
            If from = EnumSendToMap.FromDefault Then
                If InStr(tech.ToUpper, _strNetwork.ToUpper) <> 0 Then
                    selectedTech = _strNetwork
                End If
            Else

                If (from = EnumSendToMap.FromPH) Then
                    themeExpression = "ParamHistoryMap"
                    layerPrefix = "PH_"
                ElseIf (from = EnumSendToMap.ICMFromOverview) Then
                    themeExpression = "ICM_Overview"
                    layerPrefix = "ICMOverview_"
                ElseIf (from = EnumSendToMap.ICMFromPreconfigured) Then

                    themeExpression = IIf(mappingColumns.Length >= 1, kpi, mappingColumns(0))
                    layerPrefix = "ICMPreconfigured_"
                ElseIf (from = EnumSendToMap.FromPCHR) Then

                    themeExpression = "Count"
                    layerPrefix = "PCHR_"
                ElseIf (from = EnumSendToMap.FromCapacity) Then
                    themeExpression = "CapacityMap"
                    layerPrefix = "CAP_"
                Else
                    themeExpression = kpi
                    'themeExpression = "KPI"
                    layerPrefix = "ICM_"
                End If

                If InStr(tech.ToUpper, _strNetwork.ToUpper) <> 0 Then
                    selectedTech = _strNetwork
                End If
            End If
            If Not (String.IsNullOrEmpty(selectedTech)) Then
                If (from = EnumSendToMap.ICMFromOverview) Then
                    frmMapWindow.Cell_ICM_CategoryMap(selectedTech, kpi, dt_filtered, themeExpression, layerPrefix)
                ElseIf (from = EnumSendToMap.ICMFromPreconfigured) Then
                    frmMapWindow.Cell_ICM_Thematic(selectedTech, kpi, dt_filtered, themeExpression, layerPrefix, mappingColumns, dtTheamBins, MapToVoronoi, MapToSite)
                ElseIf (from = EnumSendToMap.PolygonMap) Then
                    frmMapWindow.Polygon_TopX_Map(kpi, dt_filtered, targettable, targetcolumn)
                ElseIf (from = EnumSendToMap.FromGeoID) Then
                    frmMapWindow.CNE_TopX_GEOID_Map(kpi, dt_filtered, targettable, topxmaptype)
                Else
                    frmMapWindow.Cell_Top20_Map(selectedTech, kpi, dt_filtered, topxmaptype, themeExpression, layerPrefix, MapToVoronoi, MapToSite)
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Public Function GetMinMax(data As DataTable) As DataTable
        Dim dt As DataTable = data.Clone()
        Dim minpx As Double = Double.MaxValue
        Dim maxpx As Double = Double.MinValue
        Dim minpy As Double = Double.MaxValue
        Dim maxpy As Double = Double.MinValue
        Dim IsHavingValues As Boolean = True
        For Each dr As DataRow In data.Rows
            If dr(0) = DBNull.Value Then
                IsHavingValues = False
                Exit For
            End If
            Dim px1 As Double = dr.Field(Of Double)("px1")
            Dim px2 As Double = dr.Field(Of Double)("px2")
            minpx = Math.Min(minpx, px1)
            maxpx = Math.Max(maxpx, px2)
            Dim py1 As Double = dr.Field(Of Double)("py1")
            Dim py2 As Double = dr.Field(Of Double)("py2")
            minpy = Math.Min(minpy, py1)
            maxpy = Math.Max(maxpy, py2)
        Next
        If IsHavingValues Then
            dt.Rows.Add(minpx, maxpx, minpy, maxpy)
        End If
        Return dt
    End Function

    Public Sub MapSelectedNB(ByVal dt As DataTable)
        Dim connection As New MapInfo.Data.MIConnection
        Try
            connection.Open()
            connection.Catalog.CloseTable("NB_Table")
            connection.Catalog.CloseTable("NB_Lines")
            connection.Catalog.CloseTable("NB_Plot_Source")
            connection.Catalog.CloseTable("NB_Plot_Target")

            Dim iSourceCount As Integer = 0
            Dim iTargetCount As Integer = 0

            Dim sSourceName As String = "NB_Plot_Source"
            Dim sTargetName As String = "NB_Plot_Target"
            Dim expression As String = ""
            Dim expression_Source As String = ""
            Dim sNBTableColList As String = ""

            Dim tblCurrent As MapInfo.Data.Table
            Dim tblNB As MapInfo.Data.Table

            Dim tblNBInfo As New MapInfo.Data.TableInfoAdoNet("NB_Table")
            tblNBInfo.ReadOnly = False
            tblNBInfo.DataTable = dt
            tblNB = connection.Catalog.OpenTable(tblNBInfo)

            sNBTableColList = frmMapWindow.ColumnString_DataTable(tblNB.Alias, dt)

            expression = ("SELECT " & sNBTableColList & ", @tblNB.Obj from NB_Table, @tblNB where @tblNB.IOS_CELL_GID = NB_Table.T_IOS_CELL_GID ")
            expression_Source = ("SELECT " & sNBTableColList & ", @tblNB.Obj from NB_Table, @tblNB where @tblNB.IOS_CELL_GID = NB_Table.S_IOS_CELL_GID ")


            Dim tblSourceInfoMem As MapInfo.Data.TableInfoMemTable = Nothing
            Dim tblSource As MapInfo.Data.Table = Nothing

            Dim tblTargetInfoMem As MapInfo.Data.TableInfoMemTable = Nothing
            Dim tblTarget As MapInfo.Data.Table = Nothing

            'Source
            Dim enumSource As MapInfo.Data.ITableEnumerator = Session.Current.Catalog.GetEnumerator
            Do While enumSource.MoveNext
                tblCurrent = enumSource.Current
                Dim sourceFeature As MapInfo.Data.IResultSetFeatureCollection = Session.Current.Selections.DefaultSelection.Item(tblCurrent)
                If ((Not sourceFeature Is Nothing) AndAlso ((sourceFeature.Count <> 0) And frmMapWindow.Layer_InMapConfiguration(sourceFeature.BaseTable.Alias))) Then
                    If (tblSource Is Nothing) Then


                        Dim sSourceQuery As String = Strings.Replace(expression_Source, "@tblNB", tblCurrent.Alias, 1, -1, CompareMethod.Binary)
                        sSourceQuery = Replace(sSourceQuery, "@uniquefield", "IOS_CELL_GID")

                        Dim sourceCommand As MapInfo.Data.MICommand = connection.CreateCommand
                        sourceCommand.CommandText = sSourceQuery

                        Dim sourceFeature2 As MapInfo.Data.IResultSetFeatureCollection = Nothing

                        sourceFeature2 = sourceCommand.ExecuteFeatureCollection
                        tblSourceInfoMem = MapInfo.Data.TableInfoFactory.CreateFromFeatureCollection(sSourceName, MapInfo.Data.TableType.MemTable, sourceFeature2)

                        tblSource = Session.Current.Catalog.CreateTable(tblSourceInfoMem)
                        tblSource.InsertFeatures(sourceFeature2)

                        'tblSourceInfoMem = MapInfo.Data.TableInfoFactory.CreateFromFeatureCollection(sSourceName, MapInfo.Data.TableType.MemTable, sourceFeature)
                        'tblSource = Session.Current.Catalog.CreateTable(tblSourceInfoMem)
                        iSourceCount = (iSourceCount + sourceFeature2.Count)

                        sourceFeature2.Clear()
                        sourceFeature2.Close()
                        sourceCommand.Dispose()
                    End If
                    'tblSource.InsertFeatures(sourceFeature)


                End If
            Loop

            'Target
            Dim enumMapConfig As IEnumerator = Nothing
            Try
                enumMapConfig = dt_Map_Configuration.Rows.GetEnumerator
                Do While enumMapConfig.MoveNext
                    Dim currentRow As DataRow = DirectCast(enumMapConfig.Current, DataRow)
                    tblCurrent = Session.Current.Catalog.GetTable(currentRow.Item("LayerName").ToString.Trim)
                    If ((Not tblCurrent Is Nothing)) Then

                        Dim sTargetQuery As String = Strings.Replace(expression, "@tblNB", tblCurrent.Alias, 1, -1, CompareMethod.Binary)
                        sTargetQuery = Replace(sTargetQuery, "@uniquefield", "IOS_CELL_GID")

                        Dim targetCommand As MapInfo.Data.MICommand = connection.CreateCommand
                        targetCommand.CommandText = sTargetQuery

                        Dim targetFeature As MapInfo.Data.IResultSetFeatureCollection = Nothing
                        If (tblTargetInfoMem Is Nothing) Then
                            targetFeature = targetCommand.ExecuteFeatureCollection
                            tblTargetInfoMem = MapInfo.Data.TableInfoFactory.CreateFromFeatureCollection(sTargetName, MapInfo.Data.TableType.MemTable, targetFeature)

                            tblTarget = Session.Current.Catalog.CreateTable(tblTargetInfoMem)
                            tblTarget.InsertFeatures(targetFeature)
                            iTargetCount = targetFeature.Count
                        Else
                            targetFeature = targetCommand.ExecuteFeatureCollection
                            tblTarget.InsertFeatures(targetFeature)
                            iTargetCount = (iTargetCount + targetFeature.Count)
                        End If
                        targetFeature.Clear()
                        targetFeature.Close()
                        targetCommand.Dispose()
                    End If
                Loop
            Catch ex As Exception
            Finally
                If TypeOf enumMapConfig Is IDisposable Then
                    TryCast(enumMapConfig, IDisposable).Dispose()
                End If
            End Try

            If (iSourceCount <> 0 AndAlso iTargetCount <> 0) Then

                Dim command2 As MapInfo.Data.MICommand = connection.CreateCommand
                command2.CommandText = "SELECT * FROM " & tblTarget.Alias
                Dim features As MapInfo.Data.IResultSetFeatureCollection = Nothing
                features = command2.ExecuteFeatureCollection

                Dim command As MapInfo.Data.MICommand = connection.CreateCommand
                command.CommandText = "SELECT * FROM " & tblTarget.Alias

                Dim tblNBLinesInfoMem As MapInfo.Data.TableInfoMemTable = MapInfo.Data.TableInfoFactory.CreateFromFeatureCollection("NB_Lines", MapInfo.Data.TableType.MemTable, command.ExecuteFeatureCollection)
                Dim tblNBLines As MapInfo.Data.Table = Session.Current.Catalog.CreateTable(tblNBLinesInfoMem)

                command.Dispose()
                command2.Dispose()

                For ind As Integer = 0 To iTargetCount - 1
                    Try
                        Dim feature As MapInfo.Data.Feature = features.Table(ind)
                        Dim info As MapInfo.Data.SearchInfo = MapInfo.Data.SearchInfoFactory.SearchWhere(("S_IOS_CELL_GID = '" & feature.Item("S_IOS_CELL_GID").ToString) & "'")

                        Dim feature2 As MapInfo.Data.Feature = connection.Catalog.SearchForFeature(tblSource, info)
                        If (Not feature.Geometry.IsEmpty) Then
                            Dim point As New DPoint
                            point = feature2.Geometry.Centroid
                            Dim point2 As New DPoint
                            point2 = feature.Geometry.Centroid

                            feature.Geometry = DirectCast(MapInfo.Geometry.MultiCurve.CreateLine(csysWGS84, point2, point), FeatureGeometry)
                            tblNBLines.InsertFeature(feature)
                        End If
                    Catch ex As Exception
                        _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                    End Try
                Next

                'Configure Source Layer
                Dim lyrSource As New FeatureLayer(tblSource)
                frmMapWindow.MapControl1.Map.Layers.Insert(0, lyrSource)
                LayerHelper.SetSelectable(lyrSource, False)
                MapInfo.Tools.MapTool.SetInfoTipExpression(frmMapWindow.MapControl1.Tools.MapToolProperties, lyrSource, frmMapWindow.ColumnsInfoTip_MapinfoTable(tblSource))

                'Configure Target Layer
                Dim lyrTarget As New FeatureLayer(tblTarget)
                frmMapWindow.MapControl1.Map.Layers.Insert(0, lyrTarget)
                LayerHelper.SetSelectable(lyrTarget, False)
                MapInfo.Tools.MapTool.SetInfoTipExpression(frmMapWindow.MapControl1.Tools.MapToolProperties, lyrTarget, frmMapWindow.ColumnsInfoTip_MapinfoTable(tblTarget))

                'Configure Line Layer
                Dim lyrLines As New FeatureLayer(tblNBLines)
                frmMapWindow.MapControl1.Map.Layers.Insert(0, lyrLines)
                LayerHelper.SetSelectable(lyrLines, False)
                MapInfo.Tools.MapTool.SetInfoTipExpression(frmMapWindow.MapControl1.Tools.MapToolProperties, lyrLines, frmMapWindow.ColumnsInfoTip_MapinfoTable(tblNBLines))

                'Style Source Layer
                Dim interiorSource As New MapInfo.Styles.SimpleInterior(2, Color.Lime, Color.AliceBlue, False)
                Dim lineStyleSource As New MapInfo.Styles.SimpleLineStyle(New MapInfo.Styles.LineWidth(3, 0), 1, Color.Black, False)
                Dim areaStyleSource As New MapInfo.Styles.AreaStyle(lineStyleSource, interiorSource)

                Dim sourceModifier As New FeatureOverrideStyleModifier(Nothing, New MapInfo.Styles.CompositeStyle(areaStyleSource))
                lyrSource.Modifiers.Append(sourceModifier)

                'Style Target Layer
                Dim interiorTarget As New MapInfo.Styles.SimpleInterior(2, Color.FromArgb(170, Color.Orange), Color.FromArgb(170, Color.Orange), False)
                Dim lineStyleTarget As New MapInfo.Styles.SimpleLineStyle(New MapInfo.Styles.LineWidth(1, 0), 1, Color.Black, False)
                Dim areaStyleTarget As New MapInfo.Styles.AreaStyle(lineStyleTarget, interiorTarget)

                Dim targetModifier As New FeatureOverrideStyleModifier(Nothing, New MapInfo.Styles.CompositeStyle(areaStyleTarget))
                lyrTarget.Modifiers.Append(targetModifier)

                'Style Line Layer
                Dim styleLine As New MapInfo.Styles.SimpleLineStyle(New MapInfo.Styles.LineWidth(3, MapInfo.Styles.LineWidthUnit.Pixel), 60, Color.Gray, False)
                Dim lineModifier As New FeatureOverrideStyleModifier(Nothing, New MapInfo.Styles.CompositeStyle(styleLine))
                lyrLines.Modifiers.Append(lineModifier)

                'MapInfo.Engine.Session.Current.MapFactory(0).SetView(lyrLines.Bounds, Session.Current.Selections.DefaultSelection.Envelope.CoordSys)
                MapInfo.Engine.Session.Current.MapFactory(0).SetView(lyrLines)

                'Show/Hide Line Layer
                If (frmMapWindow.btnNBArrows.Text = "Arrows") Then
                    frmMapWindow.Layer_View("NB_Lines", True)
                Else
                    frmMapWindow.Layer_View("NB_Lines", False)
                End If
                connection.Close()
            Else
                tblSource.Close()
                tblTarget.Close()
                tblNB.Close()
                connection.Close()
                connection.Dispose()
            End If
            MapInfo.Engine.Session.Current.Selections.DefaultSelection.Clear()
        Catch exception3 As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & exception3.Message & " - " & exception3.StackTrace)
            connection.Close()
        End Try
    End Sub

#End Region

#Region "Context Menu Supporting Methods"

    Public Sub AddThemeType(ByRef tsmi_TopXMapType As ToolStripComboBox, Optional ForTimeBasedChart As Boolean = False)
        If ForTimeBasedChart = True Then
            tsmi_TopXMapType.Items.Add("Ranged Theme")
            tsmi_TopXMapType.Items.Add("Graduated Theme")
            tsmi_TopXMapType.Items.Add("Voronoi")
        Else
            tsmi_TopXMapType.Items.Add("Ranged Theme")
            tsmi_TopXMapType.Items.Add("Graduated Theme")
            tsmi_TopXMapType.Items.Add("Heatmap")
            tsmi_TopXMapType.Items.Add("Voronoi")
        End If
    End Sub

#End Region

#Region "Technology TopX Methods"

    Public Structure SourceButton
        Public SourceButtonText As String
        Public SourceButtonChecked As Boolean
        Public SourceButtonHierachy As Integer
        Public SourceButtonTag As String
    End Structure

    Public Function GetChartsKPI(ByRef myChart As dotnetCHARTING.WinForms.Chart)
        Dim tech As String = myChart.Tag
        Dim chartname As String = myChart.Name
        Dim sqlchart As String = "SELECT TechTab,SQLKPI_ID,ObjectTab,ChartElements from IOS_Chart_Configuration WHERE (((TechTab = " & Chr(39) & tech & Chr(39) & ") AND (ChartName = " & Chr(39) & chartname & Chr(39) & ") AND ((ChartSetName = " & Chr(39) & chartSetName & Chr(39) & ") OR (ChartSetName = " & Chr(39) & Environment.UserName.ToString & Chr(39) & ")))) ORDER BY techtab, categorytabindex, chartindex, chartelementid ASC"
        Return DataAccessorODBC.GetDataTable(connStrIOSServer, sqlchart)
    End Function

    Public Function flpSourceBtn_GetType(ByVal tech As String, ByVal type As String, ByRef _flowLayoutControl As FlowLayoutPanel) As SourceButton
        Dim flp As FlowLayoutPanel = _flowLayoutControl
        Dim i As Integer = 0

        For Each vb As IOSToggleButton In flp.Controls
            If vb.Tag = type Then
                Dim srcbtn As SourceButton = New SourceButton
                srcbtn.SourceButtonChecked = True
                srcbtn.SourceButtonHierachy = i
                srcbtn.SourceButtonText = vb.Text
                Return srcbtn
            End If
            i += 1
        Next
        Return Nothing
    End Function

    Public Function flpSourceBtn_GetChecked(ByVal tech As String, ByRef _flowLayoutControl As FlowLayoutPanel) As List(Of SourceButton)
        Dim checkedbtns As New List(Of SourceButton)
        Dim flp As FlowLayoutPanel = _flowLayoutControl
        Dim i As Integer = 0

        UserTrackingStatsCounterType = ""
        UserTrackingTopXObjects = ""

        For Each vb As IOSToggleButton In flp.Controls
            If vb.ToggleState = CheckState.Checked Then
                Dim srcbtn As SourceButton = New SourceButton
                srcbtn.SourceButtonChecked = True
                srcbtn.SourceButtonHierachy = i
                srcbtn.SourceButtonText = vb.Text
                srcbtn.SourceButtonTag = vb.Tag
                checkedbtns.Add(srcbtn)
                If tech.ToLower.Contains("topx") Then
                    UserTrackingTopXObjects = vb.Text & ","
                Else
                    UserTrackingStatsCounterType = vb.Text & ","
                End If
            End If
            i += 1
        Next
        UserTrackingStatsCounterType.TrimEnd(",")
        UserTrackingTopXObjects.TrimEnd(",")
        Return checkedbtns
    End Function

#End Region

#Region "MDI Block"

    Public Sub ResetNetworkAll(_techInternal As String)
        Try
            Select Case _techInternal
                Case IOSInternalTechnology.Tech2G1
                    networkAll.Network2G1 = "not used"
                Case IOSInternalTechnology.Tech2G2
                    networkAll.Network2G2 = "not used"
                Case IOSInternalTechnology.Tech2G3
                    networkAll.Network2G3 = "not used"
                Case IOSInternalTechnology.Tech3G1
                    networkAll.Network3G1 = "not used"
                Case IOSInternalTechnology.Tech3G2
                    networkAll.Network3G2 = "not used"
                Case IOSInternalTechnology.Tech3G3
                    networkAll.Network3G3 = "not used"
                Case IOSInternalTechnology.Tech4G1
                    networkAll.Network4G1 = "not used"
                Case IOSInternalTechnology.Tech4G2
                    networkAll.Network4G2 = "not used"
                Case IOSInternalTechnology.Tech4G3
                    networkAll.Network4G3 = "not used"
                Case IOSInternalTechnology.Tech5G1
                    networkAll.Network5G1 = "not used"
                Case IOSInternalTechnology.Tech5G2
                    networkAll.Network5G2 = "not used"
                Case IOSInternalTechnology.Tech5G3
                    networkAll.Network5G3 = "not used"
                Case IOSInternalTechnology.EPC1
                    networkAll.NetworkEPC1 = "not used"
                Case IOSInternalTechnology.EPC2
                    networkAll.NetworkEPC2 = "not used"
                Case IOSInternalTechnology.EPC3
                    networkAll.NetworkEPC3 = "not used"
                Case IOSInternalTechnology.CDRMSC
                    networkAll.NetworkMSCCDR = "not used"
                Case IOSInternalTechnology.CDRSGSN
                    networkAll.NetworkSGSNCDR = "not used"
                Case IOSInternalTechnology.CDRGGSN
                    networkAll.NetworkGGSNCDR = "not used"
                Case IOSInternalTechnology.GGSN
                    networkAll.NetworkGGSN = "not used"
                Case IOSInternalTechnology.IMS
                    networkAll.NetworkIMS = "not used"
                Case IOSInternalTechnology.MEE
                    networkAll.NetworkMEE = "not used"
                Case IOSInternalTechnology.MGW
                    networkAll.NetworkMGW = "not used"
                Case IOSInternalTechnology.MSS
                    networkAll.NetworkMSS = "not used"
                Case IOSInternalTechnology.PGW
                    networkAll.NetworkPGW = "not used"
                Case IOSInternalTechnology.SGSN
                    networkAll.NetworkSGSN = "not used"
                Case IOSInternalTechnology.SGW
                    networkAll.NetworkSGW = "not used"
                Case IOSInternalTechnology.TX
                    networkAll.NetworkTX = "not used"
                Case IOSInternalTechnology.SAPC
                    networkAll.NetworkSAPC = "not used"
                Case IOSInternalTechnology.TRANSPORT
                    networkAll.NetworkTransport = "not used"
                Case IOSInternalTechnology.PDUM
                    networkAll.NetworkPDUM = "not used"
                Case IOSInternalTechnology.TWAMP
                    networkAll.NetworkTWAMP = "not used"
                Case IOSInternalTechnology.HLR
                    networkAll.NetworkHLR = "not used"
                Case IOSInternalTechnology.DWDM
                    networkAll.NetworkDWDM = "not used"
                Case IOSInternalTechnology.HSS
                    networkAll.NetworkHSS = "not used"
                Case IOSInternalTechnology.UDR
                    networkAll.NetworkUDR = "not used"
            End Select
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Public Sub ObjectTree_DataSet_Load_new(ByVal dr As DataRow, Optional ByVal reload As Boolean = False, Optional ByVal dt As DataTable = Nothing)
        Try
            Dim strArray As String()()
            Dim strArray2 As String()()
            Dim connStr As String
            Dim sql As String

            Dim list = New List(Of String)
            If Not dt Is Nothing Then
                For Each row As DataRow In dt.Rows
                    list.Add(row(0).ToString)
                Next
            End If

            'Identify Destination Dataset
            Dim ds2use As DataSet = Nothing
            Select Case dr("TechInternal").ToString
                Case IOSInternalTechnology.Tech2G1
                    networkAll.Network2G1 = dr("Tech").ToString
                    ds2use = dsTree2GVendor1

                Case IOSInternalTechnology.Tech2G2
                    networkAll.Network2G2 = dr("Tech").ToString
                    ds2use = dsTree2GVendor2

                Case IOSInternalTechnology.Tech2G3
                    networkAll.Network2G3 = dr("Tech").ToString
                    ds2use = dsTree2GVendor3

                Case IOSInternalTechnology.Tech3G1
                    networkAll.Network3G1 = dr("Tech").ToString
                    ds2use = dsTree3GVendor1

                Case IOSInternalTechnology.Tech3G2
                    networkAll.Network3G2 = dr("Tech").ToString
                    ds2use = dsTree3GVendor2

                Case IOSInternalTechnology.Tech3G3
                    networkAll.Network3G3 = dr("Tech").ToString
                    ds2use = dsTree3GVendor3

                Case IOSInternalTechnology.Tech4G1
                    networkAll.Network4G1 = dr("Tech").ToString
                    ds2use = dsTree4GVendor1

                Case IOSInternalTechnology.Tech4G2
                    networkAll.Network4G2 = dr("Tech").ToString
                    ds2use = dsTree4GVendor2

                Case IOSInternalTechnology.Tech4G3
                    networkAll.Network4G3 = dr("Tech").ToString
                    ds2use = dsTree4GVendor3

                Case IOSInternalTechnology.Tech5G1
                    networkAll.Network5G1 = dr("Tech").ToString
                    ds2use = dsTree5GVendor1

                Case IOSInternalTechnology.Tech5G2
                    networkAll.Network5G2 = dr("Tech").ToString
                    ds2use = dsTree5GVendor2

                Case IOSInternalTechnology.Tech5G3
                    networkAll.Network5G3 = dr("Tech").ToString
                    ds2use = dsTree5GVendor3

                Case IOSInternalTechnology.TechNode1
                    networkAll.NetworkNode1 = dr("Tech").ToString
                    ds2use = dsTreeNodeVendor1

                Case IOSInternalTechnology.TechNode2
                    networkAll.NetworkNode2 = dr("Tech").ToString
                    ds2use = dsTreeNodeVendor2

                Case IOSInternalTechnology.TechNode3
                    networkAll.NetworkNode3 = dr("Tech").ToString
                    ds2use = dsTreeNodeVendor3

                Case IOSInternalTechnology.EPC1
                    networkAll.NetworkEPC1 = dr("Tech").ToString
                    ds2use = dsTreeMMEVendor1

                Case IOSInternalTechnology.EPC2
                    networkAll.NetworkEPC2 = dr("Tech").ToString
                    ds2use = dsTreeSGWVendor1

                Case IOSInternalTechnology.EPC3
                    networkAll.NetworkEPC3 = dr("Tech").ToString

                Case IOSInternalTechnology.CDRMSC
                    networkAll.NetworkMSCCDR = dr("Tech").ToString
                    ds2use = dsTreeMSCVendorCDR

                Case IOSInternalTechnology.CDRSGSN
                    networkAll.NetworkSGSNCDR = dr("Tech").ToString
                    ds2use = dsTreeSGSNVendorCDR

                Case IOSInternalTechnology.CDRGGSN
                    networkAll.NetworkGGSNCDR = dr("Tech").ToString
                    ds2use = dsTreeGGSNVendorCDR

                Case IOSInternalTechnology.GGSN
                    networkAll.NetworkGGSN = dr("Tech").ToString
                    ds2use = dsTreeGGSNVendor1

                Case IOSInternalTechnology.IMS
                    networkAll.NetworkIMS = dr("Tech").ToString
                    ds2use = dsTreeIMSVendor1

                Case IOSInternalTechnology.MEE
                    networkAll.NetworkMEE = dr("Tech").ToString
                    ds2use = dsTreeMMEVendor1

                Case IOSInternalTechnology.MGW
                    networkAll.NetworkMGW = dr("Tech").ToString
                    ds2use = dsTreeMGWVendor1

                Case IOSInternalTechnology.MSS
                    networkAll.NetworkMSS = dr("Tech").ToString
                    ds2use = dsTreeMSSVendor1

                Case IOSInternalTechnology.PGW
                    networkAll.NetworkPGW = dr("Tech").ToString
                    ds2use = dsTreePGWVendor1

                Case IOSInternalTechnology.SGSN
                    networkAll.NetworkSGSN = dr("Tech").ToString
                    ds2use = dsTreeSGSNVendor1

                Case IOSInternalTechnology.SGW
                    networkAll.NetworkSGW = dr("Tech").ToString
                    ds2use = dsTreeSGWVendor1

                Case IOSInternalTechnology.TX
                    networkAll.NetworkTX = dr("Tech").ToString
                    ds2use = dsTreeTXVendor1

                Case IOSInternalTechnology.TX2
                    networkAll.NetworkTX2 = dr("Tech").ToString
                    ds2use = dsTreeTX2Vendor1

                Case IOSInternalTechnology.SAPC
                    networkAll.NetworkSAPC = dr("Tech").ToString
                    ds2use = dsTreeSAPCVendor1

                Case IOSInternalTechnology.COMMON
                    networkAll.NetworkCommon = dr("Tech").ToString
                    ds2use = dsTreeCommonTech

                Case IOSInternalTechnology.TRANSPORT
                    networkAll.NetworkTransport = dr("Tech").ToString
                    ds2use = dsTreeTransportVendor1

                Case IOSInternalTechnology.PDUM
                    networkAll.NetworkPDUM = dr("Tech").ToString
                    ds2use = dsTreePDUMVendor

                Case IOSInternalTechnology.TWAMP
                    networkAll.NetworkTWAMP = dr("Tech").ToString
                    ds2use = dsTreeTwampVendor

                Case IOSInternalTechnology.HLR
                    networkAll.NetworkHLR = dr("Tech").ToString
                    ds2use = dsTreeHLRVendor

                Case IOSInternalTechnology.DWDM
                    networkAll.NetworkDWDM = dr("Tech").ToString
                    ds2use = dsTreeDwdmVendor

                Case IOSInternalTechnology.HSS
                    networkAll.NetworkHSS = dr("Tech").ToString
                    ds2use = dsTreeHSSVendor

                Case IOSInternalTechnology.UDR
                    networkAll.NetworkUDR = dr("Tech").ToString
                    ds2use = dsTreeUDRVendor
            End Select

            If Not ds2use Is Nothing Then
                Dim ds As New DataSet
                Dim internaltech As String = dr("TechInternal").ToString
                Dim node As String = dr("Object").ToString
                Dim tblname As String = String.Format("dsTree{0}_{1}", internaltech, node)
                If (File.Exists((String.Format("{0}\{1}.xml", GetUserDataPath(), tblname))) And Not reload) Then
                    ds.ReadXml((String.Format("{0}\{1}.xml", GetUserDataPath(), tblname)), XmlReadMode.ReadSchema)
                Else
                    'strArray2 = Nothing
                    strArray = Nothing
                    Dim sqlAndConnectionStr() As String = GetSQL(dr("SqlID").ToString, strArray, dt_IOS_SQL)
                    If sqlAndConnectionStr IsNot Nothing Then
                        ds = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                    End If
                    If Not ds Is Nothing Then
                        ds.WriteXml((String.Format("{0}\{1}.xml", GetUserDataPath(), tblname)), XmlWriteMode.WriteSchema)
                    End If
                End If

                If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                    ds.Tables(0).TableName = tblname
                    If Not dr("ParentID").ToString = "0" Then
                        ds.Tables.Item(0).Merge(ds2use.Tables.Item("dsTree" & internaltech & "_" & dr("ParentObject").ToString))
                    End If
                    If Not ds2use.Tables.Contains(tblname) Then
                        ds2use.Tables.Add(ds.Tables(0).Copy)
                    Else
                        ds2use.Tables.Remove(tblname)
                        ds2use.Tables.Add(ds.Tables(0).Copy)
                    End If
                    ds.Dispose()
                End If
            Else
                Select Case dr("InternalObjectName").ToString.ToLower
                    Case "sgsn"
                        dsTreeSGSNVendor1 = New DataSet
                        If (File.Exists((GetUserDataPath() & "\dsTree_sgsn.xml")) And Not reload) Then
                            dsTreeSGSNVendor1.ReadXml((GetUserDataPath() & "\dsTree_sgsn.xml"), XmlReadMode.ReadSchema)
                        Else
                            strArray2 = Nothing
                            strArray = Nothing
                            connStr = GetSQL(IOSSqlIds.OBJECTS_SGSN, strArray2, dt_IOS_SQL)(0)
                            If list.Contains(connStr) Then
                                sql = GetSQL(1109, strArray, dt_IOS_SQL)(1)
                                dsTreeSGSNVendor1 = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)
                                If Not dsTreeSGSNVendor1 Is Nothing Then
                                    dsTreeSGSNVendor1.WriteXml((GetUserDataPath() & "\dsTree_sgsn.xml"), XmlWriteMode.WriteSchema)
                                End If
                            End If
                        End If
                        Return
                    Case "ggsn"
                        dsTreeGGSNVendor1 = New DataSet
                        If (File.Exists((GetUserDataPath() & "\dsTree_GGSN.xml")) And Not reload) Then
                            dsTreeGGSNVendor1.ReadXml((GetUserDataPath() & "\dsTree_GGSN.xml"), XmlReadMode.ReadSchema)
                        Else
                            strArray2 = Nothing
                            strArray = Nothing
                            connStr = GetSQL(IOSSqlIds.OBJECTS_GGSN, strArray2, dt_IOS_SQL)(0)
                            If list.Contains(connStr) Then
                                sql = GetSQL(IOSSqlIds.OBJECTS_GGSN, strArray, dt_IOS_SQL)(1)
                                dsTreeGGSNVendor1 = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)
                                If Not dsTreeGGSNVendor1 Is Nothing Then
                                    dsTreeGGSNVendor1.WriteXml((GetUserDataPath() & "\dsTree_GGSN.xml"), XmlWriteMode.WriteSchema)
                                End If
                            End If
                        End If
                        Return
                    Case "ims"
                        dsTreeIMSVendor1 = New DataSet
                        If (File.Exists((GetUserDataPath() & "\dsTree_IMS.xml")) And Not reload) Then
                            dsTreeIMSVendor1.ReadXml((GetUserDataPath() & "\dsTree_IMS.xml"), XmlReadMode.ReadSchema)
                        Else
                            strArray2 = Nothing
                            strArray = Nothing
                            connStr = GetSQL(IOSSqlIds.OBJECTS_IMS, strArray2, dt_IOS_SQL)(0)
                            If list.Contains(connStr) Then
                                sql = GetSQL(IOSSqlIds.OBJECTS_IMS, strArray, dt_IOS_SQL)(1)
                                dsTreeIMSVendor1 = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)
                                If Not dsTreeIMSVendor1 Is Nothing Then
                                    dsTreeIMSVendor1.WriteXml((GetUserDataPath() & "\dsTree_IMS.xml"), XmlWriteMode.WriteSchema)
                                End If
                            End If
                        End If
                        Return
                    Case "mgw"
                        dsTreeMGWVendor1 = New DataSet
                        If (File.Exists((GetUserDataPath() & "\dsTreeMGW.xml")) And Not reload) Then
                            dsTreeMGWVendor1.ReadXml((GetUserDataPath() & "\dsTreeMGW.xml"), XmlReadMode.ReadSchema)
                        Else
                            strArray2 = Nothing
                            strArray = Nothing
                            connStr = GetSQL(IOSSqlIds.OBJECTS_MGW, strArray2, dt_IOS_SQL)(0)
                            If list.Contains(connStr) Then
                                sql = GetSQL(IOSSqlIds.OBJECTS_MGW, strArray, dt_IOS_SQL)(1)
                                dsTreeMGWVendor1 = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)
                                If Not dsTreeMGWVendor1 Is Nothing Then
                                    dsTreeMGWVendor1.WriteXml((GetUserDataPath() & "\dsTreeMGW.xml"), XmlWriteMode.WriteSchema)
                                End If
                            End If
                        End If
                        Return
                    Case "mss"
                        dsTreeMSSVendor1 = New DataSet
                        If (File.Exists((GetUserDataPath() & "\dsTree_mss.xml")) And Not reload) Then
                            dsTreeMSSVendor1.ReadXml((GetUserDataPath() & "\dsTree_mss.xml"), XmlReadMode.ReadSchema)
                        Else
                            strArray2 = Nothing
                            strArray = Nothing
                            connStr = GetSQL(IOSSqlIds.OBJECTS_MSC, strArray2, dt_IOS_SQL)(0)
                            If list.Contains(connStr) Then
                                sql = GetSQL(IOSSqlIds.OBJECTS_MSC, strArray, dt_IOS_SQL)(1)
                                dsTreeMSSVendor1 = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)
                                If Not dsTreeMSSVendor1 Is Nothing Then
                                    dsTreeMSSVendor1.WriteXml((GetUserDataPath() & "\dsTree_mss.xml"), XmlWriteMode.WriteSchema)
                                End If
                            End If
                        End If
                        Return
                    Case "zone"
                        If (File.Exists((GetUserDataPath() & "\dsTree3G_zone.xml")) And Not reload) Then
                            dsTree3G_zone.ReadXml((GetUserDataPath() & "\dsTree3G_zone.xml"), XmlReadMode.ReadSchema)
                        Else
                            strArray2 = Nothing
                            strArray = Nothing
                            connStr = GetSQL(IOSSqlIds.OBJECTTREE_ZONE, strArray2, dt_IOS_SQL)(0)
                            If list.Contains(connStr) Then
                                sql = GetSQL(IOSSqlIds.OBJECTTREE_ZONE, strArray, dt_IOS_SQL)(1)
                                dsTree3G_zone = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)
                                If Not dsTree3G_zone Is Nothing Then
                                    dsTree3G_zone.WriteXml((GetUserDataPath() & "\dsTree3G_zone.xml"), XmlWriteMode.WriteSchema)
                                End If
                            End If
                        End If
                        Return
                    Case "region"
                        If (File.Exists((GetUserDataPath() & "\dsTree3G_region.xml")) And Not reload) Then
                            dsTree3G_region.ReadXml((GetUserDataPath() & "\dsTree3G_region.xml"), XmlReadMode.ReadSchema)
                        Else
                            strArray2 = Nothing
                            strArray = Nothing
                            connStr = GetSQL(&H401, strArray2, dt_IOS_SQL)(0)
                            If list.Contains(connStr) Then
                                sql = GetSQL(&H401, strArray, dt_IOS_SQL)(1)
                                dsTree3G_region = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)
                                If Not dsTree3G_region Is Nothing Then
                                    dsTree3G_region.WriteXml((GetUserDataPath() & "\dsTree3G_region.xml"), XmlWriteMode.WriteSchema)
                                End If
                            End If
                        End If
                        Return
                    Case "vci"
                        dsTree3G_VCI = New DataSet
                        If (File.Exists((GetUserDataPath() & "\dsTree3G_VCI.xml")) And Not reload) Then
                            dsTree3G_VCI.ReadXml((GetUserDataPath() & "\dsTree3G_VCI.xml"), XmlReadMode.ReadSchema)
                        Else
                            strArray2 = Nothing
                            strArray = Nothing
                            connStr = GetSQL(IOSSqlIds.OBJECTTREE_TX_VCI, strArray2, dt_IOS_SQL)(0)
                            If list.Contains(connStr) Then
                                sql = GetSQL(IOSSqlIds.OBJECTTREE_TX_VCI, strArray, dt_IOS_SQL)(1)
                                dsTree3G_VCI = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)
                                If Not dsTree3G_VCI Is Nothing Then
                                    dsTree3G_VCI.WriteXml((GetUserDataPath() & "\dsTree3G_VCI.xml"), XmlWriteMode.WriteSchema)
                                End If
                            End If
                        End If
                        If dsTree3G_VCI.Tables.Count > 0 Then
                            dsTree3G_VCI.Tables.Item(0).Merge(dsTree3G_VPI.Tables.Item(0))
                        End If
                        Exit Select
                    Case "mr"
                        dsTree3G_mr = New DataSet
                        If (File.Exists((GetUserDataPath() & "\dsTree3G_mr.xml")) And Not reload) Then
                            dsTree3G_mr.ReadXml((GetUserDataPath() & "\dsTree3G_mr.xml"), XmlReadMode.ReadSchema)
                        Else
                            strArray2 = Nothing
                            strArray = Nothing
                            connStr = GetSQL(IOSSqlIds.MR_3G, strArray2, dt_IOS_SQL)(0)
                            If list.Contains(connStr) Then
                                sql = GetSQL(IOSSqlIds.MR_3G, strArray, dt_IOS_SQL)(1)
                                dsTree3G_mr = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)
                                If Not dsTree3G_mr Is Nothing Then
                                    dsTree3G_mr.WriteXml((GetUserDataPath() & "\dsTree3G_mr.xml"), XmlWriteMode.WriteSchema)
                                End If
                            End If
                        End If
                        Return
                    Case "nanobts_cell"
                        dsTreeNanoBTS_cel = New DataSet
                        If (File.Exists((GetUserDataPath() & "\dsTreeNanoBTS_cel.xml")) And Not reload) Then
                            dsTreeNanoBTS_cel.ReadXml((GetUserDataPath() & "\dsTreeNanoBTS_cel.xml"), XmlReadMode.ReadSchema)
                        Else
                            strArray2 = Nothing
                            strArray = Nothing
                            connStr = GetSQL(&H2328, strArray2, dt_IOS_SQL)(0)
                            If list.Contains(connStr) Then
                                sql = GetSQL(&H2328, strArray, dt_IOS_SQL)(1)
                                dsTreeNanoBTS_cel = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)
                                If Not dsTreeNanoBTS_cel Is Nothing Then
                                    dsTreeNanoBTS_cel.WriteXml((GetUserDataPath() & "\dsTreeNanoBTS_cel.xml"), XmlWriteMode.WriteSchema)
                                End If
                            End If
                        End If
                        If Not dsTreeNanoBTS_cel Is Nothing Then
                            dsTreeNanoBTS_cel.Tables.Item(0).Merge(dsTreeNanoBTS_site.Tables.Item(0))
                        End If
                        Return
                    Case "nanobts_site"
                        dsTreeNanoBTS_site = New DataSet
                        If (File.Exists((GetUserDataPath() & "\dsTreeNanoBTS_site.xml")) And Not reload) Then
                            dsTreeNanoBTS_site.ReadXml((GetUserDataPath() & "\dsTreeNanoBTS_site.xml"), XmlReadMode.ReadSchema)
                        Else
                            strArray2 = Nothing
                            strArray = Nothing
                            connStr = GetSQL(&H2329, strArray2, dt_IOS_SQL)(0)
                            If list.Contains(connStr) Then
                                sql = GetSQL(&H2329, strArray, dt_IOS_SQL)(1)
                                dsTreeNanoBTS_site = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)
                                If Not dsTreeNanoBTS_site Is Nothing Then
                                    dsTreeNanoBTS_site.WriteXml((GetUserDataPath() & "\dsTreeNanoBTS_site.xml"), XmlWriteMode.WriteSchema)
                                End If
                            End If
                        End If
                        If Not dsTreeNanoBTS_site Is Nothing Then
                            dsTreeNanoBTS_site.Tables.Item(0).Merge(dsTreeNanoBTS_bsc.Tables.Item(0))
                        End If
                        Exit Select
                    Case "nanobts_bsc"
                        dsTreeNanoBTS_bsc = New DataSet
                        If (File.Exists((GetUserDataPath() & "\dsTreeNanoBTS_bsc.xml")) And Not reload) Then
                            dsTreeNanoBTS_bsc.ReadXml((GetUserDataPath() & "\dsTreeNanoBTS_bsc.xml"), XmlReadMode.ReadSchema)
                        Else
                            strArray2 = Nothing
                            strArray = Nothing
                            connStr = GetSQL(&H232A, strArray2, dt_IOS_SQL)(0)
                            If list.Contains(connStr) Then
                                sql = GetSQL(&H232A, strArray, dt_IOS_SQL)(1)
                                dsTreeNanoBTS_bsc = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)
                                If Not dsTreeNanoBTS_bsc Is Nothing Then
                                    dsTreeNanoBTS_bsc.WriteXml((GetUserDataPath() & "\dsTreeNanoBTS_bsc.xml"), XmlWriteMode.WriteSchema)
                                End If
                            End If
                        End If
                    Case "nano3g_cell"
                        dsTreeNano3g_cel = New DataSet
                        If (File.Exists((GetUserDataPath() & "\dsTreeNano3g_cel.xml")) And Not reload) Then
                            dsTreeNano3g_cel.ReadXml((GetUserDataPath() & "\dsTreeNano3g_cel.xml"), XmlReadMode.ReadSchema)
                        Else
                            strArray2 = Nothing
                            strArray = Nothing
                            connStr = GetSQL(IOSSqlIds.OBJECTTREE_NANO3G_CELL, strArray2, dt_IOS_SQL)(0)
                            If list.Contains(connStr) Then
                                sql = GetSQL(IOSSqlIds.OBJECTTREE_NANO3G_CELL, strArray, dt_IOS_SQL)(1)
                                dsTreeNano3g_cel = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)
                                If Not dsTreeNano3g_cel Is Nothing Then
                                    dsTreeNano3g_cel.WriteXml((GetUserDataPath() & "\dsTreeNano3g_cel.xml"), XmlWriteMode.WriteSchema)
                                End If
                            End If
                        End If
                        If Not dsTreeNano3g_cel Is Nothing Then
                            dsTreeNano3g_cel.Tables.Item(0).Merge(dsTreeNano3g_site.Tables.Item(0))
                        End If
                        Return
                    Case "nano3g_site"
                        dsTreeNano3g_site = New DataSet
                        If (File.Exists((GetUserDataPath() & "\dsTreeNano3g_site.xml")) And Not reload) Then
                            dsTreeNano3g_site.ReadXml((GetUserDataPath() & "\dsTreeNano3g_site.xml"), XmlReadMode.ReadSchema)
                        Else
                            strArray2 = Nothing
                            strArray = Nothing
                            connStr = GetSQL(IOSSqlIds.OBJECTTREE_NANO3G_SITE, strArray2, dt_IOS_SQL)(0)
                            If list.Contains(connStr) Then
                                sql = GetSQL(IOSSqlIds.OBJECTTREE_NANO3G_SITE, strArray, dt_IOS_SQL)(1)
                                dsTreeNano3g_site = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)
                                If Not dsTreeNano3g_site Is Nothing Then
                                    dsTreeNano3g_site.WriteXml((GetUserDataPath() & "\dsTreeNano3g_site.xml"), XmlWriteMode.WriteSchema)
                                End If
                            End If
                        End If
                        If Not dsTreeNano3g_site Is Nothing Then
                            dsTreeNano3g_site.Tables.Item(0).Merge(dsTreeNano3g_ac.Tables.Item(0))
                        End If
                        Exit Select
                    Case "nano3g_rnc"
                        dsTreeNano3g_ac = New DataSet
                        If (File.Exists((GetUserDataPath() & "\dsTreeNano3g_ac.xml")) And Not reload) Then
                            dsTreeNano3g_ac.ReadXml((GetUserDataPath() & "\dsTreeNano3g_ac.xml"), XmlReadMode.ReadSchema)
                        Else
                            strArray2 = Nothing
                            strArray = Nothing
                            connStr = GetSQL(IOSSqlIds.OBJECTTREE_NANO3G_RNC, strArray2, dt_IOS_SQL)(0)
                            If list.Contains(connStr) Then
                                sql = GetSQL(IOSSqlIds.OBJECTTREE_NANO3G_RNC, strArray, dt_IOS_SQL)(1)
                                dsTreeNano3g_ac = DataAccessorODBC.GetDataSet(connStr, sql, iQryTimeOut)
                                If Not dsTreeNano3g_ac Is Nothing Then
                                    dsTreeNano3g_ac.WriteXml((GetUserDataPath() & "\dsTreeNano3g_ac.xml"), XmlWriteMode.WriteSchema)
                                End If
                            End If
                        End If
                        Exit Select
                End Select
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Public Sub IOS_ObjectConfig_Load_New(ByVal tech As String, ByVal reload As Boolean)
        If dt_IOS_ObjectConfig Is Nothing Then
            dt_IOS_ObjectConfig = clsSQLCommands.Get_ObjectConfig_New_Data_By_Tech(connStrIOSServer, tech)
        End If

        If Not dt_IOS_ObjectConfig Is Nothing Then
            If tech = "ALL" Then
                For Each drow As DataRow In dt_IOS_ObjectConfig.Rows
                    ObjectTree_DataSet_Load_new(drow, reload, dtIOSSources)
                Next
            Else
                For Each drow As DataRow In dt_IOS_ObjectConfig.Rows
                    If tech.ToUpper = drow("Tech").ToString.ToUpper Then
                        ObjectTree_DataSet_Load_new(drow, reload, dtIOSSources)
                    End If
                Next
            End If
        End If
        IOS_ObjectConfig_Active()
    End Sub

    Public Sub IOS_ObjectConfig_Tech(ByVal dt As DataTable)
        Dim arryVandor() As String = {"TechInternal", "Tech"}
        Dim dtTechInternal As DataTable = dt.DistinctCol(arryVandor)
        If (dtTechInternal.IsValid) Then
            For Each dr As DataRow In dtTechInternal.Rows
                Select Case dr("TechInternal").ToString
                    Case IOSInternalTechnology.Tech2G1
                        networkAll.Network2G1 = dr("Tech").ToString
                    Case IOSInternalTechnology.Tech2G2
                        networkAll.Network2G2 = dr("Tech").ToString
                    Case IOSInternalTechnology.Tech2G3
                        networkAll.Network2G3 = dr("Tech").ToString
                    Case IOSInternalTechnology.Tech3G1
                        networkAll.Network3G1 = dr("Tech").ToString
                    Case IOSInternalTechnology.Tech3G2
                        networkAll.Network3G2 = dr("Tech").ToString
                    Case IOSInternalTechnology.Tech3G3
                        networkAll.Network3G3 = dr("Tech").ToString
                    Case IOSInternalTechnology.Tech4G1
                        networkAll.Network4G1 = dr("Tech").ToString
                    Case IOSInternalTechnology.Tech4G2
                        networkAll.Network4G2 = dr("Tech").ToString
                    Case IOSInternalTechnology.Tech4G3
                        networkAll.Network4G3 = dr("Tech").ToString
                    Case IOSInternalTechnology.Tech5G1
                        networkAll.Network5G1 = dr("Tech").ToString
                    Case IOSInternalTechnology.Tech5G2
                        networkAll.Network5G2 = dr("Tech").ToString
                    Case IOSInternalTechnology.Tech5G3
                        networkAll.Network5G3 = dr("Tech").ToString
                    Case IOSInternalTechnology.EPC1
                        networkAll.NetworkEPC1 = dr("Tech").ToString
                    Case IOSInternalTechnology.EPC2
                        networkAll.NetworkEPC2 = dr("Tech").ToString
                    Case IOSInternalTechnology.EPC3
                        networkAll.NetworkEPC3 = dr("Tech").ToString
                    Case IOSInternalTechnology.CDRMSC
                        networkAll.NetworkMSCCDR = dr("Tech").ToString
                    Case IOSInternalTechnology.CDRSGSN
                        networkAll.NetworkSGSNCDR = dr("Tech").ToString
                    Case IOSInternalTechnology.CDRGGSN
                        networkAll.NetworkGGSNCDR = dr("Tech").ToString
                    Case IOSInternalTechnology.GGSN
                        networkAll.NetworkGGSN = dr("Tech").ToString
                    Case IOSInternalTechnology.IMS
                        networkAll.NetworkIMS = dr("Tech").ToString
                    Case IOSInternalTechnology.MEE
                        networkAll.NetworkMEE = dr("Tech").ToString
                    Case IOSInternalTechnology.MGW
                        networkAll.NetworkMGW = dr("Tech").ToString
                    Case IOSInternalTechnology.MSS
                        networkAll.NetworkMSS = dr("Tech").ToString
                    Case IOSInternalTechnology.PGW
                        networkAll.NetworkPGW = dr("Tech").ToString
                    Case IOSInternalTechnology.SGSN
                        networkAll.NetworkSGSN = dr("Tech").ToString
                    Case IOSInternalTechnology.SGW
                        networkAll.NetworkSGW = dr("Tech").ToString
                    Case IOSInternalTechnology.TX
                        networkAll.NetworkTX = dr("Tech").ToString
                    Case IOSInternalTechnology.TX2
                        networkAll.NetworkTX2 = dr("Tech").ToString
                    Case IOSInternalTechnology.COMMON
                        networkAll.NetworkCommon = dr("Tech").ToString
                    Case IOSInternalTechnology.TRANSPORT
                        networkAll.NetworkTransport = dr("Tech").ToString
                    Case IOSInternalTechnology.PDUM
                        networkAll.NetworkPDUM = dr("Tech").ToString
                    Case IOSInternalTechnology.TWAMP
                        networkAll.NetworkTWAMP = dr("Tech").ToString
                    Case IOSInternalTechnology.HLR
                        networkAll.NetworkHLR = dr("Tech").ToString
                    Case IOSInternalTechnology.DWDM
                        networkAll.NetworkDWDM = dr("Tech").ToString
                    Case IOSInternalTechnology.HSS
                        networkAll.NetworkHSS = dr("Tech").ToString
                    Case IOSInternalTechnology.UDR
                        networkAll.NetworkUDR = dr("Tech").ToString
                End Select
            Next
        End If
    End Sub

    Public Sub IOS_ObjectConfig_Active()
        Dim aResult As IAsyncResult = Nothing
        Dim sql_IOS_ObjectActive As String = Nothing
        dtIOSObjectActive = clsSQLCommands.Get_IOS_ObjectConfig_Active_Data(connStrIOSServer)
    End Sub

    Public Sub IOS_KPITrees_Load(ByVal tech As String, Optional chSetName As String = Nothing)
        Dim dt_IOS_KPITreeConfig As DataTable
        If tech = "ALL" Then
            dt_IOS_KPITreeConfig = clsSQLCommands.Get_ChartConfig_Data(connStrIOSServer)
        Else
            dt_IOS_KPITreeConfig = clsSQLCommands.Get_ChartConfig_Data_By_Tech(connStrIOSServer, tech)
        End If

        If Not dt_IOS_KPITreeConfig Is Nothing Then
            For Each drow As DataRow In dt_IOS_KPITreeConfig.Rows
                KPITree_DataSet_Load(drow(0).ToString, chSetName)
            Next
        End If
    End Sub

    Public Sub KPITree_DataSet_Load(ByVal tech As String, Optional chSetName As String = Nothing)
        Try
            Dim username As String = Environment.UserName
            Dim csn As String = chartSetName
            If chSetName IsNot Nothing Then
                csn = chSetName
            End If
            Dim parray()() As String = {
                New String() {"@chartsetname", Chr(39) & csn & Chr(39)},
                New String() {"@username", Chr(39) & username & Chr(39)}
            }
            Dim sqlAndConnectionStr() As String

            Select Case tech.ToUpper
                Case networkAll.Network2G1.ToUpper
                    dsTree2G_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_2G, parray, dt_IOS_SQL)
                    dsTree2G_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case networkAll.Network2G2.ToUpper
                    dsTreeNanoBTS_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_NANOBTS, parray, dt_IOS_SQL)
                    dsTreeNanoBTS_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case networkAll.Network2G3.ToUpper
                    dsTree2G3_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_2G3, parray, dt_IOS_SQL)
                    dsTree2G3_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)

                Case networkAll.Network3G1.ToUpper
                    dsTree3G_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_3G, parray, dt_IOS_SQL)
                    dsTree3G_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case networkAll.Network3G2.ToUpper
                    dsTreeNano3G_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_NANO3G, parray, dt_IOS_SQL)
                    dsTreeNano3G_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case networkAll.Network3G3.ToUpper
                    dsTree3G3_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_HUAWEI3G, parray, dt_IOS_SQL)
                    dsTree3G3_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)

                Case networkAll.Network4G1.ToUpper
                    dsTree4G1_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_4G1, parray, dt_IOS_SQL)
                    dsTree4G1_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case networkAll.Network4G2.ToUpper
                    dsTree4G2_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_4G2, parray, dt_IOS_SQL)
                    dsTree4G2_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case networkAll.Network4G3.ToUpper
                    dsTree4G3_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_4G3, parray, dt_IOS_SQL)
                    dsTree4G3_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)

                Case networkAll.Network5G1.ToUpper
                    dsTree5G1_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_5G1, parray, dt_IOS_SQL)
                    dsTree5G1_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case networkAll.Network5G2.ToUpper
                    dsTree5G2_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_5G2, parray, dt_IOS_SQL)
                    dsTree5G2_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case networkAll.Network5G3.ToUpper
                    dsTree5G3_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_5G3, parray, dt_IOS_SQL)
                    dsTree5G3_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)

                Case networkAll.NetworkNode1.ToUpper
                    dsTreeNode1_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_NODE1, parray, dt_IOS_SQL)
                    dsTreeNode1_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case networkAll.NetworkNode2.ToUpper
                    dsTreeNode2_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_NODE2, parray, dt_IOS_SQL)
                    dsTreeNode2_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case networkAll.NetworkNode3.ToUpper
                    dsTreeNode3_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_NODE3, parray, dt_IOS_SQL)
                    dsTreeNode3_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)

                Case networkAll.NetworkMSCCDR.ToUpper
                    dsTreeMSC_Kpi_CDR = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL("50015", parray, dt_IOS_SQL)
                    dsTreeMSC_Kpi_CDR = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case networkAll.NetworkSGSNCDR.ToUpper
                    dsTreeSGSN_Kpi_CDR = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL("50017", parray, dt_IOS_SQL)
                    dsTreeSGSN_Kpi_CDR = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case networkAll.NetworkGGSNCDR.ToUpper
                    dsTreeGGSN_Kpi_CDR = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL("50019", parray, dt_IOS_SQL)
                    dsTreeGGSN_Kpi_CDR = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)

                Case networkAll.NetworkCommon.ToUpper
                    dsTreeCommon_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_COMMON, parray, dt_IOS_SQL)
                    dsTreeCommon_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)

                Case "TOPX_" & networkAll.Network2G1.ToUpper
                    dsTreeTopX2G_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_TOPX_2G, parray, dt_IOS_SQL)
                    dsTreeTopX2G_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case "TOPX_" & networkAll.Network2G2.ToUpper
                    dsTreeTopXNanoBTS_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_TOPX_NANOBTS, parray, dt_IOS_SQL)
                    dsTreeTopXNanoBTS_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case "TOPX_" & networkAll.Network2G3.ToUpper
                    dsTreeTopX2G3_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_Tree_TopX_2G3, parray, dt_IOS_SQL)
                    dsTreeTopX2G3_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)

                Case "TOPX_" & networkAll.Network3G1.ToUpper
                    dsTreeTopX3G_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_TOPX_3G, parray, dt_IOS_SQL)
                    dsTreeTopX3G_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case "TOPX_" & networkAll.Network3G2.ToUpper
                    dsTreeTopXNano3G_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_TOPX_NANO3G, parray, dt_IOS_SQL)
                    dsTreeTopXNano3G_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case "TOPX_" & networkAll.Network3G3.ToUpper
                    dsTreeTopX3G3_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_Tree_TopX_HUAWEI3G, parray, dt_IOS_SQL)
                    dsTreeTopX3G3_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)

                Case "TOPX_" & networkAll.Network4G1.ToUpper
                    dsTreeTopX4G1_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_TOPX_4G1, parray, dt_IOS_SQL)
                    dsTreeTopX4G1_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case "TOPX_" & networkAll.Network4G2.ToUpper
                    dsTreeTopX4G2_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_Tree_TopX_4G2, parray, dt_IOS_SQL)
                    dsTreeTopX4G2_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case "TOPX_" & networkAll.Network4G3.ToUpper
                    dsTreeTopX4G3_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_Tree_TopX_4G3, parray, dt_IOS_SQL)
                    dsTreeTopX4G3_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)

                Case "TOPX_" & networkAll.Network5G1.ToUpper
                    dsTreeTopX5G1_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_TOPX_5G1, parray, dt_IOS_SQL)
                    dsTreeTopX5G1_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case "TOPX_" & networkAll.Network5G2.ToUpper
                    dsTreeTopX5G2_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_Tree_TopX_5G2, parray, dt_IOS_SQL)
                    dsTreeTopX5G2_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case "TOPX_" & networkAll.Network5G3.ToUpper
                    dsTreeTopX5G3_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_Tree_TopX_5G3, parray, dt_IOS_SQL)
                    dsTreeTopX5G3_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)

                Case "TOPX_" & networkAll.NetworkNode1.ToUpper
                    dsTreeTopXNode1_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_TOPX_NODE1, parray, dt_IOS_SQL)
                    dsTreeTopXNode1_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case "TOPX_" & networkAll.NetworkNode2.ToUpper
                    dsTreeTopXNode2_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_TOPX_NODE2, parray, dt_IOS_SQL)
                    dsTreeTopXNode2_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case "TOPX_" & networkAll.NetworkNode3.ToUpper
                    dsTreeTopXNode3_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_TOPX_NODE3, parray, dt_IOS_SQL)
                    dsTreeTopXNode3_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)

                Case "TOPX_" & networkAll.NetworkMSCCDR.ToUpper
                    dsTreeTopXMSC_Kpi_CDR = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL("50016", parray, dt_IOS_SQL)
                    dsTreeTopXMSC_Kpi_CDR = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case "TOPX_" & networkAll.NetworkSGSNCDR.ToUpper
                    dsTreeTopXSGSN_Kpi_CDR = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL("50018", parray, dt_IOS_SQL)
                    dsTreeTopXSGSN_Kpi_CDR = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case "TOPX_" & networkAll.NetworkGGSNCDR.ToUpper
                    dsTreeTopXGGSN_Kpi_CDR = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL("50020", parray, dt_IOS_SQL)
                    dsTreeTopXGGSN_Kpi_CDR = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case "TOPX_" & networkAll.NetworkCommon.ToUpper
                    dsTreeTopXCommon_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_TOPX_COMMON, parray, dt_IOS_SQL)
                    dsTreeTopXCommon_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)

                Case networkAll.NetworkMSS.ToUpper
                    dsTreeMSS_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL("1112", parray, dt_IOS_SQL)
                    dsTreeMSS_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case networkAll.NetworkMGW.ToUpper
                    dsTreeMGW_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL("1113", parray, dt_IOS_SQL)
                    dsTreeMGW_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case networkAll.NetworkEPC1.ToUpper
                    dsTreeMME_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL("11015", parray, dt_IOS_SQL)
                    dsTreeMME_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case networkAll.NetworkEPC2.ToUpper
                    dsTreeSGW_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL("12015", parray, dt_IOS_SQL)
                    dsTreeSGW_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case networkAll.NetworkPGW.ToUpper
                    dsTreePGW_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL("13015", parray, dt_IOS_SQL)
                    dsTreePGW_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case networkAll.NetworkSAPC.ToUpper
                    dsTreeSAPC_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL("13115", parray, dt_IOS_SQL)
                    dsTreeSAPC_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case networkAll.NetworkIMS.ToUpper
                    dsTreeIMS_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL("1120", parray, dt_IOS_SQL)
                    dsTreeIMS_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case networkAll.NetworkSGSN.ToUpper
                    dsTreeSGSN_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL("1114", parray, dt_IOS_SQL)
                    dsTreeSGSN_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case networkAll.NetworkGGSN.ToUpper
                    dsTreeGGSN_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL("1116", parray, dt_IOS_SQL)
                    dsTreeGGSN_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)

                Case networkAll.NetworkTX.ToUpper
                    dsTreeTM_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_TX, parray, dt_IOS_SQL)
                    dsTreeTM_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case "TOPX_" & networkAll.NetworkTX.ToUpper
                    dsTreeTopXTM_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_TOPX_TX, parray, dt_IOS_SQL)
                    dsTreeTopXTM_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)

                Case networkAll.NetworkTX2.ToUpper
                    dsTreeTM2_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_TX2, parray, dt_IOS_SQL)
                    dsTreeTM2_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case "TOPX_" & networkAll.NetworkTX2.ToUpper
                    dsTreeTopXTM2_Kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_TOPX_TX2, parray, dt_IOS_SQL)
                    dsTreeTopXTM2_Kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)

                Case networkAll.NetworkTransport.ToUpper
                    dsTreeTransport_kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_TRANSPORT, parray, dt_IOS_SQL)
                    dsTreeTransport_kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case "TOPX_" & networkAll.NetworkTransport.ToUpper
                    dsTreeTopXTransport_kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_TOPX_TRANSPORT, parray, dt_IOS_SQL)
                    dsTreeTopXTransport_kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)

                Case networkAll.NetworkPDUM.ToUpper
                    dsTreePDUM_kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_PDUM, parray, dt_IOS_SQL)
                    dsTreePDUM_kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case "TOPX_" & networkAll.NetworkPDUM.ToUpper
                    dsTreeTopXPDUM_kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_TOPX_PDUM, parray, dt_IOS_SQL)
                    dsTreeTopXPDUM_kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)

                Case networkAll.NetworkTWAMP.ToUpper
                    dsTreeTwamp_kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_TWAMP, parray, dt_IOS_SQL)
                    dsTreeTwamp_kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case "TOPX_" & networkAll.NetworkTWAMP.ToUpper
                    dsTreeTopXTwamp_kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_TOPX_TWAMP, parray, dt_IOS_SQL)
                    dsTreeTopXTwamp_kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)

                Case networkAll.NetworkHLR.ToUpper
                    dsTreeHLR_kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_HLR, parray, dt_IOS_SQL)
                    dsTreeHLR_kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case "TOPX_" & networkAll.NetworkHLR.ToUpper
                    dsTreeTopXHLR_kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_TOPX_HLR, parray, dt_IOS_SQL)
                    dsTreeTopXHLR_kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)

                Case networkAll.NetworkDWDM.ToUpper
                    dsTreeDwdm_kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_DWDM, parray, dt_IOS_SQL)
                    dsTreeDwdm_kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case "TOPX_" & networkAll.NetworkDWDM.ToUpper
                    dsTreeTopXDwdm_kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_TOPX_DWDM, parray, dt_IOS_SQL)
                    dsTreeTopXDwdm_kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)

                Case networkAll.NetworkHSS.ToUpper
                    dsTreeHSS_kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_HSS, parray, dt_IOS_SQL)
                    dsTreeHSS_kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case "TOPX_" & networkAll.NetworkHSS.ToUpper
                    dsTreeTopXHSS_kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_TOPX_HSS, parray, dt_IOS_SQL)
                    dsTreeTopXHSS_kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)

                Case networkAll.NetworkUDR.ToUpper
                    dsTreeUDR_kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_UDR, parray, dt_IOS_SQL)
                    dsTreeUDR_kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
                Case "TOPX_" & networkAll.NetworkUDR.ToUpper
                    dsTreeTopXUDR_kpi = New System.Data.DataSet
                    sqlAndConnectionStr = GetSQL(IOSSqlIds.KPI_TREE_TOPX_UDR, parray, dt_IOS_SQL)
                    dsTreeTopXUDR_kpi = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1), iQryTimeOut)
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Public Sub IOS_ObjectConfig_Load(ByVal tech As String, ByVal reload As Boolean)
        Dim aResult As IAsyncResult = Nothing
        Dim dt_IOS_ObjectConfig As DataTable = Nothing

        If tech = "ALL" Then
            dt_IOS_ObjectConfig = clsSQLCommands.Get_ObjectConfig_Data(connStrIOSServer)
        Else
            dt_IOS_ObjectConfig = clsSQLCommands.Get_ObjectConfig_Data_By_tech(connStrIOSServer, tech)
        End If

        WriteString_Log(Now() & "    " & "Configuration: Querying Object Configuration")
        If Not dt_IOS_ObjectConfig Is Nothing Then
            For Each drow As DataRow In dt_IOS_ObjectConfig.Rows
                ObjectTree_DataSet_Load(drow(0).ToString.ToLower, reload, dtIOSSources)
                ''WriteString_Log(Now() & "    " & "Configuration: Object Configuration - " & drow(0).ToString.ToLower & " - Loaded")
                ''If SplashScreen.Visible = True Then
                ''    aResult = My.Application.SplashScreen.BeginInvoke( _
                ''            New SplashScreen.UpdateStatusLabel( _
                ''            AddressOf SplashScreen.Update_ProgressBar))
                ''End If
            Next
        End If
    End Sub

    Public Function GetObjectTreeDSByTechInternal(_TechInternal As String) As DataSet
        Select Case _TechInternal.ToString
            Case "2G1"
                Return dsTree2GVendor1
            Case "2G2"
                Return dsTree2GVendor2
            Case "2G3"
                Return dsTree2GVendor3
            Case "3G1"
                Return dsTree3GVendor1
            Case "3G2"
                Return dsTree3GVendor2
            Case "3G3"
                Return dsTree3GVendor3
            Case "4G1"
                Return dsTree4GVendor1
            Case "4G2"
                Return dsTree4GVendor2
            Case "4G3"
                Return dsTree4GVendor3
            Case "5G1"
                Return dsTree5GVendor1
            Case "5G2"
                Return dsTree5GVendor2
            Case "5G3"
                Return dsTree5GVendor3
            Case "NODE1"
                Return dsTreeNodeVendor1
            Case "NODE2"
                Return dsTreeNodeVendor2
            Case "NODE3"
                Return dsTreeNodeVendor3
            Case "CDRMSC"
                Return dsTreeMSCVendorCDR
            Case "CDRSGSN"
                Return dsTreeSGSNVendorCDR
            Case "CDRGGSN"
                Return dsTreeGGSNVendorCDR
            Case "EPC1"
                Return dsTreeMMEVendor1
            Case "MSS"
                Return dsTreeMSSVendor1
            Case "MSC"
                Return dsTreeMSCVendor1
            Case "MGW"
                Return dsTreeMGWVendor1
            Case "SGSN"
                Return dsTreeSGSNVendor1
            Case "GGSN"
                Return dsTreeGGSNVendor1
            Case "IMS"
                Return dsTreeIMSVendor1
            Case "EPC2"
                Return dsTreeSGWVendor1
            Case "PGW"
                Return dsTreePGWVendor1
            Case "SAPC"
                Return dsTreeSAPCVendor1
            Case "COMMON"
                Return dsTreeCommonTech
            Case "TX"
                Return dsTreeTXVendor1
            Case "TX2"
                Return dsTreeTX2Vendor1
            Case "TRANSPORT"
                Return dsTreeTransportVendor1
            Case "PDUM"
                Return dsTreePDUMVendor
            Case "TWAMP"
                Return dsTreeTwampVendor
            Case "HLR"
                Return dsTreeHLRVendor
            Case "DWDM"
                Return dsTreeDwdmVendor
            Case "HSS"
                Return dsTreeHSSVendor
            Case "UDR"
                Return dsTreeUDRVendor
        End Select
        Return Nothing
    End Function

#End Region

#Region "DevExpress GridView Set Hyperlink Column"

    Public Sub SetHyperlinkColumnsInGridControl(ByRef gridCtrl As DevExpress.XtraGrid.GridControl, ByRef grdView As DevExpress.XtraGrid.Views.Grid.GridView, ByVal dt As DataTable)
        Try
            If (dt Is Nothing) Then
                Return
            End If

            Dim dicHyper As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))
            Dim colNameHyperMapList As List(Of String) = New List(Of String)
            Dim colNameAll As List(Of String) = New List(Of String)
            Dim dtCol As DataColumnCollection = dt.Columns
            Dim dtCollHyper As DataColumnCollection = dt.Columns
            Dim colNameHyperList As List(Of String) = New List(Of String)
            Dim dtNew As DataTable = New DataTable()

            For Each _dataColumn As DataColumn In dtCol
                colNameAll.Add(_dataColumn.ColumnName)
                If (_dataColumn.ColumnName.ToUpper.Contains("HTTP")) Then
                    colNameHyperList.Add(_dataColumn.ColumnName)
                    Dim listofColOrdinal As List(Of String) = New List(Of String)
                    listofColOrdinal.Add(_dataColumn.Ordinal)
                    colNameHyperMapList.Add(_dataColumn.ColumnName)

                    If _dataColumn.ColumnName.Contains("_") Then
                        listofColOrdinal.Add(dt.Columns(_dataColumn.ColumnName.Split("_")(1)).Ordinal)
                    Else
                        listofColOrdinal.Add(dt.Columns(_dataColumn.ColumnName).Ordinal)
                    End If
                    dicHyper.Add(_dataColumn.ColumnName.ToString, listofColOrdinal)
                Else
                    If (colNameHyperMapList.Count > 0) Then
                        If (Not colNameHyperMapList.Contains(_dataColumn.ColumnName.ToString)) Then
                            Dim listofColOrdinal As List(Of String) = New List(Of String)
                            listofColOrdinal.Add(_dataColumn.Ordinal)
                            listofColOrdinal.Add(0)
                            dicHyper.Add(_dataColumn.ColumnName.ToString, listofColOrdinal)
                        End If
                    Else
                        Dim isMapable As Boolean = False
                        For Each _dataColumnNoneHyper As DataColumn In dt.Columns
                            Dim splitArra As String() = _dataColumnNoneHyper.ColumnName.Split("_")
                            If (splitArra.Length > 1) Then
                                If (_dataColumnNoneHyper.ColumnName.Split("_")(1).ToUpper = _dataColumn.ColumnName.ToUpper) Then
                                    isMapable = True
                                    Exit For
                                End If
                            Else
                                If (_dataColumnNoneHyper.ColumnName.ToUpper = _dataColumn.ColumnName.ToUpper) Then
                                    isMapable = False
                                    Exit For
                                End If
                            End If
                        Next
                        If (Not isMapable) Then
                            Dim listofColOrdinal As List(Of String) = New List(Of String)
                            listofColOrdinal.Add(_dataColumn.Ordinal)
                            listofColOrdinal.Add(0)
                            dicHyper.Add(_dataColumn.ColumnName.ToString, listofColOrdinal)
                        End If
                    End If
                End If
            Next

            Dim newDT As DataTable = New DataTable
            Dim colNameNoneHype As List(Of String) = New List(Of String)()

            Dim colIndex As Integer = 0
            For Each _dataColumn As String In colNameAll
                If (_dataColumn.ToUpper.Contains("HTTP")) Then
                    If _dataColumn.Contains("_") Then
                        dt.Columns(_dataColumn.Split("_")(1).ToString).ColumnName = _dataColumn.Split("_")(1).ToString & "_#"
                    Else
                        dt.Columns(_dataColumn).ColumnName = _dataColumn.ToString & "_#"
                    End If
                End If
            Next

            grdView.Columns.Clear()
            For Each dCol As DataColumn In dt.Columns
                Dim gCol As New DevExpress.XtraGrid.Columns.GridColumn()
                If (dCol.ColumnName.Contains("_#")) Then
                    gCol.Caption = dCol.ColumnName.Replace("_#", "")
                Else
                    gCol.Caption = dCol.Caption
                End If
                gCol.Visible = True
                gCol.Name = dCol.ColumnName
                gCol.FieldName = dCol.ColumnName

                If dCol.DataType = GetType(DateTime) Then
                    gCol.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    If regionalSettings = False Then
                        gCol.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss"
                    Else
                        gCol.DisplayFormat.FormatString = CultureInfoDefault.DateTimeFormat.ShortDatePattern & " " & CultureInfoDefault.DateTimeFormat.ShortTimePattern
                    End If
                End If
                grdView.Columns.Add(gCol)
            Next

            grdView.OptionsBehavior.AutoPopulateColumns = False
            gridCtrl.DataSource = Nothing
            gridCtrl.DataSource = dt

            For Each col As DevExpress.XtraGrid.Columns.GridColumn In grdView.Columns
                If (col.Name.Contains("#")) Then
                    col.AppearanceCell.ForeColor = Color.Blue
                    col.AppearanceCell.Font = New Font("Arial", 8.0F, FontStyle.Underline)
                    col.Tag = "Link"
                End If
                If (col.Name.Contains("url")) Then
                    col.AppearanceCell.ForeColor = Color.Blue
                    col.AppearanceCell.Font = New Font("Arial", 8.0F, FontStyle.Underline)
                    col.Tag = "Link"
                End If
                If col.Caption.Contains("HTTP_") Or col.Caption.Contains("http_") Then
                    col.Visible = False
                End If
            Next

            RemoveHandler grdView.RowCellClick, AddressOf GridControl_RowCellClick
            AddHandler grdView.RowCellClick, AddressOf GridControl_RowCellClick

            grdView.OptionsView.ColumnAutoWidth = False
            grdView.BestFitColumns()
            gridCtrl.Refresh()

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub GridControl_RowCellClick(sender As Object, args As Views.Grid.RowCellClickEventArgs)
        Try
            Dim selectedCellObj As GridView = TryCast(sender, GridView)
            If args.Button = MouseButtons.Left Then
                If args.Column.Tag IsNot Nothing Then
                    If (args.Column.Tag.ToString.ToUpper = "LINK") Then
                        Dim selectedColumn As String = args.Column.Caption.Split("_")(0)
                        Dim dr As DataRow = selectedCellObj.GetDataRow(args.RowHandle)
                        For Each col As DataColumn In dr.Table.Columns
                            If ("HTTP_" & selectedColumn.ToUpper = col.Caption.ToUpper) Then
                                Process.Start(dr(col.ColumnName))
                            End If
                        Next
                        If selectedColumn.ToUpper = "URL" Then
                            If objTechTicketsUrlWC Is Nothing Then
                                objTechTicketsUrlWC = New frmInternetExplorer("tickets", dr(selectedColumn))
                            End If
                            objTechTicketsUrlWC.NavigationUrl = dr(selectedColumn)
                            objTechTicketsUrlWC.WebRequestFrom = "Technology"
                            frmMDI.OpenFormAsDockPanel("Tickets Web Url",, objTechTicketsUrlWC, dr(selectedColumn).ToString)
                        ElseIf selectedColumn.ToUpper = "HTTP" Then
                            'NBI Report Status grid
                            If selectedCellObj.Name = "gvReportStatus" Then
                                Clipboard.Clear()
                                Clipboard.SetText(args.CellValue.ToString)
                                XtraMessageBox.Show("The link is copied to the clipboard", "Report Status Link")
                                Exit Sub
                            End If
                            If objTechTicketsUrlWC Is Nothing Then
                                objTechTicketsUrlWC = New frmInternetExplorer("NBIReports", args.CellValue)
                            End If
                            objTechTicketsUrlWC.NavigationUrl = args.CellValue
                            objTechTicketsUrlWC.WebRequestFrom = "NBIReports"
                            frmMDI.OpenFormAsDockPanel("NBIReports Web Url",, objTechTicketsUrlWC, args.CellValue.ToString)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub LoadGridWithHyperlink(grid As GridControl, view As GridView, table As DataTable, columnName As String, Optional columnToHide As String = Nothing)
        Try
            view.OptionsView.ColumnAutoWidth = False
            view.OptionsBehavior.AutoPopulateColumns = True
            view.Columns.Clear()
            grid.DataSource = table
            grid.MainView = view
            view.PopulateColumns()

            If Not String.IsNullOrWhiteSpace(columnToHide) AndAlso view.Columns.ColumnByFieldName(columnToHide) IsNot Nothing Then
                view.Columns(columnToHide).Visible = False
            End If

            If Not table.Columns.Contains(columnName) Then Exit Sub

            ' Hyperlink editor
            Dim hyperlinkEdit As New RepositoryItemHyperLinkEdit()
            hyperlinkEdit.SingleClick = True
            hyperlinkEdit.Appearance.ForeColor = Color.Blue
            hyperlinkEdit.Appearance.Font = New Font(view.Appearance.Row.Font, FontStyle.Underline)
            hyperlinkEdit.Appearance.Options.UseForeColor = True
            hyperlinkEdit.Appearance.Options.UseFont = True

            grid.RepositoryItems.Add(hyperlinkEdit)
            view.Columns(columnName).ColumnEdit = hyperlinkEdit

            ' Display ticket number only
            AddHandler view.CustomColumnDisplayText,
            Sub(sender, e)
                If e.Column.FieldName = columnName AndAlso e.Value IsNot Nothing Then
                    Dim match = Regex.Match(e.Value.ToString(),
                        "<a[^>]*>(.*?)</a>",
                        RegexOptions.IgnoreCase)

                    If match.Success Then
                        e.DisplayText = match.Groups(1).Value
                    End If
                End If
            End Sub

            ' Change cursor to hand on hover
            AddHandler view.RowCellStyle,
            Sub(sender, e)
                If e.Column.FieldName = columnName Then
                    e.Appearance.ForeColor = Color.Blue
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Underline)
                End If
            End Sub

            ' Handle click
            AddHandler view.RowCellClick,
            Sub(sender, e)
                If e.Column.FieldName <> columnName Then Exit Sub

                Dim html As String = view.GetRowCellValue(e.RowHandle, columnName).ToString()

                Dim match = Regex.Match(html, "href\s*=\s*""([^""]+)""", RegexOptions.IgnoreCase)

                If match.Success Then
                    If columnName.ToLower = "ticketurl" Then
                        'If objTechTicketsUrlWC Is Nothing Then
                        '    objTechTicketsUrlWC = New frmInternetExplorer("PM TopX Ticket Url", match.Groups(1).Value)
                        'End If
                        'objTechTicketsUrlWC.NavigationUrl = match.Groups(1).Value.ToString
                        'objTechTicketsUrlWC.WebRequestFrom = "PMTopX"
                        'frmMDI.OpenFormAsDockPanel("PM TopX Ticket Url",, objTechTicketsUrlWC, match.Groups(1).Value.ToString)
                        If Not String.IsNullOrWhiteSpace(match.Groups(1).Value.ToString) Then
                            Process.Start(New ProcessStartInfo(match.Groups(1).Value.ToString) With {.UseShellExecute = True})
                        End If
                    ElseIf columnName.ToLower = "weblink" Then
                        If objReportEditWebLinkWC Is Nothing Then
                            objReportEditWebLinkWC = New frmInternetExplorer("Report Editor WebLink", match.Groups(1).Value)
                        End If
                        objReportEditWebLinkWC.NavigationUrl = match.Groups(1).Value.ToString
                        objReportEditWebLinkWC.WebRequestFrom = "ReportEditor"
                        frmMDI.OpenFormAsDockPanel("Report Editor WebLink",, objReportEditWebLinkWC, match.Groups(1).Value.ToString)
                    End If
                End If
            End Sub

            view.OptionsBehavior.Editable = False
            view.OptionsView.ShowGroupPanel = False
            view.BestFitColumns()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

#End Region

#Region "NB Management/Tilt Manager"

    Public Sub UpdateCampaignLastStatus(CampaignID As Integer, LastStatus As Integer)
        Dim parray()() As String = {
            New String() {"@CampaignID", CampaignID},
            New String() {"@LastStatus", LastStatus}
        }
        Dim strConnection As String = GetSQL(4547, parray)(0)
        Dim sqlParam As String = GetSQL(4547, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Public Sub LoadCellList()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        strConnection = GetSQL(4512, parray)(0)
        sqlParam = GetSQL(4512, parray)(1)

        dtCellList = New DataTable()
        dtCellList = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Sub

    Public Sub LoadLayers()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        strConnection = GetSQL(4506, Nothing)(0)
        sqlParam = GetSQL(4506, Nothing)(1)

        dtLayer = New DataTable
        dtLayer = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Sub

    Public Sub UpdateTiltCampaignLastStatus(CampaignID As Integer, LastStatus As Integer)
        Dim parray()() As String = {
            New String() {"@CampaignID", CampaignID},
            New String() {"@LastStatus", LastStatus}
        }
        Dim strConnection As String = GetSQL(4926, parray)(0)
        Dim sqlParam As String = GetSQL(4926, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

#End Region

#Region "Ref Check"

    Public Sub UpdateTemplateLastStatus(templateID As Integer, lastStatus As Integer)
        Dim parray()() As String = {
            New String() {"@templateID", templateID},
            New String() {"@lastStatus", lastStatus}
        }
        Dim strConnection As String = GetSQL(4147, parray)(0)
        Dim sqlParam As String = GetSQL(4147, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Public Sub GetTextboxDataWithAutoCompleteFeature(ByRef txt As TextEdit, ByVal sql As String)
        Try
            Dim str() As String = Nothing
            Dim tempDT As New DataTable
            tempDT = DataAccessorODBC.GetDataTable(connStrIOSServer, sql)
            If tempDT IsNot Nothing AndAlso tempDT.Rows.Count > 0 Then
                str = tempDT.Rows.OfType(Of DataRow)().[Select](Function(k) k(0).ToString()).ToArray()

                Dim collection As New AutoCompleteStringCollection()
                collection.AddRange(str)
                txt.MaskBox.AutoCompleteCustomSource = collection
                txt.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource
                txt.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "PM Evaluate"

    Public Function GetKPISetList(tech As String) As DataTable
        Dim parray()() As String = {
            New String() {"@IOSTech", Chr(39) & tech & Chr(39)}
        }
        Dim strConnection As String = GetSQL(7000, parray)(0)
        Dim sqlParam As String = GetSQL(7000, parray)(1)
        Return DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Function

    Public Function GetThresholdSetList(tech As String) As DataTable
        Dim parray()() As String = {
            New String() {"@IOSTech", Chr(39) & tech & Chr(39)}
        }
        Dim strConnection As String = GetSQL(7001, parray)(0)
        Dim sqlParam As String = GetSQL(7001, parray)(1)
        Return DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Function

    Public Function StringToStream(input As String) As System.IO.Stream
        Dim memoryStream = New System.IO.MemoryStream()
        Dim streamWriter = New System.IO.StreamWriter(memoryStream)
        streamWriter.Write(input)
        streamWriter.Flush()
        memoryStream.Position = 0
        Return memoryStream
    End Function

    Public Sub SelectComboByMatchingString(ByRef cmb As ComboBoxEdit, ByRef dt As DataTable, colName As String, searchText As String)
        Dim match = dt.AsEnumerable().
        Select(Function(r) r.Field(Of String)(colName)).
        FirstOrDefault(Function(v) v IsNot Nothing AndAlso v.StartsWith(searchText, StringComparison.OrdinalIgnoreCase))

        If match IsNot Nothing Then
            SetComboBox(cmb, ComboSelectBased.TextBased, match)
        End If
    End Sub

#End Region

#Region "SON - Job Run Manual"

    Public Sub UpdateJobRunManualStatus(runManual As Integer, jobid As Integer)
        Dim parray()() As String = {
            New String() {"@RunManual", runManual},
            New String() {"@JobID", jobid}
        }
        Dim connString As String = GetSQL(9322, parray)(0)
        Dim sqlParam As String = GetSQL(9322, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(connString, sqlParam,, iQryTimeOut)
    End Sub

#End Region

End Module