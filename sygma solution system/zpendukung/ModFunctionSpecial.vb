Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports DevExpress.XtraEditors.Controls
Imports System.Runtime.InteropServices
Imports DevExpress.XtraCharts
Imports System.Collections.Generic
Imports Microsoft.VisualBasic
Imports System
Imports System.Net
Imports System.IO

Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Text

'Imports Microsoft.Office.Interop.Excel

Public Class FlashWindow

    Public Enum enuFlashOptions As UInteger
        FLASHW_ALL = &H3 ' Flash both the window caption and taskbar button.

        FLASHW_CAPTION = &H1 ' Flash the window caption.
        FLASHW_STOP = 0 ' Stop flashing. The system restores the window to its original state.
        FLASHW_TIMER = &H4 ' Flash continuously, until the FLASHW_STOP flag is set.
        FLASHW_TIMERNOFG = &HC ' Flash continuously until the window comes to the foreground.
        FLASHW_TRAY = &H2
    End Enum

    Public Structure FlashWindowInfo
        Public cbSize As Integer
        Public hwnd As IntPtr
        Public dwFlags As UInteger
        Public uCount As UInteger
        Public dwTimeout As UInteger
    End Structure
    Declare Function FlashWindowEx Lib "user32.dll" (ByRef pInfo As FlashWindowInfo) As Boolean

    Public Sub FlashWindow(ByVal frmForm As Form, _
    ByVal FlashWindowInfoFlags As enuFlashOptions, _
    Optional ByVal intFlashTimes As UInteger = 5)
        '  If (frmForm.WindowState = FormWindowState.Minimized) Or FlashWindowInfoFlags = enuFlashOptions.FLASHW_STOP Then
        'If FlashWindowInfoFlags = enuFlashOptions.FLASHW_STOP Then
        '    Dim info As FlashWindowInfo
        '    With info
        '        .cbSize = Marshal.SizeOf(info)
        '        .dwFlags = FlashWindowInfoFlags ' See enumeration for flag values
        '        .dwTimeout = 0 'Flash rate in ms or default cursor blink rate
        '        .hwnd = frmForm.Handle()
        '        .uCount = intFlashTimes ' Number of times to flash
        '    End With
        '    FlashWindowEx(info)
        'Else
        Dim info As FlashWindowInfo
        With info
            .cbSize = Marshal.SizeOf(info)
            .dwFlags = FlashWindowInfoFlags ' See enumeration for flag values
            .dwTimeout = 0 'Flash rate in ms or default cursor blink rate
            .hwnd = frmForm.Handle()
            .uCount = intFlashTimes ' Number of times to flash
        End With
        FlashWindowEx(info)
        'End If
    End Sub
End Class

