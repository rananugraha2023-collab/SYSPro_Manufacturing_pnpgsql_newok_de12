Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FBSSetting_old

    Dim _loc_oid_mstr As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FDomain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        Try
            init_le(le_account, "account_all")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Code", "code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remark", "remark", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Code Hirarki", "ac_code_hirarki", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account", "ac_name", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
            & "  a.code, " _
            & "  a.remark, " _
            & "  CAST(a.setting as integer) as setting, " _
            & "  b.ac_name,b.ac_code,b.ac_code_hirarki " _
            & "FROM " _
            & "  public.tconfsettingacc a " _
            & "  INNER JOIN  public.ac_mstr b on (CAST(a.setting as integer)=b.ac_id)" _
            & "ORDER BY a.code"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        cancel_data()
        Box("Menu not available")
    End Sub

    Public Overrides Function insert() As Boolean
        insert = True
        Box("Menu not available")
        Return insert
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            Code.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                Code.Text = .Item("code")
                remark.Text = .Item("remark")
                le_account.EditValue = .Item("setting")
            End With

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim ssql As String
        Try
            ssql = "update tconfsettingacc set setting='" & le_account.EditValue & "' where code='" & Code.EditValue & "'"
            dbrun(ssql)
            edit = True


            after_success()
            set_row(Trim(Code.Text), "code")
            'dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
        Catch ex As Exception
            Pesan(Err)
            edit = False
        End Try
        Return edit
    End Function

    Public Overrides Function delete_data() As Boolean
        delete_data = True
        Box("Menu not available")
        Return delete_data
    End Function

End Class
