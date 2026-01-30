Public Class dlgSONException 
    Public jobid As Integer

#Region "Form & Control Events"

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            'Insert Into Exception List
            Dim excstring As String = Nothing
            Dim rowhash() As Byte = Nothing

            Dim rowIndex() As Integer = objfrmSON.gvResult.GetSelectedRows()
            For iCntr As Integer = 0 To rowIndex.Length - 1
                rowhash = CType(objfrmSON.gvResult.GetRowCellValue(rowIndex(iCntr), objfrmSON.gvResult.Columns.Last()), Byte())
                For col = 3 To objfrmSON.gvResult.Columns.Count - 2
                    excstring = excstring + objfrmSON.gvResult.GetRowCellValue(rowIndex(iCntr), objfrmSON.gvResult.Columns(col)).ToString + "_"
                Next
                excstring = excstring.TrimEnd("_")

                Dim params As New List(Of Odbc.OdbcParameter)
                Dim p1 As New Odbc.OdbcParameter
                p1.OdbcType = Odbc.OdbcType.DateTime
                p1.Value = CType(dtpExceptionStartTime.EditValue, DateTime)
                p1.ParameterName = ""
                params.Add(p1)

                Dim p2 As New Odbc.OdbcParameter
                p2.OdbcType = Odbc.OdbcType.DateTime
                p2.Value = CType(dtpExceptionExpiryTime.EditValue, DateTime)
                p2.ParameterName = ""
                params.Add(p2)

                Dim p3 As New Odbc.OdbcParameter
                p3.OdbcType = Odbc.OdbcType.Int
                p3.Value = jobid
                p3.ParameterName = ""
                params.Add(p3)

                Dim p4 As New Odbc.OdbcParameter
                p4.OdbcType = Odbc.OdbcType.NVarChar
                p4.Value = excstring
                p4.ParameterName = ""
                params.Add(p4)

                Dim p5 As New Odbc.OdbcParameter
                p5.OdbcType = Odbc.OdbcType.VarBinary
                p5.Value = rowhash
                p5.ParameterName = ""
                params.Add(p5)

                ''Dim sql As String = "INSERT INTO IOS_Jobs_Exceptions (ExceptionTimeStamp, ExceptionExpiryDate, JobId, ExceptionString, rowHash) VALUES (?,?,?,?,?)"
                ''Dim ds As DataSet = IOS.DataLibrary.DataAccessorODBC.GetDataSet(IOS.Configuration.IOSAppConfigManage.IOSServer, Sql, params)
                IOS.DataLibrary.clsSQLCommands.InsertJobsExceptions(connStrIOSServer, params)
                excstring = ""
            Next
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Catch ex As Exception
            MsgBox("Failed")
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmSONException_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            dtpExceptionExpiryTime.EditValue = DateAdd(DateInterval.Month, 1, Now())
            dtpExceptionStartTime.EditValue = Now()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

End Class