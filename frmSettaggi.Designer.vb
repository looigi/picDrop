<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettaggi
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
        Me.optNormale = New System.Windows.Forms.RadioButton()
        Me.optProxy = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmdSalva = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDominio = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtUtenza = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cmdPulisce = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lstCartelle = New System.Windows.Forms.ListBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNome = New System.Windows.Forms.TextBox()
        Me.txtDirectory = New System.Windows.Forms.TextBox()
        Me.cmdAggiunge = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmdSceglie = New System.Windows.Forms.Button()
        Me.chkLog = New System.Windows.Forms.CheckBox()
        Me.chkSovrascrivi = New System.Windows.Forms.CheckBox()
        Me.chkScartaPiccole = New System.Windows.Forms.CheckBox()
        Me.chkApre = New System.Windows.Forms.CheckBox()
        Me.chkPathSito = New System.Windows.Forms.CheckBox()
        Me.chkGiaScaricata = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'optNormale
        '
        Me.optNormale.AutoSize = True
        Me.optNormale.Location = New System.Drawing.Point(13, 13)
        Me.optNormale.Name = "optNormale"
        Me.optNormale.Size = New System.Drawing.Size(64, 17)
        Me.optNormale.TabIndex = 0
        Me.optNormale.TabStop = True
        Me.optNormale.Text = "Normale"
        Me.optNormale.UseVisualStyleBackColor = True
        '
        'optProxy
        '
        Me.optProxy.AutoSize = True
        Me.optProxy.Location = New System.Drawing.Point(185, 12)
        Me.optProxy.Name = "optProxy"
        Me.optProxy.Size = New System.Drawing.Size(51, 17)
        Me.optProxy.TabIndex = 1
        Me.optProxy.TabStop = True
        Me.optProxy.Text = "Proxy"
        Me.optProxy.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.cmdSalva)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtDominio)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtPassword)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtUtenza)
        Me.Panel1.Location = New System.Drawing.Point(185, 36)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(284, 113)
        Me.Panel1.TabIndex = 2
        '
        'cmdSalva
        '
        Me.cmdSalva.Location = New System.Drawing.Point(203, 83)
        Me.cmdSalva.Name = "cmdSalva"
        Me.cmdSalva.Size = New System.Drawing.Size(75, 23)
        Me.cmdSalva.TabIndex = 6
        Me.cmdSalva.Text = "Salva"
        Me.cmdSalva.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 63)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Dominio"
        '
        'txtDominio
        '
        Me.txtDominio.Location = New System.Drawing.Point(63, 56)
        Me.txtDominio.Name = "txtDominio"
        Me.txtDominio.Size = New System.Drawing.Size(220, 20)
        Me.txtDominio.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Password"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(63, 30)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(216, 20)
        Me.txtPassword.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Utenza"
        '
        'txtUtenza
        '
        Me.txtUtenza.Location = New System.Drawing.Point(63, 4)
        Me.txtUtenza.Name = "txtUtenza"
        Me.txtUtenza.Size = New System.Drawing.Size(216, 20)
        Me.txtUtenza.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.cmdPulisce)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.lstCartelle)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.txtNome)
        Me.Panel2.Controls.Add(Me.txtDirectory)
        Me.Panel2.Controls.Add(Me.cmdAggiunge)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.cmdSceglie)
        Me.Panel2.Location = New System.Drawing.Point(13, 182)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(456, 175)
        Me.Panel2.TabIndex = 3
        '
        'cmdPulisce
        '
        Me.cmdPulisce.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPulisce.Location = New System.Drawing.Point(397, 145)
        Me.cmdPulisce.Name = "cmdPulisce"
        Me.cmdPulisce.Size = New System.Drawing.Size(25, 23)
        Me.cmdPulisce.TabIndex = 9
        Me.cmdPulisce.Text = "*"
        Me.cmdPulisce.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(7, 150)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 13)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Nome"
        '
        'lstCartelle
        '
        Me.lstCartelle.FormattingEnabled = True
        Me.lstCartelle.Location = New System.Drawing.Point(7, 21)
        Me.lstCartelle.Name = "lstCartelle"
        Me.lstCartelle.Size = New System.Drawing.Size(443, 95)
        Me.lstCartelle.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(4, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(139, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Cartelle di destinazione"
        '
        'txtNome
        '
        Me.txtNome.Location = New System.Drawing.Point(62, 145)
        Me.txtNome.Name = "txtNome"
        Me.txtNome.Size = New System.Drawing.Size(126, 20)
        Me.txtNome.TabIndex = 7
        '
        'txtDirectory
        '
        Me.txtDirectory.Enabled = False
        Me.txtDirectory.Location = New System.Drawing.Point(62, 122)
        Me.txtDirectory.Name = "txtDirectory"
        Me.txtDirectory.Size = New System.Drawing.Size(360, 20)
        Me.txtDirectory.TabIndex = 3
        '
        'cmdAggiunge
        '
        Me.cmdAggiunge.Location = New System.Drawing.Point(428, 145)
        Me.cmdAggiunge.Name = "cmdAggiunge"
        Me.cmdAggiunge.Size = New System.Drawing.Size(25, 23)
        Me.cmdAggiunge.TabIndex = 6
        Me.cmdAggiunge.Text = "->"
        Me.cmdAggiunge.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 125)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Directory"
        '
        'cmdSceglie
        '
        Me.cmdSceglie.Location = New System.Drawing.Point(428, 120)
        Me.cmdSceglie.Name = "cmdSceglie"
        Me.cmdSceglie.Size = New System.Drawing.Size(25, 23)
        Me.cmdSceglie.TabIndex = 5
        Me.cmdSceglie.Text = "..."
        Me.cmdSceglie.UseVisualStyleBackColor = True
        '
        'chkLog
        '
        Me.chkLog.AutoSize = True
        Me.chkLog.Location = New System.Drawing.Point(13, 159)
        Me.chkLog.Name = "chkLog"
        Me.chkLog.Size = New System.Drawing.Size(44, 17)
        Me.chkLog.TabIndex = 4
        Me.chkLog.Text = "Log"
        Me.chkLog.UseVisualStyleBackColor = True
        '
        'chkSovrascrivi
        '
        Me.chkSovrascrivi.AutoSize = True
        Me.chkSovrascrivi.Location = New System.Drawing.Point(63, 159)
        Me.chkSovrascrivi.Name = "chkSovrascrivi"
        Me.chkSovrascrivi.Size = New System.Drawing.Size(94, 17)
        Me.chkSovrascrivi.TabIndex = 5
        Me.chkSovrascrivi.Text = "Sovrascrivi file"
        Me.chkSovrascrivi.UseVisualStyleBackColor = True
        '
        'chkScartaPiccole
        '
        Me.chkScartaPiccole.AutoSize = True
        Me.chkScartaPiccole.Location = New System.Drawing.Point(163, 159)
        Me.chkScartaPiccole.Name = "chkScartaPiccole"
        Me.chkScartaPiccole.Size = New System.Drawing.Size(94, 17)
        Me.chkScartaPiccole.TabIndex = 6
        Me.chkScartaPiccole.Text = "Scarta piccole"
        Me.chkScartaPiccole.UseVisualStyleBackColor = True
        '
        'chkApre
        '
        Me.chkApre.AutoSize = True
        Me.chkApre.Location = New System.Drawing.Point(263, 159)
        Me.chkApre.Name = "chkApre"
        Me.chkApre.Size = New System.Drawing.Size(85, 17)
        Me.chkApre.TabIndex = 7
        Me.chkApre.Text = "Apre finestra"
        Me.chkApre.UseVisualStyleBackColor = True
        '
        'chkPathSito
        '
        Me.chkPathSito.AutoSize = True
        Me.chkPathSito.Location = New System.Drawing.Point(355, 159)
        Me.chkPathSito.Name = "chkPathSito"
        Me.chkPathSito.Size = New System.Drawing.Size(114, 17)
        Me.chkPathSito.TabIndex = 8
        Me.chkPathSito.Text = "Nome sito nel path"
        Me.chkPathSito.UseVisualStyleBackColor = True
        '
        'chkGiaScaricata
        '
        Me.chkGiaScaricata.AutoSize = True
        Me.chkGiaScaricata.Location = New System.Drawing.Point(13, 132)
        Me.chkGiaScaricata.Name = "chkGiaScaricata"
        Me.chkGiaScaricata.Size = New System.Drawing.Size(139, 17)
        Me.chkGiaScaricata.TabIndex = 9
        Me.chkGiaScaricata.Text = "Scarica se già scaricata"
        Me.chkGiaScaricata.UseVisualStyleBackColor = True
        '
        'frmSettaggi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(480, 363)
        Me.Controls.Add(Me.chkGiaScaricata)
        Me.Controls.Add(Me.chkPathSito)
        Me.Controls.Add(Me.chkApre)
        Me.Controls.Add(Me.chkScartaPiccole)
        Me.Controls.Add(Me.chkSovrascrivi)
        Me.Controls.Add(Me.chkLog)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.optProxy)
        Me.Controls.Add(Me.optNormale)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettaggi"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Settaggi"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents optNormale As System.Windows.Forms.RadioButton
    Friend WithEvents optProxy As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDominio As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtUtenza As System.Windows.Forms.TextBox
    Friend WithEvents cmdSalva As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lstCartelle As System.Windows.Forms.ListBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmdSceglie As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtDirectory As System.Windows.Forms.TextBox
    Friend WithEvents cmdAggiunge As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtNome As System.Windows.Forms.TextBox
    Friend WithEvents chkLog As System.Windows.Forms.CheckBox
    Friend WithEvents chkSovrascrivi As System.Windows.Forms.CheckBox
    Friend WithEvents chkScartaPiccole As System.Windows.Forms.CheckBox
    Friend WithEvents chkApre As System.Windows.Forms.CheckBox
    Friend WithEvents cmdPulisce As System.Windows.Forms.Button
    Friend WithEvents chkPathSito As System.Windows.Forms.CheckBox
    Friend WithEvents chkGiaScaricata As System.Windows.Forms.CheckBox
End Class
