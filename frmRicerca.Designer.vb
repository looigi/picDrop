<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRicerca
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRicerca))
		Me.Label1 = New System.Windows.Forms.Label()
		Me.txtFiltro = New System.Windows.Forms.TextBox()
		Me.cmdRicerca = New System.Windows.Forms.Button()
		Me.pnlContenuto = New System.Windows.Forms.Panel()
		Me.lblAvanzamento = New System.Windows.Forms.Label()
		Me.cmdSelTutti = New System.Windows.Forms.Button()
		Me.cmdDeSelTutti = New System.Windows.Forms.Button()
		Me.cmdScarica = New System.Windows.Forms.Button()
		Me.cmdStop = New System.Windows.Forms.Button()
		Me.txtPagine = New System.Windows.Forms.TextBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.cmdPiu = New System.Windows.Forms.Button()
		Me.cmdMeno = New System.Windows.Forms.Button()
		Me.lblPagina = New System.Windows.Forms.Label()
		Me.lblImmagini = New System.Windows.Forms.Label()
		Me.picAnteprima = New System.Windows.Forms.PictureBox()
		Me.picChiude = New System.Windows.Forms.Button()
		Me.cmdMenoInizio = New System.Windows.Forms.Button()
		Me.cmdPiuInizio = New System.Windows.Forms.Button()
		Me.txtInizio = New System.Windows.Forms.TextBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.cmdRicerche = New System.Windows.Forms.Button()
		Me.lstRicerche = New System.Windows.Forms.ListBox()
		CType(Me.picAnteprima, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'Label1
		'
		Me.Label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(13, 13)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(44, 13)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "Ricerca"
		'
		'txtFiltro
		'
		Me.txtFiltro.Location = New System.Drawing.Point(116, 9)
		Me.txtFiltro.MaxLength = 100
		Me.txtFiltro.Name = "txtFiltro"
		Me.txtFiltro.Size = New System.Drawing.Size(259, 20)
		Me.txtFiltro.TabIndex = 1
		'
		'cmdRicerca
		'
		Me.cmdRicerca.Location = New System.Drawing.Point(381, 8)
		Me.cmdRicerca.Name = "cmdRicerca"
		Me.cmdRicerca.Size = New System.Drawing.Size(42, 20)
		Me.cmdRicerca.TabIndex = 2
		Me.cmdRicerca.Text = "->"
		Me.cmdRicerca.UseVisualStyleBackColor = True
		'
		'pnlContenuto
		'
		Me.pnlContenuto.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.pnlContenuto.AutoScroll = True
		Me.pnlContenuto.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
		Me.pnlContenuto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.pnlContenuto.Location = New System.Drawing.Point(16, 57)
		Me.pnlContenuto.Name = "pnlContenuto"
		Me.pnlContenuto.Size = New System.Drawing.Size(908, 443)
		Me.pnlContenuto.TabIndex = 3
		'
		'lblAvanzamento
		'
		Me.lblAvanzamento.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
		Me.lblAvanzamento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.lblAvanzamento.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblAvanzamento.Location = New System.Drawing.Point(904, -5)
		Me.lblAvanzamento.Name = "lblAvanzamento"
		Me.lblAvanzamento.Size = New System.Drawing.Size(851, 80)
		Me.lblAvanzamento.TabIndex = 0
		Me.lblAvanzamento.Text = "Label2"
		Me.lblAvanzamento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.lblAvanzamento.Visible = False
		'
		'cmdSelTutti
		'
		Me.cmdSelTutti.Location = New System.Drawing.Point(532, 8)
		Me.cmdSelTutti.Name = "cmdSelTutti"
		Me.cmdSelTutti.Size = New System.Drawing.Size(118, 20)
		Me.cmdSelTutti.TabIndex = 4
		Me.cmdSelTutti.Text = "Seleziona tutti"
		Me.cmdSelTutti.UseVisualStyleBackColor = True
		Me.cmdSelTutti.Visible = False
		'
		'cmdDeSelTutti
		'
		Me.cmdDeSelTutti.Location = New System.Drawing.Point(656, 8)
		Me.cmdDeSelTutti.Name = "cmdDeSelTutti"
		Me.cmdDeSelTutti.Size = New System.Drawing.Size(118, 20)
		Me.cmdDeSelTutti.TabIndex = 5
		Me.cmdDeSelTutti.Text = "De-Seleziona tutti"
		Me.cmdDeSelTutti.UseVisualStyleBackColor = True
		Me.cmdDeSelTutti.Visible = False
		'
		'cmdScarica
		'
		Me.cmdScarica.Location = New System.Drawing.Point(806, 8)
		Me.cmdScarica.Name = "cmdScarica"
		Me.cmdScarica.Size = New System.Drawing.Size(118, 20)
		Me.cmdScarica.TabIndex = 6
		Me.cmdScarica.Text = "Scarica"
		Me.cmdScarica.UseVisualStyleBackColor = True
		Me.cmdScarica.Visible = False
		'
		'cmdStop
		'
		Me.cmdStop.Location = New System.Drawing.Point(429, 8)
		Me.cmdStop.Name = "cmdStop"
		Me.cmdStop.Size = New System.Drawing.Size(82, 20)
		Me.cmdStop.TabIndex = 7
		Me.cmdStop.Text = "Stop"
		Me.cmdStop.UseVisualStyleBackColor = True
		Me.cmdStop.Visible = False
		'
		'txtPagine
		'
		Me.txtPagine.Enabled = False
		Me.txtPagine.Location = New System.Drawing.Point(306, 32)
		Me.txtPagine.Name = "txtPagine"
		Me.txtPagine.Size = New System.Drawing.Size(33, 20)
		Me.txtPagine.TabIndex = 9
		Me.txtPagine.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		'
		'Label2
		'
		Me.Label2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(12, 36)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(66, 13)
		Me.Label2.TabIndex = 8
		Me.Label2.Text = "Pagina inizio"
		'
		'cmdPiu
		'
		Me.cmdPiu.Location = New System.Drawing.Point(345, 32)
		Me.cmdPiu.Name = "cmdPiu"
		Me.cmdPiu.Size = New System.Drawing.Size(29, 20)
		Me.cmdPiu.TabIndex = 10
		Me.cmdPiu.Text = "+"
		Me.cmdPiu.UseVisualStyleBackColor = True
		'
		'cmdMeno
		'
		Me.cmdMeno.Location = New System.Drawing.Point(275, 32)
		Me.cmdMeno.Name = "cmdMeno"
		Me.cmdMeno.Size = New System.Drawing.Size(25, 20)
		Me.cmdMeno.TabIndex = 11
		Me.cmdMeno.Text = "-"
		Me.cmdMeno.UseVisualStyleBackColor = True
		'
		'lblPagina
		'
		Me.lblPagina.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblPagina.AutoSize = True
		Me.lblPagina.Location = New System.Drawing.Point(374, 37)
		Me.lblPagina.Name = "lblPagina"
		Me.lblPagina.Size = New System.Drawing.Size(40, 13)
		Me.lblPagina.TabIndex = 12
		Me.lblPagina.Text = "Pagine"
		Me.lblPagina.Visible = False
		'
		'lblImmagini
		'
		Me.lblImmagini.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblImmagini.AutoSize = True
		Me.lblImmagini.Location = New System.Drawing.Point(564, 35)
		Me.lblImmagini.Name = "lblImmagini"
		Me.lblImmagini.Size = New System.Drawing.Size(40, 13)
		Me.lblImmagini.TabIndex = 13
		Me.lblImmagini.Text = "Pagine"
		Me.lblImmagini.Visible = False
		'
		'picAnteprima
		'
		Me.picAnteprima.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.picAnteprima.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.picAnteprima.Location = New System.Drawing.Point(838, 1)
		Me.picAnteprima.Name = "picAnteprima"
		Me.picAnteprima.Size = New System.Drawing.Size(100, 50)
		Me.picAnteprima.TabIndex = 14
		Me.picAnteprima.TabStop = False
		Me.picAnteprima.Visible = False
		'
		'picChiude
		'
		Me.picChiude.BackColor = System.Drawing.Color.Red
		Me.picChiude.Location = New System.Drawing.Point(715, 34)
		Me.picChiude.Name = "picChiude"
		Me.picChiude.Size = New System.Drawing.Size(29, 20)
		Me.picChiude.TabIndex = 15
		Me.picChiude.Text = "X"
		Me.picChiude.UseVisualStyleBackColor = False
		Me.picChiude.Visible = False
		'
		'cmdMenoInizio
		'
		Me.cmdMenoInizio.Location = New System.Drawing.Point(85, 32)
		Me.cmdMenoInizio.Name = "cmdMenoInizio"
		Me.cmdMenoInizio.Size = New System.Drawing.Size(25, 20)
		Me.cmdMenoInizio.TabIndex = 18
		Me.cmdMenoInizio.Text = "-"
		Me.cmdMenoInizio.UseVisualStyleBackColor = True
		'
		'cmdPiuInizio
		'
		Me.cmdPiuInizio.Location = New System.Drawing.Point(157, 32)
		Me.cmdPiuInizio.Name = "cmdPiuInizio"
		Me.cmdPiuInizio.Size = New System.Drawing.Size(29, 20)
		Me.cmdPiuInizio.TabIndex = 17
		Me.cmdPiuInizio.Text = "+"
		Me.cmdPiuInizio.UseVisualStyleBackColor = True
		'
		'txtInizio
		'
		Me.txtInizio.Enabled = False
		Me.txtInizio.Location = New System.Drawing.Point(116, 32)
		Me.txtInizio.Name = "txtInizio"
		Me.txtInizio.Size = New System.Drawing.Size(33, 20)
		Me.txtInizio.TabIndex = 16
		Me.txtInizio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		'
		'Label3
		'
		Me.Label3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(192, 36)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(77, 13)
		Me.Label3.TabIndex = 19
		Me.Label3.Text = "Quante pagine"
		'
		'cmdRicerche
		'
		Me.cmdRicerche.Location = New System.Drawing.Point(85, 9)
		Me.cmdRicerche.Name = "cmdRicerche"
		Me.cmdRicerche.Size = New System.Drawing.Size(25, 20)
		Me.cmdRicerche.TabIndex = 20
		Me.cmdRicerche.Text = "..."
		Me.cmdRicerche.UseVisualStyleBackColor = True
		'
		'lstRicerche
		'
		Me.lstRicerche.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lstRicerche.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
		Me.lstRicerche.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lstRicerche.FormattingEnabled = True
		Me.lstRicerche.HorizontalScrollbar = True
		Me.lstRicerche.ItemHeight = 16
		Me.lstRicerche.Location = New System.Drawing.Point(484, -30)
		Me.lstRicerche.Name = "lstRicerche"
		Me.lstRicerche.Size = New System.Drawing.Size(120, 84)
		Me.lstRicerche.TabIndex = 21
		Me.lstRicerche.Visible = False
		'
		'frmRicerca
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(936, 512)
		Me.Controls.Add(Me.lstRicerche)
		Me.Controls.Add(Me.cmdRicerche)
		Me.Controls.Add(Me.picChiude)
		Me.Controls.Add(Me.picAnteprima)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.cmdMenoInizio)
		Me.Controls.Add(Me.cmdPiuInizio)
		Me.Controls.Add(Me.txtInizio)
		Me.Controls.Add(Me.lblImmagini)
		Me.Controls.Add(Me.lblPagina)
		Me.Controls.Add(Me.cmdMeno)
		Me.Controls.Add(Me.cmdPiu)
		Me.Controls.Add(Me.txtPagine)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.lblAvanzamento)
		Me.Controls.Add(Me.cmdStop)
		Me.Controls.Add(Me.cmdScarica)
		Me.Controls.Add(Me.cmdDeSelTutti)
		Me.Controls.Add(Me.cmdSelTutti)
		Me.Controls.Add(Me.pnlContenuto)
		Me.Controls.Add(Me.cmdRicerca)
		Me.Controls.Add(Me.txtFiltro)
		Me.Controls.Add(Me.Label1)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "frmRicerca"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Ricerca"
		CType(Me.picAnteprima, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFiltro As System.Windows.Forms.TextBox
    Friend WithEvents cmdRicerca As System.Windows.Forms.Button
    Friend WithEvents pnlContenuto As System.Windows.Forms.Panel
    Friend WithEvents cmdSelTutti As System.Windows.Forms.Button
    Friend WithEvents cmdDeSelTutti As System.Windows.Forms.Button
    Friend WithEvents cmdScarica As System.Windows.Forms.Button
    Friend WithEvents cmdStop As System.Windows.Forms.Button
    Friend WithEvents lblAvanzamento As System.Windows.Forms.Label
    Friend WithEvents txtPagine As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdPiu As System.Windows.Forms.Button
    Friend WithEvents cmdMeno As System.Windows.Forms.Button
    Friend WithEvents lblPagina As System.Windows.Forms.Label
    Friend WithEvents lblImmagini As System.Windows.Forms.Label
    Friend WithEvents picAnteprima As System.Windows.Forms.PictureBox
    Friend WithEvents picChiude As System.Windows.Forms.Button
    Friend WithEvents cmdMenoInizio As System.Windows.Forms.Button
    Friend WithEvents cmdPiuInizio As System.Windows.Forms.Button
    Friend WithEvents txtInizio As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmdRicerche As System.Windows.Forms.Button
    Friend WithEvents lstRicerche As System.Windows.Forms.ListBox
End Class
