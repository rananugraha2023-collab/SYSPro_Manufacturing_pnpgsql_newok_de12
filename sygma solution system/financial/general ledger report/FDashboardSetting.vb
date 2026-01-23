Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FDashboardSetting
    Dim _loc_oid_mstr As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FDomain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'Ds_invoice1.DataTable1' table. You can move, or remove it, as needed.
        'Me.DataTable1TableAdapter.Fill(Me.Ds_invoice1.DataTable1)
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        Try

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Caption", "dash_type_filter", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "dash_type_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Caption", "dash_type_number", DevExpress.Utils.HorzAlignment.Default)


        add_column_copy(gv_master, "User Create", "dash_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "dash_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "dash_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "dash_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")


        add_column(gv_edit, "Account ID", "dashd_ac_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account Code Hirarki", "ac_code_hirarki", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail, "dashd_dash_type_id", False)
        add_column(gv_detail, "Account ID", "dashd_ac_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Account Code Hirarki", "ac_code_hirarki", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
            & " * " _
            & "FROM " _
            & "  public.dashboard_setting a " _
            & "ORDER BY " _
            & "  a.dash_type_filter, " _
            & "  a.dash_type_number"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        cancel_data()
        Box("Menu not available")
    End Sub
    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function
    Public Overrides Function insert() As Boolean
        insert = True
        Box("Menu not available")
        Return insert
    End Function
    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If
        Dim sql As String
        Try
            Try
                ds.Tables("detail").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                & "  public.dashboard_setting_account.dashd_oid, " _
                & "  public.dashboard_setting_account.dashd_dash_type_id, " _
                & "  public.dashboard_setting_account.dashd_ac_id, " _
                & "  public.ac_mstr.ac_code,public.ac_mstr.ac_code_hirarki, " _
                & "  public.ac_mstr.ac_name " _
                & "FROM " _
                & "  public.dashboard_setting_account " _
                & "  INNER JOIN public.ac_mstr ON (public.dashboard_setting_account.dashd_ac_id = public.ac_mstr.ac_id) " _
                & "WHERE " _
                & "  1=1 " _
                & "ORDER BY " _
                & "  public.ac_mstr.ac_name"



            load_data_detail(sql, gc_detail, "detail")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("dashd_dash_type_id").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("dashd_dash_type_id='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dash_type_id").ToString & "'")
            gv_detail.BestFitColumns()

        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            dash_type_filter.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                dash_type_filter.Text = .Item("dash_type_filter")
                dash_type_id.Text = SetString(.Item("dash_type_id"))
                dash_type_number.EditValue = .Item("dash_type_number")
              
            End With

            Dim sql As String

            Try
                Try
                    ds.Tables("edit").Clear()
                Catch ex As Exception
                End Try



                sql = "SELECT  " _
                    & "  public.dashboard_setting_account.dashd_oid, " _
                    & "  public.dashboard_setting_account.dashd_dash_type_id, " _
                    & "  public.dashboard_setting_account.dashd_ac_id,public.ac_mstr.ac_code_hirarki, " _
                    & "  public.ac_mstr.ac_code, " _
                    & "  public.ac_mstr.ac_name " _
                    & "FROM " _
                    & "  public.dashboard_setting_account " _
                    & "  INNER JOIN public.ac_mstr ON (public.dashboard_setting_account.dashd_ac_id = public.ac_mstr.ac_id) " _
                    & "WHERE " _
                    & "  public.dashboard_setting_account.dashd_dash_type_id = '" & ds.Tables(0).Rows(row).Item("dash_type_id") & "' " _
                    & "ORDER BY " _
                    & "  public.ac_mstr.ac_name"

                load_data_detail(sql, GC_Edit, "edit")

            Catch ex As Exception
                Pesan(Err)
            End Try

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim ssql As String
        Dim ssqls As New ArrayList
        Try

            GC_Edit.EmbeddedNavigator.Buttons.DoClick(GC_Edit.EmbeddedNavigator.Buttons.EndEdit)
            ds.Tables("edit").AcceptChanges()

            'ssql = "update  bsdi_det_item set bsdi_caption='" & bsdi_caption.EditValue & "'  where bsdi_oid='" & bsdi_caption.Tag & "'"

            'ssqls.Add(ssql)


            ssql = "delete from dashboard_setting_account where dashd_dash_type_id='" & ds.Tables(0).Rows(row).Item("dash_type_id") & "'"

            ssqls.Add(ssql)

            For i As Integer = 0 To ds.Tables("edit").Rows.Count - 1
                With ds.Tables("edit").Rows(i)

                    ssql = "INSERT INTO  " _
                        & "  public.dashboard_setting_account " _
                        & "( " _
                        & "  dashd_oid, " _
                        & "  dashd_dash_type_id, " _
                        & "  dashd_ac_id " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(ds.Tables(0).Rows(row).Item("dash_type_id")) & ",  " _
                        & SetInteger(.Item("dashd_ac_id")) & "  " _
                        & ")"

                    ssqls.Add(ssql)
                    'Exit Try
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

            edit = True
            after_success()
            set_row(Trim(dash_type_id.Text), "dash_type_id")
            DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
        Catch ex As Exception
            Pesan(Err)
            edit = False
        End Try
        Return edit
    End Function

    Public Overrides Function delete_data() As Boolean
        'delete_data = True
        'Box("Menu not available")
        'Return delete_data

        delete_data = False

        ' gv_master_SelectionChanged(Nothing, Nothing)

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

        'If before_delete() = True Then
        '    row = BindingContext(ds.Tables(0)).Position

        '    If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
        '        row = row - 1
        '    ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
        '        row = 0
        '    End If

        '    Try
        '        With ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position)

        '            sSQL = "DELETE FROM  " _
        '                & "  public.bsdi_det_item  " _
        '                & "WHERE  " _
        '                & "  bsdi_oid ='" & .Item("bsdi_oid") & "'"

        '            ssqls.Add(sSQL)


        '        End With

        '        If master_new.PGSqlConn.status_sync = True Then
        '            If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
        '                Return False
        '                Exit Function
        '            End If
        '            ssqls.Clear()
        '        Else
        '            If DbRunTran(ssqls, "") = False Then
        '                Return False
        '                Exit Function
        '            End If
        '            ssqls.Clear()
        '        End If

        '        help_load_data(True)
        '        MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)

        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message)
        '    End Try
        'End If

        Return delete_data

    End Function

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        Try
            Dim _col As String = gv_edit.FocusedColumn.Name
            Dim _row As Integer = gv_edit.FocusedRowHandle

            If _col = "dashd_ac_id" Or _col = "ac_code" Or _col = "ac_name" Or _col = "ac_code_hirarki" Then
                Dim frm As New FAccountSearch
                frm.set_win(Me)
                frm._row = _row
                frm.type_form = True
                frm.ShowDialog()
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub CheckAccountToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckAccountToolStripMenuItem.Click
        Try

        Catch ex As Exception

        End Try
    End Sub
End Class
