Imports System.IO

Public Class frmRicerca
    Private sizeX As Integer = 100
    Private sizeY As Integer = 120
    Private posX As Integer
    Private posY As Integer
    Private DaScaricare() As String
    Private qDaScaricare As Integer
    Private c() As CheckBox
    Private qC As Integer = 0
    Private Trovati As Boolean
    Private Stoppa As Boolean
    Private Immagini As Integer

    Private Sub frmRicerca_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        MascheraRicerca = False
    End Sub

    Private Sub frmRicerca_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtPagine.Text = GetSetting("picDrop", "Settaggi", "Pagine", 5)
        txtInizio.Text = GetSetting("picDrop", "Settaggi", "Inizio", 1)
    End Sub

    Private Function LeggeListaRicerche() As String
        Dim varConnessione As SQLSERVERCE
        Dim conn As Object = CreateObject("ADODB.Connection")
        Dim rec As Object = "ADODB.Recordset"

        varConnessione = New SQLSERVERCE
        varConnessione.ImpostaNomeDB(Application.StartupPath & "\DB\picDrop.sdf")
        varConnessione.LeggeImpostazioniDiBase()
        conn = varConnessione.ApreDB()

        Dim Sql As String
        Dim Ritorno As String = ""

        Sql = "Select * From Ricerche Where Ricerca Like '%" & txtFiltro.Text.Replace("'", "''") & "%'"
        rec = varConnessione.LeggeQuery(conn, Sql)
        If Not rec.eof Then
            Ritorno = rec("InizioPagina").Value & ";" & rec("FinePagina").Value & ";"
        End If
        rec.Close()

        conn.Close()
        varConnessione = Nothing

        Return Ritorno
    End Function

    Private Sub SalvaRicerca()
        Dim varConnessione As SQLSERVERCE
        Dim conn As Object = CreateObject("ADODB.Connection")
        Dim rec As Object = "ADODB.Recordset"

        varConnessione = New SQLSERVERCE
        varConnessione.ImpostaNomeDB(Application.StartupPath & "\DB\picDrop.sdf")
        varConnessione.LeggeImpostazioniDiBase()
        conn = varConnessione.ApreDB()

        Dim Sql As String

        Sql = "Select * From Ricerche Where Ricerca Like '%" & txtFiltro.Text.Replace("'", "''") & "%'"
        rec = varConnessione.LeggeQuery(conn, Sql)
        If Not rec.eof Then
            Sql = "Update Ricerche Set InizioPagina=" & txtInizio.Text & ", FinePagina=" & txtPagine.Text & " Where Ricerca Like '%" & txtFiltro.Text.Replace("'", "''") & "%'"
        Else
            Sql = "Insert Into Ricerche Values ('" & txtFiltro.Text.Replace("'", "''") & "', " & txtInizio.Text & ", " & txtPagine.Text & ", 0)"
        End If
        rec.Close()

        varConnessione.EsegueSql(conn, Sql)

        conn.Close()
        varConnessione = Nothing
    End Sub

    Private Sub EliminaRicerca(Cosa As String)
        Dim varConnessione As SQLSERVERCE
        Dim conn As Object = CreateObject("ADODB.Connection")

        varConnessione = New SQLSERVERCE
        varConnessione.ImpostaNomeDB(Application.StartupPath & "\DB\picDrop.sdf")
        varConnessione.LeggeImpostazioniDiBase()
        conn = varConnessione.ApreDB()

        Dim Sql As String

        Sql = "Delete From Ricerche Where Ricerca Like '%" & Cosa.Replace("'", "''") & "%'"
        varConnessione.EsegueSql(conn, Sql)

        conn.Close()
        varConnessione = Nothing
    End Sub

    Private Sub AggiungeScaricate(Quante As Integer)
        Dim varConnessione As SQLSERVERCE
        Dim conn As Object = CreateObject("ADODB.Connection")
        Dim rec As Object = "ADODB.Recordset"

        varConnessione = New SQLSERVERCE
        varConnessione.ImpostaNomeDB(Application.StartupPath & "\DB\picDrop.sdf")
        varConnessione.LeggeImpostazioniDiBase()
        conn = varConnessione.ApreDB()

        Dim Sql As String

        Sql = "Update Ricerche Set Scaricate=Scaricate+" & Quante & " Where Ricerca Like '%" & txtFiltro.Text.Replace("'", "''") & "%'"
        varConnessione.EsegueSql(conn, Sql)

        conn.Close()
        varConnessione = Nothing
    End Sub

    Private Sub cmdRicerca_Click(sender As Object, e As EventArgs) Handles cmdRicerca.Click
        If txtFiltro.Text = "" Then
            MsgBox("Selezionare un argomento di ricerca", vbInformation)
            Exit Sub
        End If

        Dim GiaFatta As String = LeggeListaRicerche()
        If GiaFatta <> "" Then
            Dim c() As String = GiaFatta.Split(";")
            Dim Risposta As Integer = MsgBox("La ricerca '" & txtFiltro.Text & "' è già stata eseguita." & vbCrLf & vbCrLf & "Imposto la pagina iniziale per continuare dal punto in cui ci " & vbCrLf & "si era fermati la volta precedente " & vbCrLf & "Inizio pagina: " & c(0) & " - Fine pagina: " & c(1) & " ?", vbInformation + vbYesNoCancel + vbDefaultButton2)
            If Risposta = vbCancel Then
                Exit Sub
            Else
                If Risposta = vbYes Then
                    txtInizio.Text = c(1) + 1
                    txtPagine.Text = Val(c(1)) + 6
                End If
            End If
        End If

        SalvaRicerca()

        Me.Cursor = Cursors.WaitCursor
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle

        Dim varConnessione As SQLSERVERCE
        Dim conn As Object = CreateObject("ADODB.Connection")

        varConnessione = New SQLSERVERCE
        varConnessione.ImpostaNomeDB(Application.StartupPath & "\DB\picDrop.sdf")
        varConnessione.LeggeImpostazioniDiBase()
        conn = varConnessione.ApreDB()

        pnlContenuto.Controls.Clear()
        posX = 5
        posY = 5
        qDaScaricare = 0
        Erase DaScaricare
        qC = 0
        Erase c
        Trovati = False
        Immagini = 0

        cmdDeSelTutti.Visible = False
        cmdSelTutti.Visible = False
        cmdScarica.Visible = False
        Stoppa = False
        cmdStop.Visible = True
        cmdRicerca.Enabled = False
        cmdRicerche.Enabled = False

        cmdPiu.Enabled = False
        cmdMeno.Enabled = False

        lblPagina.Visible = True
        lblImmagini.Visible = True

        lblPagina.Text = ""
        lblImmagini.Text = ""
        Application.DoEvents()

        pnlContenuto.Enabled = False

        ' Dim Ricerca As String = "https://it.images.search.yahoo.com/search/images?p=" & txtFiltro.Text.Replace(" ", "%20") & "&fr=yfp-t-909&fr2=piv-web"
        'Dim Ricerca As String = "https://www.google.it/search?q=" & txtFiltro.Text.Replace(" ", "%20") & "&newwindow=1&source=lnms&tbm=isch&sa=X&ved=0ahUKEwj44-Ol697YAhUGXBQKHbrMBfAQ_AUICygC&biw=1920&bih=949"

        Dim Inizio As Integer = ((Val(txtInizio.Text) - 1) * 29) + 1
        Dim Pagina As Integer = Val(txtInizio.Text)

        For conta As Integer = 1 To Val(txtPagine.Text) - Val(txtInizio.Text)
            ' Dim Ricerca As String = "https://www.bing.com/images/search?q=" & txtFiltro.Text.Replace(" ", "%20") & "&qs=n&form=QBLH&scope=images&sp=-1&pq=" & txtFiltro.Text.Replace(" ", "%20") & "&sc=8-5&sk=&cvid=C713FE68173A4CD3993A58C7F868EDE4&first=" & Inizio & "&count=28&FORM=IBASEP"
            Dim Ricerca As String = "https://www.bing.com/images/search?q=" & txtFiltro.Text.Replace(" ", "%20") & "&qs=n&form=IBASEP&scope=images&sp=-1&pq=" & txtFiltro.Text.Replace(" ", "%20") & "&sc=8-5&cvid=C713FE68173A4CD3993A58C7F868EDE4&first=" & Inizio & "&count=28&cw=1663&ch=907&tsc=ImageBasicHover"

            lblPagina.Text = "Pagina " & Pagina & "/" & txtPagine.Text
            Pagina += 1
            Application.DoEvents()

            ScaricaPagina(varConnessione, conn, Ricerca)

            If Stoppa Then
                Exit For
            End If

            Inizio += 29
        Next

        pnlContenuto.Enabled = True

        cmdDeSelTutti.Enabled = True
        cmdSelTutti.Enabled = True
        cmdScarica.Enabled = True
        txtFiltro.Enabled = True
        cmdRicerca.Enabled = True
        cmdRicerche.Enabled = True
        lblAvanzamento.Visible = False

        cmdPiu.Enabled = False
        cmdMeno.Enabled = False

        cmdStop.Visible = False

        lblImmagini.Visible = False
        lblPagina.Visible = False

        conn.Close()
        varConnessione = Nothing

        If Trovati Then
            cmdDeSelTutti.Visible = True
            cmdSelTutti.Visible = True
            cmdScarica.Visible = True
        Else
            MsgBox("Nessuna immagine rilevata", vbInformation)
        End If

        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
        Me.Cursor = Cursors.Default
    End Sub

    Private Function ConverteSourceCode(s) As String
        Dim Rit As String = s
        Dim tutte = "0123456789ABCDEF"
        Dim perc As Integer = Rit.IndexOf("%")

        Do While perc > -1
            Dim a As String = Mid(Rit, perc + 1, 3)
            Dim car As String = ""

            If tutte.IndexOf(Mid(a, 2, 1).ToUpper) > -1 And tutte.IndexOf(Mid(a, 3, 1).ToUpper) > -1 Then
                car = Mid(a, 2, 2)
                Dim h1 As Integer = CInt("&H" & car)
                car = Chr(h1)
                'Stop
            Else
                car = "-"
            End If
            Rit = Rit.Replace(a, car)

            perc = Rit.IndexOf("%")
        Loop
        Rit = Rit.Replace("&quot;", Chr(34))

        Return Rit
    End Function

    Private Sub ScaricaPagina(varConnessione As SQLSERVERCE, conn As Object, Url As String)
        Dim sNomeFile As String = "Links\AppoggioRicerca.html"
        Dim gf As New GestioneFilesDirectory

        Try
            Dim sourcecode As String = ""
            Dim sito As String = Url

            For i As Integer = 12 To Url.Length
                If Mid(Url, i, 1) = "/" Then
                    sito = Mid(Url, 1, i - 1)
                    Exit For
                End If
            Next

            If TipoCollegamento = "Proxy" Then
                Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create(Url)
                request.Proxy.Credentials = New System.Net.NetworkCredential(Utenza, Password, Dominio)
                Dim response As System.Net.HttpWebResponse = request.GetResponse()
                Application.DoEvents()
                Dim sr As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
                Application.DoEvents()
                sourcecode = sr.ReadToEnd()
                sr.Close()
                response.Close()
                request = Nothing

                gf.CreaAggiornaFile(sNomeFile, sourcecode)
            Else
                ScaricaFileGlobale(Url, sNomeFile, varConnessione, conn)
            End If

            sourcecode = gf.LeggeFileIntero(sNomeFile)
            sourcecode = ConverteSourceCode(sourcecode)

            Dim a As Long
            Dim Appoggio As String
            Dim Inizio As Long
            Dim Fine As Long
            Dim Scaricate As Integer = 0
            Dim Cambia As String
            Dim sourceCodeOriginale As String = sourcecode

            a = ControllaSeCiSonoImmagini(sourcecode)
            Do While a > -1
                Appoggio = Mid(sourcecode, a, sourcecode.Length)
                For i As Long = a To 1 Step -1
                    If Mid(sourcecode, i, 1) = Chr(34) Or Mid(sourcecode, i, 1) = "'" Or Mid(sourcecode, i, 1) = ">" Or Mid(sourcecode, i, 1) = "=" Then ' Or Mid(sourcecode, i, 1) = " "
                        Inizio = i + 1
                        Exit For
                    End If
                Next
                For i As Long = a To sourcecode.Length - 1
                    If Mid(sourcecode, i, 1) = Chr(34) Or Mid(sourcecode, i, 1) = "'" Or Mid(sourcecode, i, 1) = "<" Then ' Or Mid(sourcecode, i, 1) = " "
                        Fine = i ' + 1
                        Exit For
                    End If
                Next
                Appoggio = Mid(sourcecode, Inizio, Fine - Inizio)

                Cambia = Appoggio
                If Appoggio.ToUpper.IndexOf(".HTM") = -1 Then
                    If Mid(Appoggio, 2, 1) = "/" Then
                        Appoggio = Mid(Appoggio, 1, 1) & sito & Mid(Appoggio, 2, Appoggio.Length)
                    Else
                        If Mid(Appoggio, 2, 2) = ".." Then
                            Appoggio = SistemaNome(Appoggio, Url)
                        End If
                    End If
                    If Appoggio.ToUpper.IndexOf("HTTP") > -1 Then
                        Appoggio = Appoggio.Replace("\/", "\")
                        If Appoggio.IndexOf("yimg.com") = -1 And Appoggio.ToUpper.IndexOf("YAHOO") = -1 Then
                            Dim im As Image = webDownloadImage(Appoggio, True)
                            Dim s As String = ""
                            If Not im Is Nothing Then
                                s = im.Width & " x " & im.Height
                            End If
                            AggiungeImmagine(varConnessione, conn, Appoggio, s, im)

                            Immagini += 1
                            lblImmagini.Text = "Immagini: " & Immagini
                            Application.DoEvents()
                        End If
                    End If
                End If

                sourcecode = sourcecode.Replace(Cambia, "")

                a = ControllaSeCiSonoImmagini(sourcecode)

                If Stoppa Then
                    Exit Do
                End If

                Application.DoEvents()
            Loop
        Catch
        End Try
    End Sub

    Private Sub AggiungeToglieImmagine(sender As Object, e As EventArgs)
        Dim c As CheckBox = DirectCast(sender, CheckBox)
        Dim Immagine As String = c.Tag

        If c.Checked Then
            qDaScaricare += 1
            ReDim Preserve DaScaricare(qDaScaricare)
            DaScaricare(qDaScaricare) = Immagine
        Else
            Dim Appoggio(qDaScaricare - 1) As String
            Dim cc As Integer = 0
            For i As Integer = 1 To qDaScaricare
                If DaScaricare(i) = Immagine Then
                Else
                    cc += 1
                    Appoggio(cc) = DaScaricare(i)
                End If
            Next
            DaScaricare = Appoggio
            qDaScaricare -= 1
        End If
    End Sub

    Private Sub Anteprima(sender As Object, e As EventArgs)
        Dim P As PictureBox = DirectCast(sender, PictureBox)

        picAnteprima.Image = P.Image
        picAnteprima.Left = 10
        picAnteprima.Top = 10
        picAnteprima.Width = Me.Width - 40
        picAnteprima.Height = Me.Height - 50
        picAnteprima.SizeMode = PictureBoxSizeMode.StretchImage

        picChiude.Left = (picAnteprima.Left + picAnteprima.Width) - picChiude.Width - 5
        picChiude.Top = picAnteprima.Top + 5
        picChiude.Width = 29
        picChiude.Height = 20

        picAnteprima.Visible = True
        picChiude.Visible = True
        picChiude.BringToFront()
    End Sub

    Private Sub AggiungeImmagine(varConnessione As SQLSERVERCE, conn As Object, UrlImmagine As String, Dime As String, Immagine As Image)
        If Not Immagine Is Nothing Then
            Dim l As New Label
            Dim p As New PictureBox

            p.Left = posX
            p.Top = posY
            p.BorderStyle = BorderStyle.FixedSingle
            p.SizeMode = PictureBoxSizeMode.StretchImage
            p.Width = sizeX
            p.Height = sizeY
            p.Image = Immagine
            AddHandler p.Click, AddressOf Anteprima

            l.Left = posX
            l.Top = posY + sizeY + 2
            l.Width = sizeX
            l.Height = 15
            l.Text = Dime
            l.TextAlign = ContentAlignment.MiddleCenter

            qC += 1
            ReDim Preserve c(qC)

            c(qC) = New CheckBox
            c(qC).Top = l.Top + l.Height + 2
            c(qC).Left = posX
            c(qC).Width = p.Width
            c(qC).Height = 20
            c(qC).Tag = UrlImmagine

            Dim pp As New picDropCls
            If Not pp.ControllaSeGiaScaricata(varConnessione, conn, UrlImmagine) Then
                c(qC).Text = "Seleziona"
                AddHandler c(qC).Click, AddressOf AggiungeToglieImmagine
            Else
                c(qC).Appearance = Appearance.Button
                'c(qC).BackColor = Color.DarkRed
                'c(qC).ForeColor = Color.White
                c(qC).Text = "Già scaricata"
                c(qC).Enabled = False
            End If
            pp = Nothing

            pnlContenuto.Controls.Add(p)
            pnlContenuto.Controls.Add(l)
            pnlContenuto.Controls.Add(c(qC))

            posX += (sizeX + 5)
            If posX > pnlContenuto.Width - (posX / 5) Then
                posX = 5
                posY += (sizeY + 20 + l.Height + c(qC).Height)
            End If

            Trovati = True
        End If
    End Sub

    Private Sub cmdSelTutti_Click(sender As Object, e As EventArgs) Handles cmdSelTutti.Click
        Erase DaScaricare
        qDaScaricare = 0

        For i As Integer = 1 To qC
            c(i).Checked = True

            qDaScaricare += 1
            ReDim Preserve DaScaricare(qDaScaricare)
            DaScaricare(qDaScaricare) = c(i).Tag
        Next
    End Sub

    Private Sub cmdDeSelTutti_Click(sender As Object, e As EventArgs) Handles cmdDeSelTutti.Click
        For i As Integer = 1 To qC
            c(i).Checked = False
        Next

        Erase DaScaricare
        qDaScaricare = 0
    End Sub

    Private Sub cmdScarica_Click(sender As Object, e As EventArgs) Handles cmdScarica.Click
        Me.Cursor = Cursors.WaitCursor

        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle

        lblAvanzamento.Visible = True
        Application.DoEvents()

        cmdDeSelTutti.Enabled = False
        cmdSelTutti.Enabled = False
        cmdScarica.Enabled = False
        txtFiltro.Enabled = False
        cmdRicerca.Enabled = False
        cmdRicerche.Enabled = False
        cmdPiu.Enabled = False
        cmdMeno.Enabled = False
        cmdPiuInizio.Enabled = False
        cmdMenoInizio.Enabled = False
        Application.DoEvents()

        Try
            MkDir("Links\Ricerca")
        Catch ex As Exception

        End Try

        Dim p As New picDropCls
        Dim gf As New GestioneFilesDirectory
        Dim varConnessione As SQLSERVERCE
        Dim conn As Object = CreateObject("ADODB.Connection")

        varConnessione = New SQLSERVERCE
        varConnessione.ImpostaNomeDB(Application.StartupPath & "\DB\picDrop.sdf")
        varConnessione.LeggeImpostazioniDiBase()
        conn = varConnessione.ApreDB()

        lblAvanzamento.Top = (Me.Height / 2) - (lblAvanzamento.Height / 2)
        lblAvanzamento.Left = -10
        lblAvanzamento.Width = Me.Width + 10

        Dim Scaricate As Integer = 0

        For i As Integer = 1 To qDaScaricare
            lblAvanzamento.Text = "Immagine " & i & "/" & qDaScaricare & vbCrLf & DaScaricare(i)
            Application.DoEvents()

            Dim giaScaricata As Boolean = False

            If Not ScaricaAncheSeGiaScaricata Then
                giaScaricata = p.ControllaSeGiaScaricata(varConnessione, conn, DaScaricare(i))
            End If

            If Not giaScaricata Then
                Dim sNomeFile As String = "Links\Ricerca\" & gf.TornaNomeFileDaPath(DaScaricare(i))
                If sNomeFile.Contains("?") Then
                    sNomeFile = Mid(sNomeFile, 1, sNomeFile.IndexOf("?"))
                End If
                sNomeFile = sNomeFile.Replace(":", "_")
                sNomeFile = sNomeFile.Replace(">", "_")
                sNomeFile = sNomeFile.Replace("<", "_")
                sNomeFile = sNomeFile.Replace("*", "_")

                Dim c As String = gf.TornaNomeDirectoryDaPath(sNomeFile)
                Dim n As String = gf.TornaNomeFileDaPath(sNomeFile)
                If n.Length > 100 Then
                    n = Mid(n, 1, 47) & Mid(n, n.Length - 47, n.Length)
                End If
                sNomeFile = c & "\" & n

                ScaricaFileGlobale(DaScaricare(i).Replace("\", "/"), sNomeFile, varConnessione, conn)
                Application.DoEvents()

                If File.Exists(sNomeFile) Then
                    ' Scrive tag exif
                    Dim NomeSito As String = DaScaricare(i)
                    Dim sResto As String
                    Dim Resto() As String = {}
                    Dim a1 As Integer = Mid(NomeSito, 11, NomeSito.Length).IndexOf("/")
                    If a1 > -1 Then
                        sResto = Mid(NomeSito, a1 + 11, NomeSito.Length)
                        If Mid(sResto, 1, 1) = "/" Then
                            sResto = Mid(sResto, 2, sResto.Length)
                        End If
                        Resto = sResto.Split("/")

                        NomeSito = Mid(NomeSito, 1, a1 + 10)
                        NomeSito = NomeSito.ToUpper.Replace("HTTPS://", "").Replace("HTTP://", "")
                    End If
                    ScriveTag(sNomeFile, DaScaricare(i), Resto)
                    Application.DoEvents()
                    ' Scrive tag exif

                    Scaricate += 1
                End If

                Dim sDatella As String = Now.Year & "-" & Format(Now.Month, "00") & "-" & Format(Now.Day, "00") & " " & Format(Now.Hour, "00") & ":" & Format(Now.Minute, "00") & ":" & Format(Now.Second, "00")
                Dim Dime As Integer = p.ScriveDimensioniImmagine(sNomeFile, Nothing)

                p.ScriveIndirizzoSuDB(varConnessione, conn, DaScaricare(i), sDatella, Dime, sNomeFile)
                Application.DoEvents()
            End If
        Next

        lblAvanzamento.Visible = False
        Application.DoEvents()

        cmdDeSelTutti.Enabled = True
        cmdSelTutti.Enabled = True
        cmdScarica.Enabled = True
        txtFiltro.Enabled = True
        cmdRicerca.Enabled = True
        cmdRicerche.Enabled = True
        cmdPiu.Enabled = False
        cmdMeno.Enabled = False
        cmdPiuInizio.Enabled = False
        cmdMenoInizio.Enabled = False
        Application.DoEvents()

        p = Nothing

        AggiungeScaricate(Scaricate)

        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cmdStop_Click(sender As Object, e As EventArgs) Handles cmdStop.Click
        Stoppa = True
    End Sub

    Private Sub cmdPiu_Click(sender As Object, e As EventArgs) Handles cmdPiu.Click
        txtPagine.Text = (Val(txtPagine.Text) + 1)
        SaveSetting("picDrop", "Settaggi", "Pagine", txtPagine.Text)
    End Sub

    Private Sub cmdMeno_Click(sender As Object, e As EventArgs) Handles cmdMeno.Click
        If Val(txtPagine.Text) > 1 And Val(txtPagine.Text) > Val(txtInizio.Text) Then
            txtPagine.Text = (Val(txtPagine.Text) - 1)
            SaveSetting("picDrop", "Settaggi", "Pagine", txtPagine.Text)
        End If
    End Sub

    Private Sub cmdPiuInizio_Click(sender As Object, e As EventArgs) Handles cmdPiuInizio.Click
        If Val(txtInizio.Text) < Val(txtPagine.Text) Then
            txtInizio.Text = (Val(txtInizio.Text) + 1)
            SaveSetting("picDrop", "Settaggi", "Inizio", txtInizio.Text)
        End If
    End Sub

    Private Sub cmdMenoInizio_Click(sender As Object, e As EventArgs) Handles cmdMenoInizio.Click
        If Val(txtInizio.Text) > 1 Then
            txtInizio.Text = (Val(txtInizio.Text) - 1)
            SaveSetting("picDrop", "Settaggi", "Inizio", txtInizio.Text)
        End If
    End Sub

    Private Sub picChiude_Click(sender As Object, e As EventArgs) Handles picChiude.Click
        picAnteprima.Visible = False
        picChiude.Visible = False
    End Sub

    Private Sub CaricaRicerche()
        Dim varConnessione As SQLSERVERCE
        Dim conn As Object = CreateObject("ADODB.Connection")
        Dim rec As Object = "ADODB.Recordset"

        varConnessione = New SQLSERVERCE
        varConnessione.ImpostaNomeDB(Application.StartupPath & "\DB\picDrop.sdf")
        varConnessione.LeggeImpostazioniDiBase()
        conn = varConnessione.ApreDB()

        Dim Sql As String
        Dim s As String

        Sql = "Select * From Ricerche Order By Ricerca"
        rec = varConnessione.LeggeQuery(conn, Sql)
        lstRicerche.Items.Clear()
        Do Until rec.eof
            s = rec("Ricerca").Value
            If (s.Length > 40) Then
                s = Mid(s, 1, 37) & "..."
            End If
            For k As Integer = s.Length To 40
                s &= " "
            Next

            Dim i As String = rec("InizioPagina").Value.ToString.Trim

            For k As Integer = i.Length To 3
                i = "0" & i
            Next

            Dim f As String = rec("FinePagina").Value.ToString.Trim

            For k As Integer = f.Length To 3
                f = "0" & f
            Next

            Dim d As String = rec("Scaricate").Value.ToString.Trim

            For k As Integer = d.Length To 4
                d = "0" & d
            Next

            s &= " Inizio: " & i & " Fine: " & f & " Scaricate: " & d

            lstRicerche.Items.Add(s)

            rec.movenext()
        Loop
        rec.Close()

        lstRicerche.Items.Add("")
        lstRicerche.Items.Add("Doppio click per selezionare / Canc per eliminare")
        lstRicerche.Items.Add("Chiude maschera")

        conn.Close()
        varConnessione = Nothing
    End Sub

    Private Sub cmdRicerche_Click(sender As Object, e As EventArgs) Handles cmdRicerche.Click
        lstRicerche.Width = Me.Width * 80 / 100
        lstRicerche.Height = Me.Height * 70 / 100
        lstRicerche.Left = (Me.Width / 2) - (lstRicerche.Width / 2)
        lstRicerche.Top = (Me.Height / 2) - (lstRicerche.Height / 2)
        lstRicerche.Visible = True

        CaricaRicerche()
    End Sub

    Private Sub lstRicerche_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstRicerche.DoubleClick
        If lstRicerche.Text.Trim <> "" And lstRicerche.Text <> "Chiude maschera" And Not lstRicerche.Text.Contains("Doppio click per selezionare") Then
            Dim s As String = lstRicerche.Text
            Dim r As String = Mid(s, 1, s.IndexOf(" Inizio:")).Trim
            s = Mid(s, s.IndexOf(" Inizio:") + 1, s.Length)
            Dim i As String = Mid(s, 1, s.IndexOf(" Fine:"))
            Dim f As String = Mid(s, s.IndexOf(" Fine:") + 1, s.Length)
            f = Mid(f, 1, f.IndexOf(" Scaricate:")).Trim

            i = i.Replace(" Inizio: ", "").Trim
            f = f.Replace("Fine: ", "").Trim

            txtFiltro.Text = r
            txtInizio.Text = Val(i)
            txtPagine.Text = Val(f)

            lstRicerche.Visible = False
        Else
            If lstRicerche.Text = "Chiude maschera" Then
                lstRicerche.Visible = False
            End If
        End If
    End Sub

    Private Sub txtFiltro_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFiltro.KeyDown
        If e.KeyCode = 13 Then
            Call cmdRicerca_Click(sender, e)
        End If
    End Sub

    Private Sub lstRicerche_KeyDown(sender As Object, e As KeyEventArgs) Handles lstRicerche.KeyDown
        If e.KeyCode = 46 Then
            If lstRicerche.Text.Trim <> "" And lstRicerche.Text <> "Chiude maschera" And Not lstRicerche.Text.Contains("Doppio click per selezionare") Then
                Dim s As String = lstRicerche.Text
                Dim r As String = Mid(s, 1, s.IndexOf(" Inizio:")).Trim

                If MsgBox("Vuoi eliminare la ricerca selezionata (" & r & ") ?", vbYesNo + vbDefaultButton2 + vbInformation) = vbYes Then
                    EliminaRicerca(r)

                    CaricaRicerche()
                End If
            End If
        End If
    End Sub
End Class