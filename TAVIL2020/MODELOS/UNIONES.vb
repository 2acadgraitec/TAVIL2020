Imports Autodesk.AutoCAD.Interop.Common
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.ApplicationServices
Imports TAVIL2020.TAVIL2020
Imports System.Windows.Forms
Imports System.Linq
Imports a2 = AutoCAD2acad.A2acad
Public Class UNIONES
    Public Shared LUniones As New List(Of UNION)                ' Todas las uniones
    Public Shared NUniones As New Dictionary(Of String, Integer)  ' Totales de Uniones
    Public Shared Sub UNION_Crea(h As String)
        If LUniones Is Nothing Then LUniones = New List(Of UNION)
        If NUniones Is Nothing Then NUniones = New Dictionary(Of String, Integer)
        Dim oUnion As UNION = New UNION(h)
        LUniones.Add(oUnion)
        If NUniones.ContainsKey(oUnion.KEY) Then
            NUniones(oUnion.KEY) += 1
        Else
            NUniones.Add(oUnion.KEY, 1)
        End If
    End Sub

    Public Shared Sub Report_UNIONES()
        If LUniones Is Nothing OrElse NUniones Is Nothing Then
            Exit Sub
        End If
        '
        Dim columnas() As String = {"BLOCK", "COUNT", "UNION", "UNITS", "ROTATION"}
        Dim fiOut As String = IO.Path.ChangeExtension(Eventos.COMDoc.Path, "UNIONS.csv")
        If IO.File.Exists(fiOut) Then IO.File.Delete(fiOut)
        Dim texto As String = String.Join(";", columnas) & vbCrLf
        '
        For Each key As String In NUniones.Keys
            Dim uni = From x In LUniones
                      Where x.KEY = key
                      Select x

            Dim oU As UNION = Nothing
            If uni IsNot Nothing AndAlso uni.Count > 0 Then
                oU = uni.First
            Else
                Continue For
            End If
            '
            texto &= oU.NAME & ";" & NUniones(key) & ";" & oU.UNION.Replace(";", "|") & ";" & oU.UNITS.Replace(";", "|") & ";" & oU.ROTATION & vbCrLf
        Next
        texto = texto.Substring(0, texto.Length - 2)
        IO.File.WriteAllText(fiOut, texto, Text.Encoding.UTF8)
        If IO.File.Exists(fiOut) Then Process.Start(fiOut)
    End Sub
End Class
Public Class UNION
    Public HANDLE As String
    Public NAME As String
    Public UNION As String
    Public UNITS As String
    Public T1INFEED As String
    Public T2OUTFEED As String
    Private _ROTATION As String
    Public KEY As String        ' Todas las propiedades concatenadas.
    Public Property ROTATION As String
        Set(value As String)
            If value = "" Then value = "0"
            _ROTATION = value
        End Set
        Get
            Return _ROTATION
        End Get
    End Property
    '
    Public Sub New(h As String) 'Handle
        Dim acadObj As AcadObject = Eventos.COMDoc.HandleToObject(h)
        If acadObj IsNot Nothing AndAlso TypeOf acadObj Is AcadBlockReference Then
            Dim oBl As AcadBlockReference = acadObj
            Dim oBlDatos As New AutoCAD2acad.A2acad.Bloque_Datos(oBl)
            Me.HANDLE = h
            Me.NAME = oBl.EffectiveName
            Me.UNION = clsA.Bloque_DameDato_AttPropX(oBlDatos, "UNION")
            Me.UNITS = clsA.Bloque_DameDato_AttPropX(oBlDatos, "UNITS")
            Me.T1INFEED = clsA.Bloque_DameDato_AttPropX(oBlDatos, "T1INFEED")
            Me.T2OUTFEED = clsA.Bloque_DameDato_AttPropX(oBlDatos, "T2OUTFEED")
            Me.ROTATION = clsA.Bloque_DameDato_AttPropX(oBlDatos, "ROTATION")
            KEY = UNION & UNITS & T1INFEED & T2OUTFEED & ROTATION
        End If
    End Sub
End Class
