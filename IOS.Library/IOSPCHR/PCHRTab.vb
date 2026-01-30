

Public Class PCHRTab
    Public Enum OverviewChartErrorType
        Type
        Reason
        Cause
        ReleaseCause

    End Enum
    Public Enum OverviewGridType
        CS
        PS
    End Enum

    Public Function CSOverviewChartLINQ_ErrorType(ByRef dt As DataTable) As DataTable
        Dim dt1 As DataTable = New DataTable
        Dim dt2 As DataTable = New DataTable
        Try
            Dim query1ForErrorType = From detalle In dt.AsEnumerable() Where detalle.Field(Of String)("FaultType") = "CS_RAB_DROP" _
    Group detalle By grupoClave = New With _
                    { _
                        Key .FaultType = detalle("faultType"), _
                        Key .CS_Rab_Release_Type = detalle("CS_Rab_Release_Type")} Into g = Group _
                Select New With _
                { _
                    .FaultType = g(0).Field(Of String)("faultType"), _
                    .CS_Rab_Release_Type = g(0).Field(Of String)("CS_Rab_Release_Type"), _
                    .Counter = g.Count() _
                }
            Dim query2ForErrorType = From detalle In dt.AsEnumerable() Where detalle.Field(Of String)("FaultType") = "CS_RAB_SETUP_FAIL" _
                               Group detalle By grupoClave = New With _
                                                           { _
                                                               Key .FaultType = detalle("faultType"), _
                                                               Key .CS_Rab_Release_Type = detalle("CsSetupErrorType")} Into g = Group _
                                                       Select New With _
                                                       { _
                                                           .FaultType = g(0).Field(Of String)("faultType"), _
                                                           .CS_Rab_Release_Type = g(0).Field(Of String)("CsSetupErrorType"), _
                                                           .Counter = g.Count() _
                                                       }

            dt1 = ConvertOverviewLINQueryToDataTable(query1ForErrorType)
            dt2 = ConvertOverviewLINQueryToDataTable(query2ForErrorType)
            dt1.Merge(dt2)
        Catch ex As Exception

        End Try
        Return dt1

    End Function
    Public Function CSOverviewChartLINQ_ErrorReason(ByRef dt As DataTable) As DataTable
        Dim dt1 As DataTable = New DataTable
        Dim dt2 As DataTable = New DataTable
        Try

            Dim query1ForReason = From detalle In dt.AsEnumerable() Where detalle.Field(Of String)("FaultType") = "CS_RAB_DROP"
                                   Group detalle By grupoClave = New With _
                                                           { _
                                                               Key .FaultType = detalle("faultType"), _
                                                               Key .CS_Rab_Release_Reason = detalle("CS_Rab_Release_Reason")} Into g = Group _
                                                       Select New With _
                                                       { _
                                                           .FaultType = g(0).Field(Of String)("faultType"), _
                                                           .CS_Rab_Release_Reason = g(0).Field(Of String)("CS_Rab_Release_Reason"), _
                                                           .Counter = g.Count() _
                                                       }

            Dim query2ForReason = From detalle In dt.AsEnumerable() Where detalle.Field(Of String)("FaultType") = "CS_RAB_SETUP_FAIL"
                                   Group detalle By grupoClave = New With _
                                                           { _
                                                               Key .FaultType = detalle("faultType"), _
                                                               Key .CS_Rab_Release_Reason = detalle("CsSetupErrorReason")} Into g = Group _
                                                       Select New With _
                                                       { _
                                                           .FaultType = g(0).Field(Of String)("faultType"), _
                                                           .CS_Rab_Release_Reason = g(0).Field(Of String)("CsSetupErrorReason"), _
                                                           .Counter = g.Count() _
                                                       }
            dt1 = ConvertOverviewLINQueryToDataTable(query1ForReason)
            dt2 = ConvertOverviewLINQueryToDataTable(query2ForReason)
            dt1.Merge(dt2)
        Catch ex As Exception

        End Try
        Return dt1
    End Function
    Public Function CSOverviewChartLINQ_ErrorCause(ByRef dt As DataTable) As DataTable
        Dim dt1 As DataTable = New DataTable
        Dim dt2 As DataTable = New DataTable
        Try


            Dim query1ForCause = From detalle In dt.AsEnumerable() Where detalle.Field(Of String)("FaultType") = "CS_RAB_DROP"
                                   Group detalle By grupoClave = New With _
                                                               { _
                                                                   Key .FaultType = detalle("faultType"), _
                                                                   Key .CS_Rab_Release_Cause = detalle("CS_Rab_Release_Cause")} Into g = Group _
                                                           Select New With _
                                                           { _
                                                               .FaultType = g(0).Field(Of String)("faultType"), _
                                                               .CS_Rab_Release_Cause = g(0).Field(Of String)("CS_Rab_Release_Cause"), _
                                                               .Counter = g.Count() _
                                                           }
            Dim query2ForCause = From detalle In dt.AsEnumerable() Where detalle.Field(Of String)("FaultType") = "CS_RAB_SETUP_FAIL"
                                  Group detalle By grupoClave = New With _
                                                              { _
                                                                  Key .FaultType = detalle("faultType"), _
                                                                  Key .CS_Rab_Release_Cause = detalle("CsSetupErrorCause")} Into g = Group _
                                                          Select New With _
                                                          { _
                                                              .FaultType = g(0).Field(Of String)("faultType"), _
                                                              .CS_Rab_Release_Cause = g(0).Field(Of String)("CsSetupErrorCause"), _
                                                              .Counter = g.Count() _
                                                          }
            dt1 = ConvertOverviewLINQueryToDataTable(query1ForCause)
            dt2 = ConvertOverviewLINQueryToDataTable(query2ForCause)
            dt1.Merge(dt2)
        Catch ex As Exception

        End Try
        Return dt1
    End Function
    Public Function CSOverviewChartLINQ_ReleaseCause(ByRef dt As DataTable) As DataTable
        Dim dt1 As DataTable = New DataTable
        Try


            Dim queryForReleaseCause = From detalle In dt.AsEnumerable() _
                                        Group detalle By grupoClave = New With _
                                                               { _
                                                                   Key .FaultType = detalle("faultType"), _
                                                                   Key .IU_Release_Cause = detalle("IU_Release_Cause")} Into g = Group _
                                                           Select New With _
                                                           { _
                                                               .FaultType = g(0).Field(Of String)("faultType"), _
                                                               .IU_Release_Cause = g(0).Field(Of String)("IU_Release_Cause"), _
                                                               .Counter = g.Count() _
                }
            dt1 = ConvertOverviewLINQueryToDataTable(queryForReleaseCause)
        Catch ex As Exception

        End Try
        Return dt1
    End Function

    Public Function PSOverviewChartLINQ_ErrorType(ByRef dt As DataTable) As DataTable
        Dim dt1 As DataTable = New DataTable
        Dim dt2 As DataTable = New DataTable
        Try
            Dim query1ForReason = From detalle In dt.AsEnumerable() Where detalle.Field(Of String)("FaultType") = "PS_RAB_DROP" _
                               Group detalle By grupoClave = New With _
                                                           { _
                                                               Key .FaultType = detalle("faultType"), _
                                                               Key .PS_Rab_Release_Type = detalle("PS_Rab_Release_Type")} Into g = Group _
                                                       Select New With _
                                                       { _
                                                           .FaultType = g(0).Field(Of String)("faultType"), _
                                                           .PS_Rab_Release_Type = g(0).Field(Of String)("PS_Rab_Release_Type"), _
                                                           .Counter = g.Count() _
                                                       }

            Dim query2ForReason = From detalle In dt.AsEnumerable() Where detalle.Field(Of String)("FaultType") = "PS_RAB_SETUP_FAIL"
                                   Group detalle By grupoClave = New With _
                                                           { _
                                                               Key .FaultType = detalle("faultType"), _
                                                               Key .PS_Rab_Release_Reason = detalle("PsSetupErrorReason")} Into g = Group _
                                                       Select New With _
                                                       { _
                                                           .FaultType = g(0).Field(Of String)("faultType"), _
                                                           .PS_Rab_Release_Type = g(0).Field(Of String)("PsSetupErrorReason"), _
                                                           .Counter = g.Count() _
                                                       }
            dt1 = ConvertOverviewLINQueryToDataTable(query1ForReason)
            dt2 = ConvertOverviewLINQueryToDataTable(query2ForReason)
            dt1.Merge(dt2)
        Catch ex As Exception

        End Try
        Return dt1
    End Function
    Public Function PSOverviewChartLINQ_ErrorReason(ByRef dt As DataTable) As DataTable
        Dim dt1 As DataTable = New DataTable
        Dim dt2 As DataTable = New DataTable
        Try


            Dim query1ForReason = From detalle In dt.AsEnumerable() Where detalle.Field(Of String)("FaultType") = "PS_RAB_DROP"
                                   Group detalle By grupoClave = New With _
                                                           { _
                                                               Key .FaultType = detalle("faultType"), _
                                                               Key .PS_Rab_Release_Reason = detalle("PS_Rab_Release_Reason")} Into g = Group _
                                                       Select New With _
                                                       { _
                                                           .FaultType = g(0).Field(Of String)("faultType"), _
                                                           .PS_Rab_Release_Reason = g(0).Field(Of String)("PS_Rab_Release_Reason"), _
                                                           .Counter = g.Count() _
                                                       }
            Dim query2ForReason = From detalle In dt.AsEnumerable() Where detalle.Field(Of String)("FaultType") = "PS_RAB_SETUP_FAIL"
                                  Group detalle By grupoClave = New With _
                                                          { _
                                                              Key .FaultType = detalle("faultType"), _
                                                              Key .PS_Rab_Release_Reason = detalle("PsSetupErrorReason")} Into g = Group _
                                                      Select New With _
                                                      { _
                                                          .FaultType = g(0).Field(Of String)("faultType"), _
                                                          .PS_Rab_Release_Reason = g(0).Field(Of String)("PsSetupErrorReason"), _
                                                          .Counter = g.Count() _
                                                      }
            dt1 = ConvertOverviewLINQueryToDataTable(query1ForReason)
            dt2 = ConvertOverviewLINQueryToDataTable(query2ForReason)
            dt1.Merge(dt2)
        Catch ex As Exception

        End Try
        Return dt1
    End Function

    Public Function PSOverviewChartLINQ_ErrorCause(ByRef dt As DataTable) As DataTable
        Dim dt1 As DataTable = New DataTable
        Dim dt2 As DataTable = New DataTable
        Try
            Dim query1ForCause = From detalle In dt.AsEnumerable() Where detalle.Field(Of String)("FaultType") = "PS_RAB_DROP"
                                   Group detalle By grupoClave = New With _
                                                               { _
                                                                   Key .FaultType = detalle("faultType"), _
                                                                   Key .PS_Rab_Release_Cause = detalle("PS_Rab_Release_Cause")} Into g = Group _
                                                           Select New With _
                                                           { _
                                                               .FaultType = g(0).Field(Of String)("faultType"), _
                                                               .PS_Rab_Release_Cause = g(0).Field(Of String)("PS_Rab_Release_Cause"), _
                                                               .Counter = g.Count() _
                                                           }

            Dim query2ForCause = From detalle In dt.AsEnumerable() Where detalle.Field(Of String)("FaultType") = "PS_RAB_SETUP_FAIL"
                                 Group detalle By grupoClave = New With _
                                                             { _
                                                                 Key .FaultType = detalle("faultType"), _
                                                                 Key .PS_Rab_Release_Cause = detalle("PsSetupErrorCause")} Into g = Group _
                                                         Select New With _
                                                         { _
                                                             .FaultType = g(0).Field(Of String)("faultType"), _
                                                             .PS_Rab_Release_Cause = g(0).Field(Of String)("PsSetupErrorCause"), _
                                                             .Counter = g.Count() _
                                                         }
            dt1 = ConvertOverviewLINQueryToDataTable(query1ForCause)
            dt2 = ConvertOverviewLINQueryToDataTable(query2ForCause)
            dt1.Merge(dt2)
        Catch ex As Exception

        End Try
        Return dt1
        ''BindOverviewChart(ConvertLINQueryToDataTable(queryForCause), chartOverview3, "CS Error Cause")
    End Function
    Public Function PSOverviewChartLINQ_ReleaseCause(ByRef dt As DataTable) As DataTable
        Dim dt1 As DataTable = New DataTable
        Try
            Dim queryForReleaseCause = From detalle In dt.AsEnumerable() _
                                    Group detalle By grupoClave = New With _
                                                           { _
                                                               Key .FaultType = detalle("faultType"), _
                                                               Key .IU_Release_Cause = detalle("IU_Release_Cause")} Into g = Group _
                                                       Select New With _
                                                       { _
                                                           .FaultType = g(0).Field(Of String)("faultType"), _
                                                           .IU_Release_Cause = g(0).Field(Of String)("IU_Release_Cause"), _
                                                           .Counter = g.Count() _
                                                       }
            dt1 = ConvertOverviewLINQueryToDataTable(queryForReleaseCause)
        Catch ex As Exception

        End Try

        Return dt1

    End Function
    Public Function ConvertOverviewLINQueryToDataTable(ByVal query As Object) As DataTable
        Dim dtData As New DataTable
        Dim isFirstTime As Boolean = True
        For Each p In query
            Dim qResult() As String = p.ToString.Split(",")
            If (isFirstTime) Then
                dtData.Columns.Add(qResult(0).Split("=")(0).Replace("{", "").Replace(" ", ""), GetType(String))
                dtData.Columns.Add(qResult(1).Split("=")(0).Replace(" ", ""), GetType(String))
                dtData.Columns.Add(qResult(2).Split("=")(0).Replace("}", "").Replace(" ", ""), GetType(Integer))
                isFirstTime = False
            End If
            If (Not qResult(1).Split("=")(1).ToString.Trim = "-") Then
                dtData.Rows.Add(qResult(0).Split("=")(1).Replace("{", ""), qResult(1).Split("=")(1).Replace("-", " "), qResult(2).Split("=")(1).Replace("}", ""))
            End If
        Next
        Return dtData

    End Function

