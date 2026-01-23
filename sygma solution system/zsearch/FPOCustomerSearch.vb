Imports master_new.ModFunction

Public Class FPOCustomerSearch
    Public _row As Integer
    Public _en_id, _ptnr_id, _cu_id As Integer
    Public func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime
    Public _obj As Object

    Private Sub FPOCustomerSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "PO Number", "pocust_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "PO Date", "pocust_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Start Date", "pocust_star_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_master, "End Date", "pocust_end_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_master, "Remarks", "pocust_remarks", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        get_sequel = "SELECT  " _
                    & "  pocust_oid, " _
                    & "  pocust_dom_id, " _
                    & "  pocust_en_id, " _
                    & "  pocust_add_by, " _
                    & "  pocust_add_date, " _
                    & "  pocust_upd_by, " _
                    & "  pocust_upd_date, " _
                    & "  pocust_dt, " _
                    & "  pocust_code, " _
                    & "  pocust_date, " _
                    & "  pocust_ptnr_id, " _
                    & "  pocust_star_date, " _
                    & "  pocust_end_date, " _
                    & "  pocust_remarks, " _
                    & "  pocust_cu_id, " _
                    & "  pocust_amount, " _
                    & "  en_desc, " _
                    & "  ptnr_name, " _
                    & "  cu_name " _
                    & "FROM  " _
                    & "  public.pocust_mstr " _
                    & "  inner join en_mstr on en_id = pocust_en_id " _
                    & "  inner join ptnr_mstr on ptnr_id = pocust_ptnr_id " _
                    & "  inner join cu_mstr on cu_id = pocust_cu_id" _
                    & " where pocust_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and pocust_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and pocust_en_id = " + _en_id.ToString _
                    & " and pocust_ptnr_id = " + _ptnr_id.ToString

        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

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

        Dim ds_bantu As New DataSet
        If fobject.name = "FSoPj" Then
            fobject.gv_edit.SetRowCellValue(_row, "sopjd_pocust_oid", ds.Tables(0).Rows(_row_gv).Item("pocust_oid").ToString)
            fobject.gv_edit.SetRowCellValue(_row, "pocust_code", ds.Tables(0).Rows(_row_gv).Item("pocust_code"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FProjectMaintenance" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("pocust_code")
            fobject._pocust_oid = ds.Tables(0).Rows(_row_gv).Item("pocust_oid").ToString
        End If
    End Sub
End Class
