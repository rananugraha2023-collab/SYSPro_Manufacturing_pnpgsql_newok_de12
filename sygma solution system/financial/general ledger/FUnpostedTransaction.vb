Imports master_new.ModFunction

Public Class FUnpostedTransaction

    Private Sub FUnpostedTransaction_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = Now
        pr_txttglakhir.DateTime = Now
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "GL Number", "glt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "glt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Type", "glt_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Exchange Rate", "glt_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Partner", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "glt_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Debit", "glt_debit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Credit", "glt_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Exc Rate", "glt_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'glt_exc_rate
        add_column_copy(gv_master, "Ext Debit", "glt_ext_debit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_copy(gv_master, "Ext Credit", "glt_ext_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        'add_column_copy(gv_master, "Transaction", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Ref. Number", "glt_ref_trans_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Daybook", "glt_daybook", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Posted", "glt_posted", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Reverse", "glt_is_reverse", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "glt_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "glt_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "glt_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "glt_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
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
                            & "  public.glt_det.glt_credit, " _
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
                            & " and glt_posted ~~* 'n'" _
                            & " and glt_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " order by glt_code, ac_code "
                        .InitializeCommand()
                        .FillDataSet(ds, "unposted")
                        gc_master.DataSource = ds.Tables("unposted")

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
             & "glt_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("glt_code") + "'"

        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRGeneralLedgerPrint"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("glt_code")
        frm.ShowDialog()
    End Sub

    Private Sub CheckUnBalanceTransactionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckUnBalanceTransactionToolStripMenuItem.Click
        Try
            Dim _debet, _credit As Double
            Dim _nomor As String = ""
            _debet = 0
            _credit = 0
            Dim _temp As String = ""
            With ds.Tables("unposted")
                For i As Integer = 0 To ds.Tables("unposted").Rows.Count - 1

                    If _nomor <> .Rows(i).Item("glt_code").ToString Then
                        If System.Math.Round(_debet, 2) <> System.Math.Round(_credit, 2) Then
                            'Box("GL Number " & _nomor & " is not balanced")
                            _temp += _nomor & vbNewLine
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
                    Box("GL Number " & _temp & " is not balanced")
                    Clipboard.SetText(_temp)
                Else
                    Box("All Balanced")
                End If
               
            End With
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
