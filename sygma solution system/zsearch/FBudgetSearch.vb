Imports master_new.ModFunction

Public Class FBudgetSearch
    Public _row, _cu_id, _cc_id, _en_id As Integer

    Private Sub FBudgetSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        format_grid()
        Me.Width = 800
        Me.Height = 360
        'help_load_data(True)
        'gv_master.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Code", "bdgt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Cost Centre", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Code", "bdgt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Cost Centre", "cc_desc", DevExpress.Utils.HorzAlignment.Default)

        'add_column(gv_master, "Deskripsi", "ac_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "remarks", "cca_remarks", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  bdgt_oid, " _
                    & "  bdgt_dom_id, " _
                    & "  bdgt_en_id, " _
                    & "  bdgt_add_by, " _
                    & "  bdgt_add_date, " _
                    & "  bdgt_upd_by, " _
                    & "  bdgt_upd_date, " _
                    & "  bdgt_date, " _
                    & "  bdgt_year, " _
                    & "  bdgt_remarks, " _
                    & "  bdgt_trans_id, " _
                    & "  bdgt_dt, " _
                    & "  bdgt_tran_id, " _
                    & "  bdgt_cc_id,cc_desc, " _
                    & "  bdgt_code, " _
                    & "  bdgt_rev, " _
                    & "  bdgt_active, bdgt_year_periode " _
                    & "FROM  " _
                    & "  public.bdgt_mstr " _
                    & "  inner join cc_mstr on cc_id = bdgt_cc_id "
        If fobject.name = "FCrossBudget" Then
            get_sequel = get_sequel + " " _
                        & " where bdgt_en_id = " + SetInteger(_en_id) _
                        & "order by bdgt_cc_id asc "
        Else
            get_sequel = get_sequel + "" _
                        & " where bdgt_code ~~* '%" + Trim(te_search.Text) + "%'" _
                        & "order by bdgt_cc_id asc "
            '& " where bdgt_year = " + SetInteger(_yearh) _
            '& " and bdgt_code ~~* '%" + Trim(te_search.Text) + "%'" _
            '& " and bdgt_trans_id = 'D' and bdgt_active = 'Y' " _
            '& "order by bdgt_cc_id asc "
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

        If fobject.name = "FBudget" Then
            fobject.gv_edit.SetRowCellValue(_row, "bdgtd_ac_id", ds.Tables(0).Rows(_row_gv).Item("cca_ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject._en_id = ds.Tables(0).Rows(_row_gv).Item("bdgt_en_id")
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FBudgetDetail" Then
            fobject.pr_budget_code.Text = ds.Tables(0).Rows(_row_gv).Item("bdgt_code")
            fobject._cc_id = ds.Tables(0).Rows(_row_gv).Item("bdgt_cc_id")
            fobject._bdgt_oid = ds.Tables(0).Rows(_row_gv).Item("bdgt_oid").ToString
            fobject._en_id = ds.Tables(0).Rows(_row_gv).Item("bdgt_en_id")
            fobject._year = ds.Tables(0).Rows(_row_gv).Item("bdgt_year_periode")
            fobject._trans_id = ds.Tables(0).Rows(_row_gv).Item("bdgt_trans_id")
            fobject._year = ds.Tables(0).Rows(_row_gv).Item("bdgt_year_periode")
            fobject._active = ds.Tables(0).Rows(_row_gv).Item("bdgt_active")
            fobject.load_data_many(True)
            fobject.load_lookup()
        ElseIf fobject.name = "FCrossBudget" Then
            fobject._bdgt_oid = ds.Tables(0).Rows(_row_gv).Item("bdgt_oid").ToString
            fobject.cbdgt_bdgt_oid.Text = ds.Tables(0).Rows(_row_gv).Item("bdgt_code")
            fobject.cbdgt_cc_from_id.EditValue = ds.Tables(0).Rows(_row_gv).Item("bdgt_cc_id")
            fobject.cbdgt_cc_to_id.EditValue = ds.Tables(0).Rows(_row_gv).Item("bdgt_cc_id")
            fobject.load_detail(ds.Tables(0).Rows(_row_gv).Item("bdgt_oid").ToString())
        End If
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
