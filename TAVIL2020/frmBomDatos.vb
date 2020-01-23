Imports Autodesk.AutoCAD.Interop.Common
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.ApplicationServices
Imports TAVIL2020.TAVIL2020
Imports System.Windows.Forms
Imports System.Linq
Imports System.Threading
Imports a2 = AutoCAD2acad.A2acad

Partial Public Class frmBomDatos
    Private ultimoHandle As String = ""
    Private Sub frmBomDatos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Eventos.SYSMONVAR(True)
        Me.Text = "BILL OF MATERIAL - v" & appVersion
        app_procesointerno = True
        If cPT Is Nothing Then cPT = New clsPT
        oBlR = Nothing
        Global.TAVIL2020.GRUPOS.LGrupos = New List(Of GRUPO)
        Global.TAVIL2020.GRUPOS.DGrupos = New Dictionary(Of String, GRUPO)
        'Dim t As New System.Threading.Tasks.Task(AddressOf tvUnions_LlenaXDATA) : t.Start()
        'Dim t1 As New System.Threading.Tasks.Task(AddressOf tvGroups_LlenaXDATA) : t.Start()
        tvUnions_LlenaXDATA()
        tvGroups_LlenaXDATA()
    End Sub

    Private Sub frmBomDatos_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Eventos.SYSMONVAR()
        app_procesointerno = False
        frmBo = Nothing
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Private Sub BtnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCerrar.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#Region "GRUPOS"
    Private Sub tvGrupos_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TvGroups.AfterSelect
        If sender Is Nothing OrElse e Is Nothing Then Exit Sub
        If TvGroups.SelectedNode Is Nothing Then Exit Sub
        '
        If Global.TAVIL2020.GRUPOS.DGrupos Is Nothing Then Global.TAVIL2020.GRUPOS.DGrupos = New Dictionary(Of String, GRUPO)
        If Global.TAVIL2020.GRUPOS.DGrupos.ContainsKey(e.Node.Text) Then
            LblCountGroups.Text = "Blocks in Group = " & Global.TAVIL2020.GRUPOS.DGrupos(e.Node.Text).lMembers.Count
            BtnReportSelected.Enabled = True
        Else
            LblCountGroups.Text = "Blocks in Group = X"
            BtnReportSelected.Enabled = False
        End If
    End Sub

    Private Sub TvGrupos_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles TvGroups.MouseDoubleClick
        If sender Is Nothing OrElse e Is Nothing Then Exit Sub
        If TvGroups.SelectedNode Is Nothing Then Exit Sub
        '
        Grupo_SeleccionarObjetos(TvGroups.SelectedNode.Text, True)
    End Sub

    Private Sub BtnReportSelected_Click(sender As Object, e As EventArgs) Handles BtnReportSelected.Click
        Dim g As GRUPO = Global.TAVIL2020.GRUPOS.DGrupos(TvGroups.SelectedNode.Text)
        Report_Blocks(g.lMembers, "GRUPO_" & g.name, cbPLANTA.Checked)
    End Sub

    Public Sub tvGroups_LlenaXDATA()
        ' Rellenar tvGrupos con los grupos que haya ([nombre grupo]) Sacado de XData elementos (regAPPCliente, XData = "GRUPO")
        TvGroups.Nodes.Clear()
        Dim arrTodos As List(Of String) = clsA.SeleccionaTodosObjetos_Handle(,, True)
        If arrTodos Is Nothing OrElse arrTodos.Count = 0 Then
            Exit Sub
        End If
        ' Filtrar lista de grupo. Sacar nombres únicos.
        For Each queHandle As String In arrTodos
            Dim oG As GRUPO = Nothing
            Dim acadObj As AcadObject = Eventos.COMDoc().HandleToObject(queHandle)
            Dim nGrupo As String = clsA.XLeeDato(acadObj.Handle, cGRUPO)
            If nGrupo = "" Then Continue For
            '
            If TvGroups.Nodes.ContainsKey(nGrupo) Then
                Call Global.TAVIL2020.GRUPOS.DGrupos(nGrupo).lMembers.Add(acadObj.Handle)
                Continue For
            Else
                oG = New GRUPO
                oG.name = nGrupo
                oG.lMembers.Add(queHandle)
                Call Global.TAVIL2020.GRUPOS.DGrupos.Add(nGrupo, oG)
            End If
            '
            Dim oNode As New TreeNode
            oNode.Text = nGrupo
            oNode.Name = nGrupo
            oNode.Tag = nGrupo
            oNode.ToolTipText = oNode.Tag
            TvGroups.Nodes.Add(oNode)
            oNode = Nothing
            acadObj = Nothing
            oG = Nothing
        Next
        TvGroups.Sort()
        '
        TvGroups.SelectedNode = Nothing
        tvGrupos_AfterSelect(Nothing, Nothing)
    End Sub

#End Region

