Imports IOS.Library
Imports IOS.DataLibrary

Public Class frmNotes

#Region "Variables"

    Dim tech As String
    Dim username As String
    Dim targettype As String
    Dim longdescription As String
    Dim shortdescription As String
    Dim datechange As Date
    Dim targetcount As Integer
    Dim childrencount As Integer
    Dim objs As String
    Dim noteid As Integer
    Private objfrmTechnology As frmTechnology = Nothing

#End Region

#Region "Notes Form Events"

    Private Sub frmNotes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            cmbChangeType.Text = ""
            cmbNoteDepartment.Text = ""
        Catch
        End Try
    End Sub

    Private Sub frmNotes_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Application.DoEvents()
        Try
            'Dim sql As String = "SELECT * FROM IOS_Note_Departments"
            Dim dt As DataTable = IOS.DataLibrary.clsSQLCommands.GetNoteDepartments(connStrIOSServer) 'IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, sql)
            BindDevExComboBoxWithValueMember(cmbNoteDepartment, dt, "DepartmentId", "Department", , True)

            'Dim sql1 As String = "SELECT * FROM IOS_Note_Types"
            Dim dt1 As DataTable = IOS.DataLibrary.clsSQLCommands.GetNoteTypes(connStrIOSServer) 'IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, sql1)
            BindDevExComboBoxWithValueMember(cmbChangeType, dt1, "NoteTypeId", "NoteType", , True)
        Catch
        End Try
    End Sub

#End Region

