Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FCSESearch
    Public _row, _en_id As Integer
    Public _obj As Object
    Public _type As String
    Public _filter As String

    Private Sub FSiteSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        'Me.Width = 371
        'Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Cost Element Code", "cse_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Category", "cse_category", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "cse_desc", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  a.cse_id, " _
                & "  a.cse_code, " _
                & "  a.cse_category, " _
                & "  a.cse_desc " _
                & "FROM " _
                & "  public.cse_mstr a " _
                 & " Where a.cse_desc ~~* '%" + Trim(te_search.Text) + "%' "

        If _filter <> "" Then
            get_sequel += _filter
        End If

        get_sequel += "  order by a.cse_desc"

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
            Dim _row_gv As Integer
            _row_gv = BindingContext(ds.Tables(0)).Position

            If fobject.name = "FProject" Then
                fobject.gv_edit.SetRowCellValue(_row, "prjd_cse_id", ds.Tables(0).Rows(_row_gv).Item("cse_id"))
                fobject.gv_edit.SetRowCellValue(_row, "cse_code", ds.Tables(0).Rows(_row_gv).Item("cse_code"))
                fobject.gv_edit.SetRowCellValue(_row, "cse_desc", ds.Tables(0).Rows(_row_gv).Item("cse_desc"))

                fobject.gv_edit.BestFitColumns()
            ElseIf fobject.name = "FCostSet" Then

                fobject.gv_edit.SetRowCellValue(_row, "cstd_cse_id", ds.Tables(0).Rows(_row_gv).Item("cse_id"))
                fobject.gv_edit.SetRowCellValue(_row, "cse_desc", ds.Tables(0).Rows(_row_gv).Item("cse_desc"))
                fobject.gv_edit.SetRowCellValue(_row, "cse_code", ds.Tables(0).Rows(_row_gv).Item("cse_code"))
                fobject.gv_edit.BestFitColumns()

            End If
        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub

End Class