#Region "UNIONES"
    Private Sub TvUnions_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TvUnions.AfterSelect

    End Sub

    Private Sub BtnReportUnions_Click(sender As Object, e As EventArgs) Handles BtnReportUnions.Click
        Call Global.TAVIL2020.UNIONES.Report_UNIONES_TODO()
    End Sub

    Public Sub tvUnions_LlenaXDATA()
        BtnReportUnions.Enabled = False
        ' Rellenar tvGrupos con los grupos que haya ([nombre grupo]) Sacado de XData elementos (regAPPCliente, XData = "GRUPO")
        TvUnions.Nodes.Clear()
        Dim arrTodos As ArrayList = clsA.SeleccionaDameBloquesTODOS(regAPPCliente, "UNION") ', CapaUniones) ', "UNION")
        If arrTodos Is Nothing OrElse arrTodos.Count = 0 Then
            Exit Sub
        End If
        ' Filtrar lista de grupo. Sacar nombres únicos.
        For Each acadObj As AcadObject In arrTodos
            Call Global.TAVIL2020.UNIONES.UNION_Crea(acadObj.Handle)
            Dim oUnion As UNION = Global.TAVIL2020.UNIONES.LUniones.Last
            '
            Dim oNode As New TreeNode
            oNode.Text = oUnion.UnionHANDLE
            oNode.Name = oUnion.UnionHANDLE
            oNode.Tag = oUnion.UnionHANDLE
            oNode.ToolTipText = oUnion.UnionHANDLE & vbCrLf & oUnion.UnionBlock.EffectiveName
            TvUnions.Nodes.Add(oNode)
            oNode = Nothing
            acadObj = Nothing
        Next
        TvUnions.Sort()
        TvUnions.SelectedNode = Nothing
        TvUnions_AfterSelect(Nothing, Nothing)
        BtnReportUnions.Enabled = True
        LblTotalUniones.Text = "Total Unions = " & TvUnions.Nodes.Count
    End Sub

