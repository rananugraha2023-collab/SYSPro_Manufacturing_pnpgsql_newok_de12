Imports master_new.ModFunction

Public Class FPartNumberSearchbyQR
    Public _row, _en_id, _si_id, _pi_id As Integer
    Public _obj, _objk As Object
    Public _so_type, _price, _pi_oid As String
    Public _sq_booking As String
    Public _tran_oid As String = ""
    Public _ppn_type As String = ""
    Dim func_data As New function_data
    Public _filter As String
    Public grid_call As String = ""
    Public _so_cash As Boolean = False
    'Public _sq_booking As Boolean = False
    Public _qty_receive As Double
    Public _pt_id As Integer



    Private Sub FPartNumberSearchbyQR_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Pricelist Name", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Price", "pidd_price", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
    End Sub

    Public Overrides Function get_sequel() As String
        'Dim _en_id_coll As String = func_data.entity_parent(_en_id)
        get_sequel = ""

        get_sequel = "SELECT DISTINCT  " _
                    & "  public.en_mstr.en_id, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.pi_mstr.pi_id, " _
                    & "  public.pi_mstr.pi_code, " _
                    & "  public.pi_mstr.pi_desc, " _
                    & "  public.pt_mstr.pt_id, " _
                    & "  public.pt_mstr.pt_code, " _
                    & "  public.pt_mstr.pt_desc1, " _
                    & "  public.pidd_det.pidd_payment_type, " _
                    & "  public.pidd_det.pidd_price, " _
                    & "  public.pi_mstr.pi_active " _
                    & "FROM " _
                    & "  public.pid_det " _
                    & "  INNER JOIN public.pt_mstr ON (public.pid_det.pid_pt_id = public.pt_mstr.pt_id) " _
                    & "  INNER JOIN public.pidd_det ON (public.pid_det.pid_oid = public.pidd_det.pidd_pid_oid) " _
                    & "  INNER JOIN public.pi_mstr ON (public.pid_det.pid_pi_oid = public.pi_mstr.pi_oid) " _
                    & "  INNER JOIN public.en_mstr ON (public.pi_mstr.pi_en_id = public.en_mstr.en_id) " _
                    & "WHERE " _
                    & "  public.pi_mstr.pi_en_id = " & _en_id & " " _
                    & "  and public.pi_mstr.pi_id = " & _pi_id & " " _
                    & "  and public.pidd_det.pidd_payment_type = '9941' " _
                    & "  and public.pi_mstr.pi_active = 'Y'"

        Return get_sequel
    End Function

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub

    Public Overrides Sub fill_data()


        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position
        Dim dt_bantu As New DataTable()
        Dim func_coll As New function_collection

        _obj.text = ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
        fobject._pt_id = ds.Tables(0).Rows(_row_gv).Item("pt_id")
        fobject.invqr_price_jawa.editvalue = ds.Tables(0).Rows(_row_gv).Item("pidd_price")
        fobject.invqr_price_ljawa.editvalue = ds.Tables(0).Rows(_row_gv).Item("pidd_price")
        'fobject.ccre_um_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_um")
        'fobject.ccre_cost.editvalue = ds.Tables(0).Rows(_row_gv).Item("invct_cost")

        '_obj.text = ds.Tables(0).Rows(_row_gv).Item("pt_code")
        'fobject._pt_id_global = ds.Tables(0).Rows(_row_gv).Item("pt_id")
        'fobject.ccre_loc_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_loc_id")
        'fobject.ccre_um_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_um")
        'fobject.ccre_cost.editvalue = ds.Tables(0).Rows(_row_gv).Item("invct_cost")

        'Dim dt_price As New DataTable
        'Dim sSQL As String
        'sSQL = "SELECT  " _
        '        & "  pi_id, " _
        '        & "  pidd_oid, " _
        '        & "  pidd_pid_oid, " _
        '        & "  pidd_payment_type, " _
        '        & "  pidd_price, " _
        '        & "  pidd_disc, " _
        '        & "  pidd_dp, " _
        '        & "  pidd_interval, " _
        '        & "  coalesce(pidd_payment,0) as pidd_payment, " _
        '        & "  pidd_min_qty, " _
        '        & "  pidd_sales_unit, " _
        '        & "  pid_pt_id " _
        '        & "FROM  " _
        '        & "  public.pidd_det " _
        '        & "  inner join public.pid_det on pid_oid = pidd_pid_oid " _
        '        & "  inner join public.pi_mstr on pi_oid = pid_pi_oid " _
        '        & "  where pi_id = " & _pi_id & " " _
        '        & "  and pidd_payment_type =  '9941' " _
        '        & "  and pid_pt_id = " & ds.Tables(0).Rows(_row_gv).Item("pt_id") _
        '        & "  order by pidd_min_qty desc "

        'dt_price = master_new.PGSqlConn.GetTableData(sSQL)

        'For Each dr As DataRow In dt_price.Rows
        '    'fobject.invqr_price_jawa.editvalue = SetNumber(dr("pidd_price"))
        '    fobject.invqr_price_jawa.editvalue = ds.Tables(0).Rows(_row_gv).Item("pidd_price")
        'Next

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

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
