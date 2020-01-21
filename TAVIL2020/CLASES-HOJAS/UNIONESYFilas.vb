Imports System.Linq
Imports ce = ClosedXML.Excel
Imports System.Windows.Forms

Public Class UNIONESYFilas
    Public nHoja As String = HojaUniones
    Public Campos As List(Of String)
    Public filas As List(Of UNIONESYFila)        ' Key = ITEM_NUMBER, Value = clsPTItem
    Public Sub New()
        'If cXML Is Nothing Then cXML = New ClosedXML2acad.ClosedXML2acad
        Campos = New List(Of String)
        filas = New List(Of UNIONESYFila)
        '
        For Each fila As ce.IXLRow In modClosedXMLTavil.Excel_LeeFilar(LAYOUTDB, nHoja, concabeceras:=True).AsParallel
            If fila.RowNumber = 1 Then
                For Each oCe As ce.IXLCell In fila.CellsUsed
                    Campos.Add(Convert.ToString(oCe.Value))
                Next
            Else
                filas.Add(New UNIONESYFila(fila))
            End If
            System.Windows.Forms.Application.DoEvents()
        Next
    End Sub
    ' inC y outC siempre enviarlos con 8 carácteres (Pueden ser TRD300XX o TRD30015)
    ' Debemos buscar el que sea único, con las XX y sin las XX
    Public Function Fila_BuscaDame(inC As String, inInc As String, outC As String, outInc As String, angle As String) As UNIONESYFila
        Dim resultado As UNIONESYFila = Nothing
        Dim inCFin As String = ""
        Dim outCFin As String = ""
        '
        ' Filtro 1 (para ver si existe inC con 8 carácteres o con 6 y terminado en XX)
        Dim filtros() As String = {inC, inC.Substring(0, 6) & "XX"}
        For Each filtro As String In filtros
            Dim f1 As IEnumerable(Of UNIONESYFila) = From x In filas
                                                     Where x.INFEED_CONVEYOR.ToUpper.Trim = filtro
                                                     Select x
            If f1 IsNot Nothing AndAlso f1.Count > 0 Then
                inCFin = filtro
                Exit For
            End If
        Next
        ' Filtro 2 (para ver si existe outC con 8 carácteres o con 6 y terminado en XX)
        filtros = {outC, outC.Substring(0, 6) & "XX"}
        For Each filtro As String In filtros
            Dim f1 As IEnumerable(Of UNIONESYFila) = From x In filas
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
    ' inC siempre enviarlos con 8 carácteres (Pueden ser TRD300XX o TRD30015)
    ' Debemos buscar el que sea único, con las XX y sin las XX
    Public Function Fila_InclinationsInOut(inC As String, INFEED As Boolean) As String()
        Dim resultado As String() = {}
        Dim inCFin As String = ""
        '
        ' Filtro 1 (para ver si existe inC con 8 carácteres o con 6 y terminado en XX)
        Dim filtros() As String = {inC, inC.Substring(0, 6) & "XX"}
        For Each filtro As String In filtros
            Dim f1 As IEnumerable(Of UNIONESYFila) = Nothing
            If INFEED Then
                f1 = From x In Me.filas
                     Where x.INFEED_CONVEYOR.ToUpper.Trim = filtro
                     Select x

            Else
                f1 = From x In Me.filas
                     Where x.OUTFEED_CONVEYOR.ToUpper.Trim = filtro
                     Select x
            End If
            If f1 IsNot Nothing AndAlso f1.Count > 0 Then
                inCFin = filtro
                Exit For
            End If
        Next
        '
        If inCFin = "" Then
            ' Salimos con Nothing
            Return resultado
            'Exit Function
        End If

        Dim inclinations As IEnumerable(Of String) = Nothing
        If INFEED Then
            inclinations = From x In Me.filas
                           Where x.INFEED_CONVEYOR.Trim.StartsWith(inCFin)
                           Select x.INFEED_INCLINATION
                           Distinct
        Else
            inclinations = From x In Me.filas
                           Where x.OUTFEED_CONVEYOR.Trim.StartsWith(inCFin)
                           Select x.OUTFEED_INCLINATION
                           Distinct
        End If
        '
        If inclinations.Count > 0 Then
            resultado = inclinations.ToArray
        End If
        '
        Return resultado
    End Function
End Class


