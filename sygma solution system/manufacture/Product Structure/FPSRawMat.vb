Imports npgsql
Imports master_new.ModFunction

Public Class FPSRawMat
    Dim dt_bantu As DataTable
    Dim ds_bantu As New DataSet
    Dim func_data As New function_data
    Dim _en_id As Integer

    Private Sub FPSRawMat_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim ds_bantu As New DataSet

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from pt_raw_temp where userid_temp = " + master_new.ClsVar.sUserID.ToString
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()

                        'help_load_data(True)

                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_bantu = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select en_id, en_code, en_desc from en_mstr where en_active ~~* 'Y'" + _
                               " and en_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                               " and en_id in (select user_en_id from tconfuserentity " + _
                               " where userid = " + master_new.ClsVar.sUserID.ToString + ") " + _
                               " order by en_code "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "en_mstr")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        _en_id = ds_bantu.Tables("en_mstr").Rows(0).Item("en_id")

        form_first_load()

    End Sub

    Public Overrides Sub form_first_load()
        'create_table()
        'help_load_data(False)
        load_cb()
        on_load()
        format_grid()
        add_handler_numeric()
        'add_groupsummary()
        'AllowIncrementalSearch()
        set_component()
        'load_Columns()

        spv_master = scc_master.PanelVisibility
        xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False
        xtp_edit.PageVisible = False
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "P.Strc. Code", "ps_par_temp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "ps_desc_temp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part/BOM Code", "pt_bom_code_temp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part/BOM Desc.", "pt_bom_desc_temp", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "pt_bom_id_desc", False)
        add_column_copy(gv_master, "Quantity", "ps_quan_temp", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    'Public Overrides Function get_sequel() As String
    '    get_sequel = "SELECT   " _
    '                & "  userid_temp, " _
    '                & "  ps_par_temp, " _
    '                & "  ps_desc_temp, " _
    '                & "  ps_use_bom_temp, " _
    '                & "  ps_pt_bom_id_desc_temp, " _
    '                & "  pt_code_temp, " _
    '                & "  pt_bom_desc_temp, " _
    '                & "  ps_quan_temp" _
    '                & " FROM  " _
    '                & "   public.pt_raw_temp  "
    '    Return get_sequel
    'End Function

    Public Overrides Sub load_data_grid_detail()

        Dim sql As String

        Try
            ds.Tables("raw_mat").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  userid_temp, " _
            & "  ps_par_temp, " _
            & "  ps_desc_temp, " _
            & "  ps_use_bom_temp, " _
            & "  ps_pt_bom_id_desc_temp, " _
            & "  pt_bom_desc_temp, " _
            & "  ps_quan_temp, " _
            & "  pt_bom_code_temp " _
            & "FROM  " _
            & "  public.pt_raw_temp " _
            & "where userid_temp = " + master_new.ClsVar.sUserID.ToString


        load_data_detail(sql, gc_master, "raw_mat")

    End Sub

    Private Sub sb_add_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_add_save.Click
        Dim i As Integer

        If MessageBox.Show("Add ..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        If be_from.EditValue > be_to.EditValue Then
            Box("Part Number destination lower than source ")
            Exit Sub
        End If

        ds_bantu = New DataSet()

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT   " _
                        & "   en_desc,  " _
                        & "   ps_oid,  " _
                        & "   ps_dom_id,  " _
                        & "   ps_en_id,  " _
                        & "   ps_add_by,  " _
                        & "   ps_add_date,  " _
                        & "   ps_upd_by,  " _
                        & "   ps_upd_date,  " _
                        & "   ps_par,  " _
                        & "   ps_id,  " _
                        & "   ps_desc,  " _
                        & "   ps_use_bom, " _
                        & "   ps_pt_bom_id,  " _
                        & "   ps_active, " _
                        & "   CASE WHEN ps_use_bom = 'Y' " _
                        & "   THEN (SELECT bom_mstr.bom_code from bom_mstr where bom_id=ps_pt_bom_id) " _
                        & "   ELSE (SELECT pt_mstr.pt_code from pt_mstr where pt_id=ps_pt_bom_id) " _
                        & "   END AS ptbomcode, " _
                        & "   CASE WHEN ps_use_bom = 'Y' " _
                        & "   THEN (SELECT bom_mstr.bom_desc from bom_mstr where bom_id=ps_pt_bom_id) " _
                        & "   ELSE (SELECT pt_mstr.pt_desc1 from pt_mstr where pt_id=ps_pt_bom_id) " _
                        & "   END AS ptbomdesc " _
                        & " FROM  " _
                        & "   public.ps_mstr  " _
                        & "  INNER JOIN en_mstr on (ps_mstr.ps_en_id = en_mstr.en_id) " _
                        & " WHERE ps_par between " & SetSetring(be_from.EditValue) & " AND " & SetSetring(be_to.EditValue) & ""
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "psptbom_mstr")

                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Try
            For i = 0 To (ds_bantu.Tables("psptbom_mstr").Rows.Count - 1)
                Try
                    Using objcb As New master_new.WDABasepgsql("", "")
                        With objcb
                            .SQL = "INSERT INTO  " _
                                    & "  public.pt_raw_temp " _
                                    & "( " _
                                    & "  userid_temp, " _
                                    & "  ps_par_temp, " _
                                    & "  ps_desc_temp, " _
                                    & "  ps_use_bom_temp, " _
                                    & "  ps_pt_bom_id_desc_temp, " _
                                    & "  pt_bom_desc_temp, " _
                                    & "  ps_quan_temp, " _
                                    & "  pt_bom_code_temp " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetInteger(master_new.ClsVar.sUserID) & ",  " _
                                    & SetSetring(ds_bantu.Tables(0).Rows(i).Item("ps_par")) & ",  " _
                                    & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("ps_desc")) & ",  " _
                                    & SetSetring(ds_bantu.Tables(0).Rows(i).Item("ps_use_bom")) & ",  " _
                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("ps_pt_bom_id")) & ",  " _
                                    & SetSetring(ds_bantu.Tables(0).Rows(i).Item("ptbomdesc")) & ",  " _
                                    & SetInteger(te_quan.EditValue) & ",  " _
                                    & SetSetring(ds_bantu.Tables(0).Rows(i).Item("ptbomcode")) & "  " _
                                    & ");"


                            .InitializeCommand()
                            .ExecuteStoredProcedure()
                            load_data_grid_detail()
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            Next
            MessageBox.Show("Congratulations " + master_new.ClsVar.sNama + ", Data Has Been Loaded..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            load_data_grid_detail()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub sb_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_delete.Click
        If MessageBox.Show("Delete This Account From This Department..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "delete from pt_raw_temp where ps_pt_bom_id_desc_temp =  " + SetSetring(ds.Tables("raw_mat").Rows(BindingContext(ds.Tables("raw_mat")).Position).Item("ps_pt_bom_id_desc_temp").ToString) _
                        & " and userid_temp = " + master_new.ClsVar.sUserID.ToString

                    .InitializeCommand()
                    .ExecuteStoredProcedure()
                    load_data_grid_detail()
                    MessageBox.Show("Congratulation " + master_new.ClsVar.sNama + ", Data Has Been Unloaded..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        load_data_grid_detail()
    End Sub


    Private Sub be_from_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_from.ButtonClick
        Dim frm As New FPTBOMSrch
        frm.set_win(Me)
        frm._en_id = "select user_en_id from tconfuserentity where userid = " + master_new.ClsVar.sUserID.ToString + " "
        frm._pil = 1
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_to.ButtonClick
        Dim frm As New FPTBOMSrch
        frm.set_win(Me)
        frm._en_id = "select user_en_id from tconfuserentity where userid = " + master_new.ClsVar.sUserID.ToString + " "
        frm._pil = 2
        frm.type_form = True
        frm.ShowDialog()
    End Sub


    Private Sub sb_generate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        input_pivot()


    End Sub

    Public Overrides Sub help_load_data(ByVal par As Boolean)
        ' MyBase.help_load_data(par)
        input_pivot()
    End Sub


    Public Sub input_pivot()
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        '===> perbaiki user id-nya
                        .SQL = "SELECT ps_par,psd_comp,pt_desc1, pt_desc2, psd_qty, psd_cost, tot_cost from public.getallproductstructureraw2(1) "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "ptpivot")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
                pgc_master.DataSource = ds_bantu.Tables(0)
                'TreeProdStruc.KeyFieldName = "ps_pt_bom_id"
                'TreeProdStruc.ParentFieldName = "ptbomch"
                'TreeProdStruc.KeyFieldName = "psd_seq"
                'TreeProdStruc.ParentFieldName = "ps_seq"
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Using
    End Sub

    Public Overrides Function export_data() As Boolean
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls")
        If fileName <> "" Then
            pgc_master.ExportToXls(fileName)
            OpenFile(fileName)
        End If
    End Function
End Class
