Imports npgsql
Imports master_new.ModFunction

Public Class FRosettaRuleReport
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FInventoryAdjusmentPlusReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        form_first_load()
        _now = func_coll.get_now
      
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_view1, "rstrule_oid", False)
        add_column_copy(gv_view1, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Account Group", "group_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Account Name", "account_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Transaction Line", "tranline_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Cashflow Code", "cashflow_name", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "Remarks", "rstrule_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "User Create", "rstrule_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Date Create", "rstrule_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_view1, "User Update", "rstrule_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Date Update", "rstrule_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_view1, "rstruled_oid", False)
        add_column(gv_view1, "rstruled_rstrule_oid", False)
        add_column_copy(gv_view1, "Account Code", "ac_code1", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "Account Name", "ac_name1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Account Sign", "rstruled_sign1", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "For Account Code", "ac_code2", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "For Account Name", "ac_name2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "For Account Sign", "rstruled_sign2", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        If xtc_master.SelectedTabPageIndex = 0 Then
                            .SQL = "SELECT  " _
                                & "  rstrule_oid, " _
                                & "  rstrule_dom_id, " _
                                & "  rstrule_en_id, " _
                                & "  rstrule_add_by, " _
                                & "  rstrule_add_date, " _
                                & "  rstrule_upd_by, " _
                                & "  rstrule_upd_date, " _
                                & "  rstrule_group_id, " _
                                & "  rstrule_name_id, " _
                                & "  rstrule_line_id, " _
                                & "  rstrule_cashflow_id, " _
                                & "  rstrule_dt, " _
                                & "  rstrule_remarks, " _
                                & "  en_desc, " _
                                & "  group_mstr.code_name as group_name, " _
                                & "  account_mstr.code_name as account_name, " _
                                & "  tranline_mstr.code_name as tranline_name, " _
                                & "  cashflow_mstr.code_name as cashflow_name, " _
                                  & "  rstruled_oid, " _
                                & "  rstruled_rstrule_oid, " _
                                & "  rstruled_seq, " _
                                & "  rstruled_ac_id1, " _
                                & "  ac_mstr1.ac_code as ac_code1, " _
                                & "  ac_mstr1.ac_name as ac_name1, " _
                                & "  rstruled_sign1, " _
                                & "  rstruled_ac_id2, " _
                                & "  ac_mstr2.ac_code as ac_code2, " _
                                & "  ac_mstr2.ac_name as ac_name2, " _
                                & "  rstruled_sign2 " _
                                & "FROM  " _
                                & "  public.rstrule_mstr" _
                                & "  INNER JOIN public.en_mstr ON (public.rstrule_mstr.rstrule_en_id = public.en_mstr.en_id)" _
                                & "  INNER JOIN code_mstr group_mstr on rstrule_group_id = group_mstr.code_id " _
                                & "  INNER JOIN code_mstr account_mstr on rstrule_name_id = account_mstr.code_id " _
                                & "  INNER JOIN code_mstr tranline_mstr on rstrule_line_id = tranline_mstr.code_id " _
                                & "  INNER JOIN code_mstr cashflow_mstr on rstrule_cashflow_id = cashflow_mstr.code_id " _
                                 & " INNER JOIN public.rstruled_det on rstruled_rstrule_oid=rstrule_oid " _
                                & "  INNER JOIN ac_mstr ac_mstr1 ON (rstruled_ac_id1 = ac_mstr1.ac_id) " _
                                & "  LEFT OUTER JOIN ac_mstr ac_mstr2 ON (rstruled_ac_id2 = ac_mstr2.ac_id) "


                            .InitializeCommand()
                            .FillDataSet(ds, "view1")
                            gc_view1.DataSource = ds.Tables("view1")
                        End If

                        bestfit_column()
                        ConditionsAdjustment()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub
End Class
