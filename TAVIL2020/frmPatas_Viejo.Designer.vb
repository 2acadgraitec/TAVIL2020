<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPatas
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.btnSelCinta = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtL1 = New System.Windows.Forms.TextBox()
        Me.txtA1 = New System.Windows.Forms.TextBox()
        Me.lblBloque = New System.Windows.Forms.Label()
        Me.btnInsertaPata = New System.Windows.Forms.Button()
        Me.lblNPatas = New System.Windows.Forms.Label()
        Me.cbNPatas = New System.Windows.Forms.ComboBox()
        Me.cbPatas = New System.Windows.Forms.ComboBox()
        Me.pbBloque = New System.Windows.Forms.PictureBox()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.pbBloque, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(369, 337)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(195, 36)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(4, 4)
        Me.OK_Button.Margin = New System.Windows.Forms.Padding(4)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(89, 28)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "Aceptar"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(101, 4)
        Me.Cancel_Button.Margin = New System.Windows.Forms.Padding(4)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(89, 28)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancelar"
        '
        'btnSelCinta
        '
        Me.btnSelCinta.ForeColor = System.Drawing.Color.Red
        Me.btnSelCinta.Location = New System.Drawing.Point(22, 29)
        Me.btnSelCinta.Name = "btnSelCinta"
        Me.btnSelCinta.Size = New System.Drawing.Size(279, 35)
        Me.btnSelCinta.TabIndex = 1
        Me.btnSelCinta.Text = "1.- Seleccione Cinta para poner patas"
        Me.btnSelCinta.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(344, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 17)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Largo :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(344, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 17)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Ancho :"
        '
        'txtL1
        '
        Me.txtL1.Enabled = False
        Me.txtL1.Location = New System.Drawing.Point(403, 30)
        Me.txtL1.Name = "txtL1"
        Me.txtL1.Size = New System.Drawing.Size(135, 22)
        Me.txtL1.TabIndex = 6
        '
        'txtA1
        '
        Me.txtA1.Enabled = False
        Me.txtA1.Location = New System.Drawing.Point(403, 66)
        Me.txtA1.Name = "txtA1"
        Me.txtA1.Size = New System.Drawing.Size(135, 22)
        Me.txtA1.TabIndex = 7
        '
        'lblBloque
        '
        Me.lblBloque.AutoSize = True
        Me.lblBloque.ForeColor = System.Drawing.Color.Red
        Me.lblBloque.Location = New System.Drawing.Point(34, 111)
        Me.lblBloque.Name = "lblBloque"
        Me.lblBloque.Size = New System.Drawing.Size(202, 17)
        Me.lblBloque.TabIndex = 8
        Me.lblBloque.Text = "2.- Seleccionar bloque de pata"
        '
        'btnInsertaPata
        '
        Me.btnInsertaPata.ForeColor = System.Drawing.Color.Red
        Me.btnInsertaPata.Location = New System.Drawing.Point(23, 295)
        Me.btnInsertaPata.Name = "btnInsertaPata"
        Me.btnInsertaPata.Size = New System.Drawing.Size(156, 35)
        Me.btnInsertaPata.TabIndex = 10
        Me.btnInsertaPata.Text = "5.- Insertar Patas"
        Me.btnInsertaPata.UseVisualStyleBackColor = True
        '
        'lblNPatas
        '
        Me.lblNPatas.AutoSize = True
        Me.lblNPatas.ForeColor = System.Drawing.Color.Red
        Me.lblNPatas.Location = New System.Drawing.Point(34, 215)
        Me.lblNPatas.Name = "lblNPatas"
        Me.lblNPatas.Size = New System.Drawing.Size(210, 17)
        Me.lblNPatas.TabIndex = 11
        Me.lblNPatas.Text = "4.- ¿Cuantas Patas Insertamos?"
        '
        'cbNPatas
        '
        Me.cbNPatas.FormattingEnabled = True
        Me.cbNPatas.Items.AddRange(New Object() {"1", "2", "3", "4", "Segun largo"})
        Me.cbNPatas.Location = New System.Drawing.Point(262, 212)
        Me.cbNPatas.Name = "cbNPatas"
        Me.cbNPatas.Size = New System.Drawing.Size(85, 24)
        Me.cbNPatas.TabIndex = 12
        Me.cbNPatas.Text = "2"
        '
        'cbPatas
        '
        Me.cbPatas.FormattingEnabled = True
        Me.cbPatas.Location = New System.Drawing.Point(37, 131)
        Me.cbPatas.Name = "cbPatas"
        Me.cbPatas.Size = New System.Drawing.Size(264, 24)
        Me.cbPatas.TabIndex = 9
        '
        'pbBloque
        '
        Me.pbBloque.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.pbBloque.Location = New System.Drawing.Point(403, 131)
        Me.pbBloque.Name = "pbBloque"
        Me.pbBloque.Size = New System.Drawing.Size(128, 128)
        Me.pbBloque.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbBloque.TabIndex = 13
        Me.pbBloque.TabStop = False
        '
        'frmPatas
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(580, 388)
        Me.Controls.Add(Me.pbBloque)
        Me.Controls.Add(Me.cbNPatas)
        Me.Controls.Add(Me.lblNPatas)
        Me.Controls.Add(Me.btnInsertaPata)
        Me.Controls.Add(Me.cbPatas)
        Me.Controls.Add(Me.lblBloque)
        Me.Controls.Add(Me.txtA1)
        Me.Controls.Add(Me.txtL1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnSelCinta)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPatas"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "PATAS TRANSPORTADORES"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.pbBloque, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents btnSelCinta As Windows.Forms.Button
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents txtL1 As Windows.Forms.TextBox
    Friend WithEvents txtA1 As Windows.Forms.TextBox
    Friend WithEvents lblBloque As Windows.Forms.Label
    Friend WithEvents btnInsertaPata As Windows.Forms.Button
    Friend WithEvents lblNPatas As Windows.Forms.Label
    Friend WithEvents cbNPatas As Windows.Forms.ComboBox
    Friend WithEvents cbPatas As Windows.Forms.ComboBox
    Friend WithEvents pbBloque As Windows.Forms.PictureBox
End Class
