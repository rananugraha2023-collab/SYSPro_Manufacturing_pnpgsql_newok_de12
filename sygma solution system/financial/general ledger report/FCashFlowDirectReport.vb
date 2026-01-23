Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FCashFlowDirectReport
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _now As DateTime
    Dim ssql As String


    Private Sub FBalanceSheetReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            _now = func_coll.get_now
            de_first.DateTime = _now
            de_end.DateTime = _now

            dt_bantu = New DataTable
            dt_bantu = (func_data.load_dom_mstr())
            le_domain.Properties.DataSource = dt_bantu
            le_domain.Properties.DisplayMember = dt_bantu.Columns("dom_desc").ToString
            le_domain.Properties.ValueMember = dt_bantu.Columns("dom_id").ToString
            le_domain.ItemIndex = 0

            init_le(le_gcal, "gcal_mstr")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub le_entity_EditalueChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Public Overrides Sub preview()
        Dim level As Integer
        Dim posisi As String = ""
        Dim ssqls As New ArrayList
        Dim dt As New DataTable
        Try

            level = 1


            ssql = "SELECT  " _
                & "  a.code, " _
                & "  a.remark, " _
                & "  a.sort_number, " _
                & "  a.remark_header, " _
                & "  a.remark_footer, " _
                & "  a.cfsign_header, " _
                & "  a.cf_value_sign, " _
                & "  b.cfdet_oid, " _
                & "  b.cfdet_pk, " _
                & "  b.seq, 0.0 as cf_value_beginning,0.0 as cf_value_ending,b.sub_header2,  " _
                & "  b.sub_header,coalesce((select  " _
                & " sum(f_get_cfvalue_direct(d.ac_id,'" & le_gcal.EditValue.ToString & "',c.ac_sign,ac_value)) as _value " _
                & "from public.tconfsettingcashflowdet_item c " _
                & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
                & " where c.code=b.cfdet_pk and d.ac_is_sumlevel='N'),0) * ac_value_header as cf_value " _
                & "FROM " _
                & "  public.tconfsettingcashflow a " _
                & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
                & "WHERE " _
                & "  a.cfsign_header = 'T'  and cf_type='D'"


            Dim rpt As New rptDirectCashFlow
            With rpt
                Dim ds As New DataSet
                ds = ReportDataset(ssql)
                If ds.Tables(0).Rows.Count < 1 Then
                    Box("Maaf data kosong")
                    Exit Sub
                End If

                ssql = "SELECT  " _
                    & " sum((SELECT  " _
                    & "       sum(glbal_balance_open) AS jml " _
                    & "       FROM " _
                    & "       public.glbal_balance x " _
                    & "       WHERE " _
                    & "       x.glbal_gcal_oid = '" & le_gcal.EditValue.ToString & "' AND   " _
                    & "       x.glbal_ac_id = d.ac_id )) as total " _
                    & "FROM " _
                    & "  public.tconfsettingcashflow a " _
                    & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
                    & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code) " _
                    & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
                    & "WHERE " _
                    & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D' "

                Dim _awal, _akhir As Double
                _awal = 0
                _akhir = 0

                Dim dt_temp As New DataTable

                dt_temp = master_new.PGSqlConn.GetTableData(ssql)

                For Each dr As DataRow In dt_temp.Rows
                    _awal = dr("total")
                Next

                ssql = "SELECT  " _
                   & " sum((SELECT  " _
                   & "       sum(glbal_balance_open + glbal_balance_posted ) AS jml " _
                   & "       FROM " _
                   & "       public.glbal_balance x " _
                   & "       WHERE " _
                   & "       x.glbal_gcal_oid = '" & le_gcal.EditValue.ToString & "' AND   " _
                   & "       x.glbal_ac_id = d.ac_id )) as total " _
                   & "FROM " _
                   & "  public.tconfsettingcashflow a " _
                   & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
                   & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code) " _
                   & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
                   & "WHERE " _
                   & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D' "


                dt_temp = master_new.PGSqlConn.GetTableData(ssql)

                For Each dr As DataRow In dt_temp.Rows
                    _akhir = dr("total")
                Next


                For Each dr As DataRow In ds.Tables(0).Rows
                    dr("cf_value_beginning") = _awal
                    dr("cf_value_ending") = _akhir
                Next

                '.vtglawal = tanggal.ToString
                '.vtglakhir = EndOfMonth(tanggal, 0).ToString

                '.vlevel = level
                '.vdom = dom
                '.ven = en
                '.vsb = sb
                '.vcc = cc

                If Ce_Posting.EditValue = True Then
                    .Posting_Option = True
                Else
                    .Posting_Option = False
                End If

                .periode = de_end.DateTime.Date.ToString("dd MMMM yyyy")
                .DataSource = ds
                .DataMember = "Table"
                '.Parameters("PPeriode").Value = "PERIODE : " & de_first.EditValue & " " & de_end.EditValue
                '.Parameters("PPosisi").Value = posisi

                ' Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

                'printTool.PreviewForm.Text = "Direct Cash Flow Report"
                ''.PrintingSystem = ps
                'printTool.ShowPreview()


                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.PreviewForm.Text = "Balance Sheet Report"
                printTool.ShowPreview()

            End With
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub le_gcal_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_gcal.EditValueChanged
        Try
            de_first.EditValue = le_gcal.GetColumnValue("gcal_start_date")
            de_end.EditValue = le_gcal.GetColumnValue("gcal_end_date")

            If le_gcal.GetColumnValue("gcal_closing") = "Y" Then
                Ce_Posting.EditValue = True
            Else
                Ce_Posting.EditValue = False
            End If

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub BtGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtGenerate.Click
        Try
            ssql = "delete from cf_save where glt_periode='" & le_gcal.EditValue.ToString & "'"
            DbRun(ssql)

            ssql = "insert into cf_save select '" & le_gcal.EditValue.ToString _
                & "' as glt_periode, y.glt_code,y.glt_date, y.glt_ac_id,ac_code, " _
                & "ac_name,y.glt_desc,y.glt_debit * y.glt_exc_rate as glt_debit  ," _
                & "y.glt_credit * y.glt_exc_rate as glt_credit,glt_ref_trans_code,glt_oid  " _
                & "from glt_det y  inner join ac_mstr on (y.glt_ac_id=ac_id)  where y.glt_date between " _
                & SetDateNTime00(de_first.DateTime) & " and " & SetDateNTime00(de_end.DateTime) _
                & " and y.glt_code in ( " _
                & " select distinct x.glt_code from glt_det x where x.glt_date  between " _
                & SetDateNTime00(de_first.DateTime) & " and " & SetDateNTime00(de_end.DateTime) _
                & " and x.glt_ac_id in (SELECT  " _
                & "  d.ac_id " _
                & "FROM " _
                & "  public.tconfsettingcashflow a " _
                & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
                & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code) " _
                & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
                & "WHERE " _
                & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D') and (x.glt_debit + x.glt_credit) <>0 ) "

            DbRun(ssql)
            Box("Generate data success")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub BtCfDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtCfDetail.Click
        Dim level As Integer
        Dim posisi As String = ""
        Dim ssqls As New ArrayList
        Dim dt As New DataTable
        Try

            level = 1


            'ssql = "SELECT  " _
            '    & "  a.code, " _
            '    & "  a.remark, " _
            '    & "  a.sort_number, " _
            '    & "  a.remark_header, " _
            '    & "  a.remark_footer, " _
            '    & "  a.cfsign_header, " _
            '    & "  a.cf_value_sign, " _
            '    & "  b.cfdet_oid, " _
            '    & "  b.cfdet_pk, " _
            '    & "  b.seq, 0.0 as cf_value_beginning,0.0 as cf_value_ending,b.sub_header2,  " _
            '    & "  b.sub_header,coalesce((select  " _
            '    & " sum(f_get_cfvalue_direct(d.ac_id,'" & le_gcal.EditValue & "',c.ac_sign,ac_value)) as _value " _
            '    & "from public.tconfsettingcashflowdet_item c " _
            '    & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
            '    & " where c.code=b.cfdet_pk and d.ac_is_sumlevel='N'),0) * ac_value_header as cf_value " _
            '    & "FROM " _
            '    & "  public.tconfsettingcashflow a " _
            '    & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
            '    & "WHERE " _
            '    & "  a.cfsign_header = 'T'  and cf_type='D'"

            ssql = "SELECT  " _
                & "  a.code, " _
                & "  a.remark, " _
                & "  a.sort_number, " _
                & "  a.remark_header, " _
                & "  a.remark_footer, " _
                & "  a.cfsign_header, " _
                & "  a.cf_value_sign, " _
                & "  b.cfdet_oid, " _
                & "  b.cfdet_pk, " _
                & "  b.seq, " _
                & "  b.sub_header, " _
                & "  c.code as code_det, " _
                & "  c.seq as seq_det, " _
                & "  c.ac_hirarki, " _
                & "  d.ac_id,0.0 as cf_value_beginning,0.0 as cf_value_ending,b.sub_header2,b.ac_value_header,  " _
                & "  d.ac_code,coalesce(f_get_cfvalue_direct(d.ac_id,'" & le_gcal.EditValue.ToString & "',c.ac_sign,ac_value),0)  as cf_value_original, " _
                & "  d.ac_name,c.ac_sign,ac_value,coalesce(f_get_cfvalue_direct(d.ac_id,'" & le_gcal.EditValue.ToString & "',c.ac_sign,ac_value),0) * ac_value_header as cf_value " _
                & "FROM " _
                & "  public.tconfsettingcashflow a " _
                & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
                & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code) " _
                & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
                & "WHERE " _
                & "  a.cfsign_header = 'T' and d.ac_is_sumlevel='N' and cf_type='D' " _
                & "ORDER BY " _
                & "  a.sort_number, " _
                & "  b.seq,c.seq"


            Dim rpt As New rptDirectCashFlowDetail
            With rpt
                Dim ds As New DataSet
                ds = ReportDataset(ssql)
                If ds.Tables(0).Rows.Count < 1 Then
                    Box("Maaf data kosong")
                    Exit Sub
                End If
                .glt_periode = le_gcal.EditValue.ToString
                ssql = "SELECT  " _
                    & " sum((SELECT  " _
                    & "       sum(glbal_balance_open) AS jml " _
                    & "       FROM " _
                    & "       public.glbal_balance x " _
                    & "       WHERE " _
                    & "       x.glbal_gcal_oid = '" & le_gcal.EditValue.ToString & "' AND   " _
                    & "       x.glbal_ac_id = d.ac_id )) as total " _
                    & "FROM " _
                    & "  public.tconfsettingcashflow a " _
                    & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
                    & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code) " _
                    & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
                    & "WHERE " _
                    & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D' "

                Dim _awal, _akhir As Double
                _awal = 0
                _akhir = 0

                Dim dt_temp As New DataTable

                dt_temp = master_new.PGSqlConn.GetTableData(ssql)

                For Each dr As DataRow In dt_temp.Rows
                    _awal = dr("total")
                Next

                ssql = "SELECT  " _
                   & " sum((SELECT  " _
                   & "       sum(glbal_balance_open + glbal_balance_posted ) AS jml " _
                   & "       FROM " _
                   & "       public.glbal_balance x " _
                   & "       WHERE " _
                   & "       x.glbal_gcal_oid = '" & le_gcal.EditValue.ToString & "' AND   " _
                   & "       x.glbal_ac_id = d.ac_id )) as total " _
                   & "FROM " _
                   & "  public.tconfsettingcashflow a " _
                   & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
                   & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code) " _
                   & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
                   & "WHERE " _
                   & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D' "


                dt_temp = master_new.PGSqlConn.GetTableData(ssql)

                For Each dr As DataRow In dt_temp.Rows
                    _akhir = dr("total")
                Next


                For Each dr As DataRow In ds.Tables(0).Rows
                    dr("cf_value_beginning") = _awal
                    dr("cf_value_ending") = _akhir
                Next

                '.vtglawal = tanggal.ToString
                '.vtglakhir = EndOfMonth(tanggal, 0).ToString

                '.vlevel = level
                '.vdom = dom
                '.ven = en
                '.vsb = sb
                '.vcc = cc

                If Ce_Posting.EditValue = True Then
                    .Posting_Option = True
                Else
                    .Posting_Option = False
                End If

                .periode = de_end.DateTime.Date.ToString("dd MMMM yyyy")
                .DataSource = ds
                .DataMember = "Table"
                '.Parameters("PPeriode").Value = "PERIODE : " & de_first.EditValue & " " & de_end.EditValue
                '.Parameters("PPosisi").Value = posisi

                ' Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

                'printTool.PreviewForm.Text = "Direct Cash Flow Report Detail"
                ''.PrintingSystem = ps
                'printTool.ShowPreview()

                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.PreviewForm.Text = "Direct Cash Flow Report Detail"
                printTool.ShowPreview()

            End With
        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub

    Private Sub CleanData()
        Try
            Dim ssqls As New ArrayList
            ssql = "SELECT  " _
                & "  d.ac_id " _
                & "FROM " _
                & "  public.tconfsettingcashflow a " _
                & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
                & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code) " _
                & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
                & "WHERE " _
                & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D'"

            Dim dt_akun As New DataTable
            dt_akun = GetTableData(ssql)

            ssql = "SELECT  " _
                & "  a.glt_periode, " _
                & "  a.glt_code, " _
                & "  a.glt_date, " _
                & "  a.glt_ac_id, " _
                & "  a.ac_code, " _
                & "  a.ac_name, " _
                & "  a.glt_desc, " _
                & "  a.glt_debit, " _
                & "  a.glt_credit,glt_ref_trans_code,glt_oid " _
                & "FROM " _
                & "  public.cf_save a " _
                & "WHERE " _
                & "  a.glt_periode = '" & le_gcal.EditValue.ToString & "' " _
                & "ORDER BY " _
                & "  a.glt_code, " _
                & "  a.glt_ref_trans_code"

            Dim dt As New DataTable
            dt = GetTableData(ssql)

            ssql = "SELECT DISTINCT  " _
                & "  a.glt_code, " _
                & "  a.glt_ref_trans_code " _
                & "FROM " _
                & "  public.cf_save a " _
                & "WHERE " _
                & "  a.glt_periode = '" & le_gcal.EditValue.ToString & "' " _
                & "ORDER BY " _
                & "  a.glt_code, " _
                & "  a.glt_ref_trans_code"

            Dim dt_distinct As New DataTable
            dt_distinct = GetTableData(ssql)

            Dim _glt_code As String = ""
            Dim _reff As String = ""
            Dim _status As Boolean = False
            Dim dr_ps_virtual() As DataRow

            For Each dr As DataRow In dt_distinct.Rows
                dr_ps_virtual = dt.Select("glt_code = '" & dr("glt_code") & "' and glt_ref_trans_code='" & dr("glt_ref_trans_code") & "'")
                If cek_akun(dt_akun, dr_ps_virtual) = False Then
                    ssql = "delete from cf_save where glt_code='" & dr("glt_code") & "' and glt_ref_trans_code='" & dr("glt_ref_trans_code") & "'"
                    ssqls.Add(ssql)
                Else
                    'jika lebih dr 2 maka cek dulu apakah ada akun selain kas dan bank yg bersatu dengan kas dan bank
                    If dr_ps_virtual.Length > 2 Then
                        'cek dl apakah akun kas dan banknya ada di debet atau di kredit
                        Dim _jenis As String = ""
                        For Each dr_vir As DataRow In dr_ps_virtual
                            Dim _ac_id As Integer
                            _ac_id = dr_vir("glt_ac_id")

                            If cek_akun(_ac_id, dt_akun) = True Then
                                'benar baris tsb ada kas dan banknya
                                If dr_vir("glt_debit") <> 0 Then
                                    'berarti baris tersebut kas debit
                                    _jenis = "D"
                                Else
                                    _jenis = "C"
                                End If
                            End If

                        Next

                        Dim _nilai As Double = 0
                        For Each dr_vir As DataRow In dr_ps_virtual
                            If _jenis = "D" Then
                                If dr_vir("glt_debit") <> 0 Then
                                    Dim _ac_id As Integer
                                    _ac_id = dr_vir("glt_ac_id")

                                    If cek_akun(_ac_id, dt_akun) = False Then
                                        _nilai += dr_vir("glt_debit")

                                        ssql = "delete from cf_save where glt_oid='" & dr_vir("glt_oid").ToString & "'"
                                        ssqls.Add(ssql)

                                    End If
                                End If
                            Else
                                If dr_vir("glt_credit") <> 0 Then
                                    Dim _ac_id As Integer
                                    _ac_id = dr_vir("glt_ac_id")

                                    If cek_akun(_ac_id, dt_akun) = False Then
                                        _nilai += dr_vir("glt_credit")

                                        ssql = "delete from cf_save where glt_oid='" & dr_vir("glt_oid").ToString & "'"
                                        ssqls.Add(ssql)

                                    End If
                                End If
                            End If
                        Next

                        If _nilai > 0 Then
                            For Each dr_vir As DataRow In dr_ps_virtual
                                If _jenis = "D" Then
                                    If dr_vir("glt_credit") <> 0 Then
                                        ssql = "update cf_save set glt_credit=glt_credit-" & SetDec(_nilai) & " where glt_oid='" & dr_vir("glt_oid").ToString & "'"
                                        ssqls.Add(ssql)
                                    End If
                                Else
                                    If dr_vir("glt_debit") <> 0 Then
                                        ssql = "update cf_save set glt_debit=glt_debit-" & SetDec(_nilai) & " where glt_oid='" & dr_vir("glt_oid").ToString & "'"
                                        ssqls.Add(ssql)
                                    End If
                                End If
                            Next
                        End If


                    End If

                End If


            Next

            DbRunTran(ssqls)
            Box("Clean data success")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Function cek_akun(ByVal dt_cari As DataTable, ByVal dr_cari() As DataRow) As Boolean
        Dim ds_virtual_func() As DataRow

        Dim _hasil As Boolean = False
        For Each dr_vir As DataRow In dr_cari
            If (dr_vir("glt_debit") + dr_vir("glt_credit")) > 0 Then
                ds_virtual_func = dt_cari.Select("ac_id=" & dr_vir("glt_ac_id"))
                If ds_virtual_func.Length > 0 Then
                    _hasil = True
                End If
            End If
        Next
        Return _hasil

    End Function

    Function cek_akun(ByVal id_akun As Integer, ByVal dt_cari As DataTable) As Boolean
        Dim ds_virtual_func() As DataRow
        Dim _hasil As Boolean = False

        ds_virtual_func = dt_cari.Select("ac_id=" & id_akun)

        If ds_virtual_func.Length > 0 Then
            _hasil = True
        End If
        Return _hasil

    End Function

    Private Sub BtCleanData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtShowGrid.Click
        'CleanData()
        Try
            ssql = "SELECT  " _
                    & "  public.cf_save.glt_periode, " _
                    & "  public.cf_save.glt_code, " _
                    & "  public.cf_save.glt_date, " _
                    & "  public.cf_save.glt_ac_id, " _
                    & "  public.cf_save.ac_code, " _
                    & "  public.cf_save.ac_name, " _
                    & "  public.cf_save.glt_desc, " _
                    & "  public.cf_save.glt_debit, " _
                    & "  public.cf_save.glt_credit, " _
                    & "  public.cf_save.glt_ref_trans_code, " _
                    & "  public.cf_save.glt_oid " _
                    & "FROM " _
                    & "  public.cf_save " _
                    & "WHERE " _
                    & "  public.cf_save.glt_periode = '" & le_gcal.EditValue.ToString & "' " _
                    & "ORDER BY " _
                    & "  public.cf_save.glt_code"


            Dim dt_hasil As New DataTable
            dt_hasil = GetTableData(ssql)

            Dim frm As New frmExport
            With frm
                add_column_copy(.gv_export, "GL Code", "glt_code", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Date", "glt_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
                add_column_copy(.gv_export, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)

                add_column_copy(.gv_export, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Desc", "glt_desc", DevExpress.Utils.HorzAlignment.Default)

                add_column_copy(.gv_export, "Debit", "glt_debit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
                add_column_copy(.gv_export, "Credit", "glt_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

                add_column_copy(.gv_export, "Reff", "glt_ref_trans_code", DevExpress.Utils.HorzAlignment.Default)



                .gc_export.DataSource = dt_hasil
                .gv_export.BestFitColumns()
            End With

            frm.Show()



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtCheck.Click
        Try
            ssql = "SELECT  " _
                & "  a.code, " _
                & "  a.remark, " _
                & "  a.sort_number, " _
                & "  a.remark_header, " _
                & "  a.remark_footer, " _
                & "  a.cfsign_header, " _
                & "  a.cf_value_sign, " _
                & "  b.cfdet_oid, " _
                & "  b.cfdet_pk, " _
                & "  b.seq, " _
                & "  b.sub_header, " _
                & "  c.code as code_det, " _
                & "  c.seq as seq_det, " _
                & "  c.ac_hirarki, " _
                & "  d.ac_id,0.0 as cf_value_beginning,0.0 as cf_value_ending,b.sub_header2,b.ac_value_header,  " _
                & "  d.ac_code,coalesce(f_get_cfvalue_direct(d.ac_id,'" & le_gcal.EditValue.ToString & "',c.ac_sign,ac_value),0)  as cf_value_original, " _
                & "  d.ac_name,c.ac_sign,ac_value,coalesce(f_get_cfvalue_direct(d.ac_id,'" & le_gcal.EditValue.ToString & "',c.ac_sign,ac_value),0) * ac_value_header as cf_value " _
                & "FROM " _
                & "  public.tconfsettingcashflow a " _
                & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
                & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code) " _
                & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
                & "WHERE " _
                & "  a.cfsign_header = 'T' and d.ac_is_sumlevel='N' and cf_type='D' " _
                & "ORDER BY " _
                & "  a.sort_number, " _
                & "  b.seq,c.seq"

            Dim dt_akun As New DataTable
            dt_akun = GetTableData(ssql)

            ssql = "SELECT  " _
                & "  public.cf_save.glt_periode, " _
                & "  public.cf_save.glt_code, " _
                & "  public.cf_save.glt_date, " _
                & "  public.cf_save.glt_ac_id, " _
                & "  public.cf_save.ac_code, " _
                & "  public.cf_save.ac_name, " _
                & "  public.cf_save.glt_desc, " _
                & "  public.cf_save.glt_debit, " _
                & "  public.cf_save.glt_credit, " _
                & "  public.cf_save.glt_ref_trans_code, " _
                & "  public.cf_save.glt_oid " _
                & "FROM " _
                & "  public.cf_save " _
                & "WHERE " _
                & "  public.cf_save.glt_periode = '" & le_gcal.EditValue.ToString & "' and public.cf_save.glt_ac_id not in (SELECT  " _
                & "  d.ac_id " _
                & "FROM " _
                & "  public.tconfsettingcashflow a " _
                & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
                & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code) " _
                & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
                & "WHERE " _
                & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D') order by public.cf_save.ac_code"


            Dim dt_cf As New DataTable
            dt_cf = GetTableData(ssql)




            ssql = "select '' as ac_code, '' as ac_name ,'' as sign,'' as transaction"

            Dim dt_hasil As New DataTable
            dt_hasil = GetTableData(ssql)

            dt_hasil.Rows.Clear()

            Dim _ada As Boolean = False
            For Each dr_cf As DataRow In dt_cf.Rows
                _ada = False
                For Each dr_akun As DataRow In dt_akun.Rows
                    If dr_akun("ac_sign") = "C" Then
                        If dr_cf("glt_credit") <> 0.0 Then
                            If dr_cf("glt_ac_id") = dr_akun("ac_id") Then
                                _ada = True
                            End If
                        End If

                    Else
                        If dr_cf("glt_debit") <> 0.0 Then
                            If dr_cf("glt_ac_id") = dr_akun("ac_id") Then
                                _ada = True
                            End If
                        End If
                    End If
                Next
                If _ada = False Then

                    Dim _exist As Boolean = False
                    'For Each dr_hasil As DataRow In dt_hasil.Rows
                    '    If dr_hasil("ac_code") = dr_cf("ac_code") Then
                    '        _exist = True
                    '    End If
                    'Next

                    If _exist = False Then
                        Dim dr_new As DataRow
                        dr_new = dt_hasil.NewRow

                        dr_new("ac_code") = dr_cf("ac_code")
                        dr_new("ac_name") = dr_cf("ac_name")

                        If dr_cf("glt_credit") <> 0.0 Then
                            dr_new("sign") = "C"
                        Else
                            dr_new("sign") = "D"
                        End If

                        dr_new("transaction") = dr_cf("glt_code")

                        dt_hasil.Rows.Add(dr_new)
                        dt_hasil.AcceptChanges()
                    End If
                    
                End If


            Next
            Dim frm As New frmExport
            With frm
                add_column_edit(.gv_export, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
                add_column_edit(.gv_export, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
                add_column_edit(.gv_export, "Sign", "sign", DevExpress.Utils.HorzAlignment.Default)
                add_column_edit(.gv_export, "Transaction", "transaction", DevExpress.Utils.HorzAlignment.Default)
                .gc_export.DataSource = dt_hasil
                .gv_export.BestFitColumns()
            End With

            frm.Show()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
