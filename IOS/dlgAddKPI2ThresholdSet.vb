Imports IOS.DataLibrary
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls

Public Class dlgAddKPI2ThresholdSet

    Private dtKPIs As DataTable = Nothing
    Private lstSelectedKPIIDs As New List(Of Integer)

    Public thresholdSetID As Integer = Nothing
    Public kpiSetID As Integer = Nothing

#Region "Events"

    Private Sub dlgAddKPI2ThresholdSet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LoadKPIs()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnInsert_Click(sender As Object, e As EventArgs) Handles btnInsert.Click
        Try
            If chkListKPIs.CheckedItems.Count > 0 Then
                For Each chkItm As DataRowView In chkListKPIs.CheckedItems
                    AddKpiToThresholdSet(CInt(chkItm(0)))
                Next
                SetMessage("Inserted " & chkListKPIs.CheckedItems.Count & " KPIs")
                Me.Close()
            Else
                SetMessage("Select at least one KPI")
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub


    Private Sub chkListKPIs_MouseDown(sender As Object, e As MouseEventArgs) Handles chkListKPIs.MouseDown
        If e.Button = MouseButtons.Right Then
            chkListKPIs.CheckOnClick = False
        ElseIf e.Button = MouseButtons.Left Then
            chkListKPIs.CheckOnClick = True
        End If
    End Sub


    'Private Sub txtSearchKPI_EditValueChanged(sender As Object, e As EventArgs)
    '    Try
    '        'chkListKPIs.SuspendLayout()

    '        If txtSearchKPI.Text.Length > 2 Then
    '            Dim dtTemp As DataTable = dtKPIs.AsEnumerable().Where(Function(x) x.Field(Of String)("KPI_Name").ToLower.Contains(txtSearchKPI.Text.Trim.ToLower)).CopyToDataTable()
    '            chkListKPIs.DataSource = dtTemp
    '        Else
    '            chkListKPIs.DataSource = dtKPIs
    '        End If
    '        chkListKPIs.DisplayMember = "KPI_Name"
    '        chkListKPIs.ValueMember = "SQLKPI_ID"

    'chkListKPIs.ResumeLayout()

    'If lstSelectedKPIIDs.Count > 0 Then
    '    For Each kpiID As Integer In lstSelectedKPIIDs
    '        Dim index As Integer = chkListKPIs.FindItem(0, True, Function(x) x.ItemValue = kpiID)
    '        chkListKPIs.SetItemChecked(index, True)
    '    Next
    'End If

    'Catch ex As Exception
    '        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
    '        _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
    '    End Try
    'End Sub

    Private Sub chkListKPIs_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles chkListKPIs.ItemCheck
        Try
            If e.State = CheckState.Checked Then
                lstSelectedKPIIDs.Add(chkListKPIs.GetItem(e.Index)(0))
            Else
                If lstSelectedKPIIDs.Contains(chkListKPIs.GetItem(e.Index)(0)) Then
                    lstSelectedKPIIDs.Remove(chkListKPIs.GetItem(e.Index)(0))
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
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

#Region "Methods"

    Private Sub LoadKPIs()
        Dim parray()() As String = {
            New String() {"@ThresholdSetID", thresholdSetID},
            New String() {"@KPISetID", kpiSetID}
        }
        Dim strConnection As String = GetSQL(7039, parray)(0)
        Dim sqlParam As String = GetSQL(7039, parray)(1)
        dtKPIs = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        chkListKPIs.DataSource = dtKPIs
        chkListKPIs.DisplayMember = "KPI_Name"
        chkListKPIs.ValueMember = "SQLKPIID"
    End Sub

    Private Sub AddKpiToThresholdSet(sqlKpiID As Integer)
        Dim parray()() As String = {
            New String() {"@ThresholdSetID", thresholdSetID},
            New String() {"@SQLKPIID", sqlKpiID}
        }
        Dim strConnection As String = GetSQL(7040, parray)(0)
        Dim sqlParam As String = GetSQL(7040, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

#End Region

End Class