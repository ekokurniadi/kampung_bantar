Imports MySql.Data.MySqlClient
Public Class Variabel_penilaian

    Private Sub Variabel_penilaian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksinya()
        placeholder()
        autonumber()
        autonumber2()
        view()
        view_detail()
        initialCombobox()
    End Sub
    Private Sub initialCombobox()
        cmd = New MySqlCommand("select * from kategori_penilaian", conn)
        rd = cmd.ExecuteReader
        While rd.Read()
            cmb_kat.Items.Add(rd("kategori_penilaian"))
        End While
        rd.Close()
    End Sub
    Sub view()
        tampil(Me, "penilaian", "")
        dgvkampung.DataSource = (ds.Tables("penilaian"))
    End Sub
    Sub view_detail()
        da = New MySqlDataAdapter("select * from kriteria where kode_penilaian = '" & txt_kode.Text & "'", conn)
        ds = New DataSet
        ds.Clear()
        rd.Close()
        da.Fill(ds, "kriteria")
        DataGridView1.DataSource = (ds.Tables("kriteria"))
        DataGridView1.Columns(0).Visible = False
    End Sub

    Sub autonumber()
        txt_kode.Enabled = False
        Call koneksinya()
        cmd = New MySqlCommand("select * from penilaian order by kode_penilaian desc", conn)
        rd = cmd.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            txt_kode.Text = "VP" + "0001"
        Else
            txt_kode.Text = Val(Microsoft.VisualBasic.Mid(rd.Item("kode_penilaian").ToString, 4, 3)) + 1
            If Len(txt_kode.Text) = 1 Then
                txt_kode.Text = "VP000" & txt_kode.Text & ""
            ElseIf Len(txt_kode.Text) = 2 Then
                txt_kode.Text = "VP00" & txt_kode.Text & ""
            ElseIf Len(txt_kode.Text) = 3 Then
                txt_kode.Text = "VP0" & txt_kode.Text & ""
            End If
        End If
        rd.Close()
    End Sub

    Sub autonumber2()
        TextBox1.Enabled = False
        Call koneksinya()
        cmd = New MySqlCommand("select * from kriteria order by relasi desc", conn)
        rd = cmd.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            TextBox1.Text = "RL" + "0001"
        Else
            TextBox1.Text = Val(Microsoft.VisualBasic.Mid(rd.Item("relasi").ToString, 4, 3)) + 1
            If Len(TextBox1.Text) = 1 Then
                TextBox1.Text = "RL000" & TextBox1.Text & ""
            ElseIf Len(TextBox1.Text) = 2 Then
                TextBox1.Text = "RL00" & TextBox1.Text & ""
            ElseIf Len(txt_kode.Text) = 3 Then
                TextBox1.Text = "RL0" & TextBox1.Text & ""
            End If
        End If
        rd.Close()
    End Sub
    Sub placeholder()
        SetWatermark(txt_kode, "Kode")
        SetWatermark(txt_aspek_penilaian, "Aspek Penilaian")
        SetWatermark(cmb_kat, "Kategori Penilaian")
        SetWatermark(txt_kepentingan, "Tingkat Kepentingan")
        SetWatermark(txt_nilai_kep, "Nilai Kepentingan")
        SetWatermark(txt_cari, "Search Data")
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        On Error GoTo msg
        If txt_kode.Text = "" Or cmb_kat.Text = "" Then
            MsgBox("Mohon Isi Dengan Lengkap")
            Exit Sub
        Else
            cmd = New MySqlCommand("select * from penilaian where kode_penilaian='" & txt_kode.Text & "'", conn)
            rd.Close()
            rd = cmd.ExecuteReader()
            rd.Read()
            If Not rd.HasRows Then
                Dim sqlsave As String = "insert into penilaian values('" & txt_kode.Text & "', '" & cmb_kat.Text & "','" & txt_aspek_penilaian.Text & "','" & txt_kepentingan.Text & "','" & txt_nilai_kep.Text & "')"
                rd.Close()
                cmd = New MySqlCommand(sqlsave, conn)
                cmd.ExecuteNonQuery()
                view()
                view_detail()
                MsgBox("Data Berhasil Disimpan")
                bersih()

                DataGridView1.DataSource = ds.Tables()
            Else
                MsgBox("Gagal Menyimpan Data")
            End If
        End If
        Exit Sub