#Region "Cell Bar Chart"
#Region "CS Bar Chart"
    Public Function GetCSCellBarChartData(ByRef dt As DataTable, ByVal selectedValue As String, ByVal selectedItemTag As String) As DataTable
        Try


            Dim pchrTab As PCHRTab = New PCHRTab
            Dim dtTemp As New DataTable
            If (selectedItemTag = "Default") Then
                If (selectedValue = "All Error") Then
                    dtTemp = pchrTab.CSCellBarChartLINQ_AllError(dt)
                ElseIf (selectedValue = "All CS Setup Errors") Then
                    dtTemp = pchrTab.CSCellBarChartLINQ_AllCSSetupErrors(dt)
                ElseIf (selectedValue = "All CS Drop Errors") Then
                    dtTemp = pchrTab.CSCellBarChartLINQ_AllCSDropErrors(dt)
                End If
            ElseIf (selectedItemTag = "CSSetupErrorCause") Then
                dtTemp = pchrTab.CSCellBarChartLINQ_CSRabSetupCause(dt, selectedValue)
            ElseIf (selectedItemTag = "CS_RAB_Release_Cause") Then
                dtTemp = pchrTab.CSCellBarChartLINQ_CSRabReleaseCause(dt, selectedValue)
            ElseIf (selectedItemTag = "IU_Release_Cause") Then
                dtTemp = pchrTab.CSCellBarChartLINQ_IUReleaseCause(dt, selectedValue)
            End If
            If (dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0) Then
                Dim ss As DataTable = dtTemp.Select("", "Count DESC").CopyToDataTable()
                Return ss
            Else
                Return dtTemp
            End If

        Catch ex As Exception
            Return Nothing
        End Try

        '  Return IIf((dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0), dtTemp.Select("", "Count DESC").CopyToDataTable(), dtTemp)

    End Function
    Public Function CSCellBarChartLINQ_AllError(ByRef dt As DataTable) As DataTable
        Dim dt1 As DataTable = New DataTable
        Try
            Dim queryLINQ = From detalle In dt.AsEnumerable() _
                                    Group detalle By grupoClave = New With _
                                                           { _
                                                               Key .CellName_Setup = detalle("CellName_Setup") _
                                                               } Into g = Group _
                                                       Select New With _
                                                       { _
                                                        .CS_RAB_CellName_SetupSetup_Cell = IIf(g(0).Field(Of String)("CellName_Setup") <> "", _
                                                                                         g(0).Field(Of String)(DataFieldEntityCS.CellName_Setup), _
                                                                                         g(0).Field(Of String)(DataFieldEntityCS.CellName_Release)), _
                                                        .Count = g.Count()
                                                    } Order By ("Count") Descending
            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)
        Catch ex As Exception

        End Try

        Return dt1 ''.Select("", "Count DESC").CopyToDataTable()

    End Function
    Public Function CSCellBarChartLINQ_AllCSSetupErrors(ByRef dt As DataTable) As DataTable
        Dim dt1 As DataTable = New DataTable
        Try
            Dim queryLINQ = From detalle In dt.AsEnumerable() Where detalle.Field(Of String)("FaultType") = "CS_RAB_SETUP_FAIL"
                                  Group detalle By grupoClave = New With _
                                                              { _
                                                                  Key .CellName_Setup = detalle("CellName_Setup")} Into g = Group _
                                                          Select New With _
                                                          { _
                                                              .CellName_Setup = IIf(g(0).Field(Of String)("CellName_Setup") <> "", _
                                                                                         g(0).Field(Of String)(DataFieldEntityCS.CellName_Setup), _
                                                                                         g(0).Field(Of String)(DataFieldEntityCS.CellName_Release)), _
                                                             .Count = g.Count()
                                                    } Order By ("Count") Descending

            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)
        Catch ex As Exception

        End Try

        Return dt1 ''.Select("", "Count DESC").CopyToDataTable()

    End Function
    Public Function CSCellBarChartLINQ_AllCSDropErrors(ByRef dt As DataTable) As DataTable
        Dim dt1 As DataTable = New DataTable
        Try
            Dim queryLINQ = From detalle In dt.AsEnumerable() Where detalle.Field(Of String)("FaultType") = "CS_RAB_DROP"
                                 Group detalle By grupoClave = New With _
                                                             { _
                                                                 Key .CellName_Release = detalle("CellName_Release")} Into g = Group _
                                                         Select New With _
                                                         { _
                                                             .CellName_Release = IIf(g(0).Field(Of String)("CellName_Release") <> "", _
                                                                                         g(0).Field(Of String)(DataFieldEntityCS.CellName_Release), _
                                                                                         g(0).Field(Of String)(DataFieldEntityCS.CellName_Setup)), _
                                                            .Count = g.Count()
                                                    } Order By ("Count") Descending
            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)
        Catch ex As Exception

        End Try

        Return dt1 ''.Select("", "Count DESC").CopyToDataTable()

    End Function

    Public Function CSCellBarChartLINQ_CSRabSetupCause(ByRef dt As DataTable, ByVal selectedItem As String) As DataTable
        Dim dt1 As DataTable = New DataTable
        Try
            Dim queryLINQ = From detalle In dt.AsEnumerable() Where detalle.Field(Of String)("CSSetupErrorCause") = selectedItem
                                 Group detalle By grupoClave = New With _
                                                             { _
                                                                 Key .CellName_Setup = detalle("CellName_Setup")} Into g = Group _
                                                         Select New With _
                                                         { _
                                                             .CellName_Release = IIf(g(0).Field(Of String)("CellName_Release") <> "", _
                                                                                         g(0).Field(Of String)(DataFieldEntityCS.CellName_Release), _
                                                                                         g(0).Field(Of String)(DataFieldEntityCS.CellName_Setup)), _
                                                            .Count = g.Count()
                                                    } Order By ("Count") Descending
            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)
        Catch ex As Exception

        End Try

        Return dt1 ''.Select("", "Count DESC").CopyToDataTable()

    End Function
    Public Function CSCellBarChartLINQ_CSRabReleaseCause(ByRef dt As DataTable, ByVal selectedItem As String) As DataTable
        Dim dt1 As DataTable = New DataTable
        Try
            Dim queryLINQ = From detalle In dt.AsEnumerable() Where detalle.Field(Of String)("CS_RAB_Release_Cause") = selectedItem
                                 Group detalle By grupoClave = New With _
                                                             { _
                                                                 Key .CellName_Release = detalle("CellName_Release")} Into g = Group _
                                                         Select New With _
                                                         { _
                                                              .CellName_Release = IIf(g(0).Field(Of String)("CellName_Release") <> "", _
                                                                                         g(0).Field(Of String)(DataFieldEntityCS.CellName_Release), _
                                                                                         g(0).Field(Of String)(DataFieldEntityCS.CellName_Setup)), _
                                                             .Count = g.Count()
                                                    } Order By ("Count") Descending
            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)
        Catch ex As Exception

        End Try

        Return dt1 ''.Select("", "Count DESC").CopyToDataTable()

    End Function
    Public Function CSCellBarChartLINQ_IUReleaseCause(ByRef dt As DataTable, ByVal selectedItem As String) As DataTable
        Dim dt1 As DataTable = New DataTable
        Try
            Dim queryLINQ = From detalle In dt.AsEnumerable() Where detalle.Field(Of String)("IU_Release_Cause") = selectedItem
                                 Group detalle By grupoClave = New With _
                                                             { _
                                                                 Key .CellName_Release = detalle("CellName_Release")
                                                                 } Into g = Group _
                                                         Select New With _
                                                         { _
                                                            .CellName_Release = IIf(g(0).Field(Of String)("CellName_Release") <> "", _
                                                                                         g(0).Field(Of String)(DataFieldEntityCS.CellName_Release), _
                                                                                         g(0).Field(Of String)(DataFieldEntityCS.CellName_Setup)), _
                                                           .Count = g.Count() _
                                                    } Order By ("Count") Descending

            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)

        Catch ex As Exception
        End Try
        Return dt1 ''.Select("", "Count DESC").CopyToDataTable()
    End Function

    'Public Function ConvertCellLINQueryToDataTable(ByVal query As Object) As DataTable
    '    Dim dtData As New DataTable
    '    Dim isFirstTime As Boolean = True
    '    For Each p In query
    '        Dim qResult() As String = p.ToString.Split(",")
    '        If (isFirstTime) Then
    '            dtData.Columns.Add(qResult(0).Split("=")(0).Replace("{", "").Replace(" ", ""), GetType(String))
    '            dtData.Columns.Add(qResult(1).Split("=")(0).Replace("}", ""), GetType(String))
    '            'dtData.Columns.Add(qResult(2).Split("=")(0).Replace("}", "").Replace(" ", ""), GetType(Integer))
    '            isFirstTime = False
    '        End If
    '        If (Not qResult(0).Split("=")(1).ToString.Trim = "-1") Then
    '            dtData.Rows.Add(qResult(0).Split("=")(1).Replace("{", ""), qResult(1).Split("=")(1).Replace("}", ""))
    '            '', qResult(2).Split("=")(1).Replace("}", ""))
    '        End If
    '    Next
    '    Return dtData

    'End Function
