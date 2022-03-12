Imports System.Drawing.Drawing2D
Imports GlobeEditor

Public Class GlobeView
	Private Enum EDragPhase
		None
		Started
		Moving
		MapScroll
	End Enum
	Private Structure SUI
		Friend ScrollX As Single
		Friend ScrollY As Single
		Friend Zoom As Single

		Friend KeyScrollX As Integer
		Friend KeyScrollY As Integer
		Friend KeyZoom As Integer

		Friend EditMode As EEditMode

		Friend DragPhase As EDragPhase
		Friend DragIndex As Integer

		Friend SelectedObject As Object
		Friend SelectionBoxOrigin As CVector
	End Structure
	Friend Structure SBackground
		Friend Filename As String
		Friend Image As Bitmap
		Friend Destination As RectangleF
		Friend OnTop As Boolean
		Friend Opacity As Integer
	End Structure
	Private UI As SUI
	Private MouseX As Integer
	Private MouseY As Integer
	Private LastMouseX As Integer
	Private LastMouseY As Integer
	Friend Background As SBackground

	Private Sub GlobeView_Load(sender As Object, e As EventArgs) Handles Me.Load
		Call Hl.Initialize()
		UI.Zoom = 3
		UI.ScrollX = 0
		UI.EditMode = EEditMode.Polygons
		Background.Opacity = 255
	End Sub

	Friend Sub EditModeChanged(NewEditMode As EEditMode)
		UI.EditMode = NewEditMode
		Me.Refresh()
	End Sub

	Friend Sub LoadBackground(Filename As String)
		Background.Filename = Filename
		Background.Image = New Bitmap(Filename)
		FitBackgroundImage()
		Me.Refresh()
	End Sub
	Friend Sub FitBackgroundImage()
		Background.Destination.X = 0
		Background.Destination.Y = -90
		Background.Destination.Width = 360
		Background.Destination.Height = 180
	End Sub

	Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
		If CheckUnsavedFile() Then
			Dim OpenFileDialog As New OpenFileDialog
			OpenFileDialog.Filter = "Rule Files (*.rul)|*.rul|All Files (*.*)|*.*"
			OpenFileDialog.Multiselect = False
			If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
				Dim FileName As String
				FileName = OpenFileDialog.FileName
				Call Hl.OpenGlobeFile(FileName)
				Hl.Project = CProject.CreateFromLoadedGlobe()
				Me.Refresh()
			End If
		End If
	End Sub
	Private Function CheckUnsavedFile() As Boolean
		If Not Hl.ChangesSaved Then
			Dim Result As MsgBoxResult
			Result = MsgBox("You have unsaved changes. Do you want to save the file before closing it?", MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Question, "Unsaved changes")
			If Result = MsgBoxResult.Yes Then
				SaveToolStripMenuItem_Click(Me, New EventArgs)
			ElseIf Result = MsgBoxResult.Cancel Then
				Return False
			End If
		End If
		Return True
	End Function
	Private Sub DrawBackground(G As Graphics)
		If Not Background.Image Is Nothing Then
			Dim SavedTransfrom = G.Transform
			G.ScaleTransform(UI.Zoom, UI.Zoom)
			G.TranslateTransform(UI.ScrollX, UI.ScrollY)
			Dim SecondDestination As RectangleF
			SecondDestination = Background.Destination
			G.DrawImage(Background.Image, Background.Destination)
			SecondDestination.X = Background.Destination.X - Background.Destination.Width
			G.DrawImage(Background.Image, SecondDestination)
			'SecondDestination.X = -720
			'G.DrawImage(Background.Image, SecondDestination)
			'SecondDestination.X = 360
			'G.DrawImage(Background.Image, SecondDestination)

			G.Transform = SavedTransfrom
		End If
	End Sub
	Private TextureIndexShiftVector As CVector = New CVector(4, 9)
	Private Sub DrawPolygons(G As Graphics)
		Dim ShowTextureIndexes As Boolean = ShowTextureIndexesToolStripMenuItem.Checked

		For Each Polygon In Hl.Globe.Polygons
			Dim IsPartial As Boolean = False
			Dim PolygonPoints = New List(Of PointF)
			Dim MiddleVertex As CVector = New CVector(0, 0)
			For v = 0 To Polygon.Vertices.Count - 1
				Dim V1 = New CVector(Polygon.Vertices(v))
				Dim V2 = New CVector(Polygon.Vertices((v + 1) Mod Polygon.Vertices.Count))
				V1.X += UI.ScrollX
				V2.X += UI.ScrollX
				V1.Y += UI.ScrollY
				V2.Y += UI.ScrollY
				If V1.X > 360 Then V1.X -= 360
				If V2.X > 360 Then V2.X -= 360
				Dim V3 = New CVector(V2.X - 360, V2.Y)
				Dim V4 = New CVector(V2.X + 360, V2.Y)
				V1.Scale(UI.Zoom)
				V2.Scale(UI.Zoom)
				V3.Scale(UI.Zoom)
				V4.Scale(UI.Zoom)

				If V1.DistanceTo(V2) > V1.DistanceTo(V3) Then
					G.DrawLine(Pens.Red, V1.AsPointF, V3.AsPointF)
					IsPartial = True
				ElseIf V1.DistanceTo(V2) > V1.DistanceTo(V4) Then
					G.DrawLine(Pens.Green, V1.AsPointF, V4.AsPointF)
					IsPartial = True
				Else
					If ShowTextureIndexes Then MiddleVertex = MiddleVertex + V1
					PolygonPoints.Add(V1.AsPointF)
					G.DrawLine(Pens.Black, V1.AsPointF, V2.AsPointF)
				End If
			Next v
			If Not IsPartial Then
				Dim PolygonBrush As Brush
				PolygonBrush = Hl.GetTexture(Polygon.Texture)
				If Background.Opacity < 255 Then
					Dim OpacityColor = Color.FromArgb(TextureColors(Polygon.Texture).ToArgb And 16777215 + (Background.Opacity << 24))
					PolygonBrush = New SolidBrush(OpacityColor)
				End If
				G.FillPolygon(PolygonBrush, PolygonPoints.ToArray(), FillMode.Alternate)
				For f = 0 To PolygonPoints.Count - 1
					G.DrawLine(Pens.Black, PolygonPoints(f), PolygonPoints((f + 1) Mod PolygonPoints.Count))
				Next f
				If ShowTextureIndexes Then
					MiddleVertex = MiddleVertex / PolygonPoints.Count - TextureIndexShiftVector
					Dim TextBrush As Brush
					If TextureColors(Polygon.Texture).GetBrightness() > 0.3 Then
						TextBrush = Brushes.Black
					Else
						TextBrush = Brushes.White
					End If
					G.DrawString(Polygon.Texture, Me.Font, TextBrush, MiddleVertex.AsPointF())

				End If
			End If
		Next Polygon
	End Sub
	Private Sub DrawBorders(G As Graphics)
		For Each PolyLine In Hl.Globe.PolyLines
			For v = 0 To PolyLine.Vertices.Count - 1
				Dim V1 = New CVector(PolyLine.Vertices(v))
				Dim V2 = New CVector(PolyLine.Vertices(Math.Min(PolyLine.Vertices.Count - 1, v + 1)))
				V1.X += UI.ScrollX
				V2.X += UI.ScrollX
				V1.Y += UI.ScrollY
				V2.Y += UI.ScrollY
				If V1.X > 360 Then V1.X -= 360
				If V2.X > 360 Then V2.X -= 360
				Dim V3 = New CVector(V2.X - 360, V2.Y)
				Dim V4 = New CVector(V2.X + 360, V2.Y)
				V1.Scale(UI.Zoom)
				V2.Scale(UI.Zoom)
				V3.Scale(UI.Zoom)
				V4.Scale(UI.Zoom)

				If V1.DistanceTo(V2) > V1.DistanceTo(V3) Then
					G.DrawLine(Pens.White, V1.AsPointF, V3.AsPointF)
				ElseIf V1.DistanceTo(V2) > V1.DistanceTo(V4) Then
					G.DrawLine(Pens.White, V1.AsPointF, V4.AsPointF)
				Else
					G.DrawLine(Pens.White, V1.AsPointF, V2.AsPointF)
				End If
				G.DrawEllipse(Pens.DarkMagenta, V1.X - 5, V1.Y - 5, 10, 10)
			Next v
		Next PolyLine
	End Sub
	Private Sub DrawVertices(G As Graphics)
		For Each V In Globe.Vertices
			Dim V0 As CVector
			V0 = New CVector(V(0))
			V0.X += UI.ScrollX
			V0.Y += UI.ScrollY
			If V0.X > 360 Then V0.X -= 360
			V0.Scale(UI.Zoom)
			G.DrawEllipse(Pens.DarkMagenta, V0.X - 5, V0.Y - 5, 10, 10)
		Next V
	End Sub
	Private Sub DrawAreas(G As Graphics)
		Dim RegionID As Integer = 0
		If Globe.Regions Is Nothing Then Exit Sub
		For Each GlobeRegion In Globe.Regions
			Dim AreaPen As Pen
			AreaPen = New Pen(New SolidBrush(Hl.GetRectangleColor(RegionID)), 2)
			For Each Area In GlobeRegion.Value.Areas
				Dim TopLeftCorner As CVector = GlobeToScreenPoint(New CVector(Area.Longitude1, Area.Latitude1))
				Dim BottomRightCorner As CVector = GlobeToScreenPoint(New CVector(Area.Longitude2, Area.Latitude2))

				If TopLeftCorner.X > BottomRightCorner.X Then
					BottomRightCorner.X += 360 * UI.Zoom
				End If

				If Area Is UI.SelectedObject Then
					G.FillRectangle(New SolidBrush(Color.FromArgb(100, Hl.GetRectangleColor(RegionID))), TopLeftCorner.X, TopLeftCorner.Y, BottomRightCorner.X - TopLeftCorner.X, BottomRightCorner.Y - TopLeftCorner.Y)
				End If
				G.DrawRectangle(AreaPen, TopLeftCorner.X, TopLeftCorner.Y, BottomRightCorner.X - TopLeftCorner.X, BottomRightCorner.Y - TopLeftCorner.Y)
			Next Area
			RegionID += 1
		Next GlobeRegion
	End Sub
	Private Sub DrawCountries(G As Graphics)
		Dim CountryID As Integer = 0
		If Globe.Countries Is Nothing Then Exit Sub
		For Each GlobeCountry In Globe.Countries
			Dim AreaPen As Pen
			AreaPen = New Pen(New SolidBrush(Hl.GetRectangleColor(CountryID)), 2)
			For Each Area In GlobeCountry.Value.Areas
				Dim TopLeftCorner As CVector = GlobeToScreenPoint(New CVector(Area.Longitude1, Area.Latitude1))
				Dim BottomRightCorner As CVector = GlobeToScreenPoint(New CVector(Area.Longitude2, Area.Latitude2))

				If TopLeftCorner.X > BottomRightCorner.X Then
					BottomRightCorner.X += 360 * UI.Zoom
				End If

				If Area Is UI.SelectedObject Then
					G.FillRectangle(New SolidBrush(Color.FromArgb(100, Hl.GetRectangleColor(CountryID))), TopLeftCorner.X, TopLeftCorner.Y, BottomRightCorner.X - TopLeftCorner.X, BottomRightCorner.Y - TopLeftCorner.Y)
				End If
				G.DrawRectangle(AreaPen, TopLeftCorner.X, TopLeftCorner.Y, BottomRightCorner.X - TopLeftCorner.X, BottomRightCorner.Y - TopLeftCorner.Y)
			Next Area
			Dim LabelPosition As PointF = GlobeToScreenPoint(GlobeCountry.Value.LabelPosition).AsPointF()
			G.DrawEllipse(Pens.White, LabelPosition.X - 4, LabelPosition.Y - 4, 8, 8)
			G.DrawString(GlobeCountry.Key, Me.Font, Brushes.Black, LabelPosition)
			CountryID += 1
		Next GlobeCountry
	End Sub
	Private Sub DrawMissionZones(G As Graphics)
		If Globe.Regions Is Nothing Then Exit Sub
		Try
			If FormControls.ZonesRegionsListBox.SelectedIndex = -1 Then Exit Sub
		Catch ex As Exception
			Exit Sub
		End Try
		Dim RegionsToDraw As IEnumerable(Of KeyValuePair(Of String, CGlobe.CRegion))
		If ModifierKeys = Keys.Shift + Keys.Control Then
			RegionsToDraw = Globe.Regions
		Else
			RegionsToDraw = Globe.Regions.Where(Function(Item) Item.Key = CStr(FormControls.ZonesRegionsListBox.SelectedItem))
		End If

		For Each R In RegionsToDraw
			Dim MissionZoneID As Integer = 0
			For Each MissionZone In R.Value.MissionZones
				Dim ZonePen As Pen
				ZonePen = New Pen(New SolidBrush(Hl.GetRectangleColor(MissionZoneID)), 2)
				For Each Zone In MissionZone
					Dim TopLeftCorner As CVector = GlobeToScreenPoint(New CVector(Zone.Longitude1, Zone.Latitude1))
					Dim BottomRightCorner As CVector = GlobeToScreenPoint(New CVector(Zone.Longitude2, Zone.Latitude2))

					If TopLeftCorner = BottomRightCorner Then
						If Zone Is UI.SelectedObject Then
							G.FillEllipse(New SolidBrush(Color.FromArgb(200, Hl.GetRectangleColor(MissionZoneID))), TopLeftCorner.X - 3, TopLeftCorner.Y - 3, 6, 6)
						End If
						G.DrawEllipse(ZonePen, TopLeftCorner.X - 3, TopLeftCorner.Y - 3, 6, 6)
						If Not Zone.Texture Is Nothing Then
							G.DrawString(CStr(Zone.Texture), Me.Font, New SolidBrush(ZonePen.Color), (TopLeftCorner + New CVector(-8, -18)).AsPointF())
						End If
						If Not Zone.CityName Is Nothing Then
							G.DrawString(Zone.CityName, Me.Font, New SolidBrush(ZonePen.Color), (TopLeftCorner + New CVector(0, 0)).AsPointF())
						End If
					Else
						If Zone Is UI.SelectedObject Then
							G.FillRectangle(New SolidBrush(Color.FromArgb(100, Hl.GetRectangleColor(MissionZoneID))), TopLeftCorner.X, TopLeftCorner.Y, BottomRightCorner.X - TopLeftCorner.X, BottomRightCorner.Y - TopLeftCorner.Y)
						End If
						G.DrawRectangle(ZonePen, TopLeftCorner.X, TopLeftCorner.Y, BottomRightCorner.X - TopLeftCorner.X, BottomRightCorner.Y - TopLeftCorner.Y)
					End If
				Next Zone
				MissionZoneID += 1
			Next MissionZone
		Next R
	End Sub
	Private Sub GlobeView_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
		Dim G = e.Graphics
		G.TranslateTransform(Me.Width / 2 - 180 * UI.Zoom, Me.Height / 2)
		DrawBackground(G)
		If Not Globe Is Nothing Then
			Try
				DrawPolygons(G)
			Catch ex As Exception
				Debug.Print(ex.StackTrace)
			End Try
			If UI.EditMode = EEditMode.Polygons Then
				DrawVertices(G)
			ElseIf UI.EditMode = EEditMode.Areas Then
				DrawAreas(G)
			ElseIf UI.EditMode = EEditMode.MissionZones Then
				DrawMissionZones(G)
			ElseIf UI.EditMode = EEditMode.Countries Then
				DrawCountries(G)
			ElseIf UI.EditMode = EEditMode.Borders Then
				DrawBorders(G)
			End If
		End If
		If ShowGridToolStripMenuItem.Checked Then
			For GridX = -360 To 360 Step Project.GridLongitudalStep
				G.DrawLine(Pens.DarkGray, (UI.ScrollX + GridX) * UI.Zoom, (UI.ScrollY - 90) * UI.Zoom, (UI.ScrollX + GridX) * UI.Zoom, (UI.ScrollY + 90) * UI.Zoom)
			Next GridX
			For GridY = -90 To 90 Step Project.GridLatitudalStep
				G.DrawLine(Pens.DarkGray, 180 * UI.Zoom - CInt(Me.Width / 2), (GridY + UI.ScrollY) * UI.Zoom, CInt(Me.Width / 2) + 180 * UI.Zoom, (GridY + UI.ScrollY) * UI.Zoom)
			Next GridY
		End If
		G.DrawLine(Pens.DarkSeaGreen, 180 * UI.Zoom - CInt(Me.Width / 2), UI.ScrollY * UI.Zoom, CInt(Me.Width / 2) + 180 * UI.Zoom, UI.ScrollY * UI.Zoom)
		G.DrawLine(Pens.Crimson, 180 * UI.Zoom - CInt(Me.Width / 2), (90 + UI.ScrollY) * UI.Zoom, CInt(Me.Width / 2) + 180 * UI.Zoom, (90 + UI.ScrollY) * UI.Zoom)
		G.DrawLine(Pens.Crimson, 180 * UI.Zoom - CInt(Me.Width / 2), (UI.ScrollY - 90) * UI.Zoom, CInt(Me.Width / 2) + 180 * UI.Zoom, (UI.ScrollY - 90) * UI.Zoom)
		G.DrawLine(Pens.Crimson, (UI.ScrollX + 360) * UI.Zoom, (UI.ScrollY - 90) * UI.Zoom, (UI.ScrollX + 360) * UI.Zoom, (UI.ScrollY + 90) * UI.Zoom)
		G.DrawLine(Pens.Crimson, (UI.ScrollX + 0) * UI.Zoom, (UI.ScrollY - 90) * UI.Zoom, (UI.ScrollX + 0) * UI.Zoom, (UI.ScrollY + 90) * UI.Zoom)
		G.DrawLine(Pens.Crimson, (UI.ScrollX - 360) * UI.Zoom, (UI.ScrollY - 90) * UI.Zoom, (UI.ScrollX - 360) * UI.Zoom, (UI.ScrollY + 90) * UI.Zoom)
		If IsDelaunayOptimization() AndAlso UI.SelectionBoxOrigin IsNot Nothing Then
			Dim TopLeft = New CVector(UI.SelectionBoxOrigin)
			Dim BottomRight = ScreenToGlobePoint(MouseX, MouseY)
			If TopLeft.X > BottomRight.X Then
				Dim Temp = TopLeft.X
				TopLeft.X = BottomRight.X
				BottomRight.X = Temp
			End If
			If TopLeft.Y > BottomRight.Y Then
				Dim Temp = TopLeft.Y
				TopLeft.Y = BottomRight.Y
				BottomRight.Y = Temp
			End If
			Dim Width = (BottomRight.X - TopLeft.X) * UI.Zoom
			Dim Height = (BottomRight.Y - TopLeft.Y) * UI.Zoom
			G.DrawRectangle(Pens.Yellow, (UI.ScrollX + TopLeft.X) * UI.Zoom, (UI.ScrollY + TopLeft.Y) * UI.Zoom, Width, Height)
			G.DrawRectangle(Pens.Yellow, (UI.ScrollX + TopLeft.X + 360) * UI.Zoom, (UI.ScrollY + TopLeft.Y) * UI.Zoom, Width, Height)
			G.DrawRectangle(Pens.Yellow, (UI.ScrollX + TopLeft.X - 360) * UI.Zoom, (UI.ScrollY + TopLeft.Y) * UI.Zoom, Width, Height)
		End If
	End Sub

	Private Sub GlobeView_Resize(sender As Object, e As EventArgs) Handles Me.Resize
		Me.Refresh()
	End Sub

	Private Sub GlobeView_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
		If e.KeyCode = Keys.Left Then
			UI.KeyScrollX = 1
		End If
		If e.KeyCode = Keys.Right Then
			UI.KeyScrollX = -1
		End If
		If e.KeyCode = Keys.Up Then
			UI.KeyScrollY = 1
		End If
		If e.KeyCode = Keys.Down Then
			UI.KeyScrollY = -1
		End If
		If e.KeyCode = Keys.Add Then
			UI.KeyZoom = 1
		End If
		If e.KeyCode = Keys.Subtract Then
			UI.KeyZoom = -1
		End If

	End Sub

	Private Sub ChangeMade()
		If FormGlobus.Visible Then
			FormGlobus.Refresh()
		End If
		Hl.ChangesSaved = False
	End Sub

	Private Sub GlobeView_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
		If e.KeyCode = Keys.Left Or e.KeyCode = Keys.Right Then
			UI.KeyScrollX = 0
		End If
		If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
			UI.KeyScrollY = 0
		End If
		If e.KeyCode = Keys.Add Or e.KeyCode = Keys.Subtract Then
			UI.KeyZoom = 0
		End If
		If e.KeyCode = Keys.Delete Then
			Try
				Select Case UI.EditMode
					Case EEditMode.Polygons
						Dim PolygonToDelete = Globe.FindPolygon(ScreenToGlobePoint(MouseX, MouseY))
						Globe.RemovePolygon(PolygonToDelete)
						ChangeMade()
						Me.Refresh()
					Case EEditMode.Borders
						Dim BorderVertex = FindBorderVertex(MouseX, MouseY)
						If BorderVertex IsNot Nothing Then
							Globe.RemovePolyLineVertex(BorderVertex)
							ChangeMade()
							Me.Refresh()
						End If
					Case EEditMode.Areas
						If UI.SelectedObject IsNot Nothing Then
							Dim SelectedRegion = Globe.Regions(CStr(FormControls.AreaListBox.SelectedItem))
							SelectedRegion.Areas.Remove(UI.SelectedObject)
							ChangeMade()
							Me.Refresh()
						End If
					Case EEditMode.MissionZones
						If UI.SelectedObject IsNot Nothing Then
							Dim SelectedRegion = Globe.Regions(CStr(FormControls.ZonesRegionsListBox.SelectedItem))
							Dim SelectedMissionZone = FormControls.ZonesListBox.SelectedIndex
							SelectedRegion.MissionZones(SelectedMissionZone).Remove(UI.SelectedObject)
							ChangeMade()
							Me.Refresh()
						End If
				End Select
			Catch ex As Exception

			End Try
		End If
		If e.KeyCode = Keys.PageDown Then
			If UI.EditMode = EEditMode.Polygons Then
				Globe.FlipEdge(ScreenToGlobePoint(MouseX, MouseY))
				ChangeMade()
				Me.Refresh()
			End If
		End If
		If e.KeyCode = Keys.PageUp Then
			If UI.EditMode = EEditMode.Polygons Then
				Globe.OptimizeTriangle(ScreenToGlobePoint(MouseX, MouseY))
				ChangeMade()
				Me.Refresh()
			End If
		End If
		If e.KeyCode = Keys.G And ModifierKeys = Keys.Control Then
			ShowGridToolStripMenuItem.Checked = Not ShowGridToolStripMenuItem.Checked
			Me.Refresh()
		End If
	End Sub

	Private Sub KeyboardTimer_Tick(sender As Object, e As EventArgs) Handles KeyboardTimer.Tick
		If UI.KeyScrollX <> 0 Then
			UI.ScrollX += UI.KeyScrollX * 3
			If UI.ScrollX < 0 Then UI.ScrollX = 360 + UI.ScrollX
			If UI.ScrollX > 360 Then UI.ScrollX = UI.ScrollX - 360
			Me.Refresh()
		End If
		If UI.KeyScrollY <> 0 Then
			UI.ScrollY += UI.KeyScrollY * 3
			Me.Refresh()
		End If
		If UI.KeyZoom <> 0 Then
			UI.Zoom = Math.Max(UI.Zoom + UI.KeyZoom * (0.1 * UI.Zoom), 1)
			Me.Refresh()
		End If

	End Sub

	Private Sub ToolboxToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ToolboxToolStripMenuItem.Click
		If FormControls.Visible Then
			FormControls.Close()
		Else
			FormControls.Show(Me)
		End If
	End Sub

	Private Sub BackgroundToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackgroundToolStripMenuItem.Click
		If FormBackground.Visible Then
			FormBackground.Close()
		Else
			FormBackground.Show(Me)
		End If
	End Sub

	Private Function ScreenToGlobePoint(X As Single, Y As Single) As CVector
		ScreenToGlobePoint = New CVector(X - Me.Width / 2 - 180 * UI.Zoom, Y - Me.Height / 2)
		ScreenToGlobePoint.Scale(1 / UI.Zoom)
		ScreenToGlobePoint.X -= UI.ScrollX
		ScreenToGlobePoint.Y -= UI.ScrollY
		While ScreenToGlobePoint.X < 0
			ScreenToGlobePoint.X = 360 + ScreenToGlobePoint.X
		End While
		While ScreenToGlobePoint.X >= 360
			ScreenToGlobePoint.X = ScreenToGlobePoint.X - 360
		End While

	End Function

	Private Function GlobeToScreenPoint(Point As CVector) As CVector
		Dim ScreenPoint As CVector
		ScreenPoint = New CVector(Point)
		ScreenPoint.X += UI.ScrollX
		ScreenPoint.Y += UI.ScrollY
		If ScreenPoint.X > 360 Then ScreenPoint.X -= 360
		ScreenPoint.Scale(UI.Zoom)
		Return ScreenPoint
	End Function

	Private Function FindVertex(X As Single, Y As Single) As Integer
		For f = 0 To Globe.Vertices.Count - 1
			Dim V0 As CVector
			V0 = New CVector(Globe.Vertices(f)(0))
			V0.X += UI.ScrollX
			V0.Y += UI.ScrollY
			If V0.X > 360 Then V0.X -= 360
			V0.Scale(UI.Zoom)
			V0.X += Me.Width / 2 - 180 * UI.Zoom
			V0.Y += Me.Height / 2
			Dim Distance As Single = (V0.X - X) * (V0.X - X) + (V0.Y - Y) * (V0.Y - Y)
			If (Distance < 25) Then
				Return f
			End If
		Next f
		Return -1
	End Function

	Private Function FindBorderVertex(X As Single, Y As Single) As CVector
		For Each PL In Globe.PolyLines
			For Each V In PL.Vertices
				Dim V0 As CVector
				V0 = New CVector(V)
				V0.X += UI.ScrollX
				V0.Y += UI.ScrollY
				If V0.X > 360 Then V0.X -= 360
				V0.Scale(UI.Zoom)
				V0.X += Me.Width / 2 - 180 * UI.Zoom
				V0.Y += Me.Height / 2
				Dim Distance As Single = (V0.X - X) * (V0.X - X) + (V0.Y - Y) * (V0.Y - Y)
				If (Distance < 25) Then
					Return V
				End If
			Next V
		Next PL
		Return Nothing
	End Function

	Private Sub GlobeView_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
		If e.Button = MouseButtons.Left Then
			If UI.EditMode = EEditMode.Polygons Then
				Dim SelectedVertex As Integer
				SelectedVertex = FindVertex(e.X, e.Y)
				If SelectedVertex > -1 Then
					If ModifierKeys = Keys.Alt Then
						UI.DragPhase = EDragPhase.Started
						UI.DragIndex = Globe.SplitVertex(SelectedVertex)
						ChangeMade()
					ElseIf ModifierKeys = Keys.None Then
						UI.DragPhase = EDragPhase.Started
						UI.DragIndex = SelectedVertex
					End If
				End If
			ElseIf UI.EditMode = EEditMode.Borders And ModifierKeys = Keys.None Then
				Dim SelectedBorderVertex As CVector
				SelectedBorderVertex = FindBorderVertex(e.X, e.Y)
				If SelectedBorderVertex IsNot Nothing Then
					UI.DragPhase = EDragPhase.Started
					UI.SelectedObject = SelectedBorderVertex
				End If
			ElseIf UI.EditMode = EEditMode.DelaunayOptimization Then
				UI.DragPhase = EDragPhase.Started
				UI.SelectionBoxOrigin = ScreenToGlobePoint(e.X, e.Y)
			ElseIf UI.EditMode = EEditMode.Textures Then
				Dim AffectedPolygon = Globe.FindPolygon(ScreenToGlobePoint(e.X, e.Y))
				If AffectedPolygon <> -1 Then
					Globe.Polygons(AffectedPolygon).Texture = FormControls.GetSelectedTexture()
					ChangeMade()
					Me.Refresh()
				End If
			ElseIf UI.EditMode = EEditMode.Areas Or UI.EditMode = EEditMode.MissionZones Or UI.EditMode = EEditMode.Countries Then
				If UI.SelectedObject IsNot Nothing Then
					Dim GlobePoint = ScreenToGlobePoint(e.X, e.Y)
					UI.DragIndex = 0
					UI.DragPhase = EDragPhase.None
					If Not UI.SelectedObject.IsPointLike() Then
						If Math.Abs(GlobePoint.X - UI.SelectedObject.Longitude1) < 1 Then
							UI.DragPhase = EDragPhase.Started
							UI.DragIndex = 4
						End If
						If Math.Abs(GlobePoint.X - UI.SelectedObject.Longitude2) < 1 OrElse Math.Abs(GlobePoint.X + 360 - UI.SelectedObject.Longitude2) < 1 Then
							UI.DragPhase = EDragPhase.Started
							UI.DragIndex = 6
						End If
						If Math.Abs(GlobePoint.Y - UI.SelectedObject.Latitude1) < 1 Then
							UI.DragPhase = EDragPhase.Started
							UI.DragIndex = 8
						End If
						If Math.Abs(GlobePoint.Y - UI.SelectedObject.Latitude2) < 1 Then
							UI.DragPhase = EDragPhase.Started
							UI.DragIndex = 2
						End If
					End If
					If UI.DragPhase = EDragPhase.None Then
						Dim ObjectUnderCursor As Object = Nothing
						If UI.EditMode = EEditMode.MissionZones Then
							Dim SelectedRegion = Globe.Regions(CStr(FormControls.ZonesRegionsListBox.SelectedItem))
							Dim SelectedMissionZone = FormControls.ZonesListBox.SelectedIndex
							ObjectUnderCursor = Globe.FindMissionZone(SelectedRegion, SelectedMissionZone, GlobePoint)
						End If
						If UI.EditMode = EEditMode.Areas Then ObjectUnderCursor = Globe.FindArea(GlobePoint)
						If UI.EditMode = EEditMode.Countries Then ObjectUnderCursor = Globe.FindCountry(GlobePoint)
						If UI.SelectedObject Is ObjectUnderCursor Then
							UI.DragPhase = EDragPhase.Started
							UI.DragIndex = -1
						End If
					End If
				End If
			End If
		ElseIf e.Button = MouseButtons.Middle Then
			UI.DragPhase = EDragPhase.MapScroll
		End If
	End Sub

	Private Sub GlobeView_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
		MouseX = e.X
		MouseY = e.Y
		If UI.DragPhase = EDragPhase.MapScroll And e.Button = MouseButtons.Middle Then
			Dim ScrollOffsetX = MouseX - LastMouseX
			Dim ScrollOffsetY = MouseY - LastMouseY
			If ScrollOffsetX <> 0 Then
				UI.ScrollX += ScrollOffsetX / UI.Zoom
				If UI.ScrollX < 0 Then UI.ScrollX = 360 + UI.ScrollX
				If UI.ScrollX > 360 Then UI.ScrollX = UI.ScrollX - 360
			End If
			If ScrollOffsetY <> 0 Then
				UI.ScrollY += ScrollOffsetY / UI.Zoom
			End If
			If ScrollOffsetX <> 0 Or ScrollOffsetY <> 0 Then
				Me.Refresh()
			End If
		Else
			If UI.EditMode = EEditMode.Polygons Then
				If UI.DragPhase = EDragPhase.Started Then
					UI.DragPhase = EDragPhase.Moving
				End If
				If UI.DragPhase = EDragPhase.Moving Then
					Globe.MoveVertex(UI.DragIndex, ScreenToGlobePoint(e.X, e.Y))
					ChangeMade()
					Me.Refresh()
				End If
			ElseIf UI.EditMode = EEditMode.Borders Then
				If UI.DragPhase = EDragPhase.Started Then
					UI.DragPhase = EDragPhase.Moving
				End If
				If UI.DragPhase = EDragPhase.Moving Then
					Dim GlobePoint = ScreenToGlobePoint(e.X, e.Y)
					UI.SelectedObject.x = GlobePoint.X
					UI.SelectedObject.y = GlobePoint.Y
					ChangeMade()
					Me.Refresh()
				End If
			ElseIf UI.EditMode = EEditMode.DelaunayOptimization Then
				If UI.DragPhase = EDragPhase.Started Then
					UI.DragPhase = EDragPhase.Moving
				End If
				Me.Refresh()
			ElseIf UI.EditMode = EEditMode.Textures Then
				If e.Button = MouseButtons.Left Then
					Dim AffectedPolygon = Globe.FindPolygon(ScreenToGlobePoint(e.X, e.Y))
					If AffectedPolygon <> -1 Then
						Globe.Polygons(AffectedPolygon).Texture = FormControls.GetSelectedTexture()
						ChangeMade()
						Me.Refresh()
					End If
				End If
			ElseIf UI.EditMode = EEditMode.Areas Or UI.EditMode = EEditMode.MissionZones Or UI.EditMode = EEditMode.Countries Then
				If e.Button = MouseButtons.Left Then
					If UI.DragPhase = EDragPhase.Started Then
						UI.DragPhase = EDragPhase.Moving
					End If
					If UI.DragPhase = EDragPhase.Moving Then
						If ModifierKeys = Keys.Shift + Keys.Control Or ModifierKeys = Keys.Shift Then
							Select Case UI.DragIndex
								Case 4
									UI.SelectedObject.Longitude1 = FindSnapLongitude(e.X, e.Y, ModifierKeys = Keys.Shift)
								Case 6
									UI.SelectedObject.Longitude2 = FindSnapLongitude(e.X, e.Y, ModifierKeys = Keys.Shift)
								Case 8
									UI.SelectedObject.Latitude1 = FindSnapLatitude(e.X, e.Y, ModifierKeys = Keys.Shift)
								Case 2
									UI.SelectedObject.Latitude2 = FindSnapLatitude(e.X, e.Y, ModifierKeys = Keys.Shift)
							End Select
						Else
							Dim GlobePoint = ScreenToGlobePoint(MouseX, MouseY)
							Select Case UI.DragIndex
								Case 4
									UI.SelectedObject.Longitude1 = GlobePoint.X
								Case 6
									UI.SelectedObject.Longitude2 = GlobePoint.X
								Case 8
									UI.SelectedObject.Latitude1 = GlobePoint.Y
								Case 2
									UI.SelectedObject.Latitude2 = GlobePoint.Y
								Case -1
									Dim LastGlobePoint = ScreenToGlobePoint(LastMouseX, LastMouseY)
									Dim Delta = GlobePoint - LastGlobePoint
									UI.SelectedObject.Longitude1 += Delta.x
									UI.SelectedObject.Longitude2 += Delta.x
									UI.SelectedObject.Latitude1 += Delta.y
									UI.SelectedObject.Latitude2 += Delta.y
							End Select
						End If
						If UI.EditMode = EEditMode.MissionZones Then
							If UI.SelectedObject.Longitude2 < UI.SelectedObject.Longitude1 Then
								UI.SelectedObject.Longitude2 += 360
							End If
						End If
						ChangeMade()
						Me.Refresh()
					End If
				End If
			End If
		End If
		LastMouseX = MouseX
		LastMouseY = MouseY
	End Sub
	Private Sub GlobeView_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
		If UI.DragPhase = EDragPhase.MapScroll Then
			UI.DragPhase = EDragPhase.None
		End If
		If UI.EditMode = EEditMode.Polygons Then
			If UI.DragPhase = EDragPhase.None Then
				If e.Button = MouseButtons.Left Then
					If ModifierKeys = Keys.Control + Keys.Shift Then
						Globe.InsertNewTriangle(ScreenToGlobePoint(e.X, e.Y))
						ChangeMade()
						Me.Refresh()
					End If
					If ModifierKeys = Keys.Control Then
						Globe.AttachVertex(ScreenToGlobePoint(e.X, e.Y))
						ChangeMade()
						Me.Refresh()
					End If
				ElseIf e.Button = MouseButtons.Right Then
					Dim Polygon = Globe.FindPolygon(ScreenToGlobePoint(e.X, e.Y))
					If Polygon <> -1 Then
						Globe.SplitPolygon(Polygon, ScreenToGlobePoint(e.X, e.Y))
						ChangeMade()
						Me.Refresh()
					End If
				End If
			ElseIf UI.DragPhase = EDragPhase.Moving Then
				If Not ModifierKeys = Keys.Shift Then
					Globe.MergeVertex(UI.DragIndex, 5 / UI.Zoom)
					ChangeMade()
					Me.Refresh()
				End If

				UI.DragPhase = EDragPhase.None
				UI.DragIndex = -1
			End If
		ElseIf UI.EditMode = EEditMode.Borders Then
			If UI.DragPhase = EDragPhase.None Then
				If e.Button = MouseButtons.Left Then
					If ModifierKeys = Keys.Control + Keys.Shift Then
						Globe.AddPolyLine(ScreenToGlobePoint(e.X, e.Y))
						ChangeMade()
						Me.Refresh()
					ElseIf ModifierKeys = Keys.Control Then
						Globe.AddPolyLinePoint(ScreenToGlobePoint(e.X, e.Y))
						ChangeMade()
						Me.Refresh()
					End If
				End If
			ElseIf UI.DragPhase = EDragPhase.Moving Then
				UI.DragPhase = EDragPhase.None
				UI.DragIndex = -1
			End If
		ElseIf UI.EditMode = EEditMode.DelaunayOptimization Then
			If UI.DragPhase = EDragPhase.Moving Then
				DoDelaunayOptimization(UI.SelectionBoxOrigin, ScreenToGlobePoint(e.X, e.Y))
				UI.DragPhase = EDragPhase.None
				UI.SelectionBoxOrigin = Nothing
			End If
		ElseIf UI.EditMode = EEditMode.Areas Then
			If UI.DragPhase = EDragPhase.Moving Or UI.DragPhase = EDragPhase.Started Then
				UI.DragIndex = -1
				UI.DragPhase = EDragPhase.None
			ElseIf e.Button = MouseButtons.Left Then
				If ModifierKeys = Keys.Control Then
					If FormControls.AreaListBox.SelectedIndex <> -1 Then
						Dim GlobePoint = ScreenToGlobePoint(e.X, e.Y)
						Globe.Regions(CStr(FormControls.AreaListBox.SelectedItem)).Areas.Add(New CGlobe.CGlobeRectangle(GlobePoint.X, GlobePoint.Y, GlobePoint.X + 10, GlobePoint.Y + 10))
						ChangeMade()
						Me.Refresh()
					End If
				Else
					UI.SelectedObject = Globe.FindArea(ScreenToGlobePoint(e.X, e.Y))
					Try
						FormControls.AreaListBox.SelectedIndex = Globe.GetAreaRegionIndex(UI.SelectedObject)
					Catch ex As Exception

					End Try
					Me.Refresh()
				End If
			End If
		ElseIf UI.EditMode = EEditMode.MissionZones Then
			If UI.DragPhase = EDragPhase.Moving Or UI.DragPhase = EDragPhase.Started Then
				UI.DragIndex = -1
				UI.DragPhase = EDragPhase.None
			ElseIf e.Button = MouseButtons.Left Then
				Try
					If ModifierKeys = Keys.Control Then
						Dim SelectedRegion = Globe.Regions(CStr(FormControls.ZonesRegionsListBox.SelectedItem))
						Dim SelectedMissionZone = FormControls.ZonesListBox.SelectedIndex
						If SelectedMissionZone <> -1 Then
							Dim GlobePoint = ScreenToGlobePoint(e.X, e.Y)
							SelectedRegion.MissionZones(SelectedMissionZone).Add(New CGlobe.CMissionZone(GlobePoint.X, GlobePoint.Y, GlobePoint.X + 10, GlobePoint.Y + 10, Nothing, Nothing))
							ChangeMade()
							Me.Refresh()
						End If
					ElseIf ModifierKeys = Keys.Control + Keys.Alt Then
						Dim SelectedRegion = Globe.Regions(CStr(FormControls.ZonesRegionsListBox.SelectedItem))
						Dim SelectedMissionZone = FormControls.ZonesListBox.SelectedIndex
						If SelectedMissionZone <> -1 Then
							Dim GlobePoint = ScreenToGlobePoint(e.X, e.Y)
							SelectedRegion.MissionZones(SelectedMissionZone).Add(New CGlobe.CMissionZone(GlobePoint.X, GlobePoint.Y, GlobePoint.X, GlobePoint.Y, Nothing, Nothing))
							ChangeMade()
							Me.Refresh()
						End If
					Else
						Dim SelectedRegion = Globe.Regions(CStr(FormControls.ZonesRegionsListBox.SelectedItem))
						Dim SelectedMissionZone = FormControls.ZonesListBox.SelectedIndex
						UI.SelectedObject = Globe.FindMissionZone(SelectedRegion, SelectedMissionZone, ScreenToGlobePoint(e.X, e.Y))

						Me.Refresh()
					End If
				Catch ex As Exception
					Debug.Print(ex.StackTrace)
				End Try
			ElseIf e.Button = MouseButtons.Right Then
				If UI.SelectedObject IsNot Nothing Then
					MissionZoneContextMenu.Show(Me, MouseX, MouseY)
				End If
			End If
		ElseIf UI.EditMode = EEditMode.Countries Then
			If UI.DragPhase = EDragPhase.Moving Or UI.DragPhase = EDragPhase.Started Then
				UI.DragIndex = -1
				UI.DragPhase = EDragPhase.None
			ElseIf e.Button = MouseButtons.Left Then
				If ModifierKeys = Keys.Control Then
					If FormControls.CountryListBox.SelectedIndex <> -1 Then
						Dim GlobePoint = ScreenToGlobePoint(e.X, e.Y)
						Globe.Countries(CStr(FormControls.CountryListBox.SelectedItem)).Areas.Add(New CGlobe.CGlobeRectangle(GlobePoint.X, GlobePoint.Y, GlobePoint.X + 10, GlobePoint.Y + 10))
						ChangeMade()
						Me.Refresh()
					End If
				Else
					UI.SelectedObject = Globe.FindCountry(ScreenToGlobePoint(e.X, e.Y))
					Try
						FormControls.CountryListBox.SelectedIndex = Globe.GetAreaCountryIndex(UI.SelectedObject)
					Catch ex As Exception

					End Try
					Me.Refresh()
				End If
			ElseIf e.Button = MouseButtons.Right Then
				If FormControls.CountryListBox.SelectedIndex <> -1 Then
					Globe.Countries(CStr(FormControls.CountryListBox.SelectedItem)).LabelPosition = ScreenToGlobePoint(e.X, e.Y)
					ChangeMade()
					Me.Refresh()
				End If
			End If
		End If
	End Sub
	Private Sub GlobeView_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
		UI.Zoom = Math.Max(UI.Zoom + e.Delta * 0.1, 1)
		Me.Refresh()
	End Sub

	Private Sub DoDelaunayOptimization(PointFrom As CVector, PointTo As CVector)
		If PointFrom.X > PointTo.X Then
			Dim Temp = PointFrom.X
			PointFrom.X = PointTo.X
			PointTo.X = Temp
		End If
		If PointFrom.Y > PointTo.Y Then
			Dim Temp = PointFrom.Y
			PointFrom.Y = PointTo.Y
			PointTo.Y = Temp
		End If
		Globe.OptimizeSelection(PointFrom, PointTo)
		ChangeMade()
		Me.Refresh()
	End Sub

	Private Function FindSnapLongitude(x As Integer, y As Integer, OnlySelectedRegion As Boolean) As Single
		If UI.EditMode = EEditMode.Areas Then Return FindAreaSnapLongitude(x, y)
		If UI.EditMode = EEditMode.Countries Then Return FindCountrySnapLongitude(x, y)
		If UI.EditMode = EEditMode.MissionZones Then
			If OnlySelectedRegion Then
				Return FindMissionZoneSnapLongitude(x, y, Globe.Regions.Where(Function(Item) Item.Key = CStr(FormControls.ZonesRegionsListBox.SelectedItem)))
			Else
				Return FindMissionZoneSnapLongitude(x, y, Globe.Regions)
			End If
		End If
	End Function

	Private Function FindSnapLatitude(x As Integer, y As Integer, OnlySelectedRegion As Boolean) As Single
		If UI.EditMode = EEditMode.Areas Then Return FindAreaSnapLatitude(x, y)
		If UI.EditMode = EEditMode.Countries Then Return FindCountrySnapLatitude(x, y)
		If UI.EditMode = EEditMode.MissionZones Then
			If OnlySelectedRegion Then
				Return FindMissionZoneSnapLatitude(x, y, Globe.Regions.Where(Function(Item) Item.Key = CStr(FormControls.ZonesRegionsListBox.SelectedItem)))
			Else
				Return FindMissionZoneSnapLatitude(x, y, Globe.Regions)
			End If
		End If
	End Function

	Private Function FindAreaSnapLongitude(x As Integer, y As Integer) As Single
		Dim GlobePoint = ScreenToGlobePoint(x, y)
		FindAreaSnapLongitude = GlobePoint.X
		For Each GlobeRegion In Globe.Regions
			For Each Area In GlobeRegion.Value.Areas
				If Area Is UI.SelectedObject Then Continue For
				Dim TopLeftCorner = New CVector(Area.Longitude1, Area.Latitude1)
				Dim BottomRightCorner = New CVector(Area.Longitude2, Area.Latitude2)
				If GlobePoint.Y >= TopLeftCorner.Y And GlobePoint.Y <= BottomRightCorner.Y Then
					If Math.Abs(BottomRightCorner.X - GlobePoint.X) < 5 Then
						FindAreaSnapLongitude = BottomRightCorner.X
						Exit For
					End If
					If Math.Abs(TopLeftCorner.X - GlobePoint.X) < 5 Then
						FindAreaSnapLongitude = TopLeftCorner.X
						Exit For
					End If
				End If
			Next Area
		Next GlobeRegion
	End Function
	Private Function FindCountrySnapLongitude(x As Integer, y As Integer) As Single
		Dim GlobePoint = ScreenToGlobePoint(x, y)
		FindCountrySnapLongitude = GlobePoint.X
		For Each GlobeCountry In Globe.Countries
			For Each Area In GlobeCountry.Value.Areas
				If Area Is UI.SelectedObject Then Continue For
				Dim TopLeftCorner = New CVector(Area.Longitude1, Area.Latitude1)
				Dim BottomRightCorner = New CVector(Area.Longitude2, Area.Latitude2)
				If GlobePoint.Y >= TopLeftCorner.Y And GlobePoint.Y <= BottomRightCorner.Y Then
					If Math.Abs(BottomRightCorner.X - GlobePoint.X) < 5 Then
						FindCountrySnapLongitude = BottomRightCorner.X
						Exit For
					End If
					If Math.Abs(TopLeftCorner.X - GlobePoint.X) < 5 Then
						FindCountrySnapLongitude = TopLeftCorner.X
						Exit For
					End If
				End If
			Next Area
		Next GlobeCountry
	End Function

	Private Function FindAreaSnapLatitude(x As Integer, y As Integer) As Single
		Dim GlobePoint = ScreenToGlobePoint(x, y)
		FindAreaSnapLatitude = GlobePoint.Y
		For Each GlobeRegion In Globe.Regions
			For Each Area In GlobeRegion.Value.Areas
				If Area Is UI.SelectedObject Then Continue For
				Dim TopLeftCorner = New CVector(Area.Longitude1, Area.Latitude1)
				Dim BottomRightCorner = New CVector(Area.Longitude2, Area.Latitude2)

				If TopLeftCorner.X > BottomRightCorner.X Then
					BottomRightCorner.X += 360
				End If

				If (GlobePoint.X >= TopLeftCorner.X And GlobePoint.X <= BottomRightCorner.X) Or (GlobePoint.X + 360 >= TopLeftCorner.X And GlobePoint.X + 360 <= BottomRightCorner.X) Then
					If Math.Abs(BottomRightCorner.Y - GlobePoint.Y) < 3 Then
						FindAreaSnapLatitude = BottomRightCorner.Y
						Exit For
					End If
					If Math.Abs(TopLeftCorner.Y - GlobePoint.Y) < 3 Then
						FindAreaSnapLatitude = TopLeftCorner.Y
						Exit For
					End If
				End If
			Next Area
		Next GlobeRegion
	End Function
	Private Function FindCountrySnapLatitude(x As Integer, y As Integer) As Single
		Dim GlobePoint = ScreenToGlobePoint(x, y)
		FindCountrySnapLatitude = GlobePoint.Y
		For Each GlobeCountry In Globe.Countries
			For Each Area In GlobeCountry.Value.Areas
				If Area Is UI.SelectedObject Then Continue For
				Dim TopLeftCorner = New CVector(Area.Longitude1, Area.Latitude1)
				Dim BottomRightCorner = New CVector(Area.Longitude2, Area.Latitude2)

				If TopLeftCorner.X > BottomRightCorner.X Then
					BottomRightCorner.X += 360
				End If

				If (GlobePoint.X >= TopLeftCorner.X And GlobePoint.X <= BottomRightCorner.X) Or (GlobePoint.X + 360 >= TopLeftCorner.X And GlobePoint.X + 360 <= BottomRightCorner.X) Then
					If Math.Abs(BottomRightCorner.Y - GlobePoint.Y) < 3 Then
						FindCountrySnapLatitude = BottomRightCorner.Y
						Exit For
					End If
					If Math.Abs(TopLeftCorner.Y - GlobePoint.Y) < 3 Then
						FindCountrySnapLatitude = TopLeftCorner.Y
						Exit For
					End If
				End If
			Next Area
		Next GlobeCountry
	End Function
	Private Function FindMissionZoneSnapLongitude(x As Integer, y As Integer, InRegions As IEnumerable(Of KeyValuePair(Of String, CGlobe.CRegion))) As Single
		Dim GlobePoint = ScreenToGlobePoint(x, y)
		FindMissionZoneSnapLongitude = GlobePoint.X
		For Each GlobeRegion In InRegions
			For Each MissionZone In GlobeRegion.Value.MissionZones
				For Each Zone In MissionZone
					If Zone Is UI.SelectedObject Then Continue For
					Dim TopLeftCorner = New CVector(Zone.Longitude1, Zone.Latitude1)
					Dim BottomRightCorner = New CVector(Zone.Longitude2, Zone.Latitude2)
					If GlobePoint.Y >= TopLeftCorner.Y And GlobePoint.Y <= BottomRightCorner.Y Then
						If Math.Abs(BottomRightCorner.X - GlobePoint.X) < 5 Then
							FindMissionZoneSnapLongitude = BottomRightCorner.X
							Exit For
						End If
						If Math.Abs(TopLeftCorner.X - GlobePoint.X) < 5 Then
							FindMissionZoneSnapLongitude = TopLeftCorner.X
							Exit For
						End If
					End If
				Next Zone
			Next MissionZone
		Next GlobeRegion
	End Function

	Private Function FindMissionZoneSnapLatitude(x As Integer, y As Integer, InRegions As IEnumerable(Of KeyValuePair(Of String, CGlobe.CRegion))) As Single
		Dim GlobePoint = ScreenToGlobePoint(x, y)
		FindMissionZoneSnapLatitude = GlobePoint.Y
		For Each GlobeRegion In InRegions
			For Each MissionZone In GlobeRegion.Value.MissionZones
				For Each Zone In MissionZone
					If Zone Is UI.SelectedObject Then Continue For
					Dim TopLeftCorner = New CVector(Zone.Longitude1, Zone.Latitude1)
					Dim BottomRightCorner = New CVector(Zone.Longitude2, Zone.Latitude2)

					If (GlobePoint.X >= TopLeftCorner.X And GlobePoint.X <= BottomRightCorner.X) Or (GlobePoint.X + 360 >= TopLeftCorner.X And GlobePoint.X + 360 <= BottomRightCorner.X) Then
						If Math.Abs(BottomRightCorner.Y - GlobePoint.Y) < 3 Then
							FindMissionZoneSnapLatitude = BottomRightCorner.Y
							Exit For
						End If
						If Math.Abs(TopLeftCorner.Y - GlobePoint.Y) < 3 Then
							FindMissionZoneSnapLatitude = TopLeftCorner.Y
							Exit For
						End If
					End If
				Next Zone
			Next MissionZone
		Next GlobeRegion
	End Function

	Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
		If CheckUnsavedFile() Then
			End
		End If
	End Sub

	Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
		If Hl.CurrentFileName <> "" Then
			Hl.SaveGlobeFile(Hl.CurrentFileName)
		Else
			Call SaveAsToolStripMenuItem_Click(sender, e)
		End If
	End Sub

	Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
		Dim SaveFileDialog As New SaveFileDialog

		SaveFileDialog.Filter = "Rule files (*.rul)|*.rul|All Files (*.*)|*.*"
		SaveFileDialog.FileName = Hl.CurrentFileName

		If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
			Dim FileName As String = SaveFileDialog.FileName
			Hl.SaveGlobeFile(FileName)
		End If
	End Sub

	Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
		If CheckUnsavedFile() Then
			Globe = New CGlobe()
			Me.Refresh()
		End If
	End Sub
	Public Function IsDelaunayOptimization() As Boolean
		Return UI.EditMode = EEditMode.DelaunayOptimization
	End Function
	Public Sub StartDelaunayOptimization()
		UI.EditMode = EEditMode.DelaunayOptimization
		Me.Refresh()
	End Sub

	Public Sub EndDelaunayOptimization()
		FormControls.EndDelaunayOptimization()
		UI.EditMode = EEditMode.Polygons
		Me.Refresh()
	End Sub

	Private Sub TexturesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TexturesToolStripMenuItem.Click
		If FormTextures.Visible Then
			FormTextures.Close()
		Else
			FormTextures.Show(Me)
		End If
	End Sub

	Private Sub SaveProjectAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem1.Click
		Dim SaveFileDialog As New SaveFileDialog

		SaveFileDialog.Filter = "Project files (*.globe)|*.globe|All Files (*.*)|*.*"
		SaveFileDialog.FileName = Project.ProjectFilename

		If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
			Dim FileName As String = SaveFileDialog.FileName
			Project.Save(FileName)
		End If
	End Sub

	Private Sub SaveProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem1.Click
		If Project.ProjectFilename <> "" Then
			Project.Save(Project.ProjectFilename)
		Else
			Call SaveProjectAsToolStripMenuItem_Click(sender, e)
		End If
	End Sub

	Private Sub OpenToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem1.Click
		If CheckUnsavedFile() Then
			Dim OpenFileDialog As New OpenFileDialog
			OpenFileDialog.Filter = "Project files (*.globe)|*.globe|All Files (*.*)|*.*"
			OpenFileDialog.Multiselect = False
			If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
				Dim FileName As String
				FileName = OpenFileDialog.FileName
				Hl.Project = CProject.CreateFromFile(FileName)
				Hl.Project.ApplyToGlobals()
				Me.Refresh()
			End If
		End If
	End Sub

	Private Sub GlobeView_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
		If Not CheckUnsavedFile() Then e.Cancel = True
	End Sub

	Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteMissionZoneMenuItem.Click
		If UI.SelectedObject IsNot Nothing Then
			Dim SelectedRegion = Globe.Regions(CStr(FormControls.ZonesRegionsListBox.SelectedItem))
			Dim SelectedMissionZone = FormControls.ZonesListBox.SelectedIndex
			SelectedRegion.MissionZones(SelectedMissionZone).Remove(UI.SelectedObject)
			ChangeMade()
			Me.Refresh()
		End If
	End Sub

	Private Sub SetTextureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditMissionZoneMenuItem.Click
		If UI.SelectedObject IsNot Nothing Then
			If UI.SelectedObject.Texture Is Nothing Then
				FormPointLikeZone.TextBoxTexture.Text = ""
			Else
				FormPointLikeZone.TextBoxTexture.Text = Trim(Str(UI.SelectedObject.Texture))
			End If
			FormPointLikeZone.TextBoxName.Text = UI.SelectedObject.CityName
			FormPointLikeZone.Location = Me.PointToScreen(New Point(LastMouseX, LastMouseY))
			Dim Response = FormPointLikeZone.ShowDialog(Me)
			If Response = DialogResult.OK Then
				Dim SelectedRegion = Globe.Regions(CStr(FormControls.ZonesRegionsListBox.SelectedItem))
				Dim SelectedMissionZone = FormControls.ZonesListBox.SelectedIndex
				UI.SelectedObject.Texture = Val(FormPointLikeZone.TextBoxTexture.Text)
				UI.SelectedObject.CityName = FormPointLikeZone.TextBoxName.Text
				ChangeMade()
				Me.Refresh()
			End If
		End If
	End Sub

	Private Sub GlobusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GlobusToolStripMenuItem.Click
		If FormGlobus.Visible Then
			FormGlobus.Close()
		Else
			FormGlobus.Show(Me)
		End If
	End Sub

	Private Sub ShowGridToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowGridToolStripMenuItem.Click
		Me.Refresh()
	End Sub

	Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
		If Not FormProjectSettings.Visible Then
			FormProjectSettings.Show(Me)
		End If
	End Sub

	Private Sub ShowTextureIndexesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowTextureIndexesToolStripMenuItem.Click
		Me.Refresh()
	End Sub
End Class
