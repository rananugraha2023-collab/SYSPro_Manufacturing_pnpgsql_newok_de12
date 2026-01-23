Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports npgsql

Public Class frmEntitySearch
    Public userid As String
    Private Sub FSiteSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        'Me.Width = 371
        'Me.Height = 360
        te_search.Focus()
        help_load_data(True)
    End Sub

    Public Overrides Sub format_grid()

        add_column_edit(gv_master, "Select", "pilih", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "en_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "en_desc", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT DISTINCT  " _
            & "  false as pilih, public.en_mstr.en_id, " _
            & "  public.en_mstr.en_code, " _
            & "  public.en_mstr.en_desc " _
            & "FROM " _
            & "  public.en_mstr " _
            & "WHERE " _
            & "  public.en_mstr.en_desc ~~* '%" & te_search.Text & "%' " _
            & "ORDER BY " _
            & "  public.en_mstr.en_desc"




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

            'If fobject.name = FChartSalesYearMultiSeries.Name Then

            'Dim _hasil As String = ""
            Dim ssqls As New ArrayList

            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                      

                        For Each dr As DataRow In ds.Tables(0).Rows
                            If dr("pilih") = True Then
                                '_hasil = _hasil & dr("conf_name") & ","

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "insert into tconfuserentity (userid, user_en_id) " + _
                                                       " values (" + userid + "," + _
                                                       dr("en_id").ToString + ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                            End If
                        Next


                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        'load_data_grid_detail()
                        'MessageBox.Show("Congratulation " + master_new.ClsVar.sNama + ", Data Have Been Saved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'set_row(_userid, "userid")
                        fobject.load_entity()
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using




            'If _hasil.Length > 0 Then
            '    _hasil = Microsoft.VisualBasic.Left(_hasil, _hasil.Length - 1)
            'End If


            '_obj.text = _hasil
            'End If
        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub

End Class
