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
    Public Shared UltimoBloqueUnion As AcadBlockReference = Nothing
    Public Shared UltimoBloqueT1 As AcadBlockReference = Nothing
    Public Shared UltimoBloqueT1L As AcadBlockReference = Nothing
    Public Shared UltimoBloqueT1R As AcadBlockReference = Nothing
    Public Shared UltimoBloqueT2 As AcadBlockReference = Nothing
    Public Shared UltimoBloqueT2L As AcadBlockReference = Nothing
    Public Shared UltimoBloqueT2R As AcadBlockReference = Nothing
    Public Shared UltimaUnion As UNION = Nothing
    Public Shared ActualFilaExcel As UNIONESFila = Nothing
    Private HighlightedPictureBox As PictureBox = Nothing
    Private oTT As ToolTip = Nothing
    Private capaUnionesVisible As Boolean = True
    Public Shared EsUnionNueva As Boolean = False
    Public Shared WithEvents UUni As UCUnion = Nothing
    Public Shared WithEvents UUniX As UCUnionX = Nothing
    Public Shared WithEvents UUniY As UCUnionY = Nothing
    Public WithEvents CMenu As ContextMenuStrip = Nothing
    '
    Public Shared LADO As String = ""
    'Public Shared HOJA As String = "UNIONES"
    Public Shared T1HANDLE As String = ""
    'Public Shared T1INFEED As String = ""
    Public Shared T1INCLINATION As String = ""
    Public Shared T1HANDLEL As String = ""
    'Public Shared T1INFEEDL As String = ""
    Public Shared T1INCLINATIONL As String = ""
    Public Shared T1HANDLER As String = ""
    'Public Shared T1INFEEDR As String = ""
    Public Shared T1INCLINATIONR As String = ""

    'Public Shared UNION As String = ""
    'Public Shared UNITS As String = ""
    Public Shared T2HANDLE As String = ""
    'Public Shared T2OUTFEED As String = ""
    Public Shared T2INCLINATION As String = ""
    Public Shared ANGLE As String = ""
    Public Shared T2HANDLEL As String = ""
    Public Shared T2OUTFEEDL As String = ""
    Public Shared T2INCLINATIONL As String = ""
    Public Shared ANGLEL As String = ""
    Public Shared T2HANDLER As String = ""
    'Public Shared T2OUTFEEDR As String = ""
    Public Shared T2INCLINATIONR As String = ""
    Public Shared ANGLER As String = ""

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

    Private Sub frmUniones_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler UCUnion.TodoSeleccionado, AddressOf UUni_TodoSeleccionado
        AddHandler UCUnionX.TodoSeleccionado, AddressOf UUni_TodoSeleccionado
        AddHandler UCUnionY.TodoSeleccionado, AddressOf UUni_TodoSeleccionado
        AddHandler UCUnion.HayCambioDatos, AddressOf UUni_HayCambios
        AddHandler UCUnionX.HayCambioDatos, AddressOf UUni_HayCambios
        AddHandler UCUnionY.HayCambioDatos, AddressOf UUni_HayCambios
        '
        Eventos.SYSMONVAR(True)
        Me.Text = "UNIONES - v" & appVersion
        ' Cargar recursos
        Using oLock As DocumentLock = Eventos.AXDoc.LockDocument
            'clsA.ClonaTODODesdeDWG(BloqueRecursos, True, True)
            'clsA.ClonaTODODesdeDWG(IO.Path.Combine(IO.Path.GetDirectoryName(BloqueRecursos), "UNION.dwg"), True, True)
            'clsA.ClonaBloqueDesdeDWG(IO.Path.Combine(IO.Path.GetDirectoryName(BloqueRecursos), "UNION.dwg"), "UNION")
            clsA.Clona_UnBloqueDesdeDWG(BloqueRecursos, "UNION")
        End Using
        UserControl_Crear()
        'PonToolTipControles()
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
        CMenu = New ContextMenuStrip()
        Dim TItem As New ToolStripMenuItem
        TItem.Text = "1 -- 1 (UNIONES)"
        TItem.Tag = "U"
        CMenu.Items.Add(TItem)
        Dim TItem1 As New ToolStripMenuItem
        TItem1.Text = "1 -- 2 (UNIONES_Y)"
        TItem1.Tag = "Y"
        CMenu.Items.Add(TItem1)
        Dim TItem2 As New ToolStripMenuItem
        TItem2.Text = "2 -- 1 (UNIONES_X)"
        TItem2.Tag = "X"
        CMenu.Items.Add(TItem2)
        BtnCrearUnion.ContextMenuStrip = CMenu
    End Sub
    Public Sub UserControl_Crear()
        BtnEditarUnion.Enabled = False
        BtnBorrarUnion.Enabled = False
        PUnion.Enabled = False
        GUnion.Enabled = False
        PUnion.Controls.Clear()
        '
        UUni = New UCUnion
        UUni.Dock = DockStyle.Fill
        UUni.Visible = False
        '
        UUniX = New UCUnionX
        UUniX.Dock = DockStyle.Fill
        UUniX.Visible = False
        '
        UUniY = New UCUnionY
        UUniY.Dock = DockStyle.Fill
        UUniY.Visible = False
        '
        PUnion.Controls.Add(UUni)
        PUnion.Controls.Add(UUniX)
        PUnion.Controls.Add(UUniY)
        PUnion.Enabled = False
        GUnion.Enabled = False
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
        Objetos_Vacia()
        ' Poner los controles con el Enabled por defecto.
        If tvUniones.SelectedNode Is Nothing Then Exit Sub
        '
        Me.Uniones_SeleccionarObjetos(tvUniones.SelectedNode.Tag.ToString)
        ' Mostrar los datos en sus controles.
        PonDatosUnion(tvUniones.SelectedNode.Tag.ToString)
    End Sub
    Public Sub Objetos_Vacia()
        UltimoBloqueT1 = Nothing
        UltimoBloqueT1L = Nothing
        UltimoBloqueT1R = Nothing
        UltimoBloqueT2 = Nothing
        UltimoBloqueT2L = Nothing
        UltimoBloqueT2R = Nothing
        UltimoBloqueUnion = Nothing
        ActualFilaExcel = Nothing
        UltimaUnion = Nothing
        ' Vaciar los UserControl
        BorraDatos()
        '
        PUnion.Enabled = False
        GUnion.Enabled = False
        BtnCrearUnion.Enabled = True
        BtnEditarUnion.Enabled = False
        BtnBorrarUnion.Enabled = False
        BtnSeleccionar.Enabled = True
    End Sub

    Private Sub PonDatosUnion(Optional handle As String = "")
        If tvUniones.SelectedNode Is Nothing Then Exit Sub
        If handle = "" Then handle = tvUniones.SelectedNode.Tag.ToString
        If handle = "" Then Exit Sub
        '
        Try
            UltimoBloqueUnion = Eventos.COMDoc().HandleToObject(handle)
        Catch ex As Exception
            Exit Sub
        End Try
        '
        Dim lUniHANLE As IEnumerable(Of UNION) = From x In LClsUnion
                                                 Where x.UnionHANDLE = handle
                                                 Select x
        If lUniHANLE.Count > 0 Then
            UltimaUnion = lUniHANLE.First
        Else
            Exit Sub
        End If
        '
