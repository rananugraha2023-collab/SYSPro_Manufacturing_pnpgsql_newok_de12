Imports npgsql
Imports master_new.ModFunction

Public Class FRSInsentif
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _min_pct, _max_pct As Double

    Private Sub FRSInsentif_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_periode_bonus_rs())
        pr_periode.Properties.DataSource = dt_bantu
        pr_periode.Properties.DisplayMember = dt_bantu.Columns("brs_code").ToString
        pr_periode.Properties.ValueMember = dt_bantu.Columns("brs_code").ToString
        pr_periode.ItemIndex = 0

        If pr_periode.GetColumnValue("brs_generate").ToString.ToUpper = "N" Then
            sb_generate.Enabled = True
        Else
            sb_generate.Enabled = False
        End If

        'Cari nilai terendah dan tertinggi dari perhitungan insentif...
        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select brsc_min_pct, brsc_max_pct from brsc_cf"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "min_max")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        _min_pct = ds_bantu.Tables("min_max").Rows(0).Item("brsc_min_pct")
        _max_pct = ds_bantu.Tables("min_max").Rows(0).Item("brsc_max_pct")
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Periode Code", "brs_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Branch", "area_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Person", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Jabatan", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Target", "brsd_target", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Pencapaian", "brsd_pencapaian", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Presentase", "brsd_presentase", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_master, "Bobot Insentif", "brsd_bobot_insentive", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Nilai Insentif", "brsd_nilai_insentive", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Selisih", "brsd_selisih", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Bobot Selisih", "brsd_bobot_selisih", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_master, "Tambahan Insentif", "brsd_tambahan_insentive", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Total Insentif", "brsd_total_insentive", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "Start Date", "brs_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "End Date", "brs_end_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Remark", "brs_remark", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Generate", "brs_gen_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Generate", "brs_gen_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & " brsd_oid, " _
                    & " brsd_brs_oid, " _
                    & " brsd_ptnr_id, " _
                    & " brsd_position_id, " _
                    & " brsd_target, " _
                    & " brsd_pencapaian, " _
                    & " brsd_pencapaian / brsd_target as brsd_presentase, " _
                    & " brsd_bobot_insentive, " _
                    & " brsd_nilai_insentive, " _
                    & " case  " _
                    & "	   when brsd_pencapaian - ( " + _max_pct.ToString + " * brsd_target) < 0 then 0 " _
                    & "    when brsd_pencapaian - ( " + _max_pct.ToString + " * brsd_target) > 0 then brsd_pencapaian - ( 1.2 * brsd_target) " _
                    & " end as brsd_selisih, " _
                    & " brsd_bobot_selisih, " _
                    & " brsd_tambahan_insentive, " _
                    & " brsd_nilai_insentive + brsd_tambahan_insentive as brsd_total_insentive, " _
                    & " brsd_dt, " _
                    & " brsd_area_id, " _
                    & " brs_code, " _
                    & " brs_start_date, " _
                    & " brs_end_date, " _
                    & " brs_remark, " _
                    & " brs_gen_by, " _
                    & " brs_gen_date, " _
                    & " code_name, " _
                    & " en_desc, " _
                    & " ptnr_name, " _
                    & " code_name, " _
                    & " area_name " _
                    & " FROM  " _
                    & " public.brsd_det " _
                    & " inner join brs_mstr on brs_oid = brsd_brs_oid " _
                    & " inner join en_mstr on en_id = brs_en_id " _
                    & " inner join ptnr_mstr on ptnr_id = brsd_ptnr_id " _
                    & " inner join code_mstr on code_id = brsd_position_id " _
                    & " inner join area_mstr on area_id = brsd_area_id " _
                    & " where brs_code ~~* '" + pr_periode.GetColumnValue("brs_code") + "'" _
                    & " order by area_name, code_usr_1, ptnr_name "

        Return get_sequel
    End Function

    Private Sub pr_periode_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pr_periode.EditValueChanged
        If pr_periode.GetColumnValue("brs_generate").ToString.ToUpper = "N" Then
            sb_generate.Enabled = True
        Else
            sb_generate.Enabled = False
        End If
    End Sub

    Private Sub sb_generate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_generate.Click
        If MessageBox.Show("Generate Insentif Regular Selling For This Periode...?", "Generate", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim ds_bantu As New DataSet()
        Dim i As Integer
        Dim _brs_start_date, _brs_end_date As Date

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select brs_start_date, brs_end_date from brs_mstr " + _
                                           " where brs_oid = '" + pr_periode.GetColumnValue("brs_oid").ToString + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _brs_start_date = .DataReader("brs_start_date")
                        _brs_end_date = .DataReader("brs_end_date")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  brsr_en_id, " _
                        & "  brsr_position_id, " _
                        & "  brsr_start_date, " _
                        & "  brsr_end_date, " _
                        & "  brsr_target_amount, " _
                        & "  brsr_insentive, " _
                        & "  brsr_bobot, " _
                        & "  brsr_dt, " _
                        & "  brsr_area_id, " _
                        & "  code_name, " _
                        & "  area_name, " _
                        & "  ptnr_id, " _
                        & "  emp_fname " _
                        & "FROM  " _
                        & "  public.brsr_rule " _
                        & "  inner join code_mstr on code_id = brsr_position_id " _
                        & "  inner join area_mstr on area_id = brsr_area_id " _
                        & "  inner join emp_mstr on emp_area_id = brsr_area_id " _
                        & "                     and emp_pos_id = brsr_position_id " _
                        & "  inner join ptnr_mstr on ptnr_oid = emp_oid " _
                        & "  where brsr_en_id = " + pr_periode.GetColumnValue("brs_en_id").ToString _
                        & "  and brsr_start_date <= " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & " and brsr_end_date >= " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & " " _
                        & "  and ptnr_active ~~* 'Y' " _
                        & "  order by area_name, code_usr_1 "

                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "rule")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        'Cari nilai terendah dan tertinggi dari perhitungan insentif...
        Dim _min_pct, _max_pct As Double
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select brsc_min_pct, brsc_max_pct from brsc_cf"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "min_max")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        _min_pct = ds_bantu.Tables("min_max").Rows(0).Item("brsc_min_pct")
        _max_pct = ds_bantu.Tables("min_max").Rows(0).Item("brsc_max_pct")

        'Cari Pengiriman Dan Retur Yang Terjadi Di Periode Ini....
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select soship_en_id, ptnr_id, ptnr_name,  " _
                        & " emp_pos_id, pos_mstr.code_name as pos_name, emp_area_id, area_name, " _
                        & " ((sod_price - (sod_price * sod_disc)) + ((sod_price - (sod_price * sod_disc)) * (taxr_rate/100))) * -1 * soshipd_qty_real as sod_total " _
                        & " from soshipd_det " _
                        & " inner join soship_mstr on soshipd_soship_oid = soship_oid " _
                        & " inner join sod_det on sod_oid = soshipd_sod_oid " _
                        & " inner join so_mstr on so_oid = sod_so_oid " _
                        & " inner join ptnr_mstr on ptnr_id = so_sales_person " _
                        & " inner join emp_mstr on ptnr_oid = emp_oid " _
                        & " inner join area_mstr on area_id = emp_area_id " _
                        & " inner join code_mstr pos_mstr on pos_mstr.code_id = emp_pos_id " _
                        & " inner join taxr_mstr on taxr_tax_class = sod_tax_class " _
                        & " inner join code_mstr tax_type_mstr on tax_type_mstr.code_id = taxr_tax_type " _
                        & " where soship_date >= " + SetDate(_brs_start_date) _
                        & " and soship_date <= " + SetDate(_brs_end_date) _
                        & " and so_type ~~* 'R' " _
                        & " and tax_type_mstr.code_name ~~* 'PPN' " _
                        & " and soship_en_id = " + pr_periode.GetColumnValue("brs_en_id").ToString
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "hasil")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        'Cari Struktur Sales Untuk Update Nilai2 Ke Parentnya....
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select  " _
                         & " sls_emp_id,  " _
                         & " emp_mstr_child.emp_fname,  " _
                         & " ptnr_mstr_child.ptnr_id as ptnr_id_child,  " _
                         & " emp_mstr_child.emp_pos_id as emp_pos_id_child,  " _
                         & " emp_mstr_child.emp_area_id as emp_area_id_child, " _
                         & " sls_parent_id,  " _
                         & " ptnr_mstr_parent.ptnr_id as ptnr_id_parent,  " _
                         & " emp_mstr_parent.emp_pos_id as emp_pos_id_parent,  " _
                         & " emp_mstr_parent.emp_area_id as emp_area_id_parent, " _
                         & " code_usr_1 " _
                         & " from sls_strct " _
                         & " inner join emp_mstr emp_mstr_child on emp_mstr_child.emp_id = sls_emp_id " _
                         & " inner join ptnr_mstr ptnr_mstr_child on ptnr_mstr_child.ptnr_oid = emp_mstr_child.emp_oid " _
                         & " inner join emp_mstr emp_mstr_parent on emp_mstr_parent.emp_id = sls_parent_id " _
                         & " inner join ptnr_mstr ptnr_mstr_parent on ptnr_mstr_parent.ptnr_oid = emp_mstr_parent.emp_oid " _
                         & " inner join brsr_rule on brsr_rule.brsr_position_id = emp_mstr_child.emp_pos_id " _
                         & "                     and brsr_rule.brsr_area_id = emp_mstr_child.emp_area_id " _
                         & " inner join code_mstr on code_id = emp_mstr_child.emp_pos_id " _
                         & " where sls_active ~~* 'Y' " _
                         & " and sls_start_date <= " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & " and sls_end_date >= " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & " " _
                         & " order by code_usr_1 desc"
                    'order by code_usr_1 ini sangat penting agar update tidak salah...
                    'dan setingan di form position pun harus benar juga agar update tidak salah juga 
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "prnt_upd")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim ssqls As New ArrayList

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from brsd_det where brsd_brs_oid = '" + pr_periode.GetColumnValue("brs_oid").ToString + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_bantu.Tables("rule").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.brsd_det " _
                                                & "( " _
                                                & "  brsd_oid, " _
                                                & "  brsd_brs_oid, " _
                                                & "  brsd_ptnr_id, " _
                                                & "  brsd_position_id, " _
                                                & "  brsd_target, " _
                                                & "  brsd_pencapaian, " _
                                                & "  brsd_bobot_insentive, " _
                                                & "  brsd_nilai_insentive, " _
                                                & "  brsd_bobot_selisih, " _
                                                & "  brsd_tambahan_insentive, " _
                                                & "  brsd_dt, " _
                                                & "  brsd_area_id " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(pr_periode.GetColumnValue("brs_oid").ToString) & ",  " _
                                                & SetInteger(ds_bantu.Tables(0).Rows(i).Item("ptnr_id")) & ",  " _
                                                & SetInteger(ds_bantu.Tables(0).Rows(i).Item("brsr_position_id")) & ",  " _
                                                & SetDbl(ds_bantu.Tables(0).Rows(i).Item("brsr_target_amount")) & ",  " _
                                                & SetDbl(0) & ",  " _
                                                & SetDbl(ds_bantu.Tables(0).Rows(i).Item("brsr_insentive")) & ",  " _
                                                & SetDbl(0) & ",  " _
                                                & SetDbl(ds_bantu.Tables(0).Rows(i).Item("brsr_bobot")) & ",  " _
                                                & SetDbl(0) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetInteger(ds_bantu.Tables(0).Rows(i).Item("brsr_area_id")) & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        'Update Nilai Pencapaian Dari Sales
                        For i = 0 To ds_bantu.Tables("hasil").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                                & "  public.brsd_det   " _
                                                & "SET  " _
                                                & "  brsd_pencapaian = brsd_pencapaian + " & SetDbl(ds_bantu.Tables("hasil").Rows(i).Item("sod_total")) & ",  " _
                                                & "  brsd_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " _
                                                & "  " _
                                                & " WHERE brsd_brs_oid = " & SetSetring(pr_periode.GetColumnValue("brs_oid").ToString) _
                                                & " and brsd_ptnr_id = " & SetInteger(ds_bantu.Tables("hasil").Rows(i).Item("ptnr_id")) _
                                                & " and brsd_position_id = " & SetInteger(ds_bantu.Tables("hasil").Rows(i).Item("emp_pos_id")) _
                                                & " and brsd_area_id = " & SetInteger(ds_bantu.Tables("hasil").Rows(i).Item("emp_area_id"))
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        'Update Nilai Pencapaian Dari Parent (SPV, BM, NSM)
                        For i = 0 To ds_bantu.Tables("prnt_upd").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                        & " public.brsd_det   " _
                                        & " SET  " _
                                        & " brsd_pencapaian = brsd_pencapaian + (select brsd_pencapaian from brsd_det " _
                                                             & " where brsd_brs_oid = " & SetSetring(pr_periode.GetColumnValue("brs_oid").ToString) _
                                                             & " and brsd_ptnr_id = " & SetInteger(ds_bantu.Tables("prnt_upd").Rows(i).Item("ptnr_id_child")) _
                                                             & " and brsd_position_id = " & SetInteger(ds_bantu.Tables("prnt_upd").Rows(i).Item("emp_pos_id_child")) _
                                                             & " and brsd_area_id = " & SetInteger(ds_bantu.Tables("prnt_upd").Rows(i).Item("emp_area_id_child")) _
                                                             & " ), " _
                                        & " brsd_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " _
                                        & " WHERE brsd_brs_oid = " & SetSetring(pr_periode.GetColumnValue("brs_oid").ToString) _
                                        & " and brsd_ptnr_id = " & SetInteger(ds_bantu.Tables("prnt_upd").Rows(i).Item("ptnr_id_parent")) _
                                        & " and brsd_position_id = " & SetInteger(ds_bantu.Tables("prnt_upd").Rows(i).Item("emp_pos_id_parent")) _
                                        & " and brsd_area_id = " & SetInteger(ds_bantu.Tables("prnt_upd").Rows(i).Item("emp_area_id_parent"))
                            'order by code_usr_1 ini sangat penting agar update tidak salah...
                            'dan setingan di form position pun harus benar juga agar update tidak salah juga 
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        'Update Nilai Insentif Apabila presentase (pencapaian/target) diantara min_pct dan max_pct
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update brsd_det set brsd_nilai_insentive = brsd_bobot_insentive * (brsd_pencapaian/brsd_target)" + _
                                               " where brsd_brs_oid = '" + pr_periode.GetColumnValue("brs_oid").ToString + "'" + _
                                               " and (brsd_pencapaian/brsd_target) >= " + _min_pct.ToString + _
                                               " and (brsd_pencapaian/brsd_target) <= " + _max_pct.ToString
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'Update Nilai Insentif Apabila presentase (pencapaian/target) diantara diatas max_pct
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update brsd_det set brsd_nilai_insentive = brsd_bobot_insentive * " + _max_pct.ToString + _
                                               " where brsd_brs_oid = '" + pr_periode.GetColumnValue("brs_oid").ToString + "'" + _
                                               " and (brsd_pencapaian/brsd_target) >= " + _max_pct.ToString
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'Update Tambahan Insentif Apabila presentase (pencapaian/target) diantara diatas max_pct
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update brsd_det set brsd_tambahan_insentive = (brsd_bobot_selisih * (brsd_pencapaian - (" + _max_pct.ToString + " * brsd_target))) " + _
                                               " where brsd_brs_oid = '" + pr_periode.GetColumnValue("brs_oid").ToString + "'" + _
                                               " and (brsd_pencapaian/brsd_target) >= " + _max_pct.ToString
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update brs_mstr set brs_generate = 'Y', brs_gen_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ", brs_gen_by = " + SetSetring(master_new.ClsVar.sNama) _
                                             & " where brs_oid = '" + pr_periode.GetColumnValue("brs_oid").ToString + "'"
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

                        Dim _brs_code As String
                        _brs_code = pr_periode.EditValue
                        load_cb() 'tolong codingnya lagi dibawah atau diatas agar le nnya ada di posisi semula lagi
                        pr_periode.EditValue = _brs_code
                        MessageBox.Show("Selamat Data Telah Ter-Generate...", "Information", MessageBoxButtons.OK)
                        help_load_data(True)

                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overrides Function delete_data() As Boolean
        MessageBox.Show("Delete Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Function edit_data() As Boolean
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Function insert_data() As Boolean
        MessageBox.Show("Insert Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function
End Class
