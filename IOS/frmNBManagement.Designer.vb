<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNBManagement
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNBManagement))
        Me.xtcMain = New DevExpress.XtraTab.XtraTabControl()
        Me.tpNBDetect = New DevExpress.XtraTab.XtraTabPage()
        Me.sccDetectCamp = New DevExpress.XtraEditors.SplitContainerControl()
        Me.sccLeft = New DevExpress.XtraEditors.SplitContainerControl()
        Me.grpCampDetect = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.grpCampPropDetect = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.lblLastRunTimeDetect = New DevExpress.XtraEditors.LabelControl()
        Me.lblLastEndTimeDetect = New DevExpress.XtraEditors.LabelControl()
        Me.lblOwnerDetect = New DevExpress.XtraEditors.LabelControl()
        Me.ceActiveDetect = New DevExpress.XtraEditors.CheckEdit()
        Me.deSchNxtStartTimeDetect = New DevExpress.XtraEditors.DateEdit()
        Me.cmbSchRptIntervalDetect = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnRunNowDetect = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl30 = New DevExpress.XtraEditors.LabelControl()
        Me.ceIsPublicDetect = New DevExpress.XtraEditors.CheckEdit()
        Me.gcDetectCampaigns = New DevExpress.XtraGrid.GridControl()
        Me.gvDetectCampaigns = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnDetectRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.txtSearchDetect = New DevExpress.XtraEditors.ButtonEdit()
        Me.btnDeleteDetect = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCloneDetect = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.grpCampSummDetect = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel28 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel33 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl13 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbDetectResultSetID = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnDeleteDetectResultSet = New DevExpress.XtraEditors.SimpleButton()
        Me.xtcCampSummDetect = New DevExpress.XtraTab.XtraTabControl()
        Me.tpCampDetectSumm = New DevExpress.XtraTab.XtraTabPage()
        Me.gcCampSummDetect = New DevExpress.XtraGrid.GridControl()
        Me.gvCampSummDetect = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tpCampDetectData = New DevExpress.XtraTab.XtraTabPage()
        Me.TableLayoutPanel34 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcCampDataDetect = New DevExpress.XtraGrid.GridControl()
        Me.cmsMapNB = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiMapSelectedNB = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvCampDataDetect = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView21 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel35 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnDetectDataLoadGrid = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDetectDataAllCsv = New DevExpress.XtraEditors.SimpleButton()
        Me.lblDetectDataRowCount = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel21 = New System.Windows.Forms.TableLayoutPanel()
        Me.grpConfigSummDetect = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel37 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.grpLayerPropDetect = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel59 = New System.Windows.Forms.TableLayoutPanel()
        Me.layerPropGridDetect = New System.Windows.Forms.PropertyGrid()
        Me.ceApplyConfigAllDetect = New DevExpress.XtraEditors.CheckEdit()
        Me.btnListMngrDetect = New DevExpress.XtraEditors.SimpleButton()
        Me.gcConfigSummDetect = New DevExpress.XtraGrid.GridControl()
        Me.cmsConfigurationSummary = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiAddNewRow = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiCloneSelectedRows = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiDeleteSelectedRows = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvConfigSummDetect = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tpNBCopy = New DevExpress.XtraTab.XtraTabPage()
        Me.sccCopyCamp = New DevExpress.XtraEditors.SplitContainerControl()
        Me.SplitContainerControl2 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.grpCampCopy = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel16 = New System.Windows.Forms.TableLayoutPanel()
        Me.grpCampPropCopy = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel17 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl15 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl16 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl17 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl18 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl19 = New DevExpress.XtraEditors.LabelControl()
        Me.lblLastRunTimeCopy = New DevExpress.XtraEditors.LabelControl()
        Me.lblLastEndTimeCopy = New DevExpress.XtraEditors.LabelControl()
        Me.lblOwnerCopy = New DevExpress.XtraEditors.LabelControl()
        Me.ceActiveCopy = New DevExpress.XtraEditors.CheckEdit()
        Me.deSchNxtStartTimeCopy = New DevExpress.XtraEditors.DateEdit()
        Me.cmbSchRptIntervalCopy = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnRunNowCopy = New DevExpress.XtraEditors.SimpleButton()
        Me.ceIsPublicCopy = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl32 = New DevExpress.XtraEditors.LabelControl()
        Me.gcCopyCampaigns = New DevExpress.XtraGrid.GridControl()
        Me.gvCopyCampaigns = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView9 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel18 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnCopyRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.txtSearchCopy = New DevExpress.XtraEditors.ButtonEdit()
        Me.btnCloneCopy = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDeleteCopy = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel19 = New System.Windows.Forms.TableLayoutPanel()
        Me.grpCampSummCopy = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel29 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbCopyResultSetID = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl20 = New DevExpress.XtraEditors.LabelControl()
        Me.btnDeleteCopyResultSet = New DevExpress.XtraEditors.SimpleButton()
        Me.xtcCampSummCopy = New DevExpress.XtraTab.XtraTabControl()
        Me.tpCampCopySumm = New DevExpress.XtraTab.XtraTabPage()
        Me.gcCampSummCopy = New DevExpress.XtraGrid.GridControl()
        Me.gvCampSummCopy = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView14 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tpCampCopyData = New DevExpress.XtraTab.XtraTabPage()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcCampDataCopy = New DevExpress.XtraGrid.GridControl()
        Me.gvCampDataCopy = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView13 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel36 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnCopyDataLoadGrid = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCopyDataAllCsv = New DevExpress.XtraEditors.SimpleButton()
        Me.lblCopyDataRowCount = New DevExpress.XtraEditors.LabelControl()
        Me.grpConfigSummCopy = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel22 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcConfigSummCopy = New DevExpress.XtraGrid.GridControl()
        Me.gvConfigSummCopy = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView16 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel20 = New System.Windows.Forms.TableLayoutPanel()
        Me.grpLayerPropCopy = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel60 = New System.Windows.Forms.TableLayoutPanel()
        Me.ceApplyConfigAllCopy = New DevExpress.XtraEditors.CheckEdit()
        Me.layerPropGridCopy = New System.Windows.Forms.PropertyGrid()
        Me.btnListMngrCopy = New DevExpress.XtraEditors.SimpleButton()
        Me.tpNBDelete = New DevExpress.XtraTab.XtraTabPage()
        Me.sccDeleteCamp = New DevExpress.XtraEditors.SplitContainerControl()
        Me.SplitContainerControl8 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.grpCampDelete = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel63 = New System.Windows.Forms.TableLayoutPanel()
        Me.grpCampPropDelete = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel64 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl52 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl53 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl54 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl55 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl56 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl57 = New DevExpress.XtraEditors.LabelControl()
        Me.lblLastRunTimeDelete = New DevExpress.XtraEditors.LabelControl()
        Me.lblLastEndTimeDelete = New DevExpress.XtraEditors.LabelControl()
        Me.lblOwnerDelete = New DevExpress.XtraEditors.LabelControl()
        Me.ceActiveDelete = New DevExpress.XtraEditors.CheckEdit()
        Me.deSchNxtStartTimeDelete = New DevExpress.XtraEditors.DateEdit()
        Me.cmbSchRptIntervalDelete = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnRunNowDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ceIsPublicDelete = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl61 = New DevExpress.XtraEditors.LabelControl()
        Me.gcDeleteCampaigns = New DevExpress.XtraGrid.GridControl()
        Me.gvDeleteCampaigns = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView27 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel65 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnDeleteRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.txtSearchDelete = New DevExpress.XtraEditors.ButtonEdit()
        Me.btnCloneDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDeleteDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel66 = New System.Windows.Forms.TableLayoutPanel()
        Me.grpCampSummDelete = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel67 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel68 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbResultSetIDDelete = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl62 = New DevExpress.XtraEditors.LabelControl()
        Me.btnDeleteResultSetDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.XtraTabControl2 = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage3 = New DevExpress.XtraTab.XtraTabPage()
        Me.gcCampSummDelete = New DevExpress.XtraGrid.GridControl()
        Me.gvCampSummDelete = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView31 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.XtraTabPage4 = New DevExpress.XtraTab.XtraTabPage()
        Me.TableLayoutPanel69 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcCampDataDelete = New DevExpress.XtraGrid.GridControl()
        Me.gvCampDataDelete = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView33 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel70 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnDataLoadGridDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDataAllCsvDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.lblDataRowCountDelete = New DevExpress.XtraEditors.LabelControl()
        Me.grpConfigSummDelete = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel71 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcConfigSummDelete = New DevExpress.XtraGrid.GridControl()
        Me.gvConfigSummDelete = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView35 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel72 = New System.Windows.Forms.TableLayoutPanel()
        Me.grpLayerPropDelete = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel73 = New System.Windows.Forms.TableLayoutPanel()
        Me.ceApplyConfigAllDelete = New DevExpress.XtraEditors.CheckEdit()
        Me.layerPropGridDelete = New System.Windows.Forms.PropertyGrid()
        Me.btnListMngrDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.tpNBManual = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.SplitContainerControl4 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.grpCampManual = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.grpCampPropManual = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.lblOwnerManual = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl42 = New DevExpress.XtraEditors.LabelControl()
        Me.ceIsPublicManual = New DevExpress.XtraEditors.CheckEdit()
        Me.gcCampaignManual = New DevExpress.XtraGrid.GridControl()
        Me.gvCampaignManual = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView19 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel25 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtSearchManual = New DevExpress.XtraEditors.ButtonEdit()
        Me.btnDeleteManual = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAddManual = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCloneManual = New DevExpress.XtraEditors.SimpleButton()
        Me.btnRefreshManual = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel26 = New System.Windows.Forms.TableLayoutPanel()
        Me.grpCampSummManual = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel32 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbManualResultSetID = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl21 = New DevExpress.XtraEditors.LabelControl()
        Me.gcCampSummManual = New DevExpress.XtraGrid.GridControl()
        Me.gvCampSummManual = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView20 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grpManual = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel30 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcManual = New DevExpress.XtraGrid.GridControl()
        Me.cmManualPaste = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiTagPastePaste = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_Manual_DeleteRows = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvManual = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView23 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LabelControl33 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl34 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl35 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel31 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblRecordsCountManual = New DevExpress.XtraEditors.LabelControl()
        Me.btnCommitManual = New DevExpress.XtraEditors.SimpleButton()
        Me.tpNBAudit = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitContainerControl5 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel39 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel40 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl22 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl23 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl24 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl25 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl26 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl27 = New DevExpress.XtraEditors.LabelControl()
        Me.lblLastRunTimeNBAudit = New DevExpress.XtraEditors.LabelControl()
        Me.lblLastEndTimeNBAudit = New DevExpress.XtraEditors.LabelControl()
        Me.lblOwnerNBAudit = New DevExpress.XtraEditors.LabelControl()
        Me.chkActiveNBAudit = New DevExpress.XtraEditors.CheckEdit()
        Me.dtpStartTimeNBAudit = New DevExpress.XtraEditors.DateEdit()
        Me.cmbRepeatIntervalNBAudit = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnRunNowNBAudit = New DevExpress.XtraEditors.SimpleButton()
        Me.ceIsPublicAudit = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl43 = New DevExpress.XtraEditors.LabelControl()
        Me.gcCampNBAudit = New DevExpress.XtraGrid.GridControl()
        Me.gvCampNBAudit = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView24 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel41 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnCloneCampNBAudit = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnRefreshCampNBAudit = New DevExpress.XtraEditors.SimpleButton()
        Me.txtNBAuditCampSearch = New DevExpress.XtraEditors.ButtonEdit()
        Me.BtnDeleteCampNBAudit = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnAddCampNBAudit = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel42 = New System.Windows.Forms.TableLayoutPanel()
        Me.grpConfigGen = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel47 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl36 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl37 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl38 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbMMLConfigIDNBAudit = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbTechnologyNBAudit = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbInclusionListNBAudit = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnAddConfigNBAudit = New DevExpress.XtraEditors.SimpleButton()
        Me.grpOptionalSettings = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel50 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl39 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl40 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl41 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbExclusionListNBAudit = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbSLayerNBAudit = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbTLayerNBAudit = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl28 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl29 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbNBType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbMMLScriptID = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.SplitContainerControl3 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.GroupControl6 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel43 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel44 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl31 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbResultSetIdNBAudit = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnDeleteResultSetIdNBAudit = New DevExpress.XtraEditors.SimpleButton()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.gcResultSummaryNBAudit = New DevExpress.XtraGrid.GridControl()
        Me.gvResultSummaryNBAudit = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView26 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.TableLayoutPanel45 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcResultDataNBAudit = New DevExpress.XtraGrid.GridControl()
        Me.gvResultDataNBAudit = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView28 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel46 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnLoadToGridNBAudit = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDataToCSVNBAudit = New DevExpress.XtraEditors.SimpleButton()
        Me.lblRecordCountNBAudit = New DevExpress.XtraEditors.LabelControl()
        Me.grpConfigSummaryNBAudit = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel48 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel49 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl8 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel61 = New System.Windows.Forms.TableLayoutPanel()
        Me.ceApplyConfigAllAudit = New DevExpress.XtraEditors.CheckEdit()
        Me.grdPropertyNBAudit = New System.Windows.Forms.PropertyGrid()
        Me.btnListManagerNBAudit = New DevExpress.XtraEditors.SimpleButton()
        Me.gcConfigNBAudit = New DevExpress.XtraGrid.GridControl()
        Me.gvConfigNBAudit = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView30 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tpNBFetch = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitContainerControl6 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.gcSelectObjects = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel51 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcObjectTree = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel52 = New System.Windows.Forms.TableLayoutPanel()
        Me.tvObjectsTree = New System.Windows.Forms.TreeView()
        Me.TableLayoutPanel53 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtSearchObject = New DevExpress.XtraEditors.ButtonEdit()
        Me.LabelControl45 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel54 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbObjectType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl46 = New DevExpress.XtraEditors.LabelControl()
        Me.lblObjectTreeCount = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel55 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbTechnology = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl47 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel56 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl49 = New DevExpress.XtraEditors.LabelControl()
        Me.btnNBFetch = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel62 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnNBFetchCells = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl48 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl7 = New DevExpress.XtraEditors.GroupControl()
        Me.gcNBFetch = New DevExpress.XtraGrid.GridControl()
        Me.gvNBFetch = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView25 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tpMML = New DevExpress.XtraTab.XtraTabPage()
        Me.sccMML = New DevExpress.XtraEditors.SplitContainerControl()
        Me.sccMmlTop = New DevExpress.XtraEditors.SplitContainerControl()
        Me.grpMmlInput = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel24 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel27 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.lblOwnerMmlInput = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.lblLastEndTimeMml = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel13 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbMMLConfig = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel58 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnPreFilter = New DevExpress.XtraEditors.SimpleButton()
        Me.btnValidate = New DevExpress.XtraEditors.SimpleButton()
        Me.gcMmlCampaign = New DevExpress.XtraGrid.GridControl()
        Me.cmMMLCampaign = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiInsertTempNB = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiRemoveTempNB = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiEditTempObjects = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiRemoveAllTempNB = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvMmlCampaign = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel38 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtSearchMml = New DevExpress.XtraEditors.ButtonEdit()
        Me.btnRefreshMml = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDeleteMml = New DevExpress.XtraEditors.SimpleButton()
        Me.xtcMmlTop = New DevExpress.XtraTab.XtraTabControl()
        Me.tpValidation = New DevExpress.XtraTab.XtraTabPage()
        Me.TableLayoutPanel14 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcValidation = New DevExpress.XtraGrid.GridControl()
        Me.gvValidation = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView8 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel15 = New System.Windows.Forms.TableLayoutPanel()
        Me.tvSelectionMml = New System.Windows.Forms.TreeView()
        Me.grpMmlOutput = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel23 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnGenerate = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel57 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbOutputLocation = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtFileNameSuffix = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl50 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl51 = New DevExpress.XtraEditors.LabelControl()
        Me.seFileSize = New DevExpress.XtraEditors.SpinEdit()
        Me.txtSearchMMLObject = New DevExpress.XtraEditors.ButtonEdit()
        Me.tpData = New DevExpress.XtraTab.XtraTabPage()
        Me.gcData = New DevExpress.XtraGrid.GridControl()
        Me.gvData = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView10 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tpExcluded = New DevExpress.XtraTab.XtraTabPage()
        Me.gcExcluded = New DevExpress.XtraGrid.GridControl()
        Me.gvExcluded = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView11 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grpMmlConfig = New DevExpress.XtraEditors.GroupControl()
        Me.sccMmlBottom = New DevExpress.XtraEditors.SplitContainerControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel11 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.lblOwnerMmlConfig = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl44 = New DevExpress.XtraEditors.LabelControl()
        Me.ceIsPublicMML = New DevExpress.XtraEditors.CheckEdit()
        Me.gcMmlConfig = New DevExpress.XtraGrid.GridControl()
        Me.gvMmlConfig = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel12 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtMmlConfigSearch = New DevExpress.XtraEditors.ButtonEdit()
        Me.btnMmlConfigClone = New DevExpress.XtraEditors.SimpleButton()
        Me.btnMmlConfigDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.xtcMmlBottom = New DevExpress.XtraTab.XtraTabControl()
        Me.tpScriptsNB = New DevExpress.XtraTab.XtraTabPage()
        Me.gcScriptsNB = New DevExpress.XtraGrid.GridControl()
        Me.gvScriptsNB = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView7 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tpScriptsExtcell = New DevExpress.XtraTab.XtraTabPage()
        Me.gcScriptsExtCell = New DevExpress.XtraGrid.GridControl()
        Me.gvScriptsExtCell = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView12 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tpStaticScripts = New DevExpress.XtraTab.XtraTabPage()
        Me.gcStaticScripts = New DevExpress.XtraGrid.GridControl()
        Me.gvStaticScripts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView22 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView15 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView18 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.lblIntegrityMsg = New DevExpress.XtraEditors.LabelControl()
        CType(Me.xtcMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtcMain.SuspendLayout()
        Me.tpNBDetect.SuspendLayout()
        CType(Me.sccDetectCamp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccDetectCamp.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccDetectCamp.Panel1.SuspendLayout()
        CType(Me.sccDetectCamp.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccDetectCamp.Panel2.SuspendLayout()
        Me.sccDetectCamp.SuspendLayout()
        CType(Me.sccLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccLeft.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccLeft.Panel1.SuspendLayout()
        CType(Me.sccLeft.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccLeft.Panel2.SuspendLayout()
        Me.sccLeft.SuspendLayout()
        CType(Me.grpCampDetect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCampDetect.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.grpCampPropDetect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCampPropDetect.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.ceActiveDetect.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deSchNxtStartTimeDetect.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deSchNxtStartTimeDetect.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSchRptIntervalDetect.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceIsPublicDetect.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcDetectCampaigns, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetectCampaigns, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.txtSearchDetect.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel7.SuspendLayout()
        CType(Me.grpCampSummDetect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCampSummDetect.SuspendLayout()
        Me.TableLayoutPanel28.SuspendLayout()
        Me.TableLayoutPanel33.SuspendLayout()
        CType(Me.cmbDetectResultSetID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcCampSummDetect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtcCampSummDetect.SuspendLayout()
        Me.tpCampDetectSumm.SuspendLayout()
        CType(Me.gcCampSummDetect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCampSummDetect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpCampDetectData.SuspendLayout()
        Me.TableLayoutPanel34.SuspendLayout()
        CType(Me.gcCampDataDetect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsMapNB.SuspendLayout()
        CType(Me.gvCampDataDetect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView21, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel35.SuspendLayout()
        Me.TableLayoutPanel21.SuspendLayout()
        CType(Me.grpConfigSummDetect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpConfigSummDetect.SuspendLayout()
        Me.TableLayoutPanel37.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.grpLayerPropDetect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpLayerPropDetect.SuspendLayout()
        Me.TableLayoutPanel59.SuspendLayout()
        CType(Me.ceApplyConfigAllDetect.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcConfigSummDetect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsConfigurationSummary.SuspendLayout()
        CType(Me.gvConfigSummDetect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpNBCopy.SuspendLayout()
        CType(Me.sccCopyCamp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccCopyCamp.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccCopyCamp.Panel1.SuspendLayout()
        CType(Me.sccCopyCamp.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccCopyCamp.Panel2.SuspendLayout()
        Me.sccCopyCamp.SuspendLayout()
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl2.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl2.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl2.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl2.Panel2.SuspendLayout()
        Me.SplitContainerControl2.SuspendLayout()
        CType(Me.grpCampCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCampCopy.SuspendLayout()
        Me.TableLayoutPanel16.SuspendLayout()
        CType(Me.grpCampPropCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCampPropCopy.SuspendLayout()
        Me.TableLayoutPanel17.SuspendLayout()
        CType(Me.ceActiveCopy.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deSchNxtStartTimeCopy.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deSchNxtStartTimeCopy.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSchRptIntervalCopy.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceIsPublicCopy.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcCopyCampaigns, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCopyCampaigns, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel18.SuspendLayout()
        CType(Me.txtSearchCopy.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel19.SuspendLayout()
        CType(Me.grpCampSummCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCampSummCopy.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.TableLayoutPanel29.SuspendLayout()
        CType(Me.cmbCopyResultSetID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcCampSummCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtcCampSummCopy.SuspendLayout()
        Me.tpCampCopySumm.SuspendLayout()
        CType(Me.gcCampSummCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCampSummCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView14, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpCampCopyData.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        CType(Me.gcCampDataCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCampDataCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView13, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel36.SuspendLayout()
        CType(Me.grpConfigSummCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpConfigSummCopy.SuspendLayout()
        Me.TableLayoutPanel22.SuspendLayout()
        CType(Me.gcConfigSummCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvConfigSummCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView16, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel20.SuspendLayout()
        CType(Me.grpLayerPropCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpLayerPropCopy.SuspendLayout()
        Me.TableLayoutPanel60.SuspendLayout()
        CType(Me.ceApplyConfigAllCopy.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpNBDelete.SuspendLayout()
        CType(Me.sccDeleteCamp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccDeleteCamp.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccDeleteCamp.Panel1.SuspendLayout()
        CType(Me.sccDeleteCamp.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccDeleteCamp.Panel2.SuspendLayout()
        Me.sccDeleteCamp.SuspendLayout()
        CType(Me.SplitContainerControl8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl8.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl8.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl8.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl8.Panel2.SuspendLayout()
        Me.SplitContainerControl8.SuspendLayout()
        CType(Me.grpCampDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCampDelete.SuspendLayout()
        Me.TableLayoutPanel63.SuspendLayout()
        CType(Me.grpCampPropDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCampPropDelete.SuspendLayout()
        Me.TableLayoutPanel64.SuspendLayout()
        CType(Me.ceActiveDelete.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deSchNxtStartTimeDelete.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deSchNxtStartTimeDelete.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSchRptIntervalDelete.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceIsPublicDelete.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcDeleteCampaigns, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDeleteCampaigns, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView27, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel65.SuspendLayout()
        CType(Me.txtSearchDelete.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel66.SuspendLayout()
        CType(Me.grpCampSummDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCampSummDelete.SuspendLayout()
        Me.TableLayoutPanel67.SuspendLayout()
        Me.TableLayoutPanel68.SuspendLayout()
        CType(Me.cmbResultSetIDDelete.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XtraTabControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl2.SuspendLayout()
        Me.XtraTabPage3.SuspendLayout()
        CType(Me.gcCampSummDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCampSummDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView31, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage4.SuspendLayout()
        Me.TableLayoutPanel69.SuspendLayout()
        CType(Me.gcCampDataDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCampDataDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView33, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel70.SuspendLayout()
        CType(Me.grpConfigSummDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpConfigSummDelete.SuspendLayout()
        Me.TableLayoutPanel71.SuspendLayout()
        CType(Me.gcConfigSummDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvConfigSummDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView35, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel72.SuspendLayout()
        CType(Me.grpLayerPropDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpLayerPropDelete.SuspendLayout()
        Me.TableLayoutPanel73.SuspendLayout()
        CType(Me.ceApplyConfigAllDelete.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpNBManual.SuspendLayout()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel2.SuspendLayout()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.SplitContainerControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl4.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl4.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl4.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl4.Panel2.SuspendLayout()
        Me.SplitContainerControl4.SuspendLayout()
        CType(Me.grpCampManual, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCampManual.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        CType(Me.grpCampPropManual, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCampPropManual.SuspendLayout()
        Me.TableLayoutPanel9.SuspendLayout()
        CType(Me.ceIsPublicManual.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcCampaignManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCampaignManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView19, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel25.SuspendLayout()
        CType(Me.txtSearchManual.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel26.SuspendLayout()
        CType(Me.grpCampSummManual, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCampSummManual.SuspendLayout()
        Me.TableLayoutPanel32.SuspendLayout()
        CType(Me.cmbManualResultSetID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcCampSummManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCampSummManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpManual, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpManual.SuspendLayout()
        Me.TableLayoutPanel30.SuspendLayout()
        CType(Me.gcManual, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmManualPaste.SuspendLayout()
        CType(Me.gvManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView23, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel31.SuspendLayout()
        Me.tpNBAudit.SuspendLayout()
        CType(Me.SplitContainerControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl5.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl5.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl5.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl5.Panel2.SuspendLayout()
        Me.SplitContainerControl5.SuspendLayout()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        Me.TableLayoutPanel39.SuspendLayout()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl5.SuspendLayout()
        Me.TableLayoutPanel40.SuspendLayout()
        CType(Me.chkActiveNBAudit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpStartTimeNBAudit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpStartTimeNBAudit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbRepeatIntervalNBAudit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceIsPublicAudit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcCampNBAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCampNBAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView24, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel41.SuspendLayout()
        CType(Me.txtNBAuditCampSearch.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel42.SuspendLayout()
        CType(Me.grpConfigGen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpConfigGen.SuspendLayout()
        Me.TableLayoutPanel47.SuspendLayout()
        CType(Me.cmbMMLConfigIDNBAudit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTechnologyNBAudit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbInclusionListNBAudit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpOptionalSettings, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpOptionalSettings.SuspendLayout()
        Me.TableLayoutPanel50.SuspendLayout()
        CType(Me.cmbExclusionListNBAudit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSLayerNBAudit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTLayerNBAudit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbNBType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbMMLScriptID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl3.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl3.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl3.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl3.Panel2.SuspendLayout()
        Me.SplitContainerControl3.SuspendLayout()
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl6.SuspendLayout()
        Me.TableLayoutPanel43.SuspendLayout()
        Me.TableLayoutPanel44.SuspendLayout()
        CType(Me.cmbResultSetIdNBAudit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        CType(Me.gcResultSummaryNBAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvResultSummaryNBAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView26, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage2.SuspendLayout()
        Me.TableLayoutPanel45.SuspendLayout()
        CType(Me.gcResultDataNBAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvResultDataNBAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView28, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel46.SuspendLayout()
        CType(Me.grpConfigSummaryNBAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpConfigSummaryNBAudit.SuspendLayout()
        Me.TableLayoutPanel48.SuspendLayout()
        Me.TableLayoutPanel49.SuspendLayout()
        CType(Me.GroupControl8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl8.SuspendLayout()
        Me.TableLayoutPanel61.SuspendLayout()
        CType(Me.ceApplyConfigAllAudit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcConfigNBAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvConfigNBAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView30, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpNBFetch.SuspendLayout()
        CType(Me.SplitContainerControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl6.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl6.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl6.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl6.Panel2.SuspendLayout()
        Me.SplitContainerControl6.SuspendLayout()
        CType(Me.gcSelectObjects, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcSelectObjects.SuspendLayout()
        Me.TableLayoutPanel51.SuspendLayout()
        CType(Me.gcObjectTree, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcObjectTree.SuspendLayout()
        Me.TableLayoutPanel52.SuspendLayout()
        Me.TableLayoutPanel53.SuspendLayout()
        CType(Me.txtSearchObject.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel54.SuspendLayout()
        CType(Me.cmbObjectType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel55.SuspendLayout()
        CType(Me.cmbTechnology.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel56.SuspendLayout()
        Me.TableLayoutPanel62.SuspendLayout()
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl7.SuspendLayout()
        CType(Me.gcNBFetch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvNBFetch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView25, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpMML.SuspendLayout()
        CType(Me.sccMML, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccMML.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccMML.Panel1.SuspendLayout()
        CType(Me.sccMML.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccMML.Panel2.SuspendLayout()
        Me.sccMML.SuspendLayout()
        CType(Me.sccMmlTop, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccMmlTop.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccMmlTop.Panel1.SuspendLayout()
        CType(Me.sccMmlTop.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccMmlTop.Panel2.SuspendLayout()
        Me.sccMmlTop.SuspendLayout()
        CType(Me.grpMmlInput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMmlInput.SuspendLayout()
        Me.TableLayoutPanel24.SuspendLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        Me.TableLayoutPanel27.SuspendLayout()
        Me.TableLayoutPanel13.SuspendLayout()
        CType(Me.cmbMMLConfig.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel58.SuspendLayout()
        CType(Me.gcMmlCampaign, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmMMLCampaign.SuspendLayout()
        CType(Me.gvMmlCampaign, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel38.SuspendLayout()
        CType(Me.txtSearchMml.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcMmlTop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtcMmlTop.SuspendLayout()
        Me.tpValidation.SuspendLayout()
        Me.TableLayoutPanel14.SuspendLayout()
        CType(Me.gcValidation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvValidation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel15.SuspendLayout()
        CType(Me.grpMmlOutput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMmlOutput.SuspendLayout()
        Me.TableLayoutPanel23.SuspendLayout()
        Me.TableLayoutPanel57.SuspendLayout()
        CType(Me.cmbOutputLocation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFileNameSuffix.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.seFileSize.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSearchMMLObject.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpData.SuspendLayout()
        CType(Me.gcData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpExcluded.SuspendLayout()
        CType(Me.gcExcluded, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvExcluded, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpMmlConfig, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMmlConfig.SuspendLayout()
        CType(Me.sccMmlBottom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccMmlBottom.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccMmlBottom.Panel1.SuspendLayout()
        CType(Me.sccMmlBottom.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccMmlBottom.Panel2.SuspendLayout()
        Me.sccMmlBottom.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.TableLayoutPanel10.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.TableLayoutPanel11.SuspendLayout()
        CType(Me.ceIsPublicMML.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcMmlConfig, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvMmlConfig, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel12.SuspendLayout()
        CType(Me.txtMmlConfigSearch.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcMmlBottom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtcMmlBottom.SuspendLayout()
        Me.tpScriptsNB.SuspendLayout()
        CType(Me.gcScriptsNB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvScriptsNB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpScriptsExtcell.SuspendLayout()
        CType(Me.gcScriptsExtCell, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvScriptsExtCell, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView12, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpStaticScripts.SuspendLayout()
        CType(Me.gcStaticScripts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvStaticScripts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView18, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'xtcMain
        '
        Me.xtcMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtcMain.Location = New System.Drawing.Point(3, 33)
        Me.xtcMain.Name = "xtcMain"
        Me.xtcMain.SelectedTabPage = Me.tpNBDetect
        Me.xtcMain.Size = New System.Drawing.Size(1222, 713)
        Me.xtcMain.TabIndex = 0
        Me.xtcMain.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tpNBDetect, Me.tpNBCopy, Me.tpNBDelete, Me.tpNBManual, Me.tpNBAudit, Me.tpNBFetch, Me.tpMML})
        '
        'tpNBDetect
        '
        Me.tpNBDetect.Controls.Add(Me.sccDetectCamp)
        Me.tpNBDetect.Name = "tpNBDetect"
        Me.tpNBDetect.Size = New System.Drawing.Size(1220, 688)
        Me.tpNBDetect.Text = "NB Detect"
        '
        'sccDetectCamp
        '
        Me.sccDetectCamp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccDetectCamp.Horizontal = False
        Me.sccDetectCamp.Location = New System.Drawing.Point(0, 0)
        Me.sccDetectCamp.Name = "sccDetectCamp"
        '
        'sccDetectCamp.Panel1
        '
        Me.sccDetectCamp.Panel1.Controls.Add(Me.sccLeft)
        Me.sccDetectCamp.Panel1.MinSize = 400
        Me.sccDetectCamp.Panel1.Text = "Panel1"
        '
        'sccDetectCamp.Panel2
        '
        Me.sccDetectCamp.Panel2.Controls.Add(Me.TableLayoutPanel21)
        Me.sccDetectCamp.Panel2.MinSize = 300
        Me.sccDetectCamp.Panel2.Text = "Panel2"
        Me.sccDetectCamp.Size = New System.Drawing.Size(1220, 688)
        Me.sccDetectCamp.SplitterPosition = 480
        Me.sccDetectCamp.TabIndex = 0
        Me.sccDetectCamp.Text = "SplitContainerControl1"
        '
        'sccLeft
        '
        Me.sccLeft.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccLeft.Location = New System.Drawing.Point(0, 0)
        Me.sccLeft.Name = "sccLeft"
        '
        'sccLeft.Panel1
        '
        Me.sccLeft.Panel1.Controls.Add(Me.grpCampDetect)
        Me.sccLeft.Panel1.MinSize = 300
        Me.sccLeft.Panel1.Text = "Panel1"
        '
        'sccLeft.Panel2
        '
        Me.sccLeft.Panel2.Controls.Add(Me.TableLayoutPanel7)
        Me.sccLeft.Panel2.MinSize = 500
        Me.sccLeft.Panel2.Text = "Panel2"
        Me.sccLeft.Size = New System.Drawing.Size(1220, 400)
        Me.sccLeft.SplitterPosition = 410
        Me.sccLeft.TabIndex = 0
        Me.sccLeft.Text = "SplitContainerControl1"
        '
        'grpCampDetect
        '
        Me.grpCampDetect.Controls.Add(Me.TableLayoutPanel1)
        Me.grpCampDetect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpCampDetect.Location = New System.Drawing.Point(0, 0)
        Me.grpCampDetect.Name = "grpCampDetect"
        Me.grpCampDetect.Size = New System.Drawing.Size(410, 400)
        Me.grpCampDetect.TabIndex = 0
        Me.grpCampDetect.Text = "NB | Detect Campaigns"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.grpCampPropDetect, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.gcDetectCampaigns, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 219.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(406, 375)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'grpCampPropDetect
        '
        Me.grpCampPropDetect.Controls.Add(Me.TableLayoutPanel3)
        Me.grpCampPropDetect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpCampPropDetect.Location = New System.Drawing.Point(2, 158)
        Me.grpCampPropDetect.Margin = New System.Windows.Forms.Padding(2)
        Me.grpCampPropDetect.Name = "grpCampPropDetect"
        Me.grpCampPropDetect.Size = New System.Drawing.Size(402, 215)
        Me.grpCampPropDetect.TabIndex = 4
        Me.grpCampPropDetect.Text = "Campaign Properties"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 3
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl1, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl2, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl3, 0, 3)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl4, 0, 4)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl5, 0, 5)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl6, 0, 6)
        Me.TableLayoutPanel3.Controls.Add(Me.lblLastRunTimeDetect, 1, 5)
        Me.TableLayoutPanel3.Controls.Add(Me.lblLastEndTimeDetect, 1, 6)
        Me.TableLayoutPanel3.Controls.Add(Me.lblOwnerDetect, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.ceActiveDetect, 1, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.deSchNxtStartTimeDetect, 1, 3)
        Me.TableLayoutPanel3.Controls.Add(Me.cmbSchRptIntervalDetect, 1, 4)
        Me.TableLayoutPanel3.Controls.Add(Me.btnRunNowDetect, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl30, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.ceIsPublicDetect, 1, 2)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 8
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(398, 190)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(129, 24)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Owner"
        '
        'LabelControl2
        '
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl2.Location = New System.Drawing.Point(3, 33)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl2.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Active"
        '
        'LabelControl3
        '
        Me.LabelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl3.Location = New System.Drawing.Point(3, 85)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl3.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl3.TabIndex = 2
        Me.LabelControl3.Text = "Schedule Next Start Time"
        '
        'LabelControl4
        '
        Me.LabelControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl4.Location = New System.Drawing.Point(3, 111)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl4.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl4.TabIndex = 3
        Me.LabelControl4.Text = "Schedule Repeat Interval"
        '
        'LabelControl5
        '
        Me.LabelControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl5.Location = New System.Drawing.Point(3, 137)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl5.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl5.TabIndex = 4
        Me.LabelControl5.Text = "Last Run TIme"
        '
        'LabelControl6
        '
        Me.LabelControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl6.Location = New System.Drawing.Point(3, 163)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl6.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl6.TabIndex = 5
        Me.LabelControl6.Text = "Last End Time"
        '
        'lblLastRunTimeDetect
        '
        Me.TableLayoutPanel3.SetColumnSpan(Me.lblLastRunTimeDetect, 2)
        Me.lblLastRunTimeDetect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLastRunTimeDetect.Location = New System.Drawing.Point(138, 137)
        Me.lblLastRunTimeDetect.Name = "lblLastRunTimeDetect"
        Me.lblLastRunTimeDetect.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblLastRunTimeDetect.Size = New System.Drawing.Size(257, 20)
        Me.lblLastRunTimeDetect.TabIndex = 6
        '
        'lblLastEndTimeDetect
        '
        Me.TableLayoutPanel3.SetColumnSpan(Me.lblLastEndTimeDetect, 2)
        Me.lblLastEndTimeDetect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLastEndTimeDetect.Location = New System.Drawing.Point(138, 163)
        Me.lblLastEndTimeDetect.Name = "lblLastEndTimeDetect"
        Me.lblLastEndTimeDetect.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblLastEndTimeDetect.Size = New System.Drawing.Size(257, 20)
        Me.lblLastEndTimeDetect.TabIndex = 7
        '
        'lblOwnerDetect
        '
        Me.lblOwnerDetect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblOwnerDetect.Location = New System.Drawing.Point(138, 3)
        Me.lblOwnerDetect.Name = "lblOwnerDetect"
        Me.lblOwnerDetect.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblOwnerDetect.Size = New System.Drawing.Size(187, 24)
        Me.lblOwnerDetect.TabIndex = 9
        '
        'ceActiveDetect
        '
        Me.ceActiveDetect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceActiveDetect.Location = New System.Drawing.Point(140, 33)
        Me.ceActiveDetect.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceActiveDetect.Name = "ceActiveDetect"
        Me.ceActiveDetect.Properties.Caption = ""
        Me.ceActiveDetect.Size = New System.Drawing.Size(185, 20)
        Me.ceActiveDetect.TabIndex = 10
        Me.ceActiveDetect.Tag = "NB_Detect"
        '
        'deSchNxtStartTimeDetect
        '
        Me.deSchNxtStartTimeDetect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.deSchNxtStartTimeDetect.EditValue = New Date(2016, 9, 14, 12, 41, 50, 900)
        Me.deSchNxtStartTimeDetect.Enabled = False
        Me.deSchNxtStartTimeDetect.Location = New System.Drawing.Point(138, 85)
        Me.deSchNxtStartTimeDetect.Name = "deSchNxtStartTimeDetect"
        Me.deSchNxtStartTimeDetect.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deSchNxtStartTimeDetect.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deSchNxtStartTimeDetect.Size = New System.Drawing.Size(187, 20)
        Me.deSchNxtStartTimeDetect.TabIndex = 11
        Me.deSchNxtStartTimeDetect.Tag = "NB_Detect"
        '
        'cmbSchRptIntervalDetect
        '
        Me.cmbSchRptIntervalDetect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbSchRptIntervalDetect.EditValue = "WEEKLY"
        Me.cmbSchRptIntervalDetect.Enabled = False
        Me.cmbSchRptIntervalDetect.Location = New System.Drawing.Point(138, 111)
        Me.cmbSchRptIntervalDetect.Name = "cmbSchRptIntervalDetect"
        Me.cmbSchRptIntervalDetect.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbSchRptIntervalDetect.Properties.Items.AddRange(New Object() {"DAILY", "WEEKLY"})
        Me.cmbSchRptIntervalDetect.Size = New System.Drawing.Size(187, 20)
        Me.cmbSchRptIntervalDetect.TabIndex = 12
        Me.cmbSchRptIntervalDetect.Tag = "NB_Detect"
        '
        'btnRunNowDetect
        '
        Me.btnRunNowDetect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRunNowDetect.Location = New System.Drawing.Point(331, 3)
        Me.btnRunNowDetect.Name = "btnRunNowDetect"
        Me.btnRunNowDetect.Size = New System.Drawing.Size(64, 24)
        Me.btnRunNowDetect.TabIndex = 8
        Me.btnRunNowDetect.Text = "Run Now"
        '
        'LabelControl30
        '
        Me.LabelControl30.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl30.Location = New System.Drawing.Point(3, 59)
        Me.LabelControl30.Name = "LabelControl30"
        Me.LabelControl30.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl30.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl30.TabIndex = 13
        Me.LabelControl30.Text = "Is Public"
        '
        'ceIsPublicDetect
        '
        Me.ceIsPublicDetect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceIsPublicDetect.Location = New System.Drawing.Point(140, 59)
        Me.ceIsPublicDetect.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceIsPublicDetect.Name = "ceIsPublicDetect"
        Me.ceIsPublicDetect.Properties.Caption = ""
        Me.ceIsPublicDetect.Size = New System.Drawing.Size(185, 20)
        Me.ceIsPublicDetect.TabIndex = 14
        Me.ceIsPublicDetect.Tag = "NB_Detect"
        '
        'gcDetectCampaigns
        '
        Me.gcDetectCampaigns.AllowDrop = True
        Me.gcDetectCampaigns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcDetectCampaigns.Location = New System.Drawing.Point(2, 29)
        Me.gcDetectCampaigns.MainView = Me.gvDetectCampaigns
        Me.gcDetectCampaigns.Margin = New System.Windows.Forms.Padding(2)
        Me.gcDetectCampaigns.Name = "gcDetectCampaigns"
        Me.gcDetectCampaigns.Size = New System.Drawing.Size(402, 125)
        Me.gcDetectCampaigns.TabIndex = 5
        Me.gcDetectCampaigns.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvDetectCampaigns, Me.GridView2})
        '
        'gvDetectCampaigns
        '
        Me.gvDetectCampaigns.ActiveFilterEnabled = False
        Me.gvDetectCampaigns.GridControl = Me.gcDetectCampaigns
        Me.gvDetectCampaigns.Name = "gvDetectCampaigns"
        Me.gvDetectCampaigns.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvDetectCampaigns.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvDetectCampaigns.OptionsBehavior.Editable = False
        Me.gvDetectCampaigns.OptionsBehavior.ReadOnly = True
        Me.gvDetectCampaigns.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvDetectCampaigns.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvDetectCampaigns.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvDetectCampaigns.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvDetectCampaigns.OptionsSelection.MultiSelect = True
        Me.gvDetectCampaigns.OptionsView.ShowGroupPanel = False
        '
        'GridView2
        '
        Me.GridView2.GridControl = Me.gcDetectCampaigns
        Me.GridView2.Name = "GridView2"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 4
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.btnDetectRefresh, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.txtSearchDetect, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnDeleteDetect, 3, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnCloneDetect, 2, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(1, 1)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(404, 25)
        Me.TableLayoutPanel2.TabIndex = 6
        '
        'btnDetectRefresh
        '
        Me.btnDetectRefresh.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDetectRefresh.Location = New System.Drawing.Point(241, 2)
        Me.btnDetectRefresh.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDetectRefresh.Name = "btnDetectRefresh"
        Me.btnDetectRefresh.Size = New System.Drawing.Size(51, 21)
        Me.btnDetectRefresh.TabIndex = 7
        Me.btnDetectRefresh.Tag = "NB_Detect"
        Me.btnDetectRefresh.Text = "Refresh"
        '
        'txtSearchDetect
        '
        Me.txtSearchDetect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchDetect.Location = New System.Drawing.Point(2, 2)
        Me.txtSearchDetect.Margin = New System.Windows.Forms.Padding(2)
        Me.txtSearchDetect.Name = "txtSearchDetect"
        Me.txtSearchDetect.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSearchDetect.Properties.NullValuePrompt = "Search..."
        Me.txtSearchDetect.Size = New System.Drawing.Size(235, 20)
        Me.txtSearchDetect.TabIndex = 3
        '
        'btnDeleteDetect
        '
        Me.btnDeleteDetect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDeleteDetect.Location = New System.Drawing.Point(351, 2)
        Me.btnDeleteDetect.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDeleteDetect.Name = "btnDeleteDetect"
        Me.btnDeleteDetect.Size = New System.Drawing.Size(51, 21)
        Me.btnDeleteDetect.TabIndex = 6
        Me.btnDeleteDetect.Text = "Delete"
        '
        'btnCloneDetect
        '
        Me.btnCloneDetect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCloneDetect.Location = New System.Drawing.Point(296, 2)
        Me.btnCloneDetect.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCloneDetect.Name = "btnCloneDetect"
        Me.btnCloneDetect.Size = New System.Drawing.Size(51, 21)
        Me.btnCloneDetect.TabIndex = 5
        Me.btnCloneDetect.Text = "Clone"
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 1
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Controls.Add(Me.grpCampSummDetect, 0, 0)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 2
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(800, 400)
        Me.TableLayoutPanel7.TabIndex = 0
        '
        'grpCampSummDetect
        '
        Me.grpCampSummDetect.Controls.Add(Me.TableLayoutPanel28)
        Me.grpCampSummDetect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpCampSummDetect.Location = New System.Drawing.Point(3, 3)
        Me.grpCampSummDetect.Name = "grpCampSummDetect"
        Me.grpCampSummDetect.Size = New System.Drawing.Size(794, 391)
        Me.grpCampSummDetect.TabIndex = 1
        Me.grpCampSummDetect.Text = "Campaign Result"
        '
        'TableLayoutPanel28
        '
        Me.TableLayoutPanel28.ColumnCount = 1
        Me.TableLayoutPanel28.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel28.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel28.Controls.Add(Me.TableLayoutPanel33, 0, 0)
        Me.TableLayoutPanel28.Controls.Add(Me.xtcCampSummDetect, 0, 1)
        Me.TableLayoutPanel28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel28.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel28.Name = "TableLayoutPanel28"
        Me.TableLayoutPanel28.RowCount = 2
        Me.TableLayoutPanel28.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel28.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel28.Size = New System.Drawing.Size(790, 366)
        Me.TableLayoutPanel28.TabIndex = 0
        '
        'TableLayoutPanel33
        '
        Me.TableLayoutPanel33.ColumnCount = 3
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220.0!))
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel33.Controls.Add(Me.LabelControl13, 0, 0)
        Me.TableLayoutPanel33.Controls.Add(Me.cmbDetectResultSetID, 1, 0)
        Me.TableLayoutPanel33.Controls.Add(Me.btnDeleteDetectResultSet, 2, 0)
        Me.TableLayoutPanel33.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel33.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel33.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel33.Name = "TableLayoutPanel33"
        Me.TableLayoutPanel33.RowCount = 1
        Me.TableLayoutPanel33.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel33.Size = New System.Drawing.Size(786, 26)
        Me.TableLayoutPanel33.TabIndex = 3
        '
        'LabelControl13
        '
        Me.LabelControl13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl13.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl13.Name = "LabelControl13"
        Me.LabelControl13.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl13.Size = New System.Drawing.Size(74, 20)
        Me.LabelControl13.TabIndex = 3
        Me.LabelControl13.Text = "Result Set ID"
        '
        'cmbDetectResultSetID
        '
        Me.cmbDetectResultSetID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbDetectResultSetID.EditValue = ""
        Me.cmbDetectResultSetID.Location = New System.Drawing.Point(83, 3)
        Me.cmbDetectResultSetID.Name = "cmbDetectResultSetID"
        Me.cmbDetectResultSetID.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbDetectResultSetID.Size = New System.Drawing.Size(214, 20)
        Me.cmbDetectResultSetID.TabIndex = 13
        '
        'btnDeleteDetectResultSet
        '
        Me.btnDeleteDetectResultSet.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDeleteDetectResultSet.Location = New System.Drawing.Point(302, 2)
        Me.btnDeleteDetectResultSet.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDeleteDetectResultSet.Name = "btnDeleteDetectResultSet"
        Me.btnDeleteDetectResultSet.Size = New System.Drawing.Size(62, 22)
        Me.btnDeleteDetectResultSet.TabIndex = 14
        Me.btnDeleteDetectResultSet.Tag = "NB_Detect"
        Me.btnDeleteDetectResultSet.Text = "Delete"
        '
        'xtcCampSummDetect
        '
        Me.xtcCampSummDetect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtcCampSummDetect.Location = New System.Drawing.Point(3, 33)
        Me.xtcCampSummDetect.Name = "xtcCampSummDetect"
        Me.xtcCampSummDetect.SelectedTabPage = Me.tpCampDetectSumm
        Me.xtcCampSummDetect.Size = New System.Drawing.Size(784, 330)
        Me.xtcCampSummDetect.TabIndex = 4
        Me.xtcCampSummDetect.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tpCampDetectSumm, Me.tpCampDetectData})
        '
        'tpCampDetectSumm
        '
        Me.tpCampDetectSumm.Controls.Add(Me.gcCampSummDetect)
        Me.tpCampDetectSumm.Name = "tpCampDetectSumm"
        Me.tpCampDetectSumm.Size = New System.Drawing.Size(782, 305)
        Me.tpCampDetectSumm.Text = "Summary"
        '
        'gcCampSummDetect
        '
        Me.gcCampSummDetect.AllowDrop = True
        Me.gcCampSummDetect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcCampSummDetect.Location = New System.Drawing.Point(0, 0)
        Me.gcCampSummDetect.MainView = Me.gvCampSummDetect
        Me.gcCampSummDetect.Name = "gcCampSummDetect"
        Me.gcCampSummDetect.Size = New System.Drawing.Size(782, 305)
        Me.gcCampSummDetect.TabIndex = 2
        Me.gcCampSummDetect.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvCampSummDetect, Me.GridView1})
        '
        'gvCampSummDetect
        '
        Me.gvCampSummDetect.ActiveFilterEnabled = False
        Me.gvCampSummDetect.GridControl = Me.gcCampSummDetect
        Me.gvCampSummDetect.Name = "gvCampSummDetect"
        Me.gvCampSummDetect.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampSummDetect.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampSummDetect.OptionsBehavior.Editable = False
        Me.gvCampSummDetect.OptionsBehavior.ReadOnly = True
        Me.gvCampSummDetect.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampSummDetect.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampSummDetect.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampSummDetect.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvCampSummDetect.OptionsSelection.MultiSelect = True
        Me.gvCampSummDetect.OptionsView.ShowGroupPanel = False
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.gcCampSummDetect
        Me.GridView1.Name = "GridView1"
        '
        'tpCampDetectData
        '
        Me.tpCampDetectData.Controls.Add(Me.TableLayoutPanel34)
        Me.tpCampDetectData.Name = "tpCampDetectData"
        Me.tpCampDetectData.Size = New System.Drawing.Size(782, 305)
        Me.tpCampDetectData.Text = "Data"
        '
        'TableLayoutPanel34
        '
        Me.TableLayoutPanel34.ColumnCount = 1
        Me.TableLayoutPanel34.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel34.Controls.Add(Me.gcCampDataDetect, 0, 1)
        Me.TableLayoutPanel34.Controls.Add(Me.TableLayoutPanel35, 0, 0)
        Me.TableLayoutPanel34.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel34.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel34.Name = "TableLayoutPanel34"
        Me.TableLayoutPanel34.RowCount = 2
        Me.TableLayoutPanel34.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel34.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel34.Size = New System.Drawing.Size(782, 305)
        Me.TableLayoutPanel34.TabIndex = 0
        '
        'gcCampDataDetect
        '
        Me.gcCampDataDetect.AllowDrop = True
        Me.gcCampDataDetect.ContextMenuStrip = Me.cmsMapNB
        Me.gcCampDataDetect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcCampDataDetect.Location = New System.Drawing.Point(3, 38)
        Me.gcCampDataDetect.MainView = Me.gvCampDataDetect
        Me.gcCampDataDetect.Name = "gcCampDataDetect"
        Me.gcCampDataDetect.Size = New System.Drawing.Size(776, 264)
        Me.gcCampDataDetect.TabIndex = 4
        Me.gcCampDataDetect.Tag = "NBDetect"
        Me.gcCampDataDetect.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvCampDataDetect, Me.GridView21})
        '
        'cmsMapNB
        '
        Me.cmsMapNB.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.cmsMapNB.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiMapSelectedNB})
        Me.cmsMapNB.Name = "cm_TagManagement"
        Me.cmsMapNB.Size = New System.Drawing.Size(165, 26)
        '
        'tsmiMapSelectedNB
        '
        Me.tsmiMapSelectedNB.Name = "tsmiMapSelectedNB"
        Me.tsmiMapSelectedNB.Size = New System.Drawing.Size(164, 22)
        Me.tsmiMapSelectedNB.Text = "Map Selected NB"
        '
        'gvCampDataDetect
        '
        Me.gvCampDataDetect.ActiveFilterEnabled = False
        Me.gvCampDataDetect.GridControl = Me.gcCampDataDetect
        Me.gvCampDataDetect.Name = "gvCampDataDetect"
        Me.gvCampDataDetect.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampDataDetect.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampDataDetect.OptionsBehavior.Editable = False
        Me.gvCampDataDetect.OptionsBehavior.ReadOnly = True
        Me.gvCampDataDetect.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampDataDetect.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampDataDetect.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampDataDetect.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.gvCampDataDetect.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvCampDataDetect.OptionsSelection.MultiSelect = True
        Me.gvCampDataDetect.OptionsView.ShowGroupPanel = False
        '
        'GridView21
        '
        Me.GridView21.GridControl = Me.gcCampDataDetect
        Me.GridView21.Name = "GridView21"
        '
        'TableLayoutPanel35
        '
        Me.TableLayoutPanel35.ColumnCount = 3
        Me.TableLayoutPanel35.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel35.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel35.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel35.Controls.Add(Me.btnDetectDataLoadGrid, 0, 0)
        Me.TableLayoutPanel35.Controls.Add(Me.btnDetectDataAllCsv, 1, 0)
        Me.TableLayoutPanel35.Controls.Add(Me.lblDetectDataRowCount, 2, 0)
        Me.TableLayoutPanel35.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel35.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel35.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel35.Name = "TableLayoutPanel35"
        Me.TableLayoutPanel35.RowCount = 1
        Me.TableLayoutPanel35.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel35.Size = New System.Drawing.Size(778, 31)
        Me.TableLayoutPanel35.TabIndex = 0
        '
        'btnDetectDataLoadGrid
        '
        Me.btnDetectDataLoadGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDetectDataLoadGrid.Location = New System.Drawing.Point(3, 3)
        Me.btnDetectDataLoadGrid.Name = "btnDetectDataLoadGrid"
        Me.btnDetectDataLoadGrid.Size = New System.Drawing.Size(94, 25)
        Me.btnDetectDataLoadGrid.TabIndex = 0
        Me.btnDetectDataLoadGrid.Text = "Load To Grid"
        '
        'btnDetectDataAllCsv
        '
        Me.btnDetectDataAllCsv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDetectDataAllCsv.Location = New System.Drawing.Point(103, 3)
        Me.btnDetectDataAllCsv.Name = "btnDetectDataAllCsv"
        Me.btnDetectDataAllCsv.Size = New System.Drawing.Size(94, 25)
        Me.btnDetectDataAllCsv.TabIndex = 1
        Me.btnDetectDataAllCsv.Text = "All Data To CSV"
        '
        'lblDetectDataRowCount
        '
        Me.lblDetectDataRowCount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDetectDataRowCount.Location = New System.Drawing.Point(203, 3)
        Me.lblDetectDataRowCount.Name = "lblDetectDataRowCount"
        Me.lblDetectDataRowCount.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblDetectDataRowCount.Size = New System.Drawing.Size(572, 25)
        Me.lblDetectDataRowCount.TabIndex = 2
        Me.lblDetectDataRowCount.Text = "Count of Records: "
        Me.lblDetectDataRowCount.Visible = False
        '
        'TableLayoutPanel21
        '
        Me.TableLayoutPanel21.ColumnCount = 1
        Me.TableLayoutPanel21.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel21.Controls.Add(Me.grpConfigSummDetect, 0, 0)
        Me.TableLayoutPanel21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel21.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel21.Name = "TableLayoutPanel21"
        Me.TableLayoutPanel21.RowCount = 1
        Me.TableLayoutPanel21.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel21.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 278.0!))
        Me.TableLayoutPanel21.Size = New System.Drawing.Size(1220, 278)
        Me.TableLayoutPanel21.TabIndex = 1
        '
        'grpConfigSummDetect
        '
        Me.grpConfigSummDetect.Controls.Add(Me.TableLayoutPanel37)
        Me.grpConfigSummDetect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpConfigSummDetect.Location = New System.Drawing.Point(3, 3)
        Me.grpConfigSummDetect.Name = "grpConfigSummDetect"
        Me.grpConfigSummDetect.Size = New System.Drawing.Size(1214, 272)
        Me.grpConfigSummDetect.TabIndex = 0
        Me.grpConfigSummDetect.Text = "Configuration Summary"
        '
        'TableLayoutPanel37
        '
        Me.TableLayoutPanel37.ColumnCount = 2
        Me.TableLayoutPanel37.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel37.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350.0!))
        Me.TableLayoutPanel37.Controls.Add(Me.TableLayoutPanel4, 1, 0)
        Me.TableLayoutPanel37.Controls.Add(Me.gcConfigSummDetect, 0, 0)
        Me.TableLayoutPanel37.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel37.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel37.Name = "TableLayoutPanel37"
        Me.TableLayoutPanel37.RowCount = 1
        Me.TableLayoutPanel37.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel37.Size = New System.Drawing.Size(1210, 247)
        Me.TableLayoutPanel37.TabIndex = 4
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 1
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.grpLayerPropDetect, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.btnListMngrDetect, 0, 1)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(863, 3)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(344, 241)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'grpLayerPropDetect
        '
        Me.grpLayerPropDetect.Controls.Add(Me.TableLayoutPanel59)
        Me.grpLayerPropDetect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpLayerPropDetect.Location = New System.Drawing.Point(3, 3)
        Me.grpLayerPropDetect.Name = "grpLayerPropDetect"
        Me.grpLayerPropDetect.Size = New System.Drawing.Size(338, 203)
        Me.grpLayerPropDetect.TabIndex = 1
        Me.grpLayerPropDetect.Text = "Layer Properties"
        '
        'TableLayoutPanel59
        '
        Me.TableLayoutPanel59.ColumnCount = 1
        Me.TableLayoutPanel59.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel59.Controls.Add(Me.layerPropGridDetect, 0, 0)
        Me.TableLayoutPanel59.Controls.Add(Me.ceApplyConfigAllDetect, 0, 1)
        Me.TableLayoutPanel59.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel59.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel59.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel59.Name = "TableLayoutPanel59"
        Me.TableLayoutPanel59.RowCount = 2
        Me.TableLayoutPanel59.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel59.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel59.Size = New System.Drawing.Size(334, 178)
        Me.TableLayoutPanel59.TabIndex = 1
        '
        'layerPropGridDetect
        '
        Me.layerPropGridDetect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layerPropGridDetect.LineColor = System.Drawing.SystemColors.ControlDark
        Me.layerPropGridDetect.Location = New System.Drawing.Point(3, 3)
        Me.layerPropGridDetect.Name = "layerPropGridDetect"
        Me.layerPropGridDetect.Size = New System.Drawing.Size(328, 147)
        Me.layerPropGridDetect.TabIndex = 0
        Me.layerPropGridDetect.Tag = "NB_Detect"
        Me.layerPropGridDetect.ToolbarVisible = False
        '
        'ceApplyConfigAllDetect
        '
        Me.ceApplyConfigAllDetect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceApplyConfigAllDetect.Location = New System.Drawing.Point(5, 156)
        Me.ceApplyConfigAllDetect.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceApplyConfigAllDetect.Name = "ceApplyConfigAllDetect"
        Me.ceApplyConfigAllDetect.Properties.Caption = "Apply changes to all configuration"
        Me.ceApplyConfigAllDetect.Size = New System.Drawing.Size(326, 19)
        Me.ceApplyConfigAllDetect.TabIndex = 1
        '
        'btnListMngrDetect
        '
        Me.btnListMngrDetect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnListMngrDetect.Location = New System.Drawing.Point(3, 212)
        Me.btnListMngrDetect.Name = "btnListMngrDetect"
        Me.btnListMngrDetect.Size = New System.Drawing.Size(338, 26)
        Me.btnListMngrDetect.TabIndex = 1
        Me.btnListMngrDetect.Text = "List Manager"
        '
        'gcConfigSummDetect
        '
        Me.gcConfigSummDetect.AllowDrop = True
        Me.gcConfigSummDetect.ContextMenuStrip = Me.cmsConfigurationSummary
        Me.gcConfigSummDetect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcConfigSummDetect.Location = New System.Drawing.Point(3, 3)
        Me.gcConfigSummDetect.MainView = Me.gvConfigSummDetect
        Me.gcConfigSummDetect.Name = "gcConfigSummDetect"
        Me.gcConfigSummDetect.Size = New System.Drawing.Size(854, 241)
        Me.gcConfigSummDetect.TabIndex = 3
        Me.gcConfigSummDetect.Tag = "NB_Detect"
        Me.gcConfigSummDetect.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvConfigSummDetect, Me.GridView4})
        '
        'cmsConfigurationSummary
        '
        Me.cmsConfigurationSummary.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiAddNewRow, Me.tsmiCloneSelectedRows, Me.tsmiDeleteSelectedRows})
        Me.cmsConfigurationSummary.Name = "cmsConfigurationSummary"
        Me.cmsConfigurationSummary.Size = New System.Drawing.Size(194, 70)
        '
        'tsmiAddNewRow
        '
        Me.tsmiAddNewRow.Name = "tsmiAddNewRow"
        Me.tsmiAddNewRow.Size = New System.Drawing.Size(193, 22)
        Me.tsmiAddNewRow.Text = "Add New Row"
        '
        'tsmiCloneSelectedRows
        '
        Me.tsmiCloneSelectedRows.Name = "tsmiCloneSelectedRows"
        Me.tsmiCloneSelectedRows.Size = New System.Drawing.Size(193, 22)
        Me.tsmiCloneSelectedRows.Text = "Clone Selected Row(s)"
        '
        'tsmiDeleteSelectedRows
        '
        Me.tsmiDeleteSelectedRows.Name = "tsmiDeleteSelectedRows"
        Me.tsmiDeleteSelectedRows.Size = New System.Drawing.Size(193, 22)
        Me.tsmiDeleteSelectedRows.Text = "Delete Selected Row(s)"
        '
        'gvConfigSummDetect
        '
        Me.gvConfigSummDetect.ActiveFilterEnabled = False
        Me.gvConfigSummDetect.GridControl = Me.gcConfigSummDetect
        Me.gvConfigSummDetect.Name = "gvConfigSummDetect"
        Me.gvConfigSummDetect.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvConfigSummDetect.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvConfigSummDetect.OptionsBehavior.Editable = False
        Me.gvConfigSummDetect.OptionsBehavior.ReadOnly = True
        Me.gvConfigSummDetect.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvConfigSummDetect.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvConfigSummDetect.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvConfigSummDetect.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvConfigSummDetect.OptionsSelection.MultiSelect = True
        Me.gvConfigSummDetect.OptionsView.ShowGroupPanel = False
        '
        'GridView4
        '
        Me.GridView4.GridControl = Me.gcConfigSummDetect
        Me.GridView4.Name = "GridView4"
        '
        'tpNBCopy
        '
        Me.tpNBCopy.Controls.Add(Me.sccCopyCamp)
        Me.tpNBCopy.Name = "tpNBCopy"
        Me.tpNBCopy.Size = New System.Drawing.Size(1220, 688)
        Me.tpNBCopy.Text = "NB Copy"
        '
        'sccCopyCamp
        '
        Me.sccCopyCamp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccCopyCamp.Horizontal = False
        Me.sccCopyCamp.Location = New System.Drawing.Point(0, 0)
        Me.sccCopyCamp.Name = "sccCopyCamp"
        '
        'sccCopyCamp.Panel1
        '
        Me.sccCopyCamp.Panel1.Controls.Add(Me.SplitContainerControl2)
        Me.sccCopyCamp.Panel1.MinSize = 400
        Me.sccCopyCamp.Panel1.Text = "Panel1"
        '
        'sccCopyCamp.Panel2
        '
        Me.sccCopyCamp.Panel2.Controls.Add(Me.grpConfigSummCopy)
        Me.sccCopyCamp.Panel2.MinSize = 300
        Me.sccCopyCamp.Panel2.Text = "Panel2"
        Me.sccCopyCamp.Size = New System.Drawing.Size(1220, 688)
        Me.sccCopyCamp.SplitterPosition = 480
        Me.sccCopyCamp.TabIndex = 1
        Me.sccCopyCamp.Text = "SplitContainerControl1"
        '
        'SplitContainerControl2
        '
        Me.SplitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl2.Name = "SplitContainerControl2"
        '
        'SplitContainerControl2.Panel1
        '
        Me.SplitContainerControl2.Panel1.Controls.Add(Me.grpCampCopy)
        Me.SplitContainerControl2.Panel1.MinSize = 300
        Me.SplitContainerControl2.Panel1.Text = "Panel1"
        '
        'SplitContainerControl2.Panel2
        '
        Me.SplitContainerControl2.Panel2.Controls.Add(Me.TableLayoutPanel19)
        Me.SplitContainerControl2.Panel2.MinSize = 500
        Me.SplitContainerControl2.Panel2.Text = "Panel2"
        Me.SplitContainerControl2.Size = New System.Drawing.Size(1220, 400)
        Me.SplitContainerControl2.SplitterPosition = 396
        Me.SplitContainerControl2.TabIndex = 0
        Me.SplitContainerControl2.Text = "SplitContainerControl1"
        '
        'grpCampCopy
        '
        Me.grpCampCopy.Controls.Add(Me.TableLayoutPanel16)
        Me.grpCampCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpCampCopy.Location = New System.Drawing.Point(0, 0)
        Me.grpCampCopy.Name = "grpCampCopy"
        Me.grpCampCopy.Size = New System.Drawing.Size(396, 400)
        Me.grpCampCopy.TabIndex = 0
        Me.grpCampCopy.Text = "NB | Copy Campaigns"
        '
        'TableLayoutPanel16
        '
        Me.TableLayoutPanel16.ColumnCount = 1
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel16.Controls.Add(Me.grpCampPropCopy, 0, 2)
        Me.TableLayoutPanel16.Controls.Add(Me.gcCopyCampaigns, 0, 1)
        Me.TableLayoutPanel16.Controls.Add(Me.TableLayoutPanel18, 0, 0)
        Me.TableLayoutPanel16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel16.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel16.Name = "TableLayoutPanel16"
        Me.TableLayoutPanel16.RowCount = 3
        Me.TableLayoutPanel16.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel16.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel16.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 220.0!))
        Me.TableLayoutPanel16.Size = New System.Drawing.Size(392, 375)
        Me.TableLayoutPanel16.TabIndex = 0
        '
        'grpCampPropCopy
        '
        Me.grpCampPropCopy.Controls.Add(Me.TableLayoutPanel17)
        Me.grpCampPropCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpCampPropCopy.Location = New System.Drawing.Point(2, 157)
        Me.grpCampPropCopy.Margin = New System.Windows.Forms.Padding(2)
        Me.grpCampPropCopy.Name = "grpCampPropCopy"
        Me.grpCampPropCopy.Size = New System.Drawing.Size(388, 216)
        Me.grpCampPropCopy.TabIndex = 4
        Me.grpCampPropCopy.Text = "Campaign Properties"
        '
        'TableLayoutPanel17
        '
        Me.TableLayoutPanel17.ColumnCount = 3
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135.0!))
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel17.Controls.Add(Me.LabelControl14, 0, 0)
        Me.TableLayoutPanel17.Controls.Add(Me.LabelControl15, 0, 1)
        Me.TableLayoutPanel17.Controls.Add(Me.LabelControl16, 0, 3)
        Me.TableLayoutPanel17.Controls.Add(Me.LabelControl17, 0, 4)
        Me.TableLayoutPanel17.Controls.Add(Me.LabelControl18, 0, 5)
        Me.TableLayoutPanel17.Controls.Add(Me.LabelControl19, 0, 6)
        Me.TableLayoutPanel17.Controls.Add(Me.lblLastRunTimeCopy, 1, 5)
        Me.TableLayoutPanel17.Controls.Add(Me.lblLastEndTimeCopy, 1, 6)
        Me.TableLayoutPanel17.Controls.Add(Me.lblOwnerCopy, 1, 0)
        Me.TableLayoutPanel17.Controls.Add(Me.ceActiveCopy, 1, 1)
        Me.TableLayoutPanel17.Controls.Add(Me.deSchNxtStartTimeCopy, 1, 3)
        Me.TableLayoutPanel17.Controls.Add(Me.cmbSchRptIntervalCopy, 1, 4)
        Me.TableLayoutPanel17.Controls.Add(Me.btnRunNowCopy, 2, 0)
        Me.TableLayoutPanel17.Controls.Add(Me.ceIsPublicCopy, 1, 2)
        Me.TableLayoutPanel17.Controls.Add(Me.LabelControl32, 0, 2)
        Me.TableLayoutPanel17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel17.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel17.Name = "TableLayoutPanel17"
        Me.TableLayoutPanel17.RowCount = 8
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel17.Size = New System.Drawing.Size(384, 191)
        Me.TableLayoutPanel17.TabIndex = 0
        '
        'LabelControl14
        '
        Me.LabelControl14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl14.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl14.Name = "LabelControl14"
        Me.LabelControl14.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl14.Size = New System.Drawing.Size(129, 24)
        Me.LabelControl14.TabIndex = 0
        Me.LabelControl14.Text = "Owner"
        '
        'LabelControl15
        '
        Me.LabelControl15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl15.Location = New System.Drawing.Point(3, 33)
        Me.LabelControl15.Name = "LabelControl15"
        Me.LabelControl15.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl15.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl15.TabIndex = 1
        Me.LabelControl15.Text = "Active"
        '
        'LabelControl16
        '
        Me.LabelControl16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl16.Location = New System.Drawing.Point(3, 85)
        Me.LabelControl16.Name = "LabelControl16"
        Me.LabelControl16.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl16.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl16.TabIndex = 2
        Me.LabelControl16.Text = "Schedule Next Start Time"
        '
        'LabelControl17
        '
        Me.LabelControl17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl17.Location = New System.Drawing.Point(3, 111)
        Me.LabelControl17.Name = "LabelControl17"
        Me.LabelControl17.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl17.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl17.TabIndex = 3
        Me.LabelControl17.Text = "Schedule Repeat Interval"
        '
        'LabelControl18
        '
        Me.LabelControl18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl18.Location = New System.Drawing.Point(3, 137)
        Me.LabelControl18.Name = "LabelControl18"
        Me.LabelControl18.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl18.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl18.TabIndex = 4
        Me.LabelControl18.Text = "Last Run TIme"
        '
        'LabelControl19
        '
        Me.LabelControl19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl19.Location = New System.Drawing.Point(3, 163)
        Me.LabelControl19.Name = "LabelControl19"
        Me.LabelControl19.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl19.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl19.TabIndex = 5
        Me.LabelControl19.Text = "Last End Time"
        '
        'lblLastRunTimeCopy
        '
        Me.TableLayoutPanel17.SetColumnSpan(Me.lblLastRunTimeCopy, 2)
        Me.lblLastRunTimeCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLastRunTimeCopy.Location = New System.Drawing.Point(138, 137)
        Me.lblLastRunTimeCopy.Name = "lblLastRunTimeCopy"
        Me.lblLastRunTimeCopy.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblLastRunTimeCopy.Size = New System.Drawing.Size(243, 20)
        Me.lblLastRunTimeCopy.TabIndex = 6
        '
        'lblLastEndTimeCopy
        '
        Me.TableLayoutPanel17.SetColumnSpan(Me.lblLastEndTimeCopy, 2)
        Me.lblLastEndTimeCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLastEndTimeCopy.Location = New System.Drawing.Point(138, 163)
        Me.lblLastEndTimeCopy.Name = "lblLastEndTimeCopy"
        Me.lblLastEndTimeCopy.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblLastEndTimeCopy.Size = New System.Drawing.Size(243, 20)
        Me.lblLastEndTimeCopy.TabIndex = 7
        '
        'lblOwnerCopy
        '
        Me.lblOwnerCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblOwnerCopy.Location = New System.Drawing.Point(138, 3)
        Me.lblOwnerCopy.Name = "lblOwnerCopy"
        Me.lblOwnerCopy.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblOwnerCopy.Size = New System.Drawing.Size(173, 24)
        Me.lblOwnerCopy.TabIndex = 9
        '
        'ceActiveCopy
        '
        Me.ceActiveCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceActiveCopy.Location = New System.Drawing.Point(140, 33)
        Me.ceActiveCopy.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceActiveCopy.Name = "ceActiveCopy"
        Me.ceActiveCopy.Properties.Caption = ""
        Me.ceActiveCopy.Size = New System.Drawing.Size(171, 20)
        Me.ceActiveCopy.TabIndex = 10
        Me.ceActiveCopy.Tag = "NB_Copy"
        '
        'deSchNxtStartTimeCopy
        '
        Me.deSchNxtStartTimeCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.deSchNxtStartTimeCopy.EditValue = New Date(2016, 9, 14, 12, 41, 50, 900)
        Me.deSchNxtStartTimeCopy.Enabled = False
        Me.deSchNxtStartTimeCopy.Location = New System.Drawing.Point(138, 85)
        Me.deSchNxtStartTimeCopy.Name = "deSchNxtStartTimeCopy"
        Me.deSchNxtStartTimeCopy.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deSchNxtStartTimeCopy.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deSchNxtStartTimeCopy.Size = New System.Drawing.Size(173, 20)
        Me.deSchNxtStartTimeCopy.TabIndex = 11
        Me.deSchNxtStartTimeCopy.Tag = "NB_Copy"
        '
        'cmbSchRptIntervalCopy
        '
        Me.cmbSchRptIntervalCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbSchRptIntervalCopy.EditValue = "WEEKLY"
        Me.cmbSchRptIntervalCopy.Enabled = False
        Me.cmbSchRptIntervalCopy.Location = New System.Drawing.Point(138, 111)
        Me.cmbSchRptIntervalCopy.Name = "cmbSchRptIntervalCopy"
        Me.cmbSchRptIntervalCopy.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbSchRptIntervalCopy.Properties.Items.AddRange(New Object() {"DAILY", "WEEKLY"})
        Me.cmbSchRptIntervalCopy.Size = New System.Drawing.Size(173, 20)
        Me.cmbSchRptIntervalCopy.TabIndex = 12
        Me.cmbSchRptIntervalCopy.Tag = "NB_Copy"
        '
        'btnRunNowCopy
        '
        Me.btnRunNowCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRunNowCopy.Location = New System.Drawing.Point(317, 3)
        Me.btnRunNowCopy.Name = "btnRunNowCopy"
        Me.btnRunNowCopy.Size = New System.Drawing.Size(64, 24)
        Me.btnRunNowCopy.TabIndex = 8
        Me.btnRunNowCopy.Text = "Run Now"
        '
        'ceIsPublicCopy
        '
        Me.ceIsPublicCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceIsPublicCopy.Location = New System.Drawing.Point(140, 59)
        Me.ceIsPublicCopy.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceIsPublicCopy.Name = "ceIsPublicCopy"
        Me.ceIsPublicCopy.Properties.Caption = ""
        Me.ceIsPublicCopy.Size = New System.Drawing.Size(171, 20)
        Me.ceIsPublicCopy.TabIndex = 13
        Me.ceIsPublicCopy.Tag = "NB_Copy"
        '
        'LabelControl32
        '
        Me.LabelControl32.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl32.Location = New System.Drawing.Point(3, 59)
        Me.LabelControl32.Name = "LabelControl32"
        Me.LabelControl32.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl32.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl32.TabIndex = 14
        Me.LabelControl32.Text = "Is Public"
        '
        'gcCopyCampaigns
        '
        Me.gcCopyCampaigns.AllowDrop = True
        Me.gcCopyCampaigns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcCopyCampaigns.Location = New System.Drawing.Point(2, 29)
        Me.gcCopyCampaigns.MainView = Me.gvCopyCampaigns
        Me.gcCopyCampaigns.Margin = New System.Windows.Forms.Padding(2)
        Me.gcCopyCampaigns.Name = "gcCopyCampaigns"
        Me.gcCopyCampaigns.Size = New System.Drawing.Size(388, 124)
        Me.gcCopyCampaigns.TabIndex = 5
        Me.gcCopyCampaigns.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvCopyCampaigns, Me.GridView9})
        '
        'gvCopyCampaigns
        '
        Me.gvCopyCampaigns.ActiveFilterEnabled = False
        Me.gvCopyCampaigns.GridControl = Me.gcCopyCampaigns
        Me.gvCopyCampaigns.Name = "gvCopyCampaigns"
        Me.gvCopyCampaigns.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCopyCampaigns.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCopyCampaigns.OptionsBehavior.Editable = False
        Me.gvCopyCampaigns.OptionsBehavior.ReadOnly = True
        Me.gvCopyCampaigns.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCopyCampaigns.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCopyCampaigns.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCopyCampaigns.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvCopyCampaigns.OptionsSelection.MultiSelect = True
        Me.gvCopyCampaigns.OptionsView.ShowGroupPanel = False
        '
        'GridView9
        '
        Me.GridView9.GridControl = Me.gcCopyCampaigns
        Me.GridView9.Name = "GridView9"
        '
        'TableLayoutPanel18
        '
        Me.TableLayoutPanel18.ColumnCount = 4
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel18.Controls.Add(Me.btnCopyRefresh, 0, 0)
        Me.TableLayoutPanel18.Controls.Add(Me.txtSearchCopy, 0, 0)
        Me.TableLayoutPanel18.Controls.Add(Me.btnCloneCopy, 2, 0)
        Me.TableLayoutPanel18.Controls.Add(Me.btnDeleteCopy, 3, 0)
        Me.TableLayoutPanel18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel18.Location = New System.Drawing.Point(1, 1)
        Me.TableLayoutPanel18.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel18.Name = "TableLayoutPanel18"
        Me.TableLayoutPanel18.RowCount = 1
        Me.TableLayoutPanel18.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel18.Size = New System.Drawing.Size(390, 25)
        Me.TableLayoutPanel18.TabIndex = 6
        '
        'btnCopyRefresh
        '
        Me.btnCopyRefresh.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCopyRefresh.Location = New System.Drawing.Point(227, 2)
        Me.btnCopyRefresh.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCopyRefresh.Name = "btnCopyRefresh"
        Me.btnCopyRefresh.Size = New System.Drawing.Size(51, 21)
        Me.btnCopyRefresh.TabIndex = 7
        Me.btnCopyRefresh.Tag = "NB_Copy"
        Me.btnCopyRefresh.Text = "Refresh"
        '
        'txtSearchCopy
        '
        Me.txtSearchCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchCopy.Location = New System.Drawing.Point(2, 2)
        Me.txtSearchCopy.Margin = New System.Windows.Forms.Padding(2)
        Me.txtSearchCopy.Name = "txtSearchCopy"
        Me.txtSearchCopy.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSearchCopy.Properties.NullValuePrompt = "Search..."
        Me.txtSearchCopy.Size = New System.Drawing.Size(221, 20)
        Me.txtSearchCopy.TabIndex = 3
        '
        'btnCloneCopy
        '
        Me.btnCloneCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCloneCopy.Location = New System.Drawing.Point(282, 2)
        Me.btnCloneCopy.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCloneCopy.Name = "btnCloneCopy"
        Me.btnCloneCopy.Size = New System.Drawing.Size(51, 21)
        Me.btnCloneCopy.TabIndex = 5
        Me.btnCloneCopy.Text = "Clone"
        '
        'btnDeleteCopy
        '
        Me.btnDeleteCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDeleteCopy.Location = New System.Drawing.Point(337, 2)
        Me.btnDeleteCopy.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDeleteCopy.Name = "btnDeleteCopy"
        Me.btnDeleteCopy.Size = New System.Drawing.Size(51, 21)
        Me.btnDeleteCopy.TabIndex = 6
        Me.btnDeleteCopy.Text = "Delete"
        '
        'TableLayoutPanel19
        '
        Me.TableLayoutPanel19.ColumnCount = 1
        Me.TableLayoutPanel19.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel19.Controls.Add(Me.grpCampSummCopy, 0, 0)
        Me.TableLayoutPanel19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel19.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel19.Name = "TableLayoutPanel19"
        Me.TableLayoutPanel19.RowCount = 2
        Me.TableLayoutPanel19.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel19.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3.0!))
        Me.TableLayoutPanel19.Size = New System.Drawing.Size(814, 400)
        Me.TableLayoutPanel19.TabIndex = 0
        '
        'grpCampSummCopy
        '
        Me.grpCampSummCopy.Controls.Add(Me.TableLayoutPanel5)
        Me.grpCampSummCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpCampSummCopy.Location = New System.Drawing.Point(3, 3)
        Me.grpCampSummCopy.Name = "grpCampSummCopy"
        Me.grpCampSummCopy.Size = New System.Drawing.Size(808, 391)
        Me.grpCampSummCopy.TabIndex = 1
        Me.grpCampSummCopy.Text = "Campaign Result Summary"
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 1
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.TableLayoutPanel29, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.xtcCampSummCopy, 0, 1)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 2
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(804, 366)
        Me.TableLayoutPanel5.TabIndex = 3
        '
        'TableLayoutPanel29
        '
        Me.TableLayoutPanel29.ColumnCount = 3
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220.0!))
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel29.Controls.Add(Me.cmbCopyResultSetID, 1, 0)
        Me.TableLayoutPanel29.Controls.Add(Me.LabelControl20, 0, 0)
        Me.TableLayoutPanel29.Controls.Add(Me.btnDeleteCopyResultSet, 2, 0)
        Me.TableLayoutPanel29.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel29.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel29.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel29.Name = "TableLayoutPanel29"
        Me.TableLayoutPanel29.RowCount = 1
        Me.TableLayoutPanel29.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel29.Size = New System.Drawing.Size(800, 26)
        Me.TableLayoutPanel29.TabIndex = 1
        '
        'cmbCopyResultSetID
        '
        Me.cmbCopyResultSetID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbCopyResultSetID.EditValue = ""
        Me.cmbCopyResultSetID.Location = New System.Drawing.Point(83, 3)
        Me.cmbCopyResultSetID.Name = "cmbCopyResultSetID"
        Me.cmbCopyResultSetID.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbCopyResultSetID.Size = New System.Drawing.Size(214, 20)
        Me.cmbCopyResultSetID.TabIndex = 13
        '
        'LabelControl20
        '
        Me.LabelControl20.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl20.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl20.Name = "LabelControl20"
        Me.LabelControl20.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl20.Size = New System.Drawing.Size(74, 20)
        Me.LabelControl20.TabIndex = 3
        Me.LabelControl20.Text = "Result Set ID"
        '
        'btnDeleteCopyResultSet
        '
        Me.btnDeleteCopyResultSet.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDeleteCopyResultSet.Location = New System.Drawing.Point(302, 2)
        Me.btnDeleteCopyResultSet.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDeleteCopyResultSet.Name = "btnDeleteCopyResultSet"
        Me.btnDeleteCopyResultSet.Size = New System.Drawing.Size(65, 22)
        Me.btnDeleteCopyResultSet.TabIndex = 14
        Me.btnDeleteCopyResultSet.Tag = "NB_Copy"
        Me.btnDeleteCopyResultSet.Text = "Delete"
        '
        'xtcCampSummCopy
        '
        Me.xtcCampSummCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtcCampSummCopy.Location = New System.Drawing.Point(3, 33)
        Me.xtcCampSummCopy.Name = "xtcCampSummCopy"
        Me.xtcCampSummCopy.SelectedTabPage = Me.tpCampCopySumm
        Me.xtcCampSummCopy.Size = New System.Drawing.Size(798, 330)
        Me.xtcCampSummCopy.TabIndex = 2
        Me.xtcCampSummCopy.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tpCampCopySumm, Me.tpCampCopyData})
        '
        'tpCampCopySumm
        '
        Me.tpCampCopySumm.Controls.Add(Me.gcCampSummCopy)
        Me.tpCampCopySumm.Name = "tpCampCopySumm"
        Me.tpCampCopySumm.Size = New System.Drawing.Size(796, 305)
        Me.tpCampCopySumm.Text = "Summary"
        '
        'gcCampSummCopy
        '
        Me.gcCampSummCopy.AllowDrop = True
        Me.gcCampSummCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcCampSummCopy.Location = New System.Drawing.Point(0, 0)
        Me.gcCampSummCopy.MainView = Me.gvCampSummCopy
        Me.gcCampSummCopy.Name = "gcCampSummCopy"
        Me.gcCampSummCopy.Size = New System.Drawing.Size(796, 305)
        Me.gcCampSummCopy.TabIndex = 2
        Me.gcCampSummCopy.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvCampSummCopy, Me.GridView14})
        '
        'gvCampSummCopy
        '
        Me.gvCampSummCopy.ActiveFilterEnabled = False
        Me.gvCampSummCopy.GridControl = Me.gcCampSummCopy
        Me.gvCampSummCopy.Name = "gvCampSummCopy"
        Me.gvCampSummCopy.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampSummCopy.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampSummCopy.OptionsBehavior.Editable = False
        Me.gvCampSummCopy.OptionsBehavior.ReadOnly = True
        Me.gvCampSummCopy.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampSummCopy.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampSummCopy.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampSummCopy.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvCampSummCopy.OptionsSelection.MultiSelect = True
        Me.gvCampSummCopy.OptionsView.ShowGroupPanel = False
        '
        'GridView14
        '
        Me.GridView14.GridControl = Me.gcCampSummCopy
        Me.GridView14.Name = "GridView14"
        '
        'tpCampCopyData
        '
        Me.tpCampCopyData.Controls.Add(Me.TableLayoutPanel6)
        Me.tpCampCopyData.Name = "tpCampCopyData"
        Me.tpCampCopyData.Size = New System.Drawing.Size(796, 305)
        Me.tpCampCopyData.Text = "Data"
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 1
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.gcCampDataCopy, 0, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.TableLayoutPanel36, 0, 0)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 2
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(796, 305)
        Me.TableLayoutPanel6.TabIndex = 1
        '
        'gcCampDataCopy
        '
        Me.gcCampDataCopy.AllowDrop = True
        Me.gcCampDataCopy.ContextMenuStrip = Me.cmsMapNB
        Me.gcCampDataCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcCampDataCopy.Location = New System.Drawing.Point(3, 38)
        Me.gcCampDataCopy.MainView = Me.gvCampDataCopy
        Me.gcCampDataCopy.Name = "gcCampDataCopy"
        Me.gcCampDataCopy.Size = New System.Drawing.Size(790, 264)
        Me.gcCampDataCopy.TabIndex = 4
        Me.gcCampDataCopy.Tag = "NBCopy"
        Me.gcCampDataCopy.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvCampDataCopy, Me.GridView13})
        '
        'gvCampDataCopy
        '
        Me.gvCampDataCopy.ActiveFilterEnabled = False
        Me.gvCampDataCopy.GridControl = Me.gcCampDataCopy
        Me.gvCampDataCopy.Name = "gvCampDataCopy"
        Me.gvCampDataCopy.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampDataCopy.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampDataCopy.OptionsBehavior.Editable = False
        Me.gvCampDataCopy.OptionsBehavior.ReadOnly = True
        Me.gvCampDataCopy.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampDataCopy.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampDataCopy.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampDataCopy.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.gvCampDataCopy.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvCampDataCopy.OptionsSelection.MultiSelect = True
        Me.gvCampDataCopy.OptionsView.ShowGroupPanel = False
        '
        'GridView13
        '
        Me.GridView13.GridControl = Me.gcCampDataCopy
        Me.GridView13.Name = "GridView13"
        '
        'TableLayoutPanel36
        '
        Me.TableLayoutPanel36.ColumnCount = 3
        Me.TableLayoutPanel36.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel36.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel36.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel36.Controls.Add(Me.btnCopyDataLoadGrid, 0, 0)
        Me.TableLayoutPanel36.Controls.Add(Me.btnCopyDataAllCsv, 1, 0)
        Me.TableLayoutPanel36.Controls.Add(Me.lblCopyDataRowCount, 2, 0)
        Me.TableLayoutPanel36.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel36.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel36.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel36.Name = "TableLayoutPanel36"
        Me.TableLayoutPanel36.RowCount = 1
        Me.TableLayoutPanel36.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel36.Size = New System.Drawing.Size(792, 31)
        Me.TableLayoutPanel36.TabIndex = 0
        '
        'btnCopyDataLoadGrid
        '
        Me.btnCopyDataLoadGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCopyDataLoadGrid.Location = New System.Drawing.Point(3, 3)
        Me.btnCopyDataLoadGrid.Name = "btnCopyDataLoadGrid"
        Me.btnCopyDataLoadGrid.Size = New System.Drawing.Size(94, 25)
        Me.btnCopyDataLoadGrid.TabIndex = 0
        Me.btnCopyDataLoadGrid.Text = "Load To Grid"
        '
        'btnCopyDataAllCsv
        '
        Me.btnCopyDataAllCsv.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnCopyDataAllCsv.Location = New System.Drawing.Point(103, 3)
        Me.btnCopyDataAllCsv.Name = "btnCopyDataAllCsv"
        Me.btnCopyDataAllCsv.Size = New System.Drawing.Size(94, 25)
        Me.btnCopyDataAllCsv.TabIndex = 1
        Me.btnCopyDataAllCsv.Text = "All Data To CSV"
        '
        'lblCopyDataRowCount
        '
        Me.lblCopyDataRowCount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCopyDataRowCount.Location = New System.Drawing.Point(203, 3)
        Me.lblCopyDataRowCount.Name = "lblCopyDataRowCount"
        Me.lblCopyDataRowCount.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblCopyDataRowCount.Size = New System.Drawing.Size(586, 25)
        Me.lblCopyDataRowCount.TabIndex = 3
        Me.lblCopyDataRowCount.Text = "Count of Records: "
        Me.lblCopyDataRowCount.Visible = False
        '
        'grpConfigSummCopy
        '
        Me.grpConfigSummCopy.Controls.Add(Me.TableLayoutPanel22)
        Me.grpConfigSummCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpConfigSummCopy.Location = New System.Drawing.Point(0, 0)
        Me.grpConfigSummCopy.Name = "grpConfigSummCopy"
        Me.grpConfigSummCopy.Size = New System.Drawing.Size(1220, 278)
        Me.grpConfigSummCopy.TabIndex = 0
        Me.grpConfigSummCopy.Text = "Configuration Summary"
        '
        'TableLayoutPanel22
        '
        Me.TableLayoutPanel22.ColumnCount = 2
        Me.TableLayoutPanel22.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel22.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350.0!))
        Me.TableLayoutPanel22.Controls.Add(Me.gcConfigSummCopy, 0, 0)
        Me.TableLayoutPanel22.Controls.Add(Me.TableLayoutPanel20, 1, 0)
        Me.TableLayoutPanel22.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel22.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel22.Name = "TableLayoutPanel22"
        Me.TableLayoutPanel22.RowCount = 1
        Me.TableLayoutPanel22.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel22.Size = New System.Drawing.Size(1216, 253)
        Me.TableLayoutPanel22.TabIndex = 1
        '
        'gcConfigSummCopy
        '
        Me.gcConfigSummCopy.AllowDrop = True
        Me.gcConfigSummCopy.ContextMenuStrip = Me.cmsConfigurationSummary
        Me.gcConfigSummCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcConfigSummCopy.Location = New System.Drawing.Point(3, 3)
        Me.gcConfigSummCopy.MainView = Me.gvConfigSummCopy
        Me.gcConfigSummCopy.Name = "gcConfigSummCopy"
        Me.gcConfigSummCopy.Size = New System.Drawing.Size(860, 247)
        Me.gcConfigSummCopy.TabIndex = 3
        Me.gcConfigSummCopy.Tag = "NB_Copy"
        Me.gcConfigSummCopy.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvConfigSummCopy, Me.GridView16})
        '
        'gvConfigSummCopy
        '
        Me.gvConfigSummCopy.ActiveFilterEnabled = False
        Me.gvConfigSummCopy.GridControl = Me.gcConfigSummCopy
        Me.gvConfigSummCopy.Name = "gvConfigSummCopy"
        Me.gvConfigSummCopy.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvConfigSummCopy.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvConfigSummCopy.OptionsBehavior.Editable = False
        Me.gvConfigSummCopy.OptionsBehavior.ReadOnly = True
        Me.gvConfigSummCopy.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvConfigSummCopy.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvConfigSummCopy.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvConfigSummCopy.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvConfigSummCopy.OptionsSelection.MultiSelect = True
        Me.gvConfigSummCopy.OptionsView.ShowGroupPanel = False
        '
        'GridView16
        '
        Me.GridView16.GridControl = Me.gcConfigSummCopy
        Me.GridView16.Name = "GridView16"
        '
        'TableLayoutPanel20
        '
        Me.TableLayoutPanel20.ColumnCount = 1
        Me.TableLayoutPanel20.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel20.Controls.Add(Me.grpLayerPropCopy, 0, 0)
        Me.TableLayoutPanel20.Controls.Add(Me.btnListMngrCopy, 0, 1)
        Me.TableLayoutPanel20.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel20.Location = New System.Drawing.Point(869, 3)
        Me.TableLayoutPanel20.Name = "TableLayoutPanel20"
        Me.TableLayoutPanel20.RowCount = 2
        Me.TableLayoutPanel20.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel20.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel20.Size = New System.Drawing.Size(344, 247)
        Me.TableLayoutPanel20.TabIndex = 0
        '
        'grpLayerPropCopy
        '
        Me.grpLayerPropCopy.Controls.Add(Me.TableLayoutPanel60)
        Me.grpLayerPropCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpLayerPropCopy.Location = New System.Drawing.Point(3, 3)
        Me.grpLayerPropCopy.Name = "grpLayerPropCopy"
        Me.grpLayerPropCopy.Size = New System.Drawing.Size(338, 209)
        Me.grpLayerPropCopy.TabIndex = 1
        Me.grpLayerPropCopy.Text = "Layer Properties"
        '
        'TableLayoutPanel60
        '
        Me.TableLayoutPanel60.ColumnCount = 1
        Me.TableLayoutPanel60.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel60.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel60.Controls.Add(Me.ceApplyConfigAllCopy, 0, 1)
        Me.TableLayoutPanel60.Controls.Add(Me.layerPropGridCopy, 0, 0)
        Me.TableLayoutPanel60.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel60.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel60.Name = "TableLayoutPanel60"
        Me.TableLayoutPanel60.RowCount = 2
        Me.TableLayoutPanel60.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel60.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel60.Size = New System.Drawing.Size(334, 184)
        Me.TableLayoutPanel60.TabIndex = 1
        '
        'ceApplyConfigAllCopy
        '
        Me.ceApplyConfigAllCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceApplyConfigAllCopy.Location = New System.Drawing.Point(5, 162)
        Me.ceApplyConfigAllCopy.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceApplyConfigAllCopy.Name = "ceApplyConfigAllCopy"
        Me.ceApplyConfigAllCopy.Properties.Caption = "Apply changes to all configuration"
        Me.ceApplyConfigAllCopy.Size = New System.Drawing.Size(326, 19)
        Me.ceApplyConfigAllCopy.TabIndex = 2
        '
        'layerPropGridCopy
        '
        Me.layerPropGridCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layerPropGridCopy.LineColor = System.Drawing.SystemColors.ControlDark
        Me.layerPropGridCopy.Location = New System.Drawing.Point(3, 3)
        Me.layerPropGridCopy.Name = "layerPropGridCopy"
        Me.layerPropGridCopy.Size = New System.Drawing.Size(328, 153)
        Me.layerPropGridCopy.TabIndex = 0
        Me.layerPropGridCopy.Tag = "NB_Copy"
        Me.layerPropGridCopy.ToolbarVisible = False
        '
        'btnListMngrCopy
        '
        Me.btnListMngrCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnListMngrCopy.Location = New System.Drawing.Point(3, 218)
        Me.btnListMngrCopy.Name = "btnListMngrCopy"
        Me.btnListMngrCopy.Size = New System.Drawing.Size(338, 26)
        Me.btnListMngrCopy.TabIndex = 1
        Me.btnListMngrCopy.Text = "List Manager"
        '
        'tpNBDelete
        '
        Me.tpNBDelete.Controls.Add(Me.sccDeleteCamp)
        Me.tpNBDelete.Name = "tpNBDelete"
        Me.tpNBDelete.Size = New System.Drawing.Size(1220, 688)
        Me.tpNBDelete.Text = "NB Delete"
        '
        'sccDeleteCamp
        '
        Me.sccDeleteCamp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccDeleteCamp.Horizontal = False
        Me.sccDeleteCamp.Location = New System.Drawing.Point(0, 0)
        Me.sccDeleteCamp.Name = "sccDeleteCamp"
        '
        'sccDeleteCamp.Panel1
        '
        Me.sccDeleteCamp.Panel1.Controls.Add(Me.SplitContainerControl8)
        Me.sccDeleteCamp.Panel1.MinSize = 400
        Me.sccDeleteCamp.Panel1.Text = "Panel1"
        '
        'sccDeleteCamp.Panel2
        '
        Me.sccDeleteCamp.Panel2.Controls.Add(Me.grpConfigSummDelete)
        Me.sccDeleteCamp.Panel2.MinSize = 300
        Me.sccDeleteCamp.Panel2.Text = "Panel2"
        Me.sccDeleteCamp.Size = New System.Drawing.Size(1220, 688)
        Me.sccDeleteCamp.SplitterPosition = 480
        Me.sccDeleteCamp.TabIndex = 2
        Me.sccDeleteCamp.Text = "SplitContainerControl1"
        '
        'SplitContainerControl8
        '
        Me.SplitContainerControl8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl8.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl8.Name = "SplitContainerControl8"
        '
        'SplitContainerControl8.Panel1
        '
        Me.SplitContainerControl8.Panel1.Controls.Add(Me.grpCampDelete)
        Me.SplitContainerControl8.Panel1.MinSize = 300
        Me.SplitContainerControl8.Panel1.Text = "Panel1"
        '
        'SplitContainerControl8.Panel2
        '
        Me.SplitContainerControl8.Panel2.Controls.Add(Me.TableLayoutPanel66)
        Me.SplitContainerControl8.Panel2.MinSize = 500
        Me.SplitContainerControl8.Panel2.Text = "Panel2"
        Me.SplitContainerControl8.Size = New System.Drawing.Size(1220, 400)
        Me.SplitContainerControl8.SplitterPosition = 396
        Me.SplitContainerControl8.TabIndex = 0
        Me.SplitContainerControl8.Text = "SplitContainerControl1"
        '
        'grpCampDelete
        '
        Me.grpCampDelete.Controls.Add(Me.TableLayoutPanel63)
        Me.grpCampDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpCampDelete.Location = New System.Drawing.Point(0, 0)
        Me.grpCampDelete.Name = "grpCampDelete"
        Me.grpCampDelete.Size = New System.Drawing.Size(396, 400)
        Me.grpCampDelete.TabIndex = 0
        Me.grpCampDelete.Text = "NB | Delete Campaigns"
        '
        'TableLayoutPanel63
        '
        Me.TableLayoutPanel63.ColumnCount = 1
        Me.TableLayoutPanel63.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel63.Controls.Add(Me.grpCampPropDelete, 0, 2)
        Me.TableLayoutPanel63.Controls.Add(Me.gcDeleteCampaigns, 0, 1)
        Me.TableLayoutPanel63.Controls.Add(Me.TableLayoutPanel65, 0, 0)
        Me.TableLayoutPanel63.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel63.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel63.Name = "TableLayoutPanel63"
        Me.TableLayoutPanel63.RowCount = 3
        Me.TableLayoutPanel63.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel63.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel63.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 220.0!))
        Me.TableLayoutPanel63.Size = New System.Drawing.Size(392, 375)
        Me.TableLayoutPanel63.TabIndex = 0
        '
        'grpCampPropDelete
        '
        Me.grpCampPropDelete.Controls.Add(Me.TableLayoutPanel64)
        Me.grpCampPropDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpCampPropDelete.Location = New System.Drawing.Point(2, 157)
        Me.grpCampPropDelete.Margin = New System.Windows.Forms.Padding(2)
        Me.grpCampPropDelete.Name = "grpCampPropDelete"
        Me.grpCampPropDelete.Size = New System.Drawing.Size(388, 216)
        Me.grpCampPropDelete.TabIndex = 4
        Me.grpCampPropDelete.Text = "Campaign Properties"
        '
        'TableLayoutPanel64
        '
        Me.TableLayoutPanel64.ColumnCount = 3
        Me.TableLayoutPanel64.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135.0!))
        Me.TableLayoutPanel64.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel64.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel64.Controls.Add(Me.LabelControl52, 0, 0)
        Me.TableLayoutPanel64.Controls.Add(Me.LabelControl53, 0, 1)
        Me.TableLayoutPanel64.Controls.Add(Me.LabelControl54, 0, 3)
        Me.TableLayoutPanel64.Controls.Add(Me.LabelControl55, 0, 4)
        Me.TableLayoutPanel64.Controls.Add(Me.LabelControl56, 0, 5)
        Me.TableLayoutPanel64.Controls.Add(Me.LabelControl57, 0, 6)
        Me.TableLayoutPanel64.Controls.Add(Me.lblLastRunTimeDelete, 1, 5)
        Me.TableLayoutPanel64.Controls.Add(Me.lblLastEndTimeDelete, 1, 6)
        Me.TableLayoutPanel64.Controls.Add(Me.lblOwnerDelete, 1, 0)
        Me.TableLayoutPanel64.Controls.Add(Me.ceActiveDelete, 1, 1)
        Me.TableLayoutPanel64.Controls.Add(Me.deSchNxtStartTimeDelete, 1, 3)
        Me.TableLayoutPanel64.Controls.Add(Me.cmbSchRptIntervalDelete, 1, 4)
        Me.TableLayoutPanel64.Controls.Add(Me.btnRunNowDelete, 2, 0)
        Me.TableLayoutPanel64.Controls.Add(Me.ceIsPublicDelete, 1, 2)
        Me.TableLayoutPanel64.Controls.Add(Me.LabelControl61, 0, 2)
        Me.TableLayoutPanel64.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel64.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel64.Name = "TableLayoutPanel64"
        Me.TableLayoutPanel64.RowCount = 8
        Me.TableLayoutPanel64.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel64.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel64.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel64.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel64.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel64.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel64.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel64.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel64.Size = New System.Drawing.Size(384, 191)
        Me.TableLayoutPanel64.TabIndex = 0
        '
        'LabelControl52
        '
        Me.LabelControl52.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl52.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl52.Name = "LabelControl52"
        Me.LabelControl52.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl52.Size = New System.Drawing.Size(129, 24)
        Me.LabelControl52.TabIndex = 0
        Me.LabelControl52.Text = "Owner"
        '
        'LabelControl53
        '
        Me.LabelControl53.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl53.Location = New System.Drawing.Point(3, 33)
        Me.LabelControl53.Name = "LabelControl53"
        Me.LabelControl53.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl53.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl53.TabIndex = 1
        Me.LabelControl53.Text = "Active"
        '
        'LabelControl54
        '
        Me.LabelControl54.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl54.Location = New System.Drawing.Point(3, 85)
        Me.LabelControl54.Name = "LabelControl54"
        Me.LabelControl54.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl54.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl54.TabIndex = 2
        Me.LabelControl54.Text = "Schedule Next Start Time"
        '
        'LabelControl55
        '
        Me.LabelControl55.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl55.Location = New System.Drawing.Point(3, 111)
        Me.LabelControl55.Name = "LabelControl55"
        Me.LabelControl55.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl55.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl55.TabIndex = 3
        Me.LabelControl55.Text = "Schedule Repeat Interval"
        '
        'LabelControl56
        '
        Me.LabelControl56.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl56.Location = New System.Drawing.Point(3, 137)
        Me.LabelControl56.Name = "LabelControl56"
        Me.LabelControl56.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl56.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl56.TabIndex = 4
        Me.LabelControl56.Text = "Last Run TIme"
        '
        'LabelControl57
        '
        Me.LabelControl57.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl57.Location = New System.Drawing.Point(3, 163)
        Me.LabelControl57.Name = "LabelControl57"
        Me.LabelControl57.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl57.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl57.TabIndex = 5
        Me.LabelControl57.Text = "Last End Time"
        '
        'lblLastRunTimeDelete
        '
        Me.TableLayoutPanel64.SetColumnSpan(Me.lblLastRunTimeDelete, 2)
        Me.lblLastRunTimeDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLastRunTimeDelete.Location = New System.Drawing.Point(138, 137)
        Me.lblLastRunTimeDelete.Name = "lblLastRunTimeDelete"
        Me.lblLastRunTimeDelete.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblLastRunTimeDelete.Size = New System.Drawing.Size(243, 20)
        Me.lblLastRunTimeDelete.TabIndex = 6
        '
        'lblLastEndTimeDelete
        '
        Me.TableLayoutPanel64.SetColumnSpan(Me.lblLastEndTimeDelete, 2)
        Me.lblLastEndTimeDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLastEndTimeDelete.Location = New System.Drawing.Point(138, 163)
        Me.lblLastEndTimeDelete.Name = "lblLastEndTimeDelete"
        Me.lblLastEndTimeDelete.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblLastEndTimeDelete.Size = New System.Drawing.Size(243, 20)
        Me.lblLastEndTimeDelete.TabIndex = 7
        '
        'lblOwnerDelete
        '
        Me.lblOwnerDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblOwnerDelete.Location = New System.Drawing.Point(138, 3)
        Me.lblOwnerDelete.Name = "lblOwnerDelete"
        Me.lblOwnerDelete.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblOwnerDelete.Size = New System.Drawing.Size(173, 24)
        Me.lblOwnerDelete.TabIndex = 9
        '
        'ceActiveDelete
        '
        Me.ceActiveDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceActiveDelete.Location = New System.Drawing.Point(140, 33)
        Me.ceActiveDelete.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceActiveDelete.Name = "ceActiveDelete"
        Me.ceActiveDelete.Properties.Caption = ""
        Me.ceActiveDelete.Size = New System.Drawing.Size(171, 20)
        Me.ceActiveDelete.TabIndex = 10
        Me.ceActiveDelete.Tag = "NB_Delete"
        '
        'deSchNxtStartTimeDelete
        '
        Me.deSchNxtStartTimeDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.deSchNxtStartTimeDelete.EditValue = New Date(2016, 9, 14, 12, 41, 50, 900)
        Me.deSchNxtStartTimeDelete.Enabled = False
        Me.deSchNxtStartTimeDelete.Location = New System.Drawing.Point(138, 85)
        Me.deSchNxtStartTimeDelete.Name = "deSchNxtStartTimeDelete"
        Me.deSchNxtStartTimeDelete.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deSchNxtStartTimeDelete.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deSchNxtStartTimeDelete.Size = New System.Drawing.Size(173, 20)
        Me.deSchNxtStartTimeDelete.TabIndex = 11
        Me.deSchNxtStartTimeDelete.Tag = "NB_Delete"
        '
        'cmbSchRptIntervalDelete
        '
        Me.cmbSchRptIntervalDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbSchRptIntervalDelete.EditValue = "WEEKLY"
        Me.cmbSchRptIntervalDelete.Enabled = False
        Me.cmbSchRptIntervalDelete.Location = New System.Drawing.Point(138, 111)
        Me.cmbSchRptIntervalDelete.Name = "cmbSchRptIntervalDelete"
        Me.cmbSchRptIntervalDelete.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbSchRptIntervalDelete.Properties.Items.AddRange(New Object() {"DAILY", "WEEKLY"})
        Me.cmbSchRptIntervalDelete.Size = New System.Drawing.Size(173, 20)
        Me.cmbSchRptIntervalDelete.TabIndex = 12
        Me.cmbSchRptIntervalDelete.Tag = "NB_Delete"
        '
        'btnRunNowDelete
        '
        Me.btnRunNowDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRunNowDelete.Location = New System.Drawing.Point(317, 3)
        Me.btnRunNowDelete.Name = "btnRunNowDelete"
        Me.btnRunNowDelete.Size = New System.Drawing.Size(64, 24)
        Me.btnRunNowDelete.TabIndex = 8
        Me.btnRunNowDelete.Text = "Run Now"
        '
        'ceIsPublicDelete
        '
        Me.ceIsPublicDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceIsPublicDelete.Location = New System.Drawing.Point(140, 59)
        Me.ceIsPublicDelete.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceIsPublicDelete.Name = "ceIsPublicDelete"
        Me.ceIsPublicDelete.Properties.Caption = ""
        Me.ceIsPublicDelete.Size = New System.Drawing.Size(171, 20)
        Me.ceIsPublicDelete.TabIndex = 13
        Me.ceIsPublicDelete.Tag = "NB_Delete"
        '
        'LabelControl61
        '
        Me.LabelControl61.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl61.Location = New System.Drawing.Point(3, 59)
        Me.LabelControl61.Name = "LabelControl61"
        Me.LabelControl61.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl61.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl61.TabIndex = 14
        Me.LabelControl61.Text = "Is Public"
        '
        'gcDeleteCampaigns
        '
        Me.gcDeleteCampaigns.AllowDrop = True
        Me.gcDeleteCampaigns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcDeleteCampaigns.Location = New System.Drawing.Point(2, 29)
        Me.gcDeleteCampaigns.MainView = Me.gvDeleteCampaigns
        Me.gcDeleteCampaigns.Margin = New System.Windows.Forms.Padding(2)
        Me.gcDeleteCampaigns.Name = "gcDeleteCampaigns"
        Me.gcDeleteCampaigns.Size = New System.Drawing.Size(388, 124)
        Me.gcDeleteCampaigns.TabIndex = 5
        Me.gcDeleteCampaigns.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvDeleteCampaigns, Me.GridView27})
        '
        'gvDeleteCampaigns
        '
        Me.gvDeleteCampaigns.ActiveFilterEnabled = False
        Me.gvDeleteCampaigns.GridControl = Me.gcDeleteCampaigns
        Me.gvDeleteCampaigns.Name = "gvDeleteCampaigns"
        Me.gvDeleteCampaigns.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvDeleteCampaigns.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvDeleteCampaigns.OptionsBehavior.Editable = False
        Me.gvDeleteCampaigns.OptionsBehavior.ReadOnly = True
        Me.gvDeleteCampaigns.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvDeleteCampaigns.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvDeleteCampaigns.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvDeleteCampaigns.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvDeleteCampaigns.OptionsSelection.MultiSelect = True
        Me.gvDeleteCampaigns.OptionsView.ShowGroupPanel = False
        '
        'GridView27
        '
        Me.GridView27.GridControl = Me.gcDeleteCampaigns
        Me.GridView27.Name = "GridView27"
        '
        'TableLayoutPanel65
        '
        Me.TableLayoutPanel65.ColumnCount = 4
        Me.TableLayoutPanel65.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel65.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel65.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel65.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel65.Controls.Add(Me.btnDeleteRefresh, 0, 0)
        Me.TableLayoutPanel65.Controls.Add(Me.txtSearchDelete, 0, 0)
        Me.TableLayoutPanel65.Controls.Add(Me.btnCloneDelete, 2, 0)
        Me.TableLayoutPanel65.Controls.Add(Me.btnDeleteDelete, 3, 0)
        Me.TableLayoutPanel65.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel65.Location = New System.Drawing.Point(1, 1)
        Me.TableLayoutPanel65.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel65.Name = "TableLayoutPanel65"
        Me.TableLayoutPanel65.RowCount = 1
        Me.TableLayoutPanel65.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel65.Size = New System.Drawing.Size(390, 25)
        Me.TableLayoutPanel65.TabIndex = 6
        '
        'btnDeleteRefresh
        '
        Me.btnDeleteRefresh.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDeleteRefresh.Location = New System.Drawing.Point(227, 2)
        Me.btnDeleteRefresh.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDeleteRefresh.Name = "btnDeleteRefresh"
        Me.btnDeleteRefresh.Size = New System.Drawing.Size(51, 21)
        Me.btnDeleteRefresh.TabIndex = 7
        Me.btnDeleteRefresh.Tag = "NB_Delete"
        Me.btnDeleteRefresh.Text = "Refresh"
        '
        'txtSearchDelete
        '
        Me.txtSearchDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchDelete.Location = New System.Drawing.Point(2, 2)
        Me.txtSearchDelete.Margin = New System.Windows.Forms.Padding(2)
        Me.txtSearchDelete.Name = "txtSearchDelete"
        Me.txtSearchDelete.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSearchDelete.Properties.NullValuePrompt = "Search..."
        Me.txtSearchDelete.Size = New System.Drawing.Size(221, 20)
        Me.txtSearchDelete.TabIndex = 3
        '
        'btnCloneDelete
        '
        Me.btnCloneDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCloneDelete.Location = New System.Drawing.Point(282, 2)
        Me.btnCloneDelete.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCloneDelete.Name = "btnCloneDelete"
        Me.btnCloneDelete.Size = New System.Drawing.Size(51, 21)
        Me.btnCloneDelete.TabIndex = 5
        Me.btnCloneDelete.Text = "Clone"
        '
        'btnDeleteDelete
        '
        Me.btnDeleteDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDeleteDelete.Location = New System.Drawing.Point(337, 2)
        Me.btnDeleteDelete.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDeleteDelete.Name = "btnDeleteDelete"
        Me.btnDeleteDelete.Size = New System.Drawing.Size(51, 21)
        Me.btnDeleteDelete.TabIndex = 6
        Me.btnDeleteDelete.Text = "Delete"
        '
        'TableLayoutPanel66
        '
        Me.TableLayoutPanel66.ColumnCount = 1
        Me.TableLayoutPanel66.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel66.Controls.Add(Me.grpCampSummDelete, 0, 0)
        Me.TableLayoutPanel66.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel66.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel66.Name = "TableLayoutPanel66"
        Me.TableLayoutPanel66.RowCount = 2
        Me.TableLayoutPanel66.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel66.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3.0!))
        Me.TableLayoutPanel66.Size = New System.Drawing.Size(814, 400)
        Me.TableLayoutPanel66.TabIndex = 0
        '
        'grpCampSummDelete
        '
        Me.grpCampSummDelete.Controls.Add(Me.TableLayoutPanel67)
        Me.grpCampSummDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpCampSummDelete.Location = New System.Drawing.Point(3, 3)
        Me.grpCampSummDelete.Name = "grpCampSummDelete"
        Me.grpCampSummDelete.Size = New System.Drawing.Size(808, 391)
        Me.grpCampSummDelete.TabIndex = 1
        Me.grpCampSummDelete.Text = "Campaign Result Summary"
        '
        'TableLayoutPanel67
        '
        Me.TableLayoutPanel67.ColumnCount = 1
        Me.TableLayoutPanel67.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel67.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel67.Controls.Add(Me.TableLayoutPanel68, 0, 0)
        Me.TableLayoutPanel67.Controls.Add(Me.XtraTabControl2, 0, 1)
        Me.TableLayoutPanel67.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel67.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel67.Name = "TableLayoutPanel67"
        Me.TableLayoutPanel67.RowCount = 2
        Me.TableLayoutPanel67.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel67.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel67.Size = New System.Drawing.Size(804, 366)
        Me.TableLayoutPanel67.TabIndex = 3
        '
        'TableLayoutPanel68
        '
        Me.TableLayoutPanel68.ColumnCount = 3
        Me.TableLayoutPanel68.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel68.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220.0!))
        Me.TableLayoutPanel68.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel68.Controls.Add(Me.cmbResultSetIDDelete, 1, 0)
        Me.TableLayoutPanel68.Controls.Add(Me.LabelControl62, 0, 0)
        Me.TableLayoutPanel68.Controls.Add(Me.btnDeleteResultSetDelete, 2, 0)
        Me.TableLayoutPanel68.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel68.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel68.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel68.Name = "TableLayoutPanel68"
        Me.TableLayoutPanel68.RowCount = 1
        Me.TableLayoutPanel68.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel68.Size = New System.Drawing.Size(800, 26)
        Me.TableLayoutPanel68.TabIndex = 1
        '
        'cmbResultSetIDDelete
        '
        Me.cmbResultSetIDDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbResultSetIDDelete.EditValue = ""
        Me.cmbResultSetIDDelete.Location = New System.Drawing.Point(83, 3)
        Me.cmbResultSetIDDelete.Name = "cmbResultSetIDDelete"
        Me.cmbResultSetIDDelete.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbResultSetIDDelete.Size = New System.Drawing.Size(214, 20)
        Me.cmbResultSetIDDelete.TabIndex = 13
        '
        'LabelControl62
        '
        Me.LabelControl62.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl62.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl62.Name = "LabelControl62"
        Me.LabelControl62.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl62.Size = New System.Drawing.Size(74, 20)
        Me.LabelControl62.TabIndex = 3
        Me.LabelControl62.Text = "Result Set ID"
        '
        'btnDeleteResultSetDelete
        '
        Me.btnDeleteResultSetDelete.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDeleteResultSetDelete.Location = New System.Drawing.Point(302, 2)
        Me.btnDeleteResultSetDelete.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDeleteResultSetDelete.Name = "btnDeleteResultSetDelete"
        Me.btnDeleteResultSetDelete.Size = New System.Drawing.Size(65, 22)
        Me.btnDeleteResultSetDelete.TabIndex = 14
        Me.btnDeleteResultSetDelete.Tag = "NB_Copy"
        Me.btnDeleteResultSetDelete.Text = "Delete"
        '
        'XtraTabControl2
        '
        Me.XtraTabControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl2.Location = New System.Drawing.Point(3, 33)
        Me.XtraTabControl2.Name = "XtraTabControl2"
        Me.XtraTabControl2.SelectedTabPage = Me.XtraTabPage3
        Me.XtraTabControl2.Size = New System.Drawing.Size(798, 330)
        Me.XtraTabControl2.TabIndex = 2
        Me.XtraTabControl2.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage3, Me.XtraTabPage4})
        '
        'XtraTabPage3
        '
        Me.XtraTabPage3.Controls.Add(Me.gcCampSummDelete)
        Me.XtraTabPage3.Name = "XtraTabPage3"
        Me.XtraTabPage3.Size = New System.Drawing.Size(796, 305)
        Me.XtraTabPage3.Text = "Summary"
        '
        'gcCampSummDelete
        '
        Me.gcCampSummDelete.AllowDrop = True
        Me.gcCampSummDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcCampSummDelete.Location = New System.Drawing.Point(0, 0)
        Me.gcCampSummDelete.MainView = Me.gvCampSummDelete
        Me.gcCampSummDelete.Name = "gcCampSummDelete"
        Me.gcCampSummDelete.Size = New System.Drawing.Size(796, 305)
        Me.gcCampSummDelete.TabIndex = 2
        Me.gcCampSummDelete.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvCampSummDelete, Me.GridView31})
        '
        'gvCampSummDelete
        '
        Me.gvCampSummDelete.ActiveFilterEnabled = False
        Me.gvCampSummDelete.GridControl = Me.gcCampSummDelete
        Me.gvCampSummDelete.Name = "gvCampSummDelete"
        Me.gvCampSummDelete.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampSummDelete.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampSummDelete.OptionsBehavior.Editable = False
        Me.gvCampSummDelete.OptionsBehavior.ReadOnly = True
        Me.gvCampSummDelete.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampSummDelete.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampSummDelete.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampSummDelete.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvCampSummDelete.OptionsSelection.MultiSelect = True
        Me.gvCampSummDelete.OptionsView.ShowGroupPanel = False
        '
        'GridView31
        '
        Me.GridView31.GridControl = Me.gcCampSummDelete
        Me.GridView31.Name = "GridView31"
        '
        'XtraTabPage4
        '
        Me.XtraTabPage4.Controls.Add(Me.TableLayoutPanel69)
        Me.XtraTabPage4.Name = "XtraTabPage4"
        Me.XtraTabPage4.Size = New System.Drawing.Size(796, 305)
        Me.XtraTabPage4.Text = "Data"
        '
        'TableLayoutPanel69
        '
        Me.TableLayoutPanel69.ColumnCount = 1
        Me.TableLayoutPanel69.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel69.Controls.Add(Me.gcCampDataDelete, 0, 1)
        Me.TableLayoutPanel69.Controls.Add(Me.TableLayoutPanel70, 0, 0)
        Me.TableLayoutPanel69.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel69.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel69.Name = "TableLayoutPanel69"
        Me.TableLayoutPanel69.RowCount = 2
        Me.TableLayoutPanel69.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel69.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel69.Size = New System.Drawing.Size(796, 305)
        Me.TableLayoutPanel69.TabIndex = 1
        '
        'gcCampDataDelete
        '
        Me.gcCampDataDelete.AllowDrop = True
        Me.gcCampDataDelete.ContextMenuStrip = Me.cmsMapNB
        Me.gcCampDataDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcCampDataDelete.Location = New System.Drawing.Point(3, 38)
        Me.gcCampDataDelete.MainView = Me.gvCampDataDelete
        Me.gcCampDataDelete.Name = "gcCampDataDelete"
        Me.gcCampDataDelete.Size = New System.Drawing.Size(790, 264)
        Me.gcCampDataDelete.TabIndex = 4
        Me.gcCampDataDelete.Tag = "NBDelete"
        Me.gcCampDataDelete.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvCampDataDelete, Me.GridView33})
        '
        'gvCampDataDelete
        '
        Me.gvCampDataDelete.ActiveFilterEnabled = False
        Me.gvCampDataDelete.GridControl = Me.gcCampDataDelete
        Me.gvCampDataDelete.Name = "gvCampDataDelete"
        Me.gvCampDataDelete.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampDataDelete.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampDataDelete.OptionsBehavior.Editable = False
        Me.gvCampDataDelete.OptionsBehavior.ReadOnly = True
        Me.gvCampDataDelete.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampDataDelete.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampDataDelete.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampDataDelete.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.gvCampDataDelete.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvCampDataDelete.OptionsSelection.MultiSelect = True
        Me.gvCampDataDelete.OptionsView.ShowGroupPanel = False
        '
        'GridView33
        '
        Me.GridView33.GridControl = Me.gcCampDataDelete
        Me.GridView33.Name = "GridView33"
        '
        'TableLayoutPanel70
        '
        Me.TableLayoutPanel70.ColumnCount = 3
        Me.TableLayoutPanel70.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel70.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel70.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel70.Controls.Add(Me.btnDataLoadGridDelete, 0, 0)
        Me.TableLayoutPanel70.Controls.Add(Me.btnDataAllCsvDelete, 1, 0)
        Me.TableLayoutPanel70.Controls.Add(Me.lblDataRowCountDelete, 2, 0)
        Me.TableLayoutPanel70.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel70.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel70.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel70.Name = "TableLayoutPanel70"
        Me.TableLayoutPanel70.RowCount = 1
        Me.TableLayoutPanel70.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel70.Size = New System.Drawing.Size(792, 31)
        Me.TableLayoutPanel70.TabIndex = 0
        '
        'btnDataLoadGridDelete
        '
        Me.btnDataLoadGridDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDataLoadGridDelete.Location = New System.Drawing.Point(3, 3)
        Me.btnDataLoadGridDelete.Name = "btnDataLoadGridDelete"
        Me.btnDataLoadGridDelete.Size = New System.Drawing.Size(94, 25)
        Me.btnDataLoadGridDelete.TabIndex = 0
        Me.btnDataLoadGridDelete.Text = "Load To Grid"
        '
        'btnDataAllCsvDelete
        '
        Me.btnDataAllCsvDelete.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDataAllCsvDelete.Location = New System.Drawing.Point(103, 3)
        Me.btnDataAllCsvDelete.Name = "btnDataAllCsvDelete"
        Me.btnDataAllCsvDelete.Size = New System.Drawing.Size(94, 25)
        Me.btnDataAllCsvDelete.TabIndex = 1
        Me.btnDataAllCsvDelete.Text = "All Data To CSV"
        '
        'lblDataRowCountDelete
        '
        Me.lblDataRowCountDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDataRowCountDelete.Location = New System.Drawing.Point(203, 3)
        Me.lblDataRowCountDelete.Name = "lblDataRowCountDelete"
        Me.lblDataRowCountDelete.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblDataRowCountDelete.Size = New System.Drawing.Size(586, 25)
        Me.lblDataRowCountDelete.TabIndex = 3
        Me.lblDataRowCountDelete.Text = "Count of Records: "
        Me.lblDataRowCountDelete.Visible = False
        '
        'grpConfigSummDelete
        '
        Me.grpConfigSummDelete.Controls.Add(Me.TableLayoutPanel71)
        Me.grpConfigSummDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpConfigSummDelete.Location = New System.Drawing.Point(0, 0)
        Me.grpConfigSummDelete.Name = "grpConfigSummDelete"
        Me.grpConfigSummDelete.Size = New System.Drawing.Size(1220, 278)
        Me.grpConfigSummDelete.TabIndex = 0
        Me.grpConfigSummDelete.Text = "Configuration Summary"
        '
        'TableLayoutPanel71
        '
        Me.TableLayoutPanel71.ColumnCount = 2
        Me.TableLayoutPanel71.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel71.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350.0!))
        Me.TableLayoutPanel71.Controls.Add(Me.gcConfigSummDelete, 0, 0)
        Me.TableLayoutPanel71.Controls.Add(Me.TableLayoutPanel72, 1, 0)
        Me.TableLayoutPanel71.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel71.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel71.Name = "TableLayoutPanel71"
        Me.TableLayoutPanel71.RowCount = 1
        Me.TableLayoutPanel71.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel71.Size = New System.Drawing.Size(1216, 253)
        Me.TableLayoutPanel71.TabIndex = 1
        '
        'gcConfigSummDelete
        '
        Me.gcConfigSummDelete.AllowDrop = True
        Me.gcConfigSummDelete.ContextMenuStrip = Me.cmsConfigurationSummary
        Me.gcConfigSummDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcConfigSummDelete.Location = New System.Drawing.Point(3, 3)
        Me.gcConfigSummDelete.MainView = Me.gvConfigSummDelete
        Me.gcConfigSummDelete.Name = "gcConfigSummDelete"
        Me.gcConfigSummDelete.Size = New System.Drawing.Size(860, 247)
        Me.gcConfigSummDelete.TabIndex = 3
        Me.gcConfigSummDelete.Tag = "NB_Delete"
        Me.gcConfigSummDelete.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvConfigSummDelete, Me.GridView35})
        '
        'gvConfigSummDelete
        '
        Me.gvConfigSummDelete.ActiveFilterEnabled = False
        Me.gvConfigSummDelete.GridControl = Me.gcConfigSummDelete
        Me.gvConfigSummDelete.Name = "gvConfigSummDelete"
        Me.gvConfigSummDelete.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvConfigSummDelete.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvConfigSummDelete.OptionsBehavior.Editable = False
        Me.gvConfigSummDelete.OptionsBehavior.ReadOnly = True
        Me.gvConfigSummDelete.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvConfigSummDelete.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvConfigSummDelete.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvConfigSummDelete.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvConfigSummDelete.OptionsSelection.MultiSelect = True
        Me.gvConfigSummDelete.OptionsView.ShowGroupPanel = False
        '
        'GridView35
        '
        Me.GridView35.GridControl = Me.gcConfigSummDelete
        Me.GridView35.Name = "GridView35"
        '
        'TableLayoutPanel72
        '
        Me.TableLayoutPanel72.ColumnCount = 1
        Me.TableLayoutPanel72.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel72.Controls.Add(Me.grpLayerPropDelete, 0, 0)
        Me.TableLayoutPanel72.Controls.Add(Me.btnListMngrDelete, 0, 1)
        Me.TableLayoutPanel72.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel72.Location = New System.Drawing.Point(869, 3)
        Me.TableLayoutPanel72.Name = "TableLayoutPanel72"
        Me.TableLayoutPanel72.RowCount = 2
        Me.TableLayoutPanel72.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel72.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel72.Size = New System.Drawing.Size(344, 247)
        Me.TableLayoutPanel72.TabIndex = 0
        '
        'grpLayerPropDelete
        '
        Me.grpLayerPropDelete.Controls.Add(Me.TableLayoutPanel73)
        Me.grpLayerPropDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpLayerPropDelete.Location = New System.Drawing.Point(3, 3)
        Me.grpLayerPropDelete.Name = "grpLayerPropDelete"
        Me.grpLayerPropDelete.Size = New System.Drawing.Size(338, 209)
        Me.grpLayerPropDelete.TabIndex = 1
        Me.grpLayerPropDelete.Text = "Layer Properties"
        '
        'TableLayoutPanel73
        '
        Me.TableLayoutPanel73.ColumnCount = 1
        Me.TableLayoutPanel73.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel73.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel73.Controls.Add(Me.ceApplyConfigAllDelete, 0, 1)
        Me.TableLayoutPanel73.Controls.Add(Me.layerPropGridDelete, 0, 0)
        Me.TableLayoutPanel73.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel73.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel73.Name = "TableLayoutPanel73"
        Me.TableLayoutPanel73.RowCount = 2
        Me.TableLayoutPanel73.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel73.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel73.Size = New System.Drawing.Size(334, 184)
        Me.TableLayoutPanel73.TabIndex = 1
        '
        'ceApplyConfigAllDelete
        '
        Me.ceApplyConfigAllDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceApplyConfigAllDelete.Location = New System.Drawing.Point(5, 162)
        Me.ceApplyConfigAllDelete.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceApplyConfigAllDelete.Name = "ceApplyConfigAllDelete"
        Me.ceApplyConfigAllDelete.Properties.Caption = "Apply changes to all configuration"
        Me.ceApplyConfigAllDelete.Size = New System.Drawing.Size(326, 19)
        Me.ceApplyConfigAllDelete.TabIndex = 2
        '
        'layerPropGridDelete
        '
        Me.layerPropGridDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layerPropGridDelete.LineColor = System.Drawing.SystemColors.ControlDark
        Me.layerPropGridDelete.Location = New System.Drawing.Point(3, 3)
        Me.layerPropGridDelete.Name = "layerPropGridDelete"
        Me.layerPropGridDelete.Size = New System.Drawing.Size(328, 153)
        Me.layerPropGridDelete.TabIndex = 0
        Me.layerPropGridDelete.Tag = "NB_Delete"
        Me.layerPropGridDelete.ToolbarVisible = False
        '
        'btnListMngrDelete
        '
        Me.btnListMngrDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnListMngrDelete.Location = New System.Drawing.Point(3, 218)
        Me.btnListMngrDelete.Name = "btnListMngrDelete"
        Me.btnListMngrDelete.Size = New System.Drawing.Size(338, 26)
        Me.btnListMngrDelete.TabIndex = 1
        Me.btnListMngrDelete.Text = "List Manager"
        '
        'tpNBManual
        '
        Me.tpNBManual.Controls.Add(Me.SplitContainerControl1)
        Me.tpNBManual.Name = "tpNBManual"
        Me.tpNBManual.Size = New System.Drawing.Size(1220, 688)
        Me.tpNBManual.Text = "NB Manual"
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        '
        'SplitContainerControl1.Panel1
        '
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.SplitContainerControl4)
        Me.SplitContainerControl1.Panel1.MinSize = 300
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        '
        'SplitContainerControl1.Panel2
        '
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.grpManual)
        Me.SplitContainerControl1.Panel2.MinSize = 500
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1220, 688)
        Me.SplitContainerControl1.SplitterPosition = 396
        Me.SplitContainerControl1.TabIndex = 2
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'SplitContainerControl4
        '
        Me.SplitContainerControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl4.Horizontal = False
        Me.SplitContainerControl4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl4.Name = "SplitContainerControl4"
        '
        'SplitContainerControl4.Panel1
        '
        Me.SplitContainerControl4.Panel1.Controls.Add(Me.grpCampManual)
        Me.SplitContainerControl4.Panel1.MinSize = 300
        Me.SplitContainerControl4.Panel1.Text = "Panel1"
        '
        'SplitContainerControl4.Panel2
        '
        Me.SplitContainerControl4.Panel2.Controls.Add(Me.TableLayoutPanel26)
        Me.SplitContainerControl4.Panel2.MinSize = 200
        Me.SplitContainerControl4.Panel2.Text = "Panel2"
        Me.SplitContainerControl4.Size = New System.Drawing.Size(396, 688)
        Me.SplitContainerControl4.SplitterPosition = 442
        Me.SplitContainerControl4.TabIndex = 0
        Me.SplitContainerControl4.Text = "SplitContainerControl1"
        '
        'grpCampManual
        '
        Me.grpCampManual.Controls.Add(Me.TableLayoutPanel8)
        Me.grpCampManual.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpCampManual.Location = New System.Drawing.Point(0, 0)
        Me.grpCampManual.Name = "grpCampManual"
        Me.grpCampManual.Size = New System.Drawing.Size(396, 442)
        Me.grpCampManual.TabIndex = 0
        Me.grpCampManual.Text = "NB | Manual Campaigns"
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 1
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Controls.Add(Me.grpCampPropManual, 0, 2)
        Me.TableLayoutPanel8.Controls.Add(Me.gcCampaignManual, 0, 1)
        Me.TableLayoutPanel8.Controls.Add(Me.TableLayoutPanel25, 0, 0)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 3
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 88.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(392, 417)
        Me.TableLayoutPanel8.TabIndex = 0
        '
        'grpCampPropManual
        '
        Me.grpCampPropManual.Controls.Add(Me.TableLayoutPanel9)
        Me.grpCampPropManual.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpCampPropManual.Location = New System.Drawing.Point(2, 331)
        Me.grpCampPropManual.Margin = New System.Windows.Forms.Padding(2)
        Me.grpCampPropManual.Name = "grpCampPropManual"
        Me.grpCampPropManual.Size = New System.Drawing.Size(388, 84)
        Me.grpCampPropManual.TabIndex = 4
        Me.grpCampPropManual.Text = "Campaign Properties"
        '
        'TableLayoutPanel9
        '
        Me.TableLayoutPanel9.ColumnCount = 2
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 54.0!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel9.Controls.Add(Me.LabelControl8, 0, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.lblOwnerManual, 1, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.LabelControl42, 0, 1)
        Me.TableLayoutPanel9.Controls.Add(Me.ceIsPublicManual, 1, 1)
        Me.TableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel9.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel9.Name = "TableLayoutPanel9"
        Me.TableLayoutPanel9.RowCount = 3
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel9.Size = New System.Drawing.Size(384, 59)
        Me.TableLayoutPanel9.TabIndex = 0
        '
        'LabelControl8
        '
        Me.LabelControl8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl8.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl8.Size = New System.Drawing.Size(48, 20)
        Me.LabelControl8.TabIndex = 0
        Me.LabelControl8.Text = "Owner"
        '
        'lblOwnerManual
        '
        Me.lblOwnerManual.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblOwnerManual.Location = New System.Drawing.Point(57, 3)
        Me.lblOwnerManual.Name = "lblOwnerManual"
        Me.lblOwnerManual.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblOwnerManual.Size = New System.Drawing.Size(324, 20)
        Me.lblOwnerManual.TabIndex = 9
        '
        'LabelControl42
        '
        Me.LabelControl42.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl42.Location = New System.Drawing.Point(3, 29)
        Me.LabelControl42.Name = "LabelControl42"
        Me.LabelControl42.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl42.Size = New System.Drawing.Size(48, 20)
        Me.LabelControl42.TabIndex = 10
        Me.LabelControl42.Text = "Is Public"
        '
        'ceIsPublicManual
        '
        Me.ceIsPublicManual.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceIsPublicManual.Location = New System.Drawing.Point(59, 29)
        Me.ceIsPublicManual.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceIsPublicManual.Name = "ceIsPublicManual"
        Me.ceIsPublicManual.Properties.Caption = ""
        Me.ceIsPublicManual.Size = New System.Drawing.Size(322, 20)
        Me.ceIsPublicManual.TabIndex = 11
        Me.ceIsPublicManual.Tag = "NB_Manual"
        '
        'gcCampaignManual
        '
        Me.gcCampaignManual.AllowDrop = True
        Me.gcCampaignManual.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcCampaignManual.Location = New System.Drawing.Point(2, 29)
        Me.gcCampaignManual.MainView = Me.gvCampaignManual
        Me.gcCampaignManual.Margin = New System.Windows.Forms.Padding(2)
        Me.gcCampaignManual.Name = "gcCampaignManual"
        Me.gcCampaignManual.Size = New System.Drawing.Size(388, 298)
        Me.gcCampaignManual.TabIndex = 5
        Me.gcCampaignManual.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvCampaignManual, Me.GridView19})
        '
        'gvCampaignManual
        '
        Me.gvCampaignManual.ActiveFilterEnabled = False
        Me.gvCampaignManual.GridControl = Me.gcCampaignManual
        Me.gvCampaignManual.Name = "gvCampaignManual"
        Me.gvCampaignManual.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampaignManual.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampaignManual.OptionsBehavior.Editable = False
        Me.gvCampaignManual.OptionsBehavior.ReadOnly = True
        Me.gvCampaignManual.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampaignManual.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampaignManual.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampaignManual.OptionsView.ShowGroupPanel = False
        '
        'GridView19
        '
        Me.GridView19.GridControl = Me.gcCampaignManual
        Me.GridView19.Name = "GridView19"
        '
        'TableLayoutPanel25
        '
        Me.TableLayoutPanel25.ColumnCount = 5
        Me.TableLayoutPanel25.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel25.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel25.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel25.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel25.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel25.Controls.Add(Me.txtSearchManual, 0, 0)
        Me.TableLayoutPanel25.Controls.Add(Me.btnDeleteManual, 4, 0)
        Me.TableLayoutPanel25.Controls.Add(Me.btnAddManual, 3, 0)
        Me.TableLayoutPanel25.Controls.Add(Me.btnCloneManual, 2, 0)
        Me.TableLayoutPanel25.Controls.Add(Me.btnRefreshManual, 1, 0)
        Me.TableLayoutPanel25.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel25.Location = New System.Drawing.Point(1, 1)
        Me.TableLayoutPanel25.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel25.Name = "TableLayoutPanel25"
        Me.TableLayoutPanel25.RowCount = 1
        Me.TableLayoutPanel25.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel25.Size = New System.Drawing.Size(390, 25)
        Me.TableLayoutPanel25.TabIndex = 6
        '
        'txtSearchManual
        '
        Me.txtSearchManual.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchManual.Location = New System.Drawing.Point(2, 2)
        Me.txtSearchManual.Margin = New System.Windows.Forms.Padding(2)
        Me.txtSearchManual.Name = "txtSearchManual"
        Me.txtSearchManual.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSearchManual.Properties.NullValuePrompt = "Search..."
        Me.txtSearchManual.Size = New System.Drawing.Size(176, 20)
        Me.txtSearchManual.TabIndex = 3
        '
        'btnDeleteManual
        '
        Me.btnDeleteManual.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDeleteManual.Location = New System.Drawing.Point(342, 2)
        Me.btnDeleteManual.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDeleteManual.Name = "btnDeleteManual"
        Me.btnDeleteManual.Size = New System.Drawing.Size(46, 21)
        Me.btnDeleteManual.TabIndex = 6
        Me.btnDeleteManual.Text = "Delete"
        '
        'btnAddManual
        '
        Me.btnAddManual.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAddManual.Location = New System.Drawing.Point(292, 2)
        Me.btnAddManual.Margin = New System.Windows.Forms.Padding(2)
        Me.btnAddManual.Name = "btnAddManual"
        Me.btnAddManual.Size = New System.Drawing.Size(46, 21)
        Me.btnAddManual.TabIndex = 5
        Me.btnAddManual.Text = "Add"
        '
        'btnCloneManual
        '
        Me.btnCloneManual.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCloneManual.Location = New System.Drawing.Point(242, 2)
        Me.btnCloneManual.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCloneManual.Name = "btnCloneManual"
        Me.btnCloneManual.Size = New System.Drawing.Size(46, 21)
        Me.btnCloneManual.TabIndex = 7
        Me.btnCloneManual.Text = "Clone"
        '
        'btnRefreshManual
        '
        Me.btnRefreshManual.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRefreshManual.Location = New System.Drawing.Point(182, 2)
        Me.btnRefreshManual.Margin = New System.Windows.Forms.Padding(2)
        Me.btnRefreshManual.Name = "btnRefreshManual"
        Me.btnRefreshManual.Size = New System.Drawing.Size(56, 21)
        Me.btnRefreshManual.TabIndex = 8
        Me.btnRefreshManual.Tag = "NB_Manual"
        Me.btnRefreshManual.Text = "Refresh"
        '
        'TableLayoutPanel26
        '
        Me.TableLayoutPanel26.ColumnCount = 1
        Me.TableLayoutPanel26.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel26.Controls.Add(Me.grpCampSummManual, 0, 0)
        Me.TableLayoutPanel26.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel26.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel26.Name = "TableLayoutPanel26"
        Me.TableLayoutPanel26.RowCount = 2
        Me.TableLayoutPanel26.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel26.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3.0!))
        Me.TableLayoutPanel26.Size = New System.Drawing.Size(396, 236)
        Me.TableLayoutPanel26.TabIndex = 0
        '
        'grpCampSummManual
        '
        Me.grpCampSummManual.Controls.Add(Me.TableLayoutPanel32)
        Me.grpCampSummManual.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpCampSummManual.Location = New System.Drawing.Point(3, 3)
        Me.grpCampSummManual.Name = "grpCampSummManual"
        Me.grpCampSummManual.Size = New System.Drawing.Size(390, 227)
        Me.grpCampSummManual.TabIndex = 1
        Me.grpCampSummManual.Text = "Campaign Result Summary"
        '
        'TableLayoutPanel32
        '
        Me.TableLayoutPanel32.ColumnCount = 2
        Me.TableLayoutPanel32.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel32.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel32.Controls.Add(Me.cmbManualResultSetID, 1, 0)
        Me.TableLayoutPanel32.Controls.Add(Me.LabelControl21, 0, 0)
        Me.TableLayoutPanel32.Controls.Add(Me.gcCampSummManual, 0, 1)
        Me.TableLayoutPanel32.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel32.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel32.Name = "TableLayoutPanel32"
        Me.TableLayoutPanel32.RowCount = 2
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel32.Size = New System.Drawing.Size(386, 202)
        Me.TableLayoutPanel32.TabIndex = 1
        '
        'cmbManualResultSetID
        '
        Me.cmbManualResultSetID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbManualResultSetID.EditValue = ""
        Me.cmbManualResultSetID.Location = New System.Drawing.Point(103, 3)
        Me.cmbManualResultSetID.Name = "cmbManualResultSetID"
        Me.cmbManualResultSetID.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbManualResultSetID.Properties.Items.AddRange(New Object() {"DAILY", "WEEKLY"})
        Me.cmbManualResultSetID.Size = New System.Drawing.Size(280, 20)
        Me.cmbManualResultSetID.TabIndex = 13
        '
        'LabelControl21
        '
        Me.LabelControl21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl21.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl21.Name = "LabelControl21"
        Me.LabelControl21.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl21.Size = New System.Drawing.Size(94, 20)
        Me.LabelControl21.TabIndex = 3
        Me.LabelControl21.Text = "Result Set ID"
        '
        'gcCampSummManual
        '
        Me.gcCampSummManual.AllowDrop = True
        Me.TableLayoutPanel32.SetColumnSpan(Me.gcCampSummManual, 2)
        Me.gcCampSummManual.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcCampSummManual.Location = New System.Drawing.Point(3, 29)
        Me.gcCampSummManual.MainView = Me.gvCampSummManual
        Me.gcCampSummManual.Name = "gcCampSummManual"
        Me.gcCampSummManual.Size = New System.Drawing.Size(380, 170)
        Me.gcCampSummManual.TabIndex = 2
        Me.gcCampSummManual.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvCampSummManual, Me.GridView20})
        '
        'gvCampSummManual
        '
        Me.gvCampSummManual.ActiveFilterEnabled = False
        Me.gvCampSummManual.GridControl = Me.gcCampSummManual
        Me.gvCampSummManual.Name = "gvCampSummManual"
        Me.gvCampSummManual.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampSummManual.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampSummManual.OptionsBehavior.Editable = False
        Me.gvCampSummManual.OptionsBehavior.ReadOnly = True
        Me.gvCampSummManual.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampSummManual.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampSummManual.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampSummManual.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvCampSummManual.OptionsSelection.MultiSelect = True
        Me.gvCampSummManual.OptionsView.ShowGroupPanel = False
        '
        'GridView20
        '
        Me.GridView20.GridControl = Me.gcCampSummManual
        Me.GridView20.Name = "GridView20"
        '
        'grpManual
        '
        Me.grpManual.Controls.Add(Me.TableLayoutPanel30)
        Me.grpManual.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpManual.Location = New System.Drawing.Point(0, 0)
        Me.grpManual.Name = "grpManual"
        Me.grpManual.Size = New System.Drawing.Size(814, 688)
        Me.grpManual.TabIndex = 1
        Me.grpManual.Text = "Manual NB List"
        '
        'TableLayoutPanel30
        '
        Me.TableLayoutPanel30.ColumnCount = 1
        Me.TableLayoutPanel30.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel30.Controls.Add(Me.gcManual, 0, 4)
        Me.TableLayoutPanel30.Controls.Add(Me.LabelControl33, 0, 2)
        Me.TableLayoutPanel30.Controls.Add(Me.LabelControl34, 0, 1)
        Me.TableLayoutPanel30.Controls.Add(Me.LabelControl35, 0, 0)
        Me.TableLayoutPanel30.Controls.Add(Me.TableLayoutPanel31, 0, 3)
        Me.TableLayoutPanel30.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel30.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel30.Name = "TableLayoutPanel30"
        Me.TableLayoutPanel30.RowCount = 5
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel30.Size = New System.Drawing.Size(810, 663)
        Me.TableLayoutPanel30.TabIndex = 0
        '
        'gcManual
        '
        Me.gcManual.AllowDrop = True
        Me.gcManual.ContextMenuStrip = Me.cmManualPaste
        Me.gcManual.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcManual.Location = New System.Drawing.Point(2, 97)
        Me.gcManual.MainView = Me.gvManual
        Me.gcManual.Margin = New System.Windows.Forms.Padding(2)
        Me.gcManual.Name = "gcManual"
        Me.gcManual.Size = New System.Drawing.Size(806, 564)
        Me.gcManual.TabIndex = 11
        Me.gcManual.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvManual, Me.GridView23})
        '
        'cmManualPaste
        '
        Me.cmManualPaste.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.cmManualPaste.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiTagPastePaste, Me.tsmi_Manual_DeleteRows})
        Me.cmManualPaste.Name = "cm_TagManagement"
        Me.cmManualPaste.Size = New System.Drawing.Size(194, 48)
        '
        'tsmiTagPastePaste
        '
        Me.tsmiTagPastePaste.Name = "tsmiTagPastePaste"
        Me.tsmiTagPastePaste.Size = New System.Drawing.Size(193, 22)
        Me.tsmiTagPastePaste.Text = "Paste From Clipboard"
        '
        'tsmi_Manual_DeleteRows
        '
        Me.tsmi_Manual_DeleteRows.Name = "tsmi_Manual_DeleteRows"
        Me.tsmi_Manual_DeleteRows.Size = New System.Drawing.Size(193, 22)
        Me.tsmi_Manual_DeleteRows.Text = "Delete Selected Row(s)"
        '
        'gvManual
        '
        Me.gvManual.ActiveFilterEnabled = False
        Me.gvManual.GridControl = Me.gcManual
        Me.gvManual.Name = "gvManual"
        Me.gvManual.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvManual.OptionsBehavior.Editable = False
        Me.gvManual.OptionsBehavior.ReadOnly = True
        Me.gvManual.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvManual.OptionsClipboard.AllowCsvFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvManual.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvManual.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvManual.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Append
        Me.gvManual.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.gvManual.OptionsSelection.MultiSelect = True
        Me.gvManual.OptionsView.ShowGroupPanel = False
        '
        'GridView23
        '
        Me.GridView23.GridControl = Me.gcManual
        Me.GridView23.Name = "GridView23"
        '
        'LabelControl33
        '
        Me.LabelControl33.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LabelControl33.Appearance.Options.UseImageAlign = True
        Me.LabelControl33.Appearance.Options.UseTextOptions = True
        Me.LabelControl33.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl33.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.LabelControl33.Location = New System.Drawing.Point(3, 43)
        Me.LabelControl33.Name = "LabelControl33"
        Me.LabelControl33.Padding = New System.Windows.Forms.Padding(16, 0, 0, 0)
        Me.LabelControl33.Size = New System.Drawing.Size(804, 14)
        Me.LabelControl33.TabIndex = 10
        Me.LabelControl33.Text = "<S_CELLNAME>,<S_IOS_TECH>,<T_CELLNAME>,<T_IOS_TECH>,<DeleteFlag>,<ReverseFlag>,<H" &
    "ighPrioNBFlag>"
        '
        'LabelControl34
        '
        Me.LabelControl34.Appearance.Image = CType(resources.GetObject("LabelControl34.Appearance.Image"), System.Drawing.Image)
        Me.LabelControl34.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LabelControl34.Appearance.Options.UseImage = True
        Me.LabelControl34.Appearance.Options.UseImageAlign = True
        Me.LabelControl34.Appearance.Options.UseTextOptions = True
        Me.LabelControl34.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl34.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.LabelControl34.Location = New System.Drawing.Point(3, 23)
        Me.LabelControl34.Name = "LabelControl34"
        Me.LabelControl34.Size = New System.Drawing.Size(804, 14)
        Me.LabelControl34.TabIndex = 9
        Me.LabelControl34.Text = "Mandatory Fields:"
        '
        'LabelControl35
        '
        Me.LabelControl35.Appearance.Image = CType(resources.GetObject("LabelControl35.Appearance.Image"), System.Drawing.Image)
        Me.LabelControl35.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LabelControl35.Appearance.Options.UseImage = True
        Me.LabelControl35.Appearance.Options.UseImageAlign = True
        Me.LabelControl35.Appearance.Options.UseTextOptions = True
        Me.LabelControl35.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl35.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.LabelControl35.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl35.Name = "LabelControl35"
        Me.LabelControl35.Size = New System.Drawing.Size(804, 14)
        Me.LabelControl35.TabIndex = 8
        Me.LabelControl35.Text = "Copy Paste to Grid"
        '
        'TableLayoutPanel31
        '
        Me.TableLayoutPanel31.ColumnCount = 2
        Me.TableLayoutPanel31.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel31.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 169.0!))
        Me.TableLayoutPanel31.Controls.Add(Me.lblRecordsCountManual, 1, 0)
        Me.TableLayoutPanel31.Controls.Add(Me.btnCommitManual, 0, 0)
        Me.TableLayoutPanel31.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel31.Location = New System.Drawing.Point(2, 62)
        Me.TableLayoutPanel31.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel31.Name = "TableLayoutPanel31"
        Me.TableLayoutPanel31.RowCount = 1
        Me.TableLayoutPanel31.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel31.Size = New System.Drawing.Size(806, 31)
        Me.TableLayoutPanel31.TabIndex = 7
        '
        'lblRecordsCountManual
        '
        Me.lblRecordsCountManual.Appearance.Options.UseTextOptions = True
        Me.lblRecordsCountManual.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblRecordsCountManual.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblRecordsCountManual.Location = New System.Drawing.Point(640, 3)
        Me.lblRecordsCountManual.Name = "lblRecordsCountManual"
        Me.lblRecordsCountManual.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblRecordsCountManual.Size = New System.Drawing.Size(163, 25)
        Me.lblRecordsCountManual.TabIndex = 1
        Me.lblRecordsCountManual.Text = "Count of Records: "
        '
        'btnCommitManual
        '
        Me.btnCommitManual.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnCommitManual.Location = New System.Drawing.Point(3, 3)
        Me.btnCommitManual.Name = "btnCommitManual"
        Me.btnCommitManual.Size = New System.Drawing.Size(100, 25)
        Me.btnCommitManual.TabIndex = 0
        Me.btnCommitManual.Text = "Commit"
        '
        'tpNBAudit
        '
        Me.tpNBAudit.Controls.Add(Me.SplitContainerControl5)
        Me.tpNBAudit.Name = "tpNBAudit"
        Me.tpNBAudit.Size = New System.Drawing.Size(1220, 688)
        Me.tpNBAudit.Text = "NB Audit"
        '
        'SplitContainerControl5
        '
        Me.SplitContainerControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl5.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl5.Name = "SplitContainerControl5"
        '
        'SplitContainerControl5.Panel1
        '
        Me.SplitContainerControl5.Panel1.Controls.Add(Me.GroupControl4)
        Me.SplitContainerControl5.Panel1.MinSize = 300
        Me.SplitContainerControl5.Panel1.Text = "Panel1"
        '
        'SplitContainerControl5.Panel2
        '
        Me.SplitContainerControl5.Panel2.Controls.Add(Me.SplitContainerControl3)
        Me.SplitContainerControl5.Panel2.MinSize = 500
        Me.SplitContainerControl5.Panel2.Text = "Panel2"
        Me.SplitContainerControl5.Size = New System.Drawing.Size(1220, 688)
        Me.SplitContainerControl5.SplitterPosition = 330
        Me.SplitContainerControl5.TabIndex = 0
        Me.SplitContainerControl5.Text = "SplitContainerControl1"
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.TableLayoutPanel39)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl4.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(330, 688)
        Me.GroupControl4.TabIndex = 0
        Me.GroupControl4.Text = "NB | Audit Campaigns"
        '
        'TableLayoutPanel39
        '
        Me.TableLayoutPanel39.ColumnCount = 1
        Me.TableLayoutPanel39.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel39.Controls.Add(Me.GroupControl5, 0, 2)
        Me.TableLayoutPanel39.Controls.Add(Me.gcCampNBAudit, 0, 1)
        Me.TableLayoutPanel39.Controls.Add(Me.TableLayoutPanel41, 0, 0)
        Me.TableLayoutPanel39.Controls.Add(Me.TableLayoutPanel42, 0, 3)
        Me.TableLayoutPanel39.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel39.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel39.Name = "TableLayoutPanel39"
        Me.TableLayoutPanel39.RowCount = 4
        Me.TableLayoutPanel39.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel39.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel39.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 220.0!))
        Me.TableLayoutPanel39.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 280.0!))
        Me.TableLayoutPanel39.Size = New System.Drawing.Size(326, 663)
        Me.TableLayoutPanel39.TabIndex = 0
        '
        'GroupControl5
        '
        Me.GroupControl5.Controls.Add(Me.TableLayoutPanel40)
        Me.GroupControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl5.Location = New System.Drawing.Point(2, 165)
        Me.GroupControl5.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupControl5.Name = "GroupControl5"
        Me.GroupControl5.Size = New System.Drawing.Size(322, 216)
        Me.GroupControl5.TabIndex = 4
        Me.GroupControl5.Text = "Campaign Properties"
        '
        'TableLayoutPanel40
        '
        Me.TableLayoutPanel40.ColumnCount = 3
        Me.TableLayoutPanel40.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135.0!))
        Me.TableLayoutPanel40.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel40.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel40.Controls.Add(Me.LabelControl22, 0, 0)
        Me.TableLayoutPanel40.Controls.Add(Me.LabelControl23, 0, 1)
        Me.TableLayoutPanel40.Controls.Add(Me.LabelControl24, 0, 3)
        Me.TableLayoutPanel40.Controls.Add(Me.LabelControl25, 0, 4)
        Me.TableLayoutPanel40.Controls.Add(Me.LabelControl26, 0, 5)
        Me.TableLayoutPanel40.Controls.Add(Me.LabelControl27, 0, 6)
        Me.TableLayoutPanel40.Controls.Add(Me.lblLastRunTimeNBAudit, 1, 5)
        Me.TableLayoutPanel40.Controls.Add(Me.lblLastEndTimeNBAudit, 1, 6)
        Me.TableLayoutPanel40.Controls.Add(Me.lblOwnerNBAudit, 1, 0)
        Me.TableLayoutPanel40.Controls.Add(Me.chkActiveNBAudit, 1, 1)
        Me.TableLayoutPanel40.Controls.Add(Me.dtpStartTimeNBAudit, 1, 3)
        Me.TableLayoutPanel40.Controls.Add(Me.cmbRepeatIntervalNBAudit, 1, 4)
        Me.TableLayoutPanel40.Controls.Add(Me.btnRunNowNBAudit, 2, 0)
        Me.TableLayoutPanel40.Controls.Add(Me.ceIsPublicAudit, 1, 2)
        Me.TableLayoutPanel40.Controls.Add(Me.LabelControl43, 0, 2)
        Me.TableLayoutPanel40.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel40.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel40.Name = "TableLayoutPanel40"
        Me.TableLayoutPanel40.RowCount = 8
        Me.TableLayoutPanel40.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel40.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel40.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel40.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel40.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel40.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel40.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel40.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel40.Size = New System.Drawing.Size(318, 191)
        Me.TableLayoutPanel40.TabIndex = 0
        '
        'LabelControl22
        '
        Me.LabelControl22.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl22.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl22.Name = "LabelControl22"
        Me.LabelControl22.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl22.Size = New System.Drawing.Size(129, 24)
        Me.LabelControl22.TabIndex = 0
        Me.LabelControl22.Text = "Owner"
        '
        'LabelControl23
        '
        Me.LabelControl23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl23.Location = New System.Drawing.Point(3, 33)
        Me.LabelControl23.Name = "LabelControl23"
        Me.LabelControl23.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl23.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl23.TabIndex = 1
        Me.LabelControl23.Text = "Active"
        '
        'LabelControl24
        '
        Me.LabelControl24.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl24.Location = New System.Drawing.Point(3, 85)
        Me.LabelControl24.Name = "LabelControl24"
        Me.LabelControl24.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl24.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl24.TabIndex = 2
        Me.LabelControl24.Text = "Schedule Next Start Time"
        '
        'LabelControl25
        '
        Me.LabelControl25.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl25.Location = New System.Drawing.Point(3, 111)
        Me.LabelControl25.Name = "LabelControl25"
        Me.LabelControl25.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl25.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl25.TabIndex = 3
        Me.LabelControl25.Text = "Schedule Repeat Interval"
        '
        'LabelControl26
        '
        Me.LabelControl26.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl26.Location = New System.Drawing.Point(3, 137)
        Me.LabelControl26.Name = "LabelControl26"
        Me.LabelControl26.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl26.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl26.TabIndex = 4
        Me.LabelControl26.Text = "Last Run TIme"
        '
        'LabelControl27
        '
        Me.LabelControl27.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl27.Location = New System.Drawing.Point(3, 163)
        Me.LabelControl27.Name = "LabelControl27"
        Me.LabelControl27.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl27.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl27.TabIndex = 5
        Me.LabelControl27.Text = "Last End Time"
        '
        'lblLastRunTimeNBAudit
        '
        Me.TableLayoutPanel40.SetColumnSpan(Me.lblLastRunTimeNBAudit, 2)
        Me.lblLastRunTimeNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLastRunTimeNBAudit.Location = New System.Drawing.Point(138, 137)
        Me.lblLastRunTimeNBAudit.Name = "lblLastRunTimeNBAudit"
        Me.lblLastRunTimeNBAudit.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblLastRunTimeNBAudit.Size = New System.Drawing.Size(177, 20)
        Me.lblLastRunTimeNBAudit.TabIndex = 6
        '
        'lblLastEndTimeNBAudit
        '
        Me.TableLayoutPanel40.SetColumnSpan(Me.lblLastEndTimeNBAudit, 2)
        Me.lblLastEndTimeNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLastEndTimeNBAudit.Location = New System.Drawing.Point(138, 163)
        Me.lblLastEndTimeNBAudit.Name = "lblLastEndTimeNBAudit"
        Me.lblLastEndTimeNBAudit.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblLastEndTimeNBAudit.Size = New System.Drawing.Size(177, 20)
        Me.lblLastEndTimeNBAudit.TabIndex = 7
        '
        'lblOwnerNBAudit
        '
        Me.lblOwnerNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblOwnerNBAudit.Location = New System.Drawing.Point(138, 3)
        Me.lblOwnerNBAudit.Name = "lblOwnerNBAudit"
        Me.lblOwnerNBAudit.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblOwnerNBAudit.Size = New System.Drawing.Size(107, 24)
        Me.lblOwnerNBAudit.TabIndex = 9
        '
        'chkActiveNBAudit
        '
        Me.chkActiveNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkActiveNBAudit.Location = New System.Drawing.Point(140, 33)
        Me.chkActiveNBAudit.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.chkActiveNBAudit.Name = "chkActiveNBAudit"
        Me.chkActiveNBAudit.Properties.Caption = ""
        Me.chkActiveNBAudit.Size = New System.Drawing.Size(105, 20)
        Me.chkActiveNBAudit.TabIndex = 10
        Me.chkActiveNBAudit.Tag = "NB_Audit"
        '
        'dtpStartTimeNBAudit
        '
        Me.dtpStartTimeNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtpStartTimeNBAudit.EditValue = New Date(2016, 9, 14, 12, 41, 50, 900)
        Me.dtpStartTimeNBAudit.Enabled = False
        Me.dtpStartTimeNBAudit.Location = New System.Drawing.Point(138, 85)
        Me.dtpStartTimeNBAudit.Name = "dtpStartTimeNBAudit"
        Me.dtpStartTimeNBAudit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpStartTimeNBAudit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpStartTimeNBAudit.Size = New System.Drawing.Size(107, 20)
        Me.dtpStartTimeNBAudit.TabIndex = 11
        Me.dtpStartTimeNBAudit.Tag = "NB_Audit"
        '
        'cmbRepeatIntervalNBAudit
        '
        Me.cmbRepeatIntervalNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbRepeatIntervalNBAudit.EditValue = "WEEKLY"
        Me.cmbRepeatIntervalNBAudit.Enabled = False
        Me.cmbRepeatIntervalNBAudit.Location = New System.Drawing.Point(138, 111)
        Me.cmbRepeatIntervalNBAudit.Name = "cmbRepeatIntervalNBAudit"
        Me.cmbRepeatIntervalNBAudit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbRepeatIntervalNBAudit.Properties.Items.AddRange(New Object() {"DAILY", "WEEKLY"})
        Me.cmbRepeatIntervalNBAudit.Size = New System.Drawing.Size(107, 20)
        Me.cmbRepeatIntervalNBAudit.TabIndex = 12
        Me.cmbRepeatIntervalNBAudit.Tag = "NB_Audit"
        '
        'btnRunNowNBAudit
        '
        Me.btnRunNowNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRunNowNBAudit.Location = New System.Drawing.Point(251, 3)
        Me.btnRunNowNBAudit.Name = "btnRunNowNBAudit"
        Me.btnRunNowNBAudit.Size = New System.Drawing.Size(64, 24)
        Me.btnRunNowNBAudit.TabIndex = 8
        Me.btnRunNowNBAudit.Text = "Run Now"
        '
        'ceIsPublicAudit
        '
        Me.ceIsPublicAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceIsPublicAudit.Location = New System.Drawing.Point(140, 59)
        Me.ceIsPublicAudit.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceIsPublicAudit.Name = "ceIsPublicAudit"
        Me.ceIsPublicAudit.Properties.Caption = ""
        Me.ceIsPublicAudit.Size = New System.Drawing.Size(105, 20)
        Me.ceIsPublicAudit.TabIndex = 13
        Me.ceIsPublicAudit.Tag = "NB_Audit"
        '
        'LabelControl43
        '
        Me.LabelControl43.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl43.Location = New System.Drawing.Point(3, 59)
        Me.LabelControl43.Name = "LabelControl43"
        Me.LabelControl43.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl43.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl43.TabIndex = 14
        Me.LabelControl43.Text = "Is Public"
        '
        'gcCampNBAudit
        '
        Me.gcCampNBAudit.AllowDrop = True
        Me.gcCampNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcCampNBAudit.Location = New System.Drawing.Point(2, 29)
        Me.gcCampNBAudit.MainView = Me.gvCampNBAudit
        Me.gcCampNBAudit.Margin = New System.Windows.Forms.Padding(2)
        Me.gcCampNBAudit.Name = "gcCampNBAudit"
        Me.gcCampNBAudit.Size = New System.Drawing.Size(322, 132)
        Me.gcCampNBAudit.TabIndex = 5
        Me.gcCampNBAudit.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvCampNBAudit, Me.GridView24})
        '
        'gvCampNBAudit
        '
        Me.gvCampNBAudit.ActiveFilterEnabled = False
        Me.gvCampNBAudit.GridControl = Me.gcCampNBAudit
        Me.gvCampNBAudit.Name = "gvCampNBAudit"
        Me.gvCampNBAudit.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampNBAudit.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampNBAudit.OptionsBehavior.Editable = False
        Me.gvCampNBAudit.OptionsBehavior.ReadOnly = True
        Me.gvCampNBAudit.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampNBAudit.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampNBAudit.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampNBAudit.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvCampNBAudit.OptionsSelection.MultiSelect = True
        Me.gvCampNBAudit.OptionsView.ShowGroupPanel = False
        '
        'GridView24
        '
        Me.GridView24.GridControl = Me.gcCampNBAudit
        Me.GridView24.Name = "GridView24"
        '
        'TableLayoutPanel41
        '
        Me.TableLayoutPanel41.ColumnCount = 5
        Me.TableLayoutPanel41.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel41.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel41.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel41.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.TableLayoutPanel41.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel41.Controls.Add(Me.btnCloneCampNBAudit, 0, 0)
        Me.TableLayoutPanel41.Controls.Add(Me.BtnRefreshCampNBAudit, 0, 0)
        Me.TableLayoutPanel41.Controls.Add(Me.txtNBAuditCampSearch, 0, 0)
        Me.TableLayoutPanel41.Controls.Add(Me.BtnDeleteCampNBAudit, 4, 0)
        Me.TableLayoutPanel41.Controls.Add(Me.BtnAddCampNBAudit, 2, 0)
        Me.TableLayoutPanel41.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel41.Location = New System.Drawing.Point(1, 1)
        Me.TableLayoutPanel41.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel41.Name = "TableLayoutPanel41"
        Me.TableLayoutPanel41.RowCount = 1
        Me.TableLayoutPanel41.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel41.Size = New System.Drawing.Size(324, 25)
        Me.TableLayoutPanel41.TabIndex = 6
        '
        'btnCloneCampNBAudit
        '
        Me.btnCloneCampNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCloneCampNBAudit.Location = New System.Drawing.Point(181, 2)
        Me.btnCloneCampNBAudit.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCloneCampNBAudit.Name = "btnCloneCampNBAudit"
        Me.btnCloneCampNBAudit.Size = New System.Drawing.Size(46, 21)
        Me.btnCloneCampNBAudit.TabIndex = 8
        Me.btnCloneCampNBAudit.Text = "Clone"
        '
        'BtnRefreshCampNBAudit
        '
        Me.BtnRefreshCampNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnRefreshCampNBAudit.Location = New System.Drawing.Point(126, 2)
        Me.BtnRefreshCampNBAudit.Margin = New System.Windows.Forms.Padding(2)
        Me.BtnRefreshCampNBAudit.Name = "BtnRefreshCampNBAudit"
        Me.BtnRefreshCampNBAudit.Size = New System.Drawing.Size(51, 21)
        Me.BtnRefreshCampNBAudit.TabIndex = 7
        Me.BtnRefreshCampNBAudit.Tag = "NB_Audit"
        Me.BtnRefreshCampNBAudit.Text = "Refresh"
        '
        'txtNBAuditCampSearch
        '
        Me.txtNBAuditCampSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtNBAuditCampSearch.Location = New System.Drawing.Point(2, 2)
        Me.txtNBAuditCampSearch.Margin = New System.Windows.Forms.Padding(2)
        Me.txtNBAuditCampSearch.Name = "txtNBAuditCampSearch"
        Me.txtNBAuditCampSearch.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtNBAuditCampSearch.Properties.NullValuePrompt = "Search..."
        Me.txtNBAuditCampSearch.Size = New System.Drawing.Size(120, 20)
        Me.txtNBAuditCampSearch.TabIndex = 3
        '
        'BtnDeleteCampNBAudit
        '
        Me.BtnDeleteCampNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnDeleteCampNBAudit.Location = New System.Drawing.Point(276, 2)
        Me.BtnDeleteCampNBAudit.Margin = New System.Windows.Forms.Padding(2)
        Me.BtnDeleteCampNBAudit.Name = "BtnDeleteCampNBAudit"
        Me.BtnDeleteCampNBAudit.Size = New System.Drawing.Size(46, 21)
        Me.BtnDeleteCampNBAudit.TabIndex = 6
        Me.BtnDeleteCampNBAudit.Text = "Delete"
        '
        'BtnAddCampNBAudit
        '
        Me.BtnAddCampNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnAddCampNBAudit.Location = New System.Drawing.Point(231, 2)
        Me.BtnAddCampNBAudit.Margin = New System.Windows.Forms.Padding(2)
        Me.BtnAddCampNBAudit.Name = "BtnAddCampNBAudit"
        Me.BtnAddCampNBAudit.Size = New System.Drawing.Size(41, 21)
        Me.BtnAddCampNBAudit.TabIndex = 5
        Me.BtnAddCampNBAudit.Text = "Add"
        '
        'TableLayoutPanel42
        '
        Me.TableLayoutPanel42.ColumnCount = 1
        Me.TableLayoutPanel42.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel42.Controls.Add(Me.grpConfigGen, 0, 0)
        Me.TableLayoutPanel42.Controls.Add(Me.grpOptionalSettings, 0, 1)
        Me.TableLayoutPanel42.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel42.Location = New System.Drawing.Point(3, 386)
        Me.TableLayoutPanel42.Name = "TableLayoutPanel42"
        Me.TableLayoutPanel42.RowCount = 2
        Me.TableLayoutPanel42.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 41.32841!))
        Me.TableLayoutPanel42.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 58.67159!))
        Me.TableLayoutPanel42.Size = New System.Drawing.Size(320, 274)
        Me.TableLayoutPanel42.TabIndex = 7
        '
        'grpConfigGen
        '
        Me.grpConfigGen.Controls.Add(Me.TableLayoutPanel47)
        Me.grpConfigGen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpConfigGen.Location = New System.Drawing.Point(3, 3)
        Me.grpConfigGen.Name = "grpConfigGen"
        Me.grpConfigGen.Size = New System.Drawing.Size(314, 107)
        Me.grpConfigGen.TabIndex = 0
        Me.grpConfigGen.Text = "Config Generation"
        '
        'TableLayoutPanel47
        '
        Me.TableLayoutPanel47.ColumnCount = 3
        Me.TableLayoutPanel47.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel47.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel47.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel47.Controls.Add(Me.LabelControl36, 0, 0)
        Me.TableLayoutPanel47.Controls.Add(Me.LabelControl37, 0, 1)
        Me.TableLayoutPanel47.Controls.Add(Me.LabelControl38, 0, 2)
        Me.TableLayoutPanel47.Controls.Add(Me.cmbMMLConfigIDNBAudit, 1, 0)
        Me.TableLayoutPanel47.Controls.Add(Me.cmbTechnologyNBAudit, 1, 1)
        Me.TableLayoutPanel47.Controls.Add(Me.cmbInclusionListNBAudit, 1, 2)
        Me.TableLayoutPanel47.Controls.Add(Me.btnAddConfigNBAudit, 2, 0)
        Me.TableLayoutPanel47.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel47.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel47.Name = "TableLayoutPanel47"
        Me.TableLayoutPanel47.RowCount = 4
        Me.TableLayoutPanel47.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel47.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel47.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel47.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel47.Size = New System.Drawing.Size(310, 82)
        Me.TableLayoutPanel47.TabIndex = 0
        '
        'LabelControl36
        '
        Me.LabelControl36.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl36.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl36.Name = "LabelControl36"
        Me.LabelControl36.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl36.Size = New System.Drawing.Size(94, 19)
        Me.LabelControl36.TabIndex = 1
        Me.LabelControl36.Text = "MML ConfigID"
        '
        'LabelControl37
        '
        Me.LabelControl37.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl37.Location = New System.Drawing.Point(3, 28)
        Me.LabelControl37.Name = "LabelControl37"
        Me.LabelControl37.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl37.Size = New System.Drawing.Size(94, 19)
        Me.LabelControl37.TabIndex = 2
        Me.LabelControl37.Text = "Technology"
        '
        'LabelControl38
        '
        Me.LabelControl38.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl38.Location = New System.Drawing.Point(3, 53)
        Me.LabelControl38.Name = "LabelControl38"
        Me.LabelControl38.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl38.Size = New System.Drawing.Size(94, 19)
        Me.LabelControl38.TabIndex = 2
        Me.LabelControl38.Text = "Inclusion List"
        '
        'cmbMMLConfigIDNBAudit
        '
        Me.cmbMMLConfigIDNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbMMLConfigIDNBAudit.EditValue = ""
        Me.cmbMMLConfigIDNBAudit.Location = New System.Drawing.Point(103, 3)
        Me.cmbMMLConfigIDNBAudit.Name = "cmbMMLConfigIDNBAudit"
        Me.cmbMMLConfigIDNBAudit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbMMLConfigIDNBAudit.Size = New System.Drawing.Size(154, 20)
        Me.cmbMMLConfigIDNBAudit.TabIndex = 14
        '
        'cmbTechnologyNBAudit
        '
        Me.cmbTechnologyNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbTechnologyNBAudit.EditValue = ""
        Me.cmbTechnologyNBAudit.Location = New System.Drawing.Point(103, 28)
        Me.cmbTechnologyNBAudit.Name = "cmbTechnologyNBAudit"
        Me.cmbTechnologyNBAudit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTechnologyNBAudit.Size = New System.Drawing.Size(154, 20)
        Me.cmbTechnologyNBAudit.TabIndex = 14
        '
        'cmbInclusionListNBAudit
        '
        Me.cmbInclusionListNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbInclusionListNBAudit.EditValue = ""
        Me.cmbInclusionListNBAudit.Location = New System.Drawing.Point(103, 53)
        Me.cmbInclusionListNBAudit.Name = "cmbInclusionListNBAudit"
        Me.cmbInclusionListNBAudit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbInclusionListNBAudit.Size = New System.Drawing.Size(154, 20)
        Me.cmbInclusionListNBAudit.TabIndex = 14
        '
        'btnAddConfigNBAudit
        '
        Me.btnAddConfigNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAddConfigNBAudit.Enabled = False
        Me.btnAddConfigNBAudit.Location = New System.Drawing.Point(262, 2)
        Me.btnAddConfigNBAudit.Margin = New System.Windows.Forms.Padding(2)
        Me.btnAddConfigNBAudit.Name = "btnAddConfigNBAudit"
        Me.btnAddConfigNBAudit.Size = New System.Drawing.Size(46, 21)
        Me.btnAddConfigNBAudit.TabIndex = 15
        Me.btnAddConfigNBAudit.Text = "Add"
        '
        'grpOptionalSettings
        '
        Me.grpOptionalSettings.Controls.Add(Me.TableLayoutPanel50)
        Me.grpOptionalSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpOptionalSettings.Location = New System.Drawing.Point(3, 116)
        Me.grpOptionalSettings.Name = "grpOptionalSettings"
        Me.grpOptionalSettings.Size = New System.Drawing.Size(314, 155)
        Me.grpOptionalSettings.TabIndex = 1
        Me.grpOptionalSettings.Text = "Optional Settings"
        '
        'TableLayoutPanel50
        '
        Me.TableLayoutPanel50.ColumnCount = 3
        Me.TableLayoutPanel50.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel50.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel50.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel50.Controls.Add(Me.LabelControl39, 0, 0)
        Me.TableLayoutPanel50.Controls.Add(Me.LabelControl40, 0, 1)
        Me.TableLayoutPanel50.Controls.Add(Me.LabelControl41, 0, 2)
        Me.TableLayoutPanel50.Controls.Add(Me.cmbExclusionListNBAudit, 1, 0)
        Me.TableLayoutPanel50.Controls.Add(Me.cmbSLayerNBAudit, 1, 1)
        Me.TableLayoutPanel50.Controls.Add(Me.cmbTLayerNBAudit, 1, 2)
        Me.TableLayoutPanel50.Controls.Add(Me.LabelControl28, 0, 3)
        Me.TableLayoutPanel50.Controls.Add(Me.LabelControl29, 0, 4)
        Me.TableLayoutPanel50.Controls.Add(Me.cmbNBType, 1, 3)
        Me.TableLayoutPanel50.Controls.Add(Me.cmbMMLScriptID, 1, 4)
        Me.TableLayoutPanel50.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel50.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel50.Name = "TableLayoutPanel50"
        Me.TableLayoutPanel50.RowCount = 6
        Me.TableLayoutPanel50.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel50.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel50.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel50.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel50.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel50.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel50.Size = New System.Drawing.Size(310, 130)
        Me.TableLayoutPanel50.TabIndex = 1
        '
        'LabelControl39
        '
        Me.LabelControl39.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl39.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl39.Name = "LabelControl39"
        Me.LabelControl39.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl39.Size = New System.Drawing.Size(94, 19)
        Me.LabelControl39.TabIndex = 1
        Me.LabelControl39.Text = "Exclusion List"
        '
        'LabelControl40
        '
        Me.LabelControl40.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl40.Location = New System.Drawing.Point(3, 28)
        Me.LabelControl40.Name = "LabelControl40"
        Me.LabelControl40.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl40.Size = New System.Drawing.Size(94, 19)
        Me.LabelControl40.TabIndex = 2
        Me.LabelControl40.Text = "SLAYER"
        '
        'LabelControl41
        '
        Me.LabelControl41.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl41.Location = New System.Drawing.Point(3, 53)
        Me.LabelControl41.Name = "LabelControl41"
        Me.LabelControl41.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl41.Size = New System.Drawing.Size(94, 19)
        Me.LabelControl41.TabIndex = 2
        Me.LabelControl41.Text = "TLAYER"
        '
        'cmbExclusionListNBAudit
        '
        Me.cmbExclusionListNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbExclusionListNBAudit.EditValue = ""
        Me.cmbExclusionListNBAudit.Location = New System.Drawing.Point(103, 3)
        Me.cmbExclusionListNBAudit.Name = "cmbExclusionListNBAudit"
        Me.cmbExclusionListNBAudit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbExclusionListNBAudit.Size = New System.Drawing.Size(154, 20)
        Me.cmbExclusionListNBAudit.TabIndex = 14
        '
        'cmbSLayerNBAudit
        '
        Me.cmbSLayerNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbSLayerNBAudit.EditValue = ""
        Me.cmbSLayerNBAudit.Location = New System.Drawing.Point(103, 28)
        Me.cmbSLayerNBAudit.Name = "cmbSLayerNBAudit"
        Me.cmbSLayerNBAudit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbSLayerNBAudit.Size = New System.Drawing.Size(154, 20)
        Me.cmbSLayerNBAudit.TabIndex = 14
        '
        'cmbTLayerNBAudit
        '
        Me.cmbTLayerNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbTLayerNBAudit.EditValue = ""
        Me.cmbTLayerNBAudit.Location = New System.Drawing.Point(103, 53)
        Me.cmbTLayerNBAudit.Name = "cmbTLayerNBAudit"
        Me.cmbTLayerNBAudit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTLayerNBAudit.Size = New System.Drawing.Size(154, 20)
        Me.cmbTLayerNBAudit.TabIndex = 14
        '
        'LabelControl28
        '
        Me.LabelControl28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl28.Location = New System.Drawing.Point(3, 78)
        Me.LabelControl28.Name = "LabelControl28"
        Me.LabelControl28.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl28.Size = New System.Drawing.Size(94, 19)
        Me.LabelControl28.TabIndex = 15
        Me.LabelControl28.Text = "NB Type"
        '
        'LabelControl29
        '
        Me.LabelControl29.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl29.Location = New System.Drawing.Point(3, 103)
        Me.LabelControl29.Name = "LabelControl29"
        Me.LabelControl29.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl29.Size = New System.Drawing.Size(94, 19)
        Me.LabelControl29.TabIndex = 16
        Me.LabelControl29.Text = "MML Script ID"
        '
        'cmbNBType
        '
        Me.cmbNBType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbNBType.Location = New System.Drawing.Point(103, 78)
        Me.cmbNBType.Name = "cmbNBType"
        Me.cmbNBType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbNBType.Size = New System.Drawing.Size(154, 20)
        Me.cmbNBType.TabIndex = 17
        '
        'cmbMMLScriptID
        '
        Me.cmbMMLScriptID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbMMLScriptID.Location = New System.Drawing.Point(103, 103)
        Me.cmbMMLScriptID.Name = "cmbMMLScriptID"
        Me.cmbMMLScriptID.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbMMLScriptID.Size = New System.Drawing.Size(154, 20)
        Me.cmbMMLScriptID.TabIndex = 18
        '
        'SplitContainerControl3
        '
        Me.SplitContainerControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl3.Horizontal = False
        Me.SplitContainerControl3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl3.Name = "SplitContainerControl3"
        '
        'SplitContainerControl3.Panel1
        '
        Me.SplitContainerControl3.Panel1.Controls.Add(Me.GroupControl6)
        Me.SplitContainerControl3.Panel1.MinSize = 350
        Me.SplitContainerControl3.Panel1.Text = "Panel1"
        '
        'SplitContainerControl3.Panel2
        '
        Me.SplitContainerControl3.Panel2.Controls.Add(Me.grpConfigSummaryNBAudit)
        Me.SplitContainerControl3.Panel2.MinSize = 250
        Me.SplitContainerControl3.Panel2.Text = "Panel2"
        Me.SplitContainerControl3.Size = New System.Drawing.Size(880, 688)
        Me.SplitContainerControl3.SplitterPosition = 432
        Me.SplitContainerControl3.TabIndex = 1
        Me.SplitContainerControl3.Text = "SplitContainerControl1"
        '
        'GroupControl6
        '
        Me.GroupControl6.Controls.Add(Me.TableLayoutPanel43)
        Me.GroupControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl6.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl6.Name = "GroupControl6"
        Me.GroupControl6.Size = New System.Drawing.Size(880, 428)
        Me.GroupControl6.TabIndex = 1
        Me.GroupControl6.Text = "Campaign Result"
        '
        'TableLayoutPanel43
        '
        Me.TableLayoutPanel43.ColumnCount = 1
        Me.TableLayoutPanel43.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel43.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel43.Controls.Add(Me.TableLayoutPanel44, 0, 0)
        Me.TableLayoutPanel43.Controls.Add(Me.XtraTabControl1, 0, 1)
        Me.TableLayoutPanel43.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel43.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel43.Name = "TableLayoutPanel43"
        Me.TableLayoutPanel43.RowCount = 2
        Me.TableLayoutPanel43.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel43.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel43.Size = New System.Drawing.Size(876, 403)
        Me.TableLayoutPanel43.TabIndex = 0
        '
        'TableLayoutPanel44
        '
        Me.TableLayoutPanel44.ColumnCount = 3
        Me.TableLayoutPanel44.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel44.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220.0!))
        Me.TableLayoutPanel44.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel44.Controls.Add(Me.LabelControl31, 0, 0)
        Me.TableLayoutPanel44.Controls.Add(Me.cmbResultSetIdNBAudit, 1, 0)
        Me.TableLayoutPanel44.Controls.Add(Me.btnDeleteResultSetIdNBAudit, 2, 0)
        Me.TableLayoutPanel44.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel44.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel44.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel44.Name = "TableLayoutPanel44"
        Me.TableLayoutPanel44.RowCount = 1
        Me.TableLayoutPanel44.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel44.Size = New System.Drawing.Size(872, 26)
        Me.TableLayoutPanel44.TabIndex = 3
        '
        'LabelControl31
        '
        Me.LabelControl31.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl31.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl31.Name = "LabelControl31"
        Me.LabelControl31.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl31.Size = New System.Drawing.Size(74, 20)
        Me.LabelControl31.TabIndex = 3
        Me.LabelControl31.Text = "Result Set ID"
        '
        'cmbResultSetIdNBAudit
        '
        Me.cmbResultSetIdNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbResultSetIdNBAudit.EditValue = ""
        Me.cmbResultSetIdNBAudit.Location = New System.Drawing.Point(83, 3)
        Me.cmbResultSetIdNBAudit.Name = "cmbResultSetIdNBAudit"
        Me.cmbResultSetIdNBAudit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbResultSetIdNBAudit.Size = New System.Drawing.Size(214, 20)
        Me.cmbResultSetIdNBAudit.TabIndex = 13
        '
        'btnDeleteResultSetIdNBAudit
        '
        Me.btnDeleteResultSetIdNBAudit.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDeleteResultSetIdNBAudit.Location = New System.Drawing.Point(302, 2)
        Me.btnDeleteResultSetIdNBAudit.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDeleteResultSetIdNBAudit.Name = "btnDeleteResultSetIdNBAudit"
        Me.btnDeleteResultSetIdNBAudit.Size = New System.Drawing.Size(62, 22)
        Me.btnDeleteResultSetIdNBAudit.TabIndex = 14
        Me.btnDeleteResultSetIdNBAudit.Tag = "NB_Audit"
        Me.btnDeleteResultSetIdNBAudit.Text = "Delete"
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.Location = New System.Drawing.Point(3, 33)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.XtraTabPage1
        Me.XtraTabControl1.Size = New System.Drawing.Size(870, 367)
        Me.XtraTabControl1.TabIndex = 4
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1, Me.XtraTabPage2})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.gcResultSummaryNBAudit)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(868, 342)
        Me.XtraTabPage1.Text = "Summary"
        '
        'gcResultSummaryNBAudit
        '
        Me.gcResultSummaryNBAudit.AllowDrop = True
        Me.gcResultSummaryNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcResultSummaryNBAudit.Location = New System.Drawing.Point(0, 0)
        Me.gcResultSummaryNBAudit.MainView = Me.gvResultSummaryNBAudit
        Me.gcResultSummaryNBAudit.Name = "gcResultSummaryNBAudit"
        Me.gcResultSummaryNBAudit.Size = New System.Drawing.Size(868, 342)
        Me.gcResultSummaryNBAudit.TabIndex = 2
        Me.gcResultSummaryNBAudit.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvResultSummaryNBAudit, Me.GridView26})
        '
        'gvResultSummaryNBAudit
        '
        Me.gvResultSummaryNBAudit.ActiveFilterEnabled = False
        Me.gvResultSummaryNBAudit.GridControl = Me.gcResultSummaryNBAudit
        Me.gvResultSummaryNBAudit.Name = "gvResultSummaryNBAudit"
        Me.gvResultSummaryNBAudit.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvResultSummaryNBAudit.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvResultSummaryNBAudit.OptionsBehavior.Editable = False
        Me.gvResultSummaryNBAudit.OptionsBehavior.ReadOnly = True
        Me.gvResultSummaryNBAudit.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvResultSummaryNBAudit.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvResultSummaryNBAudit.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvResultSummaryNBAudit.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvResultSummaryNBAudit.OptionsSelection.MultiSelect = True
        Me.gvResultSummaryNBAudit.OptionsView.ShowGroupPanel = False
        '
        'GridView26
        '
        Me.GridView26.GridControl = Me.gcResultSummaryNBAudit
        Me.GridView26.Name = "GridView26"
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Controls.Add(Me.TableLayoutPanel45)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(868, 342)
        Me.XtraTabPage2.Text = "Data"
        '
        'TableLayoutPanel45
        '
        Me.TableLayoutPanel45.ColumnCount = 1
        Me.TableLayoutPanel45.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel45.Controls.Add(Me.gcResultDataNBAudit, 0, 1)
        Me.TableLayoutPanel45.Controls.Add(Me.TableLayoutPanel46, 0, 0)
        Me.TableLayoutPanel45.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel45.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel45.Name = "TableLayoutPanel45"
        Me.TableLayoutPanel45.RowCount = 2
        Me.TableLayoutPanel45.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel45.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel45.Size = New System.Drawing.Size(868, 342)
        Me.TableLayoutPanel45.TabIndex = 0
        '
        'gcResultDataNBAudit
        '
        Me.gcResultDataNBAudit.AllowDrop = True
        Me.gcResultDataNBAudit.ContextMenuStrip = Me.cmsMapNB
        Me.gcResultDataNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcResultDataNBAudit.Location = New System.Drawing.Point(3, 38)
        Me.gcResultDataNBAudit.MainView = Me.gvResultDataNBAudit
        Me.gcResultDataNBAudit.Name = "gcResultDataNBAudit"
        Me.gcResultDataNBAudit.Size = New System.Drawing.Size(862, 301)
        Me.gcResultDataNBAudit.TabIndex = 4
        Me.gcResultDataNBAudit.Tag = "NBDetect"
        Me.gcResultDataNBAudit.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvResultDataNBAudit, Me.GridView28})
        '
        'gvResultDataNBAudit
        '
        Me.gvResultDataNBAudit.ActiveFilterEnabled = False
        Me.gvResultDataNBAudit.GridControl = Me.gcResultDataNBAudit
        Me.gvResultDataNBAudit.Name = "gvResultDataNBAudit"
        Me.gvResultDataNBAudit.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvResultDataNBAudit.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvResultDataNBAudit.OptionsBehavior.Editable = False
        Me.gvResultDataNBAudit.OptionsBehavior.ReadOnly = True
        Me.gvResultDataNBAudit.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvResultDataNBAudit.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvResultDataNBAudit.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvResultDataNBAudit.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.gvResultDataNBAudit.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvResultDataNBAudit.OptionsSelection.MultiSelect = True
        Me.gvResultDataNBAudit.OptionsView.ShowGroupPanel = False
        '
        'GridView28
        '
        Me.GridView28.GridControl = Me.gcResultDataNBAudit
        Me.GridView28.Name = "GridView28"
        '
        'TableLayoutPanel46
        '
        Me.TableLayoutPanel46.ColumnCount = 3
        Me.TableLayoutPanel46.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel46.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel46.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel46.Controls.Add(Me.btnLoadToGridNBAudit, 0, 0)
        Me.TableLayoutPanel46.Controls.Add(Me.btnDataToCSVNBAudit, 1, 0)
        Me.TableLayoutPanel46.Controls.Add(Me.lblRecordCountNBAudit, 2, 0)
        Me.TableLayoutPanel46.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel46.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel46.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel46.Name = "TableLayoutPanel46"
        Me.TableLayoutPanel46.RowCount = 1
        Me.TableLayoutPanel46.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel46.Size = New System.Drawing.Size(864, 31)
        Me.TableLayoutPanel46.TabIndex = 0
        '
        'btnLoadToGridNBAudit
        '
        Me.btnLoadToGridNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnLoadToGridNBAudit.Location = New System.Drawing.Point(3, 3)
        Me.btnLoadToGridNBAudit.Name = "btnLoadToGridNBAudit"
        Me.btnLoadToGridNBAudit.Size = New System.Drawing.Size(94, 25)
        Me.btnLoadToGridNBAudit.TabIndex = 0
        Me.btnLoadToGridNBAudit.Text = "Load To Grid"
        '
        'btnDataToCSVNBAudit
        '
        Me.btnDataToCSVNBAudit.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDataToCSVNBAudit.Location = New System.Drawing.Point(103, 3)
        Me.btnDataToCSVNBAudit.Name = "btnDataToCSVNBAudit"
        Me.btnDataToCSVNBAudit.Size = New System.Drawing.Size(94, 25)
        Me.btnDataToCSVNBAudit.TabIndex = 1
        Me.btnDataToCSVNBAudit.Text = "All Data To CSV"
        '
        'lblRecordCountNBAudit
        '
        Me.lblRecordCountNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblRecordCountNBAudit.Location = New System.Drawing.Point(203, 3)
        Me.lblRecordCountNBAudit.Name = "lblRecordCountNBAudit"
        Me.lblRecordCountNBAudit.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblRecordCountNBAudit.Size = New System.Drawing.Size(658, 25)
        Me.lblRecordCountNBAudit.TabIndex = 2
        Me.lblRecordCountNBAudit.Text = "Count of Records: "
        Me.lblRecordCountNBAudit.Visible = False
        '
        'grpConfigSummaryNBAudit
        '
        Me.grpConfigSummaryNBAudit.Controls.Add(Me.TableLayoutPanel48)
        Me.grpConfigSummaryNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpConfigSummaryNBAudit.Location = New System.Drawing.Point(0, 0)
        Me.grpConfigSummaryNBAudit.Name = "grpConfigSummaryNBAudit"
        Me.grpConfigSummaryNBAudit.Size = New System.Drawing.Size(880, 250)
        Me.grpConfigSummaryNBAudit.TabIndex = 0
        Me.grpConfigSummaryNBAudit.Text = "Configuration Summary"
        '
        'TableLayoutPanel48
        '
        Me.TableLayoutPanel48.ColumnCount = 2
        Me.TableLayoutPanel48.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel48.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350.0!))
        Me.TableLayoutPanel48.Controls.Add(Me.TableLayoutPanel49, 1, 0)
        Me.TableLayoutPanel48.Controls.Add(Me.gcConfigNBAudit, 0, 0)
        Me.TableLayoutPanel48.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel48.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel48.Name = "TableLayoutPanel48"
        Me.TableLayoutPanel48.RowCount = 1
        Me.TableLayoutPanel48.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel48.Size = New System.Drawing.Size(876, 225)
        Me.TableLayoutPanel48.TabIndex = 4
        '
        'TableLayoutPanel49
        '
        Me.TableLayoutPanel49.ColumnCount = 1
        Me.TableLayoutPanel49.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel49.Controls.Add(Me.GroupControl8, 0, 0)
        Me.TableLayoutPanel49.Controls.Add(Me.btnListManagerNBAudit, 0, 1)
        Me.TableLayoutPanel49.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel49.Location = New System.Drawing.Point(529, 3)
        Me.TableLayoutPanel49.Name = "TableLayoutPanel49"
        Me.TableLayoutPanel49.RowCount = 2
        Me.TableLayoutPanel49.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel49.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel49.Size = New System.Drawing.Size(344, 219)
        Me.TableLayoutPanel49.TabIndex = 0
        '
        'GroupControl8
        '
        Me.GroupControl8.Controls.Add(Me.TableLayoutPanel61)
        Me.GroupControl8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl8.Location = New System.Drawing.Point(3, 3)
        Me.GroupControl8.Name = "GroupControl8"
        Me.GroupControl8.Size = New System.Drawing.Size(338, 181)
        Me.GroupControl8.TabIndex = 1
        Me.GroupControl8.Text = "Layer Properties"
        '
        'TableLayoutPanel61
        '
        Me.TableLayoutPanel61.ColumnCount = 1
        Me.TableLayoutPanel61.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel61.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel61.Controls.Add(Me.ceApplyConfigAllAudit, 0, 1)
        Me.TableLayoutPanel61.Controls.Add(Me.grdPropertyNBAudit, 0, 0)
        Me.TableLayoutPanel61.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel61.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel61.Name = "TableLayoutPanel61"
        Me.TableLayoutPanel61.RowCount = 2
        Me.TableLayoutPanel61.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel61.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel61.Size = New System.Drawing.Size(334, 156)
        Me.TableLayoutPanel61.TabIndex = 1
        '
        'ceApplyConfigAllAudit
        '
        Me.ceApplyConfigAllAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceApplyConfigAllAudit.Location = New System.Drawing.Point(5, 134)
        Me.ceApplyConfigAllAudit.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceApplyConfigAllAudit.Name = "ceApplyConfigAllAudit"
        Me.ceApplyConfigAllAudit.Properties.Caption = "Apply changes to all configuration"
        Me.ceApplyConfigAllAudit.Size = New System.Drawing.Size(326, 19)
        Me.ceApplyConfigAllAudit.TabIndex = 3
        '
        'grdPropertyNBAudit
        '
        Me.grdPropertyNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdPropertyNBAudit.LineColor = System.Drawing.SystemColors.ControlDark
        Me.grdPropertyNBAudit.Location = New System.Drawing.Point(3, 3)
        Me.grdPropertyNBAudit.Name = "grdPropertyNBAudit"
        Me.grdPropertyNBAudit.Size = New System.Drawing.Size(328, 125)
        Me.grdPropertyNBAudit.TabIndex = 0
        Me.grdPropertyNBAudit.Tag = "NB_Audit"
        Me.grdPropertyNBAudit.ToolbarVisible = False
        '
        'btnListManagerNBAudit
        '
        Me.btnListManagerNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnListManagerNBAudit.Location = New System.Drawing.Point(3, 190)
        Me.btnListManagerNBAudit.Name = "btnListManagerNBAudit"
        Me.btnListManagerNBAudit.Size = New System.Drawing.Size(338, 26)
        Me.btnListManagerNBAudit.TabIndex = 1
        Me.btnListManagerNBAudit.Text = "List Manager"
        '
        'gcConfigNBAudit
        '
        Me.gcConfigNBAudit.AllowDrop = True
        Me.gcConfigNBAudit.ContextMenuStrip = Me.cmsConfigurationSummary
        Me.gcConfigNBAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcConfigNBAudit.Location = New System.Drawing.Point(3, 3)
        Me.gcConfigNBAudit.MainView = Me.gvConfigNBAudit
        Me.gcConfigNBAudit.Name = "gcConfigNBAudit"
        Me.gcConfigNBAudit.Size = New System.Drawing.Size(520, 219)
        Me.gcConfigNBAudit.TabIndex = 3
        Me.gcConfigNBAudit.Tag = "NB_Audit"
        Me.gcConfigNBAudit.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvConfigNBAudit, Me.GridView30})
        '
        'gvConfigNBAudit
        '
        Me.gvConfigNBAudit.ActiveFilterEnabled = False
        Me.gvConfigNBAudit.GridControl = Me.gcConfigNBAudit
        Me.gvConfigNBAudit.Name = "gvConfigNBAudit"
        Me.gvConfigNBAudit.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvConfigNBAudit.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvConfigNBAudit.OptionsBehavior.Editable = False
        Me.gvConfigNBAudit.OptionsBehavior.ReadOnly = True
        Me.gvConfigNBAudit.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvConfigNBAudit.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvConfigNBAudit.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvConfigNBAudit.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvConfigNBAudit.OptionsSelection.MultiSelect = True
        Me.gvConfigNBAudit.OptionsView.ShowGroupPanel = False
        '
        'GridView30
        '
        Me.GridView30.GridControl = Me.gcConfigNBAudit
        Me.GridView30.Name = "GridView30"
        '
        'tpNBFetch
        '
        Me.tpNBFetch.Controls.Add(Me.SplitContainerControl6)
        Me.tpNBFetch.Name = "tpNBFetch"
        Me.tpNBFetch.Size = New System.Drawing.Size(1220, 688)
        Me.tpNBFetch.Text = "NB Fetch"
        '
        'SplitContainerControl6
        '
        Me.SplitContainerControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl6.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl6.Name = "SplitContainerControl6"
        '
        'SplitContainerControl6.Panel1
        '
        Me.SplitContainerControl6.Panel1.Controls.Add(Me.gcSelectObjects)
        Me.SplitContainerControl6.Panel1.MinSize = 300
        Me.SplitContainerControl6.Panel1.Text = "Panel1"
        '
        'SplitContainerControl6.Panel2
        '
        Me.SplitContainerControl6.Panel2.Controls.Add(Me.GroupControl7)
        Me.SplitContainerControl6.Panel2.Text = "Panel2"
        Me.SplitContainerControl6.Size = New System.Drawing.Size(1220, 688)
        Me.SplitContainerControl6.SplitterPosition = 300
        Me.SplitContainerControl6.TabIndex = 0
        Me.SplitContainerControl6.Text = "SplitContainerControl6"
        '
        'gcSelectObjects
        '
        Me.gcSelectObjects.Controls.Add(Me.TableLayoutPanel51)
        Me.gcSelectObjects.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcSelectObjects.Location = New System.Drawing.Point(0, 0)
        Me.gcSelectObjects.Name = "gcSelectObjects"
        Me.gcSelectObjects.Size = New System.Drawing.Size(300, 688)
        Me.gcSelectObjects.TabIndex = 2
        Me.gcSelectObjects.Text = "Select Objects"
        '
        'TableLayoutPanel51
        '
        Me.TableLayoutPanel51.ColumnCount = 1
        Me.TableLayoutPanel51.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel51.Controls.Add(Me.gcObjectTree, 0, 2)
        Me.TableLayoutPanel51.Controls.Add(Me.TableLayoutPanel56, 0, 0)
        Me.TableLayoutPanel51.Controls.Add(Me.TableLayoutPanel62, 0, 1)
        Me.TableLayoutPanel51.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel51.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel51.Name = "TableLayoutPanel51"
        Me.TableLayoutPanel51.RowCount = 3
        Me.TableLayoutPanel51.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel51.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel51.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel51.Size = New System.Drawing.Size(296, 663)
        Me.TableLayoutPanel51.TabIndex = 0
        '
        'gcObjectTree
        '
        Me.gcObjectTree.Controls.Add(Me.TableLayoutPanel52)
        Me.gcObjectTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcObjectTree.Location = New System.Drawing.Point(3, 59)
        Me.gcObjectTree.Name = "gcObjectTree"
        Me.gcObjectTree.Size = New System.Drawing.Size(290, 601)
        Me.gcObjectTree.TabIndex = 2
        Me.gcObjectTree.Text = "Object Tree"
        '
        'TableLayoutPanel52
        '
        Me.TableLayoutPanel52.ColumnCount = 1
        Me.TableLayoutPanel52.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel52.Controls.Add(Me.tvObjectsTree, 0, 3)
        Me.TableLayoutPanel52.Controls.Add(Me.TableLayoutPanel53, 0, 2)
        Me.TableLayoutPanel52.Controls.Add(Me.TableLayoutPanel54, 0, 1)
        Me.TableLayoutPanel52.Controls.Add(Me.TableLayoutPanel55, 0, 0)
        Me.TableLayoutPanel52.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel52.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel52.Name = "TableLayoutPanel52"
        Me.TableLayoutPanel52.RowCount = 4
        Me.TableLayoutPanel52.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel52.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel52.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel52.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel52.Size = New System.Drawing.Size(286, 576)
        Me.TableLayoutPanel52.TabIndex = 0
        '
        'tvObjectsTree
        '
        Me.tvObjectsTree.CheckBoxes = True
        Me.tvObjectsTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvObjectsTree.Location = New System.Drawing.Point(3, 81)
        Me.tvObjectsTree.Name = "tvObjectsTree"
        Me.tvObjectsTree.ShowNodeToolTips = True
        Me.tvObjectsTree.Size = New System.Drawing.Size(280, 492)
        Me.tvObjectsTree.TabIndex = 9
        '
        'TableLayoutPanel53
        '
        Me.TableLayoutPanel53.ColumnCount = 2
        Me.TableLayoutPanel53.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel53.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel53.Controls.Add(Me.txtSearchObject, 0, 0)
        Me.TableLayoutPanel53.Controls.Add(Me.LabelControl45, 0, 0)
        Me.TableLayoutPanel53.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel53.Location = New System.Drawing.Point(3, 52)
        Me.TableLayoutPanel53.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.TableLayoutPanel53.Name = "TableLayoutPanel53"
        Me.TableLayoutPanel53.RowCount = 1
        Me.TableLayoutPanel53.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel53.Size = New System.Drawing.Size(283, 26)
        Me.TableLayoutPanel53.TabIndex = 2
        '
        'txtSearchObject
        '
        Me.txtSearchObject.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchObject.Location = New System.Drawing.Point(83, 3)
        Me.txtSearchObject.Name = "txtSearchObject"
        Me.txtSearchObject.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSearchObject.Properties.NullValuePrompt = "Search..."
        Me.txtSearchObject.Size = New System.Drawing.Size(197, 20)
        Me.txtSearchObject.TabIndex = 14
        '
        'LabelControl45
        '
        Me.LabelControl45.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl45.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl45.Name = "LabelControl45"
        Me.LabelControl45.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl45.Size = New System.Drawing.Size(74, 20)
        Me.LabelControl45.TabIndex = 0
        Me.LabelControl45.Text = "Search"
        '
        'TableLayoutPanel54
        '
        Me.TableLayoutPanel54.ColumnCount = 3
        Me.TableLayoutPanel54.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel54.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel54.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65.0!))
        Me.TableLayoutPanel54.Controls.Add(Me.cmbObjectType, 0, 0)
        Me.TableLayoutPanel54.Controls.Add(Me.LabelControl46, 0, 0)
        Me.TableLayoutPanel54.Controls.Add(Me.lblObjectTreeCount, 2, 0)
        Me.TableLayoutPanel54.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel54.Location = New System.Drawing.Point(3, 26)
        Me.TableLayoutPanel54.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.TableLayoutPanel54.Name = "TableLayoutPanel54"
        Me.TableLayoutPanel54.RowCount = 1
        Me.TableLayoutPanel54.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel54.Size = New System.Drawing.Size(283, 26)
        Me.TableLayoutPanel54.TabIndex = 1
        '
        'cmbObjectType
        '
        Me.cmbObjectType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbObjectType.EditValue = ""
        Me.cmbObjectType.Location = New System.Drawing.Point(83, 3)
        Me.cmbObjectType.Name = "cmbObjectType"
        Me.cmbObjectType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbObjectType.Properties.Sorted = True
        Me.cmbObjectType.Size = New System.Drawing.Size(132, 20)
        Me.cmbObjectType.TabIndex = 12
        '
        'LabelControl46
        '
        Me.LabelControl46.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl46.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl46.Name = "LabelControl46"
        Me.LabelControl46.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl46.Size = New System.Drawing.Size(74, 20)
        Me.LabelControl46.TabIndex = 0
        Me.LabelControl46.Text = "Object Type"
        '
        'lblObjectTreeCount
        '
        Me.lblObjectTreeCount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblObjectTreeCount.Location = New System.Drawing.Point(221, 3)
        Me.lblObjectTreeCount.Name = "lblObjectTreeCount"
        Me.lblObjectTreeCount.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblObjectTreeCount.Size = New System.Drawing.Size(59, 20)
        Me.lblObjectTreeCount.TabIndex = 8
        Me.lblObjectTreeCount.Text = "#:"
        '
        'TableLayoutPanel55
        '
        Me.TableLayoutPanel55.ColumnCount = 2
        Me.TableLayoutPanel55.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel55.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel55.Controls.Add(Me.cmbTechnology, 1, 0)
        Me.TableLayoutPanel55.Controls.Add(Me.LabelControl47, 0, 0)
        Me.TableLayoutPanel55.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel55.Location = New System.Drawing.Point(3, 0)
        Me.TableLayoutPanel55.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.TableLayoutPanel55.Name = "TableLayoutPanel55"
        Me.TableLayoutPanel55.RowCount = 1
        Me.TableLayoutPanel55.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel55.Size = New System.Drawing.Size(283, 26)
        Me.TableLayoutPanel55.TabIndex = 0
        '
        'cmbTechnology
        '
        Me.cmbTechnology.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbTechnology.EditValue = ""
        Me.cmbTechnology.Location = New System.Drawing.Point(83, 3)
        Me.cmbTechnology.Name = "cmbTechnology"
        Me.cmbTechnology.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTechnology.Properties.Sorted = True
        Me.cmbTechnology.Size = New System.Drawing.Size(197, 20)
        Me.cmbTechnology.TabIndex = 11
        '
        'LabelControl47
        '
        Me.LabelControl47.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl47.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl47.Name = "LabelControl47"
        Me.LabelControl47.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl47.Size = New System.Drawing.Size(74, 20)
        Me.LabelControl47.TabIndex = 0
        Me.LabelControl47.Text = "Technology"
        '
        'TableLayoutPanel56
        '
        Me.TableLayoutPanel56.ColumnCount = 2
        Me.TableLayoutPanel56.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel56.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel56.Controls.Add(Me.LabelControl49, 0, 0)
        Me.TableLayoutPanel56.Controls.Add(Me.btnNBFetch, 1, 0)
        Me.TableLayoutPanel56.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel56.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel56.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel56.Name = "TableLayoutPanel56"
        Me.TableLayoutPanel56.RowCount = 1
        Me.TableLayoutPanel56.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel56.Size = New System.Drawing.Size(296, 28)
        Me.TableLayoutPanel56.TabIndex = 3
        '
        'LabelControl49
        '
        Me.LabelControl49.Appearance.Image = CType(resources.GetObject("LabelControl49.Appearance.Image"), System.Drawing.Image)
        Me.LabelControl49.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LabelControl49.Appearance.Options.UseImage = True
        Me.LabelControl49.Appearance.Options.UseImageAlign = True
        Me.LabelControl49.Appearance.Options.UseTextOptions = True
        Me.LabelControl49.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl49.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.LabelControl49.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl49.Name = "LabelControl49"
        Me.LabelControl49.Size = New System.Drawing.Size(190, 22)
        Me.LabelControl49.TabIndex = 0
        Me.LabelControl49.Text = "Select Objects"
        '
        'btnNBFetch
        '
        Me.btnNBFetch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnNBFetch.Location = New System.Drawing.Point(198, 2)
        Me.btnNBFetch.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNBFetch.Name = "btnNBFetch"
        Me.btnNBFetch.Size = New System.Drawing.Size(96, 24)
        Me.btnNBFetch.TabIndex = 1
        Me.btnNBFetch.Text = "Fetch NB"
        '
        'TableLayoutPanel62
        '
        Me.TableLayoutPanel62.ColumnCount = 2
        Me.TableLayoutPanel62.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel62.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel62.Controls.Add(Me.btnNBFetchCells, 1, 0)
        Me.TableLayoutPanel62.Controls.Add(Me.LabelControl48, 0, 0)
        Me.TableLayoutPanel62.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel62.Location = New System.Drawing.Point(0, 28)
        Me.TableLayoutPanel62.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel62.Name = "TableLayoutPanel62"
        Me.TableLayoutPanel62.RowCount = 1
        Me.TableLayoutPanel62.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel62.Size = New System.Drawing.Size(296, 28)
        Me.TableLayoutPanel62.TabIndex = 4
        '
        'btnNBFetchCells
        '
        Me.btnNBFetchCells.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnNBFetchCells.Location = New System.Drawing.Point(198, 2)
        Me.btnNBFetchCells.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNBFetchCells.Name = "btnNBFetchCells"
        Me.btnNBFetchCells.Size = New System.Drawing.Size(96, 24)
        Me.btnNBFetchCells.TabIndex = 2
        Me.btnNBFetchCells.Text = "Fetch Cells"
        '
        'LabelControl48
        '
        Me.LabelControl48.Appearance.Image = CType(resources.GetObject("LabelControl48.Appearance.Image"), System.Drawing.Image)
        Me.LabelControl48.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LabelControl48.Appearance.Options.UseImage = True
        Me.LabelControl48.Appearance.Options.UseImageAlign = True
        Me.LabelControl48.Appearance.Options.UseTextOptions = True
        Me.LabelControl48.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl48.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.LabelControl48.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl48.Name = "LabelControl48"
        Me.LabelControl48.Size = New System.Drawing.Size(190, 22)
        Me.LabelControl48.TabIndex = 1
        Me.LabelControl48.Text = "Press Fetch NB / Fetch Cells"
        '
        'GroupControl7
        '
        Me.GroupControl7.Controls.Add(Me.gcNBFetch)
        Me.GroupControl7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl7.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl7.Name = "GroupControl7"
        Me.GroupControl7.Size = New System.Drawing.Size(910, 688)
        Me.GroupControl7.TabIndex = 4
        Me.GroupControl7.Text = "Neighbors or Cells"
        '
        'gcNBFetch
        '
        Me.gcNBFetch.AllowDrop = True
        Me.gcNBFetch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcNBFetch.Location = New System.Drawing.Point(2, 23)
        Me.gcNBFetch.MainView = Me.gvNBFetch
        Me.gcNBFetch.Name = "gcNBFetch"
        Me.gcNBFetch.Size = New System.Drawing.Size(906, 663)
        Me.gcNBFetch.TabIndex = 3
        Me.gcNBFetch.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvNBFetch, Me.GridView25})
        '
        'gvNBFetch
        '
        Me.gvNBFetch.ActiveFilterEnabled = False
        Me.gvNBFetch.GridControl = Me.gcNBFetch
        Me.gvNBFetch.Name = "gvNBFetch"
        Me.gvNBFetch.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvNBFetch.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvNBFetch.OptionsBehavior.Editable = False
        Me.gvNBFetch.OptionsBehavior.ReadOnly = True
        Me.gvNBFetch.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvNBFetch.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvNBFetch.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvNBFetch.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.gvNBFetch.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvNBFetch.OptionsSelection.MultiSelect = True
        Me.gvNBFetch.OptionsView.ShowGroupPanel = False
        '
        'GridView25
        '
        Me.GridView25.GridControl = Me.gcNBFetch
        Me.GridView25.Name = "GridView25"
        '
        'tpMML
        '
        Me.tpMML.Controls.Add(Me.sccMML)
        Me.tpMML.Name = "tpMML"
        Me.tpMML.Size = New System.Drawing.Size(1220, 688)
        Me.tpMML.Text = "MML/XML"
        '
        'sccMML
        '
        Me.sccMML.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccMML.Horizontal = False
        Me.sccMML.Location = New System.Drawing.Point(0, 0)
        Me.sccMML.Name = "sccMML"
        '
        'sccMML.Panel1
        '
        Me.sccMML.Panel1.Controls.Add(Me.sccMmlTop)
        Me.sccMML.Panel1.MinSize = 300
        Me.sccMML.Panel1.Text = "Panel1"
        '
        'sccMML.Panel2
        '
        Me.sccMML.Panel2.Controls.Add(Me.grpMmlConfig)
        Me.sccMML.Panel2.MinSize = 300
        Me.sccMML.Panel2.Text = "Panel2"
        Me.sccMML.Size = New System.Drawing.Size(1220, 688)
        Me.sccMML.SplitterPosition = 410
        Me.sccMML.TabIndex = 0
        Me.sccMML.Text = "SplitContainerControl1"
        '
        'sccMmlTop
        '
        Me.sccMmlTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccMmlTop.Location = New System.Drawing.Point(0, 0)
        Me.sccMmlTop.Name = "sccMmlTop"
        '
        'sccMmlTop.Panel1
        '
        Me.sccMmlTop.Panel1.Controls.Add(Me.grpMmlInput)
        Me.sccMmlTop.Panel1.MinSize = 300
        Me.sccMmlTop.Panel1.Text = "Panel1"
        '
        'sccMmlTop.Panel2
        '
        Me.sccMmlTop.Panel2.Controls.Add(Me.xtcMmlTop)
        Me.sccMmlTop.Panel2.MinSize = 500
        Me.sccMmlTop.Panel2.Text = "Panel2"
        Me.sccMmlTop.Size = New System.Drawing.Size(1220, 378)
        Me.sccMmlTop.SplitterPosition = 396
        Me.sccMmlTop.TabIndex = 0
        Me.sccMmlTop.Text = "SplitContainerControl1"
        '
        'grpMmlInput
        '
        Me.grpMmlInput.Controls.Add(Me.TableLayoutPanel24)
        Me.grpMmlInput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpMmlInput.Location = New System.Drawing.Point(0, 0)
        Me.grpMmlInput.Name = "grpMmlInput"
        Me.grpMmlInput.Size = New System.Drawing.Size(396, 378)
        Me.grpMmlInput.TabIndex = 0
        Me.grpMmlInput.Text = "Input"
        '
        'TableLayoutPanel24
        '
        Me.TableLayoutPanel24.ColumnCount = 1
        Me.TableLayoutPanel24.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel24.Controls.Add(Me.GroupControl3, 0, 2)
        Me.TableLayoutPanel24.Controls.Add(Me.TableLayoutPanel13, 0, 3)
        Me.TableLayoutPanel24.Controls.Add(Me.gcMmlCampaign, 0, 1)
        Me.TableLayoutPanel24.Controls.Add(Me.TableLayoutPanel38, 0, 0)
        Me.TableLayoutPanel24.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel24.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel24.Name = "TableLayoutPanel24"
        Me.TableLayoutPanel24.RowCount = 4
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 86.0!))
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 67.0!))
        Me.TableLayoutPanel24.Size = New System.Drawing.Size(392, 353)
        Me.TableLayoutPanel24.TabIndex = 1
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.TableLayoutPanel27)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(2, 202)
        Me.GroupControl3.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(388, 82)
        Me.GroupControl3.TabIndex = 4
        Me.GroupControl3.Text = "Campaign Properties"
        '
        'TableLayoutPanel27
        '
        Me.TableLayoutPanel27.ColumnCount = 2
        Me.TableLayoutPanel27.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel27.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel27.Controls.Add(Me.LabelControl11, 0, 0)
        Me.TableLayoutPanel27.Controls.Add(Me.lblOwnerMmlInput, 1, 0)
        Me.TableLayoutPanel27.Controls.Add(Me.LabelControl9, 0, 1)
        Me.TableLayoutPanel27.Controls.Add(Me.lblLastEndTimeMml, 1, 1)
        Me.TableLayoutPanel27.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel27.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel27.Name = "TableLayoutPanel27"
        Me.TableLayoutPanel27.RowCount = 3
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel27.Size = New System.Drawing.Size(384, 57)
        Me.TableLayoutPanel27.TabIndex = 0
        '
        'LabelControl11
        '
        Me.LabelControl11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl11.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl11.Name = "LabelControl11"
        Me.LabelControl11.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl11.Size = New System.Drawing.Size(74, 20)
        Me.LabelControl11.TabIndex = 0
        Me.LabelControl11.Text = "Owner"
        '
        'lblOwnerMmlInput
        '
        Me.lblOwnerMmlInput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblOwnerMmlInput.Location = New System.Drawing.Point(83, 3)
        Me.lblOwnerMmlInput.Name = "lblOwnerMmlInput"
        Me.lblOwnerMmlInput.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblOwnerMmlInput.Size = New System.Drawing.Size(298, 20)
        Me.lblOwnerMmlInput.TabIndex = 9
        '
        'LabelControl9
        '
        Me.LabelControl9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl9.Location = New System.Drawing.Point(3, 29)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl9.Size = New System.Drawing.Size(74, 20)
        Me.LabelControl9.TabIndex = 10
        Me.LabelControl9.Text = "Last End Time"
        '
        'lblLastEndTimeMml
        '
        Me.lblLastEndTimeMml.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLastEndTimeMml.Location = New System.Drawing.Point(83, 29)
        Me.lblLastEndTimeMml.Name = "lblLastEndTimeMml"
        Me.lblLastEndTimeMml.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblLastEndTimeMml.Size = New System.Drawing.Size(298, 20)
        Me.lblLastEndTimeMml.TabIndex = 11
        '
        'TableLayoutPanel13
        '
        Me.TableLayoutPanel13.ColumnCount = 2
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel13.Controls.Add(Me.cmbMMLConfig, 1, 0)
        Me.TableLayoutPanel13.Controls.Add(Me.LabelControl10, 0, 0)
        Me.TableLayoutPanel13.Controls.Add(Me.TableLayoutPanel58, 1, 1)
        Me.TableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel13.Location = New System.Drawing.Point(3, 289)
        Me.TableLayoutPanel13.Name = "TableLayoutPanel13"
        Me.TableLayoutPanel13.RowCount = 3
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel13.Size = New System.Drawing.Size(386, 61)
        Me.TableLayoutPanel13.TabIndex = 0
        '
        'cmbMMLConfig
        '
        Me.cmbMMLConfig.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbMMLConfig.Location = New System.Drawing.Point(83, 3)
        Me.cmbMMLConfig.Name = "cmbMMLConfig"
        Me.cmbMMLConfig.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbMMLConfig.Size = New System.Drawing.Size(300, 20)
        Me.cmbMMLConfig.TabIndex = 8
        '
        'LabelControl10
        '
        Me.LabelControl10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl10.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl10.Size = New System.Drawing.Size(74, 19)
        Me.LabelControl10.TabIndex = 2
        Me.LabelControl10.Text = "Config"
        '
        'TableLayoutPanel58
        '
        Me.TableLayoutPanel58.ColumnCount = 2
        Me.TableLayoutPanel58.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel58.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel58.Controls.Add(Me.btnPreFilter, 0, 0)
        Me.TableLayoutPanel58.Controls.Add(Me.btnValidate, 1, 0)
        Me.TableLayoutPanel58.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel58.Location = New System.Drawing.Point(80, 25)
        Me.TableLayoutPanel58.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel58.Name = "TableLayoutPanel58"
        Me.TableLayoutPanel58.RowCount = 1
        Me.TableLayoutPanel58.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel58.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel58.Size = New System.Drawing.Size(306, 30)
        Me.TableLayoutPanel58.TabIndex = 9
        '
        'btnPreFilter
        '
        Me.btnPreFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnPreFilter.Enabled = False
        Me.btnPreFilter.Location = New System.Drawing.Point(2, 2)
        Me.btnPreFilter.Margin = New System.Windows.Forms.Padding(2)
        Me.btnPreFilter.Name = "btnPreFilter"
        Me.btnPreFilter.Size = New System.Drawing.Size(149, 26)
        Me.btnPreFilter.TabIndex = 10
        Me.btnPreFilter.Text = "Load Filter"
        '
        'btnValidate
        '
        Me.btnValidate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnValidate.Enabled = False
        Me.btnValidate.Location = New System.Drawing.Point(155, 2)
        Me.btnValidate.Margin = New System.Windows.Forms.Padding(2)
        Me.btnValidate.Name = "btnValidate"
        Me.btnValidate.Size = New System.Drawing.Size(149, 26)
        Me.btnValidate.TabIndex = 6
        Me.btnValidate.Text = "Validate"
        '
        'gcMmlCampaign
        '
        Me.gcMmlCampaign.AllowDrop = True
        Me.gcMmlCampaign.ContextMenuStrip = Me.cmMMLCampaign
        Me.gcMmlCampaign.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcMmlCampaign.Location = New System.Drawing.Point(2, 29)
        Me.gcMmlCampaign.MainView = Me.gvMmlCampaign
        Me.gcMmlCampaign.Margin = New System.Windows.Forms.Padding(2)
        Me.gcMmlCampaign.Name = "gcMmlCampaign"
        Me.gcMmlCampaign.Size = New System.Drawing.Size(388, 169)
        Me.gcMmlCampaign.TabIndex = 5
        Me.gcMmlCampaign.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvMmlCampaign, Me.GridView5})
        '
        'cmMMLCampaign
        '
        Me.cmMMLCampaign.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.cmMMLCampaign.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiInsertTempNB, Me.tsmiRemoveTempNB, Me.tsmiEditTempObjects, Me.tsmiRemoveAllTempNB})
        Me.cmMMLCampaign.Name = "cm_TagManagement"
        Me.cmMMLCampaign.Size = New System.Drawing.Size(276, 92)
        '
        'tsmiInsertTempNB
        '
        Me.tsmiInsertTempNB.Name = "tsmiInsertTempNB"
        Me.tsmiInsertTempNB.Size = New System.Drawing.Size(275, 22)
        Me.tsmiInsertTempNB.Text = "Insert ResultSet as Temporary NB"
        '
        'tsmiRemoveTempNB
        '
        Me.tsmiRemoveTempNB.Name = "tsmiRemoveTempNB"
        Me.tsmiRemoveTempNB.Size = New System.Drawing.Size(275, 22)
        Me.tsmiRemoveTempNB.Text = "Remove ResultSet from Temporary NB"
        '
        'tsmiEditTempObjects
        '
        Me.tsmiEditTempObjects.Name = "tsmiEditTempObjects"
        Me.tsmiEditTempObjects.Size = New System.Drawing.Size(275, 22)
        Me.tsmiEditTempObjects.Text = "Edit Temporary Objects"
        '
        'tsmiRemoveAllTempNB
        '
        Me.tsmiRemoveAllTempNB.Name = "tsmiRemoveAllTempNB"
        Me.tsmiRemoveAllTempNB.Size = New System.Drawing.Size(275, 22)
        Me.tsmiRemoveAllTempNB.Text = "Remove All Temporary NB"
        '
        'gvMmlCampaign
        '
        Me.gvMmlCampaign.ActiveFilterEnabled = False
        Me.gvMmlCampaign.GridControl = Me.gcMmlCampaign
        Me.gvMmlCampaign.Name = "gvMmlCampaign"
        Me.gvMmlCampaign.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvMmlCampaign.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvMmlCampaign.OptionsBehavior.Editable = False
        Me.gvMmlCampaign.OptionsBehavior.ReadOnly = True
        Me.gvMmlCampaign.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvMmlCampaign.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvMmlCampaign.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvMmlCampaign.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvMmlCampaign.OptionsSelection.MultiSelect = True
        Me.gvMmlCampaign.OptionsView.ShowGroupPanel = False
        '
        'GridView5
        '
        Me.GridView5.GridControl = Me.gcMmlCampaign
        Me.GridView5.Name = "GridView5"
        '
        'TableLayoutPanel38
        '
        Me.TableLayoutPanel38.ColumnCount = 3
        Me.TableLayoutPanel38.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel38.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel38.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel38.Controls.Add(Me.txtSearchMml, 0, 0)
        Me.TableLayoutPanel38.Controls.Add(Me.btnRefreshMml, 1, 0)
        Me.TableLayoutPanel38.Controls.Add(Me.btnDeleteMml, 2, 0)
        Me.TableLayoutPanel38.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel38.Location = New System.Drawing.Point(1, 1)
        Me.TableLayoutPanel38.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel38.Name = "TableLayoutPanel38"
        Me.TableLayoutPanel38.RowCount = 1
        Me.TableLayoutPanel38.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel38.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel38.Size = New System.Drawing.Size(390, 25)
        Me.TableLayoutPanel38.TabIndex = 6
        '
        'txtSearchMml
        '
        Me.txtSearchMml.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchMml.Location = New System.Drawing.Point(2, 2)
        Me.txtSearchMml.Margin = New System.Windows.Forms.Padding(2)
        Me.txtSearchMml.Name = "txtSearchMml"
        Me.txtSearchMml.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSearchMml.Properties.NullValuePrompt = "Search..."
        Me.txtSearchMml.Size = New System.Drawing.Size(266, 20)
        Me.txtSearchMml.TabIndex = 3
        '
        'btnRefreshMml
        '
        Me.btnRefreshMml.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRefreshMml.Location = New System.Drawing.Point(272, 2)
        Me.btnRefreshMml.Margin = New System.Windows.Forms.Padding(2)
        Me.btnRefreshMml.Name = "btnRefreshMml"
        Me.btnRefreshMml.Size = New System.Drawing.Size(56, 21)
        Me.btnRefreshMml.TabIndex = 4
        Me.btnRefreshMml.Text = "Refresh"
        '
        'btnDeleteMml
        '
        Me.btnDeleteMml.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDeleteMml.Enabled = False
        Me.btnDeleteMml.Location = New System.Drawing.Point(332, 2)
        Me.btnDeleteMml.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDeleteMml.Name = "btnDeleteMml"
        Me.btnDeleteMml.Size = New System.Drawing.Size(56, 21)
        Me.btnDeleteMml.TabIndex = 5
        Me.btnDeleteMml.Text = "Delete"
        '
        'xtcMmlTop
        '
        Me.xtcMmlTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtcMmlTop.Location = New System.Drawing.Point(0, 0)
        Me.xtcMmlTop.Name = "xtcMmlTop"
        Me.xtcMmlTop.SelectedTabPage = Me.tpValidation
        Me.xtcMmlTop.Size = New System.Drawing.Size(814, 378)
        Me.xtcMmlTop.TabIndex = 0
        Me.xtcMmlTop.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tpValidation, Me.tpData, Me.tpExcluded})
        '
        'tpValidation
        '
        Me.tpValidation.Controls.Add(Me.TableLayoutPanel14)
        Me.tpValidation.Name = "tpValidation"
        Me.tpValidation.Size = New System.Drawing.Size(812, 353)
        Me.tpValidation.Text = "Validation"
        '
        'TableLayoutPanel14
        '
        Me.TableLayoutPanel14.ColumnCount = 2
        Me.TableLayoutPanel14.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel14.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400.0!))
        Me.TableLayoutPanel14.Controls.Add(Me.gcValidation, 0, 0)
        Me.TableLayoutPanel14.Controls.Add(Me.TableLayoutPanel15, 1, 0)
        Me.TableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel14.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel14.Name = "TableLayoutPanel14"
        Me.TableLayoutPanel14.RowCount = 1
        Me.TableLayoutPanel14.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel14.Size = New System.Drawing.Size(812, 353)
        Me.TableLayoutPanel14.TabIndex = 0
        '
        'gcValidation
        '
        Me.gcValidation.AllowDrop = True
        Me.gcValidation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcValidation.Location = New System.Drawing.Point(2, 2)
        Me.gcValidation.MainView = Me.gvValidation
        Me.gcValidation.Margin = New System.Windows.Forms.Padding(2)
        Me.gcValidation.Name = "gcValidation"
        Me.gcValidation.Size = New System.Drawing.Size(408, 349)
        Me.gcValidation.TabIndex = 7
        Me.gcValidation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvValidation, Me.GridView8})
        '
        'gvValidation
        '
        Me.gvValidation.ActiveFilterEnabled = False
        Me.gvValidation.GridControl = Me.gcValidation
        Me.gvValidation.Name = "gvValidation"
        Me.gvValidation.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvValidation.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvValidation.OptionsBehavior.Editable = False
        Me.gvValidation.OptionsBehavior.ReadOnly = True
        Me.gvValidation.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvValidation.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvValidation.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvValidation.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvValidation.OptionsSelection.MultiSelect = True
        Me.gvValidation.OptionsView.ShowGroupPanel = False
        '
        'GridView8
        '
        Me.GridView8.GridControl = Me.gcValidation
        Me.GridView8.Name = "GridView8"
        '
        'TableLayoutPanel15
        '
        Me.TableLayoutPanel15.ColumnCount = 1
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel15.Controls.Add(Me.tvSelectionMml, 0, 1)
        Me.TableLayoutPanel15.Controls.Add(Me.grpMmlOutput, 0, 2)
        Me.TableLayoutPanel15.Controls.Add(Me.txtSearchMMLObject, 0, 0)
        Me.TableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel15.Location = New System.Drawing.Point(413, 1)
        Me.TableLayoutPanel15.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel15.Name = "TableLayoutPanel15"
        Me.TableLayoutPanel15.RowCount = 3
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel15.Size = New System.Drawing.Size(398, 351)
        Me.TableLayoutPanel15.TabIndex = 0
        '
        'tvSelectionMml
        '
        Me.tvSelectionMml.CheckBoxes = True
        Me.tvSelectionMml.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvSelectionMml.FullRowSelect = True
        Me.tvSelectionMml.Location = New System.Drawing.Point(3, 28)
        Me.tvSelectionMml.Name = "tvSelectionMml"
        Me.tvSelectionMml.ShowNodeToolTips = True
        Me.tvSelectionMml.Size = New System.Drawing.Size(392, 200)
        Me.tvSelectionMml.TabIndex = 8
        '
        'grpMmlOutput
        '
        Me.grpMmlOutput.Controls.Add(Me.TableLayoutPanel23)
        Me.grpMmlOutput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpMmlOutput.Location = New System.Drawing.Point(3, 234)
        Me.grpMmlOutput.Name = "grpMmlOutput"
        Me.grpMmlOutput.Size = New System.Drawing.Size(392, 114)
        Me.grpMmlOutput.TabIndex = 9
        Me.grpMmlOutput.Text = "Output"
        '
        'TableLayoutPanel23
        '
        Me.TableLayoutPanel23.ColumnCount = 1
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel23.Controls.Add(Me.btnGenerate, 0, 1)
        Me.TableLayoutPanel23.Controls.Add(Me.TableLayoutPanel57, 0, 0)
        Me.TableLayoutPanel23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel23.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel23.Name = "TableLayoutPanel23"
        Me.TableLayoutPanel23.RowCount = 3
        Me.TableLayoutPanel23.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52.0!))
        Me.TableLayoutPanel23.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel23.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel23.Size = New System.Drawing.Size(388, 89)
        Me.TableLayoutPanel23.TabIndex = 0
        '
        'btnGenerate
        '
        Me.btnGenerate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnGenerate.Location = New System.Drawing.Point(2, 54)
        Me.btnGenerate.Margin = New System.Windows.Forms.Padding(2)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(384, 26)
        Me.btnGenerate.TabIndex = 7
        Me.btnGenerate.Text = "Generate"
        '
        'TableLayoutPanel57
        '
        Me.TableLayoutPanel57.ColumnCount = 3
        Me.TableLayoutPanel57.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel57.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125.0!))
        Me.TableLayoutPanel57.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78.0!))
        Me.TableLayoutPanel57.Controls.Add(Me.cmbOutputLocation, 0, 1)
        Me.TableLayoutPanel57.Controls.Add(Me.txtFileNameSuffix, 1, 1)
        Me.TableLayoutPanel57.Controls.Add(Me.LabelControl12, 0, 0)
        Me.TableLayoutPanel57.Controls.Add(Me.LabelControl50, 1, 0)
        Me.TableLayoutPanel57.Controls.Add(Me.LabelControl51, 2, 0)
        Me.TableLayoutPanel57.Controls.Add(Me.seFileSize, 2, 1)
        Me.TableLayoutPanel57.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel57.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel57.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel57.Name = "TableLayoutPanel57"
        Me.TableLayoutPanel57.RowCount = 2
        Me.TableLayoutPanel57.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel57.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel57.Size = New System.Drawing.Size(388, 52)
        Me.TableLayoutPanel57.TabIndex = 10
        '
        'cmbOutputLocation
        '
        Me.cmbOutputLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbOutputLocation.EditValue = "Download Script - Single File"
        Me.cmbOutputLocation.Location = New System.Drawing.Point(3, 28)
        Me.cmbOutputLocation.Name = "cmbOutputLocation"
        Me.cmbOutputLocation.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbOutputLocation.Properties.Items.AddRange(New Object() {"Download Script - Single File", "Download Script - Split File"})
        Me.cmbOutputLocation.Size = New System.Drawing.Size(179, 20)
        Me.cmbOutputLocation.TabIndex = 9
        '
        'txtFileNameSuffix
        '
        Me.txtFileNameSuffix.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtFileNameSuffix.Location = New System.Drawing.Point(188, 28)
        Me.txtFileNameSuffix.Name = "txtFileNameSuffix"
        Me.txtFileNameSuffix.Size = New System.Drawing.Size(119, 20)
        Me.txtFileNameSuffix.TabIndex = 10
        '
        'LabelControl12
        '
        Me.LabelControl12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl12.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl12.Name = "LabelControl12"
        Me.LabelControl12.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl12.Size = New System.Drawing.Size(179, 19)
        Me.LabelControl12.TabIndex = 8
        Me.LabelControl12.Text = "Select Output Method"
        '
        'LabelControl50
        '
        Me.LabelControl50.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl50.Location = New System.Drawing.Point(188, 3)
        Me.LabelControl50.Name = "LabelControl50"
        Me.LabelControl50.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl50.Size = New System.Drawing.Size(119, 19)
        Me.LabelControl50.TabIndex = 11
        Me.LabelControl50.Text = "Add Filename Suffix"
        '
        'LabelControl51
        '
        Me.LabelControl51.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl51.Location = New System.Drawing.Point(313, 3)
        Me.LabelControl51.Name = "LabelControl51"
        Me.LabelControl51.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl51.Size = New System.Drawing.Size(72, 19)
        Me.LabelControl51.TabIndex = 12
        Me.LabelControl51.Text = "File Size (MB)"
        '
        'seFileSize
        '
        Me.seFileSize.Dock = System.Windows.Forms.DockStyle.Fill
        Me.seFileSize.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.seFileSize.Location = New System.Drawing.Point(313, 28)
        Me.seFileSize.Name = "seFileSize"
        Me.seFileSize.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.seFileSize.Properties.IsFloatValue = False
        Me.seFileSize.Properties.Mask.EditMask = "N00"
        Me.seFileSize.Properties.MaxValue = New Decimal(New Integer() {1024, 0, 0, 0})
        Me.seFileSize.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.seFileSize.Size = New System.Drawing.Size(72, 20)
        Me.seFileSize.TabIndex = 13
        '
        'txtSearchMMLObject
        '
        Me.txtSearchMMLObject.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchMMLObject.Location = New System.Drawing.Point(3, 3)
        Me.txtSearchMMLObject.Name = "txtSearchMMLObject"
        Me.txtSearchMMLObject.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSearchMMLObject.Properties.NullValuePrompt = "Search"
        Me.txtSearchMMLObject.Size = New System.Drawing.Size(392, 20)
        Me.txtSearchMMLObject.TabIndex = 10
        '
        'tpData
        '
        Me.tpData.Controls.Add(Me.gcData)
        Me.tpData.Name = "tpData"
        Me.tpData.Size = New System.Drawing.Size(812, 353)
        Me.tpData.Text = "Data"
        '
        'gcData
        '
        Me.gcData.AllowDrop = True
        Me.gcData.ContextMenuStrip = Me.cmsMapNB
        Me.gcData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcData.Location = New System.Drawing.Point(0, 0)
        Me.gcData.MainView = Me.gvData
        Me.gcData.Margin = New System.Windows.Forms.Padding(2)
        Me.gcData.Name = "gcData"
        Me.gcData.Size = New System.Drawing.Size(812, 353)
        Me.gcData.TabIndex = 8
        Me.gcData.Tag = "NBMmlData"
        Me.gcData.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvData, Me.GridView10})
        '
        'gvData
        '
        Me.gvData.ActiveFilterEnabled = False
        Me.gvData.GridControl = Me.gcData
        Me.gvData.Name = "gvData"
        Me.gvData.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvData.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvData.OptionsBehavior.Editable = False
        Me.gvData.OptionsBehavior.ReadOnly = True
        Me.gvData.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvData.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvData.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvData.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.gvData.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvData.OptionsSelection.MultiSelect = True
        Me.gvData.OptionsView.ShowGroupPanel = False
        '
        'GridView10
        '
        Me.GridView10.GridControl = Me.gcData
        Me.GridView10.Name = "GridView10"
        '
        'tpExcluded
        '
        Me.tpExcluded.Controls.Add(Me.gcExcluded)
        Me.tpExcluded.Name = "tpExcluded"
        Me.tpExcluded.Size = New System.Drawing.Size(812, 353)
        Me.tpExcluded.Text = "Excluded"
        '
        'gcExcluded
        '
        Me.gcExcluded.AllowDrop = True
        Me.gcExcluded.ContextMenuStrip = Me.cmsMapNB
        Me.gcExcluded.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcExcluded.Location = New System.Drawing.Point(0, 0)
        Me.gcExcluded.MainView = Me.gvExcluded
        Me.gcExcluded.Margin = New System.Windows.Forms.Padding(2)
        Me.gcExcluded.Name = "gcExcluded"
        Me.gcExcluded.Size = New System.Drawing.Size(812, 353)
        Me.gcExcluded.TabIndex = 9
        Me.gcExcluded.Tag = "NBMmlExcluded"
        Me.gcExcluded.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvExcluded, Me.GridView11})
        '
        'gvExcluded
        '
        Me.gvExcluded.ActiveFilterEnabled = False
        Me.gvExcluded.GridControl = Me.gcExcluded
        Me.gvExcluded.Name = "gvExcluded"
        Me.gvExcluded.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvExcluded.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvExcluded.OptionsBehavior.Editable = False
        Me.gvExcluded.OptionsBehavior.ReadOnly = True
        Me.gvExcluded.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvExcluded.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvExcluded.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvExcluded.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.gvExcluded.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvExcluded.OptionsSelection.MultiSelect = True
        Me.gvExcluded.OptionsView.ShowGroupPanel = False
        '
        'GridView11
        '
        Me.GridView11.GridControl = Me.gcExcluded
        Me.GridView11.Name = "GridView11"
        '
        'grpMmlConfig
        '
        Me.grpMmlConfig.Controls.Add(Me.sccMmlBottom)
        Me.grpMmlConfig.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpMmlConfig.Location = New System.Drawing.Point(0, 0)
        Me.grpMmlConfig.Name = "grpMmlConfig"
        Me.grpMmlConfig.Size = New System.Drawing.Size(1220, 300)
        Me.grpMmlConfig.TabIndex = 1
        Me.grpMmlConfig.Text = "MML/XML Configuration"
        '
        'sccMmlBottom
        '
        Me.sccMmlBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccMmlBottom.Location = New System.Drawing.Point(2, 23)
        Me.sccMmlBottom.Name = "sccMmlBottom"
        '
        'sccMmlBottom.Panel1
        '
        Me.sccMmlBottom.Panel1.Controls.Add(Me.GroupControl1)
        Me.sccMmlBottom.Panel1.MinSize = 300
        Me.sccMmlBottom.Panel1.Text = "Panel1"
        '
        'sccMmlBottom.Panel2
        '
        Me.sccMmlBottom.Panel2.Controls.Add(Me.xtcMmlBottom)
        Me.sccMmlBottom.Panel2.MinSize = 500
        Me.sccMmlBottom.Panel2.Text = "Panel2"
        Me.sccMmlBottom.Size = New System.Drawing.Size(1216, 275)
        Me.sccMmlBottom.SplitterPosition = 364
        Me.sccMmlBottom.TabIndex = 0
        Me.sccMmlBottom.Text = "SplitContainerControl1"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.TableLayoutPanel10)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(364, 275)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "MML/XML Configurations"
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 1
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Controls.Add(Me.GroupControl2, 0, 2)
        Me.TableLayoutPanel10.Controls.Add(Me.gcMmlConfig, 0, 1)
        Me.TableLayoutPanel10.Controls.Add(Me.TableLayoutPanel12, 0, 0)
        Me.TableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 3
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62.0!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(360, 250)
        Me.TableLayoutPanel10.TabIndex = 1
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.TableLayoutPanel11)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(2, 190)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(356, 58)
        Me.GroupControl2.TabIndex = 4
        Me.GroupControl2.Text = "Campaign Properties"
        '
        'TableLayoutPanel11
        '
        Me.TableLayoutPanel11.ColumnCount = 4
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel11.Controls.Add(Me.LabelControl7, 0, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.lblOwnerMmlConfig, 1, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.LabelControl44, 2, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.ceIsPublicMML, 3, 0)
        Me.TableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel11.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel11.Name = "TableLayoutPanel11"
        Me.TableLayoutPanel11.RowCount = 2
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel11.Size = New System.Drawing.Size(352, 33)
        Me.TableLayoutPanel11.TabIndex = 0
        '
        'LabelControl7
        '
        Me.LabelControl7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl7.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl7.Size = New System.Drawing.Size(44, 20)
        Me.LabelControl7.TabIndex = 0
        Me.LabelControl7.Text = "Owner"
        '
        'lblOwnerMmlConfig
        '
        Me.lblOwnerMmlConfig.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblOwnerMmlConfig.Location = New System.Drawing.Point(53, 3)
        Me.lblOwnerMmlConfig.Name = "lblOwnerMmlConfig"
        Me.lblOwnerMmlConfig.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblOwnerMmlConfig.Size = New System.Drawing.Size(115, 20)
        Me.lblOwnerMmlConfig.TabIndex = 1
        '
        'LabelControl44
        '
        Me.LabelControl44.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl44.Location = New System.Drawing.Point(174, 3)
        Me.LabelControl44.Name = "LabelControl44"
        Me.LabelControl44.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl44.Size = New System.Drawing.Size(54, 20)
        Me.LabelControl44.TabIndex = 2
        Me.LabelControl44.Text = "Is Public"
        '
        'ceIsPublicMML
        '
        Me.ceIsPublicMML.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceIsPublicMML.Location = New System.Drawing.Point(236, 3)
        Me.ceIsPublicMML.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceIsPublicMML.Name = "ceIsPublicMML"
        Me.ceIsPublicMML.Properties.Caption = ""
        Me.ceIsPublicMML.Size = New System.Drawing.Size(113, 20)
        Me.ceIsPublicMML.TabIndex = 3
        Me.ceIsPublicMML.Tag = "MML"
        '
        'gcMmlConfig
        '
        Me.gcMmlConfig.AllowDrop = True
        Me.gcMmlConfig.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcMmlConfig.Location = New System.Drawing.Point(2, 30)
        Me.gcMmlConfig.MainView = Me.gvMmlConfig
        Me.gcMmlConfig.Margin = New System.Windows.Forms.Padding(2)
        Me.gcMmlConfig.Name = "gcMmlConfig"
        Me.gcMmlConfig.Size = New System.Drawing.Size(356, 156)
        Me.gcMmlConfig.TabIndex = 5
        Me.gcMmlConfig.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvMmlConfig, Me.GridView6})
        '
        'gvMmlConfig
        '
        Me.gvMmlConfig.ActiveFilterEnabled = False
        Me.gvMmlConfig.GridControl = Me.gcMmlConfig
        Me.gvMmlConfig.Name = "gvMmlConfig"
        Me.gvMmlConfig.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvMmlConfig.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvMmlConfig.OptionsBehavior.Editable = False
        Me.gvMmlConfig.OptionsBehavior.ReadOnly = True
        Me.gvMmlConfig.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvMmlConfig.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvMmlConfig.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvMmlConfig.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvMmlConfig.OptionsSelection.MultiSelect = True
        Me.gvMmlConfig.OptionsView.ShowGroupPanel = False
        '
        'GridView6
        '
        Me.GridView6.GridControl = Me.gcMmlConfig
        Me.GridView6.Name = "GridView6"
        '
        'TableLayoutPanel12
        '
        Me.TableLayoutPanel12.ColumnCount = 3
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel12.Controls.Add(Me.txtMmlConfigSearch, 0, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.btnMmlConfigClone, 1, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.btnMmlConfigDelete, 2, 0)
        Me.TableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel12.Location = New System.Drawing.Point(1, 1)
        Me.TableLayoutPanel12.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel12.Name = "TableLayoutPanel12"
        Me.TableLayoutPanel12.RowCount = 1
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel12.Size = New System.Drawing.Size(358, 26)
        Me.TableLayoutPanel12.TabIndex = 6
        '
        'txtMmlConfigSearch
        '
        Me.txtMmlConfigSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMmlConfigSearch.Location = New System.Drawing.Point(2, 2)
        Me.txtMmlConfigSearch.Margin = New System.Windows.Forms.Padding(2)
        Me.txtMmlConfigSearch.Name = "txtMmlConfigSearch"
        Me.txtMmlConfigSearch.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtMmlConfigSearch.Properties.NullValuePrompt = "Search..."
        Me.txtMmlConfigSearch.Size = New System.Drawing.Size(234, 20)
        Me.txtMmlConfigSearch.TabIndex = 3
        '
        'btnMmlConfigClone
        '
        Me.btnMmlConfigClone.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnMmlConfigClone.Location = New System.Drawing.Point(240, 2)
        Me.btnMmlConfigClone.Margin = New System.Windows.Forms.Padding(2)
        Me.btnMmlConfigClone.Name = "btnMmlConfigClone"
        Me.btnMmlConfigClone.Size = New System.Drawing.Size(56, 22)
        Me.btnMmlConfigClone.TabIndex = 5
        Me.btnMmlConfigClone.Text = "Clone"
        '
        'btnMmlConfigDelete
        '
        Me.btnMmlConfigDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnMmlConfigDelete.Location = New System.Drawing.Point(300, 2)
        Me.btnMmlConfigDelete.Margin = New System.Windows.Forms.Padding(2)
        Me.btnMmlConfigDelete.Name = "btnMmlConfigDelete"
        Me.btnMmlConfigDelete.Size = New System.Drawing.Size(56, 22)
        Me.btnMmlConfigDelete.TabIndex = 6
        Me.btnMmlConfigDelete.Text = "Delete"
        '
        'xtcMmlBottom
        '
        Me.xtcMmlBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtcMmlBottom.Location = New System.Drawing.Point(0, 0)
        Me.xtcMmlBottom.Name = "xtcMmlBottom"
        Me.xtcMmlBottom.SelectedTabPage = Me.tpScriptsNB
        Me.xtcMmlBottom.Size = New System.Drawing.Size(842, 275)
        Me.xtcMmlBottom.TabIndex = 0
        Me.xtcMmlBottom.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tpScriptsNB, Me.tpScriptsExtcell, Me.tpStaticScripts})
        '
        'tpScriptsNB
        '
        Me.tpScriptsNB.Controls.Add(Me.gcScriptsNB)
        Me.tpScriptsNB.Name = "tpScriptsNB"
        Me.tpScriptsNB.Size = New System.Drawing.Size(840, 250)
        Me.tpScriptsNB.Text = "SCRIPTS NB"
        '
        'gcScriptsNB
        '
        Me.gcScriptsNB.AllowDrop = True
        Me.gcScriptsNB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcScriptsNB.Location = New System.Drawing.Point(0, 0)
        Me.gcScriptsNB.MainView = Me.gvScriptsNB
        Me.gcScriptsNB.Margin = New System.Windows.Forms.Padding(2)
        Me.gcScriptsNB.Name = "gcScriptsNB"
        Me.gcScriptsNB.Size = New System.Drawing.Size(840, 250)
        Me.gcScriptsNB.TabIndex = 6
        Me.gcScriptsNB.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvScriptsNB, Me.GridView7})
        '
        'gvScriptsNB
        '
        Me.gvScriptsNB.ActiveFilterEnabled = False
        Me.gvScriptsNB.GridControl = Me.gcScriptsNB
        Me.gvScriptsNB.Name = "gvScriptsNB"
        Me.gvScriptsNB.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvScriptsNB.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvScriptsNB.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvScriptsNB.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvScriptsNB.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvScriptsNB.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvScriptsNB.OptionsSelection.MultiSelect = True
        Me.gvScriptsNB.OptionsView.ShowGroupPanel = False
        '
        'GridView7
        '
        Me.GridView7.GridControl = Me.gcScriptsNB
        Me.GridView7.Name = "GridView7"
        '
        'tpScriptsExtcell
        '
        Me.tpScriptsExtcell.Controls.Add(Me.gcScriptsExtCell)
        Me.tpScriptsExtcell.Name = "tpScriptsExtcell"
        Me.tpScriptsExtcell.Size = New System.Drawing.Size(840, 250)
        Me.tpScriptsExtcell.Text = "SCRIPTS EXTCELL"
        '
        'gcScriptsExtCell
        '
        Me.gcScriptsExtCell.AllowDrop = True
        Me.gcScriptsExtCell.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcScriptsExtCell.Location = New System.Drawing.Point(0, 0)
        Me.gcScriptsExtCell.MainView = Me.gvScriptsExtCell
        Me.gcScriptsExtCell.Margin = New System.Windows.Forms.Padding(2)
        Me.gcScriptsExtCell.Name = "gcScriptsExtCell"
        Me.gcScriptsExtCell.Size = New System.Drawing.Size(840, 250)
        Me.gcScriptsExtCell.TabIndex = 7
        Me.gcScriptsExtCell.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvScriptsExtCell, Me.GridView12})
        '
        'gvScriptsExtCell
        '
        Me.gvScriptsExtCell.ActiveFilterEnabled = False
        Me.gvScriptsExtCell.GridControl = Me.gcScriptsExtCell
        Me.gvScriptsExtCell.Name = "gvScriptsExtCell"
        Me.gvScriptsExtCell.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvScriptsExtCell.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvScriptsExtCell.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvScriptsExtCell.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvScriptsExtCell.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvScriptsExtCell.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvScriptsExtCell.OptionsSelection.MultiSelect = True
        Me.gvScriptsExtCell.OptionsView.ShowGroupPanel = False
        '
        'GridView12
        '
        Me.GridView12.GridControl = Me.gcScriptsExtCell
        Me.GridView12.Name = "GridView12"
        '
        'tpStaticScripts
        '
        Me.tpStaticScripts.Controls.Add(Me.gcStaticScripts)
        Me.tpStaticScripts.Name = "tpStaticScripts"
        Me.tpStaticScripts.Size = New System.Drawing.Size(840, 250)
        Me.tpStaticScripts.Text = "STATIC SCRIPTS"
        '
        'gcStaticScripts
        '
        Me.gcStaticScripts.AllowDrop = True
        Me.gcStaticScripts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcStaticScripts.Location = New System.Drawing.Point(0, 0)
        Me.gcStaticScripts.MainView = Me.gvStaticScripts
        Me.gcStaticScripts.Margin = New System.Windows.Forms.Padding(2)
        Me.gcStaticScripts.Name = "gcStaticScripts"
        Me.gcStaticScripts.Size = New System.Drawing.Size(840, 250)
        Me.gcStaticScripts.TabIndex = 7
        Me.gcStaticScripts.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvStaticScripts, Me.GridView22})
        '
        'gvStaticScripts
        '
        Me.gvStaticScripts.ActiveFilterEnabled = False
        Me.gvStaticScripts.GridControl = Me.gcStaticScripts
        Me.gvStaticScripts.Name = "gvStaticScripts"
        Me.gvStaticScripts.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvStaticScripts.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvStaticScripts.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvStaticScripts.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvStaticScripts.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvStaticScripts.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvStaticScripts.OptionsSelection.MultiSelect = True
        Me.gvStaticScripts.OptionsView.ShowGroupPanel = False
        '
        'GridView22
        '
        Me.GridView22.GridControl = Me.gcStaticScripts
        Me.GridView22.Name = "GridView22"
        '
        'GridView3
        '
        Me.GridView3.Name = "GridView3"
        '
        'GridView15
        '
        Me.GridView15.Name = "GridView15"
        '
        'GridView18
        '
        Me.GridView18.Name = "GridView18"
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.xtcMain, 0, 1)
        Me.tlpMain.Controls.Add(Me.lblIntegrityMsg, 0, 0)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 2
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Size = New System.Drawing.Size(1228, 749)
        Me.tlpMain.TabIndex = 2
        '
        'lblIntegrityMsg
        '
        Me.lblIntegrityMsg.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIntegrityMsg.Appearance.Image = CType(resources.GetObject("lblIntegrityMsg.Appearance.Image"), System.Drawing.Image)
        Me.lblIntegrityMsg.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblIntegrityMsg.Appearance.Options.UseFont = True
        Me.lblIntegrityMsg.Appearance.Options.UseImage = True
        Me.lblIntegrityMsg.Appearance.Options.UseImageAlign = True
        Me.lblIntegrityMsg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblIntegrityMsg.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.lblIntegrityMsg.Location = New System.Drawing.Point(3, 3)
        Me.lblIntegrityMsg.Name = "lblIntegrityMsg"
        Me.lblIntegrityMsg.Padding = New System.Windows.Forms.Padding(3)
        Me.lblIntegrityMsg.Size = New System.Drawing.Size(1222, 24)
        Me.lblIntegrityMsg.TabIndex = 1
        Me.lblIntegrityMsg.Text = "Warning - Check Data Integrity"
        '
        'frmNBManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1228, 749)
        Me.Controls.Add(Me.tlpMain)
        Me.IconOptions.Icon = CType(resources.GetObject("frmNBManagement.IconOptions.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1000, 700)
        Me.Name = "frmNBManagement"
        Me.Text = "Neighbor Management"
        CType(Me.xtcMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtcMain.ResumeLayout(False)
        Me.tpNBDetect.ResumeLayout(False)
        CType(Me.sccDetectCamp.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccDetectCamp.Panel1.ResumeLayout(False)
        CType(Me.sccDetectCamp.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccDetectCamp.Panel2.ResumeLayout(False)
        CType(Me.sccDetectCamp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccDetectCamp.ResumeLayout(False)
        CType(Me.sccLeft.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccLeft.Panel1.ResumeLayout(False)
        CType(Me.sccLeft.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccLeft.Panel2.ResumeLayout(False)
        CType(Me.sccLeft, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccLeft.ResumeLayout(False)
        CType(Me.grpCampDetect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCampDetect.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.grpCampPropDetect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCampPropDetect.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        CType(Me.ceActiveDetect.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deSchNxtStartTimeDetect.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deSchNxtStartTimeDetect.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSchRptIntervalDetect.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceIsPublicDetect.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcDetectCampaigns, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetectCampaigns, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.txtSearchDetect.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel7.ResumeLayout(False)
        CType(Me.grpCampSummDetect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCampSummDetect.ResumeLayout(False)
        Me.TableLayoutPanel28.ResumeLayout(False)
        Me.TableLayoutPanel33.ResumeLayout(False)
        Me.TableLayoutPanel33.PerformLayout()
        CType(Me.cmbDetectResultSetID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcCampSummDetect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtcCampSummDetect.ResumeLayout(False)
        Me.tpCampDetectSumm.ResumeLayout(False)
        CType(Me.gcCampSummDetect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCampSummDetect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpCampDetectData.ResumeLayout(False)
        Me.TableLayoutPanel34.ResumeLayout(False)
        CType(Me.gcCampDataDetect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsMapNB.ResumeLayout(False)
        CType(Me.gvCampDataDetect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView21, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel35.ResumeLayout(False)
        Me.TableLayoutPanel35.PerformLayout()
        Me.TableLayoutPanel21.ResumeLayout(False)
        CType(Me.grpConfigSummDetect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpConfigSummDetect.ResumeLayout(False)
        Me.TableLayoutPanel37.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        CType(Me.grpLayerPropDetect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpLayerPropDetect.ResumeLayout(False)
        Me.TableLayoutPanel59.ResumeLayout(False)
        CType(Me.ceApplyConfigAllDetect.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcConfigSummDetect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsConfigurationSummary.ResumeLayout(False)
        CType(Me.gvConfigSummDetect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpNBCopy.ResumeLayout(False)
        CType(Me.sccCopyCamp.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccCopyCamp.Panel1.ResumeLayout(False)
        CType(Me.sccCopyCamp.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccCopyCamp.Panel2.ResumeLayout(False)
        CType(Me.sccCopyCamp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccCopyCamp.ResumeLayout(False)
        CType(Me.SplitContainerControl2.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl2.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.ResumeLayout(False)
        CType(Me.grpCampCopy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCampCopy.ResumeLayout(False)
        Me.TableLayoutPanel16.ResumeLayout(False)
        CType(Me.grpCampPropCopy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCampPropCopy.ResumeLayout(False)
        Me.TableLayoutPanel17.ResumeLayout(False)
        Me.TableLayoutPanel17.PerformLayout()
        CType(Me.ceActiveCopy.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deSchNxtStartTimeCopy.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deSchNxtStartTimeCopy.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSchRptIntervalCopy.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceIsPublicCopy.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcCopyCampaigns, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCopyCampaigns, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel18.ResumeLayout(False)
        CType(Me.txtSearchCopy.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel19.ResumeLayout(False)
        CType(Me.grpCampSummCopy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCampSummCopy.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel29.ResumeLayout(False)
        Me.TableLayoutPanel29.PerformLayout()
        CType(Me.cmbCopyResultSetID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcCampSummCopy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtcCampSummCopy.ResumeLayout(False)
        Me.tpCampCopySumm.ResumeLayout(False)
        CType(Me.gcCampSummCopy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCampSummCopy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView14, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpCampCopyData.ResumeLayout(False)
        Me.TableLayoutPanel6.ResumeLayout(False)
        CType(Me.gcCampDataCopy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCampDataCopy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView13, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel36.ResumeLayout(False)
        Me.TableLayoutPanel36.PerformLayout()
        CType(Me.grpConfigSummCopy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpConfigSummCopy.ResumeLayout(False)
        Me.TableLayoutPanel22.ResumeLayout(False)
        CType(Me.gcConfigSummCopy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvConfigSummCopy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView16, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel20.ResumeLayout(False)
        CType(Me.grpLayerPropCopy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpLayerPropCopy.ResumeLayout(False)
        Me.TableLayoutPanel60.ResumeLayout(False)
        CType(Me.ceApplyConfigAllCopy.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpNBDelete.ResumeLayout(False)
        CType(Me.sccDeleteCamp.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccDeleteCamp.Panel1.ResumeLayout(False)
        CType(Me.sccDeleteCamp.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccDeleteCamp.Panel2.ResumeLayout(False)
        CType(Me.sccDeleteCamp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccDeleteCamp.ResumeLayout(False)
        CType(Me.SplitContainerControl8.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl8.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl8.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl8.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl8.ResumeLayout(False)
        CType(Me.grpCampDelete, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCampDelete.ResumeLayout(False)
        Me.TableLayoutPanel63.ResumeLayout(False)
        CType(Me.grpCampPropDelete, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCampPropDelete.ResumeLayout(False)
        Me.TableLayoutPanel64.ResumeLayout(False)
        Me.TableLayoutPanel64.PerformLayout()
        CType(Me.ceActiveDelete.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deSchNxtStartTimeDelete.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deSchNxtStartTimeDelete.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSchRptIntervalDelete.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceIsPublicDelete.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcDeleteCampaigns, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDeleteCampaigns, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView27, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel65.ResumeLayout(False)
        CType(Me.txtSearchDelete.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel66.ResumeLayout(False)
        CType(Me.grpCampSummDelete, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCampSummDelete.ResumeLayout(False)
        Me.TableLayoutPanel67.ResumeLayout(False)
        Me.TableLayoutPanel68.ResumeLayout(False)
        Me.TableLayoutPanel68.PerformLayout()
        CType(Me.cmbResultSetIDDelete.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XtraTabControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl2.ResumeLayout(False)
        Me.XtraTabPage3.ResumeLayout(False)
        CType(Me.gcCampSummDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCampSummDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView31, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage4.ResumeLayout(False)
        Me.TableLayoutPanel69.ResumeLayout(False)
        CType(Me.gcCampDataDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCampDataDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView33, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel70.ResumeLayout(False)
        Me.TableLayoutPanel70.PerformLayout()
        CType(Me.grpConfigSummDelete, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpConfigSummDelete.ResumeLayout(False)
        Me.TableLayoutPanel71.ResumeLayout(False)
        CType(Me.gcConfigSummDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvConfigSummDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView35, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel72.ResumeLayout(False)
        CType(Me.grpLayerPropDelete, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpLayerPropDelete.ResumeLayout(False)
        Me.TableLayoutPanel73.ResumeLayout(False)
        CType(Me.ceApplyConfigAllDelete.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpNBManual.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.SplitContainerControl4.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl4.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl4.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl4.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl4.ResumeLayout(False)
        CType(Me.grpCampManual, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCampManual.ResumeLayout(False)
        Me.TableLayoutPanel8.ResumeLayout(False)
        CType(Me.grpCampPropManual, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCampPropManual.ResumeLayout(False)
        Me.TableLayoutPanel9.ResumeLayout(False)
        Me.TableLayoutPanel9.PerformLayout()
        CType(Me.ceIsPublicManual.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcCampaignManual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCampaignManual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView19, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel25.ResumeLayout(False)
        CType(Me.txtSearchManual.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel26.ResumeLayout(False)
        CType(Me.grpCampSummManual, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCampSummManual.ResumeLayout(False)
        Me.TableLayoutPanel32.ResumeLayout(False)
        Me.TableLayoutPanel32.PerformLayout()
        CType(Me.cmbManualResultSetID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcCampSummManual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCampSummManual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpManual, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpManual.ResumeLayout(False)
        Me.TableLayoutPanel30.ResumeLayout(False)
        Me.TableLayoutPanel30.PerformLayout()
        CType(Me.gcManual, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmManualPaste.ResumeLayout(False)
        CType(Me.gvManual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView23, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel31.ResumeLayout(False)
        Me.TableLayoutPanel31.PerformLayout()
        Me.tpNBAudit.ResumeLayout(False)
        CType(Me.SplitContainerControl5.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl5.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl5.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl5.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl5.ResumeLayout(False)
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.TableLayoutPanel39.ResumeLayout(False)
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl5.ResumeLayout(False)
        Me.TableLayoutPanel40.ResumeLayout(False)
        Me.TableLayoutPanel40.PerformLayout()
        CType(Me.chkActiveNBAudit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpStartTimeNBAudit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpStartTimeNBAudit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbRepeatIntervalNBAudit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceIsPublicAudit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcCampNBAudit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCampNBAudit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView24, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel41.ResumeLayout(False)
        CType(Me.txtNBAuditCampSearch.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel42.ResumeLayout(False)
        CType(Me.grpConfigGen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpConfigGen.ResumeLayout(False)
        Me.TableLayoutPanel47.ResumeLayout(False)
        Me.TableLayoutPanel47.PerformLayout()
        CType(Me.cmbMMLConfigIDNBAudit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTechnologyNBAudit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbInclusionListNBAudit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpOptionalSettings, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpOptionalSettings.ResumeLayout(False)
        Me.TableLayoutPanel50.ResumeLayout(False)
        Me.TableLayoutPanel50.PerformLayout()
        CType(Me.cmbExclusionListNBAudit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSLayerNBAudit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTLayerNBAudit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbNBType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbMMLScriptID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitContainerControl3.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl3.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl3.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl3.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl3.ResumeLayout(False)
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl6.ResumeLayout(False)
        Me.TableLayoutPanel43.ResumeLayout(False)
        Me.TableLayoutPanel44.ResumeLayout(False)
        Me.TableLayoutPanel44.PerformLayout()
        CType(Me.cmbResultSetIdNBAudit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        CType(Me.gcResultSummaryNBAudit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvResultSummaryNBAudit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView26, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage2.ResumeLayout(False)
        Me.TableLayoutPanel45.ResumeLayout(False)
        CType(Me.gcResultDataNBAudit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvResultDataNBAudit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView28, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel46.ResumeLayout(False)
        Me.TableLayoutPanel46.PerformLayout()
        CType(Me.grpConfigSummaryNBAudit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpConfigSummaryNBAudit.ResumeLayout(False)
        Me.TableLayoutPanel48.ResumeLayout(False)
        Me.TableLayoutPanel49.ResumeLayout(False)
        CType(Me.GroupControl8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl8.ResumeLayout(False)
        Me.TableLayoutPanel61.ResumeLayout(False)
        CType(Me.ceApplyConfigAllAudit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcConfigNBAudit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvConfigNBAudit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView30, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpNBFetch.ResumeLayout(False)
        CType(Me.SplitContainerControl6.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl6.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl6.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl6.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl6.ResumeLayout(False)
        CType(Me.gcSelectObjects, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcSelectObjects.ResumeLayout(False)
        Me.TableLayoutPanel51.ResumeLayout(False)
        CType(Me.gcObjectTree, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcObjectTree.ResumeLayout(False)
        Me.TableLayoutPanel52.ResumeLayout(False)
        Me.TableLayoutPanel53.ResumeLayout(False)
        Me.TableLayoutPanel53.PerformLayout()
        CType(Me.txtSearchObject.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel54.ResumeLayout(False)
        Me.TableLayoutPanel54.PerformLayout()
        CType(Me.cmbObjectType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel55.ResumeLayout(False)
        Me.TableLayoutPanel55.PerformLayout()
        CType(Me.cmbTechnology.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel56.ResumeLayout(False)
        Me.TableLayoutPanel56.PerformLayout()
        Me.TableLayoutPanel62.ResumeLayout(False)
        Me.TableLayoutPanel62.PerformLayout()
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl7.ResumeLayout(False)
        CType(Me.gcNBFetch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvNBFetch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView25, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpMML.ResumeLayout(False)
        CType(Me.sccMML.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMML.Panel1.ResumeLayout(False)
        CType(Me.sccMML.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMML.Panel2.ResumeLayout(False)
        CType(Me.sccMML, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMML.ResumeLayout(False)
        CType(Me.sccMmlTop.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMmlTop.Panel1.ResumeLayout(False)
        CType(Me.sccMmlTop.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMmlTop.Panel2.ResumeLayout(False)
        CType(Me.sccMmlTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMmlTop.ResumeLayout(False)
        CType(Me.grpMmlInput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMmlInput.ResumeLayout(False)
        Me.TableLayoutPanel24.ResumeLayout(False)
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.TableLayoutPanel27.ResumeLayout(False)
        Me.TableLayoutPanel27.PerformLayout()
        Me.TableLayoutPanel13.ResumeLayout(False)
        Me.TableLayoutPanel13.PerformLayout()
        CType(Me.cmbMMLConfig.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel58.ResumeLayout(False)
        CType(Me.gcMmlCampaign, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmMMLCampaign.ResumeLayout(False)
        CType(Me.gvMmlCampaign, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel38.ResumeLayout(False)
        CType(Me.txtSearchMml.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcMmlTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtcMmlTop.ResumeLayout(False)
        Me.tpValidation.ResumeLayout(False)
        Me.TableLayoutPanel14.ResumeLayout(False)
        CType(Me.gcValidation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvValidation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel15.ResumeLayout(False)
        CType(Me.grpMmlOutput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMmlOutput.ResumeLayout(False)
        Me.TableLayoutPanel23.ResumeLayout(False)
        Me.TableLayoutPanel57.ResumeLayout(False)
        Me.TableLayoutPanel57.PerformLayout()
        CType(Me.cmbOutputLocation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFileNameSuffix.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.seFileSize.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSearchMMLObject.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpData.ResumeLayout(False)
        CType(Me.gcData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpExcluded.ResumeLayout(False)
        CType(Me.gcExcluded, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvExcluded, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpMmlConfig, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMmlConfig.ResumeLayout(False)
        CType(Me.sccMmlBottom.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMmlBottom.Panel1.ResumeLayout(False)
        CType(Me.sccMmlBottom.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMmlBottom.Panel2.ResumeLayout(False)
        CType(Me.sccMmlBottom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMmlBottom.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.TableLayoutPanel10.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.TableLayoutPanel11.ResumeLayout(False)
        Me.TableLayoutPanel11.PerformLayout()
        CType(Me.ceIsPublicMML.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcMmlConfig, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvMmlConfig, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel12.ResumeLayout(False)
        CType(Me.txtMmlConfigSearch.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcMmlBottom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtcMmlBottom.ResumeLayout(False)
        Me.tpScriptsNB.ResumeLayout(False)
        CType(Me.gcScriptsNB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvScriptsNB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpScriptsExtcell.ResumeLayout(False)
        CType(Me.gcScriptsExtCell, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvScriptsExtCell, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView12, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpStaticScripts.ResumeLayout(False)
        CType(Me.gcStaticScripts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvStaticScripts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView18, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents xtcMain As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tpNBDetect As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents sccDetectCamp As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents sccLeft As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents grpCampDetect As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grpCampSummDetect As DevExpress.XtraEditors.GroupControl
    Friend WithEvents tpMML As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gcCampSummDetect As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvCampSummDetect As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grpCampPropDetect As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gcDetectCampaigns As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvDetectCampaigns As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtSearchDetect As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents btnCloneDetect As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDeleteDetect As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblLastRunTimeDetect As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblLastEndTimeDetect As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnRunNowDetect As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblOwnerDetect As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ceActiveDetect As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents grpConfigSummDetect As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents grpLayerPropDetect As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gcConfigSummDetect As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvConfigSummDetect As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btnListMngrDetect As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel7 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents layerPropGridDetect As System.Windows.Forms.PropertyGrid
    Friend WithEvents sccMML As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents sccMmlTop As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents grpMmlInput As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grpMmlConfig As DevExpress.XtraEditors.GroupControl
    Friend WithEvents sccMmlBottom As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel10 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel11 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gcMmlConfig As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvMmlConfig As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel12 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtMmlConfigSearch As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents btnMmlConfigClone As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnMmlConfigDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel13 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnValidate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmbMMLConfig As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbOutputLocation As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents xtcMmlTop As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tpValidation As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tpData As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tpExcluded As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents xtcMmlBottom As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tpScriptsNB As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gcScriptsNB As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvScriptsNB As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView7 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tpScriptsExtcell As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TableLayoutPanel14 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gcValidation As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvValidation As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView8 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel15 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnGenerate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tvSelectionMml As System.Windows.Forms.TreeView
    Friend WithEvents gcData As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvData As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView10 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcExcluded As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvExcluded As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView11 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcScriptsExtCell As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvScriptsExtCell As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView12 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents deSchNxtStartTimeDetect As DevExpress.XtraEditors.DateEdit
    Friend WithEvents cmbSchRptIntervalDetect As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents tpNBCopy As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tpNBManual As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents sccCopyCamp As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents SplitContainerControl2 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents grpCampCopy As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel16 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents grpCampPropCopy As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel17 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl15 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl16 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl17 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl18 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl19 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblLastRunTimeCopy As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblLastEndTimeCopy As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnRunNowCopy As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblOwnerCopy As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ceActiveCopy As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents deSchNxtStartTimeCopy As DevExpress.XtraEditors.DateEdit
    Friend WithEvents cmbSchRptIntervalCopy As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents gcCopyCampaigns As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvCopyCampaigns As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView9 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel18 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtSearchCopy As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents btnCloneCopy As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDeleteCopy As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel19 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents grpCampSummCopy As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grpConfigSummCopy As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gcConfigSummCopy As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvConfigSummCopy As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView16 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel20 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents grpLayerPropCopy As DevExpress.XtraEditors.GroupControl
    Friend WithEvents layerPropGridCopy As System.Windows.Forms.PropertyGrid
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents SplitContainerControl4 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents grpCampManual As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel8 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents grpCampPropManual As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel9 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblOwnerManual As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gcCampaignManual As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvCampaignManual As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView19 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel25 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtSearchManual As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents btnAddManual As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDeleteManual As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel26 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents grpCampSummManual As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel30 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl33 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl34 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl35 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel31 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblRecordsCountManual As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnCommitManual As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents grpManual As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btnCloneManual As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents grpMmlOutput As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel23 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel24 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel27 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblOwnerMmlInput As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gcMmlCampaign As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvMmlCampaign As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtSearchMml As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblOwnerMmlConfig As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmManualPaste As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmiTagPastePaste As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TableLayoutPanel28 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cmbDetectResultSetID As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl13 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel29 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cmbCopyResultSetID As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl20 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gcCampSummCopy As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvCampSummCopy As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView14 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtSearchMMLObject As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents TableLayoutPanel32 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cmbManualResultSetID As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl21 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gcCampSummManual As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvCampSummManual As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView20 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel33 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents xtcCampSummDetect As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tpCampDetectSumm As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tpCampDetectData As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TableLayoutPanel34 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gcCampDataDetect As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvCampDataDetect As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView21 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel35 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnDetectDataLoadGrid As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDetectDataAllCsv As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents xtcCampSummCopy As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tpCampCopySumm As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tpCampCopyData As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TableLayoutPanel6 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gcCampDataCopy As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvCampDataCopy As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView13 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel36 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnCopyDataLoadGrid As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCopyDataAllCsv As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnListMngrCopy As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView15 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView18 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel21 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel22 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel37 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblLastEndTimeMml As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel38 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnRefreshMml As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDeleteMml As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tpStaticScripts As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gcStaticScripts As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvStaticScripts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView22 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btnDeleteDetectResultSet As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDeleteCopyResultSet As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblDetectDataRowCount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCopyDataRowCount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmsConfigurationSummary As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmiDeleteSelectedRows As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiCloneSelectedRows As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnDetectRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCopyRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblIntegrityMsg As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gcManual As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvManual As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView23 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tsmiAddNewRow As ToolStripMenuItem
    Friend WithEvents cmsMapNB As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmiMapSelectedNB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tpNBAudit As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SplitContainerControl5 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel39 As TableLayoutPanel
    Friend WithEvents GroupControl5 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel40 As TableLayoutPanel
    Friend WithEvents LabelControl22 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl23 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl24 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl25 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl26 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl27 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblLastRunTimeNBAudit As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblLastEndTimeNBAudit As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblOwnerNBAudit As DevExpress.XtraEditors.LabelControl
    Friend WithEvents chkActiveNBAudit As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents dtpStartTimeNBAudit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents cmbRepeatIntervalNBAudit As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnRunNowNBAudit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gcCampNBAudit As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvCampNBAudit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView24 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel41 As TableLayoutPanel
    Friend WithEvents btnCloneCampNBAudit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnRefreshCampNBAudit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtNBAuditCampSearch As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents BtnDeleteCampNBAudit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnAddCampNBAudit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SplitContainerControl3 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents GroupControl6 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel43 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel44 As TableLayoutPanel
    Friend WithEvents LabelControl31 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbResultSetIdNBAudit As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnDeleteResultSetIdNBAudit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gcResultSummaryNBAudit As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvResultSummaryNBAudit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView26 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TableLayoutPanel45 As TableLayoutPanel
    Friend WithEvents gcResultDataNBAudit As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvResultDataNBAudit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView28 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel46 As TableLayoutPanel
    Friend WithEvents btnLoadToGridNBAudit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDataToCSVNBAudit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblRecordCountNBAudit As DevExpress.XtraEditors.LabelControl
    Friend WithEvents grpConfigSummaryNBAudit As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel48 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel49 As TableLayoutPanel
    Friend WithEvents GroupControl8 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grdPropertyNBAudit As PropertyGrid
    Friend WithEvents btnListManagerNBAudit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gcConfigNBAudit As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvConfigNBAudit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView30 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel42 As TableLayoutPanel
    Friend WithEvents grpConfigGen As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grpOptionalSettings As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel47 As TableLayoutPanel
    Friend WithEvents LabelControl36 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl37 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl38 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbMMLConfigIDNBAudit As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbTechnologyNBAudit As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbInclusionListNBAudit As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnAddConfigNBAudit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel50 As TableLayoutPanel
    Friend WithEvents LabelControl39 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl40 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl41 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbExclusionListNBAudit As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbSLayerNBAudit As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbTLayerNBAudit As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents tsmi_Manual_DeleteRows As ToolStripMenuItem
    Friend WithEvents LabelControl28 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl29 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbNBType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbMMLScriptID As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl30 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ceIsPublicDetect As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ceIsPublicCopy As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl32 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl42 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ceIsPublicManual As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ceIsPublicAudit As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl43 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl44 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ceIsPublicMML As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents tpNBFetch As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SplitContainerControl6 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gcSelectObjects As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel51 As TableLayoutPanel
    Friend WithEvents gcObjectTree As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel52 As TableLayoutPanel
    Friend WithEvents tvObjectsTree As TreeView
    Friend WithEvents TableLayoutPanel53 As TableLayoutPanel
    Friend WithEvents txtSearchObject As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LabelControl45 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel54 As TableLayoutPanel
    Friend WithEvents cmbObjectType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl46 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblObjectTreeCount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel55 As TableLayoutPanel
    Friend WithEvents cmbTechnology As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl47 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl48 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl49 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl7 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gcNBFetch As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvNBFetch As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView25 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel56 As TableLayoutPanel
    Friend WithEvents btnNBFetch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel57 As TableLayoutPanel
    Friend WithEvents txtFileNameSuffix As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl50 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl51 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents seFileSize As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents TableLayoutPanel58 As TableLayoutPanel
    Friend WithEvents btnPreFilter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel59 As TableLayoutPanel
    Friend WithEvents ceApplyConfigAllDetect As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents TableLayoutPanel60 As TableLayoutPanel
    Friend WithEvents ceApplyConfigAllCopy As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents TableLayoutPanel61 As TableLayoutPanel
    Friend WithEvents ceApplyConfigAllAudit As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cmMMLCampaign As ContextMenuStrip
    Friend WithEvents tsmiInsertTempNB As ToolStripMenuItem
    Friend WithEvents tsmiRemoveTempNB As ToolStripMenuItem
    Friend WithEvents TableLayoutPanel62 As TableLayoutPanel
    Friend WithEvents btnNBFetchCells As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tsmiEditTempObjects As ToolStripMenuItem
    Friend WithEvents tsmiRemoveAllTempNB As ToolStripMenuItem
    Friend WithEvents btnRefreshManual As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tpNBDelete As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents sccDeleteCamp As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents SplitContainerControl8 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents grpCampDelete As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel63 As TableLayoutPanel
    Friend WithEvents grpCampPropDelete As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel64 As TableLayoutPanel
    Friend WithEvents LabelControl52 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl53 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl54 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl55 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl56 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl57 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblLastRunTimeDelete As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblLastEndTimeDelete As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblOwnerDelete As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ceActiveDelete As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents deSchNxtStartTimeDelete As DevExpress.XtraEditors.DateEdit
    Friend WithEvents cmbSchRptIntervalDelete As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnRunNowDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ceIsPublicDelete As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl61 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gcDeleteCampaigns As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvDeleteCampaigns As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView27 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel65 As TableLayoutPanel
    Friend WithEvents btnDeleteRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtSearchDelete As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents btnCloneDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDeleteDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel66 As TableLayoutPanel
    Friend WithEvents grpCampSummDelete As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel67 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel68 As TableLayoutPanel
    Friend WithEvents cmbResultSetIDDelete As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl62 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnDeleteResultSetDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents XtraTabControl2 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage3 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gcCampSummDelete As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvCampSummDelete As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView31 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents XtraTabPage4 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TableLayoutPanel69 As TableLayoutPanel
    Friend WithEvents gcCampDataDelete As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvCampDataDelete As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView33 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel70 As TableLayoutPanel
    Friend WithEvents btnDataLoadGridDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDataAllCsvDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblDataRowCountDelete As DevExpress.XtraEditors.LabelControl
    Friend WithEvents grpConfigSummDelete As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel71 As TableLayoutPanel
    Friend WithEvents gcConfigSummDelete As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvConfigSummDelete As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView35 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel72 As TableLayoutPanel
    Friend WithEvents grpLayerPropDelete As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel73 As TableLayoutPanel
    Friend WithEvents ceApplyConfigAllDelete As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents layerPropGridDelete As PropertyGrid
    Friend WithEvents btnListMngrDelete As DevExpress.XtraEditors.SimpleButton
End Class
