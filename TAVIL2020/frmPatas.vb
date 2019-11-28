Imports Autodesk.AutoCAD.Interop.Common
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.ApplicationServices
Imports TAVIL2020.TAVIL2020
Imports System.Windows.Forms
Imports System.Linq
Imports uau = UtilesAlberto.Utiles
Imports a2 = AutoCAD2acad.A2acad

Public Class frmPatas
#Region "FORMULARIO"
    Public b As clsBloquePataDatos = Nothing
    Public valoresTemp As String = ""
    Private inicio As Boolean = True
    Private ultimoHandle As String = ""
    Private ultimaVista As AcadViewport = Nothing
    '
    Private Sub frmPatas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Eventos.SYSMONVAR(True)
        If clsA Is Nothing Then clsA = New a2.A2acad(Eventos.COMApp, cfg._appFullPath, regAPPCliente)
        If cPT Is Nothing Then cPT = New clsPT
        app_procesointerno = True
        oBlR = Nothing
        Me.Text = "PATAS - v" & cfg._appversion
        'btnActualizar_Click(Nothing, Nothing)
        ultimaVista = Eventos.COMDoc.ActiveViewport
        btnActualizar.Enabled = True
        btnSeleccionar.Enabled = False
        btnContar.Enabled = False
        btnListar.Enabled = True
        gDatos.Enabled = False
    End Sub
    '
    Private Sub frmPatas_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Eventos.SYSMONVAR(False)
        app_procesointerno = False
        If ultimaVista IsNot Nothing Then Eventos.COMDoc().ActiveViewport = ultimaVista
        oBlR = Nothing
        frmPa = Nothing
        lnBloquesPatas = Nothing
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CERRAR_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Cursor.Current = Cursors.WaitCursor
        btnActualizar.BackColor = btnOn
        tvPatas.Nodes.Clear()
        pb1.Value = 0
        ultimoHandle = ""
        pbBloque.Image = Nothing
        Campos_Vacia()
        '
        'Dim lBlo As List(Of AcadBlockReference) = clsA.SeleccionaDameBloquesTODOS_ListAcadBlockReference(, "*_*")
        Dim lBlo As List(Of AcadBlockReference) = clsA.Bloques_DameNombreContiene("PT")
        pb1.Maximum = lBlo.Count
        tvPatas.BeginUpdate()
        '
        If lBlo IsNot Nothing AndAlso lBlo.Count > 0 Then
            For Each oBl As AcadBlockReference In lBlo
                If pb1.Value < pb1.Maximum Then pb1.Value += 1
                Dim b As New clsBloquePataDatos(oBl, soloXXX:=True)
                ' Si sólo queremos los que tengan XXX
                If cbXXX.Checked And b.tieneXXX = False Then Continue For
                If cbPLANTA.Checked AndAlso b.esPlanta = False Then Continue For
                '
                Dim node As New TreeNode()
                node.Text = oBl.EffectiveName
                node.Tag = oBl.Handle   ' oBl
                tvPatas.Nodes.Add(node)
                node = Nothing
                b = Nothing
                System.Windows.Forms.Application.DoEvents()
            Next
            pb1.Value = 0
            tvPatas.Sort()
        End If
        lBlo = Nothing
        tvPatas.EndUpdate()
        lblListadoPatas.Text = "Listado Patas (" & tvPatas.Nodes.Count & ")"
        Cursor = Cursors.Default
        btnSeleccionar.Enabled = (tvPatas.Nodes.Count > 0)
    End Sub
    '
    Private Sub btnSeleccionar_Click(sender As Object, e As EventArgs) Handles btnSeleccionar.Click
        Dim oBl As AcadBlockReference = clsA.Bloque_SeleccionaDame
        If oBl Is Nothing Then Exit Sub
        '
        tvPatas.SelectedNode = Nothing
        For Each oNode As TreeNode In tvPatas.Nodes
            If oNode.Tag.ToString = oBl.Handle Then
                tvPatas.SelectedNode = oNode
                tvPatas_MouseDoubleClick(tvPatas, Nothing)  ' Hacer Zoom en el elemento.
                Exit For
            End If
        Next
    End Sub
    Private Sub btnContar_Click(sender As Object, e As EventArgs) Handles btnContar.Click
        Dim nombres As String() = {tvPatas.SelectedNode.Text}
        Dim lBlo As List(Of AcadBlockReference) = clsA.Bloques_DameNombreContiene(nombres)
        MsgBox(lBlo.Count)
        lBlo = Nothing
    End Sub
    Private Sub btnListar_Click(sender As Object, e As EventArgs) Handles btnListar.Click
        Dim nFile As String = DateTime.Now.ToString("yyyyMMddHHmmss") & "·ListadoPatas.csv"
        Dim nFolder As String = cfg._appfolder & "\LOG"
        Dim fFullPath As String = IO.Path.Combine(nFolder, nFile)
        Dim soloPlanta As Boolean = True
        Dim result As MsgBoxResult = MsgBox("¿Resumen final sólo de bloques de Planta?" & vbCrLf & vbCrLf &
                  "SI" & vbTab & "-->" & vbTab & " Resumen final SOLO bloque de Planta" & vbCrLf &
                  "NO" & vbTab & "-->" & vbTab & "Resumen final de TODOS los bloques" & vbCrLf &
                  "CANCELAR" & vbTab & "-->" & vbTab & "Cancelar impresión", MsgBoxStyle.YesNoCancel, "RESUMEN FINAL")
        If result = MsgBoxResult.Cancel Then
            Exit Sub
        ElseIf result = MsgBoxResult.No Then
            soloPlanta = False
        Else
            soloPlanta = True
        End If
        '
        Try
            If IO.Directory.Exists(nFolder) = False Then
                IO.Directory.CreateDirectory(nFolder)
            End If
        Catch ex As Exception
            fFullPath = IO.Path.Combine(cfg._appfolder, nFile)
        End Try
        Dim cabecera As String = "BLOQUE;CANTIDAD;CODE/DIRECT.;WIDTH;RADIUS;HEIGHT" & vbCrLf
        Dim mensaje As String = ""
        Dim cabecera1 As String = vbCrLf & vbCrLf & vbCrLf & "RESUMEN DE BLOQUES DE PATAS" & IIf(soloPlanta = True, " (SOLO PLANTA)", "") & ";;;;;" & vbCrLf & "ITEM_NUMBER;CANTIDAD;;;;" & vbCrLf
        Dim mensaje1 As String = ""
        '
        Cursor.Current = Cursors.WaitCursor
        pb1.Value = 0
        ultimoHandle = ""
        tvPatas.SelectedNode = Nothing
        Dim lBlo As List(Of AcadBlockReference) = clsA.Bloques_DameNombreContiene("PT") 'clsA.SeleccionaDameBloquesTODOS_ListAcadBlockReference(, "PT*")
        pb1.Maximum = lBlo.Count
        '
        Dim colData As New Dictionary(Of String, String())          ' Listado por elementos únicos
        Dim colDataResumen As New Dictionary(Of String, Object())    ' Listado resumen por nombre bloque
        For Each oBl As AcadBlockReference In lBlo
            If pb1.Value < pb1.Maximum Then pb1.Value += 1
            Dim b As New clsBloquePataDatos(oBl)
            ' Si sólo queremos los que tengan XXX
            'If cbXXX.Checked And b.tieneXXX = False Then Continue For
            'If cbPLANTA.Checked AndAlso (b._EFFECTIVENAME.Contains("SVIEW") OrElse b._EFFECTIVENAME.Contains("ALÇAT2")) Then Continue For
            '
            ' Comprobar valores en Excel, si CODE no contiene XX y si estan vacíos
            If b._CODE.ToUpper.Contains("XX") = False AndAlso b._DIRECTRIZ.ToUpper.Contains("XX") = False AndAlso b._DIRECTRIZ1.ToUpper.Contains("XX") = False Then
                If b._WIDTH = "" Then b._WIDTH = cPT.Filas_DameValorConCODE(b._CODE, nombreColumnaPT.WIDTH)
                If b._RADIUS = "" Then b._RADIUS = cPT.Filas_DameValorConCODE(b._CODE, nombreColumnaPT.RADIUS)
                If b._HEIGHT = "" Then b._HEIGHT = cPT.Filas_DameValorConCODE(b._CODE, nombreColumnaPT.HEIGHT)
            End If
            b.Pon_ITEM_NUMBER()
            ' Ver si existe en la colección colData (aumentar cantidad) o añadirlo si no existía
            '"BLOQUE;CANTIDAD;CODE;WIDTH/RADIUS;HEIGHT"
            If colData.ContainsKey(b._clave) Then
                colData(b._clave)(1) = colData(b._clave)(1) + 1
            Else
                colData.Add(b._clave, {b._EFFECTIVENAME, 1, b._CODE, b._WIDTH, b._RADIUS, b._HEIGHT})
            End If
            '
            ' ***** Cambiamos a ITEM_NUMBER (Antes era BLOCK) Ver si existe en la colección colDataResumen (aumentar cantidad) o añadirlo si no existía
            '"ITEM_NUMBER;CANTIDAD;;;"
            If colDataResumen.ContainsKey(b._ITEM_NUMBER) Then
                colDataResumen(b._ITEM_NUMBER)(0) += 1
            Else
                colDataResumen.Add(b._ITEM_NUMBER, {1, b.esPlanta})
            End If
            System.Windows.Forms.Application.DoEvents()
        Next
        pb1.Value = 0
        Cursor = Cursors.Default
        '
        ' "BLOQUE;CANTIDAD;CODE;WIDTH;RADIUS;HEIGHT"
        For Each datos As KeyValuePair(Of String, String()) In colData
            mensaje &= String.Join(";", datos.Value) & vbCrLf
        Next
        mensaje = mensaje.Substring(0, mensaje.Length - 1)  ' Quitar el último carácter (El vbCrlf)

        ' "ITEM_NUMBER;CANTIDAD"
        For Each datos As KeyValuePair(Of String, Object()) In colDataResumen
            If soloPlanta = True And datos.Value(1).ToString.ToUpper = "FALSE" Then
                Continue For
            End If
            mensaje1 &= datos.Key & ";" & datos.Value(0) & ";;;;" & vbCrLf
        Next
        If mensaje1.Length > 1 Then
            mensaje1 = mensaje1.Substring(0, mensaje1.Length - 1)  ' Quitar el último carácter (El vbCrlf)
        End If
        '
        IO.File.WriteAllText(fFullPath, cabecera & mensaje & cabecera1 & mensaje1, System.Text.Encoding.UTF8)
        If MsgBox("¿Abrir informe...?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "SOLICITUD DE CONFIRMACION") = MsgBoxResult.Yes Then
            Process.Start(fFullPath)
        End If
    End Sub

    Private Sub cbXXX_CheckedChanged(sender As Object, e As EventArgs) Handles cbXXX.CheckedChanged, cbPLANTA.CheckedChanged
        tvPatas.Nodes.Clear()
        btnSeleccionar.Enabled = False
        btnContar.Enabled = False
        gDatos.Enabled = False
        btnActualizar.BackColor = btnOff    ' Drawing.Color.Red
    End Sub

    Private Sub tvPatas_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tvPatas.AfterSelect
        b = Nothing
        pbBloque.Image = Nothing
        gDatos.Enabled = False
        Campos_Vacia()
        btnSeleccionar.Enabled = False
        btnContar.Enabled = False
        gDatos.Enabled = False
        Eventos.COMDoc().ActiveSelectionSet.Clear()
        '
        If tvPatas.SelectedNode Is Nothing Then
            btnSeleccionar.Enabled = (tvPatas.Nodes.Count > 0)
            Exit Sub
        Else
            tvPatas.HideSelection = False
            btnSeleccionar.Enabled = True
            btnContar.Enabled = True
            gDatos.Enabled = True
            tvPatas_MouseDoubleClick(tvPatas, Nothing)      ' Para que haga Zoom en el objeto seleccionado
            ultimoHandle = e.Node.Tag
        End If
        ' Si no tiene tag (Handle del bloque), salir.
        If e.Node.Tag.ToString = "" Then Exit Sub
        inicio = True
        'Dim oIntP As IntPtr = (CType(e.Node.Tag, AcadBlockReference).ObjectID)
        Dim oBlo As Autodesk.AutoCAD.Interop.Common.AcadBlockReference = Eventos.COMDoc().HandleToObject(e.Node.Tag.ToString)
        If oBlo Is Nothing Then
            Exit Sub
        End If
        '
        ' Crear la clase con todos los Atributos/Parametros que necesitamos
        b = New clsBloquePataDatos(oBlo)
        pbBloque.Image = clsA.Imagen_PreviaDeBloque(b._EFFECTIVENAME)
        '
        valoresTemp = ""
        'Dim campos As String() = {"CODE", "DIRECTRIZ", "DIRECTRIZ1", "WIDTH", "RADIUS", "HEIGHT", "STANDARD_PART"}
        '
        ' Rellenar los campos de texto y comboBox
        ' cbWIDTH
        If b.encurva = True Then
            lblWIDTH.Text = "RADIUS :"
            cbWIDTH.Items.AddRange(b._RADIUSs.ToArray)
            cbWIDTH.Text = b._RADIUS
            cbWIDTH.Tag = tipo.ATRIBUTO_RADIUS
        Else
            lblWIDTH.Text = "WIDTH :"
            cbWIDTH.Items.AddRange(b._WIDTHs.ToArray)
            cbWIDTH.Text = b._WIDTH
            cbWIDTH.Tag = tipo.ATRIBUTO_WIDTH
        End If
        cbWIDTH.Enabled = cbWIDTH.Items.Count > 1
        btnWIDTH.Enabled = cbWIDTH.Items.Count > 1
        '
        ' cbHEIGHT
        cbHEIGHT.Items.AddRange(b._HEIGHTs.ToArray)
        cbHEIGHT.Text = b._HEIGHT
        cbHEIGHT.Tag = tipo.ATRIBUTO
        cbHEIGHT.Enabled = True
        ' cbSTANDARD_PART
        cbSTANDARD.Text = b._STANDARD_PART.ToUpper
        cbSTANDARD.Tag = tipo.ATRIBUTO
        cbSTANDARD.Enabled = True
        ' CODE, DIRECTRIZ, DIRECTRIZ1
        txtCODE.Text = b._CODE : txtCODE.Tag = tipo.ATRIBUTO
        txtCODE.Enabled = False
        txtDIRECTRIZ.Text = b._DIRECTRIZ : txtDIRECTRIZ.Tag = tipo.ATRIBUTO : txtDIRECTRIZ.Visible = False
        txtDIRECTRIZ1.Text = b._DIRECTRIZ1 : txtDIRECTRIZ1.Tag = tipo.ATRIBUTO : txtDIRECTRIZ1.Visible = False
        '' AttributeReference
        'For Each nAtt As String In oBlDatos.lAttRef.Keys
        '    If campos.Contains(nAtt.ToUpper) = False Then Continue For
        '    Dim oAtt As AcadAttributeReference = oBlDatos.lAttRef(nAtt)
        '    Dim valor As String = oAtt.TextString
        '    If IsNumeric(valor) And valor <> "" Then
        '        valor = Math.Round(Convert.ToDouble(valor), 0).ToString
        '    End If
        '    Select Case nAtt.ToUpper
        '        Case "WIDTH" : cbWIDTH.Items.AddRange(cPT.Filas_DameColumnaUnicos(nombreColumna.WIDTH).ToArray) : cbWIDTH.Text = valor : cbWIDTH.Tag = tipo.ATRIBUTO_WIDTH : cbWIDTH.Enabled = True
        '        Case "RADIUS" : cbWIDTH.Items.AddRange({valor}) : cbWIDTH.Text = valor : cbWIDTH.Tag = tipo.ATRIBUTO_RADIUS : cbWIDTH.Enabled = False
        '        Case "HEIGHT" : cbHEIGHT.Items.AddRange(cPT.Filas_DameColumnaUnicos(nombreColumna.HEIGHT).ToArray) : cbHEIGHT.Text = valor : cbHEIGHT.Tag = tipo.ATRIBUTO
        '        Case "STANDARD_PART" : cbSTANDARD.Text = valor : cbSTANDARD.Tag = tipo.ATRIBUTO
        '        Case "CODE" : txtCODE.Text = oAtt.TextString : txtCODE.Tag = tipo.ATRIBUTO
        '    End Select
        'Next
        '' DynamicParameters
        'For Each nPro As String In oBlDatos.lDPro.Keys
        '    If campos.Contains(nPro.ToUpper) = False Then Continue For
        '    Dim oPro As AcadDynamicBlockReferenceProperty = oBlDatos.lDPro(nPro)
        '    Dim valor As String = ""
        '    If IsNumeric(oPro.Value) Then
        '        valor = Math.Round(CDbl(oPro.Value), 0).ToString
        '    Else
        '        valor = oPro.Value.ToString
        '    End If
        '    Select Case nPro.ToUpper
        '        Case "WIDTH" : cbWIDTH.Items.AddRange(cPT.Filas_DameColumnaUnicos(nombreColumna.WIDTH).ToArray) : cbWIDTH.Text = valor : cbWIDTH.Tag = tipo.PARAMETRO
        '        'Case "CODE" : txtCODE.Text = valor : txtCODE.Tag = tipo.PARAMETRO
        '        Case "HEIGHT" : cbHEIGHT.Items.AddRange(cPT.Filas_DameColumnaUnicos(nombreColumna.HEIGHT).ToArray) : cbHEIGHT.Text = valor : cbHEIGHT.Tag = tipo.PARAMETRO
        '            'Case "STANDART_PART" : cbSTANDARD.Text = valor
        '    End Select
        'Next
        valoresTemp = DameValoresFormulario()
        inicio = False
        'Controles_PonEstado()
        If valoresTemp.ToUpper.Contains("XX") = True Then
            btnGUARDAR.Enabled = True
        Else
            btnGUARDAR.Enabled = False
        End If
        If cbWIDTH.Text = "" OrElse cbHEIGHT.Text = "" Then
            btnGUARDAR.Enabled = False
        End If
        '
        If b.esPlanta = False Then btnGUARDAR.Enabled = False : gDatos.Enabled = False
        btnSeleccionar.Enabled = (tvPatas.Nodes.Count > 0)
        oBlo = Nothing
    End Sub
    Public Enum tipo
        ATRIBUTO
        ATRIBUTO_WIDTH
        ATRIBUTO_RADIUS
        PARAMETRO
    End Enum
    '
    Private Sub tvPatas_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles tvPatas.MouseDoubleClick
        Dim oNode As TreeNode = CType(sender, TreeView).SelectedNode
        Dim oBlo As Autodesk.AutoCAD.Interop.Common.AcadBlockReference = Eventos.COMDoc().HandleToObject(oNode.Tag.ToString)
        Dim oIPrt As New IntPtr(oBlo.ObjectID)
        Dim oId As New ObjectId(oIPrt)
        Dim arrIds() As ObjectId = {oId}
        Autodesk.AutoCAD.Internal.Utils.SelectObjects(arrIds)
        clsA.HazZoomObjeto(oBlo, 2)
        oBlo = Nothing
    End Sub


    Private Sub cbWIDTH_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbWIDTH.SelectedIndexChanged, cbHEIGHT.SelectedIndexChanged, cbSTANDARD.SelectedIndexChanged
        If inicio = True Then Exit Sub
        gDatos_PonEstado()
    End Sub
    '
    Public Sub gDatos_PonEstado()
        Dim valActual As String = DameValoresFormulario()
        If valoresTemp <> valActual Then
            btnGUARDAR.Enabled = True
            valoresTemp = valActual
        Else
            btnGUARDAR.Enabled = False
        End If

    End Sub
    Private Sub btnSAVE_Click(sender As Object, e As EventArgs) Handles btnGUARDAR.Click
        Dim oNode As TreeNode = tvPatas.SelectedNode
        Dim oBlo As Autodesk.AutoCAD.Interop.Common.AcadBlockReference = Eventos.COMDoc().HandleToObject(oNode.Tag)
        Dim dicNomVal As New Dictionary(Of String, String)
        ' WIDTH/RADIUS
        If cbWIDTH.Text <> "" Then
            If b.widthEsAtt = True Then
                If b.encurva = False Then   ' cbWIDTH.Tag = tipo.ATRIBUTO_WIDTH Then
                    dicNomVal.Add("WIDTH", cbWIDTH.Text)
                Else    'If cbWIDTH.Tag = tipo.ATRIBUTO_RADIUS Then
                    dicNomVal.Add("RADIUS", cbWIDTH.Text)
                End If
            ElseIf b.widthEsAtt = False AndAlso IsNumeric(cbWIDTH.Text) Then    ' Solo si es numérico
                clsA.BloqueDinamico_ParametroEscribe(oBlo.ObjectID, "WIDTH", CDbl(cbWIDTH.Text))
            End If
        End If
        ' HEIGHT
        If cbHEIGHT.Text <> "" AndAlso IsNumeric(cbHEIGHT.Text) Then
            dicNomVal.Add("HEIGHT", cbHEIGHT.Text)
        End If
        ' STANDART_PART
        dicNomVal.Add("STANDART_PART", cbSTANDARD.Text.ToUpper)
        '
        ' CODE, DIRECTRIZ, DIRECTRIZ1
        If cbWIDTH.Text <> "" And cbHEIGHT.Text <> "" Then
            txtCODE.Text = cPT.Filas_DameCODE(b._PREFIJO, cbWIDTH.Text, cbHEIGHT.Text, b.encurva)
            If txtCODE.Text <> "" Then
                txtDIRECTRIZ.Text = txtCODE.Text
                txtDIRECTRIZ1.Text = txtCODE.Text
                dicNomVal.Add("CODE", txtCODE.Text)
                dicNomVal.Add("DIRECTRIZ", txtCODE.Text)
                dicNomVal.Add("DIRECTRIZ1", txtCODE.Text)
            End If
        End If
        ' Escribir todos los atributos
        If dicNomVal.Count > 0 Then
            clsA.Bloque_AtributoEscribe(oBlo.ObjectID, dicNomVal)
        End If
        ' Actualizar bloque para poner datos de tablas
        oBlo.Update()
        Eventos.COMDoc.Regen(AcRegenType.acActiveViewport)
        Eventos.COMDoc.ActiveSelectionSet.Clear()
        '
        btnGUARDAR.Enabled = False
        tvPatas.SelectedNode = Nothing
        tvPatas.Nodes.Remove(oNode)
        valoresTemp = ""
        'If tvPatas.Nodes.Count > 0 Then
        '    tvPatas.SelectedNode = tvPatas.Nodes.Item(0)
        'End If
        Try
            tvPatas_AfterSelect(Nothing, Nothing)
        Catch ex As Exception
            '
        End Try
        lblListadoPatas.Text = "Listado Patas (" & tvPatas.Nodes.Count & ")"
        b = Nothing
    End Sub
    'Private Sub btnSAVE_Click(sender As Object, e As EventArgs) Handles btnGUARDAR.Click
    '    Dim oNode As TreeNode = tvPatas.SelectedNode
    '    Dim oBlo As Autodesk.AutoCAD.Interop.Common.AcadBlockReference = Ev.EvApp.ActiveDocument.HandleToObject(oNode.Tag)
    '    ' Actualizar los datos del bloque
    '    If cbWIDTH.Text <> "" Then
    '        If cbWIDTH.Tag = tipo.ATRIBUTO_WIDTH Then
    '            clsA.Bloque_AtributoEscribe(oBlo.ObjectID, "WIDTH", cbWIDTH.Text)
    '        ElseIf cbWIDTH.Tag = tipo.ATRIBUTO_RADIUS Then
    '            clsA.Bloque_AtributoEscribe(oBlo.ObjectID, "RADIUS", cbWIDTH.Text)
    '        Else
    '            clsA.BloqueDinamico_ParametroEscribe(oBlo.ObjectID, "WIDTH", CDbl(cbWIDTH.Text))
    '        End If
    '    End If
    '    '
    '    If cbHEIGHT.Text <> "" AndAlso IsNumeric(cbHEIGHT.Text) Then
    '        If cbHEIGHT.Tag = tipo.ATRIBUTO Then
    '            clsA.Bloque_AtributoEscribe(oBlo.ObjectID, "HEIGHT", cbHEIGHT.Text)
    '        Else
    '            clsA.BloqueDinamico_ParametroEscribe(oBlo.ObjectID, "HEIGHT", CDbl(cbHEIGHT.Text))
    '        End If
    '    End If
    '    ' Los Atributos
    '    clsA.Bloque_AtributoEscribe(oBlo.ObjectID, "STANDART_PART", cbSTANDARD.Text)
    '    '
    '    ' Código correcto.
    '    If cbWIDTH.Text <> "" And cbHEIGHT.Text <> "" Then
    '        txtCODE.Text = cPT.Filas_DameCODE(oBlo.EffectiveName.Split("_")(0), cbWIDTH.Text, cbHEIGHT.Text, b.encurva)
    '        If txtCODE.Text <> "" Then
    '            txtDIRECTRIZ.Text = txtCODE.Text
    '            txtDIRECTRIZ1.Text = txtCODE.Text
    '            clsA.Bloque_AtributoEscribe(oBlo.ObjectID, "CODE", txtCODE.Text)
    '            clsA.Bloque_AtributoEscribe(oBlo.ObjectID, "DIRECTRIZ", txtCODE.Text)
    '            clsA.Bloque_AtributoEscribe(oBlo.ObjectID, "DIRECTRIZ1", txtCODE.Text)
    '        End If
    '    End If
    '    ' Actualizar bloque para poner datos de tablas
    '    oBlo.Update()
    '    '
    '    btnGUARDAR.Enabled = False
    '    tvPatas.SelectedNode = Nothing
    '    tvPatas.Nodes.Remove(oNode)
    '    valoresTemp = ""
    '    If tvPatas.Nodes.Count > 0 Then
    '        tvPatas.SelectedNode = tvPatas.Nodes.Item(0)
    '    End If
    '    Try
    '        tvPatas_AfterSelect(Nothing, Nothing)
    '    Catch ex As Exception
    '        '
    '    End Try
    'End Sub

    Public Function DameValoresFormulario() As String
        If b IsNot Nothing Then
            txtCODE.Text = cPT.Filas_DameCODE(b._PREFIJO, cbWIDTH.Text, cbHEIGHT.Text, b.encurva)
        End If
        Return cbWIDTH.Text & cbHEIGHT.Text & cbSTANDARD.Text & txtCODE.Text & txtDIRECTRIZ.Text & txtDIRECTRIZ1.Text
    End Function
    '
    Public Sub Campos_Vacia()
        cbWIDTH.Items.Clear() : cbWIDTH.Text = "" : lblWIDTH.Text = "WIDTH/RADIUS :"
        cbHEIGHT.Items.Clear() : cbHEIGHT.Text = ""
        cbSTANDARD.Items.Clear() : cbSTANDARD.Text = "" : cbSTANDARD.Items.AddRange({"SI", "NO"})
        txtCODE.Text = ""
        txtDIRECTRIZ.Text = ""
        txtDIRECTRIZ1.Text = ""
        btnGUARDAR.Enabled = False
    End Sub
    '

    Private Sub btnTODO_Click(sender As Object, e As EventArgs) Handles btnTODO.Click, btnHEIGHT.Click, btnWIDTH.Click
        Dim oBtn As Button = CType(sender, Button)
        Dim name As String = oBtn.Name
        Dim oBl As AcadBlockReference = clsA.Bloque_SeleccionaDame()
        If oBl Is Nothing Then Exit Sub

        Dim b As New clsBloquePataDatos(oBl)
        '
        ' Rellenar los campos activos, con los valores copiados. Según el botón pulsado.
        ' Si no existía en la lista del comboBox, añadirlo antes para que se pueda poner en .Text.
        Select Case name
            Case "btnTODO"
                If b.encurva Then
                    If b._RADIUS <> "" Then
                        'If cbWIDTH.Items.Contains(b._RADIUS) = False Then cbWIDTH.Items.Add(b._RADIUS)
                        If cbWIDTH.Items.Contains(b._RADIUS) = True Then cbWIDTH.Text = b._RADIUS
                    End If
                Else
                    If b._WIDTH <> "" Then
                        'If cbWIDTH.Items.Contains(b._WIDTH) = False Then cbWIDTH.Items.Add(b._WIDTH)
                        If cbWIDTH.Items.Contains(b._WIDTH) = True Then cbWIDTH.Text = b._WIDTH
                    End If
                End If
                If b._HEIGHT <> "" Then
                    'If cbHEIGHT.Items.Contains(b._HEIGHT) = False Then cbHEIGHT.Items.Add(b._HEIGHT)
                    If cbHEIGHT.Items.Contains(b._HEIGHT) = True Then cbHEIGHT.Text = b._HEIGHT
                End If
                If b._STANDARD_PART <> "" Then
                    If cbSTANDARD.Items.Contains(b._STANDARD_PART) = True Then cbSTANDARD.Text = b._STANDARD_PART.ToUpper
                End If
            Case "btnWIDTH"
                If b.encurva Then
                    If b._RADIUS <> "" Then
                        'If cbWIDTH.Items.Contains(b._RADIUS) = False Then cbWIDTH.Items.Add(b._RADIUS)
                        If cbWIDTH.Items.Contains(b._RADIUS) = True Then cbWIDTH.Text = b._RADIUS
                    End If
                Else
                    If b._WIDTH <> "" Then
                        'If cbWIDTH.Items.Contains(b._WIDTH) = False Then cbWIDTH.Items.Add(b._WIDTH)
                        If cbWIDTH.Items.Contains(b._WIDTH) = True Then cbWIDTH.Text = b._WIDTH
                    End If
                End If
            Case "btnHEIGHT"
                If b._HEIGHT <> "" Then
                    'If cbHEIGHT.Items.Contains(b._HEIGHT) = False Then cbHEIGHT.Items.Add(b._HEIGHT)
                    If cbHEIGHT.Items.Contains(b._HEIGHT) = True Then cbHEIGHT.Text = b._HEIGHT
                End If
        End Select
    End Sub
#End Region

    '#Region "EVENTOS"

    '    Private Sub MyPlugin_AppIdle(ByVal sender As Object, ByVal e As EventArgs)
    '        If (app_procesointerno = False) Then
    '            'ReseteaXDataIncorrectos()
    '            'ActualizaProxyIncorrectos()
    '        End If
    '    End Sub
    '    '
    '    Private Sub MyPlugin_DBObjectModified(sender As Object, e As Autodesk.AutoCAD.DatabaseServices.ObjectEventArgs)
    '        If (app_procesointerno = False) Then
    '            'If TypeOf e.DBObject Is MLeader Then

    '            '    Dim oMLeader As MLeader = CType(e.DBObject, MLeader)
    '            '    Dim attRef As AttributeReference = clsA.AttributeReference_Get_FromMLeader(oMLeader.Id, "ELEMENTO", OpenMode.ForWrite, False)

    '            '    If Not attRef Is Nothing Then
    '            '        'Obtiene los valores nuevos (AttributeReference) y antiguos(xdata)
    '            '        Dim strElementoEntityNew As String = attRef.TextString
    '            '        Dim strElementoEntityOld As String = clsA.XLeeDato(oMLeader.AcadObject, "ELEMENTO", clsA.regAPP)
    '            '        'Mira si el formato es correcto
    '            '        If strElementoEntityNew.Contains(".") AndAlso strElementoEntityNew.Split(".").Length = 2 Then
    '            '            'Comprueba si ha cambiado el valor
    '            '            If strElementoEntityNew <> strElementoEntityOld Then
    '            '                If Not ExisteElemento(strElementoEntityNew) Then
    '            '                    Dim arrayElementoNew() As String = strElementoEntityNew.Split(".")
    '            '                    Dim arrayElementoOld() As String = strElementoEntityOld.Split(".")
    '            '                    'Mira si ha cambiado de familia. Si ha cambiado, pregunta al usuario si desea continuar.
    '            '                    If arrayElementoOld(0) <> arrayElementoNew(0) Then
    '            '                        If Not ProxyToUpdate.ContainsKey(oMLeader.Id) Then
    '            '                            If MessageBox.Show("¿Desea cambiar la familia al Proxy?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = vbYes Then
    '            '                                ProxyToUpdate.Add(oMLeader.Id, strElementoEntityNew)

    '            '                            End If
    '            '                        End If

    '            '                    Else
    '            '                        If Not ProxyToUpdate.ContainsKey(oMLeader.Id) Then
    '            '                            ProxyToUpdate.Add(oMLeader.Id, strElementoEntityNew)
    '            '                        End If

    '            '                    End If

    '            '                Else

    '            '                    If Not ProxyToUpdate.ContainsKey(oMLeader.Id) Then
    '            '                        ProxyToUpdate.Add(oMLeader.Id, strElementoEntityOld)
    '            '                        MessageBox.Show("El identificador que se quiere asociar está asociado a otro Proxy. Se cargará al valor anterior.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            '                    End If

    '            '                End If
    '            '            End If
    '            '        Else
    '            '            If Not ProxyToUpdate.ContainsKey(oMLeader.Id) Then
    '            '                ProxyToUpdate.Add(oMLeader.Id, strElementoEntityOld)
    '            '                MessageBox.Show("El identificador que se quiere asociar tiene formato incorrecto. Se cargará al valor anterior.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            '            End If
    '            '        End If


    '            '    End If

    '            'End If

    '        End If

    '    End Sub

    '    Private Sub MyPlugin_DBObjectAppended(sender As Object, e As ObjectEventArgs)
    '        'Cuando se inserta mediante este plugin, no hace nada. Es solo para los casos que el usuario realiza insercciones.
    '        Dim strElementoEntity As String = ""
    '        If (app_procesointerno = False) Then
    '            'If TypeOf e.DBObject Is BlockReference Then

    '            '    listIdToReset.Add(e.DBObject.Id)
    '            'ElseIf TypeOf e.DBObject Is MLeader Then
    '            '    'Mira si el Mleader añadido es basado en una de la aplicacion, para ello, mira si tiene el atributo ELEMENTO

    '            '    Try
    '            '        'Hay que acceder con Active X a la base de datos porque con ACAD da error al estar eliminado el objeto
    '            '        ' Cuando el usuario copia y pega, Autocad genera un blockreference temporal que es diferente al que se inserta y es visible en el documento
    '            '        ' por eso se pasa el parametro BlkRefIsErased a true ya que esta erased. Si no daría error
    '            '        strElementoEntity = clsA.AttributeReference_Get_FromMLeader(e.DBObject.Id, "ELEMENTO", OpenMode.ForRead, True).TextString
    '            '    Catch ex As Exception
    '            '        strElementoEntity = "-1" 'Ha seleccionado un Mleader que no tiene atributo ELEMENTO
    '            '    End Try

    '            '    If strElementoEntity <> "" And strElementoEntity <> "-1" Then
    '            '        'Es un proxy añadido fuera de aplicacion, por lo tanto, lo elimina.
    '            '        MessageBox.Show("Debe añadir el Proxy a través de la aplicación. Se cancelará el comando.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            '        app_procesointerno = True
    '            '        clsA.oAppA.ActiveDocument.SendCommand(Chr(27)) 'Manda la tecla escape para cancelar el comando actual.
    '            '        'CType(e.DBObject.AcadObject, AcadMLeader).Delete()
    '            '        app_procesointerno = False
    '            '    End If


    '            'End If
    '        End If

    '    End Sub

    '    Private Sub MyPlugin_ObjectErased(ByVal sender As Object, ByVal e As ObjectErasedEventArgs)
    '        Dim strElementoEntity As String = ""

    '        If (app_procesointerno = False) Then
    '            'If TypeOf e.DBObject Is BlockReference Then

    '            '    Try
    '            '        'Hay que acceder con Active X a la base de datos porque con ACAD da error al estar eliminado el objeto
    '            '        'strElementoEntity = clsA.BloqueAtributoDame(CType(ent.AcadObject, AcadBlockReference).ObjectID, "ELEMENTO")
    '            '        'strElementoEntity = clsA.AttributeReference_Get(e.DBObject.Id, "ELEMENTO", OpenMode.ForRead, True).TextString
    '            '        strElementoEntity = clsA.XLeeDato(e.DBObject.Id, "ELEMENTO", True)
    '            '    Catch ex As Exception
    '            '        strElementoEntity = "-1" 'Ha seleccionado un blockReference que no tiene atributo ELEMENTO y por lo tanto no se le puede asignar un proxy
    '            '    End Try


    '            '    If strElementoEntity <> "" And strElementoEntity <> "-1" Then
    '            '        'Esta eliminando un blockreference que tiene asignado un Elemento proxy
    '            '        'If colP.ContainsKey(strElementoEntity) Then
    '            '        '    colP.Remove(strElementoEntity) ' Elimina del diccionario el elemento borrado
    '            '        'End If

    '            '        Dim arrayProxy As ArrayList = clsA.MleaderDameTodos_PorAtributo("ELEMENTO", strElementoEntity, True)


    '            '        If arrayProxy.Count > 0 Then
    '            '            'En principio solo tiene que encontrar uno, porque cada Mleader tiene un unico elemento
    '            '            DeleteElemento(strElementoEntity)
    '            '            CType(arrayProxy(0), AcadMLeader).Delete()

    '            '        End If

    '            '    End If

    '            '    'End If

    '            'ElseIf TypeOf e.DBObject Is MLeader Then


    '            '    Try
    '            '        'Hay que acceder con Active X a la base de datos porque con ACAD da error al estar eliminado el objeto
    '            '        strElementoEntity = clsA.AttributeReference_Get_FromMLeader(e.DBObject.Id, "ELEMENTO", OpenMode.ForRead, True).TextString
    '            '    Catch ex As Exception
    '            '        strElementoEntity = "-1" 'Ha seleccionado un Mleader que no tiene atributo ELEMENTO
    '            '    End Try

    '            '    If strElementoEntity <> "" And strElementoEntity <> "-1" Then
    '            '        'Dim arrayProxy As ArrayList = clsA.BloquesDameTodos_PorAtributo("ELEMENTO", strElementoEntity, True)
    '            '        Dim arrayProxy As ArrayList = clsA.DameBloquesTODOS_XData("ELEMENTO", strElementoEntity)

    '            '        If arrayProxy.Count > 0 Then
    '            '            'En principio solo tiene que encontrar uno
    '            '            'clsA.BloqueAtributoEscribe(CType(arrayProxy(0), AcadBlockReference).ObjectID, "ELEMENTO", "") 'Lo pone vacio porque se elimina el proxy.
    '            '            clsA.XPonDato(CType(arrayProxy(0), AcadBlockReference), "ELEMENTO", "")
    '            '        End If

    '            '        'If colP.ContainsKey(strElementoEntity) Then
    '            '        '    colP.Remove(strElementoEntity)
    '            '        'End If
    '            '        DeleteElemento(strElementoEntity)
    '            '    End If

    '            'End If
    '        End If
    '    End Sub

    '#End Region

#Region "BOTONES"
    'Private Sub btnSelCinta_Click(sender As Object, e As EventArgs)
    '    'If clsA.oAppA.ActiveDocument.ActiveSpace = Autodesk.AutoCAD.Interop.Common.AcActiveSpace.acPaperSpace Then
    '    '    MsgBox("Proxy sólo puede insertarse en Espacio Modelo", MsgBoxStyle.Critical, "AVISOS AL USUARIO")
    '    '    Exit Sub
    '    'End If
    '    '
    '    Me.Visible = False
    '    ' Cargar recursos
    '    clsA.ClonaTODODesdeDWG(BloqueRecursos)

    '    Try
    '        Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CMLEADERSTYLE", fijoCliente)
    '    Catch ex As Exception
    '        MsgBox("No existe el estilo " & fijoCliente)
    '        Exit Sub
    '    End Try
    '    ' Activar la capa 'proxy' y quitar el contorno de cobertura
    '    clsA.CapaActiva(capaproxy)
    '    clsA.CoberturaOnOff(False)
    '    ' Seleccionar el bloque

    '    oBlR = clsA.BloqueSeleccionaDame()
    '    '
    '    If oBlR Is Nothing Then
    '        MsgBox("No ha seleccionado ningún bloque...")
    '        Exit Sub
    '        'ElseIf cbPatas.Text <> "" Then
    '        '    btnInsertaPata.Enabled = True
    '        btnSelCinta.ForeColor = Drawing.Color.Red
    '    Else
    '        btnSelCinta.ForeColor = Drawing.Color.Black
    '        clsA.XPonDato(oBlR, "tipo", "cinta")
    '    End If
    '    '
    '    txtL1.Text = ""
    '    txtA1.Text = ""
    '    Dim oPars As Hashtable = clsA.BloqueDinamicoParametrosDameTodos(oBlR.ObjectID)
    '    Dim mensaje As String = ""
    '    For Each nPar As String In oPars.Keys
    '        mensaje &= nPar & " = " & oPars(nPar) & vbCrLf
    '        If nPar = "DISTANCIA1" Or nPar = "DISTANCE1" Or nPar = "LENGTH" Then
    '            txtL1.Text = oPars(nPar)
    '            largo = CDbl(oPars(nPar))
    '        ElseIf nPar = "DISTANCIA2" Or nPar = "DISTANCE2" Or nPar = "WIDTH" Then
    '            txtA1.Text = oPars(nPar)
    '            ancho = CDbl(oPars(nPar))
    '        End If
    '    Next
    '    ' MsgBox(oBlr.EffectiveName)
    '    ' MsgBox(mensaje)
    '    '
    '    Me.Visible = True
    'End Sub

    'Private Sub cbPatas_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    If cbPatas.Text = "" Then
    '        lblBloque.ForeColor = Drawing.Color.Red
    '    Else
    '        lblBloque.ForeColor = Drawing.Color.Black
    '        Dim fullBloque As String = IO.Path.Combine(CintasDir, cbPatas.Text & ".dwg")
    '        'Dim fullBloquePNG As String = IO.Path.ChangeExtension(fullBloque, ".png")
    '        If IO.File.Exists(fullBloque) Then
    '            Try
    '                If clsA Is Nothing Then clsA = New AutoCAD2acad.clsAutoCAD2acad(Ev.EvApp, cfg._appFullPath, regAPPCliente)
    '                'If IO.File.Exists(fullBloquePNG) Then
    '                'pbBloque.Image = Drawing.Image.FromFile(fullBloquePNG)
    '                'Else
    '                pbBloque.Image = clsA.DameBitmapDWG_IO(fullBloque)
    '                'End If
    '            Catch ex As Exception
    '                pbBloque.Image = Nothing
    '            End Try
    '        End If
    '    End If
    'End Sub

    'Private Sub cbNPatas_SelectedIndexChanged_1(sender As Object, e As EventArgs)
    '    If cbNPatas.Text = "" Then
    '        lblNPatas.ForeColor = Drawing.Color.Red
    '    Else
    '        lblNPatas.ForeColor = Drawing.Color.Black
    '    End If
    'End Sub

    'Private Sub btnInsertaPata_Click(sender As Object, e As EventArgs)
    '    If oBlR Is Nothing Then Exit Sub
    '    Dim fullPata As String = IO.Path.Combine(CintasDir, cbPatas.Text & ".dwg")
    '    If IO.File.Exists(fullPata) = False Then
    '        MsgBox("No existe el bloque de pata a insertar desde:" & vbCrLf & fullPata)
    '        Exit Sub
    '    End If
    '    '
    '    If clsA Is Nothing Then clsA = New AutoCAD2acad.clsAutoCAD2acad(Ev.EvApp, cfg._appFullPath, regAPPCliente)
    '    Dim cuantas As Integer = IIf(IsNumeric(cbNPatas.Text), CInt(cbNPatas.Text), 0)
    '    PonPatasEnCinta(fullPata, cuantas)
    '    'Dim oBlPara As AcadBlockReference = clsA.BloqueInserta(fullPata, 1)
    '    ' Insertar las 2 patas en la cinta transportadora oBlR
    '    '
    '    ' Pata 1
    '    'Dim oPt1 As Object = oBlR.InsertionPoint
    '    'Dim oBlPata1 As AcadBlockReference = Ev.EvApp.ActiveDocument.ModelSpace.InsertBlock(oPt1, fullPata, 1, 1, 1, 0)
    '    '' Pata 2
    '    'Dim oPt2 = New Double() {oPt1(0), oPt1(1) + 200, oPt1(2)}
    '    'Dim oBlPata2 As AcadBlockReference = Ev.EvApp.ActiveDocument.ModelSpace.InsertBlock(oPt2, fullPata, 1, 1, 1, 0)
    '    ''
    '    'If oBlPata1 IsNot Nothing And oBlPata2 IsNot Nothing Then
    '    '    Me.OnLoad(Nothing)
    '    'End If
    'End Sub
    ''
    'Public Sub PonPatasEnCinta(queFullPata As String, cuantas As Integer)
    '    ' Crear/Poner capa de patas activa
    '    clsA.CapaCreaActiva(PatasCapa,,,, True)
    '    '
    '    If cuantas = 1 Then
    '        Dim oblPata As AcadBlockReference = clsA.BloqueInserta(queFullPata, oBlR.XScaleFactor)
    '        oblPata.Rotate(oblPata.InsertionPoint, Utilidades.GraRad(90))
    '        oblPata.Rotate(oblPata.InsertionPoint, oBlR.Rotation)
    '        clsA.XPonDato(oblPata, "cinta", oBlR.Handle)
    '        clsA.XPonDato(oblPata, "tipo", "pata")
    '        If oblPata.Layer <> PatasCapa Then oblPata.Layer = PatasCapa
    '        clsA.BloqueDinamicoParametroEscribe(oblPata.ObjectID, "WIDTH", CDbl(ancho)) ' ancho.ToString)
    '        oblPata.Update()
    '        Exit Sub
    '    End If
    '    '
    '    Dim trozoIniFin As Double = 70  '(largo / cuantas) / 2       ' Distancia al inicio y final de las patas 1 y final
    '    Dim trozo As Double = (largo - (trozoIniFin * 2)) / (cuantas - 1)         ' Distancia entre cada pata
    '    Dim cXIni As Double = Nothing   ' CDbl(oBlR.InsertionPoint(0)) + trozoIniFin
    '    If oBlR.XEffectiveScaleFactor > 0 Then
    '        cXIni = CDbl(oBlR.InsertionPoint(0)) + trozoIniFin
    '    Else
    '        cXIni = CDbl(oBlR.InsertionPoint(0)) - trozoIniFin
    '        trozo = trozo * -1
    '    End If
    '    'Dim coordXFin As Double = CDbl(oBlR.InsertionPoint(0)) + largo - trozoIniFin
    '    '
    '    ' Insertar la pata
    '    For x As Integer = 1 To cuantas
    '        'Dim oPt() As Double = New Double() _
    '        '    {cXIni,
    '        '    IIf(oBlR.YEffectiveScaleFactor > 0, oBlR.InsertionPoint(1) + (ancho / 2), oBlR.InsertionPoint(1) - (ancho / 2)),
    '        '        oBlR.InsertionPoint(2)}
    '        Dim oPt() As Double = New Double() {cXIni, oBlR.InsertionPoint(1), oBlR.InsertionPoint(2)}
    '        Dim oBlPata As AcadBlockReference = Ev.EvApp.ActiveDocument.ModelSpace.InsertBlock(oPt, queFullPata, oBlR.XEffectiveScaleFactor, oBlR.YEffectiveScaleFactor, oBlR.ZEffectiveScaleFactor, Utilidades.GraRad(90))
    '        'Dim oblPata As AcadBlockReference = clsA.BloqueInserta(queFullPata, oBlR.XScaleFactor)
    '        oBlPata.Rotate(oBlR.InsertionPoint, oBlR.Rotation)
    '        clsA.XPonDato(oBlPata, "cinta", oBlR.Handle)
    '        clsA.XPonDato(oBlPata, "tipo", "pata")
    '        If oBlPata.Layer <> PatasCapa Then oBlPata.Layer = PatasCapa
    '        clsA.BloqueDinamicoParametroEscribe(oBlPata.ObjectID, "WIDTH", CDbl(ancho)) ' ancho.ToString)
    '        oBlPata.Update()
    '        cXIni += trozo
    '    Next



    '    ' Pata 1
    '    'Dim oPt1 As Object = oBlR.InsertionPoint
    '    'Dim oBlPata1 As AcadBlockReference = Ev.EvApp.ActiveDocument.ModelSpace.InsertBlock(oPt1, queFullPata, oBlR.XEffectiveScaleFactor, oBlR.YEffectiveScaleFactor, oBlR.ZEffectiveScaleFactor, 0)
    '    '' Pata 2
    '    'Dim oPt2 = New Double() {oPt1(0) + largo, oPt1(1), oPt1(2)}
    '    'Dim oBlPata2 As AcadBlockReference = Ev.EvApp.ActiveDocument.ModelSpace.InsertBlock(oPt2, queFullPata, 1, 1, 1, 0)
    '    ''
    '    'If oBlPata1 IsNot Nothing Then
    '    '    clsA.BloqueDinamicoParametroEscribe(oBlPata1.ObjectID, "Visibilidad", ancho)
    '    '    'oBlPata1.Rotate(oBlPata1.InsertionPoint, oBlR.Rotation)
    '    '    oBlPata1.Rotate(oBlPata1.InsertionPoint, Utilidades.GraRad(90))
    '    'End If
    '    ''
    '    'If oBlPata2 IsNot Nothing Then
    '    '    clsA.BloqueDinamicoParametroEscribe(oBlPata2.ObjectID, "Visibilidad", ancho)
    '    '    oBlPata2.Rotate(oBlPata1.InsertionPoint, Utilidades.GraRad(90))
    '    'End If
    '    ''
    '    'If oBlPata1 IsNot Nothing And oBlPata2 IsNot Nothing Then
    '    '    oBlPata1.Rotate(oBlR.InsertionPoint, oBlR.Rotation)
    '    '    oBlPata2.Rotate(oBlR.InsertionPoint, oBlR.Rotation)
    '    Ev.EvApp.ActiveDocument.Regen(AcRegenType.acAllViewports)
    '    Me.OnLoad(Nothing)
    '    'End If
    'End Sub

    'Private Sub cbNPatas_SelectedIndexChanged(sender As Object, e As EventArgs)

    'End Sub
#End Region
End Class
