Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Imports System.Text.RegularExpressions

Public Module DataTableExtensions

#Region "Extension Table"
    <Extension()> _
    Public Function IsValid(ByVal dt As DataTable) As Boolean
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Return True
        Else
            Return False
        End If
    End Function

    <Extension()> _
    Public Function DistinctCol(ByVal dt As DataTable, ByVal distinctColumn As String) As DataTable
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Return dt.DefaultView.ToTable(True, distinctColumn)
        Else
            Return dt
        End If
    End Function

    <Extension()> _
    Public Function DistinctCol(ByVal dt As DataTable, ByVal distinctColumn() As String) As DataTable
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Return dt.DefaultView.ToTable(True, distinctColumn)
        Else
            Return dt
        End If
    End Function

    <Extension()> _
    Public Function SelectedRowsAsTable(ByVal dt As DataTable, ByVal columnName As String, ByVal oprator As String, ByVal value As String) As DataTable
        Try
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                Dim drTachPackCounter() As DataRow = dt.Select(columnName & oprator & "'" & value & "'")
                If (drTachPackCounter.Count > 0) Then
                    Return drTachPackCounter.CopyToDataTable()
                Else
                    Return New DataTable
                End If
            Else
                Return New DataTable
            End If
        Catch ex As Exception
            Return New DataTable
        End Try
    End Function

    <Extension()> _
    Public Function SelectedRowsAsTable(ByVal dt As DataTable, ByVal filterString As String) As DataTable
        Try
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                Dim drTachPackCounter() As DataRow = dt.Select(filterString)
                If (drTachPackCounter.Count > 0) Then
                    Return drTachPackCounter.CopyToDataTable()
                Else
                    Return New DataTable
                End If
            Else
                Return New DataTable
            End If
        Catch ex As Exception
            Return New DataTable
        End Try
    End Function


#End Region

#Region "Other"

    <Extension()> _
    Public Function GetCountItems(ByVal str As String, ByVal seperator As String) As Integer
        Dim strArray() As String
        strArray = Regex.Split(str, seperator)
        Return strArray.Length
    End Function

#End Region

#Region "Chart"

    <Extension()>
    Public Sub ClearAll(ByVal chartObject As dotnetCHARTING.WinForms.Chart)
        chartObject.SuspendLayout()
        chartObject.SeriesCollection.Clear()
        chartObject.ResetToParent()
        chartObject.Update()
        chartObject.ResumeLayout()
    End Sub

    <Extension()>
    Public Sub ResetToParent(ByVal dgv As dotnetCHARTING.WinForms.Chart)
        Dim parent As Control = dgv.Parent
        If (parent IsNot Nothing) Then
            dgv.Width = parent.Width - 10
        End If
    End Sub

#End Region

#Region "Extension String"

    <Extension()> _
    Public Function GetString(ByVal o As Object) As String
        If (o.Equals(DBNull.Value)) Then
            Return ""
        Else
            Return o.ToString()
        End If
    End Function

    <Extension()> _
    Public Function GetDecimalString(ByVal value As String) As String
        Dim IsAlphabet As Boolean = False
        Dim alp() As String = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"}
        'Dim alp() As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray()
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

#End Region

End Module
