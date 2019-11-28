Imports Autodesk.AutoCAD.Interop.Common
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.ApplicationServices
Imports TAVIL2020.TAVIL2020
Imports System.Windows.Forms
Imports System.Linq
Imports a2 = AutoCAD2acad.A2acad

Public Class GRUPOS
    Public Shared LGrupos As New List(Of GRUPO)
    Public Shared DGrupos As New Dictionary(Of String, GRUPO)
End Class
Public Class GRUPO
    Public name As String
    Public lMembers As New List(Of String)  ' hast
End Class