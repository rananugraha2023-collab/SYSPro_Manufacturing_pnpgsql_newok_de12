Imports master_new.ModFunction

Public Class FPartNumberSearchPacking
    Public _row, _en_id, _si_id As Integer
    Public _obj As Object
    Public _so_type, _soo_code As String
    Public _sq_booking As String
    Public _tran_oid As String = ""
    Public _trans_oid As String = ""
    Public _ppn_type As String = ""
    Dim func_data As New function_data
    Public _filter As String
    Public grid_call As String = ""
    Public _so_cash As Boolean = False
    'Public _sq_booking As Boolean = False
    Public _qty_receive As Double
    Public _pt_id As Integer

    Private Sub FPartNumberSearchPacking_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Barcode", "pt_syslog_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Qty Open", "qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_master, "Qty", "sod_qty_shipment", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column(gv_master, "Qty", "invc_qty_available", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Approval Status (Initial, Tax, Accounting)", "pt_approval_status", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Taxable", "pt_taxable", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Tax Class", "tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "PPN Type", "pt_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Cost", "invct_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
    End Sub

    Public Overrides Function get_sequel() As String
        Dim _en_id_coll As String = func_data.entity_parent(_en_id)
        get_sequel = "SELECT DISTINCT " _
                    & "  public.so_mstr.so_en_id, " _
                    & "  public.en_mstr.en_id, " _
                    & "  public.pt_mstr.pt_id, " _
                    & "  public.pt_mstr.pt_code, " _
                    & "  public.pt_mstr.pt_desc1, " _
                    & "  public.pt_mstr.pt_desc2, " _
                    & "  SUM (coalesce (public.sod_det.sod_qty,0)) as qty, " _
                    & "  SUM (coalesce(public.sod_det.sod_qty,0)) as qty_packing, " _
                    & "  SUM (coalesce (public.sod_det.sod_qty_shipment)) qty_open " _
                    & "FROM " _
                    & "  public.sod_det " _
                    & "  INNER JOIN public.so_mstr ON (public.sod_det.sod_so_oid = public.so_mstr.so_oid) " _
                    & "  INNER JOIN public.pt_mstr ON (public.sod_det.sod_pt_id = public.pt_mstr.pt_id) " _
                    & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id) " _
                    & "  where public.so_mstr.so_code in (" + _soo_code + ") " _
                    & "  GROUP BY public.so_mstr.so_en_id, " _
                    & "  public.en_mstr.en_id, " _
                    & "  public.pt_mstr.pt_id, " _
                    & "  public.pt_mstr.pt_code, " _
                    & "  public.pt_mstr.pt_desc1, " _
                    & "  public.pt_mstr.pt_desc2 "

        Return get_sequel
    End Function

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub

    Public Overrides Sub fill_data()
        Try


            Dim _row_gv As Integer
            _row_gv = BindingContext(ds.Tables(0)).Position
            Dim dt_bantu As New DataTable()
            Dim func_coll As New function_collection

            'fobject.gv_edit_shipment.SetRowCellValue(_row, "ceklist", ds.Tables(0).Rows(_row_gv).Item("ceklist"))
            fobject.gv_edit_shipment.SetRowCellValue(_row, "pcklsd_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit_shipment.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit_shipment.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        'gv_master.Focus()

        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            fill_data()
            Me.Close()
        End If
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
