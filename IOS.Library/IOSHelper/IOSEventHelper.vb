Public Class IOSEventHelper
    Public Shared Function GetEventData(ByRef data As DataTable, ByRef eventId As String, ByRef dtid As String, ByRef connStr As String) As DataTable
        If Not data Is Nothing And Not data.Rows.Count > 0 Then
            Dim sqlChart As String = "EXEC dbo.IOS_EventsGridTab " & eventId & ", " & dtid '& "', '" & ss3G & "' "
            data = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStr, sqlChart)
            Dim dataTable As DataTable = New DataTable(data.TableName)
            Dim col As DataColumn = New DataColumn("ID", Type.GetType("System.Int32"))
            col.AutoIncrement = True
            col.AutoIncrementSeed = 1
            col.AutoIncrementStep = 1
            dataTable.Columns.Add(col)
            Dim dataTableReader As DataTableReader = New DataTableReader(data)
            dataTable.Load(dataTableReader)

            'Dim col As DataColumn = New DataColumn()
            'col.ColumnName = "ID"
            'col.DataType = System.Type.GetType("System.Int32")
            'col.AutoIncrement = True
            'col.AutoIncrementSeed = 1
            'col.AutoIncrementStep = 1
            'data.Columns.Add(col)

            'data.AcceptChanges()
            Return dataTable
        Else
            Return data
        End If
    End Function

    Public Shared Function GetEvent(ByRef eventId As String, ByRef dtid As String, ByRef connstr As String) As IOSEvent
        'frm_IOS_MDI.LogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit FormatTableDateTimeString")
        Dim sql As String = "select AnalysisComment, EventStatus, AcceptanceAnalysis, AcceptanceProposal, TimeStamp, Analysis, ImplementationDate, ResponsibleEngineer from DT_Events where EventID = " & eventId & " and dtid=" + dtid + ""
        Dim data As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connstr, sql)
        If (Not data Is Nothing) And data.Rows.Count > 0 Then
            Return New IOSEvent(eventId, dtid, data.Rows(0))
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function GetApplicationPath(ByVal rootPath) As String
        Dim path As String = rootPath
        path = path.Substring(0, path.LastIndexOf(System.IO.Path.DirectorySeparatorChar))
        path = path.Substring(0, path.LastIndexOf(System.IO.Path.DirectorySeparatorChar))
        path = path.Substring(0, path.LastIndexOf(System.IO.Path.DirectorySeparatorChar))
        Return path
    End Function
End Class
