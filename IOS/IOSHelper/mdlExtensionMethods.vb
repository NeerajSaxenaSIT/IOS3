Imports System
Imports System.Reflection
Imports System.Windows.Forms
Imports IOS
Imports System.Runtime.CompilerServices

Public Module mdlExtensionMethods

    Public Sub DoubleBuffered(ByVal dgv As DataGridView, ByVal setting As Boolean)
        Dim dgvType As Type = dgv.[GetType]()
        Dim pi As PropertyInfo = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        pi.SetValue(dgv, setting, Nothing)
    End Sub

    <Extension()> _
    Public Sub ClearAll(ByVal dgv As dotnetCHARTING.WinForms.Chart)
        dgv.SuspendLayout()
        dgv.SeriesCollection.Clear()
        dgv.ResetToParent()
        dgv.Update()
        dgv.ResumeLayout()
    End Sub

    <Extension()> _
    Public Sub ResetToParent(ByVal dgv As dotnetCHARTING.WinForms.Chart)
        Dim parent As Control = dgv.Parent
        If (parent IsNot Nothing) Then
            dgv.Width = parent.Width - 10
        End If
    End Sub

    <Extension()> _
    Public Function GetString(ByVal o As Object) As String
        If (o.Equals(DBNull.Value)) Then
            Return ""
        Else
            Return o.ToString()
        End If
    End Function

    <Extension()> _
    Public Sub ConfigurControl(ByVal dgv As System.Windows.Forms.Control, ByRef form As IOS.Configuration.EntityModel.IOSForm)
        Dim control As IOS.Configuration.EntityModel.Control = form.FindControlByName(dgv.Name)

        If Not control Is Nothing Then
            If control.ConfigType = Configuration.EntityModel.ConfigType.Hidden Then
                dgv.Visible = False
            End If
            If control.ConfigType = Configuration.EntityModel.ConfigType.Enable Then
                dgv.Enabled = True
            ElseIf control.ConfigType = Configuration.EntityModel.ConfigType.Disable Then
                dgv.Enabled = False
            End If
        End If
    End Sub

    <Extension()> _
    Public Sub ConfigurControl(ByVal dgv As System.Windows.Forms.Control, ByRef type As IOS.Configuration.EntityModel.ConfigType)
        Dim t As Type = dgv.GetType()
        If type = Configuration.EntityModel.ConfigType.Hidden Then
            If (t.Name = "XtraTabPage") Then
                Dim page As DevExpress.XtraTab.XtraTabPage = TryCast(dgv, DevExpress.XtraTab.XtraTabPage)
                If (page IsNot Nothing) Then
                    page.PageVisible = False
                End If
            End If
            dgv.Visible = False
        Else
            If (t.Name = "XtraTabPage") Then
                Dim page As DevExpress.XtraTab.XtraTabPage = TryCast(dgv, DevExpress.XtraTab.XtraTabPage)
                If (page IsNot Nothing) Then
                    page.PageVisible = True
                End If
            End If
            dgv.Visible = True
        End If
        If type = Configuration.EntityModel.ConfigType.Enable Then
            If (t.Name = "XtraTabPage") Then
                Dim page As DevExpress.XtraTab.XtraTabPage = TryCast(dgv, DevExpress.XtraTab.XtraTabPage)
                If (page IsNot Nothing) Then
                    page.PageEnabled = True
                End If
            End If
            dgv.Enabled = True
        ElseIf type = Configuration.EntityModel.ConfigType.Disable Then
            If (t.Name = "XtraTabPage") Then
                Dim page As DevExpress.XtraTab.XtraTabPage = TryCast(dgv, DevExpress.XtraTab.XtraTabPage)
                If (page IsNot Nothing) Then
                    page.PageEnabled = False
                End If
            End If
            dgv.Enabled = False
        End If
    End Sub

    <Extension()> _
    Public Function GetDecimalString(ByVal value As String) As String
        Dim IsAlphabet As Boolean = False
        Dim alp() As String = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"}
        If (value.Contains(".") Or value.Contains(",")) Then
            For Each Character As Char In value
                If alp.Contains(Character) Then
                    IsAlphabet = True
                    Exit For
                End If
            Next

            If (Not IsAlphabet) Then
                Dim decimalSeparator As String = Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
                value = value.Replace(".", decimalSeparator).Replace(",", decimalSeparator)
            End If
        End If
        ' End If
        Return value
    End Function

    <Extension()> _
    Public Function ToRangesTheme(ByVal modifier As MapInfo.Mapping.FeatureStyleModifier) As MapInfo.Mapping.Thematics.RangedTheme
        Return TryCast(modifier, MapInfo.Mapping.Thematics.RangedTheme)
    End Function

    <Extension()> _
    Public Function IsRangesTheme(ByVal modifier As MapInfo.Mapping.FeatureStyleModifier) As Boolean
        Dim selectedModifier As String = modifier.GetType().Name
        Return selectedModifier = "RangedTheme"
    End Function

    <Extension()> _
    Public Function ToIndividualValueTheme(ByVal modifier As MapInfo.Mapping.FeatureStyleModifier) As MapInfo.Mapping.Thematics.IndividualValueTheme
        Return TryCast(modifier, MapInfo.Mapping.Thematics.IndividualValueTheme)
    End Function

    <Extension()> _
    Public Function IsIndividualValueTheme(ByVal modifier As MapInfo.Mapping.FeatureStyleModifier) As Boolean
        Dim selectedModifier As String = modifier.GetType().Name
        Return selectedModifier = "IndividualValueTheme"
    End Function

End Module
