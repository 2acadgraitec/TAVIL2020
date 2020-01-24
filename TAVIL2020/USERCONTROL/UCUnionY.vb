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
    Private Sub UCUnion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PonToolTipControles()
    End Sub

    Public Sub PonToolTipControles()
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
End Class
