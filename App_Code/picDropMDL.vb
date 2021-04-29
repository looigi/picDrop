Imports System.IO
Imports System.Threading
Imports System.Net
Imports System.ComponentModel
Imports System.Net.Cache

Module picDropMDL
    Public TipoCollegamento As String
    Public Utenza As String
    Public Dominio As String
    Public Password As String
    Public Cartelle() As String
    Public Nomi() As String
    Public DirBase As String
    Public NomeDirBase As String
    Public numFiles As Long
    Public totKBytes As Long
    Public CreaLog As Boolean
    Public Sovrascrivi As Boolean
    Public ScartaPiccole As Boolean
    Public ApreFinestra As Boolean
    Public PathSito As Boolean
    Public NomeTag() As String
    Public idTag() As Integer
    Public QuantiTag As Integer
    Public StaScaricando As Boolean
    Public BottoniDirs() As Button
    Public Rimanenti As Integer
    Public StavaScaricando As Boolean
    Public Visibile As Boolean
    Public NonFareLoad As Boolean = False
    Public BloccoDownloadForzato As Boolean = False
    Public SitiDaSaltare() As String
    Public ListaDownloadVisibile As Boolean
    Public MascheraRicerca As Boolean
    Public BloccaDownloadPagina As Boolean = False
    Public StaScaricandoPagina As Boolean = False
    Public ScaricaAncheSeGiaScaricata As Boolean = False
    Public CaratteriStop1() As String = {Chr(34), "'", ">", "=", ";", ")", "(", "<", "{", "}"}
    Public CaratteriStop2() As String = {Chr(34), "'", ">", ";", ")", "(", "<", "{", "}"}
    Public FilesImmagini() As String = {"CID:", ".JPG", ".JPEG", ".PNG", ".BMP", ".GIF", ".PCX", ".IMG?H"}
    Public Filesvideo() As String = {".ZIP", ".RAR", ".MP4", ".WMV", ".3GP", ".MKV", ".AVI", ".SWV", ".MP3", ".MPG", ".MPEG"}

    Public Sub LeggeSettaggi()
        LeggeCartelle()

        TipoCollegamento = GetSetting("picDrop", "Settaggi", "TipoCollegamento")
        Utenza = GetSetting("picDrop", "Settaggi", "Utenza")
        Password = GetSetting("picDrop", "Settaggi", "Password")
        Dominio = GetSetting("picDrop", "Settaggi", "Dominio")
        DirBase = GetSetting("picDrop", "Settaggi", "DirBase")

        NomeDirBase = GetSetting("picDrop", "Settaggi", "NomeDirBase")

        If GetSetting("picDrop", "Settaggi", "Log") = "False" Then
            CreaLog = False
        Else
            CreaLog = True
        End If

        If GetSetting("picDrop", "Settaggi", "ScartaPiccole") = "False" Then
            ScartaPiccole = False
        Else
            ScartaPiccole = True
        End If

        If GetSetting("picDrop", "Settaggi", "Sovrascrivi") = "True" Then
            Sovrascrivi = True
        Else
            Sovrascrivi = False
        End If

        If GetSetting("picDrop", "Settaggi", "ApreFinestra") = "True" Then
            ApreFinestra = True
        Else
            ApreFinestra = False
        End If

        If GetSetting("picDrop", "Settaggi", "PathSito") = "True" Then
            PathSito = True
        Else
            PathSito = False
        End If

        If GetSetting("picDrop", "Settaggi", "GiaScaricata") = "True" Then
            ScaricaAncheSeGiaScaricata = True
        Else
            ScaricaAncheSeGiaScaricata = False
        End If

        'If DirBase = "" Then
        Dim Errore As Boolean = False

            Try
                DirBase = Cartelle(0)
                NomeDirBase = Nomi(0)

                SaveSetting("picDrop", "Settaggi", "NomeDirBase", NomeDirBase)
                SaveSetting("picDrop", "Settaggi", "DirBase", DirBase)
            Catch ex As Exception
                Errore = True
            End Try
        'End If

        LeggeTags()

        LeggeSitiDaSaltare()
    End Sub

    Public Function ControllaSeSitoDaSaltare(Url As String) As Boolean
        Dim Ok As Boolean = True

        If Not SitiDaSaltare Is Nothing Then
            For i As Integer = 0 To SitiDaSaltare.Length - 1
                If SitiDaSaltare(i).Trim <> "" Then
                    If Url.ToUpper.Contains(SitiDaSaltare(i).ToUpper.Trim) Then
                        Ok = False
                        Exit For
                    End If
                End If
            Next
        End If
        Return Ok
    End Function

    Private Sub LeggeSitiDaSaltare()
        Dim gf As New GestioneFilesDirectory
        Dim Siti As String = gf.LeggeFileIntero("SitiDaSaltare.txt")
        gf = Nothing
        If Siti <> "" Then
            SitiDaSaltare = Siti.Split(";")
        End If
    End Sub

    Private Sub LeggeTags()
        Dim gf As New GestioneFilesDirectory
        Dim tagghetti As String = gf.LeggeFileIntero("tags.csv")
        gf = Nothing

        Dim righelle() As String = tagghetti.Split("*")
        QuantiTag = 0
        Dim campetti() As String
        For i As Integer = 0 To righelle.Length - 1
            If righelle(i).Trim <> "" Then
                campetti = righelle(i).Split(";")
                ReDim Preserve NomeTag(i)
                ReDim Preserve idTag(i)

                NomeTag(i) = campetti(1).Trim.ToUpper
                idTag(i) = campetti(0)

                QuantiTag += 1
            End If
        Next
    End Sub

    Public Sub LeggeCartelle()
        ReDim Preserve Cartelle(0)
        ReDim Preserve Nomi(0)

        If File.Exists("Dirs.txt") = True Then
            Dim objReader As StreamReader = New StreamReader("Dirs.txt")
            Dim sLine As String = ""
            Dim Ritorno As String = ""

            Do
                sLine = objReader.ReadLine()
                Ritorno += sLine
            Loop Until sLine Is Nothing
            objReader.Close()

            If Ritorno <> "" Then
                Ritorno = Mid(Ritorno, 1, Ritorno.Length - 1)

                Dim sCartelle() As String = Ritorno.Split("§")
                Dim Campi() As String

                For i As Integer = 0 To sCartelle.Length - 1
                    Campi = sCartelle(i).Split(">")

                    ReDim Preserve Cartelle(i)
                    ReDim Preserve Nomi(i)

                    Nomi(i) = Campi(0)
                    Cartelle(i) = Campi(1)

                    If Not Directory.Exists(Cartelle(i)) Then
                        Dim c As String = Application.StartupPath & "\Download"
                        'MsgBox("Cartella " & vbCrLf & Cartelle(i) & vbCrLf & "per il tipo '" & Nomi(i) & "' non esistente." & vbCrLf & vbCrLf & "Applicata cartella di sistema:" & vbCrLf & c, vbExclamation)
                        Try
                            MkDir(c & "\")
                        Catch ex As Exception

                        End Try
                        Cartelle(i) = c
                    End If
                Next
            End If
        End If
    End Sub

    Public Function SistemaNome(Nome As String, Sito As String) As String
        Dim Ritorno As String
        Dim sNome As String = Nome
        Dim sSito As String = Sito
        Dim a As Integer
        Dim Quanti As Integer = 0
        Dim tot As Integer = 0

        ' Conta i '../'
        a = sNome.IndexOf("../")
        Do While a > -1
            Quanti += 1

            sNome = Mid(sNome, a + 3, sNome.Length)

            a = sNome.IndexOf("../")
        Loop
        If Mid(sNome, 1, 1) = "/" Then
            sNome = Mid(sNome, 2, sNome.Length)
        End If

        For i As Integer = sSito.Length To 1 Step -1
            If Mid(sSito, i, 1) = "/" Then
                tot += 1
                If tot = Quanti Then
                    sSito = Mid(sSito, 1, i)
                    Exit For
                End If
            End If
        Next

        Ritorno = sSito & sNome

        Return Ritorno
    End Function

    Public Function ControllaSeCiSonoImmagini(Cosa As String) As Long
        Dim Ritorno As Long = -1

        For i As Integer = 0 To FilesImmagini.Length - 1
            If Cosa.ToUpper.IndexOf(FilesImmagini(i)) > -1 Then
                Ritorno = Cosa.ToUpper.IndexOf(FilesImmagini(i))
                Exit For
            End If
        Next

        Return Ritorno
    End Function

    Public Function ControllaSeCiSonoFilesVari(Cosa As String) As Long
        Dim Ritorno As Long = -1

        For i As Integer = 0 To Filesvideo.Length - 1
            If Cosa.ToUpper.IndexOf(Filesvideo(i)) > -1 Then
                Ritorno = Cosa.ToUpper.IndexOf(Filesvideo(i))
                Exit For
            End If
        Next

        Return Ritorno
    End Function

    Public Function webDownloadImage(ByVal Url As String, ByVal saveFile As Boolean) As Image
        Dim webClient As New System.Net.WebClient
        Try
            Dim bytes() As Byte = webClient.DownloadData(Url.Replace("\", "/"))
            Dim stream As New IO.MemoryStream(bytes)
            Dim location As String = Application.StartupPath & "\Links\Buttami.jpg"

            If saveFile Then My.Computer.FileSystem.WriteAllBytes(location, bytes, False)

            Return New System.Drawing.Bitmap(stream)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub Completed(sender As Object, e As AsyncCompletedEventArgs)
        'file downloaded
        StaScaricando = False
    End Sub

    Private Sub Scaricando(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)
        Try
            Dim bytesIn As Double = Double.Parse(e.BytesReceived.ToString())
            Dim totalBytes As Double = Double.Parse(e.TotalBytesToReceive.ToString())
            Dim percentage As Double = bytesIn / totalBytes * 100

            frmMain.pbDownload.Value = Int32.Parse(Math.Truncate(percentage).ToString())
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ScaricaDaMSN(sNomeFile As String)
        Try
            MkDir("Links\DaMSN")
        Catch ex As Exception

        End Try

        Dim varConnessione As SQLSERVERCE = New SQLSERVERCE
        varConnessione.ImpostaNomeDB(Application.StartupPath & "\DB\picDrop.sdf")
        varConnessione.LeggeImpostazioniDiBase()
        Dim conn As Object = varConnessione.ApreDB()

        Dim gf As New GestioneFilesDirectory
        Dim sourcecode As String = gf.LeggeFileIntero(sNomeFile)
        Dim estensione As String = gf.TornaEstensioneFileDaPath(sNomeFile)
        gf = Nothing

        Dim a As Long
        Dim Appoggio As String
        Dim Cambia As String
        Dim Inizio As Long
        Dim Fine As Long

        a = sourcecode.ToUpper.IndexOf(".IMG")
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
                    Fine = i '+ 1
                    Exit For
                End If
            Next
            Appoggio = Mid(sourcecode, Inizio, Fine - Inizio)

            Cambia = Appoggio

            If Appoggio.Contains("?") Then
                Appoggio = Mid(Appoggio, 1, Appoggio.IndexOf("?"))
            End If

            Dim d As String = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & ".jpg"
            Dim sNomeFileDaSalvare As String = "Links\DaMSN\" & d

            Try
                ScaricaFileGlobale("http:" & Appoggio, sNomeFileDaSalvare, varConnessione, conn)
            Catch ex As Exception

            End Try

            If File.Exists(sNomeFileDaSalvare) Then
                Dim Ritorno As Integer = SistemaFileScaricato(sNomeFileDaSalvare, Nothing, varConnessione, conn, "http:" & Appoggio, "DaMSN", {"DaPHP", "MSN.COM"}, Nothing)
            End If

            sourcecode = sourcecode.Replace(Cambia, "")

            a = sourcecode.ToUpper.IndexOf(".IMG")

            If BloccaDownloadPagina Then
                Exit Do
            End If
        Loop

        conn.Close()
        varConnessione = Nothing
    End Sub

    Public Sub ScaricaFileGlobale(Url As String, sNomeFiletto As String, varConnessione As SQLSERVERCE, conn As Object)
        Dim giaScaricata As Boolean = False
        Dim p As New picDropCls

        If Not ScaricaAncheSeGiaScaricata Then
            giaScaricata = p.ControllaSeGiaScaricata(varConnessione, conn, Url)
        End If

        If Not giaScaricata Then
            StaScaricando = True

            Dim gf As New GestioneFilesDirectory
            Dim sNomeFile As String = sNomeFiletto
            Dim estensione As String = gf.TornaEstensioneFileDaPath(sNomeFile).ToUpper.Trim
            Dim estOk As Boolean = False
            Select Case estensione
                Case ".JPG", ".JPEG", ".PNG", ".BMP", ".GIF", ".IMG"
                    estOk = True
            End Select

            Try
                'If Url.ToUpper.Contains("WWW.MSN.COM") And Not estOk Then
                If Not estOk And Not Url.ToUpper.Contains("HTTPS://WWW.BING.COM") Then
                    Dim iMessage As CDO.Message = New CDO.Message
                    iMessage.CreateMHTMLBody(Url,
                CDO.CdoMHTMLFlags.cdoSuppressNone, "", "")
                    Dim adodbstream As ADODB.Stream = New ADODB.Stream
                    adodbstream.Type = ADODB.StreamTypeEnum.adTypeText
                    adodbstream.Charset = "US-ASCII"
                    adodbstream.Open()
                    iMessage.DataSource.SaveToObject(adodbstream, "_Stream")
                    adodbstream.SaveToFile(sNomeFile, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
                Else
                    Dim wb As New WebClient
                    Dim Uri As New Uri(Url.Replace(" ", "%20"))
                    AddHandler wb.DownloadFileCompleted, AddressOf Completed
                    AddHandler wb.DownloadProgressChanged, AddressOf Scaricando
                    Application.DoEvents()

                    frmMain.pbDownload.Value = 0
                    frmMain.pbDownload.Visible = True
                    wb.CachePolicy = New RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
                    wb.DownloadFileAsync(Uri, sNomeFile)
                    Application.DoEvents()

                    Do While StaScaricando = True
                        Application.DoEvents()
                    Loop
                    frmMain.pbDownload.Visible = False
                End If
            Catch ex As Exception

            End Try

            ' If Url.ToUpper.Contains("MSN.COM/C.GIF?") Then
            'Dim sUrl As String = Url
            'sUrl = Mid(sUrl, sUrl.IndexOf("=https") + 2, sUrl.Length)
            'Uri = New Uri(sUrl.Replace(" ", "%20"))
            'sNomeFile = "Links\Msn.htm"

            'End If

            'If Url.ToUpper.Contains("MSN.COM/C.GIF?") Then
            '    ScaricaDaMSN(sNomeFile)
            '    Exit Sub
            'End If

            If Url.ToUpper.Contains(".PHP") Then
                GestionePHP(Url, sNomeFile, sNomeFiletto)
            End If

            If File.Exists(sNomeFile) Then
                Dim a As Integer = FileLen(sNomeFile)
                If a = 0 Then
                    Try
                        Kill(sNomeFile)
                    Catch ex As Exception

                    End Try
                End If
            End If
        End If
    End Sub

    Public Sub GestisceImmagineInline(sourcecode As String, Appoggio As String)
        Dim sNomeFileInline As String = Mid(Appoggio, 5, Appoggio.Length)
        Dim a As Long
        Dim Inizio As Long
        Dim Fine As Long
        Dim gf As New GestioneFilesDirectory

        Appoggio = "Content-ID: <" & Mid(Appoggio, 5, Appoggio.Length) & ">"
        a = sourcecode.IndexOf(Appoggio)
        If a > -1 Then
            Appoggio = Mid(sourcecode, a, sourcecode.Length)
            'For k As Long = a To Appoggio.Length
            '    If Mid(Appoggio, k, 2) = Chr(10) Then
            '        Inizio = k
            '        Exit For
            '    End If
            'Next
            'If Inizio = 0 Then Inizio = 1

            'For k As Long = Inizio To Appoggio.Length
            '    If Mid(Appoggio, k, 3) = Chr(10) Then
            '        Fine = k + 3
            '        Exit For
            '    End If
            'Next

            'Appoggio = Mid(Appoggio, Inizio, Fine - Inizio)

            Appoggio = Mid(Appoggio, 1, Appoggio.IndexOf("------=_NextPart"))

            Dim Righe() As String = Appoggio.Split(Chr(10))

            Appoggio = ""
            If Righe.Length > 4 Then
                Try
                    Dim B As Byte()
                    Dim c As Integer = 0
                    For Each S As String In Righe
                        c += 1
                        If c > 4 Then
                            If S.Replace(Chr(13), "").Replace(Chr(10), "").Replace(" ", "") <> "" Then
                                B = Convert.FromBase64String(S)

                                Dim fs As New FileStream("Links\Scaricate\" & sNomeFileInline & ".jpg", FileMode.Append)
                                fs.Write(B, 0, B.Length)
                                fs.Close()
                            End If
                        End If
                    Next
                Catch ex As Exception
                    Appoggio = ""
                End Try
            End If
        End If

        gf = Nothing
    End Sub

    Private Sub GestionePHP(Url As String, sNomeFile As String, sNomeFiletto As String)
        Dim gf As New GestioneFilesDirectory
        Dim sourcecode As String = gf.LeggeFileIntero(sNomeFile)
        Dim estensione As String = gf.TornaEstensioneFileDaPath(sNomeFile)

        Dim varConnessione As SQLSERVERCE = New SQLSERVERCE
        varConnessione.ImpostaNomeDB(Application.StartupPath & "\DB\picDrop.sdf")
        varConnessione.LeggeImpostazioniDiBase()
        Dim conn As Object = varConnessione.ApreDB()

        Try
            MkDir("Links\DaPhp")
        Catch ex As Exception

        End Try

        Dim Appoggio As String
        Dim Cambia As String
        Dim a As Long
        Dim Inizio As Long
        Dim Fine As Long
        Dim c As Integer = 0
        Dim Sito As String = Mid(Url, 1, Url.ToUpper.IndexOf(".PHP"))
        For i As Integer = Sito.Length To 1 Step -1
            If Mid(Sito, i, 1) = "/" Then
                Sito = Mid(Sito, 1, i)
                Exit For
            End If
        Next
        sNomeFile = sNomeFile.Replace(estensione, "")
        sNomeFile = gf.TornaNomeFileDaPath(sNomeFile)
        sNomeFile = "Links\DaPhp\" & sNomeFile
        gf = Nothing

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
                    Fine = i '+ 1
                    Exit For
                End If
            Next
            Appoggio = Mid(sourcecode, Inizio, Fine - Inizio)

            Appoggio = Sito & Appoggio

            Cambia = Appoggio
            If Appoggio.ToUpper.IndexOf(".HTM") = -1 Then
                If Mid(Appoggio, 2, 1) = "/" Then
                    Appoggio = Mid(Appoggio, 1, 1) & Sito & Mid(Appoggio, 2, Appoggio.Length)
                Else
                    If Mid(Appoggio, 2, 2) = ".." Then
                        Appoggio = SistemaNome(Appoggio, Url)
                    End If
                End If
                If Appoggio.ToUpper.IndexOf("HTTP") > -1 Then
                    c += 1
                    ScaricaFileGlobale(Appoggio, sNomeFile & "_" & c.ToString & estensione, varConnessione, conn)
                End If
            End If

            If File.Exists(sNomeFile & "_" & c.ToString & estensione) Then
                Dim Ritorno As Integer = SistemaFileScaricato(sNomeFile & "_" & c.ToString & estensione, Nothing, varConnessione, conn, Url, "DaPHP", {"DaPHP", Sito}, Nothing)
            End If

            sourcecode = sourcecode.Replace(Cambia, "")

            a = ControllaSeCiSonoImmagini(sourcecode)

            If BloccaDownloadPagina Then
                Exit Do
            End If
        Loop

        File.Delete(sNomeFiletto)

        conn.Close()
        varConnessione = Nothing
    End Sub

    Private Function EsegueProcessoDOS(FileName As String, Arguments As String, WaitForExit As Boolean) As Integer
        Using myProc As New Process()
            'Create a new object called myProc as Process.
            myProc.StartInfo.FileName = FileName
            'Give the process file name reveived by the method.
            myProc.StartInfo.Arguments = Arguments
            'Give the arguments received.
            myProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            'As hidden window style.
            myProc.StartInfo.CreateNoWindow = True
            'Create no window property as true.
            myProc.StartInfo.UseShellExecute = False
            'Disable the shellexecute since we do not want to see any window.
            myProc.Start()
            'Start the process.
            If WaitForExit Then
                myProc.WaitForExit()
            End If
            'Wait for the process if the 'WaitForExit' was sent as true.
            'Return the exit code of the process to the method.
            Return myProc.ExitCode
        End Using
    End Function

    Public Function SistemaFileScaricato(sNomeFile As String, lblDimensioni As Label, varConnessione As SQLSERVERCE, conn As Object, Url As String, NomeSito As String, Resto() As String, frmOrigine As Form) As Integer
        Dim gf As New GestioneFilesDirectory
        Dim p As New picDropCls
        Dim Ritorno As Integer = 0

        If File.Exists(sNomeFile) = True Then
            If sNomeFile.ToUpper.IndexOf(".BMP") > -1 Or sNomeFile.ToUpper.IndexOf(".JPG") > -1 Or sNomeFile.ToUpper.IndexOf(".JPEG") > -1 Or sNomeFile.ToUpper.IndexOf(".JPG") > -1 Or sNomeFile.ToUpper.IndexOf(".GIF") > -1 Or sNomeFile.ToUpper.IndexOf(".PNG") > -1 Then
                Dim ima As Image = Nothing
                Application.DoEvents()
                ima = CaricaImmagine(sNomeFile)
                Application.DoEvents()
                If ima Is Nothing = True Then
                    If sNomeFile.ToUpper.IndexOf(".GIF") > -1 Then
                    Else
                        Dim Contenuto As String = gf.LeggeFileIntero(sNomeFile)
                        If Mid(Contenuto, 1, 4) = "RIFF" Then
                            Dim sNomeFile2 As String = sNomeFile.Replace(gf.TornaEstensioneFileDaPath(sNomeFile), "") & ".webp"
                            Rename(sNomeFile, sNomeFile2)
                            Application.DoEvents()

                            EsegueProcessoDOS(Application.StartupPath & "\dwebp.exe", sNomeFile2 & " -o " & sNomeFile2.Replace(gf.TornaEstensioneFileDaPath(sNomeFile2), "") & ".bmp", True)
                            Application.DoEvents()

                            gf.EliminaFileFisico(sNomeFile2)
                            Application.DoEvents()

                            sNomeFile = sNomeFile2.Replace(gf.TornaEstensioneFileDaPath(sNomeFile2), "") & ".bmp"

                            Dim img As Image = CaricaImmagine(sNomeFile)
                            img.Save(sNomeFile.Replace(".bmp", "") & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
                            img.Dispose()
                            Application.DoEvents()

                            gf.EliminaFileFisico(sNomeFile)
                            Application.DoEvents()

                            sNomeFile = sNomeFile.Replace(".bmp", "") & ".jpg"

                            Application.DoEvents()
                            ima = CaricaImmagine(sNomeFile)
                            Application.DoEvents()

                            If ima Is Nothing Then
                                Try
                                    File.Delete(sNomeFile)
                                Catch ex As Exception

                                End Try
                            End If
                        End If
                    End If
                End If
                ima = Nothing

                Dim bt As System.Drawing.Bitmap
                bt = CaricaImmagine(sNomeFile)
                Application.DoEvents()

                Dim w As Integer = bt.Width
                Dim h As Integer = bt.Height

                bt.Dispose()
                bt = Nothing

                If w > 200 And h > 200 And ScartaPiccole = True Then
                    Dim Dime As Integer = p.ScriveDimensioniImmagine(sNomeFile, lblDimensioni)
                    If Not lblDimensioni Is Nothing Then
                        lblDimensioni.Visible = True
                    End If

                    Dim sDatella As String = Now.Year & "-" & Format(Now.Month, "00") & "-" & Format(Now.Day, "00") & " " & Format(Now.Hour, "00") & ":" & Format(Now.Minute, "00") & ":" & Format(Now.Second, "00")

                    Application.DoEvents()
                    p.ScriveIndirizzoSuDB(varConnessione, conn, Url, sDatella, Dime, sNomeFile)
                    Application.DoEvents()

                    Ritorno = 1

                    ' Scrive tag exif
                    ScriveTag(sNomeFile, NomeSito, Resto)
                    ' Scrive tag exif

                    Application.DoEvents()

                    If Not frmOrigine Is Nothing Then
                        frmOrigine.BackgroundImageLayout = ImageLayout.Stretch
                        frmOrigine.BackgroundImage = CaricaImmagine(sNomeFile)
                        Application.DoEvents()
                    End If
                Else
                    Try
                        Kill(sNomeFile)
                    Catch ex As Exception

                    End Try
                End If
            End If
        Else
            If Not frmOrigine Is Nothing Then
                frmOrigine.BackgroundImage = Image.FromFile("Icone\errore.png")
                lblDimensioni.Visible = False
                Application.DoEvents()
            End If
            Ritorno = 0
        End If

        Return Ritorno
    End Function

    Public Sub ScaricaFileDallaRete(Url As String, sNomeFile As String, nomeForm As Form, lblDimensioni As Label, varConnessione As SQLSERVERCE, conn As Object)
        Dim gf As New GestioneFilesDirectory
        Dim p As New picDropCls
        Dim giaScaricata As Boolean = False

        If Not ScaricaAncheSeGiaScaricata Then
            giaScaricata = p.ControllaSeGiaScaricata(varConnessione, conn, Url)
        End If

        If Not giaScaricata Then
            Try
                If TipoCollegamento = "Proxy" Then
                    Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create(Url)
                    request.Proxy.Credentials = New System.Net.NetworkCredential(Utenza, Password, Dominio)
                    Dim response As System.Net.HttpWebResponse = request.GetResponse()
                    Application.DoEvents()

                    Dim responseStream As Stream = response.GetResponseStream()
                    Dim imageBytes() As Byte

                    Using br As New BinaryReader(responseStream)
                        imageBytes = br.ReadBytes(500000)
                        br.Close()
                    End Using
                    responseStream.Close()
                    response.Close()

                    Dim fs As New FileStream(sNomeFile, FileMode.Create)
                    Dim bw As New BinaryWriter(fs)
                    Try
                        bw.Write(imageBytes)
                    Finally
                        fs.Close()
                        bw.Close()
                    End Try

                    request = Nothing

                    response.Close()
                    response = Nothing
                    request = Nothing
                Else
                    If Sovrascrivi Then
                        Try
                            File.Delete(sNomeFile)
                        Catch ex As Exception

                        End Try

                        ScaricaFileGlobale(Url, "Links\Appoggio.jpg", varConnessione, conn)
                        If File.Exists("Links\Appoggio.jpg") Then
                            If File.Exists(sNomeFile) Then
                                Dim l1 As Integer = FileLen("Links\Appoggio.jpg")
                                Dim l2 As Integer = FileLen(sNomeFile)
                                If l1 = l2 Then
                                    Kill("Link\Appoggio.jpg")
                                Else
                                    Dim sNomeFile2 As String = gf.NomeFileEsistente(sNomeFile)
                                    Rename("Links\Appoggio.jpg", sNomeFile2)
                                End If
                            Else
                                Rename("Links\Appoggio.jpg", sNomeFile)
                            End If
                        End If
                    Else
                        ScaricaFileGlobale(Url, sNomeFile, varConnessione, conn)
                    End If
                End If
            Catch ex As Exception
                nomeForm.BackgroundImage = Image.FromFile("Icone\errore.png")
                lblDimensioni.Visible = False
                Application.DoEvents()
            End Try
        End If

        p = Nothing
        gf = Nothing
    End Sub

    Public Function CaricaImmagine(NomeImmagine As String) As Image
        Dim bmp As Image = Nothing
        Dim fs As System.IO.FileStream = Nothing

        Try
            fs = New System.IO.FileStream(NomeImmagine, IO.FileMode.Open, IO.FileAccess.Read)
            bmp = System.Drawing.Image.FromStream(fs)
        Catch ex As Exception
            'Stop
        End Try

        If fs Is Nothing = False Then
            fs.Close()
        End If
        fs.Dispose()
        fs = Nothing

        Return bmp
    End Function

    Private Function RitornaDatiExif(Immagine As String) As String()
        Dim imm As Bitmap = CaricaImmagine(Immagine)
        Dim Campi() As String = {}

        Try
            Dim er As Goheer.EXIF.EXIFextractor = New Goheer.EXIF.EXIFextractor(imm, "§")
            Campi = er.ToString.Split("§")
            er = Nothing
        Catch ex As Exception

        End Try

        imm.Dispose()
        imm = Nothing

        Return Campi
    End Function

    Private Function PrendeIdDaTag(Tagghetto As String) As Integer
        Dim id As Integer = -1

        For i As Integer = 0 To QuantiTag - 1
            If Tagghetto.Replace(" ", "") = NomeTag(i) Then
                id = idTag(i)
                Exit For
            End If
        Next

        Return id
    End Function

    Public Sub ScriveTag(sNomeFile As String, NomeSito As String, Resto() As String)
        Dim DatiExif() As String = RitornaDatiExif(sNomeFile)

        Dim bmp As Bitmap = Image.FromFile(sNomeFile)

        Dim er As Goheer.EXIF.EXIFextractor = New Goheer.EXIF.EXIFextractor(bmp, "\n")
        Dim Datella As String = Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "00") & " " & Format(Now.Hour, "00") & ":" & Format(Now.Minute, "00") & ":" & Format(Now.Second, "00")

        Dim testo As String = NomeSito & ";"
        For i As Integer = 0 To Resto.Length - 2
            testo += Resto(i) & ";"
        Next

        Dim nomeimm As String = Resto(Resto.Length - 1)
        For i As Integer = nomeimm.Length To 1 Step -1
            If Mid(nomeimm, i, 1) = "." Then
                nomeimm = Mid(nomeimm, 1, i - 1)
                Exit For
            End If
        Next

        ' imposta codici originali
        Dim testina As String
        Dim testone As String
        Dim id As Integer
        Dim ceCommento As Boolean = False

        For i As Integer = 0 To DatiExif.Length - 1
            If DatiExif(i) <> "" Then
                testina = Mid(DatiExif(i), 1, DatiExif(i).IndexOf(":")).Trim.ToUpper
                testone = Mid(DatiExif(i), DatiExif(i).IndexOf(":") + 2, DatiExif(i).Length).Trim
                id = PrendeIdDaTag(testina)
                If id <> -1 Then
                    If id = 270 Then
                        testone = testo & "§;" & testone & ";"
                        ceCommento = True
                    End If

                    er.setTag(id, testone & Chr(0))
                End If
            End If
        Next
        ' imposta codici originali

        If ceCommento = False Then
            er.setTag(270, testo & Chr(0))
        End If
        er.setTag(305, "picDROP" & Chr(0))
        er.setTag(306, Datella & Chr(0))

        Try
            bmp.Save(sNomeFile & ".bbb")
        Catch ex As Exception
            'Stop
        End Try

        er = Nothing
        bmp.Dispose()
        bmp = Nothing

        File.Delete(sNomeFile)
        Dim c As Integer = 0
        Do While File.Exists(sNomeFile & ".bbb")
            Rename(sNomeFile & ".bbb", sNomeFile)
            Thread.Sleep(1000)
            c += 1
            If c = 5 Then
                Exit Do
            End If
        Loop
    End Sub

End Module
