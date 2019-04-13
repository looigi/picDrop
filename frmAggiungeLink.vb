Public Class frmAggiungeLink
    Private CarattereSpeciale As String = "*****"

    Private Sub frmAggiungeLink_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblTitolo.Text = "Inserire " & CarattereSpeciale & " per scaricamenti multipli"
    End Sub

    Private Sub cmdSceglieFile_Click(sender As Object, e As EventArgs) Handles cmdSceglieFile.Click
        OpenFileDialog1.InitialDirectory = GetSetting("picDrop", "Settaggi", "PathFile", Application.StartupPath)
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            txtIndirizzo.Text = OpenFileDialog1.FileName
            Dim gf As New GestioneFilesDirectory
            Dim path As String = gf.TornaNomeDirectoryDaPath(txtIndirizzo.Text)
            gf = Nothing
            SaveSetting("picDrop", "Settaggi", "PathFile", path)
        End If
    End Sub

    Private Sub cmdOk_Click(sender As Object, e As EventArgs) Handles cmdOk.Click
        Dim CarattereSpeciale As String = "*****"

        Dim Ritorno As String = txtIndirizzo.Text

        If Ritorno <> "" Then
            If Ritorno.IndexOf(CarattereSpeciale) > -1 Then
                Dim ValoreMinimo As Integer = 1
                Dim Passo As Integer = 1
                Dim ValoreMassimo As Integer = 100

                Dim Ritorno2 As String
                Dim Ancora As Boolean = True

                Do While Ancora = True
                    Ritorno2 = InputBox("Valore minimo pagina", , ValoreMinimo)
                    If Ritorno2 <> "" And IsNumeric(Ritorno2) = True Then
                        ValoreMinimo = Ritorno2
                        Ancora = False
                    End If
                Loop

                Ancora = True
                Do While Ancora = True
                    Ritorno2 = InputBox("Valore massimo pagina", , ValoreMassimo)
                    If Ritorno2 <> "" And IsNumeric(Ritorno2) = True Then
                        ValoreMassimo = Ritorno2
                        Ancora = False
                    End If
                Loop

                Ancora = True
                Do While Ancora = True
                    Ritorno2 = InputBox("Passo", , Passo)
                    If Ritorno2 <> "" And IsNumeric(Ritorno2) = True Then
                        Passo = Ritorno2
                        Ancora = False
                    End If
                Loop

                If ValoreMinimo > ValoreMassimo Then
                    Dim Appoggio As Integer = ValoreMinimo
                    ValoreMinimo = ValoreMassimo
                    ValoreMassimo = Appoggio
                End If

                For i As Integer = ValoreMinimo To ValoreMassimo Step Passo
                    Ritorno2 = Ritorno.Replace(CarattereSpeciale, i)

                    If frmMain.MetteInCodaLink(Ritorno2) Then
                        Me.Close()
                    End If
                Next
            Else
                If frmMain.MetteInCodaLink(Ritorno) Then
                    Me.Close()
                    frmMain.FaiRipartireTimerDownload()
                End If
            End If

        Else
            MsgBox("Inserire un indirizzo o un file valido", vbInformation)
        End If
    End Sub

    Private Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        Me.Close()
    End Sub
End Class