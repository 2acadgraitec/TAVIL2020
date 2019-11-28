
' Idioma Ingles para todo el desarollo.
Public Class clsLAYOUTDB
    Public arrNombresHojasDatos() As String = New String() {"TRD3", "TCB3", "PT"}
    Public Const TRD3Hoja As String = "TRD3"
    Public Const TCB3Hoja As String = "TCB3"
    Public Const PTHoja As String = "PT"
    Public Const UNIONESHoja As String = "UNIONES"
    Public Const SELECCIONABLESHoja As String = "SELECCIONABLES"
    Public Const IDIOMASHoja As String = "IDIOMAS"
    Public Const esp As String = "_"
    '
    ' Fila 1. Propiedades de todos los TRD3, TCB3 y PT (Cada uno en una hoja de LAYOUTDB.xlsx)
    ' Todos tendrán las mismas propiedades. Algunas rellenas y otras no.
    Public arrPropCintas() As String = New String() {
        "BLOCK", "LENGTH", "WIDTH", "WEIGHT", "HAND", "MOTOR_POSITION", "MOTOR_OFFSET", "MOTOR_TYPE", "FLAT_BELT",
        "O_RING_BELT", "DESCRIPTION", "ESPECIFICATION", "INFEED_HEIGHT", "OUTFEED_HEIGHT", "ITEM_NUMBER",
        "O_RING_PROTECTION", "STANDARD_PART", "CONVEYOR_BELT_TYPE", "UNDERBELT", "UNDER_CONVEYOR_PROTECTION",
        "CONVEYOR_BELT_REFERENCE", "RADIUS", "Nº_MOTORS", "SUPPORT_TYPE", "REAL_LENGHT",
        "CONVEYOR_HEIGHT_1", "CONVEYOR_HEIGHT_2", "CONVEYOR_HEIGHT_3", "CONVEYOR_HEIGHT_4",
        "ANGLE", "FRAME", "CODE", "P1", "P2"}
    ' Fila 1. Propiedades de UNIONES (hoja UNIONES=UNIONESHoja)
    Public arrPropUniones() As String = New String() {"INFEED_CONVEYOR", "OUTFEED_CONVEYOR", "UNION", "TYPE", "UNITS"}
    ' Columna 1. Nombre de todas las propiedades que tendrás desplegables con valores (Hoja SELECCIONABLES=SELCCIONABLESHoja)
    ' En cada fila habrá: Nombre de la propiedad en columna 1. Resto de columnas: un valor en cada columna (2 hasta la última)
    Public SELECCIONABLES As Dictionary(Of String, List(Of String))      ' coleccion de desplegables
    'Public TRD3 As Dictionary(Of String, Dictionary(Of String, String))
    'Public TCB3 As Dictionary(Of String, Dictionary(Of String, String))
    'Public PT As Dictionary(Of String, Dictionary(Of String, String))
    Public DATOS As Dictionary(Of String, Dictionary(Of String, String))
    '
    Public Sub New()
        LlenaDatosExcelDB()
    End Sub
    '
    Public Sub LlenaDatosExcelDB()
        Dim SELECCIONABLESTemp As Dictionary(Of String, List(Of String)) = Excel_DameDesplegablesEnFilas(LAYOUTDB, SELECCIONABLESHoja)
        QuitaEspaciosDictionaryList(SELECCIONABLESTemp)
        'ImprimeDictionaryList(SELECCIONABLES)
        'TRD3 = Excel_DameFormato_DB(LAYOUTDB, TRD3Hoja, 1)
        'ImprimeDictionaryDictionary(TRD3)
        'TCB3 = Excel_DameFormato_DB(LAYOUTDB, TCB3Hoja, 1)
        'ImprimeDictionaryDictionary(TCB3)
        'PT = Excel_DameFormato_DB(LAYOUTDB, PTHoja, 1)
        'ImprimeDictionaryDictionary(PT)
        Dim DATOSTemp As Dictionary(Of String, Dictionary(Of String, String)) = Excel_DameFormato_DB_Hojas(LAYOUTDB, arrNombresHojasDatos, 1)
        QuitaEspaciosDictionaryDictionary(DATOSTemp)
        'ImprimeDictionaryDictionary(DATOS)
        dicBloques_PonNombresBloques(dicBloques)
    End Sub
    '
    Public Sub dicBloques_PonNombresBloques(ByRef quedicBloques As Dictionary(Of String, String))
        For Each queNomBloTemp As String In DATOS.Keys
            Dim queNomBlo As String = queNomBloTemp.Split("·"c)(0)
            If quedicBloques.ContainsKey(queNomBlo) = True Then Continue For
            '
            Dim colFi() As String = IO.Directory.GetFiles(dirBloques, queNomBlo & ".png", IO.SearchOption.AllDirectories)
            If colFi IsNot Nothing AndAlso colFi.Length > 0 Then
                quedicBloques.Add(queNomBlo, IO.Path.GetDirectoryName(colFi(0)))
            Else
                quedicBloques.Add(queNomBlo, dirBloques)
            End If
        Next
    End Sub
    'Public Function Dictionary_DameValores_List(queDic As Dictionary(Of String, List(Of String)), queKey As String) As List(Of String)
    '    If queDic.ContainsKey(queKey) Then
    '        Return queDic(queKey)
    '    Else
    '        Return Nothing
    '    End If
    'End Function
    'Public Function Dictionary_DameValores_Array(queDic As Dictionary(Of String, List(Of String)), queKey As String) As Array
    '    If queDic.ContainsKey(queKey) Then
    '        Return queDic(queKey).ToArray
    '    Else
    '        Return Nothing
    '    End If
    'End Function
    Public Sub ImprimeDictionaryList(queDic As Dictionary(Of String, List(Of String)))
        Dim mensaje As String = ""
        For Each quePro As String In queDic.Keys
            mensaje &= quePro & vbCrLf
            For Each queVal As String In queDic(quePro)
                mensaje &= vbTab & queVal & vbCrLf
            Next
            mensaje &= vbCrLf
        Next
        MsgBox(mensaje)
        'Debug.Print(mensaje)
    End Sub
    Public Sub ImprimeDictionaryDictionary(queDic As Dictionary(Of String, Dictionary(Of String, String)), Optional soloIndex As Boolean = True)
        If queDic Is Nothing Or (queDic IsNot Nothing AndAlso queDic.Count = 0) Then Exit Sub
        '
        Dim mensaje As String = ""
        For Each quePro As String In queDic.Keys
            mensaje &= quePro & vbCrLf
            If soloIndex Then
                mensaje &= vbTab & "Nº Datos = " & queDic(quePro).Count & vbCrLf
            Else
                For Each queVal As KeyValuePair(Of String, String) In queDic(quePro)
                    mensaje &= vbTab & queVal.Key & " = " & queVal.Value & vbCrLf
                Next
            End If
            mensaje &= vbCrLf
        Next
        MsgBox(mensaje)
        'Debug.Print(mensaje)
    End Sub

    Public Sub QuitaEspaciosDictionaryList(queDic As Dictionary(Of String, List(Of String)))
        If queDic Is Nothing Or (queDic IsNot Nothing AndAlso queDic.Count = 0) Then Exit Sub
        '
        SELECCIONABLES = New Dictionary(Of String, List(Of String))
        For Each quePro As String In queDic.Keys
            Dim nombre As String = quePro.Replace(" ", esp)
            Dim oList As New List(Of String)
            For Each queVal As String In queDic(quePro)
                oList.Add(queVal.Replace(" ", esp))
            Next
            SELECCIONABLES.Add(nombre, oList)
        Next
    End Sub
    Public Sub QuitaEspaciosDictionaryDictionary(queDic As Dictionary(Of String, Dictionary(Of String, String)), Optional soloIndex As Boolean = True)
        If queDic Is Nothing Or (queDic IsNot Nothing AndAlso queDic.Count = 0) Then Exit Sub
        '
        DATOS = New Dictionary(Of String, Dictionary(Of String, String))
        For Each quePro As String In queDic.Keys
            Dim oDic As New Dictionary(Of String, String)
            For Each queVal As KeyValuePair(Of String, String) In queDic(quePro)
                If queVal.Key.Contains(" ") = False Then Continue For
                '
                Dim nombre As String = queVal.Key.Replace(" ", esp)
                Dim valor As String = queVal.Value
                oDic.Add(nombre, valor)
            Next
            DATOS.Add(quePro, oDic)
        Next
    End Sub
End Class
