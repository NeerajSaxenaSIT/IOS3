<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCompare4G
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
        Dim Annotation1 As dotnetCHARTING.WinForms.Annotation = New dotnetCHARTING.WinForms.Annotation()
        Dim Label1 As dotnetCHARTING.WinForms.Label = New dotnetCHARTING.WinForms.Label()
        Dim Annotation2 As dotnetCHARTING.WinForms.Annotation = New dotnetCHARTING.WinForms.Annotation()
        Dim Label2 As dotnetCHARTING.WinForms.Label = New dotnetCHARTING.WinForms.Label()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCompare4G))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.Chart1 = New dotnetCHARTING.WinForms.Chart()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.Chart2 = New dotnetCHARTING.WinForms.Chart()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.Chart2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.PanelControl1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.PanelControl2, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(884, 561)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.Chart1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(3, 3)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(878, 274)
        Me.PanelControl1.TabIndex = 0
		'
		'Chart1
		'
		Me.Chart1.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
		Me.Chart1.ApplicationDNC = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
		Me.Chart1.Background.Color = System.Drawing.Color.White
        Annotation1.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        Annotation1.DynamicSize = True
        Annotation1.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Annotation1.InteriorLine.Visible = True
        Annotation1.Line.Color = System.Drawing.Color.Gray
        Annotation1.Line.Visible = True
        Annotation1.Orientation = dotnetCHARTING.WinForms.Orientation.TopRight
        Annotation1.Padding = 2
        Annotation1.Shadow.Visible = False
        Annotation1.Size = New System.Drawing.Size(873, 269)
        Annotation1.Visible = True
        Me.Chart1.Box = Annotation1
        Me.Chart1.ChartArea.Background.Color = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.Chart1.ChartArea.CornerTopLeft = dotnetCHARTING.WinForms.BoxCorner.Square
        Me.Chart1.ChartArea.DefaultElement.DefaultSubValue.Line.Color = System.Drawing.Color.FromArgb(CType(CType(93, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.Chart1.ChartArea.DefaultElement.DefaultSubValue.Line.Visible = True
        Me.Chart1.ChartArea.DefaultElement.DefaultSubValue.Visible = True
        Me.Chart1.ChartArea.DefaultElement.LegendEntry.DividerLine.Color = System.Drawing.Color.Empty
        Me.Chart1.ChartArea.DefaultElement.LegendEntry.DividerLine.Visible = True
        Me.Chart1.ChartArea.DefaultElement.Outline.Visible = True
        Me.Chart1.ChartArea.DefaultElement.SmartLabel.Color = System.Drawing.Color.Empty
        Me.Chart1.ChartArea.DefaultElement.SmartLabel.Line.Visible = True
        Me.Chart1.ChartArea.InteriorLine.Color = System.Drawing.Color.LightGray
        Me.Chart1.ChartArea.InteriorLine.Visible = True
        Me.Chart1.ChartArea.Label.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Chart1.ChartArea.LegendBox.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.Chart1.ChartArea.LegendBox.CornerBottomRight = dotnetCHARTING.WinForms.BoxCorner.Cut
        Me.Chart1.ChartArea.LegendBox.DefaultEntry.DividerLine.Color = System.Drawing.Color.Empty
        Me.Chart1.ChartArea.LegendBox.DefaultEntry.DividerLine.Visible = True
        Me.Chart1.ChartArea.LegendBox.HeaderEntry.DividerLine.Color = System.Drawing.Color.Gray
        Me.Chart1.ChartArea.LegendBox.HeaderEntry.DividerLine.Visible = True
        Me.Chart1.ChartArea.LegendBox.HeaderEntry.Name = "Name"
        Me.Chart1.ChartArea.LegendBox.HeaderEntry.SortOrder = -1
        Me.Chart1.ChartArea.LegendBox.HeaderEntry.Value = "Value"
        Me.Chart1.ChartArea.LegendBox.HeaderEntry.Visible = False
        Me.Chart1.ChartArea.LegendBox.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Chart1.ChartArea.LegendBox.InteriorLine.Visible = True
        Me.Chart1.ChartArea.LegendBox.Line.Color = System.Drawing.Color.Gray
        Me.Chart1.ChartArea.LegendBox.Line.Visible = True
        Me.Chart1.ChartArea.LegendBox.Padding = 4
        Me.Chart1.ChartArea.LegendBox.Position = dotnetCHARTING.WinForms.LegendBoxPosition.Top
        Me.Chart1.ChartArea.LegendBox.Visible = True
        Me.Chart1.ChartArea.Line.Color = System.Drawing.Color.Gray
        Me.Chart1.ChartArea.Line.Visible = True
        Me.Chart1.ChartArea.StartDateOfYear = New Date(CType(0, Long))
        Me.Chart1.ChartArea.TitleBox.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.Chart1.ChartArea.TitleBox.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Chart1.ChartArea.TitleBox.InteriorLine.Visible = True
        Me.Chart1.ChartArea.TitleBox.Label.Color = System.Drawing.Color.FromArgb(CType(CType(97, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.Chart1.ChartArea.TitleBox.Label.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Chart1.ChartArea.TitleBox.Line.Color = System.Drawing.Color.Gray
        Me.Chart1.ChartArea.TitleBox.Line.Visible = True
        Me.Chart1.ChartArea.TitleBox.Visible = True
        Me.Chart1.ChartArea.XAxis.DefaultTick.GridLine.Color = System.Drawing.Color.LightGray
        Me.Chart1.ChartArea.XAxis.DefaultTick.GridLine.Visible = True
        Me.Chart1.ChartArea.XAxis.DefaultTick.Line.Length = 3
        Me.Chart1.ChartArea.XAxis.DefaultTick.Line.Visible = True
        Me.Chart1.ChartArea.XAxis.MinorTimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.Chart1.ChartArea.XAxis.ScaleBreakLine.Color = System.Drawing.Color.Gray
        Me.Chart1.ChartArea.XAxis.ScaleBreakLine.Visible = True
        Me.Chart1.ChartArea.XAxis.TickLabelSeparatorLine.Visible = True
        Me.Chart1.ChartArea.XAxis.TimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.Chart1.ChartArea.XAxis.ZeroTick.GridLine.Color = System.Drawing.Color.Red
        Me.Chart1.ChartArea.XAxis.ZeroTick.GridLine.Visible = True
        Me.Chart1.ChartArea.XAxis.ZeroTick.Line.Length = 3
        Me.Chart1.ChartArea.XAxis.ZeroTick.Line.Visible = True
        Me.Chart1.ChartArea.YAxis.DefaultTick.GridLine.Color = System.Drawing.Color.LightGray
        Me.Chart1.ChartArea.YAxis.DefaultTick.GridLine.Visible = True
        Me.Chart1.ChartArea.YAxis.DefaultTick.Line.Length = 3
        Me.Chart1.ChartArea.YAxis.DefaultTick.Line.Visible = True
        Me.Chart1.ChartArea.YAxis.ScaleBreakLine.Color = System.Drawing.Color.Gray
        Me.Chart1.ChartArea.YAxis.ScaleBreakLine.Visible = True
        Me.Chart1.ChartArea.YAxis.TickLabelSeparatorLine.Visible = True
        Me.Chart1.ChartArea.YAxis.TimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.Chart1.ChartArea.YAxis.ZeroTick.GridLine.Color = System.Drawing.Color.Red
        Me.Chart1.ChartArea.YAxis.ZeroTick.GridLine.Visible = True
        Me.Chart1.ChartArea.YAxis.ZeroTick.Line.Length = 3
        Me.Chart1.ChartArea.YAxis.ZeroTick.Line.Visible = True
        Me.Chart1.DataGrid = Nothing
        Me.Chart1.DefaultElement.DefaultSubValue.Line.Visible = True
        Me.Chart1.DefaultElement.DefaultSubValue.Visible = True
        Me.Chart1.DefaultElement.LegendEntry.DividerLine.Color = System.Drawing.Color.Empty
        Me.Chart1.DefaultElement.LegendEntry.DividerLine.Visible = True
        Me.Chart1.DefaultElement.Outline.Visible = True
        Me.Chart1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Chart1.Location = New System.Drawing.Point(2, 2)
        Me.Chart1.Name = "Chart1"
        Me.Chart1.NoDataLabel.Text = "No Data"
        Me.Chart1.ObjectChart = Label1
        Me.Chart1.Size = New System.Drawing.Size(874, 270)
        Me.Chart1.SmartLabelLine.Visible = True
        Me.Chart1.StartDateOfYear = New Date(CType(0, Long))
        Me.Chart1.TabIndex = 2
        Me.Chart1.TempDirectory = "C:\Users\Charul\AppData\Local\Temp\"
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.Chart2)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl2.Location = New System.Drawing.Point(3, 283)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(878, 275)
        Me.PanelControl2.TabIndex = 1
		'
		'Chart2
		'
		Me.Chart2.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
		Me.Chart2.ApplicationDNC = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
		Me.Chart2.Background.Color = System.Drawing.Color.White
        Annotation2.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        Annotation2.DynamicSize = True
        Annotation2.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Annotation2.InteriorLine.Visible = True
        Annotation2.Line.Color = System.Drawing.Color.Gray
        Annotation2.Line.Visible = True
        Annotation2.Orientation = dotnetCHARTING.WinForms.Orientation.TopRight
        Annotation2.Padding = 2
        Annotation2.Shadow.Visible = False
        Annotation2.Size = New System.Drawing.Size(873, 270)
        Annotation2.Visible = True
        Me.Chart2.Box = Annotation2
        Me.Chart2.ChartArea.Background.Color = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.Chart2.ChartArea.CornerTopLeft = dotnetCHARTING.WinForms.BoxCorner.Square
        Me.Chart2.ChartArea.DefaultElement.DefaultSubValue.Line.Color = System.Drawing.Color.FromArgb(CType(CType(93, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.Chart2.ChartArea.DefaultElement.DefaultSubValue.Line.Visible = True
        Me.Chart2.ChartArea.DefaultElement.DefaultSubValue.Visible = True
        Me.Chart2.ChartArea.DefaultElement.LegendEntry.DividerLine.Color = System.Drawing.Color.Empty
        Me.Chart2.ChartArea.DefaultElement.LegendEntry.DividerLine.Visible = True
        Me.Chart2.ChartArea.DefaultElement.Outline.Visible = True
        Me.Chart2.ChartArea.DefaultElement.SmartLabel.Color = System.Drawing.Color.Empty
        Me.Chart2.ChartArea.DefaultElement.SmartLabel.Line.Visible = True
        Me.Chart2.ChartArea.InteriorLine.Color = System.Drawing.Color.LightGray
        Me.Chart2.ChartArea.InteriorLine.Visible = True
        Me.Chart2.ChartArea.Label.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Chart2.ChartArea.LegendBox.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.Chart2.ChartArea.LegendBox.CornerBottomRight = dotnetCHARTING.WinForms.BoxCorner.Cut
        Me.Chart2.ChartArea.LegendBox.DefaultEntry.DividerLine.Color = System.Drawing.Color.Empty
        Me.Chart2.ChartArea.LegendBox.DefaultEntry.DividerLine.Visible = True
        Me.Chart2.ChartArea.LegendBox.HeaderEntry.DividerLine.Color = System.Drawing.Color.Gray
        Me.Chart2.ChartArea.LegendBox.HeaderEntry.DividerLine.Visible = True
        Me.Chart2.ChartArea.LegendBox.HeaderEntry.Name = "Name"
        Me.Chart2.ChartArea.LegendBox.HeaderEntry.SortOrder = -1
        Me.Chart2.ChartArea.LegendBox.HeaderEntry.Value = "Value"
        Me.Chart2.ChartArea.LegendBox.HeaderEntry.Visible = False
        Me.Chart2.ChartArea.LegendBox.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Chart2.ChartArea.LegendBox.InteriorLine.Visible = True
        Me.Chart2.ChartArea.LegendBox.Line.Color = System.Drawing.Color.Gray
        Me.Chart2.ChartArea.LegendBox.Line.Visible = True
        Me.Chart2.ChartArea.LegendBox.Padding = 4
        Me.Chart2.ChartArea.LegendBox.Position = dotnetCHARTING.WinForms.LegendBoxPosition.Top
        Me.Chart2.ChartArea.LegendBox.Visible = True
        Me.Chart2.ChartArea.Line.Color = System.Drawing.Color.Gray
        Me.Chart2.ChartArea.Line.Visible = True
        Me.Chart2.ChartArea.StartDateOfYear = New Date(CType(0, Long))
        Me.Chart2.ChartArea.TitleBox.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.Chart2.ChartArea.TitleBox.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Chart2.ChartArea.TitleBox.InteriorLine.Visible = True
        Me.Chart2.ChartArea.TitleBox.Label.Color = System.Drawing.Color.FromArgb(CType(CType(97, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.Chart2.ChartArea.TitleBox.Label.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Chart2.ChartArea.TitleBox.Line.Color = System.Drawing.Color.Gray
        Me.Chart2.ChartArea.TitleBox.Line.Visible = True
        Me.Chart2.ChartArea.TitleBox.Visible = True
        Me.Chart2.ChartArea.XAxis.DefaultTick.GridLine.Color = System.Drawing.Color.LightGray
        Me.Chart2.ChartArea.XAxis.DefaultTick.GridLine.Visible = True
        Me.Chart2.ChartArea.XAxis.DefaultTick.Line.Length = 3
        Me.Chart2.ChartArea.XAxis.DefaultTick.Line.Visible = True
        Me.Chart2.ChartArea.XAxis.MinorTimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.Chart2.ChartArea.XAxis.ScaleBreakLine.Color = System.Drawing.Color.Gray
        Me.Chart2.ChartArea.XAxis.ScaleBreakLine.Visible = True
        Me.Chart2.ChartArea.XAxis.TickLabelSeparatorLine.Visible = True
        Me.Chart2.ChartArea.XAxis.TimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.Chart2.ChartArea.XAxis.ZeroTick.GridLine.Color = System.Drawing.Color.Red
        Me.Chart2.ChartArea.XAxis.ZeroTick.GridLine.Visible = True
        Me.Chart2.ChartArea.XAxis.ZeroTick.Line.Length = 3
        Me.Chart2.ChartArea.XAxis.ZeroTick.Line.Visible = True
        Me.Chart2.ChartArea.YAxis.DefaultTick.GridLine.Color = System.Drawing.Color.LightGray
        Me.Chart2.ChartArea.YAxis.DefaultTick.GridLine.Visible = True
        Me.Chart2.ChartArea.YAxis.DefaultTick.Line.Length = 3
        Me.Chart2.ChartArea.YAxis.DefaultTick.Line.Visible = True
        Me.Chart2.ChartArea.YAxis.ScaleBreakLine.Color = System.Drawing.Color.Gray
        Me.Chart2.ChartArea.YAxis.ScaleBreakLine.Visible = True
        Me.Chart2.ChartArea.YAxis.TickLabelSeparatorLine.Visible = True
        Me.Chart2.ChartArea.YAxis.TimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.Chart2.ChartArea.YAxis.ZeroTick.GridLine.Color = System.Drawing.Color.Red
        Me.Chart2.ChartArea.YAxis.ZeroTick.GridLine.Visible = True
        Me.Chart2.ChartArea.YAxis.ZeroTick.Line.Length = 3
        Me.Chart2.ChartArea.YAxis.ZeroTick.Line.Visible = True
        Me.Chart2.DataGrid = Nothing
        Me.Chart2.DefaultElement.DefaultSubValue.Line.Visible = True
        Me.Chart2.DefaultElement.DefaultSubValue.Visible = True
        Me.Chart2.DefaultElement.LegendEntry.DividerLine.Color = System.Drawing.Color.Empty
        Me.Chart2.DefaultElement.LegendEntry.DividerLine.Visible = True
        Me.Chart2.DefaultElement.Outline.Visible = True
        Me.Chart2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Chart2.Location = New System.Drawing.Point(2, 2)
        Me.Chart2.Name = "Chart2"
        Me.Chart2.NoDataLabel.Text = "No Data"
        Me.Chart2.ObjectChart = Label2
        Me.Chart2.Size = New System.Drawing.Size(874, 271)
        Me.Chart2.SmartLabelLine.Visible = True
        Me.Chart2.StartDateOfYear = New Date(CType(0, Long))
        Me.Chart2.TabIndex = 3
        Me.Chart2.TempDirectory = "C:\Users\Charul\AppData\Local\Temp\"
        '
        'frmCompare4G
        '
        Me.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.Appearance.Options.UseForeColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 561)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.LookAndFeel.SkinName = "Office 2013"
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCompare4G"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Compare Drive Test"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.Chart2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents Chart1 As dotnetCHARTING.WinForms.Chart
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents Chart2 As dotnetCHARTING.WinForms.Chart
End Class
