Public Class CProject
	Friend BackgroundFilename As String
	Friend BackgroundDestination As RectangleF
	Friend BackgroundOpacity As Integer

	Friend GlobeFilename As String

	Friend Textures As List(Of Color)

	Friend ProjectFilename As String = Nothing

	Private Sub UpdateFromGlobals()
		BackgroundFilename = GlobeView.Background.Filename
		BackgroundDestination = GlobeView.Background.Destination
		BackgroundOpacity = GlobeView.Background.Opacity
	End Sub

	Friend Sub ApplyToGlobals()
		FormBackground.Close()
		Hl.OpenGlobeFile(GlobeFilename)
		ApplyTexturesToGlobe()
		If BackgroundFilename <> "" Then
			GlobeView.LoadBackground(BackgroundFilename)
			GlobeView.Background.Destination = BackgroundDestination
			GlobeView.Background.Opacity = BackgroundOpacity
		End If
		Call FormControls.InitializeTextures()
	End Sub

	Friend Sub Save(Filename As String)
		UpdateFromGlobals()
		Dim Data = New YamlNode(YamlNode.EType.Mapping)
		Data.SetMapping("GlobeFilename", New YamlNode(GlobeFilename))
		If BackgroundFilename <> "" Then
			Data.SetMapping("BackgroundFilename", New YamlNode(BackgroundFilename))
			Dim YamlBackgroundDestination As YamlNode = New YamlNode(YamlNode.EType.Sequence)
			YamlBackgroundDestination.AddItem(New YamlNode(Trim(Str(BackgroundDestination.X))))
			YamlBackgroundDestination.AddItem(New YamlNode(Trim(Str(BackgroundDestination.Y))))
			YamlBackgroundDestination.AddItem(New YamlNode(Trim(Str(BackgroundDestination.Width))))
			YamlBackgroundDestination.AddItem(New YamlNode(Trim(Str(BackgroundDestination.Height))))
			Data.SetMapping("BackgroundDestination", YamlBackgroundDestination)
			Data.SetMapping("BackgroundOpacity", New YamlNode(Trim(Str(BackgroundOpacity))))
		End If
		Dim YamlTextures As YamlNode = New YamlNode(YamlNode.EType.Sequence)
		For Each C In Textures
			Dim YamlColor As YamlNode = New YamlNode(YamlNode.EType.Sequence)
			YamlColor.AddItem(New YamlNode(Trim(Str(C.R))))
			YamlColor.AddItem(New YamlNode(Trim(Str(C.G))))
			YamlColor.AddItem(New YamlNode(Trim(Str(C.B))))
			YamlTextures.AddItem(YamlColor)
		Next C
		Data.SetMapping("Textures", YamlTextures)
		Dim Writer = New YamlFileWriter(Data, Filename)
		Writer.Write()
	End Sub

	Friend Shared Function CreateFromFile(Filename As String) As CProject
		CreateFromFile = New CProject()
		Dim Data = New YamlFileParser(Filename).Parse()
		CreateFromFile.ProjectFilename = Filename
		ChangesSaved = True
		CreateFromFile.GlobeFilename = Data.GetMapping("GlobeFilename").GetValue()
		If Data.HasMapping("BackgroundFilename") Then
			CreateFromFile.BackgroundFilename = Data.GetMapping("BackgroundFilename").GetValue()
			Dim YamlBackgroundDestination = Data.GetMapping("BackgroundDestination")
			CreateFromFile.BackgroundDestination.X = Val(YamlBackgroundDestination.GetItem(0).GetValue())
			CreateFromFile.BackgroundDestination.Y = Val(YamlBackgroundDestination.GetItem(1).GetValue())
			CreateFromFile.BackgroundDestination.Width = Val(YamlBackgroundDestination.GetItem(2).GetValue())
			CreateFromFile.BackgroundDestination.Height = Val(YamlBackgroundDestination.GetItem(3).GetValue())
			CreateFromFile.BackgroundOpacity = Val(Data.GetMapping("BackgroundOpacity").GetValue())
		End If
		Dim YamlTextures = Data.GetMapping("Textures")
		CreateFromFile.Textures = New List(Of Color)
		For f = 0 To YamlTextures.ItemCount() - 1
			Dim YamlColor = YamlTextures.GetItem(f)
			Dim C As Color
			C = Color.FromArgb(Val(YamlColor.GetItem(0).GetValue()), Val(YamlColor.GetItem(1).GetValue()), Val(YamlColor.GetItem(2).GetValue()))
			CreateFromFile.Textures.Add(C)
		Next f
	End Function

	Friend Shared Function CreateFromLoadedGlobe() As CProject
		CreateFromLoadedGlobe = New CProject()
		CreateFromLoadedGlobe.GlobeFilename = Hl.CurrentFileName
		CreateFromLoadedGlobe.ProjectFilename = Nothing
		CreateFromLoadedGlobe.Textures = New List(Of Color)(Hl.TextureColors)
	End Function

	Friend Sub ApplyTexturesToGlobe()
		ReDim Hl.TextureColors(Textures.Count - 1)
		Array.Copy(Textures.ToArray(), TextureColors, Textures.Count)
		ReDim TextureBrushes(UBound(TextureColors))
		For f = 0 To UBound(TextureColors)
			TextureBrushes(f) = New SolidBrush(TextureColors(f))
		Next f
	End Sub

End Class
