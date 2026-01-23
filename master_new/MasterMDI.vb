Imports npgsql
Imports DevExpress.XtraBars
Imports DevExpress.Skins
Imports master_new.ModFunction

Public Class MasterMDI

    Public frm As FPass
    Public fps As FPrintScreen

    Dim skinMask As String = "Skin: "

    Private Sub MasterMDI_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As NpgsqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.StoredProcedure
                        .Command.CommandText = "userac_accs_update"
                        .AddParameter("@userac_oid", master_new.ClsVar._oid)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                    Catch ex As NpgsqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Public Overridable Sub setbbi()

    End Sub
   
    Public Overridable Sub Load_Form()

        Try
            Ips_Init()
            InitSkins()
            skin_user()
            InitDockPanel()
            InitMenu()
            load_mycustom()
            load_myfavorite()
            setbbi()

            iText.Caption = "Active User : " & master_new.ClsVar.sNama & ", " & "Connect To : " _
            & konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\pgserver.txt"), "server") _
            & ", Database : " & konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\pgserver.txt"), "db") & ", Port : " & konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\pgserver.txt"), "port") & ", Sync : " & _
            PGSqlConn.status_sync.ToString

            reminder_check()
            timer_reminder.Interval = (master_new.ClsVar.sTimeInterval) * 3600000
            timer_reminder.Enabled = True

            load_server()
        Catch ex As Exception
        End Try
    End Sub
    Public Overridable Sub load_server()

    End Sub
    Private Sub MasterMDI_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
         Load_Form()
    End Sub

    Private Sub timer_reminder_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer_reminder.Tick
        reminder_check()
    End Sub

    Public Overridable Sub LoadTimeInterval()

    End Sub

    Public Overridable Sub reminder_check()
        
    End Sub

    Public Overridable Sub load_mycustom()

    End Sub

    Public Overridable Sub load_myfavorite()

    End Sub

    Public Overridable Sub InitMenu()

    End Sub

#Region "Form Skin"

    Public Overridable Sub InitDockPanel()

    End Sub

    Public Overridable Sub skin_user()
        If master_new.ClsVar.sFormSkin = "Default" Or master_new.ClsVar.sFormSkin = "WindowsXP" Or _
           master_new.ClsVar.sFormSkin = "OfficeXP" Or master_new.ClsVar.sFormSkin = "Office2000" Or master_new.ClsVar.sFormSkin = "Office2003" Then
            BarManager1.GetController().PaintStyleName = master_new.ClsVar.sFormSkin
            BarManager1.GetController().ResetStyleDefaults()
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetDefaultStyle()
        Else
            Dim skinName As String = master_new.ClsVar.sFormSkin
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName)
            BarManager1.GetController().PaintStyleName = "Skin"
        End If

        iPaintStyle.Caption = "Form : " + master_new.ClsVar.sFormSkin
        iPaintStyle.Hint = master_new.ClsVar.sFormSkin

        bsi_style_mainmenu.Caption = "Main Menu Style : " + master_new.ClsVar.SMainMenuStyle
        sytle_mainmenu(master_new.ClsVar.SMainMenuStyle)

        bsi_gridstyle.Caption = "Grid : " + master_new.ClsVar.sGridStyle
    End Sub

    Sub InitSkins()
        BarManager1.ForceInitialize()
        iPaintStyle.ImageIndex = 0 : iPaintStyle.ImageIndex = -1
        If BarManager1.GetController().PaintStyleName = "Skin" Then
            iPaintStyle.Caption = "Form : " + skinMask + DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName
            iPaintStyle.Hint = iPaintStyle.Caption
        End If
        
        Dim cnt As DevExpress.Skins.SkinContainer
        For Each cnt In DevExpress.Skins.SkinManager.Default.Skins
            Dim item As BarButtonItem = New BarButtonItem(BarManager1, skinMask + cnt.SkinName)
            iPaintStyle.AddItem(item)
            AddHandler item.ItemClick, New ItemClickEventHandler(AddressOf OnSkinClick)
        Next
    End Sub

    Sub OnSkinClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
        Dim skinName As String = e.Item.Caption.Replace(skinMask, "")
        DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName)
        BarManager1.GetController().PaintStyleName = "Skin"
        iPaintStyle.Caption = "Form : " + e.Item.Caption
        iPaintStyle.Hint = iPaintStyle.Caption
        iPaintStyle.ImageIndex = -1

        Try
            Using objcb As New WDABasepgsql("", "")
                With objcb
                    .SQL = "update tconfskin set form_skin = '" + skinName + "'" _
                          + " where userid = " + ClsVar.sUserID.ToString
                    .InitializeCommand()
                    .ExecuteStoredProcedure()
                    ClsVar.sFormSkin = skinName
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub InitPaintStyle(ByVal item As BarItem)
        If item Is Nothing Then Return
        'iPaintStyle.ImageIndex = item.ImageIndex
        iPaintStyle.Caption = "Form : " + item.Caption
        iPaintStyle.Hint = item.Description
    End Sub

    Private Sub Ips_Init()
        Dim item As BarItem = Nothing
        Dim i As Integer
        For i = 0 To BarManager1.Items.Count - 1
            If BarManager1.Items(i).Description = BarManager1.GetController().PaintStyleName Then
                item = BarManager1.Items(i)
            End If
        Next
        InitPaintStyle(item)
    End Sub

    Private Sub ips_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_default.ItemClick, bbi_windowsxp.ItemClick, bbi_officexp.ItemClick, bbi_office2000.ItemClick, bbi_office2003.ItemClick
        BarManager1.GetController().PaintStyleName = e.Item.Description
        InitPaintStyle(e.Item)
        BarManager1.GetController().ResetStyleDefaults()
        DevExpress.LookAndFeel.UserLookAndFeel.Default.SetDefaultStyle()
        Try
            Using objcb As New WDABasepgsql("", "")
                With objcb
                    .SQL = "update tconfskin set form_skin = '" + e.Item.Description + "'" _
                          + " where userid = " + ClsVar.sUserID.ToString
                    .InitializeCommand()
                    .ExecuteStoredProcedure()
                    ClsVar.sFormSkin = e.Item.Description
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


