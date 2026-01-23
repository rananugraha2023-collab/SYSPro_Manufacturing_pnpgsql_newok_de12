Imports master_new.PGSqlConn
Imports master_new.ModFunction
Imports DevExpress.XtraEditors.Controls
Imports npgsql

Public Class FTransferIssueExport
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim sSQL As String

    Private Sub FInventoryReportDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        init_le(pr_entity, "en_mstr")
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number Name", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty", "ptsfrd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Price", "price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Unit Measure", "um_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "ISBN", "pt_isbn", DevExpress.Utils.HorzAlignment.Default)

    
    End Sub


    Private Sub pr_entity_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pr_entity.EditValueChanged
        Dim _en_id_coll As String = func_data.entity_parent(pr_entity.EditValue)
        Dim dt As New DataTable
        Try
            sSQL = "select pi_id, pi_desc from pi_mstr where pi_active ~~* 'Y'" _
                 & " AND pi_dom_id = " & master_new.ClsVar.sdom_id _
                 & " AND pi_en_id in (" & _en_id_coll + ")" _
                 & " AND pi_start_date <= " + SetDate(CekTanggal) + " and pi_end_date >= " + SetDate(CekTanggal) _
                 & " AND pi_active ~~* 'Y' " _
                 & " order by pi_desc"

            dt = GetTableData(sSQL)
            With pr_price_list
                If .Properties.Columns.VisibleCount = 0 Then
                    .Properties.Columns.Add(New LookUpColumnInfo("pi_id", "ID", 10))
                    .Properties.Columns.Add(New LookUpColumnInfo("pi_desc", "Code", 15))
                End If

                .Properties.DataSource = dt
                .Properties.DisplayMember = dt.Columns("pi_desc").ToString
                .Properties.ValueMember = dt.Columns("pi_id").ToString
                If dt.Rows.Count > 0 Then
                    .EditValue = dt.Rows(0).Item("pi_id")
                End If
                .Properties.DropDownRows = 30
                .Properties.TextEditStyle = TextEditStyles.Standard
                .Properties.PopupWidth = 400
            End With
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub ptsfr_code_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ptsfr_code.ButtonClick
        Try
            Dim frm As New FTransferSearch()
            frm.set_win(Me)
            frm._en_id = pr_entity.EditValue
            frm._obj = ptsfr_code
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub

    Private Sub BtExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtExport.Click
        Dim sSQL As String
        Try
            Dim objConn As nPgSqlConnection
            Dim daT1 As nPgSqlDataAdapter


            objConn = New nPgSqlConnection(master_new.PGSqlConn.DbConString)
            objConn.Open()

            Dim Ds_export As DataSet
            Ds_export = New DataSet


            sSQL = "SELECT  " _
                & "  a.ptsfr_code, " _
                & "  a.ptsfr_date, " _
                & "  a.ptsfr_remarks " _
                & "FROM " _
                & "  public.ptsfr_mstr a " _
                & "WHERE " _
                & "  a.ptsfr_code = '" & ptsfr_code.Text & "'"


            daT1 = New nPgSqlDataAdapter(sSQL, objConn)
            daT1.Fill(Ds_export, "ti_master")


            sSQL = "SELECT  " _
                & "  c.pt_code, " _
                & "  c.pt_desc1, " _
                & "  d.code_code AS um_desc, " _
                & "  coalesce((SELECT t.pidd_price FROM public.pi_mstr r INNER JOIN public.pid_det s " _
                & "  ON(r.pi_oid = s.pid_pi_oid) INNER JOIN public.pidd_det t ON(s.pid_oid = t.pidd_pid_oid) " _
                & "  WHERE r.pi_id = " & pr_price_list.EditValue & " AND s.pid_pt_id = b.ptsfrd_pt_id ORDER BY t.pidd_min_qty LIMIT 1), 0) AS price, " _
                & "  c.pt_isbn, " _
                & "  b.ptsfrd_seq, " _
                & "  b.ptsfrd_qty,c.pt_ppn_type " _
                & "FROM " _
                & "  public.ptsfr_mstr a " _
                & "  INNER JOIN public.ptsfrd_det b ON (a.ptsfr_oid = b.ptsfrd_ptsfr_oid) " _
                & "  INNER JOIN public.pt_mstr c ON (b.ptsfrd_pt_id = c.pt_id) " _
                & "  INNER JOIN public.code_mstr d ON (c.pt_um = d.code_id) " _
                & "WHERE " _
                & "  a.ptsfr_code = '" & ptsfr_code.Text & "' " _
                & "ORDER BY " _
                & "  b.ptsfrd_seq"


            daT1 = New nPgSqlDataAdapter(sSQL, objConn)
            daT1.Fill(Ds_export, "ti_detail")


            gc_master.DataSource = Ds_export.Tables("ti_detail")
            gv_master.BestFitColumns()

            Dim objSaveFileDialog As New SaveFileDialog
            Dim filePath As String

            'Set the Save dialog properties
            'Dim _remark As String = ptsfr_code.Tag

            With objSaveFileDialog
                .DefaultExt = "xml"
                .FileName = ("Export_TI_" & Now().ToString("yyyyMMdd-HHmmss") & "_" & ptsfr_code.Text & "_" & ptsfr_code.Tag).ToString.Replace(":", "_")
                .Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*"
                .FilterIndex = 1
                .OverwritePrompt = True
                .Title = .FileName
            End With

            If objSaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then

                filePath = System.IO.Path.Combine( _
                    My.Computer.FileSystem.SpecialDirectories.MyDocuments, _
                    objSaveFileDialog.FileName)

                Ds_export.WriteXml(filePath, XmlWriteMode.WriteSchema)
            Else
                Exit Sub
            End If

            objConn.Close()
            objSaveFileDialog.Dispose()
            objSaveFileDialog = Nothing
            Box("Export Success")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub BtPrintBarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtPrintBarcode.Click
        Dim sSQL As String
        Try
            Dim objConn As nPgSqlConnection
            Dim daT1 As nPgSqlDataAdapter


            objConn = New nPgSqlConnection(master_new.PGSqlConn.DbConString)
            objConn.Open()

            Dim Ds_export As DataSet
            Ds_export = New DataSet


            sSQL = "SELECT  " _
                & "  a.ptsfr_code, " _
                & "  a.ptsfr_date, " _
                & "  a.ptsfr_remarks " _
                & "FROM " _
                & "  public.ptsfr_mstr a " _
                & "WHERE " _
                & "  a.ptsfr_code = '" & ptsfr_code.Text & "'"


            daT1 = New nPgSqlDataAdapter(sSQL, objConn)
            daT1.Fill(Ds_export, "ti_master")


            sSQL = "SELECT  " _
                & "  c.pt_code, " _
                & "  c.pt_desc1, " _
                & "  d.code_code AS um_desc, " _
                & "  coalesce((SELECT t.pidd_price FROM public.pi_mstr r INNER JOIN public.pid_det s " _
                & "  ON(r.pi_oid = s.pid_pi_oid) INNER JOIN public.pidd_det t ON(s.pid_oid = t.pidd_pid_oid) " _
                & "  WHERE r.pi_id = " & pr_price_list.EditValue & " AND s.pid_pt_id = b.ptsfrd_pt_id ORDER BY t.pidd_min_qty LIMIT 1), 0) AS price, " _
                & "  c.pt_isbn, " _
                & "  b.ptsfrd_seq, " _
                & "  b.ptsfrd_qty,c.pt_ppn_type " _
                & "FROM " _
                & "  public.ptsfr_mstr a " _
                & "  INNER JOIN public.ptsfrd_det b ON (a.ptsfr_oid = b.ptsfrd_ptsfr_oid) " _
                & "  INNER JOIN public.pt_mstr c ON (b.ptsfrd_pt_id = c.pt_id) " _
                & "  INNER JOIN public.code_mstr d ON (c.pt_um = d.code_id) " _
                & "WHERE " _
                & "  a.ptsfr_code = '" & ptsfr_code.Text & "' " _
                & "ORDER BY " _
                & "  b.ptsfrd_seq"


            daT1 = New nPgSqlDataAdapter(sSQL, objConn)
            daT1.Fill(Ds_export, "ti_detail")


            gc_master.DataSource = Ds_export.Tables("ti_detail")
            gv_master.BestFitColumns()
            sSQL = ""

            For Each dr As DataRow In Ds_export.Tables("ti_detail").Rows
                For i As Integer = 1 To SetNumber(dr("ptsfrd_qty"))

                    sSQL = sSQL & "SELECT  " _
                        & "  b.pt_code as kode_produk, " _
                        & "  b.pt_desc1 as nama_produk,'*' || coalesce(pt_code) || '*' as kode_barcode, " _
                        & SetNumber(dr("price")) & "  as harga_jual_pembulatan," & i & " as nomor " _
                        & "FROM " _
                        & "  public.pt_mstr b "


                    sSQL = sSQL & " Where pt_code = " & SetSetring(dr("pt_code"))

                    sSQL = sSQL & "  union all "
                Next
            Next

            If sSQL <> "" Then
                sSQL = Microsoft.VisualBasic.Left(sSQL, sSQL.Length - 10)
            End If

            sSQL = sSQL & " order by nama_produk, nomor "


            Dim rpt As New rptLabelHarga108
            With rpt
                Dim ds As New DataSet
                ds = MyPGDll.ClassReportDev.ReportDataset(sSQL)
                If ds.Tables(0).Rows.Count < 1 Then
                    Box("Maaf data kosong")
                    Exit Sub
                End If
                .DataSource = ds
                .DataMember = "Table"

                ' Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

                'printTool.PreviewForm.Text = "Tag Price"
                ''ps.Document.AutoFitToPagesWidth = 0
                ''ps.Document.ScaleFactor = 0.75F
                ''.PrintingSystem = ps
                'printTool.ShowPreview()

                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.PreviewForm.Text = "Tag Price"
                printTool.ShowPreview()

            End With


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        Dim sSQL As String
        Try
            Dim objConn As nPgSqlConnection
            Dim daT1 As nPgSqlDataAdapter


            objConn = New nPgSqlConnection(master_new.PGSqlConn.DbConString)
            objConn.Open()

            Dim Ds_export As DataSet
            Ds_export = New DataSet


            sSQL = "SELECT  " _
                & "  a.ptsfr_code, " _
                & "  a.ptsfr_date, " _
                & "  a.ptsfr_remarks " _
                & "FROM " _
                & "  public.ptsfr_mstr a " _
                & "WHERE " _
                & "  a.ptsfr_code = '" & ptsfr_code.Text & "'"


            daT1 = New nPgSqlDataAdapter(sSQL, objConn)
            daT1.Fill(Ds_export, "ti_master")


            sSQL = "SELECT  " _
                & "  c.pt_code, " _
                & "  c.pt_desc1, " _
                & "  d.code_code AS um_desc, " _
                & "  coalesce((SELECT t.pidd_price FROM public.pi_mstr r INNER JOIN public.pid_det s " _
                & "  ON(r.pi_oid = s.pid_pi_oid) INNER JOIN public.pidd_det t ON(s.pid_oid = t.pidd_pid_oid) " _
                & "  WHERE r.pi_id = " & pr_price_list.EditValue & " AND s.pid_pt_id = b.ptsfrd_pt_id ORDER BY t.pidd_min_qty LIMIT 1), 0) AS price, " _
                & "  c.pt_isbn, " _
                & "  b.ptsfrd_seq, " _
                & "  b.ptsfrd_qty,c.pt_ppn_type " _
                & "FROM " _
                & "  public.ptsfr_mstr a " _
                & "  INNER JOIN public.ptsfrd_det b ON (a.ptsfr_oid = b.ptsfrd_ptsfr_oid) " _
                & "  INNER JOIN public.pt_mstr c ON (b.ptsfrd_pt_id = c.pt_id) " _
                & "  INNER JOIN public.code_mstr d ON (c.pt_um = d.code_id) " _
                & "WHERE " _
                & "  a.ptsfr_code = '" & ptsfr_code.Text & "' " _
                & "ORDER BY " _
                & "  b.ptsfrd_seq"


            daT1 = New nPgSqlDataAdapter(sSQL, objConn)
            daT1.Fill(Ds_export, "ti_detail")


            gc_master.DataSource = Ds_export.Tables("ti_detail")
            gv_master.BestFitColumns()
            sSQL = ""

            For Each dr As DataRow In Ds_export.Tables("ti_detail").Rows
                For i As Integer = 1 To SetNumber(dr("ptsfrd_qty"))

                    sSQL = sSQL & "SELECT  " _
                        & "  b.pt_code as kode_produk, " _
                        & "  b.pt_desc1 as nama_produk,'*' || coalesce(pt_code) || '*' as kode_barcode, " _
                        & SetNumber(dr("price")) & "  as harga_jual_pembulatan," & i & " as nomor " _
                        & "FROM " _
                        & "  public.pt_mstr b "


                    sSQL = sSQL & " Where pt_code = " & SetSetring(dr("pt_code"))

                    sSQL = sSQL & "  union all "
                Next
            Next

            If sSQL <> "" Then
                sSQL = Microsoft.VisualBasic.Left(sSQL, sSQL.Length - 10)
            End If

            sSQL = sSQL & " order by nama_produk, nomor "


            Dim rpt As New rptLabelHarga108b
            With rpt
                Dim ds As New DataSet
                ds = MyPGDll.ClassReportDev.ReportDataset(sSQL)
                If ds.Tables(0).Rows.Count < 1 Then
                    Box("Maaf data kosong")
                    Exit Sub
                End If
                .DataSource = ds
                .DataMember = "Table"

                ' Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

                'printTool.PreviewForm.Text = "Tag Price"
                ''ps.Document.AutoFitToPagesWidth = 0
                ''ps.Document.ScaleFactor = 0.75F
                ''.PrintingSystem = ps
                'printTool.ShowPreview()

                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.PreviewForm.Text = "Tag Price"
                printTool.ShowPreview()

            End With


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
