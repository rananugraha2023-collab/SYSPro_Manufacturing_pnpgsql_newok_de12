Imports master_new.ModFunction

Public Class FUMSearch
    Public _row, _en_id, _pt_id As Integer

    Private Sub FUMSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "code_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "select en_desc, code_id, code_code, code_name, code_desc " + _
                     " from code_mstr " + _
                     " inner join en_mstr on code_en_id = en_id " + _
                     " where code_field ~~* 'unitmeasure'" _
                    & " and (code_code ~~* '%" + Trim(te_search.Text) + "%' or code_name ~~* '%" + Trim(te_search.Text) + "%' or code_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and code_active ~~* 'Y'" _
                    & " and code_en_id in (0," + _en_id.ToString + ")" _
                    & " order by code_desc"
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

        If fobject.name = "FPurchaseOrder" Or fobject.name = "FPurchaseOrderFilm" Then
            'by sys 20110418 ---------------------
            Dim _um_conv As Double = 1
            Try
                Using objcek As New master_new.WDABasepgsql("", "")
                    With objcek
                        .Connection.Open()
                        .Command = .Connection.CreateCommand
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "select um_conv from um_mstr " + _
                               " where um_pt_id = " + _pt_id.ToString + _
                               " and um_pt_um = " + ds.Tables(0).Rows(_row_gv).Item("code_id").ToString + _
                               " and um_pt_um_alt = (select pt_um from pt_mstr where pt_id = " + _pt_id.ToString + ") "
                        .InitializeCommand()
                        .DataReader = .Command.ExecuteReader
                        While .DataReader.Read
                            _um_conv = .DataReader("um_conv")
                        End While
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            '------------------------------------------------------
            fobject.gv_edit.SetRowCellValue(_row, "pod_um", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_um_conv", _um_conv)
            fobject.gv_edit.BestFitColumns()

        ElseIf fobject.name = "FRequisition" Then
            'by sys 20110418 ---------------------
            Dim _um_conv As Double = 1
            Try
                Using objcek As New master_new.WDABasepgsql("", "")
                    With objcek
                        .Connection.Open()
                        .Command = .Connection.CreateCommand
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "select um_conv from um_mstr " + _
                               " where um_pt_id = " + _pt_id.ToString + _
                               " and um_pt_um = " + ds.Tables(0).Rows(_row_gv).Item("code_id").ToString + _
                               " and um_pt_um_alt = (select pt_um from pt_mstr where pt_id = " + _pt_id.ToString + ") "
                        .InitializeCommand()
                        .DataReader = .Command.ExecuteReader
                        While .DataReader.Read
                            _um_conv = .DataReader("um_conv")
                        End While
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            fobject.gv_edit.SetRowCellValue(_row, "reqd_um", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_um_conv", _um_conv)
            fobject.gv_edit.BestFitColumns()

        ElseIf fobject.name = "FInventoryRequest" Or fobject.name = "FInventoryRequestCabang" Then
            fobject.gv_edit.SetRowCellValue(_row, "pbd_um", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FSalesOrder" Then
            fobject.gv_edit.SetRowCellValue(_row, "sod_um", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_edit.SetRowCellValue(_row, "um_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
            fobject.gv_edit.BestFitColumns()
        End If
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