#End Region

    Public Overridable Sub set_window(ByVal arg As FPass)
        frm = arg
    End Sub

    Public Overridable Sub set_window_print_screen(ByVal arg As FPrintScreen)
        fps = arg
    End Sub

    Private Sub bbi_turnoff_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_turnoff.ItemClick
        'Global.System.Windows.Forms.Application.Exit()
        'Me.Visible = False
        Try
            master_new.ClsVar.CExit = True
            Me.Close()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub bbi_close_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_close.ItemClick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            activeChild.Close()
        End If
    End Sub

    Private Sub bbi_print_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_print.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.preview()
    End Sub

    Private Sub blb_print_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles blb_print.ItemClick

        Dim my As Master = CType(ActiveMdiChild, Master)
        my.preview()
    End Sub

    Private Sub bbi_export_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_export.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.export_data()
    End Sub

    Private Sub blb_export_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles blb_export.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.export_data()
    End Sub

    Private Sub bbi_lock_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_lock.ItemClick
        Dim fop As New FOpacity
        fop.Show()
        Dim frm As New FLock
        frm.set_window(fop)
        frm.ShowDialog()
    End Sub

    Private Sub blb_lock_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles blb_lock.ItemClick
        Dim fop As New FOpacity
        fop.Show()
        Dim frm As New FLock
        frm.set_window(fop)

        frm.ShowDialog()
    End Sub

    Private Sub blb_logoff_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles blb_logoff.ItemClick
        Try
            master_new.ClsVar.CExit = False
            frm.Show()
            master_new.ClsVar.sPass = True
            fps.Close()
            'With frmLogin
            '    .Show()
            '    .BringToFront()
            'End With
            Me.Close()
        Catch ex As Exception
            Pesan(Err)
        End Try

        'frm.Show()
        'master_new.ClsVar.sPass = True
        'fps.Close()
        'Me.Close()
    End Sub

    Private Sub bbi_logoff_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_logoff.ItemClick
        frm.Show()
        master_new.ClsVar.sPass = True
        fps.Close()
        Me.Close()
    End Sub

    Private Sub bbi_insert_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_insert.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.insert_data()
    End Sub

    Private Sub blb_insert_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles blb_insert.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.insert_data()
    End Sub

    Private Sub bbi_edit_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_edit.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        If my.edit_data() = True Then
            configurasi_menu("transaksi")
            my.after_edit()
        Else
            configurasi_menu("awal_transaksi")
            my.after_edit()
        End If
    End Sub

    Private Sub blb_editdata_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles blb_editdata.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        If my.edit_data() = True Then
            configurasi_menu("transaksi")
            my.after_edit()
        Else
            configurasi_menu("awal_transaksi")
            my.after_edit()
        End If
    End Sub

    Private Sub bbi_save_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_save.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.save_data()
    End Sub
    Public Overridable Sub execute_teamviewer()

    End Sub


    Private Sub blb_save_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles blb_save.ItemClick

        blb_save.Enabled = False
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.save_data()


    End Sub

    Private Sub blb_delete_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles blb_delete.ItemClick
        Try
            blb_delete.Enabled = False
            Dim my As Master = CType(ActiveMdiChild, Master)
            my.delete_data()
        Catch ex As Exception
        Finally
            blb_delete.Enabled = True
        End Try
       
    End Sub

    Private Sub bbi_delete_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_delete.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.delete_data()
    End Sub

    Private Sub bbi_cancel_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_cancel.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.cancel_data()
    End Sub

    Private Sub blb_cancel_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles blb_cancel.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.cancel_data()
    End Sub

    Private Sub bbi_retrieve_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_retrieve.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.find()
    End Sub

    Public Sub blb_retrieve_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles blb_retrieve.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.find()

    End Sub

    Private Sub blb_refresh_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles blb_refresh.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.refresh_data()
    End Sub

    Private Sub bbi_refresh_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_refresh.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.refresh_data()
    End Sub

    Private Sub bbi_email_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_email.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.email_data()
    End Sub

    Private Sub blb_email_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles blb_email.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.email_data()
    End Sub

    Private Sub bbi_reset_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_reset.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.reset()
    End Sub

    Private Sub blb_set_default_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles blb_set_default.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.set_default()
    End Sub

    Private Sub blb_reset_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles blb_reset.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.reset()
    End Sub

    Private Sub bbi_favorite_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_favorite.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.favorite()
    End Sub

    Private Sub blb_favorite_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles blb_favorite.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.favorite()
    End Sub

    Private Sub bbi_set_default_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_set_default.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.set_default()
    End Sub

    Private Sub bbi_custom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_mycustom.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.save_as()
    End Sub

    Private Sub blb_mycustom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles blb_saveas.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.save_as()
    End Sub

    Private Sub bbi_approve_line_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_approve_line.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.approve_line()
    End Sub

    Private Sub bbi_cancel_line_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_cancel_line.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.cancel_line()
    End Sub

    Private Sub blb_approve_line_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles blb_approve_line.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.approve_line()
    End Sub

    Private Sub blb_cancel_line_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles blb_cancel_line.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.cancel_line()
    End Sub

    Private Sub blb_smart_approve_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles blb_smart_approve.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.smart_approve()
    End Sub

    Private Sub blb_reminder_mail_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles blb_reminder_mail.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.reminder_mail()
    End Sub

    Private Sub bbi_smart_approve_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_smart_approve.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.smart_approve()
    End Sub

    Private Sub bbi_reminder_mail_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_reminder_mail.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.reminder_mail()
    End Sub

    Private Sub bbi_closeallform_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_closeallform.ItemClick
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private Sub bbi_freezepanes_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_freezepanes.ItemClick
        Dim my As Master = CType(ActiveMdiChild, Master)
        my.freeze()
    End Sub

    Public Overridable Sub configurasi_menu(ByVal menu As String)
        If menu = "nothing" Then
            blb_set_default.Enabled = False
            blb_reset.Enabled = False
            blb_favorite.Enabled = False
            blb_saveas.Enabled = False
            blb_email.Enabled = False
            blb_export.Enabled = False
            blb_print.Enabled = False
            blb_insert.Enabled = False
            blb_editdata.Enabled = False
            blb_delete.Enabled = False
            blb_cancel.Enabled = False
            blb_save.Enabled = False
            blb_retrieve.Enabled = False
            blb_refresh.Enabled = False
            blb_approve_line.Enabled = False
            blb_cancel_line.Enabled = False
            blb_smart_approve.Enabled = False
            blb_reminder_mail.Enabled = False

            bbi_set_default.Enabled = False
            bbi_close.Enabled = False
            bbi_reset.Enabled = False
            bbi_favorite.Enabled = False
            bbi_mycustom.Enabled = False
            bbi_email.Enabled = False
            bbi_export.Enabled = False
            bbi_print.Enabled = False
            bbi_insert.Enabled = False
            bbi_edit.Enabled = False
            bbi_delete.Enabled = False
            bbi_cancel.Enabled = False
            bbi_save.Enabled = False
            bbi_retrieve.Enabled = False
            bbi_refresh.Enabled = False
            bbi_closeallform.Enabled = False
            bbi_freezepanes.Enabled = False
            bbi_approve_line.Enabled = False
            bbi_cancel_line.Enabled = False
            bbi_smart_approve.Enabled = False
            bbi_reminder_mail.Enabled = False
        ElseIf menu = "awal_transaksi" Then
            blb_set_default.Enabled = True
            blb_reset.Enabled = True
            blb_favorite.Enabled = True
            blb_saveas.Enabled = True
            blb_email.Enabled = True
            blb_export.Enabled = True
            blb_print.Enabled = True
            blb_insert.Enabled = True
            blb_editdata.Enabled = True
            blb_delete.Enabled = True
            blb_cancel.Enabled = False
            blb_save.Enabled = False
            blb_retrieve.Enabled = True
            blb_refresh.Enabled = True
            blb_approve_line.Enabled = True
            blb_cancel_line.Enabled = True
            blb_smart_approve.Enabled = True
            blb_reminder_mail.Enabled = True

            bbi_close.Enabled = True
            bbi_set_default.Enabled = True
            bbi_reset.Enabled = True
            bbi_favorite.Enabled = True
            bbi_mycustom.Enabled = True
            bbi_email.Enabled = True
            bbi_export.Enabled = True
            bbi_print.Enabled = True
            bbi_insert.Enabled = True
            bbi_edit.Enabled = True
            bbi_delete.Enabled = True
            bbi_cancel.Enabled = False
            bbi_save.Enabled = False
            bbi_retrieve.Enabled = True
            bbi_refresh.Enabled = True
            bbi_closeallform.Enabled = True
            bbi_freezepanes.Enabled = True
            bbi_approve_line.Enabled = True
            bbi_cancel_line.Enabled = True
            bbi_smart_approve.Enabled = True
            bbi_reminder_mail.Enabled = True
        ElseIf menu = "transaksi" Then
            blb_set_default.Enabled = True
            blb_reset.Enabled = True
            blb_favorite.Enabled = False
            blb_saveas.Enabled = False
            blb_email.Enabled = False
            blb_export.Enabled = False
            blb_print.Enabled = False
            blb_insert.Enabled = False
            blb_editdata.Enabled = False
            blb_delete.Enabled = False
            blb_cancel.Enabled = True
            blb_save.Enabled = True
            blb_retrieve.Enabled = False
            blb_refresh.Enabled = True
            blb_approve_line.Enabled = False
            blb_cancel_line.Enabled = False
            blb_smart_approve.Enabled = False
            blb_reminder_mail.Enabled = False

            bbi_close.Enabled = False
            bbi_set_default.Enabled = True
            bbi_reset.Enabled = True
            bbi_favorite.Enabled = False
            bbi_mycustom.Enabled = False
            bbi_email.Enabled = False
            bbi_export.Enabled = False
            bbi_print.Enabled = False
            bbi_insert.Enabled = False
            bbi_edit.Enabled = False
            bbi_delete.Enabled = False
            bbi_cancel.Enabled = True
            bbi_save.Enabled = True
            bbi_retrieve.Enabled = False
            bbi_refresh.Enabled = True
            bbi_closeallform.Enabled = False
            bbi_freezepanes.Enabled = False
            bbi_approve_line.Enabled = False
            bbi_cancel_line.Enabled = False
            bbi_smart_approve.Enabled = False
            bbi_reminder_mail.Enabled = False
        ElseIf menu = "informasi" Then
            blb_set_default.Enabled = True
            blb_reset.Enabled = True
            blb_favorite.Enabled = True
            blb_saveas.Enabled = True
            blb_email.Enabled = True
            blb_export.Enabled = True
            blb_print.Enabled = True
            'agar mode insertable bs di terapkan
            blb_insert.Enabled = True
            '  blb_insert.Enabled = False
            blb_editdata.Enabled = False
            'ini juga
            blb_delete.Enabled = True
            blb_cancel.Enabled = False
            blb_save.Enabled = False
            blb_retrieve.Enabled = True
            blb_refresh.Enabled = True
            blb_approve_line.Enabled = False
            blb_cancel_line.Enabled = False
            blb_smart_approve.Enabled = False
            blb_reminder_mail.Enabled = False

            bbi_close.Enabled = True
            bbi_set_default.Enabled = True
            bbi_reset.Enabled = True
            bbi_favorite.Enabled = True
            bbi_mycustom.Enabled = True
            bbi_email.Enabled = True
            bbi_export.Enabled = True
            bbi_print.Enabled = True
            bbi_insert.Enabled = False
            bbi_edit.Enabled = False
            bbi_delete.Enabled = False
            bbi_cancel.Enabled = False
            bbi_save.Enabled = False
            bbi_retrieve.Enabled = True
            bbi_refresh.Enabled = True
            bbi_closeallform.Enabled = True
            bbi_freezepanes.Enabled = True
            bbi_approve_line.Enabled = False
            bbi_cancel_line.Enabled = False
            bbi_smart_approve.Enabled = False
            bbi_reminder_mail.Enabled = False
        End If
    End Sub

    Private Sub MasterMDI_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'If master_new.ClsVar.sPass = False Then
        '    Global.System.Windows.Forms.Application.Exit()
        'End If
    End Sub

    Public Overridable Sub sytle_mainmenu(ByVal par_caption As String)

    End Sub

