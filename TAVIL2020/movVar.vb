Imports Autodesk.AutoCAD.Interop.Common
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.ApplicationServices
Imports TAVIL2020.TAVIL2020
Imports System.Windows.Forms
Imports AutoCAD2acad.A2acad
Imports System.Linq
Imports a2 = AutoCAD2acad.A2acad
Imports System.Runtime.InteropServices

Module movVar
    ' ***** OBJETOS AUTOCAD
    Public ed As Autodesk.AutoCAD.EditorInput.Editor
    Public oSel As Autodesk.AutoCAD.Interop.AcadSelectionSet
    Public oBlR As AcadBlockReference = Nothing     ' AcadBlockReference de la cinta que seleccionemos.
    Public btnOn As Drawing.Color = Control.DefaultBackColor
    Public btnOff As Drawing.Color = Drawing.Color.FromArgb(255, 192, 192)

    ' ***** OBJETOS AUTOCAD ACTIVE X
    Public docAct As Document

    ' ***** ASSEMBLIES
    Public autocad2acad As String = IO.Path.Combine(IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location), "AutoCAD2acad.dll")  ' Dll 2acad
    Public dll2acad As Reflection.Assembly = Nothing
    '
    ' ***** CLASES
    Public clsA As a2.A2acad = Nothing
    Public clsD As clsLAYOUTDBS4 = Nothing
    Public cIni As clsINI = Nothing
    'Public cfg As UtilesAlberto.Conf
    'Public cXML As ClosedXML2acad.ClosedXML2acad
    Public pataD As clsBloquePataDatos     ' Datos de un bloque de pata (Parametros y Atributos)
    ' ***** CLASES con los datos de cada Hoja Excel
    Public cPT As clsPT     ' Hoja PT de LAYOUTDBS4.xlsx
    Public cU As UNIONESFilas
    '
    ' ***** FORMULARIO
    Public frmCo As frmConfigura
    Public frmAu As frmAutonumera
    Public frmPa As frmPatas
    Public frmUn As frmUniones
    Public frmAg As frmAgrupa
    Public frmBo As frmBomDatos
    Public frmBlo As frmBloques
    Public frmBloE As frmBloquesEditar
    '
    ' ***** COLECCIONES
    Public colIds As List(Of Long)      ' Coleccion de Ids borrados
    Public colHan As List(Of String)
    Public dicBloques As Dictionary(Of String, String) = Nothing          ' Key=nombre bloque (Sin extension), Value=Directorio Bloque
    Public lnBloquesPatas As List(Of String)     ' Nombres únicos de bloques de patas
    Public lnRADIUS As List(Of String)           ' Nombres únicos de RADIUS de patas
    Public lnWIDTH As List(Of String)            ' Nombres únicos de WIDTH de patas
    Public lnHEIGHT As List(Of String)           ' Nombres únicos de HEIGHT de patas
    Public patasSIPlanta As String() = {"PLANTA", "TVIEW"}
    Public patasNoPlanta As String() = {"ALÇAT1", "ALÇAT2", "FVIEW", "SVIEW"}
    Public HojasTransportadores As List(Of String)
    '
    ' ***** CONSTANTES
    Public Const regAPPCliente As String = "TAVIL2acad"
    Public Const fijoCliente As String = "TAVIL"
    Public Const fijoYear As String = "2020"
    Public Const fijoClienteYear As String = fijoCliente & fijoYear
    Public Const capaproxytabla As String = "CAPABOM"
    Public Const capaproxy As String = "PROXY"
    Public Const estilotexto As String = fijoCliente & "_TEXTO"
    Public Const estilotabla As String = fijoCliente & "_TABLA"
    Public Const cGRUPO As String = "GRUPO"
    Public Const NombreBloqueUNION As String = "UNION"
    'Public Const BloqueUnion As String = cUNION
    '
    ' ***** VARIABLES CONFIGURACION
    Public Log As Boolean = False
    Public BloqueRecursos As String = ""    ' DWG con todos los recursos del desarrollo
    Public sizeImg As Integer = 100
    Public BloquesDir As String = "BLOQUES"
    Public PatasDir As String = "PEUS"
    Public PatasCapa As String = "PEUS I ACCESOS"
    Public PatasRef As String = "REF.PEUS"
    Public CintasDir As String = "CINTES"
    Public CintasCapa As String = "CINTES"
    Public CintasRef As String = "REF.CINTES"
    Public LAYOUTDB As String = "LAYOUTDBS4.xlsx"                  ' fullPath del fichero LAYOUTDB.xlsx que hace de base de datos.
    Public HojaPatas As String = "PT"
    Public HojaUniones As String = "UNIONES"
    Public HojaATR As String = "ATR"
    Public HojaSeleccionables As String = "SELECCIONABLES"
    Public HojaConceptos As String = "CONCEPTOS"
    Public HojaIdiomas As String = "IDIOMAS"
    '
    '
    ' ***** VARIABLES APLICACION
    Public appFull As String = System.Reflection.Assembly.GetExecutingAssembly.Location
    Public appFolder As String = IO.Path.GetDirectoryName(appFull)     '' Solo Directorio
    Public appName As String = My.Application.Info.AssemblyName        '' 
    Public appFolderandname As String = IO.Path.Combine(appFolder, appName)        '' 
    Public appVersion As String = My.Application.Info.Version.ToString
    Public appNameandversion As String = appName & " - v" & appVersion
    Public appIni As String = IO.Path.ChangeExtension(appFull, ".ini")
    Public appLog As String = IO.Path.ChangeExtension(appFull, ".log")
    Public appConf As String = IO.Path.ChangeExtension(appFull, ".config")
    Public appXLSX As String = IO.Path.Combine(appFolder, "LAYOUTDBS4.xlsx")
    Public app_procesointerno As Boolean = False 'afleta
    '
