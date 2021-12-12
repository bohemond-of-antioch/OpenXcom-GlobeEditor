Imports GlobeEditor

Public Class CGlobe
	Public Class CGlobeRectangle
		Public Longitude1 As Single
		Public Latitude1 As Single
		Public Longitude2 As Single
		Public Latitude2 As Single

		Public Sub New(longitude1 As Double, latitude1 As Double, longitude2 As Double, latitude2 As Double)
			Me.Longitude1 = longitude1
			Me.Latitude1 = latitude1
			Me.Longitude2 = longitude2
			Me.Latitude2 = latitude2
		End Sub

		Public Function IsPointLike() As Boolean
			Return Longitude1 = Longitude2 And Latitude1 = Latitude2
		End Function
	End Class

	Public Class CMissionZone
		Inherits CGlobeRectangle
		Public Texture As Integer?
		Public CityName As String

		Public Sub New(longitude1 As Double, latitude1 As Double, longitude2 As Double, latitude2 As Double, texture As Integer?, cityName As String)
			MyBase.New(longitude1, latitude1, longitude2, latitude2)
			If longitude2 < longitude1 Then Me.Longitude2 += 360
			Me.Texture = texture
			Me.CityName = cityName
		End Sub
	End Class

	Public Class CPolygon
		Public Vertices As List(Of CVector)
		Public Texture As Integer
	End Class
	Public Class CRegion
		Public Areas As List(Of CGlobeRectangle)
		Public MissionZones As List(Of List(Of CMissionZone))

		Public Sub New()
			Areas = New List(Of CGlobeRectangle)
			MissionZones = New List(Of List(Of CMissionZone))
		End Sub
	End Class
	Public Class CCountry
		Public LabelPosition As CVector
		Public Areas As List(Of CGlobeRectangle)

		Public Sub New()
			Areas = New List(Of CGlobeRectangle)
			LabelPosition = New CVector(0, 0)
		End Sub
	End Class
	Public Class CPolyLine
		Public Vertices As List(Of CVector)
	End Class
	Public Enum EFormat
		Standard
		Anabasis
	End Enum

	Public Name As String
	Public Polygons As List(Of CPolygon)
	Public Vertices As List(Of List(Of CVector))
	Public Regions As Dictionary(Of String, CRegion)
	Public Countries As Dictionary(Of String, CCountry)
	Public PolyLines As List(Of CPolyLine)
	Public Format As EFormat

	Public Sub New()
		Polygons = New List(Of CPolygon)
		Vertices = New List(Of List(Of CVector))
		Regions = New Dictionary(Of String, CRegion)
		Countries = New Dictionary(Of String, CCountry)
		PolyLines = New List(Of CPolyLine)
	End Sub
	Public Sub AddVertex(ByRef NewVertex As CVector)
		For Each VList In Vertices
			Dim V0 As CVector
			V0 = VList(0)
			If V0.DistanceTo(NewVertex) < 0.01 Then
				VList.Add(NewVertex)
				Exit Sub
			End If
		Next VList
		Dim NewVList As List(Of CVector)
		NewVList = New List(Of CVector)
		NewVList.Add(NewVertex)
		Vertices.Add(NewVList)
	End Sub


	Public Sub MoveVertex(Index As Integer, Position As CVector)
		If Index < 0 Or Index >= Vertices.Count Then Exit Sub
		For f = 0 To Vertices(Index).Count - 1
			Vertices(Index)(f).X = Position.X
			Vertices(Index)(f).Y = Position.Y
		Next f
	End Sub

	Public Function FindVertex(NearPoint As CVector, Optional Except As Integer = -1, Optional WithinRange As Single = 5)
		For f = 0 To Vertices.Count - 1
			If f = Except Then Continue For
			Dim V0 As CVector
			V0 = Vertices(f)(0)
			Dim Distance As Single = (V0.X - NearPoint.X) * (V0.X - NearPoint.X) + (V0.Y - NearPoint.Y) * (V0.Y - NearPoint.Y)
			If (Distance < WithinRange * WithinRange) Then
				Return f
			End If
		Next f
		Return -1
	End Function
	Public Function PolygonIndex(ByRef Polygon As CPolygon) As Integer
		For f = 0 To Polygons.Count - 1
			If Polygons(f) Is Polygon Then Return f
		Next f
		Return -1
	End Function
	Public Sub RemovePolygon(Index As Integer)
		Dim VListsToRemove As List(Of List(Of CVector)) = New List(Of List(Of CVector))
		For Each VList In Vertices
			For Each PolygonVertex In Polygons(Index).Vertices
				VList.Remove(PolygonVertex)
			Next PolygonVertex
			If VList.Count = 0 Then VListsToRemove.Add(VList)
		Next VList

		For Each VListToRemove In VListsToRemove
			Vertices.Remove(VListToRemove)
		Next VListToRemove

		Polygons.RemoveAt(Index)
	End Sub

	Public Sub MergeVertex(Index As Integer, WithinRange As Single)
		Dim SourcePoint As CVector = New CVector(Globe.Vertices(Index)(0))
		Dim MergeWith As Integer = -1

		MergeWith = FindVertex(SourcePoint, Index, WithinRange)
		If MergeWith = -1 Then Exit Sub


		Dim MergePoint = New CVector(Vertices(MergeWith)(0))

		For f = Polygons.Count - 1 To 0 Step -1
			Dim VertexInSource As CVector = Nothing
			Dim VertexInMerged As CVector = Nothing
			For Each PolygonVertex In Polygons(f).Vertices
				For Each SourceVertex In Vertices(Index)
					If PolygonVertex Is SourceVertex Then
						VertexInSource = PolygonVertex
						Continue For
					End If
				Next SourceVertex
				For Each MergedVertex In Vertices(MergeWith)
					If PolygonVertex Is MergedVertex Then
						VertexInMerged = PolygonVertex
						Continue For
					End If
				Next MergedVertex
			Next PolygonVertex
			If Not VertexInMerged Is Nothing And Not VertexInSource Is Nothing Then
				Polygons(f).Vertices.Remove(VertexInSource)
				If Polygons(f).Vertices.Count <= 2 Then
					RemovePolygon(f)
				End If
			End If
		Next f

		For Each v In Vertices(Index)
			Dim HasPolygon As Boolean
			HasPolygon = False
			For Each P In Polygons
				For Each PolygonVertex In P.Vertices
					If PolygonVertex Is v Then
						HasPolygon = True
						GoTo PolygonFound
					End If
				Next PolygonVertex
			Next P
