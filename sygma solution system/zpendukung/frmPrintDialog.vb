Imports master_new.PGSqlConn
Imports master_new.ModFunction
Imports System.IO
Imports System.Reflection

Public Class frmPrintDialog
   
    Public _ssql As String
    Public _report As String
    Dim dt As New DataTable
    Dim _dir As String
    Dim _file As String
    Dim report As DevExpress.XtraReports.UI.XtraReport
    Public _remarks As String
    Public _data_from_api As Boolean
    
    
    Private Sub frmPrintDialog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            init_report()
            load_file()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Private Sub load_file()
        Try

            Dim ssql As String
            ssql = "select '' as name"


            dt = GetTableData(ssql)

            dt.Rows.Clear()
            gridControl1.DataSource = dt

            Dim Files As String() = Directory.GetFiles(_dir)
            Dim _dtrow As DataRow
            For Each Filename In Files
                _dtrow = dt.NewRow
                _dtrow("name") = Filename.Replace(_dir, "")
                dt.Rows.Add(_dtrow)
                dt.AcceptChanges()
            Next
            _dtrow = dt.NewRow
            _dtrow("name") = "Default"
            dt.Rows.Add(_dtrow)
            dt.AcceptChanges()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub bt_design_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_design.Click
        Try
            init_report()

            Dim _file_selected As String = ""

            If dt.Rows.Count = 0 Then
                report.SaveLayout(_file)
                _file_selected = _file
            Else
                If dt.Rows(BindingContext(dt).Position).Item("name") = "Default" Then
                    report.SaveLayout(_file)
                    _file_selected = _file
                Else
                    _file_selected = _dir & dt.Rows(BindingContext(dt).Position).Item("name")
                End If

            End If


            Dim designForm As DevExpress.XtraReports.UserDesigner.XRDesignFormExBase = New CustomDesignForm()
            designForm.OpenReport(_file_selected)
            designForm.FileName = _file_selected
            designForm.Show()


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub init_report()
        Try
            Dim ds As New DataSet
           
            Dim rpt As New DevExpress.XtraReports.UI.XtraReport
            rpt = CreateReport(_report)
            report = rpt


            _dir = CekPath(konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\conf_system.txt"), "report_path")) _
                 & report.GetType().Name.ToString & "\"

            _file = _dir & report.GetType().Name.ToString & ".repx"

            If System.IO.Directory.Exists(_dir) = False Then
                System.IO.Directory.CreateDirectory(_dir)
            End If

            With report
                ds = ReportDataset(_ssql)

                If ds.Tables(0).Rows.Count = 0 Then
                    MessageBox.Show("Data Doens't Exist.., Contact Your Admin Program..", "Information", _
                                    MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                .DataSource = ds
                .DataMember = "Table"

            End With
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub bt_preview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_preview.Click
        Try

            init_report()
            'open_report(_report)

            Dim _file_selected As String = ""

            If dt.Rows.Count = 0 Then
                _file_selected = _file
            Else
                _file_selected = _dir & dt.Rows(BindingContext(dt).Position).Item("name")
            End If

            If System.IO.File.Exists(_file_selected) Then
                report.LoadLayout(_file_selected)
            End If
            '' reportprintTool.ShowPreview()
            'Dim frmReport As New frmPrintDialogReport
            'frmReport._report = report
            'frmReport._remarks = _remarks
            'frmReport.Show()

            'RibbonForm1

            Dim frmReport As New frmPrintDialogReportNew
            frmReport._report = report
            frmReport._remarks = _remarks
            frmReport.Show()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Shared Function CreateObjectInstance(ByVal objectName As String) As Object
        ' Creates and returns an instance of any object in the assembly by its type name.  

        Dim obj As Object
        'Dim ObjInstanceType As Type
        Try

            Dim sValue As String
            Dim FullTypeName As String
            Dim FormInstanceType As Type

            ' Form class name
            sValue = objectName

            ' Assume that form classes' namespace is the same as ProductName
            FullTypeName = [Assembly].GetEntryAssembly.GetName.Name.ToString.Replace(" ", "_") & "." & sValue

            ' Now, get the actual type
            FormInstanceType = Type.GetType(FullTypeName, True, True)
            ' Create an instance of this form type
            obj = Activator.CreateInstance(FormInstanceType)


            'Dim report As DevExpress.XtraReports.UI.XtraReport = DirectCast(Activator.CreateInstance(reportType), DevExpress.XtraReports.UI.XtraReport)


        Catch ex As Exception
            obj = Nothing
        End Try

        Return obj

    End Function

    Public Shared Function CreateForm(ByVal formName As String) As Form ' Return the instance of the form by specifying its name. 

        Return DirectCast(CreateObjectInstance(formName), Form)

    End Function

    Public Shared Function CreateReport(ByVal ReportName As String) As DevExpress.XtraReports.UI.XtraReport ' Return the instance of the form by specifying its name. 
        Return DirectCast(CreateObjectInstance(ReportName), DevExpress.XtraReports.UI.XtraReport)
    End Function
   
    Private Sub ExportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToolStripMenuItem.Click
        Try

            System.IO.File.Delete(_dir & dt.Rows(BindingContext(dt).Position).Item("name"))
            load_file()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class