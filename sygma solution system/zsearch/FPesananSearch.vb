Imports master_new.ModFunction

Public Class FPesananSearch
    Public _row As Integer
    Public _en_id, _ptnr_id, _cu_id As Integer
    Public _obj As Object
    Public func_data As New function_data
    Public _ppn_type, _so_cash As String
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FFakturPajakSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()

        ' add_column_edit(gv_master, "Select", "pilih", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "pesan_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Date", "pesan_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_master, "Sales Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Sales Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Old Customer", "pesan_status_customer_lama", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Customer Code", "pesan_kode_costumer", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Customer Name", "pesan_customer", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Product", "pesan_nama_barang", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""

        get_sequel = "SELECT  " _
            & "  a.pesan_oid,en_desc, " _
            & "  a.pesan_code, " _
            & "  a.pesan_date, " _
            & "  a.pesan_sales_id,  b.ptnr_code,  b.ptnr_name,  " _
            & "  a.pesan_en_id, " _
            & "  a.pesan_add_date, " _
            & "  a.pesan_status_customer_lama, " _
            & "  a.pesan_kode_costumer, " _
            & "  a.pesan_customer, " _
            & "  a.pesan_costumer_alamat, " _
            & "  a.pesan_costumer_hp, " _
            & "  a.pesan_kode_barang1, " _
            & "  a.pesan_nama_barang1,  a.pesan_nama_barang1 || ' [' || to_char(coalesce(pesan_qty1,0),'FM999,999,999') || '], ' " _
            & " || pesan_nama_barang2 || ' [' || to_char(coalesce(pesan_qty2,0),'FM999,999,999')  || '], ' || pesan_nama_barang3 || ' [' " _
            & " ||  to_char(coalesce(pesan_qty3,0),'FM999,999,999') || '], ' || pesan_nama_barang4 || ' [' || to_char(coalesce(pesan_qty4,0),'FM999,999,999') || ']' as pesan_nama_barang, " _
            & "  a.pesan_qty1, " _
            & "  a.pesan_ket1, " _
            & "  a.pesan_kode_barang2, " _
            & "  a.pesan_nama_barang2, " _
            & "  a.pesan_qty2, " _
            & "  a.pesan_ket2, " _
            & "  a.pesan_kode_barang3, " _
            & "  a.pesan_nama_barang3, " _
            & "  a.pesan_qty3, " _
            & "  a.pesan_ket3, " _
            & "  a.pesan_kode_barang4, " _
            & "  a.pesan_nama_barang4, " _
            & "  a.pesan_qty4, " _
            & "  a.pesan_ket4, coalesce(pesan_status,'') as pesan_status " _
            & "FROM " _
            & "  public.pesanan a " _
            & "   INNER JOIN public.ptnr_mstr b ON (a.pesan_sales_id = b.ptnr_id) " _
            & "   INNER JOIN public.en_mstr c ON  (a.pesan_en_id = c.en_id) " _
            & "WHERE " _
            & "  a.pesan_date BETWEEN " & SetDate(pr_txttglawal.DateTime.Date) & " AND " & SetDate(pr_txttglakhir.DateTime.Date) & "  AND  " _
            & "  a.pesan_en_id = " + _en_id.ToString & " and  coalesce(pesan_status,'')='Belum Proses' " _
            & "ORDER BY " _
            & "  a.pesan_code"
        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
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

        Dim ds_bantu As New DataSet
        If fobject.name = FSalesOrder.Name Then

            'Dim _hasil As String = ""
            'For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            '    If ds.Tables(0).Rows(i).Item("pilih") = True Then
            '        'If i < ds.Tables(0).Rows.Count Then
            '        '    _hasil = _hasil & ds.Tables(0).Rows(_row_gv).Item("pesan_code") & ","
            '        'Else
            '        _hasil = _hasil & ds.Tables(0).Rows(i).Item("pesan_code") & ","
            '        'End If

            '    End If

            'Next
            '_obj.text = _hasil

            _obj.text = ds.Tables(0).Rows(_row_gv).Item("pesan_code")
            _obj.tag = ds.Tables(0).Rows(_row_gv).Item("pesan_oid").ToString

            fobject.so_ptnr_id_sold.editvalue = ds.Tables(0).Rows(_row_gv).Item("pesan_customer")

            'pesan_status_customer_lama
            Dim ssql As String
            If ds.Tables(0).Rows(_row_gv).Item("pesan_status_customer_lama") = True Then

                ssql = "select ptnr_id from ptnr_mstr where ptnr_code='" & ds.Tables(0).Rows(_row_gv).Item("pesan_kode_costumer") & "'"
                fobject._so_ptnr_id_sold_mstr = master_new.PGSqlConn.GetRowInfo(ssql)(0)
            Else
                fobject._so_ptnr_id_sold_mstr = -1
            End If
            fobject.so_shipping_address.editvalue = ds.Tables(0).Rows(_row_gv).Item("pesan_costumer_alamat")

            fobject.so_sales_person.editvalue = ds.Tables(0).Rows(_row_gv).Item("pesan_sales_id")

        End If
    End Sub

    Private Sub BtSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        fill_data()
    End Sub
End Class
