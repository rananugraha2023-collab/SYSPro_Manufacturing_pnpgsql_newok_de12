Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FSalesPlanB
    Dim ssql As String
    Dim _mstr_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public ds_edit As DataSet

    Private Sub FPartnerAll_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = CekTanggal()
        pr_txttglakhir.DateTime = CekTanggal()
    End Sub

    Public Overrides Sub load_cb()
        init_le(plans_en_id, "en_mstr")
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_periode_mstr())
        plans_periode.Properties.DataSource = dt_bantu
        plans_periode.Properties.DisplayMember = dt_bantu.Columns("periode_code").ToString
        plans_periode.Properties.ValueMember = dt_bantu.Columns("periode_code").ToString
        plans_periode.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Trans Code", "plans_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "plans_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Periode", "plans_periode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "plans_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Amount Total", "plans_amount_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "User Create", "plans_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "plans_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "plans_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "plans_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column_copy(gv_detail, "Customer Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Customer Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Amount", "plansd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_edit, "plansd_ptnr_id", False)
        add_column(gv_edit, "Customer Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Customer Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Amount", "plansd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "{0:n}")

    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  a.plans_oid, " _
                & "  a.plans_code, " _
                & "  a.plans_date, " _
                & "  a.plans_periode, " _
                & "  d.periode_start_date, " _
                & "  d.periode_end_date, " _
                & "  a.plans_sales_id, " _
                & "  c.ptnr_code, " _
                & "  c.ptnr_name, " _
                & "  a.plans_en_id, " _
                & "  b.en_desc, " _
                & "  a.plans_remarks, " _
                & "  a.plans_amount_total, " _
                & "  a.plans_add_by, " _
                & "  a.plans_add_date, " _
                & "  a.plans_upd_by, " _
                & "  a.plans_upd_date " _
                & "FROM " _
                & "  public.plans_mstr a " _
                & "  INNER JOIN public.en_mstr b ON (a.plans_en_id = b.en_id) " _
                & "  INNER JOIN public.ptnr_mstr c ON (a.plans_sales_id = c.ptnr_id) " _
                & "  INNER JOIN public.psperiode_mstr d ON (a.plans_periode = d.periode_code) " _
                & "WHERE " _
                 & " a.plans_en_id in (select user_en_id from tconfuserentity " _
                & "  where userid = " + master_new.ClsVar.sUserID.ToString + ") and " _
                & "  a.plans_date BETWEEN " _
                & SetDateNTime00(pr_txttglawal.DateTime) & " and " & SetDateNTime00(pr_txttglakhir.DateTime)

        Dim ssql As String
        ssql = "select ptnr_id,ptnr_is_bm from ptnr_mstr where ptnr_user_name = (select usernama from tconfuser where userid= " _
                                           & master_new.ClsVar.sUserID & ")"
        Dim dt As New DataTable
        dt = master_new.PGSqlConn.GetTableData(ssql)

        Dim _ptnr_id, _ptnr_is_bm As String
        _ptnr_id = ""
        _ptnr_is_bm = ""

        For Each dr As DataRow In dt.Rows
            _ptnr_id = dr(0).ToString
            _ptnr_is_bm = SetString(dr(1))
        Next

        If _ptnr_is_bm = "" Or _ptnr_is_bm = "N" Then
            get_sequel += " and plans_sales_id=" & SetInteger(_ptnr_id)
        End If

        get_sequel += "   ORDER BY " _
                & "  a.plans_code"



        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        plans_en_id.EditValue = ""
        plans_sales_id.Tag = ""
        plans_sales_id.EditValue = ""
        plans_date.DateTime = CekTanggal()
        plans_remarks.EditValue = ""
        plans_en_id.Focus()
        plans_periode.ItemIndex = 0

        Dim ssql As String
        ssql = "select ptnr_id,ptnr_is_bm,ptnr_name from ptnr_mstr where ptnr_user_name = (select usernama from tconfuser where userid= " _
                                           & master_new.ClsVar.sUserID & ")"
        Dim dt As New DataTable
        dt = master_new.PGSqlConn.GetTableData(ssql)

        Dim _ptnr_id, _ptnr_is_bm, _ptnr_name As String
        _ptnr_id = ""
        _ptnr_is_bm = ""
        _ptnr_name = ""
        For Each dr As DataRow In dt.Rows
            _ptnr_id = dr(0).ToString
            _ptnr_is_bm = SetString(dr(1))
            _ptnr_name = SetString(dr(2))
        Next

        If _ptnr_is_bm = "" Or _ptnr_is_bm = "N" Then
            plans_sales_id.Tag = _ptnr_id
            plans_sales_id.EditValue = _ptnr_name
            plans_sales_id.Enabled = False
        Else

            plans_sales_id.Enabled = True
        End If

        Try
            tcg_header.SelectedTabPageIndex = 0
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
                        & "  a.plansd_oid, " _
                        & "  a.plansd_plans_oid, " _
                        & "  a.plansd_ptnr_id, " _
                        & "  a.plansd_amount, " _
                        & "  a.plansd_seq, " _
                        & "  b.ptnr_code, " _
                        & "  b.ptnr_name " _
                        & "FROM " _
                        & "  public.plansd_det a " _
                        & "  INNER JOIN public.ptnr_mstr b ON (a.plansd_ptnr_id = b.ptnr_id) " _
                        & "WHERE " _
                        & "  a.plansd_plans_oid IS NULL " _
                        & "ORDER BY " _
                        & "  a.plansd_seq"


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

        Dim _mstr_oid As String = Guid.NewGuid.ToString
        Dim i As Integer
        Dim ssqls As New ArrayList
        Dim _code As String
        Dim _total As Double

        _code = GetNewNumberYM("plans_mstr", "plans_code", 5, "PLA" & plans_en_id.GetColumnValue("en_code") _
                                     & CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)

        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
        ds_edit.Tables(0).AcceptChanges()

        Try
            _total = 0
            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                _total = _total + SetNumber(ds_edit.Tables(0).Rows(i).Item("plansd_amount"))
            Next

            ssql = "INSERT INTO  " _
                & "  public.plans_mstr " _
                & "( " _
                & "  plans_oid, " _
                & "  plans_code, " _
                & "  plans_date, " _
                & "  plans_periode, " _
                & "  plans_sales_id, " _
                & "  plans_add_by, " _
                & "  plans_add_date, " _
                & "  plans_amount_total, " _
                & "  plans_dom_id, " _
                & "  plans_en_id, " _
                & "  plans_remarks " _
                & ")  " _
                & "VALUES ( " _
                & SetSetring(_mstr_oid) & ",  " _
                & SetSetring(_code) & ",  " _
                & SetDateNTime00(plans_date.EditValue) & ",  " _
                & SetSetring(plans_periode.EditValue) & ",  " _
                & SetInteger(plans_sales_id.Tag) & ",  " _
                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                & SetDec(_total) & ",  " _
                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                & SetInteger(plans_en_id.EditValue) & ",  " _
                & SetSetring(plans_remarks.EditValue) & "  " _
                & ")"

            ssqls.Add(ssql)

            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                With ds_edit.Tables(0).Rows(i)


                    ssql = "INSERT INTO  " _
                        & "  public.plansd_det " _
                        & "( " _
                        & "  plansd_oid, " _
                        & "  plansd_plans_oid, " _
                        & "  plansd_ptnr_id, " _
                        & "  plansd_amount, " _
                        & "  plansd_seq " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_mstr_oid) & ",  " _
                        & SetInteger(.Item("plansd_ptnr_id")) & ",  " _
                        & SetDec(.Item("plansd_amount")) & ",  " _
                        & SetInteger(i) & "  " _
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
            set_row(_mstr_oid, "plans_oid")
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

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _mstr_oid = .Item("plans_oid").ToString
                plans_en_id.EditValue = .Item("plans_en_id")
                plans_date.DateTime = .Item("plans_date")
                plans_sales_id.Tag = .Item("plans_sales_id")
                plans_sales_id.EditValue = .Item("ptnr_name")
                plans_remarks.EditValue = .Item("plans_remarks")
                plans_periode.EditValue = .Item("plans_periode")
            End With
            plans_en_id.Focus()

            ds_edit = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb

                        .SQL = "SELECT  " _
                            & "  a.plansd_oid, " _
                            & "  a.plansd_plans_oid, " _
                            & "  a.plansd_ptnr_id, " _
                            & "  a.plansd_amount, " _
                            & "  a.plansd_seq, " _
                            & "  b.ptnr_code, " _
                            & "  b.ptnr_name " _
                            & "FROM " _
                            & "  public.plansd_det a " _
                            & "  INNER JOIN public.ptnr_mstr b ON (a.plansd_ptnr_id = b.ptnr_id) " _
                            & "WHERE " _
                            & "  a.plansd_plans_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("plans_oid").ToString & "' " _
                            & "ORDER BY " _
                            & "  a.plansd_seq"

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail")
                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim ssqls As New ArrayList
        Dim i As Integer
        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran
                        .Command.CommandType = CommandType.Text

                        Dim _total As Double

                        _total = 0
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            _total = _total + SetNumber(ds_edit.Tables(0).Rows(i).Item("plansd_amount"))
                        Next

                        .Command.CommandText = "UPDATE  " _
                                & "  public.plans_mstr   " _
                                & "SET  " _
                                & "  plans_date = " & SetDateNTime00(plans_date.EditValue) & ",  " _
                                & "  plans_periode = " & SetSetring(plans_periode.EditValue) & ",  " _
                                & "  plans_sales_id = " & SetInteger(plans_sales_id.Tag) & ",  " _
                                & "  plans_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                & "  plans_upd_date = " & SetDateNTime(CekTanggal) & ",  " _
                                & "  plans_amount_total = " & SetDec(_total) & ",  " _
                                & "  plans_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                & "  plans_en_id = " & SetSetring(plans_en_id.EditValue) & ",  " _
                                & "  plans_remarks = " & SetSetring(plans_remarks.EditValue) & "  " _
                                & "WHERE  " _
                                & "  plans_oid = " & SetSetring(_mstr_oid) & " "


                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "Delete from plansd_det " _
                                            & "WHERE  " _
                                            & "  plansd_plans_oid = " & SetSetring(_mstr_oid) & " "

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            'With ds_edit.Tables(0).Rows(i)



                            ssql = "INSERT INTO  " _
                              & "  public.plansd_det " _
                              & "( " _
                              & "  plansd_oid, " _
                              & "  plansd_plans_oid, " _
                              & "  plansd_ptnr_id, " _
                              & "  plansd_amount, " _
                              & "  plansd_seq " _
                              & ")  " _
                              & "VALUES ( " _
                              & SetSetring(Guid.NewGuid.ToString) & ",  " _
                              & SetSetring(_mstr_oid) & ",  " _
                              & SetInteger(ds_edit.Tables(0).Rows(i).Item("plansd_ptnr_id")) & ",  " _
                              & SetDec(ds_edit.Tables(0).Rows(i).Item("plansd_amount")) & ",  " _
                              & SetInteger(i) & "  " _
                              & ")"


                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = ssql

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'End With
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
                        set_row(_mstr_oid, "plans_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        edit = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        edit = False
                    End Try
                End With
            End Using
        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
    End Function
    Public Overrides Function before_delete() As Boolean
        before_delete = True


    End Function
    Public Overrides Function delete_data() As Boolean
        delete_data = False

        gv_master_SelectionChanged(Nothing, Nothing)

        Dim sSQL As String
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
                With ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position)

                    sSQL = "DELETE FROM  " _
                        & "  public.plans_mstr  " _
                        & "WHERE  " _
                        & "  plans_oid ='" & .Item("plans_oid").ToString & "'"

                    ssqls.Add(sSQL)


                End With

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

                help_load_data(True)
                MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
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


        If ds_edit.Tables(0).Rows.Count = 0 Then
            Box("Detail can't blank")
            Return False
            Exit Function
        End If


        Return before_save
    End Function

    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        gv_master_SelectionChanged(sender, Nothing)
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        Try
            If ds.Tables(0).Rows.Count = 0 Then
                Exit Sub
            End If

            Dim sql As String = ""

            Try
                ds.Tables("detail").Clear()
            Catch ex As Exception
            End Try


            sql = "SELECT  " _
                & "  a.plansd_oid, " _
                & "  a.plansd_plans_oid, " _
                & "  a.plansd_ptnr_id, " _
                & "  a.plansd_amount, " _
                & "  a.plansd_seq, " _
                & "  b.ptnr_code, " _
                & "  b.ptnr_name " _
                & "FROM " _
                & "  public.plansd_det a " _
                & "  INNER JOIN public.ptnr_mstr b ON (a.plansd_ptnr_id = b.ptnr_id) " _
                & "WHERE " _
                & "  a.plansd_plans_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("plans_oid").ToString & "' " _
                & "ORDER BY " _
                & "  a.plansd_seq"


            load_data_detail(sql, gc_detail, "detail")

        Catch ex As Exception
            Pesan(Err)
        End Try
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
        'Dim _en_id As Integer = casho_en_id.EditValue

        If _col = "ptnr_name" Or _col = "ptnr_code" Then


            Dim frm As New FPartnerSearch
            frm.set_win(Me)
            frm._obj = gv_edit
            frm._row = _row
            frm._en_id = plans_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()

        End If
    End Sub

    Private Sub ps_bom_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles plans_sales_id.ButtonClick
        Try

            Dim frm As New FPartnerSearch
            frm.set_win(Me)
            frm._obj = plans_sales_id
            frm._en_id = plans_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub



End Class
