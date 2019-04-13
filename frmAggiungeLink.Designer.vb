<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAggiungeLink
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAggiungeLink))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtIndirizzo = New System.Windows.Forms.TextBox()
        Me.cmdSceglieFile = New System.Windows.Forms.Button()
        Me.lblTitolo = New System.Windows.Forms.Label()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.cmdAnnulla = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.chkForza = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Indirizzo da scaricare"
        '
        'txtIndirizzo
        '
        Me.txtIndirizzo.Location = New System.Drawing.Point(15, 50)
        Me.txtIndirizzo.Name = "txtIndirizzo"
        Me.txtIndirizzo.Size = New System.Drawing.Size(356, 20)
        Me.txtIndirizzo.TabIndex = 1
        '
        'cmdSceglieFile
        '
        Me.cmdSceglieFile.Location = New System.Drawing.Point(378, 50)
        Me.cmdSceglieFile.Name = "cmdSceglieFile"
        Me.cmdSceglieFile.Size = New System.Drawing.Size(34, 20)
        Me.cmdSceglieFile.TabIndex = 2
        Me.cmdSceglieFile.Text = "..."
        Me.cmdSceglieFile.UseVisualStyleBackColor = True
        '
        'lblTitolo
        '
        Me.lblTitolo.AutoSize = True
        Me.lblTitolo.Location = New System.Drawing.Point(12, 9)
        Me.lblTitolo.Name = "lblTitolo"
        Me.lblTitolo.Size = New System.Drawing.Size(106, 13)
        Me.lblTitolo.TabIndex = 3
        Me.lblTitolo.Text = "Indirizzo da scaricare"
        '
        'cmdOk
        '
        Me.cmdOk.Location = New System.Drawing.Point(256, 76)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(75, 23)
        Me.cmdOk.TabIndex = 4
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.UseVisualStyleBackColor = True
        '
        'cmdAnnulla
        '
        Me.cmdAnnulla.Location = New System.Drawing.Point(337, 76)
        Me.cmdAnnulla.Name = "cmdAnnulla"
        Me.cmdAnnulla.Size = New System.Drawing.Size(75, 23)
        Me.cmdAnnulla.TabIndex = 5
        Me.cmdAnnulla.Text = "Annulla"
        Me.cmdAnnulla.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'chkForza
        '
        Me.chkForza.AutoSize = True
        Me.chkForza.Location = New System.Drawing.Point(15, 76)
        Me.chkForza.Name = "chkForza"
        Me.chkForza.Size = New System.Drawing.Size(209, 17)
        Me.chkForza.TabIndex = 6
        Me.chkForza.Text = "Forza download anche se già presente"
        Me.chkForza.UseVisualStyleBackColor = True
        '
        'frmAggiungeLink
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(425, 114)
        Me.ControlBox = False
        Me.Controls.Add(Me.chkForza)
        Me.Controls.Add(Me.cmdAnnulla)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.lblTitolo)
        Me.Controls.Add(Me.cmdSceglieFile)
        Me.Controls.Add(Me.txtIndirizzo)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmAggiungeLink"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Aggiunge link da scaricare"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtIndirizzo As System.Windows.Forms.TextBox
    Friend WithEvents cmdSceglieFile As System.Windows.Forms.Button
    Friend WithEvents lblTitolo As System.Windows.Forms.Label
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents cmdAnnulla As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents chkForza As System.Windows.Forms.CheckBox
End Class
