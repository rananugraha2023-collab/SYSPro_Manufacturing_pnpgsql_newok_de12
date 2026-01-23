Imports master_new.ModFunction

Public Class FCashOutSearch
    Public _row, _en_id, _ptnr_id As Integer
    Public _obj As Object

    Private Sub FCostCenterSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        'Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Cash Out Number", "casho_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Date", "casho_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_master, "Amount", "casho_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_master, "Remarks", "casho_remarks", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String

        'get_sequel = "SELECT  " _
        '    & "  a.cashi_code,cashi_oid, " _
        '    & "  a.cashi_date, " _
        '    & "  a.cashi_amount_remains " _
        '    & "FROM " _
        '    & "  public.cashi_in a " _
        '    & "WHERE " _
        '    & "  cashi_so_oid is not null and cashi_amount_remains > 0 and   a.cashi_ptnr_id = " & SetInteger(_ptnr_id) _
        '    & "ORDER BY " _
        '    & "  a.cashi_code"


        If fobject.name = FCashOut.Name Then
            get_sequel = "SELECT  " _
               & "  a.casho_code, " _
               & "  a.casho_date, " _
               & "  a.casho_oid, " _
               & "  a.casho_amount, " _
               & "  a.casho_remarks, " _
               & "  a.casho_type " _
               & "FROM " _
               & "  public.casho_out a " _
               & "WHERE " _
               & " casho_type='TEMP' and coalesce(casho_close,'N')='N' and  a.casho_ptnr_id =  " & SetInteger(_ptnr_id) _
               & " ORDER BY " _
               & "  a.casho_date"
        ElseIf fobject.name = FCashIn.Name Then
            get_sequel = "SELECT  " _
               & "  a.casho_code, " _
               & "  a.casho_date, " _
               & "  a.casho_oid, " _
               & "  a.casho_amount, " _
               & "  a.casho_remarks, " _
               & "  a.casho_type " _
               & "FROM " _
               & "  public.casho_out a " _
               & "WHERE " _
               & " casho_type='TEMP' and casho_amount -  coalesce(casho_amount_reverse,0) >0 and coalesce(casho_close,'N')='N' and  a.casho_ptnr_id =  " & SetInteger(_ptnr_id) _
               & " ORDER BY " _
               & "  a.casho_date"
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
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position

        ' If fobject.name = FARPaymentDetail.Name Then
        _obj.text = ds.Tables(0).Rows(_row_gv).Item("casho_code")
        _obj.tag = ds.Tables(0).Rows(_row_gv).Item("casho_oid").ToString
        ' fobject.arpay_cashi_amount.editvalue = ds.Tables(0).Rows(_row_gv).Item("cashi_amount_remains")


        'End If
    End Sub


    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        'gv_master.Focus()
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub

End Class
