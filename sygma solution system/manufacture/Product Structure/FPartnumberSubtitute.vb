Imports master_new.PGSqlConn
Imports npgsql
Imports master_new.ModFunction

Public Class FPartnumberSubtitute
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _oid_mstr As String

    Private Sub FAlts_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Product Structure Code", "ps_par", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Product Structure Desc", "ps_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Part Number Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number Desc", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Part Number Code Subtitute", "pt_code_sub", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number Desc Subtitute", "pt_desc1_sub", DevExpress.Utils.HorzAlignment.Default)


        add_column_copy(gv_master, "Qty", "pts_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Description", "pts_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Active", "pts_active", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "pts_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "pts_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "pts_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "pts_upd_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.pts_oid, " _
                & "  a.pts_en_id, " _
                & "  b.en_desc, " _
                & "  a.pts_ps_id, " _
                & "  c.ps_par, " _
                & "  c.ps_desc, " _
                & "  a.pts_pt_id, " _
                & "  d.pt_code, " _
                & "  d.pt_desc1, " _
                & "  a.pts_pt_sub_id, " _
                & "  e.pt_code as pt_code_sub, " _
                & "  e.pt_desc1 as pt_desc1_sub, " _
                & "  a.pts_qty, " _
                & "  a.pts_desc, " _
                & "  a.pts_active, " _
                & "  a.pts_add_by, " _
                & "  a.pts_add_date, " _
                & "  a.pts_upd_by, " _
                & "  a.pts_upd_date " _
                & "FROM " _
                & "  public.pts_mstr a " _
                & "  INNER JOIN public.en_mstr b ON (a.pts_en_id = b.en_id) " _
                & "  LEFT OUTER JOIN public.ps_mstr c ON (a.pts_ps_id = c.ps_id) " _
                & "  INNER JOIN public.pt_mstr d ON (a.pts_pt_id = d.pt_id) " _
                & "  INNER JOIN public.pt_mstr e ON (a.pts_pt_sub_id = e.pt_id) " _
                   & "  and pts_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        pts_en_id.ItemIndex = 0
        pts_active.EditValue = True
        pts_ps_id.Text = ""
        pts_ps_id.Tag = ""
        pts_pt_id.Text = ""
        pts_pt_id.Tag = ""
        pts_pt_sub_id.Text = ""
        pts_pt_sub_id.Tag = ""
        pts_qty.EditValue = ""
        pts_desc.EditValue = ""
        pts_en_id.Focus()
    End Sub

    Public Overrides Function insert() As Boolean

        Try
            _oid_mstr = Guid.NewGuid.ToString
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
                                            & SetSetring(_oid_mstr) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetSetring(pts_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & SetDate(CekTanggal) & ",  " _
                                            & SetInteger(pts_ps_id.Tag) & ",  " _
                                            & SetInteger(pts_pt_id.Tag) & ",  " _
                                            & SetInteger(pts_pt_sub_id.Tag) & ",  " _
                                            & SetDec(pts_qty.EditValue) & ",  " _
                                            & SetSetring(pts_desc.Text) & ",  " _
                                            & SetBitYN(pts_active.EditValue) & ",  " _
                                            & SetDate(CekTanggal) & "  " _
                                            & ")"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        after_success()
                        set_row(Trim(_oid_mstr), "pts_oid")
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
                _oid_mstr = .Item("alts_oid").ToString
                pts_en_id.EditValue = .Item("alts_en_id")

                pts_desc.Text = SetString(.Item("alts_ref"))
                pts_active.EditValue = SetBitYNB(.Item("alts_active"))

                pts_en_id.EditValue = .Item("pts_en_id")
                pts_active.EditValue = SetBitYNB(.Item("pts_active"))
                pts_ps_id.Text = SetString(.Item("ps_par"))
                pts_ps_id.Tag = SetString(.Item("pts_ps_id"))
                pts_pt_id.Text = .Item("pt_code")
                pts_pt_id.Tag = .Item("pts_pt_id")
                pts_pt_sub_id.Text = .Item("pt_code_sub")
                pts_pt_sub_id.Tag = .Item("pts_pt_sub_id")
                pts_qty.EditValue = SetNumber(.Item("pts_qty"))
                pts_desc.EditValue = SetString(.Item("pts_desc"))
                pts_en_id.Focus()

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
                                        & "  pts_dom_id = " & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                        & "  pts_en_id = " & SetSetring(pts_en_id.Text) & ",  " _
                                        & "  pts_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "  pts_upd_date = " & SetDate(CekTanggal) & ",  " _
                                        & "  pts_ps_id = " & SetInteger(pts_ps_id.Tag) & ",  " _
                                        & "  pts_pt_id = " & SetInteger(pts_pt_id.Tag) & ",  " _
                                        & "  pts_pt_sub_id = " & SetInteger(pts_pt_sub_id.Tag) & ",  " _
                                        & "  pts_qty = " & SetDec(pts_qty.EditValue) & ",  " _
                                        & "  pts_desc = " & SetSetring(pts_desc.Text) & ",  " _
                                        & "  pts_active = " & SetBitYN(pts_active.EditValue) & ",  " _
                                        & "  pts_dt = " & SetDate(CekTanggal) & "  " _
                                        & "  " _
                                        & "WHERE  " _
                                        & "  pts_oid = " & SetSetring(_oid_mstr) & " "


                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        after_success()
                        set_row(Trim(_oid_mstr.ToString), "pts_oid")
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

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Akan Menghapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
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
                            .Command.CommandText = "delete from pts_mstr where ptss_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pts_oid").ToString + "'"
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

    Private Sub pts_ps_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles pts_ps_id.ButtonClick
        Dim frm As New FProdStrucSearch()
        frm.set_win(Me)
        frm._en_id = pts_en_id.EditValue
        frm._obj = pts_ps_id
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub pts_pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles pts_pt_id.ButtonClick
        Dim frm As New FPartNumberSearch()
        frm.set_win(Me)
        frm._en_id = pts_en_id.EditValue
        frm._obj = pts_pt_id
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub pts_pt_sub_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles pts_pt_sub_id.ButtonClick
        Dim frm As New FPartNumberSearch()
        frm.set_win(Me)
        frm._en_id = pts_en_id.EditValue
        frm._obj = pts_pt_sub_id
        frm.type_form = True
        frm.ShowDialog()
    End Sub
End Class
