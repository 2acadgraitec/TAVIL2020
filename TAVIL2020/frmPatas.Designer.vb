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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatas))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.CERRAR_Button = New System.Windows.Forms.Button()
        Me.pbBloque = New System.Windows.Forms.PictureBox()
        Me.tvPatas = New System.Windows.Forms.TreeView()
        Me.lblListadoPatas = New System.Windows.Forms.Label()
        Me.lblWIDTH = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCODE = New System.Windows.Forms.TextBox()
        Me.cbSTANDARD = New System.Windows.Forms.ComboBox()
        Me.btnWIDTH = New System.Windows.Forms.Button()
        Me.btnHEIGHT = New System.Windows.Forms.Button()
        Me.btnGUARDAR = New System.Windows.Forms.Button()
        Me.gDatos = New System.Windows.Forms.GroupBox()
        Me.txtDIRECTRIZ1 = New System.Windows.Forms.TextBox()
        Me.txtDIRECTRIZ = New System.Windows.Forms.TextBox()
        Me.btnTODO = New System.Windows.Forms.Button()
        Me.cbHEIGHT = New System.Windows.Forms.ComboBox()
        Me.cbWIDTH = New System.Windows.Forms.ComboBox()
        Me.tt1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnActualizar = New System.Windows.Forms.Button()
        Me.btnSeleccionar = New System.Windows.Forms.Button()
        Me.cbXXX = New System.Windows.Forms.CheckBox()
        Me.btnContar = New System.Windows.Forms.Button()
        Me.btnListar = New System.Windows.Forms.Button()
        Me.cbPLANTA = New System.Windows.Forms.CheckBox()
        Me.pb1 = New System.Windows.Forms.ProgressBar()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.pbBloque, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gDatos.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CERRAR_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(309, 517)
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
        'CERRAR_Button
        '
        Me.CERRAR_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CERRAR_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CERRAR_Button.Location = New System.Drawing.Point(101, 4)
        Me.CERRAR_Button.Margin = New System.Windows.Forms.Padding(4)
        Me.CERRAR_Button.Name = "CERRAR_Button"
        Me.CERRAR_Button.Size = New System.Drawing.Size(89, 28)
        Me.CERRAR_Button.TabIndex = 1
        Me.CERRAR_Button.Text = "Cerrar"
        '
        'pbBloque
        '
        Me.pbBloque.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbBloque.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.pbBloque.Location = New System.Drawing.Point(382, 11)
        Me.pbBloque.Name = "pbBloque"
        Me.pbBloque.Size = New System.Drawing.Size(128, 128)
        Me.pbBloque.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbBloque.TabIndex = 13
        Me.pbBloque.TabStop = False
        '
        'tvPatas
        '
        Me.tvPatas.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvPatas.HideSelection = False
        Me.tvPatas.HotTracking = True
        Me.tvPatas.Location = New System.Drawing.Point(12, 42)
        Me.tvPatas.Name = "tvPatas"
        Me.tvPatas.ShowNodeToolTips = True
        Me.tvPatas.Size = New System.Drawing.Size(189, 498)
        Me.tvPatas.TabIndex = 14
        '
        'lblListadoPatas
        '
        Me.lblListadoPatas.AutoSize = True
        Me.lblListadoPatas.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblListadoPatas.Location = New System.Drawing.Point(12, 12)
        Me.lblListadoPatas.Name = "lblListadoPatas"
        Me.lblListadoPatas.Size = New System.Drawing.Size(151, 20)
        Me.lblListadoPatas.TabIndex = 15
        Me.lblListadoPatas.Text = "Listado Patas (XX)"
        '
        'lblWIDTH
        '
        Me.lblWIDTH.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblWIDTH.Location = New System.Drawing.Point(34, 73)
        Me.lblWIDTH.Name = "lblWIDTH"
        Me.lblWIDTH.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWIDTH.Size = New System.Drawing.Size(116, 17)
        Me.lblWIDTH.TabIndex = 16
        Me.lblWIDTH.Text = "WIDTH/RADIUS :"
        Me.lblWIDTH.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(93, 202)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 17)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "CODE :"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(81, 114)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 17)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "HEIGHT :"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 156)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(137, 17)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "STANDARD_PART :"
        '
        'txtCODE
        '
        Me.txtCODE.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCODE.Enabled = False
        Me.txtCODE.Location = New System.Drawing.Point(157, 199)
        Me.txtCODE.Name = "txtCODE"
        Me.txtCODE.Size = New System.Drawing.Size(128, 22)
        Me.txtCODE.TabIndex = 23
        '
        'cbSTANDARD
        '
        Me.cbSTANDARD.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbSTANDARD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSTANDARD.FormattingEnabled = True
        Me.cbSTANDARD.Items.AddRange(New Object() {"SI", "NO"})
        Me.cbSTANDARD.Location = New System.Drawing.Point(157, 153)
        Me.cbSTANDARD.MaxDropDownItems = 2
        Me.cbSTANDARD.Name = "cbSTANDARD"
        Me.cbSTANDARD.Size = New System.Drawing.Size(59, 24)
        Me.cbSTANDARD.TabIndex = 25
        '
        'btnWIDTH
        '
        Me.btnWIDTH.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnWIDTH.BackgroundImage = Global.TAVIL2020.My.Resources.Resources.copy32
        Me.btnWIDTH.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnWIDTH.Location = New System.Drawing.Point(256, 65)
        Me.btnWIDTH.Name = "btnWIDTH"
        Me.btnWIDTH.Size = New System.Drawing.Size(30, 30)
        Me.btnWIDTH.TabIndex = 26
        Me.tt1.SetToolTip(Me.btnWIDTH, "Copiar WIDTH desde:")
        Me.btnWIDTH.UseVisualStyleBackColor = True
        '
        'btnHEIGHT
        '
        Me.btnHEIGHT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnHEIGHT.BackgroundImage = Global.TAVIL2020.My.Resources.Resources.copy32
        Me.btnHEIGHT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnHEIGHT.Location = New System.Drawing.Point(256, 107)
        Me.btnHEIGHT.Name = "btnHEIGHT"
        Me.btnHEIGHT.Size = New System.Drawing.Size(30, 30)
        Me.btnHEIGHT.TabIndex = 29
        Me.btnHEIGHT.Text = "·"
        Me.tt1.SetToolTip(Me.btnHEIGHT, "Copiar HEIGHT desde:")
        Me.btnHEIGHT.UseVisualStyleBackColor = True
        '
        'btnGUARDAR
        '
        Me.btnGUARDAR.Enabled = False
        Me.btnGUARDAR.Location = New System.Drawing.Point(51, 240)
        Me.btnGUARDAR.Name = "btnGUARDAR"
        Me.btnGUARDAR.Size = New System.Drawing.Size(186, 23)
        Me.btnGUARDAR.TabIndex = 30
        Me.btnGUARDAR.Text = "GUARDAR CAMBIOS"
        Me.tt1.SetToolTip(Me.btnGUARDAR, "Guardar cambios en bloque")
        Me.btnGUARDAR.UseVisualStyleBackColor = True
        '
        'gDatos
        '
        Me.gDatos.Controls.Add(Me.txtDIRECTRIZ1)
        Me.gDatos.Controls.Add(Me.txtDIRECTRIZ)
        Me.gDatos.Controls.Add(Me.btnTODO)
        Me.gDatos.Controls.Add(Me.cbHEIGHT)
        Me.gDatos.Controls.Add(Me.cbWIDTH)
        Me.gDatos.Controls.Add(Me.btnGUARDAR)
        Me.gDatos.Controls.Add(Me.lblWIDTH)
        Me.gDatos.Controls.Add(Me.btnHEIGHT)
        Me.gDatos.Controls.Add(Me.btnWIDTH)
        Me.gDatos.Controls.Add(Me.cbSTANDARD)
        Me.gDatos.Controls.Add(Me.Label4)
        Me.gDatos.Controls.Add(Me.Label5)
        Me.gDatos.Controls.Add(Me.txtCODE)
        Me.gDatos.Controls.Add(Me.Label6)
        Me.gDatos.Enabled = False
        Me.gDatos.Location = New System.Drawing.Point(213, 232)
        Me.gDatos.Name = "gDatos"
        Me.gDatos.Size = New System.Drawing.Size(295, 274)
        Me.gDatos.TabIndex = 31
        Me.gDatos.TabStop = False
        Me.gDatos.Text = "Atributos / Parametros"
        '
        'txtDIRECTRIZ1
        '
        Me.txtDIRECTRIZ1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDIRECTRIZ1.Enabled = False
        Me.txtDIRECTRIZ1.Location = New System.Drawing.Point(16, 212)
        Me.txtDIRECTRIZ1.Name = "txtDIRECTRIZ1"
        Me.txtDIRECTRIZ1.Size = New System.Drawing.Size(45, 22)
        Me.txtDIRECTRIZ1.TabIndex = 40
        Me.txtDIRECTRIZ1.Visible = False
        '
        'txtDIRECTRIZ
        '
        Me.txtDIRECTRIZ.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDIRECTRIZ.Enabled = False
        Me.txtDIRECTRIZ.Location = New System.Drawing.Point(16, 185)
        Me.txtDIRECTRIZ.Name = "txtDIRECTRIZ"
        Me.txtDIRECTRIZ.Size = New System.Drawing.Size(45, 22)
        Me.txtDIRECTRIZ.TabIndex = 39
        Me.txtDIRECTRIZ.Visible = False
        '
        'btnTODO
        '
        Me.btnTODO.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTODO.BackgroundImage = Global.TAVIL2020.My.Resources.Resources.copy32red
        Me.btnTODO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnTODO.Location = New System.Drawing.Point(258, 13)
        Me.btnTODO.Name = "btnTODO"
        Me.btnTODO.Size = New System.Drawing.Size(30, 30)
        Me.btnTODO.TabIndex = 33
        Me.btnTODO.Text = "·"
        Me.tt1.SetToolTip(Me.btnTODO, "Copiar TODO desde:")
        Me.btnTODO.UseVisualStyleBackColor = True
        '
        'cbHEIGHT
        '
        Me.cbHEIGHT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbHEIGHT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbHEIGHT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbHEIGHT.FormattingEnabled = True
        Me.cbHEIGHT.Location = New System.Drawing.Point(157, 111)
        Me.cbHEIGHT.Name = "cbHEIGHT"
        Me.cbHEIGHT.Size = New System.Drawing.Size(93, 24)
        Me.cbHEIGHT.TabIndex = 35
        Me.tt1.SetToolTip(Me.cbHEIGHT, "WIDTH / RADIO")
        '
        'cbWIDTH
        '
        Me.cbWIDTH.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbWIDTH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbWIDTH.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbWIDTH.FormattingEnabled = True
        Me.cbWIDTH.Location = New System.Drawing.Point(157, 69)
        Me.cbWIDTH.Name = "cbWIDTH"
        Me.cbWIDTH.Size = New System.Drawing.Size(94, 24)
        Me.cbWIDTH.TabIndex = 33
        Me.tt1.SetToolTip(Me.cbWIDTH, "WIDTH's")
        '
        'tt1
        '
        Me.tt1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.tt1.ToolTipTitle = "Información"
        '
        'btnActualizar
        '
        Me.btnActualizar.BackColor = System.Drawing.Color.Red
        Me.btnActualizar.Location = New System.Drawing.Point(213, 75)
        Me.btnActualizar.Name = "btnActualizar"
        Me.btnActualizar.Size = New System.Drawing.Size(128, 24)
        Me.btnActualizar.TabIndex = 32
        Me.btnActualizar.Text = "Actualizar Lista"
        Me.tt1.SetToolTip(Me.btnActualizar, "Actualizar Listado Patas")
        Me.btnActualizar.UseVisualStyleBackColor = False
        '
        'btnSeleccionar
        '
        Me.btnSeleccionar.Enabled = False
        Me.btnSeleccionar.Location = New System.Drawing.Point(213, 111)
        Me.btnSeleccionar.Name = "btnSeleccionar"
        Me.btnSeleccionar.Size = New System.Drawing.Size(128, 24)
        Me.btnSeleccionar.TabIndex = 33
        Me.btnSeleccionar.Text = "Seleccionar"
        Me.tt1.SetToolTip(Me.btnSeleccionar, "Seleccionar bloque en dibujo")
        Me.btnSeleccionar.UseVisualStyleBackColor = True
        '
        'cbXXX
        '
        Me.cbXXX.AutoSize = True
        Me.cbXXX.Checked = True
        Me.cbXXX.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbXXX.Location = New System.Drawing.Point(213, 11)
        Me.cbXXX.Name = "cbXXX"
        Me.cbXXX.Size = New System.Drawing.Size(89, 21)
        Me.cbXXX.TabIndex = 34
        Me.cbXXX.Text = "Sólo XXX"
        Me.tt1.SetToolTip(Me.cbXXX, "Solo con XXX en Atributos")
        Me.cbXXX.UseVisualStyleBackColor = True
        '
        'btnContar
        '
        Me.btnContar.Enabled = False
        Me.btnContar.Location = New System.Drawing.Point(213, 149)
        Me.btnContar.Name = "btnContar"
        Me.btnContar.Size = New System.Drawing.Size(128, 24)
        Me.btnContar.TabIndex = 37
        Me.btnContar.Text = "Contar"
        Me.tt1.SetToolTip(Me.btnContar, "Contar Bloques")
        Me.btnContar.UseVisualStyleBackColor = True
        '
        'btnListar
        '
        Me.btnListar.Location = New System.Drawing.Point(321, 202)
        Me.btnListar.Name = "btnListar"
        Me.btnListar.Size = New System.Drawing.Size(189, 24)
        Me.btnListar.TabIndex = 38
        Me.btnListar.Text = "Listado Bloques TODOS"
        Me.tt1.SetToolTip(Me.btnListar, "Contar Bloques")
        Me.btnListar.UseVisualStyleBackColor = True
        '
        'cbPLANTA
        '
        Me.cbPLANTA.AutoSize = True
        Me.cbPLANTA.Checked = True
        Me.cbPLANTA.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbPLANTA.Location = New System.Drawing.Point(213, 38)
        Me.cbPLANTA.Name = "cbPLANTA"
        Me.cbPLANTA.Size = New System.Drawing.Size(116, 21)
        Me.cbPLANTA.TabIndex = 39
        Me.cbPLANTA.Text = "Sólo PLANTA"
        Me.tt1.SetToolTip(Me.cbPLANTA, "Solo con XXX en Atributos")
        Me.cbPLANTA.UseVisualStyleBackColor = True
        '
        'pb1
        '
        Me.pb1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pb1.Location = New System.Drawing.Point(12, 546)
        Me.pb1.Name = "pb1"
        Me.pb1.Size = New System.Drawing.Size(189, 18)
        Me.pb1.TabIndex = 36
        '
        'frmPatas1
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.CERRAR_Button
        Me.ClientSize = New System.Drawing.Size(520, 568)
        Me.Controls.Add(Me.cbPLANTA)
        Me.Controls.Add(Me.btnListar)
        Me.Controls.Add(Me.btnContar)
        Me.Controls.Add(Me.pb1)
        Me.Controls.Add(Me.cbXXX)
        Me.Controls.Add(Me.btnSeleccionar)
        Me.Controls.Add(Me.btnActualizar)
        Me.Controls.Add(Me.gDatos)
        Me.Controls.Add(Me.lblListadoPatas)
        Me.Controls.Add(Me.tvPatas)
        Me.Controls.Add(Me.pbBloque)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPatas1"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "PATAS"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.pbBloque, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gDatos.ResumeLayout(False)
        Me.gDatos.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents CERRAR_Button As System.Windows.Forms.Button
    Friend WithEvents pbBloque As Windows.Forms.PictureBox
    Friend WithEvents tvPatas As Windows.Forms.TreeView
    Friend WithEvents lblListadoPatas As Windows.Forms.Label
    Friend WithEvents lblWIDTH As Windows.Forms.Label
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents txtCODE As Windows.Forms.TextBox
    Friend WithEvents cbSTANDARD As Windows.Forms.ComboBox
    Friend WithEvents btnWIDTH As Windows.Forms.Button
    Friend WithEvents btnHEIGHT As Windows.Forms.Button
    Friend WithEvents btnGUARDAR As Windows.Forms.Button
    Friend WithEvents gDatos As Windows.Forms.GroupBox
    Friend WithEvents tt1 As Windows.Forms.ToolTip
    Friend WithEvents btnActualizar As Windows.Forms.Button
    Friend WithEvents cbHEIGHT As Windows.Forms.ComboBox
    Friend WithEvents cbWIDTH As Windows.Forms.ComboBox
    Friend WithEvents btnTODO As Windows.Forms.Button
    Friend WithEvents btnSeleccionar As Windows.Forms.Button
    Friend WithEvents cbXXX As Windows.Forms.CheckBox
    Friend WithEvents pb1 As Windows.Forms.ProgressBar
    Friend WithEvents btnContar As Windows.Forms.Button
    Friend WithEvents btnListar As Windows.Forms.Button
    Friend WithEvents txtDIRECTRIZ1 As Windows.Forms.TextBox
    Friend WithEvents txtDIRECTRIZ As Windows.Forms.TextBox
    Friend WithEvents cbPLANTA As Windows.Forms.CheckBox
End Class