PolygonFound:
			If HasPolygon Then
				v.X = MergePoint.X
				v.Y = MergePoint.Y
				Vertices(MergeWith).Add(v)
			End If
		Next v

		Vertices.RemoveAt(Index)
	End Sub
	Private Function TriangleAngle(A As CVector, B As CVector, C As CVector) As Single
		Dim T1B, T1C As CVector
		Dim T2B, T2C As CVector
		T1B = New CVector(B.X + 360, B.Y)
		T1C = New CVector(C.X + 360, C.Y)
		T2B = New CVector(B.X - 360, B.Y)
		T2C = New CVector(C.X - 360, C.Y)
		If A.DistanceTo(B) > A.DistanceTo(T1B) Then
			B = T1B
		ElseIf A.DistanceTo(B) > A.DistanceTo(T2B) Then
			B = T2B
		End If
		If A.DistanceTo(C) > A.DistanceTo(T1C) Then
			C = T1C
		ElseIf A.DistanceTo(C) > A.DistanceTo(T2C) Then
			C = T2C
		End If
		Return CVector.TriangleAngle(A, B, C)
	End Function
	Private Function LinesIntersect(LA1 As CVector, LA2 As CVector, LB1 As CVector, LB2 As CVector) As Boolean
		Dim SourceLines As New List(Of Tuple(Of CVector, CVector))
		Dim DestinationLines As New List(Of Tuple(Of CVector, CVector))
		If LA1.X > LA2.X Then
			Dim Temp = LA2
			LA2 = LA1
			LA1 = Temp
		End If
		If LB1.X > LB2.X Then
			Dim Temp = LB2
			LB2 = LB1
			LB1 = Temp
		End If
		Dim LA3 = New CVector(LA2.X - 360, LA2.Y)
		Dim LB3 = New CVector(LB2.X - 360, LB2.Y)
		If LA1.DistanceTo(LA2) > LA1.DistanceTo(LA3) Then
			Dim LA4 = New CVector(LA1.X + 360, LA1.Y)
			SourceLines.Add(New Tuple(Of CVector, CVector)(LA2, LA4))
			SourceLines.Add(New Tuple(Of CVector, CVector)(LA1, LA3))
		Else
			SourceLines.Add(New Tuple(Of CVector, CVector)(LA1, LA2))
		End If
		If LB1.DistanceTo(LB2) > LB1.DistanceTo(LB3) Then
			Dim LB4 = New CVector(LB1.X + 360, LB1.Y)
			DestinationLines.Add(New Tuple(Of CVector, CVector)(LB2, LB4))
			DestinationLines.Add(New Tuple(Of CVector, CVector)(LB1, LB3))
		Else
			DestinationLines.Add(New Tuple(Of CVector, CVector)(LB1, LB2))
		End If
		For Each SourceLine In SourceLines
			For Each DestinationLine In DestinationLines
				If CVector.LinesIntersect(SourceLine.Item1, SourceLine.Item2, DestinationLine.Item1, DestinationLine.Item2) Then Return True
			Next
		Next
		Return False
	End Function

	Private Function PointInTriangle(P As CVector, T1 As CVector, T2 As CVector, T3 As CVector) As Boolean
		Dim Denominator As Single = ((T2.Y - T3.Y) * (T1.X - T3.X) + (T3.X - T2.X) * (T1.Y - T3.Y))
		Dim A As Single = ((T2.Y - T3.Y) * (P.X - T3.X) + (T3.X - T2.X) * (P.Y - T3.Y)) / Denominator
		Dim B As Single = ((T3.Y - T1.Y) * (P.X - T3.X) + (T1.X - T3.X) * (P.Y - T3.Y)) / Denominator
		Dim C As Single = 1 - A - B
		Return 0 <= A And A <= 1 And 0 <= B And B <= 1 And 0 <= C And C <= 1
	End Function

	Public Function IsInPolygon(Point As CVector, Polygon As CPolygon) As Boolean
		If Polygon.Vertices.Count = 3 Then
			Dim PhasedPoint = New CVector(Point)
			PhasedPoint.X += 360
			Dim NormalizedVertex0 = New CVector(Polygon.Vertices(0))
			Dim NormalizedVertex1 = New CVector(Polygon.Vertices(1))
			Dim NormalizedVertex2 = New CVector(Polygon.Vertices(2))
			Dim AcrossPrimeMeridian As Boolean = False
			If Math.Abs(Polygon.Vertices(0).X - Polygon.Vertices(1).X) > Math.Abs(Polygon.Vertices(0).X - (Polygon.Vertices(1).X + 360)) Then AcrossPrimeMeridian = True
			If Math.Abs(Polygon.Vertices(1).X - Polygon.Vertices(2).X) > Math.Abs(Polygon.Vertices(1).X - (Polygon.Vertices(2).X + 360)) Then AcrossPrimeMeridian = True
			If Math.Abs(Polygon.Vertices(2).X - Polygon.Vertices(0).X) > Math.Abs(Polygon.Vertices(2).X - (Polygon.Vertices(0).X + 360)) Then AcrossPrimeMeridian = True
			If AcrossPrimeMeridian Then
				If NormalizedVertex0.X < 180 Then NormalizedVertex0.X += 360
				If NormalizedVertex1.X < 180 Then NormalizedVertex1.X += 360
				If NormalizedVertex2.X < 180 Then NormalizedVertex2.X += 360
			End If

			Return PointInTriangle(Point, NormalizedVertex0, NormalizedVertex1, NormalizedVertex2) OrElse PointInTriangle(PhasedPoint, NormalizedVertex0, NormalizedVertex1, NormalizedVertex2)
		Else
			Dim PhasedPoint = New CVector(Point)
			PhasedPoint.X += 360
			Dim NormalizedVertex0 = New CVector(Polygon.Vertices(0))
			Dim NormalizedVertex1 = New CVector(Polygon.Vertices(1))
			Dim NormalizedVertex2 = New CVector(Polygon.Vertices(2))
			Dim NormalizedVertex3 = New CVector(Polygon.Vertices(3))
			Dim AcrossPrimeMeridian As Boolean = False
			If Math.Abs(Polygon.Vertices(0).X - Polygon.Vertices(1).X) > Math.Abs(Polygon.Vertices(0).X - (Polygon.Vertices(1).X + 360)) Then AcrossPrimeMeridian = True
			If Math.Abs(Polygon.Vertices(1).X - Polygon.Vertices(2).X) > Math.Abs(Polygon.Vertices(1).X - (Polygon.Vertices(2).X + 360)) Then AcrossPrimeMeridian = True
			If Math.Abs(Polygon.Vertices(2).X - Polygon.Vertices(0).X) > Math.Abs(Polygon.Vertices(2).X - (Polygon.Vertices(0).X + 360)) Then AcrossPrimeMeridian = True
			If Math.Abs(Polygon.Vertices(2).X - Polygon.Vertices(3).X) > Math.Abs(Polygon.Vertices(2).X - (Polygon.Vertices(3).X + 360)) Then AcrossPrimeMeridian = True
			If AcrossPrimeMeridian Then
				If NormalizedVertex0.X < 180 Then NormalizedVertex0.X += 360
				If NormalizedVertex1.X < 180 Then NormalizedVertex1.X += 360
				If NormalizedVertex2.X < 180 Then NormalizedVertex2.X += 360
				If NormalizedVertex3.X < 180 Then NormalizedVertex3.X += 360
			End If

			Return PointInTriangle(Point, NormalizedVertex0, NormalizedVertex1, NormalizedVertex2) OrElse
				PointInTriangle(PhasedPoint, NormalizedVertex0, NormalizedVertex1, NormalizedVertex2) OrElse
				PointInTriangle(Point, NormalizedVertex2, NormalizedVertex3, NormalizedVertex0) OrElse
				PointInTriangle(PhasedPoint, NormalizedVertex2, NormalizedVertex3, NormalizedVertex0)

		End If
	End Function
	Public Function FindPolygon(Point As CVector) As Integer
		For f = 0 To Polygons.Count - 1
			If IsInPolygon(Point, Polygons(f)) Then Return f
		Next f
		Return -1
	End Function
	Public Function SplitVertex(Index As Integer) As Integer
		If Vertices(Index).Count = 1 Then Return Index
		Dim NewVertex = New List(Of CVector)
		NewVertex.Add(Vertices(Index).Last)
		Vertices(Index).Remove(Vertices(Index).Last)
		Vertices.Add(NewVertex)
		Return Vertices.Count - 1
	End Function
	Private Function FindTriangleByEdge(ByRef V1 As CVector, ByRef V2 As CVector, ByRef Optional Exclude As CPolygon = Nothing, ByRef Optional OutsideVertex As CVector = Nothing) As CPolygon
		For Each P In Polygons
			If P.Vertices.Count = 3 And Not P Is Exclude Then
				If (P.Vertices(0) = V1 AndAlso P.Vertices(1) = V2) OrElse (P.Vertices(0) = V2 AndAlso P.Vertices(1) = V1) Then
					If Not OutsideVertex Is Nothing Then OutsideVertex.X = P.Vertices(2).X : OutsideVertex.Y = P.Vertices(2).Y
					Return P
				End If
				If (P.Vertices(1) = V1 AndAlso P.Vertices(2) = V2) OrElse (P.Vertices(1) = V2 AndAlso P.Vertices(2) = V1) Then
					If Not OutsideVertex Is Nothing Then OutsideVertex.X = P.Vertices(0).X : OutsideVertex.Y = P.Vertices(0).Y
					Return P
				End If
				If (P.Vertices(2) = V1 AndAlso P.Vertices(0) = V2) OrElse (P.Vertices(2) = V2 AndAlso P.Vertices(0) = V1) Then
					If Not OutsideVertex Is Nothing Then OutsideVertex.X = P.Vertices(1).X : OutsideVertex.Y = P.Vertices(1).Y
					Return P
				End If
			End If
		Next P
		Return Nothing
	End Function
	Private Function FlipEdgeIfNotDelaunay(MainTriangle As CPolygon, Edge As Integer) As Boolean
		If MainTriangle.Vertices.Count > 3 Then Return False
		Dim AdjacentTriangle As CPolygon = Nothing
		Dim MainEdgeVertex1, MainEdgeVertex2 As CVector
		Dim MainOutsideVertex, AdjacentOutsideVertex As CVector
		AdjacentOutsideVertex = New CVector(0, 0)

		AdjacentTriangle = FindTriangleByEdge(MainTriangle.Vertices(Edge), MainTriangle.Vertices((Edge + 1) Mod 3), MainTriangle, AdjacentOutsideVertex)
		MainEdgeVertex1 = MainTriangle.Vertices(Edge)
		MainEdgeVertex2 = MainTriangle.Vertices((Edge + 1) Mod 3)
		MainOutsideVertex = MainTriangle.Vertices((Edge + 2) Mod 3)

		If Not AdjacentTriangle Is Nothing Then
			If LinesIntersect(MainOutsideVertex, AdjacentOutsideVertex, MainEdgeVertex1, MainEdgeVertex2) Then
				Dim MainOutsideVertexAngle, AdjacentOutsideVertexAngle As Single

				MainOutsideVertexAngle = TriangleAngle(MainOutsideVertex, MainEdgeVertex1, MainEdgeVertex2)
				AdjacentOutsideVertexAngle = TriangleAngle(AdjacentOutsideVertex, MainEdgeVertex2, MainEdgeVertex1)
				If MainOutsideVertexAngle + AdjacentOutsideVertexAngle > Math.PI Then
					Dim Texture1, Texture2 As Integer
					Texture1 = MainTriangle.Texture
					Texture2 = AdjacentTriangle.Texture
					RemovePolygon(PolygonIndex(MainTriangle))
					RemovePolygon(PolygonIndex(AdjacentTriangle))
					AddTriangle(MainOutsideVertex, MainEdgeVertex1, AdjacentOutsideVertex, Texture1)
					AddTriangle(MainOutsideVertex, AdjacentOutsideVertex, MainEdgeVertex2, Texture2)
					Return True
				End If
			End If
		End If
		Return False
	End Function
	Public Function OptimizeTriangle(Position As CVector) As Boolean
		Dim MainTriangleIndex = FindPolygon(Position)
		If MainTriangleIndex = -1 Then Return False
		Dim MainTriangle = Polygons(MainTriangleIndex)
		OptimizeTriangle(MainTriangle)
	End Function
	Public Function OptimizeTriangle(MainTriangle As CPolygon) As Boolean
		OptimizeTriangle = FlipEdgeIfNotDelaunay(MainTriangle, 0)
		If Not OptimizeTriangle Then OptimizeTriangle = FlipEdgeIfNotDelaunay(MainTriangle, 1)
		If Not OptimizeTriangle Then OptimizeTriangle = FlipEdgeIfNotDelaunay(MainTriangle, 2)
	End Function
	Public Sub OptimizeEverything()
		Dim AllOptimized = False
		While Not AllOptimized
			AllOptimized = True
			For f = 0 To Polygons.Count - 1
				AllOptimized = AllOptimized And Not OptimizeTriangle(Polygons(f))
			Next f
		End While
	End Sub
	Public Sub OptimizeSelection(TopLeft As CVector, BottomRight As CVector)
		Dim AllOptimized = False
		While Not AllOptimized
			AllOptimized = True
			For f = 0 To Polygons.Count - 1
				Dim InSelection As Boolean = False
				For Each V In Polygons(f).Vertices
					If V.X >= TopLeft.X And V.X <= BottomRight.X And V.Y >= TopLeft.Y And V.Y <= BottomRight.Y Then InSelection = True : Exit For
				Next V
				If InSelection Then
					AllOptimized = AllOptimized And Not OptimizeTriangle(Polygons(f))
				End If
			Next f
		End While
	End Sub
	Public Sub FlipEdge(Position As CVector)
		Dim MainTriangleIndex = FindPolygon(Position)
		If MainTriangleIndex = -1 OrElse Polygons(MainTriangleIndex).Vertices.Count > 3 Then Exit Sub
		Dim MainTriangle = Polygons(MainTriangleIndex)
		Dim AdjacentTriangle As CPolygon = Nothing
		Dim MainEdgeVertex1, MainEdgeVertex2 As CVector
		Dim MainOutsideVertex, AdjacentOutsideVertex As CVector
		AdjacentOutsideVertex = New CVector(0, 0)
		If CVector.LineDistanceToPoint(MainTriangle.Vertices(0), MainTriangle.Vertices(1), Position) < 1 Then
			AdjacentTriangle = FindTriangleByEdge(MainTriangle.Vertices(0), MainTriangle.Vertices(1), MainTriangle, AdjacentOutsideVertex)
			MainEdgeVertex1 = MainTriangle.Vertices(0)
			MainEdgeVertex2 = MainTriangle.Vertices(1)
			MainOutsideVertex = MainTriangle.Vertices(2)
		End If
		If AdjacentTriangle Is Nothing And CVector.LineDistanceToPoint(MainTriangle.Vertices(1), MainTriangle.Vertices(2), Position) < 1 Then
			AdjacentTriangle = FindTriangleByEdge(MainTriangle.Vertices(1), MainTriangle.Vertices(2), MainTriangle, AdjacentOutsideVertex)
			MainEdgeVertex1 = MainTriangle.Vertices(1)
			MainEdgeVertex2 = MainTriangle.Vertices(2)
			MainOutsideVertex = MainTriangle.Vertices(0)
		End If
		If AdjacentTriangle Is Nothing And CVector.LineDistanceToPoint(MainTriangle.Vertices(2), MainTriangle.Vertices(0), Position) < 1 Then
			AdjacentTriangle = FindTriangleByEdge(MainTriangle.Vertices(2), MainTriangle.Vertices(0), MainTriangle, AdjacentOutsideVertex)
			MainEdgeVertex1 = MainTriangle.Vertices(2)
			MainEdgeVertex2 = MainTriangle.Vertices(0)
			MainOutsideVertex = MainTriangle.Vertices(1)
		End If
		If Not AdjacentTriangle Is Nothing Then
			If LinesIntersect(MainOutsideVertex, AdjacentOutsideVertex, MainEdgeVertex1, MainEdgeVertex2) Then
				Dim Texture1, Texture2 As Integer
				Texture1 = MainTriangle.Texture
				Texture2 = AdjacentTriangle.Texture
				RemovePolygon(PolygonIndex(MainTriangle))
				RemovePolygon(PolygonIndex(AdjacentTriangle))
				AddTriangle(MainOutsideVertex, MainEdgeVertex1, AdjacentOutsideVertex, Texture1)
				AddTriangle(MainOutsideVertex, AdjacentOutsideVertex, MainEdgeVertex2, Texture2)
			End If
		End If
	End Sub
	Public Sub SplitPolygon(Index As Integer, Position As CVector)
		If Polygons(Index).Vertices.Count > 3 Then
			Dim Texture = Polygons(Index).Texture
			Dim Vertex1, Vertex2, Vertex3, Vertex4 As CVector
			Vertex1 = Polygons(Index).Vertices(0)
			Vertex2 = Polygons(Index).Vertices(1)
			Vertex3 = Polygons(Index).Vertices(2)
			Vertex4 = Polygons(Index).Vertices(3)
			RemovePolygon(Index)
			AddTriangle(Vertex1, Vertex2, Vertex3, Texture)
			AddTriangle(Vertex3, Vertex4, Vertex1, Texture)
		Else
			Dim Texture = Polygons(Index).Texture
			Dim Vertex1, Vertex2, Vertex3 As CVector
			Vertex1 = Polygons(Index).Vertices(0)
			Vertex2 = Polygons(Index).Vertices(1)
			Vertex3 = Polygons(Index).Vertices(2)
			RemovePolygon(Index)
			AddTriangle(Vertex1, Vertex2, Position, Texture)
			AddTriangle(Vertex2, Vertex3, Position, Texture)
			AddTriangle(Vertex3, Vertex1, Position, Texture)
		End If
	End Sub
	Private Sub AddTriangle(V1 As CVector, V2 As CVector, V3 As CVector, Optional Texture As Integer = 0)
		Dim NewPolygon = New CPolygon
		NewPolygon.Texture = Texture
		NewPolygon.Vertices = New List(Of CVector)

		Dim Vertex1, Vertex2, Vertex3 As CVector
		Vertex1 = New CVector(V1)
		Vertex2 = New CVector(V2)
		Vertex3 = New CVector(V3)

		NewPolygon.Vertices.Add(Vertex1)
		AddVertex(Vertex1)
		NewPolygon.Vertices.Add(Vertex2)
		AddVertex(Vertex2)
		NewPolygon.Vertices.Add(Vertex3)
		AddVertex(Vertex3)

		Polygons.Add(NewPolygon)
	End Sub
	Public Sub InsertNewTriangle(Position As CVector)
		Dim Vertex1, Vertex2, Vertex3 As CVector
		Vertex1 = New CVector(Position + New CVector(1, 1))
		Vertex2 = New CVector(Position + New CVector(1, 0))
		Vertex3 = New CVector(Position)
		AddTriangle(Vertex1, Vertex2, Vertex3)
	End Sub
	Public Sub AttachVertex(Position As CVector)
		If FindPolygon(Position) <> -1 Then Exit Sub
		Dim NearestVertices(2) As Integer
		Dim NearestVerticesDistance(2) As Single
		For f = 0 To 2
			NearestVerticesDistance(f) = Single.NaN
		Next f
		Dim ViableVertices As Integer = 0
		For f = 0 To Vertices.Count - 1
			Dim Distance As Single
			Distance = Vertices(f)(0).DistanceTo(Position)
			If Distance > 20 Then Continue For
			For Each P In Polygons
				For v = 0 To P.Vertices.Count - 1
					Dim V0 = P.Vertices(v)
					Dim V1 = P.Vertices((v + 1) Mod P.Vertices.Count)
					If Vertices(f)(0).DistanceTo(V0) > 0.1 AndAlso Vertices(f)(0).DistanceTo(V1) > 0.1 Then
						If LinesIntersect(V0, V1, Vertices(f)(0), Position) Then GoTo ZaTo
					End If
				Next v
			Next P
			ViableVertices += 1
			If Single.IsNaN(NearestVerticesDistance(0)) OrElse NearestVerticesDistance(0) > Distance Then
				For t = 2 To 1 Step -1
					NearestVerticesDistance(t) = NearestVerticesDistance(t - 1)
					NearestVertices(t) = NearestVertices(t - 1)
				Next t
				NearestVerticesDistance(0) = Distance
				NearestVertices(0) = f
			ElseIf Single.IsNaN(NearestVerticesDistance(1)) OrElse NearestVerticesDistance(1) > Distance Then
				For t = 2 To 2 Step -1
					NearestVerticesDistance(t) = NearestVerticesDistance(t - 1)
					NearestVertices(t) = NearestVertices(t - 1)
				Next t
				NearestVerticesDistance(1) = Distance
				NearestVertices(1) = f
			ElseIf Single.IsNaN(NearestVerticesDistance(2)) OrElse NearestVerticesDistance(2) > Distance Then
				NearestVerticesDistance(2) = Distance
				NearestVertices(2) = f
			End If
