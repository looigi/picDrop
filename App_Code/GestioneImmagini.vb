Imports System.IO
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Drawing.Image

Public Class GestioneImmagini
    Public Function RitornaDimensioneImmagine(Immagine As String) As String
        If File.Exists(Immagine) = False Then
            Return "0x0"
        End If

        Dim bt As Bitmap = CaricaImmagine(Immagine)
        Dim w As Integer = 0
        Dim h As Integer = 0

        If bt Is Nothing = False Then
            w = bt.Width
            h = bt.Height

            bt.Dispose()
        Else
            Stop
            Try
                Kill(Immagine)
            Catch ex As Exception

            End Try
        End If
        bt = Nothing

        Return w & "x" & h
    End Function

    Private Function CaricaImmagine(NomeImmagine As String) As Image
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

    Public Sub ConverteImmaginInBN(Path As String, Path2 As String)
        Dim img As Bitmap
        Dim ImmaginePiccola As Image
        Dim ImmaginePiccola2 As Image
        Dim jgpEncoder As Imaging.ImageCodecInfo
        Dim myEncoder As System.Drawing.Imaging.Encoder
        Dim myEncoderParameters As New Imaging.EncoderParameters(1)

        img = New Bitmap(Path)

        ImmaginePiccola = New Bitmap(img)

        img = Nothing

        ImmaginePiccola = Converte(ImmaginePiccola)

        jgpEncoder = GetEncoder(Imaging.ImageFormat.Jpeg)
        myEncoder = System.Drawing.Imaging.Encoder.Quality
        Dim myEncoderParameter As New Imaging.EncoderParameter(myEncoder, 75)
        myEncoderParameters.Param(0) = myEncoderParameter

        ImmaginePiccola.Save(Path2, jgpEncoder, myEncoderParameters)

        ImmaginePiccola = Nothing
        ImmaginePiccola2 = Nothing
        jgpEncoder = Nothing
        myEncoderParameter = Nothing
    End Sub

    Public Sub CreaThumbDaDb(Progressivo As Integer, NomeFile As String, Cosa As String)
        Dim Nome As String = "Buttami\" & Progressivo & ".Jpg"

        If File.Exists(Nome) = False Then
            Dim Immagine As Bitmap
            Dim x As Integer = 0
            Dim y As Integer = 0
            Dim cs As String
            Dim c As Integer
            Dim cc As Color

            Immagine = New Bitmap(60, 60)
            For i As Integer = 0 To Cosa.Length - 1 Step 3
                cs = Cosa.Substring(i, 3)
                c = Val(cs)
                If c = 0 Then c = 255
                cc = Color.FromArgb(c, c, c)

                For k As Integer = 0 To 2
                    For Z As Integer = 0 To 2
                        Immagine.SetPixel(x + k, y + Z, cc)
                    Next
                Next

                x += 3
                If x = 60 Then
                    x = 0
                    y += 3
                End If
            Next
            Immagine.Save(Nome)

            Immagine.Dispose()
            Immagine = Nothing
        End If
    End Sub

    Public Function Ridimensiona(Path As String, Path2 As String, Larghezza As Integer, Altezza As Integer) As Boolean
        Dim myEncoder As System.Drawing.Imaging.Encoder
        Dim myEncoderParameters As New Imaging.EncoderParameters(1)
        Dim img2 As Bitmap
        Dim ImmaginePiccola22 As Image
        Dim jgpEncoder2 As Imaging.ImageCodecInfo
        Dim myEncoder2 As System.Drawing.Imaging.Encoder
        Dim myEncoderParameters2 As New Imaging.EncoderParameters(1)

        Try
            img2 = New Bitmap(Path)
        Catch ex As Exception
            Return False
            Exit Function
        End Try

        ImmaginePiccola22 = New Bitmap(img2, Val(Larghezza), Val(Altezza))
        img2 = Nothing

        myEncoder = System.Drawing.Imaging.Encoder.Quality
        jgpEncoder2 = GetEncoder(Imaging.ImageFormat.Jpeg)
        myEncoder2 = System.Drawing.Imaging.Encoder.Quality
        Dim myEncoderParameter2 As New Imaging.EncoderParameter(myEncoder, 75)
        myEncoderParameters2.Param(0) = myEncoderParameter2
        ImmaginePiccola22.Save(Path2, jgpEncoder2, myEncoderParameters2)

        ImmaginePiccola22 = Nothing
        jgpEncoder2 = Nothing
        myEncoderParameter2 = Nothing

        Return True
    End Function

    Private Function Converte(ByVal inputImage As Image) As Image
        Dim outputBitmap As Bitmap = New Bitmap(inputImage.Width, inputImage.Height)
        Dim X As Long
        Dim Y As Long
        Dim currentBWColor As Color

        For X = 0 To outputBitmap.Width - 1
            For Y = 0 To outputBitmap.Height - 1
                currentBWColor = ConverteColore(DirectCast(inputImage, Bitmap).GetPixel(X, Y))
                outputBitmap.SetPixel(X, Y, currentBWColor)
            Next
        Next

        inputImage = Nothing
        Return outputBitmap
    End Function

    Private Function ConverteColore(ByVal InputColor As Color)
        'Dim eyeGrayScale As Integer = (InputColor.R * 0.3 + InputColor.G * 0.59 + InputColor.B * 0.11)
        Dim Rosso As Single = InputColor.R * 0.3
        Dim Verde As Single = InputColor.G * 0.59
        Dim Blu As Single = InputColor.B * 0.41
        Dim eyeGrayScale As Integer = (Rosso + Verde + Blu) ' * 1.7
        If eyeGrayScale > 255 Then eyeGrayScale = 255
        Dim outputColor As Color = Color.FromArgb(eyeGrayScale, eyeGrayScale, eyeGrayScale)

        Return outputColor
    End Function

    Private Function ConverteChiara(ByVal inputImage As Image) As Image
        Dim outputBitmap As Bitmap = New Bitmap(inputImage.Width, inputImage.Height)
        Dim X As Long
        Dim Y As Long
        Dim currentBWColor As Color

        For X = 0 To outputBitmap.Width - 1
            For Y = 0 To outputBitmap.Height - 1
                currentBWColor = ConverteColoreChiaro(DirectCast(inputImage, Bitmap).GetPixel(X, Y))
                outputBitmap.SetPixel(X, Y, currentBWColor)
            Next
        Next

        inputImage = Nothing
        Return outputBitmap
    End Function

    Private Function ConverteColoreChiaro(ByVal InputColor As Color)
        'Dim eyeGrayScale As Integer = (InputColor.R * 0.3 + InputColor.G * 0.59 + InputColor.B * 0.11)
        Dim Rosso As Single = InputColor.R * 0.49999999999999994
        Dim Verde As Single = InputColor.G * 0.49000000000000005
        Dim Blu As Single = InputColor.B * 0.49999999999999595
        Dim eyeGrayScale As Integer = (Rosso + Verde + Blu) '* 4.1000000000000005
        If eyeGrayScale > 250 Then eyeGrayScale = 250
        If eyeGrayScale < 185 Then eyeGrayScale = 185
        Dim outputColor As Color = Color.FromArgb(eyeGrayScale, eyeGrayScale, eyeGrayScale)

        Return outputColor
    End Function

    Private Function GetEncoder(ByVal format As Imaging.ImageFormat) As Imaging.ImageCodecInfo

        Dim codecs As Imaging.ImageCodecInfo() = Imaging.ImageCodecInfo.GetImageDecoders()

        Dim codec As Imaging.ImageCodecInfo
        For Each codec In codecs
            If codec.FormatID = format.Guid Then
                Return codec
            End If
        Next codec
        Return Nothing

    End Function

    Public Sub RidimensionaEArrotondaIcona(ByVal PercorsoImmagine As String)
        Dim bm As Bitmap
        Dim originalX As Integer
        Dim originalY As Integer

        'carica immagine originale
        bm = New Bitmap(PercorsoImmagine)

        originalX = bm.Width
        originalY = bm.Height

        Dim thumb As New Bitmap(originalX, originalY)
        Dim g As Graphics = Graphics.FromImage(thumb)

        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        g.DrawImage(bm, New Rectangle(0, 0, originalX, originalY), New Rectangle(0, 0, bm.Width, bm.Height), GraphicsUnit.Pixel)

        Dim r As System.Drawing.Rectangle
        Dim s As System.Drawing.Size
        Dim coloreRosso As Pen = New Pen(Color.Red)
        coloreRosso.Width = 3

        For dimeX = originalX - 15 To originalX * 2
            r.X = (originalX / 2) - (dimeX / 2)
            r.Y = (originalY / 2) - (dimeX / 2)
            s.Width = dimeX
            s.Height = dimeX
            r.Size = s
            g.DrawEllipse(coloreRosso, r)
        Next

        Dim InizioY As Integer = -1
        Dim InizioX As Integer = -1
        Dim FineY As Integer = -1
        Dim FineX As Integer = -1
        Dim pixelColor As Color

        For i As Integer = 1 To originalX - 1
            For k As Integer = 1 To originalY - 1
                pixelColor = thumb.GetPixel(i, k)
                If pixelColor.ToArgb <> Color.Red.ToArgb Then
                    InizioX = i
                    'varConnessione.DrawLine(Pens.Black, i, 0, i, originalY)
                    Exit For
                End If
            Next
            If InizioX <> -1 Then
                Exit For
            End If
        Next

        For i As Integer = originalX - 1 To 1 Step -1
            For k As Integer = originalY - 1 To 1 Step -1
                pixelColor = thumb.GetPixel(i, k)
                If pixelColor.ToArgb <> Color.Red.ToArgb Then
                    FineX = i
                    'varConnessione.DrawLine(Pens.Black, i, 0, i, originalY)
                    Exit For
                End If
            Next
            If FineX <> -1 Then
                Exit For
            End If
        Next

        For i As Integer = 1 To originalY - 1
            For k As Integer = 1 To originalX - 1
                pixelColor = thumb.GetPixel(k, i)
                If pixelColor.ToArgb <> Color.Red.ToArgb Then
                    InizioY = i
                    'varConnessione.DrawLine(Pens.Black, 0, i, originalX, i)
                    Exit For
                End If
            Next
            If InizioY <> -1 Then
                Exit For
            End If
        Next

        For i As Integer = originalY - 1 To 1 Step -1
            For k As Integer = originalX - 1 To 1 Step -1
                pixelColor = thumb.GetPixel(k, i)
                If pixelColor.ToArgb <> Color.Red.ToArgb Then
                    FineY = i
                    'varConnessione.DrawLine(Pens.Black, 0, i, originalX, i)
                    Exit For
                End If
            Next
            If FineY <> -1 Then
                Exit For
            End If
        Next

        Dim nDimeX As Integer = FineX - InizioX
        Dim nDimeY As Integer = FineY - InizioY

        r.X = InizioX - 1
        r.Y = InizioY - 1
        r.Width = nDimeX + 1
        r.Height = nDimeY + 1

        Dim bmpAppoggio As Bitmap = New Bitmap(nDimeX, nDimeY)
        Dim g2 As Graphics = Graphics.FromImage(bmpAppoggio)

        g2.DrawImage(thumb, 0, 0, r, GraphicsUnit.Pixel)

        thumb = bmpAppoggio
        g2.Dispose()

        g.Dispose()

        thumb.MakeTransparent(Color.Red)

        thumb.Save(PercorsoImmagine & ".tsz", System.Drawing.Imaging.ImageFormat.Png)
        bm.Dispose()
        thumb.Dispose()

        Try
            Kill(PercorsoImmagine)
        Catch ex As Exception

        End Try

        Rename(PercorsoImmagine & ".tsz", PercorsoImmagine)
    End Sub

    Public Function RuotaFoto(Nome As String, Angolo As Single) As String
        Dim r As RotateFlipType

        Select Case Angolo
            Case 1
                r = RotateFlipType.RotateNoneFlipX
            Case 2
                r = RotateFlipType.RotateNoneFlipY
            Case 90
                r = RotateFlipType.Rotate90FlipNone
            Case -90
                r = RotateFlipType.Rotate270FlipNone
        End Select

        Dim bitmap1 As Bitmap = CType(Bitmap.FromFile(Nome), Bitmap)

        bitmap1.RotateFlip(r)
        bitmap1.Save(Nome & ".ruo", System.Drawing.Imaging.ImageFormat.Jpeg)
        bitmap1.Dispose()
        bitmap1 = Nothing

        Try
            Kill(Nome)

            Rename(Nome & ".ruo", Nome)

            Return "OK"
        Catch ex As Exception
            Return "ERRORE: " & ex.Message
        End Try
    End Function
End Class
