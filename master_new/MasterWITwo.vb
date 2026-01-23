Imports npgsql
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports DevExpress.XtraLayout

Public Class MasterWITwo
    Public insert_edit As Boolean
    Dim my_form_name As String
    Dim mf As New ModFunction
    Dim _bt_delete As Boolean = True
    Dim _bt_insert As Boolean = True
    Dim _bt_edit As Boolean = True
    Public _dt_lang As New DataTable



    Public Sub set_lang(ByVal par_obj As Object)
        Try
            For Each ctrl In par_obj.Controls
                set_lang_obj(ctrl)
            Next
        Catch ex As Exception
        End Try

        Try
            loop_group(par_obj)
        Catch ex3 As Exception
        End Try
    End Sub

    Private Sub loop_group(ByVal par_group As DevExpress.XtraLayout.LayoutControlGroup)

        For i As Integer = 0 To par_group.Count - 1
            If TypeOf par_group(i) Is TabbedControlGroup Then
                loop_tab(par_group(i))
            End If
            If TypeOf par_group(i) Is LayoutControlItem Then
                set_lang_obj(par_group(i))
            End If

        Next

    End Sub
    Private Sub loop_tab(ByVal par_tab As TabbedControlGroup)
        For i As Integer = 0 To par_tab.TabPages.GroupCount - 1
            set_lang_obj(par_tab.TabPages(i))
        Next
    End Sub

    Public Sub set_lang(ByVal par_grid As DevExpress.XtraGrid.Views.Grid.GridView)
        For i As Integer = 0 To par_grid.Columns.Count - 1
            set_lang(par_grid, par_grid.Columns(i).Name)
        Next
        par_grid.BestFitColumns()
    End Sub

    Private Sub set_lang_obj(ByVal par_obj As Object)
        Dim _data As String = ""
        _data = f_lang(_dt_lang, par_obj.Name, master_new.ClsVar.par_lang)
        If _data <> "" Then
            par_obj.Text = _data
        End If
    End Sub
    Private Sub set_lang(ByVal par_grid As Object, ByVal par_name As String)
        Dim _data As String = ""
        _data = f_lang(_dt_lang, par_grid.name & "." & par_name, master_new.ClsVar.par_lang)
        If _data <> "" Then
            par_grid.Columns(par_name).Caption = _data
        End If
    End Sub

    Private Function f_lang(ByVal par_dt As DataTable, ByVal par_comp_name As String, ByVal par_lang As String) As String
        Try
            Dim _hasil As String = ""
            For Each dr As DataRow In par_dt.Rows
                If dr("lang_comp_name") = par_comp_name Then
                    If par_lang = "id" Then
                        _hasil = dr("lang_id")
                    ElseIf par_lang = "en" Then
                        _hasil = dr("lang_en")
                    End If
                    Exit For
                End If
            Next

            Return _hasil
        Catch ex As Exception
            MsgBox(ex.Message)
            Return ""
        End Try
    End Function

    Public Overridable Sub set_window(ByVal arg As MasterMDI)
        fmm = arg
    End Sub

    Public Overrides Sub panel_visibility()
        scc_master.PanelVisibility = spv_master
    End Sub

    Public Overridable Sub set_button_mdi()
        'If type_form = True Then
        '    my_form_name = Me.Name
        '    my_form_name = my_form_name.Substring(1, my_form_name.Length - 1)
        'Else
        '    Try
        '        Using objcb As New master_new.WDABasepgsql("", "")
        '            With objcb
        '                .Connection.Open()
        '                .Command = .Connection.CreateCommand
        '                .Command.CommandType = CommandType.Text
        '                .Command.CommandText = "select form_name_asal from tconfmycustom " + _
        '                                       " where form_name_baru = '" + Me.Name + "'"
        '                .InitializeCommand()
        '                .DataReader = .Command.ExecuteReader

        '                While .DataReader.Read
        '                    my_form_name = .DataReader.Item("form_name_asal").Substring(1, Len(.DataReader.Item("form_name_asal")) - 1)
        '                End While
        '            End With
        '        End Using
        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message)
        '    End Try
        'End If

        If xtc_master.SelectedTabPageIndex = 0 Then
            my_form_name = Me.Name.Substring(1, Me.Name.Length - 1)

            Dim conf_menu As String = ""

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
                            'If .DataReader("editablestatus") = True Then
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
                            '_bt_delete = .DataReader("deleteablestatus")
                            '_bt_insert = .DataReader("insertablestatus")
                            '_bt_edit = .DataReader("editablestatus")
                            'End If


                            'If .DataReader("deleteablestatus") = False Then
                            '    _bt_delete = False
                            'ElseIf .DataReader("deleteablestatus") = True Then
                            '    _bt_delete = False
                            'End If
                            'If .DataReader("insertablestatus") = False Then
                            '    _bt_insert = False
                            'End If
                            'If .DataReader("editablestatus") = False Then
                            '    _bt_edit = False
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

    Public Overrides Function after_edit() As Boolean
        set_button_mdi()
        Return True
    End Function

    Public Overridable Sub MasterWITwo_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        set_button_mdi()
    End Sub

    Public Overrides Sub form_first_load()
        'create_table()
        help_load_data(False)
        load_cb()
        on_load()
        format_grid()
        add_handler_numeric()
        load_Columns()

        spv_master = scc_master.PanelVisibility
        xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False
        xtp_edit.PageVisible = False


        'gv = gc.MainView

        '========= setting grid style
        'fmm.bsi_gridstyle.Caption = "Grid : " + master_new.ClsVar.sGridStyle
        ' style_grid(gv, master_new.ClsVar.sGridStyle)
        'gv.BestFitColumns()
        'style_grid_detail()
        style_grid(get_gv(), master_new.ClsVar.sGridStyle)
        style_grid_detail()
        '============================
        gc = get_gc()

        gc.UseEmbeddedNavigator = True
        format_embeddednavigator(gc)

        gv.OptionsView.ColumnAutoWidth = False
        gv.OptionsView.ShowAutoFilterRow = True
        gv.OptionsView.ShowFooter = True
        gv.OptionsView.ShowGroupedColumns = True
        gv.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "groupsummary")
        gv.OptionsBehavior.AllowIncrementalSearch = True
        gv.OptionsSelection.MultiSelect = True

        AddHandler gv.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv.ColumnFilterChanged, AddressOf relation_detail
        '================================================================================================

        '================================================================================================
        'load layout
        Dim nama_file, path As String
        path = "c:\syspro\layout\"
        nama_file = Me.Name + "_" + master_new.ClsVar.sUserID.ToString + ".xml"
        Dim status_file As String = DevExpress.Utils.FilesHelper.FindingFileName(path, nama_file, False)
        If status_file <> "" Then
            Dim layout As DevExpress.XtraLayout.LayoutControl
            layout = get_layout()
            layout.RestoreLayoutFromXml(path + nama_file)
        End If
        '================================================================================================

        change_lang()
    End Sub

    Public Overrides Sub help_load_data(ByVal par As Boolean)
        load_data(par, get_gc())
    End Sub

    Public Overrides Sub load_data(ByVal arg As Boolean, ByVal gc As DevExpress.XtraGrid.GridControl)
        Try
            row = BindingContext(ds.Tables(0)).Position
        Catch ex As Exception
            row = 0
        End Try

        Cursor = Cursors.WaitCursor
        If arg <> False Then
            set_param_textbox()
            '================================================================
            Try
                'row = BindingContext(ds.Tables(0)).Position
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload

                        .SQL = get_sequel()
                        .InitializeCommand()
                        .FillDataSet(ds, Me.Name + "_select")
                        gc.DataSource = ds.Tables(0)
                        BindingContext(ds.Tables(0)).Position = row
                        bestfit_column()
                        load_data_grid_detail()
                        ConditionsAdjustment()
                    End With

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
                    'gc.DataSource = Nothing 'ini agar kalau di refresh dan data master kosong maka detail juga kosong
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
        scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        xtp_edit.PageVisible = True
        xtp_data.PageVisible = False

        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraBars.Docking.DockPanel Then
                dp = CType(ctrl, DevExpress.XtraBars.Docking.DockPanel)
                dp.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
            End If
        Next

        sc(True)

        'biar selalu terisi data
        Dim my As MasterMDI = CType(Me.ParentForm, MasterMDI)
        my.configurasi_menu("transaksi")

        ''------ ini agar combobox diset ke selectedindex  = 0 dan focus ke komponen awal ketika insert data       
        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                For Each ctrl_spc In spc.Controls
                    If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                        sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                        For Each ctrl_sgp In sgp.Controls
                            If TypeOf ctrl_sgp Is DevExpress.XtraTab.XtraTabControl Then
                                xtc = CType(ctrl_sgp, DevExpress.XtraTab.XtraTabControl)
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
        '------ end of ini agar combobox diset ke selectedindex  = 0 ketika insert data

        insert_data_awal()
        'penambahan untuk bug pada edit value is null
        refresh_data()

        insert_data_awal_2()

    End Function

    Public Overridable Sub insert_data_awal_2()

    End Sub
    Public Overridable Sub insert_data_awal()

    End Sub


    Public Overridable Function before_edit() As Boolean
        before_edit = True

        Return before_edit
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

        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraBars.Docking.DockPanel Then
                dp = CType(ctrl, DevExpress.XtraBars.Docking.DockPanel)
                dp.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
            End If
        Next

        scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2

        sc(True)
        ' ini supaya komponen dengan _1 di focus
        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                For Each ctrl_spc In spc.Controls
                    If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                        sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                        For Each ctrl_sgp In sgp.Controls
                            If TypeOf ctrl_sgp Is DevExpress.XtraTab.XtraTabControl Then
                                xtc = CType(ctrl_sgp, DevExpress.XtraTab.XtraTabControl)
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
                    Dim sp As String = sp_delete()
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
        panel_visibility()
        xtc_master.SelectedTabPageIndex = 0
        xtp_edit.PageVisible = False
        xtp_data.PageVisible = True

        For Each ctrl In Me.Controls
            'MessageBox.Show(ctrl.name)
            If TypeOf ctrl Is DevExpress.XtraBars.Docking.DockPanel Then
                dp = CType(ctrl, DevExpress.XtraBars.Docking.DockPanel)
                'dp.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                dp.Show()
            End If
        Next
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

            panel_visibility()
            xtp_edit.PageVisible = False
            xtp_data.PageVisible = True
            For Each ctrl In Me.Controls
                If TypeOf ctrl Is DevExpress.XtraBars.Docking.DockPanel Then
                    dp = CType(ctrl, DevExpress.XtraBars.Docking.DockPanel)
                    'dp.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                    dp.Show()
                End If
            Next
        Else
            save_data = False
            my.blb_save.Enabled = True
        End If

        Return save_data
    End Function

    Public Overridable Sub set_to_data_insert()

    End Sub

    Public Overridable Function sp_insert() As String
        sp_insert = my_form_name + "_insert"
        Return sp_insert
    End Function

    Public Overridable Function sp_update() As String
        sp_update = my_form_name + "_update"
        Return sp_update
    End Function

    Public Overridable Function sp_delete() As String
        sp_delete = my_form_name + "_delete"
        Return sp_delete
    End Function

    Public Overridable Function insert() As Boolean
        'Dim sp As String = Me.Name.Substring(1, Len(Me.Name) - 1).ToLower + "_insert"
        Dim sp As String = sp_insert()
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
        Dim sp As String = sp_update()
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

    Public Overridable Sub after_success()
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
        'For Each ctrl In Me.Controls
        '    If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
        '        spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
        '        For Each ctrl_spc In spc.Controls
        '            If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
        '                sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
        '                'For Each ctrl_sgp In sgp.Controls
        '                'If ctrl_sgp.name.Substring(0, 6) = "pr_txt" Then
        '                '    If TypeOf ctrl_sgp Is TextBox Then
        '                '        ctrl_sgp.Text = "%"
        '                '    ElseIf TypeOf ctrl_sgp Is DateTimePicker Then
        '                '        If ctrl_sgp.Name.ToLower = "pr_txttglawal" Then
        '                '            ctrl_sgp.Value = CDate(Month(Today.Date).ToString + "/" + Year(Today.Date).ToString)
        '                '        ElseIf ctrl_sgp.Name.ToLower = "pr_txttglakhir" Then
        '                '            ctrl_sgp.Value = Today.Date()
        '                '        End If
        '                '    ElseIf TypeOf ctrl_sgp Is ComboBox Then
        '                '        ctrl_sgp.text = ""
        '                '    ElseIf TypeOf ctrl_sgp Is NumericUpDown Then
        '                '        ctrl_sgp.Value = ctrl_sgp.Minimum
        '                '    End If
        '                'End If
        '                'Next
        '            End If
        '        Next
        '    End If
        'Next

        'gv = get_gc().mainview()
        'gv.ActiveFilter.Clear()

        'help_load_data(True)

        load_cb()
        load_cb_en()
    End Sub

    Public Overridable Sub load_cb_en()

    End Sub

    Public Overridable Sub kosong()
        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                For Each ctrl_spc In spc.Controls
                    If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                        sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                        For Each ctrl_sgp In sgp.Controls
                            If TypeOf ctrl_sgp Is DevExpress.XtraTab.XtraTabControl Then
                                xtc = CType(ctrl_sgp, DevExpress.XtraTab.XtraTabControl)
                                For Each ctrl_xtc In xtc.Controls
                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                        If ctrl_xtc.name = "xtp_edit" Then
                                            xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                            For Each ctrl_xtp In xtp.Controls
                                                If TypeOf ctrl_xtp Is Panel Then
                                                    pnl = CType(ctrl_xtp, Panel)
                                                    For Each ctrl_pnl In pnl.Controls
                                                        If ctrl_pnl.Name.Substring(0, 6) = "sc_txt" Then
                                                            If TypeOf ctrl_pnl Is TextBox Then
                                                                ctrl_pnl.Text = ""
                                                            ElseIf TypeOf ctrl_pnl Is DevExpress.XtraEditors.TextEdit Then
                                                                ctrl_pnl.Text = ""
                                                            End If
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
                            Try
                                ctrl_sgp.borderstyle = "Office2003"
                                ctrl_sgp.properties.borderstyle = "Office2003"
                            Catch ex As Exception
                            End Try
                            If TypeOf ctrl_sgp Is DevExpress.XtraTab.XtraTabControl Then
                                xtc = CType(ctrl_sgp, DevExpress.XtraTab.XtraTabControl)
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
                            If TypeOf ctrl_sgp Is DevExpress.XtraTab.XtraTabControl Then
                                xtc = CType(ctrl_sgp, DevExpress.XtraTab.XtraTabControl)
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
                            If TypeOf ctrl_sgp Is DevExpress.XtraTab.XtraTabControl Then
                                xtc = CType(ctrl_sgp, DevExpress.XtraTab.XtraTabControl)
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
                            If TypeOf ctrl_sgp Is DevExpress.XtraTab.XtraTabControl Then
                                xtc = CType(ctrl_sgp, DevExpress.XtraTab.XtraTabControl)
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
                                                            If TypeOf ctrl_pnl Is TextBox Then
                                                                ow = CType(ctrl_pnl, TextBox)
                                                                AddHandler ow.KeyPress, AddressOf NumericValuesOnly
                                                            ElseIf TypeOf ctrl_pnl Is DevExpress.XtraEditors.TextEdit Then
                                                                ow = CType(ctrl_pnl, DevExpress.XtraEditors.TextEdit)
                                                                AddHandler ow.KeyPress, AddressOf NumericValuesOnly
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
    End Sub

