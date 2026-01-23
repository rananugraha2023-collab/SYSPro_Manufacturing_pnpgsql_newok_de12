Imports master_new.ModFunction

Public Class FShow
    Public par_dt As DataTable

    Private Sub FAccountSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        'Me.Width = 800
        'Me.Height = 500
    End Sub

    Public Overrides Sub format_grid()

        If fobject.name = FRosGenerate.Name Then
            'add_column(gv_master, "ID", "ac_id1", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Code", "ac_code1", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Name", "ac_name1", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Sign", "ac_sign1", DevExpress.Utils.HorzAlignment.Default)

            'add_column(gv_master, "ID", "ac_id2", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Code", "ac_code2", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Name", "ac_name2", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Sign", "ac_sign2", DevExpress.Utils.HorzAlignment.Default)

            gv_master.OptionsBehavior.AutoPopulateColumns = True
            gc_master.DataSource = par_dt
            gv_master.BestFitColumns()
        ElseIf fobject.name = FRosView.Name Then

            add_column(gv_master, "GL Number", "rsthistory_glt_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Amount", "rsthistory_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

            add_column(gv_master, "ID1", "rsthistory_ac_id1", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Code1", "ac_code1", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Name1", "ac_name1", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Sign1", "rsthistory_ac_sign1", DevExpress.Utils.HorzAlignment.Default)

            add_column(gv_master, "ID2", "rsthistory_ac_id2", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Code2", "ac_code2", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Name2", "ac_name2", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Sign2", "rsthistory_ac_sign2", DevExpress.Utils.HorzAlignment.Default)

            gc_master.DataSource = par_dt
            gv_master.BestFitColumns()
        End If
      
    End Sub



End Class
