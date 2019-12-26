Imports System.Windows.Forms

Public Class frmConfigura
    Private Sub frmConfigura_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Eventos.SYSMONVAR(True)
        Me.Text = "CONFIGURACIÓN - v" & appVersion
        PonConfiguracionInicial()
    End Sub

    Private Sub frmConfigura_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Eventos.SYSMONVAR()
        frmCo = Nothing
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim actualizadatos As Boolean = False
        If LAYOUTDB <> txtExcelDb.Text Then
            LAYOUTDB = txtExcelDb.Text
            cIni.IniWrite(appIni, "OPTIONS", "LAYOUTDB", LAYOUTDB)
            actualizadatos = True
        End If
        If BloqueRecursos <> txtBloqueRecursos.Text Then
            BloqueRecursos = txtBloqueRecursos.Text
            cIni.IniWrite(appIni, "OPTIONS", "BloqueRecursos", BloqueRecursos)
        End If
        '
        If BloquesDir <> txtDirBloques.Text Then
            BloquesDir = txtDirBloques.Text
            cIni.IniWrite(appIni, "OPTIONS", "BloquesDir", BloquesDir)
            actualizadatos = True
        End If
        '
        If PatasCapa <> txtPatasCapa.Text Then
            PatasCapa = txtPatasCapa.Text
            cIni.IniWrite(appIni, "OPTIONS", "PatasCapa", PatasCapa)
        End If
        '
        If CintasCapa <> txtCintasCapa.Text Then
            CintasCapa = txtCintasCapa.Text
            cIni.IniWrite(appIni, "OPTIONS", "CintasCapa", CintasCapa)
        End If
        ' Actualizar colecciones de clsLAYOUTDB
        If actualizadatos = True Then btnReReadDB_Click(Nothing, Nothing)
        '
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Public Sub PonConfiguracionInicial()
        txtExcelDb.Text = LAYOUTDB : txtExcelDb.Enabled = False
        txtBloqueRecursos.Text = BloqueRecursos : txtBloqueRecursos.Enabled = False
        txtDirBloques.Text = BloquesDir : txtDirBloques.Enabled = False
        txtPatasCapa.Text = PatasCapa : txtPatasCapa.Enabled = True
        txtCintasCapa.Text = CintasCapa : txtCintasCapa.Enabled = True
        lblInf.Text = ""
    End Sub
    '
    Private Sub btnExcelDb_Click(sender As Object, e As EventArgs) Handles btnExcelDb.Click
        Dim oFd As New OpenFileDialog
        oFd.FileName = txtExcelDb.Text
        oFd.CheckFileExists = True
        oFd.Filter = "Fichero *.xlsx|*.xlsx"
        oFd.FilterIndex = 1
        oFd.InitialDirectory = IO.Path.GetDirectoryName(oFd.FileName)
        If oFd.ShowDialog = DialogResult.OK Then
            If oFd.FileName <> txtExcelDb.Text Then
                txtExcelDb.Text = oFd.FileName
            End If
        End If
    End Sub

    Private Sub btnReReadDB_Click(sender As Object, e As EventArgs) Handles btnReReadDB.Click
        'clsD = New clsLAYOUTDBS4
        'lblInf.Text = clsD.DATOS.Count + clsD.SELECCIONABLES.Count.ToString & " files read..."
        'dicBloques_LlenaConDirRaiz(BloquesDir)
    End Sub
    Private Sub btnBloqueRecursos_Click(sender As Object, e As EventArgs) Handles btnBloqueRecursos.Click
        Dim oFd As New OpenFileDialog
        oFd.FileName = txtBloqueRecursos.Text
        oFd.CheckFileExists = True
        oFd.Filter = "Fichero *.dwg|*.dwg"
        oFd.FilterIndex = 1
        oFd.InitialDirectory = IO.Path.GetDirectoryName(oFd.FileName)
        If oFd.ShowDialog = DialogResult.OK Then
            If oFd.FileName <> txtBloqueRecursos.Text Then
                txtBloqueRecursos.Text = oFd.FileName
            End If
        End If
    End Sub

    Private Sub btnDirBloques_Click(sender As Object, e As EventArgs) Handles btnDirBloques.Click
        Dim oFb As New FolderBrowserDialog
        oFb.Description = "Seleccionar directorio principal de BLOQUES"
        oFb.SelectedPath = txtDirBloques.Text
        If oFb.ShowDialog = DialogResult.OK Then
            If oFb.SelectedPath <> txtDirBloques.Text Then
                txtDirBloques.Text = oFb.SelectedPath
            End If
        End If
    End Sub
End Class
