Public Class DataAccessorSQL
    Private Shared _AsyncConnString As String
    Private Shared _AsyncSQL As String
    Private Shared _AsyncTimeOut As Integer
    Private Shared _KeepConnectionOpen As Boolean = False
    Shared con As System.Data.Odbc.OdbcConnection

    Public Shared Property Async_ConnectionString() As String
        Get
            Return _AsyncConnString
        End Get
        Set(ByVal value As String)
            _AsyncConnString = value
        End Set
    End Property
    Public Shared Property Async_SQL() As String
        Get
            Return _AsyncSQL
        End Get
        Set(ByVal value As String)
            _AsyncSQL = value
        End Set
    End Property
    Public Shared Property Async_TimeOut() As Integer
        Get
            Return _AsyncTimeOut
        End Get
        Set(ByVal value As Integer)
            _AsyncTimeOut = value
        End Set
    End Property
    Public Shared WriteOnly Property KeepConnectionOpen() As Boolean
        Set(ByVal value As Boolean)
            _KeepConnectionOpen = value
            If Not (value) Then
                CloseConnection(con)
            End If
        End Set
    End Property
    Shared Function OpenConnection(ByVal constr As String, Optional ByVal timeOut As Integer = 5) As System.Data.Odbc.OdbcConnection
        If (con Is Nothing) Then
            con = New System.Data.Odbc.OdbcConnection(constr)
        ElseIf Not (con.ConnectionString = constr) Then
            con = New System.Data.Odbc.OdbcConnection(constr)
        End If
        If (con.State = ConnectionState.Closed) Then
            con.ConnectionTimeout = timeOut
            con.Open()
        End If
        Return con
    End Function
    Shared Sub CloseConnection(ByRef conn As System.Data.Odbc.OdbcConnection)
        If Not (con Is Nothing) And Not _KeepConnectionOpen Then
            If (conn.State = ConnectionState.Open) Then
                conn.Close()
            End If
        End If

    End Sub
    Public Shared Sub ExecuteNonQuery_Async(state As Object)
        Dim cnQODBC As System.Data.Odbc.OdbcConnection = Nothing
        Dim daQODBC As System.Data.Odbc.OdbcCommand = Nothing

        Try
            cnQODBC = OpenConnection(_AsyncConnString)
            ' cnQODBC.Open()
            daQODBC = New System.Data.Odbc.OdbcCommand(_AsyncSQL, cnQODBC)
            daQODBC.CommandTimeout = _AsyncTimeOut

            daQODBC.ExecuteNonQuery()
        Catch ex As Exception
            KeepConnectionOpen = False
            Throw ex
        Finally
            If Not cnQODBC Is Nothing Then
                CloseConnection(cnQODBC)
            End If
            If Not daQODBC Is Nothing Then
                daQODBC.Dispose()
            End If
        End Try
    End Sub
    Public Shared Function ExecuteNonQuery(ByVal connstring As String, ByVal sql As String, Optional ByVal ConnectTimeout As Integer = 10) As Integer
        Dim cnQODBC As System.Data.Odbc.OdbcConnection = Nothing
        Dim daQODBC As System.Data.Odbc.OdbcCommand = Nothing

        Try
            cnQODBC = OpenConnection(connstring, ConnectTimeout)
            daQODBC = New System.Data.Odbc.OdbcCommand(sql, cnQODBC)
            Return daQODBC.ExecuteNonQuery()
        Catch ex As Exception
            KeepConnectionOpen = False
            Throw ex
        Finally
            If Not cnQODBC Is Nothing Then
                CloseConnection(cnQODBC)
            End If
            If Not daQODBC Is Nothing Then
                daQODBC.Dispose()
            End If
        End Try
    End Function
    Public Shared Function ExecuteScalar(ByVal connstring As String, ByVal sql As String) As Integer
        Dim cnQODBC As System.Data.Odbc.OdbcConnection = Nothing
        Dim daQODBC As System.Data.Odbc.OdbcCommand = Nothing

        Try
            cnQODBC = OpenConnection(connstring)
            daQODBC = New System.Data.Odbc.OdbcCommand(sql, cnQODBC)
            Return daQODBC.ExecuteScalar()
        Catch ex As Exception
            KeepConnectionOpen = False
            Throw ex
        Finally
            If Not cnQODBC Is Nothing Then
                CloseConnection(cnQODBC)
            End If
            If Not daQODBC Is Nothing Then
                daQODBC.Dispose()
            End If
        End Try
    End Function
    Public Shared Function ExecuteNonQuery(ByVal connstring As String, ByVal sql As String, ByRef parameters As List(Of Odbc.OdbcParameter)) As Integer
        Dim cnQODBC As System.Data.Odbc.OdbcConnection = Nothing
        Dim daQODBC As System.Data.Odbc.OdbcCommand = Nothing

        Try
            cnQODBC = OpenConnection(connstring)
            daQODBC = New System.Data.Odbc.OdbcCommand(sql, cnQODBC)
            For Each parameter As Odbc.OdbcParameter In parameters
                daQODBC.Parameters.Add(parameter)
            Next

            Return daQODBC.ExecuteNonQuery()
        Catch ex As Exception
            KeepConnectionOpen = False
            Throw ex
        Finally
            If Not cnQODBC Is Nothing Then
                CloseConnection(cnQODBC)
            End If
            If Not daQODBC Is Nothing Then
                daQODBC.Dispose()
            End If
        End Try
    End Function
    Public Shared Function ExecuteDataTable(ByVal connstring As String, ByVal sql As String) As DataTable

        If sql = "" Or connstring = "" Then
            Return Nothing
        End If

        Dim dsOSS As System.Data.DataSet = Nothing
        Dim dtOSS As DataTable = Nothing

        Try
            dsOSS = ExecuteDataSet(connstring, sql)
            If Not dsOSS Is Nothing AndAlso dsOSS.Tables.Count > 0 Then
                dtOSS = dsOSS.Tables(0)
            End If
            Return dtOSS
        Catch ex As Exception

            KeepConnectionOpen = False
            MsgBox("Problem getting data from server using: " & connstring.Split(";uid")(0) & Chr(13) & ex.Message.ToString)
            Return Nothing
        Finally

            If Not dsOSS Is Nothing Then
                dsOSS.Dispose()
            End If

        End Try

    End Function
    Public Shared Function ExecuteDataTable(ByVal connstring As String, ByVal sql As String, ByVal parameters As List(Of Odbc.OdbcParameter)) As DataTable

        If sql = "" Or connstring = "" Then
            Return Nothing
        End If

        Dim dsOSS As System.Data.DataSet = Nothing
        Dim dtOSS As DataTable = Nothing

        Try
            dsOSS = ExecuteDataSet(connstring, sql, parameters)
            If Not dsOSS Is Nothing And dsOSS.Tables.Count > 0 Then
                dtOSS = dsOSS.Tables(0)
            End If
            Return dtOSS
        Catch ex As Exception
            KeepConnectionOpen = False

            MsgBox("Problem getting data from server using: " & connstring.Split(";uid")(0) & Chr(13) & ex.Message.ToString)
            Return Nothing
        Finally
            If Not dsOSS Is Nothing Then
                dsOSS.Dispose()
            End If
        End Try
    End Function
    Public Shared Function ExecuteDataSet(ByVal connstring As String, ByVal sql As String, ByVal parameters As List(Of Odbc.OdbcParameter)) As DataSet
        If sql = "" Or connstring = "" Then
            Return Nothing
        End If

        Dim cnOSS As System.Data.Odbc.OdbcConnection = Nothing
        Dim daOSS As System.Data.Odbc.OdbcDataAdapter = Nothing
        Dim dsOSS As System.Data.DataSet = Nothing

        Try
            cnOSS = OpenConnection(connstring, 5)
            Dim cmd As New Odbc.OdbcCommand(sql, cnOSS)
            cmd.CommandTimeout = 0
            If Not parameters Is Nothing Then
                For Each parameter As Odbc.OdbcParameter In parameters
                    cmd.Parameters.Add(parameter)
                Next
            End If

            daOSS = New System.Data.Odbc.OdbcDataAdapter(cmd)
            dsOSS = New System.Data.DataSet
            daOSS.SelectCommand.CommandTimeout = 0
            daOSS.Fill(dsOSS)
            Return dsOSS

        Catch ex As Exception

            KeepConnectionOpen = False
            MsgBox("Problem getting data from server using: " & connstring.Split(";uid")(0) & Chr(13) & ex.Message.ToString)
            Return Nothing
        Finally
            If Not daOSS Is Nothing Then
                daOSS.Dispose()
            End If

            If Not cnOSS Is Nothing Then
                CloseConnection(cnOSS)
            End If
        End Try
    End Function
    Public Shared Function ExecuteDataSet(ByVal connstring As String, ByVal sql As String, Optional ByVal isStoredProcedure As Boolean = False, Optional ByVal queryTimeOut As Integer = 0) As DataSet
        If sql = "" Or connstring = "" Then
            Return Nothing
        End If

        Dim cnOSS As System.Data.Odbc.OdbcConnection = Nothing
        Dim daOSS As System.Data.Odbc.OdbcDataAdapter = Nothing
        Dim dsOSS As System.Data.DataSet = Nothing

        Try
            cnOSS = OpenConnection(connstring, 5)
            daOSS = New System.Data.Odbc.OdbcDataAdapter(sql, cnOSS)
            dsOSS = New System.Data.DataSet
            daOSS.SelectCommand.CommandTimeout = queryTimeOut
            If isStoredProcedure = True Then
                daOSS.SelectCommand.CommandType = CommandType.StoredProcedure
            End If

            daOSS.Fill(dsOSS)
            Return dsOSS
        Catch ex As Exception
            KeepConnectionOpen = False
            MsgBox("Problem getting data from server using: " & connstring.Split(";uid")(0) & Chr(13) & ex.Message.ToString)
            Return Nothing
        Finally
            If Not daOSS Is Nothing Then
                daOSS.Dispose()
            End If

            If Not cnOSS Is Nothing Then
                CloseConnection(cnOSS)
            End If
        End Try
    End Function
    
End Class
