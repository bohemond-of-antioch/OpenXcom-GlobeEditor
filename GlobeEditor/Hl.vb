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

	Private RectangleColors As Color()

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
		Globe = New CGlobe("", GlobeRules)
		InitializeTextures()
		CurrentFileName = ""
		ChangesSaved = True
		Project = CProject.CreateFromLoadedGlobe()
		FormControls.Show(GlobeView)
	End Sub
	Friend Function GetRectangleColor(Index As Integer) As Color
		If Index >= RectangleColors.Count Then
			Dim RNG As New Random(Index)
			Dim OldCount = RectangleColors.Count
			ReDim Preserve RectangleColors(Index)
			For f = OldCount To Index
				RectangleColors(f) = Color.FromArgb(RNG.Next(0, 255), RNG.Next(0, 255), RNG.Next(0, 255))
			Next f
		End If
		Return RectangleColors(Index)
	End Function
	Friend Function GetTexture(Index As Integer) As Brush
		If Index >= TextureColors.Count Then
			Dim RNG As New Random(Index)
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
		CurrentFileName = FileName
		If IO.Path.GetExtension(FileName).ToLower = ".rul" Then
			GlobeRules = New YamlFileParser(FileName).Parse()
			ChangesSaved = True
			Try
				Globe = New CGlobe(FileName, GlobeRules)
			Catch e As Exception
				MsgBox(e.Message, MsgBoxStyle.Critical)
			End Try
		Else
			Globe = New CGlobe()
			Globe.LoadDat(FileName)
			ChangesSaved = False
		End If
		InitializeTextures()
		FormControls.LoadGlobe()
	End Sub

	Friend Sub SaveGlobeFile(FileName As String)
		If IO.Path.GetExtension(FileName).ToLower = ".rul" Then
			Dim ForceEmbedding As Boolean = IO.Path.GetExtension(CurrentFileName).ToLower = ".dat"
			If Globe.PolygonSourceData <> "" Then
				Try
					Globe.FindDat(FileName, Globe.PolygonSourceData)
				Catch ex As IO.FileNotFoundException
					Dim result = MsgBox("The " + Globe.PolygonSourceData + " dat file does not exist at the new location. Do you want to embed the polygon data in the .rul file?", MsgBoxStyle.YesNo Or MsgBoxStyle.Exclamation)
					If result = MsgBoxResult.Yes Then
						ForceEmbedding = True
					End If
				End Try
			End If
			Globe.ApplyToYaml(GlobeRules, ForceEmbedding)
			Dim Writer = New YamlFileWriter(GlobeRules, FileName)
			Writer.Write()
		Else
			Globe.SaveDat(FileName)
		End If
		CurrentFileName = FileName
		ChangesSaved = True
	End Sub

End Module
