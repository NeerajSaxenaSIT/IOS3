Public Class dlgMapTables

#Region "Variables"

    Dim tableselected As String = Nothing
    Dim tablereason As String = Nothing

#End Region

#Region "Form & Controls Event"

    Private Sub dlgMapTables_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            tableselected = Nothing
            lstMapTables.Items.Clear()
            For Each tbl In MapInfo.Engine.Session.Current.Catalog
                If tbl.Alias.StartsWith("Quer") = False Then
                    lstMapTables.Items.Add(tbl.Alias)
                End If
            Next
            If tablereason = "Close" Then
                lblMapControlTbl.Text = "Select table to close: "
            ElseIf tablereason = "Save" Then
                lblMapControlTbl.Text = "Select table to save: "
            ElseIf tablereason = "Grid" Then
                lblMapControlTbl.Text = "Select table to grid: "
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (lstMapTables.Items.Count > 0 AndAlso lstMapTables.SelectedIndex >= 0) Then
                tableselected = lstMapTables.Items.Item(lstMapTables.SelectedIndex).ToString
                Application.UseWaitCursor = True
                Application.DoEvents()
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
                If tablereason = "Close" Then
                    MapInfo.Engine.Session.Current.Catalog.CloseTable(tableselected)
                ElseIf tablereason = "Save" Then
                    Call SaveTable()
                ElseIf tablereason = "Grid" Then
                    Call GridTable()

                End If
                Application.UseWaitCursor = False
                Application.DoEvents()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#End Region

#Region "Helper"

    Public Sub dlgMapTables_Setting(ByVal reason As String)
        If reason = "Close" Then
            tablereason = "Close"
        ElseIf reason = "Save" Then
            tablereason = "Save"
        ElseIf reason = "Grid" Then
            tablereason = "Grid"
        End If
    End Sub

    Private Sub GridTable()
        'grid tableselected
        Try
            Dim dataTab As System.Data.DataTable = New System.Data.DataTable
            dataTab.TableName = tableselected 'todo
            Dim maptbl As MapInfo.Data.Table = MapInfo.Engine.Session.Current.Catalog.GetTable(tableselected)
            Dim i As Integer
            Dim j As Integer = 0
            For i = 0 To maptbl.TableInfo.Columns.Count - 1
                If maptbl.TableInfo.Columns(i).DataType <> MapInfo.Data.MIDbType.FeatureGeometry And maptbl.TableInfo.Columns(i).DataType <> MapInfo.Data.MIDbType.Style Then
                    If maptbl.TableInfo.Columns(i).DataType = MapInfo.Data.MIDbType.DateTime Then
                        dataTab.Columns.Add(maptbl.TableInfo.Columns(i).Alias, GetType(DateTime))
                    Else
                        dataTab.Columns.Add(maptbl.TableInfo.Columns(i).Alias)
                    End If
                End If
            Next
            Dim f As MapInfo.Data.Feature
            For Each f In maptbl
                j = 0
                Dim dr As System.Data.DataRow = dataTab.NewRow()
                For i = 0 To maptbl.TableInfo.Columns.Count - 1
                    If maptbl.TableInfo.Columns(i).DataType <> MapInfo.Data.MIDbType.FeatureGeometry And maptbl.TableInfo.Columns(i).DataType <> MapInfo.Data.MIDbType.Style Then
                        If maptbl.TableInfo.Columns(i).DataType = MapInfo.Data.MIDbType.DateTime Then
                            dr(j) = f(i)
                        Else
                            dr(j) = f(i).ToString()
                        End If
                        j = j + 1
                    End If
                Next
                dataTab.Rows.Add(dr)
            Next
            Dim newGCMap As DevExpress.XtraGrid.GridControl = frmMapWindow.CreateTabWithGridViewForMapData(tableselected)
            SetHyperlinkColumnsInGridControl(newGCMap, newGCMap.MainView, dataTab)
            dataTab.Dispose()
            dataTab = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SaveTable()
        Try
            Dim fd As New SaveFileDialog
            fd.InitialDirectory = GetUserDataPath() & "\Data\"
            fd.DefaultExt = "*.tab"
            fd.Filter = "Mapinfo TAB|*.tab"
            fd.OverwritePrompt = True
            fd.Title = "Save TAB File"
            fd.FileName = GetUserDataPath() & "\Data\" & tableselected & ".tab"
            Dim diagres As DialogResult = fd.ShowDialog()

            If fd.FileName <> "" And diagres = DialogResult.OK Then
                Dim tbl As MapInfo.Data.Table = MapInfo.Engine.Session.Current.Catalog.GetTable(tableselected)
                CreateNativeFromExisting(fd.FileName, tbl)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CreateNativeFromExisting(ByVal fn As String, ByVal tablesource As MapInfo.Data.Table)
        'Saving to Native. Closing original. Opening native
        Try
            Dim fname As String = System.IO.Path.GetFileName(fn)

            Dim ti As MapInfo.Data.TableInfoNative = CType(MapInfo.Data.TableInfoFactory.CreateFromFeatureCollection(fname, MapInfo.Data.TableType.Native, tablesource), MapInfo.Data.TableInfoNative)
            ti.TablePath = fn
            ti.WriteTabFile()
            Dim nativeTable As MapInfo.Data.Table = MapInfo.Engine.Session.Current.Catalog.CreateTable(ti)
            nativeTable.Close()
            nativeTable = MapInfo.Engine.Session.Current.Catalog.OpenTable(fn)

            'Populating Native Table with data
            nativeTable.BeginAccess(MapInfo.Data.TableAccessMode.Write)

            Dim con As New MapInfo.Data.MIConnection
            con.Open()
            Dim com As MapInfo.Data.MICommand = Nothing
            Dim totalrecords As Integer
            com = con.CreateCommand()
            com.CommandText = "Insert into " + nativeTable.Alias + " (" + ColumnString_MapinfoTable(tablesource) + ") Select " & ColumnString_MapinfoTable(tablesource) + " from " + tablesource.Alias
            com.Prepare()
            totalrecords = com.ExecuteNonQuery()
            nativeTable.EndAccess()
            com.Dispose()
            con.Close()
            con.Dispose()
        Catch ex As Exception
        End Try
    End Sub

    Private Function ColumnString_MapinfoTable(ByVal dt As MapInfo.Data.Table) As String
        Dim str As String = ""
        For Each col As MapInfo.Data.Column In dt.TableInfo.Columns
            str = str & col.Alias & ","
        Next col
        str = str.TrimEnd(",")
        Return str
    End Function

#End Region

End Class