ZaTo:
		Next f

		Dim PolygonSetupDone As Boolean = False
		Dim Vertex1, Vertex2, Vertex3 As CVector
		If ViableVertices = 3 Then
			Dim V1, V2, V3 As CVector
			V1 = New CVector(Vertices(NearestVertices(0))(0))
			V2 = New CVector(Vertices(NearestVertices(1))(0))
			V3 = New CVector(Vertices(NearestVertices(2))(0))

			If PointInTriangle(Position, V1, V2, V3) Then
				Vertex1 = V1
				Vertex2 = V2
				Vertex3 = V3
				PolygonSetupDone = True
			End If

		End If
		If Not PolygonSetupDone Then
			If Single.IsNaN(NearestVerticesDistance(0)) Then
				Vertex1 = New CVector(Position + New CVector(1, 1))
				Vertex2 = New CVector(Position + New CVector(1, 0))
				Vertex3 = New CVector(Position)
			Else
				Vertex1 = New CVector(Vertices(NearestVertices(0))(0))
				Vertex2 = New CVector(Vertices(NearestVertices(1))(0))
				Vertex3 = New CVector(Position)
			End If
		End If

		AddTriangle(Vertex1, Vertex2, Vertex3)
	End Sub

	Public MaxTextureIndex As Integer
	Public Sub New(Data As YamlNode)
		MaxTextureIndex = 0
		Polygons = New List(Of CPolygon)
		Vertices = New List(Of List(Of CVector))
		Regions = New Dictionary(Of String, CRegion)
		Countries = New Dictionary(Of String, CCountry)
		PolyLines = New List(Of CPolyLine)
		If Data.HasMapping("globe") Then
			Dim GlobeData As YamlNode
			If (Data.GetMapping("globe").Type = YamlNode.EType.Sequence) Then
				Format = EFormat.Anabasis
				GlobeData = Data.GetMapping("globe").GetItem(0)
				Name = GlobeData.GetMapping("planet").GetValue()
			Else
				Format = EFormat.Standard
				GlobeData = Data.GetMapping("globe")
				Name = "N/A"
			End If
			If GlobeData.HasMapping("polygons") Then
				Dim GlobePolygonsData = GlobeData.GetMapping("polygons")
				For p = 0 To GlobePolygonsData.ItemCount - 1
					Dim PolygonData = GlobePolygonsData.GetItem(p)
					Dim NewPolygon = New CPolygon
					NewPolygon.Texture = Val(PolygonData.GetItem(0).GetValue())
					If NewPolygon.Texture > MaxTextureIndex Then MaxTextureIndex = NewPolygon.Texture
					NewPolygon.Vertices = New List(Of CVector)
					For v = 0 To (PolygonData.ItemCount - 1) / 2 - 1
						Dim PolygonVertex = New CVector(Val(PolygonData.GetItem(v * 2 + 1).GetValue()), Val(PolygonData.GetItem(v * 2 + 2).GetValue()))
						While PolygonVertex.X > 360
							PolygonVertex.X -= 360
						End While
						NewPolygon.Vertices.Add(PolygonVertex)
						AddVertex(PolygonVertex)
					Next v
					Polygons.Add(NewPolygon)
				Next p
			End If
			If GlobeData.HasMapping("polylines") Then
				Dim GlobePolyLinesData = GlobeData.GetMapping("polylines")
				For p = 0 To GlobePolyLinesData.ItemCount - 1
					Dim PolyLineData = GlobePolyLinesData.GetItem(p)
					Dim NewPolyLine = New CPolyLine
					NewPolyLine.Vertices = New List(Of CVector)
					For v = 0 To PolyLineData.ItemCount / 2 - 1
						Dim PolyLineVertex = New CVector(Val(PolyLineData.GetItem(v * 2).GetValue()), Val(PolyLineData.GetItem(v * 2 + 1).GetValue()))
						NewPolyLine.Vertices.Add(PolyLineVertex)
					Next v
					PolyLines.Add(NewPolyLine)
				Next p
            End If
		End If
		If Data.HasMapping("regions") Then
			Dim RegionData As YamlNode
			RegionData = Data.GetMapping("regions")
			For r = 0 To RegionData.ItemCount - 1
				Dim RegionItem = RegionData.GetItem(r)
				If Not RegionItem.HasMapping("type") Then Continue For ' Probably a delete node
				Dim RegionName = RegionItem.GetMapping("type").GetValue()
				If Not Regions.ContainsKey(RegionName) Then
					Regions.Add(RegionName, New CRegion())
				End If

				If RegionItem.HasMapping("areas") Then
					Dim AreaData = RegionItem.GetMapping("areas")
					If AreaData.Type = YamlNode.EType.Sequence Then
						For a = 0 To AreaData.ItemCount - 1
							Dim Longitude1 = Val(AreaData.GetItem(a).GetItem(0).GetValue())
							Dim Longitude2 = Val(AreaData.GetItem(a).GetItem(1).GetValue())
							Dim Latitude1 = Val(AreaData.GetItem(a).GetItem(2).GetValue())
							Dim Latitude2 = Val(AreaData.GetItem(a).GetItem(3).GetValue())
							Regions(RegionName).Areas.Add(New CGlobeRectangle(Longitude1, Latitude1, Longitude2, Latitude2))
						Next a
					End If
				End If
				If RegionItem.HasMapping("missionZones") Then
					Dim ZoneData = RegionItem.GetMapping("missionZones")
					If ZoneData.Type = YamlNode.EType.Sequence Then
						For ZoneID = 0 To ZoneData.ItemCount - 1
							Dim NewMissionZone = New List(Of CMissionZone)
							Dim ZoneRectangles = ZoneData.GetItem(ZoneID)
							For ZoneRectangleID = 0 To ZoneRectangles.ItemCount - 1
								Dim ZoneAttributes = ZoneRectangles.GetItem(ZoneRectangleID)
								Dim Longitude1 = Val(ZoneAttributes.GetItem(0).GetValue())
								Dim Longitude2 = Val(ZoneAttributes.GetItem(1).GetValue())
								Dim Latitude1 = Val(ZoneAttributes.GetItem(2).GetValue())
								Dim Latitude2 = Val(ZoneAttributes.GetItem(3).GetValue())
								Dim Texture As Integer? = Nothing
								Dim CityName As String = Nothing
								If ZoneAttributes.ItemCount() > 4 Then Texture = Val(ZoneAttributes.GetItem(4).GetValue())
								If ZoneAttributes.ItemCount() > 5 Then CityName = ZoneAttributes.GetItem(5).GetValue()
								NewMissionZone.Add(New CMissionZone(Longitude1, Latitude1, Longitude2, Latitude2, Texture, CityName))
							Next ZoneRectangleID
							Regions(RegionName).MissionZones.Add(NewMissionZone)
						Next ZoneID
					End If
				End If
			Next r
		End If
		If Data.HasMapping("countries") Then
			Dim CountryData As YamlNode
			CountryData = Data.GetMapping("countries")

			For C = 0 To CountryData.ItemCount - 1
				Dim CountryItem = CountryData.GetItem(C)
				If Not CountryItem.HasMapping("type") Then Continue For ' Probably a delete node
				Dim CountryName = CountryItem.GetMapping("type").GetValue()
				If Not Countries.ContainsKey(CountryName) Then
					Countries.Add(CountryName, New CCountry())
				End If

				If CountryItem.HasMapping("areas") Then
					Dim AreaData = CountryItem.GetMapping("areas")
					If AreaData.Type = YamlNode.EType.Sequence Then
						For a = 0 To AreaData.ItemCount - 1
							Dim Longitude1 = Val(AreaData.GetItem(a).GetItem(0).GetValue())
							Dim Longitude2 = Val(AreaData.GetItem(a).GetItem(1).GetValue())
							Dim Latitude1 = Val(AreaData.GetItem(a).GetItem(2).GetValue())
							Dim Latitude2 = Val(AreaData.GetItem(a).GetItem(3).GetValue())
							Countries(CountryName).Areas.Add(New CGlobeRectangle(Longitude1, Latitude1, Longitude2, Latitude2))
						Next a
					End If
				End If
				If CountryItem.HasMapping("labelLon") Then
					Countries(CountryName).LabelPosition.X = Val(CountryItem.GetMapping("labelLon").GetValue())
				End If
				If CountryItem.HasMapping("labelLat") Then
					Countries(CountryName).LabelPosition.Y = Val(CountryItem.GetMapping("labelLat").GetValue())
				End If
			Next C
		End If
	End Sub
	Friend Sub ApplyToYaml(GlobeRules As YamlNode)
		If GlobeRules.HasMapping("globe") Then
			Dim GlobeData As YamlNode
			If (GlobeRules.GetMapping("globe").Type = YamlNode.EType.Sequence) Then
				Format = EFormat.Anabasis
				GlobeData = GlobeRules.GetMapping("globe").GetItem(0)
				Name = GlobeData.GetMapping("planet").GetValue()
			Else
				Format = EFormat.Standard
				GlobeData = GlobeRules.GetMapping("globe")
				Name = "N/A"
			End If
			Dim GlobePolygonsData = New YamlNode(YamlNode.EType.Sequence)
			For Each P In Polygons
				Dim PolygonData = New YamlNode(YamlNode.EType.Sequence)
				PolygonData.AddItem(New YamlNode(Trim(Str(P.Texture))))
				For Each V In P.Vertices
					PolygonData.AddItem(New YamlNode(Trim(Str(V.X))))
					PolygonData.AddItem(New YamlNode(Trim(Str(V.Y))))
				Next V
				GlobePolygonsData.AddItem(PolygonData)
			Next P
			GlobeData.SetMapping("polygons", GlobePolygonsData)
			Dim GlobePolyLineData = New YamlNode(YamlNode.EType.Sequence)
			For Each P In PolyLines
				Dim PolyLineData = New YamlNode(YamlNode.EType.Sequence)
				For Each V In P.Vertices
					PolyLineData.AddItem(New YamlNode(Trim(Str(V.X))))
					PolyLineData.AddItem(New YamlNode(Trim(Str(V.Y))))
				Next V
				GlobePolyLineData.AddItem(PolyLineData)
			Next P
			GlobeData.SetMapping("polylines", GlobePolyLineData)
		End If
		If Regions.Count > 0 Then
			Dim GlobeRegionsData As YamlNode
			If GlobeRules.HasMapping("regions") Then
				GlobeRegionsData = GlobeRules.GetMapping("regions")
			Else
				GlobeRegionsData = New YamlNode(YamlNode.EType.Sequence)
			End If
			For f = GlobeRegionsData.ItemCount() - 1 To 0 Step -1
				If GlobeRegionsData.GetItem(f).HasMapping("type") Then
					For Each R In Regions
						If GlobeRegionsData.GetItem(f).GetMapping("type").GetValue() = R.Key Then
							GoTo DontDeleteRegion
						End If
					Next R
					GlobeRegionsData.RemoveItem(f)