#Region "style_grid"
    Public Overrides Sub style_grid(ByVal gv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal par_style As String)
        gv.OptionsView.EnableAppearanceEvenRow = True
    End Sub
#End Region


    Public Function get_gc() As Object
        get_gc = DBNull.Value
        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                For Each ctrl_spc In spc.Controls
                    If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                        sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                        For Each ctrl_sgp In sgp.Controls
                            If TypeOf ctrl_sgp Is DevExpress.XtraTab.XtraTabControl Then
                                xtc = CType(ctrl_sgp, DevExpress.XtraTab.XtraTabControl)
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
            If TypeOf ctrl Is DevExpress.XtraBars.Docking.DockPanel Then
                dp = CType(ctrl, DevExpress.XtraBars.Docking.DockPanel)
                For Each ctrl_dp In dp.Controls
                    If TypeOf ctrl_dp Is DevExpress.XtraBars.Docking.ControlContainer Then
                        cc = CType(ctrl_dp, DevExpress.XtraBars.Docking.ControlContainer)
                        For Each ctrl_cc In cc.Controls
                            If TypeOf ctrl_cc Is DevExpress.XtraTab.XtraTabControl Then
                                xtc = CType(ctrl_cc, DevExpress.XtraTab.XtraTabControl)
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
                                                                gv_detail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
                                                                gv_detail.OptionsView.ShowGroupedColumns = True
                                                                gv_detail.OptionsSelection.MultiSelect = True
                                                                gv_detail.OptionsView.EnableAppearanceEvenRow = True

                                                                style_grid(gv_detail, master_new.ClsVar.sGridStyle)
                                                            End If
                                                        Next
                                                    End If
                                                Next
                                            ElseIf TypeOf ctrl_xtp Is DevExpress.XtraTab.XtraTabControl Then
                                                xtcd = CType(ctrl_xtp, DevExpress.XtraTab.XtraTabControl)
                                                For Each ctrl_xtcd In xtcd.Controls
                                                    If TypeOf ctrl_xtcd Is DevExpress.XtraTab.XtraTabPage Then
                                                        xtpd = CType(ctrl_xtcd, DevExpress.XtraTab.XtraTabPage)
                                                        For Each ctrl_xtpd In xtpd.Controls
                                                            If TypeOf ctrl_xtpd Is DevExpress.XtraEditors.SplitContainerControl Then
                                                                spcd2 = CType(ctrl_xtpd, DevExpress.XtraEditors.SplitContainerControl)
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
                                                                                gv_detail.OptionsSelection.MultiSelect = True
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

    Public Overrides Sub set_default()
        If MessageBox.Show("Set As Default For This Configuration Form..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If
        cek_folder()

        save_setting_columns()

        Dim layout As DevExpress.XtraLayout.LayoutControl
        layout = get_layout()
        layout.SaveLayoutToXml("c:\syspro\layout\" + Me.Name + "_" + master_new.ClsVar.sUserID.ToString + ".xml")
        MessageBox.Show("Configuration Have Been Saved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Function get_layout() As Object
        get_layout = DBNull.Value
        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                For Each ctrl_spc In spc.Controls
                    If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                        sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                        For Each ctrl_sgp In sgp.Controls
                            If TypeOf ctrl_sgp Is DevExpress.XtraTab.XtraTabControl Then
                                xtc = CType(ctrl_sgp, DevExpress.XtraTab.XtraTabControl)
                                For Each ctrl_xtc In xtc.Controls
                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                        If ctrl_xtc.name = "xtp_edit" Then
                                            xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                            For Each ctrl_xtp In xtp.Controls
                                                If TypeOf ctrl_xtp Is Panel Then
                                                    pnl = CType(ctrl_xtp, Panel)
                                                    For Each ctrl_pnl In pnl.Controls
                                                        If TypeOf ctrl_pnl Is DevExpress.XtraLayout.LayoutControl Then
                                                            Dim layout_master As DevExpress.XtraLayout.LayoutControl
                                                            layout_master = CType(ctrl_pnl, DevExpress.XtraLayout.LayoutControl)
                                                            get_layout = layout_master
                                                            Exit For
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

        Return get_layout
    End Function

    Public Overrides Sub reset()
        If MessageBox.Show("Yakin Untuk Mereset Setingan Ini..", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim FileToDelete As String
        gv = get_gv()
        FileToDelete = "c:\syspro\layout\" + Me.Name + "_" + gv.Name + "_" + master_new.ClsVar.sUserID.ToString + ".xml"

        If System.IO.File.Exists(FileToDelete) = True Then
            System.IO.File.Delete(FileToDelete)
        End If

        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraBars.Docking.DockPanel Then
                dp = CType(ctrl, DevExpress.XtraBars.Docking.DockPanel)
                For Each ctrl_dp In dp.Controls
                    If TypeOf ctrl_dp Is DevExpress.XtraBars.Docking.ControlContainer Then
                        cc = CType(ctrl_dp, DevExpress.XtraBars.Docking.ControlContainer)
                        For Each ctrl_cc In cc.Controls
                            If TypeOf ctrl_cc Is DevExpress.XtraTab.XtraTabControl Then
                                xtc = CType(ctrl_cc, DevExpress.XtraTab.XtraTabControl)
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
                                                                Dim gv_detail As New DevExpress.XtraGrid.Views.Grid.GridView
                                                                gv_detail = gc_detail.MainView
                                                                FileToDelete = "c:\syspro\layout\" + Me.Name + "_" + gv_detail.Name + "_" + master_new.ClsVar.sUserID.ToString + ".xml"

                                                                If System.IO.File.Exists(FileToDelete) = True Then
                                                                    System.IO.File.Delete(FileToDelete)
                                                                End If
                                                            End If
                                                        Next
                                                    End If
                                                Next
                                            ElseIf TypeOf ctrl_xtp Is DevExpress.XtraTab.XtraTabControl Then
                                                xtcd = CType(ctrl_xtp, DevExpress.XtraTab.XtraTabControl)
                                                For Each ctrl_xtcd In xtcd.Controls
                                                    If TypeOf ctrl_xtcd Is DevExpress.XtraTab.XtraTabPage Then
                                                        xtpd = CType(ctrl_xtcd, DevExpress.XtraTab.XtraTabPage)
                                                        For Each ctrl_xtpd In xtpd.Controls
                                                            If TypeOf ctrl_xtpd Is DevExpress.XtraEditors.SplitContainerControl Then
                                                                spcd2 = CType(ctrl_xtpd, DevExpress.XtraEditors.SplitContainerControl)
                                                                For Each ctrl_spcd2 In spcd2.Controls
                                                                    If TypeOf ctrl_spcd2 Is DevExpress.XtraEditors.SplitGroupPanel Then
                                                                        sgpd2 = CType(ctrl_spcd2, DevExpress.XtraEditors.SplitGroupPanel)
                                                                        For Each ctrl_sgpd2 In sgpd2.Controls
                                                                            If TypeOf ctrl_sgpd2 Is DevExpress.XtraGrid.GridControl Then
                                                                                Dim gc_detail As New DevExpress.XtraGrid.GridControl
                                                                                gc_detail = CType(ctrl_sgpd2, DevExpress.XtraGrid.GridControl)
                                                                                Dim gv_detail As New DevExpress.XtraGrid.Views.Grid.GridView
                                                                                gv_detail = gc_detail.MainView
                                                                                FileToDelete = "c:\syspro\layout\" + Me.Name + "_" + gv_detail.Name + "_" + master_new.ClsVar.sUserID.ToString + ".xml"
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

        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                For Each ctrl_spc In spc.Controls
                    If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                        sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                        For Each ctrl_sgp In sgp.Controls
                            If TypeOf ctrl_sgp Is DevExpress.XtraTab.XtraTabControl Then
                                xtc = CType(ctrl_sgp, DevExpress.XtraTab.XtraTabControl)
                                For Each ctrl_xtc In xtc.Controls
                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                        xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                        For Each ctrl_xtp In xtp.Controls
                                            If TypeOf ctrl_xtp Is DevExpress.XtraGrid.GridControl Then
                                                gc = CType(ctrl_xtp, DevExpress.XtraGrid.GridControl)
                                                gv = gc.MainView
                                                FileToDelete = "c:\syspro\layout\" + Me.Name + "_" + gv.Name + "_" + master_new.ClsVar.sUserID.ToString + ".xml"
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

        FileToDelete = "c:\syspro\layout\" + Me.Name + "_" + master_new.ClsVar.sUserID.ToString + ".xml"

        If System.IO.File.Exists(FileToDelete) = True Then
            System.IO.File.Delete(FileToDelete)
        End If

        MessageBox.Show("Data Have Been Reseted..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Public Overridable Sub set_row(ByVal par_code As String, ByVal par_column As String)
        Dim i As Integer
        For i = 0 To ds.Tables(0).Rows.Count - 1
            If par_code = ds.Tables(0).Rows(i).Item(par_column).ToString Then
                BindingContext(ds.Tables(0)).Position = i
                Exit Sub
            End If
        Next
    End Sub

    Public Overridable Sub approve(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String, ByVal par_code As String, _
                                   ByVal par_initial As String, ByVal par_type As String, ByVal par_gv As Object, ByVal par_title As String)
        'par_initial contohnya pby
        'par_type contohnya dr


        Dim _trn_status, user_wf, user_wf_email, filename, format_email_bantu, _pby_code As String

        If mf.get_transaction_status(par_colom, par_table, par_criteria, par_code) <> "D" Then
            MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If MessageBox.Show("Approve This Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        _pby_code = par_code
        _trn_status = "W"
        user_wf = mf.get_user_wf(par_code, 0)
        user_wf_email = mf.get_email_address(user_wf)

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update " + par_table + " set " + par_colom + " = '" + _trn_status + "'," + _
                                               " " + par_initial + "_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " " + par_initial + "_upd_date = current_timestamp " + _
                                               " where " + par_initial + "_oid = '" + par_oid + "'"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                               " where wf_ref_code ~~* '" + par_code + "'" + _
                                               " and wf_seq = 0"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'Karena bisa saja datanya di rollback sehingga pada saat approve lagi...datanya harus dikosongkan lagi....
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update wf_mstr set wf_wfs_id = 0, wf_date_to = null, wf_desc = '', wf_aprv_user = '', wf_aprv_date = null " + _
                                               " where wf_ref_code ~~* '" + par_code + "'"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()

                        format_email_bantu = mf.format_email(user_wf, par_code, par_type)

                        filename = "c:\syspro\" + par_code + ".xls"
                        ExportTo(par_gv, New ExportXlsProvider(filename))

                        If user_wf_email <> "" Then
                            mf.sent_email(user_wf_email, "", mf.title_email(par_title, par_code), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
                        Else
                            MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If

                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        help_load_data(True)
                        set_row(Trim(par_oid), par_initial + "_oid")
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Public Overridable Sub cancel_approve(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String, ByVal par_code As String, _
                                   ByVal par_initial As String, ByVal par_type As String)

        Dim _trans_id As String = ""

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select " + par_colom + " as trans_id from " + par_table + " where " + par_criteria + " = '" + par_code + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _trans_id = .DataReader("trans_id").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If _trans_id = "D" Then
            MessageBox.Show("Can't Cancel For Draft Data..", "Conf", MessageBoxButtons.OK)
            Exit Sub
        Else
            If MessageBox.Show("Cancel This Data..", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
        End If

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'X'" + _
                                               " where " + par_criteria + " = '" + par_code + "'"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update wf_mstr set wf_iscurrent = 'N'" + _
                                               " where wf_ref_code = '" + par_code + "'"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Updated..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        row = BindingContext(ds.Tables(0)).Position
                        help_load_data(True)
                        set_row(Trim(par_oid), par_initial + "_oid")
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overridable Sub reminder_by_mail(ByVal par_code As String, ByVal par_type As String, ByVal par_user As String, ByVal par_gv As Object, ByVal par_title As String)
        Dim filename, format_email_bantu, user_wf_email As String

        If MessageBox.Show("Reminder Current User By Mail..?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If
        user_wf_email = mf.get_email_address(par_user)
        filename = "c:\syspro\" + par_code + ".xls"
        format_email_bantu = mf.format_email(par_user, par_code, par_type)
        ExportTo(par_gv, New ExportXlsProvider(filename))

        If user_wf_email <> "" Then
            If mf.sent_email(user_wf_email, "", mf.title_email(par_title, par_code), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename) = False Then
                Exit Sub
            End If
        Else
            MessageBox.Show("Email Address Not Available For User " + par_user + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Reminder By Mail..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub xtc_master_SelectedPageChanged(ByVal sender As Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles xtc_master.SelectedPageChanged
        cek_page()
    End Sub
    Public Overridable Sub cek_page()

    End Sub

    Public Function get_status_wf(ByVal par_pd_code As String) As String
        get_status_wf = "-1"
        Try
            Using objkalendar As New master_new.WDABasepgsql("", "")
                With objkalendar
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select wf_wfs_id from wf_mstr  " + _
                                           " where wf_ref_code = '" + par_pd_code + "'" + _
                                           " and wf_seq = 0"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_status_wf = .DataReader.Item("wf_wfs_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return get_status_wf
    End Function
End Class
