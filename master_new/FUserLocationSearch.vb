Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FUserLocationSearch
    Public _row, _en_id As Integer
    Public _obj As Object
    Public _type, _userid As String

    Private Sub FUserLocationSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        'Me.Width = 371
        'Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Warehouse", "wh_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "loc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Type", "loc_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Category", "loc_cat_name", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  loc_oid, " _
                    & "  loc_dom_id, " _
                    & "  loc_en_id, " _
                    & "  en_code, " _
                    & "  loc_id, " _
                    & "  loc_wh_id, " _
                    & "  wh_desc, " _
                    & "  loc_si_id, " _
                    & "  si_desc, " _
                    & "  en_desc, " _
                    & "  loc_code, " _
                    & "  loc_desc, " _
                    & "  loc_type, " _
                    & "  loc_type_mstr.code_name as loc_type_name, " _
                    & "  loc_cat, " _
                    & "  loc_cat_mstr.code_name as loc_cat_name, " _
                    & "  loc_is_id, " _
                    & "  is_desc, " _
                    & "  loc_active, " _
                    & "  loc_dt " _
                    & "FROM  " _
                    & "  public.loc_mstr" _
                    & " inner join en_mstr on en_id = loc_en_id " _
                    & " inner join wh_mstr on wh_id = loc_wh_id " _
                    & " inner join si_mstr on si_id = loc_si_id " _
                    & " inner join is_mstr on is_id = loc_is_id " _
                    & " inner join code_mstr as loc_type_mstr on loc_type_mstr.code_id = loc_type" _
                    & " inner join code_mstr as loc_cat_mstr on loc_cat_mstr.code_id = loc_cat" _
                    & " where (loc_code ~~* '%" + Trim(te_search.Text) + "%' or loc_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and loc_active ~~* 'Y'" _
                    & " and loc_desc <> '-'"
        Return get_sequel
    End Function

    'Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
    '    fill_data()
    '    Me.Close()
    'End Sub

    'Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
    '    If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
    '        fill_data()
    '        Me.Close()
    '    End If
    'End Sub
    'Public Overrides Sub format_grid()

    '    add_column(gv_master, "Menu ID", "menuid", DevExpress.Utils.HorzAlignment.Default)
    '    add_column(gv_master, "Menu Name", "menudesc", DevExpress.Utils.HorzAlignment.Default)
    '    add_column(gv_master, "Menu ID Parent", "menuid_parent", DevExpress.Utils.HorzAlignment.Default)
    '    add_column(gv_master, "Menu Parent", "menudesc_parent", DevExpress.Utils.HorzAlignment.Default)

    'End Sub

    'Public Overrides Function get_sequel() As String
    '    get_sequel = "SELECT a.menuid,a.menuname,a.menuid_parent,a.menudesc,b.menudesc as menudesc_parent " _
    '            & " from tconfmenucollection  a left outer join tconfmenucollection b on a.menuid_parent=b.menuid " _
    '            & " where a.menuname ~~* '%" & te_search.Text & "%' or a.menudesc ~~* '%" & te_search.Text & "%'  order by menudesc"

    '    Return get_sequel
    'End Function

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
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

    Public Overrides Sub fill_data()

        Try

            Dim _row_gv As Integer
            _row_gv = BindingContext(ds.Tables(0)).Position


            If fobject.name = "FUserGroup" Then
                _obj.editvalue = ds.Tables(0).Rows(_row_gv).Item("loc_desc")
                fobject.__locationid = ds.Tables(0).Rows(_row_gv).Item("loc_id")


            End If


        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub


End Class
