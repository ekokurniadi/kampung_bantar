Public Class MenuUtama

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Label5.Text = Microsoft.VisualBasic.Right(Label5.Text, 1) + Microsoft.VisualBasic.Left(Label5.Text, Len(Label5.Text) - 1)
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Label17.Text = TimeOfDay()
    End Sub

    Private Sub Panel1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel1.Click
        Me.Close()
        Kampung.Show()
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Me.Close()
        Kampung.Show()
    End Sub

    Private Sub Panel9_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel9.Click
        Me.Close()
        Form1.Show()
    End Sub

    Private Sub PictureBox9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox9.Click
        Me.Close()
        Form1.Show()
    End Sub

    Private Sub Panel4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel4.Click
        Me.Close()
        Alternatif.Show()
    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        Me.Close()
        Alternatif.Show()
    End Sub
End Class