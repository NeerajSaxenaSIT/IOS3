Public Class ucChartHistMenuItem

    Public percentile As Integer
    Public project As String

    Public Sub New(ByVal direction As String, ByVal defaultValue As Integer)
        InitializeComponent()
        RemoveHandler txtUnderflowPercentile.TextChanged, AddressOf txtUnderflowPercentile_TextChanged
        LabelControl1.Text = direction
        percentile = defaultValue
        txtUnderflowPercentile.Text = defaultValue.ToString
        AddHandler txtUnderflowPercentile.TextChanged, AddressOf txtUnderflowPercentile_TextChanged
    End Sub

    Private Sub txtUnderflowPercentile_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUnderflowPercentile.KeyPress
        e.Handled = IsNumeric(txtUnderflowPercentile.Text)
        If Asc(e.KeyChar) > 0 And Asc(e.KeyChar) < 100 Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtUnderflowPercentile_TextChanged(sender As Object, e As EventArgs)
        If txtUnderflowPercentile.Text.Trim <> "" Then
            percentile = CInt(txtUnderflowPercentile.Text.Trim)
        Else
            percentile = 0
        End If

        If project.ToUpper = "EVAL" Then
            If LabelControl1.Text.Contains("Under") Then
                underFlowPercentileEval = percentile
            Else
                overFlowPercentileEval = percentile
            End If
        Else
            If LabelControl1.Text.Contains("Under") Then
                underFlowPercentile = percentile
            Else
                overFlowPercentile = percentile
            End If
        End If

    End Sub

End Class
