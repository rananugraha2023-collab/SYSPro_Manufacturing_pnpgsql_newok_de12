Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FQRCode
    Dim _wh_oid_mstr As String
    Dim _id As Integer
    'Public _obj As Object
    'Public _objk As Object
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FQRCOde_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'Ds_req1.DataTable1' table. You can move, or remove it, as needed.
        'Me.DataTable1TableAdapter.Fill(Me.Ds_req1.DataTable1)
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))

        With qrcode_en_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("en_id").ToString
            .ItemIndex = 0
        End With
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "QR Code", "qrcode_oid", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "QR Code Name", "qrcode_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Used Date", "qrcode_used_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Refferensi oid", "qrcode_reff_oid", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Refferensi Dok", "qrcode_reff_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Print Date", "qrcode_print_date", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Name", "qrcode_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Remarks Sub", "locs_remarks", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "Location ID", "locs_loc_id", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Location Code", "loc_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Location Desc", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Active", "qrcode_active", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "qrcode_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "qrcode_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "qrcode_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "qrcode_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    'Public Overrides Sub load_cb_en()
    '    dt_bantu = New DataTable
    '    dt_bantu = (func_data.load_loc_mstr_to(qrcode_en_id.EditValue))
    '    locs_loc_id.Properties.DataSource = dt_bantu
    '    locs_loc_id.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
    '    locs_loc_id.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
    '    locs_loc_id.ItemIndex = 0

    'End Sub

    Private Sub wh_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles qrcode_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  a.qrcode_en_id, " _
                & "  b.en_desc, " _
                & "  a.qrcode_oid, " _
                & "  a.qrcode_id, " _
                & "  a.qrcode_code, " _
                & "  a.qrcode_name, " _
                & "  a.qrcode_remarks, " _
                & "  a.qrcode_used_date, " _
                & "  a.qrcode_reff_oid, " _
                & "  a.qrcode_reff_code, " _
                & "  a.qrcode_print_date, " _
                & "  a.qrcode_active, " _
                & "  a.qrcode_add_date, " _
                & "  a.qrcode_add_by, " _
                & "  a.qrcode_upd_date, " _
                & "  a.qrcode_upd_by " _
                & "FROM " _
                & "  public.qrcode_mstr a " _
                & "  INNER JOIN public.en_mstr b ON (a.qrcode_en_id = b.en_id) " _
                & " where qrcode_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & "order by qrcode_id"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        qrcode_en_id.Focus()
        qrcode_en_id.ItemIndex = 0
        qrcode_remarks.Text = ""
        qrcode_name.Text = ""
        qrcode_name.EditValue = False
        qrcode_active.EditValue = False
        qrcode_ce_series.EditValue = False

    End Sub

    Private Function GetID_Local(ByVal par_en_code As String) As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text

                    .Command.CommandText = "select coalesce(max(cast(substring(cast(wh_id as varchar),5,100) as integer)),0) as max_col  from wh_mstr " + _
                                           " where substring(cast(wh_id as varchar),5,100) <> ''"
                    .InitializeCommand()

                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read

                        GetID_Local = .DataReader("max_col") + 1
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If par_en_code = "0" Then
            par_en_code = "99"
        End If

        GetID_Local = CInt(par_en_code + master_new.ClsVar.sServerCode + GetID_Local.ToString)

        Return GetID_Local
    End Function

    Public Overrides Function insert() As Boolean
        Dim _qr_oid As Guid
        _qr_oid = Guid.NewGuid
        Dim ssqls As New ArrayList

        Dim _qr_id As Integer
        _qr_id = SetInteger(func_coll.GetID("qrcode_mstr", "qrcode_id"))

        Dim _qr_code As String
        _qr_code = func_coll.get_transaction_number("QR", qrcode_en_id.GetColumnValue("en_code"), "qrcode_mstr", "qrcode_code")
        '_qr_code = SetSetring("QR", func_coll.GetID("qrcode_mstr", "qrcode_id"))

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                    & "  public.qrcode_mstr " _
                                    & "( " _
                                    & "  qrcode_oid, " _
                                    & "  qrcode_en_id, " _
                                    & "  qrcode_id, " _
                                    & "  qrcode_code, " _
                                    & "  qrcode_add_date, " _
                                    & "  qrcode_add_by, " _
                                    & "  qrcode_name, " _
                                    & "  qrcode_remarks, " _
                                    & "  qrcode_active " _
                                    & ") " _
                                    & "VALUES ( " _
                                    & SetSetring(_qr_oid.ToString) & ",  " _
                                    & SetSetring(qrcode_en_id.EditValue) & ",  " _
                                    & SetInteger(_qr_id) & ",  " _
                                    & SetSetring(_qr_code) & ",  " _
                                    & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & SetSetring(_qr_code) & ",  " _
                                    & SetSetring(qrcode_remarks.EditValue) & ",  " _
                                    & SetBitYN(qrcode_active.EditValue) & "  " _
                                    & ")"

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
                        insert = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        insert = False
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
        'If MyBase.edit_data = True Then
        '    row = BindingContext(ds.Tables(0)).Position

        '    With ds.Tables(0).Rows(row)
        '        _id = .Item("losc_id")
        '        qrcode_en_id.EditValue = .Item("qrcode_en_id")
        '        'locs_id.Text = .Item("locs_id")
        '        qrcode_name.EditValue = .Item("qrcode_name")
        '        qrcode_remarks.EditValue = .Item("qrcode_remarks")
        '        'wh_parent.EditValue = .Item("wh_parent")
        '        qrcode_active.EditValue = IIf(.Item("qrcode_active") = "Y", True, False)
        '    End With
        '    qrcode_en_id.Focus()
        '    'wh_parent.Enabled = False
        '    edit_data = True
        'End If
    End Function

    Public Overrides Function edit()
        edit = True
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
                        .Command.CommandText = "UPDATE  " _
                                    & "  public.qrcode_mstr  " _
                                    & "SET  " _
                                    & "  qrcode_en_id = " & SetInteger(qrcode_en_id.EditValue) & ",  " _
                                    & "  qrcode_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                    & "  qrcode_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & "  qrcode_name = " & SetSetring(qrcode_name.EditValue) & ",  " _
                                    & "  qrcode_remarks = " & SetSetring(qrcode_remarks.EditValue) & ",  " _
                                    & "  qrcode_active = " & SetBitYN(qrcode_active.EditValue) & "  " _
                                    & "WHERE  " _
                                    & "  qrcode_id = " & SetInteger(_id) & " "


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

    Public Overrides Function delete_data() As Boolean
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
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from qrcode_mstr where losc_id = " + SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("qrcode_id")) + ""
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
        End If

        Return delete_data
    End Function

    Private Sub qrcode_be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles qrcode_be_first.ButtonClick
        Dim frm As New FQRCodeSearch()
        frm.set_win(Me)
        'frm._en_id = le_entity.EditValue
        frm._obj = qrcode_be_first
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub qrcode_be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles qrcode_be_to.ButtonClick
        Dim frm As New FQRCodeSearch()
        frm.set_win(Me)
        'frm._en_id = le_entity.EditValue
        frm._obj = qrcode_be_to
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String



        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("qrcode_en_id")
        _type = 10
        _table = "qrcode_mstr"
        _initial = "qr"
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("qrcode_id")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("qrcode_id")

        'func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir)

        Dim ds_bantu As New DataSet
        Dim _sql As String
        If qrcode_ce_series.EditValue = True Then
            Try
                'Dim ssql As String
                _sql = "SELECT  " _
                    & "  public.qrcode_mstr.qrcode_oid, " _
                    & "  public.qrcode_mstr.qrcode_id, " _
                    & "  public.qrcode_mstr.qrcode_en_id, " _
                    & "  public.qrcode_mstr.qrcode_code, " _
                    & "  public.qrcode_mstr.qrcode_name, " _
                    & "  public.qrcode_mstr.qrcode_remarks, " _
                    & "  public.qrcode_mstr.qrcode_active, " _
                    & "  public.en_mstr.en_desc " _
                    & "FROM " _
                    & "  public.qrcode_mstr " _
                    & "  INNER JOIN public.en_mstr ON (public.qrcode_mstr.qrcode_en_id = public.en_mstr.en_id)" _
                    & " Where " _
                    & " qrcode_reff_oid IS NULL " _
                    & " and qrcode_mstr.qrcode_name >= '" + qrcode_be_first.Text + "'" _
                    & "and qrcode_mstr.qrcode_name <= '" + qrcode_be_to.Text + "'" _
                    & "ORDER BY " _
                    & "  public.qrcode_mstr.qrcode_name"

                'Dim rpt As New rptLabelQRCode2
                'With rpt
                '    Dim ds As New DataSet
                '    ds = ReportDataset(ssql)
                '    'If ds.Tables(0).Rows.Count < 1 Then
                '    '    Box("Maaf data kosong")
                '    '    Exit Sub
                '    'End If

                '    .DataSource = ds
                '    .DataMember = "Table"

                '     Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
  
                '    printTool.PreviewForm.Text = "Location Sub"
                '    '.PrintingSystem = ps
                '    printTool.ShowPreview()
                'End With

                Dim frm As New frmPrintDialog
                frm._ssql = _sql
                frm._report = "rptLabelQRCode2"
                frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("qrcode_code")
                frm.ShowDialog()


            Catch ex As Exception

            End Try

        Else
            _sql = "SELECT  " _
                & "  public.qrcode_mstr.qrcode_oid, " _
                & "  public.qrcode_mstr.qrcode_id, " _
                & "  public.qrcode_mstr.qrcode_en_id, " _
                & "  public.qrcode_mstr.qrcode_code, " _
                & "  public.qrcode_mstr.qrcode_name, " _
                & "  public.qrcode_mstr.qrcode_remarks, " _
                & "  public.qrcode_mstr.qrcode_active, " _
                & "  public.en_mstr.en_desc " _
                & "FROM " _
                & "  public.qrcode_mstr " _
                & "  INNER JOIN public.en_mstr ON (public.qrcode_mstr.qrcode_en_id = public.en_mstr.en_id)" _
                & "WHERE " _
                & " qrcode_reff_oid IS NULL " _
                & "and qrcode_mstr.qrcode_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("qrcode_code") + "'"

            Dim frm As New frmPrintDialog
            frm._ssql = _sql
            frm._report = "rptLabelQRCode2"
            frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("qrcode_code")
            frm.ShowDialog()

            'Dim rpt As New rptLabelQRCode
            'With rpt
            '    Dim ds As New DataSet
            '    ds = ReportDataset(_sql)
            '    'If ds.Tables(0).Rows.Count < 1 Then
            '    '    Box("Maaf data kosong")
            '    '    Exit Sub
            '    'End If

            '    .DataSource = ds
            '    .DataMember = "Table"

            '     Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
  
            '    printTool.PreviewForm.Text = "Location Sub"
            '    '.PrintingSystem = ps
            '    printTool.ShowPreview()
            'End With
            'Catch ex As Exception

        End If


    End Sub
End Class

