<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormProjectSettings
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
		Me.GridLongitudalStep = New System.Windows.Forms.TextBox()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.GridLatitudalStep = New System.Windows.Forms.TextBox()
		Me.GridInitiallyShown = New System.Windows.Forms.CheckBox()
		Me.GroupBox1.SuspendLayout()
		Me.SuspendLayout()
		'
		'ButtonOK
		'
		Me.ButtonOK.Location = New System.Drawing.Point(232, 248)
		Me.ButtonOK.Name = "ButtonOK"
		Me.ButtonOK.Size = New System.Drawing.Size(59, 23)
		Me.ButtonOK.TabIndex = 0
		Me.ButtonOK.Text = "OK"
		Me.ButtonOK.UseVisualStyleBackColor = True
		'
		'ButtonCancel
		'
		Me.ButtonCancel.Location = New System.Drawing.Point(168, 248)
		Me.ButtonCancel.Name = "ButtonCancel"
		Me.ButtonCancel.Size = New System.Drawing.Size(59, 23)
		Me.ButtonCancel.TabIndex = 1
		Me.ButtonCancel.Text = "Cancel"
		Me.ButtonCancel.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(8, 20)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(99, 13)
		Me.Label1.TabIndex = 2
		Me.Label1.Text = "Longitudal Degrees"
		'
		'GridLongitudalStep
		'
		Me.GridLongitudalStep.Location = New System.Drawing.Point(112, 16)
		Me.GridLongitudalStep.Name = "GridLongitudalStep"
		Me.GridLongitudalStep.Size = New System.Drawing.Size(76, 20)
		Me.GridLongitudalStep.TabIndex = 3
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.GridInitiallyShown)
		Me.GroupBox1.Controls.Add(Me.Label2)
		Me.GroupBox1.Controls.Add(Me.GridLatitudalStep)
		Me.GroupBox1.Controls.Add(Me.Label1)
		Me.GroupBox1.Controls.Add(Me.GridLongitudalStep)
		Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(200, 100)
		Me.GroupBox1.TabIndex = 4
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Grid"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(8, 44)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(90, 13)
		Me.Label2.TabIndex = 4
		Me.Label2.Text = "Latitudal Degrees"
		'
		'GridLatitudalStep
		'
		Me.GridLatitudalStep.Location = New System.Drawing.Point(112, 40)
		Me.GridLatitudalStep.Name = "GridLatitudalStep"
		Me.GridLatitudalStep.Size = New System.Drawing.Size(76, 20)
		Me.GridLatitudalStep.TabIndex = 5
		'
		'GridInitiallyShown
		'
		Me.GridInitiallyShown.AutoSize = True
		Me.GridInitiallyShown.Location = New System.Drawing.Point(8, 64)
		Me.GridInitiallyShown.Name = "GridInitiallyShown"
		Me.GridInitiallyShown.Size = New System.Drawing.Size(93, 17)
		Me.GridInitiallyShown.TabIndex = 6
		Me.GridInitiallyShown.Text = "Initially Shown"
		Me.GridInitiallyShown.UseVisualStyleBackColor = True
		'
		'FormProjectSettings
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(292, 273)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.ButtonCancel)
		Me.Controls.Add(Me.ButtonOK)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Name = "FormProjectSettings"
		Me.Text = "Settings"
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents ButtonOK As Button
	Friend WithEvents ButtonCancel As Button
	Friend WithEvents Label1 As Label
	Friend WithEvents GridLongitudalStep As TextBox
	Friend WithEvents GroupBox1 As GroupBox
	Friend WithEvents GridInitiallyShown As CheckBox
	Friend WithEvents Label2 As Label
	Friend WithEvents GridLatitudalStep As TextBox
End Class
