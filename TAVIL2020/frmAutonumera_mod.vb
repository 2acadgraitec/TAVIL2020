Imports System.Windows.Forms
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.Interop.Common
Imports System.Linq

Module frmAutonumera_mod
    Public colP As New Dictionary(Of String, List(Of clsProxyML))    ' Key=ELEMENTO (Atributo), Value=clsProxy
    Public arrayProxiesEliminados() As String
    Public ElementoProxyRecomendado As String

    Public listIdToReset As List(Of ObjectId) = New List(Of ObjectId)
    Public ProxyToUpdate As New Dictionary(Of ObjectId, String)

#Region "UTILITIES"

    Public Sub AutoEnumera_DBObjectModified(sender As Object, e As ObjectEventArgs)
        If (app_procesointerno = False) Then
            If TypeOf e.DBObject Is MLeader Then
                Dim oMLeader As MLeader = CType(e.DBObject, MLeader)
                Dim attRef As AttributeReference = clsA.AttributeReference_Get_FromMLeader(oMLeader.Id, "ELEMENTO", OpenMode.ForWrite, False)

                If Not attRef Is Nothing Then
                    'Obtiene los valores nuevos (AttributeReference) y antiguos(xdata)
                    Dim strElementoEntityNew As String = attRef.TextString
                    Dim strElementoEntityOld As String = clsA.XLeeDato(oMLeader.AcadObject, "ELEMENTO") ', regAPPCliente)
                    'Como en el atributo no esta la familia, consulta en el xdata y coge la misma familia del elemento antiguo y genera correctamente el valor de elemento nuevo.
                    strElementoEntityNew = strElementoEntityOld.Split(".")(0) & "." & strElementoEntityNew
                    'Mira si el formato es correcto
                    If strElementoEntityNew.Contains(".") AndAlso strElementoEntityNew.Split(".").Length = 2 Then
                        'Comprueba si ha cambiado el valor
                        If strElementoEntityNew <> strElementoEntityOld Then
                            If Not colP_ExisteElemento(strElementoEntityNew) Then
                                If Not ExisteEnArrayProxiesEliminados(strElementoEntityNew) Then
                                    Dim arrayElementoNew() As String = strElementoEntityNew.Split(".")
                                    Dim arrayElementoOld() As String = strElementoEntityOld.Split(".")
                                    'Mira si ha cambiado de familia. Si ha cambiado, pregunta al usuario si desea continuar.
                                    If arrayElementoOld(0) <> arrayElementoNew(0) Then
                                        'Como en el atributo no se indica la familia, no es posible modificar la familia, por lo tanto, no deberia entrar aqui el proceso.
                                        If Not ProxyToUpdate.ContainsKey(oMLeader.Id) Then
                                            If MessageBox.Show("¿Desea cambiar la familia al Proxy?", "Numeración Con Proxy", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = vbYes Then
                                                ProxyToUpdate.Add(oMLeader.Id, strElementoEntityNew)

                                            End If
                                        End If

                                    Else
                                        If Not ProxyToUpdate.ContainsKey(oMLeader.Id) Then
                                            ProxyToUpdate.Add(oMLeader.Id, strElementoEntityNew)
                                            ActualizaArrayProxiesEliminados(strElementoEntityOld)
                                        End If

                                    End If

                                Else
                                    If Not ProxyToUpdate.ContainsKey(oMLeader.Id) Then
                                        ProxyToUpdate.Add(oMLeader.Id, strElementoEntityOld)
                                        MessageBox.Show("El identificador que se quiere asociar fue borrado. Se cargará al valor anterior.", "Numeración Con Proxy", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    End If
                                End If


                            Else

                                If Not ProxyToUpdate.ContainsKey(oMLeader.Id) Then
                                    ProxyToUpdate.Add(oMLeader.Id, strElementoEntityOld)
                                    MessageBox.Show("El identificador que se quiere asociar está asociado a otro Proxy. Se cargará al valor anterior.", "Numeración Con Proxy", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                End If

                            End If
                        End If
                    Else
                        If Not ProxyToUpdate.ContainsKey(oMLeader.Id) Then
                            ProxyToUpdate.Add(oMLeader.Id, strElementoEntityOld)
                            MessageBox.Show("El identificador que se quiere asociar tiene formato incorrecto. Se cargará al valor anterior.", "Numeración Con Proxy", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If
                    End If


                End If
                'ElseIf TypeOf e.DBObject Is BlockReference Then
                '    modTavil.AcadBlockReference_Modified(Ev.EvApp.ActiveDocument.HandleToObject(e.GetHashCode))
            End If

        End If

    End Sub

    Public Sub AutoEnumera_DBObjectAppended(sender As Object, e As ObjectEventArgs)
        'Cuando se inserta mediante este plugin, no hace nada. Es solo para los casos que el usuario realiza insercciones.
        Dim strElementoEntity As String
        If (app_procesointerno = False) Then
            If TypeOf e.DBObject Is BlockReference Then
                listIdToReset.Add(e.DBObject.Id)
            ElseIf TypeOf e.DBObject Is MLeader Then
                'Mira si el Mleader añadido es basado en una de la aplicacion, para ello, mira si tiene el atributo ELEMENTO
                Try
                    'Hay que acceder con Active X a la base de datos porque con ACAD da error al estar eliminado el objeto
                    ' Cuando el usuario copia y pega, Autocad genera un blockreference temporal que es diferente al que se inserta y es visible en el documento
                    ' por eso se pasa el parametro BlkRefIsErased a true ya que esta erased. Si no daría error
                    strElementoEntity = clsA.AttributeReference_Get_FromMLeader(e.DBObject.Id, "ELEMENTO", OpenMode.ForRead, True).TextString
                Catch ex As Exception
                    strElementoEntity = "-1" 'Ha seleccionado un Mleader que no tiene atributo ELEMENTO
                End Try

                If strElementoEntity <> "" And strElementoEntity <> "-1" Then
                    'Es un proxy añadido fuera de aplicacion, por lo tanto, lo elimina.
                    MessageBox.Show("Debe añadir el Proxy a través de la aplicación. Se cancelará el comando.", "Numeración Con Proxy", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    app_procesointerno = True
                    clsA.oAppA.ActiveDocument.SendCommand(Chr(27)) 'Manda la tecla escape para cancelar el comando actual.
                    'CType(e.DBObject.AcadObject, AcadMLeader).Delete()
                    app_procesointerno = False
                End If
            End If
        End If

    End Sub

    Public Sub AutoEnumera_ObjectErased(ByVal sender As Object, ByVal e As ObjectErasedEventArgs)
        Dim strElementoEntity As String

        If (app_procesointerno = False) Then
            If TypeOf e.DBObject Is BlockReference Then

                Try
                    'Hay que acceder con Active X a la base de datos porque con ACAD da error al estar eliminado el objeto
                    'strElementoEntity = clsA.BloqueAtributoDame(CType(ent.AcadObject, AcadBlockReference).ObjectID, "ELEMENTO")
                    'strElementoEntity = clsA.AttributeReference_Get(e.DBObject.Id, "ELEMENTO", OpenMode.ForRead, True).TextString
                    strElementoEntity = clsA.XLeeDatoNET(e.DBObject.Id, "ELEMENTO", True)
                Catch ex As Exception
                    strElementoEntity = "-1" 'Ha seleccionado un blockReference que no tiene atributo ELEMENTO y por lo tanto no se le puede asignar un proxy
                End Try


                If strElementoEntity <> "" And strElementoEntity <> "-1" Then
                    ActualizaArrayProxiesEliminados(strElementoEntity)
                    Try
                        colP_BuscaProxyPorElemento(strElementoEntity).oMl.Delete()
                    Catch ex As Exception

                    End Try





                End If

                'End If

            ElseIf TypeOf e.DBObject Is MLeader Then


                Try
                    'Hay que acceder con Active X a la base de datos porque con ACAD da error al estar eliminado el objeto
                    strElementoEntity = clsA.AttributeReference_Get_FromMLeader(e.DBObject.Id, "ELEMENTO", OpenMode.ForRead, True).TextString
                    ' como falta la familia, hay que buscarlo en el colP la familia correspondiente
                    strElementoEntity = colP_BuscaFamiliaPorObjectId(CType(e.DBObject.AcadObject.ObjectID, Long)) & "." & strElementoEntity


                Catch ex As Exception
                    strElementoEntity = "-1" 'Ha seleccionado un Mleader que no tiene atributo ELEMENTO
                End Try

                If strElementoEntity <> "" And strElementoEntity <> "-1" Then
                    'Dim arrayProxy As ArrayList = clsA.BloquesDameTodos_PorAtributo("ELEMENTO", strElementoEntity, True)
                    Dim arrayProxy As ArrayList = clsA.DameBloquesTODOS_XData("ELEMENTO", strElementoEntity)

                    If arrayProxy.Count > 0 Then
                        'En principio solo tiene que encontrar uno
                        'clsA.BloqueAtributoEscribe(CType(arrayProxy(0), AcadBlockReference).ObjectID, "ELEMENTO", "") 'Lo pone vacio porque se elimina el proxy.
                        clsA.XPonDato(CType(arrayProxy(0), AcadBlockReference).Handle, "ELEMENTO", "")
                    End If
                    ActualizaArrayProxiesEliminados(strElementoEntity)

                    'DeleteElemento(strElementoEntity)
                End If

            End If
        End If

    End Sub

    Public Sub AutoEnumera_AppIdle(ByVal sender As Object, ByVal e As EventArgs)
        If (app_procesointerno = False) Then
            ReseteaXDataIncorrectos()
            ActualizaProxyIncorrectos()
        End If
    End Sub

    Public Function colP_BuscaIDLibreEnFamilia(strFamilia As String) As String
        Dim bolFin As Boolean = False
        Dim intID As Integer = 1
        '27/09/19 Se pone en comentario la siguiente funcion porque devolvia el numero vacio en la familia
        Do While bolFin = False

            If colP(strFamilia).Exists(Function(p)
                                           Return p.ELEMENTO = intID.ToString()
                                       End Function) = False And Not ExisteEnArrayProxiesEliminados(strFamilia & "." & intID.ToString()) Then
                bolFin = True
            Else
                intID = intID + 1
            End If
        Loop

        ''Devuelve el ultimo id de la familia + 1, sin tener los eliminados entre medio
        'intID = colP(strFamilia).Max(Function(p)
        '                                 Convert.ToInt32(p.ELEMENTO)
        '                             End Function) + 1

        'Fin 27/09/19
        Return intID.ToString()
    End Function

    Public Function colP_BuscaFamiliaLibre() As String
        Dim bolFin As Boolean = False
        Dim intFamilia As Integer = 1
        'Busca la primera Familia libre
        Do While bolFin = False

            If colP.ContainsKey(intFamilia.ToString()) = False Then
                bolFin = True
            Else
                intFamilia = intFamilia + 1
            End If

        Loop
        'Como ya no hay familia, devuelve siempre 1
        'Return "1" '27/09/19 se pone en comentario
        Return intFamilia '27/09/19 se pone en comentario

    End Function

    Public Function colP_ExisteElemento(strElemento As String)
        Dim arrayElemento() As String = strElemento.Split(".")

        If colP.ContainsKey(arrayElemento(0)) = True AndAlso colP(arrayElemento(0)).Exists(Function(p)
                                                                                               Return p.ELEMENTO = arrayElemento(1)

                                                                                           End Function) = True Then

            Return True
        Else
            Return False
        End If
    End Function

    Public Sub colP_AddElemento(strElemento As String, oMl As AcadMLeader)
        Dim arrayElemento() As String = strElemento.Split(".")
        Dim clsP As New clsProxyML(oMl.ObjectID)

        If colP.ContainsKey(arrayElemento(0)) = False Then
            'No existe la familia
            Dim ListClsP As New List(Of clsProxyML)
            ListClsP.Add(clsP)
            colP.Add(arrayElemento(0), ListClsP)
        Else
            'Existe la familia
            colP(arrayElemento(0)).Add(clsP)

        End If


    End Sub

    Public Sub colP_DeleteElemento(strElemento As String)
        Dim arrayElemento() As String = strElemento.Split(".")

        If colP.ContainsKey(arrayElemento(0)) Then
            'Elimina el proxy de la familia

            Dim intIndex = colP(arrayElemento(0)).FindIndex(Function(p)
                                                                Return p.ELEMENTO = arrayElemento(1)
                                                            End Function)
            If intIndex >= 0 Then

                colP(arrayElemento(0)).RemoveAt(intIndex)

                If colP(arrayElemento(0)).Count = 0 Then
                    colP.Remove(arrayElemento(0)) 'Elimina Familia si ya no tiene proxys asignados
                End If
            End If


        End If


    End Sub

    Public Function colP_BuscaFamiliaPorObjectId(lngObjecId As Long) As String
        For Each oFamily In colP
            If oFamily.Value.Exists(Function(x)
                                        Return x.oMl.ObjectID = lngObjecId
                                    End Function) Then
                Return oFamily.Key
                Exit For
            End If
        Next
        Return ""
    End Function

    Public Function colP_BuscaProxyPorElemento(strElemento As String) As clsProxyML
        Dim arrayElemento() As String = strElemento.Split(".")
        Dim oResult As clsProxyML = Nothing

        If colP.ContainsKey(arrayElemento(0)) Then

            'oResult = colP(arrayElemento(0)).Find(Function(x)
            '                                          x.ELEMENTO = arrayElemento(1)
            '                                      End Function)

            For Each iProxyML In colP(arrayElemento(0))
                If iProxyML.ELEMENTO = arrayElemento(1) Then
                    oResult = iProxyML
                    Exit For
                End If

            Next

        End If

        Return oResult

    End Function

    Public Sub ReseteaXDataIncorrectos()
        'listIdToReset contiene el objectid de todos los blockreference a quitar xdata ELEMENTO pq se han añadido fuera de la aplicacion.
        app_procesointerno = True
        For Each oId As ObjectId In listIdToReset
            Dim oBlkRef As BlockReference = clsA.BlockReference_Get(oId)
            If Not oBlkRef Is Nothing Then
                Try
                    'Mira si tiene el xdata Elemento, si lo tiene lo modifica, y si no lo tiene, salta la Exception
                    clsA.XLeeDato(CType(oBlkRef.AcadObject, AcadObject).Handle, "ELEMENTO")
                    'Resetea valor
                    clsA.XPonDato(CType(oBlkRef.AcadObject, AcadObject).Handle, "ELEMENTO", "")
                Catch ex As Exception

                End Try

            End If

        Next
        If listIdToReset.Count > 0 Then
            colP_ProxiesRellena()
        End If
        listIdToReset.Clear()

        app_procesointerno = False
    End Sub

    Public Sub ActualizaProxyIncorrectos()
        app_procesointerno = True
        Dim ent As Entity = Nothing
        For Each item As KeyValuePair(Of ObjectId, String) In ProxyToUpdate
            ent = clsA.Entity_Get(item.Key)
            If Not ent Is Nothing Then
                Dim oMl As AcadMLeader = CType(ent.AcadObject, AcadMLeader)
                'Con el valor antiguo de ELEMENTO, busca el blockreference asociado para actualizar su XDATA
                Dim strElementoEntityOld As String = clsA.XLeeDato(oMl.Handle, "ELEMENTO")
                Dim arrayProxy As ArrayList = clsA.DameBloquesTODOS_XData("ELEMENTO", strElementoEntityOld)
                If arrayProxy.Count > 0 Then
                    'En principio solo tiene que encontrar un blockReference
                    'Actualiza el XDATA del blockReference
                    clsA.XPonDato(CType(arrayProxy(0), AcadBlockReference).Handle, "ELEMENTO", item.Value)
                End If
                'Actualiza el atributo del Mleader
                clsA.MLeaderBlock_PonValorAtributo(oMl, "ELEMENTO", item.Value.Split(".")(1))
                'Actualiza el Xdata del Mleader
                clsA.XPonDato(oMl.Handle, "ELEMENTO", item.Value)  ', regAPPCliente)

            End If
        Next
        If ProxyToUpdate.Count > 0 Then
            colP_ProxiesRellena()
            ElementoProxyRecomendado = RecomiendaElementoLibre(ent)

        End If
        ProxyToUpdate.Clear()

        app_procesointerno = False
    End Sub

    Public Sub colP_ProxiesRellena()
        Dim arrMl As ArrayList = clsA.MleaderDameTodos_PorNombreBloque("BloqueProxy")
        colP.Clear()
        If arrMl IsNot Nothing AndAlso arrMl.Count > 0 Then

            For Each oMl As AcadMLeader In arrMl

                Dim clsP As New clsProxyML(oMl.ObjectID)

                'Elemento esta formado por Familia.Id
                'arrayElemento(0) es Familia
                'arrayElemento(1) es Id

                'Dim arrayElemento() As String = clsP.ELEMENTO.Split(".")
                Dim arrayElemento() As String = clsA.XLeeDato(oMl.Handle, "ELEMENTO").Split(".")

                If colP.ContainsKey(arrayElemento(0)) = True Then
                    colP(arrayElemento(0)).Add(clsP)
                ElseIf colP.ContainsKey(arrayElemento(0)) = False Then
                    Dim ListClsP As New List(Of clsProxyML)
                    ListClsP.Add(clsP)
                    colP.Add(arrayElemento(0), ListClsP)
                End If
                clsP = Nothing
            Next

        End If
    End Sub

    Public Sub ActualizaArrayProxiesEliminados(strElementoEliminadoToADD As String)
        Dim strTempEliminados = clsA.PropiedadCustomDocumento_Lee("ElementosProxiesEliminados")
        strTempEliminados = strTempEliminados & "·" & strElementoEliminadoToADD
        'Si no existe, la crea
        clsA.PropiedadCustomDocumento_Escribe("ElementosProxiesEliminados", strTempEliminados, True)
        arrayProxiesEliminados = clsA.PropiedadCustomDocumento_Lee("ElementosProxiesEliminados").Split("·")
    End Sub

    Public Function ExisteEnArrayProxiesEliminados(strElementoEliminadoToADD As String) As Boolean
        Dim bolResult As Boolean = False
        If Not arrayProxiesEliminados Is Nothing Then
            For intPos As Integer = 0 To arrayProxiesEliminados.Length - 1
                If arrayProxiesEliminados(intPos) = strElementoEliminadoToADD Then
                    bolResult = True
                    Exit For

                End If
            Next
        End If

        Return bolResult
    End Function

    Public Function RecomiendaElementoLibre(Optional oEntity As Entity = Nothing) As String

        Dim strResult As String = ""

        If oEntity <> Nothing Then
            If TypeOf oEntity Is MLeader Then
                Dim oMl As AcadMLeader = Nothing

                oMl = CType(oEntity, MLeader).AcadObject
                'Obtiene familia, porque el usuario ha indicado que siga con la misma del ballon/proxy seleccionado
                ' Busca el primer ID vacío o el ultimo el cual se muestra en el formulario.
                'Dim strElemento = clsA.MLeaderBlock_DameValorAtributo(oEntity.AcadObject, "ELEMENTO")
                Dim strElemento = clsA.XLeeDato(oEntity.AcadObject, "ELEMENTO") ', regAPPCliente)
                Dim arrayElemento() As String = strElemento.Split(".")

                Dim strID As String = colP_BuscaIDLibreEnFamilia(arrayElemento(0))

                strResult = arrayElemento(0) & "." & strID

            ElseIf TypeOf oEntity Is BlockReference Then
                'Mira si este blockreference tiene un Id proxy asociado
                Dim strElementoEntity As String

                Try
                    'strElementoEntity = clsA.BloqueAtributoDame(CType(oEntity.AcadObject, AcadBlockReference).ObjectID, "ELEMENTO")
                    strElementoEntity = clsA.XLeeDato(CType(oEntity.AcadObject, AcadBlockReference).ObjectID, "ELEMENTO")
                Catch ex As Exception
                    strElementoEntity = "-1" 'Ha seleccionado un blockReference que no tiene atributo ELEMENTO y por lo tanto no se le puede asignar un proxy
                End Try

                If (strElementoEntity = "") Then
                    'Si no tiene, genera nueva familia y el ID/Elemento empieza por 1
                    Dim strFamilia As String = colP_BuscaFamiliaLibre()
                    strResult = strFamilia & ".1"
                ElseIf strElementoEntity <> "-1" Then
                    'Si tiene, mantiene familia y busca el primer ID vacío o el ultimo el cual se muestra en el formulario.                    
                    Dim arrayElemento() As String = strElementoEntity.Split(".")
                    Dim strID As String = colP_BuscaIDLibreEnFamilia(arrayElemento(0))
                    strResult = arrayElemento(0) & "." & strID
                End If

            End If

        Else
            'No se ha pasado ningun objeto 
            Dim strFamilia As String = colP_BuscaFamiliaLibre()
            strResult = strFamilia & ".1"
        End If
        Return strResult
    End Function

    Public Sub InsertaNumeracion()
        If ElementoProxyRecomendado Is Nothing Then ElementoProxyRecomendado = RecomiendaElementoLibre()
        app_procesointerno = True
        If ElementoProxyRecomendado.Contains(".") = False Then
            MsgBox("Numeración con Formato Incorrecto", MsgBoxStyle.Critical, "AVISOS AL USUARIO")
            Exit Sub
        End If

        If colP_ExisteElemento(ElementoProxyRecomendado) Then
            MsgBox("La Numeración Indicada ya existe en un Proxy", MsgBoxStyle.Critical, "AVISOS AL USUARIO")
            Exit Sub
        End If

        If ExisteEnArrayProxiesEliminados(ElementoProxyRecomendado) Then
            MsgBox("La Numeración Indicada fue eliminada", MsgBoxStyle.Critical, "AVISOS AL USUARIO")
            Exit Sub
        End If


        If clsA.oAppA.ActiveDocument.ActiveSpace = Autodesk.AutoCAD.Interop.Common.AcActiveSpace.acPaperSpace Then
            MsgBox("Proxy sólo se puede insertar en Espacio Modelo", MsgBoxStyle.Critical, "AVISOS AL USUARIO")
            Exit Sub
        End If

        '

        ' Cargar recursos
        clsA.Clona_TodoDesdeDWGInsertando(BloqueRecursos, True, True)

        Try
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CMLEADERSTYLE", regAPPCliente)
        Catch ex As Exception
            MsgBox("No existe el estilo " & regAPPCliente)
            Exit Sub
        End Try
        ' Activar la capa 'proxy' y quitar el contorno de cobertura
        clsA.CapaActiva(capaproxy)
        clsA.CoberturaOnOff(False)

        Dim bolFin As Boolean = False
        Do While bolFin = False
            'Filtro para buscar a que BlockReference asociar el proxy
            Dim arrayFilterType() As Type = {GetType(BlockReference)}
            Dim oIdSel As ObjectId = clsA.ObjectId_SelectOne(OpenMode.ForRead, arrayFilterType)

            If oIdSel <> Nothing Then
                Dim oEntity As Entity = clsA.Entity_Get(oIdSel)
                'Mira si el blockReference seleccionado tiene el atributo ELEMENTO y que esta vacía para osociarle uno nuevo.
                Dim strElementoEntity As String
                Try
                    'strElementoEntity = clsA.BloqueAtributoDame(CType(oEntity.AcadObject, AcadBlockReference).ObjectID, "ELEMENTO")
                    strElementoEntity = clsA.XLeeDato(CType(oEntity.AcadObject, AcadBlockReference).Handle, "ELEMENTO")
                Catch ex As Exception
                    strElementoEntity = "-1" 'Ha seleccionado un blockReference que no tiene atributo ELEMENTO y por lo tanto no se le puede asignar un proxy
                End Try

                If (strElementoEntity = "") Then
                    Dim oMl As AcadMLeader = Nothing
                    oMl = clsA.MLeader_InsertaCommand()
                    If oMl IsNot Nothing Then

                        'Actualiza el proxy con el Elemento (Familia.ID)
                        clsA.MLeaderBlock_PonValorAtributo(oMl, "ELEMENTO", ElementoProxyRecomendado.Split(".")(1))
                        clsA.XPonDato(oMl.Handle, "ELEMENTO", ElementoProxyRecomendado)    ', regAPPCliente)

                        colP_AddElemento(ElementoProxyRecomendado, oMl)

                        'clsA.BloqueAtributoEscribe(CType(oEntity.AcadObject, AcadBlockReference).ObjectID, "ELEMENTO", ElementoProxyRecomendado)
                        clsA.XPonDato(CType(oEntity.AcadObject, AcadBlockReference).Handle, "ELEMENTO", ElementoProxyRecomendado)  ', regAPPCliente)

                        ElementoProxyRecomendado = RecomiendaElementoLibre(oEntity)
                    End If
                ElseIf (strElementoEntity = "-1") Then
                    MsgBox("Error al leer XData del BlockReference")
                Else
                    MsgBox("El BlockReference seleccionado ya dispone del proxy: " & strElementoEntity.Split(".")(1))
                End If
            Else
                bolFin = True
            End If
        Loop
        app_procesointerno = False

    End Sub


#End Region
End Module
