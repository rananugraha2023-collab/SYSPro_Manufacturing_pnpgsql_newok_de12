Imports master_new.PGSqlConn
Imports master_new.ModFunction
Imports System.Drawing.Printing
Imports System.IO

Public Class frmPrintDialogReport
    Public _report As DevExpress.XtraReports.UI.XtraReport
    Public _remarks As String
    Dim filename As String

    Private Sub frmPrintDialogReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.P And e.Modifiers = Keys.Control Then
            log_print()
        End If
    End Sub

    'Private Sub frmPrintDialogReport_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyDown
    '    Try
    '        If (e.KeyCode = Keys.P And e.Modifiers = Keys.Control) Then
    '            log_print()
    '        End If
    '    Catch ex As Exception
    '        Pesan(Err)
    '    End Try
    'End Sub

    Private Sub frmPrintDialogReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            PrintControl1.PrintingSystem = _report.PrintingSystem

            ' Generate the document for preview. 
            If _report.GetType().Name.ToString = "rptDistribusi" Then
                '_report.LblTgl.text = _remarks
                Dim my As rptDistribusi = CType(_report, rptDistribusi)
                my.LblTgl.Text = _remarks.Replace(">>", "to")
            End If

            _report.CreateDocument()
            Me.Text = "Report Preview - " & _report.GetType().Name.ToString
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub PrintPreviewBarItem5_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles PrintPreviewBarItem5.ItemClick
        log_print()
    End Sub

    Private Sub PrintPreviewBarItem6_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles PrintPreviewBarItem6.ItemClick
        log_print()
    End Sub
    Private Sub log_print()
        Try
            Dim ssqls As New ArrayList

            ssqls.Add(insert_log("Print " & _report.GetType().Name.ToString & " " & _remarks & " " & CekTanggal.ToString("dd/MM/yyyy HH:mm:ss")))

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    'Return False
                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    'Return False
                    Exit Sub
                End If
                ssqls.Clear()
            End If

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub BarButtonItem2_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        Try
            'Dim imageOptions As DevExpress.XtraPrinting.ImageExportOptions = _report.ExportOptions.Image

            '' Set Image-specific export options.
            'imageOptions.Resolution = 300
            'imageOptions.ExportMode = DevExpress.XtraPrinting.ImageExportMode.SingleFile
            'imageOptions.Format = System.Drawing.Imaging.ImageFormat.Png


            filename = My.Application.Info.DirectoryPath & "\export\" + "Print" + Now.ToString("_yyyyMMdd_HHmmss") + ".pdf"

            Try
                If IO.Directory.Exists(My.Application.Info.DirectoryPath & "\export") = True Then
                    For Each foundFile As String In My.Computer.FileSystem.GetFiles( _
                     My.Application.Info.DirectoryPath & "\export", _
                     Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, "*.*")

                        My.Computer.FileSystem.DeleteFile(foundFile, _
                            Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, _
                            Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently, FileIO.UICancelOption.DoNothing)
                    Next
                End If
            Catch ex As Exception
            End Try

            If IO.Directory.Exists(My.Application.Info.DirectoryPath & "\export") = False Then
                IO.Directory.CreateDirectory(My.Application.Info.DirectoryPath & "\export")
            End If

            '_report.ExportToImage(filename, imageOptions)

            Dim pdfOptions As DevExpress.XtraPrinting.PdfExportOptions = _report.ExportOptions.Pdf



            _report.ExportToPdf(filename)

            'C:\Program Files\Foxit Software\Foxit Reader>"Foxit Reader".exe  d:\file.pdf

            Dim _program As String
            _program = "C:\Program Files\Foxit Software\Foxit Reader\Foxit Reader.exe" '  d:\file.pdf

            If IO.File.Exists(_program) = False Then
                _program = "C:\Program Files (x86)\Foxit Software\Foxit Reader\Foxit Reader.exe"
            End If

            Dim myprocess As System.Diagnostics.Process = New System.Diagnostics.Process()
            Dim outputReader As StreamReader = Nothing
            Dim errorReader As StreamReader = Nothing
            With myprocess
                .StartInfo.FileName = _program
                .StartInfo.RedirectStandardOutput = True
                .StartInfo.RedirectStandardError = True
                .StartInfo.RedirectStandardInput = True
                .StartInfo.ErrorDialog = False
                .StartInfo.UseShellExecute = False
                .StartInfo.Arguments = " " & ControlChars.Quote & filename & ControlChars.Quote
                .StartInfo.WindowStyle = ProcessWindowStyle.Maximized
                .Start()

                'outputReader = myprocess.StandardOutput
                'errorReader = myprocess.StandardError

            End With

            'AddHandler PrintGraphicControl.PrintPage, AddressOf Me.GraphicPrint

            'Dim pkCustomSize1 As New PaperSize("Custom Paper Size", 827, 583)

            'PrintGraphicControl.DefaultPageSettings.PaperSize = pkCustomSize1
            'PrintGraphicControl.DefaultPageSettings.Margins.Left = 5
            'PrintGraphicControl.DefaultPageSettings.Margins.Top = 5
            'PrintGraphicControl.Print()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Private Sub GraphicPrint(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        e.Graphics.DrawImage(Image.FromFile(filename), e.Graphics.VisibleClipBounds)
        e.HasMorePages = False

    End Sub



    Public Sub SettingsHandler(ByVal sender As Object, ByVal e As QueryPageSettingsEventArgs)
        'Dim a As New System.Drawing.Printing.PaperSize("A5 (148 x 210 mm)", 584, 827)
        'Dim ps As New PageSettings
        'ps.PaperSize = a
        'ps.Landscape = True
        'ps.Margins.Top = 0
        'ps.Margins.Bottom = 0
        'ps.Margins.Left = 5
        'ps.Margins.Right = 5
        'e.PageSettings = ps
    End Sub

    Private Sub BarButtonItem3_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        Try
            'Dim imageOptions As DevExpress.XtraPrinting.ImageExportOptions = _report.ExportOptions.Image

            '' Set Image-specific export options.
            'imageOptions.Resolution = 300
            'imageOptions.ExportMode = DevExpress.XtraPrinting.ImageExportMode.SingleFile
            'imageOptions.Format = System.Drawing.Imaging.ImageFormat.Png


            filename = My.Application.Info.DirectoryPath & "\export\" + "Print" + Now.ToString("_yyyyMMdd_HHmmss") + ".pdf"

            Try
                If IO.Directory.Exists(My.Application.Info.DirectoryPath & "\export") = True Then
                    For Each foundFile As String In My.Computer.FileSystem.GetFiles( _
                     My.Application.Info.DirectoryPath & "\export", _
                     Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, "*.*")

                        My.Computer.FileSystem.DeleteFile(foundFile, _
                            Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, _
                            Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently, FileIO.UICancelOption.DoNothing)
                    Next
                End If
            Catch ex As Exception
            End Try

            If IO.Directory.Exists(My.Application.Info.DirectoryPath & "\export") = False Then
                IO.Directory.CreateDirectory(My.Application.Info.DirectoryPath & "\export")
            End If


            Dim pdfOptions As DevExpress.XtraPrinting.PdfExportOptions = _report.ExportOptions.Pdf


            _report.ExportToPdf(filename)

            Dim _program As String
            _program = My.Application.Info.DirectoryPath & "\export\print.bat" '  d:\file.pdf

            'Microsoft.VisualBasic.Left(My.Application.Info.DirectoryPath, 1).ToLower & Microsoft.VisualBasic.Right(My.Application.Info.DirectoryPath, My.Application.Info.DirectoryPath.Length - 1)
            Dim _bat As String = "/home/%username%/FoxitSoftware/FoxitReader/FoxitReader.sh " & "/home/%username%/.wine/dosdevices/" & ControlChars.Quote & Microsoft.VisualBasic.Left(filename.Replace("\", "/"), 1).ToLower & Microsoft.VisualBasic.Right(filename.Replace("\", "/"), filename.Replace("\", "/").Length - 1) & ControlChars.Quote
            SaveTextToFile(_bat, My.Application.Info.DirectoryPath & "\export\print.bat")

            Dim myprocess As System.Diagnostics.Process = New System.Diagnostics.Process()
            
            With myprocess
                .StartInfo.FileName = _program
                .StartInfo.RedirectStandardOutput = False
                .StartInfo.UseShellExecute = False
                .Start()

            End With

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class