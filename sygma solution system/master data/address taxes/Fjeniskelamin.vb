Public Class Fjeniskelamin
    Private Sub FPosition_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _code_field = "Jenis_Kelamin"
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "code_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "IsDefault", "code_default", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "IsActive", "code_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sequence", "code_usr_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "CodeUser2", "code_usr_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "CodeUser3", "code_usr_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "CodeUser4", "code_usr_4", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "CodeUser5", "code_usr_5", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "code_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "code_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "code_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "code_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