#Region "User Controls UCUnion, UCUnionY y UCUnionX"
        ' T1
        If UltimaUnion.T1HANDLE <> "" Then UltimoBloqueT1 = UltimaUnion.T1Block  '  Eventos.COMDoc().HandleToObject(UClsUnion.T1HANDLE)
        If UltimaUnion.T1HANDLEL <> "" Then UltimoBloqueT1L = UltimaUnion.T1BlockL  '  Eventos.COMDoc().HandleToObject(UClsUnion.T1HANDLE)
        If UltimaUnion.T1HANDLER <> "" Then UltimoBloqueT1R = UltimaUnion.T1BlockR  '  Eventos.COMDoc().HandleToObject(UClsUnion.T1HANDLE)
        If UltimaUnion.T1INCLINATION <> "" Then
            ListBox_SeleccionaPorTexto(UUni.LbIncT1, UltimaUnion.T1INCLINATION)
            ListBox_SeleccionaPorTexto(UUniY.LbIncT1, UltimaUnion.T1INCLINATION)
        End If
        If UltimaUnion.T1INCLINATIONL <> "" Then ListBox_SeleccionaPorTexto(UUniX.LbIncT1L, UltimaUnion.T1INCLINATIONL)
        If UltimaUnion.T1INCLINATIONR <> "" Then ListBox_SeleccionaPorTexto(UUniX.LbIncT1R, UltimaUnion.T1INCLINATIONR)
        ' T2
        If UltimaUnion.T2HANDLE <> "" Then UltimoBloqueT2 = UltimaUnion.T2Block
        If UltimaUnion.T2HANDLEL <> "" Then UltimoBloqueT2L = UltimaUnion.T2BlockL
        If UltimaUnion.T2HANDLER <> "" Then UltimoBloqueT2R = UltimaUnion.T2BlockR
        If UltimaUnion.T2INCLINATION <> "" Then
            ListBox_SeleccionaPorTexto(UUni.LbIncT2, UltimaUnion.T2INCLINATION)
            ListBox_SeleccionaPorTexto(UUniX.LbIncT2, UltimaUnion.T2INCLINATION)
        End If
        If UltimaUnion.T2INCLINATIONL <> "" Then ListBox_SeleccionaPorTexto(UUniY.LbIncT2L, UltimaUnion.T2INCLINATIONL)
        If UltimaUnion.T2INCLINATIONR <> "" Then ListBox_SeleccionaPorTexto(UUniY.LbIncT2R, UltimaUnion.T2INCLINATIONR)
        ' Angle
        If UltimaUnion.ANGLE = "0" OrElse UltimaUnion.ANGLE = "" Then
            UUni.LbAngle.SelectedIndex = 0
            UUniX.LbAngle.SelectedIndex = 0
        ElseIf UltimaUnion.ANGLE = "90" Then
            UUni.LbAngle.SelectedIndex = 1
            UUniX.LbAngle.SelectedIndex = 1
        Else
            UUni.LbAngle.SelectedIndex = -1
            UUniX.LbAngle.SelectedIndex = -1
        End If
        ' AngleL
        If UltimaUnion.ANGLEL = "0" OrElse UltimaUnion.ANGLEL = "" Then
            UUniY.LbAngleL.SelectedIndex = 0
        ElseIf UltimaUnion.ANGLEL = "90" Then
            UUniY.LbAngleL.SelectedIndex = 1
        Else
            UUniY.LbAngleL.SelectedIndex = -1
        End If
        ' AngleR.
        If UltimaUnion.ANGLER = "0" OrElse UltimaUnion.ANGLER = "" Then
            UUniY.LbAngleR.SelectedIndex = 0
        ElseIf UltimaUnion.ANGLER = "90" Then
            UUniY.LbAngleR.SelectedIndex = 1
        Else
            UUniY.LbAngleR.SelectedIndex = -1
        End If
        ' LADO
        ListBox_SeleccionaPorTexto(UUni.LbLado, UltimaUnion.LADO)
