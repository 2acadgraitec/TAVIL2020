Imports Autodesk.AutoCAD.Interop.Common
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.ApplicationServices
Imports TAVIL2020.TAVIL2020
Imports System.Windows.Forms

Public Class frmPatas
#Region "FORMULARIO"
    Public largo As Double = Nothing
    Public ancho As Double = Nothing
    Private Sub frmPatas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        app_procesointerno = True
        oBlR = Nothing
        Me.Text = "PATAS TRANSPORTADORES - v" & app_version
        cbPatas.Items.AddRange(arrBloquesPatas)
        'cbPatas.SelectedIndex = 0
        '
        btnSelCinta.ForeColor = Drawing.Color.Red
        lblBloque.ForeColor = Drawing.Color.Red
        lblNPatas.ForeColor = Drawing.Color.Red
        btnInsertaPata.ForeColor = Drawing.Color.Red
        oBlR = Nothing
        '
        'AddHandler MyPlugin.DBObjectModified, AddressOf Me.MyPlugin_DBObjectModified
        'AddHandler MyPlugin.DBObjectErased, AddressOf Me.MyPlugin_ObjectErased
        'AddHandler MyPlugin.DBObjectAppended, AddressOf Me.MyPlugin_DBObjectAppended
        'AddHandler MyPlugin.AppIdle, AddressOf Me.MyPlugin_AppIdle
    End Sub
    Private Sub frmPatas_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        app_procesointerno = False
        oBlR = Nothing
        frmPa = Nothing
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
#End Region

