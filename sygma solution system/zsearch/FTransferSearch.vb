Imports master_new.ModFunction

Public Class FTransferSearch
    Public _row As Integer
    Public _en_id As Integer
    Public _obj As Object
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FTransferSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        pr_txttglawal.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Transfer Number", "ptsfr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Receive Date", "ptsfr_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Remarks", "ptsfr_remarks", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FDRCRMemoKonsiyasiPrint" Then
            get_sequel = "SELECT  " _
                        & "  ptsfr_code, " _
                        & "  ptsfr_date,ptsfr_remarks " _
                        & "FROM  " _
                        & "  public.ptsfr_mstr " _
                        & "  inner join so_mstr on so_oid = ptsfr_so_oid" _
                        & "  where ptsfr_en_id = " & _en_id.ToString _
                        & "  and ptsfr_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                        & "  and ptsfr_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                        & "  order by ptsfr_code "
        Else
            get_sequel = "SELECT  " _
                        & "  ptsfr_code, " _
                        & "  ptsfr_date,ptsfr_remarks " _
                        & "FROM  " _
                        & "  public.ptsfr_mstr " _
                        & "  where ptsfr_en_id = " & _en_id.ToString _
                        & "  and ptsfr_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                        & "  and ptsfr_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                        & "  order by ptsfr_code "
        End If
        
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
        If fobject.name = "FTransferIssuesPrint" Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_code")
        ElseIf fobject.name = "FTransferReceiptsPrint" Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_code")
        ElseIf fobject.name = "FDRCRMemoKonsiyasiPrint" Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_code")
        ElseIf fobject.name = FTransferIssueExport.Name Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_code")
            _obj.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_remarks")
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub
End Class
