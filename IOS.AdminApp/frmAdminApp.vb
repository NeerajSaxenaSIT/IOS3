Imports System.Collections.Specialized
Imports System.Data.OleDb
Imports System.Reflection
Imports System.Reflection.Assembly
Imports System.Configuration
Imports System.Text
Imports System.Data
Imports System.Collections
Imports System.Collections.Generic
Imports System.IO
Imports System.Windows.Forms.TrackBar
Imports IOS.Configuration.TreeControl
Imports IOS.Configuration.ExcelManager
Imports IOS.Library
Imports System.Data.SqlClient

Public Class frmAdminApp
    ''Public configManager As IOS.ConfigManager = IOS.ConfigManager.Instance
    Public rootPath As String = String.Empty
    Public usersData As DataTable = Nothing
    Public conStr As String = String.Empty
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frm_IOS_Map_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
    End Sub

    Private Sub frm_IOS_Map_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        rootPath = Application.StartupPath
        chkNew_CheckedChanged(chkNew, EventArgs.Empty)
        'rootPath = rootPath.Substring(0, rootPath.LastIndexOf(System.IO.Path.DirectorySeparatorChar))
        'rootPath = rootPath.Substring(0, rootPath.LastIndexOf(System.IO.Path.DirectorySeparatorChar))
        '' ddlUserName.SelectedIndex = 0
        '  rootPath = rootPath.Substring(0, rootPath.LastIndexOf(System.IO.Path.DirectorySeparatorChar))

        ''LoadTree("", "", DateTime.Now, rootPath + "//Template//IOSConfigurationTemplate.xls")
    End Sub
    Sub LoadTree(ByVal userName As String, ByVal company As String, ByVal ExpiryDate As DateTime, ByVal path As String)
        Dim data As New DataSet()
        data.ReadXml(path)
        LoadTreeHelper(userName, company, ExpiryDate, data.Tables(0))

    End Sub
    Sub LoadTree(ByVal userName As String, ByVal company As String, ByVal ExpiryDate As DateTime, ByVal data As DataTable)
        LoadTreeHelper(userName, company, ExpiryDate, data)
    End Sub
    Sub LoadTreeHelper(ByVal userName As String, ByVal company As String, ByVal ExpiryDate As DateTime, ByVal data As DataTable)
        'Dim forms As DataTable = data.DefaultView.ToTable(True, "form")
        'For Each Item As DataRow In forms.Rows
        '    Dim RootNode As New XMLNode()
        '    RootNode.NodeButton.CText = Item(0)
        '    RootNode.NodeButton.IsTextOnly = True
        '    Dim categories As DataTable = data.Select("form='" + RootNode.NodeButton.CText + "'").CopyToDataTable().DefaultView.ToTable(True, "Category")
        '    For Each category As DataRow In categories.Rows
        '        Dim categoryNode As New XMLNode()
        '        categoryNode.NodeButton.CText = category("Category")
        '        categoryNode.NodeButton.IsTextOnly = True
        '        Dim controls() As DataRow = data.Select("form='" + RootNode.NodeButton.CText + "' and Category='" + categoryNode.NodeButton.CText + "' and (ParentId='' or ParentId=' ' or ParentId IS NULL)")
        '        For Each control As DataRow In controls
        '            Dim perentNode As New XMLNode()
        '            perentNode.NodeButton.CText = control("Control_Name")
        '            perentNode.NodeButton.DefaultEnable = Convert.ToBoolean(control("Default_Enabled"))
        '            perentNode.NodeButton.DefaultVisible = Convert.ToBoolean(control("Default_Visible"))
        '            AddChildern(perentNode, data, control)
        '            categoryNode.AddChild(perentNode)
        '        Next
        '        RootNode.AddChild(categoryNode)
        '    Next
        '    XTreeMain.Add(RootNode)
        'Next
    End Sub

    Sub AddChildern(ByRef Node As XMLNode, ByRef data As DataTable, ByRef control As DataRow)
        Dim controls() As DataRow = data.Select("ParentId='" + Convert.ToString(control("id")) + "'")
        For Each child As DataRow In controls
            Dim ChildNode As New XMLNode()
            ChildNode.NodeButton.CText = child("Control_Name")
            ChildNode.NodeButton.DefaultEnable = Convert.ToBoolean(child("Default_Enabled"))
            ChildNode.NodeButton.DefaultVisible = Convert.ToBoolean(child("Default_Visible"))
            AddChildern(ChildNode, data, child)
            Node.AddChild(ChildNode)
        Next
    End Sub

    Private Sub chkNew_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNew.CheckedChanged
        Dim checkedItem = GetCheckedItems()
        txtUserName.Visible = chkNew.Checked
        btnNew.Enabled = chkNew.Checked
        vchkListBox.Enabled = Not chkNew.Checked
        btnExport.Enabled = (Not chkNew.Checked) AndAlso (checkedItem.Count() = 1)
        btnDelete.Enabled = Not chkNew.Checked
        btnUpdate.Enabled = Not chkNew.Checked
    End Sub

    Private Sub textBox_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtServerName.Leave, txtDatabase.Leave, txtDBUserName.Leave, txtDBPassword.Leave
        If (txtServerName.Text.Trim() = "" Or txtDatabase.Text.Trim() = "" Or txtDBUserName.Text.Trim() = "" Or txtDBPassword.Text.Trim() = "") Then
            conStr = ""
        Else
            conStr = IOS.Configuration.EntityModel.IOSAdminInput.getConnectionString(txtServerName.Text.Trim(), txtDatabase.Text.Trim(), txtDBUserName.Text.Trim(), txtDBPassword.Text.Trim())
            Dim con As New SqlConnection(conStr)
            Try
                con.Open()
                FillUserDropdown(con)
                '' ddlUserName.Focus()
            Catch ex As Exception
                conStr = ""
                MessageBox.Show("Error:" + ex.Message)
            Finally
                If (con.State = ConnectionState.Open) Then
                    con.Close()
                End If
            End Try
        End If
    End Sub

    Function GetCheckedItems() As IEnumerable(Of DevExpress.XtraEditors.Controls.CheckedListBoxItem)
        Dim checkedItem = From w In vchkListBox.Items.Cast(Of DevExpress.XtraEditors.Controls.CheckedListBoxItem)() _
                                  Where w.CheckState = CheckState.Checked Select w
        Return checkedItem
    End Function

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            Dim checkedItem = GetCheckedItems()
            Dim message As String = String.Empty
            If (rtxtEncryptedString.Text.Trim() = "") Then
                MessageBox.Show("Please provide encrypted string to exprot")
            ElseIf (checkedItem.Count() = 0) Then
                MessageBox.Show("Please select user name first")
            ElseIf (ValidateExcelPath(message)) Then
                MessageBox.Show(message)
            Else
                Application.UseWaitCursor = True
                btnExport.Text = "Exporting.."
                btnExport.Enabled = False
                Application.DoEvents()
                Dim sp As New IOS.Library.IOSCustomSecurityProvider()
                sp.EncryptionSalt = checkedItem.First().Value.ToString.Trim.ToLower
                Dim dc As New DataConverter()
                Dim ds As DataSet = dc.ConvertXmlStringToDataSet(sp.Decrypt(rtxtEncryptedString.Text))
                ValidatedataSet(ds)
                dc.DatasetToExcel(ds, txtExcelPath.Text.Trim(), True)
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " + ex.Message)
        Finally
            btnExport.Text = "Export to Edit"
            btnExport.Enabled = True
            Application.UseWaitCursor = False
        End Try

    End Sub

    Sub ValidatedataSet(ByRef ds As DataSet)
        If (ds.Tables.Count > 2) Then
            For index As Integer = 2 To ds.Tables.Count - 1
                ds.Tables.RemoveAt(index)
            Next
        End If
        If (ds.Tables.Count > 1) Then
            If (ds.Tables(1).Rows.Count = 0) Then
                ds.Tables.RemoveAt(1)
                AddUserInfo(ds)
            ElseIf (String.IsNullOrEmpty(Convert.ToString(ds.Tables(1).Rows(0)(0)))) Then
                ds.Tables.RemoveAt(1)
                AddUserInfo(ds)
            End If
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Dim message As String = String.Empty
            If (chkNew.Checked) Then
                If (txtUserName.Text.Trim() = "") Then
                    MessageBox.Show("Please insert username.")
                ElseIf (ValidateExcelPath(message)) Then
                    MessageBox.Show(message)
                ElseIf (txtCompanyName.Text.Trim() = "") Then
                    MessageBox.Show("Please provide company Name.")
                ElseIf (String.IsNullOrEmpty(conStr)) Then
                    MessageBox.Show("Please insert connection information")
                Else
                    If (usersData IsNot Nothing) Then
                        Dim user() As DataRow = usersData.Select("LicenseUser='" + txtUserName.Text.Trim + "'")
                        If (user.Length > 0) Then
                            MessageBox.Show("User " + txtUserName.Text.Trim + " already exists. Please try other name")
                            Exit Sub
                        End If
                    End If
                    Dim encryptString As String = ConvertExcelToEncryptedString(txtUserName.Text.Trim)
                    rtxtEncryptedString.Text = encryptString
                    Dim con As New SqlConnection(conStr)
                    Try
                        con.Open()
                        Dim cmd As New SqlCommand("INSERT INTO [dbo].[IOS_Licenses]([LicenseType] ,[LicenseCompany] ,[LicenseUser] ,[ExpirationDate],[Setting]) VALUES (@LicenseType,@LicenseCompany,@LicenseUser,@ExpirationDate,@Setting)", con)
                        cmd.Parameters.AddWithValue("@LicenseType", "Full")
                        cmd.Parameters.AddWithValue("@LicenseCompany", txtCompanyName.Text.Trim)
                        cmd.Parameters.AddWithValue("@LicenseUser", txtUserName.Text.Trim)
                        cmd.Parameters.AddWithValue("@ExpirationDate", dtExpiryDate.Value)
                        cmd.Parameters.AddWithValue("@Setting", encryptString)
                        cmd.ExecuteNonQuery()
                        MessageBox.Show("User has been inserted")
                        FillUserDropdown(con)
                    Catch ex As Exception
                        MessageBox.Show("Error:" + ex.Message)
                    Finally
                        If (con.State = ConnectionState.Open) Then
                            con.Close()
                        End If
                    End Try
                End If
            Else
                MessageBox.Show("Please check New user check box to add new user.")
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " + ex.Message)
        End Try
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            Dim checkedItem = GetCheckedItems()
            Dim message As String = String.Empty
            If (checkedItem.Count() = 0) Then
                MessageBox.Show("Please select username.")
            ElseIf (ValidateExcelPath(message)) Then
                MessageBox.Show(message)
            ElseIf (txtCompanyName.Text.Trim() = "") Then
                MessageBox.Show("Please provide company Name.")
            ElseIf (String.IsNullOrEmpty(conStr)) Then
                MessageBox.Show("Please insert connection information")
            Else
                Dim con As New SqlConnection(conStr)

                con.Open()
                Dim trans As SqlTransaction = con.BeginTransaction()
                Try
                    For Each Item As Object In checkedItem
                        Dim encryptString As String = ConvertExcelToEncryptedString(Item.ToString.Trim.ToLower)
                        rtxtEncryptedString.Text = encryptString
                        Dim cmtText As String = "UPDATE [dbo].[IOS_Licenses] SET [LicenseCompany] = @LicenseCompany,"
                        If (chkDateUpdate.Checked) Then
                            cmtText = cmtText & "[ExpirationDate] = @ExpirationDate,"
                        End If
                        cmtText = cmtText & "[Setting] = @Setting WHERE [LicenseUser] = @LicenseUser"
                        Dim cmd As New SqlCommand(cmtText, con, trans)
                        cmd.Parameters.AddWithValue("@LicenseCompany", txtCompanyName.Text.Trim)
                        If (chkDateUpdate.Checked) Then
                            cmd.Parameters.AddWithValue("@ExpirationDate", dtExpiryDate.Value)
                        End If

                        cmd.Parameters.AddWithValue("@Setting", encryptString)
                        cmd.Parameters.AddWithValue("@LicenseUser", Item.ToString.Trim)
                        Dim result As Integer = cmd.ExecuteNonQuery()
                    Next
                    MessageBox.Show("User has been updated")
                    Dim cmdNew As New SqlCommand("select * from dbo.IOS_Licenses order by LicenseUser", con, trans)
                    Dim adp As New SqlDataAdapter(cmdNew)
                    Dim dt As New DataTable()
                    adp.Fill(dt)
                    usersData = dt
                    trans.Commit()
                Catch ex As Exception
                    trans.Rollback()
                    MessageBox.Show("Error:" + ex.Message)
                Finally
                    If (con.State = ConnectionState.Open) Then
                        con.Close()
                    End If
                End Try
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " + ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim checkedItem = GetCheckedItems()
            If (checkedItem.Count() = 0) Then
                MessageBox.Show("Please select username.")
            ElseIf (String.IsNullOrEmpty(conStr)) Then
                MessageBox.Show("Please insert connection information")
            Else
                Dim r As DialogResult = MessageBox.Show("Do you want to delete?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                If (r = Windows.Forms.DialogResult.OK) Then
                    Dim con As New SqlConnection(conStr)
                    con.Open()
                    Try

                        For Each item As Object In checkedItem
                            Dim cmd As New SqlCommand("delete from [dbo].[IOS_Licenses]  WHERE [LicenseUser] = @LicenseUser", con)
                            cmd.Parameters.AddWithValue("@LicenseUser", item.Text.Trim)
                            cmd.ExecuteNonQuery()
                        Next

                        MessageBox.Show("User has been deleted")
                        FillUserDropdown(con)
                    Catch ex As Exception
                        MessageBox.Show("Error:" + ex.Message)
                    Finally
                        If (con.State = ConnectionState.Open) Then
                            con.Close()
                        End If
                    End Try
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " + ex.Message)
        End Try
    End Sub

    Sub AddUserInfo(ByRef ds As DataSet)
        Dim userInfo As DataTable = GetUserInformatationStructure()
        Dim row As DataRow = userInfo.NewRow()
        row("CompanyName") = txtCompanyName.Text.Trim()
        row("ExpirationDate") = dtExpiryDate.Value
        userInfo.Rows.Add(row)
        ds.Tables.Add(userInfo)
    End Sub

    Function ConvertExcelToEncryptedString(ByVal salt As String) As String
        Dim testXMLPath As String = rootPath + "\test.xml"
        Dim dc As New DataConverter()
        Dim ds As DataSet = dc.ConvertExcelToDataSet(txtExcelPath.Text.Trim())
        ValidatedataSet(ds)
        If (ds.Tables.Count > 1) Then
            ds.Tables(1).Rows(0)("CompanyName") = txtCompanyName.Text
            ds.Tables(1).Rows(0)("ExpirationDate") = dtExpiryDate.Value
        End If
        ds.WriteXml(testXMLPath, XmlWriteMode.WriteSchema)
        Dim xml As String = System.IO.File.ReadAllText(testXMLPath)
        Dim sp As New IOS.Library.IOSCustomSecurityProvider()
        sp.EncryptionSalt = salt.ToLower
        Dim encryptString As String = sp.Encrypt(xml)
        Return encryptString
    End Function

    Function GetUserInformatationStructure() As DataTable
        Dim userInfo As New DataTable("UserSetting")
        userInfo.Columns.Add("CompanyName", GetType(String))
        userInfo.Columns.Add("ExpirationDate", GetType(DateTime))
        Return userInfo
    End Function

    Sub FillUserDropdown(ByRef con As SqlConnection)
        Dim adp As New SqlDataAdapter("select * from dbo.IOS_Licenses order by LicenseUser", con)
        Dim dt As New DataTable()
        adp.Fill(dt)
        vchkListBox.SuspendLayout()
        vchkListBox.Items.Clear()
        For Each row As DataRow In dt.Rows
            Dim lItem As New DevExpress.XtraEditors.Controls.CheckedListBoxItem()
            lItem.Value = row("LicenseUser").ToString()
            lItem.Tag = row("LicenseID").ToString()
            vchkListBox.Items.Add(lItem)
        Next
        vchkListBox.ResumeLayout()
        usersData = dt
    End Sub

    Function ValidateExcelPath(ByRef message As String) As Boolean
        If (txtExcelPath.Text.Trim() = "") Then
            message = "Please insert excel file path"
            Return True
        Else
            Dim serverUri As New Uri(txtExcelPath.Text.Trim)
            If Not (serverUri.Scheme = Uri.UriSchemeFile) Then
                message = "Not a valid excel path"
                Return True
            ElseIf Not (serverUri.IsFile) Then
                message = "Path must contain a file"
                Return True
            ElseIf Not (Path.GetExtension(txtExcelPath.Text) = ".xls") Or Not (Path.GetExtension(txtExcelPath.Text) = ".xlsx") Then
                message = "File extension must be xls or xlsx"
                Return False
            End If
        End If
        Return False
    End Function

    Private Sub btnTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTemplate.Click
        txtExcelPath.Text = rootPath + "\Template\IOSConfigurationTemplate.xls"
    End Sub

    Private Sub vchkListBox_ItemCheck(sender As Object, e As DevExpress.XtraEditors.Controls.ItemCheckEventArgs) Handles vchkListBox.ItemCheck
        Dim checkedItem = GetCheckedItems()
        Dim count As Integer = checkedItem.Count()
        If (count = 0) Then
            txtCompanyName.Text = ""
            dtExpiryDate.Value = DateTime.Now
            rtxtEncryptedString.Text = ""
            Exit Sub
        End If
        If (count = 1) Then
            If (usersData IsNot Nothing) Then
                Dim user() As DataRow = usersData.Select("LicenseUser='" + checkedItem.First().Value.ToString() + "'")
                If (user.Length > 0) Then
                    If Not (String.IsNullOrEmpty(Convert.ToString(user(0)("Setting")))) Then
                        Dim sp As New IOS.Library.IOSCustomSecurityProvider()
                        sp.EncryptionSalt = checkedItem.First().Value.ToString.Trim.ToLower
                        Dim dc As New DataConverter()
                        Dim ds As DataSet = dc.ConvertXmlStringToDataSet(sp.Decrypt(Convert.ToString(user(0)("Setting"))))
                        If (ds.Tables.Count > 1) Then
                            If Not String.IsNullOrEmpty(ds.Tables(1).Rows(0)(0)) Then
                                txtCompanyName.Text = Convert.ToString(ds.Tables(1).Rows(0)("CompanyName"))
                                dtExpiryDate.Value = Convert.ToDateTime(ds.Tables(1).Rows(0)("ExpirationDate"))
                                rtxtEncryptedString.Text = Convert.ToString(user(0)("Setting"))
                                Exit Sub
                            End If
                        End If
                    End If
                    txtCompanyName.Text = Convert.ToString(user(0)("LicenseCompany"))
                    dtExpiryDate.Value = Convert.ToDateTime(user(0)("ExpirationDate"))
                    rtxtEncryptedString.Text = Convert.ToString(user(0)("Setting"))
                End If
            End If
        End If
        If (count > 1) Then
            btnExport.Enabled = False
            rtxtEncryptedString.Text = ""
        End If
    End Sub

    Private Sub vchkListBox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles vchkListBox.MouseDown, vchkListBox.MouseDoubleClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            vchkListBox.CheckOnClick = False
        ElseIf e.Button = Windows.Forms.MouseButtons.Left Then
            vchkListBox.CheckOnClick = True
        End If
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllToolStripMenuItem.Click
        ChangeStatusofCheckboxItemList(True)
    End Sub

    Private Sub UncheckAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UncheckAllToolStripMenuItem.Click
        ChangeStatusofCheckboxItemList(False)
    End Sub

    Sub ChangeStatusofCheckboxItemList(ByVal status As Boolean)
        For Each Item As DevExpress.XtraEditors.Controls.CheckedListBoxItem In vchkListBox.Items
            Item.CheckState = IIf(status = True, CheckState.Checked, CheckState.Unchecked)
        Next
    End Sub

End Class
