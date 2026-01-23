Imports master_new.ModFunction

Public Class FPayOutSearch
    Public _row, _en_id, _pt_id As Integer

    Private Sub FSlsProgSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        'add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "payout_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Sales Rate", "payout_sls_inctv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "p2")
        add_column(gv_master, "Parent", "payout_prn_inctv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "p2")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.payout_oid, " _
                & "  a.payout_id, " _
                & "  a.payout_code, " _
                & "  a.payout_pi_id, " _
                & "  a.payout_sls_inctv, " _
                & "  a.payout_prn_inctv, " _
                & "  a.payout_slsgrp_id, " _
                & "  a.payout_is " _
                & "FROM " _
                & "  public.payout_mstr a"
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
        Dim ds_bantu As New DataSet

        fobject.sls_payout_id.Tag = ds.Tables(0).Rows(_row_gv).Item("payout_id")
        fobject.sls_payout_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("payout_code")

    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        'gv_master.Focus()
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub

    Private Function _um_conv() As Object
        Throw New NotImplementedException
    End Function

End Class
