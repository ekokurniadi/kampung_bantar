Imports MySql.Data.MySqlClient
Public Class Form1
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksinya()
        SetWatermark(txt_user, "Username Anda")
        SetWatermark(txt_pass, "Password Anda")
        bersih()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            txt_pass.UseSystemPasswordChar = False
        Else
            txt_pass.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        txt_user.Text = ""
        txt_pass.Text = ""
        CheckBox1.Checked = False
        txt_user.Focus()
        SetWatermark(txt_user, "Username Anda")
        SetWatermark(txt_pass, "Password Anda")
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Label5.Text = Microsoft.VisualBasic.Right(Label5.Text, 1) + Microsoft.VisualBasic.Left(Label5.Text, Len(Label5.Text) - 1)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        login()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        End
    End Sub
    Sub bersih()
        txt_user.Text = ""
        txt_pass.Text = ""
        CheckBox1.Checked = False
        txt_user.Focus()
        SetWatermark(txt_user, "Username Anda")
        SetWatermark(txt_pass, "Password Anda")
    End Sub

    Private Sub txt_pass_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_pass.KeyPress
        If (e.KeyChar = Chr(13)) Then
            login()
        End If
    End Sub

    Sub login()
        cmd = New MySqlCommand("select * from pengguna where username='" & txt_user.Text & "' and password='" & txt_pass.Text & "' and aktif='aktif'", conn)
        rd = cmd.ExecuteReader
        If rd.HasRows Then
            MenuUtama.Show()
            Me.Hide()
            rd.Close()
            bersih()
        Else
            rd.Close()
            MessageBox.Show("Login Gagal, Username Dan Password Salah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txt_user.Focus()
        End If
    End Sub
End Class
