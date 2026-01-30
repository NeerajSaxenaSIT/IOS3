Imports IOS.DataLibrary
Imports IOS.Configuration
Imports IOS.Configuration.EntityModel

Public Class SandBoxTable

    Public Shared Function GetVSandBoxObjectForTimer(ByRef dt As DataTable, ByVal filtter As String) As SandBoxFieldModel
        Dim SandBoxFieldModel As SandBoxFieldModel = New SandBoxFieldModel()
        Dim sourceObjectID As Integer = 0
        Dim dtFilter As DataTable = dt.SelectedRowsAsTable(filtter)
        If (dtFilter.IsValid) Then
            SandBoxFieldModel.VSandBoxType = DatamartFieldType.Time
            ''SandBoxFieldModel.
        End If
        Return SandBoxFieldModel
    End Function
    Public Shared Function GetVSandBoxObjectForKPI(ByRef dt As DataTable, ByVal filtter As String) As SandBoxFieldModel
        Dim SandBoxFieldModel As SandBoxFieldModel = New SandBoxFieldModel()
        Dim sourceObjectID As Integer = 0
        Dim dtFilter As DataTable = dt.SelectedRowsAsTable(filtter)
        If (dtFilter.IsValid) Then
            SandBoxFieldModel.VSandBoxType = DatamartFieldType.Kpi
            Try
                SandBoxFieldModel.SourceObjectID = dtFilter.DistinctCol(TechnologyPackageKPIFields.SOURCE_OBJECT_ID).Rows(0)(0)
            Catch ex As Exception

            End Try

            Try

                ''Dim asss As String = dtFilter.DistinctCol(TechnologyPackageCountersFields.SQL_SOURCE_TABLE).Rows(0)(0).ToString.Replace("[", "").Replace("]", "")
                Dim sqlSourceTable As List(Of String) = New List(Of String)
                If (dtFilter.Rows.Count > 0) Then
                    For Each dr As DataRow In dtFilter.Rows
                        Dim sqlST As String = dr(TechnologyPackageKPIFields.SQL_SOURCE_TABLE)
                        If (sqlST.Length > 0 AndAlso sqlST IsNot Nothing AndAlso Not sqlSourceTable.Contains(sqlST)) Then
                            sqlSourceTable.Add(sqlST)
                        End If

                    Next
                End If

                SandBoxFieldModel.SQL_SourceTable = sqlSourceTable ''dtFilter.DistinctCol(TechnologyPackageKPIFields.SQL_SOURCE_TABLE).Rows(0)(0)
            Catch ex As Exception

            End Try

            Try
                SandBoxFieldModel.SQL_KPI_ID = dtFilter.DistinctCol(TechnologyPackageKPIFields.KPI_ID).Rows(0)(0)
            Catch ex As Exception

            End Try
            Try
                SandBoxFieldModel.SQL_KPIFormula = dtFilter.DistinctCol(TechnologyPackageKPIFields.KPI_SQL).Rows(0)(0)
            Catch ex As Exception

            End Try
        End If
        Return SandBoxFieldModel
    End Function
    Public Shared Function GetVSandBoxObjectForMeasurment(ByRef dt As DataTable, ByVal filtter As String) As SandBoxFieldModel
        Dim SandBoxFieldModel As SandBoxFieldModel = New SandBoxFieldModel()
        Dim sourceObjectID As Integer = 0
        Dim dtFilter As DataTable = dt.SelectedRowsAsTable(filtter)
        If (dtFilter.IsValid) Then
            SandBoxFieldModel.VSandBoxType = DatamartFieldType.Counter
            Try
                SandBoxFieldModel.SourceObjectID = dtFilter.DistinctCol(TechnologyPackageCountersFields.SOURCE_OBJECT_ID).Rows(0)(0)
            Catch ex As Exception

            End Try
            Try
                SandBoxFieldModel.ObjectAggregation = dtFilter.DistinctCol(TechnologyPackageCountersFields.OBJECT_AGGREGATION_FORMULA).Rows(0)(0)
            Catch ex As Exception

            End Try
            Try
                SandBoxFieldModel.SQL_SourceTable = dtFilter.DistinctCol(TechnologyPackageCountersFields.SQL_SOURCE_TABLE).Rows(0)(0)
            Catch ex As Exception

            End Try
        End If
        Return SandBoxFieldModel
    End Function
    Public Shared Function GetVSandBoxObjectForCounter(ByRef dt As DataTable, ByVal filtter As String) As SandBoxFieldModel
        Dim SandBoxFieldModel As SandBoxFieldModel = New SandBoxFieldModel()
        Dim sourceObjectID As Integer = 0
        Dim dtFilter As DataTable = dt.SelectedRowsAsTable(filtter)
        If (dtFilter.IsValid) Then
            SandBoxFieldModel.VSandBoxType = DatamartFieldType.Counter
            Try
                SandBoxFieldModel.SourceObjectID = dtFilter.DistinctCol(TechnologyPackageCountersFields.SOURCE_OBJECT_ID).Rows(0)(0)
            Catch ex As Exception

            End Try
            Try
                SandBoxFieldModel.TimeAggregation = dtFilter.DistinctCol(TechnologyPackageCountersFields.TIME_AGGREGATION_FORMULA).Rows(0)(0)
            Catch ex As Exception

            End Try
            Try
                SandBoxFieldModel.ObjectAggregation = dtFilter.DistinctCol(TechnologyPackageCountersFields.OBJECT_AGGREGATION_FORMULA).Rows(0)(0)
            Catch ex As Exception

            End Try
            Try
                Dim asss As String = dtFilter.DistinctCol(TechnologyPackageCountersFields.SQL_SOURCE_TABLE).Rows(0)(0).ToString '.Replace("[", "").Replace("]", "")
                Dim disss As List(Of String) = New List(Of String)
                disss.Add(asss)
                SandBoxFieldModel.SQL_SourceTable = disss
            Catch ex As Exception

            End Try
        End If
        Return SandBoxFieldModel
    End Function
    Public Shared Function GetVSandBoxObjectForObject(ByRef dt As DataTable, ByVal filtter As String) As SandBoxFieldModel
        Dim SandBoxFieldModel As SandBoxFieldModel = New SandBoxFieldModel()
        Dim sourceObjectID As Integer = 0
        Dim dtFilter As DataTable = dt.SelectedRowsAsTable(filtter)
        If (dtFilter.IsValid) Then
            SandBoxFieldModel.VSandBoxType = DatamartFieldType.Counter
            Try
                SandBoxFieldModel.SourceObjectID = dtFilter.DistinctCol(TechnologyPackageCountersFields.SOURCE_OBJECT_ID).Rows(0)(0)
            Catch ex As Exception

            End Try
            Try
                SandBoxFieldModel.SQL_KPI_ID = "NULL"
            Catch ex As Exception

            End Try
            Try
                SandBoxFieldModel.SQL_KPIFormula = "NULL"
            Catch ex As Exception

            End Try


            'Try
            '    Dim asss As String = dtFilter.DistinctCol(TechnologyPackageCountersFields.SQL_SOURCE_TABLE).Rows(0)(0).ToString.Replace("[", "").Replace("]", "")
            '    Dim disss As List(Of String) = New List(Of String)
            '    disss.Add(asss)
            '    SandBoxFieldModel.SQL_SourceTable = disss
            'Catch ex As Exception

            'End Try
        End If
        Return SandBoxFieldModel
    End Function

    Public Shared Function GetSourceObjectID(ByRef dt As DataTable, ByVal filtter As String) As Integer
        Dim sourceObjectID As Integer = 0
        Dim dtFilter As DataTable = dt.SelectedRowsAsTable(filtter)
        If (dtFilter.IsValid) Then
            sourceObjectID = dtFilter.DistinctCol(TechnologyPackageCountersFields.SOURCE_OBJECT_ID).Rows(0)(0)
        End If
        Return sourceObjectID
    End Function
    Public Shared Function GetObjectAggregration(ByRef dt As DataTable, ByVal filtter As String) As String
        Dim objectAggregration As String = 0
        Dim dtFilter As DataTable = dt.SelectedRowsAsTable(filtter)
        If (dtFilter.IsValid) Then
            objectAggregration = dtFilter.DistinctCol(TechnologyPackageCountersFields.OBJECT_AGGREGATION_CONFIGID).Rows(0)(0)
        End If
        Return objectAggregration
    End Function
    Public Shared Function GetSQLSourceTable(ByRef dt As DataTable, ByVal filtter As String) As String
        Dim objectAggregration As String = 0
        Dim dtFilter As DataTable = dt.SelectedRowsAsTable(filtter)
        If (dtFilter.IsValid) Then
            objectAggregration = dtFilter.DistinctCol(TechnologyPackageCountersFields.SQL_SOURCE_TABLE).Rows(0)(0)
        End If
        Return objectAggregration
    End Function
End Class
