Public Class SQLTechnologyMeasurements
    Inherits SQLCommanCommand
    Sub New()
        _tableName = DataBaseTableName.TBL_TECHNOLOGY_MEASUREMENTS
    End Sub
    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub


    'Public Shared Function SelectAll()
    '    Return "Select * from " & DataBaseTableName.TBL_TECHNOLOGY_MEASUREMENTS
    'End Function

    'Public Shared Function SelectAll(ByVal orderByColumn As String)
    '    Return SelectAll() & " order by " & orderByColumn
    'End Function

    'Public Shared Function SelectAll(ByVal withAlias As Boolean)
    '    Return SelectAll() & If(withAlias, " AS " & DataBaseTableName.TBL_TECHNOLOGY_MEASUREMENTS, "")
    'End Function

    'Public Shared Function SelectAll(ByVal orderByColumn As String, ByVal withAlias As Boolean)
    '    If (withAlias) Then
    '        Return SelectAll(withAlias) & " order by " & orderByColumn
    '    Else
    '        Return SelectAll() & " order by " & orderByColumn
    '    End If
    'End Function

    'Public Shared Function SelectAll(ByVal whereExpressionString As String, ByVal orderByColumn As String, ByVal withAlias As Boolean)
    '    If (withAlias) Then
    '        Return SelectAll(withAlias) & " Where " & whereExpressionString & " order by " & orderByColumn
    '    Else
    '        Return SelectAll() & " Where " & whereExpressionString & " order by " & orderByColumn
    '    End If
    'End Function
    'Public Shared Function SelectAll(ByVal withAlias As Boolean, ByVal whereExpressionString As String)
    '    If (withAlias) Then
    '        Return SelectAll(withAlias) & " Where " & whereExpressionString
    '    Else
    '        Return SelectAll() & " Where " & whereExpressionString
    '    End If
    'End Function


    Public Shared Function GetPrimaryKey(ByVal measurementIDAsSourcetable As String, ByVal MeasurementTables As String)
        Return "Exec " & StoreProcedurName.SP_GET_MEASUREMENT_PRIMARYKEY & " '" & measurementIDAsSourcetable & "'" & ", '" & MeasurementTables & "'"
    End Function


End Class