#Region "EVENTOS"

    Private Sub MyPlugin_AppIdle(ByVal sender As Object, ByVal e As EventArgs)
        If (app_procesointerno = False) Then
            'ReseteaXDataIncorrectos()
            'ActualizaProxyIncorrectos()
        End If
    End Sub
    '
    Private Sub MyPlugin_DBObjectModified(sender As Object, e As Autodesk.AutoCAD.DatabaseServices.ObjectEventArgs)
        If (app_procesointerno = False) Then
            'If TypeOf e.DBObject Is MLeader Then

            '    Dim oMLeader As MLeader = CType(e.DBObject, MLeader)
            '    Dim attRef As AttributeReference = clsA.AttributeReference_Get_FromMLeader(oMLeader.Id, "ELEMENTO", OpenMode.ForWrite, False)

            '    If Not attRef Is Nothing Then
            '        'Obtiene los valores nuevos (AttributeReference) y antiguos(xdata)
            '        Dim strElementoEntityNew As String = attRef.TextString
            '        Dim strElementoEntityOld As String = clsA.XLeeDato(oMLeader.AcadObject, "ELEMENTO", clsA.regAPP)
            '        'Mira si el formato es correcto
            '        If strElementoEntityNew.Contains(".") AndAlso strElementoEntityNew.Split(".").Length = 2 Then
            '            'Comprueba si ha cambiado el valor
            '            If strElementoEntityNew <> strElementoEntityOld Then
            '                If Not ExisteElemento(strElementoEntityNew) Then
            '                    Dim arrayElementoNew() As String = strElementoEntityNew.Split(".")
            '                    Dim arrayElementoOld() As String = strElementoEntityOld.Split(".")
            '                    'Mira si ha cambiado de familia. Si ha cambiado, pregunta al usuario si desea continuar.
            '                    If arrayElementoOld(0) <> arrayElementoNew(0) Then
            '                        If Not ProxyToUpdate.ContainsKey(oMLeader.Id) Then
            '                            If MessageBox.Show("¿Desea cambiar la familia al Proxy?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = vbYes Then
            '                                ProxyToUpdate.Add(oMLeader.Id, strElementoEntityNew)

            '                            End If
            '                        End If

            '                    Else
            '                        If Not ProxyToUpdate.ContainsKey(oMLeader.Id) Then
            '                            ProxyToUpdate.Add(oMLeader.Id, strElementoEntityNew)
            '                        End If

            '                    End If

            '                Else

            '                    If Not ProxyToUpdate.ContainsKey(oMLeader.Id) Then
            '                        ProxyToUpdate.Add(oMLeader.Id, strElementoEntityOld)
            '                        MessageBox.Show("El identificador que se quiere asociar está asociado a otro Proxy. Se cargará al valor anterior.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                    End If

            '                End If
            '            End If
            '        Else
            '            If Not ProxyToUpdate.ContainsKey(oMLeader.Id) Then
            '                ProxyToUpdate.Add(oMLeader.Id, strElementoEntityOld)
            '                MessageBox.Show("El identificador que se quiere asociar tiene formato incorrecto. Se cargará al valor anterior.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '            End If
            '        End If


            '    End If

            'End If

        End If

    End Sub

    Private Sub MyPlugin_DBObjectAppended(sender As Object, e As ObjectEventArgs)
        'Cuando se inserta mediante este plugin, no hace nada. Es solo para los casos que el usuario realiza insercciones.
        Dim strElementoEntity As String = ""
        If (app_procesointerno = False) Then
            'If TypeOf e.DBObject Is BlockReference Then

            '    listIdToReset.Add(e.DBObject.Id)
            'ElseIf TypeOf e.DBObject Is MLeader Then
            '    'Mira si el Mleader añadido es basado en una de la aplicacion, para ello, mira si tiene el atributo ELEMENTO

            '    Try
            '        'Hay que acceder con Active X a la base de datos porque con ACAD da error al estar eliminado el objeto
            '        ' Cuando el usuario copia y pega, Autocad genera un blockreference temporal que es diferente al que se inserta y es visible en el documento
            '        ' por eso se pasa el parametro BlkRefIsErased a true ya que esta erased. Si no daría error
            '        strElementoEntity = clsA.AttributeReference_Get_FromMLeader(e.DBObject.Id, "ELEMENTO", OpenMode.ForRead, True).TextString
            '    Catch ex As Exception
            '        strElementoEntity = "-1" 'Ha seleccionado un Mleader que no tiene atributo ELEMENTO
            '    End Try

            '    If strElementoEntity <> "" And strElementoEntity <> "-1" Then
            '        'Es un proxy añadido fuera de aplicacion, por lo tanto, lo elimina.
            '        MessageBox.Show("Debe añadir el Proxy a través de la aplicación. Se cancelará el comando.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        app_procesointerno = True
            '        clsA.oAppA.ActiveDocument.SendCommand(Chr(27)) 'Manda la tecla escape para cancelar el comando actual.
            '        'CType(e.DBObject.AcadObject, AcadMLeader).Delete()
            '        app_procesointerno = False
            '    End If


            'End If
        End If

    End Sub

    Private Sub MyPlugin_ObjectErased(ByVal sender As Object, ByVal e As ObjectErasedEventArgs)
        Dim strElementoEntity As String = ""

        If (app_procesointerno = False) Then
            'If TypeOf e.DBObject Is BlockReference Then

            '    Try
            '        'Hay que acceder con Active X a la base de datos porque con ACAD da error al estar eliminado el objeto
            '        'strElementoEntity = clsA.BloqueAtributoDame(CType(ent.AcadObject, AcadBlockReference).ObjectID, "ELEMENTO")
            '        'strElementoEntity = clsA.AttributeReference_Get(e.DBObject.Id, "ELEMENTO", OpenMode.ForRead, True).TextString
            '        strElementoEntity = clsA.XLeeDato(e.DBObject.Id, "ELEMENTO", True)
            '    Catch ex As Exception
            '        strElementoEntity = "-1" 'Ha seleccionado un blockReference que no tiene atributo ELEMENTO y por lo tanto no se le puede asignar un proxy
            '    End Try


            '    If strElementoEntity <> "" And strElementoEntity <> "-1" Then
            '        'Esta eliminando un blockreference que tiene asignado un Elemento proxy
            '        'If colP.ContainsKey(strElementoEntity) Then
            '        '    colP.Remove(strElementoEntity) ' Elimina del diccionario el elemento borrado
            '        'End If

            '        Dim arrayProxy As ArrayList = clsA.MleaderDameTodos_PorAtributo("ELEMENTO", strElementoEntity, True)


            '        If arrayProxy.Count > 0 Then
            '            'En principio solo tiene que encontrar uno, porque cada Mleader tiene un unico elemento
            '            DeleteElemento(strElementoEntity)
            '            CType(arrayProxy(0), AcadMLeader).Delete()

            '        End If

            '    End If

            '    'End If

            'ElseIf TypeOf e.DBObject Is MLeader Then


            '    Try
            '        'Hay que acceder con Active X a la base de datos porque con ACAD da error al estar eliminado el objeto
            '        strElementoEntity = clsA.AttributeReference_Get_FromMLeader(e.DBObject.Id, "ELEMENTO", OpenMode.ForRead, True).TextString
            '    Catch ex As Exception
            '        strElementoEntity = "-1" 'Ha seleccionado un Mleader que no tiene atributo ELEMENTO
            '    End Try

            '    If strElementoEntity <> "" And strElementoEntity <> "-1" Then
            '        'Dim arrayProxy As ArrayList = clsA.BloquesDameTodos_PorAtributo("ELEMENTO", strElementoEntity, True)
            '        Dim arrayProxy As ArrayList = clsA.DameBloquesTODOS_XData("ELEMENTO", strElementoEntity)

            '        If arrayProxy.Count > 0 Then
            '            'En principio solo tiene que encontrar uno
            '            'clsA.BloqueAtributoEscribe(CType(arrayProxy(0), AcadBlockReference).ObjectID, "ELEMENTO", "") 'Lo pone vacio porque se elimina el proxy.
            '            clsA.XPonDato(CType(arrayProxy(0), AcadBlockReference), "ELEMENTO", "")
            '        End If

            '        'If colP.ContainsKey(strElementoEntity) Then
            '        '    colP.Remove(strElementoEntity)
            '        'End If
            '        DeleteElemento(strElementoEntity)
            '    End If

            'End If
        End If

    End Sub

