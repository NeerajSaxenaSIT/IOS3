<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTiltManagement
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim Annotation1 As dotnetCHARTING.WinForms.Annotation = New dotnetCHARTING.WinForms.Annotation()
        Dim BoxHeaderOptions1 As dotnetCHARTING.WinForms.BoxHeaderOptions = New dotnetCHARTING.WinForms.BoxHeaderOptions()
        Dim Element1 As dotnetCHARTING.WinForms.Element = New dotnetCHARTING.WinForms.Element()
        Dim Line1 As dotnetCHARTING.WinForms.Line = New dotnetCHARTING.WinForms.Line()
        Dim BoxHeaderOptions2 As dotnetCHARTING.WinForms.BoxHeaderOptions = New dotnetCHARTING.WinForms.BoxHeaderOptions()
        Dim BoxHeaderOptions3 As dotnetCHARTING.WinForms.BoxHeaderOptions = New dotnetCHARTING.WinForms.BoxHeaderOptions()
        Dim Element2 As dotnetCHARTING.WinForms.Element = New dotnetCHARTING.WinForms.Element()
        Dim Line2 As dotnetCHARTING.WinForms.Line = New dotnetCHARTING.WinForms.Line()
        Dim View3D1 As dotnetCHARTING.WinForms.View3D = New dotnetCHARTING.WinForms.View3D()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTiltManagement))
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.xtcMain = New DevExpress.XtraTab.XtraTabControl()
        Me.tpTiltMngrAdHoc = New DevExpress.XtraTab.XtraTabPage()
        Me.sccMain = New DevExpress.XtraEditors.SplitContainerControl()
        Me.TableLayoutPanel32 = New System.Windows.Forms.TableLayoutPanel()
        Me.ch_TiltManager = New dotnetCHARTING.WinForms.Chart()
        Me.TableLayoutPanel36 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcSectorList = New DevExpress.XtraGrid.GridControl()
        Me.gvSectorList = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel39 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnGenerateTiltCampaign = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCalculateAndSave = New DevExpress.XtraEditors.SimpleButton()
        Me.tglPlanned = New IOS.Library.IOSToggleButton()
        Me.btnClearThematics = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel40 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbManualCampaign = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnAddCampaign = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDeleteCampaign = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel41 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbResolution = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnManageTree = New DevExpress.XtraEditors.SimpleButton()
        Me.txtETiltValue = New DevExpress.XtraEditors.TextEdit()
        Me.TableLayoutPanel42 = New System.Windows.Forms.TableLayoutPanel()
        Me.tbcETiltSlider = New DevExpress.XtraEditors.TrackBarControl()
        Me.lbl_EtiltPlanned = New DevExpress.XtraEditors.LabelControl()
        Me.sccTiltTreeValidGrid = New DevExpress.XtraEditors.SplitContainerControl()
        Me.tlTiltManager = New DevExpress.XtraTreeList.TreeList()
        Me.Antennas = New DevExpress.XtraTreeList.Columns.TreeListBand()
        Me.TreeListColumn1 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn2 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn3 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.treeListBand1 = New DevExpress.XtraTreeList.Columns.TreeListBand()
        Me.TreeListColumn4 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn5 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn6 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.treeListBand2 = New DevExpress.XtraTreeList.Columns.TreeListBand()
        Me.TreeListColumn7 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn8 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn10 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.treeListBand3 = New DevExpress.XtraTreeList.Columns.TreeListBand()
        Me.tlcValidation = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.RepositoryItemPictureEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.TreeListColumn11 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn12 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn13 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn14 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn15 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn9 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn16 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn17 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn18 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn19 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn20 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn21 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.RepositoryItemImageEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemImageEdit()
        Me.gcCampaignValidation = New DevExpress.XtraGrid.GridControl()
        Me.gvCampaignValidation = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tpTmBulk = New DevExpress.XtraTab.XtraTabPage()
        Me.sccDetectCamp = New DevExpress.XtraEditors.SplitContainerControl()
        Me.sccLeft = New DevExpress.XtraEditors.SplitContainerControl()
        Me.grpCampBulk = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.grpCampPropDetect = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.lblLastRunTimeBulk = New DevExpress.XtraEditors.LabelControl()
        Me.lblLastEndTimeBulk = New DevExpress.XtraEditors.LabelControl()
        Me.lblOwnerBulk = New DevExpress.XtraEditors.LabelControl()
        Me.ceActiveBulk = New DevExpress.XtraEditors.CheckEdit()
        Me.btnRunNowBulk = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl30 = New DevExpress.XtraEditors.LabelControl()
        Me.ceIsPublicBulk = New DevExpress.XtraEditors.CheckEdit()
        Me.gcCampaignsBulk = New DevExpress.XtraGrid.GridControl()
        Me.cmsCampaignsBulk = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_RenameCampaignBulk = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvCampaignsBulk = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnAddBulk = New DevExpress.XtraEditors.SimpleButton()
        Me.txtSearchBulk = New DevExpress.XtraEditors.ButtonEdit()
        Me.btnDeleteBulk = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCloneBulk = New DevExpress.XtraEditors.SimpleButton()
        Me.btnRefreshBulk = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.grpCampResultBulk = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel28 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel33 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl13 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbResultSetIDBulk = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnDeleteResultSetBulk = New DevExpress.XtraEditors.SimpleButton()
        Me.xtcTMBulk = New DevExpress.XtraTab.XtraTabControl()
        Me.tpCampBulkSumm = New DevExpress.XtraTab.XtraTabPage()
        Me.gcSummDataBulk = New DevExpress.XtraGrid.GridControl()
        Me.gvSummDataBulk = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tpCampImportedData = New DevExpress.XtraTab.XtraTabPage()
        Me.gcImportedDataBulk = New DevExpress.XtraGrid.GridControl()
        Me.cmBulkPaste = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_PasteDataFromClipboard = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvImportedDataBulk = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView19 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tpValidationData = New DevExpress.XtraTab.XtraTabPage()
        Me.gcValidationData = New DevExpress.XtraGrid.GridControl()
        Me.gvValidationData = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView20 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tpCampBulkOutputData = New DevExpress.XtraTab.XtraTabPage()
        Me.TableLayoutPanel34 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcOutputDataBulk = New DevExpress.XtraGrid.GridControl()
        Me.cmsLaunchAdHocTiltMngr = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_AdHocTiltManager = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_AddNewTiltCampaign = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvOutputDataBulk = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView21 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel35 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnDetectDataLoadGrid = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDataAllCsvBulk = New DevExpress.XtraEditors.SimpleButton()
        Me.lblDataRowCountBulk = New DevExpress.XtraEditors.LabelControl()
        Me.grpImportBulk = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel29 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.btnImportBulk = New DevExpress.XtraEditors.SimpleButton()
        Me.lblStatus = New DevExpress.XtraEditors.LabelControl()
        Me.btnOpenFile = New DevExpress.XtraEditors.SimpleButton()
        Me.txtImportfileName = New DevExpress.XtraEditors.TextEdit()
        Me.TableLayoutPanel21 = New System.Windows.Forms.TableLayoutPanel()
        Me.grpConfigSummBulk = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel37 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.grpLayerPropBulk = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel59 = New System.Windows.Forms.TableLayoutPanel()
        Me.layerPropGridBulk = New System.Windows.Forms.PropertyGrid()
        Me.TableLayoutPanel31 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnLayerPropertiesAddBulk = New DevExpress.XtraEditors.SimpleButton()
        Me.ceApplyConfigAllBulk = New DevExpress.XtraEditors.CheckEdit()
        Me.btnListMngrBulk = New DevExpress.XtraEditors.SimpleButton()
        Me.gcConfigSummBulk = New DevExpress.XtraGrid.GridControl()
        Me.cmsConfigSummary = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_DeleteSelectedRows = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvConfigSummBulk = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tpTmAudit = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.SplitContainerControl2 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.grpCampAudit = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl17 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl18 = New DevExpress.XtraEditors.LabelControl()
        Me.lblLastRunTimeAudit = New DevExpress.XtraEditors.LabelControl()
        Me.lblLastEndTimeAudit = New DevExpress.XtraEditors.LabelControl()
        Me.lblOwnerAudit = New DevExpress.XtraEditors.LabelControl()
        Me.ceActiveAudit = New DevExpress.XtraEditors.CheckEdit()
        Me.btnRunNowAudit = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl22 = New DevExpress.XtraEditors.LabelControl()
        Me.ceIsPublicAudit = New DevExpress.XtraEditors.CheckEdit()
        Me.gcCampaignsAudit = New DevExpress.XtraGrid.GridControl()
        Me.cmsCampaignsAudit = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_RenameCampaignAudit = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvCampaignsAudit = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView9 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnRefreshAudit = New DevExpress.XtraEditors.SimpleButton()
        Me.txtSearchAudit = New DevExpress.XtraEditors.ButtonEdit()
        Me.btnDeleteAudit = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCloneAudit = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel()
        Me.grpCampResultAudit = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel16 = New System.Windows.Forms.TableLayoutPanel()
        Me.xtcTMAudit = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.gcSummDataAudit = New DevExpress.XtraGrid.GridControl()
        Me.gvSummDataAudit = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView13 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.gcInputDataAudit = New DevExpress.XtraGrid.GridControl()
        Me.gvInputDataAudit = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView15 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.XtraTabPage3 = New DevExpress.XtraTab.XtraTabPage()
        Me.gcValidationDataAudit = New DevExpress.XtraGrid.GridControl()
        Me.gvValidationDataAudit = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView22 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.XtraTabPage4 = New DevExpress.XtraTab.XtraTabPage()
        Me.TableLayoutPanel18 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcOutputDataAudit = New DevExpress.XtraGrid.GridControl()
        Me.gvOutputDataAudit = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView24 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel19 = New System.Windows.Forms.TableLayoutPanel()
        Me.SimpleButton6 = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDataAllCsvAudit = New DevExpress.XtraEditors.SimpleButton()
        Me.lblDataRowCountAudit = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel17 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl23 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbResultSetIDAudit = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnDeleteResultSetAudit = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel20 = New System.Windows.Forms.TableLayoutPanel()
        Me.grpConfigSummAudit = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel22 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel25 = New System.Windows.Forms.TableLayoutPanel()
        Me.grpLayerPropAudit = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel26 = New System.Windows.Forms.TableLayoutPanel()
        Me.layerPropGridAudit = New System.Windows.Forms.PropertyGrid()
        Me.TableLayoutPanel43 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnLayerPropertiesAddAudit = New DevExpress.XtraEditors.SimpleButton()
        Me.ceApplyConfigAllAudit = New DevExpress.XtraEditors.CheckEdit()
        Me.btnListMngrAudit = New DevExpress.XtraEditors.SimpleButton()
        Me.gcConfigSummAudit = New DevExpress.XtraGrid.GridControl()
        Me.gvConfigSummAudit = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView18 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tpTmMML = New DevExpress.XtraTab.XtraTabPage()
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
        Me.TableLayoutPanel57 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbOutputLocation = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtFileNameSuffix = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl50 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl51 = New DevExpress.XtraEditors.LabelControl()
        Me.seFileSize = New DevExpress.XtraEditors.SpinEdit()
        Me.TableLayoutPanel30 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnMMLRollback = New DevExpress.XtraEditors.SimpleButton()
        Me.btnMML = New DevExpress.XtraEditors.SimpleButton()
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
        Me.tpScripts = New DevExpress.XtraTab.XtraTabPage()
        Me.gcScripts = New DevExpress.XtraGrid.GridControl()
        Me.gvScripts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView7 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.lblIntegrityMsg = New DevExpress.XtraEditors.LabelControl()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.cmsSectorList = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_DeleteSector = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTipController1 = New DevExpress.Utils.ToolTipController(Me.components)
        Me.tlpMain.SuspendLayout()
        CType(Me.xtcMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtcMain.SuspendLayout()
        Me.tpTiltMngrAdHoc.SuspendLayout()
        CType(Me.sccMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccMain.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccMain.Panel1.SuspendLayout()
        CType(Me.sccMain.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccMain.Panel2.SuspendLayout()
        Me.sccMain.SuspendLayout()
        Me.TableLayoutPanel32.SuspendLayout()
        CType(Me.ch_TiltManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel36.SuspendLayout()
        CType(Me.gcSectorList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSectorList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel39.SuspendLayout()
        Me.TableLayoutPanel40.SuspendLayout()
        CType(Me.cmbManualCampaign.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel41.SuspendLayout()
        CType(Me.cmbResolution.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtETiltValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel42.SuspendLayout()
        CType(Me.tbcETiltSlider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbcETiltSlider.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccTiltTreeValidGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccTiltTreeValidGrid.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccTiltTreeValidGrid.Panel1.SuspendLayout()
        CType(Me.sccTiltTreeValidGrid.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccTiltTreeValidGrid.Panel2.SuspendLayout()
        Me.sccTiltTreeValidGrid.SuspendLayout()
        CType(Me.tlTiltManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcCampaignValidation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCampaignValidation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpTmBulk.SuspendLayout()
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
        CType(Me.grpCampBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCampBulk.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.grpCampPropDetect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCampPropDetect.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.ceActiveBulk.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceIsPublicBulk.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcCampaignsBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsCampaignsBulk.SuspendLayout()
        CType(Me.gvCampaignsBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.txtSearchBulk.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel7.SuspendLayout()
        CType(Me.grpCampResultBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCampResultBulk.SuspendLayout()
        Me.TableLayoutPanel28.SuspendLayout()
        Me.TableLayoutPanel33.SuspendLayout()
        CType(Me.cmbResultSetIDBulk.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcTMBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtcTMBulk.SuspendLayout()
        Me.tpCampBulkSumm.SuspendLayout()
        CType(Me.gcSummDataBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSummDataBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpCampImportedData.SuspendLayout()
        CType(Me.gcImportedDataBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmBulkPaste.SuspendLayout()
        CType(Me.gvImportedDataBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView19, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpValidationData.SuspendLayout()
        CType(Me.gcValidationData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvValidationData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView20, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpCampBulkOutputData.SuspendLayout()
        Me.TableLayoutPanel34.SuspendLayout()
        CType(Me.gcOutputDataBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsLaunchAdHocTiltMngr.SuspendLayout()
        CType(Me.gvOutputDataBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView21, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel35.SuspendLayout()
        CType(Me.grpImportBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpImportBulk.SuspendLayout()
        Me.TableLayoutPanel29.SuspendLayout()
        CType(Me.txtImportfileName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel21.SuspendLayout()
        CType(Me.grpConfigSummBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpConfigSummBulk.SuspendLayout()
        Me.TableLayoutPanel37.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.grpLayerPropBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpLayerPropBulk.SuspendLayout()
        Me.TableLayoutPanel59.SuspendLayout()
        Me.TableLayoutPanel31.SuspendLayout()
        CType(Me.ceApplyConfigAllBulk.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcConfigSummBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsConfigSummary.SuspendLayout()
        CType(Me.gvConfigSummBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpTmAudit.SuspendLayout()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel2.SuspendLayout()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl2.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl2.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl2.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl2.Panel2.SuspendLayout()
        Me.SplitContainerControl2.SuspendLayout()
        CType(Me.grpCampAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCampAudit.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl5.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        CType(Me.ceActiveAudit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceIsPublicAudit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcCampaignsAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsCampaignsAudit.SuspendLayout()
        CType(Me.gvCampaignsAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel8.SuspendLayout()
        CType(Me.txtSearchAudit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel9.SuspendLayout()
        CType(Me.grpCampResultAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCampResultAudit.SuspendLayout()
        Me.TableLayoutPanel16.SuspendLayout()
        CType(Me.xtcTMAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtcTMAudit.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        CType(Me.gcSummDataAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSummDataAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView13, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage2.SuspendLayout()
        CType(Me.gcInputDataAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvInputDataAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView15, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage3.SuspendLayout()
        CType(Me.gcValidationDataAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvValidationDataAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView22, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage4.SuspendLayout()
        Me.TableLayoutPanel18.SuspendLayout()
        CType(Me.gcOutputDataAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvOutputDataAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView24, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel19.SuspendLayout()
        Me.TableLayoutPanel17.SuspendLayout()
        CType(Me.cmbResultSetIDAudit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel20.SuspendLayout()
        CType(Me.grpConfigSummAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpConfigSummAudit.SuspendLayout()
        Me.TableLayoutPanel22.SuspendLayout()
        Me.TableLayoutPanel25.SuspendLayout()
        CType(Me.grpLayerPropAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpLayerPropAudit.SuspendLayout()
        Me.TableLayoutPanel26.SuspendLayout()
        Me.TableLayoutPanel43.SuspendLayout()
        CType(Me.ceApplyConfigAllAudit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcConfigSummAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvConfigSummAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView18, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpTmMML.SuspendLayout()
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
        Me.TableLayoutPanel30.SuspendLayout()
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
        Me.tpScripts.SuspendLayout()
        CType(Me.gcScripts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvScripts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsSectorList.SuspendLayout()
        Me.SuspendLayout()
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
        Me.tlpMain.Size = New System.Drawing.Size(1272, 831)
        Me.tlpMain.TabIndex = 3
        '
        'xtcMain
        '
        Me.xtcMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtcMain.Location = New System.Drawing.Point(3, 33)
        Me.xtcMain.Name = "xtcMain"
        Me.xtcMain.SelectedTabPage = Me.tpTiltMngrAdHoc
        Me.xtcMain.Size = New System.Drawing.Size(1266, 795)
        Me.xtcMain.TabIndex = 0
        Me.xtcMain.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tpTiltMngrAdHoc, Me.tpTmBulk, Me.tpTmAudit, Me.tpTmMML})
        '
        'tpTiltMngrAdHoc
        '
        Me.tpTiltMngrAdHoc.Controls.Add(Me.sccMain)
        Me.tpTiltMngrAdHoc.Name = "tpTiltMngrAdHoc"
        Me.tpTiltMngrAdHoc.Size = New System.Drawing.Size(1264, 770)
        Me.tpTiltMngrAdHoc.Text = "Ad-Hoc Tilt Manager"
        '
        'sccMain
        '
        Me.sccMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccMain.Horizontal = False
        Me.sccMain.Location = New System.Drawing.Point(0, 0)
        Me.sccMain.Name = "sccMain"
        '
        'sccMain.Panel1
        '
        Me.sccMain.Panel1.Controls.Add(Me.TableLayoutPanel32)
        Me.sccMain.Panel1.MinSize = 200
        Me.sccMain.Panel1.Text = "Panel1"
        '
        'sccMain.Panel2
        '
        Me.sccMain.Panel2.Controls.Add(Me.sccTiltTreeValidGrid)
        Me.sccMain.Panel2.MinSize = 300
        Me.sccMain.Panel2.Text = "Panel2"
        Me.sccMain.Size = New System.Drawing.Size(1264, 770)
        Me.sccMain.SplitterPosition = 460
        Me.sccMain.TabIndex = 1
        '
        'TableLayoutPanel32
        '
        Me.TableLayoutPanel32.ColumnCount = 3
        Me.TableLayoutPanel32.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel32.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel32.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400.0!))
        Me.TableLayoutPanel32.Controls.Add(Me.ch_TiltManager, 0, 0)
        Me.TableLayoutPanel32.Controls.Add(Me.TableLayoutPanel36, 2, 0)
        Me.TableLayoutPanel32.Controls.Add(Me.TableLayoutPanel42, 1, 0)
        Me.TableLayoutPanel32.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel32.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel32.Name = "TableLayoutPanel32"
        Me.TableLayoutPanel32.RowCount = 1
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel32.Size = New System.Drawing.Size(1264, 460)
        Me.TableLayoutPanel32.TabIndex = 0
        '
        'ch_TiltManager
        '
        Me.ch_TiltManager.Background.Color = System.Drawing.Color.White
        Annotation1.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        Annotation1.Background.ShadingEffectMode = dotnetCHARTING.WinForms.ShadingEffectMode.[Default]
        Annotation1.DynamicSize = True
        BoxHeaderOptions1.Background.ShadingEffectMode = dotnetCHARTING.WinForms.ShadingEffectMode.[Default]
        BoxHeaderOptions1.Label.Font = New System.Drawing.Font("Tahoma", 7.5!, System.Drawing.FontStyle.Bold)
        BoxHeaderOptions1.Label.Offset = New System.Drawing.Point(0, 0)
        BoxHeaderOptions1.Label.Width = -2147483648
        BoxHeaderOptions1.Line.Color = System.Drawing.Color.Gray
        BoxHeaderOptions1.Shadow.Color = System.Drawing.Color.Transparent
        Annotation1.Header = BoxHeaderOptions1
        Annotation1.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Annotation1.Label.Offset = New System.Drawing.Point(0, 0)
        Annotation1.Label.Width = -2147483648
        Annotation1.Line.Color = System.Drawing.Color.Gray
        Annotation1.Orientation = dotnetCHARTING.WinForms.Orientation.TopRight
        Annotation1.Padding = 4
        Annotation1.Shadow.Color = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Annotation1.Shadow.Depth = 1
        Annotation1.Shadow.ExpandBy = 2.0!
        Annotation1.Shadow.Visible = False
        Annotation1.Size = New System.Drawing.Size(817, 453)
        Annotation1.Visible = True
        Me.ch_TiltManager.Box = Annotation1
        Me.ch_TiltManager.ChartArea.Background.Color = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.ch_TiltManager.ChartArea.CornerTopLeft = dotnetCHARTING.WinForms.BoxCorner.Square
        Element1.DefaultSubValue.Line.Color = System.Drawing.Color.FromArgb(CType(CType(93, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(59, Byte), Integer))
        Element1.DefaultSubValue.Visible = True
        Element1.FocusGlow = Line1
        Element1.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Element1.LegendEntry.DividerLine.Color = System.Drawing.Color.Empty
        Element1.LegendEntry.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Element1.LegendEntry.LabelStyle.Offset = New System.Drawing.Point(0, 0)
        Element1.LegendEntry.LabelStyle.Width = -2147483648
        Element1.SmartLabel.Color = System.Drawing.Color.Empty
        Element1.SmartLabel.Offset = New System.Drawing.Point(0, 0)
        Element1.SmartLabel.Width = -2147483648
        Me.ch_TiltManager.ChartArea.DefaultElement = Element1
        Me.ch_TiltManager.ChartArea.InteriorLine.Color = System.Drawing.Color.LightGray
        Me.ch_TiltManager.ChartArea.Label.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.ch_TiltManager.ChartArea.Label.Offset = New System.Drawing.Point(0, 0)
        Me.ch_TiltManager.ChartArea.Label.Width = -2147483648
        Me.ch_TiltManager.ChartArea.LegendBox.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.ch_TiltManager.ChartArea.LegendBox.CornerBottomRight = dotnetCHARTING.WinForms.BoxCorner.Cut
        Me.ch_TiltManager.ChartArea.LegendBox.DefaultEntry.DividerLine.Color = System.Drawing.Color.Empty
        Me.ch_TiltManager.ChartArea.LegendBox.DefaultEntry.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.ch_TiltManager.ChartArea.LegendBox.DefaultEntry.LabelStyle.Font = New System.Drawing.Font("Trebuchet MS", 8.0!)
        Me.ch_TiltManager.ChartArea.LegendBox.DefaultEntry.LabelStyle.Offset = New System.Drawing.Point(0, 0)
        Me.ch_TiltManager.ChartArea.LegendBox.DefaultEntry.LabelStyle.Width = -2147483648
        BoxHeaderOptions2.Label.Offset = New System.Drawing.Point(0, 0)
        BoxHeaderOptions2.Label.Width = -2147483648
        BoxHeaderOptions2.Line.Color = System.Drawing.Color.Gray
        BoxHeaderOptions2.Shadow.Color = System.Drawing.Color.Transparent
        Me.ch_TiltManager.ChartArea.LegendBox.Header = BoxHeaderOptions2
        Me.ch_TiltManager.ChartArea.LegendBox.HeaderEntry.DividerLine.Color = System.Drawing.Color.Gray
        Me.ch_TiltManager.ChartArea.LegendBox.HeaderEntry.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.ch_TiltManager.ChartArea.LegendBox.HeaderEntry.LabelStyle.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.ch_TiltManager.ChartArea.LegendBox.HeaderEntry.LabelStyle.Offset = New System.Drawing.Point(0, 0)
        Me.ch_TiltManager.ChartArea.LegendBox.HeaderEntry.LabelStyle.Width = -2147483648
        Me.ch_TiltManager.ChartArea.LegendBox.HeaderEntry.Name = "Name"
        Me.ch_TiltManager.ChartArea.LegendBox.HeaderEntry.SortOrder = -1
        Me.ch_TiltManager.ChartArea.LegendBox.HeaderEntry.Value = "Value"
        Me.ch_TiltManager.ChartArea.LegendBox.HeaderEntry.Visible = False
        Me.ch_TiltManager.ChartArea.LegendBox.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ch_TiltManager.ChartArea.LegendBox.Line.Color = System.Drawing.Color.Gray
        Me.ch_TiltManager.ChartArea.LegendBox.Padding = 4
        Me.ch_TiltManager.ChartArea.LegendBox.Position = dotnetCHARTING.WinForms.LegendBoxPosition.Top
        Me.ch_TiltManager.ChartArea.LegendBox.Shadow.ExpandBy = 2.0!
        Me.ch_TiltManager.ChartArea.LegendBox.Visible = True
        Me.ch_TiltManager.ChartArea.Line.Color = System.Drawing.Color.Gray
        Me.ch_TiltManager.ChartArea.Shadow.Color = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.ch_TiltManager.ChartArea.Shadow.Depth = 1
        Me.ch_TiltManager.ChartArea.Shadow.ExpandBy = 2.0!
        Me.ch_TiltManager.ChartArea.Shadow.Visible = False
        Me.ch_TiltManager.ChartArea.StartDateOfYear = New Date(CType(0, Long))
        Me.ch_TiltManager.ChartArea.TitleBox.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        BoxHeaderOptions3.Label.Offset = New System.Drawing.Point(0, 0)
        BoxHeaderOptions3.Label.Width = -2147483648
        BoxHeaderOptions3.Line.Color = System.Drawing.Color.Gray
        BoxHeaderOptions3.Shadow.Color = System.Drawing.Color.Transparent
        Me.ch_TiltManager.ChartArea.TitleBox.Header = BoxHeaderOptions3
        Me.ch_TiltManager.ChartArea.TitleBox.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ch_TiltManager.ChartArea.TitleBox.Label.Color = System.Drawing.Color.FromArgb(CType(CType(97, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.ch_TiltManager.ChartArea.TitleBox.Label.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.ch_TiltManager.ChartArea.TitleBox.Label.Offset = New System.Drawing.Point(0, 0)
        Me.ch_TiltManager.ChartArea.TitleBox.Label.Width = -2147483648
        Me.ch_TiltManager.ChartArea.TitleBox.Line.Color = System.Drawing.Color.Gray
        Me.ch_TiltManager.ChartArea.TitleBox.Shadow.ExpandBy = 2.0!
        Me.ch_TiltManager.ChartArea.TitleBox.Visible = True
        Me.ch_TiltManager.ChartArea.XAxis.Crosshair = Nothing
        Me.ch_TiltManager.ChartArea.XAxis.DefaultTick.AxisID = ""
        Me.ch_TiltManager.ChartArea.XAxis.DefaultTick.GridLine.Color = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.ch_TiltManager.ChartArea.XAxis.DefaultTick.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.ch_TiltManager.ChartArea.XAxis.DefaultTick.Label.Offset = New System.Drawing.Point(0, 0)
        Me.ch_TiltManager.ChartArea.XAxis.DefaultTick.Label.Width = -2147483648
        Me.ch_TiltManager.ChartArea.XAxis.DefaultTick.Line.Length = 3
        Me.ch_TiltManager.ChartArea.XAxis.Label.Offset = New System.Drawing.Point(0, 0)
        Me.ch_TiltManager.ChartArea.XAxis.Label.Width = -2147483648
        Me.ch_TiltManager.ChartArea.XAxis.MinorTimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.ch_TiltManager.ChartArea.XAxis.MinorTimeIntervalAdvanced.Unit = dotnetCHARTING.WinForms.TimeInterval.None
        Me.ch_TiltManager.ChartArea.XAxis.TimeInterval = dotnetCHARTING.WinForms.TimeInterval.Hours
        Me.ch_TiltManager.ChartArea.XAxis.TimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.ch_TiltManager.ChartArea.XAxis.TimeScaleLabels.MaximumRangeRows = 4
        Me.ch_TiltManager.ChartArea.XAxis.ZeroTick.AxisID = ""
        Me.ch_TiltManager.ChartArea.XAxis.ZeroTick.GridLine.Color = System.Drawing.Color.Red
        Me.ch_TiltManager.ChartArea.XAxis.ZeroTick.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.ch_TiltManager.ChartArea.XAxis.ZeroTick.Label.Offset = New System.Drawing.Point(0, 0)
        Me.ch_TiltManager.ChartArea.XAxis.ZeroTick.Label.Width = -2147483648
        Me.ch_TiltManager.ChartArea.XAxis.ZeroTick.Line.Length = 3
        Me.ch_TiltManager.ChartArea.YAxis.Crosshair = Nothing
        Me.ch_TiltManager.ChartArea.YAxis.DefaultTick.AxisID = ""
        Me.ch_TiltManager.ChartArea.YAxis.DefaultTick.GridLine.Color = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.ch_TiltManager.ChartArea.YAxis.DefaultTick.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.ch_TiltManager.ChartArea.YAxis.DefaultTick.Label.Offset = New System.Drawing.Point(0, 0)
        Me.ch_TiltManager.ChartArea.YAxis.DefaultTick.Label.Width = -2147483648
        Me.ch_TiltManager.ChartArea.YAxis.DefaultTick.Line.Length = 3
        Me.ch_TiltManager.ChartArea.YAxis.Label.Offset = New System.Drawing.Point(0, 0)
        Me.ch_TiltManager.ChartArea.YAxis.Label.Width = -2147483648
        Me.ch_TiltManager.ChartArea.YAxis.TimeInterval = dotnetCHARTING.WinForms.TimeInterval.Hours
        Me.ch_TiltManager.ChartArea.YAxis.TimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.ch_TiltManager.ChartArea.YAxis.TimeScaleLabels.MaximumRangeRows = 4
        Me.ch_TiltManager.ChartArea.YAxis.ZeroTick.AxisID = ""
        Me.ch_TiltManager.ChartArea.YAxis.ZeroTick.GridLine.Color = System.Drawing.Color.Red
        Me.ch_TiltManager.ChartArea.YAxis.ZeroTick.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.ch_TiltManager.ChartArea.YAxis.ZeroTick.Label.Offset = New System.Drawing.Point(0, 0)
        Me.ch_TiltManager.ChartArea.YAxis.ZeroTick.Label.Width = -2147483648
        Me.ch_TiltManager.ChartArea.YAxis.ZeroTick.Line.Length = 3
        Me.ch_TiltManager.DataGrid = Nothing
        Element2.DefaultSubValue.Visible = True
        Element2.FocusGlow = Line2
        Element2.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Element2.LegendEntry.DividerLine.Color = System.Drawing.Color.Empty
        Element2.LegendEntry.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Element2.LegendEntry.LabelStyle.Offset = New System.Drawing.Point(0, 0)
        Element2.LegendEntry.LabelStyle.Width = -2147483648
        Element2.SmartLabel.Color = System.Drawing.Color.Empty
        Element2.SmartLabel.Offset = New System.Drawing.Point(0, 0)
        Element2.SmartLabel.Width = -2147483648
        Me.ch_TiltManager.DefaultElement = Element2
        Me.ch_TiltManager.DefaultShadow.ExpandBy = 2.0!
        Me.ch_TiltManager.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ch_TiltManager.LegacyMode = False
        Me.ch_TiltManager.Location = New System.Drawing.Point(3, 3)
        Me.ch_TiltManager.Name = "ch_TiltManager"
        Me.ch_TiltManager.NoDataLabel.Offset = New System.Drawing.Point(0, 0)
        Me.ch_TiltManager.NoDataLabel.Text = "No Data"
        Me.ch_TiltManager.NoDataLabel.Width = -2147483648
        Me.ch_TiltManager.Size = New System.Drawing.Size(818, 454)
        Me.ch_TiltManager.StartDateOfYear = New Date(CType(0, Long))
        Me.ch_TiltManager.TabIndex = 6
        Me.ch_TiltManager.TempDirectory = "C:\Users\Guy\AppData\Local\Temp\"
        Me.ch_TiltManager.View3D = View3D1
        '
        'TableLayoutPanel36
        '
        Me.TableLayoutPanel36.ColumnCount = 1
        Me.TableLayoutPanel36.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel36.Controls.Add(Me.gcSectorList, 0, 1)
        Me.TableLayoutPanel36.Controls.Add(Me.TableLayoutPanel39, 0, 2)
        Me.TableLayoutPanel36.Controls.Add(Me.TableLayoutPanel40, 0, 0)
        Me.TableLayoutPanel36.Controls.Add(Me.TableLayoutPanel41, 0, 3)
        Me.TableLayoutPanel36.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel36.Location = New System.Drawing.Point(867, 3)
        Me.TableLayoutPanel36.Name = "TableLayoutPanel36"
        Me.TableLayoutPanel36.RowCount = 4
        Me.TableLayoutPanel36.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel36.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel36.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel36.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel36.Size = New System.Drawing.Size(394, 454)
        Me.TableLayoutPanel36.TabIndex = 0
        '
        'gcSectorList
        '
        Me.gcSectorList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcSectorList.Location = New System.Drawing.Point(3, 31)
        Me.gcSectorList.MainView = Me.gvSectorList
        Me.gcSectorList.Name = "gcSectorList"
        Me.gcSectorList.Size = New System.Drawing.Size(388, 353)
        Me.gcSectorList.TabIndex = 10
        Me.gcSectorList.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvSectorList})
        '
        'gvSectorList
        '
        Me.gvSectorList.GridControl = Me.gcSectorList
        Me.gvSectorList.Name = "gvSectorList"
        Me.gvSectorList.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvSectorList.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvSectorList.OptionsBehavior.Editable = False
        Me.gvSectorList.OptionsBehavior.ReadOnly = True
        Me.gvSectorList.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvSectorList.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvSectorList.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvSectorList.OptionsView.ColumnAutoWidth = False
        Me.gvSectorList.OptionsView.ShowGroupPanel = False
        '
        'TableLayoutPanel39
        '
        Me.TableLayoutPanel39.ColumnCount = 4
        Me.TableLayoutPanel39.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel39.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel39.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel39.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.TableLayoutPanel39.Controls.Add(Me.btnGenerateTiltCampaign, 2, 0)
        Me.TableLayoutPanel39.Controls.Add(Me.btnCalculateAndSave, 1, 0)
        Me.TableLayoutPanel39.Controls.Add(Me.tglPlanned, 0, 0)
        Me.TableLayoutPanel39.Controls.Add(Me.btnClearThematics, 3, 0)
        Me.TableLayoutPanel39.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel39.Location = New System.Drawing.Point(1, 388)
        Me.TableLayoutPanel39.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel39.Name = "TableLayoutPanel39"
        Me.TableLayoutPanel39.RowCount = 1
        Me.TableLayoutPanel39.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel39.Size = New System.Drawing.Size(392, 33)
        Me.TableLayoutPanel39.TabIndex = 11
        '
        'btnGenerateTiltCampaign
        '
        Me.btnGenerateTiltCampaign.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnGenerateTiltCampaign.Location = New System.Drawing.Point(215, 2)
        Me.btnGenerateTiltCampaign.Margin = New System.Windows.Forms.Padding(2)
        Me.btnGenerateTiltCampaign.Name = "btnGenerateTiltCampaign"
        Me.btnGenerateTiltCampaign.Size = New System.Drawing.Size(139, 29)
        Me.btnGenerateTiltCampaign.TabIndex = 2
        Me.btnGenerateTiltCampaign.Text = "Get MML/XML Campaign"
        '
        'btnCalculateAndSave
        '
        Me.btnCalculateAndSave.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCalculateAndSave.Location = New System.Drawing.Point(72, 2)
        Me.btnCalculateAndSave.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCalculateAndSave.Name = "btnCalculateAndSave"
        Me.btnCalculateAndSave.Size = New System.Drawing.Size(139, 29)
        Me.btnCalculateAndSave.TabIndex = 3
        Me.btnCalculateAndSave.Text = "Calculate And Save"
        '
        'tglPlanned
        '
        Me.tglPlanned.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tglPlanned.Location = New System.Drawing.Point(2, 2)
        Me.tglPlanned.LookAndFeel.SkinName = "McSkin"
        Me.tglPlanned.LookAndFeel.UseDefaultLookAndFeel = False
        Me.tglPlanned.Margin = New System.Windows.Forms.Padding(2)
        Me.tglPlanned.Name = "tglPlanned"
        Me.tglPlanned.Size = New System.Drawing.Size(66, 29)
        Me.tglPlanned.TabIndex = 4
        Me.tglPlanned.Text = "Current"
        Me.tglPlanned.ToggleState = System.Windows.Forms.CheckState.Unchecked
        '
        'btnClearThematics
        '
        Me.btnClearThematics.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnClearThematics.ImageOptions.Image = Global.IOS.My.Resources.Resources.Clear_all_thematic
        Me.btnClearThematics.Location = New System.Drawing.Point(358, 2)
        Me.btnClearThematics.Margin = New System.Windows.Forms.Padding(2)
        Me.btnClearThematics.Name = "btnClearThematics"
        Me.btnClearThematics.Size = New System.Drawing.Size(32, 29)
        Me.btnClearThematics.TabIndex = 5
        Me.btnClearThematics.ToolTip = "Clear thematics on the map window"
        '
        'TableLayoutPanel40
        '
        Me.TableLayoutPanel40.ColumnCount = 3
        Me.TableLayoutPanel40.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel40.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel40.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel40.Controls.Add(Me.cmbManualCampaign, 0, 0)
        Me.TableLayoutPanel40.Controls.Add(Me.btnAddCampaign, 1, 0)
        Me.TableLayoutPanel40.Controls.Add(Me.btnDeleteCampaign, 2, 0)
        Me.TableLayoutPanel40.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel40.Location = New System.Drawing.Point(1, 1)
        Me.TableLayoutPanel40.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel40.Name = "TableLayoutPanel40"
        Me.TableLayoutPanel40.RowCount = 1
        Me.TableLayoutPanel40.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel40.Size = New System.Drawing.Size(392, 26)
        Me.TableLayoutPanel40.TabIndex = 12
        '
        'cmbManualCampaign
        '
        Me.cmbManualCampaign.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbManualCampaign.EditValue = "Select Campaign"
        Me.cmbManualCampaign.Location = New System.Drawing.Point(3, 3)
        Me.cmbManualCampaign.Name = "cmbManualCampaign"
        Me.cmbManualCampaign.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbManualCampaign.Properties.Sorted = True
        Me.cmbManualCampaign.Size = New System.Drawing.Size(266, 20)
        Me.cmbManualCampaign.TabIndex = 10
        Me.cmbManualCampaign.ToolTip = "Select Campaign"
        '
        'btnAddCampaign
        '
        Me.btnAddCampaign.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAddCampaign.Location = New System.Drawing.Point(274, 2)
        Me.btnAddCampaign.Margin = New System.Windows.Forms.Padding(2)
        Me.btnAddCampaign.Name = "btnAddCampaign"
        Me.btnAddCampaign.Size = New System.Drawing.Size(56, 22)
        Me.btnAddCampaign.TabIndex = 11
        Me.btnAddCampaign.Text = "Add"
        '
        'btnDeleteCampaign
        '
        Me.btnDeleteCampaign.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDeleteCampaign.Location = New System.Drawing.Point(334, 2)
        Me.btnDeleteCampaign.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDeleteCampaign.Name = "btnDeleteCampaign"
        Me.btnDeleteCampaign.Size = New System.Drawing.Size(56, 22)
        Me.btnDeleteCampaign.TabIndex = 12
        Me.btnDeleteCampaign.Text = "Delete"
        '
        'TableLayoutPanel41
        '
        Me.TableLayoutPanel41.ColumnCount = 4
        Me.TableLayoutPanel41.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel41.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel41.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel41.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel41.Controls.Add(Me.LabelControl4, 1, 0)
        Me.TableLayoutPanel41.Controls.Add(Me.cmbResolution, 2, 0)
        Me.TableLayoutPanel41.Controls.Add(Me.btnManageTree, 3, 0)
        Me.TableLayoutPanel41.Controls.Add(Me.txtETiltValue, 0, 0)
        Me.TableLayoutPanel41.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel41.Location = New System.Drawing.Point(1, 423)
        Me.TableLayoutPanel41.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel41.Name = "TableLayoutPanel41"
        Me.TableLayoutPanel41.RowCount = 1
        Me.TableLayoutPanel41.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel41.Size = New System.Drawing.Size(392, 30)
        Me.TableLayoutPanel41.TabIndex = 13
        '
        'LabelControl4
        '
        Me.LabelControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl4.Location = New System.Drawing.Point(53, 3)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl4.Size = New System.Drawing.Size(64, 24)
        Me.LabelControl4.TabIndex = 0
        Me.LabelControl4.Text = "Resolution"
        '
        'cmbResolution
        '
        Me.cmbResolution.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbResolution.EditValue = "Low"
        Me.cmbResolution.Location = New System.Drawing.Point(123, 5)
        Me.cmbResolution.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.cmbResolution.Name = "cmbResolution"
        Me.cmbResolution.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbResolution.Properties.Items.AddRange(New Object() {"Low", "Medium", "High"})
        Me.cmbResolution.Size = New System.Drawing.Size(166, 20)
        Me.cmbResolution.TabIndex = 1
        '
        'btnManageTree
        '
        Me.btnManageTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnManageTree.Location = New System.Drawing.Point(294, 2)
        Me.btnManageTree.Margin = New System.Windows.Forms.Padding(2)
        Me.btnManageTree.Name = "btnManageTree"
        Me.btnManageTree.Size = New System.Drawing.Size(96, 26)
        Me.btnManageTree.TabIndex = 4
        Me.btnManageTree.Text = "Expand Tree"
        '
        'txtETiltValue
        '
        Me.txtETiltValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtETiltValue.EditValue = ""
        Me.txtETiltValue.Location = New System.Drawing.Point(2, 5)
        Me.txtETiltValue.Margin = New System.Windows.Forms.Padding(2, 5, 2, 2)
        Me.txtETiltValue.Name = "txtETiltValue"
        Me.txtETiltValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtETiltValue.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtETiltValue.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False")
        Me.txtETiltValue.Properties.MaskSettings.Set("autoHideDecimalSeparator", True)
        Me.txtETiltValue.Properties.MaskSettings.Set("hideInsignificantZeros", True)
        Me.txtETiltValue.Properties.MaskSettings.Set("mask", "##.#")
        Me.txtETiltValue.Properties.MaxLength = 4
        Me.txtETiltValue.Size = New System.Drawing.Size(46, 20)
        Me.txtETiltValue.TabIndex = 5
        '
        'TableLayoutPanel42
        '
        Me.TableLayoutPanel42.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel42.ColumnCount = 1
        Me.TableLayoutPanel42.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel42.Controls.Add(Me.tbcETiltSlider, 0, 0)
        Me.TableLayoutPanel42.Controls.Add(Me.lbl_EtiltPlanned, 0, 1)
        Me.TableLayoutPanel42.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel42.Location = New System.Drawing.Point(824, 0)
        Me.TableLayoutPanel42.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel42.Name = "TableLayoutPanel42"
        Me.TableLayoutPanel42.RowCount = 2
        Me.TableLayoutPanel42.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel42.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.TableLayoutPanel42.Size = New System.Drawing.Size(40, 460)
        Me.TableLayoutPanel42.TabIndex = 7
        '
        'tbcETiltSlider
        '
        Me.tbcETiltSlider.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbcETiltSlider.EditValue = Nothing
        Me.tbcETiltSlider.Location = New System.Drawing.Point(4, 4)
        Me.tbcETiltSlider.Name = "tbcETiltSlider"
        Me.tbcETiltSlider.Properties.LabelAppearance.Options.UseTextOptions = True
        Me.tbcETiltSlider.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.tbcETiltSlider.Properties.LargeChange = 1
        Me.tbcETiltSlider.Properties.Maximum = 150
        Me.tbcETiltSlider.Properties.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.tbcETiltSlider.Size = New System.Drawing.Size(32, 418)
        Me.tbcETiltSlider.TabIndex = 7
        '
        'lbl_EtiltPlanned
        '
        Me.lbl_EtiltPlanned.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lbl_EtiltPlanned.Appearance.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_EtiltPlanned.Appearance.Options.UseFont = True
        Me.lbl_EtiltPlanned.Appearance.Options.UseForeColor = True
        Me.lbl_EtiltPlanned.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_EtiltPlanned.Location = New System.Drawing.Point(3, 428)
        Me.lbl_EtiltPlanned.Margin = New System.Windows.Forms.Padding(2)
        Me.lbl_EtiltPlanned.Name = "lbl_EtiltPlanned"
        Me.lbl_EtiltPlanned.Padding = New System.Windows.Forms.Padding(7, 0, 0, 0)
        Me.lbl_EtiltPlanned.Size = New System.Drawing.Size(34, 29)
        Me.lbl_EtiltPlanned.TabIndex = 8
        '
        'sccTiltTreeValidGrid
        '
        Me.sccTiltTreeValidGrid.Collapsed = True
        Me.sccTiltTreeValidGrid.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2
        Me.sccTiltTreeValidGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccTiltTreeValidGrid.Horizontal = False
        Me.sccTiltTreeValidGrid.Location = New System.Drawing.Point(0, 0)
        Me.sccTiltTreeValidGrid.Name = "sccTiltTreeValidGrid"
        '
        'sccTiltTreeValidGrid.Panel1
        '
        Me.sccTiltTreeValidGrid.Panel1.Controls.Add(Me.tlTiltManager)
        Me.sccTiltTreeValidGrid.Panel1.MinSize = 250
        Me.sccTiltTreeValidGrid.Panel1.Text = "Panel1"
        '
        'sccTiltTreeValidGrid.Panel2
        '
        Me.sccTiltTreeValidGrid.Panel2.Controls.Add(Me.gcCampaignValidation)
        Me.sccTiltTreeValidGrid.Panel2.Text = "Panel2"
        Me.sccTiltTreeValidGrid.Size = New System.Drawing.Size(1264, 300)
        Me.sccTiltTreeValidGrid.SplitterPosition = 165
        Me.sccTiltTreeValidGrid.TabIndex = 2
        '
        'tlTiltManager
        '
        Me.tlTiltManager.Bands.AddRange(New DevExpress.XtraTreeList.Columns.TreeListBand() {Me.Antennas, Me.treeListBand1, Me.treeListBand2, Me.treeListBand3})
        Me.tlTiltManager.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.TreeListColumn1, Me.TreeListColumn2, Me.TreeListColumn3, Me.TreeListColumn4, Me.TreeListColumn5, Me.TreeListColumn6, Me.TreeListColumn7, Me.TreeListColumn8, Me.TreeListColumn10, Me.tlcValidation, Me.TreeListColumn11, Me.TreeListColumn12, Me.TreeListColumn13, Me.TreeListColumn14, Me.TreeListColumn15, Me.TreeListColumn9, Me.TreeListColumn16, Me.TreeListColumn17, Me.TreeListColumn18, Me.TreeListColumn19, Me.TreeListColumn20, Me.TreeListColumn21})
        Me.tlTiltManager.CustomizationFormBounds = New System.Drawing.Rectangle(1666, 617, 254, 222)
        Me.tlTiltManager.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlTiltManager.Location = New System.Drawing.Point(0, 0)
        Me.tlTiltManager.Name = "tlTiltManager"
        Me.tlTiltManager.OptionsCustomization.AllowSort = False
        Me.tlTiltManager.OptionsMenu.EnableNodeMenu = False
        Me.tlTiltManager.OptionsView.ShowHorzLines = False
        Me.tlTiltManager.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemImageEdit1, Me.RepositoryItemPictureEdit1})
        Me.tlTiltManager.Size = New System.Drawing.Size(1264, 290)
        Me.tlTiltManager.TabIndex = 1
        '
        'Antennas
        '
        Me.Antennas.Caption = "ANTENNAS"
        Me.Antennas.Columns.Add(Me.TreeListColumn1)
        Me.Antennas.Columns.Add(Me.TreeListColumn2)
        Me.Antennas.Columns.Add(Me.TreeListColumn3)
        Me.Antennas.Name = "Antennas"
        Me.Antennas.OptionsBand.AllowMove = False
        Me.Antennas.Width = 190
        '
        'TreeListColumn1
        '
        Me.TreeListColumn1.Caption = "Antenna Type"
        Me.TreeListColumn1.FieldName = "AntennaType"
        Me.TreeListColumn1.MinWidth = 100
        Me.TreeListColumn1.Name = "TreeListColumn1"
        Me.TreeListColumn1.OptionsColumn.AllowFocus = False
        Me.TreeListColumn1.OptionsColumn.AllowMove = False
        Me.TreeListColumn1.Visible = True
        Me.TreeListColumn1.VisibleIndex = 0
        Me.TreeListColumn1.Width = 100
        '
        'TreeListColumn2
        '
        Me.TreeListColumn2.Caption = "Azimuth"
        Me.TreeListColumn2.FieldName = "Azimuth"
        Me.TreeListColumn2.MinWidth = 50
        Me.TreeListColumn2.Name = "TreeListColumn2"
        Me.TreeListColumn2.OptionsColumn.AllowFocus = False
        Me.TreeListColumn2.OptionsColumn.AllowMove = False
        Me.TreeListColumn2.Visible = True
        Me.TreeListColumn2.VisibleIndex = 1
        Me.TreeListColumn2.Width = 50
        '
        'TreeListColumn3
        '
        Me.TreeListColumn3.Caption = "M-Tilt"
        Me.TreeListColumn3.FieldName = "MTilt"
        Me.TreeListColumn3.MinWidth = 40
        Me.TreeListColumn3.Name = "TreeListColumn3"
        Me.TreeListColumn3.OptionsColumn.AllowFocus = False
        Me.TreeListColumn3.OptionsColumn.AllowMove = False
        Me.TreeListColumn3.Visible = True
        Me.TreeListColumn3.VisibleIndex = 2
        Me.TreeListColumn3.Width = 40
        '
        'treeListBand1
        '
        Me.treeListBand1.Caption = "RET DEVICES"
        Me.treeListBand1.Columns.Add(Me.TreeListColumn4)
        Me.treeListBand1.Columns.Add(Me.TreeListColumn5)
        Me.treeListBand1.Columns.Add(Me.TreeListColumn6)
        Me.treeListBand1.Name = "treeListBand1"
        Me.treeListBand1.OptionsBand.AllowMove = False
        Me.treeListBand1.Width = 186
        '
        'TreeListColumn4
        '
        Me.TreeListColumn4.Caption = "Device Name"
        Me.TreeListColumn4.FieldName = "DeviceName"
        Me.TreeListColumn4.MinWidth = 214
        Me.TreeListColumn4.Name = "TreeListColumn4"
        Me.TreeListColumn4.OptionsColumn.AllowFocus = False
        Me.TreeListColumn4.OptionsColumn.AllowMove = False
        Me.TreeListColumn4.Visible = True
        Me.TreeListColumn4.VisibleIndex = 3
        Me.TreeListColumn4.Width = 214
        '
        'TreeListColumn5
        '
        Me.TreeListColumn5.Caption = "E-Tilt"
        Me.TreeListColumn5.FieldName = "ETilt"
        Me.TreeListColumn5.MinWidth = 40
        Me.TreeListColumn5.Name = "TreeListColumn5"
        Me.TreeListColumn5.OptionsColumn.AllowFocus = False
        Me.TreeListColumn5.OptionsColumn.AllowMove = False
        Me.TreeListColumn5.Visible = True
        Me.TreeListColumn5.VisibleIndex = 4
        Me.TreeListColumn5.Width = 40
        '
        'TreeListColumn6
        '
        Me.TreeListColumn6.Caption = "Device No"
        Me.TreeListColumn6.FieldName = "DeviceNo"
        Me.TreeListColumn6.MinWidth = 60
        Me.TreeListColumn6.Name = "TreeListColumn6"
        Me.TreeListColumn6.OptionsColumn.AllowFocus = False
        Me.TreeListColumn6.OptionsColumn.AllowMove = False
        Me.TreeListColumn6.Visible = True
        Me.TreeListColumn6.VisibleIndex = 5
        Me.TreeListColumn6.Width = 60
        '
        'treeListBand2
        '
        Me.treeListBand2.Caption = "PLAN"
        Me.treeListBand2.Columns.Add(Me.TreeListColumn7)
        Me.treeListBand2.Columns.Add(Me.TreeListColumn8)
        Me.treeListBand2.Columns.Add(Me.TreeListColumn10)
        Me.treeListBand2.Name = "treeListBand2"
        Me.treeListBand2.OptionsBand.AllowMove = False
        Me.treeListBand2.Width = 270
        '
        'TreeListColumn7
        '
        Me.TreeListColumn7.Caption = "Include In Plan"
        Me.TreeListColumn7.FieldName = "IncludeInPlan"
        Me.TreeListColumn7.MinWidth = 129
        Me.TreeListColumn7.Name = "TreeListColumn7"
        Me.TreeListColumn7.OptionsColumn.AllowMove = False
        Me.TreeListColumn7.Visible = True
        Me.TreeListColumn7.VisibleIndex = 6
        Me.TreeListColumn7.Width = 129
        '
        'TreeListColumn8
        '
        Me.TreeListColumn8.Caption = "E-Tilt Planned"
        Me.TreeListColumn8.FieldName = "ETiltPlanned"
        Me.TreeListColumn8.MinWidth = 90
        Me.TreeListColumn8.Name = "TreeListColumn8"
        Me.TreeListColumn8.OptionsColumn.AllowMove = False
        Me.TreeListColumn8.Visible = True
        Me.TreeListColumn8.VisibleIndex = 7
        Me.TreeListColumn8.Width = 90
        '
        'TreeListColumn10
        '
        Me.TreeListColumn10.Caption = "Rule"
        Me.TreeListColumn10.FieldName = "Rule"
        Me.TreeListColumn10.MinWidth = 100
        Me.TreeListColumn10.Name = "TreeListColumn10"
        Me.TreeListColumn10.OptionsColumn.AllowMove = False
        Me.TreeListColumn10.Visible = True
        Me.TreeListColumn10.VisibleIndex = 8
        Me.TreeListColumn10.Width = 100
        '
        'treeListBand3
        '
        Me.treeListBand3.Caption = "CELLS"
        Me.treeListBand3.Columns.Add(Me.tlcValidation)
        Me.treeListBand3.Columns.Add(Me.TreeListColumn11)
        Me.treeListBand3.Columns.Add(Me.TreeListColumn12)
        Me.treeListBand3.Columns.Add(Me.TreeListColumn13)
        Me.treeListBand3.Columns.Add(Me.TreeListColumn14)
        Me.treeListBand3.Columns.Add(Me.TreeListColumn15)
        Me.treeListBand3.Columns.Add(Me.TreeListColumn9)
        Me.treeListBand3.Columns.Add(Me.TreeListColumn16)
        Me.treeListBand3.Columns.Add(Me.TreeListColumn17)
        Me.treeListBand3.Name = "treeListBand3"
        Me.treeListBand3.OptionsBand.AllowMove = False
        Me.treeListBand3.Width = 413
        '
        'tlcValidation
        '
        Me.tlcValidation.ColumnEdit = Me.RepositoryItemPictureEdit1
        Me.tlcValidation.FieldName = "Validation"
        Me.tlcValidation.ImageOptions.Alignment = System.Drawing.StringAlignment.Center
        Me.tlcValidation.ImageOptions.Image = Global.IOS.My.Resources.Resources.cellinfo16_2
        Me.tlcValidation.MinWidth = 25
        Me.tlcValidation.Name = "tlcValidation"
        Me.tlcValidation.Visible = True
        Me.tlcValidation.VisibleIndex = 9
        Me.tlcValidation.Width = 25
        '
        'RepositoryItemPictureEdit1
        '
        Me.RepositoryItemPictureEdit1.Name = "RepositoryItemPictureEdit1"
        Me.RepositoryItemPictureEdit1.NullText = " "
        '
        'TreeListColumn11
        '
        Me.TreeListColumn11.Caption = "Technology"
        Me.TreeListColumn11.FieldName = "Technology"
        Me.TreeListColumn11.MinWidth = 70
        Me.TreeListColumn11.Name = "TreeListColumn11"
        Me.TreeListColumn11.OptionsColumn.AllowFocus = False
        Me.TreeListColumn11.OptionsColumn.AllowMove = False
        Me.TreeListColumn11.Visible = True
        Me.TreeListColumn11.VisibleIndex = 10
        Me.TreeListColumn11.Width = 70
        '
        'TreeListColumn12
        '
        Me.TreeListColumn12.Caption = "Location ID"
        Me.TreeListColumn12.FieldName = "LocationID"
        Me.TreeListColumn12.MinWidth = 70
        Me.TreeListColumn12.Name = "TreeListColumn12"
        Me.TreeListColumn12.OptionsColumn.AllowFocus = False
        Me.TreeListColumn12.OptionsColumn.AllowMove = False
        Me.TreeListColumn12.Visible = True
        Me.TreeListColumn12.VisibleIndex = 11
        Me.TreeListColumn12.Width = 70
        '
        'TreeListColumn13
        '
        Me.TreeListColumn13.Caption = "MBTS Name"
        Me.TreeListColumn13.FieldName = "MBTS_Name"
        Me.TreeListColumn13.MinWidth = 80
        Me.TreeListColumn13.Name = "TreeListColumn13"
        Me.TreeListColumn13.OptionsColumn.AllowFocus = False
        Me.TreeListColumn13.OptionsColumn.AllowMove = False
        Me.TreeListColumn13.Visible = True
        Me.TreeListColumn13.VisibleIndex = 12
        Me.TreeListColumn13.Width = 80
        '
        'TreeListColumn14
        '
        Me.TreeListColumn14.Caption = "Sector ID"
        Me.TreeListColumn14.FieldName = "SectorID"
        Me.TreeListColumn14.MinWidth = 70
        Me.TreeListColumn14.Name = "TreeListColumn14"
        Me.TreeListColumn14.OptionsColumn.AllowFocus = False
        Me.TreeListColumn14.OptionsColumn.AllowMove = False
        Me.TreeListColumn14.Visible = True
        Me.TreeListColumn14.VisibleIndex = 13
        Me.TreeListColumn14.Width = 70
        '
        'TreeListColumn15
        '
        Me.TreeListColumn15.Caption = "Layer"
        Me.TreeListColumn15.FieldName = "Layer"
        Me.TreeListColumn15.MinWidth = 60
        Me.TreeListColumn15.Name = "TreeListColumn15"
        Me.TreeListColumn15.OptionsColumn.AllowFocus = False
        Me.TreeListColumn15.Visible = True
        Me.TreeListColumn15.VisibleIndex = 14
        Me.TreeListColumn15.Width = 60
        '
        'TreeListColumn9
        '
        Me.TreeListColumn9.Caption = "VBeam Angle"
        Me.TreeListColumn9.FieldName = "Vangle"
        Me.TreeListColumn9.MinWidth = 100
        Me.TreeListColumn9.Name = "TreeListColumn9"
        Me.TreeListColumn9.OptionsColumn.AllowFocus = False
        Me.TreeListColumn9.OptionsColumn.AllowMove = False
        Me.TreeListColumn9.Visible = True
        Me.TreeListColumn9.VisibleIndex = 15
        Me.TreeListColumn9.Width = 100
        '
        'TreeListColumn16
        '
        Me.TreeListColumn16.Caption = "Cell Name"
        Me.TreeListColumn16.FieldName = "CellName"
        Me.TreeListColumn16.MinWidth = 171
        Me.TreeListColumn16.Name = "TreeListColumn16"
        Me.TreeListColumn16.OptionsColumn.AllowFocus = False
        Me.TreeListColumn16.OptionsColumn.AllowMove = False
        Me.TreeListColumn16.Visible = True
        Me.TreeListColumn16.VisibleIndex = 16
        Me.TreeListColumn16.Width = 171
        '
        'TreeListColumn17
        '
        Me.TreeListColumn17.Caption = "Cell ID"
        Me.TreeListColumn17.FieldName = "CellID"
        Me.TreeListColumn17.MinWidth = 50
        Me.TreeListColumn17.Name = "TreeListColumn17"
        Me.TreeListColumn17.OptionsColumn.AllowFocus = False
        Me.TreeListColumn17.OptionsColumn.AllowMove = False
        Me.TreeListColumn17.Visible = True
        Me.TreeListColumn17.VisibleIndex = 17
        Me.TreeListColumn17.Width = 50
        '
        'TreeListColumn18
        '
        Me.TreeListColumn18.Caption = "X"
        Me.TreeListColumn18.FieldName = "X"
        Me.TreeListColumn18.Name = "TreeListColumn18"
        Me.TreeListColumn18.OptionsColumn.AllowFocus = False
        '
        'TreeListColumn19
        '
        Me.TreeListColumn19.Caption = "Y"
        Me.TreeListColumn19.FieldName = "Y"
        Me.TreeListColumn19.Name = "TreeListColumn19"
        Me.TreeListColumn19.OptionsColumn.AllowFocus = False
        '
        'TreeListColumn20
        '
        Me.TreeListColumn20.Caption = "RADIATIONCENTER"
        Me.TreeListColumn20.FieldName = "RADIATIONCENTER"
        Me.TreeListColumn20.Name = "TreeListColumn20"
        Me.TreeListColumn20.OptionsColumn.AllowFocus = False
        '
        'TreeListColumn21
        '
        Me.TreeListColumn21.Caption = "DEVICELINKEDTO"
        Me.TreeListColumn21.FieldName = "DEVICELINKEDTO"
        Me.TreeListColumn21.Name = "TreeListColumn21"
        Me.TreeListColumn21.OptionsColumn.AllowFocus = False
        '
        'RepositoryItemImageEdit1
        '
        Me.RepositoryItemImageEdit1.AutoHeight = False
        Me.RepositoryItemImageEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemImageEdit1.Name = "RepositoryItemImageEdit1"
        '
        'gcCampaignValidation
        '
        Me.gcCampaignValidation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcCampaignValidation.Location = New System.Drawing.Point(0, 0)
        Me.gcCampaignValidation.MainView = Me.gvCampaignValidation
        Me.gcCampaignValidation.Name = "gcCampaignValidation"
        Me.gcCampaignValidation.Size = New System.Drawing.Size(0, 0)
        Me.gcCampaignValidation.TabIndex = 11
        Me.gcCampaignValidation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvCampaignValidation})
        '
        'gvCampaignValidation
        '
        Me.gvCampaignValidation.GridControl = Me.gcCampaignValidation
        Me.gvCampaignValidation.Name = "gvCampaignValidation"
        Me.gvCampaignValidation.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampaignValidation.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampaignValidation.OptionsBehavior.Editable = False
        Me.gvCampaignValidation.OptionsBehavior.ReadOnly = True
        Me.gvCampaignValidation.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampaignValidation.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampaignValidation.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampaignValidation.OptionsView.ColumnAutoWidth = False
        Me.gvCampaignValidation.OptionsView.ShowGroupPanel = False
        '
        'tpTmBulk
        '
        Me.tpTmBulk.Controls.Add(Me.sccDetectCamp)
        Me.tpTmBulk.Name = "tpTmBulk"
        Me.tpTmBulk.Size = New System.Drawing.Size(1264, 770)
        Me.tpTmBulk.Text = "Bulk"
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
        Me.sccDetectCamp.Size = New System.Drawing.Size(1264, 770)
        Me.sccDetectCamp.SplitterPosition = 460
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
        Me.sccLeft.Panel1.Controls.Add(Me.grpCampBulk)
        Me.sccLeft.Panel1.MinSize = 300
        Me.sccLeft.Panel1.Text = "Panel1"
        '
        'sccLeft.Panel2
        '
        Me.sccLeft.Panel2.Controls.Add(Me.TableLayoutPanel7)
        Me.sccLeft.Panel2.MinSize = 500
        Me.sccLeft.Panel2.Text = "Panel2"
        Me.sccLeft.Size = New System.Drawing.Size(1264, 460)
        Me.sccLeft.SplitterPosition = 410
        Me.sccLeft.TabIndex = 0
        Me.sccLeft.Text = "SplitContainerControl1"
        '
        'grpCampBulk
        '
        Me.grpCampBulk.Controls.Add(Me.TableLayoutPanel1)
        Me.grpCampBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpCampBulk.Location = New System.Drawing.Point(0, 0)
        Me.grpCampBulk.Name = "grpCampBulk"
        Me.grpCampBulk.Size = New System.Drawing.Size(410, 460)
        Me.grpCampBulk.TabIndex = 0
        Me.grpCampBulk.Text = "Bulk Tilt Campaigns"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.grpCampPropDetect, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.gcCampaignsBulk, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 169.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(406, 435)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'grpCampPropDetect
        '
        Me.grpCampPropDetect.Controls.Add(Me.TableLayoutPanel3)
        Me.grpCampPropDetect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpCampPropDetect.Location = New System.Drawing.Point(2, 268)
        Me.grpCampPropDetect.Margin = New System.Windows.Forms.Padding(2)
        Me.grpCampPropDetect.Name = "grpCampPropDetect"
        Me.grpCampPropDetect.Size = New System.Drawing.Size(402, 165)
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
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl5, 0, 3)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl6, 0, 4)
        Me.TableLayoutPanel3.Controls.Add(Me.lblLastRunTimeBulk, 1, 3)
        Me.TableLayoutPanel3.Controls.Add(Me.lblLastEndTimeBulk, 1, 4)
        Me.TableLayoutPanel3.Controls.Add(Me.lblOwnerBulk, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.ceActiveBulk, 1, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.btnRunNowBulk, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl30, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.ceIsPublicBulk, 1, 2)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 6
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(398, 140)
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
        'LabelControl5
        '
        Me.LabelControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl5.Location = New System.Drawing.Point(3, 85)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl5.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl5.TabIndex = 4
        Me.LabelControl5.Text = "Last Run TIme"
        '
        'LabelControl6
        '
        Me.LabelControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl6.Location = New System.Drawing.Point(3, 111)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl6.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl6.TabIndex = 5
        Me.LabelControl6.Text = "Last End Time"
        '
        'lblLastRunTimeBulk
        '
        Me.TableLayoutPanel3.SetColumnSpan(Me.lblLastRunTimeBulk, 2)
        Me.lblLastRunTimeBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLastRunTimeBulk.Location = New System.Drawing.Point(138, 85)
        Me.lblLastRunTimeBulk.Name = "lblLastRunTimeBulk"
        Me.lblLastRunTimeBulk.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblLastRunTimeBulk.Size = New System.Drawing.Size(257, 20)
        Me.lblLastRunTimeBulk.TabIndex = 6
        '
        'lblLastEndTimeBulk
        '
        Me.TableLayoutPanel3.SetColumnSpan(Me.lblLastEndTimeBulk, 2)
        Me.lblLastEndTimeBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLastEndTimeBulk.Location = New System.Drawing.Point(138, 111)
        Me.lblLastEndTimeBulk.Name = "lblLastEndTimeBulk"
        Me.lblLastEndTimeBulk.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblLastEndTimeBulk.Size = New System.Drawing.Size(257, 20)
        Me.lblLastEndTimeBulk.TabIndex = 7
        '
        'lblOwnerBulk
        '
        Me.lblOwnerBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblOwnerBulk.Location = New System.Drawing.Point(138, 3)
        Me.lblOwnerBulk.Name = "lblOwnerBulk"
        Me.lblOwnerBulk.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblOwnerBulk.Size = New System.Drawing.Size(187, 24)
        Me.lblOwnerBulk.TabIndex = 9
        '
        'ceActiveBulk
        '
        Me.ceActiveBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceActiveBulk.Location = New System.Drawing.Point(140, 33)
        Me.ceActiveBulk.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceActiveBulk.Name = "ceActiveBulk"
        Me.ceActiveBulk.Properties.Caption = ""
        Me.ceActiveBulk.Size = New System.Drawing.Size(185, 20)
        Me.ceActiveBulk.TabIndex = 10
        Me.ceActiveBulk.Tag = "TM_Bulk"
        '
        'btnRunNowBulk
        '
        Me.btnRunNowBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRunNowBulk.Location = New System.Drawing.Point(331, 3)
        Me.btnRunNowBulk.Name = "btnRunNowBulk"
        Me.btnRunNowBulk.Size = New System.Drawing.Size(64, 24)
        Me.btnRunNowBulk.TabIndex = 8
        Me.btnRunNowBulk.Text = "Run Now"
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
        'ceIsPublicBulk
        '
        Me.ceIsPublicBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceIsPublicBulk.Location = New System.Drawing.Point(140, 59)
        Me.ceIsPublicBulk.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceIsPublicBulk.Name = "ceIsPublicBulk"
        Me.ceIsPublicBulk.Properties.Caption = ""
        Me.ceIsPublicBulk.Size = New System.Drawing.Size(185, 20)
        Me.ceIsPublicBulk.TabIndex = 14
        Me.ceIsPublicBulk.Tag = "TM_Bulk"
        '
        'gcCampaignsBulk
        '
        Me.gcCampaignsBulk.AllowDrop = True
        Me.gcCampaignsBulk.ContextMenuStrip = Me.cmsCampaignsBulk
        Me.gcCampaignsBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcCampaignsBulk.Location = New System.Drawing.Point(2, 29)
        Me.gcCampaignsBulk.MainView = Me.gvCampaignsBulk
        Me.gcCampaignsBulk.Margin = New System.Windows.Forms.Padding(2)
        Me.gcCampaignsBulk.Name = "gcCampaignsBulk"
        Me.gcCampaignsBulk.Size = New System.Drawing.Size(402, 235)
        Me.gcCampaignsBulk.TabIndex = 5
        Me.gcCampaignsBulk.Tag = "TM_Bulk"
        Me.gcCampaignsBulk.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvCampaignsBulk, Me.GridView2})
        '
        'cmsCampaignsBulk
        '
        Me.cmsCampaignsBulk.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_RenameCampaignBulk})
        Me.cmsCampaignsBulk.Name = "cm_SON_Incon_dgvResult"
        Me.cmsCampaignsBulk.Size = New System.Drawing.Size(176, 26)
        '
        'tsmi_RenameCampaignBulk
        '
        Me.tsmi_RenameCampaignBulk.Name = "tsmi_RenameCampaignBulk"
        Me.tsmi_RenameCampaignBulk.Size = New System.Drawing.Size(175, 22)
        Me.tsmi_RenameCampaignBulk.Text = "Rename Campaign"
        '
        'gvCampaignsBulk
        '
        Me.gvCampaignsBulk.ActiveFilterEnabled = False
        Me.gvCampaignsBulk.GridControl = Me.gcCampaignsBulk
        Me.gvCampaignsBulk.Name = "gvCampaignsBulk"
        Me.gvCampaignsBulk.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampaignsBulk.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampaignsBulk.OptionsBehavior.Editable = False
        Me.gvCampaignsBulk.OptionsBehavior.ReadOnly = True
        Me.gvCampaignsBulk.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampaignsBulk.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampaignsBulk.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampaignsBulk.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvCampaignsBulk.OptionsSelection.MultiSelect = True
        Me.gvCampaignsBulk.OptionsView.ShowGroupPanel = False
        '
        'GridView2
        '
        Me.GridView2.GridControl = Me.gcCampaignsBulk
        Me.GridView2.Name = "GridView2"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 5
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.btnAddBulk, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.txtSearchBulk, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnDeleteBulk, 4, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnCloneBulk, 3, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnRefreshBulk, 2, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(1, 1)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(404, 25)
        Me.TableLayoutPanel2.TabIndex = 6
        '
        'btnAddBulk
        '
        Me.btnAddBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAddBulk.Location = New System.Drawing.Point(186, 2)
        Me.btnAddBulk.Margin = New System.Windows.Forms.Padding(2)
        Me.btnAddBulk.Name = "btnAddBulk"
        Me.btnAddBulk.Size = New System.Drawing.Size(51, 21)
        Me.btnAddBulk.TabIndex = 8
        Me.btnAddBulk.Tag = "TM_Bulk"
        Me.btnAddBulk.Text = "Add"
        '
        'txtSearchBulk
        '
        Me.txtSearchBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchBulk.Location = New System.Drawing.Point(2, 2)
        Me.txtSearchBulk.Margin = New System.Windows.Forms.Padding(2)
        Me.txtSearchBulk.Name = "txtSearchBulk"
        Me.txtSearchBulk.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSearchBulk.Properties.NullValuePrompt = "Search..."
        Me.txtSearchBulk.Size = New System.Drawing.Size(180, 20)
        Me.txtSearchBulk.TabIndex = 3
        '
        'btnDeleteBulk
        '
        Me.btnDeleteBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDeleteBulk.Location = New System.Drawing.Point(351, 2)
        Me.btnDeleteBulk.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDeleteBulk.Name = "btnDeleteBulk"
        Me.btnDeleteBulk.Size = New System.Drawing.Size(51, 21)
        Me.btnDeleteBulk.TabIndex = 6
        Me.btnDeleteBulk.Tag = "TM_Bulk"
        Me.btnDeleteBulk.Text = "Delete"
        '
        'btnCloneBulk
        '
        Me.btnCloneBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCloneBulk.Location = New System.Drawing.Point(296, 2)
        Me.btnCloneBulk.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCloneBulk.Name = "btnCloneBulk"
        Me.btnCloneBulk.Size = New System.Drawing.Size(51, 21)
        Me.btnCloneBulk.TabIndex = 5
        Me.btnCloneBulk.Tag = "TM_Bulk"
        Me.btnCloneBulk.Text = "Clone"
        '
        'btnRefreshBulk
        '
        Me.btnRefreshBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRefreshBulk.Location = New System.Drawing.Point(241, 2)
        Me.btnRefreshBulk.Margin = New System.Windows.Forms.Padding(2)
        Me.btnRefreshBulk.Name = "btnRefreshBulk"
        Me.btnRefreshBulk.Size = New System.Drawing.Size(51, 21)
        Me.btnRefreshBulk.TabIndex = 7
        Me.btnRefreshBulk.Tag = "TM_Bulk"
        Me.btnRefreshBulk.Text = "Refresh"
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 1
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Controls.Add(Me.grpCampResultBulk, 0, 1)
        Me.TableLayoutPanel7.Controls.Add(Me.grpImportBulk, 0, 0)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 3
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 71.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(844, 460)
        Me.TableLayoutPanel7.TabIndex = 0
        '
        'grpCampResultBulk
        '
        Me.grpCampResultBulk.Controls.Add(Me.TableLayoutPanel28)
        Me.grpCampResultBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpCampResultBulk.Location = New System.Drawing.Point(3, 74)
        Me.grpCampResultBulk.Name = "grpCampResultBulk"
        Me.grpCampResultBulk.Size = New System.Drawing.Size(838, 380)
        Me.grpCampResultBulk.TabIndex = 1
        Me.grpCampResultBulk.Text = "Campaign Result"
        '
        'TableLayoutPanel28
        '
        Me.TableLayoutPanel28.ColumnCount = 1
        Me.TableLayoutPanel28.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel28.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel28.Controls.Add(Me.TableLayoutPanel33, 0, 0)
        Me.TableLayoutPanel28.Controls.Add(Me.xtcTMBulk, 0, 1)
        Me.TableLayoutPanel28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel28.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel28.Name = "TableLayoutPanel28"
        Me.TableLayoutPanel28.RowCount = 2
        Me.TableLayoutPanel28.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel28.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel28.Size = New System.Drawing.Size(834, 355)
        Me.TableLayoutPanel28.TabIndex = 0
        '
        'TableLayoutPanel33
        '
        Me.TableLayoutPanel33.ColumnCount = 3
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 227.0!))
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel33.Controls.Add(Me.LabelControl13, 0, 0)
        Me.TableLayoutPanel33.Controls.Add(Me.cmbResultSetIDBulk, 1, 0)
        Me.TableLayoutPanel33.Controls.Add(Me.btnDeleteResultSetBulk, 2, 0)
        Me.TableLayoutPanel33.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel33.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel33.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel33.Name = "TableLayoutPanel33"
        Me.TableLayoutPanel33.RowCount = 1
        Me.TableLayoutPanel33.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel33.Size = New System.Drawing.Size(830, 26)
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
        'cmbResultSetIDBulk
        '
        Me.cmbResultSetIDBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbResultSetIDBulk.EditValue = ""
        Me.cmbResultSetIDBulk.Location = New System.Drawing.Point(83, 3)
        Me.cmbResultSetIDBulk.Name = "cmbResultSetIDBulk"
        Me.cmbResultSetIDBulk.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbResultSetIDBulk.Properties.Items.AddRange(New Object() {"DAILY", "WEEKLY"})
        Me.cmbResultSetIDBulk.Size = New System.Drawing.Size(221, 20)
        Me.cmbResultSetIDBulk.TabIndex = 13
        '
        'btnDeleteResultSetBulk
        '
        Me.btnDeleteResultSetBulk.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDeleteResultSetBulk.Location = New System.Drawing.Point(309, 2)
        Me.btnDeleteResultSetBulk.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDeleteResultSetBulk.Name = "btnDeleteResultSetBulk"
        Me.btnDeleteResultSetBulk.Size = New System.Drawing.Size(61, 22)
        Me.btnDeleteResultSetBulk.TabIndex = 14
        Me.btnDeleteResultSetBulk.Tag = ""
        Me.btnDeleteResultSetBulk.Text = "Delete"
        '
        'xtcTMBulk
        '
        Me.xtcTMBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtcTMBulk.Location = New System.Drawing.Point(3, 33)
        Me.xtcTMBulk.Name = "xtcTMBulk"
        Me.xtcTMBulk.SelectedTabPage = Me.tpCampBulkSumm
        Me.xtcTMBulk.Size = New System.Drawing.Size(828, 319)
        Me.xtcTMBulk.TabIndex = 4
        Me.xtcTMBulk.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tpCampImportedData, Me.tpCampBulkSumm, Me.tpValidationData, Me.tpCampBulkOutputData})
        '
        'tpCampBulkSumm
        '
        Me.tpCampBulkSumm.Controls.Add(Me.gcSummDataBulk)
        Me.tpCampBulkSumm.Name = "tpCampBulkSumm"
        Me.tpCampBulkSumm.Size = New System.Drawing.Size(826, 294)
        Me.tpCampBulkSumm.Text = "Summary Data"
        '
        'gcSummDataBulk
        '
        Me.gcSummDataBulk.AllowDrop = True
        Me.gcSummDataBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcSummDataBulk.Location = New System.Drawing.Point(0, 0)
        Me.gcSummDataBulk.MainView = Me.gvSummDataBulk
        Me.gcSummDataBulk.Name = "gcSummDataBulk"
        Me.gcSummDataBulk.Size = New System.Drawing.Size(826, 294)
        Me.gcSummDataBulk.TabIndex = 2
        Me.gcSummDataBulk.Tag = "TM_Bulk"
        Me.gcSummDataBulk.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvSummDataBulk, Me.GridView1})
        '
        'gvSummDataBulk
        '
        Me.gvSummDataBulk.ActiveFilterEnabled = False
        Me.gvSummDataBulk.GridControl = Me.gcSummDataBulk
        Me.gvSummDataBulk.Name = "gvSummDataBulk"
        Me.gvSummDataBulk.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvSummDataBulk.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvSummDataBulk.OptionsBehavior.Editable = False
        Me.gvSummDataBulk.OptionsBehavior.ReadOnly = True
        Me.gvSummDataBulk.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvSummDataBulk.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvSummDataBulk.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvSummDataBulk.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvSummDataBulk.OptionsSelection.MultiSelect = True
        Me.gvSummDataBulk.OptionsView.ShowGroupPanel = False
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.gcSummDataBulk
        Me.GridView1.Name = "GridView1"
        '
        'tpCampImportedData
        '
        Me.tpCampImportedData.Controls.Add(Me.gcImportedDataBulk)
        Me.tpCampImportedData.Name = "tpCampImportedData"
        Me.tpCampImportedData.Size = New System.Drawing.Size(826, 294)
        Me.tpCampImportedData.Text = "Imported Data"
        '
        'gcImportedDataBulk
        '
        Me.gcImportedDataBulk.AllowDrop = True
        Me.gcImportedDataBulk.ContextMenuStrip = Me.cmBulkPaste
        Me.gcImportedDataBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcImportedDataBulk.Location = New System.Drawing.Point(0, 0)
        Me.gcImportedDataBulk.MainView = Me.gvImportedDataBulk
        Me.gcImportedDataBulk.Name = "gcImportedDataBulk"
        Me.gcImportedDataBulk.Size = New System.Drawing.Size(826, 294)
        Me.gcImportedDataBulk.TabIndex = 3
        Me.gcImportedDataBulk.Tag = "TM_Bulk"
        Me.gcImportedDataBulk.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvImportedDataBulk, Me.GridView19})
        '
        'cmBulkPaste
        '
        Me.cmBulkPaste.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.cmBulkPaste.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_PasteDataFromClipboard})
        Me.cmBulkPaste.Name = "cmBulkPaste"
        Me.cmBulkPaste.Size = New System.Drawing.Size(189, 26)
        '
        'tsmi_PasteDataFromClipboard
        '
        Me.tsmi_PasteDataFromClipboard.Name = "tsmi_PasteDataFromClipboard"
        Me.tsmi_PasteDataFromClipboard.Size = New System.Drawing.Size(188, 22)
        Me.tsmi_PasteDataFromClipboard.Text = "Paste From Clipboard"
        '
        'gvImportedDataBulk
        '
        Me.gvImportedDataBulk.ActiveFilterEnabled = False
        Me.gvImportedDataBulk.GridControl = Me.gcImportedDataBulk
        Me.gvImportedDataBulk.Name = "gvImportedDataBulk"
        Me.gvImportedDataBulk.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvImportedDataBulk.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvImportedDataBulk.OptionsBehavior.Editable = False
        Me.gvImportedDataBulk.OptionsBehavior.ReadOnly = True
        Me.gvImportedDataBulk.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvImportedDataBulk.OptionsClipboard.AllowCsvFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvImportedDataBulk.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvImportedDataBulk.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvImportedDataBulk.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Append
        Me.gvImportedDataBulk.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvImportedDataBulk.OptionsSelection.MultiSelect = True
        Me.gvImportedDataBulk.OptionsView.ShowGroupPanel = False
        '
        'GridView19
        '
        Me.GridView19.GridControl = Me.gcImportedDataBulk
        Me.GridView19.Name = "GridView19"
        '
        'tpValidationData
        '
        Me.tpValidationData.Controls.Add(Me.gcValidationData)
        Me.tpValidationData.Name = "tpValidationData"
        Me.tpValidationData.Size = New System.Drawing.Size(826, 294)
        Me.tpValidationData.Text = "Validation Data"
        '
        'gcValidationData
        '
        Me.gcValidationData.AllowDrop = True
        Me.gcValidationData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcValidationData.Location = New System.Drawing.Point(0, 0)
        Me.gcValidationData.MainView = Me.gvValidationData
        Me.gcValidationData.Name = "gcValidationData"
        Me.gcValidationData.Size = New System.Drawing.Size(826, 294)
        Me.gcValidationData.TabIndex = 3
        Me.gcValidationData.Tag = "TM_Bulk"
        Me.gcValidationData.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvValidationData, Me.GridView20})
        '
        'gvValidationData
        '
        Me.gvValidationData.ActiveFilterEnabled = False
        Me.gvValidationData.GridControl = Me.gcValidationData
        Me.gvValidationData.Name = "gvValidationData"
        Me.gvValidationData.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvValidationData.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvValidationData.OptionsBehavior.Editable = False
        Me.gvValidationData.OptionsBehavior.ReadOnly = True
        Me.gvValidationData.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvValidationData.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvValidationData.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvValidationData.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvValidationData.OptionsSelection.MultiSelect = True
        Me.gvValidationData.OptionsView.ShowGroupPanel = False
        '
        'GridView20
        '
        Me.GridView20.GridControl = Me.gcValidationData
        Me.GridView20.Name = "GridView20"
        '
        'tpCampBulkOutputData
        '
        Me.tpCampBulkOutputData.Controls.Add(Me.TableLayoutPanel34)
        Me.tpCampBulkOutputData.Name = "tpCampBulkOutputData"
        Me.tpCampBulkOutputData.Size = New System.Drawing.Size(826, 294)
        Me.tpCampBulkOutputData.Text = "Output Data"
        '
        'TableLayoutPanel34
        '
        Me.TableLayoutPanel34.ColumnCount = 1
        Me.TableLayoutPanel34.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel34.Controls.Add(Me.gcOutputDataBulk, 0, 1)
        Me.TableLayoutPanel34.Controls.Add(Me.TableLayoutPanel35, 0, 0)
        Me.TableLayoutPanel34.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel34.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel34.Name = "TableLayoutPanel34"
        Me.TableLayoutPanel34.RowCount = 2
        Me.TableLayoutPanel34.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel34.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel34.Size = New System.Drawing.Size(826, 294)
        Me.TableLayoutPanel34.TabIndex = 0
        '
        'gcOutputDataBulk
        '
        Me.gcOutputDataBulk.AllowDrop = True
        Me.gcOutputDataBulk.ContextMenuStrip = Me.cmsLaunchAdHocTiltMngr
        Me.gcOutputDataBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcOutputDataBulk.Location = New System.Drawing.Point(3, 38)
        Me.gcOutputDataBulk.MainView = Me.gvOutputDataBulk
        Me.gcOutputDataBulk.Name = "gcOutputDataBulk"
        Me.gcOutputDataBulk.Size = New System.Drawing.Size(820, 253)
        Me.gcOutputDataBulk.TabIndex = 4
        Me.gcOutputDataBulk.Tag = "TM_Bulk"
        Me.gcOutputDataBulk.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvOutputDataBulk, Me.GridView21})
        '
        'cmsLaunchAdHocTiltMngr
        '
        Me.cmsLaunchAdHocTiltMngr.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.cmsLaunchAdHocTiltMngr.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_AdHocTiltManager})
        Me.cmsLaunchAdHocTiltMngr.Name = "cmsLaunchAdHocTiltMngr"
        Me.cmsLaunchAdHocTiltMngr.Size = New System.Drawing.Size(221, 26)
        '
        'tsmi_AdHocTiltManager
        '
        Me.tsmi_AdHocTiltManager.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_AddNewTiltCampaign})
        Me.tsmi_AdHocTiltManager.Name = "tsmi_AdHocTiltManager"
        Me.tsmi_AdHocTiltManager.Size = New System.Drawing.Size(220, 22)
        Me.tsmi_AdHocTiltManager.Text = "Send - Ad Hoc Tilt Manager"
        '
        'tsmi_AddNewTiltCampaign
        '
        Me.tsmi_AddNewTiltCampaign.Name = "tsmi_AddNewTiltCampaign"
        Me.tsmi_AddNewTiltCampaign.Size = New System.Drawing.Size(200, 22)
        Me.tsmi_AddNewTiltCampaign.Text = "Add New Tilt Campaign"
        '
        'gvOutputDataBulk
        '
        Me.gvOutputDataBulk.ActiveFilterEnabled = False
        Me.gvOutputDataBulk.GridControl = Me.gcOutputDataBulk
        Me.gvOutputDataBulk.Name = "gvOutputDataBulk"
        Me.gvOutputDataBulk.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvOutputDataBulk.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvOutputDataBulk.OptionsBehavior.Editable = False
        Me.gvOutputDataBulk.OptionsBehavior.ReadOnly = True
        Me.gvOutputDataBulk.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvOutputDataBulk.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvOutputDataBulk.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvOutputDataBulk.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.gvOutputDataBulk.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvOutputDataBulk.OptionsSelection.MultiSelect = True
        Me.gvOutputDataBulk.OptionsView.ShowGroupPanel = False
        '
        'GridView21
        '
        Me.GridView21.GridControl = Me.gcOutputDataBulk
        Me.GridView21.Name = "GridView21"
        '
        'TableLayoutPanel35
        '
        Me.TableLayoutPanel35.ColumnCount = 3
        Me.TableLayoutPanel35.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel35.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel35.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel35.Controls.Add(Me.btnDetectDataLoadGrid, 0, 0)
        Me.TableLayoutPanel35.Controls.Add(Me.btnDataAllCsvBulk, 1, 0)
        Me.TableLayoutPanel35.Controls.Add(Me.lblDataRowCountBulk, 2, 0)
        Me.TableLayoutPanel35.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel35.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel35.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel35.Name = "TableLayoutPanel35"
        Me.TableLayoutPanel35.RowCount = 1
        Me.TableLayoutPanel35.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel35.Size = New System.Drawing.Size(822, 31)
        Me.TableLayoutPanel35.TabIndex = 0
        '
        'btnDetectDataLoadGrid
        '
        Me.btnDetectDataLoadGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDetectDataLoadGrid.Location = New System.Drawing.Point(3, 3)
        Me.btnDetectDataLoadGrid.Name = "btnDetectDataLoadGrid"
        Me.btnDetectDataLoadGrid.Size = New System.Drawing.Size(94, 25)
        Me.btnDetectDataLoadGrid.TabIndex = 0
        Me.btnDetectDataLoadGrid.Tag = "TM_Bulk"
        Me.btnDetectDataLoadGrid.Text = "Load To Grid"
        '
        'btnDataAllCsvBulk
        '
        Me.btnDataAllCsvBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDataAllCsvBulk.Location = New System.Drawing.Point(103, 3)
        Me.btnDataAllCsvBulk.Name = "btnDataAllCsvBulk"
        Me.btnDataAllCsvBulk.Size = New System.Drawing.Size(94, 25)
        Me.btnDataAllCsvBulk.TabIndex = 1
        Me.btnDataAllCsvBulk.Tag = "TM_Bulk"
        Me.btnDataAllCsvBulk.Text = "All Data To CSV"
        '
        'lblDataRowCountBulk
        '
        Me.lblDataRowCountBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDataRowCountBulk.Location = New System.Drawing.Point(203, 3)
        Me.lblDataRowCountBulk.Name = "lblDataRowCountBulk"
        Me.lblDataRowCountBulk.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblDataRowCountBulk.Size = New System.Drawing.Size(616, 25)
        Me.lblDataRowCountBulk.TabIndex = 2
        Me.lblDataRowCountBulk.Text = "Count of Records: "
        Me.lblDataRowCountBulk.Visible = False
        '
        'grpImportBulk
        '
        Me.grpImportBulk.Controls.Add(Me.TableLayoutPanel29)
        Me.grpImportBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpImportBulk.Location = New System.Drawing.Point(3, 3)
        Me.grpImportBulk.Name = "grpImportBulk"
        Me.grpImportBulk.Size = New System.Drawing.Size(838, 65)
        Me.grpImportBulk.TabIndex = 2
        Me.grpImportBulk.Text = "Import"
        '
        'TableLayoutPanel29
        '
        Me.TableLayoutPanel29.ColumnCount = 5
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 315.0!))
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65.0!))
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel29.Controls.Add(Me.LabelControl3, 0, 1)
        Me.TableLayoutPanel29.Controls.Add(Me.btnImportBulk, 3, 1)
        Me.TableLayoutPanel29.Controls.Add(Me.lblStatus, 4, 1)
        Me.TableLayoutPanel29.Controls.Add(Me.btnOpenFile, 2, 1)
        Me.TableLayoutPanel29.Controls.Add(Me.txtImportfileName, 1, 1)
        Me.TableLayoutPanel29.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel29.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel29.Name = "TableLayoutPanel29"
        Me.TableLayoutPanel29.RowCount = 3
        Me.TableLayoutPanel29.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel29.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel29.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel29.Size = New System.Drawing.Size(834, 40)
        Me.TableLayoutPanel29.TabIndex = 0
        '
        'LabelControl3
        '
        Me.LabelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl3.Location = New System.Drawing.Point(3, 10)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl3.Size = New System.Drawing.Size(54, 20)
        Me.LabelControl3.TabIndex = 1
        Me.LabelControl3.Text = "File Name"
        '
        'btnImportBulk
        '
        Me.btnImportBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnImportBulk.Location = New System.Drawing.Point(407, 9)
        Me.btnImportBulk.Margin = New System.Windows.Forms.Padding(2)
        Me.btnImportBulk.Name = "btnImportBulk"
        Me.btnImportBulk.Size = New System.Drawing.Size(61, 22)
        Me.btnImportBulk.TabIndex = 15
        Me.btnImportBulk.Tag = ""
        Me.btnImportBulk.Text = "Import"
        '
        'lblStatus
        '
        Me.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblStatus.Location = New System.Drawing.Point(473, 10)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblStatus.Size = New System.Drawing.Size(358, 20)
        Me.lblStatus.TabIndex = 16
        Me.lblStatus.Text = "File Import Format CSV: <CELLNAME>;<ETILT>"
        '
        'btnOpenFile
        '
        Me.btnOpenFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOpenFile.ImageOptions.Image = Global.IOS.My.Resources.Resources.import_16x16
        Me.btnOpenFile.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btnOpenFile.Location = New System.Drawing.Point(377, 9)
        Me.btnOpenFile.Margin = New System.Windows.Forms.Padding(2)
        Me.btnOpenFile.Name = "btnOpenFile"
        Me.btnOpenFile.Size = New System.Drawing.Size(26, 22)
        Me.btnOpenFile.TabIndex = 17
        Me.btnOpenFile.ToolTip = "Browse .CSV file through open file dialog"
        '
        'txtImportfileName
        '
        Me.txtImportfileName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtImportfileName.Location = New System.Drawing.Point(63, 10)
        Me.txtImportfileName.Name = "txtImportfileName"
        Me.txtImportfileName.Size = New System.Drawing.Size(309, 20)
        Me.txtImportfileName.TabIndex = 18
        '
        'TableLayoutPanel21
        '
        Me.TableLayoutPanel21.ColumnCount = 1
        Me.TableLayoutPanel21.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel21.Controls.Add(Me.grpConfigSummBulk, 0, 0)
        Me.TableLayoutPanel21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel21.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel21.Name = "TableLayoutPanel21"
        Me.TableLayoutPanel21.RowCount = 1
        Me.TableLayoutPanel21.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel21.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300.0!))
        Me.TableLayoutPanel21.Size = New System.Drawing.Size(1264, 300)
        Me.TableLayoutPanel21.TabIndex = 1
        '
        'grpConfigSummBulk
        '
        Me.grpConfigSummBulk.Controls.Add(Me.TableLayoutPanel37)
        Me.grpConfigSummBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpConfigSummBulk.Location = New System.Drawing.Point(3, 3)
        Me.grpConfigSummBulk.Name = "grpConfigSummBulk"
        Me.grpConfigSummBulk.Size = New System.Drawing.Size(1258, 294)
        Me.grpConfigSummBulk.TabIndex = 0
        Me.grpConfigSummBulk.Text = "Configuration Summary"
        '
        'TableLayoutPanel37
        '
        Me.TableLayoutPanel37.ColumnCount = 2
        Me.TableLayoutPanel37.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel37.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350.0!))
        Me.TableLayoutPanel37.Controls.Add(Me.TableLayoutPanel4, 1, 0)
        Me.TableLayoutPanel37.Controls.Add(Me.gcConfigSummBulk, 0, 0)
        Me.TableLayoutPanel37.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel37.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel37.Name = "TableLayoutPanel37"
        Me.TableLayoutPanel37.RowCount = 1
        Me.TableLayoutPanel37.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel37.Size = New System.Drawing.Size(1254, 269)
        Me.TableLayoutPanel37.TabIndex = 4
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 1
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.grpLayerPropBulk, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.btnListMngrBulk, 0, 1)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(907, 3)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(344, 263)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'grpLayerPropBulk
        '
        Me.grpLayerPropBulk.Controls.Add(Me.TableLayoutPanel59)
        Me.grpLayerPropBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpLayerPropBulk.Location = New System.Drawing.Point(3, 3)
        Me.grpLayerPropBulk.Name = "grpLayerPropBulk"
        Me.grpLayerPropBulk.Size = New System.Drawing.Size(338, 225)
        Me.grpLayerPropBulk.TabIndex = 1
        Me.grpLayerPropBulk.Text = "Layer Properties"
        '
        'TableLayoutPanel59
        '
        Me.TableLayoutPanel59.ColumnCount = 1
        Me.TableLayoutPanel59.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel59.Controls.Add(Me.layerPropGridBulk, 0, 0)
        Me.TableLayoutPanel59.Controls.Add(Me.TableLayoutPanel31, 0, 1)
        Me.TableLayoutPanel59.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel59.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel59.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel59.Name = "TableLayoutPanel59"
        Me.TableLayoutPanel59.RowCount = 2
        Me.TableLayoutPanel59.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel59.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel59.Size = New System.Drawing.Size(334, 200)
        Me.TableLayoutPanel59.TabIndex = 1
        '
        'layerPropGridBulk
        '
        Me.layerPropGridBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layerPropGridBulk.LineColor = System.Drawing.SystemColors.ControlDark
        Me.layerPropGridBulk.Location = New System.Drawing.Point(3, 3)
        Me.layerPropGridBulk.Name = "layerPropGridBulk"
        Me.layerPropGridBulk.Size = New System.Drawing.Size(328, 167)
        Me.layerPropGridBulk.TabIndex = 0
        Me.layerPropGridBulk.Tag = "TM_Bulk"
        Me.layerPropGridBulk.ToolbarVisible = False
        '
        'TableLayoutPanel31
        '
        Me.TableLayoutPanel31.ColumnCount = 2
        Me.TableLayoutPanel31.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel31.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel31.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel31.Controls.Add(Me.btnLayerPropertiesAddBulk, 1, 0)
        Me.TableLayoutPanel31.Controls.Add(Me.ceApplyConfigAllBulk, 0, 0)
        Me.TableLayoutPanel31.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel31.Location = New System.Drawing.Point(0, 173)
        Me.TableLayoutPanel31.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel31.Name = "TableLayoutPanel31"
        Me.TableLayoutPanel31.RowCount = 1
        Me.TableLayoutPanel31.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel31.Size = New System.Drawing.Size(334, 27)
        Me.TableLayoutPanel31.TabIndex = 1
        '
        'btnLayerPropertiesAddBulk
        '
        Me.btnLayerPropertiesAddBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnLayerPropertiesAddBulk.Location = New System.Drawing.Point(281, 2)
        Me.btnLayerPropertiesAddBulk.Margin = New System.Windows.Forms.Padding(2)
        Me.btnLayerPropertiesAddBulk.Name = "btnLayerPropertiesAddBulk"
        Me.btnLayerPropertiesAddBulk.Size = New System.Drawing.Size(51, 23)
        Me.btnLayerPropertiesAddBulk.TabIndex = 9
        Me.btnLayerPropertiesAddBulk.Tag = "TM_Bulk"
        Me.btnLayerPropertiesAddBulk.Text = "Add"
        '
        'ceApplyConfigAllBulk
        '
        Me.ceApplyConfigAllBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceApplyConfigAllBulk.Location = New System.Drawing.Point(5, 3)
        Me.ceApplyConfigAllBulk.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceApplyConfigAllBulk.Name = "ceApplyConfigAllBulk"
        Me.ceApplyConfigAllBulk.Properties.Caption = "Apply changes to all configuration"
        Me.ceApplyConfigAllBulk.Size = New System.Drawing.Size(271, 21)
        Me.ceApplyConfigAllBulk.TabIndex = 1
        '
        'btnListMngrBulk
        '
        Me.btnListMngrBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnListMngrBulk.Location = New System.Drawing.Point(3, 234)
        Me.btnListMngrBulk.Name = "btnListMngrBulk"
        Me.btnListMngrBulk.Size = New System.Drawing.Size(338, 26)
        Me.btnListMngrBulk.TabIndex = 1
        Me.btnListMngrBulk.Text = "List Manager"
        '
        'gcConfigSummBulk
        '
        Me.gcConfigSummBulk.AllowDrop = True
        Me.gcConfigSummBulk.ContextMenuStrip = Me.cmsConfigSummary
        Me.gcConfigSummBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcConfigSummBulk.Location = New System.Drawing.Point(3, 3)
        Me.gcConfigSummBulk.MainView = Me.gvConfigSummBulk
        Me.gcConfigSummBulk.Name = "gcConfigSummBulk"
        Me.gcConfigSummBulk.Size = New System.Drawing.Size(898, 263)
        Me.gcConfigSummBulk.TabIndex = 3
        Me.gcConfigSummBulk.Tag = "TM_Bulk"
        Me.gcConfigSummBulk.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvConfigSummBulk, Me.GridView4})
        '
        'cmsConfigSummary
        '
        Me.cmsConfigSummary.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_DeleteSelectedRows})
        Me.cmsConfigSummary.Name = "cmsConfigurationSummary"
        Me.cmsConfigSummary.Size = New System.Drawing.Size(194, 26)
        '
        'tsmi_DeleteSelectedRows
        '
        Me.tsmi_DeleteSelectedRows.Name = "tsmi_DeleteSelectedRows"
        Me.tsmi_DeleteSelectedRows.Size = New System.Drawing.Size(193, 22)
        Me.tsmi_DeleteSelectedRows.Text = "Delete Selected Row(s)"
        '
        'gvConfigSummBulk
        '
        Me.gvConfigSummBulk.ActiveFilterEnabled = False
        Me.gvConfigSummBulk.GridControl = Me.gcConfigSummBulk
        Me.gvConfigSummBulk.Name = "gvConfigSummBulk"
        Me.gvConfigSummBulk.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvConfigSummBulk.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvConfigSummBulk.OptionsBehavior.Editable = False
        Me.gvConfigSummBulk.OptionsBehavior.ReadOnly = True
        Me.gvConfigSummBulk.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvConfigSummBulk.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvConfigSummBulk.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvConfigSummBulk.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvConfigSummBulk.OptionsSelection.MultiSelect = True
        Me.gvConfigSummBulk.OptionsView.ShowGroupPanel = False
        '
        'GridView4
        '
        Me.GridView4.GridControl = Me.gcConfigSummBulk
        Me.GridView4.Name = "GridView4"
        '
        'tpTmAudit
        '
        Me.tpTmAudit.Controls.Add(Me.SplitContainerControl1)
        Me.tpTmAudit.Name = "tpTmAudit"
        Me.tpTmAudit.Size = New System.Drawing.Size(1264, 770)
        Me.tpTmAudit.Text = "Audit"
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Horizontal = False
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        '
        'SplitContainerControl1.Panel1
        '
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.SplitContainerControl2)
        Me.SplitContainerControl1.Panel1.MinSize = 400
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        '
        'SplitContainerControl1.Panel2
        '
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.TableLayoutPanel20)
        Me.SplitContainerControl1.Panel2.MinSize = 300
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1264, 770)
        Me.SplitContainerControl1.SplitterPosition = 460
        Me.SplitContainerControl1.TabIndex = 1
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'SplitContainerControl2
        '
        Me.SplitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl2.Name = "SplitContainerControl2"
        '
        'SplitContainerControl2.Panel1
        '
        Me.SplitContainerControl2.Panel1.Controls.Add(Me.grpCampAudit)
        Me.SplitContainerControl2.Panel1.MinSize = 300
        Me.SplitContainerControl2.Panel1.Text = "Panel1"
        '
        'SplitContainerControl2.Panel2
        '
        Me.SplitContainerControl2.Panel2.Controls.Add(Me.TableLayoutPanel9)
        Me.SplitContainerControl2.Panel2.MinSize = 500
        Me.SplitContainerControl2.Panel2.Text = "Panel2"
        Me.SplitContainerControl2.Size = New System.Drawing.Size(1264, 460)
        Me.SplitContainerControl2.SplitterPosition = 410
        Me.SplitContainerControl2.TabIndex = 0
        Me.SplitContainerControl2.Text = "SplitContainerControl1"
        '
        'grpCampAudit
        '
        Me.grpCampAudit.Controls.Add(Me.TableLayoutPanel5)
        Me.grpCampAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpCampAudit.Location = New System.Drawing.Point(0, 0)
        Me.grpCampAudit.Name = "grpCampAudit"
        Me.grpCampAudit.Size = New System.Drawing.Size(410, 460)
        Me.grpCampAudit.TabIndex = 0
        Me.grpCampAudit.Text = "Audit Tilt Campaigns"
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 1
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.GroupControl5, 0, 2)
        Me.TableLayoutPanel5.Controls.Add(Me.gcCampaignsAudit, 0, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.TableLayoutPanel8, 0, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 3
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 168.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(406, 435)
        Me.TableLayoutPanel5.TabIndex = 0
        '
        'GroupControl5
        '
        Me.GroupControl5.Controls.Add(Me.TableLayoutPanel6)
        Me.GroupControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl5.Location = New System.Drawing.Point(2, 269)
        Me.GroupControl5.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupControl5.Name = "GroupControl5"
        Me.GroupControl5.Size = New System.Drawing.Size(402, 164)
        Me.GroupControl5.TabIndex = 4
        Me.GroupControl5.Text = "Campaign Properties"
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 3
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.LabelControl8, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.LabelControl14, 0, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.LabelControl17, 0, 3)
        Me.TableLayoutPanel6.Controls.Add(Me.LabelControl18, 0, 4)
        Me.TableLayoutPanel6.Controls.Add(Me.lblLastRunTimeAudit, 1, 3)
        Me.TableLayoutPanel6.Controls.Add(Me.lblLastEndTimeAudit, 1, 4)
        Me.TableLayoutPanel6.Controls.Add(Me.lblOwnerAudit, 1, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.ceActiveAudit, 1, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.btnRunNowAudit, 2, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.LabelControl22, 0, 2)
        Me.TableLayoutPanel6.Controls.Add(Me.ceIsPublicAudit, 1, 2)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 6
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(398, 139)
        Me.TableLayoutPanel6.TabIndex = 0
        '
        'LabelControl8
        '
        Me.LabelControl8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl8.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl8.Size = New System.Drawing.Size(129, 24)
        Me.LabelControl8.TabIndex = 0
        Me.LabelControl8.Text = "Owner"
        '
        'LabelControl14
        '
        Me.LabelControl14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl14.Location = New System.Drawing.Point(3, 33)
        Me.LabelControl14.Name = "LabelControl14"
        Me.LabelControl14.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl14.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl14.TabIndex = 1
        Me.LabelControl14.Text = "Active"
        '
        'LabelControl17
        '
        Me.LabelControl17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl17.Location = New System.Drawing.Point(3, 85)
        Me.LabelControl17.Name = "LabelControl17"
        Me.LabelControl17.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl17.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl17.TabIndex = 4
        Me.LabelControl17.Text = "Last Run TIme"
        '
        'LabelControl18
        '
        Me.LabelControl18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl18.Location = New System.Drawing.Point(3, 111)
        Me.LabelControl18.Name = "LabelControl18"
        Me.LabelControl18.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl18.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl18.TabIndex = 5
        Me.LabelControl18.Text = "Last End Time"
        '
        'lblLastRunTimeAudit
        '
        Me.TableLayoutPanel6.SetColumnSpan(Me.lblLastRunTimeAudit, 2)
        Me.lblLastRunTimeAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLastRunTimeAudit.Location = New System.Drawing.Point(138, 85)
        Me.lblLastRunTimeAudit.Name = "lblLastRunTimeAudit"
        Me.lblLastRunTimeAudit.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblLastRunTimeAudit.Size = New System.Drawing.Size(257, 20)
        Me.lblLastRunTimeAudit.TabIndex = 6
        '
        'lblLastEndTimeAudit
        '
        Me.TableLayoutPanel6.SetColumnSpan(Me.lblLastEndTimeAudit, 2)
        Me.lblLastEndTimeAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLastEndTimeAudit.Location = New System.Drawing.Point(138, 111)
        Me.lblLastEndTimeAudit.Name = "lblLastEndTimeAudit"
        Me.lblLastEndTimeAudit.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblLastEndTimeAudit.Size = New System.Drawing.Size(257, 20)
        Me.lblLastEndTimeAudit.TabIndex = 7
        '
        'lblOwnerAudit
        '
        Me.lblOwnerAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblOwnerAudit.Location = New System.Drawing.Point(138, 3)
        Me.lblOwnerAudit.Name = "lblOwnerAudit"
        Me.lblOwnerAudit.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblOwnerAudit.Size = New System.Drawing.Size(187, 24)
        Me.lblOwnerAudit.TabIndex = 9
        '
        'ceActiveAudit
        '
        Me.ceActiveAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceActiveAudit.Location = New System.Drawing.Point(140, 33)
        Me.ceActiveAudit.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceActiveAudit.Name = "ceActiveAudit"
        Me.ceActiveAudit.Properties.Caption = ""
        Me.ceActiveAudit.Size = New System.Drawing.Size(185, 20)
        Me.ceActiveAudit.TabIndex = 10
        Me.ceActiveAudit.Tag = "TM_Audit"
        '
        'btnRunNowAudit
        '
        Me.btnRunNowAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRunNowAudit.Location = New System.Drawing.Point(331, 3)
        Me.btnRunNowAudit.Name = "btnRunNowAudit"
        Me.btnRunNowAudit.Size = New System.Drawing.Size(64, 24)
        Me.btnRunNowAudit.TabIndex = 8
        Me.btnRunNowAudit.Text = "Run Now"
        '
        'LabelControl22
        '
        Me.LabelControl22.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl22.Location = New System.Drawing.Point(3, 59)
        Me.LabelControl22.Name = "LabelControl22"
        Me.LabelControl22.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl22.Size = New System.Drawing.Size(129, 20)
        Me.LabelControl22.TabIndex = 13
        Me.LabelControl22.Text = "Is Public"
        '
        'ceIsPublicAudit
        '
        Me.ceIsPublicAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceIsPublicAudit.Location = New System.Drawing.Point(140, 59)
        Me.ceIsPublicAudit.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceIsPublicAudit.Name = "ceIsPublicAudit"
        Me.ceIsPublicAudit.Properties.Caption = ""
        Me.ceIsPublicAudit.Size = New System.Drawing.Size(185, 20)
        Me.ceIsPublicAudit.TabIndex = 14
        Me.ceIsPublicAudit.Tag = "TM_Audit"
        '
        'gcCampaignsAudit
        '
        Me.gcCampaignsAudit.AllowDrop = True
        Me.gcCampaignsAudit.ContextMenuStrip = Me.cmsCampaignsAudit
        Me.gcCampaignsAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcCampaignsAudit.Location = New System.Drawing.Point(2, 29)
        Me.gcCampaignsAudit.MainView = Me.gvCampaignsAudit
        Me.gcCampaignsAudit.Margin = New System.Windows.Forms.Padding(2)
        Me.gcCampaignsAudit.Name = "gcCampaignsAudit"
        Me.gcCampaignsAudit.Size = New System.Drawing.Size(402, 236)
        Me.gcCampaignsAudit.TabIndex = 5
        Me.gcCampaignsAudit.Tag = "TM_Audit"
        Me.gcCampaignsAudit.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvCampaignsAudit, Me.GridView9})
        '
        'cmsCampaignsAudit
        '
        Me.cmsCampaignsAudit.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_RenameCampaignAudit})
        Me.cmsCampaignsAudit.Name = "cm_SON_Incon_dgvResult"
        Me.cmsCampaignsAudit.Size = New System.Drawing.Size(176, 26)
        '
        'tsmi_RenameCampaignAudit
        '
        Me.tsmi_RenameCampaignAudit.Name = "tsmi_RenameCampaignAudit"
        Me.tsmi_RenameCampaignAudit.Size = New System.Drawing.Size(175, 22)
        Me.tsmi_RenameCampaignAudit.Text = "Rename Campaign"
        '
        'gvCampaignsAudit
        '
        Me.gvCampaignsAudit.ActiveFilterEnabled = False
        Me.gvCampaignsAudit.GridControl = Me.gcCampaignsAudit
        Me.gvCampaignsAudit.Name = "gvCampaignsAudit"
        Me.gvCampaignsAudit.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampaignsAudit.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampaignsAudit.OptionsBehavior.Editable = False
        Me.gvCampaignsAudit.OptionsBehavior.ReadOnly = True
        Me.gvCampaignsAudit.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampaignsAudit.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampaignsAudit.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampaignsAudit.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvCampaignsAudit.OptionsSelection.MultiSelect = True
        Me.gvCampaignsAudit.OptionsView.ShowGroupPanel = False
        '
        'GridView9
        '
        Me.GridView9.GridControl = Me.gcCampaignsAudit
        Me.GridView9.Name = "GridView9"
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 4
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel8.Controls.Add(Me.btnRefreshAudit, 0, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.txtSearchAudit, 0, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.btnDeleteAudit, 3, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.btnCloneAudit, 2, 0)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(1, 1)
        Me.TableLayoutPanel8.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 1
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(404, 25)
        Me.TableLayoutPanel8.TabIndex = 6
        '
        'btnRefreshAudit
        '
        Me.btnRefreshAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRefreshAudit.Location = New System.Drawing.Point(241, 2)
        Me.btnRefreshAudit.Margin = New System.Windows.Forms.Padding(2)
        Me.btnRefreshAudit.Name = "btnRefreshAudit"
        Me.btnRefreshAudit.Size = New System.Drawing.Size(51, 21)
        Me.btnRefreshAudit.TabIndex = 7
        Me.btnRefreshAudit.Tag = "TM_Audit"
        Me.btnRefreshAudit.Text = "Refresh"
        '
        'txtSearchAudit
        '
        Me.txtSearchAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchAudit.Location = New System.Drawing.Point(2, 2)
        Me.txtSearchAudit.Margin = New System.Windows.Forms.Padding(2)
        Me.txtSearchAudit.Name = "txtSearchAudit"
        Me.txtSearchAudit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSearchAudit.Properties.NullValuePrompt = "Search..."
        Me.txtSearchAudit.Size = New System.Drawing.Size(235, 20)
        Me.txtSearchAudit.TabIndex = 3
        '
        'btnDeleteAudit
        '
        Me.btnDeleteAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDeleteAudit.Location = New System.Drawing.Point(351, 2)
        Me.btnDeleteAudit.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDeleteAudit.Name = "btnDeleteAudit"
        Me.btnDeleteAudit.Size = New System.Drawing.Size(51, 21)
        Me.btnDeleteAudit.TabIndex = 6
        Me.btnDeleteAudit.Tag = "TM_Audit"
        Me.btnDeleteAudit.Text = "Delete"
        '
        'btnCloneAudit
        '
        Me.btnCloneAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCloneAudit.Location = New System.Drawing.Point(296, 2)
        Me.btnCloneAudit.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCloneAudit.Name = "btnCloneAudit"
        Me.btnCloneAudit.Size = New System.Drawing.Size(51, 21)
        Me.btnCloneAudit.TabIndex = 5
        Me.btnCloneAudit.Tag = "TM_Audit"
        Me.btnCloneAudit.Text = "Clone"
        '
        'TableLayoutPanel9
        '
        Me.TableLayoutPanel9.ColumnCount = 1
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel9.Controls.Add(Me.grpCampResultAudit, 0, 0)
        Me.TableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel9.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel9.Name = "TableLayoutPanel9"
        Me.TableLayoutPanel9.RowCount = 2
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3.0!))
        Me.TableLayoutPanel9.Size = New System.Drawing.Size(844, 460)
        Me.TableLayoutPanel9.TabIndex = 0
        '
        'grpCampResultAudit
        '
        Me.grpCampResultAudit.Controls.Add(Me.TableLayoutPanel16)
        Me.grpCampResultAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpCampResultAudit.Location = New System.Drawing.Point(3, 3)
        Me.grpCampResultAudit.Name = "grpCampResultAudit"
        Me.grpCampResultAudit.Size = New System.Drawing.Size(838, 451)
        Me.grpCampResultAudit.TabIndex = 1
        Me.grpCampResultAudit.Text = "Campaign Result"
        '
        'TableLayoutPanel16
        '
        Me.TableLayoutPanel16.ColumnCount = 1
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel16.Controls.Add(Me.xtcTMAudit, 0, 1)
        Me.TableLayoutPanel16.Controls.Add(Me.TableLayoutPanel17, 0, 0)
        Me.TableLayoutPanel16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel16.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel16.Name = "TableLayoutPanel16"
        Me.TableLayoutPanel16.RowCount = 2
        Me.TableLayoutPanel16.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel16.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel16.Size = New System.Drawing.Size(834, 426)
        Me.TableLayoutPanel16.TabIndex = 0
        '
        'xtcTMAudit
        '
        Me.xtcTMAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtcTMAudit.Location = New System.Drawing.Point(3, 33)
        Me.xtcTMAudit.Name = "xtcTMAudit"
        Me.xtcTMAudit.SelectedTabPage = Me.XtraTabPage1
        Me.xtcTMAudit.Size = New System.Drawing.Size(828, 390)
        Me.xtcTMAudit.TabIndex = 5
        Me.xtcTMAudit.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage2, Me.XtraTabPage1, Me.XtraTabPage3, Me.XtraTabPage4})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.gcSummDataAudit)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(826, 365)
        Me.XtraTabPage1.Text = "Summary Data"
        '
        'gcSummDataAudit
        '
        Me.gcSummDataAudit.AllowDrop = True
        Me.gcSummDataAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcSummDataAudit.Location = New System.Drawing.Point(0, 0)
        Me.gcSummDataAudit.MainView = Me.gvSummDataAudit
        Me.gcSummDataAudit.Name = "gcSummDataAudit"
        Me.gcSummDataAudit.Size = New System.Drawing.Size(826, 365)
        Me.gcSummDataAudit.TabIndex = 2
        Me.gcSummDataAudit.Tag = "TM_Audit"
        Me.gcSummDataAudit.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvSummDataAudit, Me.GridView13})
        '
        'gvSummDataAudit
        '
        Me.gvSummDataAudit.ActiveFilterEnabled = False
        Me.gvSummDataAudit.GridControl = Me.gcSummDataAudit
        Me.gvSummDataAudit.Name = "gvSummDataAudit"
        Me.gvSummDataAudit.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvSummDataAudit.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvSummDataAudit.OptionsBehavior.Editable = False
        Me.gvSummDataAudit.OptionsBehavior.ReadOnly = True
        Me.gvSummDataAudit.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvSummDataAudit.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvSummDataAudit.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvSummDataAudit.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvSummDataAudit.OptionsSelection.MultiSelect = True
        Me.gvSummDataAudit.OptionsView.ShowGroupPanel = False
        '
        'GridView13
        '
        Me.GridView13.GridControl = Me.gcSummDataAudit
        Me.GridView13.Name = "GridView13"
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Controls.Add(Me.gcInputDataAudit)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(826, 365)
        Me.XtraTabPage2.Text = "Input Data"
        '
        'gcInputDataAudit
        '
        Me.gcInputDataAudit.AllowDrop = True
        Me.gcInputDataAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcInputDataAudit.Location = New System.Drawing.Point(0, 0)
        Me.gcInputDataAudit.MainView = Me.gvInputDataAudit
        Me.gcInputDataAudit.Name = "gcInputDataAudit"
        Me.gcInputDataAudit.Size = New System.Drawing.Size(826, 365)
        Me.gcInputDataAudit.TabIndex = 3
        Me.gcInputDataAudit.Tag = "TM_Audit"
        Me.gcInputDataAudit.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvInputDataAudit, Me.GridView15})
        '
        'gvInputDataAudit
        '
        Me.gvInputDataAudit.ActiveFilterEnabled = False
        Me.gvInputDataAudit.GridControl = Me.gcInputDataAudit
        Me.gvInputDataAudit.Name = "gvInputDataAudit"
        Me.gvInputDataAudit.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvInputDataAudit.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvInputDataAudit.OptionsBehavior.Editable = False
        Me.gvInputDataAudit.OptionsBehavior.ReadOnly = True
        Me.gvInputDataAudit.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvInputDataAudit.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvInputDataAudit.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvInputDataAudit.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvInputDataAudit.OptionsSelection.MultiSelect = True
        Me.gvInputDataAudit.OptionsView.ShowGroupPanel = False
        '
        'GridView15
        '
        Me.GridView15.GridControl = Me.gcInputDataAudit
        Me.GridView15.Name = "GridView15"
        '
        'XtraTabPage3
        '
        Me.XtraTabPage3.Controls.Add(Me.gcValidationDataAudit)
        Me.XtraTabPage3.Name = "XtraTabPage3"
        Me.XtraTabPage3.Size = New System.Drawing.Size(826, 365)
        Me.XtraTabPage3.Text = "Validation Data"
        '
        'gcValidationDataAudit
        '
        Me.gcValidationDataAudit.AllowDrop = True
        Me.gcValidationDataAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcValidationDataAudit.Location = New System.Drawing.Point(0, 0)
        Me.gcValidationDataAudit.MainView = Me.gvValidationDataAudit
        Me.gcValidationDataAudit.Name = "gcValidationDataAudit"
        Me.gcValidationDataAudit.Size = New System.Drawing.Size(826, 365)
        Me.gcValidationDataAudit.TabIndex = 3
        Me.gcValidationDataAudit.Tag = "TM_Audit"
        Me.gcValidationDataAudit.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvValidationDataAudit, Me.GridView22})
        '
        'gvValidationDataAudit
        '
        Me.gvValidationDataAudit.ActiveFilterEnabled = False
        Me.gvValidationDataAudit.GridControl = Me.gcValidationDataAudit
        Me.gvValidationDataAudit.Name = "gvValidationDataAudit"
        Me.gvValidationDataAudit.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvValidationDataAudit.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvValidationDataAudit.OptionsBehavior.Editable = False
        Me.gvValidationDataAudit.OptionsBehavior.ReadOnly = True
        Me.gvValidationDataAudit.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvValidationDataAudit.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvValidationDataAudit.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvValidationDataAudit.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvValidationDataAudit.OptionsSelection.MultiSelect = True
        Me.gvValidationDataAudit.OptionsView.ShowGroupPanel = False
        '
        'GridView22
        '
        Me.GridView22.GridControl = Me.gcValidationDataAudit
        Me.GridView22.Name = "GridView22"
        '
        'XtraTabPage4
        '
        Me.XtraTabPage4.Controls.Add(Me.TableLayoutPanel18)
        Me.XtraTabPage4.Name = "XtraTabPage4"
        Me.XtraTabPage4.Size = New System.Drawing.Size(826, 365)
        Me.XtraTabPage4.Text = "Output Data"
        '
        'TableLayoutPanel18
        '
        Me.TableLayoutPanel18.ColumnCount = 1
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel18.Controls.Add(Me.gcOutputDataAudit, 0, 1)
        Me.TableLayoutPanel18.Controls.Add(Me.TableLayoutPanel19, 0, 0)
        Me.TableLayoutPanel18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel18.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel18.Name = "TableLayoutPanel18"
        Me.TableLayoutPanel18.RowCount = 2
        Me.TableLayoutPanel18.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel18.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel18.Size = New System.Drawing.Size(826, 365)
        Me.TableLayoutPanel18.TabIndex = 0
        '
        'gcOutputDataAudit
        '
        Me.gcOutputDataAudit.AllowDrop = True
        Me.gcOutputDataAudit.ContextMenuStrip = Me.cmsLaunchAdHocTiltMngr
        Me.gcOutputDataAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcOutputDataAudit.Location = New System.Drawing.Point(3, 38)
        Me.gcOutputDataAudit.MainView = Me.gvOutputDataAudit
        Me.gcOutputDataAudit.Name = "gcOutputDataAudit"
        Me.gcOutputDataAudit.Size = New System.Drawing.Size(820, 324)
        Me.gcOutputDataAudit.TabIndex = 4
        Me.gcOutputDataAudit.Tag = "TM_Audit"
        Me.gcOutputDataAudit.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvOutputDataAudit, Me.GridView24})
        '
        'gvOutputDataAudit
        '
        Me.gvOutputDataAudit.ActiveFilterEnabled = False
        Me.gvOutputDataAudit.GridControl = Me.gcOutputDataAudit
        Me.gvOutputDataAudit.Name = "gvOutputDataAudit"
        Me.gvOutputDataAudit.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvOutputDataAudit.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvOutputDataAudit.OptionsBehavior.Editable = False
        Me.gvOutputDataAudit.OptionsBehavior.ReadOnly = True
        Me.gvOutputDataAudit.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvOutputDataAudit.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvOutputDataAudit.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvOutputDataAudit.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.gvOutputDataAudit.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvOutputDataAudit.OptionsSelection.MultiSelect = True
        Me.gvOutputDataAudit.OptionsView.ShowGroupPanel = False
        '
        'GridView24
        '
        Me.GridView24.GridControl = Me.gcOutputDataAudit
        Me.GridView24.Name = "GridView24"
        '
        'TableLayoutPanel19
        '
        Me.TableLayoutPanel19.ColumnCount = 3
        Me.TableLayoutPanel19.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel19.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel19.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel19.Controls.Add(Me.SimpleButton6, 0, 0)
        Me.TableLayoutPanel19.Controls.Add(Me.btnDataAllCsvAudit, 1, 0)
        Me.TableLayoutPanel19.Controls.Add(Me.lblDataRowCountAudit, 2, 0)
        Me.TableLayoutPanel19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel19.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel19.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel19.Name = "TableLayoutPanel19"
        Me.TableLayoutPanel19.RowCount = 1
        Me.TableLayoutPanel19.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel19.Size = New System.Drawing.Size(822, 31)
        Me.TableLayoutPanel19.TabIndex = 0
        '
        'SimpleButton6
        '
        Me.SimpleButton6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SimpleButton6.Location = New System.Drawing.Point(3, 3)
        Me.SimpleButton6.Name = "SimpleButton6"
        Me.SimpleButton6.Size = New System.Drawing.Size(94, 25)
        Me.SimpleButton6.TabIndex = 0
        Me.SimpleButton6.Text = "Load To Grid"
        '
        'btnDataAllCsvAudit
        '
        Me.btnDataAllCsvAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDataAllCsvAudit.Location = New System.Drawing.Point(103, 3)
        Me.btnDataAllCsvAudit.Name = "btnDataAllCsvAudit"
        Me.btnDataAllCsvAudit.Size = New System.Drawing.Size(94, 25)
        Me.btnDataAllCsvAudit.TabIndex = 1
        Me.btnDataAllCsvAudit.Text = "All Data To CSV"
        '
        'lblDataRowCountAudit
        '
        Me.lblDataRowCountAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDataRowCountAudit.Location = New System.Drawing.Point(203, 3)
        Me.lblDataRowCountAudit.Name = "lblDataRowCountAudit"
        Me.lblDataRowCountAudit.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblDataRowCountAudit.Size = New System.Drawing.Size(616, 25)
        Me.lblDataRowCountAudit.TabIndex = 2
        Me.lblDataRowCountAudit.Text = "Count of Records: "
        Me.lblDataRowCountAudit.Visible = False
        '
        'TableLayoutPanel17
        '
        Me.TableLayoutPanel17.ColumnCount = 3
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220.0!))
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel17.Controls.Add(Me.LabelControl23, 0, 0)
        Me.TableLayoutPanel17.Controls.Add(Me.cmbResultSetIDAudit, 1, 0)
        Me.TableLayoutPanel17.Controls.Add(Me.btnDeleteResultSetAudit, 2, 0)
        Me.TableLayoutPanel17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel17.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel17.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel17.Name = "TableLayoutPanel17"
        Me.TableLayoutPanel17.RowCount = 1
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel17.Size = New System.Drawing.Size(830, 26)
        Me.TableLayoutPanel17.TabIndex = 3
        '
        'LabelControl23
        '
        Me.LabelControl23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl23.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl23.Name = "LabelControl23"
        Me.LabelControl23.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl23.Size = New System.Drawing.Size(74, 20)
        Me.LabelControl23.TabIndex = 3
        Me.LabelControl23.Text = "Result Set ID"
        '
        'cmbResultSetIDAudit
        '
        Me.cmbResultSetIDAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbResultSetIDAudit.EditValue = ""
        Me.cmbResultSetIDAudit.Location = New System.Drawing.Point(83, 3)
        Me.cmbResultSetIDAudit.Name = "cmbResultSetIDAudit"
        Me.cmbResultSetIDAudit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbResultSetIDAudit.Properties.Items.AddRange(New Object() {"DAILY", "WEEKLY"})
        Me.cmbResultSetIDAudit.Size = New System.Drawing.Size(214, 20)
        Me.cmbResultSetIDAudit.TabIndex = 13
        '
        'btnDeleteResultSetAudit
        '
        Me.btnDeleteResultSetAudit.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDeleteResultSetAudit.Location = New System.Drawing.Point(302, 2)
        Me.btnDeleteResultSetAudit.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDeleteResultSetAudit.Name = "btnDeleteResultSetAudit"
        Me.btnDeleteResultSetAudit.Size = New System.Drawing.Size(62, 22)
        Me.btnDeleteResultSetAudit.TabIndex = 14
        Me.btnDeleteResultSetAudit.Tag = "NB_Detect"
        Me.btnDeleteResultSetAudit.Text = "Delete"
        '
        'TableLayoutPanel20
        '
        Me.TableLayoutPanel20.ColumnCount = 1
        Me.TableLayoutPanel20.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel20.Controls.Add(Me.grpConfigSummAudit, 0, 0)
        Me.TableLayoutPanel20.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel20.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel20.Name = "TableLayoutPanel20"
        Me.TableLayoutPanel20.RowCount = 1
        Me.TableLayoutPanel20.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel20.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300.0!))
        Me.TableLayoutPanel20.Size = New System.Drawing.Size(1264, 300)
        Me.TableLayoutPanel20.TabIndex = 1
        '
        'grpConfigSummAudit
        '
        Me.grpConfigSummAudit.Controls.Add(Me.TableLayoutPanel22)
        Me.grpConfigSummAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpConfigSummAudit.Location = New System.Drawing.Point(3, 3)
        Me.grpConfigSummAudit.Name = "grpConfigSummAudit"
        Me.grpConfigSummAudit.Size = New System.Drawing.Size(1258, 294)
        Me.grpConfigSummAudit.TabIndex = 0
        Me.grpConfigSummAudit.Text = "Configuration Summary"
        '
        'TableLayoutPanel22
        '
        Me.TableLayoutPanel22.ColumnCount = 2
        Me.TableLayoutPanel22.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel22.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350.0!))
        Me.TableLayoutPanel22.Controls.Add(Me.TableLayoutPanel25, 1, 0)
        Me.TableLayoutPanel22.Controls.Add(Me.gcConfigSummAudit, 0, 0)
        Me.TableLayoutPanel22.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel22.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel22.Name = "TableLayoutPanel22"
        Me.TableLayoutPanel22.RowCount = 1
        Me.TableLayoutPanel22.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel22.Size = New System.Drawing.Size(1254, 269)
        Me.TableLayoutPanel22.TabIndex = 4
        '
        'TableLayoutPanel25
        '
        Me.TableLayoutPanel25.ColumnCount = 1
        Me.TableLayoutPanel25.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel25.Controls.Add(Me.grpLayerPropAudit, 0, 0)
        Me.TableLayoutPanel25.Controls.Add(Me.btnListMngrAudit, 0, 1)
        Me.TableLayoutPanel25.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel25.Location = New System.Drawing.Point(907, 3)
        Me.TableLayoutPanel25.Name = "TableLayoutPanel25"
        Me.TableLayoutPanel25.RowCount = 2
        Me.TableLayoutPanel25.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel25.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel25.Size = New System.Drawing.Size(344, 263)
        Me.TableLayoutPanel25.TabIndex = 0
        '
        'grpLayerPropAudit
        '
        Me.grpLayerPropAudit.Controls.Add(Me.TableLayoutPanel26)
        Me.grpLayerPropAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpLayerPropAudit.Location = New System.Drawing.Point(3, 3)
        Me.grpLayerPropAudit.Name = "grpLayerPropAudit"
        Me.grpLayerPropAudit.Size = New System.Drawing.Size(338, 225)
        Me.grpLayerPropAudit.TabIndex = 1
        Me.grpLayerPropAudit.Text = "Layer Properties"
        '
        'TableLayoutPanel26
        '
        Me.TableLayoutPanel26.ColumnCount = 1
        Me.TableLayoutPanel26.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel26.Controls.Add(Me.layerPropGridAudit, 0, 0)
        Me.TableLayoutPanel26.Controls.Add(Me.TableLayoutPanel43, 0, 1)
        Me.TableLayoutPanel26.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel26.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel26.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel26.Name = "TableLayoutPanel26"
        Me.TableLayoutPanel26.RowCount = 2
        Me.TableLayoutPanel26.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel26.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel26.Size = New System.Drawing.Size(334, 200)
        Me.TableLayoutPanel26.TabIndex = 1
        '
        'layerPropGridAudit
        '
        Me.layerPropGridAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layerPropGridAudit.LineColor = System.Drawing.SystemColors.ControlDark
        Me.layerPropGridAudit.Location = New System.Drawing.Point(3, 3)
        Me.layerPropGridAudit.Name = "layerPropGridAudit"
        Me.layerPropGridAudit.Size = New System.Drawing.Size(328, 167)
        Me.layerPropGridAudit.TabIndex = 0
        Me.layerPropGridAudit.Tag = "TM_Audit"
        Me.layerPropGridAudit.ToolbarVisible = False
        '
        'TableLayoutPanel43
        '
        Me.TableLayoutPanel43.ColumnCount = 2
        Me.TableLayoutPanel43.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel43.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel43.Controls.Add(Me.btnLayerPropertiesAddAudit, 1, 0)
        Me.TableLayoutPanel43.Controls.Add(Me.ceApplyConfigAllAudit, 0, 0)
        Me.TableLayoutPanel43.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel43.Location = New System.Drawing.Point(1, 174)
        Me.TableLayoutPanel43.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel43.Name = "TableLayoutPanel43"
        Me.TableLayoutPanel43.RowCount = 1
        Me.TableLayoutPanel43.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel43.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel43.Size = New System.Drawing.Size(332, 25)
        Me.TableLayoutPanel43.TabIndex = 1
        '
        'btnLayerPropertiesAddAudit
        '
        Me.btnLayerPropertiesAddAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnLayerPropertiesAddAudit.Location = New System.Drawing.Point(279, 2)
        Me.btnLayerPropertiesAddAudit.Margin = New System.Windows.Forms.Padding(2)
        Me.btnLayerPropertiesAddAudit.Name = "btnLayerPropertiesAddAudit"
        Me.btnLayerPropertiesAddAudit.Size = New System.Drawing.Size(51, 21)
        Me.btnLayerPropertiesAddAudit.TabIndex = 10
        Me.btnLayerPropertiesAddAudit.Tag = "TM_Bulk"
        Me.btnLayerPropertiesAddAudit.Text = "Add"
        '
        'ceApplyConfigAllAudit
        '
        Me.ceApplyConfigAllAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceApplyConfigAllAudit.Location = New System.Drawing.Point(5, 3)
        Me.ceApplyConfigAllAudit.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceApplyConfigAllAudit.Name = "ceApplyConfigAllAudit"
        Me.ceApplyConfigAllAudit.Properties.Caption = "Apply changes to all configuration"
        Me.ceApplyConfigAllAudit.Size = New System.Drawing.Size(269, 19)
        Me.ceApplyConfigAllAudit.TabIndex = 1
        '
        'btnListMngrAudit
        '
        Me.btnListMngrAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnListMngrAudit.Location = New System.Drawing.Point(3, 234)
        Me.btnListMngrAudit.Name = "btnListMngrAudit"
        Me.btnListMngrAudit.Size = New System.Drawing.Size(338, 26)
        Me.btnListMngrAudit.TabIndex = 1
        Me.btnListMngrAudit.Text = "List Manager"
        '
        'gcConfigSummAudit
        '
        Me.gcConfigSummAudit.AllowDrop = True
        Me.gcConfigSummAudit.ContextMenuStrip = Me.cmsConfigSummary
        Me.gcConfigSummAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcConfigSummAudit.Location = New System.Drawing.Point(3, 3)
        Me.gcConfigSummAudit.MainView = Me.gvConfigSummAudit
        Me.gcConfigSummAudit.Name = "gcConfigSummAudit"
        Me.gcConfigSummAudit.Size = New System.Drawing.Size(898, 263)
        Me.gcConfigSummAudit.TabIndex = 3
        Me.gcConfigSummAudit.Tag = "TM_Audit"
        Me.gcConfigSummAudit.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvConfigSummAudit, Me.GridView18})
        '
        'gvConfigSummAudit
        '
        Me.gvConfigSummAudit.ActiveFilterEnabled = False
        Me.gvConfigSummAudit.GridControl = Me.gcConfigSummAudit
        Me.gvConfigSummAudit.Name = "gvConfigSummAudit"
        Me.gvConfigSummAudit.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvConfigSummAudit.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvConfigSummAudit.OptionsBehavior.Editable = False
        Me.gvConfigSummAudit.OptionsBehavior.ReadOnly = True
        Me.gvConfigSummAudit.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvConfigSummAudit.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvConfigSummAudit.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvConfigSummAudit.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvConfigSummAudit.OptionsSelection.MultiSelect = True
        Me.gvConfigSummAudit.OptionsView.ShowGroupPanel = False
        '
        'GridView18
        '
        Me.GridView18.GridControl = Me.gcConfigSummAudit
        Me.GridView18.Name = "GridView18"
        '
        'tpTmMML
        '
        Me.tpTmMML.Controls.Add(Me.sccMML)
        Me.tpTmMML.Name = "tpTmMML"
        Me.tpTmMML.Size = New System.Drawing.Size(1264, 770)
        Me.tpTmMML.Text = "MML/XML"
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
        Me.sccMML.Size = New System.Drawing.Size(1264, 770)
        Me.sccMML.SplitterPosition = 460
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
        Me.sccMmlTop.Size = New System.Drawing.Size(1264, 460)
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
        Me.grpMmlInput.Size = New System.Drawing.Size(396, 460)
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
        Me.TableLayoutPanel24.Size = New System.Drawing.Size(392, 435)
        Me.TableLayoutPanel24.TabIndex = 1
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.TableLayoutPanel27)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(2, 284)
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
        Me.TableLayoutPanel13.Location = New System.Drawing.Point(3, 371)
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
        Me.cmbMMLConfig.Enabled = False
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
        Me.LabelControl10.Text = "MML Config"
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
        Me.gcMmlCampaign.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcMmlCampaign.Location = New System.Drawing.Point(2, 29)
        Me.gcMmlCampaign.MainView = Me.gvMmlCampaign
        Me.gcMmlCampaign.Margin = New System.Windows.Forms.Padding(2)
        Me.gcMmlCampaign.Name = "gcMmlCampaign"
        Me.gcMmlCampaign.Size = New System.Drawing.Size(388, 251)
        Me.gcMmlCampaign.TabIndex = 5
        Me.gcMmlCampaign.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvMmlCampaign, Me.GridView5})
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
        Me.xtcMmlTop.Size = New System.Drawing.Size(858, 460)
        Me.xtcMmlTop.TabIndex = 0
        Me.xtcMmlTop.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tpValidation, Me.tpData, Me.tpExcluded})
        '
        'tpValidation
        '
        Me.tpValidation.Controls.Add(Me.TableLayoutPanel14)
        Me.tpValidation.Name = "tpValidation"
        Me.tpValidation.Size = New System.Drawing.Size(856, 435)
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
        Me.TableLayoutPanel14.Size = New System.Drawing.Size(856, 435)
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
        Me.gcValidation.Size = New System.Drawing.Size(452, 431)
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
        Me.TableLayoutPanel15.Location = New System.Drawing.Point(457, 1)
        Me.TableLayoutPanel15.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel15.Name = "TableLayoutPanel15"
        Me.TableLayoutPanel15.RowCount = 3
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 119.0!))
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel15.Size = New System.Drawing.Size(398, 433)
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
        Me.tvSelectionMml.Size = New System.Drawing.Size(392, 283)
        Me.tvSelectionMml.TabIndex = 8
        '
        'grpMmlOutput
        '
        Me.grpMmlOutput.Controls.Add(Me.TableLayoutPanel23)
        Me.grpMmlOutput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpMmlOutput.Location = New System.Drawing.Point(3, 317)
        Me.grpMmlOutput.Name = "grpMmlOutput"
        Me.grpMmlOutput.Size = New System.Drawing.Size(392, 113)
        Me.grpMmlOutput.TabIndex = 9
        Me.grpMmlOutput.Text = "Output"
        '
        'TableLayoutPanel23
        '
        Me.TableLayoutPanel23.ColumnCount = 1
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel23.Controls.Add(Me.TableLayoutPanel57, 0, 0)
        Me.TableLayoutPanel23.Controls.Add(Me.TableLayoutPanel30, 0, 1)
        Me.TableLayoutPanel23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel23.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel23.Name = "TableLayoutPanel23"
        Me.TableLayoutPanel23.RowCount = 3
        Me.TableLayoutPanel23.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52.0!))
        Me.TableLayoutPanel23.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel23.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel23.Size = New System.Drawing.Size(388, 88)
        Me.TableLayoutPanel23.TabIndex = 0
        '
        'TableLayoutPanel57
        '
        Me.TableLayoutPanel57.ColumnCount = 3
        Me.TableLayoutPanel57.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel57.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel57.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
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
        Me.cmbOutputLocation.Size = New System.Drawing.Size(182, 20)
        Me.cmbOutputLocation.TabIndex = 9
        '
        'txtFileNameSuffix
        '
        Me.txtFileNameSuffix.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtFileNameSuffix.Location = New System.Drawing.Point(191, 28)
        Me.txtFileNameSuffix.Name = "txtFileNameSuffix"
        Me.txtFileNameSuffix.Size = New System.Drawing.Size(114, 20)
        Me.txtFileNameSuffix.TabIndex = 10
        '
        'LabelControl12
        '
        Me.LabelControl12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl12.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl12.Name = "LabelControl12"
        Me.LabelControl12.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl12.Size = New System.Drawing.Size(182, 19)
        Me.LabelControl12.TabIndex = 8
        Me.LabelControl12.Text = "Select Output Method"
        '
        'LabelControl50
        '
        Me.LabelControl50.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl50.Location = New System.Drawing.Point(191, 3)
        Me.LabelControl50.Name = "LabelControl50"
        Me.LabelControl50.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl50.Size = New System.Drawing.Size(114, 19)
        Me.LabelControl50.TabIndex = 11
        Me.LabelControl50.Text = "Add Filename Suffix"
        '
        'LabelControl51
        '
        Me.LabelControl51.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl51.Location = New System.Drawing.Point(311, 3)
        Me.LabelControl51.Name = "LabelControl51"
        Me.LabelControl51.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl51.Size = New System.Drawing.Size(74, 19)
        Me.LabelControl51.TabIndex = 12
        Me.LabelControl51.Text = "File Size (MB)"
        '
        'seFileSize
        '
        Me.seFileSize.Dock = System.Windows.Forms.DockStyle.Fill
        Me.seFileSize.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.seFileSize.Location = New System.Drawing.Point(311, 28)
        Me.seFileSize.Name = "seFileSize"
        Me.seFileSize.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.seFileSize.Properties.IsFloatValue = False
        Me.seFileSize.Properties.MaskSettings.Set("mask", "N00")
        Me.seFileSize.Properties.MaxValue = New Decimal(New Integer() {1024, 0, 0, 0})
        Me.seFileSize.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.seFileSize.Size = New System.Drawing.Size(74, 20)
        Me.seFileSize.TabIndex = 13
        '
        'TableLayoutPanel30
        '
        Me.TableLayoutPanel30.ColumnCount = 2
        Me.TableLayoutPanel30.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.TableLayoutPanel30.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.0!))
        Me.TableLayoutPanel30.Controls.Add(Me.btnMMLRollback, 0, 0)
        Me.TableLayoutPanel30.Controls.Add(Me.btnMML, 0, 0)
        Me.TableLayoutPanel30.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel30.Location = New System.Drawing.Point(1, 53)
        Me.TableLayoutPanel30.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel30.Name = "TableLayoutPanel30"
        Me.TableLayoutPanel30.RowCount = 1
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel30.Size = New System.Drawing.Size(386, 30)
        Me.TableLayoutPanel30.TabIndex = 11
        '
        'btnMMLRollback
        '
        Me.btnMMLRollback.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnMMLRollback.Location = New System.Drawing.Point(193, 3)
        Me.btnMMLRollback.Name = "btnMMLRollback"
        Me.btnMMLRollback.Size = New System.Drawing.Size(190, 24)
        Me.btnMMLRollback.TabIndex = 2
        Me.btnMMLRollback.Text = "Get MML/XML Rollback"
        '
        'btnMML
        '
        Me.btnMML.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnMML.Location = New System.Drawing.Point(3, 3)
        Me.btnMML.Name = "btnMML"
        Me.btnMML.Size = New System.Drawing.Size(184, 24)
        Me.btnMML.TabIndex = 0
        Me.btnMML.Text = "Get MML/XML"
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
        Me.tpData.Size = New System.Drawing.Size(856, 435)
        Me.tpData.Text = "Data"
        '
        'gcData
        '
        Me.gcData.AllowDrop = True
        Me.gcData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcData.Location = New System.Drawing.Point(0, 0)
        Me.gcData.MainView = Me.gvData
        Me.gcData.Margin = New System.Windows.Forms.Padding(2)
        Me.gcData.Name = "gcData"
        Me.gcData.Size = New System.Drawing.Size(856, 435)
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
        Me.tpExcluded.Size = New System.Drawing.Size(856, 435)
        Me.tpExcluded.Text = "Excluded"
        '
        'gcExcluded
        '
        Me.gcExcluded.AllowDrop = True
        Me.gcExcluded.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcExcluded.Location = New System.Drawing.Point(0, 0)
        Me.gcExcluded.MainView = Me.gvExcluded
        Me.gcExcluded.Margin = New System.Windows.Forms.Padding(2)
        Me.gcExcluded.Name = "gcExcluded"
        Me.gcExcluded.Size = New System.Drawing.Size(856, 435)
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
        Me.grpMmlConfig.Enabled = False
        Me.grpMmlConfig.Location = New System.Drawing.Point(0, 0)
        Me.grpMmlConfig.Name = "grpMmlConfig"
        Me.grpMmlConfig.Size = New System.Drawing.Size(1264, 300)
        Me.grpMmlConfig.TabIndex = 1
        Me.grpMmlConfig.Text = "MML Configuration"
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
        Me.sccMmlBottom.Size = New System.Drawing.Size(1260, 275)
        Me.sccMmlBottom.SplitterPosition = 364
        Me.sccMmlBottom.TabIndex = 0
        Me.sccMmlBottom.Text = "SplitContainerControl1"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.TableLayoutPanel10)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Enabled = False
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(364, 275)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "MML Configurations"
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
        Me.xtcMmlBottom.Enabled = False
        Me.xtcMmlBottom.Location = New System.Drawing.Point(0, 0)
        Me.xtcMmlBottom.Name = "xtcMmlBottom"
        Me.xtcMmlBottom.SelectedTabPage = Me.tpScripts
        Me.xtcMmlBottom.Size = New System.Drawing.Size(886, 275)
        Me.xtcMmlBottom.TabIndex = 0
        Me.xtcMmlBottom.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tpScripts})
        '
        'tpScripts
        '
        Me.tpScripts.Controls.Add(Me.gcScripts)
        Me.tpScripts.Name = "tpScripts"
        Me.tpScripts.Size = New System.Drawing.Size(884, 250)
        Me.tpScripts.Text = "SCRIPTS"
        '
        'gcScripts
        '
        Me.gcScripts.AllowDrop = True
        Me.gcScripts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcScripts.Location = New System.Drawing.Point(0, 0)
        Me.gcScripts.MainView = Me.gvScripts
        Me.gcScripts.Margin = New System.Windows.Forms.Padding(2)
        Me.gcScripts.Name = "gcScripts"
        Me.gcScripts.Size = New System.Drawing.Size(884, 250)
        Me.gcScripts.TabIndex = 6
        Me.gcScripts.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvScripts, Me.GridView7})
        '
        'gvScripts
        '
        Me.gvScripts.ActiveFilterEnabled = False
        Me.gvScripts.GridControl = Me.gcScripts
        Me.gvScripts.Name = "gvScripts"
        Me.gvScripts.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvScripts.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvScripts.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvScripts.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvScripts.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvScripts.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvScripts.OptionsSelection.MultiSelect = True
        Me.gvScripts.OptionsView.ShowGroupPanel = False
        '
        'GridView7
        '
        Me.GridView7.GridControl = Me.gcScripts
        Me.GridView7.Name = "GridView7"
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
        Me.lblIntegrityMsg.Size = New System.Drawing.Size(1266, 24)
        Me.lblIntegrityMsg.TabIndex = 1
        Me.lblIntegrityMsg.Text = "Warning - Check Data Integrity"
        '
        'Timer1
        '
        Me.Timer1.Interval = 5000
        '
        'cmsSectorList
        '
        Me.cmsSectorList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_DeleteSector})
        Me.cmsSectorList.Name = "cm_SON_Incon_dgvResult"
        Me.cmsSectorList.Size = New System.Drawing.Size(144, 26)
        '
        'tsmi_DeleteSector
        '
        Me.tsmi_DeleteSector.Name = "tsmi_DeleteSector"
        Me.tsmi_DeleteSector.Size = New System.Drawing.Size(143, 22)
        Me.tsmi_DeleteSector.Text = "Delete Sector"
        '
        'frmTiltManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1272, 831)
        Me.Controls.Add(Me.tlpMain)
        Me.IconOptions.Icon = CType(resources.GetObject("frmTiltManagement.IconOptions.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(1000, 700)
        Me.Name = "frmTiltManagement"
        Me.Text = "Tilt Management"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        CType(Me.xtcMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtcMain.ResumeLayout(False)
        Me.tpTiltMngrAdHoc.ResumeLayout(False)
        CType(Me.sccMain.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMain.Panel1.ResumeLayout(False)
        CType(Me.sccMain.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMain.Panel2.ResumeLayout(False)
        CType(Me.sccMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMain.ResumeLayout(False)
        Me.TableLayoutPanel32.ResumeLayout(False)
        CType(Me.ch_TiltManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel36.ResumeLayout(False)
        CType(Me.gcSectorList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSectorList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel39.ResumeLayout(False)
        Me.TableLayoutPanel40.ResumeLayout(False)
        CType(Me.cmbManualCampaign.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel41.ResumeLayout(False)
        Me.TableLayoutPanel41.PerformLayout()
        CType(Me.cmbResolution.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtETiltValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel42.ResumeLayout(False)
        Me.TableLayoutPanel42.PerformLayout()
        CType(Me.tbcETiltSlider.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbcETiltSlider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sccTiltTreeValidGrid.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccTiltTreeValidGrid.Panel1.ResumeLayout(False)
        CType(Me.sccTiltTreeValidGrid.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccTiltTreeValidGrid.Panel2.ResumeLayout(False)
        CType(Me.sccTiltTreeValidGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccTiltTreeValidGrid.ResumeLayout(False)
        CType(Me.tlTiltManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcCampaignValidation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCampaignValidation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpTmBulk.ResumeLayout(False)
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
        CType(Me.grpCampBulk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCampBulk.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.grpCampPropDetect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCampPropDetect.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        CType(Me.ceActiveBulk.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceIsPublicBulk.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcCampaignsBulk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsCampaignsBulk.ResumeLayout(False)
        CType(Me.gvCampaignsBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.txtSearchBulk.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel7.ResumeLayout(False)
        CType(Me.grpCampResultBulk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCampResultBulk.ResumeLayout(False)
        Me.TableLayoutPanel28.ResumeLayout(False)
        Me.TableLayoutPanel33.ResumeLayout(False)
        Me.TableLayoutPanel33.PerformLayout()
        CType(Me.cmbResultSetIDBulk.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcTMBulk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtcTMBulk.ResumeLayout(False)
        Me.tpCampBulkSumm.ResumeLayout(False)
        CType(Me.gcSummDataBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSummDataBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpCampImportedData.ResumeLayout(False)
        CType(Me.gcImportedDataBulk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmBulkPaste.ResumeLayout(False)
        CType(Me.gvImportedDataBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView19, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpValidationData.ResumeLayout(False)
        CType(Me.gcValidationData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvValidationData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView20, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpCampBulkOutputData.ResumeLayout(False)
        Me.TableLayoutPanel34.ResumeLayout(False)
        CType(Me.gcOutputDataBulk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsLaunchAdHocTiltMngr.ResumeLayout(False)
        CType(Me.gvOutputDataBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView21, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel35.ResumeLayout(False)
        Me.TableLayoutPanel35.PerformLayout()
        CType(Me.grpImportBulk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpImportBulk.ResumeLayout(False)
        Me.TableLayoutPanel29.ResumeLayout(False)
        Me.TableLayoutPanel29.PerformLayout()
        CType(Me.txtImportfileName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel21.ResumeLayout(False)
        CType(Me.grpConfigSummBulk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpConfigSummBulk.ResumeLayout(False)
        Me.TableLayoutPanel37.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        CType(Me.grpLayerPropBulk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpLayerPropBulk.ResumeLayout(False)
        Me.TableLayoutPanel59.ResumeLayout(False)
        Me.TableLayoutPanel31.ResumeLayout(False)
        CType(Me.ceApplyConfigAllBulk.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcConfigSummBulk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsConfigSummary.ResumeLayout(False)
        CType(Me.gvConfigSummBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpTmAudit.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.SplitContainerControl2.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl2.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.ResumeLayout(False)
        CType(Me.grpCampAudit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCampAudit.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl5.ResumeLayout(False)
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel6.PerformLayout()
        CType(Me.ceActiveAudit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceIsPublicAudit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcCampaignsAudit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsCampaignsAudit.ResumeLayout(False)
        CType(Me.gvCampaignsAudit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel8.ResumeLayout(False)
        CType(Me.txtSearchAudit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel9.ResumeLayout(False)
        CType(Me.grpCampResultAudit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCampResultAudit.ResumeLayout(False)
        Me.TableLayoutPanel16.ResumeLayout(False)
        CType(Me.xtcTMAudit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtcTMAudit.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        CType(Me.gcSummDataAudit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSummDataAudit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView13, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage2.ResumeLayout(False)
        CType(Me.gcInputDataAudit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvInputDataAudit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView15, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage3.ResumeLayout(False)
        CType(Me.gcValidationDataAudit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvValidationDataAudit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView22, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage4.ResumeLayout(False)
        Me.TableLayoutPanel18.ResumeLayout(False)
        CType(Me.gcOutputDataAudit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvOutputDataAudit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView24, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel19.ResumeLayout(False)
        Me.TableLayoutPanel19.PerformLayout()
        Me.TableLayoutPanel17.ResumeLayout(False)
        Me.TableLayoutPanel17.PerformLayout()
        CType(Me.cmbResultSetIDAudit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel20.ResumeLayout(False)
        CType(Me.grpConfigSummAudit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpConfigSummAudit.ResumeLayout(False)
        Me.TableLayoutPanel22.ResumeLayout(False)
        Me.TableLayoutPanel25.ResumeLayout(False)
        CType(Me.grpLayerPropAudit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpLayerPropAudit.ResumeLayout(False)
        Me.TableLayoutPanel26.ResumeLayout(False)
        Me.TableLayoutPanel43.ResumeLayout(False)
        CType(Me.ceApplyConfigAllAudit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcConfigSummAudit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvConfigSummAudit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView18, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpTmMML.ResumeLayout(False)
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
        Me.TableLayoutPanel30.ResumeLayout(False)
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
        Me.tpScripts.ResumeLayout(False)
        CType(Me.gcScripts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvScripts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsSectorList.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents xtcMain As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tpTmBulk As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents sccDetectCamp As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents sccLeft As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents grpCampBulk As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents grpCampPropDetect As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblLastRunTimeBulk As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblLastEndTimeBulk As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblOwnerBulk As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ceActiveBulk As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents btnRunNowBulk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl30 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ceIsPublicBulk As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents gcCampaignsBulk As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvCampaignsBulk As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents btnRefreshBulk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtSearchBulk As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents btnDeleteBulk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCloneBulk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel7 As TableLayoutPanel
    Friend WithEvents grpCampResultBulk As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel28 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel33 As TableLayoutPanel
    Friend WithEvents LabelControl13 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbResultSetIDBulk As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnDeleteResultSetBulk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents xtcTMBulk As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tpCampBulkSumm As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gcSummDataBulk As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvSummDataBulk As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tpCampBulkOutputData As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TableLayoutPanel34 As TableLayoutPanel
    Friend WithEvents gcOutputDataBulk As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvOutputDataBulk As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView21 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel35 As TableLayoutPanel
    Friend WithEvents btnDetectDataLoadGrid As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDataAllCsvBulk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblDataRowCountBulk As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel21 As TableLayoutPanel
    Friend WithEvents grpConfigSummBulk As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel37 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents grpLayerPropBulk As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel59 As TableLayoutPanel
    Friend WithEvents layerPropGridBulk As PropertyGrid
    Friend WithEvents ceApplyConfigAllBulk As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents btnListMngrBulk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gcConfigSummBulk As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvConfigSummBulk As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tpTmAudit As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tpTmMML As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents sccMML As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents sccMmlTop As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents grpMmlInput As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel24 As TableLayoutPanel
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel27 As TableLayoutPanel
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblOwnerMmlInput As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblLastEndTimeMml As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel13 As TableLayoutPanel
    Friend WithEvents cmbMMLConfig As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel58 As TableLayoutPanel
    Friend WithEvents btnPreFilter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnValidate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gcMmlCampaign As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvMmlCampaign As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel38 As TableLayoutPanel
    Friend WithEvents txtSearchMml As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents btnRefreshMml As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDeleteMml As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents xtcMmlTop As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tpValidation As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TableLayoutPanel14 As TableLayoutPanel
    Friend WithEvents gcValidation As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvValidation As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView8 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel15 As TableLayoutPanel
    Friend WithEvents tvSelectionMml As TreeView
    Friend WithEvents grpMmlOutput As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel23 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel57 As TableLayoutPanel
    Friend WithEvents cmbOutputLocation As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtFileNameSuffix As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl50 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl51 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents seFileSize As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtSearchMMLObject As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents tpData As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gcData As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvData As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView10 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tpExcluded As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gcExcluded As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvExcluded As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView11 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grpMmlConfig As DevExpress.XtraEditors.GroupControl
    Friend WithEvents sccMmlBottom As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel10 As TableLayoutPanel
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel11 As TableLayoutPanel
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblOwnerMmlConfig As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl44 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ceIsPublicMML As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents gcMmlConfig As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvMmlConfig As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel12 As TableLayoutPanel
    Friend WithEvents txtMmlConfigSearch As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents btnMmlConfigClone As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnMmlConfigDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents xtcMmlBottom As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tpScripts As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gcScripts As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvScripts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView7 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblIntegrityMsg As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents SplitContainerControl2 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents grpCampAudit As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents GroupControl5 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl17 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl18 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblLastRunTimeAudit As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblLastEndTimeAudit As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblOwnerAudit As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ceActiveAudit As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents btnRunNowAudit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl22 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ceIsPublicAudit As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents gcCampaignsAudit As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvCampaignsAudit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView9 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel8 As TableLayoutPanel
    Friend WithEvents btnRefreshAudit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtSearchAudit As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents btnDeleteAudit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCloneAudit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel9 As TableLayoutPanel
    Friend WithEvents grpCampResultAudit As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel16 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel17 As TableLayoutPanel
    Friend WithEvents LabelControl23 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbResultSetIDAudit As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnDeleteResultSetAudit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel20 As TableLayoutPanel
    Friend WithEvents grpConfigSummAudit As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel22 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel25 As TableLayoutPanel
    Friend WithEvents grpLayerPropAudit As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel26 As TableLayoutPanel
    Friend WithEvents layerPropGridAudit As PropertyGrid
    Friend WithEvents ceApplyConfigAllAudit As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents btnListMngrAudit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gcConfigSummAudit As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvConfigSummAudit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView18 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grpImportBulk As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel29 As TableLayoutPanel
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnImportBulk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblStatus As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel30 As TableLayoutPanel
    Friend WithEvents btnMML As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnOpenFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtImportfileName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnAddBulk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel31 As TableLayoutPanel
    Friend WithEvents btnLayerPropertiesAddBulk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tpCampImportedData As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gcImportedDataBulk As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvImportedDataBulk As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView19 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Timer1 As Timer
    Friend WithEvents tpValidationData As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gcValidationData As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvValidationData As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView20 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmsCampaignsBulk As ContextMenuStrip
    Friend WithEvents tsmi_RenameCampaignBulk As ToolStripMenuItem
    Friend WithEvents xtcTMAudit As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gcSummDataAudit As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvSummDataAudit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView13 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gcInputDataAudit As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvInputDataAudit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView15 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents XtraTabPage3 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gcValidationDataAudit As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvValidationDataAudit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView22 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents XtraTabPage4 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TableLayoutPanel18 As TableLayoutPanel
    Friend WithEvents gcOutputDataAudit As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvOutputDataAudit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView24 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel19 As TableLayoutPanel
    Friend WithEvents SimpleButton6 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDataAllCsvAudit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblDataRowCountAudit As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmsCampaignsAudit As ContextMenuStrip
    Friend WithEvents tsmi_RenameCampaignAudit As ToolStripMenuItem
    Friend WithEvents cmBulkPaste As ContextMenuStrip
    Friend WithEvents tsmi_PasteDataFromClipboard As ToolStripMenuItem
    Friend WithEvents cmsConfigSummary As ContextMenuStrip
    Friend WithEvents tsmi_DeleteSelectedRows As ToolStripMenuItem
    Friend WithEvents btnMMLRollback As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmsLaunchAdHocTiltMngr As ContextMenuStrip
    Friend WithEvents tsmi_AdHocTiltManager As ToolStripMenuItem
    Friend WithEvents tsmi_AddNewTiltCampaign As ToolStripMenuItem
    Friend WithEvents tpTiltMngrAdHoc As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents sccMain As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents TableLayoutPanel32 As TableLayoutPanel
    Friend WithEvents ch_TiltManager As dotnetCHARTING.WinForms.Chart
    Friend WithEvents TableLayoutPanel36 As TableLayoutPanel
    Public WithEvents gcSectorList As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvSectorList As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel39 As TableLayoutPanel
    Friend WithEvents btnGenerateTiltCampaign As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCalculateAndSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tglPlanned As Library.IOSToggleButton
    Friend WithEvents btnClearThematics As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel40 As TableLayoutPanel
    Public WithEvents cmbManualCampaign As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnAddCampaign As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDeleteCampaign As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel41 As TableLayoutPanel
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbResolution As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnManageTree As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtETiltValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TableLayoutPanel42 As TableLayoutPanel
    Friend WithEvents tbcETiltSlider As DevExpress.XtraEditors.TrackBarControl
    Friend WithEvents lbl_EtiltPlanned As DevExpress.XtraEditors.LabelControl
    Friend WithEvents sccTiltTreeValidGrid As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents tlTiltManager As DevExpress.XtraTreeList.TreeList
    Friend WithEvents Antennas As DevExpress.XtraTreeList.Columns.TreeListBand
    Friend WithEvents TreeListColumn1 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn2 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn3 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents treeListBand1 As DevExpress.XtraTreeList.Columns.TreeListBand
    Friend WithEvents TreeListColumn4 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn5 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn6 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents treeListBand2 As DevExpress.XtraTreeList.Columns.TreeListBand
    Friend WithEvents TreeListColumn7 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn8 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn10 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents treeListBand3 As DevExpress.XtraTreeList.Columns.TreeListBand
    Friend WithEvents tlcValidation As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents RepositoryItemPictureEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents TreeListColumn11 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn12 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn13 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn14 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn15 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn9 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn16 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn17 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn18 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn19 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn20 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn21 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents RepositoryItemImageEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemImageEdit
    Public WithEvents gcCampaignValidation As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvCampaignValidation As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmsSectorList As ContextMenuStrip
    Friend WithEvents tsmi_DeleteSector As ToolStripMenuItem
    Friend WithEvents ToolTipController1 As DevExpress.Utils.ToolTipController
    Friend WithEvents TableLayoutPanel43 As TableLayoutPanel
    Friend WithEvents btnLayerPropertiesAddAudit As DevExpress.XtraEditors.SimpleButton
End Class