#End Region
#Region "PS Bar Chart"

    Public Function GetPSCellBarChartData(ByRef dt As DataTable, ByVal selectedValue As String, ByVal selectedItemTag As String) As DataTable
        Try


            Dim pchrTab As PCHRTab = New PCHRTab
            Dim dtTemp As New DataTable
            If (selectedItemTag = "Default") Then
                If (selectedValue = "All Error") Then
                    dtTemp = pchrTab.PSCellBarChartLINQ_AllError(dt)
                ElseIf (selectedValue = "All PS Setup Errors") Then
                    dtTemp = pchrTab.PSCellBarChartLINQ_AllPSSetupErrors(dt)
                ElseIf (selectedValue = "All PS Drop Errors") Then
                    dtTemp = pchrTab.PSCellBarChartLINQ_AllPSDropErrors(dt)
                End If
            ElseIf (selectedItemTag = "PSSetupErrorCause") Then
                dtTemp = pchrTab.PSCellBarChartLINQ_PSRabSetupCause(dt, selectedValue)
            ElseIf (selectedItemTag = "PS_RAB_Release_Cause") Then
                dtTemp = pchrTab.PSCellBarChartLINQ_PSRabReleaseCause(dt, selectedValue)
            ElseIf (selectedItemTag = "IU_Release_Cause") Then
                dtTemp = pchrTab.PSCellBarChartLINQ_IUReleaseCause(dt, selectedValue)
            End If
            If (dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0) Then
                Dim ss As DataTable = dtTemp.Select("", "Count DESC").CopyToDataTable()
                Return ss
            Else
                Return dtTemp
            End If

            'Return IIf((dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0), dtTemp.Select("", "Count DESC").CopyToDataTable(), dtTemp)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function PSCellBarChartLINQ_AllError(ByRef dt As DataTable) As DataTable
        Dim dt1 As DataTable = New DataTable
        Try
            Dim queryLINQ = From detalle In dt.AsEnumerable() _
                                    Group detalle By grupoClave = New With _
                                                           { _
                                                               Key .CellName_Setup = detalle("CellName_Setup") _
                                                               } Into g = Group _
                                                       Select New With _
                                                       { _
                                                        .PS_RAB_CellName_SetupSetup_Cell = IIf(g(0).Field(Of String)("CellName_Setup") <> "", _
                                                                                         g(0).Field(Of String)(DataFieldEntityPS.CellName_Setup), _
                                                                                         g(0).Field(Of String)(DataFieldEntityPS.CellName_Release)), _
                                                        .Count = g.Count()
                                                    } Order By ("Count") Descending
            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)


        Catch ex As Exception

        End Try
        Return dt1 ''.Select("", "Count DESC").CopyToDataTable()

    End Function
    Public Function PSCellBarChartLINQ_AllPSSetupErrors(ByRef dt As DataTable) As DataTable
        Dim dt1 As DataTable = New DataTable
        Try
            Dim queryLINQ = From detalle In dt.AsEnumerable() Where detalle.Field(Of String)("FaultType") = "PS_RAB_SETUP_FAIL"
                                  Group detalle By grupoClave = New With _
                                                              { _
                                                                  Key .CellName_Setup = detalle("CellName_Setup")} Into g = Group _
                                                          Select New With _
                                                          { _
                                                              .CellName_Setup = IIf(g(0).Field(Of String)("CellName_Setup") <> "", _
                                                                                         g(0).Field(Of String)(DataFieldEntityPS.CellName_Setup), _
                                                                                         g(0).Field(Of String)(DataFieldEntityPS.CellName_Release)), _
                                                             .Count = g.Count()
                                                    } Order By ("Count") Descending

            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)
        Catch ex As Exception

        End Try

        Return dt1 ''.Select("", "Count DESC").CopyToDataTable()

    End Function
    Public Function PSCellBarChartLINQ_AllPSDropErrors(ByRef dt As DataTable) As DataTable
        Dim dt1 As DataTable = New DataTable
        Try
            Dim queryLINQ = From detalle In dt.AsEnumerable() Where detalle.Field(Of String)("FaultType") = "PS_RAB_DROP"
                                 Group detalle By grupoClave = New With _
                                                             { _
                                                                 Key .CellName_Release = detalle("CellName_Release")} Into g = Group _
                                                         Select New With _
                                                         { _
                                                             .CellName_Release = IIf(g(0).Field(Of String)("CellName_Release") <> "", _
                                                                                         g(0).Field(Of String)(DataFieldEntityPS.CellName_Release), _
                                                                                         g(0).Field(Of String)(DataFieldEntityPS.CellName_Setup)), _
                                                            .Count = g.Count()
                                                    } Order By ("Count") Descending
            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)
        Catch ex As Exception

        End Try

        Return dt1 ''.Select("", "Count DESC").CopyToDataTable()

    End Function

    Public Function PSCellBarChartLINQ_PSRabSetupCause(ByRef dt As DataTable, ByVal selectedItem As String) As DataTable
        Dim dt1 As DataTable = New DataTable
        Try
            Dim queryLINQ = From detalle In dt.AsEnumerable() Where detalle.Field(Of String)("PSSetupErrorCause") = selectedItem
                                 Group detalle By grupoClave = New With _
                                                             { _
                                                                 Key .CellName_Setup = detalle("CellName_Setup")} Into g = Group _
                                                         Select New With _
                                                         { _
                                                             .CellName_Release = IIf(g(0).Field(Of String)("CellName_Release") <> "", _
                                                                                         g(0).Field(Of String)(DataFieldEntityPS.CellName_Release), _
                                                                                         g(0).Field(Of String)(DataFieldEntityPS.CellName_Setup)), _
                                                            .Count = g.Count()
                                                    } Order By ("Count") Descending
            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)
        Catch ex As Exception

        End Try

        Return dt1 ''.Select("", "Count DESC").CopyToDataTable()

    End Function
    Public Function PSCellBarChartLINQ_PSRabReleaseCause(ByRef dt As DataTable, ByVal selectedItem As String) As DataTable
        Dim dt1 As DataTable = New DataTable
        Try
            Dim queryLINQ = From detalle In dt.AsEnumerable() Where detalle.Field(Of String)("PS_RAB_Release_Cause") = selectedItem
                                 Group detalle By grupoClave = New With _
                                                             { _
                                                                 Key .CellName_Release = detalle("CellName_Release")} Into g = Group _
                                                         Select New With _
                                                         { _
                                                              .CellName_Release = IIf(g(0).Field(Of String)("CellName_Release") <> "", _
                                                                                         g(0).Field(Of String)(DataFieldEntityPS.CellName_Release), _
                                                                                         g(0).Field(Of String)(DataFieldEntityPS.CellName_Setup)), _
                                                             .Count = g.Count()
                                                    } Order By ("Count") Descending
            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)
        Catch ex As Exception

        End Try

        Return dt1 ''.Select("", "Count DESC").CopyToDataTable()

    End Function
    Public Function PSCellBarChartLINQ_IUReleaseCause(ByRef dt As DataTable, ByVal selectedItem As String) As DataTable
        Dim dt1 As DataTable = New DataTable
        Try
            Dim queryLINQ = From detalle In dt.AsEnumerable() Where detalle.Field(Of String)("IU_Release_Cause") = selectedItem
                                 Group detalle By grupoClave = New With _
                                                             { _
                                                                 Key .CellName_Release = detalle("CellName_Release")
                                                                 } Into g = Group _
                                                         Select New With _
                                                         { _
                                                            .CellName_Release = IIf(g(0).Field(Of String)("CellName_Release") <> "", _
                                                                                         g(0).Field(Of String)(DataFieldEntityPS.CellName_Release), _
                                                                                         g(0).Field(Of String)(DataFieldEntityPS.CellName_Setup)), _
                                                           .Count = g.Count() _
                                                    } Order By ("Count") Descending
            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)
        Catch ex As Exception
        End Try
        Return dt1 ''.Select("", "Count DESC").CopyToDataTable()
    End Function


