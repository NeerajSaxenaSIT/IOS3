Imports System.Windows.Forms

Public Class IOSToggleButton

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.ToggleState = CheckState.Unchecked
    End Sub

    Private _toggleState As CheckState
    Public Property ToggleState() As CheckState
        Get
            Return _toggleState
        End Get
        Set(ByVal value As CheckState)
            _toggleState = value
            Me.LookAndFeel.UseDefaultLookAndFeel = False
            If value Then
                Me.LookAndFeel.SkinName = "McSkin"
                Me.LookAndFeel.SetSkinMaskColors(Drawing.Color.Orange, Drawing.Color.Red)
                'Me.ForeColor = Drawing.Color.OrangeRed
            Else
                Me.LookAndFeel.SkinName = "McSkin"
                Me.LookAndFeel.ResetSkinMaskColors()
                'Me.ForeColor = Drawing.Color.Black
            End If
        End Set
    End Property

    Public Sub ChangeToggleState()
        If Me.ToggleState = CheckState.Checked Then
            Me.ToggleState = CheckState.Unchecked
        Else
            Me.ToggleState = CheckState.Checked
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)

        'Add your custom paint code here
    End Sub

End Class
