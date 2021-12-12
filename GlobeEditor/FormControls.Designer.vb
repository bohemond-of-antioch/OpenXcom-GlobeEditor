<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormControls
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormControls))
		Me.ComboBoxMode = New System.Windows.Forms.ComboBox()
		Me.PanelEditPolygons = New System.Windows.Forms.Panel()
		Me.ButtonOptimizeAll = New System.Windows.Forms.Button()
		Me.ButtonDelaunayOptimization = New System.Windows.Forms.Button()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.PanelTextures = New System.Windows.Forms.Panel()
		Me.TextureBoxTemplate = New System.Windows.Forms.PictureBox()
		Me.PanelAreas = New System.Windows.Forms.Panel()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.AreaColor = New System.Windows.Forms.PictureBox()
		Me.AreaTextBox = New System.Windows.Forms.TextBox()
		Me.AreaButtonDelete = New System.Windows.Forms.Button()
		Me.AreaButtonAdd = New System.Windows.Forms.Button()
		Me.AreaListBox = New System.Windows.Forms.ListBox()
		Me.PanelZones = New System.Windows.Forms.Panel()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.ZonesButtonRemove = New System.Windows.Forms.Button()
		Me.ZonesButtonAdd = New System.Windows.Forms.Button()
		Me.ZonesColor = New System.Windows.Forms.PictureBox()
		Me.ZonesListBox = New System.Windows.Forms.ListBox()
		Me.ZonesRegionsListBox = New System.Windows.Forms.ListBox()
		Me.PanelCountries = New System.Windows.Forms.Panel()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.CountryColor = New System.Windows.Forms.PictureBox()
		Me.CountryTextBox = New System.Windows.Forms.TextBox()
		Me.CountryButtonDelete = New System.Windows.Forms.Button()
		Me.CountryButtonAdd = New System.Windows.Forms.Button()
		Me.CountryListBox = New System.Windows.Forms.ListBox()
		Me.PanelBorders = New System.Windows.Forms.Panel()
		Me.Label8 = New System.Windows.Forms.Label()
		Me.PanelEditPolygons.SuspendLayout()
		Me.PanelTextures.SuspendLayout()
		CType(Me.TextureBoxTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.PanelAreas.SuspendLayout()
		CType(Me.AreaColor, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.PanelZones.SuspendLayout()
		CType(Me.ZonesColor, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.PanelCountries.SuspendLayout()
		CType(Me.CountryColor, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.PanelBorders.SuspendLayout()
		Me.SuspendLayout()
		'
		'ComboBoxMode
		'
		Me.ComboBoxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.ComboBoxMode.FormattingEnabled = True
		Me.ComboBoxMode.Location = New System.Drawing.Point(0, 0)
		Me.ComboBoxMode.Name = "ComboBoxMode"
		Me.ComboBoxMode.Size = New System.Drawing.Size(184, 21)
		Me.ComboBoxMode.TabIndex = 0
		'
		'PanelEditPolygons
		'
		Me.PanelEditPolygons.BackColor = System.Drawing.SystemColors.Control
		Me.PanelEditPolygons.Controls.Add(Me.ButtonOptimizeAll)
		Me.PanelEditPolygons.Controls.Add(Me.ButtonDelaunayOptimization)
		Me.PanelEditPolygons.Controls.Add(Me.Label1)
		Me.PanelEditPolygons.Location = New System.Drawing.Point(0, 24)
		Me.PanelEditPolygons.Name = "PanelEditPolygons"
		Me.PanelEditPolygons.Size = New System.Drawing.Size(392, 256)
		Me.PanelEditPolygons.TabIndex = 1
		'
		'ButtonOptimizeAll
		'
		Me.ButtonOptimizeAll.Location = New System.Drawing.Point(128, 160)
		Me.ButtonOptimizeAll.Name = "ButtonOptimizeAll"
		Me.ButtonOptimizeAll.Size = New System.Drawing.Size(48, 24)
		Me.ButtonOptimizeAll.TabIndex = 2
		Me.ButtonOptimizeAll.Text = "All"
		Me.ButtonOptimizeAll.UseVisualStyleBackColor = True
		Me.ButtonOptimizeAll.Visible = False
		'
		'ButtonDelaunayOptimization
		'
		Me.ButtonDelaunayOptimization.Location = New System.Drawing.Point(8, 160)
		Me.ButtonDelaunayOptimization.Name = "ButtonDelaunayOptimization"
		Me.ButtonDelaunayOptimization.Size = New System.Drawing.Size(120, 24)
		Me.ButtonDelaunayOptimization.TabIndex = 1
		Me.ButtonDelaunayOptimization.Text = "Delaunay Optimization"
		Me.ButtonDelaunayOptimization.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(8, 8)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(257, 130)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = resources.GetString("Label1.Text")
		'
		'PanelTextures
		'
		Me.PanelTextures.Controls.Add(Me.TextureBoxTemplate)
		Me.PanelTextures.Location = New System.Drawing.Point(0, 24)
		Me.PanelTextures.Name = "PanelTextures"
		Me.PanelTextures.Size = New System.Drawing.Size(392, 256)
		Me.PanelTextures.TabIndex = 2
		'
		'TextureBoxTemplate
		'
		Me.TextureBoxTemplate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.TextureBoxTemplate.Location = New System.Drawing.Point(8, 8)
		Me.TextureBoxTemplate.Name = "TextureBoxTemplate"
		Me.TextureBoxTemplate.Size = New System.Drawing.Size(32, 32)
		Me.TextureBoxTemplate.TabIndex = 0
		Me.TextureBoxTemplate.TabStop = False
		Me.TextureBoxTemplate.Visible = False
		'
		'PanelAreas
		'
		Me.PanelAreas.Controls.Add(Me.Label6)
		Me.PanelAreas.Controls.Add(Me.Label7)
		Me.PanelAreas.Controls.Add(Me.AreaColor)
		Me.PanelAreas.Controls.Add(Me.AreaTextBox)
		Me.PanelAreas.Controls.Add(Me.AreaButtonDelete)
		Me.PanelAreas.Controls.Add(Me.AreaButtonAdd)
		Me.PanelAreas.Controls.Add(Me.AreaListBox)
		Me.PanelAreas.Location = New System.Drawing.Point(0, 24)
		Me.PanelAreas.Name = "PanelAreas"
		Me.PanelAreas.Size = New System.Drawing.Size(392, 256)
		Me.PanelAreas.TabIndex = 3
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Location = New System.Drawing.Point(192, 208)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(170, 26)
		Me.Label6.TabIndex = 8
		Me.Label6.Text = "Shift+Drag to enable snapping" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Shift+Ctrl+Drag to snap to all areas"
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.Location = New System.Drawing.Point(8, 208)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(173, 26)
		Me.Label7.TabIndex = 7
		Me.Label7.Text = "Ctrl+Click to add a new area" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Delete to remove the selected area" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
		'
		'AreaColor
		'
		Me.AreaColor.Location = New System.Drawing.Point(184, 8)
		Me.AreaColor.Name = "AreaColor"
		Me.AreaColor.Size = New System.Drawing.Size(104, 40)
		Me.AreaColor.TabIndex = 4
		Me.AreaColor.TabStop = False
		'
		'AreaTextBox
		'
		Me.AreaTextBox.Location = New System.Drawing.Point(8, 128)
		Me.AreaTextBox.Name = "AreaTextBox"
		Me.AreaTextBox.Size = New System.Drawing.Size(168, 20)
		Me.AreaTextBox.TabIndex = 3
		'
		'AreaButtonDelete
		'
		Me.AreaButtonDelete.Location = New System.Drawing.Point(88, 160)
		Me.AreaButtonDelete.Name = "AreaButtonDelete"
		Me.AreaButtonDelete.Size = New System.Drawing.Size(48, 24)
		Me.AreaButtonDelete.TabIndex = 2
		Me.AreaButtonDelete.Text = "Delete"
		Me.AreaButtonDelete.UseVisualStyleBackColor = True
		'
		'AreaButtonAdd
		'
		Me.AreaButtonAdd.Location = New System.Drawing.Point(8, 160)
		Me.AreaButtonAdd.Name = "AreaButtonAdd"
		Me.AreaButtonAdd.Size = New System.Drawing.Size(48, 24)
		Me.AreaButtonAdd.TabIndex = 1
		Me.AreaButtonAdd.Text = "Add"
		Me.AreaButtonAdd.UseVisualStyleBackColor = True
		'
		'AreaListBox
		'
		Me.AreaListBox.FormattingEnabled = True
		Me.AreaListBox.Location = New System.Drawing.Point(8, 8)
		Me.AreaListBox.Name = "AreaListBox"
		Me.AreaListBox.Size = New System.Drawing.Size(168, 108)
		Me.AreaListBox.TabIndex = 0
		'
		'PanelZones
		'
		Me.PanelZones.Controls.Add(Me.Label3)
		Me.PanelZones.Controls.Add(Me.Label2)
		Me.PanelZones.Controls.Add(Me.ZonesButtonRemove)
		Me.PanelZones.Controls.Add(Me.ZonesButtonAdd)
		Me.PanelZones.Controls.Add(Me.ZonesColor)
		Me.PanelZones.Controls.Add(Me.ZonesListBox)
		Me.PanelZones.Controls.Add(Me.ZonesRegionsListBox)
		Me.PanelZones.Location = New System.Drawing.Point(0, 24)
		Me.PanelZones.Name = "PanelZones"
		Me.PanelZones.Size = New System.Drawing.Size(392, 256)
		Me.PanelZones.TabIndex = 4
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(184, 216)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(172, 26)
		Me.Label3.TabIndex = 6
		Me.Label3.Text = "Shift+Drag to enable snapping" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Shift+Ctrl+Drag to snap to all zones"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(0, 216)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(179, 39)
		Me.Label2.TabIndex = 5
		Me.Label2.Text = "Ctrl+Click to add a new mission zone" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ctrl+Alt+Click to add pointlike zone" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Delet" &
	"e to remove the selected zone" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
		'
		'ZonesButtonRemove
		'
		Me.ZonesButtonRemove.Location = New System.Drawing.Point(224, 184)
		Me.ZonesButtonRemove.Name = "ZonesButtonRemove"
		Me.ZonesButtonRemove.Size = New System.Drawing.Size(56, 24)
		Me.ZonesButtonRemove.TabIndex = 4
		Me.ZonesButtonRemove.Text = "Remove"
		Me.ZonesButtonRemove.UseVisualStyleBackColor = True
		'
		'ZonesButtonAdd
		'
		Me.ZonesButtonAdd.Location = New System.Drawing.Point(184, 184)
		Me.ZonesButtonAdd.Name = "ZonesButtonAdd"
		Me.ZonesButtonAdd.Size = New System.Drawing.Size(40, 24)
		Me.ZonesButtonAdd.TabIndex = 3
		Me.ZonesButtonAdd.Text = "Add"
		Me.ZonesButtonAdd.UseVisualStyleBackColor = True
		'
		'ZonesColor
		'
		Me.ZonesColor.Location = New System.Drawing.Point(360, 8)
		Me.ZonesColor.Name = "ZonesColor"
		Me.ZonesColor.Size = New System.Drawing.Size(24, 96)
		Me.ZonesColor.TabIndex = 2
		Me.ZonesColor.TabStop = False
		'
		'ZonesListBox
		'
		Me.ZonesListBox.FormattingEnabled = True
		Me.ZonesListBox.Location = New System.Drawing.Point(184, 8)
		Me.ZonesListBox.Name = "ZonesListBox"
		Me.ZonesListBox.Size = New System.Drawing.Size(168, 173)
		Me.ZonesListBox.TabIndex = 1
		'
		'ZonesRegionsListBox
		'
		Me.ZonesRegionsListBox.FormattingEnabled = True
		Me.ZonesRegionsListBox.Location = New System.Drawing.Point(8, 8)
		Me.ZonesRegionsListBox.Name = "ZonesRegionsListBox"
		Me.ZonesRegionsListBox.Size = New System.Drawing.Size(168, 173)
		Me.ZonesRegionsListBox.TabIndex = 0
		'
		'PanelCountries
		'
		Me.PanelCountries.Controls.Add(Me.Label4)
		Me.PanelCountries.Controls.Add(Me.Label5)
		Me.PanelCountries.Controls.Add(Me.CountryColor)
		Me.PanelCountries.Controls.Add(Me.CountryTextBox)
		Me.PanelCountries.Controls.Add(Me.CountryButtonDelete)
		Me.PanelCountries.Controls.Add(Me.CountryButtonAdd)
		Me.PanelCountries.Controls.Add(Me.CountryListBox)
		Me.PanelCountries.Location = New System.Drawing.Point(1, 24)
		Me.PanelCountries.Name = "PanelCountries"
		Me.PanelCountries.Size = New System.Drawing.Size(392, 256)
		Me.PanelCountries.TabIndex = 5
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(192, 208)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(170, 26)
		Me.Label4.TabIndex = 8
		Me.Label4.Text = "Shift+Drag to enable snapping" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Shift+Ctrl+Drag to snap to all areas"
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Location = New System.Drawing.Point(8, 208)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(173, 39)
		Me.Label5.TabIndex = 7
		Me.Label5.Text = "Ctrl+Click to add a new area" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Delete to remove the selected area" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "RightClick to r" &
	"elocate country label" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
		'
		'CountryColor
		'
		Me.CountryColor.Location = New System.Drawing.Point(184, 8)
		Me.CountryColor.Name = "CountryColor"
		Me.CountryColor.Size = New System.Drawing.Size(104, 40)
		Me.CountryColor.TabIndex = 4
		Me.CountryColor.TabStop = False
		'
		'CountryTextBox
		'
		Me.CountryTextBox.Location = New System.Drawing.Point(8, 128)
		Me.CountryTextBox.Name = "CountryTextBox"
		Me.CountryTextBox.Size = New System.Drawing.Size(168, 20)
		Me.CountryTextBox.TabIndex = 3
		'
		'CountryButtonDelete
		'
		Me.CountryButtonDelete.Location = New System.Drawing.Point(88, 160)
		Me.CountryButtonDelete.Name = "CountryButtonDelete"
		Me.CountryButtonDelete.Size = New System.Drawing.Size(48, 24)
		Me.CountryButtonDelete.TabIndex = 2
		Me.CountryButtonDelete.Text = "Delete"
		Me.CountryButtonDelete.UseVisualStyleBackColor = True
		'
		'CountryButtonAdd
		'
		Me.CountryButtonAdd.Location = New System.Drawing.Point(8, 160)
		Me.CountryButtonAdd.Name = "CountryButtonAdd"
		Me.CountryButtonAdd.Size = New System.Drawing.Size(48, 24)
		Me.CountryButtonAdd.TabIndex = 1
		Me.CountryButtonAdd.Text = "Add"
		Me.CountryButtonAdd.UseVisualStyleBackColor = True
		'
		'CountryListBox
		'
		Me.CountryListBox.FormattingEnabled = True
		Me.CountryListBox.Location = New System.Drawing.Point(8, 8)
		Me.CountryListBox.Name = "CountryListBox"
		Me.CountryListBox.Size = New System.Drawing.Size(168, 108)
		Me.CountryListBox.TabIndex = 0
		'
		'PanelBorders
		'
		Me.PanelBorders.BackColor = System.Drawing.SystemColors.Control
		Me.PanelBorders.Controls.Add(Me.Label8)
		Me.PanelBorders.Location = New System.Drawing.Point(1, 24)
		Me.PanelBorders.Name = "PanelBorders"
		Me.PanelBorders.Size = New System.Drawing.Size(392, 256)
		Me.PanelBorders.TabIndex = 6
		'
		'Label8
		'
		Me.Label8.AutoSize = True
		Me.Label8.Location = New System.Drawing.Point(8, 8)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(188, 65)
		Me.Label8.TabIndex = 0
		Me.Label8.Text = "Drag && Drop to move vertexes" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ctrl+Shift+Click to create a new border" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ctrl+Clic" &
	"k to attach new vertex" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ctrl+Click to split line segment" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Delete to remove verte" &
	"x" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
		'
		'FormControls
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(394, 282)
		Me.Controls.Add(Me.PanelBorders)
		Me.Controls.Add(Me.ComboBoxMode)
		Me.Controls.Add(Me.PanelEditPolygons)
		Me.Controls.Add(Me.PanelZones)
		Me.Controls.Add(Me.PanelCountries)
		Me.Controls.Add(Me.PanelAreas)
		Me.Controls.Add(Me.PanelTextures)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "FormControls"
		Me.Text = "Tools"
		Me.PanelEditPolygons.ResumeLayout(False)
		Me.PanelEditPolygons.PerformLayout()
		Me.PanelTextures.ResumeLayout(False)
		CType(Me.TextureBoxTemplate, System.ComponentModel.ISupportInitialize).EndInit()
		Me.PanelAreas.ResumeLayout(False)
		Me.PanelAreas.PerformLayout()
		CType(Me.AreaColor, System.ComponentModel.ISupportInitialize).EndInit()
		Me.PanelZones.ResumeLayout(False)
		Me.PanelZones.PerformLayout()
		CType(Me.ZonesColor, System.ComponentModel.ISupportInitialize).EndInit()
		Me.PanelCountries.ResumeLayout(False)
		Me.PanelCountries.PerformLayout()
		CType(Me.CountryColor, System.ComponentModel.ISupportInitialize).EndInit()
		Me.PanelBorders.ResumeLayout(False)
		Me.PanelBorders.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents ComboBoxMode As ComboBox
	Friend WithEvents PanelEditPolygons As Panel
	Friend WithEvents PanelTextures As Panel
	Friend WithEvents TextureBoxTemplate As PictureBox
	Friend WithEvents PanelAreas As Panel
	Friend WithEvents AreaButtonDelete As Button
	Friend WithEvents AreaButtonAdd As Button
	Friend WithEvents AreaListBox As ListBox
	Friend WithEvents AreaTextBox As TextBox
	Friend WithEvents AreaColor As PictureBox
	Friend WithEvents PanelZones As Panel
	Friend WithEvents ZonesButtonRemove As Button
	Friend WithEvents ZonesButtonAdd As Button
	Friend WithEvents ZonesColor As PictureBox
	Friend WithEvents ZonesListBox As ListBox
	Friend WithEvents ZonesRegionsListBox As ListBox
	Friend WithEvents Label1 As Label
	Friend WithEvents ButtonDelaunayOptimization As Button
	Friend WithEvents ButtonOptimizeAll As Button
	Friend WithEvents Label2 As Label
	Friend WithEvents Label3 As Label
	Friend WithEvents PanelCountries As Panel
	Friend WithEvents CountryColor As PictureBox
	Friend WithEvents CountryTextBox As TextBox
	Friend WithEvents CountryButtonDelete As Button
	Friend WithEvents CountryButtonAdd As Button
	Friend WithEvents CountryListBox As ListBox
	Friend WithEvents Label6 As Label
	Friend WithEvents Label7 As Label
	Friend WithEvents Label4 As Label
	Friend WithEvents Label5 As Label
	Friend WithEvents PanelBorders As Panel
	Friend WithEvents Label8 As Label
End Class
