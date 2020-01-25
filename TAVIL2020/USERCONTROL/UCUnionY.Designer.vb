<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCUnionY
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
        Me.LblRotation = New System.Windows.Forms.Label()
        Me.LbAngleL = New System.Windows.Forms.ListBox()
        Me.LbIncL = New System.Windows.Forms.ListBox()
        Me.LbInc = New System.Windows.Forms.ListBox()
        Me.BtnT2L = New System.Windows.Forms.Button()
        Me.BtnT1 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LbAngleR = New System.Windows.Forms.ListBox()
        Me.LbIncR = New System.Windows.Forms.ListBox()
        Me.BtnT2R = New System.Windows.Forms.Button()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(442, 134)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 17)
        Me.Label2.TabIndex = 71
        Me.Label2.Text = "INCLI.:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 94)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 15)
        Me.Label1.TabIndex = 70
        Me.Label1.Text = "INCLI.:"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.TAVIL2020.My.Resources.Resources.Transportador_300x206
        Me.PictureBox2.Location = New System.Drawing.Point(260, 172)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(100, 66)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 69
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.TAVIL2020.My.Resources.Resources.Transportador_300x206
        Me.PictureBox1.Location = New System.Drawing.Point(91, 96)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(100, 66)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 68
        Me.PictureBox1.TabStop = False
        '
        'LblRotation
        '
        Me.LblRotation.Location = New System.Drawing.Point(364, 134)
        Me.LblRotation.Name = "LblRotation"
        Me.LblRotation.Size = New System.Drawing.Size(67, 17)
        Me.LblRotation.TabIndex = 65
        Me.LblRotation.Text = "ANGLE :"
        '
        'LbAngleL
        '
        Me.LbAngleL.FormattingEnabled = True
        Me.LbAngleL.ItemHeight = 16
        Me.LbAngleL.Items.AddRange(New Object() {"0", "90"})
        Me.LbAngleL.Location = New System.Drawing.Point(367, 154)
        Me.LbAngleL.Name = "LbAngleL"
        Me.LbAngleL.Size = New System.Drawing.Size(66, 36)
        Me.LbAngleL.TabIndex = 64
        '
        'LbIncL
        '
        Me.LbIncL.FormattingEnabled = True
        Me.LbIncL.ItemHeight = 16
        Me.LbIncL.Items.AddRange(New Object() {"FLAT", "DOWN", "UP"})
        Me.LbIncL.Location = New System.Drawing.Point(445, 154)
        Me.LbIncL.Name = "LbIncL"
        Me.LbIncL.Size = New System.Drawing.Size(66, 52)
        Me.LbIncL.TabIndex = 63
        '
        'LbInc
        '
        Me.LbInc.FormattingEnabled = True
        Me.LbInc.ItemHeight = 16
        Me.LbInc.Items.AddRange(New Object() {"FLAT", "DOWN", "UP"})
        Me.LbInc.Location = New System.Drawing.Point(11, 113)
        Me.LbInc.Name = "LbInc"
        Me.LbInc.Size = New System.Drawing.Size(66, 52)
        Me.LbInc.TabIndex = 62
        '
        'BtnT2L
        '
        Me.BtnT2L.Location = New System.Drawing.Point(288, 131)
        Me.BtnT2L.Name = "BtnT2L"
        Me.BtnT2L.Size = New System.Drawing.Size(45, 35)
        Me.BtnT2L.TabIndex = 61
        Me.BtnT2L.Text = "T2L"
        Me.BtnT2L.UseVisualStyleBackColor = True
        '
        'BtnT1
        '
        Me.BtnT1.Location = New System.Drawing.Point(124, 55)
        Me.BtnT1.Name = "BtnT1"
        Me.BtnT1.Size = New System.Drawing.Size(35, 35)
        Me.BtnT1.TabIndex = 60
        Me.BtnT1.Text = "T1"
        Me.BtnT1.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(442, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 17)
        Me.Label3.TabIndex = 78
        Me.Label3.Text = "INCLI.:"
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.TAVIL2020.My.Resources.Resources.Transportador_300x206
        Me.PictureBox3.Location = New System.Drawing.Point(260, 43)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(100, 66)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox3.TabIndex = 77
        Me.PictureBox3.TabStop = False
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(364, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 17)
        Me.Label4.TabIndex = 76
        Me.Label4.Text = "ANGLE :"
        '
        'LbAngleR
        '
        Me.LbAngleR.FormattingEnabled = True
        Me.LbAngleR.ItemHeight = 16
        Me.LbAngleR.Items.AddRange(New Object() {"0", "90"})
        Me.LbAngleR.Location = New System.Drawing.Point(367, 27)
        Me.LbAngleR.Name = "LbAngleR"
        Me.LbAngleR.Size = New System.Drawing.Size(66, 36)
        Me.LbAngleR.TabIndex = 75
        '
        'LbIncR
        '
        Me.LbIncR.FormattingEnabled = True
        Me.LbIncR.ItemHeight = 16
        Me.LbIncR.Items.AddRange(New Object() {"FLAT", "DOWN", "UP"})
        Me.LbIncR.Location = New System.Drawing.Point(445, 27)
        Me.LbIncR.Name = "LbIncR"
        Me.LbIncR.Size = New System.Drawing.Size(66, 52)
        Me.LbIncR.TabIndex = 74
        '
        'BtnT2R
        '
        Me.BtnT2R.Location = New System.Drawing.Point(288, 3)
        Me.BtnT2R.Name = "BtnT2R"
        Me.BtnT2R.Size = New System.Drawing.Size(45, 35)
        Me.BtnT2R.TabIndex = 73
        Me.BtnT2R.Text = "T2R"
        Me.BtnT2R.UseVisualStyleBackColor = True
        '
        'UCUnionY
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.LbAngleR)
        Me.Controls.Add(Me.LbIncR)
        Me.Controls.Add(Me.BtnT2R)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.LblRotation)
        Me.Controls.Add(Me.LbAngleL)
        Me.Controls.Add(Me.LbIncL)
        Me.Controls.Add(Me.LbInc)
        Me.Controls.Add(Me.BtnT2L)
        Me.Controls.Add(Me.BtnT1)
        Me.Name = "UCUnionY"
        Me.Size = New System.Drawing.Size(516, 240)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents PictureBox2 As Windows.Forms.PictureBox
    Friend WithEvents PictureBox1 As Windows.Forms.PictureBox
    Friend WithEvents LblRotation As Windows.Forms.Label
    Friend WithEvents LbAngleL As Windows.Forms.ListBox
    Friend WithEvents LbIncL As Windows.Forms.ListBox
    Friend WithEvents LbInc As Windows.Forms.ListBox
    Friend WithEvents BtnT2L As Windows.Forms.Button
    Friend WithEvents BtnT1 As Windows.Forms.Button
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents PictureBox3 As Windows.Forms.PictureBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents LbAngleR As Windows.Forms.ListBox
    Friend WithEvents LbIncR As Windows.Forms.ListBox
    Friend WithEvents BtnT2R As Windows.Forms.Button
End Class
