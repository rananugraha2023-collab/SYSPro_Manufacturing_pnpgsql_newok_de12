Imports master_new.PGSqlConn
Imports npgsql
Imports master_new.ModFunction
Imports System.IO
Imports System.Text
Imports System.Web.UI.WebControls


Public Class FTransactionPost
    Dim ds As New DataSet
    Dim func_coll As New function_collection


    Private Sub FTransactionPost_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = Now
        pr_txttglakhir.DateTime = Now
    End Sub

    Public Overrides Sub format_grid()
        add_column_edit(gv_unposted, "Status", "status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_unposted, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_unposted, "GL Number", "glt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_unposted, "Date", "glt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_unposted, "Type", "glt_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_unposted, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_unposted, "Exchange Rate", "glt_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_unposted, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_unposted, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_unposted, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_unposted, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_unposted, "Description", "glt_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_unposted, "Debit", "glt_debit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_unposted, "Credit", "glt_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_unposted, "Exc Rate", "glt_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_unposted, "Ext Debit", "glt_ext_debit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_copy(gv_unposted, "Ext Credit", "glt_ext_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")

        add_column_copy(gv_unposted, "Ref. Number", "glt_ref_trans_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_unposted, "Daybook", "glt_daybook", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_unposted, "Posted", "glt_posted", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_unposted, "Status Check", "glt_check_status", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_unposted, "Is Reverse", "glt_is_reverse", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_unposted, "User Create", "glt_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_unposted, "Date Create", "glt_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_unposted, "User Update", "glt_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_unposted, "Date Update", "glt_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column_copy(gv_posted, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_posted, "GL Number", "glt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_posted, "Date", "glt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_posted, "Type", "glt_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_posted, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_posted, "Exchange Rate", "glt_exc_rate", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_posted, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_posted, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_posted, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_posted, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_posted, "Description", "glt_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_posted, "Debit", "glt_debit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_posted, "Credit", "glt_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_posted, "Exc Rate", "glt_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_posted, "Ext Debit", "glt_ext_debit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_copy(gv_posted, "Ext Credit", "glt_ext_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_copy(gv_posted, "Transaction", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_posted, "Ref. Number", "glt_ref_trans_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_posted, "Daybook", "glt_daybook", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_posted, "Posted", "glt_posted", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_posted, "Status Check", "glt_check_status", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_posted, "Is Reverse", "glt_is_reverse", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_posted, "User Create", "glt_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_posted, "Date Create", "glt_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_posted, "User Update", "glt_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_posted, "Date Update", "glt_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column_copy(gv_all, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "GL Number", "glt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date", "glt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Type", "glt_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Exchange Rate", "glt_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_all, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Description", "glt_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Debit", "glt_debit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_all, "Credit", "glt_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_all, "Exc Rate", "glt_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_all, "Ext Debit", "glt_ext_debit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_copy(gv_all, "Ext Credit", "glt_ext_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_copy(gv_all, "Ref. Number", "glt_ref_trans_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Daybook", "glt_daybook", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Posted", "glt_posted", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Status Check", "glt_check_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Seq", "glt_seq", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_copy(gv_all, "Is Reverse", "glt_is_reverse", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "User Create", "glt_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date Create", "glt_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_all, "User Update", "glt_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date Update", "glt_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                ds.Clear()
                ds.Dispose()
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        If xtc_master.SelectedTabPageIndex = 0 Then
                            .SQL = "SELECT  " _
                            & "  public.glt_det.glt_oid, " _
                            & "  public.glt_det.glt_dom_id, " _
                            & "  public.glt_det.glt_en_id, " _
                            & "  public.en_mstr.en_desc, " _
                            & "  public.glt_det.glt_add_by, " _
                            & "  public.glt_det.glt_add_date, " _
                            & "  public.glt_det.glt_upd_by, " _
                            & "  public.glt_det.glt_upd_date, " _
                            & "  public.glt_det.glt_gl_oid, " _
                            & "  public.glt_det.glt_code, " _
                            & "  public.glt_det.glt_date, " _
                            & "  public.glt_det.glt_type, " _
                            & "  public.glt_det.glt_cu_id, " _
                            & "  public.glt_det.glt_exc_rate, " _
                            & "  public.glt_det.glt_seq, " _
                            & "  public.glt_det.glt_ac_id, " _
                            & "  public.glt_det.glt_sb_id, " _
                            & "  public.glt_det.glt_cc_id, " _
                            & "  public.glt_det.glt_desc, " _
                            & "  public.glt_det.glt_debit, " _
                            & "  public.glt_det.glt_credit,coalesce(glt_check_status,'N') as glt_check_status, " _
                            & "  public.glt_det.glt_debit * public.glt_det.glt_exc_rate as glt_ext_debit, " _
                            & "  public.glt_det.glt_credit * public.glt_det.glt_exc_rate as glt_ext_credit, " _
                            & "  public.glt_det.glt_posted, " _
                            & "  public.glt_det.glt_dt, " _
                            & "  public.glt_det.glt_is_reverse, " _
                            & "  public.tran_mstr.tran_name, " _
                            & "  public.glt_det.glt_ref_trans_code, glt_daybook, " _
                            & "  public.cu_mstr.cu_name, " _
                            & "  public.ac_mstr.ac_code, " _
                            & "  public.ac_mstr.ac_name, " _
                            & "  public.cc_mstr.cc_desc, " _
                            & "  public.sb_mstr.sb_desc, false as status  " _
                            & "FROM " _
                            & "  public.glt_det " _
                            & "  INNER JOIN public.en_mstr ON (public.glt_det.glt_en_id = public.en_mstr.en_id) " _
                            & "  INNER JOIN public.cu_mstr ON (public.glt_det.glt_cu_id = public.cu_mstr.cu_id) " _
                            & "  INNER JOIN public.ac_mstr ON (public.glt_det.glt_ac_id = public.ac_mstr.ac_id) " _
                            & "  left outer JOIN public.cc_mstr ON (public.glt_det.glt_cc_id = public.cc_mstr.cc_id) " _
                            & "  left outer JOIN public.sb_mstr ON (public.glt_det.glt_sb_id = public.sb_mstr.sb_id)" _
                            & "  left outer JOIN public.tran_mstr ON (public.glt_det.glt_ref_tran_id = public.tran_mstr.tran_id)" _
                            & " where glt_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                            & " and glt_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                            & " and glt_posted ~~* 'n'" _
                            & " and glt_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " order by glt_code, glt_seq "
                            .InitializeCommand()
                            .FillDataSet(ds, "unposted")
                            gc_unposted.DataSource = ds.Tables("unposted")
                        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                            .SQL = "SELECT  " _
                            & "  public.glt_det.glt_oid, " _
                            & "  public.glt_det.glt_dom_id, " _
                            & "  public.glt_det.glt_en_id, " _
                            & "  public.en_mstr.en_desc, " _
                            & "  public.glt_det.glt_add_by, " _
                            & "  public.glt_det.glt_add_date, " _
                            & "  public.glt_det.glt_upd_by, " _
                            & "  public.glt_det.glt_upd_date, " _
                            & "  public.glt_det.glt_gl_oid, " _
                            & "  public.glt_det.glt_code, " _
                            & "  public.glt_det.glt_date, " _
                            & "  public.glt_det.glt_type, " _
                            & "  public.glt_det.glt_cu_id, " _
                            & "  public.glt_det.glt_exc_rate, " _
                            & "  public.glt_det.glt_seq, " _
                            & "  public.glt_det.glt_ac_id, " _
                            & "  public.glt_det.glt_sb_id,coalesce(glt_check_status,'N') as glt_check_status, " _
                            & "  public.glt_det.glt_cc_id, " _
                            & "  public.glt_det.glt_desc, " _
                            & "  public.glt_det.glt_debit, " _
                            & "  public.glt_det.glt_credit, " _
                            & "  public.glt_det.glt_debit * public.glt_det.glt_exc_rate as glt_ext_debit, " _
                            & "  public.glt_det.glt_credit * public.glt_det.glt_exc_rate as glt_ext_credit, " _
                            & "  public.glt_det.glt_posted, " _
                            & "  public.glt_det.glt_dt, " _
                            & "  public.glt_det.glt_is_reverse, " _
                            & "  public.tran_mstr.tran_name, " _
                            & "  public.glt_det.glt_ref_trans_code, glt_daybook," _
                            & "  public.cu_mstr.cu_name, " _
                            & "  public.ac_mstr.ac_code, " _
                            & "  public.ac_mstr.ac_name, " _
                            & "  public.cc_mstr.cc_desc, " _
                            & "  public.sb_mstr.sb_desc " _
                            & "FROM " _
                            & "  public.glt_det " _
                            & "  INNER JOIN public.en_mstr ON (public.glt_det.glt_en_id = public.en_mstr.en_id) " _
                            & "  INNER JOIN public.cu_mstr ON (public.glt_det.glt_cu_id = public.cu_mstr.cu_id) " _
                            & "  INNER JOIN public.ac_mstr ON (public.glt_det.glt_ac_id = public.ac_mstr.ac_id) " _
                            & "  left outer JOIN public.cc_mstr ON (public.glt_det.glt_cc_id = public.cc_mstr.cc_id) " _
                            & "  left outer JOIN public.sb_mstr ON (public.glt_det.glt_sb_id = public.sb_mstr.sb_id)" _
                            & "  left outer JOIN public.tran_mstr ON (public.glt_det.glt_ref_tran_id = public.tran_mstr.tran_id)" _
                            & " where glt_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                            & " and glt_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                            & " and glt_posted ~~* 'y'" _
                            & " and glt_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " order by glt_code, glt_seq "
                            .InitializeCommand()
                            .FillDataSet(ds, "posted")
                            gc_posted.DataSource = ds.Tables("posted")
                        ElseIf xtc_master.SelectedTabPageIndex = 2 Then
                            '.SQL = "SELECT  " _
                            '& "  public.glt_det.glt_oid, " _
                            '& "  public.glt_det.glt_dom_id, " _
                            '& "  public.glt_det.glt_en_id, " _
                            '& "  public.en_mstr.en_desc, " _
                            '& "  public.glt_det.glt_add_by, " _
                            '& "  public.glt_det.glt_add_date, " _
                            '& "  public.glt_det.glt_upd_by, " _
                            '& "  public.glt_det.glt_upd_date, " _
                            '& "  public.glt_det.glt_gl_oid, " _
                            '& "  public.glt_det.glt_code, " _
                            '& "  public.glt_det.glt_date,coalesce(glt_check_status,'N') as glt_check_status, " _
                            '& "  public.glt_det.glt_type, " _
                            '& "  public.glt_det.glt_cu_id, " _
                            '& "  public.glt_det.glt_exc_rate, " _
                            '& "  public.glt_det.glt_seq, " _
                            '& "  public.glt_det.glt_ac_id, " _
                            '& "  public.glt_det.glt_sb_id, " _
                            '& "  public.glt_det.glt_cc_id, " _
                            '& "  public.glt_det.glt_desc, " _
                            '& "  public.glt_det.glt_debit, " _
                            '& "  public.glt_det.glt_credit, " _
                            '& "  public.glt_det.glt_debit * public.glt_det.glt_exc_rate as glt_ext_debit, " _
                            '& "  public.glt_det.glt_credit * public.glt_det.glt_exc_rate as glt_ext_credit, " _
                            '& "  public.glt_det.glt_posted, " _
                            '& "  public.glt_det.glt_dt, " _
                            '& "  public.glt_det.glt_is_reverse, " _
                            '& "  public.tran_mstr.tran_name, " _
                            '& "  public.glt_det.glt_ref_trans_code, glt_daybook," _
                            '& "  public.cu_mstr.cu_name, " _
                            '& "  public.ac_mstr.ac_code, " _
                            '& "  public.ac_mstr.ac_name, " _
                            '& "  public.cc_mstr.cc_desc, " _
                            '& "  public.sb_mstr.sb_desc " _
                            '& "FROM " _
                            '& "  public.glt_det " _
                            '& "  INNER JOIN public.en_mstr ON (public.glt_det.glt_en_id = public.en_mstr.en_id) " _
                            '& "  INNER JOIN public.cu_mstr ON (public.glt_det.glt_cu_id = public.cu_mstr.cu_id) " _
                            '& "  INNER JOIN public.ac_mstr ON (public.glt_det.glt_ac_id = public.ac_mstr.ac_id) " _
                            '& "  left outer JOIN public.cc_mstr ON (public.glt_det.glt_cc_id = public.cc_mstr.cc_id) " _
                            '& "  left outer JOIN public.sb_mstr ON (public.glt_det.glt_sb_id = public.sb_mstr.sb_id)" _
                            '& "  left outer JOIN public.tran_mstr ON (public.glt_det.glt_ref_tran_id = public.tran_mstr.tran_id)" _
                            '& " where glt_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                            '& " and glt_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                            '& " and glt_en_id in (select user_en_id from tconfuserentity " _
                            '           & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            '& " order by glt_code, glt_seq "


                            .SQL = "SELECT  " _
                                & "  public.en_mstr.en_desc, " _
                                & "  public.glt_det.glt_code, " _
                                & "  public.glt_det.glt_date, " _
                                & "  public.glt_det.glt_type, " _
                                & "  public.glt_det.glt_exc_rate, " _
                                & "  public.glt_det.glt_desc, " _
                                & "  public.glt_det.glt_debit, " _
                                & "  public.glt_det.glt_credit, " _
                                & "  public.glt_det.glt_debit * public.glt_det.glt_exc_rate as glt_ext_debit, " _
                                & "  public.glt_det.glt_credit * public.glt_det.glt_exc_rate as glt_ext_credit, " _
                                & "  public.glt_det.glt_posted, " _
                                & "  public.glt_det.glt_is_reverse, " _
                                & "  public.tran_mstr.tran_name, " _
                                & "  public.glt_det.glt_ref_trans_code, glt_daybook, " _
                                & "  public.cu_mstr.cu_name, " _
                                & "  public.ac_mstr.ac_code, " _
                                & "  public.ac_mstr.ac_name, " _
                                & "  public.cc_mstr.cc_desc, " _
                                & "  public.sb_mstr.sb_desc, " _
                                & "  public.glt_det.glt_add_by, " _
                                & "  public.glt_det.glt_add_date, " _
                                & "  public.glt_det.glt_upd_by, " _
                                & "  public.glt_det.glt_upd_date " _
                                & "FROM " _
                                & "  public.glt_det " _
                                & "  INNER JOIN public.en_mstr ON (public.glt_det.glt_en_id = public.en_mstr.en_id) " _
                                & "  INNER JOIN public.cu_mstr ON (public.glt_det.glt_cu_id = public.cu_mstr.cu_id) " _
                                & "  INNER JOIN public.ac_mstr ON (public.glt_det.glt_ac_id = public.ac_mstr.ac_id) " _
                                & "  left outer JOIN public.cc_mstr ON (public.glt_det.glt_cc_id = public.cc_mstr.cc_id) " _
                                & "  left outer JOIN public.sb_mstr ON (public.glt_det.glt_sb_id = public.sb_mstr.sb_id)" _
                                & "  left outer JOIN public.tran_mstr ON (public.glt_det.glt_ref_tran_id = public.tran_mstr.tran_id)" _
                                & " where glt_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                                & " and glt_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                                & " and glt_en_id in (select user_en_id from tconfuserentity " _
                                           & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                & " order by glt_code, glt_seq"


                            .InitializeCommand()
                            .FillDataSet(ds, "all")
                            gc_all.DataSource = ds.Tables("all")
                        End If

                        bestfit_column()
                        ConditionsAdjustment()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

    Private Sub xtc_master_SelectedPageChanged(ByVal sender As Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles xtc_master.SelectedPageChanged
        If xtc_master.SelectedTabPageIndex = 0 Then
            sb_posting.Enabled = True
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            sb_posting.Enabled = False
        End If
        load_data_many(True)
    End Sub

    'Private progressForm As master_new.FProgress = Nothing

    Private Sub sb_posting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_posting.Click
        Dim i As Integer
        Dim _status As Boolean = False
        Dim _date As Date
        Dim _ac_id, _en_id, _cu_id As Integer
        Dim _sb_id, _cc_id As String ' diset setring agar bisa diberi nilai null
        Dim _glt_value As Double
        Dim _ac_sign As String = ""
        Dim _glt_exc_rate As Double


        'gv_unposted.UpdateCurrentRow()
        'For i = 0 To ds.Tables("unposted").Rows.Count - 1
        '    If ds.Tables("unposted").Rows(i).Item("status") = True Then
        '        _status = True
        '    End If
        'Next

        'If _status = False Then
        '    MessageBox.Show("No Data Selected...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End If

        If MessageBox.Show("Posting Selected Transaction...", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Try
            If CeMassive.Checked = True Then

                Dim ssqls As New ArrayList
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran
                            .Command.CommandTimeout = 480

                            Dim _day As Integer
                            _day = DateDiff(DateInterval.Day, pr_txttglawal.DateTime.Date, pr_txttglakhir.DateTime.Date)

                            For x As Integer = 0 To _day
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "UPDATE  " _
                                                    & "  public.glt_det   " _
                                                    & "SET  " _
                                                    & "  glt_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "  glt_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                                    & "  glt_posted = 'Y',  " _
                                                    & "  glt_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                                    & "  " _
                                                   & " where glt_date = " + SetDate(DateAdd(DateInterval.Day, x, pr_txttglawal.DateTime.Date)) _
                                                & " and glt_posted = 'N'" _
                                                & " and glt_en_id in (select user_en_id from tconfuserentity " _
                                                           & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"



                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                                LblStatus.Text = x & " of " & _day & " " & DateAdd(DateInterval.Day, x, pr_txttglawal.DateTime.Date).ToString("dd/MM/yyyy")
                                Windows.Forms.Application.DoEvents()
                            Next
                            


                            If master_new.PGSqlConn.status_sync = True Then
                                For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = Data
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                    'i += 1
                                    'LblStatus.Text = i & " of " & _number
                                    'Windows.Forms.Application.DoEvents()
                                Next
                            End If

                            sqlTran.Commit()
                            ssqls.Clear()

                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                        End Try
                    End With
                End Using

                load_data_many(True)
                MessageBox.Show("Transactions Have Been Posted..", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Else
                Dim nomor_gl As String = ""



                'progressForm = New master_new.FProgress(FMainMenu)
                'progressForm.Show()


                Dim _number As Double = ds.Tables("unposted").Rows.Count - 1

                For i = 0 To ds.Tables("unposted").Rows.Count - 1
                    If ds.Tables("unposted").Rows(i).Item("status") = True Then
                        If nomor_gl <> ds.Tables("unposted").Rows(i).Item("glt_code") Then
                            nomor_gl = ds.Tables("unposted").Rows(i).Item("glt_code")

                            Dim x As Integer
                            Dim ssqls As New ArrayList

                            Using objinsert As New master_new.WDABasepgsql("", "")
                                With objinsert
                                    .Connection.Open()
                                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                                    Try
                                        .Command = .Connection.CreateCommand
                                        .Command.Transaction = sqlTran
                                        .Command.CommandTimeout = 480

                                        For x = i To ds.Tables("unposted").Rows.Count - 1
                                            If ds.Tables("unposted").Rows(x).Item("glt_code") = nomor_gl Then

                                                .Command.CommandType = CommandType.Text
                                                .Command.CommandText = "UPDATE  " _
                                                                    & "  public.glt_det   " _
                                                                    & "SET  " _
                                                                    & "  glt_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                                    & "  glt_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                                                    & "  glt_posted = 'Y',  " _
                                                                    & "  glt_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                                                    & "  " _
                                                                    & "WHERE  " _
                                                                    & "  glt_oid = " & SetSetring(ds.Tables("unposted").Rows(x).Item("glt_oid").ToString) & " "
                                                ssqls.Add(.Command.CommandText)
                                                .Command.ExecuteNonQuery()
                                                .Command.Parameters.Clear()

                                                _date = ds.Tables("unposted").Rows(x).Item("glt_date")
                                                _ac_id = ds.Tables("unposted").Rows(x).Item("glt_ac_id")
                                                _sb_id = SetIntegerDB(ds.Tables("unposted").Rows(x).Item("glt_sb_id"))
                                                _cc_id = SetIntegerDB(ds.Tables("unposted").Rows(x).Item("glt_cc_id"))
                                                _en_id = ds.Tables("unposted").Rows(x).Item("glt_en_id")
                                                _cu_id = ds.Tables("unposted").Rows(x).Item("glt_cu_id")
                                                _glt_exc_rate = ds.Tables("unposted").Rows(x).Item("glt_exc_rate")

                                                If ds.Tables("unposted").Rows(x).Item("glt_debit") <> 0 Then
                                                    _glt_value = ds.Tables("unposted").Rows(x).Item("glt_debit")
                                                    _ac_sign = "D"
                                                ElseIf ds.Tables("unposted").Rows(x).Item("glt_credit") <> 0 Then
                                                    _glt_value = ds.Tables("unposted").Rows(x).Item("glt_credit")
                                                    _ac_sign = "C"
                                                End If
                                                If func_coll.update_posted_glbal_balance(ssqls, objinsert, _date, _ac_id, _sb_id, _cc_id, _en_id, _cu_id, _glt_exc_rate, _glt_value, _ac_sign) = False Then
                                                    sqlTran.Rollback()
                                                    Exit Sub
                                                End If



                                            Else


                                                Exit For
                                            End If
                                        Next

                                        'beda nomor
                                        'i = 0


                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = insert_log("Posting GL " & ds.Tables("unposted").Rows(i).Item("glt_code"))
                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()

                                        If master_new.PGSqlConn.status_sync = True Then
                                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                                .Command.CommandType = CommandType.Text
                                                .Command.CommandText = Data
                                                .Command.ExecuteNonQuery()
                                                .Command.Parameters.Clear()
                                                'i += 1
                                                'LblStatus.Text = i & " of " & _number
                                                Windows.Forms.Application.DoEvents()
                                            Next
                                        End If

                                        sqlTran.Commit()
                                        ssqls.Clear()

                                    Catch ex As nPgSqlException
                                        sqlTran.Rollback()
                                        MessageBox.Show(ex.Message)
                                    End Try
                                End With
                            End Using

                        End If

                    End If
                    'progressForm.SetProgressValue(i / _number * 100)
                    LblStatus.Text = i + 1 & " of " & _number + 1
                    Windows.Forms.Application.DoEvents()
                Next



                'progressForm.Dispose()
                'progressForm = Nothing

                'i = 0
                'If master_new.PGSqlConn.status_sync = True Then
                '    For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                '        .Command.CommandType = CommandType.Text
                '        .Command.CommandText = Data
                '        .Command.ExecuteNonQuery()
                '        .Command.Parameters.Clear()
                '        i += 1
                '        LblStatus.Text = i & " of " & _number
                '        Windows.Forms.Application.DoEvents()
                '    Next
                'End If

                'sqlTran.Commit()

                load_data_many(True)
                MessageBox.Show("Transactions Have Been Posted..", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information)



            End If


            'Using objinsert As New master_new.WDABasepgsql("", "")
            '    With objinsert
            '        .Connection.Open()
            '        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
            '        Try
            '            .Command = .Connection.CreateCommand
            '            .Command.Transaction = sqlTran

            '            'progressForm = New master_new.FProgress(FMainMenu)
            '            'progressForm.Show()
            '            Dim _number As Double = ds.Tables("unposted").Rows.Count - 1

            '            For i = 0 To ds.Tables("unposted").Rows.Count - 1
            '                If ds.Tables("unposted").Rows(i).Item("status") = True Then
            '                    If nomor_gl <> ds.Tables("unposted").Rows(i).Item("glt_code") Then
            '                        nomor_gl = ds.Tables("unposted").Rows(i).Item("glt_code")
            '                        'beda nomor
            '                        i = 0
            '                        If master_new.PGSqlConn.status_sync = True Then
            '                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
            '                                .Command.CommandType = CommandType.Text
            '                                .Command.CommandText = Data
            '                                .Command.ExecuteNonQuery()
            '                                .Command.Parameters.Clear()
            '                                i += 1
            '                                LblStatus.Text = i & " of " & _number
            '                                Windows.Forms.Application.DoEvents()
            '                            Next
            '                        End If

            '                        sqlTran.Commit()
            '                        ssqls.Clear()

            '                    End If

            '                    .Command.CommandType = CommandType.Text
            '                    .Command.CommandText = "UPDATE  " _
            '                                        & "  public.glt_det   " _
            '                                        & "SET  " _
            '                                        & "  glt_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
            '                                        & "  glt_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
            '                                        & "  glt_posted = 'Y',  " _
            '                                        & "  glt_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
            '                                        & "  " _
            '                                        & "WHERE  " _
            '                                        & "  glt_oid = " & SetSetring(ds.Tables("unposted").Rows(i).Item("glt_oid").ToString) & " "
            '                    ssqls.Add(.Command.CommandText)
            '                    .Command.ExecuteNonQuery()
            '                    .Command.Parameters.Clear()

            '                    _date = ds.Tables("unposted").Rows(i).Item("glt_date")
            '                    _ac_id = ds.Tables("unposted").Rows(i).Item("glt_ac_id")
            '                    _sb_id = SetIntegerDB(ds.Tables("unposted").Rows(i).Item("glt_sb_id"))
            '                    _cc_id = SetIntegerDB(ds.Tables("unposted").Rows(i).Item("glt_cc_id"))
            '                    _en_id = ds.Tables("unposted").Rows(i).Item("glt_en_id")
            '                    _cu_id = ds.Tables("unposted").Rows(i).Item("glt_cu_id")
            '                    _glt_exc_rate = ds.Tables("unposted").Rows(i).Item("glt_exc_rate")

            '                    If ds.Tables("unposted").Rows(i).Item("glt_debit") <> 0 Then
            '                        _glt_value = ds.Tables("unposted").Rows(i).Item("glt_debit")
            '                        _ac_sign = "D"
            '                    ElseIf ds.Tables("unposted").Rows(i).Item("glt_credit") <> 0 Then
            '                        _glt_value = ds.Tables("unposted").Rows(i).Item("glt_credit")
            '                        _ac_sign = "C"
            '                    End If
            '                    If func_coll.update_posted_glbal_balance(ssqls, objinsert, _date, _ac_id, _sb_id, _cc_id, _en_id, _cu_id, _glt_exc_rate, _glt_value, _ac_sign) = False Then
            '                        sqlTran.Rollback()
            '                        Exit Sub
            '                    End If


            '                    .Command.CommandType = CommandType.Text
            '                    .Command.CommandText = insert_log("Posting GL " & ds.Tables("unposted").Rows(i).Item("glt_code"))
            '                    ssqls.Add(.Command.CommandText)
            '                    .Command.ExecuteNonQuery()
            '                    .Command.Parameters.Clear()

            '                End If
            '                'progressForm.SetProgressValue(i / _number * 100)
            '                LblStatus.Text = i & " of " & _number
            '                Windows.Forms.Application.DoEvents()
            '            Next



            '            'progressForm.Dispose()
            '            'progressForm = Nothing

            '            'i = 0
            '            'If master_new.PGSqlConn.status_sync = True Then
            '            '    For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
            '            '        .Command.CommandType = CommandType.Text
            '            '        .Command.CommandText = Data
            '            '        .Command.ExecuteNonQuery()
            '            '        .Command.Parameters.Clear()
            '            '        i += 1
            '            '        LblStatus.Text = i & " of " & _number
            '            '        Windows.Forms.Application.DoEvents()
            '            '    Next
            '            'End If

            '            'sqlTran.Commit()

            '            load_data_many(True)
            '            MessageBox.Show("Transactions Have Been Posted..", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        Catch ex As nPgSqlException
            '            sqlTran.Rollback()
            '            MessageBox.Show(ex.Message)
            '        End Try
            '    End With
            'End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub gv_unposted_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_unposted.CellValueChanged
        If e.Column.Name = "status" Then
            Dim _status As Boolean
            Dim _glt_code As String
            Dim i As Integer

            _status = e.Value
            _glt_code = (gv_unposted.GetRowCellValue(e.RowHandle, "glt_code"))

            For i = 0 To ds.Tables("unposted").Rows.Count - 1
                If ds.Tables("unposted").Rows(i).Item("glt_code") = _glt_code Then
                    ds.Tables("unposted").Rows(i).Item("status") = _status

                    LblStatus.Text = i & " of " & ds.Tables("unposted").Rows.Count - 1
                    Windows.Forms.Application.DoEvents()
                End If
            Next
            ds.Tables("unposted").AcceptChanges()
        End If
    End Sub
    Private Sub ce_all_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ce_all.CheckedChanged
        Dim i As Integer

        If ce_all.Checked = True Then
            For i = 0 To ds.Tables("unposted").Rows.Count - 1
                ds.Tables("unposted").Rows(i).Item("status") = True
            Next
        ElseIf ce_all.Checked = False Then
            For i = 0 To ds.Tables("unposted").Rows.Count - 1
                ds.Tables("unposted").Rows(i).Item("status") = False
            Next
        End If

        ds.AcceptChanges()
        gv_unposted.BestFitColumns()


        'For i = 0 To gv_unposted.RowCount - 1
        '    gv_unposted.SetRowCellValue(i, "status", ce_all.EditValue)
        'Next
        'ds.Tables("unposted").AcceptChanges()

    End Sub

    Public Overrides Sub preview()
        'Dim _en_id As Integer
        'Dim _type, _table, _initial, _code_awal, _code_akhir As String

        '_en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_en_id")
        '_type = 10
        '_table = "so_mstr"
        '_initial = "so"
        '_code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code")
        '_code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code")

        'func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        Dim ds_bantu As New DataSet
        Dim _sql As String
        Dim _glt_code As String = ""

        If xtc_master.SelectedTabPageIndex = 0 Then
            _glt_code = ds.Tables("unposted").Rows(BindingContext(ds.Tables("unposted")).Position).Item("glt_code")
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            _glt_code = ds.Tables("posted").Rows(BindingContext(ds.Tables("posted")).Position).Item("glt_code")
        ElseIf xtc_master.SelectedTabPageIndex = 2 Then
            _glt_code = ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("glt_code")
        End If

        _sql = "SELECT  " _
             & "glt_det.glt_oid, " _
             & "glt_det.glt_dom_id, " _
             & "glt_det.glt_en_id, " _
             & "en_mstr.en_desc, " _
             & "glt_det.glt_add_by, " _
             & "glt_det.glt_add_date, " _
             & "glt_det.glt_upd_by, " _
             & "glt_det.glt_upd_date, " _
             & "glt_det.glt_gl_oid, " _
             & "glt_det.glt_code, " _
             & "glt_det.glt_date, " _
             & "glt_det.glt_type, " _
             & "glt_det.glt_cu_id, " _
             & "glt_det.glt_exc_rate, " _
             & "glt_det.glt_seq, " _
             & "glt_det.glt_ac_id, " _
             & "glt_det.glt_sb_id, " _
             & "glt_det.glt_cc_id, " _
             & "glt_det.glt_desc, " _
             & "glt_det.glt_debit, " _
             & "glt_det.glt_credit, " _
             & "glt_det.glt_debit * glt_det.glt_exc_rate as glt_ext_debit, " _
             & "glt_det.glt_credit * glt_det.glt_exc_rate as glt_ext_credit, " _
             & "glt_det.glt_posted, " _
             & "glt_det.glt_dt, " _
             & "tran_mstr.tran_name, " _
             & "glt_det.glt_ref_trans_code, " _
             & "glt_det.glt_daybook, " _
             & "cu_mstr.cu_name, " _
             & "ac_mstr.ac_code, " _
             & "ac_mstr.ac_name, " _
             & "cc_mstr.cc_desc, " _
             & "sb_mstr.sb_desc, " _
             & "cmaddr_name, " _
             & "cmaddr_line_1, " _
             & "cmaddr_line_2, " _
             & "cmaddr_line_3 " _
             & "FROM " _
             & "glt_det " _
             & "INNER JOIN en_mstr ON (glt_det.glt_en_id = en_mstr.en_id) " _
             & "INNER JOIN cu_mstr ON (glt_det.glt_cu_id = cu_mstr.cu_id) " _
             & "INNER JOIN ac_mstr ON (glt_det.glt_ac_id = ac_mstr.ac_id) " _
             & "LEFT OUTER JOIN tran_mstr ON (glt_det.glt_ref_tran_id = tran_mstr.tran_id) " _
             & "left outer join cc_mstr ON (glt_det.glt_cc_id = cc_mstr.cc_id) " _
             & "left outer join sb_mstr ON (glt_det.glt_sb_id = sb_mstr.sb_id)  " _
             & "inner join cmaddr_mstr on cmaddr_en_id = glt_en_id " _
             & "WHERE " _
             & "glt_code ~~* '" + _glt_code + "'"

        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRGeneralLedgerPrint"
        frm._remarks = _glt_code
        frm.ShowDialog()
    End Sub

    Public Overrides Function export_data() As Boolean
        If xtc_master.SelectedTabPageIndex = 0 Then

            Dim ssql As String
            Try
                'ssql = "SELECT  " _
                '        & "  public.glt_det.glt_oid, " _
                '        & "  public.glt_det.glt_dom_id, " _
                '        & "  public.glt_det.glt_en_id, " _
                '        & "  public.en_mstr.en_desc, " _
                '        & "  public.glt_det.glt_add_by, " _
                '        & "  public.glt_det.glt_add_date, " _
                '        & "  public.glt_det.glt_upd_by, " _
                '        & "  public.glt_det.glt_upd_date, " _
                '        & "  public.glt_det.glt_gl_oid, " _
                '        & "  public.glt_det.glt_code, " _
                '        & "  public.glt_det.glt_date, " _
                '        & "  public.glt_det.glt_type, " _
                '        & "  public.glt_det.glt_cu_id, " _
                '        & "  public.glt_det.glt_exc_rate, " _
                '        & "  public.glt_det.glt_seq, " _
                '        & "  public.glt_det.glt_ac_id, " _
                '        & "  public.glt_det.glt_sb_id, " _
                '        & "  public.glt_det.glt_cc_id, " _
                '        & "  public.glt_det.glt_desc, " _
                '        & "  public.glt_det.glt_debit, " _
                '        & "  public.glt_det.glt_credit, " _
                '        & "  public.glt_det.glt_debit * public.glt_det.glt_exc_rate as glt_ext_debit, " _
                '        & "  public.glt_det.glt_credit * public.glt_det.glt_exc_rate as glt_ext_credit, " _
                '        & "  public.glt_det.glt_posted, " _
                '        & "  public.glt_det.glt_dt, " _
                '        & "  public.glt_det.glt_is_reverse, " _
                '        & "  public.tran_mstr.tran_name, " _
                '        & "  public.glt_det.glt_ref_trans_code, glt_daybook, " _
                '        & "  public.cu_mstr.cu_name, " _
                '        & "  public.ac_mstr.ac_code, " _
                '        & "  public.ac_mstr.ac_name, " _
                '        & "  public.cc_mstr.cc_desc, " _
                '        & "  public.sb_mstr.sb_desc, false as status  " _
                '        & "FROM " _
                '        & "  public.glt_det " _
                '        & "  INNER JOIN public.en_mstr ON (public.glt_det.glt_en_id = public.en_mstr.en_id) " _
                '        & "  INNER JOIN public.cu_mstr ON (public.glt_det.glt_cu_id = public.cu_mstr.cu_id) " _
                '        & "  INNER JOIN public.ac_mstr ON (public.glt_det.glt_ac_id = public.ac_mstr.ac_id) " _
                '        & "  left outer JOIN public.cc_mstr ON (public.glt_det.glt_cc_id = public.cc_mstr.cc_id) " _
                '        & "  left outer JOIN public.sb_mstr ON (public.glt_det.glt_sb_id = public.sb_mstr.sb_id)" _
                '        & "  left outer JOIN public.tran_mstr ON (public.glt_det.glt_ref_tran_id = public.tran_mstr.tran_id)" _
                '        & " where glt_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                '        & " and glt_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                '        & " and glt_posted ~~* 'n' " _
                '        & " and glt_en_id in (select user_en_id from tconfuserentity " _
                '                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                '        & " order by glt_code, ac_code "

                Dim frm As New frmExport
                Dim _file As String = AskSaveAsFile("Excel Files | *.xls")

                'With frm
                '    add_column_edit(.gv_export, "Status", "status", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "GL Number", "glt_code", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Date", "glt_date", DevExpress.Utils.HorzAlignment.Center)
                '    add_column_copy(.gv_export, "Type", "glt_type", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Exchange Rate", "glt_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
                '    add_column_copy(.gv_export, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Description", "glt_desc", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Debit", "glt_debit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
                '    add_column_copy(.gv_export, "Credit", "glt_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
                '    add_column_copy(.gv_export, "Ext Debit", "glt_ext_debit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
                '    add_column_copy(.gv_export, "Ext Credit", "glt_ext_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
                '    add_column_copy(.gv_export, "Ref. Number", "glt_ref_trans_code", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Daybook", "glt_daybook", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Posted", "glt_posted", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Is Reverse", "glt_is_reverse", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "User Create", "glt_add_by", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Date Create", "glt_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
                '    add_column_copy(.gv_export, "User Update", "glt_upd_by", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Date Update", "glt_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

                '    .gc_export.DataSource = master_new.PGSqlConn.GetTableData(ssql)
                '    .gv_export.BestFitColumns()
                '    .gv_export.ExportToXls(_file)
                'End With

                'frm.Dispose()

                'gv_unposted.Columns("glt_debit").DisplayFormat.Reset()
                'gv_unposted.Columns("glt_credit").DisplayFormat.Reset()
                'gv_unposted.Columns("glt_ext_debit").DisplayFormat.Reset()
                'gv_unposted.Columns("glt_ext_credit").DisplayFormat.Reset()

                gv_unposted.ExportToExcelOld(_file)


                'gv_unposted.Columns("glt_debit").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                'gv_unposted.Columns("glt_debit").DisplayFormat.FormatString = "n"
                'gv_unposted.Columns("glt_credit").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                'gv_unposted.Columns("glt_credit").DisplayFormat.FormatString = "n"
                'gv_unposted.Columns("glt_ext_debit").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                'gv_unposted.Columns("glt_ext_debit").DisplayFormat.FormatString = "n"
                'gv_unposted.Columns("glt_ext_credit").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                'gv_unposted.Columns("glt_ext_credit").DisplayFormat.FormatString = "n"



                Box("Export data sucess")

                OpenFile(_file)

            Catch ex As Exception
                Pesan(Err)
                Return False
            End Try
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then

            Dim ssql As String
            Try
                'ssql = "SELECT  " _
                '        & "  public.en_mstr.en_desc, " _
                '        & "  public.glt_det.glt_add_by, " _
                '        & "  public.glt_det.glt_add_date, " _
                '        & "  public.glt_det.glt_upd_by, " _
                '        & "  public.glt_det.glt_upd_date, " _
                '        & "  public.glt_det.glt_code, " _
                '        & "  public.glt_det.glt_date, " _
                '        & "  public.glt_det.glt_type, " _
                '        & "  public.glt_det.glt_exc_rate, " _
                '        & "  public.glt_det.glt_desc, " _
                '        & "  public.glt_det.glt_debit, " _
                '        & "  public.glt_det.glt_credit, " _
                '        & "  public.glt_det.glt_debit * public.glt_det.glt_exc_rate as glt_ext_debit, " _
                '        & "  public.glt_det.glt_credit * public.glt_det.glt_exc_rate as glt_ext_credit, " _
                '        & "  public.glt_det.glt_posted, " _
                '        & "  public.glt_det.glt_is_reverse, " _
                '        & "  public.tran_mstr.tran_name, " _
                '        & "  public.glt_det.glt_ref_trans_code, " _
                '        & "  public.cu_mstr.cu_name, " _
                '        & "  public.ac_mstr.ac_code, " _
                '        & "  public.ac_mstr.ac_name, " _
                '        & "  public.cc_mstr.cc_desc, " _
                '        & "  public.sb_mstr.sb_desc " _
                '        & "FROM " _
                '        & "  public.glt_det " _
                '        & "  INNER JOIN public.en_mstr ON (public.glt_det.glt_en_id = public.en_mstr.en_id) " _
                '        & "  INNER JOIN public.cu_mstr ON (public.glt_det.glt_cu_id = public.cu_mstr.cu_id) " _
                '        & "  INNER JOIN public.ac_mstr ON (public.glt_det.glt_ac_id = public.ac_mstr.ac_id) " _
                '        & "  left outer JOIN public.cc_mstr ON (public.glt_det.glt_cc_id = public.cc_mstr.cc_id) " _
                '        & "  left outer JOIN public.sb_mstr ON (public.glt_det.glt_sb_id = public.sb_mstr.sb_id)" _
                '        & "  left outer JOIN public.tran_mstr ON (public.glt_det.glt_ref_tran_id = public.tran_mstr.tran_id)" _
                '        & " where glt_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                '        & " and glt_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                '        & " and glt_posted ~~* 'y'" _
                '        & " and glt_en_id in (select user_en_id from tconfuserentity " _
                '                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                '        & " order by glt_code, ac_code "

                'Dim frm As New frmExport
                Dim _file As String = AskSaveAsFile("Excel Files | *.xls")

                'With frm
                '    add_column_copy(.gv_export, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "GL Number", "glt_code", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Date", "glt_date", DevExpress.Utils.HorzAlignment.Center)
                '    add_column_copy(.gv_export, "Type", "glt_type", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Exchange Rate", "glt_exc_rate", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Description", "glt_desc", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Debit", "glt_debit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
                '    add_column_copy(.gv_export, "Credit", "glt_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
                '    add_column_copy(.gv_export, "Ext Debit", "glt_ext_debit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
                '    add_column_copy(.gv_export, "Ext Credit", "glt_ext_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
                '    add_column_copy(.gv_export, "Transaction", "tran_name", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Ref. Number", "glt_ref_trans_code", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Daybook", "glt_daybook", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Posted", "glt_posted", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Is Reverse", "glt_is_reverse", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "User Create", "glt_add_by", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Date Create", "glt_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
                '    add_column_copy(.gv_export, "User Update", "glt_upd_by", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Date Update", "glt_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")


                '    .gc_export.DataSource = master_new.PGSqlConn.GetTableData(ssql)
                '    .gv_export.BestFitColumns()
                '    .gv_export.ExportToXls(_file)
                'End With

                'frm.Dispose()

                'gv_posted.Columns("glt_debit").DisplayFormat.Reset()
                'gv_posted.Columns("glt_credit").DisplayFormat.Reset()
                'gv_posted.Columns("glt_ext_debit").DisplayFormat.Reset()
                'gv_posted.Columns("glt_ext_credit").DisplayFormat.Reset()

                gv_posted.ExportToExcelOld(_file)

                'gv_posted.Columns("glt_debit").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                'gv_posted.Columns("glt_debit").DisplayFormat.FormatString = "n"
                'gv_posted.Columns("glt_credit").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                'gv_posted.Columns("glt_credit").DisplayFormat.FormatString = "n"
                'gv_posted.Columns("glt_ext_debit").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                'gv_posted.Columns("glt_ext_debit").DisplayFormat.FormatString = "n"
                'gv_posted.Columns("glt_ext_credit").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                'gv_posted.Columns("glt_ext_credit").DisplayFormat.FormatString = "n"

                Box("Export data sucess")

                OpenFile(_file)

            Catch ex As Exception
                Pesan(Err)
                Return False
            End Try
        ElseIf xtc_master.SelectedTabPageIndex = 2 Then

            'If DevExpress.XtraEditors.XtraMessageBox.Show("Format export CSV ?", "Format Export..", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
            '    csv4()

            'Else

            Dim ssql As String
            Dim _file As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls")

            Try
                'ssql = "SELECT  " _
                '        & "  public.glt_det.glt_oid, " _
                '        & "  public.glt_det.glt_dom_id, " _
                '        & "  public.glt_det.glt_en_id, " _
                '        & "  public.en_mstr.en_desc, " _
                '        & "  public.glt_det.glt_add_by, " _
                '        & "  public.glt_det.glt_add_date, " _
                '        & "  public.glt_det.glt_upd_by, " _
                '        & "  public.glt_det.glt_upd_date, " _
                '        & "  public.glt_det.glt_gl_oid, " _
                '        & "  public.glt_det.glt_code, " _
                '        & "  public.glt_det.glt_date, " _
                '        & "  public.glt_det.glt_type, " _
                '        & "  public.glt_det.glt_cu_id, " _
                '        & "  public.glt_det.glt_exc_rate, " _
                '        & "  public.glt_det.glt_seq, " _
                '        & "  public.glt_det.glt_ac_id, " _
                '        & "  public.glt_det.glt_sb_id, " _
                '        & "  public.glt_det.glt_cc_id, " _
                '        & "  public.glt_det.glt_desc, " _
                '        & "  public.glt_det.glt_debit, " _
                '        & "  public.glt_det.glt_credit, " _
                '        & "  public.glt_det.glt_debit * public.glt_det.glt_exc_rate as glt_ext_debit, " _
                '        & "  public.glt_det.glt_credit * public.glt_det.glt_exc_rate as glt_ext_credit, " _
                '        & "  public.glt_det.glt_posted, " _
                '        & "  public.glt_det.glt_dt, " _
                '        & "  public.glt_det.glt_is_reverse, " _
                '        & "  public.tran_mstr.tran_name, " _
                '        & "  public.glt_det.glt_ref_trans_code, " _
                '        & "  public.cu_mstr.cu_name, " _
                '        & "  public.ac_mstr.ac_code, " _
                '        & "  public.ac_mstr.ac_name, " _
                '        & "  public.cc_mstr.cc_desc, " _
                '        & "  public.sb_mstr.sb_desc " _
                '        & "FROM " _
                '        & "  public.glt_det " _
                '        & "  INNER JOIN public.en_mstr ON (public.glt_det.glt_en_id = public.en_mstr.en_id) " _
                '        & "  INNER JOIN public.cu_mstr ON (public.glt_det.glt_cu_id = public.cu_mstr.cu_id) " _
                '        & "  INNER JOIN public.ac_mstr ON (public.glt_det.glt_ac_id = public.ac_mstr.ac_id) " _
                '        & "  left outer JOIN public.cc_mstr ON (public.glt_det.glt_cc_id = public.cc_mstr.cc_id) " _
                '        & "  left outer JOIN public.sb_mstr ON (public.glt_det.glt_sb_id = public.sb_mstr.sb_id)" _
                '        & "  left outer JOIN public.tran_mstr ON (public.glt_det.glt_ref_tran_id = public.tran_mstr.tran_id)" _
                '        & " where glt_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                '        & " and glt_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                '        & " and glt_en_id in (select user_en_id from tconfuserentity " _
                '                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                '        & " order by glt_code, ac_code "

                ''Dim dt As New DataTable
                ''dt = GetTableData(ssql)


                ''If export_to_excel(ssql) = False Then
                ''    Return False
                ''    Exit Function
                ''End If
                ''Exit Function
                ''ExportToExcel(dt, _file)
                ''Dim _file As String = AskSaveAsFile("Excel Files | *.xlsx")
                ''Export_excel.ExportToExcel(_file, dt)
                ''Exit Function


                'Dim frm As New frmExport
                ''Dim _file As String = AskSaveAsFile("Excel Files | *.xls")

                'With frm
                '    add_column_copy(.gv_export, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "GL Number", "glt_code", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Date", "glt_date", DevExpress.Utils.HorzAlignment.Center)
                '    add_column_copy(.gv_export, "Type", "glt_type", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Exchange Rate", "glt_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
                '    add_column_copy(.gv_export, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Description", "glt_desc", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Debit", "glt_debit", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Credit", "glt_credit", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Ext Debit", "glt_ext_debit", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Ext Credit", "glt_ext_credit", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Ref. Number", "glt_ref_trans_code", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Daybook", "glt_daybook", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Posted", "glt_posted", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Is Reverse", "glt_is_reverse", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "User Create", "glt_add_by", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Date Create", "glt_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
                '    add_column_copy(.gv_export, "User Update", "glt_upd_by", DevExpress.Utils.HorzAlignment.Default)
                '    add_column_copy(.gv_export, "Date Update", "glt_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

                '    .gc_export.DataSource = master_new.PGSqlConn.GetTableData(ssql)
                '    .gv_export.BestFitColumns()
                '    .gv_export.ExportToXls(_file)
                'End With

                'frm.Dispose()

                'gv_all.ExportToXls(_file)
                'gv_all.ExportToText(_file)

                'gv_all.Columns("glt_debit").DisplayFormat.FormatType = DevExpress.Utils.FormatType.None
                'gv_all.Columns("glt_credit").DisplayFormat.FormatType = DevExpress.Utils.FormatType.None
                'gv_all.Columns("glt_ext_debit").DisplayFormat.FormatType = DevExpress.Utils.FormatType.None
                'gv_all.Columns("glt_ext_credit").DisplayFormat.FormatType = DevExpress.Utils.FormatType.None

                'DevExpress.Utils.FormatType.Numeric, "n"
                'column = New DevExpress.XtraGrid.Columns.GridColumn

                'gv.Columns.Add(column)
                'column.Caption = par_caption
                'column.FieldName = par_fn
                'column.Name = par_fn
                'column.Visible = True
                'column.OptionsColumn.AllowEdit = True
                'column.OptionsColumn.ReadOnly = True
                'column.OptionsColumn.AllowIncrementalSearch = True
                'column.AppearanceCell.TextOptions.HAlignment = par_align

                'column.DisplayFormat.FormatType = formatType
                'column.DisplayFormat.FormatString = formatString
                'conf_col(gv.Name, par_fn, True)


                gv_all.ExportToExcelOld(_file)

                'gv_all.Columns("glt_debit").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                'gv_all.Columns("glt_debit").DisplayFormat.FormatString = "n"
                'gv_all.Columns("glt_credit").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                'gv_all.Columns("glt_credit").DisplayFormat.FormatString = "n"
                'gv_all.Columns("glt_ext_debit").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                'gv_all.Columns("glt_ext_debit").DisplayFormat.FormatString = "n"
                'gv_all.Columns("glt_ext_credit").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                'gv_all.Columns("glt_ext_credit").DisplayFormat.FormatString = "n"


                Box("Export data sucess")

                OpenFile(_file)


                'frm.Dispose()


                'If _file = "" Then
                '    Return False
                '    Exit Function
                'End If
                'Dim dt2 As New DataTable
                'dt2 = GetTableData(ssql)

                'Dim grid As New System.Web.UI.WebControls.DataGrid()
                'grid.HeaderStyle.Font.Bold = True

                ''Dim NumberColumn As New BoundColumn
                ''NumberColumn.HeaderText = "Entity"
                ''NumberColumn.DataField = "en_desc"

                ' '' Add column to Columns collection.
                ''grid.Columns.AddAt(0, NumberColumn)

                ''add_columns(grid, 0, "en_desc", "Entity")
                ''add_columns(grid, 1, "glt_code", "GL Number")
                ''add_columns(grid, 2, "glt_date", "Date")
                ''add_columns(grid, 3, "glt_type", "Type")
                ''add_columns(grid, 4, "cu_name", "Currency")
                ''add_columns(grid, 5, "glt_exc_rate", "Exchange Rate")
                ''add_columns(grid, 6, "ac_code", "Account Code")
                ''add_columns(grid, 7, "ac_name", "Account Name")
                ''add_columns(grid, 8, "sb_desc", "Sub Account")
                ''add_columns(grid, 9, "cc_desc", "Cost Center")
                ''add_columns(grid, 10, "glt_desc", "Description")
                ''add_columns(grid, 11, "glt_debit", "Debit")
                ''add_columns(grid, 12, "glt_credit", "Credit")
                ''add_columns(grid, 13, "glt_ext_debit", "Ext Debit")

                ''add_columns(grid, 14, "glt_ext_credit", "Ext Credit")
                ''add_columns(grid, 15, "glt_ref_trans_code", "Ref. Number")
                ' ''add_columns(grid, 16, "glt_daybook", "Daybook")
                ''add_columns(grid, 16, "glt_posted", "Posted")
                ' ''add_columns(grid, 17, "Is Reverse", "glt_is_reverse")

                'grid.DataSource = dt2
                ''grid.DataMember = Data.Stats.TableName

                ''grid.DataBind()
                'grid.DataBind()

                '' 1 Step -1
                ''For i As Integer = (grid.Columns.Count - 1) To 17 Step -1
                ''    grid.Columns.RemoveAt(i)
                ''Next

                ''grid.Columns(3).HeaderText = "Entity"

                '' render the DataGrid control to a file
                'Using sw As New IO.StreamWriter(_file)
                '    Using hw As New Web.UI.HtmlTextWriter(sw)
                '        grid.RenderControl(hw)
                '    End Using
                'End Using
                'Box("Export data sukses.")

                'If ask("Do you want to open this file?", "Export To...") = True Then
                '    Try
                '        Dim process As System.Diagnostics.Process = New System.Diagnostics.Process()
                '        process.StartInfo.FileName = _file
                '        process.StartInfo.Verb = "Open"
                '        process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
                '        process.Start()
                '    Catch
                '        'MessageBox.Show(Me, "Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                '        Pesan(Err)
                '    End Try
                'End If


                ''Box("Export data sucess")

                ''OpenFile(_file)


            Catch ex As Exception
                Pesan(Err)
                Return False
            End Try
        End If

        'Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls")
        'If fileName <> "" Then
        'ExportToEx(gv_all, fileName, "xls")
        '    OpenFile(fileName)
        '    Return True
        'End If

        'Dim _file As String = AskSaveAsFile("Excel Files | *.csv")
        'Try
        '    gv_all.ExportToText(_file)
        '    Box("Export data sucess")
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try


        'End If
    End Function
    Private Function add_columns(ByVal par_grid As DataGrid, ByVal par_position As Integer, ByVal par_name As String, ByVal par_caption As String) As Boolean
        Try
            Dim NumberColumn As New BoundColumn
            NumberColumn.HeaderText = par_caption
            NumberColumn.DataField = par_name

            ' Add column to Columns collection.
            par_grid.Columns.AddAt(par_position, NumberColumn)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try

    End Function
    Sub csv()
        Dim _file As String = AskSaveAsFile("CSV Files | *.csv")

        Dim StrExport As String = ""
        For Each column As DevExpress.XtraGrid.Columns.GridColumn In gv_all.Columns
            StrExport &= """" & column.Caption & """;"
        Next

        StrExport = StrExport.Substring(0, StrExport.Length - 1)
        StrExport &= Environment.NewLine

        For i As Integer = 0 To gv_all.DataRowCount - 1
            For Each column As DevExpress.XtraGrid.Columns.GridColumn In gv_all.Columns
                StrExport &= """" & IIf(gv_all.GetRowCellValue(i, column) Is System.DBNull.Value, "", gv_all.GetRowCellValue(i, column)) & """;"
            Next

            StrExport = StrExport.Substring(0, StrExport.Length - 1)
            StrExport &= Environment.NewLine
            LblStatus.Text = i & " of " & gv_all.RowCount - 1
            System.Windows.Forms.Application.DoEvents()

        Next i

        Dim tw As IO.TextWriter = New IO.StreamWriter(_file)

        tw.Write(StrExport)
        tw.Close()
        MsgBox("File Saved")
    End Sub
    Sub csv2()
        Dim _file As String = AskSaveAsFile("CSV Files | *.csv")

        Dim StrExport As String = ""
        Dim cols As Integer = 0
        For Each column As DevExpress.XtraGrid.Columns.GridColumn In gv_all.Columns
            StrExport &= """" & column.Caption & """;"
            cols = cols + 1
        Next

        StrExport = StrExport.Substring(0, StrExport.Length - 1)
        StrExport &= Environment.NewLine
        Dim rows As Integer = ds.Tables("all").Rows.Count - 1
        Dim x As Integer = 0

        For i As Integer = 0 To rows

            For x = 0 To cols
                StrExport &= """" & IIf(ds.Tables("all").Rows(i).Item(x) Is System.DBNull.Value, "", ds.Tables("all").Rows(i).Item(x)) & """;"
            Next

            StrExport = StrExport.Substring(0, StrExport.Length - 1) & Environment.NewLine

            LblStatus.Text = i & " of " & gv_all.RowCount - 1
            System.Windows.Forms.Application.DoEvents()

        Next i

        Dim tw As IO.TextWriter = New IO.StreamWriter(_file)

        tw.Write(StrExport)
        tw.Close()
        MsgBox("File Saved")
    End Sub
    Sub csv3()
        Try

            Dim ds_new As New DataSet

            ds_new = ds.Copy

            Dim _file As String = AskSaveAsFile("XML Files | *.xml")

            ds_new.Tables("all").Columns("en_desc").ColumnName = "Entity"
            ds_new.Tables("all").Columns("glt_code").ColumnName = "GL Number"
            ds_new.Tables("all").Columns("glt_date").ColumnName = "Date"
            ds_new.Tables("all").Columns("glt_type").ColumnName = "Type"
            ds_new.Tables("all").Columns("cu_name").ColumnName = "Currency"
            ds_new.Tables("all").Columns("glt_exc_rate").ColumnName = "Exchange Rate"
            ds_new.Tables("all").Columns("ac_code").ColumnName = "Account Code"
            ds_new.Tables("all").Columns("ac_name").ColumnName = "Account Name"
            ds_new.Tables("all").Columns("sb_desc").ColumnName = "Sub Account"
            ds_new.Tables("all").Columns("cc_desc").ColumnName = "Cost Center"
            ds_new.Tables("all").Columns("glt_desc").ColumnName = "Description"
            ds_new.Tables("all").Columns("glt_debit").ColumnName = "Debit"
            ds_new.Tables("all").Columns("glt_credit").ColumnName = "Credit"
            ds_new.Tables("all").Columns("glt_ext_debit").ColumnName = "Ext Debit"
            ds_new.Tables("all").Columns("glt_ext_credit").ColumnName = "Ext Credit"
            ds_new.Tables("all").Columns("glt_ref_trans_code").ColumnName = "Ref. Number"
            ds_new.Tables("all").Columns("glt_daybook").ColumnName = "Daybook"
            ds_new.Tables("all").Columns("glt_posted").ColumnName = "Posted"
            ds_new.Tables("all").Columns("glt_add_by").ColumnName = "User Create"
            ds_new.Tables("all").Columns("glt_add_date").ColumnName = "Date Create"
            ds_new.Tables("all").Columns("glt_upd_by").ColumnName = "User Update"
            ds_new.Tables("all").Columns("glt_upd_date").ColumnName = "Date Update"

            ds_new.Tables("all").WriteXml(_file)

            'WriteToCsv(_file, "")


            'Dim builder As New StringBuilder()
            'For Each dr As DataRow In ds.Tables("all").Rows
            '    For Each item As Object In dr.ItemArray
            '        builder.AppendFormat("""{0}"", ", item.ToString())
            '    Next
            '    builder.Replace(", ", Environment.NewLine, builder.Length - 2, 2)
            'Next
            '' =================================
            'File.WriteAllText(_file, builder.ToString())
            MsgBox("File Saved")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub csv4()
        Try
            Dim _file As String = AskSaveAsFile("CSV Files | *.csv")


            Dim ds_new As New DataSet

            ds_new = ds.Copy

            'Dim _file As String = AskSaveAsFile("XML Files | *.xml")

            ds_new.Tables("all").Columns("en_desc").ColumnName = "Entity"
            ds_new.Tables("all").Columns("glt_code").ColumnName = "GL Number"
            ds_new.Tables("all").Columns("glt_date").ColumnName = "Date"
            ds_new.Tables("all").Columns("glt_type").ColumnName = "Type"
            ds_new.Tables("all").Columns("cu_name").ColumnName = "Currency"
            ds_new.Tables("all").Columns("glt_exc_rate").ColumnName = "Exchange Rate"
            ds_new.Tables("all").Columns("ac_code").ColumnName = "Account Code"
            ds_new.Tables("all").Columns("ac_name").ColumnName = "Account Name"
            ds_new.Tables("all").Columns("sb_desc").ColumnName = "Sub Account"
            ds_new.Tables("all").Columns("cc_desc").ColumnName = "Cost Center"
            ds_new.Tables("all").Columns("glt_desc").ColumnName = "Description"
            ds_new.Tables("all").Columns("glt_debit").ColumnName = "Debit"
            ds_new.Tables("all").Columns("glt_credit").ColumnName = "Credit"
            ds_new.Tables("all").Columns("glt_ext_debit").ColumnName = "Ext Debit"
            ds_new.Tables("all").Columns("glt_ext_credit").ColumnName = "Ext Credit"
            ds_new.Tables("all").Columns("glt_ref_trans_code").ColumnName = "Ref. Number"
            ds_new.Tables("all").Columns("glt_daybook").ColumnName = "Daybook"
            ds_new.Tables("all").Columns("glt_posted").ColumnName = "Posted"
            ds_new.Tables("all").Columns("glt_add_by").ColumnName = "User Create"
            ds_new.Tables("all").Columns("glt_add_date").ColumnName = "Date Create"
            ds_new.Tables("all").Columns("glt_upd_by").ColumnName = "User Update"
            ds_new.Tables("all").Columns("glt_upd_date").ColumnName = "Date Update"


            Dim StrExport As String = ""
            Dim cols As Integer = ds_new.Tables("all").Columns.Count - 1
            'For Each column As DevExpress.XtraGrid.Columns.GridColumn In gv_all.Columns
            '    StrExport &= """" & column.Caption & """;"
            '    'cols = cols + 1
            'Next

            For Each column As DataColumn In ds_new.Tables("all").Columns
                StrExport &= """" & column.ColumnName & """;"
            Next

            StrExport = StrExport.Substring(0, StrExport.Length - 1)
            StrExport &= Environment.NewLine

            WriteToCsv(_file, StrExport)
            StrExport = ""
            Dim rows As Integer = ds_new.Tables("all").Rows.Count - 1
            Dim x As Integer = 0

            For i As Integer = 0 To rows
                StrExport = ""
                For x = 0 To cols
                    StrExport &= """" & IIf(ds_new.Tables("all").Rows(i).Item(x) Is System.DBNull.Value, "", ds_new.Tables("all").Rows(i).Item(x)) & """;"
                Next

                StrExport = StrExport.Substring(0, StrExport.Length - 1) & Environment.NewLine
                WriteToCsv(_file, StrExport)

                'LblStatus.Text = i & " of " & gv_all.RowCount - 1
                'System.Windows.Forms.Application.DoEvents()

            Next i


            Box("Export data sucess")

            OpenFile(_file)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub WriteToCsv(ByVal file As String, ByVal msg As String)
        Try
            Dim fs1 As FileStream = New FileStream(file, FileMode.Append, FileAccess.Write)
            Dim s1 As StreamWriter = New StreamWriter(fs1)
            s1.Write(msg)
            s1.Close()
            fs1.Close()
        Catch ex As Exception
        End Try
    End Sub


    Private Sub CheckTheTransactionIsNotBalancedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckTheTransactionIsNotBalancedToolStripMenuItem.Click
        Try
            Dim _debet, _credit As Double
            Dim _nomor As String = ""
            _debet = 0
            _credit = 0
            Dim _temp As String = ""
            Dim _temp_view As String = ""

            If xtc_master.SelectedTabPageIndex = 0 Then
                With ds.Tables("unposted") 'posted all
                    For i As Integer = 0 To ds.Tables("unposted").Rows.Count - 1

                        If _nomor <> .Rows(i).Item("glt_code").ToString Then
                            If System.Math.Round(_debet, 2) <> System.Math.Round(_credit, 2) Then
                                'Box("GL Number " & _nomor & " is not balanced")
                                _temp_view += "" & _nomor & " " & .Rows(i).Item("en_desc").ToString & "," & vbNewLine
                                _temp += "'" & _nomor & "'," & vbNewLine
                                'Clipboard.SetText(_nomor)
                                'Exit Try
                            End If
                            _nomor = .Rows(i).Item("glt_code").ToString
                            _debet = .Rows(i).Item("glt_debit")
                            _credit = .Rows(i).Item("glt_credit")
                        Else
                            _debet = _debet + .Rows(i).Item("glt_debit")
                            _credit = _credit + .Rows(i).Item("glt_credit")
                        End If

                    Next

                    If _temp <> "" Then
                        Box("GL Number " & _temp_view & " is not balanced")
                        _temp = _temp.Substring(0, _temp.Length - 1)
                        Dim _sql As String = "select * from glt_det where glt_code in (" & _temp & ")"
                        Clipboard.SetText(_sql)
                    Else
                        Box("All Balanced")
                    End If

                End With
            ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                With ds.Tables("posted") 'posted all
                    For i As Integer = 0 To ds.Tables("posted").Rows.Count - 1

                        If _nomor <> .Rows(i).Item("glt_code").ToString Then
                            If System.Math.Round(_debet, 2) <> System.Math.Round(_credit, 2) Then
                                'Box("GL Number " & _nomor & " is not balanced")
                                ' _temp += _nomor & vbNewLine
                                _temp_view += "" & _nomor & " " & .Rows(i).Item("en_desc").ToString & "," & vbNewLine
                                _temp += "'" & _nomor & "'," & vbNewLine
                                'Clipboard.SetText(_nomor)
                                'Exit Try
                            End If
                            _nomor = .Rows(i).Item("glt_code").ToString
                            _debet = .Rows(i).Item("glt_debit")
                            _credit = .Rows(i).Item("glt_credit")
                        Else
                            _debet = _debet + .Rows(i).Item("glt_debit")
                            _credit = _credit + .Rows(i).Item("glt_credit")
                        End If

                    Next

                    If _temp <> "" Then
                        Box("GL Number " & _temp_view & " is not balanced")
                        _temp = _temp.Substring(0, _temp.Length - 1)
                        Dim _sql As String = "select * from glt_det where glt_code in (" & _temp & ")"
                        Clipboard.SetText(_sql)
                    Else
                        Box("All Balanced")
                    End If

                End With
            ElseIf xtc_master.SelectedTabPageIndex = 2 Then
                With ds.Tables("all") 'posted all
                    For i As Integer = 0 To ds.Tables("all").Rows.Count - 1

                        If _nomor <> .Rows(i).Item("glt_code").ToString Then
                            If System.Math.Round(_debet, 2) <> System.Math.Round(_credit, 2) Then
                                'Box("GL Number " & _nomor & " is not balanced")
                                ' _temp += _nomor & vbNewLine
                                _temp_view += "" & _nomor & " " & .Rows(i).Item("en_desc").ToString & "," & vbNewLine
                                _temp += "'" & _nomor & "'," & vbNewLine
                                'Clipboard.SetText(_nomor)
                                'Exit Try
                            End If
                            _nomor = .Rows(i).Item("glt_code").ToString
                            _debet = .Rows(i).Item("glt_debit")
                            _credit = .Rows(i).Item("glt_credit")
                        Else
                            _debet = _debet + .Rows(i).Item("glt_debit")
                            _credit = _credit + .Rows(i).Item("glt_credit")
                        End If

                    Next

                    If _temp <> "" Then
                        Box("GL Number " & _temp_view & " is not balanced")
                        _temp = _temp.Substring(0, _temp.Length - 1)
                        Dim _sql As String = "select * from glt_det where glt_code in (" & _temp & ")"
                        Clipboard.SetText(_sql)
                    Else
                        Box("All Balanced")
                    End If

                End With
            End If


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub ApproveCheckToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApproveCheckToolStripMenuItem.Click
        Try
            Dim ssql As String
            Dim ssqls As New ArrayList
            ssql = "update glt_det set glt_check_status='Y' where glt_code='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("glt_code") & "'"
            ssqls.Add(ssql)

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Exit Sub
                End If
                ssqls.Clear()
            End If
            Box("Success")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
Public Class Export_excel

    Public Shared Sub ExportToExcel(ByVal FileName As String, ByVal objDataReader As DataTable)
        Dim i As Integer
        Dim sb As New System.Text.StringBuilder
        Try
            Dim intColumn, intColumnValue As Integer
            Dim row As DataRow
            For intColumn = 0 To objDataReader.Columns.Count - 1
                sb.Append(objDataReader.Columns(intColumn).ColumnName)
                If intColumnValue <> objDataReader.Columns.Count - 1 Then
                    sb.Append(vbTab)
                End If
            Next
            sb.Append(vbCrLf)
            For Each row In objDataReader.Rows
                For intColumnValue = 0 To objDataReader.Columns.Count - 1
                    sb.Append(StrConv(IIf(IsDBNull(row.Item(intColumnValue)), "", row.Item(intColumnValue)), VbStrConv.ProperCase))
                    If intColumnValue <> objDataReader.Columns.Count - 1 Then
                        sb.Append(vbTab)
                    End If
                Next
                sb.Append(vbCrLf)
            Next
            SaveExcel(FileName, sb)
        Catch ex As Exception
            Throw
        Finally
            objDataReader = Nothing
            sb = Nothing
        End Try
    End Sub

    Private Shared Sub SaveExcel(ByVal fpath As String, ByVal sb As System.Text.StringBuilder)
        Dim fsFile As New FileStream(fpath, FileMode.Create, FileAccess.Write)
        Dim strWriter As New StreamWriter(fsFile)
        Try
            With strWriter
                .BaseStream.Seek(0, SeekOrigin.End)
                .WriteLine(sb)
                .Close()
            End With
        Catch e As Exception
            Throw
        Finally
            sb = Nothing
            strWriter = Nothing
            fsFile = Nothing
        End Try
    End Sub
End Class