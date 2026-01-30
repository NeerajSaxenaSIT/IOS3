Public Class dlgTechExportOption

    Public CellBasedExport As Boolean = False
    'Public PeriodSelectionAggr As Boolean = False
    Public WholePeriodAggr As Boolean = False
    Public fileDelimiter As String = Nothing

    Private Sub dlgTechExportOption_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            rbTargetType.Checked = True
            rbPeriodSelectionAggr.Checked = True
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If String.IsNullOrEmpty(txtSelectPath.Text) Then
                SetMessage("Please select a folder")
                Exit Sub
            End If

            If rbTargetType.Checked = True Then
                CellBasedExport = False
            ElseIf rbCellBased.Checked = True Then
                CellBasedExport = True
            End If

            If rbPeriodSelectionAggr.Checked = True Then
                WholePeriodAggr = False
            ElseIf rbWholePeriodAggr.Checked = True Then
                WholePeriodAggr = True
            End If

            techExportFilePath = txtSelectPath.Text

            Me.fileDelimiter = cmbDelimiter.SelectedItem.ToString()
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub txtSelectPath_Properties_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtSelectPath.Properties.ButtonClick
        Try
            Dim objFBD As New FolderBrowserDialog()

            If techExportFilePath Is Nothing Then
                objFBD.SelectedPath = IO.Directory.GetCurrentDirectory()
            Else
                objFBD.SelectedPath = techExportFilePath
            End If

            If objFBD.ShowDialog() = DialogResult.OK Then
                techExportFilePath = objFBD.SelectedPath
                txtSelectPath.Text = techExportFilePath
            Else
                techExportFilePath = Nothing
                txtSelectPath.Text = ""
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        lblMessage.Text = ""
        lblMessage.Visible = False
        RemoveHandler Timer1.Tick, AddressOf Timer1_Tick
        Timer1.Enabled = False
        Timer1.Stop()
        Me.Cursor = Cursors.Default
        Application.DoEvents()
    End Sub

End Class