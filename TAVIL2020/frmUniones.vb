Imports Autodesk.AutoCAD.Interop.Common
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.ApplicationServices
Imports TAVIL2020.TAVIL2020
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Linq
Imports a2 = AutoCAD2acad.A2acad

Public Class frmUniones
    Private UltimoBloqueUnion As AcadBlockReference = Nothing
    Private UltimoBloqueT1 As AcadBlockReference = Nothing
    Private UltimoBloqueT2 As AcadBlockReference = Nothing
    Private UClsUnion As ClsUnion = Nothing
    Public ActualFilaExcel As UNIONESFila = Nothing
    Private HighlightedPictureBox As PictureBox = Nothing
    Private oTT As ToolTip = Nothing
    Private capaUnionesVisible As Boolean = True
    Private EsUnionNueva As Boolean = False

    Private Sub frmUniones_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Eventos.SYSMONVAR(True)
        Me.Text = "UNIONES - v" & appVersion
        ' Cargar recursos
        Using oLock As DocumentLock = Eventos.AXDoc.LockDocument
            'clsA.ClonaTODODesdeDWG(BloqueRecursos, True, True)
            'clsA.ClonaTODODesdeDWG(IO.Path.Combine(IO.Path.GetDirectoryName(BloqueRecursos), "UNION.dwg"), True, True)
            'clsA.ClonaBloqueDesdeDWG(IO.Path.Combine(IO.Path.GetDirectoryName(BloqueRecursos), "UNION.dwg"), "UNION")
            clsA.Clona_UnBloqueDesdeDWG(BloqueRecursos, "UNION")
        End Using
        PonToolTipControles()
        Dim queCapa As AcadLayer = clsA.CapaDame(CapaUniones)
        If queCapa IsNot Nothing Then
            capaUnionesVisible = queCapa.LayerOn
            queCapa.LayerOn = True
            queCapa.Lock = False
            queCapa.Freeze = False
            cbCapa.Checked = capaUnionesVisible
        End If
        clsA.SelectionSet_Delete()
        cbTipo.Text = "TODOS"
    End Sub

    Private Sub frmUniones_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Eventos.SYSMONVAR()
        If cbCapa.Enabled = True Then
            Dim queCapa As AcadLayer = clsA.CapaDame(CapaUniones)
            If queCapa IsNot Nothing Then
                queCapa.LayerOn = capaUnionesVisible
                queCapa.Freeze = Not (cbCapa.Checked)
            End If
        End If
        frmUn = Nothing
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    '
    ' CONTROLES
    Private Sub cbTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbTipo.SelectedIndexChanged
        tvUniones_Rellena(cbTipo.SelectedIndex)
    End Sub
    '
    Private Sub tvUniones_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tvUniones.AfterSelect
        clsA.Seleccion_Quitar()
        UltimoBloqueT1 = Nothing
        UltimoBloqueT2 = Nothing
        UltimoBloqueUnion = Nothing
        UClsUnion = Nothing
        If tvUniones.SelectedNode Is Nothing Then
            PonEstadoControlesInicial()
            Exit Sub
        End If
        BtnEditarUnion.Enabled = True
        BtnBorrarUnion.Enabled = True
        Me.Uniones_SeleccionarObjetos(tvUniones.SelectedNode.Tag.ToString)
        ' Mostrar los datos en sus controles.
        PonDatosUnion(tvUniones.SelectedNode.Tag.ToString)
    End Sub

    Private Sub PonDatosUnion(Optional handle As String = "")
        BorraDatos()
        If tvUniones.SelectedNode Is Nothing Then Exit Sub
        If handle = "" Then handle = tvUniones.SelectedNode.Tag.ToString
        If handle = "" Then Exit Sub
        If IsNumeric(handle) Then Exit Sub
        '
        Try
            UltimoBloqueUnion = Eventos.COMDoc().HandleToObject(handle)
        Catch ex As Exception
            Exit Sub
        End Try
        '
        Dim lUniHANLE As IEnumerable(Of ClsUnion) = From x In LClsUnion
                                                    Where x.UnionHANDLE = handle
                                                    Select x
        If lUniHANLE.Count > 0 Then
            UClsUnion = lUniHANLE.First
        Else
            Exit Sub
        End If
        '
        ' T1
        If UClsUnion.T1HANDLE <> "" Then
            Try
                UltimoBloqueT1 = Eventos.COMDoc().HandleToObject(UClsUnion.T1HANDLE)
                If UClsUnion.T1INCLINATION <> "" Then ListBox_SeleccionaPorTexto(LbInclinationT1, UClsUnion.T1INCLINATION)
                LblT1.Text = "Datos T1:" & vbCrLf & UltimoBloqueT1.EffectiveName & vbCrLf & UClsUnion.T1INFEED
            Catch ex As Exception
            End Try
        End If
        ' T2
        If UClsUnion.T2HANDLE <> "" Then
            Try
                UltimoBloqueT2 = Eventos.COMDoc().HandleToObject(UClsUnion.T2HANDLE)
                If UClsUnion.T2INCLINATION <> "" Then ListBox_SeleccionaPorTexto(LbInclinationT2, UClsUnion.T2INCLINATION)
                LblT2.Text = "Datos T2:" & vbCrLf & UltimoBloqueT2.EffectiveName & vbCrLf & UClsUnion.T2OUTFEED
            Catch ex As Exception
            End Try
        End If
        '
        If UClsUnion.ANGLE = "0" OrElse UClsUnion.ANGLE = "" Then
            LbRotation.SelectedIndex = 0
        ElseIf UClsUnion.ANGLE = "90" Then
            LbRotation.SelectedIndex = 1
        Else
            LbRotation.SelectedIndex = -1
        End If
        '
        DgvUnion.Rows.Clear()
        If UClsUnion.ExcelFilaUnion IsNot Nothing AndAlso
            UClsUnion.ExcelFilaUnion.RowsL IsNot Nothing _
            AndAlso UClsUnion.ExcelFilaUnion.RowsL.Count > 0 Then
            DgvUnion.Rows.AddRange(UClsUnion.ExcelFilaUnion.RowsL(UClsUnion.ExcelFilaUnion.LADO).ToArray)
        End If
        ' LADO
        ListBox_SeleccionaPorTexto(LbLado, UClsUnion.LADO)
    End Sub
    Private Sub tvUniones_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles tvUniones.MouseDoubleClick
        Uniones_SeleccionarObjetos(tvUniones.SelectedNode.Tag, conZoom:=True)
    End Sub
    Private Sub BtnCrearUnion_Click(sender As Object, e As EventArgs) Handles BtnCrearUnion.Click
        clsA.Seleccion_Quitar()
        EsUnionNueva = True
        BorraDatos()
        UltimoBloqueT1 = Nothing
        UltimoBloqueT2 = Nothing
        UltimoBloqueUnion = Nothing
        UClsUnion = Nothing
        '
        GUnion.Enabled = True
        BtnT1.Enabled = True
        BtnT2.Enabled = True
        BtnInsertarUnion.Visible = True
        BtnInsertarUnion.Enabled = False
        BtnAceptar.Visible = False
        BtnAceptar.Enabled = False
    End Sub

    Private Sub BtnEditarUnion_Click(sender As Object, e As EventArgs) Handles BtnEditarUnion.Click
        If tvUniones.SelectedNode Is Nothing Then Exit Sub
        '
        EsUnionNueva = False
        GUnion.Enabled = True
        BtnInsertarUnion.Visible = False
        BtnInsertarUnion.Enabled = False
        BtnAceptar.Visible = True
        BtnAceptar.Enabled = True
        Datos_1PonUnion()
        Datos_2CompruebaDatos()
    End Sub
    Private Sub BtnBorrarUnion_Click(sender As Object, e As EventArgs) Handles BtnBorrarUnion.Click
        clsA.Seleccion_Quitar()
        Dim oBl As AcadBlockReference = Eventos.COMDoc().HandleToObject(tvUniones.SelectedNode.Tag)
        If oBl Is Nothing Then Exit Sub
        '
        oBl.Delete()
        tvUniones.SelectedNode.Remove()
        tvUniones.SelectedNode = Nothing
        tvUniones_AfterSelect(Nothing, Nothing)
        BtnSeleccionar.Enabled = (tvUniones.Nodes.Count > 0)
    End Sub
    Private Sub BtnSeleccionar_Click(sender As Object, e As EventArgs) Handles BtnSeleccionar.Click
        Dim oBl As AcadBlockReference = clsA.Bloque_SeleccionaDame
        If oBl Is Nothing Then Exit Sub
        '
        tvUniones.SelectedNode = Nothing
        For Each oNode As TreeNode In tvUniones.Nodes
            If oNode.Tag.ToString = oBl.Handle Then
                tvUniones.SelectedNode = oNode
                tvUniones_MouseDoubleClick(tvUniones, Nothing)  ' Hacer Zoom en el elemento.
                Exit For
            End If
        Next
    End Sub
    Private Sub CbCapa_CheckedChanged(sender As Object, e As EventArgs) Handles cbCapa.CheckedChanged
        Dim queCapa As AcadLayer = clsA.CapaDame(CapaUniones)
        If queCapa IsNot Nothing Then
            queCapa.LayerOn = cbCapa.Checked
            queCapa.Freeze = Not (cbCapa.Checked)
            Eventos.COMDoc().Regen(AcRegenType.acActiveViewport)
            PonEstadoControlesInicial()
            tvUniones.Enabled = cbCapa.Checked
            BtnCrearUnion.Enabled = cbCapa.Checked
            BtnSeleccionar.Enabled = cbCapa.Checked
        End If
        BtnCancelar_Click(Nothing, Nothing)
    End Sub
    Private Sub BtnInsertaMultiplesUniones_Click(sender As Object, e As EventArgs) Handles BtnInsertaMultiplesUniones.Click
        Me.Visible = False
        clsA.ActivaAppAPI()
        Dim oBlr As AcadBlockReference = Nothing
        Try
            Do
                oBlr = clsA.Bloque_InsertaMultiple(, NombreBloqueUNION)
                'If oBlr IsNot Nothing Then
                '    'tvUniones_PonNodo(oBlr.ObjectID)
                '    'tvUniones.SelectedNode = Nothing
                '    'tvUniones_AfterSelect(Nothing, Nothing)
                'End If
            Loop While oBlr IsNot Nothing
        Catch ex As Exception
            Debug.Print(ex.ToString)
        End Try
        tvUniones_Rellena()
        Me.Visible = True
    End Sub

    Private Sub BtnT1_Click(sender As Object, e As EventArgs) Handles BtnT1.Click
        Me.Visible = False
        clsA.ActivaAppAPI()
        UltimoBloqueT1 = clsA.Bloque_SeleccionaDame
        'If UltimoBloqueT1 IsNot Nothing AndAlso UltimoBloqueUnion IsNot Nothing Then
        '    Datos_1PonUnion()
        'End If
        Me.Visible = True
        LbT1_SelectedIndexChanged(Nothing, Nothing)
    End Sub
    Private Sub BtnT2_Click(sender As Object, e As EventArgs) Handles BtnT2.Click
        Me.Visible = False
        clsA.ActivaAppAPI()
        UltimoBloqueT2 = clsA.Bloque_SeleccionaDame
        'If UltimoBloqueT2 IsNot Nothing AndAlso UltimoBloqueUnion IsNot Nothing Then
        '    Datos_1PonUnion()
        'End If
        Me.Visible = True
        LbT1_SelectedIndexChanged(Nothing, Nothing)
    End Sub


    Private Sub LbT1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LbInclinationT1.SelectedIndexChanged, LbInclinationT2.SelectedIndexChanged, LbRotation.SelectedIndexChanged, LbLado.SelectedIndexChanged
        If GUnion.Enabled = False Then Exit Sub
        '
        If UltimoBloqueT1 IsNot Nothing AndAlso
            UltimoBloqueT2 IsNot Nothing AndAlso
            LbInclinationT1.SelectedIndex >= 0 AndAlso
            LbInclinationT2.SelectedIndex >= 0 AndAlso
            LbRotation.SelectedIndex >= 0 Then
            Datos_1PonUnion()   ' Rellenar datos union y ejecutar las 2 comprobaciones siguientes.
        Else
            DgvUnion.Rows.Clear()
            Datos_2CompruebaDatos()
        End If
    End Sub
    Private Sub BtnInsertarUnion_Click(sender As Object, e As EventArgs) Handles BtnInsertarUnion.Click
        clsA.ActivaAppAPI()
        If UltimoBloqueUnion Is Nothing Then
            clsA.Bloque_Inserta(, NombreBloqueUNION)
            If clsA.oBlult IsNot Nothing Then
                UltimoBloqueUnion = Eventos.COMDoc.HandleToObject(clsA.oBlult.Handle)
                BtnAceptar_Click(Nothing, Nothing)
            End If
        End If
    End Sub

    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        UltimoBloqueT1 = Nothing
        UltimoBloqueT2 = Nothing
        UltimoBloqueUnion = Nothing
        UClsUnion = Nothing
        tvUniones.SelectedNode = Nothing
        tvUniones_AfterSelect(Nothing, Nothing)
        BorraDatos()
    End Sub
    Public Sub BtnAceptar_Click(sender As Object, e As EventArgs) Handles BtnAceptar.Click
        ' Coger UNION y UNITS del DataGrigView
        Dim DatosUnion As New List(Of String)
        Dim DatosUnits As New List(Of String)
        For Each oRow As DataGridViewRow In DgvUnion.Rows
            If oRow.Cells.Item("UNION").Value Is Nothing OrElse oRow.Cells.Item("UNION").Value.ToString = "" Then Continue For
            DatosUnion.Add(oRow.Cells.Item("UNION").Value.ToString)
            DatosUnits.Add(oRow.Cells.Item("UNITS").Value.ToString)
        Next
        If DatosUnion.Count = 0 Then Exit Sub
        '
        If UClsUnion IsNot Nothing Then
            UClsUnion.ExcelFilaUnion = ActualFilaExcel
            UClsUnion.T1HANDLE = UltimoBloqueT1.Handle
            UClsUnion.T1INCLINATION = LbInclinationT1.Text
            UClsUnion.T1INFEED = ActualFilaExcel.INFEED_CONVEYOR
            UClsUnion.T2HANDLE = UltimoBloqueT2.Handle
            UClsUnion.T2INCLINATION = LbInclinationT2.Text
            UClsUnion.T2OUTFEED = ActualFilaExcel.OUTFEED_CONVEYOR
            UClsUnion.UNION = String.Join(";", DatosUnion.ToArray)
            UClsUnion.UNITS = String.Join(";", DatosUnits.ToArray)
            UClsUnion.ANGLE = LbRotation.Text
            UClsUnion.UNIONFin_Pon(Me.DgvUnion)   ' Escribir finalmente los atributos UNION y UNITS en el bloque.
            UClsUnion.LADO = LbLado.Text
        Else
            UClsUnion = New ClsUnion(
                UltimoBloqueUnion.Handle,
                String.Join(";", DatosUnion.ToArray),
                String.Join(";", DatosUnits.ToArray),
                UltimoBloqueT1.Handle,
                ActualFilaExcel.INFEED_CONVEYOR,
                LbInclinationT1.Text,
                UltimoBloqueT2.Handle,
                ActualFilaExcel.OUTFEED_CONVEYOR,
                LbInclinationT2.Text,
                LbRotation.Text,
                LbLado.Text)
            UClsUnion.UNIONFin_Pon(Me.DgvUnion)
        End If
        '
        UltimoBloqueT1 = Nothing
        UltimoBloqueT2 = Nothing
        UltimoBloqueUnion = Nothing
        UClsUnion = Nothing
        ActualFilaExcel = Nothing
        tvUniones.SelectedNode = Nothing
        tvUniones_AfterSelect(Nothing, Nothing)
        tvUniones_Rellena()
    End Sub
