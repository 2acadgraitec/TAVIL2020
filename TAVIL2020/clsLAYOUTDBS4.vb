
' Idioma Ingles para todo el desarollo.
Public Class clsLAYOUTDBS4
    'Public arrNombresHojasDatos() As String = New String() {"TRD3", "TCB3", "PT"}
    'Public Const TRD3Hoja As String = "TRD3"
    'Public Const TCB3Hoja As String = "TCB3"
    'Public Const PTHoja As String = "PT"
    'Public Const UNIONESHoja As String = "UNIONES"
    'Public Const SELECCIONABLESHoja As String = "SELECCIONABLES"
    'Public Const IDIOMASHoja As String = "IDIOMAS"
    Public Const sep As String = "_"
    '
    ' Fila 1. Propiedades de todos los TRD3, TCB3, PT y ATR (Cada uno en una hoja de LAYOUTDB.xlsx)
    ' Todos tendrán las mismas propiedades. Algunas rellenas y otras no.
    Public arrPropCintas() As String = New String() {
        "CODE", "BLOCK", "LENGTH", "WIDTH", "WEIGHT", "RADIUS", "HEIGHT", "HAND", "MOTOR_POSITION", "MOTOR_OFFSET", "MOTOR_TYPE", "FLAT_BELT",
        "O_RING_BELT", "DESCRIPTION", "ESPECIFICATION", "INFEED_HEIGHT", "OUTFEED_HEIGHT", "ITEM_NUMBER",
        "O_RING_PROTECTION", "STANDARD_PART", "CONVEYOR_BELT_TYPE", "UNDERBELT", "UNDER_CONVEYOR_PROTECTION",
        "BELT_REFERENCE", "NUM_MOTORS", "SUPPORT_TYPE", "REAL_LENGHT",
        "CONVEYOR_HEIGHT_1", "CONVEYOR_HEIGHT_2", "CONVEYOR_HEIGHT_3", "CONVEYOR_HEIGHT_4",
        "ANGLE", "FRAME", "OBSERVATION"}
    ' Fila 1. Propiedades de UNIONES (hoja UNIONES=UNIONESHoja)
    Public arrPropUniones() As String = New String() {"INFEED_CONVEYOR", "INCLINATION", "UNION", "UNITS", "OUTFEED_CONVEYOR", "INCLINATION", "ANGLE", "INFORMATION"}
    ' Columna 1. Nombre de todas las propiedades que tendrás desplegables con valores (Hoja SELECCIONABLES=SELCCIONABLESHoja)
    ' En cada fila habrá: Nombre de la propiedad en columna 1. Resto de columnas: un valor en cada columna (2 hasta la última)
    Public SELECCIONABLES As Dictionary(Of String, List(Of String))      ' coleccion de desplegables
    Public TRANSPORTADORES As Dictionary(Of String, Dictionary(Of String, String))
    '
    Public Sub New()
        LlenaDatosExcelDB()
    End Sub
    '
    Public Sub LlenaDatosExcelDB()
        Dim SELECCIONABLESTemp As Dictionary(Of String, List(Of String)) = Excel_DameDesplegablesEnFilas(LAYOUTDB, HojaSeleccionables)
        SELECCIONABLES_RELLENA(SELECCIONABLESTemp)
        Dim TRANSPORTADORESTemp As Dictionary(Of String, Dictionary(Of String, String)) = Excel_DameFormato_DB_Hojas(LAYOUTDB, HojasTransportadores.ToArray, 1)
        TRANSPORTADORES_RELLENA(TRANSPORTADORESTemp)
        'Dim DATOSTemp As Dictionary(Of String, Dictionary(Of String, String)) = Excel_DameFormato_DB_Hojas(LAYOUTDB, HojasTransportadores, 1)
        'TRANSPORTADORES_RELLENA(TRANSPORTADORESTemp)
        dicBloques_PonNombresBloques(dicBloques)
    End Sub
    Public Sub dicBloques_PonNombresBloques(ByRef quedicBloques As Dictionary(Of String, String))
        For Each queNomBloTemp As String In TRANSPORTADORES.Keys
            Dim queNomBlo As String = queNomBloTemp.Split("·"c)(0)
            If quedicBloques.ContainsKey(queNomBlo) = True Then Continue For
            '
            Dim colFi() As String = IO.Directory.GetFiles(BloquesDir, queNomBlo & ".png", IO.SearchOption.AllDirectories)
            If colFi IsNot Nothing AndAlso colFi.Length > 0 Then
                quedicBloques.Add(queNomBlo, IO.Path.GetDirectoryName(colFi(0)))
            Else
                quedicBloques.Add(queNomBlo, BloquesDir)
            End If
        Next
    End Sub
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

    Public Sub SELECCIONABLES_RELLENA(queDic As Dictionary(Of String, List(Of String)))
        If queDic Is Nothing Or (queDic IsNot Nothing AndAlso queDic.Count = 0) Then Exit Sub
        '
        SELECCIONABLES = New Dictionary(Of String, List(Of String))
        For Each quePro As String In queDic.Keys
            Dim nombre As String = quePro.Replace(" ", sep)
            Dim oList As New List(Of String)
            For Each queVal As String In queDic(quePro)
                oList.Add(queVal.Replace(" ", ""))
            Next
            SELECCIONABLES.Add(nombre, oList)
        Next
    End Sub
    Public Sub TRANSPORTADORES_RELLENA(queDic As Dictionary(Of String, Dictionary(Of String, String)), Optional soloIndex As Boolean = True)
        If queDic Is Nothing Or (queDic IsNot Nothing AndAlso queDic.Count = 0) Then Exit Sub
        TRANSPORTADORES = New Dictionary(Of String, Dictionary(Of String, String))
        For Each quePro As String In queDic.Keys
            Dim oDic As New Dictionary(Of String, String)
            For Each queVal As KeyValuePair(Of String, String) In queDic(quePro)
                'If queVal.Key.Contains(" ") = False Then Continue For
                '
                Dim nombre As String = queVal.Key.Replace(" ", sep)
                Dim valor As String = queVal.Value
                oDic.Add(nombre, valor)
            Next
            TRANSPORTADORES.Add(quePro, oDic)
        Next
    End Sub
End Class
