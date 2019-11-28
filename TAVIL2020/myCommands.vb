' (C) Copyright 2011 by  
'
Imports System
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.EditorInput
Imports uau = UtilesAlberto.Utiles
Imports a2 = AutoCAD2acad.A2acad

' This line is not mandatory, but improves loading performances
<Assembly: CommandClass(GetType(TAVIL2020.MyCommands))>
Namespace TAVIL2020

    ' This class is instantiated by AutoCAD for each document when
    ' a command is called by the user the first time in the context
    ' of a given document. In other words, non static data in this class
    ' is implicitly per-document!
    Public Class MyCommands

        ' The CommandMethod attribute can be applied to any public  member 
        ' function of any public class.
        ' The function should take no arguments and return nothing.
        ' If the method is an instance member then the enclosing class is 
        ' instantiated for each document. If the member is a static member then
        ' the enclosing class is NOT instantiated.
        '
        ' NOTE: CommandMethod has overloads where you can provide helpid and
        ' context menu.

        ' Application Session Command with localized name
        <CommandMethod("TAVILUTILIDADES", "TAVILACERCADE", "TAVILACERCADE", CommandFlags.Modal + CommandFlags.Session)>
        Public Sub TAVILACERCADE() ' This method can have any name
            ' Put your command code here
            MsgBox(cfg._appnameversion)
        End Sub

        ' Application Session Command with localized name
        <CommandMethod("TAVILUTILIDADES", "TAVILCONFIGURAR", "TAVILCONFIGURAR", CommandFlags.Modal + CommandFlags.Session)>
        Public Sub TAVILCONFIGURAR() ' This method can have any name
            ' Put your command code here
            ' Put your command code here
            If Eventos.AXDocM.Count = 0 Then Exit Sub
            '
            CierraFormularios()
            If Autodesk.AutoCAD.Internal.AcAeUtilities.IsInBlockEditor Then
                MsgBox("Utilidad CONFIGURAR no permitida en Editor de Bloques...")
                Exit Sub
            End If

            frmCo = New frmConfigura
            frmCo.Visible = True
            frmCo.WindowState = Windows.Forms.FormWindowState.Normal
            frmCo.StartPosition = Windows.Forms.FormStartPosition.CenterParent
            Application.ShowModelessDialog(Application.MainWindow.Handle, frmCo, True)
        End Sub
        '
        <CommandMethod("TAVILUTILIDADES", "TAVILAUTONUMERAR", "TAVILAUTONUMERAR", CommandFlags.Modal + CommandFlags.Session)>
        Public Sub TAVILAUTONUMERAR() ' This method can have any name
            ' Put your command code here
            If Eventos.AXDocM.Count = 0 Then Exit Sub
            '
            CierraFormularios()
            If Autodesk.AutoCAD.Internal.AcAeUtilities.IsInBlockEditor Then
                MsgBox("Utilidad AUTONUMERAR no permitida en Editor de Bloques...")
                Exit Sub
            End If

            frmAu = New frmAutonumera
            frmAu.Visible = True
            frmAu.WindowState = Windows.Forms.FormWindowState.Normal
            frmAu.StartPosition = Windows.Forms.FormStartPosition.CenterParent
            Application.ShowModelessDialog(Application.MainWindow.Handle, frmAu, True)
        End Sub
        '

        '
        <CommandMethod("TAVILUTILIDADES", "TAVILPATAS", "TAVILPATAS", CommandFlags.Modal + CommandFlags.Session)>
        Public Sub TAVILPATAS() ' This method can have any name
            ' Put your command code here
            If Eventos.AXDocM.Count = 0 Then
                MsgBox("Abrir antes un fichero DWG...")
                Exit Sub
            End If
            '
            If Autodesk.AutoCAD.Internal.AcAeUtilities.IsInBlockEditor Then
                MsgBox("Utilidad PATAS no permitida en Editor de Bloques...")
                Exit Sub
            End If
            '
            ' Si está abierto, propone cerrarlo o cancelar, salimos sin cargar el AddIn
            If uau.FicheroEstaAbiertoMensaje(LAYOUTDB, "TAVIL.Patas") = MsgBoxResult.Cancel Then
                Exit Sub
            End If
            '
            ' Rellenar las clases con los datos de Excel
            cPT = New clsPT
            If Log Then cfg.PonLog("Llenada clase datos patas (cPT)", False)

            CierraFormularios()
            '
            lnBloquesPatas = cPT.Filas_DameColumnaUnicos(nombreColumnaPT.BLOCK)
            If lnBloquesPatas Is Nothing OrElse lnBloquesPatas.Count = 0 Then
                MsgBox("No hay bloques de patas TAVIL que procesar... (PT*)")
                Exit Sub
            End If
            ' Llenar listados de nombres únicos RADIUS, WIDTH y HEIGTH
            lnWIDTH = cPT.Filas_DameColumnaUnicos(nombreColumnaPT.WIDTH)
            lnRADIUS = cPT.Filas_DameColumnaUnicos(nombreColumnaPT.RADIUS)
            lnHEIGHT = cPT.Filas_DameColumnaUnicos(nombreColumnaPT.HEIGHT)
            'MsgBox("En construcción. Pendiente de datos TAVIL...")
            'Exit Sub
            '

            frmPa = New frmPatas
            frmPa.Visible = True
            frmPa.WindowState = Windows.Forms.FormWindowState.Normal
            frmPa.StartPosition = Windows.Forms.FormStartPosition.CenterParent
            Application.ShowModelessDialog(Application.MainWindow.Handle, frmPa, True)
        End Sub
        '
        '
        <CommandMethod("TAVILUTILIDADES", "TAVILUNIONES", "TAVILUNIONES", CommandFlags.Modal + CommandFlags.Session)>
        Public Sub TAVILUNIONES() ' This method can have any name
            ' Put your command code here
            If Eventos.AXDocM.Count = 0 Then Exit Sub
            '
            If Autodesk.AutoCAD.Internal.AcAeUtilities.IsInBlockEditor Then
                MsgBox("Utilidad UNIONES no permitida en Editor de Bloques...")
                Exit Sub
            End If
            '
            '
            ' Rellenar las clases con los datos de Excel
            cU = New UNIONESExcelFilas
            If Log Then cfg.PonLog("Llenada clase datos UNIONES (cU)", False)
            '
            CierraFormularios()
            '
            'MsgBox("En construcción. Pendiente de datos TAVIL...")
            'Exit Sub

            frmUn = New frmUniones
            frmUn.Visible = True
            frmUn.WindowState = Windows.Forms.FormWindowState.Normal
            frmUn.StartPosition = Windows.Forms.FormStartPosition.CenterParent
            Application.ShowModelessDialog(Application.MainWindow.Handle, frmUn, True)
        End Sub
        '
        '
        <CommandMethod("TAVILUTILIDADES", "TAVILAGRUPAR", "TAVILAGRUPAR", CommandFlags.Modal + CommandFlags.Session)>
        Public Sub TAVILAGRUPA() ' This method can have any name
            ' Put your command code here
            If Eventos.AXDocM.Count = 0 Then Exit Sub
            '
            CierraFormularios()
            '
            'MsgBox("En construcción. Pendiente de datos TAVIL...")
            'Exit Sub
            '
            If Autodesk.AutoCAD.Internal.AcAeUtilities.IsInBlockEditor Then
                MsgBox("Utilidad AGRUPAR no permitida en Editor de Bloques...")
                Exit Sub
            End If

            frmAg = New frmAgrupa
            frmAg.Visible = True
            frmAg.WindowState = Windows.Forms.FormWindowState.Normal
            frmAg.StartPosition = Windows.Forms.FormStartPosition.CenterParent
            Application.ShowModelessDialog(Application.MainWindow.Handle, frmAg, True)
        End Sub
        '
        '
        <CommandMethod("TAVILUTILIDADES", "TAVILLISTAPIEZAS", "TAVILLISTAPIEZAS", CommandFlags.Modal + CommandFlags.Session)>
        Public Sub TAVILLISTAPIEZAS() ' This method can have any name
            ' Put your command code here
            If Eventos.AXDocM.Count = 0 Then Exit Sub
            '
            CierraFormularios()
            '
            '
            If Autodesk.AutoCAD.Internal.AcAeUtilities.IsInBlockEditor Then
                MsgBox("Utilidad LISTA DE PIEZAS no permitida en Editor de Bloques...")
                Exit Sub
            End If

            frmBo = New frmBomDatos
            frmBo.Visible = True
            frmBo.WindowState = Windows.Forms.FormWindowState.Normal
            frmBo.StartPosition = Windows.Forms.FormStartPosition.CenterParent
            Application.ShowModelessDialog(Application.MainWindow.Handle, frmBo, True)
        End Sub
        '
        <CommandMethod("TAVILUTILIDADES", "TAVILBLOQUES", "TAVILBLOQUES", CommandFlags.Modal + CommandFlags.Session)>
        Public Sub TAVILBLOQUES() ' This method can have any name
            ' Put your command code here
            If Eventos.AXDocM.Count = 0 Then Exit Sub
            '
            CierraFormularios()
            '
            MsgBox("En construcción. Pendiente de datos TAVIL...")
            Exit Sub
            '
            If Autodesk.AutoCAD.Internal.AcAeUtilities.IsInBlockEditor Then
                MsgBox("Utilidad BLOQUES no permitida en Editor de Bloques...")
                Exit Sub
            End If

            frmBlo = New frmBloques
            frmBlo.Visible = True
            frmBlo.WindowState = Windows.Forms.FormWindowState.Normal
            frmBlo.StartPosition = Windows.Forms.FormStartPosition.CenterParent
            Application.ShowModelessDialog(Application.MainWindow.Handle, frmBlo, True)
        End Sub
        '
        <CommandMethod("TAVILUTILIDADES", "ATTEDITTAVIL", "ATTEDITTAVIL", CommandFlags.Modal)>  ' + CommandFlags.Session)>
        Public Sub ATTEDITTAVIL() ' This method can have any name
            ' Put your command code here
            If Eventos.AXDocM.Count = 0 Then Exit Sub
            '
            CierraFormularios()
            '
            'MsgBox("En construcción. Pendiente de datos TAVIL...")
            'Exit Sub
            '
            If Autodesk.AutoCAD.Internal.AcAeUtilities.IsInBlockEditor Then
                MsgBox("Utilidad BLOQUES no permitida en Editor de Bloques...")
                Exit Sub
            End If
            ' Si es un bloque de TAVIL, abrir nuestro formulario.
            ' Si no es un bloque de TAVIL, ejecutar comando _eattedit
            Try
                If Eventos.COMDoc().ActiveSelectionSet Is Nothing Then Exit Sub
                If Eventos.COMDoc().ActiveSelectionSet.Count > 1 Then Exit Sub
                If TypeOf Eventos.COMDoc().ActiveSelectionSet.Item(0) IsNot Autodesk.AutoCAD.Interop.Common.AcadBlockReference Then Exit Sub
                Dim oBl As Autodesk.AutoCAD.Interop.Common.AcadBlockReference = Eventos.COMDoc().ActiveSelectionSet.Item(0)
                If dicBloques.ContainsKey(oBl.EffectiveName) Then
                    Dim fiImage As String = IO.Path.Combine(dicBloques(oBl.EffectiveName), oBl.EffectiveName & ".png")
                    frmBloE = New frmBloquesEditar
                    If IO.File.Exists(fiImage) Then
                        frmBloE.pbBloque.Image = System.Drawing.Image.FromFile(fiImage)
                    Else
                        frmBloE.pbBloque.Image = My.Resources.SinImagen
                    End If
                    frmBloE.oBl = oBl
                    frmBloE.Visible = True
                    frmBloE.WindowState = Windows.Forms.FormWindowState.Normal
                    frmBloE.StartPosition = Windows.Forms.FormStartPosition.CenterParent
                    Application.ShowModelessDialog(Application.MainWindow.Handle, frmBloE, True)
                Else
                    Dim arrEnt As New ArrayList : arrEnt.Add(oBl)
                    clsA.Seleccion_CreaResalta(arrEnt, False)
                    Eventos.COMDoc().SendCommand("_eattedit ")
                End If
            Catch ex As System.Exception
                '
            End Try
        End Sub
        '
        ' Modal Command with localized name
        ' AutoCAD will search for a resource string with Id "MyCommandLocal" in the 
        ' same namespace as this command class. 
        ' If a resource string is not found, then the string "MyLocalCommand" is used 
        ' as the localized command name.
        ' To view/edit the resx file defining the resource strings for this command, 
        ' * click the 'Show All Files' button in the Solution Explorer;
        ' * expand the tree node for myCommands.vb;
        ' * and double click on myCommands.resx
        '<CommandMethod("MyGroup", "MyCommand", "MyCommandLocal", CommandFlags.Modal)> _
        'Public Sub MyCommand() ' This method can have any name
        ' Put your command code here
        'End Sub

        ' Modal Command with pickfirst selection
        '<CommandMethod("MyGroup", "MyPickFirst", "MyPickFirstLocal", CommandFlags.Modal + CommandFlags.UsePickSet)> _
        'Public Sub MyPickFirst() ' This method can have any name
        '    Dim result As PromptSelectionResult = Application.DocumentManager.MdiActiveDocument.Editor.GetSelection()
        '    If (result.Status = PromptStatus.OK) Then
        '        ' There are selected entities
        '        ' Put your command using pickfirst set code here
        '    Else
        '        ' There are no selected entities
        '        ' Put your command code here
        '    End If
        'End Sub

        ' Application Session Command with localized name
        '<CommandMethod("MyGroup", "MySessionCmd", "MySessionCmdLocal", CommandFlags.Modal + CommandFlags.Session)> _
        'Public Sub MySessionCmd() ' This method can have any name
        '    ' Put your command code here
        'End Sub

        ' LispFunction is similar to CommandMethod but it creates a lisp 
        ' callable function. Many return types are supported not just string
        ' or integer.
        '<LispFunction("MyLispFunction", "MyLispFunctionLocal")> _
        'Public Function MyLispFunction(ByVal args As ResultBuffer) ' This method can have any name
        '    ' Put your command code here

        '    ' Return a value to the AutoCAD Lisp Interpreter
        '    Return 1
        'End Function
    End Class
End Namespace