Imports npgsql
Imports master_new.ModFunction

Public Class FInsentifDS
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    
    Private Sub FInsentifDS_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_periode_bonus_ds())
        'pr_periode.Properties.DataSource = dt_bantu
        'pr_periode.Properties.DisplayMember = dt_bantu.Columns("bds_code").ToString
        'pr_periode.Properties.ValueMember = dt_bantu.Columns("bds_code").ToString
        'pr_periode.ItemIndex = 0

        With pr_periode
            If .Properties.Columns.VisibleCount = 0 Then
                .Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("en_desc", "Entity", 20))
                .Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("bds_start_date", "Start Date", 20, DevExpress.Utils.FormatType.DateTime, "D", True, DevExpress.Utils.HorzAlignment.Near))
                .Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("bds_end_date", "End Date", 20, DevExpress.Utils.FormatType.DateTime, "D", True, DevExpress.Utils.HorzAlignment.Near))
                .Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("bds_end_date2", "End Date 2", 20, DevExpress.Utils.FormatType.DateTime, "D", True, DevExpress.Utils.HorzAlignment.Near))
                .Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("bds_generate", "Generate 1", 20))
                .Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("bds_generate2", "Generate 2", 20))
                .Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("bds_oid", "", 0, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Default))
            End If

            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("bds_start_date").ToString
            .Properties.ValueMember = dt_bantu.Columns("bds_oid").ToString
            If dt_bantu.Rows.Count > 0 Then
                .EditValue = dt_bantu.Rows(0).Item("bds_oid").ToString
            End If

            .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
            .Properties.BestFit()
            .Properties.DropDownRows = 14
            .Properties.PopupWidth = 300
            '.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            '.Properties.DisplayFormat.FormatString = "D"
            '.Properties.TextEditStyle = TextEditStyles.Standard
        End With

        'If pr_periode.GetColumnValue("bds_generate").ToString.ToUpper = "N" Then
        '    sb_generate.Enabled = True
        'Else
        '    sb_generate.Enabled = False
        'End If
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Periode Code", "bds_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Person", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Sales Unit", "bdsd_sales_unit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Faktor Pengali", "bdsd_faktor_pengali", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Insentif", "bdsd_insentif", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Start Date", "bds_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "End Date", "bds_end_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "End Date Periode 2", "bds_end_date2", DevExpress.Utils.HorzAlignment.Center)

        add_column_copy(gv_master, "Sales Unit 2", "bdsd_sales_unit2", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Faktor Pengali 2", "bdsd_faktor_pengali2", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Insentif 2", "bdsd_insentif2", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "Sisa", "sisa", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Remark", "bds_remark", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Generate", "bds_gen_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Generate", "bds_gen_date", DevExpress.Utils.HorzAlignment.Center)

        add_column_copy(gv_master, "User Generate 2", "bds_gen_by2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Generate 2", "bds_gen_date2", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  bdsd_oid, " _
                    & "  bdsd_bds_oid, " _
                    & "  bdsd_ptnr_id, " _
                    & "  bdsd_sales_unit,bdsd_sales_unit2,bdsd_faktor_pengali2, " _
                    & "  bdsd_faktor_pengali, " _
                    & "  bdsd_sales_unit * bdsd_faktor_pengali as bdsd_insentif,bdsd_sales_unit2 * bdsd_faktor_pengali2 as bdsd_insentif2, " _
                    & "  bds_code, " _
                    & "  bds_start_date, " _
                    & "  bds_end_date,bds_end_date2,CASE (coalesce(bdsd_sales_unit2,0) * coalesce(bdsd_faktor_pengali2,0)) WHEN  0  then 0 else (coalesce(bdsd_sales_unit2,0) * coalesce(bdsd_faktor_pengali2,0)) - (bdsd_sales_unit * bdsd_faktor_pengali) END as sisa, " _
                    & "  ptnr_name, " _
                    & "  en_desc, " _
                    & "  bds_gen_by,bds_gen_by2, " _
                    & "  bds_gen_date,bds_gen_date2, " _
                    & "  bdsd_dt " _
                    & "FROM  " _
                    & "  public.bdsd_det " _
                    & "  inner join public.bds_mstr on bds_oid = bdsd_bds_oid " _
                    & "  inner join public.ptnr_mstr on ptnr_id = bdsd_ptnr_id " _
                    & "  inner join public.en_mstr on en_id = bds_en_id" _
                    & "  where bds_code ~~* '" + pr_periode.GetColumnValue("bds_code") + "'"

        Return get_sequel
    End Function

    Private Sub pr_periode_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pr_periode.EditValueChanged
        'If pr_periode.GetColumnValue("bds_generate").ToString.ToUpper = "N" Then
        '    sb_generate.Enabled = True
        'Else
        '    sb_generate.Enabled = False
        'End If
    End Sub

    Private Sub sb_generate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_generate.Click
        If MessageBox.Show("Generate Insentif Direct Selling For This Periode...?", "Generate", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim ds_bantu As New DataSet()
        Dim i As Integer
        Dim _bds_start_date, _bds_end_date As Date
        Dim _bds_generate As String = "Y"

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select bds_start_date, bds_end_date, bds_generate from bds_mstr " + _
                                           " where bds_oid = '" + pr_periode.GetColumnValue("bds_oid").ToString + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _bds_start_date = .DataReader("bds_start_date")
                        _bds_end_date = .DataReader("bds_end_date")
                        _bds_generate = .DataReader("bds_generate")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If _bds_generate = "Y" Then
            MsgBox("This periode is closed")
            Exit Sub
        End If

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select ptnr_id From ptnr_mstr where ptnr_is_member ~~* 'Y' " + _
                           " and ptnr_active ~~* 'Y' "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "ptnr_mstr")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  so_sales_person, " _
                        & "  sum(sod_sales_unit * soshipd_qty_real * -1) as sod_ext_sales_unit  " _
                        & "  FROM  " _
                        & "  public.soshipd_det " _
                        & "  inner join public.soship_mstr on soship_oid = soshipd_soship_oid " _
                        & "  inner join public.sod_det on sod_oid = soshipd_sod_oid " _
                        & "  inner join public.so_mstr on so_oid = sod_so_oid " _
                        & "  where so_type ~~* 'D' " _
                        & "  and soship_date >=  " + SetDate(_bds_start_date) _
                        & "  and soship_date <= " + SetDate(_bds_end_date) _
                        & "  group by so_sales_person "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "sales_unit")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select   " _
                        & "  bdsr_sales_unit_from, " _
                        & "  bdsr_sales_unit_to, " _
                        & "  bdsr_faktor_pengali " _
                        & "  from bdsr_rule " _
                        & "  where  bdsr_start_date <= " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & " and bdsr_end_date >= " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & " "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "faktor_pengali")
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
                        .Command.CommandText = "delete from bdsd_det where bdsd_bds_oid = '" + pr_periode.GetColumnValue("bds_oid").ToString + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_bantu.Tables("ptnr_mstr").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.bdsd_det " _
                                                & "( " _
                                                & "  bdsd_oid, " _
                                                & "  bdsd_bds_oid, " _
                                                & "  bdsd_ptnr_id, " _
                                                & "  bdsd_sales_unit, " _
                                                & "  bdsd_faktor_pengali, " _
                                                & "  bdsd_dt, " _
                                                & "  bds_gen_by, " _
                                                & "  bds_gen_date " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(pr_periode.GetColumnValue("bds_oid").ToString) & ",  " _
                                                & SetSetring(ds_bantu.Tables("ptnr_mstr").Rows(i).Item("ptnr_id")) & ",  " _
                                                & SetDbl(0) & ", " _
                                                & SetDbl(0) & ", " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        'Update Untuk Sales Unit
                        For i = 0 To ds_bantu.Tables("sales_unit").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update bdsd_det set bdsd_sales_unit = " + SetDbl(ds_bantu.Tables("sales_unit").Rows(i).Item("sod_ext_sales_unit")).ToString _
                                                 & " where bdsd_ptnr_id = " + SetInteger(ds_bantu.Tables("sales_unit").Rows(i).Item("so_sales_person")).ToString
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        'Update Untuk Faktor Pengali
                        For i = 0 To ds_bantu.Tables("faktor_pengali").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update bdsd_det set bdsd_faktor_pengali = " + SetDbl(ds_bantu.Tables("faktor_pengali").Rows(i).Item("bdsr_faktor_pengali")).ToString + _
                                                   " where bdsd_sales_unit >= " + SetDbl(ds_bantu.Tables("faktor_pengali").Rows(i).Item("bdsr_sales_unit_from")).ToString + _
                                                   " and bdsd_sales_unit <= " + SetDbl(ds_bantu.Tables("faktor_pengali").Rows(i).Item("bdsr_sales_unit_to")).ToString
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update bds_mstr set bds_generate = 'Y' " _
                                             & " where bds_oid = '" + pr_periode.GetColumnValue("bds_oid").ToString + "'"
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

                        Dim _bds_code As String
                        _bds_code = pr_periode.EditValue
                        load_cb() 'tolong codingnya lagi dibawah atau diatas agar le nnya ada di posisi semula lagi
                        pr_periode.EditValue = _bds_code
                        MessageBox.Show("Generate success...", "Information", MessageBoxButtons.OK)
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

    Private Sub sb_generate2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_generate2.Click
        If MessageBox.Show("Generate Insentif Direct Selling For This Periode...?", "Generate", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim dt As New DataTable
        Dim sSQL As String
        Dim ds_bantu As New DataSet()
        Dim i As Integer
        Dim _bds_start_date, _bds_end_date As Date
        Dim _bds_generate As String = "N"

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select bds_start_date, bds_end_date2, coalesce(bds_generate2,'N') as bds_generate2  from bds_mstr " + _
                                           " where bds_oid = '" + pr_periode.GetColumnValue("bds_oid").ToString + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _bds_start_date = .DataReader("bds_start_date")
                        _bds_end_date = .DataReader("bds_end_date2")
                        _bds_generate = .DataReader("bds_generate2")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If _bds_generate = "Y" Then
            MsgBox("This periode is closed")
            Exit Sub
        End If

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select ptnr_id From ptnr_mstr where ptnr_is_member ~~* 'Y' " + _
                           " and ptnr_active ~~* 'Y' "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "ptnr_mstr")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  so_sales_person, " _
                        & "  sum(sod_sales_unit * soshipd_qty_real * -1) as sod_ext_sales_unit  " _
                        & "  FROM  " _
                        & "  public.soshipd_det " _
                        & "  inner join public.soship_mstr on soship_oid = soshipd_soship_oid " _
                        & "  inner join public.sod_det on sod_oid = soshipd_sod_oid " _
                        & "  inner join public.so_mstr on so_oid = sod_so_oid " _
                        & "  where so_type ~~* 'D' " _
                        & "  and soship_date >=  " + SetDate(_bds_start_date) _
                        & "  and soship_date <= " + SetDate(_bds_end_date) _
                        & "  group by so_sales_person "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "sales_unit")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select   " _
                        & "  bdsr_sales_unit_from, " _
                        & "  bdsr_sales_unit_to, " _
                        & "  bdsr_faktor_pengali " _
                        & "  from bdsr_rule " _
                        & "  where bdsr_start_date <= " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & " and bdsr_end_date >= " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & " "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "faktor_pengali")
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

                        '.Command.CommandType = CommandType.Text
                        '.Command.CommandText = "delete from bdsd_det where bdsd_bds_oid = '" + pr_periode.GetColumnValue("bds_oid").ToString + "'"
                        'ssqls.Add(.Command.CommandText)
                        '.Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        For i = 0 To ds_bantu.Tables("ptnr_mstr").Rows.Count - 1

                            sSQL = "select * from bdsd_det where bdsd_bds_oid='" & pr_periode.GetColumnValue("bds_oid").ToString _
                                & "' and bdsd_ptnr_id=" & SetInteger(ds_bantu.Tables("ptnr_mstr").Rows(i).Item("ptnr_id"))

                            dt = master_new.PGSqlConn.GetTableData(sSQL)
                            If dt.Rows.Count = 0 Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.bdsd_det " _
                                                    & "( " _
                                                    & "  bdsd_oid, " _
                                                    & "  bdsd_bds_oid, " _
                                                    & "  bdsd_ptnr_id, " _
                                                    & "  bdsd_sales_unit2, " _
                                                    & "  bdsd_faktor_pengali2, " _
                                                    & "  bdsd_dt, " _
                                                    & "  bds_gen_by2, " _
                                                    & "  bds_gen_date2 " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                    & SetSetring(pr_periode.GetColumnValue("bds_oid").ToString) & ",  " _
                                                    & SetSetring(ds_bantu.Tables("ptnr_mstr").Rows(i).Item("ptnr_id")) & ",  " _
                                                    & SetDbl(0) & ", " _
                                                    & SetDbl(0) & ", " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                                    & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If

                        Next

                        'Update Untuk Sales Unit
                        For i = 0 To ds_bantu.Tables("sales_unit").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update bdsd_det set bdsd_sales_unit2 = " + SetDbl(ds_bantu.Tables("sales_unit").Rows(i).Item("sod_ext_sales_unit")).ToString _
                                                 & " where bdsd_ptnr_id = " + SetInteger(ds_bantu.Tables("sales_unit").Rows(i).Item("so_sales_person")).ToString _
                                                 & " and bdsd_bds_oid='" & pr_periode.GetColumnValue("bds_oid").ToString & "'"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        'Update Untuk Faktor Pengali
                        For i = 0 To ds_bantu.Tables("faktor_pengali").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update bdsd_det set bdsd_faktor_pengali2 = " + SetDbl(ds_bantu.Tables("faktor_pengali").Rows(i).Item("bdsr_faktor_pengali")).ToString + _
                                                   " where bdsd_sales_unit2 >= " + SetDbl(ds_bantu.Tables("faktor_pengali").Rows(i).Item("bdsr_sales_unit_from")).ToString + _
                                                   " and bdsd_sales_unit2 <= " + SetDbl(ds_bantu.Tables("faktor_pengali").Rows(i).Item("bdsr_sales_unit_to")).ToString _
                                                 & " and bdsd_bds_oid='" & pr_periode.GetColumnValue("bds_oid").ToString & "'"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update bds_mstr set bds_generate2 = 'Y' " _
                                             & " where bds_oid = '" + pr_periode.GetColumnValue("bds_oid").ToString + "'"
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

                        Dim _bds_code As String
                        _bds_code = pr_periode.EditValue
                        load_cb() 'tolong codingnya lagi dibawah atau diatas agar le nnya ada di posisi semula lagi
                        pr_periode.EditValue = _bds_code
                        MessageBox.Show("Generate success...", "Information", MessageBoxButtons.OK)
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
End Class
