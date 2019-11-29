Imports System.Windows.Forms
Imports a2 = AutoCAD2acad.A2acad

Public Class frmBloques
    Private oImgL As ImageList
    Private Sub frmBloques_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Eventos.SYSMONVAR(True)
        app_procesointerno = True
        oBlR = Nothing
        Me.Text = "GESTOR BLOQUES - v" & cfg._appversion
        ListBoxDirectorios_llena()
    End Sub

    Private Sub frmBloques_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Eventos.SYSMONVAR(False)
        app_procesointerno = False
        oBlR = Nothing
        frmBlo = Nothing
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Private Sub lbTipos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbTipos.SelectedIndexChanged
        If lbTipos.SelectedIndex < 0 Then Exit Sub
        '
        Dim fullDir As String = IO.Path.Combine(BloquesDir, lbTipos.SelectedItem.ToString)
        lblInf.Text = fullDir
        lvBloques.Tag = fullDir
        ListViewBloques_llena()
    End Sub

    Private Sub lvBloques_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvBloques.SelectedIndexChanged
        If lvBloques.SelectedItems.Count = 0 Then
            IIf(lbTipos.SelectedIndex = -1, lblInf.Text = "", lblInf.Text = lvBloques.Tag.ToString)
            btnInsertar.Enabled = False
            btnCambiar.Enabled = False
            Exit Sub
        End If
        '
        lblInf.Text = lvBloques.SelectedItems.Item(0).Tag
        btnInsertar.Enabled = True
        btnCambiar.Enabled = True
    End Sub
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#Region "FUNCIONES"
    Public Sub ListBoxDirectorios_llena()
        lbTipos.Items.Clear()
        lvBloques.Items.Clear()
        lblInf.Text = ""
        btnInsertar.Enabled = False
        btnCambiar.Enabled = False
        '
        For Each queDir As String In IO.Directory.GetDirectories(BloquesDir)
            Dim nombre As String = IO.Path.GetFileNameWithoutExtension(queDir)
            Dim indice As Integer = lbTipos.Items.Add(nombre.ToUpper)
        Next
    End Sub
    '
    Public Sub ListViewBloques_llena()
        lvBloques.Items.Clear()
        btnInsertar.Enabled = False
        btnCambiar.Enabled = False
        Dim fullDir As String = lvBloques.Tag.ToString
        '
        If oImgL Is Nothing Then
            oImgL = New ImageList
            oImgL.ImageSize = New Drawing.Size(sizeImg, sizeImg)
            oImgL.ColorDepth = ColorDepth.Depth16Bit
            oImgL.TransparentColor = System.Drawing.Color.White
            lvBloques.LargeImageList = oImgL
            lvBloques.SmallImageList = oImgL
        End If


        ' Coger cada fichero DWG y sacar la imagen.
        'lv1.AutoArrange = True
        'lv1.BeginUpdate()     ' Que no se redibuje lv1
        For Each queFi As String In IO.Directory.GetFiles(fullDir, "*.dwg")
            Dim quePng As String = IO.Path.ChangeExtension(queFi, ".png")
            Dim queImg As System.Drawing.Image = Nothing
            If IO.File.Exists(quePng) Then
                queImg = Drawing.Image.FromFile(quePng)
            Else
                queImg = clsA.DameBitmapDWG_Thumbnail(queFi)
            End If

            Dim key As String = IO.Path.GetFileName(queFi)
            If oImgL.Images.ContainsKey(key) = False Then
                oImgL.Images.Add(key, queImg)
            End If
            Dim oLvI As New ListViewItem(key, key)
            oLvI.Tag = queFi
            oLvI.ToolTipText = queFi
            lvBloques.Items.Add(oLvI)
            System.Windows.Forms.Application.DoEvents()
            oLvI = Nothing
        Next
    End Sub

    Private Sub btnInsertar_Click(sender As Object, e As EventArgs) Handles btnInsertar.Click
        If lvBloques.SelectedItems.Count = 0 Then Exit Sub
        '
        Dim fullpath As String = lvBloques.SelectedItems(0).Tag
        Me.WindowState = FormWindowState.Minimized
        Try
            oBlR = clsA.Bloque_Inserta(fullpath, 1, False)
            If oBlR IsNot Nothing Then
                Dim tipoSel As String = lbTipos.SelectedItem.ToString.ToLower
                Dim tipo As String = tipoSel.Substring(0, tipoSel.Length - 1)
                clsA.XPonDato(oBlR.Handle, "tipo", tipo)
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub btnCambiar_Click(sender As Object, e As EventArgs) Handles btnCambiar.Click
        If lvBloques.SelectedItems.Count = 0 Then Exit Sub
        '
        Dim fullpath As String = lvBloques.SelectedItems(0).Tag
        'Me.WindowState = FormWindowState.Minimized
        Try
            oBlR = clsA.Bloque_SeleccionaDame
            If oBlR IsNot Nothing Then
                Dim msgResult As MsgBoxResult = MsgBox("Desea cambiar sólo este bloque [SI] o todos los bloques como este [NO]", MsgBoxStyle.YesNoCancel, "CAMBIAR UNO O TODOS")
                If msgResult = MsgBoxResult.Yes Then
                    clsA.Bloque_Cambia(oBlR, fullpath, False)     ' False, solo cambia el actual
                ElseIf msgResult = MsgBoxResult.No Then
                    clsA.Bloque_Cambia(oBlR, fullpath, True)     ' True, cambia todos
                ElseIf msgResult = MsgBoxResult.Cancel Then
                    oBlR = Nothing
                    Exit Sub
                End If
                If clsA.oBlult IsNot Nothing Then
                    Dim tipoSel As String = lbTipos.SelectedItem.ToString.ToLower
                    Dim tipo As String = tipoSel.Substring(0, tipoSel.Length - 1)
                    clsA.XPonDato(clsA.oBlult.Handle, "tipo", tipo)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
        Me.WindowState = FormWindowState.Normal
    End Sub

#End Region
End Class
