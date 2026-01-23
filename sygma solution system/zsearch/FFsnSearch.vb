Imports master_new.ModFunction

Public Class FFsnSearch
    Public _row, _cu_id, _cc_id, _en_id As Integer

    Private Sub FFsnSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        format_grid()
        Me.Width = 800
        Me.Height = 360
        'help_load_data(True)
        'gv_master.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "FSN Code", "genfsn_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "FSN Name", "qrtr_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Cost Centre", "qrtr_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Cost Centre", "genfsn_year", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Code", "bdgt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Cost Centre", "cc_desc", DevExpress.Utils.HorzAlignment.Default)

        'add_column(gv_master, "Deskripsi", "ac_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "remarks", "cca_remarks", DevExpress.Utils.HorzAlignment.Default)
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
                    & "  b.qrtr_name, " _
                    & "  a.genfsn_year, " _
                    & "  a.genfsn_all_child, " _
                    & "  b.qrtr_code_id, " _
                    & "  a.genfsn_start_dt, " _
                    & "  a.genfsn_end_dt " _
                    & "FROM public.genfsn_mstr a " _
                    & "  INNER JOIN public.qrtr_mstr b ON (a.genfsn_qrtr_id = b.qrtr_id) " _
                    & "  INNER JOIN public.en_mstr c ON (a.genfsn_en_id = c.en_id) " _
                    & "   where genfsn_en_id = " + _en_id.ToString


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

        fobject.name = "FBonusInsentifGenerator"
        fobject._geninstf_fsn_ids = ds.Tables(0).Rows(_row_gv).Item("genfsn_oid").ToString
        fobject.geninstf_fsn_id.Text = ds.Tables(0).Rows(_row_gv).Item("genfsn_code")
        fobject.geninstf_qrtr_code_id.EditValue = ds.Tables(0).Rows(_row_gv).Item("genfsn_qrtr_code_id")
        fobject.geninstf_qrtr_code_id.EditValue = ds.Tables(0).Rows(_row_gv).Item("qrtr_name")
        'fobject.load_detail(ds.Tables(0).Rows(_row_gv).Item("bdgt_oid").ToString())

    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        'gv_master.Focus()
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub

End Class
