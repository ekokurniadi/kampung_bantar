Imports MySql.Data.MySqlClient
Public Class Normalisasi

    Private Sub Normalisasi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksinya()
        view_normalisasi()
        view_rangking()
    End Sub


    Sub view_normalisasi()
        da = New MySqlDataAdapter("select c.kode_rating_kecocokan,b.kode_alternatif,a.nama_kampung,((b.bobot/2) +(b.bobot/2) * nilai_kepentingan)as nilai from rating_kecocokan_detail b join alternatif a on b.kode_alternatif=a.kode_alternatif join rating_kecocokan c on b.kode_rating_kecocokan=c.kode_rating_kecocokan where c.status ='input' ORDER BY b.kode_alternatif", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "rating_kecocokan_detail")
        DataGridView1.DataSource = (ds.Tables("rating_kecocokan_detail"))
        ''DataGridView1.Columns(0).Visible = False
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
    Sub generate_normalisasi()

    End Sub
    Sub generate_rangking()

    End Sub

    Private Sub DataGridView2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
       
    End Sub
    Sub tinggi()
        Dim tinggi, rendah, ratarata, total, banyak As Double

        Dim a, b As String
        tinggi = (From row As DataGridViewRow In DataGridView2.Rows Where row.Cells(3).FormattedValue.ToString() <> String.Empty Select Convert.ToDecimal(row.Cells(3).FormattedValue)).Max().ToString()
        rendah = (From row As DataGridViewRow In DataGridView1.Rows Where row.Cells(3).FormattedValue.ToString() <> String.Empty Select Convert.ToDecimal(row.Cells(3).FormattedValue)).Min().ToString()

        For baris As Integer = 0 To DataGridView2.RowCount - 2
            DataGridView2.Rows(baris).Cells(4).Value = baris + 1
        Next
    End Sub
  
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Call tinggi()
        Call rendah()
    End Sub
    Sub rendah()
        Dim tinggi, rendah, ratarata, total, banyak As Double
        rendah = (From row As DataGridViewRow In DataGridView1.Rows Where row.Cells(3).FormattedValue.ToString() <> String.Empty Select Convert.ToDecimal(row.Cells(3).FormattedValue)).Min().ToString()
    End Sub
End Class