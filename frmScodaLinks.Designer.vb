<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScodaLinks
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmScodaLinks))
        Me.picScoda = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblOperazione = New System.Windows.Forms.Label()
        Me.lblNomeImm = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cmdRuota = New System.Windows.Forms.Button()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.cmdAvanti = New System.Windows.Forms.Button()
        Me.cmdIndietro = New System.Windows.Forms.Button()
        Me.cmdSposta = New System.Windows.Forms.Button()
        Me.cmdElimina = New System.Windows.Forms.Button()
        Me.lblDirSalvataggio = New System.Windows.Forms.Label()
        Me.cmbDest = New System.Windows.Forms.ComboBox()
        Me.cmdUscita = New System.Windows.Forms.Button()
        Me.pnlTools = New System.Windows.Forms.Panel()
        Me.cmdRitaglia = New System.Windows.Forms.Button()
        Me.cmdInverteVert = New System.Windows.Forms.Button()
        Me.cmdInverteOrizz = New System.Windows.Forms.Button()
        Me.cmdBN = New System.Windows.Forms.Button()
        Me.cmdMeno90 = New System.Windows.Forms.Button()
        Me.cmdPiu90 = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.picScoda, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlTools.SuspendLayout()
        Me.SuspendLayout()
        '
        'picScoda
        '
        Me.picScoda.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.picScoda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picScoda.Location = New System.Drawing.Point(12, 49)
        Me.picScoda.Name = "picScoda"
        Me.picScoda.Size = New System.Drawing.Size(432, 543)
        Me.picScoda.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picScoda.TabIndex = 0
        Me.picScoda.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Panel1.Controls.Add(Me.lblOperazione)
        Me.Panel1.Controls.Add(Me.lblNomeImm)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(432, 34)
        Me.Panel1.TabIndex = 11
        '
        'lblOperazione
        '
        Me.lblOperazione.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblOperazione.AutoSize = True
        Me.lblOperazione.BackColor = System.Drawing.Color.Transparent
        Me.lblOperazione.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOperazione.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblOperazione.Location = New System.Drawing.Point(3, 17)
        Me.lblOperazione.Name = "lblOperazione"
        Me.lblOperazione.Size = New System.Drawing.Size(80, 13)
        Me.lblOperazione.TabIndex = 11
        Me.lblOperazione.Text = "Destinazione"
        '
        'lblNomeImm
        '
        Me.lblNomeImm.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNomeImm.AutoSize = True
        Me.lblNomeImm.BackColor = System.Drawing.Color.Transparent
        Me.lblNomeImm.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNomeImm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNomeImm.Location = New System.Drawing.Point(3, 0)
        Me.lblNomeImm.Name = "lblNomeImm"
        Me.lblNomeImm.Size = New System.Drawing.Size(80, 13)
        Me.lblNomeImm.TabIndex = 10
        Me.lblNomeImm.Text = "Destinazione"
        '
        'Panel2
        '
        Me.Panel2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Panel2.Controls.Add(Me.cmdRuota)
        Me.Panel2.Controls.Add(Me.cmdUpdate)
        Me.Panel2.Controls.Add(Me.cmdAvanti)
        Me.Panel2.Controls.Add(Me.cmdIndietro)
        Me.Panel2.Controls.Add(Me.cmdSposta)
        Me.Panel2.Controls.Add(Me.cmdElimina)
        Me.Panel2.Controls.Add(Me.lblDirSalvataggio)
        Me.Panel2.Controls.Add(Me.cmbDest)
        Me.Panel2.Controls.Add(Me.cmdUscita)
        Me.Panel2.Location = New System.Drawing.Point(12, 598)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(429, 55)
        Me.Panel2.TabIndex = 12
        '
        'cmdRuota
        '
        Me.cmdRuota.Location = New System.Drawing.Point(293, 27)
        Me.cmdRuota.Name = "cmdRuota"
        Me.cmdRuota.Size = New System.Drawing.Size(52, 23)
        Me.cmdRuota.TabIndex = 12
        Me.cmdRuota.Text = ">>"
        Me.cmdRuota.UseVisualStyleBackColor = True
        '
        'cmdUpdate
        '
        Me.cmdUpdate.Location = New System.Drawing.Point(293, 0)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(133, 23)
        Me.cmdUpdate.TabIndex = 18
        Me.cmdUpdate.Text = "Update"
        Me.cmdUpdate.UseVisualStyleBackColor = True
        '
        'cmdAvanti
        '
        Me.cmdAvanti.Location = New System.Drawing.Point(44, 27)
        Me.cmdAvanti.Name = "cmdAvanti"
        Me.cmdAvanti.Size = New System.Drawing.Size(35, 23)
        Me.cmdAvanti.TabIndex = 17
        Me.cmdAvanti.Text = ">>"
        Me.cmdAvanti.UseVisualStyleBackColor = True
        '
        'cmdIndietro
        '
        Me.cmdIndietro.Location = New System.Drawing.Point(3, 27)
        Me.cmdIndietro.Name = "cmdIndietro"
        Me.cmdIndietro.Size = New System.Drawing.Size(35, 23)
        Me.cmdIndietro.TabIndex = 16
        Me.cmdIndietro.Text = "<<"
        Me.cmdIndietro.UseVisualStyleBackColor = True
        '
        'cmdSposta
        '
        Me.cmdSposta.Location = New System.Drawing.Point(122, -2)
        Me.cmdSposta.Name = "cmdSposta"
        Me.cmdSposta.Size = New System.Drawing.Size(56, 23)
        Me.cmdSposta.TabIndex = 14
        Me.cmdSposta.Text = "Sposta"
        Me.cmdSposta.UseVisualStyleBackColor = True
        Me.cmdSposta.Visible = False
        '
        'cmdElimina
        '
        Me.cmdElimina.Location = New System.Drawing.Point(85, 27)
        Me.cmdElimina.Name = "cmdElimina"
        Me.cmdElimina.Size = New System.Drawing.Size(56, 23)
        Me.cmdElimina.TabIndex = 15
        Me.cmdElimina.Text = "Elimina"
        Me.cmdElimina.UseVisualStyleBackColor = True
        '
        'lblDirSalvataggio
        '
        Me.lblDirSalvataggio.BackColor = System.Drawing.Color.Transparent
        Me.lblDirSalvataggio.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDirSalvataggio.Location = New System.Drawing.Point(3, 0)
        Me.lblDirSalvataggio.Name = "lblDirSalvataggio"
        Me.lblDirSalvataggio.Size = New System.Drawing.Size(109, 17)
        Me.lblDirSalvataggio.TabIndex = 13
        Me.lblDirSalvataggio.Text = "Destinazione"
        Me.lblDirSalvataggio.Visible = False
        '
        'cmbDest
        '
        Me.cmbDest.FormattingEnabled = True
        Me.cmbDest.Location = New System.Drawing.Point(118, 0)
        Me.cmbDest.Name = "cmbDest"
        Me.cmbDest.Size = New System.Drawing.Size(163, 21)
        Me.cmbDest.TabIndex = 12
        Me.cmbDest.Visible = False
        '
        'cmdUscita
        '
        Me.cmdUscita.Location = New System.Drawing.Point(351, 27)
        Me.cmdUscita.Name = "cmdUscita"
        Me.cmdUscita.Size = New System.Drawing.Size(75, 23)
        Me.cmdUscita.TabIndex = 11
        Me.cmdUscita.Text = "Uscita"
        Me.cmdUscita.UseVisualStyleBackColor = True
        '
        'pnlTools
        '
        Me.pnlTools.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlTools.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.pnlTools.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTools.Controls.Add(Me.cmdRitaglia)
        Me.pnlTools.Controls.Add(Me.cmdInverteVert)
        Me.pnlTools.Controls.Add(Me.cmdInverteOrizz)
        Me.pnlTools.Controls.Add(Me.cmdBN)
        Me.pnlTools.Controls.Add(Me.cmdMeno90)
        Me.pnlTools.Controls.Add(Me.cmdPiu90)
        Me.pnlTools.Location = New System.Drawing.Point(12, 559)
        Me.pnlTools.Name = "pnlTools"
        Me.pnlTools.Size = New System.Drawing.Size(432, 33)
        Me.pnlTools.TabIndex = 13
        Me.pnlTools.Visible = False
        '
        'cmdRitaglia
        '
        Me.cmdRitaglia.Location = New System.Drawing.Point(315, 5)
        Me.cmdRitaglia.Name = "cmdRitaglia"
        Me.cmdRitaglia.Size = New System.Drawing.Size(52, 23)
        Me.cmdRitaglia.TabIndex = 18
        Me.cmdRitaglia.Text = "Ritaglia"
        Me.cmdRitaglia.UseVisualStyleBackColor = True
        Me.cmdRitaglia.Visible = False
        '
        'cmdInverteVert
        '
        Me.cmdInverteVert.Location = New System.Drawing.Point(179, 5)
        Me.cmdInverteVert.Name = "cmdInverteVert"
        Me.cmdInverteVert.Size = New System.Drawing.Size(52, 23)
        Me.cmdInverteVert.TabIndex = 17
        Me.cmdInverteVert.Text = "/\ \/"
        Me.cmdInverteVert.UseVisualStyleBackColor = True
        '
        'cmdInverteOrizz
        '
        Me.cmdInverteOrizz.Location = New System.Drawing.Point(121, 5)
        Me.cmdInverteOrizz.Name = "cmdInverteOrizz"
        Me.cmdInverteOrizz.Size = New System.Drawing.Size(52, 23)
        Me.cmdInverteOrizz.TabIndex = 16
        Me.cmdInverteOrizz.Text = "<>"
        Me.cmdInverteOrizz.UseVisualStyleBackColor = True
        '
        'cmdBN
        '
        Me.cmdBN.Location = New System.Drawing.Point(373, 5)
        Me.cmdBN.Name = "cmdBN"
        Me.cmdBN.Size = New System.Drawing.Size(52, 23)
        Me.cmdBN.TabIndex = 15
        Me.cmdBN.Text = "B/N"
        Me.cmdBN.UseVisualStyleBackColor = True
        '
        'cmdMeno90
        '
        Me.cmdMeno90.Location = New System.Drawing.Point(5, 5)
        Me.cmdMeno90.Name = "cmdMeno90"
        Me.cmdMeno90.Size = New System.Drawing.Size(52, 23)
        Me.cmdMeno90.TabIndex = 14
        Me.cmdMeno90.Text = "-90°"
        Me.cmdMeno90.UseVisualStyleBackColor = True
        '
        'cmdPiu90
        '
        Me.cmdPiu90.Location = New System.Drawing.Point(63, 5)
        Me.cmdPiu90.Name = "cmdPiu90"
        Me.cmdPiu90.Size = New System.Drawing.Size(52, 23)
        Me.cmdPiu90.TabIndex = 13
        Me.cmdPiu90.Text = "+90°"
        Me.cmdPiu90.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 300
        '
        'frmScodaLinks
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(456, 654)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlTools)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.picScoda)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmScodaLinks"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Scoda Links"
        CType(Me.picScoda, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.pnlTools.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents picScoda As System.Windows.Forms.PictureBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblOperazione As System.Windows.Forms.Label
    Friend WithEvents lblNomeImm As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cmdUpdate As System.Windows.Forms.Button
    Friend WithEvents cmdAvanti As System.Windows.Forms.Button
    Friend WithEvents cmdIndietro As System.Windows.Forms.Button
    Friend WithEvents cmdElimina As System.Windows.Forms.Button
    Friend WithEvents cmdSposta As System.Windows.Forms.Button
    Friend WithEvents lblDirSalvataggio As System.Windows.Forms.Label
    Friend WithEvents cmbDest As System.Windows.Forms.ComboBox
    Friend WithEvents cmdUscita As System.Windows.Forms.Button
    Friend WithEvents cmdRuota As System.Windows.Forms.Button
    Friend WithEvents pnlTools As System.Windows.Forms.Panel
    Friend WithEvents cmdPiu90 As System.Windows.Forms.Button
    Friend WithEvents cmdMeno90 As System.Windows.Forms.Button
    Friend WithEvents cmdBN As System.Windows.Forms.Button
    Friend WithEvents cmdInverteVert As System.Windows.Forms.Button
    Friend WithEvents cmdInverteOrizz As System.Windows.Forms.Button
    Friend WithEvents cmdRitaglia As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
