Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.ComponentModel.Design.Serialization
Imports System.Windows.Forms.Design
Imports System.Reflection

Public Class CustomClass
    Inherits CollectionBase
    Implements ICustomTypeDescriptor
    Dim _CampaignID As Integer
    Dim _ConfigID As Integer

    Public Property CampaignID As Integer
        Get
            Return _CampaignID
        End Get
        Set(value As Integer)
            _CampaignID = value
        End Set
    End Property
    Public Property ConfigID As String
        Get
            Return _ConfigID
        End Get
        Set(value As String)
            _ConfigID = value
        End Set
    End Property

    Public Sub Add(ByVal Value As CustomProperty)
        MyBase.List.Add(Value)
    End Sub

    Public Sub Remove(ByVal Name As String)
        For Each prop As CustomProperty In MyBase.List
            If prop.Name = Name Then
                MyBase.List.Remove(prop)
                Return
            End If
        Next
    End Sub

    Default Public Property Item(ByVal index As Integer) As CustomProperty
        Get
            Return CType(MyBase.List(index), CustomProperty)
        End Get

        Set(ByVal value As CustomProperty)
            MyBase.List(index) = CType(value, CustomProperty)
        End Set
    End Property

    Public Function GetClassName() As String Implements ICustomTypeDescriptor.GetClassName
        Return TypeDescriptor.GetClassName(Me, True)
    End Function

    Public Function GetAttributes() As AttributeCollection Implements ICustomTypeDescriptor.GetAttributes
        Return TypeDescriptor.GetAttributes(Me, True)
    End Function

    Public Function GetComponentName() As String Implements ICustomTypeDescriptor.GetComponentName
        Return TypeDescriptor.GetComponentName(Me, True)
    End Function

    Public Function GetConverter() As TypeConverter Implements ICustomTypeDescriptor.GetConverter
        Return TypeDescriptor.GetConverter(Me, True)
    End Function

    Public Function GetDefaultEvent() As EventDescriptor Implements ICustomTypeDescriptor.GetDefaultEvent
        Return TypeDescriptor.GetDefaultEvent(Me, True)
    End Function

    Public Function GetDefaultProperty() As PropertyDescriptor Implements ICustomTypeDescriptor.GetDefaultProperty
        Return TypeDescriptor.GetDefaultProperty(Me, True)
    End Function

    Public Function GetEditor(ByVal editorBaseType As Type) As Object Implements ICustomTypeDescriptor.GetEditor
        Return TypeDescriptor.GetEditor(Me, editorBaseType, True)
    End Function

    Public Function GetEvents(ByVal attributes As Attribute()) As EventDescriptorCollection Implements ICustomTypeDescriptor.GetEvents
        Return TypeDescriptor.GetEvents(Me, attributes, True)
    End Function

    Public Function GetEvents() As EventDescriptorCollection Implements ICustomTypeDescriptor.GetEvents
        Return TypeDescriptor.GetEvents(Me, True)
    End Function

    Public Function GetProperties(ByVal attributes As Attribute()) As PropertyDescriptorCollection Implements ICustomTypeDescriptor.GetProperties
        Dim newProps As PropertyDescriptor() = New PropertyDescriptor(Me.Count - 1) {}
        For i As Integer = 0 To Me.Count - 1
            Dim prop As CustomProperty = CType(Me(i), CustomProperty)
            If prop.PropertyType.ToLower = "ComboBoxLayer".ToLower Then
                Dim attrs As ArrayList = New ArrayList()
                Dim EditAtt As New EditorAttribute(GetType(UIComboBoxEditor), GetType(UITypeEditor))
                attrs.Add(EditAtt)
                Dim attrArray As Attribute() = attrs.ToArray(GetType(Attribute))
                newProps(i) = New CustomPropertyDescriptor(prop, attrArray)
            Else
                newProps(i) = New CustomPropertyDescriptor(prop, attributes)
            End If
        Next
        Return New PropertyDescriptorCollection(newProps)
    End Function

    Public Function GetProperties() As PropertyDescriptorCollection Implements ICustomTypeDescriptor.GetProperties
        Return TypeDescriptor.GetProperties(Me, True)
    End Function

    Public Function GetPropertyOwner(ByVal pd As PropertyDescriptor) As Object Implements ICustomTypeDescriptor.GetPropertyOwner
        Return Me
    End Function

End Class

Public Class CustomProperty

    Private _Category As String = String.Empty
    Private _Name As String = String.Empty
    Private _Type As String = String.Empty
    Private _Description As String = String.Empty
    Private _ReadOnly As Boolean = False
    Private _DefaultValue As Object = Nothing

    Public Sub New(ByVal propertyCategory As String, ByVal propertyName As String, propertyType As String, propertyDescription As String, ByVal bReadOnly As Boolean, ByVal propertyValue As Object)
        Me._Category = propertyCategory
        Me._Name = propertyName
        Me._Type = propertyType
        Me._Description = propertyDescription
        Me._ReadOnly = bReadOnly
        Me._DefaultValue = propertyValue
    End Sub

    Public Property Category As String
        Get
            Return _Category
        End Get
        Set(value As String)
            _Category = value
        End Set
    End Property

    Public Property Name As String
        Get
            Return _Name
        End Get
        Set(value As String)
            _Name = value
        End Set
    End Property

    Public Property PropertyType As String
        Get
            Return _Type
        End Get
        Set(value As String)
            _Type = value
        End Set
    End Property

    Public Property PropertyDescription As String
        Get
            Return _Description
        End Get
        Set(value As String)
            _Description = value
        End Set
    End Property

    Public Property [ReadOnly] As Boolean
        Get
            Return _ReadOnly
        End Get
        Set(value As Boolean)
            _ReadOnly = value
        End Set
    End Property

    Public Property Value As Object
        Get
            Return _DefaultValue
        End Get

        Set(ByVal value As Object)
            _DefaultValue = value
        End Set
    End Property

