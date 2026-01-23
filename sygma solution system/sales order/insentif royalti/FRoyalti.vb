Imports npgsql
Imports master_new.ModFunction

Public Class FRoyalti
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FRoyalti_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        Try
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_periode_royalti())
            pr_periode.Properties.DataSource = dt_bantu
            pr_periode.Properties.DisplayMember = dt_bantu.Columns("rms_code").ToString
            pr_periode.Properties.ValueMember = dt_bantu.Columns("rms_code").ToString
            pr_periode.ItemIndex = 0

            If pr_periode.GetColumnValue("rms_generate").ToString.ToUpper = "N" Then
                sb_generate.Enabled = True
            Else
                sb_generate.Enabled = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
       


    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Periode", "rms_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Periode Awal", "rms_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Periode Akhir", "rms_end_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Shipment Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Shipment Date", "soship_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "SO Date", "so_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Deskripsi1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Deskripsi2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Price", "rmsd_pt_price", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Prosentase", "rmsd_prosentase", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Qty", "rmsd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Price * Prosentase", "rmsd_nilai", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Total Line", "rmsd_nilai_ttl_line", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        'add_column_copy(gv_serial, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_serial, "Deskripsi1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_serial, "Deskripsi2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_serial, "Product Line ", "pl_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_serial, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_serial, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_serial, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_serial, "Lot / Serial Number", "invc_serial", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_serial, "Qty On Hand", "invc_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_serial, "Cost", "pt_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_serial, "Ext Cost", "pt_cost_ext", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        .SQL = "SELECT  " _
                            & "  rmsd_oid, " _
                            & "  rmsd_rms_oid, " _
                            & "  rmsd_soshipd_oid, " _
                            & "  rmsd_ptnr_id, " _
                            & "  rmsd_pt_id, " _
                            & "  rmsd_pt_price, " _
                            & "  rmsd_prosentase, " _
                            & "  rmsd_qty, " _
                            & "  rmsd_pt_price * rmsd_prosentase as rmsd_nilai, " _
                            & "  rmsd_pt_price * rmsd_prosentase * rmsd_qty as rmsd_nilai_ttl_line, " _
                            & "  rmsd_dt, " _
                            & "  rms_code, " _
                            & "  rms_start_date, " _
                            & "  rms_end_date, " _
                            & "  soship_code, " _
                            & "  soship_date, " _
                            & "  so_code, " _
                            & "  so_date, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  um_master.code_name as um_name, " _
                            & "  en_desc " _
                            & "FROM  " _
                            & "  public.rmsd_det " _
                            & "  inner join rms_mstr on rms_oid = rmsd_rms_oid " _
                            & "  inner join en_mstr on en_id = rms_en_id " _
                            & "  inner join soshipd_det on soshipd_oid = rmsd_soshipd_oid " _
                            & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                            & "  inner join so_mstr on so_oid = soship_so_oid " _
                            & "  inner join ptnr_mstr on ptnr_id = rmsd_ptnr_id " _
                            & "  inner join pt_mstr on pt_id = rmsd_pt_id " _
                            & "  inner join code_mstr um_master on um_master.code_id = pt_um " _
                            & "  where rms_oid = '" + pr_periode.GetColumnValue("rms_oid").ToString + "'"

                        .InitializeCommand()
                        .FillDataSet(ds, "detail")
                        gc_detail.DataSource = ds.Tables("detail")

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

    Private Sub sb_generate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_generate.Click
        If MessageBox.Show("Generate Royalti For This Periode..?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim ds_bantu As New DataSet
        Dim ds_ship As New DataSet
        Dim ds_retur As New DataSet
        Dim i, j As Integer
        Dim _rms_start_date, _rms_end_date As Date
        Dim ssqls As New ArrayList

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select rms_start_date, rms_end_date from rms_mstr " + _
                                           " where rms_oid = '" + pr_periode.GetColumnValue("rms_oid").ToString + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _rms_start_date = .DataReader("rms_start_date")
                        _rms_end_date = .DataReader("rms_end_date")
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
                    .SQL = "select  " _
                         & " soship_en_id, " _
                         & " soshipd_oid, " _
                         & " pt_writer_id, " _
                         & " pt_id, " _
                         & " sod_price, " _
                         & " soshipd_qty_real * -1 as soshipd_qty_real " _
                         & " from soshipd_det " _
                         & " inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                         & " inner join sod_det on sod_oid = soshipd_sod_oid " _
                         & " inner join pt_mstr on pt_id = sod_pt_id " _
                         & " inner join ptnr_mstr on ptnr_id = pt_writer_id " _
                         & " where soship_date >= '" + _rms_start_date + "'" _
                         & " and soship_date <= '" + _rms_end_date + "'" _
                         & " and soship_en_id = " + pr_periode.GetColumnValue("rms_en_id").ToString _
                         & " and soship_is_shipment = 'Y' "
                    .InitializeCommand()
                    .FillDataSet(ds_ship, "ship")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select  " _
                         & " soship_en_id, " _
                         & " soshipd_oid, " _
                         & " pt_writer_id, " _
                         & " pt_id, " _
                         & " sod_price, " _
                         & " soshipd_qty_real " _
                         & " from soshipd_det " _
                         & " inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                         & " inner join sod_det on sod_oid = soshipd_sod_oid " _
                         & " inner join pt_mstr on pt_id = sod_pt_id " _
                         & " inner join ptnr_mstr on ptnr_id = pt_writer_id " _
                         & " where soship_date >= '" + _rms_start_date + "'" _
                         & " and soship_date <= '" + _rms_end_date + "'" _
                         & " and soship_en_id = " + pr_periode.GetColumnValue("rms_en_id").ToString _
                         & " and soship_is_shipment = 'N' "
                    .InitializeCommand()
                    .FillDataSet(ds_retur, "retur")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim _soshipd_qty_real As Double = 0

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from rmsd_det where rmsd_rms_oid = '" + pr_periode.GetColumnValue("rms_oid").ToString + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'Untuk Perhitungan Dari Pengiriman 
                        For i = 0 To ds_ship.Tables(0).Rows.Count - 1
                            ds_bantu = New DataSet
                            Try
                                Using objcb As New master_new.WDABasepgsql("", "")
                                    With objcb
                                        .SQL = "select  " _
                                             & " royt_oid, " _
                                             & " royt_pt_id, " _
                                             & " royt_seq, " _
                                             & " royt_qty_royalti, " _
                                             & " royt_royalti_amt, " _
                                             & " royt_qty_so, " _
                                             & " royt_qty_royalti - coalesce(royt_qty_so,0) as royt_qty_open " _
                                             & " from royt_table " _
                                             & " where royt_qty_royalti - coalesce(royt_qty_so,0) > 0 " _
                                             & " and royt_en_id = " + pr_periode.GetColumnValue("rms_en_id").ToString _
                                             & " and royt_pt_id = " + ds_ship.Tables(0).Rows(i).Item("pt_id").ToString _
                                             & " order by royt_pt_id, royt_seq"
                                        .InitializeCommand()
                                        .FillDataSet(ds_bantu, "royt_table")
                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                            End Try

                            _soshipd_qty_real = ds_ship.Tables(0).Rows(i).Item("soshipd_qty_real")
                            For j = 0 To ds_bantu.Tables(0).Rows.Count - 1
                                If _soshipd_qty_real > ds_bantu.Tables(0).Rows(j).Item("royt_qty_open") Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update royt_table set royt_qty_so = royt_qty_so + " + ds_bantu.Tables(0).Rows(j).Item("royt_qty_open").ToString _
                                                         & " where royt_oid = '" + ds_bantu.Tables(0).Rows(j).Item("royt_oid").ToString + "'"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                    _soshipd_qty_real = _soshipd_qty_real - ds_bantu.Tables(0).Rows(j).Item("royt_qty_open")

                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "INSERT INTO  " _
                                                         & "  public.rmsd_det " _
                                                         & "( " _
                                                         & "  rmsd_oid, " _
                                                         & "  rmsd_rms_oid, " _
                                                         & "  rmsd_soshipd_oid, " _
                                                         & "  rmsd_ptnr_id, " _
                                                         & "  rmsd_pt_id, " _
                                                         & "  rmsd_pt_price, " _
                                                         & "  rmsd_prosentase, " _
                                                         & "  rmsd_qty, " _
                                                         & "  rmsd_dt " _
                                                         & ")  " _
                                                         & "VALUES ( " _
                                                         & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                         & SetSetring(pr_periode.GetColumnValue("rms_oid").ToString) & ",  " _
                                                         & SetSetring(ds_ship.Tables(0).Rows(i).Item("soshipd_oid").ToString) & ",  " _
                                                         & SetInteger(ds_ship.Tables(0).Rows(i).Item("pt_writer_id")) & ",  " _
                                                         & SetInteger(ds_ship.Tables(0).Rows(i).Item("pt_id")) & ",  " _
                                                         & SetDbl(ds_ship.Tables(0).Rows(i).Item("sod_price")) & ",  " _
                                                         & SetDbl(ds_bantu.Tables(0).Rows(j).Item("royt_royalti_amt")) & ",  " _
                                                         & SetDbl(ds_bantu.Tables(0).Rows(j).Item("royt_qty_open")) & ",  " _
                                                         & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                         & ")"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Else
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update royt_table set royt_qty_so = royt_qty_so + " + _soshipd_qty_real.ToString _
                                                         & " where royt_oid = '" + ds_bantu.Tables(0).Rows(j).Item("royt_oid").ToString + "'"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "INSERT INTO  " _
                                                         & "  public.rmsd_det " _
                                                         & "( " _
                                                         & "  rmsd_oid, " _
                                                         & "  rmsd_rms_oid, " _
                                                         & "  rmsd_soshipd_oid, " _
                                                         & "  rmsd_ptnr_id, " _
                                                         & "  rmsd_pt_id, " _
                                                         & "  rmsd_pt_price, " _
                                                         & "  rmsd_prosentase, " _
                                                         & "  rmsd_qty, " _
                                                         & "  rmsd_dt " _
                                                         & ")  " _
                                                         & "VALUES ( " _
                                                         & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                         & SetSetring(pr_periode.GetColumnValue("rms_oid").ToString) & ",  " _
                                                         & SetSetring(ds_ship.Tables(0).Rows(i).Item("soshipd_oid").ToString) & ",  " _
                                                         & SetInteger(ds_ship.Tables(0).Rows(i).Item("pt_writer_id")) & ",  " _
                                                         & SetInteger(ds_ship.Tables(0).Rows(i).Item("pt_id")) & ",  " _
                                                         & SetDbl(ds_ship.Tables(0).Rows(i).Item("sod_price")) & ",  " _
                                                         & SetDbl(ds_bantu.Tables(0).Rows(j).Item("royt_royalti_amt")) & ",  " _
                                                         & SetDbl(_soshipd_qty_real) & ",  " _
                                                         & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                         & ")"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                    Exit For 'karena nilai _shosipd_qty_real sudah habis...
                                End If
                            Next
                        Next

                        'Untuk Perhitungan Dari Retur
                        For i = 0 To ds_retur.Tables(0).Rows.Count - 1
                            ds_bantu = New DataSet
                            Try
                                Using objcb As New master_new.WDABasepgsql("", "")
                                    With objcb
                                        .SQL = "select  " _
                                             & " royt_oid, " _
                                             & " royt_pt_id, " _
                                             & " royt_seq, " _
                                             & " royt_qty_royalti, " _
                                             & " royt_royalti_amt, " _
                                             & " royt_qty_so, " _
                                             & " royt_qty_royalti - coalesce(royt_qty_so,0) as royt_qty_open " _
                                             & " from royt_table " _
                                             & " where royt_qty_royalti - coalesce(royt_qty_so,0) > 0 " _
                                             & " and royt_en_id = " + pr_periode.GetColumnValue("rms_en_id").ToString _
                                             & " and royt_pt_id = " + ds_retur.Tables(0).Rows(i).Item("pt_id").ToString _
                                             & " order by royt_seq desc"
                                        .InitializeCommand()
                                        .FillDataSet(ds_bantu, "royt_table")
                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                            End Try

                            _soshipd_qty_real = ds_retur.Tables(0).Rows(i).Item("soshipd_qty_real")
                            For j = 0 To ds_bantu.Tables(0).Rows.Count - 1
                                If _soshipd_qty_real > ds_bantu.Tables(0).Rows(j).Item("royt_qty_open") Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update royt_table set royt_qty_so = royt_qty_so - " + ds_bantu.Tables(0).Rows(j).Item("royt_qty_open").ToString _
                                                         & " where royt_oid = '" + ds_bantu.Tables(0).Rows(j).Item("royt_oid").ToString + "'"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                    _soshipd_qty_real = _soshipd_qty_real - ds_bantu.Tables(0).Rows(j).Item("royt_qty_open")

                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "INSERT INTO  " _
                                                         & "  public.rmsd_det " _
                                                         & "( " _
                                                         & "  rmsd_oid, " _
                                                         & "  rmsd_rms_oid, " _
                                                         & "  rmsd_soshipd_oid, " _
                                                         & "  rmsd_ptnr_id, " _
                                                         & "  rmsd_pt_id, " _
                                                         & "  rmsd_pt_price, " _
                                                         & "  rmsd_prosentase, " _
                                                         & "  rmsd_qty, " _
                                                         & "  rmsd_dt " _
                                                         & ")  " _
                                                         & "VALUES ( " _
                                                         & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                         & SetSetring(pr_periode.GetColumnValue("rms_oid").ToString) & ",  " _
                                                         & SetSetring(ds_retur.Tables(0).Rows(i).Item("soshipd_oid").ToString) & ",  " _
                                                         & SetInteger(ds_retur.Tables(0).Rows(i).Item("pt_writer_id")) & ",  " _
                                                         & SetInteger(ds_retur.Tables(0).Rows(i).Item("pt_id")) & ",  " _
                                                         & SetDbl(ds_retur.Tables(0).Rows(i).Item("sod_price")) & ",  " _
                                                         & SetDbl(ds_bantu.Tables(0).Rows(j).Item("royt_royalti_amt")) & ",  " _
                                                         & SetDbl(ds_bantu.Tables(0).Rows(j).Item("royt_qty_open") * -1) & ",  " _
                                                         & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                         & ")"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Else
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update royt_table set royt_qty_so = royt_qty_so - " + _soshipd_qty_real.ToString _
                                                         & " where royt_oid = '" + ds_bantu.Tables(0).Rows(j).Item("royt_oid").ToString + "'"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "INSERT INTO  " _
                                                         & "  public.rmsd_det " _
                                                         & "( " _
                                                         & "  rmsd_oid, " _
                                                         & "  rmsd_rms_oid, " _
                                                         & "  rmsd_soshipd_oid, " _
                                                         & "  rmsd_ptnr_id, " _
                                                         & "  rmsd_pt_id, " _
                                                         & "  rmsd_pt_price, " _
                                                         & "  rmsd_prosentase, " _
                                                         & "  rmsd_qty, " _
                                                         & "  rmsd_dt " _
                                                         & ")  " _
                                                         & "VALUES ( " _
                                                         & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                         & SetSetring(pr_periode.GetColumnValue("rms_oid").ToString) & ",  " _
                                                         & SetSetring(ds_retur.Tables(0).Rows(i).Item("soshipd_oid").ToString) & ",  " _
                                                         & SetInteger(ds_retur.Tables(0).Rows(i).Item("pt_writer_id")) & ",  " _
                                                         & SetInteger(ds_retur.Tables(0).Rows(i).Item("pt_id")) & ",  " _
                                                         & SetDbl(ds_retur.Tables(0).Rows(i).Item("sod_price")) & ",  " _
                                                         & SetDbl(ds_bantu.Tables(0).Rows(j).Item("royt_royalti_amt")) & ",  " _
                                                         & SetDbl(_soshipd_qty_real * -1) & ",  " _
                                                         & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                         & ")"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                    Exit For 'karena nilai _shosipd_qty_real sudah habis...
                                End If
                            Next
                        Next

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update rms_mstr set rms_generate = 'Y' " _
                                             & " where rms_oid = '" + pr_periode.GetColumnValue("rms_oid").ToString + "'"
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

                        Dim _rms_code As String
                        _rms_code = pr_periode.EditValue
                        load_cb() 'tolong codingnya lagi dibawah atau diatas agar le nnya ada di posisi semula lagi
                        pr_periode.EditValue = _rms_code
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
End Class
