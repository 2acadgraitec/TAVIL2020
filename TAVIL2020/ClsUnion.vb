Imports Autodesk.AutoCAD.Interop.Common
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.ApplicationServices
Imports TAVIL2020.TAVIL2020
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports a2 = AutoCAD2acad.A2acad
Imports TAVIL2020

'Public Class ClsUnion
'    Private _UNION As String = ""
'    Private _UNITS As String = ""
'    Private _ANGLE As String = ""
'    Private _ANGLEL As String = ""
'    Private _ANGLER As String = ""
'#Region "PROPERTIES"
'    Public Property T1HANDLE As String = ""
'    Public Property T1INFEED As String = ""
'    Public Property T1INCLINATION As String = "0"
'    Public Property T1HANDLEL As String = ""
'    Public Property T1INFEEDL As String = ""
'    Public Property T1INCLINATIONL As String = "0"
'    Public Property T1HANDLER As String = ""
'    Public Property T1INFEEDR As String = ""
'    Public Property T1INCLINATIONR As String = "0"
'    Public Property T2HANDLE As String = ""
'    Public Property T2OUTFEED As String = ""
'    Public Property T2INCLINATION As String = "0"
'    Public Property T2HANDLEL As String = ""
'    Public Property T2OUTFEEDL As String = ""
'    Public Property T2INCLINATIONL As String = "0"
'    Public Property T2HANDLER As String = ""
'    Public Property T2OUTFEEDR As String = ""
'    Public Property T2INCLINATIONR As String = "0"
'    Public Property LADO As String = "L"
'    Public Property HOJA As String = "UNIONES"

'    Public Property UnionHANDLE As String = ""
'    Public Property UnionBlock As AcadBlockReference = Nothing
'    Public Property T1Block As AcadBlockReference = Nothing
'    Public Property T1BlockL As AcadBlockReference = Nothing
'    Public Property T1BlockR As AcadBlockReference = Nothing
'    Public Property T2Block As AcadBlockReference = Nothing
'    Public Property T2BlockL As AcadBlockReference = Nothing
'    Public Property T2BlockR As AcadBlockReference = Nothing
'    Public Property ExcelFilaUnion As UNIONESFila = Nothing
'    Public Property UNIONFin As String = ""

'    Public Property UNION As String
'        Get
'            Return _UNION
'        End Get
'        Set(value As String)
'            value = value.Replace(" ", "").Trim
'            If value <> _UNION Then
'                _UNION = value
'                'clsA.XPonDato(Me.HANDLE, "UNION", value)
'            End If
'        End Set
'    End Property

'    Public Property UNITS As String
'        Get
'            Return _UNITS
'        End Get
'        Set(value As String)
'            value = value.Replace(" ", "")
'            If value <> _UNITS Then
'                _UNITS = value
'                'clsA.XPonDato(Me.HANDLE, "UNITS", value)
'            End If
'        End Set
'    End Property

'    Public Property ANGLE As String
'        Get
'            If Me._ANGLE = "0" Then
'                Return ""
'            Else
'                Return _ANGLE
'            End If
'        End Get
'        Set(value As String)
'            If value = "" Then value = "0"
'            If value <> _ANGLE Then
'                _ANGLE = value
'            End If
'        End Set
'    End Property
'    Public Property ANGLEL As String
'        Get
'            If Me._ANGLEL = "0" Then
'                Return ""
'            Else
'                Return _ANGLEL
'            End If
'        End Get
'        Set(value As String)
'            If value = "" Then value = "0"
'            If value <> _ANGLEL Then
'                _ANGLEL = value
'            End If
'        End Set
'    End Property
'    Public Property ANGLER As String
'        Get
'            If Me._ANGLER = "0" Then
'                Return ""
'            Else
'                Return _ANGLER
'            End If
'        End Get
'        Set(value As String)
'            If value = "" Then value = "0"
'            If value <> _ANGLER Then
'                _ANGLER = value
'            End If
'        End Set
'    End Property

