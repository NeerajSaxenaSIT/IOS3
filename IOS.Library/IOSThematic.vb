Public Class IOSThematic

    Dim _thematicID As Integer
    Public Property ThematicID() As Integer
        Get
            Return _thematicID
        End Get
        Set(value As Integer)
            _thematicID = value
        End Set
    End Property
    Dim _bins As Integer
    Public Property Bins() As Integer
        Get
            Return _bins
        End Get
        Set(value As Integer)
            _bins = value
        End Set
    End Property
    Dim _objectName As String
    Public Property ObjectName() As String
        Get
            Return _objectName
        End Get
        Set(value As String)
            _objectName = value
        End Set
    End Property
    Dim _thematicType As String
    Public Property ThematicType() As String
        Get
            Return _thematicType
        End Get
        Set(value As String)
            _thematicType = value
        End Set
    End Property
    Dim _kpiID As Integer
    Public Property KPIID() As Integer
        Get
            Return _kpiID
        End Get
        Set(value As Integer)
            _kpiID = value
        End Set
    End Property
    Dim _kpiTypeID As Integer
    Public Property KPITypeID() As Integer
        Get
            Return _kpiTypeID
        End Get
        Set(value As Integer)
            _kpiTypeID = value
        End Set
    End Property
    Dim _targetObject As String
    Public Property TargetObject() As String
        Get
            Return _targetObject
        End Get
        Set(value As String)
            _targetObject = value
        End Set
    End Property
    Dim _distributionMethod As String
    Public Property DistributionMethod() As String
        Get
            Return _distributionMethod
        End Get
        Set(value As String)
            _distributionMethod = value
        End Set
    End Property
    Dim _roundBy As Double
    Public Property RoundBy() As Double
        Get
            Return _roundBy
        End Get
        Set(value As Double)
            _roundBy = value
        End Set
    End Property
    'Dim _kpiSetID As Integer
    'Public Property KPISetID() As Integer
    '    Get
    '        Return _kpiSetID
    '    End Get
    '    Set(value As Integer)
    '        _kpiSetID = value
    '    End Set
    'End Property
    Dim _isGraduated As Boolean
    Public Property IsGraduated() As Boolean
        Get
            Return _isGraduated
        End Get
        Set(value As Boolean)
            _isGraduated = value
        End Set
    End Property
    Dim _isHalfPie As Boolean
    Public Property IsHalfPie() As Boolean
        Get
            Return _isHalfPie
        End Get
        Set(value As Boolean)
            _isHalfPie = value
        End Set
    End Property
    Dim _pieDiameter As Double
    Public Property PieDiameter() As Double
        Get
            Return _pieDiameter
        End Get
        Set(value As Double)
            _pieDiameter = value
        End Set
    End Property
End Class


Public Class IOSThematicBins
    Dim _thematicID As Integer
    Public Property ThematicID() As Integer
        Get
            Return _thematicID
        End Get
        Set(value As Integer)
            _thematicID = value
        End Set
    End Property
    Dim _regionColor As String
    Public Property regionColor() As String
        Get
            Return _regionColor
        End Get
        Set(value As String)
            _regionColor = value
        End Set
    End Property
    Dim _regionTransparency As String
    Public Property RegionTransparency() As String
        Get
            Return _regionTransparency
        End Get
        Set(value As String)
            _regionTransparency = value
        End Set
    End Property
    Dim _regionInteriorType As String
    Public Property RegionInteriorType() As String
        Get
            Return _regionInteriorType
        End Get
        Set(value As String)
            _regionInteriorType = value
        End Set
    End Property
    Dim _regionBorderLineWidth As Integer
    Public Property RegionBorderLineWidth() As Integer
        Get
            Return _regionBorderLineWidth
        End Get
        Set(value As Integer)
            _regionBorderLineWidth = value
        End Set
    End Property
    Dim _rangeMin As Double
    Public Property RangeMin() As Double
        Get
            Return _rangeMin
        End Get
        Set(value As Double)
            _rangeMin = value
        End Set
    End Property
    Dim _rangeMax As Double
    Public Property RangeMax() As Double
        Get
            Return _rangeMax
        End Get
        Set(value As Double)
            _rangeMax = value
        End Set
    End Property
    Dim _regionBorderLineTransparancy As Integer = 0
    Public Property RegionBorderLineTransparancy() As Integer
        Get
            Return _regionBorderLineTransparancy
        End Get
        Set(value As Integer)
            _regionBorderLineTransparancy = value
        End Set
    End Property
    Dim _symbolNumber As Integer = 0
    Public Property SymbolNumber() As Integer
        Get
            Return _symbolNumber
        End Get
        Set(value As Integer)
            _symbolNumber = value
        End Set
    End Property
    Dim _symbolSize As Integer = 0
    Public Property SymbolSize() As Integer
        Get
            Return _symbolSize
        End Get
        Set(value As Integer)
            _symbolSize = value
        End Set
    End Property
    Dim _symbolInteriorColor As Integer = 0
    Public Property SymbolInteriorColor() As Integer
        Get
            Return _symbolInteriorColor
        End Get
        Set(value As Integer)
            _symbolInteriorColor = value
        End Set
    End Property
    Dim _symbolBorderColor As Integer = 0
    Public Property SymbolBorderColor() As Integer
        Get
            Return _symbolBorderColor
        End Get
        Set(value As Integer)
            _symbolBorderColor = value
        End Set
    End Property
    Dim _symbolInteriorTransparancy As Integer = 0
    Public Property SymbolInteriorTransparancy() As Integer
        Get
            Return _symbolInteriorTransparancy
        End Get
        Set(value As Integer)
            _symbolInteriorTransparancy = value
        End Set
    End Property
    Dim _symbolBorderTransparancy As Integer = 0
    Public Property SymbolBorderTransparancy() As Integer
        Get
            Return _symbolBorderTransparancy
        End Get
        Set(value As Integer)
            _symbolBorderTransparancy = value
        End Set
    End Property
    Dim _individualValue As String
    Public Property IndividualValue() As String
        Get
            Return _symbolBorderTransparancy
        End Get
        Set(value As String)
            _symbolBorderTransparancy = value
        End Set
    End Property
End Class
