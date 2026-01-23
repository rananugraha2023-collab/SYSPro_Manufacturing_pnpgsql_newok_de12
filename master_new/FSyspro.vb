Public Class FSyspro

    Dim column As DevExpress.XtraGrid.Columns.GridColumn
    Dim ds As New DataSet

    Private Sub FSyspro_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        format_grid()
        load_data(True)
        If ClsVar.sUserID = 8 Or ClsVar.sUserID = 3 Then
            xtp_rule.PageVisible = True
            xtp_format.PageVisible = True
        Else
            xtp_rule.PageVisible = False
            xtp_format.PageVisible = False
        End If
    End Sub

    Private Sub format_grid()
        add_column(gv_master, "NIK", "nik", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Nama Karyawan", "nama_depan", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Dep./Divisi", "departement", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Tanggal Lahir", "tgl_lahir", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yyyy")
    End Sub

    Private Sub load_data(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            ''================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        .SQL = "select nik, nama_depan, coalesce(departement,divisi) as departement, tgl_lahir" + _
                               " from tkaryawan k " + _
                               " left outer join tkaryawan_kerja kk on kk.id_karyawan = k.id_karyawan " + _
                               " left outer join tdepartement d on d.id_departement = kk.id_departement " + _
                               " left outer join tdivisi v on v.id_divisi = kk.id_divisi " + _
                               " where substring(cast(tgl_lahir as varchar),9,2) = (substring(cast(current_date as varchar),9,2)) " + _
                               " and substring(cast(tgl_lahir as varchar),6,2) = (substring(cast(current_date as varchar),6,2)) "
                        .InitializeCommand()
                        .FillDataSet(ds, Me.Name + "_select")
                        gc_master.DataSource = ds.Tables(0)

                        .SQL = "select k.id_karyawan,k.nik,nama_depan,jenis_kelamin, " + _
                             " tmp_lahir,tgl_lahir,warga_negara,alamat," + _
                             " kota,kode_pos,telp1,telp2,telp_rumah," + _
                             " email_perusahaan,email_pribadi," + _
                             " k.status_karyawan," + _
                             " ag.agama,d.darah,n.nikah,j.jenis," + _
                             " sdp.sub_departement, " + _
                             " tgl_awal_kerja,masa_kerja,tgl_karyawan_tetap, " + _
                             " masa_kerja_tetap, tgl_awal_kontrak,tgl_akhir_kontrak, " + _
                             " p.nama_perusahaan,klk.kelompok_kerja,dr.direktorat,dp.departement,dv.divisi,sd.sub_divisi, " + _
                             " kj.kelompok_jabatan,lv.level,es.eselon,lk.lokasi_kerja,jl.job_level, " + _
                             " jg.job_grading,gl.golongan,jb.jabatan,ar.area " + _
                             " from  tkaryawan k " + _
                             " left outer join tagama ag on ag.id_agama = k.id_agama " + _
                             " left outer join tdarah d on d.id_darah = k.id_darah " + _
                             " left outer join tnikah n on n.id_nikah = k.id_nikah " + _
                             " left outer join tjenis j on j.id_jenis = k.id_jenis " + _
                             " left outer join tkaryawan_kerja kk on kk.id_karyawan = k.id_karyawan " + _
                             " left outer join tperusahaan p on p.id_perusahaan = kk.id_perusahaan " + _
                             " left outer join tkelompok_kerja klk on klk.id_kelompok_kerja = kk.id_kelompok_kerja " + _
                             " left outer join tdirektorat dr on dr.id_direktorat = kk.id_direktorat " + _
                             " left outer join tdepartement dp on dp.id_departement = kk.id_departement" + _
                             " left outer join tsub_departement sdp on sdp.id_sub_departement = kk.id_sub_departement" + _
                             " left outer join tdivisi dv on dv.id_divisi = kk.id_divisi " + _
                             " left outer join tsub_divisi sd on sd.id_sub_divisi = kk.id_sub_divisi " + _
                             " left outer join tstatus_karyawan sk on sk.id_status_karyawan = kk.id_status_karyawan " + _
                             " left outer join tkelompok_jabatan kj on kj.id_kelompok_jabatan = kk.id_kelompok_jabatan " + _
                             " left outer join tlevel lv on lv.id_level = kk.id_level " + _
                             " left outer join teselon es on es.id_eselon = kk.id_eselon " + _
                             " left outer join tlokasi_kerja lk on lk.id_lokasi_kerja = kk.id_lokasi_kerja " + _
                             " left outer join tjob_level jl on jl.id_job_level = kk.id_job_level " + _
                             " left outer join tjob_grading jg on jg.id_job_grading = kk.id_job_grading " + _
                             " left outer join tgolongan gl on gl.id_golongan = kk.id_golongan " + _
                             " left outer join tjabatan jb on jb.id_jabatan = kk.id_jabatan " + _
                             " left outer join tarea ar on ar.id_area = kk.id_area " + _
                             " where k.id_karyawan = " + master_new.ClsVar.sKaryawanID.ToString
                        .InitializeCommand()
                        .FillDataSet(ds, "datapribadi")

                        With ds.Tables("datapribadi").Rows(0)
                            lbl_nik.Text = IIf(IsDBNull(.Item("nik")) = True, "", .Item("nik"))
                            lbl_nama.Text = IIf(IsDBNull(.Item("nama_depan")) = True, "", .Item("nama_depan"))
                            lbl_jenis_kelamin.Text = IIf(IsDBNull(.Item("jenis_kelamin")) = True, "", .Item("jenis_kelamin"))
                            lbl_tmplahir.Text = IIf(IsDBNull(.Item("tmp_lahir")) = True, "", .Item("tmp_lahir"))
                            lbl_tgllahir.Text = IIf(IsDBNull(.Item("tgl_lahir")) = True, "", .Item("tgl_lahir"))
                            lblagama.Text = IIf(IsDBNull(.Item("agama")) = True, "", .Item("agama"))
                            lbl_warganegara.Text = IIf(IsDBNull(.Item("warga_negara")) = True, "", .Item("warga_negara"))
                            lbl_goldar.Text = IIf(IsDBNull(.Item("darah")) = True, "", .Item("darah"))
                            lbl_nikah.Text = IIf(IsDBNull(.Item("nikah")) = True, "", .Item("nikah"))
                            lbl_alamat.Text = IIf(IsDBNull(.Item("alamat")) = True, "", .Item("alamat"))
                            lbl_kota.Text = IIf(IsDBNull(.Item("kota")) = True, "", .Item("kota"))
                            lbl_kodepos.Text = IIf(IsDBNull(.Item("kode_pos")) = True, "", .Item("kode_pos"))
                            lbl_hp1.Text = IIf(IsDBNull(.Item("telp1")) = True, "", .Item("telp1"))
                            lbl_hp2.Text = IIf(IsDBNull(.Item("telp2")) = True, "", .Item("telp2"))
                            lbl_telprumah.Text = IIf(IsDBNull(.Item("telp_rumah")) = True, "", .Item("telp_rumah"))
                            lbl_emailperusahaan.Text = IIf(IsDBNull(.Item("email_perusahaan")) = True, "", .Item("email_perusahaan"))
                            lbl_emailpribadi.Text = IIf(IsDBNull(.Item("email_pribadi")) = True, "", .Item("email_pribadi"))
                            lbl_perusahaan.Text = IIf(IsDBNull(.Item("nama_perusahaan")) = True, "", .Item("nama_perusahaan"))
                            lbl_direktorat.Text = IIf(IsDBNull(.Item("direktorat")) = True, "", .Item("direktorat"))
                            lbl_divisi.Text = IIf(IsDBNull(.Item("divisi")) = True, "", .Item("divisi"))
                            lbl_subdivisi.Text = IIf(IsDBNull(.Item("sub_divisi")) = True, "", .Item("sub_divisi"))
                            lbl_departement.Text = IIf(IsDBNull(.Item("departement")) = True, "", .Item("departement"))
                            lbl_subdepartement.Text = IIf(IsDBNull(.Item("sub_departement")) = True, "", .Item("sub_departement"))
                            lbl_status_karyawan.Text = IIf(IsDBNull(.Item("status_karyawan")) = True, "", .Item("status_karyawan"))
                            lbl_kelompokkerja.Text = IIf(IsDBNull(.Item("kelompok_kerja")) = True, "", .Item("kelompok_kerja"))
                            lbl_kelompokjabatan.Text = IIf(IsDBNull(.Item("kelompok_jabatan")) = True, "", .Item("kelompok_jabatan"))
                            lbl_eselon.Text = IIf(IsDBNull(.Item("eselon")) = True, "", .Item("eselon"))
                            lbl_joblabel.Text = IIf(IsDBNull(.Item("job_level")) = True, "", .Item("job_level"))
                            lbl_golongan.Text = IIf(IsDBNull(.Item("golongan")) = True, "", .Item("golongan"))
                            lbl_jabatan.Text = IIf(IsDBNull(.Item("jabatan")) = True, "", .Item("jabatan"))
                            lbl_lokasikerja.Text = IIf(IsDBNull(.Item("lokasi_kerja")) = True, "", .Item("lokasi_kerja"))
                            lbl_mulaikerja.Text = IIf(IsDBNull(.Item("tgl_awal_kerja")) = True, "", .Item("tgl_awal_kerja"))
                            lbl_masakerja.Text = IIf(IsDBNull(.Item("masa_kerja")) = True, "", .Item("masa_kerja"))
                            lbl_area.Text = IIf(IsDBNull(.Item("area")) = True, "", .Item("area"))

                            Dim status_file As String = DevExpress.Utils.FilesHelper.FindingFileName("\\192.50.1.3\syspro programe\image\", .Item("nik") + ".jpg", False)
                            If status_file <> "" Then
                                PictureBox7.ImageLocation = "\\192.50.1.3\syspro programe\image\" + .Item("nik") + ".jpg"
                            Else
                                PictureBox7.ImageLocation = "\\192.50.1.3\syspro programe\image\non.gif"
                            End If
                        End With
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub
#Region "add_column"
    Public Overridable Sub add_column(ByVal gv As DevExpress.XtraGrid.Views.Card.CardView, ByVal par_fn As String, ByVal par_visible As Boolean)
        column = New DevExpress.XtraGrid.Columns.GridColumn

        gv.Columns.Add(column)
        column.Caption = par_fn
        column.FieldName = par_fn
        column.Name = par_fn
        column.Visible = par_visible
    End Sub

    Public Overridable Sub add_column(ByVal gv As DevExpress.XtraGrid.Views.Card.CardView, ByVal par_caption As String, ByVal par_fn As String, ByVal par_align As DevExpress.Utils.HorzAlignment)
        column = New DevExpress.XtraGrid.Columns.GridColumn

        gv.Columns.Add(column)
        column.Caption = par_caption
        column.FieldName = par_fn
        column.Name = par_fn
        column.Visible = True
        column.OptionsColumn.AllowEdit = False
        column.OptionsColumn.AllowIncrementalSearch = True
        column.AppearanceCell.TextOptions.HAlignment = par_align
    End Sub

    Public Overridable Sub add_column(ByVal gv As DevExpress.XtraGrid.Views.Card.CardView, ByVal par_caption As String, ByVal par_fn As String, ByVal par_align As DevExpress.Utils.HorzAlignment, ByVal formatType As DevExpress.Utils.FormatType, ByVal formatString As String)
        column = New DevExpress.XtraGrid.Columns.GridColumn

        gv.Columns.Add(column)
        column.Caption = par_caption
        column.FieldName = par_fn
        column.Name = par_fn
        column.Visible = True
        column.OptionsColumn.AllowEdit = False
        column.OptionsColumn.AllowIncrementalSearch = True
        column.AppearanceCell.TextOptions.HAlignment = par_align

        column.DisplayFormat.FormatType = formatType
        column.DisplayFormat.FormatString = formatString
    End Sub

    Public Overridable Sub add_column(ByVal gv As DevExpress.XtraGrid.Views.Card.CardView, ByVal par_caption As String, ByVal par_fn As String, ByVal par_align As DevExpress.Utils.HorzAlignment, ByVal formatType As DevExpress.Utils.FormatType, ByVal formatString As String, ByVal par_summarytype As DevExpress.Data.SummaryItemType, ByVal par_displayformat As String)
        column = New DevExpress.XtraGrid.Columns.GridColumn

        gv.Columns.Add(column)
        column.Caption = par_caption
        column.FieldName = par_fn
        column.Name = par_fn
        column.Visible = True
        column.OptionsColumn.AllowEdit = False
        column.OptionsColumn.AllowIncrementalSearch = True
        column.AppearanceCell.TextOptions.HAlignment = par_align

        column.DisplayFormat.FormatType = formatType
        column.DisplayFormat.FormatString = formatString
        column.SummaryItem.SummaryType = par_summarytype
        column.SummaryItem.DisplayFormat = par_displayformat
        'gv.GroupSummary.Add(par_summarytype, par_fn, column, par_displayformat)
    End Sub
#End Region

    Private Sub gv_master_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_master.FocusedRowChanged
        Dim status_file As String = DevExpress.Utils.FilesHelper.FindingFileName("\\192.50.1.3\syspro programe\image\", ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("nik") + ".jpg", False)
        If status_file <> "" Then
            PictureBox6.ImageLocation = "\\192.50.1.3\syspro programe\image\" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("nik") + ".jpg"
        Else
            PictureBox6.ImageLocation = "\\192.50.1.3\syspro programe\image\non.jpg"
        End If
    End Sub

    Private Sub hl_mgpspd_OpenLink(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.OpenLinkEventArgs) Handles hl_mgpspd.OpenLink
        Dim myprocess As New Process
        myprocess.StartInfo.FileName = "\\192.50.1.3\syspro programe\manual guide\Manual Guide Paperless System Permohonan Dana.pdf"
        myprocess.Start()
    End Sub

    Private Sub hl_mgeappd_OpenLink(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.OpenLinkEventArgs) Handles hl_mgeappd.OpenLink
        Dim myprocess As New Process
        myprocess.StartInfo.FileName = "\\192.50.1.3\syspro programe\manual guide\Manual Guide e-Approval System Permohonan Dana.pdf"
        myprocess.Start()
    End Sub
End Class

