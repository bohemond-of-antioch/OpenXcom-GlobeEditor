Public Class FormControls
	Private TextureBoxes As PictureBox()
	Private SelectedTextureBox As Integer

	Private Sub FormControls_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		ComboBoxMode.Items.Clear()
		For Each EnumItem In System.Enum.GetNames(GetType(EEditMode))
			If EnumItem <> "DelaunayOptimization" Then
				ComboBoxMode.Items.Add(EnumItem)
			End If
		Next EnumItem

		ComboBoxMode.SelectedIndex = 0

		LoadGlobe()
	End Sub
	Friend Sub InitializeTextures()
		For f = PanelTextures.Controls.Count - 1 To 0 Step -1
			If TypeOf PanelTextures.Controls(f) Is PictureBox Then
				Dim Temp = PanelTextures.Controls(f)
				PanelTextures.Controls.RemoveAt(f)
				Temp.Dispose()
			End If
		Next f
		For Each C In PanelTextures.Controls.OfType(Of PictureBox)
			PanelTextures.Controls.Remove(C)
			C.Dispose()
		Next C
		ReDim TextureBoxes(Hl.TextureColors.Count - 1)
		For f = 0 To UBound(TextureBoxes)
			TextureBoxes(f) = New PictureBox()
			TextureBoxes(f).BorderStyle = TextureBoxTemplate.BorderStyle
			TextureBoxes(f).Width = TextureBoxTemplate.Width
			TextureBoxes(f).Height = TextureBoxTemplate.Height
			TextureBoxes(f).BackColor = Hl.TextureColors(f)
			TextureBoxes(f).Visible = True
			TextureBoxes(f).Top = TextureBoxTemplate.Top + (f \ 5) * (TextureBoxTemplate.Height + 10)
			TextureBoxes(f).Left = TextureBoxTemplate.Left + (f Mod 5) * (TextureBoxTemplate.Width + 10)
			TextureBoxes(f).Tag = Trim(Str(f))
			AddHandler TextureBoxes(f).Click, AddressOf TextureBox_Click
			AddHandler TextureBoxes(f).Paint, AddressOf TextureBox_Paint
			PanelTextures.Controls.Add(TextureBoxes(f))
		Next f
	End Sub
	Friend Sub LoadGlobe()
		InitializeTextures()
		AreaListBox.Items.Clear()
		ZonesRegionsListBox.Items.Clear()
		If Not Globe.Regions Is Nothing Then
			For Each R In Globe.Regions
				AreaListBox.Items.Add(R.Key)
				ZonesRegionsListBox.Items.Add(R.Key)
			Next R
		End If
	End Sub

	Friend Function GetEditMode() As EEditMode
		Return System.Enum.Parse(GetType(EEditMode), CStr(ComboBoxMode.SelectedItem))
	End Function

	Friend Function GetSelectedTexture() As Integer
		Return SelectedTextureBox
	End Function

	Private Sub ComboBoxMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxMode.SelectedIndexChanged
		PanelTextures.Visible = False
		PanelEditPolygons.Visible = False
		PanelAreas.Visible = False
		PanelZones.Visible = False
		Select Case GetEditMode()
			Case EEditMode.Polygons
				PanelEditPolygons.Visible = True
			Case EEditMode.Textures
				PanelTextures.Visible = True
			Case EEditMode.Areas
				PanelAreas.Visible = True
			Case EEditMode.MissionZones
				PanelZones.Visible = True
		End Select
		GlobeView.EditModeChanged(GetEditMode())
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
		If SelectedTextureBox = Val(PaintedPictureBox.Tag) Then
			e.Graphics.DrawRectangle(Pens.Black, 0, 0, PaintedPictureBox.Width - 5, PaintedPictureBox.Height - 5)
			e.Graphics.DrawRectangle(Pens.Black, 1, 1, PaintedPictureBox.Width - 7, PaintedPictureBox.Height - 7)
		End If
	End Sub

	Private Sub AreaListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles AreaListBox.SelectedIndexChanged
		If AreaListBox.SelectedIndex = -1 Then Exit Sub
		AreaColor.BackColor = Hl.RectangleColors(AreaListBox.SelectedIndex)
	End Sub

	Private Sub ZonesRegionsListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ZonesRegionsListBox.SelectedIndexChanged
		If ZonesRegionsListBox.SelectedIndex = -1 Then
			ZonesListBox.Items.Clear()
		Else
			ZonesListBox.Items.Clear()
			For Z = 0 To Globe.Regions(CStr(ZonesRegionsListBox.SelectedItem)).MissionZones.Count - 1
				ZonesListBox.Items.Add(Z)
			Next Z
			GlobeView.EditModeChanged(GetEditMode())
		End If
	End Sub

	Private Sub ZonesListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ZonesListBox.SelectedIndexChanged
		If ZonesListBox.SelectedItem = -1 Then Exit Sub
		ZonesColor.BackColor = Hl.RectangleColors(ZonesListBox.SelectedIndex)
	End Sub

	Private Sub ZonesButtonAdd_Click(sender As Object, e As EventArgs) Handles ZonesButtonAdd.Click
		Globe.Regions(CStr(ZonesRegionsListBox.SelectedItem)).MissionZones.Add(New List(Of CGlobe.CMissionZone))
		ZonesListBox.Items.Add(Trim(Str(ZonesListBox.Items.Count)))
	End Sub
	Private Sub ZonesButtonRemove_Click(sender As Object, e As EventArgs) Handles ZonesButtonRemove.Click
		If ZonesListBox.SelectedIndex = -1 Then Exit Sub
		Globe.Regions(CStr(ZonesRegionsListBox.SelectedItem)).MissionZones.RemoveAt(ZonesListBox.SelectedIndex)
		ZonesListBox.Items.Clear()
		For Z = 0 To Globe.Regions(CStr(ZonesRegionsListBox.SelectedItem)).MissionZones.Count - 1
			ZonesListBox.Items.Add(Z)
		Next Z
		GlobeView.Refresh()
	End Sub

	Private Sub AreaButtonAdd_Click(sender As Object, e As EventArgs) Handles AreaButtonAdd.Click
		Globe.Regions.Add(AreaTextBox.Text, New CGlobe.CRegion())
		AreaListBox.Items.Add(AreaTextBox.Text)
		AreaTextBox.Text = ""
		LoadGlobe()
	End Sub
	Private Sub AreaButtonDelete_Click(sender As Object, e As EventArgs) Handles AreaButtonDelete.Click
		If AreaListBox.SelectedIndex = -1 Then Exit Sub
		If MsgBox("Are you sure you want to delete the region " + AreaListBox.SelectedItem + "?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
			Globe.Regions.Remove(AreaListBox.Items(AreaListBox.SelectedIndex))
			AreaListBox.Items.RemoveAt(AreaListBox.SelectedIndex)
			LoadGlobe()
			GlobeView.Refresh()
		End If
	End Sub

	Private Sub ButtonDelaunayOptimization_Click(sender As Object, e As EventArgs) Handles ButtonDelaunayOptimization.Click
		If GlobeView.IsDelaunayOptimization() Then
			GlobeView.EndDelaunayOptimization()
		Else
			GlobeView.StartDelaunayOptimization()
			ButtonOptimizeAll.Visible = True
			ButtonDelaunayOptimization.Tag = ButtonDelaunayOptimization.Text
			ButtonDelaunayOptimization.Text = "Done"
			ComboBoxMode.Enabled = False
		End If
	End Sub

	Private Sub ButtonOptimizeAll_Click(sender As Object, e As EventArgs) Handles ButtonOptimizeAll.Click
		GlobeView.EndDelaunayOptimization()
		Globe.OptimizeEverything()
		Hl.ChangesSaved = False
		GlobeView.Refresh()
	End Sub

	Public Sub EndDelaunayOptimization()
		ButtonOptimizeAll.Visible = False
		ComboBoxMode.Enabled = True
		ButtonDelaunayOptimization.Text = ButtonDelaunayOptimization.Tag
	End Sub
End Class