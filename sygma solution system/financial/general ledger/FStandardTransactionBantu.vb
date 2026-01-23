Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FStandardTransactionBantu

    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ds_edit As DataSet
    Dim _glt_code As String

#Region "Setting Awal"
    Private Sub FStandardTransaction_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = Now
        pr_txttglakhir.DateTime = Now
        gv_edit.OptionsSelection.MultiSelect = True
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        glt_en_id.Properties.DataSource = dt_bantu
        glt_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        glt_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        glt_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        glt_cu_id.Properties.DataSource = dt_bantu
        glt_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        glt_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        glt_cu_id.ItemIndex = 0
    End Sub

    Private Sub glt_cu_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles glt_cu_id.EditValueChanged
        Dim _glt_exc_rate As Double
        If glt_cu_id.EditValue <> master_new.ClsVar.ibase_cur_id Then
            _glt_exc_rate = func_data.get_exchange_rate(glt_cu_id.EditValue)
            If _glt_exc_rate = 1 Then
                glt_exc_rate.EditValue = 0
            Else
                glt_exc_rate.EditValue = _glt_exc_rate
            End If

        Else
            glt_exc_rate.EditValue = 1
        End If
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "GL Number", "glt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Effective Date", "glt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Type", "glt_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Exchange Rate", "glt_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "glt_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Debit", "glt_debit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Credit", "glt_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Ext Debit", "glt_ext_debit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_copy(gv_master, "Ext Credit", "glt_ext_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        'add_column_copy(gv_master, "Transaction", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Ref. Number", "glt_ref_trans_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Daybook", "glt_daybook", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Posted", "glt_posted", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Reverse", "glt_is_reverse", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "glt_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "glt_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "glt_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "glt_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_edit, "glt_ac_id", False)
        add_column(gv_edit, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "glt_sb_id", False)
        add_column(gv_edit, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "glt_cc_id", False)
        add_column(gv_edit, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Description", "glt_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Debit", "glt_debit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n2}")
        add_column_edit(gv_edit, "Credit", "glt_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n2}")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  public.glt_det.glt_oid, " _
                    & "  public.glt_det.glt_dom_id, " _
                    & "  public.glt_det.glt_en_id, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.glt_det.glt_add_by, " _
                    & "  public.glt_det.glt_add_date, " _
                    & "  public.glt_det.glt_upd_by, " _
                    & "  public.glt_det.glt_upd_date, " _
                    & "  public.glt_det.glt_gl_oid, " _
                    & "  public.glt_det.glt_code, " _
                    & "  public.glt_det.glt_date, " _
                    & "  public.glt_det.glt_type, " _
                    & "  public.glt_det.glt_cu_id, " _
                    & "  public.glt_det.glt_exc_rate, " _
                    & "  public.glt_det.glt_seq, " _
                    & "  public.glt_det.glt_ac_id, " _
                    & "  public.glt_det.glt_sb_id, " _
                    & "  public.glt_det.glt_cc_id, " _
                    & "  public.glt_det.glt_desc, " _
                    & "  public.glt_det.glt_debit, " _
                    & "  public.glt_det.glt_credit, " _
                    & "  public.glt_det.glt_debit * public.glt_det.glt_exc_rate as glt_ext_debit, " _
                    & "  public.glt_det.glt_credit * public.glt_det.glt_exc_rate as glt_ext_credit, " _
                    & "  public.glt_det.glt_posted, " _
                    & "  public.glt_det.glt_dt, " _
                    & "  public.tran_mstr.tran_name, " _
                    & "  public.glt_det.glt_ref_trans_code, " _
                    & "  public.glt_det.glt_daybook, " _
                    & "  coalesce(glt_is_reverse,'N') as glt_is_reverse, " _
                    & "  public.cu_mstr.cu_name, " _
                    & "  public.ac_mstr.ac_code, " _
                    & "  public.ac_mstr.ac_name, " _
                    & "  public.cc_mstr.cc_desc, " _
                    & "  public.sb_mstr.sb_desc " _
                    & "FROM " _
                    & "  public.glt_det " _
                    & "  INNER JOIN public.en_mstr ON (public.glt_det.glt_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.cu_mstr ON (public.glt_det.glt_cu_id = public.cu_mstr.cu_id) " _
                    & "  INNER JOIN public.ac_mstr ON (public.glt_det.glt_ac_id = public.ac_mstr.ac_id) " _
                    & "  LEFT OUTER JOIN public.tran_mstr ON (public.glt_det.glt_ref_tran_id = public.tran_mstr.tran_id) " _
                    & "  left outer join public.cc_mstr ON (public.glt_det.glt_cc_id = public.cc_mstr.cc_id) " _
                    & "  left outer join public.sb_mstr ON (public.glt_det.glt_sb_id = public.sb_mstr.sb_id)" _
                    & " where glt_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and glt_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and glt_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function
