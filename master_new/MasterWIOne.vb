Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export

Public Class MasterWIOne
    Public insert_edit As Boolean
    Dim my_form_name As String
    Dim _bt_delete As Boolean = True
    Dim _bt_insert As Boolean = True
    Dim _bt_edit As Boolean = True

    Public Overridable Sub set_window(ByVal arg As MasterMDI)
        fmm = arg
    End Sub

    'form load di delete karena eerror entah kenapa
    Private Sub MasterWIOne_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'form_first_load()
    End Sub

    Public Overrides Sub panel_visibility()
        scc_master.PanelVisibility = spv_master
        scc_detail.PanelVisibility = spv_detail
    End Sub

    Public Overridable Sub set_button_mdi()
       

        Dim conf_menu As String = ""
        my_form_name = Me.Name.Substring(1, Me.Name.Length - 1)

        If xtc_master.SelectedTabPageIndex = 0 Then

            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .Connection.Open()
                        .Command = .Connection.CreateCommand
                        .Command.CommandType = CommandType.Text

                        .Command.CommandText = "select editablestatus,deleteablestatus,insertablestatus " & _
                                               " from tconfusergroup ug " & _
                                               " inner join tconfmenu m on m.groupid = ug.groupid " & _
                                               " inner join tconfmenucollection mc on mc.menuid = m.menuid " & _
                                               " where enablestatus = true " & _
                                               " and userid = " & master_new.ClsVar.sUserID.ToString & _
                                               " and menuname ~~* '" & my_form_name & "'" & _
                                               "UNION  " _
                                                & "SELECT  " _
                                                & "  " _
                                                & "  a.editablestatus, " _
                                                & "  a.deleteablestatus,a.insertablestatus " _
                                                & "FROM " _
                                                & "  public.tconfmenuuser a " _
                                                & "  INNER JOIN public.tconfmenucollection b ON (a.menuid = b.menuid) " _
                                                & "WHERE " _
                                                & "  a.userid = " & master_new.ClsVar.sUserID.ToString & " " _
                                                & " and menuname ~~* '" & my_form_name & "' " _
                                                & " order by insertablestatus,editablestatus,deleteablestatus"
                        .InitializeCommand()
                        .DataReader = .Command.ExecuteReader

                        Dim my As MasterMDI = CType(Me.ParentForm, MasterMDI)

                        While .DataReader.Read
                            ' If .DataReader("editablestatus") = True Then
                            ' conf_menu = "awal_transaksi"
                            If xtc_master.SelectedTabPageIndex = 0 Then
                                conf_menu = "awal_transaksi"
                                _bt_delete = .DataReader("deleteablestatus")
                                _bt_insert = .DataReader("insertablestatus")
                                _bt_edit = .DataReader("editablestatus")
                            ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                                conf_menu = "transaksi"
                            End If
                            'Else
                            'conf_menu = "awal_transaksi"
                            'End If

                        End While

                        Try
                            my.configurasi_menu(conf_menu)
                            If _bt_delete = False Then
                                my.blb_delete.Enabled = False
                            End If
                            If _bt_insert = False Then
                                my.blb_insert.Enabled = False
                            End If
                            If _bt_edit = False Then
                                my.blb_editdata.Enabled = False
                            End If
                        Catch ex As Exception
                        End Try

                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Else
            Dim my As MasterMDI = CType(Me.ParentForm, MasterMDI)
            my.configurasi_menu("transaksi")
        End If
      
    End Sub

    Public Overridable Sub MasterWIOne_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        set_button_mdi()
    End Sub

    Public Overrides Sub form_first_load()
        'create_table()
        help_load_data(False)
        load_cb()
        on_load()
        format_grid()
        add_handler_numeric()
        add_groupsummary()
        AllowIncrementalSearch()
        set_component()
        load_Columns()

        spv_master = scc_master.PanelVisibility
        spv_detail = scc_detail.PanelVisibility

        xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False
        xtp_edit.PageVisible = False

        '========= setting grid style
        fmm.bsi_gridstyle.Caption = "Grid : " + master_new.ClsVar.sGridStyle
        style_grid(get_gv(), master_new.ClsVar.sGridStyle)
        style_grid_detail()
        '============================

        'seting embeddednavigator dan setting gridview

        '================================================================================================
        gc = get_gc()
        gc.UseEmbeddedNavigator = True
        format_embeddednavigator(gc)

        gv = gc.MainView
        gv.OptionsView.ColumnAutoWidth = False
        gv.OptionsView.ShowAutoFilterRow = True
        gv.OptionsView.ShowFooter = True
        gv.OptionsView.ShowGroupedColumns = True

        AddHandler gv.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv.ColumnFilterChanged, AddressOf relation_detail

        '================================================================================================
    End Sub

    Public Overrides Sub help_load_data(ByVal par As Boolean)
        load_data(par, get_gc())
    End Sub

    Public Overrides Sub load_data(ByVal arg As Boolean, ByVal gc As DevExpress.XtraGrid.GridControl)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            set_param_textbox()
            '================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        .SQL = get_sequel()
                        .InitializeCommand()
                        .FillDataSet(ds, Me.Name + "_select")
                        gc.DataSource = ds.Tables(0)
                        ConditionsAdjustment()
                        bestfit_column()
                        load_data_grid_detail()
                    End With
                    BindingContext(ds.Tables(0)).Position = row
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

    Public Overrides Sub load_data_detail(ByVal sql As String, ByVal gc As DevExpress.XtraGrid.GridControl, ByVal tn As String)
        Cursor = Cursors.WaitCursor
        '================================================================
        Try
            Using objload As New master_new.WDABasepgsql("", "")
                With objload
                    .SQL = sql
                    .InitializeCommand()
                    .FillDataSet(ds, tn)
                    gc.DataSource = ds.Tables(tn)
                    Dim gv_detail As New DevExpress.XtraGrid.Views.Grid.GridView
                    gv_detail = gc.MainView
                    gv_detail.BestFitColumns()
                    relation_detail()
                End With
                'BindingContext(ds.Tables(0)).Position = row
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Cursor = Cursors.Arrow
    End Sub

    Public Overrides Sub add_groupsummary()
        gv = get_gc().MainView
        gv.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "groupsummary")
    End Sub

    Public Overrides Sub AllowIncrementalSearch()
        gv = get_gc().MainView
        gv.OptionsBehavior.AllowIncrementalSearch = True
    End Sub

    Public Overrides Sub bestfit_column()
        gv = get_gc().MainView
        gv.BestFitColumns()
    End Sub

    Public Overrides Function insert_data() As Boolean
        If _bt_insert = False Then
            MsgBox("Sorry You do not have authorization")
            Return False
            Exit Function
        End If

        insert_edit = True
        xtc_master.SelectedTabPageIndex = 1
        sc(True)
        Dim my As MasterMDI = CType(Me.ParentForm, MasterMDI)
        my.configurasi_menu("transaksi")

        xtp_data.PageVisible = False
        xtp_edit.PageVisible = True
        scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        scc_detail.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1

        ''------ ini agar combobox diset ke selectedindex  = 0 dan focus ke komponen awal ketika insert data       
        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                For Each ctrl_spc In spc.Controls
                    If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                        sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                        For Each ctrl_sgp In sgp.Controls
                            If TypeOf ctrl_sgp Is DevExpress.XtraEditors.SplitContainerControl Then
                                spcd = CType(ctrl_sgp, DevExpress.XtraEditors.SplitContainerControl)
                                For Each ctrl_spcd In spcd.Controls
                                    If TypeOf ctrl_spcd Is DevExpress.XtraEditors.SplitGroupPanel Then
                                        sgpd = CType(ctrl_spcd, DevExpress.XtraEditors.SplitGroupPanel)
                                        For Each ctrl_sgpd In sgpd.Controls
                                            If TypeOf ctrl_sgpd Is DevExpress.XtraTab.XtraTabControl Then
                                                xtc = CType(ctrl_sgpd, DevExpress.XtraTab.XtraTabControl)
                                                For Each ctrl_xtc In xtc.Controls
                                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                                        If ctrl_xtc.name = "xtp_edit" Then
                                                            xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                                            For Each ctrl_xtp In xtp.Controls
                                                                If TypeOf ctrl_xtp Is Panel Then
                                                                    pnl = CType(ctrl_xtp, Panel)
                                                                    For Each ctrl_pnl In pnl.Controls
                                                                        If ctrl_pnl.Name.Substring(0, 5) = "sc_cb" Then
                                                                            If TypeOf ctrl_pnl Is ComboBox Then
                                                                                ctrl_pnl.Selectedindex = 0
                                                                            ElseIf TypeOf ctrl_pnl Is DevExpress.XtraEditors.LookUpEdit Then
                                                                                ctrl_pnl.itemindex = 0
                                                                            End If
                                                                        End If
                                                                        If ctrl_pnl.Name.Substring(Len(ctrl_pnl.Name) - 2, 2) = "_1" Then
                                                                            ctrl_pnl.Focus()
                                                                        End If
                                                                    Next
                                                                End If
                                                            Next
                                                        End If
                                                    End If
                                                Next
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
        '------ end of ini agar combobox diset ke selectedindex  = 0 ketika insert data
    End Function

    Public Overridable Function before_edit() As Boolean
        before_edit = True

        Return before_edit
    End Function

    Public Overrides Function after_edit() As Boolean
        set_button_mdi()
        Return True
    End Function


    Public Overrides Function edit_data() As Boolean

        If _bt_edit = False Then
            MsgBox("Sorry You do not have authorization")
            Return False
            Exit Function
        End If
        edit_data = True
        If ds.Tables.Count = 0 Then
            edit_data = False
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            edit_data = False
            Exit Function
        End If

        xtc_master.SelectedTabPageIndex = 1
        xtp_edit.PageVisible = True
        xtp_data.PageVisible = False

        scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        scc_detail.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1

        sc(True)
        ' ini supaya komponen dengan _1 di focus
        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                For Each ctrl_spc In spc.Controls
                    If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                        sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                        For Each ctrl_sgp In sgp.Controls
                            If TypeOf ctrl_sgp Is DevExpress.XtraEditors.SplitContainerControl Then
                                spcd = CType(ctrl_sgp, DevExpress.XtraEditors.SplitContainerControl)
                                For Each ctrl_spcd In spcd.Controls
                                    If TypeOf ctrl_spcd Is DevExpress.XtraEditors.SplitGroupPanel Then
                                        sgpd = CType(ctrl_spcd, DevExpress.XtraEditors.SplitGroupPanel)
                                        For Each ctrl_sgpd In sgpd.Controls
                                            If TypeOf ctrl_sgpd Is DevExpress.XtraTab.XtraTabControl Then
                                                xtc = CType(ctrl_sgpd, DevExpress.XtraTab.XtraTabControl)
                                                For Each ctrl_xtc In xtc.Controls
                                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                                        If ctrl_xtc.name = "xtp_edit" Then
                                                            xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                                            For Each ctrl_xtp In xtp.Controls
                                                                If TypeOf ctrl_xtp Is Panel Then
                                                                    pnl = CType(ctrl_xtp, Panel)
                                                                    For Each ctrl_pnl In pnl.Controls
                                                                        If ctrl_pnl.Name.Substring(Len(ctrl_pnl.Name) - 2, 2) = "_1" Then
                                                                            ctrl_pnl.Focus()
                                                                        End If
                                                                    Next
                                                                End If
                                                            Next
                                                        End If
                                                    End If
                                                Next
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next

        insert_edit = False
        Return edit_data
    End Function

    Public Overridable Function before_delete() As Boolean
        before_delete = True

        Return before_delete
    End Function

    Public Overrides Function delete_data() As Boolean
        If _bt_delete = False Then
            MsgBox("Sorry You do not have authorization")
            Return False
            Exit Function
        End If
        delete_data = True
        If ds.Tables.Count = 0 Then
            delete_data = False
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            delete_data = False
            Exit Function
        End If

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
            End If

            If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                Try
                    'Dim sp As String = Me.Name.Substring(1, Len(Me.Name) - 1).ToLower + "_delete"
                    Dim sp As String = my_form_name + "_delete"
                    Using objdelete As New WDABasepgsql("", "")
                        With objdelete
                            .SQL = sp
                            .InitializeCommand()
                            param_delete(objdelete)
                            .ExecuteStoredProcedure()
                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End If
        End If

        Return delete_data
    End Function

    Public Overridable Sub param_delete(ByVal obj As Object)
        obj.AddParameter("@" + ds.Tables(0).Columns(0).ToString, ds.Tables(0).Rows(Me.BindingContext(ds.Tables(0)).Position).Item(ds.Tables(0).Columns(0).ToString))
    End Sub

    Public Overrides Function cancel_data() As Boolean
        Dim my As MasterMDI = CType(Me.ParentForm, MasterMDI)
        my.configurasi_menu("awal_transaksi")
        If _bt_delete = False Then
            my.blb_delete.Enabled = False
        End If
        If _bt_insert = False Then
            my.blb_insert.Enabled = False
        End If
        If _bt_edit = False Then
            my.blb_editdata.Enabled = False
        End If

        xtc_master.SelectedTabPageIndex = 0
        xtp_data.PageVisible = True
        xtp_edit.PageVisible = False

        panel_visibility()

        sc(False)
        kosong()
    End Function

    Public Overridable Function before_save() As Boolean
        before_save = True

        Return before_save
    End Function

    Public Overrides Function save_data() As Boolean
        save_data = validasi_save()
        Dim my As MasterMDI = CType(Me.ParentForm, MasterMDI)
        If save_data = True And before_save() = True Then
            If insert_edit = True Then
                save_data = insert()
                If save_data = False Then ' Ini dirubah agar kalau terjadi error tidak langsung kembali ke form awal
                    my.blb_save.Enabled = True
                    Return False
                End If
            Else
                save_data = edit()
                If save_data = False Then ' Ini dirubah agar kalau terjadi error tidak langsung kembali ke form awal
                    my.blb_save.Enabled = True
                    Return False
                End If
            End If

            my.configurasi_menu("awal_transaksi")

            If _bt_delete = False Then
                my.blb_delete.Enabled = False
            End If
            If _bt_insert = False Then
                my.blb_insert.Enabled = False
            End If
            If _bt_edit = False Then
                my.blb_editdata.Enabled = False
            End If
            xtp_edit.PageVisible = False
            xtp_data.PageVisible = True
            panel_visibility()
        Else
            save_data = False
            my.blb_save.Enabled = True
        End If

        Return save_data
    End Function

    Public Overridable Sub set_to_data_insert()

    End Sub

    Public Overridable Function insert() As Boolean
        'Dim sp As String = Me.Name.Substring(1, Len(Me.Name) - 1).ToLower + "_insert"
        Dim sp As String = my_form_name + "_insert"
        Try
            Using objinsert As New WDABasepgsql("", "")
                With objinsert
                    .SQL = sp
                    .InitializeCommand()
                    param_insert(objinsert)
                    .ExecuteStoredProcedure()

                    set_to_data_insert()
                    after_success()
                    insert = True
                End With
            End Using
        Catch ex As Exception
            row = 0
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function

    Public Overridable Function edit()
        'Dim sp As String = Me.Name.Substring(1, Len(Me.Name) - 1).ToLower + "_update"
        Dim sp As String = my_form_name + "_update"
        Try
            Using objedit As New WDABasepgsql("", "")
                With objedit
                    .SQL = sp
                    .InitializeCommand()
                    param_edit(objedit)
                    .ExecuteStoredProcedure()
                    after_success()
                    edit = True
                End With
            End Using
        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
    End Function

    Public Overridable Sub param_insert(ByVal obj As Object)

    End Sub

    Public Overridable Sub param_edit(ByVal obj As Object)

    End Sub

    Public Sub after_success()
        xtc_master.SelectedTabPageIndex = 0
        help_load_data(True)
        sc(False)
        kosong()
        If insert_edit = True Then
            MessageBox.Show("Selamat " + master_new.ClsVar.sNama + ", Data Telah Tersimpan..", "Save Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ElseIf insert_edit = False Then
            MessageBox.Show("Selamat " + master_new.ClsVar.sNama + ", Data Telah TerUpdate..", "Update Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Public Overrides Sub refresh_data()
        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                For Each ctrl_spc In spc.Controls
                    If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                        sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                        For Each ctrl_sgp In sgp.Controls
                            'If ctrl_sgp.name.Substring(0, 6) = "pr_txt" Then
                            '    If TypeOf ctrl_sgp Is TextBox Then
                            '        ctrl_sgp.Text = "%"
                            '    ElseIf TypeOf ctrl_sgp Is DateTimePicker Then
                            '        If ctrl_sgp.Name.ToLower = "pr_txttglawal" Then
                            '            ctrl_sgp.Value = CDate(Month(Today.Date).ToString + "/" + Year(Today.Date).ToString)
                            '        ElseIf ctrl_sgp.Name.ToLower = "pr_txttglakhir" Then
                            '            ctrl_sgp.Value = Today.Date()
                            '        End If
                            '    ElseIf TypeOf ctrl_sgp Is ComboBox Then
                            '        ctrl_sgp.text = ""
                            '    ElseIf TypeOf ctrl_sgp Is NumericUpDown Then
                            '        ctrl_sgp.Value = ctrl_sgp.Minimum
                            '    End If
                            'End If
                            If TypeOf ctrl_sgp Is DevExpress.XtraEditors.SplitContainerControl Then
                                spcd = CType(ctrl_sgp, DevExpress.XtraEditors.SplitContainerControl)
                                For Each ctrl_spcd In spcd.Controls
                                    If TypeOf ctrl_spcd Is DevExpress.XtraEditors.SplitGroupPanel Then
                                        sgpd = CType(ctrl_spcd, DevExpress.XtraEditors.SplitGroupPanel)
                                        For Each ctrl_sgpd In sgpd.Controls
                                            If TypeOf ctrl_sgpd Is DevExpress.XtraTab.XtraTabControl Then
                                                xtc = CType(ctrl_sgpd, DevExpress.XtraTab.XtraTabControl)
                                                For Each ctrl_xtc In xtc.Controls
                                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                                        If ctrl_xtc.name = "xtp_data" Then
                                                            xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                                            For Each ctrl_xtp In xtp.Controls
                                                                If TypeOf ctrl_xtp Is DevExpress.XtraGrid.GridControl Then
                                                                    gc = CType(ctrl_xtp, DevExpress.XtraGrid.GridControl)
                                                                    gv = gc.MainView
                                                                    gv.ActiveFilter.Clear()
                                                                End If
                                                            Next
                                                        End If
                                                    End If
                                                Next
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
        help_load_data(True)
    End Sub

    Public Overridable Sub kosong()
        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                For Each ctrl_spc In spc.Controls
                    If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                        sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                        For Each ctrl_sgp In sgp.Controls
                            If TypeOf ctrl_sgp Is DevExpress.XtraEditors.SplitContainerControl Then
                                spcd = CType(ctrl_sgp, DevExpress.XtraEditors.SplitContainerControl)
                                For Each ctrl_spcd In spcd.Controls
                                    If TypeOf ctrl_spcd Is DevExpress.XtraEditors.SplitGroupPanel Then
                                        sgpd = CType(ctrl_spcd, DevExpress.XtraEditors.SplitGroupPanel)
                                        For Each ctrl_sgpd In sgpd.Controls
                                            If TypeOf ctrl_sgpd Is DevExpress.XtraTab.XtraTabControl Then
                                                xtc = CType(ctrl_sgpd, DevExpress.XtraTab.XtraTabControl)
                                                For Each ctrl_xtc In xtc.Controls
                                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                                        If ctrl_xtc.name = "xtp_edit" Then
                                                            xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                                            For Each ctrl_xtp In xtp.Controls
                                                                If TypeOf ctrl_xtp Is Panel Then
                                                                    pnl = CType(ctrl_xtp, Panel)
                                                                    For Each ctrl_pnl In pnl.Controls
                                                                        If ctrl_pnl.Name.Substring(0, 6) = "sc_txt" Then
                                                                            ctrl_pnl.Text = ""
                                                                        ElseIf ctrl_pnl.Name.Substring(0, 5) = "sc_cb" Then
                                                                            If TypeOf ctrl_pnl Is ComboBox Then
                                                                                ctrl_pnl.Selectedindex = 0
                                                                            ElseIf TypeOf ctrl_pnl Is DevExpress.XtraEditors.LookUpEdit Then
                                                                                ctrl_pnl.itemindex = 0
                                                                            End If
                                                                        ElseIf ctrl_pnl.Name.Substring(0, 6) = "sc_nud" Then
                                                                            ctrl_pnl.Value = ctrl_pnl.properties.minvalue
                                                                            'nud = CType(ctrl_xtp, NumericUpDown)
                                                                            'nud.Value = nud.Minimum
                                                                        ElseIf ctrl_pnl.Name.Substring(0, 5) = "sc_ck" Then
                                                                            ctrl_pnl.Checked = False
                                                                            'ck = CType(ctrl_xtp, CheckBox)
                                                                            'ck.Checked = False
                                                                        End If
                                                                    Next
                                                                End If
                                                            Next
                                                        End If
                                                    End If
                                                Next
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    Public Overridable Sub sc(ByVal bolakbalik As Boolean)
        ' ini supaya control text box menjadi enable dan berubah warna
        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                For Each ctrl_spc In spc.Controls
                    If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                        sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                        For Each ctrl_sgp In sgp.Controls
                            If TypeOf ctrl_sgp Is DevExpress.XtraEditors.SplitContainerControl Then
                                spcd = CType(ctrl_sgp, DevExpress.XtraEditors.SplitContainerControl)
                                For Each ctrl_spcd In spcd.Controls
                                    If TypeOf ctrl_spcd Is DevExpress.XtraEditors.SplitGroupPanel Then
                                        sgpd = CType(ctrl_spcd, DevExpress.XtraEditors.SplitGroupPanel)
                                        For Each ctrl_sgpd In sgpd.Controls
                                            If TypeOf ctrl_sgpd Is DevExpress.XtraTab.XtraTabControl Then
                                                xtc = CType(ctrl_sgpd, DevExpress.XtraTab.XtraTabControl)
                                                For Each ctrl_xtc In xtc.Controls
                                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                                        If ctrl_xtc.name = "xtp_data" Then
                                                            xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                                            For Each ctrl_xtp In xtp.Controls
                                                                If TypeOf ctrl_xtp Is DevExpress.XtraGrid.GridControl Then
                                                                    gc = CType(ctrl_xtp, DevExpress.XtraGrid.GridControl)
                                                                    gc.Enabled = Not bolakbalik
                                                                End If
                                                            Next
                                                        ElseIf ctrl_xtc.name = "xtp_edit" Then
                                                            xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                                            For Each ctrl_xtp In xtp.Controls
                                                                If TypeOf ctrl_xtp Is Panel Then
                                                                    pnl = CType(ctrl_xtp, Panel)
                                                                    For Each ctrl_pnl In pnl.Controls
                                                                        If ctrl_pnl.Name.Substring(0, 2) = "sc" Then
                                                                            If bolakbalik = True Then
                                                                                ctrl_pnl.BackColor = System.Drawing.Color.White
                                                                            Else
                                                                                ctrl_pnl.BackColor = System.Drawing.Color.Silver
                                                                            End If
                                                                            ctrl_pnl.Enabled = bolakbalik
                                                                        End If
                                                                    Next
                                                                End If
                                                            Next
                                                        End If
                                                    End If
                                                Next
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    Public Overridable Function validasi_save()
        validasi_save = True
        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                For Each ctrl_spc In spc.Controls
                    If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                        sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                        For Each ctrl_sgp In sgp.Controls
                            If TypeOf ctrl_sgp Is DevExpress.XtraEditors.SplitContainerControl Then
                                spcd = CType(ctrl_sgp, DevExpress.XtraEditors.SplitContainerControl)
                                For Each ctrl_spcd In spcd.Controls
                                    If TypeOf ctrl_spcd Is DevExpress.XtraEditors.SplitGroupPanel Then
                                        sgpd = CType(ctrl_spcd, DevExpress.XtraEditors.SplitGroupPanel)
                                        For Each ctrl_sgpd In sgpd.Controls
                                            If TypeOf ctrl_sgpd Is DevExpress.XtraTab.XtraTabControl Then
                                                xtc = CType(ctrl_sgpd, DevExpress.XtraTab.XtraTabControl)
                                                For Each ctrl_xtc In xtc.Controls
                                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                                        If ctrl_xtc.name = "xtp_edit" Then
                                                            xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                                            For Each ctrl_xtp In xtp.Controls
                                                                If TypeOf ctrl_xtp Is Panel Then
                                                                    pnl = CType(ctrl_xtp, Panel)
                                                                    For Each ctrl_pnl In pnl.Controls
                                                                        If ctrl_pnl.Name.Substring(Len(ctrl_pnl.Name) - 3, 3) <> "_em" And ctrl_pnl.Name.Substring(0, 2) = "sc" Then
                                                                            If ctrl_pnl.Name.Substring(0, 6) = "sc_txt" Then
                                                                                If Trim(ctrl_pnl.Text) = "" Then
                                                                                    MessageBox.Show("Data " + ctrl_pnl.Name.Substring(6, Len(ctrl_pnl.Name) - 6) + " Can't Empty..", "Error")
                                                                                    ctrl_pnl.Focus()
                                                                                    validasi_save = False
                                                                                    Exit For
                                                                                End If
                                                                            ElseIf ctrl_pnl.Name.Substring(0, 5) = "sc_cb" Then
                                                                                If TypeOf ctrl_pnl Is ComboBox Then
                                                                                    If ctrl_pnl.SelectedIndex = 0 Then
                                                                                        MessageBox.Show("Data " + ctrl_pnl.Name.Substring(5, Len(ctrl_pnl.Name) - 5) + " Can't Empty..", "Error")
                                                                                        ctrl_pnl.Focus()
                                                                                        validasi_save = False
                                                                                        Exit For
                                                                                    End If
                                                                                ElseIf TypeOf ctrl_pnl Is DevExpress.XtraEditors.LookUpEdit Then
                                                                                End If
                                                                            End If
                                                                        End If
                                                                    Next
                                                                End If
                                                            Next
                                                        End If
                                                    End If
                                                Next
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
        Return validasi_save
    End Function

    Public Sub add_handler_numeric()
        Dim ctrl_xtp As System.Windows.Forms.Control
        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                For Each ctrl_spc In spc.Controls
                    If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                        sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                        For Each ctrl_sgp In sgp.Controls
                            If TypeOf ctrl_sgp Is DevExpress.XtraEditors.SplitContainerControl Then
                                spcd = CType(ctrl_sgp, DevExpress.XtraEditors.SplitContainerControl)
                                For Each ctrl_spcd In spcd.Controls
                                    If TypeOf ctrl_spcd Is DevExpress.XtraEditors.SplitGroupPanel Then
                                        sgpd = CType(ctrl_spcd, DevExpress.XtraEditors.SplitGroupPanel)
                                        For Each ctrl_sgpd In sgpd.Controls
                                            If TypeOf ctrl_sgpd Is DevExpress.XtraTab.XtraTabControl Then
                                                xtc = CType(ctrl_sgpd, DevExpress.XtraTab.XtraTabControl)
                                                For Each ctrl_xtc In xtc.Controls
                                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                                        If ctrl_xtc.name = "xtp_edit" Then
                                                            xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                                            For Each ctrl_xtp In xtp.Controls
                                                                If TypeOf ctrl_xtp Is Panel Then
                                                                    pnl = CType(ctrl_xtp, Panel)
                                                                    For Each ctrl_pnl In pnl.Controls
                                                                        If ctrl_pnl.Name.Substring(Len(ctrl_pnl.Name) - 3, 3) = "_nr" Or _
                                                                            ctrl_pnl.Name.Substring(Len(ctrl_pnl.Name) - 6, 3) = "_nr" Then
                                                                            ow = CType(ctrl_pnl, TextBox)
                                                                            AddHandler ow.KeyPress, AddressOf NumericValuesOnly
                                                                        End If
                                                                    Next
                                                                End If
                                                            Next
                                                        End If
                                                    End If
                                                Next
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

#Region "style_grid"
    Public Overrides Sub style_grid(ByVal gv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal par_style As String)
        gv.OptionsView.EnableAppearanceEvenRow = True
    End Sub
#End Region

    Private Function get_gc() As Object
        get_gc = DBNull.Value
        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                For Each ctrl_spc In spc.Controls
                    If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                        sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                        For Each ctrl_sgp In sgp.Controls
                            If TypeOf ctrl_sgp Is DevExpress.XtraEditors.SplitContainerControl Then
                                spcd = CType(ctrl_sgp, DevExpress.XtraEditors.SplitContainerControl)
                                For Each ctrl_spcd In spcd.Controls
                                    If TypeOf ctrl_spcd Is DevExpress.XtraEditors.SplitGroupPanel Then
                                        sgpd = CType(ctrl_spcd, DevExpress.XtraEditors.SplitGroupPanel)
                                        For Each ctrl_sgpd In sgpd.Controls
                                            If TypeOf ctrl_sgpd Is DevExpress.XtraTab.XtraTabControl Then
                                                xtc = CType(ctrl_sgpd, DevExpress.XtraTab.XtraTabControl)
                                                For Each ctrl_xtc In xtc.Controls
                                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                                        If ctrl_xtc.name = "xtp_data" Then
                                                            xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                                            For Each ctrl_xtp In xtp.Controls
                                                                If TypeOf ctrl_xtp Is DevExpress.XtraGrid.GridControl Then
                                                                    get_gc = CType(ctrl_xtp, DevExpress.XtraGrid.GridControl)
                                                                End If
                                                            Next
                                                        End If
                                                    End If
                                                Next
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
        Return get_gc
    End Function

    Public Overrides Function get_gv() As Object
        get_gv = DBNull.Value
        gv = get_gc().MainView
        get_gv = gv
        Return get_gv
    End Function

    Public Overrides Sub style_grid_detail()
        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                For Each ctrl_spc In spc.Controls
                    If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                        sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                        For Each ctrl_sgp In sgp.Controls
                            If TypeOf ctrl_sgp Is DevExpress.XtraEditors.SplitContainerControl Then
                                spcd = CType(ctrl_sgp, DevExpress.XtraEditors.SplitContainerControl)
                                For Each ctrl_spcd In spcd.Controls
                                    If TypeOf ctrl_spcd Is DevExpress.XtraEditors.SplitGroupPanel Then
                                        sgpd = CType(ctrl_spcd, DevExpress.XtraEditors.SplitGroupPanel)
                                        For Each ctrl_sgpd In sgpd.Controls
                                            If TypeOf ctrl_sgpd Is DevExpress.XtraTab.XtraTabControl Then
                                                xtc = CType(ctrl_sgpd, DevExpress.XtraTab.XtraTabControl)
                                                For Each ctrl_xtc In xtc.Controls
                                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                                        xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                                        For Each ctrl_xtp In xtp.Controls
                                                            If TypeOf ctrl_xtp Is DevExpress.XtraEditors.SplitContainerControl Then
                                                                spcd2 = CType(ctrl_xtp, DevExpress.XtraEditors.SplitContainerControl)
                                                                For Each ctrl_spcd2 In spcd2.Controls
                                                                    If TypeOf ctrl_spcd2 Is DevExpress.XtraEditors.SplitGroupPanel Then
                                                                        sgpd2 = CType(ctrl_spcd2, DevExpress.XtraEditors.SplitGroupPanel)
                                                                        For Each ctrl_sgpd2 In sgpd2.Controls
                                                                            If TypeOf ctrl_sgpd2 Is DevExpress.XtraGrid.GridControl Then
                                                                                Dim gc_detail As New DevExpress.XtraGrid.GridControl
                                                                                gc_detail = CType(ctrl_sgpd2, DevExpress.XtraGrid.GridControl)
                                                                                gc_detail.UseEmbeddedNavigator = True
                                                                                format_embeddednavigator(gc_detail)

                                                                                Dim gv_detail As New DevExpress.XtraGrid.Views.Grid.GridView
                                                                                gv_detail = gc_detail.MainView
                                                                                gv_detail.OptionsView.ColumnAutoWidth = False
                                                                                gv_detail.OptionsView.ShowAutoFilterRow = False
                                                                                gv_detail.OptionsView.ShowFooter = False
                                                                                gv_detail.OptionsView.ShowGroupPanel = False
                                                                                gv_detail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Default
                                                                                gv_detail.OptionsView.ShowGroupedColumns = True
                                                                                gv_detail.OptionsView.EnableAppearanceEvenRow = True

                                                                                style_grid(gv_detail, master_new.ClsVar.sGridStyle)
                                                                            End If
                                                                        Next
                                                                    End If
                                                                Next
                                                            End If
                                                        Next
                                                    End If
                                                Next
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    Public Overrides Sub set_component()
        Dim sb As DevExpress.XtraEditors.SimpleButton

        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                For Each ctrl_spc In spc.Controls
                    If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                        sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                        For Each ctrl_sgp In sgp.Controls
                            If TypeOf ctrl_sgp Is DevExpress.XtraEditors.SplitContainerControl Then
                                spcd = CType(ctrl_sgp, DevExpress.XtraEditors.SplitContainerControl)
                                For Each ctrl_spcd In spcd.Controls
                                    If TypeOf ctrl_spcd Is DevExpress.XtraEditors.SplitGroupPanel Then
                                        sgpd = CType(ctrl_spcd, DevExpress.XtraEditors.SplitGroupPanel)
                                        For Each ctrl_sgpd In sgpd.Controls
                                            If TypeOf ctrl_sgpd Is DevExpress.XtraTab.XtraTabControl Then
                                                xtc = CType(ctrl_sgpd, DevExpress.XtraTab.XtraTabControl)
                                                For Each ctrl_xtc In xtc.Controls
                                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                                        If ctrl_xtc.name = "xtp_edit" Then
                                                            xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                                            For Each ctrl_xtp In xtp.Controls
                                                                If TypeOf ctrl_xtp Is Panel Then
                                                                    pnl = CType(ctrl_xtp, Panel)
                                                                    For Each ctrl_pnl In pnl.Controls
                                                                        If ctrl_pnl.name.substring(0, 3) = "sc_" Then
                                                                            Try
                                                                                ctrl_pnl.enabled = False
                                                                                ctrl_pnl.borderstyle = "Office2003"
                                                                                ctrl_pnl.properties.borderstyle = "Office2003"
                                                                                ctrl_pnl.movenextcontrol = True
                                                                            Catch ex As Exception
                                                                            End Try
                                                                        End If
                                                                        If TypeOf ctrl_pnl Is DevExpress.XtraEditors.SimpleButton Then
                                                                            sb = CType(ctrl_pnl, DevExpress.XtraEditors.SimpleButton)
                                                                            sb.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
                                                                        End If
                                                                    Next
                                                                End If
                                                            Next
                                                        End If
                                                    End If
                                                Next
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub
End Class
