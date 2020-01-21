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

Public Class ClsUnion
    Private _UNION As String = ""
    Private _UNITS As String = ""
    Private _T1HANDLE As String = ""
    Private _T1INFEED As String = ""
    Private _T1INCLINATION As String = ""
    Private _T2HANDLE As String = ""
    Private _T2OUTFEED As String = ""
    Private _T2INCLINATION As String = "0"
    Private _ANGLE As String = ""
    Private _LADO As String = "L"

#Region "PROPERTIES"
    Public Property HANDLE As String = ""
    Public Property UnionBlock As AcadBlockReference = Nothing
    Public Property T1Block As AcadBlockReference = Nothing
    Public Property T2Block As AcadBlockReference = Nothing
    Public Property ExcelFilaUnion As UNIONESFila = Nothing
    Public Property UNIONFin As String = ""

    Public Property UNION As String
        Get
            Return _UNION
        End Get
        Set(value As String)
            value = value.Replace(" ", "").Trim
            If value <> _UNION Then
                _UNION = value
                'clsA.XPonDato(Me.HANDLE, "UNION", value)
            End If
        End Set
    End Property

    Public Property UNITS As String
        Get
            Return _UNITS
        End Get
        Set(value As String)
            value = value.Replace(" ", "")
            If value <> _UNITS Then
                _UNITS = value
                'clsA.XPonDato(Me.HANDLE, "UNITS", value)
            End If
        End Set
    End Property

    Public Property T1HANDLE As String
        Get
            Return _T1HANDLE
        End Get
        Set(value As String)
            If value <> _T1HANDLE Then
                _T1HANDLE = value
            End If
        End Set
    End Property

    Public Property T1INFEED As String
        Get
            Return _T1INFEED
        End Get
        Set(value As String)
            If value <> _T1INFEED Then
                _T1INFEED = value
                'clsA.XPonDato(Me.HANDLE, "T1INFEED", value)
            End If
        End Set
    End Property

    Public Property T1INCLINATION As String
        Get
            Return _T1INCLINATION
        End Get
        Set(value As String)
            If value <> _T1INCLINATION Then
                _T1INCLINATION = value
                'clsA.XPonDato(Me.HANDLE, "T1INCLINATION", value)
            End If
        End Set
    End Property

    Public Property T2HANDLE As String
        Get
            Return _T2HANDLE
        End Get
        Set(value As String)
            If value <> _T2HANDLE Then
                _T2HANDLE = value
                'clsA.XPonDato(Me.HANDLE, "T2HANDLE", value)
            End If
        End Set
    End Property

    Public Property T2OUTFEED As String
        Get
            Return _T2OUTFEED
        End Get
        Set(value As String)
            If value <> _T2OUTFEED Then
                _T2OUTFEED = value
                'clsA.XPonDato(Me.HANDLE, "T2OUTFEED", value)
            End If
        End Set
    End Property

    Public Property T2INCLINATION As String
        Get
            Return _T2INCLINATION
        End Get
        Set(value As String)
            If value <> _T2INCLINATION Then
                _T2INCLINATION = value
                'clsA.XPonDato(Me.HANDLE, "T2INCLINATION", value)
            End If
        End Set
    End Property
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
                'clsA.XPonDato(Me.HANDLE, "ROTATION", value)
            End If
        End Set
    End Property

    Public Property LADO As String
        Get
            Return _LADO
        End Get
        Set(value As String)
            _LADO = value
        End Set
    End Property

