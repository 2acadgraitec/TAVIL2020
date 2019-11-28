Imports Autodesk.AutoCAD.Interop.Common
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.ApplicationServices
Imports TAVIL2020.TAVIL2020
Imports System.Windows.Forms
Imports System.Linq
Imports System.Threading
Imports a2 = AutoCAD2acad.A2acad

Public Class frmBomDatos
    Private Sub frmBomDatos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Eventos.SYSMONVAR(True)
        Me.Text = "BILL OF MATERIAL - v" & cfg._appversion
        If clsA Is Nothing Then clsA = New a2.A2acad(Eventos.COMApp(), cfg._appFullPath, regAPPCliente)
        app_procesointerno = True
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
        Call Global.TAVIL2020.UNIONES.Report_UNIONES()
    End Sub

    Public Sub tvUnions_LlenaXDATA()
        BtnReportUnions.Enabled = False
        ' Rellenar tvGrupos con los grupos que haya ([nombre grupo]) Sacado de XData elementos (regAPPCliente, XData = "GRUPO")
        TvUnions.Nodes.Clear()
        Dim arrTodos As ArrayList = clsA.SeleccionaDameBloquesTODOS(regAPPCliente, "UNION")
        If arrTodos Is Nothing OrElse arrTodos.Count = 0 Then
            Exit Sub
        End If
        ' Filtrar lista de grupo. Sacar nombres únicos.
        For Each acadObj As AcadObject In arrTodos
            Call Global.TAVIL2020.UNIONES.UNION_Crea(acadObj.Handle)
            Dim oUnion As UNION = Global.TAVIL2020.UNIONES.LUniones.Last
            '
            Dim oNode As New TreeNode
            oNode.Text = oUnion.HANDLE
            oNode.Name = oUnion.HANDLE
            oNode.Tag = oUnion.HANDLE
            oNode.ToolTipText = oUnion.HANDLE & vbCrLf & oUnion.NAME
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
End Class
