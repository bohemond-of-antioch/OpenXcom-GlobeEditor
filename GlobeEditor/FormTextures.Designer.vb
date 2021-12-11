<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormTextures
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormTextures))
		Me.TextureBoxTemplate = New System.Windows.Forms.PictureBox()
		Me.ButtonAddTexture = New System.Windows.Forms.Button()
		Me.ButtonRemoveTexture = New System.Windows.Forms.Button()
		Me.ButtonSetColor = New System.Windows.Forms.Button()
		CType(Me.TextureBoxTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'TextureBoxTemplate
		'
		Me.TextureBoxTemplate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.TextureBoxTemplate.Location = New System.Drawing.Point(8, 8)
		Me.TextureBoxTemplate.Name = "TextureBoxTemplate"
		Me.TextureBoxTemplate.Size = New System.Drawing.Size(32, 32)
		Me.TextureBoxTemplate.TabIndex = 1
		Me.TextureBoxTemplate.TabStop = False
		Me.TextureBoxTemplate.Visible = False
		'
		'ButtonAddTexture
		'
		Me.ButtonAddTexture.Location = New System.Drawing.Point(216, 8)
		Me.ButtonAddTexture.Name = "ButtonAddTexture"
		Me.ButtonAddTexture.Size = New System.Drawing.Size(56, 23)
		Me.ButtonAddTexture.TabIndex = 2
		Me.ButtonAddTexture.Text = "Add"
		Me.ButtonAddTexture.UseVisualStyleBackColor = True
		'
		'ButtonRemoveTexture
		'
		Me.ButtonRemoveTexture.Location = New System.Drawing.Point(280, 8)
		Me.ButtonRemoveTexture.Name = "ButtonRemoveTexture"
		Me.ButtonRemoveTexture.Size = New System.Drawing.Size(56, 23)
		Me.ButtonRemoveTexture.TabIndex = 3
		Me.ButtonRemoveTexture.Text = "Remove"
		Me.ButtonRemoveTexture.UseVisualStyleBackColor = True
		'
		'ButtonSetColor
		'
		Me.ButtonSetColor.Location = New System.Drawing.Point(216, 56)
		Me.ButtonSetColor.Name = "ButtonSetColor"
		Me.ButtonSetColor.Size = New System.Drawing.Size(120, 23)
		Me.ButtonSetColor.TabIndex = 4
		Me.ButtonSetColor.Text = "Set Color"
		Me.ButtonSetColor.UseVisualStyleBackColor = True
		'
		'FormTextures
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(341, 273)
		Me.Controls.Add(Me.ButtonSetColor)
		Me.Controls.Add(Me.ButtonRemoveTexture)
		Me.Controls.Add(Me.ButtonAddTexture)
		Me.Controls.Add(Me.TextureBoxTemplate)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.Name = "FormTextures"
		Me.Text = "Project Textures"
		CType(Me.TextureBoxTemplate, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents TextureBoxTemplate As PictureBox
	Friend WithEvents ButtonAddTexture As Button
	Friend WithEvents ButtonRemoveTexture As Button
	Friend WithEvents ButtonSetColor As Button
End Class
