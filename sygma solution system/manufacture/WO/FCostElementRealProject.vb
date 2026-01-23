Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FCostElementRealProject
    Dim ssql As String
    Dim _wo_oid_master As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Public __prjdr_prj_oid As String
   
    Public ds_edit As DataSet

    Private Sub FPartnerAll_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
        start_date.DateTime = Now()
        end_date.DateTime = Now
    End Sub

    Public Overrides Sub format_grid()
        'master
        add_column(gv_master, "prjdr_oid", False)
        add_column_copy(gv_master, "Real Project Code", "prjdr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Real Project Date", "prjdr_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Real Project Remarks", "prjdr_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Project Remarks", "prj_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "prjdr_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "prjdr_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "prjdr_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "prjdr_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "prjdrd_oid", False)
        add_column_copy(gv_detail, "Cost Element Code", "cse_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost Element Desc", "cse_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Project Detail Remarks", "prjd_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost", "prjdrd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Real Project Remarks", "prjdrd_remarks", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "prjdrd_oid", False)
        add_column(gv_edit, "prjdrd_prjd_oid", False)
        add_column(gv_edit, "prjd_cse_id", False)
        add_column(gv_edit, "Cost Element Code", "cse_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Cost Element Desc", "cse_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Project Detail Remarks", "prjd_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Cost", "prjdrd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Real Project Remarks", "prjdrd_remarks", DevExpress.Utils.HorzAlignment.Default)


    End Sub
    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        Try
            If ds.Tables(0).Rows.Count = 0 Then
                Exit Sub
            End If

            Try
                ds.Tables("detail").Clear()
            Catch ex As Exception
            End Try
            ssql = "SELECT  " _
                & "  a.prjdrd_oid, " _
                & "  a.prjdrd_prjdr_oid, " _
                & "  a.prjdrd_prjd_oid, " _
                & "  b.prjd_seq, " _
                & "  b.prjd_cse_id, " _
                & "  c.cse_code, " _
                & "  c.cse_desc, " _
                & "  b.prjd_remarks, " _
                & "  a.prjdrd_cost, " _
                & "  a.prjdrd_remarks " _
                & "FROM " _
                & "  public.prjdrd_det a " _
                & "  INNER JOIN public.prjd_det b ON (a.prjdrd_prjd_oid = b.prjd_oid) " _
                & "  INNER JOIN public.cse_mstr c ON (b.prjd_cse_id = c.cse_id) " _
                & "WHERE " _
                & "  a.prjdrd_prjdr_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prjdr_oid").ToString & "' " _
               

            load_data_detail(ssql, gc_detail, "detail")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
            & "  a.prjdr_oid, " _
            & "  a.prjdr_prj_oid, " _
            & "  b.prj_code, " _
            & "  b.prj_remarks, " _
            & "  a.prjdr_add_by, " _
            & "  a.prjdr_add_date, " _
            & "  a.prjdr_upd_by, " _
            & "  a.prjdr_upd_date, " _
            & "  a.prjdr_code, " _
            & "  a.prjdr_date, " _
            & "  a.prjdr_remarks, " _
            & "  a.prjdr_dt " _
            & "FROM " _
            & "  public.prjdr_real a " _
            & "  INNER JOIN public.prj_mstr b ON (a.prjdr_prj_oid = b.prj_oid) " _
            & " Where prjdr_date between " & SetDateNTime00(start_date.DateTime) & " and " & SetDateNTime99(end_date.DateTime) _
            & " ORDER BY prjdr_code"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()

        prjdr_code.Text = ""
        prjdr_date.DateTime = Now
        prjdr_prj_oid.Text = ""
        __prjdr_prj_oid = ""
        prjdr_remarks.Text = ""

        Try
            tcg_header.SelectedTabPageIndex = 0
            dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        Catch ex As Exception
        End Try
    End Sub
    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  a.prjdrd_oid, " _
                        & "  a.prjdrd_prjdr_oid, " _
                        & "  a.prjdrd_prjd_oid, " _
                        & "  b.prjd_seq, " _
                        & "  b.prjd_cse_id, " _
                        & "  c.cse_code, " _
                        & "  c.cse_desc, " _
                        & "  b.prjd_remarks, " _
                        & "  a.prjdrd_cost, " _
                        & "  a.prjdrd_remarks " _
                        & "FROM " _
                        & "  public.prjdrd_det a " _
                        & "  INNER JOIN public.prjd_det b ON (a.prjdrd_prjd_oid = b.prjd_oid) " _
                        & "  INNER JOIN public.cse_mstr c ON (b.prjd_cse_id = c.cse_id) " _
                        & "WHERE " _
                        & "  a.prjdrd_prjdr_oid is null  "

                    .InitializeCommand()
                    .FillDataSet(ds_edit, "edit")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Public Overrides Function insert() As Boolean
        Dim _prj_code As String = ""
        Dim _oid_master As String = Guid.NewGuid.ToString
        Dim ssqls As New ArrayList

        _prj_code = GetNewNumberYM("prj_mstr", "prj_code", 5, "RPJ" & CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)

        prjdr_code.EditValue = _prj_code

        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
        ds_edit.Tables(0).AcceptChanges()

        Try

            ssql = "INSERT INTO  " _
                & "  public.prjdr_real " _
                & "( " _
                & "  prjdr_oid, " _
                & "  prjdr_prj_oid, " _
                & "  prjdr_add_by, " _
                & "  prjdr_add_date, " _
                & "  prjdr_code, " _
                & "  prjdr_date, " _
                & "  prjdr_remarks, " _
                & "  prjdr_dt " _
                & ")  " _
                & "VALUES ( " _
                & SetSetring(_oid_master) & ",  " _
                & SetSetring(__prjdr_prj_oid) & ",  " _
                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                & SetDateNTime(CekTanggal) & ",  " _
                & SetSetring(prjdr_code.Text) & ",  " _
                & SetSetring(prjdr_date.DateTime) & ",  " _
                & SetSetring(prjdr_remarks.Text) & ",  " _
                & SetDateNTime00(CekTanggal) & "  " _
                & ")"

            ssqls.Add(ssql)

            For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
                With ds_edit.Tables(0).Rows(i)
                    ssql = "INSERT INTO  " _
                        & "  public.prjdrd_det " _
                        & "( " _
                        & "  prjdrd_oid, " _
                        & "  prjdrd_prjdr_oid, " _
                        & "  prjdrd_add_by, " _
                        & "  prjdrd_add_date, " _
                        & "  prjdrd_prjd_oid, " _
                        & "  prjdrd_cost, " _
                        & "  prjdrd_remarks, " _
                        & "  prjdrd_dt " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_oid_master) & ",  " _
                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                        & SetDateNTime(CekTanggal) & ",  " _
                        & SetSetring(.Item("prjdrd_prjd_oid")) & ",  " _
                        & SetDec(.Item("prjdrd_cost")) & ",  " _
                        & SetSetring(.Item("prjdrd_remarks")) & ",  " _
                        & SetDateNTime00(CekTanggal) & "  " _
                        & ")"

                    ssqls.Add(ssql)

                    ssql = "update prjd_det set prjd_cost_rea=coalesce(prjd_cost_rea,0)+" & SetDec(.Item("prjdrd_cost")) _
                        & " where prjd_oid=" & SetSetring(.Item("prjdrd_prjd_oid"))

                    ssqls.Add(ssql)

                End With
            Next


            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Return False
                    Exit Function
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Return False
                    Exit Function
                End If
                ssqls.Clear()
            End If


            after_success()
            set_row(_oid_master, "wo_oid")
            dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
            insert = True


        Catch ex As Exception
            row = 0
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function

    Public Overrides Function edit_data() As Boolean

        If MyBase.edit_data = True Then
            'prj_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)

                '_wo_oid_master = .Item("prj_oid")
                'prj_en_id.EditValue = .Item("prj_en_id")
                'prjdr_code.EditValue = .Item("prj_code")
                'prj_si_id.EditValue = .Item("prj_si_id")
                'prj_type_id.EditValue = .Item("prj_type_id")

                'prjdr_date.DateTime = .Item("prj_date_ord")
                'prj_date_end.DateTime = .Item("prj_date_end")
                'prj_date_start.DateTime = .Item("prj_date_start")

                'prjdr_prj_oid.Text = .Item("pt_desc1")
                '__prj_pt_id = .Item("prj_pt_id")
                'prj_ro_id.Text = .Item("ro_desc")
                '__prj_ro_id = .Item("prj_ro_id")
                'prj_bom_id.Text = SetString(.Item("bom_desc"))
                '__prj_bom_id = SetString(.Item("prj_bom_id"))
                'prj_remarks.EditValue = SetString(.Item("prj_remarks"))
                'prj_ptnr_id.Text = .Item("ptnr_name")
                '__prj_ptnr_id = .Item("prj_ptnr_id")
                'prj_cst_id.Text = .Item("cst_desc")
                '__prj_cs_id = .Item("prj_cst_id")

                'prj_cu_id.EditValue = .Item("prj_cu_id")
                'prjdr_remarks.Text = SetString(.Item("prj_po_nbr"))
                'prj_qty.EditValue = .Item("prj_qty")
                'prj_price.EditValue = .Item("prj_price")
                'prj_spc_ord.EditValue = SetBitYNB(.Item("prj_spc_ord"))

            End With

            ds_edit = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                       & "  a.prjd_oid, " _
                       & "  a.prjd_prj_oid, " _
                       & "  a.prjd_seq, " _
                       & "  a.prjd_cse_id, " _
                       & "  b.cse_code, " _
                       & "  b.cse_desc, " _
                       & "  a.prjd_cost_est, " _
                       & "  a.prjd_cost_rea, " _
                       & "  a.prjd_var, " _
                       & "  a.prjd_remarks " _
                       & "FROM " _
                       & "  public.prjd_det a " _
                       & "  INNER JOIN public.cse_mstr b ON (a.prjd_cse_id = b.cse_id) " _
                       & "WHERE " _
                       & "  a.prjd_prj_oid='" & ds.Tables(0).Rows(row).Item("prj_oid").ToString & "' " _
                       & "ORDER BY " _
                       & "  a.prjd_seq"


                        .InitializeCommand()
                        .FillDataSet(ds_edit, "edit")
                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            'ds_edit = ds.Copy
            'gc_edit.DataSource = ds_edit.Tables(0)
            'gv_edit.BestFitColumns()
            Try
                tcg_header.SelectedTabPageIndex = 0
                dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
            Catch ex As Exception
            End Try

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()

        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
        ds_edit.Tables(0).AcceptChanges()

        edit = True
        Dim ssqls As New ArrayList
        Try

            'ssql = "UPDATE  " _
            '    & "  public.prj_mstr   " _
            '    & "SET  " _
            '    & "  prj_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
            '    & "  prj_en_id = " & SetInteger(prj_en_id.EditValue) & ",  " _
            '    & "  prj_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
            '    & "  prj_upd_date = " & SetDateNTime(CekTanggal) & ",  " _
            '    & "  prj_code = " & SetSetring(prj_code.Text) & ",  " _
            '    & "  prj_date_ord = " & SetDateNTime00(prj_date_ord.DateTime) & ",  " _
            '    & "  prj_date_start = " & SetDateNTime00(prj_date_start.DateTime) & ",  " _
            '    & "  prj_date_end = " & SetDateNTime00(prj_date_end.DateTime) & ",  " _
            '    & "  prj_type_id = " & SetInteger(prj_type_id.EditValue) & ",  " _
            '    & "  prj_ptnr_id = " & SetInteger(__prj_ptnr_id) & ",  " _
            '    & "  prj_pt_id = " & SetInteger(__prj_pt_id) & ",  " _
            '    & "  prj_qty = " & SetDec(prj_qty.EditValue) & ",  " _
            '    & "  prj_bom_id = " & SetInteger(__prj_bom_id) & ",  " _
            '    & "  prj_ro_id = " & SetInteger(__prj_ro_id) & ",  " _
            '    & "  prj_po_nbr = " & SetSetring(prj_po_nbr.Text) & ",  " _
            '    & "  prj_si_id = " & SetInteger(prj_si_id.EditValue) & ",  " _
            '    & "  prj_cu_id = " & SetInteger(prj_cu_id.EditValue) & ",  " _
            '    & "  prj_cst_id = " & SetInteger(__prj_cs_id) & ",  " _
            '    & "  prj_price = " & SetDec(prj_price.EditValue) & ",  " _
            '    & "  prj_spc_ord = " & SetBitYN(prj_spc_ord.EditValue) & ",  " _
            '    & "  prj_remarks = " & SetSetring(prj_remarks.Text) & "  " _
            '    & "WHERE  " _
            '    & "  prj_oid = " & SetSetring(_wo_oid_master) & " "

            ssqls.Add(ssql)

            ssql = "delete from prjd_det where prjd_prj_oid=" & SetSetring(_wo_oid_master)
            ssqls.Add(ssql)

            For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
                With ds_edit.Tables(0).Rows(i)
                    ssql = "INSERT INTO  " _
                        & "  public.prjd_det " _
                        & "( " _
                        & "  prjd_oid, " _
                        & "  prjd_prj_oid, " _
                        & "  prjd_add_by, " _
                        & "  prjd_add_date, " _
                        & "  prjd_seq, " _
                        & "  prjd_cse_id, " _
                        & "  prjd_cost_est, " _
                        & "  prjd_cost_rea, " _
                        & "  prjd_var, " _
                        & "  prjd_remarks, " _
                        & "  prjd_dt " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_wo_oid_master) & ",  " _
                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                        & SetDateNTime(CekTanggal) & ",  " _
                        & SetInteger(i) & ",  " _
                        & SetInteger(.Item("prjd_cse_id")) & ",  " _
                        & SetDec(.Item("prjd_cost_est")) & ",  " _
                        & SetDec(.Item("prjd_cost_rea")) & ",  " _
                        & SetSetring(.Item("prjd_var")) & ",  " _
                        & SetSetring(.Item("prjd_remarks")) & ",  " _
                        & SetDateNTime(CekTanggal) & "  " _
                        & ")"
                    ssqls.Add(ssql)
                End With
            Next


            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Return False
                    Exit Function
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Return False
                    Exit Function
                End If
                ssqls.Clear()
            End If


            after_success()
            set_row(Trim(_wo_oid_master.ToString), "prj_oid")
            dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
            edit = True

        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
    End Function

    Public Overrides Function delete_data() As Boolean
        If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_status") = "R" Then
            Return False
            Exit Function
        End If

        delete_data = True
        If ds.Tables.Count = 0 Then
            delete_data = False
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            delete_data = False
            Exit Function
        End If

        Dim ssqls As New ArrayList

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
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
                            .Command.CommandText = "delete from wo_mstr where wo_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid").ToString + "'"
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
                            delete_data = False
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                delete_data = False
            End Try
        End If

        Return delete_data
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True


        If required(prjdr_prj_oid, "Part Number") = False Then
            Return False
            Exit Function
        End If


        Return before_save
    End Function

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub
    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        Try
            browse_data()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle
        ' Dim _ps_en_id As Integer = prj_en_id.EditValue

        'If _col = "cse_code" Or _col = "cse_desc" Then
        '    Dim frm As New FCSESearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    'frm._en_id = _ps_en_id
        '    frm.type_form = True
        '    frm.ShowDialog()
        'End If
    End Sub
    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
        Try
            If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
                browse_data()
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub


   
    Private Sub prjdr_prj_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles prjdr_prj_oid.ButtonClick
        Try

            Dim frm As New FProjectSearch
            frm.set_win(Me)
            frm._obj = prjdr_prj_oid
            'frm._en_id = wo_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
