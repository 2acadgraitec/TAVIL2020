<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCUnion
    Inherits System.Windows.Forms.UserControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.LbLado = New System.Windows.Forms.ListBox()
        Me.LblLado = New System.Windows.Forms.Label()
        Me.LblRotation = New System.Windows.Forms.Label()
        Me.LbAngle = New System.Windows.Forms.ListBox()
        Me.LbIncT2 = New System.Windows.Forms.ListBox()
        Me.LbIncT1 = New System.Windows.Forms.ListBox()
        Me.BtnT2 = New System.Windows.Forms.Button()
        Me.BtnT1 = New System.Windows.Forms.Button()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(220, 127)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 17)
        Me.Label2.TabIndex = 71
        Me.Label2.Text = "INCLINATION:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(40, 127)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 17)
        Me.Label1.TabIndex = 70
        Me.Label1.Text = "INCLINATION:"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.TAVIL2020.My.Resources.Resources.Transportador_300x206
        Me.PictureBox2.Location = New System.Drawing.Point(218, 12)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(100, 66)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 69
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.TAVIL2020.My.Resources.Resources.Transportador_300x206
        Me.PictureBox1.Location = New System.Drawing.Point(38, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(100, 66)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 68
        Me.PictureBox1.TabStop = False
        '
        'LbLado
        '
        Me.LbLado.FormattingEnabled = True
        Me.LbLado.ItemHeight = 16
        Me.LbLado.Items.AddRange(New Object() {"", "L", "C", "R"})
        Me.LbLado.Location = New System.Drawing.Point(417, 108)
        Me.LbLado.Name = "LbLado"
        Me.LbLado.Size = New System.Drawing.Size(66, 68)
        Me.LbLado.TabIndex = 67
        '
        'LblLado
        '
        Me.LblLado.Location = New System.Drawing.Point(416, 88)
        Me.LblLado.Name = "LblLado"
        Me.LblLado.Size = New System.Drawing.Size(56, 17)
        Me.LblLado.TabIndex = 66
        Me.LblLado.Text = "LADO :"
        '
        'LblRotation
        '
        Me.LblRotation.Location = New System.Drawing.Point(416, 12)
        Me.LblRotation.Name = "LblRotation"
        Me.LblRotation.Size = New System.Drawing.Size(67, 17)
        Me.LblRotation.TabIndex = 65
        Me.LblRotation.Text = "ANGLE :"
        '
        'LbAngle
        '
        Me.LbAngle.FormattingEnabled = True
        Me.LbAngle.ItemHeight = 16
        Me.LbAngle.Items.AddRange(New Object() {"0", "90"})
        Me.LbAngle.Location = New System.Drawing.Point(417, 32)
        Me.LbAngle.Name = "LbAngle"
        Me.LbAngle.Size = New System.Drawing.Size(66, 36)
        Me.LbAngle.TabIndex = 64
        '
        'LbIncT2
        '
        Me.LbIncT2.FormattingEnabled = True
        Me.LbIncT2.ItemHeight = 16
        Me.LbIncT2.Items.AddRange(New Object() {"FLAT", "DOWN", "UP"})
        Me.LbIncT2.Location = New System.Drawing.Point(235, 147)
        Me.LbIncT2.Name = "LbIncT2"
        Me.LbIncT2.Size = New System.Drawing.Size(66, 52)
        Me.LbIncT2.TabIndex = 63
        '
        'LbIncT1
        '
        Me.LbIncT1.FormattingEnabled = True
        Me.LbIncT1.ItemHeight = 16
        Me.LbIncT1.Items.AddRange(New Object() {"FLAT", "DOWN", "UP"})
        Me.LbIncT1.Location = New System.Drawing.Point(55, 147)
        Me.LbIncT1.Name = "LbIncT1"
        Me.LbIncT1.Size = New System.Drawing.Size(66, 52)
        Me.LbIncT1.TabIndex = 62
        '
        'BtnT2
        '
        Me.BtnT2.BackColor = System.Drawing.SystemColors.Control
        Me.BtnT2.Location = New System.Drawing.Point(251, 84)
        Me.BtnT2.Name = "BtnT2"
        Me.BtnT2.Size = New System.Drawing.Size(35, 35)
        Me.BtnT2.TabIndex = 61
        Me.BtnT2.Text = "T2"
        Me.BtnT2.UseVisualStyleBackColor = False
        '
        'BtnT1
        '
        Me.BtnT1.BackColor = System.Drawing.SystemColors.Control
        Me.BtnT1.Location = New System.Drawing.Point(71, 84)
        Me.BtnT1.Name = "BtnT1"
        Me.BtnT1.Size = New System.Drawing.Size(35, 35)
        Me.BtnT1.TabIndex = 60
        Me.BtnT1.Text = "T1"
        Me.BtnT1.UseVisualStyleBackColor = False
        '
        'UCUnion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.LbLado)
        Me.Controls.Add(Me.LblLado)
        Me.Controls.Add(Me.LblRotation)
        Me.Controls.Add(Me.LbAngle)
        Me.Controls.Add(Me.LbIncT2)
        Me.Controls.Add(Me.LbIncT1)
        Me.Controls.Add(Me.BtnT2)
        Me.Controls.Add(Me.BtnT1)
        Me.Name = "UCUnion"
        Me.Size = New System.Drawing.Size(516, 240)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents PictureBox2 As Windows.Forms.PictureBox
    Friend WithEvents PictureBox1 As Windows.Forms.PictureBox
    Friend WithEvents LbLado As Windows.Forms.ListBox
    Friend WithEvents LblLado As Windows.Forms.Label
    Friend WithEvents LblRotation As Windows.Forms.Label
    Friend WithEvents LbAngle As Windows.Forms.ListBox
    Friend WithEvents LbIncT2 As Windows.Forms.ListBox
    Friend WithEvents LbIncT1 As Windows.Forms.ListBox
    Friend WithEvents BtnT2 As Windows.Forms.Button
    Friend WithEvents BtnT1 As Windows.Forms.Button
End Class
