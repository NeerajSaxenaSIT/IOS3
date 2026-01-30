Imports dotnetCHARTING.WinForms

Public Class frmTerrainProfile

#Region "Variables Declaration"

    Private ftr As MapInfo.Data.Feature
    Private tiltvalue As Double
    Private cluttervalue As Double
    Private tiltValueDefault As Double
    Private azimuthValueDefault As Double
    Private cluttervalueDefault As Double
    Private cellid As String
    Private cellid_x As String
    Private cellid_y As String
    Private cellid_az As Double
    Private cellid_rc As Double
    Private cellid_ant As String
    Private cellid_et As Double
    Private cellid_mt As Double
    Private recordid As Double
    Private cell_resolution As String
    Private azimuthChanged As Boolean = False

    'Dim an_step As Annotation = Nothing
    Dim an_clutter As Annotation = Nothing
    'Dim an_Downtilt As Annotation = Nothing
    'Dim an_Uptilt As Annotation = Nothing
    Dim an_DownClutter As Annotation = Nothing
    Dim an_UpClutter As Annotation = Nothing

#End Region

#Region "Properties"

    Public Property tp_cellid As String
        Get
            Return cellid
        End Get
        Set(ByVal value As String)
            cellid = value
        End Set
    End Property

    Public Property tp_cellidx As String
        Get
            Return cellid_x
        End Get
        Set(ByVal value As String)
            cellid_x = value
        End Set
    End Property

    Public Property tp_cellidy As String
        Get
            Return cellid_y
        End Get
        Set(ByVal value As String)
            cellid_y = value
        End Set
    End Property

    Public Property tp_cellid_az As Double
        Get
            Return cellid_az
        End Get
        Set(ByVal value As Double)
            cellid_az = value
        End Set
    End Property

    Public Property tp_cellid_rc As Double
        Get
            Return cellid_rc
        End Get
        Set(ByVal value As Double)
            cellid_rc = value
        End Set
    End Property

    Public Property tp_recordid As Double
        Get
            Return recordid
        End Get
        Set(ByVal value As Double)
            recordid = value
        End Set
    End Property

    Public Property tp_cellid_et As Double
        Get
            Return cellid_et
        End Get
        Set(ByVal value As Double)
            cellid_et = value
        End Set
    End Property

    Public Property tp_cellid_mt As Double
        Get
            Return cellid_mt
        End Get
        Set(ByVal value As Double)
            cellid_mt = value
        End Set
    End Property

    Public Property tp_cellid_ant As String
        Get
            Return cellid_ant
        End Get
        Set(ByVal value As String)
            cellid_ant = value
        End Set
    End Property

    Public Property CustomTilt As Double
        Get
            Return tiltvalue
        End Get
        Set(ByVal value As Double)
            tiltvalue = value
        End Set
    End Property

    Public Property CustomClutter As Double
        Get
            Return cluttervalue
        End Get
        Set(ByVal value As Double)
            cluttervalue = value

        End Set
    End Property

    Public Property CustomTiltDefault As Double
        Get
            Return tiltValueDefault
        End Get
        Set(ByVal value As Double)
            tiltValueDefault = value
        End Set
    End Property

    Public Property CustomClutterDefault As Double
        Get
            Return cluttervalueDefault
        End Get
        Set(ByVal value As Double)
            cluttervalueDefault = value
        End Set
    End Property

    Public Property tp_cellResolution As Integer
        Get
            Return cell_resolution
        End Get
        Set(ByVal value As Integer)
            cell_resolution = value
        End Set
    End Property

    Public Property CustomAzimuthDefault As Double
        Get
            Return azimuthValueDefault
        End Get
        Set(ByVal value As Double)
            azimuthValueDefault = value
        End Set
    End Property

#End Region

