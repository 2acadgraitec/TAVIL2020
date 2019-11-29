Imports Autodesk.AutoCAD.Interop.Common
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.ApplicationServices
Imports TAVIL2020.TAVIL2020
Imports System.Windows.Forms
Imports System.Linq
Imports a2 = AutoCAD2acad.A2acad
Public Class PATAS
    Public Shared DPatas As Dictionary(Of String, PATA)
End Class

Public Class PATA
    Public Property HANDLE As String
    Public Property CODE As String
    Public Property BLOCK As String
    Public Property HEIGHT As String
    Public Property WIDTH As String
    Public Property RADIUS As String
    Public Property KEY As String
    '
    Public Sub New(h As String) 'Handle
        Dim acadObj As AcadObject = Eventos.COMDoc.HandleToObject(h)
        If acadObj IsNot Nothing AndAlso TypeOf acadObj Is AcadBlockReference Then
            Dim oBl As AcadBlockReference = CType(acadObj, AcadBlockReference)
            Dim oBlDatos As New AutoCAD2acad.A2acad.Bloque_Datos(oBl)
            Me.HANDLE = oBl.Handle
            Me.CODE = clsA.Bloque_DameDato_AttPropX(oBlDatos, "CODE")
            Me.BLOCK = oBlDatos.eName   ' clsA.Bloque_DameDato_AttPropX(oBlDatos, "BLOCK")
            Me.HEIGHT = clsA.Bloque_DameDato_AttPropX(oBlDatos, "HEIGHT")
            Me.WIDTH = clsA.Bloque_DameDato_AttPropX(oBlDatos, "WIDTH")
            Me.RADIUS = clsA.Bloque_DameDato_AttPropX(oBlDatos, "RADIUS")
            KEY = CODE & BLOCK & HEIGHT & WIDTH & RADIUS
            oBl = Nothing
            acadObj = Nothing
        End If
    End Sub
End Class
