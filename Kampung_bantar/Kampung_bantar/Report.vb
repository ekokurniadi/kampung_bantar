Imports MySql.Data.MySqlClient
Public Class Report
    Private Sub Report_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksinya()
        tahun()
        SetWatermark(ComboBox1, "Pilih Tahun")
    End Sub

    Sub tahun()
        Dim a As Integer = 2000
        For a = 2000 To Year(Now)
            ComboBox1.Items.Add(a)
        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim pilihan As String = ComboBox1.Text
        Dim link As String = "http://127.0.0.1/kampung_bantar/laporan_pdf/laporan_pertahun/"
        System.Diagnostics.Process.Start(link + pilihan)
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim pilihan As String = ComboBox1.Text
        Dim link As String = "http://127.0.0.1/kampung_bantar/laporan_pdf/download/"
        System.Diagnostics.Process.Start(link + pilihan)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        bersih()
    End Sub
    Sub bersih()
        ComboBox1.Text = ""
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
End Class