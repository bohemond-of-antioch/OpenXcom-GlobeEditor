Public Class FormTextures
	Private TextureBoxes As PictureBox()
	Private SelectedTextureBox As Integer

	Private Sub FormTextures_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		InitializeTextures()
	End Sub

	Private Sub InitializeTextures()
		If Not TextureBoxes Is Nothing Then
			For Each C In TextureBoxes
				Controls.Remove(C)
				C.Dispose()
			Next C
		End If
		ReDim TextureBoxes(Hl.Project.Textures.Count - 1)
		For f = 0 To UBound(TextureBoxes)
			TextureBoxes(f) = New PictureBox()
			TextureBoxes(f).BorderStyle = TextureBoxTemplate.BorderStyle
			TextureBoxes(f).Width = TextureBoxTemplate.Width
			TextureBoxes(f).Height = TextureBoxTemplate.Height
			TextureBoxes(f).BackColor = Hl.Project.Textures(f)
			TextureBoxes(f).Visible = True
			TextureBoxes(f).Top = TextureBoxTemplate.Top + (f \ 5) * (TextureBoxTemplate.Height + 10)
			TextureBoxes(f).Left = TextureBoxTemplate.Left + (f Mod 5) * (TextureBoxTemplate.Width + 10)
			TextureBoxes(f).Tag = Trim(Str(f))
			AddHandler TextureBoxes(f).Click, AddressOf TextureBox_Click
			AddHandler TextureBoxes(f).Paint, AddressOf TextureBox_Paint
			Me.Controls.Add(TextureBoxes(f))
		Next f
	End Sub

	Private Sub TextureBox_Click(sender As Object, e As EventArgs)
		Dim ClickedPictureBox As PictureBox = sender
		Dim PreviousSelectedTextureBox = SelectedTextureBox
		SelectedTextureBox = Val(ClickedPictureBox.Tag)
		TextureBoxes(PreviousSelectedTextureBox).Refresh()
		TextureBoxes(SelectedTextureBox).Refresh()
	End Sub

	Private Sub TextureBox_Paint(sender As Object, e As PaintEventArgs)
		Dim PaintedPictureBox As PictureBox = sender
		Dim TextBrush As Brush
		If PaintedPictureBox.BackColor.GetBrightness() > 0.3 Then
			TextBrush = Brushes.Black
		Else
			TextBrush = Brushes.White
		End If
		e.Graphics.DrawString(PaintedPictureBox.Tag, Me.Font, TextBrush, New PointF(3, 3))
		If SelectedTextureBox = Val(PaintedPictureBox.Tag) Then
			e.Graphics.DrawRectangle(Pens.Black, 0, 0, PaintedPictureBox.Width - 5, PaintedPictureBox.Height - 5)
			e.Graphics.DrawRectangle(Pens.Black, 1, 1, PaintedPictureBox.Width - 7, PaintedPictureBox.Height - 7)
		End If
	End Sub

	Private Sub ButtonSetColor_Click(sender As Object, e As EventArgs) Handles ButtonSetColor.Click
		Dim dialog = New ColorDialog()
		If dialog.ShowDialog(Me) = DialogResult.OK Then
			Project.Textures(SelectedTextureBox) = dialog.Color
			Project.ApplyTexturesToGlobe()
			TextureBoxes(SelectedTextureBox).BackColor = dialog.Color
			Hl.ChangesSaved = False
			GlobeView.Refresh()
		End If
	End Sub

	Private Sub ButtonAddTexture_Click(sender As Object, e As EventArgs) Handles ButtonAddTexture.Click
		Dim RNG As New Random
		Project.Textures.Add(Color.FromArgb(RNG.Next(0, 255), RNG.Next(0, 255), RNG.Next(0, 255)))
		Hl.ChangesSaved = False
		Project.ApplyTexturesToGlobe()
		InitializeTextures()
	End Sub

	Private Sub ButtonRemoveTexture_Click(sender As Object, e As EventArgs) Handles ButtonRemoveTexture.Click
		If MsgBox("Are you sure you want to delete a texture?" + vbCrLf + "This will re-number the polygon textures and could mess something up. Don't proceed unless you know what you're doing.", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation) = MsgBoxResult.Yes Then
			Project.Textures.RemoveAt(SelectedTextureBox)
			Project.ApplyTexturesToGlobe()
			Globe.RemoveTexture(SelectedTextureBox)
			InitializeTextures()
			Hl.ChangesSaved = False
			GlobeView.Refresh()
		End If
	End Sub
End Class