msg:    MsgBox("Opps, Something went wrong !!")
        bersih()
    End Sub
    Sub bersih()
        txt_kode.Text = ""
        cmb_kat.Text = ""
        txt_aspek_penilaian.Text = ""
        txt_kepentingan.Text = ""
        txt_nilai_kep.Text = ""
        txt_variabel.Text = ""
        txt_kriteria.Text = ""
        txt_bobot.Text = ""
        DataGridView1.DataSource = ds.Tables()
        autonumber()
    End Sub
    Sub bersih2()
        txt_kriteria.Text = ""
        txt_bobot.Text = ""
        autonumber2()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        On Error GoTo msg
        If txt_kode.Text = "" Or cmb_kat.Text = "" Or txt_variabel.Text = "" Or txt_kriteria.Text = "" Or txt_bobot.Text = "" Then
            MsgBox("Mohon Isi Dengan Lengkap")
            Exit Sub
        Else
            Dim sqlsave As String = "insert into kriteria values('""','" & txt_kode.Text & "', '" & txt_variabel.Text & "','" & txt_kriteria.Text & "','" & txt_bobot.Text & "','" & TextBox1.Text & "')"
            rd.Close()
            cmd = New MySqlCommand(sqlsave, conn)
            cmd.ExecuteNonQuery()
            view()
            view_detail()
            MsgBox("Data Berhasil Disimpan")
            bersih2()
        End If
        Exit Sub
msg:    MsgBox("Opps, Something went wrong !!")
        bersih2()
    End Sub
    Private Sub dgvkampung_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvkampung.DoubleClick
        On Error GoTo pesan
        If dgvkampung.SelectedCells(0).Value Is DBNull.Value Then
            MsgBox("Data Masih Kosong")
        Else
            txt_kode.Text = dgvkampung.SelectedCells(0).Value
            cmb_kat.Text = dgvkampung.SelectedCells(1).Value
            txt_aspek_penilaian.Text = dgvkampung.SelectedCells(2).Value
            txt_kepentingan.Text = dgvkampung.SelectedCells(3).Value
            txt_nilai_kep.Text = dgvkampung.SelectedCells(4).Value
            view_detail()
        End If
        Exit Sub
pesan:  MsgBox("Opps, Something went wrong !!")
    End Sub

    Private Sub DataGridView1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DataGridView1.Click
        If MsgBox("anda yakin ingin menghapus data ini?", MsgBoxStyle.Question + MsgBoxStyle.OkCancel, "pesan") = MsgBoxResult.Ok Then
            cmd = New MySqlCommand _
          ("delete from kriteria where id_kriteria = '" & DataGridView1.SelectedCells(0).Value & "'", conn)
            cmd.ExecuteNonQuery()
            MsgBox("Berhasil Hapus")
            view_detail()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        On Error GoTo pesan
        If txt_kode.Text = "" Or cmb_kat.Text = "" Or txt_aspek_penilaian.Text = "" Or txt_kepentingan.Text = "" Or txt_nilai_kep.Text = "" Then
            MsgBox("gagal menyimpan, cek ulang")
            Exit Sub
        Else
            Dim sqledit As String = "update penilaian set kategori = '" & cmb_kat.Text & "',variabel_penilaian = '" & txt_aspek_penilaian.Text & "',kepentingan = '" & txt_kepentingan.Text & "',nilai_kepentingan = '" & txt_nilai_kep.Text & "'  where kode_penilaian='" & txt_kode.Text & "'"
            cmd = New MySqlCommand(sqledit, conn)
            cmd.ExecuteNonQuery()
            MsgBox("berhasil diubah")
            view_detail()
            DataGridView1.DataSource = ds.Tables()
            view()
            bersih()
            txt_kode.Enabled = False
        End If
        Exit Sub
pesan:  MsgBox("Oops, Something went wrong !!")
        bersih()
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If MsgBox("anda yakin?", MsgBoxStyle.Question + MsgBoxStyle.OkCancel, "pesan") = MsgBoxResult.Ok Then
            cmd = New MySqlCommand _
          ("delete from penilaian where kode_penilaian = '" & txt_kode.Text & "'", conn)
            cmd.ExecuteNonQuery()
            MsgBox("Berhasil Hapus")
            view_detail()
            view()
            bersih()
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        bersih()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Me.Close()
        MenuUtama.Show()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class