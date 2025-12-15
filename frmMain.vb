Imports System.IO
Imports System.Data.OleDb
Imports System.Text
Imports System.Drawing.Imaging
Imports System.Net
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Threading

Public Class frmMain
    Inherits System.Windows.Forms.Form

    Private UltimaImmagine As String = ""
    'Private SecondiPassati As Integer = 0
    Private QuantiFilesVecchi As Long = -1
    Private StaElaborando As Boolean
    Private AppenaPartito As Boolean = True
    Private StringaRiprendeDownload As String = "R&iprende download"
    Private StringaBloccaDownload As String = "B&locca download"
    Private varConnessione As SQLSERVERCE
    Private conn As Object = CreateObject("ADODB.Connection")
    Private Spostamento As Boolean = False

    Private NotifyIcon1 As NotifyIcon = New NotifyIcon
    Private contextMenu1 As System.Windows.Forms.ContextMenu = New System.Windows.Forms.ContextMenu
    Private mnuApreMaschera As GestioneMenu
    Private mnuScaricaLink As GestioneMenu
    Private mnuRiprovaLinks As GestioneMenu
    Private mnuStatistiche As GestioneMenu
    Private mnuSistemaLink As GestioneMenu
    Private mnuRicerca As GestioneMenu
    Private mnuImpostazioni As GestioneMenu
    Private mnuUtility As GestioneMenu
    Private mnuBloccaDownload As GestioneMenu
    Private mnuListaDownload As GestioneMenu
    Private mnuUscita As GestioneMenu
    Private menuItemSeparatore As GestioneMenu
    Private screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
    Private screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height

    <DllImportAttribute("user32.dll")>
    Public Shared Function SendMessage(hWnd As IntPtr, Msg As Integer, wParam As Integer, lParam As Integer) As Integer
    End Function

    <DllImportAttribute("user32.dll")> Public Shared Function ReleaseCapture() As Boolean
    End Function

    Private Sub frmMain_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown, pbDownload.MouseDown, lblDimensioni.MouseDown
        Const WM_NCLBUTTONDOWN As Integer = &HA1
        Const HT_CAPTION As Integer = &H2

        If e.Button = MouseButtons.Left Then
            Spostamento = True
            ReleaseCapture()
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0)
        End If
    End Sub

    Private Sub frmMain_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        If Spostamento Then
            SaveSetting("picDrop", "Settaggi", "PosX", Me.Left)
            SaveSetting("picDrop", "Settaggi", "PosY", Me.Top)
            Spostamento = False
        End If
    End Sub

    Private Sub Form1_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        e.Effect = DragDropEffects.Move
    End Sub

    Private Sub Form1_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        e.Effect = DragDropEffects.Move
    End Sub

    Private Sub Form1_DragOver(sender As Object, e As DragEventArgs) Handles Me.DragOver
        'If StaScaricandoPagina = True Then
        '    Exit Sub
        'End If

        e.Effect = DragDropEffects.Move

        If e.Data.GetDataPresent(DataFormats.Text) Then
            If DirBase = "" Then
                MsgBox("Nessuna directory impostata")
            Else
                Dim url As String

                Try
                    url = e.Data.GetData(DataFormats.Html).ToString()
                Catch ex As Exception
                    url = ""
                End Try
                If url = "" Then
                    Try
                        url = e.Data.GetData(DataFormats.Text).ToString()
                    Catch ex As Exception
                        url = ""
                    End Try
                End If

                Dim a As Integer = url.IndexOf("src=" & Chr(34))

                If a > -1 Then
                    url = Mid(url, a + 6, url.Length)
                    a = url.IndexOf(Chr(34))
                    url = Mid(url, 1, a)

                    If Mid(url, 1, 1) = "{" Then
                        url = Mid(url, 2, url.Length)
                    End If
                    If Mid(url, url.Length - 1, url.Length) = "}" Then
                        url = Mid(url, 1, url.Length - 1)
                    End If
                    If url.IndexOf("&quot;default&quot;") > -1 Then
                        url = "http:" & Mid(url, url.IndexOf("//") + 1, url.Length)
                    End If
                    'If url.IndexOf("?") > -1 Then
                    '    url = Mid(url, 1, url.IndexOf("?"))
                    'End If

                    ScaricaFile(url)
                Else
                    Dim sUrl As String = url.ToUpper.Trim
                    Dim ok As Boolean = False

                    If sUrl.IndexOf(".HTM") > -1 Or sUrl.IndexOf(".PHP") > -1 Or Mid(sUrl, sUrl.Length - 1, 1) = "/" Then
                        ok = True
                    Else
                        If sUrl.IndexOf(".JPG") = -1 And sUrl.IndexOf(".JPEG") = -1 And sUrl.IndexOf(".PNG") = -1 And sUrl.IndexOf(".GIF") = -1 And sUrl.IndexOf(".PCX") = -1 Then
                            ok = True
                        End If
                    End If

                    If ok = True Then
                        If sUrl.IndexOf("HTTP://") = -1 Then
                            url = "http://" & url
                        End If

                        MetteInCodaLink(url, True)

                        Me.BackgroundImage = Image.FromFile("Icone\DRAG1PG.ICO")

                        tmrDownload.Enabled = True
                        tmrControllo.Enabled = False
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub ScaricaFile(Url As String)
        If Url = UltimaImmagine Then
            Exit Sub
        End If

        Dim NomeFile As String = ""
        Dim p As New picDropCls

        For i As Integer = Url.Length To 1 Step -1
            If Mid(Url, i, 1) = "/" Or Mid(Url, i, 1) = "\" Then
                NomeFile = Mid(Url, i + 1, Url.Length)
                Exit For
            End If
        Next
        If NomeFile = "" Then
            NomeFile = Now.Year & Format(Now.Month, "00") & Format(Now.Day, "00") & Format(Now.Hour, "00") & Format(Now.Minute, "00") & Format(Now.Second, "00") & ".jpg"
        Else
            If NomeFile.IndexOf(".") = -1 Then
                NomeFile += ".jpg"
            End If
        End If

        Try
            If Not Me.BackgroundImage Is Nothing Then
                Me.BackgroundImage.Dispose()
                Me.BackgroundImage = Nothing
            End If
        Catch ex As Exception

        End Try

        Dim sNomeFile As String
        Dim Dime As Long = 0

        If Sovrascrivi = False Then
            sNomeFile = p.ControllaSeEsisteFile(DirBase & "\" & NomeFile)
        Else
            Try
                Kill(DirBase & "\" & NomeFile)
            Catch ex As Exception

            End Try

            sNomeFile = DirBase & "\" & NomeFile
        End If

        sNomeFile = sNomeFile.Replace("+", "_")

        Dim gf As New GestioneFilesDirectory
        Dim NomeSito As String = Url
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

        If sNomeFile.ToUpper.IndexOf("HTTP://") > -1 Then
            Dim Appo As String = Mid(sNomeFile, sNomeFile.ToUpper.IndexOf("HTTP://") + 1, sNomeFile.Length)
            Appo = gf.TornaNomeFileDaPath(Appo)
            gf = Nothing
            sNomeFile = Mid(sNomeFile, 1, sNomeFile.ToUpper.IndexOf("HTTP://"))
            If Mid(sNomeFile, sNomeFile.Length - 1, 1) <> "\" Then
                sNomeFile += "\"
            End If
            sNomeFile += Appo
        End If

        Dim OkEst As Boolean = False
        For i As Integer = sNomeFile.Length To sNomeFile.Length - 5 Step -1
            If Mid(sNomeFile, i, 1) = "." Then
                OkEst = True
                Exit For
            End If
        Next

        Dim SoloNome As String = gf.TornaNomeFileDaPath(sNomeFile)
        Dim SoloDir As String = gf.TornaNomeDirectoryDaPath(sNomeFile) & "\"
        If SoloDir.Length + SoloNome.Length > 150 Then
            SoloNome = Mid(SoloNome, 1, 24) & "_" & Mid(SoloNome, SoloNome.Length - 25, SoloNome.Length)
        End If

        Dim Estensione As String = gf.TornaEstensioneFileDaPath(SoloNome)
        SoloNome = Mid(SoloNome, 1, SoloNome.IndexOf(Estensione))

        Dim sConta As String = ""
        Dim Conta As Integer = 0

        Do While File.Exists(SoloDir & SoloNome & sConta & Estensione)
            Conta += 1
            sConta = "_" & Format(Conta, "000")
        Loop
        sNomeFile = SoloDir & SoloNome & sConta & Estensione

        If sNomeFile.IndexOf("?") > -1 And sNomeFile.IndexOf(".") < sNomeFile.IndexOf("?") Then
            sNomeFile = Mid(sNomeFile, 1, sNomeFile.IndexOf("?"))
        End If

        lblDimensioni.Visible = False
        Application.DoEvents()

        ScaricaFileDallaRete(Url, sNomeFile, Me, lblDimensioni, varConnessione, conn)

        If File.Exists(sNomeFile) = True Then
            Dim Ritorno As Integer = SistemaFileScaricato(sNomeFile, lblDimensioni, varConnessione, conn, Url, NomeSito, Resto, Me)
        Else
            Me.BackgroundImage = Image.FromFile("Icone\icona_ELIMINA-TAG.png")
        End If
        tmrDownload.Enabled = True
        tmrControllo.Enabled = False

        Try
            File.Delete("Links\Appoggio.jpg")
        Catch ex As Exception

        End Try

        p = Nothing
    End Sub

    'Private Sub SetProperty(ByRef prop As System.Drawing.Imaging.PropertyItem, iId As Integer, sTxt As String)
    '    Dim iLen As Integer = sTxt.Length + 1
    '    Dim bTxt As Byte() = New [Byte](iLen - 1) {}
    '    For i As Integer = 1 To iLen - 2
    '        bTxt(i) = CByte(Mid(sTxt, i, 1))
    '    Next
    '    bTxt(iLen - 1) = &H0
    '    prop.Id = iId
    '    prop.Type = 2
    '    prop.Value = bTxt
    '    prop.Len = iLen
    'End Sub

    Private Function ScaricaFileDaPagina(sUrl As String, Modalita As String) As Integer
        Dim Url As String = sUrl
        Dim Ritorno As Integer = 0

        'If Mid(Url, 1, 1) = Chr(34) Or Mid(Url, 1, 1) = "'" Then
        '    Url = Mid(Url, 2, Url.Length)
        'End If
        'If Mid(Url, Url.Length, Url.Length) = Chr(34) Or Mid(Url, Url.Length, Url.Length) = "'" Then
        '    Url = Mid(Url, 1, Url.Length - 1)
        'End If

        If Not ControllaSeSitoDaSaltare(Url) Then
            Return 0
            Exit Function
        End If

        Dim Direct As String = Url
        Direct = Mid(Direct, 8, Direct.Length)
        For i As Integer = 1 To Direct.Length - 1
            If Mid(Direct, i, 1) = "\" Or Mid(Direct, i, 1) = "/" Then
                Direct = Mid(Direct, 1, i - 1)
                Exit For
            End If
        Next
        Application.DoEvents()

        Dim NomeFile As String = ""
        Dim p As New picDropCls

        For i As Integer = Url.Length To 1 Step -1
            If Mid(Url, i, 1) = "/" Or Mid(Url, i, 1) = "\" Then
                NomeFile = Mid(Url, i + 1, Url.Length)
                Exit For
            End If
        Next
        Application.DoEvents()
        If NomeFile = "" Then
            NomeFile = Now.Year & Format(Now.Month, "00") & Format(Now.Day, "00") & Format(Now.Hour, "00") & Format(Now.Minute, "00") & Format(Now.Second, "00") & ".jpg"
        End If

        If NomeFile.IndexOf("?") > -1 Then
            ' NomeFile = Mid(NomeFile, 1, NomeFile.IndexOf("?"))
            NomeFile = NomeFile.Replace("?", "_")
        End If
        Application.DoEvents()

        Dim gf As New GestioneFilesDirectory
        Dim sNomeFile As String

        If Modalita = "FILES" Then
            sNomeFile = "Links\FilesVari\" & NomeFile
            gf.CreaDirectoryDaPercorso("Links\FilesVari\")
        Else
            If PathSito Then
                sNomeFile = "Links\" & Direct & "\" & NomeFile
                gf.CreaDirectoryDaPercorso("Links\" & Direct & "\")
            Else
                sNomeFile = "Links\Scaricate\" & NomeFile
                gf.CreaDirectoryDaPercorso("Links\Scaricate\")
            End If
        End If
        Application.DoEvents()

        sNomeFile = sNomeFile.Replace("+", "_")

        Dim FilesImmagini() As String = {".JPG", ".JPEG", ".PNG", ".BMP", ".GIF", ".PCX", ".IMG"}
        For Each S As String In FilesImmagini
            If sNomeFile.ToUpper.Contains(S) Then
                sNomeFile = Mid(sNomeFile, 1, sNomeFile.ToUpper.IndexOf(S) + S.Length)
                Exit For
            End If
        Next

        If sNomeFile.IndexOf("?") > -1 Then
            sNomeFile = Mid(sNomeFile, 1, sNomeFile.IndexOf("?"))
        End If
        sNomeFile = sNomeFile.Replace("+", "_").Replace("*", "_").Replace("|", "_").Replace(">", "_").Replace("<", "_")

        If sNomeFile.ToUpper.Contains(".IMG") Then
            Dim a As Integer = sNomeFile.ToUpper.IndexOf(".IMG")
            sNomeFile = Mid(sNomeFile, 1, a) & ".Jpg"
        End If

        If Sovrascrivi Then
        Else
            sNomeFile = gf.NomeFileEsistente(sNomeFile)
        End If

        Dim NomeSito As String = Url
        Dim sResto As String
        Dim Resto() As String = {}
        Dim a1 As Integer = Mid(NomeSito, 11, NomeSito.Length).IndexOf("/")
        If a1 > -1 Then
            Application.DoEvents()
            sResto = Mid(NomeSito, a1 + 11, NomeSito.Length)
            If Mid(sResto, 1, 1) = "/" Then
                sResto = Mid(sResto, 2, sResto.Length)
            End If
            Resto = sResto.Split("/")

            NomeSito = Mid(NomeSito, 1, a1 + 10)
            NomeSito = NomeSito.ToUpper.Replace("HTTPS://", "").Replace("HTTP://", "")
        End If

        lblDimensioni.Visible = False
        Application.DoEvents()

        'If Url.ToUpper.Contains("MSN.COM/C.GIF?") Then
        '    sNomeFile = "Links\Msn.htm"
        'End If

        'If Not sNomeFile.Contains(Application.StartupPath & "\") Then
        '    sNomeFile = Application.StartupPath & "\" & sNomeFile
        'End If

        ScaricaFileDallaRete(Url, sNomeFile, Me, lblDimensioni, varConnessione, conn)

        Ritorno = SistemaFileScaricato(sNomeFile, lblDimensioni, varConnessione, conn, Url, NomeSito, Resto, Me)

        gf = Nothing

        Return Ritorno
    End Function

    Private Function ScaricaPagina(Url As String) As Integer
        Dim Quanti As Integer

        StaScaricandoPagina = True
        'mnuApreMaschera.Disabilita()
        mnuScaricaLink.Disabilita()
        mnuRiprovaLinks.Disabilita()
        'mnuStatistiche.Disabilita()
        'mnuSistemaLink.Disabilita()
        'mnuImpostazioni.Disabilita()
        mnuUtility.Disabilita()
        'mnuBloccaDownload.Disabilita()
        'mnuUscita.Disabilita()

        NotifyIcon1.Icon = New Icon("Icone\18.ICO")

        Dim sNomeFile As String = "Links\Appoggio.html"
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
                If Url.ToUpper.Contains("C:\") Or Url.ToUpper.Contains("D:\") Then
                    If File.Exists(Url) Then
                        FileCopy(Url, sNomeFile)
                    End If
                Else
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
                End If
            Else
                If Url.ToUpper.Contains("C:\") Or Url.ToUpper.Contains("D:\") Then
                    If File.Exists(Url) Then
                        FileCopy(Url, sNomeFile)
                    End If
                Else
                    ScaricaFileGlobale(Url, sNomeFile, varConnessione, conn)
                End If
            End If

            If File.Exists(sNomeFile) Then
                sourcecode = gf.LeggeFileIntero(sNomeFile)

                Dim a As Long
                Dim Appoggio As String
                Dim Inizio As Long
                Dim Fine As Long
                Dim Scaricate As Integer = 0
                Dim Cambia As String
                Dim sourceCodeOriginale As String = sourcecode

                sourcecode = sourcecode.Replace("%253A", ":")
                sourcecode = sourcecode.Replace("%252F", "/")
                sourcecode = sourcecode.Replace("&amp;", "&")
                sourcecode = sourcecode.Replace("%253F", "?")
                sourcecode = sourcecode.Replace("%2526", "?")
                sourcecode = sourcecode.Replace("%253D", "=")
                sourcecode = sourcecode.Replace("%2B", "+")
                sourcecode = sourcecode.Replace("\/", "//")
                sourcecode = sourcecode.Replace("&amp;", "&")
                sourcecode = sourcecode.Replace("&quot;", Chr(34))

                If qualePagina = 0 Then
                    pagineHtml = ControllaSeCiSonoSiti(sourcecode)
                End If

                ' Immagini
                a = ControllaSeCiSonoImmagini(sourcecode)
                    Do While a > -1
                        Appoggio = Mid(sourcecode, a, sourcecode.Length)

                        Inizio = -1
                        For i As Long = a To 1 Step -1
                            For k As Integer = 0 To CaratteriStop1.Length - 1
                                If Mid(sourcecode, i, 1) = CaratteriStop1(k) Then
                                    Inizio = i + 1
                                    Exit For
                                End If
                            Next
                            If Inizio <> -1 Then
                                Exit For
                            End If
                        Next
                        Fine = -1
                        For i As Long = a + 1 To sourcecode.Length - 1
                            For k As Integer = 0 To CaratteriStop2.Length - 1
                                If Mid(sourcecode, i, 1) = CaratteriStop2(k) Then
                                    Fine = i '+ 1
                                    Exit For
                                End If
                            Next
                            If Fine <> -1 Then
                                Exit For
                            End If
                        Next
                        Appoggio = Mid(sourcecode, Inizio, Fine - Inizio)

                        Cambia = Appoggio

                        Appoggio = Appoggio.Replace(vbCrLf, "")

                        Appoggio = Appoggio.Trim

                        ' Inline image
                        If Appoggio.ToUpper.Contains("CID:") Then
                            GestisceImmagineInline(sourcecode, Appoggio)
                        Else
                            For Each s As String In FilesImmagini
                                If Appoggio.ToUpper.Contains(s) Then
                                    Appoggio = Mid(Appoggio, 1, Appoggio.ToUpper.IndexOf(s) + s.Length)
                                    Exit For
                                End If
                            Next

                            If Mid(Appoggio, 1, 7).ToUpper <> "HTTP://" And Mid(Appoggio, 1, 8).ToUpper <> "HTTPS://" Then
                                If Appoggio.ToUpper.Contains("MSN.COM/C.GIF?") Then
                                    Appoggio = "http://" & Appoggio
                                Else
                                    If Mid(Appoggio, 1, 2) <> "//" And Mid(Appoggio, 1, 2) <> ".." Then
                                        If Mid(Appoggio, 1, 1) <> "/" Then
                                            Appoggio = "/" & Appoggio
                                        End If
                                        Appoggio = sito & Appoggio
                                    Else
                                        If Mid(Appoggio, 1, 2) = ".." Then
                                            Appoggio = Mid(Appoggio, 3, Appoggio.Length)
                                            Appoggio = sito & Appoggio
                                        End If
                                        If Mid(Appoggio, 1, 2) = "//" Then
                                            Appoggio = "http:" & Appoggio
                                        End If
                                    End If
                                End If
                            End If

                            If Appoggio.ToUpper.Contains(".IMG?H") And Not Appoggio.ToUpper.Contains("HTTP:") And Not Appoggio.ToUpper.Contains("HTTPS:") Then
                                Appoggio = "http:" & Appoggio
                                ' Appoggio = Mid(Appoggio, 1, Appoggio.IndexOf("?"))
                            End If

                            Dim appSASGS As Boolean = ScaricaAncheSeGiaScaricata
                            If Url.ToUpper.Contains("C:\") Or Url.ToUpper.Contains("D:\") Then
                                ScaricaAncheSeGiaScaricata = True
                            End If
                            Scaricate += ScaricaFileDaPagina(Appoggio, "IMMAGINI")
                            If Url.ToUpper.Contains("C:\") Or Url.ToUpper.Contains("D:\") Then
                                ScaricaAncheSeGiaScaricata = appSASGS
                            End If
                            'End If
                        End If

                        sourcecode = sourcecode.Replace(Cambia, "")

                        a = ControllaSeCiSonoImmagini(sourcecode)

                        If BloccaDownloadPagina Then
                            Exit Do
                        End If
                    Loop

                    If Not BloccaDownloadPagina Then
                        ' Files
                        sourcecode = sourceCodeOriginale
                        a = ControllaSeCiSonoFilesVari(sourcecode)
                        Do While a > -1
                            Appoggio = Mid(sourcecode, a, sourcecode.Length)
                            For i As Long = a To 1 Step -1
                                If Mid(sourcecode, i, 1) = Chr(34) Or Mid(sourcecode, i, 1) = "'" Or Mid(sourcecode, i, 1) = " " Then
                                    Inizio = i
                                    Exit For
                                End If
                            Next
                            For i As Long = a To sourcecode.Length - 1
                                If Mid(sourcecode, i, 1) = Chr(34) Or Mid(sourcecode, i, 1) = "'" Or Mid(sourcecode, i, 1) = " " Then
                                    Fine = i + 1
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
                                    Scaricate += ScaricaFileDaPagina(Appoggio, "FILES")
                                End If
                            End If

                            sourcecode = sourcecode.Replace(Cambia, "")

                            a = ControllaSeCiSonoFilesVari(sourcecode)
                        Loop

                        gf = Nothing

                        Try
                            Kill(sNomeFile)
                        Catch ex As Exception

                        End Try
                    End If

                    NotifyIcon1.Icon = New Icon("Icone\DRAG1PG.ICO")

                    Quanti = Scaricate
                Else
                    Quanti = 0
            End If

            'MsgBox("Operazione effettuata:" & vbCrLf & vbCrLf & "Immagini scaricate: " & Scaricate)
        Catch ex As Exception
            Me.BackgroundImage = Image.FromFile("Icone\errore.png")
            tmrDownload.Enabled = True
            tmrControllo.Enabled = False
        End Try

        If Not BloccaDownloadPagina Then
            If pagineHtml.Count > 0 Then
                qualePagina += 1
                If qualePagina < pagineHtml.Count Then
                    ScaricaPagina(pagineHtml.Item(qualePagina))
                End If
            End If
        End If

            'mnuApreMaschera.Abilita()
            'mnuStatistiche.Abilita()
            'mnuSistemaLink.Abilita()
            'mnuImpostazioni.Abilita()
            mnuUtility.Abilita()
        'If mnuBloccaDownload.LeggeTesto <> StringaRiprendeDownload Then
        '    mnuBloccaDownload.Abilita()
        'End If
        'mnuUscita.Abilita()

        StaScaricandoPagina = False

        Return Quanti
    End Function

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        mnuApreMaschera = New GestioneMenu("Verdana", 9, "Apre maschera", "Icone\Menu\Maschera_si.png", 24, New EventHandler(AddressOf ApreMaschera), Nothing)
        mnuScaricaLink = New GestioneMenu("Verdana", 9, "Scarica Link", "Icone\Menu\ScaricaLink.png", 24, New EventHandler(AddressOf AggiungeLink), Nothing)
        mnuStatistiche = New GestioneMenu("Verdana", 9, "Statistiche", "Icone\Menu\excel.png", 24, New EventHandler(AddressOf Statistiche), Nothing)
        mnuRicerca = New GestioneMenu("Verdana", 9, "Ricerca", "Icone\Menu\ricerca.png", 24, New EventHandler(AddressOf Ricerca), Nothing)
        mnuRiprovaLinks = New GestioneMenu("Verdana", 9, "Riprova Link", "Icone\Menu\download.png", 24, New EventHandler(AddressOf RiprovaLink), Nothing)
        mnuSistemaLink = New GestioneMenu("Verdana", 9, "Sistema Links", "Icone\Menu\ScodaLinks.png", 24, New EventHandler(AddressOf ScodaLinks), Nothing)
        mnuImpostazioni = New GestioneMenu("Verdana", 9, "Impostazioni", "Icone\Menu\Impostazioni.png", 24, New EventHandler(AddressOf Impostazioni), Nothing)
        mnuUtility = New GestioneMenu("Verdana", 9, "Utility", "Icone\Menu\Utility.png", 24, New EventHandler(AddressOf ApreUtility), Nothing)
        mnuBloccaDownload = New GestioneMenu("Verdana", 9, StringaBloccaDownload, "Icone\Menu\Blocca.png", 24, New EventHandler(AddressOf BloccaDownload), Nothing)
        mnuListaDownload = New GestioneMenu("Verdana", 9, "Lista downloads", "Icone\Menu\Statistiche.png", 24, New EventHandler(AddressOf ListaDownload), Nothing)
        mnuUscita = New GestioneMenu("Verdana", 9, "Uscita", "Icone\Menu\Uscita.png", 24, New EventHandler(AddressOf Uscita), Nothing)
        menuItemSeparatore = New GestioneMenu("Verdana", 9, "-", "", 0, Nothing, Nothing)

        Me.contextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() _
                            {Me.mnuApreMaschera, Me.menuItemSeparatore, Me.mnuRiprovaLinks, Me.mnuScaricaLink, Me.mnuStatistiche, Me.mnuSistemaLink, Me.mnuRicerca, Me.mnuImpostazioni, Me.mnuUtility, Me.mnuBloccaDownload, _
                             Me.mnuListaDownload, menuItemSeparatore, Me.mnuUscita})

        NotifyIcon1.Icon = New Icon("Icone\DRAG1PG.ICO")
        NotifyIcon1.Text = "picDrop"
        NotifyIcon1.ContextMenu = Me.contextMenu1
        NotifyIcon1.Visible = True

        mnuBloccaDownload.Disabilita()
    End Sub

    Public Sub ApreConnessione()
        varConnessione = New SQLSERVERCE
        varConnessione.ImpostaNomeDB(Application.StartupPath & "\DB\picDrop.sdf")
        varConnessione.LeggeImpostazioniDiBase()
        conn = varConnessione.ApreDB()
        tmrControllo.Enabled = True
        NotifyIcon1.Visible = True
    End Sub

    Public Sub ChiudeConnessione()
        conn.Close()
        varConnessione = Nothing
        tmrControllo.Enabled = False
    End Sub

    Public Sub BloccaTimerDownload()
        tmrControllo.Enabled = False
        NotifyIcon1.Icon = New Icon("Icone\DRAG1PG.ICO")
        StaScaricandoPagina = False
        BloccaDownloadPagina = True
        mnuBloccaDownload.Disabilita()

        mnuScaricaLink.Abilita()
        mnuRiprovaLinks.Abilita()
        mnuImpostazioni.Abilita()
        mnuUtility.Abilita()

        StavaScaricando = True
    End Sub

    Public Sub FaiRipartireTimerDownload()
        NotifyIcon1.Icon = New Icon("Icone\18.ICO")
        StaScaricandoPagina = True
        BloccaDownloadPagina = False
        mnuBloccaDownload.Abilita()

        mnuScaricaLink.Disabilita()
        mnuRiprovaLinks.Disabilita()
        mnuImpostazioni.Disabilita()
        mnuUtility.Disabilita()

        tmrControllo.Enabled = True
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If NonFareLoad Then
            NonFareLoad = False
            Exit Sub
        End If

        Dim u As UpdateDB = New UpdateDB
        u.ControllaAggiornamentoDB()

        ApreConnessione()

        LeggeSettaggi()

        ' lblDirSalvataggio.Text = NomeDirBase

        CreaTastiDir()

        'cmbDir.Items.Clear()
        'For i As Integer = 0 To Cartelle.Length - 1
        '    cmbDir.Items.Add(Nomi(i))
        'Next
        'If NomeDirBase <> "" Then
        '    cmbDir.Text = NomeDirBase
        'End If
        'cmbDir.Visible = False

        Me.TopMost = True

        Me.Width = 100
        Me.Height = 125

        'cmbDir.Width = Me.Width - 15
        'lblDirSalvataggio.Width = cmbDir.Width
        'lblDimensioni.Width = cmbDir.Width

        Me.Left = -300
        Me.Top = -300

        LeggeStatistiche()

        Visibile = False

        Try
            MkDir("Buttami")
        Catch ex As Exception

        End Try

        Try
            MkDir("Uguali")
        Catch ex As Exception

        End Try

        Try
            MkDir("Links")
        Catch ex As Exception

        End Try

        pbDownload.Visible = False
        StaElaborando = False
        ListaDownloadVisibile = False
    End Sub

    Private Sub CreaTastiDir()
        'ReDim BottoniDirs(Cartelle.Length - 1)

        'Dim myFont As System.Drawing.Font
        'myFont = New System.Drawing.Font("Arial", 8, FontStyle.Regular)

        'For i As Integer = 0 To Cartelle.Length - 1
        '    BottoniDirs(i) = New Button
        '    BottoniDirs(i).Left = (i * 35) + 1
        '    BottoniDirs(i).Top = 0
        '    BottoniDirs(i).Width = 35
        '    BottoniDirs(i).Font = myFont
        '    BottoniDirs(i).Text = Nomi(i)
        '    If Nomi(i) = NomeDirBase Then
        '        BottoniDirs(i).BackColor = Color.LightGreen
        '    Else
        '        BottoniDirs(i).BackColor = Color.LightGray
        '    End If
        '    AddHandler BottoniDirs(i).Click, AddressOf TastoClickato

        '    Me.Controls.Add(BottoniDirs(i))
        'Next
        ''If NomeDirBase <> "" Then
        ''    cmbDir.Text = NomeDirBase
        ''End If
    End Sub

    Private Sub LeggeStatistiche()
        Dim sql As String = "Select * From Statistiche"
        Dim rec As Object = "ADODB.Recordset"
        rec = varConnessione.LeggeQuery(conn, sql)
        If rec.Eof = False Then
            numFiles = rec("numFiles").Value
            totKBytes = rec("Dimensioni").Value
            QuantiFilesVecchi = numFiles
            rec.Close()
        Else
            rec.Close()
            numFiles = 0
            totKBytes = 0
            QuantiFilesVecchi = 0

            sql = "Insert Into Statistiche Values(0,0)"
            varConnessione.EsegueSql(conn, sql)
        End If
        'conn.Close()
        'g = Nothing
    End Sub

    Private Sub RiprovaLink(Sender As Object, e As EventArgs)
        tmrControllo.Enabled = True
    End Sub

    Private Sub Ricerca(Sender As Object, e As EventArgs)
        If Not MascheraRicerca Then
            frmRicerca.Show()
            MascheraRicerca = True
        Else
            frmRicerca.Close()
            MascheraRicerca = False
        End If
    End Sub

    Private Sub ListaDownload(Sender As Object, e As EventArgs)
        If Not ListaDownloadVisibile Then
            frmListaDownload.Show()
            ListaDownloadVisibile = True
        Else
            frmListaDownload.Close()
            ListaDownloadVisibile = False
        End If
    End Sub

    Private Sub ApreMaschera(Sender As Object, e As EventArgs) 'Handles menuItem1.Click
        If Visibile = True Then
            NonFareLoad = True

            Me.Left = -300
            Me.Top = -300
            Visibile = False
            mnuApreMaschera.ImpostaTesto("Apre maschera")
            mnuApreMaschera.ImpostaImmagine("Icone\Menu\maschera_si.png", 24)

            If StavaScaricando Then
                NotifyIcon1.Icon = New Icon("Icone\18.ICO")
                StaScaricandoPagina = True
                BloccaDownloadPagina = False
                mnuBloccaDownload.Abilita()

                mnuScaricaLink.Disabilita()
                mnuRiprovaLinks.Disabilita()
                mnuImpostazioni.Disabilita()
                mnuUtility.Disabilita()

                tmrControllo.Enabled = True
            End If
        Else
            NonFareLoad = True

            Dim x As Integer = GetSetting("picDrop", "Settaggi", "PosX", -300)
            Dim y As Integer = GetSetting("picDrop", "Settaggi", "PosY", -300)

            If x <> -300 Or y <> -300 Then
                Me.Left = x
                Me.Top = y
            Else
                Me.Left = screenWidth - Me.Width - 10
                Me.Top = screenHeight - Me.Height - 50
            End If
            Visibile = True
            mnuApreMaschera.ImpostaTesto("Nasconde maschera")
            mnuApreMaschera.ImpostaImmagine("Icone\Menu\maschera_no.png", 24)

            If StaScaricandoPagina Then
                'trd.Abort()
                'trd = Nothing

                NotifyIcon1.Icon = New Icon("Icone\DRAG1PG.ICO")
                StaScaricandoPagina = False
                BloccaDownloadPagina = True
                mnuBloccaDownload.Disabilita()

                mnuScaricaLink.Abilita()
                mnuRiprovaLinks.Abilita()
                mnuImpostazioni.Abilita()
                mnuUtility.Abilita()

                StavaScaricando = True
            Else
                StavaScaricando = False
            End If

            NotifyIcon1.Text = "picDrop"
            Application.DoEvents()
        End If
    End Sub

    Private Sub ScodaLinks(Sender As Object, e As EventArgs) ' Handles menuItem6.Click
        If StaElaborando = False Then
            tmrControllo.Enabled = False
            frmScodaLinks.ImpostaConnessione(varConnessione, conn)
            frmScodaLinks.Visible = True

            'NotifyIcon1.Visible = False
        End If
    End Sub

    Private Sub ApreUtility(Sender As Object, e As EventArgs) 'Handles menuItem8.Click
        If StaElaborando = False Then
            tmrControllo.Enabled = False
            Utility.ImpostaConnessione(varConnessione, conn)
            Utility.Visible = True

            NotifyIcon1.Visible = False
        End If
    End Sub

    Private Sub Uscita(Sender As Object, e As EventArgs) 'Handles menuItem2.Click
        'If StaScaricandoPagina = True Then
        '    Exit Sub
        'End If

        NotifyIcon1.Icon = New Icon("Icone\DRAG1PG.ICO")
        StaScaricandoPagina = False
        BloccaDownloadPagina = True
        mnuBloccaDownload.Disabilita()

        mnuScaricaLink.Abilita()
        mnuRiprovaLinks.Abilita()
        mnuImpostazioni.Abilita()
        mnuUtility.Abilita()

        StavaScaricando = False

        conn.Close()
        varConnessione = Nothing

        NotifyIcon1.Visible = False
        End
    End Sub

    Private Sub BloccaDownload(Sender As Object, e As EventArgs) ' Handles menuItem7.Click
        If mnuBloccaDownload.LeggeTesto = StringaBloccaDownload Then
            BloccoDownloadForzato = True

            BloccaDownloadPagina = True
            mnuBloccaDownload.ImpostaTesto(StringaRiprendeDownload)

            NotifyIcon1.Icon = New Icon("Icone\DRAG1PG.ICO")
            StaScaricandoPagina = False
            StavaScaricando = True

            mnuScaricaLink.Abilita()
            mnuRiprovaLinks.Abilita()
            mnuImpostazioni.Abilita()

            mnuBloccaDownload.ImpostaImmagine("Icone\Menu\Blocca.png", 24)
        Else
            mnuBloccaDownload.ImpostaImmagine("Icone\errore.png", 24)

            mnuBloccaDownload.ImpostaTesto(StringaBloccaDownload)
            BloccaDownloadPagina = False
            If Rimanenti > 0 Then
                tmrControllo.Enabled = True
            End If
        End If
    End Sub

    Public Function MetteInCodaLink(sUrl As String, Optional Automatico As Boolean = False) As Boolean
        Dim sql As String
        Dim rec As Object = CreateObject("ADODB.Recordset")
        Dim Ritorno As Boolean = True

        sql = "Select * From LinksDaScaricare Where Link='" & sUrl.Replace("'", "''") & "'"
        rec = varConnessione.LeggeQuery(conn, sql)
        If rec.Eof = True Then
            rec.Close()

            sql = "Insert Into LinksDaScaricare Values ('" & sUrl.Replace("'", "''") & "', 'N', 0)"
            varConnessione.EsegueSql(conn, sql)
        Else
            rec.Close()

            If frmAggiungeLink.chkForza.Checked Then
                sql = "Update LinksDaScaricare Set Scaricato='N', Quanti=0 Where Link='" & sUrl.Replace("'", "''") & "'"
                varConnessione.EsegueSql(conn, sql)
            Else
                If Automatico = False Then
                    Ritorno = False
                    MsgBox("Link già inserito")
                End If
            End If
        End If

        Return Ritorno
    End Function

    Private Sub AggiungeLink(Sender As Object, e As EventArgs) ' Handles menuItem5.Click
        frmAggiungeLink.Show()
    End Sub

    Private Sub Impostazioni(Sender As Object, e As EventArgs) ' Handles menuItem3.Click
        'If StaScaricandoPagina = True Then
        '    Exit Sub
        'End If

        'If StaElaborando = False Then
        frmSettaggi.Visible = True
        'End If
    End Sub

    Private Sub Statistiche(Sender As Object, e As EventArgs) ' Handles menuItem4.Click
        'If StaScaricandoPagina = True Then
        '    Exit Sub
        'End If

        'If StaElaborando = False Then
        frmStatistiche.Visible = True
        'End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles tmrDownload.Tick
        tmrDownload.Enabled = False
        lblDimensioni.Visible = False
        'SecondiPassati = 0
        'tmrControllo.Enabled = True

        Try
            Me.BackgroundImage.Dispose()
            Me.BackgroundImage = Nothing
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub Label1_Click(sender As Object, e As EventArgs) Handles lblDirSalvataggio.Click
    '    If cmbDir.Visible = True Then
    '        cmbDir.Visible = False
    '    Else
    '        cmbDir.Visible = True
    '    End If
    'End Sub

    'Private Sub cmbDir_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    For i As Integer = 0 To Nomi.Length - 1
    '        If Nomi(i) = cmbDir.Text Then
    '            lblDirSalvataggio.Text = Nomi(i)

    '            NomeDirBase = Nomi(i)
    '            DirBase = Cartelle(i)

    '            SaveSetting("picDrop", "Settaggi", "NomeDirBase", NomeDirBase)
    '            SaveSetting("picDrop", "Settaggi", "DirBase", DirBase)

    '            cmbDir.Visible = False
    '            Exit For
    '        End If
    '    Next
    'End Sub

    Private Sub ControllaLinksDascaricare()
        BloccoDownloadForzato = False
        'If StaScaricandoPagina = True Or BloccaDownloadPagina = True Then
        '    Exit Sub
        'End If

        Dim sql As String = "Select * From LinksDaScaricare Where Scaricato='N'"
        Dim rec As Object = CreateObject("ADODB.Recordset")
        Dim Ritorno As String = ""

        rec = varConnessione.LeggeQuery(conn, sql)
        If rec.Eof = False Then
            Ritorno = rec("Link").Value
        Else
            Ritorno = ""
        End If
        rec.Close()

        If Ritorno <> "" Then
            mnuBloccaDownload.Abilita()
            mnuBloccaDownload.ImpostaImmagine("Icone\errore.png", 24)

            sql = "Select count(*) From LinksDaScaricare Where Scaricato='N'"
            rec = varConnessione.LeggeQuery(conn, sql)
            If rec(0).Value Is DBNull.Value = True Then
                Rimanenti = 0
            Else
                Rimanenti = rec(0).Value
            End If
            rec.Close()

            Dim Appo As String = NotifyIcon1.Text
            Dim sRitorno As String = Ritorno

            If sRitorno.Length > 22 Then
                sRitorno = Mid(sRitorno, 1, 10) & ".." & Mid(sRitorno, Ritorno.Length - 10, Ritorno.Length)
            End If
            NotifyIcon1.Text = "Download: " & sRitorno & "-Rim. " & Rimanenti
            Application.DoEvents()

            If ApreFinestra = True Then
                Me.Left = screenWidth - Me.Width - 10
                Me.Top = screenHeight - Me.Height - 50
                mnuApreMaschera.Text = "N&asconde maschera"
                ' lblDirSalvataggio.Visible = False
                Me.Enabled = False
            End If

            qualePagina = 0
            pagineHtml = New List(Of String)
            Dim Quanti As Integer = ScaricaPagina(Ritorno)

            sql = "Update LinksDaScaricare Set Scaricato='S', Quanti=" & Quanti & " Where Link='" & Ritorno.Replace("'", "''") & "'"
            varConnessione.EsegueSql(conn, sql)

            If ApreFinestra = True Then
                Me.Enabled = True
                ' lblDirSalvataggio.Visible = True
                Me.Left = -300
                Me.Top = -300
                mnuApreMaschera.Text = "A&pre maschera"
            End If

            NotifyIcon1.Text = Appo
            Application.DoEvents()

            'mnuBloccaDownload.Disabilita()
        End If

        'conn.Close()
        'g = Nothing

        Try
            If Not Me.BackgroundImage Is Nothing Then
                Me.BackgroundImage.Dispose()
                Me.BackgroundImage = Nothing
            End If
        Catch ex As Exception

        End Try

        If Rimanenti > 0 And Not BloccaDownloadPagina And Ritorno <> "" Then
            tmrControllo.Enabled = True
        Else
            NotifyIcon1.Icon = New Icon("Icone\DRAG1PG.ICO")
            StaScaricandoPagina = False
            BloccaDownloadPagina = True
            If Not BloccoDownloadForzato Then
                mnuBloccaDownload.Disabilita()
            End If

            mnuScaricaLink.Abilita()
            mnuRiprovaLinks.Abilita()
            mnuImpostazioni.Abilita()
            mnuUtility.Abilita()

            StavaScaricando = False
        End If
    End Sub

    Private Sub tmrControllo_Tick(sender As Object, e As EventArgs) Handles tmrControllo.Tick
        'If Visibile = False And StaScaricandoPagina = False Then
        'SecondiPassati += 1

        tmrControllo.Enabled = False
        BloccaDownloadPagina = False

        'trd = New Thread(AddressOf ControllaLinksDascaricare)
        'trd.IsBackground = True
        'trd.Start()

        ControllaLinksDascaricare()

        'If SecondiPassati > 10 Then
        'Dim gf As New GestioneFilesDirectory
        'Dim Compatta As Boolean = False

        '' Passato un periodo di inattività fa le operazioni nel sottobosco 
        '' per individuare eventuali doppioni di immagini
        'Dim NomeFileSalvato As String = ""
        'Dim Quale As String = ""

        'Dim rec As Object = "ADODB.Recordset"
        'Dim sql As String = "Select * From Indirizzi Where CRC='' Or CRC Is Null"
        'rec = varConnessione.LeggeQuery(conn, sql)
        'If rec.Eof = False Then
        '    Quale = rec("Url").Value
        '    NomeFileSalvato = rec("NomeFile").Value
        'End If
        'rec.Close()

        'If NomeFileSalvato <> "" Then
        '    If File.Exists(NomeFileSalvato) = True Then
        '        StaElaborando = True
        '        NotifyIcon1.Icon = New Icon("Icone\Find.ico")

        '        Dim p As New picDropCls
        '        Dim Struttura As String = p.ConverteImmagine(NomeFileSalvato)
        '        Dim Numerone As Long = 0
        '        For i As Integer = 0 To Struttura.Length - 1
        '            Numerone += (Val(Struttura.Substring(i, 1))) * (i + 1)
        '        Next
        '        ' sql = "Update Indirizzi Set Dett='" & Struttura & "', CRC='" & Numerone & "' Where Url='" & Quale & "'"
        '        sql = "Update Indirizzi Set CRC='" & Numerone & "' Where Url='" & Quale.Replace("'", "''") & "'"
        '        varConnessione.EsegueSql(conn, sql)
        '        p = Nothing

        '        NotifyIcon1.Icon = New Icon("Icone\DRAG1PG.ICO")
        '        StaElaborando = False
        '    Else
        '        'sql = "Update Indirizzi Set Dett='SPOSTATA', CRC=-1 Where Url='" & Quale & "'"
        '        sql = "Update Indirizzi Set CRC=-1 Where Url='" & Quale.Replace("'", "''") & "'"
        '        varConnessione.EsegueSql(conn, sql)
        '    End If
        'Else
        '    If QuantiFilesVecchi <> numFiles Or AppenaPartito = True Then
        '    StaElaborando = True

        '    If AppenaPartito = True Then AppenaPartito = False

        '    ' Non c'è niente da elaborare. Controllo doppioni
        '    ' ma solo se il numero dei files è cambiato
        '    QuantiFilesVecchi = numFiles

        '    Dim Primo As String
        '    Dim Dime1 As String
        '    Dim PrimoFile As String
        '    Dim DataPrima As Date
        '    Dim Secondo As String
        '    Dim Dime2 As String
        '    Dim SecondoFile As String
        '    Dim DataSeconda As Date
        '    Dim idPrimo As String
        '    Dim idSecondo As String
        '    'Dim Errori As Integer
        '    Dim Eliminati() As String = {}
        '    Dim qEliminati As Integer = 0
        '    'Dim Progressivo As Integer = 0
        '    Dim gi As New GestioneImmagini

        '    NotifyIcon1.Icon = New Icon("Icone\46.ICO")
        '    ' sql = "Select * From Indirizzi Where Dett<>'SPOSTATA' Order By CRC"
        '    sql = "Select * From Indirizzi Where CRC<>'' And CRC Is Not Null And CRC<>'-1' Order By CRC"
        '    rec = varConnessione.LeggeQuery(conn, sql)
        '    Do Until rec.Eof
        '        If Visibile = True Then
        '            Exit Do
        '        End If

        '        ' Primo = rec("Dett").Value
        '        Primo = rec("CRC").Value
        '        Dime1 = rec("Dimensione").Value
        '        DataPrima = rec("DataOra").Value
        '        PrimoFile = rec("NomeFile").Value
        '        idPrimo = rec("Url").Value

        '        rec.MoveNext()

        '        If rec.EOF = False Then
        '            'Secondo = rec("Dett").Value
        '            Secondo = rec("CRC").Value
        '            Dime2 = rec("Dimensione").Value
        '            DataSeconda = rec("DataOra").Value
        '            SecondoFile = rec("NomeFile").Value
        '            idSecondo = rec("url").Value

        '            If Visibile = True Then
        '                Exit Do
        '            End If

        '            If PrimoFile <> SecondoFile And Primo = Secondo Then 'And Dime1 = Dime2
        '                'Errori = 0
        '                'For I = 0 To Primo.Length - 1 Step 3
        '                '    If Primo.Substring(I, 3) <> Secondo.Substring(I, 3) Then
        '                '        Errori = Errori + 1
        '                '        If Errori > 8 Then
        '                '            Exit For
        '                '        End If
        '                '    End If
        '                'Next I

        '                'If Errori <= 8 Then
        '                NotifyIcon1.Icon = New Icon("Icone\63.ICO")

        '                Dim FileSpostato As String = ""
        '                Dim QualeFile As String
        '                Dim AltroFile As String = ""

        '                Dim DimeI1() As String = gi.RitornaDimensioneImmagine(PrimoFile).Split("x")
        '                Dim dx1 As Single = DimeI1(0) * DimeI1(1)
        '                Dim DimeI2() As String = gi.RitornaDimensioneImmagine(SecondoFile).Split("x")
        '                Dim dx2 As Single = DimeI2(0) * DimeI2(1)

        '                If dx1 > dx2 Then
        '                    QualeFile = PrimoFile
        '                    AltroFile = gf.TornaNomeFileDaPath(SecondoFile)
        '                Else
        '                    If dx1 < dx2 Then
        '                        QualeFile = SecondoFile
        '                        AltroFile = gf.TornaNomeFileDaPath(PrimoFile)
        '                    Else
        '                        If DataPrima > DataSeconda Then
        '                            QualeFile = PrimoFile
        '                            AltroFile = gf.TornaNomeFileDaPath(SecondoFile)
        '                        Else
        '                            QualeFile = SecondoFile
        '                            AltroFile = gf.TornaNomeFileDaPath(PrimoFile)
        '                        End If
        '                    End If
        '                End If

        '                For i As Integer = QualeFile.Length To 1 Step -1
        '                    If Mid(QualeFile, i, 1) = "\" Or Mid(QualeFile, i, 1) = "/" Then
        '                        FileSpostato = "Uguali\" & Mid(QualeFile, i + 1, QualeFile.Length) & " Uguale a " & AltroFile
        '                        Exit For
        '                    End If
        '                Next

        '                Try
        '                    FileCopy(QualeFile, FileSpostato)
        '                    Kill(QualeFile)
        '                Catch ex As Exception

        '                End Try

        '                qEliminati += 1
        '                ReDim Preserve Eliminati(qEliminati)
        '                Eliminati(qEliminati) = idSecondo

        '                NotifyIcon1.Icon = New Icon("Icone\46.ICO")
        '                'End If
        '            End If
        '        End If
        '    Loop
        '    rec.Close()

        '    gi = Nothing

        '    If Visibile = False Then
        '        '' Elimina valori che non sono più presenti come files
        '        'sql = "Select * From Indirizzi"
        '        'rec = varConnessione.LeggeQuery(conn, sql)
        '        'Do Until rec.Eof
        '        '    If File.Exists(rec("NomeFile").Value) = False Then
        '        '        qEliminati += 1
        '        '        ReDim Preserve Eliminati(qEliminati)
        '        '        Eliminati(qEliminati) = rec("Url").Value
        '        '    End If

        '        '    rec.MoveNext()
        '        'Loop
        '        'rec.Close()

        '        If qEliminati > 0 Then
        '            For i As Integer = 1 To qEliminati
        '                sql = "Delete * From Indirizzi Where Url='" & Eliminati(i).Replace("'", "''") & "'"
        '                varConnessione.EsegueSql(conn, sql)

        '                Compatta = True
        '            Next
        '        End If

        '        ' Elimina i files temporanei dentro la cartella di appoggio
        '        gf.LeggeFilesDaDirectory("Buttami")
        '        Dim Filetti() As String = gf.RitornaFilesRilevati
        '        Dim quantiFiletti As Integer = 0

        '        Try
        '            quantiFiletti = Filetti.Length - 1
        '        Catch ex As Exception

        '        End Try

        '        For i As Integer = 1 To quantiFiletti
        '            Try
        '                Kill(Filetti(i))
        '            Catch ex As Exception

        '            End Try
        '        Next
        '    End If

        '    SecondiPassati = 0
        'End If

        ''gf = Nothing

        'NotifyIcon1.Icon = New Icon("Icone\DRAG1PG.ICO")

        'StaElaborando = False
        'End If
        'conn.Close()
        'g = Nothing

        'If Compatta = True Then
        'Dim jro As JRO.JetEngine

        'jro = New JRO.JetEngine()

        'jro.CompactDatabase("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=picDrop.mdb", _
        '"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=picDropComp.mdb;Jet OLEDB:Engine Type=5")

        'jro = Nothing

        'Kill("picDrop.mdb")
        'Rename("picDropComp.mdb", "picDrop.mdb")
        'End If
        'End If
        'tmrControllo.Enabled = True
        'End If
    End Sub

    Private Sub frmMain_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        lblDimensioni.Top = Me.Height - 20 - lblDimensioni.Height
        pbDownload.Left = 2
        pbDownload.Top = Me.Height - pbDownload.Height - 18
        pbDownload.Width = Me.Width - 21
        'cmbDir.Width = pbDownload.Width
        'cmbDir.Left = pbDownload.Left
    End Sub

    Private Sub TastoClickato(sender As Object, e As EventArgs)
        'Dim b As Button = DirectCast(sender, Button)

        'For i As Integer = 0 To Cartelle.Length - 1
        '    If BottoniDirs(i).Text = b.Text Then
        '        BottoniDirs(i).BackColor = Color.LightGreen

        '        lblDirSalvataggio.Text = b.Text

        '        NomeDirBase = sender.Text
        '        DirBase = Cartelle(i)

        '        SaveSetting("picDrop", "Settaggi", "NomeDirBase", NomeDirBase)
        '        SaveSetting("picDrop", "Settaggi", "DirBase", DirBase)
        '    Else
        '        BottoniDirs(i).BackColor = Color.LightGray
        '    End If
        'Next
    End Sub

    Private MinutiPerBackup As Integer = 60
    Private MinutiAttualiPerBackup As Integer = 0

    Private Sub tmrBackup_Tick(sender As Object, e As EventArgs) Handles tmrBackup.Tick
        MinutiAttualiPerBackup += 1
        If MinutiAttualiPerBackup >= MinutiPerBackup Then
            tmrBackup.Enabled = False
            MinutiAttualiPerBackup = 0

            ChiudeConnessione()

            Dim PathDB As String = Application.StartupPath & "\DB\picDrop.sdf"

            Dim Giorno As String = Format(Now.Date, "dddd").Substring(0, 3).ToUpper

            varConnessione = New SQLSERVERCE
            varConnessione.ImpostaNomeDB(Application.StartupPath & "\DB\picDrop.sdf")
            varConnessione.LeggeImpostazioniDiBase()
            conn = varConnessione.ApreDB()

            Dim Ok As Boolean = False

            If Not conn Is Nothing Then
                Ok = True
            End If

            conn.Close()
            varConnessione = Nothing

            If Ok Then
                Dim dataFile1 As DateTime = Now.Date
                Dim dataFile2 As DateTime = Now.Date

                If File.Exists(Application.StartupPath & "\DB\picDrop.sdf") Then
                    dataFile1 = FileDateTime(Application.StartupPath & "\DB\picDrop.sdf")
                End If
                If File.Exists(Application.StartupPath & "\DB\picDrop_Backup_" & Giorno & ".sdf") Then
                    dataFile2 = FileDateTime(Application.StartupPath & "\DB\picDrop_Backup_" & Giorno & ".sdf")
                End If

                If dataFile1 <> dataFile2 Then
                    Dim fileLen1 As Long = 0
                    Dim fileLen2 As Long = 0

                    If File.Exists(Application.StartupPath & "\DB\picDrop.sdf") Then
                        fileLen1 = FileLen(Application.StartupPath & "\DB\picDrop.sdf")
                    End If
                    If File.Exists(Application.StartupPath & "\DB\picDrop_Backup_" & Giorno & ".sdf") Then
                        fileLen2 = FileLen(Application.StartupPath & "\DB\picDrop_Backup_" & Giorno & ".sdf")
                    End If

                    If fileLen1 <> fileLen2 Then
                        Try
                            Kill(Application.StartupPath & "\DB\picDrop_Backup_" & Giorno & ".sdf")
                        Catch ex As Exception

                        End Try

                        File.Copy(Application.StartupPath & "\DB\picDrop.sdf", Application.StartupPath & "\DB\picDrop_Backup_" & Giorno & ".sdf")
                    End If
                End If
            End If

            ApreConnessione()
            tmrBackup.Enabled = True
        End If
    End Sub
End Class
