Public Class dlgListToAdd

#Region "Variables/Properties"

    Private _templateMOConfigID As Integer
    Public Property TemplateMOConfigID() As Integer
        Get
            Return _templateMOConfigID
        End Get
        Set(ByVal value As Integer)
            _templateMOConfigID = value
        End Set
    End Property

    Private _templateID As Integer
    Public Property TemplateID() As Integer
        Get
            Return _templateID
        End Get
        Set(ByVal value As Integer)
            _templateID = value
        End Set
    End Property

    Private _moType As String
    Public Property MOType() As String
        Get
            Return _moType
        End Get
        Set(ByVal value As String)
            _moType = value
        End Set
    End Property

    Private _filterType As String
    Public Property FilterType() As String
        Get
            Return _filterType
        End Get
        Set(ByVal value As String)
            _filterType = value
        End Set
    End Property

    Private _moName As String
    Public Property MOName() As String
        Get
            Return _moName
        End Get
        Set(ByVal value As String)
            _moName = value
        End Set
    End Property

#End Region

#Region "Private Methods"

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub InsertList2CurrentMO()
        Try
            Dim dtRow() As DataRow = Nothing
            If dtRefChkList IsNot Nothing Then
                dtRow = dtRefChkList.Select("ListName = '" & cmbListName.SelectedItem.ToString & "'")
            End If
            If (dtRow.Length > 0) Then
                SetMessage("Fail : List Name already exists.")
                cmbListName.Focus()
            Else
                Dim parray()() As String = {
                    New String() {"@templateMOConfigID", Me.TemplateMOConfigID},
                    New String() {"@listID", TryCast(cmbListName.SelectedItem, IOS.Library.clsComboBoxItem).Value},
                    New String() {"@InclusionOrExclusion", Chr(39) & Me.FilterType & Chr(39)}
                }
                dtRefChkList = IOS.DataLibrary.DataAccessorODBC.GetDataTable(GetSQL(4155, parray)(0), GetSQL(4155, parray)(1))
                SetMessage("List Name Saved Successfully")
                'save change log
                frmRefCheck.SaveChangeLog(Me.TemplateID, Me.MOName, Me.TemplateMOConfigID, Me.FilterType & " list: " & cmbListName.SelectedItem.ToString & " added to mo: " & Me.MOName)
            End If
        Catch ex As Exception
            SetMessage("Error : List Name Insertion Fail")
        End Try
    End Sub

    Private Sub InsertList2AllMO()
        Try
            Dim parray()() As String = {
                New String() {"@templateID", Me.TemplateID},
                New String() {"@listID", TryCast(cmbListName.SelectedItem, IOS.Library.clsComboBoxItem).Value},
                New String() {"@InclusionOrExclusion", Chr(39) & Me.FilterType & Chr(39)}
            }
            dtRefChkList = IOS.DataLibrary.DataAccessorODBC.GetDataTable(GetSQL(4165, parray)(0), GetSQL(4165, parray)(1))
            SetMessage("List Name Saved Successfully")
            'save change log
            frmRefCheck.SaveChangeLog(Me.TemplateID, Me.MOName, Me.TemplateMOConfigID, Me.FilterType & " list: " & cmbListName.SelectedItem.ToString & " added to mo: " & Me.MOName)
        Catch ex As Exception
            SetMessage("Error : List Name Insertion Fail")
        End Try
    End Sub

    Private Sub FillList2CurrentMO()
        If (Me.FilterType.ToUpper = "INCLUSION") Then

            Dim dr As DataRow = dtIncList.NewRow()
            dr("ListID") = TryCast(cmbListName.SelectedItem, IOS.Library.clsComboBoxItem).Value
            dr("ListName") = cmbListName.SelectedItem.ToString
            dr("ListType") = Me.FilterType

            dtIncList.Rows.Add(dr)
            dtIncList.AcceptChanges()

        ElseIf (Me.FilterType.ToUpper = "EXCLUSION") Then

            Dim dr As DataRow = dtExcList.NewRow()
            dr("ListID") = TryCast(cmbListName.SelectedItem, IOS.Library.clsComboBoxItem).Value
            dr("ListName") = cmbListName.SelectedItem.ToString
            dr("ListType") = Me.FilterType

            dtExcList.Rows.Add(dr)
            dtExcList.AcceptChanges()

        End If
    End Sub

    Private Sub FillList2allMO()
        If (Me.FilterType.ToUpper = "INCLUSION") Then

            Dim dr As DataRow = dtIncList.NewRow()
            dr("ListID") = TryCast(cmbListName.SelectedItem, IOS.Library.clsComboBoxItem).Value
            dr("ListName") = cmbListName.SelectedItem.ToString
            dr("ListType") = Me.FilterType

            dtIncList.Rows.Add(dr)
            dtIncList.AcceptChanges()

        ElseIf (Me.FilterType.ToUpper = "EXCLUSION") Then

            Dim dr As DataRow = dtExcList.NewRow()
            dr("ListID") = TryCast(cmbListName.SelectedItem, IOS.Library.clsComboBoxItem).Value
            dr("ListName") = cmbListName.SelectedItem.ToString
            dr("ListType") = Me.FilterType

            dtExcList.Rows.Add(dr)
            dtExcList.AcceptChanges()

        End If
    End Sub

#End Region

#Region "Form Events"

    Private Sub dlgListToAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        strConnection = GetSQL(4158, parray)(0)
        sqlParam = GetSQL(4158, parray)(1)
        Dim dt As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        BindDevExComboBoxWithValueMember(cmbListName, dt, "ListID", "ListName", "Select List", False)
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

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If cmbListName.SelectedIndex = 0 Then
                SetMessage("Please Select List Name")
            Else
                If (Me.MOType.ToLower = "current") Then
                    InsertList2CurrentMO()
                ElseIf (Me.MOType.ToLower = "all") Then
                    InsertList2AllMO()
                ElseIf (Me.MOType.ToLower = "currentgentemp") Then
                    FillList2CurrentMO()
                ElseIf (Me.MOType.ToLower = "allgentemp") Then
                    FillList2AllMO()
                End If
                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#End Region

End Class
