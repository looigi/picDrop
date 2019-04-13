Public Class frmSettaggi

    Private StaModificando As Boolean = False

    Private Sub frmSettaggi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ImpostaMaschera()
    End Sub

    Private Sub ImpostaMaschera()
        If TipoCollegamento = "Proxy" Then
            optProxy.Checked = True
            optNormale.Checked = False
            Panel1.Enabled = True
        Else
            optProxy.Checked = False
            optNormale.Checked = True
            Panel1.Enabled = False
        End If

        txtUtenza.Text = Utenza
        txtPassword.Text = Password
        txtDominio.Text = Dominio

        chkLog.Checked = CreaLog
        chkSovrascrivi.Checked = Sovrascrivi
        chkApre.Checked = ApreFinestra
        chkScartaPiccole.Checked = ScartaPiccole
        chkGiaScaricata.Checked = ScaricaAncheSeGiaScaricata

        'cmdAggiunge.Visible = False

        LeggeCartelle()

        RiempieCartelle()
    End Sub

    Private Sub optProxy_Click(sender As Object, e As EventArgs) Handles optProxy.Click
        optNormale.Checked = False
        Panel1.Enabled = True
        TipoCollegamento = "Proxy"

        SaveSetting("picDrop", "Settaggi", "TipoCollegamento", "Proxy")
    End Sub

    Private Sub optNormale_Click(sender As Object, e As EventArgs) Handles optNormale.Click
        optProxy.Checked = False
        Panel1.Enabled = False
        TipoCollegamento = "Normale"

        SaveSetting("picDrop", "Settaggi", "TipoCollegamento", "Normale")
    End Sub

    Private Sub cmdSalva_Click(sender As Object, e As EventArgs) Handles cmdSalva.Click
        SaveSetting("picDrop", "Settaggi", "Utenza", txtUtenza.Text)
        SaveSetting("picDrop", "Settaggi", "Password", txtPassword.Text)
        SaveSetting("picDrop", "Settaggi", "Dominio", txtDominio.Text)

        Utenza = txtUtenza.Text
        Password = txtPassword.Text
        Dominio = txtDominio.Text

        MsgBox("Dati salvati")
    End Sub

    Private Sub SalvaFileDir()
        Dim Cosa As String = ""

        For i As Integer = 0 To lstCartelle.Items.Count - 1
            If lstCartelle.Items(i).ToString.Trim <> "" Then
                Cosa += lstCartelle.Items(i).ToString.Trim & "§"
            End If
        Next

        Try
            Kill("Dirs.txt")
        Catch ex As Exception

        End Try

        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter("Dirs.txt", True)
        file.WriteLine(Cosa)
        file.Close()
    End Sub

    Private Sub cmdSceglie_Click(sender As Object, e As EventArgs) Handles cmdSceglie.Click
        Dim folder As New FolderBrowserDialog

        If folder.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtDirectory.Text = folder.SelectedPath
            cmdAggiunge.Visible = True
        End If
    End Sub

    Private Sub cmdAggiunge_Click(sender As Object, e As EventArgs) Handles cmdAggiunge.Click
        If txtNome.Text = "" Then
            MsgBox("Selezionare una descrizione per il tipo", vbInformation)
            Exit Sub
        End If
        If txtDirectory.Text = "" Then
            MsgBox("Selezionare la directory di destinazione per il tipo", vbInformation)
            Exit Sub
        End If

        If cmdAggiunge.Text = "->" Then
            lstCartelle.Items.Add(txtNome.Text & ">" & txtDirectory.Text)
        Else
            Dim i As Integer = lstCartelle.SelectedIndex

            lstCartelle.Items(i) = txtNome.Text & ">" & txtDirectory.Text
        End If
        txtDirectory.Text = ""
        txtNome.Text = ""
        'cmdAggiunge.Visible = False

        SalvaFileDir()

        LeggeCartelle()

        cmdAggiunge.Text = "->"
    End Sub

    Private Sub RiempieCartelle()
        lstCartelle.Items.Clear()

        For i As Integer = 0 To Cartelle.Length - 1
            If Nomi(i) <> "" And Cartelle(i) <> "" Then
                lstCartelle.Items.Add(Nomi(i) & ">" & Cartelle(i))
            End If
        Next
    End Sub

    Private Sub lstCartelle_DoubleClick(sender As Object, e As EventArgs) Handles lstCartelle.DoubleClick
        Dim Elimina As String

        If lstCartelle.Text <> "" Then
            Elimina = lstCartelle.Text
            For i As Integer = 0 To lstCartelle.Items.Count - 1
                If lstCartelle.Items(i) = Elimina Then
                    lstCartelle.Items.RemoveAt(i)
                    Exit For
                End If
            Next

            SalvaFileDir()

            LeggeCartelle()
        End If
    End Sub

    Private Sub chkLog_Click(sender As Object, e As EventArgs) Handles chkLog.Click
        Dim Cosa As String

        If chkLog.Checked = True Then
            Cosa = "True"
            CreaLog = True
        Else
            Cosa = "False"
            CreaLog = False
        End If

        SaveSetting("picDrop", "Settaggi", "Log", Cosa)
    End Sub

    Private Sub chkSovrascrivi_Click(sender As Object, e As EventArgs) Handles chkSovrascrivi.Click
        Dim Cosa As String

        If chkSovrascrivi.Checked = True Then
            Cosa = "True"
            Sovrascrivi = True
        Else
            Cosa = "False"
            Sovrascrivi = False
        End If

        SaveSetting("picDrop", "Settaggi", "Sovrascrivi", Cosa)
    End Sub

    Private Sub chkGiaScaricata_Click(sender As Object, e As EventArgs) Handles chkGiaScaricata.Click
        Dim Cosa As String

        If chkGiaScaricata.Checked = True Then
            Cosa = "True"
            ScaricaAncheSeGiaScaricata = True
        Else
            Cosa = "False"
            ScaricaAncheSeGiaScaricata = False
        End If

        SaveSetting("picDrop", "Settaggi", "GiaScaricata", Cosa)
    End Sub

    Private Sub chkScartaPiccole_CheckedChanged(sender As Object, e As EventArgs) Handles chkScartaPiccole.CheckedChanged
        Dim Cosa As String

        If chkScartaPiccole.Checked = True Then
            Cosa = "True"
            ScartaPiccole = True
        Else
            Cosa = "False"
            ScartaPiccole = False
        End If

        SaveSetting("picDrop", "Settaggi", "ScartaPiccole", Cosa)
    End Sub

    Private Sub chkApre_Click(sender As Object, e As EventArgs) Handles chkApre.Click
        Dim Cosa As String

        If chkApre.Checked = True Then
            Cosa = "True"
            ApreFinestra = True
        Else
            Cosa = "False"
            ApreFinestra = False
        End If

        SaveSetting("picDrop", "Settaggi", "ApreFinestra", Cosa)
    End Sub

    Private Sub lstCartelle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstCartelle.Click
        Dim t As String = lstCartelle.Text

        txtNome.Text = Mid(t, 1, t.IndexOf(">"))
        txtDirectory.Text = Mid(t, t.IndexOf(">") + 2, t.Length)

        cmdAggiunge.Text = ">"
    End Sub

    Private Sub cmdPulisce_Click(sender As Object, e As EventArgs) Handles cmdPulisce.Click
        txtDirectory.Text = ""
        txtUtenza.Text = ""

        cmdAggiunge.Text = "->"
    End Sub

    Private Sub chkPathSito_CheckedChanged(sender As Object, e As EventArgs) Handles chkPathSito.Click
        Dim Cosa As String

        If chkPathSito.Checked = True Then
            Cosa = "True"
            PathSito = True
        Else
            Cosa = "False"
            PathSito = False
        End If

        SaveSetting("picDrop", "Settaggi", "PathSito", Cosa)
    End Sub
End Class