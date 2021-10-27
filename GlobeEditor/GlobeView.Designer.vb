<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GlobeView
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Me.MainMenu = New System.Windows.Forms.MenuStrip()
		Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
		Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
		Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ToolboxToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.KeyboardTimer = New System.Windows.Forms.Timer(Me.components)
		Me.BackgroundToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.MainMenu.SuspendLayout()
		Me.SuspendLayout()
		'
		'MainMenu
		'
		Me.MainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ViewToolStripMenuItem})
		Me.MainMenu.Location = New System.Drawing.Point(0, 0)
		Me.MainMenu.Name = "MainMenu"
		Me.MainMenu.Size = New System.Drawing.Size(614, 24)
		Me.MainMenu.TabIndex = 0
		Me.MainMenu.Text = "MainMenu"
		'
		'FileToolStripMenuItem
		'
		Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.ToolStripMenuItem1, Me.OpenToolStripMenuItem, Me.SaveToolStripMenuItem, Me.SaveAsToolStripMenuItem, Me.ToolStripMenuItem2, Me.ExitToolStripMenuItem})
		Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
		Me.FileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
		Me.FileToolStripMenuItem.Text = "File"
		'
		'NewToolStripMenuItem
		'
		Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
		Me.NewToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
		Me.NewToolStripMenuItem.Text = "New"
		'
		'ToolStripMenuItem1
		'
		Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
		Me.ToolStripMenuItem1.Size = New System.Drawing.Size(119, 6)
		'
		'OpenToolStripMenuItem
		'
		Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
		Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
		Me.OpenToolStripMenuItem.Text = "Open"
		'
		'SaveToolStripMenuItem
		'
		Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
		Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
		Me.SaveToolStripMenuItem.Text = "Save"
		'
		'SaveAsToolStripMenuItem
		'
		Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
		Me.SaveAsToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
		Me.SaveAsToolStripMenuItem.Text = "Save as..."
		'
		'ToolStripMenuItem2
		'
		Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
		Me.ToolStripMenuItem2.Size = New System.Drawing.Size(119, 6)
		'
		'ExitToolStripMenuItem
		'
		Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
		Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
		Me.ExitToolStripMenuItem.Text = "Exit"
		'
		'ViewToolStripMenuItem
		'
		Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolboxToolStripMenuItem, Me.BackgroundToolStripMenuItem})
		Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
		Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(42, 20)
		Me.ViewToolStripMenuItem.Text = "View"
		'
		'ToolboxToolStripMenuItem
		'
		Me.ToolboxToolStripMenuItem.Name = "ToolboxToolStripMenuItem"
		Me.ToolboxToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.ToolboxToolStripMenuItem.Text = "Toolbox"
		'
		'KeyboardTimer
		'
		Me.KeyboardTimer.Enabled = True
		Me.KeyboardTimer.Interval = 20
		'
		'BackgroundToolStripMenuItem
		'
		Me.BackgroundToolStripMenuItem.Name = "BackgroundToolStripMenuItem"
		Me.BackgroundToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.BackgroundToolStripMenuItem.Text = "Background"
		'
		'GlobeView
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(614, 375)
		Me.Controls.Add(Me.MainMenu)
		Me.DoubleBuffered = True
		Me.MainMenuStrip = Me.MainMenu
		Me.Name = "GlobeView"
		Me.Text = "Globe"
		Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
		Me.MainMenu.ResumeLayout(False)
		Me.MainMenu.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents MainMenu As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents OpenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveAsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents KeyboardTimer As Timer
    Friend WithEvents ViewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolboxToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents BackgroundToolStripMenuItem As ToolStripMenuItem
End Class
