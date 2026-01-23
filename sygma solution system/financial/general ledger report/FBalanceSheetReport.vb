Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports DevExpress.XtraReports
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.UI


Public Class FBalanceSheetReport
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _now As DateTime
    Dim ssql As String


    Private Sub FBalanceSheetReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            _now = func_coll.get_now
            de_first.DateTime = _now
            de_end.DateTime = _now

            dt_bantu = New DataTable
            dt_bantu = (func_data.load_dom_mstr())
            le_domain.Properties.DataSource = dt_bantu
            le_domain.Properties.DisplayMember = dt_bantu.Columns("dom_desc").ToString
            le_domain.Properties.ValueMember = dt_bantu.Columns("dom_id").ToString
            le_domain.ItemIndex = 0

            init_le(le_gcal, "gcal_mstr")
            init_le(le_entity, "en_mstr")
            ssql = "select DISTINCT ac_level from ac_mstr where ac_id<>0  order by ac_level "
            InsertCombo(ssql, Cb_Level)

            Cb_Level.EditValue = GetRowInfo("select max(ac_level) from ac_mstr")(0).ToString

            init_le(bdgt_type_code, "bdgtt_type")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub le_entity_EditalueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_entity.EditValueChanged

    End Sub

    Public Overrides Sub preview()

        Dim level, dom, en, sb, cc As Integer
        Dim posisi As String = ""
        Dim ssqls As New ArrayList


        Try
            dom = 0
            en = 0
            sb = 0
            cc = 0

            If le_domain.EditValue > 0 Then
                level = 1
                dom = CInt(le_domain.EditValue)
                If le_entity.EditValue > 0 Then
                    level = 2
                    en = CInt(le_entity.EditValue)
                End If
            Else
                level = 1
                dom = 1
            End If

            If CeDetail.EditValue = False Then

                If CEBudget.EditValue = True Then

                    ssql = "SELECT a.bs_number,  a.bs_caption,  a.bs_group,  a.bs_remarks,  b.bsd_number,  b.bsd_caption,  b.bsd_remarks, " _
                            & "c.bsdi_number,  c.bsdi_caption,  c.bsdi_oid, " _
                            & "  (select sum(jml) from (SELECT " _
                            & " (select sum(v_nilai) as nilai from ( SELECT  " _
                            & "  y.ac_id, " _
                            & "  y.ac_code_hirarki, " _
                            & "  y.ac_code, " _
                            & "  y.ac_name, " _
                            & "  y.ac_type,f_get_balance_sheet(y.ac_id," _
                            & "cast('" & le_gcal.EditValue.ToString & "' as uuid)," & SetSetring(bdgt_type_code.EditValue) & ") as v_nilai " _
                            & "FROM " _
                            & "  public.ac_mstr y " _
                            & "WHERE " _
                            & "  substring(y.ac_code_hirarki, 1, length(x.bsda_ac_hirarki)) = x.bsda_ac_hirarki AND  " _
                            & "  y.ac_is_sumlevel = 'N') as temp) as jml " _
                            & " FROM " _
                            & "  public.bsda_account x " _
                            & "WHERE " _
                            & "  x.bsda_bsdi_oid = c.bsdi_oid) as temp2)   as z_nilai  " _
                            & "FROM " _
                            & "  public.bs_mstr a " _
                            & "  INNER JOIN public.bsd_det b ON (a.bs_number = b.bsd_bs_number) " _
                            & "  INNER JOIN public.bsdi_det_item c ON (b.bsd_oid = c.bsdi_bsd_oid) "

                Else
                    ssql = "SELECT a.bs_number,  a.bs_caption,  a.bs_group,  a.bs_remarks,  b.bsd_number,  b.bsd_caption,  b.bsd_remarks, " _
                            & "c.bsdi_number,  c.bsdi_caption,  c.bsdi_oid, " _
                            & "  (select sum(jml) from (SELECT " _
                            & " (select sum(v_nilai) as nilai from ( SELECT  " _
                            & "  y.ac_id, " _
                            & "  y.ac_code_hirarki, " _
                            & "  y.ac_code, " _
                            & "  y.ac_name, " _
                            & "  y.ac_type,f_get_balance_sheet(y.ac_id," _
                            & level & "," & dom & "," & en & ",cast('" & le_gcal.EditValue.ToString & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai " _
                            & "FROM " _
                            & "  public.ac_mstr y " _
                            & "WHERE " _
                            & "  substring(y.ac_code_hirarki, 1, length(x.bsda_ac_hirarki)) = x.bsda_ac_hirarki AND  " _
                            & "  y.ac_is_sumlevel = 'N') as temp) as jml " _
                            & " FROM " _
                            & "  public.bsda_account x " _
                            & "WHERE " _
                            & "  x.bsda_bsdi_oid = c.bsdi_oid) as temp2)   as z_nilai  " _
                            & "FROM " _
                            & "  public.bs_mstr a " _
                            & "  INNER JOIN public.bsd_det b ON (a.bs_number = b.bsd_bs_number) " _
                            & "  INNER JOIN public.bsdi_det_item c ON (b.bsd_oid = c.bsdi_bsd_oid) "
                End If


                Dim rpt As New rptBalanceSheetReport
                With rpt
                    Dim ds As New DataSet
                    ds = ReportDataset(ssql)
                    If ds.Tables(0).Rows.Count < 1 Then
                        Box("Maaf data kosong")
                        Exit Sub
                    End If

                    '.vtglawal = tanggal.ToString
                    '.vtglakhir = EndOfMonth(tanggal, 0).ToString

                    '.vlevel = level
                    '.vdom = dom
                    '.ven = en
                    '.vsb = sb
                    '.vcc = cc
                    'System.Windows.Forms.Application.DoEvents()

                    If Ce_Posting.EditValue = True Then
                        .Posting_Option = True
                    Else
                        .Posting_Option = False
                    End If

                    ssql = "select * from dom_mstr where dom_id=" & le_domain.EditValue

                    Dim dt As New DataTable
                    dt = GetTableData(ssql)

                    For Each dr As DataRow In dt.Rows
                        .XrLabelTitle.Text = dr("dom_company")
                    Next

                    .periode = de_end.DateTime.ToString("dd MMMM yyyy")
                    .DataSource = ds
                    .DataMember = "Table"


                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

                    printTool.PreviewForm.Text = "Balance Sheet Report"
                    '.PrintingSystem = ps
                    printTool.ShowPreview()

                End With

            Else

                If CEBudget.EditValue = True Then
                    ssql = "SELECT a.bs_number,  a.bs_caption,  a.bs_group,  a.bs_remarks,  b.bsd_number,  b.bsd_caption,  b.bsd_remarks, " _
                       & "c.bsdi_number,  c.bsdi_caption,  c.bsdi_oid, y.ac_code, " _
                       & "  y.ac_name, " _
                       & "  y.ac_type,f_get_balance_sheet(y.ac_id," _
                       & "cast('" & le_gcal.EditValue.ToString & "' as uuid)," & SetSetring(bdgt_type_code.EditValue) & ") as z_nilai " _
                       & "FROM " _
                       & "  public.bs_mstr a " _
                       & "  INNER JOIN public.bsd_det b ON (a.bs_number = b.bsd_bs_number) " _
                       & "  INNER JOIN public.bsdi_det_item c ON (b.bsd_oid = c.bsdi_bsd_oid) " _
                       & "  INNER JOIN public.bsda_account x ON (x.bsda_bsdi_oid = c.bsdi_oid) " _
                       & "  INNER JOIN public.ac_mstr y ON (substring(y.ac_code_hirarki, 1, length(x.bsda_ac_hirarki)) = x.bsda_ac_hirarki)  " _
                       & " Where y.ac_is_sumlevel='N'"
                Else
                    ssql = "SELECT a.bs_number,  a.bs_caption,  a.bs_group,  a.bs_remarks,  b.bsd_number,  b.bsd_caption,  b.bsd_remarks, " _
                       & "c.bsdi_number,  c.bsdi_caption,  c.bsdi_oid, y.ac_code, " _
                       & "  y.ac_name, " _
                       & "  y.ac_type,f_get_balance_sheet(y.ac_id," _
                       & level & "," & dom & "," & en & ",cast('" & le_gcal.EditValue.ToString & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as z_nilai " _
                       & "FROM " _
                       & "  public.bs_mstr a " _
                       & "  INNER JOIN public.bsd_det b ON (a.bs_number = b.bsd_bs_number) " _
                       & "  INNER JOIN public.bsdi_det_item c ON (b.bsd_oid = c.bsdi_bsd_oid) " _
                       & "  INNER JOIN public.bsda_account x ON (x.bsda_bsdi_oid = c.bsdi_oid) " _
                       & "  INNER JOIN public.ac_mstr y ON (substring(y.ac_code_hirarki, 1, length(x.bsda_ac_hirarki)) = x.bsda_ac_hirarki)  " _
                       & " Where y.ac_is_sumlevel='N'"
                End If



                Dim rpt As New rptBalanceSheetDetailReport
                With rpt
                    Dim ds As New DataSet
                    ds = ReportDataset(ssql)
                    If ds.Tables(0).Rows.Count < 1 Then
                        Box("Maaf data kosong")
                        Exit Sub
                    End If

                    '.vtglawal = tanggal.ToString
                    '.vtglakhir = EndOfMonth(tanggal, 0).ToString

                    '.vlevel = level
                    '.vdom = dom
                    '.ven = en
                    '.vsb = sb
                    '.vcc = cc
                    'System.Windows.Forms.Application.DoEvents()

                    If Ce_Posting.EditValue = True Then
                        .Posting_Option = True
                    Else
                        .Posting_Option = False
                    End If

                    ssql = "select * from dom_mstr where dom_id=" & le_domain.EditValue

                    Dim dt As New DataTable
                    dt = GetTableData(ssql)

                    For Each dr As DataRow In dt.Rows
                        .XrLabelTitle.Text = dr("dom_company")
                    Next

                    .periode = de_end.DateTime.ToString("dd MMMM yyyy")
                    .DataSource = ds
                    .DataMember = "Table"


                    '  Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

                    'printTool.PreviewForm.Text = "Balance Sheet Report"
                    ''.PrintingSystem = ps
                    'printTool.ShowPreview()
                    'rpt.ShowPreviewMarginLines()
                    Dim printTool As New ReportPrintTool(rpt)
                    printTool.PreviewForm.Text = "Balance Sheet Report"
                    printTool.ShowPreview()


                End With

            End If

        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub

    Private Sub le_gcal_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_gcal.EditValueChanged
        Try
            de_first.EditValue = le_gcal.GetColumnValue("gcal_start_date")
            de_end.EditValue = le_gcal.GetColumnValue("gcal_end_date")

            If le_gcal.GetColumnValue("gcal_closing") = "Y" Then
                Ce_Posting.EditValue = True
            Else
                Ce_Posting.EditValue = False
            End If

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