#End Region

#Region "BOTONES"
    Private Sub btnSelCinta_Click(sender As Object, e As EventArgs) Handles btnSelCinta.Click
        'If clsA.oAppA.ActiveDocument.ActiveSpace = Autodesk.AutoCAD.Interop.Common.AcActiveSpace.acPaperSpace Then
        '    MsgBox("Proxy sólo puede insertarse en Espacio Modelo", MsgBoxStyle.Critical, "AVISOS AL USUARIO")
        '    Exit Sub
        'End If
        '
        Me.Visible = False
        ' Cargar recursos
        clsA.ClonaTODODesdeDWG(BloqueRecursos)

        Try
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CMLEADERSTYLE", fijoCliente)
        Catch ex As Exception
            MsgBox("No existe el estilo " & fijoCliente)
            Exit Sub
        End Try
        ' Activar la capa 'proxy' y quitar el contorno de cobertura
        clsA.CapaActiva(capaproxy)
        clsA.CoberturaOnOff(False)
        ' Seleccionar el bloque

        oBlR = clsA.BloqueSeleccionaDame()
        '
        If oBlR Is Nothing Then
            MsgBox("No ha seleccionado ningún bloque...")
            Exit Sub
            'ElseIf cbPatas.Text <> "" Then
            '    btnInsertaPata.Enabled = True
            btnSelCinta.ForeColor = Drawing.Color.Red
        Else
            btnSelCinta.ForeColor = Drawing.Color.Black
            clsA.XPonDato(oBlR, "tipo", "cinta")
        End If
        '
        txtL1.Text = ""
        txtA1.Text = ""
        Dim oPars As Hashtable = clsA.BloqueDinamicoParametrosDameTodos(oBlr.ObjectID)
        Dim mensaje As String = ""
        For Each nPar As String In oPars.Keys
            mensaje &= nPar & " = " & oPars(nPar) & vbCrLf
            If nPar = "DISTANCIA1" Or nPar = "DISTANCE1" Or nPar = "LENGTH" Then
                txtL1.Text = oPars(nPar)
                largo = CDbl(oPars(nPar))
            ElseIf nPar = "DISTANCIA2" Or nPar = "DISTANCE2" Or nPar = "WIDTH" Then
                txtA1.Text = oPars(nPar)
                ancho = CDbl(oPars(nPar))
            End If
        Next
        ' MsgBox(oBlr.EffectiveName)
        ' MsgBox(mensaje)
        '
        Me.Visible = True
    End Sub

    Private Sub cbPatas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbPatas.SelectedIndexChanged
        If cbPatas.Text = "" Then
            lblBloque.ForeColor = Drawing.Color.Red
        Else
            lblBloque.ForeColor = Drawing.Color.Black
            Dim fullBloque As String = IO.Path.Combine(dirPatas, cbPatas.Text & ".dwg")
            'Dim fullBloquePNG As String = IO.Path.ChangeExtension(fullBloque, ".png")
            If IO.File.Exists(fullBloque) Then
                Try
                    If clsA Is Nothing Then clsA = New AutoCAD2acad.clsAutoCAD2acad(oApp, app_fullPath, regAPPCliente)
                    'If IO.File.Exists(fullBloquePNG) Then
                    'pbBloque.Image = Drawing.Image.FromFile(fullBloquePNG)
                    'Else
                    pbBloque.Image = clsA.DameBitmapDWG_IO(fullBloque)
                    'End If
                Catch ex As Exception
                    pbBloque.Image = Nothing
                End Try
            End If
        End If
    End Sub

    Private Sub cbNPatas_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles cbNPatas.SelectedIndexChanged
        If cbNPatas.Text = "" Then
            lblNPatas.ForeColor = Drawing.Color.Red
        Else
            lblNPatas.ForeColor = Drawing.Color.Black
        End If
    End Sub

    Private Sub btnInsertaPata_Click(sender As Object, e As EventArgs) Handles btnInsertaPata.Click
        If oBlR Is Nothing Then Exit Sub
        Dim fullPata As String = IO.Path.Combine(dirPatas, cbPatas.Text & ".dwg")
        If IO.File.Exists(fullPata) = False Then
            MsgBox("No existe el bloque de pata a insertar desde:" & vbCrLf & fullPata)
            Exit Sub
        End If
        '
        If clsA Is Nothing Then clsA = New AutoCAD2acad.clsAutoCAD2acad(oApp, app_fullPath, regAPPCliente)
        Dim cuantas As Integer = IIf(IsNumeric(cbNPatas.Text), CInt(cbNPatas.Text), 0)
        PonPatasEnCinta(fullPata, cuantas)
        'Dim oBlPara As AcadBlockReference = clsA.BloqueInserta(fullPata, 1)
        ' Insertar las 2 patas en la cinta transportadora oBlR
        '
        ' Pata 1
        'Dim oPt1 As Object = oBlR.InsertionPoint
        'Dim oBlPata1 As AcadBlockReference = oApp.ActiveDocument.ModelSpace.InsertBlock(oPt1, fullPata, 1, 1, 1, 0)
        '' Pata 2
        'Dim oPt2 = New Double() {oPt1(0), oPt1(1) + 200, oPt1(2)}
        'Dim oBlPata2 As AcadBlockReference = oApp.ActiveDocument.ModelSpace.InsertBlock(oPt2, fullPata, 1, 1, 1, 0)
        ''
        'If oBlPata1 IsNot Nothing And oBlPata2 IsNot Nothing Then
        '    Me.OnLoad(Nothing)
        'End If
    End Sub
    '
    Public Sub PonPatasEnCinta(queFullPata As String, cuantas As Integer)
        ' Crear/Poner capa de patas activa
        clsA.CapaCreaActiva(PatasCapa,,,, True)
        '
        If cuantas = 1 Then
            Dim oblPata As AcadBlockReference = clsA.BloqueInserta(queFullPata, oBlR.XScaleFactor)
            oblPata.Rotate(oblPata.InsertionPoint, Utilidades.GraRad(90))
            oblPata.Rotate(oblPata.InsertionPoint, oBlR.Rotation)
            clsA.XPonDato(oblPata, "cinta", oBlR.Handle)
            clsA.XPonDato(oblPata, "tipo", "pata")
            If oblPata.Layer <> PatasCapa Then oblPata.Layer = PatasCapa
            clsA.BloqueDinamicoParametroEscribe(oblPata.ObjectID, "WIDTH", CDbl(ancho)) ' ancho.ToString)
            oblPata.Update()
            Exit Sub
        End If
        '
        Dim trozoIniFin As Double = 70  '(largo / cuantas) / 2       ' Distancia al inicio y final de las patas 1 y final
        Dim trozo As Double = (largo - (trozoIniFin * 2)) / (cuantas - 1)         ' Distancia entre cada pata
        Dim cXIni As Double = Nothing   ' CDbl(oBlR.InsertionPoint(0)) + trozoIniFin
        If oBlR.XEffectiveScaleFactor > 0 Then
            cXIni = CDbl(oBlR.InsertionPoint(0)) + trozoIniFin
        Else
            cXIni = CDbl(oBlR.InsertionPoint(0)) - trozoIniFin
            trozo = trozo * -1
        End If
        'Dim coordXFin As Double = CDbl(oBlR.InsertionPoint(0)) + largo - trozoIniFin
        '
        ' Insertar la pata
        For x As Integer = 1 To cuantas
            'Dim oPt() As Double = New Double() _
            '    {cXIni,
            '    IIf(oBlR.YEffectiveScaleFactor > 0, oBlR.InsertionPoint(1) + (ancho / 2), oBlR.InsertionPoint(1) - (ancho / 2)),
            '        oBlR.InsertionPoint(2)}
            Dim oPt() As Double = New Double() {cXIni, oBlR.InsertionPoint(1), oBlR.InsertionPoint(2)}
            Dim oBlPata As AcadBlockReference = oApp.ActiveDocument.ModelSpace.InsertBlock(oPt, queFullPata, oBlR.XEffectiveScaleFactor, oBlR.YEffectiveScaleFactor, oBlR.ZEffectiveScaleFactor, Utilidades.GraRad(90))
            'Dim oblPata As AcadBlockReference = clsA.BloqueInserta(queFullPata, oBlR.XScaleFactor)
            oBlPata.Rotate(oBlR.InsertionPoint, oBlR.Rotation)
            clsA.XPonDato(oBlPata, "cinta", oBlR.Handle)
            clsA.XPonDato(oBlPata, "tipo", "pata")
            If oBlPata.Layer <> PatasCapa Then oBlPata.Layer = PatasCapa
            clsA.BloqueDinamicoParametroEscribe(oBlPata.ObjectID, "WIDTH", CDbl(ancho)) ' ancho.ToString)
            oBlPata.Update()
            cXIni += trozo
        Next



        ' Pata 1
        'Dim oPt1 As Object = oBlR.InsertionPoint
        'Dim oBlPata1 As AcadBlockReference = oApp.ActiveDocument.ModelSpace.InsertBlock(oPt1, queFullPata, oBlR.XEffectiveScaleFactor, oBlR.YEffectiveScaleFactor, oBlR.ZEffectiveScaleFactor, 0)
        '' Pata 2
        'Dim oPt2 = New Double() {oPt1(0) + largo, oPt1(1), oPt1(2)}
        'Dim oBlPata2 As AcadBlockReference = oApp.ActiveDocument.ModelSpace.InsertBlock(oPt2, queFullPata, 1, 1, 1, 0)
        ''
        'If oBlPata1 IsNot Nothing Then
        '    clsA.BloqueDinamicoParametroEscribe(oBlPata1.ObjectID, "Visibilidad", ancho)
        '    'oBlPata1.Rotate(oBlPata1.InsertionPoint, oBlR.Rotation)
        '    oBlPata1.Rotate(oBlPata1.InsertionPoint, Utilidades.GraRad(90))
        'End If
        ''
        'If oBlPata2 IsNot Nothing Then
        '    clsA.BloqueDinamicoParametroEscribe(oBlPata2.ObjectID, "Visibilidad", ancho)
        '    oBlPata2.Rotate(oBlPata1.InsertionPoint, Utilidades.GraRad(90))
        'End If
        ''
        'If oBlPata1 IsNot Nothing And oBlPata2 IsNot Nothing Then
        '    oBlPata1.Rotate(oBlR.InsertionPoint, oBlR.Rotation)
        '    oBlPata2.Rotate(oBlR.InsertionPoint, oBlR.Rotation)
        oApp.ActiveDocument.Regen(AcRegenType.acAllViewports)
        Me.OnLoad(Nothing)
        'End If
    End Sub

    Private Sub cbNPatas_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub
#End Region
End Class