#End Region
    ''' <summary>
    ''' Editar uniones existentes o Nuevas inserciones masivas.
    ''' Solo Handle para sacar/poner el resto de los XData del AcadBlockReference
    ''' </summary>
    ''' <param name="handle"></param>
    Public Sub New(handle As String)
        Me.HANDLE = handle
        'PonDatosX()
        Try
            UnionBlock = Eventos.COMDoc().HandleToObject(Me.HANDLE)
        Catch ex As Exception
            Exit Sub
        End Try
        '
        Me.UNION = clsA.XLeeDato(UnionBlock.Handle, "UNION")
        Me.UNITS = clsA.XLeeDato(UnionBlock.Handle, "UNITS")
        Me.T1HANDLE = clsA.XLeeDato(UnionBlock.Handle, "T1HANDLE")
        Me.T1INFEED = clsA.XLeeDato(UnionBlock.Handle, "T1INFEED")
        Me.T1INCLINATION = clsA.XLeeDato(UnionBlock.Handle, "T1INCLINATION")
        Me.T2HANDLE = clsA.XLeeDato(UnionBlock.Handle, "T2HANDLE")
        Me.T2OUTFEED = clsA.XLeeDato(UnionBlock.Handle, "T2OUTFEED")
        Me.T2INCLINATION = clsA.XLeeDato(UnionBlock.Handle, "T2INCLINATION")
        Me.ANGLE = clsA.XLeeDato(UnionBlock.Handle, "ROTATION")
        Me.LADO = clsA.XLeeDato(UnionBlock.Handle, "LADO")
        Try
            If Me._T1HANDLE <> "" AndAlso IsNumeric(Me._T1HANDLE) Then T1Block = Eventos.COMDoc().HandleToObject(Me._T1HANDLE)
        Catch ex As Exception
        End Try
        Try
            If Me._T2HANDLE <> "" AndAlso IsNumeric(Me._T2HANDLE) Then T2Block = Eventos.COMDoc().HandleToObject(Me._T2HANDLE)
        Catch ex As Exception
        End Try
        If Me.T1INFEED <> "" AndAlso Me.T1INCLINATION <> "" AndAlso Me.T2OUTFEED <> "" AndAlso Me.T2INCLINATION <> "" Then
            ExcelFilaUnion = cU.Fila_BuscaDame(Me.T1INFEED, Me.T1INCLINATION, Me.T2OUTFEED, Me.T2INCLINATION, Me.ANGLE)
        End If
        'If Me.UNION = "" AndAlso ExcelFilaUnion IsNot Nothing Then Me.UNION = ExcelFilaUnion.UNION
        'If Me.UNITS = "" AndAlso ExcelFilaUnion IsNot Nothing Then Me.UNITS = ExcelFilaUnion.UNITS
    End Sub

    ''' <summary>
    ''' Nuevas uniones. Tenemos que pasar todos los parámetros.
    ''' </summary>
    ''' <param name="handle"></param>
    ''' <param name="Union"></param>
    ''' <param name="Units"></param>
    ''' <param name="T1Handle"></param>
    ''' <param name="T1Infeed"></param>
    ''' <param name="T1Inclination"></param>
    ''' <param name="T2Handle"></param>
    ''' <param name="T2Outfeed"></param>
    ''' <param name="T2Inclination"></param>
    ''' <param name="Rotation"></param>
    Public Sub New(handle As String, Union As String, Units As String,
                   T1Handle As String, T1Infeed As String, T1Inclination As String,
                    T2Handle As String, T2Outfeed As String, T2Inclination As String,
                    Rotation As String, lado As String)
        Me.HANDLE = handle
        'PonDatosX(Union, Units, T1Handle, T1Handle, T1Inclination, T2Handle, T2Outfeed, T2Inclination, Rotation)
        Try
            UnionBlock = Eventos.COMDoc().HandleToObject(Me.HANDLE)
        Catch ex As Exception
            Exit Sub
        End Try
        '
        Me.UNION = Union
        Me.UNITS = Units
        Me.T1HANDLE = T1Handle
        Me.T1INFEED = T1Infeed
        Me.T1INCLINATION = T1Inclination
        Me.T2HANDLE = T2Handle
        Me.T2OUTFEED = T2Outfeed
        Me.T2INCLINATION = T2Inclination
        Me.ANGLE = Rotation
        Me.LADO = lado
        Try
            T1Block = Eventos.COMDoc().HandleToObject(Me._T1HANDLE)
        Catch ex As Exception
        End Try
        Try
            T2Block = Eventos.COMDoc().HandleToObject(Me._T2HANDLE)
        Catch ex As Exception
        End Try
        If Me.T1INFEED <> "" AndAlso Me.T1INCLINATION <> "" AndAlso Me.T2OUTFEED <> "" AndAlso Me.T2INCLINATION <> "" Then
            ExcelFilaUnion = cU.Fila_BuscaDame(Me.T1INFEED, Me.T1INCLINATION, Me.T2OUTFEED, Me.T2INCLINATION, Me.ANGLE)
        End If
    End Sub

    'Public Function OUnionDame() As AcadBlockReference
    '    If clsA Is Nothing Then clsA = New a2.A2acad(Eventos.COMApp, cfg._appFullPath, regAPPCliente)
    '    Return Eventos.COMDoc().HandleToObject(Me.HANDLE)
    'End Function
    Public Sub PonAtributos()
        If UnionBlock IsNot Nothing Then
            Try
                Dim dicNomVal As New Dictionary(Of String, String)
                dicNomVal.Add("UNION", Me.UNIONFin)
                dicNomVal.Add("UNITS", Me.UNITS)
                clsA.Bloque_AtributoEscribe(UnionBlock.ObjectID, dicNomVal)
                clsA.XPonDato(Me.HANDLE, "UNION", Me.UNIONFin)
                clsA.XPonDato(Me.HANDLE, "UNITS", Me.UNITS)
                clsA.XPonDato(Me.HANDLE, "T1HANDLE", Me.T1HANDLE)
                clsA.XPonDato(Me.HANDLE, "T1INFEED", Me.T1INFEED)
                clsA.XPonDato(Me.HANDLE, "T1INCLINATION", Me.T1INCLINATION)
                clsA.XPonDato(Me.HANDLE, "T2HANDLE", Me.T2HANDLE)
                clsA.XPonDato(Me.HANDLE, "T2OUTFEED", Me.T2OUTFEED)
                clsA.XPonDato(Me.HANDLE, "T2INCLINATION", Me.T2INCLINATION)
                clsA.XPonDato(Me.HANDLE, "ROTATION", Me.ANGLE)
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
End Class

