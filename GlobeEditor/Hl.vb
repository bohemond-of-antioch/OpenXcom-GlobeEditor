Module Hl
	Friend Enum EEditMode
		Polygons
		Textures
		Areas
		MissionZones
		Borders
		Cities
		Countries
	End Enum

	Public ChangesSaved As Boolean
	Public Globe As CGlobe
	Public CurrentFileName As String
	Public TextureColors As Color()
	Public TextureBrushes As Brush()

	Public RectangleColors As Color()

	Dim GlobeRules As YamlNode

	Public Sub Initialize()
		TextureColors = New Color() {Color.DarkGreen, Color.ForestGreen, Color.LawnGreen, Color.Green, Color.LawnGreen, Color.DarkGray, Color.PaleGreen, Color.Yellow, Color.FromArgb(181, 131, 59), Color.Snow, Color.DarkGreen, Color.DarkGreen, Color.LightSkyBlue}
		RectangleColors = New Color() {
			Color.Blue, Color.Red, Color.Yellow, Color.Magenta, Color.Cyan,
			Color.Orange, Color.Salmon, Color.White, Color.Green, Color.Gold,
			Color.Moccasin, Color.SteelBlue, Color.YellowGreen, Color.Pink, Color.MintCream}
		ReDim TextureBrushes(UBound(TextureColors))
		For f = 0 To UBound(TextureColors)
			TextureBrushes(f) = New SolidBrush(TextureColors(f))
		Next f
		GlobeRules = New YamlNode(YamlNode.EType.Mapping)
		GlobeRules.SetMapping("globe", New YamlNode(YamlNode.EType.Mapping))
		Globe = New CGlobe(GlobeRules)
		CurrentFileName = ""
		ChangesSaved = True
		FormControls.Show(GlobeView)
	End Sub


	Friend Sub OpenGlobeFile(FileName As String)
		GlobeRules = New YamlFileParser(FileName).Parse()
		CurrentFileName = FileName
		ChangesSaved = True
		Globe = New CGlobe(GlobeRules)
		FormControls.LoadGlobe()
	End Sub

	Friend Sub SaveGlobeFile(FileName As String)
		Globe.ApplyToYaml(GlobeRules)
		Dim Writer = New YamlFileWriter(GlobeRules, FileName)
		Writer.Write()
		ChangesSaved = True
	End Sub

End Module