Module ModFunctionSpecial
    Dim func_data As New function_data
    Dim sSQL As String
    Public func_coll As New function_collection

    Public Sub init_le(ByVal le_object As DevExpress.XtraEditors.LookUpEdit, ByVal sql As String, _
                       ByVal par_member As String, ByVal par_member_caption As String, ByVal par_status As Boolean)
        Try
            Dim dt2 As New DataTable
            sSQL = sql

            dt2 = GetTableData(sSQL)
            With le_object
                If .Properties.Columns.VisibleCount = 0 Then
                    .Properties.Columns.Add(New LookUpColumnInfo(par_member, par_member_caption, 20))
                    '.Properties.Columns.Add(New LookUpColumnInfo(par_member, par_member_caption, 20))
                End If
                .Properties.DataSource = dt2
                .Properties.DisplayMember = dt2.Columns(par_member).ToString
                .Properties.ValueMember = dt2.Columns(par_member).ToString

                If dt2.Rows.Count > 0 Then
                    .EditValue = dt2.Rows(0).Item(par_member)
                End If

                .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                .Properties.BestFit()
                .Properties.DropDownRows = 6
                .Properties.PopupWidth = 300
            End With

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Sub init_le(ByVal le_object As DevExpress.XtraEditors.LookUpEdit, ByVal tipe As String)
        Dim dt2 As New DataTable
        Try
            If tipe = "en_id" Then
                sSQL = "select en_id ,en_code,en_desc  from en_mstr where en_id>0 " _
                    & "union  " _
                    & "select -1 as en_id,'' as en_code, '-' as en_desc " _
                    & "order by en_code"

                dt2 = GetTableData(sSQL)
                With le_object
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("en_desc").ToString
                    .Properties.ValueMember = dt2.Columns("en_id").ToString
                    .EditValue = dt2.Rows(0).Item("en_id")
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                    .Properties.BestFit()
                    .Properties.DropDownRows = 14
                    .Properties.PopupWidth = 300
                    .Properties.TextEditStyle = TextEditStyles.Standard
                End With
            ElseIf tipe = "area_id" Then
                sSQL = "select code_id ,code_name  from code_mstr where code_field='area_id' " _
                    & "union  " _
                    & "select -1 as code_id, '-' as code_name " _
                    & "order by code_id"

                dt2 = GetTableData(sSQL)
                With le_object 'emp_area_id
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("code_name").ToString
                    .Properties.ValueMember = dt2.Columns("code_id").ToString
                    .EditValue = dt2.Rows(0).Item("code_id")
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                    .Properties.BestFit()
                    .Properties.DropDownRows = 6
                    .Properties.PopupWidth = 300
                End With


            ElseIf tipe = "cc_mstr" Then
                sSQL = "select cc_id ,cc_code,cc_desc  from cc_mstr  " _
                    & "order by cc_desc"

                dt2 = GetTableData(sSQL)
                With le_object 'emp_area_id
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("cc_desc").ToString
                    .Properties.ValueMember = dt2.Columns("cc_id").ToString
                    .EditValue = dt2.Rows(0).Item("cc_id")
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                    .Properties.BestFit()
                    .Properties.DropDownRows = 6
                    .Properties.PopupWidth = 300
                End With
            ElseIf tipe = "bdgtt_type" Then
                sSQL = "select type_code ,type_desc  from bdgtt_type  " _
                    & "order by type_code"

                dt2 = GetTableData(sSQL)
                With le_object 'emp_area_id
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("type_code").ToString
                    .Properties.ValueMember = dt2.Columns("type_code").ToString
                    .EditValue = dt2.Rows(0).Item("type_code")
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                    .Properties.BestFit()
                    .Properties.DropDownRows = 6
                    .Properties.PopupWidth = 300
                End With

            ElseIf tipe = "Jenis_Kelamin" Then
                sSQL = "select code_id ,code_name  from code_mstr where code_field='Jenis_Kelamin' " _
                      & "order by code_seq, code_id"

                dt2 = GetTableData(sSQL)
                With le_object 'emp_area_id
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("code_name").ToString
                    .Properties.ValueMember = dt2.Columns("code_id").ToString
                    If dt2.Rows.Count > 0 Then
                        .EditValue = dt2.Rows(0).Item("code_id")
                    End If

                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                    .Properties.BestFit()
                    .Properties.DropDownRows = 6
                    .Properties.PopupWidth = 300
                End With

            ElseIf tipe = "gol_darah" Then
                sSQL = "select code_id ,code_name  from code_mstr where code_field='gol_darah' " _
                       & "order by code_seq, code_id"

                dt2 = GetTableData(sSQL)
                With le_object 'emp_area_id
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("code_name").ToString
                    .Properties.ValueMember = dt2.Columns("code_id").ToString
                    If dt2.Rows.Count > 0 Then
                        .EditValue = dt2.Rows(0).Item("code_id")
                    End If

                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                    .Properties.BestFit()
                    .Properties.DropDownRows = 6
                    .Properties.PopupWidth = 300
                End With

            ElseIf tipe = "bp_type" Then
                sSQL = "select code_id ,code_name  from code_mstr where code_field='bp_type' " _
                       & "order by code_seq, code_id"

                dt2 = GetTableData(sSQL)
                With le_object
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("code_name").ToString
                    .Properties.ValueMember = dt2.Columns("code_id").ToString
                    If dt2.Rows.Count > 0 Then
                        .EditValue = dt2.Rows(0).Item("code_id")
                    End If

                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                    .Properties.BestFit()
                    .Properties.DropDownRows = 6
                    .Properties.PopupWidth = 300
                End With

            ElseIf tipe = "WNegara" Then
                sSQL = "select code_id ,code_name  from code_mstr where code_field='WNegara' " _
                       & "order by code_seq, code_id"

                dt2 = GetTableData(sSQL)
                With le_object 'emp_area_id
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("code_name").ToString
                    .Properties.ValueMember = dt2.Columns("code_id").ToString
                    If dt2.Rows.Count > 0 Then
                        .EditValue = dt2.Rows(0).Item("code_id")
                    End If

                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                    .Properties.BestFit()
                    .Properties.DropDownRows = 6
                    .Properties.PopupWidth = 300
                End With
            ElseIf tipe = "category" Then
                sSQL = "SELECT  " _
                        & "  a.cat_id, " _
                        & "  a.cat_desc " _
                        & "FROM " _
                        & "  public.t_category a " _
                        & "union  " _
                        & "select '' as cat_id, '#Clear' as cat_desc " _
                        & "ORDER BY " _
                        & "  cat_desc"

                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("cat_desc", "Pilihan", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("cat_id", "", 0, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Default))
                    End If
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("cat_desc").ToString
                    .Properties.ValueMember = dt2.Columns("cat_id").ToString
                    .ItemIndex = 0
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 8
                    '.Properties.PopupWidth = 300
                End With
            ElseIf tipe = "unit_measure" Then
                sSQL = "select code_id,code_code,code_name from code_mstr where code_field='unitmeasure' order by code_code "

                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("code_id", "ID", 10, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Default))
                        .Properties.Columns.Add(New LookUpColumnInfo("code_code", "Code", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("code_name", "Name", 20))
                    End If
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("code_code").ToString
                    .Properties.ValueMember = dt2.Columns("code_id").ToString
                    .EditValue = dt2.Rows(0).Item("code_id")
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 12
                    '.Properties.PopupWidth = 300
                End With
            ElseIf tipe = "gl_periode" Then
                sSQL = "SELECT  " _
                    & "  a.gcal_year, " _
                    & "  a.gcal_periode, " _
                    & "  a.gcal_start_date, " _
                    & "  a.gcal_end_date,a.gcal_oid " _
                    & "FROM " _
                    & "  public.gcal_mstr a " _
                    & "ORDER BY " _
                    & "  a.gcal_year, " _
                    & "  a.gcal_periode"

                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("gcal_year", "Year", 10, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Default))
                        .Properties.Columns.Add(New LookUpColumnInfo("gcal_periode", "Periode", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("gcal_start_date", "Start Date", 20, DevExpress.Utils.FormatType.DateTime, "D", True, DevExpress.Utils.HorzAlignment.Default))
                        .Properties.Columns.Add(New LookUpColumnInfo("gcal_end_date", "End Date", 20, DevExpress.Utils.FormatType.DateTime, "D", True, DevExpress.Utils.HorzAlignment.Default))

                    End If

                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("gcal_periode").ToString
                    .Properties.ValueMember = dt2.Columns("gcal_start_date").ToString
                    .EditValue = dt2.Rows(0).Item("gcal_periode")
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 8
                    .Properties.TextEditStyle = TextEditStyles.Standard
                    '.Properties.PopupWidth = 300
                End With

            ElseIf tipe = "en_mstr" Then
                sSQL = "select en_id, en_code, en_desc from en_mstr where en_active ~~* 'Y'" + _
                        " and en_id in (select user_en_id from tconfuserentity " + _
                        " where userid = " + master_new.ClsVar.sUserID.ToString + ") " + _
                        " order by en_code "

                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("en_id", "ID", 10))
                        .Properties.Columns.Add(New LookUpColumnInfo("en_code", "Code", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("en_desc", "Desc", 20))
                    End If

                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("en_desc").ToString
                    .Properties.ValueMember = dt2.Columns("en_id").ToString
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 20
                    .Properties.TextEditStyle = TextEditStyles.Standard
                    .EditValue = dt2.Rows(0).Item("en_id")
                    '.Properties.PopupWidth = 300
                End With
            ElseIf tipe = "account_all" Then
                sSQL = "select  " _
                    & "ac_id,ac_code_hirarki, " _
                    & "ac_code, " _
                    & "ac_name, " _
                    & "ac_desc " _
                    & " from ac_mstr  " _
                    & "where   " _
                    & "lower(ac_active)='y' " _
                    & "order by ac_code_hirarki"


                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("ac_id", "ac_id", 10, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Default))
                        .Properties.Columns.Add(New LookUpColumnInfo("ac_code", "Code", 10))
                        .Properties.Columns.Add(New LookUpColumnInfo("ac_code_hirarki", "Code Hide", 10))
                        .Properties.Columns.Add(New LookUpColumnInfo("ac_name", "Account Name", 10))
                        .Properties.Columns.Add(New LookUpColumnInfo("ac_desc", "Desc", 20))
                    End If

                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("ac_name").ToString
                    .Properties.ValueMember = dt2.Columns("ac_id").ToString
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 30
                    .Properties.TextEditStyle = TextEditStyles.Standard
                    .EditValue = dt2.Rows(0).Item("ac_id")
                    '.Properties.PopupWidth = 300
                End With
            ElseIf tipe = "account" Then
                sSQL = "select  " _
                    & "ac_id, " _
                    & "ac_code, " _
                    & "ac_name, " _
                    & "ac_desc " _
                    & " from ac_mstr  " _
                    & "where   " _
                    & "lower(ac_active)='y' and lower(ac_is_sumlevel)='n' " _
                    & "order by ac_code"


                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("ac_id", "ac_id", 10, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Default))
                        .Properties.Columns.Add(New LookUpColumnInfo("ac_code", "Code", 5))
                        .Properties.Columns.Add(New LookUpColumnInfo("ac_name", "Account Name", 15))
                        .Properties.Columns.Add(New LookUpColumnInfo("ac_desc", "Desc", 20))
                    End If

                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("ac_name").ToString
                    .Properties.ValueMember = dt2.Columns("ac_id").ToString
                    .EditValue = dt2.Rows(0).Item("ac_id")
                    '.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    '.Properties.BestFit()
                    .Properties.DropDownRows = 30
                    .Properties.TextEditStyle = TextEditStyles.Standard
                    .Properties.PopupWidth = 600
                End With
            ElseIf tipe = "gcal_mstr" Then
                sSQL = "SELECT  " _
                    & "  a.gcal_oid, " _
                    & "  a.gcal_year, " _
                    & "  a.gcal_periode, " _
                     & "  a.gcal_start_date,TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') as periode, " _
                    & "  a.gcal_end_date,coalesce(a.gcal_pra_closing,'N') as gcal_pra_closing,coalesce(gcal_closing,'N') as  gcal_closing " _
                    & "FROM " _
                    & "  public.gcal_mstr a " _
                    & "ORDER BY " _
                    & "  a.gcal_year desc, " _
                    & "  a.gcal_periode desc"


                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("gcal_oid", "gcal_oid", 10, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Default))
                        .Properties.Columns.Add(New LookUpColumnInfo("gcal_year", "Year", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("gcal_periode", "Periode", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("gcal_start_date", "Start Date", 10, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Default))
                        .Properties.Columns.Add(New LookUpColumnInfo("gcal_end_date", "Start Date", 10, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Default))
                        .Properties.Columns.Add(New LookUpColumnInfo("gcal_pra_closing", "Pra Closing", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("gcal_closing", "Closing", 20))
                    End If
                    .Properties.DataSource = dt2
                    .Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    .Properties.DisplayFormat.FormatString = "d"
                    .Properties.DisplayMember = dt2.Columns("gcal_start_date").ToString
                    .Properties.ValueMember = dt2.Columns("gcal_oid").ToString
                    .EditValue = dt2.Rows(0).Item("gcal_oid").ToString
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 20
                    .Properties.PopupWidth = 300
                End With
            ElseIf tipe = "cu_mstr" Then
                sSQL = "select cu_id, cu_name from cu_mstr where cu_active ~~* 'Y' order by cu_id"

                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("cu_id", "ID", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("cu_name", "Name", 20))
                    End If
                    .Properties.DataSource = dt2

                    .Properties.DisplayMember = dt2.Columns("cu_name").ToString
                    .Properties.ValueMember = dt2.Columns("cu_id").ToString
                    .EditValue = dt2.Rows(0).Item("cu_id")
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 8
                    .Properties.PopupWidth = 300
                End With
            ElseIf tipe = "si_mstr" Then
                sSQL = "select si_id,si_code, si_desc from si_mstr where si_active ~~* 'Y' order by si_code"

                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("si_id", "ID", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("si_code", "Code", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("si_desc", "Desc", 20))
                    End If
                    .Properties.DataSource = dt2

                    .Properties.DisplayMember = dt2.Columns("si_desc").ToString
                    .Properties.ValueMember = dt2.Columns("si_id").ToString
                    .EditValue = dt2.Rows(0).Item("si_id")
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 8
                    .Properties.PopupWidth = 300
                End With
            ElseIf tipe = "type_wo" Then
                sSQL = "select 'N' as id,'Normal' as value union select 'R' as id,'Repeat Order' as value order by id"

                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("id", "ID", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("value", "Value", 20))
                    End If
                    .Properties.DataSource = dt2

                    .Properties.DisplayMember = dt2.Columns("value").ToString
                    .Properties.ValueMember = dt2.Columns("id").ToString
                    .EditValue = dt2.Rows(0).Item("id")
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 8
                    .Properties.PopupWidth = 300
                End With
            ElseIf tipe = "status_wo" Then
                sSQL = "select 'F' as id,'Firm' as value union select 'R' as id,'Release' as value order by id"

                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("id", "ID", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("value", "Value", 20))
                    End If
                    .Properties.DataSource = dt2

                    .Properties.DisplayMember = dt2.Columns("value").ToString
                    .Properties.ValueMember = dt2.Columns("id").ToString
                    .EditValue = dt2.Rows(0).Item("id")
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 8
                    .Properties.PopupWidth = 300
                End With
            ElseIf tipe = "jenis_buku" Then
                sSQL = "SELECT  " _
                    & "  a.finishing_code,finishing_sb,finishing_cv,finishing_brd " _
                    & "FROM " _
                    & "  public.cetak_finishing a"


                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("finishing_code", "ID", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("finishing_sb", "SB", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("finishing_cv", "CV", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("finishing_brd", "BRD", 20))
                    End If
                    .Properties.DataSource = dt2

                    .Properties.DisplayMember = dt2.Columns("finishing_code").ToString
                    .Properties.ValueMember = dt2.Columns("finishing_code").ToString
                    .EditValue = dt2.Rows(0).Item("finishing_code")
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 8
                    .Properties.PopupWidth = 300
                End With
            ElseIf tipe = "spek_buku" Then
                sSQL = "SELECT  " _
                    & "  a.spek_ukuran,spek_hal_lembar_plano,spek_pot_skiblat_plano,spek_pot_cover_plano " _
                    & "FROM " _
                    & "  public.cetak_spec_buku a " _
                    & "ORDER BY " _
                    & "  a.spek_number"

                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("spek_ukuran", "ID", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("spek_hal_lembar_plano", "Lembar Plano", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("spek_pot_skiblat_plano", "Potongan Skipblat Plano", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("spek_pot_cover_plano", "Potongan Cover Plano", 20))
                        '
                    End If
                    .Properties.DataSource = dt2

                    .Properties.DisplayMember = dt2.Columns("spek_ukuran").ToString
                    .Properties.ValueMember = dt2.Columns("spek_ukuran").ToString
                    .EditValue = dt2.Rows(0).Item("spek_ukuran")
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 8
                    .Properties.PopupWidth = 300
                End With
            ElseIf tipe = "opsi_isi" Then
                sSQL = "SELECT  " _
                    & "  a.kode,nilai " _
                    & "FROM " _
                    & "  public.cetak_proses_isi a"

                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("kode", "Opsi", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("nilai", "Nilai", 20))

                    End If
                    .Properties.DataSource = dt2

                    .Properties.DisplayMember = dt2.Columns("kode").ToString
                    .Properties.ValueMember = dt2.Columns("kode").ToString
                    .EditValue = dt2.Rows(0).Item("kode")
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 4
                    .Properties.PopupWidth = 300
                End With
            ElseIf tipe = "yes_no" Then
                sSQL = "select 'Y' as id,'Yes' as value union select 'N' as id,'No' as value order by id"

                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("id", "ID", 10, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Default))
                        .Properties.Columns.Add(New LookUpColumnInfo("value", "Value", 20))
                    End If
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("value").ToString
                    .Properties.ValueMember = dt2.Columns("id").ToString
                    .EditValue = dt2.Rows(0).Item("id")
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 2
                    .Properties.PopupWidth = 25
                End With

            ElseIf tipe = "ya_tidak" Then
                sSQL = "select 1 as id,'Ya' as value union select 0 as id,'Tidak' as value order by id desc"

                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("id", "ID", 10, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Default))
                        .Properties.Columns.Add(New LookUpColumnInfo("value", "Value", 20))
                    End If
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("value").ToString
                    .Properties.ValueMember = dt2.Columns("id").ToString
                    .EditValue = dt2.Rows(0).Item("id")
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 2
                    .Properties.PopupWidth = 25
                End With

            ElseIf tipe = "sales_report_type" Then
                sSQL = "select '1' as id,'Sales Order Shipment Summary' as value " _
                        & "union select '2' as id,'Customer Growth Report' as value " _
                        & "union select '3' as id,'Product Summary Report' as value " _
                        & "union select '4' as id,'Payment Type Summary Report' as value " _
                        & "union select '5' as id,'Product Summary Report by Entity' as value " _
                        & "order by id"

                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("id", "ID", 10, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Default))
                        .Properties.Columns.Add(New LookUpColumnInfo("value", "Value", 20))
                    End If
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("value").ToString
                    .Properties.ValueMember = dt2.Columns("id").ToString
                    .EditValue = dt2.Rows(0).Item("id")
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 6
                    .Properties.PopupWidth = 25
                End With
            ElseIf tipe = "server_list" Then
                sSQL = "SELECT  " _
                    & "  a.conf_oid, " _
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


                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("conf_ip", "Server", 10, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Default))
                        .Properties.Columns.Add(New LookUpColumnInfo("conf_port", "Server", 10, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Default))
                        .Properties.Columns.Add(New LookUpColumnInfo("conf_db", "Server", 10, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Default))
                        .Properties.Columns.Add(New LookUpColumnInfo("conf_user", "Server", 10, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Default))
                        .Properties.Columns.Add(New LookUpColumnInfo("conf_en_id", "Server", 10, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Default))
                        .Properties.Columns.Add(New LookUpColumnInfo("conf_name", "Server", 20))
                    End If
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("conf_name").ToString
                    .Properties.ValueMember = dt2.Columns("conf_name").ToString
                    .EditValue = dt2.Rows(0).Item("conf_name")
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 6
                    .Properties.PopupWidth = 25
                End With
            ElseIf tipe = "server_list_check" Then
                sSQL = "SELECT  " _
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


                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("conf_ip", "Server", 10, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Default))
                        .Properties.Columns.Add(New LookUpColumnInfo("conf_port", "Server", 10, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Default))
                        .Properties.Columns.Add(New LookUpColumnInfo("conf_db", "Server", 10, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Default))
                        .Properties.Columns.Add(New LookUpColumnInfo("conf_user", "Server", 10, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Default))
                        .Properties.Columns.Add(New LookUpColumnInfo("conf_en_id", "Server", 10, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Default))
                        .Properties.Columns.Add(New LookUpColumnInfo("checklist", "Check", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("conf_name", "Server", 20))

                    End If
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("conf_name").ToString
                    .Properties.ValueMember = dt2.Columns("conf_name").ToString
                    .EditValue = dt2.Rows(0).Item("conf_name")
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 6
                    .Properties.PopupWidth = 25
                End With

            ElseIf tipe = "casho_type" Then
                sSQL = "select 'TEMP' as id,'Temporary Expenses' as value union select 'REAL' as id,'Realization' as value union select '' as id,'-' as value order by id desc"

                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("id", "ID", 10))
                        .Properties.Columns.Add(New LookUpColumnInfo("value", "Value", 20))
                    End If
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("value").ToString
                    .Properties.ValueMember = dt2.Columns("id").ToString
                    .EditValue = dt2.Rows(0).Item("id")
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 2
                    .Properties.PopupWidth = 25

                End With



            ElseIf tipe = "invest_type" Then
                sSQL = "SELECT  " _
                    & "  a.investt_code,investt_desc " _
                    & "FROM " _
                    & "  public.investt_type a order by investt_code"

                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("investt_code", "Code", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("investt_desc", "Desc", 20))

                    End If
                    .Properties.DataSource = dt2

                    .Properties.DisplayMember = dt2.Columns("investt_code").ToString
                    .Properties.ValueMember = dt2.Columns("investt_code").ToString
                    .EditValue = dt2.Rows(0).Item("investt_code")
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 4
                    .Properties.PopupWidth = 300
                End With

                'invest_type
            ElseIf tipe = "investp_project" Then

                sSQL = "SELECT  " _
                    & "  investp_code, " _
                    & "  investp_code || ' ' || coalesce(investp_desc,'') as investp_desc ," _
                    & "  investp_nilai, " _
                    & "  investp_tanggal_awal, " _
                    & "  investp_tanggal_akhir " _
                    & "FROM  " _
                    & "  public.investp_project  " _
                    & "order by investp_code"


                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("investp_code", "Code", 10, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Default))
                        .Properties.Columns.Add(New LookUpColumnInfo("investp_desc", "Desc", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("investp_tanggal_awal", "Start Date", 20, DevExpress.Utils.FormatType.DateTime, "D", True, DevExpress.Utils.HorzAlignment.Default))
                        .Properties.Columns.Add(New LookUpColumnInfo("investp_tanggal_akhir", "End Date", 20, DevExpress.Utils.FormatType.DateTime, "D", True, DevExpress.Utils.HorzAlignment.Default))

                    End If

                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("investp_desc").ToString
                    .Properties.ValueMember = dt2.Columns("investp_code").ToString
                    .EditValue = dt2.Rows(0).Item("investp_code")
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 8
                    .Properties.TextEditStyle = TextEditStyles.Standard
                    '.Properties.PopupWidth = 300
                End With

            ElseIf tipe = "year_periode" Then

                Dim dtnew As New DataTable()

                ' Add columns to the DataTable
                dtnew.Columns.Add("year", GetType(Integer))
                dtnew.Columns.Add("description", GetType(String))

                ' Loop through years
                For year As Integer = Now.Year To (Now.Year - 10) Step -1

                    ' Add a row to the DataTable for the current year
                    Dim row As DataRow = dtnew.NewRow()
                    row("Year") = year
                    row("Description") = year.ToString()
                    dtnew.Rows.Add(row)

                Next



                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("year", "Year", 10, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Default))
                        .Properties.Columns.Add(New LookUpColumnInfo("description", "Desc", 20))


                    End If

                    .Properties.DataSource = dtnew
                    .Properties.DisplayMember = dtnew.Columns("year").ToString
                    .Properties.ValueMember = dtnew.Columns("year").ToString
                    .EditValue = dtnew.Rows(0).Item("year")
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 8
                    .Properties.TextEditStyle = TextEditStyles.Standard

                End With
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub

    Public Sub init_le(ByVal le_object As DevExpress.XtraEditors.LookUpEdit, ByVal tipe As String, ByVal par_en_id As String, ByVal par_filter As String)
        Dim dt2 As New DataTable
        Try
            If tipe = "bk_mstr" Then
                sSQL = "select bk_id, bk_code, bk_name from bk_mstr where bk_active ~~* 'Y'" & _
                           " and bk_dom_id = " & master_new.ClsVar.sdom_id.ToString & _
                           " and bk_en_id in (0," & par_en_id.ToString & ")" & par_filter & _
                           " order by bk_name "

                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("bk_id", "ID", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("bk_code", "Code", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("bk_name", "Description", 20))
                    End If
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("bk_name").ToString
                    .Properties.ValueMember = dt2.Columns("bk_id").ToString
                    .ItemIndex = 0
                    If dt2.Rows.Count > 0 Then
                        .EditValue = dt2.Rows(0).Item("bk_id")
                    End If
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                    .Properties.BestFit()
                    .Properties.DropDownRows = 8
                    .Properties.PopupWidth = 300
                End With
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Public Sub init_le(ByVal le_object As DevExpress.XtraEditors.LookUpEdit, ByVal tipe As String, ByVal par_en_id As String)
        Dim dt2 As New DataTable
        Try
            If tipe = "cus_mstr" Then
                ssql = "select ptnr_id, ptnr_name, ptnr_ac_ar_id, ptnr_code from ptnr_mstr where ptnr_active ~~* 'Y'" + _
                           " and ptnr_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                           " and ptnr_en_id in (0," + par_en_id + ")" + _
                           " and ptnr_is_cust ~~* 'Y' " + _
                           " order by ptnr_name "

                dt2 = GetTableData(ssql)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("ptnr_id", "ID", 10))
                        .Properties.Columns.Add(New LookUpColumnInfo("ptnr_code", "Code", 15))
                        .Properties.Columns.Add(New LookUpColumnInfo("ptnr_name", "Name", 25))
                    End If

                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("ptnr_name").ToString
                    .Properties.ValueMember = dt2.Columns("ptnr_id").ToString
                    If dt2.Rows.Count > 0 Then
                        .EditValue = dt2.Rows(0).Item("ptnr_id")
                    End If
                    .Properties.DropDownRows = 30
                    .Properties.TextEditStyle = TextEditStyles.Standard
                    .Properties.PopupWidth = 400
                End With


            ElseIf tipe = "cus_mstr_parent" Then
                Dim _en_id_coll As String = func_data.entity_parent(par_en_id)

                If func_coll.get_conf_file("partner_so_cross_entity") = "1" Then
                    sSQL = "SELECT  " _
                       & "  a.ptnr_oid, " _
                       & "  a.ptnr_dom_id, " _
                       & "  a.ptnr_en_id, " _
                       & "  a.ptnr_id, " _
                       & "  a.ptnr_code, " _
                       & "  a.ptnr_name, " _
                       & "  en_desc, " _
                       & "  ptnr_ac_ar_id, " _
                       & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                       & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                       & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                       & "FROM " _
                       & "  public.ptnr_mstr a " _
                       & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                       & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                       & "  left outer JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                       & " where  ptnr_active ~~* 'Y' " _
                       & " and ptnr_is_cust ~~* 'Y' order by ptnr_name"
                Else
                    sSQL = "SELECT  " _
                       & "  a.ptnr_oid, " _
                       & "  a.ptnr_dom_id, " _
                       & "  a.ptnr_en_id, " _
                       & "  a.ptnr_id, " _
                       & "  a.ptnr_code, " _
                       & "  a.ptnr_name, " _
                       & "  en_desc, " _
                       & "  ptnr_ac_ar_id, " _
                       & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                       & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                       & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                       & "FROM " _
                       & "  public.ptnr_mstr a " _
                       & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                       & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                       & "  left outer JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                       & " where ptnr_en_id  in (" + _en_id_coll + ")" _
                       & " and ptnr_active ~~* 'Y' " _
                       & " and ptnr_is_cust ~~* 'Y' order by ptnr_name"
                End If


                dt2 = GetTableData(ssql)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("ptnr_id", "ID", 10))
                        .Properties.Columns.Add(New LookUpColumnInfo("ptnr_code", "Code", 15))
                        .Properties.Columns.Add(New LookUpColumnInfo("ptnr_name", "Name", 25))
                        .Properties.Columns.Add(New LookUpColumnInfo("en_desc", "Entity", 25))
                    End If
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("ptnr_name").ToString
                    .Properties.ValueMember = dt2.Columns("ptnr_id").ToString
                    If dt2.Rows.Count > 0 Then
                        .EditValue = dt2.Rows(0).Item("ptnr_id")
                    End If

                    .Properties.DropDownRows = 30
                    .Properties.TextEditStyle = TextEditStyles.Standard
                    .Properties.PopupWidth = 400
                End With

            ElseIf tipe = "loc_mstr" Then
                ssql = "select loc_id,loc_code, loc_desc, code_name from loc_mstr" _
                            & " inner join code_mstr on code_id = loc_type " _
                            & " where loc_dom_id = " & master_new.ClsVar.sdom_id _
                            & " and loc_en_id in (0," + par_en_id.ToString & ") and loc_active ~~* 'y' order by loc_desc"

                dt2 = GetTableData(ssql)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("loc_id", "ID", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("loc_code", "Code", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("loc_desc", "Description", 20))
                    End If
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("loc_desc").ToString
                    .Properties.ValueMember = dt2.Columns("loc_id").ToString
                    If dt2.Rows.Count > 0 Then
                        .EditValue = dt2.Rows(0).Item("loc_id")
                    End If
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 30
                    .Properties.PopupWidth = 300
                End With
            ElseIf tipe = "dpt_mstr" Then
                ssql = "select dpt_id,dpt_code, dpt_desc from dpt_mstr WHERE dpt_active ~~* 'Y'" _
                             & " AND dpt_en_id in(0," & par_en_id + ")" _
                             & " AND dpt_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by dpt_code desc, dpt_desc "

                dt2 = GetTableData(ssql)

                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("dpt_id", "ID", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("dpt_code", "Code", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("dpt_desc", "Description", 20))
                    End If
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("dpt_desc").ToString
                    .Properties.ValueMember = dt2.Columns("dpt_id").ToString
                    If dt2.Rows.Count > 0 Then
                        .EditValue = dt2.Rows(0).Item("dpt_id")
                    End If
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 8
                    .Properties.PopupWidth = 300

                End With


            ElseIf tipe = "wc_mstr" Then
                ssql = "select wc_id,wc_code, wc_desc, wc_name from wc_mstr WHERE " _
                             & " wc_en_id in(0," & par_en_id + ")" _
                             & " AND wc_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by wc_code desc, wc_name, wc_desc "

                dt2 = GetTableData(ssql)

                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("wc_id", "ID", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("wc_code", "Code", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("wc_desc", "Description", 20))
                    End If
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("wc_desc").ToString
                    .Properties.ValueMember = dt2.Columns("wc_id").ToString
                    If dt2.Rows.Count > 0 Then
                        .EditValue = dt2.Rows(0).Item("wc_id")
                    End If
                    '.ItemIndex = 0
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 8
                    .Properties.PopupWidth = 300

                End With

            ElseIf tipe = "mch_mstr" Then
                ssql = "select mch_id, mch_code, mch_desc, mch_name from mch_mstr WHERE mch_active ~~* 'Y'" _
                             & " AND mch_en_id in(0," & par_en_id + ")" _
                             & " AND mch_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by mch_code desc, mch_name, mch_desc "

                dt2 = GetTableData(ssql)

                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("mch_id", "ID", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("mch_code", "Code", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("mch_desc", "Description", 20))
                    End If
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("mch_desc").ToString
                    .Properties.ValueMember = dt2.Columns("mch_id").ToString
                    If dt2.Rows.Count > 0 Then
                        .EditValue = dt2.Rows(0).Item("mch_id")
                    End If
                    '.ItemIndex = 0
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 8
                    .Properties.PopupWidth = 300

                End With
            ElseIf tipe = "prj_type_id" Then
                sSQL = "select code_id ,code_name  from code_mstr where code_field='prj_type_id' and code_en_id=" & par_en_id _
                    & " order by code_id"

                dt2 = GetTableData(sSQL)
                With le_object 'emp_area_id
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("code_name").ToString
                    .Properties.ValueMember = dt2.Columns("code_id").ToString

                    If dt2.Rows.Count > 0 Then
                        .EditValue = dt2.Rows(0).Item("code_id")
                    End If
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                    .Properties.BestFit()
                    .Properties.DropDownRows = 6
                    .Properties.PopupWidth = 300
                End With
            ElseIf tipe = "bk_mstr" Then
                sSQL = "select bk_id, bk_code, bk_name from bk_mstr where bk_active ~~* 'Y'" + _
                           " and bk_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                           " and bk_en_id in (0," + par_en_id.ToString + ")" + _
                           " order by bk_name "

                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("bk_id", "ID", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("bk_code", "Code", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("bk_name", "Description", 20))
                    End If
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("bk_name").ToString
                    .Properties.ValueMember = dt2.Columns("bk_id").ToString

                    If dt2.Rows.Count > 0 Then
                        .EditValue = dt2.Rows(0).Item("bk_id")
                    End If
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                    .Properties.BestFit()
                    .Properties.DropDownRows = 8
                    .Properties.PopupWidth = 300
                End With
            ElseIf tipe = "cost_set" Then
                sSQL = "SELECT " _
                    & "  cs_id, " _
                    & "  cs_name, " _
                    & "  cs_type, " _
                    & "  cs_methode, " _
                    & "  cs_is_default " _
                    & "FROM  " _
                    & "  public.cs_mstr  " _
                    & "WHERE " _
                    & "cs_en_id in (0, " & par_en_id.ToString & ") " _
                    & "ORDER BY cs_id"

                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("cs_id", "ID", 10))
                        .Properties.Columns.Add(New LookUpColumnInfo("cs_name", "Name", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("cs_type", "Type", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("cs_methode", "Methode", 20))
                        .Properties.Columns.Add(New LookUpColumnInfo("cs_is_default", "Is Default", 20))
                    End If
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("cs_name").ToString
                    .Properties.ValueMember = dt2.Columns("cs_id").ToString
                    .ItemIndex = 0
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 4

                End With

            ElseIf tipe = "site" Then
                sSQL = "select si_id, si_desc from si_mstr where si_active = 'Y' " & _
                               " and si_en_id in (0," & par_en_id & ")" & _
                               " and si_dom_id = " & master_new.ClsVar.sdom_id & _
                               " order by si_desc "

                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("si_id", "ID", 10))
                        .Properties.Columns.Add(New LookUpColumnInfo("si_desc", "Site", 20))
                    End If
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("si_desc").ToString
                    .Properties.ValueMember = dt2.Columns("si_id").ToString
                    .ItemIndex = 0
                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 4
                    '.Properties.PopupWidth = 300
                End With

            ElseIf tipe = "down_reason" Then
                sSQL = "select code_id,  code_name " & _
                     " from code_mstr " & _
                    " where code_field ~~* 'down_reason'" _
                    & " and code_active ~~* 'Y'" _
                    & " and code_en_id in (0," & par_en_id.ToString & ") " _
                    & " union " _
                    & " select null as code_id, '-' as code_name " _
                    & " order by code_name"


                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("code_id", "ID", 10))
                        .Properties.Columns.Add(New LookUpColumnInfo("code_name", "Reason", 10))
                    End If
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("code_name").ToString
                    .Properties.ValueMember = dt2.Columns("code_id").ToString

                    If dt2.Rows.Count <> 0 Then
                        .EditValue = dt2.Rows(0).Item(0)
                    End If

                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 8

                End With
            ElseIf tipe = "qc_out" Then
                sSQL = "SELECT  " _
                    & "  a.qc_oid, " _
                    & "  a.qc_id, " _
                    & "  a.qc_desc, " _
                    & "  a.qc_add_by, " _
                    & "  a.qc_add_date, " _
                    & "  a.qc_upd_by, " _
                    & "  a.qc_upd_date, " _
                    & "  a.qc_active, " _
                    & "  a.qc_type " _
                    & "FROM " _
                    & "  public.qc_reason_mstr a " _
                    & "WHERE " _
                    & "  a.qc_type = 'O' AND  " _
                    & "  a.qc_active = 'Y' " _
                    & "ORDER BY " _
                    & "  a.qc_desc"


                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("qc_id", "ID", 10))
                        .Properties.Columns.Add(New LookUpColumnInfo("qc_desc", "Reason", 10))
                    End If
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("qc_desc").ToString
                    .Properties.ValueMember = dt2.Columns("qc_id").ToString

                    If dt2.Rows.Count <> 0 Then
                        .EditValue = dt2.Rows(0).Item(0)
                    End If

                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 8

                End With
            ElseIf tipe = "qc_in" Then
                sSQL = "SELECT  " _
                    & "  a.qc_oid, " _
                    & "  a.qc_id, " _
                    & "  a.qc_desc, " _
                    & "  a.qc_add_by, " _
                    & "  a.qc_add_date, " _
                    & "  a.qc_upd_by, " _
                    & "  a.qc_upd_date, " _
                    & "  a.qc_active, " _
                    & "  a.qc_type " _
                    & "FROM " _
                    & "  public.qc_reason_mstr a " _
                    & "WHERE " _
                    & "  a.qc_type = 'I' AND  " _
                    & "  a.qc_active = 'Y' " _
                    & "ORDER BY " _
                    & "  a.qc_desc"

                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("qc_id", "ID", 10))
                        .Properties.Columns.Add(New LookUpColumnInfo("qc_desc", "Reason", 10))
                    End If
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("qc_desc").ToString
                    .Properties.ValueMember = dt2.Columns("qc_id").ToString

                    If dt2.Rows.Count <> 0 Then
                        .EditValue = dt2.Rows(0).Item(0)
                    End If

                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 8

                End With
            ElseIf tipe = "activity" Then

                sSQL = "SELECT  " _
                & "  a.lbrfa_id, " _
                & "  a.lbrfa_desc " _
                & "FROM " _
                & "  public.lbrfa_activity a " _
                & "WHERE " _
                & "  a.lbrfa_active = 'Y' " _
                & "ORDER BY " _
                & "  a.lbrfa_desc"


                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("lbrfa_id", "ID", 10))
                        .Properties.Columns.Add(New LookUpColumnInfo("lbrfa_desc", "Reason", 10))
                    End If
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("lbrfa_desc").ToString
                    .Properties.ValueMember = dt2.Columns("lbrfa_id").ToString

                    If dt2.Rows.Count <> 0 Then
                        .EditValue = dt2.Rows(0).Item(0)
                    End If

                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 8

                End With
            ElseIf tipe = "shift_mstr" Then
                sSQL = "select code_id,  code_name " & _
                     " from code_mstr " & _
                    " where code_field ~~* 'shift_mstr'" _
                    & " and code_active ~~* 'Y'" _
                    & " union " _
                    & " select null as code_id, '-' as code_name " _
                    & " order by code_name"

                dt2 = GetTableData(sSQL)
                With le_object
                    If .Properties.Columns.VisibleCount = 0 Then
                        .Properties.Columns.Add(New LookUpColumnInfo("code_id", "ID", 10))
                        .Properties.Columns.Add(New LookUpColumnInfo("code_name", "Shift", 10))
                    End If
                    .Properties.DataSource = dt2
                    .Properties.DisplayMember = dt2.Columns("code_name").ToString
                    .Properties.ValueMember = dt2.Columns("code_id").ToString

                    If dt2.Rows.Count <> 0 Then
                        .EditValue = dt2.Rows(0).Item(0)
                    End If

                    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                    .Properties.BestFit()
                    .Properties.DropDownRows = 8

                End With
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub

    Public Function SetStringJson(ByVal Str As Object) As String

        SetStringJson = ""

        If Str Is System.DBNull.Value Then
            Return SetStringJson
        End If
        SetStringJson = CType(Str, String)

        SetStringJson = SetStringJson.Replace("'", "").Replace(";", "").Replace(":", "").Replace("#", "") _
        .Replace("?", "").Replace("'", "").Replace("""", "").Replace(" ", "%20").Replace(Convert.ToChar(10), "\n") _
        .Replace("&", "%26").Replace("/", "%2F").Replace("+", "%2B").Replace(vbNewLine, "\n").Replace(vbCr, "\n")

        Return SetStringJson

    End Function

    Public Function init_le_repo(ByVal par_tipe As String) As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
        Dim sSQL As String
        Try

            Dim RepositoryItemLookUpEdit1 As New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit

            If par_tipe = "encd_code" Then
                sSQL = "SELECT  " _
                    & "  a.encd_code, " _
                    & "  a.encd_desc " _
                    & "FROM " _
                    & "  public.encd_code a " _
                    & "ORDER BY " _
                    & "  a.encd_code"


                Dim dt As New DataTable
                dt = GetTableData(sSQL)

                With RepositoryItemLookUpEdit1

                    If .Columns.VisibleCount = 0 Then
                        .Columns.Add(New LookUpColumnInfo("encd_code", "Code", 20))
                        .Columns.Add(New LookUpColumnInfo("encd_desc", "Description", 20))
                    End If

                    .DataSource = dt
                    .DisplayMember = dt.Columns("encd_desc").ToString
                    .ValueMember = dt.Columns("encd_code").ToString

                    .BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                    .BestFit()
                    .DropDownRows = 12
                    .PopupWidth = 300

                End With

            ElseIf par_tipe = "rosg_desc" Then
                sSQL = "SELECT  " _
                    & "  a.rosg_code, " _
                    & "  a.rosg_desc, " _
                    & "  a.rosg_default_sign " _
                    & "FROM " _
                    & "  public.rosg_group a " _
                    & "ORDER BY " _
                    & "  a.rosg_code"


                Dim dt As New DataTable
                dt = GetTableData(sSQL)

                With RepositoryItemLookUpEdit1

                    If .Columns.VisibleCount = 0 Then
                        .Columns.Add(New LookUpColumnInfo("rosg_code", "Code", 20))
                        .Columns.Add(New LookUpColumnInfo("rosg_desc", "Description", 20))
                    End If

                    .DataSource = dt
                    .DisplayMember = dt.Columns("rosg_desc").ToString
                    .ValueMember = dt.Columns("rosg_code").ToString

                    .BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                    .BestFit()
                    .DropDownRows = 12
                    .PopupWidth = 300

                End With
            ElseIf par_tipe = "rost_desc" Then
                sSQL = "SELECT  " _
                    & "  a.rost_code, " _
                    & "  a.rost_desc " _
                    & "FROM " _
                    & "  public.rost_trans_line a " _
                    & "ORDER BY " _
                    & "  a.rost_code"



                Dim dt As New DataTable
                dt = GetTableData(sSQL)

                With RepositoryItemLookUpEdit1

                    If .Columns.VisibleCount = 0 Then
                        .Columns.Add(New LookUpColumnInfo("rost_code", "Code", 20))
                        .Columns.Add(New LookUpColumnInfo("rost_desc", "Description", 20))
                    End If

                    .DataSource = dt
                    .DisplayMember = dt.Columns("rost_desc").ToString
                    .ValueMember = dt.Columns("rost_code").ToString

                    .BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                    .BestFit()
                    .DropDownRows = 12
                    .PopupWidth = 300

                End With
            ElseIf par_tipe = "opsi_detail_cetak" Then
                sSQL = "SELECT  " _
                    & "  a.kode, " _
                    & "  a.nilai " _
                    & "FROM " _
                    & "  public.cetak_opsi_detail_cetak a " _
                    & "ORDER BY " _
                    & "  a.kode"

                Dim dt As New DataTable
                dt = GetTableData(sSQL)

                With RepositoryItemLookUpEdit1

                    If .Columns.VisibleCount = 0 Then
                        .Columns.Add(New LookUpColumnInfo("kode", "Kode", 20))
                        .Columns.Add(New LookUpColumnInfo("nilai", "Nilai", 20))
                    End If

                    .DataSource = dt
                    .DisplayMember = dt.Columns("kode").ToString
                    .ValueMember = dt.Columns("kode").ToString

                    .BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                    .BestFit()
                    .DropDownRows = 8
                    .PopupWidth = 300

                End With
            ElseIf par_tipe = "opsi_detail_pasca" Then
                sSQL = "SELECT  " _
                    & "  a.kode, " _
                    & "  a.nilai " _
                    & "FROM " _
                    & "  public.cetak_opsi_pasca a " _
                    & "ORDER BY " _
                    & "  a.kode"

                Dim dt As New DataTable
                dt = GetTableData(sSQL)

                With RepositoryItemLookUpEdit1

                    If .Columns.VisibleCount = 0 Then
                        .Columns.Add(New LookUpColumnInfo("kode", "Kode", 20))
                        .Columns.Add(New LookUpColumnInfo("nilai", "Nilai", 20))
                    End If

                    .DataSource = dt
                    .DisplayMember = dt.Columns("kode").ToString
                    .ValueMember = dt.Columns("kode").ToString

                    .BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                    .BestFit()
                    .DropDownRows = 8
                    .PopupWidth = 300

                End With
            ElseIf par_tipe = "ya_tidak" Then
                sSQL = "select 1 as id,'Ya' as value union select 0 as id,'Tidak' as value order by id desc"

                Dim dt As New DataTable
                dt = GetTableData(sSQL)

                With RepositoryItemLookUpEdit1

                    If .Columns.VisibleCount = 0 Then
                        .Columns.Add(New LookUpColumnInfo("id", "Kode", 20))
                        .Columns.Add(New LookUpColumnInfo("value", "Nilai", 20))
                    End If

                    .DataSource = dt
                    .DisplayMember = dt.Columns("value").ToString
                    .ValueMember = dt.Columns("id").ToString

                    .BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                    .BestFit()
                    .DropDownRows = 8
                    .PopupWidth = 300

                End With

            ElseIf par_tipe = "qc_type" Then
                sSQL = "select 'I' as id,'QC IN' as value union select 'O' as id,'QC OUT' as value order by id desc"

                Dim dt As New DataTable
                dt = GetTableData(sSQL)

                With RepositoryItemLookUpEdit1

                    If .Columns.VisibleCount = 0 Then
                        .Columns.Add(New LookUpColumnInfo("id", "Kode", 20))
                        .Columns.Add(New LookUpColumnInfo("value", "Nilai", 20))
                    End If

                    .DataSource = dt
                    .DisplayMember = dt.Columns("value").ToString
                    .ValueMember = dt.Columns("id").ToString

                    .BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                    .BestFit()
                    .DropDownRows = 8
                    .PopupWidth = 300

                End With

            ElseIf par_tipe = "city" Then
                sSQL = "SELECT  " _
                   & "  a.city_name " _
                   & "FROM " _
                   & "  city_mstr a " _
                   & "   " _
                   & "ORDER BY " _
                   & "  a.city_name"

                Dim dt As New DataTable
                dt = master_new.PGSqlConn.GetTableData(sSQL)

                With RepositoryItemLookUpEdit1

                    If .Columns.VisibleCount = 0 Then
                        .Columns.Add(New LookUpColumnInfo("city_name", "City", 20))

                    End If

                    .DataSource = dt
                    .DisplayMember = dt.Columns("city_name").ToString
                    .ValueMember = dt.Columns("city_name").ToString

                    .BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                    .BestFit()
                    .DropDownRows = 12
                    .PopupWidth = 300

                End With
            End If



            Return RepositoryItemLookUpEdit1
        Catch ex As Exception
            Pesan(Err)
            Return Nothing
        End Try
    End Function
    Public Function init_le_repo(ByVal par_tipe As String, ByVal par_filter As String) As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
        Dim sSQL As String
        Try

            Dim RepositoryItemLookUpEdit1 As New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit

            If par_tipe = "opsi_detail_pasca" Then
                sSQL = "SELECT  " _
                    & "  a.kode, " _
                    & "  a.nilai " _
                    & "FROM " _
                    & "  public.cetak_opsi_pasca a " _
                    & " where " & par_filter _
                    & " ORDER BY " _
                    & "  a.kode"

                Dim dt As New DataTable
                dt = GetTableData(sSQL)

                With RepositoryItemLookUpEdit1

                    If .Columns.VisibleCount = 0 Then
                        .Columns.Add(New LookUpColumnInfo("kode", "Kode", 20))
                        .Columns.Add(New LookUpColumnInfo("nilai", "Nilai", 20))
                    End If

                    .DataSource = dt
                    .DisplayMember = dt.Columns("kode").ToString
                    .ValueMember = dt.Columns("kode").ToString

                    .BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                    .BestFit()
                    .DropDownRows = 8
                    .PopupWidth = 300

                End With
            End If

            Return RepositoryItemLookUpEdit1
        Catch ex As Exception
            Pesan(Err)
            Return Nothing
        End Try
    End Function
    Public Function init_te_repo(ByVal par_tipe As String) As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
        Try

            Dim RepositoryItemTextEdit1 As New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit

            If par_tipe = "percent" Then
                With RepositoryItemTextEdit1
                    .Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
                    .Mask.EditMask = "p"
                    .Mask.UseMaskAsDisplayFormat = True

                End With

            End If

            Return RepositoryItemTextEdit1
        Catch ex As Exception
            Pesan(Err)
            Return Nothing
        End Try
    End Function

    Public Function get_cost(ByVal _pt_id As String, ByVal _si_id As String) As Double

        Try
            sSQL = "select invct_cost from invct_table where invct_pt_id=" _
                & _pt_id & " and  invct_si_id=" & _si_id
            Dim dt As New DataTable
            dt = GetTableData(sSQL)
            If dt.Rows.Count = 0 Then
                get_cost = 0
            ElseIf dt.Rows(0).Item(0) Is System.DBNull.Value Then
                get_cost = 0
            Else
                get_cost = dt.Rows(0).Item(0)
            End If

        Catch ex As Exception
            Pesan(Err)
            Return 0
        End Try
    End Function

    Public Function get_id(ByVal par_table As String, ByVal par_field_name As String, ByVal par_name_value As String, _
                           ByVal par_field_id As String, Optional ByVal par_code_field As String = "") As String

        Dim hasil As String = ""
        Try
            sSQL = "select " & par_field_id & " from " & par_table & "  where " & _
            par_field_name & "=" & SetSetring(par_name_value) & IIf(par_code_field.Length > 0, " and code_field=" & SetSetring(par_code_field), "")

            Dim dt As New DataTable
            dt = GetTableData(sSQL)
            hasil = ""
            For Each dr As DataRow In dt.Rows
                hasil = dr(0).ToString
                'Exit Function
            Next
            Return hasil
        Catch ex As Exception
            Pesan(Err)
            Return ""
        End Try
    End Function

    Public Function GetNewIDSvrCode(ByVal par_en_code As String, ByVal par_table As String, ByVal par_field As String) As Integer
        Try
            Dim ssql As String
            Dim hasil As Integer

            ssql = "select coalesce(max(cast(substring(cast(" & par_field _
                & " as varchar),5,100) as integer)),0) as max_col  from " & par_table & _
                " where substring(cast(" & par_field & " as varchar),1,4)='" & par_en_code _
                & "" & master_new.ClsVar.sServerCode & "'"

            hasil = CInt(par_en_code & master_new.ClsVar.sServerCode & (GetRowInfo(ssql)(0) + 1))

            Return hasil

        Catch ex As Exception
            Pesan(Err)
            Return 0
        End Try
    End Function

    Public Function update_invc_mstr(ByVal par_ssqls As ArrayList, ByVal par_en_id As Integer, ByVal par_si_id As Integer, _
                                          ByVal par_loc_id As Integer, ByVal par_pt_id As Integer, ByVal par_serial As String, _
                                          ByVal par_qty As Double) As Boolean
        Dim _invc_oid As String = ""

        Try
            sSQL = "select invc_oid from invc_mstr " + _
                " where invc_dom_id = " + master_new.ClsVar.sdom_id + _
                " and invc_en_id = " + par_en_id.ToString + _
                " and invc_si_id = " + par_si_id.ToString + _
                " and invc_loc_id = " + par_loc_id.ToString + _
                " and invc_pt_id = " + par_pt_id.ToString + _
                " and coalesce(invc_serial,'') = " + IIf(par_serial = "''", "''", SetSetring(par_serial))

            Dim dt As New DataTable
            dt = GetTableData(sSQL)

            If dt.Rows.Count > 0 Then
                _invc_oid = dt.Rows(0).Item("invc_oid").ToString
            Else
                _invc_oid = ""
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try


        If _invc_oid = "" Then
            sSQL = "INSERT INTO  " _
                 & "  public.invc_mstr " _
                 & "( " _
                 & "  invc_oid, " _
                 & "  invc_dom_id, " _
                 & "  invc_en_id, " _
                 & "  invc_si_id, " _
                 & "  invc_loc_id, " _
                 & "  invc_pt_id, " _
                 & "  invc_qty, " _
                 & "  invc_serial " _
                 & ")  " _
                 & "VALUES ( " _
                 & SetSetring(Guid.NewGuid.ToString) & ",  " _
                 & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                 & SetInteger(par_en_id) & ",  " _
                 & SetInteger(par_si_id) & ",  " _
                 & SetInteger(par_loc_id) & ",  " _
                 & SetInteger(par_pt_id) & ",  " _
                 & SetDbl(par_qty) & ",  " _
                 & SetSetring(IIf(par_serial = "''", "", par_serial)) & "  " _
                 & ")"
            par_ssqls.Add(sSQL)

        Else
            sSQL = "UPDATE  " _
                 & "  public.invc_mstr   " _
                 & "SET  " _
                 & "  invc_qty = coalesce(invc_qty,0) + " & SetDbl(par_qty) _
                 & " WHERE  " _
                 & "  invc_oid = " & SetSetring(_invc_oid) & " "

            par_ssqls.Add(sSQL)

        End If
        Return True
    End Function

    Public Function update_invh_mstr(ByVal par_ssqls As ArrayList, ByVal par_tran_id As Integer, ByVal par_seq As Integer, _
              ByVal par_en_id As Integer, ByVal par_trn_code As String, ByVal par_trn_oid As String, _
              ByVal par_desc As String, ByVal par_opn_type As String, _
              ByVal par_si_id As Integer, ByVal par_loc_id As Integer, ByVal par_pt_id As Integer, _
              ByVal par_qty As Double, ByVal par_cost As Double, ByVal par_avg_cost As Double, _
              ByVal par_serial As String, ByVal par_date As Date) As Boolean

        'Insert History Inventory
        update_invh_mstr = True
        sSQL = "INSERT INTO  " _
              & "  public.invh_mstr " _
              & "( " _
              & "  invh_oid, " _
              & "  invh_tran_id, " _
              & "  invh_seq, " _
              & "  invh_dom_id, " _
              & "  invh_en_id, " _
              & "  invh_trn_code, " _
              & "  invh_trn_oid, " _
              & "  invh_date, " _
              & "  invh_desc, " _
              & "  invh_opn_type, " _
              & "  invh_si_id, " _
              & "  invh_loc_id, " _
              & "  invh_pt_id, " _
              & "  invh_qty, " _
              & "  invh_cost, " _
              & "  invh_avg_cost, " _
              & "  invh_serial " _
              & ")  " _
              & "VALUES ( " _
              & SetSetring(Guid.NewGuid.ToString) & ",  " _
              & SetInteger(par_tran_id) & ",  " _
              & SetInteger(par_seq) & ",  " _
              & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
              & SetSetring(par_en_id) & ",  " _
              & SetSetring(par_trn_code) & ",  " _
              & SetSetring(par_trn_oid) & ",  " _
              & SetDateNTime00(par_date) & ",  " _
              & SetSetring(par_desc) & ",  " _
              & SetSetring(par_opn_type) & ",  " _
              & SetInteger(par_si_id) & ",  " _
              & SetInteger(par_loc_id) & ",  " _
              & SetInteger(par_pt_id) & ",  " _
              & SetDbl(par_qty) & ",  " _
              & SetDbl(par_cost) & ",  " _
              & SetDbl(par_avg_cost) & ",  " _
              & SetSetring(par_serial) & "  " _
              & ")"
        par_ssqls.Add(ssql)

    End Function

    Public Function export_to_excel(ByVal _sql As String) As Boolean
        Dim _file As String
        Try
            _file = AskSaveAsFile("Excel Files | *.xls")

            If _file = "" Then
                Return False
                Exit Function
            End If
            Dim dt2 As New DataTable
            dt2 = GetTableData(_sql)

            Dim grid As New System.Web.UI.WebControls.DataGrid()
            grid.HeaderStyle.Font.Bold = True
            grid.DataSource = dt2
            'grid.DataMember = Data.Stats.TableName

            grid.DataBind()



            ' render the DataGrid control to a file
            Using sw As New IO.StreamWriter(_file)
                Using hw As New Web.UI.HtmlTextWriter(sw)
                    grid.RenderControl(hw)
                End Using
            End Using
            Box("Export data sukses.")

            If ask("Do you want to open this file?", "Export To...") = True Then
                Try
                    Dim process As System.Diagnostics.Process = New System.Diagnostics.Process()
                    process.StartInfo.FileName = _file
                    process.StartInfo.Verb = "Open"
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
                    process.Start()
                Catch
                    'MessageBox.Show(Me, "Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Pesan(Err)
                End Try
            End If

            Return True
        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try
    End Function

    Public Function get_stock(ByVal par_en_id As Integer, ByVal par_si_id As Integer, _
                                          ByVal par_loc_id As Integer, ByVal par_pt_id As Integer) As Double

        Try

            sSQL = "select invc_qty from invc_mstr " + _
              " where invc_dom_id = " + master_new.ClsVar.sdom_id + _
              " and invc_en_id = " + par_en_id.ToString + _
              " and invc_si_id = " + par_si_id.ToString + _
              " and invc_loc_id = " + par_loc_id.ToString + _
              " and invc_pt_id = " + par_pt_id.ToString

            Dim dt As New DataTable
            dt = GetTableData(sSQL)

            If dt.Rows.Count = 0 Then
                get_stock = 0
            ElseIf dt.Rows(0).Item(0) Is System.DBNull.Value Then
                get_stock = 0
            Else
                get_stock = dt.Rows(0).Item(0)
            End If

        Catch ex As Exception
            Pesan(Err)
            Return 0
        End Try
    End Function

    'Public Function get_en_id_child(ByVal _par_en_id As String) As String
    Public Function get_en_id_child(ByVal _par_en_id As String) As String
        Try
            Dim _hasil As String = ""

            'If _par_en_id = 0 Then
            '    sSQL = "select en_id from en_mstr "
            'Else
            'sSQL = "select en_id from en_mstr where en_parent=" & _par_en_id
            '' End If


            'Dim dt As New DataTable
            'dt = GetTableData(sSQL)

            'Dim i As Integer = 0
            'For Each dr As DataRow In dt.Rows

            '    If i = dt.Rows.Count - 1 Then
            '        _hasil = _hasil & dr(0)
            '    Else
            '        _hasil = _hasil & dr(0) & ","
            '    End If
            '    i += 1
            'Next
            'get_en_id_child = _hasil & "," & _par_en_id


            'sSQL = "select get_all_children_array(" & _par_en_id & ")"
            sSQL = "select cast(get_all_children_array(" & _par_en_id & ") as varchar) as data"

            Dim dt As New DataTable
            dt = GetTableData(sSQL)

            For Each dr As DataRow In dt.Rows
                _hasil = dr(0).ToString.Replace("{", "").Replace("}", "")
            Next
            _hasil = _hasil & "," & _par_en_id

            Return _hasil
        Catch ex As Exception
            Pesan(Err)
            Return ""
        End Try
    End Function

    Public Function get_en_id_child(ByVal _par_en_id As String, ByVal par_constring As String) As String
        Try
            Dim _hasil As String = ""

            'If _par_en_id = 0 Then
            '    sSQL = "select en_id from en_mstr "
            'Else
            'sSQL = "select en_id from en_mstr where en_parent=" & _par_en_id
            '' End If


            'Dim dt As New DataTable
            'dt = GetTableData(sSQL)

            'Dim i As Integer = 0
            'For Each dr As DataRow In dt.Rows

            '    If i = dt.Rows.Count - 1 Then
            '        _hasil = _hasil & dr(0)
            '    Else
            '        _hasil = _hasil & dr(0) & ","
            '    End If
            '    i += 1
            'Next
            'get_en_id_child = _hasil & "," & _par_en_id


            sSQL = "select get_all_children_array(" & _par_en_id & ")"

            Dim dt As New DataTable
            dt = GetTableData(sSQL, "", par_constring)

            For Each dr As DataRow In dt.Rows
                _hasil = dr(0).ToString.Replace("{", "").Replace("}", "")
            Next
            _hasil = _hasil & "," & _par_en_id

            Return _hasil
        Catch ex As Exception
            Pesan(Err)
            Return ""
        End Try
    End Function
    Public Function sent_sms(ByVal _phone As String, ByVal _message As String) As Boolean
        Dim i As String = ""
        Dim a() As String
        Dim j As Integer
        Dim sSqls As New ArrayList
        Dim id As Integer
        Dim ref As String
        Dim KeyGen As master_new.RandomKeyGenerator
        Try

            KeyGen = New master_new.RandomKeyGenerator
            KeyGen.KeyLetters = "abcdef"
            KeyGen.KeyNumbers = "0123456789"
            KeyGen.KeyChars = 2

            Dim Teks As String
            Dim Perulangan As Integer

            Teks = ""
            If Len(_message) = 0 Then
                Perulangan = 0
            ElseIf Len(_message) > 160 Then
                Perulangan = ((Len(_message) - (Len(_message) Mod 153)) / 153) + 1
            ElseIf Len(_message) <= 160 Then
                Perulangan = 1
            End If

            id = CekID()

            'sSQL = "select notification_phone from tphone where notification_status='" & _dest & "'"
            'Dim dt As New DataTable
            'dt = GetTableData(sSQL)

            'If dt.Rows.Count = 0 Then
            '    Box("Sending phone empty")
            '    Return False
            '    Exit Function
            'Else
            '    For Each dr As DataRow In dt.Rows
            '        i = i & dr(0) & ";"
            '    Next
            '    i = Microsoft.VisualBasic.Left(i, Len(i) - 1)
            'End If

            i = _phone & ";"

            a = i.Split(";")

            For j = 0 To a.GetUpperBound(0)
                If Len(RmNomor(a(j), True)) > 0 Then

                    id = id + 1
                    ref = KeyGen.Generate()

                    If Perulangan = 1 Then
                        Teks = _message

                        sSQL = "INSERT INTO `outbox` (`UpdatedInDB` ,`InsertIntoDB` ,`SendingDateTime` ,`Text` ,`DestinationNumber` , " _
                              & "`Coding` ,`UDH` ,`Class` ,`TextDecoded` ,`ID` ,`MultiPart` ,`RelativeValidity` ,`SenderID` , " _
                              & "`SendingTimeOut` ,`DeliveryReport` ,`CreatorID`) " _
                              & "VALUES (NOW() , NOW(), " & master_new.ModFunctionMy.SetDateNTime(CekTanggal) & ", NULL , '" & RmNomor((a(j)), True) & "', " _
                              & " 'Default_No_Compression', NULL , '-1', " & SetSetring(Teks) & ", " & id & " , " _
                              & "'false', '-1', NULL , NOW(), 'yes', '" & "admin" & "') "
                        sSqls.Add(sSQL)


                    ElseIf Perulangan > 1 Then
                        For n As Integer = 0 To Perulangan - 1
                            If n = Perulangan Then
                                Teks = Mid(_message, (n * 153) + 1, Len(_message) Mod 153)
                            Else
                                Teks = Mid(_message, (n * 153) + 1, 153)
                            End If

                            If n = 0 Then
                                sSQL = "INSERT INTO `outbox` (`UpdatedInDB` ,`InsertIntoDB` ,`SendingDateTime` ,`Text` ,`DestinationNumber` , " _
                                      & "`Coding` ,`UDH` ,`Class` ,`TextDecoded` ,`ID` ,`MultiPart` ,`RelativeValidity` ,`SenderID` , " _
                                      & "`SendingTimeOut` ,`DeliveryReport` ,`CreatorID`) " _
                                      & "VALUES (NOW() , NOW(), " & master_new.ModFunctionMy.SetDateNTime(CekTanggal) & ", NULL , '" & RmNomor((a(j)), True) & "', " _
                                      & " 'Default_No_Compression', '050003" & ref & Format(Perulangan, "00") & Format(n + 1, "00") & "' , '-1', " & SetSetring(Teks) & ", " & id & " , " _
                                      & "'true', '-1', NULL , NOW(), 'yes', '" & "admin" & "') "

                                sSqls.Add(sSQL)


                            Else
                                sSQL = "INSERT INTO outbox_multipart " _
                                & "(SequencePosition,UDH,Class,TextDecoded,ID,Coding) " _
                                & "VALUES (" & n + 1 & ",'050003" & ref & Format(Perulangan, "00") & Format(n + 1, "00") & "','-1', " _
                                & SetSetring(Teks) & ", " _
                                & id & ",'Default_No_Compression') "

                                sSqls.Add(sSQL)

                            End If
                        Next
                    End If
                End If
            Next


            master_new.DBMysql.DbRunTran(sSqls)
            Return True
        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try
    End Function

    Public Function sent_notification(ByVal _source As String, ByVal _dest As String, ByVal _message As String) As Boolean
        Dim i As String = ""
        Dim a() As String
        Dim j As Integer
        Dim sSqls As New ArrayList
        Dim id As Integer
        Dim ref As String
        Dim KeyGen As master_new.RandomKeyGenerator
        Try

            KeyGen = New master_new.RandomKeyGenerator
            KeyGen.KeyLetters = "abcdef"
            KeyGen.KeyNumbers = "0123456789"
            KeyGen.KeyChars = 2

            Dim Teks As String
            Dim Perulangan As Integer

            Teks = ""
            If Len(_message) = 0 Then
                Perulangan = 0
            ElseIf Len(_message) > 160 Then
                Perulangan = ((Len(_message) - (Len(_message) Mod 153)) / 153) + 1
            ElseIf Len(_message) <= 160 Then
                Perulangan = 1
            End If

            id = CekID()

            sSQL = "select notification_phone from tphone where notification_status='" & _dest & "'"
            Dim dt As New DataTable
            dt = GetTableData(sSQL)

            If dt.Rows.Count = 0 Then
                Box("Sending phone empty")
                Return False
                Exit Function
            Else
                For Each dr As DataRow In dt.Rows
                    i = i & dr(0) & ";"
                Next
                i = Microsoft.VisualBasic.Left(i, Len(i) - 1)
            End If


            a = i.Split(";")

            For j = 0 To a.GetUpperBound(0)
                If Len(RmNomor(a(j), True)) > 0 Then

                    id = id + 1
                    ref = KeyGen.Generate()

                    If Perulangan = 1 Then
                        Teks = _message

                        sSQL = "INSERT INTO `outbox` (`UpdatedInDB` ,`InsertIntoDB` ,`SendingDateTime` ,`Text` ,`DestinationNumber` , " _
                              & "`Coding` ,`UDH` ,`Class` ,`TextDecoded` ,`ID` ,`MultiPart` ,`RelativeValidity` ,`SenderID` , " _
                              & "`SendingTimeOut` ,`DeliveryReport` ,`CreatorID`) " _
                              & "VALUES (NOW() , NOW(), " & master_new.ModFunctionMy.SetDateNTime(CekTanggal) & ", NULL , '" & RmNomor((a(j)), True) & "', " _
                              & " 'Default_No_Compression', NULL , '-1', " & SetSetring(Teks) & ", " & id & " , " _
                              & "'false', '-1', NULL , NOW(), 'yes', '" & "admin" & "') "
                        sSqls.Add(sSQL)


                    ElseIf Perulangan > 1 Then
                        For n As Integer = 0 To Perulangan - 1
                            If n = Perulangan Then
                                Teks = Mid(_message, (n * 153) + 1, Len(_message) Mod 153)
                            Else
                                Teks = Mid(_message, (n * 153) + 1, 153)
                            End If

                            If n = 0 Then
                                sSQL = "INSERT INTO `outbox` (`UpdatedInDB` ,`InsertIntoDB` ,`SendingDateTime` ,`Text` ,`DestinationNumber` , " _
                                      & "`Coding` ,`UDH` ,`Class` ,`TextDecoded` ,`ID` ,`MultiPart` ,`RelativeValidity` ,`SenderID` , " _
                                      & "`SendingTimeOut` ,`DeliveryReport` ,`CreatorID`) " _
                                      & "VALUES (NOW() , NOW(), " & master_new.ModFunctionMy.SetDateNTime(CekTanggal) & ", NULL , '" & RmNomor((a(j)), True) & "', " _
                                      & " 'Default_No_Compression', '050003" & ref & Format(Perulangan, "00") & Format(n + 1, "00") & "' , '-1', " & SetSetring(Teks) & ", " & id & " , " _
                                      & "'true', '-1', NULL , NOW(), 'yes', '" & "admin" & "') "

                                sSqls.Add(sSQL)


                            Else
                                sSQL = "INSERT INTO outbox_multipart " _
                                & "(SequencePosition,UDH,Class,TextDecoded,ID,Coding) " _
                                & "VALUES (" & n + 1 & ",'050003" & ref & Format(Perulangan, "00") & Format(n + 1, "00") & "','-1', " _
                                & SetSetring(Teks) & ", " _
                                & id & ",'Default_No_Compression') "

                                sSqls.Add(sSQL)

                            End If
                        Next
                    End If
                End If
            Next


            master_new.DBMysql.DbRunTran(sSqls)
            Return True
        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try
    End Function

    Function CekID() As Integer

        Dim ssql As String
        ssql = "SELECT max(ID) AS ID FROM outbox"
        Dim dr As DataRow
        dr = master_new.DBMysql.GetRowInfo(ssql)

        If dr(0) Is System.DBNull.Value Then
            ssql = "SELECT max(sentitems.ID) AS ID FROM sentitems"
            Dim dr2 As DataRow
            dr2 = master_new.DBMysql.GetRowInfo(ssql)
            CekID = dr2(0)

        Else
            ssql = "SELECT max(sentitems.ID) AS ID FROM sentitems"
            Dim dr2 As DataRow
            dr2 = master_new.DBMysql.GetRowInfo(ssql)
            If dr(0) > dr2(0) Then
                CekID = dr(0)
            Else
                CekID = dr2(0)
            End If

        End If

    End Function

    Function RmNomor(ByVal Item As String, ByVal ModeKodeNegara As Boolean) As String
        Dim Temp1, Temp2 As String
        Try
            Temp1 = Replace(Item, "-", "")
            Temp2 = Replace(Temp1, " ", "")

            If ModeKodeNegara = True Then
                If Microsoft.VisualBasic.Left(Temp2, 1) = "0" Then
                    RmNomor = "+62" & Microsoft.VisualBasic.Right(Temp2, Len(Temp2) - 1)
                ElseIf Microsoft.VisualBasic.Left(Temp2, 1) = "+" Then
                    RmNomor = Temp2
                Else
                    RmNomor = ""
                End If
            Else
                RmNomor = Temp2
            End If

        Catch ex As Exception
            RmNomor = ""
        End Try
    End Function

    Function hpp_avg(ByVal _hpp_old As Double, ByVal _qty_old As Double, ByVal _hpp_new As Double, ByVal _qty_new As Double) As Double
        Try
            Dim _hasil As Double = 0
            _hasil = ((_hpp_old * _qty_old) + (_hpp_new * _qty_new)) / (_qty_old + _qty_new)

            Return _hasil
        Catch ex As Exception
            Pesan(Err)
            Return 0
        End Try
    End Function

    Function update_rec(ByVal par_ssqls As ArrayList, ByVal par_en_id As String, ByVal par_bk_id As String, ByVal par_cu_id As String, _
                        ByVal par_exc_rate As String, ByVal par_amount As Double, ByVal par_date As String, _
                        ByVal par_reff As String, ByVal par_remarks As String, ByVal par_type As String) As Boolean
        update_rec = True
        Try
            sSQL = "INSERT INTO  " _
                & "  public.reconciliation_det " _
                & "( " _
                & "  recd_oid, " _
                & "  recd_dom_id, " _
                & "  recd_en_id, " _
                & "  recd_bk_id, " _
                & "  recd_cu_id, " _
                & "  recd_ex_rate, " _
                & "  recd_add_by, " _
                & "  recd_add_date, " _
                & "  recd_amount, " _
                & "  recd_date, " _
                & "  recd_reff, " _
                & "  recd_remarks, " _
                & "  recd_type, " _
                & "  recd_ex_rate_ext " _
                & ")  " _
                & "VALUES ( " _
                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                & SetInteger(par_en_id) & ",  " _
                & SetInteger(par_bk_id) & ",  " _
                & SetInteger(par_cu_id) & ",  " _
                & SetDec(par_exc_rate) & ",  " _
                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                & SetDateNTime(CekTanggal) & ",  " _
                & SetDec(par_amount) & ",  " _
                & SetDateNTime00(par_date) & ",  " _
                & SetSetring(par_reff) & ",  " _
                & SetSetring(par_remarks) & ",  " _
                & SetSetring(par_type) & ",  " _
                & SetDec(par_amount * par_exc_rate) & "  " _
                & ")"

            par_ssqls.Add(sSQL)

        Catch ex As Exception
            update_rec = False
            Pesan(Err)
        End Try
        Return update_rec
    End Function

    Function update_rec(ByVal par_trans As Object, ByVal par_ssqls As ArrayList, ByVal par_en_id As String, ByVal par_bk_id As String, ByVal par_cu_id As String, _
                        ByVal par_exc_rate As String, ByVal par_amount As Double, ByVal par_date As String, _
                        ByVal par_reff As String, ByVal par_remarks As String, ByVal par_type As String) As Boolean
        update_rec = True
        Try

            With par_trans
                .Command.CommandType = CommandType.Text
                .Command.CommandText = "INSERT INTO  " _
                    & "  public.reconciliation_det " _
                    & "( " _
                    & "  recd_oid, " _
                    & "  recd_dom_id, " _
                    & "  recd_en_id, " _
                    & "  recd_bk_id, " _
                    & "  recd_cu_id, " _
                    & "  recd_ex_rate, " _
                    & "  recd_add_by, " _
                    & "  recd_add_date, " _
                    & "  recd_amount, " _
                    & "  recd_date, " _
                    & "  recd_reff, " _
                    & "  recd_remarks, " _
                    & "  recd_type, " _
                    & "  recd_ex_rate_ext " _
                    & ")  " _
                    & "VALUES ( " _
                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                    & SetInteger(par_en_id) & ",  " _
                    & SetInteger(par_bk_id) & ",  " _
                    & SetInteger(par_cu_id) & ",  " _
                    & SetDec(par_exc_rate) & ",  " _
                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                    & SetDateNTime(CekTanggal) & ",  " _
                    & SetDec(par_amount) & ",  " _
                    & SetDateNTime00(par_date) & ",  " _
                    & SetSetring(par_reff) & ",  " _
                    & SetSetring(par_remarks) & ",  " _
                    & SetSetring(par_type) & ",  " _
                    & SetDec(par_amount * par_exc_rate) & "  " _
                    & ")"

                par_ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()

            End With

        Catch ex As Exception
            update_rec = False
            Pesan(Err)
        End Try
        Return update_rec
    End Function

    Function reset_glbal(ByVal nomor_gl As String, ByVal par_ssqls As ArrayList) As Boolean
        Dim ssql As String
        Dim ssqls As New ArrayList
        Try


            'kalo misal normal D maka hasil akan + kalo normal C maka akan -
            ssql = "select glt_ac_id, coalesce(glt_debit,0) - coalesce(glt_credit,0) as hasil, " _
                    & "glt_dom_id,glt_en_id,glt_sb_id,glt_cc_id,glt_date,glt_cu_id from glt_det Where glt_code='" _
                    & nomor_gl & "'"


            Dim dt As New DataTable
            dt = GetTableData(ssql)
            For Each row As DataRow In dt.Rows

                ssql = "select gcal_oid from gcal_mstr " + _
                  " where gcal_start_date <=" + SetDateNTime00(row("glt_date")) + " " + _
                  " and gcal_end_date >=" + SetDateNTime00(row("glt_date")) + " "

                Dim gcal_oid As String = GetRowInfo(ssql)(0).ToString

                ssql = "select glbal_oid from glbal_balance WHERE " _
                    & " glbal_dom_id=" & SetInteger(row("glt_dom_id")) & "" _
                    & " and glbal_en_id=" & SetInteger(row("glt_en_id")) & "" _
                    & " and coalesce(glbal_sb_id,0)=" & SetNumber(row("glt_sb_id")) & "" _
                    & " and coalesce(glbal_cc_id,0)=" & SetNumber(row("glt_cc_id")) & "" _
                    & " and glbal_ac_id=" & SetInteger(row("glt_ac_id")) & "" _
                    & " and glbal_gcal_oid='" & gcal_oid & "'"

                If CekRowSelect(ssql) > 0 Then

                    'ini akan bug pada saat di sinkronisasikan.
                    ssql = "UPDATE  " _
                        & "  public.glbal_balance   " _
                        & "SET  " _
                        & "  glbal_upd_by = " & master_new.ClsVar.sNama & ",  " _
                        & "  glbal_upd_date = " & SetDateNTime(CekTanggal.ToString) & ",  " _
                        & "  glbal_balance_unposted = coalesce(glbal_balance_unposted,0) - " & SetDbl(row("hasil")) & ",  " _
                        & "  glbal_dt = " & SetDateNTime(CekTanggal.ToString) & "  " _
                        & "  " _
                        & "WHERE  " _
                        & "  glbal_oid = " & SetSetring(GetRowInfo(ssql)(0).ToString) & " "

                    ssqls.Add(ssql)

                End If
            Next

            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function

    Function update_glbal(ByVal par_ssqls As ArrayList, ByVal ac_id As String, ByVal hasil As Double, _
                          ByVal dom_id As String, ByVal en_id As String, _
                          ByVal sb_id As String, ByVal cc_id As String, _
                          ByVal glt_date As String, ByVal cu_id As String, ByVal exc_rate As Double, ByVal par_sign As String) As Boolean

        Dim ssql As String
        'Dim ssqls As New ArrayList
        Try

            'kalo misal normal D maka hasil akan + kalo normal C maka akan -

            ssql = "select gcal_oid from gcal_mstr " + _
              " where gcal_start_date <=" + SetDateNTime00(glt_date) + " " + _
              " and gcal_end_date >=" + SetDateNTime00(glt_date) + " "

            If CekRowSelect(ssql) = 0 Then
                Box("GL Periode does not exist")
                Return False
                Exit Function
            End If


            Dim gcal_oid As String = GetRowInfo(ssql)(0).ToString


            ssql = "select glbal_oid from glbal_balance WHERE " _
                & " glbal_dom_id=" & SetInteger(dom_id) & "" _
                & " and glbal_en_id=" & SetInteger(en_id) & "" _
                & " and coalesce(glbal_sb_id,0)=" & SetNumber(sb_id) & "" _
                & " and coalesce(glbal_cc_id,0)=" & SetNumber(cc_id) & "" _
                & " and glbal_ac_id=" & SetInteger(ac_id) & "" _
                & " and glbal_gcal_oid='" & gcal_oid & "'"


            If CekRowSelect(ssql) > 0 Then
                Dim _glbal_oid As String = GetRowInfo(ssql)(0).ToString

                Dim _amount As Double = 0

                ssql = "select ac_sign from ac_mstr where ac_id = " + ac_id

                If GetRowInfo(ssql)(0).ToString.ToUpper <> par_sign.ToUpper Then
                    _amount = hasil * -1
                Else
                    _amount = hasil
                End If

                'ini akan bug pada saat di sinkronisasikan.
                ssql = "UPDATE  " _
                    & "  public.glbal_balance   " _
                    & "SET  " _
                    & "  glbal_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                    & "  glbal_upd_date = " & SetDateNTime(CekTanggal.ToString) & ",  " _
                    & "  glbal_balance_unposted = coalesce(glbal_balance_unposted,0) + " & SetDbl(_amount) & ",  " _
                    & "  glbal_dt = " & SetDateNTime(CekTanggal.ToString) & "  " _
                    & "  " _
                    & "WHERE  " _
                    & "  glbal_oid = " & SetSetring(_glbal_oid) & " "

                par_ssqls.Add(ssql)
            Else
                ''ini akan bug pada saat di sinkronisasikan.
                'ssql = "INSERT INTO  " _
                '    & "  public.glbal_balance " _
                '    & "( " _
                '    & "  glbal_oid, " _
                '    & "  glbal_dom_id, " _
                '    & "  glbal_en_id, " _
                '    & "  glbal_add_by, " _
                '    & "  glbal_add_date, " _
                '    & "  glbal_gcal_oid, " _
                '    & "  glbal_ac_id, " _
                '    & "  glbal_sb_id, " _
                '    & "  glbal_cc_id, " _
                '    & "  glbal_cu_id, " _
                '    & "  glbal_balance_open, " _
                '    & "  glbal_balance_unposted, " _
                '    & "  glbal_balance_posted, " _
                '    & "  glbal_dt " _
                '    & ")  " _
                '    & "VALUES ( " _
                '    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                '    & SetInteger(dom_id) & ",  " _
                '    & SetInteger(en_id) & ",  " _
                '    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                '    & SetDateNTime(CekTanggal.ToString) & ",  " _
                '    & SetSetring(gcal_oid) & ",  " _
                '    & SetInteger(ac_id) & ",  " _
                '    & SetInteger(sb_id) & ",  " _
                '    & SetInteger(cc_id) & ",  " _
                '    & SetInteger(cu_id) & ",  " _
                '    & SetDbl(0) & ",  " _
                '    & SetDbl(hasil) & ",  " _
                '    & SetDbl(0) & ",  " _
                '    & SetDateNTime(CekTanggal.ToString) & "  " _
                '    & ")"
                'ssqls.Add(ssql)
                Box("Opening balance for account = " & GetIDByName("ac_mstr", "ac_name", "ac_id", ac_id) & " can not empty")

                Return False
                Exit Function
            End If

            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function

    Function insert_gl(ByVal par_ssqls As ArrayList, ByVal par_gl_code As String, ByVal par_ac_id As String, _
                       ByVal par_debit As Double, ByVal par_credit As Double, ByVal par_dom_id As String, ByVal par_en_id As String, _
                          ByVal par_sb_id As String, ByVal par_cc_id As String, ByVal par_glt_date As String, _
                          ByVal par_cu_id As String, ByVal par_exc_rate As Double, ByVal par_gl_type As String, _
                          ByVal par_seq As Integer, ByVal par_gl_desc As String, ByVal par_reff_oid As String, _
                          ByVal par_reff_code As String, ByVal par_day_book As String) As Boolean

        Dim sSQL As String = ""

        Try
            sSQL = "INSERT INTO  " _
                   & "  public.glt_det " _
                   & "( " _
                   & "  glt_oid, " _
                   & "  glt_dom_id, " _
                   & "  glt_en_id, " _
                   & "  glt_add_by, " _
                   & "  glt_add_date, " _
                   & "  glt_code, " _
                   & "  glt_date, " _
                   & "  glt_type, " _
                   & "  glt_cu_id, " _
                   & "  glt_exc_rate, " _
                   & "  glt_seq, " _
                   & "  glt_ac_id, " _
                   & "  glt_sb_id, " _
                   & "  glt_cc_id, " _
                   & "  glt_desc, " _
                   & "  glt_debit, " _
                   & "  glt_credit, " _
                   & "  glt_ref_oid, " _
                   & "  glt_ref_trans_code, " _
                   & "  glt_posted, " _
                   & "  glt_dt, " _
                   & "  glt_daybook " _
                   & ")  " _
                   & "VALUES ( " _
                   & SetSetring(Guid.NewGuid.ToString) & ",  " _
                   & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                   & SetInteger(par_en_id) & ",  " _
                   & SetSetring(master_new.ClsVar.sNama) & ",  " _
                   & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                   & SetSetring(par_gl_code) & ",  " _
                   & SetDate(par_glt_date) & ",  " _
                   & SetSetring(par_gl_type) & ",  " _
                   & SetInteger(par_cu_id) & ",  " _
                   & SetDbl(par_exc_rate) & ",  " _
                   & SetInteger(par_seq) & ",  " _
                   & SetInteger(par_ac_id) & ",  " _
                   & SetInteger(par_sb_id) & ",  " _
                   & SetInteger(par_cc_id) & ",  " _
                   & SetSetringDB(par_gl_desc) & ",  " _
                   & SetDblDB(par_debit) & ",  " _
                   & SetDblDB(par_credit) & ",  " _
                   & SetSetring(par_reff_oid) & ",  " _
                   & SetSetring(par_reff_code) & ",  " _
                   & SetSetring("N") & ",  " _
                   & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                   & SetSetring(par_day_book) & "  " _
                   & ")"
            par_ssqls.Add(sSQL)

            Return True
        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try
    End Function
    Function insert_gl(ByVal par_ssqls As ArrayList, ByVal par_gl_code As String, ByVal par_ac_id As String, _
                       ByVal par_debit As Double, ByVal par_credit As Double, ByVal par_dom_id As String, ByVal par_en_id As String, _
                          ByVal par_sb_id As String, ByVal par_cc_id As String, ByVal par_glt_date As String, _
                          ByVal par_cu_id As String, ByVal par_exc_rate As Double, ByVal par_gl_type As String, _
                          ByVal par_seq As Integer, ByVal par_gl_desc As String, ByVal par_reff_oid As String, _
                          ByVal par_reff_code As String, ByVal par_day_book As String, ByVal par_is_reverse As String) As Boolean

        Dim sSQL As String = ""

        Try
            sSQL = "INSERT INTO  " _
                   & "  public.glt_det " _
                   & "( " _
                   & "  glt_oid, " _
                   & "  glt_dom_id, " _
                   & "  glt_en_id, " _
                   & "  glt_add_by, " _
                   & "  glt_add_date, " _
                   & "  glt_code, " _
                   & "  glt_date, " _
                   & "  glt_type, " _
                   & "  glt_cu_id, " _
                   & "  glt_exc_rate, " _
                   & "  glt_seq, " _
                   & "  glt_ac_id, " _
                   & "  glt_sb_id, " _
                   & "  glt_cc_id, " _
                   & "  glt_desc, " _
                   & "  glt_debit, " _
                   & "  glt_credit, " _
                   & "  glt_ref_oid, " _
                   & "  glt_ref_trans_code, " _
                   & "  glt_posted, " _
                   & "  glt_dt, " _
                   & "  glt_daybook,glt_is_reverse " _
                   & ")  " _
                   & "VALUES ( " _
                   & SetSetring(Guid.NewGuid.ToString) & ",  " _
                   & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                   & SetInteger(par_en_id) & ",  " _
                   & SetSetring(master_new.ClsVar.sNama) & ",  " _
                   & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                   & SetSetring(par_gl_code) & ",  " _
                   & SetDate(par_glt_date) & ",  " _
                   & SetSetring(par_gl_type) & ",  " _
                   & SetInteger(par_cu_id) & ",  " _
                   & SetDbl(par_exc_rate) & ",  " _
                   & SetInteger(par_seq) & ",  " _
                   & SetInteger(par_ac_id) & ",  " _
                   & SetInteger(par_sb_id) & ",  " _
                   & SetInteger(par_cc_id) & ",  " _
                   & SetSetringDB(par_gl_desc) & ",  " _
                   & SetDblDB(par_debit) & ",  " _
                   & SetDblDB(par_credit) & ",  " _
                   & SetSetring(par_reff_oid) & ",  " _
                   & SetSetring(par_reff_code) & ",  " _
                   & SetSetring("N") & ",  " _
                   & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                   & SetSetring(par_day_book) & ",  " _
                   & par_is_reverse _
                   & ")"
            par_ssqls.Add(sSQL)

            Return True
        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try
    End Function

    Public Function limit_account(ByVal par_en_id As Integer) As Boolean
        Try
            Dim ssql As String

            ssql = "select coalesce(en_limit_account,'N') as en_limit_account from en_mstr where en_id=" & SetInteger(par_en_id)

            Dim dt As New DataTable

            dt = master_new.PGSqlConn.GetTableData(ssql)

            Dim _limit As Boolean = False

            For Each dr As DataRow In dt.Rows
                _limit = SetBitYNB(dr(0))
            Next

            Return _limit

        Catch ex As Exception
            MsgBox(ex.Message)
            Return True
        End Try
    End Function


    Public Function cek_duplikat_pt_id(ByVal par_gv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal par_kolom As String) As Boolean 'output false jika ada
        Try

            Dim _pt_id As Integer = 0
            Dim _cek As Integer = 0
            For i As Integer = 0 To par_gv.RowCount - 1
                _cek = 0
                _pt_id = par_gv.GetRowCellValue(i, par_kolom)
                For j As Integer = 0 To par_gv.RowCount - 1
                    If _pt_id = par_gv.GetRowCellValue(j, par_kolom) Then
                        _cek = _cek + 1
                    End If
                Next
                If _cek >= 2 Then
                    Dim pt_desc As String = MyPGDll.PGSqlConn.GetIDByName("pt_mstr", "pt_desc1", "pt_id", _pt_id)
                    Dim pt_code As String = MyPGDll.PGSqlConn.GetIDByName("pt_mstr", "pt_code", "pt_id", _pt_id)
                    Box("Part Number : " & pt_code & " " & pt_desc & " double")
                    Return False
                    Exit Function
                End If
            Next
            Return True
        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try
    End Function

    Public Function cek_trx_inv() As Boolean
        Try

            Dim _kode_hari As Integer = 0
            sSQL = "SELECT EXTRACT(DOW FROM CURRENT_TIMESTAMP)"
            _kode_hari = GetRowInfo(sSQL)(0)
            Dim _waktu As Date
            _waktu = CekTanggal()

            If _kode_hari = 5 Then
                'If Format(_waktu, "HH:mm") > konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\conf_system.txt"), "stop_inv") Then
                '    Box("Limit transaction on Friday at 15.00")
                '    Return False
                'End If

                If Format(_waktu, "HH:mm") > func_coll.get_conf_file("stop_inv") Then
                    Box("Limit transaction on Friday at " & func_coll.get_conf_file("stop_inv"))
                    Return False
                End If

                'func_coll.get_conf_file("stop_inv")
            End If

            Return True

        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

    Public Function cek_trx_cutting() As Boolean
        Try

            Dim _kode_hari As Integer = 0
            sSQL = "SELECT EXTRACT(DOW FROM CURRENT_TIMESTAMP)"
            _kode_hari = GetRowInfo(sSQL)(0)
            Dim _waktu As Date
            _waktu = CekTanggal()

            'If _kode_hari = 5 Then
            'If Format(_waktu, "HH:mm") > konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\conf_system.txt"), "stop_inv") Then
            '    Box("Limit transaction on Friday at 15.00")
            '    Return False
            'End If

            If Format(_waktu, "HH:mm") > func_coll.get_conf_file("stop_trx") Then
                Box("Limit transaction on Friday at " & func_coll.get_conf_file("stop_trx"))
                Return False
            End If

            'func_coll.get_conf_file("stop_inv")
            'End If

            Return True

        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

    Public Sub SetNumericOptions(ByVal series As Series, ByVal format As NumericFormat, ByVal precision As Integer)
        series.PointOptions.ValueNumericOptions.Format = format
        series.PointOptions.ValueNumericOptions.Precision = precision
    End Sub


    Public Function pembulatan(ByVal par_nilai As Double) As Double
        Try
            Dim _hasil As Double
            If par_nilai > 0 Then
                'par_nilai = 0

                Dim _nilai_akhir As String
                Dim _nilai_temp As Double
                Dim _nilai_kiri As String

                _nilai_akhir = Microsoft.VisualBasic.Right(Math.Round(par_nilai * 100, 0).ToString, 4)
                _nilai_kiri = Microsoft.VisualBasic.Left(Math.Round(par_nilai, 0).ToString, Math.Round(par_nilai, 0).ToString.Length - 2)
                'If CDbl(_nilai_akhir) > CDbl(5000) And CDbl(_nilai_akhir) <= CDbl(9999) Then
                '    _nilai_temp = CDbl(_nilai_kiri & "00") + 100
                'ElseIf CDbl(_nilai_akhir) > CDbl(0) And CDbl(_nilai_akhir) <= CDbl(5000) Then
                '    _nilai_temp = CDbl(_nilai_kiri & "00") + 50
                'Else
                '    _nilai_temp = CDbl(_nilai_kiri & "00")
                'End If

                If CDbl(_nilai_akhir) > CDbl(0) And CDbl(_nilai_akhir) <= CDbl(9999) Then
                    _nilai_temp = CDbl(_nilai_kiri & "00") + 100

                Else
                    _nilai_temp = CDbl(_nilai_kiri & "00")
                End If

                _hasil = _nilai_temp
            Else
                _hasil = par_nilai
            End If
            Return _hasil
        Catch ex As Exception
            Pesan(Err)
            Return 0
        End Try
    End Function


    Public Function get_data_from_api(ByVal par_url As String, ByVal par_error As ArrayList) As Object
        Try
            Dim request As WebRequest = WebRequest.Create(par_url)

            ' If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials
            ' Get the response.
            Dim response As WebResponse = request.GetResponse()
            ' Display the status.
            'Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
            ' Get the stream containing content returned by the server.
            Dim dataStream As Stream = response.GetResponseStream()
            ' Open the stream using a StreamReader for easy access.
            Dim reader As New StreamReader(dataStream)
            ' Read the content.
            Dim responseFromServer As String = reader.ReadToEnd()
            'Dim responseFromServer As String = reader.ReadToEnd()
            'Console.WriteLine(responseFromServer)
            'Dim result = JsonConvert.DeserializeObject(Of ArrayList)(responseFromServer)

            'Return result
            Return responseFromServer
        Catch ex As Exception
            'MsgBox(ex.Message)
            ' _MakeReport(ex.Message)
            par_error.Add(ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function postData(ByVal dictData As Dictionary(Of String, Object), ByVal actionData As String, ByVal par_url As String) As String
        Dim webClient As New WebClient()
        Dim resByte As Byte()
        Dim reqByte As Byte()
        Dim resString As String
        Dim reqString() As Byte

        Try
            webClient.Headers("content-type") = "application/json"
            reqString = Encoding.Default.GetBytes(JsonConvert.SerializeObject(dictData, Formatting.Indented))
            resByte = webClient.UploadData(par_url, actionData, reqString)

            resString = Encoding.Default.GetString(resByte)

            webClient.Dispose()
            Return resString
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return False
    End Function

    Public Function getData(ByVal par_url As String) As String

        Dim webClient As New WebClient()
        Dim resByte As Byte()
        Dim reqByte As Byte()
        Dim resString As String
        Dim reqString() As Byte

        Try
            webClient.Headers("content-type") = "application/json"
            resByte = webClient.DownloadData(par_url)
            resString = Encoding.Default.GetString(resByte)

            webClient.Dispose()
            Return resString
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return False
    End Function


    Public Class PieExplodingHelper
        Public Const None As String = "None"
        Public Const All As String = "All"
        Public Const MinValue As String = "Min Value"
        Public Const MaxValue As String = "Max Value"
        Public Const Custom As String = "Custom"
        Private Sub New()
        End Sub
        Private Shared Function CreateFilter(ByVal mode As String) As SeriesPointFilter
            Return New SeriesPointFilter(SeriesPointKey.Argument, DataFilterCondition.Equal, mode)
        End Function
        Private Shared Sub ApplyFilterMode(ByVal view As PieSeriesViewBase, ByVal mode As String)
            view.ExplodedPointsFilters.Clear()
            view.ExplodedPointsFilters.Add(CreateFilter(mode))
            view.ExplodeMode = PieExplodeMode.UseFilters
        End Sub
        Public Shared Function CreateModeList(ByRef points As SeriesPointCollection, ByVal supportCustom As Boolean) As List(Of String)
            Dim list As List(Of String) = New List(Of String)()
            list.Add(None)
            list.Add(All)
            list.Add(MinValue)
            list.Add(MaxValue)
            For Each point As SeriesPoint In points
                list.Add(point.Argument)
            Next
            If supportCustom Then
                list.Add(Custom)
            End If
            Return list
        End Function
        Public Shared Sub ApplyMode(ByVal view As PieSeriesViewBase, ByVal mode As String)
            Select Case mode
                Case Custom
                    Return
                Case None
                    view.ExplodeMode = PieExplodeMode.None
                Case All
                    view.ExplodeMode = PieExplodeMode.All
                Case MinValue
                    view.ExplodeMode = PieExplodeMode.MinValue
                Case MaxValue
                    view.ExplodeMode = PieExplodeMode.MaxValue
                Case Else
                    ApplyFilterMode(view, mode)
            End Select
        End Sub

    End Class

    Public Sub ExportToExcel(ByVal dt As System.Data.DataTable, ByVal outputPath As String)
        Try

            ' Create the Excel Application object
            'Dim excelApp As New Microsoft.Office.Interop.Excel.ApplicationClass()

            '' Create a new Excel Workbook
            'Dim excelWorkbook As Microsoft.Office.Interop.Excel.Workbook = excelApp.Workbooks.Add(Type.Missing)

            'Dim sheetIndex As Integer = 0
            'Dim col, row As Integer
            'Dim excelSheet As Microsoft.Office.Interop.Excel.Worksheet

            '' Copy each DataTable as a new Sheet
            ''For Each dt As System.Data.DataTable In dataSet.Tables

            'sheetIndex += 1

            '' Copy the DataTable to an object array
            'Dim rawData(dt.Rows.Count, dt.Columns.Count - 1) As Object

            '' Copy the column names to the first row of the object array
            'For col = 0 To dt.Columns.Count - 1
            '    rawData(0, col) = dt.Columns(col).ColumnName
            'Next

            '' Copy the values to the object array
            'For col = 0 To dt.Columns.Count - 1
            '    For row = 0 To dt.Rows.Count - 1
            '        rawData(row + 1, col) = dt.Rows(row).ItemArray(col)
            '    Next
            'Next

            '' Calculate the final column letter
            'Dim finalColLetter As String = String.Empty
            'Dim colCharset As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            'Dim colCharsetLen As Integer = colCharset.Length

            'If dt.Columns.Count > colCharsetLen Then
            '    finalColLetter = colCharset.Substring( _
            '     (dt.Columns.Count - 1) \ colCharsetLen - 1, 1)
            'End If

            'finalColLetter += colCharset.Substring( _
            '  (dt.Columns.Count - 1) Mod colCharsetLen, 1)

            '' Create a new Sheet
            'excelSheet = CType( _
            '    excelWorkbook.Sheets.Add(excelWorkbook.Sheets(sheetIndex), _
            '    Type.Missing, 1, Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet), Microsoft.Office.Interop.Excel.Worksheet)

            'excelSheet.Name = dt.TableName

            '' Fast data export to Excel
            'Dim excelRange As String = String.Format("A1:{0}{1}", finalColLetter, dt.Rows.Count + 1)
            'excelSheet.Range(excelRange, Type.Missing).Value2 = rawData

            '' Mark the first row as BOLD
            'CType(excelSheet.Rows(1, Type.Missing), Microsoft.Office.Interop.Excel.Range).Font.Bold = True

            'excelSheet = Nothing
            ''Next

            '' Save and Close the Workbook
            'excelWorkbook.SaveAs(outputPath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, _
            ' Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, _
            ' Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)

            'excelWorkbook.Close(True, Type.Missing, Type.Missing)

            'excelWorkbook = Nothing

            '' Release the Application object
            'excelApp.Quit()
            'excelApp = Nothing

            '' Collect the unreferenced objects
            'GC.Collect()
            'GC.WaitForPendingFinalizers()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    'Microsoft.Office.Interop.Excel
    Public Sub ExportToExcelFast(ByVal dataSet As DataSet, ByVal outputPath As String)
        Try

            ' Create the Excel Application object
            'Dim excelApp As New Microsoft.Office.Interop.Excel.ApplicationClass()

            '' Create a new Excel Workbook
            'Dim excelWorkbook As Microsoft.Office.Interop.Excel.Workbook = excelApp.Workbooks.Add(Type.Missing)

            'Dim sheetIndex As Integer = 0
            'Dim col, row As Integer
            'Dim excelSheet As Microsoft.Office.Interop.Excel.Worksheet

            '' Copy each DataTable as a new Sheet
            'For Each dt As System.Data.DataTable In dataSet.Tables

            '    sheetIndex += 1

            '    ' Copy the DataTable to an object array
            '    Dim rawData(dt.Rows.Count, dt.Columns.Count - 1) As Object

            '    ' Copy the column names to the first row of the object array
            '    For col = 0 To dt.Columns.Count - 1
            '        rawData(0, col) = dt.Columns(col).ColumnName
            '    Next

            '    ' Copy the values to the object array
            '    For col = 0 To dt.Columns.Count - 1
            '        For row = 0 To dt.Rows.Count - 1
            '            rawData(row + 1, col) = dt.Rows(row).ItemArray(col)
            '        Next
            '    Next

            '    ' Calculate the final column letter
            '    Dim finalColLetter As String = String.Empty
            '    Dim colCharset As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            '    Dim colCharsetLen As Integer = colCharset.Length

            '    If dt.Columns.Count > colCharsetLen Then
            '        finalColLetter = colCharset.Substring( _
            '         (dt.Columns.Count - 1) \ colCharsetLen - 1, 1)
            '    End If

            '    finalColLetter += colCharset.Substring( _
            '      (dt.Columns.Count - 1) Mod colCharsetLen, 1)

            '    ' Create a new Sheet
            '    excelSheet = CType( _
            '        excelWorkbook.Sheets.Add(excelWorkbook.Sheets(sheetIndex), _
            '        Type.Missing, 1, Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet), Microsoft.Office.Interop.Excel.Worksheet)

            '    excelSheet.Name = dt.TableName

            '    ' Fast data export to Excel
            '    Dim excelRange As String = String.Format("A1:{0}{1}", finalColLetter, dt.Rows.Count + 1)
            '    excelSheet.Range(excelRange, Type.Missing).Value2 = rawData

            '    ' Mark the first row as BOLD
            '    CType(excelSheet.Rows(1, Type.Missing), Microsoft.Office.Interop.Excel.Range).Font.Bold = True

            '    excelSheet = Nothing
            'Next

            '' Save and Close the Workbook
            'excelWorkbook.SaveAs(outputPath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, _
            ' Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, _
            ' Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)

            'excelWorkbook.Close(True, Type.Missing, Type.Missing)

            'excelWorkbook = Nothing

            '' Release the Application object
            'excelApp.Quit()
            'excelApp = Nothing

            '' Collect the unreferenced objects
            'GC.Collect()
            'GC.WaitForPendingFinalizers()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub HttpUploadFile(ByVal uri As String, ByVal filePath As String, ByVal fileParameterName As String, ByVal contentType As String)

        Dim myFile As New FileInfo(filePath)
        Dim sizeInBytes As Long = myFile.Length

        Dim boundary As String = "---------------------------" & DateTime.Now.Ticks.ToString("x")
        Dim newLine As String = System.Environment.NewLine
        Dim boundaryBytes As Byte() = Encoding.ASCII.GetBytes(newLine & "--" & boundary & newLine)
        Dim request As Net.HttpWebRequest = Net.WebRequest.Create(uri)
        request.ContentType = "multipart/form-data; boundary=" & boundary
        request.Method = "POST"
        request.KeepAlive = True
        'request.Credentials = Net.CredentialCache.DefaultCredentials

        Using requestStream As IO.Stream = request.GetRequestStream()
            Dim formDataTemplate As String = "Content-Disposition: form-data; name=""{0}""{1}{1}{2}"
            requestStream.Write(boundaryBytes, 0, boundaryBytes.Length)

            Dim headerTemplate As String = "Content-Disposition: form-data; name=""{0}""; filename=""{1}""{2}Content-Type: {3};"
            Dim header As String = String.Format(headerTemplate, fileParameterName, filePath, newLine, contentType)
            header = header & vbNewLine & "Content-Length: " & sizeInBytes.ToString & vbNewLine
            header = header & "Expect: 100-continue" & vbNewLine & vbNewLine

            'MsgBox(header)
            Debug.Print(header)

            Dim headerBytes As Byte() = Encoding.UTF8.GetBytes(header)
            requestStream.Write(headerBytes, 0, header.Length)

            Using fileStream As New IO.FileStream(filePath, IO.FileMode.Open, IO.FileAccess.Read)
                Dim buffer(4096) As Byte
                Dim bytesRead As Int32 = fileStream.Read(buffer, 0, buffer.Length)
                Do While (bytesRead > 0)
                    requestStream.Write(buffer, 0, bytesRead)
                    bytesRead = fileStream.Read(buffer, 0, buffer.Length)
                Loop
            End Using
            Dim trailer As Byte() = Encoding.ASCII.GetBytes(newLine & "--" + boundary + "--" & newLine)
            requestStream.Write(trailer, 0, trailer.Length)
            requestStream.Close()
        End Using


        Dim response As Net.WebResponse = Nothing
        Try
            response = request.GetResponse()
            Using responseStream As IO.Stream = response.GetResponseStream()
                Using responseReader As New IO.StreamReader(responseStream)
                    Dim responseText = responseReader.ReadToEnd()
                    Debug.Print(responseText)
                End Using
            End Using
        Catch exception As Net.WebException
            response = exception.Response
            If (response IsNot Nothing) Then
                Using reader As New IO.StreamReader(response.GetResponseStream())
                    Dim responseText = reader.ReadToEnd()
                    Diagnostics.Debug.Write(responseText)
                End Using
                response.Close()
            End If
        Finally
            request = Nothing
        End Try
    End Sub

End Module
