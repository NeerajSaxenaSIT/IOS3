Imports IOS.DataLibrary

Public Class DataAccessorODBC

    Private Shared _KeepConnectionOpen As Boolean = False
    Public Shared WriteOnly Property KeepConnectionOpen() As Boolean
        Set(ByVal value As Boolean)
            _KeepConnectionOpen = value
            If Not (value) Then
                CloseConnection(con)
            End If
        End Set
    End Property

    Shared con As System.Data.Odbc.OdbcConnection
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

    Public Shared Function GetDataTable(ByVal connstring As String, ByVal sql As String, Optional ByVal commandTimeOut As Integer = 60) As DataTable
        If sql = "" Or connstring = "" Then
            Return Nothing
        End If

        WriteString_Query("Timestamp: " & Now.ToString & vbCrLf & "SQL Fired: " & vbCrLf & sql & vbCrLf & "------------------------------" & vbCrLf)
        '   WriteString_Query("ConnStr: " & connstring & vbCrLf)

        Dim dtOSS As New DataTable()
        Try
            Using cnOSS As New System.Data.Odbc.OdbcConnection(connstring)
                cnOSS.ConnectionTimeout = 5
                cnOSS.Open()
                Using daOSS As New System.Data.Odbc.OdbcDataAdapter(sql, cnOSS)
                    Using dsOSS As New System.Data.DataSet
                        daOSS.SelectCommand.CommandTimeout = commandTimeOut
                        daOSS.Fill(dsOSS)
                        If dsOSS.Tables.Count > 0 Then
                            dtOSS = dsOSS.Tables(0)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            WriteString_Query("Timestamp: " & Now.ToString & vbCrLf & "Error Message: " & vbCrLf & ex.Message & vbCrLf & "------------------------------" & vbCrLf)
            Return Nothing
        End Try
        Return dtOSS
    End Function

    Public Shared Function GetDataTableForNBIReportManualSQL(ByVal connstring As String, ByVal sql As String, Optional ByVal commandTimeOut As Integer = 60) As DataTable
        If sql = "" Or connstring = "" Then
            Return Nothing
        End If

        WriteString_Query("Timestamp: " & Now.ToString & vbCrLf & "SQL Fired: " & vbCrLf & sql & vbCrLf & "------------------------------" & vbCrLf)

        Dim dtOSS As New DataTable()
        Try
            Using cnOSS As New System.Data.Odbc.OdbcConnection(connstring)
                cnOSS.ConnectionTimeout = 5
                cnOSS.Open()
                Using daOSS As New System.Data.Odbc.OdbcDataAdapter(sql, cnOSS)
                    Using dsOSS As New System.Data.DataSet
                        daOSS.SelectCommand.CommandTimeout = commandTimeOut
                        daOSS.Fill(dsOSS)
                        If dsOSS.Tables.Count > 0 Then
                            dtOSS = dsOSS.Tables(0)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Dim dt As New DataTable
            dt.Columns.Add("An Error Occured")
            Dim dr As DataRow = dt.NewRow
            dr("An Error Occured") = ex.Message
            dt.Rows.Add(dr)
            Return dt
        End Try
        Return dtOSS
    End Function

    Public Shared Function GetDataTable(ByVal connstring As String, ByVal sql As String, ByVal parameters As List(Of Odbc.OdbcParameter)) As DataTable
        If sql = "" Or connstring = "" Then
            Return Nothing
        End If

        Dim dsOSS As System.Data.DataSet = Nothing
        Dim dtOSS As New DataTable()

        WriteString_Query("Timestamp: " & Now.ToString & vbCrLf & "SQL Fired: " & vbCrLf & sql & vbCrLf & "------------------------------" & vbCrLf)

        Try
            dsOSS = GetDataSet(connstring, sql, parameters)
            If Not dsOSS Is Nothing And dsOSS.Tables.Count > 0 Then
                dtOSS = dsOSS.Tables(0)
            End If
            Return dtOSS
        Catch ex As Exception
            WriteString_Query("Timestamp: " & Now.ToString & vbCrLf & "Error Message: " & vbCrLf & ex.Message & vbCrLf & "------------------------------" & vbCrLf)
            Return Nothing
        Finally
            If Not dsOSS Is Nothing Then
                dsOSS.Dispose()
            End If
        End Try
    End Function

    Public Shared Function GetDataSet(ByVal connstring As String, ByVal sql As String, Optional ByVal commandTimeOut As Integer = 60) As DataSet
        Dim dsOSS As System.Data.DataSet = Nothing
        WriteString_Query("Timestamp: " & Now.ToString & vbCrLf & "SQL Fired: " & vbCrLf & sql & vbCrLf & "------------------------------" & vbCrLf)
        Try
            Using cnOSS As New System.Data.Odbc.OdbcConnection(connstring)
                cnOSS.ConnectionTimeout = 5
                cnOSS.Open()
                Using daOSS As New System.Data.Odbc.OdbcDataAdapter(sql, cnOSS)
                    daOSS.SelectCommand.CommandTimeout = commandTimeOut
                    dsOSS = New System.Data.DataSet
                    daOSS.Fill(dsOSS)
                End Using
            End Using
        Catch ex As Exception
            WriteString_Query("Timestamp: " & Now.ToString & vbCrLf & "Error Message: " & vbCrLf & ex.Message & vbCrLf & "------------------------------" & vbCrLf)
            Return Nothing
        End Try
        Return dsOSS
    End Function

    Public Shared Function GetDataSet(ByVal connstring As String, ByVal sql As String, ByVal parameters As List(Of Odbc.OdbcParameter)) As DataSet
        Dim dsOSS As System.Data.DataSet = Nothing
        WriteString_Query("Timestamp: " & Now.ToString & vbCrLf & "SQL Fired: " & vbCrLf & sql & vbCrLf & "------------------------------" & vbCrLf)
        Try
            Using cnOSS As New System.Data.Odbc.OdbcConnection(connstring)
                cnOSS.ConnectionTimeout = 5
                cnOSS.Open()
                Using cmd As New Odbc.OdbcCommand(sql, cnOSS)
                    cmd.CommandTimeout = 0
                    If Not parameters Is Nothing Then
                        For Each parameter As Odbc.OdbcParameter In parameters
                            cmd.Parameters.Add(parameter)
                        Next
                    End If
                    Using daOSS As New System.Data.Odbc.OdbcDataAdapter(cmd)
                        dsOSS = New System.Data.DataSet
                        daOSS.SelectCommand.CommandTimeout = 0
                        daOSS.Fill(dsOSS)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            WriteString_Query("Timestamp: " & Now.ToString & vbCrLf & "Error Message: " & vbCrLf & ex.Message & vbCrLf & "------------------------------" & vbCrLf)
            Return Nothing
        End Try
        Return dsOSS
    End Function

    Public Shared Function ExecuteNonQuery(ByVal connstring As String, ByVal sql As String)
        WriteString_Query("Timestamp: " & Now.ToString & vbCrLf & "SQL Fired: " & vbCrLf & sql & vbCrLf & "------------------------------" & vbCrLf)
        Try
            Using cnQODBC As New System.Data.Odbc.OdbcConnection(connstring)
                cnQODBC.ConnectionTimeout = 5
                cnQODBC.Open()
                Using daQODBC As New System.Data.Odbc.OdbcCommand(sql, cnQODBC)
                    Return daQODBC.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            WriteString_Query("Timestamp: " & Now.ToString & vbCrLf & "Error Message: " & vbCrLf & ex.Message & vbCrLf & "------------------------------" & vbCrLf)
        End Try
        Return Nothing
    End Function

    Public Shared Function ExecuteNonQuery(ByVal connstring As String, ByVal sql As String, Optional ByVal ConnectTimeout As Integer = 10, Optional commandTimeout As Integer = 30)
        WriteString_Query("Timestamp: " & Now.ToString & vbCrLf & "SQL Fired: " & vbCrLf & sql & vbCrLf & "------------------------------" & vbCrLf)
        Try
            Using cnQODBC As New System.Data.Odbc.OdbcConnection(connstring)
                cnQODBC.ConnectionTimeout = ConnectTimeout
                cnQODBC.Open()
                Using daQODBC As New System.Data.Odbc.OdbcCommand(sql, cnQODBC)
                    daQODBC.CommandTimeout = commandTimeout
                    Return daQODBC.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            WriteString_Query("Timestamp: " & Now.ToString & vbCrLf & "Error Message: " & vbCrLf & ex.Message & vbCrLf & "------------------------------" & vbCrLf)
            '   MsgBox("Timestamp: " & Now.ToString & vbCrLf & "Error Message: " & vbCrLf & ex.Message & vbCrLf & "------------------------------" & vbCrLf)
        End Try
        Return Nothing
    End Function

    Public Shared Function ExecuteNonQuery(ByVal connstring As String, ByVal sql As String, ByRef parameters As List(Of Odbc.OdbcParameter)) As Integer
        WriteString_Query("Timestamp: " & Now.ToString & vbCrLf & "SQL Fired: " & vbCrLf & sql & vbCrLf & "------------------------------" & vbCrLf)
        Try
            Using cnQODBC As New System.Data.Odbc.OdbcConnection(connstring)
                cnQODBC.ConnectionTimeout = 5
                cnQODBC.Open()
                Using daQODBC As New System.Data.Odbc.OdbcCommand(sql, cnQODBC)
                    For Each parameter As Odbc.OdbcParameter In parameters
                        daQODBC.Parameters.Add(parameter)
                    Next
                    Return daQODBC.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            WriteString_Query("Timestamp: " & Now.ToString & vbCrLf & "Error Message: " & vbCrLf & ex.Message & vbCrLf & "------------------------------" & vbCrLf)
            KeepConnectionOpen = False
        End Try
        Return Nothing
    End Function

    Public Shared Function ExecuteScalar(ByVal connstring As String, ByVal sql As String) As Integer
        Dim iResult As Integer = -1
        WriteString_Query("Timestamp: " & Now.ToString & vbCrLf & "SQL Fired: " & vbCrLf & sql & vbCrLf & "------------------------------" & vbCrLf)
        Try
            Using cnQODBC As New System.Data.Odbc.OdbcConnection(connstring)
                cnQODBC.ConnectionTimeout = 5
                cnQODBC.Open()
                Using daQODBC As New System.Data.Odbc.OdbcCommand(sql, cnQODBC)
                    iResult = daQODBC.ExecuteScalar()
                End Using
            End Using
        Catch ex As Exception
            WriteString_Query("Timestamp: " & Now.ToString & vbCrLf & "Error Message: " & vbCrLf & ex.Message & vbCrLf & "------------------------------" & vbCrLf)
        End Try
        Return iResult
    End Function

    Public Shared Function GetDataSetWithSchema(ByVal connstring As String, ByVal sql As String) As DataSet
        Dim dsOSS As System.Data.DataSet = Nothing
        WriteString_Query("Timestamp: " & Now.ToString & vbCrLf & "SQL Fired: " & vbCrLf & sql & vbCrLf & "------------------------------" & vbCrLf)
        Try
            Using cnOSS As New System.Data.Odbc.OdbcConnection(connstring)
                cnOSS.ConnectionTimeout = 5
                cnOSS.Open()
                Using daOSS As New System.Data.Odbc.OdbcDataAdapter(sql, cnOSS)
                    dsOSS = New System.Data.DataSet
                    daOSS.FillSchema(dsOSS, SchemaType.Source)
                End Using
            End Using
            Return dsOSS
        Catch ex As Exception
            WriteString_Query("Timestamp: " & Now.ToString & vbCrLf & "Error Message: " & vbCrLf & ex.Message & vbCrLf & "------------------------------" & vbCrLf)
            dsOSS = Nothing
        End Try
        Return dsOSS
    End Function

End Class
