Imports Autodesk.AutoCAD.Interop.Common
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.ApplicationServices
Imports TAVIL2020
Imports TAVIL2020.TAVIL2020
'Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports a2 = AutoCAD2acad.A2acad
Public Class UNIONES
    Public Shared LUniones As New List(Of UNION)                ' Todas las uniones
    Public Shared Sub UNION_Crea(handle As String)
        If LUniones Is Nothing Then LUniones = New List(Of UNION)
        If handle <> "" Then
            LUniones.Add(New UNION(handle))
        End If
    End Sub

    Public Shared Sub Report_UNIONES_TODO()
        If LUniones Is Nothing OrElse LUniones.Count = 0 Then ' OrElse NUniones Is Nothing Then
            MsgBox("No existen Uniones en este dibujo")
            Exit Sub
        End If
        '

        Dim columnas() As String = {"HANDLE",
            "INFEED_CONVEYOR", "INFEED_INCLINATION",
            "INFEED_CONVEYOR_L", "INFEED_INCLINATION_L",
            "INFEED_CONVEYOR_R", "INFEED_INCLINATION_R",
            "UNION", "UNITS",
            "OUTFEED_CONVEYOR", "OUTFEED_INCLINATION", "ANGLE",
            "OUTFEED_CONVEYOR_L", "OUTFEED_INCLINATION_L", "ANGLE_L",
            "OUTFEED_CONVEYOR_R", "OUTFEED_INCLINATION_R", "ANGLE_R",
            "LADO", "HOJA"}
        '
        Dim fiOut As String = IO.Path.ChangeExtension(Eventos.COMDoc.Path, "UNIONES.csv")
        If IO.File.Exists(fiOut) Then IO.File.Delete(fiOut)
        Dim texto As String = "UNIONES en " & Eventos.COMDoc().Name & ";;;;;;;;;;;;;;;;;;;" & vbCrLf &
            String.Join(";", columnas) & vbCrLf
        '
        Dim uni As IEnumerable(Of String) = From x In LUniones
                                            Select (
                      x.UnionHANDLE & ";" &
                      x.T1INFEED & ";" &
                      x.T1INCLINATION & ";" &
                      x.T1INFEEDL & ";" &
                      x.T1INCLINATIONL & ";" &
                      x.T1INFEEDR & ";" &
                      x.T1INCLINATIONR & ";" &
                      x.UNION.Replace(";", "|") & ";" &
                      x.UNITS.Replace(";", "|") & ";" &
                      x.T2OUTFEED & ";" &
                      x.T2INCLINATION & ";" &
                      x.ANGLE & ";" &
                      x.T2OUTFEEDL & ";" &
                      x.T2INCLINATIONL & ";" &
                      x.ANGLEL & ";" &
                      x.T2OUTFEEDR & ";" &
                      x.T2INCLINATIONR & ";" &
                      x.ANGLER & ";" &
                      x.LADO & ";" &
                      x.HOJA
                      )

        '
        texto &= String.Join(vbCrLf, uni.ToArray)
        IO.File.WriteAllText(fiOut, texto, System.Text.Encoding.UTF8)
        If IO.File.Exists(fiOut) Then Process.Start(fiOut)
    End Sub

    Public Shared Function Report_UNIONES_TOTALES(Optional imprimir As Boolean = True) As Dictionary(Of String, Integer)
        Dim DTotales As New Dictionary(Of String, Integer)
        '
        If LUniones Is Nothing OrElse LUniones.Count = 0 Then ' OrElse NUniones Is Nothing Then
            MsgBox("No existen Uniones en este dibujo")
            Return Nothing
        End If
        '
        ' Sacar cada código UNION y sus unidades de UNITS a DTotales
        For Each OUni As UNION In LUniones
            Dim PCodigo As String() = OUni.UNION.Split(";")
            Dim PCantidad As String() = OUni.UNITS.Split(";")
            If PCodigo.Count <> PCantidad.Count Then
                Debug.Print("Error cantidad")
                Continue For
            End If
            For x As Integer = 0 To PCodigo.Count - 1
                Dim codigo As String = PCodigo(x)
                Dim cantidad As Integer = 1
                If IsNumeric(PCantidad(x)) Then cantidad = Convert.ToInt32(PCantidad(x))
                If DTotales.ContainsKey(codigo) = True Then
                    DTotales(codigo) += cantidad
                Else
                    DTotales.Add(codigo, cantidad)
                End If
            Next
        Next
        '
        If imprimir = True Then
            Dim uni As IEnumerable(Of String) = From x In DTotales
                                                Select x.Key & ";" & x.Value
            '
            Dim columnas() As String = {"UNION", "UNITS"}
            Dim fiOut As String = IO.Path.ChangeExtension(Eventos.COMDoc.Path, "TOTALES.csv")
            If IO.File.Exists(fiOut) Then IO.File.Delete(fiOut)
            '
            Dim texto As String = "TOTAL UNIONES en " & Eventos.COMDoc().Name & ";" & vbCrLf &
                                    String.Join(";", columnas) & vbCrLf &
                                    String.Join(vbCrLf, uni.ToArray)
            '
            IO.File.WriteAllText(fiOut, texto, System.Text.Encoding.UTF8)
            If IO.File.Exists(fiOut) Then Process.Start(fiOut)
        End If
        '
        Return DTotales
    End Function
