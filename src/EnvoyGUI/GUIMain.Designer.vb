<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class GUIMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GUIMain))
        ToolStrip1 = New ToolStrip()
        ToolStripLabel1 = New ToolStripLabel()
        dpStart = New TSDatePicker()
        ToolStripLabel2 = New ToolStripLabel()
        dpFinish = New TSDatePicker()
        ToolStripLabel3 = New ToolStripLabel()
        cmbGrouping = New ToolStripComboBox()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnPlot = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnCSV = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnConfig = New ToolStripButton()
        PictureBox1 = New PictureBox()
        ToolStrip1.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.Items.AddRange(New ToolStripItem() {ToolStripLabel1, dpStart, ToolStripLabel2, dpFinish, ToolStripLabel3, cmbGrouping, ToolStripSeparator1, btnPlot, ToolStripSeparator2, btnCSV, ToolStripSeparator3, btnConfig})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(829, 26)
        ToolStrip1.TabIndex = 0
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' ToolStripLabel1
        ' 
        ToolStripLabel1.Name = "ToolStripLabel1"
        ToolStripLabel1.Size = New Size(31, 23)
        ToolStripLabel1.Text = "Start"
        ' 
        ' dpStart
        ' 
        dpStart.Name = "dpStart"
        dpStart.Size = New Size(200, 23)
        ' 
        ' ToolStripLabel2
        ' 
        ToolStripLabel2.Name = "ToolStripLabel2"
        ToolStripLabel2.Size = New Size(38, 23)
        ToolStripLabel2.Text = "Finish"
        ' 
        ' dpFinish
        ' 
        dpFinish.Name = "dpFinish"
        dpFinish.Size = New Size(200, 23)
        ' 
        ' ToolStripLabel3
        ' 
        ToolStripLabel3.Name = "ToolStripLabel3"
        ToolStripLabel3.Size = New Size(57, 23)
        ToolStripLabel3.Text = "Grouping"
        ' 
        ' cmbGrouping
        ' 
        cmbGrouping.FlatStyle = FlatStyle.Flat
        cmbGrouping.Items.AddRange(New Object() {"Hourly", "Daily", "Monthly"})
        cmbGrouping.Name = "cmbGrouping"
        cmbGrouping.Size = New Size(121, 26)
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 26)
        ' 
        ' btnPlot
        ' 
        btnPlot.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnPlot.Image = CType(resources.GetObject("btnPlot.Image"), Image)
        btnPlot.ImageAlign = ContentAlignment.MiddleRight
        btnPlot.ImageTransparentColor = Color.Magenta
        btnPlot.Name = "btnPlot"
        btnPlot.Size = New Size(32, 23)
        btnPlot.Text = "Plot"
        btnPlot.TextImageRelation = TextImageRelation.TextBeforeImage
        btnPlot.ToolTipText = "Plot the graph for the selcted dates"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 26)
        ' 
        ' btnCSV
        ' 
        btnCSV.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnCSV.Image = CType(resources.GetObject("btnCSV.Image"), Image)
        btnCSV.ImageAlign = ContentAlignment.MiddleRight
        btnCSV.ImageTransparentColor = Color.Magenta
        btnCSV.Name = "btnCSV"
        btnCSV.Size = New Size(32, 23)
        btnCSV.Text = "CSV"
        btnCSV.TextImageRelation = TextImageRelation.TextBeforeImage
        btnCSV.ToolTipText = "Export the data for the selected dates to a CSV file"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(6, 26)
        ' 
        ' btnConfig
        ' 
        btnConfig.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnConfig.Image = CType(resources.GetObject("btnConfig.Image"), Image)
        btnConfig.ImageAlign = ContentAlignment.MiddleRight
        btnConfig.ImageTransparentColor = Color.Magenta
        btnConfig.Name = "btnConfig"
        btnConfig.Size = New Size(47, 23)
        btnConfig.Text = "Config"
        btnConfig.TextImageRelation = TextImageRelation.TextBeforeImage
        btnConfig.ToolTipText = "Display config.json"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Dock = DockStyle.Fill
        PictureBox1.Location = New Point(0, 26)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(829, 555)
        PictureBox1.TabIndex = 1
        PictureBox1.TabStop = False
        ' 
        ' GUIMain
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(829, 581)
        Controls.Add(PictureBox1)
        Controls.Add(ToolStrip1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "GUIMain"
        Text = "EnvoyGUI"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents dpStart As TSDatePicker
    Friend WithEvents ToolStripLabel2 As ToolStripLabel
    Friend WithEvents dpFinish As TSDatePicker
    Friend WithEvents ToolStripLabel3 As ToolStripLabel
    Friend WithEvents cmbGrouping As ToolStripComboBox
    Friend WithEvents btnPlot As ToolStripButton
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents btnCSV As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents btnConfig As ToolStripButton

End Class