#End Region
        '
        DgvUnion.Rows.Clear()
        If UltimaUnion.ExcelFilaUnion IsNot Nothing AndAlso
            UltimaUnion.ExcelFilaUnion.RowsL IsNot Nothing _
            AndAlso UltimaUnion.ExcelFilaUnion.RowsL.Count > 0 AndAlso
            UltimaUnion.ExcelFilaUnion.RowsL.ContainsKey(UltimaUnion.ExcelFilaUnion.LADO) Then
            DgvUnion.Rows.AddRange(UltimaUnion.ExcelFilaUnion.RowsL(UltimaUnion.ExcelFilaUnion.LADO).ToArray)
        End If
        '
        If UltimaUnion.HOJA = "UNIONES" Then
            UUni.Visible = True
            UUniX.Visible = False
            UUniY.Visible = False
        ElseIf UltimaUnion.HOJA = "UNIONES_X" Then
            UUni.Visible = False
            UUniX.Visible = True
            UUniY.Visible = False
        ElseIf UltimaUnion.HOJA = "UNIONES_Y" Then
            UUni.Visible = False
            UUniX.Visible = False
            UUniY.Visible = True
        End If
        BtnEditarUnion.Enabled = True
        BtnBorrarUnion.Enabled = True
    End Sub
    Private Sub tvUniones_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles tvUniones.MouseDoubleClick
        Uniones_SeleccionarObjetos(tvUniones.SelectedNode.Tag, conZoom:=True)
    End Sub
    Private Sub BtnCrearUnion_Click(sender As Object, e As EventArgs) Handles BtnCrearUnion.Click
        clsA.Seleccion_Quitar()
        BorraDatos()
        EsUnionNueva = True
        BtnCrearUnion.ContextMenuStrip.Show(MousePosition)
        UltimoBloqueT1 = Nothing
        UltimoBloqueT1L = Nothing
        UltimoBloqueT1R = Nothing
        UltimoBloqueT2 = Nothing
        UltimoBloqueT2R = Nothing
        UltimoBloqueT2R = Nothing
        UltimoBloqueUnion = Nothing
        ActualFilaExcel = Nothing
        UltimaUnion = Nothing
        '
        PUnion.Enabled = True
        GUnion.Enabled = True
        DgvUnion.Enabled = False
        BtnBuscar.Enabled = False
        BtnCancelar.Visible = True
        BtnAceptar.Visible = False
        BtnInsertarUnion.Visible = False
    End Sub

    Private Sub CMenu_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles CMenu.ItemClicked
        Dim tipo As String = e.ClickedItem.Tag.ToString.ToUpper
        If tipo.ToUpper = "U" Then
            UUni.Visible = True
            UUniX.Visible = False
            UUniY.Visible = False
        ElseIf tipo.ToUpper = "X" Then
            UUni.Visible = False
            UUniX.Visible = True
            UUniY.Visible = False
        ElseIf tipo.ToUpper = "Y" Then
            UUni.Visible = False
            UUniX.Visible = False
            UUniY.Visible = True
        End If
        '
        BtnCrearUnion.Enabled = False
        BtnEditarUnion.Enabled = False
        BtnSeleccionar.Enabled = False
        BtnSeleccionar.Enabled = False
    End Sub

    Private Sub BtnEditarUnion_Click(sender As Object, e As EventArgs) Handles BtnEditarUnion.Click
        If tvUniones.SelectedNode Is Nothing Then Exit Sub
        '
        'clsA.Seleccion_Quitar()
        EsUnionNueva = False
        PUnion.Enabled = True
        GUnion.Enabled = True
        DgvUnion.Enabled = False
        BtnBuscar.Enabled = False
        BtnCancelar.Visible = True
        BtnAceptar.Visible = False
        BtnInsertarUnion.Visible = False
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
            'PonEstadoControlesInicial()
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
                If oBlr IsNot Nothing Then UNIONES.UNION_Crea(oBlr.Handle)
            Loop While oBlr IsNot Nothing
        Catch ex As Exception
            Debug.Print(ex.ToString)
        End Try
        tvUniones_Rellena()
        Me.Visible = True
    End Sub

    Private Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        ' Buscar la fila de Excel con esos valores
        DgvUnion.Rows.Clear()
        DgvUnion.Tag = Nothing
        '
        Dim inC, inCL, inCR, outC, outCL, outCR As String
        inC = If(UltimoBloqueT1 IsNot Nothing, UltimoBloqueT1.EffectiveName.Substring(0, 8), "")
        inCL = If(UltimoBloqueT1L IsNot Nothing = True, UltimoBloqueT1L.EffectiveName.Substring(0, 8), "")
        inCR = If(UltimoBloqueT1R IsNot Nothing = True, UltimoBloqueT1R.EffectiveName.Substring(0, 8), "")
        outC = If(UltimoBloqueT2 IsNot Nothing = True, UltimoBloqueT2.EffectiveName.Substring(0, 8), "")
        outCL = If(UltimoBloqueT2L IsNot Nothing = True, UltimoBloqueT2L.EffectiveName.Substring(0, 8), "")
        outCR = If(UltimoBloqueT2R IsNot Nothing = True, UltimoBloqueT2R.EffectiveName.Substring(0, 8), "")
        If UUni.Visible = True Then
            'T1INCLINATION = ""
            T1INCLINATIONL = ""
            T1INCLINATIONR = ""
            'T2INCLINATION = "" : ANGLE = ""
            T2INCLINATIONL = "" : ANGLEL = ""
            T2INCLINATIONR = "" : ANGLER = ""
        ElseIf UUniX.Visible = True Then
            T1INCLINATION = ""
            'T1INCLINATIONL = ""
            'T1INCLINATIONR = ""
            'T2INCLINATION = "" : ANGLE = ""
            T2INCLINATIONL = "" : ANGLEL = ""
            T2INCLINATIONR = "" : ANGLER = ""
        ElseIf UUniY.Visible = True Then
            'T1INCLINATION = ""
            T1INCLINATIONL = ""
            T1INCLINATIONR = ""
            T2INCLINATION = "" : ANGLE = ""
            'T2INCLINATIONL = "" : ANGLEL = ""
            'T2INCLINATIONR = "" : ANGLER = ""
        End If
        ActualFilaExcel = cU.Fila_BuscaDame(inC, T1INCLINATION,
                inCL, T1INCLINATIONL,
                inCR, T1INCLINATIONR,
                outC, T2INCLINATION, ANGLE,
                outCL, T2INCLINATIONL, ANGLEL,
                outCR, T2INCLINATIONR, ANGLER,
                LADO)

        If ActualFilaExcel IsNot Nothing Then
            DgvUnion.Tag = ActualFilaExcel
            DgvUnion.Rows.AddRange(ActualFilaExcel.RowsL(ActualFilaExcel.LADO).ToArray)
        End If


        ' Activar Aceptar (Si estamos editando) o Insertar (Si en nuevo)
        If DgvUnion.Rows.Count > 0 Then
            If EsUnionNueva Then
                ' Nueva union
                BtnAceptar.Visible = False
                BtnInsertarUnion.Visible = True
            Else
                ' Editando union
                BtnAceptar.Visible = True
                BtnInsertarUnion.Visible = False
            End If
            DgvUnion.Enabled = True
        End If
    End Sub

    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        UserControl_Crear()
        tvUniones.SelectedNode = Nothing
        tvUniones_AfterSelect(Nothing, Nothing)
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
        If UltimaUnion IsNot Nothing Then
            UltimaUnion.ExcelFilaUnion = ActualFilaExcel
            UltimaUnion.LADO = LADO
            UltimaUnion.T1HANDLE = T1HANDLE
            UltimaUnion.T1INFEED = ActualFilaExcel.INFEED_CONVEYOR
            UltimaUnion.T1INCLINATION = T1INCLINATION
            UltimaUnion.T1HANDLEL = T1HANDLEL
            UltimaUnion.T1INFEEDL = ActualFilaExcel.INFEED_CONVEYOR_L
            UltimaUnion.T1INCLINATIONL = T1INCLINATIONL
            UltimaUnion.T1HANDLER = T1HANDLER
            UltimaUnion.T1INFEEDR = ActualFilaExcel.INFEED_CONVEYOR_R
            UltimaUnion.T1INCLINATIONR = T1INCLINATIONR
            '
            UltimaUnion.UNION = String.Join(";", DatosUnion.ToArray)
            UltimaUnion.UNITS = String.Join(";", DatosUnits.ToArray)
            '
            UltimaUnion.T2HANDLE = T2HANDLE
            UltimaUnion.T2OUTFEED = ActualFilaExcel.OUTFEED_CONVEYOR
            UltimaUnion.T2INCLINATION = T2INCLINATION
            UltimaUnion.T2HANDLEL = T2HANDLEL
            UltimaUnion.T2OUTFEEDL = ActualFilaExcel.OUTFEED_CONVEYOR_L
            UltimaUnion.T2INCLINATIONL = T2INCLINATIONL
            UltimaUnion.T2HANDLER = T2HANDLER
            UltimaUnion.T2OUTFEEDR = ActualFilaExcel.OUTFEED_CONVEYOR_R
            UltimaUnion.T2INCLINATIONR = T2INCLINATIONR
            UltimaUnion.ANGLE = ANGLE
            UltimaUnion.ANGLEL = ANGLEL
            UltimaUnion.ANGLER = ANGLER
            UltimaUnion.UNIONFin_Pon(Me.DgvUnion)   ' Escribir finalmente los atributos UNION y UNITS en el bloque.
        Else
            UltimaUnion = New UNION(UltimoBloqueUnion.Handle, String.Join(";", DatosUnion.ToArray), String.Join(";", DatosUnits.ToArray),
                T1HANDLE, ActualFilaExcel.INFEED_CONVEYOR, T1INCLINATION,
                T1HANDLEL, ActualFilaExcel.INFEED_CONVEYOR_L, T1INCLINATIONL,
                T1HANDLER, ActualFilaExcel.INFEED_CONVEYOR_R, T1INCLINATIONR,
                T2HANDLE, ActualFilaExcel.OUTFEED_CONVEYOR, T2INCLINATION, ANGLE,
                T2HANDLEL, ActualFilaExcel.OUTFEED_CONVEYOR_L, T2INCLINATION, ANGLEL,
                T2HANDLER, ActualFilaExcel.OUTFEED_CONVEYOR_R, T2INCLINATIONR, ANGLER,
                LADO)
            UltimaUnion.UNIONFin_Pon(Me.DgvUnion)
        End If
        '
        tvUniones.SelectedNode = Nothing
        tvUniones_AfterSelect(Nothing, Nothing)
        tvUniones_Rellena()
    End Sub

    Private Sub BtnInsertarUnion_Click(sender As Object, e As EventArgs) Handles BtnInsertarUnion.Click
        UltimaUnion = Nothing
        clsA.ActivaAppAPI()
        clsA.Bloque_Inserta(, NombreBloqueUNION)
        If clsA.oBlult IsNot Nothing Then
            UltimoBloqueUnion = Eventos.COMDoc.HandleToObject(clsA.oBlult.Handle)
            BtnAceptar_Click(Nothing, Nothing)
        End If
    End Sub