#Region "UTILITIES"
    Public Function INICargar() As String()
        'If cXML Is Nothing Then cXML = New ClosedXML2acad.ClosedXML2acad
        Dim mensaje(1) As String
        '' Mensaje(0) contendrá los errores.
        '' mensaje(1) contendrá los valores leidos del .INI
        mensaje(0) = "" : mensaje(1) = ""
        mensaje(1) &= "***** Configuración :" & vbCrLf
        '[OPTIONS]
        'Log=1
        'BloqueRecursos=BloqueRecursos.dwg
        'sizeImg=175
        'BloquesDir=BLOQUES
        'PatasDir=PATAS
        'PatasCapa=PEUS I ACCESOS
        'PatasRef=REF.PEUS
        'CintasDir=CINTES
        'CintasCapa=CINTES
        'CintasRef=REF.CINTES
        'patasSiPlanta=PLANTA,TVIEW
        'patasNoPlanta = ALCAT1,ALÇAT2,FVIEW,SVIEW
        'LAYOUTDB=LAYOUTDBS4.xlsx
        'HojasTransportadores=TRD3-TRANSP.RODILLOS,TRD3-TRANSP.BAJADA_RODILLOS_G,TRD3-TRANSP.CURVA_RODILLOS,TCB3-TRANSP.CON_BANDA
        'HojaPatas=PT
        'HojaUniones=UNIONES
        'HojaATR=ATR
        'HojaSeleccionables=SELECCIONABLES
        'HojaConceptos=CONCEPTOS
        'HojaIdiomas=IDIOMAS

        Dim LogTemp As String = cIni.IniGet(appIni, "OPTIONS", "Log")
        Log = IIf(LogTemp = "1", True, False)
        mensaje(1) &= "Log = " & Log & vbCrLf
        '
        BloqueRecursos = cIni.IniGet(appIni, "OPTIONS", "BloqueRecursos")
        If BloqueRecursos <> "" Then
            BloqueRecursos = IO.Path.Combine(appFolder, BloqueRecursos)
        End If
        mensaje(1) &= "BloqueRecursos = " & BloqueRecursos & vbCrLf
        '
        Dim sizeImgTemp As String = cIni.IniGet(appIni, "OPTIONS", "sizeImg")
        sizeImg = IIf(IsNumeric(sizeImgTemp), CInt(sizeImgTemp), 100)
        mensaje(1) &= "sizeImg = " & sizeImg.ToString & vbCrLf
        '
        BloquesDir = cIni.IniGet(appIni, "OPTIONS", "BloquesDir")
        If BloquesDir.StartsWith(".\") Then
            BloquesDir = BloquesDir.Replace(".\", appFolder & "\")
        ElseIf BloquesDir.Contains("\") = False And BloquesDir.Contains(":") = False Then
            BloquesDir = IO.Path.Combine(appFolder, BloquesDir)
        End If
        mensaje(1) &= "BloquesDir = " & BloquesDir & vbCrLf
        '
        PatasDir = cIni.IniGet(appIni, "OPTIONS", "PatasDir")
        PatasDir = IO.Path.Combine(BloquesDir, PatasDir)
        mensaje(1) &= "PatasDir = " & PatasDir & vbCrLf
        '
        PatasCapa = cIni.IniGet(appIni, "OPTIONS", "PatasCapa")
        PatasRef = cIni.IniGet(appIni, "OPTIONS", "PatasRef")
        mensaje(1) &= "PatasCapa = " & PatasCapa & vbCrLf
        mensaje(1) &= "PatasRef = " & PatasRef & vbCrLf
        '
        CintasDir = cIni.IniGet(appIni, "OPTIONS", "CintasDir")
        CintasDir = IO.Path.Combine(BloquesDir, CintasDir)
        mensaje(1) &= "CintasDir = " & CintasDir & vbCrLf
        '
        CintasCapa = cIni.IniGet(appIni, "OPTIONS", "CintasCapa")
        CintasRef = cIni.IniGet(appIni, "OPTIONS", "CintasRef")
        mensaje(1) &= "CintasCapa = " & CintasCapa & vbCrLf
        mensaje(1) &= "CintasRef = " & CintasRef & vbCrLf
        '
        Dim partes() As String = cIni.IniGet(appIni, "OPTIONS", "patasSiPlanta").Split(","c)
        If partes IsNot Nothing AndAlso partes.Count > 0 Then
            ReDim patasSIPlanta(partes.Count - 1) : partes.CopyTo(patasSIPlanta, 0)
        End If
        ReDim partes(-1)
        partes = cIni.IniGet(appIni, "OPTIONS", "patasNoPlanta").Split(","c)
        If partes IsNot Nothing AndAlso partes.Count > 0 Then
            ReDim patasNoPlanta(partes.Count - 1) : partes.CopyTo(patasNoPlanta, 0)
        End If
        '
        ' LAYOUTDB=LAYOUTDBS4.xlsx
        LAYOUTDB = cIni.IniGet(appIni, "OPTIONS", "LAYOUTDB")
        If LAYOUTDB.StartsWith(".\") Then
            LAYOUTDB = LAYOUTDB.Replace(".\", appFolder & "\")
        ElseIf LAYOUTDB.Contains("\") = False And LAYOUTDB.Contains(":") = False Then
            LAYOUTDB = IO.Path.Combine(appFolder, LAYOUTDB)
        End If
        mensaje(1) &= "LAYOUTDB = " & LAYOUTDB & vbCrLf
        '
        'HojasTransportadores= TRD3-TRANSP.RODILLOS,TRD3-TRANSP.BAJADA_RODILLOS_G,TRD3-TRANSP.CURVA_RODILLOS,TCB3-TRANSP.CON_BANDA
        Dim partesHojasTransportadores As String() = cIni.IniGet(appIni, "OPTIONS", "HojasTransportadores").Split(","c)
        If partesHojasTransportadores IsNot Nothing AndAlso partesHojasTransportadores.Count > 0 Then
            HojasTransportadores = New List(Of String)
            HojasTransportadores.AddRange(partesHojasTransportadores)
            mensaje(1) &= "HojasTransportadores = " & String.Join(",", partesHojasTransportadores) & vbCrLf
        End If
        '
        'HojaPatas = PT
        HojaPatas = cIni.IniGet(appIni, "OPTIONS", "HojaPatas")
        mensaje(1) &= "HojaPatas = " & HojaPatas & vbCrLf
        '
        'HojaUniones = UNIONES
        HojaUniones = cIni.IniGet(appIni, "OPTIONS", "HojaUniones")
        mensaje(1) &= "HojaUniones = " & HojaUniones & vbCrLf
        '
        'HojaATR = ATR
        HojaATR = cIni.IniGet(appIni, "OPTIONS", "HojaATR")
        mensaje(1) &= "HojaATR = " & HojaATR & vbCrLf
        '
        'HojaSeleccionables = SELECCIONABLES
        HojaSeleccionables = cIni.IniGet(appIni, "OPTIONS", "HojaSeleccionables")
        mensaje(1) &= "HojaSeleccionables = " & HojaSeleccionables & vbCrLf
        '
        'HojaConceptos = CONCEPTOS
        HojaConceptos = cIni.IniGet(appIni, "OPTIONS", "HojaConceptos")
        mensaje(1) &= "HojaConceptos = " & HojaConceptos & vbCrLf
        '
        'HojaIdiomas = IDIOMAS
        HojaIdiomas = cIni.IniGet(appIni, "OPTIONS", "HojaIdiomas")
        mensaje(1) &= "HojaIdiomas = " & HojaIdiomas & vbCrLf
        '
        ' Al fichero log la configuración leida.
        'If Log Then cfg.PonLog(mensaje(1), True)
        '
        ' ***** Comprobar ficheros y directorios. Si no existe alguno, devolvemos error.
        ' FICHEROS
        If IO.File.Exists(BloqueRecursos) = False Then
            mensaje(0) &= "No existe fichero " & BloqueRecursos & vbCrLf
        End If
        '
        If IO.File.Exists(LAYOUTDB) = False Then
            mensaje(0) &= "No existe fichero " & LAYOUTDB & vbCrLf
        End If
        ' DIRECTORIOS
        If IO.Directory.Exists(BloquesDir) = False Then
            mensaje(0) &= "No existe directorio " & BloquesDir & vbCrLf
        Else
            dicBloques_LlenaConDirRaiz(BloquesDir)
        End If
        '
        If IO.Directory.Exists(PatasDir) = False Then
            mensaje(0) &= "No existe directorio " & PatasDir & vbCrLf
        End If
        '
        If IO.Directory.Exists(CintasDir) = False Then
            mensaje(0) &= "No existe directorio " & CintasDir & vbCrLf
        End If
        '
        Return mensaje
    End Function
    '
    Public Sub dicBloques_LlenaConDirRaiz(queDirRaiz As String)
        If IO.Directory.Exists(queDirRaiz) = False Then Exit Sub
        '
        dicBloques = New Dictionary(Of String, String)
        For Each queFi As String In IO.Directory.GetFiles(queDirRaiz, "*.dwg", IO.SearchOption.AllDirectories)
            Dim nombre As String = IO.Path.GetFileNameWithoutExtension(queFi)
            Dim directorio As String = IO.Path.GetDirectoryName(queFi)
            If dicBloques.ContainsKey(nombre) = False Then dicBloques.Add(nombre, directorio)
        Next
    End Sub
    '
    Public Sub CierraFormularios()
        If frmCo IsNot Nothing Then frmCo.Close()
        If frmAu IsNot Nothing Then frmAu.Close()
        If frmPa IsNot Nothing Then frmPa.Close()
        If frmUn IsNot Nothing Then frmUn.Close()
        If frmAg IsNot Nothing Then frmAg.Close()
        If frmBo IsNot Nothing Then frmBo.Close()
    End Sub

    Public Sub PonLog(ByVal quetexto As String, Optional ByVal borrar As Boolean = False)
        If borrar = True And IO.File.Exists(appLog) Then IO.File.Delete(appLog)
        If quetexto.EndsWith(vbCrLf) = False Then quetexto &= vbCrLf
        IO.File.AppendAllText(appLog, Date.Now & vbTab & quetexto)
    End Sub
#End Region
End Module
