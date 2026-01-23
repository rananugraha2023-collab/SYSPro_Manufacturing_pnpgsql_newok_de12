Imports master_new.ModFunction

Public Class FPartNumberCustomerSearch
    Public _row, _en_id, _si_id As Integer
    Public _obj As Object
    Public _so_type As String
    Public _tran_oid As String = ""
    Public _ptnr_id As Integer
    Public _prj_oid As String
    Dim func_data As New function_data

    Private Sub FPartNumberCustomerSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        If fobject.name = "FProjectGroup" Then
            add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Code", "cp_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description1", "prjc_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description2", "prjc_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        Else
            add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Code", "cp_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description1", "cp_desc1", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description2", "cp_desc2", DevExpress.Utils.HorzAlignment.Default)
        End If
        
    End Sub

    Public Overrides Function get_sequel() As String
        If fobject.name = "FProjectGroup" Then
            get_sequel = "SELECT  " _
                        & "  prjc_oid, " _
                        & "  prjc_en_id,en_desc, " _
                        & "  prjc_prj_oid, " _
                        & "  prjc_seq, " _
                        & "  prjc_si_id,si_desc, " _
                        & "  prjc_cp_id,cp_code, " _
                        & "  prjc_pt_desc1, " _
                        & "  prjc_pt_desc2, " _
                        & "  prjc_loc_id,loc_desc, " _
                        & "  prjc_qty, " _
                        & "  prjc_qty_full, " _
                        & "  prjc_um, " _
                        & "  prjc_cost, " _
                        & "  prjc_price, " _
                        & "  prjc_disc, " _
                        & "  prjc_um_conv, " _
                        & "  prjc_qty_real, " _
                        & "  prjc_trans_id, " _
                        & "  prjc_qty_inv " _
                        & "FROM  " _
                        & "  public.prjc_cust " _
                        & " inner join en_mstr on en_id = prjc_en_id " _
                        & "  inner join cp_mstr on cp_id = prjc_cp_id " _
                        & "  inner join si_mstr on si_id = prjc_si_id " _
                        & "  inner join loc_mstr on loc_id = prjc_loc_id " _
                        & " where (cp_code ~~* '%" + Trim(te_search.Text) + "%' or prjc_pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or prjc_pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                        & " and prjc_en_id in (0," + _en_id.ToString + ")" _
                        & " and prjc_prj_oid = " + SetSetring(_prj_oid.ToString()) _
                        & " order by cp_code asc "
                       
        Else
            get_sequel = "SELECT  " _
                    & "  cp_en_id, " _
                    & "  en_desc, " _
                    & "  cp_id, " _
                    & "  cp_code, " _
                    & "  cp_desc1, " _
                    & "  cp_desc2, " _
                    & "  cp_um, " _
                    & "  cp_ptnr_id, " _
                    & "  um_mstr.code_name as um_name " _
                    & "FROM  " _
                    & "  public.cp_mstr" _
                    & " inner join en_mstr on en_id = cp_en_id " _
                    & " inner join code_mstr um_mstr on cp_um = um_mstr.code_id " _
                    & " where (cp_code ~~* '%" + Trim(te_search.Text) + "%' or cp_desc1 ~~* '%" + Trim(te_search.Text) + "%' or cp_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and cp_en_id in (0," + _en_id.ToString + ")" _
                    & " and cp_ptnr_id = " & SetInteger(_ptnr_id) _
                    & " order by cp_code asc "

        End If
        
        Return get_sequel
    End Function

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub

    Private Sub fill_data()
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position
        Dim dt_bantu As New DataTable()
        Dim func_coll As New function_collection

        If fobject.name = "FProjectMaintenance" Then
            fobject.gv_edit_cust.SetRowCellValue(_row, "prjc_cp_id", ds.Tables(0).Rows(_row_gv).Item("cp_id"))
            fobject.gv_edit_cust.SetRowCellValue(_row, "cp_code", ds.Tables(0).Rows(_row_gv).Item("cp_code"))
            fobject.gv_edit_cust.SetRowCellValue(_row, "prjc_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("cp_desc1"))
            fobject.gv_edit_cust.SetRowCellValue(_row, "prjc_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("cp_desc2"))
            fobject.gv_edit_cust.SetRowCellValue(_row, "prjc_um", ds.Tables(0).Rows(_row_gv).Item("cp_um"))
            fobject.gv_edit_cust.SetRowCellValue(_row, "unit_measure", ds.Tables(0).Rows(_row_gv).Item("um_name"))
        ElseIf fobject.name = "FProjectGroup" Then
            fobject.gv_edit.SetRowCellValue(_row, "prjgd_prjc_oid", ds.Tables(0).Rows(_row_gv).Item("prjc_oid").ToString)
            fobject.gv_edit.SetRowCellValue(_row, "cp_code", ds.Tables(0).Rows(_row_gv).Item("cp_code"))
            fobject.gv_edit.SetRowCellValue(_row, "prjc_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("prjc_pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "prjc_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("prjc_pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
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
