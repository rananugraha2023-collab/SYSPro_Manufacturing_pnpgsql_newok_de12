Imports master_new.ModFunction

Public Class FPTBOMSrch
    Public _row, _pil As Integer
    Public _en_id As String
    Public func_data As New function_data

    Private Sub FBOMSrch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 600
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "codeptbom", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "descptbom", DevExpress.Utils.HorzAlignment.Default)
        If fobject.name = FInventoryAssembly.Name Or fobject.name = FInventoryDisAssembly.Name Then
            add_column(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Cost", "invct_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        End If
    End Sub

    Public Overrides Function get_sequel() As String
        Dim _en_id_coll As String = func_data.entity_parent(_en_id)

        If fobject.name = "FPSRawMat" Then
            get_sequel = "SELECT en_mstr.en_desc, pt_oid as oidptbom, pt_en_id as enptbom, pt_id as idptbom,  " _
                       & " pt_code as codeptbom, pt_desc1 as descptbom, CASE WHEN pt_desc2 = '.,.,' " _
                       & "THEN 'N' " _
                       & "ELSE 'N' " _
                       & "END AS ptsign " _
                       & "FROM " _
                       & "  public.pt_mstr  " _
                       & "  INNER JOIN en_mstr on pt_en_id=en_id where pt_pm_code='M' and pt_en_id in (" & _en_id & ")" _
                      
        ElseIf fobject.name = FProductStructure.Name Or fobject.name = FProductStructureAssembly.Name Then
            If _pil = 3 Then
                get_sequel = "SELECT false as pilih, en_mstr.en_desc, pt_oid as oidptbom, pt_en_id as enptbom, pt_id as idptbom,  " _
                        & "       pt_code as codeptbom, pt_desc1 as descptbom, code_name as um_desc " _
                        & "FROM " _
                        & "  public.pt_mstr  " _
                        & " inner join code_mstr on pt_um=code_id " _
                        & "  INNER JOIN en_mstr on pt_en_id=en_id where pt_type='I' and pt_en_id in (0," & _en_id & ") "
            Else
                get_sequel = "SELECT en_mstr.en_desc, pt_oid as oidptbom, pt_en_id as enptbom, pt_id as idptbom,  " _
                       & " pt_code as codeptbom, pt_desc1 as descptbom " _
                       & "FROM " _
                       & "  public.pt_mstr  " _
                       & "  INNER JOIN en_mstr on pt_en_id=en_id where pt_pm_code='M' and pt_en_id in (" & _en_id & ")"

            End If
        ElseIf fobject.name = FWhereInUsedReport.Name Or fobject.name = FSimulatedPickList.Name Then
            get_sequel = "SELECT en_mstr.en_desc, pt_oid as oidptbom, pt_en_id as enptbom, pt_id as idptbom,  " _
                       & "       pt_code as codeptbom, pt_desc1 as descptbom, CASE WHEN pt_desc2 = '.,.,' " _
                       & "THEN 'N' " _
                       & "ELSE 'N' " _
                       & "END AS ptsign " _
                       & "FROM " _
                       & "  public.pt_mstr  " _
                       & "  INNER JOIN en_mstr on pt_en_id=en_id where pt_type='I' and pt_en_id in (" & _en_id & ")"

        ElseIf fobject.name = FInventoryAssembly.Name Then
            get_sequel = "SELECT en_mstr.en_desc,ps_oid, pt_oid as oidptbom, pt_en_id as enptbom, pt_id as idptbom,     " _
                    & "pt_code as codeptbom, pt_desc1 as descptbom,pt_si_id,  si_desc,pt_pl_id,   invct_cost    " _
                    & "from public.ps_mstr     " _
                    & "INNER JOIN pt_mstr on pt_id=ps_pt_bom_id      " _
                    & "INNER JOIN si_mstr on si_id=pt_si_id    " _
                    & "inner join invct_table on invct_pt_id = pt_id and pt_si_id = invct_si_id    " _
                    & "INNER JOIN en_mstr on pt_en_id=en_id where pt_type='I' and ps_is_assembly='Y' and pt_en_id in (" & _en_id_coll & ")  "

        ElseIf fobject.name = FInventoryDisAssembly.Name Then
            get_sequel = "SELECT en_mstr.en_desc,ps_oid, pt_oid as oidptbom, pt_en_id as enptbom, pt_id as idptbom,  " _
                       & "  pt_code as codeptbom, pt_desc1 as descptbom,invc_si_id,invc_loc_id, " _
                       & "  si_desc,loc_desc,pt_pl_id, " _
                       & "  invct_cost " _
                       & "  from public.ps_mstr  " _
                       & "  INNER JOIN pt_mstr on pt_id=ps_pt_bom_id " _
                       & "  INNER JOIN invc_mstr on invc_pt_id=ps_pt_bom_id " _
                       & "  INNER JOIN si_mstr on si_id=invc_si_id " _
                       & "  INNER JOIN loc_mstr on loc_id=invc_loc_id " _
                       & "  inner join invct_table on invct_pt_id = invc_pt_id and invct_si_id = invc_si_id " _
                       & "  INNER JOIN en_mstr on pt_en_id=en_id where pt_type='I' and ps_is_assembly='Y' and pt_en_id in (" & _en_id_coll & ")"

        Else
            get_sequel = "SELECT en_mstr.en_desc, pt_oid as oidptbom, pt_en_id as enptbom, pt_id as idptbom,  " _
                        & "       pt_code as codeptbom, pt_desc1 as descptbom, CASE WHEN pt_desc2 = '.,.,' " _
                        & "THEN 'N' " _
                        & "ELSE 'N' " _
                        & "END AS ptsign " _
                        & "FROM " _
                        & "  public.pt_mstr  " _
                        & "  INNER JOIN en_mstr on pt_en_id=en_id where pt_type='I' and pt_en_id in (0," & _en_id_coll & ")"
                       
        End If

        get_sequel += " order by codeptbom"
        Return get_sequel
    End Function

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

        If fobject.name = "FProductStructure" Or fobject.name = FProductStructureAssembly.Name Then
            If _pil < 3 Then
                If _pil = 1 Then
                    fobject.be_first.EditValue = ds.Tables(0).Rows(_row_gv).Item("idptbom")
                ElseIf _pil = 2 Then
                    fobject.be_to.EditValue = ds.Tables(0).Rows(_row_gv).Item("idptbom")
                ElseIf _pil = 0 Then
                    fobject.ps_pt_bom_id.EditValue = ds.Tables(0).Rows(_row_gv).Item("idptbom")
                End If
            Else
                fobject.gv_edit.SetRowCellValue(_row, "psd_pt_bom_id", ds.Tables(0).Rows(_row_gv).Item("idptbom"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("codeptbom"))
                fobject.gv_edit.SetRowCellValue(_row, "psd_use_bom", "N")
                fobject.gv_edit.SetRowCellValue(_row, "psd_indirect", "N")
                fobject.gv_edit.SetRowCellValue(_row, "pt_desc", ds.Tables(0).Rows(_row_gv).Item("descptbom"))
                fobject.gv_edit.SetRowCellValue(_row, "um_desc", ds.Tables(0).Rows(_row_gv).Item("um_desc"))
                fobject.gv_edit.BestFitColumns()
            End If

        ElseIf fobject.name = "FPSRawMat" Then
            If _pil = 1 Then
                fobject.be_from.EditValue = ds.Tables(0).Rows(_row_gv).Item("codeptbom")
                fobject.be_from.tag = ds.Tables(0).Rows(_row_gv).Item("idptbom")
            ElseIf _pil = 2 Then
                fobject.be_to.EditValue = ds.Tables(0).Rows(_row_gv).Item("codeptbom")
                fobject.be_to.tag = ds.Tables(0).Rows(_row_gv).Item("idptbom")
            End If
        ElseIf fobject.name = "FWhereInUsedReport" Then
            fobject._pt_id = ds.Tables(0).Rows(_row_gv).Item("idptbom")
            fobject.be_first.EditValue = ds.Tables(0).Rows(_row_gv).Item("codeptbom")
        ElseIf fobject.name = FSimulatedPickList.Name Then
            fobject._pt_id = ds.Tables(0).Rows(_row_gv).Item("idptbom")
            fobject.par_pt_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("codeptbom")
        ElseIf fobject.name = "FInventoryAssembly" Then

            fobject._ps_oid = ds.Tables(0).Rows(_row_gv).Item("ps_oid").ToString
            fobject._pt_id = ds.Tables(0).Rows(_row_gv).Item("idptbom")
            fobject._si_id = ds.Tables(0).Rows(_row_gv).Item("pt_si_id")
            'fobject._loc_id = ds.Tables(0).Rows(_row_gv).Item("invc_loc_id")
            'fobject._ptd_ac_id = ds_bantu.Tables(0).Rows(_row_gv).Item("pla_ac_id")
            'fobject._ptd_ac_code = ds_bantu.Tables(0).Rows(_row_gv).Item("pla_ac_id")
            'fobject.asmb_loc.text = ds.Tables(0).Rows(_row_gv).Item("loc_desc")
            fobject._asmb_cost = ds.Tables(0).Rows(_row_gv).Item("invct_cost")
            fobject.be_asmb_ps.EditValue = ds.Tables(0).Rows(_row_gv).Item("codeptbom")
            fobject.asmb_desc.EditValue = ds.Tables(0).Rows(_row_gv).Item("descptbom")
            fobject.asmb_qty.EditValue = 1
            fobject.asmb_qty.focus()
        ElseIf fobject.name = "FInventoryDisAssembly" Then

            fobject._ps_oid = ds.Tables(0).Rows(_row_gv).Item("ps_oid").ToString
            fobject._pt_id = ds.Tables(0).Rows(_row_gv).Item("idptbom")
            fobject._si_id = ds.Tables(0).Rows(_row_gv).Item("invc_si_id")
            fobject._loc_id = ds.Tables(0).Rows(_row_gv).Item("invc_loc_id")
            'fobject._ptd_ac_id = ds_bantu.Tables(0).Rows(_row_gv).Item("pla_ac_id")
            'fobject._ptd_ac_code = ds_bantu.Tables(0).Rows(_row_gv).Item("pla_ac_id")
            fobject.asmb_loc_desc.text = ds.Tables(0).Rows(_row_gv).Item("loc_desc")
            fobject._asmb_cost = ds.Tables(0).Rows(_row_gv).Item("invct_cost")
            fobject.be_asmb_ps.EditValue = ds.Tables(0).Rows(_row_gv).Item("codeptbom")
            fobject.asmb_desc.EditValue = ds.Tables(0).Rows(_row_gv).Item("descptbom")
            fobject.asmb_qty.EditValue = 1
            fobject.asmb_qty.focus()
        End If

    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub
End Class
