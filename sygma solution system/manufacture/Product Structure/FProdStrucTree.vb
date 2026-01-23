Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports DevExpress.XtraTreeList


Public Class FProdStrucTree
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FProdStrucTree_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        le_entity.Properties.DataSource = dt_bantu
        le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        le_entity.ItemIndex = 0

        xtc_master.SelectedTabPageIndex = 1
        xtp_edit.PageVisible = False
        xtp_data.PageVisible = True
        xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False
        'form_first_load()
        'input_grid()
        'InitFiltering()
    End Sub

    Public Overrides Function export_data() As Boolean
        Try
            Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls")

            If fileName <> "" Then
                TreeProdStruc.ExportToXls(fileName)
                OpenFile(fileName)
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Overrides Sub help_load_data(ByVal par As Boolean)
        input_grid()
    End Sub

    Public Sub InitFiltering()
        TreeProdStruc.FilterConditions.Add(New FilterCondition(FilterConditionEnum.NotBetween, TreeProdStruc.Columns("psd_qty"), 20, 1000, True))
    End Sub

    Private Sub apply_filter(ByVal par_visible As Boolean)
        TreeProdStruc.FilterConditions(0).Visible = Not par_visible
    End Sub

    Public Sub input_grid()

        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        'If use_date.EditValue = False Then
                        '    .SQL = "SELECT ps_seq, psd_seq, psd_comp, psd_ref, psd_desc, psd_qty, psdbomdesc, " _
                        '       & " psd_op, psdstrname, psd_start_date, psd_end_date, psd_scrp_pct, psd_lt_off, psdgroupdesc,psd_fcst_pct,psd_pt_bom_id,code_name as unit_measure " _
                        '       & " from public.get_allproduct_structure(" _
                        '       & "'" & be_first.EditValue.ToString & "'" & ", " _
                        '       & "'" & be_to.EditValue.ToString & "'" & ", " _
                        '       & te_quan.EditValue & ", " _
                        '       & te_level.EditValue & ",'N',CURRENT_DATE) " _
                        '       & "inner join pt_mstr on (psd_pt_bom_id=pt_id) " _
                        '       & " inner join code_mstr on (code_id=pt_um) "

                        'ElseIf use_date.EditValue = True Then
                        '    .SQL = "SELECT ps_seq, psd_seq, psd_comp, psd_ref, psd_desc, psd_qty, psdbomdesc, " _
                        '       & " psd_op, psdstrname, psd_start_date, psd_end_date, psd_scrp_pct, psd_lt_off, psdgroupdesc,psd_fcst_pct,psd_pt_bom_id,code_name as unit_measure " _
                        '       & " from public.get_allproduct_structure(" _
                        '       & "'" & be_first.EditValue.ToString & "'" & ", " _
                        '       & "'" & be_to.EditValue.ToString & "'" & ", " _
                        '       & te_quan.EditValue & ", " _
                        '       & te_level.EditValue & ",'Y'," & SetDate(pr_ondate.DateTime) & ") " _
                        '       & "inner join pt_mstr on (psd_pt_bom_id=pt_id) " _
                        '       & " inner join code_mstr on (code_id=pt_um) "

                        'End If

                        .SQL = "select a.*,invct_cost from public.get_ps_first(" & be_first.Tag & ",1) as a " _
                        & "  left outer join invct_table on invct_pt_id = psd_pt_bom_id "

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "ptbomtree_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
                TreeProdStruc.DataSource = ds_bantu.Tables(0).DefaultView
                'TreeProdStruc.KeyFieldName = "ps_pt_bom_id"
                'TreeProdStruc.ParentFieldName = "ptbomch"
                TreeProdStruc.KeyFieldName = "psd_id"
                TreeProdStruc.ParentFieldName = "psd_parent_id"
                'TreeProdStruc.PopulateColumns()
                TreeProdStruc.OptionsBehavior.EnableFiltering = True
                TreeProdStruc.ExpandAll()
                TreeProdStruc.BestFitColumns()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Using

    End Sub

    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_first.ButtonClick
        Dim frm As New FProdStrucSearch()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_first
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        'Dim frm As New FProdStrucSearch()
        'frm.set_win(Me)
        'frm._en_id = le_entity.EditValue
        'frm._obj = be_to
        'frm.type_form = True
        'frm.ShowDialog()
    End Sub

    Private Sub use_date_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles use_date.CheckedChanged
        If use_date.EditValue = True Then
            pr_ondate.Enabled = True
            pr_ondate.DateTime = CekTanggal()
        ElseIf use_date.EditValue = False Then
            pr_ondate.Enabled = False
        End If
    End Sub


    Private Sub CheckEdit1_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'apply_filter(CheckEdit1.EditValue)
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
