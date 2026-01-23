Imports master_new.PGSqlConn
Imports npgsql
Imports master_new.ModFunction

Public Class FInventoryReportDatePivot
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection

    Private Sub FInventoryReportDate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

        ssql = "select invcs_oid,invcs_date from invcs_mstr where to_char(invcs_date,'yyyyMMdd') = " & SetSetring(Format(de_from.DateTime, "yyyyMMdd")) _
            & " order by invcs_date "

        Dim dt As New DataTable
        dt = GetTableData(ssql)

        Dim _date As Date
        Dim _oid As String = ""

        If dt.Rows.Count = 0 Then
            ssql = "select invcs_oid,invcs_date from invcs_mstr where to_char(invcs_date,'yyyyMMdd') > " & SetSetring(Format(de_from.DateTime, "yyyyMMdd")) _
                 & " order by invcs_date asc limit 1"
            dt = GetTableData(ssql)
            If dt.Rows.Count > 0 Then
                _date = dt.Rows(0).Item("invcs_date")
                _oid = dt.Rows(0).Item("invcs_oid").ToString
                _status = ">"
            End If
            If _status = "" Then
                _date = master_new.PGSqlConn.CekTanggal
                _oid = ""
                _status = "<"
            End If
        Else
            _date = dt.Rows(0).Item("invcs_date")
            _oid = dt.Rows(0).Item("invcs_oid").ToString
            _status = "="
        End If


        If arg <> False Then
            If _status = "<" Then
                ssql = "SELECT  " _
                  & "  invc_pt_id, " _
                  & "  sum(invc_qty) as inv_qty," _
                  & "  pt_code, " _
                  & "  pt_desc1, " _
                  & "  pt_desc2,invc_loc_id, " _
                  & "  d.loc_desc,'#Last Stock' as invh_group " _
                  & "FROM  " _
                  & "  invc_mstr " _
                  & "  inner join pt_mstr on pt_id = invc_pt_id " _
                  & "  INNER JOIN public.loc_mstr d ON (invc_loc_id = d.loc_id) " _
                  & "  where invc_qty <> 0 and invc_en_id in ( " & _en_id_all & " ) "

                If SetString(be_loc.Tag) <> "" Then
                    ssql = ssql & " and invc_loc_id = " & SetInteger(be_loc.Tag.ToString)
                End If


                ssql = ssql & "  group by  " _
                   & "  invc_pt_id, " _
                   & "  pt_code, " _
                   & "  pt_desc1, " _
                   & "  pt_desc2,invc_loc_id,loc_desc,invh_group " _
                   & " union all " _
                   & "SELECT   " _
                    & "  a.invh_pt_id as invc_pt_id, " _
                    & "  a.invh_qty * -1 as inv_qty, " _
                    & "  b.pt_code, " _
                    & "  b.pt_desc1, " _
                    & "  b.pt_desc2, invh_loc_id as invc_loc_id, " _
                    & "  d.loc_desc, a.invh_desc as invh_group " _
                    & "FROM " _
                    & "  public.invh_mstr a " _
                    & "  INNER JOIN public.pt_mstr b ON (a.invh_pt_id = b.pt_id) " _
                    & "  INNER JOIN public.loc_mstr d ON (invh_loc_id = d.loc_id) " _
                    & "WHERE " _
                    & "  a.invh_en_id in ( " & _en_id_all & " ) "

                If SetString(be_loc.Tag) <> "" Then
                    ssql = ssql & " and invh_loc_id = " & SetInteger(be_loc.Tag.ToString)
                End If

                ssql = ssql & " and a.invh_date >= " & SetDateNTime00(de_from.DateTime) & " "


            ElseIf _status = "=" Then

                ssql = "SELECT   " _
                    & "  b.invcsd_pt_id, " _
                    & "  b.invcsd_qty as inv_qty, " _
                    & "  c.pt_code, " _
                    & "  c.pt_desc1, " _
                    & "  c.pt_desc2, " _
                    & "  b.invcsd_loc_id as invc_loc_id, " _
                    & "  d.loc_desc, '#Stock " & Format(_date, "D") & " (Inv Save)' as invh_group " _
                    & "FROM " _
                    & "  public.invcsd_det b " _
                    & "  INNER JOIN public.invcs_mstr a ON (b.invcsd_invcs_oid = a.invcs_oid) " _
                    & "  INNER JOIN public.pt_mstr c ON (b.invcsd_pt_id = c.pt_id) " _
                    & "  INNER JOIN public.loc_mstr d ON (b.invcsd_loc_id = d.loc_id) " _
                    & "WHERE " _
                    & "b.invcsd_qty <> 0 AND " _
                    & "  a.invcs_oid = " & SetSetring(_oid) & " AND  " _
                    & "  b.invcsd_en_id in ( " & _en_id_all & " ) "

                If SetString(be_loc.Tag) <> "" Then
                    ssql = ssql & " and invcsd_loc_id = " & SetInteger(be_loc.Tag.ToString)
                End If


            ElseIf _status = ">" Then

                'ssql = "SELECT   " _
                '   & "  b.invcsd_pt_id, " _
                '   & "  b.invcsd_qty as inv_qty, " _
                '   & "  c.pt_code, " _
                '   & "  c.pt_desc1, " _
                '   & "  c.pt_desc2, " _
                '   & "  b.invcsd_loc_id as invc_loc_id, " _
                '   & "  d.loc_desc, '#Stock " & Format(_date, "D") & " (Inv Save)' as invh_group " _
                '   & "FROM " _
                '   & "  public.invcsd_det b " _
                '   & "  INNER JOIN public.invcs_mstr a ON (b.invcsd_invcs_oid = a.invcs_oid) " _
                '   & "  INNER JOIN public.pt_mstr c ON (b.invcsd_pt_id = c.pt_id) " _
                '   & "  INNER JOIN public.loc_mstr d ON (b.invcsd_loc_id = d.loc_id) " _
                '   & "WHERE " _
                '   & "b.invcsd_qty <> 0 AND " _
                '   & "  a.invcs_oid = " & SetSetring(_oid) & " AND  " _
                '   & "  b.invcsd_en_id in ( " & _en_id_all & " ) "


                ssql = "SELECT  " _
                    & "  a.invcsd_pt_id as invc_pt_id,  " _
                    & " a.invcsd_qty as inv_qty, " _
                    & "  c.pt_code, " _
                    & "  c.pt_desc1, " _
                    & "  c.pt_desc2, " _
                    & "  a.invcsd_loc_id as invc_loc_id, " _
                    & "  d.loc_desc, " _
                    & "   " _
                    & "   '#Stock " & Format(_date, "D") & " (Inv Save)' as invh_group  " _
                    & "FROM " _
                    & "  public.invcsd_det a " _
                    & "  INNER JOIN public.en_mstr b ON (a.invcsd_en_id = b.en_id) " _
                    & "  INNER JOIN public.si_mstr e ON (a.invcsd_si_id = e.si_id) " _
                    & "  INNER JOIN public.loc_mstr d ON (a.invcsd_loc_id = d.loc_id) " _
                    & "  INNER JOIN public.pt_mstr c ON (a.invcsd_pt_id = c.pt_id) " _
                    & "  INNER JOIN public.code_mstr f ON (c.pt_um = f.code_id) " _
                    & "WHERE " _
                    & "  a.invcsd_invcs_oid =" & SetSetring(_oid) & "  AND  " _
                    & "  a.invcsd_en_id in ( " & _en_id_all & " ) "


                If SetString(be_loc.Tag) <> "" Then
                    ssql = ssql & " and a.invcsd_loc_id = " & SetInteger(be_loc.Tag.ToString)
                End If

                ssql = ssql & " union all " _
                   & "SELECT   " _
                    & "  a.invh_pt_id as invc_pt_id, " _
                    & "  a.invh_qty * -1 as inv_qty, " _
                    & "  b.pt_code, " _
                    & "  b.pt_desc1, " _
                    & "  b.pt_desc2, invh_loc_id as invc_loc_id, " _
                    & "  d.loc_desc, a.invh_desc as invh_group " _
                    & "FROM " _
                    & "  public.invh_mstr a " _
                    & "  INNER JOIN public.pt_mstr b ON (a.invh_pt_id = b.pt_id) " _
                    & "  INNER JOIN public.loc_mstr d ON (invh_loc_id = d.loc_id) " _
                    & "WHERE " _
                    & "  a.invh_en_id in ( " & _en_id_all & " ) "

                If SetString(be_loc.Tag) <> "" Then
                    ssql = ssql & " and invh_loc_id = " & SetInteger(be_loc.Tag.ToString)
                End If

                ssql = ssql & " and a.invh_date >= " & SetDateNTime00(de_from.DateTime) & " and a.invh_date <= " & SetDateNTime00(_date) & ""

            End If

            Dim dt_inv As New DataTable
            dt_inv = GetTableData(ssql)

            pgc_master.DataSource = dt_inv
            pgc_master.BestFit()

            'relation_detail()
            'bestfit_column()
            'ConditionsAdjustment()





        End If
            Cursor = Cursors.Arrow
    End Sub
    Public Overrides Sub format_grid()
        '' add_column_copy(gv_loc, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_loc, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_loc, "Deskripsi1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_loc, "Deskripsi2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        ''add_column_copy(gv_loc, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_loc, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        ''add_column_copy(gv_loc, "Lot Number", "invcsd_serial", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_loc, "Qty On Hand", "inv_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        ''add_column_copy(gv_loc, "Qty On Old", "inv_old", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        '' add_column_copy(gv_loc, "Unit Measure", "um_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Private Sub be_loc_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_loc.ButtonClick
        Dim frm As New FLocationSearch()
        frm.set_win(Me)
        frm._en_id = le_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub
    Public Overrides Function export_data() As Boolean
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls")
        If fileName <> "" Then
            pgc_master.ExportToXls(fileName)
            OpenFile(fileName)
        End If
    End Function
End Class
