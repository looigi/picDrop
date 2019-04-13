Imports System.IO

Public Class picDropCls
    Public Function ControllaSeEsisteFile(Nome As String) As String
        Dim Ritorno As String = Nome

        If File.Exists(Ritorno) = True Then
            Dim Est As String = ""
            Dim Conta As Integer = 0

            For i As Integer = Ritorno.Length - 1 To 1 Step -1
                If Mid(Ritorno, i, 1) = "." Then
                    Est = Mid(Ritorno, i + 1, Ritorno.Length)
                    Ritorno = Mid(Ritorno, 1, i - 1)
                    Exit For
                End If
            Next

            Do While File.Exists(Ritorno & "_" & Format(Conta, "000") & "." & Est) = True
                Conta += 1
            Loop

            Ritorno = Ritorno & "_" & Format(Conta, "000") & "." & Est
        End If

        Ritorno = TogliePerc(Ritorno)

        Return Ritorno
    End Function

    Public Function TogliePerc(Cosa As String) As String
        Dim Appo As String = Cosa
        Dim Numerello As String
        Dim C As Char
        Dim N As Integer
        Dim N2 As String
        Dim Dove As Long

        Dove = Appo.IndexOf("%")
        Do While Dove > -1
            Numerello = Mid(Appo, Dove + 1, 3)
            Try
                N2 = Mid(Numerello, 2, 2)
                N = Convert.ToInt32(N2, 16)
            Catch ex As Exception
                N = -1
            End Try
            If N <> -1 Then
                C = Chr(N)
            End If
            Appo = Appo.Replace(Numerello, C)

            Dove = Appo.IndexOf("%")
        Loop

        Appo = Appo.Replace("&quot;", Chr(34))
        Appo = Appo.Replace("$", "_")
        Appo = Appo.Replace("\/", "/")
        Appo = Appo.Replace("\\/", "/")

        Appo = Appo.Replace("&amp;", "&")
        Appo = Appo.Replace("&gt;", "<")
        Appo = Appo.Replace("&lt;", ">")
        Appo = Appo.Replace("&quot;", Chr(34))
        Appo = Appo.Replace("&tilde;", "˜")

        Return Appo
    End Function

    Public Function ControllaSeGiaScaricata(varConnessione As SQLSERVERCE, conn As Object, Url As String) As Boolean
        Dim Ritorno As Boolean = False

        Dim rec As Object = "ADODB.Recordset"
        Dim sql As String = "Select * From Indirizzi Where Url='" & Url.Replace("'", "''") & "'"
        rec = varConnessione.LeggeQuery(conn, sql)
        If rec.Eof = False Then
            Ritorno = True
        End If
        rec.Close()

        Return Ritorno
    End Function

    Public Sub ScriveIndirizzoSuDB(varConnessione As SQLSERVERCE, conn As Object, Url As String, sDatella As String, Dimensione As Long, NomeFile As String)
        Dim sUrl As String = Url
        Dim sNomeFile As String = NomeFile

        If sUrl.Length > 255 Then
            sUrl = Mid(sUrl, 1, 255)
        End If
        If sNomeFile.Length > 255 Then
            sNomeFile = Mid(sNomeFile, 1, 255)
        End If

        Dim sql As String = "Insert Into Indirizzi Values ('" & sUrl.Replace("'", "''") & "', '" & sNomeFile.Replace("'", "''") & "', " & Dimensione & ",'" & sDatella & "', '')"
        varConnessione.EsegueSql(conn, sql)

        numFiles += 1
        totKBytes += Dimensione

        sql = "Update Statistiche Set numFiles=" & numFiles & ", Dimensioni=" & totKBytes
        varConnessione.EsegueSql(conn, sql)
    End Sub

    Public Function ConverteImmagine(Immagine As String) As String
        Dim sDatella As String = Now.Year & Format(Now.Month, "00") & Format(Now.Day, "00") & Format(Now.Hour, "00") & Format(Now.Minute, "00") & Format(Now.Second, "00")

        Dim Immagine2 As String = "Buttami\buttami_" & sDatella & ".jpg"
        Dim Immagine3 As String = "Buttami\buttamiBN_" & sDatella & ".jpg"
        Dim quadrettoX As Integer
        Dim quadrettoY As Integer
        Dim qX As Long
        Dim qY As Long
        Dim Colore As Color
        Dim Divisore As Integer
        Dim Stringona As String = ""
        Dim r As Integer
        Dim g As Integer
        Dim b As Integer
        Dim C(2) As Integer
        Dim A As Integer

        Divisore = 32
        qX = 60
        qY = 60
        quadrettoX = 3
        quadrettoY = 3

        Dim gi As New GestioneImmagini
        If gi.Ridimensiona(Immagine, Immagine2, qX, qY) = False Then
            Return ""
            Exit Function
        End If
        gi.ConverteImmaginInBN(Immagine2, Immagine3)
        gi = Nothing

        Dim imgImmagine As Image

        imgImmagine = New Bitmap(Immagine3)
        qX = imgImmagine.Width
        qY = imgImmagine.Height

        For I = 1 To qX Step quadrettoX
            For k = 1 To qY Step quadrettoY
                Colore = DirectCast(imgImmagine, Bitmap).GetPixel(k, I)

                r = Colore.R * 0.49999999999999994
                g = Colore.G * 0.49000000000000005
                b = Colore.B * 0.49999999999999595

                r = CInt((r \ Divisore)) * Divisore
                b = CInt((b \ Divisore)) * Divisore
                g = CInt((g \ Divisore)) * Divisore

                C(0) = r
                C(1) = b
                C(2) = g
                For Z = 0 To 2
                    For L = Z + 1 To 2
                        If C(Z) < C(L) Then
                            A = C(Z)
                            C(Z) = C(L)
                            C(L) = A
                        End If
                    Next L
                Next Z
                r = C(0)

                Stringona += Format(r, "000")
            Next k
        Next I

        imgImmagine.Dispose()
        imgImmagine = Nothing

        Return Stringona
    End Function

    Public Function ScriveDimensioniImmagine(Nome As String, lbl As Label) As Long
        Dim bt As System.Drawing.Bitmap
        Dim gf As New GestioneFilesDirectory
        Dim c As String = gf.TornaNomeDirectoryDaPath(Nome)
        Dim n As String = gf.TornaNomeFileDaPath(Nome)
        If n.Length > 100 Then
            n = Mid(n, 1, 47) & Mid(n, n.Length - 47, n.Length)
        End If
        Nome = c & "\" & n
        Dim d As Long = -1
        If File.Exists(Nome) Then
            bt = System.Drawing.Image.FromFile(Nome)

            Dim w As Integer = bt.Width
            Dim h As Integer = bt.Height
            d = FileLen(Nome) / 1024

            If Not lbl Is Nothing Then
                lbl.Text = d & " Kb - " & w & "x" & h
            End If

            bt.Dispose()
            bt = Nothing
        End If

        Return d
    End Function

End Class
