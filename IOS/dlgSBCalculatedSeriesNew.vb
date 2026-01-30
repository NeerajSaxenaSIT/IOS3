Imports IOS.DataLibrary
Imports IOS.Library

Public Class dlgSBCalculatedSeriesNew

    Dim conn_SandBox As String = IOS.Configuration.IOSAppConfigManage.SandBox_Server
    Private _StatisticsOrThresholdType As StatisticsOrThreshold = Nothing

    Private Sub dialog_CalculatedSeriesNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            _CalculatedSeriesTypeParameters = String.Empty
            If (Not _CalculatedSeriesTypeID = String.Empty) Then
                If (_StatisticsOrThresholdType = StatisticsOrThreshold.Statistics) Then
                    BindCalculatedSeriesTypes()
                ElseIf (_StatisticsOrThresholdType = StatisticsOrThreshold.Threshold) Then
                    BindThresholdTypes()
                End If
            Else
                Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
                Me.Close()
            End If
        Catch ex As Exception
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Try
    End Sub

    Public Property StatisticsOrThresholdType() As StatisticsOrThreshold
        Get
            Return _StatisticsOrThresholdType
        End Get
        Set(value As StatisticsOrThreshold)
            _StatisticsOrThresholdType = value
        End Set
    End Property

    Private _CalculatedSeriesTypeID As String
    Public Property CalculatedSeriesTypeID() As String
        Get
            Return _CalculatedSeriesTypeID
        End Get
        Set(value As String)
            _CalculatedSeriesTypeID = value
        End Set
    End Property

    Public Sub SetConnectionString(ByVal connstr As String)
        conn_SandBox = connstr
    End Sub

    Private _CalculatedSeriesTypeParameters As String
    Public Property CalculatedSeriesTypeParameters() As String
        Get
            Return _CalculatedSeriesTypeParameters
        End Get
        Set(value As String)
            _CalculatedSeriesTypeParameters = value
        End Set
    End Property

    Private Sub BindThresholdTypes()
        exTLP_main.SuspendLayout()
        Dim exTLP_inner As IOS.Library.ExTableLayoutPanel = New IOS.Library.ExTableLayoutPanel
        exTLP_inner.Name = "exTLP_inner"
        Dim thresholdTypeDt As DataTable = DataAccessorODBC.GetDataTable(conn_SandBox, SQLCalculatedSeriesTypes.GetByID(_CalculatedSeriesTypeID))
        If (thresholdTypeDt.Rows.Count > 0) Then
            exTLP_inner.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            exTLP_inner.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            exTLP_inner.Dock = System.Windows.Forms.DockStyle.Top
            ''exTLP_inner.Location = New System.Drawing.Point(7, 29)
            exTLP_inner.Name = "exTLP_inner"
            exTLP_inner.RowCount = thresholdTypeDt.Rows.Count

            Dim rowIndex As Integer = 0
            Dim parameters As String = thresholdTypeDt.Rows(0)(CalculatedSeriesTypesFields.Calculated_Series_Type_Parameters).ToString
            ''exTLP_inner.ColumnCount = listOfParameters.Count
            Dim calculatedSeriesTypeName As String = thresholdTypeDt.Rows(0)(CalculatedSeriesTypesFields.Calculated_Series_Type_Name).ToString
            Me.Text = calculatedSeriesTypeName
            'For Each parameter As String In listOfParameters
            exTLP_inner.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))

            Dim vlbl_Series As New DevExpress.XtraEditors.LabelControl()
            vlbl_Series.BackColor = System.Drawing.Color.Transparent
            'vlbl_Series.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
            'vlbl_Series.Ellipsis = False
            'vlbl_Series.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
            'vlbl_Series.Multiline = True
            vlbl_Series.Name = "vlbl_Series" & parameters
            vlbl_Series.Size = New System.Drawing.Size(179, 14)
            vlbl_Series.TabIndex = 4
            vlbl_Series.Text = parameters
            vlbl_Series.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
            vlbl_Series.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default
            'vlbl_Series.TextAlignment = System.Drawing.ContentAlignment.TopLeft
            'vlbl_Series.UseMnemonics = True
            'vlbl_Series.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICEBLACK
            vlbl_Series.Dock = System.Windows.Forms.DockStyle.Fill
            vlbl_Series.ForeColor = Color.Black
            exTLP_inner.Controls.Add(vlbl_Series, 0, rowIndex)

            Dim vtxt_Series As New DevExpress.XtraEditors.TextEdit()

            vtxt_Series.BackColor = System.Drawing.Color.White
            'vtxt_Series.BoundsOffset = New System.Drawing.Size(1, 1)
            'vtxt_Series.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
            'vtxt_Series.DefaultText = ""
            vtxt_Series.Dock = System.Windows.Forms.DockStyle.Top
            vtxt_Series.ForeColor = System.Drawing.Color.Gray
            vtxt_Series.Location = New System.Drawing.Point(185, 3)
            'vtxt_Series.MaxLength = 1000
            vtxt_Series.Name = "vtxt_Series" & parameters
            'vtxt_Series.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
            'vtxt_Series.ScrollBars = System.Windows.Forms.ScrollBars.None
            vtxt_Series.SelectionLength = 0
            vtxt_Series.SelectionStart = 0
            vtxt_Series.Size = New System.Drawing.Size(176, 23)
            vtxt_Series.TabIndex = 1
            'vtxt_Series.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
            'vtxt_Series.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICEBLACK

            AddHandler vtxt_Series.KeyPress, AddressOf vtxt_Series_KeyPress
            exTLP_inner.Controls.Add(vtxt_Series, 1, rowIndex)

            exTLP_inner.Refresh()

            'CreateControls(exTLP_inner, listOfParameters, rowIndex)
            'rowIndex += 1
            'Next
            exTLP_inner.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))

            exTLP_inner.TabIndex = 40
            Me.Size = New Size(Me.Width, (rowIndex * 30) + 125)
            exTLP_main.Controls.Add(exTLP_inner, 0, 0)
        End If
        exTLP_main.Controls.Add(exTLP_inner)
        exTLP_main.ResumeLayout()
        exTLP_main.Update()
        exTLP_main.Refresh()
    End Sub

    Private Sub vtxt_Series_KeyPress(sender As Object, e As KeyPressEventArgs)
        e.Handled = SandBoxTextBox.IsNumberVal(e.KeyChar)
        'If (vtxt_SeriesTopX.Text.Length = 0) Then
        '    vtxt_SandBoxTopX.Text = 0
        'End If
    End Sub

    Private Sub BindCalculatedSeriesTypes()
        exTLP_main.SuspendLayout()
        Dim exTLP_inner As IOS.Library.ExTableLayoutPanel = New IOS.Library.ExTableLayoutPanel
        exTLP_inner.Name = "exTLP_inner"
        Dim calculatedSeriesTypesDt As DataTable = DataAccessorODBC.GetDataTable(conn_SandBox, SQLCalculatedSeriesTypes.GetByID(_CalculatedSeriesTypeID))
        If (calculatedSeriesTypesDt.Rows.Count > 0) Then
            exTLP_inner.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            exTLP_inner.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            exTLP_inner.Dock = System.Windows.Forms.DockStyle.Fill
            exTLP_inner.Name = "exTLP_inner"
            ''exTLP_inner.BackColor = Color.Green
            Dim rowIndex As Integer = 0
            Dim listOfParameters As String() = calculatedSeriesTypesDt.Rows(0)(CalculatedSeriesTypesFields.Calculated_Series_Type_Parameters).ToString.Split(";")
            Dim calculatedSeriesTypeName As String = calculatedSeriesTypesDt.Rows(0)(CalculatedSeriesTypesFields.Calculated_Series_Type_Name).ToString
            Me.Text = calculatedSeriesTypeName
            For Each parameter As String In listOfParameters
                CreateControls(exTLP_inner, parameter, exTLP_inner.RowStyles.Count)
                exTLP_inner.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
                rowIndex += 1
            Next
            exTLP_inner.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            exTLP_main.Controls.Add(exTLP_inner, 0, 0)
            Me.Size = New Size(Me.Width, (rowIndex * 30) + 100)
        End If
        exTLP_main.ResumeLayout()
        exTLP_main.Update()
        exTLP_main.Refresh()
    End Sub

    Private Sub CreateControls(ByRef exTLP_inner As System.Windows.Forms.TableLayoutPanel, ByVal nameOfParameter As String, ByVal rowIndex As Integer)
        Dim vlbl_Series As New DevExpress.XtraEditors.LabelControl()
        vlbl_Series.BackColor = System.Drawing.Color.Transparent
        'vlbl_Series.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        'vlbl_Series.Ellipsis = False
        'vlbl_Series.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        'vlbl_Series.Multiline = True
        vlbl_Series.Name = "vlbl_Series" & nameOfParameter.Replace(" ", "")
        vlbl_Series.Size = New System.Drawing.Size(179, 14)
        vlbl_Series.TabIndex = 4
        vlbl_Series.Text = nameOfParameter
        vlbl_Series.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        vlbl_Series.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default
        'vlbl_Series.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        'vlbl_Series.UseMnemonics = True
        'vlbl_Series.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICEBLACK
        vlbl_Series.Dock = System.Windows.Forms.DockStyle.Fill
        vlbl_Series.ForeColor = Color.Black
        exTLP_inner.Controls.Add(vlbl_Series, 0, rowIndex)

        Dim vtxt_Series As New DevExpress.XtraEditors.TextEdit()
        vtxt_Series.BackColor = System.Drawing.Color.White
        'vtxt_Series.BoundsOffset = New System.Drawing.Size(1, 1)
        'vtxt_Series.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        'vtxt_Series.DefaultText = ""
        vtxt_Series.Dock = System.Windows.Forms.DockStyle.Top
        vtxt_Series.ForeColor = System.Drawing.Color.Gray
        vtxt_Series.Location = New System.Drawing.Point(185, 3)
        'vtxt_Series.MaxLength = 1000
        vtxt_Series.Name = "vtxt_Series" & nameOfParameter.Replace(" ", "")
        'vtxt_Series.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        'vtxt_Series.ScrollBars = System.Windows.Forms.ScrollBars.None
        vtxt_Series.SelectionLength = 0
        vtxt_Series.SelectionStart = 0
        vtxt_Series.Size = New System.Drawing.Size(176, 23)
        vtxt_Series.TabIndex = 1
        'vtxt_Series.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        'vtxt_Series.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICEBLACK

        exTLP_inner.Controls.Add(vtxt_Series, 1, rowIndex)
        exTLP_inner.Refresh()
    End Sub

    Private Sub vbtn_AddSeries_Click(sender As Object, e As EventArgs) Handles vbtn_AddSeries.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim result As String = String.Empty
            Dim exTLP_inner As TableLayoutPanel = TryCast(exTLP_main.Controls(2), TableLayoutPanel)

            Dim isFilledAllTxt As Boolean = False
            If (exTLP_inner IsNot Nothing) Then
                For Each controlTemp As Control In exTLP_inner.Controls
                    Dim vtxt_Series As DevExpress.XtraEditors.TextEdit = TryCast(controlTemp, DevExpress.XtraEditors.TextEdit)
                    If (vtxt_Series IsNot Nothing) Then
                        If (vtxt_Series.Text.Trim.Length > 0) Then
                            If (result = String.Empty) Then
                                result = vtxt_Series.Text & ";"
                            Else
                                result = result & vtxt_Series.Text & ";"
                            End If
                            isFilledAllTxt = True
                        Else
                            result = String.Empty
                            isFilledAllTxt = False
                            Exit For
                        End If
                    End If
                Next
                If (isFilledAllTxt) Then
                    _CalculatedSeriesTypeParameters = result.Remove(result.Length - 1, 1)
                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
                    Me.Close()
                Else
                    vlblMSG.Text = "Please enter all Parameters value."
                    vlblMSG.ForeColor = Color.Red
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub vbtn_Cancel_Click(sender As Object, e As EventArgs) Handles vbtn_Cancel.Click
        _CalculatedSeriesTypeParameters = String.Empty
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class