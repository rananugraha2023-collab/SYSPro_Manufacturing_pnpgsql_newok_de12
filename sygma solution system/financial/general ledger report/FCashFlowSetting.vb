Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FCashFlowSetting

    Dim _loc_oid_mstr As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FDomain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        init_le(le_gcal, "gcal_mstr")
    End Sub

    Public Overrides Sub load_cb()
        Try

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Code", "code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remark", "remark", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sort Number", "sort_number", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Seq", "seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Header", "remark_header", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Footer", "remark_footer", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Header Sign", "cfsign_header", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Sign (Indirect)", "cf_value_sign", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Header", "sub_header", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Type", "cf_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Sign (Direct)", "ac_value_header", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Header2", "sub_header2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Group", "group", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "code", False)
        add_column(gv_edit, "ac_id", False)
        add_column(gv_edit, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account Code Hirarki", "ac_code_hirarki", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Sequence Number", "seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Sign", "ac_sign", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Value", "ac_value", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail, "code", False)
        add_column(gv_detail, "ac_id", False)
        add_column(gv_detail, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Account Code Hirarki", "ac_hirarki", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Sequence Number", "seq", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Sign", "ac_sign", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Value", "ac_value", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT   " _
            & "  a.code, " _
            & "  a.remark, " _
            & "  a.sort_number, " _
            & "  a.remark_header, " _
            & "  a.remark_footer, " _
            & "  a.cfsign_header, " _
            & "  a.cf_value_sign,b.cfdet_oid, " _
            & "  b.cfdet_pk, " _
            & "  b.seq, " _
            & "  b.sub_header,a.cf_type,b.ac_value_header,b.sub_header2,b.group " _
            & "FROM " _
            & "  public.tconfsettingcashflow a " _
            & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
            & "ORDER BY " _
            & "  a.sort_number, " _
            & "  b.seq"

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
                 & "  a.code, " _
                 & "  a.ac_id, " _
                 & "  b.ac_code,a.ac_hirarki, " _
                 & "  b.ac_name,a.ac_sign,a.ac_value, " _
                 & "  a.seq " _
                 & "FROM " _
                 & "  public.tconfsettingcashflowdet_item a " _
                 & "  INNER JOIN public.ac_mstr b ON (a.ac_id = b.ac_id) " _
                 & "ORDER BY " _
                 & "  a.seq"

            load_data_detail(sql, gc_detail, "detail")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Public Overrides Sub relation_detail()
        Try

            'gv_shu_result_detail.Columns("shurd_shur_oid").FilterInfo = _
            'New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[shurd_shur_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("shur_oid").ToString & "'")

            gv_detail.Columns("code").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[code]='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cfdet_pk").ToString & "'")
            gv_detail.BestFitColumns()

        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            Code.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                Code.Text = .Item("code")
                remark.Text = .Item("remark")
                sort_number.Text = .Item("sort_number")
                remark_header.Text = .Item("remark_header")
                remark_footer.Text = SetString(.Item("remark_footer"))
                cfsign_header.Text = SetString(.Item("cfsign_header"))
                cfmark.EditValue = .Item("cf_value_sign")
                sub_header.EditValue = .Item("sub_header")
                Code.Tag = .Item("cfdet_pk")
                ac_value_header.EditValue = .Item("ac_value_header")
                sub_header2.EditValue = .Item("sub_header2")

            End With

            Dim sql As String

            Try
                Try
                    ds.Tables("edit").Clear()
                Catch ex As Exception
                End Try


                sql = "SELECT  " _
                     & "  a.code, " _
                     & "  a.ac_id, " _
                     & "  b.ac_code,b.ac_code_hirarki, " _
                     & "  b.ac_name,a.ac_sign,a.ac_value, " _
                     & "  a.seq " _
                     & "FROM " _
                     & "  public.tconfsettingcashflowdet_item a " _
                     & "  INNER JOIN public.ac_mstr b ON (a.ac_id = b.ac_id) " _
                     & " Where a.code='" & Code.Tag & "' " _
                     & "ORDER BY " _
                     & "  a.seq"

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

            'Dim btn As DevExpress.XtraEditors.NavigatorButton = GC_Edit.EmbeddedNavigator.Buttons.EndEdit
            GC_Edit.EmbeddedNavigator.Buttons.DoClick(GC_Edit.EmbeddedNavigator.Buttons.EndEdit)
            ds.Tables("edit").AcceptChanges()

            ssql = "update tconfsettingcashflow set remark_header='" & remark_header.EditValue _
            & "', remark_footer='" & remark_footer.EditValue & "' where code='" & Code.Tag & "'"
            ssqls.Add(ssql)

            ssql = "delete from tconfsettingcashflowdet_item where code='" & Code.Tag & "'"
            ssqls.Add(ssql)

            For i As Integer = 0 To ds.Tables("edit").Rows.Count - 1
                With ds.Tables("edit").Rows(i)
                    ssql = "INSERT INTO  public.tconfsettingcashflowdet_item " _
                        & "( code, " _
                        & "  ac_id,ac_hirarki,ac_sign,ac_value, " _
                        & "  seq " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(Code.Tag) & ",  " _
                        & SetInteger(.Item("ac_id")) & ",  " _
                        & SetSetring(.Item("ac_code_hirarki")) & ",  " _
                        & SetSetring(.Item("ac_sign")) & ",  " _
                        & SetInteger(.Item("ac_value")) & ",  " _
                        & SetInteger(.Item("seq")) & ")"

                    ssqls.Add(ssql)
                End With
            Next

            If DbRunTran(ssqls) = False Then
                Exit Function
            End If

            edit = True
            after_success()
            set_row(Trim(Code.Text), "code")
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

            If _col = "ac_code" Or _col = "ac_name" Or _col = "ac_code_hirarki" Then
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

   
    Private Sub BtCheckOverlapDirect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtCheckOverlapDirect.Click
        Try
            Dim ssql As String

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
               & "  d.ac_id "


            Dim dt As New DataTable
            dt = GetTableData(sSql)


            Dim _ac_id As Integer = 0
            Dim _n As Integer = 1
            Dim _data As String = ""
            For Each dr As DataRow In dt.Rows
                If _ac_id <> dr("ac_id") Then
                    _ac_id = dr("ac_id")
                    _n = 1
                    _data = dr("sub_header") & " " & dr("ac_hirarki") & " " & dr("ac_code") & " " & dr("ac_name") & vbNewLine
                Else
                    _n += 1
                    _data += dr("sub_header") & " " & dr("ac_hirarki") & " " & dr("ac_code") & " " & dr("ac_name") & vbNewLine
                End If

                If _n > 2 Then
                    Box(_data, "Confict")
                    'Exit For
                End If

            Next

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        Try
            Dim ssql As String

            ssql = "SELECT DISTINCT " _
                & "  a.glt_ac_id, " _
                & "  b.ac_code, " _
                & "  b.ac_name " _
                & "FROM " _
                & "  public.cf_save a " _
                & "  INNER JOIN public.ac_mstr b ON (a.glt_ac_id = b.ac_id) " _
                & "WHERE a.glt_periode='" & le_gcal.EditValue.ToString & "' and a.glt_ac_id not in (SELECT  " _
                & "  d.ac_id " _
                & "FROM " _
                & "  public.tconfsettingcashflow a " _
                & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
                & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code) " _
                & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
                & "WHERE " _
                & "   d.ac_is_sumlevel='N' and cf_type='D' " _
                & ")"

            Dim dt As New DataTable
            dt = GetTableData(ssql)

            Dim _data As String = ""
            For Each dr As DataRow In dt.Rows
                _data += dr("ac_code") & " " & dr("ac_name") & vbNewLine
            Next

            If _data <> "" Then
                Box(_data, "Not not exist in setting")
            End If

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
