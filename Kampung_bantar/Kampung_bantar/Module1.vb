Imports MySql.Data.MySqlClient

Module Module1
    Public conn As MySqlConnection
    Public da As MySqlDataAdapter
    Public ds As DataSet
    Public cmd As MySqlCommand
    Public cmd2 As MySqlCommand
    Public cmd3 As MySqlCommand
    Public rd As MySqlDataReader
    Public str As String
    Public Sub koneksinya()
        str = "server=localhost;uid=root;password=;database=spk_bantar;Convert Zero Datetime=True;"
        conn = New MySqlConnection(str)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
    End Sub
    Sub tampil(ByVal frm As Form, ByVal table As String, ByVal param As String)
        da = New MySqlDataAdapter("select * from " & table & "", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "" & table & "")
    End Sub
    Private Declare Function GetWindow Lib "user32.dll" (ByVal hwnd As Integer, ByVal wCmd As Integer) As Integer
    Private Declare Auto Function SendMessageString Lib "user32.dll" Alias "SendMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lparam As String) As Integer
    Const GW_CHILD = 5
    Const EM_SETCUEBANNER = &H1501
    Sub SetWatermark(ByVal Ctl As TextBox, ByVal Txt As String)
        SendMessageString(Ctl.Handle, EM_SETCUEBANNER, 1, Txt)
    End Sub
    Sub SetWatermark(ByVal Ctl As RichTextBox, ByVal Txt As String)
        SendMessageString(Ctl.Handle, EM_SETCUEBANNER, 1, Txt)
    End Sub

    Sub SetWatermark(ByVal Ctl As ComboBox, ByVal Txt As String)
        Dim i As Integer
        i = GetWindow(Ctl.Handle, GW_CHILD)

        SendMessageString(i, EM_SETCUEBANNER, 1, Txt)
    End Sub
End Module

