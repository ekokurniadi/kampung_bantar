Imports MySql.Data.MySqlClient
Public Class Form2
    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksinya()
        SetWatermark(txt_user, "Username Anda")
        SetWatermark(txt_pass, "Password Anda")
        SetWatermark(ComboBox1, "Select an option")
        bersih()
        view()
    End Sub
    Sub bersih()
        txt_user.Enabled = True
        txt_user.Text = ""
        txt_pass.Text = ""
        SetWatermark(txt_user, "Username Anda")
        SetWatermark(txt_pass, "Password Anda")
        SetWatermark(ComboBox1, "Select an option")
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        bersih()
    End Sub
    Sub view()
        tampil(Me, "pengguna", "")
        DataGridView1.DataSource = (ds.Tables("pengguna"))
        DataGridView1.Columns(0).Visible = False
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        On Error GoTo msg
        If txt_user.Text = "" Or txt_pass.Text = "" Then
            MsgBox("Mohon Isi Dengan Lengkap")
            Exit Sub
        Else
            cmd = New MySqlCommand("select * from pengguna where username='" & txt_user.Text & "'", conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If Not rd.HasRows Then
                rd.Close()
                Dim sqlsave As String = "insert into pengguna values('""','" & txt_user.Text & "', '" & txt_pass.Text & "', '" & ComboBox1.Text & "')"
                cmd = New MySqlCommand(sqlsave, conn)
                cmd.ExecuteNonQuery()
                view()
                MsgBox("Data Berhasil Disimpan")
                bersih()
            Else
                MsgBox("Gagal Menyimpan Data")
            End If
        End If
        Exit Sub
msg:    MsgBox("Ada Kesalahan, Mohon Ulangi")
        bersih()
    End Sub
    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        txt_user.Text = DataGridView1.SelectedCells(1).Value
        txt_pass.Text = DataGridView1.SelectedCells(2).Value
        ComboBox1.Text = DataGridView1.SelectedCells(3).Value
        txt_user.Enabled = False
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        On Error GoTo pesan
        Dim kode As String
        kode = DataGridView1.SelectedCells(0).Value
        If MsgBox("anda yakin?", MsgBoxStyle.Question + MsgBoxStyle.OkCancel, "pesan") = MsgBoxResult.Ok Then
            cmd = New MySqlCommand _
            ("delete from pengguna where username = '" & kode & "'", conn)
            cmd.ExecuteNonQuery()
            MsgBox("data terhapus", MsgBoxStyle.Information, "pesan")
            view()
            bersih()
            txt_user.Enabled = True
        End If
        Exit Sub
pesan:  MsgBox("ada kesalahan, mohon ulangi")
        bersih()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        MenuUtama.Show()
        Me.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        On Error GoTo pesan
        If txt_user.Text = "" Or txt_pass.Text = "" Then
            MsgBox("gagal menyimpan, cek ulang")
            Exit Sub
        Else : Dim sqledit As String = "update pengguna set password = '" & txt_pass.Text & "',aktif = '" & ComboBox1.Text & "' where username='" & txt_user.Text & "'"
            cmd = New MySqlCommand(sqledit, conn)
            cmd.ExecuteNonQuery()
            MsgBox("berhasil diubah")
            view()
            bersih()
        End If
        Exit Sub
pesan:  MsgBox("ada kesalahan, mohon ulangi")
        bersih()
    End Sub
End Class