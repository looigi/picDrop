<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmListaDownload
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla nell'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmListaDownload))
        Me.lstListaDownload = New System.Windows.Forms.ListBox()
        Me.cmdBloccaTutti = New System.Windows.Forms.Button()
        Me.cmdRipristinaTutti = New System.Windows.Forms.Button()
        Me.cmdSelezionaTutti = New System.Windows.Forms.Button()
        Me.cmdDeSelezionaTutti = New System.Windows.Forms.Button()
        Me.lblStatistiche = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtFiltro = New System.Windows.Forms.TextBox()
        Me.cmdFiltra = New System.Windows.Forms.Button()
        Me.cnmdPulisceFiltro = New System.Windows.Forms.Button()
        Me.cmdEliminaSelezionati = New System.Windows.Forms.Button()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.optLinks = New System.Windows.Forms.RadioButton()
        Me.optImmagini = New System.Windows.Forms.RadioButton()
        Me.picImmagine = New System.Windows.Forms.PictureBox()
        CType(Me.picImmagine, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lstListaDownload
        '
        Me.lstListaDownload.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lstListaDownload.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstListaDownload.FormattingEnabled = True
        Me.lstListaDownload.HorizontalScrollbar = True
        Me.lstListaDownload.ItemHeight = 15
        Me.lstListaDownload.Location = New System.Drawing.Point(3, 29)
        Me.lstListaDownload.Name = "lstListaDownload"
        Me.lstListaDownload.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstListaDownload.Size = New System.Drawing.Size(651, 394)
        Me.lstListaDownload.TabIndex = 0
        '
        'cmdBloccaTutti
        '
        Me.cmdBloccaTutti.Location = New System.Drawing.Point(408, 468)
        Me.cmdBloccaTutti.Name = "cmdBloccaTutti"
        Me.cmdBloccaTutti.Size = New System.Drawing.Size(120, 23)
        Me.cmdBloccaTutti.TabIndex = 1
        Me.cmdBloccaTutti.Text = "Blocca selezionati"
        Me.cmdBloccaTutti.UseVisualStyleBackColor = True
        '
        'cmdRipristinaTutti
        '
        Me.cmdRipristinaTutti.Location = New System.Drawing.Point(534, 468)
        Me.cmdRipristinaTutti.Name = "cmdRipristinaTutti"
        Me.cmdRipristinaTutti.Size = New System.Drawing.Size(120, 23)
        Me.cmdRipristinaTutti.TabIndex = 2
        Me.cmdRipristinaTutti.Text = "Ripristina selezionati"
        Me.cmdRipristinaTutti.UseVisualStyleBackColor = True
        '
        'cmdSelezionaTutti
        '
        Me.cmdSelezionaTutti.Location = New System.Drawing.Point(3, 468)
        Me.cmdSelezionaTutti.Name = "cmdSelezionaTutti"
        Me.cmdSelezionaTutti.Size = New System.Drawing.Size(88, 23)
        Me.cmdSelezionaTutti.TabIndex = 3
        Me.cmdSelezionaTutti.Text = "Seleziona tutti"
        Me.cmdSelezionaTutti.UseVisualStyleBackColor = True
        '
        'cmdDeSelezionaTutti
        '
        Me.cmdDeSelezionaTutti.Location = New System.Drawing.Point(97, 468)
        Me.cmdDeSelezionaTutti.Name = "cmdDeSelezionaTutti"
        Me.cmdDeSelezionaTutti.Size = New System.Drawing.Size(106, 23)
        Me.cmdDeSelezionaTutti.TabIndex = 4
        Me.cmdDeSelezionaTutti.Text = "De-Seleziona tutti"
        Me.cmdDeSelezionaTutti.UseVisualStyleBackColor = True
        '
        'lblStatistiche
        '
        Me.lblStatistiche.Location = New System.Drawing.Point(3, 10)
        Me.lblStatistiche.Name = "lblStatistiche"
        Me.lblStatistiche.Size = New System.Drawing.Size(651, 16)
        Me.lblStatistiche.TabIndex = 5
        Me.lblStatistiche.Text = "Label1"
        Me.lblStatistiche.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 445)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Filtro"
        '
        'txtFiltro
        '
        Me.txtFiltro.Location = New System.Drawing.Point(59, 442)
        Me.txtFiltro.Name = "txtFiltro"
        Me.txtFiltro.Size = New System.Drawing.Size(306, 20)
        Me.txtFiltro.TabIndex = 7
        '
        'cmdFiltra
        '
        Me.cmdFiltra.Location = New System.Drawing.Point(371, 442)
        Me.cmdFiltra.Name = "cmdFiltra"
        Me.cmdFiltra.Size = New System.Drawing.Size(31, 20)
        Me.cmdFiltra.TabIndex = 8
        Me.cmdFiltra.Text = "->"
        Me.cmdFiltra.UseVisualStyleBackColor = True
        '
        'cnmdPulisceFiltro
        '
        Me.cnmdPulisceFiltro.Location = New System.Drawing.Point(408, 442)
        Me.cnmdPulisceFiltro.Name = "cnmdPulisceFiltro"
        Me.cnmdPulisceFiltro.Size = New System.Drawing.Size(31, 20)
        Me.cnmdPulisceFiltro.TabIndex = 9
        Me.cnmdPulisceFiltro.Text = "*"
        Me.cnmdPulisceFiltro.UseVisualStyleBackColor = True
        '
        'cmdEliminaSelezionati
        '
        Me.cmdEliminaSelezionati.Location = New System.Drawing.Point(534, 441)
        Me.cmdEliminaSelezionati.Name = "cmdEliminaSelezionati"
        Me.cmdEliminaSelezionati.Size = New System.Drawing.Size(120, 23)
        Me.cmdEliminaSelezionati.TabIndex = 10
        Me.cmdEliminaSelezionati.Text = "Elimina selezionati"
        Me.cmdEliminaSelezionati.UseVisualStyleBackColor = True
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Location = New System.Drawing.Point(660, 10)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(413, 481)
        Me.WebBrowser1.TabIndex = 11
        '
        'optLinks
        '
        Me.optLinks.AutoSize = True
        Me.optLinks.Location = New System.Drawing.Point(3, 6)
        Me.optLinks.Name = "optLinks"
        Me.optLinks.Size = New System.Drawing.Size(111, 17)
        Me.optLinks.TabIndex = 12
        Me.optLinks.TabStop = True
        Me.optLinks.Text = "Links da scaricare"
        Me.optLinks.UseVisualStyleBackColor = True
        '
        'optImmagini
        '
        Me.optImmagini.AutoSize = True
        Me.optImmagini.Location = New System.Drawing.Point(128, 6)
        Me.optImmagini.Name = "optImmagini"
        Me.optImmagini.Size = New System.Drawing.Size(112, 17)
        Me.optImmagini.TabIndex = 13
        Me.optImmagini.TabStop = True
        Me.optImmagini.Text = "Immagini scaricate"
        Me.optImmagini.UseVisualStyleBackColor = True
        '
        'picImmagine
        '
        Me.picImmagine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picImmagine.Location = New System.Drawing.Point(661, 10)
        Me.picImmagine.Name = "picImmagine"
        Me.picImmagine.Size = New System.Drawing.Size(412, 481)
        Me.picImmagine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picImmagine.TabIndex = 14
        Me.picImmagine.TabStop = False
        '
        'frmListaDownload
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1081, 495)
        Me.Controls.Add(Me.picImmagine)
        Me.Controls.Add(Me.optImmagini)
        Me.Controls.Add(Me.optLinks)
        Me.Controls.Add(Me.WebBrowser1)
        Me.Controls.Add(Me.cmdEliminaSelezionati)
        Me.Controls.Add(Me.cnmdPulisceFiltro)
        Me.Controls.Add(Me.cmdFiltra)
        Me.Controls.Add(Me.txtFiltro)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblStatistiche)
        Me.Controls.Add(Me.cmdDeSelezionaTutti)
        Me.Controls.Add(Me.cmdSelezionaTutti)
        Me.Controls.Add(Me.cmdRipristinaTutti)
        Me.Controls.Add(Me.cmdBloccaTutti)
        Me.Controls.Add(Me.lstListaDownload)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmListaDownload"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Lista Download"
        CType(Me.picImmagine, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstListaDownload As System.Windows.Forms.ListBox
    Friend WithEvents cmdBloccaTutti As System.Windows.Forms.Button
    Friend WithEvents cmdRipristinaTutti As System.Windows.Forms.Button
    Friend WithEvents cmdSelezionaTutti As System.Windows.Forms.Button
    Friend WithEvents cmdDeSelezionaTutti As System.Windows.Forms.Button
    Friend WithEvents lblStatistiche As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFiltro As System.Windows.Forms.TextBox
    Friend WithEvents cmdFiltra As System.Windows.Forms.Button
    Friend WithEvents cnmdPulisceFiltro As System.Windows.Forms.Button
    Friend WithEvents cmdEliminaSelezionati As System.Windows.Forms.Button
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents optLinks As System.Windows.Forms.RadioButton
    Friend WithEvents optImmagini As System.Windows.Forms.RadioButton
    Friend WithEvents picImmagine As System.Windows.Forms.PictureBox
End Class
