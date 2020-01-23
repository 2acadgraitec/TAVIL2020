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
        Me.UNION = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.UNITS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BtnAceptar = New System.Windows.Forms.Button()
        Me.BtnCancelar = New System.Windows.Forms.Button()
        Me.BtnCrearUnion = New System.Windows.Forms.Button()
        Me.BtnEditarUnion = New System.Windows.Forms.Button()
        Me.BtnBorrarUnion = New System.Windows.Forms.Button()
        Me.BtnSeleccionar = New System.Windows.Forms.Button()
        Me.cbCapa = New System.Windows.Forms.CheckBox()
        Me.BtnInsertaMultiplesUniones = New System.Windows.Forms.Button()
        Me.cbTipo = New System.Windows.Forms.ComboBox()
        Me.LblUniones = New System.Windows.Forms.Label()
        Me.BtnReportTodo = New System.Windows.Forms.Button()
        Me.BtnReportTotal = New System.Windows.Forms.Button()
        Me.PUnion = New System.Windows.Forms.Panel()
        Me.GUnion.SuspendLayout()
        CType(Me.DgvUnion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCerrar
        '
        Me.btnCerrar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Location = New System.Drawing.Point(806, 465)
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
        Me.BtnInsertarUnion.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnInsertarUnion.BackColor = System.Drawing.SystemColors.Control
        Me.BtnInsertarUnion.Location = New System.Drawing.Point(357, 76)
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
        Me.GUnion.Controls.Add(Me.Label2)
        Me.GUnion.Controls.Add(Me.BtnAceptar)
        Me.GUnion.Controls.Add(Me.BtnCancelar)
        Me.GUnion.Controls.Add(Me.BtnInsertarUnion)
        Me.GUnion.Location = New System.Drawing.Point(378, 277)
        Me.GUnion.Name = "GUnion"
        Me.GUnion.Size = New System.Drawing.Size(518, 132)
        Me.GUnion.TabIndex = 20
        Me.GUnion.TabStop = False
        '
        'DgvUnion
        '
        Me.DgvUnion.AllowUserToAddRows = False
        Me.DgvUnion.AllowUserToDeleteRows = False
        Me.DgvUnion.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DgvUnion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvUnion.ColumnHeadersVisible = False
        Me.DgvUnion.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.UNION, Me.UNITS})
        Me.DgvUnion.Location = New System.Drawing.Point(111, 16)
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
        'UNION
        '
        Me.UNION.Frozen = True
        Me.UNION.HeaderText = "UNION"
        Me.UNION.MinimumWidth = 6
        Me.UNION.Name = "UNION"
        Me.UNION.Width = 125
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
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.Location = New System.Drawing.Point(6, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 22)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "UNION/UNITS :"
        '
        'BtnAceptar
        '
        Me.BtnAceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnAceptar.BackColor = System.Drawing.SystemColors.Control
        Me.BtnAceptar.Location = New System.Drawing.Point(415, 17)
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
        Me.BtnCancelar.Location = New System.Drawing.Point(302, 17)
        Me.BtnCancelar.Name = "BtnCancelar"
        Me.BtnCancelar.Size = New System.Drawing.Size(74, 27)
        Me.BtnCancelar.TabIndex = 25
        Me.BtnCancelar.Text = "Cancelar"
        Me.BtnCancelar.UseVisualStyleBackColor = False
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
        'BtnReportTodo
        '
        Me.BtnReportTodo.Enabled = False
        Me.BtnReportTodo.Location = New System.Drawing.Point(178, 322)
        Me.BtnReportTodo.Name = "BtnReportTodo"
        Me.BtnReportTodo.Size = New System.Drawing.Size(71, 47)
        Me.BtnReportTodo.TabIndex = 29
        Me.BtnReportTodo.Text = "TODO"
        Me.BtnReportTodo.UseVisualStyleBackColor = True
        '
        'BtnReportTotal
        '
        Me.BtnReportTotal.Enabled = False
        Me.BtnReportTotal.Location = New System.Drawing.Point(279, 322)
        Me.BtnReportTotal.Name = "BtnReportTotal"
        Me.BtnReportTotal.Size = New System.Drawing.Size(70, 47)
        Me.BtnReportTotal.TabIndex = 30
        Me.BtnReportTotal.Text = "TOTAL"
        Me.BtnReportTotal.UseVisualStyleBackColor = True
        '
        'PUnion
        '
        Me.PUnion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PUnion.Location = New System.Drawing.Point(378, 13)
        Me.PUnion.Margin = New System.Windows.Forms.Padding(0)
        Me.PUnion.MaximumSize = New System.Drawing.Size(516, 258)
        Me.PUnion.MinimumSize = New System.Drawing.Size(516, 258)
        Me.PUnion.Name = "PUnion"
        Me.PUnion.Size = New System.Drawing.Size(516, 258)
        Me.PUnion.TabIndex = 31
        '
        'frmUniones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(908, 503)
        Me.Controls.Add(Me.PUnion)
        Me.Controls.Add(Me.BtnReportTotal)
        Me.Controls.Add(Me.BtnReportTodo)
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
    Friend WithEvents DgvUnion As Windows.Forms.DataGridView
    Friend WithEvents UNION As Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents UNITS As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BtnReportTodo As Windows.Forms.Button
    Friend WithEvents BtnReportTotal As Windows.Forms.Button
    Friend WithEvents PUnion As Windows.Forms.Panel
End Class
