Imports master_new.ModFunction

Public Class FCategorySearch
    Public _row, _en_id As Integer
    'Public _code_field As String
    Public _caption As String
    'Public _filter As String
    Public _obj As Object
    Public _group_code As Integer

    Private Sub FCategorySearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = _caption
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Category Id", "ptcat_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Category Code", "ptcat_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Category Name", "ptcat_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Description", "code_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    'Public Overrides Function get_sequel() As String
    '    get_sequel = "select ptcat_id, ptcat_desc, ptcat_active, " + _
    '                 " code_upd_by, code_upd_date, code_id, code_seq, code_field, " + _
    '                 " code_code, code_name, code_desc, code_default, " + _
    '                 " code_parent, code_usr_1, code_usr_2, code_usr_3, " + _
    '                 " code_usr_4, code_usr_5, code_active, code_dt from code_mstr " + _
    '                 " inner join en_mstr on code_en_id = en_id " + _
    '                 " where (code_code ~~* '%" + Trim(te_search.Text) + "%' or code_name ~~* '%" + Trim(te_search.Text) + "%' or code_desc ~~* '%" + Trim(te_search.Text) + "%')" + _
    '                 " and code_active ~~* 'Y'" + _
    '                 " and code_en_id in (0," + _en_id.ToString + ")" + _
    '                 " and code_field ~~* " & SetSetring(_code_field)
    '    Return get_sequel
    'End Function

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  public.ptcat_mstr.ptcat_id, " _
                    & "  public.ptcat_mstr.ptcat_code, " _
                    & "  public.ptcat_mstr.ptcat_desc, " _
                    & "  public.ptcat_mstr.ptcat_active " _
                    & "FROM " _
                    & "  public.ptcat_mstr " _
                    & "  where " _
                    & "  public.ptcat_mstr.ptcat_active ~~* 'Y'"
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

    Public Overrides Sub fill_data()
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position

        '    _obj.tag = dt.Rows(BindingContext(dt).Position).Item("ptcat_id")
        '    _obj.editvalue = dt.Rows(BindingContext(dt).Position).Item("ptcat_desc")

        If fobject.name = FPartNumber.Name Then
            'If _filter = "reject" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptcat_desc")
            _obj.tag = ds.Tables(0).Rows(_row_gv).Item("ptcat_id")
            'ElseIf fobject.name = "FItemSiteCost" Then
            '_obj.text = ds.Tables(0).Rows(_row_gv).Item("ptcat_desc")
            'fobject._pt_id = ds.Tables(0).Rows(_row_gv).Item("ptcat_id")
            'Else

            'If fobject.name = FPartNumber.Name Then
            '    fobject._so_ptnr_id_sold_mstr = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
            '    fobject.pt_cat_id.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            '    fobject.so_ar_ac_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnr_ac_ar_id")
            '    fobject.so_bantu_address.text = Trim(ds.Tables(0).Rows(_row_gv).Item("ptnra_line_1") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_2") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_3"))

            '    fobject.gv_rework.SetRowCellValue(_row, "wolbrw_rea_id", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            '    fobject.gv_rework.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
            '    fobject.gv_rework.BestFitColumns()
        End If

        'End If
    End Sub


    '       fobject.so_ar_ac_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnr_ac_ar_id")
    '       fobject.so_bantu_address.text = Trim(ds.Tables(0).Rows(_row_gv).Item("ptnra_line_1") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_2") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_3"))


    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        'gv_master.Focus()
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub
End Class
