Imports System.Windows.Forms
Imports System.Diagnostics
Imports System.Collections
Imports System.Drawing
Imports System.Windows.FrameworkCompatibilityPreferences


Imports Autodesk.AutoCAD.Interop
Imports Autodesk.AutoCAD.Interop.Common
Imports Autodesk.AutoCAD.Runtime
Imports AXApp = Autodesk.AutoCAD.ApplicationServices.Application
Imports AXDoc = Autodesk.AutoCAD.ApplicationServices.Document
Imports AXWin = Autodesk.AutoCAD.Windows
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.EditorInput
'
Public Class clsProxyMLCol
    Public colP As Dictionary(Of String, clsProxyML)    ' Key=ELEMENTO (Atributo), Value=clsProxy
    Public oTable As System.Data.DataTable
    '
    Public Sub New()
        colP = New Dictionary(Of String, clsProxyML)
        oTable = New System.Data.DataTable("PROXIES")
        oTable.Columns.Add("ELEMENTO")      ' Elemento
        oTable.Columns.Add("DESCRIPCION")   ' Descripción
        oTable.Columns.Add("CTDAD")         ' Ctdad
        oTable.Columns.Add("MATERIAL")      ' Material
        oTable.Columns.Add("TOTALUD")       ' Total Ud.
        oTable.Columns.Add("TOTAL")         ' TOTAL
        ProxiesRellena()
    End Sub
    Public Sub ProxiesRellena()
        Dim arrMl As ArrayList = clsA.MleaderDameTodos_PorNombreBloque("BloqueProxy")
        If arrMl IsNot Nothing AndAlso arrMl.Count > 0 Then
            ' Poner los datos de cada bloque = BloqueProxy
            For Each oMl As AcadMLeader In arrMl
                PonDatosProxy(oMl.ObjectID)
            Next
            ' Rellenar la tabla con todos los datos.
            For Each clsP As clsProxyML In colP.Values
                clsP.RowAdd(oTable)
            Next
        End If
    End Sub
    Public Sub PonDatosProxy(oMlId As Long)
        Dim clsP As New clsProxyML(oMlId)
        If colP.ContainsKey(clsP.ELEMENTO) = True Then
            colP(clsP.ELEMENTO).AddMLeader(oMlId)
        ElseIf colP.ContainsKey(clsP.ELEMENTO) = False Then
            colP.Add(clsP.ELEMENTO, clsP)
        End If
        clsP = Nothing
    End Sub
End Class
