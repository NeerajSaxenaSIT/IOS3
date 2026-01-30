Public Class IOSEvent

    Private _EventId As String
    Public Property EventId() As String
        Get
            Return _EventId
        End Get
        Set(ByVal value As String)
            _EventId = value
        End Set
    End Property
    Private _DtId As String
    Public Property DtId() As String
        Get
            Return _DtId
        End Get
        Set(ByVal value As String)
            _DtId = value
        End Set
    End Property


    Private _AnalysisComment As String
    Public Property AnalysisComment() As String
        Get
            Return _AnalysisComment
        End Get
        Set(ByVal value As String)
            _AnalysisComment = value
        End Set
    End Property

    Private _Status As String
    Public Property Status() As String
        Get
            Return _Status
        End Get
        Set(ByVal value As String)
            _Status = value
        End Set
    End Property

    Private _AcceptanceAnalysis As String
    Public Property AcceptanceAnalysis() As String
        Get
            Return _AcceptanceAnalysis
        End Get
        Set(ByVal value As String)
            _AcceptanceAnalysis = value
        End Set
    End Property

    Private _AcceptanceProposal As String
    Public Property AcceptanceProposal() As String
        Get
            Return _AcceptanceProposal
        End Get
        Set(ByVal value As String)
            _AcceptanceProposal = value
        End Set
    End Property

    Private _ImplementationDate As DateTime?
    Public Property ImplementationDate() As DateTime?
        Get
            Return _ImplementationDate
        End Get
        Set(ByVal value As DateTime?)
            _ImplementationDate = value
        End Set
    End Property

    Private _AnalysisDesc As String
    Public Property AnalysisDesc() As String
        Get
            Return _AnalysisDesc
        End Get
        Set(ByVal value As String)
            _AnalysisDesc = value
        End Set
    End Property

    Private _ImportedDate As DateTime?
    Public Property ImportedDate() As DateTime?
        Get
            Return _ImportedDate
        End Get
        Set(ByVal value As DateTime?)
            _ImportedDate = value
        End Set
    End Property

    Private _RespEngg As String
    Public Property RespEngg() As String
        Get
            Return _RespEngg
        End Get
        Set(ByVal value As String)
            _RespEngg = value
        End Set
    End Property

    Public Sub New(ByVal eventId As String, ByVal dtid As String, ByVal AnalysisComment As String, ByVal Status As String, ByVal AcceptanceAnalysis As String, ByVal AcceptanceProposal As String, ByVal ImplementationDate As DateTime?, ByVal AnalysisDesc As String, ByVal ImportedDate As DateTime, ByVal RespEngg As String)
        Me.EventId = eventId
        Me.DtId = dtid
        Me.AnalysisComment = AnalysisComment
        Me.Status = Status
        Me.AcceptanceAnalysis = AcceptanceAnalysis
        Me.AcceptanceProposal = AcceptanceProposal
        Me.ImplementationDate = ImplementationDate
        Me.AnalysisDesc = AnalysisDesc
        Me.ImportedDate = ImportedDate
        Me.RespEngg = RespEngg
    End Sub

    Public Sub New(ByVal eventId As String, ByVal dtid As String, ByRef eventData As DataRow)
        Me.EventId = eventId
        Me.DtId = dtid
        Try
            Me.AnalysisComment = eventData("AnalysisComment")
        Catch ex As Exception
            Me.AnalysisComment = ""
        End Try
        Try
            Me.Status = eventData("EventStatus")
        Catch ex As Exception
            Me.Status = ""
        End Try
        Try
            Me.AcceptanceAnalysis = eventData("AcceptanceAnalysis")
        Catch ex As Exception
            Me.AcceptanceAnalysis = ""
        End Try
        Try
            Me.AcceptanceProposal = eventData("AcceptanceProposal")
        Catch ex As Exception
            Me.AcceptanceProposal = ""
        End Try
        Try
            Me.ImplementationDate = Convert.ToDateTime(eventData("ImplementationDate"))
        Catch ex As Exception
            Me.ImplementationDate = New Nullable(Of DateTime)
        End Try
        Try
            Me.AnalysisDesc = eventData("Analysis")
        Catch ex As Exception
            Me.AnalysisDesc = ""
        End Try
        Try
            Me.ImportedDate = eventData("TimeStamp")
        Catch ex As Exception
            Me.ImportedDate = New Nullable(Of DateTime)
        End Try
        Try
            Me.RespEngg = eventData("ResponsibleEngineer")
        Catch ex As Exception
            Me.RespEngg = ""
        End Try
    End Sub

    Public Function UpdateEvent(ByVal connStr As String) As Integer
        Dim sql As String = "update dbo.DT_Events SET AnalysisComment = ?, EventStatus = ?, AcceptanceAnalysis = ?, AcceptanceProposal = ?, ImplementationDate = ? where EventID = " & Me.EventId & " AND DTID=" & Me.DtId
        Dim parameters As New List(Of Odbc.OdbcParameter)
        Dim AnalysisComment As New Odbc.OdbcParameter("AnalysisComment", Odbc.OdbcType.NVarChar)
        AnalysisComment.Value = Me.AnalysisComment
        parameters.Add(AnalysisComment)
        Dim EventStatus As New Odbc.OdbcParameter("EventStatus", Odbc.OdbcType.NVarChar)
        If (Me.Status Is Nothing) Then
            EventStatus.Value = DBNull.Value
        Else
            EventStatus.Value = Me.Status
        End If
        parameters.Add(EventStatus)
        Dim AcceptanceAnalysis As New Odbc.OdbcParameter("AcceptanceAnalysis", Odbc.OdbcType.NVarChar)
        If (Me.AcceptanceAnalysis Is Nothing) Then
            AcceptanceAnalysis.Value = DBNull.Value
        Else
            AcceptanceAnalysis.Value = Me.AcceptanceAnalysis
        End If
        parameters.Add(AcceptanceAnalysis)
        Dim AcceptanceProposal As New Odbc.OdbcParameter("AcceptanceProposal", Odbc.OdbcType.NVarChar)
        If (Me.AcceptanceProposal Is Nothing) Then
            AcceptanceProposal.Value = DBNull.Value
        Else
            AcceptanceProposal.Value = Me.AcceptanceProposal
        End If
        parameters.Add(AcceptanceProposal)
        Dim parameter As New Odbc.OdbcParameter("iDate", Odbc.OdbcType.DateTime)
        If (Me.ImplementationDate Is Nothing) Then
            parameter.Value = DBNull.Value
        Else
            parameter.Value = Me.ImplementationDate
        End If

        parameters.Add(parameter)
        Return IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connStr, sql, parameters)
    End Function

End Class
