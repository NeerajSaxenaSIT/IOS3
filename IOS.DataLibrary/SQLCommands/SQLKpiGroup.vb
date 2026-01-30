Public Class SQLKpiGroup
    Inherits SQLCommanCommand
    Sub New()
        _tableName = DataBaseTableName.TBL_KPI_GROUPS
    End Sub
    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
    Public Shared Function GetKPIGroupsByTech(ByVal technologyPackageId As String)
        Return "Exec " & StoreProcedurName.SP_GET_KPIGROUP & " " & technologyPackageId & ",'" & Environment.UserName.ToString() & "'; "
    End Function
    Public Shared Function InsertGroup(ByVal kpiGroupName As String, ByVal isKpiGroupPrivate As Boolean)
        Return "Exec " & StoreProcedurName.SP_KPIGROUP_CREATE & " '" & kpiGroupName & "'," & isKpiGroupPrivate & ",'" & Environment.UserName.ToString() & "'"
    End Function
    Public Shared Function ModifyGroup(ByVal kpiGroupID As String, ByVal kpiGroupName As String, ByVal isKpiGroupPrivate As Boolean)
        Return "Exec " & StoreProcedurName.SP_KPIGROUP_MODIFY & " " & kpiGroupID & "," & " '" & kpiGroupName & "'," & isKpiGroupPrivate & ",'" & Environment.UserName.ToString() & "'"
    End Function
    Public Shared Function RemoveKPIFromCategory(ByVal kpiCategoryID As String, ByVal kpiID As String)
        Return "Exec " & StoreProcedurName.SP_KPICATEGORY_REMOVE_KPI & " " & kpiCategoryID & "," & kpiID
    End Function
    Public Shared Function DeleteKPIFromDB(ByVal kpiID As String)
        Return "Exec " & StoreProcedurName.SP_KPICATEGORY_DELETE_KPI_DB & " " & kpiID
    End Function
    Public Shared Function DeleteKPIGroup(ByVal kpiGroupID As String)
        Return "Exec " & StoreProcedurName.SP_KPIGROUP_DELETE & " " & kpiGroupID & ",'" & Environment.UserName.ToString() & "'"
    End Function
    Public Shared Function GetKPIData()
        Return StoreProcedurName.QRY_GET_KPI_DATA
    End Function
End Class
