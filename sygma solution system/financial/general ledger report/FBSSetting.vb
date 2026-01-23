Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FBSSetting
    Dim _loc_oid_mstr As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

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
        add_column_copy(gv_master, "Caption", "bs_caption", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "bs_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Caption", "bsd_caption", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Item Caption", "bsdi_caption", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "Account ID", "bsda_ac_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account Code Hirarki", "bsda_ac_hirarki", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail, "bsda_bsdi_oid", False)
        add_column(gv_detail, "Account ID", "bsda_ac_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Account Code Hirarki", "bsda_ac_hirarki", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
            & "  a.bs_number, " _
            & "  a.bs_caption, " _
            & "  a.bs_group, " _
            & "  a.bs_remarks, " _
            & "  b.bsd_number, " _
            & "  b.bsd_caption, " _
            & "  b.bsd_remarks, " _
            & "  c.bsdi_number, " _
            & "  c.bsdi_caption, " _
            & "  c.bsdi_oid " _
            & "FROM " _
            & "  public.bs_mstr a " _
            & "  INNER JOIN public.bsd_det b ON (a.bs_number = b.bsd_bs_number) " _
            & "  INNER JOIN public.bsdi_det_item c ON (b.bsd_oid = c.bsdi_bsd_oid) " _
            & "ORDER BY " _
            & "  a.bs_number, " _
            & "  b.bsd_number, " _
            & "  c.bsdi_number"

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
            & "  a.bsda_oid, " _
            & "  a.bsda_ac_id, " _
            & "  b.ac_code, " _
            & "  b.ac_name, " _
            & "  a.bsda_ac_hirarki, " _
            & "  a.bsda_bsdi_oid " _
            & "FROM " _
            & "  public.bsda_account a " _
            & "  INNER JOIN public.ac_mstr b ON (a.bsda_ac_id = b.ac_id)"


            load_data_detail(sql, gc_detail, "detail")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("bsda_bsdi_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("bsda_bsdi_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bsdi_oid").ToString & "'")
            gv_detail.BestFitColumns()

        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            bs_caption.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                bs_caption.Text = .Item("bs_caption")
                bs_remarks.Text = SetString(.Item("bs_remarks"))
                bsd_caption.EditValue = .Item("bsd_caption")
                bsdi_caption.EditValue = .Item("bsdi_caption")
                bsdi_caption.Tag = .Item("bsdi_oid").ToString
            End With

            Dim sql As String

            Try
                Try
                    ds.Tables("edit").Clear()
                Catch ex As Exception
                End Try

                sql = "SELECT  " _
                    & "  a.bsda_oid, " _
                    & "  a.bsda_ac_id, " _
                    & "  b.ac_code, " _
                    & "  b.ac_name, " _
                    & "  a.bsda_ac_hirarki, " _
                    & "  a.bsda_bsdi_oid " _
                    & "FROM " _
                    & "  public.bsda_account a " _
                    & "  INNER JOIN public.ac_mstr b ON (a.bsda_ac_id = b.ac_id)" _
                    & " Where a.bsda_bsdi_oid='" & ds.Tables(0).Rows(row).Item("bsdi_oid").ToString & "' "

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

            ssql = "update  bsdi_det_item set bsdi_caption='" & bsdi_caption.EditValue & "'  where bsdi_oid='" & bsdi_caption.Tag & "'"

            ssqls.Add(ssql)


            ssql = "delete from bsda_account where bsda_bsdi_oid='" & bsdi_caption.Tag & "'"

            ssqls.Add(ssql)

            For i As Integer = 0 To ds.Tables("edit").Rows.Count - 1
                With ds.Tables("edit").Rows(i)
                   
                    ssql = "INSERT INTO  " _
                        & "  public.bsda_account " _
                        & "( " _
                        & "  bsda_oid, " _
                        & "  bsda_bsdi_oid, " _
                        & "  bsda_ac_id, " _
                        & "  bsda_ac_hirarki " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(bsdi_caption.Tag) & ",  " _
                        & SetInteger(.Item("bsda_ac_id")) & ",  " _
                        & SetSetring(.Item("bsda_ac_hirarki")) & "  " _
                        & ")"

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
            set_row(Trim(bs_caption.Text), "bsdi_caption")
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

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
            End If

            Try
                With ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position)

                    sSQL = "DELETE FROM  " _
                        & "  public.bsdi_det_item  " _
                        & "WHERE  " _
                        & "  bsdi_oid ='" & .Item("bsdi_oid").ToString & "'"

                    ssqls.Add(sSQL)


                End With

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

                help_load_data(True)
                MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        Return delete_data

    End Function

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        Try
            Dim _col As String = gv_edit.FocusedColumn.Name
            Dim _row As Integer = gv_edit.FocusedRowHandle

            If _col = "bsda_ac_id" Or _col = "ac_name" Or _col = "bsda_ac_hirarki" Then
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
