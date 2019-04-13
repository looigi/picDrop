Imports System.Data.SqlServerCe
Imports System.IO
Imports System.Threading

Public Class Utility
    Private varConnessione As SQLSERVERCE
    Private conn As Object

    Private Sub cmdUscita_Click_1(sender As Object, e As EventArgs) Handles cmdUscita.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()

        frmMain.ApreConnessione()
        frmMain.Show()
    End Sub

    Private Sub cmdRiprovaSitiA0_Click(sender As Object, e As EventArgs) Handles cmdRiprovaSitiA0.Click
        Me.Cursor = Cursors.WaitCursor
        Dim sql As String = "Update LinksDaScaricare Set Scaricato='N' Where Quanti=0"
        varConnessione.EsegueSql(conn, sql)
        Me.Cursor = Cursors.Default

        MsgBox("Siti re-impostati")
    End Sub

    Public Sub ImpostaConnessione(vc As SQLSERVERCE, c As Object)
        varConnessione = vc
        conn = c

        lblAvanzamento.Text = ""
    End Sub

    Private Sub cmdBonifica_Click(sender As Object, e As EventArgs) Handles cmdBonifica.Click
        Dim sql As String = ""
        Dim rec As Object = CreateObject("ADODB.Recordset")
        Dim Sito As String
        Dim SitoOr As String
        Dim Quanti As Integer = 0
        Dim Tot As Long = 0
        Dim Quale As Long = 0
        Dim Modif As Boolean
        Dim p As New picDropCls

        Me.Cursor = Cursors.WaitCursor
        sql = "Select Count(*) From LinksDaScaricare Where Quanti=0"
        rec = varConnessione.LeggeQuery(conn, sql)
        If rec(0).Value Is DBNull.Value = True Then
            Tot = 0
        Else
            Tot = rec(0).Value
        End If
        rec.Close()

        sql = "Select * From LinksDaScaricare Where Quanti=0"
        rec = varConnessione.LeggeQuery(conn, sql)
        Do Until rec.Eof
            Sito = rec("Link").Value
            SitoOr = Sito
            Modif = False

            If Sito.IndexOf("?") > -1 And Sito.IndexOf(".") < Sito.IndexOf("?") Then
                Modif = True
                Sito = Mid(Sito, 1, Sito.IndexOf("?"))
            End If

            If Sito.IndexOf("%") > -1 Then
                Modif = True
                Sito = p.TogliePerc(Sito)
            End If

            If Modif = True Then
                sql = "Update LinksDaScaricare Set Link='" & Sito.Replace("'", "''") & "' Where Link='" & SitoOr.Replace("'", "''") & "'"
                varConnessione.EsegueSql(conn, sql)

                Quanti += 1
            End If

            Quale += 1
            If Quale / 100 = Int(Quale / 100) Then
                lblAvanzamento.Text = "Indirizzi bonificati: " & Quanti & " - " & Quale & "/" & Tot
                Application.DoEvents()
            End If

            rec.MoveNext()
        Loop
        rec.Close()
        p = Nothing
        Me.Cursor = Cursors.Default

        MsgBox("Indirizzi bonificati: " & Quanti)
        lblAvanzamento.Text = ""
    End Sub

    Private Sub cmdResetPosizione_Click(sender As Object, e As EventArgs) Handles cmdResetPosizione.Click
        Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height

        SaveSetting("picDrop", "Settaggi", "PosX", (screenWidth / 2) - (frmMain.Width / 2))
        SaveSetting("picDrop", "Settaggi", "PosY", (screenHeight / 2) - (frmMain.Height / 2))

        MsgBox("Posizione resettata", vbInformation)
    End Sub

    Private Sub cmdCompattaDB_Click(sender As Object, e As EventArgs) Handles cmdCompattaDB.Click
        Dim Dime1 As Integer
        Dim Dime2 As Integer

        Me.Cursor = Cursors.WaitCursor

        frmMain.ChiudeConnessione()

        Dime1 = FileLen(Application.StartupPath & "\Db\picDrop.sdf")

        Dim esec As String = Application.StartupPath & "\SqlCeCmd40.exe"
        Dim param As String = " -d ""Data Source=" & Application.StartupPath & "\Db\picDrop.sdf"" -e compact"
        'Dim cmdinfo As ProcessStartInfo = New ProcessStartInfo(esec)
        'cmdinfo.CreateNoWindow = True
        'cmdinfo.UseShellExecute = False
        'Process.Start(cmdinfo)
        RunCommandCom(esec, param, False)

        'While Not File.Exists(Application.StartupPath & "\Out.txt")
        '    Thread.Sleep(1000)
        '    Application.DoEvents()
        'End While

        frmMain.ApreConnessione()

        Me.Cursor = Cursors.Default

        Dime2 = FileLen(Application.StartupPath & "\Db\picDrop.sdf")

        MsgBox("Database compattato:" & vbCrLf & vbCrLf & "Dimensioni originarie: " & Dime1 & vbCrLf & "Dimensioni compattate: " & Dime2, vbInformation)
    End Sub

    Private Sub RunCommandCom(command As String, arguments As String, permanent As Boolean)
        Dim p As Process = New Process()
        Dim pi As ProcessStartInfo = New ProcessStartInfo()
        pi.Arguments = " " + If(permanent = True, "/K", "/C") + " " + command + " " + arguments
        pi.FileName = "cmd.exe"
        pi.UseShellExecute = False
        pi.CreateNoWindow = True
        p.StartInfo = pi
        p.Start()
        p.WaitForExit()
    End Sub
End Class