' (C) Copyright 2020 by 
Imports System
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.EditorInput
Imports a2 = AutoCAD2acad.A2acad


' This line is not mandatory, but improves loading performances
<Assembly: ExtensionApplication(GetType(TAVIL2020.MyPlugin))>

Namespace TAVIL2020

    ' This class is instantiated by AutoCAD once and kept alive for the 
    ' duration of the session. If you don't do any one time initialization 
    ' then you should remove this class.
    Public Class MyPlugin
        Implements IExtensionApplication

        Public Sub New()
            Me.Initialize()
        End Sub

        Public Sub Initialize() Implements IExtensionApplication.Initialize
            ' Add one time initialization here
            ' One common scenario is to setup a callback function here that 
            ' unmanaged code can call. 
            ' To do this:
            ' 1. Export a function from unmanaged code that takes a function
            '    pointer and stores the passed in value in a global variable.
            ' 2. Call this exported function in this function passing delegate.
            ' 3. When unmanaged code needs the services of this managed module
            '    you simply call acrxLoadApp() and by the time acrxLoadApp 
            '    returns  global function pointer is initialized to point to
            '    the C# delegate.
            ' For more info see: 
            ' http:'msdn2.microsoft.com/en-US/library/5zwkzwf4(VS.80).aspx
            ' http:'msdn2.microsoft.com/en-us/library/44ey4b32(VS.80).aspx
            ' http:'msdn2.microsoft.com/en-US/library/7esfatk4.aspx
            ' as well as some of the existing AutoCAD managed apps.

            ' Initialize your plug-in application hereTry
            Try
                ' ***** Inicializar los eventos que necesitemos. Y los objetos principales de AutoCAD
                Eventos.Eventos_Inicializa()
                ' **************************
                cIni = New clsINI()
                'cfg = New UtilesAlberto.Conf("aiiao2K19_2aCAD", System.Reflection.Assembly.GetExecutingAssembly)
                clsA = New AutoCAD2acad.A2acad.A2acad("aiiao2K20_2aCAD", Eventos.COMApp, appFull, regAPPCliente)
            Catch ex As [System].Exception
                MsgBox(ex.ToString)
                Exit Sub
            End Try
            '
            'If Autodesk.AutoCAD.Runtime.ExtensionLoader.IsLoaded(autocad2acad) = True Then
            '    'MsgBox("Cargado " & autocad2acad)
            'Else
            '    dll2acad = Autodesk.AutoCAD.Runtime.ExtensionLoader.Load(autocad2acad)
            '    Call AppDomain.CurrentDomain.GetAssemblies()
            'End If

            '''' Leer configuración
            Try
                Dim txtError As String() = INICargar()
                If txtError(0) <> "" Then
                    MsgBox(txtError(0))
                    If Log Then PonLog(txtError(0), True)
                    Exit Sub
                Else
                    If Log Then PonLog(txtError(1), True)
                End If
            Catch ex As System.Exception
                MsgBox("Activate--> INICargar --> " & ex.Message)
                Exit Sub
            End Try
            If Log Then PonLog("Añadidos enventos", True)
            ' 
            ' Rellenar las clasese con los datos de Excel
            'cPT = New PT
            '
            ' Cargar los datos del fichero LAYOUTDB.xlsx
            ' Si está abierto, propone cerrarlo o cancelar, salimos sin cargar el AddIn
            If Utiles.FicheroEstaAbiertoMensaje(LAYOUTDB, appName) = MsgBoxResult.Cancel Then
                Exit Sub
            End If

            Try
                'clsD = New clsLAYOUTDBS4
                'If Log Then cfg.PonLog("Llenada coleccion seleccionables (clsD)", False)
            Catch ex As System.Exception
                Debug.Print(ex.ToString)
            End Try
        End Sub

        Public Sub Terminate() Implements IExtensionApplication.Terminate
            Try
                Eventos.Eventos_Vacia()
            Catch ex As System.Exception
                Debug.Print(ex.tostring)
            End Try
            ' Do plug-in application clean up here
            clsA = Nothing
            'Me.Finalize()
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Sub
    End Class

End Namespace