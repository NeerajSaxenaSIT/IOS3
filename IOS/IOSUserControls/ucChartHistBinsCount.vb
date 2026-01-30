Public Class ucChartHistBinsCount

    Public BinsCount As Integer
    Public project As String

    Public Sub New(ByVal defaultValue As Integer)
        InitializeComponent()
        RemoveHandler txtHistChartBinsCount.TextChanged, AddressOf txtHistChartBinsCount_TextChanged
        BinsCount = defaultValue
        txtHistChartBinsCount.Text = defaultValue.ToString
        AddHandler txtHistChartBinsCount.TextChanged, AddressOf txtHistChartBinsCount_TextChanged
    End Sub

    Private Sub txtHistChartBinsCount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHistChartBinsCount.KeyPress
        e.Handled = IsNumeric(txtHistChartBinsCount.Text)
        If Asc(e.KeyChar) > 0 And Asc(e.KeyChar) < 100 Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtHistChartBinsCount_TextChanged(sender As Object, e As EventArgs)
        If txtHistChartBinsCount.Text.Trim <> "" Then
            BinsCount = CInt(txtHistChartBinsCount.Text.Trim)
        Else
            BinsCount = 50
        End If

        If project.ToUpper = "EVAL" Then
            histChartBinsCountEval = BinsCount
        Else
            histChartBinsCount = BinsCount
        End If

    End Sub

End Class
