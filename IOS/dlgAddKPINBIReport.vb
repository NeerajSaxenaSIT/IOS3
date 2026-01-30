Public Class dlgAddKPINBIReport

#Region "Variables/Properties"

    Private parray()() As String = Nothing
    Private strConnAndSqlParam() As String = Nothing

    Public reportID As Integer = Nothing
    Public iosTech As String = Nothing
    Public objectType As String = Nothing
    Private dtKPI As DataTable = Nothing

#End Region

#Region "Methods"

    Private Sub LoadKpiList()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@iosTech", Chr(39) & lblTechnology.Text.Trim & Chr(39)},
            New String() {"@counterType", Chr(39) & lblObjectType.Text.Trim & Chr(39)}
        }
        strConnection = GetSQL(3010, parray)(0)
        sqlParam = GetSQL(3010, parray)(1)

        dtKPI = New DataTable()
        dtKPI = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        lstviewKPI.DataSource = Nothing
        lstviewKPI.Columns.Clear()
        lstviewKPI.DataSource = dtKPI
        lstviewKPI.BestFitColumns()
        lstviewKPI.Columns(0).Visible = False
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

#Region "Events"

    Private Sub dlgAddKPIRule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            lblTechnology.Text = iosTech
            lblObjectType.Text = objectType
            LoadKpiList()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub txtSearckKPI_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearckKPI.KeyUp
        Try
            If dtKPI IsNot Nothing Then
                If (txtSearckKPI.Text.Length > 0) Then
                    dtKPI.DefaultView.RowFilter = "KPI_Name Like '%" + txtSearckKPI.Text + "%'"
                Else
                    dtKPI.DefaultView.RowFilter = ""
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Dim lstCheckedNodes As New List(Of DevExpress.XtraTreeList.Nodes.TreeListNode)
            lstCheckedNodes = lstviewKPI.GetAllCheckedNodes()
            If lstCheckedNodes.Count > 0 Then
                For iCntr = 0 To lstCheckedNodes.Count - 1
                    Dim nd As DevExpress.XtraTreeList.Nodes.TreeListNode = lstCheckedNodes(iCntr)
                    Dim dataKpi As DataRowView = lstviewKPI.GetDataRecordByNode(nd)

                    If dataKpi IsNot Nothing Then
                        parray = Nothing
                        strConnAndSqlParam = Nothing
                        parray = {
                        New String() {"@ReportID", Me.reportID},
                        New String() {"@SQLKPIID", dataKpi("SQLKPI_ID")}
                    }
                        strConnAndSqlParam = GetSQL(8528, parray)
                        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnAndSqlParam(0), strConnAndSqlParam(1))
                    End If
                Next
                Me.Close()
            Else
                SetMessage("Please Select KPI")
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

    Private Sub cmKPITreeList_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmKPITreeList.Opening
        Try
            If lstviewKPI.GetAllCheckedNodes().Count = 0 Then
                tsmi_ViewCheckedItems.Enabled = False
                tsmi_UncheckAll.Enabled = False
            Else
                tsmi_ViewCheckedItems.Enabled = True
                tsmi_UncheckAll.Enabled = True
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub tsmi_PasteKPI_Click(sender As Object, e As EventArgs) Handles tsmi_PasteKPI.Click
        Try
            lstviewKPI.UncheckAll()
            lstviewKPI.ClearSelection()

            Dim iCntrMatchKPI As Integer = 0
            Dim str As String = Clipboard.GetText()
            Dim rows() As String = str.Split(ControlChars.NewLine)

            Dim clipboardObjects() As String = String.Join(ControlChars.Lf, rows).Replace(ControlChars.Lf, ",").Replace(",,", ",").Split(",")
            Dim matchedObjects As List(Of String) = clipboardObjects.Intersect(clipboardObjects, StringComparer.InvariantCultureIgnoreCase).ToList()

            If matchedObjects.Count = 0 Then
                SetMessage("No Matching KPI Found")
                Exit Sub
            End If

            For Each val As String In matchedObjects
                Dim nd As DevExpress.XtraTreeList.Nodes.TreeListNode = lstviewKPI.FindNode(Function(x) x.GetDisplayText("KPI_Name").ToLower = val.ToLower)
                If nd IsNot Nothing Then
                    nd.Checked = True
                    lstviewKPI.SelectNode(nd)
                    iCntrMatchKPI = iCntrMatchKPI + 1
                End If
            Next

            If iCntrMatchKPI = 0 Then
                SetMessage("No Matching KPI(s) Found")
            Else
                SetMessage(iCntrMatchKPI & " Matching KPI(s) Found And Selected")
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub tsmi_UncheckAll_Click(sender As Object, e As EventArgs) Handles tsmi_UncheckAll.Click
        Try
            Dim lstCheckedNodes As New List(Of DevExpress.XtraTreeList.Nodes.TreeListNode)
            lstCheckedNodes = lstviewKPI.GetAllCheckedNodes()
            For iCntr = 0 To lstCheckedNodes.Count - 1
                Dim nd As DevExpress.XtraTreeList.Nodes.TreeListNode = lstCheckedNodes(iCntr)
                nd.Checked = False
            Next
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub tsmi_ViewCheckedItems_Click(sender As Object, e As EventArgs) Handles tsmi_ViewCheckedItems.Click
        Try
            lstviewKPI.SuspendLayout()
            lstviewKPI.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If tsmi_ViewCheckedItems.Checked = True Then
                tsmi_ViewCheckedItems.Checked = False
                For Each nd As DevExpress.XtraTreeList.Nodes.TreeListNode In lstviewKPI.Nodes
                    nd.Visible = True
                Next
            Else
                tsmi_ViewCheckedItems.Checked = True
                For Each nd As DevExpress.XtraTreeList.Nodes.TreeListNode In lstviewKPI.Nodes
                    If nd.Checked = False Then
                        nd.Visible = False
                    End If
                Next
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            lstviewKPI.Cursor = Cursors.Default
            Application.DoEvents()
            lstviewKPI.ResumeLayout()
        End Try
    End Sub

#End Region

End Class