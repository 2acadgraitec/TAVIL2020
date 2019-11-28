Imports System.Linq
Imports ce = ClosedXML.Excel

Public Class clsPT
    Public nHoja As String = HojaPatas
    'Public _Campos As String() = {
    '"CODE", "BLOCK", "LENGTH", "WIDTH", "RADIUS", "HEIGHT", "HAND", "MOTOR_POSITION", "MOTOR_OFFSET", "OTOR_TYPE",
    '"FLAT_BELT", "O_RING_BELT", "DESCRIPTION", "ESPECIFICATION", "INFEED_HEIGHT", "OUTFEED_HEIGHT", "ITEM_NUMBER", "O_RING_PROTECTION",
    '"STANDARD_PART", "CONVEYOR_BELT_TYPE", "UNDERBELT", "UNDER_CONVEYOR_PROTECTION", "BELT_REFERENCE", "NUM_MOTORS", "SUPPORT_TYPE",
    '"REAL_LENGHT", "CONVEYOR_HEIGHT_1", "CONVEYOR_HEIGHT_2", "CONVEYOR_HEIGHT_3", "CONVEYOR_HEIGHT_4", "ANGLE", "FRAME"}
    Public Campos As List(Of String)
    Public filas As List(Of PTItem)        ' Key = ITEM_NUMBER, Value = clsPTItem
    Public PTRXXX As String() = {"PTR400"}    ' WIDTH es un parámetro
    Public PTCXXX As String() = {"PTC170", "PTC300", "PTC500"}              ' RADIUS es fijo en cada uno
    Public PTRXXXDH As String() = {"PTR400DH", "PTR600DH"}                  ' WIDTH es fijo en cada uno
    '
    '
    Public Sub New()
        If cfg Is Nothing Then cfg = New UtilesAlberto.Conf(System.Reflection.Assembly.GetExecutingAssembly)
        'If cXML Is Nothing Then cXML = New ClosedXML2acad.ClosedXML2acad
        Campos = New List(Of String)
        filas = New List(Of PTItem)
        '
        For Each fila As ce.IXLRow In modClosedXMLTavil.Excel_LeeFilar(LAYOUTDB, nHoja, concabeceras:=True).AsParallel
            If fila.RowNumber = 1 Then
                For Each oCe As ce.IXLCell In fila.CellsUsed
                    Campos.Add(Convert.ToString(oCe.Value))
                Next
            Else
                filas.Add(New PTItem(fila))
            End If
            System.Windows.Forms.Application.DoEvents()
        Next
        '
        'Dim colItem = From it In filas
        '              Select it.nFila & " / " & it.ITEM_NUMBER

        'MsgBox(String.Join(", ", colItem.ToArray))
        ' Llenar Arrays de nombres de bloques, desde Excel
        PTRXXX = Me.Filas_DameBLOCKSUnicos(TipoPata.PTR)
        PTCXXX = Me.Filas_DameBLOCKSUnicos(TipoPata.PTC)
        PTRXXXDH = Me.Filas_DameBLOCKSUnicos(TipoPata.PTRDH)
    End Sub
    '
    Public Function Filas_DameITEM_NUMBER(nCODE As String) As String
        Dim resultado As String = ""
        Dim nombres As IEnumerable(Of PTItem) = Nothing
        nombres = From fi In filas
                  Where fi.ITEM_NUMBER = nCODE
                  Select fi

        If nombres.Count > 0 Then
            resultado = nombres.FirstOrDefault.ITEM_NUMBER
        End If
        Return resultado
    End Function

    Public Function Filas_DameCODE(nBLOQUE As String, nWIDTH As String, nHEIGHT As String, Optional esRadius As Boolean = False) As String
        Dim resultado As String = ""
        Dim nombres As IEnumerable(Of PTItem) = Nothing
        nombres = From fi In filas
                  Where fi.BLOCK = nBLOQUE And IIf(esRadius, fi.RADIUS = nWIDTH, fi.WIDTH = nWIDTH) And fi.HEIGHT = nHEIGHT
                  Select fi

        If nombres.Count > 0 Then
            resultado = nombres.FirstOrDefault.CODE
        End If
        Return resultado
    End Function

    Public Function Filas_DameHEIGHTs(nBLOCK As String) As List(Of String)
        Dim alturas As IEnumerable(Of String) = Nothing
        alturas = From fi In filas
                  Where fi.BLOCK = nBLOCK
                  Select fi.HEIGHT
                  Distinct

        If alturas Is Nothing OrElse alturas.Count = 0 Then
            Return Nothing
        Else
            Return alturas.ToList
        End If
    End Function
    Public Function Filas_DameWIDTHs(nBLOCK As String()) As List(Of String)
        Dim anchos As IEnumerable(Of String) = Nothing
        anchos = From fi In filas
                 Where nBLOCK.Contains(fi.BLOCK)
                 Select fi.WIDTH
                 Distinct

        If anchos Is Nothing OrElse anchos.Count = 0 Then
            Return Nothing
        Else
            Return anchos.ToList
        End If
    End Function
    Public Function Filas_DameRADIUSs(nBLOCK As String()) As List(Of String)
        Dim radios As IEnumerable(Of String) = Nothing
        radios = From fi In filas
                 Where nBLOCK.Contains(fi.BLOCK)
                 Select fi.RADIUS
                 Distinct

        If radios Is Nothing OrElse radios.Count = 0 Then
            Return Nothing
        Else
            Return radios.ToList
        End If
    End Function

    Public Function Filas_DameValorConCODE(nCODE As String, nombreCol As nombreColumnaPT) As String
        Dim valor As IEnumerable(Of String) = Nothing
        Select Case nombreCol
            Case nombreColumnaPT.BLOCK
                valor = From fi In filas
                        Where fi.CODE = nCODE
                        Select fi.BLOCK
            Case nombreColumnaPT.LENGTH
                valor = From fi In filas
                        Where fi.CODE = nCODE
                        Select fi.LENGTH
            Case nombreColumnaPT.WIDTH
                valor = From fi In filas
                        Where fi.CODE = nCODE
                        Select fi.WIDTH
            Case nombreColumnaPT.RADIUS
                valor = From fi In filas
                        Where fi.CODE = nCODE
                        Select fi.RADIUS
                        Distinct
            Case nombreColumnaPT.HEIGHT
                valor = From fi In filas
                        Where fi.CODE = nCODE
                        Select fi.HEIGHT
            Case nombreColumnaPT.DESCRIPTION
                valor = From fi In filas
                        Where fi.CODE = nCODE
                        Select fi.DESCRIPTION
            Case nombreColumnaPT.ITEM_NUMBER
                valor = From fi In filas
                        Where fi.CODE = nCODE
                        Select fi.ITEM_NUMBER
                        Distinct
            Case nombreColumnaPT.CONVEYOR_HEIGHT_1
                valor = From fi In filas
                        Where fi.CODE = nCODE
                        Select fi.CONVEYOR_HEIGHT_1
        End Select

        If valor Is Nothing OrElse valor.Count = 0 Then
            Return ""
        Else
            Return valor.FirstOrDefault.ToString
        End If
    End Function
    ' Que 
    Public Function Filas_DameBLOCKSUnicos(queTipoPata As TipoPata) As String()
        Dim nombres As IEnumerable(Of String) = Nothing
        Select Case queTipoPata
            Case TipoPata.PTR
                nombres = From fi In filas
                          Where fi.BLOCK.StartsWith(TipoPata.PTR.ToString) And fi.BLOCK.Contains("DH") = False
                          Select fi.BLOCK
                          Distinct
            Case TipoPata.PTC
                nombres = From fi In filas
                          Where fi.BLOCK.StartsWith(TipoPata.PTC.ToString)
                          Select fi.BLOCK
                          Distinct
            Case TipoPata.PTRDH
                nombres = From fi In filas
                          Where fi.BLOCK.StartsWith(TipoPata.PTR.ToString) And fi.BLOCK.Contains("DH") = True
                          Select fi.BLOCK
                          Distinct
        End Select
        '
        If nombres Is Nothing Then
            Return Nothing
        Else
            Return nombres.ToArray
        End If
    End Function

    Public Function Filas_DameColumnaUnicos(nombreCol As nombreColumnaPT) As List(Of String)
        Dim nombres As IEnumerable(Of String) = Nothing
        Select Case nombreCol
            Case nombreColumnaPT.BLOCK
                nombres = From fi In filas
                          Select fi.BLOCK
                          Distinct
            Case nombreColumnaPT.LENGTH
                nombres = From fi In filas
                          Where fi.LENGTH <> "" And fi.LENGTH <> "-"
                          Select fi.LENGTH
                          Distinct
            Case nombreColumnaPT.WIDTH
                nombres = From fi In filas
                          Where fi.WIDTH <> "" And fi.WIDTH <> "-"
                          Select fi.WIDTH
                          Distinct
            Case nombreColumnaPT.RADIUS
                nombres = From fi In filas
                          Where fi.RADIUS <> "" And fi.RADIUS <> "-"
                          Select fi.RADIUS
                          Distinct
            Case nombreColumnaPT.HEIGHT
                nombres = From fi In filas
                          Where fi.HEIGHT <> "" And fi.HEIGHT <> "-"
                          Select fi.HEIGHT
                          Distinct
            Case nombreColumnaPT.DESCRIPTION
                nombres = From fi In filas
                          Where fi.DESCRIPTION <> "" And fi.DESCRIPTION <> "-"
                          Select fi.DESCRIPTION
                          Distinct
            Case nombreColumnaPT.ITEM_NUMBER
                nombres = From fi In filas
                          Where fi.ITEM_NUMBER <> "" And fi.ITEM_NUMBER <> "-"
                          Select fi.ITEM_NUMBER
                          Distinct
            Case nombreColumnaPT.CONVEYOR_HEIGHT_1
                nombres = From fi In filas
                          Where fi.CONVEYOR_HEIGHT_1 <> "" And fi.CONVEYOR_HEIGHT_1 <> "-"
                          Select fi.CONVEYOR_HEIGHT_1
                          Distinct
        End Select
        '
        If nombres Is Nothing Then
            Return Nothing
        Else
            Return nombres.ToList
        End If
    End Function
