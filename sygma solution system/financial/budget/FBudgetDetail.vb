Imports master_new.ModFunction

Public Class FBudgetDetail
    Dim _ds_edit As New DataSet
    Dim _is_insert As Boolean
    Dim dt_bantu As New DataTable
    Public _bdgt_oid, _trans_id, _active As String
    Public _cc_id, _en_id, _year As Integer

    Private Sub FBudgetDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'pr_yearh.Value = Year(Today())
        _is_insert = True
    End Sub

    Public Sub load_lookup()
        dt_bantu = New DataTable
        dt_bantu = load_account()
        bdgtd_ac_id.Properties.DataSource = dt_bantu
        bdgtd_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        bdgtd_ac_id.Properties.ValueMember = dt_bantu.Columns("cca_ac_id").ToString
        bdgtd_ac_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = load_periode()
        bdgtd_bdgtp_id.Properties.DataSource = dt_bantu
        bdgtd_bdgtp_id.Properties.DisplayMember = dt_bantu.Columns("bdgtp_code").ToString
        bdgtd_bdgtp_id.Properties.ValueMember = dt_bantu.Columns("bdgtp_id").ToString
        bdgtd_bdgtp_id.ItemIndex = 0
    End Sub

    Public Function load_account() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select cca_ac_id,ac_code, ac_name " + _
                           " from cca_acount " + _
                           " inner join ac_mstr on ac_id = cca_ac_id " + _
                           " where ac_active ~~* 'Y'" + _
                           " and cca_cc_id = " + SetInteger(_cc_id) + _
                           " and ac_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                           " and cca_en_id = " + _en_id.ToString() + _
                           " and cca_status = True " + _
                           " order by ac_code "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "ac_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_periode() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    '.SQL = "SELECT  " _
                    '        & "  bdgtp_id, " _
                    '        & "  bdgtp_code, " _
                    '        & "  bdgtp_remarks " _
                    '        & "FROM  " _
                    '        & "  public.bdgtp_periode " _
                    '        & " where CAST(substring(cast(bdgtp_start_date as varchar),1,4) as Integer) = " + SetInteger(pr_yearh.Value) _
                    '        & " and bdgtp_en_id = " + _en_id.ToString() _
                    '        & " order by bdgtp_id asc "
                    .SQL = "SELECT  " _
                            & "  bdgtp_id, " _
                            & "  bdgtp_code, " _
                            & "  bdgtp_year, " _
                            & "  bdgtp_remarks " _
                            & "FROM  " _
                            & "  public.bdgtp_periode " _
                            & " where bdgtp_en_id = " + _en_id.ToString() _
                            & " and bdgtp_year = '" + _year.ToString + "'" _
                            & " order by bdgtp_id asc "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "ac_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Overrides Sub load_data_many(ByVal par As Boolean)
        _ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "  bdgtd_oid, " _
                            & "  bdgtd_bdgt_oid, " _
                            & "  bdgtd_add_by, " _
                            & "  bdgtd_add_date, " _
                            & "  bdgtd_upd_by, " _
                            & "  bdgtd_upd_date, " _
                            & "  bdgtd_bdgtp_id, " _
                            & "  bdgtp_code,bdgtp_remarks,bdgtp_start_date,bdgtp_end_date, " _
                            & "  bdgtd_ac_id,ac_code,ac_name, " _
                            & "  bdgtd_sb_id, " _
                            & "  bdgtd_budget, " _
                            & "  bdgtd_alokasi, " _
                            & "  bdgtd_realisasi, " _
                            & "  bdgtd_dt " _
                            & "FROM  " _
                            & "  public.bdgtd_det " _
                            & "  inner join bdgt_mstr on bdgt_oid = bdgtd_bdgt_oid " _
                            & "  inner join bdgtp_periode on bdgtp_id = bdgtd_bdgtp_id " _
                            & "  inner join ac_mstr on ac_id = bdgtd_ac_id " _
                            & "  where bdgt_code = " + SetSetring(pr_budget_code.Text) _
                            & "  and bdgt_active = 'Y' " _
                            & " and bdgt_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " order by bdgtd_bdgtp_id asc "
                    .InitializeCommand()
                    .FillDataSet(_ds_edit, "edit")
                    pgc_detail.DataSource = _ds_edit
                    pgc_detail.DataMember = "edit"
                    pgc_detail.BestFit()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub pr_budget_code_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles pr_budget_code.ButtonClick
        Dim frm As New FBudgetSearch
        frm.set_win(Me)
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub btn_add_update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_add_update.Click
        Try
            AddData()
        Catch ex As Exception
        End Try
    End Sub

    Function CekData(ByVal par_bdgt_oid As String, ByVal par_periode_id As Integer, ByVal par_bdgtd_ac_id As Integer) As Boolean
        CekData = False

        Dim _ds_cek As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "  bdgtd_oid, " _
                            & "  bdgtd_bdgt_oid, " _
                            & "  bdgtd_add_by, " _
                            & "  bdgtd_add_date, " _
                            & "  bdgtd_upd_by, " _
                            & "  bdgtd_upd_date, " _
                            & "  bdgtd_bdgtp_id, " _
                            & "  bdgtp_code,bdgtp_remarks,bdgtp_start_date,bdgtp_end_date, " _
                            & "  bdgtd_ac_id,ac_code,ac_name, " _
                            & "  bdgtd_sb_id, " _
                            & "  bdgtd_budget, " _
                            & "  bdgtd_alokasi, " _
                            & "  bdgtd_realisasi, " _
                            & "  bdgtd_dt " _
                            & "FROM  " _
                            & "  public.bdgtd_det " _
                            & "  inner join bdgt_mstr on bdgt_oid = bdgtd_bdgt_oid " _
                            & "  inner join bdgtp_periode on bdgtp_id = bdgtd_bdgtp_id " _
                            & "  inner join ac_mstr on ac_id = bdgtd_ac_id " _
                            & "WHERE  " _
                            & "  bdgtd_bdgt_oid = " & SetSetring(par_bdgt_oid.ToString()) & "  " _
                            & "  and bdgtd_bdgtp_id = " & SetInteger(par_periode_id) & "  " _
                            & "  and bdgtd_ac_id = " & SetInteger(par_bdgtd_ac_id) & "  " _
                            & "  and bdgtd_budget > 0 "
                    .InitializeCommand()
                    .FillDataSet(_ds_cek, "cek")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        For Each _dr As DataRow In _ds_cek.Tables(0).Rows
            CekData = True
        Next

        Return CekData
    End Function

    Public Sub AddData()
        'If bdgtd_bdgtp_id.ItemIndex = 0 Then
        '    MessageBox.Show("Periode Belum Di Tentukan,,!", "Err", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If

        'If bdgtd_ac_id.ItemIndex = 0 Then
        '    MessageBox.Show("Account Belum Di Tentukan,,!", "Err", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If

        If _trans_id <> "D" Then
            MessageBox.Show("Can't Edit Proses Transaction...", "Err", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If _active.ToString.ToUpper <> "Y" Then
            MessageBox.Show("Can't Edit Non Active Budget...", "Err", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If CekData(_bdgt_oid, bdgtd_bdgtp_id.EditValue, bdgtd_ac_id.EditValue) = True Then
            If MessageBox.Show("Budget Untuk Periode : " + SetSetring(bdgtd_bdgtp_id.Text) + " Dengan Code Account : " + SetSetring(bdgtd_ac_id.Text) + " Sudah Di Entry Sebelumnya,,! Apakah Akan Dilakukan Update Data,,?", "Conf", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Exit Sub
            End If
        End If

        'Dim _bdgtd_oid As String
        '_bdgtd_oid = _ds_edit.Tables("edit").Rows(BindingContext(_ds_edit.Tables("edit")).Position).Item("bdgtd_oid").ToString

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "UPDATE  " _
                            & "  public.bdgtd_det   " _
                            & "SET  " _
                            & "  bdgtd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                            & "  bdgtd_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ", " _
                            & "  bdgtd_sb_id = 0, " _
                            & "  bdgtd_budget = " & SetDbl(bdgtd_budget.EditValue) & ",  " _
                            & "  bdgtd_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " _
                            & "  " _
                            & "WHERE  " _
                            & "  bdgtd_bdgt_oid = " & SetSetring(_bdgt_oid.ToString()) & "  " _
                            & "  and bdgtd_bdgtp_id = " & SetInteger(bdgtd_bdgtp_id.EditValue) & "  " _
                            & "  and bdgtd_ac_id = " & SetInteger(bdgtd_ac_id.EditValue) & "  " _
                            & ";"
                    .InitializeCommand()
                    .ExecuteStoredProcedure()
                    load_data_many(True)
                    set_row(_bdgt_oid, "bdgtd_oid")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overridable Sub set_row(ByVal par_code As String, ByVal par_column As String)
        Dim i As Integer
        For i = 0 To _ds_edit.Tables("edit").Rows.Count - 1
            If par_code = _ds_edit.Tables("edit").Rows(i).Item(par_column) Then
                BindingContext(_ds_edit.Tables("edit")).Position = i
                Exit Sub
            End If
        Next
    End Sub
End Class