DontDeleteRegion:
				End If
			Next f
			For Each R In Regions
				Dim RegionData As YamlNode = Nothing
				For f = 0 To GlobeRegionsData.ItemCount() - 1
					If GlobeRegionsData.GetItem(f).HasMapping("type") AndAlso GlobeRegionsData.GetItem(f).GetMapping("type").GetValue() = R.Key Then
						RegionData = GlobeRegionsData.GetItem(f)
						Exit For
					End If
				Next f
				If RegionData Is Nothing Then
					RegionData = New YamlNode(YamlNode.EType.Mapping)
					RegionData.SetMapping("type", New YamlNode(R.Key))
					GlobeRegionsData.AddItem(RegionData)
				End If
				If Format = EFormat.Anabasis Then RegionData.SetMapping("planet", New YamlNode(Name))
				Dim AreasData = New YamlNode(YamlNode.EType.Sequence)
				For Each A In R.Value.Areas
					Dim Area = New YamlNode(YamlNode.EType.Sequence)
					Area.AddItem(New YamlNode(Trim(Str(A.Longitude1))))
					Area.AddItem(New YamlNode(Trim(Str(A.Longitude2))))
					Area.AddItem(New YamlNode(Trim(Str(A.Latitude1))))
					Area.AddItem(New YamlNode(Trim(Str(A.Latitude2))))
					AreasData.AddItem(Area)
				Next A
				RegionData.SetMapping("areas", AreasData)

				Dim MissionZonesData = New YamlNode(YamlNode.EType.Sequence)
				For Each MZ In R.Value.MissionZones
					Dim ZonesData = New YamlNode(YamlNode.EType.Sequence)
					For Each Z In MZ
						Dim Zone = New YamlNode(YamlNode.EType.Sequence)
						Zone.AddItem(New YamlNode(Trim(Str(Z.Longitude1))))
						Zone.AddItem(New YamlNode(Trim(Str(Z.Longitude2))))
						Zone.AddItem(New YamlNode(Trim(Str(Z.Latitude1))))
						Zone.AddItem(New YamlNode(Trim(Str(Z.Latitude2))))
						If Z.Texture IsNot Nothing Then Zone.AddItem(New YamlNode(Trim(Str(Z.Texture))))
						If Z.CityName IsNot Nothing Then Zone.AddItem(New YamlNode(Z.CityName))
						ZonesData.AddItem(Zone)
					Next Z
					MissionZonesData.AddItem(ZonesData)
				Next MZ
				RegionData.SetMapping("missionZones", MissionZonesData)

			Next R
			GlobeRules.SetMapping("regions", GlobeRegionsData)
		End If
		If Countries.Count > 0 Then
			Dim GlobeCountriesData As YamlNode
			If GlobeRules.HasMapping("countries") Then
				GlobeCountriesData = GlobeRules.GetMapping("countries")
			Else
				GlobeCountriesData = New YamlNode(YamlNode.EType.Sequence)
			End If
			For f = GlobeCountriesData.ItemCount() - 1 To 0 Step -1
				If GlobeCountriesData.GetItem(f).HasMapping("type") Then
					For Each C In Countries
						If GlobeCountriesData.GetItem(f).GetMapping("type").GetValue() = C.Key Then
							GoTo DontDeleteCountry
						End If
					Next C
					GlobeCountriesData.RemoveItem(f)