#Region "Form Event"

    Private Sub ch_TerrainProfile_Click(ByVal sender As Object, ByVal e As MouseEventArgs)
        'get x,y
        Try
            Dim xval As [Object] = Nothing
            Dim hitchart As HitTestInfo = ch_TerrainProfile.HitTest(e.Location)
            If TypeOf hitchart.Object Is Element Then
                Dim el As Element = CType(hitchart.Object, Element)
                xval = el.Name
                Try
                    frmMapWindow.Location_Map("AntennaTiltClick", CDbl(xval.Split(";")(1)), CDbl(xval.Split(";")(0)))
                Catch ex As Exception

                End Try
            ElseIf TypeOf hitchart.Object Is Annotation Then
                Dim an As Annotation = CType(hitchart.Object, Annotation)
                If an.Label.Text = "Step" Then
                    If ch_TerrainProfile.Annotations(0).Label.Text = "+0.5" Then
                        ch_TerrainProfile.Annotations(0).Label.Text = "+1.0"
                        ch_TerrainProfile.Annotations(1).Label.Text = "-1.0"
                    Else
                        ch_TerrainProfile.Annotations(0).Label.Text = "+0.5"
                        ch_TerrainProfile.Annotations(1).Label.Text = "-0.5"
                    End If
                Else
                    Dim newtilt As Double = Nothing
                    Dim newAzimuth As Double = Nothing
                    Dim currenttilt As Double = ch_TerrainProfile.Tag(0)
                    Select Case an.Label.Text

                        Case "+0.5"
                            newtilt = currenttilt + CDbl(an.Label.Text.TrimStart("+"))
                        Case "-0.5"
                            newtilt = currenttilt + CDbl(an.Label.Text.TrimStart("+"))
                        Case "+3m"
                            Me.CustomClutter = cluttervalue + 3
                            newtilt = currenttilt
                        Case "-3m"
                            Me.CustomClutter = cluttervalue - 3
                            newtilt = currenttilt
                        Case "Reset"
                            cluttervalue = CustomClutterDefault
                            newtilt = tiltValueDefault
                            newAzimuth = TerrainAzimuthDefault
                    End Select

                    frmMapWindow.Cell_AntennaTiltCoverage(recordid, cellid, cellid_x, cellid_y, cellid_ant, newAzimuth, cellid_rc, cellid_et, cellid_mt, newtilt, cluttervalue)
                End If
                ch_TerrainProfile.Refresh()
                lblTilt.BackColor = Nothing
                lblTilt.Text = "Tilt"
                lblAzimuth.Text = "Azim"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmTerrainProfile_FormClosed(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        RemoveHandler ch_TerrainProfile.MouseClick, AddressOf ch_TerrainProfile_Click
    End Sub

    Private Sub frmTerrainProfile_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        If Not an_clutter Is Nothing Or Not an_DownClutter Is Nothing Or Not an_UpClutter Is Nothing Then
            'an_step.Position = New System.Drawing.Point(ch_TerrainProfile.Width - 110, 2)
            an_clutter.Position = New System.Drawing.Point(ch_TerrainProfile.Width - 110, 2)
            an_DownClutter.Position = New System.Drawing.Point(ch_TerrainProfile.Width - 72, 2)
            an_UpClutter.Position = New System.Drawing.Point(ch_TerrainProfile.Width - 35, 2)
            'an_Downtilt.Position = New System.Drawing.Point(ch_TerrainProfile.Width - 72, 2)
            'an_Uptilt.Position = New System.Drawing.Point(ch_TerrainProfile.Width - 35, 2)
        End If
    End Sub

    Private Sub tbcETiltSlider_ValueChanged(sender As Object, e As EventArgs) Handles tbcETiltSlider.ValueChanged
        Try
            lbl_EtiltPlanned.Text = Math.Round((CDbl(tbcETiltSlider.EditValue) / 10), 1).ToString("F1")
            txtTilt.Text = Math.Round((CDbl(tbcETiltSlider.EditValue) / 10), 1).ToString("F1")
            azimuthChanged = False
        Catch
        End Try
    End Sub

    Private Sub tbcETiltSlider_MouseUp(sender As Object, e As MouseEventArgs) Handles tbcETiltSlider.MouseUp
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            tbcETiltSlider.Enabled = False

            Dim newtilt As Double = Nothing
            newtilt = CDbl(lbl_EtiltPlanned.Text)

            Dim newAzimuth As Double = Nothing
            newAzimuth = CDbl(lbl_Azimuth.Text)

            frmMapWindow.Cell_AntennaTiltCoverage(recordid, cellid, cellid_x, cellid_y, cellid_ant, newAzimuth, cellid_rc, cellid_et, cellid_mt, newtilt, cluttervalue, 0, azimuthChanged)

            tbcETiltSlider.Enabled = True
            'lblTilt.BackColor = Color.Yellow
            lblTilt.Text = "Reset"

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub cmbResolution_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbResolution.SelectedIndexChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            'Dim tpResolution As Integer = 0
            If (Me.cmbResolution.SelectedItem.ToString.ToLower = "low") Then
                Me.tp_cellResolution = 7
                frmMapWindow.tp_resolution = 7
            ElseIf (Me.cmbResolution.SelectedItem.ToString.ToLower = "medium") Then
                Me.tp_cellResolution = 15
                frmMapWindow.tp_resolution = 15
            ElseIf (Me.cmbResolution.SelectedItem.ToString.ToLower = "high") Then
                Me.tp_cellResolution = 25
                frmMapWindow.tp_resolution = 25
            End If

            Dim newtilt As Double = Nothing
            newtilt = CDbl(lbl_EtiltPlanned.Text)

            frmMapWindow.Cell_AntennaTiltCoverage(recordid, cellid, cellid_x, cellid_y, cellid_ant, cellid_az, cellid_rc, cellid_et, cellid_mt, newtilt, cluttervalue)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tbcAzimuth_ValueChanged(sender As Object, e As EventArgs) Handles tbcAzimuth.ValueChanged
        Try
            lbl_Azimuth.Text = CDbl(tbcAzimuth.EditValue).ToString
            txtAzimuth.Text = CDbl(tbcAzimuth.EditValue).ToString
        Catch
        End Try
    End Sub

    Private Sub tbcAzimuth_MouseUp(sender As Object, e As MouseEventArgs) Handles tbcAzimuth.MouseUp
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            tbcAzimuth.Enabled = False

            Dim newtilt As Double = Nothing
            newtilt = CDbl(lbl_EtiltPlanned.Text)

            Dim newAzimuth As Double = Nothing
            newAzimuth = CDbl(lbl_Azimuth.Text)

            azimuthChanged = True
            TerrainAzimuthDefault = cellid_az
            frmMapWindow.Cell_AntennaTiltCoverage(recordid, cellid, cellid_x, cellid_y, cellid_ant, newAzimuth, cellid_rc, cellid_et, cellid_mt, newtilt, cluttervalue, 0, azimuthChanged)

            tbcAzimuth.Enabled = True
            'lblAzimuth.BackColor = Color.Yellow
            lblAzimuth.Text = "Reset"
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub lblTilt_Click(sender As Object, e As EventArgs) Handles lblTilt.Click
        Try
            If lblTilt.Text = "Reset" Then
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                Dim newtilt As Double = Nothing
                newtilt = tiltValueDefault
                frmMapWindow.Cell_AntennaTiltCoverage(recordid, cellid, cellid_x, cellid_y, cellid_ant, CDbl(tbcAzimuth.EditValue).ToString, cellid_rc, cellid_et, cellid_mt, newtilt, cluttervalue)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            'lblTilt.BackColor = Nothing
            lblTilt.Text = "Tilt"
            Application.DoEvents()
        End Try
    End Sub

    Private Sub lblAzimuth_Click(sender As Object, e As EventArgs) Handles lblAzimuth.Click
        Try
            If lblAzimuth.Text = "Reset" Then
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                Dim newAzimuth As Double = Nothing
                newAzimuth = TerrainAzimuthDefault
                frmMapWindow.Cell_AntennaTiltCoverage(recordid, cellid, cellid_x, cellid_y, cellid_ant, newAzimuth, cellid_rc, cellid_et, cellid_mt, CDbl(lbl_EtiltPlanned.Text), cluttervalue)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            'lblAzimuth.BackColor = Nothing
            lblAzimuth.Text = "Azim"
            Application.DoEvents()
        End Try
    End Sub

    Private Sub txtTilt_KeyUp(sender As Object, e As KeyEventArgs) Handles txtTilt.KeyUp
        Try
            If e.KeyCode = Keys.Enter Then
                If CDbl(txtTilt.Text) >= 0 AndAlso CDbl(txtTilt.Text) <= 150 Then
                    tbcETiltSlider.Value = CDbl(txtTilt.Text) * 10
                    tbcETiltSlider_MouseUp(Nothing, Nothing)
                    lblTilt.Text = "Reset"
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub txtAzimuth_KeyUp(sender As Object, e As KeyEventArgs) Handles txtAzimuth.KeyUp
        Try
            If e.KeyCode = Keys.Enter Then
                If CDbl(txtAzimuth.Text) >= 0 AndAlso CDbl(txtAzimuth.Text) <= 359 Then
                    tbcAzimuth.Value = CDbl(txtAzimuth.Text)
                    tbcAzimuth_MouseUp(Nothing, Nothing)
                    lblAzimuth.Text = "Reset"
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

#End Region

#Region "Helper"

    Public Sub TerrainProfileStart(ByRef dt As DataTable, ByVal totaldist As Double, ByVal cellid As String, ByVal tilt_elec As Double, ByVal tilt_mech As Double, ByVal AntennaInCalculation As String, ByVal radcenter As String, ByVal VAngle As Double, ByVal azimuth As Double)
        Try
            Dim tilts(1) As Double
            tilts(0) = tilt_elec
            tilts(1) = tilt_mech

            ch_TerrainProfile.Tag = tilts
            ch_TerrainProfile.LegendBox.Visible = False

            'configure chart
            ch_TerrainProfile.DefaultElement.Marker.Visible = False
            ch_TerrainProfile.LegendBox.Orientation = dotnetCHARTING.WinForms.Orientation.Bottom
            ch_TerrainProfile.LegendBox.DefaultEntry.Value = ""
            ch_TerrainProfile.XAxis.Clear()
            ch_TerrainProfile.XAxis.Label.Text = "Distance [km]: " & Math.Round(totaldist, 1)

            ch_TerrainProfile.ToolTip.InitialDelay = 1
            ch_TerrainProfile.ChartAreaLayout.Mode = ChartAreaLayoutMode.Horizontal
            ch_TerrainProfile.DefaultSeries.EmptyElement.Mode = EmptyElementMode.None

            ch_TerrainProfile.TitleBox.Label.Text = "CellID: " & cellid & " - ET:" & tilt_elec & "  MT:" & tilt_mech & " H:" & radcenter & "m "
            ch_TerrainProfile.TitleBox.Visible = True
            ch_TerrainProfile.TitleBox.HeaderLabel.Text = "Antenna: " & AntennaInCalculation & "    VBeam: " & Math.Round(VAngle, 1) & "°"
            ch_TerrainProfile.TitleBox.Label.Alignment = StringAlignment.Near
            ch_TerrainProfile.TitleBox.CornerTopLeft = BoxCorner.Round
            ch_TerrainProfile.TitleBox.CornerTopRight = BoxCorner.Round

            If Not Me.Visible Then
                Me.CustomClutter = 0
                Me.DoubleBuffered = True

                Dim an_reset As Annotation = New Annotation("Reset")
                'an_step = New Annotation("Tilt")
                an_clutter = New Annotation("Clutter")
                'an_Downtilt = New Annotation("+0.5")
                'an_Uptilt = New Annotation("-0.5")

                an_reset.Background.Color = Color.LightGray
                'an_step.Background.Color = Color.LightGray
                'an_Downtilt.Background.Color = Color.LightGray
                'an_Uptilt.Background.Color = Color.LightGray
                'an_Downtilt.DefaultCorner = BoxCorner.Round
                'an_Uptilt.DefaultCorner = BoxCorner.Round
                an_reset.DefaultCorner = BoxCorner.Round

                an_DownClutter = New Annotation("+3m")
                an_UpClutter = New Annotation("-3m")

                an_DownClutter.Background.Color = Color.LightGray
                an_UpClutter.Background.Color = Color.LightGray
                an_DownClutter.DefaultCorner = BoxCorner.Round
                an_UpClutter.DefaultCorner = BoxCorner.Round
                an_DownClutter.Position = New System.Drawing.Point(ch_TerrainProfile.Width - 72, 2)
                an_UpClutter.Position = New System.Drawing.Point(ch_TerrainProfile.Width - 35, 2)
                an_clutter.Position = New System.Drawing.Point(ch_TerrainProfile.Width - 110, 2)
                an_reset.Position = New System.Drawing.Point(4, 2)

                'an_step.DefaultCorner = BoxCorner.Square
                'an_step.Background.Color = Color.White
                an_clutter.DefaultCorner = BoxCorner.Square
                an_clutter.Background.Color = Color.White
                an_clutter.Line.Color = Color.White
                an_clutter.Shadow.Visible = False

                an_clutter.ToolTip = "Change Clutter Value"
                an_reset.ToolTip = "Reset To Real Value"
                'an_step.ToolTip = "Change Step Value"
                'an_step.Line.Color = Color.White
                'an_step.Shadow.Visible = False

                'an_step.Position = New System.Drawing.Point(ch_TerrainProfile.Width - 110, 2)
                'an_Downtilt.Position = New System.Drawing.Point(ch_TerrainProfile.Width - 72, 2)
                'an_Uptilt.Position = New System.Drawing.Point(ch_TerrainProfile.Width - 35, 2)

                'an_Downtilt.Size = New Size(32, 20)
                'an_Uptilt.Size = New Size(32, 20)
                an_DownClutter.Size = New Size(32, 20)
                an_UpClutter.Size = New Size(32, 20)

                ch_TerrainProfile.Annotations.Clear()
                'ch_TerrainProfile.Annotations.Add(an_Downtilt)
                'ch_TerrainProfile.Annotations.Add(an_Uptilt)
                'ch_TerrainProfile.Annotations.Add(an_step)
                ch_TerrainProfile.Annotations.Add(an_clutter)
                ch_TerrainProfile.Annotations.Add(an_DownClutter)
                ch_TerrainProfile.Annotations.Add(an_UpClutter)
                ch_TerrainProfile.Annotations.Add(an_reset)

                ch_TerrainProfile.DefaultElement.Hotspot.ToolTip = "%SeriesName: %Value "

                AddHandler ch_TerrainProfile.MouseClick, AddressOf ch_TerrainProfile_Click
            End If

            Dim yaxis1 As Axis = New Axis
            yaxis1.Orientation = Orientation.Left
            yaxis1.Label.Text = "Elevation [m]"

            'getting min max elevation for yaxis
            Dim min_elev As Double = 100000
            Dim max_elev As Double = -100000
            For Each dr As DataRow In dt.Rows
                min_elev = Math.Min(min_elev, dr("elevation"))
                max_elev = Math.Max(max_elev, dt(0)("upperbeam_height") * 1.15)
            Next
            yaxis1.ScaleRange.ValueHigh = max_elev
            yaxis1.ScaleRange.ValueLow = min_elev

            Dim de As DataEngine = New DataEngine(dt)

            de.DataFields = String2DataFields({"elevation", "upperbeam_height", "meanbeam_height", "lowerbeam_height", "clutter_height"}, "location")
            de.DataGridFormatString = "N2"

            Dim sc As New SeriesCollection
            sc = de.GetSeries()

            sc(0).Type = SeriesType.AreaLine
            sc(0).YAxis = yaxis1

            sc(1).Type = SeriesType.Line
            sc(1).YAxis = yaxis1
            sc(1).DefaultElement.Color = Color.DarkRed
            sc(2).Type = SeriesType.Line
            sc(2).YAxis = yaxis1
            sc(2).DefaultElement.Color = Color.Red
            sc(3).Type = SeriesType.Line
            sc(3).YAxis = yaxis1
            sc(3).DefaultElement.Color = Color.OrangeRed
            sc(4).Type = SeriesType.Line
            sc(4).YAxis = yaxis1
            sc(4).DefaultElement.Color = Color.DarkGray
            ch_TerrainProfile.SeriesCollection.Clear()
            ch_TerrainProfile.SeriesCollection.Add(sc)

            'sp = Nothing
            sc = Nothing
            de = Nothing
            ch_TerrainProfile.XAxis.Markers.Clear()
            ch_TerrainProfile.RefreshChart()
            ch_TerrainProfile.ResumeLayout()

            tbcETiltSlider.EditValue = tilt_elec * 10
            tbcAzimuth.EditValue = azimuth

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            If Not ch_TerrainProfile Is Nothing Then
                '-----------
                'License Key
                '-----------
                ch_TerrainProfile.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
            End If
        End Try
        _logger.SetError("Terrain Plot Complete")
        If Me.Visible = False Then
            Me.TopLevel = False
            Me.TopMost = True
            frmMDI.Controls.Add(Me)
            Me.Top = frmMapWindow.Location.Y + frmMapWindow.Size.Height - Me.Size.Height + 45
            Me.Left = frmMapWindow.Location.X + frmMapWindow.accMapExplorerBar.Width + 20
            Me.Show()
            Me.BringToFront()
        End If
        _logger.SetError("Terrain Window Management Complete")
    End Sub

    Public Sub OnParentFormMove()
        Me.BringToFront()
    End Sub

#End Region

End Class