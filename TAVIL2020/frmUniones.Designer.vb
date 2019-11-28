<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUniones
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUniones))
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.tvUniones = New System.Windows.Forms.TreeView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbZoom = New System.Windows.Forms.CheckBox()
        Me.BtnInsertarUnion = New System.Windows.Forms.Button()
        Me.GUnion = New System.Windows.Forms.GroupBox()
        Me.DgvUnion = New System.Windows.Forms.DataGridView()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LbRotation = New System.Windows.Forms.ListBox()
        Me.LbInclinationT2 = New System.Windows.Forms.ListBox()
        Me.LbInclinationT1 = New System.Windows.Forms.ListBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BtnAceptar = New System.Windows.Forms.Button()
        Me.BtnCancelar = New System.Windows.Forms.Button()
        Me.LblT2 = New System.Windows.Forms.Label()
        Me.LblT1 = New System.Windows.Forms.Label()
        Me.BtnT2 = New System.Windows.Forms.Button()
        Me.BtnT1 = New System.Windows.Forms.Button()
        Me.BtnCrearUnion = New System.Windows.Forms.Button()
        Me.BtnEditarUnion = New System.Windows.Forms.Button()
        Me.BtnBorrarUnion = New System.Windows.Forms.Button()
        Me.BtnSeleccionar = New System.Windows.Forms.Button()
        Me.cbCapa = New System.Windows.Forms.CheckBox()
        Me.BtnInsertaMultiplesUniones = New System.Windows.Forms.Button()
        Me.cbTipo = New System.Windows.Forms.ComboBox()
        Me.LblUniones = New System.Windows.Forms.Label()
        Me.UNION = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.UNITS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GUnion.SuspendLayout()
        CType(Me.DgvUnion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCerrar
        '
        Me.btnCerrar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Location = New System.Drawing.Point(659, 465)
        Me.btnCerrar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(89, 28)
        Me.btnCerrar.TabIndex = 1
        Me.btnCerrar.Text = "Cerrar"
        '
        'tvUniones
        '
        Me.tvUniones.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tvUniones.Cursor = System.Windows.Forms.Cursors.Hand
        Me.tvUniones.HideSelection = False
        Me.tvUniones.HotTracking = True
        Me.tvUniones.Location = New System.Drawing.Point(12, 58)
        Me.tvUniones.Name = "tvUniones"
        Me.tvUniones.Size = New System.Drawing.Size(145, 413)
        Me.tvUniones.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 17)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Uniones :"
        '
        'cbZoom
        '
        Me.cbZoom.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cbZoom.AutoSize = True
        Me.cbZoom.Checked = True
        Me.cbZoom.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbZoom.Location = New System.Drawing.Point(12, 477)
        Me.cbZoom.Name = "cbZoom"
        Me.cbZoom.Size = New System.Drawing.Size(122, 21)
        Me.cbZoom.TabIndex = 11
        Me.cbZoom.Text = "Zoom Uniones"
        Me.cbZoom.UseVisualStyleBackColor = True
        '
        'BtnInsertarUnion
        '
        Me.BtnInsertarUnion.BackColor = System.Drawing.SystemColors.Control
        Me.BtnInsertarUnion.Location = New System.Drawing.Point(123, 336)
        Me.BtnInsertarUnion.Name = "BtnInsertarUnion"
        Me.BtnInsertarUnion.Size = New System.Drawing.Size(132, 45)
        Me.BtnInsertarUnion.TabIndex = 19
        Me.BtnInsertarUnion.Text = "Insertar Unión"
        Me.BtnInsertarUnion.UseVisualStyleBackColor = False
        '
        'GUnion
        '
        Me.GUnion.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GUnion.Controls.Add(Me.DgvUnion)
        Me.GUnion.Controls.Add(Me.Label4)
        Me.GUnion.Controls.Add(Me.LbRotation)
        Me.GUnion.Controls.Add(Me.LbInclinationT2)
        Me.GUnion.Controls.Add(Me.LbInclinationT1)
        Me.GUnion.Controls.Add(Me.Label2)
        Me.GUnion.Controls.Add(Me.BtnAceptar)
        Me.GUnion.Controls.Add(Me.BtnCancelar)
        Me.GUnion.Controls.Add(Me.LblT2)
        Me.GUnion.Controls.Add(Me.LblT1)
        Me.GUnion.Controls.Add(Me.BtnT2)
        Me.GUnion.Controls.Add(Me.BtnT1)
        Me.GUnion.Controls.Add(Me.BtnInsertarUnion)
        Me.GUnion.Location = New System.Drawing.Point(368, 29)
        Me.GUnion.Name = "GUnion"
        Me.GUnion.Size = New System.Drawing.Size(381, 387)
        Me.GUnion.TabIndex = 20
        Me.GUnion.TabStop = False
        Me.GUnion.Text = "Crear / Editar Unión"
        '
        'DgvUnion
        '
        Me.DgvUnion.AllowUserToAddRows = False
        Me.DgvUnion.AllowUserToDeleteRows = False
        Me.DgvUnion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvUnion.ColumnHeadersVisible = False
        Me.DgvUnion.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.UNION, Me.UNITS})
        Me.DgvUnion.Location = New System.Drawing.Point(13, 226)
        Me.DgvUnion.MaximumSize = New System.Drawing.Size(175, 104)
        Me.DgvUnion.MinimumSize = New System.Drawing.Size(175, 104)
        Me.DgvUnion.Name = "DgvUnion"
        Me.DgvUnion.RowHeadersVisible = False
        Me.DgvUnion.RowHeadersWidth = 51
        Me.DgvUnion.RowTemplate.Height = 24
        Me.DgvUnion.ShowEditingIcon = False
        Me.DgvUnion.Size = New System.Drawing.Size(175, 104)
        Me.DgvUnion.TabIndex = 37
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(183, 194)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(94, 22)
        Me.Label4.TabIndex = 35
        Me.Label4.Text = "ROTATION :"
        '
        'LbRotation
        '
        Me.LbRotation.FormattingEnabled = True
        Me.LbRotation.ItemHeight = 16
        Me.LbRotation.Items.AddRange(New Object() {"0", "90"})
        Me.LbRotation.Location = New System.Drawing.Point(309, 187)
        Me.LbRotation.Name = "LbRotation"
        Me.LbRotation.Size = New System.Drawing.Size(66, 36)
        Me.LbRotation.TabIndex = 34
        '
        'LbInclinationT2
        '
        Me.LbInclinationT2.FormattingEnabled = True
        Me.LbInclinationT2.ItemHeight = 16
        Me.LbInclinationT2.Items.AddRange(New Object() {"FLAT", "DOWN", "UP"})
        Me.LbInclinationT2.Location = New System.Drawing.Point(309, 108)
        Me.LbInclinationT2.Name = "LbInclinationT2"
        Me.LbInclinationT2.Size = New System.Drawing.Size(66, 52)
        Me.LbInclinationT2.TabIndex = 31
        '
        'LbInclinationT1
        '
        Me.LbInclinationT1.FormattingEnabled = True
        Me.LbInclinationT1.ItemHeight = 16
        Me.LbInclinationT1.Items.AddRange(New Object() {"FLAT", "DOWN", "UP"})
        Me.LbInclinationT1.Location = New System.Drawing.Point(309, 29)
        Me.LbInclinationT1.Name = "LbInclinationT1"
        Me.LbInclinationT1.Size = New System.Drawing.Size(66, 52)
        Me.LbInclinationT1.TabIndex = 30
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(10, 201)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 22)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "UNION/UNITS :"
        '
        'BtnAceptar
        '
        Me.BtnAceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnAceptar.BackColor = System.Drawing.SystemColors.Control
        Me.BtnAceptar.Location = New System.Drawing.Point(301, 354)
        Me.BtnAceptar.Name = "BtnAceptar"
        Me.BtnAceptar.Size = New System.Drawing.Size(74, 27)
        Me.BtnAceptar.TabIndex = 26
        Me.BtnAceptar.Text = "Aceptar"
        Me.BtnAceptar.UseVisualStyleBackColor = False
        '
        'BtnCancelar
        '
        Me.BtnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnCancelar.BackColor = System.Drawing.SystemColors.Control
        Me.BtnCancelar.Location = New System.Drawing.Point(8, 353)
        Me.BtnCancelar.Name = "BtnCancelar"
        Me.BtnCancelar.Size = New System.Drawing.Size(74, 27)
        Me.BtnCancelar.TabIndex = 25
        Me.BtnCancelar.Text = "Cancelar"
        Me.BtnCancelar.UseVisualStyleBackColor = False
        '
        'LblT2
        '
        Me.LblT2.Location = New System.Drawing.Point(154, 108)
        Me.LblT2.Name = "LblT2"
        Me.LblT2.Size = New System.Drawing.Size(149, 52)
        Me.LblT2.TabIndex = 23
        Me.LblT2.Text = "Datos T2:"
        '
        'LblT1
        '
        Me.LblT1.Location = New System.Drawing.Point(154, 29)
        Me.LblT1.Name = "LblT1"
        Me.LblT1.Size = New System.Drawing.Size(149, 52)
        Me.LblT1.TabIndex = 22
        Me.LblT1.Text = "Datos T1:"
        '
        'BtnT2
        '
        Me.BtnT2.Location = New System.Drawing.Point(13, 108)
        Me.BtnT2.Name = "BtnT2"
        Me.BtnT2.Size = New System.Drawing.Size(132, 41)
        Me.BtnT2.TabIndex = 21
        Me.BtnT2.Text = "Transportador 2"
        Me.BtnT2.UseVisualStyleBackColor = True
        '
        'BtnT1
        '
        Me.BtnT1.Location = New System.Drawing.Point(13, 29)
        Me.BtnT1.Name = "BtnT1"
        Me.BtnT1.Size = New System.Drawing.Size(132, 41)
        Me.BtnT1.TabIndex = 20
        Me.BtnT1.Text = "Transportador 1"
        Me.BtnT1.UseVisualStyleBackColor = True
        '
        'BtnCrearUnion
        '
        Me.BtnCrearUnion.Location = New System.Drawing.Point(178, 39)
        Me.BtnCrearUnion.Name = "BtnCrearUnion"
        Me.BtnCrearUnion.Size = New System.Drawing.Size(171, 32)
        Me.BtnCrearUnion.TabIndex = 21
        Me.BtnCrearUnion.Text = "Crear Unión"
        Me.BtnCrearUnion.UseVisualStyleBackColor = True
        '
        'BtnEditarUnion
        '
        Me.BtnEditarUnion.Location = New System.Drawing.Point(178, 91)
        Me.BtnEditarUnion.Name = "BtnEditarUnion"
        Me.BtnEditarUnion.Size = New System.Drawing.Size(171, 32)
        Me.BtnEditarUnion.TabIndex = 22
        Me.BtnEditarUnion.Text = "Editar Unión"
        Me.BtnEditarUnion.UseVisualStyleBackColor = True
        '
        'BtnBorrarUnion
        '
        Me.BtnBorrarUnion.Location = New System.Drawing.Point(178, 143)
        Me.BtnBorrarUnion.Name = "BtnBorrarUnion"
        Me.BtnBorrarUnion.Size = New System.Drawing.Size(171, 32)
        Me.BtnBorrarUnion.TabIndex = 23
        Me.BtnBorrarUnion.Text = "Borrar Unión"
        Me.BtnBorrarUnion.UseVisualStyleBackColor = True
        '
        'BtnSeleccionar
        '
        Me.BtnSeleccionar.Enabled = False
        Me.BtnSeleccionar.Location = New System.Drawing.Point(178, 195)
        Me.BtnSeleccionar.Name = "BtnSeleccionar"
        Me.BtnSeleccionar.Size = New System.Drawing.Size(171, 32)
        Me.BtnSeleccionar.TabIndex = 24
        Me.BtnSeleccionar.Text = "Seleccionar en Dibujo"
        Me.BtnSeleccionar.UseVisualStyleBackColor = True
        '
        'cbCapa
        '
        Me.cbCapa.AutoSize = True
        Me.cbCapa.Location = New System.Drawing.Point(178, 388)
        Me.cbCapa.Name = "cbCapa"
        Me.cbCapa.Size = New System.Drawing.Size(156, 21)
        Me.cbCapa.TabIndex = 25
        Me.cbCapa.Text = "Capa UNION Visible"
        Me.cbCapa.UseVisualStyleBackColor = True
        '
        'BtnInsertaMultiplesUniones
        '
        Me.BtnInsertaMultiplesUniones.BackColor = System.Drawing.SystemColors.Control
        Me.BtnInsertaMultiplesUniones.Location = New System.Drawing.Point(178, 247)
        Me.BtnInsertaMultiplesUniones.Name = "BtnInsertaMultiplesUniones"
        Me.BtnInsertaMultiplesUniones.Size = New System.Drawing.Size(171, 45)
        Me.BtnInsertaMultiplesUniones.TabIndex = 26
        Me.BtnInsertaMultiplesUniones.Text = "Insertar Multiples Uniones"
        Me.BtnInsertaMultiplesUniones.UseVisualStyleBackColor = False
        Me.BtnInsertaMultiplesUniones.Visible = False
        '
        'cbTipo
        '
        Me.cbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTipo.FormattingEnabled = True
        Me.cbTipo.Items.AddRange(New Object() {"TODOS", "XXX"})
        Me.cbTipo.Location = New System.Drawing.Point(80, 7)
        Me.cbTipo.Name = "cbTipo"
        Me.cbTipo.Size = New System.Drawing.Size(77, 24)
        Me.cbTipo.TabIndex = 27
        '
        'LblUniones
        '
        Me.LblUniones.AutoSize = True
        Me.LblUniones.Location = New System.Drawing.Point(12, 37)
        Me.LblUniones.Name = "LblUniones"
        Me.LblUniones.Size = New System.Drawing.Size(12, 17)
        Me.LblUniones.TabIndex = 28
        Me.LblUniones.Text = "."
        '
        'UNION
        '
        Me.UNION.Frozen = True
        Me.UNION.HeaderText = "UNION"
        Me.UNION.MinimumWidth = 6
        Me.UNION.Name = "UNION"
        '
        'UNITS
        '
        Me.UNITS.Frozen = True
        Me.UNITS.HeaderText = "UNITS"
        Me.UNITS.MinimumWidth = 6
        Me.UNITS.Name = "UNITS"
        Me.UNITS.ReadOnly = True
        Me.UNITS.Width = 50
        '
        'frmUniones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(761, 503)
        Me.Controls.Add(Me.LblUniones)
        Me.Controls.Add(Me.cbTipo)
        Me.Controls.Add(Me.BtnInsertaMultiplesUniones)
        Me.Controls.Add(Me.cbCapa)
        Me.Controls.Add(Me.BtnSeleccionar)
        Me.Controls.Add(Me.BtnBorrarUnion)
        Me.Controls.Add(Me.BtnEditarUnion)
        Me.Controls.Add(Me.BtnCrearUnion)
        Me.Controls.Add(Me.GUnion)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.cbZoom)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tvUniones)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmUniones"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmUniones"
        Me.GUnion.ResumeLayout(False)
        CType(Me.DgvUnion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents tvUniones As Windows.Forms.TreeView
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents cbZoom As Windows.Forms.CheckBox
    Friend WithEvents BtnInsertarUnion As Windows.Forms.Button
    Friend WithEvents GUnion As Windows.Forms.GroupBox
    Friend WithEvents BtnT2 As Windows.Forms.Button
    Friend WithEvents BtnT1 As Windows.Forms.Button
    Friend WithEvents LblT2 As Windows.Forms.Label
    Friend WithEvents LblT1 As Windows.Forms.Label
    Friend WithEvents BtnCrearUnion As Windows.Forms.Button
    Friend WithEvents BtnEditarUnion As Windows.Forms.Button
    Friend WithEvents BtnBorrarUnion As Windows.Forms.Button
    Friend WithEvents BtnCancelar As Windows.Forms.Button
    Friend WithEvents BtnSeleccionar As Windows.Forms.Button
    Friend WithEvents cbCapa As Windows.Forms.CheckBox
    Friend WithEvents BtnAceptar As Windows.Forms.Button
    Friend WithEvents BtnInsertaMultiplesUniones As Windows.Forms.Button
    Friend WithEvents cbTipo As Windows.Forms.ComboBox
    Friend WithEvents LblUniones As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents LbInclinationT2 As Windows.Forms.ListBox
    Friend WithEvents LbInclinationT1 As Windows.Forms.ListBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents LbRotation As Windows.Forms.ListBox
    Friend WithEvents DgvUnion As Windows.Forms.DataGridView
    Friend WithEvents UNION As Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents UNITS As Windows.Forms.DataGridViewTextBoxColumn
End Class
