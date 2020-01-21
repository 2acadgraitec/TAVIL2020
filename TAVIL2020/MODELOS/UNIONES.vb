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
    Public Shared Sub UNION_Crea(handle As String)
        If LUniones Is Nothing Then LUniones = New List(Of UNION)
        If NUniones Is Nothing Then NUniones = New Dictionary(Of String, Integer)
        Dim oUnion As UNION = New UNION(handle)
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

        Dim columnas() As String = {"BLOCK", "COUNT", "UNION", "UNITS", "ANGLE", "ANGLEL", "ANGLER"}
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
            texto &= oU.NAME & ";" & NUniones(key) & ";" & oU.UNION.Replace(";", "|") & ";" & oU.UNITS.Replace(";", "|") & ";" & oU.ANGLE & ";" & oU.ANGLEL & ";" & oU.ANGLER & vbCrLf
        Next
        texto = texto.Substring(0, texto.Length - 2)
        IO.File.WriteAllText(fiOut, texto, Text.Encoding.UTF8)
        If IO.File.Exists(fiOut) Then Process.Start(fiOut)
    End Sub
End Class
Public Class UNION
    Public Property HANDLE As String
    Public Property NAME As String
    Public Property UNION As String
    Public Property UNITS As String
    Public Property T1INFEED As String
    Public Property T1INFEEDL As String
    Public Property T1INFEEDR As String
    Public Property T2OUTFEED As String
    Public Property T2OUTFEEDL As String
    Public Property T2OUTFEEDR As String

    Private Property _ANGLE As String
    Private Property _ANGLEL As String
    Private Property _ANGLER As String
    Public Property HOJA As String = "UNIONES"
    Public Property KEY As String        ' Todas las propiedades concatenadas.
    'Public Property DUNIONUNITS As Dictionary(Of String, Integer)

    Public Property ANGLE As String
        Set(value As String)
            If (value = "" OrElse value Is Nothing) Then value = "0"
            _ANGLE = value
        End Set
        Get
            Return _ANGLE
        End Get
    End Property
    Public Property ANGLEL As String
        Set(value As String)
            If (value = "" OrElse value Is Nothing) Then value = "0"
            _ANGLEL = value
        End Set
        Get
            Return _ANGLEL
        End Get
    End Property
    Public Property ANGLER As String
        Set(value As String)
            If (value = "" OrElse value Is Nothing) Then value = "0"
            _ANGLER = value
        End Set
        Get
            Return _ANGLER
        End Get
    End Property
    '
    Public Sub New(handle As String) 'Handle
        'DUNIONUNITS = New Dictionary(Of String, Integer)
        Dim acadObj As AcadObject = Eventos.COMDoc.HandleToObject(handle)
        If acadObj IsNot Nothing AndAlso TypeOf acadObj Is AcadBlockReference Then
            Dim oBl As AcadBlockReference = acadObj
            Dim oBlDatos As New AutoCAD2acad.A2acad.Bloque_Datos(oBl)
            Me.HANDLE = handle
            Me.NAME = oBl.EffectiveName
            Me.UNION = clsA.Bloque_DameDato_AttPropX(oBlDatos, "UNION")
            Me.UNITS = clsA.Bloque_DameDato_AttPropX(oBlDatos, "UNITS")
            Me.T1INFEED = clsA.Bloque_DameDato_AttPropX(oBlDatos, "T1INFEED")
            Me.T1INFEEDL = clsA.Bloque_DameDato_AttPropX(oBlDatos, "T1INFEEDL")
            Me.T1INFEEDR = clsA.Bloque_DameDato_AttPropX(oBlDatos, "T1INFEEDR")
            Me.T2OUTFEED = clsA.Bloque_DameDato_AttPropX(oBlDatos, "T2OUTFEED")
            Me.T2OUTFEEDL = clsA.Bloque_DameDato_AttPropX(oBlDatos, "T2OUTFEEDL")
            Me.T2OUTFEEDR = clsA.Bloque_DameDato_AttPropX(oBlDatos, "T2OUTFEEDR")
            Me.ANGLE = clsA.Bloque_DameDato_AttPropX(oBlDatos, "ANGLE")
            Me.ANGLE = clsA.Bloque_DameDato_AttPropX(oBlDatos, "ANGLEL")
            Me.ANGLE = clsA.Bloque_DameDato_AttPropX(oBlDatos, "ANGLER")
            KEY = UNION & UNITS & T1INFEED & T1INFEEDL & T1INFEEDR & T2OUTFEED & T2OUTFEEDL & T2OUTFEEDR & ANGLE & ANGLEL & ANGLER
            ' Poner la hoja correspondiente

        End If
    End Sub
End Class
