Public Class FQRCodeSearchTo
    Public _row As Integer
    Public _en_id As Integer
    Public _obj As Object
    Public _date As Date
    Public _objk As Object
    Public grid_call As String = ""

    Private Sub FQRCodeSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        'add_column(gv_master, "Code", "qrcode_oid", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "qrcode_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "QR Code ID", "qrcode_id", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  public.qrcode_mstr.qrcode_oid, " _
                    & "  public.qrcode_mstr.qrcode_en_id, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.qrcode_mstr.qrcode_id, " _
                    & "  public.qrcode_mstr.qrcode_name, " _
                    & "  public.qrcode_mstr.qrcode_active " _
                    & "FROM " _
                    & "  public.qrcode_mstr " _
                    & "  INNER JOIN public.en_mstr ON (public.qrcode_mstr.qrcode_en_id = public.en_mstr.en_id) " _
                    & "WHERE " _
                    & "  public.qrcode_mstr.qrcode_reff_oid IS NULL AND  " _
                    & "  public.qrcode_mstr.qrcode_active ~~* 'N' " _
                    & " order by qrcode_id ASC " _
                    & "  limit " + _obj.ToString
        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        'gv_master.Focus()
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub

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

        If fobject.name = "FPurchaseReceipt" Then
            'fobject._rcv_freff_oids = ds.Tables(0).Rows(_row_gv).Item("qrcode_oid")
            'fobject.rcv_freff_oid.Text = ds.Tables(0).Rows(_row_gv).Item("qrcode_name")

            fobject._rcv_treff_oids = ds.Tables(0).Rows(_row_gv).Item("qrcode_oid").ToString
            fobject.rcv_treff_oid.Text = ds.Tables(0).Rows(_row_gv).Item("qrcode_name")

        ElseIf fobject.name = "FInventoryReceipts" Then
            'fobject._rcv_freff_oids = ds.Tables(0).Rows(_row_gv).Item("qrcode_oid")
            'fobject.rcv_freff_oid.Text = ds.Tables(0).Rows(_row_gv).Item("qrcode_name")

            fobject._riu_treff_oids = ds.Tables(0).Rows(_row_gv).Item("qrcode_oid").ToString
            fobject.riu_treff_oid.Text = ds.Tables(0).Rows(_row_gv).Item("qrcode_name")


            'ElseIf fobject.name = "FRequisition" Then
            '    fobject.gv_edit.SetRowCellValue(_row, "reqd_en_id", ds.Tables(0).Rows(_row_gv).Item("en_id"))
            '    fobject.gv_edit.SetRowCellValue(_row, "en_desc", ds.Tables(0).Rows(_row_gv).Item("en_desc"))
            'ElseIf fobject.name = "FGLCalendar" Then
            '    fobject.gv_edit.SetRowCellValue(_row, "gcald_en_id", ds.Tables(0).Rows(_row_gv).Item("en_id"))
            '    fobject.gv_edit.SetRowCellValue(_row, "en_desc", ds.Tables(0).Rows(_row_gv).Item("en_desc"))
            'ElseIf fobject.name = "FSalesOrder" Then
            '    fobject.gv_edit.SetRowCellValue(_row, "so_en_id", ds.Tables(0).Rows(_row_gv).Item("en_id"))
            '    fobject.gv_edit.SetRowCellValue(_row, "en_desc", ds.Tables(0).Rows(_row_gv).Item("en_desc"))
            'ElseIf fobject.name = "FInventoryRequest" Or fobject.name = "FInventoryRequestCabang" Then
            '    fobject.gv_edit.SetRowCellValue(_row, "pbd_en_id", ds.Tables(0).Rows(_row_gv).Item("en_id"))
            '    fobject.gv_edit.SetRowCellValue(_row, "en_desc", ds.Tables(0).Rows(_row_gv).Item("en_desc"))
            'ElseIf fobject.name = "FProjectMaintenance" Then
            '    If grid_call = "gv_edit" Then
            '        fobject.gv_edit.SetRowCellValue(_row, "prjd_en_id", ds.Tables(0).Rows(_row_gv).Item("en_id"))
            '        fobject.gv_edit.SetRowCellValue(_row, "en_desc", ds.Tables(0).Rows(_row_gv).Item("en_desc"))
            '    ElseIf grid_call = "gv_edit_cust" Then
            '        fobject.gv_edit_cust.SetRowCellValue(_row, "prjc_en_id", ds.Tables(0).Rows(_row_gv).Item("en_id"))
            '        fobject.gv_edit_cust.SetRowCellValue(_row, "en_desc", ds.Tables(0).Rows(_row_gv).Item("en_desc"))
            '    End If
        End If
    End Sub
End Class
