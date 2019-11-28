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
Imports uau = UtilesAlberto.Utiles
Imports a2 = AutoCAD2acad.A2acad
'
Public Class clsProxyML
    Public ELEMENTO As String = ""          ' Elemento
    Public DESCRIPCION As String = ""      ' Descripción
    'Public CTDAD As Double = 1             ' Ctdad
    Public MATERIAL As String = ""         ' Material
    Public TOTALUD As String = ""          ' Total Ud. 
    'Public TOTAL As Double = 0             ' TOTAL (CANTIDAD * TOTALUD)
    '
    Public colIds As List(Of Long)              ' Id de todos los Mleader iguales
    Public WithEvents oMl As AcadMLeader        ' Objeto AcadMLeader y control de eventos.
    Public campos As String() = New String() {"ELEMENTO", "DESCRIPCION", "CTDAD", "MATERIAL", "TOTALUD", "TOTAL"} ', "AREA", "LARGO", "ANCHO", "ESPESOR"}
    Public filtroclave As String = ""           ' Todas las propiedades concatenadas.
    '
    Public Sub New(oMlRefId As Long)
        oMl = CType(Eventos.COMDoc().ObjectIdToObject(oMlRefId), AcadMLeader)
        'AddHandler oMl.Modified, AddressOf oMl_Modified
        '
        ELEMENTO = clsA.MLeaderBlock_DameValorAtributo(oMl, "ELEMENTO")
        DESCRIPCION = clsA.MLeaderBlock_DameValorAtributo(oMl, "DESCRIPCION")
        'CTDAD = clsA.MLeaderBlock_DameValorAtributo(oMl, "CTDAD")
        MATERIAL = clsA.MLeaderBlock_DameValorAtributo(oMl, "MATERIAL")
        TOTALUD = clsA.MLeaderBlock_DameValorAtributo(oMl, "TOTALUD")
        'TOTAL = clsA.MLeaderBlock_DameValorAtributo(oMl, "TOTAL")
        '
        colIds = New List(Of Long)
        colIds.Add(oMlRefId)
        PonValorCorrectoTOTALUD()
    End Sub
    Public Sub New(oMlRef As AcadMLeader)
        Me.New(oMlRef.ObjectID)
    End Sub
    Public Sub AddMLeader(oMlRef As AcadMLeader)
        Dim oElem As String = clsA.MLeaderBlock_DameValorAtributo(oMlRef, "ELEMENTO")
        If ELEMENTO = oElem AndAlso colIds.Contains(oMlRef.ObjectID) = False Then
            colIds.Add(oMlRef.ObjectID)
        End If
    End Sub
    Public Sub AddMLeader(oMlRefId As Long)
        AddMLeader(clsA.oAppA.ActiveDocument.ObjectIdToObject(oMlRefId))
    End Sub
    Public Sub EraseMLeader(oMlRefId As Long)
        If colIds.Contains(oMlRefId) Then colIds.Remove(oMlRefId)
    End Sub
    Public Sub PonValorCorrectoTOTALUD()
        Dim txtMasa As String = TOTALUD ' colAtt("TOTALUD")
        txtMasa = txtMasa.ToUpper.Replace("KG", "").Trim
        txtMasa = txtMasa.ToUpper.Replace("GR", "").Trim
        If IsNumeric(txtMasa) Then
            txtMasa = txtMasa.Replace(",", ".")
        End If
        TOTALUD = txtMasa
        filtroclave = DESCRIPCION & MATERIAL & TOTALUD '& AREA & LARGO & ANCHO & ESPESOR
    End Sub
    ' Escribir los atributos del bloque externamente.
    ' Sólo los que tengan el mismo ID. Desde los datos de esta clase.
    Public Sub EscribeAtributosBloque()
        'Sólo los que tengan el mismo ID.
        Dim colError As New List(Of Long)
        ' Comprobar si cada ObjectId existe
        For Each queId As Long In colIds
            Dim oMl As AcadMLeader = Nothing
            Try
                oMl = clsA.oAppA.ActiveDocument.ObjectIdToObject(queId)
            Catch ex As Exception
                colError.Add(queId)
                Continue For
            Finally
                oMl = Nothing
            End Try
        Next
        ' Borrar cada id guardado en colIds que ya no existe.
        If colError.Count > 0 Then
            For Each queId1 As Long In colError
                'colIds.Remove(queId1)
                EraseMLeader(queId1)
            Next
        End If
        '
        ' Recorrer la lista final y poner los atributos
        Dim pesofin As String = CalculaPeso()
        For Each queId As Long In colIds
            Dim oMl As AcadMLeader = Nothing
            Try
                oMl = clsA.oAppA.ActiveDocument.ObjectIdToObject(queId)
                If oMl IsNot Nothing Then
                    '"ELEMENTO", "DESCRIPCION", "CTDAD", "MATERIAL", "TOTALUD", "TOTAL"
                    clsA.MLeaderBlock_PonValorAtributo(oMl, "ELEMENTO", ELEMENTO)
                    clsA.MLeaderBlock_PonValorAtributo(oMl, "DESCRIPCION", DESCRIPCION)
                    clsA.MLeaderBlock_PonValorAtributo(oMl, "CTDAD", colIds.Count)
                    clsA.MLeaderBlock_PonValorAtributo(oMl, "MATERIAL", MATERIAL)
                    clsA.MLeaderBlock_PonValorAtributo(oMl, "TOTALUD", TOTALUD)
                    clsA.MLeaderBlock_PonValorAtributo(oMl, "TOTAL", pesofin)
                End If
            Catch ex As Exception
                Continue For
            Finally
                oMl = Nothing
            End Try
        Next
        ' Concatenar las propiedades cambiantes (No las que calculamos o rellenamos por programacion)
        filtroclave = DESCRIPCION & MATERIAL & TOTALUD
    End Sub
    ' Escribir los atributos del bloque externamente.
    ' Sólo los que tengan el mismo ID. Desde los datos de otro clsProxyML
    Public Sub EscribeAtributosBloque(oPOri As clsProxyML)
        'CalculaPeso()
        'Sólo los que tengan el mismo ID.
        Dim colError As New List(Of Long)
        ' Comprobar si cada ObjectId existe
        For Each queId As Long In colIds
            Dim oMl As AcadMLeader = Nothing
            Try
                oMl = clsA.oAppA.ActiveDocument.ObjectIdToObject(queId)
            Catch ex As Exception
                colError.Add(queId)
                Continue For
            End Try
        Next
        ' Borrar cada id guardado en colIds que ya no existe.
        If colError.Count > 0 Then
            For Each queId1 As Long In colError
                'colIds.Remove(queId1)
                EraseMLeader(queId1)
            Next
        End If
        ' Recorrer la lista final y poner los atributos
        Dim pesofin As String = oPOri.CalculaPeso()
        For Each queId As Long In colIds
            Dim oMl As AcadMLeader = Nothing
            Try
                oMl = clsA.oAppA.ActiveDocument.ObjectIdToObject(queId)
                If oMl IsNot Nothing Then
                    '"ELEMENTO", "DESCRIPCION", "CTDAD", "MATERIAL", "TOTALUD", "TOTAL"
                    clsA.MLeaderBlock_PonValorAtributo(oMl, "ELEMENTO", oPOri.ELEMENTO)
                    clsA.MLeaderBlock_PonValorAtributo(oMl, "DESCRIPCION", oPOri.DESCRIPCION)
                    clsA.MLeaderBlock_PonValorAtributo(oMl, "CTDAD", oPOri.colIds.Count)
                    clsA.MLeaderBlock_PonValorAtributo(oMl, "MATERIAL", oPOri.MATERIAL)
                    clsA.MLeaderBlock_PonValorAtributo(oMl, "TOTALUD", oPOri.TOTALUD)
                    clsA.MLeaderBlock_PonValorAtributo(oMl, "TOTAL", pesofin)
                End If
            Catch ex As Exception
                Continue For
            End Try
        Next
        ' Concatenar las propiedades cambiantes (No las que calculamos o rellenamos por programacion)
        filtroclave = DESCRIPCION & MATERIAL & TOTALUD
    End Sub
    Public Sub EscribeAtributosBloque1()
        CalculaPeso()
        'Sólo los que tengan el mismo ID.
        Dim colError As New List(Of Long)
        ' Comprobar si cada ObjectId existe
        For Each queId As Long In colIds
            Dim oMl As AcadMLeader = Nothing
            Try
                oMl = clsA.oAppA.ActiveDocument.ObjectIdToObject(queId)
            Catch ex As Exception
                colError.Add(queId)
                Continue For
            End Try
        Next
        ' Borrar cada id guardado en colIds que ya no existe.
        If colError.Count > 0 Then
            For Each queId1 As Long In colError
                'colIds.Remove(queId1)
                EraseMLeader(queId1)
            Next
        End If
        ' Recorrer la lista final y poner los atributos
        Dim pesofin As String = CalculaPeso()
        For Each queId As Long In colIds
            Dim oMl As AcadMLeader = Nothing
            Try
                oMl = clsA.oAppA.ActiveDocument.ObjectIdToObject(queId)
                If oMl IsNot Nothing Then
                    '"ELEMENTO", "DESCRIPCION", "CTDAD", "MATERIAL", "TOTALUD", "TOTAL"
                    clsA.MLeaderBlock_PonValorAtributo(oMl, "ELEMENTO", ELEMENTO)
                    clsA.MLeaderBlock_PonValorAtributo(oMl, "DESCRIPCION", DESCRIPCION)
                    clsA.MLeaderBlock_PonValorAtributo(oMl, "CTDAD", colIds.Count)
                    clsA.MLeaderBlock_PonValorAtributo(oMl, "MATERIAL", MATERIAL)
                    clsA.MLeaderBlock_PonValorAtributo(oMl, "TOTALUD", TOTALUD)
                    clsA.MLeaderBlock_PonValorAtributo(oMl, "TOTAL", pesofin)
                End If
            Catch ex As Exception
                Continue For
            End Try
        Next
        ' Concatenar las propiedades cambiantes (No las que calculamos o rellenamos por programacion)
        filtroclave = DESCRIPCION & MATERIAL & TOTALUD
    End Sub
    Public Function CalculaPeso() As String
        Dim TOTAL As String = ""
        If TOTALUD <> "" AndAlso IsNumeric(TOTALUD) Then
            TOTAL = colIds.Count * CDbl(TOTALUD)
        End If
        Return TOTAL
    End Function
    '
    Public Sub RowAdd(ByRef oTable As System.Data.DataTable)
        CalculaPeso()
        Dim oRow As System.Data.DataRow = oTable.NewRow
        oRow("ELEMENTO") = Me.ELEMENTO                      ' Elemento
        oRow("DESCRIPCION") = Me.DESCRIPCION    ' Descripción
        oRow("CTDAD") = colIds.Count         ' Ctdad
        oRow("MATERIAL") = Me.MATERIAL          ' Material
        oRow("TOTALUD") = Me.TOTALUD                  ' Total Ud.
        oRow("TOTAL") = CalculaPeso()        ' TOTAL
        oTable.Rows.InsertAt(oRow, oTable.Rows.Count)
    End Sub
    'Private Sub oMl_Modified(pObject As AcadObject)
    '    '    'clsP.PonDatosProxy(pObject.ObjectID)
    '    '    MsgBox(pObject.ObjectID)
    'End Sub
End Class
