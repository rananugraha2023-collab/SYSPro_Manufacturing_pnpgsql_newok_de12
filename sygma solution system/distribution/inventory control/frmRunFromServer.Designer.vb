<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRunFromServer
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl
        Me.BtInsertToUpdate = New DevExpress.XtraEditors.SimpleButton
        Me.BtGetSQL = New DevExpress.XtraEditors.SimpleButton
        Me.BtAmbilServer = New DevExpress.XtraEditors.SimpleButton
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_SQL = New DevExpress.XtraTab.XtraTabPage
        Me.me_sql = New DevExpress.XtraEditors.MemoEdit
        Me.xtp_LOG = New DevExpress.XtraTab.XtraTabPage
        Me.me_log = New DevExpress.XtraEditors.MemoEdit
        Me.LblStatus = New DevExpress.XtraEditors.LabelControl
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.xtp_SQL.SuspendLayout()
        CType(Me.me_sql.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_LOG.SuspendLayout()
        CType(Me.me_log.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Horizontal = False
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.LblStatus)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.BtInsertToUpdate)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.BtGetSQL)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.BtAmbilServer)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.XtraTabControl1)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(807, 442)
        Me.SplitContainerControl1.SplitterPosition = 35
        Me.SplitContainerControl1.TabIndex = 1
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'BtInsertToUpdate
        '
        Me.BtInsertToUpdate.Location = New System.Drawing.Point(239, 5)
        Me.BtInsertToUpdate.Name = "BtInsertToUpdate"
        Me.BtInsertToUpdate.Size = New System.Drawing.Size(101, 23)
        Me.BtInsertToUpdate.TabIndex = 4
        Me.BtInsertToUpdate.Text = "Insert to Update"
        '
        'BtGetSQL
        '
        Me.BtGetSQL.Location = New System.Drawing.Point(9, 6)
        Me.BtGetSQL.Name = "BtGetSQL"
        Me.BtGetSQL.Size = New System.Drawing.Size(85, 23)
        Me.BtGetSQL.TabIndex = 0
        Me.BtGetSQL.Text = "Get SQL"
        '
        'BtAmbilServer
        '
        Me.BtAmbilServer.Location = New System.Drawing.Point(107, 5)
        Me.BtAmbilServer.Name = "BtAmbilServer"
        Me.BtAmbilServer.Size = New System.Drawing.Size(120, 23)
        Me.BtAmbilServer.TabIndex = 0
        Me.BtAmbilServer.Text = "Execute from Server"
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.xtp_SQL
        Me.XtraTabControl1.Size = New System.Drawing.Size(807, 401)
        Me.XtraTabControl1.TabIndex = 0
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_SQL, Me.xtp_LOG})
        '
        'xtp_SQL
        '
        Me.xtp_SQL.Controls.Add(Me.me_sql)
        Me.xtp_SQL.Name = "xtp_SQL"
        Me.xtp_SQL.Size = New System.Drawing.Size(800, 372)
        Me.xtp_SQL.Text = "SQL"
        '
        'me_sql
        '
        Me.me_sql.Dock = System.Windows.Forms.DockStyle.Fill
        Me.me_sql.Location = New System.Drawing.Point(0, 0)
        Me.me_sql.Name = "me_sql"
        Me.me_sql.Size = New System.Drawing.Size(800, 372)
        Me.me_sql.TabIndex = 0
        '
        'xtp_LOG
        '
        Me.xtp_LOG.Controls.Add(Me.me_log)
        Me.xtp_LOG.Name = "xtp_LOG"
        Me.xtp_LOG.Size = New System.Drawing.Size(800, 372)
        Me.xtp_LOG.Text = "LOG"
        '
        'me_log
        '
        Me.me_log.Dock = System.Windows.Forms.DockStyle.Fill
        Me.me_log.Location = New System.Drawing.Point(0, 0)
        Me.me_log.Name = "me_log"
        Me.me_log.Size = New System.Drawing.Size(800, 372)
        Me.me_log.TabIndex = 1
        '
        'LblStatus
        '
        Me.LblStatus.Location = New System.Drawing.Point(401, 11)
        Me.LblStatus.Name = "LblStatus"
        Me.LblStatus.Size = New System.Drawing.Size(4, 13)
        Me.LblStatus.TabIndex = 5
        Me.LblStatus.Text = "-"
        '
        'frmRunFromServer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(807, 442)
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.Name = "frmRunFromServer"
        Me.Text = "frmRunFromServer"
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.xtp_SQL.ResumeLayout(False)
        CType(Me.me_sql.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_LOG.ResumeLayout(False)
        CType(Me.me_log.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents BtInsertToUpdate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtGetSQL As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtAmbilServer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_SQL As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents me_sql As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents xtp_LOG As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents me_log As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LblStatus As DevExpress.XtraEditors.LabelControl
End Class
