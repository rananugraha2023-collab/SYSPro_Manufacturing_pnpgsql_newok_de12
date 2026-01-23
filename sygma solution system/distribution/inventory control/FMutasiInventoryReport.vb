Imports master_new.ModFunction
Imports npgsql

Public Class FMutasiInventoryReport
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection

    Private Sub FMutasiInventoryReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        'AddHandler gv_loc.FocusedRowChanged, AddressOf relation_detail
        'AddHandler gv_loc.ColumnFilterChanged, AddressOf relation_detail

        pr_date_1.EditValue = Today()
        pr_date_2.EditValue = Today()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        pr_entity.Properties.DataSource = dt_bantu
        pr_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        pr_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        pr_entity.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        'add_column_copy(gv_loc, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_loc, "Deskripsi1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_loc, "Deskripsi2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_loc, "Product Line ", "pl_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_loc, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_loc, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_loc, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_loc, "Qty On Hand", "sum_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        ''add_column_copy(gv_loc, "Cost", "pt_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        ''add_column_copy(gv_loc, "Ext Cost", "pt_cost_ext", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        'add_column_copy(gv_serial, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_serial, "Deskripsi1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_serial, "Deskripsi2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_serial, "Product Line ", "pl_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_serial, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_serial, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_serial, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_serial, "Lot / Serial Number", "invh_serial", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_serial, "Qty On Hand", "sum_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            Dim _ds_begining As New DataSet
            Dim _ds_mutasi As New DataSet
            Try
                Using objtrans As New master_new.WDABasepgsql("", "")
                    With objtrans
                        .SQL = "select   " _
                            & " invh_loc_id, " _
                            & " invh_pt_id,  " _
                            & " coalesce(f_sum_qty_inventory_mutasi(invh_loc_id,invh_pt_id," & SetDate(pr_date_1.DateTime.Date) & "),0) as begining_balance " _
                            & " from invh_mstr  " _
                            & " where invh_en_id = " & pr_entity.EditValue.ToString() _
                            & " and invh_date < " & SetDate(pr_date_1.DateTime.Date) _
                            & " group by " _
                            & " invh_loc_id,invh_pt_id "
                        .InitializeCommand()
                        .FillDataSet(_ds_begining, "begining")

                        .SQL = "select    " _
                            & " invh_loc_id,   " _
                            & " invh_pt_id, " _
                            & " invh_desc, " _
                            & " SUM(invh_qty) as invh_qty " _
                            & " from invh_mstr " _
                            & " where invh_en_id = " & SetInteger(pr_entity.EditValue) _
                            & " and invh_date >= " & SetDate(pr_date_1.DateTime.Date) _
                            & " and invh_date <= " & SetDate(pr_date_2.DateTime.Date) _
                            & " group by invh_loc_id,invh_pt_id,invh_desc "
                        .InitializeCommand()
                        .FillDataSet(_ds_mutasi, "mutasi")

                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            _ds_begining.Tables(0).AcceptChanges()
                            _ds_mutasi.Tables(0).AcceptChanges()

                            .Command.CommandType = CommandType.StoredProcedure
                            .Command.CommandText = "tempm_mutasi_delete"
                            .AddParameter("@arg_tempm_usr_id", master_new.ClsVar.sUserID)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            For Each _dr As DataRow In _ds_begining.Tables("begining").Rows
                                .Command.CommandType = CommandType.StoredProcedure
                                .Command.CommandText = "tempm_mutasi_ins_upd"
                                .AddParameter("@arg_tempm_oid", Guid.NewGuid())
                                .AddParameter("@arg_tempm_pt_id", _dr("invh_pt_id"))
                                .AddParameter("@arg_tempm_loc_id", _dr("invh_loc_id"))
                                .AddParameter("@arg_tempm_trans_desc", "0.Beginning Balance")
                                .AddParameter("@arg_tempm_trans_qty", _dr("begining_balance"))
                                .AddParameter("@arg_tempm_usr_id", master_new.ClsVar.sUserID)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next

                            For Each _drm As DataRow In _ds_mutasi.Tables("mutasi").Rows
                                .Command.CommandType = CommandType.StoredProcedure
                                .Command.CommandText = "tempm_mutasi_ins_upd"
                                .AddParameter("@arg_tempm_oid", Guid.NewGuid())
                                .AddParameter("@arg_tempm_pt_id", _drm("invh_pt_id"))
                                .AddParameter("@arg_tempm_loc_id", _drm("invh_loc_id"))
                                .AddParameter("@arg_tempm_trans_desc", _drm("invh_desc"))
                                .AddParameter("@arg_tempm_trans_qty", _drm("invh_qty"))
                                .AddParameter("@arg_tempm_usr_id", master_new.ClsVar.sUserID)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next

                            sqlTran.Commit()
                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            'retrieve data
            ds = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select  " _
                            & " t1.pt_id,  " _
                            & " t1.pt_code,  " _
                            & " t2.loc_desc, " _
                            & " coalesce(t2.tempm_trans_desc,'XX') as tempm_trans_desc, " _
                            & " coalesce(t2.tempm_trans_qty,0) as tempm_trans_qty " _
                            & " from " _
                            & "( " _
                            & " select pt_id,pt_code " _
                            & " from pt_mstr    " _
                            & " where pt_en_id = " & SetInteger(pr_entity.EditValue) _
                            & " order by pt_code asc " _
                            & ") " _
                            & " t1 left outer join " _
                            & "( " _
                            & " select   " _
                            & " tempm_pt_id, " _
                            & " tempm_loc_id,loc_desc, " _
                            & " tempm_trans_desc, " _
                            & " tempm_trans_qty " _
                            & " from tempm_mutasi  " _
                            & " left outer join loc_mstr on loc_id = tempm_loc_id " _
                            & " where tempm_usr_id = " & SetInteger(master_new.ClsVar.sUserID) _
                            & ") " _
                            & " t2 on t1.pt_id = t2.tempm_pt_id " _
                            & " order by t1.pt_code,t2.tempm_trans_desc asc " _
                            & ""
                        .InitializeCommand()
                        .FillDataSet(ds, "mutasi")
                        pgc_mutasi.DataSource = ds
                        pgc_mutasi.DataMember = "mutasi"
                        pgc_mutasi.BestFit()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

End Class