'#End Region
'    ''' <summary>
'    ''' Editar uniones existentes o Nuevas inserciones masivas.
'    ''' Solo Handle para sacar/poner el resto de los XData del AcadBlockReference
'    ''' </summary>
'    ''' <param name="handle"></param>
'    Public Sub New(handle As String)
'        Me.UnionHANDLE = handle
'        'PonDatosX()
'        Try
'            UnionBlock = Eventos.COMDoc().HandleToObject(Me.UnionHANDLE)
'        Catch ex As Exception
'            Exit Sub
'        End Try
'        '
'        Me.UNION = clsA.XLeeDato(UnionBlock.Handle, "UNION")
'        Me.UNITS = clsA.XLeeDato(UnionBlock.Handle, "UNITS")
'        Me.T1HANDLE = clsA.XLeeDato(UnionBlock.Handle, "T1HANDLE")
'        Me.T1INFEED = clsA.XLeeDato(UnionBlock.Handle, "T1INFEED")
'        Me.T1INCLINATION = clsA.XLeeDato(UnionBlock.Handle, "T1INCLINATION")
'        Me.T1HANDLEL = clsA.XLeeDato(UnionBlock.Handle, "T1LHANDLE")
'        Me.T1INFEEDL = clsA.XLeeDato(UnionBlock.Handle, "T1LINFEED")
'        Me.T1INCLINATIONL = clsA.XLeeDato(UnionBlock.Handle, "T1LINCLINATION")
'        Me.T1HANDLER = clsA.XLeeDato(UnionBlock.Handle, "T1RHANDLE")
'        Me.T1INFEEDR = clsA.XLeeDato(UnionBlock.Handle, "T1RINFEED")
'        Me.T1INCLINATIONR = clsA.XLeeDato(UnionBlock.Handle, "T1RINCLINATION")
'        Me.T2HANDLE = clsA.XLeeDato(UnionBlock.Handle, "T2HANDLE")
'        Me.T2OUTFEED = clsA.XLeeDato(UnionBlock.Handle, "T2OUTFEED")
'        Me.T2INCLINATION = clsA.XLeeDato(UnionBlock.Handle, "T2INCLINATION")
'        Me.T2HANDLEL = clsA.XLeeDato(UnionBlock.Handle, "T2LHANDLE")
'        Me.T2OUTFEEDL = clsA.XLeeDato(UnionBlock.Handle, "T2LOUTFEED")
'        Me.T2INCLINATIONL = clsA.XLeeDato(UnionBlock.Handle, "T2LINCLINATION")
'        Me.T2HANDLER = clsA.XLeeDato(UnionBlock.Handle, "T2RHANDLE")
'        Me.T2OUTFEEDR = clsA.XLeeDato(UnionBlock.Handle, "T2ROUTFEED")
'        Me.T2INCLINATIONR = clsA.XLeeDato(UnionBlock.Handle, "T2RINCLINATION")
'        Me.ANGLE = clsA.XLeeDato(UnionBlock.Handle, "ANGLE")
'        Me.ANGLEL = clsA.XLeeDato(UnionBlock.Handle, "ANGLEL")
'        Me.ANGLER = clsA.XLeeDato(UnionBlock.Handle, "ANGLER")
'        Me.LADO = clsA.XLeeDato(UnionBlock.Handle, "LADO")
'        ' ¿Que HOJA?
'        If Me.T2HANDLEL <> "" AndAlso Me.T2HANDLER <> "" AndAlso Me.T2OUTFEEDL <> "" AndAlso Me.T2OUTFEEDR <> "" Then
'            Me.HOJA = "UNIONES_Y"
'        ElseIf Me.T1HANDLEL <> "" AndAlso Me.T1HANDLER <> "" AndAlso Me.T1INFEEDL <> "" AndAlso Me.T1INFEEDR <> "" Then
'            Me.HOJA = "UNIONES_X"
'        End If
'        ' AcadBlockReferences
'        Try
'            If Me._T1HANDLE <> "" AndAlso IsNumeric(Me._T1HANDLE) Then T1Block = Eventos.COMDoc().HandleToObject(Me._T1HANDLE)
'            If Me._T1HANDLEL <> "" AndAlso IsNumeric(Me._T1HANDLEL) Then T1BlockL = Eventos.COMDoc().HandleToObject(Me._T1HANDLEL)
'            If Me._T1HANDLER <> "" AndAlso IsNumeric(Me._T1HANDLER) Then T1BlockR = Eventos.COMDoc().HandleToObject(Me._T1HANDLER)
'            If Me._T2HANDLE <> "" AndAlso IsNumeric(Me._T2HANDLE) Then T2Block = Eventos.COMDoc().HandleToObject(Me._T2HANDLE)
'            If Me._T2HANDLEL <> "" AndAlso IsNumeric(Me._T2HANDLEL) Then T2BlockL = Eventos.COMDoc().HandleToObject(Me._T2HANDLEL)
'            If Me._T2HANDLER <> "" AndAlso IsNumeric(Me._T2HANDLER) Then T2BlockR = Eventos.COMDoc().HandleToObject(Me._T2HANDLER)
'        Catch ex As Exception
'        End Try
'        '
'        ' Rellenar ExcelFilaUnion
'        ExcelFilaUnion = cU.Fila_BuscaDame(
'            Me.T1INFEED, Me.T1INCLINATION,
'            Me.T1INFEEDL, Me.T1INCLINATIONL,
'            Me.T1INFEEDR, Me.T1INCLINATIONR,
'            Me.T2OUTFEED, Me.T2INCLINATION, Me.ANGLE,
'            Me.T2OUTFEEDL, Me.T2INCLINATIONL, Me.ANGLEL,
'            Me.T2OUTFEEDR, Me.T2INCLINATIONR, Me.ANGLER)
'        '
'        If Me.UNION = "" AndAlso ExcelFilaUnion IsNot Nothing Then Me.UNION = ExcelFilaUnion.UNION
'        If Me.UNITS = "" AndAlso ExcelFilaUnion IsNot Nothing Then Me.UNITS = ExcelFilaUnion.UNITS
'    End Sub

