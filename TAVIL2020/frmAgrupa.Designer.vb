<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAgrupa
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cerrar_Button = New System.Windows.Forms.Button()
        Me.tvGrupos = New System.Windows.Forms.TreeView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNombreGrupo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnGrupoCrear = New System.Windows.Forms.Button()
        Me.btnGrupoBorrar = New System.Windows.Forms.Button()
        Me.gbEditar = New System.Windows.Forms.GroupBox()
        Me.gbAdministrar = New System.Windows.Forms.GroupBox()
        Me.btnGrupoQuitar = New System.Windows.Forms.Button()
        Me.btnGrupoAdd = New System.Windows.Forms.Button()
        Me.cbZoom = New System.Windows.Forms.CheckBox()
        Me.lblInf = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.gbEditar.SuspendLayout()
        Me.gbAdministrar.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cerrar_Button, 1, 0)
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
        Me.OK_Button.Visible = False
        '
        'Cerrar_Button
        '
        Me.Cerrar_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cerrar_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cerrar_Button.Location = New System.Drawing.Point(101, 4)
        Me.Cerrar_Button.Margin = New System.Windows.Forms.Padding(4)
        Me.Cerrar_Button.Name = "Cerrar_Button"
        Me.Cerrar_Button.Size = New System.Drawing.Size(89, 28)
        Me.Cerrar_Button.TabIndex = 1
        Me.Cerrar_Button.Text = "Cerrar"
        '
        'tvGrupos
        '
        Me.tvGrupos.Location = New System.Drawing.Point(12, 40)
        Me.tvGrupos.Name = "tvGrupos"
        Me.tvGrupos.ShowNodeToolTips = True
        Me.tvGrupos.Size = New System.Drawing.Size(152, 295)
        Me.tvGrupos.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 17)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Grupos :"
        '
        'txtNombreGrupo
        '
        Me.txtNombreGrupo.Location = New System.Drawing.Point(343, 21)
        Me.txtNombreGrupo.Name = "txtNombreGrupo"
        Me.txtNombreGrupo.Size = New System.Drawing.Size(202, 22)
        Me.txtNombreGrupo.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(185, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(142, 17)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Nombre de Grupo :   "
        '
        'btnGrupoCrear
        '
        Me.btnGrupoCrear.Location = New System.Drawing.Point(54, 31)
        Me.btnGrupoCrear.Name = "btnGrupoCrear"
        Me.btnGrupoCrear.Size = New System.Drawing.Size(75, 28)
        Me.btnGrupoCrear.TabIndex = 5
        Me.btnGrupoCrear.Text = "Crear"
        Me.btnGrupoCrear.UseVisualStyleBackColor = True
        '
        'btnGrupoBorrar
        '
        Me.btnGrupoBorrar.Enabled = False
        Me.btnGrupoBorrar.Location = New System.Drawing.Point(54, 74)
        Me.btnGrupoBorrar.Name = "btnGrupoBorrar"
        Me.btnGrupoBorrar.Size = New System.Drawing.Size(75, 28)
        Me.btnGrupoBorrar.TabIndex = 7
        Me.btnGrupoBorrar.Text = "Borrar"
        Me.btnGrupoBorrar.UseVisualStyleBackColor = True
        '
        'gbEditar
        '
        Me.gbEditar.Controls.Add(Me.btnGrupoCrear)
        Me.gbEditar.Controls.Add(Me.btnGrupoBorrar)
        Me.gbEditar.Location = New System.Drawing.Point(387, 67)
        Me.gbEditar.Name = "gbEditar"
        Me.gbEditar.Size = New System.Drawing.Size(177, 121)
        Me.gbEditar.TabIndex = 8
        Me.gbEditar.TabStop = False
        Me.gbEditar.Text = "Editar Grupo :"
        '
        'gbAdministrar
        '
        Me.gbAdministrar.Controls.Add(Me.btnGrupoQuitar)
        Me.gbAdministrar.Controls.Add(Me.btnGrupoAdd)
        Me.gbAdministrar.Enabled = False
        Me.gbAdministrar.Location = New System.Drawing.Point(181, 69)
        Me.gbAdministrar.Name = "gbAdministrar"
        Me.gbAdministrar.Size = New System.Drawing.Size(189, 119)
        Me.gbAdministrar.TabIndex = 9
        Me.gbAdministrar.TabStop = False
        Me.gbAdministrar.Text = "Administrar Grupo :"
        '
        'btnGrupoQuitar
        '
        Me.btnGrupoQuitar.Location = New System.Drawing.Point(20, 75)
        Me.btnGrupoQuitar.Name = "btnGrupoQuitar"
        Me.btnGrupoQuitar.Size = New System.Drawing.Size(139, 28)
        Me.btnGrupoQuitar.TabIndex = 9
        Me.btnGrupoQuitar.Text = "Quitar elementos"
        Me.btnGrupoQuitar.UseVisualStyleBackColor = True
        '
        'btnGrupoAdd
        '
        Me.btnGrupoAdd.Location = New System.Drawing.Point(20, 29)
        Me.btnGrupoAdd.Name = "btnGrupoAdd"
        Me.btnGrupoAdd.Size = New System.Drawing.Size(139, 28)
        Me.btnGrupoAdd.TabIndex = 8
        Me.btnGrupoAdd.Text = "Agregar elementos"
        Me.btnGrupoAdd.UseVisualStyleBackColor = True
        '
        'cbZoom
        '
        Me.cbZoom.AutoSize = True
        Me.cbZoom.Checked = True
        Me.cbZoom.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbZoom.Location = New System.Drawing.Point(19, 352)
        Me.cbZoom.Name = "cbZoom"
        Me.cbZoom.Size = New System.Drawing.Size(199, 21)
        Me.cbZoom.TabIndex = 10
        Me.cbZoom.Text = "Zoom Grupo Seleccionado"
        Me.cbZoom.UseVisualStyleBackColor = True
        '
        'lblInf
        '
        Me.lblInf.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblInf.Location = New System.Drawing.Point(181, 252)
        Me.lblInf.Name = "lblInf"
        Me.lblInf.Size = New System.Drawing.Size(378, 71)
        Me.lblInf.TabIndex = 11
        Me.lblInf.Text = "¿?"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(178, 226)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 17)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Información :"
        '
        'frmAgrupa
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cerrar_Button
        Me.ClientSize = New System.Drawing.Size(580, 388)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblInf)
        Me.Controls.Add(Me.cbZoom)
        Me.Controls.Add(Me.gbAdministrar)
        Me.Controls.Add(Me.gbEditar)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtNombreGrupo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tvGrupos)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAgrupa"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmAgrupa"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.gbEditar.ResumeLayout(False)
        Me.gbAdministrar.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cerrar_Button As System.Windows.Forms.Button
    Friend WithEvents tvGrupos As Windows.Forms.TreeView
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents txtNombreGrupo As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents btnGrupoCrear As Windows.Forms.Button
    Friend WithEvents btnGrupoBorrar As Windows.Forms.Button
    Friend WithEvents gbEditar As Windows.Forms.GroupBox
    Friend WithEvents gbAdministrar As Windows.Forms.GroupBox
    Friend WithEvents btnGrupoQuitar As Windows.Forms.Button
    Friend WithEvents btnGrupoAdd As Windows.Forms.Button
    Friend WithEvents cbZoom As Windows.Forms.CheckBox
    Friend WithEvents lblInf As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
End Class
