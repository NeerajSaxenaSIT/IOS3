Public Class clsComboBoxItem
    Implements IComparable

    Private _tag As String
    Private _text As String
    Private _key As Object
    Private _enable As Boolean
    Private _ischecked As Boolean
    Private _sorted As Boolean

    Public Sub New()
        _tag = Nothing
        _text = ""
        _enable = True
        _key = Nothing
    End Sub

    Public Sub New(itemText As String, itemValue As Object, Optional itemTag As String = Nothing)
        _tag = itemTag
        _text = itemText
        _enable = True
        _key = itemValue
    End Sub

    Public Property Tag() As String
        Get
            Return _tag
        End Get
        Set(ByVal value As String)
            _tag = value
        End Set
    End Property

    Public Property Value() As Object
        Get
            Return _key
        End Get
        Set(ByVal value As Object)
            _key = value
        End Set
    End Property

    Public Property Text() As String
        Get
            Return _text
        End Get
        Set(ByVal value As String)
            _text = value
        End Set
    End Property

    Public Property Enabled() As Boolean
        Get
            Return _enable
        End Get
        Set(ByVal value As Boolean)
            _enable = value
        End Set
    End Property

    Public Property IsChecked As Boolean
        Get
            Return _ischecked
        End Get
        Set(ByVal value As Boolean)
            _ischecked = value
        End Set
    End Property

    Public Property Sorted() As Boolean
        Get
            Return _sorted
        End Get
        Set(value As Boolean)
            _sorted = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return _text
    End Function

    Public Function CompareTo(other As [Object]) As Integer Implements IComparable.CompareTo
        If Sorted Then
            Return Comparer.[Default].Compare(ToString(), other.ToString())
        End If
        Return 0
    End Function

End Class