End Class

Public Class CustomPropertyDescriptor
    Inherits PropertyDescriptor

    Private m_Property As CustomProperty

    Public Sub New(ByRef myProperty As CustomProperty, ByVal attrs As Attribute())
        MyBase.New(myProperty.Name, attrs)
        m_Property = myProperty
    End Sub

    Public Overrides Function CanResetValue(ByVal component As Object) As Boolean
        Return False
    End Function

    Public Overrides ReadOnly Property ComponentType As Type
        Get
            Select Case m_Property.PropertyType.ToLower
                Case "text"
                    Return GetType(System.String)
                Case "ComboBoxLayer"
                    Return GetType(System.Array)
                Case "ComboBoxBoolean"
                    Return GetType(System.Boolean)
                Case "Integer"
                    Return GetType(System.Int32)
            End Select
            Return Nothing
        End Get
    End Property

    Public Overrides Function GetValue(ByVal component As Object) As Object
        Return m_Property.Value
    End Function

    Public Overrides ReadOnly Property Description As String
        Get
            Return m_Property.Name
        End Get
    End Property

    Public Overrides ReadOnly Property Category As String
        Get
            Return m_Property.Category
        End Get
    End Property

    Public Overrides ReadOnly Property DisplayName As String
        Get
            Return m_Property.Name
        End Get
    End Property

    Public Overrides ReadOnly Property IsReadOnly As Boolean
        Get
            Return m_Property.[ReadOnly]
        End Get
    End Property

    Public Overrides Sub ResetValue(ByVal component As Object)
    End Sub

    Public Overrides Function ShouldSerializeValue(ByVal component As Object) As Boolean
        Return False
    End Function

    Public Overrides Sub SetValue(ByVal component As Object, ByVal value As Object)
        m_Property.Value = value
    End Sub

    Public Overrides ReadOnly Property PropertyType As Type
        Get
            Return m_Property.Value.[GetType]()
        End Get
    End Property

End Class

Public Class UIComboBoxEditor
    Inherits UITypeEditor

    Private WithEvents oList As New ListBox
    Private oSelectedValue As Object = Nothing
    Private oEditorService As IWindowsFormsEditorService

    Public Overloads Overrides Function GetEditStyle(ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle
        If Not context Is Nothing AndAlso Not context.Instance Is Nothing Then
            Return UITypeEditorEditStyle.DropDown
        End If
        Return UITypeEditorEditStyle.None
    End Function

    <RefreshProperties(RefreshProperties.All)> _
    Public Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
        If context Is Nothing OrElse provider Is Nothing OrElse context.Instance Is Nothing Then
            Return MyBase.EditValue(provider, value)
        End If

        oEditorService = provider.GetService(GetType(IWindowsFormsEditorService))
        If oEditorService IsNot Nothing Then

            oList.BorderStyle = BorderStyle.None
            oList.IntegralHeight = True
            oList.SelectionMode = SelectionMode.One
            oList.Items.Clear()

            If context.PropertyDescriptor.Category.Contains("NB") Then

                If context.PropertyDescriptor.Name = "ExclusionList" Or context.PropertyDescriptor.Name = "InclusionList" Then
                    For Each dr As DataRow In dtCellList.Rows
                        oList.Items.Add(dr.Item("ListName"))
                    Next
                Else
                    For Each dr As DataRow In dtLayer.Rows
                        oList.Items.Add(dr.Item(0))
                    Next
                End If

            ElseIf context.PropertyDescriptor.Category.Contains("BulkImport") Or context.PropertyDescriptor.Category.Contains("Audit") Then

                If context.PropertyDescriptor.Name = "Exclusion List" Or context.PropertyDescriptor.Name = "Inclusion List" Then
                    For Each dr As DataRow In dtCellList.Rows
                        oList.Items.Add(dr.Item("ListName"))
                    Next
                ElseIf context.PropertyDescriptor.Name = "Reference Band" Or context.PropertyDescriptor.Name = "Target Band" Then
                    For Each dr As DataRow In dtBandListTiltMngr.Rows
                        oList.Items.Add(dr.Item("BAND"))
                    Next
                ElseIf context.PropertyDescriptor.Name = "Master Layer" Or context.PropertyDescriptor.Name = "Target Layer" Then
                    For Each dr As DataRow In dtLayer.Rows
                        oList.Items.Add(dr.Item(0))
                    Next
                ElseIf context.PropertyDescriptor.Name = "Tilt Rule" Then
                    For Each dr As DataRow In dtTiltRule.Rows
                        oList.Items.Add(dr.Item("TiltRule"))
                    Next
                End If

            End If

            If IsDBNull(value) = False Then
                If value IsNot Nothing AndAlso value <> "" Then
                    Dim index As Integer = -1
                    index = oList.Items.IndexOf(value)
                    If index > -1 Then
                        oList.SetSelected(index, True)
                    End If
                End If
            End If

            AddHandler oList.SelectedIndexChanged, AddressOf Me.SelectedItem

            oEditorService.DropDownControl(oList)
            If oList.SelectedIndices.Count = 1 Then
                value = oList.Text
            End If
            oEditorService.CloseDropDown()
        Else
            Return MyBase.EditValue(provider, value)
        End If

        Return value
    End Function

    Private Sub SelectedItem(ByVal sender As Object, ByVal e As EventArgs)
        If oEditorService IsNot Nothing Then
            If oList.SelectedValue IsNot Nothing Then oSelectedValue = oList.SelectedValue
            oEditorService.CloseDropDown()
        End If
    End Sub

End Class