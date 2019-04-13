Public Class frmListaDownload

    Private Sub frmListaDownload_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        ListaDownloadVisibile = False
    End Sub

    Private Sub frmListaDownload_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Quale As String = GetSetting("picDrop", "Settaggi", "LinksImmagini", "Links")
        If Quale = "Links" Then
            optLinks.Checked = True
            optImmagini.Checked = False
        Else
            optLinks.Checked = False
            optImmagini.Checked = True
        End If

        ImpostaMaschera()

        CaricaLista()
    End Sub

    Private Sub CaricaLista()
        Me.Cursor = Cursors.WaitCursor

        Dim varConnessione As SQLSERVERCE
        Dim conn As Object = CreateObject("ADODB.Connection")

        varConnessione = New SQLSERVERCE
        varConnessione.ImpostaNomeDB(Application.StartupPath & "\DB\picDrop.sdf")
        varConnessione.LeggeImpostazioniDiBase()
        conn = varConnessione.ApreDB()

        If optLinks.Checked Then
            Dim Altro As String = ""
            Dim Altro2 As String = ""
            If txtFiltro.Text <> "" Then
                Altro = " And Link Like '%" & txtFiltro.Text.Replace("'", "''") & "%'"
            End If

            Dim sql As String = "Select * From LinksDaScaricare Where (Scaricato='N' Or Scaricato='B') " & Altro
            Dim rec As Object = CreateObject("ADODB.Recordset")
            Dim Quanti As Integer = 0

            lstListaDownload.Items.Clear()
            rec = varConnessione.LeggeQuery(conn, sql)
            Do Until rec.Eof
                If rec(1).Value = "B" Then
                    Altro2 = "BLOCCATO"
                Else
                    Altro2 = "        "
                End If
                lstListaDownload.Items.Add(Altro2 & ";" & rec(0).Value)
                Quanti += 1
                If Quanti / 100 = Int(Quanti / 100) Then
                    Application.DoEvents()
                End If

                rec.MoveNext()
            Loop
            rec.Close()

            conn.close()
            varConnessione = Nothing

            lstListaDownload.SelectedIndex = -1

            lblStatistiche.Text = "Siti da scaricare: " & Quanti
        Else
            Dim Altro As String = ""
            Dim Altro2 As String = ""
            If txtFiltro.Text <> "" Then
                Altro = " And Url Like '%" & txtFiltro.Text.Replace("'", "''") & "%'"
            End If

            Dim sql As String = "Select * From Indirizzi Where Dimensione>0 " & Altro
            Dim rec As Object = CreateObject("ADODB.Recordset")
            Dim Quanti As Integer = 0

            lstListaDownload.Items.Clear()
            rec = varConnessione.LeggeQuery(conn, sql)
            Do Until rec.Eof
                lstListaDownload.Items.Add(rec(0).Value)
                Quanti += 1
                If Quanti / 100 = Int(Quanti / 100) Then
                    Application.DoEvents()
                End If

                rec.MoveNext()
            Loop
            rec.Close()

            conn.close()
            varConnessione = Nothing

            lstListaDownload.SelectedIndex = -1

            lblStatistiche.Text = "Immagini scaricate: " & Quanti
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cmdBloccaTutti_Click(sender As Object, e As EventArgs) Handles cmdBloccaTutti.Click
        If StaScaricandoPagina Then
            frmMain.BloccaTimerDownload()
        Else
            StavaScaricando = False
        End If

        Dim varConnessione As SQLSERVERCE
        Dim conn As Object = CreateObject("ADODB.Connection")

        varConnessione = New SQLSERVERCE
        varConnessione.ImpostaNomeDB(Application.StartupPath & "\DB\picDrop.sdf")
        varConnessione.LeggeImpostazioniDiBase()
        conn = varConnessione.ApreDB()

        Dim sql As String = ""
        Dim rec As Object = CreateObject("ADODB.Recordset")
        Dim NomeSito As String = ""

        Me.Cursor = Cursors.WaitCursor
        For i As Integer = 0 To lstListaDownload.Items.Count - 1
            If lstListaDownload.GetSelected(i) = True Then
                NomeSito = lstListaDownload.Items(i).ToString.Replace("'", "''")
                NomeSito = Mid(NomeSito, NomeSito.IndexOf(";") + 2, NomeSito.Length)
                sql = "Update LinksDaScaricare Set Scaricato='B' Where Link='" & nomesito & "'"
                varConnessione.EsegueSql(conn, sql)
            End If
        Next
        Me.Cursor = Cursors.Default

        conn.close()
        varConnessione = Nothing

        If StavaScaricando Then
            frmMain.FaiRipartireTimerDownload()
        End If

        CaricaLista()

        MsgBox("Siti bloccati")
    End Sub

    Private Sub cmdSelezionaTutti_Click(sender As Object, e As EventArgs) Handles cmdSelezionaTutti.Click
        Me.Cursor = Cursors.WaitCursor
        Dim NomeSito As String

        For i As Integer = 0 To lstListaDownload.Items.Count - 1
            NomeSito = lstListaDownload.Items(i).ToString.Replace("'", "''")
            NomeSito = Mid(NomeSito, NomeSito.IndexOf(";") + 2, NomeSito.Length)
            NomeSito = "        ;" & NomeSito
            lstListaDownload.Items(i) = NomeSito
        Next
        Me.Cursor = Cursors.Default

        lstListaDownload.SelectedIndex = -1

        MsgBox("Siti selezionati")
    End Sub

    Private Sub cmdDeSelezionaTutti_Click(sender As Object, e As EventArgs) Handles cmdDeSelezionaTutti.Click
        Me.Cursor = Cursors.WaitCursor
        Dim NomeSito As String

        For i As Integer = 0 To lstListaDownload.Items.Count - 1
            NomeSito = lstListaDownload.Items(i).ToString.Replace("'", "''")
            NomeSito = Mid(NomeSito, NomeSito.IndexOf(";") + 2, NomeSito.Length)
            NomeSito = "        ;" & NomeSito
            lstListaDownload.Items(i) = NomeSito
        Next
        Me.Cursor = Cursors.Default

        lstListaDownload.SelectedIndex = -1

        MsgBox("Siti De-selezionati")
    End Sub

    Private Sub cmdFiltra_Click(sender As Object, e As EventArgs) Handles cmdFiltra.Click
        CaricaLista()
    End Sub

    Private Sub cnmdPulisceFiltro_Click(sender As Object, e As EventArgs) Handles cnmdPulisceFiltro.Click
        txtFiltro.Text = ""
        CaricaLista()
    End Sub

    Private Sub cmdEliminaSelezionati_Click(sender As Object, e As EventArgs) Handles cmdEliminaSelezionati.Click
        If StaScaricandoPagina Then
            frmMain.BloccaTimerDownload()
        Else
            StavaScaricando = False
        End If

        Dim varConnessione As SQLSERVERCE
        Dim conn As Object = CreateObject("ADODB.Connection")

        varConnessione = New SQLSERVERCE
        varConnessione.ImpostaNomeDB(Application.StartupPath & "\DB\picDrop.sdf")
        varConnessione.LeggeImpostazioniDiBase()
        conn = varConnessione.ApreDB()

        Dim sql As String = ""
        Dim rec As Object = CreateObject("ADODB.Recordset")
        Dim NomeSito As String

        Me.Cursor = Cursors.WaitCursor
        For i As Integer = 0 To lstListaDownload.Items.Count - 1
            If lstListaDownload.GetSelected(i) = True Then
                NomeSito = lstListaDownload.Items(i).ToString.Replace("'", "''")
                NomeSito = Mid(NomeSito, NomeSito.IndexOf(";") + 2, NomeSito.Length)
                sql = "Delete LinksDaScaricare Where Link='" & nomesito & "'"
                varConnessione.EsegueSql(conn, sql)
            End If
        Next
        Me.Cursor = Cursors.Default

        conn.close()
        varConnessione = Nothing

        If StavaScaricando Then
            frmMain.FaiRipartireTimerDownload()
        End If

        CaricaLista()

        MsgBox("Siti eliminati")
    End Sub

    Private Sub lstListaDownload_DoubleClick(sender As Object, e As EventArgs) Handles lstListaDownload.DoubleClick
        Me.Cursor = Cursors.WaitCursor

        Dim NomeSito As String

        If optLinks.Checked Then
            NomeSito = lstListaDownload.Text.ToString.Replace("'", "''")
            NomeSito = Mid(NomeSito, NomeSito.IndexOf(";") + 2, NomeSito.Length)
            WebBrowser1.Navigate(NomeSito)
        Else
            NomeSito = lstListaDownload.Text.ToString
            Dim image As Image = webDownloadImage(NomeSito, False)
            If image Is Nothing Then
                picImmagine.Image = Nothing
            Else
                picImmagine.Image = image
            End If
        End If

        lstListaDownload.SelectedIndex = -1

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cmdRipristinaTutti_Click(sender As Object, e As EventArgs) Handles cmdRipristinaTutti.Click
        If StaScaricandoPagina Then
            frmMain.BloccaTimerDownload()
        Else
            StavaScaricando = False
        End If

        Dim varConnessione As SQLSERVERCE
        Dim conn As Object = CreateObject("ADODB.Connection")

        varConnessione = New SQLSERVERCE
        varConnessione.ImpostaNomeDB(Application.StartupPath & "\DB\picDrop.sdf")
        varConnessione.LeggeImpostazioniDiBase()
        conn = varConnessione.ApreDB()

        Dim sql As String = ""
        Dim rec As Object = CreateObject("ADODB.Recordset")
        Dim NomeSito As String = ""

        Me.Cursor = Cursors.WaitCursor
        For i As Integer = 0 To lstListaDownload.Items.Count - 1
            If lstListaDownload.GetSelected(i) = True Then
                NomeSito = lstListaDownload.Items(i).ToString.Replace("'", "''")
                NomeSito = Mid(NomeSito, NomeSito.IndexOf(";") + 2, NomeSito.Length)
                sql = "Update LinksDaScaricare Set Scaricato='N' Where Link='" & NomeSito & "'"
                varConnessione.EsegueSql(conn, sql)
            End If
        Next
        Me.Cursor = Cursors.Default

        conn.close()
        varConnessione = Nothing

        If StavaScaricando Then
            frmMain.FaiRipartireTimerDownload()
        End If

        CaricaLista()

        MsgBox("Siti ripristinati")
    End Sub

    Private Sub ImpostaMaschera()
        If optLinks.Checked Then
            cmdBloccaTutti.Visible = True
            cmdDeSelezionaTutti.Visible = True
            cmdEliminaSelezionati.Visible = True
            cmdRipristinaTutti.Visible = True
            cmdSelezionaTutti.Visible = True
            txtFiltro.Text = ""
            picImmagine.Visible = False
            WebBrowser1.Visible = True
        Else
            cmdBloccaTutti.Visible = False
            cmdDeSelezionaTutti.Visible = False
            cmdEliminaSelezionati.Visible = False
            cmdRipristinaTutti.Visible = False
            cmdSelezionaTutti.Visible = False
            txtFiltro.Text = ""
            picImmagine.Visible = True
            WebBrowser1.Visible = False
        End If
    End Sub

    Private Sub optLinks_CheckedChanged(sender As Object, e As EventArgs) Handles optLinks.Click
        SaveSetting("picDrop", "Settaggi", "LinksImmagini", "Links")
        optImmagini.Checked = False

        ImpostaMaschera()

        CaricaLista()
    End Sub

    Private Sub optImmagini_CheckedChanged(sender As Object, e As EventArgs) Handles optImmagini.Click
        SaveSetting("picDrop", "Settaggi", "LinksImmagini", "Immagini")
        optLinks.Checked = False

        ImpostaMaschera()

        CaricaLista()
    End Sub

    Private Sub lstListaDownload_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstListaDownload.SelectedIndexChanged

    End Sub
End Class