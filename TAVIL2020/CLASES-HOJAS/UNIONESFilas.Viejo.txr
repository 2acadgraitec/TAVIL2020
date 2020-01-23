Imports System.Linq
Imports ce = ClosedXML.Excel
Imports System.Windows.Forms

Public Class UNIONESFilas
    Public Campos As List(Of String)
    Public filas As List(Of UNIONESFila)        ' Key = ITEM_NUMBER, Value = clsPTItem
    Public Sub New()
        'If cXML Is Nothing Then cXML = New ClosedXML2acad.ClosedXML2acad
        Campos = New List(Of String)
        filas = New List(Of UNIONESFila)
        '
        'For Each nHoja In HojasUniones
        For Each fila As ce.IXLRow In modClosedXMLTavil.Excel_LeeFilar(LAYOUTDB, HojasUniones, concabeceras:=True).AsParallel
            If fila.RowNumber = 1 Then
                For Each oCe As ce.IXLCell In fila.CellsUsed
                    If Campos.Contains(Convert.ToString(oCe.Value)) = False Then
                        Campos.Add(Convert.ToString(oCe.Value))
                    End If
                Next
            Else
                filas.Add(New UNIONESFila(fila))
            End If
            System.Windows.Forms.Application.DoEvents()
        Next
        'Next
    End Sub
    ''' <summary>
    ''' Para hoja 'UNIONES' inC y outC siempre enviarlos con 8 carácteres (Pueden ser TRD300XX o TRD30015)
    ''' Debemos buscar el que sea único, con las XX y sin las XX
    ''' </summary>
    ''' <param name="inC"></param>
    ''' <param name="inInc"></param>
    ''' <param name="outC"></param>
    ''' <param name="outInc"></param>
    ''' <param name="angle"></param>
    ''' <returns></returns>
    Public Function Fila_BuscaDame(inC As String, inInc As String, outC As String, outInc As String, angle As String) As UNIONESFila
        Dim resultado As UNIONESFila = Nothing
        Dim inCFin As String = ""
        Dim outCFin As String = ""
        '
        ' Filtro 1 (para ver si existe inC con 8 carácteres o con 6 y terminado en XX)
        Dim filtros() As String = {inC.ToUpper, inC.Substring(0, 6) & "XX".ToUpper}
        For Each filtro As String In filtros
            Dim f1 As IEnumerable(Of UNIONESFila) = From x In filas
                                                    Where x.INFEED_CONVEYOR.ToUpper.Trim = filtro
                                                    Select x
            If f1 IsNot Nothing AndAlso f1.Count > 0 Then
                inCFin = filtro
                Exit For
            End If
        Next
        ' Filtro 2 (para ver si existe outC con 8 carácteres o con 6 y terminado en XX)
        filtros = {outC.ToUpper, outC.Substring(0, 6) & "XX".ToUpper}
        For Each filtro As String In filtros
            Dim f1 As IEnumerable(Of UNIONESFila) = From x In filas
                                                    Where x.INFEED_CONVEYOR.ToUpper.Trim = filtro
                                                    Select x
            If f1 IsNot Nothing AndAlso f1.Count > 0 Then
                outCFin = filtro
                Exit For
            End If
        Next
        '
        If inCFin = "" OrElse outCFin = "" Then
            ' Salimos con Nothing
            Return resultado
            'Exit Function
        End If
        '
        '
        If angle = "0" Then angle = ""
        Dim fila = From x In filas
                   Where x.INFEED_CONVEYOR.Trim.StartsWith(inCFin) AndAlso
                                    x.INFEED_INCLINATION.Trim = inInc.Trim AndAlso
                                    x.OUTFEED_CONVEYOR.Trim.StartsWith(outCFin.Trim) AndAlso
                                    x.OUTFEED_INCLINATION.Trim = outInc.Trim AndAlso
                                    x.ANGLE.Trim = angle.Trim
                   Select x
        '
        If fila.Count > 0 Then
            resultado = fila.FirstOrDefault
        End If
        '
        Return resultado
    End Function
    ''' <summary>
    ''' Para hoja 'UNIONES_Y' inC, outCL y outCR siempre enviarlos con 8 carácteres (Pueden ser TRD300XX o TRD30015)
    ''' Debemos buscar el que sea único, con las XX y sin las XX
    ''' </summary>
    ''' <param name="inC"></param>
    ''' <param name="inInc"></param>
    ''' <param name="outCL"></param>
    ''' <param name="outIncL"></param>
    ''' <param name="angleL"></param>
    ''' <param name="outCR"></param>
    ''' <param name="outIncR"></param>
    ''' <param name="angleR"></param>
    ''' <returns></returns>
    Public Function Fila_BuscaDame(inC As String, inInc As String,
                                   outCL As String, outIncL As String, angleL As String,
                                   outCR As String, outIncR As String, angleR As String) As UNIONESFila
        Dim resultado As UNIONESFila = Nothing
        Dim inCFin As String = ""
        Dim outCLFin As String = ""
        Dim outCRFin As String = ""
        '
        ' Filtro 1 (para ver si existe inC con 8 carácteres o con 6 y terminado en XX)
        Dim filtros() As String = {inC.ToUpper, inC.Substring(0, 6) & "XX".ToUpper}
        For Each filtro As String In filtros
            Dim f1 As IEnumerable(Of UNIONESFila) = From x In filas
                                                    Where x.INFEED_CONVEYOR.ToUpper.Trim = filtro
                                                    Select x
            If f1 IsNot Nothing AndAlso f1.Count > 0 Then
                inCFin = filtro
                Exit For
            End If
        Next
        ' Filtro 2 (para ver si existe outCL con 8 carácteres o con 6 y terminado en XX)
        filtros = {outCL.ToUpper, outCL.Substring(0, 6) & "XX".ToUpper}
        For Each filtro As String In filtros
            Dim f1 As IEnumerable(Of UNIONESFila) = From x In filas
                                                    Where x.OUTFEED_CONVEYOR_L.ToUpper.Trim = filtro
                                                    Select x
            If f1 IsNot Nothing AndAlso f1.Count > 0 Then
                outCLFin = filtro
                Exit For
            End If
        Next
        ' Filtro 3 (para ver si existe outCR con 8 carácteres o con 6 y terminado en XX)
        filtros = {outCR.ToUpper, outCR.Substring(0, 6) & "XX".ToUpper}
        For Each filtro As String In filtros
            Dim f1 As IEnumerable(Of UNIONESFila) = From x In filas
                                                    Where x.OUTFEED_CONVEYOR_R.ToUpper.Trim = filtro
                                                    Select x
            If f1 IsNot Nothing AndAlso f1.Count > 0 Then
                outCRFin = filtro
                Exit For
            End If
        Next
        '
        If inCFin = "" OrElse outCLFin = "" OrElse outCRFin = "" Then
            ' Salimos con Nothing
            Return resultado
            'Exit Function
        End If
        '
        '
        If angleL = "0" Then angleL = ""
        If angleR = "0" Then angleR = ""
        Dim fila = From x In filas
                   Where x.INFEED_CONVEYOR.Trim.StartsWith(inCFin) AndAlso
                                    x.INFEED_INCLINATION.Trim = inInc.Trim AndAlso
                                    x.OUTFEED_CONVEYOR_L.Trim.StartsWith(outCLFin.Trim) AndAlso
                                    x.OUTFEED_INCLINATION_L.Trim = outIncL.Trim AndAlso
                                    x.ANGLE_L.Trim = angleL.Trim AndAlso
                                    x.OUTFEED_CONVEYOR_R.Trim.StartsWith(outCRFin.Trim) AndAlso
                                    x.OUTFEED_INCLINATION_R.Trim = outIncR.Trim AndAlso
                                    x.ANGLE_R.Trim = angleR.Trim
                   Select x
        '
        If fila.Count > 0 Then
            resultado = fila.FirstOrDefault
        End If
        '
        Return resultado
    End Function
    ''' <summary>
    ''' Para hoja 'UNIONES_X' inCL, inCR, outCL y outCR siempre enviarlos con 8 carácteres (Pueden ser TRD300XX o TRD30015)
    ''' Debemos buscar el que sea único, con las XX y sin las XX
    ''' </summary>
    ''' <param name="inCL"></param>
    ''' <param name="inIncL"></param>
    ''' <param name="inCR"></param>
    ''' <param name="inIncR"></param>
    ''' <param name="outC"></param>
    ''' <param name="outInc"></param>
    ''' <param name="angle"></param>
    ''' <returns></returns>
    Public Function Fila_BuscaDame(inCL As String, inIncL As String,
                                   inCR As String, inIncR As String,
                                   outC As String, outInc As String, angle As String) As UNIONESFila
        Dim resultado As UNIONESFila = Nothing
        Dim inCLFin As String = ""
        Dim inCRFin As String = ""
        Dim outCFin As String = ""
        '
        ' Filtro 1 (para ver si existe inCL con 8 carácteres o con 6 y terminado en XX)
        Dim filtros() As String = {inCL.ToUpper, inCL.Substring(0, 6) & "XX".ToUpper}
        For Each filtro As String In filtros
            Dim f1 As IEnumerable(Of UNIONESFila) = From x In filas
                                                    Where x.INFEED_CONVEYOR_L.ToUpper.Trim = filtro
                                                    Select x
            If f1 IsNot Nothing AndAlso f1.Count > 0 Then
                inCLFin = filtro
                Exit For
            End If
        Next
        '
        ' Filtro 2 (para ver si existe inCR con 8 carácteres o con 6 y terminado en XX)
        filtros = {inCR.ToUpper, inCR.Substring(0, 6) & "XX".ToUpper}
        For Each filtro As String In filtros
            Dim f1 As IEnumerable(Of UNIONESFila) = From x In filas
                                                    Where x.INFEED_CONVEYOR_R.ToUpper.Trim = filtro
                                                    Select x
            If f1 IsNot Nothing AndAlso f1.Count > 0 Then
                inCRFin = filtro
                Exit For
            End If
        Next
        ' Filtro 3 (para ver si existe outC con 8 carácteres o con 6 y terminado en XX)
        filtros = {outC.ToUpper, outC.Substring(0, 6) & "XX".ToUpper}
        For Each filtro As String In filtros
            Dim f1 As IEnumerable(Of UNIONESFila) = From x In filas
                                                    Where x.OUTFEED_CONVEYOR.ToUpper.Trim = filtro
                                                    Select x
            If f1 IsNot Nothing AndAlso f1.Count > 0 Then
                outCFin = filtro
                Exit For
            End If
        Next
        '
        If inCLFin = "" OrElse inCRFin = "" OrElse outCFin = "" Then
            ' Salimos con Nothing
            Return resultado
            'Exit Function
        End If
        '
        '
        If angle = "0" Then angle = ""
        Dim fila = From x In filas
                   Where x.INFEED_CONVEYOR_L.Trim.StartsWith(inCLFin) AndAlso
                   x.INFEED_INCLINATION_L.Trim = inIncL.Trim AndAlso
                   x.INFEED_CONVEYOR_R.Trim.StartsWith(inCRFin) AndAlso
                   x.INFEED_INCLINATION_R.Trim = inIncR.Trim AndAlso
                   x.OUTFEED_CONVEYOR.Trim.StartsWith(outCFin.Trim) AndAlso
                   x.OUTFEED_INCLINATION.Trim = outInc.Trim AndAlso
                   x.ANGLE.Trim = angle.Trim
                   Select x
        '
        If fila.Count > 0 Then
            resultado = fila.FirstOrDefault
        End If
        '
        Return resultado
    End Function

End Class


Public Class UNIONESFila
    ' UNIONES:      INFEED_CONVEYOR	INFEED_INCLINATION	UNION	UNITS	OUTFEED_CONVEYOR	OUTFEED_INCLINATION	ANGLE	INFORMACION
    ' UNIONES_Y:    INFEED_CONVEYOR	INFEED_INCLINATION	UNION	UNITS	OUTFEED_CONVEYOR_L	OUTFEED_INCLINATION_L	ANGLE_L	OUTFEED_CONVEYOR_R	OUTFEED_INCLINATION_R	ANGLE_R	INFORMACION
    ' UNIONES_X:    INFEED_CONVEYOR_L	INFEED_INCLINATION_L	INFEED_CONVEYOR_R	INFEED_INCLINATION_R	UNION	UNITS	OUTFEED_CONVEYOR	OUTFEED_INCLINATION	ANGLE	INFORMACION
    Public NFila As Integer = -1
    '
    Public INFEED_CONVEYOR As String = ""           ' TRD300XX
    Public INFEED_INCLINATION As String = ""        ' FLAT
    Public INFEED_CONVEYOR_L As String = ""           ' TRD300XX
    Public INFEED_INCLINATION_L As String = ""        ' FLAT
    Public INFEED_CONVEYOR_R As String = ""           ' TRD300XX
    Public INFEED_INCLINATION_R As String = ""        ' FLAT
    '
    Public UNION As String = ""                   ' 132353 o 132348; 120291
    Public UNITS As String = ""                   ' 1; 1
    '
    Public OUTFEED_CONVEYOR As String = ""          ' TRD30305
    Public OUTFEED_INCLINATION As String = ""       ' FLAT
    Public ANGLE As String = ""                     ' "" o 90
    Public OUTFEED_CONVEYOR_L As String = ""          ' TRD30305
    Public OUTFEED_INCLINATION_L As String = ""       ' FLAT
    Public ANGLE_L As String = ""                     ' "" o 90
    Public OUTFEED_CONVEYOR_R As String = ""          ' TRD30305
    Public OUTFEED_INCLINATION_R As String = ""       ' FLAT
    Public ANGLE_R As String = ""                     ' "" o 90
    '
    Public INFORMACION As String = ""               ' RR     Primer transportador/union Recto. Segundo transportador/union Recto
    'Public Rows As List(Of DataGridViewRow)
    Public RowsL As Dictionary(Of String, List(Of DataGridViewRow))
    Public hayerror As Boolean = False
    Public ladotiene As Boolean = False
    Public LADO As String = ""

    Public Sub New(fila As ce.IXLRow)
        'If cXML Is Nothing Then cXML = New ClosedXML2acad.ClosedXML2acad
        NFila = fila.RowNumber
        For Each oCell As ce.IXLCell In fila.Cells.AsParallel
            Dim cabecera As String = oCell.WorksheetColumn.FirstCell.Value.ToString.Trim
            Dim valor As String = oCell.Value
            Select Case cabecera.ToUpper
                Case "INFEED_CONVEYOR" : INFEED_CONVEYOR = Convert.ToString(valor).Trim
                Case "INFEED_INCLINATION" : Me.INFEED_INCLINATION = Convert.ToString(valor).Trim
                Case "INFEED_CONVEYOR_L" : INFEED_CONVEYOR_L = Convert.ToString(valor).Trim
                Case "INFEED_INCLINATION_L" : Me.INFEED_INCLINATION_L = Convert.ToString(valor).Trim
                Case "INFEED_CONVEYOR_R" : INFEED_CONVEYOR_R = Convert.ToString(valor).Trim
                Case "INFEED_INCLINATION_R" : Me.INFEED_INCLINATION_R = Convert.ToString(valor).Trim
                    '
                Case "UNION" : Me.UNION = Convert.ToString(valor).Trim.Replace(" ", "")
                Case "UNITS" : Me.UNITS = Convert.ToString(valor).Trim.Replace(" ", "")
                    '
                Case "OUTFEED_CONVEYOR" : Me.OUTFEED_CONVEYOR = Convert.ToString(valor).Trim
                Case "OUTFEED_INCLINATION" : Me.OUTFEED_INCLINATION = Convert.ToString(valor).Trim
                Case "ANGLE" : Me.ANGLE = Convert.ToString(valor).Trim
                Case "OUTFEED_CONVEYOR_L" : Me.OUTFEED_CONVEYOR_L = Convert.ToString(valor).Trim
                Case "OUTFEED_INCLINATION_L" : Me.OUTFEED_INCLINATION_L = Convert.ToString(valor).Trim
                Case "ANGLE_L" : Me.ANGLE_L = Convert.ToString(valor).Trim
                Case "OUTFEED_CONVEYOR_R" : Me.OUTFEED_CONVEYOR_R = Convert.ToString(valor).Trim
                Case "OUTFEED_INCLINATION_R" : Me.OUTFEED_INCLINATION_R = Convert.ToString(valor).Trim
                Case "ANGLE_R" : Me.ANGLE_R = Convert.ToString(valor).Trim
                    '
                Case "INFORMACION" : Me.INFORMACION = Convert.ToString(valor).Trim
            End Select
        Next
        If Me.UNION.Contains("|") AndAlso Me.UNITS.Contains("|") Then
            ladotiene = True
            FilasDataGridView_Crea_LADO()
        Else
            ladotiene = False
            FilasDataGridView_Crea()
        End If
    End Sub
    Public Sub FilasDataGridView_Crea_LADO()
        'Dim lados() As String = [Enum].GetNames(GetType(LADO))      ' Array con L,C,R
        RowsL = New Dictionary(Of String, List(Of DataGridViewRow))
        Dim uniones As String() = Me.UNION.Split("|"c)
        Dim unidades As String() = Me.UNITS.Split("|"c)
        If uniones.Count <> unidades.Count Then
            MsgBox("Error in Excel. Columns UNION and UNITS in Row " & Me.NFila)
            hayerror = True
            Exit Sub
        Else
            hayerror = False
        End If
        '
        For x As Integer = 0 To uniones.Count - 1
            Dim lado As String = uniones(x).Substring(0, 1)     'L, C o R
            Dim uni As String = uniones(x).Substring(2)         '132353 o 132348; 12029
            '
            Dim unionesP() As String = uni.Split(";")
            Dim unidadesP() As String = unidades(x).Split(";")
            If unionesP.Count <> unidadesP.Count Then
                MsgBox("Error in Excel. Columns UNION and UNITS in Row " & Me.NFila)
                hayerror = True
                Exit Sub
            Else
                hayerror = False
            End If
            '
            Dim Row As New List(Of DataGridViewRow)
            For y As Integer = 0 To unionesP.Count - 1
                Dim Fila As New DataGridViewRow
                Dim Ctext As New DataGridViewComboBoxCell
                Dim Ttext As New DataGridViewTextBoxCell
                Dim TUnits As New DataGridViewTextBoxCell
                '
                TUnits.Value = unidadesP(y)
                If unionesP(y).Contains("o") Then
                    Dim partes() As String = unionesP(y).Split("o")
                    Ctext.Items.AddRange(partes)
                    Ctext.Value = partes(0)
                    Fila.Cells.Add(Ctext)
                Else
                    Ttext.Value = unionesP(y)
                    Fila.Cells.Add(Ttext)
                End If
                Fila.Cells.Add(TUnits)
                Row.Add(Fila)
                'TUnits = Nothing
                'Ctext = Nothing
                'Ttext = Nothing
                'Fila = Nothing
            Next
            RowsL.Add(lado.ToUpper, Row)
        Next
    End Sub
    Public Sub FilasDataGridView_Crea()
        RowsL = New Dictionary(Of String, List(Of DataGridViewRow))
        Dim unionesP() As String = Me.UNION.Split(";")
        Dim unidadesP() As String = Me.UNITS.Split(";")
        If unionesP.Count <> unidadesP.Count Then
            MsgBox("Error in Excel. Columns UNION and UNITS")
            hayerror = True
            Exit Sub
        Else
            hayerror = False
        End If
        '
        Dim Row As New List(Of DataGridViewRow)
        For x As Integer = 0 To unionesP.Count - 1
            Dim Fila As New DataGridViewRow
            Dim Ctext As New DataGridViewComboBoxCell
            Dim Ttext As New DataGridViewTextBoxCell
            Dim TUnits As New DataGridViewTextBoxCell
            '
            TUnits.Value = unidadesP(x)
            If unionesP(x).Contains("o") Then
                Dim partes() As String = unionesP(x).Split("o")
                Ctext.Items.AddRange(partes)
                Ctext.Value = partes(0)
                Fila.Cells.Add(Ctext)
            Else
                Ttext.Value = unionesP(x)
                Fila.Cells.Add(Ttext)
            End If
            Fila.Cells.Add(TUnits)
            Row.Add(Fila)
            'TUnits = Nothing
            'Ctext = Nothing
            'Ttext = Nothing
            'Fila = Nothing
        Next
        RowsL.Add("L", Row)
    End Sub
End Class