End Class

Public Class UNION
    Private _ANGLE As String = ""
    Private _ANGLEL As String = ""
    Private _ANGLER As String = ""
#Region "PROPERTIES"
    Public Property UnionHANDLE As String = ""
    Public Property ExcelFilaUnion As UNIONESFila = Nothing
    Public Property UNIONFin As String = ""
    Public Property LADO As String = "L"
    Public Property HOJA As String = "UNIONES"
    '
    Public Property T1HANDLE As String = ""
    Public Property T1INFEED As String = ""
    Public Property T1INCLINATION As String = "0"
    Public Property T1HANDLEL As String = ""
    Public Property T1INFEEDL As String = ""
    Public Property T1INCLINATIONL As String = "0"
    Public Property T1HANDLER As String = ""
    Public Property T1INFEEDR As String = ""
    Public Property T1INCLINATIONR As String = "0"

    Public Property UNION As String = ""
    Public Property UNITS As String = ""
    Public Property T2HANDLE As String = ""
    Public Property T2OUTFEED As String = ""
    Public Property T2INCLINATION As String = "0"
    Public Property T2HANDLEL As String = ""
    Public Property T2OUTFEEDL As String = ""
    Public Property T2INCLINATIONL As String = "0"
    Public Property T2HANDLER As String = ""
    Public Property T2OUTFEEDR As String = ""
    Public Property T2INCLINATIONR As String = "0"

    Public Property ANGLE As String
        Get
            If Me._ANGLE = "0" Then
                Return ""
            Else
                Return _ANGLE
            End If
        End Get
        Set(value As String)
            If value = "" Then value = "0"
            If value <> _ANGLE Then
                _ANGLE = value
            End If
        End Set
    End Property
    Public Property ANGLEL As String
        Get
            If Me._ANGLEL = "0" Then
                Return ""
            Else
                Return _ANGLEL
            End If
        End Get
        Set(value As String)
            If value = "" Then value = "0"
            If value <> _ANGLEL Then
                _ANGLEL = value
            End If
        End Set
    End Property
    Public Property ANGLER As String
        Get
            If Me._ANGLER = "0" Then
                Return ""
            Else
                Return _ANGLER
            End If
        End Get
        Set(value As String)
            If value = "" Then value = "0"
            If value <> _ANGLER Then
                _ANGLER = value
            End If
        End Set
    End Property

