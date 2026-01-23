Imports master_new.ModFunction

Public Class FInvestmentSearch
    Public _row, _en_id, _ptnr_id As Integer
    Dim sSQL As String
    Public _obj As Object
    Private Sub FWCSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 671
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        If fobject.name = FCashIn.Name Then

            add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Invest Number", "invest_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description", "investp_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Outstanding", "payment_in_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
            add_column(gv_master, "Date", "invest_tanggal", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")

        ElseIf fobject.name = FCashOut.Name Then

            add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Invest Number", "invest_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description", "investp_desc", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Outstanding", "payment_in_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
            'add_column(gv_master, "Date", "invest_tanggal", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
            add_column(gv_master, "Periode", "investd_periode", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Date Schedule", "investd_payment_date_schedule", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
            add_column(gv_master, "Outstanding Detail", "payment_in_outstanding_detail", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        End If
       
    End Sub

    Public Overrides Function get_sequel() As String


        If fobject.name = FCashIn.Name Then
            get_sequel = "SELECT  " _
                & "  b.en_desc, " _
                & "  c.invest_code, " _
                & "  a.investp_desc, invest_top_bulan, " _
                & "  c.invest_tanggal,invest_pokok - coalesce(invest_payment_in,0) as payment_in_outstanding " _
                & "FROM " _
                & "  public.investp_project a " _
                & "  INNER JOIN public.en_mstr b ON (a.investp_en_id = b.en_id) " _
                & "  INNER JOIN public.invest_mstr c ON (a.investp_code = c.invest_investp_code) " _
                & "WHERE invest_pokok - coalesce(invest_payment_in,0) >0 and invest_ptnr_id=" & SetInteger(_ptnr_id) & " and invest_code ~~* '%" + Trim(te_search.Text) + "%' " _
                & "ORDER BY " _
                & "  c.invest_code"
        ElseIf fobject.name = FCashOut.Name Then
            get_sequel = "SELECT  " _
                & " investd_oid, b.en_desc, " _
                & "  c.invest_code, " _
                & "  a.investp_desc, " _
                & "  c.invest_tanggal, " _
                & "  d.investd_periode,investd_payment_date_schedule, " _
                & "  d.investd_payment_total - coalesce(d.investd_paid,0) as payment_in_outstanding_detail " _
                & "FROM " _
                & "  public.investp_project a " _
                & "  INNER JOIN public.en_mstr b ON (a.investp_en_id = b.en_id) " _
                & "  INNER JOIN public.invest_mstr c ON (a.investp_code = c.invest_investp_code) " _
                & "  inner join investd_detail d on (c.invest_code=d.investd_invest_code) " _
                & "WHERE " _
                & "  coalesce(invest_payment_in,0) >0 and d.investd_payment_total - coalesce(d.investd_paid,0) > 0 and invest_ptnr_id=" & SetInteger(_ptnr_id) & " and invest_code ~~* '%" + Trim(te_search.Text) + "%' " _
                & " ORDER BY " _
                & "  c.invest_code,investd_periode"

        End If
        Return get_sequel
    End Function

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            fill_data()
            Me.Close()
        End If
    End Sub

    Private Sub fill_data()
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position

        If fobject.name = FCashIn.Name Then
            'fobject.gv_edit.SetRowCellValue(_row, "rod_wc_id", ds.Tables(0).Rows(_row_gv).Item("wc_id"))
            'fobject.gv_edit.SetRowCellValue(_row, "wc_desc", ds.Tables(0).Rows(_row_gv).Item("wc_desc"))
            'fobject.gv_edit.BestFitColumns()

            fobject.cashi_invest_code.text = SetString(ds.Tables(0).Rows(_row_gv).Item("invest_code")) & ", " & SetString(ds.Tables(0).Rows(_row_gv).Item("investp_desc"))
            fobject.cashi_invest_code.tag = ds.Tables(0).Rows(_row_gv).Item("invest_code")
            fobject._invest_top_bulan = SetNumber(ds.Tables(0).Rows(_row_gv).Item("invest_top_bulan"))

        ElseIf fobject.name = FCashOut.Name Then
            fobject.casho_investd_oid.text = SetString(ds.Tables(0).Rows(_row_gv).Item("invest_code")) & ", periode : " & SetString(ds.Tables(0).Rows(_row_gv).Item("investd_periode"))
            fobject.casho_investd_oid.tag = ds.Tables(0).Rows(_row_gv).Item("investd_oid").ToString
            fobject._casho_invest_code = SetString(ds.Tables(0).Rows(_row_gv).Item("invest_code"))
            fobject._casho_investd_periode = SetNumber(ds.Tables(0).Rows(_row_gv).Item("investd_periode"))
            fobject.casho_amount.editvalue = SetNumber(ds.Tables(0).Rows(_row_gv).Item("payment_in_outstanding_detail"))
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub
End Class
