Public Class SQLKpiCategory
    Inherits SQLCommanCommand
    Sub New()
        _tableName = DataBaseTableName.TBL_KPI_CATEGORY
    End Sub
    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
    Public Shared Function InsertCategory(ByVal kpiGroupID As String, ByVal kpiCategoryName As String)
        Return "Exec " & StoreProcedurName.SP_KPICATEGORY_CREATE & " " & kpiGroupID & ",'" & kpiCategoryName & "'"
    End Function
    Public Shared Function AddKpiWithCategory(ByVal kpiCategoryID As String, ByVal kpiID As String)
        Return "Exec " & StoreProcedurName.SP_KPICATEGORY_ADD_KPI & " " & kpiCategoryID & "," & kpiID & ""
    End Function
    Public Shared Function ModifyCategory(ByVal kpiCategoryID As String, ByVal kpiCategoryName As String)
        Return "Exec " & StoreProcedurName.SP_KPICATEGORY_MODIFY & " " & kpiCategoryID & ",'" & kpiCategoryName & "'"
    End Function
    Public Shared Function DeleteCategory(ByVal kpiCategoryID As String, ByVal kpiGroupID As String)
        Return "Exec " & StoreProcedurName.SP_KPICATEGORY_DELETE & " " & kpiCategoryID & "," & kpiGroupID & ""
    End Function
End Class
