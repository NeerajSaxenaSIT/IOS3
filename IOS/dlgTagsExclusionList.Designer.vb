<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgTagsExclusionList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgTagsExclusionList))
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.gcTagsExcList = New DevExpress.XtraGrid.GridControl()
        Me.gvTagsExcList = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.btnOk = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.tlpMain.SuspendLayout()
        CType(Me.gcTagsExcList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTagsExcList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.gcTagsExcList, 0, 0)
        Me.tlpMain.Controls.Add(Me.TableLayoutPanel1, 0, 1)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 2
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tlpMain.Size = New System.Drawing.Size(448, 318)
        Me.tlpMain.TabIndex = 0
        '
        'gcTagsExcList
        '
        Me.gcTagsExcList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcTagsExcList.Location = New System.Drawing.Point(3, 3)
        Me.gcTagsExcList.MainView = Me.gvTagsExcList
        Me.gcTagsExcList.Name = "gcTagsExcList"
        Me.gcTagsExcList.Size = New System.Drawing.Size(442, 277)
        Me.gcTagsExcList.TabIndex = 10
        Me.gcTagsExcList.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvTagsExcList})
        '
        'gvTagsExcList
        '
        Me.gvTagsExcList.GridControl = Me.gcTagsExcList
        Me.gvTagsExcList.Name = "gvTagsExcList"
        Me.gvTagsExcList.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvTagsExcList.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvTagsExcList.OptionsView.ColumnAutoWidth = False
        Me.gvTagsExcList.OptionsView.ShowAutoFilterRow = True
        Me.gvTagsExcList.OptionsView.ShowGroupPanel = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lblMessage, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnOk, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnCancel, 2, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(1, 284)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(446, 33)
        Me.TableLayoutPanel1.TabIndex = 11
        '
        'lblMessage
        '
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 3)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(280, 27)
        Me.lblMessage.TabIndex = 3
        '
        'btnOk
        '
        Me.btnOk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOk.Location = New System.Drawing.Point(289, 3)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(74, 27)
        Me.btnOk.TabIndex = 4
        Me.btnOk.Text = "OK"
        '
        'btnCancel
        '
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCancel.Location = New System.Drawing.Point(369, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(74, 27)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Cancel"
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'dlgTagsExclusionList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(448, 318)
        Me.Controls.Add(Me.tlpMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.Image = CType(resources.GetObject("dlgTagsExclusionList.IconOptions.Image"), System.Drawing.Image)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(450, 350)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(450, 350)
        Me.Name = "dlgTagsExclusionList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PM TopX - Tags Exclusion List"
        Me.tlpMain.ResumeLayout(False)
        CType(Me.gcTagsExcList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTagsExcList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents gcTagsExcList As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvTagsExcList As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Timer1 As Timer
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnOk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
End Class
