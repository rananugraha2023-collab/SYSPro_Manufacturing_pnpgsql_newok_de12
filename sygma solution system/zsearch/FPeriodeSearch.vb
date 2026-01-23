Public Class FPeriodeSearch
    Public _row As Integer
    Public grid_call As String = ""
    Public _obj As Object
    Public _objk As Object
    Public _en_id, _code As Integer
    Public _year As Date
    Public _years As String

    Private Sub FPeriodeSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        'If fobject.name = "FGenFSN" Or fobject.name = FSalesPlanImportNew.Name Or fobject.name = FBonusInsentifGenerator.Name Or fobject.name = FSalesPlanImportNewTarget.Name Or fobject.name = FSalesPlanImportTarget2.Name Then
        If fobject.name = "FGenFSN" Or fobject.name = FSalesPlanImportNew.Name Or fobject.name = FBonusInsentifGenerator.Name Or fobject.name = FBonusInsentifMitraGenerator.Name Or fobject.name = FSalesPlanImportNewTarget.Name Then
            add_column(gv_master, "Code", "qrtr_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Name", "qrtr_name", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Start Date", "qrtr_start_date", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "End Date", "qrtr_end_date", DevExpress.Utils.HorzAlignment.Default)

            'fobject._genfsn_qrtr_ids = ds.Tables(0).Rows(_row_gv).Item("qrtr_id")
            'fobject.genfsn_qrtr_id.text = ds.Tables(0).Rows(_row_gv).Item("qrtr_name")
        Else
            add_column(gv_master, "Code", "periode_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Start Date", "periode_start_date", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "End Date", "periode_end_date", DevExpress.Utils.HorzAlignment.Default)
        End If

    End Sub

    Public Overrides Function get_sequel() As String
        If fobject.name = "FGenFSN" Or fobject.name = FBonusInsentifMitraGenerator.Name Then
            get_sequel = "SELECT  " _
                    & "  a.qrtr_oid, " _
                    & "  a.qrtr_id, " _
                    & "  a.qrtr_code, " _
                    & "  a.qrtr_name, " _
                    & "  a.qrtr_year, " _
                    & "  a.qrtr_start_date, " _
                    & "  a.qrtr_end_date, " _
                    & "  a.qrtr_add_by, " _
                    & "  a.qrtr_add_date, " _
                    & "  a.qrtr_upd_by, " _
                    & "  a.qrtr_upd_date, " _
                    & "  a.qrtr_code_id, " _
                    & "  a.qrtr_code_name " _
                    & "FROM " _
                    & "  public.qrtr_mstr a " _
                    & "WHERE " _
                    & "  a.qrtr_code_id = " + _code.ToString _
                    & "  and a.qrtr_year = " + _years.ToString

        ElseIf fobject.name = "FBonusInsentifGenerator" Then
            If _obj.ToString().Equals("Monthlly") Then
                get_sequel = "SELECT qrtr_id, " _
                    & "  qrtr_code, " _
                    & "  qrtr_name, " _
                    & "  qrtr_code_id, " _
                    & "  qrtr_start_date, " _
                    & "  qrtr_end_date " _
                    & "FROM qrtr_mstr " _
                    & "WHERE qrtr_start_date >= " _
                    & "  ( " _
                    & "    SELECT qrtr_start_date " _
                    & "    FROM qrtr_mstr " _
                    & "    WHERE qrtr_id = " + _code.ToString _
                    & ") AND " _
                    & "  qrtr_end_date <= " _
                    & "  ( " _
                    & "    SELECT qrtr_end_date " _
                    & "    FROM qrtr_mstr " _
                    & "    WHERE qrtr_id = " + _code.ToString _
                    & "); " _
                    & ""
            Else
                get_sequel = "SELECT qrtr_id, " _
                    & "  qrtr_code, " _
                    & "  qrtr_name, " _
                    & "  qrtr_code_id, " _
                    & "  qrtr_start_date, " _
                    & "  qrtr_end_date " _
                    & "FROM qrtr_mstr " _
                    & "WHERE qrtr_id <> " + _code.ToString _
                    & "  AND " _
                    & "  qrtr_start_date >= " _
                    & "  ( " _
                    & "    SELECT qrtr_start_date " _
                    & "    FROM qrtr_mstr " _
                    & "    WHERE qrtr_id = " + _code.ToString _
                    & ") AND " _
                    & "  qrtr_end_date <= " _
                    & "  ( " _
                    & "    SELECT qrtr_end_date " _
                    & "    FROM qrtr_mstr " _
                    & "    WHERE qrtr_id = " + _code.ToString _
                    & "); " _
                    & ""
            End If

        ElseIf fobject.name = "FSalesPlanImportNewTarget" Or fobject.name = FSalesPlanImportTarget2.Name Then
            get_sequel = "SELECT qrtr_id, " _
                & "  qrtr_code, " _
                & "  qrtr_name, " _
                & "  qrtr_code_id, " _
                & "  qrtr_start_date, " _
                & "  qrtr_end_date " _
                & "FROM qrtr_mstr " _
                & "WHERE qrtr_year = " + _code.ToString _
                & ""

        Else
            get_sequel = "SELECT  " _
                                & "  a.periode_code, " _
                                & "  a.periode_start_date, " _
                                & "  a.periode_end_date, " _
                                & "  a.periode_active, " _
                                & "  a.periode_add_by, " _
                                & "  a.periode_add_date, " _
                                & "  a.periode_upd_by, " _
                                & "  a.periode_upd_date, " _
                                & "  a.periode_oid " _
                                & "FROM " _
                                & "  public.psperiode_mstr a " _
                                & "  where a.periode_active ~~* 'Y' "


        End If

        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        'gv_master.Focus()
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

    Private Sub fill_data()
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position

        If fobject.name = "FGenFSN" Then
            '_obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("qrtr_name")
            '_obj.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("qrtr_id")
            fobject.genfsn_start_dt.editvalue = ds.Tables(0).Rows(_row_gv).Item("qrtr_start_date")
            fobject.genfsn_end_dt.editvalue = ds.Tables(0).Rows(_row_gv).Item("qrtr_end_date")
            fobject._genfsn_qrtr_ids = ds.Tables(0).Rows(_row_gv).Item("qrtr_id")
            fobject.genfsn_qrtr_id.text = ds.Tables(0).Rows(_row_gv).Item("qrtr_name")

        ElseIf fobject.name = "FSalesPlanImportNew" Then
            fobject.plans_start_dt.editvalue = ds.Tables(0).Rows(_row_gv).Item("qrtr_start_date")
            fobject.plans_end_dt.editvalue = ds.Tables(0).Rows(_row_gv).Item("qrtr_end_date")
            fobject._plans_periodes = ds.Tables(0).Rows(_row_gv).Item("qrtr_id")
            fobject.plans_periode.text = ds.Tables(0).Rows(_row_gv).Item("qrtr_name")

        ElseIf fobject.name = "FSalesPlanImportNewTarget" Then
            fobject.plans_start_dt.editvalue = ds.Tables(0).Rows(_row_gv).Item("qrtr_start_date")
            fobject.plans_end_dt.editvalue = ds.Tables(0).Rows(_row_gv).Item("qrtr_end_date")
            fobject.plans_qrtr_periode_id.Tag = ds.Tables(0).Rows(_row_gv).Item("qrtr_id")
            fobject.plans_qrtr_periode_id.Text = ds.Tables(0).Rows(_row_gv).Item("qrtr_name")

        ElseIf fobject.name = "FBonusInsentifGenerator" Then
            '_obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("periode_code")
            '_obj.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("periode_oid")
            fobject.geninstf_qrtr_id.tag = ds.Tables(0).Rows(_row_gv).Item("qrtr_id")
            fobject.geninstf_qrtr_id.text = ds.Tables(0).Rows(_row_gv).Item("qrtr_name")
            fobject.geninstf_qrtr_start_date.editvalue = ds.Tables(0).Rows(_row_gv).Item("qrtr_start_date")
            fobject.geninstf_qrtr_end_date.editvalue = ds.Tables(0).Rows(_row_gv).Item("qrtr_end_date")
            fobject.geninstf_qrtr_year.editvalue = ds.Tables(0).Rows(_row_gv).Item("qrtr_start_date")

        ElseIf fobject.name = "FBonusInsentifMitraGenerator" Then
            '_obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("periode_code")
            '_obj.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("periode_oid")
            fobject.genmtr_qrtr_id.tag = ds.Tables(0).Rows(_row_gv).Item("qrtr_id")
            fobject.genmtr_qrtr_id.text = ds.Tables(0).Rows(_row_gv).Item("qrtr_name")
            fobject.genmtr_start_dt.editvalue = ds.Tables(0).Rows(_row_gv).Item("qrtr_start_date")
            fobject.genmtr_end_dt.editvalue = ds.Tables(0).Rows(_row_gv).Item("qrtr_end_date")
            'fobject.genmtr_year.editvalue = ds.Tables(0).Rows(_row_gv).Item("qrtr_start_date")

        ElseIf fobject.name = "FSalesPlanImportTarget" Then
            fobject.gv_edit.SetRowCellValue(_row, "planstgtd_qrtr_id", ds.Tables(0).Rows(_row_gv).Item("qrtr_id"))
            fobject.gv_edit.SetRowCellValue(_row, "qrtr_name", ds.Tables(0).Rows(_row_gv).Item("qrtr_name"))
            fobject.gv_edit.SetRowCellValue(_row, "planstgtd_qrtr_start_date", ds.Tables(0).Rows(_row_gv).Item("qrtr_start_date"))
            fobject.gv_edit.SetRowCellValue(_row, "planstgtd_qrtr_end_date", ds.Tables(0).Rows(_row_gv).Item("qrtr_end_date"))
        Else
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("periode_code")
            _obj.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("periode_oid")
            fobject.inv_fsn_start_date.editvalue = ds.Tables(0).Rows(_row_gv).Item("periode_start_date")
            fobject.inv_fsn_end_date.editvalue = ds.Tables(0).Rows(_row_gv).Item("periode_end_date")


        End If

    End Sub
End Class
