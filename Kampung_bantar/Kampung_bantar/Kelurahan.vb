Imports MySql.Data.MySqlClient
Public Class Kelurahan
    Private Sub Kelurahan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksinya()
        autonumber()
        initialCombobox()
        SetWatermark(txt_camat, "Kecamatan")
        SetWatermark(txt_lurah, "Kelurahan")
        SetWatermark(cmb_camat, "Pilih Kecamatan")
        bersih()
        view()
    End Sub

    Private Sub initialCombobox()
        cmd = New MySqlCommand("select concat(kode_kecamatan,' - ',kecamatan) as kode from kecamatan", conn)
        rd = cmd.ExecuteReader
        While rd.Read()
            cmb_camat.Items.Add(rd("kode"))
        End While
        rd.Close()
    End Sub

    Sub bersih()
        autonumber()
        SetWatermark(txt_camat, "Kecamatan")
        SetWatermark(txt_lurah, "Kelurahan")
        SetWatermark(cmb_camat, "Pilih Kecamatan")
        txt_camat.Text = ""
        txt_lurah.Text = ""
        cmb_camat.Text = ""
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        bersih()
    End Sub
    Sub autonumber()
        txt_kode.Enabled = False
        Call koneksinya()
        cmd = New MySqlCommand("select * from kelurahan order by kode_kelurahan desc", conn)
        rd = cmd.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            txt_kode.Text = "KL" + "0001"
        Else
            txt_kode.Text = Val(Microsoft.VisualBasic.Mid(rd.Item("kode_kelurahan").ToString, 4, 3)) + 1
            If Len(txt_kode.Text) = 1 Then
                txt_kode.Text = "KL000" & txt_kode.Text & ""
            ElseIf Len(txt_kode.Text) = 2 Then
                txt_kode.Text = "KL00" & txt_kode.Text & ""
            ElseIf Len(txt_kode.Text) = 3 Then
                txt_kode.Text = "KL0" & txt_kode.Text & ""
            End If
        End If
        rd.Close()
    End Sub

    Sub view()
        tampil(Me, "kelurahan", "")
        DataGridView1.DataSource = (ds.Tables("kelurahan"))
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim pilihan As String = cmb_camat.Text.Substring(0, 6)
        On Error GoTo msg
        If txt_kode.Text = "" Or cmb_camat.Text = "" Then
            MsgBox("Mohon Isi Dengan Lengkap")
            Exit Sub
        Else
            cmd = New MySqlCommand("select * from kelurahan where kode_kelurahan='" & txt_kode.Text & "'", conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If Not rd.HasRows Then
                rd.Close()
                Dim sqlsave As String = "insert into kelurahan values('" & txt_kode.Text & "', '" & pilihan & "', '" & txt_camat.Text & "', '" & txt_lurah.Text & "')"
                cmd = New MySqlCommand(sqlsave, conn)
                rd.Close()
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
        txt_kode.Text = DataGridView1.SelectedCells(0).Value
        cmb_camat.Text = DataGridView1.SelectedCells(1).Value
        txt_camat.Text = DataGridView1.SelectedCells(2).Value
        txt_lurah.Text = DataGridView1.SelectedCells(3).Value
        txt_kode.Enabled = False
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        On Error GoTo pesan
        Dim kode As String
        kode = DataGridView1.SelectedCells(0).Value
        If MsgBox("anda yakin?", MsgBoxStyle.Question + MsgBoxStyle.OkCancel, "pesan") = MsgBoxResult.Ok Then
            cmd = New MySqlCommand _
            ("delete from kelurahan where kode_kelurahan = '" & kode & "'", conn)
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

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        MenuUtama.Show()
        Me.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim pilihan As String = cmb_camat.Text.Substring(0, 6)
        ''On Error GoTo pesan
        If txt_camat.Text = "" Or txt_lurah.Text = "" Then
            MsgBox("gagal menyimpan, cek ulang")
            Exit Sub
        Else : Dim sqledit As String = "update kelurahan set kecamatan = '" & txt_camat.Text & "',kode_kecamatan = '" & pilihan & "',kelurahan = '" & txt_lurah.Text & "' where kode_kelurahan='" & txt_kode.Text & "'"
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

    Private Sub cmb_camat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_camat.SelectedIndexChanged
        Dim pilihan As String = cmb_camat.Text.Substring(0, 6)
        rd.Close()
        cmd = New MySqlCommand("select * from kecamatan where kode_kecamatan='" & pilihan & "'", conn)
        rd = cmd.ExecuteReader
        rd.Read()
        If rd.HasRows Then
            txt_camat.Text = rd.Item("kecamatan")
            txt_camat.ReadOnly = True
        End If
        rd.Close()
    End Sub

End Class