Imports master_new.PGSqlConn
Imports master_new.ModFunction
Imports DevExpress.XtraEditors.Controls

Public Class FWORoutingSearch
    Public _en_id As Integer
    Dim ds_show As DataSet
    Dim _row_gv As Integer
    Public _si_id As Integer
    Public _wo_oid As String
    Public _obj As Object

    Private Sub FWOSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = Now
        pr_txttglakhir.DateTime = Now
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Work Order", "wo_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Partnumber", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Order Date", "wo_ord_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Sequence", "wodr_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Operation", "op_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Work Center", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "WC Group", "dpt_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  a.wodr_uid,wo_ord_date, " _
                & "  a.wodr_wo_oid, " _
                & "  b.wo_code,pt_desc1, " _
                & "  a.wodr_op, " _
                & "  c.op_name, " _
                & "  a.wodr_start_date, " _
                & "  a.wodr_end_date, " _
                & "  a.wodr_desc, " _
                & "  a.wodr_wc_id, " _
                & "  d.wc_desc, " _
                & "  a.wodr_yield_pct, " _
                & "  a.wodr_seq, " _
                & "  a.wodr_qty_in, " _
                & "  a.wodr_qty_complete,coalesce(wodr_qty_conversion,0) as wodr_qty_conversion, " _
                & "  a.wodr_qty_reject, " _
                & "  a.wodr_qty_out, " _
                & "  a.wodr_status,wc_dpt_id , dpt_desc " _
                & "FROM " _
                & "  public.wodr_routing a " _
                & "  INNER JOIN public.wo_mstr b ON (a.wodr_wo_oid = b.wo_oid) " _
                & "  left outer JOIN public.op_mstr c ON (a.wodr_op = c.op_code) " _
                & "  INNER JOIN public.wc_mstr d ON (a.wodr_wc_id = d.wc_id) " _
                & "  INNER JOIN public.pt_mstr e ON (b.wo_pt_id = e.pt_id) " _
                & "  left outer JOIN public.dpt_mstr f ON (d.wc_dpt_id = f.dpt_id) " _
                & "WHERE " _
                & "  b.wo_status = 'R' AND  " _
                & "  b.wo_ord_date BETWEEN " & SetDate(pr_txttglawal.DateTime.Date) _
                & " AND " & SetDate(pr_txttglakhir.DateTime.Date) & " "

        If SetString(_wo_oid) <> "" Then
            get_sequel = get_sequel & " and b.wo_oid=" & SetSetring(_wo_oid)
        End If
        get_sequel = get_sequel & " order by wo_code , wodr_seq"

        Return get_sequel
    End Function

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub

    Public Sub init_le_new(ByVal le_object As DevExpress.XtraEditors.LookUpEdit, ByVal tipe As String, ByVal par_en_id As String, ByVal par_wc_id As Integer)
        Dim sSQL As String
        Dim dt2 As New DataTable
        If tipe = "mch_mstr" Then
            'sSQL = "select mch_id, mch_code, mch_desc, mch_name from mch_mstr WHERE " _
            '             & " mch_dpt_id=" & SetInteger(par_wc_id) _
            '        & " union select null as mch_id, '-' as mch_code , '-' as mch_desc, '-' as mch_name " _
            '             & " order by mch_code desc, mch_name, mch_desc "

            sSQL = "select mch_id, mch_code, mch_desc, mch_name from mch_mstr WHERE " _
                       & " 1=1 " _
                  & " union select null as mch_id, '-' as mch_code , '-' as mch_desc, '-' as mch_name " _
                       & " order by mch_code desc, mch_name, mch_desc "

            dt2 = GetTableData(sSQL)

            With le_object
                If .Properties.Columns.VisibleCount = 0 Then
                    .Properties.Columns.Add(New LookUpColumnInfo("mch_id", "ID", 20))
                    .Properties.Columns.Add(New LookUpColumnInfo("mch_code", "Code", 20))
                    .Properties.Columns.Add(New LookUpColumnInfo("mch_name", "Description", 20))
                End If
                .Properties.DataSource = dt2
                .Properties.DisplayMember = dt2.Columns("mch_name").ToString
                .Properties.ValueMember = dt2.Columns("mch_id").ToString
                'If dt2.Rows.Count > 0 Then
                '    .EditValue = dt2.Rows(0).Item("mch_id")
                'End If
                '.ItemIndex = 0
                .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                .Properties.BestFit()
                .Properties.DropDownRows = 8
                .Properties.PopupWidth = 300

            End With
        End If
    End Sub

    Private Sub fill_data()

        _row_gv = BindingContext(ds.Tables(0)).Position
        If fobject.name = FWOLaborFeedback.Name Then
            fobject.lbrf_wodr_uid.text = ds.Tables(0).Rows(_row_gv).Item("wo_code") & " " & ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
            fobject.lbrf_wodr_uid.tag = ds.Tables(0).Rows(_row_gv).Item("wodr_uid").ToString
            fobject.wc_desc.editvalue = ds.Tables(0).Rows(_row_gv).Item("wc_desc")
            fobject.lbrf_qty_conversion.editvalue = 1.0 ' ds.Tables(0).Rows(_row_gv).Item("wodr_qty_conversion")

            init_le_new(fobject.lbrf_mch_id, "mch_mstr", 1, ds.Tables(0).Rows(_row_gv).Item("wc_dpt_id"))

        ElseIf fobject.name = FTransferRouting.Name Then
            If _obj.name = "trans_wodr_uid_from" Then
                fobject.trans_wodr_uid_from.text = ds.Tables(0).Rows(_row_gv).Item("wc_desc")
                fobject.trans_wodr_uid_from.tag = ds.Tables(0).Rows(_row_gv).Item("wodr_uid").ToString
                'fobject.wc_name_from.editvalue = ds.Tables(0).Rows(_row_gv).Item("wc_desc")
                fobject.trans_qty_routing_conversion_from.editvalue = SetNumber(ds.Tables(0).Rows(_row_gv).Item("wodr_qty_conversion"))
            Else
                fobject.trans_wodr_uid_to.text = ds.Tables(0).Rows(_row_gv).Item("wc_desc")
                fobject.trans_wodr_uid_to.tag = ds.Tables(0).Rows(_row_gv).Item("wodr_uid").ToString
                'fobject.wc_name_to.editvalue = ds.Tables(0).Rows(_row_gv).Item("wc_desc")
                ' fobject.trans_qty_routing_conversion_to.editvalue = ds.Tables(0).Rows(_row_gv).Item("wodr_qty_conversion")
            End If
        End If

    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            fill_data()
            Me.Close()
        End If
    End Sub
End Class