#Region "Main Menu Skin"
    Private Sub update_style_main_menu(ByVal par_skin As String)
        Try
            Using objcb As New WDABasepgsql("", "")
                With objcb
                    .SQL = "update tconfskin set mainmenu_style = '" + par_skin + "'" _
                          + " where userid = " + ClsVar.sUserID.ToString
                    .InitializeCommand()
                    .ExecuteStoredProcedure()
                    ClsVar.sFormSkin = par_skin
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub bbi_baseview_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_baseview.ItemClick
        bsi_style_mainmenu.Caption = "Main Menu : " + bbi_baseview.Caption
        master_new.ClsVar.SMainMenuStyle = bbi_baseview.Caption
        update_style_main_menu(bbi_baseview.Caption)
        sytle_mainmenu(bbi_baseview.Caption)
    End Sub

    Private Sub bbi_flatview_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_flatview.ItemClick
        bsi_style_mainmenu.Caption = "Main Menu : " + bbi_flatview.Caption
        master_new.ClsVar.SMainMenuStyle = bbi_flatview.Caption
        update_style_main_menu(bbi_flatview.Caption)
        sytle_mainmenu(bbi_flatview.Caption)
    End Sub

    Private Sub bbi_office1view_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_office1view.ItemClick
        bsi_style_mainmenu.Caption = "Main Menu : " + bbi_office1view.Caption
        master_new.ClsVar.SMainMenuStyle = bbi_office1view.Caption
        update_style_main_menu(bbi_office1view.Caption)
        sytle_mainmenu(bbi_office1view.Caption)
    End Sub

    Private Sub bbi_office2view_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_office2view.ItemClick
        bsi_style_mainmenu.Caption = "Main Menu : " + bbi_office2view.Caption
        master_new.ClsVar.SMainMenuStyle = bbi_office2view.Caption
        update_style_main_menu(bbi_office2view.Caption)
        sytle_mainmenu(bbi_office2view.Caption)
    End Sub

    Private Sub bbi_office3view_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_office3view.ItemClick
        bsi_style_mainmenu.Caption = "Main Menu : " + bbi_office3view.Caption
        master_new.ClsVar.SMainMenuStyle = bbi_office3view.Caption
        update_style_main_menu(bbi_office3view.Caption)
        sytle_mainmenu(bbi_office3view.Caption)
    End Sub

    Private Sub bbi_vstoolboxview_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_vstoolboxview.ItemClick
        bsi_style_mainmenu.Caption = "Main Menu : " + bbi_vstoolboxview.Caption
        master_new.ClsVar.SMainMenuStyle = bbi_vstoolboxview.Caption
        update_style_main_menu(bbi_vstoolboxview.Caption)
        sytle_mainmenu(bbi_vstoolboxview.Caption)
    End Sub

    Private Sub bbi_advexplorerBarView_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_advexplorerBarView.ItemClick
        bsi_style_mainmenu.Caption = "Main Menu : " + bbi_advexplorerBarView.Caption
        master_new.ClsVar.SMainMenuStyle = bbi_advexplorerBarView.Caption
        update_style_main_menu(bbi_advexplorerBarView.Caption)
        sytle_mainmenu(bbi_advexplorerBarView.Caption)
    End Sub

    Private Sub bbi_explorerbarview_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_explorerbarview.ItemClick
        bsi_style_mainmenu.Caption = "Main Menu : " + bbi_explorerbarview.Caption
        master_new.ClsVar.SMainMenuStyle = bbi_explorerbarview.Caption
        update_style_main_menu(bbi_explorerbarview.Caption)
        sytle_mainmenu(bbi_explorerbarview.Caption)
    End Sub

    Private Sub bbi_UltraFlatExplorerBarView_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_UltraFlatExplorerBarView.ItemClick
        bsi_style_mainmenu.Caption = "Main Menu : " + bbi_UltraFlatExplorerBarView.Caption
        master_new.ClsVar.SMainMenuStyle = bbi_UltraFlatExplorerBarView.Caption
        update_style_main_menu(bbi_UltraFlatExplorerBarView.Caption)
        sytle_mainmenu(bbi_UltraFlatExplorerBarView.Caption)
    End Sub

    Private Sub bbi_SkinExplorerBarView_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_SkinExplorerBarView.ItemClick
        bsi_style_mainmenu.Caption = "Main Menu : " + bbi_SkinExplorerBarView.Caption
        master_new.ClsVar.SMainMenuStyle = bbi_SkinExplorerBarView.Caption
        update_style_main_menu(bbi_SkinExplorerBarView.Caption)
        sytle_mainmenu(bbi_SkinExplorerBarView.Caption)
    End Sub

    Private Sub bbi_xp1view_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_xp1view.ItemClick
        bsi_style_mainmenu.Caption = "Main Menu : " + bbi_xp1view.Caption
        master_new.ClsVar.SMainMenuStyle = bbi_xp1view.Caption
        update_style_main_menu(bbi_xp1view.Caption)
        sytle_mainmenu(bbi_xp1view.Caption)
    End Sub

    Private Sub bbi_XP2View_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_XP2View.ItemClick
        bsi_style_mainmenu.Caption = "Main Menu : " + bbi_XP2View.Caption
        master_new.ClsVar.SMainMenuStyle = bbi_XP2View.Caption
        update_style_main_menu(bbi_XP2View.Caption)
        sytle_mainmenu(bbi_XP2View.Caption)
    End Sub

    Private Sub bbi_XPExplorerBarView_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_XPExplorerBarView.ItemClick
        bsi_style_mainmenu.Caption = "Main Menu : " + bbi_XPExplorerBarView.Caption
        master_new.ClsVar.SMainMenuStyle = bbi_XPExplorerBarView.Caption
        update_style_main_menu(bbi_XPExplorerBarView.Caption)
        sytle_mainmenu(bbi_XPExplorerBarView.Caption)
    End Sub

    Private Sub bbi_NavigationPane_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_NavigationPane.ItemClick
        bsi_style_mainmenu.Caption = "Main Menu : " + bbi_NavigationPane.Caption
        master_new.ClsVar.SMainMenuStyle = bbi_NavigationPane.Caption
        update_style_main_menu(bbi_NavigationPane.Caption)
        sytle_mainmenu(bbi_NavigationPane.Caption)
    End Sub

    Private Sub bbi_SkinNavigationPane_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_SkinNavigationPane.ItemClick
        bsi_style_mainmenu.Caption = "Main Menu : " + bbi_SkinNavigationPane.Caption
        master_new.ClsVar.SMainMenuStyle = bbi_SkinNavigationPane.Caption
        update_style_main_menu(bbi_SkinNavigationPane.Caption)
        sytle_mainmenu(bbi_SkinNavigationPane.Caption)
    End Sub

    Private Sub bbi_SkinCaramel_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_SkinCaramel.ItemClick
        bsi_style_mainmenu.Caption = "Main Menu : " + bbi_SkinCaramel.Caption
        master_new.ClsVar.SMainMenuStyle = bbi_SkinCaramel.Caption
        update_style_main_menu(bbi_SkinCaramel.Caption)
        sytle_mainmenu(bbi_SkinCaramel.Caption)
    End Sub

    Private Sub bbi_SkinTheAsphaltWorld_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_SkinTheAsphaltWorld.ItemClick
        bsi_style_mainmenu.Caption = "Main Menu : " + bbi_SkinTheAsphaltWorld.Caption
        master_new.ClsVar.SMainMenuStyle = bbi_SkinTheAsphaltWorld.Caption
        update_style_main_menu(bbi_SkinTheAsphaltWorld.Caption)
        sytle_mainmenu(bbi_SkinTheAsphaltWorld.Caption)
    End Sub

    Private Sub bbi_SkinLiquidSky_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_SkinLiquidSky.ItemClick
        bsi_style_mainmenu.Caption = "Main Menu : " + bbi_SkinLiquidSky.Caption
        master_new.ClsVar.SMainMenuStyle = bbi_SkinLiquidSky.Caption
        update_style_main_menu(bbi_SkinLiquidSky.Caption)
        sytle_mainmenu(bbi_SkinLiquidSky.Caption)
    End Sub

    Private Sub bbi_SkinCoffee_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_SkinCoffee.ItemClick
        bsi_style_mainmenu.Caption = "Main Menu : " + bbi_SkinCoffee.Caption
        master_new.ClsVar.SMainMenuStyle = bbi_SkinCoffee.Caption
        update_style_main_menu(bbi_SkinCoffee.Caption)
        sytle_mainmenu(bbi_SkinCoffee.Caption)
    End Sub

    Private Sub bbi_SkinStardust_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_SkinStardust.ItemClick
        bsi_style_mainmenu.Caption = "Main Menu : " + bbi_SkinStardust.Caption
        master_new.ClsVar.SMainMenuStyle = bbi_SkinStardust.Caption
        update_style_main_menu(bbi_SkinStardust.Caption)
        sytle_mainmenu(bbi_SkinStardust.Caption)
    End Sub

    Private Sub bbi_SkinGlassOceans_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_SkinGlassOceans.ItemClick
        bsi_style_mainmenu.Caption = "Main Menu : " + bbi_SkinGlassOceans.Caption
        master_new.ClsVar.SMainMenuStyle = bbi_SkinGlassOceans.Caption
        update_style_main_menu(bbi_SkinGlassOceans.Caption)
        sytle_mainmenu(bbi_SkinGlassOceans.Caption)
    End Sub

    Private Sub bbi_SkinMoneyTwins_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_SkinMoneyTwins.ItemClick
        bsi_style_mainmenu.Caption = "Main Menu : " + bbi_SkinMoneyTwins.Caption
        master_new.ClsVar.SMainMenuStyle = bbi_SkinMoneyTwins.Caption
        update_style_main_menu(bbi_SkinMoneyTwins.Caption)
        sytle_mainmenu(bbi_SkinMoneyTwins.Caption)
    End Sub

    Private Sub bbi_SkinNavCaramel_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_SkinNavCaramel.ItemClick
        bsi_style_mainmenu.Caption = "Main Menu : " + bbi_SkinNavCaramel.Caption
        master_new.ClsVar.SMainMenuStyle = bbi_SkinNavCaramel.Caption
        update_style_main_menu(bbi_SkinNavCaramel.Caption)
        sytle_mainmenu(bbi_SkinNavCaramel.Caption)
    End Sub

    Private Sub bbi_SkinNavTheAsphaltWorld_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_SkinNavTheAsphaltWorld.ItemClick
        bsi_style_mainmenu.Caption = "Main Menu : " + bbi_SkinNavTheAsphaltWorld.Caption
        master_new.ClsVar.SMainMenuStyle = bbi_SkinNavTheAsphaltWorld.Caption
        update_style_main_menu(bbi_SkinNavTheAsphaltWorld.Caption)
        sytle_mainmenu(bbi_SkinNavTheAsphaltWorld.Caption)
    End Sub
