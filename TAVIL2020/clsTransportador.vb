Imports Autodesk.AutoCAD.Interop.Common
Imports System.Windows.Forms
Imports System.Linq
Public Class clsTransportador
    Public _NAME As String        ' Name
    Public _EFFECTIVENAME As String        ' EffectiveName
    Public _PREFIJO As String = ""               ' De PTR400_FVIEW el sufijo será PTR400
    Public _SUFIJO As String = ""                ' De PTR400_FVIEW el sufijo será FVIEW
    Public _HANDLE As String        ' Handle
    Public _ID As Long           ' ObjectID = Long_PTR
    Public _IDPtr As IntPtr         ' _ID convertido a IntPtr
    '
    Public _ITEM_NUMBER As String = ""
    Public _CODE As String = ""
    Public _DIRECTRIZ As String = ""
    Public _DIRECTRIZ1 As String = ""
    Public _WIDTH As String = ""
    Public _RADIUS As String = ""
    Public _HEIGHT As String = ""
    Public _STANDARD_PART As String = "SI"
    Public _clave As String
    '
    ' Coleciones
    Public _WIDTHs As List(Of String)
    Public _RADIUSs As List(Of String)
    Public _HEIGHTs As List(Of String)

    ' Array de valores, según tipo de pata


    '
    'Public lAttRef As Dictionary(Of String, AcadAttributeReference)
    'Public lAttConst As Dictionary(Of String, AcadAttribute)
    'Public lDPro As Dictionary(Of String, AcadDynamicBlockReferenceProperty)
    '
    Public tieneXXX As Boolean = False
    Public encurva As Boolean = False
    Public esPlanta As Boolean = False
    Public widthEsAtt As Boolean = True

    Public Sub New(oBl As AcadBlockReference, soloXXX As Boolean)
        '
        Me._NAME = oBl.Name
        Me._EFFECTIVENAME = oBl.EffectiveName
        Dim partes() As String = Me._EFFECTIVENAME.Split("_")
        If partes IsNot Nothing AndAlso partes.Count = 2 Then
            Me._PREFIJO = partes(0)
            Me._SUFIJO = partes(1)
        ElseIf partes IsNot Nothing AndAlso partes.Count = 1 Then
            Me._PREFIJO = partes(0)
            Me._SUFIJO = ""
        End If
        encurva = cPT.PTCXXX.Contains(Me._PREFIJO)
        Me.esPlanta = patasSIPlanta.Contains(Me._SUFIJO)
        '
        Me._HANDLE = oBl.Handle
        Me._ID = oBl.ObjectID
        Me._IDPtr = New IntPtr(oBl.ObjectID)
        '
        _CODE = clsA.Bloque_AtributoDame(oBl.ObjectID, "CODE")   ' CODE
        tieneXXX = _CODE.ToUpper.Contains("XX")
        '
        If tieneXXX = False Then
            _DIRECTRIZ = clsA.Bloque_AtributoDame(oBl.ObjectID, "DIRECTRIZ") ' DIRECTRIZ
            If _DIRECTRIZ = "" Then _DIRECTRIZ = "XX"
            tieneXXX = _DIRECTRIZ.ToUpper.Contains("XX")
        End If
        '
        If tieneXXX = False Then
            _DIRECTRIZ1 = clsA.Bloque_AtributoDame(oBl.ObjectID, "DIRECTRIZ1") ' DIRECTRIZ1
            If _DIRECTRIZ1 = "" Then _DIRECTRIZ1 = "XX"
            tieneXXX = _DIRECTRIZ1.ToUpper.Contains("XX")
        End If
        '
        If Me._CODE = "" Or Me._CODE.ToUpper.StartsWith("XX") Then
            Me._CODE = Me._PREFIJO & "XX"
        End If
    End Sub

    Public Sub New(oBl As AcadBlockReference)
        _WIDTHs = New List(Of String)
        _RADIUSs = New List(Of String)
        _HEIGHTs = New List(Of String)
        '
        Me._NAME = oBl.Name
        Me._EFFECTIVENAME = oBl.EffectiveName
        Dim partes() As String = Me._EFFECTIVENAME.Split("_")
        If partes IsNot Nothing AndAlso partes.Count = 2 Then
            Me._PREFIJO = partes(0)
            Me._SUFIJO = partes(1)
        ElseIf partes IsNot Nothing AndAlso partes.Count = 1 Then
            Me._PREFIJO = partes(0)
            Me._SUFIJO = ""
        End If
        Me.encurva = cPT.PTCXXX.Contains(Me._PREFIJO)
        Me.esPlanta = patasSIPlanta.Contains(Me._SUFIJO)
        '
        Me._HANDLE = oBl.Handle
        Me._ID = oBl.ObjectID
        Me._IDPtr = New IntPtr(oBl.ObjectID)
        '
        _CODE = clsA.Bloque_AtributoDame(oBl.ObjectID, "CODE")   ' CODE
        tieneXXX = _CODE.ToUpper.Contains("XX")
        '
        If tieneXXX = False Then
            _DIRECTRIZ = clsA.Bloque_AtributoDame(oBl.ObjectID, "DIRECTRIZ") ' DIRECTRIZ
            If _DIRECTRIZ = "" Then _DIRECTRIZ = "XX"
            tieneXXX = _DIRECTRIZ.ToUpper.Contains("XX")
        End If
        '
        If tieneXXX = False Then
            _DIRECTRIZ1 = clsA.Bloque_AtributoDame(oBl.ObjectID, "DIRECTRIZ1") ' DIRECTRIZ1
            If _DIRECTRIZ1 = "" Then _DIRECTRIZ1 = "XX"
            tieneXXX = _DIRECTRIZ1.ToUpper.Contains("XX")
        End If
        '
        _STANDARD_PART = clsA.Bloque_AtributoDame(oBl.ObjectID, "STANDARD_PART") ' STANDARD_PART
        If _STANDARD_PART <> "" Then _STANDARD_PART = _STANDARD_PART.ToUpper
        '
        '_WIDTH = clsA.Bloque_AtributoDame(oBl.ObjectID, "WIDTH")  ' WIDTH
        'If _WIDTH = "" Then _WIDTH = clsA.BloqueDinamico_ParametroDame(oBl.ObjectID, "WIDTH")
        '
        ' Poner las propiedades, según el tipo
        If cPT.PTRXXX.Contains(Me._PREFIJO) Then
            widthEsAtt = False
            _WIDTH = clsA.BloqueDinamico_ParametroDame(oBl.ObjectID, "WIDTH")   ' WIDTH
            _RADIUS = ""    ' RADIUS
            _HEIGHT = clsA.Bloque_AtributoDame(oBl.ObjectID, "HEIGHT") ' HEIGHT
            If _HEIGHT = "" Then _HEIGHT = clsA.BloqueDinamico_ParametroDame(oBl.ObjectID, "HEIGHT")   ' WIDTH
            _WIDTHs = cPT.Filas_DameWIDTHs(cPT.PTRXXX)
            _RADIUSs = Nothing
            _HEIGHTs = cPT.Filas_DameHEIGHTs(Me._PREFIJO)
        ElseIf cPT.PTCXXX.Contains(Me._PREFIJO) Then
            _WIDTH = ""   ' WIDTH
            _HEIGHT = clsA.Bloque_AtributoDame(oBl.ObjectID, "HEIGHT") ' HEIGHT
            If _HEIGHT = "" OrElse Me._HEIGHT.Contains("XX") Then _HEIGHT = clsA.BloqueDinamico_ParametroDame(oBl.ObjectID, "HEIGHT")   ' WIDTH
            _WIDTHs = Nothing
            _RADIUSs = cPT.Filas_DameRADIUSs({Me._PREFIJO})
            _HEIGHTs = cPT.Filas_DameHEIGHTs(Me._PREFIJO)
            '
            _RADIUS = _RADIUSs.FirstOrDefault
        ElseIf cPT.PTRXXXDH.Contains(Me._PREFIJO) Then
            _RADIUS = ""   ' RADIUS
            _HEIGHT = clsA.Bloque_AtributoDame(oBl.ObjectID, "HEIGHT") ' HEIGHT
            If _HEIGHT = "" Then _HEIGHT = clsA.BloqueDinamico_ParametroDame(oBl.ObjectID, "HEIGHT")   ' WIDTH
            _WIDTHs = cPT.Filas_DameWIDTHs({Me._PREFIJO})
            _RADIUSs = Nothing
            _HEIGHTs = cPT.Filas_DameHEIGHTs(Me._PREFIJO)
            '
            _WIDTH = _WIDTHs.FirstOrDefault
        End If
        '
        '_RADIUS = clsA.Bloque_AtributoDame(oBl.ObjectID, "RADIUS")  ' RADIUS
        'If _RADIUS = "" Then _RADIUS = clsA.BloqueDinamico_ParametroDame(oBl.ObjectID, "RADIUS")
        ''
        '_HEIGHT = clsA.Bloque_AtributoDame(oBl.ObjectID, "HEIGHT") ' HEIGHT
        'If _HEIGHT = "" Then _HEIGHT = clsA.BloqueDinamico_ParametroDame(oBl.ObjectID, "HEIGHT")
        '
        If Me._CODE = "" OrElse Me._CODE.ToUpper.StartsWith("XX") Then
            Me._CODE = Me._PREFIJO & "XX"
        End If
        '
        Me._clave = Me._EFFECTIVENAME & Me._CODE & Me._DIRECTRIZ & Me._DIRECTRIZ1 & Me._WIDTH & Me._RADIUS & Me._HEIGHT & Me._STANDARD_PART
        '
        tieneXXX = Me._clave.ToUpper.Contains("XX")
    End Sub

    Public Sub Pon_ITEM_NUMBER()
        If Me._CODE.Contains("XX") = False Then
            Me._ITEM_NUMBER = cPT.Filas_DameITEM_NUMBER(Me._CODE)
        Else
            Me._ITEM_NUMBER = Me._CODE
        End If
    End Sub
End Class
