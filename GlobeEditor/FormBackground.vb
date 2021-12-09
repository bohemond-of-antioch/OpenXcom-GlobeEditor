Public Class FormBackground
	Private Sub ButtonLoad_Click(sender As Object, e As EventArgs) Handles ButtonLoad.Click
		Dim OpenFileDialog As New OpenFileDialog
		OpenFileDialog.Filter = "Images (*.png;*.jpg)|*.png;*.jpg|All Files (*.*)|*.*"
		OpenFileDialog.Multiselect = False
		If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
			Dim Filename As String
			Filename = OpenFileDialog.FileName
			TextBoxFilename.Text = Filename
			GlobeView.LoadBackground(Filename)
			Me.UpdateControls()
		End If
	End Sub

	Private Sub ButtonFitToMap_Click(sender As Object, e As EventArgs) Handles ButtonFitToMap.Click
		GlobeView.FitBackgroundImage()
		Me.UpdateControls()
	End Sub

	Private Sub UpdateControls()
		TextBoxFilename.Text = GlobeView.Background.Filename
		DestinationX.Value = GlobeView.Background.Destination.X
		DestinationY.Value = GlobeView.Background.Destination.Y
		DestinationWidth.Value = GlobeView.Background.Destination.Width
		DestinationHeight.Value = GlobeView.Background.Destination.Height
		OpacityTrack.Value = GlobeView.Background.Opacity
	End Sub

	Private Sub DestinationX_ValueChanged(sender As Object, e As EventArgs) Handles DestinationX.ValueChanged
		GlobeView.Background.Destination.X = DestinationX.Value
		GlobeView.Refresh()
	End Sub
	Private Sub DestinationY_ValueChanged(sender As Object, e As EventArgs) Handles DestinationY.ValueChanged
		GlobeView.Background.Destination.Y = DestinationY.Value
		GlobeView.Refresh()
	End Sub
	Private Sub DestinationWidth_ValueChanged(sender As Object, e As EventArgs) Handles DestinationWidth.ValueChanged
		GlobeView.Background.Destination.Width = DestinationWidth.Value
		GlobeView.Refresh()
	End Sub
	Private Sub DestinationHeight_ValueChanged(sender As Object, e As EventArgs) Handles DestinationHeight.ValueChanged
		GlobeView.Background.Destination.Height = DestinationHeight.Value
		GlobeView.Refresh()
	End Sub

	Private Sub OpacityTrack_Scroll(sender As Object, e As EventArgs) Handles OpacityTrack.Scroll
		GlobeView.Background.Opacity = OpacityTrack.Value
		GlobeView.Refresh()
	End Sub

	Private Sub FormBackground_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		UpdateControls()
	End Sub
End Class