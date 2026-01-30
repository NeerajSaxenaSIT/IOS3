Imports DevExpress.Utils
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraVerticalGrid
Imports DevExpress.XtraVerticalGrid.Internal
Imports DevExpress.XtraVerticalGrid.Rows

Public Class frmParameterDescription

#Region "Variables"

    Public paramName As String = Nothing
    Public moName As String = Nothing
    Public moTblName As String = Nothing
    Public fromLeft As Integer = Nothing
    Public fromTop As Integer = Nothing
    Private riMemoEdit As RepositoryItemMemoEdit

#End Region

#Region "Events"

    Private Sub frm_ParameterDescription_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.BringToFront()
            Me.StartPosition = FormStartPosition.CenterScreen
            'Me.Location = New Point(fromLeft, fromTop)
            FillParamDescGrid()
            VGridParamDesc.RowHeaderWidth = 40
            AddHandler VGridParamDesc.SizeChanged, AddressOf VGridParamDesc_SizeChanged
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub VGridParamDesc_SizeChanged(sender As Object, e As EventArgs)
        'Dim rowHeaderWidthPercentage As Single = 0.2F
        'Dim totalWidth As Single = (CType(sender, VGridControl)).Width
        VGridParamDesc.RowHeaderWidth = 40
        'VGridParamDesc.RowHeaderWidth = VGridParamDesc.RowHeaderMinWidth  'CInt(totalWidth * rowHeaderWidthPercentage)
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

#End Region

#Region "Methods"

    Private Sub FillParamDescGrid()

        Dim dt_IOS_OSSParams As DataTable = Nothing

        If moName IsNot Nothing Then
            dt_IOS_OSSParams = DataLibrary.clsSQLCommands.GetOSSParamRef(connStrIOSServer, paramName, moName)
        ElseIf moTblName IsNot Nothing Then
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@MOTblName", moTblName},
                New String() {"@ParamName", Chr(39) & paramName & Chr(39)}
            }
            strConnection = GetSQL(3505, parray)(0)
            sqlParam = GetSQL(3505, parray)(1)
            dt_IOS_OSSParams = DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        End If

        VGridParamDesc.Rows.Clear()
        VGridParamDesc.LayoutStyle = LayoutViewStyle.SingleRecordView

        If dt_IOS_OSSParams.Rows.Count > 0 Then
            For Each dtCol As DataColumn In dt_IOS_OSSParams.Columns
                CreateMemoEditCell(dtCol.ColumnName, dt_IOS_OSSParams.Rows(0)(dtCol.ColumnName).ToString)
            Next
        Else
            SetMessage("No Data Found !!!")
        End If

    End Sub

    Private Sub CreateMemoEditCell(colName As String, colValue As String)
        riMemoEdit = New RepositoryItemMemoEdit()
        riMemoEdit.ReadOnly = True
        riMemoEdit.AutoHeight = True
        riMemoEdit.Appearance.TextOptions.WordWrap = WordWrap.Wrap
        riMemoEdit.ScrollBars = ScrollBars.Both
        riMemoEdit.Appearance.Options.UseTextOptions = True
        riMemoEdit.Appearance.TextOptions.Trimming = Trimming.None

        Dim row As EditorRow = VGridParamDesc.Rows.AddEditorRow(colName)
        row.Properties.Caption = colName
        row.Properties.FieldName = colName
        row.Properties.Value = colValue.ToString '.Replace(vbNewLine, " ").Replace(vbCrLf, " ").Replace(Environment.NewLine, " ").Replace(Chr(13), " ").Replace(vbLf, " ")
        row.Properties.RowEdit = riMemoEdit
    End Sub

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    'Private Sub frm_fill()
    '    Try
    '        Dim dt_IOS_OSSParams As DataTable = IOS.DataLibrary.clsSQLCommands.GetOSSParamRef(connStrIOSServer, paramName, moName)

    '        'get row of selected node
    '        Dim drow() As DataRow = dt_IOS_OSSParams.AsEnumerable().Where(Function(x) x.Field(Of String)("P_abbr_name") = paramName AndAlso x.Field(Of String)("Managed_Object") = moName).ToArray()
    '        'building form

    '        If drow.Length > 0 Then
    '            'add tablelayout
    '            tlp.SuspendLayout()
    '            tlp.RowCount = 1
    '            tlp.RowStyles.Clear()
    '            tlp.Controls.Clear()

    '            Dim tlpheight As Integer = 5
    '            Dim rowheight As Single = 21

    '            tlp.ColumnStyles(0).SizeType = SizeType.AutoSize
    '            tlp.ColumnStyles(0).Width = 120.0!

    '            tlp.ColumnStyles(1).SizeType = SizeType.Percent
    '            tlp.ColumnStyles(1).Width = 100.0!

    '            For Each col As DataColumn In dt_IOS_OSSParams.Columns

    '                Dim sl_left As New DevExpress.XtraEditors.LabelControl
    '                sl_left.Text = col.ColumnName.ToString
    '                Dim sl_right As New DevExpress.XtraEditors.LabelControl
    '                sl_right.Text = drow(0)(col).ToString.Replace(vbNewLine, " ").Replace(vbCrLf, " ").Replace(Environment.NewLine, " ").Replace(Chr(13), " ").Replace(vbLf, " ")

    '                If Len(sl_right.Text) > 0 Then

    '                    If sl_right.Text.Length >= 100 Then
    '                        rowheight = rowheight * 5
    '                    Else
    '                        rowheight = 20
    '                    End If

    '                    Me.tlp.RowStyles.Add(New RowStyle(SizeType.Absolute, rowheight))
    '                    tlp.RowCount = tlp.RowCount + 1

    '                    Me.tlp.Controls.Add(sl_left, 0, tlp.RowCount - 2)

    '                    sl_left.Dock = DockStyle.Fill
    '                    sl_left.ForeColor = Color.Black

    '                    sl_right.Dock = DockStyle.Fill
    '                    sl_right.ForeColor = Color.Black

    '                    Me.tlp.Controls.Add(sl_right, 1, tlp.RowCount - 2)

    '                    If sl_right.Text.Length >= 100 Then
    '                        sl_left.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    '                        sl_right.Appearance.Options.UseTextOptions = True
    '                        sl_right.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    '                        sl_right.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
    '                    End If
    '                    tlpheight = tlpheight + sl_right.Size.Height
    '                    tlp.Size = New System.Drawing.Size(CInt(Me.Width), tlpheight)
    '                End If

    '            Next

    '            tlp.ResumeLayout()

    '            'Me.BringToFront()
    '            'Me.StartPosition = FormStartPosition.Manual
    '            'Me.Location = New Point(fromLeft, fromTop)
    '            'Me.ShowDialog()

    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub

#End Region

End Class