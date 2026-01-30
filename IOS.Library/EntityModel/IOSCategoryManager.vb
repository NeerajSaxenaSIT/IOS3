Public Class IOSCategoryManager
    Private _returnEndDate As Date
    Private _returnStartDate As Date
    Private _returnCategoryID As Integer
    Private _returnCategoryName As String
    Private _returnSchdule As Boolean
    Private _returnSchduleType As Integer
    Private _isApplyTo As String
    Private _lastSchedule As String
    Public Const BY_OBJECT As String = "byObject"
    Public Const BY_GRID As String = "byGrid"
    Public Const BY_SELECTION As String = "bySelection"
    Public Const BYSCHEDULE_UPDATE As String = "ScheduleUpdate"
    Public Const VENDER_ID As String = "VendorID"
    Public Const OBJECT_ID As String = "ObjectID"
    Public Const OBJECT_NAME As String = "ObjectName"
    Public Const OBJECT_TYPE As String = "ObjectType"
    Public Const CATEGORY_ID As String = "CategoryID"
    Public Const CATEGORY_NAME As String = "CategoryName"
    Public Const IS_NEWROW As String = "IsNewRow"
    Public Const IS_UPDATED As String = "IsUpdated"
    Private _returnChartCategoryName As String
    Public Const HAS_SCHEDULE As String = "HasSchedule"

    Public Property GetEndDate() As Date
        Get
            Return _returnEndDate
        End Get
        Set(ByVal value As Date)
            _returnEndDate = value
        End Set
    End Property

    Public Property GetStartDate() As Date
        Get
            Return _returnStartDate
        End Get
        Set(ByVal value As Date)
            _returnStartDate = value
        End Set
    End Property
    Public Property GetCategoryID() As Integer
        Get
            Return _returnCategoryID
        End Get
        Set(ByVal value As Integer)
            _returnCategoryID = value
        End Set
    End Property
    Public Property GetCategoryName() As String
        Get
            Return _returnCategoryName
        End Get
        Set(ByVal value As String)
            _returnCategoryName = value
        End Set
    End Property
    Public Property IsSchdule() As Boolean
        Get
            Return _returnSchdule
        End Get
        Set(ByVal value As Boolean)
            _returnSchdule = value
        End Set
    End Property
    Public Property IsApplyTo() As String
        Get
            Return _isApplyTo
        End Get
        Set(ByVal value As String)
            _isApplyTo = value
        End Set
    End Property
    Public Property GetSchduleType() As Integer
        Get
            Return _returnSchduleType
        End Get
        Set(ByVal value As Integer)
            _returnSchduleType = value
        End Set
    End Property

    Public Property GetChartCategoryName() As Integer
        Get
            Return _returnChartCategoryName
        End Get
        Set(ByVal value As Integer)
            _returnChartCategoryName = value
        End Set
    End Property
End Class
