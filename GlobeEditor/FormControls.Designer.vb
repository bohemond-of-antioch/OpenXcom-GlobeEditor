﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
		Me.ComboBoxMode = New System.Windows.Forms.ComboBox()
		Me.PanelEditPolygons = New System.Windows.Forms.Panel()
		Me.PanelTextures = New System.Windows.Forms.Panel()
		Me.TextureBoxTemplate = New System.Windows.Forms.PictureBox()
		Me.PanelAreas = New System.Windows.Forms.Panel()
		Me.AreaColor = New System.Windows.Forms.PictureBox()
		Me.AreaTextBox = New System.Windows.Forms.TextBox()
		Me.AreaButtonDelete = New System.Windows.Forms.Button()
		Me.AreaButtonAdd = New System.Windows.Forms.Button()
		Me.AreaListBox = New System.Windows.Forms.ListBox()
		Me.PanelZones = New System.Windows.Forms.Panel()
		Me.ZonesButtonRemove = New System.Windows.Forms.Button()
		Me.ZonesButtonAdd = New System.Windows.Forms.Button()
		Me.ZonesColor = New System.Windows.Forms.PictureBox()
		Me.ZonesListBox = New System.Windows.Forms.ListBox()
		Me.ZonesRegionsListBox = New System.Windows.Forms.ListBox()
		Me.PanelTextures.SuspendLayout()
		CType(Me.TextureBoxTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.PanelAreas.SuspendLayout()
		CType(Me.AreaColor, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.PanelZones.SuspendLayout()
		CType(Me.ZonesColor, System.ComponentModel.ISupportInitialize).BeginInit()
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
		Me.PanelEditPolygons.Location = New System.Drawing.Point(0, 0)
		Me.PanelEditPolygons.Name = "PanelEditPolygons"
		Me.PanelEditPolygons.Size = New System.Drawing.Size(240, 192)
		Me.PanelEditPolygons.TabIndex = 1
		'
		'PanelTextures
		'
		Me.PanelTextures.Controls.Add(Me.TextureBoxTemplate)
		Me.PanelTextures.Controls.Add(Me.PanelEditPolygons)
		Me.PanelTextures.Location = New System.Drawing.Point(0, 24)
		Me.PanelTextures.Name = "PanelTextures"
		Me.PanelTextures.Size = New System.Drawing.Size(240, 192)
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
		Me.PanelAreas.Controls.Add(Me.AreaColor)
		Me.PanelAreas.Controls.Add(Me.AreaTextBox)
		Me.PanelAreas.Controls.Add(Me.AreaButtonDelete)
		Me.PanelAreas.Controls.Add(Me.AreaButtonAdd)
		Me.PanelAreas.Controls.Add(Me.AreaListBox)
		Me.PanelAreas.Location = New System.Drawing.Point(0, 24)
		Me.PanelAreas.Name = "PanelAreas"
		Me.PanelAreas.Size = New System.Drawing.Size(240, 192)
		Me.PanelAreas.TabIndex = 3
		'
		'AreaColor
		'
		Me.AreaColor.Location = New System.Drawing.Point(144, 8)
		Me.AreaColor.Name = "AreaColor"
		Me.AreaColor.Size = New System.Drawing.Size(88, 40)
		Me.AreaColor.TabIndex = 4
		Me.AreaColor.TabStop = False
		'
		'AreaTextBox
		'
		Me.AreaTextBox.Location = New System.Drawing.Point(8, 128)
		Me.AreaTextBox.Name = "AreaTextBox"
		Me.AreaTextBox.Size = New System.Drawing.Size(128, 20)
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
		Me.AreaListBox.Size = New System.Drawing.Size(128, 108)
		Me.AreaListBox.TabIndex = 0
		'
		'PanelZones
		'
		Me.PanelZones.Controls.Add(Me.ZonesButtonRemove)
		Me.PanelZones.Controls.Add(Me.ZonesButtonAdd)
		Me.PanelZones.Controls.Add(Me.ZonesColor)
		Me.PanelZones.Controls.Add(Me.ZonesListBox)
		Me.PanelZones.Controls.Add(Me.ZonesRegionsListBox)
		Me.PanelZones.Location = New System.Drawing.Point(0, 24)
		Me.PanelZones.Name = "PanelZones"
		Me.PanelZones.Size = New System.Drawing.Size(240, 192)
		Me.PanelZones.TabIndex = 4
		'
		'ZonesButtonRemove
		'
		Me.ZonesButtonRemove.Location = New System.Drawing.Point(152, 120)
		Me.ZonesButtonRemove.Name = "ZonesButtonRemove"
		Me.ZonesButtonRemove.Size = New System.Drawing.Size(56, 24)
		Me.ZonesButtonRemove.TabIndex = 4
		Me.ZonesButtonRemove.Text = "Remove"
		Me.ZonesButtonRemove.UseVisualStyleBackColor = True
		'
		'ZonesButtonAdd
		'
		Me.ZonesButtonAdd.Location = New System.Drawing.Point(112, 120)
		Me.ZonesButtonAdd.Name = "ZonesButtonAdd"
		Me.ZonesButtonAdd.Size = New System.Drawing.Size(40, 24)
		Me.ZonesButtonAdd.TabIndex = 3
		Me.ZonesButtonAdd.Text = "Add"
		Me.ZonesButtonAdd.UseVisualStyleBackColor = True
		'
		'ZonesColor
		'
		Me.ZonesColor.Location = New System.Drawing.Point(216, 8)
		Me.ZonesColor.Name = "ZonesColor"
		Me.ZonesColor.Size = New System.Drawing.Size(16, 104)
		Me.ZonesColor.TabIndex = 2
		Me.ZonesColor.TabStop = False
		'
		'ZonesListBox
		'
		Me.ZonesListBox.FormattingEnabled = True
		Me.ZonesListBox.Location = New System.Drawing.Point(112, 8)
		Me.ZonesListBox.Name = "ZonesListBox"
		Me.ZonesListBox.Size = New System.Drawing.Size(96, 108)
		Me.ZonesListBox.TabIndex = 1
		'
		'ZonesRegionsListBox
		'
		Me.ZonesRegionsListBox.FormattingEnabled = True
		Me.ZonesRegionsListBox.Location = New System.Drawing.Point(8, 8)
		Me.ZonesRegionsListBox.Name = "ZonesRegionsListBox"
		Me.ZonesRegionsListBox.Size = New System.Drawing.Size(96, 108)
		Me.ZonesRegionsListBox.TabIndex = 0
		'
		'FormControls
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(244, 219)
		Me.Controls.Add(Me.PanelZones)
		Me.Controls.Add(Me.PanelAreas)
		Me.Controls.Add(Me.PanelTextures)
		Me.Controls.Add(Me.ComboBoxMode)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Name = "FormControls"
		Me.Text = "Tools"
		Me.PanelTextures.ResumeLayout(False)
		CType(Me.TextureBoxTemplate, System.ComponentModel.ISupportInitialize).EndInit()
		Me.PanelAreas.ResumeLayout(False)
		Me.PanelAreas.PerformLayout()
		CType(Me.AreaColor, System.ComponentModel.ISupportInitialize).EndInit()
		Me.PanelZones.ResumeLayout(False)
		CType(Me.ZonesColor, System.ComponentModel.ISupportInitialize).EndInit()
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
End Class