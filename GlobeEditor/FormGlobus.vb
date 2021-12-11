Imports System.Drawing.Drawing2D

Public Class FormGlobus
	Dim GlobusRotation As Single
	Dim GlobusTilt As Single

	Private MouseX As Integer
	Private MouseY As Integer
	Private LastMouseX As Integer
	Private LastMouseY As Integer

	Private Sub FormGlobus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		GlobusRotation = 0
		GlobusTilt = 0
	End Sub

	Private Sub FormGlobus_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
		Dim DisplaySize As Single
		DisplaySize = Math.Min(Me.ClientSize.Width, Me.ClientSize.Height - SystemInformation.CaptionHeight) - 20
		Dim Center As CVector = New CVector(Me.ClientSize.Width / 2, Me.ClientSize.Height / 2)
		Dim G = e.Graphics

		G.FillEllipse(New SolidBrush(Color.DarkBlue), Center.X - DisplaySize / 2, Center.Y - DisplaySize / 2, DisplaySize, DisplaySize)

		For Each Polygon In Hl.Globe.Polygons
			DrawPolygon(Polygon, Center, DisplaySize / 2, G)
		Next Polygon
	End Sub

	Private Function NormalizeDegreesAngle(Angle As Single) As Single
		While Angle > 360
			Angle -= 360
		End While
		While Angle < 0
			Angle += 360
		End While
		Return Angle
	End Function

	Private Function NormalizeRadiansAngle(Angle As Single) As Single
		While Angle > Math.PI * 2
			Angle -= Math.PI * 2
		End While
		While Angle < 0
			Angle += Math.PI * 2
		End While
		Return Angle
	End Function

	Private Function MercatorToSpherical(PointDegrees As CVector, Radius As Single, RotationDegrees As Single, TiltDegrees As Single, ByRef WasClamped As Boolean) As CVector
		Dim Point As CVector
		Point = New CVector(PointDegrees.X * Math.PI / 180, PointDegrees.Y * Math.PI / 180)
		Dim Rotation, Tilt As Single
		Rotation = RotationDegrees * Math.PI / 180
		Tilt = TiltDegrees * Math.PI / 180
		Dim X, Y As Single
		Dim TransformedX As Single
		TransformedX = NormalizeRadiansAngle(Point.X - Rotation)
		If TransformedX > Math.PI / 2 And TransformedX < Math.PI Then
			WasClamped = True
			TransformedX = Math.PI / 2
		ElseIf TransformedX < Math.PI * (3.0 / 2.0) And TransformedX >= Math.PI Then
			WasClamped = True
			TransformedX = Math.PI * (3.0 / 2.0)
		Else
			WasClamped = False
		End If
		X = Math.Sin(TransformedX) * (Radius * Math.Cos(Point.Y))
		Y = Math.Sin(Point.Y) * Radius
		Return New CVector(X, Y)
	End Function

	Private Sub DrawPolygon(Polygon As CGlobe.CPolygon, Center As CVector, Radius As Single, G As Graphics)
		Dim IsPartial As Boolean = False
		Dim PolygonPoints = New List(Of PointF)
		Dim ClampedVertices As Integer = 0
		For v = 0 To Polygon.Vertices.Count - 1
			Dim WasClamped As Boolean
			PolygonPoints.Add((MercatorToSpherical(Polygon.Vertices(v), Radius, GlobusRotation, GlobusTilt, WasClamped) + Center).AsPointF())
			If WasClamped Then ClampedVertices += 1
		Next v
		If ClampedVertices >= Polygon.Vertices.Count Then Exit Sub
		Dim PolygonBrush As Brush
		PolygonBrush = Hl.GetTexture(Polygon.Texture)
		G.FillPolygon(PolygonBrush, PolygonPoints.ToArray(), FillMode.Alternate)
		G.DrawPolygon(Pens.Black, PolygonPoints.ToArray())
	End Sub

	Private Sub FormGlobus_Resize(sender As Object, e As EventArgs) Handles Me.Resize
		Me.Refresh()
	End Sub

	Private Sub FormGlobus_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
		MouseX = e.X
		MouseY = e.Y
		If e.Button <> MouseButtons.None Then
			Dim ScrollOffsetX = MouseX - LastMouseX
			Dim ScrollOffsetY = MouseY - LastMouseY

			GlobusRotation = NormalizeDegreesAngle(GlobusRotation + ScrollOffsetX)
			GlobusTilt = NormalizeDegreesAngle(GlobusTilt + ScrollOffsetY)
			Me.Refresh()
		End If
		LastMouseX = MouseX
		LastMouseY = MouseY
	End Sub
End Class