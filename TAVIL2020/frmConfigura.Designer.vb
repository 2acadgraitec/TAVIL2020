<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigura
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfigura))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.txtBloqueRecursos = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnBloqueRecursos = New System.Windows.Forms.Button()
        Me.btnDirBloques = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDirBloques = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtPatasCapa = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCintasCapa = New System.Windows.Forms.TextBox()
        Me.btnExcelDb = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtExcelDb = New System.Windows.Forms.TextBox()
        Me.btnReReadDB = New System.Windows.Forms.Button()
        Me.lblInf = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(537, 337)
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
        'txtBloqueRecursos
        '
        Me.txtBloqueRecursos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBloqueRecursos.Enabled = False
        Me.txtBloqueRecursos.Location = New System.Drawing.Point(12, 99)
        Me.txtBloqueRecursos.Name = "txtBloqueRecursos"
        Me.txtBloqueRecursos.Size = New System.Drawing.Size(679, 22)
        Me.txtBloqueRecursos.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 74)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(143, 17)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "File DWG resources :"
        '
        'btnBloqueRecursos
        '
        Me.btnBloqueRecursos.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBloqueRecursos.Location = New System.Drawing.Point(697, 98)
        Me.btnBloqueRecursos.Name = "btnBloqueRecursos"
        Me.btnBloqueRecursos.Size = New System.Drawing.Size(35, 23)
        Me.btnBloqueRecursos.TabIndex = 3
        Me.btnBloqueRecursos.Text = "···"
        Me.btnBloqueRecursos.UseVisualStyleBackColor = True
        '
        'btnDirBloques
        '
        Me.btnDirBloques.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDirBloques.Location = New System.Drawing.Point(697, 174)
        Me.btnDirBloques.Name = "btnDirBloques"
        Me.btnDirBloques.Size = New System.Drawing.Size(35, 23)
        Me.btnDirBloques.TabIndex = 6
        Me.btnDirBloques.Text = "···"
        Me.btnDirBloques.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 150)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(126, 17)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Blocks folder root :"
        '
        'txtDirBloques
        '
        Me.txtDirBloques.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDirBloques.Enabled = False
        Me.txtDirBloques.Location = New System.Drawing.Point(12, 175)
        Me.txtDirBloques.Name = "txtDirBloques"
        Me.txtDirBloques.Size = New System.Drawing.Size(679, 22)
        Me.txtDirBloques.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(453, 244)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 17)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Layer leg :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPatasCapa
        '
        Me.txtPatasCapa.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPatasCapa.Location = New System.Drawing.Point(537, 239)
        Me.txtPatasCapa.Name = "txtPatasCapa"
        Me.txtPatasCapa.Size = New System.Drawing.Size(195, 22)
        Me.txtPatasCapa.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(435, 282)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 17)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Layer cintas :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCintasCapa
        '
        Me.txtCintasCapa.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCintasCapa.Location = New System.Drawing.Point(537, 279)
        Me.txtCintasCapa.Name = "txtCintasCapa"
        Me.txtCintasCapa.Size = New System.Drawing.Size(195, 22)
        Me.txtCintasCapa.TabIndex = 9
        '
        'btnExcelDb
        '
        Me.btnExcelDb.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExcelDb.Location = New System.Drawing.Point(638, 34)
        Me.btnExcelDb.Name = "btnExcelDb"
        Me.btnExcelDb.Size = New System.Drawing.Size(35, 23)
        Me.btnExcelDb.TabIndex = 13
        Me.btnExcelDb.Text = "···"
        Me.btnExcelDb.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(98, 17)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "File Excel DB :"
        '
        'txtExcelDb
        '
        Me.txtExcelDb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtExcelDb.Enabled = False
        Me.txtExcelDb.Location = New System.Drawing.Point(12, 35)
        Me.txtExcelDb.Name = "txtExcelDb"
        Me.txtExcelDb.Size = New System.Drawing.Size(620, 22)
        Me.txtExcelDb.TabIndex = 11
        '
        'btnReReadDB
        '
        Me.btnReReadDB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReReadDB.BackgroundImage = Global.TAVIL2020.My.Resources.Resources.Recargar
        Me.btnReReadDB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnReReadDB.Location = New System.Drawing.Point(682, 20)
        Me.btnReReadDB.Name = "btnReReadDB"
        Me.btnReReadDB.Size = New System.Drawing.Size(50, 50)
        Me.btnReReadDB.TabIndex = 14
        Me.btnReReadDB.Text = "···"
        Me.btnReReadDB.UseVisualStyleBackColor = True
        '
        'lblInf
        '
        Me.lblInf.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblInf.Location = New System.Drawing.Point(12, 337)
        Me.lblInf.Name = "lblInf"
        Me.lblInf.Size = New System.Drawing.Size(516, 36)
        Me.lblInf.TabIndex = 15
        Me.lblInf.Text = "."
        '
        'frmConfigura
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(748, 388)
        Me.Controls.Add(Me.lblInf)
        Me.Controls.Add(Me.btnReReadDB)
        Me.Controls.Add(Me.btnExcelDb)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtExcelDb)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtCintasCapa)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtPatasCapa)
        Me.Controls.Add(Me.btnDirBloques)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtDirBloques)
        Me.Controls.Add(Me.btnBloqueRecursos)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtBloqueRecursos)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmConfigura"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "CONFIGURACION"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents txtBloqueRecursos As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents btnBloqueRecursos As Windows.Forms.Button
    Friend WithEvents btnDirBloques As Windows.Forms.Button
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents txtDirBloques As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents txtPatasCapa As Windows.Forms.TextBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents txtCintasCapa As Windows.Forms.TextBox
    Friend WithEvents btnExcelDb As Windows.Forms.Button
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents txtExcelDb As Windows.Forms.TextBox
    Friend WithEvents btnReReadDB As Windows.Forms.Button
    Friend WithEvents lblInf As Windows.Forms.Label
End Class