End Class

Public Class PTItem
    Public nFila As Integer = -1
    Public CODE As String = ""
    Public BLOCK As String = ""
    Public LENGTH As String = ""
    Public WIDTH As String = ""
    Public RADIUS As String = ""
    Public HEIGHT As String = ""
    Public TUBE As String = ""
    Public HAND As String = ""
    Public MOTOR_POSITION As String = ""
    Public MOTOR_OFFSET As String = ""
    Public MOTOR_TYPE As String = ""
    Public FLAT_BELT As String = ""
    Public O_RING_BELT As String = ""
    Public DESCRIPTION As String = ""
    Public ESPECIFICATION As String = ""
    Public INFEED_HEIGHT As String = ""
    Public OUTFEED_HEIGHT As String = ""
    Public ITEM_NUMBER As String = ""
    Public O_RING_PROTECTION As String = ""
    Public STANDARD_PART As String = ""
    Public CONVEYOR_BELT_TYPE As String = ""
    Public UNDERBELT As String = ""
    Public UNDER_CONVEYOR_PROTECTION As String = ""
    Public BELT_REFERENCE As String = ""
    Public NUM_MOTORS As String = ""
    Public SUPPORT_TYPE As String = ""
    Public REAL_LENGHT As String = ""
    Public CONVEYOR_HEIGHT_1 As String = ""
    Public CONVEYOR_HEIGHT_2 As String = ""
    Public CONVEYOR_HEIGHT_3 As String = ""
    Public CONVEYOR_HEIGHT_4 As String = ""
    Public ANGLE As String = ""
    Public FRAME As String = ""

    Public Sub New(fila As ce.IXLRow)
        'If cXML Is Nothing Then cXML = New ClosedXML2acad.ClosedXML2acad
        nFila = fila.RowNumber
        For Each oCell As ce.IXLCell In fila.Cells.AsParallel
            Dim cabecera As String = oCell.WorksheetColumn.FirstCell.Value.ToString
            Dim valor As String = oCell.Value
            Select Case cabecera
                Case "CODE" : Me.CODE = Convert.ToString(valor)
                Case "BLOCK" : Me.BLOCK = Convert.ToString(valor)
                Case "LENGTH" : Me.LENGTH = Convert.ToString(valor)
                Case "WIDTH" : Me.WIDTH = Convert.ToString(valor)
                Case "RADIUS" : Me.RADIUS = Convert.ToString(valor)
                Case "HEIGHT" : Me.HEIGHT = Convert.ToString(valor)
                Case "HAND" : Me.HAND = Convert.ToString(valor)
                Case "MOTOR_POSITION", "MOTOR POSITION" : Me.MOTOR_POSITION = Convert.ToString(valor)
                Case "MOTOR_OFFSET", "MOTOR OFFSET" : Me.MOTOR_OFFSET = Convert.ToString(valor)
                Case "MOTOR_TYPE", "MOTOR TYPE" : Me.MOTOR_TYPE = Convert.ToString(valor)
                Case "FLAT_BELT", "FLAT BELT" : Me.FLAT_BELT = Convert.ToString(valor)
                Case "O_RING_BELT", "O RING BELT" : Me.O_RING_BELT = Convert.ToString(valor)
                Case "DESCRIPTION" : Me.DESCRIPTION = Convert.ToString(valor)
                Case "ESPECIFICATION" : Me.ESPECIFICATION = Convert.ToString(valor)
                Case "INFEED_HEIGHT", "INFEED HEIGHT" : Me.INFEED_HEIGHT = Convert.ToString(valor)
                Case "OUTFEED_HEIGHT", "OUTFEED HEIGHT" : Me.OUTFEED_HEIGHT = Convert.ToString(valor)
                Case "ITEM_NUMBER", "ITEM NUMBER" : Me.ITEM_NUMBER = Convert.ToString(valor)
                Case "O_RING_PROTECTION", "O RING PROTECTION" : Me.O_RING_PROTECTION = Convert.ToString(valor)
                Case "STANDARD_PART", "STANDARD PART" : Me.STANDARD_PART = Convert.ToString(valor)
                Case "CONVEYOR_BELT_TYPE", "CONVEYOR BELT TYPE" : Me.CONVEYOR_BELT_TYPE = Convert.ToString(valor)
                Case "UNDERBELT" : Me.UNDERBELT = Convert.ToString(valor)
                Case "UNDER_CONVEYOR_PROTECTION", "UNDER CONVEYOR PROTECTION" : Me.UNDER_CONVEYOR_PROTECTION = Convert.ToString(valor)
                Case "BELT_REFERENCE", "BELT REFERENCE" : Me.BELT_REFERENCE = Convert.ToString(valor)
                Case "NUM_MOTORS", "NUM MOTORS" : Me.NUM_MOTORS = Convert.ToString(valor)
                Case "SUPPORT_TYPE", "SUPPORT TYPE" : Me.SUPPORT_TYPE = Convert.ToString(valor)
                Case "REAL_LENGHT", "REAL LENGHT" : Me.REAL_LENGHT = Convert.ToString(valor)
                Case "CONVEYOR_HEIGHT_1", "CONVEYOR HEIGHT 1" : Me.CONVEYOR_HEIGHT_1 = Convert.ToString(valor)
                Case "CONVEYOR_HEIGHT_2", "CONVEYOR HEIGHT 2" : Me.CONVEYOR_HEIGHT_2 = Convert.ToString(valor)
                Case "CONVEYOR_HEIGHT_3", "CONVEYOR HEIGHT 3" : Me.CONVEYOR_HEIGHT_3 = Convert.ToString(valor)
                Case "CONVEYOR_HEIGHT_4", "CONVEYOR HEIGHT 4" : Me.CONVEYOR_HEIGHT_4 = Convert.ToString(valor)
                Case "ANGLE" : Me.ANGLE = Convert.ToString(valor)
                Case "FRAME" : Me.FRAME = Convert.ToString(valor)
            End Select
        Next
    End Sub
End Class

Public Enum nombreColumnaPT
    BLOCK
    LENGTH
    WIDTH
    RADIUS
    HEIGHT
    TUBE
    HAND
    MOTOR_POSITION
    MOTOR_OFFSET
    MOTOR_TYPE
    FLAT_BELT
    O_RING_BELT
    DESCRIPTION
    ESPECIFICATION
    INFEED_HEIGHT
    OUTFEED_HEIGHT
    ITEM_NUMBER
    O_RING_PROTECTION
    STANDARD_PART
    CONVEYOR_BELT_TYPE
    UNDERBELT
    UNDER_CONVEYOR_PROTECTION
    BELT_REFERENCE
    NUM_MOTORS
    SUPPORT_TYPE
    REAL_LENGHT
    CONVEYOR_HEIGHT_1
    CONVEYOR_HEIGHT_2
    CONVEYOR_HEIGHT_3
    CONVEYOR_HEIGHT_4
    ANGLE
    FRAME
End Enum


Public Enum TipoPata
    PTR
    PTC
    PTRDH
End Enum

