Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FLabourFeedback
    Dim ssql As String
    Dim _wo_oid_master As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Public __wolbr_wo_oid As String
    Public __wolbr_woop_oid As String

    Public ds_edit As DataSet

    Private Sub FPartnerAll_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
        start_date.DateTime = Now()
        end_date.DateTime = Now
    End Sub
    Public Overrides Sub load_cb()
        init_le(wolbr_en_id, "en_mstr")
        init_le(wolbr_op_move_complete, "yes_no")
        init_le(wolbr_op_move_complete_hold, "yes_no")
        init_le(wolbr_op_move_reject, "yes_no")
        init_le(wolbr_op_move_reject_hold, "yes_no")
        init_le(wolbr_op_move_rework, "yes_no")

    End Sub

    Public Overrides Sub format_grid()
        'master
        add_column(gv_master, "wolbr_oid", False)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Effective Date", "wolbr_eff_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Labour Feddback Code", "wolbr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "WO Code", "wo_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "WO Remarks", "wo_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Operation", "woop_op", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Employee", "emp_fname", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Runtime", "wolbr_run_time", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty Complete", "wolbr_qty_complete", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty Reject", "wolbr_qty_reject", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty Rework", "wolbr_qty_rework", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Operation Complete", "wolbr_op_complete", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Operation Move", "wolbr_op_move", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "wolbr_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "wolbr_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "wolbr_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "wolbr_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_reject_detail, "wolbrj_rea_id", False)
        add_column(gv_reject_detail, "Reason", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_reject_detail, "Qty", "wolbrj_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_rework_detail, "wolbrw_rea_id", False)
        add_column(gv_rework_detail, "Reason", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_rework_detail, "Qty", "wolbrw_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_reject, "wolbrj_rea_id", False)
        add_column(gv_reject, "Reason", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_reject, "Qty", "wolbrj_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_rework, "wolbrw_rea_id", False)
        add_column(gv_rework, "Reason", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_rework, "Qty", "wolbrw_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")


    End Sub
    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        Try
            If ds.Tables(0).Rows.Count = 0 Then
                Exit Sub
            End If

            Try
                ds.Tables("detail").Clear()
            Catch ex As Exception
            End Try
            ssql = "SELECT  " _
                        & "  a.wolbrj_rea_id, " _
                        & "  b.code_name, " _
                        & "  a.wolbrj_qty " _
                        & "FROM " _
                        & "  public.wolbrj_reject a " _
                        & "  INNER JOIN public.code_mstr b ON (a.wolbrj_rea_id = b.code_id) " _
                        & " Where wolbrj_wolbr_oid ='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wolbr_oid").ToString & "' "


            load_data_detail(ssql, gc_reject_detail, "detail_reject")

            ssql = "SELECT  " _
                    & "  a.wolbrw_rea_id, " _
                    & "  b.code_name, " _
                    & "  a.wolbrw_qty " _
                    & "FROM " _
                    & "  public.wolbrw_rework a " _
                    & "  INNER JOIN public.code_mstr b ON (a.wolbrw_rea_id = b.code_id) " _
                    & " Where wolbrw_wolbr_oid ='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wolbr_oid").ToString & "' "


            load_data_detail(ssql, gc_rework_detail, "detail_rework")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
            & "  a.wolbr_oid, " _
            & "  a.wolbr_en_id, " _
            & "  b.en_desc, " _
            & "  a.wolbr_add_by, " _
            & "  a.wolbr_add_date, " _
            & "  a.wolbr_upd_by, " _
            & "  a.wolbr_upd_date, " _
            & "  a.wolbr_eff_date, " _
            & "  a.wolbr_wo_oid,a.wolbr_code, " _
            & "  c.wo_code, " _
            & "  c.wo_remarks, " _
            & "  a.wolbr_woop_oid, " _
            & "  d.woop_op, " _
            & "  a.wolbr_emp_id, " _
            & "  e.emp_fname, " _
            & "  a.wolbr_run_time, " _
            & "  a.wolbr_qty_complete, " _
            & "  a.wolbr_qty_reject, " _
            & "  a.wolbr_qty_rework, " _
            & "  a.wolbr_op_complete, " _
            & "  a.wolbr_op_move_complete,wolbr_op_move_reject,wolbr_op_move_rework,wolbr_op_move_complete_hold,wolbr_op_move_reject_hold, " _
            & "  a.wolbr_dt " _
            & "FROM " _
            & "  public.wolbr_feedback a " _
            & "  INNER JOIN public.en_mstr b ON (a.wolbr_en_id = b.en_id) " _
            & "  INNER JOIN public.wo_mstr c ON (a.wolbr_wo_oid = c.wo_oid) " _
            & "  INNER JOIN public.woop_operation d ON (a.wolbr_woop_oid = d.woop_oid) " _
            & "  LEFT OUTER JOIN public.emp_mstr e ON (a.wolbr_emp_id = e.emp_id) " _
            & " Where a.wolbr_en_id in (select user_en_id from tconfuserentity " _
            & " where userid = " + master_new.ClsVar.sUserID.ToString + ") and wolbr_eff_date between " & SetDateNTime00(start_date.DateTime) & " and " & SetDateNTime00(end_date.DateTime) _
            & " ORDER BY wolbr_code"


        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()

        wolbr_en_id.ItemIndex = 0
        wolbr_code.Text = ""
        wolbr_wo_oid.Text = ""
        __wolbr_wo_oid = ""
        wolbr_woop_oid.Text = ""
        __wolbr_woop_oid = ""

        wolbr_eff_date.DateTime = Now
        wolbr_qty_complete.Text = ""
        wolbr_run_time.Text = ""
        wolbr_qty_rework.Text = ""
        wolbr_qty_reject.Text = ""

        Try
            tcg_header.SelectedTabPageIndex = 0
            dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        Catch ex As Exception
        End Try
    End Sub
    Public Overrides Sub insert_data_awal_2()
        wolbr_op_move_complete.EditValue = "Y"
        wolbr_op_move_complete_hold.EditValue = "N"
        wolbr_op_move_reject.EditValue = "Y"
        wolbr_op_move_reject_hold.EditValue = "N"
        wolbr_op_move_rework.EditValue = "Y"
    End Sub
    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  a.wolbrj_rea_id, " _
                        & "  b.code_name, " _
                        & "  a.wolbrj_qty " _
                        & "FROM " _
                        & "  public.wolbrj_reject a " _
                        & "  INNER JOIN public.code_mstr b ON (a.wolbrj_rea_id = b.code_id) " _
                        & " Where wolbrj_wolbr_oid is null "

                    .InitializeCommand()
                    .FillDataSet(ds_edit, "edit_reject")
                    gc_reject.DataSource = ds_edit.Tables(0)
                    gv_reject.BestFitColumns()

                    .SQL = "SELECT  " _
                        & "  a.wolbrw_rea_id, " _
                        & "  b.code_name, " _
                        & "  a.wolbrw_qty " _
                        & "FROM " _
                        & "  public.wolbrw_rework a " _
                        & "  INNER JOIN public.code_mstr b ON (a.wolbrw_rea_id = b.code_id) " _
                        & " Where wolbrw_wolbr_oid is null"

                    .InitializeCommand()
                    .FillDataSet(ds_edit, "edit_rework")
                    gc_rework.DataSource = ds_edit.Tables(1)
                    gv_rework.BestFitColumns()

                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Public Overrides Function insert() As Boolean
        Dim _lbr_code As String = ""
        Dim _oid_master As String = Guid.NewGuid.ToString
        Dim ssqls As New ArrayList

        _lbr_code = GetNewNumberYM("wolbr_feedback", "wolbr_code", 5, "LFB" & wolbr_en_id.GetColumnValue("en_code") _
                                       & CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)

        wolbr_code.EditValue = _lbr_code

        gc_reject.EmbeddedNavigator.Buttons.DoClick(gc_reject.EmbeddedNavigator.Buttons.EndEdit)
        ds_edit.Tables(0).AcceptChanges()

        gc_rework.EmbeddedNavigator.Buttons.DoClick(gc_rework.EmbeddedNavigator.Buttons.EndEdit)
        ds_edit.Tables(1).AcceptChanges()

        Try
            ssql = "INSERT INTO  " _
                & "  public.wolbr_feedback " _
                & "( " _
                & "  wolbr_oid, " _
                & "  wolbr_dom_id, " _
                & "  wolbr_en_id, " _
                & "  wolbr_add_by, " _
                & "  wolbr_add_date, " _
                & "  wolbr_eff_date, " _
                & "  wolbr_wo_oid, " _
                & "  wolbr_woop_oid, " _
                & "  wolbr_emp_id, " _
                & "  wolbr_run_time, " _
                & "  wolbr_qty_complete,wolbr_op_move_complete,wolbr_op_move_complete_hold, " _
                & "  wolbr_qty_reject,wolbr_op_move_reject,wolbr_op_move_reject_hold, " _
                & "  wolbr_qty_rework,wolbr_op_move_rework, " _
                & "  wolbr_dt, " _
                & "  wolbr_code " _
                & ")  " _
                & "VALUES ( " _
                & SetSetring(_oid_master) & ",  " _
                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                & SetInteger(wolbr_en_id.EditValue) & ",  " _
                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                & SetDateNTime(CekTanggal()) & ",  " _
                & SetDateNTime00(wolbr_eff_date.DateTime) & ",  " _
                & SetSetring(__wolbr_wo_oid) & ",  " _
                & SetSetring(__wolbr_woop_oid) & ",  " _
                & SetSetring("") & ",  " _
                & SetDec(wolbr_run_time.EditValue) & ",  " _
                & SetDec(wolbr_qty_complete.EditValue) & ",  " _
                & SetSetring(wolbr_op_move_complete.EditValue) & ",  " _
                & SetSetring(wolbr_op_move_complete_hold.EditValue) & ",  " _
                & SetDec(wolbr_qty_reject.EditValue) & ",  " _
                & SetSetring(wolbr_op_move_reject.EditValue) & ",  " _
                & SetSetring(wolbr_op_move_reject_hold.EditValue) & ",  " _
                & SetDec(wolbr_qty_rework.EditValue) & ",  " _
                & SetSetring(wolbr_op_move_rework.EditValue) & ",  " _
                & SetDateNTime(CekTanggal()) & ",  " _
                & SetSetring(wolbr_code.Text) & "  " _
                & ")"

            ssqls.Add(ssql)

            'woop_operation >> gambaran dari status operasi
            'wolbr_feedback >> inputan dari lapangan merupakan gambaran dari kinerja


            'woop ini ada kolom :
            '            wip()
            '            complete()
            '            complete(hold)
            '            reject()
            '            reject(hold)
            '            rework()
            '            rework(history)



            'feedback ada kolom :
            '            complete()
            '            reject()
            '            rework()
            'op_move:
            'complete:
            '		jika move maka complete tambahkan ke complete op tsb dan masukan ke wip op 		
            '            selanjutnya()
            '		jika tdk move maka masukan ke complete dan complete hold op tsb
            'reject:
            '		jika move maka reject tambahkan ke reject op tsb dan tambahkan ke rework dan 
            '		rework history op sebelumnya 
            '		jika tdk move maka cukup masukan ke reject dan reject hold op tsb
            'rework:
            '		jika move maka tambahkan ke complete op tsb dan kurangi rework op tsb terus
            '		tambahkan ke wip op selanjutnya 
            '		jika tdk move maka tambahkan ke complete op tsb kurangi rework op tsb dan
            ' 		tambahkan complete hold op tsb


            If SetNumber(wolbr_qty_complete.EditValue) > 0 Then
                'jika complete move
                If wolbr_op_move_complete.EditValue = "Y" Then
                    If wolbr_op_move_complete_hold.EditValue = "Y" Then
                        'jika complete move from hold
                        'kurangi ke complete hold op tsb
                        ssql = "UPDATE  " _
                            & "  public.woop_operation   " _
                            & "SET  " _
                            & "  woop_qty_complete_hold = coalesce(woop_qty_complete_hold,0) - " & SetDec(wolbr_qty_complete.EditValue) & "  " _
                            & "WHERE  " _
                            & "  woop_oid = " & SetSetring(__wolbr_woop_oid) & " "

                        ssqls.Add(ssql)

                        'tambahkan ke wip selanjutnya
                        ssql = "UPDATE  " _
                          & "  public.woop_operation   " _
                          & "SET  " _
                          & "  woop_qty_wip = coalesce(woop_qty_wip,0) + " & SetDec(wolbr_qty_complete.EditValue) & "  " _
                          & "WHERE  " _
                          & "  woop_oid = " & SetSetring(find_op_next(__wolbr_woop_oid)) & " "

                        ssqls.Add(ssql)
                    Else
                        'jika tidak
                        'tambahkan ke complete op tsb
                        ssql = "UPDATE  " _
                            & "  public.woop_operation   " _
                            & "SET  " _
                            & "  woop_qty_complete = coalesce(woop_qty_complete,0) + " & SetDec(wolbr_qty_complete.EditValue) & "  " _
                            & "WHERE  " _
                            & "  woop_oid = " & SetSetring(__wolbr_woop_oid) & " "

                        ssqls.Add(ssql)

                        'tambahkan ke wip selanjutnya
                        ssql = "UPDATE  " _
                          & "  public.woop_operation   " _
                          & "SET  " _
                          & "  woop_qty_wip = coalesce(woop_qty_wip,0) + " & SetDec(wolbr_qty_complete.EditValue) & "  " _
                          & "WHERE  " _
                          & "  woop_oid = " & SetSetring(find_op_next(__wolbr_woop_oid)) & " "

                        ssqls.Add(ssql)

                    End If
                Else
                    'jika tidak move
                    'tambahkan ke complete op tsb
                    'tambahkan ke complete hold op tsb
                    ssql = "UPDATE  " _
                        & "  public.woop_operation   " _
                        & "SET  " _
                        & "  woop_qty_complete = coalesce(woop_qty_complete,0) + " & SetDec(wolbr_qty_complete.EditValue) & ",  " _
                        & "  woop_qty_complete_hold = coalesce(woop_qty_complete_hold,0) + " & SetDec(wolbr_qty_complete.EditValue) & "  " _
                        & "WHERE  " _
                        & "  woop_oid = " & SetSetring(__wolbr_woop_oid) & " "

                    ssqls.Add(ssql)

                End If
            End If


            If SetNumber(wolbr_qty_reject.EditValue) > 0 Then
                'jika complete move
                If wolbr_op_move_reject.EditValue = "Y" Then
                    If wolbr_op_move_reject_hold.EditValue = "Y" Then
                        'jika complete move from hold
                        'kurangi ke complete hold op tsb
                        ssql = "UPDATE  " _
                            & "  public.woop_operation   " _
                            & "SET  " _
                            & "  woop_qty_reject_hold = coalesce(woop_qty_reject_hold,0) - " & SetDec(wolbr_qty_reject.EditValue) & "  " _
                            & "WHERE  " _
                            & "  woop_oid = " & SetSetring(__wolbr_woop_oid) & " "

                        ssqls.Add(ssql)

                        'tambahkan ke wip selanjutnya
                        ssql = "UPDATE  " _
                          & "  public.woop_operation   " _
                          & "SET  " _
                          & "  woop_qty_rework = coalesce(woop_qty_rework,0) + " & SetDec(wolbr_qty_reject.EditValue) & ",  " _
                          & "  woop_qty_rework_history = coalesce(woop_qty_rework_history,0) + " & SetDec(wolbr_qty_reject.EditValue) & "  " _
                          & "WHERE  " _
                          & "  woop_oid = " & SetSetring(find_op_before(__wolbr_woop_oid)) & " "

                        ssqls.Add(ssql)

                    Else
                        'jika tidak
                        'tambahkan ke complete op tsb
                        ssql = "UPDATE  " _
                            & "  public.woop_operation   " _
                            & "SET  " _
                            & "  woop_qty_reject = coalesce(woop_qty_reject,0) + " & SetDec(wolbr_qty_reject.EditValue) & "  " _
                            & "WHERE  " _
                            & "  woop_oid = " & SetSetring(__wolbr_woop_oid) & " "

                        ssqls.Add(ssql)

                        'tambahkan ke wip selanjutnya
                        ssql = "UPDATE  " _
                          & "  public.woop_operation   " _
                          & "SET  " _
                          & "  woop_qty_rework = coalesce(woop_qty_rework,0) + " & SetDec(wolbr_qty_reject.EditValue) & ",  " _
                          & "  woop_qty_rework_history = coalesce(woop_qty_rework_history,0) + " & SetDec(wolbr_qty_reject.EditValue) & "  " _
                          & "WHERE  " _
                          & "  woop_oid = " & SetSetring(find_op_before(__wolbr_woop_oid)) & " "

                        ssqls.Add(ssql)

                    End If
                Else
                    'jika tidak move
                    'tambahkan ke complete op tsb
                    'tambahkan ke complete hold op tsb
                    ssql = "UPDATE  " _
                        & "  public.woop_operation   " _
                        & "SET  " _
                        & "  woop_qty_reject = coalesce(woop_qty_reject,0) + " & SetDec(wolbr_qty_reject.EditValue) & ",  " _
                        & "  woop_qty_reject_hold = coalesce(woop_qty_reject_hold,0) + " & SetDec(wolbr_qty_reject.EditValue) & "  " _
                        & "WHERE  " _
                        & "  woop_oid = " & SetSetring(__wolbr_woop_oid) & " "

                    ssqls.Add(ssql)

                End If
            End If


            If SetNumber(wolbr_qty_rework.EditValue) > 0 Then
                'jika complete move
                If wolbr_op_move_reject.EditValue = "Y" Then
                    'jika move
                    'tambahkan ke complete op tsb
                    ssql = "UPDATE  " _
                        & "  public.woop_operation   " _
                        & "SET  " _
                        & "  woop_qty_complete = coalesce(woop_qty_complete,0) + " & SetDec(wolbr_qty_rework.EditValue) & ",  " _
                        & "  woop_qty_rework = coalesce(woop_qty_rework,0) - " & SetDec(wolbr_qty_rework.EditValue) & "  " _
                        & "WHERE  " _
                        & "  woop_oid = " & SetSetring(__wolbr_woop_oid) & " "

                    ssqls.Add(ssql)

                    'tambahkan ke wip selanjutnya
                    ssql = "UPDATE  " _
                      & "  public.woop_operation   " _
                      & "SET  " _
                      & "  woop_qty_wip = coalesce(woop_qty_wip,0) + " & SetDec(wolbr_qty_rework.EditValue) & "  " _
                      & "WHERE  " _
                      & "  woop_oid = " & SetSetring(find_op_next(__wolbr_woop_oid)) & " "

                    ssqls.Add(ssql)

                Else
                    'jika tidak move
                    'tambahkan ke complete op tsb
                    ssql = "UPDATE  " _
                        & "  public.woop_operation   " _
                        & "SET  " _
                        & "  woop_qty_complete = coalesce(woop_qty_complete,0) + " & SetDec(wolbr_qty_rework.EditValue) & ",  " _
                        & "  woop_qty_rework = coalesce(woop_qty_rework,0) - " & SetDec(wolbr_qty_rework.EditValue) & ",  " _
                        & "  woop_qty_complete_hold = coalesce(woop_qty_complete_hold,0) + " & SetDec(wolbr_qty_rework.EditValue) & "  " _
                        & "WHERE  " _
                        & "  woop_oid = " & SetSetring(__wolbr_woop_oid) & " "

                    ssqls.Add(ssql)

                End If
            End If

            For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
                With ds_edit.Tables(0).Rows(i)
                    ssql = "INSERT INTO  " _
                        & "  public.wolbrj_reject " _
                        & "( " _
                        & "  wolbrj_oid, " _
                        & "  wolbrj_wolbr_oid, " _
                        & "  wolbrj_add_by, " _
                        & "  wolbrj_add_date, " _
                        & "  wolbrj_rea_id, " _
                        & "  wolbrj_qty, " _
                        & "  wolbrj_dt " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_oid_master) & ",  " _
                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                        & SetSetring(CekTanggal) & ",  " _
                        & SetInteger(.Item("wolbrj_rea_id")) & ",  " _
                        & SetDec(.Item("wolbrj_qty")) & ",  " _
                        & SetDateNTime(CekTanggal()) & "  " _
                        & ")"


                    ssqls.Add(ssql)
                End With
            Next

            For i As Integer = 0 To ds_edit.Tables(1).Rows.Count - 1
                With ds_edit.Tables(1).Rows(i)
                    ssql = "INSERT INTO  " _
                        & "  public.wolbrw_rework " _
                        & "( " _
                        & "  wolbrw_oid, " _
                        & "  wolbrw_wolbr_oid, " _
                        & "  wolbrw_add_by, " _
                        & "  wolbrw_add_date, " _
                        & "  wolbrw_rea_id, " _
                        & "  wolbrw_qty, " _
                        & "  wolbrw_dt " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_oid_master) & ",  " _
                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                        & SetSetring(CekTanggal) & ",  " _
                        & SetSetring(.Item("wolbrw_rea_id")) & ",  " _
                        & SetSetring(.Item("wolbrw_qty")) & ",  " _
                        & SetSetring(CekTanggal) & "  " _
                        & ")"

                    ssqls.Add(ssql)
                End With
            Next

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Return False
                    Exit Function
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Return False
                    Exit Function
                End If
                ssqls.Clear()
            End If


            after_success()
            set_row(_oid_master, "wolbr_oid")
            dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
            insert = True


        Catch ex As Exception
            row = 0
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function
    Function find_op_next(ByVal _woop_oid_curent As String) As String
        ssql = "select woop_oid from woop_operation where woop_op > " _
            & "(select woop_op from woop_operation where woop_oid='" & _woop_oid_curent & "') and woop_wo_oid='" & __wolbr_wo_oid & "'  order by woop_op limit 1"
        Dim dr As DataRow
        dr = GetRowInfo(ssql)

        If dr Is Nothing Then
            Return ""
        Else
            Return dr(0)
        End If

    End Function
    Function find_op_before(ByVal _woop_oid_curent As String) As String
        ssql = "select woop_oid from woop_operation where woop_op < " _
            & "(select woop_op from woop_operation where woop_oid='" & _woop_oid_curent & "')  and woop_wo_oid='" & __wolbr_wo_oid & "' order by woop_op limit 1 "
        Dim dr As DataRow
        dr = GetRowInfo(ssql)

        If dr Is Nothing Then
            Return ""
        Else
            Return dr(0)
        End If

    End Function
    Public Overrides Function edit_data() As Boolean

        If MyBase.edit_data = True Then
            'prj_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)

                '_wo_oid_master = .Item("prj_oid")
                'prj_en_id.EditValue = .Item("prj_en_id")
                'prjdr_code.EditValue = .Item("prj_code")
                'prj_si_id.EditValue = .Item("prj_si_id")
                'prj_type_id.EditValue = .Item("prj_type_id")

                'prjdr_date.DateTime = .Item("prj_date_ord")
                'prj_date_end.DateTime = .Item("prj_date_end")
                'prj_date_start.DateTime = .Item("prj_date_start")

                'prjdr_prj_oid.Text = .Item("pt_desc1")
                '__prj_pt_id = .Item("prj_pt_id")
                'prj_ro_id.Text = .Item("ro_desc")
                '__prj_ro_id = .Item("prj_ro_id")
                'prj_bom_id.Text = SetString(.Item("bom_desc"))
                '__prj_bom_id = SetString(.Item("prj_bom_id"))
                'prj_remarks.EditValue = SetString(.Item("prj_remarks"))
                'prj_ptnr_id.Text = .Item("ptnr_name")
                '__prj_ptnr_id = .Item("prj_ptnr_id")
                'prj_cst_id.Text = .Item("cst_desc")
                '__prj_cs_id = .Item("prj_cst_id")

                'prj_cu_id.EditValue = .Item("prj_cu_id")
                'prjdr_remarks.Text = SetString(.Item("prj_po_nbr"))
                'prj_qty.EditValue = .Item("prj_qty")
                'prj_price.EditValue = .Item("prj_price")
                'prj_spc_ord.EditValue = SetBitYNB(.Item("prj_spc_ord"))

            End With

            ds_edit = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                       & "  a.prjd_oid, " _
                       & "  a.prjd_prj_oid, " _
                       & "  a.prjd_seq, " _
                       & "  a.prjd_cse_id, " _
                       & "  b.cse_code, " _
                       & "  b.cse_desc, " _
                       & "  a.prjd_cost_est, " _
                       & "  a.prjd_cost_rea, " _
                       & "  a.prjd_var, " _
                       & "  a.prjd_remarks " _
                       & "FROM " _
                       & "  public.prjd_det a " _
                       & "  INNER JOIN public.cse_mstr b ON (a.prjd_cse_id = b.cse_id) " _
                       & "WHERE " _
                       & "  a.prjd_prj_oid='" & ds.Tables(0).Rows(row).Item("prj_oid").ToString & "' " _
                       & "ORDER BY " _
                       & "  a.prjd_seq"


                        .InitializeCommand()
                        .FillDataSet(ds_edit, "edit")
                        gc_reject.DataSource = ds_edit.Tables(0)
                        gv_reject.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            'ds_edit = ds.Copy
            'gc_edit.DataSource = ds_edit.Tables(0)
            'gv_edit.BestFitColumns()
            Try
                tcg_header.SelectedTabPageIndex = 0
                dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
            Catch ex As Exception
            End Try

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()

        gc_reject.EmbeddedNavigator.Buttons.DoClick(gc_reject.EmbeddedNavigator.Buttons.EndEdit)
        ds_edit.Tables(0).AcceptChanges()

        edit = True
        Dim ssqls As New ArrayList
        Try

            'ssql = "UPDATE  " _
            '    & "  public.prj_mstr   " _
            '    & "SET  " _
            '    & "  prj_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
            '    & "  prj_en_id = " & SetInteger(prj_en_id.EditValue) & ",  " _
            '    & "  prj_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
            '    & "  prj_upd_date = " & SetDateNTime(CekTanggal) & ",  " _
            '    & "  prj_code = " & SetSetring(prj_code.Text) & ",  " _
            '    & "  prj_date_ord = " & SetDateNTime00(prj_date_ord.DateTime) & ",  " _
            '    & "  prj_date_start = " & SetDateNTime00(prj_date_start.DateTime) & ",  " _
            '    & "  prj_date_end = " & SetDateNTime00(prj_date_end.DateTime) & ",  " _
            '    & "  prj_type_id = " & SetInteger(prj_type_id.EditValue) & ",  " _
            '    & "  prj_ptnr_id = " & SetInteger(__prj_ptnr_id) & ",  " _
            '    & "  prj_pt_id = " & SetInteger(__prj_pt_id) & ",  " _
            '    & "  prj_qty = " & SetDec(prj_qty.EditValue) & ",  " _
            '    & "  prj_bom_id = " & SetInteger(__prj_bom_id) & ",  " _
            '    & "  prj_ro_id = " & SetInteger(__prj_ro_id) & ",  " _
            '    & "  prj_po_nbr = " & SetSetring(prj_po_nbr.Text) & ",  " _
            '    & "  prj_si_id = " & SetInteger(prj_si_id.EditValue) & ",  " _
            '    & "  prj_cu_id = " & SetInteger(prj_cu_id.EditValue) & ",  " _
            '    & "  prj_cst_id = " & SetInteger(__prj_cs_id) & ",  " _
            '    & "  prj_price = " & SetDec(prj_price.EditValue) & ",  " _
            '    & "  prj_spc_ord = " & SetBitYN(prj_spc_ord.EditValue) & ",  " _
            '    & "  prj_remarks = " & SetSetring(prj_remarks.Text) & "  " _
            '    & "WHERE  " _
            '    & "  prj_oid = " & SetSetring(_wo_oid_master) & " "

            ssqls.Add(ssql)

            ssql = "delete from prjd_det where prjd_prj_oid=" & SetSetring(_wo_oid_master)
            ssqls.Add(ssql)

            For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
                With ds_edit.Tables(0).Rows(i)
                    ssql = "INSERT INTO  " _
                        & "  public.prjd_det " _
                        & "( " _
                        & "  prjd_oid, " _
                        & "  prjd_prj_oid, " _
                        & "  prjd_add_by, " _
                        & "  prjd_add_date, " _
                        & "  prjd_seq, " _
                        & "  prjd_cse_id, " _
                        & "  prjd_cost_est, " _
                        & "  prjd_cost_rea, " _
                        & "  prjd_var, " _
                        & "  prjd_remarks, " _
                        & "  prjd_dt " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_wo_oid_master) & ",  " _
                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                        & SetDateNTime(CekTanggal) & ",  " _
                        & SetInteger(i) & ",  " _
                        & SetInteger(.Item("prjd_cse_id")) & ",  " _
                        & SetDec(.Item("prjd_cost_est")) & ",  " _
                        & SetDec(.Item("prjd_cost_rea")) & ",  " _
                        & SetSetring(.Item("prjd_var")) & ",  " _
                        & SetSetring(.Item("prjd_remarks")) & ",  " _
                        & SetDateNTime(CekTanggal) & "  " _
                        & ")"
                    ssqls.Add(ssql)
                End With
            Next

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Return False
                    Exit Function
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Return False
                    Exit Function
                End If
                ssqls.Clear()
            End If


            after_success()
            set_row(Trim(_wo_oid_master.ToString), "prj_oid")
            dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
            edit = True

        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
    End Function

    Public Overrides Function delete_data() As Boolean
        If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_status") = "R" Then
            Return False
            Exit Function
        End If

        delete_data = True
        If ds.Tables.Count = 0 Then
            delete_data = False
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            delete_data = False
            Exit Function
        End If

        Dim ssqls As New ArrayList

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
            End If

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from wo_mstr where wo_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If master_new.PGSqlConn.status_sync = True Then
                                For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = Data
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next
                            End If

                            sqlTran.Commit()

                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                            delete_data = False
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                delete_data = False
            End Try
        End If

        Return delete_data
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True


        If required(wolbr_wo_oid, "Work Order") = False Then
            Return False
            Exit Function
        End If


        Return before_save
    End Function

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_reject.KeyDown, gv_rework.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_reject.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_reject.DeleteSelectedRows()
        End If
    End Sub
    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_reject.DoubleClick, gv_rework.DoubleClick
        Try
            browse_data()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Private Sub browse_data()

        If XtraTabControl1.SelectedTabPageIndex = 0 Then
            Dim _col As String = gv_reject.FocusedColumn.Name
            Dim _row As Integer = gv_reject.FocusedRowHandle

            If _col = "code_name" Then
                Dim frm As New FReasonSearch
                frm.set_win(Me)
                frm._en_id = wolbr_en_id.EditValue
                frm._row = _row
                frm._filter = "reject"
                frm._caption = "Reason Reject"
                frm._code_field = "reason_reject"
                frm.type_form = True
                frm.ShowDialog()
            End If

        Else
            Dim _col As String = gv_rework.FocusedColumn.Name
            Dim _row As Integer = gv_rework.FocusedRowHandle

            If _col = "code_name" Then
                Dim frm As New FReasonSearch
                frm.set_win(Me)
                frm._row = _row
                frm._en_id = wolbr_en_id.EditValue
                frm._filter = "rework"
                frm._caption = "Reason Rework"
                frm._code_field = "reason_rework"
                frm.type_form = True
                frm.ShowDialog()
            End If

        End If

    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_reject.KeyPress, gv_rework.KeyPress
        Try
            If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
                browse_data()
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub wolbr_wo_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles wolbr_wo_oid.ButtonClick
        Try

            Dim frm As New FWOSearch
            frm.set_win(Me)
            'frm._obj = wolbr_wo_oid
            frm._en_id = wolbr_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub


    Private Sub wolbr_woop_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles wolbr_woop_oid.ButtonClick
        Try
            If __wolbr_wo_oid = "" Then
                Box("Please select WO first")
                Exit Sub
            End If
            Dim frm As New FOperationSearch
            frm.set_win(Me)
            frm._obj = wolbr_woop_oid
            frm._en_id = wolbr_en_id.EditValue
            frm._wo_oid = __wolbr_wo_oid
            frm.type_form = True
            frm.ShowDialog()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
