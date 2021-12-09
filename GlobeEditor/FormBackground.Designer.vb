<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormBackground
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
		Me.TextBoxFilename = New System.Windows.Forms.TextBox()
		Me.ButtonLoad = New System.Windows.Forms.Button()
		Me.ButtonFitToMap = New System.Windows.Forms.Button()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.DestinationX = New System.Windows.Forms.NumericUpDown()
		Me.DestinationY = New System.Windows.Forms.NumericUpDown()
		Me.DestinationHeight = New System.Windows.Forms.NumericUpDown()
		Me.DestinationWidth = New System.Windows.Forms.NumericUpDown()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.CheckBoxOnTop = New System.Windows.Forms.CheckBox()
		Me.OpacityTrack = New System.Windows.Forms.TrackBar()
		Me.Label5 = New System.Windows.Forms.Label()
		CType(Me.DestinationX, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DestinationY, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DestinationHeight, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DestinationWidth, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.OpacityTrack, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'TextBoxFilename
		'
		Me.TextBoxFilename.Location = New System.Drawing.Point(8, 8)
		Me.TextBoxFilename.Name = "TextBoxFilename"
		Me.TextBoxFilename.ReadOnly = True
		Me.TextBoxFilename.Size = New System.Drawing.Size(192, 20)
		Me.TextBoxFilename.TabIndex = 0
		'
		'ButtonLoad
		'
		Me.ButtonLoad.Location = New System.Drawing.Point(208, 8)
		Me.ButtonLoad.Name = "ButtonLoad"
		Me.ButtonLoad.Size = New System.Drawing.Size(24, 23)
		Me.ButtonLoad.TabIndex = 1
		Me.ButtonLoad.Text = "..."
		Me.ButtonLoad.UseVisualStyleBackColor = True
		'
		'ButtonFitToMap
		'
		Me.ButtonFitToMap.Location = New System.Drawing.Point(8, 40)
		Me.ButtonFitToMap.Name = "ButtonFitToMap"
		Me.ButtonFitToMap.Size = New System.Drawing.Size(75, 23)
		Me.ButtonFitToMap.TabIndex = 2
		Me.ButtonFitToMap.Text = "Fit to map"
		Me.ButtonFitToMap.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(12, 79)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(14, 13)
		Me.Label1.TabIndex = 7
		Me.Label1.Text = "X"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(84, 79)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(14, 13)
		Me.Label2.TabIndex = 8
		Me.Label2.Text = "Y"
		'
		'DestinationX
		'
		Me.DestinationX.DecimalPlaces = 2
		Me.DestinationX.Location = New System.Drawing.Point(29, 75)
		Me.DestinationX.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
		Me.DestinationX.Minimum = New Decimal(New Integer() {3600, 0, 0, -2147483648})
		Me.DestinationX.Name = "DestinationX"
		Me.DestinationX.Size = New System.Drawing.Size(51, 20)
		Me.DestinationX.TabIndex = 11
		'
		'DestinationY
		'
		Me.DestinationY.DecimalPlaces = 2
		Me.DestinationY.Location = New System.Drawing.Point(102, 75)
		Me.DestinationY.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
		Me.DestinationY.Minimum = New Decimal(New Integer() {3600, 0, 0, -2147483648})
		Me.DestinationY.Name = "DestinationY"
		Me.DestinationY.Size = New System.Drawing.Size(51, 20)
		Me.DestinationY.TabIndex = 12
		'
		'DestinationHeight
		'
		Me.DestinationHeight.DecimalPlaces = 2
		Me.DestinationHeight.Location = New System.Drawing.Point(103, 104)
		Me.DestinationHeight.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
		Me.DestinationHeight.Minimum = New Decimal(New Integer() {3600, 0, 0, -2147483648})
		Me.DestinationHeight.Name = "DestinationHeight"
		Me.DestinationHeight.Size = New System.Drawing.Size(51, 20)
		Me.DestinationHeight.TabIndex = 16
		'
		'DestinationWidth
		'
		Me.DestinationWidth.DecimalPlaces = 2
		Me.DestinationWidth.Location = New System.Drawing.Point(30, 104)
		Me.DestinationWidth.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
		Me.DestinationWidth.Minimum = New Decimal(New Integer() {3600, 0, 0, -2147483648})
		Me.DestinationWidth.Name = "DestinationWidth"
		Me.DestinationWidth.Size = New System.Drawing.Size(51, 20)
		Me.DestinationWidth.TabIndex = 15
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(85, 108)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(15, 13)
		Me.Label3.TabIndex = 14
		Me.Label3.Text = "H"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(13, 108)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(18, 13)
		Me.Label4.TabIndex = 13
		Me.Label4.Text = "W"
		'
		'CheckBoxOnTop
		'
		Me.CheckBoxOnTop.AutoSize = True
		Me.CheckBoxOnTop.Location = New System.Drawing.Point(16, 136)
		Me.CheckBoxOnTop.Name = "CheckBoxOnTop"
		Me.CheckBoxOnTop.Size = New System.Drawing.Size(58, 17)
		Me.CheckBoxOnTop.TabIndex = 17
		Me.CheckBoxOnTop.Text = "On top"
		Me.CheckBoxOnTop.UseVisualStyleBackColor = True
		Me.CheckBoxOnTop.Visible = False
		'
		'OpacityTrack
		'
		Me.OpacityTrack.LargeChange = 10
		Me.OpacityTrack.Location = New System.Drawing.Point(8, 176)
		Me.OpacityTrack.Maximum = 255
		Me.OpacityTrack.Name = "OpacityTrack"
		Me.OpacityTrack.Size = New System.Drawing.Size(248, 34)
		Me.OpacityTrack.TabIndex = 18
		Me.OpacityTrack.TickFrequency = 15
		Me.OpacityTrack.Value = 255
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Location = New System.Drawing.Point(16, 160)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(43, 13)
		Me.Label5.TabIndex = 19
		Me.Label5.Text = "Opacity"
		'
		'FormBackground
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(264, 267)
		Me.Controls.Add(Me.Label5)
		Me.Controls.Add(Me.OpacityTrack)
		Me.Controls.Add(Me.CheckBoxOnTop)
		Me.Controls.Add(Me.DestinationHeight)
		Me.Controls.Add(Me.DestinationWidth)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.DestinationY)
		Me.Controls.Add(Me.DestinationX)
		Me.Controls.Add(Me.ButtonFitToMap)
		Me.Controls.Add(Me.ButtonLoad)
		Me.Controls.Add(Me.TextBoxFilename)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Label1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Name = "FormBackground"
		Me.Text = "FormBackground"
		CType(Me.DestinationX, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DestinationY, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DestinationHeight, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DestinationWidth, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.OpacityTrack, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents TextBoxFilename As TextBox
	Friend WithEvents ButtonLoad As Button
	Friend WithEvents ButtonFitToMap As Button
	Friend WithEvents Label1 As Label
	Friend WithEvents Label2 As Label
	Friend WithEvents DestinationX As NumericUpDown
	Friend WithEvents DestinationY As NumericUpDown
	Friend WithEvents DestinationHeight As NumericUpDown
	Friend WithEvents DestinationWidth As NumericUpDown
	Friend WithEvents Label3 As Label
	Friend WithEvents Label4 As Label
	Friend WithEvents CheckBoxOnTop As CheckBox
	Friend WithEvents OpacityTrack As TrackBar
	Friend WithEvents Label5 As Label
End Class
