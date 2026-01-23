Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FPLSetting
    Dim _loc_oid_mstr As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    'FProfitLossPerpetual
    Private Sub FDomain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        Try

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Number", "pl_number", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remark Footer", "pl_footer", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sign", "pl_sign", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Number", "pls_number", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Item", "pls_item", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Value", "pls_value", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "Account ID", "pla_ac_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account Code Hirarki", "pla_ac_hirarki", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail, "pla_pls_oid", False)
        add_column(gv_detail, "Account ID", "pla_ac_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Account Code Hirarki", "pla_ac_hirarki", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
            & "  a.pl_oid, " _
            & "  a.pl_footer, " _
            & "  a.pl_sign, " _
            & "  a.pl_number, " _
            & "  b.pls_oid, " _
            & "  b.pls_pl_oid, " _
            & "  b.pls_item, " _
            & "  b.pls_number, " _
            & "  b.pls_value " _
            & "FROM " _
            & "  public.pl_setting_mstr a " _
            & "  INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid) " _
            & "ORDER BY " _
            & "  a.pl_number, " _
            & "  b.pls_number"

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
            & "  a.pla_ac_id, " _
            & "  a.pla_ac_hirarki, " _
            & "  b.ac_name ,a.pla_pls_oid " _
            & "FROM " _
            & "  public.pl_setting_account a " _
            & "  INNER JOIN public.ac_mstr b ON (a.pla_ac_id = b.ac_id) "
            
            load_data_detail(sql, gc_detail, "detail")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("pla_pls_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("pla_pls_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pls_oid").ToString & "'")
            gv_detail.BestFitColumns()

            'gv_detail.Columns("code").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("code"))
            'gv_detail.BestFitColumns()

        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            pl_number.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                pl_number.Tag = .Item("pl_oid").ToString
                pl_number.Text = .Item("pl_number")
                pl_footer.Text = SetString(.Item("pl_footer"))
                pl_sign.EditValue = .Item("pl_sign")
                pls_number.EditValue = .Item("pls_number")
                pls_number.Tag = .Item("pls_oid").ToString
                pls_item.Text = SetString(.Item("pls_item"))
                pls_value.EditValue = .Item("pls_value")

            End With

            Dim sql As String

            Try
                Try
                    ds.Tables("edit").Clear()
                Catch ex As Exception
                End Try

                sql = "SELECT  " _
                    & "  a.pla_ac_id, " _
                    & "  a.pla_ac_hirarki, " _
                    & "  b.ac_name ,a.pla_pls_oid " _
                    & "FROM " _
                    & "  public.pl_setting_account a " _
                    & "  INNER JOIN public.ac_mstr b ON (a.pla_ac_id = b.ac_id) " _
                    & " Where a.pla_pls_oid='" & ds.Tables(0).Rows(row).Item("pls_oid").ToString & "' "


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

            ssql = "delete from pl_setting_account where pla_pls_oid='" & pls_number.Tag & "'"

            ssqls.Add(ssql)

            For i As Integer = 0 To ds.Tables("edit").Rows.Count - 1
                With ds.Tables("edit").Rows(i)
                    ssql = "INSERT INTO  public.pl_setting_account " _
                        & "( pla_oid,pla_pls_oid,pla_ac_id, " _
                        & "  pla_ac_hirarki " _
                        & ")  " _
                        & "VALUES ( " & SetSetring(Guid.NewGuid.ToString) & "," _
                        & SetSetring(pls_number.Tag) & ",  " _
                        & .Item("pla_ac_id") & ",  " _
                        & SetSetring(.Item("pla_ac_hirarki")) & ")"

                    ssqls.Add(ssql)
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
            set_row(Trim(pl_number.Text), "pl_number")
            DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
        Catch ex As Exception
            Pesan(Err)
            edit = False
        End Try
        Return edit
    End Function

    Public Overrides Function delete_data() As Boolean
        delete_data = True
        Box("Menu not available")
        Return delete_data
    End Function


    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        Try
            Dim _col As String = gv_edit.FocusedColumn.Name
            Dim _row As Integer = gv_edit.FocusedRowHandle
            'Dim _sod_en_id As Integer = gv_edit.GetRowCellValue(_row, "sod_en_id")

            If _col = "pla_ac_id" Or _col = "ac_name" Or _col = "pla_ac_hirarki" Then
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
End Class