#End Region



    Public Function ConvertCellLINQueryToDataTable(ByVal query As Object) As DataTable
        Dim dtData As New DataTable
        Dim isFirstTime As Boolean = True
        For Each p In query
            Dim qResult() As String = p.ToString.Split(",")
            If (isFirstTime) Then
                dtData.Columns.Add(qResult(0).Split("=")(0).Replace("{", "").Replace(" ", ""), GetType(String))
                Dim colName As String = qResult(1).Split("=")(0).Replace("}", "").Replace(" ", "")
                If (colName.Contains("Count")) Then
                    dtData.Columns.Add(colName, GetType(Integer))
                Else
                    dtData.Columns.Add(colName, GetType(String))
                End If


                'dtData.Columns.Add(qResult(2).Split("=")(0).Replace("}", "").Replace(" ", ""), GetType(Integer))
                isFirstTime = False
            End If
            If (Not qResult(0).Split("=")(1).ToString.Trim = "-1") Then
                dtData.Rows.Add(qResult(0).Split("=")(1).Replace("{", "").Replace(" ", ""), qResult(1).Split("=")(1).Replace("}", "").Replace(" ", ""))
                '', qResult(2).Split("=")(1).Replace("}", ""))
            End If
        Next

        Return dtData

    End Function





#End Region
#Region "Cell Pie Chart"
    Public Enum PieChartErrorType
        ErrorType
        ErrorReason
        ErrorCause
        ErrorIUCause
    End Enum
    Public Function GetSelectedFilterColumn(ByVal filterValue As String, ByVal filterBy As String, ByVal overviewGridType As OverviewGridType) As String()
        Dim filterColumnAndValue(2) As String
        If (overviewGridType = PCHRTab.OverviewGridType.CS) Then
            If (filterBy = "Default") Then
                If (filterValue = "All Error") Then
                    filterColumnAndValue(0) = "faulttype"
                    filterColumnAndValue(1) = ""
                ElseIf (filterValue = "All CS Setup Errors") Then
                    filterColumnAndValue(0) = "faulttype"
                    filterColumnAndValue(1) = "CS_RAB_SETUP_FAIL"
                ElseIf (filterValue = "All CS Drop Errors") Then
                    filterColumnAndValue(0) = "faulttype"
                    filterColumnAndValue(1) = "CS_RAB_DROP"
                End If
            ElseIf (filterBy = "CSSetupErrorCause") Then
                filterColumnAndValue(0) = filterBy
                filterColumnAndValue(1) = filterValue
            ElseIf (filterBy = "CS_RAB_Release_Cause") Then
                filterColumnAndValue(0) = filterBy
                filterColumnAndValue(1) = filterValue
            ElseIf (filterBy = "IU_Release_Cause") Then
                filterColumnAndValue(0) = filterBy
                filterColumnAndValue(1) = filterValue
            End If
        ElseIf (overviewGridType = PCHRTab.OverviewGridType.PS) Then
            If (filterBy = "Default") Then
                If (filterValue = "All Error") Then
                    filterColumnAndValue(0) = "faulttype"
                    filterColumnAndValue(1) = ""
                ElseIf (filterValue = "All PS Setup Errors") Then
                    filterColumnAndValue(0) = "faulttype"
                    filterColumnAndValue(1) = "PS_RAB_SETUP_FAIL"
                ElseIf (filterValue = "All PS Drop Errors") Then
                    filterColumnAndValue(0) = "faulttype"
                    filterColumnAndValue(1) = "PS_RAB_DROP"
                End If
            ElseIf (filterBy = "PSSetupErrorCause") Then
                filterColumnAndValue(0) = filterBy
                filterColumnAndValue(1) = filterValue
            ElseIf (filterBy = "PS_RAB_Release_Cause") Then
                filterColumnAndValue(0) = filterBy
                filterColumnAndValue(1) = filterValue
            ElseIf (filterBy = "IU_Release_Cause") Then
                filterColumnAndValue(0) = filterBy
                filterColumnAndValue(1) = filterValue
            End If
        End If
        Return filterColumnAndValue
    End Function
