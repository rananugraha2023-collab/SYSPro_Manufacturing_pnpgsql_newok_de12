Imports master_new.ModFunction

Public Class FTaxInvoiceAttachmentPrint
    Dim dt_bantu As DataTable
    Dim func_data As New function_data

    Private Sub FTaxInvoiceAttachmentPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        le_entity.Properties.DataSource = dt_bantu
        le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        le_entity.ItemIndex = 0
    End Sub

    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_first.ButtonClick
        Dim frm As New FFakturPajakSearch
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_first
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_to.ButtonClick
        Dim frm As New FFakturPajakSearch
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_to
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Public Overrides Sub preview()
        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "select ti_code,  " _
                & " ti_date,  " _
                & " ar_code,  " _
                & " ar_date,  " _
                & " pt_code,  " _
                & " pt_desc1,  " _
                & " (tip_price - tip_disc) * tip_qty as tip_price,  " _
                & " tip_ppn * tip_qty as tip_ppn, ((tip_price - tip_disc) * tip_qty) + (tip_ppn * tip_qty) as tip_total,  " _
                & " code_name as sign_name, " _
                & " cmaddr_tax_line_3 " _
                & " from tip_pt " _
                & " inner join ti_mstr on ti_oid = tip_ti_oid " _
                & " inner join pt_mstr on pt_id = tip_pt_id " _
                & " inner join ars_ship on ars_soshipd_oid = tip_soshipd_oid " _
                & " inner join ar_mstr on ar_oid = ars_ar_oid " _
                & " inner join code_mstr on code_id = ti_sign_id " _
                & " inner join cmaddr_mstr on cmaddr_en_id = ti_en_id " _
                & " where ti_mstr.ti_code >= '" + be_first.Text + "'" _
                & " and ti_mstr.ti_code <= '" + be_to.Text + "'"

        Dim rpt As Object = Nothing

        rpt = New XRTaxInvoiceAttachment
        Try
            With rpt
                Try
                    Using objcb As New master_new.WDABasepgsql("", "")
                        With objcb
                            .SQL = _sql
                            .InitializeCommand()
                            .FillDataSet(ds_bantu, "data")
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try

                If ds_bantu.Tables(0).Rows.Count = 0 Then
                    MessageBox.Show("Data Doens't Exist.., Contact Your Admin Program..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                .DataSource = ds_bantu
                .DataMember = "data"
                .ShowPreview()

            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
