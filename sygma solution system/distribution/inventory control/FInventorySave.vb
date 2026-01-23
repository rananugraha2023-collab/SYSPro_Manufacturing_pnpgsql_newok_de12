Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports npgsql

Public Class FInventorySave
    Dim dt_bantu As DataTable
    Dim dt_detail As New DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public _sql As String
    Public _par_item As String

    Private Sub FInventoryReportDetailLog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = CekTanggal()
        pr_txttglakhir.DateTime = CekTanggal()
        AddHandler gv_loc.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv_loc.ColumnFilterChanged, AddressOf relation_detail
    End Sub

    Public Overrides Sub load_cb()
     
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_loc, "Number", "invcs_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Date", "invcs_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_loc, "Add By", "invcs_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Add Date", "invcs_add_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "G")

        add_column_copy(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Deskripsi1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Deskripsi2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_detail, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Lot Number", "invcsd_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty On Hand", "invcsd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail, "Qty On Old", "invcsd_qty_old", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail, "Unit Measure", "um_desc", DevExpress.Utils.HorzAlignment.Default)

        gv_detail.Columns("en_desc").Width = 100
        gv_detail.Columns("pt_code").Width = 100
        gv_detail.Columns("pt_desc1").Width = 200
        gv_detail.Columns("pt_desc2").Width = 150
        gv_detail.Columns("si_desc").Width = 100
        gv_detail.Columns("loc_desc").Width = 200
        gv_detail.Columns("invcsd_serial").Width = 100
        gv_detail.Columns("invcsd_qty").Width = 100
        gv_detail.Columns("invcsd_qty_old").Width = 100
        gv_detail.Columns("um_desc").Width = 100

        gv_detail.OptionsView.ShowAutoFilterRow = True

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
                            & " false as pilih, a.invcs_oid, " _
                            & "  a.invcs_date, " _
                            & "  a.invcs_add_by, " _
                            & "  a.invcs_add_date, " _
                            & "  a.invcs_code " _
                            & "FROM " _
                            & "  public.invcs_mstr a " _
                            & "WHERE " _
                            & "  a.invcs_date BETWEEN " & SetDateNTime00(pr_txttglawal.DateTime) & " AND " & SetDateNTime99(pr_txttglakhir.DateTime) & " " _
                            & "ORDER BY " _
                            & "  a.invcs_code"


                        .InitializeCommand()
                        .FillDataSet(ds, "inv_master")
                        gc_loc.DataSource = ds.Tables("inv_master")
                        'relation_detail()
                        bestfit_column()
                        ConditionsAdjustment()
                        gv_detail.OptionsView.ShowAutoFilterRow = True
                        gv_detail.OptionsView.ShowFooter = True
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

    Public Overrides Sub relation_detail()
        Try
            If CeDetail.EditValue = True Then
                Exit Sub
            End If
            Dim ssql As String
            ssql = "SELECT  " _
                & "  a.invcsd_oid, " _
                & "  a.invcsd_dom_id, " _
                & "  a.invcsd_en_id, " _
                & "  b.en_desc, " _
                & "  a.invcsd_si_id, " _
                & "  e.si_desc, " _
                & "  a.invcsd_loc_id, " _
                & "  d.loc_desc, " _
                & "  a.invcsd_pt_id, " _
                & "  c.pt_code, " _
                & "  c.pt_desc1, " _
                & "  c.pt_desc2, " _
                & "  f.code_name AS um_desc, " _
                & "  a.invcsd_qty, " _
                & "  a.invcsd_serial, " _
                & "  a.invcsd_qty_old, " _
                & "  a.invcsd_invcs_oid " _
                & "FROM " _
                & "  public.invcsd_det a " _
                & "  INNER JOIN public.en_mstr b ON (a.invcsd_en_id = b.en_id) " _
                & "  INNER JOIN public.si_mstr e ON (a.invcsd_si_id = e.si_id) " _
                & "  INNER JOIN public.loc_mstr d ON (a.invcsd_loc_id = d.loc_id) " _
                & "  INNER JOIN public.pt_mstr c ON (a.invcsd_pt_id = c.pt_id) " _
                & "  INNER JOIN public.code_mstr f ON (c.pt_um = f.code_id) " _
                & "WHERE " _
                & "  a.invcsd_invcs_oid ='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("invcs_oid").ToString & "' " _
                & "ORDER BY " _
                & "  b.en_desc,loc_desc, " _
                & "  c.pt_desc1"

            'dt_detail = GetTableData(ssql)

            gc_detail.DataSource = GetTableData(ssql)
            'gv_detail.BestFitColumns()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub BtEItem_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Try

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Overrides Function delete_data() As Boolean
        Dim ssqls As New ArrayList
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

        ssqls.Clear()


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

                        ''Ini untuk update pod_qty_so dikarenakan ada link antara so dan po yang satu group perusahaan
                        '.Command.CommandType = CommandType.Text
                        '.Command.CommandText = "update pod_det set pod_qty_so = 0 where pod_oid in (select sod_pod_oid from sod_det where sod_so_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid").ToString) + ")"
                        'ssqls.Add(.Command.CommandText)
                        '.Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()
                        ''******************************************************

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from invcs_mstr where invcs_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("invcs_oid").ToString + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        '.Command.CommandType = CommandType.Text
                        '.Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid") + "'"
                        'ssqls.Add(.Command.CommandText)
                        '.Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Delete Inventory save " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("invcs_code"))
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

        Return delete_data
    End Function

    Private Sub BtSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtSave.Click
        Try

            If ask("Are you sure to save inventory this state?", "Confirmation ...", MessageBoxDefaultButton.Button2) = False Then
                Exit Sub
            End If
            If ask("Please check synchronyze data first", "Confirmation ...", MessageBoxDefaultButton.Button2) = False Then
                Exit Sub
            End If

            Dim dt As New DataTable
            Dim sSQL As String
            Dim dt_en As New DataTable
            Dim i, x As Integer
            i = 0
            x = 0
            Dim z As Integer = 0
            dt_en = master_new.PGSqlConn.GetTableData("select en_id,en_desc from en_mstr where en_active='Y' and en_code <> '0' order by en_desc")

            Dim _code As String
            _code = GetNewNumberYM("invcs_mstr", "invcs_code", 5, "INV" & CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)
            Dim _oid As String
            _oid = Guid.NewGuid.ToString

            Dim sSQLs As New ArrayList

            Dim dt_loc As New DataTable


            sSQL = "INSERT INTO  " _
                & "  public.invcs_mstr " _
                & "( " _
                & "  invcs_oid, " _
                & "  invcs_date, " _
                & "  invcs_add_by, " _
                & "  invcs_add_date, " _
                & "  invcs_code " _
                & ")  " _
                & "VALUES ( " _
                & SetSetring(_oid) & ",  " _
                & SetDateNTime(CekTanggal) & ",  " _
                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                & SetDateNTime(CekTanggal) & ",  " _
                & SetSetring(_code) & "  " _
                & ")"
            sSQLs.Add(sSQL)

            For Each dr_en As DataRow In dt_en.Rows
                x = x + 1
                LabelControl3.Text = x & " of " & dt_en.Rows.Count & " " & dr_en("en_desc")
                System.Windows.Forms.Application.DoEvents()

                sSQL = "select distinct invc_loc_id,loc_desc from invc_mstr left outer join loc_mstr on invc_loc_id=loc_id  where invc_en_id=" & SetInteger(dr_en("en_id")) & " order by invc_loc_id"
                dt_loc = GetTableData(sSQL)
                z = 1
                For Each dr_loc As DataRow In dt_loc.Rows

                    sSQL = "SELECT  " _
                       & "  invc_oid, " _
                       & "  invc_dom_id, " _
                       & "  invc_en_id, " _
                       & "  invc_si_id, " _
                       & "  invc_loc_id, " _
                       & "  invc_pt_id, " _
                       & "  invc_serial, " _
                       & "  sum(invc_qty) as invc_qty_sum,sum(invc_qty_old) as invc_qty_old, " _
                       & "  en_desc, " _
                       & "  si_desc, " _
                       & "  loc_desc, " _
                       & "  pt_code, " _
                       & "  pt_desc1, " _
                       & "  pt_desc2, " _
                       & "  pl_desc, " _
                       & "  pt_cost,um_mstr.code_name as um_name " _
                       & "FROM  " _
                       & "  invc_mstr " _
                       & "  inner join en_mstr on en_id = invc_en_id " _
                       & "  inner join si_mstr on si_id = invc_si_id " _
                       & "  inner join loc_mstr on loc_id = invc_loc_id " _
                       & "  inner join pt_mstr on pt_id = invc_pt_id " _
                       & "  inner join code_mstr um_mstr on pt_um = um_mstr.code_id  " _
                       & "  inner join pl_mstr on pt_pl_id = pl_id " _
                       & " WHERE invc_qty <> 0 and invc_en_id=" & SetInteger(dr_en("en_id")) & " and invc_loc_id=" & SetInteger(dr_loc("invc_loc_id")) _
                       & "  group by invc_oid, " _
                       & "  invc_dom_id, " _
                       & "  invc_en_id, " _
                       & "  invc_si_id, " _
                       & "  invc_loc_id, " _
                       & "  invc_pt_id, " _
                       & "  invc_serial, " _
                       & "  en_desc, " _
                       & "  si_desc, " _
                       & "  loc_desc, " _
                       & "  pt_code, " _
                       & "  pt_desc1, " _
                       & "  pt_desc2, " _
                       & "  pl_desc, " _
                       & "  pt_cost, um_mstr.code_name "

                    dt = GetTableData(sSQL)


                    i = 0
                    For Each dr As DataRow In dt.Rows
                        sSQL = "INSERT INTO  " _
                            & "  public.invcsd_det " _
                            & "( " _
                            & "  invcsd_oid, " _
                            & "  invcsd_dom_id, " _
                            & "  invcsd_en_id, " _
                            & "  invcsd_si_id, " _
                            & "  invcsd_loc_id, " _
                            & "  invcsd_pt_id, " _
                            & "  invcsd_qty, " _
                            & "  invcsd_serial, " _
                            & "  invcsd_qty_old, " _
                            & "  invcsd_invcs_oid " _
                            & ")  " _
                            & "VALUES ( " _
                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                            & SetInteger(dr("invc_en_id")) & ",  " _
                            & SetInteger(dr("invc_si_id")) & ",  " _
                            & SetInteger(dr("invc_loc_id")) & ",  " _
                            & SetInteger(dr("invc_pt_id")) & ",  " _
                            & SetDec(dr("invc_qty_sum")) & ",  " _
                            & SetSetring(dr("invc_serial")) & ",  " _
                            & SetDec(dr("invc_qty_old")) & ",  " _
                            & SetSetring(_oid) & "  " _
                            & ")"
                        sSQLs.Add(sSQL)

                        i = i + 1
                        LabelControl3.Text = x & " of " & dt_en.Rows.Count & "," & z & " of " & dt_loc.Rows.Count & ", " & i & " of " & dt.Rows.Count & " " & dr_en("en_desc") & " " & dr_loc("loc_desc") & " " & dr("pt_code") & " " & dr("pt_desc1")
                        System.Windows.Forms.Application.DoEvents()
                    Next

                    If master_new.PGSqlConn.status_sync = True Then
                        If DbRunTran(sSQLs, "", master_new.PGSqlConn.FinsertSQL2Array(sSQLs), "") = False Then
                            Exit Sub
                        End If
                        sSQLs.Clear()
                    Else
                        If DbRunTran(sSQLs, "") = False Then
                            Exit Sub
                        End If
                        sSQLs.Clear()
                    End If
                    z = z + 1
                Next

            Next


            Box("Save data success")
            load_data_many(True)
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub BtBackup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtBackup.Click
        Try
            If ask("Are you sure to backup?", "Confirmation ...", MessageBoxDefaultButton.Button2) = False Then
                Exit Sub
            End If

            Dim ssqls As New ArrayList
            Dim ssqls_backup As New ArrayList
            Dim sSQL As String
            Dim dt_detail As New DataTable
            ds.Tables(0).AcceptChanges()
            Dim k As Integer = 0
            For Each dr As DataRow In ds.Tables(0).Rows
                If dr("pilih") = True Then
                    k = k + 1
                End If
            Next
            Dim l As Integer = 1

            For Each dr As DataRow In ds.Tables(0).Rows
                If dr("pilih") = True Then
                    sSQL = "delete from invcs_mstr where invcs_oid = '" + dr("invcs_oid").ToString + "'"
                    ssqls.Add(sSQL)
                    sSQL = "INSERT INTO  " _
                            & "  public.invcs_mstr " _
                            & "( " _
                            & "  invcs_oid, " _
                            & "  invcs_date, " _
                            & "  invcs_add_by, " _
                            & "  invcs_add_date, " _
                            & "  invcs_code " _
                            & ")  " _
                            & "VALUES ( " _
                            & SetSetring(dr("invcs_oid")) & ",  " _
                            & SetDateNTime(dr("invcs_date")) & ",  " _
                            & SetSetring(dr("invcs_add_by")) & ",  " _
                            & SetDateNTime(dr("invcs_add_date")) & ",  " _
                            & SetSetring(dr("invcs_code")) & "  " _
                            & ")"

                    ssqls_backup.Add(sSQL)

                    sSQL = "SELECT  " _
                        & "  invcsd_oid, " _
                        & "  invcsd_dom_id, " _
                        & "  invcsd_en_id, " _
                        & "  invcsd_si_id, " _
                        & "  invcsd_loc_id, " _
                        & "  invcsd_pt_id, " _
                        & "  invcsd_qty, " _
                        & "  invcsd_serial, " _
                        & "  invcsd_qty_old, " _
                        & "  invcsd_invcs_oid " _
                        & "FROM  " _
                        & "  public.invcsd_det  " _
                        & "  where invcsd_invcs_oid='" & dr("invcs_oid").ToString & "'"


                    dt_detail = GetTableData(sSQL)
                    Dim x As Integer = 1
                    Dim j As Integer = dt_detail.Rows.Count
                    For Each dr_det As DataRow In dt_detail.Rows
                        sSQL = "INSERT INTO  " _
                         & "  public.invcsd_det " _
                         & "( " _
                         & "  invcsd_oid, " _
                         & "  invcsd_dom_id, " _
                         & "  invcsd_en_id, " _
                         & "  invcsd_si_id, " _
                         & "  invcsd_loc_id, " _
                         & "  invcsd_pt_id, " _
                         & "  invcsd_qty, " _
                         & "  invcsd_serial, " _
                         & "  invcsd_qty_old, " _
                         & "  invcsd_invcs_oid " _
                         & ")  " _
                         & "VALUES ( " _
                         & SetSetring(dr_det("invcsd_oid")) & ",  " _
                         & SetInteger(dr_det("invcsd_dom_id")) & ",  " _
                         & SetInteger(dr_det("invcsd_en_id")) & ",  " _
                         & SetInteger(dr_det("invcsd_si_id")) & ",  " _
                         & SetInteger(dr_det("invcsd_loc_id")) & ",  " _
                         & SetInteger(dr_det("invcsd_pt_id")) & ",  " _
                         & SetDec(dr_det("invcsd_qty")) & ",  " _
                         & SetSetring(dr_det("invcsd_serial")) & ",  " _
                         & SetDec(dr_det("invcsd_qty_old")) & ",  " _
                         & SetSetring(dr_det("invcsd_invcs_oid")) & "  " _
                         & ")"

                        ssqls_backup.Add(sSQL)

                        LabelControl3.Text = l & " of " & k & ", " & x & " of " & j
                        Windows.Forms.Application.DoEvents()
                        x = x + 1
                    Next
                    l = l + 1
                End If
            Next
            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "", ssqls_backup, "SVR1") = False Then
                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "", ssqls_backup, "SVR1") = False Then
                    Exit Sub
                End If
                ssqls.Clear()
            End If
            Box("Backup data success")
            load_data_many(True)
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class



