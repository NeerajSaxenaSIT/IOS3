Imports IOS.DataLibrary

Public Class IOSUtility

    Public Shared Function GetNotesDataFromFile(ByVal path As String, ByVal kpiNotesFilePAth As String) As String
        Try
            Dim fileName As String = kpiNotesFilePAth
            Dim fileText As String = Nothing
            If System.IO.File.Exists(fileName) = True Then
                Dim objReader As New System.IO.StreamReader(fileName)
                Do While objReader.Peek() <> -1
                    fileText = fileText & objReader.ReadLine() & vbNewLine
                Loop
                Return fileText
            Else
                Return GetText_GloblePath(path)
            End If
        Catch ex As Exception
            Return GetText_GloblePath(path)
        End Try
    End Function

    Private Shared Function GetText_GloblePath(ByVal path As String) As String
        Dim fileName As String = String.Empty
        Dim fileText As String = Nothing
        fileName = path + "\\IOS_KpiNoteDefaultText.txt"
        If (System.IO.File.Exists(fileName)) Then
            Dim objReader As New System.IO.StreamReader(fileName)
            Do While objReader.Peek() <> -1
                fileText = fileText & objReader.ReadLine() & vbNewLine
            Loop
            Return fileText
        Else
            Return String.Empty
        End If
    End Function

    Public Shared Sub KPINotesGridRefreshing(ByRef _grdTemp As DevExpress.XtraGrid.GridControl, ByRef _gvTemp As DevExpress.XtraGrid.Views.Grid.GridView, ByVal isHeader As Boolean, ByVal multilineColumn As Integer)
        If (_grdTemp IsNot Nothing) Then
            _grdTemp.SuspendLayout()
            _gvTemp.OptionsSelection.MultiSelect = True
            _gvTemp.OptionsFilter.AllowFilterEditor = True

            For Each col As DevExpress.XtraGrid.Columns.GridColumn In _gvTemp.Columns
                If ((col.Caption).ToUpper = ("NoteDescription").ToUpper) Then
                    col.AppearanceCell.TextOptions.WordWrap = True
                    col.AppearanceHeader.TextOptions.WordWrap = True
                ElseIf ((col.Caption).ToUpper = ("RelationType").ToUpper) Then
                    col.Visible = False
                End If
                col.BestFit()
                col.OptionsColumn.AllowSize = True
                col.OptionsFilter.AllowFilter = True
            Next

            _gvTemp.OptionsView.ShowColumnHeaders = isHeader
            _grdTemp.Refresh()
            _grdTemp.ResumeLayout()
        End If
    End Sub

    Public Shared Function GetKPINote(ByVal kpiID As String, ByVal conStr As String) As DataTable
        Dim sqlCmd As New System.Text.StringBuilder()
        sqlCmd.Append("Select T3.NoteID,T3.NoteTimeStamp,T3.NoteDescription,SqlKPI.[KPI_Name] as KPIName,T3.RelatedKPI, T3.NoteOwner,T3.RelationType ")
        sqlCmd.AppendLine("from [IOS_SQL_KPI] SqlKPI ")
        sqlCmd.AppendLine("INNER JOIN (")
        sqlCmd.AppendLine("Select T2.NoteID,T2.NoteTimeStamp,T2.NoteDescription,T2.NoteOwner, [KPI_Name] as RelatedKPI, T2.RelationType,T2.MainKPIID ")
        sqlCmd.AppendLine("from [IOS_SQL_KPI] SKPI ")
        sqlCmd.AppendLine("INNER JOIN (")
        sqlCmd.AppendLine("SELECT T1.KPIID, KN.NoteID,KN.NoteTimeStamp, KN.NoteDescription, KN.NoteOwner,T1.RelationType,T1.MainKPIID FROM [dbo].[IOS_KPI_Notes] KN ")
        sqlCmd.AppendLine("INNER JOIN  ( ")
        sqlCmd.AppendLine("SELECT KPIID_Relation AS KPIID, RelationType,KPIID as MainKPIID,NoteID FROM [dbo].[IOS_KPI_Notes_Relations] where (KPIID in (" & kpiID & ") AND RelationType='Sibling')")
        sqlCmd.AppendLine("Union all ")
        sqlCmd.AppendLine("SELECT KPIID AS KPIID, RelationType,KPIID_Relation as MainKPIID,NoteID FROM [dbo].[IOS_KPI_Notes_Relations] where (KPIID_Relation in (" & kpiID & ") AND RelationType='Sibling')")
        sqlCmd.AppendLine("Union ALL ")
        sqlCmd.AppendLine("SELECT KPIID AS KPIID,'Self' as RelationType, KPIID as MainKPIID, NoteID from IOS_KPI_Notes WHERE KPIID in (" & kpiID & ")")
        sqlCmd.AppendLine("Union ALL ")
        sqlCmd.AppendLine("select KPIID AS KPIID, RelationType,KPIID_Relation as MainKPIID,NoteID from [dbo].[IOS_KPI_Notes_Relations] where KPIID_Relation in (" & kpiID & ") AND RelationType='Parent'")
        sqlCmd.AppendLine(") T1  ON T1.NoteID= KN.NoteID")
        sqlCmd.AppendLine(") T2 ON T2.KPIid= SKPI.SQLKPI_ID")
        sqlCmd.AppendLine(") T3 on T3.MainKPIID=SqlKPI.SQLKPI_ID")
        Return DataAccessorODBC.GetDataTable(conStr, sqlCmd.ToString)
    End Function

End Class
