Imports Autodesk.AutoCAD.Interop.Common
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.ApplicationServices
Imports TAVIL2020.TAVIL2020
Imports System.Windows.Forms
Imports a2 = AutoCAD2acad.A2acad


Public Class frmAutonumera

    Dim FamiliaProxyUltimo As String = ""

    Private Sub frmAutonumera_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "AUTONUMERACION - v" & appVersion

        'ElementoProxyRecomendado = RecomiendaElementoLibre()
        'AddHandler Autodesk.AutoCAD.ApplicationServices.Application.Idle, AddressOf frmAutonumera_mod.AutoEnumera_AppIdle
        'AddHandler Eventos.AXDb.ObjectAppended, AddressOf frmAutonumera_mod.AutoEnumera_DBObjectAppended
        'AddHandler Eventos.AXDb.ObjectModified, AddressOf frmAutonumera_mod.AutoEnumera_DBObjectModified
        'AddHandler Eventos.AXDb.ObjectErased, AddressOf frmAutonumera_mod.AutoEnumera_ObjectErased
    End Sub

    Private Sub frmAutonumera_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'RemoveHandler Autodesk.AutoCAD.ApplicationServices.Application.Idle, AddressOf frmAutonumera_mod.AutoEnumera_AppIdle
        'RemoveHandler Eventos.AXDb.ObjectModified, AddressOf frmAutonumera_mod.AutoEnumera_DBObjectModified
        'RemoveHandler Eventos.AXDb.ObjectErased, AddressOf frmAutonumera_mod.AutoEnumera_ObjectErased
        'frmAu = Nothing
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub



    'AFLETA 27/09/19
    Private Sub BtnInsertaNum_Click(sender As Object, e As EventArgs) Handles btnInsertaNum.Click
        app_procesointerno = True
        colP_ProxiesRellena()

        Dim ElementoProxyRecomendadoTemp As String = ""
        Dim FamiliaProxyRecomendadoTemp As String = ""
        Dim IdProxyRecomendadoTemp As String = ""

        If clsA.oAppA.ActiveDocument.ActiveSpace = Autodesk.AutoCAD.Interop.Common.AcActiveSpace.acPaperSpace Then
            MsgBox("Proxy sólo se puede insertar en Espacio Modelo", MsgBoxStyle.Critical, "AVISOS AL USUARIO")
            Exit Sub
        End If

        Me.Visible = False
        CargaRecursosProxy()

        Dim bolFin As Boolean = False
        Do While bolFin = False
            Dim arrayFilterType() As Type = {GetType(BlockReference)}
            Dim oIdSel As ObjectId = clsA.ObjectId_SelectOne(OpenMode.ForRead, arrayFilterType)

            If oIdSel <> Nothing Then
                'El usuario a seleccionado un blockreference
                Dim entitySel As Entity = clsA.Entity_Get(oIdSel)
                'Mira si el blockReference seleccionado tiene el XDATA ELEMENTO
                Dim strElementoEntity As String
                Try
                    strElementoEntity = clsA.XLeeDato(entitySel.AcadObject, "ELEMENTO")
                Catch ex As Exception
                    'El blockReference no tiene el xdata. Por lo tanto se lo crea vacio para posteriormente asociarselo
                    strElementoEntity = ""
                    'Ha seleccionado un blockReference que no tiene XDATA ELEMENTO y por lo tanto le asigna el XDATA
                    clsA.XPonDato(entitySel.AcadObject, "ELEMENTO", "")
                End Try
                'En este punto, el blockreference seleccionado por el usuario ya tiene el xdata ELEMENTO

                If (strElementoEntity = "") Then
                    'Si esta vacio el xdata ELEMENTO, le asigna numeracion
                    Dim oMl As AcadMLeader = Nothing
                    oMl = clsA.MLeader_InsertaCommand()
                    If oMl IsNot Nothing Then
                        If FamiliaProxyUltimo = "" Then
                            'Para la primera vez que entra en el while
                            FamiliaProxyRecomendadoTemp = colP_BuscaFamiliaLibre()
                            IdProxyRecomendadoTemp = 1 'Como es familia nueva, empieza por el 1
                        Else
                            'Cuando entra las siguientes veces
                            FamiliaProxyRecomendadoTemp = FamiliaProxyUltimo
                            IdProxyRecomendadoTemp = colP_BuscaIDLibreEnFamilia(FamiliaProxyRecomendadoTemp)
                        End If


                        ElementoProxyRecomendadoTemp = FamiliaProxyRecomendadoTemp & "." & IdProxyRecomendadoTemp
                        'En el atributo del mleader se pone solo el id, porque solo quieren que se muestre el Id.
                        'Internamente se guardara la familia y el id del mleader. Concretamente se guardará en el XDATA
                        clsA.MLeaderBlock_PonValorAtributo(oMl, "ELEMENTO", IdProxyRecomendadoTemp)
                        clsA.XPonDato(oMl.Handle, "ELEMENTO", ElementoProxyRecomendadoTemp)
                        'Guarda en el diccionario el proxy creado para controlar futuras inserciones.
                        colP_AddElemento(ElementoProxyRecomendadoTemp, oMl)
                        'En el blockreference guarda tambien el proxy                        
                        clsA.XPonDato(CType(entitySel.AcadObject, AcadBlockReference).Handle, "ELEMENTO", ElementoProxyRecomendadoTemp)  ', regAPPCliente)
                        FamiliaProxyUltimo = FamiliaProxyRecomendadoTemp
                    End If
                ElseIf (strElementoEntity = "-1") Then
                    MsgBox("Error al leer XData del BlockReference")
                Else
                    MsgBox("El BlockReference seleccionado ya dispone del proxy: " & strElementoEntity)
                End If

            Else
                FamiliaProxyUltimo = ""
                bolFin = True
            End If

        Loop
        Me.Visible = True
        app_procesointerno = False

    End Sub

    Private Sub CargaRecursosProxy()
        ' Cargar recursos

        clsA.Clona_TodoDesdeDWGInsertando(BloqueRecursos, True, True)

        Try
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CMLEADERSTYLE", fijoCliente)
        Catch ex As Exception
            MsgBox("No existe el estilo " & fijoCliente)
            Exit Sub
        End Try
        ' Activar la capa 'proxy' y quitar el contorno de cobertura
        clsA.CapaActiva(capaproxy)
        clsA.CoberturaOnOff(False)

    End Sub

    Private Sub BtnCargarNumeracion_Click(sender As Object, e As EventArgs) Handles btnCargarNumeracion.Click
        Me.Visible = False
        colP_ProxiesRellena()
        app_procesointerno = True
        Dim arrayFilterType() As Type = {GetType(BlockReference), GetType(MLeader)}
        Dim oIdSel As ObjectId = clsA.ObjectId_SelectOne(OpenMode.ForRead, arrayFilterType)
        Dim oMl As AcadMLeader = Nothing

        Dim oEntity As Entity = Nothing

        If oIdSel <> Nothing Then
            oEntity = clsA.Entity_Get(oIdSel)
        End If
        FamiliaProxyUltimo = RecomiendaElementoLibre(oEntity).Split(".")(0)
        Me.Visible = True
        BtnInsertaNum_Click(sender, e)
        app_procesointerno = False
    End Sub


    'FIN 27/09/19
End Class