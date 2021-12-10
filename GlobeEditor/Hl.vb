Module Hl
	Friend Enum EEditMode
		Polygons
		Textures
		Areas
		MissionZones
		Borders
		Countries
		DelaunayOptimization
	End Enum

	Public ChangesSaved As Boolean
	Public Globe As CGlobe
	Public Project As CProject
	Public CurrentFileName As String
	Public DefaultTextureColors As Color()
	Public TextureColors As Color()
	Public TextureBrushes As Brush()

	Public RectangleColors As Color()

	Dim GlobeRules As YamlNode
	Private Sub InitializeTextures()
		Dim RNG As New Random
		ReDim TextureColors(Math.Max(Globe.MaxTextureIndex, UBound(DefaultTextureColors)))
		Array.Copy(DefaultTextureColors, TextureColors, DefaultTextureColors.Count)
		For f = DefaultTextureColors.Count To Globe.MaxTextureIndex
			TextureColors(f) = Color.FromArgb(RNG.Next(0, 255), RNG.Next(0, 255), RNG.Next(0, 255))
		Next f
		ReDim TextureBrushes(UBound(TextureColors))
		For f = 0 To UBound(TextureColors)
			TextureBrushes(f) = New SolidBrush(TextureColors(f))
		Next f
	End Sub
	Public Sub Initialize()
		DefaultTextureColors = New Color() {Color.DarkGreen, Color.ForestGreen, Color.LawnGreen, Color.Green, Color.LawnGreen, Color.DarkGray, Color.PaleGreen, Color.Yellow, Color.FromArgb(181, 131, 59), Color.Snow, Color.DarkGreen, Color.DarkGreen, Color.LightSkyBlue}
		RectangleColors = New Color() {
			Color.Blue, Color.Red, Color.Yellow, Color.Magenta, Color.Cyan,
			Color.Orange, Color.Salmon, Color.White, Color.Green, Color.Gold,
			Color.Moccasin, Color.SteelBlue, Color.YellowGreen, Color.Pink, Color.MintCream}
		GlobeRules = New YamlNode(YamlNode.EType.Mapping)
		GlobeRules.SetMapping("globe", New YamlNode(YamlNode.EType.Mapping))
		Globe = New CGlobe(GlobeRules)
		InitializeTextures()
		CurrentFileName = ""
		ChangesSaved = True
		Project = CProject.CreateFromLoadedGlobe()
		FormControls.Show(GlobeView)
	End Sub
	Friend Function GetTexture(Index As Integer) As Brush
		If Index >= TextureColors.Count Then
			Dim RNG As New Random
			Dim OldCount = TextureColors.Count
			ReDim Preserve TextureColors(Index)
			ReDim Preserve TextureBrushes(Index)
			For f = OldCount To Index
				TextureColors(f) = Color.FromArgb(RNG.Next(0, 255), RNG.Next(0, 255), RNG.Next(0, 255))
				TextureBrushes(f) = New SolidBrush(TextureColors(f))
			Next f
			FormControls.InitializeTextures()
		End If
		Return TextureBrushes(Index)
	End Function

	Friend Sub OpenGlobeFile(FileName As String)
		GlobeRules = New YamlFileParser(FileName).Parse()
		CurrentFileName = FileName
		ChangesSaved = True
		Globe = New CGlobe(GlobeRules)
		InitializeTextures()
		FormControls.LoadGlobe()
	End Sub

	Friend Sub SaveGlobeFile(FileName As String)
		Globe.ApplyToYaml(GlobeRules)
		Dim Writer = New YamlFileWriter(GlobeRules, FileName)
		Writer.Write()
		ChangesSaved = True
	End Sub

End Module
