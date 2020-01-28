Imports Autodesk.AutoCAD.Interop.Common
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.ApplicationServices
Imports TAVIL2020.TAVIL2020
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Linq
Imports a2 = AutoCAD2acad.A2acad
Public Class UCUnionX
    Public Shared Event TodoSeleccionado(correcto As Boolean)
    Public Shared Event HayCambioDatos()
    Public editando As Boolean = False
    Private Sub UCUnionX_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Botones_PonToolTip()
    End Sub

    Private Sub UCUnionX_EnabledChanged(sender As Object, e As EventArgs) Handles Me.EnabledChanged
        Botones_PonColor()
    End Sub

    Public Sub Botones_PonToolTip()
        oTt = New ToolTip()
        oTt.AutoPopDelay = 5000 ' Tiempo que estará visible
        oTt.InitialDelay = 500  ' Tiempo inicial para mostrarse
        oTt.ReshowDelay = 100   ' Tiempo de espera entre controles
        oTt.ShowAlways = True   ' Forzar a que se muestre el tooltip, aunque no este activo el Form.
        '
        oTt.SetToolTip(BtnT1L, "Seleccionar Transportador 1 (Inquierda)")
        oTt.SetToolTip(BtnT1R, "Seleccionar Transportador 1 (Derecha)")
        oTt.SetToolTip(BtnT2, "Seleccionar Transportador 2")
    End Sub
    Public Sub Botones_PonColor()
        If frmUniones.UltimoBloqueT1R IsNot Nothing Then
            BtnT1R.BackColor = btnOn
        Else
            BtnT1R.BackColor = btnOff
        End If
        If frmUniones.UltimoBloqueT1L IsNot Nothing Then
            BtnT1L.BackColor = btnOn
        Else
            BtnT1L.BackColor = btnOff
        End If
        If frmUniones.UltimoBloqueT2 IsNot Nothing Then
            BtnT2.BackColor = btnOn
        Else
            BtnT2.BackColor = btnOff
        End If
    End Sub

    Private Sub BtnT1R_Click(sender As Object, e As EventArgs) Handles BtnT1R.Click
        frmUn.Visible = False
        clsA.ActivaAppAPI()
        Dim obl As AcadBlockReference = clsA.Bloque_SeleccionaDame
        If obl IsNot Nothing Then
            BtnT1R.BackColor = btnOn
            frmUniones.T1HANDLER = obl.Handle
            frmUniones.UltimoBloqueT1R = obl
            RaiseEvent HayCambioDatos()
        End If
        Controles_Comprueba()
        frmUn.Visible = True
    End Sub

    Private Sub LbIncT1R_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LbIncT1R.SelectedIndexChanged
        If LbIncT1R.SelectedIndex = -1 Then Exit Sub
        frmUniones.T1INCLINATIONR = LbIncT1R.SelectedItem.ToString
        Controles_Comprueba()
    End Sub

    Private Sub BtnT1L_Click(sender As Object, e As EventArgs) Handles BtnT1L.Click
        frmUn.Visible = False
        clsA.ActivaAppAPI()
        Dim obl As AcadBlockReference = clsA.Bloque_SeleccionaDame
        If obl IsNot Nothing Then
            BtnT1L.BackColor = btnOn
            frmUniones.T1HANDLEL = obl.Handle
            frmUniones.UltimoBloqueT1L = obl
            RaiseEvent HayCambioDatos()
        End If
        Controles_Comprueba()
        frmUn.Visible = True
    End Sub

    Private Sub LbIncT1L_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LbIncT1L.SelectedIndexChanged
        If LbIncT1L.SelectedIndex = -1 Then Exit Sub
        frmUniones.T1INCLINATIONL = LbIncT1L.SelectedItem.ToString
        Controles_Comprueba()
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
        Controles_Comprueba()
        frmUn.Visible = True
    End Sub

    Private Sub LbAngle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LbAngle.SelectedIndexChanged
        If LbAngle.SelectedIndex = -1 Then Exit Sub
        frmUniones.ANGLE = LbAngle.SelectedItem.ToString
        Controles_Comprueba()
    End Sub

    Private Sub LbIncT2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LbIncT2.SelectedIndexChanged
        If LbIncT2.SelectedIndex = -1 Then Exit Sub
        frmUniones.T2INCLINATION = LbIncT2.SelectedItem.ToString
        Controles_Comprueba()
    End Sub

    Public Sub Controles_Comprueba()
        If frmUniones.UltimoBloqueT1R IsNot Nothing AndAlso
           frmUniones.UltimoBloqueT1L IsNot Nothing AndAlso
           frmUniones.UltimoBloqueT2 IsNot Nothing AndAlso
           LbIncT1R.SelectedIndex > -1 AndAlso
           LbIncT1L.SelectedIndex > -1 AndAlso
           LbAngle.SelectedIndex > -1 AndAlso
           LbIncT2.SelectedIndex > -1 Then
            RaiseEvent TodoSeleccionado(True)
        Else
            RaiseEvent TodoSeleccionado(False)
        End If
    End Sub
End Class
