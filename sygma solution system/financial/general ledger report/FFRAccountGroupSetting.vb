Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FFRAccountGroupSetting
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim sSQL As String
    Public dt_edit As New DataTable
    Dim _oid_mstr As String

    Private Sub FFinancialReportSetting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Description", "xfs1_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail, "xfs1d_xfs1_oid", False)
        add_column(gv_detail, "xfs1d_ac_id", False)
        add_column_copy(gv_detail, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sequence Number", "xfs1d_seq", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "xfs1d_xfs1_oid", False)
        add_column(gv_edit, "xfs1d_ac_id", False)
        add_column(gv_edit, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Sequence Number", "xfs1d_seq", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
            & "  xfs1_oid, " _
            & "  xfs1_desc, " _
            & "  xfs1_type, " _
            & "  xfs1_add_by,xfs1_add_date,xfs1_upd_by,xfs1_upd_date " _
            & " FROM " _
            & "  public.xfs1_mstr " _
            & " WHERE xfs1_type = 'FS' " _
            & " ORDER BY " _
            & "  xfs1_desc "
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        xfs1_desc.Text = ""
        Try
            sSQL = "SELECT  " _
                  & "  xfs1d_oid,xfs1d_xfs1_oid, " _
                  & "  xfs1d_ac_id, " _
                  & "  xfs1d_seq, " _
                  & "  ac_code, " _
                  & "  ac_name " _
                  & " FROM " _
                  & " public.xfs1d_det " _
                  & " INNER JOIN public.ac_mstr ON ac_id = xfs1d_ac_id " _
                  & " where xfs1d_ac_id = -33234 " _
                  & " order by xfs1d_seq asc "
            dt_edit = GetTableData(sSQL)
            gc_edit.DataSource = dt_edit
            gv_edit.BestFitColumns()
            gv_edit.OptionsView.ColumnAutoWidth = False

        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub
    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function insert() As Boolean
        insert = True
        Dim sSQLs As New ArrayList
        Try
            gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
            dt_edit.AcceptChanges()

            'xrst_code.Text = "PL" & Format(GetNewNumber("tconfsetting_plproject", "code", 3), "000")
            Dim _xfs1_oid As Guid = Guid.NewGuid()

            sSQL = "INSERT INTO  " _
               & "  public.xfs1_mstr " _
               & "( " _
               & "  xfs1_oid," _
               & "  xfs1_dom_id," _
               & "  xfs1_add_by," _
               & "  xfs1_add_date," _
               & "  xfs1_dt," _
               & "  xfs1_desc, " _
               & "  xfs1_type " _
               & ")  " _
               & "VALUES ( " _
               & SetSetring(_xfs1_oid.ToString()) & ",  " _
               & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
               & SetSetring(master_new.ClsVar.sNama) & ",  " _
               & "current_timestamp" & ",  " _
               & "current_timestamp" & ",  " _
               & SetSetring(xfs1_desc.Text) & ",  " _
               & SetSetring("FS") & "  " _
               & ")"
            sSQLs.Add(sSQL)

            For i As Integer = 0 To dt_edit.Rows.Count - 1
                With dt_edit.Rows(i)
                    sSQL = "INSERT INTO public.xfs1d_det " _
                        & "(xfs1d_oid, " _
                        & " xfs1d_xfs1_oid, " _
                        & " xfs1d_add_by, " _
                        & " xfs1d_add_date, " _
                        & " xfs1d_dt, " _
                        & " xfs1d_ac_id, " _
                        & " xfs1d_seq " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid().ToString()) & ", " _
                        & SetSetring(_xfs1_oid.ToString()) & ", " _
                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                        & "current_timestamp" & ",  " _
                        & "current_timestamp" & ",  " _
                        & SetInteger(dt_edit.Rows(i).Item("xfs1d_ac_id")) & ",  " _
                        & SetInteger(dt_edit.Rows(i).Item("xfs1d_seq")) & ")  "
                    sSQLs.Add(sSQL)
                End With
            Next
            DbRunTran(sSQLs)
            after_success()
            set_row(Trim(xfs1_desc.Text), "xfs1_desc")
            DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible

        Catch ex As Exception
            Pesan(Err)
            insert = False
        End Try

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
                  & "  xfs1d_oid,xfs1d_xfs1_oid, " _
                  & "  xfs1d_ac_id, " _
                  & "  xfs1d_seq, " _
                  & "  ac_code, " _
                  & "  ac_name " _
                  & " FROM " _
                  & " public.xfs1d_det " _
                  & " INNER JOIN public.ac_mstr ON ac_id = xfs1d_ac_id " _
                  & " ORDER BY " _
                  & " xfs1d_seq asc "
            load_data_detail(sql, gc_detail, "detail")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("xfs1d_xfs1_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[xfs1d_xfs1_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("xfs1_oid").ToString & "'")
            gv_detail.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            xfs1_desc.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _oid_mstr = .Item("xfs1_oid").ToString
                xfs1_desc.Text = SetString(.Item("xfs1_desc"))
            End With

            'Dim sql As String

            Try
                'Try
                '    ds.Tables("edit").Clear()
                'Catch ex As Exception
                'End Try

                sSQL = "SELECT  " _
                  & "  xfs1d_oid,xfs1d_xfs1_oid," _
                  & "  xfs1d_ac_id, " _
                  & "  xfs1d_seq, " _
                  & "  ac_code, " _
                  & "  ac_name " _
                  & " FROM " _
                  & " public.xfs1d_det a " _
                  & " INNER JOIN public.ac_mstr ON ac_id = xfs1d_ac_id " _
                  & " where xfs1d_xfs1_oid = " & SetSetring(_oid_mstr.ToString()) _
                  & " order by xfs1d_seq asc "
                dt_edit = GetTableData(sSQL)
                gc_edit.DataSource = dt_edit
                gv_edit.BestFitColumns()
                gv_edit.OptionsView.ColumnAutoWidth = False

            Catch ex As Exception
                Pesan(Err)
            End Try

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim ssqls As New ArrayList
        Try

            gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
            dt_edit.AcceptChanges()

            sSQL = "update xfs1_mstr set " _
                    & " xfs1_desc ='" & xfs1_desc.Text & "', " _
                    & " xfs1_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ", " _
                    & " xfs1_upd_date = current_timestamp, " _
                    & " xfs1_dt = current_timestamp " _
                    & " where xfs1_oid ='" & _oid_mstr.ToString() & "'"
            ssqls.Add(ssql)

            sSQL = "delete from xfs1d_det where xfs1d_xfs1_oid ='" & _oid_mstr.ToString() & "'"

            ssqls.Add(ssql)
            For i As Integer = 0 To dt_edit.Rows.Count - 1
                With dt_edit.Rows(i)
                    sSQL = "INSERT INTO public.xfs1d_det " _
                        & "(xfs1d_oid, " _
                        & " xfs1d_xfs1_oid, " _
                        & " xfs1d_add_by, " _
                        & " xfs1d_add_date, " _
                        & " xfs1d_dt, " _
                        & " xfs1d_ac_id, " _
                        & " xfs1d_seq " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid().ToString()) & ", " _
                        & SetSetring(_oid_mstr.ToString()) & ", " _
                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                        & "current_timestamp" & ",  " _
                        & "current_timestamp" & ",  " _
                        & SetInteger(dt_edit.Rows(i).Item("xfs1d_ac_id")) & ",  " _
                        & SetInteger(dt_edit.Rows(i).Item("xfs1d_seq")) & "  )"
                    ssqls.Add(sSQL)
                End With
            Next
            DbRunTran(ssqls)

            edit = True
            after_success()
            set_row(Trim(xfs1_desc.Text), "xfs1_desc")
            DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
        Catch ex As Exception
            Pesan(Err)
            edit = False
        End Try
        Return edit
    End Function

    Public Overrides Function delete_data() As Boolean
        delete_data = True
        If ds.Tables.Count = 0 Then
            delete_data = False
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            delete_data = False
            Exit Function
        End If

        If MessageBox.Show("Are you sure to delete this data..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

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
                            .Command.CommandText = "delete from xfs1_mstr where xfs1_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("xfs1_oid").ToString() + "'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            sqlTran.Commit()

                            help_load_data(True)
                            MessageBox.Show("Delete data successfull..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        Try
            Dim _col As String = gv_edit.FocusedColumn.Name
            Dim _row As Integer = gv_edit.FocusedRowHandle

            If _col = "ac_code" Or _col = "ac_name" Then
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