#Region "FUNCIONES"
    ''' <summary>
    ''' Poner estado de los controles Enable True/False
    ''' </summary>
    ''' <param name="accion">"" (Nada), "C" (Crear), "E" (Editar)</param>
    Public Sub PonEstadoControlesInicial(Optional accion As String = "")
        'clsA.Seleccion_Quitar()
        '' Objectos
        'UltimoBloqueT1 = Nothing
        'UltimoBloqueT1L = Nothing
        'UltimoBloqueT1R = Nothing
        'UltimoBloqueT2 = Nothing
        'UltimoBloqueT2L = Nothing
        'UltimoBloqueT2R = Nothing
        'UltimoBloqueUnion = Nothing
        'ActualFilaExcel = Nothing
        'UltimaUnion = Nothing
        '' Controles
        'BtnCrearUnion.Enabled = True
        'BtnEditarUnion.Enabled = False
        'BtnBorrarUnion.Enabled = False
        'BtnSeleccionar.Enabled = True
        'BtnInsertarUnion.Enabled = False : BtnInsertarUnion.BackColor = btnOn
        'PUnion.Enabled = False
        'GUnion.Enabled = False
        ''BorraDatos()
        'PonUserControl()
        ''
        ''If tvUniones.SelectedNode IsNot Nothing Then
        ''    BtnEditarUnion.Enabled = True
        ''    BtnBorrarUnion.Enabled = True
        ''End If
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
        'oTT.SetToolTip(frmU.BtnT1, "Seleccionar Transportador 1")
        'oTT.SetToolTip(frmU.BtnT2, "Seleccionar Transportador 2")
        oTT.SetToolTip(Me.BtnInsertarUnion, "Insertar o Mover Unión")
        oTT.SetToolTip(Me.btnCerrar, "Cerrar Uniones")
        oTT.SetToolTip(Me.BtnAceptar, "Aceptar unión y escribir atributos")
    End Sub

    Public Sub Datos_1PonUnion()
        'If GUnion.Enabled = False Then Exit Sub
        'If UltimoBloqueT1 Is Nothing OrElse UltimoBloqueT2 Is Nothing Then Exit Sub
        'DgvUnion.Rows.Clear()
        'DgvUnion.Tag = Nothing
        ''
        'Dim angulo As String = LbRotation.Text
        ''UltimaFilaExcel = cU.Fila_BuscaDame(UltimoBloqueT1.EffectiveName.Split("_"c)(0), LbInclinationT1.Text, UltimoBloqueT2.EffectiveName.Split("_"c)(0), LbInclinationT2.Text, angulo)
        'ActualFilaExcel = cU.Fila_BuscaDame(UltimoBloqueT1.EffectiveName.Substring(0, 8), LbInclinationT1.Text, UltimoBloqueT2.EffectiveName.Substring(0, 8), LbInclinationT2.Text, angulo)
        'If ActualFilaExcel IsNot Nothing Then
        '    DgvUnion.Tag = ActualFilaExcel
        '    DgvUnion.Rows.AddRange(ActualFilaExcel.RowsL(ActualFilaExcel.LADO).ToArray)
        '    'DgvUnion.Height = DgvUnion.PreferredSize.Height
        '    LblLado.Visible = ActualFilaExcel.RowsL.Count > 1
        '    LbLado.Visible = ActualFilaExcel.RowsL.Count > 1
        'End If
        'If UClsUnion IsNot Nothing Then
        '    UClsUnion.UNION_PonValue(DgvUnion)
        '    LblLado.Visible = True
        '    LbLado.Visible = True
        'End If
        'Datos_2CompruebaDatos()
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
        UUni.BtnT1.Enabled = True
        UUni.BtnT2.Enabled = True
        ' *** BtnT1
        If UltimoBloqueT1 IsNot Nothing Then
            UUni.BtnT1.BackColor = btnOn
        Else
            UUni.BtnT1.BackColor = btnOff
        End If
        ' *** BtnT2
        If UltimoBloqueT2 IsNot Nothing Then
            UUni.BtnT2.BackColor = btnOn
        Else
            UUni.BtnT2.BackColor = btnOff
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
                UUni.LbIncT1.SelectedIndex > -1 AndAlso
                UUni.LbIncT2.SelectedIndex > -1 AndAlso
                UUni.LbAngle.SelectedIndex > -1 AndAlso
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
        ' Rellenar tvGrupos con los grupos que haya ([nombre grupo]) Sacado de XData elementos (regAPPCliente, XData = "GRUPO")
        tvUniones.Nodes.Clear()
        'PonEstadoControlesInicial()
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
        ' Falla si la llamamos como tarea
        'Dim t As New Threading.Thread(AddressOf TvUniones_CreaUNION) : t.Start()
        TvUniones_CreaUNION()
    End Sub

    Public Sub TvUniones_CreaUNION()
        For Each oNode As TreeNode In tvUniones.Nodes
            UNIONES.UNION_Crea(oNode.Name)
        Next
        BtnReportTodo.Enabled = True : BtnReportTodo.Refresh()
        BtnReportTotal.Enabled = True : BtnReportTotal.Refresh()
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
        ' UCUnion
        UUni.LbIncT1.SelectedIndex = -1 ': Me.LbInclinationT1.Refresh()
        UUni.LbIncT2.SelectedIndex = -1 ': Me.LbInclinationT2.Refresh()
        UUni.LbAngle.SelectedIndex = -1 ': Me.LbRotation.Refresh()
        UUni.LbLado.SelectedIndex = -1
        ' UCUnionX
        UUniX.LbIncT1L.SelectedIndex = -1 ': Me.LbInclinationT2.Refresh()
        UUniX.LbIncT1R.SelectedIndex = -1 ': Me.LbInclinationT1.Refresh()
        UUniX.LbAngle.SelectedIndex = -1 ': Me.LbRotation.Refresh()
        UUniX.LbIncT2.SelectedIndex = -1
        ' UCUNIONY
        UUniY.LbIncT1.SelectedIndex = -1 ': Me.LbInclinationT1.Refresh()
        UUniY.LbIncT2L.SelectedIndex = -1 ': Me.LbInclinationT2.Refresh()
        UUniY.LbIncT2R.SelectedIndex = -1 ': Me.LbInclinationT2.Refresh()
        UUniY.LbAngleL.SelectedIndex = -1 ': Me.LbRotation.Refresh()
        UUniY.LbAngleR.SelectedIndex = -1 ': Me.LbRotation.Refresh()
        'GUnion.Enabled = True
        Me.DgvUnion.Rows.Clear()
        'Me.DgvUnion.Height = Me.DgvUnion.PreferredSize.Height
        'GUnion.Enabled = False
    End Sub

    Private Sub BtnReportTodo_Click(sender As Object, e As EventArgs) Handles BtnReportTodo.Click
        UNIONES.Report_UNIONES_TODO()
    End Sub

    Private Sub BtnReportTotal_Click(sender As Object, e As EventArgs) Handles BtnReportTotal.Click
        UNIONES.Report_UNIONES_TOTALES()
    End Sub

    Private Sub UUni_TodoSeleccionado(correcto As Boolean)
        If PUnion.Enabled = False Then Exit Sub
        If correcto = True Then
            GUnion.Enabled = True
            DgvUnion.Enabled = False
            BtnBuscar.Enabled = True
            BtnCancelar.Visible = BtnCancelar.Enabled = True
            BtnAceptar.Visible = False
            BtnInsertarUnion.Visible = False
        Else
            GUnion.Enabled = False
        End If
    End Sub
    Private Sub UUni_HayCambios()
        If PUnion.Enabled = False Then Exit Sub
        DgvUnion.Rows.Clear()
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