#Region "Submit/Update Event"

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Dim inputgood As Boolean = True
        'Validation
        If deChangeDate.EditValue Is Nothing Then
            inputgood = False
        ElseIf txtShortDescription.Text = "" Then
            inputgood = False
        ElseIf txtLongDescription.Text = "" Then
            inputgood = False
        ElseIf cmbChangeType.Text = "" Then
            inputgood = False
        ElseIf cmbNoteDepartment.Text = "" Then
            inputgood = False
        End If

        If inputgood = False Then
            MsgBox("Input is not complete")
            Exit Sub
        End If

        'Dim sql As String = "EXECUTE IOS_Notes_Add @timestamp, @username, @tech, @NoteType, @ShortDesc, @LongDesc"
        Dim cnQODBC As System.Data.Odbc.OdbcConnection = New System.Data.Odbc.OdbcConnection(connStrIOSServer)
        Dim objCmd As Odbc.OdbcCommand = Nothing
        'Dim objCmd2 As Odbc.OdbcCommand = Nothing

        Dim sql_insert1 As String = "INSERT INTO dbo.IOS_Note ([Timestamp], [Username], [Tech], [NoteTypeId], [DepartmentId], [ShortDescription], [LongDescription]) VALUES (?, ?, ?, ?, ?, ?, ?)"
        Dim sql_update As String = "UPDATE dbo.IOS_Note SET Timestamp = ?, Username = ?, Tech = ?, NoteTypeId = ?, DepartmentId = ?, ShortDescription = ?, LongDescription = ? WHERE NotesID = " & noteid

        Try
            'Dim objCmd As Odbc.OdbcCommand = New System.Data.Odbc.OdbcCommand("{? = call IOS_Notes_Add (?, ?, ?, ?, ?, ?)}", cnQODBC)
            If btnSubmit.Text = "Submit" Then
                objCmd = New System.Data.Odbc.OdbcCommand(sql_insert1, cnQODBC)
            Else
                objCmd = New System.Data.Odbc.OdbcCommand(sql_update, cnQODBC)
            End If

            Dim fulldatetime As DateTime = CType(deChangeDate.EditValue, DateTime)
            Dim truncatedDateTime As DateTime = New DateTime(fulldatetime.Ticks - (fulldatetime.Ticks Mod TimeSpan.TicksPerSecond), fulldatetime.Kind)

            Dim param1 As Odbc.OdbcParameter = New Odbc.OdbcParameter("Timestamp", Odbc.OdbcType.DateTime)
            param1.Value = truncatedDateTime
            Dim param2 As Odbc.OdbcParameter = New Odbc.OdbcParameter("Username", Odbc.OdbcType.NVarChar)
            param2.Value = username
            Dim param3 As Odbc.OdbcParameter = New Odbc.OdbcParameter("Tech", Odbc.OdbcType.NVarChar)
            param3.Value = tech
            Dim param4 As Odbc.OdbcParameter = New Odbc.OdbcParameter("NoteType", Odbc.OdbcType.Int)
            param4.Value = TryCast(cmbChangeType.SelectedItem, IOS.Library.clsComboBoxItem).Value
            Dim param7 As Odbc.OdbcParameter = New Odbc.OdbcParameter("Department", Odbc.OdbcType.Int)
            param7.Value = TryCast(cmbNoteDepartment.SelectedItem, IOS.Library.clsComboBoxItem).Value
            Dim param5 As Odbc.OdbcParameter = New Odbc.OdbcParameter("ShortDescription", Odbc.OdbcType.NVarChar)
            param5.Value = txtShortDescription.Text.Trim
            Dim param6 As Odbc.OdbcParameter = New Odbc.OdbcParameter("LongDescription", Odbc.OdbcType.NVarChar)
            param6.Value = txtLongDescription.Text.Trim

            With objCmd
                ' .Parameters.Add(paramReturn)
                .Parameters.Add(param1)
                .Parameters.Add(param2)
                .Parameters.Add(param3)
                .Parameters.Add(param4)
                .Parameters.Add(param7)
                .Parameters.Add(param5)
                .Parameters.Add(param6)
            End With
            cnQODBC.ConnectionTimeout = 5
            cnQODBC.Open()
            objCmd.ExecuteNonQuery()

            'Dim sql2 As String = "SELECT TOP 1 NotesID FROM dbo.IOS_Note WHERE Username = " & Chr(39) & username & Chr(39) & " ORDER BY NotesID DESC"
            'objCmd2 = New System.Data.Odbc.OdbcCommand(sql2, cnQODBC)

            noteid = IOS.DataLibrary.clsSQLCommands.GetNoteIdByUsername(connStrIOSServer, username) 'objCmd2.ExecuteScalar
            objCmd.Dispose()
            cnQODBC.Close()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            If Not objCmd Is Nothing Then
                objCmd.Dispose()
                objCmd = Nothing
            End If
            'If Not objCmd2 Is Nothing Then
            '    objCmd2.Dispose()
            '    objCmd = Nothing
            'End If
            If Not cnQODBC Is Nothing Then
                cnQODBC.Close()
            End If
        End Try
        'Inserting records into IOS_Notes_Objects

        If btnSubmit.Text = "Update" Then
            Me.Close()
            objfrmTechnology = Nothing
            If Not objFrmTechList.Exists(Function(x) x.Network.ToUpper.Equals(tech)) Then
                frmMDI.OpenTechFormDynamically(tech, objfrmTechnology, False)
            Else
                objfrmTechnology = objFrmTechList.Where(Function(x) x.Network.Equals(tech)).LastOrDefault()
            End If
            objfrmTechnology.Notes_Get(tech)
            Exit Sub
        End If

        Try
            For Each str_obj As String In Split(objs, ",")
                Dim sql_insert As String = "INSERT INTO dbo.IOS_Note_Objects ([NoteID], [NoteObject], [NoteObjectName]) VALUES (?, ?, ?)"
                Dim objCmd1 As Odbc.OdbcCommand = New System.Data.Odbc.OdbcCommand(sql_insert, cnQODBC)
                Dim prm1 As Odbc.OdbcParameter = New Odbc.OdbcParameter("NoteID", Odbc.OdbcType.Int)
                prm1.Value = noteid
                Dim prm2 As Odbc.OdbcParameter = New Odbc.OdbcParameter("NoteObject", Odbc.OdbcType.NVarChar)
                prm2.Value = targettype
                Dim prm3 As Odbc.OdbcParameter = New Odbc.OdbcParameter("NoteObjectName", Odbc.OdbcType.NVarChar)
                prm3.Value = str_obj

                With objCmd1
                    .Parameters.Add(prm1)
                    .Parameters.Add(prm2)
                    .Parameters.Add(prm3)
                End With
                cnQODBC.Open()
                objCmd1.ExecuteNonQuery()
                cnQODBC.Close()

                objCmd1.Dispose()
                objCmd1 = Nothing

            Next
            Me.Close()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            If Not cnQODBC Is Nothing Then
                cnQODBC.Close()
                cnQODBC.Dispose()
            End If
            objfrmTechnology = Nothing
            If Not objFrmTechList.Exists(Function(x) x.Network.ToUpper.Equals(tech)) Then
                frmMDI.OpenTechFormDynamically(tech, objfrmTechnology, False)
            Else
                objfrmTechnology = objFrmTechList.Where(Function(x) x.Network.Equals(tech)).LastOrDefault()
            End If
            objfrmTechnology.Notes_Get(tech)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "Public Methods"

    Public Sub SetForm_New(ByVal str_technology As String, ByVal str_username As String, ByVal str_targettype As String, ByVal int_targetcount As Integer, ByVal int_childcount As String, ByVal str_obj As String)
        Application.DoEvents()
        Try
            lvNoteObjects.Clear()
            btnSubmit.Text = "Submit"
            tech = str_technology
            username = str_username
            targettype = str_targettype
            targetcount = int_targetcount
            childrencount = int_childcount
            objs = str_obj
            deChangeDate.EditValue = Now()

            For Each dr As String In Split(objs, ",")
                AddObjectToListview(dr, targettype)
            Next

            UpdateForm()
        Catch ex As Exception
        End Try
    End Sub

    Public Sub SetForm_Edit(ByVal int_NoteID As Integer)
        Application.DoEvents()
        Try
            lvNoteObjects.Clear()
            btnSubmit.Text = "Update"
            'Dim sql As String = "SELECT * from dbo.qry_IOS_Notes WHERE NotesID = " & int_NoteID
            Dim dt As DataTable = clsSQLCommands.GetIOSNote(connStrIOSServer, int_NoteID) ' DataAccessorODBC.GetDataTable(connStrIOSServer, sql)

            If dt.Rows.Count > 0 Then
                deChangeDate.Text = dt(0)("Timestamp").ToString
                deChangeDate.EditValue = dt(0)("Timestamp")
                username = dt(0)("Username").ToString
                tech = dt(0)("Tech").ToString
                cmbChangeType.SelectedItem = GetComboItemFromValue(dt(0)("NoteTypeID"), cmbChangeType)
                cmbNoteDepartment.SelectedItem = GetComboItemFromValue(dt(0)("DepartmentID"), cmbNoteDepartment)
                txtShortDescription.Text = dt(0)("ShortDescription").ToString
                txtLongDescription.Text = dt(0)("LongDescription").ToString

                targettype = dt(0)("NoteObject").ToString

                targetcount = dt.Rows.Count
                For Each dr As DataRow In dt.Rows
                    AddObjectToListview(dr("NoteObjectName").ToString, dr("NoteObject").ToString)
                Next

                noteid = int_NoteID
                UpdateForm()
            End If

        Catch ex As Exception
        End Try
    End Sub

    Public Sub AddObjectToListview(ByVal obj As String, ByVal objtype As String)
        lvNoteObjects.BorderStyle = BorderStyle.FixedSingle ' Set BorderStyle property.
        lvNoteObjects.View = View.Details ' Set View property to Report.
        lvNoteObjects.GridLines = True
        lvNoteObjects.FullRowSelect = True
        Dim col1 As ColumnHeader
        Dim col2 As ColumnHeader
        If lvNoteObjects.Columns.Count < 2 Then
            col1 = lvNoteObjects.Columns.Add("Object")
            col2 = lvNoteObjects.Columns.Add("Type")
        Else
            col1 = lvNoteObjects.Columns(0)
            col2 = lvNoteObjects.Columns(1)
        End If

        Dim str(1) As String
        Dim itm As New ListViewItem
        itm.Name = obj
        itm.Text = obj
        itm.SubItems.Add(objtype)

        lvNoteObjects.BeginUpdate()
        lvNoteObjects.Items.Add(itm)
        lvNoteObjects.EndUpdate()
        col1.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
        col2.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
        lvNoteObjects.Refresh()
    End Sub

    Public Sub UpdateForm()
        lblTechnology.Text = tech
        lblObjectType.Text = targettype
        lblObjectsSelected.Text = targetcount
        lblUserName.Text = username
    End Sub

#End Region

End Class