'    ''' <summary>
'    ''' Nuevas uniones. Tenemos que pasar todos los parámetros.
'    ''' </summary>
'    ''' <param name="handle"></param>
'    ''' <param name="Union"></param>
'    ''' <param name="Units"></param>
'    ''' <param name="T1Handle"></param>
'    ''' <param name="T1Infeed"></param>
'    ''' <param name="T1Inclination"></param>
'    ''' <param name="T1HandleL"></param>
'    ''' <param name="T1InfeedL"></param>
'    ''' <param name="T1InclinationL"></param>
'    ''' <param name="T1HandleR"></param>
'    ''' <param name="T1InfeedR"></param>
'    ''' <param name="T1InclinationR"></param>
'    ''' <param name="T2Handle"></param>
'    ''' <param name="T2Outfeed"></param>
'    ''' <param name="T2Inclination"></param>
'    ''' <param name="angle"></param>
'    ''' <param name="T2HandleL"></param>
'    ''' <param name="T2OutfeedL"></param>
'    ''' <param name="T2InclinationL"></param>
'    ''' <param name="angleL"></param>
'    ''' <param name="T2HandleR"></param>
'    ''' <param name="T2OutfeedR"></param>
'    ''' <param name="T2InclinationR"></param>
'    ''' <param name="angleR"></param>
'    Public Sub New(handle As String, Union As String, Units As String,
'                   T1Handle As String, T1Infeed As String, T1Inclination As String,
'                   T1HandleL As String, T1InfeedL As String, T1InclinationL As String,
'                   T1HandleR As String, T1InfeedR As String, T1InclinationR As String,
'                   T2Handle As String, T2Outfeed As String, T2Inclination As String, angle As String,
'                   T2HandleL As String, T2OutfeedL As String, T2InclinationL As String, angleL As String,
'                   T2HandleR As String, T2OutfeedR As String, T2InclinationR As String, angleR As String,
'                   lado As String)
'        Me.UnionHANDLE = handle
'        Try
'            If handle <> "" AndAlso IsNumeric(handle) Then UnionBlock = Eventos.COMDoc().HandleToObject(Me.UnionHANDLE)
'        Catch ex As Exception
'            Exit Sub
'        End Try
'        '
'        Me.UNION = Union
'        Me.UNITS = Units
'        Me.T1HANDLE = T1Handle
'        Me.T1INFEED = T1Infeed
'        Me.T1INCLINATION = T1Inclination
'        Me.T1HANDLEL = T1HandleL
'        Me.T1INFEEDL = T1InfeedL
'        Me.T1INCLINATIONL = T1InclinationL
'        Me.T1HANDLER = T1HandleR
'        Me.T1INFEEDR = T1InfeedR
'        Me.T1INCLINATIONR = T1InclinationR
'        Me.T2HANDLE = T2Handle
'        Me.T2OUTFEED = T2Outfeed
'        Me.T2INCLINATION = T2Inclination
'        Me.ANGLE = angle
'        Me.T2HANDLEL = T2HandleL
'        Me.T2OUTFEEDL = T2OutfeedL
'        Me.T2INCLINATIONL = T2InclinationL
'        Me.ANGLEL = angleL
'        Me.T2HANDLER = T2HandleR
'        Me.T2OUTFEEDR = T2OutfeedR
'        Me.T2INCLINATIONR = T2InclinationR
'        Me.ANGLER = angleR
'        Me.LADO = lado
'        ' AcadBlockReferences
'        Try
'            If Me._T1HANDLE <> "" AndAlso IsNumeric(Me._T1HANDLE) Then T1Block = Eventos.COMDoc().HandleToObject(Me._T1HANDLE)
'            If Me._T1HANDLEL <> "" AndAlso IsNumeric(Me._T1HANDLEL) Then T1BlockL = Eventos.COMDoc().HandleToObject(Me._T1HANDLEL)
'            If Me._T1HANDLER <> "" AndAlso IsNumeric(Me._T1HANDLER) Then T1BlockR = Eventos.COMDoc().HandleToObject(Me._T1HANDLER)
'            If Me._T2HANDLE <> "" AndAlso IsNumeric(Me._T2HANDLE) Then T2Block = Eventos.COMDoc().HandleToObject(Me._T2HANDLE)
'            If Me._T2HANDLEL <> "" AndAlso IsNumeric(Me._T2HANDLEL) Then T2BlockL = Eventos.COMDoc().HandleToObject(Me._T2HANDLEL)
'            If Me._T2HANDLER <> "" AndAlso IsNumeric(Me._T2HANDLER) Then T2BlockR = Eventos.COMDoc().HandleToObject(Me._T2HANDLER)
'        Catch ex As Exception
'        End Try