#Region "CS Pie Chart"
    Public Function GetCSCellPieChartData(ByRef dt As DataTable, ByVal filterValue As String, ByVal filterBy As String, ByVal isByChart As Boolean, ByVal chartSelectedValue As String, ByVal pieChartErrorType As PieChartErrorType) As DataTable
        Dim pchrTab As PCHRTab = New PCHRTab
        Dim dtTemp As DataTable = New DataTable
        ''Dim csGridEntity As List(Of OverviewGridEntityCS) = ConvertCSDataTableToEntity(dt)
        Dim filterColumnAndValue() As String = GetSelectedFilterColumn(filterValue, filterBy, OverviewGridType.CS)

        If (pieChartErrorType = Library.PCHRTab.PieChartErrorType.ErrorType) Then
            If (isByChart) Then
                dtTemp = CSCellPieChartLINQ_ErrorType(dt, filterColumnAndValue(0), filterColumnAndValue(1), chartSelectedValue)
            Else
                dtTemp = CSCellPieChartLINQ_ErrorType(dt, filterColumnAndValue(0), filterColumnAndValue(1))
            End If
        ElseIf (pieChartErrorType = Library.PCHRTab.PieChartErrorType.ErrorReason) Then
            If (isByChart) Then
                dtTemp = CSCellPieChartLINQ_ErrorReason(dt, filterColumnAndValue(0), filterColumnAndValue(1), chartSelectedValue)
            Else
                dtTemp = CSCellPieChartLINQ_ErrorReason(dt, filterColumnAndValue(0), filterColumnAndValue(1))
            End If
        ElseIf (pieChartErrorType = Library.PCHRTab.PieChartErrorType.ErrorCause) Then
            If (isByChart) Then
                dtTemp = CSCellPieChartLINQ_ErrorCause(dt, filterColumnAndValue(0), filterColumnAndValue(1), chartSelectedValue)
            Else
                dtTemp = CSCellPieChartLINQ_ErrorCause(dt, filterColumnAndValue(0), filterColumnAndValue(1))
            End If
        ElseIf (pieChartErrorType = Library.PCHRTab.PieChartErrorType.ErrorIUCause) Then
            If (isByChart) Then
                dtTemp = CSCellPieChartLINQ_ErrorIUCause(dt, filterColumnAndValue(0), filterColumnAndValue(1), chartSelectedValue)
            Else
                dtTemp = CSCellPieChartLINQ_ErrorIUCause(dt, filterColumnAndValue(0), filterColumnAndValue(1))
            End If
        End If

        Return dtTemp.Copy()

    End Function

    Public Function CSCellPieChartLINQ_ErrorCause(ByRef dt As DataTable, ByVal filterValue As String, ByVal filterBy As String) As DataTable
        Dim dt1 As DataTable = New DataTable
        Dim dt2 As DataTable = New DataTable
        Dim dtUnion As DataTable = New DataTable

        Try
            Dim queryLINQ = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                            And detalle.Field(Of String)(DataFieldEntityCS.CsSetupErrorCause) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .CsSetupErrorCause = detalle(DataFieldEntityCS.CsSetupErrorCause) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .CSSetupErrorCause = "Setup_" & g(0).Field(Of String)(DataFieldEntityCS.CsSetupErrorCause), _
                                              .Counter = g.Count()
                                              }
            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)

            Dim queryLINQ2 = From detalle In dt.AsEnumerable() _
                         Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                           And detalle.Field(Of String)(DataFieldEntityCS.CS_RAB_Release_Cause) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .CS_RAB_Release_Cause = detalle(DataFieldEntityCS.CS_RAB_Release_Cause) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .CS_RAB_Release_Cause = "Release_" & g(0).Field(Of String)(DataFieldEntityCS.CS_RAB_Release_Cause), _
                                              .Counter = g.Count()
                                              }

            dt2 = ConvertCellLINQueryToDataTable(queryLINQ2)
            If (dt2 IsNot Nothing AndAlso dt2 IsNot Nothing) Then
                If (dt1.Rows.Count > 0) Then
                    dtUnion = UnionTable(dt1, dt2)
                Else
                    dtUnion = UnionTable(dt2, dt1)
                End If
            End If
        Catch ex As Exception

        End Try
        Return dtUnion

    End Function
    Public Function CSCellPieChartLINQ_ErrorCause(ByRef dt As DataTable, ByVal filterValue As String, ByVal filterBy As String, ByVal chartSelectedValue As String) As DataTable
        Dim dt1 As DataTable = New DataTable
        Dim dt2 As DataTable = New DataTable
        Dim dtUnion As DataTable = New DataTable

        Try

            Dim queryLINQ = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                          And (detalle.Field(Of String)(DataFieldEntityCS.CellName_Setup) = chartSelectedValue _
                               Or detalle.Field(Of String)(DataFieldEntityCS.CellName_Release) = chartSelectedValue) _
                            And detalle.Field(Of String)(DataFieldEntityCS.CsSetupErrorCause) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .CsSetupErrorCause = detalle(DataFieldEntityCS.CsSetupErrorCause) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .CSSetupErrorCause = "Setup_" & g(0).Field(Of String)(DataFieldEntityCS.CsSetupErrorCause), _
                                              .Counter = g.Count()
                                              }
            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)

            Dim queryLINQ2 = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                          And (detalle.Field(Of String)(DataFieldEntityCS.CellName_Setup) = chartSelectedValue _
                               Or detalle.Field(Of String)(DataFieldEntityCS.CellName_Release) = chartSelectedValue) _
                           And detalle.Field(Of String)(DataFieldEntityCS.CS_RAB_Release_Cause) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .CS_RAB_Release_Cause = detalle(DataFieldEntityCS.CS_RAB_Release_Cause) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .CS_RAB_Release_Cause = "Release_" & g(0).Field(Of String)(DataFieldEntityCS.CS_RAB_Release_Cause), _
                                              .Counter = g.Count()
                                              }

            dt2 = ConvertCellLINQueryToDataTable(queryLINQ2)
            If (dt2 IsNot Nothing AndAlso dt2 IsNot Nothing) Then
                If (dt1.Rows.Count > 0) Then
                    dtUnion = UnionTable(dt1, dt2)
                Else
                    dtUnion = UnionTable(dt2, dt1)
                End If
            End If

        Catch ex As Exception

        End Try
        Return dtUnion

    End Function


    Public Function CSCellPieChartLINQ_ErrorType(ByRef dt As DataTable, ByVal filterValue As String, ByVal filterBy As String) As DataTable

        Dim dt1 As DataTable = New DataTable
        Dim dt2 As DataTable = New DataTable
        Dim dtUnion As DataTable = New DataTable

        Try

            Dim queryLINQ = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                            And detalle.Field(Of String)(DataFieldEntityCS.CsSetupErrorType) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .CsSetupErrorType = detalle(DataFieldEntityCS.CsSetupErrorType) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .CsSetupErrorType = "Setup_" & g(0).Field(Of String)(DataFieldEntityCS.CsSetupErrorType), _
                                              .Counter = g.Count()
                                              }
            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)

            Dim queryLINQ2 = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                           And detalle.Field(Of String)(DataFieldEntityCS.CS_RAB_Release_Cause) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .CS_RAB_Release_Type = detalle(DataFieldEntityCS.CS_RAB_Release_Type) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .CS_RAB_Release_Type = "Release_" & g(0).Field(Of String)(DataFieldEntityCS.CS_RAB_Release_Type), _
                                              .Counter = g.Count()
                                              }

            dt2 = ConvertCellLINQueryToDataTable(queryLINQ2)
            If (dt2 IsNot Nothing AndAlso dt2 IsNot Nothing) Then
                If (dt1.Rows.Count > 0) Then
                    dtUnion = UnionTable(dt1, dt2)
                Else
                    dtUnion = UnionTable(dt2, dt1)
                End If
            End If
        Catch ex As Exception

        End Try
        Return dtUnion

    End Function
    Public Function CSCellPieChartLINQ_ErrorType(ByRef dt As DataTable, ByVal filterValue As String, ByVal filterBy As String, ByVal chartSelectedValue As String) As DataTable
        Dim dt1 As DataTable = New DataTable
        Dim dt2 As DataTable = New DataTable
        Dim dtUnion As DataTable = New DataTable

        Try

            Dim queryLINQ = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                          And (detalle.Field(Of String)(DataFieldEntityCS.CellName_Setup) = chartSelectedValue _
                               Or detalle.Field(Of String)(DataFieldEntityCS.CellName_Release) = chartSelectedValue) _
                            And detalle.Field(Of String)(DataFieldEntityCS.CsSetupErrorType) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .CsSetupErrorType = detalle(DataFieldEntityCS.CsSetupErrorType) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .CsSetupErrorType = "Setup_" & g(0).Field(Of String)(DataFieldEntityCS.CsSetupErrorType), _
                                              .Counter = g.Count()
                                              }
            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)

            Dim queryLINQ2 = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                          And (detalle.Field(Of String)(DataFieldEntityCS.CellName_Setup) = chartSelectedValue _
                               Or detalle.Field(Of String)(DataFieldEntityCS.CellName_Release) = chartSelectedValue) _
                           And detalle.Field(Of String)(DataFieldEntityCS.CS_RAB_Release_Cause) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .CS_RAB_Release_Type = detalle(DataFieldEntityCS.CS_RAB_Release_Type) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .CS_RAB_Release_Type = "Release_" & g(0).Field(Of String)(DataFieldEntityCS.CS_RAB_Release_Type), _
                                              .Counter = g.Count()
                                              }

            dt2 = ConvertCellLINQueryToDataTable(queryLINQ2)
            If (dt2 IsNot Nothing AndAlso dt2 IsNot Nothing) Then
                If (dt.Rows.Count > 0) Then
                    dtUnion = UnionTable(dt1, dt2)
                Else
                    dtUnion = UnionTable(dt2, dt1)
                End If
            End If
        Catch ex As Exception

        End Try
        Return dtUnion

    End Function
    Public Function CSCellPieChartLINQ_ErrorReason(ByRef dt As DataTable, ByVal filterValue As String, ByVal filterBy As String) As DataTable
        Dim dt1 As DataTable = New DataTable
        Dim dt2 As DataTable = New DataTable
        Dim dtUnion As DataTable = New DataTable

        Try

            Dim queryLINQ = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                            And detalle.Field(Of String)(DataFieldEntityCS.CsSetupErrorReason) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .CsSetupErrorReason = detalle(DataFieldEntityCS.CsSetupErrorReason) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .CsSetupErrorReason = "Setup_" & g(0).Field(Of String)(DataFieldEntityCS.CsSetupErrorReason), _
                                              .Counter = g.Count()
                                              }
            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)

            Dim queryLINQ2 = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                           And detalle.Field(Of String)(DataFieldEntityCS.CS_RAB_Release_Cause) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .CS_RAB_Release_reason = detalle(DataFieldEntityCS.CS_RAB_Release_reason) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .CS_RAB_Release_reason = "Release_" & g(0).Field(Of String)(DataFieldEntityCS.CS_RAB_Release_reason), _
                                              .Counter = g.Count()
                                              }

            dt2 = ConvertCellLINQueryToDataTable(queryLINQ2)
            If (dt2 IsNot Nothing AndAlso dt2 IsNot Nothing) Then
                If (dt1.Rows.Count > 0) Then
                    dtUnion = UnionTable(dt1, dt2)
                Else
                    dtUnion = UnionTable(dt2, dt1)
                End If
            End If
        Catch ex As Exception

        End Try
        Return dtUnion

    End Function
    Public Function CSCellPieChartLINQ_ErrorReason(ByRef dt As DataTable, ByVal filterValue As String, ByVal filterBy As String, ByVal chartSelectedValue As String) As DataTable
        Dim dt1 As DataTable = New DataTable
        Dim dt2 As DataTable = New DataTable
        Dim dtUnion As DataTable = New DataTable

        Try

            Dim queryLINQ = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                          And (detalle.Field(Of String)(DataFieldEntityCS.CellName_Setup) = chartSelectedValue _
                               Or detalle.Field(Of String)(DataFieldEntityCS.CellName_Release) = chartSelectedValue) _
                            And detalle.Field(Of String)(DataFieldEntityCS.CsSetupErrorReason) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .CsSetupErrorReason = detalle(DataFieldEntityCS.CsSetupErrorReason) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .CsSetupErrorReason = "setup_" & g(0).Field(Of String)(DataFieldEntityCS.CsSetupErrorReason), _
                                              .Counter = g.Count()
                                              }
            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)

            Dim queryLINQ2 = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                          And (detalle.Field(Of String)(DataFieldEntityCS.CellName_Setup) = chartSelectedValue _
                               Or detalle.Field(Of String)(DataFieldEntityCS.CellName_Release) = chartSelectedValue) _
                           And detalle.Field(Of String)(DataFieldEntityCS.CS_RAB_Release_Cause) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .CS_RAB_Release_reason = detalle(DataFieldEntityCS.CS_RAB_Release_reason) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .CS_RAB_Release_reason = "Release_" & g(0).Field(Of String)(DataFieldEntityCS.CS_RAB_Release_reason), _
                                              .Counter = g.Count()
                                              }

            dt2 = ConvertCellLINQueryToDataTable(queryLINQ2)
            If (dt2 IsNot Nothing AndAlso dt2 IsNot Nothing) Then
                If (dt1.Rows.Count > 0) Then
                    dtUnion = UnionTable(dt1, dt2)
                Else
                    dtUnion = UnionTable(dt2, dt1)
                End If
            End If
        Catch ex As Exception

        End Try
        Return dtUnion

    End Function
    Public Function CSCellPieChartLINQ_ErrorIUCause(ByRef dt As DataTable, ByVal filterValue As String, ByVal filterBy As String) As DataTable
        Dim dt1 As DataTable = New DataTable
        ''Dim dt2 As DataTable = New DataTable
        ''Dim dtUnion As DataTable = New DataTable

        Try

            Dim queryLINQ = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                            And detalle.Field(Of String)(DataFieldEntityCS.IU_Release_Cause) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .IU_Release_Cause = detalle(DataFieldEntityCS.IU_Release_Cause) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .IU_Release_Cause = g(0).Field(Of String)(DataFieldEntityCS.IU_Release_Cause), _
                                              .Counter = g.Count()
                                              }
            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)


        Catch ex As Exception

        End Try
        Return dt1

    End Function
    Public Function CSCellPieChartLINQ_ErrorIUCause(ByRef dt As DataTable, ByVal filterValue As String, ByVal filterBy As String, ByVal chartSelectedValue As String) As DataTable
        Dim dt1 As DataTable = New DataTable
        ''Dim dt2 As DataTable = New DataTable
        ''Dim dtUnion As DataTable = New DataTable

        Try

            Dim queryLINQ = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                          And (detalle.Field(Of String)(DataFieldEntityCS.CellName_Setup) = chartSelectedValue _
                               Or detalle.Field(Of String)(DataFieldEntityCS.CellName_Release) = chartSelectedValue) _
                            And detalle.Field(Of String)(DataFieldEntityCS.IU_Release_Cause) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .IU_Release_Cause = detalle(DataFieldEntityCS.IU_Release_Cause) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .IU_Release_Cause = g(0).Field(Of String)(DataFieldEntityCS.IU_Release_Cause), _
                                              .Counter = g.Count()
                                              }
            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)


        Catch ex As Exception

        End Try
        Return dt1

    End Function

#End Region

