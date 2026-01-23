Imports master_new.ModFunction

Public Class FSOReportCheck
    Public par_tgl_awal As String
    Public par_tgl_akhir As String
    Public par_pt_code As String

    Private Sub FAccountSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        'Me.Width = 800
        'Me.Height = 500
        
        sb_retrieve_Click(Nothing, Nothing)

    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Date", "soship_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_master, "Qty", "soshipd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column(gv_master, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default) 'pt_desc1
        add_column(gv_master, "Part Number", "pt_desc1", DevExpress.Utils.HorzAlignment.Default) 'pt_desc1
        add_column(gv_master, "Create Date", "soship_add_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_master, "Create By", "soship_add_by", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String
      
        get_sequel = "SELECT  " _
                & "  soship_oid, " _
                & "  soship_dom_id, " _
                & "  soship_en_id, " _
                & "  soship_add_by, " _
                & "  soship_add_date, " _
                & "  soship_upd_by, " _
                & "  soship_upd_date, " _
                & "  soship_code, " _
                & "  soship_date, " _
                & "  soship_so_oid, " _
                & "  soship_si_id, " _
                & "  soship_is_shipment, " _
                & "  soship_dt, " _
                & "  so_code, " _
                & "  ptnr_name as ptnr_name_sold, " _
                & "  en_desc, " _
                & "  si_desc, " _
                & "  soshipd_oid, " _
                & "  soshipd_soship_oid, " _
                & "  soshipd_sod_oid, " _
                & "  soshipd_seq, " _
                & "  soshipd_qty * -1 as soshipd_qty, " _
                & "  soshipd_um, " _
                & "  soshipd_um_conv, " _
                & "  soshipd_cancel_bo, " _
                & "  soshipd_qty_real * -1 as soshipd_qty_real, " _
                & "  soshipd_qty_inv, " _
                & "  soshipd_qty_allocated, " _
                & "  soshipd_si_id, " _
                & "  soshipd_loc_id, " _
                & "  soshipd_lot_serial, " _
                & "  soshipd_rea_code_id, " _
                & "  soshipd_dt, " _
                & "  pt_id, " _
                & "  pt_code, " _
                & "  pt_desc1, " _
                & "  pt_desc2, " _
                & "  pt_ls, " _
                & "  si_desc, " _
                & "  loc_desc, " _
                & "  um_mstr.code_name as soshipd_um_name " _
                & "FROM  " _
                & "  public.soship_mstr " _
                & "  inner join so_mstr on so_oid = soship_so_oid " _
                & "  inner join en_mstr on en_id = soship_en_id " _
                & "  inner join si_mstr on si_id = soship_si_id " _
                & "  inner join ptnr_mstr on ptnr_id = so_ptnr_id_sold" _
                & "  inner join soshipd_det on soship_oid = soshipd_soship_oid" _
                & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                & "  inner join pt_mstr on pt_id = sod_pt_id " _
                & "  inner join loc_mstr on loc_id = soshipd_loc_id " _
                & "  inner join code_mstr um_mstr on um_mstr.code_id = soshipd_um" _
                & " where soship_date >= " + SetDate(par_tgl_awal) _
                & " and soship_date <= " + SetDate(par_tgl_akhir) _
                & " and pt_code='" & par_pt_code & "'" _
                & " and soship_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()
    End Sub

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        Me.Close()
    End Sub

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            Me.Close()
        End If
    End Sub

   
End Class
