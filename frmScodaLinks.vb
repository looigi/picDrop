Imports System.IO

Public Class frmScodaLinks
    Private NomeDest As String
    Private DirDest As String
    Private QuanteImm As Integer
    Private QualeImm As Integer
    Private NomeImmagine() As String
    Private Operazioni() As String

    Private Iniziato As Boolean = False
    Private CoordInizioIniziale As New PointF
    Private CoordInizio As New PointF
    Private CoordFine As New PointF
    Private Rect As Rectangle
    Private percentualeResize As Single

    Private varConnessione As SQLSERVERCE
    Private conn As Object

    Public Sub ImpostaConnessione(vc As SQLSERVERCE, c As Object)
        varConnessione = vc
        conn = c
    End Sub

    Private Esci As Boolean = False

    Private Sub cmdUscita_Click(sender As Object, e As EventArgs) Handles cmdUscita.Click
        Esci = True

        Me.Hide()
        Me.Close()
        Me.Dispose()

        frmMain.ApreConnessione()
        frmMain.Show()
    End Sub

    Private Sub frmScodaLinks_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Esci = False Then
            e.Cancel = True
        End If
    End Sub

    Private Tasti() As Button

    Private Sub frmScodaLinks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 8, FontStyle.Regular)
        'Dim x As Integer

        cmbDest.Items.Clear()
        For i As Integer = 0 To Cartelle.Length - 1
            ReDim Preserve Tasti(i)
            Tasti(i) = New Button
            Tasti(i).Font = myFont
            Tasti(i).Width = 35
            Tasti(i).Text = Nomi(i)

            AddHandler Tasti(i).Click, AddressOf TastoClickato

            Panel2.Controls.Add(Tasti(i))

            cmbDest.Items.Add(Nomi(i))
        Next
        cmbDest.Text = cmbDest.Items(0)


        DirDest = Cartelle(0)

        CaricaImmagini()

        If QuanteImm = 0 Then
            MsgBox("Nessuna immagine nei links")

            'Me.Hide()
            'Me.Close()
            'Me.Dispose()

            'frmMain.ApreConnessione()
            'frmMain.Show()

            cmdUpdate.Enabled = False
            cmdRuota.Enabled = False
            cmdElimina.Enabled = False
            cmdSposta.Enabled = False
            cmdAvanti.Enabled = False
            cmdIndietro.Enabled = False
            cmdUpdate.Enabled = False
            cmbDest.Enabled = False
            lblNomeImm.Text = ""
            lblOperazione.Text = ""
            picScoda.Enabled = False
        Else
            QualeImm = 1
            CaricaImmagine()
        End If
    End Sub

    Private Sub TastoClickato(sender As Object, e As EventArgs)
        Dim b As Button = DirectCast(sender, Button)

        For i As Integer = 0 To Cartelle.Length - 1
            If BottoniDirs(i).Text = b.Text Then
                Operazioni(QualeImm) = "Sposta Verso " & b.Text

                Call cmdAvanti_Click(sender, e)
            End If
        Next
    End Sub

    Private GifImage As GifImage

    Private Sub CaricaImmagine()
        If QuanteImm = 0 Then
            Exit Sub
        End If

        Dim Errore As Boolean = False
        Dim EGif As Boolean = False

        If NomeImmagine(QualeImm).ToUpper.IndexOf(".GIF") > -1 Then
            EGif = True

            GifImage = New GifImage(NomeImmagine(QualeImm))
            GifImage.ReverseAtEnd = False

            Timer1.Enabled = True
        Else
            Dim fs As System.IO.FileStream

            picScoda.Image = Nothing
            Try
                fs = New System.IO.FileStream(NomeImmagine(QualeImm),
                     IO.FileMode.Open, IO.FileAccess.Read)
                picScoda.Image = System.Drawing.Image.FromStream(fs)
                fs.Close()
            Catch ex As Exception
                Errore = True
            End Try
            fs = Nothing

            Timer1.Enabled = False

            If GifImage Is Nothing = False Then
                GifImage.Pulisce()
                GifImage = Nothing
            End If
        End If

        Dim sNome As String = NomeImmagine(QualeImm)

        If sNome.Length > 50 Then
            sNome = Mid(sNome, 1, 23) & "..." & Mid(sNome, sNome.Length - 22, 23)
        End If

        If Errore = False Then
            Dim gi As New GestioneImmagini

            Dim Dime() As String = gi.RitornaDimensioneImmagine(NomeImmagine(QualeImm)).Split("x")
            Dim dX As Integer = Val(Dime(0))
            Dim dY As Integer = Val(Dime(1))
            Dim odX As Integer = dX
            Dim odY As Integer = dY

            gi = Nothing

            Dim sX As Integer = Me.Width
            Dim sY As Integer = Me.Height

            Dim d1 As Single = dX / (sX - 70)
            Dim d2 As Single = dY / (sY - 160)

            If d1 > d2 Then
                percentualeResize = d1
            Else
                percentualeResize = d2
            End If

            dX /= percentualeResize
            dY /= percentualeResize

            picScoda.Width = dX
            picScoda.Height = dY

            picScoda.Left = (sX / 2) - (dX / 2)
            picScoda.Top = ((sY / 2) - (dY / 2)) - 25

            For i As Integer = 0 To Tasti.Length - 1
                Tasti(i).Left = (i * 35) + (cmdElimina.Left + cmdElimina.Width) + 5
                Tasti(i).Top = cmdAvanti.Top
            Next

            lblNomeImm.Text = QualeImm & "/" & QuanteImm & ": (" & odX & "x" & odY & ") " & sNome
            lblOperazione.Text = Operazioni(QualeImm)

            picScoda.Visible = True
        Else
            lblNomeImm.Text = QualeImm & "/" & QuanteImm & ": " & sNome & " - Errore caricamento"
            lblOperazione.Text = ""

            picScoda.Visible = False
        End If

        cmdRitaglia.Visible = False
        Iniziato = False
    End Sub

    Private Sub CaricaImmagini()
        Dim gf As New GestioneFilesDirectory
        gf.PulisceInfo()
        gf.ScansionaDirectorySingola("Links", 2)
        Dim QuanteImmApp As Integer = gf.RitornaQuantiFilesRilevati
        Dim NomeImmagineApp() As String = gf.RitornaFilesRilevati
        Erase NomeImmagine
        QuanteImm = 0

        If Not NomeImmagineApp Is Nothing Then
            For Each s As String In NomeImmagineApp
                If Not s Is Nothing Then
                    If s.ToUpper.Contains(".HTM") Or s.ToUpper.Contains("APPOGGIO") Or s.ToUpper.Contains("BUTTAMI.JPG") Then
                    Else
                        If s.ToUpper.Contains(".BBB") Then
                            Dim est As String = gf.TornaEstensioneFileDaPath(s)
                            Dim S2 As String = s.Replace(est, "")
                            Rename(s, S2)
                            s = S2
                        End If

                        QuanteImm += 1
                        ReDim Preserve NomeImmagine(QuanteImm)
                        NomeImmagine(QuanteImm) = s
                    End If
                End If
            Next

            Erase NomeImmagineApp
        End If

        ReDim Operazioni(QuanteImm)
        For i As Integer = 1 To QuanteImm
            Operazioni(i) = ""
        Next

        gf = Nothing
    End Sub

    Private Sub cmbDest_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDest.SelectedIndexChanged
        For i As Integer = 0 To Nomi.Length - 1
            If Nomi(i) = cmbDest.Text Then
                NomeDest = Nomi(i)
                DirDest = Cartelle(i)
                Exit For
            End If
        Next
    End Sub

    Private Sub cmdIndietro_Click(sender As Object, e As EventArgs) Handles cmdIndietro.Click
        QualeImm -= 1
        If QualeImm = 0 Then
            QualeImm = QuanteImm
        End If

        CaricaImmagine()
    End Sub

    Private Sub cmdAvanti_Click(sender As Object, e As EventArgs) Handles cmdAvanti.Click
        QualeImm += 1
        If QualeImm > QuanteImm Then
            QualeImm = 1
        End If

        CaricaImmagine()
    End Sub

    Private Sub cmdElimina_Click(sender As Object, e As EventArgs) Handles cmdElimina.Click
        Operazioni(QualeImm) = "Elimina"

        Call cmdAvanti_Click(sender, e)
    End Sub

    Private Sub cmdSposta_Click(sender As Object, e As EventArgs) Handles cmdSposta.Click
        Operazioni(QualeImm) = "Sposta Verso " & DirDest

        Call cmdAvanti_Click(sender, e)
    End Sub

    Private Sub Aggiorna(sender As Object, e As EventArgs) Handles cmdUpdate.Click
        Dim Destinazione As String
        Dim d As String
        Dim gf As New GestioneFilesDirectory
        Dim Fatto As Boolean = False
        Dim sql As String
        Dim Appoggio As String = Me.Text

        For i As Integer = 0 To QuanteImm
            Me.Text = "Elaborazione " & i + 1 & "/" & QuanteImm
            Application.DoEvents()

            If Operazioni(i) <> "" Then
                If Mid(Operazioni(i), 1, 7) = "Elimina" Then
                    gf.EliminaFileFisico(NomeImmagine(i))
                    Fatto = True
                Else
                    If Mid(Operazioni(i), 1, 12) = "Sposta Verso" Then
                        Dim PathImm As String = ""

                        d = Mid(Operazioni(i), 13, Operazioni.Length).Trim.ToUpper
                        For k As Integer = 0 To Cartelle.Length - 1
                            If Nomi(k).ToUpper.Trim = d Then
                                PathImm = Cartelle(k)
                                Exit For
                            End If
                        Next
                        If PathImm = "" Then Stop

                        Destinazione = gf.TornaNomeFileDaPath(NomeImmagine(i))
                        Destinazione = PathImm & "\" & Destinazione

                        gf.CopiaOMuoveFileFisico(NomeImmagine(i), Destinazione, False, True)
                        Fatto = True

                        sql = "Update Indirizzi Set NomeFile='" & Destinazione.Replace("'", "''") & "' Where NomeFile='" & NomeImmagine(i).Replace("'", "''") & "'"
                        varConnessione.EsegueSql(conn, sql)
                    End If
                End If
            End If
        Next

        Me.Text = Appoggio

        If Fatto = True Then
            MsgBox("Operazione effettuata")

            'Me.Hide()
            'Me.Close()
            'Me.Dispose()

            CaricaImmagini()

            QualeImm = 1
            CaricaImmagine()
        End If

        gf.EliminaAlberoDirectory("Links", GestioneFilesDirectory.NonEliminareRoot, GestioneFilesDirectory.NonEliminareFiles)
        gf = Nothing
    End Sub

    Private Sub frmScodaLinks_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        CaricaImmagine()
    End Sub

    Private Sub cmdRuota_Click(sender As Object, e As EventArgs) Handles cmdRuota.Click
        If pnlTools.Visible = False Then
            pnlTools.Visible = True
        Else
            pnlTools.Visible = False
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles cmdPiu90.Click
        RuotaImmagine(90)
    End Sub

    Private Sub cmdMeno90_Click(sender As Object, e As EventArgs) Handles cmdMeno90.Click
        RuotaImmagine(-90)
    End Sub

    Private Sub cmdBN_Click(sender As Object, e As EventArgs) Handles cmdBN.Click
        Dim gi As New GestioneImmagini
        gi.ConverteImmaginInBN(NomeImmagine(QualeImm), NomeImmagine(QualeImm) & ".BN")
        gi = Nothing

        Kill(NomeImmagine(QualeImm))
        Rename(NomeImmagine(QualeImm) & ".BN", NomeImmagine(QualeImm))

        CaricaImmagine()
    End Sub

    Private Sub cmdInverteOrizz_Click(sender As Object, e As EventArgs) Handles cmdInverteOrizz.Click
        RuotaImmagine(1)
    End Sub

    Private Sub RuotaImmagine(Come As Integer)
        Dim gi As New GestioneImmagini
        Dim Ritorno As String = gi.RuotaFoto(NomeImmagine(QualeImm), Come)
        gi = Nothing

        If Ritorno = "OK" Then
            CaricaImmagine()
        Else
            MsgBox(Ritorno)
        End If
    End Sub

    Private Sub cmdInverteVert_Click(sender As Object, e As EventArgs) Handles cmdInverteVert.Click
        RuotaImmagine(2)
    End Sub

    Private Sub picScoda_Click(sender As Object, e As EventArgs) Handles picScoda.Click
        If Iniziato = False Then
            Iniziato = True

            CoordInizio.X = -1
            CoordInizio.Y = -1
        Else
            Iniziato = False

            cmdRitaglia.Visible = True
        End If
    End Sub

    Private Sub picScoda_MouseMove(sender As Object, e As MouseEventArgs) Handles picScoda.MouseMove
        If Iniziato = True Then
            If CoordInizio.X = -1 And CoordInizio.Y = -1 Then
                CoordInizio.X = e.X
                CoordInizio.Y = e.Y

                CoordInizioIniziale.X = e.X
                CoordInizioIniziale.Y = e.Y
            End If

            CoordFine.X = e.X
            CoordFine.Y = e.Y

            If CoordFine.X < CoordInizio.X Then
                CoordInizio.X = CoordFine.X
                CoordFine.X = CoordInizioIniziale.X
            End If
            If CoordFine.Y < CoordInizio.Y Then
                CoordInizio.Y = CoordFine.Y
                CoordFine.Y = CoordInizioIniziale.Y
            End If

            picScoda.Invalidate()
        End If
    End Sub

    Private Sub picScoda_Paint(sender As Object, e As PaintEventArgs) Handles picScoda.Paint
        If Iniziato = True Then
            Rect.X = CoordInizio.X
            Rect.Y = CoordInizio.Y
            Rect.Width = CoordFine.X - CoordInizio.X
            Rect.Height = CoordFine.Y - CoordInizio.Y

            e.Graphics.DrawRectangle(Pens.Black, Rect)
        End If
    End Sub

    Private Sub cmdRitaglia_Click(sender As Object, e As EventArgs) Handles cmdRitaglia.Click
        Dim dX As Integer = Rect.Width * percentualeResize
        Dim dY As Integer = Rect.Height * percentualeResize

        Dim sourceBmp As New Bitmap(picScoda.Image)
        Dim destinationBmp As New Bitmap(dX, dY)
        Dim gr As Graphics = Graphics.FromImage(destinationBmp)

        Dim selectionRectangle As New Rectangle(Rect.X * percentualeResize, Rect.Y * percentualeResize, dX, dY)
        Dim destinationRectangle As New Rectangle(0, 0, dX, dY)

        gr.DrawImage(sourceBmp, destinationRectangle, selectionRectangle, GraphicsUnit.Pixel)

        picScoda.Image = destinationBmp

        picScoda.Image.Save(NomeImmagine(QualeImm) & ".RSZ")

        gr.Dispose()
        sourceBmp.Dispose()
        destinationBmp.Dispose()

        sourceBmp = Nothing
        destinationBmp = Nothing
        gr = Nothing

        Kill(NomeImmagine(QualeImm))
        Rename(NomeImmagine(QualeImm) & ".RSZ", NomeImmagine(QualeImm))

        CaricaImmagine()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        picScoda.Image = GifImage.GetNextFrame()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class