Imports MySql.Data.MySqlClient
Public Class Kecamatan
    Private Sub Kecamatan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksinya()
        autonumber()
        SetWatermark(txt_pass, "Kecamatan")
        bersih()
        view()
    End Sub
    Sub bersih()
        autonumber()
        SetWatermark(txt_pass, "Kecamatan")
        txt_pass.Text = ""
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        bersih()
    End Sub
    Sub autonumber()
        txt_user.Enabled = False
        Call koneksinya()
        cmd = New MySqlCommand("select * from kecamatan order by kode_kecamatan desc", conn)
        rd = cmd.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            txt_user.Text = "KC" + "0001"
        Else
            txt_user.Text = Val(Microsoft.VisualBasic.Mid(rd.Item("kode_kecamatan").ToString, 4, 3)) + 1
            If Len(txt_user.Text) = 1 Then
                txt_user.Text = "KC000" & txt_user.Text & ""
            ElseIf Len(txt_user.Text) = 2 Then
                txt_user.Text = "KC00" & txt_user.Text & ""
            ElseIf Len(txt_user.Text) = 3 Then
                txt_user.Text = "KC0" & txt_user.Text & ""
            End If
        End If
        rd.Close()
    End Sub

    Sub view()
        tampil(Me, "kecamatan", "")
        DataGridView1.DataSource = (ds.Tables("kecamatan"))
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        On Error GoTo msg
        If txt_user.Text = "" Or txt_pass.Text = "" Then
            MsgBox("Mohon Isi Dengan Lengkap")
            Exit Sub
        Else
            cmd = New MySqlCommand("select * from kecamatan where kode_kecamatan='" & txt_user.Text & "'", conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If Not rd.HasRows Then
                rd.Close()
                Dim sqlsave As String = "insert into kecamatan values('" & txt_user.Text & "', '" & txt_pass.Text & "')"
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
        txt_user.Text = DataGridView1.SelectedCells(0).Value
        txt_pass.Text = DataGridView1.SelectedCells(1).Value
        txt_user.Enabled = False
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        On Error GoTo pesan
        Dim kode As String
        kode = DataGridView1.SelectedCells(0).Value
        If MsgBox("anda yakin?", MsgBoxStyle.Question + MsgBoxStyle.OkCancel, "pesan") = MsgBoxResult.Ok Then
            cmd = New MySqlCommand _
            ("delete from kecamatan where kode_kecamatan = '" & kode & "'", conn)
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
        Else : Dim sqledit As String = "update kecamatan set kecamatan = '" & txt_pass.Text & "' where kode_kecamatan='" & txt_user.Text & "'"
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