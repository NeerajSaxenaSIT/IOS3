Imports System.Windows.Forms
Imports MapInfo.Data
Imports IOS.Library
Imports MapInfo.Mapping

Public Class dlgTags

    'Dim conn_IOS As String = IOS.Configuration.IOSAppConfigManage.IOSServer

    'Public Sub SetConnectionString(ByVal connstr As String)
    '    conn_IOS = connstr
    'End Sub

#Region "Form & Control Events"

    Private Sub frmDialogTags_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            BindTagsData()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")

    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (lstTags.ItemCount > 0 AndAlso lstTags.SelectedIndex >= 0) Then
                Application.UseWaitCursor = True
                Application.DoEvents()
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
                Dim itm As clsComboBoxItem = TryCast(lstTags.SelectedItem, clsComboBoxItem)
                GetTagDetailsRegion(itm.Text, itm.Value.ToString)
                Application.DoEvents()
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Application.UseWaitCursor = False
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#End Region

#Region "Helper Methods"

    Public Sub BindTagsData()
        Try
            Dim dtTags As DataTable = IOS.DataLibrary.clsSQLCommands.GetTagsData(connStrIOSServer)
            lstTags.Items.Clear()
            For Each dr As DataRow In dtTags.Rows
                Dim itm As New clsComboBoxItem()
                itm.Text = dr.Item("TagName")
                itm.Value = dr.Item("TagID")
                lstTags.Items.Add(itm)
            Next
        Catch ex As Exception
        End Try
    End Sub

    Public Sub GetTagDetailsRegion(ByVal tagName As String, ByVal tagId As Integer)
        Try
            Dim dtTags As DataTable = IOS.DataLibrary.clsSQLCommands.GetTagDetailsRegion(connStrIOSServer, tagId)
            If (dtTags.Rows.Count > 0) Then
                CreateTagTableInMem(dtTags, "Regions_" & tagName)
            End If
        Catch
        End Try
    End Sub

    Private Sub CreateTagTableInMem(ByVal tagData As DataTable, ByVal tableName As String)
        Try
            Dim connection As New MapInfo.Data.MIConnection
            frmMapWindow.objMapHelper.RemoveLayer(tableName)

            Dim tblMem As MapInfo.Data.TableInfoMemTable = New MapInfo.Data.TableInfoMemTable(tableName)
            tblMem.Columns.Add(ColumnFactory.CreateIndexedStringColumn("PolygonName", 100))
            tblMem.Columns.Add(ColumnFactory.CreateFeatureGeometryColumn(csysWGS84))
            tblMem.Columns.Add(ColumnFactory.CreateStyleColumn())
            MapInfo.Engine.Session.Current.Catalog.CloseTable(tableName)
            Dim tblBuffer As MapInfo.Data.Table = MapInfo.Engine.Session.Current.Catalog.CreateTable(tblMem)

            Dim cStyle As MapInfo.Styles.CompositeStyle = Nothing
            cStyle = New MapInfo.Styles.CompositeStyle(New MapInfo.Styles.AreaStyle( _
                                                        New MapInfo.Styles.SimpleLineStyle( _
                                                        New MapInfo.Styles.LineWidth(3, MapInfo.Styles.LineWidthUnit.Pixel), 3, System.Drawing.Color.Black), _
                                                        New MapInfo.Styles.SimpleInterior(1, System.Drawing.Color.Black, Color.White, True)))

            For Each item As DataRow In tagData.Rows
                Dim ft As New Feature(tblMem.Columns)
                ft("PolygonName") = item("RegionName").ToString
                ft.Geometry = GetGeomatryFromGeomatryString(item("RegionPoly").ToString, csysWGS84)
                ft.Style = cStyle
                tblBuffer.InsertFeature(ft)
            Next
            Dim lyr As New FeatureLayer(tblBuffer)
            frmMapWindow.MapControl1.Map.Layers.Add(lyr)
            frmMapWindow.MapControl1.Map.Layers.Move(frmMapWindow.MapControl1.Map.Layers.IndexOf(lyr), Math.Min(frmMapWindow.GetIndexOfLayerName("IOS_TileMap") - 1, frmMapWindow.MapControl1.Map.Layers.Count - 1))
        Catch ex As Exception
        End Try
    End Sub

#End Region

End Class