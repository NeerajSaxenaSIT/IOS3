Public Class WaitFormDataMart

    Sub New()
        InitializeComponent()
        'Me.ucProgressReport.AutoHeight = True
    End Sub

    Public Overrides Sub SetCaption(ByVal caption As String)
        MyBase.SetCaption(caption)
        'Me.ucProgressReport.Caption = caption
    End Sub

    Public Overrides Sub SetDescription(ByVal description As String)
        MyBase.SetDescription(description)
        'Me.ucProgressReport.Description = description
    End Sub

    Public Overrides Sub ProcessCommand(ByVal cmd As System.Enum, ByVal arg As Object)
        MyBase.ProcessCommand(cmd, arg)
    End Sub

    Public Enum WaitFormCommand
        SomeCommandId
    End Enum

    Private Sub btnAbort_Click(sender As Object, e As EventArgs) Handles btnAbort.Click
        IOS.Library.ReportChartGrid.reportAbort = True
        Me.Close()
    End Sub

End Class
