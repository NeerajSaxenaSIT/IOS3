Public Class SelectionToTreeFlags

    Public Sub New(ByVal ran3g As Boolean, ByVal ran2g As Boolean, ByVal ranNanoBTS As Boolean, ByVal ranNano3G As Boolean, ByVal ran4G1 As Boolean, ByVal ran3g3 As Boolean, ByVal ran2g3 As Boolean)
        Me.ran2g = ran2g
        Me.ran3g = ran3g
        Me.ran2g3 = ran2g3
        Me.ran3g3 = ran3g3
        Me.ran4G1 = ran4G1
        Me.ran4G2 = ran4G2
        Me.ran4G3 = ran4G3
        Me.ran5G1 = ran5G1
        Me.ran5G2 = ran5G2
        Me.ran5G3 = ran5G3
        Me.ranNano3G = ranNano3G
        Me.ranNanoBTS = ranNanoBTS
    End Sub

    Public Sub New()
        Me.ran2g = False
        Me.ran3g = False
        Me.ran2g3 = False
        Me.ran3g3 = False
        Me.ran4G1 = False
        Me.ran4G2 = False
        Me.ran4G3 = False
        Me.ran5G1 = False
        Me.ran5G2 = False
        Me.ran5G3 = False
        Me.ranNode1 = False
        Me.ranNode2 = False
        Me.ranNode3 = False
        Me.ranNano3G = False
        Me.ranNanoBTS = False
        Me.tx = False
        Me.transport = False
        Me.ranCommon = False
        Me.pdum = False
        Me.twamp = False
    End Sub

    Public ran3g As Boolean = False
    Public ran2g As Boolean = False
    Public ranNanoBTS As Boolean = False
    Public ranNano3G As Boolean = False
    Public ran4G1 As Boolean = False
    Public ran4G2 As Boolean = False
    Public ran4G3 As Boolean = False
    Public ran5G1 As Boolean = False
    Public ran5G2 As Boolean = False
    Public ran5G3 As Boolean = False
    Public ranNode1 As Boolean = False
    Public ranNode2 As Boolean = False
    Public ranNode3 As Boolean = False
    Public ran3g3 As Boolean = False
    Public ran2g3 As Boolean = False
    Public tx As Boolean = False
    Public transport As Boolean = False
    Public ranCommon As Boolean = False
    Public pdum As Boolean = False
    Public twamp As Boolean = False

End Class
