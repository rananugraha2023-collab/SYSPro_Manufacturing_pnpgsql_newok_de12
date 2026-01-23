Imports master_new.ModFunction
Imports master_new.PGSqlConn

Imports System.Net
Imports System.Text
Imports Newtonsoft.Json
Imports System.Collections.Generic
Imports Newtonsoft.Json.Linq
Imports System.Collections.Specialized
Imports System.IO

Public Class FGenerateReport
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim sSQL As String
    Dim _is_running As Boolean = False

    Private Sub FTaxInvoiceAttachmentPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        le_glperiode.Properties.DataSource = dt_bantu
        le_glperiode.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        le_glperiode.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        le_glperiode.ItemIndex = 0

        init_le(le_glperiode, "gcal_mstr")


    End Sub



    Public Overrides Sub preview()
        Dim ds_bantu As New DataSet
        Dim _sql As String

        '_sql = "select ti_code,  " _
        '        & " ti_date,  " _
        '        & " ar_code,  " _
        '        & " ar_date,  " _
        '        & " pt_code,  " _
        '        & " pt_desc1,  " _
        '        & " (tip_price - tip_disc) * tip_qty as tip_price,  " _
        '        & " tip_ppn * tip_qty as tip_ppn, ((tip_price - tip_disc) * tip_qty) + (tip_ppn * tip_qty) as tip_total,  " _
        '        & " code_name as sign_name, " _
        '        & " cmaddr_tax_line_3 " _
        '        & " from tip_pt " _
        '        & " inner join ti_mstr on ti_oid = tip_ti_oid " _
        '        & " inner join pt_mstr on pt_id = tip_pt_id " _
        '        & " inner join ars_ship on ars_soshipd_oid = tip_soshipd_oid " _
        '        & " inner join ar_mstr on ar_oid = ars_ar_oid " _
        '        & " inner join code_mstr on code_id = ti_sign_id " _
        '        & " inner join cmaddr_mstr on cmaddr_en_id = ti_en_id " _
        '        & " where ti_mstr.ti_code >= '" + be_first.Text + "'" _
        '        & " and ti_mstr.ti_code <= '" + be_to.Text + "'"

        Dim rpt As Object = Nothing

        rpt = New XRTaxInvoiceAttachment
        Try
            With rpt
                Try
                    Using objcb As New master_new.WDABasepgsql("", "")
                        With objcb
                            .SQL = _sql
                            .InitializeCommand()
                            .FillDataSet(ds_bantu, "data")
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try

                If ds_bantu.Tables(0).Rows.Count = 0 Then
                    MessageBox.Show("Data Doens't Exist.., Contact Your Admin Program..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                .DataSource = ds_bantu
                .DataMember = "data"
                .ShowPreview()

            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub BtGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtGenerate.Click
        Try
            _is_running = True
            sSQL = "SELECT  " _
                    & "  db_code, " _
                    & "  db_desc, " _
                    & "  db_active, " _
                    & "  db_host, " _
                    & "  db_port, " _
                    & "  db_database, " _
                    & "  db_user " _
                    & "FROM  " _
                    & "  public.dashboard_db_mstr  " _
                    & "WHERE db_code='DASHBOARD'"

            Dim dt_db As New DataTable
            dt_db = GetTableData(sSQL)

            Dim _report_oid As String = ""


            For Each dr_db As DataRow In dt_db.Rows
                sSQL = "SELECT report_oid, report_db from report_mstr where report_db='" & func_coll.get_conf_file("syspro_approval_code") & "'"

                Dim dt_report As New DataTable
                dt_report = GetTableData(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                For Each dr_report As DataRow In dt_report.Rows
                    _report_oid = dr_report("report_oid")
                Next
            Next


            sSQL = "SELECT  " _
                   & "  a.gcal_oid, " _
                   & "  a.gcal_dom_id, " _
                   & "  a.gcal_add_by, " _
                   & "  a.gcal_add_date, " _
                   & "  a.gcal_upd_by, " _
                   & "  a.gcal_upd_date, " _
                   & "  a.gcal_year, " _
                   & "  a.gcal_periode, " _
                   & "  a.gcal_start_date, " _
                   & "  a.gcal_end_date, " _
                   & "  a.gcal_dt, " _
                   & "  a.gcal_pra_closing, " _
                   & "  a.gcal_closing " _
                   & "FROM " _
                   & "  public.gcal_mstr a " _
                   & "where  " _
                   & "gcal_start_date >= (select gcal_start_date from gcal_mstr where gcal_oid='" & le_glperiode.EditValue & "') " _
                   & "order by gcal_start_date limit " & SetInteger(te_month.EditValue)


            Dim dt_gl_cal As New DataTable
            dt_gl_cal = GetTableData(sSQL)

            Dim _percent As Double = 0.0
            If cb_type.EditValue = "Balance Sheet" Then

                sSQL = "SELECT  " _
                   & "  a.dash_type_id, " _
                   & "  a.dash_type_number, " _
                   & "  a.dash_type_filter " _
                   & "FROM " _
                   & "  public.dashboard_setting a " _
                   & "WHERE " _
                   & "  a.dash_type_filter = 'BALANCE_SHEET' " _
                   & "ORDER BY " _
                   & "  a.dash_type_filter, " _
                   & "  a.dash_type_number"


                'sSQL = "SELECT  " _
                '  & "  a.dash_type_id, " _
                '  & "  a.dash_type_number, " _
                '  & "  a.dash_type_filter, " _
                '  & "  b.dashd_oid, " _
                '  & "  b.dashd_dash_type_id, " _
                '  & "  b.dashd_ac_id, " _
                '  & "  public.ac_mstr.ac_code, " _
                '  & "  public.ac_mstr.ac_code_hirarki, " _
                '  & "  public.ac_mstr.ac_name,0.0 as value " _
                '  & "FROM " _
                '  & "  public.dashboard_setting a " _
                '  & "  INNER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id) " _
                '  & "  LEFT OUTER JOIN public.ac_mstr ON (b.dashd_ac_id = public.ac_mstr.ac_id) " _
                '  & "WHERE " _
                '  & "  a.dash_type_filter = 'BALANCE_SHEET' " _
                '  & "ORDER BY " _
                '  & "  a.dash_type_filter, " _
                '  & "  a.dash_type_number"

                Dim dt_setting As New DataTable
                dt_setting = GetTableData(sSQL)


                Dim dt_bs As New DataTable
                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)

                For Each dr_gl_cal As DataRow In dt_gl_cal.Rows

                    sSQL = "select sum(z_nilai) as value, bs_caption from  " _
                      & "(SELECT a.bs_number,  a.bs_caption,  a.bs_group,   " _
                      & "a.bs_remarks,  b.bsd_number,  b.bsd_caption,   " _
                      & "b.bsd_remarks, c.bsdi_number,  c.bsdi_caption,   " _
                      & "c.bsdi_oid,    " _
                      & "(select sum(jml) from (SELECT  (select sum(v_nilai) as nilai from (  " _
                      & "SELECT y.ac_id,   y.ac_code_hirarki, y.ac_code,   y.ac_name,    " _
                      & "y.ac_type,f_get_balance_sheet(y.ac_id,1,1,0,cast('" & dr_gl_cal("gcal_oid").ToString & "' as uuid),'" & dr_gl_cal("gcal_closing") & "') as v_nilai  " _
                      & "FROM   public.ac_mstr y WHERE   substring(y.ac_code_hirarki, 1, length(x.bsda_ac_hirarki)) = x.bsda_ac_hirarki AND     " _
                      & "y.ac_is_sumlevel = 'N') as temp) as jml  FROM   public.bsda_account x WHERE   x.bsda_bsdi_oid = c.bsdi_oid) as temp2)   as z_nilai   " _
                      & "FROM   public.bs_mstr a    " _
                      & "INNER JOIN public.bsd_det b ON (a.bs_number = b.bsd_bs_number)    " _
                      & "INNER JOIN public.bsdi_det_item c ON (b.bsd_oid = c.bsdi_bsd_oid)  " _
                      & ") as tp group by bs_caption"

                    dt_bs = GetTableData(sSQL)

                    For Each dr_bs As DataRow In dt_bs.Rows
                        For Each dr_setting As DataRow In dt_setting.Rows
                            If dr_bs("bs_caption") = "HARTA" And dr_setting("dash_type_id") = "BALANCE_SHEET-ASSET" Then
                                dr_setting("value") = dr_bs("value")
                            ElseIf dr_bs("bs_caption") = "KEWAJIBAN" And dr_setting("dash_type_id") = "BALANCE_SHEET-KEWAJIBAN" Then
                                dr_setting("value") = dr_bs("value")
                            ElseIf dr_bs("bs_caption") = "MODAL" And dr_setting("dash_type_id") = "BALANCE_SHEET-MODAL" Then
                                dr_setting("value") = dr_bs("value")
                            End If
                        Next
                    Next

                    dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows

                        sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                            & " and reportd_type=" & SetSetring(dr_setting("dash_type_id")) & " and " _
                            & " reportd_periode= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM"))


                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next

                        sSQL = "INSERT INTO  " _
                            & "  public.reportd_detail " _
                            & "( " _
                            & "  reportd_oid, " _
                            & "  reportd_report_oid, " _
                            & "  reportd_type, " _
                            & "  reportd_periode, " _
                            & "  reportd_ac_id, " _
                            & "  reportd_ac_code, " _
                            & "  reportd_ac_hierarchy, " _
                            & "  reportd_ac_name,reportd_group," _
                            & "  reportd_value " _
                            & ") " _
                            & "VALUES ( " _
                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                            & SetSetring(_report_oid) & ",  " _
                            & SetSetring(dr_setting("dash_type_id")) & ",  " _
                            & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",'BALANCE_SHEET',   " _
                            & SetDec(dr_setting("value")) & "  " _
                            & ")"

                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next
                    Next


                    For Each dr_setting As DataRow In dt_setting.Rows
                        dr_setting("value") = 0.0
                    Next
                    dt_setting.AcceptChanges()

                    _processed = _processed + 1.0
                    _percent = _processed / _count * 100
                    LblStatus.Text = "Processing .. " & cb_type.EditValue & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()
                Next


            ElseIf cb_type.EditValue = "Profit Loss" Then


                sSQL = "SELECT  " _
                  & "  a.dash_type_id, " _
                  & "  a.dash_type_number, " _
                  & "  a.dash_type_filter, " _
                  & "  b.dashd_oid, " _
                  & "  b.dashd_dash_type_id, " _
                  & "  b.dashd_ac_id, " _
                  & "  public.ac_mstr.ac_code, " _
                  & "  public.ac_mstr.ac_code_hirarki, " _
                  & "  public.ac_mstr.ac_name,0.0 as value " _
                  & "FROM " _
                  & "  public.dashboard_setting a " _
                  & "  LEFT OUTER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id) " _
                  & "  LEFT OUTER JOIN public.ac_mstr ON (b.dashd_ac_id = public.ac_mstr.ac_id) " _
                  & "WHERE " _
                  & "  a.dash_type_filter = 'PROFIT_LOSS' " _
                  & "ORDER BY " _
                  & "  a.dash_type_filter, " _
                  & "  a.dash_type_number"

                Dim dt_setting As New DataTable
                dt_setting = GetTableData(sSQL)


                Dim dt_bs As New DataTable

                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)

                For Each dr_gl_cal As DataRow In dt_gl_cal.Rows


                    sSQL = "SELECT    a.pl_oid,   a.pl_footer,   a.pl_sign,   a.pl_number,    " _
                      & "b.pls_oid,   b.pls_item,   b.pls_number,   c.pla_ac_id,   d.ac_code,    " _
                      & "d.ac_name,   c.pla_ac_hirarki,(select  sum(v_nilai) as jml from  " _
                      & "( SELECT    x.ac_id,   x.ac_code_hirarki,   x.ac_code,   x.ac_name,    " _
                      & "x.ac_type,f_get_balance_sheet_pl(x.ac_id,1,1,0,cast('" & dr_gl_cal("gcal_oid") & "' as uuid),'" & dr_gl_cal("gcal_closing") & "') as v_nilai  " _
                      & "FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1, length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
                      & "ac_is_sumlevel = 'N') as temp) * pls_value as value FROM   public.pl_setting_mstr a    " _
                      & "INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid)    " _
                      & "INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid)    " _
                      & "INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id) ORDER BY   a.pl_number,   b.pls_number "


                    dt_bs = GetTableData(sSQL)
                    Dim _penjualan, _hpp, _biaya_op, _laba_kotor, _laba_operasi, _laba_sebelum_pajak, _pendapatan_lain, _biaya_lain As Double
                    Dim _gpm, _om, _margin_laba_bersih, _percent_hpp, _percent_adm_umum, _percent_total_biaya As Double
                    _penjualan = 0.0
                    _hpp = 0.0
                    _biaya_op = 0.0
                    _laba_kotor = 0.0
                    _laba_operasi = 0.0
                    _laba_sebelum_pajak = 0.0
                    _pendapatan_lain = 0.0
                    _biaya_lain = 0.0


                    _gpm = 0.0
                    _om = 0.0
                    _margin_laba_bersih = 0.0
                    _percent_hpp = 0.0
                    _percent_adm_umum = 0.0
                    _percent_total_biaya = 0.0



                    For Each dr_bs As DataRow In dt_bs.Rows
                        For Each dr_setting As DataRow In dt_setting.Rows
                            If dr_bs("pl_footer") = "Penjualan Bersih" And dr_setting("dash_type_id") = "PROFIT_LOSS-PENDAPATAN" Then
                                dr_setting("value") = dr_setting("value") + dr_bs("value")
                                _penjualan = _penjualan + dr_bs("value")
                            ElseIf dr_bs("pl_footer") = "Laba Rugi Kotor" And dr_setting("dash_type_id") = "PROFIT_LOSS-HPP" Then
                                dr_setting("value") = dr_setting("value") + dr_bs("value")
                                _hpp = _hpp + dr_bs("value")
                            ElseIf dr_bs("pl_footer") = "Laba Rugi Operasional" And dr_setting("dash_type_id") = "PROFIT_LOSS-BEBAN OPERASIONAL" Then
                                dr_setting("value") = dr_setting("value") + dr_bs("value")
                                _biaya_op = _biaya_op + dr_bs("value")

                            ElseIf dr_bs("pls_item") = "Pendapatan Lain-Lain" And dr_setting("dash_type_id") = "PROFIT_LOSS-PENDAPATAN LAIN-LAIN" Then

                                dr_setting("value") = dr_setting("value") + dr_bs("value")
                                _pendapatan_lain = _pendapatan_lain + dr_bs("value")

                            ElseIf dr_bs("pls_item") = "Biaya Lain-Lain" And dr_setting("dash_type_id") = "PROFIT_LOSS-BIAYA LAIN-LAIN" Then
                                dr_setting("value") = dr_setting("value") + dr_bs("value")
                                _biaya_lain = _biaya_lain + dr_bs("value")
                            End If


                        Next
                    Next

                    _laba_kotor = _penjualan + _hpp
                    _laba_operasi = _laba_kotor + _biaya_op
                    _laba_sebelum_pajak = _laba_operasi + _pendapatan_lain + _biaya_lain
                    Try
                        _gpm = _laba_kotor / _penjualan
                    Catch ex As Exception

                    End Try

                    Try
                        _om = _laba_operasi / _penjualan
                    Catch ex As Exception

                    End Try
                    Try
                        _margin_laba_bersih = _laba_sebelum_pajak / _penjualan
                    Catch ex As Exception

                    End Try

                    Try
                        _percent_hpp = (_hpp * -1.0) / _penjualan
                    Catch ex As Exception

                    End Try

                    Try
                        _percent_adm_umum = (_biaya_op * -1.0) / _penjualan
                    Catch ex As Exception

                    End Try
                    Try
                        _percent_total_biaya = ((_biaya_op + _biaya_lain + _hpp) * -1.0) / (_penjualan + _pendapatan_lain)
                    Catch ex As Exception

                    End Try


                    dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows
                        If dr_setting("dash_type_id") = "PROFIT_LOSS-LABA KOTOR" Then
                            dr_setting("value") = _laba_kotor
                        ElseIf dr_setting("dash_type_id") = "PROFIT_LOSS-LABA OPERASI" Then
                            dr_setting("value") = _laba_operasi

                        ElseIf dr_setting("dash_type_id") = "PROFIT_LOSS-LABA SEBELUM PAJAK" Then
                            dr_setting("value") = _laba_sebelum_pajak


                        ElseIf dr_setting("dash_type_id") = "PROFIT_LOSS-MARGIN LABA KOTOR (GPM)" Then
                            dr_setting("value") = _gpm
                        ElseIf dr_setting("dash_type_id") = "PROFIT_LOSS-MARGIN LABA OPERASI (OM)" Then
                            dr_setting("value") = _om
                        ElseIf dr_setting("dash_type_id") = "PROFIT_LOSS-MARGIN LABA BERSIH" Then
                            dr_setting("value") = _margin_laba_bersih
                        ElseIf dr_setting("dash_type_id") = "PROFIT_LOSS-% HPP" Then
                            dr_setting("value") = _percent_hpp
                        ElseIf dr_setting("dash_type_id") = "PROFIT_LOSS-% BIAYA ADM & UMUM" Then
                            dr_setting("value") = _percent_adm_umum
                        ElseIf dr_setting("dash_type_id") = "PROFIT_LOSS-% TOTAL BIAYA" Then
                            dr_setting("value") = _percent_total_biaya
                        End If
                    Next

                    dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows

                        sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                            & " and reportd_type=" & SetSetring(dr_setting("dash_type_id")) & " and " _
                            & " reportd_periode= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM"))


                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next

                        sSQL = "INSERT INTO  " _
                            & "  public.reportd_detail " _
                            & "( " _
                            & "  reportd_oid, " _
                            & "  reportd_report_oid, " _
                            & "  reportd_type, " _
                            & "  reportd_periode, " _
                            & "  reportd_ac_id, " _
                            & "  reportd_ac_code, " _
                            & "  reportd_ac_hierarchy, " _
                            & "  reportd_ac_name, " _
                            & "  reportd_value " _
                            & ") " _
                            & "VALUES ( " _
                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                            & SetSetring(_report_oid) & ",  " _
                            & SetSetring(dr_setting("dash_type_id")) & ",  " _
                            & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetDec(dr_setting("value")) & "  " _
                            & ")"

                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next
                    Next


                    For Each dr_setting As DataRow In dt_setting.Rows
                        dr_setting("value") = 0.0
                    Next
                    dt_setting.AcceptChanges()

                    _processed = _processed + 1.0
                    _percent = _processed / _count * 100
                    LblStatus.Text = "Processing .. " & cb_type.EditValue & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()
                Next


            ElseIf cb_type.EditValue = "Cash and Accounts Receivable" Then

                Dim dt_bs As New DataTable
                Dim dt_ac As New DataTable


                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)
                Dim _last_date As Date
                _last_date = DateAdd(DateInterval.Month, te_month.EditValue, CDate(le_glperiode.GetColumnValue("gcal_start_date")))
                Dim _count_day As Integer
                _count_day = DateDiff(DateInterval.Day, CDate(le_glperiode.GetColumnValue("gcal_start_date")), _last_date)
                Dim _date_current As Date


                For n As Integer = 0 To _count_day - 1


                    _date_current = DateAdd(DateInterval.Day, n, CDate(le_glperiode.GetColumnValue("gcal_start_date")))

                    sSQL = "SELECT  " _
                        & " 'CASH_AR-KAS' as type, sum((public.glt_det.glt_debit * public.glt_det.glt_exc_rate) - (public.glt_det.glt_credit * public.glt_det.glt_exc_rate)) AS glt_value " _
                        & "FROM " _
                        & "  public.glt_det " _
                        & "WHERE " _
                        & "  public.glt_det.glt_date = " & SetDateNTime00(_date_current) & " AND  " _
                        & "  public.glt_det.glt_ac_id in (SELECT  x.ac_id FROM   public.dashboard_setting a   " _
                        & "INNER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id)  " _
                        & " LEFT OUTER JOIN public.ac_mstr c ON (b.dashd_ac_id = c.ac_id)   " _
                        & "LEFT OUTER JOIN public.ac_mstr x ON (substring(x.ac_code_hirarki, 1, length(c.ac_code_hirarki)) = c.ac_code_hirarki)   " _
                        & "WHERE   a.dash_type_id in ('ACCOUNT-KAS','ACCOUNT-BANK') and x.ac_is_sumlevel='N' ) union all " _
                        & "SELECT  " _
                       & " 'CASH_AR-PIUTANG USAHA' as type, sum((public.glt_det.glt_debit * public.glt_det.glt_exc_rate) - (public.glt_det.glt_credit * public.glt_det.glt_exc_rate)) AS glt_value " _
                       & "FROM " _
                       & "  public.glt_det " _
                       & "WHERE " _
                       & "  public.glt_det.glt_date = " & SetDateNTime00(_date_current) & " AND  " _
                       & "  public.glt_det.glt_ac_id in (SELECT  x.ac_id FROM   public.dashboard_setting a   " _
                       & "INNER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id)  " _
                       & " LEFT OUTER JOIN public.ac_mstr c ON (b.dashd_ac_id = c.ac_id)   " _
                       & "LEFT OUTER JOIN public.ac_mstr x ON (substring(x.ac_code_hirarki, 1, length(c.ac_code_hirarki)) = c.ac_code_hirarki)   " _
                       & "WHERE   a.dash_type_id ='ACCOUNT-PIUTANG USAHA' and x.ac_is_sumlevel='N' )"

                    dt_ac = GetTableData(sSQL)

                    For Each dr_ac As DataRow In dt_ac.Rows
                        If SetNumber(dr_ac("glt_value")) <> 0 Then
                            sSQL = "delete from reportd_detail_sub where reportd_report_oid=" & SetSetring(_report_oid) _
                                & " and reportd_type=" & SetSetring(dr_ac("type")) & " and reportd_group='CASH_AR' and " _
                                & " reportd_periode= " & SetSetring(Format(_date_current, "yyyyMMdd"))


                            For Each dr_db As DataRow In dt_db.Rows
                                DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                            Next

                            sSQL = "INSERT INTO  " _
                                & "  public.reportd_detail_sub " _
                                & "( " _
                                & "  reportd_oid, " _
                                & "  reportd_report_oid, " _
                                & "  reportd_type, " _
                                & "  reportd_periode, " _
                                & "  reportd_ac_id, " _
                                & "  reportd_ac_code, " _
                                & "  reportd_ac_hierarchy, " _
                                & "  reportd_ac_name,reportd_date,reportd_group, reportd_date_generate," _
                                & "  reportd_value " _
                                & ") " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(_report_oid) & ",  " _
                                & SetSetring(dr_ac("type")) & ",  " _
                                & SetSetring(Format(_date_current, "yyyyMMdd")) & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetDateNTime00(_date_current) & ", 'CASH_AR', " _
                                & SetDateNTime(CekTanggal) & ",  " _
                                & SetDec(dr_ac("glt_value")) & "  " _
                                & ")"

                            For Each dr_db As DataRow In dt_db.Rows
                                DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                            Next
                        End If

                    Next



                    _percent = (n + 1) / _count_day * 100
                    LblStatus.Text = "Processing .. " & cb_type.EditValue & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()

                Next

            ElseIf cb_type.EditValue = "Cash In vs Cash Out" Then
                Dim dt_ac As New DataTable
                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)
                For Each dr_gl_cal As DataRow In dt_gl_cal.Rows
                    sSQL = "SELECT  " _
                       & " 'CASH-CASH IN' as type, sum((public.glt_det.glt_debit * public.glt_det.glt_exc_rate)) AS glt_value " _
                       & "FROM " _
                       & "  public.glt_det " _
                       & "WHERE " _
                       & " to_char(public.glt_det.glt_date,'yyyyMM') = " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & " AND  " _
                       & "  public.glt_det.glt_ac_id in (SELECT  x.ac_id FROM   public.dashboard_setting a   " _
                       & "INNER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id)  " _
                       & " LEFT OUTER JOIN public.ac_mstr c ON (b.dashd_ac_id = c.ac_id)   " _
                       & "LEFT OUTER JOIN public.ac_mstr x ON (substring(x.ac_code_hirarki, 1, length(c.ac_code_hirarki)) = c.ac_code_hirarki)   " _
                       & "WHERE   a.dash_type_id in ('ACCOUNT-KAS','ACCOUNT-BANK') and x.ac_is_sumlevel='N' ) union all " _
                        & "SELECT  " _
                       & " 'CASH-CASH OUT' as type, sum((public.glt_det.glt_credit * public.glt_det.glt_exc_rate)) AS glt_value " _
                       & "FROM " _
                       & "  public.glt_det " _
                       & "WHERE " _
                       & "  to_char(public.glt_det.glt_date,'yyyyMM')= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & " AND  " _
                       & "  public.glt_det.glt_ac_id in (SELECT  x.ac_id FROM   public.dashboard_setting a   " _
                       & "INNER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id)  " _
                       & " LEFT OUTER JOIN public.ac_mstr c ON (b.dashd_ac_id = c.ac_id)   " _
                       & "LEFT OUTER JOIN public.ac_mstr x ON (substring(x.ac_code_hirarki, 1, length(c.ac_code_hirarki)) = c.ac_code_hirarki)   " _
                       & "WHERE   a.dash_type_id in ('ACCOUNT-KAS','ACCOUNT-BANK') and x.ac_is_sumlevel='N' )"

                    dt_ac = GetTableData(sSQL)
                    For Each dr_ac As DataRow In dt_ac.Rows
                        If SetNumber(dr_ac("glt_value")) <> 0 Then
                            sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                              & " and reportd_type=" & SetSetring(dr_ac("type")) & "  and reportd_group='CASH_IN_CASH_OUT' and " _
                              & " reportd_periode= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM"))


                            For Each dr_db As DataRow In dt_db.Rows
                                DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                            Next

                            sSQL = "INSERT INTO  " _
                                & "  public.reportd_detail " _
                                & "( " _
                                & "  reportd_oid, " _
                                & "  reportd_report_oid, " _
                                & "  reportd_type, " _
                                & "  reportd_periode, " _
                                & "  reportd_ac_id, " _
                                & "  reportd_ac_code, " _
                                & "  reportd_ac_hierarchy, " _
                                & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                                & "  reportd_value " _
                                & ") " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(_report_oid) & ",  " _
                                & SetSetring(dr_ac("type")) & ",  " _
                                & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & "'CASH_IN_CASH_OUT', " _
                                & SetDateNTime(CekTanggal) & ",  " _
                                & SetDec(dr_ac("glt_value")) & "  " _
                                & ")"

                            For Each dr_db As DataRow In dt_db.Rows
                                DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                            Next
                        End If
                    Next
                    _processed = _processed + 1.0
                    _percent = _processed / _count * 100
                    LblStatus.Text = "Processing .. " & cb_type.EditValue & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()
                Next

            ElseIf cb_type.EditValue = "AR Customer" Then

                sSQL = "SELECT  " _
                    & " b.ptnr_code, b.ptnr_name, sum(a.ar_amount  * a.ar_exc_rate) AS ar_amount, sum(coalesce(a.ar_pay_amount, 0.0) * a.ar_exc_rate) AS ar_payment_amount, sum((a.ar_amount - coalesce(a.ar_pay_amount, 0.0)) * a.ar_exc_rate) AS ar_outstanding_amount, " _
                    & " min(a.ar_due_date) as first_due_date, max(a.ar_due_date) as last_due_date " _
                    & "FROM " _
                    & "  public.ar_mstr a " _
                    & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
                    & "WHERE " _
                    & "  a.ar_status IS NULL  " _
                    & "  GROUP by ptnr_code, ptnr_name"

                Dim dt_data As New DataTable
                dt_data = GetTableData(sSQL)

                sSQL = "delete from reportd_detail_customer_piutang where reportd_report_oid=" & SetSetring(_report_oid)

                For Each dr_db As DataRow In dt_db.Rows
                    DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                Next

                For Each dr_data As DataRow In dt_data.Rows
                    sSQL = "INSERT INTO  " _
                        & "  public.reportd_detail_customer_piutang " _
                        & "( " _
                        & "  reportd_oid, " _
                        & "  reportd_report_oid, " _
                        & "  reportd_type, " _
                        & "  reportd_date_generate, " _
                        & "  reportd_ptnr_code, " _
                        & "  reportd_ptnr_name, " _
                        & "  reportd_ar_amount, " _
                        & "  reportd_ar_payment_amount, " _
                        & "  reportd_ar_outstanding_amount, " _
                        & "  reportd_first_duedate, " _
                        & "  reportd_last_duedate " _
                        & ") " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_report_oid) & ",  " _
                        & SetSetring("AR_CUSTOMER-AR CUSTOMER") & ",  " _
                        & SetDateNTime00(CekTanggal()) & ",  " _
                        & SetSetring(dr_data("ptnr_code")) & ",  " _
                        & SetSetring(dr_data("ptnr_name")) & ",  " _
                        & SetDecDB(dr_data("ar_amount")) & ",  " _
                        & SetDecDB(dr_data("ar_payment_amount")) & ",  " _
                        & SetDecDB(dr_data("ar_outstanding_amount")) & ",  " _
                        & SetDateNTime00(dr_data("first_due_date")) & ",  " _
                        & SetDateNTime00(dr_data("last_due_date")) & "  " _
                        & ")"
                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                    Next


                Next

            ElseIf cb_type.EditValue = "AR Aging" Then
                sSQL = "SELECT  " _
                    & "ar_code,c.en_desc,b.ptnr_code,   " _
                    & "b.ptnr_name,  a.ar_due_date, " _
                    & "a.ar_amount  * a.ar_exc_rate AS ar_amount,  " _
                    & "coalesce(a.ar_pay_amount, 0.0) * a.ar_exc_rate AS ar_payment_amount,  " _
                    & "(a.ar_amount - coalesce(a.ar_pay_amount, 0.0)) * a.ar_exc_rate AS ar_outstanding_amount " _
                    & "FROM " _
                    & "  public.ar_mstr a " _
                    & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
                    & "  INNER JOIN public.en_mstr c ON (a.ar_en_id = c.en_id) " _
                    & "WHERE " _
                    & "  a.ar_status IS NULL " _
                    & ""

                Dim dt_data As New DataTable
                dt_data = GetTableData(sSQL)

                sSQL = "delete from reportd_detail_ar_aging where reportd_report_oid=" & SetSetring(_report_oid)

                For Each dr_db As DataRow In dt_db.Rows
                    DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                Next

                For Each dr_data As DataRow In dt_data.Rows
                    sSQL = "INSERT INTO  " _
                        & "  public.reportd_detail_ar_aging " _
                        & "( " _
                        & "  reportd_oid, " _
                        & "  reportd_report_oid, " _
                        & "  reportd_type, " _
                        & "  reportd_date_generate, " _
                        & "  reportd_ptnr_code, " _
                        & "  reportd_ptnr_name, " _
                        & "  reportd_ar_amount, " _
                        & "  reportd_ar_payment_amount, " _
                        & "  reportd_ar_outstanding_amount, " _
                        & "  reportd_duedate, " _
                        & "  reportd_ar_code,reportd_en_desc " _
                        & ") " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_report_oid) & ",  " _
                        & SetSetring("AR_AGING-AR AGING") & ",  " _
                        & SetDateNTime00(CekTanggal()) & ",  " _
                        & SetSetring(dr_data("ptnr_code")) & ",  " _
                        & SetSetring(dr_data("ptnr_name")) & ",  " _
                        & SetDecDB(dr_data("ar_amount")) & ",  " _
                        & SetDecDB(dr_data("ar_payment_amount")) & ",  " _
                        & SetDecDB(dr_data("ar_outstanding_amount")) & ",  " _
                        & SetDateNTime00(dr_data("ar_due_date")) & ",  " _
                        & SetSetring(dr_data("ar_code")) & " , " _
                        & SetSetring(dr_data("en_desc")) & "  " _
                        & ")"
                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                    Next


                Next


            ElseIf cb_type.EditValue = "AR Overview" Then
                sSQL = "SELECT  " _
                    & "'AR_OVERVIEW-AR AMOUNT' as type, sum((a.ar_amount - coalesce(a.ar_pay_amount, 0.0)) * a.ar_exc_rate) AS amount " _
                    & "FROM " _
                    & "  public.ar_mstr a " _
                    & "WHERE " _
                    & "  a.ar_status IS NULL  " _
                    & "union all " _
                    & "SELECT  " _
                    & "'AR_OVERVIEW-AR DUE' as type, sum((a.ar_amount - coalesce(a.ar_pay_amount, 0.0)) * a.ar_exc_rate) AS amount " _
                    & "FROM " _
                    & "  public.ar_mstr a " _
                    & "WHERE " _
                    & "  a.ar_status IS NULL and a.ar_due_date <= current_date " _
                    & "  union all " _
                    & "SELECT  " _
                    & "'AR_OVERVIEW-AR PAYMENT' as type, sum(b.arpayd_amount * b.arpayd_exc_rate) AS amount " _
                    & "FROM " _
                    & "  public.arpay_payment a " _
                    & "  inner join arpayd_det b on a.arpay_oid=b.arpayd_arpay_oid  " _
                    & "WHERE " _
                    & "  to_char(a.arpay_eff_date,'yyyyMM')= to_char(current_date,'yyyyMM')"


                Dim dt_data As New DataTable
                dt_data = GetTableData(sSQL)



                For Each dr_data As DataRow In dt_data.Rows

                    sSQL = "delete from reportd_detail where reportd_type=" & SetSetring(dr_data("type")) & " and reportd_report_oid=" & SetSetring(_report_oid)

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                    Next

                    sSQL = "INSERT INTO  " _
                           & "  public.reportd_detail " _
                           & "( " _
                           & "  reportd_oid, " _
                           & "  reportd_report_oid, " _
                           & "  reportd_type, " _
                           & "  reportd_periode, " _
                           & "  reportd_ac_id, " _
                           & "  reportd_ac_code, " _
                           & "  reportd_ac_hierarchy, " _
                           & "  reportd_ac_name, reportd_date_generate," _
                           & "  reportd_value " _
                           & ") " _
                           & "VALUES ( " _
                           & SetSetring(Guid.NewGuid.ToString) & ",  " _
                           & SetSetring(_report_oid) & ",  " _
                           & SetSetring(dr_data("type")) & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetDateNTime(CekTanggal) & ",  " _
                           & SetDec(SetNumber(dr_data("amount"))) & "  " _
                           & ")"

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))

                    Next

                Next





            ElseIf cb_type.EditValue = "AP Suplier" Then

                sSQL = "SELECT  " _
                    & " b.ptnr_code, b.ptnr_name, sum(a.ap_amount  * a.ap_exc_rate) AS ap_amount, sum(coalesce(a.ap_pay_amount, 0.0) * a.ap_exc_rate) AS ap_payment_amount, sum((a.ap_amount - coalesce(a.ap_pay_amount, 0.0)) * a.ap_exc_rate) AS ap_outstanding_amount, " _
                    & " min(a.ap_due_date) as first_due_date, max(a.ap_due_date) as last_due_date " _
                    & "FROM " _
                    & "  public.ap_mstr a " _
                    & "  INNER JOIN public.ptnr_mstr b ON (a.ap_ptnr_id = b.ptnr_id) " _
                    & "WHERE " _
                    & "  a.ap_status IS NULL  " _
                    & "  GROUP by ptnr_code, ptnr_name"

                Dim dt_data As New DataTable
                dt_data = GetTableData(sSQL)

                sSQL = "delete from reportd_detail_suplier_hutang where reportd_report_oid=" & SetSetring(_report_oid)

                For Each dr_db As DataRow In dt_db.Rows
                    DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                Next

                For Each dr_data As DataRow In dt_data.Rows
                    sSQL = "INSERT INTO  " _
                        & "  public.reportd_detail_suplier_hutang " _
                        & "( " _
                        & "  reportd_oid, " _
                        & "  reportd_report_oid, " _
                        & "  reportd_type, " _
                        & "  reportd_date_generate, " _
                        & "  reportd_ptnr_code, " _
                        & "  reportd_ptnr_name, " _
                        & "  reportd_ap_amount, " _
                        & "  reportd_ap_payment_amount, " _
                        & "  reportd_ap_outstanding_amount, " _
                        & "  reportd_first_duedate, " _
                        & "  reportd_last_duedate " _
                        & ") " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_report_oid) & ",  " _
                        & SetSetring("AP_SUPLIER-AP SUPLIER") & ",  " _
                        & SetDateNTime00(CekTanggal()) & ",  " _
                        & SetSetring(dr_data("ptnr_code")) & ",  " _
                        & SetSetring(dr_data("ptnr_name")) & ",  " _
                        & SetDecDB(dr_data("ap_amount")) & ",  " _
                        & SetDecDB(dr_data("ap_payment_amount")) & ",  " _
                        & SetDecDB(dr_data("ap_outstanding_amount")) & ",  " _
                        & SetDateNTime00(dr_data("first_due_date")) & ",  " _
                        & SetDateNTime00(dr_data("last_due_date")) & "  " _
                        & ")"
                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                    Next


                Next

            ElseIf cb_type.EditValue = "AP Aging" Then
                sSQL = "SELECT  " _
                    & "ap_code,c.en_desc,b.ptnr_code,   " _
                    & "b.ptnr_name,  a.ap_due_date, " _
                    & "a.ap_amount  * a.ap_exc_rate AS ap_amount,  " _
                    & "coalesce(a.ap_pay_amount, 0.0) * a.ap_exc_rate AS ap_payment_amount,  " _
                    & "(a.ap_amount - coalesce(a.ap_pay_amount, 0.0)) * a.ap_exc_rate AS ap_outstanding_amount " _
                    & "FROM " _
                    & "  public.ap_mstr a " _
                    & "  INNER JOIN public.ptnr_mstr b ON (a.ap_ptnr_id = b.ptnr_id) " _
                    & "  INNER JOIN public.en_mstr c ON (a.ap_en_id = c.en_id) " _
                    & "WHERE " _
                    & "  a.ap_status IS NULL " _
                    & ""

                Dim dt_data As New DataTable
                dt_data = GetTableData(sSQL)

                sSQL = "delete from reportd_detail_ap_aging where reportd_report_oid=" & SetSetring(_report_oid)

                For Each dr_db As DataRow In dt_db.Rows
                    DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                Next

                For Each dr_data As DataRow In dt_data.Rows
                    sSQL = "INSERT INTO  " _
                        & "  public.reportd_detail_ap_aging " _
                        & "( " _
                        & "  reportd_oid, " _
                        & "  reportd_report_oid, " _
                        & "  reportd_type, " _
                        & "  reportd_date_generate, " _
                        & "  reportd_ptnr_code, " _
                        & "  reportd_ptnr_name, " _
                        & "  reportd_ap_amount, " _
                        & "  reportd_ap_payment_amount, " _
                        & "  reportd_ap_outstanding_amount, " _
                        & "  reportd_duedate, " _
                        & "  reportd_ap_code,reportd_en_desc " _
                        & ") " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_report_oid) & ",  " _
                        & SetSetring("AP_AGING-AP AGING") & ",  " _
                        & SetDateNTime00(CekTanggal()) & ",  " _
                        & SetSetring(dr_data("ptnr_code")) & ",  " _
                        & SetSetring(dr_data("ptnr_name")) & ",  " _
                        & SetDecDB(dr_data("ap_amount")) & ",  " _
                        & SetDecDB(dr_data("ap_payment_amount")) & ",  " _
                        & SetDecDB(dr_data("ap_outstanding_amount")) & ",  " _
                        & SetDateNTime00(dr_data("ap_due_date")) & ",  " _
                        & SetSetring(dr_data("ap_code")) & " , " _
                        & SetSetring(dr_data("en_desc")) & "  " _
                        & ")"
                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                    Next


                Next


            ElseIf cb_type.EditValue = "AP Overview" Then
                sSQL = "SELECT  " _
                    & "'AP_OVERVIEW-AP AMOUNT' as type, sum((a.ap_amount - coalesce(a.ap_pay_amount, 0.0)) * a.ap_exc_rate) AS amount " _
                    & "FROM " _
                    & "  public.ap_mstr a " _
                    & "WHERE " _
                    & "  a.ap_status IS NULL  " _
                    & "union all " _
                    & "SELECT  " _
                    & "'AP_OVERVIEW-AP DUE' as type, sum((a.ap_amount - coalesce(a.ap_pay_amount, 0.0)) * a.ap_exc_rate) AS amount " _
                    & "FROM " _
                    & "  public.ap_mstr a " _
                    & "WHERE " _
                    & "  a.ap_status IS NULL and a.ap_due_date <= current_date " _
                    & "  union all " _
                    & "SELECT  " _
                    & "'AP_OVERVIEW-AP PAYMENT' as type, sum(b.appayd_amount * b.appayd_exc_rate) AS amount " _
                    & "FROM " _
                    & "  public.appay_payment a " _
                    & "  inner join appayd_det b on a.appay_oid=b.appayd_appay_oid  " _
                    & "WHERE " _
                    & "  to_char(a.appay_eff_date,'yyyyMM')= to_char(current_date,'yyyyMM')"


                Dim dt_data As New DataTable
                dt_data = GetTableData(sSQL)



                For Each dr_data As DataRow In dt_data.Rows

                    sSQL = "delete from reportd_detail where reportd_type=" & SetSetring(dr_data("type")) & " and reportd_report_oid=" & SetSetring(_report_oid)

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                    Next

                    sSQL = "INSERT INTO  " _
                           & "  public.reportd_detail " _
                           & "( " _
                           & "  reportd_oid, " _
                           & "  reportd_report_oid, " _
                           & "  reportd_type, " _
                           & "  reportd_periode, " _
                           & "  reportd_ac_id, " _
                           & "  reportd_ac_code, " _
                           & "  reportd_ac_hierarchy, " _
                           & "  reportd_ac_name,reportd_date_generate, " _
                           & "  reportd_value " _
                           & ") " _
                           & "VALUES ( " _
                           & SetSetring(Guid.NewGuid.ToString) & ",  " _
                           & SetSetring(_report_oid) & ",  " _
                           & SetSetring(dr_data("type")) & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetDateNTime(CekTanggal) & ",  " _
                           & SetDec(SetNumber(dr_data("amount"))) & "  " _
                           & ")"

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))

                    Next

                Next


            ElseIf cb_type.EditValue = "AP" Then

                Dim dt_bs As New DataTable
                Dim dt_ac As New DataTable


                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)
                Dim _last_date As Date
                _last_date = DateAdd(DateInterval.Month, te_month.EditValue, CDate(le_glperiode.GetColumnValue("gcal_start_date")))
                Dim _count_day As Integer
                _count_day = DateDiff(DateInterval.Day, CDate(le_glperiode.GetColumnValue("gcal_start_date")), _last_date)
                Dim _date_current As Date


                sSQL = "select dash_type_id,dash_type_filter from dashboard_setting where dash_type_filter='AP'"
                Dim dt_ap As New DataTable
                dt_ap = GetTableData(sSQL)


                For n As Integer = 0 To _count_day - 1

                    For Each dr_ap As DataRow In dt_ap.Rows
                        _date_current = DateAdd(DateInterval.Day, n, CDate(le_glperiode.GetColumnValue("gcal_start_date")))

                        sSQL = "SELECT  " _
                            & " '" & dr_ap("dash_type_id") & "' as type, sum(((public.glt_det.glt_debit * public.glt_det.glt_exc_rate) - (public.glt_det.glt_credit * public.glt_det.glt_exc_rate)) * -1.0) AS glt_value " _
                            & "FROM " _
                            & "  public.glt_det " _
                            & "WHERE " _
                            & "  public.glt_det.glt_date = " & SetDateNTime00(_date_current) & " AND  " _
                            & "  public.glt_det.glt_ac_id in (SELECT  x.ac_id FROM   public.dashboard_setting a   " _
                            & "INNER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id)  " _
                            & " LEFT OUTER JOIN public.ac_mstr c ON (b.dashd_ac_id = c.ac_id)   " _
                            & "LEFT OUTER JOIN public.ac_mstr x ON (substring(x.ac_code_hirarki, 1, length(c.ac_code_hirarki)) = c.ac_code_hirarki)   " _
                            & "WHERE   a.dash_type_id ='" & dr_ap("dash_type_id") & "' and x.ac_is_sumlevel='N' )"

                        dt_ac = GetTableData(sSQL)

                        For Each dr_ac As DataRow In dt_ac.Rows
                            If SetNumber(dr_ac("glt_value")) <> 0 Then
                                sSQL = "delete from reportd_detail_sub where reportd_report_oid=" & SetSetring(_report_oid) _
                                & " and reportd_type=" & SetSetring(dr_ap("dash_type_id")) & " and reportd_group='" & dr_ap("dash_type_filter") & "' and " _
                                & " reportd_periode= " & SetSetring(Format(_date_current, "yyyyMMdd"))


                                For Each dr_db As DataRow In dt_db.Rows
                                    DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                                Next

                                sSQL = "INSERT INTO  " _
                                    & "  public.reportd_detail_sub " _
                                    & "( " _
                                    & "  reportd_oid, " _
                                    & "  reportd_report_oid, " _
                                    & "  reportd_type, " _
                                    & "  reportd_periode, " _
                                    & "  reportd_ac_id, " _
                                    & "  reportd_ac_code, " _
                                    & "  reportd_ac_hierarchy, " _
                                    & "  reportd_ac_name,reportd_date,reportd_group,reportd_date_generate, " _
                                    & "  reportd_value " _
                                    & ") " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(_report_oid) & ",  " _
                                    & SetSetring(dr_ap("dash_type_id")) & ",  " _
                                    & SetSetring(Format(_date_current, "yyyyMMdd")) & ",  " _
                                    & SetSetring("") & ",  " _
                                    & SetSetring("") & ",  " _
                                    & SetSetring("") & ",  " _
                                    & SetSetring("") & ",  " _
                                    & SetDateNTime00(_date_current) & ", '" & dr_ap("dash_type_filter") & "', " _
                                    & SetDateNTime(CekTanggal) & ",  " _
                                    & SetDec(dr_ac("glt_value")) & "  " _
                                    & ")"

                                For Each dr_db As DataRow In dt_db.Rows
                                    DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                                Next
                            End If

                        Next

                    Next



                    _percent = (n + 1) / _count_day * 100
                    LblStatus.Text = "Processing .. " & cb_type.EditValue & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()

                Next


            ElseIf cb_type.EditValue = "Sales and Purchase" Then

                Dim _date As String
                _date = "to_char(to_date('" & le_glperiode.GetColumnValue("gcal_start_date") & "', 'dd/MM/yyyy'), 'yyyyMM')" '"current_date"


                sSQL = "SELECT      sum(soshipd_qty * -1.0 * sod_price) as sales_ttl, " _
                    & "sum((soshipd_qty * -1.0 * sod_price) - (soshipd_qty * -1.0 * sod_price * sod_disc) ) as sales_nett  " _
                    & "FROM    public.soship_mstr    " _
                    & "inner join soshipd_det on soshipd_soship_oid = soship_oid    " _
                    & "inner join so_mstr on so_oid = soship_so_oid    " _
                    & "inner join pi_mstr on so_pi_id = pi_id    " _
                    & "inner join sod_det on sod_oid = soshipd_sod_oid   " _
                    & "inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person    " _
                    & "inner join en_mstr on en_id = soship_en_id    " _
                    & "inner join si_mstr on si_id = soship_si_id    " _
                    & "inner join ptnr_mstr on ptnr_mstr.ptnr_id = so_ptnr_id_sold    " _
                    & "inner join pt_mstr y on y.pt_id = sod_pt_id    " _
                    & "inner join code_mstr as um_mstr on um_mstr.code_id = soshipd_um    " _
                    & "inner join loc_mstr on loc_id = soshipd_loc_id    " _
                    & "inner join cu_mstr on cu_id = so_cu_id    " _
                    & "inner join code_mstr as tax_mstr on tax_mstr.code_id = sod_tax_class    " _
                    & "inner join code_mstr pay_type on pay_type.code_id = so_pay_type    " _
                    & "left outer join code_mstr as reason_mstr on reason_mstr.code_id = soshipd_rea_code_id    " _
                    & "left outer join ars_ship on ars_soshipd_oid = soshipd_oid    " _
                    & "left outer join ar_mstr on ar_oid = ars_ar_oid    " _
                    & "left outer join tia_ar on tia_ar_oid = ar_oid    " _
                    & "left outer join ti_mstr on ti_oid = tia_ti_oid    " _
                    & "left outer join ptnrg_grp on ptnrg_grp.ptnrg_id = ptnr_mstr.ptnr_ptnrg_id    " _
                    & "inner join pl_mstr on pl_id = pt_pl_id    " _
                    & "left outer join pt_mstr x on so_pt_id = x.pt_id    " _
                    & "where   to_char(soship_date,'yyyyMM')= " & _date


                Dim dt_data As New DataTable
                dt_data = GetTableData(sSQL)

                sSQL = "delete from reportd_detail where reportd_group=" & SetSetring("SALES") & " and reportd_report_oid=" & SetSetring(_report_oid)

                For Each dr_db As DataRow In dt_db.Rows
                    DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                Next

                For Each dr_data As DataRow In dt_data.Rows

                    sSQL = "INSERT INTO  " _
                           & "  public.reportd_detail " _
                           & "( " _
                           & "  reportd_oid, " _
                           & "  reportd_report_oid, " _
                           & "  reportd_type, " _
                           & "  reportd_periode, " _
                           & "  reportd_ac_id, " _
                           & "  reportd_ac_code, " _
                           & "  reportd_ac_hierarchy, " _
                           & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                           & "  reportd_value " _
                           & ") " _
                           & "VALUES ( " _
                           & SetSetring(Guid.NewGuid.ToString) & ",  " _
                           & SetSetring(_report_oid) & ",  " _
                           & SetSetring("SALES-BRUTO") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ", 'SALES', " _
                           & SetDateNTime(CekTanggal) & ",  " _
                           & SetDec(SetNumber(dr_data("sales_ttl"))) & "  " _
                           & ")"

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))

                    Next

                    sSQL = "INSERT INTO  " _
                          & "  public.reportd_detail " _
                          & "( " _
                          & "  reportd_oid, " _
                          & "  reportd_report_oid, " _
                          & "  reportd_type, " _
                          & "  reportd_periode, " _
                          & "  reportd_ac_id, " _
                          & "  reportd_ac_code, " _
                          & "  reportd_ac_hierarchy, " _
                          & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                          & "  reportd_value " _
                          & ") " _
                          & "VALUES ( " _
                          & SetSetring(Guid.NewGuid.ToString) & ",  " _
                          & SetSetring(_report_oid) & ",  " _
                          & SetSetring("SALES-NETTO") & ",  " _
                          & SetSetring("") & ",  " _
                          & SetSetring("") & ",  " _
                          & SetSetring("") & ",  " _
                          & SetSetring("") & ",  " _
                          & SetSetring("") & ", 'SALES', " _
                          & SetDateNTime(CekTanggal) & ",  " _
                          & SetDec(SetNumber(dr_data("sales_nett"))) & "  " _
                          & ")"

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                    Next
                Next


                sSQL = "select distinct ptnr_mstr.ptnr_code,ptnr_mstr.ptnr_name " _
                    & "FROM    public.soship_mstr    " _
                    & "inner join soshipd_det on soshipd_soship_oid = soship_oid    " _
                    & "inner join so_mstr on so_oid = soship_so_oid    " _
                    & "inner join pi_mstr on so_pi_id = pi_id    " _
                    & "inner join sod_det on sod_oid = soshipd_sod_oid   " _
                    & "inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person    " _
                    & "inner join en_mstr on en_id = soship_en_id    " _
                    & "inner join si_mstr on si_id = soship_si_id    " _
                    & "inner join ptnr_mstr on ptnr_mstr.ptnr_id = so_ptnr_id_sold    " _
                    & "inner join pt_mstr y on y.pt_id = sod_pt_id    " _
                    & "inner join code_mstr as um_mstr on um_mstr.code_id = soshipd_um    " _
                    & "inner join loc_mstr on loc_id = soshipd_loc_id    " _
                    & "inner join cu_mstr on cu_id = so_cu_id    " _
                    & "inner join code_mstr as tax_mstr on tax_mstr.code_id = sod_tax_class    " _
                    & "inner join code_mstr pay_type on pay_type.code_id = so_pay_type    " _
                    & "left outer join code_mstr as reason_mstr on reason_mstr.code_id = soshipd_rea_code_id    " _
                    & "left outer join ars_ship on ars_soshipd_oid = soshipd_oid    " _
                    & "left outer join ar_mstr on ar_oid = ars_ar_oid    " _
                    & "left outer join tia_ar on tia_ar_oid = ar_oid    " _
                    & "left outer join ti_mstr on ti_oid = tia_ti_oid    " _
                    & "left outer join ptnrg_grp on ptnrg_grp.ptnrg_id = ptnr_mstr.ptnr_ptnrg_id    " _
                    & "inner join pl_mstr on pl_id = pt_pl_id    " _
                    & "left outer join pt_mstr x on so_pt_id = x.pt_id    " _
                    & "where to_char(soship_date,'yyyyMM')= " & _date


                dt_data = GetTableData(sSQL)

                If dt_data.Rows.Count > 0 Then
                    sSQL = "INSERT INTO  " _
                     & "  public.reportd_detail " _
                     & "( " _
                     & "  reportd_oid, " _
                     & "  reportd_report_oid, " _
                     & "  reportd_type, " _
                     & "  reportd_periode, " _
                     & "  reportd_ac_id, " _
                     & "  reportd_ac_code, " _
                     & "  reportd_ac_hierarchy, " _
                     & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                     & "  reportd_value " _
                     & ") " _
                     & "VALUES ( " _
                     & SetSetring(Guid.NewGuid.ToString) & ",  " _
                     & SetSetring(_report_oid) & ",  " _
                     & SetSetring("SALES-JUMLAH PELANGGAN") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ", 'SALES', " _
                     & SetDateNTime(CekTanggal) & ",  " _
                     & SetDec(SetNumber(dt_data.Rows.Count)) & "  " _
                     & ")"

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))

                    Next
                End If


                sSQL = "select distinct so_code " _
                    & "FROM    public.soship_mstr    " _
                    & "inner join soshipd_det on soshipd_soship_oid = soship_oid    " _
                    & "inner join so_mstr on so_oid = soship_so_oid    " _
                    & "inner join pi_mstr on so_pi_id = pi_id    " _
                    & "inner join sod_det on sod_oid = soshipd_sod_oid   " _
                    & "inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person    " _
                    & "inner join en_mstr on en_id = soship_en_id    " _
                    & "inner join si_mstr on si_id = soship_si_id    " _
                    & "inner join ptnr_mstr on ptnr_mstr.ptnr_id = so_ptnr_id_sold    " _
                    & "inner join pt_mstr y on y.pt_id = sod_pt_id    " _
                    & "inner join code_mstr as um_mstr on um_mstr.code_id = soshipd_um    " _
                    & "inner join loc_mstr on loc_id = soshipd_loc_id    " _
                    & "inner join cu_mstr on cu_id = so_cu_id    " _
                    & "inner join code_mstr as tax_mstr on tax_mstr.code_id = sod_tax_class    " _
                    & "inner join code_mstr pay_type on pay_type.code_id = so_pay_type    " _
                    & "left outer join code_mstr as reason_mstr on reason_mstr.code_id = soshipd_rea_code_id    " _
                    & "left outer join ars_ship on ars_soshipd_oid = soshipd_oid    " _
                    & "left outer join ar_mstr on ar_oid = ars_ar_oid    " _
                    & "left outer join tia_ar on tia_ar_oid = ar_oid    " _
                    & "left outer join ti_mstr on ti_oid = tia_ti_oid    " _
                    & "left outer join ptnrg_grp on ptnrg_grp.ptnrg_id = ptnr_mstr.ptnr_ptnrg_id    " _
                    & "inner join pl_mstr on pl_id = pt_pl_id    " _
                    & "left outer join pt_mstr x on so_pt_id = x.pt_id    " _
                    & "where to_char(soship_date,'yyyyMM')= " & _date


                dt_data = GetTableData(sSQL)

                If dt_data.Rows.Count > 0 Then
                    sSQL = "INSERT INTO  " _
                     & "  public.reportd_detail " _
                     & "( " _
                     & "  reportd_oid, " _
                     & "  reportd_report_oid, " _
                     & "  reportd_type, " _
                     & "  reportd_periode, " _
                     & "  reportd_ac_id, " _
                     & "  reportd_ac_code, " _
                     & "  reportd_ac_hierarchy, " _
                     & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                     & "  reportd_value " _
                     & ") " _
                     & "VALUES ( " _
                     & SetSetring(Guid.NewGuid.ToString) & ",  " _
                     & SetSetring(_report_oid) & ",  " _
                     & SetSetring("SALES-JUMLAH TRANSAKSI") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ", 'SALES', " _
                     & SetDateNTime(CekTanggal) & ",  " _
                     & SetDec(SetNumber(dt_data.Rows.Count)) & "  " _
                     & ")"

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))

                    Next
                End If

                sSQL = "select distinct y.pt_code " _
                  & "FROM    public.soship_mstr    " _
                  & "inner join soshipd_det on soshipd_soship_oid = soship_oid    " _
                  & "inner join so_mstr on so_oid = soship_so_oid    " _
                  & "inner join pi_mstr on so_pi_id = pi_id    " _
                  & "inner join sod_det on sod_oid = soshipd_sod_oid   " _
                  & "inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person    " _
                  & "inner join en_mstr on en_id = soship_en_id    " _
                  & "inner join si_mstr on si_id = soship_si_id    " _
                  & "inner join ptnr_mstr on ptnr_mstr.ptnr_id = so_ptnr_id_sold    " _
                  & "inner join pt_mstr y on y.pt_id = sod_pt_id    " _
                  & "inner join code_mstr as um_mstr on um_mstr.code_id = soshipd_um    " _
                  & "inner join loc_mstr on loc_id = soshipd_loc_id    " _
                  & "inner join cu_mstr on cu_id = so_cu_id    " _
                  & "inner join code_mstr as tax_mstr on tax_mstr.code_id = sod_tax_class    " _
                  & "inner join code_mstr pay_type on pay_type.code_id = so_pay_type    " _
                  & "left outer join code_mstr as reason_mstr on reason_mstr.code_id = soshipd_rea_code_id    " _
                  & "left outer join ars_ship on ars_soshipd_oid = soshipd_oid    " _
                  & "left outer join ar_mstr on ar_oid = ars_ar_oid    " _
                  & "left outer join tia_ar on tia_ar_oid = ar_oid    " _
                  & "left outer join ti_mstr on ti_oid = tia_ti_oid    " _
                  & "left outer join ptnrg_grp on ptnrg_grp.ptnrg_id = ptnr_mstr.ptnr_ptnrg_id    " _
                  & "inner join pl_mstr on pl_id = pt_pl_id    " _
                  & "left outer join pt_mstr x on so_pt_id = x.pt_id    " _
                  & "where to_char(soship_date,'yyyyMM')= " & _date


                dt_data = GetTableData(sSQL)

                If dt_data.Rows.Count > 0 Then
                    sSQL = "INSERT INTO  " _
                     & "  public.reportd_detail " _
                     & "( " _
                     & "  reportd_oid, " _
                     & "  reportd_report_oid, " _
                     & "  reportd_type, " _
                     & "  reportd_periode, " _
                     & "  reportd_ac_id, " _
                     & "  reportd_ac_code, " _
                     & "  reportd_ac_hierarchy, " _
                     & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                     & "  reportd_value " _
                     & ") " _
                     & "VALUES ( " _
                     & SetSetring(Guid.NewGuid.ToString) & ",  " _
                     & SetSetring(_report_oid) & ",  " _
                     & SetSetring("SALES-PRODUK TERJUAL") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ", 'SALES', " _
                     & SetDateNTime(CekTanggal) & ",  " _
                     & SetDec(SetNumber(dt_data.Rows.Count)) & "  " _
                     & ")"

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))

                    Next
                End If



                sSQL = "SELECT  " _
                        & "  sum(public.rcvd_det.rcvd_qty * public.pod_det.pod_cost) AS purchase_ttl, " _
                        & "  sum((public.rcvd_det.rcvd_qty * public.pod_det.pod_cost) - (public.rcvd_det.rcvd_qty * public.pod_det.pod_cost * public.pod_det.pod_disc)) AS purchase_net " _
                        & "FROM " _
                        & "  public.rcv_mstr " _
                        & "  INNER JOIN public.rcvd_det ON (public.rcv_mstr.rcv_oid = public.rcvd_det.rcvd_rcv_oid) " _
                        & "  INNER JOIN public.po_mstr ON (public.rcv_mstr.rcv_po_oid = public.po_mstr.po_oid) " _
                        & "  INNER JOIN public.pod_det ON (public.rcvd_det.rcvd_pod_oid = public.pod_det.pod_oid) " _
                        & "WHERE " _
                        & "  to_char(public.rcv_mstr.rcv_eff_date, 'yyyyMM') = " & _date


                dt_data = GetTableData(sSQL)

                sSQL = "delete from reportd_detail where reportd_group=" & SetSetring("PURCHASE") & " and reportd_report_oid=" & SetSetring(_report_oid)

                For Each dr_db As DataRow In dt_db.Rows
                    DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                Next


                For Each dr_data As DataRow In dt_data.Rows

                    sSQL = "INSERT INTO  " _
                           & "  public.reportd_detail " _
                           & "( " _
                           & "  reportd_oid, " _
                           & "  reportd_report_oid, " _
                           & "  reportd_type, " _
                           & "  reportd_periode, " _
                           & "  reportd_ac_id, " _
                           & "  reportd_ac_code, " _
                           & "  reportd_ac_hierarchy, " _
                           & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                           & "  reportd_value " _
                           & ") " _
                           & "VALUES ( " _
                           & SetSetring(Guid.NewGuid.ToString) & ",  " _
                           & SetSetring(_report_oid) & ",  " _
                           & SetSetring("PURCHASE-BRUTO") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ", 'PURCHASE', " _
                           & SetDateNTime(CekTanggal) & ",  " _
                           & SetDec(SetNumber(dr_data("purchase_ttl"))) & "  " _
                           & ")"

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))

                    Next

                    sSQL = "INSERT INTO  " _
                          & "  public.reportd_detail " _
                          & "( " _
                          & "  reportd_oid, " _
                          & "  reportd_report_oid, " _
                          & "  reportd_type, " _
                          & "  reportd_periode, " _
                          & "  reportd_ac_id, " _
                          & "  reportd_ac_code, " _
                          & "  reportd_ac_hierarchy, " _
                          & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                          & "  reportd_value " _
                          & ") " _
                          & "VALUES ( " _
                          & SetSetring(Guid.NewGuid.ToString) & ",  " _
                          & SetSetring(_report_oid) & ",  " _
                          & SetSetring("PURCHASE-NETTO") & ",  " _
                          & SetSetring("") & ",  " _
                          & SetSetring("") & ",  " _
                          & SetSetring("") & ",  " _
                          & SetSetring("") & ",  " _
                          & SetSetring("") & ", 'PURCHASE', " _
                          & SetDateNTime(CekTanggal) & ",  " _
                          & SetDec(SetNumber(dr_data("purchase_net"))) & "  " _
                          & ")"

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                    Next
                Next


                sSQL = "SELECT  " _
                        & "  distinct po_ptnr_id " _
                        & "FROM " _
                        & "  public.rcv_mstr " _
                        & "  INNER JOIN public.rcvd_det ON (public.rcv_mstr.rcv_oid = public.rcvd_det.rcvd_rcv_oid) " _
                        & "  INNER JOIN public.po_mstr ON (public.rcv_mstr.rcv_po_oid = public.po_mstr.po_oid) " _
                        & "  INNER JOIN public.pod_det ON (public.rcvd_det.rcvd_pod_oid = public.pod_det.pod_oid) " _
                        & "WHERE " _
                        & "  to_char(public.rcv_mstr.rcv_eff_date, 'yyyyMM') = " & _date


                dt_data = GetTableData(sSQL)

                If dt_data.Rows.Count > 0 Then
                    sSQL = "INSERT INTO  " _
                     & "  public.reportd_detail " _
                     & "( " _
                     & "  reportd_oid, " _
                     & "  reportd_report_oid, " _
                     & "  reportd_type, " _
                     & "  reportd_periode, " _
                     & "  reportd_ac_id, " _
                     & "  reportd_ac_code, " _
                     & "  reportd_ac_hierarchy, " _
                     & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                     & "  reportd_value " _
                     & ") " _
                     & "VALUES ( " _
                     & SetSetring(Guid.NewGuid.ToString) & ",  " _
                     & SetSetring(_report_oid) & ",  " _
                     & SetSetring("PURCHASE-JUMLAH PELANGGAN") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ", 'PURCHASE', " _
                     & SetDateNTime(CekTanggal) & ",  " _
                     & SetDec(SetNumber(dt_data.Rows.Count)) & "  " _
                     & ")"

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))

                    Next
                End If




                sSQL = "SELECT  " _
                       & "  distinct po_code " _
                       & "FROM " _
                       & "  public.rcv_mstr " _
                       & "  INNER JOIN public.rcvd_det ON (public.rcv_mstr.rcv_oid = public.rcvd_det.rcvd_rcv_oid) " _
                       & "  INNER JOIN public.po_mstr ON (public.rcv_mstr.rcv_po_oid = public.po_mstr.po_oid) " _
                       & "  INNER JOIN public.pod_det ON (public.rcvd_det.rcvd_pod_oid = public.pod_det.pod_oid) " _
                       & "WHERE " _
                       & "  to_char(public.rcv_mstr.rcv_eff_date, 'yyyyMM') = " & _date


                dt_data = GetTableData(sSQL)

                If dt_data.Rows.Count > 0 Then
                    sSQL = "INSERT INTO  " _
                     & "  public.reportd_detail " _
                     & "( " _
                     & "  reportd_oid, " _
                     & "  reportd_report_oid, " _
                     & "  reportd_type, " _
                     & "  reportd_periode, " _
                     & "  reportd_ac_id, " _
                     & "  reportd_ac_code, " _
                     & "  reportd_ac_hierarchy, " _
                     & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                     & "  reportd_value " _
                     & ") " _
                     & "VALUES ( " _
                     & SetSetring(Guid.NewGuid.ToString) & ",  " _
                     & SetSetring(_report_oid) & ",  " _
                     & SetSetring("PURCHASE-JUMLAH TRANSAKSI") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ", 'PURCHASE', " _
                     & SetDateNTime(CekTanggal) & ",  " _
                     & SetDec(SetNumber(dt_data.Rows.Count)) & "  " _
                     & ")"

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))

                    Next
                End If


                sSQL = "SELECT  " _
                      & "  distinct pod_pt_id " _
                      & "FROM " _
                      & "  public.rcv_mstr " _
                      & "  INNER JOIN public.rcvd_det ON (public.rcv_mstr.rcv_oid = public.rcvd_det.rcvd_rcv_oid) " _
                      & "  INNER JOIN public.po_mstr ON (public.rcv_mstr.rcv_po_oid = public.po_mstr.po_oid) " _
                      & "  INNER JOIN public.pod_det ON (public.rcvd_det.rcvd_pod_oid = public.pod_det.pod_oid) " _
                      & "WHERE " _
                      & "  to_char(public.rcv_mstr.rcv_eff_date, 'yyyyMM') = " & _date ' to_char(to_date('1/1/2021', 'dd/MM/yyyy'), 'yyyyMM')


                dt_data = GetTableData(sSQL)

                If dt_data.Rows.Count > 0 Then
                    sSQL = "INSERT INTO  " _
                     & "  public.reportd_detail " _
                     & "( " _
                     & "  reportd_oid, " _
                     & "  reportd_report_oid, " _
                     & "  reportd_type, " _
                     & "  reportd_periode, " _
                     & "  reportd_ac_id, " _
                     & "  reportd_ac_code, " _
                     & "  reportd_ac_hierarchy, " _
                     & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                     & "  reportd_value " _
                     & ") " _
                     & "VALUES ( " _
                     & SetSetring(Guid.NewGuid.ToString) & ",  " _
                     & SetSetring(_report_oid) & ",  " _
                     & SetSetring("PURCHASE-PRODUK TERBELI") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ", 'PURCHASE', " _
                     & SetDateNTime(CekTanggal) & ",  " _
                     & SetDec(SetNumber(dt_data.Rows.Count)) & "  " _
                     & ")"

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))

                    Next
                End If


            ElseIf cb_type.EditValue = "Sales Chart" Then

                Dim dt_ac As New DataTable
                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)
                For Each dr_gl_cal As DataRow In dt_gl_cal.Rows


                    sSQL = "SELECT      sum(soshipd_qty * -1.0 * sod_price) as sales_ttl, " _
                    & "sum((soshipd_qty * -1.0 * sod_price) - (soshipd_qty * -1.0 * sod_price * sod_disc) ) as sales_nett  " _
                    & "FROM    public.soship_mstr    " _
                    & "inner join soshipd_det on soshipd_soship_oid = soship_oid    " _
                    & "inner join so_mstr on so_oid = soship_so_oid    " _
                    & "inner join pi_mstr on so_pi_id = pi_id    " _
                    & "inner join sod_det on sod_oid = soshipd_sod_oid   " _
                    & "inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person    " _
                    & "inner join en_mstr on en_id = soship_en_id    " _
                    & "inner join si_mstr on si_id = soship_si_id    " _
                    & "inner join ptnr_mstr on ptnr_mstr.ptnr_id = so_ptnr_id_sold    " _
                    & "inner join pt_mstr y on y.pt_id = sod_pt_id    " _
                    & "inner join code_mstr as um_mstr on um_mstr.code_id = soshipd_um    " _
                    & "inner join loc_mstr on loc_id = soshipd_loc_id    " _
                    & "inner join cu_mstr on cu_id = so_cu_id    " _
                    & "inner join code_mstr as tax_mstr on tax_mstr.code_id = sod_tax_class    " _
                    & "inner join code_mstr pay_type on pay_type.code_id = so_pay_type    " _
                    & "left outer join code_mstr as reason_mstr on reason_mstr.code_id = soshipd_rea_code_id    " _
                    & "left outer join ars_ship on ars_soshipd_oid = soshipd_oid    " _
                    & "left outer join ar_mstr on ar_oid = ars_ar_oid    " _
                    & "left outer join tia_ar on tia_ar_oid = ar_oid    " _
                    & "left outer join ti_mstr on ti_oid = tia_ti_oid    " _
                    & "left outer join ptnrg_grp on ptnrg_grp.ptnrg_id = ptnr_mstr.ptnr_ptnrg_id    " _
                    & "inner join pl_mstr on pl_id = pt_pl_id    " _
                    & "left outer join pt_mstr x on so_pt_id = x.pt_id    " _
                    & "where   to_char(soship_date,'yyyyMM')= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ""

                    dt_ac = GetTableData(sSQL)
                    For Each dr_ac As DataRow In dt_ac.Rows
                        If SetNumber(dr_ac("sales_ttl")) <> 0 Or SetNumber(dr_ac("sales_nett")) <> 0 Then
                            sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                              & " and reportd_type=" & SetSetring("SALES_CHART-BRUTO") & "  and reportd_group='SALES_CHART' and " _
                              & " reportd_periode= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM"))


                            For Each dr_db As DataRow In dt_db.Rows
                                DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                            Next

                            sSQL = "INSERT INTO  " _
                                & "  public.reportd_detail " _
                                & "( " _
                                & "  reportd_oid, " _
                                & "  reportd_report_oid, " _
                                & "  reportd_type, " _
                                & "  reportd_periode, " _
                                & "  reportd_ac_id, " _
                                & "  reportd_ac_code, " _
                                & "  reportd_ac_hierarchy, " _
                                & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                                & "  reportd_value " _
                                & ") " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(_report_oid) & ",  " _
                                & SetSetring("SALES_CHART-BRUTO") & ",  " _
                                & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & "'SALES_CHART', " _
                                & SetDateNTime(CekTanggal) & ",  " _
                                & SetDec(dr_ac("sales_ttl")) & "  " _
                                & ")"

                            For Each dr_db As DataRow In dt_db.Rows
                                DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                            Next


                            sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                              & " and reportd_type=" & SetSetring("SALES_CHART-NETTO") & "  and reportd_group='SALES_CHART' and " _
                              & " reportd_periode= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM"))


                            For Each dr_db As DataRow In dt_db.Rows
                                DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                            Next

                            sSQL = "INSERT INTO  " _
                                & "  public.reportd_detail " _
                                & "( " _
                                & "  reportd_oid, " _
                                & "  reportd_report_oid, " _
                                & "  reportd_type, " _
                                & "  reportd_periode, " _
                                & "  reportd_ac_id, " _
                                & "  reportd_ac_code, " _
                                & "  reportd_ac_hierarchy, " _
                                & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                                & "  reportd_value " _
                                & ") " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(_report_oid) & ",  " _
                                & SetSetring("SALES_CHART-NETTO") & ",  " _
                                & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & "'SALES_CHART', " _
                                & SetDateNTime(CekTanggal) & ",  " _
                                & SetDec(dr_ac("sales_nett")) & "  " _
                                & ")"

                            For Each dr_db As DataRow In dt_db.Rows
                                DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                            Next
                        End If
                    Next
                    _processed = _processed + 1.0
                    _percent = _processed / _count * 100
                    LblStatus.Text = "Processing .. " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()
                Next

            ElseIf cb_type.EditValue = "Expense Chart" Then
                Dim dt_bs As New DataTable
                Dim dt_ac As New DataTable
                Dim dt_setting As New DataTable

                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)
                Dim _last_date As Date
                _last_date = DateAdd(DateInterval.Month, te_month.EditValue, CDate(le_glperiode.GetColumnValue("gcal_start_date")))
                Dim _count_day As Integer
                _count_day = DateDiff(DateInterval.Day, CDate(le_glperiode.GetColumnValue("gcal_start_date")), _last_date)
                Dim _date_current As Date


                sSQL = "SELECT  " _
                   & "  a.dash_type_id, " _
                   & "  a.dash_type_number, " _
                   & "  a.dash_type_filter, " _
                   & "  b.dashd_oid, " _
                   & "  b.dashd_dash_type_id, " _
                   & "  b.dashd_ac_id, " _
                   & "  public.ac_mstr.ac_code, " _
                   & "  public.ac_mstr.ac_code_hirarki, " _
                   & "  public.ac_mstr.ac_name,0.0 as value " _
                   & "FROM " _
                   & "  public.dashboard_setting a " _
                   & "  LEFT OUTER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id) " _
                   & "  LEFT OUTER JOIN public.ac_mstr ON (b.dashd_ac_id = public.ac_mstr.ac_id) " _
                   & "WHERE " _
                   & "  a.dash_type_filter = 'EXPENSE_CHART' " _
                   & "ORDER BY " _
                   & "  a.dash_type_filter, " _
                   & "  a.dash_type_number"

                dt_setting = GetTableData(sSQL)

                For Each dr_gl_cal As DataRow In dt_gl_cal.Rows

                    For Each dr_setting As DataRow In dt_setting.Rows


                        sSQL = "SELECT  " _
                          & " '" & dr_setting("dash_type_id") & "' as type, sum( f_get_balance_sheet_pl(z.ac_id,1,1,0,cast('" & dr_gl_cal("gcal_oid") & "' as uuid),'" & dr_gl_cal("gcal_closing") & "')) AS glt_value " _
                          & "FROM " _
                          & "  public.ac_mstr z " _
                          & "WHERE " _
                          & "  z.ac_id in (SELECT  x.ac_id FROM   public.dashboard_setting a   " _
                          & "INNER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id)  " _
                          & " LEFT OUTER JOIN public.ac_mstr c ON (b.dashd_ac_id = c.ac_id)   " _
                          & "LEFT OUTER JOIN public.ac_mstr x ON (substring(x.ac_code_hirarki, 1, length(c.ac_code_hirarki)) = c.ac_code_hirarki)   " _
                          & "WHERE   a.dash_type_id ='" & dr_setting("dash_type_id") & "' and x.ac_is_sumlevel='N' )"


                        dt_ac = GetTableData(sSQL)



                        sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                                  & " and reportd_type=" & SetSetring(dr_setting("dash_type_id")) & "  and reportd_group='EXPENSE_CHART' and " _
                                  & " reportd_periode= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM"))


                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next

                        For Each dr_ac As DataRow In dt_ac.Rows
                            If SetNumber(dr_ac("glt_value")) <> 0 Then


                                sSQL = "INSERT INTO  " _
                                    & "  public.reportd_detail " _
                                    & "( " _
                                    & "  reportd_oid, " _
                                    & "  reportd_report_oid, " _
                                    & "  reportd_type, " _
                                    & "  reportd_periode, " _
                                    & "  reportd_ac_id, " _
                                    & "  reportd_ac_code, " _
                                    & "  reportd_ac_hierarchy, " _
                                    & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                                    & "  reportd_value " _
                                    & ") " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(_report_oid) & ",  " _
                                    & SetSetring(dr_setting("dash_type_id")) & ",  " _
                                    & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                                    & SetSetring("") & ",  " _
                                    & SetSetring("") & ",  " _
                                    & SetSetring("") & ",  " _
                                    & SetSetring("") & ",  " _
                                    & "'EXPENSE_CHART', " _
                                    & SetDateNTime(CekTanggal) & ",  " _
                                    & SetDec(dr_ac("glt_value")) & "  " _
                                    & ")"

                                For Each dr_db As DataRow In dt_db.Rows
                                    DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                                Next


                                
                            End If
                        Next
                    Next


                    _processed = _processed + 1.0
                    _percent = _processed / _count * 100
                    LblStatus.Text = "Processing .. " & cb_type.EditValue & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()

                Next
                For n As Integer = 0 To _count_day - 1
                    _date_current = DateAdd(DateInterval.Day, n, CDate(le_glperiode.GetColumnValue("gcal_start_date")))

                    For Each dr_setting As DataRow In dt_setting.Rows

                        sSQL = "SELECT  " _
                        & " '" & dr_setting("dash_type_id") & "' as type, sum(((public.glt_det.glt_debit * public.glt_det.glt_exc_rate) - (public.glt_det.glt_credit * public.glt_det.glt_exc_rate)) ) AS glt_value " _
                        & "FROM " _
                        & "  public.glt_det " _
                        & "WHERE " _
                        & "  public.glt_det.glt_date = " & SetDateNTime00(_date_current) & " AND  " _
                        & "  public.glt_det.glt_ac_id in (SELECT  x.ac_id FROM   public.dashboard_setting a   " _
                        & "INNER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id)  " _
                        & " LEFT OUTER JOIN public.ac_mstr c ON (b.dashd_ac_id = c.ac_id)   " _
                        & "LEFT OUTER JOIN public.ac_mstr x ON (substring(x.ac_code_hirarki, 1, length(c.ac_code_hirarki)) = c.ac_code_hirarki)   " _
                        & "WHERE   a.dash_type_id ='" & dr_setting("dash_type_id") & "' and x.ac_is_sumlevel='N' )"

                        dt_ac = GetTableData(sSQL)



                        sSQL = "delete from reportd_detail_sub where reportd_report_oid=" & SetSetring(_report_oid) _
                                   & " and reportd_type=" & SetSetring(dr_setting("dash_type_id")) & " and reportd_group='" & dr_setting("dash_type_filter") & "' and " _
                                   & " reportd_periode= " & SetSetring(Format(_date_current, "yyyyMMdd"))


                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next

                        For Each dr_ac As DataRow In dt_ac.Rows

                            If SetNumber(dr_ac("glt_value")) <> 0 Then




                                sSQL = "INSERT INTO  " _
                                    & "  public.reportd_detail_sub " _
                                    & "( " _
                                    & "  reportd_oid, " _
                                    & "  reportd_report_oid, " _
                                    & "  reportd_type, " _
                                    & "  reportd_periode, " _
                                    & "  reportd_ac_id, " _
                                    & "  reportd_ac_code, " _
                                    & "  reportd_ac_hierarchy, " _
                                    & "  reportd_ac_name,reportd_date,reportd_group, reportd_date_generate," _
                                    & "  reportd_value " _
                                    & ") " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(_report_oid) & ",  " _
                                    & SetSetring(dr_ac("type")) & ",  " _
                                    & SetSetring(Format(_date_current, "yyyyMMdd")) & ",  " _
                                    & SetSetring("") & ",  " _
                                    & SetSetring("") & ",  " _
                                    & SetSetring("") & ",  " _
                                    & SetSetring("") & ",  " _
                                    & SetDateNTime00(_date_current) & ", 'EXPENSE_CHART', " _
                                    & SetDateNTime(CekTanggal) & ",  " _
                                    & SetDec(dr_ac("glt_value")) & "  " _
                                    & ")"

                                For Each dr_db As DataRow In dt_db.Rows
                                    DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                                Next
                            End If

                        Next
                    Next

                    _percent = (n + 1) / _count_day * 100
                    LblStatus.Text = "Processing .. " & cb_type.EditValue & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()

                Next


            ElseIf cb_type.EditValue = "Penjualan Terhutang Tagihan Belum Dibayar" Then
                Dim dt_bs As New DataTable
                Dim dt_ac As New DataTable


                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)
                Dim _last_date As Date
                _last_date = DateAdd(DateInterval.Month, te_month.EditValue, CDate(le_glperiode.GetColumnValue("gcal_start_date")))
                Dim _count_day As Integer
                _count_day = DateDiff(DateInterval.Day, CDate(le_glperiode.GetColumnValue("gcal_start_date")), _last_date)
                Dim _date_current As Date


                For n As Integer = 0 To _count_day - 1


                    _date_current = DateAdd(DateInterval.Day, n, CDate(le_glperiode.GetColumnValue("gcal_start_date")))


                    sSQL = "SELECT  " _
                    & "'SALES_DUE-TERHUTANG' as type, sum((a.ar_amount - coalesce(a.ar_pay_amount, 0.0)) * a.ar_exc_rate) AS glt_value " _
                    & "FROM " _
                    & "  public.ar_mstr a " _
                    & "WHERE " _
                    & "  a.ar_status IS NULL and a.ar_eff_date= " & SetDateNTime00(_date_current) & " " _
                    & " union all SELECT  " _
                    & "'SALES_DUE-BELUM DIBAYAR' as type, sum((a.ar_amount - coalesce(a.ar_pay_amount, 0.0)) * a.ar_exc_rate) AS glt_value " _
                    & "FROM " _
                    & "  public.ar_mstr a " _
                    & "WHERE " _
                    & "  a.ar_status IS NULL  and a.ar_due_date= " & SetDateNTime00(_date_current) & " "

                    dt_ac = GetTableData(sSQL)

                    sSQL = "delete from reportd_detail_sub where reportd_report_oid=" & SetSetring(_report_oid) _
                            & " and  reportd_group='SALES_DUE' and " _
                            & " reportd_periode= " & SetSetring(Format(_date_current, "yyyyMMdd"))


                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                    Next

                    For Each dr_ac As DataRow In dt_ac.Rows

                        If SetNumber(dr_ac("glt_value")) <> 0 Then


                            sSQL = "INSERT INTO  " _
                                & "  public.reportd_detail_sub " _
                                & "( " _
                                & "  reportd_oid, " _
                                & "  reportd_report_oid, " _
                                & "  reportd_type, " _
                                & "  reportd_periode, " _
                                & "  reportd_ac_id, " _
                                & "  reportd_ac_code, " _
                                & "  reportd_ac_hierarchy, " _
                                & "  reportd_ac_name,reportd_date,reportd_group, reportd_date_generate," _
                                & "  reportd_value " _
                                & ") " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(_report_oid) & ",  " _
                                & SetSetring(dr_ac("type")) & ",  " _
                                & SetSetring(Format(_date_current, "yyyyMMdd")) & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetDateNTime00(_date_current) & ", 'SALES_DUE', " _
                                & SetDateNTime(CekTanggal) & ",  " _
                                & SetDec(dr_ac("glt_value")) & "  " _
                                & ")"

                            For Each dr_db As DataRow In dt_db.Rows
                                DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                            Next
                        End If

                    Next



                    _percent = (n + 1) / _count_day * 100
                    LblStatus.Text = "Processing .. " & cb_type.EditValue & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()

                Next




                '======

            ElseIf cb_type.EditValue = "Expense Overview" Then

                sSQL = "SELECT  " _
                   & "  a.gcal_oid, " _
                   & "  a.gcal_dom_id, " _
                   & "  a.gcal_add_by, " _
                   & "  a.gcal_add_date, " _
                   & "  a.gcal_upd_by, " _
                   & "  a.gcal_upd_date, " _
                   & "  a.gcal_year, " _
                   & "  a.gcal_periode, " _
                   & "  a.gcal_start_date, " _
                   & "  a.gcal_end_date, " _
                   & "  a.gcal_dt, " _
                   & "  a.gcal_pra_closing, " _
                   & "  a.gcal_closing " _
                   & "FROM " _
                   & "  public.gcal_mstr a " _
                   & "where  " _
                   & "gcal_start_date = (select gcal_start_date from gcal_mstr where gcal_oid='" & le_glperiode.EditValue & "') " _
                   & "order by gcal_start_date "


                'Dim dt_gl_cal As New DataTable
                dt_gl_cal = GetTableData(sSQL)

                sSQL = "SELECT  " _
                     & "  a.dash_type_id, " _
                     & "  a.dash_type_number, " _
                     & "  a.dash_type_filter, " _
                     & "  b.dashd_oid, " _
                     & "  b.dashd_dash_type_id, " _
                     & "  b.dashd_ac_id, " _
                     & "  public.ac_mstr.ac_code, " _
                     & "  public.ac_mstr.ac_code_hirarki, " _
                     & "  public.ac_mstr.ac_name,0.0 as value " _
                     & "FROM " _
                     & "  public.dashboard_setting a " _
                     & "  LEFT OUTER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id) " _
                     & "  LEFT OUTER JOIN public.ac_mstr ON (b.dashd_ac_id = public.ac_mstr.ac_id) " _
                     & "WHERE " _
                     & "  a.dash_type_filter = 'EXPENSE' " _
                     & "ORDER BY " _
                     & "  a.dash_type_filter, " _
                     & "  a.dash_type_number"

                Dim dt_setting As New DataTable
                dt_setting = GetTableData(sSQL)


                Dim dt_bs As New DataTable

                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)

                For Each dr_gl_cal As DataRow In dt_gl_cal.Rows

                    sSQL = "select dash_type_id,sum(v_nilai) as value from ( " _
                        & "SELECT  " _
                        & "  a.dash_type_id, " _
                        & "  a.dash_type_number, " _
                        & "  a.dash_type_filter, " _
                        & "  b.dashd_oid, " _
                        & "  b.dashd_dash_type_id, " _
                        & "  b.dashd_ac_id, " _
                        & "  c.ac_code, " _
                        & "  c.ac_code_hirarki, " _
                        & "  c.ac_name,(select  sum(v_nilai) as jml from   " _
                        & "  ( SELECT    x.ac_id,   x.ac_code_hirarki,   x.ac_code,   x.ac_name,     " _
                        & "  x.ac_type,f_get_balance_sheet_pl(x.ac_id,1,1,0, " _
                        & "  cast('" & dr_gl_cal("gcal_oid") & "' as uuid),'" & dr_gl_cal("gcal_closing") & "') as v_nilai   " _
                        & "  FROM   public.ac_mstr x WHERE    " _
                        & "  substring(x.ac_code_hirarki, 1, length(c.ac_code_hirarki)) = c.ac_code_hirarki AND      " _
                        & "  ac_is_sumlevel = 'N') as temp) * 1.0 as v_nilai  " _
                        & "FROM " _
                        & "  public.dashboard_setting a " _
                        & "  INNER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id) " _
                        & "  LEFT OUTER JOIN public.ac_mstr c ON (b.dashd_ac_id = c.ac_id) " _
                        & "WHERE " _
                        & "  a.dash_type_filter = 'EXPENSE' " _
                        & "ORDER BY " _
                        & "  a.dash_type_filter, " _
                        & "  a.dash_type_number) as tp group by dash_type_id"


                    dt_bs = GetTableData(sSQL)
                    Dim _penjualan, _hpp, _biaya_op, _laba_kotor, _laba_operasi, _laba_sebelum_pajak, _pendapatan_biaya_lain As Double
                    _penjualan = 0.0
                    _hpp = 0.0
                    _biaya_op = 0.0

                    _pendapatan_biaya_lain = 0.0

                    For Each dr_bs As DataRow In dt_bs.Rows
                        For Each dr_setting As DataRow In dt_setting.Rows
                            If dr_bs("dash_type_id") = dr_setting("dash_type_id") Then
                                dr_setting("value") = dr_setting("value") + dr_bs("value")
                            End If
                        Next
                    Next

                    dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows

                        sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                            & " and reportd_type=" & SetSetring(dr_setting("dash_type_id")) & " "


                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next

                        sSQL = "INSERT INTO  " _
                            & "  public.reportd_detail " _
                            & "( " _
                            & "  reportd_oid, " _
                            & "  reportd_report_oid, " _
                            & "  reportd_type, " _
                            & "  reportd_periode, " _
                            & "  reportd_ac_id, " _
                            & "  reportd_ac_code, " _
                            & "  reportd_ac_hierarchy, " _
                            & "  reportd_ac_name,reportd_date_generate,reportd_group, " _
                            & "  reportd_value " _
                            & ") " _
                            & "VALUES ( " _
                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                            & SetSetring(_report_oid) & ",  " _
                            & SetSetring(dr_setting("dash_type_id")) & ",  " _
                            & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetDateNTime(CekTanggal) & ", 'EXPENSE', " _
                            & SetDec(dr_setting("value")) & "  " _
                            & ")"

                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next
                    Next


                    For Each dr_setting As DataRow In dt_setting.Rows
                        dr_setting("value") = 0.0
                    Next
                    dt_setting.AcceptChanges()

                    '_processed = _processed + 1.0
                    '_percent = _processed / _count * 100
                    'LblStatus.Text = "Processing .. " & Math.Round(_percent, 2) & " %"
                    'System.Windows.Forms.Application.DoEvents()
                Next


                '====


            ElseIf cb_type.EditValue = "Cash Detail" Then

                Dim dt_bs As New DataTable
                Dim dt_ac As New DataTable


                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)
                Dim _last_date As Date
                _last_date = DateAdd(DateInterval.Month, te_month.EditValue, CDate(le_glperiode.GetColumnValue("gcal_start_date")))
                Dim _count_day As Integer
                _count_day = DateDiff(DateInterval.Day, CDate(le_glperiode.GetColumnValue("gcal_start_date")), _last_date)
                Dim _date_current As Date
                Dim _selisih As Double = 0.0


                For n As Integer = 0 To _count_day - 1

                    _date_current = DateAdd(DateInterval.Day, n, CDate(le_glperiode.GetColumnValue("gcal_start_date")))

                    sSQL = "SELECT  " _
                        & " 'CASH_IN_OUT-CASH IN' as type, sum((public.glt_det.glt_debit * public.glt_det.glt_exc_rate)) AS glt_value " _
                        & "FROM " _
                        & "  public.glt_det " _
                        & "WHERE " _
                        & " public.glt_det.glt_date = " & SetDateNTime00(_date_current) & " AND  " _
                        & "  public.glt_det.glt_ac_id in (SELECT  x.ac_id FROM   public.dashboard_setting a   " _
                        & "INNER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id)  " _
                        & " LEFT OUTER JOIN public.ac_mstr c ON (b.dashd_ac_id = c.ac_id)   " _
                        & "LEFT OUTER JOIN public.ac_mstr x ON (substring(x.ac_code_hirarki, 1, length(c.ac_code_hirarki)) = c.ac_code_hirarki)   " _
                        & "WHERE   a.dash_type_id in ('ACCOUNT-KAS','ACCOUNT-BANK') and x.ac_is_sumlevel='N' ) union all " _
                        & "SELECT  " _
                        & " 'CASH_IN_OUT-CASH OUT' as type, sum((public.glt_det.glt_credit * public.glt_det.glt_exc_rate)) AS glt_value " _
                        & "FROM " _
                        & "  public.glt_det " _
                        & "WHERE " _
                        & "  public.glt_det.glt_date= " & SetDateNTime00(_date_current) & " AND  " _
                        & "  public.glt_det.glt_ac_id in (SELECT  x.ac_id FROM   public.dashboard_setting a   " _
                        & "INNER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id)  " _
                        & " LEFT OUTER JOIN public.ac_mstr c ON (b.dashd_ac_id = c.ac_id)   " _
                        & "LEFT OUTER JOIN public.ac_mstr x ON (substring(x.ac_code_hirarki, 1, length(c.ac_code_hirarki)) = c.ac_code_hirarki)   " _
                        & "WHERE   a.dash_type_id in ('ACCOUNT-KAS','ACCOUNT-BANK') and x.ac_is_sumlevel='N' )"


                    dt_ac = GetTableData(sSQL)

                    sSQL = "delete from reportd_detail_sub where reportd_report_oid=" & SetSetring(_report_oid) _
                         & "  and reportd_group='CASH_IN_OUT' and " _
                         & " reportd_periode= " & SetSetring(Format(_date_current, "yyyyMMdd"))


                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                    Next

                    _selisih = 0.0

                    For Each dr_ac As DataRow In dt_ac.Rows
                        If SetNumber(dr_ac("glt_value")) <> 0 Then


                            sSQL = "INSERT INTO  " _
                             & "  public.reportd_detail_sub " _
                             & "( " _
                             & "  reportd_oid, " _
                             & "  reportd_report_oid, " _
                             & "  reportd_type, " _
                             & "  reportd_periode, " _
                             & "  reportd_ac_id, " _
                             & "  reportd_ac_code, " _
                             & "  reportd_ac_hierarchy, " _
                             & "  reportd_ac_name,reportd_date,reportd_group, reportd_date_generate," _
                             & "  reportd_value " _
                             & ") " _
                             & "VALUES ( " _
                             & SetSetring(Guid.NewGuid.ToString) & ",  " _
                             & SetSetring(_report_oid) & ",  " _
                             & SetSetring(dr_ac("type")) & ",  " _
                             & SetSetring(Format(_date_current, "yyyyMMdd")) & ",  " _
                             & SetSetring("") & ",  " _
                             & SetSetring("") & ",  " _
                             & SetSetring("") & ",  " _
                             & SetSetring("") & ",  " _
                             & SetDateNTime00(_date_current) & ", 'CASH_IN_OUT', " _
                             & SetDateNTime(CekTanggal) & ",  " _
                             & SetDec(dr_ac("glt_value")) & "  " _
                             & ")"

                            If dr_ac("type") = "CASH_IN_OUT-CASH IN" Then
                                _selisih = _selisih + SetNumber(dr_ac("glt_value"))
                            Else
                                _selisih = _selisih - SetNumber(dr_ac("glt_value"))
                            End If

                            For Each dr_db As DataRow In dt_db.Rows
                                DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                            Next
                        End If

                    Next

                    sSQL = "INSERT INTO  " _
                            & "  public.reportd_detail_sub " _
                            & "( " _
                            & "  reportd_oid, " _
                            & "  reportd_report_oid, " _
                            & "  reportd_type, " _
                            & "  reportd_periode, " _
                            & "  reportd_ac_id, " _
                            & "  reportd_ac_code, " _
                            & "  reportd_ac_hierarchy, " _
                            & "  reportd_ac_name,reportd_date,reportd_group, reportd_date_generate," _
                            & "  reportd_value " _
                            & ") " _
                            & "VALUES ( " _
                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                            & SetSetring(_report_oid) & ",  " _
                            & SetSetring("CASH_IN_OUT-SELISIH") & ",  " _
                            & SetSetring(Format(_date_current, "yyyyMMdd")) & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetDateNTime00(_date_current) & ", 'CASH_IN_OUT', " _
                            & SetDateNTime(CekTanggal) & ",  " _
                            & SetDec(_selisih) & "  " _
                            & ")"


                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                    Next


                    _percent = (n + 1) / _count_day * 100
                    LblStatus.Text = "Processing .. " & cb_type.EditValue & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()

                Next


            ElseIf cb_type.EditValue = "Account" Then
                sSQL = "SELECT  " _
                 & "  a.dash_type_id, " _
                 & "  a.dash_type_number, " _
                 & "  a.dash_type_filter, " _
                 & "  b.dashd_oid, " _
                 & "  b.dashd_dash_type_id, " _
                 & "  b.dashd_ac_id, " _
                 & "  public.ac_mstr.ac_code, " _
                 & "  public.ac_mstr.ac_code_hirarki, " _
                 & "  public.ac_mstr.ac_name,0.0 as value " _
                 & "FROM " _
                 & "  public.dashboard_setting a " _
                 & "  LEFT OUTER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id) " _
                 & "  LEFT OUTER JOIN public.ac_mstr ON (b.dashd_ac_id = public.ac_mstr.ac_id) " _
                 & "WHERE " _
                 & "  a.dash_type_filter = 'ACCOUNT' " _
                 & "ORDER BY " _
                 & "  a.dash_type_filter, " _
                 & "  a.dash_type_number"

                Dim dt_setting As New DataTable
                dt_setting = GetTableData(sSQL)


                Dim dt_bs As New DataTable

                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)

                For Each dr_gl_cal As DataRow In dt_gl_cal.Rows

                    sSQL = "select dash_type_id,sum(v_nilai) as value from ( " _
                        & "SELECT  " _
                        & "  a.dash_type_id, " _
                        & "  a.dash_type_number, " _
                        & "  a.dash_type_filter, " _
                        & "  b.dashd_oid, " _
                        & "  b.dashd_dash_type_id, " _
                        & "  b.dashd_ac_id, " _
                        & "  c.ac_code, " _
                        & "  c.ac_code_hirarki, " _
                        & "  c.ac_name,(select  sum(v_nilai) as jml from   " _
                        & "  ( SELECT    x.ac_id,   x.ac_code_hirarki,   x.ac_code,   x.ac_name,     " _
                        & "  x.ac_type,f_get_balance_sheet(x.ac_id,1,1,0, " _
                        & "  cast('" & dr_gl_cal("gcal_oid") & "' as uuid),'" & dr_gl_cal("gcal_closing") & "') as v_nilai   " _
                        & "  FROM   public.ac_mstr x WHERE    " _
                        & "  substring(x.ac_code_hirarki, 1, length(c.ac_code_hirarki)) = c.ac_code_hirarki AND      " _
                        & "  ac_is_sumlevel = 'N') as temp) * 1.0 as v_nilai  " _
                        & "FROM " _
                        & "  public.dashboard_setting a " _
                        & "  INNER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id) " _
                        & "  LEFT OUTER JOIN public.ac_mstr c ON (b.dashd_ac_id = c.ac_id) " _
                        & "WHERE " _
                        & "  a.dash_type_filter = 'ACCOUNT' " _
                        & "ORDER BY " _
                        & "  a.dash_type_filter, " _
                        & "  a.dash_type_number) as tp group by dash_type_id"


                    dt_bs = GetTableData(sSQL)
                    Dim _penjualan, _hpp, _biaya_op, _laba_kotor, _laba_operasi, _laba_sebelum_pajak, _pendapatan_biaya_lain As Double
                    _penjualan = 0.0
                    _hpp = 0.0
                    _biaya_op = 0.0
                    '_laba_kotor = 0.0
                    '_laba_operasi = 0.0
                    '_laba_sebelum_pajak = 0.0
                    _pendapatan_biaya_lain = 0.0

                    For Each dr_bs As DataRow In dt_bs.Rows
                        For Each dr_setting As DataRow In dt_setting.Rows
                            If dr_bs("dash_type_id") = dr_setting("dash_type_id") Then
                                dr_setting("value") = dr_bs("value")
                            End If
                        Next
                    Next

                    '_laba_kotor = _penjualan + _hpp
                    '_laba_operasi = _laba_kotor + _biaya_op
                    '_laba_sebelum_pajak = _laba_operasi + _pendapatan_biaya_lain

                    dt_setting.AcceptChanges()

                    'For Each dr_setting As DataRow In dt_setting.Rows
                    '    If dr_setting("dash_type_id") = "ACCOUNT-BANK" Then
                    '        dr_setting("value") = _laba_kotor
                    '    ElseIf dr_setting("dash_type_id") = "PROFIT_LOSS-LABA OPERASI" Then
                    '        dr_setting("value") = _laba_operasi

                    '    ElseIf dr_setting("dash_type_id") = "PROFIT_LOSS-LABA SEBELUM PAJAK" Then
                    '        dr_setting("value") = _laba_sebelum_pajak
                    '    End If
                    'Next

                    'dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows

                        sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                            & " and reportd_type=" & SetSetring(dr_setting("dash_type_id")) & " and " _
                            & " reportd_periode= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM"))


                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next

                        sSQL = "INSERT INTO  " _
                            & "  public.reportd_detail " _
                            & "( " _
                            & "  reportd_oid, " _
                            & "  reportd_report_oid, " _
                            & "  reportd_type, " _
                            & "  reportd_periode, " _
                            & "  reportd_ac_id, " _
                            & "  reportd_ac_code, " _
                            & "  reportd_ac_hierarchy, " _
                            & "  reportd_ac_name,reportd_date_generate,reportd_group, " _
                            & "  reportd_value " _
                            & ") " _
                            & "VALUES ( " _
                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                            & SetSetring(_report_oid) & ",  " _
                            & SetSetring(dr_setting("dash_type_id")) & ",  " _
                            & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetDateNTime(CekTanggal) & ", 'ACCOUNT', " _
                            & SetDec(dr_setting("value")) & "  " _
                            & ")"

                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next
                    Next


                    For Each dr_setting As DataRow In dt_setting.Rows
                        dr_setting("value") = 0.0
                    Next
                    dt_setting.AcceptChanges()

                    _processed = _processed + 1.0
                    _percent = _processed / _count * 100
                    LblStatus.Text = "Processing .. " & cb_type.EditValue & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()
                Next
            ElseIf cb_type.EditValue = "Balance Sheet New" Then
                'Balance Sheet New
                'Profit Loss New
                'Cash Flow New

                Dim _balance_sheet_new_current_assets As Double = 0
                Dim _balance_sheet_new_investment As Double = 0
                Dim _balance_sheet_new_fixed_assets As Double = 0
                Dim _balance_sheet_new_total_assets As Double = 0
                Dim _balance_sheet_new_current_liabilities As Double = 0
                Dim _balance_sheet_new_long_term_liabilities As Double = 0
                Dim _balance_sheet_new_net_worth As Double = 0
                Dim _balance_sheet_new_total_liabilities_equities As Double = 0
                Dim _balance_sheet_new_cash_on_hand As Double = 0
                Dim _balance_sheet_new_inventories As Double = 0



                sSQL = "SELECT  " _
                   & "  a.dash_type_id, " _
                   & "  a.dash_type_number, " _
                   & "  a.dash_type_filter, " _
                   & "  b.dashd_oid, " _
                   & "  b.dashd_dash_type_id, " _
                   & "  b.dashd_ac_id, " _
                   & "  public.ac_mstr.ac_code, " _
                   & "  public.ac_mstr.ac_code_hirarki, " _
                   & "  public.ac_mstr.ac_name,0.0 as value " _
                   & "FROM " _
                   & "  public.dashboard_setting a " _
                   & "  LEFT OUTER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id) " _
                   & "  LEFT OUTER JOIN public.ac_mstr ON (b.dashd_ac_id = public.ac_mstr.ac_id) " _
                   & "WHERE " _
                   & "  a.dash_type_filter = 'BALANCE_SHEET_NEW' " _
                   & "ORDER BY " _
                   & "  a.dash_type_filter, " _
                   & "  a.dash_type_number"

                Dim dt_setting As New DataTable
                dt_setting = GetTableData(sSQL)

                'Exit Try

                Dim dt_bs As New DataTable
                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)

                For Each dr_gl_cal As DataRow In dt_gl_cal.Rows

                    sSQL = "SELECT a.bs_number,  a.bs_caption,  a.bs_group,   " _
                      & "a.bs_remarks,  b.bsd_number,  b.bsd_caption,   " _
                      & "b.bsd_remarks, c.bsdi_number,  c.bsdi_caption,   " _
                      & "c.bsdi_oid,    " _
                      & "(select sum(jml) from (SELECT  (select sum(v_nilai) as nilai from (  " _
                      & "SELECT y.ac_id,   y.ac_code_hirarki, y.ac_code,   y.ac_name,    " _
                      & "y.ac_type,f_get_balance_sheet(y.ac_id,1,1,0,cast('" & dr_gl_cal("gcal_oid") & "' as uuid),'" & dr_gl_cal("gcal_closing") & "') as v_nilai  " _
                      & "FROM   public.ac_mstr y WHERE   substring(y.ac_code_hirarki, 1, length(x.bsda_ac_hirarki)) = x.bsda_ac_hirarki AND     " _
                      & "y.ac_is_sumlevel = 'N') as temp) as jml  FROM   public.bsda_account x WHERE   x.bsda_bsdi_oid = c.bsdi_oid) as temp2)   as value   " _
                      & "FROM   public.bs_mstr a    " _
                      & "INNER JOIN public.bsd_det b ON (a.bs_number = b.bsd_bs_number)    " _
                      & "INNER JOIN public.bsdi_det_item c ON (b.bsd_oid = c.bsdi_bsd_oid)  " _
                      & " order by bs_number, bsd_number, bsdi_number"

                    dt_bs = GetTableData(sSQL)
                    ' Exit Try

                    For Each dr_bs As DataRow In dt_bs.Rows
                        If dr_bs("bsd_caption") = "Harta Lancar" Then
                            _balance_sheet_new_current_assets = _balance_sheet_new_current_assets + SetNumber(dr_bs("value"))
                        ElseIf dr_bs("bsd_caption") = "Investasi Jangka Panjang" Then
                            _balance_sheet_new_investment = _balance_sheet_new_investment + SetNumber(dr_bs("value"))
                        ElseIf dr_bs("bsd_caption") = "Harta Tetap" Then
                            _balance_sheet_new_fixed_assets = _balance_sheet_new_fixed_assets + SetNumber(dr_bs("value"))
                        ElseIf dr_bs("bsd_caption") = "Hutang Lancar" Then
                            _balance_sheet_new_current_liabilities = _balance_sheet_new_current_liabilities + SetNumber(dr_bs("value"))
                        ElseIf dr_bs("bsd_caption") = "Hutang Bank" Then
                            _balance_sheet_new_long_term_liabilities = _balance_sheet_new_long_term_liabilities + SetNumber(dr_bs("value"))
                        End If
                        If dr_bs("bsdi_caption") = "Kas" Then
                            _balance_sheet_new_cash_on_hand = _balance_sheet_new_cash_on_hand + SetNumber(dr_bs("value"))
                        ElseIf dr_bs("bsdi_caption") = "Persediaan Bahan Baku" Or dr_bs("bsdi_caption") = "Persediaan Barang Dalam Proses (WIP)" _
                        Or dr_bs("bsdi_caption") = "Persediaan Barang Jadi" Or dr_bs("bsdi_caption") = "Persediaan Konsinyasi" _
                         Or dr_bs("bsdi_caption") = "Persedian Suku Cadang" Or dr_bs("bsdi_caption") = "Persediaan Lain-lain" Then
                            _balance_sheet_new_inventories = _balance_sheet_new_inventories + SetNumber(dr_bs("value"))
                        End If


                       
                    Next

                    _balance_sheet_new_total_assets = _balance_sheet_new_current_assets + _balance_sheet_new_investment + _balance_sheet_new_fixed_assets
                    _balance_sheet_new_net_worth = _balance_sheet_new_total_assets - _balance_sheet_new_current_liabilities - _balance_sheet_new_long_term_liabilities
                    _balance_sheet_new_total_liabilities_equities = _balance_sheet_new_current_liabilities + _balance_sheet_new_long_term_liabilities + _balance_sheet_new_net_worth


                    For Each dr_setting As DataRow In dt_setting.Rows
                        If dr_setting("dash_type_id") = "BALANCE_SHEET_NEW-CURRENT ASSETS" Then
                            dr_setting("value") = _balance_sheet_new_current_assets
                        ElseIf dr_setting("dash_type_id") = "BALANCE_SHEET_NEW-INVESTMENT" Then
                            dr_setting("value") = _balance_sheet_new_investment
                        ElseIf dr_setting("dash_type_id") = "BALANCE_SHEET_NEW-FIXED ASSETS" Then
                            dr_setting("value") = _balance_sheet_new_fixed_assets
                        ElseIf dr_setting("dash_type_id") = "BALANCE_SHEET_NEW-TOTAL ASSETS" Then
                            dr_setting("value") = _balance_sheet_new_total_assets
                        ElseIf dr_setting("dash_type_id") = "BALANCE_SHEET_NEW-CURRENT LIABILITIES" Then
                            dr_setting("value") = _balance_sheet_new_current_liabilities
                        ElseIf dr_setting("dash_type_id") = "BALANCE_SHEET_NEW-LONG-TERM LIABILITIES" Then
                            dr_setting("value") = _balance_sheet_new_long_term_liabilities
                        ElseIf dr_setting("dash_type_id") = "BALANCE_SHEET_NEW-NET WORTH" Then
                            dr_setting("value") = _balance_sheet_new_net_worth
                        ElseIf dr_setting("dash_type_id") = "BALANCE_SHEET_NEW-TOTAL LIABILITIES & EQUITIES" Then
                            dr_setting("value") = _balance_sheet_new_total_liabilities_equities
                        ElseIf dr_setting("dash_type_id") = "BALANCE_SHEET_NEW-CASH ON HAND" Then
                            dr_setting("value") = _balance_sheet_new_cash_on_hand
                        ElseIf dr_setting("dash_type_id") = "BALANCE_SHEET_NEW-INVENTORIES" Then
                            dr_setting("value") = _balance_sheet_new_inventories

                        End If
                    Next

                    dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows

                        sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                            & " and reportd_type=" & SetSetring(dr_setting("dash_type_id")) & " and " _
                            & " reportd_periode= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM"))


                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next

                        sSQL = "INSERT INTO  " _
                            & "  public.reportd_detail " _
                            & "( " _
                            & "  reportd_oid, " _
                            & "  reportd_report_oid, " _
                            & "  reportd_type, " _
                            & "  reportd_periode, " _
                            & "  reportd_ac_id, " _
                            & "  reportd_ac_code, " _
                            & "  reportd_ac_hierarchy, " _
                            & "  reportd_ac_name,reportd_group,reportd_date_generate," _
                            & "  reportd_value " _
                            & ") " _
                            & "VALUES ( " _
                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                            & SetSetring(_report_oid) & ",  " _
                            & SetSetring(dr_setting("dash_type_id")) & ",  " _
                            & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",'BALANCE_SHEET_NEW',   " _
                            & SetDateNTime(CekTanggal()) & "," _
                            & SetDec(dr_setting("value")) & "  " _
                            & ")"

                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next
                    Next


                    For Each dr_setting As DataRow In dt_setting.Rows
                        dr_setting("value") = 0.0
                    Next
                    dt_setting.AcceptChanges()

                    _processed = _processed + 1.0
                    _percent = _processed / _count * 100
                    LblStatus.Text = "Processing .. " & cb_type.EditValue & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()
                Next

            ElseIf cb_type.EditValue = "Profit Loss New" Then


                sSQL = "SELECT  " _
                  & "  a.dash_type_id, " _
                  & "  a.dash_type_number, " _
                  & "  a.dash_type_filter, " _
                  & "  b.dashd_oid, " _
                  & "  b.dashd_dash_type_id, " _
                  & "  b.dashd_ac_id, " _
                  & "  public.ac_mstr.ac_code, " _
                  & "  public.ac_mstr.ac_code_hirarki, " _
                  & "  public.ac_mstr.ac_name,0.0 as value " _
                  & "FROM " _
                  & "  public.dashboard_setting a " _
                  & "  LEFT OUTER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id) " _
                  & "  LEFT OUTER JOIN public.ac_mstr ON (b.dashd_ac_id = public.ac_mstr.ac_id) " _
                  & "WHERE " _
                  & "  a.dash_type_filter = 'PROFIT_LOSS_NEW' " _
                  & "ORDER BY " _
                  & "  a.dash_type_filter, " _
                  & "  a.dash_type_number"

                Dim dt_setting As New DataTable
                dt_setting = GetTableData(sSQL)


                Dim dt_bs As New DataTable

                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)

                For Each dr_gl_cal As DataRow In dt_gl_cal.Rows


                    sSQL = "SELECT    a.pl_oid,   a.pl_footer,   a.pl_sign,   a.pl_number,    " _
                      & "b.pls_oid,   b.pls_item,   b.pls_number,   c.pla_ac_id,   d.ac_code,    " _
                      & "d.ac_name,   c.pla_ac_hirarki,(select  sum(v_nilai) as jml from  " _
                      & "( SELECT    x.ac_id,   x.ac_code_hirarki,   x.ac_code,   x.ac_name,    " _
                      & "x.ac_type,f_get_balance_sheet_pl(x.ac_id,1,1,0,cast('" & dr_gl_cal("gcal_oid") & "' as uuid),'" & dr_gl_cal("gcal_closing") & "') as v_nilai  " _
                      & "FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1, length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
                      & "ac_is_sumlevel = 'N') as temp) * pls_value as value FROM   public.pl_setting_mstr a    " _
                      & "INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid)    " _
                      & "INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid)    " _
                      & "INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id) ORDER BY   a.pl_number,   b.pls_number "


                    dt_bs = GetTableData(sSQL)
                    Dim _penjualan, _diskon_and_return, _hpp, _biaya_op, _laba_kotor, _laba_operasi, _laba_sebelum_pajak, _pendapatan_lain, _biaya_lain As Double
                    Dim _gpm, _om, _margin_laba_bersih, _percent_hpp, _percent_adm_umum, _percent_total_biaya As Double
                    _penjualan = 0.0
                    _hpp = 0.0
                    _biaya_op = 0.0
                    _laba_kotor = 0.0
                    _laba_operasi = 0.0
                    _laba_sebelum_pajak = 0.0
                    _pendapatan_lain = 0.0
                    _biaya_lain = 0.0


                    _gpm = 0.0
                    _om = 0.0
                    _margin_laba_bersih = 0.0
                    _percent_hpp = 0.0
                    _percent_adm_umum = 0.0
                    _percent_total_biaya = 0.0



                    For Each dr_bs As DataRow In dt_bs.Rows
                        For Each dr_setting As DataRow In dt_setting.Rows
                           
                            If dr_bs("pls_item").ToString.ToLower = "Penjualan".ToLower And dr_setting("dash_type_id") = "PROFIT_LOSS_NEW-SALES BRUTO" Then
                                dr_setting("value") = dr_setting("value") + SetNumber(dr_bs("value"))
                            End If
                            If dr_bs("pls_item").ToString.ToLower = "Retur Penjualan & Potongan Harga".ToLower And dr_setting("dash_type_id") = "PROFIT_LOSS_NEW-DISCOUNT & RETURN" Then
                                dr_setting("value") = dr_setting("value") + (SetNumber(dr_bs("value")) * -1.0)
                            End If
                            If dr_bs("pls_item").ToString.ToLower = "Potongan Penjualan".ToLower And dr_setting("dash_type_id") = "PROFIT_LOSS_NEW-DISCOUNT & RETURN" Then
                                dr_setting("value") = dr_setting("value") + (SetNumber(dr_bs("value")) * -1.0)
                            End If

                            If dr_bs("pls_item").ToString.ToLower = "Harga pokok penjualan".ToLower And dr_setting("dash_type_id") = "PROFIT_LOSS_NEW-COGS" Then
                                dr_setting("value") = dr_setting("value") + (SetNumber(dr_bs("value")) * -1.0)
                            End If

                            If dr_bs("pls_item").ToString.ToLower = "Biaya Marketing".ToLower And dr_setting("dash_type_id") = "PROFIT_LOSS_NEW-MARKETING EXP" Then
                                dr_setting("value") = dr_setting("value") + (SetNumber(dr_bs("value")) * -1.0)
                            End If

                            If dr_bs("pls_item").ToString.ToLower = "Biaya Operasional".ToLower And dr_setting("dash_type_id") = "PROFIT_LOSS_NEW-OPERATIONAL EXP" Then
                                dr_setting("value") = dr_setting("value") + (SetNumber(dr_bs("value")) * -1.0)
                            End If

                            If dr_bs("pls_item").ToString.ToLower = "Biaya Administrasi dan Umum".ToLower And dr_setting("dash_type_id") = "PROFIT_LOSS_NEW-ADMINISTRATION EXP" Then
                                dr_setting("value") = dr_setting("value") + (SetNumber(dr_bs("value")) * -1.0)
                            End If
                            If dr_bs("pls_item").ToString.ToLower = "Pendapatan Lain-Lain".ToLower And dr_setting("dash_type_id") = "PROFIT_LOSS_NEW-OTHER INCOME (NET)" Then
                                dr_setting("value") = dr_setting("value") + SetNumber(dr_bs("value"))
                            End If
                            If dr_bs("pls_item").ToString.ToLower = "Biaya Lain-lain".ToLower And dr_setting("dash_type_id") = "PROFIT_LOSS_NEW-OTHER INCOME (NET)" Then
                                dr_setting("value") = dr_setting("value") + SetNumber(dr_bs("value"))
                            End If

                        Next
                        _laba_sebelum_pajak = _laba_sebelum_pajak + SetNumber(dr_bs("value"))
                    Next

                    '_laba_kotor = _penjualan + _hpp
                    '_laba_operasi = _laba_kotor + _biaya_op
                    '_laba_sebelum_pajak = _laba_operasi + _pendapatan_lain + _biaya_lain
                    Try
                        '_gpm = _laba_kotor / _penjualan
                    Catch ex As Exception
                    End Try

                    Try
                        '_om = _laba_operasi / _penjualan
                    Catch ex As Exception
                    End Try
                    Try
                        '_margin_laba_bersih = _laba_sebelum_pajak / _penjualan
                    Catch ex As Exception
                    End Try

                    Try
                        '_percent_hpp = (_hpp * -1.0) / _penjualan
                    Catch ex As Exception

                    End Try

                    Try
                        '_percent_adm_umum = (_biaya_op * -1.0) / _penjualan
                    Catch ex As Exception

                    End Try
                    Try
                        '_percent_total_biaya = ((_biaya_op + _biaya_lain + _hpp) * -1.0) / (_penjualan + _pendapatan_lain)
                    Catch ex As Exception

                    End Try


                    dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows
                        If dr_setting("dash_type_id") = "PROFIT_LOSS_NEW-NET INCOME" Then
                            dr_setting("value") = _laba_sebelum_pajak
                        End If
                    Next

                    dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows

                        sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                            & " and reportd_type=" & SetSetring(dr_setting("dash_type_id")) & " and " _
                            & " reportd_periode= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM"))


                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next

                        sSQL = "INSERT INTO  " _
                            & "  public.reportd_detail " _
                            & "( " _
                            & "  reportd_oid, " _
                            & "  reportd_report_oid, " _
                            & "  reportd_type, " _
                            & "  reportd_periode, " _
                            & "  reportd_ac_id, " _
                            & "  reportd_ac_code, " _
                            & "  reportd_ac_hierarchy, " _
                            & "  reportd_ac_name, reportd_group,reportd_date_generate," _
                            & "  reportd_value " _
                            & ") " _
                            & "VALUES ( " _
                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                            & SetSetring(_report_oid) & ",  " _
                            & SetSetring(dr_setting("dash_type_id")) & ",  " _
                            & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ", 'PROFIT_LOSS_NEW',   " _
                            & SetDateNTime(CekTanggal()) & "," _
                            & SetDec(dr_setting("value")) & "  " _
                            & ")"

                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next
                    Next


                    For Each dr_setting As DataRow In dt_setting.Rows
                        dr_setting("value") = 0.0
                    Next
                    dt_setting.AcceptChanges()

                    _processed = _processed + 1.0
                    _percent = _processed / _count * 100
                    LblStatus.Text = "Processing .. " & cb_type.EditValue & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()
                Next

            ElseIf cb_type.EditValue = "Monthly Trend" Then


                sSQL = "SELECT  " _
                  & "  a.dash_type_id, " _
                  & "  a.dash_type_number, " _
                  & "  a.dash_type_filter, " _
                  & "  b.dashd_oid, " _
                  & "  b.dashd_dash_type_id, " _
                  & "  b.dashd_ac_id, " _
                  & "  public.ac_mstr.ac_code, " _
                  & "  public.ac_mstr.ac_code_hirarki, " _
                  & "  public.ac_mstr.ac_name,0.0 as value " _
                  & "FROM " _
                  & "  public.dashboard_setting a " _
                  & "  LEFT OUTER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id) " _
                  & "  LEFT OUTER JOIN public.ac_mstr ON (b.dashd_ac_id = public.ac_mstr.ac_id) " _
                  & "WHERE " _
                  & "  a.dash_type_filter = 'MONTHLY_TREND' " _
                  & "ORDER BY " _
                  & "  a.dash_type_filter, " _
                  & "  a.dash_type_number"

                Dim dt_setting As New DataTable
                dt_setting = GetTableData(sSQL)


                Dim dt_bs As New DataTable
                Dim dt_bs_ok As New DataTable

                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)

                For Each dr_gl_cal As DataRow In dt_gl_cal.Rows


                    sSQL = "SELECT    a.pl_oid,   a.pl_footer,   a.pl_sign,   a.pl_number,    " _
                      & "b.pls_oid,   b.pls_item,   b.pls_number,   c.pla_ac_id,   d.ac_code,    " _
                      & "d.ac_name,   c.pla_ac_hirarki,(select  sum(v_nilai) as jml from  " _
                      & "( SELECT    x.ac_id,   x.ac_code_hirarki,   x.ac_code,   x.ac_name,    " _
                      & "x.ac_type,f_get_balance_sheet_pl(x.ac_id,1,1,0,cast('" & dr_gl_cal("gcal_oid") & "' as uuid),'" & dr_gl_cal("gcal_closing") & "') as v_nilai  " _
                      & "FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1, length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
                      & "ac_is_sumlevel = 'N') as temp) * pls_value as value FROM   public.pl_setting_mstr a    " _
                      & "INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid)    " _
                      & "INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid)    " _
                      & "INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id) ORDER BY   a.pl_number,   b.pls_number "


                    dt_bs = GetTableData(sSQL)


                    sSQL = "SELECT a.bs_number,  a.bs_caption,  a.bs_group,   " _
                     & "a.bs_remarks,  b.bsd_number,  b.bsd_caption,   " _
                     & "b.bsd_remarks, c.bsdi_number,  c.bsdi_caption,   " _
                     & "c.bsdi_oid,    " _
                     & "(select sum(jml) from (SELECT  (select sum(v_nilai) as nilai from (  " _
                     & "SELECT y.ac_id,   y.ac_code_hirarki, y.ac_code,   y.ac_name,    " _
                     & "y.ac_type,f_get_balance_sheet(y.ac_id,1,1,0,cast('" & dr_gl_cal("gcal_oid").ToString & "' as uuid),'" & dr_gl_cal("gcal_closing") & "') as v_nilai  " _
                     & "FROM   public.ac_mstr y WHERE   substring(y.ac_code_hirarki, 1, length(x.bsda_ac_hirarki)) = x.bsda_ac_hirarki AND     " _
                     & "y.ac_is_sumlevel = 'N') as temp) as jml  FROM   public.bsda_account x WHERE   x.bsda_bsdi_oid = c.bsdi_oid) as temp2)   as z_nilai   " _
                     & "FROM   public.bs_mstr a    " _
                     & "INNER JOIN public.bsd_det b ON (a.bs_number = b.bsd_bs_number)    " _
                     & "INNER JOIN public.bsdi_det_item c ON (b.bsd_oid = c.bsdi_bsd_oid)  " _
                     & " order by bs_number,bsd_number,bsdi_number"

                    dt_bs_ok = GetTableData(sSQL)


                    Dim _penjualan, _penjualan_bersih, _hpp, _biaya_op, _laba_kotor, _laba_operasi, _laba_sebelum_pajak, _pendapatan_lain, _biaya_lain As Double
                    Dim _gpm, _om, _margin_laba_bersih, _percent_hpp, _percent_adm_umum, _percent_total_biaya, _
                     _biaya_operasional, _biaya_adum, _biaya_marketing, _net_work_modal, _working_capital, _current_ratio, _quick_ratio, _der_ratio As Double

                    Dim _laba_sebelum_pajak_persen, _biaya_marketing_persen, _biaya_adum_persen, _biaya_operasional_persen As Double
                    Dim _harta_lancar, _hutang_lancar, _kas_setara_kas, _persediaan, _hutang, _modal As Double

                    _hutang = 0.0
                    _modal = 0.0
                    _persediaan = 0.0
                    _kas_setara_kas = 0.0
                    _harta_lancar = 0.0
                    _hutang_lancar = 0.0
                    _penjualan = 0.0
                    _hpp = 0.0
                    _biaya_op = 0.0
                    _laba_kotor = 0.0
                    _laba_operasi = 0.0
                    _laba_sebelum_pajak = 0.0
                    _pendapatan_lain = 0.0
                    _biaya_lain = 0.0
                    _penjualan_bersih = 0.0

                    _laba_sebelum_pajak_persen = 0.0
                    _biaya_marketing_persen = 0.0
                    _biaya_adum_persen = 0.0
                    _biaya_operasional_persen = 0.0



                    _gpm = 0.0
                    _om = 0.0
                    _margin_laba_bersih = 0.0
                    _percent_hpp = 0.0
                    _percent_adm_umum = 0.0
                    _percent_total_biaya = 0.0

                    _biaya_operasional = 0.0
                    _biaya_adum = 0.0
                    _biaya_marketing = 0.0
                    _net_work_modal = 0.0
                    _working_capital = 0.0
                    _current_ratio = 0.0
                    _quick_ratio = 0.0
                    _der_ratio = 0.0


                    For Each dr_bs_new As DataRow In dt_bs_ok.Rows
                        If dr_bs_new("bsd_caption") = "Harta Lancar" Then
                            _harta_lancar = _harta_lancar + SetNumber(dr_bs_new("z_nilai"))
                        ElseIf dr_bs_new("bsd_caption") = "Hutang Lancar" Then
                            _hutang_lancar = _hutang_lancar + SetNumber(dr_bs_new("z_nilai"))
                        End If
                        If dr_bs_new("bsdi_caption") = "Kas" Or dr_bs_new("bsdi_caption") = "Warkat Intransit" Or dr_bs_new("bsdi_caption") = "Bank" Then
                            _kas_setara_kas = _kas_setara_kas + SetNumber(dr_bs_new("z_nilai"))
                        End If

                        If dr_bs_new("bsdi_caption").ToString.ToLower.Contains("persediaan") Then
                            _persediaan = _persediaan + SetNumber(dr_bs_new("z_nilai"))
                        End If

                        If dr_bs_new("bs_caption") = "HARTA" Then
                            _hutang = _hutang + SetNumber(dr_bs_new("z_nilai"))
                        ElseIf dr_bs_new("bs_caption") = "MODAL" Then
                            _modal = _modal + SetNumber(dr_bs_new("z_nilai"))
                        End If
                    Next


                    For Each dr_bs As DataRow In dt_bs.Rows
                        If dr_bs("pls_item") = "Penjualan" Then
                            _penjualan = _penjualan + SetNumber(dr_bs("value"))
                        ElseIf dr_bs("pls_item") = "Biaya Marketing" Then
                            _biaya_marketing = _biaya_marketing + SetNumber(dr_bs("value"))
                        ElseIf dr_bs("pls_item") = "Biaya Operasional" Then
                            _biaya_operasional = _biaya_operasional + SetNumber(dr_bs("value"))
                        ElseIf dr_bs("pls_item") = "Biaya Administrasi dan Umum" Then
                            _biaya_adum = _biaya_adum + SetNumber(dr_bs("value"))
                        End If


                        _laba_sebelum_pajak = _laba_sebelum_pajak + SetNumber(dr_bs("value"))

                       

                    Next

                  
                    Try
                        _biaya_marketing_persen = _biaya_marketing / _penjualan * 100
                    Catch ex As Exception
                    End Try

                    Try
                        _biaya_operasional_persen = _biaya_operasional / _penjualan * 100
                    Catch ex As Exception
                    End Try

                    Try
                        _biaya_adum_persen = _biaya_adum / _penjualan * 100
                    Catch ex As Exception
                    End Try

                    Try
                        _laba_sebelum_pajak_persen = _laba_sebelum_pajak / _penjualan * 100
                    Catch ex As Exception
                    End Try

                    Try
                        ' _percent_adm_umum = (_biaya_op * -1.0) / _penjualan
                    Catch ex As Exception
                    End Try

                    Try
                        ' _percent_total_biaya = ((_biaya_op + _biaya_lain + _hpp) * -1.0) / (_penjualan + _pendapatan_lain)
                    Catch ex As Exception
                    End Try


                    dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows
                        If dr_setting("dash_type_id") = "MONTHLY_TREND-Penjualan Bruto" Then


                            Try
                                dr_setting("value") = _penjualan
                            Catch ex As Exception

                            End Try
                        ElseIf dr_setting("dash_type_id") = "MONTHLY_TREND-Biaya Marketing & Penjualan" Then

                            Try
                                dr_setting("value") = _biaya_marketing_persen
                            Catch ex As Exception

                            End Try

                        ElseIf dr_setting("dash_type_id") = "MONTHLY_TREND-Biaya Operasional" Then


                            Try
                                dr_setting("value") = _biaya_operasional_persen
                            Catch ex As Exception

                            End Try

                        ElseIf dr_setting("dash_type_id") = "MONTHLY_TREND-Biaya Administrasi dan Umum" Then
                            dr_setting("value") = _biaya_adum_persen
                        ElseIf dr_setting("dash_type_id") = "MONTHLY_TREND-Laba Rugi Sebelum Pajak" Then
                            dr_setting("value") = _laba_sebelum_pajak_persen

                        ElseIf dr_setting("dash_type_id") = "MONTHLY_TREND-Net Worth" Then

                            'sSQL = "select reportd_value from reportd_detail where reportd_type='BALANCE_SHEET-MODAL' and reportd_periode='" _
                            '    & CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM") & "'"

                            'Dim dt_temp As New DataTable

                            'dt_temp = GetTableData(sSQL)
                            'Dim _modal As Double = 0.0
                            'For Each dr_temp As DataRow In dt_temp.Rows
                            '    _modal = SetNumber(dr_temp(0))
                            'Next
                            'Try
                            '    dr_setting("value") = _modal
                            'Catch ex As Exception

                            'End Try
                            dr_setting("value") = _modal
                        ElseIf dr_setting("dash_type_id") = "MONTHLY_TREND-Working Capital" Then



                            dr_setting("value") = (_harta_lancar - _hutang_lancar)
                        ElseIf dr_setting("dash_type_id") = "MONTHLY_TREND-Current Ratio" Then
                            dr_setting("value") = _harta_lancar / _hutang_lancar

                        ElseIf dr_setting("dash_type_id") = "MONTHLY_TREND-Quick Ratio" Then
                            dr_setting("value") = (_harta_lancar - _persediaan) / _hutang_lancar

                        ElseIf dr_setting("dash_type_id") = "MONTHLY_TREND-Debt to Equity Ratio" Then
                            dr_setting("value") = _hutang / _modal
                        ElseIf dr_setting("dash_type_id") = "MONTHLY_TREND-Efektivitas Karyawan" Then
                            dr_setting("value") = 0.0



                        End If
                    Next

                    dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows

                        sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                            & " and reportd_type=" & SetSetring(dr_setting("dash_type_id")) & " and " _
                            & " reportd_periode= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM"))


                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next

                        sSQL = "INSERT INTO  " _
                            & "  public.reportd_detail " _
                            & "( " _
                            & "  reportd_oid, " _
                            & "  reportd_report_oid, " _
                            & "  reportd_type, " _
                            & "  reportd_periode, " _
                            & "  reportd_ac_id, " _
                            & "  reportd_ac_code, " _
                            & "  reportd_ac_hierarchy,reportd_group,reportd_date_generate, " _
                            & "  reportd_ac_name, " _
                            & "  reportd_value " _
                            & ") " _
                            & "VALUES ( " _
                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                            & SetSetring(_report_oid) & ",  " _
                            & SetSetring(dr_setting("dash_type_id")) & ",  " _
                            & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  'MONTHLY_TREND',   " _
                            & SetDateNTime(CekTanggal()) & "," _
                            & SetSetring("") & ",  " _
                            & SetDec(dr_setting("value")) & "  " _
                            & ")"

                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next
                    Next


                    For Each dr_setting As DataRow In dt_setting.Rows
                        dr_setting("value") = 0.0
                    Next
                    dt_setting.AcceptChanges()

                    _processed = _processed + 1.0
                    _percent = _processed / _count * 100
                    LblStatus.Text = "Processing .. " & cb_type.EditValue & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()
                Next

            ElseIf cb_type.EditValue = "Cash IN OUT New" Then

                sSQL = "SELECT  " _
                  & "  a.dash_type_id, " _
                  & "  a.dash_type_number, " _
                  & "  a.dash_type_filter, " _
                  & "  b.dashd_oid, " _
                  & "  b.dashd_dash_type_id, " _
                  & "  b.dashd_ac_id, " _
                  & "  public.ac_mstr.ac_code, " _
                  & "  public.ac_mstr.ac_code_hirarki, " _
                  & "  public.ac_mstr.ac_name,0.0 as value " _
                  & "FROM " _
                  & "  public.dashboard_setting a " _
                  & "  LEFT OUTER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id) " _
                  & "  LEFT OUTER JOIN public.ac_mstr ON (b.dashd_ac_id = public.ac_mstr.ac_id) " _
                  & "WHERE " _
                  & "  a.dash_type_filter = 'CASH_IN_OUT_NEW' " _
                  & "ORDER BY " _
                  & "  a.dash_type_filter, " _
                  & "  a.dash_type_number"

                Dim dt_setting As New DataTable
                dt_setting = GetTableData(sSQL)


                Dim dt_bs As New DataTable

                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)

                For Each dr_gl_cal As DataRow In dt_gl_cal.Rows
                    sSQL = "SELECT  " _
                        & " sum((SELECT  " _
                        & "       sum(glbal_balance_open) AS jml " _
                        & "       FROM " _
                        & "       public.glbal_balance x " _
                        & "       WHERE " _
                        & "       x.glbal_gcal_oid = '" & dr_gl_cal("gcal_oid").ToString & "' AND   " _
                        & "       x.glbal_ac_id = d.ac_id )) as total " _
                        & "FROM " _
                        & "  public.tconfsettingcashflow a " _
                        & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
                        & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code) " _
                        & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
                        & "WHERE " _
                        & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D' "

                    Dim _awal, _akhir, _cash_in, _cash_out As Double
                    _awal = 0
                    _akhir = 0
                    _cash_in = 0
                    _cash_out = 0

                    Dim dt_temp As New DataTable

                    dt_temp = master_new.PGSqlConn.GetTableData(sSQL)

                    For Each dr As DataRow In dt_temp.Rows
                        _awal = dr("total")
                    Next

                    sSQL = "SELECT  " _
                        & " 'CASH-CASH IN' as type, sum((public.glt_det.glt_debit * public.glt_det.glt_exc_rate)) AS glt_value " _
                        & "FROM " _
                        & "  public.glt_det " _
                        & "WHERE " _
                        & " to_char(public.glt_det.glt_date,'yyyyMM') = " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & " AND  " _
                        & "  public.glt_det.glt_ac_id in (SELECT   d.ac_id " _
                            & "FROM " _
                            & "  public.tconfsettingcashflow a " _
                            & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
                            & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code) " _
                            & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
                            & "WHERE " _
                            & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D' ) union all " _
                        & "SELECT  " _
                        & " 'CASH-CASH OUT' as type, sum((public.glt_det.glt_credit * public.glt_det.glt_exc_rate)) AS glt_value " _
                        & "FROM " _
                        & "  public.glt_det " _
                        & "WHERE " _
                        & "  to_char(public.glt_det.glt_date,'yyyyMM')= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & " AND  " _
                        & "  public.glt_det.glt_ac_id in (SELECT   d.ac_id " _
                            & "FROM " _
                            & "  public.tconfsettingcashflow a " _
                            & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
                            & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code) " _
                            & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
                            & "WHERE " _
                            & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D')"

                    Dim dt_temp2 As New DataTable
                    dt_temp2 = master_new.PGSqlConn.GetTableData(sSQL)

                    For Each dr As DataRow In dt_temp2.Rows
                        If dr("type") = "CASH-CASH IN" Then
                            _cash_in = dr("glt_value")

                        ElseIf dr("type") = "CASH-CASH OUT" Then
                            _cash_out = dr("glt_value")
                        End If
                    Next

                    For Each dr_setting As DataRow In dt_setting.Rows
                        If dr_setting("dash_type_id") = "CASH_IN_OUT_NEW-BEGINNING BALANCE  REALIZATION" Then
                            dr_setting("value") = _awal
                        ElseIf dr_setting("dash_type_id") = "CASH_IN_OUT_NEW-CASH IN REALIZATION" Then
                            dr_setting("value") = _cash_in
                        ElseIf dr_setting("dash_type_id") = "CASH_IN_OUT_NEW-CASH OUT REALIZATION" Then
                            dr_setting("value") = _cash_out
                        ElseIf dr_setting("dash_type_id") = "CASH_IN_OUT_NEW-ENDING BALANCE REALIZATION" Then
                            dr_setting("value") = _awal - _cash_in + _cash_out
                        End If
                    Next

                    dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows


                        sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                            & " and reportd_type=" & SetSetring(dr_setting("dash_type_id")) & " and " _
                            & " reportd_periode= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM"))


                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next

                        sSQL = "INSERT INTO  " _
                            & "  public.reportd_detail " _
                            & "( " _
                            & "  reportd_oid, " _
                            & "  reportd_report_oid, " _
                            & "  reportd_type, " _
                            & "  reportd_periode, " _
                            & "  reportd_ac_id, " _
                            & "  reportd_ac_code, " _
                            & "  reportd_ac_hierarchy, " _
                            & "  reportd_ac_name, reportd_group,reportd_date_generate," _
                            & "  reportd_value " _
                            & ") " _
                            & "VALUES ( " _
                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                            & SetSetring(_report_oid) & ",  " _
                            & SetSetring(dr_setting("dash_type_id")) & ",  " _
                            & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ", 'CASH_IN_OUT_NEW',   " _
                            & SetDateNTime(CekTanggal()) & "," _
                            & SetDec(dr_setting("value")) & "  " _
                            & ")"

                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next
                    Next


                    For Each dr_setting As DataRow In dt_setting.Rows
                        dr_setting("value") = 0.0
                    Next
                    dt_setting.AcceptChanges()


                    _processed = _processed + 1.0
                    _percent = _processed / _count * 100
                    LblStatus.Text = "Processing .. " & cb_type.EditValue & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()

                Next
            ElseIf cb_type.EditValue = "Cash Flow New" Then


                sSQL = "SELECT  " _
                  & "  a.dash_type_id, " _
                  & "  a.dash_type_number, " _
                  & "  a.dash_type_filter, " _
                  & "  b.dashd_oid, " _
                  & "  b.dashd_dash_type_id, " _
                  & "  b.dashd_ac_id, " _
                  & "  public.ac_mstr.ac_code, " _
                  & "  public.ac_mstr.ac_code_hirarki, " _
                  & "  public.ac_mstr.ac_name,0.0 as value " _
                  & "FROM " _
                  & "  public.dashboard_setting a " _
                  & "  LEFT OUTER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id) " _
                  & "  LEFT OUTER JOIN public.ac_mstr ON (b.dashd_ac_id = public.ac_mstr.ac_id) " _
                  & "WHERE " _
                  & "  a.dash_type_filter = 'CASH_FLOW_NEW' " _
                  & "ORDER BY " _
                  & "  a.dash_type_filter, " _
                  & "  a.dash_type_number"

                Dim dt_setting As New DataTable
                dt_setting = GetTableData(sSQL)


                Dim dt_bs As New DataTable

                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)

                For Each dr_gl_cal As DataRow In dt_gl_cal.Rows



                    sSQL = "SELECT     " _
                        & "a.code,   a.remark,   a.sort_number,   a.remark_header,    " _
                        & "a.remark_footer,   a.cfsign_header,   a.cf_value_sign,   b.cfdet_oid,    " _
                        & "b.cfdet_pk,   b.seq, 0.0 as cf_value_beginning,0.0 as cf_value_ending,b.sub_header2,     " _
                        & "b.sub_header,coalesce((select   sum(f_get_cfvalue_direct(d.ac_id,cast('" & dr_gl_cal("gcal_oid").ToString & "' as uuid),c.ac_sign,ac_value)) as _value  " _
                        & "from public.tconfsettingcashflowdet_item c    " _
                        & "INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki)   " _
                        & "where c.code=b.cfdet_pk and d.ac_is_sumlevel='N'),0) * ac_value_header as cf_value  " _
                        & "FROM   public.tconfsettingcashflow a    " _
                        & "INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code)  " _
                        & "WHERE   a.cfsign_header = 'T'  and cf_type='D'"




                    Dim _total As Double = 0
                    dt_bs = GetTableData(sSQL)


                    sSQL = "SELECT  " _
                        & " sum((SELECT  " _
                        & "       sum(glbal_balance_open) AS jml " _
                        & "       FROM " _
                        & "       public.glbal_balance x " _
                        & "       WHERE " _
                        & "       x.glbal_gcal_oid = '" & dr_gl_cal("gcal_oid").ToString & "' AND   " _
                        & "       x.glbal_ac_id = d.ac_id )) as total " _
                        & "FROM " _
                        & "  public.tconfsettingcashflow a " _
                        & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
                        & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code) " _
                        & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
                        & "WHERE " _
                        & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D' "

                    Dim _awal, _akhir As Double
                    _awal = 0
                    _akhir = 0

                    Dim dt_temp As New DataTable

                    dt_temp = master_new.PGSqlConn.GetTableData(sSQL)

                    For Each dr As DataRow In dt_temp.Rows
                        _awal = dr("total")
                    Next

                    For Each dr_bs As DataRow In dt_bs.Rows
                        For Each dr_setting As DataRow In dt_setting.Rows
                            If dr_bs("code") = "cfd_usaha" And dr_setting("dash_type_id") = "CASH_FLOW_NEW-OPERATING" Then
                                dr_setting("value") = dr_setting("value") + SetNumber(dr_bs("cf_value"))

                            ElseIf dr_bs("code") = "cfd_investasi" And dr_setting("dash_type_id") = "CASH_FLOW_NEW-INVESTING" Then
                                dr_setting("value") = dr_setting("value") + SetNumber(dr_bs("cf_value"))

                            ElseIf dr_bs("code") = "cfd_keuangan" And dr_setting("dash_type_id") = "CASH_FLOW_NEW-FINANCING" Then
                                dr_setting("value") = dr_setting("value") + SetNumber(dr_bs("cf_value"))

                            End If
                            If dr_setting("dash_type_id") = "CASH_FLOW_NEW-INCREASE / (DECREASE) ON CASH" Then
                                dr_setting("value") = dr_setting("value") + SetNumber(dr_bs("cf_value"))
                            End If

                        Next

                        _total = _total + SetNumber(dr_bs("cf_value"))

                    Next

                    dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows
                        If dr_setting("dash_type_id") = "CASH_FLOW_NEW-BEGINNING BALANCE" Then
                            dr_setting("value") = _awal
                        ElseIf dr_setting("dash_type_id") = "CASH_FLOW_NEW-CASH BALANCE" Then
                            dr_setting("value") = _awal + _total
                        End If
                    Next

                    dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows


                        sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                            & " and reportd_type=" & SetSetring(dr_setting("dash_type_id")) & " and " _
                            & " reportd_periode= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM"))


                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next

                        sSQL = "INSERT INTO  " _
                            & "  public.reportd_detail " _
                            & "( " _
                            & "  reportd_oid, " _
                            & "  reportd_report_oid, " _
                            & "  reportd_type, " _
                            & "  reportd_periode, " _
                            & "  reportd_ac_id, " _
                            & "  reportd_ac_code, " _
                            & "  reportd_ac_hierarchy, " _
                            & "  reportd_ac_name, reportd_group,reportd_date_generate," _
                            & "  reportd_value " _
                            & ") " _
                            & "VALUES ( " _
                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                            & SetSetring(_report_oid) & ",  " _
                            & SetSetring(dr_setting("dash_type_id")) & ",  " _
                            & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ", 'CASH_FLOW_NEW',   " _
                            & SetDateNTime(CekTanggal()) & "," _
                            & SetDec(dr_setting("value")) & "  " _
                            & ")"

                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next
                    Next


                    For Each dr_setting As DataRow In dt_setting.Rows
                        dr_setting("value") = 0.0
                    Next
                    dt_setting.AcceptChanges()


                    _processed = _processed + 1.0
                    _percent = _processed / _count * 100
                    LblStatus.Text = "Processing .. " & cb_type.EditValue & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()

                Next
            End If


            'Box("Generate success")

            LblStatus.Text = "Processing .. " & cb_type.EditValue & " Success"
            System.Windows.Forms.Application.DoEvents()

            _is_running = False

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub



    Private Function gendata(ByVal par_type As String) As Boolean
        Try

            sSQL = "SELECT  " _
                    & "  db_code, " _
                    & "  db_desc, " _
                    & "  db_active, " _
                    & "  db_host, " _
                    & "  db_port, " _
                    & "  db_database, " _
                    & "  db_user " _
                    & "FROM  " _
                    & "  public.dashboard_db_mstr  " _
                    & "WHERE db_code='DASHBOARD'"

            Dim dt_db As New DataTable
            dt_db = GetTableData(sSQL)

            Dim _report_oid As String = ""


            For Each dr_db As DataRow In dt_db.Rows
                sSQL = "SELECT report_oid, report_db from report_mstr where report_db='" & func_coll.get_conf_file("syspro_approval_code") & "'"

                Dim dt_report As New DataTable
                dt_report = GetTableData(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                For Each dr_report As DataRow In dt_report.Rows
                    _report_oid = dr_report("report_oid").ToString
                Next
            Next


            sSQL = "SELECT  " _
                   & "  a.gcal_oid, " _
                   & "  a.gcal_dom_id, " _
                   & "  a.gcal_add_by, " _
                   & "  a.gcal_add_date, " _
                   & "  a.gcal_upd_by, " _
                   & "  a.gcal_upd_date, " _
                   & "  a.gcal_year, " _
                   & "  a.gcal_periode, " _
                   & "  a.gcal_start_date, " _
                   & "  a.gcal_end_date, " _
                   & "  a.gcal_dt, " _
                   & "  a.gcal_pra_closing, " _
                   & "  a.gcal_closing " _
                   & "FROM " _
                   & "  public.gcal_mstr a " _
                   & "where  " _
                   & "gcal_start_date >= (select gcal_start_date from gcal_mstr where gcal_oid='" & le_glperiode.EditValue & "') " _
                   & "order by gcal_start_date limit " & SetInteger(te_month.EditValue)


            Dim dt_gl_cal As New DataTable
            dt_gl_cal = GetTableData(sSQL)

            Dim _percent As Double = 0.0
            If par_type = "Balance Sheet" Then

                sSQL = "SELECT  " _
                   & "  a.dash_type_id, " _
                   & "  a.dash_type_number, " _
                   & "  a.dash_type_filter, " _
                   & "  0.0 as value " _
                   & "FROM " _
                   & "  public.dashboard_setting a " _
                   & "WHERE " _
                   & "  a.dash_type_filter = 'BALANCE_SHEET' " _
                   & "ORDER BY " _
                   & "  a.dash_type_filter, " _
                   & "  a.dash_type_number"


                'sSQL = "SELECT  " _
                '  & "  a.dash_type_id, " _
                '  & "  a.dash_type_number, " _
                '  & "  a.dash_type_filter, " _
                '  & "  b.dashd_oid, " _
                '  & "  b.dashd_dash_type_id, " _
                '  & "  b.dashd_ac_id, " _
                '  & "  public.ac_mstr.ac_code, " _
                '  & "  public.ac_mstr.ac_code_hirarki, " _
                '  & "  public.ac_mstr.ac_name,0.0 as value " _
                '  & "FROM " _
                '  & "  public.dashboard_setting a " _
                '  & "  INNER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id) " _
                '  & "  LEFT OUTER JOIN public.ac_mstr ON (b.dashd_ac_id = public.ac_mstr.ac_id) " _
                '  & "WHERE " _
                '  & "  a.dash_type_filter = 'BALANCE_SHEET' " _
                '  & "ORDER BY " _
                '  & "  a.dash_type_filter, " _
                '  & "  a.dash_type_number"


                Dim dt_setting As New DataTable
                dt_setting = GetTableData(sSQL)


                Dim dt_bs As New DataTable
                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)

                For Each dr_gl_cal As DataRow In dt_gl_cal.Rows

                    sSQL = "select sum(z_nilai) as value, bs_caption from  " _
                      & "(SELECT a.bs_number,  a.bs_caption,  a.bs_group,   " _
                      & "a.bs_remarks,  b.bsd_number,  b.bsd_caption,   " _
                      & "b.bsd_remarks, c.bsdi_number,  c.bsdi_caption,   " _
                      & "c.bsdi_oid,    " _
                      & "(select sum(jml) from (SELECT  (select sum(v_nilai) as nilai from (  " _
                      & "SELECT y.ac_id,   y.ac_code_hirarki, y.ac_code,   y.ac_name,    " _
                      & "y.ac_type,f_get_balance_sheet(y.ac_id,1,1,0,cast('" & dr_gl_cal("gcal_oid").ToString & "' as uuid),'" & dr_gl_cal("gcal_closing") & "') as v_nilai  " _
                      & "FROM   public.ac_mstr y WHERE   substring(y.ac_code_hirarki, 1, length(x.bsda_ac_hirarki)) = x.bsda_ac_hirarki AND     " _
                      & "y.ac_is_sumlevel = 'N') as temp) as jml  FROM   public.bsda_account x WHERE   x.bsda_bsdi_oid = c.bsdi_oid) as temp2)   as z_nilai   " _
                      & "FROM   public.bs_mstr a    " _
                      & "INNER JOIN public.bsd_det b ON (a.bs_number = b.bsd_bs_number)    " _
                      & "INNER JOIN public.bsdi_det_item c ON (b.bsd_oid = c.bsdi_bsd_oid)  " _
                      & ") as tp group by bs_caption"

                    dt_bs = GetTableData(sSQL)

                    For Each dr_bs As DataRow In dt_bs.Rows
                        For Each dr_setting As DataRow In dt_setting.Rows
                            If dr_bs("bs_caption") = "HARTA" And dr_setting("dash_type_id") = "BALANCE_SHEET-ASSET" Then
                                dr_setting("value") = dr_bs("value")
                            ElseIf dr_bs("bs_caption") = "KEWAJIBAN" And dr_setting("dash_type_id") = "BALANCE_SHEET-KEWAJIBAN" Then
                                dr_setting("value") = dr_bs("value")
                            ElseIf dr_bs("bs_caption") = "MODAL" And dr_setting("dash_type_id") = "BALANCE_SHEET-MODAL" Then
                                dr_setting("value") = dr_bs("value")
                            End If
                        Next
                    Next

                    dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows

                        sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                            & " and reportd_type=" & SetSetring(dr_setting("dash_type_id")) & " and " _
                            & " reportd_periode= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM"))


                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next

                        sSQL = "INSERT INTO  " _
                            & "  public.reportd_detail " _
                            & "( " _
                            & "  reportd_oid, " _
                            & "  reportd_report_oid, " _
                            & "  reportd_type, " _
                            & "  reportd_periode, " _
                            & "  reportd_ac_id, " _
                            & "  reportd_ac_code, " _
                            & "  reportd_ac_hierarchy, " _
                            & "  reportd_ac_name,reportd_group," _
                            & "  reportd_value " _
                            & ") " _
                            & "VALUES ( " _
                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                            & SetSetring(_report_oid) & ",  " _
                            & SetSetring(dr_setting("dash_type_id")) & ",  " _
                            & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",'BALANCE_SHEET',   " _
                            & SetDec(dr_setting("value")) & "  " _
                            & ")"

                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next
                    Next


                    For Each dr_setting As DataRow In dt_setting.Rows
                        dr_setting("value") = 0.0
                    Next
                    dt_setting.AcceptChanges()

                    _processed = _processed + 1.0
                    _percent = _processed / _count * 100
                    LblStatus.Text = "Processing .. " & par_type & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()
                Next


            ElseIf par_type = "Profit Loss" Then


                sSQL = "SELECT  " _
                  & "  a.dash_type_id, " _
                  & "  a.dash_type_number, " _
                  & "  a.dash_type_filter,0.0 as value " _
                  & "FROM " _
                  & "  public.dashboard_setting a " _
                  & "WHERE " _
                  & "  a.dash_type_filter = 'PROFIT_LOSS' " _
                  & "ORDER BY " _
                  & "  a.dash_type_filter, " _
                  & "  a.dash_type_number"


                'sSQL = "SELECT  " _
                '  & "  a.dash_type_id, " _
                '  & "  a.dash_type_number, " _
                '  & "  a.dash_type_filter, " _
                '  & "  b.dashd_oid, " _
                '  & "  b.dashd_dash_type_id, " _
                '  & "  b.dashd_ac_id, " _
                '  & "  public.ac_mstr.ac_code, " _
                '  & "  public.ac_mstr.ac_code_hirarki, " _
                '  & "  public.ac_mstr.ac_name,0.0 as value " _
                '  & "FROM " _
                '  & "  public.dashboard_setting a " _
                '  & "  LEFT OUTER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id) " _
                '  & "  LEFT OUTER JOIN public.ac_mstr ON (b.dashd_ac_id = public.ac_mstr.ac_id) " _
                '  & "WHERE " _
                '  & "  a.dash_type_filter = 'PROFIT_LOSS' " _
                '  & "ORDER BY " _
                '  & "  a.dash_type_filter, " _
                '  & "  a.dash_type_number"


                Dim dt_setting As New DataTable
                dt_setting = GetTableData(sSQL)


                Dim dt_bs As New DataTable

                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)

                For Each dr_gl_cal As DataRow In dt_gl_cal.Rows


                    sSQL = "SELECT    a.pl_oid,   a.pl_footer,   a.pl_sign,   a.pl_number,    " _
                      & "b.pls_oid,   b.pls_item,   b.pls_number,   c.pla_ac_id,   d.ac_code,    " _
                      & "d.ac_name,   c.pla_ac_hirarki,(select  sum(v_nilai) as jml from  " _
                      & "( SELECT    x.ac_id,   x.ac_code_hirarki,   x.ac_code,   x.ac_name,    " _
                      & "x.ac_type,f_get_balance_sheet_pl(x.ac_id,1,1,0,cast('" & dr_gl_cal("gcal_oid").ToString & "' as uuid),'" & dr_gl_cal("gcal_closing") & "') as v_nilai  " _
                      & "FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1, length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
                      & "ac_is_sumlevel = 'N') as temp) * pls_value as value FROM   public.pl_setting_mstr a    " _
                      & "INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid)    " _
                      & "INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid)    " _
                      & "INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id) ORDER BY   a.pl_number,   b.pls_number "


                    dt_bs = GetTableData(sSQL)
                    Dim _penjualan, _hpp, _biaya_op, _laba_kotor, _laba_operasi, _laba_sebelum_pajak, _pendapatan_lain, _biaya_lain As Double
                    Dim _gpm, _om, _margin_laba_bersih, _percent_hpp, _percent_adm_umum, _percent_total_biaya As Double
                    _penjualan = 0.0
                    _hpp = 0.0
                    _biaya_op = 0.0
                    _laba_kotor = 0.0
                    _laba_operasi = 0.0
                    _laba_sebelum_pajak = 0.0
                    _pendapatan_lain = 0.0
                    _biaya_lain = 0.0


                    _gpm = 0.0
                    _om = 0.0
                    _margin_laba_bersih = 0.0
                    _percent_hpp = 0.0
                    _percent_adm_umum = 0.0
                    _percent_total_biaya = 0.0



                    For Each dr_bs As DataRow In dt_bs.Rows
                        For Each dr_setting As DataRow In dt_setting.Rows
                            If dr_bs("pl_footer") = "Penjualan Bersih" And dr_setting("dash_type_id") = "PROFIT_LOSS-PENDAPATAN" Then
                                dr_setting("value") = dr_setting("value") + dr_bs("value")
                                _penjualan = _penjualan + dr_bs("value")
                            ElseIf dr_bs("pl_footer") = "Laba Rugi Kotor" And dr_setting("dash_type_id") = "PROFIT_LOSS-HPP" Then
                                dr_setting("value") = dr_setting("value") + dr_bs("value")
                                _hpp = _hpp + dr_bs("value")
                            ElseIf dr_bs("pl_footer") = "Laba Rugi Operasional" And dr_setting("dash_type_id") = "PROFIT_LOSS-BEBAN OPERASIONAL" Then
                                dr_setting("value") = dr_setting("value") + dr_bs("value")
                                _biaya_op = _biaya_op + dr_bs("value")

                            ElseIf dr_bs("pls_item") = "Pendapatan Lain-Lain" And dr_setting("dash_type_id") = "PROFIT_LOSS-PENDAPATAN LAIN-LAIN" Then

                                dr_setting("value") = dr_setting("value") + dr_bs("value")
                                _pendapatan_lain = _pendapatan_lain + dr_bs("value")

                            ElseIf dr_bs("pls_item") = "Biaya Lain-Lain" And dr_setting("dash_type_id") = "PROFIT_LOSS-BIAYA LAIN-LAIN" Then
                                dr_setting("value") = dr_setting("value") + dr_bs("value")
                                _biaya_lain = _biaya_lain + dr_bs("value")
                            End If


                        Next
                    Next

                    _laba_kotor = _penjualan + _hpp
                    _laba_operasi = _laba_kotor + _biaya_op
                    _laba_sebelum_pajak = _laba_operasi + _pendapatan_lain + _biaya_lain
                    Try
                        _gpm = _laba_kotor / _penjualan
                    Catch ex As Exception

                    End Try

                    Try
                        _om = _laba_operasi / _penjualan
                    Catch ex As Exception

                    End Try
                    Try
                        _margin_laba_bersih = _laba_sebelum_pajak / _penjualan
                    Catch ex As Exception

                    End Try

                    Try
                        _percent_hpp = (_hpp * -1.0) / _penjualan
                    Catch ex As Exception

                    End Try

                    Try
                        _percent_adm_umum = (_biaya_op * -1.0) / _penjualan
                    Catch ex As Exception

                    End Try
                    Try
                        _percent_total_biaya = ((_biaya_op + _biaya_lain + _hpp) * -1.0) / (_penjualan + _pendapatan_lain)
                    Catch ex As Exception

                    End Try


                    dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows
                        If dr_setting("dash_type_id") = "PROFIT_LOSS-LABA KOTOR" Then
                            dr_setting("value") = _laba_kotor
                        ElseIf dr_setting("dash_type_id") = "PROFIT_LOSS-LABA OPERASI" Then
                            dr_setting("value") = _laba_operasi

                        ElseIf dr_setting("dash_type_id") = "PROFIT_LOSS-LABA SEBELUM PAJAK" Then
                            dr_setting("value") = _laba_sebelum_pajak


                        ElseIf dr_setting("dash_type_id") = "PROFIT_LOSS-MARGIN LABA KOTOR (GPM)" Then
                            dr_setting("value") = _gpm
                        ElseIf dr_setting("dash_type_id") = "PROFIT_LOSS-MARGIN LABA OPERASI (OM)" Then
                            dr_setting("value") = _om
                        ElseIf dr_setting("dash_type_id") = "PROFIT_LOSS-MARGIN LABA BERSIH" Then
                            dr_setting("value") = _margin_laba_bersih
                        ElseIf dr_setting("dash_type_id") = "PROFIT_LOSS-% HPP" Then
                            dr_setting("value") = _percent_hpp
                        ElseIf dr_setting("dash_type_id") = "PROFIT_LOSS-% BIAYA ADM & UMUM" Then
                            dr_setting("value") = _percent_adm_umum
                        ElseIf dr_setting("dash_type_id") = "PROFIT_LOSS-% TOTAL BIAYA" Then
                            dr_setting("value") = _percent_total_biaya
                        End If
                    Next

                    dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows

                        sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                            & " and reportd_type=" & SetSetring(dr_setting("dash_type_id")) & " and " _
                            & " reportd_periode= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM"))


                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next

                        sSQL = "INSERT INTO  " _
                            & "  public.reportd_detail " _
                            & "( " _
                            & "  reportd_oid, " _
                            & "  reportd_report_oid, " _
                            & "  reportd_type, " _
                            & "  reportd_periode, " _
                            & "  reportd_ac_id, " _
                            & "  reportd_ac_code, " _
                            & "  reportd_ac_hierarchy, " _
                            & "  reportd_ac_name, " _
                            & "  reportd_value " _
                            & ") " _
                            & "VALUES ( " _
                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                            & SetSetring(_report_oid) & ",  " _
                            & SetSetring(dr_setting("dash_type_id")) & ",  " _
                            & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetDec(dr_setting("value")) & "  " _
                            & ")"

                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next
                    Next


                    For Each dr_setting As DataRow In dt_setting.Rows
                        dr_setting("value") = 0.0
                    Next
                    dt_setting.AcceptChanges()

                    _processed = _processed + 1.0
                    _percent = _processed / _count * 100
                    LblStatus.Text = "Processing .. " & par_type & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()
                Next


            ElseIf par_type = "Cash and Accounts Receivable" Then

                Dim dt_bs As New DataTable
                Dim dt_ac As New DataTable


                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)
                Dim _last_date As Date
                _last_date = DateAdd(DateInterval.Month, te_month.EditValue, CDate(le_glperiode.GetColumnValue("gcal_start_date")))
                Dim _count_day As Integer
                _count_day = DateDiff(DateInterval.Day, CDate(le_glperiode.GetColumnValue("gcal_start_date")), _last_date)
                Dim _date_current As Date


                For n As Integer = 0 To _count_day - 1


                    _date_current = DateAdd(DateInterval.Day, n, CDate(le_glperiode.GetColumnValue("gcal_start_date")))

                    sSQL = "SELECT  " _
                        & " 'CASH_AR-KAS' as type, sum((public.glt_det.glt_debit * public.glt_det.glt_exc_rate) - (public.glt_det.glt_credit * public.glt_det.glt_exc_rate)) AS glt_value " _
                        & "FROM " _
                        & "  public.glt_det " _
                        & "WHERE " _
                        & "  public.glt_det.glt_date = " & SetDateNTime00(_date_current) & " AND  " _
                        & "  public.glt_det.glt_ac_id in (SELECT  x.ac_id FROM   public.dashboard_setting a   " _
                        & "INNER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id)  " _
                        & " LEFT OUTER JOIN public.ac_mstr c ON (b.dashd_ac_id = c.ac_id)   " _
                        & "LEFT OUTER JOIN public.ac_mstr x ON (substring(x.ac_code_hirarki, 1, length(c.ac_code_hirarki)) = c.ac_code_hirarki)   " _
                        & "WHERE   a.dash_type_id in ('ACCOUNT-KAS','ACCOUNT-BANK') and x.ac_is_sumlevel='N' ) union all " _
                        & "SELECT  " _
                       & " 'CASH_AR-PIUTANG USAHA' as type, sum((public.glt_det.glt_debit * public.glt_det.glt_exc_rate) - (public.glt_det.glt_credit * public.glt_det.glt_exc_rate)) AS glt_value " _
                       & "FROM " _
                       & "  public.glt_det " _
                       & "WHERE " _
                       & "  public.glt_det.glt_date = " & SetDateNTime00(_date_current) & " AND  " _
                       & "  public.glt_det.glt_ac_id in (SELECT  x.ac_id FROM   public.dashboard_setting a   " _
                       & "INNER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id)  " _
                       & " LEFT OUTER JOIN public.ac_mstr c ON (b.dashd_ac_id = c.ac_id)   " _
                       & "LEFT OUTER JOIN public.ac_mstr x ON (substring(x.ac_code_hirarki, 1, length(c.ac_code_hirarki)) = c.ac_code_hirarki)   " _
                       & "WHERE   a.dash_type_id ='ACCOUNT-PIUTANG USAHA' and x.ac_is_sumlevel='N' )"

                    dt_ac = GetTableData(sSQL)

                    For Each dr_ac As DataRow In dt_ac.Rows
                        If SetNumber(dr_ac("glt_value")) <> 0 Then
                            sSQL = "delete from reportd_detail_sub where reportd_report_oid=" & SetSetring(_report_oid) _
                                & " and reportd_type=" & SetSetring(dr_ac("type")) & " and reportd_group='CASH_AR' and " _
                                & " reportd_periode= " & SetSetring(Format(_date_current, "yyyyMMdd"))


                            For Each dr_db As DataRow In dt_db.Rows
                                DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                            Next

                            sSQL = "INSERT INTO  " _
                                & "  public.reportd_detail_sub " _
                                & "( " _
                                & "  reportd_oid, " _
                                & "  reportd_report_oid, " _
                                & "  reportd_type, " _
                                & "  reportd_periode, " _
                                & "  reportd_ac_id, " _
                                & "  reportd_ac_code, " _
                                & "  reportd_ac_hierarchy, " _
                                & "  reportd_ac_name,reportd_date,reportd_group, reportd_date_generate," _
                                & "  reportd_value " _
                                & ") " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(_report_oid) & ",  " _
                                & SetSetring(dr_ac("type")) & ",  " _
                                & SetSetring(Format(_date_current, "yyyyMMdd")) & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetDateNTime00(_date_current) & ", 'CASH_AR', " _
                                & SetDateNTime(CekTanggal) & ",  " _
                                & SetDec(dr_ac("glt_value")) & "  " _
                                & ")"

                            For Each dr_db As DataRow In dt_db.Rows
                                DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                            Next
                        End If

                    Next



                    _percent = (n + 1) / _count_day * 100
                    LblStatus.Text = "Processing .. " & par_type & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()

                Next

            ElseIf par_type = "Cash In vs Cash Out" Then
                Dim dt_ac As New DataTable
                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)
                For Each dr_gl_cal As DataRow In dt_gl_cal.Rows
                    sSQL = "SELECT  " _
                       & " 'CASH-CASH IN' as type, sum((public.glt_det.glt_debit * public.glt_det.glt_exc_rate)) AS glt_value " _
                       & "FROM " _
                       & "  public.glt_det " _
                       & "WHERE " _
                       & " to_char(public.glt_det.glt_date,'yyyyMM') = " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & " AND  " _
                       & "  public.glt_det.glt_ac_id in (SELECT  x.ac_id FROM   public.dashboard_setting a   " _
                       & "INNER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id)  " _
                       & " LEFT OUTER JOIN public.ac_mstr c ON (b.dashd_ac_id = c.ac_id)   " _
                       & "LEFT OUTER JOIN public.ac_mstr x ON (substring(x.ac_code_hirarki, 1, length(c.ac_code_hirarki)) = c.ac_code_hirarki)   " _
                       & "WHERE   a.dash_type_id in ('ACCOUNT-KAS','ACCOUNT-BANK') and x.ac_is_sumlevel='N' ) union all " _
                        & "SELECT  " _
                       & " 'CASH-CASH OUT' as type, sum((public.glt_det.glt_credit * public.glt_det.glt_exc_rate)) AS glt_value " _
                       & "FROM " _
                       & "  public.glt_det " _
                       & "WHERE " _
                       & "  to_char(public.glt_det.glt_date,'yyyyMM')= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & " AND  " _
                       & "  public.glt_det.glt_ac_id in (SELECT  x.ac_id FROM   public.dashboard_setting a   " _
                       & "INNER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id)  " _
                       & " LEFT OUTER JOIN public.ac_mstr c ON (b.dashd_ac_id = c.ac_id)   " _
                       & "LEFT OUTER JOIN public.ac_mstr x ON (substring(x.ac_code_hirarki, 1, length(c.ac_code_hirarki)) = c.ac_code_hirarki)   " _
                       & "WHERE   a.dash_type_id in ('ACCOUNT-KAS','ACCOUNT-BANK') and x.ac_is_sumlevel='N' )"

                    dt_ac = GetTableData(sSQL)
                    For Each dr_ac As DataRow In dt_ac.Rows
                        If SetNumber(dr_ac("glt_value")) <> 0 Then
                            sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                              & " and reportd_type=" & SetSetring(dr_ac("type")) & "  and reportd_group='CASH_IN_CASH_OUT' and " _
                              & " reportd_periode= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM"))


                            For Each dr_db As DataRow In dt_db.Rows
                                DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                            Next

                            sSQL = "INSERT INTO  " _
                                & "  public.reportd_detail " _
                                & "( " _
                                & "  reportd_oid, " _
                                & "  reportd_report_oid, " _
                                & "  reportd_type, " _
                                & "  reportd_periode, " _
                                & "  reportd_ac_id, " _
                                & "  reportd_ac_code, " _
                                & "  reportd_ac_hierarchy, " _
                                & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                                & "  reportd_value " _
                                & ") " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(_report_oid) & ",  " _
                                & SetSetring(dr_ac("type")) & ",  " _
                                & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & "'CASH_IN_CASH_OUT', " _
                                & SetDateNTime(CekTanggal) & ",  " _
                                & SetDec(dr_ac("glt_value")) & "  " _
                                & ")"

                            For Each dr_db As DataRow In dt_db.Rows
                                DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                            Next
                        End If
                    Next
                    _processed = _processed + 1.0
                    _percent = _processed / _count * 100
                    LblStatus.Text = "Processing .. " & par_type & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()
                Next

            ElseIf par_type = "AR Customer" Then

                sSQL = "SELECT  " _
                    & " b.ptnr_code, b.ptnr_name, sum(a.ar_amount  * a.ar_exc_rate) AS ar_amount, sum(coalesce(a.ar_pay_amount, 0.0) * a.ar_exc_rate) AS ar_payment_amount, sum((a.ar_amount - coalesce(a.ar_pay_amount, 0.0)) * a.ar_exc_rate) AS ar_outstanding_amount, " _
                    & " min(a.ar_due_date) as first_due_date, max(a.ar_due_date) as last_due_date " _
                    & "FROM " _
                    & "  public.ar_mstr a " _
                    & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
                    & "WHERE " _
                    & "  a.ar_status IS NULL  " _
                    & "  GROUP by ptnr_code, ptnr_name"

                Dim dt_data As New DataTable
                dt_data = GetTableData(sSQL)

                sSQL = "delete from reportd_detail_customer_piutang where reportd_report_oid=" & SetSetring(_report_oid)

                For Each dr_db As DataRow In dt_db.Rows
                    DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                Next

                For Each dr_data As DataRow In dt_data.Rows
                    sSQL = "INSERT INTO  " _
                        & "  public.reportd_detail_customer_piutang " _
                        & "( " _
                        & "  reportd_oid, " _
                        & "  reportd_report_oid, " _
                        & "  reportd_type, " _
                        & "  reportd_date_generate, " _
                        & "  reportd_ptnr_code, " _
                        & "  reportd_ptnr_name, " _
                        & "  reportd_ar_amount, " _
                        & "  reportd_ar_payment_amount, " _
                        & "  reportd_ar_outstanding_amount, " _
                        & "  reportd_first_duedate, " _
                        & "  reportd_last_duedate " _
                        & ") " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_report_oid) & ",  " _
                        & SetSetring("AR_CUSTOMER-AR CUSTOMER") & ",  " _
                        & SetDateNTime00(CekTanggal()) & ",  " _
                        & SetSetring(dr_data("ptnr_code")) & ",  " _
                        & SetSetring(dr_data("ptnr_name")) & ",  " _
                        & SetDecDB(dr_data("ar_amount")) & ",  " _
                        & SetDecDB(dr_data("ar_payment_amount")) & ",  " _
                        & SetDecDB(dr_data("ar_outstanding_amount")) & ",  " _
                        & SetDateNTime00(dr_data("first_due_date")) & ",  " _
                        & SetDateNTime00(dr_data("last_due_date")) & "  " _
                        & ")"
                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                    Next


                Next

            ElseIf par_type = "AR Aging" Then
                sSQL = "SELECT  " _
                    & "ar_code,c.en_desc,b.ptnr_code,   " _
                    & "b.ptnr_name,  a.ar_due_date, " _
                    & "a.ar_amount  * a.ar_exc_rate AS ar_amount,  " _
                    & "coalesce(a.ar_pay_amount, 0.0) * a.ar_exc_rate AS ar_payment_amount,  " _
                    & "(a.ar_amount - coalesce(a.ar_pay_amount, 0.0)) * a.ar_exc_rate AS ar_outstanding_amount " _
                    & "FROM " _
                    & "  public.ar_mstr a " _
                    & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
                    & "  INNER JOIN public.en_mstr c ON (a.ar_en_id = c.en_id) " _
                    & "WHERE " _
                    & "  a.ar_status IS NULL " _
                    & ""

                Dim dt_data As New DataTable
                dt_data = GetTableData(sSQL)

                sSQL = "delete from reportd_detail_ar_aging where reportd_report_oid=" & SetSetring(_report_oid)

                For Each dr_db As DataRow In dt_db.Rows
                    DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                Next

                For Each dr_data As DataRow In dt_data.Rows
                    sSQL = "INSERT INTO  " _
                        & "  public.reportd_detail_ar_aging " _
                        & "( " _
                        & "  reportd_oid, " _
                        & "  reportd_report_oid, " _
                        & "  reportd_type, " _
                        & "  reportd_date_generate, " _
                        & "  reportd_ptnr_code, " _
                        & "  reportd_ptnr_name, " _
                        & "  reportd_ar_amount, " _
                        & "  reportd_ar_payment_amount, " _
                        & "  reportd_ar_outstanding_amount, " _
                        & "  reportd_duedate, " _
                        & "  reportd_ar_code,reportd_en_desc " _
                        & ") " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_report_oid) & ",  " _
                        & SetSetring("AR_AGING-AR AGING") & ",  " _
                        & SetDateNTime00(CekTanggal()) & ",  " _
                        & SetSetring(dr_data("ptnr_code")) & ",  " _
                        & SetSetring(dr_data("ptnr_name")) & ",  " _
                        & SetDecDB(dr_data("ar_amount")) & ",  " _
                        & SetDecDB(dr_data("ar_payment_amount")) & ",  " _
                        & SetDecDB(dr_data("ar_outstanding_amount")) & ",  " _
                        & SetDateNTime00(dr_data("ar_due_date")) & ",  " _
                        & SetSetring(dr_data("ar_code")) & " , " _
                        & SetSetring(dr_data("en_desc")) & "  " _
                        & ")"
                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                    Next


                Next


            ElseIf par_type = "AR Overview" Then
                sSQL = "SELECT  " _
                    & "'AR_OVERVIEW-AR AMOUNT' as type, sum((a.ar_amount - coalesce(a.ar_pay_amount, 0.0)) * a.ar_exc_rate) AS amount " _
                    & "FROM " _
                    & "  public.ar_mstr a " _
                    & "WHERE " _
                    & "  a.ar_status IS NULL  " _
                    & "union all " _
                    & "SELECT  " _
                    & "'AR_OVERVIEW-AR DUE' as type, sum((a.ar_amount - coalesce(a.ar_pay_amount, 0.0)) * a.ar_exc_rate) AS amount " _
                    & "FROM " _
                    & "  public.ar_mstr a " _
                    & "WHERE " _
                    & "  a.ar_status IS NULL and a.ar_due_date <= current_date " _
                    & "  union all " _
                    & "SELECT  " _
                    & "'AR_OVERVIEW-AR PAYMENT' as type, sum(b.arpayd_amount * b.arpayd_exc_rate) AS amount " _
                    & "FROM " _
                    & "  public.arpay_payment a " _
                    & "  inner join arpayd_det b on a.arpay_oid=b.arpayd_arpay_oid  " _
                    & "WHERE " _
                    & "  to_char(a.arpay_eff_date,'yyyyMM')= to_char(current_date,'yyyyMM')"


                Dim dt_data As New DataTable
                dt_data = GetTableData(sSQL)



                For Each dr_data As DataRow In dt_data.Rows

                    sSQL = "delete from reportd_detail where reportd_type=" & SetSetring(dr_data("type")) & " and reportd_report_oid=" & SetSetring(_report_oid)

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                    Next

                    sSQL = "INSERT INTO  " _
                           & "  public.reportd_detail " _
                           & "( " _
                           & "  reportd_oid, " _
                           & "  reportd_report_oid, " _
                           & "  reportd_type, " _
                           & "  reportd_periode, " _
                           & "  reportd_ac_id, " _
                           & "  reportd_ac_code, " _
                           & "  reportd_ac_hierarchy, " _
                           & "  reportd_ac_name, reportd_date_generate," _
                           & "  reportd_value " _
                           & ") " _
                           & "VALUES ( " _
                           & SetSetring(Guid.NewGuid.ToString) & ",  " _
                           & SetSetring(_report_oid) & ",  " _
                           & SetSetring(dr_data("type")) & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetDateNTime(CekTanggal) & ",  " _
                           & SetDec(SetNumber(dr_data("amount"))) & "  " _
                           & ")"

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))

                    Next

                Next





            ElseIf par_type = "AP Suplier" Then

                sSQL = "SELECT  " _
                    & " b.ptnr_code, b.ptnr_name, sum(a.ap_amount  * a.ap_exc_rate) AS ap_amount, sum(coalesce(a.ap_pay_amount, 0.0) * a.ap_exc_rate) AS ap_payment_amount, sum((a.ap_amount - coalesce(a.ap_pay_amount, 0.0)) * a.ap_exc_rate) AS ap_outstanding_amount, " _
                    & " min(a.ap_due_date) as first_due_date, max(a.ap_due_date) as last_due_date " _
                    & "FROM " _
                    & "  public.ap_mstr a " _
                    & "  INNER JOIN public.ptnr_mstr b ON (a.ap_ptnr_id = b.ptnr_id) " _
                    & "WHERE " _
                    & "  a.ap_status IS NULL  " _
                    & "  GROUP by ptnr_code, ptnr_name"

                Dim dt_data As New DataTable
                dt_data = GetTableData(sSQL)

                sSQL = "delete from reportd_detail_suplier_hutang where reportd_report_oid=" & SetSetring(_report_oid)

                For Each dr_db As DataRow In dt_db.Rows
                    DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                Next

                For Each dr_data As DataRow In dt_data.Rows
                    sSQL = "INSERT INTO  " _
                        & "  public.reportd_detail_suplier_hutang " _
                        & "( " _
                        & "  reportd_oid, " _
                        & "  reportd_report_oid, " _
                        & "  reportd_type, " _
                        & "  reportd_date_generate, " _
                        & "  reportd_ptnr_code, " _
                        & "  reportd_ptnr_name, " _
                        & "  reportd_ap_amount, " _
                        & "  reportd_ap_payment_amount, " _
                        & "  reportd_ap_outstanding_amount, " _
                        & "  reportd_first_duedate, " _
                        & "  reportd_last_duedate " _
                        & ") " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_report_oid) & ",  " _
                        & SetSetring("AP_SUPLIER-AP SUPLIER") & ",  " _
                        & SetDateNTime00(CekTanggal()) & ",  " _
                        & SetSetring(dr_data("ptnr_code")) & ",  " _
                        & SetSetring(dr_data("ptnr_name")) & ",  " _
                        & SetDecDB(dr_data("ap_amount")) & ",  " _
                        & SetDecDB(dr_data("ap_payment_amount")) & ",  " _
                        & SetDecDB(dr_data("ap_outstanding_amount")) & ",  " _
                        & SetDateNTime00(dr_data("first_due_date")) & ",  " _
                        & SetDateNTime00(dr_data("last_due_date")) & "  " _
                        & ")"
                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                    Next


                Next

            ElseIf par_type = "AP Aging" Then
                sSQL = "SELECT  " _
                    & "ap_code,c.en_desc,b.ptnr_code,   " _
                    & "b.ptnr_name,  a.ap_due_date, " _
                    & "a.ap_amount  * a.ap_exc_rate AS ap_amount,  " _
                    & "coalesce(a.ap_pay_amount, 0.0) * a.ap_exc_rate AS ap_payment_amount,  " _
                    & "(a.ap_amount - coalesce(a.ap_pay_amount, 0.0)) * a.ap_exc_rate AS ap_outstanding_amount " _
                    & "FROM " _
                    & "  public.ap_mstr a " _
                    & "  INNER JOIN public.ptnr_mstr b ON (a.ap_ptnr_id = b.ptnr_id) " _
                    & "  INNER JOIN public.en_mstr c ON (a.ap_en_id = c.en_id) " _
                    & "WHERE " _
                    & "  a.ap_status IS NULL " _
                    & ""

                Dim dt_data As New DataTable
                dt_data = GetTableData(sSQL)

                sSQL = "delete from reportd_detail_ap_aging where reportd_report_oid=" & SetSetring(_report_oid)

                For Each dr_db As DataRow In dt_db.Rows
                    DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                Next

                For Each dr_data As DataRow In dt_data.Rows
                    sSQL = "INSERT INTO  " _
                        & "  public.reportd_detail_ap_aging " _
                        & "( " _
                        & "  reportd_oid, " _
                        & "  reportd_report_oid, " _
                        & "  reportd_type, " _
                        & "  reportd_date_generate, " _
                        & "  reportd_ptnr_code, " _
                        & "  reportd_ptnr_name, " _
                        & "  reportd_ap_amount, " _
                        & "  reportd_ap_payment_amount, " _
                        & "  reportd_ap_outstanding_amount, " _
                        & "  reportd_duedate, " _
                        & "  reportd_ap_code,reportd_en_desc " _
                        & ") " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_report_oid) & ",  " _
                        & SetSetring("AP_AGING-AP AGING") & ",  " _
                        & SetDateNTime00(CekTanggal()) & ",  " _
                        & SetSetring(dr_data("ptnr_code")) & ",  " _
                        & SetSetring(dr_data("ptnr_name")) & ",  " _
                        & SetDecDB(dr_data("ap_amount")) & ",  " _
                        & SetDecDB(dr_data("ap_payment_amount")) & ",  " _
                        & SetDecDB(dr_data("ap_outstanding_amount")) & ",  " _
                        & SetDateNTime00(dr_data("ap_due_date")) & ",  " _
                        & SetSetring(dr_data("ap_code")) & " , " _
                        & SetSetring(dr_data("en_desc")) & "  " _
                        & ")"
                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                    Next


                Next


            ElseIf par_type = "AP Overview" Then
                sSQL = "SELECT  " _
                    & "'AP_OVERVIEW-AP AMOUNT' as type, sum((a.ap_amount - coalesce(a.ap_pay_amount, 0.0)) * a.ap_exc_rate) AS amount " _
                    & "FROM " _
                    & "  public.ap_mstr a " _
                    & "WHERE " _
                    & "  a.ap_status IS NULL  " _
                    & "union all " _
                    & "SELECT  " _
                    & "'AP_OVERVIEW-AP DUE' as type, sum((a.ap_amount - coalesce(a.ap_pay_amount, 0.0)) * a.ap_exc_rate) AS amount " _
                    & "FROM " _
                    & "  public.ap_mstr a " _
                    & "WHERE " _
                    & "  a.ap_status IS NULL and a.ap_due_date <= current_date " _
                    & "  union all " _
                    & "SELECT  " _
                    & "'AP_OVERVIEW-AP PAYMENT' as type, sum(b.appayd_amount * b.appayd_exc_rate) AS amount " _
                    & "FROM " _
                    & "  public.appay_payment a " _
                    & "  inner join appayd_det b on a.appay_oid=b.appayd_appay_oid  " _
                    & "WHERE " _
                    & "  to_char(a.appay_eff_date,'yyyyMM')= to_char(current_date,'yyyyMM')"


                Dim dt_data As New DataTable
                dt_data = GetTableData(sSQL)



                For Each dr_data As DataRow In dt_data.Rows

                    sSQL = "delete from reportd_detail where reportd_type=" & SetSetring(dr_data("type")) & " and reportd_report_oid=" & SetSetring(_report_oid)

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                    Next

                    sSQL = "INSERT INTO  " _
                           & "  public.reportd_detail " _
                           & "( " _
                           & "  reportd_oid, " _
                           & "  reportd_report_oid, " _
                           & "  reportd_type, " _
                           & "  reportd_periode, " _
                           & "  reportd_ac_id, " _
                           & "  reportd_ac_code, " _
                           & "  reportd_ac_hierarchy, " _
                           & "  reportd_ac_name,reportd_date_generate, " _
                           & "  reportd_value " _
                           & ") " _
                           & "VALUES ( " _
                           & SetSetring(Guid.NewGuid.ToString) & ",  " _
                           & SetSetring(_report_oid) & ",  " _
                           & SetSetring(dr_data("type")) & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetDateNTime(CekTanggal) & ",  " _
                           & SetDec(SetNumber(dr_data("amount"))) & "  " _
                           & ")"

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))

                    Next

                Next


            ElseIf par_type = "AP" Then

                Dim dt_bs As New DataTable
                Dim dt_ac As New DataTable


                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)
                Dim _last_date As Date
                _last_date = DateAdd(DateInterval.Month, te_month.EditValue, CDate(le_glperiode.GetColumnValue("gcal_start_date")))
                Dim _count_day As Integer
                _count_day = DateDiff(DateInterval.Day, CDate(le_glperiode.GetColumnValue("gcal_start_date")), _last_date)
                Dim _date_current As Date


                sSQL = "select dash_type_id,dash_type_filter from dashboard_setting where dash_type_filter='AP'"
                Dim dt_ap As New DataTable
                dt_ap = GetTableData(sSQL)


                For n As Integer = 0 To _count_day - 1

                    For Each dr_ap As DataRow In dt_ap.Rows
                        _date_current = DateAdd(DateInterval.Day, n, CDate(le_glperiode.GetColumnValue("gcal_start_date")))

                        sSQL = "SELECT  " _
                            & " '" & dr_ap("dash_type_id") & "' as type, sum(((public.glt_det.glt_debit * public.glt_det.glt_exc_rate) - (public.glt_det.glt_credit * public.glt_det.glt_exc_rate)) * -1.0) AS glt_value " _
                            & "FROM " _
                            & "  public.glt_det " _
                            & "WHERE " _
                            & "  public.glt_det.glt_date = " & SetDateNTime00(_date_current) & " AND  " _
                            & "  public.glt_det.glt_ac_id in (SELECT  x.ac_id FROM   public.dashboard_setting a   " _
                            & "INNER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id)  " _
                            & " LEFT OUTER JOIN public.ac_mstr c ON (b.dashd_ac_id = c.ac_id)   " _
                            & "LEFT OUTER JOIN public.ac_mstr x ON (substring(x.ac_code_hirarki, 1, length(c.ac_code_hirarki)) = c.ac_code_hirarki)   " _
                            & "WHERE   a.dash_type_id ='" & dr_ap("dash_type_id") & "' and x.ac_is_sumlevel='N' )"

                        dt_ac = GetTableData(sSQL)

                        For Each dr_ac As DataRow In dt_ac.Rows
                            If SetNumber(dr_ac("glt_value")) <> 0 Then
                                sSQL = "delete from reportd_detail_sub where reportd_report_oid=" & SetSetring(_report_oid) _
                                & " and reportd_type=" & SetSetring(dr_ap("dash_type_id")) & " and reportd_group='" & dr_ap("dash_type_filter") & "' and " _
                                & " reportd_periode= " & SetSetring(Format(_date_current, "yyyyMMdd"))


                                For Each dr_db As DataRow In dt_db.Rows
                                    DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                                Next

                                sSQL = "INSERT INTO  " _
                                    & "  public.reportd_detail_sub " _
                                    & "( " _
                                    & "  reportd_oid, " _
                                    & "  reportd_report_oid, " _
                                    & "  reportd_type, " _
                                    & "  reportd_periode, " _
                                    & "  reportd_ac_id, " _
                                    & "  reportd_ac_code, " _
                                    & "  reportd_ac_hierarchy, " _
                                    & "  reportd_ac_name,reportd_date,reportd_group,reportd_date_generate, " _
                                    & "  reportd_value " _
                                    & ") " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(_report_oid) & ",  " _
                                    & SetSetring(dr_ap("dash_type_id")) & ",  " _
                                    & SetSetring(Format(_date_current, "yyyyMMdd")) & ",  " _
                                    & SetSetring("") & ",  " _
                                    & SetSetring("") & ",  " _
                                    & SetSetring("") & ",  " _
                                    & SetSetring("") & ",  " _
                                    & SetDateNTime00(_date_current) & ", '" & dr_ap("dash_type_filter") & "', " _
                                    & SetDateNTime(CekTanggal) & ",  " _
                                    & SetDec(dr_ac("glt_value")) & "  " _
                                    & ")"

                                For Each dr_db As DataRow In dt_db.Rows
                                    DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                                Next
                            End If

                        Next

                    Next



                    _percent = (n + 1) / _count_day * 100
                    LblStatus.Text = "Processing .. " & par_type & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()

                Next


            ElseIf par_type = "Sales and Purchase" Then

                Dim _date As String
                _date = "to_char(to_date('" & le_glperiode.GetColumnValue("gcal_start_date") & "', 'dd/MM/yyyy'), 'yyyyMM')" '"current_date"



                sSQL = "SELECT      sum(soshipd_qty * -1.0 * sod_price) as sales_ttl, " _
                    & "sum((soshipd_qty * -1.0 * sod_price) - (soshipd_qty * -1.0 * sod_price * sod_disc) ) as sales_nett  " _
                    & "FROM    public.soship_mstr    " _
                    & "inner join soshipd_det on soshipd_soship_oid = soship_oid    " _
                    & "inner join so_mstr on so_oid = soship_so_oid    " _
                    & "inner join pi_mstr on so_pi_id = pi_id    " _
                    & "inner join sod_det on sod_oid = soshipd_sod_oid   " _
                    & "inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person    " _
                    & "inner join en_mstr on en_id = soship_en_id    " _
                    & "inner join si_mstr on si_id = soship_si_id    " _
                    & "inner join ptnr_mstr on ptnr_mstr.ptnr_id = so_ptnr_id_sold    " _
                    & "inner join pt_mstr y on y.pt_id = sod_pt_id    " _
                    & "inner join code_mstr as um_mstr on um_mstr.code_id = soshipd_um    " _
                    & "inner join loc_mstr on loc_id = soshipd_loc_id    " _
                    & "inner join cu_mstr on cu_id = so_cu_id    " _
                    & "inner join code_mstr as tax_mstr on tax_mstr.code_id = sod_tax_class    " _
                    & "inner join code_mstr pay_type on pay_type.code_id = so_pay_type    " _
                    & "left outer join code_mstr as reason_mstr on reason_mstr.code_id = soshipd_rea_code_id    " _
                    & "left outer join ars_ship on ars_soshipd_oid = soshipd_oid    " _
                    & "left outer join ar_mstr on ar_oid = ars_ar_oid    " _
                    & "left outer join tia_ar on tia_ar_oid = ar_oid    " _
                    & "left outer join ti_mstr on ti_oid = tia_ti_oid    " _
                    & "left outer join ptnrg_grp on ptnrg_grp.ptnrg_id = ptnr_mstr.ptnr_ptnrg_id    " _
                    & "inner join pl_mstr on pl_id = pt_pl_id    " _
                    & "left outer join pt_mstr x on so_pt_id = x.pt_id    " _
                    & "where   to_char(soship_date,'yyyyMM')= " & _date


                Dim dt_data As New DataTable
                dt_data = GetTableData(sSQL)

                sSQL = "delete from reportd_detail where reportd_group=" & SetSetring("SALES") & " and reportd_report_oid=" & SetSetring(_report_oid)

                For Each dr_db As DataRow In dt_db.Rows
                    DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                Next

                For Each dr_data As DataRow In dt_data.Rows

                    sSQL = "INSERT INTO  " _
                           & "  public.reportd_detail " _
                           & "( " _
                           & "  reportd_oid, " _
                           & "  reportd_report_oid, " _
                           & "  reportd_type, " _
                           & "  reportd_periode, " _
                           & "  reportd_ac_id, " _
                           & "  reportd_ac_code, " _
                           & "  reportd_ac_hierarchy, " _
                           & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                           & "  reportd_value " _
                           & ") " _
                           & "VALUES ( " _
                           & SetSetring(Guid.NewGuid.ToString) & ",  " _
                           & SetSetring(_report_oid) & ",  " _
                           & SetSetring("SALES-BRUTO") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ", 'SALES', " _
                           & SetDateNTime(CekTanggal) & ",  " _
                           & SetDec(SetNumber(dr_data("sales_ttl"))) & "  " _
                           & ")"

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))

                    Next

                    sSQL = "INSERT INTO  " _
                          & "  public.reportd_detail " _
                          & "( " _
                          & "  reportd_oid, " _
                          & "  reportd_report_oid, " _
                          & "  reportd_type, " _
                          & "  reportd_periode, " _
                          & "  reportd_ac_id, " _
                          & "  reportd_ac_code, " _
                          & "  reportd_ac_hierarchy, " _
                          & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                          & "  reportd_value " _
                          & ") " _
                          & "VALUES ( " _
                          & SetSetring(Guid.NewGuid.ToString) & ",  " _
                          & SetSetring(_report_oid) & ",  " _
                          & SetSetring("SALES-NETTO") & ",  " _
                          & SetSetring("") & ",  " _
                          & SetSetring("") & ",  " _
                          & SetSetring("") & ",  " _
                          & SetSetring("") & ",  " _
                          & SetSetring("") & ", 'SALES', " _
                          & SetDateNTime(CekTanggal) & ",  " _
                          & SetDec(SetNumber(dr_data("sales_nett"))) & "  " _
                          & ")"

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                    Next
                Next


                sSQL = "select distinct ptnr_mstr.ptnr_code,ptnr_mstr.ptnr_name " _
                    & "FROM    public.soship_mstr    " _
                    & "inner join soshipd_det on soshipd_soship_oid = soship_oid    " _
                    & "inner join so_mstr on so_oid = soship_so_oid    " _
                    & "inner join pi_mstr on so_pi_id = pi_id    " _
                    & "inner join sod_det on sod_oid = soshipd_sod_oid   " _
                    & "inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person    " _
                    & "inner join en_mstr on en_id = soship_en_id    " _
                    & "inner join si_mstr on si_id = soship_si_id    " _
                    & "inner join ptnr_mstr on ptnr_mstr.ptnr_id = so_ptnr_id_sold    " _
                    & "inner join pt_mstr y on y.pt_id = sod_pt_id    " _
                    & "inner join code_mstr as um_mstr on um_mstr.code_id = soshipd_um    " _
                    & "inner join loc_mstr on loc_id = soshipd_loc_id    " _
                    & "inner join cu_mstr on cu_id = so_cu_id    " _
                    & "inner join code_mstr as tax_mstr on tax_mstr.code_id = sod_tax_class    " _
                    & "inner join code_mstr pay_type on pay_type.code_id = so_pay_type    " _
                    & "left outer join code_mstr as reason_mstr on reason_mstr.code_id = soshipd_rea_code_id    " _
                    & "left outer join ars_ship on ars_soshipd_oid = soshipd_oid    " _
                    & "left outer join ar_mstr on ar_oid = ars_ar_oid    " _
                    & "left outer join tia_ar on tia_ar_oid = ar_oid    " _
                    & "left outer join ti_mstr on ti_oid = tia_ti_oid    " _
                    & "left outer join ptnrg_grp on ptnrg_grp.ptnrg_id = ptnr_mstr.ptnr_ptnrg_id    " _
                    & "inner join pl_mstr on pl_id = pt_pl_id    " _
                    & "left outer join pt_mstr x on so_pt_id = x.pt_id    " _
                    & "where to_char(soship_date,'yyyyMM')= " & _date


                dt_data = GetTableData(sSQL)

                If dt_data.Rows.Count > 0 Then
                    sSQL = "INSERT INTO  " _
                     & "  public.reportd_detail " _
                     & "( " _
                     & "  reportd_oid, " _
                     & "  reportd_report_oid, " _
                     & "  reportd_type, " _
                     & "  reportd_periode, " _
                     & "  reportd_ac_id, " _
                     & "  reportd_ac_code, " _
                     & "  reportd_ac_hierarchy, " _
                     & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                     & "  reportd_value " _
                     & ") " _
                     & "VALUES ( " _
                     & SetSetring(Guid.NewGuid.ToString) & ",  " _
                     & SetSetring(_report_oid) & ",  " _
                     & SetSetring("SALES-JUMLAH PELANGGAN") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ", 'SALES', " _
                     & SetDateNTime(CekTanggal) & ",  " _
                     & SetDec(SetNumber(dt_data.Rows.Count)) & "  " _
                     & ")"

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))

                    Next
                End If


                sSQL = "select distinct so_code " _
                    & "FROM    public.soship_mstr    " _
                    & "inner join soshipd_det on soshipd_soship_oid = soship_oid    " _
                    & "inner join so_mstr on so_oid = soship_so_oid    " _
                    & "inner join pi_mstr on so_pi_id = pi_id    " _
                    & "inner join sod_det on sod_oid = soshipd_sod_oid   " _
                    & "inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person    " _
                    & "inner join en_mstr on en_id = soship_en_id    " _
                    & "inner join si_mstr on si_id = soship_si_id    " _
                    & "inner join ptnr_mstr on ptnr_mstr.ptnr_id = so_ptnr_id_sold    " _
                    & "inner join pt_mstr y on y.pt_id = sod_pt_id    " _
                    & "inner join code_mstr as um_mstr on um_mstr.code_id = soshipd_um    " _
                    & "inner join loc_mstr on loc_id = soshipd_loc_id    " _
                    & "inner join cu_mstr on cu_id = so_cu_id    " _
                    & "inner join code_mstr as tax_mstr on tax_mstr.code_id = sod_tax_class    " _
                    & "inner join code_mstr pay_type on pay_type.code_id = so_pay_type    " _
                    & "left outer join code_mstr as reason_mstr on reason_mstr.code_id = soshipd_rea_code_id    " _
                    & "left outer join ars_ship on ars_soshipd_oid = soshipd_oid    " _
                    & "left outer join ar_mstr on ar_oid = ars_ar_oid    " _
                    & "left outer join tia_ar on tia_ar_oid = ar_oid    " _
                    & "left outer join ti_mstr on ti_oid = tia_ti_oid    " _
                    & "left outer join ptnrg_grp on ptnrg_grp.ptnrg_id = ptnr_mstr.ptnr_ptnrg_id    " _
                    & "inner join pl_mstr on pl_id = pt_pl_id    " _
                    & "left outer join pt_mstr x on so_pt_id = x.pt_id    " _
                    & "where to_char(soship_date,'yyyyMM')= " & _date


                dt_data = GetTableData(sSQL)

                If dt_data.Rows.Count > 0 Then
                    sSQL = "INSERT INTO  " _
                     & "  public.reportd_detail " _
                     & "( " _
                     & "  reportd_oid, " _
                     & "  reportd_report_oid, " _
                     & "  reportd_type, " _
                     & "  reportd_periode, " _
                     & "  reportd_ac_id, " _
                     & "  reportd_ac_code, " _
                     & "  reportd_ac_hierarchy, " _
                     & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                     & "  reportd_value " _
                     & ") " _
                     & "VALUES ( " _
                     & SetSetring(Guid.NewGuid.ToString) & ",  " _
                     & SetSetring(_report_oid) & ",  " _
                     & SetSetring("SALES-JUMLAH TRANSAKSI") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ", 'SALES', " _
                     & SetDateNTime(CekTanggal) & ",  " _
                     & SetDec(SetNumber(dt_data.Rows.Count)) & "  " _
                     & ")"

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))

                    Next
                End If

                sSQL = "select distinct y.pt_code " _
                  & "FROM    public.soship_mstr    " _
                  & "inner join soshipd_det on soshipd_soship_oid = soship_oid    " _
                  & "inner join so_mstr on so_oid = soship_so_oid    " _
                  & "inner join pi_mstr on so_pi_id = pi_id    " _
                  & "inner join sod_det on sod_oid = soshipd_sod_oid   " _
                  & "inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person    " _
                  & "inner join en_mstr on en_id = soship_en_id    " _
                  & "inner join si_mstr on si_id = soship_si_id    " _
                  & "inner join ptnr_mstr on ptnr_mstr.ptnr_id = so_ptnr_id_sold    " _
                  & "inner join pt_mstr y on y.pt_id = sod_pt_id    " _
                  & "inner join code_mstr as um_mstr on um_mstr.code_id = soshipd_um    " _
                  & "inner join loc_mstr on loc_id = soshipd_loc_id    " _
                  & "inner join cu_mstr on cu_id = so_cu_id    " _
                  & "inner join code_mstr as tax_mstr on tax_mstr.code_id = sod_tax_class    " _
                  & "inner join code_mstr pay_type on pay_type.code_id = so_pay_type    " _
                  & "left outer join code_mstr as reason_mstr on reason_mstr.code_id = soshipd_rea_code_id    " _
                  & "left outer join ars_ship on ars_soshipd_oid = soshipd_oid    " _
                  & "left outer join ar_mstr on ar_oid = ars_ar_oid    " _
                  & "left outer join tia_ar on tia_ar_oid = ar_oid    " _
                  & "left outer join ti_mstr on ti_oid = tia_ti_oid    " _
                  & "left outer join ptnrg_grp on ptnrg_grp.ptnrg_id = ptnr_mstr.ptnr_ptnrg_id    " _
                  & "inner join pl_mstr on pl_id = pt_pl_id    " _
                  & "left outer join pt_mstr x on so_pt_id = x.pt_id    " _
                  & "where to_char(soship_date,'yyyyMM')= " & _date


                dt_data = GetTableData(sSQL)

                If dt_data.Rows.Count > 0 Then
                    sSQL = "INSERT INTO  " _
                     & "  public.reportd_detail " _
                     & "( " _
                     & "  reportd_oid, " _
                     & "  reportd_report_oid, " _
                     & "  reportd_type, " _
                     & "  reportd_periode, " _
                     & "  reportd_ac_id, " _
                     & "  reportd_ac_code, " _
                     & "  reportd_ac_hierarchy, " _
                     & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                     & "  reportd_value " _
                     & ") " _
                     & "VALUES ( " _
                     & SetSetring(Guid.NewGuid.ToString) & ",  " _
                     & SetSetring(_report_oid) & ",  " _
                     & SetSetring("SALES-PRODUK TERJUAL") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ", 'SALES', " _
                     & SetDateNTime(CekTanggal) & ",  " _
                     & SetDec(SetNumber(dt_data.Rows.Count)) & "  " _
                     & ")"

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))

                    Next
                End If



                sSQL = "SELECT  " _
                        & "  sum(public.rcvd_det.rcvd_qty * public.pod_det.pod_cost) AS purchase_ttl, " _
                        & "  sum((public.rcvd_det.rcvd_qty * public.pod_det.pod_cost) - (public.rcvd_det.rcvd_qty * public.pod_det.pod_cost * public.pod_det.pod_disc)) AS purchase_net " _
                        & "FROM " _
                        & "  public.rcv_mstr " _
                        & "  INNER JOIN public.rcvd_det ON (public.rcv_mstr.rcv_oid = public.rcvd_det.rcvd_rcv_oid) " _
                        & "  INNER JOIN public.po_mstr ON (public.rcv_mstr.rcv_po_oid = public.po_mstr.po_oid) " _
                        & "  INNER JOIN public.pod_det ON (public.rcvd_det.rcvd_pod_oid = public.pod_det.pod_oid) " _
                        & "WHERE " _
                        & "  to_char(public.rcv_mstr.rcv_eff_date, 'yyyyMM') = " & _date


                dt_data = GetTableData(sSQL)

                sSQL = "delete from reportd_detail where reportd_group=" & SetSetring("PURCHASE") & " and reportd_report_oid=" & SetSetring(_report_oid)

                For Each dr_db As DataRow In dt_db.Rows
                    DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                Next


                For Each dr_data As DataRow In dt_data.Rows

                    sSQL = "INSERT INTO  " _
                           & "  public.reportd_detail " _
                           & "( " _
                           & "  reportd_oid, " _
                           & "  reportd_report_oid, " _
                           & "  reportd_type, " _
                           & "  reportd_periode, " _
                           & "  reportd_ac_id, " _
                           & "  reportd_ac_code, " _
                           & "  reportd_ac_hierarchy, " _
                           & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                           & "  reportd_value " _
                           & ") " _
                           & "VALUES ( " _
                           & SetSetring(Guid.NewGuid.ToString) & ",  " _
                           & SetSetring(_report_oid) & ",  " _
                           & SetSetring("PURCHASE-BRUTO") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ",  " _
                           & SetSetring("") & ", 'PURCHASE', " _
                           & SetDateNTime(CekTanggal) & ",  " _
                           & SetDec(SetNumber(dr_data("purchase_ttl"))) & "  " _
                           & ")"

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))

                    Next

                    sSQL = "INSERT INTO  " _
                          & "  public.reportd_detail " _
                          & "( " _
                          & "  reportd_oid, " _
                          & "  reportd_report_oid, " _
                          & "  reportd_type, " _
                          & "  reportd_periode, " _
                          & "  reportd_ac_id, " _
                          & "  reportd_ac_code, " _
                          & "  reportd_ac_hierarchy, " _
                          & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                          & "  reportd_value " _
                          & ") " _
                          & "VALUES ( " _
                          & SetSetring(Guid.NewGuid.ToString) & ",  " _
                          & SetSetring(_report_oid) & ",  " _
                          & SetSetring("PURCHASE-NETTO") & ",  " _
                          & SetSetring("") & ",  " _
                          & SetSetring("") & ",  " _
                          & SetSetring("") & ",  " _
                          & SetSetring("") & ",  " _
                          & SetSetring("") & ", 'PURCHASE', " _
                          & SetDateNTime(CekTanggal) & ",  " _
                          & SetDec(SetNumber(dr_data("purchase_net"))) & "  " _
                          & ")"

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                    Next
                Next


                sSQL = "SELECT  " _
                        & "  distinct po_ptnr_id " _
                        & "FROM " _
                        & "  public.rcv_mstr " _
                        & "  INNER JOIN public.rcvd_det ON (public.rcv_mstr.rcv_oid = public.rcvd_det.rcvd_rcv_oid) " _
                        & "  INNER JOIN public.po_mstr ON (public.rcv_mstr.rcv_po_oid = public.po_mstr.po_oid) " _
                        & "  INNER JOIN public.pod_det ON (public.rcvd_det.rcvd_pod_oid = public.pod_det.pod_oid) " _
                        & "WHERE " _
                        & "  to_char(public.rcv_mstr.rcv_eff_date, 'yyyyMM') = " & _date


                dt_data = GetTableData(sSQL)

                If dt_data.Rows.Count > 0 Then
                    sSQL = "INSERT INTO  " _
                     & "  public.reportd_detail " _
                     & "( " _
                     & "  reportd_oid, " _
                     & "  reportd_report_oid, " _
                     & "  reportd_type, " _
                     & "  reportd_periode, " _
                     & "  reportd_ac_id, " _
                     & "  reportd_ac_code, " _
                     & "  reportd_ac_hierarchy, " _
                     & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                     & "  reportd_value " _
                     & ") " _
                     & "VALUES ( " _
                     & SetSetring(Guid.NewGuid.ToString) & ",  " _
                     & SetSetring(_report_oid) & ",  " _
                     & SetSetring("PURCHASE-JUMLAH PELANGGAN") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ", 'PURCHASE', " _
                     & SetDateNTime(CekTanggal) & ",  " _
                     & SetDec(SetNumber(dt_data.Rows.Count)) & "  " _
                     & ")"

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))

                    Next
                End If




                sSQL = "SELECT  " _
                       & "  distinct po_code " _
                       & "FROM " _
                       & "  public.rcv_mstr " _
                       & "  INNER JOIN public.rcvd_det ON (public.rcv_mstr.rcv_oid = public.rcvd_det.rcvd_rcv_oid) " _
                       & "  INNER JOIN public.po_mstr ON (public.rcv_mstr.rcv_po_oid = public.po_mstr.po_oid) " _
                       & "  INNER JOIN public.pod_det ON (public.rcvd_det.rcvd_pod_oid = public.pod_det.pod_oid) " _
                       & "WHERE " _
                       & "  to_char(public.rcv_mstr.rcv_eff_date, 'yyyyMM') = " & _date


                dt_data = GetTableData(sSQL)

                If dt_data.Rows.Count > 0 Then
                    sSQL = "INSERT INTO  " _
                     & "  public.reportd_detail " _
                     & "( " _
                     & "  reportd_oid, " _
                     & "  reportd_report_oid, " _
                     & "  reportd_type, " _
                     & "  reportd_periode, " _
                     & "  reportd_ac_id, " _
                     & "  reportd_ac_code, " _
                     & "  reportd_ac_hierarchy, " _
                     & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                     & "  reportd_value " _
                     & ") " _
                     & "VALUES ( " _
                     & SetSetring(Guid.NewGuid.ToString) & ",  " _
                     & SetSetring(_report_oid) & ",  " _
                     & SetSetring("PURCHASE-JUMLAH TRANSAKSI") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ", 'PURCHASE', " _
                     & SetDateNTime(CekTanggal) & ",  " _
                     & SetDec(SetNumber(dt_data.Rows.Count)) & "  " _
                     & ")"

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))

                    Next
                End If


                sSQL = "SELECT  " _
                      & "  distinct pod_pt_id " _
                      & "FROM " _
                      & "  public.rcv_mstr " _
                      & "  INNER JOIN public.rcvd_det ON (public.rcv_mstr.rcv_oid = public.rcvd_det.rcvd_rcv_oid) " _
                      & "  INNER JOIN public.po_mstr ON (public.rcv_mstr.rcv_po_oid = public.po_mstr.po_oid) " _
                      & "  INNER JOIN public.pod_det ON (public.rcvd_det.rcvd_pod_oid = public.pod_det.pod_oid) " _
                      & "WHERE " _
                      & "  to_char(public.rcv_mstr.rcv_eff_date, 'yyyyMM') = " & _date ' to_char(to_date('1/1/2021', 'dd/MM/yyyy'), 'yyyyMM')


                dt_data = GetTableData(sSQL)

                If dt_data.Rows.Count > 0 Then
                    sSQL = "INSERT INTO  " _
                     & "  public.reportd_detail " _
                     & "( " _
                     & "  reportd_oid, " _
                     & "  reportd_report_oid, " _
                     & "  reportd_type, " _
                     & "  reportd_periode, " _
                     & "  reportd_ac_id, " _
                     & "  reportd_ac_code, " _
                     & "  reportd_ac_hierarchy, " _
                     & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                     & "  reportd_value " _
                     & ") " _
                     & "VALUES ( " _
                     & SetSetring(Guid.NewGuid.ToString) & ",  " _
                     & SetSetring(_report_oid) & ",  " _
                     & SetSetring("PURCHASE-PRODUK TERBELI") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("") & ", 'PURCHASE', " _
                     & SetDateNTime(CekTanggal) & ",  " _
                     & SetDec(SetNumber(dt_data.Rows.Count)) & "  " _
                     & ")"

                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))

                    Next
                End If


            ElseIf par_type = "Sales Chart" Then

                Dim dt_ac As New DataTable
                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)
                For Each dr_gl_cal As DataRow In dt_gl_cal.Rows


                    sSQL = "SELECT      sum(soshipd_qty * -1.0 * sod_price) as sales_ttl, " _
                    & "sum((soshipd_qty * -1.0 * sod_price) - (soshipd_qty * -1.0 * sod_price * sod_disc) ) as sales_nett  " _
                    & "FROM    public.soship_mstr    " _
                    & "inner join soshipd_det on soshipd_soship_oid = soship_oid    " _
                    & "inner join so_mstr on so_oid = soship_so_oid    " _
                    & "inner join pi_mstr on so_pi_id = pi_id    " _
                    & "inner join sod_det on sod_oid = soshipd_sod_oid   " _
                    & "inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person    " _
                    & "inner join en_mstr on en_id = soship_en_id    " _
                    & "inner join si_mstr on si_id = soship_si_id    " _
                    & "inner join ptnr_mstr on ptnr_mstr.ptnr_id = so_ptnr_id_sold    " _
                    & "inner join pt_mstr y on y.pt_id = sod_pt_id    " _
                    & "inner join code_mstr as um_mstr on um_mstr.code_id = soshipd_um    " _
                    & "inner join loc_mstr on loc_id = soshipd_loc_id    " _
                    & "inner join cu_mstr on cu_id = so_cu_id    " _
                    & "inner join code_mstr as tax_mstr on tax_mstr.code_id = sod_tax_class    " _
                    & "inner join code_mstr pay_type on pay_type.code_id = so_pay_type    " _
                    & "left outer join code_mstr as reason_mstr on reason_mstr.code_id = soshipd_rea_code_id    " _
                    & "left outer join ars_ship on ars_soshipd_oid = soshipd_oid    " _
                    & "left outer join ar_mstr on ar_oid = ars_ar_oid    " _
                    & "left outer join tia_ar on tia_ar_oid = ar_oid    " _
                    & "left outer join ti_mstr on ti_oid = tia_ti_oid    " _
                    & "left outer join ptnrg_grp on ptnrg_grp.ptnrg_id = ptnr_mstr.ptnr_ptnrg_id    " _
                    & "inner join pl_mstr on pl_id = pt_pl_id    " _
                    & "left outer join pt_mstr x on so_pt_id = x.pt_id    " _
                    & "where   to_char(soship_date,'yyyyMM')= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ""

                    dt_ac = GetTableData(sSQL)
                    For Each dr_ac As DataRow In dt_ac.Rows
                        If SetNumber(dr_ac("sales_ttl")) <> 0 Or SetNumber(dr_ac("sales_nett")) <> 0 Then
                            sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                              & " and reportd_type=" & SetSetring("SALES_CHART-BRUTO") & "  and reportd_group='SALES_CHART' and " _
                              & " reportd_periode= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM"))


                            For Each dr_db As DataRow In dt_db.Rows
                                DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                            Next

                            sSQL = "INSERT INTO  " _
                                & "  public.reportd_detail " _
                                & "( " _
                                & "  reportd_oid, " _
                                & "  reportd_report_oid, " _
                                & "  reportd_type, " _
                                & "  reportd_periode, " _
                                & "  reportd_ac_id, " _
                                & "  reportd_ac_code, " _
                                & "  reportd_ac_hierarchy, " _
                                & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                                & "  reportd_value " _
                                & ") " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(_report_oid) & ",  " _
                                & SetSetring("SALES_CHART-BRUTO") & ",  " _
                                & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & "'SALES_CHART', " _
                                & SetDateNTime(CekTanggal) & ",  " _
                                & SetDec(dr_ac("sales_ttl")) & "  " _
                                & ")"

                            For Each dr_db As DataRow In dt_db.Rows
                                DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                            Next


                            sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                              & " and reportd_type=" & SetSetring("SALES_CHART-NETTO") & "  and reportd_group='SALES_CHART' and " _
                              & " reportd_periode= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM"))


                            For Each dr_db As DataRow In dt_db.Rows
                                DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                            Next

                            sSQL = "INSERT INTO  " _
                                & "  public.reportd_detail " _
                                & "( " _
                                & "  reportd_oid, " _
                                & "  reportd_report_oid, " _
                                & "  reportd_type, " _
                                & "  reportd_periode, " _
                                & "  reportd_ac_id, " _
                                & "  reportd_ac_code, " _
                                & "  reportd_ac_hierarchy, " _
                                & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                                & "  reportd_value " _
                                & ") " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(_report_oid) & ",  " _
                                & SetSetring("SALES_CHART-NETTO") & ",  " _
                                & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & "'SALES_CHART', " _
                                & SetDateNTime(CekTanggal) & ",  " _
                                & SetDec(dr_ac("sales_nett")) & "  " _
                                & ")"

                            For Each dr_db As DataRow In dt_db.Rows
                                DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                            Next
                        End If
                    Next
                    _processed = _processed + 1.0
                    _percent = _processed / _count * 100
                    LblStatus.Text = "Processing .. " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()
                Next

            ElseIf par_type = "Expense Chart" Then
                Dim dt_bs As New DataTable
                Dim dt_ac As New DataTable
                Dim dt_setting As New DataTable

                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)
                Dim _last_date As Date
                _last_date = DateAdd(DateInterval.Month, te_month.EditValue, CDate(le_glperiode.GetColumnValue("gcal_start_date")))
                Dim _count_day As Integer
                _count_day = DateDiff(DateInterval.Day, CDate(le_glperiode.GetColumnValue("gcal_start_date")), _last_date)
                Dim _date_current As Date


                sSQL = "SELECT  " _
                   & "  a.dash_type_id, " _
                   & "  a.dash_type_number, " _
                   & "  a.dash_type_filter, " _
                   & "  b.dashd_oid, " _
                   & "  b.dashd_dash_type_id, " _
                   & "  b.dashd_ac_id, " _
                   & "  public.ac_mstr.ac_code, " _
                   & "  public.ac_mstr.ac_code_hirarki, " _
                   & "  public.ac_mstr.ac_name,0.0 as value " _
                   & "FROM " _
                   & "  public.dashboard_setting a " _
                   & "  LEFT OUTER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id) " _
                   & "  LEFT OUTER JOIN public.ac_mstr ON (b.dashd_ac_id = public.ac_mstr.ac_id) " _
                   & "WHERE " _
                   & "  a.dash_type_filter = 'EXPENSE_CHART' " _
                   & "ORDER BY " _
                   & "  a.dash_type_filter, " _
                   & "  a.dash_type_number"

                dt_setting = GetTableData(sSQL)

                For Each dr_gl_cal As DataRow In dt_gl_cal.Rows

                    For Each dr_setting As DataRow In dt_setting.Rows


                        sSQL = "SELECT  " _
                          & " '" & dr_setting("dash_type_id") & "' as type, sum( f_get_balance_sheet_pl(z.ac_id,1,1,0,cast('" & dr_gl_cal("gcal_oid").ToString & "' as uuid),'" & dr_gl_cal("gcal_closing") & "')) AS glt_value " _
                          & "FROM " _
                          & "  public.ac_mstr z " _
                          & "WHERE " _
                          & "  z.ac_id in (SELECT  x.ac_id FROM   public.dashboard_setting a   " _
                          & "INNER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id)  " _
                          & " LEFT OUTER JOIN public.ac_mstr c ON (b.dashd_ac_id = c.ac_id)   " _
                          & "LEFT OUTER JOIN public.ac_mstr x ON (substring(x.ac_code_hirarki, 1, length(c.ac_code_hirarki)) = c.ac_code_hirarki)   " _
                          & "WHERE   a.dash_type_id ='" & dr_setting("dash_type_id") & "' and x.ac_is_sumlevel='N' )"


                        dt_ac = GetTableData(sSQL)



                        sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                                  & " and reportd_type=" & SetSetring(dr_setting("dash_type_id")) & "  and reportd_group='EXPENSE_CHART' and " _
                                  & " reportd_periode= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM"))


                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next

                        For Each dr_ac As DataRow In dt_ac.Rows
                            If SetNumber(dr_ac("glt_value")) <> 0 Then


                                sSQL = "INSERT INTO  " _
                                    & "  public.reportd_detail " _
                                    & "( " _
                                    & "  reportd_oid, " _
                                    & "  reportd_report_oid, " _
                                    & "  reportd_type, " _
                                    & "  reportd_periode, " _
                                    & "  reportd_ac_id, " _
                                    & "  reportd_ac_code, " _
                                    & "  reportd_ac_hierarchy, " _
                                    & "  reportd_ac_name,reportd_group,reportd_date_generate, " _
                                    & "  reportd_value " _
                                    & ") " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(_report_oid) & ",  " _
                                    & SetSetring(dr_setting("dash_type_id")) & ",  " _
                                    & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                                    & SetSetring("") & ",  " _
                                    & SetSetring("") & ",  " _
                                    & SetSetring("") & ",  " _
                                    & SetSetring("") & ",  " _
                                    & "'EXPENSE_CHART', " _
                                    & SetDateNTime(CekTanggal) & ",  " _
                                    & SetDec(dr_ac("glt_value")) & "  " _
                                    & ")"

                                For Each dr_db As DataRow In dt_db.Rows
                                    DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                                Next



                            End If
                        Next
                    Next


                    _processed = _processed + 1.0
                    _percent = _processed / _count * 100
                    LblStatus.Text = "Processing .. " & par_type & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()

                Next
                For n As Integer = 0 To _count_day - 1
                    _date_current = DateAdd(DateInterval.Day, n, CDate(le_glperiode.GetColumnValue("gcal_start_date")))

                    For Each dr_setting As DataRow In dt_setting.Rows

                        sSQL = "SELECT  " _
                        & " '" & dr_setting("dash_type_id") & "' as type, sum(((public.glt_det.glt_debit * public.glt_det.glt_exc_rate) - (public.glt_det.glt_credit * public.glt_det.glt_exc_rate)) ) AS glt_value " _
                        & "FROM " _
                        & "  public.glt_det " _
                        & "WHERE " _
                        & "  public.glt_det.glt_date = " & SetDateNTime00(_date_current) & " AND  " _
                        & "  public.glt_det.glt_ac_id in (SELECT  x.ac_id FROM   public.dashboard_setting a   " _
                        & "INNER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id)  " _
                        & " LEFT OUTER JOIN public.ac_mstr c ON (b.dashd_ac_id = c.ac_id)   " _
                        & "LEFT OUTER JOIN public.ac_mstr x ON (substring(x.ac_code_hirarki, 1, length(c.ac_code_hirarki)) = c.ac_code_hirarki)   " _
                        & "WHERE   a.dash_type_id ='" & dr_setting("dash_type_id") & "' and x.ac_is_sumlevel='N' )"

                        dt_ac = GetTableData(sSQL)



                        sSQL = "delete from reportd_detail_sub where reportd_report_oid=" & SetSetring(_report_oid) _
                                   & " and reportd_type=" & SetSetring(dr_setting("dash_type_id")) & " and reportd_group='" & dr_setting("dash_type_filter") & "' and " _
                                   & " reportd_periode= " & SetSetring(Format(_date_current, "yyyyMMdd"))


                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next

                        For Each dr_ac As DataRow In dt_ac.Rows

                            If SetNumber(dr_ac("glt_value")) <> 0 Then




                                sSQL = "INSERT INTO  " _
                                    & "  public.reportd_detail_sub " _
                                    & "( " _
                                    & "  reportd_oid, " _
                                    & "  reportd_report_oid, " _
                                    & "  reportd_type, " _
                                    & "  reportd_periode, " _
                                    & "  reportd_ac_id, " _
                                    & "  reportd_ac_code, " _
                                    & "  reportd_ac_hierarchy, " _
                                    & "  reportd_ac_name,reportd_date,reportd_group, reportd_date_generate," _
                                    & "  reportd_value " _
                                    & ") " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(_report_oid) & ",  " _
                                    & SetSetring(dr_ac("type")) & ",  " _
                                    & SetSetring(Format(_date_current, "yyyyMMdd")) & ",  " _
                                    & SetSetring("") & ",  " _
                                    & SetSetring("") & ",  " _
                                    & SetSetring("") & ",  " _
                                    & SetSetring("") & ",  " _
                                    & SetDateNTime00(_date_current) & ", 'EXPENSE_CHART', " _
                                    & SetDateNTime(CekTanggal) & ",  " _
                                    & SetDec(dr_ac("glt_value")) & "  " _
                                    & ")"

                                For Each dr_db As DataRow In dt_db.Rows
                                    DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                                Next
                            End If

                        Next
                    Next

                    _percent = (n + 1) / _count_day * 100
                    LblStatus.Text = "Processing .. " & par_type & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()

                Next


            ElseIf par_type = "Penjualan Terhutang Tagihan Belum Dibayar" Then
                Dim dt_bs As New DataTable
                Dim dt_ac As New DataTable


                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)
                Dim _last_date As Date
                _last_date = DateAdd(DateInterval.Month, te_month.EditValue, CDate(le_glperiode.GetColumnValue("gcal_start_date")))
                Dim _count_day As Integer
                _count_day = DateDiff(DateInterval.Day, CDate(le_glperiode.GetColumnValue("gcal_start_date")), _last_date)
                Dim _date_current As Date


                For n As Integer = 0 To _count_day - 1


                    _date_current = DateAdd(DateInterval.Day, n, CDate(le_glperiode.GetColumnValue("gcal_start_date")))


                    sSQL = "SELECT  " _
                    & "'SALES_DUE-TERHUTANG' as type, sum((a.ar_amount - coalesce(a.ar_pay_amount, 0.0)) * a.ar_exc_rate) AS glt_value " _
                    & "FROM " _
                    & "  public.ar_mstr a " _
                    & "WHERE " _
                    & "  a.ar_status IS NULL and a.ar_eff_date= " & SetDateNTime00(_date_current) & " " _
                    & " union all SELECT  " _
                    & "'SALES_DUE-BELUM DIBAYAR' as type, sum((a.ar_amount - coalesce(a.ar_pay_amount, 0.0)) * a.ar_exc_rate) AS glt_value " _
                    & "FROM " _
                    & "  public.ar_mstr a " _
                    & "WHERE " _
                    & "  a.ar_status IS NULL  and a.ar_due_date= " & SetDateNTime00(_date_current) & " "

                    dt_ac = GetTableData(sSQL)

                    sSQL = "delete from reportd_detail_sub where reportd_report_oid=" & SetSetring(_report_oid) _
                            & " and  reportd_group='SALES_DUE' and " _
                            & " reportd_periode= " & SetSetring(Format(_date_current, "yyyyMMdd"))


                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                    Next

                    For Each dr_ac As DataRow In dt_ac.Rows

                        If SetNumber(dr_ac("glt_value")) <> 0 Then


                            sSQL = "INSERT INTO  " _
                                & "  public.reportd_detail_sub " _
                                & "( " _
                                & "  reportd_oid, " _
                                & "  reportd_report_oid, " _
                                & "  reportd_type, " _
                                & "  reportd_periode, " _
                                & "  reportd_ac_id, " _
                                & "  reportd_ac_code, " _
                                & "  reportd_ac_hierarchy, " _
                                & "  reportd_ac_name,reportd_date,reportd_group, reportd_date_generate," _
                                & "  reportd_value " _
                                & ") " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(_report_oid) & ",  " _
                                & SetSetring(dr_ac("type")) & ",  " _
                                & SetSetring(Format(_date_current, "yyyyMMdd")) & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetDateNTime00(_date_current) & ", 'SALES_DUE', " _
                                & SetDateNTime(CekTanggal) & ",  " _
                                & SetDec(dr_ac("glt_value")) & "  " _
                                & ")"

                            For Each dr_db As DataRow In dt_db.Rows
                                DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                            Next
                        End If

                    Next



                    _percent = (n + 1) / _count_day * 100
                    LblStatus.Text = "Processing .. " & par_type & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()

                Next




                '======

            ElseIf par_type = "Expense Overview" Then

                sSQL = "SELECT  " _
                   & "  a.gcal_oid, " _
                   & "  a.gcal_dom_id, " _
                   & "  a.gcal_add_by, " _
                   & "  a.gcal_add_date, " _
                   & "  a.gcal_upd_by, " _
                   & "  a.gcal_upd_date, " _
                   & "  a.gcal_year, " _
                   & "  a.gcal_periode, " _
                   & "  a.gcal_start_date, " _
                   & "  a.gcal_end_date, " _
                   & "  a.gcal_dt, " _
                   & "  a.gcal_pra_closing, " _
                   & "  a.gcal_closing " _
                   & "FROM " _
                   & "  public.gcal_mstr a " _
                   & "where  " _
                   & "gcal_start_date = (select gcal_start_date from gcal_mstr where gcal_oid='" & le_glperiode.EditValue & "') " _
                   & "order by gcal_start_date "


                'Dim dt_gl_cal As New DataTable
                dt_gl_cal = GetTableData(sSQL)

                sSQL = "SELECT  " _
                     & "  a.dash_type_id, " _
                     & "  a.dash_type_number, " _
                     & "  a.dash_type_filter, " _
                     & "  b.dashd_oid, " _
                     & "  b.dashd_dash_type_id, " _
                     & "  b.dashd_ac_id, " _
                     & "  public.ac_mstr.ac_code, " _
                     & "  public.ac_mstr.ac_code_hirarki, " _
                     & "  public.ac_mstr.ac_name,0.0 as value " _
                     & "FROM " _
                     & "  public.dashboard_setting a " _
                     & "  LEFT OUTER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id) " _
                     & "  LEFT OUTER JOIN public.ac_mstr ON (b.dashd_ac_id = public.ac_mstr.ac_id) " _
                     & "WHERE " _
                     & "  a.dash_type_filter = 'EXPENSE' " _
                     & "ORDER BY " _
                     & "  a.dash_type_filter, " _
                     & "  a.dash_type_number"

                Dim dt_setting As New DataTable
                dt_setting = GetTableData(sSQL)


                Dim dt_bs As New DataTable

                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)

                For Each dr_gl_cal As DataRow In dt_gl_cal.Rows

                    sSQL = "select dash_type_id,sum(v_nilai) as value from ( " _
                        & "SELECT  " _
                        & "  a.dash_type_id, " _
                        & "  a.dash_type_number, " _
                        & "  a.dash_type_filter, " _
                        & "  b.dashd_oid, " _
                        & "  b.dashd_dash_type_id, " _
                        & "  b.dashd_ac_id, " _
                        & "  c.ac_code, " _
                        & "  c.ac_code_hirarki, " _
                        & "  c.ac_name,(select  sum(v_nilai) as jml from   " _
                        & "  ( SELECT    x.ac_id,   x.ac_code_hirarki,   x.ac_code,   x.ac_name,     " _
                        & "  x.ac_type,f_get_balance_sheet_pl(x.ac_id,1,1,0, " _
                        & "  cast('" & dr_gl_cal("gcal_oid").ToString & "' as uuid),'" & dr_gl_cal("gcal_closing") & "') as v_nilai   " _
                        & "  FROM   public.ac_mstr x WHERE    " _
                        & "  substring(x.ac_code_hirarki, 1, length(c.ac_code_hirarki)) = c.ac_code_hirarki AND      " _
                        & "  ac_is_sumlevel = 'N') as temp) * 1.0 as v_nilai  " _
                        & "FROM " _
                        & "  public.dashboard_setting a " _
                        & "  INNER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id) " _
                        & "  LEFT OUTER JOIN public.ac_mstr c ON (b.dashd_ac_id = c.ac_id) " _
                        & "WHERE " _
                        & "  a.dash_type_filter = 'EXPENSE' " _
                        & "ORDER BY " _
                        & "  a.dash_type_filter, " _
                        & "  a.dash_type_number) as tp group by dash_type_id"


                    dt_bs = GetTableData(sSQL)
                    Dim _penjualan, _hpp, _biaya_op, _laba_kotor, _laba_operasi, _laba_sebelum_pajak, _pendapatan_biaya_lain As Double
                    _penjualan = 0.0
                    _hpp = 0.0
                    _biaya_op = 0.0

                    _pendapatan_biaya_lain = 0.0

                    For Each dr_bs As DataRow In dt_bs.Rows
                        For Each dr_setting As DataRow In dt_setting.Rows
                            If dr_bs("dash_type_id") = dr_setting("dash_type_id") Then
                                dr_setting("value") = dr_setting("value") + dr_bs("value")
                            End If
                        Next
                    Next

                    dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows

                        sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                            & " and reportd_type=" & SetSetring(dr_setting("dash_type_id")) & " "


                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next

                        sSQL = "INSERT INTO  " _
                            & "  public.reportd_detail " _
                            & "( " _
                            & "  reportd_oid, " _
                            & "  reportd_report_oid, " _
                            & "  reportd_type, " _
                            & "  reportd_periode, " _
                            & "  reportd_ac_id, " _
                            & "  reportd_ac_code, " _
                            & "  reportd_ac_hierarchy, " _
                            & "  reportd_ac_name,reportd_date_generate,reportd_group, " _
                            & "  reportd_value " _
                            & ") " _
                            & "VALUES ( " _
                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                            & SetSetring(_report_oid) & ",  " _
                            & SetSetring(dr_setting("dash_type_id")) & ",  " _
                            & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetDateNTime(CekTanggal) & ", 'EXPENSE', " _
                            & SetDec(dr_setting("value")) & "  " _
                            & ")"

                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next
                    Next


                    For Each dr_setting As DataRow In dt_setting.Rows
                        dr_setting("value") = 0.0
                    Next
                    dt_setting.AcceptChanges()

                    '_processed = _processed + 1.0
                    '_percent = _processed / _count * 100
                    'LblStatus.Text = "Processing .. " & Math.Round(_percent, 2) & " %"
                    'System.Windows.Forms.Application.DoEvents()
                Next


                '====


            ElseIf par_type = "Cash Detail" Then

                Dim dt_bs As New DataTable
                Dim dt_ac As New DataTable


                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)
                Dim _last_date As Date
                _last_date = DateAdd(DateInterval.Month, te_month.EditValue, CDate(le_glperiode.GetColumnValue("gcal_start_date")))
                Dim _count_day As Integer
                _count_day = DateDiff(DateInterval.Day, CDate(le_glperiode.GetColumnValue("gcal_start_date")), _last_date)
                Dim _date_current As Date
                Dim _selisih As Double = 0.0


                For n As Integer = 0 To _count_day - 1

                    _date_current = DateAdd(DateInterval.Day, n, CDate(le_glperiode.GetColumnValue("gcal_start_date")))

                    sSQL = "SELECT  " _
                        & " 'CASH_IN_OUT-CASH IN' as type, sum((public.glt_det.glt_debit * public.glt_det.glt_exc_rate)) AS glt_value " _
                        & "FROM " _
                        & "  public.glt_det " _
                        & "WHERE " _
                        & " public.glt_det.glt_date = " & SetDateNTime00(_date_current) & " AND  " _
                        & "  public.glt_det.glt_ac_id in (SELECT  x.ac_id FROM   public.dashboard_setting a   " _
                        & "INNER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id)  " _
                        & " LEFT OUTER JOIN public.ac_mstr c ON (b.dashd_ac_id = c.ac_id)   " _
                        & "LEFT OUTER JOIN public.ac_mstr x ON (substring(x.ac_code_hirarki, 1, length(c.ac_code_hirarki)) = c.ac_code_hirarki)   " _
                        & "WHERE   a.dash_type_id in ('ACCOUNT-KAS','ACCOUNT-BANK') and x.ac_is_sumlevel='N' ) union all " _
                        & "SELECT  " _
                        & " 'CASH_IN_OUT-CASH OUT' as type, sum((public.glt_det.glt_credit * public.glt_det.glt_exc_rate)) AS glt_value " _
                        & "FROM " _
                        & "  public.glt_det " _
                        & "WHERE " _
                        & "  public.glt_det.glt_date= " & SetDateNTime00(_date_current) & " AND  " _
                        & "  public.glt_det.glt_ac_id in (SELECT  x.ac_id FROM   public.dashboard_setting a   " _
                        & "INNER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id)  " _
                        & " LEFT OUTER JOIN public.ac_mstr c ON (b.dashd_ac_id = c.ac_id)   " _
                        & "LEFT OUTER JOIN public.ac_mstr x ON (substring(x.ac_code_hirarki, 1, length(c.ac_code_hirarki)) = c.ac_code_hirarki)   " _
                        & "WHERE   a.dash_type_id in ('ACCOUNT-KAS','ACCOUNT-BANK') and x.ac_is_sumlevel='N' )"


                    dt_ac = GetTableData(sSQL)

                    sSQL = "delete from reportd_detail_sub where reportd_report_oid=" & SetSetring(_report_oid) _
                         & "  and reportd_group='CASH_IN_OUT' and " _
                         & " reportd_periode= " & SetSetring(Format(_date_current, "yyyyMMdd"))


                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                    Next

                    _selisih = 0.0

                    For Each dr_ac As DataRow In dt_ac.Rows
                        If SetNumber(dr_ac("glt_value")) <> 0 Then


                            sSQL = "INSERT INTO  " _
                             & "  public.reportd_detail_sub " _
                             & "( " _
                             & "  reportd_oid, " _
                             & "  reportd_report_oid, " _
                             & "  reportd_type, " _
                             & "  reportd_periode, " _
                             & "  reportd_ac_id, " _
                             & "  reportd_ac_code, " _
                             & "  reportd_ac_hierarchy, " _
                             & "  reportd_ac_name,reportd_date,reportd_group, reportd_date_generate," _
                             & "  reportd_value " _
                             & ") " _
                             & "VALUES ( " _
                             & SetSetring(Guid.NewGuid.ToString) & ",  " _
                             & SetSetring(_report_oid) & ",  " _
                             & SetSetring(dr_ac("type")) & ",  " _
                             & SetSetring(Format(_date_current, "yyyyMMdd")) & ",  " _
                             & SetSetring("") & ",  " _
                             & SetSetring("") & ",  " _
                             & SetSetring("") & ",  " _
                             & SetSetring("") & ",  " _
                             & SetDateNTime00(_date_current) & ", 'CASH_IN_OUT', " _
                             & SetDateNTime(CekTanggal) & ",  " _
                             & SetDec(dr_ac("glt_value")) & "  " _
                             & ")"

                            If dr_ac("type") = "CASH_IN_OUT-CASH IN" Then
                                _selisih = _selisih + SetNumber(dr_ac("glt_value"))
                            Else
                                _selisih = _selisih - SetNumber(dr_ac("glt_value"))
                            End If

                            For Each dr_db As DataRow In dt_db.Rows
                                DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                            Next
                        End If

                    Next

                    sSQL = "INSERT INTO  " _
                            & "  public.reportd_detail_sub " _
                            & "( " _
                            & "  reportd_oid, " _
                            & "  reportd_report_oid, " _
                            & "  reportd_type, " _
                            & "  reportd_periode, " _
                            & "  reportd_ac_id, " _
                            & "  reportd_ac_code, " _
                            & "  reportd_ac_hierarchy, " _
                            & "  reportd_ac_name,reportd_date,reportd_group, reportd_date_generate," _
                            & "  reportd_value " _
                            & ") " _
                            & "VALUES ( " _
                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                            & SetSetring(_report_oid) & ",  " _
                            & SetSetring("CASH_IN_OUT-SELISIH") & ",  " _
                            & SetSetring(Format(_date_current, "yyyyMMdd")) & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetDateNTime00(_date_current) & ", 'CASH_IN_OUT', " _
                            & SetDateNTime(CekTanggal) & ",  " _
                            & SetDec(_selisih) & "  " _
                            & ")"


                    For Each dr_db As DataRow In dt_db.Rows
                        DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                    Next


                    _percent = (n + 1) / _count_day * 100
                    LblStatus.Text = "Processing .. " & par_type & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()

                Next


            ElseIf par_type = "Account" Then
                sSQL = "SELECT  " _
                 & "  a.dash_type_id, " _
                 & "  a.dash_type_number, " _
                 & "  a.dash_type_filter, " _
                 & "  b.dashd_oid, " _
                 & "  b.dashd_dash_type_id, " _
                 & "  b.dashd_ac_id, " _
                 & "  public.ac_mstr.ac_code, " _
                 & "  public.ac_mstr.ac_code_hirarki, " _
                 & "  public.ac_mstr.ac_name,0.0 as value " _
                 & "FROM " _
                 & "  public.dashboard_setting a " _
                 & "  LEFT OUTER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id) " _
                 & "  LEFT OUTER JOIN public.ac_mstr ON (b.dashd_ac_id = public.ac_mstr.ac_id) " _
                 & "WHERE " _
                 & "  a.dash_type_filter = 'ACCOUNT' " _
                 & "ORDER BY " _
                 & "  a.dash_type_filter, " _
                 & "  a.dash_type_number"

                Dim dt_setting As New DataTable
                dt_setting = GetTableData(sSQL)


                Dim dt_bs As New DataTable

                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)

                For Each dr_gl_cal As DataRow In dt_gl_cal.Rows

                    sSQL = "select dash_type_id,sum(v_nilai) as value from ( " _
                        & "SELECT  " _
                        & "  a.dash_type_id, " _
                        & "  a.dash_type_number, " _
                        & "  a.dash_type_filter, " _
                        & "  b.dashd_oid, " _
                        & "  b.dashd_dash_type_id, " _
                        & "  b.dashd_ac_id, " _
                        & "  c.ac_code, " _
                        & "  c.ac_code_hirarki, " _
                        & "  c.ac_name,(select  sum(v_nilai) as jml from   " _
                        & "  ( SELECT    x.ac_id,   x.ac_code_hirarki,   x.ac_code,   x.ac_name,     " _
                        & "  x.ac_type,f_get_balance_sheet(x.ac_id,1,1,0, " _
                        & "  cast('" & dr_gl_cal("gcal_oid").ToString & "' as uuid),'" & dr_gl_cal("gcal_closing") & "') as v_nilai   " _
                        & "  FROM   public.ac_mstr x WHERE    " _
                        & "  substring(x.ac_code_hirarki, 1, length(c.ac_code_hirarki)) = c.ac_code_hirarki AND      " _
                        & "  ac_is_sumlevel = 'N') as temp) * 1.0 as v_nilai  " _
                        & "FROM " _
                        & "  public.dashboard_setting a " _
                        & "  INNER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id) " _
                        & "  LEFT OUTER JOIN public.ac_mstr c ON (b.dashd_ac_id = c.ac_id) " _
                        & "WHERE " _
                        & "  a.dash_type_filter = 'ACCOUNT' " _
                        & "ORDER BY " _
                        & "  a.dash_type_filter, " _
                        & "  a.dash_type_number) as tp group by dash_type_id"


                    dt_bs = GetTableData(sSQL)
                    Dim _penjualan, _hpp, _biaya_op, _laba_kotor, _laba_operasi, _laba_sebelum_pajak, _pendapatan_biaya_lain As Double
                    _penjualan = 0.0
                    _hpp = 0.0
                    _biaya_op = 0.0
                    '_laba_kotor = 0.0
                    '_laba_operasi = 0.0
                    '_laba_sebelum_pajak = 0.0
                    _pendapatan_biaya_lain = 0.0

                    For Each dr_bs As DataRow In dt_bs.Rows
                        For Each dr_setting As DataRow In dt_setting.Rows
                            If dr_bs("dash_type_id") = dr_setting("dash_type_id") Then
                                dr_setting("value") = dr_bs("value")
                            End If
                        Next
                    Next

                    '_laba_kotor = _penjualan + _hpp
                    '_laba_operasi = _laba_kotor + _biaya_op
                    '_laba_sebelum_pajak = _laba_operasi + _pendapatan_biaya_lain

                    dt_setting.AcceptChanges()

                    'For Each dr_setting As DataRow In dt_setting.Rows
                    '    If dr_setting("dash_type_id") = "ACCOUNT-BANK" Then
                    '        dr_setting("value") = _laba_kotor
                    '    ElseIf dr_setting("dash_type_id") = "PROFIT_LOSS-LABA OPERASI" Then
                    '        dr_setting("value") = _laba_operasi

                    '    ElseIf dr_setting("dash_type_id") = "PROFIT_LOSS-LABA SEBELUM PAJAK" Then
                    '        dr_setting("value") = _laba_sebelum_pajak
                    '    End If
                    'Next

                    'dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows

                        sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                            & " and reportd_type=" & SetSetring(dr_setting("dash_type_id")) & " and " _
                            & " reportd_periode= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM"))


                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next

                        sSQL = "INSERT INTO  " _
                            & "  public.reportd_detail " _
                            & "( " _
                            & "  reportd_oid, " _
                            & "  reportd_report_oid, " _
                            & "  reportd_type, " _
                            & "  reportd_periode, " _
                            & "  reportd_ac_id, " _
                            & "  reportd_ac_code, " _
                            & "  reportd_ac_hierarchy, " _
                            & "  reportd_ac_name,reportd_date_generate,reportd_group, " _
                            & "  reportd_value " _
                            & ") " _
                            & "VALUES ( " _
                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                            & SetSetring(_report_oid) & ",  " _
                            & SetSetring(dr_setting("dash_type_id")) & ",  " _
                            & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetDateNTime(CekTanggal) & ", 'ACCOUNT', " _
                            & SetDec(dr_setting("value")) & "  " _
                            & ")"

                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next
                    Next


                    For Each dr_setting As DataRow In dt_setting.Rows
                        dr_setting("value") = 0.0
                    Next
                    dt_setting.AcceptChanges()

                    _processed = _processed + 1.0
                    _percent = _processed / _count * 100
                    LblStatus.Text = "Processing .. " & par_type & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()
                Next
            ElseIf par_type = "Balance Sheet New" Then
                'Balance Sheet New
                'Profit Loss New
                'Cash Flow New

                Dim _balance_sheet_new_current_assets As Double = 0
                Dim _balance_sheet_new_investment As Double = 0
                Dim _balance_sheet_new_fixed_assets As Double = 0
                Dim _balance_sheet_new_total_assets As Double = 0
                Dim _balance_sheet_new_current_liabilities As Double = 0
                Dim _balance_sheet_new_long_term_liabilities As Double = 0
                Dim _balance_sheet_new_net_worth As Double = 0
                Dim _balance_sheet_new_total_liabilities_equities As Double = 0
                Dim _balance_sheet_new_cash_on_hand As Double = 0
                Dim _balance_sheet_new_inventories As Double = 0



                sSQL = "SELECT  " _
                   & "  a.dash_type_id, " _
                   & "  a.dash_type_number, " _
                   & "  a.dash_type_filter, " _
                   & "  b.dashd_oid, " _
                   & "  b.dashd_dash_type_id, " _
                   & "  b.dashd_ac_id, " _
                   & "  public.ac_mstr.ac_code, " _
                   & "  public.ac_mstr.ac_code_hirarki, " _
                   & "  public.ac_mstr.ac_name,0.0 as value " _
                   & "FROM " _
                   & "  public.dashboard_setting a " _
                   & "  LEFT OUTER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id) " _
                   & "  LEFT OUTER JOIN public.ac_mstr ON (b.dashd_ac_id = public.ac_mstr.ac_id) " _
                   & "WHERE " _
                   & "  a.dash_type_filter = 'BALANCE_SHEET_NEW' " _
                   & "ORDER BY " _
                   & "  a.dash_type_filter, " _
                   & "  a.dash_type_number"

                Dim dt_setting As New DataTable
                dt_setting = GetTableData(sSQL)

                'Exit Try

                Dim dt_bs As New DataTable
                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)

                For Each dr_gl_cal As DataRow In dt_gl_cal.Rows

                    sSQL = "SELECT a.bs_number,  a.bs_caption,  a.bs_group,   " _
                      & "a.bs_remarks,  b.bsd_number,  b.bsd_caption,   " _
                      & "b.bsd_remarks, c.bsdi_number,  c.bsdi_caption,   " _
                      & "c.bsdi_oid,    " _
                      & "(select sum(jml) from (SELECT  (select sum(v_nilai) as nilai from (  " _
                      & "SELECT y.ac_id,   y.ac_code_hirarki, y.ac_code,   y.ac_name,    " _
                      & "y.ac_type,f_get_balance_sheet(y.ac_id,1,1,0,cast('" & dr_gl_cal("gcal_oid").ToString & "' as uuid),'" & dr_gl_cal("gcal_closing") & "') as v_nilai  " _
                      & "FROM   public.ac_mstr y WHERE   substring(y.ac_code_hirarki, 1, length(x.bsda_ac_hirarki)) = x.bsda_ac_hirarki AND     " _
                      & "y.ac_is_sumlevel = 'N') as temp) as jml  FROM   public.bsda_account x WHERE   x.bsda_bsdi_oid = c.bsdi_oid) as temp2)   as value   " _
                      & "FROM   public.bs_mstr a    " _
                      & "INNER JOIN public.bsd_det b ON (a.bs_number = b.bsd_bs_number)    " _
                      & "INNER JOIN public.bsdi_det_item c ON (b.bsd_oid = c.bsdi_bsd_oid)  " _
                      & " order by bs_number, bsd_number, bsdi_number"

                    dt_bs = GetTableData(sSQL)
                    ' Exit Try

                    For Each dr_bs As DataRow In dt_bs.Rows
                        If dr_bs("bsd_caption") = "Harta Lancar" Then
                            _balance_sheet_new_current_assets = _balance_sheet_new_current_assets + SetNumber(dr_bs("value"))
                        ElseIf dr_bs("bsd_caption") = "Investasi Jangka Panjang" Then
                            _balance_sheet_new_investment = _balance_sheet_new_investment + SetNumber(dr_bs("value"))
                        ElseIf dr_bs("bsd_caption") = "Harta Tetap" Then
                            _balance_sheet_new_fixed_assets = _balance_sheet_new_fixed_assets + SetNumber(dr_bs("value"))
                        ElseIf dr_bs("bsd_caption") = "Hutang Lancar" Then
                            _balance_sheet_new_current_liabilities = _balance_sheet_new_current_liabilities + SetNumber(dr_bs("value"))
                        ElseIf dr_bs("bsd_caption") = "Hutang Bank" Then
                            _balance_sheet_new_long_term_liabilities = _balance_sheet_new_long_term_liabilities + SetNumber(dr_bs("value"))
                        End If
                        If dr_bs("bsdi_caption") = "Kas" Then
                            _balance_sheet_new_cash_on_hand = _balance_sheet_new_cash_on_hand + SetNumber(dr_bs("value"))
                        ElseIf dr_bs("bsdi_caption") = "Persediaan Bahan Baku" Or dr_bs("bsdi_caption") = "Persediaan Barang Dalam Proses (WIP)" _
                        Or dr_bs("bsdi_caption") = "Persediaan Barang Jadi" Or dr_bs("bsdi_caption") = "Persediaan Konsinyasi" _
                         Or dr_bs("bsdi_caption") = "Persedian Suku Cadang" Or dr_bs("bsdi_caption") = "Persediaan Lain-lain" Then
                            _balance_sheet_new_inventories = _balance_sheet_new_inventories + SetNumber(dr_bs("value"))
                        End If



                    Next

                    _balance_sheet_new_total_assets = _balance_sheet_new_current_assets + _balance_sheet_new_investment + _balance_sheet_new_fixed_assets
                    _balance_sheet_new_net_worth = _balance_sheet_new_total_assets - _balance_sheet_new_current_liabilities - _balance_sheet_new_long_term_liabilities
                    _balance_sheet_new_total_liabilities_equities = _balance_sheet_new_current_liabilities + _balance_sheet_new_long_term_liabilities + _balance_sheet_new_net_worth


                    For Each dr_setting As DataRow In dt_setting.Rows
                        If dr_setting("dash_type_id") = "BALANCE_SHEET_NEW-CURRENT ASSETS" Then
                            dr_setting("value") = _balance_sheet_new_current_assets
                        ElseIf dr_setting("dash_type_id") = "BALANCE_SHEET_NEW-INVESTMENT" Then
                            dr_setting("value") = _balance_sheet_new_investment
                        ElseIf dr_setting("dash_type_id") = "BALANCE_SHEET_NEW-FIXED ASSETS" Then
                            dr_setting("value") = _balance_sheet_new_fixed_assets
                        ElseIf dr_setting("dash_type_id") = "BALANCE_SHEET_NEW-TOTAL ASSETS" Then
                            dr_setting("value") = _balance_sheet_new_total_assets
                        ElseIf dr_setting("dash_type_id") = "BALANCE_SHEET_NEW-CURRENT LIABILITIES" Then
                            dr_setting("value") = _balance_sheet_new_current_liabilities
                        ElseIf dr_setting("dash_type_id") = "BALANCE_SHEET_NEW-LONG-TERM LIABILITIES" Then
                            dr_setting("value") = _balance_sheet_new_long_term_liabilities
                        ElseIf dr_setting("dash_type_id") = "BALANCE_SHEET_NEW-NET WORTH" Then
                            dr_setting("value") = _balance_sheet_new_net_worth
                        ElseIf dr_setting("dash_type_id") = "BALANCE_SHEET_NEW-TOTAL LIABILITIES & EQUITIES" Then
                            dr_setting("value") = _balance_sheet_new_total_liabilities_equities
                        ElseIf dr_setting("dash_type_id") = "BALANCE_SHEET_NEW-CASH ON HAND" Then
                            dr_setting("value") = _balance_sheet_new_cash_on_hand
                        ElseIf dr_setting("dash_type_id") = "BALANCE_SHEET_NEW-INVENTORIES" Then
                            dr_setting("value") = _balance_sheet_new_inventories

                        End If
                    Next

                    dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows

                        sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                            & " and reportd_type=" & SetSetring(dr_setting("dash_type_id")) & " and " _
                            & " reportd_periode= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM"))


                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next

                        sSQL = "INSERT INTO  " _
                            & "  public.reportd_detail " _
                            & "( " _
                            & "  reportd_oid, " _
                            & "  reportd_report_oid, " _
                            & "  reportd_type, " _
                            & "  reportd_periode, " _
                            & "  reportd_ac_id, " _
                            & "  reportd_ac_code, " _
                            & "  reportd_ac_hierarchy, " _
                            & "  reportd_ac_name,reportd_group,reportd_date_generate," _
                            & "  reportd_value " _
                            & ") " _
                            & "VALUES ( " _
                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                            & SetSetring(_report_oid) & ",  " _
                            & SetSetring(dr_setting("dash_type_id")) & ",  " _
                            & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",'BALANCE_SHEET_NEW',   " _
                            & SetDateNTime(CekTanggal()) & "," _
                            & SetDec(dr_setting("value")) & "  " _
                            & ")"

                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next
                    Next


                    For Each dr_setting As DataRow In dt_setting.Rows
                        dr_setting("value") = 0.0
                    Next
                    dt_setting.AcceptChanges()

                    _processed = _processed + 1.0
                    _percent = _processed / _count * 100
                    LblStatus.Text = "Processing .. " & par_type & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()
                Next

            ElseIf par_type = "Profit Loss New" Then


                sSQL = "SELECT  " _
                  & "  a.dash_type_id, " _
                  & "  a.dash_type_number, " _
                  & "  a.dash_type_filter, " _
                  & "  b.dashd_oid, " _
                  & "  b.dashd_dash_type_id, " _
                  & "  b.dashd_ac_id, " _
                  & "  public.ac_mstr.ac_code, " _
                  & "  public.ac_mstr.ac_code_hirarki, " _
                  & "  public.ac_mstr.ac_name,0.0 as value " _
                  & "FROM " _
                  & "  public.dashboard_setting a " _
                  & "  LEFT OUTER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id) " _
                  & "  LEFT OUTER JOIN public.ac_mstr ON (b.dashd_ac_id = public.ac_mstr.ac_id) " _
                  & "WHERE " _
                  & "  a.dash_type_filter = 'PROFIT_LOSS_NEW' " _
                  & "ORDER BY " _
                  & "  a.dash_type_filter, " _
                  & "  a.dash_type_number"

                Dim dt_setting As New DataTable
                dt_setting = GetTableData(sSQL)


                Dim dt_bs As New DataTable

                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)

                For Each dr_gl_cal As DataRow In dt_gl_cal.Rows


                    sSQL = "SELECT    a.pl_oid,   a.pl_footer,   a.pl_sign,   a.pl_number,    " _
                      & "b.pls_oid,   b.pls_item,   b.pls_number,   c.pla_ac_id,   d.ac_code,    " _
                      & "d.ac_name,   c.pla_ac_hirarki,(select  sum(v_nilai) as jml from  " _
                      & "( SELECT    x.ac_id,   x.ac_code_hirarki,   x.ac_code,   x.ac_name,    " _
                      & "x.ac_type,f_get_balance_sheet_pl(x.ac_id,1,1,0,cast('" & dr_gl_cal("gcal_oid").ToString & "' as uuid),'" & dr_gl_cal("gcal_closing") & "') as v_nilai  " _
                      & "FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1, length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
                      & "ac_is_sumlevel = 'N') as temp) * pls_value as value FROM   public.pl_setting_mstr a    " _
                      & "INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid)    " _
                      & "INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid)    " _
                      & "INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id) ORDER BY   a.pl_number,   b.pls_number "


                    dt_bs = GetTableData(sSQL)
                    Dim _penjualan, _diskon_and_return, _hpp, _biaya_op, _laba_kotor, _laba_operasi, _laba_sebelum_pajak, _pendapatan_lain, _biaya_lain As Double
                    Dim _gpm, _om, _margin_laba_bersih, _percent_hpp, _percent_adm_umum, _percent_total_biaya As Double
                    _penjualan = 0.0
                    _hpp = 0.0
                    _biaya_op = 0.0
                    _laba_kotor = 0.0
                    _laba_operasi = 0.0
                    _laba_sebelum_pajak = 0.0
                    _pendapatan_lain = 0.0
                    _biaya_lain = 0.0


                    _gpm = 0.0
                    _om = 0.0
                    _margin_laba_bersih = 0.0
                    _percent_hpp = 0.0
                    _percent_adm_umum = 0.0
                    _percent_total_biaya = 0.0



                    For Each dr_bs As DataRow In dt_bs.Rows
                        For Each dr_setting As DataRow In dt_setting.Rows

                            If dr_bs("pls_item").ToString.ToLower = "Penjualan".ToLower And dr_setting("dash_type_id") = "PROFIT_LOSS_NEW-SALES BRUTO" Then
                                dr_setting("value") = dr_setting("value") + SetNumber(dr_bs("value"))
                            End If
                            If dr_bs("pls_item").ToString.ToLower = "Retur Penjualan & Potongan Harga".ToLower And dr_setting("dash_type_id") = "PROFIT_LOSS_NEW-DISCOUNT & RETURN" Then
                                dr_setting("value") = dr_setting("value") + (SetNumber(dr_bs("value")) * -1.0)
                            End If
                            If dr_bs("pls_item").ToString.ToLower = "Potongan Penjualan".ToLower And dr_setting("dash_type_id") = "PROFIT_LOSS_NEW-DISCOUNT & RETURN" Then
                                dr_setting("value") = dr_setting("value") + (SetNumber(dr_bs("value")) * -1.0)
                            End If

                            If dr_bs("pls_item").ToString.ToLower = "Harga pokok penjualan".ToLower And dr_setting("dash_type_id") = "PROFIT_LOSS_NEW-COGS" Then
                                dr_setting("value") = dr_setting("value") + (SetNumber(dr_bs("value")) * -1.0)
                            End If

                            If dr_bs("pls_item").ToString.ToLower = "Biaya Marketing".ToLower And dr_setting("dash_type_id") = "PROFIT_LOSS_NEW-MARKETING EXP" Then
                                dr_setting("value") = dr_setting("value") + (SetNumber(dr_bs("value")) * -1.0)
                            End If

                            If dr_bs("pls_item").ToString.ToLower = "Biaya Operasional".ToLower And dr_setting("dash_type_id") = "PROFIT_LOSS_NEW-OPERATIONAL EXP" Then
                                dr_setting("value") = dr_setting("value") + (SetNumber(dr_bs("value")) * -1.0)
                            End If

                            If dr_bs("pls_item").ToString.ToLower = "Biaya Administrasi dan Umum".ToLower And dr_setting("dash_type_id") = "PROFIT_LOSS_NEW-ADMINISTRATION EXP" Then
                                dr_setting("value") = dr_setting("value") + (SetNumber(dr_bs("value")) * -1.0)
                            End If
                            If dr_bs("pls_item").ToString.ToLower = "Pendapatan Lain-Lain".ToLower And dr_setting("dash_type_id") = "PROFIT_LOSS_NEW-OTHER INCOME (NET)" Then
                                dr_setting("value") = dr_setting("value") + SetNumber(dr_bs("value"))
                            End If
                            If dr_bs("pls_item").ToString.ToLower = "Biaya Lain-lain".ToLower And dr_setting("dash_type_id") = "PROFIT_LOSS_NEW-OTHER INCOME (NET)" Then
                                dr_setting("value") = dr_setting("value") + SetNumber(dr_bs("value"))
                            End If

                        Next
                        _laba_sebelum_pajak = _laba_sebelum_pajak + SetNumber(dr_bs("value"))
                    Next

                    '_laba_kotor = _penjualan + _hpp
                    '_laba_operasi = _laba_kotor + _biaya_op
                    '_laba_sebelum_pajak = _laba_operasi + _pendapatan_lain + _biaya_lain
                    Try
                        '_gpm = _laba_kotor / _penjualan
                    Catch ex As Exception
                    End Try

                    Try
                        '_om = _laba_operasi / _penjualan
                    Catch ex As Exception
                    End Try
                    Try
                        '_margin_laba_bersih = _laba_sebelum_pajak / _penjualan
                    Catch ex As Exception
                    End Try

                    Try
                        '_percent_hpp = (_hpp * -1.0) / _penjualan
                    Catch ex As Exception

                    End Try

                    Try
                        '_percent_adm_umum = (_biaya_op * -1.0) / _penjualan
                    Catch ex As Exception

                    End Try
                    Try
                        '_percent_total_biaya = ((_biaya_op + _biaya_lain + _hpp) * -1.0) / (_penjualan + _pendapatan_lain)
                    Catch ex As Exception

                    End Try


                    dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows
                        If dr_setting("dash_type_id") = "PROFIT_LOSS_NEW-NET INCOME" Then
                            dr_setting("value") = _laba_sebelum_pajak
                        End If
                    Next

                    dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows

                        sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                            & " and reportd_type=" & SetSetring(dr_setting("dash_type_id")) & " and " _
                            & " reportd_periode= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM"))


                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next

                        sSQL = "INSERT INTO  " _
                            & "  public.reportd_detail " _
                            & "( " _
                            & "  reportd_oid, " _
                            & "  reportd_report_oid, " _
                            & "  reportd_type, " _
                            & "  reportd_periode, " _
                            & "  reportd_ac_id, " _
                            & "  reportd_ac_code, " _
                            & "  reportd_ac_hierarchy, " _
                            & "  reportd_ac_name, reportd_group,reportd_date_generate," _
                            & "  reportd_value " _
                            & ") " _
                            & "VALUES ( " _
                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                            & SetSetring(_report_oid) & ",  " _
                            & SetSetring(dr_setting("dash_type_id")) & ",  " _
                            & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ", 'PROFIT_LOSS_NEW',   " _
                            & SetDateNTime(CekTanggal()) & "," _
                            & SetDec(dr_setting("value")) & "  " _
                            & ")"

                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next
                    Next


                    For Each dr_setting As DataRow In dt_setting.Rows
                        dr_setting("value") = 0.0
                    Next
                    dt_setting.AcceptChanges()

                    _processed = _processed + 1.0
                    _percent = _processed / _count * 100
                    LblStatus.Text = "Processing .. " & par_type & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()
                Next

            ElseIf par_type = "Monthly Trend" Then


                sSQL = "SELECT  " _
                  & "  a.dash_type_id, " _
                  & "  a.dash_type_number, " _
                  & "  a.dash_type_filter, " _
                  & "  b.dashd_oid, " _
                  & "  b.dashd_dash_type_id, " _
                  & "  b.dashd_ac_id, " _
                  & "  public.ac_mstr.ac_code, " _
                  & "  public.ac_mstr.ac_code_hirarki, " _
                  & "  public.ac_mstr.ac_name,0.0 as value " _
                  & "FROM " _
                  & "  public.dashboard_setting a " _
                  & "  LEFT OUTER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id) " _
                  & "  LEFT OUTER JOIN public.ac_mstr ON (b.dashd_ac_id = public.ac_mstr.ac_id) " _
                  & "WHERE " _
                  & "  a.dash_type_filter = 'MONTHLY_TREND' " _
                  & "ORDER BY " _
                  & "  a.dash_type_filter, " _
                  & "  a.dash_type_number"

                Dim dt_setting As New DataTable
                dt_setting = GetTableData(sSQL)


                Dim dt_bs As New DataTable
                Dim dt_bs_ok As New DataTable

                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)

                For Each dr_gl_cal As DataRow In dt_gl_cal.Rows


                    sSQL = "SELECT    a.pl_oid,   a.pl_footer,   a.pl_sign,   a.pl_number,    " _
                      & "b.pls_oid,   b.pls_item,   b.pls_number,   c.pla_ac_id,   d.ac_code,    " _
                      & "d.ac_name,   c.pla_ac_hirarki,(select  sum(v_nilai) as jml from  " _
                      & "( SELECT    x.ac_id,   x.ac_code_hirarki,   x.ac_code,   x.ac_name,    " _
                      & "x.ac_type,f_get_balance_sheet_pl(x.ac_id,1,1,0,cast('" & dr_gl_cal("gcal_oid").ToString & "' as uuid),'" & dr_gl_cal("gcal_closing") & "') as v_nilai  " _
                      & "FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1, length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
                      & "ac_is_sumlevel = 'N') as temp) * pls_value as value FROM   public.pl_setting_mstr a    " _
                      & "INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid)    " _
                      & "INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid)    " _
                      & "INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id) ORDER BY   a.pl_number,   b.pls_number "


                    dt_bs = GetTableData(sSQL)


                    sSQL = "SELECT a.bs_number,  a.bs_caption,  a.bs_group,   " _
                     & "a.bs_remarks,  b.bsd_number,  b.bsd_caption,   " _
                     & "b.bsd_remarks, c.bsdi_number,  c.bsdi_caption,   " _
                     & "c.bsdi_oid,    " _
                     & "(select sum(jml) from (SELECT  (select sum(v_nilai) as nilai from (  " _
                     & "SELECT y.ac_id,   y.ac_code_hirarki, y.ac_code,   y.ac_name,    " _
                     & "y.ac_type,f_get_balance_sheet(y.ac_id,1,1,0,cast('" & dr_gl_cal("gcal_oid").ToString & "' as uuid),'" & dr_gl_cal("gcal_closing") & "') as v_nilai  " _
                     & "FROM   public.ac_mstr y WHERE   substring(y.ac_code_hirarki, 1, length(x.bsda_ac_hirarki)) = x.bsda_ac_hirarki AND     " _
                     & "y.ac_is_sumlevel = 'N') as temp) as jml  FROM   public.bsda_account x WHERE   x.bsda_bsdi_oid = c.bsdi_oid) as temp2)   as z_nilai   " _
                     & "FROM   public.bs_mstr a    " _
                     & "INNER JOIN public.bsd_det b ON (a.bs_number = b.bsd_bs_number)    " _
                     & "INNER JOIN public.bsdi_det_item c ON (b.bsd_oid = c.bsdi_bsd_oid)  " _
                     & " order by bs_number,bsd_number,bsdi_number"

                    dt_bs_ok = GetTableData(sSQL)


                    Dim _penjualan, _penjualan_bersih, _hpp, _biaya_op, _laba_kotor, _laba_operasi, _laba_sebelum_pajak, _pendapatan_lain, _biaya_lain As Double
                    Dim _gpm, _om, _margin_laba_bersih, _percent_hpp, _percent_adm_umum, _percent_total_biaya, _
                     _biaya_operasional, _biaya_adum, _biaya_marketing, _net_work_modal, _working_capital, _current_ratio, _quick_ratio, _der_ratio As Double

                    Dim _laba_sebelum_pajak_persen, _biaya_marketing_persen, _biaya_adum_persen, _biaya_operasional_persen As Double
                    Dim _harta_lancar, _hutang_lancar, _kas_setara_kas, _persediaan, _hutang, _modal As Double

                    _hutang = 0.0
                    _modal = 0.0
                    _persediaan = 0.0
                    _kas_setara_kas = 0.0
                    _harta_lancar = 0.0
                    _hutang_lancar = 0.0
                    _penjualan = 0.0
                    _hpp = 0.0
                    _biaya_op = 0.0
                    _laba_kotor = 0.0
                    _laba_operasi = 0.0
                    _laba_sebelum_pajak = 0.0
                    _pendapatan_lain = 0.0
                    _biaya_lain = 0.0
                    _penjualan_bersih = 0.0

                    _laba_sebelum_pajak_persen = 0.0
                    _biaya_marketing_persen = 0.0
                    _biaya_adum_persen = 0.0
                    _biaya_operasional_persen = 0.0



                    _gpm = 0.0
                    _om = 0.0
                    _margin_laba_bersih = 0.0
                    _percent_hpp = 0.0
                    _percent_adm_umum = 0.0
                    _percent_total_biaya = 0.0

                    _biaya_operasional = 0.0
                    _biaya_adum = 0.0
                    _biaya_marketing = 0.0
                    _net_work_modal = 0.0
                    _working_capital = 0.0
                    _current_ratio = 0.0
                    _quick_ratio = 0.0
                    _der_ratio = 0.0


                    For Each dr_bs_new As DataRow In dt_bs_ok.Rows
                        If dr_bs_new("bsd_caption") = "Harta Lancar" Then
                            _harta_lancar = _harta_lancar + SetNumber(dr_bs_new("z_nilai"))
                        ElseIf dr_bs_new("bsd_caption") = "Hutang Lancar" Then
                            _hutang_lancar = _hutang_lancar + SetNumber(dr_bs_new("z_nilai"))
                        End If
                        If dr_bs_new("bsdi_caption") = "Kas" Or dr_bs_new("bsdi_caption") = "Warkat Intransit" Or dr_bs_new("bsdi_caption") = "Bank" Then
                            _kas_setara_kas = _kas_setara_kas + SetNumber(dr_bs_new("z_nilai"))
                        End If

                        If dr_bs_new("bsdi_caption").ToString.ToLower.Contains("persediaan") Then
                            _persediaan = _persediaan + SetNumber(dr_bs_new("z_nilai"))
                        End If

                        If dr_bs_new("bs_caption") = "HARTA" Then
                            _hutang = _hutang + SetNumber(dr_bs_new("z_nilai"))
                        ElseIf dr_bs_new("bs_caption") = "MODAL" Then
                            _modal = _modal + SetNumber(dr_bs_new("z_nilai"))
                        End If
                    Next


                    For Each dr_bs As DataRow In dt_bs.Rows
                        If dr_bs("pls_item") = "Penjualan" Then
                            _penjualan = _penjualan + SetNumber(dr_bs("value"))
                        ElseIf dr_bs("pls_item") = "Biaya Marketing" Then
                            _biaya_marketing = _biaya_marketing + SetNumber(dr_bs("value"))
                        ElseIf dr_bs("pls_item") = "Biaya Operasional" Then
                            _biaya_operasional = _biaya_operasional + SetNumber(dr_bs("value"))
                        ElseIf dr_bs("pls_item") = "Biaya Administrasi dan Umum" Then
                            _biaya_adum = _biaya_adum + SetNumber(dr_bs("value"))
                        End If


                        _laba_sebelum_pajak = _laba_sebelum_pajak + SetNumber(dr_bs("value"))



                    Next


                    Try
                        _biaya_marketing_persen = _biaya_marketing / _penjualan * 100
                    Catch ex As Exception
                    End Try

                    Try
                        _biaya_operasional_persen = _biaya_operasional / _penjualan * 100
                    Catch ex As Exception
                    End Try

                    Try
                        _biaya_adum_persen = _biaya_adum / _penjualan * 100
                    Catch ex As Exception
                    End Try

                    Try
                        _laba_sebelum_pajak_persen = _laba_sebelum_pajak / _penjualan * 100
                    Catch ex As Exception
                    End Try

                    Try
                        ' _percent_adm_umum = (_biaya_op * -1.0) / _penjualan
                    Catch ex As Exception
                    End Try

                    Try
                        ' _percent_total_biaya = ((_biaya_op + _biaya_lain + _hpp) * -1.0) / (_penjualan + _pendapatan_lain)
                    Catch ex As Exception
                    End Try


                    dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows
                        If dr_setting("dash_type_id") = "MONTHLY_TREND-Penjualan Bruto" Then


                            Try
                                dr_setting("value") = _penjualan
                            Catch ex As Exception

                            End Try
                        ElseIf dr_setting("dash_type_id") = "MONTHLY_TREND-Biaya Marketing & Penjualan" Then

                            Try
                                dr_setting("value") = _biaya_marketing_persen
                            Catch ex As Exception

                            End Try

                        ElseIf dr_setting("dash_type_id") = "MONTHLY_TREND-Biaya Operasional" Then


                            Try
                                dr_setting("value") = _biaya_operasional_persen
                            Catch ex As Exception

                            End Try

                        ElseIf dr_setting("dash_type_id") = "MONTHLY_TREND-Biaya Administrasi dan Umum" Then
                            dr_setting("value") = _biaya_adum_persen
                        ElseIf dr_setting("dash_type_id") = "MONTHLY_TREND-Laba Rugi Sebelum Pajak" Then
                            dr_setting("value") = _laba_sebelum_pajak_persen

                        ElseIf dr_setting("dash_type_id") = "MONTHLY_TREND-Net Worth" Then

                            'sSQL = "select reportd_value from reportd_detail where reportd_type='BALANCE_SHEET-MODAL' and reportd_periode='" _
                            '    & CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM") & "'"

                            'Dim dt_temp As New DataTable

                            'dt_temp = GetTableData(sSQL)
                            'Dim _modal As Double = 0.0
                            'For Each dr_temp As DataRow In dt_temp.Rows
                            '    _modal = SetNumber(dr_temp(0))
                            'Next
                            'Try
                            '    dr_setting("value") = _modal
                            'Catch ex As Exception

                            'End Try
                            dr_setting("value") = _modal
                        ElseIf dr_setting("dash_type_id") = "MONTHLY_TREND-Working Capital" Then



                            dr_setting("value") = (_harta_lancar - _hutang_lancar)
                        ElseIf dr_setting("dash_type_id") = "MONTHLY_TREND-Current Ratio" Then
                            dr_setting("value") = _harta_lancar / _hutang_lancar

                        ElseIf dr_setting("dash_type_id") = "MONTHLY_TREND-Quick Ratio" Then
                            dr_setting("value") = (_harta_lancar - _persediaan) / _hutang_lancar

                        ElseIf dr_setting("dash_type_id") = "MONTHLY_TREND-Debt to Equity Ratio" Then
                            dr_setting("value") = _hutang / _modal
                        ElseIf dr_setting("dash_type_id") = "MONTHLY_TREND-Efektivitas Karyawan" Then
                            dr_setting("value") = 0.0



                        End If
                    Next

                    dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows

                        sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                            & " and reportd_type=" & SetSetring(dr_setting("dash_type_id")) & " and " _
                            & " reportd_periode= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM"))


                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next

                        sSQL = "INSERT INTO  " _
                            & "  public.reportd_detail " _
                            & "( " _
                            & "  reportd_oid, " _
                            & "  reportd_report_oid, " _
                            & "  reportd_type, " _
                            & "  reportd_periode, " _
                            & "  reportd_ac_id, " _
                            & "  reportd_ac_code, " _
                            & "  reportd_ac_hierarchy,reportd_group,reportd_date_generate, " _
                            & "  reportd_ac_name, " _
                            & "  reportd_value " _
                            & ") " _
                            & "VALUES ( " _
                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                            & SetSetring(_report_oid) & ",  " _
                            & SetSetring(dr_setting("dash_type_id")) & ",  " _
                            & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  'MONTHLY_TREND',   " _
                            & SetDateNTime(CekTanggal()) & "," _
                            & SetSetring("") & ",  " _
                            & SetDec(dr_setting("value")) & "  " _
                            & ")"

                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next
                    Next


                    For Each dr_setting As DataRow In dt_setting.Rows
                        dr_setting("value") = 0.0
                    Next
                    dt_setting.AcceptChanges()

                    _processed = _processed + 1.0
                    _percent = _processed / _count * 100
                    LblStatus.Text = "Processing .. " & par_type & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()
                Next

            ElseIf par_type = "Cash IN OUT New" Then

                sSQL = "SELECT  " _
                  & "  a.dash_type_id, " _
                  & "  a.dash_type_number, " _
                  & "  a.dash_type_filter, " _
                  & "  b.dashd_oid, " _
                  & "  b.dashd_dash_type_id, " _
                  & "  b.dashd_ac_id, " _
                  & "  public.ac_mstr.ac_code, " _
                  & "  public.ac_mstr.ac_code_hirarki, " _
                  & "  public.ac_mstr.ac_name,0.0 as value " _
                  & "FROM " _
                  & "  public.dashboard_setting a " _
                  & "  LEFT OUTER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id) " _
                  & "  LEFT OUTER JOIN public.ac_mstr ON (b.dashd_ac_id = public.ac_mstr.ac_id) " _
                  & "WHERE " _
                  & "  a.dash_type_filter = 'CASH_IN_OUT_NEW' " _
                  & "ORDER BY " _
                  & "  a.dash_type_filter, " _
                  & "  a.dash_type_number"

                Dim dt_setting As New DataTable
                dt_setting = GetTableData(sSQL)


                Dim dt_bs As New DataTable

                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)

                For Each dr_gl_cal As DataRow In dt_gl_cal.Rows
                    sSQL = "SELECT  " _
                        & " sum((SELECT  " _
                        & "       sum(glbal_balance_open) AS jml " _
                        & "       FROM " _
                        & "       public.glbal_balance x " _
                        & "       WHERE " _
                        & "       x.glbal_gcal_oid = '" & dr_gl_cal("gcal_oid").ToString & "' AND   " _
                        & "       x.glbal_ac_id = d.ac_id )) as total " _
                        & "FROM " _
                        & "  public.tconfsettingcashflow a " _
                        & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
                        & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code) " _
                        & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
                        & "WHERE " _
                        & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D' "

                    Dim _awal, _akhir, _cash_in, _cash_out As Double
                    _awal = 0
                    _akhir = 0
                    _cash_in = 0
                    _cash_out = 0

                    Dim dt_temp As New DataTable

                    dt_temp = master_new.PGSqlConn.GetTableData(sSQL)

                    For Each dr As DataRow In dt_temp.Rows
                        _awal = dr("total")
                    Next

                    sSQL = "SELECT  " _
                        & " 'CASH-CASH IN' as type, sum((public.glt_det.glt_debit * public.glt_det.glt_exc_rate)) AS glt_value " _
                        & "FROM " _
                        & "  public.glt_det " _
                        & "WHERE " _
                        & " to_char(public.glt_det.glt_date,'yyyyMM') = " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & " AND  " _
                        & "  public.glt_det.glt_ac_id in (SELECT   d.ac_id " _
                            & "FROM " _
                            & "  public.tconfsettingcashflow a " _
                            & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
                            & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code) " _
                            & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
                            & "WHERE " _
                            & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D' ) union all " _
                        & "SELECT  " _
                        & " 'CASH-CASH OUT' as type, sum((public.glt_det.glt_credit * public.glt_det.glt_exc_rate)) AS glt_value " _
                        & "FROM " _
                        & "  public.glt_det " _
                        & "WHERE " _
                        & "  to_char(public.glt_det.glt_date,'yyyyMM')= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & " AND  " _
                        & "  public.glt_det.glt_ac_id in (SELECT   d.ac_id " _
                            & "FROM " _
                            & "  public.tconfsettingcashflow a " _
                            & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
                            & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code) " _
                            & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
                            & "WHERE " _
                            & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D')"

                    Dim dt_temp2 As New DataTable
                    dt_temp2 = master_new.PGSqlConn.GetTableData(sSQL)

                    For Each dr As DataRow In dt_temp2.Rows
                        If dr("type") = "CASH-CASH IN" Then
                            _cash_in = dr("glt_value")

                        ElseIf dr("type") = "CASH-CASH OUT" Then
                            _cash_out = dr("glt_value")
                        End If
                    Next

                    For Each dr_setting As DataRow In dt_setting.Rows
                        If dr_setting("dash_type_id") = "CASH_IN_OUT_NEW-BEGINNING BALANCE  REALIZATION" Then
                            dr_setting("value") = _awal
                        ElseIf dr_setting("dash_type_id") = "CASH_IN_OUT_NEW-CASH IN REALIZATION" Then
                            dr_setting("value") = _cash_in
                        ElseIf dr_setting("dash_type_id") = "CASH_IN_OUT_NEW-CASH OUT REALIZATION" Then
                            dr_setting("value") = _cash_out
                        ElseIf dr_setting("dash_type_id") = "CASH_IN_OUT_NEW-ENDING BALANCE REALIZATION" Then
                            dr_setting("value") = _awal - _cash_in + _cash_out
                        End If
                    Next

                    dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows


                        sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                            & " and reportd_type=" & SetSetring(dr_setting("dash_type_id")) & " and " _
                            & " reportd_periode= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM"))


                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next

                        sSQL = "INSERT INTO  " _
                            & "  public.reportd_detail " _
                            & "( " _
                            & "  reportd_oid, " _
                            & "  reportd_report_oid, " _
                            & "  reportd_type, " _
                            & "  reportd_periode, " _
                            & "  reportd_ac_id, " _
                            & "  reportd_ac_code, " _
                            & "  reportd_ac_hierarchy, " _
                            & "  reportd_ac_name, reportd_group,reportd_date_generate," _
                            & "  reportd_value " _
                            & ") " _
                            & "VALUES ( " _
                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                            & SetSetring(_report_oid) & ",  " _
                            & SetSetring(dr_setting("dash_type_id")) & ",  " _
                            & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ", 'CASH_IN_OUT_NEW',   " _
                            & SetDateNTime(CekTanggal()) & "," _
                            & SetDec(dr_setting("value")) & "  " _
                            & ")"

                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next
                    Next


                    For Each dr_setting As DataRow In dt_setting.Rows
                        dr_setting("value") = 0.0
                    Next
                    dt_setting.AcceptChanges()


                    _processed = _processed + 1.0
                    _percent = _processed / _count * 100
                    LblStatus.Text = "Processing .. " & par_type & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()

                Next
            ElseIf par_type = "Cash Flow New" Then


                sSQL = "SELECT  " _
                  & "  a.dash_type_id, " _
                  & "  a.dash_type_number, " _
                  & "  a.dash_type_filter, " _
                  & "  b.dashd_oid, " _
                  & "  b.dashd_dash_type_id, " _
                  & "  b.dashd_ac_id, " _
                  & "  public.ac_mstr.ac_code, " _
                  & "  public.ac_mstr.ac_code_hirarki, " _
                  & "  public.ac_mstr.ac_name,0.0 as value " _
                  & "FROM " _
                  & "  public.dashboard_setting a " _
                  & "  LEFT OUTER JOIN public.dashboard_setting_account b ON (a.dash_type_id = b.dashd_dash_type_id) " _
                  & "  LEFT OUTER JOIN public.ac_mstr ON (b.dashd_ac_id = public.ac_mstr.ac_id) " _
                  & "WHERE " _
                  & "  a.dash_type_filter = 'CASH_FLOW_NEW' " _
                  & "ORDER BY " _
                  & "  a.dash_type_filter, " _
                  & "  a.dash_type_number"

                Dim dt_setting As New DataTable
                dt_setting = GetTableData(sSQL)


                Dim dt_bs As New DataTable

                Dim _processed As Double = 0.0
                Dim _count As Double = CDbl(dt_gl_cal.Rows.Count)

                For Each dr_gl_cal As DataRow In dt_gl_cal.Rows



                    sSQL = "SELECT     " _
                        & "a.code,   a.remark,   a.sort_number,   a.remark_header,    " _
                        & "a.remark_footer,   a.cfsign_header,   a.cf_value_sign,   b.cfdet_oid,    " _
                        & "b.cfdet_pk,   b.seq, 0.0 as cf_value_beginning,0.0 as cf_value_ending,b.sub_header2,     " _
                        & "b.sub_header,coalesce((select   sum(f_get_cfvalue_direct(d.ac_id,cast('" & dr_gl_cal("gcal_oid").ToString & "' as uuid),c.ac_sign,ac_value)) as _value  " _
                        & "from public.tconfsettingcashflowdet_item c    " _
                        & "INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki)   " _
                        & "where c.code=b.cfdet_pk and d.ac_is_sumlevel='N'),0) * ac_value_header as cf_value  " _
                        & "FROM   public.tconfsettingcashflow a    " _
                        & "INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code)  " _
                        & "WHERE   a.cfsign_header = 'T'  and cf_type='D'"




                    Dim _total As Double = 0
                    dt_bs = GetTableData(sSQL)


                    sSQL = "SELECT  " _
                        & " sum((SELECT  " _
                        & "       sum(glbal_balance_open) AS jml " _
                        & "       FROM " _
                        & "       public.glbal_balance x " _
                        & "       WHERE " _
                        & "       x.glbal_gcal_oid = '" & dr_gl_cal("gcal_oid").ToString & "' AND   " _
                        & "       x.glbal_ac_id = d.ac_id )) as total " _
                        & "FROM " _
                        & "  public.tconfsettingcashflow a " _
                        & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
                        & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code) " _
                        & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
                        & "WHERE " _
                        & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D' "

                    Dim _awal, _akhir As Double
                    _awal = 0
                    _akhir = 0

                    Dim dt_temp As New DataTable

                    dt_temp = master_new.PGSqlConn.GetTableData(sSQL)

                    For Each dr As DataRow In dt_temp.Rows
                        _awal = dr("total")
                    Next

                    For Each dr_bs As DataRow In dt_bs.Rows
                        For Each dr_setting As DataRow In dt_setting.Rows
                            If dr_bs("code") = "cfd_usaha" And dr_setting("dash_type_id") = "CASH_FLOW_NEW-OPERATING" Then
                                dr_setting("value") = dr_setting("value") + SetNumber(dr_bs("cf_value"))

                            ElseIf dr_bs("code") = "cfd_investasi" And dr_setting("dash_type_id") = "CASH_FLOW_NEW-INVESTING" Then
                                dr_setting("value") = dr_setting("value") + SetNumber(dr_bs("cf_value"))

                            ElseIf dr_bs("code") = "cfd_keuangan" And dr_setting("dash_type_id") = "CASH_FLOW_NEW-FINANCING" Then
                                dr_setting("value") = dr_setting("value") + SetNumber(dr_bs("cf_value"))

                            End If
                            If dr_setting("dash_type_id") = "CASH_FLOW_NEW-INCREASE / (DECREASE) ON CASH" Then
                                dr_setting("value") = dr_setting("value") + SetNumber(dr_bs("cf_value"))
                            End If

                        Next

                        _total = _total + SetNumber(dr_bs("cf_value"))

                    Next

                    dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows
                        If dr_setting("dash_type_id") = "CASH_FLOW_NEW-BEGINNING BALANCE" Then
                            dr_setting("value") = _awal
                        ElseIf dr_setting("dash_type_id") = "CASH_FLOW_NEW-CASH BALANCE" Then
                            dr_setting("value") = _awal + _total
                        End If
                    Next

                    dt_setting.AcceptChanges()

                    For Each dr_setting As DataRow In dt_setting.Rows


                        sSQL = "delete from reportd_detail where reportd_report_oid=" & SetSetring(_report_oid) _
                            & " and reportd_type=" & SetSetring(dr_setting("dash_type_id")) & " and " _
                            & " reportd_periode= " & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM"))


                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next

                        sSQL = "INSERT INTO  " _
                            & "  public.reportd_detail " _
                            & "( " _
                            & "  reportd_oid, " _
                            & "  reportd_report_oid, " _
                            & "  reportd_type, " _
                            & "  reportd_periode, " _
                            & "  reportd_ac_id, " _
                            & "  reportd_ac_code, " _
                            & "  reportd_ac_hierarchy, " _
                            & "  reportd_ac_name, reportd_group,reportd_date_generate," _
                            & "  reportd_value " _
                            & ") " _
                            & "VALUES ( " _
                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                            & SetSetring(_report_oid) & ",  " _
                            & SetSetring(dr_setting("dash_type_id")) & ",  " _
                            & SetSetring(CDate(dr_gl_cal("gcal_start_date")).ToString("yyyyMM")) & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ",  " _
                            & SetSetring("") & ", 'CASH_FLOW_NEW',   " _
                            & SetDateNTime(CekTanggal()) & "," _
                            & SetDec(dr_setting("value")) & "  " _
                            & ")"

                        For Each dr_db As DataRow In dt_db.Rows
                            DbRun(sSQL, dr_db("db_host"), dr_db("db_database"), dr_db("db_port"), dr_db("db_user"))
                        Next
                    Next


                    For Each dr_setting As DataRow In dt_setting.Rows
                        dr_setting("value") = 0.0
                    Next
                    dt_setting.AcceptChanges()


                    _processed = _processed + 1.0
                    _percent = _processed / _count * 100
                    LblStatus.Text = "Processing .. " & par_type & " " & Math.Round(_percent, 2) & " %"
                    System.Windows.Forms.Application.DoEvents()

                Next
            End If


            'Box("Generate success")

            LblStatus.Text = "Processing .. " & par_type & " Success"
            System.Windows.Forms.Application.DoEvents()


            Return True
        Catch ex As Exception
            Return False
            MessageBox.Show(ex.Message)
        End Try
    End Function


    Dim UserIndex As Integer
    Dim UserName As String = ""
    Dim UserEmail As String = ""
    Dim UserId As String = ""

    Dim URL As String = "http://192.168.1.113:8000"
    Private dataResults As List(Of Data) = New List(Of Data)
    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        URL = "http://localhost/jsontest"
        Dim jsonPost As New JsonPost(URL & "/data.php")
        Dim dictData As New Dictionary(Of String, Object)
        dictData.Add("name", "Andi")
        dictData.Add("email", "andi@gmail.com")
        dictData.Add("password", "123")
        dictData.Add("password_confirmation", "123")


        Dim dict As New Dictionary(Of String, Dictionary(Of String, String))
        dict.Add("test", New Dictionary(Of String, String))
        dict("test").Add("nested1", "value1")
        dict("test").Add("nested2", "value2")
        Dim output As String = JsonConvert.SerializeObject(dict)


        'Dim response As String = jsonPost.postData(dictData, "put")

        Dim _json As String = ""
        _json = "{""nama"":""and'i yulianto"",""password"":""123"", ""detail"":[{""no"":1,""kota"":""jak'arta utara""},{""no"":2,""kota"":""bandung""}]}"



        Dim response As String = jsonPost.postDataJson(_json, "post")
        Try
            Dim _data1 As String = ""
            Dim _kota As String = ""

            Dim result As JObject = JObject.Parse(response.ToString)
            'For Each item As JObject In result
            _data1 = result.GetValue("data").ToString()

            Dim data As JObject = JObject.Parse(_data1)

            Dim result2 As JArray = JArray.Parse(data.GetValue("detail").ToString())

            For Each item As JObject In result2
                _kota = item.GetValue("kota").ToString()
            Next

            'Next
            'Dim results As Data = JsonConvert.DeserializeObject(response, GetType(Data))
            'Dim row As Data = New Data With {.id = results.id, .name = results.name, .email = results.email, .created_at = results.created_at, .updated_at = results.updated_at}

            'If results.id Then
            '    'Dim dataCell As Data = dataResults.Item(
            '    'dataCell.name = results.name
            '    'dataCell.email = results.email
            '    'dataCell.created_at = results.created_at
            '    'dataCell.updated_at = results.updated_at

            '    'Dim UserBindingSource As New BindingSource
            '    'UserBindingSource.DataSource = dataResults
            '    'DataGridView1.DataSource = UserBindingSource
            'End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub SimpleButton1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtGenAll.Click
        Try
            'Dim fileLocation As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & Path.DirectorySeparatorChar & "b.jpg"
            'Dim values As NameValueCollection = New NameValueCollection()
            'Dim files As NameValueCollection = New NameValueCollection()
            'values.Add("number", "6285743191154")
            'values.Add("caption", "test ini fotonya4")
            'files.Add("file", fileLocation)
            'sendHttpRequest("http://vpnsygma.ddns.net:8001/send-media-upload", values, files)


            For Each item In cb_type.Properties.Items
                gendata(item)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Shared Function sendHttpRequest(ByVal url As String, ByVal values As NameValueCollection, _
                                            Optional ByVal files As NameValueCollection = Nothing) As String

        Dim boundary As String = "----------------------------" & DateTime.Now.Ticks.ToString("x")
        Dim boundaryBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(vbCrLf & "--" & boundary & vbCrLf)
        Dim trailer As Byte() = System.Text.Encoding.UTF8.GetBytes(vbCrLf & "--" & boundary & "--" & vbCrLf)
        Dim boundaryBytesF As Byte() = System.Text.Encoding.ASCII.GetBytes("--" & boundary & vbCrLf)
        Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
        request.ContentType = "multipart/form-data; boundary=" & boundary
        request.Method = "POST"
        request.KeepAlive = True
        request.Credentials = System.Net.CredentialCache.DefaultCredentials
        Dim requestStream As Stream = request.GetRequestStream()

        For Each key As String In values.Keys
            Dim formItemBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(String.Format("Content-Disposition: form-data; name=""{0}"";" & vbCrLf & vbCrLf & "{1}", key, values(key)))
            requestStream.Write(boundaryBytes, 0, boundaryBytes.Length)
            requestStream.Write(formItemBytes, 0, formItemBytes.Length)
        Next

        If files IsNot Nothing Then

            For Each key As String In files.Keys

                If File.Exists(files(key)) Then
                    Dim bytesRead As Integer = 0
                    Dim buffer As Byte() = New Byte(2047) {}
                    Dim formItemBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(String.Format("Content-Disposition: form-data; name=""{0}""; filename=""{1}""" & vbCrLf & "Content-Type: image/png" & vbCrLf & vbCrLf, key, files(key)))
                    requestStream.Write(boundaryBytes, 0, boundaryBytes.Length)
                    requestStream.Write(formItemBytes, 0, formItemBytes.Length)

                    Using fileStream As FileStream = New FileStream(files(key), FileMode.Open, FileAccess.Read)

                        While (CSharpImpl.__Assign(bytesRead, fileStream.Read(buffer, 0, buffer.Length))) <> 0
                            requestStream.Write(buffer, 0, bytesRead)
                        End While

                        fileStream.Close()
                    End Using
                End If
            Next
        End If

        requestStream.Write(trailer, 0, trailer.Length)
        requestStream.Close()

        Using reader As StreamReader = New StreamReader(request.GetResponse().GetResponseStream())
            Return reader.ReadToEnd()
        End Using
    End Function

    Private Class CSharpImpl
        ' <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
        Shared Function __Assign(Of T)(ByRef target As T, ByVal value As T) As T
            target = value
            Return value
        End Function
    End Class


    'Public Function GetResponse(ByVal Uri As String, ByVal formFields As List(Of DictionaryEntry), ByVal ParamArray objects As mUploadData()) As Object
    '    Dim Boundary As String = Guid.NewGuid().ToString().Replace("-", "")
    '    Try
    '        Dim Request As HttpWebRequest = DirectCast(WebRequest.Create(New Uri(Uri)), HttpWebRequest)
    '        With Request
    '            'If Not String.IsNullOrEmpty(Proxy) Then
    '            '    Dim pData() As String = Split(Proxy, ":")
    '            '    Dim myProxy As New WebProxy(pData(0), Convert.ToInt32(pData(1)))
    '            '    If pData.Count > 2 Then myProxy.Credentials = New NetworkCredential(pData(2), pData(3))
    '            '    .Proxy = myProxy
    '            'Else
    '            '    .Proxy = Nothing
    '            'End If

    '            .UserAgent = Useragent
    '            .AllowAutoRedirect = False
    '            .Timeout = TimeOut
    '            .KeepAlive = True
    '            .AutomaticDecompression = DecompressionMethods.GZip And DecompressionMethods.Deflate
    '            .Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"
    '            If Not String.IsNullOrEmpty(Referer) Then .Referer = Referer

    '            .Headers.Add(HttpRequestHeader.AcceptLanguage, "en-us,en;q=0.5")
    '            .Headers.Add(HttpRequestHeader.AcceptCharset, "ISO-8859-1,utf-8;q=0.7,*;q=0.7")

    '            If SendCookies = True Then AddCookies(Request)

    '            .Method = "POST"
    '            .ContentType = "multipart/form-data; boundary=" & Boundary

    '            Dim PostData As New MemoryStream()

    '            Dim Writer As New StreamWriter(PostData)
    '            With Writer
    '                If formFields IsNot Nothing Then
    '                    For Each de As DictionaryEntry In formFields
    '                        .Write(("--" & Boundary) + LineFeed)
    '                        .Write("Content-Disposition: form-data; name=""{0}""{1}{1}{2}{1}", de.Key, LineFeed, de.Value)
    '                    Next
    '                End If
    '                If Not (objects Is Nothing) Then
    '                    For Each us As mUploadData In objects
    '                        .Write(("--" & Boundary) + LineFeed)
    '                        .Write("Content-Disposition: form-data; name=""{0}""; filename=""{1}""{2}", us.FieldName, us.FileName, LineFeed)
    '                        .Write(("Content-Type: application/octet-stream" & LineFeed) & LineFeed)
    '                        .Flush()
    '                        If Not (us.Contents Is Nothing) Then PostData.Write(us.Contents, 0, us.Contents.Length)
    '                        .Write(LineFeed)
    '                    Next
    '                End If
    '                .Write("--{0}--{1}", Boundary, LineFeed)
    '                .Flush()
    '            End With

    '            .ContentLength = PostData.Length
    '            Using s As Stream = .GetRequestStream()
    '                PostData.WriteTo(s)
    '            End Using
    '            PostData.Close()

    '            Dim Response As HttpWebResponse = CType(.GetResponse(), HttpWebResponse)
    '            If StoreCookies = True Then SaveCookies(Response.Cookies, False)

    '            Referer = String.Empty
    '            Return Response
    '        End With
    '    Catch we As WebException
    '        Return we
    '    Catch ex As Exception
    '        Return ex
    '    End Try
    'End Function

End Class
Public Class Data
    Public Property id() As Long
        Get

        End Get
        Set(ByVal value As Long)

        End Set
    End Property
    Public Property name() As String
        Get

        End Get
        Set(ByVal value As String)

        End Set
    End Property
    Public Property email() As String
        Get

        End Get
        Set(ByVal value As String)

        End Set
    End Property
    Public Property email_verified_at() As String
        Get

        End Get
        Set(ByVal value As String)

        End Set
    End Property
    Public Property created_at() As Date
        Get

        End Get
        Set(ByVal value As Date)

        End Set
    End Property
    Public Property updated_at() As Date
        Get

        End Get
        Set(ByVal value As Date)

        End Set
    End Property
End Class
Public Class DataError
    Public Property email() As List(Of String)
        Get

        End Get
        Set(ByVal value As List(Of String))

        End Set
    End Property
    Public Property name() As List(Of String)
        Get

        End Get
        Set(ByVal value As List(Of String))

        End Set
    End Property
    Public Property password() As List(Of String)
        Get

        End Get
        Set(ByVal value As List(Of String))

        End Set
    End Property
    Public Property password_confirmation() As List(Of String)
        Get

        End Get
        Set(ByVal value As List(Of String))

        End Set
    End Property
End Class
Public Class RootObject
    Public Property data() As List(Of Data)
        Get

        End Get
        Set(ByVal value As List(Of Data))

        End Set
    End Property
End Class

Public Class JsonPost

    Private urlToPost As String = ""
    Public postResponse As String
    Public Sub New(ByVal urlToPost As String)
        Me.urlToPost = urlToPost
    End Sub
    Public Function postData(ByVal dictData As Dictionary(Of String, Object), ByVal actionData As String) As String
        Dim webClient As New WebClient()
        Dim resByte As Byte()
        Dim reqByte As Byte()
        Dim resString As String
        Dim reqString() As Byte

        Try
            webClient.Headers("content-type") = "application/json"
            reqString = Encoding.Default.GetBytes(JsonConvert.SerializeObject(dictData, Formatting.Indented))
            resByte = webClient.UploadData(Me.urlToPost, actionData, reqString)

            resString = Encoding.Default.GetString(resByte)

            webClient.Dispose()
            Return resString
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return False
    End Function

    Public Function postDataJson(ByVal par_json As String, ByVal actionData As String) As String
        Dim webClient As New WebClient()
        Dim resByte As Byte()
        Dim reqByte As Byte()
        Dim resString As String
        Dim reqString() As Byte

        Try
            webClient.Headers("content-type") = "application/json"
            reqString = Encoding.Default.GetBytes(par_json)
            resByte = webClient.UploadData(Me.urlToPost, actionData, reqString)

            resString = Encoding.Default.GetString(resByte)

            webClient.Dispose()
            Return resString
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return False
    End Function
    Public Function getData() As String
        Dim webClient As New WebClient()
        Dim resByte As Byte()
        Dim reqByte As Byte()
        Dim resString As String
        Dim reqString() As Byte

        Try
            webClient.Headers("content-type") = "application/json"
            resByte = webClient.DownloadData(Me.urlToPost)
            resString = Encoding.Default.GetString(resByte)

            webClient.Dispose()
            Return resString
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return False
    End Function

End Class