#End Region

#Region "style grid"
    Private Sub update_style_grid(ByVal par_style As String)
        Try
            Using objcb As New WDABasepgsql("", "")
                With objcb
                    .SQL = "update tconfskin set grid_style = '" + par_style + "'" _
                          + " where userid = " + ClsVar.sUserID.ToString
                    .InitializeCommand()
                    .ExecuteStoredProcedure()
                    ClsVar.sGridStyle = par_style
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub bbi_defaultgrid_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_defaultgrid.ItemClick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            Dim my As Master = CType(ActiveMdiChild, Master)
            bsi_gridstyle.Caption = "Grid : " + bbi_defaultgrid.Caption
            update_style_grid("default")

            Try
                my.style_grid(my.get_gv(), "default")
            Catch ex As Exception
            End Try

            my.style_grid_many()
            my.style_grid_detail()
        End If
    End Sub

    Private Sub bbi_Pastel1_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Pastel1.ItemClick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            Dim my As Master = CType(ActiveMdiChild, Master)
            bsi_gridstyle.Caption = "Grid : " + bbi_Pastel1.Caption
            update_style_grid("Pastel#1")

            Try
                my.style_grid(my.get_gv(), "Pastel#1")
            Catch ex As Exception
            End Try

            my.style_grid_many()
            my.style_grid_detail()
        End If
    End Sub

    Private Sub bbi_Pastel2_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Pastel2.ItemClick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            Dim my As Master = CType(ActiveMdiChild, Master)
            bsi_gridstyle.Caption = "Grid : " + bbi_Pastel2.Caption
            update_style_grid("Pastel#2")

            Try
                my.style_grid(my.get_gv(), "Pastel#2")
            Catch ex As Exception
            End Try

            my.style_grid_many()
            my.style_grid_detail()
        End If
    End Sub

    Private Sub bbi_Pastel3_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Pastel3.ItemClick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            Dim my As Master = CType(ActiveMdiChild, Master)
            bsi_gridstyle.Caption = "Grid : " + bbi_Pastel3.Caption
            update_style_grid("Pastel#3")

            Try
                my.style_grid(my.get_gv(), "Pastel#3")
            Catch ex As Exception
            End Try

            my.style_grid_many()
            my.style_grid_detail()
        End If
    End Sub

    Private Sub bbi_winter_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_winter.ItemClick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            Dim my As Master = CType(ActiveMdiChild, Master)
            bsi_gridstyle.Caption = "Grid : " + bbi_winter.Caption
            update_style_grid("Winter")

            Try
                my.style_grid(my.get_gv(), "Winter")
            Catch ex As Exception
            End Try

            my.style_grid_many()
            my.style_grid_detail()
        End If
    End Sub

    Private Sub bbi_spring_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_spring.ItemClick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            Dim my As Master = CType(ActiveMdiChild, Master)
            bsi_gridstyle.Caption = "Grid : " + bbi_spring.Caption
            update_style_grid("Spring")

            Try
                my.style_grid(my.get_gv(), "Spring")
            Catch ex As Exception
            End Try

            my.style_grid_many()
            my.style_grid_detail()
        End If
    End Sub

    Private Sub bbi_summer_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_summer.ItemClick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            Dim my As Master = CType(ActiveMdiChild, Master)
            bsi_gridstyle.Caption = "Grid : " + bbi_summer.Caption
            update_style_grid("Summer")

            Try
                my.style_grid(my.get_gv(), "Summer")
            Catch ex As Exception
            End Try

            my.style_grid_many()
            my.style_grid_detail()
        End If
    End Sub

    Private Sub bbi_Autumn_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Autumn.ItemClick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            Dim my As Master = CType(ActiveMdiChild, Master)
            bsi_gridstyle.Caption = "Grid : " + bbi_Autumn.Caption
            update_style_grid("Autumn")

            Try
                my.style_grid(my.get_gv(), "Autumn")
            Catch ex As Exception
            End Try

            my.style_grid_many()
            my.style_grid_detail()
        End If
    End Sub

    Private Sub bbi_Brown_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Brown.ItemClick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            Dim my As Master = CType(ActiveMdiChild, Master)
            bsi_gridstyle.Caption = "Grid : " + bbi_Brown.Caption
            update_style_grid("Brown")

            Try
                my.style_grid(my.get_gv(), "Brown")
            Catch ex As Exception
            End Try

            my.style_grid_many()
            my.style_grid_detail()
        End If
    End Sub

    Private Sub bbi_Chiaroscuro_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Chiaroscuro.ItemClick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            Dim my As Master = CType(ActiveMdiChild, Master)
            bsi_gridstyle.Caption = "Grid : " + bbi_Chiaroscuro.Caption
            update_style_grid("Chiaroscuro")

            Try
                my.style_grid(my.get_gv(), "Chiaroscuro")
            Catch ex As Exception
            End Try

            my.style_grid_many()
            my.style_grid_detail()
        End If
    End Sub

    Private Sub bbi_Desert_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Desert.ItemClick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            Dim my As Master = CType(ActiveMdiChild, Master)
            bsi_gridstyle.Caption = "Grid : " + bbi_Desert.Caption
            update_style_grid("Desert")

            Try
                my.style_grid(my.get_gv(), "Desert")
            Catch ex As Exception
            End Try

            my.style_grid_many()
            my.style_grid_detail()
        End If
    End Sub

    Private Sub bbi_Gray_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Gray.ItemClick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            Dim my As Master = CType(ActiveMdiChild, Master)
            bsi_gridstyle.Caption = "Grid : " + bbi_Gray.Caption
            update_style_grid("Gray")

            Try
                my.style_grid(my.get_gv(), "Gray")
            Catch ex As Exception
            End Try

            my.style_grid_many()
            my.style_grid_detail()
        End If
    End Sub

    Private Sub bbi_Orange_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Orange.ItemClick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            Dim my As Master = CType(ActiveMdiChild, Master)
            bsi_gridstyle.Caption = "Grid : " + bbi_Orange.Caption
            update_style_grid("Orange")

            Try
                my.style_grid(my.get_gv(), "Orange")
            Catch ex As Exception
            End Try

            my.style_grid_many()
            my.style_grid_detail()
        End If
    End Sub

    Private Sub bbi_BlueOffice_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_BlueOffice.ItemClick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            Dim my As Master = CType(ActiveMdiChild, Master)
            bsi_gridstyle.Caption = "Grid : " + bbi_BlueOffice.Caption
            update_style_grid("Blue Office")

            Try
                my.style_grid(my.get_gv(), "Blue Office")
            Catch ex As Exception
            End Try

            my.style_grid_many()
            my.style_grid_detail()
        End If
    End Sub

    Private Sub bbi_OliveOffice_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_OliveOffice.ItemClick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            Dim my As Master = CType(ActiveMdiChild, Master)
            bsi_gridstyle.Caption = "Grid : " + bbi_OliveOffice.Caption
            update_style_grid("Olive Office")

            Try
                my.style_grid(my.get_gv(), "Olive Office")
            Catch ex As Exception
            End Try

            my.style_grid_many()
            my.style_grid_detail()
        End If
    End Sub

    Private Sub bbiSilverOffice_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiSilverOffice.ItemClick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            Dim my As Master = CType(ActiveMdiChild, Master)
            bsi_gridstyle.Caption = "Grid : " + bbiSilverOffice.Caption
            update_style_grid("Silver Office")

            Try
                my.style_grid(my.get_gv(), "Silver Office")
            Catch ex As Exception
            End Try

            my.style_grid_many()
            my.style_grid_detail()
        End If
    End Sub

    Private Sub bbi_UserFormat1_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_UserFormat1.ItemClick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            Dim my As Master = CType(ActiveMdiChild, Master)
            bsi_gridstyle.Caption = "Grid : " + bbi_UserFormat1.Caption
            update_style_grid("UserFormat1")

            Try
                my.style_grid(my.get_gv(), "UserFormat1")
            Catch ex As Exception
            End Try

            my.style_grid_many()
            my.style_grid_detail()
        End If
    End Sub

    Private Sub bbi_UserFormat2_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_UserFormat2.ItemClick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            Dim my As Master = CType(ActiveMdiChild, Master)
            update_style_grid("UserFormat2")
            bsi_gridstyle.Caption = "Grid : " + bbi_UserFormat2.Caption

            Try
                my.style_grid(my.get_gv(), "UserFormat2")
            Catch ex As Exception
            End Try

            my.style_grid_many()
            my.style_grid_detail()
        End If
    End Sub

    Private Sub bbi_UserFormat3_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_UserFormat3.ItemClick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            Dim my As Master = CType(ActiveMdiChild, Master)
            bsi_gridstyle.Caption = "Grid : " + bbi_UserFormat3.Caption
            update_style_grid("UserFormat3")

            Try
                my.style_grid(my.get_gv(), "UserFormat3")
            Catch ex As Exception
            End Try

            my.style_grid_many()
            my.style_grid_detail()
        End If
    End Sub

    Private Sub bbi_UserFormat4_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_UserFormat4.ItemClick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            Dim my As Master = CType(ActiveMdiChild, Master)
            bsi_gridstyle.Caption = "Grid : " + bbi_UserFormat4.Caption
            update_style_grid("UserFormat4")

            Try
                my.style_grid(my.get_gv(), "UserFormat4")
            Catch ex As Exception
            End Try

            my.style_grid_many()
            my.style_grid_detail()
        End If
    End Sub

    Private Sub bbi_rose_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_rose.ItemClick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            Dim my As Master = CType(ActiveMdiChild, Master)
            bsi_gridstyle.Caption = "Grid : " + bbi_rose.Caption
            update_style_grid("Rose")

            Try
                my.style_grid(my.get_gv(), "Rose")
            Catch ex As Exception
            End Try

            my.style_grid_many()
            my.style_grid_detail()
        End If
    End Sub

    Private Sub bbi_WindowsClassic_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_WindowsClassic.ItemClick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            Dim my As Master = CType(ActiveMdiChild, Master)
            bsi_gridstyle.Caption = "Grid : " + bbi_WindowsClassic.Caption
            update_style_grid("Windows Classic")

            Try
                my.style_grid(my.get_gv(), "Windows Classic")
            Catch ex As Exception
            End Try

            my.style_grid_many()
            my.style_grid_detail()
        End If
    End Sub

    Private Sub bbi_WindowsStandard_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_WindowsStandard.ItemClick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            Dim my As Master = CType(ActiveMdiChild, Master)
            bsi_gridstyle.Caption = "Grid : " + bbi_WindowsStandard.Caption
            update_style_grid("Windows Standard")

            Try
                my.style_grid(my.get_gv(), "Windows Standard")
            Catch ex As Exception
            End Try

            my.style_grid_many()
            my.style_grid_detail()
        End If
    End Sub

    Private Sub bbi_Slate_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Slate.ItemClick
        Dim activeChild As Form = Me.ActiveMdiChild
        If (Not activeChild Is Nothing) Then
            Dim my As Master = CType(ActiveMdiChild, Master)
            bsi_gridstyle.Caption = "Grid : " + bbi_Slate.Caption
            update_style_grid("Slate")

            Try
                my.style_grid(my.get_gv(), "Slate")
            Catch ex As Exception
            End Try

            my.style_grid_many()
            my.style_grid_detail()
        End If
    End Sub