#End Region
    ''' <summary>
    ''' Editar uniones existentes o Nuevas inserciones masivas.
    ''' Solo Handle para sacar/poner el resto de los XData del AcadBlockReference
    ''' </summary>
    ''' <param name="handle"></param>
    Public Sub New(handle As String)
        Me.UnionHANDLE = handle
        Dim BlUnion As AcadBlockReference = UnionBlock()
        If BlUnion IsNot Nothing Then
            Me.T1HANDLE = clsA.XLeeDato(BlUnion.Handle, "T1HANDLE")
            Me.T1INFEED = clsA.XLeeDato(BlUnion.Handle, "T1INFEED")
            Me.T1INCLINATION = clsA.XLeeDato(BlUnion.Handle, "T1INCLINATION")
            Me.T1HANDLEL = clsA.XLeeDato(BlUnion.Handle, "T1HANDLEL")
            Me.T1INFEEDL = clsA.XLeeDato(BlUnion.Handle, "T1INFEEDL")
            Me.T1INCLINATIONL = clsA.XLeeDato(BlUnion.Handle, "T1INCLINATIONL")
            Me.T1HANDLER = clsA.XLeeDato(BlUnion.Handle, "T1HANDLER")
            Me.T1INFEEDR = clsA.XLeeDato(BlUnion.Handle, "T1INFEEDR")
            Me.T1INCLINATIONR = clsA.XLeeDato(BlUnion.Handle, "T1INCLINATIONR")
            Me.UNION = clsA.XLeeDato(BlUnion.Handle, "UNION").Replace(" ", "")
            Me.UNITS = clsA.XLeeDato(BlUnion.Handle, "UNITS").Replace(" ", "")
            Me.T2HANDLE = clsA.XLeeDato(BlUnion.Handle, "T2HANDLE")
            Me.T2OUTFEED = clsA.XLeeDato(BlUnion.Handle, "T2OUTFEED")
            Me.T2INCLINATION = clsA.XLeeDato(BlUnion.Handle, "T2INCLINATION")
            Me.ANGLE = clsA.XLeeDato(BlUnion.Handle, "ANGLE")
            Me.T2HANDLEL = clsA.XLeeDato(BlUnion.Handle, "T2HANDLEL")
            Me.T2OUTFEEDL = clsA.XLeeDato(BlUnion.Handle, "T2OUTFEEDL")
            Me.T2INCLINATIONL = clsA.XLeeDato(BlUnion.Handle, "T2INCLINATIONL")
            Me.ANGLEL = clsA.XLeeDato(BlUnion.Handle, "ANGLEL")
            Me.T2HANDLER = clsA.XLeeDato(BlUnion.Handle, "T2HANDLER")
            Me.T2OUTFEEDR = clsA.XLeeDato(BlUnion.Handle, "T2OUTFEEDR")
            Me.T2INCLINATIONR = clsA.XLeeDato(BlUnion.Handle, "T2INCLINATIONR")
            Me.ANGLER = clsA.XLeeDato(BlUnion.Handle, "ANGLER")
            Me.LADO = clsA.XLeeDato(BlUnion.Handle, "LADO")
        End If
        ' ¿Que HOJA?
        If Me.T2HANDLEL <> "" AndAlso Me.T2HANDLER <> "" AndAlso Me.T2OUTFEEDL <> "" AndAlso Me.T2OUTFEEDR <> "" Then
            Me.HOJA = "UNIONES_Y"
            'Me.T1HANDLE = T1Handle
            'Me.T1INFEED = T1Infeed
            'Me.T1INCLINATION = T1Inclination
            Me.T1HANDLEL = ""
            Me.T1INFEEDL = ""
            Me.T1INCLINATIONL = ""
            Me.T1HANDLER = ""
            Me.T1INFEEDR = ""
            Me.T1INCLINATIONR = ""
            Me.T2HANDLE = ""
            Me.T2OUTFEED = ""
            Me.T2INCLINATION = ""
            Me.ANGLE = ""
            Me.T2HANDLEL = T2HandleL
            Me.T2OUTFEEDL = T2OutfeedL
            Me.T2INCLINATIONL = T2InclinationL
            Me.ANGLEL = angleL
            Me.T2HANDLER = T2HandleR
            Me.T2OUTFEEDR = T2OutfeedR
            Me.T2INCLINATIONR = T2InclinationR
            Me.ANGLER = angleR
            Me.LADO = ""
        ElseIf Me.T1HANDLEL <> "" AndAlso Me.T1HANDLER <> "" AndAlso Me.T1INFEEDL <> "" AndAlso Me.T1INFEEDR <> "" Then
            Me.HOJA = "UNIONES_X"
            Me.T1HANDLE = ""
            Me.T1INFEED = ""
            Me.T1INCLINATION = ""
            'Me.T1HANDLEL = T1HandleL
            'Me.T1INFEEDL = T1InfeedL
            'Me.T1INCLINATIONL = T1InclinationL
            'Me.T1HANDLER = T1HandleR
            'Me.T1INFEEDR = T1InfeedR
            'Me.T1INCLINATIONR = T1InclinationR
            'Me.T2HANDLE = T2Handle
            'Me.T2OUTFEED = T2Outfeed
            'Me.T2INCLINATION = T2Inclination
            'Me.ANGLE = angle
            Me.T2HANDLEL = ""
            Me.T2OUTFEEDL = ""
            Me.T2INCLINATIONL = ""
            Me.ANGLEL = ""
            Me.T2HANDLER = ""
            Me.T2OUTFEEDR = ""
            Me.T2INCLINATIONR = ""
            Me.ANGLER = ""
            Me.LADO = ""
        Else
            Me.HOJA = "UNIONES"
            'Me.T1HANDLE = T1Handle
            'Me.T1INFEED = T1Infeed
            'Me.T1INCLINATION = T1Inclination
            Me.T1HANDLEL = ""
            Me.T1INFEEDL = ""
            Me.T1INCLINATIONL = ""
            Me.T1HANDLER = ""
            Me.T1INFEEDR = ""
            Me.T1INCLINATIONR = ""
            'Me.T2HANDLE = T2Handle
            'Me.T2OUTFEED = T2Outfeed
            'Me.T2INCLINATION = T2Inclination
            'Me.ANGLE = angle
            Me.T2HANDLEL = ""
            Me.T2OUTFEEDL = ""
            Me.T2INCLINATIONL = ""
            Me.ANGLEL = ""
            Me.T2HANDLER = ""
            Me.T2OUTFEEDR = ""
            Me.T2INCLINATIONR = ""
            Me.ANGLER = ""
            'Me.LADO = lado
        End If
        '
        ' Rellenar ExcelFilaUnion
        ExcelFilaUnion = cU.Fila_BuscaDame(
            Me.T1INFEED, Me.T1INCLINATION,
            Me.T1INFEEDL, Me.T1INCLINATIONL,
            Me.T1INFEEDR, Me.T1INCLINATIONR,
            Me.T2OUTFEED, Me.T2INCLINATION, Me.ANGLE,
            Me.T2OUTFEEDL, Me.T2INCLINATIONL, Me.ANGLEL,
            Me.T2OUTFEEDR, Me.T2INCLINATIONR, Me.ANGLER,
            Me.LADO)
        '
        If Me.UNION = "" AndAlso ExcelFilaUnion IsNot Nothing Then Me.UNION = ExcelFilaUnion.UNION
        If Me.UNITS = "" AndAlso ExcelFilaUnion IsNot Nothing Then Me.UNITS = ExcelFilaUnion.UNITS
    End Sub

    ''' <summary>
    ''' Nuevas uniones. Tenemos que pasar todos los parámetros.
    ''' </summary>
    ''' <param name="Uhandle"></param>
    ''' <param name="Union"></param>
    ''' <param name="Units"></param>
    ''' <param name="T1Handle"></param>
    ''' <param name="T1Infeed"></param>
    ''' <param name="T1Inclination"></param>
    ''' <param name="T1HandleL"></param>
    ''' <param name="T1InfeedL"></param>
    ''' <param name="T1InclinationL"></param>
    ''' <param name="T1HandleR"></param>
    ''' <param name="T1InfeedR"></param>
    ''' <param name="T1InclinationR"></param>
    ''' <param name="T2Handle"></param>
    ''' <param name="T2Outfeed"></param>
    ''' <param name="T2Inclination"></param>
    ''' <param name="angle"></param>
    ''' <param name="T2HandleL"></param>
    ''' <param name="T2OutfeedL"></param>
    ''' <param name="T2InclinationL"></param>
    ''' <param name="angleL"></param>
    ''' <param name="T2HandleR"></param>
    ''' <param name="T2OutfeedR"></param>
    ''' <param name="T2InclinationR"></param>
    ''' <param name="angleR"></param>
    ''' <param name="lado"></param>
    Public Sub New(Uhandle As String, Union As String, Units As String,
                   Optional T1Handle As String = "", Optional T1Infeed As String = "", Optional T1Inclination As String = "",
                   Optional T1HandleL As String = "", Optional T1InfeedL As String = "", Optional T1InclinationL As String = "",
                   Optional T1HandleR As String = "", Optional T1InfeedR As String = "", Optional T1InclinationR As String = "",
                   Optional T2Handle As String = "", Optional T2Outfeed As String = "", Optional T2Inclination As String = "", Optional angle As String = "",
                   Optional T2HandleL As String = "", Optional T2OutfeedL As String = "", Optional T2InclinationL As String = "", Optional angleL As String = "",
                   Optional T2HandleR As String = "", Optional T2OutfeedR As String = "", Optional T2InclinationR As String = "", Optional angleR As String = "",
                   Optional lado As String = "")
        '
        Me.UnionHANDLE = Uhandle
        Me.UNION = Union
        Me.UNITS = Units
        Me.T1HANDLE = T1Handle
        Me.T1INFEED = T1Infeed
        Me.T1INCLINATION = T1Inclination
        Me.T1HANDLEL = T1HandleL
        Me.T1INFEEDL = T1InfeedL
        Me.T1INCLINATIONL = T1InclinationL
        Me.T1HANDLER = T1HandleR
        Me.T1INFEEDR = T1InfeedR
        Me.T1INCLINATIONR = T1InclinationR
        Me.T2HANDLE = T2Handle
        Me.T2OUTFEED = T2Outfeed
        Me.T2INCLINATION = T2Inclination
        Me.ANGLE = angle
        Me.T2HANDLEL = T2HandleL
        Me.T2OUTFEEDL = T2OutfeedL
        Me.T2INCLINATIONL = T2InclinationL
        Me.ANGLEL = angleL
        Me.T2HANDLER = T2HandleR
        Me.T2OUTFEEDR = T2OutfeedR
        Me.T2INCLINATIONR = T2InclinationR
        Me.ANGLER = angleR
        Me.LADO = lado
        '
        ' ¿Que HOJA?
        If Me.T2HANDLEL <> "" AndAlso Me.T2HANDLER <> "" AndAlso Me.T2OUTFEEDL <> "" AndAlso Me.T2OUTFEEDR <> "" Then
            Me.HOJA = "UNIONES_Y"
            'Me.T1HANDLE = T1Handle
            'Me.T1INFEED = T1Infeed
            'Me.T1INCLINATION = T1Inclination
            Me.T1HANDLEL = ""
            Me.T1INFEEDL = ""
            Me.T1INCLINATIONL = ""
            Me.T1HANDLER = ""
            Me.T1INFEEDR = ""
            Me.T1INCLINATIONR = ""
            Me.T2HANDLE = ""
            Me.T2OUTFEED = ""
            Me.T2INCLINATION = ""
            Me.ANGLE = ""
            Me.T2HANDLEL = T2HandleL
            Me.T2OUTFEEDL = T2OutfeedL
            Me.T2INCLINATIONL = T2InclinationL
            Me.ANGLEL = angleL
            Me.T2HANDLER = T2HandleR
            Me.T2OUTFEEDR = T2OutfeedR
            Me.T2INCLINATIONR = T2InclinationR
            Me.ANGLER = angleR
            Me.LADO = ""
        ElseIf Me.T1HANDLEL <> "" AndAlso Me.T1HANDLER <> "" AndAlso Me.T1INFEEDL <> "" AndAlso Me.T1INFEEDR <> "" Then
            Me.HOJA = "UNIONES_X"
            Me.T1HANDLE = ""
            Me.T1INFEED = ""
            Me.T1INCLINATION = ""
            'Me.T1HANDLEL = T1HandleL
            'Me.T1INFEEDL = T1InfeedL
            'Me.T1INCLINATIONL = T1InclinationL
            'Me.T1HANDLER = T1HandleR
            'Me.T1INFEEDR = T1InfeedR
            'Me.T1INCLINATIONR = T1InclinationR
            'Me.T2HANDLE = T2Handle
            'Me.T2OUTFEED = T2Outfeed
            'Me.T2INCLINATION = T2Inclination
            'Me.ANGLE = angle
            Me.T2HANDLEL = ""
            Me.T2OUTFEEDL = ""
            Me.T2INCLINATIONL = ""
            Me.ANGLEL = ""
            Me.T2HANDLER = ""
            Me.T2OUTFEEDR = ""
            Me.T2INCLINATIONR = ""
            Me.ANGLER = ""
            Me.LADO = ""
        Else
            Me.HOJA = "UNIONES"
            'Me.T1HANDLE = T1Handle
            'Me.T1INFEED = T1Infeed
            'Me.T1INCLINATION = T1Inclination
            Me.T1HANDLEL = ""
            Me.T1INFEEDL = ""
            Me.T1INCLINATIONL = ""
            Me.T1HANDLER = ""
            Me.T1INFEEDR = ""
            Me.T1INCLINATIONR = ""
            'Me.T2HANDLE = T2Handle
            'Me.T2OUTFEED = T2Outfeed
            'Me.T2INCLINATION = T2Inclination
            'Me.ANGLE = angle
            Me.T2HANDLEL = ""
            Me.T2OUTFEEDL = ""
            Me.T2INCLINATIONL = ""
            Me.ANGLEL = ""
            Me.T2HANDLER = ""
            Me.T2OUTFEEDR = ""
            Me.T2INCLINATIONR = ""
            Me.ANGLER = ""
            'Me.LADO = lado
        End If
        ' Rellenar ExcelFilaUnion
        ExcelFilaUnion = cU.Fila_BuscaDame(
            Me.T1INFEED, Me.T1INCLINATION,
            Me.T1INFEEDL, Me.T1INCLINATIONL,
            Me.T1INFEEDR, Me.T1INCLINATIONR,
            Me.T2OUTFEED, Me.T2INCLINATION, Me.ANGLE,
            Me.T2OUTFEEDL, Me.T2INCLINATIONL, Me.ANGLEL,
            Me.T2OUTFEEDR, Me.T2INCLINATIONR, Me.ANGLER,
            Me.LADO)
        '
        If Me.UNION = "" AndAlso ExcelFilaUnion IsNot Nothing Then Me.UNION = ExcelFilaUnion.UNION
        If Me.UNITS = "" AndAlso ExcelFilaUnion IsNot Nothing Then Me.UNITS = ExcelFilaUnion.UNITS
    End Sub
    '
    Public Sub PonAtributos()
        If UnionBlock() IsNot Nothing Then
            Try
                Dim dicNomVal As New Dictionary(Of String, String)
                dicNomVal.Add("UNION", Me.UNIONFin)
                dicNomVal.Add("UNITS", Me.UNITS)
                clsA.Bloque_AtributoEscribe(UnionBlock.ObjectID, dicNomVal)
                clsA.XPonDato(Me.UnionHANDLE, "UNION", Me.UNIONFin)
                clsA.XPonDato(Me.UnionHANDLE, "UNITS", Me.UNITS)
                '
                clsA.XPonDato(Me.UnionHANDLE, "T1HANDLE", Me.T1HANDLE)
                clsA.XPonDato(Me.UnionHANDLE, "T1INFEED", Me.T1INFEED)
                clsA.XPonDato(Me.UnionHANDLE, "T1INCLINATION", Me.T1INCLINATION)
                clsA.XPonDato(Me.UnionHANDLE, "T1HANDLEL", Me.T1HANDLEL)
                clsA.XPonDato(Me.UnionHANDLE, "T1INFEEDL", Me.T1INFEEDL)
                clsA.XPonDato(Me.UnionHANDLE, "T1INCLINATIONL", Me.T1INCLINATIONL)
                clsA.XPonDato(Me.UnionHANDLE, "T1HANDLER", Me.T1HANDLER)
                clsA.XPonDato(Me.UnionHANDLE, "T1INFEEDR", Me.T1INFEEDR)
                clsA.XPonDato(Me.UnionHANDLE, "T1INCLINATIONR", Me.T1INCLINATIONR)
                '
                clsA.XPonDato(Me.UnionHANDLE, "T2HANDLE", Me.T2HANDLE)
                clsA.XPonDato(Me.UnionHANDLE, "T2OUTFEED", Me.T2OUTFEED)
                clsA.XPonDato(Me.UnionHANDLE, "T2INCLINATION", Me.T2INCLINATION)
                clsA.XPonDato(Me.UnionHANDLE, "ANGLE", Me.ANGLE)
                clsA.XPonDato(Me.UnionHANDLE, "T2HANDLEL", Me.T2HANDLEL)
                clsA.XPonDato(Me.UnionHANDLE, "T2OUTFEEDL", Me.T2OUTFEEDL)
                clsA.XPonDato(Me.UnionHANDLE, "T2INCLINATIONL", Me.T2INCLINATIONL)
                clsA.XPonDato(Me.UnionHANDLE, "ANGLEL", Me.ANGLEL)
                clsA.XPonDato(Me.UnionHANDLE, "T2HANDLER", Me.T2HANDLER)
                clsA.XPonDato(Me.UnionHANDLE, "T2OUTFEEDR", Me.T2OUTFEEDR)
                clsA.XPonDato(Me.UnionHANDLE, "T2INCLINATIONR", Me.T2INCLINATIONR)
                clsA.XPonDato(Me.UnionHANDLE, "ANGLER", Me.ANGLER)
                '
                clsA.XPonDato(Me.UnionHANDLE, "LADO", Me.LADO)
            Catch ex As Exception
                Exit Sub
            End Try
        End If
    End Sub
    Public Sub UNIONFin_Pon(oDg As DataGridView)
        UNIONFin = ""
        Dim lU As New List(Of String)
        For x As Integer = 0 To oDg.Rows.Count - 1
            Dim value As String = oDg.Rows.Item(x).Cells.Item("UNION").Value.ToString.Trim
            lU.Add(value)
        Next
        UNIONFin = String.Join(";", lU.ToArray)
        PonAtributos()
    End Sub
    Public Sub UNION_PonValue(ByRef oDg As DataGridView)
        Dim partes() As String = UNION.Split(";"c)
        For x As Integer = 0 To oDg.Rows.Count - 1
            If TypeOf oDg.Rows.Item(x).Cells.Item("UNION") Is DataGridViewComboBoxCell Then
                Dim dgC As DataGridViewComboBoxCell = oDg.Rows.Item(x).Cells.Item("UNION")
                Try
                    If dgC.Items.Contains(partes(x)) Then
                        dgC.Value = partes(x)
                        oDg.Refresh()
                        Exit For
                    End If
                Catch ex As Exception
                    Exit Sub
                End Try
            End If
        Next
    End Sub
#Region "AcadBlockReferences"
    Public Function UnionBlock() As AcadBlockReference
        If Me.UnionHANDLE <> "" Then
            Try
                Return Eventos.COMDoc().HandleToObject(Me.UnionHANDLE)
            Catch ex As Exception
                Return Nothing
            End Try
        Else
            Return Nothing
        End If
    End Function
    Public Function T1Block() As AcadBlockReference
        If Me.T1HANDLE <> "" Then
            Return Eventos.COMDoc().HandleToObject(Me.T1HANDLE)
        Else
            Return Nothing
        End If
    End Function
    Public Function T1BlockL() As AcadBlockReference
        If Me.T1HANDLEL <> "" Then
            Return Eventos.COMDoc().HandleToObject(Me.T1HANDLEL)
        Else
            Return Nothing
        End If
    End Function
    Public Function T1BlockR() As AcadBlockReference
        If Me.T1HANDLER <> "" Then
            Return Eventos.COMDoc().HandleToObject(Me.T1HANDLER)
        Else
            Return Nothing
        End If
    End Function
    Public Function T2Block() As AcadBlockReference
        If Me.T2HANDLE <> "" Then
            Return Eventos.COMDoc().HandleToObject(Me.T2HANDLE)
        Else
            Return Nothing
        End If
    End Function
    Public Function T2BlockL() As AcadBlockReference
        If Me.T2HANDLEL <> "" Then
            Return Eventos.COMDoc().HandleToObject(Me.T2HANDLEL)
        Else
            Return Nothing
        End If
    End Function
    Public Function T2BlockR() As AcadBlockReference
        If Me.T2HANDLER <> "" Then
            Return Eventos.COMDoc().HandleToObject(Me.T2HANDLER)
        Else
            Return Nothing
        End If
    End Function
#End Region
End Class
