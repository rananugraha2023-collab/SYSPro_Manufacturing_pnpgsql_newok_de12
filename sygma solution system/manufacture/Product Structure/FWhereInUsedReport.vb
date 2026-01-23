Imports npgsql
Imports master_new.ModFunction

Public Class FWhereInUsedReport
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _bom_oid_mstr As String
    Public _pt_id As String

    Private Sub FBom_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()

        xtc_master.SelectedTabPageIndex = 1
        xtp_edit.PageVisible = False
        xtp_data.PageVisible = True
        xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False

    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "psd_oid", False)
        add_column(gv_master, "psd_ps_oid", False)
        add_column(gv_master, "Product Structure Desc.", "ps_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Use BOM", "psd_use_bom", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "psd_pt_bom_id", False)
        add_column(gv_master, "Part/BOM", "ptbomdesc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Component", "psd_comp", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Reference", "psd_ref", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "psd_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Start Date", "psd_start_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_master, "End Date", "psd_end_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_master, "Qty", "psd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_master, "Str Type", "psd_str_type", DevExpress.Utils.HorzAlignment.Far)
        add_column(gv_master, "Scrap", "psd_scrp_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column(gv_master, "Lead Time Offset", "psd_lt_off", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column(gv_master, "Operation", "psd_op", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column(gv_master, "Sequence", "psd_seq", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column(gv_master, "Forecast Percent", "psd_fcst_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column(gv_master, "Option Group", "code_group_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Process", "code_proc_name", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
            & "  psd_det.psd_oid, " _
            & "  psd_det.psd_ps_oid, " _
            & "  psd_det.psd_use_bom, " _
            & "  psd_det.psd_pt_bom_id, " _
            & "  psd_det.psd_comp, " _
            & "  psd_det.psd_ref, " _
            & "  psd_det.psd_desc, " _
            & "  psd_det.psd_start_date, " _
            & "  psd_det.psd_end_date, " _
            & "  psd_det.psd_qty, " _
            & "  psd_det.psd_str_type, " _
            & "  psd_det.psd_scrp_pct, " _
            & "  psd_det.psd_lt_off, " _
            & "  psd_det.psd_op, " _
            & "  psd_det.psd_seq, " _
            & "  psd_det.psd_fcst_pct, " _
            & "  psd_det.psd_group, " _
            & "  psd_det.psd_process,ps_par,ps_desc, " _
            & "   CASE WHEN ps_use_bom = 'Y' " _
            & "   THEN (SELECT bom_mstr.bom_desc from bom_mstr where bom_id=ps_pt_bom_id) " _
            & "   ELSE (SELECT pt_mstr.pt_desc1 from pt_mstr where pt_id=ps_pt_bom_id) " _
            & "   END AS ptbomdesc, " _
            & "  code_mstr_group.code_en_id AS code_group_en_id, " _
            & "  code_mstr_group.code_id AS code_group_id, " _
            & "  code_mstr_group.code_field AS code_group_field, " _
            & "  code_mstr_group.code_code AS code_group_code, " _
            & "  code_mstr_group.code_name AS code_group_name, " _
            & "  code_mstr_proc.code_en_id AS code_proc_en_id, " _
            & "  code_mstr_proc.code_id AS code_proc_id, " _
            & "  code_mstr_proc.code_field AS code_proc_field, " _
            & "  code_mstr_proc.code_code AS code_proc_code, " _
            & "  code_mstr_proc.code_name AS code_proc_name " _
            & "FROM " _
            & "  psd_det " _
            & "  INNER JOIN code_mstr code_mstr_group ON (psd_det.psd_group = code_mstr_group.code_id) " _
            & "  INNER JOIN code_mstr code_mstr_proc ON (psd_det.psd_process = code_mstr_proc.code_id) " _
            & "  INNER JOIN  ps_mstr on (psd_ps_oid=ps_oid) where psd_pt_bom_id=(select pt_id from pt_mstr where pt_code='" & be_first.EditValue & "')"

        Return get_sequel
    End Function

    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_first.ButtonClick
        Dim frm As New FPTBOMSrch
        frm.set_win(Me)
        frm._en_id = "select user_en_id from tconfuserentity where userid = " + master_new.ClsVar.sUserID.ToString + " "
        frm._pil = 1
        frm.type_form = True
        frm.ShowDialog()
    End Sub
End Class
