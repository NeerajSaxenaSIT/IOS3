Imports IOS.Library
Imports IOS.DataLibrary

Public Class frmCopyFromTemplate

    Dim parray()() As String = Nothing
    Dim ConnStringAndSqlParam() As String = Nothing
    Public vendorName As String = Nothing

#Region "Methods"

    Private Sub LoadTemplateList()
        RemoveHandler cmbTemplate.SelectedIndexChanged, AddressOf cmbTemplate_SelectedIndexChanged
        parray = {
            New String() {"@TemplateVendor", Chr(39) & Me.vendorName & Chr(39)}
        }
        ConnStringAndSqlParam = Nothing
        ConnStringAndSqlParam = GetSQL(4192, parray)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(ConnStringAndSqlParam(0), ConnStringAndSqlParam(1))
        BindDevExComboBoxWithValueMember(cmbTemplate, dt, "TemplateID", "TemplateName", "Select Template")
        AddHandler cmbTemplate.SelectedIndexChanged, AddressOf cmbTemplate_SelectedIndexChanged
    End Sub

    Private Sub LoadMoListForTemplete()
        parray = Nothing
        ConnStringAndSqlParam = Nothing
        parray = {
            New String() {"@TemplateID", CInt(TryCast(cmbTemplate.SelectedItem, clsComboBoxItem).Value)}
        }
        ConnStringAndSqlParam = GetSQL(4110, parray)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(ConnStringAndSqlParam(0), ConnStringAndSqlParam(1))
        BindDevExComboBoxWithValueMember(cmbMOForTemplate, dt, "TemplateMOConfigID", "MOName", "Select MO")
    End Sub

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        lblMessage.Text = ""
        lblMessage.Visible = False
        RemoveHandler Timer1.Tick, AddressOf Timer1_Tick
        Timer1.Enabled = False
        Timer1.Stop()
        Me.Cursor = Cursors.Default
        Application.DoEvents()
    End Sub

#End Region

#Region "Events"

    Private Sub frmCopyFromTemplate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            LoadTemplateList()
            RefCheckCopyFromCommitted = False

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cmbTemplate_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If cmbTemplate.SelectedIndex <> 0 Then
                LoadMoListForTemplete()
            Else
                ClearComboBox(cmbMOForTemplate, "Select MO")
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnCommit_Click(sender As Object, e As EventArgs) Handles btnCommit.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If cmbTemplate.SelectedIndex = 0 Then
                SetMessage("Please Select Template Name")
                Exit Sub
            ElseIf cmbMOForTemplate.SelectedIndex = 0 Then
                SetMessage("Please Select MO Name")
                Exit Sub
            ElseIf (ceCopyFilterStrings.Checked = False) AndAlso (ceCopyInclusionList.Checked = False) AndAlso (ceCopyExclusionList.Checked = False) AndAlso (ceCopyExclusionList.Checked = False) Then
                SetMessage("Please select at least one check box")
                Exit Sub
            End If

            If Not objGenerateTemplate Is Nothing Then
                objGenerateTemplate.copyFromTemplateID = TryCast(cmbTemplate.SelectedItem, clsComboBoxItem).Value
                objGenerateTemplate.copyFromTemplateMOConfigID = TryCast(cmbMOForTemplate.SelectedItem, clsComboBoxItem).Value
                objGenerateTemplate.copyFilterStringsFromMO = ceCopyFilterStrings.Checked
                objGenerateTemplate.copyInclusionListFromMO = ceCopyInclusionList.Checked
                objGenerateTemplate.copyExclusionListFromMO = ceCopyExclusionList.Checked
                objGenerateTemplate.copyParamExclusionListFromTemplate = ceCopyParamExclusions.Checked
            Else
                RefCheckCopyFromCommitted = True
                frmRefCheck.copyFromSrcTemplateID = TryCast(cmbTemplate.SelectedItem, clsComboBoxItem).Value
                frmRefCheck.copyFromSrcTemplateMOConfigID = TryCast(cmbMOForTemplate.SelectedItem, clsComboBoxItem).Value
                frmRefCheck.copyFilterStringsFromMO = ceCopyFilterStrings.Checked
                frmRefCheck.copyInclusionListFromMO = ceCopyInclusionList.Checked
                frmRefCheck.copyExclusionListFromMO = ceCopyExclusionList.Checked
                frmRefCheck.copyParamExclusionListFromTemplate = ceCopyParamExclusions.Checked
            End If

            Me.Close()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

End Class