#Region "PS Pie Chart"
    Public Function GetPSCellPieChartData(ByRef dt As DataTable, ByVal filterValue As String, ByVal filterBy As String, ByVal isByChart As Boolean, ByVal chartSelectedValue As String, ByVal pieChartErrorType As PieChartErrorType) As DataTable
        Dim pchrTab As PCHRTab = New PCHRTab
        Dim dtTemp As DataTable = New DataTable
        ''Dim PSGridEntity As List(Of OverviewGridEntityPS) = ConvertPSDataTableToEntity(dt)
        Dim filterColumnAndValue() As String = GetSelectedFilterColumn(filterValue, filterBy, OverviewGridType.PS)

        If (pieChartErrorType = Library.PCHRTab.PieChartErrorType.ErrorType) Then
            If (isByChart) Then
                dtTemp = PSCellPieChartLINQ_ErrorType(dt, filterColumnAndValue(0), filterColumnAndValue(1), chartSelectedValue)
            Else
                dtTemp = PSCellPieChartLINQ_ErrorType(dt, filterColumnAndValue(0), filterColumnAndValue(1))
            End If
        ElseIf (pieChartErrorType = Library.PCHRTab.PieChartErrorType.ErrorReason) Then
            If (isByChart) Then
                dtTemp = PSCellPieChartLINQ_ErrorReason(dt, filterColumnAndValue(0), filterColumnAndValue(1), chartSelectedValue)
            Else
                dtTemp = PSCellPieChartLINQ_ErrorReason(dt, filterColumnAndValue(0), filterColumnAndValue(1))
            End If
        ElseIf (pieChartErrorType = Library.PCHRTab.PieChartErrorType.ErrorCause) Then
            If (isByChart) Then
                dtTemp = PSCellPieChartLINQ_ErrorCause(dt, filterColumnAndValue(0), filterColumnAndValue(1), chartSelectedValue)
            Else
                dtTemp = PSCellPieChartLINQ_ErrorCause(dt, filterColumnAndValue(0), filterColumnAndValue(1))
            End If
        ElseIf (pieChartErrorType = Library.PCHRTab.PieChartErrorType.ErrorIUCause) Then
            If (isByChart) Then
                dtTemp = PSCellPieChartLINQ_ErrorIUCause(dt, filterColumnAndValue(0), filterColumnAndValue(1), chartSelectedValue)
            Else
                dtTemp = PSCellPieChartLINQ_ErrorIUCause(dt, filterColumnAndValue(0), filterColumnAndValue(1))
            End If
        End If

        Return dtTemp.Copy()

    End Function

    Public Function PSCellPieChartLINQ_ErrorCause(ByRef dt As DataTable, ByVal filterValue As String, ByVal filterBy As String) As DataTable
        Dim dt1 As DataTable = New DataTable
        Dim dt2 As DataTable = New DataTable
        Dim dtUnion As DataTable = New DataTable

        Try
            Dim queryLINQ = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                            And detalle.Field(Of String)(DataFieldEntityPS.PSSetupErrorCause) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .PSSetupErrorCause = detalle(DataFieldEntityPS.PSSetupErrorCause) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .PSSetupErrorCause = "Setup_" & g(0).Field(Of String)(DataFieldEntityPS.PSSetupErrorCause), _
                                              .Counter = g.Count()
                                              }
            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)

            Dim queryLINQ2 = From detalle In dt.AsEnumerable() _
                         Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                           And detalle.Field(Of String)(DataFieldEntityPS.PS_RAB_Release_Cause) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .PS_RAB_Release_Cause = detalle(DataFieldEntityPS.PS_RAB_Release_Cause) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .PS_RAB_Release_Cause = "Release_" & g(0).Field(Of String)(DataFieldEntityPS.PS_RAB_Release_Cause), _
                                              .Counter = g.Count()
                                              }

            dt2 = ConvertCellLINQueryToDataTable(queryLINQ2)
            If (dt2 IsNot Nothing AndAlso dt2 IsNot Nothing) Then
                If (dt1.Rows.Count > 0) Then
                    dtUnion = UnionTable(dt1, dt2)
                Else
                    dtUnion = UnionTable(dt2, dt1)
                End If
            End If
        Catch ex As Exception

        End Try
        Return dtUnion

    End Function
    Public Function PSCellPieChartLINQ_ErrorCause(ByRef dt As DataTable, ByVal filterValue As String, ByVal filterBy As String, ByVal chartSelectedValue As String) As DataTable
        Dim dt1 As DataTable = New DataTable
        Dim dt2 As DataTable = New DataTable
        Dim dtUnion As DataTable = New DataTable

        Try

            Dim queryLINQ = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                          And (detalle.Field(Of String)(DataFieldEntityPS.CellName_Setup) = chartSelectedValue _
                               Or detalle.Field(Of String)(DataFieldEntityPS.CellName_Release) = chartSelectedValue) _
                            And detalle.Field(Of String)(DataFieldEntityPS.PSSetupErrorCause) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .PSSetupErrorCause = detalle(DataFieldEntityPS.PSSetupErrorCause) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .PSSetupErrorCause = "Setup_" & g(0).Field(Of String)(DataFieldEntityPS.PSSetupErrorCause), _
                                              .Counter = g.Count()
                                              }
            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)

            Dim queryLINQ2 = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                          And (detalle.Field(Of String)(DataFieldEntityPS.CellName_Setup) = chartSelectedValue _
                               Or detalle.Field(Of String)(DataFieldEntityPS.CellName_Release) = chartSelectedValue) _
                           And detalle.Field(Of String)(DataFieldEntityPS.PS_RAB_Release_Cause) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .PS_RAB_Release_Cause = detalle(DataFieldEntityPS.PS_RAB_Release_Cause) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .PS_RAB_Release_Cause = "Release_" & g(0).Field(Of String)(DataFieldEntityPS.PS_RAB_Release_Cause), _
                                              .Counter = g.Count()
                                              }

            dt2 = ConvertCellLINQueryToDataTable(queryLINQ2)
            If (dt2 IsNot Nothing AndAlso dt2 IsNot Nothing) Then
                If (dt1.Rows.Count > 0) Then
                    dtUnion = UnionTable(dt1, dt2)
                Else
                    dtUnion = UnionTable(dt2, dt1)
                End If
            End If

        Catch ex As Exception

        End Try
        Return dtUnion

    End Function


    Public Function PSCellPieChartLINQ_ErrorType(ByRef dt As DataTable, ByVal filterValue As String, ByVal filterBy As String) As DataTable

        Dim dt1 As DataTable = New DataTable
        Dim dt2 As DataTable = New DataTable
        Dim dtUnion As DataTable = New DataTable

        Try

            Dim queryLINQ = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                            And detalle.Field(Of String)(DataFieldEntityPS.PSSetupErrorType) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .PSSetupErrorType = detalle(DataFieldEntityPS.PSSetupErrorType) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .PSSetupErrorType = "Setup_" & g(0).Field(Of String)(DataFieldEntityPS.PSSetupErrorType), _
                                              .Counter = g.Count()
                                              }
            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)

            Dim queryLINQ2 = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                           And detalle.Field(Of String)(DataFieldEntityPS.PS_RAB_Release_Cause) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .PS_RAB_Release_Type = detalle(DataFieldEntityPS.PS_RAB_Release_Type) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .PS_RAB_Release_Type = "Release_" & g(0).Field(Of String)(DataFieldEntityPS.PS_RAB_Release_Type), _
                                              .Counter = g.Count()
                                              }

            dt2 = ConvertCellLINQueryToDataTable(queryLINQ2)
            If (dt2 IsNot Nothing AndAlso dt2 IsNot Nothing) Then
                If (dt1.Rows.Count > 0) Then
                    dtUnion = UnionTable(dt1, dt2)
                Else
                    dtUnion = UnionTable(dt2, dt1)
                End If
            End If
        Catch ex As Exception

        End Try
        Return dtUnion

    End Function
    Public Function PSCellPieChartLINQ_ErrorType(ByRef dt As DataTable, ByVal filterValue As String, ByVal filterBy As String, ByVal chartSelectedValue As String) As DataTable
        Dim dt1 As DataTable = New DataTable
        Dim dt2 As DataTable = New DataTable
        Dim dtUnion As DataTable = New DataTable

        Try

            Dim queryLINQ = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                          And (detalle.Field(Of String)(DataFieldEntityPS.CellName_Setup) = chartSelectedValue _
                               Or detalle.Field(Of String)(DataFieldEntityPS.CellName_Release) = chartSelectedValue) _
                            And detalle.Field(Of String)(DataFieldEntityPS.PSSetupErrorType) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .PSSetupErrorType = detalle(DataFieldEntityPS.PSSetupErrorType) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .PSSetupErrorType = "Setup_" & g(0).Field(Of String)(DataFieldEntityPS.PSSetupErrorType), _
                                              .Counter = g.Count()
                                              }
            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)

            Dim queryLINQ2 = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                          And (detalle.Field(Of String)(DataFieldEntityPS.CellName_Setup) = chartSelectedValue _
                               Or detalle.Field(Of String)(DataFieldEntityPS.CellName_Release) = chartSelectedValue) _
                           And detalle.Field(Of String)(DataFieldEntityPS.PS_RAB_Release_Cause) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .PS_RAB_Release_Type = detalle(DataFieldEntityPS.PS_RAB_Release_Type) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .PS_RAB_Release_Type = "Release_" & g(0).Field(Of String)(DataFieldEntityPS.PS_RAB_Release_Type), _
                                              .Counter = g.Count()
                                              }

            dt2 = ConvertCellLINQueryToDataTable(queryLINQ2)
            If (dt2 IsNot Nothing AndAlso dt2 IsNot Nothing) Then
                If (dt1.Rows.Count > 0) Then
                    dtUnion = UnionTable(dt1, dt2)
                Else
                    dtUnion = UnionTable(dt2, dt1)
                End If
            End If
        Catch ex As Exception

        End Try
        Return dtUnion

    End Function
    Public Function PSCellPieChartLINQ_ErrorReason(ByRef dt As DataTable, ByVal filterValue As String, ByVal filterBy As String) As DataTable
        Dim dt1 As DataTable = New DataTable
        Dim dt2 As DataTable = New DataTable
        Dim dtUnion As DataTable = New DataTable

        Try

            Dim queryLINQ = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                            And detalle.Field(Of String)(DataFieldEntityPS.PSSetupErrorReason) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .PSSetupErrorReason = detalle(DataFieldEntityPS.PSSetupErrorReason) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .PSSetupErrorReason = "Setup_" & g(0).Field(Of String)(DataFieldEntityPS.PSSetupErrorReason), _
                                              .Counter = g.Count()
                                              }
            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)

            Dim queryLINQ2 = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                           And detalle.Field(Of String)(DataFieldEntityPS.PS_RAB_Release_Cause) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .PS_RAB_Release_reason = detalle(DataFieldEntityPS.PS_RAB_Release_reason) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .PS_RAB_Release_reason = "Release_" & g(0).Field(Of String)(DataFieldEntityPS.PS_RAB_Release_reason), _
                                              .Counter = g.Count()
                                              }

            dt2 = ConvertCellLINQueryToDataTable(queryLINQ2)
            If (dt2 IsNot Nothing AndAlso dt2 IsNot Nothing) Then
                If (dt1.Rows.Count > 0) Then
                    dtUnion = UnionTable(dt1, dt2)
                Else
                    dtUnion = UnionTable(dt2, dt1)
                End If
            End If
        Catch ex As Exception

        End Try
        Return dtUnion

    End Function
    Public Function PSCellPieChartLINQ_ErrorReason(ByRef dt As DataTable, ByVal filterValue As String, ByVal filterBy As String, ByVal chartSelectedValue As String) As DataTable
        Dim dt1 As DataTable = New DataTable
        Dim dt2 As DataTable = New DataTable
        Dim dtUnion As DataTable = New DataTable

        Try

            Dim queryLINQ = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                          And (detalle.Field(Of String)(DataFieldEntityPS.CellName_Setup) = chartSelectedValue _
                               Or detalle.Field(Of String)(DataFieldEntityPS.CellName_Release) = chartSelectedValue) _
                            And detalle.Field(Of String)(DataFieldEntityPS.PSSetupErrorReason) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .PSSetupErrorReason = detalle(DataFieldEntityPS.PSSetupErrorReason) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .PSSetupErrorReason = "Setup_" & g(0).Field(Of String)(DataFieldEntityPS.PSSetupErrorReason), _
                                              .Counter = g.Count()
                                              }
            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)

            Dim queryLINQ2 = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                          And (detalle.Field(Of String)(DataFieldEntityPS.CellName_Setup) = chartSelectedValue _
                               Or detalle.Field(Of String)(DataFieldEntityPS.CellName_Release) = chartSelectedValue) _
                           And detalle.Field(Of String)(DataFieldEntityPS.PS_RAB_Release_Cause) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .PS_RAB_Release_reason = detalle(DataFieldEntityPS.PS_RAB_Release_reason) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .PS_RAB_Release_reason = "Release_" & g(0).Field(Of String)(DataFieldEntityPS.PS_RAB_Release_reason), _
                                              .Counter = g.Count()
                                              }

            dt2 = ConvertCellLINQueryToDataTable(queryLINQ2)
            If (dt2 IsNot Nothing AndAlso dt2 IsNot Nothing) Then
                If (dt.Rows.Count > 0) Then
                    dtUnion = UnionTable(dt1, dt2)
                Else
                    dtUnion = UnionTable(dt2, dt1)
                End If
            End If
        Catch ex As Exception

        End Try
        Return dtUnion

    End Function

    Public Function PSCellPieChartLINQ_ErrorIUCause(ByRef dt As DataTable, ByVal filterValue As String, ByVal filterBy As String) As DataTable
        Dim dt1 As DataTable = New DataTable
        ''Dim dt2 As DataTable = New DataTable
        ''Dim dtUnion As DataTable = New DataTable

        Try

            Dim queryLINQ = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                            And detalle.Field(Of String)(DataFieldEntityPS.IU_Release_Cause) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .IU_Release_Cause = detalle(DataFieldEntityPS.IU_Release_Cause) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .IU_Release_Cause = g(0).Field(Of String)(DataFieldEntityPS.IU_Release_Cause), _
                                              .Counter = g.Count()
                                              }
            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)


        Catch ex As Exception

        End Try
        Return dt1

    End Function
    Public Function PSCellPieChartLINQ_ErrorIUCause(ByRef dt As DataTable, ByVal filterValue As String, ByVal filterBy As String, ByVal chartSelectedValue As String) As DataTable
        Dim dt1 As DataTable = New DataTable
        ''Dim dt2 As DataTable = New DataTable
        ''Dim dtUnion As DataTable = New DataTable

        Try

            Dim queryLINQ = From detalle In dt.AsEnumerable() _
                          Where IIf(filterBy = "", detalle.Field(Of String)(filterValue) <> "", detalle.Field(Of String)(filterValue) = filterBy) _
                          And (detalle.Field(Of String)(DataFieldEntityPS.CellName_Setup) = chartSelectedValue _
                               Or detalle.Field(Of String)(DataFieldEntityPS.CellName_Release) = chartSelectedValue) _
                            And detalle.Field(Of String)(DataFieldEntityPS.IU_Release_Cause) <> "-"
                                  Group detalle By grupoClave = New With _
                                  { _
                                  Key .IU_Release_Cause = detalle(DataFieldEntityPS.IU_Release_Cause) _
                                  } Into g = Group _
                                         Select New With _
                                              { _
                                              .IU_Release_Cause = g(0).Field(Of String)(DataFieldEntityPS.IU_Release_Cause), _
                                              .Counter = g.Count()
                                              }
            dt1 = ConvertCellLINQueryToDataTable(queryLINQ)


        Catch ex As Exception

        End Try
        Return dt1

    End Function
