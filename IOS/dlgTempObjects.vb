Public Class dlgTempObjects

    Private Sub dlgTempObjects_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = Nothing
            strConnection = GetSQL(4580, parray)(0)
            sqlParam = GetSQL(4580, parray)(1)

            Dim dtTempObjects = New DataTable()
            dtTempObjects = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

            IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcTempObjects, gvTempObjects, dtTempObjects, "ALL")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

End Class