#End Region

#Region "Hide Bar & Dock Panel"
    Private Sub bci_standard_CheckedChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bci_standard.CheckedChanged
        If bci_standard.Checked = True Then
            barStandardButton.Visible = True
        Else
            barStandardButton.Visible = False
        End If
    End Sub

    Private Sub bci_skin_style_CheckedChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bci_skin_style.CheckedChanged
        If bci_skin_style.Checked = True Then
            BarPaintStyle.Visible = True
        Else
            BarPaintStyle.Visible = False
        End If
    End Sub

    Private Sub bci_statusbar_CheckedChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bci_statusbar.CheckedChanged
        If bci_statusbar.Checked = True Then
            bar_statusbar.Visible = True
        Else
            bar_statusbar.Visible = False
        End If
    End Sub

    Private Sub bci_mybar_CheckedChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bci_mybar.CheckedChanged
        If bci_mybar.Checked = True Then
            bar_mybar.Visible = True
        Else
            bar_mybar.Visible = False
        End If
    End Sub

    Public Overridable Sub status_visible_dock_panel(ByVal par_nama As String, ByVal par_status As Boolean)

    End Sub

    Public Overridable Sub update_status_dock(ByVal par_field As String, ByVal par_status As Boolean)
        Try
            Using objupdate As New master_new.WDABasepgsql("", "")
                With objupdate
                    .SQL = "update tconfskin set " + par_field + " = " + par_status.ToString + _
                    " where userid = " + master_new.ClsVar.sUserID.ToString
                    .InitializeCommand()
                    .ExecuteStoredProcedure()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overridable Sub bci_hris_master_data_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bci_hris_master_data.ItemClick
        If bci_hris_master_data.Checked = True Then
            status_visible_dock_panel("dp_masterdata", False)
            'update_status_dock("hris_master_data", False)
        Else
            status_visible_dock_panel("dp_masterdata", True)
            'update_status_dock("hris_master_data", True)
        End If
    End Sub

    Private Sub bci_hris_recrutment_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bci_distribution.ItemClick
        If bci_distribution.Checked = True Then
            status_visible_dock_panel("dp_distribution", False)
            'update_status_dock("hris_recrutment", False)
        Else
            status_visible_dock_panel("dp_distribution", True)
            'update_status_dock("hris_recrutment", True)
        End If
    End Sub

    Private Sub bci_financial_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bci_financial.ItemClick
        If bci_financial.Checked = True Then
            status_visible_dock_panel("dp_financial", False)
            'update_status_dock("hris_hr", False)
        Else
            status_visible_dock_panel("dp_financial", True)
            'update_status_dock("hris_hr", True)
        End If
    End Sub

    Private Sub bci_hris_attendance_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bci_sales_order.ItemClick
        If bci_sales_order.Checked = True Then
            status_visible_dock_panel("dp_sales_order", False)
            'update_status_dock("hris_attendance", False)
        Else
            status_visible_dock_panel("dp_sales_order", True)
            'update_status_dock("hris_attendance", True)
        End If
    End Sub

    Private Sub bci_hris_payroll_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bci_hris_payroll.ItemClick
        If bci_hris_payroll.Checked = True Then
            status_visible_dock_panel("dp_payroll", False)
            update_status_dock("hris_payroll", False)
        Else
            status_visible_dock_panel("dp_payroll", True)
            update_status_dock("hris_payroll", True)
        End If
    End Sub

    Private Sub bci_erp_master_data_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bci_erp_master_data.ItemClick
        If bci_erp_master_data.Checked = True Then
            status_visible_dock_panel("dp_erp_masterdata", False)
            update_status_dock("erp_master_data", False)
        Else
            status_visible_dock_panel("dp_erp_masterdata", True)
            update_status_dock("erp_master_data", True)
        End If
    End Sub

    Private Sub bci_erp_distribution_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bci_erp_distribution.ItemClick
        If bci_erp_distribution.Checked = True Then
            status_visible_dock_panel("dp_erp_distribution", False)
            update_status_dock("erp_distribution", False)
        Else
            status_visible_dock_panel("dp_erp_distribution", True)
            update_status_dock("erp_distribution", True)
        End If
    End Sub

    Private Sub bci_erp_manufacturing_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bci_erp_manufacturing.ItemClick
        If bci_erp_manufacturing.Checked = True Then
            status_visible_dock_panel("dp_manufacturing", False)
            update_status_dock("erp_manufacturing", False)
        Else
            status_visible_dock_panel("dp_manufacturing", True)
            update_status_dock("erp_manufacturing", True)
        End If
    End Sub

    Private Sub bci_erp_financial_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bci_erp_financial.ItemClick
        If bci_erp_financial.Checked = True Then
            status_visible_dock_panel("dp_financial", False)
            update_status_dock("erp_financial", False)
        Else
            status_visible_dock_panel("dp_financial", True)
            update_status_dock("erp_financial", True)
        End If
    End Sub

    Private Sub bci_erp_customer_services_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bci_erp_customer_services.ItemClick
        If bci_erp_customer_services.Checked = True Then
            status_visible_dock_panel("dp_customer_service", False)
            update_status_dock("erp_customer_services", False)
        Else
            status_visible_dock_panel("dp_customer_service", True)
            update_status_dock("erp_customer_services", True)
        End If
    End Sub

    Private Sub bci_syspro_financial_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bci_syspro_financial.ItemClick
        If bci_syspro_financial.Checked = True Then
            status_visible_dock_panel("dp_syspro_financial", False)
            update_status_dock("syspro_financial", False)
        Else
            status_visible_dock_panel("dp_syspro_financial", True)
            update_status_dock("syspro_financial", True)
        End If
    End Sub

    Private Sub bci_syspro_additional_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bci_syspro_additional.ItemClick
        If bci_syspro_additional.Checked = True Then
            status_visible_dock_panel("dp_syspro_additional", False)
            update_status_dock("syspro_additional", False)
        Else
            status_visible_dock_panel("dp_syspro_additional", True)
            update_status_dock("syspro_additional", True)
        End If
    End Sub