#End Region

    Public Shared Function UnionTable(ByVal First As DataTable, ByVal Second As DataTable) As DataTable

        'Result table
        Dim table As New DataTable("UnionTBL")

        'Build new columns
        Dim newcolumns As DataColumn() = New DataColumn(First.Columns.Count - 1) {}

        For i As Integer = 0 To First.Columns.Count - 1
            newcolumns(i) = New DataColumn(First.Columns(i).ColumnName, First.Columns(i).DataType)
        Next

        table.Columns.AddRange(newcolumns)
        table.BeginLoadData()

        For Each row As DataRow In First.Rows
            table.LoadDataRow(row.ItemArray, True)
        Next

        For Each row As DataRow In Second.Rows
            table.LoadDataRow(row.ItemArray, True)
        Next

        table.EndLoadData()
        Return table
    End Function

#End Region
    Public Shared Function GetCellFilterErrorCount(ByRef dt As DataTable, ByVal selectedValue As String, ByVal selectedItemTag As String, ByVal overviewGridType As OverviewGridType) As Integer
        Try


            Dim pchrTab As PCHRTab = New PCHRTab()
            Dim dtTemp As New DataTable
            If (overviewGridType = Library.PCHRTab.OverviewGridType.CS) Then
                dtTemp = pchrTab.GetCSCellBarChartData(dt, selectedValue, selectedItemTag)
            ElseIf (overviewGridType = Library.PCHRTab.OverviewGridType.PS) Then
                dtTemp = pchrTab.GetPSCellBarChartData(dt, selectedValue, selectedItemTag)
            End If
            If (dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0) Then
                Return dtTemp.Rows.Count
            Else
                Return 0
            End If
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Shared Sub BindCellTabFilter(ByRef dt As DataTable, ByRef cmb As DevExpress.XtraEditors.ComboBoxEdit, ByVal overviewGridType As OverviewGridType)

        cmb.SuspendLayout()
        cmb.Properties.Items.Clear()
        cmb.Refresh()
        Dim item As clsComboBoxItem
        If (overviewGridType = PCHRTab.OverviewGridType.CS) Then
            item = New clsComboBoxItem()
            item.Text = "All Error(" & GetCellFilterErrorCount(dt, "All Error", "Default", PCHRTab.OverviewGridType.CS) & ")"
            item.Tag = "Default"
            cmb.Properties.Items.Add(item)
            item = New clsComboBoxItem()
            item.Text = "All CS Setup Errors(" & GetCellFilterErrorCount(dt, "All CS Setup Errors", "Default", PCHRTab.OverviewGridType.CS) & ")"
            item.Tag = "Default"
            cmb.Properties.Items.Add(item)
            item = New clsComboBoxItem()
            item.Text = "All CS Drop Errors(" & GetCellFilterErrorCount(dt, "All CS Drop Errors", "Default", PCHRTab.OverviewGridType.CS) & ")"
            item.Tag = "Default"
            cmb.Properties.Items.Add(item)
            item = New clsComboBoxItem()
            item.Text = "-----------------"
            item.Tag = "-"
            item.Enabled = False
            cmb.Properties.Items.Add(item)
            GetDistinctField(dt, "CSSetupErrorCause", cmb, PCHRTab.OverviewGridType.CS)
            GetDistinctField(dt, "CS_RAB_Release_Cause", cmb, PCHRTab.OverviewGridType.CS)
            item = New clsComboBoxItem()
            item.Text = "-----------------"
            item.Tag = "-"
            item.Enabled = False
            cmb.Properties.Items.Add(item)
            GetDistinctField(dt, "IU_Release_Cause", cmb, PCHRTab.OverviewGridType.CS)
        ElseIf (overviewGridType = PCHRTab.OverviewGridType.PS) Then
            item = New clsComboBoxItem()
            item.Text = "All Error(" & GetCellFilterErrorCount(dt, "All Error", "Default", PCHRTab.OverviewGridType.PS) & ")"
            item.Tag = "Default"
            cmb.Properties.Items.Add(item)
            item = New clsComboBoxItem()
            item.Text = "All PS Setup Errors(" & GetCellFilterErrorCount(dt, "All PS Setup Errors", "Default", PCHRTab.OverviewGridType.PS) & ")"
            item.Tag = "Default"
            cmb.Properties.Items.Add(item)
            item = New clsComboBoxItem()
            item.Text = "All PS Drop Errors(" & GetCellFilterErrorCount(dt, "All PS Drop Errors", "Default", PCHRTab.OverviewGridType.PS) & ")"
            item.Tag = "Default"
            cmb.Properties.Items.Add(item)
            item = New clsComboBoxItem()
            item.Text = "-----------------"
            item.Tag = "-"
            item.Enabled = False
            cmb.Properties.Items.Add(item)
            GetDistinctField(dt, "PSSetupErrorCause", cmb, PCHRTab.OverviewGridType.PS)
            GetDistinctField(dt, "PS_RAB_Release_Cause", cmb, PCHRTab.OverviewGridType.PS)
            item = New clsComboBoxItem()
            item.Text = "-----------------"
            item.Tag = "-"
            item.Enabled = False
            cmb.Properties.Items.Add(item)
            GetDistinctField(dt, "IU_Release_Cause", cmb, PCHRTab.OverviewGridType.PS)
        End If
        cmb.Update()
        cmb.ResumeLayout()
    End Sub
    Private Shared Sub GetDistinctField(ByRef dt As DataTable, ByVal distinctColumn As String, ByRef cmb As DevExpress.XtraEditors.ComboBoxEdit, ByVal overviewGridType As OverviewGridType)
        Try

            Dim item As clsComboBoxItem
            If (dt.Rows.Count > 0) Then
                Dim distinctDT As DataTable = dt.DefaultView.ToTable(True, distinctColumn)
                If (distinctDT.Rows.Count > 0) Then
                    For Each dr As DataRow In distinctDT.Rows
                        If (Not dr(0).ToString.Trim = "-") Then
                            item = New clsComboBoxItem()
                            item.Text = dr(0).ToString & "(" & GetCellFilterErrorCount(dt, dr(0).ToString, distinctColumn, overviewGridType) & ")"
                            item.Tag = distinctColumn
                            cmb.Properties.Items.Add(item)
                        End If
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function ConvertCSDataTableToEntity(ByRef dt As DataTable) As List(Of OverviewGridEntityCS)
        ''.fileld = r.Field(Of String)(DataFieldEntityCS.fileld),
        Dim list As New List(Of OverviewGridEntityCS)()
        list = (From r In dt.AsEnumerable() _
                            Select New OverviewGridEntityCS With _
                                   { _
                                    .CallRecordNum = r.Field(Of Integer)(DataFieldEntityCS.CallRecordNum),
                                    .CallType = r.Field(Of String)(DataFieldEntityCS.CallType),
                                    .Fault = r.Field(Of Integer)(DataFieldEntityCS.Fault),
                                    .FaultType = r.Field(Of String)(DataFieldEntityCS.FaultType),
                                    .ConnSetuptime = r.Field(Of String)(DataFieldEntityCS.ConnSetuptime),
                                    .Imsi = r.Field(Of String)(DataFieldEntityCS.Imsi),
                                    .Imei = r.Field(Of String)(DataFieldEntityCS.Imei),
                                    .RRCEstCause = r.Field(Of String)(DataFieldEntityCS.RRCEstCause),
                                    .CSRabTrafficClass = r.Field(Of String)(DataFieldEntityCS.CSRabTrafficClass),
                                    .CsDetailTrafficType = r.Field(Of String)(DataFieldEntityCS.CsDetailTrafficType),
                                    .CellName_Setup = r.Field(Of Integer)(DataFieldEntityCS.CellName_Setup),
                                    .CsSetupErrorType = r.Field(Of String)(DataFieldEntityCS.CsSetupErrorType),
                                    .CsSetupErrorReason = r.Field(Of String)(DataFieldEntityCS.CsSetupErrorReason),
                                    .CsSetupErrorCause = r.Field(Of String)(DataFieldEntityCS.CsSetupErrorCause),
                                    .CellName_Release = r.Field(Of Integer)(DataFieldEntityCS.CellName_Release),
                                    .CS_RAB_Release_Type = r.Field(Of String)(DataFieldEntityCS.CS_RAB_Release_Type),
                                    .CS_RAB_Release_reason = r.Field(Of String)(DataFieldEntityCS.CS_RAB_Release_reason),
                                    .CS_RAB_Release_Cause = r.Field(Of String)(DataFieldEntityCS.CS_RAB_Release_Cause),
                                    .CSRabFailTime = r.Field(Of String)(DataFieldEntityCS.CSRabFailTime),
                                    .IU_Release_Cause = r.Field(Of String)(DataFieldEntityCS.IU_Release_Cause),
                                    .UeRelease = r.Field(Of Integer)(DataFieldEntityCS.UeRelease),
                                    .supportband8 = r.Field(Of Integer)(DataFieldEntityCS.supportband8),
                                    .usPropgDelay = r.Field(Of Integer)(DataFieldEntityCS.usPropgDelay),
                                    .ucHsdschCategory = r.Field(Of Integer)(DataFieldEntityCS.ucHsdschCategory),
                                    .ucEdchCategory = r.Field(Of Integer)(DataFieldEntityCS.ucEdchCategory),
                                    .ulEutraFreqBand1 = r.Field(Of Integer)(DataFieldEntityCS.ulEutraFreqBand1),
                                    .ulEutraFreqBand2 = r.Field(Of Integer)(DataFieldEntityCS.ulEutraFreqBand2),
                                    .ConnSetupcurrCellRscp = r.Field(Of String)(DataFieldEntityCS.ConnSetupcurrCellRscp),
                                    .ConnSetupCurrCellEcno = r.Field(Of String)(DataFieldEntityCS.ConnSetupCurrCellEcno),
                                    .ConnSetupCellRncId = r.Field(Of Integer)(DataFieldEntityCS.ConnSetupCellRncId),
                                    .ConnSetupCellCellId = r.Field(Of String)(DataFieldEntityCS.ConnSetupCellCellId),
                                    .rrcrelactivecellset = r.Field(Of String)(DataFieldEntityCS.rrcrelactivecellset),
        .RRCReleaseTime1 = r.Field(Of String)(DataFieldEntityCS.RRCReleaseTime1)
                                    }).ToList


        Return list
    End Function







