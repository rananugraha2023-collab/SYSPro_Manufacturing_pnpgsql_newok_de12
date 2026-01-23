Public Class FkartustokPrint
    Public dt_bantu As DataTable
    Public func_data As New function_data

    Private Sub FSalesOrderShipmentPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'le_entity.Properties.DataSource = dt_bantu
        'le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'le_entity.ItemIndex = 0

        init_le(le_entity, "en_mstr")
        init_le(par_loc, "loc_mstr", le_entity.EditValue)

        TxtStartDate.DateTime = Now
        TxtEndDate.DateTime = Now
    End Sub


    Public Overrides Sub preview()


        Dim _en_id_all As String

        If CeChild.EditValue = True Then
            _en_id_all = get_en_id_child(le_entity.EditValue)
        Else

            _en_id_all = le_entity.EditValue

        End If

        Dim _sql As String


        _sql = "SELECT  " _
            & "  a.invh_oid, " _
            & "  b.en_desc, " _
            & "  a.invh_date, " _
            & "  a.invh_desc, " _
            & "  c.si_desc, " _
            & "  d.loc_desc, " _
            & "  e.pt_code, " _
            & "  e.pt_desc1, " _
            & "  e.pt_desc2, a.dt_timestamp, " _
            & "  a.invh_trn_code, " _
            & "  a.invh_qty, a.invh_qty_old, " _
            & "  a.invh_qty_old as awal, " _
            & "  case  " _
            & "  when invh_qty < 0 then " _
            & "  round(invh_qty*(-1),2)  " _
            & "  else 0 " _
            & "  END as keluar, " _
            & " case  " _
            & "  when invh_qty > 0 then " _
            & "  round(invh_qty,2) " _
            & "   else 0 " _
            & "   END as masuk,  " _
            & "   case  " _
            & "  when invh_qty < 0 then " _
            & "  round(invh_qty_old + (invh_qty),2)  " _
            & "  else round(invh_qty_old + invh_qty,2) " _
            & "  END as akhir " _
            & "    " _
            & "FROM " _
            & "  public.invh_mstr a " _
            & "  INNER JOIN public.en_mstr b ON (a.invh_en_id = b.en_id) " _
            & "  INNER JOIN public.si_mstr c ON (a.invh_si_id = c.si_id) " _
            & "  INNER JOIN public.loc_mstr d ON (a.invh_loc_id = d.loc_id) " _
            & "  INNER JOIN public.pt_mstr e ON (a.invh_pt_id = e.pt_id) " _
            & "WHERE " _
            & " " _
            & "  invh_date BETWEEN " & master_new.ModFunction.SetDate(TxtStartDate.DateTime) & " AND " & master_new.ModFunction.SetDate(TxtEndDate.DateTime) & " " _
            & "  AND pt_code = " & master_new.ModFunction.SetSetring(pt_id.EditValue) & "  AND en_id = " & "(" & _en_id_all & ") " _
            & "  order by  " _
            & "  e.pt_code, a.dt_timestamp"

        Dim rpt As New XR_kartustok
        With rpt
            Dim ds As New DataSet
            ds = master_new.PGSqlConn.ReportDataset(_sql)
            If ds.Tables(0).Rows.Count < 1 Then
                MsgBox("Maaf data kosong")
                Exit Sub
            End If

            ' Exit Sub
            .XrLblPeriode.Text = TxtStartDate.DateTime.ToString("dd/MM/yyyy") & " to " & TxtEndDate.DateTime.ToString("dd/MM/yyyy")
            '.XrPivotGrid1.DataSource = ds
            '.XrPivotGrid1.DataMember = "Table"
            .DataSource = ds
            .DataMember = "Table1"

             Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
  
            printTool.PreviewForm.Text = "Kartu Stok"
            '.PrintingSystem = ps
            printTool.ShowPreview()

        End With

    End Sub


    Private Sub pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles pt_id.ButtonClick
        Try
            Dim frm As New FPartNumberSearch
            frm.set_win(Me)
            frm._obj = pt_id
            frm._en_id = le_entity.EditValue
            frm._si_id = par_loc.EditValue
            frm.type_form = True
            frm.ShowDialog()



        Catch ex As Exception
            'Pesan(Err)
        End Try
    End Sub

    Private Sub le_entity_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_entity.EditValueChanged
        Try
            init_le(par_loc, "loc_mstr", le_entity.EditValue)
        Catch ex As Exception
        End Try
    End Sub
End Class
