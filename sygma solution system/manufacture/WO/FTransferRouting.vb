Imports npgsql
Imports master_new.ModFunction


Public Class FTransferRouting
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _oid_mstr As String
    Dim sSQLs As New ArrayList

    Private Sub FItemSub_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = Now
        pr_txttglakhir.DateTime = Now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        trans_en_id.Properties.DataSource = dt_bantu
        trans_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        trans_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        trans_en_id.ItemIndex = 0

        pr_entity.Properties.DataSource = dt_bantu
        pr_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        pr_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        pr_entity.ItemIndex = 0

    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "trans_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transaction Number", "trans_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Work Order Number", "wo_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Operation from", "op_name_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Work Center from", "wc_desc_from", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Operation to", "op_name_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Work Center to", "wc_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty", "trans_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Conversion Out", "trans_qty_routing_conversion_from", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Conversion To", "trans_qty_conversion", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "Remarks", "trans_remarks", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "trans_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "trans_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "trans_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "trans_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  a.trans_code, " _
                & "  b.en_desc, " _
                & "  f.op_name AS op_name_from, " _
                & "  a.trans_oid,trans_qty_conversion, " _
                & "  a.trans_date, " _
                & "  a.trans_wo_oid, " _
                & "  e.wo_code, " _
                & "  a.trans_en_id, " _
                & "  a.trans_dom_id, " _
                & "  a.trans_wodr_uid_from, " _
                & "  d.wc_desc AS wc_desc_from, " _
                & "  a.trans_wodr_uid_to, " _
                & "  h.op_name AS op_name_to,trans_qty_routing_conversion_from,trans_qty_routing_conversion_to, " _
                & "  i.wc_desc AS wc_desc_to, " _
                & "  a.trans_qty, " _
                & "  a.trans_remarks, " _
                & "  a.trans_add_by, " _
                & "  a.trans_add_date, " _
                & "  a.trans_upd_by, " _
                & "  a.trans_upd_date " _
                & "FROM " _
                & "  public.wodr_routing c " _
                & "  left outer JOIN public.op_mstr f ON (c.wodr_op = f.op_code) " _
                & "  INNER JOIN public.transrouting_mstr a ON (c.wodr_uid = a.trans_wodr_uid_from) " _
                & "  INNER JOIN public.wo_mstr e ON (a.trans_wo_oid = e.wo_oid) " _
                & "  INNER JOIN public.en_mstr b ON (b.en_id = a.trans_en_id) " _
                & "  INNER JOIN public.wc_mstr d ON (c.wodr_wc_id = d.wc_id) " _
                & "  INNER JOIN public.wodr_routing g ON (a.trans_wodr_uid_to = g.wodr_uid) " _
                & "  left outer  JOIN public.op_mstr h ON (g.wodr_op = h.op_code) " _
                & "  INNER JOIN public.wc_mstr i ON (g.wodr_wc_id = i.wc_id) " _
                & "WHERE " _
                & "  a.trans_en_id = " & SetInteger(pr_entity.EditValue) & " AND  " _
                & "  a.trans_date BETWEEN " & SetDate(pr_txttglawal.DateTime) & " AND " & SetDate(pr_txttglakhir.DateTime) & " " _
                & " order by trans_code"


        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        trans_en_id.ItemIndex = 0
        trans_en_id.Focus()
        trans_date.EditValue = master_new.PGSqlConn.CekTanggal
        trans_wo_oid.Tag = ""
        trans_wo_oid.Text = ""

        trans_wodr_uid_from.Tag = ""
        trans_wodr_uid_from.Text = ""
        trans_wodr_uid_to.Tag = ""
        trans_wodr_uid_to.Text = ""

        'wc_name_from.EditValue = ""
        'wc_name_to.EditValue = ""

        trans_qty.EditValue = 0.0
        trans_remarks.EditValue = ""
    End Sub

    Public Overrides Function insert() As Boolean

        _oid_mstr = Guid.NewGuid.ToString
        Try
            Dim _code As String
            sSQLs.Clear()
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        '_code = func_coll.get_transaction_number("TR", trans_en_id.GetColumnValue("en_code"), "transrouting_mstr", "trans_code", trans_date.DateTime)

                        _code = master_new.PGSqlConn.GetNewNumberYM("transrouting_mstr", "trans_code", 5, "TR" _
                            & trans_en_id.GetColumnValue("en_code") & (master_new.PGSqlConn.CekTanggal.Date.ToString("yyMM")) & master_new.ClsVar.sServerCode)


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                    & "  public.transrouting_mstr " _
                                    & "( " _
                                    & "  trans_oid, " _
                                    & "  trans_dom_id, " _
                                    & "  trans_en_id, " _
                                    & "  trans_wodr_uid_from,trans_qty_routing_conversion_from, " _
                                    & "  trans_wodr_uid_to,trans_qty_routing_conversion_to,trans_qty_conversion, " _
                                    & "  trans_qty, " _
                                    & "  trans_add_by, " _
                                    & "  trans_add_date, " _
                                    & "  trans_remarks, " _
                                    & "  trans_date, " _
                                    & "  trans_wo_oid, " _
                                    & "  trans_code " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(_oid_mstr) & ",  " _
                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                    & SetInteger(trans_en_id.EditValue) & ",  " _
                                    & SetSetring(trans_wodr_uid_from.Tag) & ",  " _
                                    & SetDec(trans_qty_routing_conversion_from.EditValue) & ",  " _
                                    & SetSetring(trans_wodr_uid_to.Tag) & ",  " _
                                    & SetDec(0) & ",  " _
                                    & SetDec(trans_qty_conversion.EditValue) & ",  " _
                                    & SetDec(trans_qty.EditValue) & ",  " _
                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                    & SetSetring(trans_remarks.EditValue) & ",  " _
                                    & SetDate(trans_date.EditValue) & ",  " _
                                    & SetSetring(trans_wo_oid.Tag) & ",  " _
                                    & SetSetring(_code) & "  " _
                                    & ")"

                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'update wr_routing
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                            & "  public.wodr_routing   " _
                            & "SET  " _
                            & "  wodr_qty_out = coalesce(wodr_qty_out,0) + " & SetDec(trans_qty.EditValue) & "  " _
                            & "WHERE  " _
                            & "  wodr_uid = " & SetSetring(trans_wodr_uid_from.Tag) & " "

                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                            & "  public.wodr_routing   " _
                            & "SET  " _
                            & "  wodr_qty_in = coalesce(wodr_qty_in,0) + " & SetDec(trans_qty_conversion.EditValue) & "  " _
                            & "WHERE  " _
                            & "  wodr_uid = " & SetSetring(trans_wodr_uid_to.Tag) & " "

                        sSQLs.Add(.Command.CommandText)
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
                        after_success()
                        set_row(Trim(_oid_mstr), "trans_oid")
                        insert = True
                    Catch ex As nPgSqlException
                        insert = False
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            row = 0
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function

    Public Overrides Function edit_data() As Boolean
        Box("This menu is not available")
        Return False
        Exit Function
        If MyBase.edit_data = True Then


            trans_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _oid_mstr = .Item("lbrf_oid").ToString
                trans_en_id.EditValue = .Item("lbrf_en_id")
                trans_wodr_uid_from.Tag = .Item("lbrf_wodr_uid").ToString
                trans_wodr_uid_from.Text = .Item("wo_code")
                'wc_name_from.EditValue = SetNumber(.Item("op_name"))
                trans_date.DateTime = .Item("lbrf_date")
                trans_qty.EditValue = SetNumber(.Item("lbrf_qty_complete"))
             
                trans_remarks.EditValue = SetString(.Item("lbrf_remarks"))

            End With

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        sSQLs.Clear()
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        '.Command.CommandText = "UPDATE  " _
                        '            & "  public.wrd_det   " _
                        '            & "SET  " _
                        '            & "  wrd_en_id = " & SetInteger(wrd_en_id.EditValue) & ",  " _
                        '            & "  wrd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                        '            & "  wrd_upd_date = " & "current_timestamp" & ",  " _
                        '            & "  wrd_dt = " & "current_timestamp" & ",  " _
                        '            & "  wrd_wr_oid = " & SetSetring(wrd_wr_oid.Tag) & ",  " _
                        '            & "  wrd_eff_date = " & SetDate(wrd_eff_date.DateTime) & ",  " _
                        '            & "  wrd_qty = " & SetDec(wrd_qty.EditValue) & ",  " _
                        '            & "  wrd_start_setup = " & SetDateNTime(wrd_start_setup.EditValue) & ",  " _
                        '            & "  wrd_stop_setup = " & SetDateNTime(wrd_stop_setup.EditValue) & ",  " _
                        '            & "  wrd_elapsed_setup = " & SetDec(wrd_elapsed_setup.EditValue) & ",  " _
                        '            & "  wrd_start_run = " & SetDateNTime(wrd_start_run.EditValue) & ",  " _
                        '            & "  wrd_stop_run = " & SetDateNTime(wrd_stop_run.EditValue) & ",  " _
                        '            & "  wrd_elapsed_run = " & SetDec(wrd_elapsed_run.EditValue) & ",  " _
                        '            & "  wrd_down_time = " & SetDec(wrd_down_time.EditValue) & ",  " _
                        '            & "  wrd_down_reason_id = " & SetInteger(wrd_down_reason_id.EditValue) & ",  " _
                        '            & "  wrd_sub_cost = " & SetDec(wrd_sub_cost.EditValue) & ",  " _
                        '            & "  wrd_remark = " & SetSetring(wrd_remark.Text) & "  " _
                        '            & "WHERE  " _
                        '            & "  wrd_oid = " & SetSetring(_oid_mstr) & " "


                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'update wr_routing penyesuaian awal
                        Dim _row As Integer = BindingContext(ds.Tables(0)).Position
                        .Command.CommandType = CommandType.Text
                        '.Command.CommandText = "Update wr_route set wr_setup_real=coalesce(wr_setup_real,0) - " & SetDec(ds.Tables(0).Rows(_row).Item("wrd_elapsed_setup")) _
                        '                    & ", wr_run_real=coalesce(wr_run_real,0) - " & SetDec(ds.Tables(0).Rows(_row).Item("wrd_elapsed_run")) _
                        '                    & ", wr_sub_cost_real=coalesce(wr_sub_cost_real,0) - " & SetDec(ds.Tables(0).Rows(_row).Item("wrd_sub_cost")) _
                        '                    & " , wr_qty_feedback=coalesce(wr_qty_feedback,0) - " & SetDec(ds.Tables(0).Rows(_row).Item("wrd_qty")) _
                        '                    & " WHERE wr_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wrd_wr_oid") & "'"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'update wr_routing
                        .Command.CommandType = CommandType.Text
                        '.Command.CommandText = "Update wr_route set wr_setup_real=coalesce(wr_setup_real,0) + " & SetDec(wrd_elapsed_setup.EditValue) _
                        '                    & ", wr_run_real=coalesce(wr_run_real,0) + " & SetDec(wrd_elapsed_run.EditValue) _
                        '                    & ", wr_sub_cost_real=coalesce(wr_sub_cost_real,0) + " & SetDec(wrd_sub_cost.EditValue) _
                        '                    & " , wr_qty_feedback=coalesce(wr_qty_feedback,0) + " & SetDec(wrd_qty.EditValue) _
                        '                    & " WHERE wr_oid='" & wrd_wr_oid.Tag & "'"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        after_success()
                        set_row(Trim(_oid_mstr.ToString), "wrd_oid")
                        edit = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
    End Function

    Public Overrides Function delete_data() As Boolean
        delete_data = True
        If ds.Tables.Count = 0 Then
            delete_data = False
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            delete_data = False
            Exit Function
        End If

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " akan menghapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
            End If
            sSQLs.Clear()
            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            'update wr_routing penyesuaian awal
                            Dim _row As Integer = BindingContext(ds.Tables(0)).Position
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                    & "  public.wodr_routing   " _
                                    & "SET  " _
                                    & "  wodr_qty_out = coalesce(wodr_qty_out,0) - " & SetDec(ds.Tables(0).Rows(_row).Item("trans_qty")) & "  " _
                                    & "WHERE  " _
                                    & "  wodr_uid = " & SetSetring(ds.Tables(0).Rows(_row).Item("trans_wodr_uid_from")) & " "

                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                    & "  public.wodr_routing   " _
                                    & "SET  " _
                                    & "  wodr_qty_in = coalesce(wodr_qty_in,0) - " & SetDec(ds.Tables(0).Rows(_row).Item("trans_qty_conversion")) & "  " _
                                    & "WHERE  " _
                                    & "  wodr_uid = " & SetSetring(ds.Tables(0).Rows(_row).Item("trans_wodr_uid_to")) & " "

                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from transrouting_mstr where trans_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("trans_oid").ToString + "'"
                            sSQLs.Add(.Command.CommandText)
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
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        Return delete_data
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        Dim sSQL As String
        Try
            'sSQL = "select count (*) as jml from wr_route where coalesce(wr_qty_feedback,0) + " _
            '    & SetDec(lbrf_qty_complete.EditValue) & " > coalesce(wr_qty_wo) and wr_oid='" & lbrf_wodr_uid.Tag & "'"

            'If master_new.PGSqlConn.GetRowInfo(sSQL)(0) > 0 Then
            '    Box("Qty feedback over than qty WO")
            '    Return False
            '    Exit Function
            'End If

            If trans_wodr_uid_from.Tag = "" Then
                Box("WO Code can't blank")
                Return False
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try

        Return before_save
    End Function

    Private Sub wrd_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trans_en_id.EditValueChanged
        'init_le(lbrf_down_reason_id, "down_reason", trans_en_id.EditValue)
    End Sub

    Private Sub wrd_wr_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles trans_wodr_uid_from.ButtonClick
        Dim frm As New FWORoutingSearch
        frm.set_win(Me)
        frm._en_id = trans_en_id.EditValue
        frm._obj = trans_wodr_uid_from
        frm._wo_oid = trans_wo_oid.Tag
        frm.type_form = True
        frm.ShowDialog()

    End Sub


    Private Sub lbrf_start_setup_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub trans_wo_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles trans_wo_oid.ButtonClick
        Dim frm As New FWOSearch
        frm.set_win(Me)
        frm._en_id = trans_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub trans_wodr_uid_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles trans_wodr_uid_to.ButtonClick
        Dim frm As New FWORoutingSearch
        frm.set_win(Me)
        frm._en_id = trans_en_id.EditValue
        frm._obj = trans_wodr_uid_to
        frm._wo_oid = trans_wo_oid.Tag
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub trans_qty_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trans_qty.EditValueChanged
        Try
            Dim _conv_from, _conv_to, _qty, _qty_conv As Double
            _conv_from = 0
            _conv_to = 0
            _qty = 0
            _qty_conv = 0



            _conv_from = SetNumber(trans_qty_routing_conversion_from.EditValue)
            '_conv_to = SetNumber(trans_qty_routing_conversion_to.EditValue)
            _qty = SetNumber(trans_qty.EditValue)
            '_qty_conv = SetNumber(trans_qty_conversion.EditValue)

            'If _conv_from < _conv_to Then
            '    _qty_conv = _qty * (_conv_to / _conv_from)
            'ElseIf _conv_from > _conv_to Then
            '    _qty_conv = _qty * (_conv_from / _conv_to)
            'Else
            '    _qty_conv = _qty
            'End If
            trans_qty_conversion.EditValue = _qty * _conv_from

        Catch ex As Exception

        End Try
    End Sub
End Class
