Imports npgsql
Imports master_new.ModFunction

Public Class FItemSub
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _pts_oid_mstr As String

    Private Sub FItemSub_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        pts_en_id.Properties.DataSource = dt_bantu
        pts_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        pts_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        pts_en_id.ItemIndex = 0
    End Sub

    Private Sub pts_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pts_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub load_cb_en()
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_ps_subs_mstr(pts_en_id.EditValue))
        'pts_ps_id.Properties.DataSource = dt_bantu
        'pts_ps_id.Properties.DisplayMember = dt_bantu.Columns("ps_desc").ToString
        'pts_ps_id.Properties.DisplayMember = dt_bantu.Columns("ps_par").ToString
        'pts_ps_id.Properties.ValueMember = dt_bantu.Columns("ps_id").ToString
        'pts_ps_id.ItemIndex = 0
    End Sub

    Private Sub pts_ps_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pts_ps_id.EditValueChanged
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_ps_pt_mstr(pts_ps_id.EditValue))
        'pts_pt_id.Properties.DataSource = dt_bantu
        'pts_pt_id.Properties.DisplayMember = dt_bantu.Columns("pt_desc1").ToString
        'pts_pt_id.Properties.DisplayMember = dt_bantu.Columns("pt_code").ToString
        'pts_pt_id.Properties.ValueMember = dt_bantu.Columns("pt_id").ToString
        'pts_pt_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_pt_inv_mstr(pts_en_id.EditValue))
        'pts_pt_sub_id.Properties.DataSource = dt_bantu
        'pts_pt_sub_id.Properties.DisplayMember = dt_bantu.Columns("pt_desc1").ToString
        'pts_pt_sub_id.Properties.DisplayMember = dt_bantu.Columns("pt_code").ToString
        'pts_pt_sub_id.Properties.ValueMember = dt_bantu.Columns("pt_id").ToString
        'pts_pt_sub_id.ItemIndex = 0
    End Sub


    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Product Structure ", "ps_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number Code", "pt_code_or", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Description", "pt_desc1_or", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Substitution Part Code", "pt_code_sub", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Substitution Part Description", "pt_desc1_sub", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Quantity", "pts_qty", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "pts_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "pts_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "pts_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "pts_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "pts_upd_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  en_mstr.en_id, " _
                    & "  en_mstr.en_desc, " _
                    & "  pts_mstr.pts_oid, " _
                    & "  pts_mstr.pts_en_id, " _
                    & "  pts_mstr.pts_add_by, " _
                    & "  pts_mstr.pts_add_date, " _
                    & "  pts_mstr.pts_upd_by, " _
                    & "  pts_mstr.pts_upd_date, " _
                    & "  pts_mstr.pts_ps_id, " _
                    & "  pts_mstr.pts_pt_id, " _
                    & "  pts_mstr.pts_pt_sub_id, " _
                    & "  pts_mstr.pts_qty, " _
                    & "  pts_mstr.pts_desc, " _
                    & "  pts_mstr.pts_active, " _
                    & "  pts_mstr.pts_dt, " _
                    & "  pt_mstr_or.pt_en_id AS pt_en_id_or, " _
                    & "  pt_mstr_or.pt_id AS pt_id_or, " _
                    & "  pt_mstr_or.pt_code AS pt_code_or, " _
                    & "  pt_mstr_or.pt_desc1 AS pt_desc1_or, " _
                    & "  pt_mstr_or.pt_desc2 AS pt_desc2_or, " _
                    & "  pt_mstr_or.pt_um AS pt_um_or, " _
                    & "  pt_mstr_sub.pt_en_id AS pt_en_id_sub, " _
                    & "  pt_mstr_sub.pt_id AS pt_id_sub, " _
                    & "  pt_mstr_sub.pt_code AS pt_code_sub, " _
                    & "  pt_mstr_sub.pt_desc1 AS pt_desc1_sub, " _
                    & "  pt_mstr_sub.pt_desc2 AS pt_desc2_sub, " _
                    & "  pt_mstr_sub.pt_um AS pt_um_sub, " _
                    & "  ps_mstr.ps_id, " _
                    & "  ps_mstr.ps_par, " _
                    & "  ps_mstr.ps_en_id, " _
                    & "  ps_mstr.ps_desc, " _
                    & "  ps_mstr.ps_use_bom, " _
                    & "  ps_mstr.ps_pt_bom_id, " _
                    & "  code_mstr.code_id, " _
                    & "  code_mstr.code_code, " _
                    & "  code_mstr.code_field, " _
                    & "  code_mstr.code_name, " _
                    & "  code_mstr.code_desc, " _
                    & "  pt_mstr_or.pt_desc1 " _
                    & "FROM " _
                    & "  pts_mstr " _
                    & "  INNER JOIN pt_mstr pt_mstr_or ON (pts_mstr.pts_pt_id = pt_mstr_or.pt_id) " _
                    & "  INNER JOIN pt_mstr pt_mstr_sub ON (pts_mstr.pts_pt_sub_id = pt_mstr_sub.pt_id) " _
                    & "  INNER JOIN en_mstr ON (pts_mstr.pts_en_id = en_mstr.en_id) " _
                    & "  INNER JOIN ps_mstr ON (pts_mstr.pts_ps_id = ps_mstr.ps_id) " _
                    & "  INNER JOIN code_mstr ON (pt_mstr_or.pt_um = code_mstr.code_id) " _
                    & "  AND (pt_mstr_sub.pt_um = code_mstr.code_id)"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()  
        pts_en_id.ItemIndex = 0
        pts_ps_id.ItemIndex = 0
        pts_pt_id.ItemIndex = 0
        pts_pt_sub_id.ItemIndex = 0
        pts_qty.Text = ""
        pts_desc.Text = ""
        pts_active.EditValue = True
        pts_en_id.Focus()
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _pts_oid As Guid
        _pts_oid = Guid.NewGuid

        'Dim _pts_id As Integer
        '_pts_id = SetInteger(func_coll.GetID("pts_mstr", pts_en_id.GetColumnValue("en_code"), "pts_ps_id", "pts_en_id", pts_en_id.EditValue.ToString))

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
                                                & "  public.pts_mstr " _
                                                & "( " _
                                                & "  pts_oid, " _
                                                & "  pts_dom_id, " _
                                                & "  pts_en_id, " _
                                                & "  pts_add_by, " _
                                                & "  pts_add_date, " _
                                                & "  pts_ps_id, " _
                                                & "  pts_pt_id, " _
                                                & "  pts_pt_sub_id, " _
                                                & "  pts_qty, " _
                                                & "  pts_desc, " _
                                                & "  pts_active, " _
                                                & "  pts_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(_pts_oid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetSetring(pts_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & " current_timestamp " & ",  " _
                                                & SetSetring(pts_ps_id.EditValue) & ",  " _
                                                & SetSetring(pts_pt_id.EditValue) & ",  " _
                                                & SetSetring(pts_pt_sub_id.EditValue) & ",  " _
                                                & SetInteger(pts_qty.Text) & ",  " _
                                                & SetSetring(pts_desc.Text) & ",  " _
                                                & SetBitYN(pts_active.EditValue) & ",  " _
                                                & " current_timestamp " & "  " _
                                                & ");"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        after_success()
                        'set_row(Trim(_pts_id.ToString), "pts_oid")
                        insert = True
                    Catch ex As nPgSqlException
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
        If MyBase.edit_data = True Then
            pts_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _pts_oid_mstr = .Item("pts_oid").ToString
                pts_en_id.EditValue = .Item("pts_en_id")
                pts_ps_id.EditValue = .Item("pts_ps_id")
                pts_pt_id.EditValue = .Item("pts_pt_id")
                pts_pt_sub_id.EditValue = .Item("pts_pt_sub_id")
                pts_qty.Text = SetIntegerDB(.Item("pts_qty"))
                pts_desc.Text = SetString(.Item("pts_desc"))
                pts_active.EditValue = SetBitYNB(.Item("pts_active"))
            End With

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                                & "  public.pts_mstr   " _
                                                & "SET  " _
                                                & "  pts_en_id = " & SetInteger(pts_en_id.EditValue) & ",  " _
                                                & "  pts_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "  pts_upd_date = current_timestamp,  " _
                                                & "  pts_ps_id = " & SetInteger(pts_ps_id.EditValue) & ",  " _
                                                & "  pts_pt_id = " & SetInteger(pts_pt_id.EditValue) & ",  " _
                                                & "  pts_pt_sub_id = " & SetInteger(pts_pt_sub_id.EditValue) & ",  " _
                                                & "  pts_qty = " & SetInteger(pts_qty.Text) & ",  " _
                                                & "  pts_desc = " & SetSetring(pts_desc.Text) & ",  " _
                                                & "  pts_active = " & SetBitYN(pts_active.EditValue) & ",  " _
                                                & "  pts_dt = current_timestamp  " _
                                                & "  " _
                                                & "WHERE  " _
                                                & "  pts_oid = " & SetSetring(_pts_oid_mstr.ToString) & "  " _
                                                & ";"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        after_success()
                        set_row(Trim(_pts_oid_mstr.ToString), "pts_oid")
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

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " akan mengHapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
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
                            .Command.CommandText = "delete from pts_mstr where pts_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pts_oid").ToString + "'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

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

    Private Sub gc_master_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gc_master.Click

    End Sub

End Class
