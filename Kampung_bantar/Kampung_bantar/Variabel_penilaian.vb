Imports MySql.Data.MySqlClient
Public Class Variabel_penilaian

    Private Sub Variabel_penilaian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksinya()
        placeholder()
        autonumber()
        view()
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
    Sub placeholder()
        SetWatermark(txt_kode, "Kode")
        SetWatermark(txt_aspek_penilaian, "Aspek Penilaian")
        SetWatermark(cmb_kat, "Kategori Penilaian")
        SetWatermark(txt_kepentingan, "Tingkat Kepentingan")
        SetWatermark(txt_nilai_kep, "Nilai Kepentingan")
        SetWatermark(txt_cari, "Search Data")
    End Sub

End Class