#End Region

#Region "DML"
    Public Overrides Sub insert_data_awal()
    End Sub

    Public Overrides Function insert_data() As Boolean
        MessageBox.Show("Insert Data Not Available For This Menu...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Return False
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        Return before_save
    End Function

    Public Overrides Function before_delete() As Boolean
        before_delete = True
        
        Return before_delete
    End Function

    Public Overrides Function delete_data() As Boolean
        MessageBox.Show("Delete Data Not Available For This Menu...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Return False
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _glt_code = .Item("glt_code")
                glt_en_id.EditValue = .Item("glt_en_id")
                glt_date.DateTime = .Item("glt_date")
                glt_type.EditValue = .Item("glt_type")
                glt_ref_trans_code.Text = SetString(.Item("glt_ref_trans_code"))
                glt_cu_id.EditValue = .Item("glt_cu_id")
                glt_exc_rate.EditValue = .Item("glt_exc_rate")
                glt_is_reverse.EditValue = SetBitYNB(.Item("glt_is_reverse"))
                'glt_desc.Text = .Item("glt_desc")
            End With
            glt_is_reverse.Focus()
            glt_en_id.Enabled = False
            glt_type.Enabled = False
            glt_cu_id.Enabled = False
            glt_ref_trans_code.Enabled = False
            glt_exc_rate.Enabled = False
            gc_edit.Enabled = False
            glt_date.Enabled = False 'effective date tidak bisa dirubah.....titik..
            ds_edit = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                        & "  public.glt_det.glt_ac_id, " _
                        & "  public.glt_det.glt_sb_id, " _
                        & "  public.glt_det.glt_cc_id, " _
                        & "  public.glt_det.glt_desc, " _
                        & "  public.glt_det.glt_debit, " _
                        & "  public.glt_det.glt_credit, " _
                        & "  public.ac_mstr.ac_code, " _
                        & "  public.ac_mstr.ac_name, " _
                        & "  public.cc_mstr.cc_desc, " _
                        & "  public.sb_mstr.sb_desc,glt_date,glt_en_id,glt_cu_id,glt_cu_id,glt_exc_rate " _
                        & "FROM " _
                        & "  public.glt_det " _
                        & "  INNER JOIN public.en_mstr ON (public.glt_det.glt_en_id = public.en_mstr.en_id) " _
                        & "  INNER JOIN public.cu_mstr ON (public.glt_det.glt_cu_id = public.cu_mstr.cu_id) " _
                        & "  INNER JOIN public.ac_mstr ON (public.glt_det.glt_ac_id = public.ac_mstr.ac_id) " _
                        & "  LEFT OUTER JOIN public.tran_mstr ON (public.glt_det.glt_ref_tran_id = public.tran_mstr.tran_id) " _
                        & "  left outer join public.cc_mstr ON (public.glt_det.glt_cc_id = public.cc_mstr.cc_id) " _
                        & "  left outer join public.sb_mstr ON (public.glt_det.glt_sb_id = public.sb_mstr.sb_id)" _
                        & " where glt_code = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("glt_code") + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail")
                        .FillDataSet(ds_edit, "old")
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
#End Region

#Region "gv_edit"
    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        If e.Column.Name = "glt_debit" Then
            If e.Value > 0 Then
                gv_edit.SetRowCellValue(e.RowHandle, "glt_credit", 0)
            End If
        ElseIf e.Column.Name = "glt_credit" Then
            If e.Value > 0 Then
                gv_edit.SetRowCellValue(e.RowHandle, "glt_debit", 0)
            End If
        End If
        gv_edit.BestFitColumns()
    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        With gv_edit
            .SetRowCellValue(e.RowHandle, "glt_debit", 0)
            .SetRowCellValue(e.RowHandle, "glt_credit", 0)
            .SetRowCellValue(e.RowHandle, "glt_sb_id", 0)
            .SetRowCellValue(e.RowHandle, "sb_desc", "-")
            .SetRowCellValue(e.RowHandle, "glt_cc_id", 0)
            .SetRowCellValue(e.RowHandle, "cc_desc", "-")
            .BestFitColumns()
        End With
    End Sub

    Public Overrides Function insert() As Boolean
        
    End Function

    Public Overrides Function edit()
        edit = True
        Dim ssqls As New ArrayList
        Dim _glt_code As String = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("glt_code")

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update glt_det set glt_is_reverse = " + SetBitYN(glt_is_reverse.EditValue) + "where glt_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("glt_code") + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit GL " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("glt_code"))
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

                        after_success()
                        set_row(_glt_code, "glt_code")
                        edit = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        edit = False
                    End Try
                End With
            End Using
        Catch ex As Exception
            row = 0
            edit = False
            MessageBox.Show(ex.Message)
        End Try

        Return edit
    End Function
#End Region

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data()
        End If
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle
        Dim _gl_en_id As Integer = glt_en_id.EditValue

        If _col = "ac_code" Then
            Dim frm As New FAccountSearch
            frm.set_win(Me)
            frm._row = _row
            frm._cu_id = glt_cu_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "ac_name" Then
            Dim frm As New FAccountSearch
            frm.set_win(Me)
            frm._row = _row
            frm._cu_id = glt_cu_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
            'ElseIf _col = "sb_desc" Then
            '    Dim frm As New FSubAccountSearch
            '    frm.set_win(Me)
            '    frm._row = _row
            '    frm._en_id = _gl_en_id
            '    frm.type_form = True
            '    frm.ShowDialog()
            'ElseIf _col = "cc_desc" Then
            '    Dim frm As New FCostCenterSearch
            '    frm.set_win(Me)
            '    frm._row = _row
            '    frm._en_id = _gl_en_id
            '    frm.type_form = True
            '    frm.ShowDialog()
        End If
    End Sub

    Private Sub PasteFromExcelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteFromExcelToolStripMenuItem.Click
        Dim data As String = Clipboard.GetDataObject().GetData(DataFormats.Text)
        Dim i As String
        Dim a() As String
        Dim j As Integer

        Try
            'Box(data)
            i = data
            a = i.Split(vbNewLine)
            ds_edit.AcceptChanges()
            For j = 0 To a.GetUpperBound(0)
                'Box(a(j))
                If j <= ds_edit.Tables(0).Rows.Count - 1 Then
                    ds_edit.Tables(0).Rows(j).Item("glt_debit") = a(j).ToString
                End If
            Next
            ds_edit.AcceptChanges()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub PasteFromExcelToCreditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteFromExcelToCreditToolStripMenuItem.Click
        Dim data As String = Clipboard.GetDataObject().GetData(DataFormats.Text)
        Dim i As String
        Dim a() As String
        Dim j As Integer

        Try
            'Box(data)
            i = data
            a = i.Split(vbNewLine)
            ds_edit.AcceptChanges()
            For j = 0 To a.GetUpperBound(0)
                'Box(a(j))
                If j <= ds_edit.Tables(0).Rows.Count - 1 Then
                    ds_edit.Tables(0).Rows(j).Item("glt_credit") = a(j).ToString
                End If
            Next
            ds_edit.AcceptChanges()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub PasteToRemarkToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToRemarkToolStripMenuItem.Click
        Dim data As String = Clipboard.GetDataObject().GetData(DataFormats.Text)
        Dim i As String
        Dim a() As String
        Dim j As Integer

        Try
            'Box(data)
            i = data.Replace(vbNewLine, "~")
            a = i.Split("~")
            ds_edit.AcceptChanges()
            For j = 0 To a.GetUpperBound(0)
                'Box(a(j))
                If j <= ds_edit.Tables(0).Rows.Count - 1 Then
                    ds_edit.Tables(0).Rows(j).Item("glt_desc") = a(j).ToString
                End If

            Next
            ds_edit.AcceptChanges()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub PasteFromExcelToAccountToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteFromExcelToAccountToolStripMenuItem.Click
        Dim data As String = Clipboard.GetDataObject().GetData(DataFormats.Text)
        Dim i As String
        Dim a() As String
        Dim j As Integer
        Dim ssql As String
        Try
            'Box(data)
            i = data.Replace(vbNewLine, "~")
            a = i.Split("~")
            ds_edit.AcceptChanges()
            For j = 0 To a.GetUpperBound(0)
                'Box(a(j))
                If j <= ds_edit.Tables(0).Rows.Count - 1 Then
                    ssql = "Select ac_id,ac_code,ac_name from ac_mstr where ac_code='" & Trim(a(j).ToString) & "'"
                    Dim dr As DataRow = GetRowInfo(ssql)
                    ds_edit.Tables(0).Rows(j).Item("glt_ac_id") = dr("ac_id")
                    ds_edit.Tables(0).Rows(j).Item("ac_code") = a(j).ToString
                    ds_edit.Tables(0).Rows(j).Item("ac_name") = dr("ac_name")
                End If

            Next
            ds_edit.AcceptChanges()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Overrides Sub preview()
        'Dim _en_id As Integer
        'Dim _type, _table, _initial, _code_awal, _code_akhir As String

        '_en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_en_id")
        '_type = 10
        '_table = "so_mstr"
        '_initial = "so"
        '_code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code")
        '_code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code")

        'func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
             & "glt_det.glt_oid, " _
             & "glt_det.glt_dom_id, " _
             & "glt_det.glt_en_id, " _
             & "en_mstr.en_desc, " _
             & "glt_det.glt_add_by, " _
             & "glt_det.glt_add_date, " _
             & "glt_det.glt_upd_by, " _
             & "glt_det.glt_upd_date, " _
             & "glt_det.glt_gl_oid, " _
             & "glt_det.glt_code, " _
             & "glt_det.glt_date, " _
             & "glt_det.glt_type, " _
             & "glt_det.glt_cu_id, " _
             & "glt_det.glt_exc_rate, " _
             & "glt_det.glt_seq, " _
             & "glt_det.glt_ac_id, " _
             & "glt_det.glt_sb_id, " _
             & "glt_det.glt_cc_id, " _
             & "glt_det.glt_desc, " _
             & "glt_det.glt_debit, " _
             & "glt_det.glt_credit, " _
             & "glt_det.glt_debit * glt_det.glt_exc_rate as glt_ext_debit, " _
             & "glt_det.glt_credit * glt_det.glt_exc_rate as glt_ext_credit, " _
             & "glt_det.glt_posted, " _
             & "glt_det.glt_dt, " _
             & "tran_mstr.tran_name, " _
             & "glt_det.glt_ref_trans_code, " _
             & "glt_det.glt_daybook, " _
             & "cu_mstr.cu_name, " _
             & "ac_mstr.ac_code, " _
             & "ac_mstr.ac_name, " _
             & "cc_mstr.cc_desc, " _
             & "sb_mstr.sb_desc, " _
             & "cmaddr_name, " _
             & "cmaddr_line_1, " _
             & "cmaddr_line_2, " _
             & "cmaddr_line_3 " _
             & "FROM " _
             & "glt_det " _
             & "INNER JOIN en_mstr ON (glt_det.glt_en_id = en_mstr.en_id) " _
             & "INNER JOIN cu_mstr ON (glt_det.glt_cu_id = cu_mstr.cu_id) " _
             & "INNER JOIN ac_mstr ON (glt_det.glt_ac_id = ac_mstr.ac_id) " _
             & "LEFT OUTER JOIN tran_mstr ON (glt_det.glt_ref_tran_id = tran_mstr.tran_id) " _
             & "left outer join cc_mstr ON (glt_det.glt_cc_id = cc_mstr.cc_id) " _
             & "left outer join sb_mstr ON (glt_det.glt_sb_id = sb_mstr.sb_id)  " _
             & "inner join cmaddr_mstr on cmaddr_en_id = glt_en_id " _
             & "WHERE " _
             & "glt_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("glt_code") + "'"

        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRGeneralLedgerPrint"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("glt_code")
        frm.ShowDialog()
    End Sub
End Class
