Imports Autodesk.AutoCAD.Interop.Common
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.ApplicationServices
Imports TAVIL2020.TAVIL2020
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Linq
Imports a2 = AutoCAD2acad.A2acad
Public Class UCUnionY
    Public Shared Event TodoSeleccionado(correcto As Boolean)
    Public Shared Event HayCambioDatos()
    Public editando As Boolean = False
    Private Sub UCUnionY_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Botones_PonToolTip()
    End Sub

    Private Sub UCUniony_EnabledChanged(sender As Object, e As EventArgs) Handles Me.EnabledChanged
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
        oTt.SetToolTip(BtnT2L, "Seleccionar Transportador 2 (Izquierda)")
        oTt.SetToolTip(BtnT2R, "Seleccionar Transportador 2 (Derecha)")
    End Sub
    Public Sub Botones_PonColor()
        If frmUniones.UltimoBloqueT1 IsNot Nothing Then
            BtnT1.BackColor = btnOn
        Else
            BtnT1.BackColor = btnOff
        End If
        If frmUniones.UltimoBloqueT2R IsNot Nothing Then
            BtnT2R.BackColor = btnOn
        Else
            BtnT2R.BackColor = btnOff
        End If
        If frmUniones.UltimoBloqueT2L IsNot Nothing Then
            BtnT2L.BackColor = btnOn
        Else
            BtnT2L.BackColor = btnOff
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

    Private Sub BtnT2R_Click(sender As Object, e As EventArgs) Handles BtnT2R.Click
        frmUn.Visible = False
        clsA.ActivaAppAPI()
        Dim obl As AcadBlockReference = clsA.Bloque_SeleccionaDame
        If obl IsNot Nothing Then
            BtnT2R.BackColor = btnOn
            frmUniones.T2HANDLER = obl.Handle
            frmUniones.UltimoBloqueT2R = obl
            RaiseEvent HayCambioDatos()
        End If
        Controles_Comprueba()
        frmUn.Visible = True
    End Sub

    Private Sub LbAngleR_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LbAngleR.SelectedIndexChanged
        If LbAngleR.SelectedIndex = -1 Then Exit Sub
        frmUniones.ANGLER = LbAngleR.SelectedItem.ToString
        Controles_Comprueba()
        RaiseEvent HayCambioDatos()
    End Sub

    Private Sub LbIncT2R_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LbIncT2R.SelectedIndexChanged
        If LbIncT2R.SelectedIndex = -1 Then Exit Sub
        frmUniones.T2INCLINATIONR = LbIncT2R.SelectedItem.ToString
        Controles_Comprueba()
        RaiseEvent HayCambioDatos()
    End Sub

    Private Sub BtnT2L_Click(sender As Object, e As EventArgs) Handles BtnT2L.Click
        frmUn.Visible = False
        clsA.ActivaAppAPI()
        Dim obl As AcadBlockReference = clsA.Bloque_SeleccionaDame
        If obl IsNot Nothing Then
            BtnT2L.BackColor = btnOn
            frmUniones.T2HANDLEL = obl.Handle
            frmUniones.UltimoBloqueT2L = obl
            RaiseEvent HayCambioDatos()
        End If
        Controles_Comprueba()
        frmUn.Visible = True
    End Sub

    Private Sub LbAngleL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LbAngleL.SelectedIndexChanged
        If LbAngleL.SelectedIndex = -1 Then Exit Sub
        frmUniones.ANGLEL = LbAngleL.SelectedItem.ToString
        Controles_Comprueba()
        RaiseEvent HayCambioDatos()
    End Sub

    Private Sub LbIncT2L_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LbIncT2L.SelectedIndexChanged
        If LbIncT2L.SelectedIndex = -1 Then Exit Sub
        frmUniones.T2INCLINATIONL = LbIncT2L.SelectedItem.ToString
        Controles_Comprueba()
        RaiseEvent HayCambioDatos()
    End Sub


    Public Sub Controles_Comprueba()
        If frmUniones.UltimoBloqueT1 IsNot Nothing AndAlso
           frmUniones.UltimoBloqueT2R IsNot Nothing AndAlso
           frmUniones.UltimoBloqueT2L IsNot Nothing AndAlso
           LbIncT1.SelectedIndex > -1 AndAlso
           LbIncT2R.SelectedIndex > -1 AndAlso
           LbIncT2L.SelectedIndex > -1 AndAlso
           LbAngleR.SelectedIndex > -1 AndAlso
           LbAngleL.SelectedIndex > -1 Then
            RaiseEvent TodoSeleccionado(True)
        Else
            RaiseEvent TodoSeleccionado(False)
        End If
    End Sub
End Class
