Imports npgsql
Imports master_new.ModFunction

Public Class FPriceListCopy
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public _pi_oid_from, _pi_oid_to As String
    Public ds_edit_item, ds_edit_rule As DataSet

    Private Sub FPriceListCopy_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        pic_en_id_from.Properties.DataSource = dt_bantu
        pic_en_id_from.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        pic_en_id_from.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        pic_en_id_from.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        pic_en_id_to.Properties.DataSource = dt_bantu
        pic_en_id_to.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        pic_en_id_to.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        pic_en_id_to.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity From", "en_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Price List Code From", "pi_code_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Price List Desc. From", "pi_desc_from", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Entity To", "en_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Price List Code To", "pi_code_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Price List Desc. To", "pi_desc_to", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "pic_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "pic_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "pic_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "pic_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  pic_oid, " _
                    & "  pic_dom_id, " _
                    & "  pic_en_id_from, " _
                    & "  pic_add_by, " _
                    & "  pic_add_date, " _
                    & "  pic_upd_by, " _
                    & "  pic_upd_date, " _
                    & "  pic_dt, " _
                    & "  pic_pi_oid_from, " _
                    & "  pic_pi_oid_to, " _
                    & "  pic_en_id_to, " _
                    & "  en_from.en_desc as en_desc_from, " _
                    & "  en_to.en_desc as en_desc_to, " _
                    & "  pi_from.pi_code as pi_code_from, " _
                    & "  pi_from.pi_desc as pi_desc_from, " _
                    & "  pi_to.pi_code as pi_code_to, " _
                    & "  pi_to.pi_desc as pi_desc_to " _
                    & "FROM  " _
                    & "  public.pic_copy " _
                    & "  inner join public.en_mstr en_from on en_from.en_id = pic_en_id_from " _
                    & "  inner join public.en_mstr en_to on en_to.en_id = pic_en_id_to " _
                    & "  inner join public.pi_mstr pi_from on pi_from.pi_oid = pic_pi_oid_from " _
                    & "  inner join public.pi_mstr pi_to on pi_to.pi_oid = pic_pi_oid_to "
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        pic_en_id_from.Focus()
        pic_en_id_from.ItemIndex = 0
        pic_en_id_to.ItemIndex = 0
        pic_pi_oid_from.Text = ""
        pic_pi_oid_to.Text = ""
    End Sub

    Private Sub pic_pi_oid_from_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles pic_pi_oid_from.ButtonClick
        Dim frm As New FPriceListSearch()
        frm.set_win(Me)
        frm._en_id = pic_en_id_from.EditValue
        frm._obj = pic_pi_oid_from
        frm._type = "from"
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub pic_pi_oid_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles pic_pi_oid_to.ButtonClick
        Dim frm As New FPriceListSearch()
        frm.set_win(Me)
        frm._en_id = pic_en_id_to.EditValue
        frm._obj = pic_pi_oid_to
        frm._type = "to"
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub prepare_insert()
        ds_edit_item = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  pid_oid, " _
                        & "  pid_add_by, " _
                        & "  pid_add_date, " _
                        & "  pid_upd_date, " _
                        & "  pid_upd_by, " _
                        & "  pid_pi_oid, " _
                        & "  pid_pt_id, " _
                        & "  pt_code, pt_desc1, pt_desc2, " _
                        & "  pid_dt " _
                        & "FROM  " _
                        & "  public.pid_det " _
                        & " inner join pt_mstr on pt_id = pid_pt_id " _
                        & " inner join pi_mstr on pi_oid = pid_pi_oid " _
                        & "  where pi_oid = '" + _pi_oid_from + "'"

                    .InitializeCommand()
                    .FillDataSet(ds_edit_item, "pid_det")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_edit_rule = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  pidd_oid, " _
                        & "  pidd_add_by, " _
                        & "  pidd_add_date, " _
                        & "  pidd_upd_date, " _
                        & "  pidd_upd_by, " _
                        & "  pidd_pid_oid, " _
                        & "  pidd_payment_type, " _
                        & "  code_name as payment_type_name, " _
                        & "  pidd_price, " _
                        & "  pidd_disc, " _
                        & "  pidd_dp, " _
                        & "  pidd_interval, " _
                        & "  pidd_payment, " _
                        & "  pidd_min_qty, " _
                        & "  pidd_sales_unit, " _
                        & "  pidd_dt " _
                        & "FROM  " _
                        & "  public.pidd_det " _
                        & "  inner join public.code_mstr on code_id = pidd_payment_type " _
                        & "  inner join public.pid_det on pid_oid = pidd_pid_oid " _
                        & "  inner join public.pi_mstr on pi_oid = pid_pi_oid " _
                        & "  where pi_oid = '" + _pi_oid_from + "'"

                    .InitializeCommand()
                    .FillDataSet(ds_edit_rule, "rule")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overrides Function insert() As Boolean
        prepare_insert()

        Dim i, j As Integer
        Dim ssqls As New ArrayList
        Dim _pic_oid As Guid = Guid.NewGuid
        Dim _pid_oid As Guid
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.pic_copy " _
                                            & "( " _
                                            & "  pic_oid, " _
                                            & "  pic_dom_id, " _
                                            & "  pic_en_id_from, " _
                                            & "  pic_add_by, " _
                                            & "  pic_add_date, " _
                                            & "  pic_dt, " _
                                            & "  pic_pi_oid_from, " _
                                            & "  pic_pi_oid_to, " _
                                            & "  pic_en_id_to " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_pic_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetSetring(pic_en_id_from.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & SetSetring(_pi_oid_from) & ",  " _
                                            & SetSetring(_pi_oid_to) & ",  " _
                                            & SetSetring(pic_en_id_to.EditValue) & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit_item.Tables(0).Rows.Count - 1
                            LblStatus.Text = (i + 1) & " of " & ds_edit_item.Tables(0).Rows.Count
                            System.Windows.Forms.Application.DoEvents()
                            _pid_oid = Guid.NewGuid

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.pid_det " _
                                                & "( " _
                                                & "  pid_oid, " _
                                                & "  pid_add_by, " _
                                                & "  pid_add_date, " _
                                                & "  pid_pi_oid, " _
                                                & "  pid_pt_id, " _
                                                & "  pid_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(_pid_oid.ToString) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(_pi_oid_to) & ",  " _
                                                & SetInteger(ds_edit_item.Tables(0).Rows(i).Item("pid_pt_id").ToString) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'Untuk Update data rule
                            For j = 0 To ds_edit_rule.Tables(0).Rows.Count - 1


                                If ds_edit_rule.Tables(0).Rows(j).Item("pidd_pid_oid").ToString = ds_edit_item.Tables(0).Rows(i).Item("pid_oid").ToString Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "INSERT INTO  " _
                                                        & "  public.pidd_det " _
                                                        & "( " _
                                                        & "  pidd_oid, " _
                                                        & "  pidd_add_by, " _
                                                        & "  pidd_add_date, " _
                                                        & "  pidd_pid_oid, " _
                                                        & "  pidd_payment_type, " _
                                                        & "  pidd_price, " _
                                                        & "  pidd_disc, " _
                                                        & "  pidd_dp, " _
                                                        & "  pidd_interval, " _
                                                        & "  pidd_payment, " _
                                                        & "  pidd_min_qty, " _
                                                        & "  pidd_sales_unit, " _
                                                        & "  pidd_dt " _
                                                        & ")  " _
                                                        & "VALUES ( " _
                                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                        & SetSetring(_pid_oid.ToString) & ",  " _
                                                        & SetInteger(ds_edit_rule.Tables(0).Rows(j).Item("pidd_payment_type")) & ",  " _
                                                        & SetDblDB(ds_edit_rule.Tables(0).Rows(j).Item("pidd_price")) & ",  " _
                                                        & SetDblDB(ds_edit_rule.Tables(0).Rows(j).Item("pidd_disc")) & ",  " _
                                                        & SetDblDB(ds_edit_rule.Tables(0).Rows(j).Item("pidd_dp")) & ",  " _
                                                        & SetDblDB(0) & ",  " _
                                                        & SetDblDB(ds_edit_rule.Tables(0).Rows(j).Item("pidd_payment")) & ",  " _
                                                        & SetDblDB(ds_edit_rule.Tables(0).Rows(j).Item("pidd_min_qty")) & ",  " _
                                                        & SetDblDB(ds_edit_rule.Tables(0).Rows(j).Item("pidd_sales_unit")) & ",  " _
                                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                        & ")"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                End If
                            Next

                            'If master_new.PGSqlConn.status_sync = True Then
                            '    For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                            '        .Command.CommandType = CommandType.Text
                            '        .Command.CommandText = Data
                            '        .Command.ExecuteNonQuery()
                            '        .Command.Parameters.Clear()
                            '    Next
                            'End If

                            'ssqls.Clear()

                        Next

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
                        set_row(_pic_oid.ToString, "pic_oid")
                        insert = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        insert = False
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
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        Dim ssqls As New ArrayList

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
                            .Command.CommandText = "delete from pic_copy where pic_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pic_oid").ToString + "'"
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
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        Return delete_data
    End Function

End Class
