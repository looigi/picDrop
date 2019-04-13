Imports System.Globalization

Public Class frmStatistiche
    Private Sub frmStatistiche_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblFilesScaricati.Text = numFiles.ToString("0,0", CultureInfo.InvariantCulture)
        lblKbScaricati.Text = totKBytes.ToString("0,0", CultureInfo.InvariantCulture)
    End Sub
End Class