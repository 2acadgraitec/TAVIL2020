Imports Autodesk.AutoCAD.Interop.Common
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.ApplicationServices
Imports TAVIL2020.TAVIL2020
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Linq
Imports a2 = AutoCAD2acad.A2acad
Public Class UCUnion
    Public Shared Event TodoSeleccionado(correcto As Boolean)
    Public Shared Event HayCambioDatos()
    'Public Shared Event CambioAlgo()

    Private Sub UCUnion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Botones_PonToolTip()
    End Sub

    Private Sub UCUnion_EnabledChanged(sender As Object, e As EventArgs) Handles Me.EnabledChanged
        Botones_PonColor()
    End Sub

    Public Sub Botones_PonToolTip()
        oTt = New ToolTip()
        oTt.AutoPopDelay = 5000 ' Tiempo que estará visible
        oTt.InitialDelay = 500  ' Tiempo inicial para mostrarse
        oTt.ReshowDelay = 100   ' Tiempo de espera entre controles
        oTt.ShowAlways = True   ' Forzar a que se muestre el tooltip, aunque no este activo el Form.
        '
        oTt.SetToolTip(BtnT1, "Seleccionar Transportador 1")
        oTt.SetToolTip(BtnT2, "Seleccionar Transportador 2")
    End Sub
    Public Sub Botones_PonColor()
        If frmUniones.UltimoBloqueT1 IsNot Nothing Then
            BtnT1.BackColor = btnOn
        Else
            BtnT1.BackColor = btnOff
        End If
        If frmUniones.UltimoBloqueT2 IsNot Nothing Then
            BtnT2.BackColor = btnOn
        Else
            BtnT2.BackColor = btnOff
        End If
    End Sub

    Private Sub BtnT1_Click(sender As Object, e As EventArgs) Handles BtnT1.Click
        frmUn.Visible = False
        clsA.ActivaAppAPI()
        Dim obl As AcadBlockReference = clsA.Bloque_SeleccionaDame
        If obl IsNot Nothing Then
            BtnT1.BackColor = btnOn
            frmUniones.T1HANDLE = obl.Handle
            frmUniones.UltimoBloqueT1 = obl
            RaiseEvent HayCambioDatos()
        End If
        Controles_Comprueba()
        frmUn.Visible = True
    End Sub

    Private Sub LbIncT1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LbIncT1.SelectedIndexChanged
        If LbIncT1.SelectedIndex = -1 Then Exit Sub
        frmUniones.T1INCLINATION = LbIncT1.SelectedItem.ToString
        Controles_Comprueba()
        RaiseEvent HayCambioDatos()
    End Sub

    Private Sub BtnT2_Click(sender As Object, e As EventArgs) Handles BtnT2.Click
        frmUn.Visible = False
        clsA.ActivaAppAPI()
        Dim obl As AcadBlockReference = clsA.Bloque_SeleccionaDame
        If obl IsNot Nothing Then
            BtnT2.BackColor = btnOn
            frmUniones.T2HANDLE = obl.Handle
            frmUniones.UltimoBloqueT2 = obl
            RaiseEvent HayCambioDatos()
        End If
        frmUniones.UltimoBloqueT2 = clsA.Bloque_SeleccionaDame
        Controles_Comprueba()
        frmUn.Visible = True
    End Sub

    Private Sub LbIncT2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LbIncT2.SelectedIndexChanged
        If LbIncT2.SelectedIndex = -1 Then Exit Sub
        frmUniones.T2INCLINATION = LbIncT2.SelectedItem.ToString
        Controles_Comprueba()
        RaiseEvent HayCambioDatos()
    End Sub

    Private Sub LbAngle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LbAngle.SelectedIndexChanged
        If LbAngle.SelectedIndex = -1 Then Exit Sub
        frmUniones.ANGLE = LbAngle.SelectedItem.ToString
        Controles_Comprueba()
        RaiseEvent HayCambioDatos()
    End Sub

    Private Sub LbLado_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LbLado.SelectedIndexChanged
        If LbLado.SelectedIndex = -1 Then Exit Sub
        frmUniones.LADO = LbLado.SelectedItem.ToString
        Controles_Comprueba()
        RaiseEvent HayCambioDatos()
    End Sub
    Public Sub Controles_Comprueba()
        If frmUniones.UltimoBloqueT1 IsNot Nothing AndAlso
           frmUniones.UltimoBloqueT2 IsNot Nothing AndAlso
           LbIncT1.SelectedIndex > -1 AndAlso
           LbIncT2.SelectedIndex > -1 AndAlso
           LbAngle.SelectedIndex > -1 AndAlso
           LbLado.SelectedIndex > -1 Then
            RaiseEvent TodoSeleccionado(True)
        Else
            RaiseEvent TodoSeleccionado(False)
        End If
    End Sub
End Class
