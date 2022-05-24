<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StoreTrueRNGOutputIntoFiles
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.label_bytesPerSecond = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.label_totalBytesWritten = New System.Windows.Forms.Label()
        Me.label_cachePercent = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.label_filesWritten = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ButtonStop = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 96)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Bytes per second:"
        '
        'label_bytesPerSecond
        '
        Me.label_bytesPerSecond.AutoSize = True
        Me.label_bytesPerSecond.Location = New System.Drawing.Point(121, 96)
        Me.label_bytesPerSecond.Name = "label_bytesPerSecond"
        Me.label_bytesPerSecond.Size = New System.Drawing.Size(16, 13)
        Me.label_bytesPerSecond.TabIndex = 1
        Me.label_bytesPerSecond.Text = "..."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 118)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Total bytes written:"
        '
        'label_totalBytesWritten
        '
        Me.label_totalBytesWritten.AutoSize = True
        Me.label_totalBytesWritten.Location = New System.Drawing.Point(121, 118)
        Me.label_totalBytesWritten.Name = "label_totalBytesWritten"
        Me.label_totalBytesWritten.Size = New System.Drawing.Size(16, 13)
        Me.label_totalBytesWritten.TabIndex = 3
        Me.label_totalBytesWritten.Text = "..."
        '
        'label_cachePercent
        '
        Me.label_cachePercent.AutoSize = True
        Me.label_cachePercent.Location = New System.Drawing.Point(121, 73)
        Me.label_cachePercent.Name = "label_cachePercent"
        Me.label_cachePercent.Size = New System.Drawing.Size(16, 13)
        Me.label_cachePercent.TabIndex = 5
        Me.label_cachePercent.Text = "..."
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 73)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Cache:"
        '
        'label_filesWritten
        '
        Me.label_filesWritten.AutoSize = True
        Me.label_filesWritten.Location = New System.Drawing.Point(121, 139)
        Me.label_filesWritten.Name = "label_filesWritten"
        Me.label_filesWritten.Size = New System.Drawing.Size(16, 13)
        Me.label_filesWritten.TabIndex = 7
        Me.label_filesWritten.Text = "..."
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 139)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Files written:"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(12, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(240, 32)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "This program generates a sequence of files that store the output of the TrueRNG U" &
    "SB dongle."
        '
        'ButtonStop
        '
        Me.ButtonStop.Location = New System.Drawing.Point(161, 169)
        Me.ButtonStop.Name = "ButtonStop"
        Me.ButtonStop.Size = New System.Drawing.Size(80, 27)
        Me.ButtonStop.TabIndex = 9
        Me.ButtonStop.Text = "Stop"
        Me.ButtonStop.UseVisualStyleBackColor = True
        '
        'StoreTrueRNDOutputIntoFiles
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(253, 208)
        Me.Controls.Add(Me.ButtonStop)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.label_filesWritten)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.label_cachePercent)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.label_totalBytesWritten)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.label_bytesPerSecond)
        Me.Controls.Add(Me.Label1)
        Me.Name = "StoreTrueRNDOutputIntoFiles"
        Me.Text = "TrueRNG Saver"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents label_bytesPerSecond As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents label_totalBytesWritten As Label
    Friend WithEvents label_cachePercent As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents label_filesWritten As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents ButtonStop As Button
End Class
