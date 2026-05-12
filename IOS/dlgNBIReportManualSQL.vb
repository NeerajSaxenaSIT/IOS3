Imports IOS.Library
Imports IOS.DataLibrary

Public Class dlgNBIReportManualSQL

    Public sqlQuery As String = Nothing
    Public ReadOnlyConnStr As String = Nothing

    Private Sub dlgNBIReportManualSQL_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If Me.sqlQuery IsNot Nothing Then
                Dim dt As DataTable = DataAccessorODBC.GetDataTableForNBIReportManualSQL(ReadOnlyConnStr, sqlQuery)
                IOSDevExpressGrid.PopulateDataInGrid(gcManualSQLQry, gvManualSQLQry, dt, "ALL")
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

End Class