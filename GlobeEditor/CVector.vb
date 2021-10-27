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

	Public Shared Function LinesIntersect(LA1 As CVector, LA2 As CVector, LB1 As CVector, LB2 As CVector) As Boolean
		Dim a As Single

		' Adx=LA2.x-LA1.x
		' Ady=LA2.y-LA1.y
		' Ax=LA1.x+Adx*a
		' Ay=LA1.y+Ady*a
		' Bx=LB1.x+Bdx*b
		' By=LB1.y+Bdy*b

		' LA1.x+Adx*a=LB1.x+Bdx*b
		' LA1.y+Ady*a=LB1.y+Bdy*b
		' b=(LA1.y+Ady*a-LB1.y) / Bdy
		' LA1.x+Adx*a=LB1.x+Bdx*((LA1.y+Ady*a-LB1.y) / Bdy)
		' Adx*a=LB1.x+Bdx*((LA1.y+Ady*a-LB1.y) / Bdy)-LA1.x
		' Adx*a=LB1.x-LA1.x+Bdx*((LA1.y/Bdy)-(LB1.y/Bdy)+(Ady*a/Bdy))
		' Adx*a=LB1.x-LA1.x+(Bdx*LA1.y/Bdy)-(Bdx*LB1.y/Bdy)+(Bdx*Ady*a/Bdy)
		' 

	End Function

End Class
