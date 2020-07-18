Imports MySql.Data.MySqlClient
Public Class Rating_pencocokan

    Private Sub Rating_pencocokan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        kolombaru()
    End Sub
    Sub autonumber()
        txt_kode.Enabled = False
        Call koneksinya()
        cmd = New MySqlCommand("select * from rating_kecocokan order by kode desc", conn)
        rd = cmd.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            txt_kode.Text = "KP" + "0001"
        Else
            txt_kode.Text = Val(Microsoft.VisualBasic.Mid(rd.Item("kode").ToString, 4, 3)) + 1
            If Len(txt_kode.Text) = 1 Then
                txt_kode.Text = "KP000" & txt_kode.Text & ""
            ElseIf Len(txt_kode.Text) = 2 Then
                txt_kode.Text = "KP00" & txt_kode.Text & ""
            ElseIf Len(txt_kode.Text) = 3 Then
                txt_kode.Text = "KP0" & txt_kode.Text & ""
            End If
        End If
        rd.Close()
    End Sub
    Sub kolombaru()
        Call list_alternatif()
        Call list_kategori()
        DataGridView1.Columns.Add("Variabel Penilaian", "Variabel Penilaian")
        DataGridView1.Columns(2).Width = 300
        Call list_kriteria()
        DataGridView1.Columns.Add("Bobot", "Bobot")
    End Sub

    Sub list_kriteria()
        Call koneksinya()
        da = New MySqlDataAdapter("SELECT concat(relasi,' - ' , variabel_penilaian, ' - ', kriteria) as variabel_penilaian FROM kriteria", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds)
        Dim cols As New DataGridViewComboBoxColumn
        cols.DataSource = ds.Tables(0)
        cols.DisplayMember = "variabel_penilaian"
        DataGridView1.Columns.Add(cols)
        cols.HeaderText = "Pilih Kriteria"
        cols.Width = 200
    End Sub
    Sub list_kategori()
        Call koneksinya()
        da = New MySqlDataAdapter("SELECT concat(kode_penilaian,' - ' , kategori, ' - ', variabel_penilaian) as kat FROM penilaian", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds)
        Dim cols As New DataGridViewComboBoxColumn
        cols.DataSource = ds.Tables(0)
        cols.DisplayMember = "kat"
        DataGridView1.Columns.Add(cols)
        cols.HeaderText = "Pilih Kategori"
        cols.Width = 400
    End Sub
    Sub list_alternatif()
        Call koneksinya()
        da = New MySqlDataAdapter("SELECT concat(kode_alternatif,' - ',nama_kampung) as kode_alternatif from  alternatif", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds)
        Dim cols As New DataGridViewComboBoxColumn
        cols.DataSource = ds.Tables(0)
        cols.DisplayMember = "kode_alternatif"
        DataGridView1.Columns.Add(cols)
        cols.HeaderText = "Pilih Peserta"
        cols.Width = 200
    End Sub
    Private Sub DataGridView1_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        On Error Resume Next
        Dim pilihan As String = DataGridView1.Rows(e.RowIndex).Cells(1).Value.Substring(0, 6)
        If e.ColumnIndex = 1 Then
            cmd = New MySqlCommand("select * from penilaian where kode_penilaian='" & pilihan & "'", conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows Then
                DataGridView1.Rows(e.RowIndex).Cells(2).Value = rd.Item(2)
            Else
                MsgBox("Kode tidak terdaftar")
            End If
        End If

        rd.Close()

        On Error Resume Next
        Dim pilihan2 As String = DataGridView1.Rows(e.RowIndex).Cells(3).Value.Substring(0, 6)
        If e.ColumnIndex = 3 Then
            cmd = New MySqlCommand("select * from kriteria where relasi='" & pilihan2 & "'", conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows Then
                DataGridView1.Rows(e.RowIndex).Cells(4).Value = rd.Item(4)
            Else
                MsgBox("Kode tidak terdaftar")
            End If
        End If
        rd.Close()
    End Sub

End Class