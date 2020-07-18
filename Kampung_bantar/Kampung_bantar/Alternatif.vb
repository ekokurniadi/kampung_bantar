﻿Imports MySql.Data.MySqlClient

Public Class Alternatif
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Label5.Text = Microsoft.VisualBasic.Right(Label5.Text, 1) + Microsoft.VisualBasic.Left(Label5.Text, Len(Label5.Text) - 1)
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Label17.Text = TimeOfDay()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim pilihan As String = ComboBox1.Text.Substring(0, 6)
        On Error GoTo msg
        If txt_kode.Text = "" Or ComboBox1.Text = "" Then
            MsgBox("Mohon Isi Dengan Lengkap")
            Exit Sub
        Else
            cmd = New MySqlCommand("select * from alternatif where kode_alternatif='" & txt_kode.Text & "'", conn)
            rd.Close()
            rd = cmd.ExecuteReader()
            rd.Read()
            If Not rd.HasRows Then
                Dim sqlsave As String = "insert into alternatif values('" & txt_kode.Text & "', '" & pilihan & "','" & txt_nama.Text & "')"
                rd.Close()
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
msg:    MsgBox("Opps, Something went wrong !!")
        bersih()
    End Sub
    Private Sub initialCombobox()
        cmd = New MySqlCommand("select concat(kode,' - ',nama,' - ',kecamatan) as kode from kampung", conn)
        rd = cmd.ExecuteReader
        While rd.Read()
            ComboBox1.Items.Add(rd("kode"))
        End While
        rd.Close()
    End Sub

    Private Sub Kampung_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksinya()
        placeholder()
        autonumber()
        view()
        initialCombobox()
    End Sub
    Sub bersih()
        placeholder()
        autonumber()
        txt_cari.Text = ""
        ComboBox1.Text = ""
        txt_nama.Text = ""
    End Sub
    Sub view()
        tampil(Me, "alternatif", "")
        dgvkampung.DataSource = (ds.Tables("alternatif"))
    End Sub

    Sub autonumber()
        txt_kode.Enabled = False
        Call koneksinya()
        cmd = New MySqlCommand("select * from alternatif order by kode_alternatif desc", conn)
        rd = cmd.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            txt_kode.Text = "AL" + "0001"
        Else
            txt_kode.Text = Val(Microsoft.VisualBasic.Mid(rd.Item("kode_alternatif").ToString, 4, 3)) + 1
            If Len(txt_kode.Text) = 1 Then
                txt_kode.Text = "AL000" & txt_kode.Text & ""
            ElseIf Len(txt_kode.Text) = 2 Then
                txt_kode.Text = "AL00" & txt_kode.Text & ""
            ElseIf Len(txt_kode.Text) = 3 Then
                txt_kode.Text = "AL0" & txt_kode.Text & ""
            End If
        End If
        rd.Close()
    End Sub
    Sub placeholder()
        SetWatermark(txt_kode, "Kode")
        SetWatermark(ComboBox1, "Select an Option")
        SetWatermark(txt_nama, "Nama Kampung")
        SetWatermark(txt_cari, "Search Data")
    End Sub

    Private Sub txt_cari_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_cari.TextChanged
        da = New MySqlDataAdapter("select * from alternatif where kode_alternatif like '" & txt_cari.Text & "%' or nama_kampung like '" & txt_cari.Text & "%' or kode_kampung like '" & txt_cari.Text & "%' ", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "alternatif")
        dgvkampung.DataSource = (ds.Tables("alternatif"))
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim pilihan As String = ComboBox1.Text.Substring(0, 6)
        On Error GoTo pesan
        If txt_kode.Text = "" Or ComboBox1.Text = "" Then
            MsgBox("gagal menyimpan, cek ulang")
            Exit Sub
        Else
            Dim sqledit As String = "update alternatif set nama_kampung = '" & txt_nama.Text & _
                   "',kode_kampung = '" & pilihan & "' where kode_alternatif='" & txt_kode.Text & "'"
            cmd = New MySqlCommand(sqledit, conn)
            cmd.ExecuteNonQuery()
            MsgBox("berhasil diubah")
            view()
            bersih()
            txt_kode.Enabled = False
        End If
        Exit Sub
pesan:  MsgBox("ada kesalahan, mohon ulangi")
        bersih()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        On Error GoTo pesan
        Dim kode As String
        kode = dgvkampung.SelectedCells(0).Value
        If MsgBox("anda yakin?", MsgBoxStyle.Question + MsgBoxStyle.OkCancel, "pesan") = MsgBoxResult.Ok Then
            cmd = New MySqlCommand _
            ("delete from alternatif where kode_alternatif = '" & kode & "'", conn)
            cmd.ExecuteNonQuery()
            MsgBox("data terhapus", MsgBoxStyle.Information, "pesan")
            view()
            bersih()
            txt_kode.Enabled = False
        End If
        Exit Sub
pesan:  MsgBox("ada kesalahan, mohon ulangi")
        bersih()
    End Sub

    Private Sub dgvkampung_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvkampung.DoubleClick
        txt_kode.Text = dgvkampung.SelectedCells(0).Value
        ComboBox1.Text = dgvkampung.SelectedCells(1).Value
        txt_nama.Text = dgvkampung.SelectedCells(2).Value

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        bersih()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Me.Close()
        MenuUtama.Show()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim pilihan As String = ComboBox1.Text.Substring(0, 6)
        rd.Close()
        cmd = New MySqlCommand("select * from kampung where kode='" & pilihan & "'", conn)
        rd = cmd.ExecuteReader
        rd.Read()
        If rd.HasRows Then
            txt_nama.Text = rd.Item("nama")
            txt_nama.ReadOnly = True
        End If
    End Sub
End Class