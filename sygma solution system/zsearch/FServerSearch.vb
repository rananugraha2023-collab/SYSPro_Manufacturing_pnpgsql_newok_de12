Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FServerSearch
    Public _row, _en_id As Integer
    Public _obj As Object
    Public _type As String
    Public _filter As String

    Private Sub FSiteSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        'Me.Width = 371
        'Me.Height = 360
        te_search.Focus()
        help_load_data(True)
    End Sub

    Public Overrides Sub format_grid()
        add_column_edit(gv_master, "Select", "checklist", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Server", "conf_name", DevExpress.Utils.HorzAlignment.Default)


    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                    & "  a.conf_oid,false as checklist, " _
                    & "  a.conf_name, " _
                    & "  a.conf_ip, " _
                    & "  a.conf_port, " _
                    & "  a.conf_db, " _
                    & "  a.conf_user, " _
                    & "  a.conf_en_id " _
                    & "FROM " _
                    & "  public.dash_conf a " _
                    & "ORDER BY " _
                    & "  a.conf_name"

      

        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
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

    Public Overrides Sub fill_data()
        'Dim sSQL As String
        Try
            ds.AcceptChanges()

            Dim _row_gv As Integer
            _row_gv = BindingContext(ds.Tables(0)).Position

            If fobject.name = FChartSalesYearMultiSeries.Name Then

                Dim _hasil As String = ""

                For Each dr As DataRow In ds.Tables(0).Rows
                    If dr("checklist") = True Then
                        _hasil = _hasil & dr("conf_name") & ","
                    End If
                Next
                If _hasil.Length > 0 Then
                    _hasil = Microsoft.VisualBasic.Left(_hasil, _hasil.Length - 1)
                End If
                _obj.text = _hasil
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub

End Class
