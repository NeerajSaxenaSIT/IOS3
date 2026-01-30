Imports DevExpress.XtraTab
Imports dotnetCHARTING.WinForms

Public Class ICMTab
    Inherits XtraTabPage

    Private _ChartA As Chart
    Public Property ChartA() As Chart
        Get
            Return _ChartA
        End Get
        Set(ByVal value As Chart)
            _ChartA = value
        End Set
    End Property

    Private _ChartB As Chart
    Public Property ChartB() As Chart
        Get
            Return _ChartB
        End Get
        Set(ByVal value As Chart)
            _ChartB = value
        End Set
    End Property

    Private _dgv As DevExpress.XtraGrid.GridControl
    Public Property vdgv() As DevExpress.XtraGrid.GridControl
        Get
            Return _dgv
        End Get
        Set(ByVal value As DevExpress.XtraGrid.GridControl)
            _dgv = value
        End Set
    End Property

End Class
