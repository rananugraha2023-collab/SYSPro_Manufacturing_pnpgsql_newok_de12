Imports npgsql
Imports master_new.ModFunction

Public Class FSalesOrderReturnReport
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim func_bill As New Cls_Bilangan
    Dim _ar_oid_mstr As String
    Public ds_edit_so, ds_edit_shipment, ds_edit_dist, ds_edit_piutang, ds_sod_piutang As DataSet
    Dim _now As DateTime

    Private Sub FSalesOrderReturnReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SO Date", "so_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Sold To", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Return Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Return Date", "soship_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty", "sod_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Shipment", "sod_qty_shipment", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty Return", "soshipd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Balance", "public.sod_det.sod_qty_shipment - public.soshipd_det.soshipd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "UM", "soshipd_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "UM Conversion", "soshipd_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Real", "soshipd_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Serial Number", "soshipds_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Reason Code", "rea_code_name", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  public.so_mstr.so_en_id, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.so_mstr.so_code, " _
                    & "  public.so_mstr.so_date, " _
                    & "  ptnr_name as ptnr_name_sold, " _
                    & "  public.soship_mstr.soship_code, " _
                    & "  public.soship_mstr.soship_date, " _
                    & "  public.sod_det.sod_pt_id, " _
                    & "  public.pt_mstr.pt_code, " _
                    & "  public.pt_mstr.pt_desc1, " _
                    & "  public.pt_mstr.pt_desc2, " _
                    & "  public.sod_det.sod_qty, " _
                    & "  public.sod_det.sod_qty_shipment, " _
                    & "  public.soshipd_det.soshipd_qty, " _
                    & "  public.soshipd_det.soshipd_um, " _
                    & "  public.soshipd_det.soshipd_um_conv, " _
                    & "  um_mstr.code_name AS soshipd_um_name, " _
                    & "  rea_code_mstr.code_name AS rea_code_name, " _
                    & "  public.soship_mstr.soship_is_shipment, " _
                    & "  public.sod_det.sod_qty_shipment - public.soshipd_det.soshipd_qty AS soshipd_det_balance, " _
                    & "  public.loc_mstr.loc_desc, " _
                    & "  public.soshipd_det.soshipd_rea_code_id, " _
                    & "  public.soshipd_det.soshipd_qty_inv, " _
                    & "  public.soshipd_det.soshipd_qty_real " _
                    & "FROM " _
                    & "  public.sod_det " _
                    & "  INNER JOIN public.so_mstr ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                    & "  INNER JOIN public.soshipd_det ON (public.sod_det.sod_oid = public.soshipd_det.soshipd_sod_oid) " _
                    & "  INNER JOIN public.soship_mstr ON (public.soship_mstr.soship_oid = public.soshipd_det.soshipd_soship_oid) " _
                    & "  AND (public.so_mstr.so_oid = public.soship_mstr.soship_so_oid) " _
                    & "  INNER JOIN public.pt_mstr ON (public.sod_det.sod_pt_id = public.pt_mstr.pt_id) " _
                    & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                    & "  INNER JOIN public.loc_mstr ON (public.soshipd_det.soshipd_loc_id = public.loc_mstr.loc_id) " _
                    & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id) " _
                    & "  LEFT OUTER JOIN public.code_mstr rea_code_mstr ON (rea_code_mstr.code_id = soshipd_rea_code_id) " _
                    & "  INNER JOIN public.code_mstr um_mstr ON (um_mstr.code_id = soshipd_um)" _
                    & "  where soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and soship_is_shipment ~~* 'N'"


        Return get_sequel
    End Function

End Class
