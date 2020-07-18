Imports MySql.Data.MySqlClient

Public Class Kategori
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Label5.Text = Microsoft.VisualBasic.Right(Label5.Text, 1) + Microsoft.VisualBasic.Left(Label5.Text, Len(Label5.Text) - 1)
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Label17.Text = TimeOfDay()
    End Sub
    Private Sub Kategori_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksinya()
        placeholder()
        autonumber()
        view()
    End Sub
    Sub bersih()
        placeholder()
        autonumber()
        txt_cari.Text = ""
        txt_nama.Text = ""
    End Sub
    Sub view()
        tampil(Me, "kategori_penilaian", "")
        dgvkampung.DataSource = (ds.Tables("kategori_penilaian"))
        dgvkampung.Columns(0).Visible = False
    End Sub

    Sub autonumber()
        txt_kode.Enabled = False
        Call koneksinya()
        cmd = New MySqlCommand("select * from kategori_penilaian order by id desc", conn)
        rd = cmd.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            txt_kode.Text = 1
        Else
            txt_kode.Text = Val(Microsoft.VisualBasic.Mid(rd.Item("id").ToString, 1, 1)) + 1
            If Len(txt_kode.Text) = 1 Then
                txt_kode.Text = "" & txt_kode.Text & ""
            ElseIf Len(txt_kode.Text) = 2 Then
                txt_kode.Text = "" & txt_kode.Text & ""
            ElseIf Len(txt_kode.Text) = 3 Then
                txt_kode.Text = "" & txt_kode.Text & ""
            End If
        End If
        rd.Close()
    End Sub
    Sub placeholder()
        SetWatermark(txt_kode, "Kode")
        SetWatermark(txt_nama, "Kategori Penilaian")
        SetWatermark(txt_cari, "Search Data")
    End Sub

    Private Sub txt_cari_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_cari.TextChanged
        da = New MySqlDataAdapter("select * from kategori_penilaian where id like '" & txt_cari.Text & "%' or kategori_penilaian like '" & txt_cari.Text & "%' ", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "kategori_penilaian")
        dgvkampung.DataSource = (ds.Tables("kategori_penilaian"))
        dgvkampung.Columns(0).Visible = False
    End Sub
    Private Sub dgvkampung_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvkampung.DoubleClick
        txt_kode.Text = dgvkampung.SelectedCells(0).Value
        txt_nama.Text = dgvkampung.SelectedCells(1).Value

    End Sub
    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        On Error GoTo msg
        If txt_kode.Text = "" Then
            MsgBox("Mohon Isi Dengan Lengkap")
            Exit Sub
        Else
            cmd = New MySqlCommand("select * from kategori_penilaian where id='" & txt_kode.Text & "'", conn)
            rd.Close()
            rd = cmd.ExecuteReader()
            rd.Read()
            If Not rd.HasRows Then
                Dim sqlsave As String = "insert into kategori_penilaian values('" & txt_kode.Text & "','" & txt_nama.Text & "')"
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

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        On Error GoTo pesan
        If txt_kode.Text = "" Then
            MsgBox("gagal menyimpan, cek ulang")
            Exit Sub
        Else
            Dim sqledit As String = "update kategori_penilaian set kategori_penilaian = '" & txt_nama.Text & _
                   "' where id='" & txt_kode.Text & "'"
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

    Private Sub Button5_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        On Error GoTo pesan
        Dim kode As String
        kode = dgvkampung.SelectedCells(0).Value
        If MsgBox("anda yakin?", MsgBoxStyle.Question + MsgBoxStyle.OkCancel, "pesan") = MsgBoxResult.Ok Then
            cmd = New MySqlCommand _
            ("delete from kategori_penilaian where id = '" & kode & "'", conn)
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

    Private Sub Button4_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        bersih()
    End Sub

    Private Sub Button7_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Me.Close()
        MenuUtama.Show()
    End Sub
End Class