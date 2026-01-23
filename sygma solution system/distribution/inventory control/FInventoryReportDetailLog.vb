Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FInventoryReportDetailLog
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public _sql As String
    Public _par_item As String

    Private Sub FInventoryReportDetailLog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()

        AddHandler gv_loc.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv_loc.ColumnFilterChanged, AddressOf relation_detail
        CeNo0.EditValue = True
    End Sub

    Public Overrides Sub load_cb()
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'pr_entity.Properties.DataSource = dt_bantu
        'pr_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'pr_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'pr_entity.ItemIndex = 0

        init_le(pr_entity, "en_mstr")
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_loc, "invc_oid", False)
        add_column_edit(gv_loc, "Select", "pilih", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, "")
        add_column_copy(gv_loc, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Deskripsi1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Deskripsi2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Product Line ", "pl_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Location ID", "invc_loc_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Location Code", "loc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Monitored Location", "loc_mon", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Lot Number", "invc_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Qty On Hand", "invc_qty_sum", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_loc, "Qty On Old", "invc_qty_old", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_loc, "Unit Measure", "um_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_loc, "Cost", "invct_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_loc, "Ext Cost", "invct_cost_ext", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        add_column_copy(gv_serial, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_serial, "Deskripsi1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_serial, "Deskripsi2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_serial, "Product Line ", "pl_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_serial, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_serial, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_serial, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_serial, "Lot / Serial Number", "invc_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_serial, "Qty On Hand", "invc_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        'add_column_copy(gv_serial, "Cost", "invct_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_serial, "Ext Cost", "invct_cost_ext", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        add_column(gv_detail, "invc_oid", False)
        add_column_copy(gv_detail, "Sales Order Code", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty Allocated", "qty_alloc_remain", DevExpress.Utils.HorzAlignment.Default)



    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor

        Dim _en_id_all As String

        If  CBChild.EditValue = True Then
            _en_id_all = get_en_id_child(pr_entity.EditValue)
        Else

            _en_id_all = pr_entity.EditValue

        End If

        If arg <> False Then
            '================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        If _sql <> "" Then
                            .SQL = _sql
                        Else
                            .SQL = "SELECT  false as pilih, " _
                               & "  invc_oid, " _
                               & "  invc_dom_id, " _
                               & "  invc_en_id, " _
                               & "  invc_si_id, " _
                               & "  invc_loc_id, " _
                               & "  invc_pt_id, " _
                               & "  invc_serial, " _
                               & "  invc_qty as invc_qty_sum,invc_qty_old as invc_qty_old, " _
                               & "  en_desc, " _
                               & "  si_desc, loc_code, " _
                               & "  loc_desc, " _
                               & "  loc_mon, " _
                               & "  pt_code, " _
                               & "  pt_desc1, " _
                               & "  pt_desc2, " _
                               & "  pl_desc, " _
                               & "  pt_cost,um_mstr.code_name as um_name " _
                               & "FROM  " _
                               & "  invc_mstr " _
                               & "  inner join en_mstr on en_id = invc_en_id " _
                               & "  inner join si_mstr on si_id = invc_si_id " _
                               & "  inner join loc_mstr on loc_id = invc_loc_id " _
                               & "  inner join pt_mstr on pt_id = invc_pt_id " _
                               & "  inner join pl_mstr on pt_pl_id = pl_id " _
                               & "  left outer join code_mstr um_mstr on pt_um = um_mstr.code_id  " _
                               & "  where invc_en_id in (" + _en_id_all & ") "
                        
                        End If

                        If BtEItem.Text <> "" Then
                            .SQL = .SQL & " and pt_id in (" & _par_item & ") "
                        End If

                       

                        .SQL = .SQL & "  order by loc_desc,pt_desc1 " 


                        If CEGroupByWh.EditValue = True Then

                            .SQL = "SELECT false as pilih,  " _
                             & "  invc_pt_id, pt_code, " _
                             & "  pt_desc1, " _
                             & "  pt_desc2, " _
                             & "  pl_desc, " _
                             & "  invc_en_id,en_desc," _
                             & "  invc_si_id,si_desc, invc_serial, " _
                             & "  um_mstr.code_name as um_name , " _
                             & "   " _
                             & "   " _
                             & "  sum(invc_qty) as invc_qty_sum, " _
                             & "   " _
                             & "   wh_code as loc_code, " _
                             & "  wh_desc as loc_desc " _
                             & "  " _
                             & "FROM  " _
                             & "  invc_mstr " _
                             & "  inner join en_mstr on en_id = invc_en_id " _
                             & "  inner join si_mstr on si_id = invc_si_id " _
                             & "  inner join loc_mstr on loc_id = invc_loc_id " _
                             & "  inner join pt_mstr on pt_id = invc_pt_id " _
                             & "  inner join pl_mstr on pt_pl_id = pl_id " _
                             & "  inner join wh_mstr on loc_wh_id = wh_id " _
                             & "  left outer join code_mstr um_mstr on pt_um = um_mstr.code_id  " _
                             & "  where invc_en_id in (" + _en_id_all & ") "


                            If BtEItem.Text <> "" Then
                                .SQL = .SQL & " and pt_id in (" & _par_item & ") "
                            End If

                            .SQL = .SQL & "  group by invc_pt_id, pt_code, " _
                            & " pt_desc1,pt_desc2,pl_desc, invc_en_id,en_desc,invc_si_id,si_desc, invc_serial, " _
                            & " um_mstr.code_name,wh_code,wh_desc "

                        End If

                        .InitializeCommand()
                        .FillDataSet(ds, "inv_location")
                        gc_loc.DataSource = ds.Tables("inv_location")



                        'If _sql <> "" Then
                        '    .SQL = _sql
                        'Else
                        '    .SQL = "SELECT  " _
                        '   & "  invc_oid, " _
                        '   & "  invc_dom_id, " _
                        '   & "  invc_en_id, " _
                        '   & "  invc_si_id, " _
                        '   & "  invc_loc_id, " _
                        '   & "  invc_pt_id, " _
                        '   & "  invc_serial, " _
                        '   & "  sum(invc_qty) as invc_qty_sum,sum(invc_qty_old) as invc_qty_old, " _
                        '   & "  en_desc, " _
                        '   & "  si_desc, loc_code, " _
                        '   & "  loc_desc, " _
                        '   & "  pt_code, " _
                        '   & "  pt_desc1, " _
                        '   & "  pt_desc2, " _
                        '   & "  pl_desc, " _
                        '   & "  pt_cost,um_mstr.code_name as um_name " _
                        '   & "FROM  " _
                        '   & "  invc_mstr " _
                        '   & "  inner join en_mstr on en_id = invc_en_id " _
                        '   & "  inner join si_mstr on si_id = invc_si_id " _
                        '   & "  inner join loc_mstr on loc_id = invc_loc_id " _
                        '   & "  inner join pt_mstr on pt_id = invc_pt_id " _
                        '   & "  inner join code_mstr um_mstr on pt_um = um_mstr.code_id  " _
                        '   & "  inner join pl_mstr on pt_pl_id = pl_id " _
                        '   & "  where invc_en_id in (" + _en_id_all & ") "

                        'End If

                        'If BtEItem.Text <> "" Then
                        '    .SQL = .SQL & " and pt_id in (" & _par_item & ") "
                        'End If

                        '.SQL = .SQL & "  group by invc_oid, " _
                        '   & "  invc_dom_id, " _
                        '   & "  invc_en_id, " _
                        '   & "  invc_si_id, " _
                        '   & "  invc_loc_id, " _
                        '   & "  invc_pt_id, " _
                        '   & "  invc_serial, " _
                        '   & "  en_desc, " _
                        '   & "  si_desc, loc_code," _
                        '   & "  loc_desc, " _
                        '   & "  pt_code, " _
                        '   & "  pt_desc1, " _
                        '   & "  pt_desc2, " _
                        '   & "  pl_desc, " _
                        '   & "  pt_cost, um_mstr.code_name "

                        '.InitializeCommand()
                        '.FillDataSet(ds, "inv_location")
                        'gc_loc.DataSource = ds.Tables("inv_location")



                        bestfit_column()
                        ConditionsAdjustment()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("invc_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("invc_oid='" & ds.Tables("inv_location").Rows(BindingContext(ds.Tables("inv_location")).Position).Item("invc_oid").ToString & "'")
            gv_detail.BestFitColumns()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub MergeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MergeToolStripMenuItem.Click
        Dim ssql As String
        Dim ssqls As New ArrayList
        Dim oid_awal As String = ""

        Try

            ssql = "select * from invc_mstr where invc_pt_id=" & _
                ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("invc_pt_id") _
                & " and invc_en_id=" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("invc_en_id") _
                & " and invc_loc_id=" _
                & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("invc_loc_id") _
                & " order by invc_qty desc"

            Dim dt_invc As New DataTable

            dt_invc = GetTableData(ssql)

            Dim i As Integer = 0
            If dt_invc.Rows.Count > 1 Then
                For Each dr As DataRow In dt_invc.Rows
                    If i = 0 Then
                        oid_awal = dr("invc_oid").ToString
                    Else

                        ssql = "delete from invc_mstr where invc_oid='" & dr("invc_oid").ToString & "'"
                        ssqls.Add(ssql)

                        ssql = "update invc_mstr set invc_qty=invc_qty+" & SetDec(dr("invc_qty")) & " where invc_oid='" & oid_awal & "'"
                        ssqls.Add(ssql)

                        ssql = insert_log("Merge Stok " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_code") & " " & SetDec(dr("invc_qty")))
                        ssqls.Add(ssql)
                    End If

                    i += 1
                Next
            End If

           

            If master_new.PGSqlConn.status_sync = True Then
                'DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "")
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    'Return False
                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    'Return False
                    Exit Sub
                End If
                ssqls.Clear()
            End If
            Box("Proses Merge sucess")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        Dim ssql As String
        Dim ssqls As New ArrayList
        Try
            If ask("Are you sure to delete this data?", "Confirm ...") = False Then
                Exit Sub
            End If
            If InputBox("Pass", "Pass") <> "sygmasyspro" Then
                Exit Sub
            End If

            ssql = "delete from invc_mstr where invc_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("invc_oid").ToString & "'"
            ssqls.Add(ssql)

            ssql = insert_log("Delete Stok " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_code"))
            ssqls.Add(ssql)

            If master_new.PGSqlConn.status_sync = True Then
                DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "")
            Else
                DbRunTran(ssqls, "")
            End If
            Box("Proses Delete sucess")
            'Box("This menu is not active")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub BtEItem_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BtEItem.ButtonClick
        Try

            Dim frm As New FPartNumberSearchCheck
            frm.set_win(Me)
            frm._obj = BtEItem
            frm._en_id = pr_entity.EditValue
            frm.type_form = True
            frm.ShowDialog()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub BtCek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtCek.Click
        Try
            Dim _id_loc, _pt_code As String
            _id_loc = ""
            _pt_code = ""
            Dim _id_loc_awal, _pt_code_awal As String

            Dim _temp As String = ""
            Dim x As Integer = 0
            With ds.Tables("inv_location") 'posted all
                For i As Integer = 0 To ds.Tables("inv_location").Rows.Count - 1
                    _id_loc_awal = .Rows(i).Item("invc_loc_id").ToString
                    _pt_code_awal = .Rows(i).Item("pt_code").ToString
                    If _id_loc = _id_loc_awal And _pt_code = _pt_code_awal Then
                        _temp += .Rows(i).Item("pt_code") & " - " & .Rows(i).Item("loc_desc") & vbNewLine

                    End If
                    _id_loc = .Rows(i).Item("invc_loc_id").ToString
                    _pt_code = .Rows(i).Item("pt_code")


                    'If _nomor <> .Rows(i).Item("glt_code").ToString Then
                    '    If System.Math.Round(_debet, 2) <> System.Math.Round(_credit, 2) Then
                    '        'Box("GL Number " & _nomor & " is not balanced")
                    '        _temp += _nomor & vbNewLine
                    '        'Clipboard.SetText(_nomor)
                    '        'Exit Try
                    '    End If
                    '    _nomor = .Rows(i).Item("glt_code").ToString
                    '    _debet = .Rows(i).Item("glt_debit")
                    '    _credit = .Rows(i).Item("glt_credit")
                    'Else
                    '    _debet = _debet + .Rows(i).Item("glt_debit")
                    '    _credit = _credit + .Rows(i).Item("glt_credit")
                    'End If

                Next

                If _temp <> "" Then
                    Box("Partnumber Number " & _temp & " is duplicate")
                    Clipboard.SetText(_temp)
                Else
                    Box("All Normal")
                End If

            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LabelControl1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LabelControl1.DoubleClick
        Try
            Dim ssqls As New ArrayList
            Dim ssql As String
            If InputBox("Pass") = "sygma" Then
                ssql = "DELETE FROM  " _
                    & "  public.invc_mstr  " _
                    & "WHERE  " _
                    & "invc_en_id=" & pr_entity.EditValue
                ssqls.Add(ssql)

                Dim n As Integer = 1
                For Each dr As DataRow In ds.Tables("inv_location").Rows
                    ssql = "INSERT INTO  " _
                        & "  public.invc_mstr " _
                        & "( " _
                        & "  invc_oid, " _
                        & "  invc_dom_id, " _
                        & "  invc_en_id, " _
                        & "  invc_si_id, " _
                        & "  invc_loc_id, " _
                        & "  invc_pt_id, " _
                        & "  invc_qty, " _
                        & "  invc_serial, " _
                        & "  pt_tax_class, " _
                        & "  invc_qty_alloc, " _
                        & "  invc_qty_old " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(dr("invc_oid")) & ",  " _
                        & SetInteger(dr("invc_dom_id")) & ",  " _
                        & SetInteger(dr("invc_en_id")) & ",  " _
                        & SetInteger(dr("invc_si_id")) & ",  " _
                        & SetInteger(dr("invc_loc_id")) & ",  " _
                        & SetInteger(dr("invc_pt_id")) & ",  " _
                        & SetDec(dr("invc_qty_sum")) & ",  " _
                        & SetSetring(dr("invc_serial")) & ",  " _
                        & SetSetring("") & ",  " _
                        & SetDec("") & ",  " _
                        & SetDec(dr("invc_qty_old")) & "  " _
                        & ")"
                    ssqls.Add(ssql)
                    n = n + 1
                    LabelControl2.Text = n & " of " & ds.Tables(0).Rows.Count
                    System.Windows.Forms.Application.DoEvents()
                Next

                If DbRunTran(master_new.PGSqlConn.FinsertSQL2Array(ssqls)) = False Then
                    Box("Error execute")
                End If
                Box("Success")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CeNo0_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CeNo0.CheckedChanged
        Try
            If CeNo0.EditValue = True Then
                gv_loc.ActiveFilterString = "[invc_qty_sum]<>0"
            Else
                gv_loc.ActiveFilterString = ""
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LabelControl1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LabelControl1.Click

    End Sub

    Private Sub LabelControl2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LabelControl2.Click

    End Sub

    Private Sub LabelControl3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub LabelControl2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LabelControl2.DoubleClick
       
        

    End Sub

    Public Overrides Sub preview()
        Try
            Dim ssql As String
            

            Dim _data As String = ""
            For Each dr_master As DataRow In ds.Tables(0).Rows
                If dr_master("pilih") = True Then
                    'ssql = "update pb_mstr set pb_status_packing='D' where pb_oid='" & dr_master("pb_oid") & "'"

                    'sSQLs.Add(ssql)
                    _data = _data & "'" & dr_master("pt_code") & "',"
                End If
            Next

            If _data.Length > 0 Then
                _data = _data.Substring(0, _data.Length - 1)
            Else
                MsgBox("Please select data")
                Exit Sub

            End If



            ssql = "SELECT  pt_code, pt_desc1 from pt_mstr where pt_code in (" & _data & ") " _
                & "ORDER BY " _
                & "  pt_desc1"

            Dim rpt As New rptLabelPartnumber
            With rpt
                Dim ds As New DataSet
                ds = ReportDataset(ssql)
                If ds.Tables(0).Rows.Count < 1 Then
                    Box("Maaf data kosong")
                    Exit Sub
                End If


                .DataSource = ds
                .DataMember = "Table"

                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

                printTool.PreviewForm.Text = "Partnumber"
                '.PrintingSystem = ps
                printTool.ShowPreview()
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class



