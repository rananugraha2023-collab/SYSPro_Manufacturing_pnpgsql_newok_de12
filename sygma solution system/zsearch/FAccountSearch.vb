Imports master_new.ModFunction

Public Class FAccountSearch
    Public _row, _cu_id As Integer
    Public _obj As Object = ""
    Public _en_id As Integer
    Public _col_name As String
    Public _obj2 As Object
    Private Sub FAccountSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 800
        Me.Height = 500
        'te_search.Focus()
        'gv_master.Focus()
    End Sub

    Public Overrides Sub format_grid()
        BtRetrive.Visible = False
        If fobject.name = "FPLSetting" Or fobject.name = "FCashFlowSetting" _
        Or fobject.name = FBSSetting.Name Or fobject.name = FRosettaRuleNew.Name _
        Or fobject.name = FRosettaGroupRule.Name Or fobject.name = FDashboardSetting.Name Then
            add_column(gv_master, "ID", "ac_id", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Code Hirarki", "ac_code_hirarki", DevExpress.Utils.HorzAlignment.Default)
        ElseIf fobject.name = FFRAccountGroupSetting.Name Or fobject.name = FEntity.Name Then
            add_column_edit(gv_master, "Check", "pilih", DevExpress.Utils.HorzAlignment.Default)
            BtRetrive.Visible = True
        End If
        add_column(gv_master, "Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Deskripsi", "ac_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Subclass 1", "subclass_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Subclass 2", "subclass_name_2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Subclass 3", "subclass_name_3", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FVoucher" Then
            get_sequel = "SELECT  " _
                    & "  b.ac_id, " _
                    & "  b.ac_code, " _
                    & "  b.ac_name, " _
                    & "  b.ac_desc, " _
                    & "  ac_subclass, ac_subclass_2, ac_subclass_3, " _
                    & "  code_mstr_subclass.code_name as subclass_name, " _
                    & "  code_mstr_subclass_2.code_name as subclass_name_2, " _
                    & "  code_mstr_subclass_3.code_name as subclass_name_3 " _
                    & "FROM " _
                    & "  public.ac_mstr b " _
                    & "  left outer join code_mstr code_mstr_subclass on code_mstr_subclass.code_id = ac_subclass " _
                    & "  left outer join code_mstr code_mstr_subclass_2 on code_mstr_subclass_2.code_id = ac_subclass_2 " _
                    & "  left outer join code_mstr code_mstr_subclass_3 on code_mstr_subclass_3.code_id = ac_subclass_3 " _
                    & " where (ac_code ~~* '%" + Trim(te_search.Text) + "%' or ac_name ~~* '%" + Trim(te_search.Text) + "%' or ac_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and ac_active ~~* 'Y'" _
                    & " and ac_is_sumlevel ~~* 'N'"
            get_sequel = get_sequel & " " & SetString(_obj) _
                   & " order by ac_code"
        ElseIf fobject.name = "FPLSetting" Or fobject.name = "FCashFlowSetting" Or fobject.name = FBSSetting.Name _
        Or fobject.name = FDashboardSetting.Name Then
            get_sequel = "SELECT  " _
                   & "  b.ac_id, " _
                   & "  b.ac_code,b.ac_code_hirarki,ac_sign, " _
                   & "  b.ac_name, " _
                   & "  b.ac_desc, " _
                   & "  ac_subclass, ac_subclass_2, ac_subclass_3, " _
                   & "  code_mstr_subclass.code_name as subclass_name, " _
                   & "  code_mstr_subclass_2.code_name as subclass_name_2, " _
                   & "  code_mstr_subclass_3.code_name as subclass_name_3 " _
                   & "FROM " _
                   & "  public.ac_mstr b " _
                   & "  left outer join code_mstr code_mstr_subclass on code_mstr_subclass.code_id = ac_subclass " _
                   & "  left outer join code_mstr code_mstr_subclass_2 on code_mstr_subclass_2.code_id = ac_subclass_2 " _
                   & "  left outer join code_mstr code_mstr_subclass_3 on code_mstr_subclass_3.code_id = ac_subclass_3 " _
                   & " where (ac_code ~~* '%" + Trim(te_search.Text) + "%' or ac_name ~~* '%" + Trim(te_search.Text) + "%' or ac_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                   & " and ac_active ~~* 'Y' " _
                   & " "

            get_sequel = get_sequel & " " & SetString(_obj) _
                   & " order by ac_code_hirarki"
        ElseIf fobject.name = FRosettaRuleNew.Name Or fobject.name = FRosettaGroupRule.Name Then
            get_sequel = "SELECT  " _
                  & "  b.ac_id, " _
                  & "  b.ac_code,b.ac_code_hirarki,ac_sign, " _
                  & "  b.ac_name, " _
                  & "  b.ac_desc " _
                  & "FROM " _
                  & "  public.ac_mstr b " _
                  & " where (ac_code ~~* '%" + Trim(te_search.Text) + "%' or ac_name ~~* '%" + Trim(te_search.Text) + "%' or ac_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                  & " and ac_active ~~* 'Y' "
            get_sequel = get_sequel & " " & SetString(_obj) _
                   & " order by ac_code_hirarki"
        ElseIf fobject.name = FFRAccountGroupSetting.Name Or fobject.name = FEntity.Name Then
            get_sequel = "SELECT  " _
                   & "  false as pilih,b.ac_id, " _
                   & "  b.ac_code, " _
                   & "  b.ac_name, " _
                   & "  b.ac_desc, " _
                   & "  ac_subclass, ac_subclass_2, ac_subclass_3, " _
                   & "  code_mstr_subclass.code_name as subclass_name, " _
                   & "  code_mstr_subclass_2.code_name as subclass_name_2, " _
                   & "  code_mstr_subclass_3.code_name as subclass_name_3 " _
                   & "FROM " _
                   & "  public.ac_mstr b " _
                   & "  left outer join code_mstr code_mstr_subclass on code_mstr_subclass.code_id = ac_subclass " _
                   & "  left outer join code_mstr code_mstr_subclass_2 on code_mstr_subclass_2.code_id = ac_subclass_2 " _
                   & "  left outer join code_mstr code_mstr_subclass_3 on code_mstr_subclass_3.code_id = ac_subclass_3 " _
                   & " where (ac_code ~~* '%" + Trim(te_search.Text) + "%' or ac_name ~~* '%" + Trim(te_search.Text) + "%' or ac_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                   & " and ac_active ~~* 'Y'" _
                   & " and ac_is_sumlevel ~~* 'N'"
            get_sequel = get_sequel & " " & SetString(_obj) _
                   & " order by ac_code"
        ElseIf fobject.name = FCashOut.Name Or fobject.name = FCashIn.Name Then
            get_sequel = "SELECT  " _
                    & "  b.ac_id, " _
                    & "  b.ac_code, " _
                    & "  b.ac_name, " _
                    & "  b.ac_desc,b.ac_sign, " _
                    & "  ac_subclass, ac_subclass_2, ac_subclass_3, " _
                    & "  code_mstr_subclass.code_name as subclass_name, " _
                    & "  code_mstr_subclass_2.code_name as subclass_name_2, " _
                    & "  code_mstr_subclass_3.code_name as subclass_name_3 " _
                    & "FROM " _
                    & "  public.ac_mstr b " _
                    & "  left outer join code_mstr code_mstr_subclass on code_mstr_subclass.code_id = ac_subclass " _
                    & "  left outer join code_mstr code_mstr_subclass_2 on code_mstr_subclass_2.code_id = ac_subclass_2 " _
                    & "  left outer join code_mstr code_mstr_subclass_3 on code_mstr_subclass_3.code_id = ac_subclass_3 " _
                    & " where (ac_code ~~* '%" + Trim(te_search.Text) + "%' or ac_name ~~* '%" + Trim(te_search.Text) + "%' or ac_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and ac_active ~~* 'Y'" _
                    & " and ac_is_sumlevel ~~* 'N' "
            get_sequel = get_sequel & " " & SetString(_obj) _
                   & " order by ac_code"

        Else
            get_sequel = "SELECT  " _
                    & "  b.ac_id, " _
                    & "  b.ac_code, " _
                    & "  b.ac_name, " _
                    & "  b.ac_desc,b.ac_sign, " _
                    & "  ac_subclass, ac_subclass_2, ac_subclass_3, " _
                    & "  code_mstr_subclass.code_name as subclass_name, " _
                    & "  code_mstr_subclass_2.code_name as subclass_name_2, " _
                    & "  code_mstr_subclass_3.code_name as subclass_name_3 " _
                    & "FROM " _
                    & "  public.ac_mstr b " _
                    & "  left outer join code_mstr code_mstr_subclass on code_mstr_subclass.code_id = ac_subclass " _
                    & "  left outer join code_mstr code_mstr_subclass_2 on code_mstr_subclass_2.code_id = ac_subclass_2 " _
                    & "  left outer join code_mstr code_mstr_subclass_3 on code_mstr_subclass_3.code_id = ac_subclass_3 " _
                    & " where (ac_code ~~* '%" + Trim(te_search.Text) + "%' or ac_name ~~* '%" + Trim(te_search.Text) + "%' or ac_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and ac_active ~~* 'Y'" _
                    & " and ac_is_sumlevel ~~* 'N'"

            get_sequel = get_sequel & " " & SetString(_obj) _
                   & " order by ac_code"
        End If

       

        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        'gv_master.FocusedRowHandle = gv_master.GetVisibleRowHandle(0)

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

        If fobject.name = "FStandardTransaction" Then
            fobject.gv_edit.SetRowCellValue(_row, "glt_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FVoucher" Then
            fobject.gv_edit_dist.SetRowCellValue(_row, "apd_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit_dist.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit_dist.SetRowCellValue(_row, "apd_remarks", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit_dist.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit_dist.BestFitColumns()
        ElseIf fobject.name = "FInventoryReceipts" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryIssues" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryIssuesHadiah" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryAdjustmentPlus" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryAdjustmentMinus" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryBeginingBalance" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FSalesOrder" Then
            fobject.gv_edit.SetRowCellValue(_row, "sod_sales_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code_sales", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name_sales", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FDRCRMemo" Then
            fobject.gv_edit_dist.SetRowCellValue(_row, "ard_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit_dist.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit_dist.SetRowCellValue(_row, "ard_remarks", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit_dist.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit_dist.BestFitColumns()
        ElseIf fobject.name = "FPLSetting" Then
            fobject.gv_edit.SetRowCellValue(_row, "pla_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pla_ac_hirarki", ds.Tables(0).Rows(_row_gv).Item("ac_code_hirarki"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = FCashFlowSetting.Name Then
            fobject.gv_edit.SetRowCellValue(_row, "ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code_hirarki", ds.Tables(0).Rows(_row_gv).Item("ac_code_hirarki"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = FCashOut.Name Then
            fobject.gv_edit.SetRowCellValue(_row, "cashod_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = FCashIn.Name Then
            fobject.gv_edit.SetRowCellValue(_row, "cashid_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = FBSSetting.Name Then
            fobject.gv_edit.SetRowCellValue(_row, "bsda_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "bsda_ac_hirarki", ds.Tables(0).Rows(_row_gv).Item("ac_code_hirarki"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()


        ElseIf fobject.name = FDashboardSetting.Name Then
            fobject.gv_edit.SetRowCellValue(_row, "dashd_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code_hirarki", ds.Tables(0).Rows(_row_gv).Item("ac_code_hirarki"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FRosettaRuleNew" Then
            If _col_name.Contains("debit") = True Then
                fobject.gv_edit.SetRowCellValue(_row, "rosr_ac_hirarki_debit", ds.Tables(0).Rows(_row_gv).Item("ac_code_hirarki"))
                fobject.gv_edit.SetRowCellValue(_row, "ac_code_hirarki_debit", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
                fobject.gv_edit.SetRowCellValue(_row, "ac_name_hirarki_debit", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
                fobject.gv_edit.SetRowCellValue(_row, "ac_sign_hirarki_debit", ds.Tables(0).Rows(_row_gv).Item("ac_sign"))
                fobject.gv_edit.BestFitColumns()
            ElseIf _col_name.Contains("credit") = True Then
                fobject.gv_edit.SetRowCellValue(_row, "rosr_ac_hirarki_credit", ds.Tables(0).Rows(_row_gv).Item("ac_code_hirarki"))
                fobject.gv_edit.SetRowCellValue(_row, "ac_code_hirarki_credit", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
                fobject.gv_edit.SetRowCellValue(_row, "ac_name_hirarki_credit", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
                fobject.gv_edit.SetRowCellValue(_row, "ac_sign_hirarki_credit", ds.Tables(0).Rows(_row_gv).Item("ac_sign"))
                fobject.gv_edit.BestFitColumns()
            End If
        ElseIf fobject.name = FRosettaGroupRule.Name Then
            fobject.gv_edit.SetRowCellValue(_row, "rosgr_ac_hirarki", ds.Tables(0).Rows(_row_gv).Item("ac_code_hirarki"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FFRAccountGroupSetting" Then
            fobject.gv_edit.SetRowCellValue(_row, "xfs1d_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()

        ElseIf fobject.name = "FCFDirectRule" Then
            fobject.gv_edit.SetRowCellValue(_row, "cfdruled_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.SetRowCellValue(_row, "cfdruled_sign", ds.Tables(0).Rows(_row_gv).Item("ac_sign"))
            fobject.gv_edit.BestFitColumns()
       
        ElseIf fobject.name = "FInventoryAssembly" Or fobject.name = "FInventoryDisAssembly" Then
            fobject.gv_edit.SetRowCellValue(_row, "pla_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()

        ElseIf fobject.name = "FEntity" Then
            fobject.gv_edit.SetRowCellValue(_row, "enacc_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()
        End If
    End Sub

    Private Sub BtRetrive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRetrive.Click
        Try
            If fobject.name = FEntity.Name Then
                Dim i As Integer = 0
                For Each dr As DataRow In ds.Tables(0).Rows
                    If dr("pilih") = True Then
                        If i = 0 Then
                            fobject.dt_edit.Rows(_row).item("enacc_ac_id") = dr("ac_id")
                            fobject.dt_edit.Rows(_row).item("ac_code") = dr("ac_code")
                            fobject.dt_edit.Rows(_row).item("ac_name") = dr("ac_name")

                        Else
                            Dim _dtrow As DataRow
                            _dtrow = fobject.dt_edit.NewRow
                            _dtrow("enacc_code") = fobject.dt_edit.Rows(_row).item("enacc_code")
                            _dtrow("enacc_ac_id") = dr("ac_id")
                            _dtrow("ac_code") = dr("ac_code")
                            _dtrow("ac_name") = dr("ac_name")

                            fobject.dt_edit.Rows.Add(_dtrow)
                        End If

                        i += 1
                    End If

                Next
                fobject.dt_edit.AcceptChanges()

                fobject.gv_edit.BestFitColumns()
            ElseIf fobject.name = FFRAccountGroupSetting.Name Then
                Dim i As Integer = 0
                For Each dr As DataRow In ds.Tables(0).Rows
                    If dr("pilih") = True Then
                        If i = 0 Then
                            fobject.dt_edit.Rows(_row).item("xfs1d_ac_id") = dr("ac_id")
                            fobject.dt_edit.Rows(_row).item("ac_code") = dr("ac_code")
                            fobject.dt_edit.Rows(_row).item("ac_name") = dr("ac_name")

                        Else
                            Dim _dtrow As DataRow
                            _dtrow = fobject.dt_edit.NewRow

                            _dtrow("xfs1d_ac_id") = dr("ac_id")
                            _dtrow("ac_code") = dr("ac_code")
                            _dtrow("ac_name") = dr("ac_name")

                            fobject.dt_edit.Rows.Add(_dtrow)
                        End If

                        i += 1
                    End If

                Next
                fobject.dt_edit.AcceptChanges()

                fobject.gv_edit.BestFitColumns()
            End If
            
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
