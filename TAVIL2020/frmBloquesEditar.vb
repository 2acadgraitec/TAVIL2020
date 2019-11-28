Imports Autodesk.AutoCAD.Interop.Common
Imports System.Windows.Forms
Imports a2 = AutoCAD2acad.A2acad

Public Class frmBloquesEditar
    Public oBl As AcadBlockReference = Nothing
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Bloque_ActualizaDatos()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmBloquesEditar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Eventos.SYSMONVAR(True)
        app_procesointerno = True
        Me.Text = "BLOCK EDITOR - v" & cfg._appversion
        If clsA Is Nothing Then clsA = New a2.A2acad(Eventos.COMApp, cfg._appFullPath, regAPPCliente)
        Bloque_PonDatos()
    End Sub

    Private Sub frmBloquesEditar_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Eventos.SYSMONVAR()
        frmBloE = Nothing
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
    '
    Public Sub Bloque_PonDatos()
        If Me.oBl Is Nothing Then Exit Sub
        dgvA.Rows.Clear()
        'pgD.PropertyTabs.Clear(System.ComponentModel.PropertyTabScope.Component)
        Dim colDatos As Hashtable = clsA.Bloque_AtributosDameTodos(Me.oBl.ObjectID)
        For Each queAtt As String In colDatos.Keys
            Dim oRow As New DataGridViewRow
            Dim oCname As New DataGridViewTextBoxCell
            oCname.Value = queAtt
            'Cname.ReadOnly = True
            Call oRow.Cells.Add(oCname)
            '
            If clsD.SELECCIONABLES.ContainsKey(queAtt) Then
                Dim oCvalue As New DataGridViewComboBoxCell
                oCvalue.Items.AddRange(clsD.SELECCIONABLES(queAtt).ToArray)
                oCvalue.Value = colDatos(queAtt)
                Call oRow.Cells.Add(oCvalue)
            Else
                Dim oCvalue As New DataGridViewTextBoxCell
                oCvalue.Value = colDatos(queAtt)
                Call oRow.Cells.Add(oCvalue)
            End If
            '
            dgvA.Rows.Add(oRow)
            oCname = Nothing
            oRow = Nothing
        Next
    End Sub
    Public Sub Bloque_ActualizaDatos()
        If Me.oBl Is Nothing Then Exit Sub
        For Each oRow As DataGridViewRow In dgvA.Rows
            Dim atributo As String = oRow.Cells.Item(0).Value
            Dim valor As String = oRow.Cells.Item(1).Value
            clsA.Bloque_AtributoEscribe(oBl.ObjectID, atributo, valor)
        Next
    End Sub

    Private Sub dgvA_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvA.CellMouseClick
        If dgvA.CurrentCell.ReadOnly = True Then Exit Sub
        '
        If TypeOf dgvA.CurrentCell Is DataGridViewTextBoxCell Then
            Dim oTb As DataGridViewTextBoxCell = dgvA.CurrentCell
            dgvA.BeginEdit(True)
        ElseIf TypeOf dgvA.CurrentCell Is DataGridViewComboBoxCell Then
            Dim oCb As DataGridViewComboBoxCell = dgvA.CurrentCell
            dgvA.BeginEdit(False)
            CType(dgvA.EditingControl, ComboBox).DroppedDown = True
        End If
    End Sub
End Class
