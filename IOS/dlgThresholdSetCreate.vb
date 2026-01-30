Imports IOS.Library
Imports IOS.DataLibrary
Imports DevExpress.XtraEditors

Public Class dlgThresholdSetCreate

#Region "Variables"

    Public thresholdSetTech As String = Nothing
    Public dtTargetType As DataTable = Nothing
    Public defTargetType As String = Nothing

#End Region

#Region "Form Events"

    Private Sub dlgThresholdSetCreate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            lblIOSTech.Text = Me.thresholdSetTech
            txtThresholdSetName.Text = String.Empty
            LoadMthods()
            BindComboWithTargetType(dtTargetType, cmbTargetType)
            'tlpMain.RowStyles(3).SizeType = SizeType.Absolute
            tlpMain.RowStyles(3).Height = 0
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
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
            If txtThresholdSetName.Text = String.Empty Then
                SetMessage("Enter Threshold Set Name")
                Exit Sub
            ElseIf cmbMethod.SelectedIndex = 0 Then
                SetMessage("Select Method")
                Exit Sub
            ElseIf cmbMethod.SelectedItem.ToString.ToUpper = "DATELISTBASED" Then
                If cmbTargetType.Text = String.Empty Then
                    SetMessage("Select Target Type")
                    Exit Sub
                End If
            End If
                AddThresholdSetName()
            objThresholdSetCreate.newThresholdSetName = txtThresholdSetName.Text.Trim
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            SetMessage(ex.Message.ToString)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cmbMethod_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            'tlpMain.RowStyles(3).SizeType = SizeType.Absolute
            If cmbMethod.SelectedItem.ToString.ToUpper = "DATELISTBASED" Then
                lblTargetType.Visible = True
                cmbTargetType.Visible = True
                SetComboBox(cmbTargetType, ComboSelectBased.TextBased, defTargetType)
                tlpMain.RowStyles(3).Height = 25
            Else
                lblTargetType.Visible = False
                cmbTargetType.Visible = False
                tlpMain.RowStyles(3).Height = 0
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

#End Region

#Region "Private Methods"

    Private Sub LoadMthods()
        RemoveHandler cmbMethod.SelectedIndexChanged, AddressOf cmbMethod_SelectedIndexChanged
        Dim parray()() As String = Nothing
        Dim strConnection As String = GetSQL(7011, parray)(0)
        Dim sqlParam As String = GetSQL(7011, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        BindDevExComboBoxWithValueMember(cmbMethod, dt, "ThresholdMethodID", "ThresholdMethodName", "Select Method")
        AddHandler cmbMethod.SelectedIndexChanged, AddressOf cmbMethod_SelectedIndexChanged
    End Sub

    Private Sub AddThresholdSetName()
        Try
            Dim targetType As String = "NULL"
            If cmbMethod.SelectedItem.ToString.ToUpper = "DATELISTBASED" Then
                targetType = Chr(39) & cmbTargetType.SelectedItem.ToString & Chr(39)
            End If

            Dim parray()() As String = {
                New String() {"@ThresholdSetName", Chr(39) & txtThresholdSetName.Text.Trim & Chr(39)},
                New String() {"@Owner", Chr(39) & Environment.UserName.ToString & Chr(39)},
                New String() {"@ThresholdMethodID", CInt(TryCast(cmbMethod.SelectedItem, clsComboBoxItem).Value)},
                New String() {"@IOSTech", Chr(39) & Me.thresholdSetTech & Chr(39)},
                New String() {"@ThresholdTargetType", targetType}
            }
            DataAccessorODBC.ExecuteNonQuery(GetSQL(7017, parray)(0), GetSQL(7017, parray)(1))
        Catch ex As Exception
            SetMessage(ex.Message.ToString)
        End Try
    End Sub

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub BindComboWithTargetType(ByRef dt As DataTable, ByRef cmb As ComboBoxEdit)
        Try
            If Not dt Is Nothing Then
                cmb.Properties.Items.Clear()

                Dim cmbitem As New clsComboBoxItem()
                cmbitem.Text = "PLMN"
                cmbitem.Value = "PLMN"
                cmbitem.Enabled = True
                cmbitem.Tag = "PLMN"
                cmb.Properties.Items.Add(cmbitem)

                For Each drow As DataRow In dt.AsEnumerable().Where(Function(x) x.Field(Of String)("tech").ToUpper = thresholdSetTech.ToUpper And x.Field(Of Integer)("ObjectTreeEnabled") = 1).OrderBy(Function(x) x.Field(Of Integer)("loadorder"))
                    cmbitem = New clsComboBoxItem()
                    cmbitem.Text = drow("Object").ToString.ToUpper
                    cmbitem.Value = drow("Object").ToString.ToUpper
                    cmbitem.Enabled = drow("ObjectTreeEnabled")
                    cmbitem.Tag = drow("InternalObjectName").ToString.ToUpper
                    cmb.Properties.Items.Add(cmbitem)
                Next
                cmb.SelectedItem = cmb.Properties.Items(0)

                Select Case thresholdSetTech
                    Case "SGSN", "GGSN", "MGW", "MME", "MSS", "PGW", "SGW", "IMS", "TX", "TRANSPORT"
                    Case Else
                        cmbitem = New clsComboBoxItem()
                        cmbitem.Text = "TAGS"
                        cmbitem.Value = "TAGS"
                        cmbitem.Enabled = True
                        cmbitem.Tag = "TAGS"
                        cmb.Properties.Items.Add(cmbitem)
                End Select
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

End Class