DontDeleteCountry:
				End If
			Next f
			For Each C In Countries
				Dim CountryData As YamlNode = Nothing
				For f = 0 To GlobeCountriesData.ItemCount() - 1
					If GlobeCountriesData.GetItem(f).HasMapping("type") AndAlso GlobeCountriesData.GetItem(f).GetMapping("type").GetValue() = C.Key Then
						CountryData = GlobeCountriesData.GetItem(f)
						Exit For
					End If
				Next f
				If CountryData Is Nothing Then
					CountryData = New YamlNode(YamlNode.EType.Mapping)
					CountryData.SetMapping("type", New YamlNode(C.Key))
					GlobeCountriesData.AddItem(CountryData)
				End If
				If Format = EFormat.Anabasis Then CountryData.SetMapping("planet", New YamlNode(Name))
				Dim AreasData = New YamlNode(YamlNode.EType.Sequence)
				For Each A In C.Value.Areas
					Dim Area = New YamlNode(YamlNode.EType.Sequence)
					Area.AddItem(New YamlNode(Trim(Str(A.Longitude1))))
					Area.AddItem(New YamlNode(Trim(Str(A.Longitude2))))
					Area.AddItem(New YamlNode(Trim(Str(A.Latitude1))))
					Area.AddItem(New YamlNode(Trim(Str(A.Latitude2))))
					AreasData.AddItem(Area)
				Next A
				CountryData.SetMapping("areas", AreasData)
				CountryData.SetMapping("labelLon", New YamlNode(Trim(Str(C.Value.LabelPosition.X))))
				CountryData.SetMapping("labelLat", New YamlNode(Trim(Str(C.Value.LabelPosition.Y))))
			Next C
			GlobeRules.SetMapping("countries", GlobeCountriesData)
		End If
	End Sub

	Friend Function FindArea(Point As CVector) As CGlobeRectangle
		For Each GlobeRegion In Regions
			For Each Area In GlobeRegion.Value.Areas
				Dim TopLeftCorner As CVector = New CVector(Area.Longitude1, Area.Latitude1)
				Dim BottomRightCorner As CVector = New CVector(Area.Longitude2, Area.Latitude2)

				If TopLeftCorner.X > BottomRightCorner.X Then
					BottomRightCorner.X += 360
				End If

				If Point.X >= TopLeftCorner.X And Point.X <= BottomRightCorner.X And Point.Y >= TopLeftCorner.Y And Point.Y <= BottomRightCorner.Y Then
					Return Area
				End If

				If Point.X + 360 >= TopLeftCorner.X And Point.X + 360 <= BottomRightCorner.X And Point.Y >= TopLeftCorner.Y And Point.Y <= BottomRightCorner.Y Then
					Return Area
				End If
			Next Area
		Next GlobeRegion
		Return Nothing
	End Function
	Friend Function FindCountry(Point As CVector) As CGlobeRectangle
		For Each GlobeCountry In Countries
			For Each Area In GlobeCountry.Value.Areas
				Dim TopLeftCorner As CVector = New CVector(Area.Longitude1, Area.Latitude1)
				Dim BottomRightCorner As CVector = New CVector(Area.Longitude2, Area.Latitude2)

				If TopLeftCorner.X > BottomRightCorner.X Then
					BottomRightCorner.X += 360
				End If

				If Point.X >= TopLeftCorner.X And Point.X <= BottomRightCorner.X And Point.Y >= TopLeftCorner.Y And Point.Y <= BottomRightCorner.Y Then
					Return Area
				End If

				If Point.X + 360 >= TopLeftCorner.X And Point.X + 360 <= BottomRightCorner.X And Point.Y >= TopLeftCorner.Y And Point.Y <= BottomRightCorner.Y Then
					Return Area
				End If
			Next Area
		Next GlobeCountry
		Return Nothing
	End Function
	Friend Function GetAreaRegionIndex(Area As CGlobeRectangle) As Integer
		Dim RegionID As Integer = 0
		For Each GlobeRegion In Regions
			For Each RegionArea In GlobeRegion.Value.Areas
				If RegionArea Is Area Then
					Return RegionID
				End If
			Next RegionArea
			RegionID += 1
		Next GlobeRegion
		Return -1
	End Function
	Friend Function GetAreaCountryIndex(Area As CGlobeRectangle) As Integer
		Dim CountryID As Integer = 0
		For Each GlobeCountry In Countries
			For Each CountryArea In GlobeCountry.Value.Areas
				If CountryArea Is Area Then
					Return CountryID
				End If
			Next CountryArea
			CountryID += 1
		Next GlobeCountry
		Return -1
	End Function
	Friend Function FindMissionZone(Region As CRegion, MissionZone As Integer, Point As CVector) As CMissionZone
		For Each Zone In Region.MissionZones(MissionZone)
			Dim TopLeftCorner As CVector = New CVector(Zone.Longitude1, Zone.Latitude1)
			Dim BottomRightCorner As CVector = New CVector(Zone.Longitude2, Zone.Latitude2)

			If Point.X >= TopLeftCorner.X And Point.X <= BottomRightCorner.X And Point.Y >= TopLeftCorner.Y And Point.Y <= BottomRightCorner.Y Then
				Return Zone
			End If

			If Point.X + 360 >= TopLeftCorner.X And Point.X + 360 <= BottomRightCorner.X And Point.Y >= TopLeftCorner.Y And Point.Y <= BottomRightCorner.Y Then
				Return Zone
			End If

			If Zone.IsPointLike() Then
				If Point.DistanceTo(TopLeftCorner) <= 1 Then
					Return Zone
				End If
			End If
		Next Zone
		Return Nothing
	End Function
	Friend Sub RemoveTexture(Index As Integer)
		For Each P In Polygons
			If P.Texture >= Index Then
				P.Texture = Math.Max(0, P.Texture - 1)
			End If
		Next P
	End Sub
	Friend Sub RemovePolyLineVertex(Vertex As CVector)
		For Each PL In PolyLines
			For Each V In PL.Vertices
				If V Is Vertex Then
					If PL.Vertices.Count > 1 Then
						PL.Vertices.Remove(Vertex)
						Exit Sub
					Else
						PolyLines.Remove(PL)
						Exit Sub
					End If
				End If
			Next V
		Next PL
    End Sub
	Friend Sub AddPolyLine(Point As CVector)
		Dim NewPolyLine As CPolyLine
		NewPolyLine = New CPolyLine()
		NewPolyLine.Vertices = New List(Of CVector)
		NewPolyLine.Vertices.Add(Point)
		PolyLines.Add(NewPolyLine)
	End Sub
	Private Sub SplitPolyLine(PolyLine As CPolyLine, After As Integer, Point As CVector)
		PolyLine.Vertices.Insert(After + 1, Point)
	End Sub
	Friend Sub AddPolyLinePoint(Point As CVector)
		For Each PL In PolyLines
			For v = 0 To PL.Vertices.Count - 2
				Dim V1 = New CVector(PL.Vertices(v))
				Dim V2 = New CVector(PL.Vertices(v + 1))
				Dim V3 = New CVector(V2.X - 360, V2.Y)
				Dim V4 = New CVector(V2.X + 360, V2.Y)

				If V1.DistanceTo(V2) > V1.DistanceTo(V3) Then
					If V1.DistanceTo(Point) + V3.DistanceTo(Point) < V1.DistanceTo(V3) + 2 AndAlso CVector.LineDistanceToPoint(V1, V3, Point) < 1 Then
						SplitPolyLine(PL, v, Point)
						Exit Sub
					End If
				ElseIf V1.DistanceTo(V2) > V1.DistanceTo(V4) Then
					If V1.DistanceTo(Point) + V4.DistanceTo(Point) < V1.DistanceTo(V4) + 2 AndAlso CVector.LineDistanceToPoint(V1, V4, Point) < 1 Then
						SplitPolyLine(PL, v, Point)
						Exit Sub
					End If
				Else
					If V1.DistanceTo(Point) + V2.DistanceTo(Point) < V1.DistanceTo(V2) + 2 AndAlso CVector.LineDistanceToPoint(V1, V2, Point) < 1 Then
						SplitPolyLine(PL, v, Point)
						Exit Sub
					End If
				End If
			Next v
		Next PL
		Dim NearestPolyLine As CPolyLine = Nothing
		Dim NearestPolyLineVertex As Integer = -1
		Dim NearestDistance As Single = Single.NaN
		For Each PL In PolyLines
			Dim V1 = New CVector(PL.Vertices(0))
			Dim V2 = New CVector(V1.X - 360, V1.Y)
			Dim V3 = New CVector(V1.X + 360, V1.Y)
			If Single.IsNaN(NearestDistance) OrElse V1.DistanceTo(Point) < NearestDistance Then
				NearestDistance = V1.DistanceTo(Point)
				NearestPolyLine = PL
				NearestPolyLineVertex = 0
			ElseIf V2.DistanceTo(Point) < NearestDistance Then
				NearestDistance = V2.DistanceTo(Point)
				NearestPolyLine = PL
				NearestPolyLineVertex = 0
			ElseIf V3.DistanceTo(Point) < NearestDistance Then
				NearestDistance = V3.DistanceTo(Point)
				NearestPolyLine = PL
				NearestPolyLineVertex = 0
			End If

			V1 = New CVector(PL.Vertices(PL.Vertices.Count - 1))
			V2 = New CVector(V1.X - 360, V1.Y)
			V3 = New CVector(V1.X + 360, V1.Y)
			If Single.IsNaN(NearestDistance) OrElse V1.DistanceTo(Point) < NearestDistance Then
				NearestDistance = V1.DistanceTo(Point)
				NearestPolyLine = PL
				NearestPolyLineVertex = PL.Vertices.Count - 1
			ElseIf V2.DistanceTo(Point) < NearestDistance Then
				NearestDistance = V2.DistanceTo(Point)
				NearestPolyLine = PL
				NearestPolyLineVertex = PL.Vertices.Count - 1
			ElseIf V3.DistanceTo(Point) < NearestDistance Then
				NearestDistance = V3.DistanceTo(Point)
				NearestPolyLine = PL
				NearestPolyLineVertex = PL.Vertices.Count - 1
			End If
		Next PL
		If NearestPolyLineVertex = 0 Then
			NearestPolyLine.Vertices.Insert(0, Point)
		ElseIf NearestPolyLineVertex > 0 Then
			NearestPolyLine.Vertices.Add(Point)
		End If
	End Sub
End Class
