Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FBOMSearch
    Public _row, _en_id As Integer
    Public _obj As Object
    Public _type As String
    Public _filter As String

    Public _site_id As String

    Private Sub FSiteSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        'Me.Width = 371
        'Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "BOM Code", "bom_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "BOM Description", "bom_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Part Number Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Part Number Desc", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.bom_oid, " _
                & "  a.bom_dom_id, " _
                & "  a.bom_en_id, " _
                & "  a.bom_id, " _
                & "  a.bom_code, " _
                & "  a.bom_desc, " _
                & "  a.bom_um_id, " _
                & "  a.bom_active, " _
                & "  a.bom_dt, " _
                & "  c.en_desc, " _
                & "  a.bom_pt_id, " _
                & "  d.pt_code, " _
                & "  d.pt_desc1 " _
                & "FROM " _
                & "  public.bom_mstr a " _
                & "  INNER JOIN public.en_mstr c ON (a.bom_en_id = c.en_id) " _
                & "  INNER JOIN public.pt_mstr d ON (a.bom_pt_id = d.pt_id) " _
                & " Where a.bom_en_id in (0," + _en_id.ToString + ") and  a.bom_desc ~~* '%" + Trim(te_search.Text) + "%' and a.bom_active='Y' "

        If _filter <> "" Then
            get_sequel += _filter
        End If

        get_sequel += "  order by a.bom_desc"

        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            fill_data()
            Me.Close()
        End If
    End Sub

    Public Overrides Sub fill_data()
        Dim sSQL As String
        Try
            Dim _row_gv As Integer
            _row_gv = BindingContext(ds.Tables(0)).Position

            If fobject.name = "FProductStructure" Then
                If _type = "detail" Then
                    'fobject.ds_edit


                    sSQL = "SELECT  " _
                        & "  a.psd_pt_bom_id, " _
                        & "  c.pt_code, " _
                        & "  c.pt_desc1, " _
                        & "  a.psd_comp, " _
                        & "  a.psd_qty, " _
                        & "  a.psd_scrp_pct, " _
                        & "  a.psd_op, " _
                        & "  a.psd_um, " _
                        & "  d.code_name, " _
                        & "  a.psd_um_conv, " _
                        & "  a.psd_qty_real " _
                        & "FROM " _
                        & "  public.psd_det a " _
                        & "  INNER JOIN public.ps_mstr b ON (a.psd_ps_oid = b.ps_oid) " _
                        & "  INNER JOIN public.pt_mstr c ON (a.psd_pt_bom_id = c.pt_id) " _
                        & "  INNER JOIN public.code_mstr d ON (a.psd_um = d.code_id) " _
                        & "WHERE " _
                        & "  b.ps_bom_id = " & ds.Tables(0).Rows(_row_gv).Item("bom_id")

                    Dim dt As New DataTable
                    dt = GetTableData(sSQL)


                    For Each dr As DataRow In dt.Rows
                        Dim _dtrow As DataRow
                        _dtrow = fobject.ds_edit.tables(0).NewRow

                        _dtrow("psd_pt_bom_id") = dr("psd_pt_bom_id")
                        _dtrow("pt_code") = SetString(dr("pt_code"))
                        _dtrow("pt_desc1") = SetString(dr("pt_desc1"))

                        _dtrow("psd_comp") = ds.Tables(0).Rows(_row_gv).Item("bom_code")
                        _dtrow("psd_desc") = ds.Tables(0).Rows(_row_gv).Item("bom_desc")
                        _dtrow("psd_qty") = dr("psd_qty")
                        _dtrow("psd_scrp_pct") = dr("psd_scrp_pct")
                        _dtrow("psd_op") = dr("psd_op")

                        _dtrow("psd_um") = dr("psd_um")
                        _dtrow("code_code") = SetString(dr("pt_desc1"))

                        _dtrow("psd_um_conv") = dr("psd_um_conv")
                        _dtrow("psd_qty_real") = dr("psd_qty_real")

                        fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
                        fobject.ds_edit.Tables(0).AcceptChanges()

                    Next

                    fobject.gv_edit.BestFitColumns()

                Else
                    fobject.ps_bom_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("bom_desc")
                    fobject._bom_id = ds.Tables(0).Rows(_row_gv).Item("bom_id")

                    'fobject.ps_code.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_code")
                    fobject.ps_desc.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_code") & ", " & ds.Tables(0).Rows(_row_gv).Item("pt_desc1")

                End If


            ElseIf fobject.name = "FWorkOrder" Then
                _obj.editvalue = ds.Tables(0).Rows(_row_gv).Item("bom_desc")
                fobject.__wo_bom_id = ds.Tables(0).Rows(_row_gv).Item("bom_id")

            ElseIf fobject.name = "FProject" Then
                '_obj.editvalue = ds.Tables(0).Rows(_row_gv).Item("bom_desc")
                'fobject.__prj_bom_id = ds.Tables(0).Rows(_row_gv).Item("bom_id")

                Dim _cost, _qty, _total As Double

                _obj.editvalue = ds.Tables(0).Rows(_row_gv).Item("bom_desc")
                fobject.__prj_bom_id = ds.Tables(0).Rows(_row_gv).Item("bom_id")

                sSQL = "SELECT  " _
                   & "  a.psd_pt_bom_id, " _
                   & "  c.pt_code, " _
                   & "  c.pt_desc1, " _
                   & "  a.psd_comp, " _
                   & "  a.psd_qty, " _
                   & "  a.psd_scrp_pct, " _
                   & "  a.psd_op, " _
                   & "  a.psd_um, " _
                   & "  d.code_name, " _
                   & "  a.psd_um_conv, " _
                   & "  a.psd_qty_real " _
                   & "FROM " _
                   & "  public.psd_det a " _
                   & "  INNER JOIN public.ps_mstr b ON (a.psd_ps_oid = b.ps_oid) " _
                   & "  INNER JOIN public.pt_mstr c ON (a.psd_pt_bom_id = c.pt_id) " _
                   & "  INNER JOIN public.code_mstr d ON (a.psd_um = d.code_id) " _
                   & "WHERE " _
                   & "  b.ps_bom_id = " & ds.Tables(0).Rows(_row_gv).Item("bom_id")

                Dim dt As New DataTable
                dt = GetTableData(sSQL)

                _total = 0

                For Each dr As DataRow In dt.Rows
                    _qty = dr("psd_qty")
                    _cost = get_cost(dr("psd_pt_bom_id"), _site_id)
                    _total = _total + (_qty * _cost)

                Next

                fobject.prj_cost_mtl_est.text = _total

            End If
        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub

End Class
