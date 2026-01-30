Public Class ICMGridInfo

    Shared gridList As New Dictionary(Of String, DevExpress.XtraGrid.GridControl)()
    Public Shared Sub SetGrid(shortName As String, vdg As DevExpress.XtraGrid.GridControl)
        If gridList.Keys.Contains(shortName) Then
            gridList.Remove(shortName)
        Else
            gridList.Add(shortName, vdg)
        End If
    End Sub

    Public Shared Function GetGrid(shortname As String) As DevExpress.XtraGrid.GridControl
        Return gridList.FirstOrDefault(Function(x) x.Key = shortname).Value
    End Function

    Public Shared Sub ClearAllGrid()
        For Each gList As KeyValuePair(Of String, DevExpress.XtraGrid.GridControl) In gridList
            gList.Value.DataSource = Nothing
        Next
    End Sub

End Class
