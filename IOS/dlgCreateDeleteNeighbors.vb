Public Class dlgCreateDeleteNeighbors

    Private sCellName As String
    Public Property SourceCellName() As String
        Get
            Return sCellName
        End Get
        Set(ByVal value As String)
            sCellName = value
        End Set
    End Property

    Private tCellNameList As New List(Of String)
    Public Property TargetCellName() As List(Of String)
        Get
            Return tCellNameList
        End Get
        Set(ByVal value As List(Of String))
            tCellNameList = value
        End Set
    End Property

    Private Sub dlgCreateDeleteNeighbors_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim dtCells As New DataTable
            dtCells.Columns.Add("S_CELLNAME", GetType(System.String))
            dtCells.Columns.Add("T_CELLNAME", GetType(System.String))
            dtCells.Columns.Add("DELETEFLAG", GetType(System.Int32))
            dtCells.Columns.Add("REVERSEFLAG", GetType(System.Int32))
            dtCells.Columns.Add("HIGHPRIONB", GetType(System.Int32))

            For Each tCellName As String In TargetCellName
                Dim newRow As DataRow = dtCells.NewRow()
                newRow("S_CELLNAME") = sCellName
                newRow("T_CELLNAME") = tCellName
                newRow("DELETEFLAG") = 0
                newRow("REVERSEFLAG") = 0
                newRow("HIGHPRIONB") = 0
                dtCells.Rows.Add(newRow)
            Next
            gcNeighbors.DataSource = dtCells
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub btnCommit_Click(sender As Object, e As EventArgs) Handles btnCommit.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If txtManualCampName.Text.Trim = "" Then
                SetStatus("Manual Campaign Name cannot be empty")
                txtManualCampName.Focus()
                Exit Sub
            Else
                'Create manual campaign
                Dim parray()() As String = {
                                                New String() {"@CampaignNameNew", Chr(39) & txtManualCampName.Text.Trim & Chr(39)},
                                                New String() {"@CampaignDescription", Chr(39) & txtManualCampName.Text.Trim & Chr(39)},
                                                New String() {"@Owner", Chr(39) & Environment.UserName.ToString & Chr(39)},
                                                New String() {"@IsPublic", IIf(ceIsPublic.Checked = True, 1, 0)}
                                           }
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(GetSQL(4510, parray)(0), GetSQL(4510, parray)(1))

                'Get CampaignID of newly created manual campaign
                parray = {
                             New String() {"@campaignName", Chr(39) & txtManualCampName.Text.Trim & Chr(39)}
                         }
                Dim campaignID As Integer = IOS.DataLibrary.DataAccessorODBC.GetDataTable(GetSQL(4583, parray)(0), GetSQL(4583, parray)(1)).Rows(0)(0)

                'Bulk Insert grid info into [NB_Manual_Input]
                Dim connArr() As String = GetIOSConnection(1000)
                Dim dtRecords As DataTable = CType(gcNeighbors.DataSource, DataTable).GetChanges(DataRowState.Added)

                Dim colCampID As New Data.DataColumn("CampaignID", GetType(System.String))
                colCampID.DefaultValue = campaignID
                dtRecords.Columns.Add(colCampID)
                colCampID.SetOrdinal(0)

                If dtRecords IsNot Nothing Then
                    frmNBManagement.InsertBulkDataToServer(connArr(1), "[" & connArr(2) & "].[dbo].[NB_Manual_Input]", dtRecords)
                End If

                'Run [NB_Campaign_Run_Manual]
                parray = {
                             New String() {"@CampaignID", campaignID}
                         }
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(GetSQL(4541, parray)(0), GetSQL(4541, parray)(1))
            End If
            Me.Close()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub gvNeighbors_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gvNeighbors.ShowingEditor
        Try
            If (gvNeighbors.FocusedColumn().FieldName = "DELETEFLAG") Or (gvNeighbors.FocusedColumn().FieldName = "REVERSEFLAG") Or (gvNeighbors.FocusedColumn().FieldName = "HIGHPRIONB") Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvNeighbors_CellValueChanging(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gvNeighbors.CellValueChanging
        Try
            If (e.Column.FieldName = "DELETEFLAG") Or (e.Column.FieldName = "REVERSEFLAG") Or (e.Column.FieldName = "HIGHPRIONB") Then
                If e.Value = 0 Or e.Value = 1 Then
                    tlpMain.RowStyles(2).SizeType = SizeType.Absolute
                    tlpMain.RowStyles(2).Height = 0
                Else
                    SetStatus("DELETEFLAG, REVERSEFLAG and HIGHPRIONB columns only accept 0 or 1")
                    gvNeighbors.SetRowCellValue(e.RowHandle, e.Column.FieldName, 0)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetStatus(ByVal message As String)
        lblStatus.ForeColor = Color.Red
        lblStatus.Visible = True
        lblStatus.Text = message

        tlpMain.RowStyles(2).SizeType = SizeType.Absolute
        tlpMain.RowStyles(2).Height = 25

        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        lblStatus.Text = ""
        lblStatus.Visible = False
        RemoveHandler Timer1.Tick, AddressOf Timer1_Tick
        Timer1.Enabled = False
        Timer1.Stop()

        tlpMain.RowStyles(2).SizeType = SizeType.Absolute
        tlpMain.RowStyles(2).Height = 0

        Me.Cursor = Cursors.Default
        Application.DoEvents()
    End Sub

End Class