#Region "FUNCIONES"
    Public Sub PonEstadoControlesInicial()
        ' Estado inicila de GUniones (Con todos los controles de selección y edición)
        tvUniones.SelectedNode = Nothing
        GUnion.Enabled = False
        BtnCrearUnion.Enabled = True
        BtnEditarUnion.Enabled = False
        BtnBorrarUnion.Enabled = False
        GUnion.Enabled = False
        BtnT1.Enabled = False : BtnT1.BackColor = btnOn
        LblT1.Text = "Datos T1: "
        BtnT2.Enabled = False : BtnT2.BackColor = btnOn
        LblT2.Text = "Datos T2: "
        BtnInsertarUnion.Enabled = False : BtnInsertarUnion.BackColor = btnOn
        BorraDatos()
    End Sub
    Public Sub PonToolTipControles()
        oTT = New ToolTip()
        oTT.AutoPopDelay = 5000 ' Tiempo que estará visible
        oTT.InitialDelay = 500  ' Tiempo inicial para mostrarse
        oTT.ReshowDelay = 100   ' Tiempo de espera entre controles
        oTT.ShowAlways = True   ' Forzar a que se muestre el tooltip, aunque no este activo el Form.
        '
        Me.tvUniones.ShowNodeToolTips = True
        oTT.SetToolTip(Me.tvUniones, "Listado de uniones")
        oTT.SetToolTip(Me.cbZoom, "Zoom uniones")
        '
        oTT.SetToolTip(Me.BtnCrearUnion, "Crear nueva unión")
        oTT.SetToolTip(Me.BtnEditarUnion, "Editar unión seleccionada")
        oTT.SetToolTip(Me.BtnBorrarUnion, "Borrar unión seleccionada")
        oTT.SetToolTip(Me.BtnSeleccionar, "Seleccionar bloque UNION en Dibujo")
        oTT.SetToolTip(Me.BtnBorrarUnion, "Mostra/Ocultar capa UNIONES")
        oTT.SetToolTip(Me.BtnInsertaMultiplesUniones, "Insertar múltiples bloques '" & NombreBloqueUNION & "'")
        '
        oTT.SetToolTip(Me.BtnT1, "Seleccionar Transportador 1")
        oTT.SetToolTip(Me.BtnT2, "Seleccionar Transportador 2")
        oTT.SetToolTip(Me.BtnInsertarUnion, "Insertar o Mover Unión")
        oTT.SetToolTip(Me.btnCerrar, "Cerrar Uniones")
        oTT.SetToolTip(Me.BtnAceptar, "Aceptar unión y escribir atributos")
    End Sub

    Public Sub Datos_1PonUnion()
        If GUnion.Enabled = False Then Exit Sub
        If UltimoBloqueT1 Is Nothing OrElse UltimoBloqueT2 Is Nothing Then Exit Sub
        DgvUnion.Rows.Clear()
        DgvUnion.Tag = Nothing
        '
        Dim angulo As String = LbRotation.Text
        'UltimaFilaExcel = cU.Fila_BuscaDame(UltimoBloqueT1.EffectiveName.Split("_"c)(0), LbInclinationT1.Text, UltimoBloqueT2.EffectiveName.Split("_"c)(0), LbInclinationT2.Text, angulo)
        ActualFilaExcel = cU.Fila_BuscaDame(UltimoBloqueT1.EffectiveName.Substring(0, 8), LbInclinationT1.Text, UltimoBloqueT2.EffectiveName.Substring(0, 8), LbInclinationT2.Text, angulo)
        If ActualFilaExcel IsNot Nothing Then
            DgvUnion.Tag = ActualFilaExcel
            DgvUnion.Rows.AddRange(ActualFilaExcel.RowsL(ActualFilaExcel.LADO).ToArray)
            'DgvUnion.Height = DgvUnion.PreferredSize.Height
            LblLado.Visible = ActualFilaExcel.RowsL.Count > 1
            LbLado.Visible = ActualFilaExcel.RowsL.Count > 1
        End If
        If UClsUnion IsNot Nothing Then
            UClsUnion.UNION_PonValue(DgvUnion)
            LblLado.Visible = True
            LbLado.Visible = True
        End If
        Datos_2CompruebaDatos()
    End Sub
    Public Sub Datos_2CompruebaDatos()
        ' Salir, si no esta activo GUnion
        If GUnion.Enabled = False Then Exit Sub
        '
        Me.SuspendLayout()
        BtnInsertarUnion.Enabled = False
        BtnAceptar.Enabled = False
        '
        ' Comprobar objetos. BtnT1 y BtnT2 siempre activo
        BtnT1.Enabled = True
        BtnT2.Enabled = True
        ' *** BtnT1
        If UltimoBloqueT1 IsNot Nothing Then
            If ActualFilaExcel Is Nothing Then
                LblT1.Text = "Datos T1:" & vbCrLf & UltimoBloqueT1.EffectiveName
            Else
                LblT1.Text = "Datos T1:" & vbCrLf & UltimoBloqueT1.EffectiveName & vbCrLf & ActualFilaExcel.INFEED_CONVEYOR
            End If
            BtnT1.BackColor = btnOn
        Else
            LblT1.Text = "Datos T1:"
            BtnT1.BackColor = btnOff
        End If
        ' *** BtnT2
        If UltimoBloqueT2 IsNot Nothing Then
            If ActualFilaExcel Is Nothing Then
                LblT2.Text = "Datos T2:" & vbCrLf & UltimoBloqueT2.EffectiveName
            Else
                LblT2.Text = "Datos T2:" & vbCrLf & UltimoBloqueT2.EffectiveName & vbCrLf & ActualFilaExcel.OUTFEED_CONVEYOR
            End If
            BtnT2.BackColor = btnOn
        Else
            LblT2.Text = "Datos T2:"
            BtnT2.BackColor = btnOff
        End If
        '
        Datos_3PonEstadoBotonesInsertarAceptar()
        '****************************************
        ' Deshabilitar controles, por defecto.
        'pb1.BackColor = btnOff 'Drawing.Color.FromArgb(255, 192, 192)
        'pb2.BackColor = Drawing.Color.FromArgb(255, 192, 192)
        'btnAgregarUnion.BackColor = Drawing.Color.FromArgb(255, 192, 192)
        'pb2.Enabled = False ': Control_Borde(pb2)
        'btnAgregarUnion.Enabled = False ': Control_Borde(btnAgregarUnion)
        'pb1.BackColor = Drawing.Color.FromArgb(255, 192, 192)
    End Sub
    Public Sub Datos_3PonEstadoBotonesInsertarAceptar()
        Me.SuspendLayout()
        ' *** BtnInsertarUnion y BtnAceptar
        If UltimoBloqueT1 IsNot Nothing AndAlso
                UltimoBloqueT2 IsNot Nothing AndAlso
                LbInclinationT1.SelectedIndex > -1 AndAlso
                LbInclinationT2.SelectedIndex > -1 AndAlso
                LbRotation.SelectedIndex > -1 AndAlso
                DgvUnion.Rows.Count > 0 Then
            BtnInsertarUnion.Enabled = True
            BtnAceptar.Enabled = True
        Else
            BtnInsertarUnion.Enabled = False
            BtnAceptar.Enabled = False
        End If
        Me.ResumeLayout()
    End Sub

    'Public Sub ExcelFila_Actualizar()
    '    If GUnion.Enabled = False Then Exit Sub
    '    ' Si no tenemos estos datos, salir. No se puede buscar nueva fila Excel.
    '    If UltimoBloqueT1 Is Nothing OrElse
    '            UltimoBloqueT2 Is Nothing OrElse
    '            LbInclinationT1.SelectedIndex = -1 OrElse
    '            LbInclinationT2.SelectedIndex = -1 OrElse
    '            LbRotation.SelectedIndex = -1 Then
    '        Exit Sub
    '    End If
    '    'UltimaFilaExcel = cU.Fila_BuscaDame(UltimoBloqueT1.EffectiveName.Split("_"c)(0), LbInclinationT1.Text, UltimoBloqueT2.EffectiveName.Split("_"c)(0), LbInclinationT2.Text, LbRotation.Text)
    '    UltimaFilaExcel = cU.Fila_BuscaDame(UltimoBloqueT1.EffectiveName.Substring(0, 6), LbInclinationT1.Text, UltimoBloqueT2.EffectiveName.Substring(0, 6), LbInclinationT2.Text, LbRotation.Text)
    '    If UltimaFilaExcel IsNot Nothing Then
    '        If UltimaClsUnion IsNot Nothing Then
    '            UltimaClsUnion.ExcelFilaUnion = UltimaFilaExcel
    '        End If
    '        LbUnion.Tag = UltimaFilaExcel
    '        Dim valores As String = UltimaFilaExcel.UNION
    '        valores = valores.Replace(" o ", ";")
    '        valores = valores.Replace(" ", "")
    '        Dim partes() As String = valores.Split(";")
    '        LbUnion.Items.AddRange(partes)
    '        'If CbUnion.Items.Contains(CbUnion.Text) = False Then CbUnion.Text = ""
    '        If LbUnion.Items.Count >= 1 AndAlso LbUnion.Items.Contains(UltimaClsUnion.UNION) Then
    '            ListBox_SeleccionaPorTexto(LbUnion, UltimaClsUnion.UNION)
    '        Else
    '            LbUnion.SelectedIndex = -1
    '            'CbUnion.Text = ""
    '        End If
    '    End If
    'End Sub
    Public Sub tvUniones_Rellena(Optional tipo As modTavil.EFiltro = EFiltro.TODOS)
        PonEstadoControlesInicial()
        ' Rellenar tvGrupos con los grupos que haya ([nombre grupo]) Sacado de XData elementos (regAPPCliente, XData = "GRUPO")
        tvUniones.Nodes.Clear()
        tvUniones.BeginUpdate()
        Dim arrTodos As List(Of TreeNode) = modTavil.tvUniones_DameListTreeNodes
        If arrTodos Is Nothing OrElse arrTodos.Count = 0 Then
            tvUniones.EndUpdate()
            Exit Sub
        End If
        '
        tvUniones.Nodes.AddRange(arrTodos.ToArray)
        tvUniones.Sort()
        tvUniones.SelectedNode = Nothing
        tvUniones_AfterSelect(Nothing, Nothing)
        BtnSeleccionar.Enabled = (tvUniones.Nodes.Count > 0)
        cbCapa.Enabled = (tvUniones.Nodes.Count > 0)
        LblUniones.Text = tvUniones.Nodes.Count & " Uniones"
        tvUniones.EndUpdate()
    End Sub

    Public Sub Uniones_SeleccionarObjetos(HandleUnion As String, Optional conZoom As Boolean = False)
        Dim lGrupos As New List(Of Long)
        Try
            UltimoBloqueUnion = Eventos.COMDoc().HandleToObject(HandleUnion)
            lGrupos.Add(UltimoBloqueUnion.ObjectID)
        Catch ex As Exception
            Exit Sub
        End Try
        '
        Dim idT1 As String = clsA.XLeeDato(UltimoBloqueUnion.Handle, "T1HANDLE")
        Dim idT2 As String = clsA.XLeeDato(UltimoBloqueUnion.Handle, "T2HANDLE")
        If idT1 <> "" Then
            Try
                UltimoBloqueT1 = Eventos.COMDoc().HandleToObject(idT1)
                lGrupos.Add(UltimoBloqueT1.ObjectID)
            Catch ex As Exception
            End Try
        End If
        If idT2 <> "" Then
            Try
                UltimoBloqueT2 = Eventos.COMDoc().HandleToObject(idT2)
                lGrupos.Add(UltimoBloqueT2.ObjectID)
            Catch ex As Exception
            End Try
        End If

        If lGrupos.Count > 0 Then
            If cbZoom.Checked OrElse conZoom = True Then
                clsA.Selecciona_AcadID(lGrupos.ToArray)
                clsA.HazZoomObjeto(UltimoBloqueUnion, 3, False)
            Else
                clsA.Selecciona_AcadID(lGrupos.ToArray)
            End If
        End If
    End Sub

    ' PictureBox (U otros controles). Poner el borde de color rojo  
    Private Sub Control_Borde(oC As Control, Optional restaurar As Boolean = False)
        ' Restaurar botón.
        If restaurar = True Then
            oC.Invalidate()
            Exit Sub
        End If
        ' Poner borde Rojo
        If TypeOf oC Is PictureBox Then
            CType(oC, PictureBox).BorderStyle = BorderStyle.None
        End If
        'Rectangulo del control + Offset del rectangulo hacia fuera.  
        Dim BorderBounds As Rectangle = oC.ClientRectangle
        'BorderBounds.Inflate(-1, -1)

        'Use ControlPaint to draw the border.  
        'Change the Color.Red parameter to your own colour below.  
        ControlPaint.DrawBorder(oC.CreateGraphics,
                                        BorderBounds,
                                        Color.Red,
                                        ButtonBorderStyle.Outset)
    End Sub
    '
    Public Sub BorraDatos()
        'GUnion.Enabled = True
        LblT1.Text = "Datos T1:" ': LblT1.Refresh()
        LblT2.Text = "Datos T2:" ': LblT2.Refresh()
        Me.LbInclinationT1.SelectedIndex = -1 ': Me.LbInclinationT1.Refresh()
        Me.LbInclinationT2.SelectedIndex = -1 ': Me.LbInclinationT2.Refresh()
        Me.LbRotation.SelectedIndex = -1 ': Me.LbRotation.Refresh()
        Me.DgvUnion.Rows.Clear()
        'Me.DgvUnion.Height = Me.DgvUnion.PreferredSize.Height
        'GUnion.Enabled = False
    End Sub
#End Region
End Class
'
'Efecto Click en PictureBox. Click (Borde Rojo), otro click (Sin borde)  
'Private Sub pb1_Click(sender As Object, e As EventArgs) Handles pb1.Click, pb2.Click 'etc  
'    Dim pbX As PictureBox = DirectCast(sender, PictureBox)
'    'Get the rectangle of the control and inflate it to represent the border area  
'    Dim BorderBounds As Rectangle = pbX.ClientRectangle
'    BorderBounds.Inflate(-1, -1)

'    'Use ControlPaint to draw the border.  
'    'Change the Color.Red parameter to your own colour below.  
'    ControlPaint.DrawBorder(pbX.CreateGraphics,
'                            BorderBounds,
'                            Color.Red,
'                            ButtonBorderStyle.Solid)

'    If Not (HighlightedPictureBox Is Nothing) Then
'        'Remove the border of the last PictureBox  
'        HighlightedPictureBox.Invalidate()
'    End If

'    'Rememeber the last highlighted PictureBox  
'    HighlightedPictureBox = CType(sender, PictureBox)
'    pbX = Nothing
'End Sub
