<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRefChkUpdateTemplateObj
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRefChkUpdateTemplateObj))
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.gcTemplateObject = New DevExpress.XtraGrid.GridControl()
        Me.gvTemplateObject = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tlpTop = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.txtObjectName = New DevExpress.XtraEditors.TextEdit()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.btnCheck = New DevExpress.XtraEditors.SimpleButton()
        Me.btnUpdate = New DevExpress.XtraEditors.SimpleButton()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.tlpMain.SuspendLayout()
        CType(Me.gcTemplateObject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTemplateObject, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpTop.SuspendLayout()
        CType(Me.txtObjectName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.gcTemplateObject, 0, 2)
        Me.tlpMain.Controls.Add(Me.tlpTop, 0, 0)
        Me.tlpMain.Controls.Add(Me.LabelControl1, 0, 1)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 3
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Size = New System.Drawing.Size(690, 468)
        Me.tlpMain.TabIndex = 0
        '
        'gcTemplateObject
        '
        Me.gcTemplateObject.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcTemplateObject.Location = New System.Drawing.Point(3, 64)
        Me.gcTemplateObject.MainView = Me.gvTemplateObject
        Me.gcTemplateObject.Name = "gcTemplateObject"
        Me.gcTemplateObject.Size = New System.Drawing.Size(684, 401)
        Me.gcTemplateObject.TabIndex = 10
        Me.gcTemplateObject.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvTemplateObject})
        '
        'gvTemplateObject
        '
        Me.gvTemplateObject.GridControl = Me.gcTemplateObject
        Me.gvTemplateObject.Name = "gvTemplateObject"
        Me.gvTemplateObject.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvTemplateObject.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvTemplateObject.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvTemplateObject.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvTemplateObject.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvTemplateObject.OptionsSelection.MultiSelect = True
        Me.gvTemplateObject.OptionsView.ColumnAutoWidth = False
        Me.gvTemplateObject.OptionsView.ShowAutoFilterRow = True
        Me.gvTemplateObject.OptionsView.ShowGroupPanel = False
        '
        'tlpTop
        '
        Me.tlpTop.ColumnCount = 5
        Me.tlpTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.tlpTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.tlpTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.tlpTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.tlpTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpTop.Controls.Add(Me.LabelControl2, 0, 0)
        Me.tlpTop.Controls.Add(Me.txtObjectName, 1, 0)
        Me.tlpTop.Controls.Add(Me.lblMessage, 4, 0)
        Me.tlpTop.Controls.Add(Me.btnCheck, 2, 0)
        Me.tlpTop.Controls.Add(Me.btnUpdate, 3, 0)
        Me.tlpTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpTop.Location = New System.Drawing.Point(1, 1)
        Me.tlpTop.Margin = New System.Windows.Forms.Padding(1)
        Me.tlpTop.Name = "tlpTop"
        Me.tlpTop.RowCount = 1
        Me.tlpTop.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpTop.Size = New System.Drawing.Size(688, 31)
        Me.tlpTop.TabIndex = 11
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Options.UseTextOptions = True
        Me.LabelControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl2.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl2.Size = New System.Drawing.Size(74, 25)
        Me.LabelControl2.TabIndex = 22
        Me.LabelControl2.Text = "Object Name:"
        '
        'txtObjectName
        '
        Me.txtObjectName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtObjectName.Location = New System.Drawing.Point(83, 6)
        Me.txtObjectName.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.txtObjectName.Name = "txtObjectName"
        Me.txtObjectName.Properties.MaxLength = 200
        Me.txtObjectName.Size = New System.Drawing.Size(244, 20)
        Me.txtObjectName.TabIndex = 21
        Me.txtObjectName.ToolTip = "Enter description and press ENTER key to save the changes"
        '
        'lblMessage
        '
        Me.lblMessage.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Appearance.Options.UseForeColor = True
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(473, 3)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(212, 25)
        Me.lblMessage.TabIndex = 23
        '
        'btnCheck
        '
        Me.btnCheck.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCheck.Location = New System.Drawing.Point(332, 2)
        Me.btnCheck.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCheck.Name = "btnCheck"
        Me.btnCheck.Size = New System.Drawing.Size(66, 27)
        Me.btnCheck.TabIndex = 20
        Me.btnCheck.Text = "Check"
        '
        'btnUpdate
        '
        Me.btnUpdate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnUpdate.Location = New System.Drawing.Point(402, 2)
        Me.btnUpdate.Margin = New System.Windows.Forms.Padding(2)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(66, 27)
        Me.btnUpdate.TabIndex = 24
        Me.btnUpdate.Text = "Update"
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 36)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(684, 22)
        Me.LabelControl1.TabIndex = 12
        Me.LabelControl1.Text = "The update will happen for all the filtered rows."
        '
        'frmRefChkUpdateTemplateObj
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(690, 468)
        Me.Controls.Add(Me.tlpMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.Icon = CType(resources.GetObject("frmRefChkUpdateTemplateObj.IconOptions.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(700, 500)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(700, 500)
        Me.Name = "frmRefChkUpdateTemplateObj"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ref Check: Update Template Object"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        CType(Me.gcTemplateObject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTemplateObject, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpTop.ResumeLayout(False)
        Me.tlpTop.PerformLayout()
        CType(Me.txtObjectName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents gcTemplateObject As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvTemplateObject As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tlpTop As TableLayoutPanel
    Friend WithEvents btnCheck As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtObjectName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Timer1 As Timer
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnUpdate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
