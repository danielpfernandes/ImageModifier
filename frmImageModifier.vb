Public Class frmImageModifier

    Dim myImage As Bitmap

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        Dim open As New OpenFileDialog
        open.Title = "Image Location"
        open.Filter = "JPEG Image |*.jpg|All filed (*.*)|*.*"
        If open.ShowDialog() = Windows.Forms.DialogResult.OK Then
            myImage = New Bitmap(open.FileName, True)
            picOriginal.BorderStyle = BorderStyle.None
            picOriginal.Image = myImage
            picConverted.Image = Nothing
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If picConverted.Image IsNot Nothing Then
            Dim save As New SaveFileDialog
            save.Title = "Save Folder"
            save.Filter = "JPEG Image |*.jpg|All filed (*.*)|*.*"
            If save.ShowDialog() = Windows.Forms.DialogResult.OK Then
                picConverted.Image.Save(save.FileName)
                MessageBox.Show("The converted image has benn successfully saved!")
            End If
        Else
            MessageBox.Show("There is no image to be saved.", "Error")
        End If
    End Sub

    Private Sub btnConvert_Click(sender As Object, e As EventArgs) Handles btnConvert.Click
        If picOriginal.Image IsNot Nothing Then
            myImage = New Bitmap(picOriginal.Image)
            Dim r, g, b, average As Integer
            Dim newColor As Color

            If cboFilter.SelectedItem IsNot Nothing Then
                For y = 0 To myImage.Height - 1
                    For x = 0 To myImage.Width - 1
                        Dim pixelColor As Color = myImage.GetPixel(x, y)
                        r = CInt(pixelColor.R)
                        g = CInt(pixelColor.G)
                        b = CInt(pixelColor.B)

                        Select Case cboFilter.SelectedItem.ToString
                            Case "   Monochrome"
                                average = CInt((r + g + b) / 3)
                                If (average > 128) Then
                                    r = 255
                                    g = 255
                                    b = 255
                                Else
                                    r = 0
                                    g = 0
                                    b = 0
                                End If
                                newColor = Color.FromArgb(r, g, b)
                            Case "   Gray Scale"
                                average = CInt((r + g + b) / 3)
                                newColor = Color.FromArgb(average, average, average)
                        End Select
                        myImage.SetPixel(x, y, newColor)
                    Next
                Next
                picConverted.Image = myImage
                picConverted.BorderStyle = BorderStyle.None
            Else
                MessageBox.Show("Please select a filter to work with.", "Error")
            End If
        Else
            MessageBox.Show("Please select a picture to convert.", "Error")
        End If
    End Sub
End Class
