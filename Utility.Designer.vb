<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Utility
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
        Me.cmdUscita = New System.Windows.Forms.Button()
        Me.cmdRiprovaSitiA0 = New System.Windows.Forms.Button()
        Me.cmdBonifica = New System.Windows.Forms.Button()
        Me.lblAvanzamento = New System.Windows.Forms.Label()
        Me.cmdResetPosizione = New System.Windows.Forms.Button()
        Me.cmdCompattaDB = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cmdUscita
        '
        Me.cmdUscita.Location = New System.Drawing.Point(197, 113)
        Me.cmdUscita.Name = "cmdUscita"
        Me.cmdUscita.Size = New System.Drawing.Size(75, 23)
        Me.cmdUscita.TabIndex = 12
        Me.cmdUscita.Text = "Uscita"
        Me.cmdUscita.UseVisualStyleBackColor = True
        '
        'cmdRiprovaSitiA0
        '
        Me.cmdRiprovaSitiA0.Location = New System.Drawing.Point(12, 36)
        Me.cmdRiprovaSitiA0.Name = "cmdRiprovaSitiA0"
        Me.cmdRiprovaSitiA0.Size = New System.Drawing.Size(260, 23)
        Me.cmdRiprovaSitiA0.TabIndex = 13
        Me.cmdRiprovaSitiA0.Text = "Rimette in coda i siti che hanno 0 Download"
        Me.cmdRiprovaSitiA0.UseVisualStyleBackColor = True
        '
        'cmdBonifica
        '
        Me.cmdBonifica.Location = New System.Drawing.Point(12, 12)
        Me.cmdBonifica.Name = "cmdBonifica"
        Me.cmdBonifica.Size = New System.Drawing.Size(260, 23)
        Me.cmdBonifica.TabIndex = 14
        Me.cmdBonifica.Text = "Bonifica i dati sul db"
        Me.cmdBonifica.UseVisualStyleBackColor = True
        '
        'lblAvanzamento
        '
        Me.lblAvanzamento.Location = New System.Drawing.Point(12, 62)
        Me.lblAvanzamento.Name = "lblAvanzamento"
        Me.lblAvanzamento.Size = New System.Drawing.Size(260, 21)
        Me.lblAvanzamento.TabIndex = 15
        Me.lblAvanzamento.Text = "Label1"
        '
        'cmdResetPosizione
        '
        Me.cmdResetPosizione.Location = New System.Drawing.Point(12, 60)
        Me.cmdResetPosizione.Name = "cmdResetPosizione"
        Me.cmdResetPosizione.Size = New System.Drawing.Size(260, 23)
        Me.cmdResetPosizione.TabIndex = 16
        Me.cmdResetPosizione.Text = "Resetta posizione maschera"
        Me.cmdResetPosizione.UseVisualStyleBackColor = True
        '
        'cmdCompattaDB
        '
        Me.cmdCompattaDB.Location = New System.Drawing.Point(12, 84)
        Me.cmdCompattaDB.Name = "cmdCompattaDB"
        Me.cmdCompattaDB.Size = New System.Drawing.Size(260, 23)
        Me.cmdCompattaDB.TabIndex = 17
        Me.cmdCompattaDB.Text = "Compatta DB"
        Me.cmdCompattaDB.UseVisualStyleBackColor = True
        '
        'Utility
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 148)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdCompattaDB)
        Me.Controls.Add(Me.cmdResetPosizione)
        Me.Controls.Add(Me.lblAvanzamento)
        Me.Controls.Add(Me.cmdBonifica)
        Me.Controls.Add(Me.cmdRiprovaSitiA0)
        Me.Controls.Add(Me.cmdUscita)
        Me.Name = "Utility"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Utility"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdUscita As System.Windows.Forms.Button
    Friend WithEvents cmdRiprovaSitiA0 As System.Windows.Forms.Button
    Friend WithEvents cmdBonifica As System.Windows.Forms.Button
    Friend WithEvents lblAvanzamento As System.Windows.Forms.Label
    Friend WithEvents cmdResetPosizione As System.Windows.Forms.Button
    Friend WithEvents cmdCompattaDB As System.Windows.Forms.Button
End Class
