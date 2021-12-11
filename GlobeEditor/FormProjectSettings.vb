Public Class FormProjectSettings
	Private Sub FormProjectSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		GridLongitudalStep.Text = Trim(Str(Project.GridLongitudalStep))
		GridLatitudalStep.Text = Trim(Str(Project.GridLatitudalStep))
		GridInitiallyShown.Checked = Project.GridShown
	End Sub

	Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
		Project.GridLongitudalStep = Val(GridLongitudalStep.Text)
		Project.GridLatitudalStep = Val(GridLatitudalStep.Text)
		Project.GridShown = GridInitiallyShown.Checked
		GlobeView.Refresh()
		Me.Close()
	End Sub

	Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
		Me.Close()
	End Sub
End Class