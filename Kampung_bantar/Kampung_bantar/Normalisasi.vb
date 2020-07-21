Imports MySql.Data.MySqlClient
Public Class Normalisasi

    Private Sub Normalisasi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksinya()
        view_normalisasi()
        view_rangking()
    End Sub

    Sub view_normalisasi()
        da = New MySqlDataAdapter("select c.kode_rating_kecocokan,b.kode_alternatif,d.variabel_penilaian,d.kriteria,a.nama_kampung,((b.bobot/2) +(b.bobot/2) * nilai_kepentingan)as nilai from rating_kecocokan_detail b join alternatif a on b.kode_alternatif=a.kode_alternatif join rating_kecocokan c on b.kode_rating_kecocokan=c.kode_rating_kecocokan join kriteria d on b.relasi_kriteria =d.relasi where c.status ='input' ORDER BY b.kode_alternatif", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "rating_kecocokan_detail")
        DataGridView1.DataSource = (ds.Tables("rating_kecocokan_detail"))
        DataGridView1.Columns(0).Visible = False
    End Sub
    Sub view_rangking()
        da = New MySqlDataAdapter("select c.kode_rating_kecocokan,b.kode_alternatif,a.nama_kampung,sum((b.bobot/2) +(b.bobot/2) * nilai_kepentingan)as nilai from rating_kecocokan_detail b join alternatif a on b.kode_alternatif=a.kode_alternatif join rating_kecocokan c on b.kode_rating_kecocokan=c.kode_rating_kecocokan where c.status ='input' group by b.kode_alternatif ORDER BY nilai DESC", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "rating_kecocokan_detail")
        DataGridView2.DataSource = (ds.Tables("rating_kecocokan_detail"))
        DataGridView2.Columns.Add("Peringkat", "Peringkat")
        DataGridView2.Columns(0).Visible = False
        tinggi()
    End Sub
    Sub tinggi()
        Dim tinggi, rendah, ratarata, total, banyak As Double
        On Error Resume Next
        Dim a, b As String
        tinggi = (From row As DataGridViewRow In DataGridView2.Rows Where row.Cells(3).FormattedValue.ToString() <> String.Empty Select Convert.ToDecimal(row.Cells(3).FormattedValue)).Max().ToString()
        rendah = (From row As DataGridViewRow In DataGridView2.Rows Where row.Cells(3).FormattedValue.ToString() <> String.Empty Select Convert.ToDecimal(row.Cells(3).FormattedValue)).Min().ToString()

        For baris As Integer = 0 To DataGridView2.RowCount - 2
            DataGridView2.Rows(baris).Cells(4).Value = baris + 1
        Next
    End Sub
  
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If txt_kode.Text = "" Or DateTimePicker1.Text = "" Then
            MsgBox("Data belum lengkap")
            Exit Sub
        End If
        cmd = New MySqlCommand("select * from normalisasi where kode_normalisasi='" & txt_kode.Text & "'", conn)
        rd = cmd.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            rd.Close()
            Dim sqledit As String = "update rating_kecocokan set status = 'selesai' where kode_rating_kecocokan='" & txt_kode.Text & "'"
            cmd = New MySqlCommand(sqledit, conn)
            cmd.ExecuteNonQuery()
            Dim simpannormalisasi As String = "insert into normalisasi values('" & txt_kode.Text & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "')"
            cmd2 = New MySqlCommand(simpannormalisasi, conn)
            cmd2.ExecuteNonQuery()
            rd.Close()
            Dim simpanrangking As String = "insert into perangkingan values('" & txt_kode.Text & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "')"
            cmd3 = New MySqlCommand(simpanrangking, conn)
            cmd3.ExecuteNonQuery()
            rd.Close()
        End If
        For baris As Integer = 0 To DataGridView1.Rows.Count - 2
            Dim nilai_normal As String = DataGridView1.Rows(baris).Cells(5).Value
            Dim n As String = nilai_normal.Replace(",", ".")
            'simpan ke tabel detail
            Dim simpan_detail_normalisasi As String = "insert into normalisasi_detail values('""','" & txt_kode.Text & "','" & DataGridView1.Rows(baris).Cells(1).Value & "','" & DataGridView1.Rows(baris).Cells(2).Value & "','" & DataGridView1.Rows(baris).Cells(3).Value & "','" & n & "')"
            cmd4 = New MySqlCommand(simpan_detail_normalisasi, conn)
            rd.Close()
            cmd4.ExecuteNonQuery()
        Next baris
        For baris As Integer = 0 To DataGridView2.Rows.Count - 2
            Dim nilai As String = DataGridView2.Rows(baris).Cells(3).Value
            Dim nn As String = nilai.Replace(",", ".")
            'simpan ke tabel detail
            Dim simpan_detail_rangking As String = "insert into perangkingan_detail values('""','" & txt_kode.Text & "','" & DataGridView2.Rows(baris).Cells(1).Value & "','" & nn & "','" & DataGridView2.Rows(baris).Cells(4).Value & "')"
            cmd5 = New MySqlCommand(simpan_detail_rangking, conn)
            cmd5.ExecuteNonQuery()
            rd.Close()
        Next baris
        MsgBox("Data Berhasil disimpan")
        cetak()
        rd.Close()
        DataGridView1.Columns.Clear()
        DataGridView2.Columns.Clear()
        txt_kode.Clear()

    End Sub
    Sub cetak()
        Dim id As String = txt_kode.Text
        Dim link As String = "http://127.0.0.1/kampung_bantar/laporan_pdf/cetak_hasil_penilaian/"
        System.Diagnostics.Process.Start(link + id)

    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Me.Close()
        MenuUtama.Show()
    End Sub
End Class