﻿Imports MySql.Data.MySqlClient
Public Class Rating_pencocokan

    Private Sub Rating_pencocokan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        autonumber()
        kolombaru()
        initialCombobox()
    End Sub
    Sub atur_kolom()
        DataGridView1.Columns(0).Width = 150
        DataGridView1.Columns(1).Width = 550
        DataGridView1.Columns(2).Width = 400
        DataGridView1.Columns(3).Width = 550
        DataGridView1.Columns(4).Width = 150
        DataGridView1.Columns(2).Visible = False
    End Sub

    Private Sub initialCombobox()
        cmd = New MySqlCommand("SELECT concat(kode_alternatif,' - ',nama_kampung) as kode_alternatif from  alternatif", conn)
        rd = cmd.ExecuteReader
        While rd.Read()
            ComboBox1.Items.Add(rd("kode_alternatif"))
        End While
        rd.Close()
    End Sub

    Sub autonumber()
        txt_kode.ReadOnly = False
        Call koneksinya()
        cmd = New MySqlCommand("select * from rating_kecocokan order by kode_rating_kecocokan desc", conn)
        rd = cmd.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            txt_kode.Text = "RK" + "0001"
        Else
            txt_kode.Text = Val(Microsoft.VisualBasic.Mid(rd.Item("kode_rating_kecocokan").ToString, 4, 3)) + 1
            If Len(txt_kode.Text) = 1 Then
                txt_kode.Text = "RK000" & txt_kode.Text & ""
            ElseIf Len(txt_kode.Text) = 2 Then
                txt_kode.Text = "RK00" & txt_kode.Text & ""
            ElseIf Len(txt_kode.Text) = 3 Then
                txt_kode.Text = "RK0" & txt_kode.Text & ""
            End If
        End If
        rd.Close()
    End Sub
    Sub kolombaru()

        Call list_alternatif()
        'Dim chk As New DataGridViewCheckBoxColumn()
        'DataGridView1.Columns.Add(chk)
        'chk.HeaderText = "Pilih"
        'chk.Name = "chk"
        'DataGridView1.Rows(0).Cells(0).Value = False
        Call list_kategori()
        DataGridView1.Columns.Add("Variabel Penilaian", "Variabel Penilaian")
        Call list_kriteria()
        DataGridView1.Columns.Add("Bobot", "Bobot")
        DataGridView1.Columns.Add("Nilai Kepentingan", "Nilai")
        DataGridView1.Columns.Add("Kode Penilaian", "Nilai")
        DataGridView1.Columns(5).Visible = False
        DataGridView1.Columns(6).Visible = False
        atur_kolom()
        view()
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
        DataGridView1.Columns(0).Visible = False
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
                DataGridView1.Rows(e.RowIndex).Cells(5).Value = rd.Item(4)
                DataGridView1.Rows(e.RowIndex).Cells(6).Value = rd.Item(0)
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

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        simpan()
    End Sub

    Sub simpan()
        MsgBox("Data Berhasil disimpan")
        Normalisasi.Show()
        Normalisasi.txt_kode.Text = txt_kode.Text
        Normalisasi.DateTimePicker1.Text = DateTimePicker1.Value
        rd.Close()
        DataGridView1.Columns.Clear()
        DataGridView2.Columns.Clear()
        Call kolombaru()
        Call autonumber()
        Call bersih()
    End Sub

    Sub view()
        da = New MySqlDataAdapter("select c.kode_rating_kecocokan,b.kode_alternatif,d.variabel_penilaian,d.kriteria,a.nama_kampung,b.bobot from rating_kecocokan_detail b join alternatif a on b.kode_alternatif=a.kode_alternatif join rating_kecocokan c on b.kode_rating_kecocokan=c.kode_rating_kecocokan join kriteria d on b.relasi_kriteria =d.relasi where c.kode_rating_kecocokan = '" & txt_kode.Text & "' and c.status ='input' ORDER BY b.kode_alternatif", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "rating_kecocokan_detail")
        DataGridView2.DataSource = (ds.Tables("rating_kecocokan_detail"))
        DataGridView2.Columns(0).Visible = False
    End Sub
    Sub bersih()
        DateTimePicker1.Value = Now
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Me.Close()
        MenuUtama.Show()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        DataGridView1.Columns.Clear()
        Call kolombaru()
        Call autonumber()
        Call bersih()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If txt_kode.Text = "" Or DateTimePicker1.Text = "" Then
            MsgBox("Data belum lengkap")
            Exit Sub
        End If
        Dim sqlsave As String = "insert into rating_kecocokan values('" & txt_kode.Text & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "','input','""')"
        cmd = New MySqlCommand(sqlsave, conn)
        cmd.ExecuteNonQuery()
        For baris As Integer = 0 To DataGridView1.Rows.Count - 2
            'simpan ke tabel detail
            Dim simpanmaster As String = "insert into rating_kecocokan_detail values('""','" & txt_kode.Text & "','" & ComboBox1.Text.Substring(0, 6) & "','" & DataGridView1.Rows(baris).Cells(3).Value.Substring(0, 6) & "','" & DataGridView1.Rows(baris).Cells(4).Value & "','" & DataGridView1.Rows(baris).Cells(5).Value & "','" & DataGridView1.Rows(baris).Cells(6).Value & "')"
            cmd = New MySqlCommand(simpanmaster, conn)
            cmd.ExecuteNonQuery()
            rd.Close()
        Next baris
        MsgBox("Data Berhasil ditambahkan")
        rd.Close()
        DataGridView1.Columns.Clear()
        Call kolombaru()
        Call bersih()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Label17.Text = TimeOfDay
    End Sub

    Private Sub DataGridView2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub
End Class