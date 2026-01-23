Imports master_new.PGSqlConn
Imports npgsql
Imports master_new.ModFunction

Public Class F3CReportAnalisys
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection

    Private Sub F3CReportAnalisys_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_data("entity", ""))
        'le_en_id.Properties.DataSource = dt_bantu
        'le_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'le_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'le_en_id.ItemIndex = 0

        init_le(le_en_id, "en_id")
        de_from.DateTime = Now.Date
        xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False
        format_grid()

    End Sub

    Private Sub le_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_en_id.EditValueChanged
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_loc_mstr())
        'le_loc.Properties.DataSource = dt_bantu
        'le_loc.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        'le_loc.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        'le_loc.ItemIndex = 0
    End Sub

    Public Overrides Sub help_load_data(ByVal arg As Boolean)

        Cursor = Cursors.WaitCursor

        Dim _en_id_all As String

        If ce_child.EditValue = True Then
            _en_id_all = get_en_id_child(le_en_id.EditValue)
        Else
            _en_id_all = le_en_id.EditValue

        End If


        Dim ssql As String
        Dim _status As String = ""

        'ssql = "select invcs_oid,invcs_date from invcs_mstr where to_char(invcs_date,'yyyyMMdd') = " & SetSetring(Format(de_from.DateTime, "yyyyMMdd")) _
        '    & " order by invcs_date desc limit 1"

        'Dim dt As New DataTable
        'dt = GetTableData(ssql)

        'Dim _date As Date
        'Dim _oid As String = ""

        'If dt.Rows.Count = 0 Then
        '    ssql = "select invcs_oid,invcs_date from invcs_mstr where to_char(invcs_date,'yyyyMMdd') > " & SetSetring(Format(de_from.DateTime, "yyyyMMdd")) _
        '         & " order by invcs_date asc limit 1"
        '    dt = GetTableData(ssql)
        '    If dt.Rows.Count > 0 Then
        '        _date = dt.Rows(0).Item("invcs_date")
        '        _oid = dt.Rows(0).Item("invcs_oid")
        '        _status = ">"
        '    End If
        '    If _status = "" Then
        '        _date = master_new.PGSqlConn.CekTanggal
        '        _oid = ""
        '        _status = "<"
        '    End If

        'Else
        '    _date = dt.Rows(0).Item("invcs_date")
        '    _oid = dt.Rows(0).Item("invcs_oid")
        '    _status = "="
        'End If


        'If arg <> False Then
        '    If _status = "<" Then

        '        If de_from.DateTime.Date < CekTanggal.Date Then
        '            ssql = "select sum(inv_qty) as inv_qty,invc_pt_id,pt_code,pt_desc1,pt_desc2 ,invc_loc_id,loc_desc, '#First Stock' as invh_group from (SELECT  " _
        '                  & "  invc_pt_id, " _
        '                  & "  sum(invc_qty) as inv_qty," _
        '                  & "  pt_code, " _
        '                  & "  pt_desc1, " _
        '                  & "  pt_desc2,invc_loc_id, " _
        '                  & "  d.loc_desc,en_desc " _
        '                  & "FROM  " _
        '                  & "  invc_mstr " _
        '                  & "  inner join pt_mstr on pt_id = invc_pt_id " _
        '                   & "  inner join en_mstr on invc_en_id = en_id " _
        '                  & "  INNER JOIN public.loc_mstr d ON (invc_loc_id = d.loc_id) " _
        '                  & "  where invc_qty <> 0 and invc_en_id in ( " & _en_id_all & " ) "

        '            If SetString(be_loc.Tag) <> "" Then
        '                ssql = ssql & " and invc_loc_id = " & SetInteger(be_loc.Tag.ToString)
        '            End If


        '            ssql = ssql & "  group by  " _
        '               & "  invc_pt_id, " _
        '               & "  pt_code, " _
        '               & "  pt_desc1, " _
        '               & "  pt_desc2,invc_loc_id,loc_desc,en_desc " _
        '               & " union all " _
        '               & "SELECT   " _
        '                & "  a.invh_pt_id as invc_pt_id, " _
        '                & "  a.invh_qty * -1 as inv_qty, " _
        '                & "  b.pt_code, " _
        '                & "  b.pt_desc1, " _
        '                & "  b.pt_desc2, invh_loc_id as invc_loc_id, " _
        '                & "  d.loc_desc,en_desc " _
        '                & "FROM " _
        '                & "  public.invh_mstr a " _
        '                & "  INNER JOIN public.pt_mstr b ON (a.invh_pt_id = b.pt_id) " _
        '                 & "  inner join en_mstr on invh_en_id = en_id " _
        '                & "  INNER JOIN public.loc_mstr d ON (invh_loc_id = d.loc_id) " _
        '                & "WHERE " _
        '                & "  a.invh_en_id in ( " & _en_id_all & " ) "

        '            If SetString(be_loc.Tag) <> "" Then
        '                ssql = ssql & " and invh_loc_id = " & SetInteger(be_loc.Tag.ToString)
        '            End If

        '            ssql = ssql & " and a.invh_date >= " & SetDateNTime00(de_from.DateTime) & " ) as t_temp " _
        '            & " group by invc_pt_id,pt_code,pt_desc1,pt_desc2,invc_loc_id,loc_desc,en_desc "
        '        Else

        '            ssql = "SELECT  " _
        '              & "  invc_pt_id, " _
        '              & "  sum(invc_qty) as inv_qty," _
        '              & "  pt_code, " _
        '              & "  pt_desc1, " _
        '              & "  pt_desc2,invc_loc_id, " _
        '              & "  d.loc_desc,en_desc,'#First Stock' as invh_group " _
        '              & "FROM  " _
        '              & "  invc_mstr " _
        '              & "  inner join pt_mstr on pt_id = invc_pt_id " _
        '               & "  inner join en_mstr on invc_en_id = en_id " _
        '              & "  INNER JOIN public.loc_mstr d ON (invc_loc_id = d.loc_id) " _
        '              & "  where invc_qty <> 0 and invc_en_id in ( " & _en_id_all & " ) "

        '            If SetString(be_loc.Tag) <> "" Then
        '                ssql = ssql & " and invc_loc_id = " & SetInteger(be_loc.Tag.ToString)
        '            End If


        '            ssql = ssql & "  group by  " _
        '               & "  invc_pt_id, " _
        '               & "  pt_code, " _
        '               & "  pt_desc1, " _
        '               & "  pt_desc2,invc_loc_id,loc_desc,en_desc "

        '        End If



        '    ElseIf _status = "=" Then
        '            ssql = "SELECT   " _
        '             & "  b.invcsd_pt_id, " _
        '             & "  b.invcsd_qty as inv_qty, " _
        '             & "  c.pt_code, " _
        '             & "  c.pt_desc1, " _
        '             & "  c.pt_desc2, " _
        '             & "  b.invcsd_loc_id as invc_loc_id, " _
        '             & "  d.loc_desc, '#Stock " & Format(_date, "D") & " (Inv Save)' as invh_group,en_desc " _
        '             & "FROM " _
        '             & "  public.invcsd_det b " _
        '             & "  INNER JOIN public.invcs_mstr a ON (b.invcsd_invcs_oid = a.invcs_oid) " _
        '             & "  INNER JOIN public.pt_mstr c ON (b.invcsd_pt_id = c.pt_id) " _
        '             & "  INNER JOIN public.loc_mstr d ON (b.invcsd_loc_id = d.loc_id) " _
        '              & "  inner join en_mstr on invcsd_en_id = en_id " _
        '             & "WHERE " _
        '             & "b.invcsd_qty <> 0 AND " _
        '             & "  a.invcs_oid = " & SetSetring(_oid) & " AND  " _
        '             & "  b.invcsd_en_id in ( " & _en_id_all & " ) "

        '            If SetString(be_loc.Tag) <> "" Then
        '                ssql = ssql & " and invcsd_loc_id = " & SetInteger(be_loc.Tag.ToString)
        '            End If

        '    ElseIf _status = ">" Then
        '            ssql = "select sum(inv_qty) as inv_qty,invc_pt_id,pt_code,pt_desc1,pt_desc2 ,invc_loc_id,loc_desc, '#First Stock' as invh_group,en_desc from (SELECT   " _
        '              & "  b.invcsd_pt_id as invc_pt_id, " _
        '              & "  b.invcsd_qty as inv_qty, " _
        '              & "  c.pt_code, " _
        '              & "  c.pt_desc1, " _
        '              & "  c.pt_desc2, " _
        '              & "  b.invcsd_loc_id as invc_loc_id, " _
        '              & "  d.loc_desc, '#Stock " & Format(_date, "D") & " (Inv Save)' as invh_group,en_desc " _
        '              & "FROM " _
        '              & "  public.invcsd_det b " _
        '              & "  INNER JOIN public.invcs_mstr a ON (b.invcsd_invcs_oid = a.invcs_oid) " _
        '              & "  INNER JOIN public.pt_mstr c ON (b.invcsd_pt_id = c.pt_id) " _
        '              & "  INNER JOIN public.loc_mstr d ON (b.invcsd_loc_id = d.loc_id) " _
        '              & "  inner join en_mstr on invcsd_en_id = en_id " _
        '              & "WHERE " _
        '              & "b.invcsd_qty <> 0 AND " _
        '              & "  a.invcs_oid = " & SetSetring(_oid) & " AND  " _
        '              & "  b.invcsd_en_id in ( " & _en_id_all & " ) "

        '            If SetString(be_loc.Tag) <> "" Then
        '                ssql = ssql & " and invcsd_loc_id = " & SetInteger(be_loc.Tag.ToString)
        '            End If

        '            ssql = ssql & " union all " _
        '               & "SELECT   " _
        '                & "  a.invh_pt_id as invc_pt_id, " _
        '                & "  a.invh_qty * -1 as inv_qty, " _
        '                & "  b.pt_code, " _
        '                & "  b.pt_desc1, " _
        '                & "  b.pt_desc2, invh_loc_id as invc_loc_id, " _
        '                & "  d.loc_desc, a.invh_desc as invh_group,en_desc " _
        '                & "FROM " _
        '                & "  public.invh_mstr a " _
        '                & "  INNER JOIN public.pt_mstr b ON (a.invh_pt_id = b.pt_id) " _
        '                & "  INNER JOIN public.loc_mstr d ON (invh_loc_id = d.loc_id) " _
        '                & "  inner join en_mstr on invh_en_id = en_id " _
        '                & "WHERE " _
        '                & "  a.invh_en_id in ( " & _en_id_all & " ) "

        '            If SetString(be_loc.Tag) <> "" Then
        '                ssql = ssql & " and invh_loc_id = " & SetInteger(be_loc.Tag.ToString)
        '            End If

        '            ssql = ssql & " and a.invh_date >= " & SetDateNTime00(de_from.DateTime) & " and a.invh_date <= " & SetDateNTime00(_date) & " ) as t_temp " _
        '            & " group by invc_pt_id,pt_code,pt_desc1,pt_desc2,invc_loc_id,loc_desc,en_desc "

        '    End If


        ssql = "SELECT  " _
          & "  invc_pt_id, " _
          & "  sum(invc_qty) as inv_qty," _
          & "  pt_code, " _
          & "  pt_desc1, " _
          & "  pt_desc2,invc_loc_id, " _
          & "  d.loc_desc,invc_en_id,en_desc,'#First Stock' as invh_group, 0 as po_rcv, 0 as po_rtr, 0 as so_ship, 0 as so_rtr, 0 as tr_out, 0 as tr_git_out, 0 as git_rcv, 0 as tr_rcv, 0 as inv_issue, 0 as inv_rcv, sum(invc_qty)  as opening " _
          & "FROM  " _
          & "  invc_mstr " _
          & "  inner join pt_mstr on pt_id = invc_pt_id " _
           & "  inner join en_mstr on invc_en_id = en_id " _
          & "  INNER JOIN public.loc_mstr d ON (invc_loc_id = d.loc_id) " _
          & "  where invc_en_id in ( " & _en_id_all & " ) "

        If SetString(be_loc.Tag) <> "" Then
            ssql = ssql & " and invc_loc_id = " & SetInteger(be_loc.Tag.ToString)
        End If


        ssql = ssql & "  group by  " _
           & "  invc_pt_id, " _
           & "  pt_code, " _
           & "  pt_desc1, " _
           & "  pt_desc2,invc_loc_id,loc_desc,invc_en_id,en_desc "



        Dim dt_inv As New DataTable
        dt_inv = GetTableData(ssql)


        'po receipt
        ssql = "SELECT  " _
            & "  sum(public.rcvd_det.rcvd_qty_real) as qty_inv, " _
            & "  public.rcvd_det.rcvd_loc_id, " _
            & "  public.pod_det.pod_pt_id,rcv_en_id " _
            & "FROM " _
            & "  public.rcv_mstr " _
            & "  INNER JOIN public.rcvd_det ON (public.rcv_mstr.rcv_oid = public.rcvd_det.rcvd_rcv_oid) " _
            & "  INNER JOIN public.pod_det ON (public.rcvd_det.rcvd_pod_oid = public.pod_det.pod_oid) " _
            & "WHERE " _
            & "  public.rcvd_det.rcvd_qty_real > 0 and public.rcv_mstr.rcv_en_id IN ( " & _en_id_all & " )  And " _
            & "  public.rcv_mstr.rcv_eff_date BETWEEN " & SetDateNTime00(de_from.DateTime) & " AND current_date "


        If SetString(be_loc.Tag) <> "" Then
            ssql = ssql & " and public.rcvd_det.rcvd_loc_id = " & SetInteger(be_loc.Tag.ToString)
        End If


        ssql = ssql & " GROUP BY " _
            & "  rcv_en_id,public.rcvd_det.rcvd_loc_id, " _
            & "  public.pod_det.pod_pt_id"

        Dim dt_po As New DataTable
        dt_po = GetTableData(ssql)


        For Each dr As DataRow In dt_inv.Rows
            For Each dr_po As DataRow In dt_po.Rows
                If dr("invc_pt_id") = dr_po("pod_pt_id") And dr("invc_loc_id") = dr_po("rcvd_loc_id") And dr("invc_en_id") = dr_po("rcv_en_id") Then
                    dr("opening") = SetNumber(dr("opening")) - SetNumber(dr_po("qty_inv"))
                    dr("po_rcv") = dr("po_rcv") + SetNumber(dr_po("qty_inv"))
                    Exit For
                End If
            Next

        Next

        dt_inv.AcceptChanges()


        'po return
        ssql = "SELECT  " _
           & "  sum(public.rcvd_det.rcvd_qty_real) as qty_inv, " _
           & "  public.rcvd_det.rcvd_loc_id, " _
           & "  public.pod_det.pod_pt_id,rcv_en_id " _
           & "FROM " _
           & "  public.rcv_mstr " _
           & "  INNER JOIN public.rcvd_det ON (public.rcv_mstr.rcv_oid = public.rcvd_det.rcvd_rcv_oid) " _
           & "  INNER JOIN public.pod_det ON (public.rcvd_det.rcvd_pod_oid = public.pod_det.pod_oid) " _
           & "WHERE " _
           & "  public.rcvd_det.rcvd_qty_real < 0 and public.rcv_mstr.rcv_en_id IN ( " & _en_id_all & " )  And " _
           & "  public.rcv_mstr.rcv_eff_date BETWEEN " & SetDateNTime00(de_from.DateTime) & " AND current_date "


        If SetString(be_loc.Tag) <> "" Then
            ssql = ssql & " and public.rcvd_det.rcvd_loc_id = " & SetInteger(be_loc.Tag.ToString)
        End If


        ssql = ssql & " GROUP BY " _
            & "  rcv_en_id,public.rcvd_det.rcvd_loc_id, " _
            & "  public.pod_det.pod_pt_id"

        Dim dt_poreturn As New DataTable
        dt_poreturn = GetTableData(ssql)

        'ditambah krn minus

        For Each dr As DataRow In dt_inv.Rows
            For Each dr_po As DataRow In dt_poreturn.Rows
                If dr("invc_pt_id") = dr_po("pod_pt_id") And dr("invc_loc_id") = dr_po("rcvd_loc_id") And dr("invc_en_id") = dr_po("rcv_en_id") Then
                    dr("opening") = SetNumber(dr("opening")) + (SetNumber(dr_po("qty_inv")) * -1.0)
                    dr("po_rtr") = dr("po_rtr") + (SetNumber(dr_po("qty_inv")) * -1.0)
                    Exit For
                End If
            Next

        Next

        dt_inv.AcceptChanges()

        'inv receipt
        ssql = "SELECT  " _
            & "  public.riud_det.riud_pt_id, " _
            & "  sum(public.riud_det.riud_qty_real) as inv_qty, " _
            & "  public.riud_det.riud_loc_id,riu_en_id " _
            & "FROM " _
            & "  public.riu_mstr " _
            & "  INNER JOIN public.riud_det ON (public.riu_mstr.riu_oid = public.riud_det.riud_riu_oid) " _
            & " WHERE " _
            & "  public.riud_det.riud_qty_real > 0 and public.riu_mstr.riu_en_id IN ( " & _en_id_all _
            & " ) and public.riu_mstr.riu_date BETWEEN " & SetDateNTime00(de_from.DateTime) & " AND current_date  "


        If SetString(be_loc.Tag) <> "" Then
            ssql = ssql & " and public.riud_det.riud_loc_id = " & SetInteger(be_loc.Tag.ToString)
        End If


        ssql = ssql & " GROUP BY " _
            & "  riu_en_id,public.riud_det.riud_pt_id, " _
            & "  public.riud_det.riud_loc_id"


        Dim dt_issue As New DataTable
        dt_issue = GetTableData(ssql)


        For Each dr As DataRow In dt_inv.Rows
            For Each dr_issue As DataRow In dt_issue.Rows
                If dr("invc_pt_id") = dr_issue("riud_pt_id") And dr("invc_loc_id") = dr_issue("riud_loc_id") And dr("invc_en_id") = dr_issue("riu_en_id") Then
                    dr("opening") = SetNumber(dr("opening")) - SetNumber(dr_issue("inv_qty"))
                    dr("inv_rcv") = dr("inv_rcv") + SetNumber(dr_issue("inv_qty"))
                    Exit For
                End If
            Next

        Next

        dt_inv.AcceptChanges()



        'inv issue
        ssql = "SELECT  " _
           & "  public.riud_det.riud_pt_id, " _
           & "  sum(public.riud_det.riud_qty_real) as inv_qty, " _
           & "  public.riud_det.riud_loc_id,riu_en_id " _
           & "FROM " _
           & "  public.riu_mstr " _
           & "  INNER JOIN public.riud_det ON (public.riu_mstr.riu_oid = public.riud_det.riud_riu_oid) " _
           & " WHERE " _
           & "  public.riud_det.riud_qty_real < 0 and public.riu_mstr.riu_en_id IN ( " & _en_id_all _
           & " ) and public.riu_mstr.riu_date BETWEEN " & SetDateNTime00(de_from.DateTime) & " AND current_date  "


        If SetString(be_loc.Tag) <> "" Then
            ssql = ssql & " and public.riud_det.riud_loc_id = " & SetInteger(be_loc.Tag.ToString)
        End If


        ssql = ssql & " GROUP BY " _
            & "  riu_en_id,public.riud_det.riud_pt_id, " _
            & "  public.riud_det.riud_loc_id"


        Dim dt_issue2 As New DataTable
        dt_issue2 = GetTableData(ssql)


        For Each dr As DataRow In dt_inv.Rows
            For Each dr_issue As DataRow In dt_issue2.Rows
                If dr("invc_pt_id") = dr_issue("riud_pt_id") And dr("invc_loc_id") = dr_issue("riud_loc_id") And dr("invc_en_id") = dr_issue("riu_en_id") Then
                    dr("opening") = SetNumber(dr("opening")) + (SetNumber(dr_issue("inv_qty")) * -1.0)
                    dr("inv_issue") = dr("inv_issue") + (SetNumber(dr_issue("inv_qty")) * -1.0)
                    Exit For
                End If
            Next

        Next

        dt_inv.AcceptChanges()




        'transfer issue asal
        ssql = "SELECT  " _
            & "  sum(public.ptsfrd_det.ptsfrd_qty) as inv_qty, " _
            & "  public.ptsfrd_det.ptsfrd_pt_id, " _
            & "  public.ptsfr_mstr.ptsfr_loc_id,ptsfr_en_id " _
            & "FROM " _
            & "  public.ptsfr_mstr " _
            & "  INNER JOIN public.ptsfrd_det ON (public.ptsfr_mstr.ptsfr_oid = public.ptsfrd_det.ptsfrd_ptsfr_oid) " _
            & "WHERE " _
            & "  public.ptsfr_mstr.ptsfr_date BETWEEN " & SetDateNTime00(de_from.DateTime) & " AND current_date AND  " _
            & "  public.ptsfr_mstr.ptsfr_en_id IN ( " & _en_id_all _
            & " ) "

        If SetString(be_loc.Tag) <> "" Then
            ssql = ssql & " and public.ptsfr_mstr.ptsfr_loc_id = " & SetInteger(be_loc.Tag.ToString)
        End If


        ssql = ssql & "GROUP BY " _
            & "  public.ptsfr_mstr.ptsfr_en_id, " _
            & "  public.ptsfrd_det.ptsfrd_pt_id, " _
            & "  public.ptsfr_mstr.ptsfr_loc_id"


        Dim dt_trfout As New DataTable
        dt_trfout = GetTableData(ssql)


        For Each dr As DataRow In dt_inv.Rows
            For Each trfout As DataRow In dt_trfout.Rows
                If dr("invc_pt_id") = trfout("ptsfrd_pt_id") And dr("invc_loc_id") = trfout("ptsfr_loc_id") And dr("invc_en_id") = trfout("ptsfr_en_id") Then
                    dr("opening") = SetNumber(dr("opening")) + SetNumber(trfout("inv_qty"))
                    dr("tr_out") = dr("tr_out") + SetNumber(trfout("inv_qty"))
                    Exit For
                End If
            Next

        Next

        dt_inv.AcceptChanges()



        ssql = "SELECT  " _
           & "  sum(public.ptsfrd_det.ptsfrd_qty) as inv_qty, " _
           & "  public.ptsfrd_det.ptsfrd_pt_id, " _
           & "  public.ptsfr_mstr.ptsfr_loc_git,ptsfr_en_id " _
           & "FROM " _
           & "  public.ptsfr_mstr " _
           & "  INNER JOIN public.ptsfrd_det ON (public.ptsfr_mstr.ptsfr_oid = public.ptsfrd_det.ptsfrd_ptsfr_oid) " _
           & "WHERE " _
           & "  public.ptsfr_mstr.ptsfr_date BETWEEN " & SetDateNTime00(de_from.DateTime) & " AND current_date AND  " _
           & "  public.ptsfr_mstr.ptsfr_en_id IN ( " & _en_id_all _
           & " ) "

        If SetString(be_loc.Tag) <> "" Then
            ssql = ssql & " and public.ptsfr_mstr.ptsfr_loc_git = " & SetInteger(be_loc.Tag.ToString)
        End If


        ssql = ssql & " GROUP BY " _
           & "  public.ptsfr_mstr.ptsfr_en_id, " _
           & "  public.ptsfrd_det.ptsfrd_pt_id, " _
           & "  public.ptsfr_mstr.ptsfr_loc_git"


        Dim dt_trfout_git As New DataTable
        dt_trfout_git = GetTableData(ssql)


        For Each dr As DataRow In dt_inv.Rows
            For Each trfout As DataRow In dt_trfout_git.Rows
                If dr("invc_pt_id") = trfout("ptsfrd_pt_id") And dr("invc_loc_id") = trfout("ptsfr_loc_git") And dr("invc_en_id") = trfout("ptsfr_en_id") Then
                    dr("opening") = SetNumber(dr("opening")) - SetNumber(trfout("inv_qty"))
                    dr("git_rcv") = dr("git_rcv") + SetNumber(trfout("inv_qty"))
                    Exit For
                End If
            Next

        Next

        dt_inv.AcceptChanges()




        ssql = "SELECT  " _
            & "  sum(public.ptsfrd_det.ptsfrd_qty_receive) as inv_qty, " _
            & "  public.ptsfrd_det.ptsfrd_pt_id, " _
            & "  public.ptsfr_mstr.ptsfr_loc_to_id,ptsfr_en_to_id " _
            & "FROM " _
            & "  public.ptsfr_mstr " _
            & "  INNER JOIN public.ptsfrd_det ON (public.ptsfr_mstr.ptsfr_oid = public.ptsfrd_det.ptsfrd_ptsfr_oid) " _
            & "WHERE " _
            & "  public.ptsfr_mstr.ptsfr_date BETWEEN " & SetDateNTime00(de_from.DateTime) & " AND current_date AND  " _
            & "  public.ptsfr_mstr.ptsfr_en_to_id IN ( " & _en_id_all _
            & " ) " _
            & "  and ptsfr_trans_id=('C') "

        If SetString(be_loc.Tag) <> "" Then
            ssql = ssql & " and public.ptsfr_mstr.ptsfr_loc_to_id = " & SetInteger(be_loc.Tag.ToString)
        End If


        ssql = ssql & "GROUP BY " _
            & "  public.ptsfr_mstr.ptsfr_en_to_id, " _
            & "  public.ptsfrd_det.ptsfrd_pt_id, " _
            & "  public.ptsfr_mstr.ptsfr_loc_to_id"


        Dim dt_trfrcv As New DataTable
        dt_trfrcv = GetTableData(ssql)


        For Each dr As DataRow In dt_inv.Rows
            For Each trfout As DataRow In dt_trfrcv.Rows
                If dr("invc_pt_id") = trfout("ptsfrd_pt_id") And dr("invc_loc_id") = trfout("ptsfr_loc_to_id") And dr("invc_en_id") = trfout("ptsfr_en_to_id") Then
                    dr("opening") = SetNumber(dr("opening")) - SetNumber(trfout("inv_qty"))
                    dr("tr_rcv") = dr("tr_rcv") + SetNumber(trfout("inv_qty"))
                    'dr("git_rcv") = dr("git_rcv") - SetNumber(trfout("inv_qty"))
                    Exit For
                End If
            Next

        Next

        dt_inv.AcceptChanges()


        ssql = "SELECT  " _
         & "  sum(public.ptsfrd_det.ptsfrd_qty_receive) as inv_qty, " _
         & "  public.ptsfrd_det.ptsfrd_pt_id, " _
         & "  public.ptsfr_mstr.ptsfr_loc_git,ptsfr_en_id " _
         & "FROM " _
         & "  public.ptsfr_mstr " _
         & "  INNER JOIN public.ptsfrd_det ON (public.ptsfr_mstr.ptsfr_oid = public.ptsfrd_det.ptsfrd_ptsfr_oid) " _
         & "WHERE " _
         & "  public.ptsfr_mstr.ptsfr_date BETWEEN " & SetDateNTime00(de_from.DateTime) & " AND current_date AND  " _
         & "  public.ptsfr_mstr.ptsfr_en_id IN ( " & _en_id_all _
         & " ) " _
        & "  and ptsfr_trans_id=('C') "

        If SetString(be_loc.Tag) <> "" Then
            ssql = ssql & " and public.ptsfr_mstr.ptsfr_loc_git = " & SetInteger(be_loc.Tag.ToString)
        End If


        ssql = ssql & " GROUP BY " _
           & "  public.ptsfr_mstr.ptsfr_en_id, " _
           & "  public.ptsfrd_det.ptsfrd_pt_id, " _
           & "  public.ptsfr_mstr.ptsfr_loc_git"


        Dim dt_trfrcv_git As New DataTable
        dt_trfrcv_git = GetTableData(ssql)


        For Each dr As DataRow In dt_inv.Rows
            For Each trfout As DataRow In dt_trfrcv_git.Rows
                If dr("invc_pt_id") = trfout("ptsfrd_pt_id") And dr("invc_loc_id") = trfout("ptsfr_loc_git") And dr("invc_en_id") = trfout("ptsfr_en_id") Then
                    dr("opening") = SetNumber(dr("opening")) + SetNumber(trfout("inv_qty"))
                    dr("tr_git_out") = dr("tr_git_out") + SetNumber(trfout("inv_qty"))
                    Exit For
                End If
            Next

        Next

        dt_inv.AcceptChanges()

        ssql = "SELECT  " _
            & "  public.sod_det.sod_pt_id, " _
            & "  sum(public.soshipd_det.soshipd_qty_real) as inv_qty, " _
            & "  public.soship_mstr.soship_en_id, " _
            & "  public.soshipd_det.soshipd_loc_id " _
            & "FROM " _
            & "  public.soship_mstr " _
            & "  INNER JOIN public.soshipd_det ON (public.soship_mstr.soship_oid = public.soshipd_det.soshipd_soship_oid) " _
            & "  INNER JOIN public.sod_det ON (public.soshipd_det.soshipd_sod_oid = public.sod_det.sod_oid) " _
            & "WHERE " _
            & "  public.soshipd_det.soshipd_qty_real > 0 and public.soship_mstr.soship_date BETWEEN " & SetDateNTime00(de_from.DateTime) & " AND current_date AND  " _
            & "  public.soship_mstr.soship_en_id IN ( " & _en_id_all _
            & " ) "


        If SetString(be_loc.Tag) <> "" Then
            ssql = ssql & " and public.soshipd_det.soshipd_loc_id = " & SetInteger(be_loc.Tag.ToString)
        End If


        ssql = ssql & " GROUP BY " _
            & "  public.sod_det.sod_pt_id, " _
            & "  public.soship_mstr.soship_en_id, " _
            & "  public.soshipd_det.soshipd_loc_id"


        Dim dt_soship As New DataTable
        dt_soship = GetTableData(ssql)


        For Each dr As DataRow In dt_inv.Rows
            For Each trfout As DataRow In dt_soship.Rows
                If dr("invc_pt_id") = trfout("sod_pt_id") And dr("invc_loc_id") = trfout("soshipd_loc_id") And dr("invc_en_id") = trfout("soship_en_id") Then
                    dr("opening") = SetNumber(dr("opening")) + SetNumber(trfout("inv_qty"))
                    dr("so_ship") = dr("so_ship") + SetNumber(trfout("inv_qty"))
                    Exit For
                End If
            Next

        Next

        dt_inv.AcceptChanges()


        ssql = "SELECT  " _
            & "  public.sod_det.sod_pt_id, " _
            & "  sum(public.soshipd_det.soshipd_qty_real) as inv_qty, " _
            & "  public.soship_mstr.soship_en_id, " _
            & "  public.soshipd_det.soshipd_loc_id " _
            & "FROM " _
            & "  public.soship_mstr " _
            & "  INNER JOIN public.soshipd_det ON (public.soship_mstr.soship_oid = public.soshipd_det.soshipd_soship_oid) " _
            & "  INNER JOIN public.sod_det ON (public.soshipd_det.soshipd_sod_oid = public.sod_det.sod_oid) " _
            & "WHERE " _
            & "  public.soshipd_det.soshipd_qty_real < 0 and public.soship_mstr.soship_date BETWEEN " & SetDateNTime00(de_from.DateTime) & " AND current_date AND  " _
            & "  public.soship_mstr.soship_en_id IN ( " & _en_id_all _
            & " ) "


        If SetString(be_loc.Tag) <> "" Then
            ssql = ssql & " and public.soshipd_det.soshipd_loc_id = " & SetInteger(be_loc.Tag.ToString)
        End If


        ssql = ssql & " GROUP BY " _
            & "  public.sod_det.sod_pt_id, " _
            & "  public.soship_mstr.soship_en_id, " _
            & "  public.soshipd_det.soshipd_loc_id"


        Dim dt_soship2 As New DataTable
        dt_soship2 = GetTableData(ssql)


        For Each dr As DataRow In dt_inv.Rows
            For Each trfout As DataRow In dt_soship2.Rows
                If dr("invc_pt_id") = trfout("sod_pt_id") And dr("invc_loc_id") = trfout("soshipd_loc_id") And dr("invc_en_id") = trfout("soship_en_id") Then
                    dr("opening") = SetNumber(dr("opening")) - (SetNumber(trfout("inv_qty")) * -1.0)
                    dr("so_ship") = dr("so_ship") + SetNumber(trfout("inv_qty")) * -1.0
                    Exit For
                End If
            Next

        Next

        dt_inv.AcceptChanges()


        gc_loc.DataSource = dt_inv
        gv_loc.OptionsView.ColumnAutoWidth = False
        'relation_detail()
        bestfit_column()
        ConditionsAdjustment()

        'Else

        'End If





        'End If
        Cursor = Cursors.Arrow
    End Sub
    Public Overrides Sub format_grid()
        add_column_copy(gv_loc, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Deskripsi1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Deskripsi2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_loc, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_loc, "Lot Number", "invcsd_serial", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_loc, "Last Stock", "inv_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        add_column_copy(gv_loc, "PO Receipt", "po_rcv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_loc, "PO Return", "po_rtr", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_loc, "SO Shipment", "so_ship", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_loc, "SO Return", "so_rtr", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_loc, "Transfer Issue", "tr_out", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_loc, "Transfer Issue GIT", "tr_git_out", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_loc, "Transfer Receipt GIT", "git_rcv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_loc, "Transfer Receipt", "tr_rcv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_loc, "Inventory Issue", "inv_issue", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_loc, "Inventiry Receipt", "inv_rcv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_loc, "Opening Stock", "opening", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")


        'add_column_copy(gv_loc, "Qty On Old", "inv_old", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        ' add_column_copy(gv_loc, "Unit Measure", "um_desc", DevExpress.Utils.HorzAlignment.Default)



    End Sub

    Private Sub be_loc_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_loc.ButtonClick
        Dim frm As New FLocationSearch()
        frm.set_win(Me)
        frm._en_id = le_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub
End Class
