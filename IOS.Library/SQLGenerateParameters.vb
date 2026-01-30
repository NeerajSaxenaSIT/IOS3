Public Class SQLGenerateParameters


    Private _SQLCommand As List(Of String)
    Public Property SQLCommands() As List(Of String)
        Get
            Return _SQLCommand
        End Get
        Set(ByVal value As List(Of String))
            _SQLCommand = value
        End Set
    End Property

    Private _connectionString As List(Of String)
    Public Property ConnectionString() As List(Of String)
        Get
            Return _connectionString
        End Get
        Set(ByVal value As List(Of String))
            _connectionString = value
        End Set
    End Property

    Private _expConnectionString As List(Of String)
    Public Property ExpConnectionString() As List(Of String)
        Get
            Return _expConnectionString
        End Get
        Set(ByVal value As List(Of String))
            _expConnectionString = value
        End Set
    End Property
End Class