Public Class UNIONESYFila
    ' INFEED_CONVEYOR	INFEED_INCLINATION	UNION	UNITS	OUTFEED_CONVEYOR_L	OUTFEED_INCLINATION_L	ANGLE_L	OUTFEED_CONVEYOR_R	OUTFEED_INCLINATION_R	ANGLE_R	INFORMACION
    Public NFila As Integer = -1
    Public INFEED_CONVEYOR As String = ""           ' TRD300XX
    Public INFEED_INCLINATION As String = ""        ' FLAT
    Private _UNION As String = ""                   ' 132353 o 132348; 120291
    Private _UNITS As String = ""                   ' 1; 1
    Public OUTFEED_CONVEYOR_L As String = ""          ' TRD30305
    Public OUTFEED_INCLINATION_L As String = ""       ' FLAT
    Public ANGLE_L As String = ""                     ' "" o 90
    Public OUTFEED_CONVEYOR_R As String = ""          ' TRD30305
    Public OUTFEED_INCLINATION_R As String = ""       ' FLAT
    Public ANGLE_R As String = ""                     ' "" o 90
    Public INFORMACION As String = ""               ' RR     Primer transportador/union Recto. Segundo transportador/union Recto
    'Public Rows As List(Of DataGridViewRow)
    Public RowsL As Dictionary(Of String, List(Of DataGridViewRow))
    Public hayerror As Boolean = False
    Public ladotiene As Boolean = False
    Public LADO As String = "L"

    Public Property UNION As String
        Get
            Return _UNION
        End Get
        Set(value As String)
            _UNION = value.Replace(" ", "").Trim    ' value.Replace("o", ";").Replace(" ", "").Trim
        End Set
    End Property

    Public Property UNITS As String
        Get
            Return _UNITS
        End Get
        Set(value As String)
            _UNITS = value.Replace(" ", "").Trim
        End Set
    End Property

    Public Sub New(fila As ce.IXLRow)
        ' INFEED_CONVEYOR	INFEED_INCLINATION	UNION	UNITS	OUTFEED_CONVEYOR_L	OUTFEED_INCLINATION_L	ANGLE_L	OUTFEED_CONVEYOR_R	OUTFEED_INCLINATION_R	ANGLE_R	INFORMACION
        'If cXML Is Nothing Then cXML = New ClosedXML2acad.ClosedXML2acad
        NFila = fila.RowNumber
        For Each oCell As ce.IXLCell In fila.Cells.AsParallel
            Dim cabecera As String = oCell.WorksheetColumn.FirstCell.Value.ToString.Trim
            Dim valor As String = oCell.Value
            Select Case cabecera.ToUpper
                Case "INFEED_CONVEYOR" : INFEED_CONVEYOR = Convert.ToString(valor).Trim
                Case "INFEED_INCLINATION" : Me.INFEED_INCLINATION = Convert.ToString(valor).Trim
                Case "UNION" : Me.UNION = Convert.ToString(valor).Trim.Replace(" ", "")
                Case "UNITS" : Me.UNITS = Convert.ToString(valor).Trim.Replace(" ", "")
                Case "OUTFEED_CONVEYOR_L" : Me.OUTFEED_CONVEYOR_L = Convert.ToString(valor).Trim
                Case "OUTFEED_INCLINATION_L" : Me.OUTFEED_INCLINATION_L = Convert.ToString(valor).Trim
                Case "ANGLE_L" : Me.ANGLE_L = Convert.ToString(valor).Trim
                Case "OUTFEED_CONVEYOR_R" : Me.OUTFEED_CONVEYOR_R = Convert.ToString(valor).Trim
                Case "OUTFEED_INCLINATION_R" : Me.OUTFEED_INCLINATION_R = Convert.ToString(valor).Trim
                Case "ANGLE_R" : Me.ANGLE_R = Convert.ToString(valor).Trim
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
        hayerror = False
        If uniones.Count <> unidades.Count Then
            MsgBox("Error in Excel. Columns UNION and UNITS in Sheet 'UNIONES_Y', Row " & Me.NFila)
            hayerror = True
            Exit Sub
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
        hayerror = False
        If unionesP.Count <> unidadesP.Count Then
            MsgBox("Error in Excel. Columns UNION and UNITS in Sheet 'UNIONES_Y', Row " & Me.NFila)
            hayerror = True
            Exit Sub
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
