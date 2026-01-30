Public Class SQLReportContentDimensions
    Inherits SQLCommanCommand
    Sub New()
        _tableName = StoreProcedurName.SP_REPORTCONTENT_DIMENSIONS
    End Sub
    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
    'Public Shared Function SelectAll() As String
    '    Return "Select * from " & DataBaseTableName.TBL_REPORTCONTENT_DIMENSIONS_SP
    'End Function

    'Public Shared Function SelectAll(ByVal orderByColumn As String) As String
    '    Return SelectAll() & " order by " & orderByColumn
    'End Function

    'Public Shared Function SelectAll(ByVal withAlias As Boolean) As String
    '    Return SelectAll() & If(withAlias, " AS " & DataBaseTableName.TBL_REPORTCONTENT_DIMENSIONS_SP, "")
    'End Function

    'Public Shared Function SelectAll(ByVal orderByColumn As String, ByVal withAlias As Boolean) As String
    '    If (withAlias) Then
    '        Return SelectAll(withAlias) & " order by " & orderByColumn
    '    Else
    '        Return SelectAll() & " order by " & orderByColumn
    '    End If
    'End Function

    Public Shared Function InsertReportContent_Dimensions(ByVal reportId As String, ByVal dimensionAxis As Integer, ByVal sandBoxFieldType As Integer, ByVal counterID As String, ByVal kpiID As String,
                                                          ByVal objectTypeId As String, ByVal sortOrder As String, ByVal DimensionName As String) As String
        Return "Exec " & StoreProcedurName.SP_REPORTCONTENT_DIMENSIONS & " " & reportId & "," & dimensionAxis & "," & sandBoxFieldType & ",'" & counterID & "','" & kpiID & "','" & objectTypeId & "','" & sortOrder & "','" & DimensionName & "'"
    End Function
End Class
