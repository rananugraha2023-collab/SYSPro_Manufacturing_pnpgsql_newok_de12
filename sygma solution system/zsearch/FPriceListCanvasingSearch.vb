Imports master_new.ModFunction

Public Class FPriceListCanvasingSearch
    Public _row, _en_id As Integer
    Public _obj, _so_type As Object
    Public _start, _end As Date
    Public _objk As Object
    Public _type As String
    Dim func_data As New function_data

    Private Sub FPriceListCanvasingSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

        'Dim _en_id_coll As String = entity_parent(par_en_id)

        get_sequel = ""

        'get_sequel = "SELECT  " _
        '            & "  pi_oid, " _
        '            & "  pi_dom_id, " _
        '            & "  pi_en_id, " _
        '            & "  en_desc, " _
        '            & "  pi_add_by, " _
        '            & "  pi_add_date, " _
        '            & "  pi_upd_by, " _
        '            & "  pi_upd_date, " _
        '            & "  pi_id, " _
        '            & "  pi_code, " _
        '            & "  pi_desc, " _
        '            & "  pi_so_type, " _
        '            & "  pi_promo_id, " _
        '            & "  promo_desc, " _
        '            & "  pi_cu_id, " _
        '            & "  cu_name, " _
        '            & "  pi_sls_program, " _
        '            & "  pi_ptnrg_id, " _
        '            & "  ptnrg_name, " _
        '            & "  sls_name as sales_program_name, " _
        '            & "  pi_start_date, " _
        '            & "  pi_end_date, " _
        '            & "  pi_active, " _
        '            & "  pi_dt " _
        '            & "FROM  " _
        '            & "  public.pi_mstr " _
        '            & " inner join en_mstr on en_id = pi_en_id " _
        '            & " inner join cu_mstr on cu_id = pi_cu_id " _
        '            & " inner join promo_mstr on promo_id = pi_promo_id " _
        '            & " inner join ptnrg_grp on ptnrg_id = pi_ptnrg_id " _
        '            & " left outer join sls_program on pi_sls_program = sls_code " _
        '            & " where pi_so_type ~~* '" & _so_type + "'" _
        '            & " AND pi_active = 'Y' "

        get_sequel = "SELECT  " _
                    & "  public.pi_mstr.pi_oid, " _
                    & "  public.pi_mstr.pi_dom_id, " _
                    & "  public.pi_mstr.pi_en_id, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.pi_mstr.pi_add_by, " _
                    & "  public.pi_mstr.pi_add_date, " _
                    & "  public.pi_mstr.pi_upd_by, " _
                    & "  public.pi_mstr.pi_upd_date, " _
                    & "  public.pi_mstr.pi_id, " _
                    & "  public.pi_mstr.pi_code, " _
                    & "  public.pi_mstr.pi_desc, " _
                    & "  public.pi_mstr.pi_so_type, " _
                    & "  public.pi_mstr.pi_promo_id, " _
                    & "  public.promo_mstr.promo_desc, " _
                    & "  public.pi_mstr.pi_cu_id, " _
                    & "  public.cu_mstr.cu_name, " _
                    & "  public.pi_mstr.pi_sales_program, " _
                    & "  public.code_mstr.code_name AS sales_program, " _
                    & "  public.pi_mstr.pi_start_date, " _
                    & "  public.pi_mstr.pi_end_date, " _
                    & "  public.pi_mstr.pi_active, " _
                    & "  public.pi_mstr.pi_ptnrg_id, " _
                    & "  public.ptnrg_grp.ptnrg_name " _
                    & "FROM " _
                    & "  public.pi_mstr " _
                    & "  INNER JOIN public.en_mstr ON (public.pi_mstr.pi_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.promo_mstr ON (public.pi_mstr.pi_promo_id = public.promo_mstr.promo_id) " _
                    & "  INNER JOIN public.code_mstr ON (public.pi_mstr.pi_sales_program = public.code_mstr.code_id) " _
                    & "  LEFT OUTER JOIN public.ptnrg_grp ON (public.pi_mstr.pi_ptnrg_id = public.ptnrg_grp.ptnrg_id) " _
                    & "  INNER JOIN public.cu_mstr ON (public.pi_mstr.pi_cu_id = public.cu_mstr.cu_id)" _
                    & " where pi_so_type ~~* '" & _so_type + "'" _
                    & " AND pi_active = 'Y' "


        '   .SQL = "select pi_id, pi_desc from pi_mstr where pi_active ~~* 'Y'" _
        '& " AND pi_dom_id = " & master_new.ClsVar.sdom_id _
        '& " AND pi_en_id in (" & _en_id_coll + ")" _
        '& " AND pi_cu_id = " & par_cu_id.ToString _
        '& " AND pi_so_type ~~* '" & par_so_type + "'" _
        ' & " AND pi_ptnrg_id = " & par_ptnrg_id.ToString + " " _
        '& " AND pi_start_date <= " + SetDate(par_date) + " and pi_end_date >= " + SetDate(par_date) _
        '& " AND pi_active ~~* 'Y' " _
        '& " order by pi_desc"

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

        ElseIf fobject.name = "FCanvasingPlanner" Or fobject.name = FCanvasingSalesPlanning.Name Then
            '_obj.text = ds.Tables(0).Rows(_row_gv).Item("pi_code")
            'fobject._pi_id = ds.Tables(0).Rows(_row_gv).Item("pi_id")
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("pi_desc")
            _obj.tag = ds.Tables(0).Rows(_row_gv).Item("pi_id")
            '_obj.text = ds.Tables(0).Rows(_row_gv).Item("pi_desc")
            '_obj.tag = ds.Tables(0).Rows(_row_gv).Item("pi_id")

            'fobject.cnv_so_type.text = SetString(ds.Tables(0).Rows(_row_gv).Item("pi_so_type"))

            'fobject.cnv_promo_id.tag = ds.Tables(0).Rows(_row_gv).Item("pi_promo_id")
            'fobject.cnv_promo_id.text = SetString(ds.Tables(0).Rows(_row_gv).Item("promo_desc"))

            'fobject.cnv_sales_program.tag = ds.Tables(0).Rows(_row_gv).Item("pi_sales_program")
            'fobject.cnv_sales_program.text = SetString(ds.Tables(0).Rows(_row_gv).Item("sales_program"))

            'fobject.cnv_ptnrg_id.tag = ds.Tables(0).Rows(_row_gv).Item("pi_ptnrg_id")
            'fobject.cnv_ptnrg_id.text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnrg_name"))

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
