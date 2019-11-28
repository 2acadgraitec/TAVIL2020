Imports System.Linq
Imports ce = ClosedXML.Excel
Imports System.Windows.Forms

Public Class TRANSPORTADORESExcelFilas
    Public nHojas() As String = {"TRD3-TRANSP.RODILLOS", "TRD3-TRANSP.BAJADA_RODILLOS_G", "TRD3-TRANSP.CURVA_RODILLOS", "TCB3-TRANSP.CON_BANDA"}
    Public Campos As List(Of String)
    Public filas As List(Of TRANSPORTADORESExcelFila)        ' Key = ITEM_NUMBER, Value = TRANSPORTADORESExcelFila
    Public Sub New()
        If cfg Is Nothing Then cfg = New UtilesAlberto.Conf(System.Reflection.Assembly.GetExecutingAssembly)
        'If cXML Is Nothing Then cXML = New ClosedXML2acad.ClosedXML2acad
        Campos = New List(Of String)
        filas = New List(Of TRANSPORTADORESExcelFila)
        '
        For Each nHoja In nHojas
            For Each fila As ce.IXLRow In modClosedXMLTavil.Excel_LeeFilar(LAYOUTDB, nHoja, concabeceras:=True).AsParallel
                If fila.RowNumber = 1 Then
                    For Each oCe As ce.IXLCell In fila.CellsUsed
                        Campos.Add(Convert.ToString(oCe.Value))
                    Next
                Else
                    filas.Add(New TRANSPORTADORESExcelFila(fila))
                End If
                System.Windows.Forms.Application.DoEvents()
            Next
        Next
    End Sub

    Public Function Fila_BuscaDame(cod As String) As List(Of TRANSPORTADORESExcelFila)
        Dim resultado As List(Of TRANSPORTADORESExcelFila) = Nothing
        '
        Dim filas As IEnumerable(Of TRANSPORTADORESExcelFila) = From x In Me.filas
                                                                Where x.dicDatos("CODE").ToUpper.Trim = cod.Trim.ToUpper
        '
        If filas.Count > 0 Then
            resultado = filas.ToList
        End If
        '
        Return resultado
    End Function

    Public Function Fila_BuscaDame(filtros As Dictionary(Of String, String)) As List(Of TRANSPORTADORESExcelFila)
        Dim resultado As List(Of TRANSPORTADORESExcelFila) = Nothing
        '
        Dim filas As IEnumerable(Of TRANSPORTADORESExcelFila) = Me.filas
        For Each name As String In filtros.Keys
            Dim filasX As IEnumerable(Of TRANSPORTADORESExcelFila) = From x In filas
                                                                     Where x.dicDatos(name).ToUpper.Trim = filtros(name).ToUpper.Trim
                                                                     Select x

            filas = filasX
            If filas Is Nothing OrElse filas.Count = 0 Then Exit For
        Next
        '
        If filas IsNot Nothing AndAlso filas.Count > 0 Then
            resultado = filas.ToList
        End If
        '
        Return resultado
    End Function
End Class


Public Class TRANSPORTADORESExcelFila
    Public nFila As Integer = -1
    Public dicDatos As Dictionary(Of String, String)
    Public txtDatos As String = ""

    Public Sub New(fila As ce.IXLRow)
        dicDatos = New Dictionary(Of String, String)
        nFila = fila.RowNumber
        For Each oCell As ce.IXLCell In fila.Cells.AsParallel
            Dim cabecera As String = oCell.WorksheetColumn.FirstCell.Value.ToString.Trim
            Dim valor As String = Convert.ToString(oCell.Value).Trim
            dicDatos.Add(cabecera, valor)
        Next
        txtDatos = String.Join("·", dicDatos.Values.ToArray)
    End Sub
End Class
