Public Class CVector
	Public X As Single
	Public Y As Single
	Public Sub New(Other As CVector)
		Me.X = Other.X
		Me.Y = Other.Y
	End Sub
	Public Sub New(A As PointF, B As PointF)
		X = B.X - A.X
		Y = B.Y - A.Y
	End Sub
	Public Sub New(X As Single, Y As Single)
		Me.X = X
		Me.Y = Y
	End Sub
	Public Function Length() As Single
		Length = Math.Sqrt(X * X + Y * Y)
	End Function
	Public Function AsPointF() As PointF
		AsPointF.X = X
		AsPointF.Y = Y
	End Function

	Public Function DistanceTo(Other As CVector) As Single
		Return Math.Sqrt((X - Other.X) * (X - Other.X) + (Y - Other.Y) * (Y - Other.Y))
	End Function
	Public Sub Scale(Factor As Single)
		X *= Factor
		Y *= Factor
	End Sub

	Public Shared Operator -(ByVal A As CVector, ByVal B As CVector)
		Return New CVector(A.X - B.X, A.Y - B.Y)
	End Operator
	Public Shared Operator +(ByVal A As CVector, ByVal B As CVector)
		Return New CVector(A.X + B.X, A.Y + B.Y)
	End Operator
	Public Shared Operator /(ByVal A As CVector, ByVal S As Single)
		Return New CVector(A.X / S, A.Y / S)
	End Operator
	Public Shared Operator =(ByVal A As CVector, ByVal B As CVector)
		Return A.X = B.X AndAlso A.Y = B.Y
	End Operator
	Public Shared Operator <>(ByVal A As CVector, ByVal B As CVector)
		Return A.X <> B.X OrElse A.Y <> B.Y
	End Operator
	Public Shared Function TriangleAngle(A As CVector, B As CVector, C As CVector) As Single
		Dim Al, Bl, Cl As Single
		Al = B.DistanceTo(C)
		Bl = C.DistanceTo(A)
		Cl = A.DistanceTo(B)
		Return Math.Acos((Bl * Bl + Cl * Cl - Al * Al) / (2 * Bl * Cl))
	End Function
	Public Shared Function LineDistanceToPoint(L1 As CVector, L2 As CVector, P As CVector) As Single
		Dim a, b, c As Single
		a = L2.Y - L1.Y
		b = L1.X - L2.X
		c = -((L2.Y - L1.Y) * L1.X - (L2.X - L1.X) * L1.Y)
		Return Math.Abs(a * P.X + b * P.Y + c) / Math.Sqrt(a * a + b * b)
	End Function
	Public Shared Function LinesIntersect(LA1 As CVector, LA2 As CVector, LB1 As CVector, LB2 As CVector) As Boolean
		Dim a1x, a1y, s1x, s1y As Single
		a1x = LA1.X
		a1y = LA1.Y
		s1x = LA2.X - LA1.X
		s1y = LA2.Y - LA1.Y
		Dim a2x, a2y, s2x, s2y As Single
		a2x = LB1.X
		a2y = LB1.Y
		s2x = LB2.X - LB1.X
		s2y = LB2.Y - LB1.Y

		'x=a1x+t1*s1x
		'y=a1y+t1*s1y
		'x=a2x+t2*s2x
		'y=a2y+t2*s2y

		'a1x+t1*s1x=a2x+t2*s2x
		'a1y+t1*s1y=a2y+t2*s2y
		't1=(a2x+t2*s2x-a1x)/s1x     | s1x<>0
		't1=(a2y+t2*s2y-a1y)/s1y     | s1y<>0
		'(a2x+t2*s2x-a1x)/s1x=(a2y+t2*s2y-a1y)/s1y
		'(a2x+t2*s2x-a1x)*s1y=(a2y+t2*s2y-a1y)*s1x
		'a2x*s1y+t2*s2x*s1y-a1x*s1y=a2y*s1x+t2*s2y*s1x-a1y*s1x
		't2*s2x*s1y-t2*s2y*s1x=a2y*s1x-a1y*s1x-a2x*s1y+a1x*s1y
		Dim t1, t2 As Single
		Try
			t2 = (a2y * s1x - a1y * s1x - a2x * s1y + a1x * s1y) / (s2x * s1y - s2y * s1x)
			If s1y = 0 Then
				t1 = (a2x + t2 * s2x - a1x) / s1x
			Else
				t1 = (a2y + t2 * s2y - a1y) / s1y
			End If
		Catch ex As Exception
			Return False
		End Try
		Return t2 >= 0 AndAlso t2 <= 1 AndAlso t1 >= 0 AndAlso t1 <= 1
	End Function

End Class
