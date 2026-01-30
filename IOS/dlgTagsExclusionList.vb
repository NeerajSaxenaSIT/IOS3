Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Grid
Imports IOS.DataLibrary
Imports IOS.Library

Public Class dlgTagsExclusionList

    Public _tech As String = Nothing
    Dim objfrmTech As frmTechnology = Nothing

    Private Sub dlgTagsExclusionList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            LoadTagsList()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub LoadTagsList()
        Try
            Dim sqlParam As String = Nothing
            Dim connString As String = Nothing
            Dim parray()() As String = {
                New String() {"@Tech", Chr(39) & _tech & Chr(39)}
            }
            sqlParam = GetSQL(8822, parray)(1)
            connString = GetSQL(8822, parray)(0)
            Dim dt = DataAccessorODBC.GetDataTable(connString, sqlParam)
            dt.Columns.Add("Select", GetType(Boolean))
            IOSDevExpressGrid.PopulateDataInGrid(gcTagsExcList, gvTagsExcList, dt, "ALL")

            Dim riChkSelect As RepositoryItemCheckEdit = TryCast(gcTagsExcList.RepositoryItems.Add("CheckEdit"), RepositoryItemCheckEdit)
            riChkSelect.CheckStyle = CheckStyles.Standard
            riChkSelect.AllowGrayed = False
            riChkSelect.NullStyle = StyleIndeterminate.Unchecked
            gvTagsExcList.Columns("Select").ColumnEdit = riChkSelect
            gvTagsExcList.Columns("Select").VisibleIndex = 0
        Catch
        End Try
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

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Try
            objfrmTech = objFrmTechList.Where(Function(x) x.Network.ToUpper.Equals(_tech)).LastOrDefault()
            For iCntr = 0 To gvTagsExcList.RowCount - 1
                If Not IsDBNull(gvTagsExcList.GetRowCellValue(iCntr, "Select")) AndAlso CBool(gvTagsExcList.GetRowCellValue(iCntr, "Select")) = True Then
                    If objfrmTech.dtTagsExcListTopX.Select("ListID=" & CInt(gvTagsExcList.GetRowCellValue(iCntr, "TagID"))).Length = 0 Then
                        Dim drNewRow As DataRow = objfrmTech.dtTagsExcListTopX.NewRow()
                        drNewRow("Select") = CBool(gvTagsExcList.GetRowCellValue(iCntr, "Select"))
                        drNewRow("ListID") = CInt(gvTagsExcList.GetRowCellValue(iCntr, "TagID"))
                        drNewRow("ListName") = CStr(gvTagsExcList.GetRowCellValue(iCntr, "TagName"))
                        objfrmTech.dtTagsExcListTopX.Rows.Add(drNewRow)
                        objfrmTech.dtTagsExcListTopX.AcceptChanges()
                    End If
                End If
            Next
            Me.DialogResult = DialogResult.OK
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub gvTagsExcList_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gvTagsExcList.ShowingEditor
        Try
            If (gvTagsExcList.FocusedColumn().FieldName = "Select") Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class