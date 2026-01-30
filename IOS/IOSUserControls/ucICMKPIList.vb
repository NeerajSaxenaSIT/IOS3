Public Class ucICMKPIList

    Public dtIOSKPI As DataTable
    Public Event ItemKeyDown As EventHandler

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        dtIOSKPI = Nothing
    End Sub

    Private Sub IOS_ICMKPIList_Load(sender As Object, e As EventArgs) Handles Me.Load
        If dtIOSKPI IsNot Nothing Then
            If dtIOSKPI.Rows.Count > 0 Then
                For Each dr As DataRow In dtIOSKPI.Rows
                    cmbKPI.Items.Add(dr("DBColumn").ToString)
                Next
            End If
        End If
    End Sub

    Public WriteOnly Property SetKPIComboData() As DataTable
        Set(ByVal value As DataTable)
            dtIOSKPI = value
        End Set
    End Property

    Public WriteOnly Property SetListKPI() As String
        Set(ByVal value As String)
            lstICMKPI.SuspendLayout()
            lstICMKPI.Refresh()
            lstICMKPI.Items.Add(value)
            lstICMKPI.Update()
            lstICMKPI.ResumeLayout()
        End Set
    End Property

    Public WriteOnly Property SetComboIndex() As Integer
        Set(ByVal value As Integer)
            cmbKPI.SelectedIndex = value
        End Set
    End Property

    Public ReadOnly Property GetKPI() As DevExpress.XtraEditors.Controls.ListBoxItemCollection
        Get
            Return lstICMKPI.Items
        End Get
    End Property

    Private Sub lstICMKPI_KeyDown(sender As Object, e As KeyEventArgs) Handles lstICMKPI.KeyDown
        RaiseEvent ItemKeyDown(sender, e)
    End Sub

    Private Sub cmbKPI_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbKPI.SelectedIndexChanged
        If cmbKPI.SelectedIndex > 0 Then
            If IsExistPKIItem(cmbKPI.SelectedItem.ToString) = False Then
                lstICMKPI.Items.Add(cmbKPI.SelectedItem.ToString)
            End If
        End If
    End Sub

    Public Function IsExistPKIItem(ByVal findItem As String) As Boolean
        Return lstICMKPI.Items.Contains(findItem)
    End Function

End Class
