Public Class SQLTechnologyKPIs
    Inherits SQLCommanCommand
    Sub New()
        _tableName = DataBaseTableName.TBL_TECHNOLOGY_KPIS
    End Sub
    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
    Public Shared Function GetByTechAndCreator(ByVal technologyPackageId As String, ByVal kpiGroup As String)
        Return "Exec " & StoreProcedurName.SP_TECH_KPI_GETBY_TECHANDCREATOR & " " & technologyPackageId & ",'" & Environment.UserName.ToString() & "','" & kpiGroup & "'; "
    End Function

    Public Shared Function Insert(ByVal technologyPackageID As String, ByVal kPIName As String, ByVal kPIDescription As String, ByVal kPISQL As String, ByVal isPrivate As Boolean, ByVal kpiGroupID As Integer)
        Return "Exec " & StoreProcedurName.SP_TECH_KPI_INSERT & " " & technologyPackageID & ",'" & kPIName & "' , '" & kPIDescription & "', '" & kPISQL & "', '" & System.Environment.UserName.ToString() & "','" & isPrivate & "'," & kpiGroupID
    End Function
    Public Shared Function Update(ByVal kpiID As String, ByVal technologyPackageID As String, ByVal kPIName As String, ByVal kPIDescription As String, ByVal kPISQL As String, ByVal isPrivate As Boolean)
        Return "Exec " & StoreProcedurName.SP_TECH_KPI_UPDATE & " " & kpiID & "," & technologyPackageID & ",'" & kPIName & "' , '" & kPIDescription & "', '" & kPISQL & "', '" & System.Environment.UserName.ToString() & "','" & isPrivate & "'"
    End Function

    Public Shared Function InsertSourceMeasurements(ByVal kpiID As String, ByVal measurementID As String)
        Return "INSERT INTO " & DataBaseTableName.TBL_TECHNOLOGY_KPI_SOURCEMEASUREMENTS & "(KPIID,MeasurementID) Values (" & kpiID & "," & measurementID & ");"
    End Function
    Public Shared Function DeleteKPIFromSourceMeasurements(ByVal kpiID As String)
        Return "DELETE " & DataBaseTableName.TBL_TECHNOLOGY_KPI_SOURCEMEASUREMENTS & " WHERE KPIID=" & kpiID & ";"
    End Function

    Public Shared Function DELETEKPI(ByVal KPIID As String, ByVal Creator As String)
        Return "Exec " & StoreProcedurName.SP_TECH_KPI_DELETE & " '" & KPIID & "','" & Creator & "'"
    End Function
End Class
