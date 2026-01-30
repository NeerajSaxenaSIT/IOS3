Public Class SQLReportContentSourceTime
    Inherits SQLCommanCommand
    Sub New()
        _tableName = StoreProcedurName.SP_REPORTCONTENT_SOURCETIME
    End Sub
    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    'Public Shared Function SelectAll()
    '    Return "Select * from " & DataBaseTableName.TBL_REPORTCONTENT_SOURCETIME_SP
    'End Function

    'Public Shared Function SelectAll(ByVal orderByColumn As String)
    '    Return SelectAll() & " order by " & orderByColumn
    'End Function

    'Public Shared Function SelectAll(ByVal withAlias As Boolean)
    '    Return SelectAll() & If(withAlias, " AS " & DataBaseTableName.TBL_REPORTCONTENT_SOURCETIME_SP, "")
    'End Function

    'Public Shared Function SelectAll(ByVal orderByColumn As String, ByVal withAlias As Boolean)
    '    If (withAlias) Then
    '        Return SelectAll(withAlias) & " order by " & orderByColumn
    '    Else
    '        Return SelectAll() & " order by " & orderByColumn
    '    End If
    'End Function

    Public Shared Function InsertReportContent_SourceTime(ByVal reportID As Integer, ByVal techpackId As Integer, ByVal sourceObjectID As Integer, ByVal objectTypeID As Integer, ByVal timeResolution As String, ByVal predefinedID As Integer,
                                                          ByVal timeManualStart As String, ByVal timeManualStop As String, ByVal cmOrpm As Integer, ByVal aggregrationOrSplit As Integer, ByVal topXValue As Integer) As String
        Dim sqlCommand As String = "Exec " & StoreProcedurName.SP_REPORTCONTENT_SOURCETIME & " " & reportID & "," & techpackId & "," & sourceObjectID & "," & objectTypeID & ",'" & timeResolution & "'," & predefinedID & ",?,?," & cmOrpm & "," & aggregrationOrSplit & "," & topXValue & ";"

        Return sqlCommand
    End Function

End Class
