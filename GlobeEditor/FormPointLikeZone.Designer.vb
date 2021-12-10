<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormPointLikeZone
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
		Me.ButtonOK = New System.Windows.Forms.Button()
		Me.ButtonCancel = New System.Windows.Forms.Button()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.TextBoxTexture = New System.Windows.Forms.TextBox()
		Me.TextBoxName = New System.Windows.Forms.TextBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.SuspendLayout()
		'
		'ButtonOK
		'
		Me.ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.ButtonOK.Location = New System.Drawing.Point(128, 56)
		Me.ButtonOK.Name = "ButtonOK"
		Me.ButtonOK.Size = New System.Drawing.Size(48, 23)
		Me.ButtonOK.TabIndex = 0
		Me.ButtonOK.Text = "OK"
		Me.ButtonOK.UseVisualStyleBackColor = True
		'
		'ButtonCancel
		'
		Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.ButtonCancel.Location = New System.Drawing.Point(8, 56)
		Me.ButtonCancel.Name = "ButtonCancel"
		Me.ButtonCancel.Size = New System.Drawing.Size(48, 23)
		Me.ButtonCancel.TabIndex = 1
		Me.ButtonCancel.Text = "Cancel"
		Me.ButtonCancel.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(8, 12)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(43, 13)
		Me.Label1.TabIndex = 2
		Me.Label1.Text = "Texture"
		'
		'TextBoxTexture
		'
		Me.TextBoxTexture.Location = New System.Drawing.Point(56, 8)
		Me.TextBoxTexture.Name = "TextBoxTexture"
		Me.TextBoxTexture.Size = New System.Drawing.Size(40, 20)
		Me.TextBoxTexture.TabIndex = 3
		'
		'TextBoxName
		'
		Me.TextBoxName.Location = New System.Drawing.Point(56, 32)
		Me.TextBoxName.Name = "TextBoxName"
		Me.TextBoxName.Size = New System.Drawing.Size(120, 20)
		Me.TextBoxName.TabIndex = 5
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(8, 36)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(35, 13)
		Me.Label2.TabIndex = 4
		Me.Label2.Text = "Name"
		'
		'FormPointLikeZone
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(181, 85)
		Me.Controls.Add(Me.TextBoxName)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.TextBoxTexture)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.ButtonCancel)
		Me.Controls.Add(Me.ButtonOK)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "FormPointLikeZone"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
		Me.Text = "FormPointLikeZone"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents ButtonOK As Button
	Friend WithEvents ButtonCancel As Button
	Friend WithEvents Label1 As Label
	Friend WithEvents TextBoxTexture As TextBox
	Friend WithEvents TextBoxName As TextBox
	Friend WithEvents Label2 As Label
End Class
