Public Class YamlFileWriter
	Private FileName As String
	Private FileWriter As System.IO.StreamWriter
	Private RootNode As YamlNode

	Public Sub New(RootNode As YamlNode, FileName As String)
		Me.FileName = FileName
		Me.RootNode = RootNode
	End Sub

	Public Sub Write()
		FileWriter = New System.IO.StreamWriter(FileName)
		WriteNode(RootNode, 0)
		FileWriter.Close()
	End Sub

	Private Function CreateIndent(Value As Integer)
		Return StrDup(Value, " ")
	End Function

	Private Sub WriteLine(Value As String, Indent As Integer, Collapsed As Boolean)
		FileWriter.WriteLine(If(Collapsed, "", StrDup(Indent, " ")) + Value)
	End Sub
	Private Sub Write(Value As String, Indent As Integer, Collapsed As Boolean)
		FileWriter.Write(If(Collapsed, "", StrDup(Indent, " ")) + Value)
	End Sub

	Private Sub WriteNode(Node As YamlNode, Indent As Integer, Optional Collapsed As Boolean = False)
		Select Case Node.Type
			Case YamlNode.EType.Value
				WriteLine(Node.GetValue(), Indent, Collapsed)
			Case YamlNode.EType.Mapping
				For Each Key In Node.GetMappingKeys()
					If (Node.GetMapping(Key).Type = YamlNode.EType.Value) Then
						WriteLine(Key + ": " + Node.GetMapping(Key).GetValue(), Indent, Collapsed)
					Else
						WriteLine(Key + ": ", Indent, Collapsed)
						WriteNode(Node.GetMapping(Key), Indent + 2, False)
					End If
					Collapsed = False
				Next Key
			Case YamlNode.EType.Sequence
				For f = 0 To Node.ItemCount - 1
					If (Node.GetItem(f).Type = YamlNode.EType.Value) Then
						WriteLine("- " + Node.GetItem(f).GetValue(), Indent, Collapsed)
					Else
						Write("- ", Indent, Collapsed)
						WriteNode(Node.GetItem(f), Indent + 2, True)
					End If
					Collapsed = False
				Next f
		End Select
	End Sub

End Class
