<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInfo
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
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.lblDettaglio = New System.Windows.Forms.Label()
        Me.lblScaricate = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblInfo
        '
        Me.lblInfo.AutoEllipsis = True
        Me.lblInfo.AutoSize = True
        Me.lblInfo.Location = New System.Drawing.Point(3, 9)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(39, 13)
        Me.lblInfo.TabIndex = 0
        Me.lblInfo.Text = "Label1"
        '
        'lblDettaglio
        '
        Me.lblDettaglio.AutoEllipsis = True
        Me.lblDettaglio.AutoSize = True
        Me.lblDettaglio.Location = New System.Drawing.Point(3, 31)
        Me.lblDettaglio.Name = "lblDettaglio"
        Me.lblDettaglio.Size = New System.Drawing.Size(39, 13)
        Me.lblDettaglio.TabIndex = 1
        Me.lblDettaglio.Text = "Label1"
        '
        'lblScaricate
        '
        Me.lblScaricate.AutoEllipsis = True
        Me.lblScaricate.AutoSize = True
        Me.lblScaricate.Location = New System.Drawing.Point(3, 54)
        Me.lblScaricate.Name = "lblScaricate"
        Me.lblScaricate.Size = New System.Drawing.Size(39, 13)
        Me.lblScaricate.TabIndex = 2
        Me.lblScaricate.Text = "Label1"
        '
        'frmInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(354, 148)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblScaricate)
        Me.Controls.Add(Me.lblDettaglio)
        Me.Controls.Add(Me.lblInfo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmInfo"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblInfo As Label
    Friend WithEvents lblDettaglio As Label
    Friend WithEvents lblScaricate As Label
End Class
