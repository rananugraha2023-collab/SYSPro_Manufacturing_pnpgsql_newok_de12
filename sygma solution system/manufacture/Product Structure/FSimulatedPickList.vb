Imports master_new.PGSqlConn
Imports npgsql
Imports master_new.ModFunction


Public Class FSimulatedPickList
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _wc_oid_mstr As String
    Public _pt_id As String

    Private Sub FWc_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()

        xtc_master.SelectedTabPageIndex = 1
        xtp_edit.PageVisible = False
        xtp_data.PageVisible = True
        xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        le_entity.Properties.DataSource = dt_bantu
        le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        le_entity.ItemIndex = 0

    End Sub

    
    Public Overrides Sub format_grid()
        add_column(gv_master, "psd_pt_bom_id", False)
        add_column_copy(gv_master, "Component", "psd_comp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "psd_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Start Date", "psd_start_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        'add_column_copy(gv_master, "End Date", "psd_end_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Qty", "psd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Str Type Desc", "psdstrname", DevExpress.Utils.HorzAlignment.Far)
        'add_column_copy(gv_master, "Phantom", "par_pt_phantom", DevExpress.Utils.HorzAlignment.Far)
        'add_column_copy(gv_master, "Scrap", "psd_scrp_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")

        add_column_copy(gv_master, "Location", "psd_loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty Available", "psd_loc_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_copy(gv_master, "Serial", "psd_loc_serial", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    'Public Overrides Function get_sequel() As String

    '    get_sequel = "SELECT  psd_comp,   " _
    '            & "psd_desc, psdstrname, psd_start_date, " _
    '            & "psd_end_date, psd_scrp_pct,par_pt_phantom,psd_qty " _
    '            & " from public.get_all_simulated('" & par_pt_id.EditValue & "',  " _
    '            & par_qty.EditValue & ", " & SetBitYN(ce_phantom.EditValue) & "," & le_entity.EditValue & ") "


    '    Return get_sequel
    'End Function

    Public Overrides Sub help_load_data(ByVal par As Boolean)
        Dim dt As New DataTable
        Dim sSQL As String
        Try

            If ce_use_date.EditValue = True Then
                'sSQL = "SELECT  psd_comp,   " _
                '    & "psd_desc, psdstrname, psd_start_date, " _
                '    & "psd_end_date, psd_scrp_pct,par_pt_phantom,sum(psd_qty) as psd_qty,'' as psd_loc_desc,0 as psd_loc_qty,'' as psd_loc_serial " _
                '    & " from public.get_all_simulated('" & par_pt_id.EditValue & "',  " _
                '    & SetNumber(par_qty.EditValue) & ", " & SetBitYN(ce_phantom.EditValue) & "," _
                '    & SetNumber(le_entity.EditValue) & ",'Y', " & SetDate(par_date.DateTime) & ") " _
                '    & "Group by  psd_comp,psd_desc, psdstrname, psd_start_date,psd_end_date, psd_scrp_pct,par_pt_phantom "

                sSQL = "SELECT  psd_comp,   " _
                    & "psd_desc, psd_qty  " _
                    & " from public.get_all_simulated('" & par_pt_id.EditValue & "',  " _
                    & SetNumber(par_qty.EditValue) & ", " & SetBitYN(ce_phantom.EditValue) & "," _
                    & SetNumber(le_entity.EditValue) & ",'Y', " & SetDate(par_date.DateTime) & ") "

            Else
                'sSQL = "SELECT  psd_comp,   " _
                '     & "psd_desc, psdstrname, psd_start_date, " _
                '     & "psd_end_date, psd_scrp_pct,par_pt_phantom,sum(psd_qty) as psd_qty,'' as psd_loc_desc,0 as psd_loc_qty,'' as psd_loc_serial " _
                '     & " from public.get_all_simulated('" & par_pt_id.EditValue & "',  " _
                '     & SetNumber(par_qty.EditValue) & ", " & SetBitYN(ce_phantom.EditValue) & "," _
                '     & SetNumber(le_entity.EditValue) & ",'N', CURRENT_DATE) " _
                '     & "Group by  psd_comp,psd_desc, psdstrname, psd_start_date,psd_end_date, psd_scrp_pct,par_pt_phantom "

                sSQL = "SELECT  psd_comp,   " _
                     & "psd_desc, psd_qty  " _
                     & " from public.get_all_simulated('" & par_pt_id.EditValue & "',  " _
                     & SetNumber(par_qty.EditValue) & ", " & SetBitYN(ce_phantom.EditValue) & "," _
                     & SetNumber(le_entity.EditValue) & ",'N', CURRENT_DATE) "

            End If

            sSQL = "Select psd_comp,psd_desc,sum(psd_qty) as psd_qty,'' as psd_loc_desc,0 as psd_loc_qty,'' as psd_loc_serial from (" _
                & sSQL & ") as temp group by  psd_comp,psd_desc "

            gv_master.OptionsView.ColumnAutoWidth = False
            gv_master.OptionsView.ShowAutoFilterRow = True
            gv_master.OptionsView.ShowFooter = True
            gv_master.OptionsView.ShowGroupedColumns = True
            gv_master.OptionsBehavior.AllowIncrementalSearch = True
            gv_master.OptionsSelection.MultiSelect = True

            dt = GetTableData(sSQL)


            Dim dt_temp As New DataTable

            dt_temp = dt.Copy
            dt_temp.Rows.Clear()

            Dim _dtrow As DataRow
            For Each dr As DataRow In dt.Rows

                sSQL = "SELECT  " _
                    & "  y.loc_desc,x.invc_qty,x.invc_serial " _
                    & "FROM " _
                    & "  public.invc_mstr x " _
                    & "  INNER JOIN public.loc_mstr y ON (x.invc_loc_id = y.loc_id) " _
                    & "  INNER JOIN public.is_mstr z ON (y.loc_is_id = z.is_id)  " _
                    & "   WHERE " _
                    & "  z.is_avail = 'Y' AND x.invc_qty>0 AND  " _
                    & "  x.invc_en_id = " & SetInteger(le_entity.EditValue) & " AND  " _
                    & "  x.invc_pt_id = (select pt_id from pt_mstr where pt_code=" _
                    & SetSetring(dr("psd_comp")) & ") order by x.invc_qty, x.invc_serial"

           

                Dim dt_loc As New DataTable
                dt_loc = GetTableData(sSQL)
                Dim _qty_temp As Double = 0
                Dim _qty_all As Double = dr("psd_qty")
                Dim _qty_temp_loc As Double = 0
                If dt_loc.Rows.Count > 0 Then

                    'If dr("psd_comp") = "CBL0012.50097" Then
                    '    Box("")
                    'End If

                    For Each dr_loc As DataRow In dt_loc.Rows
                        _qty_temp_loc = dr_loc("invc_qty")
                        If _qty_temp_loc <= (_qty_all - _qty_temp) Then

                            _dtrow = dt_temp.NewRow
                            _dtrow("psd_comp") = dr("psd_comp")
                            _dtrow("psd_desc") = dr("psd_desc")
                            _dtrow("psd_loc_desc") = dr_loc("loc_desc")

                            If (_qty_all - _qty_temp) < dr_loc("invc_qty") Then
                                _dtrow("psd_loc_qty") = _qty_all - _qty_temp
                            Else
                                _dtrow("psd_loc_qty") = dr_loc("invc_qty")
                            End If
                            _dtrow("psd_loc_serial") = dr_loc("invc_serial")

                            'If z = dt_loc.Rows.Count - 1 Then
                            '    _dtrow("psd_qty") = _qty_all - _qty_temp
                            'Else
                            _dtrow("psd_qty") = _qty_temp_loc
                            'End If

                            dt_temp.Rows.Add(_dtrow)
                            dt_temp.AcceptChanges()
                            _qty_temp = _qty_temp + dr_loc("invc_qty")
                        Else
                            _dtrow = dt_temp.NewRow
                            _dtrow("psd_comp") = dr("psd_comp")
                            _dtrow("psd_desc") = dr("psd_desc")

                            _dtrow("psd_loc_desc") = dr_loc("loc_desc")
                            _dtrow("psd_loc_qty") = _qty_all - _qty_temp

                            'If z = dt_loc.Rows.Count - 1 Then
                            '    _dtrow("psd_qty") = _qty_all - _qty_temp
                            'Else
                            _dtrow("psd_qty") = _qty_all - _qty_temp
                            'End If
                            _dtrow("psd_loc_serial") = dr_loc("invc_serial")
                            dt_temp.Rows.Add(_dtrow)
                            dt_temp.AcceptChanges()
                            _qty_temp = _qty_temp + _qty_all - _qty_temp
                            Exit For
                        End If

                        'z += 1

                    Next
                    If _qty_temp < _qty_all Then
                        _dtrow = dt_temp.NewRow
                        _dtrow("psd_comp") = dr("psd_comp")
                        _dtrow("psd_desc") = dr("psd_desc")
                        _dtrow("psd_loc_desc") = ""
                        _dtrow("psd_loc_qty") = 0
                        _dtrow("psd_loc_serial") = ""
                        _dtrow("psd_qty") = _qty_all - _qty_temp
                        dt_temp.Rows.Add(_dtrow)
                        dt_temp.AcceptChanges()
                    End If
                Else
                    _dtrow = dt_temp.NewRow
                    _dtrow("psd_comp") = dr("psd_comp")
                    _dtrow("psd_desc") = dr("psd_desc")
                    _dtrow("psd_qty") = dr("psd_qty")
                    _dtrow("psd_loc_desc") = dr("psd_loc_desc")
                    _dtrow("psd_loc_qty") = dr("psd_loc_qty")
                    dt_temp.Rows.Add(_dtrow)
                    dt_temp.AcceptChanges()
                End If
            Next

            gc_master.DataSource = dt_temp
            gv_master.BestFitColumns()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub par_pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles par_pt_id.ButtonClick
        Try
            Dim frm As New FPTBOMSrch
            frm.set_win(Me)
            frm._en_id = "select user_en_id from tconfuserentity where userid = " + master_new.ClsVar.sUserID.ToString + " "
            frm._pil = 1
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception
            Pesan(Err)
        End Try
     
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub


    Private Sub ce_use_date_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ce_use_date.EditValueChanged
        Try
            If ce_use_date.EditValue = True Then
                par_date.Enabled = True
                If par_date.EditValue = Nothing Then
                    par_date.DateTime = Now
                End If
            Else
                par_date.Enabled = False
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
