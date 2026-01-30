Public Class frmLegend

    Private defaultEX As Integer = -1
    Private enableFormLevelDoubleBuffering As Boolean = True

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams

            If defaultEX = -1 Then
                defaultEX = cp.ExStyle
            End If
            If enableFormLevelDoubleBuffering = True Then
                cp.ExStyle = cp.ExStyle Or &H2000000
            Else
                cp.ExStyle = defaultEX
            End If
            Return cp
        End Get
    End Property

End Class