'        '
'        ' Rellenar ExcelFilaUnion
'        ExcelFilaUnion = cU.Fila_BuscaDame(
'            Me.T1INFEED, Me.T1INCLINATION,
'            Me.T1INFEEDL, Me.T1INCLINATIONL,
'            Me.T1INFEEDR, Me.T1INCLINATIONR,
'            Me.T2OUTFEED, Me.T2INCLINATION, Me.ANGLE,
'            Me.T2OUTFEEDL, Me.T2INCLINATIONL, Me.ANGLEL,
'            Me.T2OUTFEEDR, Me.T2INCLINATIONR, Me.ANGLER)
'        '
'        If Me.UNION = "" AndAlso ExcelFilaUnion IsNot Nothing Then Me.UNION = ExcelFilaUnion.UNION
'        If Me.UNITS = "" AndAlso ExcelFilaUnion IsNot Nothing Then Me.UNITS = ExcelFilaUnion.UNITS
'    End Sub

'    'Public Function OUnionDame() As AcadBlockReference
'    '    If clsA Is Nothing Then clsA = New a2.A2acad(Eventos.COMApp, cfg._appFullPath, regAPPCliente)
'    '    Return Eventos.COMDoc().HandleToObject(Me.HANDLE)
'    'End Function
'    Public Sub PonAtributos()
'        If UnionBlock IsNot Nothing Then
'            Try
'                Dim dicNomVal As New Dictionary(Of String, String)
'                dicNomVal.Add("UNION", Me.UNIONFin)
'                dicNomVal.Add("UNITS", Me.UNITS)
'                clsA.Bloque_AtributoEscribe(UnionBlock.ObjectID, dicNomVal)
'                clsA.XPonDato(Me.UnionHANDLE, "UNION", Me.UNIONFin)
'                clsA.XPonDato(Me.UnionHANDLE, "UNITS", Me.UNITS)
'                clsA.XPonDato(Me.UnionHANDLE, "T1HANDLE", Me.T1HANDLE)
'                clsA.XPonDato(Me.UnionHANDLE, "T1INFEED", Me.T1INFEED)
'                clsA.XPonDato(Me.UnionHANDLE, "T1INCLINATION", Me.T1INCLINATION)
'                clsA.XPonDato(Me.UnionHANDLE, "T2HANDLE", Me.T2HANDLE)
'                clsA.XPonDato(Me.UnionHANDLE, "T2OUTFEED", Me.T2OUTFEED)
'                clsA.XPonDato(Me.UnionHANDLE, "T2INCLINATION", Me.T2INCLINATION)
'                clsA.XPonDato(Me.UnionHANDLE, "ROTATION", Me.ANGLE)
'            Catch ex As Exception
'                Exit Sub
'            End Try
'        End If
'    End Sub
'    Public Sub UNIONFin_Pon(oDg As DataGridView)
'        UNIONFin = ""
'        Dim lU As New List(Of String)
'        For x As Integer = 0 To oDg.Rows.Count - 1
'            Dim value As String = oDg.Rows.Item(x).Cells.Item("UNION").Value.ToString.Trim
'            lU.Add(value)
'        Next
'        UNIONFin = String.Join(";", lU.ToArray)
'        PonAtributos()
'    End Sub
'    Public Sub UNION_PonValue(ByRef oDg As DataGridView)
'        Dim partes() As String = UNION.Split(";"c)
'        For x As Integer = 0 To oDg.Rows.Count - 1
'            If TypeOf oDg.Rows.Item(x).Cells.Item("UNION") Is DataGridViewComboBoxCell Then
'                Dim dgC As DataGridViewComboBoxCell = oDg.Rows.Item(x).Cells.Item("UNION")
'                Try
'                    If dgC.Items.Contains(partes(x)) Then
'                        dgC.Value = partes(x)
'                        oDg.Refresh()
'                        Exit For
'                    End If
'                Catch ex As Exception
'                    Exit Sub
'                End Try
'            End If
'        Next
'    End Sub
'End Class

