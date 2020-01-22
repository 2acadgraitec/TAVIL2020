Option Compare Text


Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports Microsoft.Win32
Imports System.Linq
Imports System.IO
Imports Microsoft.VisualBasic
Imports ClosedXML
Imports ClosedXML.Excel
Public Module modClosedXMLTavil
    Public Sub LimpiaMemoria()
        GC.WaitForPendingFinalizers()
        GC.Collect()
        GC.WaitForPendingFinalizers()
        GC.Collect()
    End Sub
    Public Function Excel_GuardaWorkbookMemoryStream(excelWorkbook As XLWorkbook) As MemoryStream
        Dim fs As New MemoryStream
        excelWorkbook.SaveAs(fs)
        fs.Position = 0
        Return fs
    End Function
    '
    Public Function Excel_DameStreamFicheroExcel(queFi As String) As XLWorkbook
        Dim oFs As FileStream = File.Open(queFi, FileMode.Open)
        oFs.Seek(0, SeekOrigin.Begin)
        Dim xlWb As IXLWorkbook = New XLWorkbook(oFs)
        oFs.Dispose()
        Return xlWb
    End Function
    '
    Public Sub Excel_BorraFilas(fiExcel As String, rowIni As Integer, rowFin As Integer, Optional nHoja As Object = Nothing)
        If IO.File.Exists(fiExcel) = False Then
            Exit Sub
        End If
        ' Cerrar Excel siempre que vayamos a escribir en los ficheros.
        Utilidades.CierraProceso("EXCEL")
        Using xlWb As XLWorkbook = New XLWorkbook(fiExcel)
            Dim xlWs As IXLWorksheet = Nothing
            ' Coger la hoja indicada (Nombre o número) o la 1 en caso de que no exista
            If nHoja IsNot Nothing Then
                Try
                    xlWs = xlWb.Worksheet(nHoja)
                Catch ex As Exception
                    xlWs = xlWb.Worksheet(1)
                End Try
            Else
                xlWs = xlWb.Worksheet(1)
            End If
            '
            Dim rUse As IXLRange = xlWs.RangeUsed
            For x As Integer = rowIni To rowFin
                If x > rUse.LastRow.RowNumber Then Exit For
                xlWs.Row(x).Clear()
            Next
            xlWb.Save(New SaveOptions)
            '
        End Using
        LimpiaMemoria()
    End Sub
    '
    ' Siempre empieza en la primera fila
    Public Function Excel_DameDesplegablesEnFilas(fiExcel As String, Optional nHoja As Object = Nothing, Optional nColIni As Integer = 1, Optional nRowIni As Integer = 1) As Dictionary(Of String, List(Of String))
        'nRowIni = 1 (Si no lleva cabeceras)  nRowIni = 2 o superior (Si lleva cabeceras o algo antes)
        If IO.File.Exists(fiExcel) = False Then
            Return Nothing
            Exit Function
        End If
        '
        Dim resultado As New Dictionary(Of String, List(Of String))
        ' Coger el nombre/numero de la hoja indicada. O la primera si no existe o no se indica.
        Try
            Using xlWb As XLWorkbook = New XLWorkbook(fiExcel)
                Dim xlWs As IXLWorksheet = Nothing
                ' Coger la hoja indicada (Nombre o número) o la 1 en caso de que no exista
                If nHoja IsNot Nothing Then
                    Try
                        xlWs = xlWb.Worksheet(nHoja)
                    Catch ex As Exception
                        xlWs = xlWb.Worksheet(1)
                    End Try
                Else
                    xlWs = xlWb.Worksheet(1)
                End If
                ' Columna inicial que contiene los datos
                Dim col As IXLColumn = xlWs.Column(nColIni)
                ' Rango de nombres de la columna inicial
                'Dim colR As IXLRange = xlWs.Range(nRowIni, col.CellsUsed.LastOrDefault.WorksheetRow.RowNumber)
                '
                'Dim contadorCol As Integer = ncol
                For x As Integer = nRowIni To 10000 ' col.LastCellUsed.WorksheetRow.RowNumber
                    ' Nombre del parametro a leer. A la derecha estarán todos los valores del desplegable.
                    Dim param As String = col.Cell(x).Value.ToString
                    ' Si no existe nombre, salir del bucle.
                    If param = "" Then Exit For
                    ' Leer todos los datos que tiene a la derecha al List(of String)
                    Dim row As IXLRow = xlWs.Row(x)
                    Dim valores As New List(Of String)
                    For y = nColIni + 1 To row.LastCellUsed.WorksheetColumn.ColumnNumber
                        valores.Add(row.Cell(y).Value.ToString)
                    Next
                    ' Añadir a resultado key=parametro, value = List(of String) de todos los valores
                    Try
                        resultado.Add(param, valores)
                    Catch ex As Exception
                        ' Ya existe la clave. Sería un error de parámetro repetido. Inusual.
                        Continue For
                    End Try
                    valores = Nothing
                Next
            End Using
        Catch ex As Exception
            MsgBox("Cierre el fichero de Excel " & vbCrLf & fiExcel)
        End Try
        '
        LimpiaMemoria()
        Return resultado
    End Function
    '
    ' Formato de Base de datos: Fila 1, con las cabeceras de columna
    ' El resto de filas, cada fila con todos los parametros del elemento
    ' Resultado Dictionary(Key: nombre único, Value: Dictionary(key: nombre columna, value: valor))
    Public Function Excel_DameFormato_DB(fiExcel As String, Optional nHoja As Object = Nothing, Optional nRowCab As Integer = 1) As Dictionary(Of String, Dictionary(Of String, String))
        If IO.File.Exists(fiExcel) = False Then
            Return Nothing
            Exit Function
        End If
        '

        Dim resultado As New Dictionary(Of String, Dictionary(Of String, String))
        '
        Using xlWb As XLWorkbook = New XLWorkbook(fiExcel)
            Dim xlWs As IXLWorksheet = Nothing
            ' Coger la hoja indicada (Nombre o número) o la 1 en caso de que no exista
            If nHoja IsNot Nothing Then
                Try
                    xlWs = xlWb.Worksheet(nHoja)
                Catch ex As Exception
                    xlWs = xlWb.Worksheet(1)
                End Try
            Else
                xlWs = xlWb.Worksheet(1)
            End If
            '
            ' *** Fila de cabeceras. Sacar todos los nombres de cabeceras a colCab(nº col, nombre col)
            Dim colCab As New Dictionary(Of Integer, String)    ' Key=numero de columna, Value=nombre columna
            Dim oRowCab As IXLRow = xlWs.Row(nRowCab)        ' Fila de cabeceras
            Dim totalCol As Integer = oRowCab.LastCellUsed.WorksheetColumn.ColumnNumber   ' Total columnas de cabeceras.
            For x As Integer = 1 To 1000    'totalCol
                Dim nombre As String = oRowCab.Cell(x).Value.ToString
                If nombre <> "" Then
                    colCab.Add(x, nombre)
                Else
                    Exit For
                End If
            Next
            oRowCab = Nothing
            '
            ' *** Filas de datos. Sacar los datos de cada fila (Empezando en nRowCab + 1) a resultado(Value col 1 & nº fila -1, (nombre col, value)
            Dim oColIni As IXLColumn = xlWs.Column(1)
            Dim totalFil As Integer = oColIni.LastCellUsed.WorksheetRow.RowNumber - nRowCab
            For x As Integer = nRowCab + 1 To 10000   ' totalFil
                Dim oRowD As IXLRow = xlWs.Row(x)               ' Fila de Excel, con todos los datos de un elemento BLOCK
                Dim param As String = oRowD.Cell(1).Value       ' Columna 1 = BLOCK
                If param = "" Then Exit For                     ' Solo por seguridad. Salid si no tiene valor
                '
                Dim clave As String = param & "·" & x - 1.ToString.PadLeft(3, "0"c)     ' Nombre Bloque · Número de fila (00X. Con 3 carácteres)
                Dim valoresFila As New Dictionary(Of String, String)
                For y As Integer = 1 To totalCol
                    Dim nombre As String = colCab(y)
                    Console.WriteLine(nombre)
                    'Debug.Print(nombre)
                    If nombre <> "" Then
                        Dim valor As String = oRowD.Cell(y).Value.ToString
                        valoresFila.Add(nombre, valor)
                    End If
                Next
                Try
                    resultado.Add(clave, valoresFila)
                    'resultado.Add(clave, Nothing)
                    'resultado(clave) = valoresFila
                Catch ex As Exception
                    ' Ya existe la clave. Sería un error de parámetro repetido. Inusual.
                    Debug.Print(ex.ToString)
                    Continue For
                End Try
                oRowD = Nothing
                valoresFila = Nothing
            Next
            '
        End Using
        '
        LimpiaMemoria()
        Return resultado
    End Function
    '
    ' Formato de Base de datos: Fila 1, con las cabeceras de columna
    ' El resto de filas, cada fila con todos los parametros del elemento
    ' Resultado Dictionary(Key: nombre único, Value: Dictionary(key: nombre columna, value: valor))
    Public Function Excel_DameFormato_DB_Hojas(fiExcel As String, arrNHojas As String(), Optional nRowCab As Integer = 1) As Dictionary(Of String, Dictionary(Of String, String))
        If IO.File.Exists(fiExcel) = False Then
            Return Nothing
            Exit Function
        End If
        '
        Dim dicDatos As New Dictionary(Of String, Dictionary(Of String, String))
        Using xlWb As XLWorkbook = New XLWorkbook(fiExcel)
            For Each nHoja As String In arrNHojas
                Dim xlWs As IXLWorksheet = Nothing
                ' Coger la hoja indicada (Nombre o número) o la 1 en caso de que no exista
                If nHoja IsNot Nothing Then
                    Try
                        xlWs = xlWb.Worksheet(nHoja)
                    Catch ex As Exception
                        xlWs = xlWb.Worksheet(1)
                    End Try
                Else
                    xlWs = xlWb.Worksheet(1)
                End If
                '
                ' *** Fila de cabeceras. Sacar todos los nombres de cabeceras a colCab(nº col, nombre col)
                Dim colCab As New Dictionary(Of Integer, String)    ' Key=numero de columna, Value=nombre columna
                Dim oRowCab As IXLRow = xlWs.Row(nRowCab)        ' Fila de cabeceras
                Dim totalCol As Integer = oRowCab.LastCellUsed.WorksheetColumn.ColumnNumber   ' Total columnas de cabeceras.
                For x As Integer = 1 To 1000    ' totalCol
                    Dim nombre As String = oRowCab.Cell(x).Value.ToString
                    If nombre <> "" Then
                        colCab.Add(x, nombre)
                    Else
                        Exit For
                    End If
                Next
                oRowCab = Nothing
                '
                ' *** Filas de datos. Sacar los datos de cada fila (Empezando en nRowCab + 1) a resultado(Value col 1 & nº fila -1, (nombre col, value)
                Dim oColIni As IXLColumn = xlWs.Column(1)
                Dim totalFil As Integer = oColIni.LastCellUsed.WorksheetRow.RowNumber - nRowCab
                For x As Integer = nRowCab + 1 To 10000   ' totalFil
                    Dim oRowD As IXLRow = xlWs.Row(x)               ' Fila de Excel, con todos los datos de un elemento BLOCK
                    Dim param As String = oRowD.Cell(1).Value       ' Columna 1 = BLOCK
                    If param = "" Then Exit For                     ' Solo por seguridad. Salid si no tiene valor
                    '
                    Dim clave As String = param & "·" & (x - 1).ToString.PadLeft(3, "0"c)     ' Nombre Bloque · Número de fila (00X. Con 3 carácteres)
                    Dim valoresFila As New Dictionary(Of String, String)
                    For y As Integer = 1 To totalCol
                        Dim nombre As String = colCab(y)
                        'Console.WriteLine(nombre)
                        'Debug.Print(nombre)
                        If nombre <> "" Then
                            Dim valor As String = oRowD.Cell(y).Value.ToString
                            valoresFila.Add(nombre, valor)
                        End If
                    Next
                    Try
                        dicDatos.Add(clave, valoresFila)
                        'resultado.Add(clave, Nothing)
                        'resultado(clave) = valoresFila
                    Catch ex As Exception
                        ' Ya existe la clave. Sería un error de parámetro repetido. Inusual.
                        Debug.Print(ex.ToString)
                        Continue For
                    End Try
                    oRowD = Nothing
                    valoresFila = Nothing
                Next
                xlWs = Nothing
            Next
        End Using
        '
        LimpiaMemoria()
        Return dicDatos
    End Function
    Public Function Excel_DameParametrosEnColumnas(fiExcel As String, cellIni As String, Optional nHoja As Object = Nothing) As Dictionary(Of String, String())
        If IO.File.Exists(fiExcel) = False Then
            Return Nothing
            Exit Function
        End If
        'key=nombre de parámetro
        Dim resultado As New Dictionary(Of String, String())
        Using xlWb As XLWorkbook = New XLWorkbook(fiExcel)
            Dim xlWs As IXLWorksheet = Nothing
            ' Coger la hoja indicada (Nombre o número) o la 1 en caso de que no exista
            If nHoja IsNot Nothing Then
                Try
                    xlWs = xlWb.Worksheet(nHoja)
                Catch ex As Exception
                    xlWs = xlWb.Worksheet(1)
                End Try
            End If
            '
            Dim xlWc As IXLCell = xlWs.Cell(cellIni)
            Dim row As IXLRow = xlWc.WorksheetRow
            Dim nrow As Integer = row.RowNumber
            Dim col As IXLColumn = xlWc.WorksheetColumn
            Dim ncol As Integer = col.ColumnNumber
            '
            For x As Integer = ncol To 10000
                Dim param As String = xlWs.Cell(nrow, x).Value
                If param = "" Then Exit For
                '
                Dim datos(2) As String
                ' 0=valor o ecuación
                ' 1=unidad de medida
                ' 2=comentario.
                datos(0) = xlWs.Cell(nrow + 1, x).Value
                datos(1) = xlWs.Cell(nrow + 2, x).Value
                datos(2) = xlWs.Cell(nrow + 3, x).Value
                Try
                    resultado.Add(param, datos)
                Catch ex As Exception
                    ' Ya existe la clave. Sería un error de parámetro repetido. Inusual.
                    Continue For
                End Try
            Next
        End Using
        '
        LimpiaMemoria()
        Return resultado
    End Function
    ' Siempre empieza en la primera columna de la fila indicada
    Public Function Excel_DameParametrosEnColumnas(fiExcel As String, nRow As Integer, Optional nHoja As Object = Nothing) As Dictionary(Of String, String())
        If IO.File.Exists(fiExcel) = False Then
            Return Nothing
            Exit Function
        End If
        'key=nombre de parámetro
        Dim resultado As New Dictionary(Of String, String())
        Using xlWb As XLWorkbook = New XLWorkbook(fiExcel)
            Dim xlWs As IXLWorksheet = Nothing
            ' Coger la hoja indicada (Nombre o número) o la 1 en caso de que no exista
            If nHoja IsNot Nothing Then
                Try
                    xlWs = xlWb.Worksheet(nHoja)
                Catch ex As Exception
                    xlWs = xlWb.Worksheet(1)
                End Try
            Else
                xlWs = xlWb.Worksheet(1)
            End If
            '
            'Dim xlWc As IXLCell = xlWs.Cell(nRow, 1)
            'Dim row As IXLRow = xlWc.WorksheetRow
            'Dim col As IXLColumn = xlWc.WorksheetColumn
            'Dim ncol As Integer = col.ColumnNumber
            '
            'Dim contadorCol As Integer = ncol
            For x As Integer = 1 To 10000
                Dim param As String = xlWs.Cell(nRow, x).Value
                If param = "" Then Exit For
                '
                Dim datos(2) As String
                ' 0=valor o ecuación
                ' 1=unidad de medida
                ' 2=comentario.
                datos(0) = xlWs.Cell(nRow + 1, x).Value
                datos(1) = xlWs.Cell(nRow + 2, x).Value
                datos(2) = xlWs.Cell(nRow + 3, x).Value
                Try
                    resultado.Add(param, datos)
                Catch ex As Exception
                    ' Ya existe la clave. Sería un error de parámetro repetido. Inusual.
                    Continue For
                End Try
            Next
            '
        End Using
        '
        LimpiaMemoria()
        Return resultado
    End Function
    '
    Public Function Excel_DameMultiParametrosEnColumnas(fiExcel As String, nRow As Integer, Optional nHoja As Object = Nothing) As Dictionary(Of String, List(Of String))
        If IO.File.Exists(fiExcel) = False Then
            Return Nothing
            Exit Function
        End If
        'key=nombre de parámetro
        Dim resultado As New Dictionary(Of String, List(Of String))
        Using xlWb As XLWorkbook = New XLWorkbook(fiExcel)
            Dim xlWs As IXLWorksheet = Nothing
            ' Coger la hoja indicada (Nombre o número) o la 1 en caso de que no exista
            If nHoja IsNot Nothing Then
                Try
                    xlWs = xlWb.Worksheet(nHoja)
                Catch ex As Exception
                    xlWs = xlWb.Worksheet(1)
                End Try
            Else
                xlWs = xlWb.Worksheet(1)
            End If
            '
            '
            'Dim contadorCol As Integer = ncol
            For x As Integer = 1 To 10000
                Dim param As String = xlWs.Cell(nRow, x).Value
                If param = "" Then Exit For
                '
                'Dim datos(-1) As String      ' Array con los Multivalues
                Dim datos As New List(Of String)
                For y As Integer = 1 To 1000
                    Dim multi As String = xlWs.Cell(nRow + y, x).Value
                    ' Si no tiene valor, pasar a la siguiente columna
                    If multi = "" Then Exit For
                    ' Tiene valor, añadirlo al array
                    'ReDim Preserve datos(UBound(datos) + 1)
                    'datos(UBound(datos)) = multi
                    datos.Add(multi)
                Next
                Try
                    resultado.Add(param, datos)
                Catch ex As Exception
                    ' Ya existe la clave. Sería un error de parámetro repetido. Inusual.
                    Continue For
                End Try
            Next
            '
        End Using
        '
        LimpiaMemoria()
        Return resultado
    End Function
    '
    Public Function Excel_LeeFilar(ByVal queExcel As String, queHojas As String(), Optional concabeceras As Boolean = True) As List(Of IXLRow)
        ' concabeceras = True, fila 1 serán las cabeceras de columna.
        ' concabeceras = False, fila 1 será la primera fila de datos.
        Dim resultado As New List(Of IXLRow)
        '
        ' ***** Cerrar Excel
        'CierraProceso("EXCEL")
        '
        ' quePlantilla = "". Escribiremos en queExcelFin original
        Using xlWb As XLWorkbook = New XLWorkbook(queExcel, XLEventTracking.Disabled)
            Dim xlWs As IXLWorksheet = Nothing
            For Each queHoja As String In queHojas
                Try
                    xlWs = xlWb.Worksheet(queHoja)
                Catch ex As Exception
                    Continue For
                End Try
                Dim columnas As Integer = xlWs.FirstRow.CellsUsed.Count
                Dim filas As Integer = xlWs.FirstColumn.CellsUsed.Count
                'Dim todo As IXLRange = xlWs.CellsUsed
                '
                ' ***** Iteramos con las filas
                For x As Integer = 1 To filas
                    Dim valor As String = xlWs.Row(x).FirstCell.Value.ToString
                    If valor = "" Then
                        Exit For
                    Else
                        resultado.Add(xlWs.Row(x))
                    End If
                Next
            Next
        End Using
        ' Efectuamos una recolección de elementos no utilizados,
        ' ya que no se cierra la instancia de Excel existente
        ' en el Administrador de Tareas.
        LimpiaMemoria()
        Return resultado
    End Function
    Public Function Excel_LeeFilar(ByVal queExcel As String, Optional queHoja As String = "", Optional concabeceras As Boolean = True) As List(Of IXLRow)
        ' concabeceras = True, fila 1 serán las cabeceras de columna.
        ' concabeceras = False, fila 1 será la primera fila de datos.
        Dim resultado As New List(Of IXLRow)
        '
        ' ***** Cerrar Excel
        'CierraProceso("EXCEL")
        '
        ' quePlantilla = "". Escribiremos en queExcelFin original
        Using xlWb As XLWorkbook = New XLWorkbook(queExcel, XLEventTracking.Disabled)
            Dim xlWs As IXLWorksheet = Nothing
            ' ***** Referenciamos la Hoja indicada o la primera hoja del libro de trabajo
            If queHoja = "" Then
                xlWs = xlWb.Worksheet(1)
            Else
                Try
                    xlWs = xlWb.Worksheet(queHoja)
                Catch ex As Exception
                    resultado = Nothing
                    Return resultado
                    Exit Function
                    'xlWs = xlWb.Worksheet(1)
                    'xlWs.Name = queHoja
                End Try
            End If
            Dim columnas As Integer = xlWs.FirstRow.CellsUsed.Count
            Dim filas As Integer = xlWs.FirstColumn.CellsUsed.Count
            'Dim todo As IXLRange = xlWs.CellsUsed
            '
            ' ***** Iteramos con las filas
            For x As Integer = 1 To filas
                Dim valor As String = xlWs.Row(x).FirstCell.Value.ToString
                If valor = "" Then
                    Exit For
                Else
                    resultado.Add(xlWs.Row(x))
                End If
            Next
        End Using
        ' Efectuamos una recolección de elementos no utilizados,
        ' ya que no se cierra la instancia de Excel existente
        ' en el Administrador de Tareas.
        LimpiaMemoria()
        Return resultado
    End Function

    Public Function Excel_DameValorEnRango(fiExcel As String, queRango As String, Optional nHoja As Object = Nothing) As Dictionary(Of String, Object)
        If IO.File.Exists(fiExcel) = False Then
            Return Nothing
            Exit Function
        End If
        'key=nombre de parámetro
        Dim resultado As New Dictionary(Of String, Object)
        Using xlWb As XLWorkbook = New XLWorkbook(fiExcel)
            Dim xlWs As IXLWorksheet = Nothing
            ' Coger la hoja indicada (Nombre o número) o la 1 en caso de que no exista
            If nHoja IsNot Nothing Then
                Try
                    xlWs = xlWb.Worksheet(nHoja)
                Catch ex As Exception
                    xlWs = xlWb.Worksheet(1)
                End Try
            Else
                xlWs = xlWb.Worksheet(1)
            End If
            '
            Dim xlRange As IXLRange = Nothing
            xlRange = xlWs.Range(queRango)
            For Each xlCell As IXLCell In xlRange.Cells
                resultado.Add(xlCell.Address.ToString(XLReferenceStyle.R1C1, True), xlCell.Value)
            Next
            xlRange = Nothing
            '
        End Using
        '
        LimpiaMemoria()
        Return resultado
    End Function
    '
    Public Function Excel_DameValorEnRango(fiExcel As String, queCellIni As String, queCellFin As String, Optional nHoja As Object = Nothing) As Dictionary(Of String, Object)
        If IO.File.Exists(fiExcel) = False Then
            Return Nothing
            Exit Function
        End If
        'key=nombre de parámetro
        Dim resultado As New Dictionary(Of String, Object)
        Using xlWb As XLWorkbook = New XLWorkbook(fiExcel)
            Dim xlWs As IXLWorksheet = Nothing
            ' Coger la hoja indicada (Nombre o número) o la 1 en caso de que no exista
            If nHoja IsNot Nothing Then
                Try
                    xlWs = xlWb.Worksheet(nHoja)
                Catch ex As Exception
                    xlWs = xlWb.Worksheet(1)
                End Try
            Else
                xlWs = xlWb.Worksheet(1)
            End If
            '
            Dim xlRange As IXLRange = Nothing
            xlRange = xlWs.Range(queCellIni, queCellFin)
            For Each xlCell As IXLCell In xlRange.Cells
                resultado.Add(xlCell.Address.ToString(XLReferenceStyle.R1C1, True), xlCell.Value)
            Next
            xlRange = Nothing
            '
        End Using
        '
        LimpiaMemoria()
        Return resultado
    End Function

    Public Function Excel_DameValorEnRango(fiExcel As String, RowIni As String, ColIni As String, RowFin As String, ColFin As String, Optional nHoja As Object = Nothing) As Dictionary(Of String, Object)
        If IO.File.Exists(fiExcel) = False Then
            Return Nothing
            Exit Function
        End If
        'key=nombre de parámetro
        Dim resultado As New Dictionary(Of String, Object)
        Using xlWb As XLWorkbook = New XLWorkbook(fiExcel)
            Dim xlWs As IXLWorksheet = Nothing
            ' Coger la hoja indicada (Nombre o número) o la 1 en caso de que no exista
            If nHoja IsNot Nothing Then
                Try
                    xlWs = xlWb.Worksheet(nHoja)
                Catch ex As Exception
                    xlWs = xlWb.Worksheet(1)
                End Try
            Else
                xlWs = xlWb.Worksheet(1)
            End If
            '
            Dim xlRange As IXLRange = Nothing
            xlRange = xlWs.Range(RowIni, ColIni, RowFin, ColFin)
            For Each xlCell As IXLCell In xlRange.Cells
                resultado.Add(xlCell.Address.ToString(XLReferenceStyle.R1C1, True), xlCell.Value)
            Next
            'xlRange.Dispose()
            xlRange = Nothing
            '
        End Using
        '
        LimpiaMemoria()
        Return resultado
    End Function
    '
    Public Sub Excel_EscribeCell(fiExcel As String, queCell As String, queValor As Object, Optional nHoja As Object = Nothing)
        If IO.File.Exists(fiExcel) = False Then
            MsgBox("No existe el fichero " & fiExcel)
            Exit Sub
        End If
        'key=nombre de parámetro
        Dim resultado As New Dictionary(Of String, Object)
        Using xlWb As XLWorkbook = New XLWorkbook(fiExcel)
            Dim xlWs As IXLWorksheet = Nothing
            ' Coger la hoja indicada (Nombre o número) o la 1 en caso de que no exista
            If nHoja IsNot Nothing Then
                Try
                    xlWs = xlWb.Worksheet(nHoja)
                Catch ex As Exception
                    xlWs = xlWb.Worksheet(1)
                End Try
            Else
                xlWs = xlWb.Worksheet(1)
            End If
            '
            Dim xlCell As IXLCell = Nothing
            xlCell = xlWs.Cell(queCell)
            xlCell.Value = queValor
            xlCell = Nothing
            '
            xlWb.Save(New SaveOptions)
        End Using
        '
        LimpiaMemoria()
    End Sub
    '
    Public Sub Excel_EscribeCell(fiExcel As String, queCellRow As Integer, queCellCol As Integer, queValor As Object, Optional nHoja As Object = Nothing)
        If IO.File.Exists(fiExcel) = False Then
            MsgBox("No existe el fichero " & fiExcel)
            Exit Sub
        End If
        'key=nombre de parámetro
        Dim resultado As New Dictionary(Of String, Object)
        Using xlWb As XLWorkbook = New XLWorkbook(fiExcel)
            Dim xlWs As IXLWorksheet = Nothing
            ' Coger la hoja indicada (Nombre o número) o la 1 en caso de que no exista
            If nHoja IsNot Nothing Then
                Try
                    xlWs = xlWb.Worksheet(nHoja)
                Catch ex As Exception
                    xlWs = xlWb.Worksheet(1)
                End Try
            Else
                xlWs = xlWb.Worksheet(1)
            End If
            '
            Dim xlCell As IXLCell = Nothing
            xlCell = xlWs.Cell(queCellRow, queCellCol)
            xlCell.Value = queValor
            xlCell = Nothing
            '
            xlWb.Save(New SaveOptions)
        End Using
        '
        LimpiaMemoria()
    End Sub
    '
    Public Function Excel_DameValorEnCell(fiExcel As String, queCell As String, Optional nHoja As Object = Nothing) As Object
        If IO.File.Exists(fiExcel) = False Then
            Return Nothing
            Exit Function
        End If
        'key=nombre de parámetro
        Dim resultado As Object = Nothing
        Using xlWb As XLWorkbook = New XLWorkbook(fiExcel)
            Dim xlWs As IXLWorksheet = Nothing
            ' Coger la hoja indicada (Nombre o número) o la 1 en caso de que no exista
            If nHoja IsNot Nothing Then
                Try
                    xlWs = xlWb.Worksheet(nHoja)
                Catch ex As Exception
                    xlWs = xlWb.Worksheet(1)
                End Try
            Else
                xlWs = xlWb.Worksheet(1)
            End If
            '
            Dim xlCell As IXLCell = Nothing
            xlCell = xlWs.Cell(queCell)
            resultado = xlCell.Value
            xlCell = Nothing
            '
        End Using
        '
        LimpiaMemoria()
        Return resultado
    End Function
    '
    Public Function Excel_DameValorEnCell(fiExcel As String, queRow As Integer, queCol As Integer, Optional nHoja As Object = Nothing) As Object
        If IO.File.Exists(fiExcel) = False Then
            Return Nothing
            Exit Function
        End If
        'key=nombre de parámetro
        Dim resultado As Object = Nothing
        Using xlWb As XLWorkbook = New XLWorkbook(fiExcel)
            Dim xlWs As IXLWorksheet = Nothing
            ' Coger la hoja indicada (Nombre o número) o la 1 en caso de que no exista
            If nHoja IsNot Nothing Then
                Try
                    xlWs = xlWb.Worksheet(nHoja)
                Catch ex As Exception
                    xlWs = xlWb.Worksheet(1)
                End Try
            Else
                xlWs = xlWb.Worksheet(1)
            End If
            '
            Dim xlCell As IXLCell = Nothing
            xlCell = xlWs.Cell(queRow, queCol)
            resultado = xlCell.Value
            xlCell = Nothing
            '
        End Using
        '
        LimpiaMemoria()
        Return resultado
    End Function
    '

    Public Function Excel_DameValorEnCell(fiExcel As String, queCells As String(), Optional nHoja As Object = Nothing) As Dictionary(Of String, Object)
        If IO.File.Exists(fiExcel) = False Then
            Return Nothing
            Exit Function
        End If
        'key=nombre de parámetro
        Dim resultado As New Dictionary(Of String, Object)
        Using xlWb As XLWorkbook = New XLWorkbook(fiExcel)
            Dim xlWs As IXLWorksheet = Nothing
            ' Coger la hoja indicada (Nombre o número) o la 1 en caso de que no exista
            If nHoja IsNot Nothing Then
                Try
                    xlWs = xlWb.Worksheet(nHoja)
                Catch ex As Exception
                    xlWs = xlWb.Worksheet(1)
                End Try
            Else
                xlWs = xlWb.Worksheet(1)
            End If
            '
            For Each queCell As String In queCells
                Dim xlCell As IXLCell = Nothing
                xlCell = xlWs.Cell(queCell)
                resultado.Add(queCell, xlCell.Value)
                xlCell = Nothing
            Next
            '
        End Using
        '
        LimpiaMemoria()
        Return resultado
    End Function
    Public Sub Excel_EscribeCellsColeccion(fiExcel As String, queArrCells As String(), queArrValores As Object(), Optional nHoja As Object = Nothing)
        If IO.File.Exists(fiExcel) = False Then
            MsgBox("No existe el fichero " & fiExcel)
            Exit Sub
        End If
        'key=nombre de parámetro
        Dim resultado As New Dictionary(Of String, Object)
        Using xlWb As XLWorkbook = New XLWorkbook(fiExcel)
            Dim xlWs As IXLWorksheet = Nothing
            ' Coger la hoja indicada (Nombre o número) o la 1 en caso de que no exista
            If nHoja IsNot Nothing Then
                Try
                    xlWs = xlWb.Worksheet(nHoja)
                Catch ex As Exception
                    xlWs = xlWb.Worksheet(1)
                End Try
            Else
                xlWs = xlWb.Worksheet(1)
            End If
            '
            For x As Integer = LBound(queArrCells) To UBound(queArrCells)
                Dim xlCell As IXLCell = Nothing
                xlCell = xlWs.Cell(queArrCells(x))
                Try
                    xlCell.Value = queArrValores(x)
                Catch ex As Exception
                    ' Si no existia valor en el array de valores, escribimos ""
                    xlCell.Value = ""
                End Try
                xlCell = Nothing
            Next
            '
            xlWb.Save(New SaveOptions)
        End Using
        '
        LimpiaMemoria()
    End Sub
    Public Sub Excel_EscribeCellsColeccion(fiExcel As String, queHashtable As Hashtable, Optional nHoja As Object = Nothing)
        If IO.File.Exists(fiExcel) = False Then
            MsgBox("No existe el fichero " & fiExcel)
            Exit Sub
        End If
        'key=nombre de parámetro
        Dim resultado As New Dictionary(Of String, Object)
        Using xlWb As XLWorkbook = New XLWorkbook(fiExcel)
            Dim xlWs As IXLWorksheet = Nothing
            ' Coger la hoja indicada (Nombre o número) o la 1 en caso de que no exista
            If nHoja IsNot Nothing Then
                Try
                    xlWs = xlWb.Worksheet(nHoja)
                Catch ex As Exception
                    xlWs = xlWb.Worksheet(1)
                End Try
            Else
                xlWs = xlWb.Worksheet(1)
            End If
            '
            For Each queCell As String In queHashtable.Keys
                Dim xlCell As IXLCell = Nothing
                xlCell = xlWs.Cell(queCell)
                xlCell.Value = queHashtable(queCell)
                xlCell = Nothing
            Next
            '
            xlWb.Save(New SaveOptions)
        End Using
        '
        LimpiaMemoria()
    End Sub
    Public Function Excel_EscribeParametroEnColumna(fiExcel As String, nRow As Integer, quePar As String, queVal As Object, Optional nHoja As Object = Nothing) As Boolean
        If IO.File.Exists(fiExcel) = False Then
            Return False
            Exit Function
        End If
        'key=nombre de parámetro
        Dim resultado As Boolean = False
        Using xlWb As XLWorkbook = New XLWorkbook(fiExcel)
            Dim xlWs As IXLWorksheet = Nothing
            ' Coger la hoja indicada (Nombre o número) o la 1 en caso de que no exista
            If nHoja IsNot Nothing Then
                Try
                    xlWs = xlWb.Worksheet(nHoja)
                Catch ex As Exception
                    xlWs = xlWb.Worksheet(1)
                End Try
            Else
                xlWs = xlWb.Worksheet(1)
            End If
            '
            '
            'Dim contadorCol As Integer = ncol
            For x As Integer = 1 To 10000
                Dim param As String = xlWs.Cell(nRow, x).Value
                If param = "" Then
                    Exit For
                ElseIf param = quePar Then
                    resultado = True
                    xlWs.Cell(nRow + 1, x).Value = queVal
                    Exit For
                End If
            Next
            '
            If resultado = True Then
                xlWb.Save(New SaveOptions)
            End If
            '
        End Using
        '
        LimpiaMemoria()
        Return resultado
    End Function
    '
    Public Function Excel_BuscaNPieza(ByRef fiExcel As String,
                               datoBuscado As String,
                               Optional colDato As Integer = 1,
                               Optional filIni As Integer = 2,
                               Optional colIni As Integer = 1,
                               Optional colFin As Integer = 56,
                               Optional nHoja As Object = Nothing) As Hashtable
        '' Si no existe el fichero Excel, salimos devolviendo Nothing
        If IO.File.Exists(fiExcel) = False Then
            Return Nothing
            Exit Function
        End If
        '
        Dim resultado As New Hashtable
        CierraProceso("EXCEL")
        Using xlWb As XLWorkbook = New XLWorkbook(fiExcel)
            Dim xlWs As IXLWorksheet = Nothing
            ' Coger la hoja indicada (Nombre o número) o la 1 en caso de que no exista
            If nHoja IsNot Nothing Then
                ' Referenciamos hoja indicada (Nombre o Número) o la primera si da error
                Try
                    xlWs = xlWb.Worksheet(nHoja)
                Catch ex As Exception
                    xlWs = xlWb.Worksheet(1)
                End Try
            Else
                ' Referenciamos la primera hoja del libro de trabajo
                xlWs = xlWb.Worksheet(1)
            End If
            '
            ' Recorremos las filas, columna A para buscar el valor
            Dim valor As Object = Nothing
            For Each xlR As IXLRow In xlWs.Rows(filIni, xlWs.LastRowUsed.RowNumber)
                If xlR.RowNumber < filIni Then Continue For
                '
                valor = xlR.Cell(colDato).Value
                If valor Is Nothing Then Exit For
                If valor.ToString = "" Then Exit For
                'Debug.Print(valor)
                If valor.ToString = datoBuscado Then
                    For x As Integer = 1 To colFin  ' Each oRan1 As Excel.Range In oRan.Columns
                        ' La columna 56 /  BD  (CALIBRADO) será la última 
                        Try
                            resultado.Add(x, xlR.Cell(x).Value)
                        Catch ex As Exception
                            resultado.Add(x, "")
                        End Try
                    Next
                    Exit For
                End If
            Next
            '
        End Using
        '
        ' Efectuamos una recolección de elementos no utilizados,
        ' ya que no se cierra la instancia de Excel existente
        ' en el Administrador de Tareas.
        '
        LimpiaMemoria()
        '
        Return resultado
    End Function
    '
    'Public Sub Excel_EscribeCeldaLibroNuevo(ByVal queExcelFin As String,
    '                                  ByVal queCeldaValor As Hashtable,
    '                                  Optional ByVal quePlantilla As String = "",
    '                                  Optional ByVal queHoja As String = "",
    '                                    Optional ByVal ajustacolumna As Boolean = False,
    '                                    Optional arrCampos() As String = Nothing,
    '                                    Optional filaNombres As Integer = 1,
    '                                    Optional inicioborrado As Integer = -1)
    '    '
    '    Utilidades.CierraProceso("EXCEL")
    '    If IO.File.Exists(queExcelFin) = True Then IO.File.Delete(queExcelFin)
    '    If IO.File.Exists(quePlantilla) = False Then
    '        MsgBox("No existe la plantilla " & vbCrLf & vbCrLf & quePlantilla)
    '        Exit Sub
    '    End If
    '    '' Copiar la plantilla a la nueva ubicación y con el nuevo nombre. Y También ListaCorte con el nuevo nombre.
    '    'modClosedXML.XLSM_GuardaMacros(quePlantilla)
    '    IO.File.Copy(quePlantilla, queExcelFin)
    '    '' Nombre final del fichero de ListaCorte de queExcelFin
    '    Dim queExcelListaCorteFin As String = queExcelFin.Replace(".xlsm", "_ListaCorte.xlsx")
    '    ''
    '    'Me.CierraFicheroExcel(queExcelListaCorteFin, False, False)
    '    Try
    '        If IO.File.Exists(queExcelListaCorteFin) = True Then IO.File.Delete(queExcelListaCorteFin)
    '        'Me.CierraFicheroExcel(PlantillaListaCorte, False, False)
    '        IO.File.Copy(PlantillaListaCorte, queExcelListaCorteFin)
    '    Catch ex As Exception
    '        '' No hacemos nada. Ya existe.
    '    End Try
    '    ' Copiar la plantilla con el nuevo nombre. Con SaveAs
    '    ' Dim xlWbP As XLWorkbook = New XLWorkbook(quePlantilla)
    '    'xlWbP.SaveAs(queExcelFin)
    '    'xlWbP = Nothing
    '    Using xlWb As XLWorkbook = New XLWorkbook(queExcelFin, XLEventTracking.Disabled)
    '        Dim xlWs As IXLWorksheet = Nothing
    '        Dim ultimaFilaDatos As Integer = 0
    '        '
    '        '' Ponemos la propiedad BDCnc con el camino de ProgramasCNC.xlsx
    '        '' Ponemos la propiedad Calibrados con el camino de Calibrados.xlsx
    '        xlWb.CustomProperty("BDCnc").Value = PlantillaExcelCnc
    '        xlWb.CustomProperty("Calibrados").Value = PlantillaExcelCalibrados
    '        xlWb.CustomProperty("ListaCorte").Value = queExcelListaCorteFin  'PlantillaListaCorte
    '        '
    '        ' Referenciamos la primera hoja del libro de trabajo
    '        xlWs = xlWb.Worksheet(1)
    '        If queHoja <> "" Then xlWs.Name = queHoja
    '        '' Iteramos con el Hashtable enviado (celda, valor)
    '        For Each queCelda As String In queCeldaValor.Keys
    '            '' «Hoja1!CF8»
    '            'Dim r As Excel.Range = ws.Range("Hoja1!" & queCelda)
    '            Dim r As IXLCell = xlWs.Cell(queCelda)
    '            ' Establecemos el nuevo valor de la celda
    '            '
    '            If queCeldaValor(queCelda) Is Nothing Then Continue For
    '            If queCeldaValor(queCelda).ToString.StartsWith("=") Then
    '                'r.FormulaR1C1Local = queCeldaValor(queCelda)
    '                r.FormulaA1 = queCeldaValor(queCelda)
    '            Else
    '                r.Value = queCeldaValor(queCelda)
    '            End If
    '            If ajustacolumna = True Then r.WorksheetColumn.AdjustToContents()
    '            If r.WorksheetRow.RowNumber > ultimaFilaDatos Then
    '                ultimaFilaDatos = r.WorksheetRow.RowNumber
    '            End If
    '            r = Nothing
    '        Next
    '        '' Ordenar por los campos (Lo quitamos, ya que no ordena bien.
    '        Try
    '            If arrCampos IsNot Nothing Then Excel_OrdenaDatosFiltro(xlWs, arrCampos, filaNombres, ultimaFilaDatos)
    '        Catch ex As Exception
    '            'MsgBox("Error ordenando campos...")
    '            Debug.Print(ex.Message)
    '        End Try
    '        '
    '        ' Borrar las filas sobrantes
    '        If IsNumeric(inicioborrado) AndAlso inicioborrado > 0 Then
    '            xlWs.Rows(inicioborrado, inicioborrado + 1000).Clear()
    '        End If
    '        '' Finalmente guardamos la Excel.
    '        xlWb.SaveAs(queExcelFin, New SaveOptions)
    '        'xlWb.Save(New SaveOptions)
    '        '
    '    End Using
    '    '
    '    ' Efectuamos una recolección de elementos no utilizados,
    '    ' ya que no se cierra la instancia de Excel existente
    '    ' en el Administrador de Tareas.
    '    '
    '    LimpiaMemoria()
    'End Sub
    '
    Public Sub Excel_OrdenaDatosFiltro(ByRef oWs As IXLWorksheet, arrCampos() As String, filaNombres As Integer, Optional ultimafila As Integer = 0, Optional nHoja As Object = Nothing)
        '' Salimos si no hay campos para ordenar
        If arrCampos Is Nothing Then Exit Sub ' arrCampos.Length = 0 Then Exit Sub
        If filaNombres = 0 Then filaNombres = 1
        If ultimafila = 0 Then ultimafila = oWs.RangeUsed.Rows.Last.RowNumber
        '
        '' Quitamos otros filtros que ya hubiera
        'oWs.Tables.FirstOrDefault.ShowAutoFilter = False
        'oWs.Tables.FirstOrDefault.AutoFilter.Enabled = False
        'oWs.AutoFilter.Clear()
        'oWs.AutoFilter.Sorted = False
        'oWs.AutoFilter.Enabled = False
        'oWs.RangeUsed.SetAutoFilter(False)
        '
        'oWs.RangeUsed.SetAutoFilter.Column(1).Between(oWs.RangeUsed.FirstCell.Address, oWs.RangeUsed.LastCell.Address)
        'Call oWs.RangeUsed.SetAutoFilter(True)
        'Dim oRan As IXLRange = oWs.Ranges("DATOS").FirstOrDefault
        'Dim oRanSort As IXLRange = oWs.Range(oRan.FirstRow.RowNumber + 1, oRan.FirstColumn.ColumnNumber, oRan.LastRow.RowNumber, oRan.LastColumn.ColumnNumber)
        'oRan.Select()
        oWs.AutoFilter.Clear()
        Dim colSort As New List(Of Integer)
        For x As Integer = 0 To UBound(arrCampos)
            If arrCampos(x) = "" Then Continue For
            Dim oCell As IXLCell = Nothing ' oWs.Range("A" & filaNombres & ":ZZ" & filaNombres).Find(arrCampos(x))

            For Each oCol As IXLColumn In oWs.Columns
                Dim valor As String = oCol.Cell(filaNombres).Value
                If valor = "" Then Exit For
                'Debug.Print(valor)
                If valor = arrCampos(x) Then
                    'Call oCol.Sort()
                    'oRan.Sort(oCol.ColumnNumber)
                    'oWs.AutoFilter.Sort(oCol.ColumnNumber)
                    'Call oRanSort.Sort(oCol.ColumnNumber)
                    'oRan.SetAutoFilter.Column(oCol.ColumnNumber)
                    'oWs.AutoFilter.Sort(oCol.ColumnNumber, XLSortOrder.Ascending, False, True)
                    colSort.Add(oCol.ColumnNumber)
                    Exit For
                End If
            Next
        Next
        ' Poner el filtro, una vez localizadas las columnas
        If colSort.Count > 0 Then
            Dim oRow As IXLRange = oWs.Rows(filaNombres, filaNombres).FirstOrDefault.AsRange
            Dim oFilt As IXLAutoFilter = oRow.SetAutoFilter()
            For Each queCol As Integer In colSort
                oRow.SortColumns.Add(queCol, XLSortOrder.Ascending)
            Next
            oRow.Sort()
        End If
    End Sub
End Module
