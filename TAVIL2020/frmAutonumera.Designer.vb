<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAutonumera
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAutonumera))
        Me.btnInsertaNum = New System.Windows.Forms.Button()
        Me.btnCargarNumeracion = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnInsertaNum
        '
        Me.btnInsertaNum.Location = New System.Drawing.Point(23, 58)
        Me.btnInsertaNum.Name = "btnInsertaNum"
        Me.btnInsertaNum.Size = New System.Drawing.Size(205, 23)
        Me.btnInsertaNum.TabIndex = 3
        Me.btnInsertaNum.Text = "Insertar Numeración Desde Inicio"
        Me.btnInsertaNum.UseVisualStyleBackColor = True
        '
        'btnCargarNumeracion
        '
        Me.btnCargarNumeracion.Location = New System.Drawing.Point(23, 12)
        Me.btnCargarNumeracion.Name = "btnCargarNumeracion"
        Me.btnCargarNumeracion.Size = New System.Drawing.Size(205, 23)
        Me.btnCargarNumeracion.TabIndex = 4
        Me.btnCargarNumeracion.Text = "Cargar Numeración e Inserta"
        Me.btnCargarNumeracion.UseVisualStyleBackColor = True
        '
        'frmAutonumera
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(247, 94)
        Me.Controls.Add(Me.btnCargarNumeracion)
        Me.Controls.Add(Me.btnInsertaNum)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAutonumera"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Numeración Con Proxy"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnInsertaNum As Windows.Forms.Button
    Friend WithEvents btnCargarNumeracion As Windows.Forms.Button
End Class
