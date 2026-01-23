Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FRosGenerate
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim mf As New master_new.ModFunction
    Dim ds_glt As DataSet
    Dim ds_group_glt As DataSet
    Dim ds_rosetta As DataSet
    Dim dt_rule As New DataTable
    Dim sSQL As String
    Dim dt_trans_rule As New DataTable
    Dim dt_group_rule As New DataTable
    Dim _seq As Integer = 0

    Private Sub FRosGenerate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_tglAwal.DateTime = CekTanggal()
        pr_tglAkhir.DateTime = CekTanggal()
    End Sub

    Public Overrides Sub form_first_load()
        'create_table()
        'help_load_data(False)
        load_cb()
        on_load()
        format_grid()
        add_handler_numeric()
        'add_groupsummary()
        'AllowIncrementalSearch()
        set_component()
        'load_Columns()

        spv_master = scc_master.PanelVisibility
        xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False
        xtp_edit.PageVisible = False
    End Sub


    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        le_entity.Properties.DataSource = dt_bantu
        le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        le_entity.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_gcal_mstr())

        If le_periode.Properties.Columns.VisibleCount = 0 Then
            le_periode.Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("gcal_start_date", "Start Date", 20, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Center))
            le_periode.Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("gcal_end_date", "End Date", 20, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Center))
        End If

        le_periode.Properties.DataSource = dt_bantu
        le_periode.Properties.DisplayMember = dt_bantu.Columns("gcal_start_date").ToString
        le_periode.Properties.ValueMember = dt_bantu.Columns("gcal_oid").ToString
        le_periode.ItemIndex = 0
        le_periode.Properties.DropDownRows = 12

        pr_tglAwal.EditValue = le_periode.GetColumnValue("gcal_start_date")
        pr_tglAkhir.EditValue = le_periode.GetColumnValue("gcal_end_date")

    End Sub

    Public Overrides Sub help_load_data(ByVal par As Boolean)
        MsgBox("Button Disabled", MsgBoxStyle.Critical, "Cannot View Data")
    End Sub


    Private Sub sb_generate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_generate.Click

        'cek opening balance
        Dim _gcal_available As Boolean = False
        Dim _gcal_start_date As Date
        Dim _gcal_end_date As Date

        Try
            If MessageBox.Show("Are you sure to process ..?", "Rosetta..", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Exit Sub
            End If

            sSQL = "select 0 as ac_id1,'' as ac_code1,'' as ac_name1,'' as ac_sign1,'' as ac_hirarki1,0 as ac_id2,'' as ac_code2,'' as ac_name2,'' as ac_sign2,'' as ac_hirarki2"
            dt_rule = master_new.PGSqlConn.GetTableData(sSQL)

            dt_rule.Rows.Clear()


            sSQL = "SELECT  " _
                & "  a.rosr_oid, " _
                & "  a.rosr_rosg_code, " _
                & "  a.rosr_rost_code, " _
                & "  a.rosr_ac_hirarki_debit, " _
                & "  a.rosr_ac_hirarki_credit, " _
                & "  a.rosr_value, " _
                & "  a.rosr_seq " _
                & "FROM " _
                & "  public.rosr_rule a " _
                & "ORDER BY " _
                & "  a.rosr_ac_hirarki_debit, " _
                & "  a.rosr_ac_hirarki_credit"

            dt_trans_rule = GetTableData(sSQL)

            sSQL = "SELECT  " _
                & "  a.rosgr_oid, " _
                & "  a.rosgr_rosg_code, " _
                & "  a.rosgr_ac_hirarki " _
                & "FROM " _
                & "  public.rosrg_rule_group a " _
                & "ORDER BY " _
                & "  a.rosgr_ac_hirarki"

            dt_group_rule = GetTableData(sSQL)


            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand

                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "SELECT  " _
                            & "  a.rosd_oid, " _
                            & "  a.rosd_rosg_code, " _
                            & "  a.rosd_amount, " _
                            & "  a.rosd_rost_code, " _
                            & "  a.rosd_periode, " _
                            & "  a.rosd_add_by " _
                            & "FROM " _
                            & "  public.rosd_data a " _
                            & "WHERE " _
                            & "  a.rosd_periode = " & SetSetring(le_periode.EditValue) & " "


                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader

                    While .DataReader.Read
                        _gcal_available = True
                        _gcal_start_date = CDate(pr_tglAwal.DateTime)
                        _gcal_end_date = CDate(pr_tglAkhir.DateTime)
                    End While

                    If _gcal_available = False Then
                        MessageBox.Show("Rosetta Openning Balance For This Periode doesn't exist :" + le_periode.Text.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If

                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        '-------------------------------------------------------

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "DELETE FROM  " _
                                & "  public.rosd_data  " _
                                & "WHERE  " _
                                & " rosd_periode=" & SetSetring(le_periode.EditValue) & " and rosd_rost_code<>'T01'"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        '.Command.CommandType = CommandType.Text
                        '.Command.CommandText = " UPDATE  " _
                        '    & "  public.glt_det   " _
                        '    & "SET  " _
                        '    & "  glt_is_gen_ros ='N'  " _
                        '    & "WHERE  " _
                        '    & "  glt_date between  " & SetDateNTime00(pr_tglAwal.DateTime) & "  and " & SetDateNTime00(pr_tglAkhir.DateTime) & " "
                        '.Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from rosh_history  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & " rosh_periode= " & SetSetring(le_periode.EditValue) & " "

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


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

        'collecting data dari glt_det

        sSQL = "select * from ros_sample"

        Dim dt As New DataTable
        dt = master_new.PGSqlConn.GetTableData(sSQL)



        ds_glt = New DataSet
        ds_group_glt = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb

                    If dt.Rows.Count > 0 Then
                        .SQL = "SELECT  " _
                           & "  glt_oid, " _
                           & "  glt_dom_id, " _
                           & "  glt_en_id, " _
                           & "  glt_add_by, " _
                           & "  glt_add_date, " _
                           & "  glt_upd_by, " _
                           & "  glt_upd_date, " _
                           & "  glt_gl_oid, " _
                           & "  glt_code, " _
                           & "  glt_date, " _
                           & "  glt_type, " _
                           & "  glt_cu_id, " _
                           & "  glt_exc_rate, " _
                           & "  glt_seq, " _
                           & "  glt_ac_id, " _
                           & "  ac_sign, ac_code_hirarki," _
                           & "  coalesce(ac_priority,99) as ac_priority , " _
                           & "  glt_sb_id, " _
                           & "  glt_cc_id, " _
                           & "  glt_desc, " _
                           & "  glt_debit, " _
                           & "  glt_credit, " _
                           & "  glt_ref_tran_id, " _
                           & "  glt_ref_trans_code, " _
                           & "  glt_posted, " _
                           & "  glt_dt, " _
                           & "  glt_daybook, " _
                           & "  glt_ref_oid, " _
                           & "  coalesce(glt_is_reverse,'N') as glt_is_reverse, " _
                           & "  glt_desc_detail, " _
                           & "  glt_ref_detail_no, " _
                           & "  glt_is_gen_ros, " _
                           & "  'N' as _empty " _
                           & "FROM  " _
                           & "  public.glt_det " _
                           & "  INNER JOIN public.ac_mstr ON (public.glt_det.glt_ac_id = public.ac_mstr.ac_id) " _
                           & "  where glt_date >= " & SetDate(_gcal_start_date) _
                           & "  and glt_date <= " & SetDate(_gcal_end_date) _
                           & "   and glt_type<>'JE'  and glt_code in (select * from ros_sample) and  (( glt_debit <> 0 and glt_credit = 0 ) or ( glt_debit = 0 and glt_credit <> 0 ))  " _
                           & " order by glt_date, glt_code,  glt_credit, glt_debit, glt_seq "
                    Else
                        .SQL = "SELECT  " _
                           & "  glt_oid, " _
                           & "  glt_dom_id, " _
                           & "  glt_en_id, " _
                           & "  glt_add_by, " _
                           & "  glt_add_date, " _
                           & "  glt_upd_by, " _
                           & "  glt_upd_date, " _
                           & "  glt_gl_oid, " _
                           & "  glt_code, " _
                           & "  glt_date, " _
                           & "  glt_type, " _
                           & "  glt_cu_id, " _
                           & "  glt_exc_rate, " _
                           & "  glt_seq, " _
                           & "  glt_ac_id, " _
                           & "  ac_sign,ac_code_hirarki, " _
                           & "  coalesce(ac_priority,99) as ac_priority , " _
                           & "  glt_sb_id, " _
                           & "  glt_cc_id, " _
                           & "  glt_desc, " _
                           & "  glt_debit, " _
                           & "  glt_credit, " _
                           & "  glt_ref_tran_id, " _
                           & "  glt_ref_trans_code, " _
                           & "  glt_posted, " _
                           & "  glt_dt, " _
                           & "  glt_daybook, " _
                           & "  glt_ref_oid, " _
                           & "  coalesce(glt_is_reverse,'N') as glt_is_reverse, " _
                           & "  glt_desc_detail, " _
                           & "  glt_ref_detail_no, " _
                           & "  glt_is_gen_ros, " _
                           & "  'N' as _empty " _
                           & "FROM  " _
                           & "  public.glt_det " _
                           & "  INNER JOIN public.ac_mstr ON (public.glt_det.glt_ac_id = public.ac_mstr.ac_id) " _
                           & "  where glt_date >= " & SetDate(_gcal_start_date) _
                           & "  and glt_date <= " & SetDate(_gcal_end_date) _
                           & "   and glt_type<>'JE' and (( glt_debit <> 0 and glt_credit = 0 ) or ( glt_debit = 0 and glt_credit <> 0 ))   " _
                           & " order by glt_date,  glt_code,  glt_credit, glt_debit,glt_seq  "
                    End If


                    .InitializeCommand()
                    .FillDataSet(ds_glt, "list_glt")
                    .FillDataSet(ds_group_glt, "list_glt_group")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim dr As DataRow
        Dim _glt_code As String = ""
        Dim ssqls As New ArrayList

        If ds_glt.Tables(0).Rows.Count > 0 Then
            _glt_code = ds_glt.Tables(0).Rows(0).Item("glt_code")
        End If

        ds_group_glt.Tables(0).Clear()

        For i As Integer = 0 To ds_glt.Tables(0).Rows.Count - 1
            LblStatus.Text = i + 1 & " of " & ds_glt.Tables(0).Rows.Count & " " & ds_glt.Tables(0).Rows(i).Item("glt_code")
            System.Windows.Forms.Application.DoEvents()

            If _glt_code = ds_glt.Tables(0).Rows(i).Item("glt_code") Then
                dr = ds_group_glt.Tables(0).NewRow
                dr("glt_code") = ds_glt.Tables(0).Rows(i).Item("glt_code")
                dr("glt_date") = ds_glt.Tables(0).Rows(i).Item("glt_date")
                dr("glt_exc_rate") = ds_glt.Tables(0).Rows(i).Item("glt_exc_rate")
                dr("glt_ac_id") = ds_glt.Tables(0).Rows(i).Item("glt_ac_id")
                dr("ac_sign") = ds_glt.Tables(0).Rows(i).Item("ac_sign")
                dr("ac_priority") = ds_glt.Tables(0).Rows(i).Item("ac_priority")
                dr("glt_debit") = ds_glt.Tables(0).Rows(i).Item("glt_debit")
                dr("glt_credit") = ds_glt.Tables(0).Rows(i).Item("glt_credit")
                dr("glt_is_reverse") = ds_glt.Tables(0).Rows(i).Item("glt_is_reverse")
                dr("ac_code_hirarki") = ds_glt.Tables(0).Rows(i).Item("ac_code_hirarki")
                dr("_empty") = ds_glt.Tables(0).Rows(i).Item("_empty")
                ds_group_glt.Tables(0).Rows.Add(dr)

                'jika pada looping terakhir
                If i = ds_glt.Tables(0).Rows.Count - 1 Then
                    ds_group_glt.Tables(0).AcceptChanges()

                    Try
                        Using objinsert As New master_new.WDABasepgsql("", "")
                            With objinsert
                                .Connection.Open()
                                Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                                Try
                                    .Command = .Connection.CreateCommand
                                    .Command.Transaction = sqlTran

                                    If ros_allocation(ssqls, objinsert, ds_group_glt) = False Then
                                        sqlTran.Rollback()
                                        Exit Sub
                                    End If

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
                End If

            Else
                ds_group_glt.Tables(0).AcceptChanges()

                Try
                    Using objinsert As New master_new.WDABasepgsql("", "")
                        With objinsert
                            .Connection.Open()
                            Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                            Try
                                .Command = .Connection.CreateCommand
                                .Command.Transaction = sqlTran

                                If ros_allocation(ssqls, objinsert, ds_group_glt) = False Then
                                    sqlTran.Rollback()
                                    Exit Sub
                                End If

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

                _glt_code = ds_glt.Tables(0).Rows(i).Item("glt_code")
                ds_group_glt.Tables(0).Clear()
                dr = ds_group_glt.Tables(0).NewRow
                dr("glt_code") = ds_glt.Tables(0).Rows(i).Item("glt_code")
                dr("glt_date") = ds_glt.Tables(0).Rows(i).Item("glt_date")
                dr("glt_exc_rate") = ds_glt.Tables(0).Rows(i).Item("glt_exc_rate")
                dr("glt_ac_id") = ds_glt.Tables(0).Rows(i).Item("glt_ac_id")
                dr("ac_sign") = ds_glt.Tables(0).Rows(i).Item("ac_sign")
                dr("ac_priority") = ds_glt.Tables(0).Rows(i).Item("ac_priority")


                dr("glt_debit") = ds_glt.Tables(0).Rows(i).Item("glt_debit")
                dr("glt_credit") = ds_glt.Tables(0).Rows(i).Item("glt_credit")

                dr("glt_is_reverse") = ds_glt.Tables(0).Rows(i).Item("glt_is_reverse")
                dr("ac_code_hirarki") = ds_glt.Tables(0).Rows(i).Item("ac_code_hirarki")
                dr("_empty") = ds_glt.Tables(0).Rows(i).Item("_empty")
                ds_group_glt.Tables(0).Rows.Add(dr)

                'jika pada looping terakhir 
                If i = ds_glt.Tables(0).Rows.Count - 1 Then
                    ds_group_glt.Tables(0).AcceptChanges()

                    Try
                        Using objinsert As New master_new.WDABasepgsql("", "")
                            With objinsert
                                .Connection.Open()
                                Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                                Try
                                    .Command = .Connection.CreateCommand
                                    .Command.Transaction = sqlTran

                                    If ros_allocation(ssqls, objinsert, ds_group_glt) = False Then
                                        sqlTran.Rollback()
                                        Exit Sub
                                    End If

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
                End If

            End If

        Next

        If dt_rule.Rows.Count > 0 Then
            MsgBox("Succesfull generate with unavailable rule " & dt_rule.Rows.Count & " rows")
            Dim frm As New FShow
            frm.fobject = Me
            frm.par_dt = dt_rule
            frm.ShowDialog()
        End If

        MsgBox("Generate Successful...", MsgBoxStyle.Information, "Data Generated")
    End Sub

    Private Function ros_allocation(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet) As Boolean
        'Dim _sign_priority As String
        Dim _temp_amount As Double
        Dim _ac_id1, _ac_id2 As Integer
        Dim _sign1, _sign2, _hirarki1, _hirarki2 As String
        Dim _rst_amount As Double
        'Dim _negatif_jurnal As Boolean = False

        ros_allocation = True
        'MessageBox.Show(par_ds.Tables(0).Rows(0).Item("glt_ac_id"), "")
        'MessageBox.Show(par_ds.Tables(0).Rows(0).Item("glt_debit"), "")

        'Dim frm As New FShow
        'frm.fobject = Me
        'frm.par_dt = par_ds.Tables(0)
        'frm.ShowDialog()


        Dim _jml_record As Integer = 0
        _jml_record = par_ds.Tables(0).Rows.Count

        LabelControl5.Text = _jml_record
        Windows.Forms.Application.DoEvents()

        'If par_ds.Tables(0).Rows(0).Item("glt_debit") <> 0 Then
        '    _sign_priority = "D"

        '    'If par_ds.Tables(0).Rows(0).Item("glt_debit") < 0 Then
        '    '    _negatif_jurnal = True
        '    'End If
        'Else
        '    _sign_priority = "C"

        '    'If par_ds.Tables(0).Rows(0).Item("glt_credit") < 0 Then
        '    '    _negatif_jurnal = True
        '    'End If
        'End If

        'MessageBox.Show(par_ds.Tables(0).Rows(0).Item("glt_credit"), "")
        'If _negatif_jurnal = True Then 'positifkan dulu
        '    For x As Integer = 0 To par_ds.Tables(0).Rows.Count - 1
        '        par_ds.Tables(0).Rows(x).Item("glt_debit") = par_ds.Tables(0).Rows(x).Item("glt_debit") * -1
        '        par_ds.Tables(0).Rows(x).Item("glt_credit") = par_ds.Tables(0).Rows(x).Item("glt_debit") * -1
        '    Next
        'End If

        'If _sign_priority = "D" Then
        For i As Integer = 0 To par_ds.Tables(0).Rows.Count - 1
            If par_ds.Tables(0).Rows(i).Item("glt_debit") <> 0 Then 'cari yang debit saja
                For j As Integer = 0 To par_ds.Tables(0).Rows.Count - 1

                    If par_ds.Tables(0).Rows(j).Item("glt_credit") <> 0 Then 'cari yang credit saja
                        If par_ds.Tables(0).Rows(i).Item("_empty") <> "Y" Then 'jika belum kosong

                            _temp_amount = par_ds.Tables(0).Rows(i).Item("glt_debit") - par_ds.Tables(0).Rows(j).Item("glt_credit")

                            'MessageBox.Show(par_ds.Tables(0).Rows(i).Item("glt_debit"), "")
                            'MessageBox.Show(par_ds.Tables(0).Rows(j).Item("glt_credit"), "")

                            If _temp_amount = 0 Then
                                _rst_amount = par_ds.Tables(0).Rows(i).Item("glt_debit") * par_ds.Tables(0).Rows(i).Item("glt_exc_rate")
                                par_ds.Tables(0).Rows(i).Item("glt_debit") = 0
                                par_ds.Tables(0).Rows(i).Item("_empty") = "Y"
                                par_ds.Tables(0).Rows(j).Item("glt_credit") = 0
                                par_ds.Tables(0).Rows(j).Item("_empty") = "Y"
                            ElseIf _temp_amount < 0 Then
                                _rst_amount = par_ds.Tables(0).Rows(i).Item("glt_debit") * par_ds.Tables(0).Rows(i).Item("glt_exc_rate")
                                par_ds.Tables(0).Rows(i).Item("glt_debit") = 0
                                par_ds.Tables(0).Rows(i).Item("_empty") = "Y"
                                par_ds.Tables(0).Rows(j).Item("glt_credit") = par_ds.Tables(0).Rows(j).Item("glt_credit") - par_ds.Tables(0).Rows(i).Item("glt_debit")

                            ElseIf _temp_amount > 0 Then
                                _rst_amount = par_ds.Tables(0).Rows(j).Item("glt_credit") * par_ds.Tables(0).Rows(j).Item("glt_exc_rate") 'glt_exc_rate
                                par_ds.Tables(0).Rows(i).Item("glt_debit") = _temp_amount
                                par_ds.Tables(0).Rows(j).Item("glt_credit") = 0
                                par_ds.Tables(0).Rows(j).Item("_empty") = "Y"
                            End If
                            'frm.fobject = Me
                            'frm.par_dt = par_ds.Tables(0)
                            'frm.ShowDialog()

                            ' ''If _negatif_jurnal = True Then 'kembalikan ke negatif lagi
                            ' ''    _rst_amount = _rst_amount * -1
                            ' ''End If

                            '**update rosetta-nya langsung 2 untuk debit credit
                            'If par_ds.Tables(0).Rows(i).Item("ac_sign") = "C" Then
                            '    _rst_amount1 = _rst_amount * -1
                            'Else
                            '    _rst_amount1 = _rst_amount
                            'End If

                            _ac_id1 = par_ds.Tables(0).Rows(i).Item("glt_ac_id")
                            _hirarki1 = par_ds.Tables(0).Rows(i).Item("ac_code_hirarki")
                            _sign1 = par_ds.Tables(0).Rows(i).Item("ac_sign")

                            _ac_id2 = par_ds.Tables(0).Rows(j).Item("glt_ac_id")
                            _hirarki2 = par_ds.Tables(0).Rows(j).Item("ac_code_hirarki")
                            _sign2 = par_ds.Tables(0).Rows(j).Item("ac_sign")

                            'kirim untuk dialokasikan ke rosetta
                            If ros_allocation_post(par_ssqls, par_obj, _ac_id1, _sign1, _hirarki1, _ac_id2, _sign2, _hirarki2, _rst_amount, par_ds.Tables(0).Rows(i).Item("glt_code")) = False Then
                                Return False
                                Exit Function
                            End If

                            'If par_ds.Tables(0).Rows(j).Item("ac_sign") = "D" Then
                            '    _rst_amount2 = _rst_amount * -1
                            'Else
                            '    _rst_amount2 = _rst_amount
                            'End If

                            '_ac_id1 = par_ds.Tables(0).Rows(j).Item("glt_ac_id")
                            '_sign1 = "C"
                            '_ac_id2 = par_ds.Tables(0).Rows(i).Item("glt_ac_id")
                            '_sign2 = "D"

                            ''kirim untuk dialokasikan ke rosetta
                            'If ros_allocation_post(par_ssqls, par_obj, _ac_id1, _sign1, _ac_id2, _sign2, _rst_amount2, par_ds.Tables(0).Rows(i).Item("glt_code")) = False Then
                            '    Return False
                            '    Exit Function
                            'End If

                            '******************************************************

                        End If
                    End If

                Next
            End If
        Next



        'For Each dr As DataRow In par_ds.Tables(0).Rows
        '    If dr("_empty") = "N" Then

        '        frm.fobject = Me
        '        frm.par_dt = par_ds.Tables(0)
        '        frm.ShowDialog()
        '        Exit For
        '    End If
        'Next
        'ElseIf _sign_priority = "C" Then

        'For i As Integer = 0 To par_ds.Tables(0).Rows.Count - 1

        '    If par_ds.Tables(0).Rows(i).Item("glt_credit") <> 0 Then 'cari yang credit saja

        '        For j As Integer = 0 To par_ds.Tables(0).Rows.Count - 1

        '            If par_ds.Tables(0).Rows(j).Item("glt_debit") <> 0 Then 'cari yang debit saja

        '                If par_ds.Tables(0).Rows(i).Item("_empty") <> "Y" Then 'jika belum kosong

        '                    _temp_amount = par_ds.Tables(0).Rows(i).Item("glt_credit") - par_ds.Tables(0).Rows(j).Item("glt_debit")
        '                    'frm.fobject = Me
        '                    'frm.par_dt = par_ds.Tables(0)
        '                    'frm.ShowDialog()
        '                    If _temp_amount = 0 Then
        '                        _rst_amount = par_ds.Tables(0).Rows(i).Item("glt_credit")
        '                        par_ds.Tables(0).Rows(i).Item("glt_credit") = 0
        '                        par_ds.Tables(0).Rows(i).Item("_empty") = "Y"
        '                        par_ds.Tables(0).Rows(j).Item("glt_debit") = 0
        '                        par_ds.Tables(0).Rows(j).Item("_empty") = "Y"
        '                    ElseIf _temp_amount < 0 Then
        '                        _rst_amount = par_ds.Tables(0).Rows(i).Item("glt_credit")
        '                        par_ds.Tables(0).Rows(i).Item("glt_credit") = 0
        '                        par_ds.Tables(0).Rows(i).Item("_empty") = "Y"
        '                        par_ds.Tables(0).Rows(j).Item("glt_debit") = par_ds.Tables(0).Rows(j).Item("glt_debit") - par_ds.Tables(0).Rows(i).Item("glt_credit")
        '                    ElseIf _temp_amount > 0 Then
        '                        _rst_amount = par_ds.Tables(0).Rows(j).Item("glt_debit")
        '                        par_ds.Tables(0).Rows(i).Item("glt_credit") = _temp_amount
        '                        par_ds.Tables(0).Rows(j).Item("glt_debit") = 0
        '                        par_ds.Tables(0).Rows(j).Item("_empty") = "Y"
        '                    End If

        '                    If _negatif_jurnal = True Then 'kembalikan ke negatif lagi
        '                        _rst_amount = _rst_amount * -1
        '                    End If
        '                    'frm.fobject = Me
        '                    'frm.par_dt = par_ds.Tables(0)
        '                    'frm.ShowDialog()

        '                    '**update rosetta-nya langsung 2 untuk debit credit
        '                    If par_ds.Tables(0).Rows(i).Item("ac_sign") = "D" Then
        '                        _rst_amount1 = _rst_amount * -1
        '                    Else
        '                        _rst_amount1 = _rst_amount
        '                    End If

        '                    _ac_id1 = par_ds.Tables(0).Rows(i).Item("glt_ac_id")
        '                    _sign1 = "C"
        '                    _ac_id2 = par_ds.Tables(0).Rows(j).Item("glt_ac_id")
        '                    _sign2 = "D"

        '                    'kirim untuk dialokasikan ke rosetta
        '                    If ros_allocation_post(par_ssqls, par_obj, _ac_id1, _sign1, _ac_id2, _sign2, _rst_amount1, par_ds.Tables(0).Rows(i).Item("glt_code")) = False Then
        '                        Return False
        '                        Exit Function
        '                    End If

        '                    If par_ds.Tables(0).Rows(j).Item("ac_sign") = "C" Then
        '                        _rst_amount2 = _rst_amount * -1
        '                    Else
        '                        _rst_amount2 = _rst_amount
        '                    End If

        '                    _ac_id1 = par_ds.Tables(0).Rows(j).Item("glt_ac_id")
        '                    _sign1 = "D"
        '                    _ac_id2 = par_ds.Tables(0).Rows(i).Item("glt_ac_id")
        '                    _sign2 = "C"

        '                    'kirim untuk dialokasikan ke rosetta
        '                    If ros_allocation_post(par_ssqls, par_obj, _ac_id1, _sign1, _ac_id2, _sign2, _rst_amount2, par_ds.Tables(0).Rows(i).Item("glt_code")) = False Then
        '                        Return False
        '                        Exit Function
        '                    End If

        '                    '******************************************************

        '                End If
        '            End If

        '        Next
        '    End If
        'Next
        'End If

    End Function


    Private Function ros_allocation_post(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ac_id1 As Integer, ByVal par_sign1 As String, _
                                         ByVal par_hirarki1 As String, ByVal par_ac_id2 As Integer, ByVal par_sign2 As String, ByVal par_hirarki2 As String, _
                                         ByVal par_amount As Double, ByVal par_glt_code As String) As Boolean


        ros_allocation_post = True

        Dim _rosr_code As String = ""
        Dim _rosgr_code1, _rosgr_code2 As String
        Dim _is_laba_berjalan As String = ""
        Dim _acc_line As String = ""
        Dim _rule_available As Boolean = False
        Dim _value As Double = 0
        Dim _reverse As Boolean = False

        'Try
        '    Using objcek As New master_new.WDABasepgsql("", "")
        '        With objcek
        '            .Connection.Open()
        '            .Command = .Connection.CreateCommand
        '            .Command.CommandType = CommandType.Text
        '            .Command.CommandText = "select rstbal_oid, coalesce(account_name_mstr.code_usr_1,'N') as is_laba_berjalan, coalesce(account_line_mstr.code_usr_1,'N') as acc_line from rstbal_mstr " _
        '                                & "  INNER JOIN public.en_mstr ON (public.rstbal_mstr.rstbal_en_id = public.en_mstr.en_id)" _
        '                                & "  inner join gcal_mstr on gcal_oid = rstbal_gcal_oid " _
        '                                & "  INNER JOIN rstrule_mstr ON (rstrule_mstr.rstrule_oid = rstbal_mstr.rstbal_rstrule_oid)" _
        '                                & "  INNER JOIN rstruled_det ON (rstruled_det.rstruled_rstrule_oid  = rstrule_mstr.rstrule_oid)" _
        '                                & "  INNER JOIN code_mstr account_name_mstr on account_name_mstr.code_id = rstrule_name_id " _
        '                                & "  INNER JOIN code_mstr account_line_mstr on account_line_mstr.code_id = rstrule_line_id " _
        '                                & " where  rstbal_en_id=1 and  rstbal_gcal_oid = " & SetSetring(le_periode.EditValue) _
        '                                & " and rstruled_ac_id1 = " & par_ac_id1 _
        '                                & " and rstruled_sign1 = " & SetSetring(par_sign1) _
        '                                & " and ((rstruled_ac_id2 = " & par_ac_id2 & ") or (coalesce(rstruled_ac_id2,0)=0))" _
        '                                & " and ((rstruled_sign2 = " & SetSetring(par_sign2) & ") or (coalesce(rstruled_sign2,'')=''))"

        '            .InitializeCommand()
        '            .DataReader = .Command.ExecuteReader
        '            While .DataReader.Read
        '                _rstbal_oid = .DataReader("rstbal_oid")
        '                _is_laba_berjalan = .DataReader("is_laba_berjalan")
        '                _acc_line = .DataReader("acc_line")
        '                _rule_available = True
        '            End While
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try

        _rosgr_code1 = ""
        _rosgr_code2 = ""


        For Each dr_trans As DataRow In dt_trans_rule.Rows
            If dr_trans("rosr_ac_hirarki_debit") = Microsoft.VisualBasic.Left(par_hirarki1, dr_trans("rosr_ac_hirarki_debit").ToString.Length) Then
                If dr_trans("rosr_ac_hirarki_credit") = Microsoft.VisualBasic.Left(par_hirarki2, dr_trans("rosr_ac_hirarki_credit").ToString.Length) Then
                    _rosr_code = dr_trans("rosr_rost_code").ToString
                    _value = dr_trans("rosr_value")
                    Exit For
                End If
            End If
        Next

        If _rosr_code = "" Then
            For Each dr_trans As DataRow In dt_trans_rule.Rows
                If dr_trans("rosr_ac_hirarki_debit") = Microsoft.VisualBasic.Left(par_hirarki2, dr_trans("rosr_ac_hirarki_debit").ToString.Length) Then
                    If dr_trans("rosr_ac_hirarki_credit") = Microsoft.VisualBasic.Left(par_hirarki1, dr_trans("rosr_ac_hirarki_credit").ToString.Length) Then
                        _rosr_code = dr_trans("rosr_rost_code").ToString
                        _value = dr_trans("rosr_value") * -1
                        Exit For
                    End If
                End If
            Next

            If _rosr_code = "" Then
                _rule_available = False
            Else
                _rule_available = True
            End If


        Else
            _rule_available = True
        End If


        If _rule_available = False Then
            'MsgBox("Rule Account Id " & par_ac_id1 & " " & master_new.PGSqlConn.GetRowInfo("select ac_code from ac_mstr where ac_id=" _
            '      & par_ac_id1)(0) & " " & master_new.PGSqlConn.GetRowInfo("select ac_name from ac_mstr where ac_id=" _
            '      & par_ac_id1)(0) & " (" & par_sign1 & ") for Account Id " & par_ac_id2 & " " & master_new.PGSqlConn.GetRowInfo("select ac_code from ac_mstr where ac_id=" _
            '      & par_ac_id2)(0) & " " & master_new.PGSqlConn.GetRowInfo("select ac_name from ac_mstr where ac_id=" _
            '      & par_ac_id2)(0) & "(" & par_sign2 & "), Unavailable", MsgBoxStyle.Critical, "Allocation Error")
            'Return False

            Dim _row As DataRow
            _row = dt_rule.NewRow

            _row("ac_id1") = par_ac_id1
            _row("ac_code1") = master_new.PGSqlConn.GetRowInfo("select ac_code from ac_mstr where ac_id=" & par_ac_id1)(0)
            _row("ac_name1") = master_new.PGSqlConn.GetRowInfo("select ac_name from ac_mstr where ac_id=" & par_ac_id1)(0)
            _row("ac_sign1") = "D"
            _row("ac_hirarki1") = par_hirarki1

            _row("ac_id2") = par_ac_id2
            _row("ac_code2") = master_new.PGSqlConn.GetRowInfo("select ac_code from ac_mstr where ac_id=" & par_ac_id2)(0)
            _row("ac_name2") = master_new.PGSqlConn.GetRowInfo("select ac_name from ac_mstr where ac_id=" & par_ac_id2)(0)
            _row("ac_sign2") = "C"
            _row("ac_hirarki2") = par_hirarki2
            dt_rule.Rows.Add(_row)

            dt_rule.AcceptChanges()

            Return True
            Exit Function

        End If

        'If _is_laba_berjalan = "Y" And _acc_line = "E" Then
        '    par_amount = par_amount * -1
        'End If


        For Each dr_gr As DataRow In dt_group_rule.Rows
            If dr_gr("rosgr_ac_hirarki") = Microsoft.VisualBasic.Left(par_hirarki1, dr_gr("rosgr_ac_hirarki").ToString.Length) Then
                _rosgr_code1 = dr_gr("rosgr_rosg_code").ToString
                Exit For
            End If
        Next

        For Each dr_gr As DataRow In dt_group_rule.Rows
            If dr_gr("rosgr_ac_hirarki") = Microsoft.VisualBasic.Left(par_hirarki2, dr_gr("rosgr_ac_hirarki").ToString.Length) Then
                _rosgr_code2 = dr_gr("rosgr_rosg_code").ToString
                Exit For
            End If
        Next

        Dim _amount As Double = 0
        With par_obj
            Try

                '.Command.CommandText = "UPDATE  " _
                '                    & "  public.rstbal_mstr   " _
                '                    & "SET  " _
                '                    & "  rstbal_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                '                    & "  rstbal_upd_date = current_timestamp,  " _
                '                    & "  rstbal_amount = coalesce(rstbal_amount,0) + " & SetDbl(par_amount) & ",  " _
                '                    & "  rstbal_dt = current_timestamp  " _
                '                    & "  " _
                '                    & "WHERE  " _
                '                    & "  rstbal_oid = " & SetSetring(_rstbal_oid) & " "

                _amount = par_amount

                If par_sign1 <> "D" Then
                    _amount = _amount * -1
                End If

                _amount = _amount * _value


                .Command.CommandType = CommandType.Text
                .Command.CommandText = "INSERT INTO  " _
                                    & "  public.rosd_data " _
                                    & "( " _
                                    & "  rosd_oid, " _
                                    & "  rosd_rosg_code, " _
                                    & "  rosd_amount, " _
                                    & "  rosd_rost_code, " _
                                    & "  rosd_periode " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(_rosgr_code1) & ",  " _
                                    & SetDec(_amount) & ",  " _
                                    & SetSetring(_rosr_code) & ",  " _
                                    & SetSetring(le_periode.EditValue) & "  " _
                                    & ")"

                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()

                _seq += 1
                .Command.CommandType = CommandType.Text
                .Command.CommandText = "INSERT INTO  " _
                                & "  public.rosh_history " _
                                & "( " _
                                & "  rosh_oid, " _
                                & "   " _
                                & "  rosh_seg, " _
                                & "  rosh_rosg_code, " _
                                & "  rosh_rost_code, " _
                                & "  rosh_periode,rosh_ac_id1,rosh_ac_id2,rosh_hirarki1,rosh_hirarki2,rosh_glt_code, " _
                                & "  rosh_amount " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetInteger(_seq) & ",  " _
                                & SetSetring(_rosgr_code1) & ",  " _
                                & SetSetring(_rosr_code) & ",  " _
                                & SetSetring(le_periode.EditValue) & ",  " _
                                & SetSetring(par_ac_id1) & ",  " _
                                & SetSetring(par_ac_id2) & ",  " _
                                & SetSetring(par_hirarki1) & ",  " _
                                & SetSetring(par_hirarki2) & ",  " _
                                & SetSetring(par_glt_code) & ",  " _
                                & SetDec(_amount) & "  " _
                                & ")"

                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()

                _amount = par_amount

                If par_sign2 <> "C" Then
                    _amount = _amount * -1
                End If

                _amount = _amount '* _value

                .Command.CommandType = CommandType.Text
                .Command.CommandText = "INSERT INTO  " _
                                    & "  public.rosd_data " _
                                    & "( " _
                                    & "  rosd_oid, " _
                                    & "  rosd_rosg_code, " _
                                    & "  rosd_amount, " _
                                    & "  rosd_rost_code, " _
                                    & "  rosd_periode " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(_rosgr_code2) & ",  " _
                                    & SetDec(_amount) & ",  " _
                                    & SetSetring(_rosr_code) & ",  " _
                                    & SetSetring(le_periode.EditValue) & "  " _
                                    & ")"

                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()

                _seq += 1

                .Command.CommandType = CommandType.Text
                .Command.CommandText = "INSERT INTO  " _
                                & "  public.rosh_history " _
                                & "( " _
                                & "  rosh_oid, " _
                                & "   " _
                                & "  rosh_seg, " _
                                & "  rosh_rosg_code, " _
                                & "  rosh_rost_code, " _
                                & "  rosh_periode,rosh_ac_id1,rosh_ac_id2,rosh_hirarki1,rosh_hirarki2,rosh_glt_code, " _
                                & "  rosh_amount " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetInteger(_seq) & ",  " _
                                & SetSetring(_rosgr_code2) & ",  " _
                                & SetSetring(_rosr_code) & ",  " _
                                & SetSetring(le_periode.EditValue) & ",  " _
                                & SetSetring(par_ac_id1) & ",  " _
                                & SetSetring(par_ac_id2) & ",  " _
                                & SetSetring(par_hirarki1) & ",  " _
                                & SetSetring(par_hirarki2) & ",  " _
                                 & SetSetring(par_glt_code) & ",  " _
                                & SetDec(_amount) & "  " _
                                & ")"

                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()


                .Command.CommandType = CommandType.Text
                .Command.CommandText = "UPDATE  " _
                                    & "  public.glt_det   " _
                                    & "SET  " _
                                    & "  glt_is_gen_ros = 'Y' " _
                                    & " WHERE  " _
                                    & "  glt_code = " & SetSetring(par_glt_code.ToString) & " "

                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()

            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With

    End Function

    Private Function ros_allocation_post(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ac_id1 As Integer, ByVal par_sign1 As String, ByVal par_ac_id2 As Integer, ByVal par_sign2 As String, ByVal par_amount As Double, ByVal par_glt_code As String) As Boolean
        ros_allocation_post = True

        Dim _rstbal_oid As String = ""
        Dim _is_laba_berjalan As String = ""
        Dim _acc_line As String = ""
        Dim _rule_available As Boolean = False

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select rstbal_oid, coalesce(account_name_mstr.code_usr_1,'N') as is_laba_berjalan, coalesce(account_line_mstr.code_usr_1,'N') as acc_line from rstbal_mstr " _
                                        & "  INNER JOIN public.en_mstr ON (public.rstbal_mstr.rstbal_en_id = public.en_mstr.en_id)" _
                                        & "  inner join gcal_mstr on gcal_oid = rstbal_gcal_oid " _
                                        & "  INNER JOIN rstrule_mstr ON (rstrule_mstr.rstrule_oid = rstbal_mstr.rstbal_rstrule_oid)" _
                                        & "  INNER JOIN rstruled_det ON (rstruled_det.rstruled_rstrule_oid  = rstrule_mstr.rstrule_oid)" _
                                        & "  INNER JOIN code_mstr account_name_mstr on account_name_mstr.code_id = rstrule_name_id " _
                                        & "  INNER JOIN code_mstr account_line_mstr on account_line_mstr.code_id = rstrule_line_id " _
                                        & " where  rstbal_en_id=1 and  rstbal_gcal_oid = " & SetSetring(le_periode.EditValue) _
                                        & " and rstruled_ac_id1 = " & par_ac_id1 _
                                        & " and rstruled_sign1 = " & SetSetring(par_sign1) _
                                        & " and ((rstruled_ac_id2 = " & par_ac_id2 & ") or (coalesce(rstruled_ac_id2,0)=0))" _
                                        & " and ((rstruled_sign2 = " & SetSetring(par_sign2) & ") or (coalesce(rstruled_sign2,'')=''))"

                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _rstbal_oid = .DataReader("rstbal_oid").ToString
                        _is_laba_berjalan = .DataReader("is_laba_berjalan")
                        _acc_line = .DataReader("acc_line")
                        _rule_available = True
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If _rule_available = False Then
            'MsgBox("Rule Account Id " & par_ac_id1 & " " & master_new.PGSqlConn.GetRowInfo("select ac_code from ac_mstr where ac_id=" _
            '      & par_ac_id1)(0) & " " & master_new.PGSqlConn.GetRowInfo("select ac_name from ac_mstr where ac_id=" _
            '      & par_ac_id1)(0) & " (" & par_sign1 & ") for Account Id " & par_ac_id2 & " " & master_new.PGSqlConn.GetRowInfo("select ac_code from ac_mstr where ac_id=" _
            '      & par_ac_id2)(0) & " " & master_new.PGSqlConn.GetRowInfo("select ac_name from ac_mstr where ac_id=" _
            '      & par_ac_id2)(0) & "(" & par_sign2 & "), Unavailable", MsgBoxStyle.Critical, "Allocation Error")
            'Return False

            Dim _row As DataRow
            _row = dt_rule.NewRow

            _row("ac_id1") = par_ac_id1
            _row("ac_code1") = master_new.PGSqlConn.GetRowInfo("select ac_code from ac_mstr where ac_id=" & par_ac_id1)(0)
            _row("ac_name1") = master_new.PGSqlConn.GetRowInfo("select ac_name from ac_mstr where ac_id=" & par_ac_id1)(0)
            _row("ac_sign1") = par_sign1
            _row("ac_id2") = par_ac_id2
            _row("ac_code2") = master_new.PGSqlConn.GetRowInfo("select ac_code from ac_mstr where ac_id=" & par_ac_id2)(0)
            _row("ac_name2") = master_new.PGSqlConn.GetRowInfo("select ac_name from ac_mstr where ac_id=" & par_ac_id2)(0)
            _row("ac_sign2") = par_sign2

            dt_rule.Rows.Add(_row)

            dt_rule.AcceptChanges()

        End If

        If _is_laba_berjalan = "Y" And _acc_line = "E" Then
            par_amount = par_amount * -1
        End If

        With par_obj
            Try
                .Command.CommandType = CommandType.Text
                .Command.CommandText = "UPDATE  " _
                                    & "  public.rstbal_mstr   " _
                                    & "SET  " _
                                    & "  rstbal_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & "  rstbal_upd_date = current_timestamp,  " _
                                    & "  rstbal_amount = coalesce(rstbal_amount,0) + " & SetDbl(par_amount) & ",  " _
                                    & "  rstbal_dt = current_timestamp  " _
                                    & "  " _
                                    & "WHERE  " _
                                    & "  rstbal_oid = " & SetSetring(_rstbal_oid) & " "

                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()


                .Command.CommandType = CommandType.Text
                .Command.CommandText = "INSERT INTO  " _
                                & "  public.rsthistory_mstr " _
                                & "( " _
                                & "  rsthistory_oid, " _
                                & "  rsthistory_rstbal_oid,rtshistory_rstbal_gcal_oid, " _
                                & "  rsthistory_glt_code, " _
                                & "  rsthistory_amount, " _
                                & "  rsthistory_ac_id1, " _
                                & "  rsthistory_ac_sign1, " _
                                & "  rsthistory_ac_id2, " _
                                & "  rsthistory_ac_sign2 " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(_rstbal_oid) & ",  " _
                                & SetSetring(le_periode.EditValue) & ",  " _
                                & SetSetring(par_glt_code) & ",  " _
                                & SetDec(par_amount) & ",  " _
                                & SetInteger(par_ac_id1) & ",  " _
                                & SetSetring(par_sign1) & ",  " _
                                & SetInteger(par_ac_id2) & ",  " _
                                & SetSetring(par_sign2) & "  " _
                                & ")"

                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()


                .Command.CommandType = CommandType.Text
                .Command.CommandText = "UPDATE  " _
                                    & "  public.glt_det   " _
                                    & "SET  " _
                                    & "  glt_is_gen_ros = 'Y' " _
                                    & " WHERE  " _
                                    & "  glt_code = " & SetSetring(par_glt_code.ToString) & " "

                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()

            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With

    End Function


    'Public Overrides Function export_data() As Boolean
    '    Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls")
    '    If fileName <> "" Then
    '        pgc_master.ExportToXls(fileName)
    '        OpenFile(fileName)
    '    End If
    'End Function

    Private Sub le_periode_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'te_update_by.Text = ""
        'te_update_date.Text = ""
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select rstbal_upd_by, coalesce(rstbal_upd_date,'01/01/1900') as rstbal_upd_date from rstbal_mstr " _
                                           & " where rstbal_gcal_oid = " + SetSetring(le_periode.EditValue) _
                                           & " and rstbal_en_id = " + SetInteger(le_entity.EditValue) _
                                           & "  order by rstbal_upd_date desc limit 1 "
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        Try
                            'te_update_by.Text = .DataReader("rstbal_upd_by")
                            'te_update_date.Text = .DataReader("rstbal_upd_date")
                        Catch
                        End Try
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub sb_gen_continue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_gen_continue.Click

        'cek opening balance
        Dim _gcal_available As Boolean = False
        Dim _gcal_start_date As Date
        Dim _gcal_end_date As Date

        Try
            sSQL = "select 0 as ac_id1,'' as ac_code1,'' as ac_name1,'' as ac_sign1,0 as ac_id2,'' as ac_code2,'' as ac_name2,'' as ac_sign2"
            dt_rule = master_new.PGSqlConn.GetTableData(sSQL)

            dt_rule.Rows.Clear()

            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand

                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select rstbal_oid, gcal_start_date, gcal_end_date  from rstbal_mstr inner join gcal_mstr on gcal_oid = rstbal_gcal_oid " + _
                                           " where gcal_start_date <= " + SetDate(le_periode.Text) + _
                                           " and gcal_end_date >= " + SetDate(le_periode.Text)

                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader

                    While .DataReader.Read
                        _gcal_available = True
                        _gcal_start_date = CDate(.DataReader("gcal_start_date"))
                        _gcal_end_date = CDate(.DataReader("gcal_end_date"))
                    End While

                    If _gcal_available = False Then
                        MessageBox.Show("Rosetta Openning Balance For This Periode doesn't exist :" + le_periode.Text.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If

                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        '-------------------------------------------------------

        'Try
        '    Using objinsert As New master_new.WDABasepgsql("", "")
        '        With objinsert
        '            .Connection.Open()
        '            Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
        '            Try
        '                .Command = .Connection.CreateCommand
        '                .Command.Transaction = sqlTran

        '                .Command.CommandType = CommandType.Text
        '                .Command.CommandText = "UPDATE  " _
        '                                    & "  public.rstbal_mstr   " _
        '                                    & "SET  " _
        '                                    & "  rstbal_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
        '                                    & "  rstbal_upd_date = current_timestamp,  " _
        '                                    & "  rstbal_amount = 0,  " _
        '                                    & "  rstbal_dt = current_timestamp  " _
        '                                    & "  " _
        '                                    & "WHERE  " _
        '                                    & "  rstbal_gcal_oid = " & SetSetring(le_periode.EditValue) & " "

        '                'par_ssqls.Add(.Command.CommandText)
        '                .Command.ExecuteNonQuery()
        '                .Command.Parameters.Clear()

        '                sqlTran.Commit()
        '            Catch ex As nPgSqlException
        '                sqlTran.Rollback()
        '                MessageBox.Show(ex.Message)
        '            End Try
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try

        'collecting data dari glt_det

        sSQL = "select * from ac_code_rosetta"

        Dim dt As New DataTable
        dt = master_new.PGSqlConn.GetTableData(sSQL)



        ds_glt = New DataSet
        ds_group_glt = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb

                    If dt.Rows.Count > 0 Then
                        .SQL = "SELECT  " _
                           & "  glt_oid, " _
                           & "  glt_dom_id, " _
                           & "  glt_en_id, " _
                           & "  glt_add_by, " _
                           & "  glt_add_date, " _
                           & "  glt_upd_by, " _
                           & "  glt_upd_date, " _
                           & "  glt_gl_oid, " _
                           & "  glt_code, " _
                           & "  glt_date, " _
                           & "  glt_type, " _
                           & "  glt_cu_id, " _
                           & "  glt_exc_rate, " _
                           & "  glt_seq, " _
                           & "  glt_ac_id, " _
                           & "  ac_sign, " _
                           & "  coalesce(ac_priority,99) as ac_priority , " _
                           & "  glt_sb_id, " _
                           & "  glt_cc_id, " _
                           & "  glt_desc, " _
                           & "  glt_debit, " _
                           & "  glt_credit, " _
                           & "  glt_ref_tran_id, " _
                           & "  glt_ref_trans_code, " _
                           & "  glt_posted, " _
                           & "  glt_dt, " _
                           & "  glt_daybook, " _
                           & "  glt_ref_oid, " _
                           & "  coalesce(glt_is_reverse,'N') as glt_is_reverse, " _
                           & "  glt_desc_detail, " _
                           & "  glt_ref_detail_no, " _
                           & "  glt_is_gen_ros, " _
                           & "  'N' as _empty " _
                           & "FROM  " _
                           & "  public.glt_det " _
                           & "  INNER JOIN public.ac_mstr ON (public.glt_det.glt_ac_id = public.ac_mstr.ac_id) " _
                           & "  where glt_date >= " & SetDate(_gcal_start_date) _
                           & "  and glt_date <= " & SetDate(_gcal_end_date) _
                           & "   and glt_type<>'JE'  and glt_code in (select * from ac_code_rosetta) and  coalesce(glt_is_gen_ros,'N') <> 'Y'  " _
                           & " order by glt_date, glt_code, ac_priority, glt_credit, glt_debit  "
                    Else
                        .SQL = "SELECT  " _
                           & "  glt_oid, " _
                           & "  glt_dom_id, " _
                           & "  glt_en_id, " _
                           & "  glt_add_by, " _
                           & "  glt_add_date, " _
                           & "  glt_upd_by, " _
                           & "  glt_upd_date, " _
                           & "  glt_gl_oid, " _
                           & "  glt_code, " _
                           & "  glt_date, " _
                           & "  glt_type, " _
                           & "  glt_cu_id, " _
                           & "  glt_exc_rate, " _
                           & "  glt_seq, " _
                           & "  glt_ac_id, " _
                           & "  ac_sign, " _
                           & "  coalesce(ac_priority,99) as ac_priority , " _
                           & "  glt_sb_id, " _
                           & "  glt_cc_id, " _
                           & "  glt_desc, " _
                           & "  glt_debit, " _
                           & "  glt_credit, " _
                           & "  glt_ref_tran_id, " _
                           & "  glt_ref_trans_code, " _
                           & "  glt_posted, " _
                           & "  glt_dt, " _
                           & "  glt_daybook, " _
                           & "  glt_ref_oid, " _
                           & "  coalesce(glt_is_reverse,'N') as glt_is_reverse, " _
                           & "  glt_desc_detail, " _
                           & "  glt_ref_detail_no, " _
                           & "  glt_is_gen_ros, " _
                           & "  'N' as _empty " _
                           & "FROM  " _
                           & "  public.glt_det " _
                           & "  INNER JOIN public.ac_mstr ON (public.glt_det.glt_ac_id = public.ac_mstr.ac_id) " _
                           & "  where glt_date >= " & SetDate(_gcal_start_date) _
                           & "  and glt_date <= " & SetDate(_gcal_end_date) _
                           & "   and glt_type<>'JE' and  coalesce(glt_is_gen_ros,'N') <> 'Y'  " _
                           & " order by glt_date, glt_code, ac_priority, glt_credit, glt_debit  "
                    End If


                    .InitializeCommand()
                    .FillDataSet(ds_glt, "list_glt")
                    .FillDataSet(ds_group_glt, "list_glt_group")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim dr As DataRow
        Dim _glt_code As String = ""
        Dim ssqls As New ArrayList

        If ds_glt.Tables(0).Rows.Count > 0 Then
            _glt_code = ds_glt.Tables(0).Rows(0).Item("glt_code")
        End If

        ds_group_glt.Tables(0).Clear()

        For i As Integer = 0 To ds_glt.Tables(0).Rows.Count - 1
            LblStatus.Text = i & " of " & ds_glt.Tables(0).Rows.Count - 1 & " " & ds_glt.Tables(0).Rows(i).Item("glt_code")
            System.Windows.Forms.Application.DoEvents()

            If _glt_code = ds_glt.Tables(0).Rows(i).Item("glt_code") Then
                dr = ds_group_glt.Tables(0).NewRow
                dr("glt_code") = ds_glt.Tables(0).Rows(i).Item("glt_code")
                dr("glt_date") = ds_glt.Tables(0).Rows(i).Item("glt_date")
                dr("glt_exc_rate") = ds_glt.Tables(0).Rows(i).Item("glt_exc_rate")
                dr("glt_ac_id") = ds_glt.Tables(0).Rows(i).Item("glt_ac_id")
                'MessageBox.Show(ds_glt.Tables(0).Rows(i).Item("glt_ac_id"), "")
                dr("ac_sign") = ds_glt.Tables(0).Rows(i).Item("ac_sign")
                dr("ac_priority") = ds_glt.Tables(0).Rows(i).Item("ac_priority")

                'untuk yang reverse dibalik kredit debit dan dinegatifkan
                'sys 20120827
                'If ds_glt.Tables(0).Rows(i).Item("glt_is_reverse") = "Y" Then
                '    dr("glt_debit") = ds_glt.Tables(0).Rows(i).Item("glt_credit") '* -1 sys 20120827
                '    dr("glt_credit") = ds_glt.Tables(0).Rows(i).Item("glt_debit") '* -1 sys 20120827
                'Else
                '    dr("glt_debit") = ds_glt.Tables(0).Rows(i).Item("glt_debit")
                '    dr("glt_credit") = ds_glt.Tables(0).Rows(i).Item("glt_credit")
                'End If

                dr("glt_debit") = ds_glt.Tables(0).Rows(i).Item("glt_debit")
                dr("glt_credit") = ds_glt.Tables(0).Rows(i).Item("glt_credit")
                dr("glt_is_reverse") = ds_glt.Tables(0).Rows(i).Item("glt_is_reverse")

                dr("_empty") = ds_glt.Tables(0).Rows(i).Item("_empty")
                ds_group_glt.Tables(0).Rows.Add(dr)

                'jika pada looping terakhir
                If i = ds_glt.Tables(0).Rows.Count - 1 Then
                    ds_group_glt.Tables(0).AcceptChanges()

                    Try
                        Using objinsert As New master_new.WDABasepgsql("", "")
                            With objinsert
                                .Connection.Open()
                                Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                                Try
                                    .Command = .Connection.CreateCommand
                                    .Command.Transaction = sqlTran

                                    If ros_allocation(ssqls, objinsert, ds_group_glt) = False Then
                                        sqlTran.Rollback()
                                        Exit Sub
                                    End If

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
                End If

            Else
                ds_group_glt.Tables(0).AcceptChanges()

                Try
                    Using objinsert As New master_new.WDABasepgsql("", "")
                        With objinsert
                            .Connection.Open()
                            Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                            Try
                                .Command = .Connection.CreateCommand
                                .Command.Transaction = sqlTran

                                If ros_allocation(ssqls, objinsert, ds_group_glt) = False Then
                                    sqlTran.Rollback()
                                    Exit Sub
                                End If

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

                _glt_code = ds_glt.Tables(0).Rows(i).Item("glt_code")
                ds_group_glt.Tables(0).Clear()
                dr = ds_group_glt.Tables(0).NewRow
                dr("glt_code") = ds_glt.Tables(0).Rows(i).Item("glt_code")
                dr("glt_date") = ds_glt.Tables(0).Rows(i).Item("glt_date")
                dr("glt_exc_rate") = ds_glt.Tables(0).Rows(i).Item("glt_exc_rate")
                dr("glt_ac_id") = ds_glt.Tables(0).Rows(i).Item("glt_ac_id")
                dr("ac_sign") = ds_glt.Tables(0).Rows(i).Item("ac_sign")
                dr("ac_priority") = ds_glt.Tables(0).Rows(i).Item("ac_priority")

                'sys 20120827
                'untuk yang reverse dibalik kredit debit dan dinegatifkan
                'If ds_glt.Tables(0).Rows(i).Item("glt_is_reverse") = "Y" Then
                '    dr("glt_debit") = ds_glt.Tables(0).Rows(i).Item("glt_credit") '* -1 sys 20120827
                '    dr("glt_credit") = ds_glt.Tables(0).Rows(i).Item("glt_debit") '* -1 sys 20120827
                'Else
                '    dr("glt_debit") = ds_glt.Tables(0).Rows(i).Item("glt_debit")
                '    dr("glt_credit") = ds_glt.Tables(0).Rows(i).Item("glt_credit")
                'End If

                dr("glt_debit") = ds_glt.Tables(0).Rows(i).Item("glt_debit")
                dr("glt_credit") = ds_glt.Tables(0).Rows(i).Item("glt_credit")

                dr("glt_is_reverse") = ds_glt.Tables(0).Rows(i).Item("glt_is_reverse")
                dr("_empty") = ds_glt.Tables(0).Rows(i).Item("_empty")
                ds_group_glt.Tables(0).Rows.Add(dr)

                'jika pada looping terakhir 
                If i = ds_glt.Tables(0).Rows.Count - 1 Then
                    ds_group_glt.Tables(0).AcceptChanges()

                    Try
                        Using objinsert As New master_new.WDABasepgsql("", "")
                            With objinsert
                                .Connection.Open()
                                Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                                Try
                                    .Command = .Connection.CreateCommand
                                    .Command.Transaction = sqlTran

                                    If ros_allocation(ssqls, objinsert, ds_group_glt) = False Then
                                        sqlTran.Rollback()
                                        Exit Sub
                                    End If

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
                End If

            End If

        Next

        If dt_rule.Rows.Count > 0 Then
            MsgBox("Succesfull generate with unavailable rule " & dt_rule.Rows.Count & " rows")
            Dim frm As New FShow
            frm.fobject = Me
            frm.par_dt = dt_rule
            frm.ShowDialog()
        End If

        MsgBox("Generate Successful...", MsgBoxStyle.Information, "Data Generated")
    End Sub

    Private Sub le_periode_EditValueChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_periode.EditValueChanged
        Try
            pr_tglAwal.EditValue = le_periode.GetColumnValue("gcal_start_date")
            pr_tglAkhir.EditValue = le_periode.GetColumnValue("gcal_end_date")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
