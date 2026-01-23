Imports master_new.ModFunction

Public Class FInvRiuSearch
    Public _row As Integer
    Public _en_id As Integer
    Public _obj As Object
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FInvRiuSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        pr_txttglawal.Focus()

        If fobject.name = "FInvReceiptsPrint" Then
            Me.Text = "Inventory Receipt Search"
        ElseIf fobject.name = "FInvIssuePrint" Then
            Me.Text = "Inventory Issue Search"
        End If
    End Sub

    Public Overrides Sub format_grid()
        If fobject.name = "FInvReceiptsPrint" Then
            add_column(gv_master, "Inventory Issue Code", "riu_type2", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Issue Date", "riu_date", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "SO Number", "riu_ref_so_code", DevExpress.Utils.HorzAlignment.Default)
        ElseIf fobject.name = "FInvIssuePrint" Then
            add_column(gv_master, "Inventory Receipt Code", "riu_type2", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Receipt Date", "riu_date", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "PB Number", "riu_ref_pb_code", DevExpress.Utils.HorzAlignment.Default)
        End If
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""

        get_sequel = "SELECT  " _
            & "  riu_mstr.riu_oid, " _
            & "  riu_mstr.riu_dom_id, " _
            & "  riu_mstr.riu_en_id, " _
            & "  riu_mstr.riu_add_by, " _
            & "  riu_mstr.riu_add_date, " _
            & "  riu_mstr.riu_upd_by, " _
            & "  riu_mstr.riu_upd_date, " _
            & "  riu_mstr.riu_type2, " _
            & "  riu_mstr.riu_date, " _
            & "  riu_mstr.riu_type, " _
            & "  riu_mstr.riu_remarks, " _
            & "  riu_mstr.riu_dt, " _
            & "  riu_mstr.riu_ref_so_code, " _
            & "  riu_mstr.riu_ref_so_oid, " _
            & "  riu_mstr.riu_ref_pb_oid, " _
            & "  riu_mstr.riu_ref_pb_code " _
            & "FROM " _
            & "  riu_mstr " _
            & "  where riu_en_id = " & _en_id.ToString _
            & "  and riu_date >= '" + pr_txttglawal.Text + "'" _
            & "  and riu_date <= '" + pr_txttglakhir.Text + "' "

        If fobject.name = "FInvIssuePrint" Then
            get_sequel = get_sequel & " and riu_type ~~* 'I' order by riu_type2 "
        ElseIf fobject.name = "FInvReceiptsPrint" Then
            get_sequel = get_sequel & " and riu_type ~~* 'R' order by riu_type2 "
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

        _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("riu_type2")
        
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        'help_load_data(True)
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub
End Class
