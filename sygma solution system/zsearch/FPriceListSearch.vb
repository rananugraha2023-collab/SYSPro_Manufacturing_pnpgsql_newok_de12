Imports master_new.ModFunction

Public Class FPriceListSearch
    Public _row, _en_id As Integer
    Public _obj As Object
    Public _objk As Object
    Public _type As String
    Dim func_data As New function_data

    Private Sub FPriceListSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "pi_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "SO Type", "pi_so_type", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Promotion", "promo_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Sales Programe", "sales_program_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Start Date", "pi_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_master, "End Date", "pi_end_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FSalesProgramImportTarget" Then
            get_sequel = "SELECT  " _
                & "  a.pi_oid, " _
                & "  a.pi_dom_id, " _
                & "  a.pi_en_id, " _
                & "  a.pi_add_by, " _
                & "  a.pi_add_date, " _
                & "  a.pi_upd_by, " _
                & "  a.pi_upd_date, " _
                & "  a.pi_id, " _
                & "  a.pi_code, " _
                & "  a.pi_desc, " _
                & "  a.pi_so_type, " _
                & "  a.pi_cu_id, " _
                & "  a.pi_start_date, " _
                & "  a.pi_end_date, " _
                & "  a.pi_active, " _
                & "  a.pi_dt, " _
                & "  COUNT(b.pid_pt_id) AS count_pt_id, " _
                & "  SUM(d.invct_cost) AS cost, " _
                & "  ( " _
                & "    SELECT SUM(pidd_price) " _
                & "    FROM ( " _
                & "      SELECT DISTINCT pidd_pid_oid, pidd_price  " _
                & "      FROM public.pidd_det  " _
                & "      WHERE pidd_pid_oid IN ( " _
                & "        SELECT pid_oid FROM public.pid_det WHERE pid_pi_oid = a.pi_oid " _
                & "      ) " _
                & "    ) AS unique_prices " _
                & "  ) AS total_pidd_price " _
                & "FROM " _
                & "  public.pi_mstr a " _
                & "  INNER JOIN public.pid_det b ON (a.pi_oid = b.pid_pi_oid) " _
                & "  INNER JOIN public.invct_table d ON (b.pid_pt_id = d.invct_pt_id) " _
                & "WHERE  " _
                & "  a.pi_end_date >= CURRENT_DATE " _
                & "  AND a.pi_active = 'Y'  " _
                & "GROUP BY " _
                & "  a.pi_oid, " _
                & "  a.pi_dom_id, " _
                & "  a.pi_en_id, " _
                & "  a.pi_add_by, " _
                & "  a.pi_add_date, " _
                & "  a.pi_upd_by, " _
                & "  a.pi_upd_date, " _
                & "  a.pi_id, " _
                & "  a.pi_code, " _
                & "  a.pi_desc, " _
                & "  a.pi_so_type, " _
                & "  a.pi_promo_id, " _
                & "  a.pi_cu_id, " _
                & "  a.pi_sls_program, " _
                & "  a.pi_start_date, " _
                & "  a.pi_end_date, " _
                & "  a.pi_active, " _
                & "  a.pi_dt"

        Else
            get_sequel = "SELECT  " _
                    & "  pi_oid, " _
                    & "  pi_dom_id, " _
                    & "  pi_en_id, " _
                    & "  en_desc, " _
                    & "  pi_add_by, " _
                    & "  pi_add_date, " _
                    & "  pi_upd_by, " _
                    & "  pi_upd_date, " _
                    & "  pi_id, " _
                    & "  pi_code, " _
                    & "  pi_desc, " _
                    & "  pi_so_type, " _
                    & "  pi_promo_id, " _
                    & "  promo_desc, " _
                    & "  pi_cu_id, " _
                    & "  cu_name, " _
                    & "  pi_sls_program, " _
                    & "  sls_name as sales_program_name, " _
                    & "  pi_start_date, " _
                    & "  pi_end_date, " _
                    & "  pi_active, " _
                    & "  pi_dt " _
                    & "FROM  " _
                    & "  public.pi_mstr " _
                    & " inner join en_mstr on en_id = pi_en_id " _
                    & " inner join cu_mstr on cu_id = pi_cu_id " _
                    & " inner join promo_mstr on promo_id = pi_promo_id " _
                     & " left outer join sls_program on pi_sls_program = sls_code " _
                    & " where pi_en_id = " + _en_id.ToString
        End If
        

        Return get_sequel
    End Function

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub

    Private Sub fill_data()
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position
        Dim dt_bantu As New DataTable()
        Dim func_coll As New function_collection

        If fobject.name = "FPriceListDetail" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("pi_code")
            fobject._pi_oid = ds.Tables(0).Rows(_row_gv).Item("pi_oid").ToString

        ElseIf fobject.name = "FCanvasingProgram" Then
            '_obj.text = ds.Tables(0).Rows(_row_gv).Item("pi_code")
            'fobject._pi_id = ds.Tables(0).Rows(_row_gv).Item("pi_id")
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("pi_desc")
            _obj.tag = ds.Tables(0).Rows(_row_gv).Item("pi_id")

        ElseIf fobject.name = "FSalesProgramImportTarget" Then
            '_obj.text = ds.Tables(0).Rows(_row_gv).Item("pi_code")
            'fobject._pi_id = ds.Tables(0).Rows(_row_gv).Item("pi_id")
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("pi_desc")
            _obj.tag = ds.Tables(0).Rows(_row_gv).Item("pi_id")
            fobject.sls_start_date.DateTime = ds.Tables(0).Rows(_row_gv).Item("pi_start_date")
            fobject.sls_end_date.DateTime = ds.Tables(0).Rows(_row_gv).Item("pi_end_date")
            fobject.sls_target_revenue.Text = ds.Tables(0).Rows(_row_gv).Item("total_pidd_price")
            fobject.sls_target_revenue.Tag = ds.Tables(0).Rows(_row_gv).Item("total_pidd_price")
            fobject.sls_target_qty.Text = ds.Tables(0).Rows(_row_gv).Item("count_pt_id")
            fobject.sls_target_qty.Tag = ds.Tables(0).Rows(_row_gv).Item("count_pt_id")
            fobject.sls_total_cost.Text = ds.Tables(0).Rows(_row_gv).Item("cost")
            fobject.sls_total_cost.Tag = ds.Tables(0).Rows(_row_gv).Item("cost")
            'fobject.sls_avg_disc.Text = ds.Tables(0).Rows(_row_gv).Item("avg_pidd_disc")
            'fobject.sls_avg_disc.Tag = ds.Tables(0).Rows(_row_gv).Item("avg_pidd_disc")

        ElseIf fobject.name = "FPriceListCopy" Then
            If _type = "from" Then
                _obj.text = ds.Tables(0).Rows(_row_gv).Item("pi_code")
                fobject._pi_oid_from = ds.Tables(0).Rows(_row_gv).Item("pi_oid").ToString
            ElseIf _type = "to" Then
                _obj.text = ds.Tables(0).Rows(_row_gv).Item("pi_code")
                fobject._pi_oid_to = ds.Tables(0).Rows(_row_gv).Item("pi_oid").ToString
            End If
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        'gv_master.Focus()
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            fill_data()
            Me.Close()
        End If
    End Sub
End Class
