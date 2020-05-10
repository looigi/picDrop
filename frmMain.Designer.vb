<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
		Me.tmrDownload = New System.Windows.Forms.Timer(Me.components)
		Me.lblDirSalvataggio = New System.Windows.Forms.Label()
		Me.lblDimensioni = New System.Windows.Forms.Label()
		Me.tmrControllo = New System.Windows.Forms.Timer(Me.components)
		Me.pbDownload = New System.Windows.Forms.ProgressBar()
		Me.tmrBackup = New System.Windows.Forms.Timer(Me.components)
		Me.SuspendLayout()
		'
		'tmrDownload
		'
		Me.tmrDownload.Interval = 1000
		'
		'lblDirSalvataggio
		'
		Me.lblDirSalvataggio.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblDirSalvataggio.BackColor = System.Drawing.Color.Transparent
		Me.lblDirSalvataggio.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblDirSalvataggio.Location = New System.Drawing.Point(1, 27)
		Me.lblDirSalvataggio.Name = "lblDirSalvataggio"
		Me.lblDirSalvataggio.Size = New System.Drawing.Size(109, 17)
		Me.lblDirSalvataggio.TabIndex = 0
		Me.lblDirSalvataggio.Text = "Label1"
		Me.lblDirSalvataggio.Visible = False
		'
		'lblDimensioni
		'
		Me.lblDimensioni.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblDimensioni.AutoSize = True
		Me.lblDimensioni.BackColor = System.Drawing.Color.DarkGreen
		Me.lblDimensioni.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.lblDimensioni.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblDimensioni.ForeColor = System.Drawing.Color.GreenYellow
		Me.lblDimensioni.Location = New System.Drawing.Point(1, 80)
		Me.lblDimensioni.Margin = New System.Windows.Forms.Padding(0)
		Me.lblDimensioni.Name = "lblDimensioni"
		Me.lblDimensioni.Size = New System.Drawing.Size(46, 15)
		Me.lblDimensioni.TabIndex = 2
		Me.lblDimensioni.Text = "Label1"
		Me.lblDimensioni.Visible = False
		'
		'tmrControllo
		'
		Me.tmrControllo.Enabled = True
		Me.tmrControllo.Interval = 1000
		'
		'pbDownload
		'
		Me.pbDownload.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.pbDownload.Location = New System.Drawing.Point(1, 54)
		Me.pbDownload.Name = "pbDownload"
		Me.pbDownload.Size = New System.Drawing.Size(109, 23)
		Me.pbDownload.Step = 1
		Me.pbDownload.Style = System.Windows.Forms.ProgressBarStyle.Continuous
		Me.pbDownload.TabIndex = 3
		Me.pbDownload.Value = 10
		'
		'tmrBackup
		'
		Me.tmrBackup.Enabled = True
		Me.tmrBackup.Interval = 1000
		'
		'frmMain
		'
		Me.AllowDrop = True
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.Wheat
		Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
		Me.ClientSize = New System.Drawing.Size(120, 98)
		Me.ControlBox = False
		Me.Controls.Add(Me.pbDownload)
		Me.Controls.Add(Me.lblDimensioni)
		Me.Controls.Add(Me.lblDirSalvataggio)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmMain"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents tmrDownload As System.Windows.Forms.Timer
    Friend WithEvents lblDirSalvataggio As System.Windows.Forms.Label
    Friend WithEvents lblDimensioni As System.Windows.Forms.Label
    Friend WithEvents tmrControllo As System.Windows.Forms.Timer
    Friend WithEvents pbDownload As System.Windows.Forms.ProgressBar
    Friend WithEvents tmrBackup As Timer
End Class
