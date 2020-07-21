Imports MySql.Data.MySqlClient
Public Class Report
    Private Sub Report_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksinya()
        tahun()
        tahun2()
        bulan()
    End Sub

    Sub tahun()
        Dim a As Integer = 2000
        For a = 2000 To Year(Now)
            ComboBox1.Items.Add(a)
        Next
    End Sub
    Sub tahun2()
        Dim a As Integer = 2000
        For a = 2000 To Year(Now)
            ComboBox2.Items.Add(a)
        Next
    End Sub
    Sub bulan()
        Dim a As Integer = 1
        Dim bulan As String
        For a = 1 To 12
            If a = 1 Then
                bulan = "Januari"
            ElseIf a = 2 Then
                bulan = "Februari"
            ElseIf a = 3 Then
                bulan = "Maret"
            ElseIf a = 4 Then
                bulan = "April"
            ElseIf a = 5 Then
                bulan = "Mei"
            ElseIf a = 6 Then
                bulan = "Juni"
            ElseIf a = 7 Then
                bulan = "Juli"
            ElseIf a = 8 Then
                bulan = "Agustus"
            ElseIf a = 9 Then
                bulan = "September"
            ElseIf a = 10 Then
                bulan = "Oktober"
            ElseIf a = 11 Then
                bulan = "November"
            ElseIf a = 12 Then
                bulan = "Desember"
            End If
            If Len(a) = 1 Then
                a = 1 + a.ToString
            Else
                a = a
            End If
            ComboBox4.Items.Add(a.ToString + " - " + bulan)
        Next
    End Sub
End Class