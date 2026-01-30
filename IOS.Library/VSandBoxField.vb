Public Class DevExSandBoxField
    Inherits DevExpress.XtraEditors.SimpleButton

    Public Property VSandBoxType() As Integer
        Get
            Return m_VSandBoxType
        End Get
        Set(value As Integer)
            m_VSandBoxType = value
        End Set
    End Property
    Private m_VSandBoxType As Integer
    Public Property SourceObjectID() As String
        Get
            Return m_SourceObjectID
        End Get
        Set(value As String)
            m_SourceObjectID = value
        End Set
    End Property
    Private m_SourceObjectID As String
    Public Property CounterID() As String
        Get
            Return m_CounterID
        End Get
        Set(value As String)
            m_CounterID = value
        End Set
    End Property
    Private m_CounterID As String
    Public Property TimeAggregation() As String
        Get
            Return m_TimeAggregation
        End Get
        Set(value As String)
            m_TimeAggregation = value
        End Set
    End Property
    Private m_TimeAggregation As String
    Public Property ObjectAggregation() As String
        Get
            Return m_ObjectAggregation
        End Get
        Set(value As String)
            m_ObjectAggregation = value
        End Set
    End Property
    Private m_ObjectAggregation As String
    Public Property SQL_SourceTable() As List(Of String)
        Get
            Return m_SQL_SourceTable
        End Get
        Set(value As List(Of String))
            m_SQL_SourceTable = value
        End Set
    End Property
    Private m_SQL_SourceTable As List(Of String)
    Public Property SQL_KPI_ID() As String
        Get
            Return m_SQL_KPI_ID
        End Get
        Set(value As String)
            m_SQL_KPI_ID = value
        End Set
    End Property
    Private m_SQL_KPI_ID As String
    Public Property SQL_KPIFormula() As String
        Get
            Return m_SQL_KPIFormula
        End Get
        Set(value As String)
            m_SQL_KPIFormula = value
        End Set
    End Property
    Private m_SQL_KPIFormula As String
    Public Property ObjectTypeID() As String
        Get
            Return m_ObjectTypeID
        End Get
        Set(value As String)
            m_ObjectTypeID = value
        End Set
    End Property
    Private m_ObjectTypeID As String
    Public Property SortValue() As String
        Get
            Return m_SortValue
        End Get
        Set(value As String)
            m_SortValue = value
        End Set
    End Property
    Private m_SortValue As String
    Public Property CalculatedSeriesTypeID() As Integer
        Get
            Return m_CalculatedSeriesTypeID
        End Get
        Set(value As Integer)
            m_CalculatedSeriesTypeID = value
        End Set
    End Property
    Private m_CalculatedSeriesTypeID As Integer
    Public Property CalculatedSeriesParamValues() As String
        Get
            Return m_CalculatedSeriesParamValues
        End Get
        Set(value As String)
            m_CalculatedSeriesParamValues = value
        End Set
    End Property
    Private m_CalculatedSeriesParamValues As String

End Class