#End Region
#Region "PATAS"
    Private Sub cbXXX_CheckedChanged(sender As Object, e As EventArgs) Handles cbXXX.CheckedChanged, CbSoloPlanta.CheckedChanged
        TvPatas.Nodes.Clear()
        btnActualizar.BackColor = btnOff    ' Drawing.Color.Red
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Cursor.Current = Cursors.WaitCursor
        btnActualizar.BackColor = btnOn
        TvPatas.Nodes.Clear()
        BtnReportPatas.Enabled = False
        '
        'Dim lBlo As List(Of AcadBlockReference) = clsA.SeleccionaDameBloquesTODOS_ListAcadBlockReference(, "*_*")
        Dim lBlo As List(Of AcadBlockReference) = clsA.Bloques_DameNombreContiene("PT")
        pb1.Visible = lBlo.Count > 0
        pb1.Maximum = lBlo.Count
        TvPatas.BeginUpdate()
        '
        If lBlo IsNot Nothing AndAlso lBlo.Count > 0 Then
            For Each oBl As AcadBlockReference In lBlo
                If pb1.Value < pb1.Maximum Then pb1.Value += 1
                Dim b As New clsBloquePataDatos(oBl, soloXXX:=True)
                ' Si sólo queremos los que tengan XXX
                If cbXXX.Checked And b.tieneXXX = False Then Continue For
                If cbPLANTA.Checked AndAlso b.esPlanta = False Then Continue For
                '
                Dim node As New TreeNode()
                node.Text = oBl.EffectiveName
                node.Tag = oBl.Handle   ' oBl
                TvPatas.Nodes.Add(node)
                node = Nothing
                b = Nothing
                System.Windows.Forms.Application.DoEvents()
            Next
            pb1.Value = 0
            TvPatas.Sort()
        End If
        lBlo = Nothing
        pb1.Visible = False
        TvPatas.EndUpdate()
        LblTotalPatas.Text = "Total Patas = " & TvPatas.Nodes.Count
        BtnReportPatas.Enabled = TvPatas.Nodes.Count > 0
        Cursor = Cursors.Default
    End Sub

    Private Sub BtnReportPatas_Click(sender As Object, e As EventArgs) Handles BtnReportPatas.Click
        Dim nFile As String = DateTime.Now.ToString("yyyyMMddHHmmss") & "·ListadoPatas.csv"
        Dim fFullPath As String = IO.Path.Combine(appFolder, nFile)
        Dim soloPlanta As Boolean = True
        Dim result As MsgBoxResult = MsgBox("¿Resumen final sólo de bloques de Planta?" & vbCrLf & vbCrLf &
                  "SI" & vbTab & "-->" & vbTab & " Resumen final SOLO bloque de Planta" & vbCrLf &
                  "NO" & vbTab & "-->" & vbTab & "Resumen final de TODOS los bloques" & vbCrLf &
                  "CANCELAR" & vbTab & "-->" & vbTab & "Cancelar impresión", MsgBoxStyle.YesNoCancel, "RESUMEN FINAL")
        If result = MsgBoxResult.Cancel Then
            Exit Sub
        ElseIf result = MsgBoxResult.No Then
            soloPlanta = False
        Else
            soloPlanta = True
        End If
        '
        Dim cabecera As String = "BLOQUE;CANTIDAD;CODE/DIRECT.;WIDTH;RADIUS;HEIGHT" & vbCrLf
        Dim mensaje As String = ""
        Dim cabecera1 As String = vbCrLf & vbCrLf & vbCrLf & "RESUMEN DE BLOQUES DE PATAS" & IIf(soloPlanta = True, " (SOLO PLANTA)", "") & ";;;;;" & vbCrLf & "ITEM_NUMBER;CANTIDAD;;;;" & vbCrLf
        Dim mensaje1 As String = ""
        '
        Cursor.Current = Cursors.WaitCursor
        pb1.Value = 0
        ultimoHandle = ""
        TvPatas.SelectedNode = Nothing
        Dim lBlo As List(Of AcadBlockReference) = clsA.Bloques_DameNombreContiene("PT") 'clsA.SeleccionaDameBloquesTODOS_ListAcadBlockReference(, "PT*")
        pb1.Maximum = lBlo.Count
        '
        Dim colData As New Dictionary(Of String, String())          ' Listado por elementos únicos
        Dim colDataResumen As New Dictionary(Of String, Object())    ' Listado resumen por nombre bloque
        For Each oBl As AcadBlockReference In lBlo
            If pb1.Value < pb1.Maximum Then pb1.Value += 1
            Dim b As New clsBloquePataDatos(oBl)
            ' Si sólo queremos los que tengan XXX
            'If cbXXX.Checked And b.tieneXXX = False Then Continue For
            'If cbPLANTA.Checked AndAlso (b._EFFECTIVENAME.Contains("SVIEW") OrElse b._EFFECTIVENAME.Contains("ALÇAT2")) Then Continue For
            '
            ' Comprobar valores en Excel, si CODE no contiene XX y si estan vacíos
            If b._CODE.ToUpper.Contains("XX") = False AndAlso b._DIRECTRIZ.ToUpper.Contains("XX") = False AndAlso b._DIRECTRIZ1.ToUpper.Contains("XX") = False Then
                If b._WIDTH = "" Then b._WIDTH = cPT.Filas_DameValorConCODE(b._CODE, nombreColumnaPT.WIDTH)
                If b._RADIUS = "" Then b._RADIUS = cPT.Filas_DameValorConCODE(b._CODE, nombreColumnaPT.RADIUS)
                If b._HEIGHT = "" Then b._HEIGHT = cPT.Filas_DameValorConCODE(b._CODE, nombreColumnaPT.HEIGHT)
            End If
            b.Pon_ITEM_NUMBER()
            ' Ver si existe en la colección colData (aumentar cantidad) o añadirlo si no existía
            '"BLOQUE;CANTIDAD;CODE;WIDTH/RADIUS;HEIGHT"
            If colData.ContainsKey(b._clave) Then
                colData(b._clave)(1) = colData(b._clave)(1) + 1
            Else
                colData.Add(b._clave, {b._EFFECTIVENAME, 1, b._CODE, b._WIDTH, b._RADIUS, b._HEIGHT})
            End If
            '
            ' ***** Cambiamos a ITEM_NUMBER (Antes era BLOCK) Ver si existe en la colección colDataResumen (aumentar cantidad) o añadirlo si no existía
            '"ITEM_NUMBER;CANTIDAD;;;"
            If colDataResumen.ContainsKey(b._ITEM_NUMBER) Then
                colDataResumen(b._ITEM_NUMBER)(0) += 1
            Else
                colDataResumen.Add(b._ITEM_NUMBER, {1, b.esPlanta})
            End If
            System.Windows.Forms.Application.DoEvents()
        Next
        pb1.Value = 0
        Cursor = Cursors.Default
        '
        ' "BLOQUE;CANTIDAD;CODE;WIDTH;RADIUS;HEIGHT"
        For Each datos As KeyValuePair(Of String, String()) In colData
            mensaje &= String.Join(";", datos.Value) & vbCrLf
        Next
        mensaje = mensaje.Substring(0, mensaje.Length - 1)  ' Quitar el último carácter (El vbCrlf)

        ' "ITEM_NUMBER;CANTIDAD"
        For Each datos As KeyValuePair(Of String, Object()) In colDataResumen
            If soloPlanta = True And datos.Value(1).ToString.ToUpper = "FALSE" Then
                Continue For
            End If
            mensaje1 &= datos.Key & ";" & datos.Value(0) & ";;;;" & vbCrLf
        Next
        If mensaje1.Length > 1 Then
            mensaje1 = mensaje1.Substring(0, mensaje1.Length - 1)  ' Quitar el último carácter (El vbCrlf)
        End If
        '
        IO.File.WriteAllText(fFullPath, cabecera & mensaje & cabecera1 & mensaje1, System.Text.Encoding.UTF8)
        If MsgBox("¿Abrir informe...?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "SOLICITUD DE CONFIRMACION") = MsgBoxResult.Yes Then
            Process.Start(fFullPath)
        End If
    End Sub
#End Region
End Class
