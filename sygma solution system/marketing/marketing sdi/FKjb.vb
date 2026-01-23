Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FKjb
    Dim ssql As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim mf As New master_new.ModFunction
    Dim _now As DateTime
    Public _soship_oid As String
    Public _ptnr_oid As String
    Dim _conf_value, _kjb_oid_edit As String
    Public _kjb_gnt_oid As String
    Public Shared PageNum2 As String

    Private Sub FFakturPajak_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _conf_value = func_coll.get_conf_file("wf_kjb")
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now

        If _conf_value = "0" Then
            dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden
        Else
            dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
        End If
        ce_page_number.Checked = True
        ce_page_number.Checked = False
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        kjb_en_id.Properties.DataSource = dt_bantu
        kjb_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        kjb_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        kjb_en_id.ItemIndex = 0


        If _conf_value = "0" Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_tran_mstr())
            kjb_tran_id.Properties.DataSource = dt_bantu
            kjb_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            kjb_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
            kjb_tran_id.ItemIndex = 0
        Else
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_transaction())
            kjb_tran_id.Properties.DataSource = dt_bantu
            kjb_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            kjb_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
            kjb_tran_id.ItemIndex = 0
        End If
    End Sub

    Private Sub kjb_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles kjb_en_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_customer(kjb_en_id.EditValue))
        kjb_ptnr_id.Properties.DataSource = dt_bantu
        kjb_ptnr_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        kjb_ptnr_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        kjb_ptnr_id.ItemIndex = 0

    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "kjb_oid", False)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "KJB Number", "kjb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "KJB Date", "kjb_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Shipment Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "kjb_ptnr_oid", False)
        add_column_copy(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status Approval", "kjb_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "MUTE", "kjb_mute", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "WBAC", "kjb_wbac", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "PUSPI", "kjb_puspi", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "OPTIMUM", "kjb_optimum", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "INSZA", "kjb_insza", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "MIRACLEQU", "kjb_miraclequ", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tunai", "kjb_tunai", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "2 Bulan", "kjb_2bulan", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "6 Bulan", "kjb_6bulan", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "12 Bulan", "kjb_12bulan", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "18 Bulan", "kjb_18bulan", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "kjb_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "kjb_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "kjb_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "kjb_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "kjb_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_detail, "ptnratt_sq_oid", False)
        add_column_copy(gv_detail, "Tempat Bekerja", "ptnratt_bekerja_pada", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Jabatan", "ptnratt_jabatan_bagian", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Kantor Alamat1", "ptnratt_kantor_alamat_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Kantor Alamat2", "ptnratt_kantor_alamat_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Lantai Bekerja", "ptnratt_kantor_lantai", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Kantor Telephone", "ptnratt_kantor_telp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "KTP", "ptnratt_ktp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Email", "ptnratt_email", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Rumah Alamat1", "ptnratt_rumah_alamat_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Rumah Alamat2", "ptnratt_rumah_alamat_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Rumah Kode Pos", "ptnratt_rumah_kode_pos", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Rumah Telephone", "ptnratt_rumah_telp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Telephone HP", "ptnratt_rumah_hp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Status Alamat Kirim", "ptnratt_status_alamat_kirim", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Status Alamat Tagih", "ptnratt_status_alamat_tagih", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Suami/Istri Nama", "ptnratt_suami_nama", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Suami/Istri Tempat Bekerja", "ptnratt_suami_bekerja", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Suami/Istri Jabatan", "ptnratt_suami_jabatan", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Suami/Istri Kantor Alamat1", "ptnratt_suami_kantor_alamat_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Suami/Istri Kantor Alamat2", "ptnratt_suami_kantor_alamat_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Suami/Istri Telephone", "ptnratt_suami_telp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Suami/Istri HP", "ptnratt_suami_hp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Anak Pertama Nama", "ptnratt_anak_nama_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Anak Pertama Tgl Lahir", "ptnratt_anak_tgl_lahir_1", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Anak Pertama Sekolah", "ptnratt_anak_sekolah_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Anak kedua Nama", "ptnratt_anak_nama_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Anak kedua Tgl Lahir", "ptnratt_anak_tgl_lahir_2", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Anak kedua Sekolah", "ptnratt_anak_sekolah_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Anak ketiga Nama", "ptnratt_anak_nama_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Anak ketiga Tgl Lahir", "ptnratt_anak_tgl_lahir_3", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Anak ketiga Sekolah", "ptnratt_anak_sekolah_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Keluarga Dekat Nama", "ptnratt_keluarga_dekat_nama", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Keluarga Dekat Alamat1", "ptnratt_keluarga_dekat_alamat_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Keluarga Dekat Alamat2", "ptnratt_keluarga_dekat_alamat_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Keluarga Dekat Telephone", "ptnratt_keluarga_dekat_telp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Keluarga Dekat HP", "ptnratt_keluarga_dekat_hp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Status Tempat Tinggal", "ptnratt_status_tempat_tinggal", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Jenis Kartu Kredit", "ptnratt_jenis_kartu_kredit", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "No Kartu Kredit", "ptnratt_no_kartu_kredit", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Berlaku s/d", "ptnratt_berlaku_sd", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Bank", "ptnratt_bank", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Status Rumah Tinggal", "status_kepemilikan", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Lama Tinggal", "lama_tinggal", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Masa Kerja", "masa_kerja", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Income", "income", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Kepribadian", "kepribadian", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Tanggungan", "tanggungan", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Jaminan", "jaminan", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_wf, "wf_ref_code", False)
        add_column(gv_wf, "wf_ref_oid", False)
        add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "dd/MM/yy H:mm")
        add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_email, "kjb_oid", False)
        add_column_copy(gv_email, "KJB Number", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "KJB Date", "kjb_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Shipment Number", "kjb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Address", "address", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Status Approval", "kjb_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "User Create", "kjb_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date Create", "kjb_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "User Update", "kjb_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date Update", "kjb_upd_date", DevExpress.Utils.HorzAlignment.Center)


        add_column_copy(gv_smart, "Code", "kjb_code", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  kjb_oid, " _
                    & "  kjb_dom_id, " _
                    & "  kjb_en_id, " _
                    & "  en_desc, " _
                    & "  kjb_add_by, " _
                    & "  kjb_add_date, " _
                    & "  kjb_upd_by, " _
                    & "  kjb_upd_date, " _
                    & "  kjb_code, " _
                    & "  kjb_dt, " _
                    & "  kjb_date, " _
                    & "  kjb_trans_id, " _
                    & "  kjb_ptnr_oid, " _
                    & "  kjb_ptnr_id, " _
                    & "  ptnr_name, " _
                    & "  kjb_remarks, " _
                    & "  kjb_soship_oid, " _
                    & "  kjb_mute, " _
                    & "  kjb_wbac, " _
                    & "  kjb_puspi, " _
                    & "  kjb_optimum, " _
                    & "  kjb_insza, " _
                    & "  kjb_miraclequ, " _
                    & "  kjb_tunai, " _
                    & "  kjb_2bulan, " _
                    & "  kjb_6bulan, " _
                    & "  kjb_12bulan, " _
                    & "  kjb_18bulan, " _
                    & "  soship_code " _
                    & "FROM  " _
                    & "  public.kjb_mstr " _
                    & "  inner join en_mstr on en_id=kjb_en_id " _
                    & "  inner join soship_mstr on soship_oid=kjb_soship_oid " _
                    & "  inner join ptnr_mstr on ptnr_oid=kjb_ptnr_oid " _
                    & "  where kjb_mstr.kjb_en_id in (select user_en_id from tconfuserentity " _
                    & "  where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                    & "  and kjb_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and kjb_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  order by kjb_code"
        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
                            & "  ptnratt_oid, " _
                            & "  ptnratt_bekerja_pada, " _
                            & "  ptnratt_jabatan_bagian, " _
                            & "  ptnratt_kantor_alamat_1, " _
                            & "  ptnratt_kantor_alamat_2, " _
                            & "  ptnratt_kantor_lantai, " _
                            & "  ptnratt_kantor_telp, " _
                            & "  ptnratt_ktp, " _
                            & "  ptnratt_email, " _
                            & "  ptnratt_rumah_alamat_1, " _
                            & "  ptnratt_rumah_alamat_2, " _
                            & "  ptnratt_rumah_kode_pos, " _
                            & "  ptnratt_rumah_telp, " _
                            & "  ptnratt_rumah_hp, " _
                            & "  ptnratt_status_alamat_kirim, " _
                            & "  ptnratt_status_alamat_tagih, " _
                            & "  ptnratt_suami_nama, " _
                            & "  ptnratt_suami_bekerja, " _
                            & "  ptnratt_suami_jabatan, " _
                            & "  ptnratt_suami_kantor_alamat_1, " _
                            & "  ptnratt_suami_kantor_alamat_2, " _
                            & "  ptnratt_suami_telp, " _
                            & "  ptnratt_suami_hp, " _
                            & "  ptnratt_anak_nama_1, " _
                            & "  ptnratt_anak_tgl_lahir_1, " _
                            & "  ptnratt_anak_sekolah_1, " _
                            & "  ptnratt_anak_nama_2, " _
                            & "  ptnratt_anak_tgl_lahir_2, " _
                            & "  ptnratt_anak_sekolah_2, " _
                            & "  ptnratt_anak_nama_3, " _
                            & "  ptnratt_anak_tgl_lahir_3, " _
                            & "  ptnratt_anak_sekolah_3, " _
                            & "  ptnratt_keluarga_dekat_nama, " _
                            & "  ptnratt_keluarga_dekat_alamat_1, " _
                            & "  ptnratt_keluarga_dekat_alamat_2, " _
                            & "  ptnratt_keluarga_dekat_telp, " _
                            & "  ptnratt_keluarga_dekat_hp, " _
                            & "  ptnratt_status_tempat_tinggal, " _
                            & "  ptnratt_jenis_kartu_kredit, " _
                            & "  ptnratt_no_kartu_kredit, " _
                            & "  ptnratt_berlaku_sd, " _
                            & "  ptnratt_dt, " _
                            & "  ptnratt_bank, " _
                            & "  ptnratt_status_rumah_id, " _
                            & "  ptnratt_lama_tinggal_id, " _
                            & "  ptnratt_masa_kerja_id, " _
                            & "  ptnratt_income_id, " _
                            & "  ptnratt_kepribadian_id, " _
                            & "  ptnratt_tanggungan_id, " _
                            & "  ptnratt_jaminan_id, " _
                            & "  sr.code_name as status_rumah, " _
                            & "  lt.code_name as lama_tinggal, " _
                            & "  ms.code_name as masa_kerja, " _
                            & "  ic.code_name as income, " _
                            & "  tg.code_name as tanggungan, " _
                            & "  kp.code_name as kepribadian, " _
                            & "  ja.code_name as jaminan, " _
                            & "  ((select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=ptnratt_status_rumah_id)) + " _
                            & "  (select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=ptnratt_lama_tinggal_id))+ " _
                            & "  (select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=ptnratt_masa_kerja_id))+ " _
                            & "  coalesce((select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=ptnratt_kepribadian_id)),0)+ " _
                            & "  coalesce((select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=ptnratt_jaminan_id)),0)+ " _
                            & "  (select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=ptnratt_income_id))- " _
                            & "  (select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=ptnratt_tanggungan_id)))as total_point " _
                            & "FROM  " _
                            & "  public.ptnr_mstr " _
                            & "  INNER JOIN public.en_mstr ON (ptnr_en_id = en_id)" _
                            & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                            & "  INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                            & "  LEFT OUTER JOIN public.ptnratt_attr on ptnratt_ptnr_oid = ptnr_oid " _
                            & "  LEFT OUTER JOIN public.code_mstr sr on sr.code_id = ptnratt_status_rumah_id " _
                            & "  LEFT OUTER JOIN public.code_mstr lt on lt.code_id = ptnratt_lama_tinggal_id " _
                            & "  LEFT OUTER JOIN public.code_mstr ms on ms.code_id = ptnratt_masa_kerja_id " _
                            & "  LEFT OUTER JOIN public.code_mstr ic on ic.code_id = ptnratt_income_id " _
                            & "  LEFT OUTER JOIN public.code_mstr tg on tg.code_id = ptnratt_tanggungan_id " _
                            & "  LEFT OUTER JOIN public.code_mstr kp on tg.code_id = ptnratt_kepribadian_id " _
                            & "  LEFT OUTER JOIN public.code_mstr ja on tg.code_id = ptnratt_jaminan_id " _
                            & "  where ptnratt_ptnr_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("kjb_ptnr_oid").ToString & "'"

                           
        load_data_detail(sql, gc_detail, "detail")

        If _conf_value = "1" Then
            Try
                ds.Tables("wf").Clear()
            Catch ex As Exception
            End Try

            sql = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                  " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                  " wf_iscurrent, wf_seq " + _
                  " from wf_mstr w " + _
                  " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                  " inner join kjb_mstr on kjb_code = wf_ref_code " _
                & " where kjb_date >= " + SetDate(pr_txttglawal.DateTime) _
                & " and kjb_date <= " + SetDate(pr_txttglakhir.DateTime) _
                & " and kjb_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("kjb_oid").ToString & "' " _
                & " and kjb_en_id in (select user_en_id from tconfuserentity " _
                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                & " order by wf_ref_code, wf_seq "
            load_data_detail(sql, gc_wf, "wf")
            gv_wf.BestFitColumns()

            Try
                ds.Tables("email").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                    & "  kjb_oid, " _
                    & "  kjb_dom_id, " _
                    & "  kjb_en_id, " _
                    & "  kjb_add_by, " _
                    & "  kjb_add_date, " _
                    & "  kjb_upd_by, " _
                    & "  kjb_upd_date, " _
                    & "  kjb_code, " _
                    & "  kjb_dt, " _
                    & "  kjb_date, " _
                    & "  kjb_trans_id, " _
                    & "  kjb_ptnr_oid, " _
                    & "  kjb_ptnr_oid, " _
                    & "  kjb_soship_oid, " _
                    & "  kjbd_oid, " _
                    & "  kjbd_dom_id, " _
                    & "  kjbd_en_id, " _
                    & "  kjbd_add_by, " _
                    & "  kjbd_add_date, " _
                    & "  kjbd_upd_by, " _
                    & "  kjbd_upd_date, " _
                    & "  kjbd_dt, " _
                    & "  kjbd_kjb_oid " _
                    & "FROM  " _
                    & "  public.kjb_mstr " _
                    & "  inner join kjbd_det on kjbd_kjb_oid=kjb_oid " _
                    & " where kjb_date >= " + SetDate(pr_txttglawal.DateTime) _
                    & " and kjb_date <= " + SetDate(pr_txttglakhir.DateTime) _
                    & " and kjb_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

            load_data_detail(sql, gc_email, "email")
            gv_email.BestFitColumns()

            Try
                ds.Tables("smart").Clear()
            Catch ex As Exception
            End Try

            sql = "select kjb_oid, kjb_code, kjb_trans_id, false as status from kjb_mstr " _
                & " where kjb_trans_id ~~* 'd' and kjb_add_by ~~* '" + master_new.ClsVar.sNama + "'"

            load_data_detail(sql, gc_smart, "smart")
        End If
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("ptnratt_ptnr_oid").FilterInfo = _
             New DevExpress.XtraGrid.Columns.ColumnFilterInfo("ptnratt_ptnr_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("kjb_ptnr_oid").ToString & "'")
            gv_detail.BestFitColumns()

            If _conf_value = "1" Then
                gv_wf.Columns("wf_ref_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[wf_ref_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("kjb_oid").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("kjb_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[kjb_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("kjb_oid").ToString & "'")
                gv_email.BestFitColumns()
            End If
        Catch ex As Exception
        End Try

    End Sub

    Public Overrides Sub insert_data_awal()
        kjb_en_id.ItemIndex = 0
        kjb_date.DateTime = _now
        kjb_ptnr_id.ItemIndex = 0
        kjb_tran_id.ItemIndex = 0
        kjb_ptnr_addr_oid.ItemIndex = 0

        kjb_soship_oid.Text = ""
        'kjb_kjb_code.Enabled = False
        kjb_soship_oid.Enabled = True
       

        _kjb_gnt_oid = ""

        kjb_en_id.Focus()
    End Sub

   

    Private Sub kjb_ar_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles kjb_soship_oid.ButtonClick
        Dim frm As New FSalesOrderShipmentSearch
        frm.set_win(Me)
        frm._en_id = kjb_en_id.EditValue
        frm._obj = kjb_soship_oid 'be_first
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Function before_save_insert() As Boolean
        before_save_insert = True

        'Dim _jml As Integer = 0
        'Try
        '    Using objcek As New master_new.WDABasepgsql("", "")
        '        With objcek
        '            .Connection.Open()
        '            .Command = .Connection.CreateCommand
        '            .Command.CommandType = CommandType.Text
        '            .Command.CommandText = "select count(kjb_code) as jml from kjb_mstr " + _
        '                                   " where kjb_code ~~* " + SetSetring(kjb_code.Text) + _
        '                                   " and kjb_trans_id <> 'X' "
        '            .InitializeCommand()
        '            .DataReader = .Command.ExecuteReader
        '            While .DataReader.Read
        '                _jml = .DataReader.Item("jml")
        '            End While
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        '    Return False
        'End Try

        'If _jml > 0 Then
        '    MessageBox.Show("Duplicate Faktur Pajak Code...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If

        Return before_save_insert
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        Return before_save
    End Function

    Private Function get_rev() As Integer
        get_rev = 0
        'Try
        '    Using objcek As New master_new.WDABasepgsql("", "")
        '        With objcek
        '            .Connection.Open()
        '            .Command = .Connection.CreateCommand
        '            .Command.CommandType = CommandType.Text
        '            .Command.CommandText = "select count(kjb_code) as jml from kjb_mstr " + _
        '                                   " where kjb_code ~~* " + SetSetring(kjb_code.Text) + _
        '                                   " and kjb_trans_id = 'X' "
        '            .InitializeCommand()
        '            .DataReader = .Command.ExecuteReader
        '            While .DataReader.Read
        '                get_rev = .DataReader.Item("jml")
        '            End While
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        '    Return False
        'End Try

        Return get_rev
    End Function

    Public Overrides Function insert() As Boolean
        If before_save_insert() = False Then
            Exit Function
        End If

        Dim ds_bantu As New DataSet
        Dim _kjb_oid As Guid
        _kjb_oid = Guid.NewGuid

        Dim i As Integer

        Dim _kjb_trans_id As String
        Dim _kjb_code As String

        _kjb_code = func_coll.get_transaction_number("KJ", kjb_en_id.GetColumnValue("en_code"), "kjb_mstr", "kjb_code")

        If _conf_value = "1" Then
            ds_bantu = func_data.load_aprv_mstr(kjb_tran_id.EditValue)
            _kjb_trans_id = "D"
        Else
            _kjb_trans_id = "I"
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
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.kjb_mstr " _
                                            & "( " _
                                            & "  kjb_oid, " _
                                            & "  kjb_dom_id, " _
                                            & "  kjb_en_id, " _
                                            & "  kjb_add_by, " _
                                            & "  kjb_add_date, " _
                                            & "  kjb_code, " _
                                            & "  kjb_dt, " _
                                            & "  kjb_date, " _
                                            & "  kjb_trans_id, " _
                                            & "  kjb_ptnr_oid, " _
                                            & "  kjb_ptnr_id, " _
                                            & "  kjb_soship_oid, " _
                                            & "  kjb_remarks, " _
                                            & "  kjb_mute, " _
                                            & "  kjb_wbac, " _
                                            & "  kjb_puspi, " _
                                            & "  kjb_optimum, " _
                                            & "  kjb_insza, " _
                                            & "  kjb_miraclequ, " _
                                            & "  kjb_tunai, " _
                                            & "  kjb_2bulan, " _
                                            & "  kjb_6bulan, " _
                                            & "  kjb_12bulan, " _
                                            & "  kjb_18bulan " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_kjb_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(kjb_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & " current_timestamp " & ",  " _
                                            & SetSetring(_kjb_code) & ",  " _
                                            & " current_timestamp " & ",  " _
                                            & SetDate(kjb_date.DateTime) & ",  " _
                                            & SetSetring(_kjb_trans_id) & ",  " _
                                            & SetSetring(_ptnr_oid.ToString) & ",  " _
                                            & SetInteger(kjb_ptnr_id.EditValue) & ",  " _
                                            & SetSetring(_soship_oid) & ",  " _
                                            & SetSetring(kjb_rmks.Text) & ",  " _
                                            & SetBitYN(kjb_mute.EditValue) & ",  " _
                                            & SetBitYN(kjb_wbac.EditValue) & ",  " _
                                            & SetBitYN(kjb_puspi.EditValue) & ",  " _
                                            & SetBitYN(kjb_optimum.EditValue) & ",  " _
                                            & SetBitYN(kjb_insza.EditValue) & ",  " _
                                            & SetBitYN(kjb_miraclequ.EditValue) & ",  " _
                                            & SetBitYN(kjb_tunai.EditValue) & ",  " _
                                            & SetBitYN(kjb_2bulan.EditValue) & ",  " _
                                            & SetBitYN(kjb_6bulan.EditValue) & ",  " _
                                            & SetBitYN(kjb_12bulan.EditValue) & ",  " _
                                            & SetBitYN(kjb_18bulan.EditValue) & "  " _
                                            & ");"


                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If _conf_value = "1" Then
                            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                & "  public.wf_mstr " _
                                                & "( " _
                                                & "  wf_oid, " _
                                                & "  wf_dom_id, " _
                                                & "  wf_en_id, " _
                                                & "  wf_tran_id, " _
                                                & "  wf_ref_oid, " _
                                                & "  wf_ref_code, " _
                                                & "  wf_ref_desc, " _
                                                & "  wf_seq, " _
                                                & "  wf_user_id, " _
                                                & "  wf_wfs_id, " _
                                                & "  wf_iscurrent, " _
                                                & "  wf_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(kjb_en_id.EditValue) & ",  " _
                                                & SetSetring(kjb_tran_id.EditValue) & ",  " _
                                                & SetSetring(_kjb_oid.ToString) & ",  " _
                                                & SetSetring(_kjb_code) & ",  " _
                                                & SetSetring("Kontrak Jual Beli") & ",  " _
                                                & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_seq")) & ",  " _
                                                & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_user_id")) & ",  " _
                                                & SetInteger(0) & ",  " _
                                                & SetSetring("N") & ",  " _
                                                & " current_timestamp " & "  " _
                                                & ")"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If


                        sqlTran.Commit()
                        after_success()
                        set_row(Trim(_kjb_oid.ToString), "kjb_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function edit_data() As Boolean
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("kjb_trans_id") <> "D" Then
        '    If func_coll.get_status_wf(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("kjb_code")) > 0 Then
        '        MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Exit Function
        '    Else
        '        MessageBox.Show("Can't Edit Data..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Exit Function
        '    End If
        'End If

        'If MyBase.edit_data = True Then
        '    row = BindingContext(ds.Tables(0)).Position

        '    With ds.Tables(0).Rows(row)
        '        _kjb_oid_edit = SetString(.Item("kjb_oid"))
        '        kjb_en_id.EditValue = .Item("kjb_en_id")
        '        _soship_oid = .Item("kjb_soship_oid")
        '        kjb_date.DateTime = .Item("kjb_date")
        '        kjb_ptnr_id.EditValue = .Item("kjb_ptnr_id")
        '        kjb_ptnr_addr_oid.EditValue = .Item("kjb_ptnr_oid")
        '    End With

        '    kjb_soship_oid.Enabled = False


        '    kjb_en_id.Focus()

        '    edit_data = True
        'End If

    End Function

    Public Overrides Function edit()
        edit = True

        _conf_value = func_coll.get_conf_file("wf_faktur_pajak")
        Dim i As Integer

        'Cari total ammount
        Dim ds_bantu As New DataSet
        Dim _kjb_trn_id As Integer
        Dim _kjb_trn_satatus As String
        '=============================================================================

        _kjb_trn_id = kjb_tran_id.EditValue
        _kjb_trn_satatus = "D" 'set default langsung ke D

        If _conf_value = "1" Then
            ds_bantu = func_data.load_aprv_mstr(kjb_tran_id.EditValue)
        End If

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        ' ''.Command.CommandType = CommandType.Text
                        ' ''.Command.CommandText = "UPDATE  " _
                        ' ''                    & "  public.kjb_mstr   " _
                        ' ''                    & "SET  " _
                        ' ''                    & "  kjb_en_id = " & SetSetring(kjb_en_id.EditValue) & ",  " _
                        ' ''                    & "  kjb_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                        ' ''                    & "  kjb_upd_date = " & "(select current_timestamp)" & ",  " _
                        ' ''                    & "  kjb_date = " & SetDate(kjb_date.DateTime) & ",  " _
                        ' ''                    & "  kjb_sign = " & SetSetring(kjb_sign.Text) & ",  " _
                        ' ''                    & "  kjb_unstrikeout = " & SetSetring(kjb_unstrikeout.EditValue) & ",  " _
                        ' ''                    & "  kjb_tran_id = " & SetInteger(kjb_tran_id.EditValue) & ",  " _
                        ' ''                    & "  kjb_trans_id = " & SetSetring(_kjb_trn_satatus) & ",  " _
                        ' ''                    & "  kjb_dt = current_timestamp" _
                        ' ''                    & "  " _
                        ' ''                    & "WHERE  " _
                        ' ''                    & "  kjb_oid = '" & _kjb_oid_edit & "' "

                        ' ''.Command.ExecuteNonQuery()
                        ' ''.Command.Parameters.Clear()


                        '================================================================
                        If _conf_value = "1" Then

                            If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("kjb_tran_id") <> kjb_tran_id.EditValue Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + _kjb_oid_edit.ToString + "'"
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                                For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "INSERT INTO  " _
                                                            & "  public.wf_mstr " _
                                                            & "( " _
                                                            & "  wf_oid, " _
                                                            & "  wf_dom_id, " _
                                                            & "  wf_en_id, " _
                                                            & "  wf_tran_id, " _
                                                            & "  wf_ref_oid, " _
                                                            & "  wf_ref_code, " _
                                                            & "  wf_ref_desc, " _
                                                            & "  wf_seq, " _
                                                            & "  wf_user_id, " _
                                                            & "  wf_wfs_id, " _
                                                            & "  wf_iscurrent, " _
                                                            & "  wf_dt " _
                                                            & ")  " _
                                                            & "VALUES ( " _
                                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                            & SetInteger(kjb_en_id.EditValue) & ",  " _
                                                            & SetSetring(kjb_tran_id.EditValue) & ",  " _
                                                            & SetSetring(_kjb_oid_edit.ToString) & ",  " _
                                                            & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("kjb_code")) & ",  " _
                                                            & SetSetring("Faktur Pajak") & ",  " _
                                                            & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_seq")) & ",  " _
                                                            & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_user_id")) & ",  " _
                                                            & SetInteger(0) & ",  " _
                                                            & SetSetring("N") & ",  " _
                                                            & " current_timestamp " & "  " _
                                                            & ")"
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next
                            ElseIf func_coll.get_status_wf(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("kjb_code")) > 0 Then

                            End If
                        End If

                        sqlTran.Commit()
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        set_row(Trim(_kjb_oid_edit.ToString), "kjb_oid")
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
        row = BindingContext(ds.Tables(0)).Position

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

        'Dim i As Integer

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
                            .Command.CommandText = "delete from kjb_mstr where kjb_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("kjb_oid").ToString + "'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("kjb_oid").ToString + "'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

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

    Private Sub kjb_ptnr_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles kjb_ptnr_id.EditValueChanged
        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select code_name as address_type, (coalesce(ptnra_line_1,'') || ' ' || coalesce(ptnra_line_2,'') || ' ' || coalesce(ptnra_line_3,'')) as address, ptnra_oid " + _
                           " from ptnra_addr inner join code_mstr on code_id = ptnra_addr_type " + _
                           " where ptnra_ptnr_oid = '" + kjb_ptnr_id.GetColumnValue("ptnr_oid").ToString + "'"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "address")
                    kjb_ptnr_addr_oid.Properties.DataSource = ds_bantu.Tables("address")
                    kjb_ptnr_addr_oid.Properties.DisplayMember = ds_bantu.Tables("address").Columns("address").ToString
                    kjb_ptnr_addr_oid.Properties.ValueMember = ds_bantu.Tables("address").Columns("ptnra_oid").ToString
                    kjb_ptnr_addr_oid.ItemIndex = 0
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub kjb_kjb_code_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Dim frm As New FFakturPajakSearch
        frm.set_win(Me)
        frm._en_id = kjb_en_id.EditValue
        frm._ptnr_id = kjb_ptnr_id.EditValue
        'frm._obj = kjb_kjb_code
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Public Overrides Sub approve_line()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("kjb_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("kjb_oid").ToString
        _colom = "kjb_trans_id"
        _table = "kjb_mstr"
        _criteria = "kjb_code"
        _initial = "kjb"
        _type = "kjb"
        _title = "Kontrak Jual Beli"
        approve(_colom, _table, _criteria, _oid, _code, _initial, _type, gv_email, _title)
    End Sub

    Public Overrides Sub approve(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String, ByVal par_code As String, _
                                   ByVal par_initial As String, ByVal par_type As String, ByVal par_gv As Object, ByVal par_title As String)
        'par_initial contohnya pby
        'par_type contohnya dr
        Dim _trn_status, user_wf, user_wf_email, filename, format_email_bantu, _pby_code As String
        If mf.get_transaction_status_by_oid(par_colom, par_table, "kjb_oid", par_oid) <> "D" Then
            MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If MessageBox.Show("Approve This Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        _pby_code = par_code
        _trn_status = "W"
        user_wf = mf.get_user_wf(par_code, 0)
        user_wf_email = mf.get_email_address(user_wf)

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update " + par_table + " set " + par_colom + " = '" + _trn_status + "'," + _
                                               " " + par_initial + "_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " " + par_initial + "_upd_date = current_timestamp " + _
                                               " where " + par_initial + "_oid = '" + par_oid + "'"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        '============================================================================
                        If func_coll.get_status_wf(par_code.ToString()) = 0 Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                   " where wf_ref_oid = '" + par_oid + "'" + _
                                                   " and wf_seq = 0"

                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'Karena bisa saja datanya di rollback sehingga pada saat approve lagi...datanya harus dikosongkan lagi....
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update wf_mstr set wf_wfs_id = 0, wf_date_to = null, wf_desc = '', wf_aprv_user = '', wf_aprv_date = null " + _
                                                   " where wf_ref_oid = '" + par_oid + "'"

                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        ElseIf func_coll.get_status_wf(par_code.ToString()) > 0 Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update wf_mstr set " + _
                                                   " wf_iscurrent = 'Y', " + _
                                                   " wf_wfs_id = '0', " + _
                                                   " wf_desc = '', " + _
                                                   " wf_date_to = null, " + _
                                                   " wf_aprv_user = '', " + _
                                                   " wf_aprv_date = null " + _
                                                   " where wf_ref_oid = '" + par_oid + "'" + _
                                                   " and wf_wfs_id = '4' "
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If
                        '============================================================================

                        sqlTran.Commit()

                        format_email_bantu = mf.format_email(user_wf, par_code, par_type)

                        filename = "c:\syspro\" + par_code + ".xls"
                        ExportTo(par_gv, New ExportXlsProvider(filename))

                        If user_wf_email <> "" Then
                            mf.sent_email(user_wf_email, "", mf.title_email(par_title, par_code), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
                        Else
                            'MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If

                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        help_load_data(True)
                        set_row(Trim(par_oid), par_initial + "_oid")
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

    Public Overrides Sub cancel_line()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("kjb_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("kjb_oid").ToString
        _colom = "kjb_trans_id"
        _table = "kjb_mstr"
        _criteria = "kjb_code"
        _initial = "fp"
        _type = "fp"

        cancel_approve(_colom, _table, _criteria, _oid, _code, _initial, _type)
    End Sub

    Public Overrides Sub cancel_approve(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String, ByVal par_code As String, _
                                   ByVal par_initial As String, ByVal par_type As String)

        Dim _trans_id As String = ""

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select " + par_colom + " as trans_id from " + par_table + " where kjb_oid = '" + par_oid + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _trans_id = .DataReader("trans_id").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If _trans_id.ToUpper = "D" Then
            MessageBox.Show("Can't Cancel For Draft Data..", "Conf", MessageBoxButtons.OK)
            Exit Sub
        ElseIf _trans_id.ToUpper = "C" Then
            MessageBox.Show("Can't Cancel For Close Data..", "Conf", MessageBoxButtons.OK)
            Exit Sub
        Else
            If MessageBox.Show("Cancel This Data..", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
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
                        .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'X'" + _
                                               " where kjb_oid = '" + par_oid + "'"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update wf_mstr set wf_iscurrent = 'N'" + _
                                               " where wf_ref_oid = '" + par_oid + "'"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Updated..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        row = BindingContext(ds.Tables(0)).Position
                        help_load_data(True)
                        set_row(Trim(par_oid), par_initial + "_oid")
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

    Public Overrides Sub reminder_mail()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _type, _user, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("kjb_code")
        _type = "fp"
        _user = func_coll.get_wf_iscurrent(_code)
        _title = "Faktur Pajak"

        If _user = "" Then
            MessageBox.Show("Not Available Current User..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        reminder_by_mail(_code, _type, _user, gv_email, _title)
    End Sub

    Public Overrides Sub smart_approve()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _trans_id, user_wf, user_wf_email, filename, format_email_bantu As String
        Dim i As Integer

        If MessageBox.Show("Approve All Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        ds.Tables("smart").AcceptChanges()

        For i = 0 To ds.Tables("smart").Rows.Count - 1
            If ds.Tables("smart").Rows(i).Item("status") = True Then

                Try
                    gv_email.Columns("kjb_oid").FilterInfo = _
                    New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[kjb_oid] = '" & ds.Tables("smart").Rows(i).Item("kjb_oid").ToString & "'")
                    gv_email.BestFitColumns()
                Catch ex As Exception
                End Try

                _trans_id = "W" 'default langsung ke W 
                user_wf = mf.get_user_wf(ds.Tables("smart").Rows(i).Item("kjb_code"), 0)
                user_wf_email = mf.get_email_address(user_wf)

                Try
                    Using objinsert As New master_new.WDABasepgsql("", "")
                        With objinsert
                            .Connection.Open()
                            Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                            Try
                                .Command = .Connection.CreateCommand
                                .Command.Transaction = sqlTran
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update kjb_mstr set kjb_trans_id = '" + _trans_id + "'," + _
                                               " kjb_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " kjb_upd_date = current_timestamp " + _
                                               " where kjb_oid = '" + ds.Tables("smart").Rows(i).Item("kjb_oid").ToString + "'"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                '============================================================================
                                If func_coll.get_status_wf(ds.Tables("smart").Rows(i).Item("kjb_code")) = 0 Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                           " where wf_ref_code = '" + ds.Tables("smart").Rows(i).Item("kjb_oid").ToString + "'" + _
                                                           " and wf_seq = 0"

                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()


                                    'Karena bisa saja datanya di rollback sehingga pada saat approve lagi...datanya harus dikosongkan lagi....
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update wf_mstr set wf_wfs_id = 0, wf_date_to = null, wf_desc = '', wf_aprv_user = '', wf_aprv_date = null " + _
                                                           " where wf_ref_oid = '" + ds.Tables("smart").Rows(i).Item("kjb_oid").ToString + "'"

                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                ElseIf func_coll.get_status_wf(ds.Tables("smart").Rows(i).Item("kjb_code")) > 0 Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update wf_mstr set " + _
                                                           " wf_iscurrent = 'Y', " + _
                                                           " wf_wfs_id = '0', " + _
                                                           " wf_desc = '', " + _
                                                           " wf_date_to = null, " + _
                                                           " wf_aprv_user = '', " + _
                                                           " wf_aprv_date = null " + _
                                                           " where wf_ref_oid = '" + ds.Tables("smart").Rows(i).Item("kjb_oid").ToString + "'" + _
                                                           " and wf_wfs_id = '4' "
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                End If
                                '============================================================================

                                sqlTran.Commit()

                                format_email_bantu = mf.format_email(user_wf, ds.Tables("smart").Rows(i).Item("kjb_code"), "dr")

                                filename = "c:\syspro\" + ds.Tables("smart").Rows(i).Item("kjb_code") + ".xls"
                                ExportTo(gv_email, New ExportXlsProvider(filename))

                                If user_wf_email <> "" Then
                                    mf.sent_email(user_wf_email, "", mf.title_email("Faktur Pajak", ds.Tables("smart").Rows(i).Item("kjb_code")), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
                                Else
                                    'MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                End If

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
        Next

        help_load_data(True)
        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("kjb_en_id")
        _type = 11
        _table = "kjb_mstr"
        _initial = "kjb"
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("kjb_code")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("kjb_code")

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)
        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
                            & "  kjb_oid, " _
                            & "  kjb_dom_id, " _
                            & "  kjb_en_id, " _
                            & "  en_desc, " _
                            & "  kjb_add_by, " _
                            & "  kjb_add_date, " _
                            & "  kjb_upd_by, " _
                            & "  kjb_upd_date, " _
                            & "  kjb_code, " _
                            & "  kjb_dt, " _
                            & "  kjb_date, " _
                            & "  kjb_trans_id, " _
                            & "  kjb_ptnr_oid, " _
                            & "  kjb_ptnr_id, " _
                            & "  ptnr_name, " _
                            & "  kjb_remarks, " _
                            & "  kjb_soship_oid, " _
                            & "  soship_code, " _
                            & "  soship_date, " _
                            & "  ptnratt_oid, " _
                            & "  ptnratt_bekerja_pada, " _
                            & "  ptnratt_jabatan_bagian, " _
                            & "  ptnratt_kantor_alamat_1, " _
                            & "  ptnratt_kantor_alamat_2, " _
                            & "  ptnratt_kantor_lantai, " _
                            & "  ptnratt_kantor_telp, " _
                            & "  ptnratt_ktp, " _
                            & "  ptnratt_email, " _
                            & "  ptnratt_rumah_alamat_1, " _
                            & "  ptnratt_rumah_alamat_2, " _
                            & "  ptnratt_rumah_kode_pos, " _
                            & "  ptnratt_rumah_telp, " _
                            & "  ptnratt_rumah_hp, " _
                            & "  CASE upper(ptnratt_status_alamat_kirim) " _
                            & "  WHEN 'N' THEN '' " _
                            & "  WHEN 'Y' THEN 'V' " _
                            & "  END AS ptnratt_status_alamat_kirim, " _
                            & "  CASE upper(ptnratt_status_alamat_tagih) " _
                            & "  WHEN 'N' THEN '' " _
                            & "  WHEN 'Y' THEN 'V' " _
                            & "  END AS ptnratt_status_alamat_tagih, " _
                            & "  CASE upper(kjb_mute) " _
                            & "  WHEN 'N' THEN '' " _
                            & "  WHEN 'Y' THEN 'V' " _
                            & "  END AS kjb_mute, " _
                            & "  CASE upper(kjb_wbac) " _
                            & "  WHEN 'N' THEN '' " _
                            & "  WHEN 'Y' THEN 'V' " _
                            & "  END AS kjb_wbac, " _
                            & "  CASE upper(kjb_puspi) " _
                            & "  WHEN 'N' THEN '' " _
                            & "  WHEN 'Y' THEN 'V' " _
                            & "  END AS kjb_puspi, " _
                            & "  CASE upper(kjb_optimum) " _
                            & "  WHEN 'N' THEN '' " _
                            & "  WHEN 'Y' THEN 'V' " _
                            & "  END AS kjb_optimum, " _
                            & "  CASE upper(kjb_insza) " _
                            & "  WHEN 'N' THEN '' " _
                            & "  WHEN 'Y' THEN 'V' " _
                            & "  END AS kjb_insza, " _
                            & "  CASE upper(kjb_miraclequ) " _
                            & "  WHEN 'N' THEN '' " _
                            & "  WHEN 'Y' THEN 'V' " _
                            & "  END AS kjb_miraclequ, " _
                            & "  CASE upper(kjb_tunai) " _
                            & "  WHEN 'N' THEN '' " _
                            & "  WHEN 'Y' THEN 'V' " _
                            & "  END AS kjb_tunai, " _
                            & "  CASE upper(kjb_2bulan) " _
                            & "  WHEN 'N' THEN '' " _
                            & "  WHEN 'Y' THEN 'V' " _
                            & "  END AS kjb_2bulan, " _
                            & "  CASE upper(kjb_6bulan) " _
                            & "  WHEN 'N' THEN '' " _
                            & "  WHEN 'Y' THEN 'V' " _
                            & "  END AS kjb_6bulan, " _
                            & "  CASE upper(kjb_12bulan) " _
                            & "  WHEN 'N' THEN '' " _
                            & "  WHEN 'Y' THEN 'V' " _
                            & "  END AS kjb_12bulan, " _
                            & "  CASE upper(kjb_18bulan) " _
                            & "  WHEN 'N' THEN '' " _
                            & "  WHEN 'Y' THEN 'V' " _
                            & "  END AS kjb_18bulan, " _
                            & "  ptnratt_suami_nama, " _
                            & "  ptnratt_suami_bekerja, " _
                            & "  ptnratt_suami_jabatan, " _
                            & "  ptnratt_suami_kantor_alamat_1, " _
                            & "  ptnratt_suami_kantor_alamat_2, " _
                            & "  ptnratt_suami_telp, " _
                            & "  ptnratt_suami_hp, " _
                            & "  ptnratt_anak_nama_1, " _
                            & "  ptnratt_anak_tgl_lahir_1, " _
                            & "  ptnratt_anak_sekolah_1, " _
                            & "  ptnratt_anak_nama_2, " _
                            & "  ptnratt_anak_tgl_lahir_2, " _
                            & "  ptnratt_anak_sekolah_2, " _
                            & "  ptnratt_anak_nama_3, " _
                            & "  ptnratt_anak_tgl_lahir_3, " _
                            & "  ptnratt_anak_sekolah_3, " _
                            & "  ptnratt_keluarga_dekat_nama, " _
                            & "  ptnratt_keluarga_dekat_alamat_1, " _
                            & "  ptnratt_keluarga_dekat_alamat_2, " _
                            & "  ptnratt_keluarga_dekat_telp, " _
                            & "  ptnratt_keluarga_dekat_hp, " _
                            & "  ptnratt_status_tempat_tinggal, " _
                            & "  ptnratt_jenis_kartu_kredit, " _
                            & "  ptnratt_no_kartu_kredit, " _
                            & "  ptnratt_berlaku_sd, " _
                            & "  ptnratt_dt, " _
                            & "  ptnratt_bank, " _
                            & "  ptnratt_status_rumah_id, " _
                            & "  ptnratt_lama_tinggal_id, " _
                            & "  ptnratt_masa_kerja_id, " _
                            & "  ptnratt_income_id, " _
                            & "  ptnratt_kepribadian_id, " _
                            & "  ptnratt_tanggungan_id, " _
                            & "  ptnratt_jaminan_id, " _
                            & "  sr.code_name as status_rumah, " _
                            & "  lt.code_name as lama_tinggal, " _
                            & "  ms.code_name as masa_kerja, " _
                            & "  ic.code_name as income, " _
                            & "  tg.code_name as tanggungan, " _
                            & "  kp.code_name as kepribadian, " _
                            & "  ja.code_name as jaminan " _
                            & "FROM  " _
                            & "  public.kjb_mstr " _
                            & "  inner join en_mstr on en_id=kjb_en_id " _
                            & "  inner join soship_mstr on soship_oid=kjb_soship_oid " _
                            & "  inner join ptnr_mstr on ptnr_oid=kjb_ptnr_oid " _
                            & "  LEFT OUTER JOIN public.ptnratt_attr on ptnratt_ptnr_oid = ptnr_oid " _
                            & "  LEFT OUTER JOIN public.code_mstr sr on sr.code_id = ptnratt_status_rumah_id " _
                            & "  LEFT OUTER JOIN public.code_mstr lt on lt.code_id = ptnratt_lama_tinggal_id " _
                            & "  LEFT OUTER JOIN public.code_mstr ms on ms.code_id = ptnratt_masa_kerja_id " _
                            & "  LEFT OUTER JOIN public.code_mstr ic on ic.code_id = ptnratt_income_id " _
                            & "  LEFT OUTER JOIN public.code_mstr tg on tg.code_id = ptnratt_tanggungan_id " _
                            & "  LEFT OUTER JOIN public.code_mstr kp on tg.code_id = ptnratt_kepribadian_id " _
                            & "  LEFT OUTER JOIN public.code_mstr ja on tg.code_id = ptnratt_jaminan_id " _
                            & "  where kjb_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("kjb_oid").ToString + "'"


        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRKjb"
        'frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_code")
        frm.ShowDialog()

    End Sub


    Private Sub ce_page_number_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ce_page_number.CheckedChanged
        XRFakturPajakFormPlain.StatusForm = "1"
        'XRFakturPajakForm.StatusFormPajak = "1"
        If ce_page_number.Checked = True Then
            PageNum2 = "Y"
        ElseIf ce_page_number.Checked = False Then
            PageNum2 = "N"
        End If
    End Sub

    Private Sub kjb_soship_oid_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles kjb_soship_oid.EditValueChanged

    End Sub

    Private Sub gc_master_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gc_master.Click

    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        Try
            load_data_grid_detail()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CheckEdit4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles kjb_insza.CheckedChanged

    End Sub

    Private Sub CheckEdit9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles kjb_6bulan.CheckedChanged
        Try
            kjb_2bulan.Checked = False
            kjb_tunai.Checked = False
            kjb_12bulan.Checked = False
            kjb_18bulan.Checked = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub kjb_tunai_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles kjb_tunai.CheckedChanged
        Try
            kjb_2bulan.Checked = False
            kjb_6bulan.Checked = False
            kjb_12bulan.Checked = False
            kjb_18bulan.Checked = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub kjb_2bulan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles kjb_2bulan.CheckedChanged
        Try
            kjb_tunai.Checked = False
            kjb_6bulan.Checked = False
            kjb_12bulan.Checked = False
            kjb_18bulan.Checked = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub kjb_12bulan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles kjb_12bulan.CheckedChanged
        Try
            kjb_2bulan.Checked = False
            kjb_tunai.Checked = False
            kjb_6bulan.Checked = False
            kjb_18bulan.Checked = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub kjb_18bulan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles kjb_18bulan.CheckedChanged
        Try
            kjb_2bulan.Checked = False
            kjb_tunai.Checked = False
            kjb_6bulan.Checked = False
            kjb_12bulan.Checked = False
        Catch ex As Exception

        End Try
    End Sub
End Class
