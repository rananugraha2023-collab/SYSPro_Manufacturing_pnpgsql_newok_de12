Imports master_new.ModFunction
Imports npgsql

Public Class FDistributionPrint
    Public dt_bantu As DataTable
    Public func_data As New function_data

    Private Sub FInvReceiptsPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        date_first.DateTime = Now.Date
        date_last.DateTime = Now.Date
    End Sub

    Public Overrides Sub preview()
        'Dim _en_id As Integer
        'Dim _type, _table, _initial, _code_awal, _code_akhir As String
        Dim func_coll As New function_collection

        
        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "  a.pt_id, " _
            & "  a.pt_code, " _
            & "  a.pt_desc1, " _
            & "  a.pt_desc2, " _
            & "  coalesce((SELECT sum(y.soshipd_qty *-1) as qty FROM public.soship_mstr x INNER JOIN public.soshipd_det y ON(x.soship_oid = y.soshipd_soship_oid) INNER JOIN public.sod_det z ON(y.soshipd_sod_oid = z.sod_oid) WHERE x.soship_en_id = 4 AND y.soshipd_loc_id = 301 AND z.sod_pt_id = a.pt_id AND x.soship_date BETWEEN " & SetDateNTime00(date_first.DateTime) & " AND " & SetDateNTime00(date_last.DateTime) & "), 0) AS so_shipment_pusat, " _
            & "  coalesce((SELECT sum(y.ptsfrd_qty) as qty FROM public.ptsfr_mstr x INNER JOIN public.ptsfrd_det y ON(x.ptsfr_oid = y.ptsfrd_ptsfr_oid) WHERE y.ptsfrd_pt_id = a.pt_id AND x.ptsfr_en_id = 4 AND x.ptsfr_en_to_id = 4 AND x.ptsfr_date BETWEEN " & SetDateNTime00(date_first.DateTime) & " AND " & SetDateNTime00(date_last.DateTime) & "), 0) AS transfer_en_pst, " _
            & "  coalesce((SELECT sum(y.ptsfrd_qty) as qty FROM public.ptsfr_mstr x INNER JOIN public.ptsfrd_det y ON(x.ptsfr_oid = y.ptsfrd_ptsfr_oid) WHERE y.ptsfrd_pt_id = a.pt_id AND x.ptsfr_en_id = 4 AND x.ptsfr_en_to_id = 8 AND x.ptsfr_date BETWEEN " & SetDateNTime00(date_first.DateTime) & " AND " & SetDateNTime00(date_last.DateTime) & "), 0) AS transfer_en_bdg, " _
            & "  coalesce((SELECT sum(y.ptsfrd_qty) as qty FROM public.ptsfr_mstr x INNER JOIN public.ptsfrd_det y ON(x.ptsfr_oid = y.ptsfrd_ptsfr_oid) WHERE y.ptsfrd_pt_id = a.pt_id AND x.ptsfr_en_id = 4 AND x.ptsfr_en_to_id = 9 AND x.ptsfr_date BETWEEN " & SetDateNTime00(date_first.DateTime) & " AND " & SetDateNTime00(date_last.DateTime) & "), 0) AS transfer_en_jkt, " _
            & "  coalesce((SELECT sum(y.ptsfrd_qty) as qty FROM public.ptsfr_mstr x INNER JOIN public.ptsfrd_det y ON(x.ptsfr_oid = y.ptsfrd_ptsfr_oid) WHERE y.ptsfrd_pt_id = a.pt_id AND x.ptsfr_en_id = 4 AND x.ptsfr_en_to_id = 15 AND x.ptsfr_date BETWEEN " & SetDateNTime00(date_first.DateTime) & " AND " & SetDateNTime00(date_last.DateTime) & "), 0) AS transfer_en_mksr, " _
            & "  coalesce((SELECT sum(y.ptsfrd_qty) as qty FROM public.ptsfr_mstr x INNER JOIN public.ptsfrd_det y ON(x.ptsfr_oid = y.ptsfrd_ptsfr_oid) WHERE y.ptsfrd_pt_id = a.pt_id AND x.ptsfr_en_id = 4 AND x.ptsfr_en_to_id = 14 AND x.ptsfr_date BETWEEN " & SetDateNTime00(date_first.DateTime) & " AND " & SetDateNTime00(date_last.DateTime) & "), 0) AS transfer_en_mdn, " _
            & "  coalesce((SELECT sum(y.ptsfrd_qty) as qty FROM public.ptsfr_mstr x INNER JOIN public.ptsfrd_det y ON(x.ptsfr_oid = y.ptsfrd_ptsfr_oid) WHERE y.ptsfrd_pt_id = a.pt_id AND x.ptsfr_en_id = 4 AND x.ptsfr_en_to_id = 5 AND x.ptsfr_date BETWEEN " & SetDateNTime00(date_first.DateTime) & " AND " & SetDateNTime00(date_last.DateTime) & "), 0) AS transfer_en_sby, " _
            & "  coalesce((SELECT sum(y.ptsfrd_qty) as qty FROM public.ptsfr_mstr x INNER JOIN public.ptsfrd_det y ON(x.ptsfr_oid = y.ptsfrd_ptsfr_oid) WHERE y.ptsfrd_pt_id = a.pt_id AND x.ptsfr_en_id = 4 AND x.ptsfr_en_to_id = 7 AND x.ptsfr_date BETWEEN " & SetDateNTime00(date_first.DateTime) & " AND " & SetDateNTime00(date_last.DateTime) & "), 0) AS transfer_en_jgj, " _
            & "  coalesce((SELECT sum(y.riud_qty *-1) as qty FROM public.riud_det y INNER JOIN public.riu_mstr x ON(y.riud_riu_oid = x.riu_oid) WHERE y.riud_pt_id = a.pt_id AND x.riu_en_id = 4 AND x.riu_type = 'I' and riu_date BETWEEN " & SetDateNTime00(date_first.DateTime) & " AND " & SetDateNTime00(date_last.DateTime) & "), 0) AS issue_pusat " _
            & "FROM " _
            & "  public.pt_mstr a " _
            & "WHERE " _
            & "  a.pt_code IN (SELECT b.dist_pt_id FROM public.dist_pt b)"

        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "rptDistribusi"
        frm._remarks = date_first.DateTime.Date & " >> " & date_last.DateTime.Date
        frm.ShowDialog()

    End Sub

   
End Class