End Class

Public Class OverviewGridEntityCS
    Public Property fileld As Integer
    Public Property CallRecordNum As Integer
    Public Property CallType As String
    Public Property Fault As Integer
    Public Property FaultType As String
    Public Property ConnSetuptime As String
    Public Property Imsi As String
    Public Property Imei As String
    Public Property RRCEstCause As String
    Public Property CSRabTrafficClass As String
    Public Property CsDetailTrafficType As String
    Public Property CellName_Setup As Integer
    Public Property CsSetupErrorType As String
    Public Property CsSetupErrorReason As String
    Public Property CsSetupErrorCause As String
    Public Property CellName_Release As Integer
    Public Property CS_RAB_Release_Type As String
    Public Property CS_RAB_Release_reason As String
    Public Property CS_RAB_Release_Cause As String
    Public Property CSRabFailTime As String
    Public Property IU_Release_Cause As String
    Public Property UeRelease As Integer
    Public Property supportband8 As Integer
    Public Property usPropgDelay As Integer
    Public Property ucHsdschCategory As Integer
    Public Property ucEdchCategory As Integer
    Public Property ulEutraFreqBand1 As Integer
    Public Property ulEutraFreqBand2 As Integer
    Public Property ConnSetupcurrCellRscp As String
    Public Property ConnSetupCurrCellEcno As String
    Public Property ConnSetupCellRncId As Integer
    Public Property ConnSetupCellCellId As Integer
    Public Property rrcrelactivecellset As String
    Public Property RRCReleaseTime1 As String

End Class

Public Class DataFieldEntityCS
    Public Const fileld As String = "fileld"
    Public Const CallRecordNum As String = "CallRecordNum"
    Public Const CallType As String = "CallType"
    Public Const Fault As String = "Fault"
    Public Const FaultType As String = "FaultType"
    Public Const ConnSetuptime As String = "ConnSetuptime"
    Public Const Imsi As String = "Imsi"
    Public Const Imei As String = "Imei"
    Public Const RRCEstCause As String = "RRCEstCause"
    Public Const CSRabTrafficClass As String = "CSRabTrafficClass"
    Public Const CsDetailTrafficType As String = "CsDetailTrafficType"
    Public Const CellName_Setup As String = "CellName_Setup"
    Public Const CsSetupErrorType As String = "CsSetupErrorType"
    Public Const CsSetupErrorReason As String = "CsSetupErrorReason"
    Public Const CsSetupErrorCause As String = "CsSetupErrorCause"
    Public Const CellName_Release As String = "CellName_Release"
    Public Const CS_RAB_Release_Type As String = "CS_RAB_Release_Type"
    Public Const CS_RAB_Release_reason As String = "CS_RAB_Release_reason"
    Public Const CS_RAB_Release_Cause As String = "CS_RAB_Release_Cause"
    Public Const CSRabFailTime As String = "CSRabFailTime"
    Public Const IU_Release_Cause As String = "IU_Release_Cause"
    Public Const UeRelease As String = "UeRelease"
    Public Const supportband8 As String = "supportband8"
    Public Const usPropgDelay As String = "usPropgDelay"
    Public Const ucHsdschCategory As String = "ucHsdschCategory"
    Public Const ucEdchCategory As String = "ucEdchCategory"
    Public Const ulEutraFreqBand1 As String = "ulEutraFreqBand1"
    Public Const ulEutraFreqBand2 As String = "ulEutraFreqBand2"
    Public Const ConnSetupcurrCellRscp As String = "ConnSetupcurrCellRscp"
    Public Const ConnSetupCurrCellEcno As String = "ConnSetupCurrCellEcno"
    Public Const ConnSetupCellRncId As String = "ConnSetupCellRncId"
    Public Const ConnSetupCellCellId As String = "ConnSetupCellCellId"
    Public Const rrcrelactivecellset As String = "rrcrelactivecellset"
    Public Const RRCReleaseTime1 As String = "RRCReleaseTime1"

End Class
Public Class DataFieldEntityPS
    Public Const fileld As String = "fileld"
    Public Const CallRecordNum As String = "CallRecordNum"
    Public Const CallType As String = "CallType"
    Public Const Fault As String = "Fault"
    Public Const FaultType As String = "FaultType"
    Public Const ConnSetuptime As String = "ConnSetuptime"
    Public Const Imsi As String = "Imsi"
    Public Const Imei As String = "Imei"
    Public Const RRCEstCause As String = "RRCEstCause"
    Public Const PSRabTrafficClass As String = "PSRabTrafficClass"
    Public Const PSDetailTrafficType As String = "PSDetailTrafficType"
    Public Const CellName_Setup As String = "CellName_Setup"
    Public Const PSSetupErrorType As String = "PSSetupErrorType"
    Public Const PSSetupErrorReason As String = "PSSetupErrorReason"
    Public Const PSSetupErrorCause As String = "PSSetupErrorCause"
    Public Const CellName_Release As String = "CellName_Release"
    Public Const PS_RAB_Release_Type As String = "PS_RAB_Release_Type"
    Public Const PS_RAB_Release_reason As String = "PS_RAB_Release_reason"
    Public Const PS_RAB_Release_Cause As String = "PS_RAB_Release_Cause"
    Public Const PSRabFailTime As String = "PSRabFailTime"
    Public Const IU_Release_Cause As String = "IU_Release_Cause"
    Public Const UeRelease As String = "UeRelease"
    Public Const supportband8 As String = "supportband8"
    Public Const usPropgDelay As String = "usPropgDelay"
    Public Const ucHsdschCategory As String = "ucHsdschCategory"
    Public Const ucEdchCategory As String = "ucEdchCategory"
    Public Const ulEutraFreqBand1 As String = "ulEutraFreqBand1"
    Public Const ulEutraFreqBand2 As String = "ulEutraFreqBand2"
    Public Const ConnSetupcurrCellRscp As String = "ConnSetupcurrCellRscp"
    Public Const ConnSetupCurrCellEcno As String = "ConnSetupCurrCellEcno"
    Public Const ConnSetupCellRncId As String = "ConnSetupCellRncId"
    Public Const ConnSetupCellCellId As String = "ConnSetupCellCellId"
    Public Const rrcrelactivecellset As String = "rrcrelactivecellset"
    Public Const RRCReleaseTime1 As String = "RRCReleaseTime1"

End Class
