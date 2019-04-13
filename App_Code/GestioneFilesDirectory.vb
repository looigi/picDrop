Imports System.IO
Imports System.Text

Public Structure ModalitaDiScan
    Dim TipologiaScan As Integer
    Const SoloStruttura = 0
    Const Elimina = 1
End Structure

Public Class GestioneFilesDirectory
    Private DirectoryRilevate() As String
    Private FilesRilevati() As String
    Private QuantiFilesRilevati As Long
    Private QuanteDirRilevate As Long
    Private RootDir As String
    Private Eliminati As Boolean
    Private Percorso As String
    Private Barra As String = "\"

    Public Const NonEliminareRoot As Boolean = False
    Public Const EliminaRoot As Boolean = True
    Public Const NonEliminareFiles As Boolean = False
    Public Const EliminaFiles As Boolean = True

    Public Sub PrendeRoot(R As String)
        RootDir = R
    End Sub

    Public Function RitornaFilesRilevati() As String()
        Return FilesRilevati
    End Function

    Public Function RitornaDirectoryRilevate() As String()
        Return DirectoryRilevate
    End Function

    Public Function RitornaQuantiFilesRilevati() As Long
        Return QuantiFilesRilevati
    End Function

    Public Function RitornaQuanteDirectoryRilevate() As Long
        Return QuanteDirRilevate
    End Function

    Public Sub ImpostaPercorsoAttuale(sPercorso As String)
        Percorso = sPercorso
    End Sub

    Public Function NomeFileEsistente(NomeFile As String) As String
        Dim NomeFileDestinazione As String = NomeFile
        If File.Exists(NomeFileDestinazione) Then
            Dim gf As New GestioneFilesDirectory
            Dim Estensione As String = gf.TornaEstensioneFileDaPath(NomeFileDestinazione)
            NomeFileDestinazione = NomeFileDestinazione.Replace(Estensione, "")
            Dim Contatore As Integer = 0

            Do While File.Exists(NomeFileDestinazione & "_" & Format(Contatore, "0000") & Estensione) = True
                Contatore += 1
            Loop

            'If Contatore > 0 Then
            NomeFileDestinazione = NomeFileDestinazione & "_" & Format(Contatore, "0000") & Estensione
            'Else
            '    NomeFileDestinazione = NomeFile
            'End If
            gf = Nothing
        End If

        Return NomeFileDestinazione
    End Function

    Public Function EliminaFileFisico(NomeFileOrigine As String) As String
        Dim Ritorno As String = ""

        If NomeFileOrigine.Trim <> "" Then
            Try
                File.Delete(NomeFileOrigine)

                Do While (System.IO.File.Exists(NomeFileOrigine) = True)
                    Threading.Thread.Sleep(1)
                Loop
            Catch ex As Exception
                Ritorno = "ERRORE: " & ex.Message
            End Try
        End If

        Return Ritorno
    End Function

    Public Function CopiaOMuoveFileFisico(NomeFileOrigine As String, NomeFileDestinazione As String, Optional Sovrascrittura As Boolean = True, Optional Muovi As Boolean = False) As String
        Dim Ritorno As String = ""

        If NomeFileOrigine.Trim <> "" And NomeFileDestinazione.Trim <> "" And NomeFileOrigine.Trim.ToUpper <> NomeFileDestinazione.Trim.ToUpper Then
            If File.Exists(NomeFileDestinazione) = True Then
                If Sovrascrittura = False Then
                    NomeFileDestinazione = NomeFileEsistente(NomeFileDestinazione)
                End If
            End If

            Try
                File.Copy(NomeFileOrigine, NomeFileDestinazione, True)

                Do Until (System.IO.File.Exists(NomeFileDestinazione))
                    Threading.Thread.Sleep(1)
                Loop
            Catch ex As Exception
                Ritorno = "ERRORE: " & ex.Message
            End Try

            If Muovi = True Then
                EliminaFileFisico(NomeFileOrigine)
            End If
        End If

        Return Ritorno
    End Function

    Public Function TornaNomeFileDaPath(Percorso As String) As String
        Dim Ritorno As String = ""

        For i As Integer = Percorso.Length To 1 Step -1
            If Mid(Percorso, i, 1) = "/" Or Mid(Percorso, i, 1) = Barra Then
                Ritorno = Mid(Percorso, i + 1, Percorso.Length)
                Exit For
            End If
        Next

        Return Ritorno
    End Function

    Public Function TornaEstensioneFileDaPath(Percorso As String) As String
        Dim Ritorno As String = ""

        For i As Integer = Percorso.Length To 1 Step -1
            If Mid(Percorso, i, 1) = "." Then
                Ritorno = Mid(Percorso, i, Percorso.Length)
                Exit For
            End If
        Next

        Return Ritorno
    End Function

    Public Function TornaNomeDirectoryDaPath(Percorso As String) As String
        Dim Ritorno As String = ""

        For i As Integer = Percorso.Length To 1 Step -1
            If Mid(Percorso, i, 1) = "/" Or Mid(Percorso, i, 1) = Barra Then
                Ritorno = Mid(Percorso, 1, i - 1)
                Exit For
            End If
        Next

        Return Ritorno
    End Function

    Public Sub CreaAggiornaFile(NomeFile As String, Cosa As String)
        Try
            Dim path As String

            If Percorso <> "" Then
                path = Percorso & Barra & NomeFile
            Else
                path = NomeFile
            End If

            path = path.Replace(Barra & Barra, Barra)

            ' Create or overwrite the file.
            Dim fs As FileStream = File.Create(path)

            ' Add text to the file.
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(Cosa)
            fs.Write(info, 0, info.Length)
            fs.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private objReader As StreamReader

    Public Sub ApreFilePerLettura(NomeFile As String)
        objReader = New StreamReader(NomeFile)
    End Sub

    Public Function RitornaRiga() As String
        Return objReader.ReadLine()
    End Function

    Public Sub ChiudeFile()
        objReader.Close()
    End Sub

    Public Function LeggeFileIntero(NomeFile As String) As String
        'Dim objReader As StreamReader = New StreamReader(NomeFile)
        'Dim sLine As String = ""
        Dim Ritorno As String = ""

        'Do
        '    sLine = objReader.ReadLine()
        '    Ritorno += sLine
        'Loop Until sLine Is Nothing
        'objReader.Close()

        Try
            ' Open the file using a stream reader.
            Using sr As New StreamReader(NomeFile)
                ' Read the stream to a string and write the string to the console.
                Ritorno = sr.ReadToEnd()
            End Using
        Catch e As Exception
        End Try

        Return Ritorno
    End Function

    Public Sub ScansionaDirectorySingola(Percorso As String, Modalita As Integer)
        Dim di As New IO.DirectoryInfo(Percorso)
        Dim diar1 As IO.DirectoryInfo() = di.GetDirectories
        Dim dra As IO.DirectoryInfo

        Eliminati = False

        PulisceInfo()

        If Modalita <> ModalitaDiScan.SoloStruttura Then
            LeggeFilesDaDirectory(Percorso)
        End If

        For Each dra In diar1
            QuanteDirRilevate += 1
            ReDim Preserve DirectoryRilevate(QuanteDirRilevate)
            DirectoryRilevate(QuanteDirRilevate) = dra.FullName

            If Modalita <> ModalitaDiScan.SoloStruttura Then
                LeggeFilesDaDirectory(dra.FullName)
            End If
        Next

        Select Case Modalita
            Case ModalitaDiScan.Elimina
        End Select

        'Erase FilesRilevati
        'Erase DirectoryRilevate
    End Sub

    Public Sub PulisceInfo()
        Erase FilesRilevati
        QuantiFilesRilevati = 0
        Erase DirectoryRilevate
        QuanteDirRilevate = 0
    End Sub

    'Public Sub ScansionaPercorsoDirectory(Percorso As String, Modalita As Integer)
    '    Dim di As New IO.DirectoryInfo(Percorso)
    '    Dim diar1 As IO.DirectoryInfo() = di.GetDirectories
    '    Dim dra As IO.DirectoryInfo

    '    Eliminati = False

    '    For Each dra In diar1
    '        QuanteDirRilevate += 1
    '        ReDim Preserve DirectoryRilevate(QuanteDirRilevate)
    '        DirectoryRilevate(QuanteDirRilevate) = dra.FullName

    '        If Modalita <> ModalitaDiScan.SoloStruttura Then
    '            LeggeFilesDaDirectory(dra.FullName)
    '        End If

    '        ScansionaPercorsoDirectory(dra.FullName, Modalita)
    '    Next

    '    Select Case Modalita
    '        Case ModalitaDiScan.Elimina
    '    End Select

    '    'Erase FilesRilevati
    '    'Erase DirectoryRilevate
    'End Sub

    Public Function RitornaEliminati() As Boolean
        Return Eliminati
    End Function

    Public Sub LeggeFilesDaDirectory(Percorso As String)
        Dim di As New IO.DirectoryInfo(Percorso)
        'Dim diar1 As IO.DirectoryInfo() = di.GetDirectories
        'Dim dra As IO.DirectoryInfo

        'Erase FilesRilevati
        'QuantiFilesRilevati = 0

        ' Lettura directory
        'For Each dra In diar1
        '    QuanteDirRilevate += 1
        '    ReDim Preserve DirectoryRilevate(QuanteDirRilevate)
        '    DirectoryRilevate(QuanteDirRilevate) = dra.FullName

        '    LeggeDirectory(dra.FullName)
        'Next

        Dim fi As New IO.DirectoryInfo(Percorso)
        Dim fiar1 As IO.FileInfo() = di.GetFiles
        Dim fra As IO.FileInfo

        For Each fra In fiar1
            QuantiFilesRilevati += 1
            ReDim Preserve FilesRilevati(QuantiFilesRilevati)
            FilesRilevati(QuantiFilesRilevati) = fra.FullName
        Next
    End Sub

    Public Sub CreaDirectoryDaPercorso(Percorso As String)
        Dim Ritorno As String = Percorso

        For i As Integer = 1 To Ritorno.Length
            If Mid(Ritorno, i, 1) = Barra Then
                On Error Resume Next
                MkDir(Mid(Ritorno, 1, i))
                On Error GoTo 0
            End If
        Next
    End Sub

    Private Function Ordina(Filetti() As String) As String()
        Dim Appoggio() As String = Filetti
        Dim Appo As String

        For i As Integer = 1 To Appoggio.Count - 1
            For k As Integer = i + 1 To Appoggio.Count - 1
                If Appoggio(i).ToUpper.Trim > Appoggio(k).ToUpper.Trim Then
                    Appo = Appoggio(i)
                    Appoggio(i) = Appoggio(k)
                    Appoggio(k) = Appo
                End If
            Next
        Next

        Return Appoggio
    End Function

    Public Sub EliminaAlberoDirectory(Percorso As String, EliminaRoot As Boolean, EliminaFiles As Boolean)
        ScansionaDirectorySingola(Percorso, 0)

        DirectoryRilevate = Ordina(DirectoryRilevate)

        If EliminaFiles = True Then
            FilesRilevati = Ordina(FilesRilevati)

            For i As Integer = FilesRilevati.Length - 1 To 1 Step -1
                Try
                    EliminaFileFisico(FilesRilevati(i))
                Catch ex As Exception

                End Try
            Next
        End If

        For i As Integer = DirectoryRilevate.Length - 1 To 1 Step -1
            Try
                RmDir(RitornaDirectoryRilevate(i))
            Catch ex As Exception

            End Try
        Next

        If EliminaRoot = True Then
            Try
                RmDir(Percorso)
            Catch ex As Exception

            End Try
        End If
    End Sub

End Class
