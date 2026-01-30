Public Class SQLTechnologyObjectTypes
    Inherits SQLCommanCommand
    Sub New()
        _tableName = DataBaseTableName.TBL_TECHNOLOGY_OBJECTTYPES
    End Sub
    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
    'Public Shared Function SelectAll()
    '    Return "Select * from " & DataBaseTableName.TBL_TECHNOLOGY_OBJECTTYPES
    'End Function

    'Public Shared Function SelectAll(ByVal orderByColumn As String)
    '    Return SelectAll() & " order by " & orderByColumn
    'End Function

    'Public Shared Function SelectAll(ByVal withAlias As Boolean)
    '    Return SelectAll() & If(withAlias, " AS " & DataBaseTableName.TBL_TECHNOLOGY_OBJECTTYPES, "")
    'End Function

    'Public Shared Function SelectAll(ByVal orderByColumn As String, ByVal withAlias As Boolean)
    '    If (withAlias) Then
    '        Return SelectAll(withAlias) & " order by " & orderByColumn
    '    Else
    '        Return SelectAll() & " order by " & orderByColumn
    '    End If
    'End Function

    Public Shared Function GetObjectSQLForCMTree(ByVal objectTypeID As Integer)
        Return "EXEC " & StoreProcedurName.SP_OBJECTTREE_CM & objectTypeID
    End Function
    Public Shared Function GetObjectSQLForPMTree(ByVal objectTypeID As Integer, ByVal techpackId As Integer)
        Return "EXEC " & StoreProcedurName.SP_OBJECTTREE_PM & objectTypeID & "," & techpackId
    End Function
    Public Shared Function GetObjectViewCMorPM(ByVal objectTypeIDs As String, ByVal techpackId As Integer)
        Return "EXEC " & StoreProcedurName.SP_GETOBJECTVIEW_CMORPM & " '" & objectTypeIDs & "'" & "," & techpackId
    End Function
    Public Shared Function GetDimensionsForSource(ByVal trSrc As String, ByVal objType As String)
        Return "EXEC " & StoreProcedurName.SP_GETDIMENSIONS_CMORPM & " '" & trSrc & "'" & ",'" & objType & "'"
    End Function

End Class
