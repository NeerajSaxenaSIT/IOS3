Public Class dlgProjectInfoPCHR

    Private _conStr As String = IOS.Configuration.IOSAppConfigManage.IOSServer
    Private _projectId As Integer = 0

    Public Property ConnString() As String
        Get
            Return _conStr
        End Get
        Set(ByVal value As String)
            _conStr = value
        End Set
    End Property

    Public Property ProjectId() As Integer
        Get
            Return _projectId
        End Get
        Set(ByVal value As Integer)
            _projectId = value
        End Set
    End Property

    Public Sub frm_fill()
        Try
            Dim dt_IOS_OSSParams As DataTable = IOS.DataLibrary.clsSQLCommands.GetPCHRProjectData(_conStr, _projectId)

            'get row of selected node
            Dim drow() As DataRow = dt_IOS_OSSParams.Select("")
            'building form

            'add table layout
            tlp.SuspendLayout()
            tlp.RowCount = 1
            tlp.RowStyles.Clear()
            tlp.Controls.Clear()

            Dim tlpheight As Integer = 5
            Dim rowheight As Single = 21
            tlp.ColumnStyles(0).SizeType = SizeType.AutoSize
            tlp.ColumnStyles(0).Width = 120.0!

            tlp.ColumnStyles(1).SizeType = SizeType.AutoSize

            For Each col As DataColumn In dt_IOS_OSSParams.Columns
                Dim sl_left As DevExpress.XtraEditors.LabelControl = New DevExpress.XtraEditors.LabelControl
                sl_left.Text = col.ColumnName.ToString
                Dim sl_right As DevExpress.XtraEditors.LabelControl = New DevExpress.XtraEditors.LabelControl
                sl_right.Text = drow(0)(col).ToString.Replace(vbNewLine, " ").Replace(vbCrLf, " ").Replace(Environment.NewLine, " ").Replace(Chr(13), " ").Replace(vbLf, " ")
                If Len(sl_right.Text) > 0 Then
                    rowheight = 20

                    Me.tlp.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, rowheight))
                    tlp.RowCount = tlp.RowCount + 1

                    Me.tlp.Controls.Add(sl_left, 0, tlp.RowCount - 2)
                    sl_left.Dock = DockStyle.Fill
                    sl_left.ForeColor = Color.Black
                    sl_right.Dock = DockStyle.Fill
                    sl_right.ForeColor = Color.Black
                    Me.tlp.Controls.Add(sl_right, 1, tlp.RowCount - 2)
                    tlpheight = tlpheight + sl_right.Size.Height
                    tlp.Size = New System.Drawing.Size(CInt(Me.Width), tlpheight)
                End If
            Next
            tlp.ResumeLayout()
            Me.BringToFront()
            Me.ShowDialog()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DialogProjectInfoPCHR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim dt_IOS_OSSParams As DataTable = IOS.DataLibrary.clsSQLCommands.GetPCHRProjectData(_conStr, _projectId)
            tlp.AutoScroll = True
            tlp.Dock = DockStyle.Fill
            'get row of selected node
            Dim drow() As DataRow = dt_IOS_OSSParams.Select("")
            'building form

            'add table layout
            tlp.SuspendLayout()
            tlp.RowCount = 1
            tlp.RowStyles.Clear()
            tlp.Controls.Clear()

            Dim tlpheight As Integer = 5
            Dim rowheight As Single = 21
            tlp.ColumnStyles(0).SizeType = SizeType.AutoSize
            tlp.ColumnStyles(0).Width = 120.0!

            tlp.ColumnStyles(1).SizeType = SizeType.AutoSize

            For Each col As DataColumn In dt_IOS_OSSParams.Columns
                Dim sl_left As DevExpress.XtraEditors.LabelControl = New DevExpress.XtraEditors.LabelControl
                sl_left.Text = col.ColumnName.ToString
                Dim sl_right As DevExpress.XtraEditors.LabelControl = New DevExpress.XtraEditors.LabelControl
                sl_right.Text = drow(0)(col).ToString.Replace(vbNewLine, " ").Replace(vbCrLf, " ").Replace(Environment.NewLine, " ").Replace(Chr(13), " ").Replace(vbLf, " ")
                If Len(sl_right.Text) > 0 Then
                    rowheight = 20

                    Me.tlp.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, rowheight))
                    tlp.RowCount = tlp.RowCount + 1

                    Me.tlp.Controls.Add(sl_left, 0, tlp.RowCount - 2)
                    sl_left.Dock = DockStyle.Fill
                    sl_left.ForeColor = Color.Black
                    sl_right.Dock = DockStyle.Fill
                    sl_right.ForeColor = Color.Black
                    Me.tlp.Controls.Add(sl_right, 1, tlp.RowCount - 2)
                    tlpheight = tlpheight + sl_right.Size.Height
                    tlp.Size = New System.Drawing.Size(CInt(Me.Width), tlpheight)
                End If
            Next
            tlp.ResumeLayout()
            Me.BringToFront()
            Me.ShowDialog()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

End Class