#End Region

    Private Sub bbi_change_password_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_change_password.ItemClick
        Dim frm As New FChangePassword
        frm.ShowDialog()
    End Sub

    Private Sub BarButtonItem2_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        Dim frm As New FAbout
        frm.ShowDialog()
    End Sub

    Private Sub bbi_syspropage_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_syspropage.ItemClick
        Dim frm As New FSyspro
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Public Overridable Sub bbi_reminder_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_reminder.ItemClick

    End Sub

    Private Sub bbi_user_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_user.ItemClick
        Dim frm As New FUserGroup
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub bbi_group_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_group.ItemClick
        Dim frm As New FGroup
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub bbi_menu_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_menu.ItemClick
        Dim frm As New FMenuAuthorization
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub bbi_custom_ItemClick_1(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_custom.ItemClick
        show_mycustom()
    End Sub

    Private Sub bbi_myfavorite_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_myfavorite.ItemClick
        show_myfavorite()
    End Sub

    Public Overridable Sub show_mycustom()

    End Sub

    Public Overridable Sub show_myfavorite()

    End Sub

    Private Sub NotifyIcon1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Visible = True
    End Sub

    Private Sub bbi_refresh_menu_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_refresh_menu.ItemClick
        InitMenu()
    End Sub

    Private Sub bbi_add_menu_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_add_menu.ItemClick
        Dim frm As New FAddMenu
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub bci_man_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bci_man.ItemClick
        Try
            If bci_man.Checked = True Then
                status_visible_dock_panel("dp_manufacturing", False)
                'update_status_dock("hris_hr", False)
            Else
                status_visible_dock_panel("dp_manufacturing", True)
                'update_status_dock("hris_hr", True)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Overridable Sub show_user_history()

    End Sub
    Public Overridable Sub chat_syspro()

    End Sub
    Public Overridable Sub Update_syspro()
        'Dim frm As New frmUpdate
        'frm.MdiParent = Me
        'frm.Show()
        Try
            'Box("Terdapat update aplikasi")
            Dim myprocess As System.Diagnostics.Process = New System.Diagnostics.Process()

            With myprocess
                .StartInfo.FileName = GetCurrentPath() & "UpdateProgram.exe"
                If System.IO.File.Exists(.StartInfo.FileName) Then
                    .StartInfo.RedirectStandardOutput = True
                    .StartInfo.UseShellExecute = False
                    .Start()
                Else
                    Box("File " & .StartInfo.FileName & " tidak ada")
                End If

            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub bbi_user_History2_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_user_History2.ItemClick
        show_user_history()
    End Sub

    Private Sub blb_teamviewer_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles blb_teamviewer.ItemClick
        execute_teamviewer()
    End Sub

    Private Sub bbi_chat_Syspro_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_chat_Syspro.ItemClick
        chat_syspro()
    End Sub

    Public Sub open_form(ByVal par_form As String)
        Try
            If par_form = "FMenuAuthorization" Then
                Dim frm As New FMenuAuthorization
                frm.MdiParent = Me
                frm.set_window(Me)
                frm.type_form = True
                frm.Show()

            ElseIf par_form = "FUserGroup" Then
                Dim frm As New FUserGroup
                frm.MdiParent = Me
                frm.set_window(Me)
                frm.type_form = True
                frm.Show()

            ElseIf par_form = "FGroup" Then
                Dim frm As New FGroup
                frm.MdiParent = Me
                frm.set_window(Me)
                frm.type_form = True
                frm.Show()

            ElseIf par_form = "FAddMenu" Then
                Dim frm As New FAddMenu
                frm.MdiParent = Me
                frm.set_window(Me)
                frm.type_form = True
                frm.Show()

            End If
        Catch ex As Exception
            MessageBox.Show("Form not found", "Err", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BarButtonItem3_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        ClsVar.par_lang = "id"
        SaveTextToFile("id", master_new.ModFunction.appbase() & _
                        "\filekonfigurasi\lang.txt")
        For Each ChildForm As Form In Me.MdiChildren
            Dim my As Master = CType(ChildForm, Master)
            my.change_lang()
        Next
    End Sub

    Private Sub BarButtonItem4_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        ClsVar.par_lang = "en"
        SaveTextToFile("en", master_new.ModFunction.appbase() & _
                  "\filekonfigurasi\lang.txt")
        For Each ChildForm As Form In Me.MdiChildren
            Dim my As Master = CType(ChildForm, Master)
            my.change_lang()
        Next
    End Sub

    Private Sub BarLargeButtonItem1_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarLargeButtonItem1.ItemClick
        Update_syspro()
    End Sub
End Class
