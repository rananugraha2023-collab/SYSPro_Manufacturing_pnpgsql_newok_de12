Public Class FFSNItemSearch
    Public _row As Integer
    Public grid_call As String = ""
    Public _obj As Object
    Public _en_id, _code As Integer

    Private Sub FFSNItemSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Code", "genfsn_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "qrtr_code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Desc", "qrtr_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Start Date", "qrtr_start_date", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "End Date", "qrtr_end_date", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.genfsn_oid, " _
                & "  a.genfsn_en_id, " _
                & "  c.en_desc, " _
                & "  a.genfsn_qrtr_id, " _
                & "  a.genfsn_code, " _
                & "  b.qrtr_code, " _
                & "  b.qrtr_code_name, " _
                & "  a.genfsn_qrtr_code_id, " _
                & "  b.qrtr_id, " _
                & "  b.qrtr_name, " _
                & "  a.genfsn_year, " _
                & "  a.genfsn_all_child, " _
                & "  b.qrtr_code_id, " _
                & "  a.genfsn_start_dt, " _
                & "  a.genfsn_end_dt " _
                & "FROM public.genfsn_mstr a " _
                & " INNER JOIN public.qrtr_mstr b ON (a.genfsn_qrtr_id = b.qrtr_id) " _
                & " INNER JOIN public.en_mstr c ON (a.genfsn_en_id = c.en_id) " _
                & " where genfsn_en_id = " + _en_id.ToString _
                & " and genfsn_ce_final = 'Y' "

            Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        'gv_master.Focus()
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
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

            '_obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("qrtr_name")
            '_obj.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("qrtr_id")
        'fobject.genfsn_start_dt.editvalue = ds.Tables(0).Rows(_row_gv).Item("qrtr_start_date")
        'fobject.genfsn_end_dt.editvalue = ds.Tables(0).Rows(_row_gv).Item("qrtr_end_date")
        'fobject._geninstf_fsn_codes = ds.Tables(0).Rows(_row_gv).Item("genfsn_oid")
        'fobject.geninstf_fsn_code.text = ds.Tables(0).Rows(_row_gv).Item("genfsn_code")

        fobject.geninstf_fsn_oid.tag = ds.Tables(0).Rows(_row_gv).Item("genfsn_oid").ToString
        fobject.geninstf_fsn_oid.text = ds.Tables(0).Rows(_row_gv).Item("genfsn_code").ToString
        fobject.geninstf_qrtr_code.tag = ds.Tables(0).Rows(_row_gv).Item("qrtr_code_id")
        fobject.geninstf_qrtr_code.text = ds.Tables(0).Rows(_row_gv).Item("qrtr_code_name").ToString
        fobject.geninstf_qrtr_periode.tag = ds.Tables(0).Rows(_row_gv).Item("qrtr_id")
        fobject.geninstf_qrtr_periode.text = ds.Tables(0).Rows(_row_gv).Item("qrtr_name").ToString

    End Sub
End Class
